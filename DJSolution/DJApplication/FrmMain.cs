using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;
using System.Threading;
using DJ.LMS.Utilities;

namespace DJ.LMS.WinForms
{
    public partial class FrmMain : Form
    {
        #region ---变量、属性定义

        FormState _formState = new FormState();
        private DataTable tableMenus = null;
        private static volatile FrmMain _instance = null;
        private static object syncRoot = new Object();
        public static FrmMain Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncRoot)
                    {
                        if (_instance == null)
                            _instance = new FrmMain();
                    }
                }
                return _instance;
            }
        }
        #endregion

        #region ---构造函数\初始化
        public FrmMain()
        {
            InitializeComponent();
            trvList.BeforeCollapse += trvList_BeforeCollapse;
            trvList.BeforeExpand += trvList_BeforeExpand;
            trvList.MouseDoubleClick += trvList_MouseDoubleClick;
            this.KeyDown += FrmMain_KeyDown;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            string caption = string.Format("当前登陆：[{0}]-[{1}]    用户角色：[{2}]", LoginUser.Instance.Code, LoginUser.Instance.RealName, LoginUser.Instance.RoleName);
            statusLabelUser.Caption = caption;
            InitMenus();
        }
        #endregion

        #region ---浏览器事件处理

        private void InitMenus()
        {
            this.trvList.Nodes.Clear();
            tableMenus = LoginUser.Instance.RoleId.Equals(0) ? CommonFunc.GetMenuList() : CommonFunc.GetMenusByRole(LoginUser.Instance.RoleId);
            //tableMenus = CSVHelper.CSVToDataTableByStreamReader("MenuData.csv");

            TreeNode node = new TreeNode("应用程序功能菜单", 0, 0);
            node.Tag = null;
            this.trvList.Nodes.Add(node);
            this.trvList.SelectedNode = node;
            this.DisplayMenu(node, null, tableMenus, "0");
            node.ExpandAll();
        }

        private void DisplayMenu(TreeNode pNode, TreeNode tNode, DataTable dataTable, object pCode)
        {
            DataView dvSub = new DataView(dataTable, "SelectedFlag=1 AND ParentID = '" + pCode + "'", "MenuOrder", DataViewRowState.CurrentRows);
            foreach (DataRowView row in dvSub)
            {
                TreeNodeStyle subNode = new TreeNodeStyle(row["MenuName"].ToString().Trim(), row["RelativeForm"].ToString().Trim());
                subNode.Tag = row["MenuID"];
                if (Convert.ToInt32(row["ChildNodeFlag"]).Equals(1))
                {
                    subNode.ImageIndex = 2;
                    subNode.SelectedImageIndex = subNode.ImageIndex;
                }
                else
                {
                    subNode.ImageIndex = 1;
                    subNode.SelectedImageIndex = subNode.ImageIndex;
                }
                if (tNode == null)
                    pNode.Nodes.Add(subNode);
                else
                    tNode.Nodes.Add(subNode);
                DisplayMenu(pNode, subNode, dataTable, row["MenuID"].ToString());
            }
        }

        private void trvList_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node != null && e.Node.Nodes.Count > 0 && Convert.ToInt32(e.Node.Tag) > 0)
            {
                e.Node.ImageIndex = 0;
            }
        }

        private void trvList_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node != null && e.Node.Nodes.Count > 0 && Convert.ToInt32(e.Node.Tag) > 0)
            {
                e.Node.ImageIndex = 1;
            }
        }

        private void trvList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                TreeNode sNode = trvList.GetNodeAt(e.X, e.Y);
                if (sNode == null)
                    return;
                if (sNode.Tag != null && sNode.Nodes.Count == 0)
                {
                    try
                    {
                        Cursor = Cursors.WaitCursor;
                        OpenForm(sNode);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message, ex);
                    }
                    finally
                    {
                        Cursor = Cursors.Default;
                    }
                }
            }
        }

        public void OpenForm(int menuID)
        {
            DataRow[] rows = tableMenus.Select(string.Format("MenuID={0} AND ChildNodeFlag=1", menuID), "MenuID");
            if (rows.Length == 0)
                return;
            DataRow row = rows[0];
            if (row["RelativeForm"].ToString().TrimEnd() == string.Empty)
                return;
            string parameters = row.IsNull("MenuParams") ? string.Empty : row["MenuParams"].ToString();
            this.ActivatForm(row["RelativeForm"].ToString().Trim(), row["MenuName"].ToString().Trim(), parameters);
        }

        private void OpenForm(TreeNode node)
        {
            DataRow[] rows = tableMenus.Select("MenuID = '" + node.Tag.ToString() + "' AND ChildNodeFlag = 1", "MenuID");
            if (rows.Length == 0)
                return;
            DataRow row = rows[0];
            if (row["RelativeForm"].ToString().TrimEnd() == string.Empty)
                return;
            string parameters = row.IsNull("MenuParams") ? string.Empty : row["MenuParams"].ToString();
            this.ActivatForm(row["RelativeForm"].ToString().Trim(), row["MenuName"].ToString().Trim(), parameters);
        }

        private void ActivatForm(string formName, string caption, string parameters)
        {
            if (string.IsNullOrEmpty(formName))
                return;
            if (ContainMDIChild(formName, caption))
                return;
            string[] args = parameters.Split(',');
            try
            {
                Type typeForm = Assembly.GetExecutingAssembly().GetType(formName, false);
                if (typeForm == null) return;
                Form objForm = (args.Length == 1 && args[0] == string.Empty) ? (Form)Activator.CreateInstance(typeForm) : (Form)Activator.CreateInstance(typeForm, args);
                if (objForm != null)
                {
                    objForm.Text = caption;
                    objForm.MdiParent = FrmMain.Instance;
                    objForm.Show();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        private bool ContainMDIChild(string ChildTypeString, string caption)
        {
            foreach (Form form in FrmMain.Instance.MdiChildren)
            {
                if (form.GetType().ToString() == ChildTypeString && form.Text.Equals(caption))
                {
                    form.Select();
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region ---菜单与工具栏事件处理

        private void menuModifyPwd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (FrmModifyPassword form = new FrmModifyPassword())
            {
                form.ShowDialog(this);
            }
        }

        private void menuAbout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmAboutBox form = new FrmAboutBox();
            form.ShowDialog();
        }

        private void menuExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnAllScreen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _formState.Maximize(this);
        }

        private void btnStandard_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _formState.Restore(this);
        }

        private void btnCloseCurrentWindow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (FrmMain.Instance.ActiveMdiChild != null)
                FrmMain.Instance.ActiveMdiChild.Close();
        }

        private void btnCloseAllWindow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            foreach (Form form in FrmMain.Instance.MdiChildren)
            {
                form.Close();
            }
        }

        private void btnNotepade_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Process.Start(AppDomain.CurrentDomain.BaseDirectory + @"Tools\记事本.exe");
        }

        private void btnCalculator_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Process.Start(AppDomain.CurrentDomain.BaseDirectory + @"Tools\计算器.exe");
        }

        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Escape)
            //    btnStandard_ItemClick(null, null);
        }

        #endregion

        #region ---退出程序

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageUtil.ShowYesNoAndTips("是否要退出管理系统？") == DialogResult.No)
                e.Cancel = true;
        }

        #endregion
    }
}