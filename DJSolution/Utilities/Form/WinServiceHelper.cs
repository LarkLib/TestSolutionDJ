using Microsoft.Win32;
using System;
using System.Collections;
using System.Configuration.Install;
using System.ServiceProcess;

namespace DJ.LMS.Utilities
{
    /// <summary>
    /// 用来方便实现Window服务的各种操作，包括安装、卸载、启动、停止、重新启动、判断服务是否存在等操作
    /// </summary>
    public class WinServiceHelper
    {
        /// 安装Windows服务    
        /// </summary>    
        /// <param name="serviceName">服务名称</param>    
        /// <param name="serviceFileName">服务文件路径</param>    
        /// <returns></returns>
        public static bool InstallService(string serviceName, string serviceFileName)
        {
            if (WinServiceHelper.ServiceIsExisted(serviceName))
            {
                throw new Exception(string.Format("{0} 服务已经存在", serviceName));
            }
            string[] commandLine = new string[0];
            TransactedInstaller transactedInstaller = new TransactedInstaller();
            AssemblyInstaller value = new AssemblyInstaller(serviceFileName, commandLine);
            transactedInstaller.Installers.Add(value);
            transactedInstaller.Install(new Hashtable());
            return true;
        }
        /// <summary>    
        /// 卸载Windows服务    
        /// </summary>    
        /// <param name="serviceName">服务名称</param>    
        /// <param name="serviceFileName">服务文件路径</param>
        public static bool UnInstallService(string serviceName, string serviceFileName)
        {
            bool result;
            if (WinServiceHelper.ServiceIsExisted(serviceName))
            {
                string[] commandLine = new string[0];
                TransactedInstaller transactedInstaller = new TransactedInstaller();
                AssemblyInstaller value = new AssemblyInstaller(serviceFileName, commandLine);
                transactedInstaller.Installers.Add(value);
                transactedInstaller.Uninstall(null);
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
        /// <summary>    
        /// 另外一种安装、卸载Windows服务的方法    
        /// </summary>    
        /// <param name="install">安装还是卸载，true为安装，false为卸载</param>    
        /// <param name="serviceFileName">服务文件路径</param>
        public static void InstallService2(bool install, string serviceFileName)
        {
            if (install)
            {
                AssemblyInstaller assemblyInstaller = new AssemblyInstaller();
                IDictionary dictionary = new Hashtable();
                assemblyInstaller.UseNewContext = true;
                assemblyInstaller.Path = serviceFileName;
                dictionary.Clear();
                assemblyInstaller.Install(dictionary);
                assemblyInstaller.Commit(dictionary);
                assemblyInstaller.Dispose();
            }
            else
            {
                AssemblyInstaller assemblyInstaller = new AssemblyInstaller();
                assemblyInstaller.UseNewContext = true;
                assemblyInstaller.Path = serviceFileName;
                assemblyInstaller.Uninstall(null);
                assemblyInstaller.Dispose();
            }
        }
        /// <summary>    
        /// 判断window服务是否存在    
        /// </summary>    
        /// <param name="serviceName">window服务名称</param>    
        /// <returns></returns>
        public static bool ServiceIsExisted(string serviceName)
        {
            ServiceController[] services = ServiceController.GetServices();
            ServiceController[] array = services;
            bool result;
            for (int i = 0; i < array.Length; i++)
            {
                ServiceController serviceController = array[i];
                if (serviceController.ServiceName.Equals(serviceName, StringComparison.OrdinalIgnoreCase))
                {
                    result = true;
                    return result;
                }
            }
            result = false;
            return result;
        }
        /// <summary>    
        /// 等待某种预期的状态（如运行，停止等）    
        /// </summary>    
        /// <param name="serviceName">window服务名称</param>    
        /// <param name="status">预期的状态</param>    
        /// <param name="second">如果获取不到预期的状态，则等待多少秒</param>    
        /// <returns></returns> 
        public static bool WaitForStatus(string serviceName, ServiceControllerStatus status, int second)
        {
            bool result = false;
            if (WinServiceHelper.ServiceIsExisted(serviceName))
            {
                ServiceController serviceController = new ServiceController(serviceName);
                if (serviceController != null)
                {
                    TimeSpan timeout = TimeSpan.FromMilliseconds((double)(1000 * second));
                    serviceController.WaitForStatus(ServiceControllerStatus.Running, timeout);
                    result = true;
                }
            }
            return result;
        }
        /// <summary>    
        /// 启动window服务    
        /// </summary>    
        /// <param name="serviceName">windows服务名称</param> 
        public static bool StartService(string serviceName)
        {
            bool result;
            try
            {
                ServiceController serviceController = new ServiceController(serviceName);
                if (serviceController.Status == ServiceControllerStatus.Running)
                {
                    result = true;
                    return result;
                }
                TimeSpan timeout = TimeSpan.FromMilliseconds(10000.0);
                serviceController.Start();
                serviceController.WaitForStatus(ServiceControllerStatus.Running, timeout);
            }
            catch
            {
                result = false;
                return result;
            }
            result = true;
            return result;
        }
        /// <summary>    
        /// 停止服务    
        /// </summary>    
        /// <param name="serviseName">windows服务名称</param>    
        /// <returns></returns> 
        public static bool StopService(string serviseName)
        {
            bool result;
            try
            {
                ServiceController serviceController = new ServiceController(serviseName);
                if (serviceController.Status == ServiceControllerStatus.Stopped)
                {
                    result = true;
                    return result;
                }
                TimeSpan timeout = TimeSpan.FromMilliseconds(10000.0);
                serviceController.Stop();
                serviceController.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
            }
            catch (Exception)
            {
                //LogTextHelper.Error(ex);
                result = false;
                return result;
            }
            result = true;
            return result;
        }
        /// <summary>    
        /// 修改服务的启动项 2为自动,3为手动    
        /// </summary>    
        /// <param name="startType"></param>    
        /// <param name="serviceName">windows服务名</param>    
        /// <returns></returns>
        public static bool ChangeServiceStartType(int startType, string serviceName)
        {
            bool result;
            try
            {
                RegistryKey localMachine = Registry.LocalMachine;
                RegistryKey registryKey = localMachine.OpenSubKey("SYSTEM");
                RegistryKey registryKey2 = registryKey.OpenSubKey("CurrentControlSet");
                RegistryKey registryKey3 = registryKey2.OpenSubKey("Services");
                RegistryKey registryKey4 = registryKey3.OpenSubKey(serviceName, true);
                registryKey4.SetValue("Start", startType);
            }
            catch
            {
                //LogTextHelper.Error(ex);
                result = false;
                return result;
            }
            result = true;
            return result;
        }
        /// <summary>    
        /// 获取服务启动类型 2为自动 3为手动 4 为禁用    
        /// </summary>    
        /// <param name="serviceName">windows服务名</param>    
        /// <returns></returns> 
        public static string GetServiceStartType(string serviceName)
        {
            string result;
            try
            {
                RegistryKey localMachine = Registry.LocalMachine;
                RegistryKey registryKey = localMachine.OpenSubKey("SYSTEM");
                RegistryKey registryKey2 = registryKey.OpenSubKey("CurrentControlSet");
                RegistryKey registryKey3 = registryKey2.OpenSubKey("Services");
                RegistryKey registryKey4 = registryKey3.OpenSubKey(serviceName, true);
                result = registryKey4.GetValue("Start").ToString();
            }
            catch
            {
                //LogTextHelper.Error(ex);
                result = string.Empty;
            }
            return result;
        }
        /// <summary>    
        /// 验证服务是否启动    
        /// </summary>    
        /// <param name="serviceName">windows服务名</param>
        /// <returns></returns>
        public static bool ServiceIsRunning(string serviceName)
        {
            ServiceController serviceController = new ServiceController(serviceName);
            return serviceController.Status == ServiceControllerStatus.Running;
        }
    }
}
