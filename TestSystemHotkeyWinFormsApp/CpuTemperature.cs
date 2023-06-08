using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace TestSystemHotkeyWinFormsApp
{
    internal class CpuTemperature
    {
        public static float GetCpuTemperature()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(@"root\WMI", "SELECT * FROM MSAcpi_ThermalZoneTemperature");
            ManagementObjectCollection objCollection = searcher.Get();

            float temperature = 0;

            foreach (ManagementObject obj in objCollection)
            {
                temperature = Convert.ToSingle(obj["CurrentTemperature"].ToString());
                temperature = (temperature - 2732) / 10.0f;
                break;
            }
            Console.WriteLine(temperature);
            return temperature;
        }

    }
}
