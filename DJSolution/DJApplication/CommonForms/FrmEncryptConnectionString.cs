using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DJ.LMS.Utilities;

namespace DJ.LMS.WinForms
{
    public partial class FrmEncryptConnectionString : Form
    {
        public FrmEncryptConnectionString()
        {
            InitializeComponent();
        }

        private void FrmEncryptConnectionString_Load(object sender, EventArgs e)
        {
            var connection = new AppConfig().GetKeyValue("connectionString");
            if (!string.IsNullOrWhiteSpace(connection))
            {
                txtSource.Text = connection;
            }
        }

        private void btnEncrypt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtResult.Text = EncodeHelper.AES_Encrypt(txtSource.Text);
        }

        private void btnDecrypt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtResult.Text = EncodeHelper.AES_Decrypt(txtSource.Text);
        }

        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnExchange_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var tempString = txtSource.Text;
            txtSource.Text = txtResult.Text;
            txtResult.Text = tempString;
        }

        private void btnCopyToClipboard_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Clipboard.SetText(txtResult.Text);
        }

    }
}
