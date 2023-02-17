using DJ.LMS.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DJ.LMS.WinForms
{
    public partial class FrmItemMainEdit : Form
    {
        private DataRowView dataRow;
        private bool IsEdit { get; set; }
        private bool IsSubNode { get; set; }
        private int ItemID { get; set; }
        private int ItemParentID { get; set; }

        public FrmItemMainEdit(DataRowView row, bool isEdit, bool isSubNode)
        {
            InitializeComponent();
            this.dataRow = row;
            this.ItemID = -1;
            this.ItemParentID = 0;
            this.IsEdit = isEdit;
            this.IsSubNode = isSubNode;
        }
        private void FrmItemMainEdit_Load(object sender, EventArgs e)
        {
            chkIsEffictive.Checked = true;
            if (IsEdit)
            {
                this.ItemID = int.Parse(dataRow["ItemID"].ToString());
                this.ItemParentID = int.Parse(dataRow["ItemParentID"].ToString());
                this.txtItemCode.EditValue = dataRow["ItemCode"];
                this.txtItemName.EditValue = dataRow["ItemName"];
                this.chkIsEffictive.EditValue = dataRow["IsEffictive"];
                this.txtRemark.EditValue = dataRow["Remark"];
            }
            LoadParentID();
        }

        private void LoadParentID()
        {
            try
            {
                var table = CommonFunc.GetItemMainItems();
                table.PrimaryKey = new DataColumn[] { table.Columns["ItemID"] };
                if (this.ItemParentID > 0)
                {
                    this.cmbItemParentID.EditValue = this.ItemParentID;
                }
                if (this.ItemID > 0)
                {
                    var row = table.Rows.Find(this.ItemID);
                    table.Rows.Remove(row);
                }
                if (!IsEdit && IsSubNode && dataRow != null)
                {
                    this.cmbItemParentID.EditValue = dataRow["ItemID"];
                }
                if (!IsEdit && !IsSubNode && dataRow != null)
                {
                    this.cmbItemParentID.EditValue = dataRow["ItemParentID"];
                }
                cmbItemParentID.Properties.DataSource = table;
                gridList.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
                cmbItemParentID.Properties.DataSource = null;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //if (!dxValidationProvider1.Validate())
            //    return;
            string message = "";// SubmitValidation();
            if (!string.IsNullOrEmpty(message))
            {
                MessageUtil.ShowError(message);
                return;
            }
            //if (MessageUtil.ShowYesNoAndTips("请确认是否保存信息？") == DialogResult.No)
            //    return;
            try
            {

                var itemCode = txtItemCode.EditValue.ToString();
                var itemName = txtItemName.EditValue.ToString();
                var remark = txtRemark.EditValue?.ToString();
                var itemParentID = (int)(cmbItemParentID.EditValue ?? 0);

                if (CommonFunc.ModifyItemMain(ItemID, itemCode, itemName, chkIsEffictive.Checked, remark, itemParentID))
                {
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                    MessageUtil.ShowError("修改失败");
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
            }

        }
    }
}
