using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DJ.LMS.Utilities;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.DXErrorProvider;

namespace DJ.LMS.WinForms
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
            InitValidationRules();
            txtLoginName.Properties.Leave += txtLoginName_Properties_Leave;
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            string previousUser = new AppConfig().GetKeyValue("previousUser").Trim();
            if (previousUser.Length > 0)
                txtLoginName.Text = previousUser;
            //txtLoginName_Properties_Leave(null, null);
            //btnLogin_Click(null, null);
        }

        private void InitValidationRules()
        {
            ConditionValidationRule loginNameValidationRule = new ConditionValidationRule();
            loginNameValidationRule.ConditionOperator = ConditionOperator.IsNotBlank;
            loginNameValidationRule.ErrorText = "用户名不能为空";
            dxValidationProvider1.SetValidationRule(txtLoginName, loginNameValidationRule);
            ConditionValidationRule loginPwdValidationRule = new ConditionValidationRule();
            loginPwdValidationRule.ConditionOperator = ConditionOperator.IsNotBlank;
            loginPwdValidationRule.ErrorText = "登录密码不能为空";
            dxValidationProvider1.SetValidationRule(txtLoginPwd, loginPwdValidationRule);
            //ConditionValidationRule roleNameValidationRule = new ConditionValidationRule();
            //roleNameValidationRule.ConditionOperator = ConditionOperator.IsNotBlank;
            //roleNameValidationRule.ErrorText = "请选择一个用户角色";
            //dxValidationProvider1.SetValidationRule(cmbRole, roleNameValidationRule);
            dxValidationProvider1.SetIconAlignment(txtLoginName, ErrorIconAlignment.MiddleRight);
            dxValidationProvider1.SetIconAlignment(txtLoginPwd, ErrorIconAlignment.MiddleRight);
            dxValidationProvider1.SetIconAlignment(cmbRole, ErrorIconAlignment.MiddleRight);
        }

        private void txtLoginName_Properties_Leave(object sender, EventArgs e)
        {
            cmbRole.Properties.DataSource = CommonFunc.GetRelationRolesByUser(txtLoginName.Text.Trim());
            cmbRole.ItemIndex = 0;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!dxValidationProvider1.Validate())
                return;
            string message = string.Empty;
            string loginName = txtLoginName.Text.Trim();
            string pwd = txtLoginPwd.Text.Trim();
            try
            {
                DataTable user = CommonFunc.GetUser(loginName);
                if (user == null || user.Rows.Count == 0)
                {
                    MessageUtil.ShowError(string.Format("输入的用户名：{0}不存在.", loginName));
                    this.txtLoginName.Focus();
                    return;
                }
                //密码验证
                if (!EncodeHelper.AES_Encrypt(pwd, "elite.com").Equals(user.Rows[0]["LoginPwd"].ToString().Trim()))
                {
                    MessageUtil.ShowError("登录密码不正确，请重新输入");
                    this.txtLoginPwd.Focus();
                    return;
                }
                //用户有效性验证
                if (!Convert.ToBoolean(user.Rows[0]["IsEffective"]))
                {
                    MessageUtil.ShowError(string.Format("要登录的用户：{0}已被禁用.", loginName));
                    return;
                }
                LoginUser.Instance.UserId = Convert.ToInt64(user.Rows[0]["ID"]);
                LoginUser.Instance.Code = user.Rows[0]["LoginName"].ToString();
                LoginUser.Instance.RealName = user.Rows[0]["RealName"].ToString();
                LoginUser.Instance.LoginPwd = user.Rows[0]["LoginPwd"].ToString();
                LoginUser.Instance.IsEffective = user.Rows[0]["IsEffective"].ToString();
                LoginUser.Instance.Remark = user.Rows[0]["Remark"].ToString();
                LoginUser.Instance.RoleId = Convert.ToInt64(cmbRole.EditValue);
                LoginUser.Instance.RoleName = cmbRole.Text;
                new AppConfig().SetKeyValue("previousUser", txtLoginName.Text.Trim());
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                //this.Close();
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
                return;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
