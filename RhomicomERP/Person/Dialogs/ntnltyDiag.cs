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
  public partial class ntnltyDiag : Form
  {
    public ntnltyDiag()
    {
      InitializeComponent();
    }

    private void ntnltyButton_Click(object sender, EventArgs e)
    {
      //Nationalities
      int[] selVals = new int[1];
      selVals[0] = Global.mnFrm.cmCde.getPssblValID(this.ntnltyTextBox.Text,
        Global.mnFrm.cmCde.getLovID("Countries"));
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        Global.mnFrm.cmCde.getLovID("Countries"), ref selVals, true, false);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.ntnltyTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
        }
      }
    }

    private void idTypeButton_Click(object sender, EventArgs e)
    {
      //National ID Types
      int[] selVals = new int[1];
      selVals[0] = Global.mnFrm.cmCde.getPssblValID(this.idTypeTextBox.Text,
        Global.mnFrm.cmCde.getLovID("National ID Types"));
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        Global.mnFrm.cmCde.getLovID("National ID Types"), ref selVals, true, false);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.idTypeTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
        }
      }
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void ntnltyDiag_Load(object sender, EventArgs e)
    {
      System.Windows.Forms.Application.DoEvents();
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
    }

    private void dteIssuedButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.selectDate(ref this.dateIssuedTextBox);
      if (this.dateIssuedTextBox.Text.Length > 11)
      {
        this.dateIssuedTextBox.Text = this.dateIssuedTextBox.Text.Substring(0, 11);
      }
    }

    private void expryDateButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.selectDate(ref this.expryDateTextBox);
      if (this.expryDateTextBox.Text.Length > 11)
      {
        this.expryDateTextBox.Text = this.expryDateTextBox.Text.Substring(0, 11);
      }
    }
  }
}