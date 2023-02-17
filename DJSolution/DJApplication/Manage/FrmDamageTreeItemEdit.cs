using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DJ.LMS.Utilities;
using DevExpress.XtraEditors.DXErrorProvider;

namespace DJ.LMS.WinForms
{
    public partial class FrmDamageTreeItemEdit : Form
    {
        private DataRow _editItem;
        private EditMode _editMode;
        private bool _isChange = false;
        /// <summary>
        /// 获取一个值，指示是否提交更改
        /// </summary>
        public bool SubmitChanged
        {
            get { return _isChange; }
        }

        public FrmDamageTreeItemEdit(DataRow dataRow, EditMode editMode)
        {
            InitializeComponent();
            InitValidationRules();
            _editMode = editMode;
            _editItem = dataRow;
        }

        private void FrmDamageTreeItemEdit_Load(object sender, EventArgs e)
        {
            switch (_editMode)
            {
                case EditMode.Add:
                    txtIndex.EditValue = -1;
                    break;
                case EditMode.Edit:
                    txtIndex.EditValue = _editItem["ID"];
                    txtName.EditValue = _editItem["Name"];
                    txtCode.EditValue = _editItem["Code"];
                    txtRemark.EditValue = _editItem["Remark"];
                    break;
            }
        }

        private void InitValidationRules()
        {
            ConditionValidationRule wmsNameValidationRule = new ConditionValidationRule();
            wmsNameValidationRule.ConditionOperator = ConditionOperator.IsNotBlank;
            wmsNameValidationRule.ErrorText = "名称不能为空";
            dxValidationProvider1.SetValidationRule(txtName, wmsNameValidationRule);

            ConditionValidationRule erpCodeValidationRule = new ConditionValidationRule();
            erpCodeValidationRule.ConditionOperator = ConditionOperator.IsNotBlank;
            erpCodeValidationRule.ErrorText = "编码不能为空";
            dxValidationProvider1.SetValidationRule(txtCode, erpCodeValidationRule);


            dxValidationProvider1.SetIconAlignment(txtCode, ErrorIconAlignment.MiddleRight);
            dxValidationProvider1.SetIconAlignment(txtName, ErrorIconAlignment.MiddleRight);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!dxValidationProvider1.Validate())
                return;

            if (MessageUtil.ShowYesNoAndTips("请确定是否保存编辑的信息.") == DialogResult.No)
                return;

            try
            {
                var wh = CommonFunc.GetDamageTreeItemDataTableScheme();
                DataRow row = wh.NewRow();
                row["ID"] = txtIndex.EditValue;
                row["Name"] = txtName.EditValue;
                row["Code"] = txtCode.EditValue;
                row["Remark"] = txtRemark.EditValue;
                row["DamageTreeID"] = _editItem["DamageTreeID"];
                row["PID"] = _editItem["PID"];
                wh.Rows.Add(row);
                string message = string.Empty;

                if (CommonFunc.SubmitDamageTreeItem(wh, _editMode, ref message))
                {
                    this._isChange = true;
                    //if (_editMode == EditMode.Edit)
                    this.Close();
                }
                else
                {
                    MessageUtil.ShowError(message);
                }
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
