using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace StoresAndInventoryManager.Forms
{
    public partial class adjustmentPrompt : Form
    {
        public adjustmentPrompt()
        {
            InitializeComponent();
        }

        public string NEWTOTALQTY
        {
            get { return this.newTtlQtytextBox.Text; }
            set { this.newTtlQtytextBox.Text = value; }
        }

        public string EXISTNCNSGMNTID
        {
            get { return this.cnsgmntNotextBox.Text; }
            set { this.cnsgmntNotextBox.Text = value; }
        }

        public string EXISTNCNSGMNTTOTALQTY
        {
            get { return this.cnsgmntTtlQtytextBox.Text; }
            set { this.cnsgmntTtlQtytextBox.Text = value; }
        }

        public string LINETOTALQTY
        {
            get { return this.lineQtytextBox.Text; }
            set { this.lineQtytextBox.Text = value; }
        }

        private void adjustmentPrompt_Load(object sender, EventArgs e)
        {

        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void newQtyRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (newQtyRadioButton.Checked == true)
            {
                this.newTtlQtytextBox.Text = double.Parse(lineQtytextBox.Text).ToString();
            }
        }

        private void consolidateRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (consolidateRadioButton.Checked == true)
            {
                this.newTtlQtytextBox.Text = (double.Parse(lineQtytextBox.Text) + double.Parse(this.cnsgmntTtlQtytextBox.Text)).ToString();
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
