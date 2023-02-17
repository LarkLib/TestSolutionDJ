using Elite.Windows.Service.SyncServiceData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite.LMS.WinForms.UtilityClass
{
    class ServiceClient
    {
        public static void RegisterLimsReport(LimsRegisterReport report)
        {
            using (var client = new SyncServiceData())
            {
                client.RegisterLimsReport(report);
            }
        }
    }
}
