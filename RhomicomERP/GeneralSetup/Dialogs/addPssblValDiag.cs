using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GeneralSetup.Classes;

namespace GeneralSetup.Dialogs
{
  public partial class addPssblValDiag : Form
  {
    public addPssblValDiag()
    {
      InitializeComponent();
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      if (this.pssblValTextBox.Text == "")
      {
        Global.myNwMainFrm.cmmnCodeGstp.showMsg("Please fill all required Fields!", 0);
        return;
      }
      if (this.pssblValIDTextBox.Text == "" || this.pssblValIDTextBox.Text == "-1")
      {
        if (Global.getPssblValID(this.pssblValTextBox.Text, int.Parse(this.lovIDTextBox.Text), this.descPssblVlTextBox.Text) > 0)
        {
          Global.myNwMainFrm.cmmnCodeGstp.showMsg("Possible Value is already in use by this Value List Name!", 0);
          return;
        }
        Global.createPssblValsForLov(int.Parse(this.lovIDTextBox.Text), this.pssblValTextBox.Text,
          this.descPssblVlTextBox.Text, this.isEnbldVlNmCheckBox.Checked, this.allwdOrgsTextBox.Text);
        if (Global.myNwMainFrm.cmmnCodeGstp.showMsg("Possible Value Saved Successfully!" +
  "\r\nDo you want to create another one?", 2) == DialogResult.Yes)
        {
          this.pssblValIDTextBox.Text = "-1";
          this.pssblValTextBox.Text = "";
          this.isEnbldVlNmCheckBox.Checked = false;
          this.descPssblVlTextBox.Text = "";
          //this.allwdOrgsTextBox.Text + Global.get_all_OrgIDs();
        }
        else
        {
          this.DialogResult = DialogResult.OK;
          this.Close();
        }
      }
      else
      {
        if (Global.getPssblValID(this.pssblValTextBox.Text, int.Parse(this.lovIDTextBox.Text), this.descPssblVlTextBox.Text) !=
        int.Parse(this.pssblValIDTextBox.Text))
        {
          if (Global.getPssblValID(this.pssblValTextBox.Text, int.Parse(this.lovIDTextBox.Text), this.descPssblVlTextBox.Text) > 0)
          {
            Global.myNwMainFrm.cmmnCodeGstp.showMsg("New Possible Value is already in use!", 0);
            return;
          }
        }
        Global.updatePssblValsForLov(int.Parse(this.pssblValIDTextBox.Text), this.pssblValTextBox.Text,
          this.descPssblVlTextBox.Text, this.isEnbldVlNmCheckBox.Checked, this.allwdOrgsTextBox.Text);
        this.DialogResult = DialogResult.OK;
        this.Close();
      }
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void addPssblValDiag_Load(object sender, EventArgs e)
    {
      System.Windows.Forms.Application.DoEvents();
      Color[] clrs = Global.myNwMainFrm.cmmnCodeGstp.getColors();
      this.BackColor = clrs[0];
      this.pssblValTextBox.Focus();
      this.pssblValTextBox.SelectAll();
    }

    private void pssblValTextBox_TextChanged(object sender, EventArgs e)
    {
      
    }

    private void pssblValTextBox_Leave(object sender, EventArgs e)
    {
      if (this.descPssblVlTextBox.Text == "")
      {
        this.descPssblVlTextBox.Text = this.pssblValTextBox.Text;
      }
    }
  }
}