using System;

namespace DJ.LMS.WinForms
{
    public class PlanAuditException : ApplicationException
    {
        public PlanAuditException(string message)
            : base(message)
        { 
        }

        public PlanAuditException(string message, Exception innerException)
            : base(message, innerException)
        { 
        }
    }
}
