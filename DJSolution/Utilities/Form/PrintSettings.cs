using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DJ.LMS.Utilities
{
    /// <summary>
    /// 打印设置对话框
    /// </summary>
    public partial class PrintSettings : Form
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public PrintSettings()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="availableFields">有效字段</param>
        public PrintSettings(List<string> availableFields)
            : this()
        {
            foreach (string current in availableFields)
            {
                this.chklst.Items.Add(current, true);
            }
        }

        private void PrintSettings_Load(object sender, EventArgs e)
        {
            this.rdoAllRows.Checked = true;
            this.chkFitToPageWidth.Checked = true;
            this.txtTitle.Text = this.PrintTitle;
        }

        public string PrintTitle
        {
            get { return this.txtTitle.Text; }
            set { this.txtTitle.Text = value; }
        }
        public bool PrintAllRows
        {
            get { return this.rdoAllRows.Checked; }
        }
        public bool FitToPageWidth
        {
            get { return this.chkFitToPageWidth.Checked; }
        }
        /// <summary>
        /// 获取所有要打印的列
        /// </summary>
        /// <returns></returns>
        public List<string> GetSelectedColumns()
        {
            List<string> list = new List<string>();
            foreach (object current in this.chklst.CheckedItems)
            {
                list.Add(current.ToString());
            }
            return list;
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
