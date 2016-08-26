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
  public partial class addBdgtLineDiag : Form
  {
    public addBdgtLineDiag()
    {
      InitializeComponent();
    }
    public long bdgtID = -1;
    public long bdgtDtID = -1;
    public int orgid = -1;
    bool txtChngd = false;
    public bool obey_events = false;

    private void OKButton_Click(object sender, EventArgs e)
    {
      if (this.accntNumTextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please enter an Account!", 0);
        return;
      }
      if (this.startDteTextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please enter a Period Start Date!", 0);
        return;
      }
      if (this.endDteTextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please enter a Period End Date!", 0);
        return;
      }
      if (this.actionComboBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please select an Action to take if Limit is Exceeded!", 0);
        return;
      }

      //if (Global.mnFrm.cmCde.showMsg("Are you sure you want to Create this Transaction!", 1) == DialogResult.No)
      //{
      //  Global.mnFrm.cmCde.showMsg("Transaction Cancelled!", 0);
      //  return;
      //}
      long oldBdgtDtID1 = Global.doesBdgtDteOvrlap(this.bdgtID,
        int.Parse(this.accntIDTextBox.Text), this.startDteTextBox.Text);
      long oldBdgtDtID2 = Global.doesBdgtDteOvrlap(this.bdgtID,
       int.Parse(this.accntIDTextBox.Text), this.endDteTextBox.Text);
      if (this.bdgtDtID <= 0 && oldBdgtDtID1 > 0)
      {
        Global.mnFrm.cmCde.showMsg("Period Start Date already Exists in another Budget Period for the Same Account!", 0);
        return;
      }
      if (this.bdgtDtID <= 0 && oldBdgtDtID2 > 0)
      {
        Global.mnFrm.cmCde.showMsg("Period End Date already Exists in another Budget Period for the Same Account!", 0);
        return;
      }
      if (this.bdgtDtID > 0 && oldBdgtDtID1 > 0 && this.bdgtDtID != oldBdgtDtID1)
      {
        Global.mnFrm.cmCde.showMsg("New Period Start Date already Exists in another Budget Period for the Same Account!", 0);
        return;
      }
      if (this.bdgtDtID > 0 && oldBdgtDtID2 > 0 && this.bdgtDtID != oldBdgtDtID2)
      {
        Global.mnFrm.cmCde.showMsg("New Period End Date already Exists in another Budget Period for the Same Account!", 0);
        return;
      }
      if (this.bdgtDtID <= 0)
      {
        Global.createBdgtLn(this.bdgtID, int.Parse(this.accntIDTextBox.Text),
          (double)this.amntNumericUpDown.Value,
            this.startDteTextBox.Text, this.endDteTextBox.Text,
            this.actionComboBox.Text);
      }
      else
      {
        Global.updateBdgtLn(this.bdgtDtID, int.Parse(this.accntIDTextBox.Text),
          (double)this.amntNumericUpDown.Value, this.startDteTextBox.Text,
            this.endDteTextBox.Text
            , this.actionComboBox.Text);
      }
      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void startDteButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.selectDate(ref this.startDteTextBox);
      if (this.startDteTextBox.Text.Length > 10)
      {
        this.startDteTextBox.Text = this.startDteTextBox.Text.Substring(0, 11) + " 00:00:00";
      }
    }

    private void endDteButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.selectDate(ref this.endDteTextBox);
      if (this.endDteTextBox.Text.Length > 10)
      {
        this.endDteTextBox.Text = this.endDteTextBox.Text.Substring(0, 11) + " 23:59:59";
      }
    }

    private void accntNumButton_Click(object sender, EventArgs e)
    {
      string[] selVals = new string[1];
      selVals[0] = this.accntIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        Global.mnFrm.cmCde.getLovID("Budget Accounts"), ref selVals, true, false, this.orgid);
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

    private void addBdgtLineDiag_Load(object sender, EventArgs e)
    {
      this.obey_events = false;
      System.Windows.Forms.Application.DoEvents();
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      this.obey_events = true;
    }

    private void startDteTextBox_TextChanged(object sender, EventArgs e)
    {
      if (this.obey_events == false)
      {
        return;
      }
      this.txtChngd = true;
    }

    private void startDteTextBox_Leave(object sender, EventArgs e)
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
      else
      {
        this.trnsDteLOVSrch(mytxt);
      }
      this.txtChngd = false;
    }

    private void trnsDteLOVSrch(TextBox mytxt)
    {
      DateTime dte1 = DateTime.Now;
      bool sccs = DateTime.TryParse(mytxt.Text, out dte1);
      if (!sccs)
      {
        dte1 = DateTime.Now;
      }
      if (mytxt.Name == "endDteTextBox")
      {
        mytxt.Text = dte1.ToString("dd-MMM-yyyy HH:mm:ss").Replace("00:00:00", "23:59:59");
      }
      else
      {
        mytxt.Text = dte1.ToString("dd-MMM-yyyy HH:mm:ss");
      }
    }

    private void accntNmLOVSearch()
    {
      if (!this.accntNumTextBox.Text.Contains("%"))
      {
        this.accntNumTextBox.Text = "%" + this.accntNumTextBox.Text + "%";
        this.accntIDTextBox.Text = "-1";
      }

      string[] selVals = new string[1];
      selVals[0] = this.accntIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        Global.mnFrm.cmCde.getLovID("Budget Accounts"), ref selVals,
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

    private void startDteTextBox_Click(object sender, EventArgs e)
    {
      TextBox mytxt = (TextBox)sender;
      //mytxt.SelectAll();

      if (mytxt.Name == "startDteTextBox")
      {
        this.startDteTextBox.SelectAll();
      }
      else if (mytxt.Name == "endDteTextBox")
      {
        this.endDteTextBox.SelectAll();
      }
      else if (mytxt.Name == "accntNumTextBox")
      {
        this.accntNumTextBox.SelectAll();
      }
    }
  }
}