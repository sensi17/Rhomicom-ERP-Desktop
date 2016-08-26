using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EventsAndAttendance.Classes;

namespace EventsAndAttendance.Dialogs
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
      selVals[0] = this.accntID1TextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        Global.mnFrm.cmCde.getLovID("Transaction Accounts"), ref selVals, true, false, this.orgid);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.accntID1TextBox.Text = selVals[i];
          this.accntName1TextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));
          this.accntNum1TextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(selVals[i]));
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
      if (this.incrsDcrs1ComboBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please select whether to INCREASE or DECREASE Costing Account!", 0);
        return;
      }
      if (this.accntNum1TextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please select a Costing Account!", 0);
        return;
      }
      if (this.incrsDcrs2ComboBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please select whether to INCREASE or DECREASE Balancing Account!", 0);
        return;
      }
      if (this.accntNum2TextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please select a Balancing Account!", 0);
        return;
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
      if (!this.accntNum1TextBox.Text.Contains("%"))
      {
        this.accntNum1TextBox.Text = "%" + this.accntNum1TextBox.Text.Replace(" ", "%") + "%";
        this.accntID1TextBox.Text = "-1";
      }

      string[] selVals = new string[1];
      selVals[0] = this.accntID1TextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        Global.mnFrm.cmCde.getLovID("Transaction Accounts"), ref selVals,
        true, true, this.orgid,
       this.accntNum1TextBox.Text, "Both", true);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.accntID1TextBox.Text = selVals[i];
          this.accntName1TextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));
          this.accntNum1TextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(selVals[i]));
        }
      }
    }

    private void accntNmLOVSearch1()
    {
      if (!this.accntNum1TextBox.Text.Contains("%"))
      {
        this.accntNum2TextBox.Text = "%" + this.accntNum2TextBox.Text.Replace(" ", "%") + "%";
        this.accntID2TextBox.Text = "-1";
      }

      string[] selVals = new string[1];
      selVals[0] = this.accntID2TextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        Global.mnFrm.cmCde.getLovID("Transaction Accounts"), ref selVals,
        true, true, this.orgid,
       this.accntNum1TextBox.Text, "Both", true);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.accntID2TextBox.Text = selVals[i];
          this.accntName2TextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));
          this.accntNum2TextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(selVals[i]));
        }
      }
    }

    private void accntNumTextBox_Click(object sender, EventArgs e)
    {
      this.accntNum1TextBox.SelectAll();
    }

    private void accntNum2Button_Click(object sender, EventArgs e)
    {
      string[] selVals = new string[1];
      selVals[0] = this.accntID2TextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        Global.mnFrm.cmCde.getLovID("Transaction Accounts"), ref selVals, true, false, this.orgid);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.accntID2TextBox.Text = selVals[i];
          this.accntName2TextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));
          this.accntNum2TextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(selVals[i]));
        }
      }

    }

  }
}