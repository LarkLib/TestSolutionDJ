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
    public partial class FrmRoleEdit : Form
    {
        private DataRow _editRole;
        private EditMode _editMode;
        private bool _isChange = false;

        /// <summary>
        /// 获取一个值，指示是否提交更改
        /// </summary>
        public bool SubmitChanged
        {
            get { return _isChange; }
        }

        public FrmRoleEdit(DataRow roleRow, EditMode editMode)
        {
            InitializeComponent();
            InitValidationRules();
            _editRole = roleRow;
            _editMode = editMode;
            clbMenu.DrawItem += clbMenu_DrawItem;
        }

        private void FrmRoleEdit_Load(object sender, EventArgs e)
        {
            LoadMenus();
            try
            {
                switch (_editMode)
                {
                    case EditMode.Add:
                        {
                            txtRoleID.EditValue = -1;
                            ckIsEffective.Checked = true;
                            break;
                        }
                    case EditMode.Edit:
                        {
                            txtRoleID.EditValue = _editRole["ID"];
                            txtRoleName.EditValue = _editRole["Name"];
                            ckIsEffective.Checked = Convert.ToBoolean(_editRole["IsEffective"]);
                            txtRemark.EditValue = _editRole["Remark"];
                            DataTable enableMenu = CommonFunc.GetEnableMenuByRole(_editRole["ID"]);
                            clbMenu.BeginUpdate();
                            foreach (DataRow row in enableMenu.Rows)
                            {
                                int i = 0;
                                while (clbMenu.GetItem(i) != null)
                                {
                                    if (clbMenu.GetItemValue(i).Equals(row["MenuID"]))
                                    {
                                        clbMenu.SetItemCheckState(i, CheckState.Checked);
                                        break;
                                    }
                                    i++;
                                }
                            }
                            clbMenu.EndUpdate();
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
            }
        }

        private void clbMenu_DrawItem(object sender, DevExpress.XtraEditors.ListBoxDrawItemEventArgs e)
        {
            if (clbMenu.GetItemChecked(e.Index))
                return;
            e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Strikeout);
        }

        private void LoadMenus()
        {
            try
            {
                clbMenu.DataSource = CommonFunc.GetDisEnableMenu();
                clbMenu.DisplayMember = "MenuName";
                clbMenu.ValueMember = "MenuID";
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
                clbMenu.DataSource = null;
            }
        }

        private void InitValidationRules()
        {
            ConditionValidationRule nameValidationRule = new ConditionValidationRule();
            nameValidationRule.ConditionOperator = ConditionOperator.IsNotBlank;
            nameValidationRule.ErrorText = "角色名称不能为空";
            dxValidationProvider1.SetValidationRule(txtRoleName, nameValidationRule);

            dxValidationProvider1.SetIconAlignment(txtRoleName, ErrorIconAlignment.MiddleRight);
        }

        /// <summary>
        /// 获取选择的功能菜单
        /// </summary>
        /// <returns></returns>
        private DataTable GetCheckedItems()
        {
            DataTable selectedMenu = CommonFunc.GetIDsDataTableScheme();
            foreach (object item in clbMenu.CheckedItems)
            {
                DataRowView rv = item as DataRowView;
                DataRow dr = selectedMenu.NewRow();
                dr["ID"] = rv.Row["MenuID"];
                selectedMenu.Rows.Add(dr);
            }
            return selectedMenu;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!dxValidationProvider1.Validate())
                return;
            if (MessageUtil.ShowYesNoAndTips("请确定是否保存编辑的用户信息.") == DialogResult.No)
                return;
            try
            {
                DataTable role = CommonFunc.GetRoleDataTableScheme();
                DataRow row = role.NewRow();
                row["ID"] = txtRoleID.EditValue;
                row["Name"] = txtRoleName.EditValue;
                row["IsEffective"] = ckIsEffective.Checked ? 1 : 0;
                row["Remark"] = txtRemark.EditValue;
                role.Rows.Add(row);
                string message = string.Empty;
                DataTable relationMenus = GetCheckedItems();

                if (CommonFunc.SubmitRoleInfo(role, relationMenus, _editMode, ref message))
                {
                    this._isChange = true;
                    if (_editMode == EditMode.Edit)
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

        private void ckAllSelect_CheckedChanged(object sender, EventArgs e)
        {
            this.clbMenu.BeginUpdate();
            try
            {
                int i = 0;
                while (clbMenu.GetItem(i) != null)
                {
                    clbMenu.SetItemCheckState(i, (ckAllSelect.Checked ? CheckState.Checked : CheckState.Unchecked));
                    i++;
                }
            }
            finally
            {
                clbMenu.EndUpdate();
            }
        }
    }
}
