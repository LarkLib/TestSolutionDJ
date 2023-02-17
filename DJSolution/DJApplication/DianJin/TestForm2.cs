using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DevExpress.Utils.Drawing.Helpers.NativeMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;
using ComboBox = System.Windows.Forms.ComboBox;
using TextBox = System.Windows.Forms.TextBox;

namespace DJ.LMS.WinForms.DianJin
{
    public partial class TestForm2 : Form
    {
        public TestForm2()
        {
            InitializeComponent();
        }

        DataTable dtUIConfig = null;
        //EnumDropDownControlType enumDropDownControlType=new EnumDropDownControlType();

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            dtUIConfig = new DataTable();
            //实际从数据库加载
            dtUIConfig.Columns.Add("name");
            dtUIConfig.Columns.Add("title");
            dtUIConfig.Columns.Add("size");
            dtUIConfig.Columns.Add("location");
            dtUIConfig.Columns.Add("type");
            dtUIConfig.Columns.Add("config");

            dtUIConfig.Rows.Add(new object[] { "ID", "ID:", "160,30", "0,0", "textbox", "" });
            dtUIConfig.Rows.Add(new object[] { "name", "用户名:", "160,30", "0,0", "textbox", "" });
            dtUIConfig.Rows.Add(new object[] { "password", "密码:", "160,30", "0,0", "passwordtext", "" });
            dtUIConfig.Rows.Add(new object[] { "sex", "性别:", "160,30", "0,0", "combobox", "Man,Female" });
            dtUIConfig.Rows.Add(new object[] { "emp", "职员:", "160,30", "0,0", "CustomComboBox", "datagridview" });
            dtUIConfig.Rows.Add(new object[] { "dept", "部门:", "160,30", "0,0", "CustomComboBox", "treeview" });
            dtUIConfig.Rows.Add(new object[] { "details", "明细:", "440,200", "0,0", "datagridview", "select * from test" });
            dtUIConfig.Rows.Add(new object[] { "btnSave", "保存", "160,30", "0,0", "button", "" });
            dtUIConfig.Rows.Add(new object[] { "rank", "等级:", "160,30", "0,0", "RadioButton", "1,2,3,4,5" });

            //获取最长的标签
            int leftMargin = 20;
            int topMargin = 20;
            int totolwidth = this.Width - 220 - leftMargin;

            Point currentLocation = new Point(leftMargin, topMargin);
            Point nextLocation = new Point(leftMargin, topMargin);
            int label_control_width = 2;
            int y = nextLocation.Y;

            int labelMaxLength = 20;
            int controlMaxLength = 160;

            int lastY = 0;
            //UI engine
            foreach (DataRow dr in dtUIConfig.Rows)
            {

                //计量字符串长度
                SizeF maxSize = this.CreateGraphics().MeasureString(dr["title"].ToString(), this.Font);
                if (labelMaxLength < maxSize.Width)
                {
                    labelMaxLength = int.Parse(maxSize.Width.ToString("0"));
                }
                if (controlMaxLength < int.Parse(dr["size"].ToString().Split(',')[0]))
                {
                    controlMaxLength = int.Parse(dr["size"].ToString().Split(',')[0]);
                }
            }

