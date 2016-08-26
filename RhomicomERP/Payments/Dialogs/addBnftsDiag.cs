using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using InternalPayments.Classes;

namespace InternalPayments.Dialogs
	{
	public partial class addBnftsDiag : Form
		{
		public addBnftsDiag()
			{
			InitializeComponent();
			}
		public int orgID = -1;
		private void dte1Button_Click(object sender, EventArgs e)
			{
			Global.mnFrm.cmCde.selectDate(ref this.vldStrtDteTextBox);
			if (this.vldStrtDteTextBox.Text.Length > 11)
				{
				this.vldStrtDteTextBox.Text = this.vldStrtDteTextBox.Text.Substring(0, 11);
				}
			}

		private void dte2Button_Click(object sender, EventArgs e)
			{
			Global.mnFrm.cmCde.selectDate(ref this.vldEndDteTextBox);
			if (this.vldEndDteTextBox.Text.Length > 11)
				{
				this.vldEndDteTextBox.Text = this.vldEndDteTextBox.Text.Substring(0, 11);
				}
			}

		private void itmNameButton_Click(object sender, EventArgs e)
			{
			//Item Names
			string[] selVals = new string[1];
			selVals[0] = this.itemIDTextBox.Text;
			DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
				Global.mnFrm.cmCde.getLovID("Pay Items"), ref selVals, true, true, this.orgID);
			if (dgRes == DialogResult.OK)
				{
				for (int i = 0; i < selVals.Length; i++)
					{
					this.itemIDTextBox.Text = selVals[i];
					this.itmNameTextBox.Text = Global.mnFrm.cmCde.getItmName(int.Parse(selVals[i]));
					}
				}
			}

		private void itmValButton_Click(object sender, EventArgs e)
			{
			//Item Names
			if (this.itemIDTextBox.Text == "-1"||
				this.itemIDTextBox.Text=="")
				{
				Global.mnFrm.cmCde.showMsg("Please select an Item First", 0);
				return;
				}
			string[] selVals = new string[1];
			selVals[0] = this.itmValIDTextBox.Text;
			DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
				Global.mnFrm.cmCde.getLovID("Pay Item Values"), ref selVals, 
				true, true, int.Parse(this.itemIDTextBox.Text));
			if (dgRes == DialogResult.OK)
				{
				for (int i = 0; i < selVals.Length; i++)
					{
					this.itmValIDTextBox.Text = selVals[i];
					this.itmValNameTextBox.Text = Global.mnFrm.cmCde.getItmValName(int.Parse(selVals[i]));
					}
				}
			}

		private void okButton_Click(object sender, EventArgs e)
			{
			if (this.itemIDTextBox.Text == "-1"
				|| this.itemIDTextBox.Text == "")
				{
				Global.mnFrm.cmCde.showMsg("Please select an Item!", 0);
				return;
				}
			if (this.itmValIDTextBox.Text == "-1"
			|| this.itmValIDTextBox.Text == "")
				{
				Global.mnFrm.cmCde.showMsg("Please select a Possible Value!", 0);
				return;
				} 
			this.DialogResult = DialogResult.OK;
			this.Close();
			}

		private void cancelButton_Click(object sender, EventArgs e)
			{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
			}

    private void addBnftsDiag_Load(object sender, EventArgs e)
    {
      System.Windows.Forms.Application.DoEvents();
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
    }
		}
	}