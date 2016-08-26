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
	public partial class addTrnsTmpltDiag : Form
		{
		public addTrnsTmpltDiag()
			{
			InitializeComponent();
			}
		public int orgid = -1;
		public long tmpltid = -1;
    bool txtChngd = false;
    public bool obey_events = false;

		private void addTrnsTmpltDiag_Load(object sender, EventArgs e)
			{
        this.obey_events = false;
        System.Windows.Forms.Application.DoEvents();
        Color[] clrs = Global.mnFrm.cmCde.getColors();
        this.BackColor = clrs[0];
        this.obey_events = true;
      }

		private void accntNumButton_Click(object sender, EventArgs e)
			{
			string[] selVals = new string[1];
			selVals[0] = this.accntIDTextBox.Text;
			DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
				Global.mnFrm.cmCde.getLovID("Transaction Accounts"), ref selVals, true, false, this.orgid);
			if (dgRes == DialogResult.OK)
				{
				for (int i = 0; i < selVals.Length; i++)
					{
					this.accntIDTextBox.Text = selVals[i];
					this.accntNameTextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));
					this.accntNumTextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(selVals[i]));
					}
				}
			}

		private void cancelButton_Click(object sender, EventArgs e)
			{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
			}

		private void OKButton_Click(object sender, EventArgs e)
			{
			if (this.trnsDescTextBox.Text == "")
				{
				Global.mnFrm.cmCde.showMsg("Please enter a Transaction Description!", 0);
				return;
				}
			if (this.incrsDcrsComboBox.Text == "")
				{
				Global.mnFrm.cmCde.showMsg("Please select whether to INCREASE or DECREASE Account!", 0);
				return;
				}
			if (this.accntNumTextBox.Text == "")
				{
				Global.mnFrm.cmCde.showMsg("Please select an Account!", 0);
				return;
				}
				if (this.trnsIDTextBox.Text == "")
					{
					Global.createTmpltTrns(int.Parse(this.accntIDTextBox.Text),
						this.trnsDescTextBox.Text, this.tmpltid, this.incrsDcrsComboBox.Text.Substring(0, 1));
					}
				else
					{
					Global.updateTmpltTrns(int.Parse(this.accntIDTextBox.Text),
						this.trnsDescTextBox.Text, this.tmpltid, 
						this.incrsDcrsComboBox.Text.Substring(0, 1), long.Parse(this.trnsIDTextBox.Text));
					}
			this.DialogResult = DialogResult.OK;
			this.Close();
			}

    private void accntNumTextBox_TextChanged(object sender, EventArgs e)
    {
      if (this.obey_events == false)
      {
        return;
      }
      this.txtChngd = true;
    }

    private void accntNumTextBox_Leave(object sender, EventArgs e)
    {
      if (this.txtChngd == false)
      {
        return;
      }
      this.txtChngd = false;
      TextBox mytxt = (TextBox)sender;

      if (mytxt.Name == "accntNumTextBox")
      {
        this.accntNmLOVSearch();
      }
      this.txtChngd = false;
    }


    private void accntNmLOVSearch()
    {
      if (!this.accntNumTextBox.Text.Contains("%"))
      {
        this.accntNumTextBox.Text = "%" + this.accntNumTextBox.Text.Replace(" ", "%") + "%";
        this.accntIDTextBox.Text = "-1";
      }

      string[] selVals = new string[1];
      selVals[0] = this.accntIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        Global.mnFrm.cmCde.getLovID("Transaction Accounts"), ref selVals,
        true, true, this.orgid,
       this.accntNumTextBox.Text, "Both", true);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.accntIDTextBox.Text = selVals[i];
          this.accntNameTextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));
          this.accntNumTextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(selVals[i]));
        }
      }
    }

    private void accntNumTextBox_Click(object sender, EventArgs e)
    {
      this.accntNumTextBox.SelectAll();
    }
  
		}
	}