using System;
using System.Runtime.InteropServices;

namespace DJ.LMS.Utilities
{
    /// <summary>
    /// 用来方便实现计算机重启、关电源、注销、关闭显示器等系统操作
    /// </summary>
    public class WindowsExitHelper
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct Struct19
        {
            public int int_0;
            public long long_0;
            public int int_1;
        }
        internal const int int_0 = 2;
        internal const int int_1 = 8;
        internal const int int_2 = 32;
        internal const string string_0 = "SeShutdownPrivilege";
        internal const int int_3 = 0;
        internal const int int_4 = 1;
        internal const int int_5 = 2;
        internal const int int_6 = 4;
        internal const int int_7 = 8;
        internal const int int_8 = 16;
        private const uint uint_0 = 274u;
        private const uint uint_1 = 61808u;
        private static readonly IntPtr intptr_0 = new IntPtr(65535);
        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GetCurrentProcess();
        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool OpenProcessToken(IntPtr intptr_1, int int_9, ref IntPtr intptr_2);
        [DllImport("advapi32.dll", SetLastError = true)]
        internal static extern bool LookupPrivilegeValue(string string_1, string string_2, ref long long_0);
        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool AdjustTokenPrivileges(IntPtr intptr_1, bool bool_0, ref WindowsExitHelper.Struct19 struct19_0, int int_9, IntPtr intptr_2, IntPtr intptr_3);
        [DllImport("User32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool ExitWindowsEx(int int_9, int int_10);
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr intptr_1, uint uint_2, uint uint_3, int int_9);
        [DllImport("User32.dll")]
        private static extern void LockWorkStation();
        private static void smethod_0(int int_9)
        {
            IntPtr currentProcess = WindowsExitHelper.GetCurrentProcess();
            IntPtr zero = IntPtr.Zero;
            WindowsExitHelper.OpenProcessToken(currentProcess, 40, ref zero);
            WindowsExitHelper.Struct19 @struct;
            @struct.int_0 = 1;
            @struct.long_0 = 0L;
            @struct.int_1 = 2;
            WindowsExitHelper.LookupPrivilegeValue(null, "SeShutdownPrivilege", ref @struct.long_0);
            WindowsExitHelper.AdjustTokenPrivileges(zero, false, ref @struct, 0, IntPtr.Zero, IntPtr.Zero);
            WindowsExitHelper.ExitWindowsEx(int_9, 0);
        }
        /// <summary>    
        /// 计算机重启    
        /// </summary>
        public static void Reboot()
        {
            WindowsExitHelper.smethod_0(6);
        }
        /// <summary>    
        /// 计算机关电源    
        /// </summary>
        public static void PowerOff()
        {
            WindowsExitHelper.smethod_0(12);
        }
        /// <summary>    
        /// 计算机注销    
        /// </summary>
        public static void LogoOff()
        {
            WindowsExitHelper.smethod_0(4);
        }
        /// <summary>
        /// 计算机锁定
        /// </summary>
        public static void Lock()
        {
            WindowsExitHelper.LockWorkStation();
        }
        /// <summary>    
        /// 关闭显示器    
        /// </summary> 
        public static void CloseMonitor()
        {
            WindowsExitHelper.SendMessage(WindowsExitHelper.intptr_0, 274u, 61808u, 2);
        }        
    }
}
