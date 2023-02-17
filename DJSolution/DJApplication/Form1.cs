using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DJ.LMS.WinForms
{
    public partial class Form1 : Form
    {
        private DataTable _tableGroup = new DataTable();
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                _tableGroup = CommonFunc.GetMaterialGroup();
                if (_tableGroup == null && _tableGroup.Rows.Count == 0)
                {
                    return;
                }

                //获取所有第一层级的分组数据
                DataView dvSub = new DataView(_tableGroup, "Level=1", "number", DataViewRowState.CurrentRows);
                foreach (DataRowView row in dvSub)
                {
                    TreeNodeStyle subNode = new TreeNodeStyle(row["name"].ToString().Trim(), row["number"].ToString().Trim());
                    subNode.Tag = row["number"];
                    this.trvList.Nodes.Add(subNode);
                    this.DisplayMaterialGroup(subNode, _tableGroup, row["number"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "系统提示");
            }
        }

        private void DisplayMaterialGroup(TreeNode tNode, DataTable tableGroup, object longnumber)
        {
            DataView dvSub = new DataView(tableGroup, string.Format("longnumber='{0}'", longnumber), "number", DataViewRowState.CurrentRows);
            foreach (DataRowView row in dvSub)
            {
                TreeNodeStyle subNode = new TreeNodeStyle(row["name"].ToString().Trim(), row["number"].ToString().Trim());
                subNode.Tag = row["longnumber"];
                tNode.Nodes.Add(subNode);
                this.DisplayMaterialGroup(subNode, _tableGroup, string.Format("{0}!{1}", row["longnumber"], row["number"]));
            }
        }
    }
}
