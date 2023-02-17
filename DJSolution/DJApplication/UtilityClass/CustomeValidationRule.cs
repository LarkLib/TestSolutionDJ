using System;
using System.Collections.Generic;
using System.Text;
using DJ.LMS.Utilities;

namespace DJ.LMS.WinForms
{
    /// <summary>
    /// 电量上限验证
    /// </summary>
    internal class CustomeElectricUpperValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            if (value == null || value.ToString().Trim().Length == 0)
                return false;
            int upper = Convert.ToInt32(value);
            return upper > 100 || upper < 80 ? false : true;
        }
    }

    /// <summary>
    /// 电量下限验证
    /// </summary>
    internal class CustomeElectricLowerValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            if (value == null || value.ToString().Trim().Length == 0)
                return false;
            int lower = Convert.ToInt32(value);
            return lower > 50 || lower < 10 ? false : true;
        }
    }

    internal class CustomeDoubleValueValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            if (value == null || value.ToString().Trim().Length == 0)
                return false;
            double qty = Convert.ToDouble(value);
            return qty <= 0 ? false : true;
        }
    }

    internal class CustomeIntgerValueValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            if (value == null || value.ToString().Trim().Length == 0)
                return false;
            int qty = Convert.ToInt32(value);
            return qty <= 0 ? false : true;
        }
    }

    internal class CustomeStorageLowerValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            if (value == null || value.ToString().Trim().Length == 0)
                return false;
            double lower = Convert.ToDouble(value);
            return lower < 0 ? false : true;
        }
    }

    internal class CustomeStationChoiceValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            return (value == null || Convert.ToInt64(value).Equals(-1)) ? false : true;
        }
    }

    internal class CustomePalletValidationForAutoWarehouse : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            if (value == null || value.ToString().Trim().Length == 0)
                return false;
            try
            {
                return SqlDbHelper.Exists("PalletValidation", string.Format("PalletCode='{0}'", value));
            }
            catch
            {
                return false;
            }
        }
    }
}