            //UI Builder
            foreach (DataRow dr in dtUIConfig.Rows)
            {
                if (dr["type"].ToString().ToLower() == "button")
                {
                    Label label = new Label();
                    label.Location = new Point(nextLocation.X, nextLocation.Y);
                    label.Width = labelMaxLength;//max size
                    label.Text = "";
                    //-----------------------------------
                    Button ctrlItem = new Button();
                    ctrlItem.Location = new Point(label.Right + label_control_width, nextLocation.Y);
                    ctrlItem.Width = int.Parse(dr["size"].ToString().Split(',')[0]);
                    ctrlItem.Height = int.Parse(dr["size"].ToString().Split(',')[1]);
                    ctrlItem.Name = dr["name"].ToString();
                    ctrlItem.Text = dr["title"].ToString();
                    //  ctrlItem.Font = this.Font;
                    ctrlItem.Click += new EventHandler(ctrlItem_Click);
                    //-------------------------------------------------------------
                    nextLocation.X = ctrlItem.Right + 8;
                    lastY = ctrlItem.Bottom + 16;
                    if (nextLocation.X >= totolwidth)
                    {
                        nextLocation.Y = ctrlItem.Bottom + 16;
                        nextLocation.X = currentLocation.X;
                    }
                    this.Controls.Add(label);
                    this.Controls.Add(ctrlItem);

                }

                //-------------------------------------------------
                if (dr["type"].ToString().ToLower() == "CustomComboBox".ToLower())
                {
                    Label label = new Label();
                    label.Location = new Point(nextLocation.X, nextLocation.Y);
                    label.Width = labelMaxLength;//max size
                    label.Text = dr["title"].ToString();
                    //-----------------------------------


                    //datagridview
                    if ((dr["config"].ToString().ToLower() == "datagridview"))
                    {
                        CustomComboBox ctrlItem = new CustomComboBox();
                        ctrlItem.Location = new Point(label.Right + label_control_width, nextLocation.Y);
                        ctrlItem.Width = int.Parse(dr["size"].ToString().Split(',')[0]);
                        ctrlItem.Height = int.Parse(dr["size"].ToString().Split(',')[1]);
                        ctrlItem.Name = dr["name"].ToString();
                        DataGridView gridView = new DataGridView();
                        gridView.Columns.Add("ID", "ID");
                        gridView.Columns.Add("Name", "Name");
                        gridView.Columns.Add("Level", "Level");
                        ctrlItem.DropDownControl = gridView;
                        gridView.Rows.Add(new object[] { "001", "jack", "9" });
                        gridView.Rows.Add(new object[] { "002", "wang", "9" });
                        gridView.Font = this.Font;
                        ctrlItem.DropDownControlType = enumDropDownControlType.DataGridView;
                        ctrlItem.DisplayMember = "Name";
                        ctrlItem.ValueMember = "ID";
                        //-------------------------------------------------------------
                        nextLocation.X = ctrlItem.Right + 8;
                        lastY = ctrlItem.Bottom + 16;
                        if (nextLocation.X >= totolwidth)
                        {
                            nextLocation.Y = ctrlItem.Bottom + 16;
                            nextLocation.X = currentLocation.X;
                        }
                        this.Controls.Add(label);
                        this.Controls.Add(ctrlItem);
                    }
                    else if (dr["config"].ToString().ToLower() == "treeview")
                    {
                        //CustomComboBox ctrlItem = new CustomComboBox();
                        //ctrlItem.Location = new Point(label.Right + label_control_width, nextLocation.Y);
                        //ctrlItem.Width = int.Parse(dr["size"].ToString().Split(',')[0]);
                        //ctrlItem.Height = int.Parse(dr["size"].ToString().Split(',')[1]);
                        //ctrlItem.Name = dr["name"].ToString();
                        ////静态变量 2个时候默认就是最后一个
                        //treeView1.Font = this.Font;
                        //ctrlItem.DropDownControlType = enumDropDownControlType.TreeView;
                        //ctrlItem.DropDownControl = this.treeView1;
                        ////not empty
                        //ctrlItem.DisplayMember = "Name";
                        //ctrlItem.ValueMember = "ID";
                        ////-------------------------------------------------------------
                        //nextLocation.X = ctrlItem.Right + 8;
                        //lastY = ctrlItem.Bottom + 16;
                        //if (nextLocation.X >= totolwidth)
                        //{
                        //    nextLocation.Y = ctrlItem.Bottom + 16;
                        //    nextLocation.X = currentLocation.X;
                        //}
                        //this.Controls.Add(label);
                        //this.Controls.Add(ctrlItem);

                    }
                    else
                    {
                    }


                }
                //---------------------------------------------------------------
                //强制换行
                if (dr["type"].ToString().ToLower() == "datagridview")
                {
                    //Label label = new Label();
                    //label.Location = new Point(nextLocation.X, nextLocation.Y);
                    //label.Width = labelMaxLength;//max size
                    //label.Text = dr["title"].ToString();
                    //-----------------------------------
                    DataGridView ctrlItem = new DataGridView();
                    //强制换行
                    ctrlItem.Location = new Point(currentLocation.X, lastY);
                    ctrlItem.Width = int.Parse(dr["size"].ToString().Split(',')[0]);
                    ctrlItem.Height = int.Parse(dr["size"].ToString().Split(',')[1]);
                    ctrlItem.Name = dr["name"].ToString();

                    //string connString = "server=.\\sql2008r2; database=GC管理; Trusted_Connection=True; ";
                    //MkMisII.DAO.SqlHelper.DefaultConnectionString = connString;
                    //DataTable dtC = MkMisII.DAO.SqlHelper.GetDataTableBySQL(dr["config"].ToString());
                    var dtC = new DataTable();
                    //实际从数据库加载
                    dtC.Columns.Add("name");
                    dtC.Columns.Add("title");
                    dtC.Columns.Add("size");
                    dtC.Columns.Add("location");
                    dtC.Columns.Add("type");
                    dtC.Columns.Add("config");

                    dtC.Rows.Add(new object[] { "ID", "ID:", "160,30", "0,0", "textbox", "" });
                    dtC.Rows.Add(new object[] { "name", "用户名:", "160,30", "0,0", "textbox", "" });
                    dtC.Rows.Add(new object[] { "password", "密码:", "160,30", "0,0", "passwordtext", "" });
                    dtC.Rows.Add(new object[] { "sex", "性别:", "160,30", "0,0", "combobox", "Man,Female" });
                    dtC.Rows.Add(new object[] { "emp", "职员:", "160,30", "0,0", "CustomComboBox", "datagridview" });
                    dtC.Rows.Add(new object[] { "dept", "部门:", "160,30", "0,0", "CustomComboBox", "treeview" });
                    dtC.Rows.Add(new object[] { "details", "明细:", "440,200", "0,0", "datagridview", "select * from test" });
                    dtC.Rows.Add(new object[] { "btnSave", "保存", "160,30", "0,0", "button", "" });

                    if (dtC != null)
                    {
                        ctrlItem.DataSource = dtC;
                    }
                    //-------------------------------------------------------------
                    //nextLocation.X = ctrlItem.Right + 8;
                    //lastY = ctrlItem.Bottom + 16;
                    //if (nextLocation.X >= totolwidth)
                    //{
                    nextLocation.Y = ctrlItem.Bottom + 16;
                    nextLocation.X = currentLocation.X;
                    //}

                    this.Controls.Add(ctrlItem);

                }
                //-------------------------------------------------
                if (dr["type"].ToString().ToLower() == "textbox")
                {
                    Label label = new Label();
                    label.Location = new Point(nextLocation.X, nextLocation.Y);
                    label.Width = labelMaxLength;//max size
                    label.Text = dr["title"].ToString();
                    //-----------------------------------
                    TextBox ctrlItem = new TextBox();
                    ctrlItem.Location = new Point(label.Right + label_control_width, nextLocation.Y);
                    ctrlItem.Width = int.Parse(dr["size"].ToString().Split(',')[0]);
                    ctrlItem.Height = int.Parse(dr["size"].ToString().Split(',')[1]);
                    ctrlItem.Name = dr["name"].ToString();

                    //-------------------------------------------------------------
                    nextLocation.X = ctrlItem.Right + 8;
                    lastY = ctrlItem.Bottom + 16;
                    if (nextLocation.X >= totolwidth)
                    {
                        nextLocation.Y = ctrlItem.Bottom + 16;
                        nextLocation.X = currentLocation.X;
                    }
                    this.Controls.Add(label);
                    this.Controls.Add(ctrlItem);

                }
                //----------------------------------------------------------
                if (dr["type"].ToString().ToLower() == "combobox")
                {
                    Label label = new Label();
                    label.Location = new Point(nextLocation.X, nextLocation.Y);
                    label.Width = labelMaxLength;
                    label.Text = dr["title"].ToString();

                    //-----------------------------------
                    ComboBox ctrlItem = new ComboBox();
                    ctrlItem.Location = new Point(label.Right + label_control_width, nextLocation.Y);
                    ctrlItem.Width = int.Parse(dr["size"].ToString().Split(',')[0]);
                    ctrlItem.Height = int.Parse(dr["size"].ToString().Split(',')[1]);
                    ctrlItem.Name = dr["name"].ToString();
                    string[] items = dr["config"].ToString().Split(',');
                    foreach (string item in items)
                    {
                        ctrlItem.Items.Add(item);
                    }
                    //-------------------------------------------------------------
                    nextLocation.X = ctrlItem.Right + 8;
                    lastY = ctrlItem.Bottom + 16;
                    if (nextLocation.X >= totolwidth)
                    {
                        nextLocation.Y = ctrlItem.Bottom + 16;
                        nextLocation.X = currentLocation.X;
                    }

                    this.Controls.Add(label);
                    this.Controls.Add(ctrlItem);

                }

                if (dr["type"].ToString().ToLower() == "passwordtext")
                {
                    Label label = new Label();
                    label.Location = new Point(nextLocation.X, nextLocation.Y);
                    label.Width = labelMaxLength;
                    label.Text = dr["title"].ToString();

                    //-----------------------------------
                    TextBox ctrlItem = new TextBox();
                    ctrlItem.PasswordChar = '*';
                    ctrlItem.Location = new Point(label.Right + label_control_width, nextLocation.Y);
                    ctrlItem.Width = int.Parse(dr["size"].ToString().Split(',')[0]);
                    ctrlItem.Height = int.Parse(dr["size"].ToString().Split(',')[1]);
                    ctrlItem.Name = dr["name"].ToString();

                    //-------------------------------------------------------------
                    nextLocation.X = ctrlItem.Right + 8;
                    lastY = ctrlItem.Bottom + 16;
                    if (nextLocation.X >= totolwidth)
                    {
                        nextLocation.Y = ctrlItem.Bottom + 16;
                        nextLocation.X = currentLocation.X;
                    }
                    this.Controls.Add(label);
                    this.Controls.Add(ctrlItem);

                }

                if (dr["type"].ToString().ToLower() == "radiobutton")
                {
                    Label label = new Label();
                    label.Location = new Point(nextLocation.X, nextLocation.Y);
                    label.Width = labelMaxLength;
                    label.Text = dr["title"].ToString();

                    //-----------------------------------
                    var ctrlItem = new GroupBox();
                    ctrlItem.Location = new Point(label.Right + label_control_width, nextLocation.Y);
                    ctrlItem.Width = int.Parse(dr["size"].ToString().Split(',')[0]);
                    var height = int.Parse(dr["size"].ToString().Split(',')[1]);
                    //ctrlItem.Height = int.Parse(dr["size"].ToString().Split(',')[1]);
                    ctrlItem.Name = dr["name"].ToString();
                    ctrlItem.Text = dr["title"].ToString();
                    string[] items = dr["config"].ToString().Split(',');
                    var panel = new Panel();
                    panel.Dock = DockStyle.Fill;
                    ctrlItem.Controls.Add(panel);

                    var radHeight = 0;
                    foreach (string item in items)
                    {
                        RadioButton radItem = new RadioButton();
                        radItem.Name = item;
                        radItem.Text = item;
                        radItem.Location = new Point(0, radHeight);
                        radHeight += height;
                        panel.Controls.Add(radItem);
                    }
                    ctrlItem.Height = height * items.Count() + height;
                    //-------------------------------------------------------------
                    nextLocation.X = ctrlItem.Right + 8;
                    lastY = ctrlItem.Bottom + 16;
                    if (nextLocation.X >= totolwidth)
                    {
                        nextLocation.Y = ctrlItem.Bottom + 16;
                        nextLocation.X = currentLocation.X;
                    }

                    //this.Controls.Add(label);
                    this.Controls.Add(ctrlItem);
                }
            }

        }
        //save
        //生成保存SQL
        string SQL = "";
        void ctrlItem_Click(object sender, EventArgs e)
        {
            try
            {
                string preSQL = "Insert into Users(";
                string postSQL = " ) values ( ";
                foreach (DataRow dr in dtUIConfig.Rows)
                {
                    if (dr["type"].ToString() != "button" && dr["type"].ToString() != "datagridview")
                    {
                        Control[] ctrl = this.Controls.Find(dr["name"].ToString(), true);
                        if (ctrl != null)
                        {
                            if (ctrl.Length == 1)
                            {
                                //if (!dic.Keys.Contains(dr["name"].ToString()))
                                //{
                                //    preSQL += string.Format("'{0}',", dr["name"].ToString());
                                //    postSQL += string.Format("'{0}',", ctrl[0].Text);
                                //    //dic.Add(dr["name"].ToString(), ctrl[0].Text);
                                //}
                            }

                        }
                    }

                }
                SQL = preSQL.TrimEnd(',') + postSQL.TrimEnd(',') + ")";
                MessageBox.Show(SQL, "insert SQL");
                //Save data to database ...
            }
            catch (Exception ex)
            {

            }

        }
    }

    internal class CustomComboBox : ComboBox
    {
        public DataGridView DropDownControl { get; internal set; }
        public object DropDownControlType { get; internal set; }
    }
    internal enum enumDropDownControlType
    {
        DataGridView
    }
}
