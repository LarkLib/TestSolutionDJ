using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Axure;
using Axure.Client;
using Axure.Client.Dialogs;
using Axure.Client.Dialogs.Error;
using Axure.Client.Dialogs.Infos;
using Axure.Client.Win32;
using Axure.Client.Win32.WinApi;
using Axure.Generators.Generators;
using Axure.International;
using Axure.IO;
using Axure.Platform;
using Axure.Platform.Configuration;
using Axure.Platform.Win32;
using Axure.Platform.Win32.Direct2d;
using Axure.Platform.Win32.WinApi;
using Axure.Win32Wrapper.Core;
using Axure.Win32Wrapper.Ole32;
using Axure.Win32Wrapper.Windows;
using Microsoft.Win32;

namespace AxureRP;

public static class Launcher
{
	private const int DEFAULT_MIN_THREADS = 1000;

	[DllImport("Kernel32.dll")]
	public static extern bool AttachConsole(int processId);

	[STAThread]
	public static void Main(string[] args)
	{
		ThreadPool.SetMinThreads(1000, 1000);
		string text = "";
		if (args.Length != 0)
		{
			text = args[0];
		}
		if (!text.StartsWith("Make") && !CanLaunch())
		{
			return;
		}
		AxPlatform.ThrownException += AxPlatform_ThrownException;
		Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
		Application.ThreadException += Application_ThreadException;
		WindowCore.UnhandledException += WindowCore_UnhandledException;
		AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
		Application.EnableVisualStyles();
		Dx.Init();
		if (text == "MakePrototypeResources")
		{
			AttachConsole(-1);
			if (args.Length < 3)
			{
				Console.Out.WriteLine("Usage:\nAxureRP9.exe MakePrototypeResources prototypeFilesDir outputDir");
				return;
			}
			string sourcePrototypeResDir = args[1];
			string destDir = args[2];
			HtmlPrototypeGenerator.MakeBasePrototypeResourcesDir(sourcePrototypeResDir, destDir, doCombineJs: true);
			Console.Out.WriteLine(text + " done.");
			return;
		}
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(defaultValue: false);
		AxPlatform.RegisterPlatformInformation(new W32PlatformInformation());
		AxPlatform.RegisterPlatformServiceProvider(new WxPlatformServices());
		if (AxureCli.Run(args, delegate
		{
			AttachConsole(-1);
		}))
		{
			return;
		}
		AxPlatform.CommandLineArguments = args;
		Ole32Methods.OleInitialize(IntPtr.Zero).Check();
		try
		{
			Local.LoadTranslationFile();
		}
		catch (Exception)
		{
		}
		WxClient wxClient = WPFClientLauncher.CreateApp(args);
		if (wxClient != null)
		{
			ThreadPool.GetMinThreads(out var workerThreads, out var completionPortThreads);
			if (workerThreads < 1000 && ThreadPool.SetMinThreads(1000, completionPortThreads))
			{
				ThreadPool.GetMinThreads(out workerThreads, out completionPortThreads);
			}
			WxWindow wxWindow = (WxWindow)wxClient.Form;
			wxWindow.Center();
			wxWindow.Show();
			WxApp.Current.Run(wxWindow);
		}
	}

	private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
	{
		ReportException(e.Exception);
	}

	private static void WindowCore_UnhandledException(WindowException windowException)
	{
		ReportException(windowException);
		windowException.IsHandled = true;
	}

	private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
	{
		ReportException((Exception)e.ExceptionObject);
	}

	public static void ReportException(Exception ex)
	{
		ex.ReportException();
	}

	private static void AxPlatform_ThrownException(Exception ex)
	{
		Exception innerException = ex.InnerException;
		if (innerException != null)
		{
			while (innerException.InnerException != null)
			{
				innerException = innerException.InnerException;
			}
		}
		if (innerException != null && innerException.Message != null && innerException.Message.StartsWith("url"))
		{
			string message = innerException.Message;
			OpenUrlErrorDialog.ShowOpenUrlError(message.Substring(message.IndexOf(":") + 1));
		}
		else if (ex.Message != null && ex.Message.StartsWith("url"))
		{
			string message2 = ex.Message;
			OpenUrlErrorDialog.ShowOpenUrlError(message2.Substring(message2.IndexOf(":") + 1));
		}
		else if (ex is PackageException || ex.InnerException is PackageException)
		{
			DocumentErrorDialog.HandlePackageError((ex as PackageException) ?? (ex.InnerException as PackageException));
		}
		else if (ex is PackageAggregateException || ex.InnerException is PackageAggregateException)
		{
			DocumentErrorDialog.HandlePackageErrors((ex as PackageAggregateException) ?? (ex.InnerException as PackageAggregateException));
		}
		else
		{
			AxClient.Dialogs.ShowDialogInfo(new FeedbackDialogInfo(ex));
		}
	}

	private static bool CanLaunch()
	{
		try
		{
			if (!NeedsWindows7PlatformUpdate())
			{
				return true;
			}
			string text = Configurations.ProductName(version: false, edition: false);
			if (MessageBox.Show($"{text} requires Platform Update for Windows 7.\r\nPlease install Platform Update using Windows Update or\r\nclick \ufffdOK\ufffd for more information and to download from Microsoft.", text, MessageBoxButtons.OKCancel) == DialogResult.OK)
			{
				Process.Start("https://support.microsoft.com/en-us/help/2670838/platform-update-for-windows-7-sp1-and-windows-server-2008-r2-sp1");
			}
			return false;
		}
		catch (Exception e)
		{
			Logging.LogError("Can't look for Win7 PU. OS Version: " + Environment.OSVersion);
			Logging.LogException(e);
		}
		return true;
	}

	private static RegistryKey GetHKLMKey(string registryPath)
	{
		RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(registryPath);
		if (registryKey != null)
		{
			return registryKey;
		}
		return RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(registryPath);
	}

	private static bool NeedsWindows7PlatformUpdate()
	{
		if (Environment.OSVersion.Version.ToAxVersion() >= W32PlatformInformation.OSVersionWindows8)
		{
			return false;
		}
		RegistryKey hKLMKey = GetHKLMKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Component Based Servicing\\Packages");
		if (hKLMKey.OpenSubKey("Microsoft-Windows-PlatformUpdate-Win7-SRV08R2-Package~31bf3856ad364e35~amd64~~7.1.7601.16492") != null)
		{
			return false;
		}
		if (hKLMKey.OpenSubKey("Microsoft-Windows-PlatformUpdate-Win7-SRV08R2-Package~31bf3856ad364e35~x86~~7.1.7601.16492") != null)
		{
			return false;
		}
		string[] subKeyNames = hKLMKey.GetSubKeyNames();
		for (int i = 0; i < subKeyNames.Length; i++)
		{
			if (subKeyNames[i].StartsWith("Microsoft-Windows-PlatformUpdate-Win7-SRV08R2-Package~"))
			{
				return false;
			}
		}
		return true;
	}
}
