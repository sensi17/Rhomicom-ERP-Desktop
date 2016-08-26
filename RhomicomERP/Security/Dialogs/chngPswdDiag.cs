using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SystemAdministration.Classes;

namespace SystemAdministration.Dialogs
{
  public partial class chngPswdDiag : Form
  {
    public chngPswdDiag()
    {
      InitializeComponent();
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      if (this.unameTextBox.Text == "" || this.nwPwdTextBox.Text == ""
        || this.cnfmNwTextBox.Text == "")
      {
        MessageBox.Show("Please fill all required fields!", "Rhomicom Message!",
          MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
      }
      if (this.nwPwdTextBox.Text != this.cnfmNwTextBox.Text)
      {
        MessageBox.Show("New Passwords don't Match!", "Rhomicom Message!",
  MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
      }
      if (Global.myNwMainFrm.cmmnCode.doesPswdCmplxtyMeetPlcy(this.nwPwdTextBox.Text, this.unameTextBox.Text) == false)
      {
        MessageBox.Show("Your new password's complexity does not meet\nthe " +
          "current password policy requirements!\nPlease contact your " +
          "system administrator for assistance!", "Rhomicom Message!",
  MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

    private void chngPswdDiag_Load(object sender, EventArgs e)
    {
      System.Windows.Forms.Application.DoEvents();
      Color[] clrs = Global.myNwMainFrm.cmmnCode.getColors();
      this.BackColor = clrs[0];
    }

    private void nwPwdTextBox_Click(object sender, EventArgs e)
    {
      this.nwPwdTextBox.SelectAll();
    }

    private void cnfmNwTextBox_Click(object sender, EventArgs e)
    {
      this.cnfmNwTextBox.SelectAll();
    }
  }
}