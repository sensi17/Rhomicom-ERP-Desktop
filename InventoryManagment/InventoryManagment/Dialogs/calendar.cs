using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StoresAndInventoryManager.Classes;

namespace StoresAndInventoryManager.Forms
{
    public partial class calendar : Form
    {
        public calendar()
        {
            InitializeComponent();
        }

        public string DATESELECTED
        {
            get { return this.txtCalendarDateSelected.Text; }
        }

        private void btnCalendarCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void monthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            this.txtCalendarDateSelected.Text = e.Start.ToString("dd-MMM-yyyy HH:mm:ss");
        }

        private void calendar_Load(object sender, EventArgs e)
        {
            Color[] clrs = Global.mnFrm.cmCde.getColors();
            this.BackColor = clrs[0];
            this.txtCalendarDateSelected.Text = DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss");
        }

        private void btnCalendarOk_Click(object sender, EventArgs e)
        {
            DateTime dt;
            if (this.txtCalendarDateSelected.Text == "")
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else if (DateTime.TryParse(this.txtCalendarDateSelected.Text, out dt) == true)
            {
              this.txtCalendarDateSelected.Text = dt.ToString("dd-MMM-yyyy HH:mm:ss");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Enter a valid date in format (dd-MMM-yyyy) e.g. 31-Jul-2013 ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCalendarFDte_Click(object sender, EventArgs e)
        {
            txtCalendarDateSelected.Text = "31-Dec-4000 11:59:59";
        }

        private void btnCalendarClear_Click(object sender, EventArgs e)
        {
            txtCalendarDateSelected.Clear();
        }
    }
}