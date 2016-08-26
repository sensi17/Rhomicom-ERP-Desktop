using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing.Imaging;
using Manuals.Forms;
using System.Windows.Forms;
using CommonCode;

namespace Manuals.Classes
{
  /// <summary>
  /// A  class containing variables and 
  /// functions we will like to call directly from 
  /// anywhere in the project without creating an instance first
  /// </summary>
  class Global
  {
    #region "GLOBAL DECLARATIONS..."
    public static Manuals myMnl = new Manuals();
    public static mainForm mnFrm = null;
    public static string[] dfltPrvldgs = { "View Manuals", 
      "View SQL", "View Record History"};
    public static string currentPanel = "";
    #endregion

    #region "INSERT STATEMENTS..."

    #endregion

    #region "UPDATE STATEMENTS..."

    #endregion

    #region "DELETE STATEMENTS..."
    #endregion

    #region "SELECT STATEMENTS..."
    public static string getSharedSiteUrl()
    {
      string strSql = "SELECT b.pssbl_value " +
       "FROM gst.gen_stp_lov_names a, gst.gen_stp_lov_values b " +
       "WHERE(a.value_list_id = b.value_list_id and b.is_enabled = '1'" +
       " and a.value_list_name= 'Shared Site Url') " +
       "ORDER BY b.pssbl_value_id DESC LIMIT 1 OFFSET 0";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return dtst.Tables[0].Rows[0][0].ToString();
      }
      else
      {
        return @"http://www.rhomicomgh.com";
      }
    }
    #endregion

    #region "CUSTOM FUNCTIONS..."
    public static void createRqrdLOVs()
    {

      string[] sysLovs = { "Shared Site Url" };
      string[] sysLovsDesc = { "Shared Site Url" };
      string[] sysLovsDynQrys = { ""};
      string[] pssblVals = { 
        "0", "http://www.rhomicomgh.com", "Rhomicom Website"};

      Global.mnFrm.cmCde.createSysLovs(sysLovs, sysLovsDynQrys, sysLovsDesc);
      Global.mnFrm.cmCde.createSysLovsPssblVals(sysLovs, pssblVals);

    }

    public static void refreshRqrdVrbls()
    {
      Global.mnFrm.cmCde.DefaultPrvldgs = Global.dfltPrvldgs;
      //Global.mnFrm.cmCde.Login_number = Global.myRpt.login_number;
      Global.mnFrm.cmCde.ModuleAdtTbl = Global.myMnl.full_audit_trail_tbl_name;
      Global.mnFrm.cmCde.ModuleDesc = Global.myMnl.mdl_description;
      Global.mnFrm.cmCde.ModuleName = Global.myMnl.name;
      //Global.mnFrm.cmCde.pgSqlConn = Global.myRpt.Host.globalSQLConn;
      //Global.mnFrm.cmCde.Role_Set_IDs = Global.myRpt.role_set_id;
      Global.mnFrm.cmCde.SampleRole = "Manuals Administrator";
      //Global.mnFrm.cmCde.User_id = Global.myRpt.user_id;
      //Global.mnFrm.cmCde.Org_id = Global.myRpt.org_id;
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      Global.myMnl.user_id = Global.mnFrm.usr_id;
      Global.myMnl.login_number = Global.mnFrm.lgn_num;
      Global.myMnl.role_set_id = Global.mnFrm.role_st_id;
      Global.myMnl.org_id = Global.mnFrm.Og_id;

    }
    #endregion
  }
}
