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
  public partial class rptParamsDiag : Form
  {
    public rptParamsDiag()
    {
      InitializeComponent();
    }
    bool txtChngd = false;
    public bool obey_events = false;

    private void OKButton_Click(object sender, EventArgs e)
    {
      this.docTypComboBox.Focus();
      if (this.startDteTextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please enter a Start Date!", 0);
        return;
      }
      if (this.endDteTextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please enter an End Date!", 0);
        return;
      }
      if (this.docTypComboBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please select a Document Type!", 0);
        return;
      }
      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    private void startDteButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.selectDate(ref this.startDteTextBox);
      if (this.startDteTextBox.Text.Length > 11)
      {
        this.startDteTextBox.Text = this.startDteTextBox.Text.Substring(0, 11) + " 00:00:00";
      }
    }

    private void endDteButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.selectDate(ref this.endDteTextBox);
      if (this.endDteTextBox.Text.Length > 11)
      {
        this.endDteTextBox.Text = this.endDteTextBox.Text.Substring(0, 11) + " 23:59:59";
      }
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void bdgtTmpDiag_Load(object sender, EventArgs e)
    {
      this.obey_events = false;
      System.Windows.Forms.Application.DoEvents();
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      this.docTypComboBox.SelectedIndex = 2;
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
      if (mytxt.Name == "startDteTextBox" || mytxt.Name == "endDteTextBox")
      {
        this.trnsDteLOVSrch(mytxt);
      }
      else if (mytxt.Name == "createdByTextBox")
      {
        this.createdByTextBox.Text = "";
        this.createdByIDTextBox.Text = "-1";
        this.createdByButton_Click(this.createdByButton, e);
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
      if (mytxt.Name == "startDteTextBox")
      {
        mytxt.Text = dte1.ToString("dd-MMM-yyyy HH:mm:ss");
      }
      else
      {
        mytxt.Text = dte1.ToString("dd-MMM-yyyy 23:59:59");
      }
    }

    private void createdByButton_Click(object sender, EventArgs e)
    {
      string[] selVals = new string[1];
      selVals[0] = this.createdByIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
       Global.mnFrm.cmCde.getLovID("Active Users"), ref selVals,
       false, false);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.createdByIDTextBox.Text = selVals[i];
          this.createdByTextBox.Text = Global.mnFrm.cmCde.getUsername(long.Parse(selVals[i]));
        }
      }
    }

    private void rptComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.rptComboBox.Text == "Money Received Report")
      {
        this.sortByComboBox.Items.Clear();
        this.sortByComboBox.Items.Add("None");
        this.sortByComboBox.Items.Add("TOTAL AMOUNT");
        this.sortByComboBox.Items.Add("OUTSTANDING AMOUNT");
        this.sortByComboBox.SelectedItem = "TOTAL AMOUNT";
      }
      else
      {
        this.sortByComboBox.Items.Clear();
        this.sortByComboBox.Items.Add("None");
        this.sortByComboBox.Items.Add("QTY");
        this.sortByComboBox.Items.Add("TOTAL AMOUNT");
        this.sortByComboBox.SelectedItem = "QTY";
      }
    }

    private void useCreationDateCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      if (this.useCreationDateCheckBox.Checked)
      {
        this.useCreationDateCheckBox.Text = "Use Creation Date to Search";
      }
      else
      {
        this.useCreationDateCheckBox.Text = "Use Document Date to Search";
      }
    }
  }
}