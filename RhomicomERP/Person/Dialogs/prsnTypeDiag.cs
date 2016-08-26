using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BasicPersonData.Classes;

namespace BasicPersonData.Dialogs
{
  public partial class prsnTypeDiag : Form
  {
    public prsnTypeDiag()
    {
      InitializeComponent();
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      if (this.prsnTypTextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please indicate the Person Type!", 0);
        return;
      }
      if (this.reasonTextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please indicate the Reason for this assignment!", 0);
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

    private void prsnTypeButton_Click(object sender, EventArgs e)
    {
      //Person Types
      int[] selVals = new int[1];
      selVals[0] = Global.mnFrm.cmCde.getPssblValID(this.prsnTypTextBox.Text,
        Global.mnFrm.cmCde.getLovID("Person Types"));
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        Global.mnFrm.cmCde.getLovID("Person Types"), ref selVals, true, false);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.prsnTypTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
        }
      }
    }

    private void reasonButton_Click(object sender, EventArgs e)
    {
      //Person Type Change Reasons
      int[] selVals = new int[1];
      selVals[0] = Global.mnFrm.cmCde.getPssblValID(this.reasonTextBox.Text,
        Global.mnFrm.cmCde.getLovID("Person Type Change Reasons"));
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        Global.mnFrm.cmCde.getLovID("Person Type Change Reasons"), ref selVals, true, false);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.reasonTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
        }
      }
    }

    private void futhDetButton_Click(object sender, EventArgs e)
    {
      //Person Types-Further Details
      int[] selVals = new int[1];
      selVals[0] = Global.mnFrm.cmCde.getPssblValID(this.furtherDetTextBox.Text,
        Global.mnFrm.cmCde.getLovID("Person Types-Further Details"));
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        Global.mnFrm.cmCde.getLovID("Person Types-Further Details"), ref selVals, true, false);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.furtherDetTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
        }
      }
    }

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

    private void prsnTypeDiag_Load(object sender, EventArgs e)
    {
      System.Windows.Forms.Application.DoEvents();
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
    }
  }
}