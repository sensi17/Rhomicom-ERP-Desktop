using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ReportsAndProcesses.Classes;

namespace ReportsAndProcesses.Dialogs
{
  public partial class addParamsDiag : Form
  {
    public addParamsDiag()
    {
      InitializeComponent();
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      if (this.paramNameTextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please provide a Parameter Name!", 0);
        return;
      }
      if (this.sqlRepTextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please provide the SQL Representation of the Parameter!", 0);
        return;
      }
      if (this.dataTypeComboBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please indicate the Datatype of the Parameter!", 0);
        return;
      }
      if (this.dataTypeComboBox.Text == "DATE" && this.dateFrmtComboBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please indicate the Date Format of the Parameter!", 0);
        return;
      }
      if (this.dataTypeComboBox.Text == "DATE" && this.defaultValTextBox.Text != "")
      {
        DateTime dte;
        bool isdte = DateTime.TryParseExact(this.defaultValTextBox.Text, this.dateFrmtComboBox.Text,
System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeLocal, out dte);
        if (!isdte)
        {
          Global.mnFrm.cmCde.showMsg("Default Value does not Match the Date Format of the Parameter!", 0);
          return;
        }
      }
      if (this.dataTypeComboBox.Text == "NUMBER" && this.defaultValTextBox.Text != "")
      {
        this.defaultValTextBox.Text = Global.computeMathExprsn(this.defaultValTextBox.Text).ToString();
      }
      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    private void lovNmButton_Click(object sender, EventArgs e)
    {
      string[] selVals = new string[1];
      selVals[0] = this.lovIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
       Global.mnFrm.cmCde.getLovID("LOV Names"), ref selVals, true, false);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.lovIDTextBox.Text = selVals[i];
          this.lovNmTextBox.Text = Global.mnFrm.cmCde.getLovNm(int.Parse(selVals[i]));
        }
      }
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void addParamsDiag_Load(object sender, EventArgs e)
    {
      System.Windows.Forms.Application.DoEvents();
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
    }

    private void dataTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.dataTypeComboBox.Text == "TEXT")
      {
        this.dateFrmtComboBox.SelectedItem = "None";
      }
      else if (this.dataTypeComboBox.Text == "NUMBER")
      {
        this.dateFrmtComboBox.SelectedItem = "None";
      }
      else if (this.dataTypeComboBox.Text == "DATE")
      {
        this.dateFrmtComboBox.SelectedItem = "yyyy-MM-dd";
      }
    }
  }
}