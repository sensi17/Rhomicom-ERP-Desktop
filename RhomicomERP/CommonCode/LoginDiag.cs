using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CommonCode
{
  public partial class loginDiag : Form
  {
    #region "GLOBAL DECLARATIONS..."
    public string login_result = "";
    public CommonCodes cmnCde;
    #endregion

    #region "FORM FUNCTIONS..."
    public loginDiag()
    {
      InitializeComponent();
    }
    #endregion

    #region "EVENT HANDLERS..."
    #region "VERIFICATION STATEMENTS..."
    //public  bool doesUserHaveThisRole(string username, string rolename)
    //{
    //  //Checks whether a given username 'admin' has a given user role
    //  DataSet dtSt = new DataSet();
    //  string sqlStr = "SELECT user_id FROM sec.sec_users_n_roles WHERE ((user_id = " +
    //          this.getUserID(username) + ") AND (role_id = " + cmnCde.getRoleID(rolename) +
    //          ") AND (now() between to_timestamp(valid_start_date,'YYYY-MM-DD HH24:MI:SS') AND " +
    //          "to_timestamp(valid_end_date,'YYYY-MM-DD HH24:MI:SS')))";
    //  dtSt = cmnCde.selectDataNoParams1(sqlStr);
    //  if (dtSt.Tables[0].Rows.Count > 0)
    //  {
    //    return true;
    //  }
    //  else
    //  {
    //    return false;
    //  }
    //}

    public  Int64 getUserID(string username)
    {
      //Example username 'admin'
      DataSet dtSt = new DataSet();
      string sqlStr = "select user_id from sec.sec_users where lower(user_name) = '" +
        username.Replace("'", "''").ToLower() + "'";
      dtSt = cmnCde.selectDataNoParams1(sqlStr);
      if (dtSt.Tables[0].Rows.Count > 0)
      {
        return Int64.Parse(dtSt.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return -1;
      }
    }

    public  Int64 getPersonID(string locidno)
    {
      //Example username 'admin'
      DataSet dtSt = new DataSet();
      string sqlStr = "select person_id from prs.prsn_names_nos where(local_id_no ilike '" +
        locidno.Replace("'", "''") + "')";
      dtSt = cmnCde.selectDataNoParams1(sqlStr);
      if (dtSt.Tables[0].Rows.Count > 0)
      {
        return Int64.Parse(dtSt.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return -1;
      }
    }

    public  bool isLoginInfoCorrct(string usrname, string pswd)
    {
      string sqlStr = "SELECT user_id FROM sec.sec_users WHERE ((lower(user_name) = '" + usrname.Replace("'", "''").ToLower() +
        "') AND (usr_password = md5('" + cmnCde.encrypt(pswd, CommonCodes.AppKey).Replace("'", "''") +
        "')) AND (now() between to_timestamp(valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
            "AND to_timestamp(valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS')))";
      DataSet dtSt = new DataSet();
      dtSt = cmnCde.selectDataNoParams1(sqlStr);
      if (dtSt.Tables[0].Rows.Count > 0)
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    public  bool isUserAccntLckd(string username)
    {
      //Checks Whether a user's account is locked
      /*
       * Criteria for checking whether a user's account is locked
       * 1. Check if failed  attempts is greater than or equal to the set number
       */
      string sqlStr = "SELECT failed_login_atmpts >= " + cmnCde.get_CurPlcy_Mx_Fld_lgns() +
      "  FROM sec.sec_users WHERE lower(user_name) = '" + username.Replace("'", "''").ToLower() + "'";
      DataSet dtSt = new DataSet();
      dtSt = cmnCde.selectDataNoParams1(sqlStr);
      if (dtSt.Tables[0].Rows.Count > 0)
      {
        if (bool.Parse(dtSt.Tables[0].Rows[0][0].ToString()) == true)
        {
          return true;
        }
        else
        {
          return false;
        }
      }
      else
      {
        return false;
      }
    }

    public  bool shdUnlckAccnt(string username)
    {
      /* 1. Check if the difference between last_login_time and now is
       *    greater than the set duration
        */
      string sqlStr = "SELECT age(now(), to_timestamp(last_login_atmpt_time, 'YYYY-MM-DD HH24:MI:SS')) " +
        ">= interval '" + cmnCde.get_CurPlcy_Auto_Unlck_tme() + " minute'" +
        " FROM sec.sec_users WHERE lower(user_name) = '" + username.Replace("'", "''").ToLower() + "'";
      DataSet dtSt = new DataSet();
      dtSt = cmnCde.selectDataNoParams1(sqlStr);
      if (dtSt.Tables[0].Rows.Count > 0)
      {
        if (bool.Parse(dtSt.Tables[0].Rows[0][0].ToString()) == true)
        {
          return true;
        }
        else
        {
          return false;
        }
      }
      else
      {
        return false;
      }
    }

    public  bool isAccntSuspended(string username)
    {
      //Checks Whether a user's account is suspended
      string sqlStr = "SELECT is_suspended FROM sec.sec_users WHERE lower(user_name) = '" + username.Replace("'", "''").ToLower() + "'";
      DataSet dtSt = new DataSet();
      dtSt = cmnCde.selectDataNoParams1(sqlStr);
      if (dtSt.Tables[0].Rows.Count > 0)
      {
        if (bool.Parse(dtSt.Tables[0].Rows[0][0].ToString()) == true)
        {
          return true;
        }
        else
        {
          return false;
        }
      }
      else
      {
        return false;
      }
    }

    public  bool isPswdTmp(string username)
    {
      //Checks Whether a user's password is temporary
      string sqlStr = "SELECT is_pswd_temp FROM sec.sec_users WHERE lower(user_name) = '" + username.Replace("'", "''").ToLower() + "'";
      DataSet dtSt = new DataSet();
      dtSt = cmnCde.selectDataNoParams1(sqlStr);
      if (dtSt.Tables[0].Rows.Count > 0)
      {
        if (bool.Parse(dtSt.Tables[0].Rows[0][0].ToString()) == true)
        {
          return true;
        }
        else
        {
          return false;
        }
      }
      else
      {
        return false;
      }
    }

    public  bool isPswdExpired(string username)
    {
      //Checks whether a user's password has expired
      /*For a password to expire
       * 1. the difference between the last pasword change time and now must be greater than
       *				the set number of expiry days
       * 2. 
       */
      string sqlStr = "SELECT age(now(), to_timestamp(last_pswd_chng_time, 'YYYY-MM-DD HH24:MI:SS')) " +
        ">= interval '" + cmnCde.get_CurPlcy_Pwd_Exp_Days() + " days'" +
        " FROM sec.sec_users WHERE lower(user_name) = '" + username.Replace("'", "''").ToLower() + "'";
      DataSet dtSt = new DataSet();
      dtSt = cmnCde.selectDataNoParams1(sqlStr);
      if (dtSt.Tables[0].Rows.Count > 0)
      {
        if (bool.Parse(dtSt.Tables[0].Rows[0][0].ToString()) == true)
        {
          return true;
        }
        else
        {
          return false;
        }
      }
      else
      {
        return false;
      }
    }

    public  bool isPswdInRcntHstry(string pswd, Int64 usrid)
    {
      // Checks whether the new password is in the past disllowed number of passwords
      string sqlStr = "SELECT a.old_pswd_id FROM " +
        "(SELECT old_pswd_id, old_password FROM sec.sec_users_old_pswds WHERE(user_id = " +
        usrid + ") ORDER BY old_pswd_id DESC limit " + cmnCde.get_CurPlcy_DsllwdPswdCnt() +
        ") a WHERE(a.old_password = md5('" + cmnCde.encrypt(pswd, CommonCodes.AppKey).Replace("'", "''") + "'))";
      DataSet dtSt = new DataSet();
      dtSt = cmnCde.selectDataNoParams1(sqlStr);
      if (dtSt.Tables[0].Rows.Count > 0)
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    #endregion

    private void okButton_Click(object sender, EventArgs e)
    {
      if (this.unameTextBox.Text == "" || this.pwdTextBox.Text == "")
      {
        cmnCde.showMsg("Please fill all required fields!", 0);
        return;
      }
      if (this.getUserID(this.unameTextBox.Text) != cmnCde.User_id)
      {
        this.cmnCde.showMsg("Please login as the Original User!", 0);
        return;
      }
      this.checkB4LgnRequireMents();
      if (this.getUserID(this.unameTextBox.Text) <= 0)
      {
        cmnCde.showMsg("Invalid Username or Password!", 0);
        return;
      }
      if (this.isAccntSuspended(this.unameTextBox.Text) == true)
      {
        cmnCde.showMsg("This account has been suspended!\nContact your System Administrator!", 0);
        return;
      }
      if (this.isUserAccntLckd(this.unameTextBox.Text) == true &&
        this.shdUnlckAccnt(this.unameTextBox.Text) == false)
      {
        cmnCde.showMsg("Your account has been Locked!\nContact your System Administrator!", 0);
        return;
      }
      if (this.isLoginInfoCorrct(this.unameTextBox.Text, this.pwdTextBox.Text))
      {
        CommonCodes.LastActvDteTme = this.cmnCde.getDB_Date_time();
        //Update successful logins table
        //this.recordSuccflLogin(this.unameTextBox.Text);
        //this.login_result = this.checkAftrSccsflLgnRequirmnts();
        this.DialogResult = DialogResult.OK;
        this.Close();
      }
      else
      {
        //Update failed logins table
        //this.recordFailedLogin(this.unameTextBox.Text);
        cmnCde.showMsg("Invalid Username or Password!", 0);
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
 
    }

    public void unlockUsrAccnt(string username)
    {
      //Set failed_login_atmpts in sec.sec_users to 0
      string sqlStr = "UPDATE sec.sec_users SET failed_login_atmpts = 0 WHERE (lower(user_name) = '" + username.Replace("'", "''").ToLower() + "')";
      cmnCde.updateDataNoParams1(sqlStr);
    }

    public void unlockUsrAccntConditnl(string username)
    {
      //Set failed_login_atmpts in sec.sec_users to 0
      string sqlStr = "UPDATE sec.sec_users SET failed_login_atmpts = 0 WHERE ((lower(user_name) = '" +
        username.Replace("'", "''").ToLower() + "') AND (failed_login_atmpts <> 0))";
      cmnCde.updateDataNoParams1(sqlStr);
    }

    //private string checkAftrSccsflLgnRequirmnts()
    //{
    //  /* Returns select role or logout or change password
    //   * 1. Check if the pswd is expired then take user to change pswd diag
    //   * 2. Check if account is suspended then logout user and display message
    //   * 3. Check if password is temporary then take user to change pswd diag
    //   * 4. Check if account is locked
    //   * 5. if shldUnlock account is true then unlock account
    //   */
     
    //  if (this.isAccntSuspended(this.unameTextBox.Text) == true)
    //  {
    //    cmnCde.showMsg("This account has been suspended!\nContact your System Administrator!", 0);
    //    return "logout";
    //  }
    //  if (this.isUserAccntLckd(this.unameTextBox.Text) == true)
    //  {
    //    this.unlockUsrAccnt(this.unameTextBox.Text);
    //  }
    //  else
    //  {
    //    this.unlockUsrAccntConditnl(this.unameTextBox.Text);
    //  }
    //  if (this.isPswdTmp(this.unameTextBox.Text))
    //  {
    //    cmnCde.showMsg("Your are using a Temporary Password!\nPlease change your password now!", 0);
    //    return "change password";
    //  }
    //  if (this.isPswdExpired(this.unameTextBox.Text))
    //  {
    //    cmnCde.showMsg("Your Password has Expired!\nPlease change your Password now!", 0);
    //    return "change password";
    //  }
    //  if (cmnCde.doesPswdCmplxtyMeetPlcy(this.pwdTextBox.Text, this.unameTextBox.Text) == false)
    //  {
    //    cmnCde.showMsg("Your password's complexity does not meet\nthe " +
    //      "current password policy requirements!\nPlease change " +
    //      "your password!", 0);
    //    return "change password";
    //  }
    //  return "select role";
    //}
    #endregion

    private void loginDiag_Load(object sender, EventArgs e)
    {
      System.Windows.Forms.Application.DoEvents();
      Color[] clrs = cmnCde.getColors();
      this.BackColor = clrs[0];
    }

    private void unameTextBox_Click(object sender, EventArgs e)
    {
      this.unameTextBox.SelectAll();
    }

    private void pwdTextBox_Click(object sender, EventArgs e)
    {
      this.pwdTextBox.SelectAll();
    }
  }
}