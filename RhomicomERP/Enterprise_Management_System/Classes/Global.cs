using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Npgsql;
using cadmaFunctions;
using Microsoft.VisualBasic.Devices;
using System.Net.Mail;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace Enterprise_Management_System.Classes
{
  /// <summary>
  /// A  class containing variables and 
  /// functions we will like to call directly from 
  /// anywhere in the project without creating an instance first
  /// </summary>
  class Global
  {
    #region "CONSTRUCTOR..."
    public Global() { }
    #endregion

    #region "GLOBAL DECLARATION..."
    public static Enterprise_Management_System.Forms.mainForm myNwMainFrm = null;
    public static Enterprise_Management_System.Forms.homePageForm homeFrm = null;

    public static Int64 usr_id = (-1);//Current User's ID
    public static int[] role_set_id = new int[0];//Current Active Role Set IDs
    public static int org_id = -1;
    public static Int64 login_number = (-1);//Current Users Login Number
    public static string login_result = "";
    public static Enterprise_Management_System.Classes.Types.AvailableModule currentPlugin = null;

    public static string db_server = "";//Current database Server IP/Name
    public static string db_name = "";//Current Database Name
    public static Enterprise_Management_System.Classes.rhoModuleFuncs moduleFuncs = new Enterprise_Management_System.Classes.rhoModuleFuncs();
    #endregion

    #region "DATE/TIME SECURITY FUNCTIONS..."

    #region "INSERT STATEMENTS..."
    public static void creatAdminAccnt()
    {
      string dateStr = Global.myNwMainFrm.cmnCdMn.getDB_Date_time();
      //string dropConstraint = "ALTER TABLE sec.sec_users DROP CONSTRAINT fk_person_id";
      //Global.myNwMainFrm.cmnCdMn.executeGnrlSQL(dropConstraint);
      //dropConstraint = "ALTER TABLE sec.sec_users DROP CONSTRAINT fk_created_by";
      //Global.myNwMainFrm.cmnCdMn.executeGnrlSQL(dropConstraint);
      //dropConstraint = "ALTER TABLE sec.sec_users DROP CONSTRAINT fk_last_update_by";
      //Global.myNwMainFrm.cmnCdMn.executeGnrlSQL(dropConstraint);

      string sqlStr = "INSERT INTO sec.sec_users(usr_password, person_id, is_suspended, is_pswd_temp, " +
                        "failed_login_atmpts, user_name, last_login_atmpt_time, last_pswd_chng_time, " +
                        "valid_start_date, valid_end_date, created_by, creation_date, last_update_by, last_update_date) " +
        "VALUES (md5('" + Global.myNwMainFrm.cmnCdMn.encrypt("admin", CommonCode.CommonCodes.AppKey).Replace("'", "''") + "'), -1, FALSE, FALSE, 0, 'admin', '" +
        dateStr + "', '" + dateStr + "', '" + dateStr + "', '4000-12-31 00:00:00', -1, '" +
        dateStr + "',-1, '" + dateStr + "');";
      Global.myNwMainFrm.cmnCdMn.insertDataNoParams(sqlStr);

      long uID = Global.getUserID("admin");
      //string createUnkwnPrsn = "INSERT INTO prs.prsn_loc_id_nos(" +
      //         "local_id_no_prfx, created_by, creation_date, last_update_by, " +
      //         "last_update_date, local_id_no_sffx, local_id_no_counter)" +
      // "VALUES ('SETUP', " + uID + ", '" + dateStr + "', " + uID + ", '" + dateStr + "', 'USER', '0001')";
      //Global.myNwMainFrm.cmnCdMn.insertDataNoParams(createUnkwnPrsn);
      long pID = Global.getPersonID("RHO0002012");

      if (pID <= 0)
      {
        string createUnkwnPrsn1 = "INSERT INTO prs.prsn_names_nos(" +
                          "local_id_no, first_name, sur_name, other_names, title, " +
              "created_by, creation_date, last_update_by, last_update_date)" +
      "VALUES ('RHO0002012', 'RHOMICOM', 'SETUP', 'USER', 'Mr.', " + uID + ", '" + dateStr + "', " + uID + ", '" + dateStr + "')";
        Global.myNwMainFrm.cmnCdMn.insertDataNoParams(createUnkwnPrsn1);
        pID = Global.getPersonID("RHO0002012");
      }

      //Update userID
      string updtUsr = "UPDATE sec.sec_users SET " +
               "person_id = " + pID + ", created_by = " + uID + ", last_update_by = " + uID +
               " WHERE (user_id = " + uID + ")";
      Global.myNwMainFrm.cmnCdMn.updateDataNoParams(updtUsr);

      //string createConstraint = "ALTER TABLE sec.sec_users ADD CONSTRAINT fk_person_id FOREIGN KEY (person_id) " +
      //   "REFERENCES prs.prsn_names_nos (person_id) MATCH SIMPLE " +
      //   "ON UPDATE RESTRICT ON DELETE RESTRICT";
      //Global.myNwMainFrm.cmnCdMn.executeGnrlSQL(createConstraint);

      //createConstraint = "ALTER TABLE sec.sec_users ADD CONSTRAINT fk_created_by FOREIGN KEY (created_by) " +
      //   "REFERENCES sec.sec_users (user_id) MATCH SIMPLE " +
      //   "ON UPDATE RESTRICT ON DELETE RESTRICT";
      //Global.myNwMainFrm.cmnCdMn.executeGnrlSQL(createConstraint);

      //createConstraint = "ALTER TABLE sec.sec_users ADD CONSTRAINT fk_last_update_by FOREIGN KEY (last_update_by) " +
      //   "REFERENCES sec.sec_users (user_id) MATCH SIMPLE " +
      //   "ON UPDATE RESTRICT ON DELETE RESTRICT";
      //Global.myNwMainFrm.cmnCdMn.executeGnrlSQL(createConstraint);
    }

    public static void createAdminRole()
    {
      long uID = Global.getUserID("admin");
      string dateStr = Global.myNwMainFrm.cmnCdMn.getDB_Date_time();
      string sqlStr = "INSERT INTO sec.sec_roles(role_name, valid_start_date, valid_end_date, created_by, " +
                        "creation_date, last_update_by, last_update_date) VALUES ('System Administrator', '" +
            dateStr + "', '4000-12-31 00:00:00', " + uID + ", '" + dateStr + "', " + uID + ", '" + dateStr + "')";
      Global.myNwMainFrm.cmnCdMn.insertDataNoParams(sqlStr);
    }

    public static void asgnAdmnRoleToAdmn()
    {
      //Assigns the System Administrator responsibility to the Admin Account
      long uID = Global.getUserID("admin");
      string dateStr = Global.myNwMainFrm.cmnCdMn.getDB_Date_time();
      string sqlStr = "INSERT INTO sec.sec_users_n_roles (user_id, role_id, valid_start_date, valid_end_date, created_by, " +
                        "creation_date, last_update_by, last_update_date) VALUES (" + Global.getUserID("admin") + ", " +
            Global.myNwMainFrm.cmnCdMn.getRoleID("System Administrator") + ", '" + dateStr + "', '4000-12-31 00:00:00', " + uID + ", '" + dateStr + "', " + uID + ", '" + dateStr + "')";
      Global.myNwMainFrm.cmnCdMn.insertDataNoParams(sqlStr);
    }

    public static void asgnScrtyPrvlgToAdmnRole(int prvldg_id)
    {

    }

    public static void asgnPrvlgToRole(int prvldg_id, int role_id)
    {

    }

    public static void recordSuccflLogin(string username)
    {
      string dateStr = Global.myNwMainFrm.cmnCdMn.getDB_Date_time();
      string[] mach_details = Global.myNwMainFrm.cmnCdMn.getMachDetails();
      string sqlStr = "INSERT INTO sec.sec_track_user_logins(user_id, login_time, logout_time, host_mach_details, was_lgn_atmpt_succsful, app_vrsn) " +
        "VALUES (" + Global.getUserID(username) + ", '" + dateStr + "', '', '" +
        mach_details[0].Replace("'", "''") + "/" + mach_details[1].Replace("'", "''") +
        "/" + mach_details[2].Replace("'", "''") + "', TRUE,'" + CommonCode.CommonCodes.AppVrsn + "')";
      Global.myNwMainFrm.cmnCdMn.insertDataNoParams(sqlStr);
      Global.updtLastLgnAttmpTme(username, dateStr);
      Global.usr_id = Global.getUserID(username);
      Global.login_number = Global.get_login_number(username, dateStr, mach_details);
      //Global.myNwMainFrm.cmnCdMn.LastActvtyTime = Global.myNwMainFrm.cmnCdMn.getDB_Date_time();
    }

    public static void createPlentyLogins()
    {
      for (int i = 0; i < 20000000; i++)
      {
        if (i % 2 == 0)
        {
          Global.recordSuccflLogin("admin");
        }
        else
        {
          Global.recordFailedLogin("John");
        }
        //System.Windows.Forms.Application.DoEvents();
      }
    }

    public static void recordFailedLogin(string username)
    {
      string dateStr = Global.myNwMainFrm.cmnCdMn.getDB_Date_time();
      string[] mach_details = Global.myNwMainFrm.cmnCdMn.getMachDetails();
      string sqlStr = "INSERT INTO sec.sec_track_user_logins(user_id, login_time, logout_time, host_mach_details, was_lgn_atmpt_succsful, app_vrsn) " +
        "VALUES (" + Global.getUserID(username) + ", '" + dateStr + "', '', '" +
        mach_details[0].Replace("'", "''") + "/" + mach_details[1].Replace("'", "''") +
        "/" + mach_details[2].Replace("'", "''") + "', FALSE,'" + CommonCode.CommonCodes.AppVrsn + "')";
      Global.myNwMainFrm.cmnCdMn.insertDataNoParams(sqlStr);
      Global.updtFailedLgnCnt(username);
      Global.updtLastLgnAttmpTme(username, dateStr);
    }

    public static void storeOldPassword(Int64 usrid, string pswd)
    {
      string dateStr = Global.myNwMainFrm.cmnCdMn.getDB_Date_time();
      string sqlStr = "INSERT INTO sec.sec_users_old_pswds(user_id, old_password, date_added) " +
    "VALUES (" + usrid + ", md5('" + Global.myNwMainFrm.cmnCdMn.encrypt(pswd, CommonCode.CommonCodes.AppKey).Replace("'", "''") +
        "'), '" + dateStr + "')";
      Global.myNwMainFrm.cmnCdMn.insertDataNoParams(sqlStr);
    }
    #endregion

    #region "UPDATE STATEMENTS..."
    public static void unlockUsrAccnt(string username)
    {
      //Set failed_login_atmpts in sec.sec_users to 0
      string sqlStr = "UPDATE sec.sec_users SET failed_login_atmpts = 0 WHERE (lower(user_name) = '" + username.Replace("'", "''").ToLower() + "')";
      Global.myNwMainFrm.cmnCdMn.updateDataNoParams(sqlStr);
    }

    public static void unlockUsrAccntConditnl(string username)
    {
      //Set failed_login_atmpts in sec.sec_users to 0
      string sqlStr = "UPDATE sec.sec_users SET failed_login_atmpts = 0 WHERE ((lower(user_name) = '" +
        username.Replace("'", "''").ToLower() + "') AND (failed_login_atmpts <> 0))";
      Global.myNwMainFrm.cmnCdMn.updateDataNoParams(sqlStr);
    }

    public static void unsuspendAccnt(string username)
    {
      //Unsuspends a user's account
      string dateStr = Global.myNwMainFrm.cmnCdMn.getDB_Date_time();
      string sqlStr = "UPDATE sec.sec_users SET is_suspended = FALSE, last_update_by = " +
        Global.usr_id + ", last_update_date = '" + dateStr + "' WHERE (lower(user_name) = '" + username.Replace("'", "''").ToLower() + "')";
      Global.myNwMainFrm.cmnCdMn.updateDataNoParams(sqlStr);
    }

    public static void updtFailedLgnCnt(string username)
    {
      string sqlStr = "UPDATE sec.sec_users SET failed_login_atmpts = failed_login_atmpts + 1 WHERE (lower(user_name) = '" + username.Replace("'", "''").ToLower() + "')";
      Global.myNwMainFrm.cmnCdMn.updateDataNoParams(sqlStr);
    }

    public static void updtLastLgnAttmpTme(string username, string lgn_time)
    {
      string sqlStr = "UPDATE sec.sec_users SET last_login_atmpt_time = '" + lgn_time +
        "' WHERE (lower(user_name) = '" + username.Replace("'", "''").ToLower() + "')";
      Global.myNwMainFrm.cmnCdMn.updateDataNoParams(sqlStr);
    }

    public static void storeLogoutTime(Int64 lgn_num)
    {
      string dateStr = Global.myNwMainFrm.cmnCdMn.getDB_Date_time();
      string sqlStr = "UPDATE sec.sec_track_user_logins SET logout_time = '" + dateStr +
  "', app_vrsn='" + CommonCode.CommonCodes.AppVrsn + "' WHERE (login_number = '" + lgn_num.ToString() + "')";
      Global.myNwMainFrm.cmnCdMn.updateDataNoParams(sqlStr);
    }

    public static void changeUserPswd(Int64 usrid, string pswd)
    {
      string dateStr = Global.myNwMainFrm.cmnCdMn.getDB_Date_time();
      string sqlStr = "UPDATE sec.sec_users SET usr_password = md5('" + Global.myNwMainFrm.cmnCdMn.encrypt(pswd, CommonCode.CommonCodes.AppKey).Replace("'", "''") +
      "'), last_pswd_chng_time = '" + dateStr + "', is_pswd_temp = FALSE, last_update_by = " +
        Global.usr_id + ", last_update_date = '" + dateStr + "' WHERE (user_id = '" + usrid.ToString() + "')";
      Global.myNwMainFrm.cmnCdMn.updateDataNoParams(sqlStr);
    }
    #endregion

    #region "DELETE STATEMENTS..."
    #endregion

    #region "SELECT STATEMENTS..."
    public static string get_last_login_time(string username)
    {
      //Gets the last login attempt time
      string sqlStr = "SELECT last_login_atmpt_time FROM " +
  "sec.sec_users WHERE lower(user_name) = '" + username.Replace("'", "''").ToLower() + "'";
      DataSet dtSt = new DataSet();
      dtSt = Global.myNwMainFrm.cmnCdMn.selectDataNoParams(sqlStr);
      if (dtSt.Tables[0].Rows.Count > 0)
      {
        return dtSt.Tables[0].Rows[0][0].ToString();
      }
      else
      {
        return Global.myNwMainFrm.cmnCdMn.getDB_Date_time();
      }
    }

    public static string get_last_pswd_time(string username)
    {
      //Gets the last password change date 
      string sqlStr = "SELECT last_pswd_chng_time FROM " +
      "sec.sec_users WHERE lower(user_name) = '" + username.Replace("'", "''").ToLower() + "'";
      DataSet dtSt = new DataSet();
      dtSt = Global.myNwMainFrm.cmnCdMn.selectDataNoParams(sqlStr);
      if (dtSt.Tables[0].Rows.Count > 0)
      {
        return dtSt.Tables[0].Rows[0][0].ToString();
      }
      else
      {
        return Global.myNwMainFrm.cmnCdMn.getDB_Date_time();
      }
    }

    public static Int64 get_login_number(string username, string login_time,
      string[] mach_details)
    {
      //Gets the last login attempt time
      string sqlStr = "SELECT login_number FROM sec.sec_track_user_logins WHERE ((user_id = " +
        Global.getUserID(username) + ") AND (login_time = '" + login_time + "') AND (host_mach_details = '" +
        mach_details[0].Replace("'", "''") + "/" + mach_details[1].Replace("'", "''") +
        "/" + mach_details[2].Replace("'", "''") + "'))";
      DataSet dtSt = new DataSet();
      dtSt = Global.myNwMainFrm.cmnCdMn.selectDataNoParams(sqlStr);
      if (dtSt.Tables[0].Rows.Count > 0)
      {
        return Int64.Parse(dtSt.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return -1;
      }
    }

    public static DataSet get_Users_Roles(string searchFor, string searchIn, long offset, long limit_size)
    {
      //Gets the Roles a user has selected 
      string sqlStr = "SELECT a.role_id, b.role_name " +
"FROM sec.sec_users_n_roles a LEFT OUTER JOIN sec.sec_roles b ON (a.role_id = b.role_id) WHERE ((now() between to_timestamp(a.valid_start_date,'YYYY-MM-DD HH24:MI:SS') AND " +
"to_timestamp(a.valid_end_date,'YYYY-MM-DD HH24:MI:SS')) AND (now() between to_timestamp(b.valid_start_date,'YYYY-MM-DD HH24:MI:SS') AND " +
"to_timestamp(b.valid_end_date,'YYYY-MM-DD HH24:MI:SS')) AND (a.user_id = " + Global.usr_id +
") AND (b.role_name ilike '" + searchFor.Replace("'", "''") + "')) ORDER BY a.role_id LIMIT " + limit_size + " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
      DataSet dtSt = new DataSet();
      dtSt = Global.myNwMainFrm.cmnCdMn.selectDataNoParams(sqlStr);
      return dtSt;
    }

    public static DataSet get_AllUsers_Roles()
    {
      //Gets the Roles a user has selected 
      string sqlStr = "SELECT a.role_id, b.role_name " +
  "FROM sec.sec_users_n_roles a LEFT OUTER JOIN sec.sec_roles b ON (a.role_id = b.role_id) WHERE ((now() between to_timestamp(a.valid_start_date,'YYYY-MM-DD HH24:MI:SS') AND " +
  "to_timestamp(a.valid_end_date,'YYYY-MM-DD HH24:MI:SS')) AND (now() between to_timestamp(b.valid_start_date,'YYYY-MM-DD HH24:MI:SS') AND " +
  "to_timestamp(b.valid_end_date,'YYYY-MM-DD HH24:MI:SS')) AND (a.user_id = " + Global.usr_id +
  ")) ORDER BY a.role_id";
      DataSet dtSt = new DataSet();
      dtSt = Global.myNwMainFrm.cmnCdMn.selectDataNoParams(sqlStr);
      return dtSt;
    }

    public static DataSet get_AvlbMdls()
    {
      //Gets the Roles a user has selected 
      string sqlStr = "select distinct a.module_name from " +
"sec.sec_modules a, sec.sec_prvldgs b, sec.sec_roles_n_prvldgs c, sec.sec_roles d " +
"where a.module_id = b.module_id and b.prvldg_id = c.prvldg_id and c.role_id = d.role_id and d.role_id IN (" +
Global.concatCurRoleIDs() + ") ORDER BY a.module_name";
      DataSet dtSt = new DataSet();
      dtSt = Global.myNwMainFrm.cmnCdMn.selectDataNoParams(sqlStr);
      Global.homeFrm.sqlStr = sqlStr;
      return dtSt;
    }

    public static long get_Totl_Users_Roles(string searchFor, string searchIn)
    {
      //Gets the Roles a user has selected 
      string sqlStr = "SELECT count(a.role_id) " +
   @"FROM sec.sec_users_n_roles a LEFT OUTER JOIN sec.sec_roles b ON (a.role_id = b.role_id) 
    WHERE ((now() between to_timestamp(a.valid_start_date,'YYYY-MM-DD HH24:MI:SS') AND " +
   "to_timestamp(a.valid_end_date,'YYYY-MM-DD HH24:MI:SS')) AND (now() between to_timestamp(b.valid_start_date,'YYYY-MM-DD HH24:MI:SS') AND " +
   "to_timestamp(b.valid_end_date,'YYYY-MM-DD HH24:MI:SS')) AND (a.user_id = " + Global.usr_id +
   ") AND (b.role_name ilike '" + searchFor.Replace("'", "''") + "'))";
      DataSet dtSt = new DataSet();
      dtSt = Global.myNwMainFrm.cmnCdMn.selectDataNoParams(sqlStr);
      if (dtSt.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtSt.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return 0;
      }
    }
    #endregion

    #region "VERIFICATION STATEMENTS..."
    public static string concatCurRoleIDs()
    {
      string nwStr = "-1000000";
      int totl = Global.myNwMainFrm.cmnCdMn.Role_Set_IDs.Length;
      for (int i = 0; i < totl; i++)
      {
        nwStr = nwStr + "," + Global.myNwMainFrm.cmnCdMn.Role_Set_IDs[i].ToString();
        if (i < totl - 1)
        {
          //nwStr = nwStr + ",";
        }
      }
      return nwStr;
    }

    public static bool doesUserHaveThisRole(string username, string rolename)
    {
      //Checks whether a given username 'admin' has a given user role
      DataSet dtSt = new DataSet();
      string sqlStr = "SELECT user_id FROM sec.sec_users_n_roles WHERE ((user_id = " +
              Global.getUserID(username) + ") AND (role_id = " + Global.myNwMainFrm.cmnCdMn.getRoleID(rolename) +
              ") AND (now() between to_timestamp(valid_start_date,'YYYY-MM-DD HH24:MI:SS') AND " +
              "to_timestamp(valid_end_date,'YYYY-MM-DD HH24:MI:SS')))";
      dtSt = Global.myNwMainFrm.cmnCdMn.selectDataNoParams(sqlStr);
      if (dtSt.Tables[0].Rows.Count > 0)
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    public static Int64 getUserID(string username)
    {
      //Example username 'admin'
      DataSet dtSt = new DataSet();
      string sqlStr = "select user_id from sec.sec_users where lower(user_name) = '" +
        username.Replace("'", "''").ToLower() + "'";
      dtSt = Global.myNwMainFrm.cmnCdMn.selectDataNoParams(sqlStr);
      if (dtSt.Tables[0].Rows.Count > 0)
      {
        return Int64.Parse(dtSt.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return -1;
      }
    }

    public static Int64 getPersonID(string locidno)
    {
      //Example username 'admin'
      DataSet dtSt = new DataSet();
      string sqlStr = "select person_id from prs.prsn_names_nos where(local_id_no ilike '" +
        locidno.Replace("'", "''") + "')";
      dtSt = Global.myNwMainFrm.cmnCdMn.selectDataNoParams(sqlStr);
      if (dtSt.Tables[0].Rows.Count > 0)
      {
        return Int64.Parse(dtSt.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return -1;
      }
    }

    public static bool isLoginInfoCorrct(string usrname, string pswd)
    {
      string sqlStr = "SELECT user_id FROM sec.sec_users WHERE ((lower(user_name) = '" + usrname.Replace("'", "''").ToLower() +
        "') AND (usr_password = md5('" + Global.myNwMainFrm.cmnCdMn.encrypt(pswd, CommonCode.CommonCodes.AppKey).Replace("'", "''") +
        "')) AND (now() between to_timestamp(valid_start_date,'YYYY-MM-DD HH24:MI:SS') AND " +
        "to_timestamp(valid_end_date,'YYYY-MM-DD HH24:MI:SS')))";
      DataSet dtSt = new DataSet();
      dtSt = Global.myNwMainFrm.cmnCdMn.selectDataNoParams(sqlStr);
      if (dtSt.Tables[0].Rows.Count > 0)
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    public static bool isUserAccntLckd(string username)
    {
      //Checks Whether a user's account is locked
      /*
       * Criteria for checking whether a user's account is locked
       * 1. Check if failed  attempts is greater than or equal to the set number
       */
      string sqlStr = "SELECT failed_login_atmpts >= " + Global.myNwMainFrm.cmnCdMn.get_CurPlcy_Mx_Fld_lgns() +
      "  FROM sec.sec_users WHERE lower(user_name) = '" + username.Replace("'", "''").ToLower() + "'";
      DataSet dtSt = new DataSet();
      dtSt = Global.myNwMainFrm.cmnCdMn.selectDataNoParams(sqlStr);
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

    public static bool shdUnlckAccnt(string username)
    {
      /* 1. Check if the difference between last_login_time and now is
       *    greater than the set duration
        */
      string sqlStr = "SELECT age(now(), to_timestamp(last_login_atmpt_time, 'YYYY-MM-DD HH24:MI:SS')) " +
        ">= interval '" + Global.myNwMainFrm.cmnCdMn.get_CurPlcy_Auto_Unlck_tme() + " minute'" +
        " FROM sec.sec_users WHERE lower(user_name) = '" + username.Replace("'", "''").ToLower() + "'";
      DataSet dtSt = new DataSet();
      dtSt = Global.myNwMainFrm.cmnCdMn.selectDataNoParams(sqlStr);
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

    public static bool isAccntSuspended(string username)
    {
      //Checks Whether a user's account is suspended
      string sqlStr = "SELECT is_suspended FROM sec.sec_users WHERE lower(user_name) = '" + username.Replace("'", "''").ToLower() + "'";
      DataSet dtSt = new DataSet();
      dtSt = Global.myNwMainFrm.cmnCdMn.selectDataNoParams(sqlStr);
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

    public static bool isPswdTmp(string username)
    {
      //Checks Whether a user's password is temporary
      string sqlStr = "SELECT is_pswd_temp FROM sec.sec_users WHERE lower(user_name) = '" + username.Replace("'", "''").ToLower() + "'";
      DataSet dtSt = new DataSet();
      dtSt = Global.myNwMainFrm.cmnCdMn.selectDataNoParams(sqlStr);
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

    public static bool isPswdExpired(string username)
    {
      //Checks whether a user's password has expired
      /*For a password to expire
       * 1. the difference between the last pasword change time and now must be greater than
       *				the set number of expiry days
       * 2. 
       */
      string sqlStr = "SELECT age(now(), to_timestamp(last_pswd_chng_time, 'YYYY-MM-DD HH24:MI:SS')) " +
        ">= interval '" + Global.myNwMainFrm.cmnCdMn.get_CurPlcy_Pwd_Exp_Days() + " days'" +
        " FROM sec.sec_users WHERE lower(user_name) = '" + username.Replace("'", "''").ToLower() + "'";
      DataSet dtSt = new DataSet();
      dtSt = Global.myNwMainFrm.cmnCdMn.selectDataNoParams(sqlStr);
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

    public static bool isPswdInRcntHstry(string pswd, Int64 usrid)
    {
      // Checks whether the new password is in the past disllowed number of passwords
      string sqlStr = "SELECT a.old_pswd_id FROM " +
        "(SELECT old_pswd_id, old_password FROM sec.sec_users_old_pswds WHERE(user_id = " +
        usrid + ") ORDER BY old_pswd_id DESC limit " + Global.myNwMainFrm.cmnCdMn.get_CurPlcy_DsllwdPswdCnt() +
        ") a WHERE(a.old_password = md5('" + Global.myNwMainFrm.cmnCdMn.encrypt(pswd, CommonCode.CommonCodes.AppKey).Replace("'", "''") + "'))";
      DataSet dtSt = new DataSet();
      dtSt = Global.myNwMainFrm.cmnCdMn.selectDataNoParams(sqlStr);
      if (dtSt.Tables[0].Rows.Count > 0)
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    public static bool isRoleSelected(int roleID)
    {
      for (int i = 0; i < Global.role_set_id.Length; i++)
      {
        if (Global.role_set_id[i] == roleID)
        {
          return true;
        }
      }
      return false;
    }
    #endregion
    #endregion

    #region "CUSTOM FUNCTIONS..."
    public static bool isRunnrRnng(string rnnrNm)
    {
      string selSQL = @"SELECT age(now(), 
to_timestamp(CASE WHEN rnnr_lst_actv_dtetme='' THEN '2013-01-01 00:00:00' ELSE rnnr_lst_actv_dtetme END, 'YYYY-MM-DD HH24:MI:SS')) " +
        @"<= interval '10 second' 
       FROM rpt.rpt_prcss_rnnrs WHERE rnnr_name='" + rnnrNm.Replace("'", "''") +
        "'";
      DataSet dtst = Global.myNwMainFrm.cmnCdMn.selectDataNoParams(selSQL);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        if (bool.Parse(dtst.Tables[0].Rows[0][0].ToString()) == true)
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

    public static void updatePrcsRnnrCmd(string rnnrNm, string cmdStr)
    {
      string dateStr = Global.myNwMainFrm.cmnCdMn.getDB_Date_time();
      string insSQL = @"UPDATE rpt.rpt_prcss_rnnrs SET 
            shld_rnnr_stop='" + cmdStr.Replace("'", "''") +
     "', last_update_by=" + Global.myNwMainFrm.cmnCdMn.User_id + ", last_update_date='" + dateStr +
     "' WHERE rnnr_name = '" + rnnrNm.Replace("'", "''") + "'";
      Global.myNwMainFrm.cmnCdMn.insertDataNoParams(insSQL);
    }

    public static void refreshRqrdVrbls()
    {
      Global.myNwMainFrm.cmnCdMn.DefaultPrvldgs = null;
      Global.myNwMainFrm.cmnCdMn.Login_number = Global.login_number;
      Global.myNwMainFrm.cmnCdMn.ModuleAdtTbl = null;
      Global.myNwMainFrm.cmnCdMn.ModuleDesc = null;
      Global.myNwMainFrm.cmnCdMn.ModuleName = null;
      //CommonCode.CommonCodes.GlobalSQLConn = CommonCode.CommonCodes.GlobalSQLConn;
      Global.myNwMainFrm.cmnCdMn.Role_Set_IDs = Global.role_set_id;
      Global.myNwMainFrm.cmnCdMn.Org_id = Global.org_id;
      Global.myNwMainFrm.cmnCdMn.SampleRole = null;
      Global.myNwMainFrm.cmnCdMn.User_id = Global.usr_id;
      Global.myNwMainFrm.cmnCdMn.Extra_Adt_Trl_Info = "";
    }
    #endregion
  }
}
