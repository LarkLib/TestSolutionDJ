using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DJ.LMS.Utilities;
using DevExpress.XtraEditors.DXErrorProvider;

namespace DJ.LMS.WinForms
{
    public partial class FrmUserEdit : Form
    {
        private DataRow _editUser;
        private EditMode _editMode;
        private bool _isChange = false;

        /// <summary>
        /// 获取一个值，指示是否提交更改
        /// </summary>
        public bool SubmitChanged
        {
            get { return _isChange; }
        }

        public FrmUserEdit(DataRow userRow, EditMode editMode)
        {
            InitializeComponent();
            InitValidationRules();
            _editUser = userRow;
            _editMode = editMode;
            clbWarehouse.DrawItem += clbWarehouse_DrawItem;
        }

        private void FrmUserEdit_Load(object sender, EventArgs e)
        {
            LoadRole();
            try
            {
                switch (_editMode)
                {
                    case EditMode.Add:
                        {
                            txtUserID.EditValue = -1;
                            ckDefaultPwd.Properties.ReadOnly = true;
                            ckDefaultPwd.Checked = true;
                            ckIsEffective.Checked = true;
                            break;
                        }
                    case EditMode.Edit:
                        {
                            txtUserID.EditValue = _editUser["ID"];
                            txtLoginName.EditValue = _editUser["LoginName"];
                            txtRealName.EditValue = _editUser["RealName"];
                            cmbRole.EditValue = _editUser["RelationRole"];
                            ckDefaultPwd.Properties.ReadOnly = false;
                            ckDefaultPwd.Checked = false;
                            ckDefaultPwd.Tag = _editUser["LoginPwd"];
                            ckIsEffective.Checked = Convert.ToBoolean(_editUser["IsEffective"]);
                            txtRemark.EditValue = _editUser["Remark"];
                            clbWarehouse.BeginUpdate();
                            clbWarehouse.EndUpdate();
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
            }
        }

        void clbWarehouse_DrawItem(object sender, DevExpress.XtraEditors.ListBoxDrawItemEventArgs e)
        {
            if (clbWarehouse.GetItemChecked(e.Index))
                return;
            e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Strikeout);
        }

        private void LoadRole()
        {
            try
            {
                cmbRole.Properties.DataSource = CommonFunc.GetRoleList();
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
                cmbRole.Properties.DataSource = null;
            }
        }

        private void InitValidationRules()
        {
            ConditionValidationRule loginNameValidationRule = new ConditionValidationRule();
            loginNameValidationRule.ConditionOperator = ConditionOperator.IsNotBlank;
            loginNameValidationRule.ErrorText = "登录名称不能为空";
            dxValidationProvider1.SetValidationRule(txtLoginName, loginNameValidationRule);

            ConditionValidationRule roleValidationRule = new ConditionValidationRule();
            roleValidationRule.ConditionOperator = ConditionOperator.IsNotBlank;
            roleValidationRule.ErrorText = "用户角色不能为空";
            dxValidationProvider1.SetValidationRule(cmbRole, roleValidationRule);

            dxValidationProvider1.SetIconAlignment(txtLoginName, ErrorIconAlignment.MiddleRight);
            dxValidationProvider1.SetIconAlignment(cmbRole, ErrorIconAlignment.MiddleRight);
        }

        /// <summary>
        /// 获取选择的仓库
        /// </summary>
        /// <returns></returns>
        private DataTable GetCheckedItems()
        {
            DataTable selectedWarehouse = CommonFunc.GetIDsDataTableScheme();
            foreach (object item in clbWarehouse.CheckedItems)
            {
                DataRowView rv = item as DataRowView;
                DataRow dr = selectedWarehouse.NewRow();
                dr["ID"] = rv.Row["ID"];
                selectedWarehouse.Rows.Add(dr);
            }
            return selectedWarehouse;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!dxValidationProvider1.Validate())
                return;
            if (MessageUtil.ShowYesNoAndTips("请确定是否保存编辑的用户信息.") == DialogResult.No)
                return;
            try
            {
                DataTable user = CommonFunc.GetUserDataTableScheme();
                DataRow row = user.NewRow();
                row["ID"] = txtUserID.EditValue;
                row["LoginName"] = txtLoginName.EditValue;
                row["RealName"] = txtRealName.EditValue;
                if (_editMode == EditMode.Add)
                {
                    row["LoginPwd"] = EncodeHelper.AES_Encrypt("123456", "elite.com");
                }
                else
                {
                    row["LoginPwd"] = ckDefaultPwd.Checked ? EncodeHelper.AES_Encrypt("123456", "elite.com") : ckDefaultPwd.Tag;
                }
                row["RelationRole"] = cmbRole.EditValue;
                row["IsEffective"] = ckIsEffective.Checked ? 1 : 0;
                row["Remark"] = txtRemark.EditValue;
                user.Rows.Add(row);
                string message = string.Empty;
                DataTable relationWarehouse = GetCheckedItems();

                if (CommonFunc.SubmitUserInfo(user, relationWarehouse, _editMode, ref message))
                {
                    this._isChange = true;
                    if (_editMode == EditMode.Edit)
                    {
                        this.Close();
                    }
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

        private void ckAllSelect_CheckedChanged(object sender, EventArgs e)
        {
            this.clbWarehouse.BeginUpdate();
            try
            {
                int i = 0;
                while (clbWarehouse.GetItem(i) != null)
                {
                    clbWarehouse.SetItemCheckState(i, (ckAllSelect.Checked ? CheckState.Checked : CheckState.Unchecked));
                    i++;
                }
            }
            finally
            {
                clbWarehouse.EndUpdate();
            }
        }
    }
}
