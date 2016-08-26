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
  public partial class bdgtTmpDiag : Form
  {
    public bdgtTmpDiag()
    {
      InitializeComponent();
    }
    bool txtChngd = false;
    public bool obey_events = false;

    private void OKButton_Click(object sender, EventArgs e)
    {
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
      if (this.prdTypComboBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please select a Period Type!", 0);
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
      this.trnsDteLOVSrch(mytxt);
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
      mytxt.Text = dte1.ToString("dd-MMM-yyyy HH:mm:ss");
    }
  }
}