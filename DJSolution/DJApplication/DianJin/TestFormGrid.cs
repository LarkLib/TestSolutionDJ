using DevExpress.DocumentView;
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
    public partial class TestFormGrid : Form
    {
        public TestFormGrid()
        {
            InitializeComponent();
        }

        private void TestFormGrid_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = CommonFunc.GetDamageTreeItemDataSchema(7L);
            gridView1.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditForm;
        }
    }
}
