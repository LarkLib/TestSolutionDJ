using DevExpress.DataAccess.Native.EntityFramework;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DJ.LMS.WinForms.DianJin
{
    public partial class TestFormTreeList : Form
    {
        public TestFormTreeList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 实现树节点的过滤查询
        /// </summary>
        private void InitSearchControl()
        {
            this.searchControl1.Client = this.treeList1;
            treeList1.CustomRowFilter += (object sender, DevExpress.XtraTreeList.CustomRowFilterEventArgs e) =>
            {
                if (treeList1.DataSource == null)
                    return;

                string nodeText = e.Node.GetDisplayText("name");//参数填写FieldName  
                if (string.IsNullOrWhiteSpace(nodeText))
                    return;

                bool isExist = nodeText.IndexOf(searchControl1.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                if (isExist)
                {
                    var node = e.Node.ParentNode;
                    while (node != null)
                    {
                        if (!node.Visible)
                        {
                            node.Visible = true;
                            node = node.ParentNode;
                        }
                        else
                            break;
                    }
                }
                e.Node.Visible = isExist;
                e.Handled = true;
            };
        }

        [Obsolete]
        private void btnTest_Click(object sender, EventArgs e)
        {
            treeList1.OptionsBehavior.Editable = false;

            //#region 设置列头、节点指示器面板、表格线样式
            treeList1.OptionsView.ShowColumns = false;             //隐藏列标头
            treeList1.OptionsView.ShowIndicator = false;           //隐藏节点指示器面板
            //treeList1.OptionsView.ShowHorzLines = false;           //隐藏水平表格线
            //treeList1.OptionsView.ShowVertLines = false;           //隐藏垂直表格线
            //treeList1.OptionsView.ShowIndentAsRowStyle = false;
            //#endregion

            //#region 初始禁用单元格选中，禁用整行选中

            //treeList1.OptionsView.ShowFocusedFrame = true;                               //设置显示焦点框
            //treeList1.OptionsSelection.EnableAppearanceFocusedCell = false;              //禁用单元格选中
            //treeList1.OptionsSelection.EnableAppearanceFocusedRow = false;               //禁用正行选中

            //#endregion

            //#region 设置TreeList的展开折叠按钮样式和树线样式

            //treeList1.OptionsView.ShowButtons = true;                  //显示展开折叠按钮
            //treeList1.LookAndFeel.UseDefaultLookAndFeel = false;       //禁用默认外观与感觉
            ////treeList1.LookAndFeel.UseWindowsXPTheme = true;            //使用WindowsXP主题
            //treeList1.TreeLineStyle = DevExpress.XtraTreeList.LineStyle.Percent50;     //设置树线的样式
            //treeList1.OptionsSelection.InvertSelection = true;

            //#endregion


            var dtUIConfig = new DataTable();
            //实际从数据库加载
            dtUIConfig.Columns.Add("ID");
            dtUIConfig.Columns.Add("PID");
            dtUIConfig.Columns.Add("name");
            dtUIConfig.Columns.Add("title");
            dtUIConfig.Columns.Add("size");
            dtUIConfig.Columns.Add("location");
            dtUIConfig.Columns.Add("type");
            dtUIConfig.Columns.Add("config");

            dtUIConfig.Rows.Add(new object[] { 1, -1, "ID", "ID:", "160,30", "0,0", "textbox", "" });
            dtUIConfig.Rows.Add(new object[] { 2, 1, "name", "用户名:", "160,30", "0,0", "textbox", "" });
            dtUIConfig.Rows.Add(new object[] { 3, 2, "password", "密码:", "160,30", "0,0", "passwordtext", "" });
            dtUIConfig.Rows.Add(new object[] { 4, 1, "sex", "性别:", "160,30", "0,0", "combobox", "Man,Female" });
            dtUIConfig.Rows.Add(new object[] { 5, 2, "emp", "职员:", "160,30", "0,0", "CustomComboBox", "datagridview" });
            dtUIConfig.Rows.Add(new object[] { 6, 3, "dept", "部门:", "160,30", "0,0", "CustomComboBox", "treeview" });
            dtUIConfig.Rows.Add(new object[] { 7, 3, "details", "明细:", "440,200", "0,0", "datagridview", "select * from test" });
            dtUIConfig.Rows.Add(new object[] { 8, 2, "btnSave", "保存", "160,30", "0,0", "button", "" });

            treeList1.KeyFieldName = "ID";
            treeList1.ParentFieldName = "PID";
            treeList1.DataSource = dtUIConfig;
            treeList1.ExpandAll();
            treeList1.OptionsBehavior.EnableFiltering = true;
            treeList1.OptionsFind.AlwaysVisible = true;

            dataLayoutControl1.DataSource = dtUIConfig;
            dataLayoutControl1.RetrieveFields();
            dataLayoutControl1.RestoreLayoutFromXml("ll.xml");

            this.textEdit1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", dtUIConfig, "title", true, DataSourceUpdateMode.OnPropertyChanged));

            searchLookUpEdit1.Properties.DataSource = dtUIConfig;
            searchLookUpEdit1.Properties.ValueMember = "ID";
            searchLookUpEdit1.Properties.DisplayMember = "title";
            //this.dateEdit1.DataBindings.Add("EditValue", t, "Value3", true, DataSourceUpdateMode.OnPropertyChanged);  

            //InitSearchControl();
        }

        int ID = 9;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var node = treeList1.FocusedNode;
            var row = ((DataTable)treeList1.DataSource).NewRow();
            row[("ID")] = ID++;
            row[("PID")] = node.GetValue("ID");
            row[("name")] = ("name");
            row[("title")] = ("title");
            row[("size")] = ("size");
            row[("location")] = ("location");
            row[("type")] = ("type");
            row[("config")] = ("config");

            //((DataTable)treeList1.DataSource).Rows.Add(row);
            TreeListNode newNode = treeList1.AppendNode(row, node);
            treeList1.FocusedNode = newNode;
            //node.Expanded = true;

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            int count = treeList1.Selection.Count;
            if (count == 0)
                return;
            string msg = string.Format("{0} nodes is about to be deleted. Do you want to proceed?", count);
            if (XtraMessageBox.Show(msg, "Deleting node", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // Delete selected nodes
                treeList1.DeleteSelectedNodes();
            }

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            //treeList1.Focus();

            //var node =  treeList1.Selection[0];
            //treeList1.FocusedNode = node;

            //treeList1.OptionsBehavior.Editable = true;
            //return;

            // Switch edit mode
            //treeList1.OptionsBehavior.EditingMode = TreeListEditingMode.EditForm;
            //treeList1.Columns["title"].OptionsEditForm.UseEditorColRowSpan = false;
            //treeList1.Columns["title"].OptionsEditForm.ColumnSpan = 3;
            //treeList1.Columns["title"].OptionsEditForm.RowSpan = 2;

            // Switch edit mode
            var treeList = treeList1;
            treeList.OptionsBehavior.EditingMode = TreeListEditingMode.EditForm;
            // Create a custom EditForm
            var control = new EditFormUserControl();
            control.Height = treeList.Height / 2;
            // Add editors
            MemoEdit memoEditNotes = new MemoEdit();
            memoEditNotes.Dock = DockStyle.Fill;
            TextEdit textEditName = new TextEdit();
            textEditName.Dock = DockStyle.Top;
            TextEdit textEditType = new TextEdit();
            textEditType.Dock = DockStyle.Top;
            DateEdit dateEditDate = new DateEdit();
            dateEditDate.Dock = DockStyle.Bottom;
            control.Controls.Add(memoEditNotes);
            control.Controls.Add(dateEditDate);
            control.Controls.Add(textEditType);
            control.Controls.Add(textEditName);
            // Bind the editors to data source fields
            //control.SetBoundFieldName(textEditName, "title");
            //control.SetBoundFieldName(memoEditNotes, "Notes");
            //control.SetBoundFieldName(textEditName, "Name");
            //control.SetBoundFieldName(textEditType, "TypeOfObject");
            //control.SetBoundFieldName(dateEditDate, "RecordDate");
            // Assing the Edit Form to the Tree List
            treeList.OptionsEditForm.CustomEditFormLayout = control;

        }

        private void treeList1_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            TreeList tl = sender as TreeList;
            if (tl.IsCellSelected(e.Node, e.Column))
                e.Appearance.BackColor = Color.AliceBlue;//tl.ViewInfo.PaintAppearance.SelectedRow.BackColor;

        }
    }
}
