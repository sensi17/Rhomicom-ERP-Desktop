using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Accounting.Classes;

namespace Accounting.Dialogs
	{
	public partial class nwDateDiag : Form
		{
		public nwDateDiag()
			{
			InitializeComponent();
			}
		public string last_period_date = "";
		private void nwDateDiag_Load(object sender, EventArgs e)
			{
        System.Windows.Forms.Application.DoEvents();
        Color[] clrs = Global.mnFrm.cmCde.getColors();
        this.BackColor = clrs[0];
			}

		private void OKButton_Click(object sender, EventArgs e)
		{
			if(this.dateTextBox.Text=="")
				{
				Global.mnFrm.cmCde.showMsg("Please provide a Date!", 0);
				return;
				}

			if(DateTime.Parse(this.dateTextBox.Text)<=DateTime.Parse(this.last_period_date))
				{
				Global.mnFrm.cmCde.showMsg("Please provide a Date that comes after"+
					"\r\n the Last Date in the Previous Period! i.e.("+this.last_period_date+")",0);
				return;
				}
		}

    private void dateButton_Click(object sender, EventArgs e)
    {

    }
		}
	}