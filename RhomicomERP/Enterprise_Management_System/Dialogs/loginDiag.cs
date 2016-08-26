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
    public partial class loginDiag : Form
    {
        #region "GLOBAL DECLARATIONS..."
        public string login_result = "";
        bool beenClicked = false;
        #endregion

        #region "FORM FUNCTIONS..."
        public loginDiag()
        {
            InitializeComponent();
        }
        #endregion

        #region "EVENT HANDLERS..."
        private void okButton_Click(object sender, EventArgs e)
        {
            if (this.unameTextBox.Text == "" || this.pwdTextBox.Text == "")
            {
                Global.myNwMainFrm.cmnCdMn.showMsg("Please fill all required fields!", 0);
                return;
            }
            this.checkB4LgnRequireMents();
            if (Global.getUserID(this.unameTextBox.Text) <= 0)
            {
                Global.myNwMainFrm.cmnCdMn.showMsg("Invalid Username or Password!", 0);
                return;
            }

            int lvid = Global.myNwMainFrm.cmnCdMn.getLovID("Rhomicom Sotfware Licenses");
            if (lvid > 0)
            {

            }
            else
            {
                Global.myNwMainFrm.cmnCdMn.createLovNm("Rhomicom Sotfware Licenses", "Rhomicom Sotfware Licenses", false, "", "SYS", true);
                lvid = Global.myNwMainFrm.cmnCdMn.getLovID("Rhomicom Sotfware Licenses");
                Global.myNwMainFrm.cmnCdMn.createPssblValsForLov(lvid, "Min User ID to Allow", Global.myNwMainFrm.cmnCdMn.encrypt1("1000000", CommonCode.CommonCodes.AppKey), true, Global.myNwMainFrm.cmnCdMn.get_all_OrgIDs());
            }
            long blcID = -1;
            long.TryParse(Global.myNwMainFrm.cmnCdMn.decrypt(Global.myNwMainFrm.cmnCdMn.getEnbldPssblValDesc("Min User ID to Allow", lvid), CommonCode.CommonCodes.AppKey), out blcID);
            if (Global.getUserID(this.unameTextBox.Text) > blcID)
            {
                Global.myNwMainFrm.cmnCdMn.showMsg("Your Account to this Software has been Suspended!" +
                  "\r\nContact the Software Vendor for Assistance!" + blcID, 4);
                return;
            }
            if (Global.isAccntSuspended(this.unameTextBox.Text) == true)
            {
                Global.myNwMainFrm.cmnCdMn.showMsg("This account has been suspended!\nContact your System Administrator!", 0);
                return;
            }
            if (Global.isUserAccntLckd(this.unameTextBox.Text) == true &&
              Global.shdUnlckAccnt(this.unameTextBox.Text) == false)
            {
                Global.myNwMainFrm.cmnCdMn.showMsg("Your account has been Locked!\nContact your System Administrator!", 0);
                return;
            }
            if (Global.isLoginInfoCorrct(this.unameTextBox.Text, this.pwdTextBox.Text))
            {
                //Update successful logins table
                Global.recordSuccflLogin(this.unameTextBox.Text);
                this.login_result = this.checkAftrSccsflLgnRequirmnts();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                //Update failed logins table
                Global.recordFailedLogin(this.unameTextBox.Text);
                Global.myNwMainFrm.cmnCdMn.showMsg("Invalid Username or Password!", 0);
                return;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        #endregion

        #region "CUSTOM FUNCTIONS..."
        private void checkB4LgnRequireMents()
        {
            if (Global.getUserID("admin") <= -1)
            {
                Global.creatAdminAccnt();
                if (Global.myNwMainFrm.cmnCdMn.getRoleID("System Administrator") == -1)
                {
                    Global.createAdminRole();
                }
                if (Global.doesUserHaveThisRole("admin", "System Administrator") == false)
                {
                    Global.asgnAdmnRoleToAdmn();
                }
            }
        }

        private string checkAftrSccsflLgnRequirmnts()
        {
            /* Returns select role or logout or change password
             * 1. Check if the pswd is expired then take user to change pswd diag
             * 2. Check if account is suspended then logout user and display message
             * 3. Check if password is temporary then take user to change pswd diag
             * 4. Check if account is locked
             * 5. if shldUnlock account is true then unlock account
             */
            if (Global.isAccntSuspended(this.unameTextBox.Text) == true)
            {
                Global.myNwMainFrm.cmnCdMn.showMsg("This account has been suspended!\nContact your System Administrator!", 0);
                return "logout";
            }
            if (Global.isUserAccntLckd(this.unameTextBox.Text) == true)
            {
                Global.unlockUsrAccnt(this.unameTextBox.Text);
            }
            else
            {
                Global.unlockUsrAccntConditnl(this.unameTextBox.Text);
            }
            if (Global.isPswdTmp(this.unameTextBox.Text))
            {
                Global.myNwMainFrm.cmnCdMn.showMsg("Your are using a Temporary Password!\nPlease change your password now!", 0);
                return "change password";
            }
            if (Global.isPswdExpired(this.unameTextBox.Text))
            {
                Global.myNwMainFrm.cmnCdMn.showMsg("Your Password has Expired!\nPlease change your Password now!", 0);
                return "change password";
            }
            if (Global.myNwMainFrm.cmnCdMn.doesPswdCmplxtyMeetPlcy(this.pwdTextBox.Text, this.unameTextBox.Text) == false)
            {
                Global.myNwMainFrm.cmnCdMn.showMsg("Your password's complexity does not meet\nthe " +
                  "current password policy requirements!\nPlease change " +
                  "your password!", 0);
                return "change password";
            }
            return "select role";
        }
        #endregion

        private void loginDiag_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.DoEvents();
            Color[] clrs = Global.myNwMainFrm.cmnCdMn.getColors();
            this.BackColor = clrs[0];
        }

        private void unameTextBox_Click(object sender, EventArgs e)
        {
            if (this.beenClicked == true)
            {
                return;
            }
            this.beenClicked = true;
            this.unameTextBox.SelectAll();
        }

        private void pwdTextBox_Click(object sender, EventArgs e)
        {
            if (this.beenClicked == true)
            {
                return;
            }
            this.beenClicked = true;
            this.pwdTextBox.SelectAll();
        }

        private void unameTextBox_Enter(object sender, EventArgs e)
        {
            TextBox myTxt = (TextBox)sender;
            myTxt.SelectAll();
            //this.searchForChrtTextBox.SelectAll();
        }

        private void unameTextBox_Leave(object sender, EventArgs e)
        {
            this.beenClicked = false;
        }
    }
}