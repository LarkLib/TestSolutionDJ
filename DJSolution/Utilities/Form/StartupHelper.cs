using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DJ.LMS.Utilities
{
    /// <summary>
    /// 用来方便实现程序只允许一个实例，以及设置/取消程序随着软件启动而启动的操作
    /// </summary>
    public class StartupHelper
    {
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr intptr_0, int int_1);
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr intptr_0);
        private const int int_0 = 1;
        private static RegistryKey registryKey_0 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        /// <summary>    
        /// 软件是否设置系统自动启动    
        /// </summary>    
        /// <param name="app">软件名称</param>    
        /// <returns></returns> 
        public static bool WillRunAtStartup(string app)
        {
            bool result;
            try
            {
                result = object.Equals(StartupHelper.registryKey_0.GetValue(app), Environment.CommandLine);
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        /// <summary>    
        /// 系统设置/取消自动启动    
        /// </summary>    
        /// <param name="app">软件名称</param>    
        /// <param name="shouldRun">设置/取消自动启动</param> 
        public static void RunAtStartup(string app, bool shouldRun)
        {
            StartupHelper.RunAtStartup(app, shouldRun, Environment.CommandLine);
        }
        /// <summary>    
        /// 系统设置/取消自动启动    
        /// </summary>    
        /// <param name="app">软件名称</param>    
        /// <param name="shouldRun">是否设置</param>    
        /// <param name="exePath">系统执行路径（可增加配置参数）</param>
        public static void RunAtStartup(string app, bool shouldRun, string exePath)
        {
            try
            {
                if (shouldRun)
                {
                    StartupHelper.registryKey_0.SetValue(app, exePath);
                }
                else
                {
                    StartupHelper.registryKey_0.DeleteValue(app, false);
                }
            }
            catch (Exception arg)
            {
                Trace.WriteLine("Unable to RunAtStartup: " + arg);
            }
        }
        /// <summary>    
        /// 获取软件运行的系统进程对象    
        /// </summary>    
        /// <returns></returns>
        public static Process RunningInstance()
        {
            Process currentProcess = Process.GetCurrentProcess();
            Process[] processesByName = Process.GetProcessesByName(currentProcess.ProcessName);
            Process[] array = processesByName;
            Process result;
            for (int i = 0; i < array.Length; i++)
            {
                Process process = array[i];
                if (process.Id != currentProcess.Id)
                {
                    result = process;
                    return result;
                }
            }
            result = null;
            return result;
        }
        /// <summary>    
        /// 处理重复运行的事件    
        /// </summary>    
        /// <param name="instance">系统进程对象</param>
        public static void HandleRunningInstance(Process instance)
        {
            StartupHelper.HandleRunningInstance(instance, null);
        }
        /// <summary>    
        /// 处理重复运行的事件    
        /// </summary>    
        /// <param name="instance">系统进程对象</param>    
        /// <param name="message">提示消息</param>
        public static void HandleRunningInstance(Process instance, string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                MessageUtil.ShowWarning(message);
            }
            StartupHelper.ShowWindowAsync(instance.MainWindowHandle, 1);
            StartupHelper.SetForegroundWindow(instance.MainWindowHandle);
        }        
    }
}
