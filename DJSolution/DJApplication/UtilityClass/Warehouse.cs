using System;

namespace DJ.LMS.WinForms
{
    public sealed class Warehouse
    {
        private static volatile Warehouse _instance = null;
        private static Object syncRoot = new Object();
        private Warehouse() { }
        public static Warehouse Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncRoot)
                    {
                        if (_instance == null)
                            _instance = new Warehouse();
                    }
                }
                return _instance;
            }
        }
        public int WarehouseID
        {
            get;
            set;
        }
        public string WarehouseName
        {
            get;
            set;
        }
    }
}
