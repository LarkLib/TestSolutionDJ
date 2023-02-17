using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace DJ.LMS.WinForms
{
    public class PrintPreviewDialogWithPrinterSelect : PrintPreviewDialog
    {
        private PrintDialog dlgPrint = new PrintDialog();  
	    private ToolStrip tb;  
	    private ToolStripButton btPrint = new ToolStripButton();  
	    private ToolStripButton btPrinter = new ToolStripButton();  
	    private StatusStrip ssStatusStrip = new StatusStrip();  
	    private ToolStripButton btPage = new ToolStripButton();  
	    private ToolStripStatusLabel tsslPrinter = new ToolStripStatusLabel();
        public PrintPreviewDialogWithPrinterSelect()
            : base()
        {
            MyInitializeComponent();
        }

        private void MyInitializeComponent()  
	    {  
	        dlgPrint.ShowNetwork = true;  
	        //Add an empty document      
	        if (this.Document == null)  
	        {  
	            this.Document = new PrintDocument();  
	        }  
	        //Take the ToolStrip1 from PrintPreviewDialog      
	        tb = (ToolStrip)this.Controls["ToolStrip1"];  
	        //Take the Print button of ToolStrip1      
	        btPrint = (ToolStripButton)tb.Items["PrintToolStripButton"];  
	        //Add a Separator to ToolStrip1      
	        ToolStripSeparator s = new ToolStripSeparator();  
	        tb.Items.Add(s);  
	        //Add a button for selection printer      
	        btPrinter.Name = "btPrinter";  
	        btPrinter.Text = "打印设置";  
	        btPrinter.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btPrinter.Image = Properties.Resources.printer;
            //tb.Items.Add(btPrinter);
            tb.Items.Insert(0, btPrinter);
	        btPrinter = (ToolStripButton)tb.Items["btPrinter"];  
            //Gine the name of the default printer to the ToolStripStatusLabel (that's whay i added an empty document)      
	        tsslPrinter.Text = this.Document.PrinterSettings.PrinterName;  
	        //Add the ToolStripStatusLabel to the StatusStrip      
	        ssStatusStrip.Items.Add(tsslPrinter);  
	        //Add the StatusStrip to me (MyPrintPreviewDialogWithPrinterSelect)      
	        this.Controls.Add(ssStatusStrip);  
	        btPrinter.Click += new EventHandler(btPrinter_Click);  
	    }
  
        //The event of the new button      
	    private void btPrinter_Click(object sender, System.EventArgs e)  
	    {  
	        if (dlgPrint.ShowDialog() == DialogResult.OK)  
	        {  
	            if (this.Document != null)  
	            {  
	                this.Document.PrinterSettings = dlgPrint.PrinterSettings;  
	                this.Document.DefaultPageSettings.Landscape = true;  
	                tsslPrinter.Text = this.Document.PrinterSettings.PrinterName;  
	            }  
	        }  
	    }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // PrintPreviewDialogWithPrinterSelect
            // 
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Name = "PrintPreviewDialogWithPrinterSelect";
            this.ResumeLayout(false);

        }  
    }
}
