using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing.Imaging;
using AlertsManager.Forms;
using System.Windows.Forms;
using CommonCode;

namespace AlertsManager.Classes
{
  /// <summary>
  /// A  class containing variables and 
  /// functions we will like to call directly from 
  /// anywhere in the project without creating an instance first
  /// </summary>
  class Global
  {
    #region "GLOBAL DECLARATIONS..."
    public static AlertsManager myAlrt = new AlertsManager();
    public static mainForm mnFrm = null;
    public static string[] dfltPrvldgs = { "View Alerts Manager", 
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

    #endregion

    #region "CUSTOM FUNCTIONS..."
    public static void refreshRqrdVrbls()
    {
      Global.mnFrm.cmCde.DefaultPrvldgs = Global.dfltPrvldgs;
      //Global.mnFrm.cmCde.Login_number = Global.myRpt.login_number;
      Global.mnFrm.cmCde.ModuleAdtTbl = Global.myAlrt.full_audit_trail_tbl_name;
      Global.mnFrm.cmCde.ModuleDesc = Global.myAlrt.mdl_description;
      Global.mnFrm.cmCde.ModuleName = Global.myAlrt.name;
      //Global.mnFrm.cmCde.pgSqlConn = Global.myRpt.Host.globalSQLConn;
      //Global.mnFrm.cmCde.Role_Set_IDs = Global.myRpt.role_set_id;
      Global.mnFrm.cmCde.SampleRole = "Alerts Manager Administrator";
      //Global.mnFrm.cmCde.User_id = Global.myRpt.user_id;
      //Global.mnFrm.cmCde.Org_id = Global.myRpt.org_id;
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      Global.myAlrt.user_id = Global.mnFrm.usr_id;
      Global.myAlrt.login_number = Global.mnFrm.lgn_num;
      Global.myAlrt.role_set_id = Global.mnFrm.role_st_id;
      Global.myAlrt.org_id = Global.mnFrm.Og_id;

    }
    #endregion
  }
}
