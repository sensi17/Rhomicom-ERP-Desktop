using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Enterprise_Management_System.Classes;

namespace Enterprise_Management_System.Dialogs
	{
	public partial class chngPswdDiag : Form
		{
		#region "CONSTRUCTOR..."
		public chngPswdDiag()
			{
			InitializeComponent();
			}
		#endregion

		#region "EVENT HANDLERS..."
		private void chngPswdDiag_Load(object sender, EventArgs e)
			{
        System.Windows.Forms.Application.DoEvents();
        Color[] clrs = Global.myNwMainFrm.cmnCdMn.getColors();
        this.BackColor = clrs[0];
        this.unameTextBox.Text = Global.myNwMainFrm.cmnCdMn.get_user_name(Global.usr_id);
			}

		private void okButton_Click(object sender, EventArgs e)
			{
			if (this.pwdTextBox.Text == "" || this.nwPwdTextBox.Text == ""
				|| this.cnfmNwTextBox.Text == "")
				{
				Global.myNwMainFrm.cmnCdMn.showMsg("Please fill all required fields!", 0);
				return;
				}
			if (this.nwPwdTextBox.Text != this.cnfmNwTextBox.Text)
				{
				Global.myNwMainFrm.cmnCdMn.showMsg("New Passwords don't Match!", 0);
				return;
				}
			if (Global.isLoginInfoCorrct(this.unameTextBox.Text, this.pwdTextBox.Text) == false)
				{
				Global.myNwMainFrm.cmnCdMn.showMsg("Old password is Invalid!", 0);
				return;
				}
			if (Global.isPswdInRcntHstry(this.nwPwdTextBox.Text, Global.usr_id) == true)
				{
				Global.myNwMainFrm.cmnCdMn.showMsg("The new password is in your last " + Global.myNwMainFrm.cmnCdMn.get_CurPlcy_DsllwdPswdCnt() +
					" password history!\nPlease provide a different password!", 0);
				return;
				}
			if (this.pwdTextBox.Text == this.nwPwdTextBox.Text)
				{
				Global.myNwMainFrm.cmnCdMn.showMsg("New Password is same as your Old Password!", 0);
				return;
				}
			if (Global.myNwMainFrm.cmnCdMn.doesPswdCmplxtyMeetPlcy(this.nwPwdTextBox.Text, this.unameTextBox.Text) == true)
				{
				Global.storeOldPassword(Global.usr_id, this.pwdTextBox.Text);
				Global.changeUserPswd(Global.usr_id, this.nwPwdTextBox.Text);
				this.DialogResult = DialogResult.OK;
				this.Close();
				}
			else
				{
				Global.myNwMainFrm.cmnCdMn.showMsg("Your new password's complexity does not meet\nthe " + 
					"current password policy requirements!\nPlease contact your " + 
					"system administrator for assistance!", 0);
				return;
				}
			}

		private void cancelButton_Click(object sender, EventArgs e)
			{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
			}
		#endregion

    private void pwdTextBox_Click(object sender, EventArgs e)
    {
      this.pwdTextBox.SelectAll();
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