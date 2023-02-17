using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace DJ.LMS.Utilities
{
    /// <summary>
    /// 用来对DataGridView控件内容的打印
    /// </summary>
    public class PrintDataGridView
    {
        private static StringFormat stringFormat_0;
        private static StringFormat stringFormat_1;
        private static Button button_0;
        private static CheckBox checkBox_0;
        private static ComboBox comboBox_0;
        private static int int_0;
        private static int int_1;
        private static bool bool_0;
        private static int int_2;
        private static ArrayList arrayList_0 = new ArrayList();
        private static ArrayList arrayList_1 = new ArrayList();
        private static ArrayList arrayList_2 = new ArrayList();
        private static int int_3;
        private static int int_4;
        private static PrintDocument printDocument_0 = new PrintDocument();
        private static string string_0 = "";
        private static DataGridView dataGridView_0;
        private static List<string> list_0 = new List<string>();
        private static List<string> list_1 = new List<string>();
        private static bool bool_1 = true;
        private static bool bool_2 = true;
        private static int int_5 = 0;
        public static void Print_DataGridView(DataGridView dgv1)
        {
            PrintDataGridView.Print_DataGridView(dgv1, "");
        }
        public static void Print_DataGridView(DataGridView dgv1, string title)
        {
            try
            {
                PrintDataGridView.dataGridView_0 = dgv1;
                PrintDataGridView.list_1.Clear();
                foreach (DataGridViewColumn dataGridViewColumn in PrintDataGridView.dataGridView_0.Columns)
                {
                    if (dataGridViewColumn.Visible)
                    {
                        PrintDataGridView.list_1.Add(dataGridViewColumn.HeaderText);
                    }
                }
                PrintSettings printSettings = new PrintSettings(PrintDataGridView.list_1);
                printSettings.PrintTitle = PrintDataGridView.string_0;
                if (printSettings.ShowDialog() == DialogResult.OK)
                {
                    PrintDataGridView.string_0 = printSettings.PrintTitle;
                    PrintDataGridView.bool_1 = printSettings.PrintAllRows;
                    PrintDataGridView.bool_2 = printSettings.FitToPageWidth;
                    PrintDataGridView.list_0 = printSettings.GetSelectedColumns();
                    PrintDataGridView.int_4 = 0;
                    PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
                    printPreviewDialog.Document = PrintDataGridView.printDocument_0;
                    PrintDataGridView.printDocument_0.BeginPrint += new PrintEventHandler(PrintDataGridView.smethod_0);
                    PrintDataGridView.printDocument_0.PrintPage += new PrintPageEventHandler(PrintDataGridView.smethod_1);
                    if (printPreviewDialog.ShowDialog() != DialogResult.OK)
                    {
                        PrintDataGridView.printDocument_0.BeginPrint -= new PrintEventHandler(PrintDataGridView.smethod_0);
                        PrintDataGridView.printDocument_0.PrintPage -= new PrintPageEventHandler(PrintDataGridView.smethod_1);
                    }
                    else
                    {
                        PrintDataGridView.printDocument_0.Print();
                        PrintDataGridView.printDocument_0.BeginPrint -= new PrintEventHandler(PrintDataGridView.smethod_0);
                        PrintDataGridView.printDocument_0.PrintPage -= new PrintPageEventHandler(PrintDataGridView.smethod_1);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            finally
            {
            }
        }
        private static void smethod_0(object object_0, object object_1)
        {
            try
            {
                PrintDataGridView.stringFormat_0 = new StringFormat();
                PrintDataGridView.stringFormat_0.Alignment = StringAlignment.Near;
                PrintDataGridView.stringFormat_0.LineAlignment = StringAlignment.Center;
                PrintDataGridView.stringFormat_0.Trimming = StringTrimming.EllipsisCharacter;
                PrintDataGridView.stringFormat_1 = new StringFormat();
                PrintDataGridView.stringFormat_1.LineAlignment = StringAlignment.Center;
                PrintDataGridView.stringFormat_1.FormatFlags = StringFormatFlags.NoWrap;
                PrintDataGridView.stringFormat_1.Trimming = StringTrimming.EllipsisCharacter;
                PrintDataGridView.arrayList_0.Clear();
                PrintDataGridView.arrayList_1.Clear();
                PrintDataGridView.arrayList_2.Clear();
                PrintDataGridView.int_3 = 0;
                PrintDataGridView.int_4 = 0;
                PrintDataGridView.button_0 = new Button();
                PrintDataGridView.checkBox_0 = new CheckBox();
                PrintDataGridView.comboBox_0 = new ComboBox();
                PrintDataGridView.int_0 = 0;
                foreach (DataGridViewColumn dataGridViewColumn in PrintDataGridView.dataGridView_0.Columns)
                {
                    if (dataGridViewColumn.Visible && PrintDataGridView.list_0.Contains(dataGridViewColumn.HeaderText))
                    {
                        PrintDataGridView.int_0 += dataGridViewColumn.Width;
                    }
                }
                PrintDataGridView.int_2 = 1;
                PrintDataGridView.bool_0 = true;
                PrintDataGridView.int_1 = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
        private static void smethod_1(object sender, PrintPageEventArgs e)
        {
            Rectangle marginBounds = e.MarginBounds;
            int num = marginBounds.Top;
            marginBounds = e.MarginBounds;
            int num2 = marginBounds.Left;
            try
            {
                if (PrintDataGridView.int_2 != 1)
                {
                    goto IL_A31;
                }
                IEnumerator enumerator = PrintDataGridView.dataGridView_0.Columns.GetEnumerator();
                try
                {
                    while (enumerator.MoveNext())
                    {
                        DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)enumerator.Current;
                        if (dataGridViewColumn.Visible && PrintDataGridView.list_0.Contains(dataGridViewColumn.HeaderText))
                        {
                            int num3;
                            if (PrintDataGridView.bool_2)
                            {
                                double arg_A9_0 = (double)dataGridViewColumn.Width / (double)PrintDataGridView.int_0 * (double)PrintDataGridView.int_0;
                                marginBounds = e.MarginBounds;
                                num3 = (int)Math.Floor(arg_A9_0 * ((double)marginBounds.Width / (double)PrintDataGridView.int_0));
                            }
                            else
                            {
                                num3 = dataGridViewColumn.Width;
                            }
                            SizeF sizeF = e.Graphics.MeasureString(dataGridViewColumn.HeaderText, dataGridViewColumn.InheritedStyle.Font, num3);
                            PrintDataGridView.int_5 = (int)sizeF.Height + 11;
                            PrintDataGridView.arrayList_0.Add(num2);
                            PrintDataGridView.arrayList_1.Add(num3);
                            PrintDataGridView.arrayList_2.Add(dataGridViewColumn.GetType());
                            num2 += num3;
                        }
                    }
                    goto IL_A31;
                }
                finally
                {
                    IDisposable disposable = enumerator as IDisposable;
                    if (disposable != null)
                    {
                        disposable.Dispose();
                    }
                }
            IL_151:
                DataGridViewRow dataGridViewRow = PrintDataGridView.dataGridView_0.Rows[PrintDataGridView.int_1];
                if (dataGridViewRow.IsNewRow || (!PrintDataGridView.bool_1 && !dataGridViewRow.Selected))
                {
                    PrintDataGridView.int_1++;
                }
                else
                {
                    PrintDataGridView.int_3 = dataGridViewRow.Height;
                    int arg_1C7_0 = num + PrintDataGridView.int_3;
                    marginBounds = e.MarginBounds;
                    int arg_1C6_0 = marginBounds.Height;
                    marginBounds = e.MarginBounds;
                    if (arg_1C7_0 >= arg_1C6_0 + marginBounds.Top)
                    {
                        PrintDataGridView.smethod_2(e, PrintDataGridView.int_4);
                        PrintDataGridView.bool_0 = true;
                        PrintDataGridView.int_2++;
                        e.HasMorePages = true;
                        return;
                    }
                    int num4;
                    if (PrintDataGridView.bool_0)
                    {
                        Graphics arg_257_0 = e.Graphics;
                        string arg_257_1 = PrintDataGridView.string_0;
                        Font arg_257_2 = new Font(PrintDataGridView.dataGridView_0.Font, FontStyle.Bold);
                        Brush arg_257_3 = Brushes.Black;
                        marginBounds = e.MarginBounds;
                        float arg_257_4 = (float)marginBounds.Left;
                        marginBounds = e.MarginBounds;
                        float arg_250_0 = (float)marginBounds.Top;
                        Graphics arg_242_0 = e.Graphics;
                        string arg_242_1 = PrintDataGridView.string_0;
                        Font arg_242_2 = new Font(PrintDataGridView.dataGridView_0.Font, FontStyle.Bold);
                        marginBounds = e.MarginBounds;
                        SizeF sizeF = arg_242_0.MeasureString(arg_242_1, arg_242_2, marginBounds.Width);
                        arg_257_0.DrawString(arg_257_1, arg_257_2, arg_257_3, arg_257_4, arg_250_0 - sizeF.Height - 13f);
                        DateTime now = DateTime.Now;
                        string arg_27D_0 = now.ToLongDateString();
                        string arg_27D_1 = " ";
                        now = DateTime.Now;
                        string text = arg_27D_0 + arg_27D_1 + now.ToShortTimeString();
                        Graphics arg_348_0 = e.Graphics;
                        string arg_348_1 = text;
                        Font arg_348_2 = new Font(PrintDataGridView.dataGridView_0.Font, FontStyle.Bold);
                        Brush arg_348_3 = Brushes.Black;
                        marginBounds = e.MarginBounds;
                        float arg_2F4_0 = (float)marginBounds.Left;
                        marginBounds = e.MarginBounds;
                        float arg_2F3_0 = (float)marginBounds.Width;
                        Graphics arg_2E5_0 = e.Graphics;
                        string arg_2E5_1 = text;
                        Font arg_2E5_2 = new Font(PrintDataGridView.dataGridView_0.Font, FontStyle.Bold);
                        marginBounds = e.MarginBounds;
                        sizeF = arg_2E5_0.MeasureString(arg_2E5_1, arg_2E5_2, marginBounds.Width);
                        float arg_348_4 = arg_2F4_0 + (arg_2F3_0 - sizeF.Width);
                        marginBounds = e.MarginBounds;
                        float arg_341_0 = (float)marginBounds.Top;
                        Graphics arg_333_0 = e.Graphics;
                        string arg_333_1 = PrintDataGridView.string_0;
                        Font arg_333_2 = new Font(new Font(PrintDataGridView.dataGridView_0.Font, FontStyle.Bold), FontStyle.Bold);
                        marginBounds = e.MarginBounds;
                        sizeF = arg_333_0.MeasureString(arg_333_1, arg_333_2, marginBounds.Width);
                        arg_348_0.DrawString(arg_348_1, arg_348_2, arg_348_3, arg_348_4, arg_341_0 - sizeF.Height - 13f);
                        marginBounds = e.MarginBounds;
                        num = marginBounds.Top;
                        num4 = 0;
                        enumerator = PrintDataGridView.dataGridView_0.Columns.GetEnumerator();
                        try
                        {
                            while (enumerator.MoveNext())
                            {
                                DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)enumerator.Current;
                                if (dataGridViewColumn.Visible && PrintDataGridView.list_0.Contains(dataGridViewColumn.HeaderText))
                                {
                                    e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), new Rectangle((int)PrintDataGridView.arrayList_0[num4], num, (int)PrintDataGridView.arrayList_1[num4], PrintDataGridView.int_5));
                                    e.Graphics.DrawRectangle(Pens.Black, new Rectangle((int)PrintDataGridView.arrayList_0[num4], num, (int)PrintDataGridView.arrayList_1[num4], PrintDataGridView.int_5));
                                    e.Graphics.DrawString(dataGridViewColumn.HeaderText, dataGridViewColumn.InheritedStyle.Font, new SolidBrush(dataGridViewColumn.InheritedStyle.ForeColor), new RectangleF((float)((int)PrintDataGridView.arrayList_0[num4]), (float)num, (float)((int)PrintDataGridView.arrayList_1[num4]), (float)PrintDataGridView.int_5), PrintDataGridView.stringFormat_0);
                                    num4++;
                                }
                            }
                        }
                        finally
                        {
                            IDisposable disposable = enumerator as IDisposable;
                            if (disposable != null)
                            {
                                disposable.Dispose();
                            }
                        }
                        PrintDataGridView.bool_0 = false;
                        num += PrintDataGridView.int_5;
                    }
                    num4 = 0;
                    enumerator = dataGridViewRow.Cells.GetEnumerator();
                    try
                    {
                        while (enumerator.MoveNext())
                        {
                            DataGridViewCell dataGridViewCell = (DataGridViewCell)enumerator.Current;
                            if (dataGridViewCell.OwningColumn.Visible && PrintDataGridView.list_0.Contains(dataGridViewCell.OwningColumn.HeaderText))
                            {
                                if (((Type)PrintDataGridView.arrayList_2[num4]).Name == "DataGridViewTextBoxColumn" || ((Type)PrintDataGridView.arrayList_2[num4]).Name == "DataGridViewLinkColumn")
                                {
                                    e.Graphics.DrawString(dataGridViewCell.Value.ToString(), dataGridViewCell.InheritedStyle.Font, new SolidBrush(dataGridViewCell.InheritedStyle.ForeColor), new RectangleF((float)((int)PrintDataGridView.arrayList_0[num4]), (float)num, (float)((int)PrintDataGridView.arrayList_1[num4]), (float)PrintDataGridView.int_3), PrintDataGridView.stringFormat_0);
                                }
                                else
                                {
                                    if (((Type)PrintDataGridView.arrayList_2[num4]).Name == "DataGridViewButtonColumn")
                                    {
                                        PrintDataGridView.button_0.Text = dataGridViewCell.Value.ToString();
                                        PrintDataGridView.button_0.Size = new Size((int)PrintDataGridView.arrayList_1[num4], PrintDataGridView.int_3);
                                        Bitmap bitmap = new Bitmap(PrintDataGridView.button_0.Width, PrintDataGridView.button_0.Height);
                                        PrintDataGridView.button_0.DrawToBitmap(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
                                        e.Graphics.DrawImage(bitmap, new Point((int)PrintDataGridView.arrayList_0[num4], num));
                                    }
                                    else
                                    {
                                        if (((Type)PrintDataGridView.arrayList_2[num4]).Name == "DataGridViewCheckBoxColumn")
                                        {
                                            PrintDataGridView.checkBox_0.Size = new Size(14, 14);
                                            PrintDataGridView.checkBox_0.Checked = (bool)dataGridViewCell.Value;
                                            Bitmap bitmap = new Bitmap((int)PrintDataGridView.arrayList_1[num4], PrintDataGridView.int_3);
                                            Graphics graphics = Graphics.FromImage(bitmap);
                                            graphics.FillRectangle(Brushes.White, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
                                            PrintDataGridView.checkBox_0.DrawToBitmap(bitmap, new Rectangle((bitmap.Width - PrintDataGridView.checkBox_0.Width) / 2, (bitmap.Height - PrintDataGridView.checkBox_0.Height) / 2, PrintDataGridView.checkBox_0.Width, PrintDataGridView.checkBox_0.Height));
                                            e.Graphics.DrawImage(bitmap, new Point((int)PrintDataGridView.arrayList_0[num4], num));
                                        }
                                        else
                                        {
                                            if (((Type)PrintDataGridView.arrayList_2[num4]).Name == "DataGridViewComboBoxColumn")
                                            {
                                                PrintDataGridView.comboBox_0.Size = new Size((int)PrintDataGridView.arrayList_1[num4], PrintDataGridView.int_3);
                                                Bitmap bitmap = new Bitmap(PrintDataGridView.comboBox_0.Width, PrintDataGridView.comboBox_0.Height);
                                                PrintDataGridView.comboBox_0.DrawToBitmap(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
                                                e.Graphics.DrawImage(bitmap, new Point((int)PrintDataGridView.arrayList_0[num4], num));
                                                e.Graphics.DrawString(dataGridViewCell.Value.ToString(), dataGridViewCell.InheritedStyle.Font, new SolidBrush(dataGridViewCell.InheritedStyle.ForeColor), new RectangleF((float)((int)PrintDataGridView.arrayList_0[num4] + 1), (float)num, (float)((int)PrintDataGridView.arrayList_1[num4] - 16), (float)PrintDataGridView.int_3), PrintDataGridView.stringFormat_1);
                                            }
                                            else
                                            {
                                                if (((Type)PrintDataGridView.arrayList_2[num4]).Name == "DataGridViewImageColumn")
                                                {
                                                    Rectangle rectangle = new Rectangle((int)PrintDataGridView.arrayList_0[num4], num, (int)PrintDataGridView.arrayList_1[num4], PrintDataGridView.int_3);
                                                    Size size = ((Image)dataGridViewCell.FormattedValue).Size;
                                                    e.Graphics.DrawImage((Image)dataGridViewCell.FormattedValue, new Rectangle((int)PrintDataGridView.arrayList_0[num4] + (rectangle.Width - size.Width) / 2, num + (rectangle.Height - size.Height) / 2, ((Image)dataGridViewCell.FormattedValue).Width, ((Image)dataGridViewCell.FormattedValue).Height));
                                                }
                                            }
                                        }
                                    }
                                }
                                e.Graphics.DrawRectangle(Pens.Black, new Rectangle((int)PrintDataGridView.arrayList_0[num4], num, (int)PrintDataGridView.arrayList_1[num4], PrintDataGridView.int_3));
                                num4++;
                            }
                        }
                    }
                    finally
                    {
                        IDisposable disposable = enumerator as IDisposable;
                        if (disposable != null)
                        {
                            disposable.Dispose();
                        }
                    }
                    num += PrintDataGridView.int_3;
                    PrintDataGridView.int_1++;
                    if (PrintDataGridView.int_2 == 1)
                    {
                        PrintDataGridView.int_4++;
                    }
                }
            IL_A31:
                if (PrintDataGridView.int_1 <= PrintDataGridView.dataGridView_0.Rows.Count - 1)
                {
                    goto IL_151;
                }
                if (PrintDataGridView.int_4 != 0)
                {
                    PrintDataGridView.smethod_2(e, PrintDataGridView.int_4);
                    e.HasMorePages = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
        private static void smethod_2(PrintPageEventArgs printPageEventArgs_0, int int_6)
        {
            double num = 0.0;
            if (PrintDataGridView.bool_1)
            {
                if (PrintDataGridView.dataGridView_0.Rows[PrintDataGridView.dataGridView_0.Rows.Count - 1].IsNewRow)
                {
                    num = (double)(PrintDataGridView.dataGridView_0.Rows.Count - 2);
                }
                else
                {
                    num = (double)(PrintDataGridView.dataGridView_0.Rows.Count - 1);
                }
            }
            else
            {
                num = (double)PrintDataGridView.dataGridView_0.SelectedRows.Count;
            }
            string text = PrintDataGridView.int_2.ToString() + " ֮ " + Math.Ceiling(num / (double)int_6).ToString();
            Graphics arg_12A_0 = printPageEventArgs_0.Graphics;
            string arg_12A_1 = text;
            Font arg_12A_2 = PrintDataGridView.dataGridView_0.Font;
            Brush arg_12A_3 = Brushes.Black;
            Rectangle marginBounds = printPageEventArgs_0.MarginBounds;
            float arg_108_0 = (float)marginBounds.Left;
            marginBounds = printPageEventArgs_0.MarginBounds;
            float arg_101_0 = (float)marginBounds.Width;
            Graphics arg_F3_0 = printPageEventArgs_0.Graphics;
            string arg_F3_1 = text;
            Font arg_F3_2 = PrintDataGridView.dataGridView_0.Font;
            marginBounds = printPageEventArgs_0.MarginBounds;
            float arg_12A_4 = arg_108_0 + (arg_101_0 - arg_F3_0.MeasureString(arg_F3_1, arg_F3_2, marginBounds.Width).Width) / 2f;
            marginBounds = printPageEventArgs_0.MarginBounds;
            float arg_125_0 = (float)marginBounds.Top;
            marginBounds = printPageEventArgs_0.MarginBounds;
            arg_12A_0.DrawString(arg_12A_1, arg_12A_2, arg_12A_3, arg_12A_4, arg_125_0 + (float)marginBounds.Height + (float)31);
        }
    }
}
