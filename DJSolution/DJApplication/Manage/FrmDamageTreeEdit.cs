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
    public partial class FrmDamageTreeEdit : Form
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

        public FrmDamageTreeEdit(DataRow dataRow, EditMode editMode)
        {
            InitializeComponent();
            InitValidationRules();
            _editMode = editMode;
            _editItem = dataRow;
        }

        private void FrmDamageTreeEdit_Load(object sender, EventArgs e)
        {
            this.LoadDamageTreeType();
            this.LoadDamageTreeStatus();
            switch (_editMode)
            {
                case EditMode.Add:
                    txtIndex.EditValue = -1;                    
                    break;
                case EditMode.Edit:
                    txtIndex.EditValue = _editItem["ID"];
                    txtName.EditValue = _editItem["Name"];
                    txtCode.EditValue = _editItem["Code"];
                    cmbType.EditValue = _editItem["Type"];
                    cmbStatus.EditValue = _editItem["Status"];
                    txtRemark.EditValue = _editItem["Remark"];
                    ckIsEffective.Checked = Convert.ToBoolean(_editItem["IsEffective"]);
                    break;
            }
        }

        private void LoadDamageTreeType()
        {
            try
            {
                cmbType.Properties.DataSource = CommonFunc.GetDamageTreeType();
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
                cmbType.Properties.DataSource = null;
            }
        }

        private void LoadDamageTreeStatus()
        {
            try
            {
                cmbStatus.Properties.DataSource = CommonFunc.GetDamageTreeStatus();
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
                cmbStatus.Properties.DataSource = null;
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

            ConditionValidationRule categoryValidationRule = new ConditionValidationRule();
            categoryValidationRule.ConditionOperator = ConditionOperator.IsNotBlank;
            categoryValidationRule.ErrorText = "类型不能为空";
            dxValidationProvider1.SetValidationRule(cmbType, categoryValidationRule);

            ConditionValidationRule baseValidationRule = new ConditionValidationRule();
            baseValidationRule.ConditionOperator = ConditionOperator.IsNotBlank;
            baseValidationRule.ErrorText = "状态不能为空";
            dxValidationProvider1.SetValidationRule(cmbStatus, baseValidationRule);

            dxValidationProvider1.SetIconAlignment(txtCode, ErrorIconAlignment.MiddleRight);
            dxValidationProvider1.SetIconAlignment(cmbType, ErrorIconAlignment.MiddleRight);
            dxValidationProvider1.SetIconAlignment(cmbStatus, ErrorIconAlignment.MiddleRight);
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
                var wh = CommonFunc.GetDamageTreeDataTableScheme();
                DataRow row = wh.NewRow();
                row["ID"] = txtIndex.EditValue;
                row["Name"] = txtName.EditValue;
                row["Code"] = txtCode.EditValue;
                row["Type"] = cmbType.EditValue;
                row["Status"] = cmbStatus.EditValue;
                row["Remark"] = txtRemark.EditValue;
                row["IsEffective"] = ckIsEffective.Checked ? 1 : 0;
                wh.Rows.Add(row);
                string message = string.Empty;

                if (CommonFunc.SubmitDamageTreeInfo(wh, _editMode, ref message))
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
