using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StoresAndInventoryManager.Classes;
using StoresAndInventoryManager.Forms;

namespace StoresAndInventoryManager.Forms
{
    public partial class adjustmentSourceDiag : Form
    {
        public adjustmentSourceDiag()
        {
            InitializeComponent();
        }

        //invAdjstmnt adjstFrm = new invAdjstmnt();
        consgmtRcpt newRcpt = new consgmtRcpt();

        public string SOURCETYPE
        {
            get { return this.srcComboBox.Text; }
            set { this.srcComboBox.Text = value; }
        }

        public string SOURCENUMBER
        {
            get { return this.numberTextBox.Text; }
            set { this.numberTextBox.Text = value; }
        }

        private void srcComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (srcComboBox.SelectedItem.ToString().Equals("ITEM"))
            {
                numberLabel.Text = "Code:";
                numberTextBox.Clear();
            }
            else
            {
                numberLabel.Text = "Number";
                numberTextBox.Clear();
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            if (!(srcComboBox.SelectedItem.ToString().Equals("ITEM")))
            {
                int output = 0;
                string rslt = string.Empty;
                if (int.TryParse(numberTextBox.Text, out output))
                {
                    if (srcComboBox.SelectedItem.ToString().Equals("CONSIGNMENT"))
                    {
                        //validate consignment
                        rslt = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_consgmt_rcpt_det", "consgmt_id", "consgmt_id", (long)output);
                    }
                    else
                    {
                        //validate stock
                        //validate consignment
                        rslt = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_stock", "stock_id", "stock_id", (long)output);
                    }

                    if (rslt == "")
                    {
                        Global.mnFrm.cmCde.showMsg(srcComboBox.Text + " does not exist \r\nEnter a valid " + srcComboBox.Text + " number", 0);
                        return;
                    }
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    Global.mnFrm.cmCde.showMsg("Enter a valid " + srcComboBox.Text + " number", 0);
                    numberTextBox.Focus();
                    numberTextBox.SelectAll();
                }
            }
            else
            {
                if (numberTextBox.Text == "" || Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_list", "item_id", "item_code", this.newRcpt.getItemID(numberTextBox.Text)) == "")
                {
                    Global.mnFrm.cmCde.showMsg("Enter a valid " + srcComboBox.Text + " code", 0);
                    numberTextBox.Focus();
                    numberTextBox.SelectAll();
                    return;
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void adjustmentSourceDiag_Load(object sender, EventArgs e)
        {
            if (this.srcComboBox.Text == "")
            {
                this.srcComboBox.SelectedIndex = 2;
            }

            numberTextBox.Focus();
            numberTextBox.SelectAll();
        }
    }
}
