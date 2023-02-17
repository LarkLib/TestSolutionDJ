using System;

namespace DJ.LMS.WinForms
{
    public sealed class LoginUser
    {
        private static volatile LoginUser _instance = null;
        private static object syncRoot = new Object();
        private LoginUser() { }
        public static LoginUser Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncRoot)
                    {
                        if (_instance == null)
                            _instance = new LoginUser();
                    }
                }
                return _instance;
            }
        }
        public Int64 UserId
        {
            get;
            set;
        }
        public string Code
        {
            get;
            set;
        }
        public string RealName
        {
            get;
            set;
        }
        public string LoginPwd
        {
            get;
            set;
        }
        public string IsEffective
        {
            get;
            set;
        }
        public string Remark
        {
            get;
            set;
        }
        public Int64 RoleId
        {
            get;
            set;
        }
        public string RoleName
        {
            get;
            set;
        }
    }
}
