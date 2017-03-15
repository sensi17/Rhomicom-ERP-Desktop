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
using System.Drawing;
using System.IO;
using Microsoft.Win32;
using System.Management;
using Microsoft.VisualBasic;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Text.RegularExpressions;
using System.Net.Mime;
using CommonCode;

namespace CommonCode
{
    /// <summary>
    /// A  class containing variables and 
    /// functions we will like to call directly from 
    /// anywhere in the various projects of Rho Business Suite
    /// </summary>
    public class CommonCodes
    {
        #region "CONSTRUCTOR..."
        public CommonCodes()
        {
            this.initialize_images();
        }
        #endregion

        #region "GLOBAL DECLARATION..."
        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        public static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        public static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
        public static readonly IntPtr HWND_TOP = new IntPtr(0);

        public const UInt32 SWP_NOSIZE = 0x0001;
        public const UInt32 SWP_NOMOVE = 0x0002;
        public const UInt32 SWP_SHOWWINDOW = 0x0040;

        static bool is64BitProcess = (IntPtr.Size == 8);
        public static bool is64BitOperatingSystem = is64BitProcess || InternalCheckIsWow64();
        public bool isDwnldDone = false;
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetProcessWorkingSetSize(IntPtr process,
            UIntPtr minimumWorkingSetSize, UIntPtr maximumWorkingSetSize);

        [DllImport("psapi.dll")]
        static extern int EmptyWorkingSet(IntPtr hwProc);

        static string appName = "Rhomicom ERP";
        static string appVrsn = "V1 P24";
        static string appVersion = "V1.2.4 (Community Edition)";
        static string modulesNeeded = "Person Records Only";
        public static string ModulesNeeded
        {
            get { return CommonCodes.modulesNeeded; }
            set { CommonCodes.modulesNeeded = value; }
        }
        public static string AppVersion
        {
            get { return CommonCodes.appVersion; }
            //set { CommonCodes.appVersion = value; }
        }
        static string appKey = "eRRTRhbnsdGeneral Key for Rhomi|com Systems "
            + "Tech. !Ltd Enterpise/Organization @763542orbjkasdbhi68103weuikfjnsdf";

        public static string AppKey
        {
            get { return CommonCodes.appKey; }
            set { CommonCodes.appKey = value; }
        }
        static string orgnlAppKey = "eRRTRhbnsdGeneral Key for Rhomi|com Systems "
            + "Tech. !Ltd Enterpise/Organization @763542orbjkasdbhi68103weuikfjnsdf";

        public static string OrgnlAppKey
        {
            get { return CommonCodes.orgnlAppKey; }
            set { CommonCodes.orgnlAppKey = value; }
        }

        public static long lgnNum = -1;
        public static int[] rlSetIDS;
        public static long uID = -1;
        public static int ogID = -1;
        private static NpgsqlConnection globalSQLConn = new NpgsqlConnection();

        public static NpgsqlConnection GlobalSQLConn
        {
            get { return CommonCodes.globalSQLConn; }
            set { CommonCodes.globalSQLConn = value; }
        }
        public static string AppVrsn
        {
            get { return CommonCodes.appVrsn; }
            //set { CommonCodes.appVrsn = value; }
        }
        public bool ignorAdtTrail = false;

        public static string AppName
        {
            get { return appName; }
            //set { appName = value; }
        }
        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWow64Process(
            [In] IntPtr hProcess,
            [Out] out bool wow64Process
        );

        //public cadmaFunctions.Encrypt encrpter = new cadmaFunctions.Encrypt();
        public cadmaFunctions.NavFuncs navFuncts = new cadmaFunctions.NavFuncs();
        public Computer myComputer = new Microsoft.VisualBasic.Devices.Computer();
        //private const string rootOrg = "RHOMICOM_ROOT";
        //private NpgsqlConnection sqlConn = new NpgsqlConnection();
        private string mdlNm;
        private string mdlDesc;
        private string adtTbl;
        private long usr_id;
        private long lgn_num;
        private static string[] localDataPool;

        public static string[] LocalDataPool
        {
            get { return CommonCodes.localDataPool; }
            set { CommonCodes.localDataPool = value; }
        }
        private string[] dfltPrvldgs;
        private string[] subGrpNames;
        private string[] mainTableNames;
        private string[] keyColumnNames;
        private string sampleRole;
        private int[] role_sets;
        private int org_id;
        private string extra_adt_info;
        private long dflt_totl = 20000000000000000;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ImageList imageList1;
        private bool autoRfrsh = false;
        private int autoRfrshTime = 10000;
        private static string lastActvDteTme = "";
        private static string db_host = "";
        private static string curRptFiles = "";
        private static string connStr = "";
        private static string databaseNm = "";
        //public Microsoft.Office.Interop.Excel.Application exclApp = null;
        //public Excel.Workbook nwWrkBk = null;
        //public Excel.Worksheet[] trgtSheets = new Excel.Worksheet[1];
        //public Microsoft.Office.Interop.Excel.Range dataRng = null;

        public static string DatabaseNm
        {
            get { return CommonCodes.databaseNm; }
            set { CommonCodes.databaseNm = value; }
        }

        public static string ConnStr
        {
            get { return CommonCodes.connStr; }
            set { CommonCodes.connStr = value; }
        }

        public static string CurRptFiles
        {
            get { return CommonCodes.curRptFiles; }
            set { CommonCodes.curRptFiles = value; }
        }

        public static string Db_host
        {
            get { return CommonCodes.db_host; }
            set { CommonCodes.db_host = value; }
        }
        private static string db_port = "";

        public static string Db_port
        {
            get { return CommonCodes.db_port; }
            set { CommonCodes.db_port = value; }
        }
        private static string db_dbase = "";

        public static string Db_dbase
        {
            get { return CommonCodes.db_dbase; }
            set { CommonCodes.db_dbase = value; }
        }
        private static string db_uname = "";

        public static string Db_uname
        {
            get { return CommonCodes.db_uname; }
            set { CommonCodes.db_uname = value; }
        }
        private static string db_pwd = "";
        public static Color[] myFrmClrs;
        private static bool autoConnect = false;

        public static bool AutoConnect
        {
            get { return CommonCodes.autoConnect; }
            set { CommonCodes.autoConnect = value; }
        }

        //= { Color.FromArgb(0, 102, 160), Color.FromArgb(0, 129, 206), Color.FromArgb(0, 255, 0) };

        public static string Db_pwd
        {
            get { return CommonCodes.db_pwd; }
            set { CommonCodes.db_pwd = value; }
        }
        private static string bsc_prsn_name = "";

        public static string Bsc_prsn_name
        {
            get { return CommonCodes.bsc_prsn_name; }
            set { CommonCodes.bsc_prsn_name = value; }
        }
        private static string intrnl_pymnts_name = "";

        public static string Intrnl_pymnts_name
        {
            get { return CommonCodes.intrnl_pymnts_name; }
            set { CommonCodes.intrnl_pymnts_name = value; }
        }
        private static string learning_name = "";

        public static string Learning_name
        {
            get { return CommonCodes.learning_name; }
            set { CommonCodes.learning_name = value; }
        }
        private static string events_name = "";

        public static string Events_name
        {
            get { return CommonCodes.events_name; }
            set { CommonCodes.events_name = value; }
        }
        private static string hospitality_name = "";

        public static string Hospitality_name
        {
            get { return CommonCodes.hospitality_name; }
            set { CommonCodes.hospitality_name = value; }
        }
        private static string store_inventory = "";

        public static string Store_inventory
        {
            get { return CommonCodes.store_inventory; }
            set { CommonCodes.store_inventory = value; }
        }

        private static string appointments_name = "";

        public static string Appointments_name
        {
            get { return CommonCodes.appointments_name; }
            set { CommonCodes.appointments_name = value; }
        }

        private static string proj_mgmnt_name = "";

        public static string Proj_mgmnt_name
        {
            get { return CommonCodes.proj_mgmnt_name; }
            set { CommonCodes.proj_mgmnt_name = value; }
        }

        public Microsoft.Office.Interop.Excel.Application exclApp = null;
        public Excel.Workbook nwWrkBk = null;
        public Excel.Worksheet[] trgtSheets = new Excel.Worksheet[1];
        public Microsoft.Office.Interop.Excel.Range dataRng = null;

        public static string LastActvDteTme
        {
            get { return lastActvDteTme; }
            set { lastActvDteTme = value; }
        }
        //private string lstActvTime;

        //public string LastActvtyTime
        //{
        //  get { return lstActvTime; }
        //  set { lstActvTime = value; }
        //}

        public int AutoRfrshTime
        {
            get { return autoRfrshTime; }
            set { autoRfrshTime = value; }
        }

        public bool AutoRfrsh
        {
            get { return autoRfrsh; }
            set { autoRfrsh = value; }
        }

        //public string RootOrg
        // {
        // get { return rootOrg; }
        // } 

        public int Org_id
        {
            get { return org_id; }
            set { org_id = value; }
        }

        public long Big_Val
        {
            get { return dflt_totl; }
            set { dflt_totl = value; }
        }

        public string Extra_Adt_Trl_Info
        {
            get { return extra_adt_info; }
            set { extra_adt_info = value; }
        }

        public int[] Role_Set_IDs
        {
            get { return role_sets; }
            set { role_sets = value; }
        }

        public string[] DefaultPrvldgs
        {
            get { return dfltPrvldgs; }
            set { dfltPrvldgs = value; }
        }
        public string[] SubGrpNames
        {
            get { return subGrpNames; }
            set { subGrpNames = value; }
        }
        public string[] MainTableNames
        {
            get { return mainTableNames; }
            set { mainTableNames = value; }
        }
        public string[] KeyColumnNames
        {
            get { return keyColumnNames; }
            set { keyColumnNames = value; }
        }

        public string SampleRole
        {
            get { return sampleRole; }
            set { sampleRole = value; }
        }

        public long User_id
        {
            get { return usr_id; }
            set { usr_id = value; }
        }

        public long Login_number
        {
            get { return lgn_num; }
            set { lgn_num = value; }
        }

        public string ModuleName
        {
            get { return mdlNm; }
            set { mdlNm = value; }
        }

        public string ModuleDesc
        {
            get { return mdlDesc; }
            set { mdlDesc = value; }
        }

        public string ModuleAdtTbl
        {
            get { return adtTbl; }
            set { adtTbl = value; }
        }

        //public NpgsqlConnection pgSqlConn
        //{
        //  get { return sqlConn; }
        //  set { sqlConn = value; }
        //}
        #endregion

        #region "GENERAL SQL FUNCTIONS..."
        /// <summary>
        /// Processes select statements passed to it
        /// </summary>
        ///

        public void deleteTmpFiles()
        {
            try
            {
                char[] w = { '|' };
                string[] arry1 = CommonCodes.CurRptFiles.Split(w, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < arry1.Length; i++)
                {
                    if (System.IO.File.Exists(arry1[i]))
                    {
                        //file exists!
                        System.IO.File.Delete(arry1[i]);
                    }
                }

                CommonCodes.CurRptFiles = "";
            }
            catch (Exception ex)
            {
            }

        }
        public DataSet selectDataNoParams(string selSql)
        {
            DataSet selDtSt = new DataSet();
            try
            {
                //if (CommonCode.GlobalSQLConn.State != ConnectionState.Open)
                //{
                //  CommonCode.GlobalSQLConn.Open();
                //  //return false;
                //}
                deleteTmpFiles();
                /*Necessary to make simultaneous connections and background processes possible 
                 * and also to make sure unused and inactive connections are 
                 * closed to free up resources on the server*/
                NpgsqlConnection mycon = new NpgsqlConnection();
                mycon.ConnectionString = CommonCodes.ConnStr;
                mycon.Open();
                if (this.hsSessionExpired(mycon))
                {
                    loginDiag nwDiag = new loginDiag();
                    nwDiag.cmnCde = this;
                    DialogResult dgRes = nwDiag.ShowDialog();
                    if (dgRes != DialogResult.OK)
                    {
                        this.showMsg("Session has expired! Application will \r\nrestart for you to login again!", 0);
                        System.Windows.Forms.Application.Restart();
                        return selDtSt;
                    }
                }
                NpgsqlDataAdapter selDtAdpt = new NpgsqlDataAdapter();
                NpgsqlCommand selCmd = new NpgsqlCommand(@selSql, mycon);
                selDtAdpt.SelectCommand = selCmd;
                selDtAdpt.Fill(selDtSt, "table_1");
                selCmd.Connection.Close();
                mycon.Close();
                return selDtSt;
            }
            catch (Exception ex)
            {
                this.showSQLNoPermsn(ex.Message + "\r\n" + selSql);
                return selDtSt;
            }
        }

        public bool hsSessionExpired(NpgsqlConnection mycon)
        {
            try
            {

                if (this.Login_number > 0 && this.User_id > 0)
                {
                    if (CommonCodes.LastActvDteTme == "")
                    {
                        CommonCodes.LastActvDteTme = this.getDB_Date_time();
                        return false;
                    }
                    else
                    {
                        DateTime dte1 = DateTime.ParseExact(
                        CommonCodes.LastActvDteTme, "yyyy-MM-dd HH:mm:ss",
                        System.Globalization.CultureInfo.InvariantCulture);
                        DateTime dte2 = DateTime.ParseExact(this.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
                        System.Globalization.CultureInfo.InvariantCulture);

                        long diff = Math.Abs(Microsoft.VisualBasic.DateAndTime.DateDiff(
                          Microsoft.VisualBasic.DateInterval.Second, dte1, dte2,
                          Microsoft.VisualBasic.FirstDayOfWeek.Sunday,
                          Microsoft.VisualBasic.FirstWeekOfYear.FirstFullWeek));
                        if (diff >= this.get_CurPlcy_SessnTime())
                        {
                            return true;
                        }
                        else
                        {
                            CommonCodes.LastActvDteTme = this.getDB_Date_time();
                            return false;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                this.showMsg(ex.Message, 0);// , 0);
                return false;
            }
        }

        public DataSet selectDataNoParams1(string selSql)
        {
            DataSet selDtSt = new DataSet();
            try
            {
                NpgsqlConnection mycon = new NpgsqlConnection();
                mycon.ConnectionString = CommonCodes.ConnStr;
                mycon.Open();
                NpgsqlDataAdapter selDtAdpt = new NpgsqlDataAdapter();
                NpgsqlCommand selCmd = new NpgsqlCommand(@selSql, mycon);
                selDtAdpt.SelectCommand = selCmd;
                selDtAdpt.Fill(selDtSt, "table_2");
                selCmd.Connection.Close();
                mycon.Close();
                return selDtSt;
            }
            catch (Exception ex)
            {
                return selDtSt;
            }

        }
        /// <summary>
        /// Processes delete statements passed to it
        /// </summary>
        public void deleteDataNoParams(string delSql)
        {
            try
            {

                NpgsqlDataAdapter delDtAdpt = new NpgsqlDataAdapter();
                NpgsqlConnection mycon = new NpgsqlConnection();
                mycon.ConnectionString = CommonCodes.ConnStr;
                mycon.Open();
                if (this.hsSessionExpired(mycon))
                {
                    loginDiag nwDiag = new loginDiag();
                    nwDiag.cmnCde = this;
                    DialogResult dgRes = nwDiag.ShowDialog();
                    if (dgRes != DialogResult.OK)
                    {
                        this.showMsg("Session has expired! Application will \r\nrestart for you to login again", 0);
                        System.Windows.Forms.Application.Restart();
                        return;
                    }
                }
                NpgsqlCommand delCmd = new NpgsqlCommand(@delSql, mycon);
                delDtAdpt.DeleteCommand = delCmd;
                delCmd.ExecuteNonQuery();
                delCmd.Connection.Close();
                mycon.Close();
                if (this.ignorAdtTrail == false)
                {
                    this.storeAdtTrailInfo(delSql, 1);
                }
                return;
            }
            catch (Exception ex)
            {
                //this.showSQLNoPermsn(ex.Message + "\r\n" + delSql);
                return;
            }
        }

        /// <summary>
        /// Processes insert statements passed to it
        /// </summary>
        public void insertDataNoParams(string insSql)
        {
            try
            {

                NpgsqlDataAdapter insDtAdpt = new NpgsqlDataAdapter();
                NpgsqlDataAdapter delDtAdpt = new NpgsqlDataAdapter();
                NpgsqlConnection mycon = new NpgsqlConnection();
                mycon.ConnectionString = CommonCodes.ConnStr;
                mycon.Open();
                if (this.hsSessionExpired(mycon))
                {
                    loginDiag nwDiag = new loginDiag();
                    nwDiag.cmnCde = this;
                    DialogResult dgRes = nwDiag.ShowDialog();
                    if (dgRes != DialogResult.OK)
                    {
                        this.showMsg("Session has expired! Application will \r\nrestart for you to login again", 0);
                        System.Windows.Forms.Application.Restart();
                        return;
                    }
                }
                NpgsqlCommand insCmd = new NpgsqlCommand(@insSql, mycon);
                insDtAdpt.InsertCommand = insCmd;
                insCmd.ExecuteNonQuery();
                insCmd.Connection.Close();
                mycon.Close();
                return;
            }
            catch (Exception ex)
            {
                //this.showSQLNoPermsn(ex.Message + "\r\n" + ex.StackTrace + "\r\n" + ex.InnerException);
                this.showSQLNoPermsn(ex.Message + "\r\n" + insSql);
                return;
            }//.Replace(@"\", @"\\")
        }

        /// <summary>
        /// Processes update statements passed to it
        /// </summary>
        public void updateDataNoParams(string updtSql)
        {
            try
            {

                NpgsqlDataAdapter updtDtAdpt = new NpgsqlDataAdapter();
                NpgsqlConnection mycon = new NpgsqlConnection();
                mycon.ConnectionString = CommonCodes.ConnStr;
                mycon.Open();
                if (this.hsSessionExpired(mycon))
                {
                    loginDiag nwDiag = new loginDiag();
                    nwDiag.cmnCde = this;
                    DialogResult dgRes = nwDiag.ShowDialog();
                    if (dgRes != DialogResult.OK)
                    {
                        this.showMsg("Session has expired! Application will \r\nrestart for you to login again", 0);
                        System.Windows.Forms.Application.Restart();
                        return;
                    }
                }
                NpgsqlCommand updtCmd = new NpgsqlCommand(@updtSql, mycon);
                updtDtAdpt.UpdateCommand = updtCmd;
                updtCmd.ExecuteNonQuery();
                updtCmd.Connection.Close();
                mycon.Close();
                if (this.ignorAdtTrail == false)
                {
                    this.storeAdtTrailInfo(updtSql, 0);
                }
                return;
            }
            catch (Exception ex)
            {
                this.showSQLNoPermsn(ex.Message + "\r\n" + ex.StackTrace + "\r\n" + ex.InnerException + "\r\n" + updtSql);
                //this.showSQLNoPermsn(ex.Message + "\r\n" + updtSql);
                return;
            }//.Replace(@"\", @"\\")
        }

        public void updateDataNoParams1(string updtSql)
        {
            try
            {
                NpgsqlConnection mycon = new NpgsqlConnection();
                mycon.ConnectionString = CommonCodes.ConnStr;
                mycon.Open();
                NpgsqlDataAdapter updtDtAdpt = new NpgsqlDataAdapter();
                NpgsqlCommand updtCmd = new NpgsqlCommand(@updtSql, mycon);
                updtDtAdpt.UpdateCommand = updtCmd;
                updtCmd.ExecuteNonQuery();
                updtCmd.Connection.Close();
                mycon.Close();
                if (this.ignorAdtTrail == false)
                {
                    this.storeAdtTrailInfo(updtSql, 0);
                }
                return;
            }
            catch (Exception ex)
            {
                return;
            }//.Replace(@"\", @"\\")
        }

        public void executeGnrlSQL(string genSql)
        {
            try
            {

                NpgsqlConnection mycon = new NpgsqlConnection();
                mycon.ConnectionString = CommonCodes.ConnStr;
                mycon.Open();
                if (this.hsSessionExpired(mycon))
                {
                    loginDiag nwDiag = new loginDiag();
                    nwDiag.cmnCde = this;
                    DialogResult dgRes = nwDiag.ShowDialog();
                    if (dgRes != DialogResult.OK)
                    {
                        this.showMsg("Session has expired! Application will \r\nrestart for you to login again", 0);
                        System.Windows.Forms.Application.Restart();
                        return;
                    }
                }
                NpgsqlCommand gnrlCmd = new NpgsqlCommand(@genSql, mycon);
                gnrlCmd.ExecuteNonQuery();
                gnrlCmd.Connection.Close();
                mycon.Close();
                return;
            }
            catch (Exception ex)
            {
                //this.showSQLNoPermsn(ex.Message + "\r\n" + ex.StackTrace + "\r\n" + ex.InnerException);
                //this.showSQLNoPermsn(ex.Message + "\r\n" + genSql);
                return;
            }//.Replace(@"\", @"\\")
        }

        public void executeGnrlDDLSQL(string genSql)
        {
            try
            {
                NpgsqlConnection mycon = new NpgsqlConnection();
                mycon.ConnectionString = CommonCodes.ConnStr;
                mycon.Open();
                NpgsqlCommand gnrlCmd = new NpgsqlCommand(@genSql, mycon);
                gnrlCmd.ExecuteNonQuery();
                gnrlCmd.Connection.Close();
                mycon.Close();
                return;
            }
            catch (Exception ex)
            {
                //this.showSQLNoPermsn(ex.Message + "\r\n" + ex.StackTrace + "\r\n" + ex.InnerException);
                //this.showSQLNoPermsn(ex.Message + "\r\n" + genSql);
                return;
            }//.Replace(@"\", @"\\")
        }
        #endregion

        #region "DATA MANIPULATION FUNCTIONS..."
        #region "INSERT STATEMENTS..."

        public void registerThsModule()
        {
            string dateStr = this.getDB_Date_time();
            string sqlStr = "INSERT INTO sec.sec_modules (module_name, module_desc, " +
             "date_added, audit_trail_tbl_name) VALUES ('" +
             this.ModuleName.Replace("'", "''") + "', '" +
             this.ModuleDesc.Replace("'", "''") +
             "', '" + dateStr + "', '" + this.ModuleAdtTbl + "')";
            this.insertDataNoParams(sqlStr);
        }

        public void registerThsModulesSubgroups(string sub_grp_nm, string mn_table_nm, string rw_pk_nm, int mdlID)
        {
            string dateStr = this.getDB_Date_time();
            string sqlStr = "INSERT INTO sec.sec_module_sub_groups (sub_group_name, main_table_name, " +
             "row_pk_col_name, module_id, date_added) VALUES ('" +
             sub_grp_nm.Replace("'", "''") + "', '" +
             mn_table_nm.Replace("'", "''") + "', '" +
             rw_pk_nm.Replace("'", "''") + "', " +
             mdlID +
             ", '" + dateStr + "')";
            this.insertDataNoParams(sqlStr);
        }

        public void createSampleRole(string roleNm)
        {
            long uID = -1;
            if (this.User_id <= 0)
            {
                uID = this.getUserID("admin");
            }
            else
            {
                uID = this.User_id;
            }
            string dateStr = this.getDB_Date_time();
            string sqlStr = "INSERT INTO sec.sec_roles(role_name, valid_start_date, valid_end_date, created_by, " +
                     "creation_date, last_update_by, last_update_date) VALUES ('" + roleNm.Replace("'", "''") + "', '" +
                     dateStr + "', '4000-12-31 00:00:00', " + uID + ", '" + dateStr + "', " + uID + ", '" + dateStr + "')";
            this.insertDataNoParams(sqlStr);
        }

        public void createPrvldg(string prvlg_nm)
        {
            string dateStr = this.getDB_Date_time();
            string sqlStr = "INSERT INTO sec.sec_prvldgs(prvldg_name, module_id) VALUES ('" +
             prvlg_nm.Replace("'", "''") + "', " + this.getModuleID(this.ModuleName) + ")";
            this.insertDataNoParams(sqlStr);
        }

        public void asgnPrvlgToSmplRole(int prvldg_id, string roleNm)
        {
            long uID = -1;
            if (this.User_id <= 0)
            {
                uID = this.getUserID("admin");
            }
            else
            {
                uID = this.User_id;
            }
            string dateStr = this.getDB_Date_time();
            string sqlStr = "INSERT INTO sec.sec_roles_n_prvldgs(role_id, prvldg_id, valid_start_date, valid_end_date, created_by, " +
                     "creation_date, last_update_by, last_update_date) VALUES (" + this.getRoleID(roleNm) + ", " + prvldg_id + ", '" +
                     dateStr + "', '4000-12-31 00:00:00', " + uID + ", '" + dateStr + "', " + uID + ", '" + dateStr + "')";
            this.insertDataNoParams(sqlStr);
        }

        /// <summary>
        /// actntype - Audit Trail Action Types
        /// {0 = UPDATE STATEMENTS}
        /// {1 = DELETE STATEMENTS}
        /// </summary>

        public void storeAdtTrailInfo(string infoStmnt, int actntype)
        {
            string[] action_types = { "UPDATE STATEMENTS", "DELETE STATEMENTS" };
            if (this.ModuleAdtTbl == null || this.ModuleAdtTbl == "")
            {
                return;
            }
            if (this.doesCrPlcTrckThisActn(action_types[actntype]) == false)
            {
                return;
            }
            string dateStr = this.getDB_Date_time();
            string sqlStr = "INSERT INTO " + this.ModuleAdtTbl + " (" +
             "user_id, action_type, action_details, action_time, login_number) " +
             "VALUES (" + this.User_id + ", '" + action_types[actntype] +
             "', '" + this.Extra_Adt_Trl_Info.Replace("'", "''") + "" +
             infoStmnt.Replace("'", "''") + "', '" + dateStr + "', " + this.Login_number + ")";
            this.insertDataNoParams(sqlStr);
        }

        public long getNewExtInfoID(string extInfSeq)
        {
            string strSql = "select nextval('" + extInfSeq + "')";
            //last_value from 
            DataSet dtst = this.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public void createRowOthrInfVal(string valTbl, long cmbntnID,
          long rowValID, string othrInfVal, string othInfLbl,
          string othrInfCtgry, long rowID)
        {
            string dateStr = this.getDB_Date_time();
            string sqlStr = "INSERT INTO " + valTbl + " (" +
                      "dflt_row_id, tbl_othr_inf_combntn_id, row_pk_id_val, other_info_value, " +
                      "created_by, creation_date, last_update_by, last_update_date, " +
                      "other_info_label, other_info_category) " +
              "VALUES (" + rowID + ", " + cmbntnID + ", " + rowValID + ", '" + othrInfVal.Replace("'", "''") +
              "', " + this.User_id + ", " +
              "'" + dateStr + "', " + this.User_id + ", '" + dateStr +
              "', '" + othInfLbl.Replace("'", "''") + "', '" + othrInfCtgry.Replace("'", "''") + "')";
            this.insertDataNoParams(sqlStr);
        }
        #endregion

        #region "UPDATE STATEMENTS..."
        public void updateRowOthrInfVal(string valTbl, long cmbntnID,
          long rowValID, string othrInfVal, string othInfLbl,
          string othrInfCtgry, long rowID)
        {
            string dateStr = this.getDB_Date_time();
            string updtStr = "UPDATE " + valTbl + " SET " +
                      "other_info_value = '" + othrInfVal.Replace("'", "''") + "', " +
                      "other_info_label = '" + othInfLbl.Replace("'", "''") + "', " +
                      "other_info_category = '" + othrInfCtgry.Replace("'", "''") + "', " +
                      "last_update_by = " + this.User_id + ", last_update_date = '" + dateStr + "' " +
              "WHERE (((tbl_othr_inf_combntn_id = " + cmbntnID +
              " and tbl_othr_inf_combntn_id>0) or (dflt_row_id = " + rowID +
              ")) AND (row_pk_id_val = " + rowValID + "))";
            this.updateDataNoParams(updtStr);
        }

        public void deleteRowOthrInfVal(long extInfoID, string valTbl)
        {
            this.Extra_Adt_Trl_Info = "";
            string delSQL = "DELETE FROM " + valTbl + " WHERE dflt_row_id = " + extInfoID;
            this.deleteDataNoParams(delSQL);
        }

        #endregion

        #region "DELETE STATEMENTS..."
        public void deleteGnrlRecs(long rowID, string extr_info, string tblnm, string pk_nm)
        {
            this.Extra_Adt_Trl_Info = extr_info;
            string delSQL = "DELETE FROM " + tblnm + " WHERE " + pk_nm + " = " + rowID;
            this.deleteDataNoParams(delSQL);
        }
        #endregion

        #region "SELECT STATEMENTS..."
        #region "GENERAL..."
        public string getDB_Date_time()
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select to_char(now(), 'YYYY-MM-DD HH24:MI:SS')";
            dtSt = this.selectDataNoParams1(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public string getFrmtdDB_Date_time()
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select to_char(now(), 'DD-Mon-YYYY HH24:MI:SS')";
            dtSt = this.selectDataNoParams1(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public int getCurPlcyID()
        {
            //Example policy name 'Standard ISO Password Policy'
            DataSet dtSt = new DataSet();
            string sqlStr = "select policy_id from sec.sec_security_policies where is_default = 't'";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public int get_CurPlcy_SessnTime()
        {
            //pswd_expiry_days
            string sqlStr = "SELECT session_timeout FROM " +
          "sec.sec_security_policies WHERE is_default = 't'";
            DataSet dtSt = new DataSet();
            dtSt = this.selectDataNoParams1(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 300;
            }
        }

        public int get_CurPlcy_Mx_Dsply_Recs()
        {
            //pswd_expiry_days
            string sqlStr = "SELECT max_no_recs_to_dsply FROM " +
          "sec.sec_security_policies WHERE is_default = 't'";
            DataSet dtSt = new DataSet();
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 30;
            }
        }

        public int get_CurPlcy_Mx_Fld_lgns()
        {
            //max_failed_lgn_attmpts
            string sqlStr = "SELECT max_failed_lgn_attmpts FROM " +
          "sec.sec_security_policies WHERE is_default = 't'";
            DataSet dtSt = new DataSet();
            dtSt = this.selectDataNoParams1(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 1000000;
            }
        }

        public int get_CurPlcy_Pwd_Exp_Days()
        {
            //pswd_expiry_days
            string sqlStr = "SELECT pswd_expiry_days FROM " +
          "sec.sec_security_policies WHERE is_default = 't'";
            DataSet dtSt = new DataSet();
            dtSt = this.selectDataNoParams1(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 1000000;
            }
        }

        public int get_CurPlcy_Auto_Unlck_tme()
        {
            //auto_unlocking_time_mins
            string sqlStr = "SELECT auto_unlocking_time_mins FROM " +
             "sec.sec_security_policies WHERE is_default = 't'";
            DataSet dtSt = new DataSet();
            dtSt = this.selectDataNoParams1(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        public int get_CurPlcy_DsllwdPswdCnt()
        {
            // Gets the number of past passwords to disallow when creating a new password
            string sqlStr = "SELECT old_pswd_cnt_to_disallow FROM " +
         "sec.sec_security_policies WHERE is_default = 't'";
            DataSet dtSt = new DataSet();
            dtSt = this.selectDataNoParams1(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 10;
            }
        }

        public int get_CurPlcy_Min_Pwd_Len()
        {
            // Gets the minimum required length of passwords
            string sqlStr = "SELECT pswd_min_length FROM " +
         "sec.sec_security_policies WHERE is_default = 't'";
            DataSet dtSt = new DataSet();
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 1;
            }
        }

        public int get_CurPlcy_Mx_Pwd_Len()
        {
            // Gets the maximum required length of passwords
            string sqlStr = "SELECT pswd_max_length FROM " +
         "sec.sec_security_policies WHERE is_default = 't'";
            DataSet dtSt = new DataSet();
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 25;
            }
        }

        public string get_CrPlc_Rqrmt_Cmbntn()
        {
            // Gets the Password requirements combinations
            string sqlStr = "SELECT pswd_reqrmnt_combntns FROM " +
         "sec.sec_security_policies WHERE is_default = 't'";
            DataSet dtSt = new DataSet();
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "NONE";
            }
        }

        public string get_user_name(Int64 userID)
        {
            //Gets the last password change date 
            string sqlStr = "SELECT user_name FROM " +
            "sec.sec_users WHERE user_id = " + userID + "";
            DataSet dtSt = new DataSet();
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public string get_role_name(Int64 roleID)
        {
            //Gets the last password change date 
            string sqlStr = "SELECT role_name FROM " +
            "sec.sec_roles WHERE role_id = " + roleID + "";
            DataSet dtSt = new DataSet();
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }
        #endregion
        #endregion

        #region "VERIFICATION STATEMENTS..."
        static void MinimizeFootprint()
        {
            EmptyWorkingSet(Process.GetCurrentProcess().Handle);
        }

        public void minimizeMemory()
        {
            GC.Collect(GC.MaxGeneration);
            GC.WaitForPendingFinalizers();
            SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle,
                (UIntPtr)0xFFFFFFFF, (UIntPtr)0xFFFFFFFF);
            MinimizeFootprint();
        }

        public bool areThereUnpstdTrns(string datestr)
        {

            datestr = DateTime.ParseExact(
         datestr, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

            string strSql = "";
            strSql = "SELECT count(1) " +
             "FROM accb.accb_trnsctn_details a " +
             "WHERE(a.trns_status = '0' and to_timestamp(a.trnsctn_date," +
             "'YYYY-MM-DD HH24:MI:SS') <= to_timestamp('" + datestr + "','YYYY-MM-DD HH24:MI:SS'))";
            DataSet dtst = this.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count == 1)
            {
                if (long.Parse(dtst.Tables[0].Rows[0][0].ToString()) > 0)
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

        public int get_DfltCashAcnt(int orgID)
        {
            string strSql = "SELECT sales_cash_acnt_id " +
             "FROM scm.scm_dflt_accnts a " +
             "WHERE(a.org_id = " + orgID + ")";

            DataSet dtst = this.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public bool isTransPrmttd(int accntID, string trnsdate, double amnt)
        {
            try
            {
                //        trnsdate = DateTime.ParseExact(
                //trnsdate, "dd-MMM-yyyy HH:mm:ss",
                //System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                //Transaction date must be >= the latest prd start date
                if (accntID <= 0 || trnsdate == "")
                {
                    return false;
                }
                DateTime trnsDte = DateTime.ParseExact(trnsdate, "dd-MMM-yyyy HH:mm:ss",
            System.Globalization.CultureInfo.InvariantCulture);
                DateTime dte1 = DateTime.ParseExact(this.getLtstPrdStrtDate(), "dd-MMM-yyyy HH:mm:ss",
            System.Globalization.CultureInfo.InvariantCulture);
                DateTime dte1Or = DateTime.ParseExact(this.getLastPrdClseDate(), "dd-MMM-yyyy HH:mm:ss",
            System.Globalization.CultureInfo.InvariantCulture);
                DateTime dte2 = DateTime.ParseExact(this.getLtstPrdEndDate(), "dd-MMM-yyyy HH:mm:ss",
            System.Globalization.CultureInfo.InvariantCulture);
                if (trnsDte <= dte1Or)
                {
                    this.showMsg("Transaction Date cannot be On or Before " + dte1Or.ToString("dd-MMM-yyyy HH:mm:ss"), 0);
                    return false;
                }
                if (trnsDte < dte1)
                {
                    this.showMsg("Transaction Date cannot be before " + dte1.ToString("dd-MMM-yyyy HH:mm:ss"), 0);
                    return false;
                }
                if (trnsDte > dte2)
                {
                    this.showMsg("Transaction Date cannot be after " + dte2.ToString("dd-MMM-yyyy HH:mm:ss"), 0);
                    return false;
                }
                //Check if trnsDate exists in an Open Period
                long prdHdrID = this.getPrdHdrID(this.Org_id);
                //this.showMsg(this.Org_id.ToString() + "-" + prdHdrID.ToString(), 0);
                if (prdHdrID > 0)
                {
                    //this.showMsg(trnsDte.ToString("yyyy-MM-dd HH:mm:ss") + "-" + prdHdrID.ToString(), 0);

                    if (this.getTrnsDteOpenPrdLnID(prdHdrID, trnsDte.ToString("yyyy-MM-dd HH:mm:ss")) < 0)
                    {
                        this.showMsg("Cannot use a Transaction Date (" + trnsDte.ToString("dd-MMM-yyyy HH:mm:ss") + ") which does not exist in any OPEN period!", 0);
                        return false;
                    }
                    //Check if Date is not in Disallowed Dates
                    string noTrnsDatesLov = this.getGnrlRecNm("accb.accb_periods_hdr", "periods_hdr_id", "no_trns_dates_lov_nm", prdHdrID);
                    string noTrnsDayLov = this.getGnrlRecNm("accb.accb_periods_hdr", "periods_hdr_id", "no_trns_wk_days_lov_nm", prdHdrID);
                    //this.showMsg(noTrnsDatesLov + "-" + noTrnsDayLov + "-" + trnsDte.ToString("dddd").ToUpper() + "-" + trnsDte.ToString("dd-MMM-yyyy").ToUpper(), 0);

                    if (noTrnsDatesLov != "")
                    {
                        if (this.getEnbldPssblValID(trnsDte.ToString("dd-MMM-yyyy").ToUpper(), this.getEnbldLovID(noTrnsDatesLov)) > 0)
                        {
                            this.showMsg("Transactions on this Date (" + trnsDte.ToString("dd-MMM-yyyy HH:mm:ss") + ") have been banned on this system!", 0);
                            return false;
                        }
                    }
                    //Check if Day of Week is not in Disaalowed days
                    if (noTrnsDatesLov != "")
                    {
                        if (this.getEnbldPssblValID(trnsDte.ToString("dddd").ToUpper(), this.getEnbldLovID(noTrnsDayLov)) > 0)
                        {
                            this.showMsg("Transactions on this Day of Week (" + trnsDte.ToString("dddd") + ") have been banned on this system!", 0);
                            return false;
                        }
                    }
                }

                //Amount must not disobey budget settings on that account
                long actvBdgtID = this.getActiveBdgtID(this.Org_id);
                double amntLmt = this.getAcntsBdgtdAmnt(actvBdgtID,
                  accntID, trnsDte.ToString("dd-MMM-yyyy HH:mm:ss"));
                DateTime bdte1 = DateTime.ParseExact(
                  this.getAcntsBdgtStrtDte(actvBdgtID, accntID,
                  trnsDte.ToString("dd-MMM-yyyy HH:mm:ss")), "dd-MMM-yyyy HH:mm:ss",
            System.Globalization.CultureInfo.InvariantCulture);
                DateTime bdte2 = DateTime.ParseExact(
                  this.getAcntsBdgtEndDte(actvBdgtID, accntID,
                  trnsDte.ToString("dd-MMM-yyyy HH:mm:ss")), "dd-MMM-yyyy HH:mm:ss",
            System.Globalization.CultureInfo.InvariantCulture);
                double crntBals = this.getTrnsSum(accntID, bdte1.ToString("dd-MMM-yyyy HH:mm:ss")
                  , bdte2.ToString("dd-MMM-yyyy HH:mm:ss"), "1");
                string actn = this.getAcntsBdgtLmtActn(actvBdgtID, accntID, trnsdate);
                //this.showMsg(amntLmt + "-" + crntBals + "-" + amnt + "-" + bdte1.ToString("dd-MMM-yyyy HH:mm:ss").ToUpper() + "-" + bdte2.ToString("dd-MMM-yyyy").ToUpper(), 0);

                if ((amnt + crntBals) > amntLmt)
                {
                    if (actn == "Disallow")
                    {
                        this.showMsg("This transaction will cause budget on \r\nthe chosen account to be exceeded! ", 4);
                        return false;
                    }
                    else if (actn == "Warn")
                    {
                        this.showMsg("This is just to WARN you that the budget on \r\nthe chosen account will be exceeded!", 0);
                        return true;
                    }
                    else if (actn == "Congratulate")
                    {
                        this.showMsg("This is just to CONGRATULATE you for exceeding the targetted Amount! ", 3);
                        return true;
                    }
                    else
                    {
                        return true;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                this.showMsg(ex.InnerException + "\r\n" + ex.StackTrace + "\r\n" + ex.Message, 0);
                return false;
            }
        }

        public double getTrnsSum(int accntid, string strDte, string endDte, string ispsted)
        {
            strDte = DateTime.ParseExact(
         strDte, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

            endDte = DateTime.ParseExact(
         endDte, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

            string strSql = "";
            strSql = "SELECT SUM(a.net_amount) " +
           "FROM accb.accb_trnsctn_details a " +
           "WHERE(a.trns_status='" + ispsted + "' and a.accnt_id = " +
           accntid + " and (to_timestamp(a.trnsctn_date,'YYYY-MM-DD HH24:MI:SS') " +
           "between to_timestamp('" + strDte + "','YYYY-MM-DD HH24:MI:SS')" +
             " AND to_timestamp('" + endDte + "','YYYY-MM-DD HH24:MI:SS')))";
            DataSet dtst = this.selectDataNoParams(strSql);
            double res = 0;
            if (dtst.Tables[0].Rows.Count > 0)
            {
                double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out res);
            }
            return res;
        }

        public long getTrnsDteOpenPrdLnID(long prdHdrID, string trnsDte)
        {
            string strSql = "SELECT a.period_det_id " +
             "FROM accb.accb_periods_det a " +
             "WHERE((a.period_hdr_id = " + prdHdrID +
             ") and (a.period_status='Open') and (to_timestamp('" + trnsDte + "','YYYY-MM-DD HH24:MI:SS') " +
         @"between to_timestamp(a.period_start_date,'YYYY-MM-DD HH24:MI:SS')
       and to_timestamp(a.period_end_date,'YYYY-MM-DD HH24:MI:SS')))";
            DataSet dtst = this.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count == 1)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public long getPrdHdrID(int orgId)
        {
            string strSql = "SELECT a.periods_hdr_id " +
             "FROM accb.accb_periods_hdr a " +
             "WHERE(a.use_periods_for_org = '1' and a.org_id = " + orgId + ")";
            DataSet dtst = this.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count == 1)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public long getActiveBdgtID(int orgId)
        {
            string strSql = "SELECT a.budget_id " +
             "FROM accb.accb_budget_header a " +
             "WHERE(a.is_the_active_one = '1' and a.org_id = " + orgId + ")";
            DataSet dtst = this.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count == 1)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public string getAcntsBdgtdAmnt(long bdgtID, int accntID, string strtdate, string enddate)
        {
            strtdate = DateTime.ParseExact(
         strtdate, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

            enddate = DateTime.ParseExact(
         enddate, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

            string strSql = "SELECT a.limit_amount " +
             "FROM accb.accb_budget_details a " +
             "WHERE((a.budget_id = " + bdgtID +
             ") and (a.accnt_id = " + accntID + ") and (a.start_date = '" + strtdate + "')" +
             " and (a.end_date = '" + enddate + "'))";
            DataSet dtst = this.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count == 1)
            {
                return dtst.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "0.00";
            }
        }

        public double getAcntsBdgtdAmnt(long bdgtID, int accntID, string trnsdate)
        {
            trnsdate = DateTime.ParseExact(
         trnsdate, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

            string strSql = "SELECT a.limit_amount " +
             "FROM accb.accb_budget_details a " +
             "WHERE((a.budget_id = " + bdgtID +
             ") and (a.accnt_id = " + accntID + ") and (to_timestamp('" + trnsdate +
             "','YYYY-MM-DD HH24:MI:SS') between to_timestamp(a.start_date,'YYYY-MM-DD HH24:MI:SS')" +
             " AND to_timestamp(a.end_date,'YYYY-MM-DD HH24:MI:SS')))";
            DataSet dtst = this.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count == 1)
            {
                return double.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0.00;
            }
        }

        public string getAcntsBdgtLmtActn(long bdgtID, int accntID, string trnsdate)
        {
            trnsdate = DateTime.ParseExact(
         trnsdate, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            string strSql = "SELECT a.action_if_limit_excded " +
             "FROM accb.accb_budget_details a " +
             "WHERE((a.budget_id = " + bdgtID +
             ") and (a.accnt_id = " + accntID + ") and (to_timestamp('" + trnsdate +
             "','YYYY-MM-DD HH24:MI:SS') between to_timestamp(a.start_date,'YYYY-MM-DD HH24:MI:SS')" +
             " AND to_timestamp(a.end_date,'YYYY-MM-DD HH24:MI:SS')))";
            DataSet dtst = this.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count == 1)
            {
                return dtst.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "None";
            }
        }

        public string getAcntsBdgtStrtDte(long bdgtID, int accntID, string trnsdate)
        {
            trnsdate = DateTime.ParseExact(
         trnsdate, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            string strSql = "SELECT to_char(to_timestamp(a.start_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') " +
             "FROM accb.accb_budget_details a " +
             "WHERE((a.budget_id = " + bdgtID +
             ") and (a.accnt_id = " + accntID + ") and (to_timestamp('" + trnsdate +
             "','YYYY-MM-DD HH24:MI:SS') between to_timestamp(a.start_date,'YYYY-MM-DD HH24:MI:SS')" +
             " AND to_timestamp(a.end_date,'YYYY-MM-DD HH24:MI:SS')))";
            DataSet dtst = this.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count == 1)
            {
                return dtst.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return DateTime.ParseExact(
            this.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
            System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy 00:00:00");
            }
        }

        public string getAcntsBdgtEndDte(long bdgtID, int accntID, string trnsdate)
        {
            trnsdate = DateTime.ParseExact(
         trnsdate, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            string strSql = "SELECT to_char(to_timestamp(a.end_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') " +
             "FROM accb.accb_budget_details a " +
             "WHERE((a.budget_id = " + bdgtID +
             ") and (a.accnt_id = " + accntID + ") and (to_timestamp('" + trnsdate +
             "','YYYY-MM-DD HH24:MI:SS') between to_timestamp(a.start_date,'YYYY-MM-DD HH24:MI:SS')" +
             " AND to_timestamp(a.end_date,'YYYY-MM-DD HH24:MI:SS')))";
            DataSet dtst = this.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count == 1)
            {
                return dtst.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return DateTime.ParseExact(
            this.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
            System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy 23:59:59");
            }
        }

        public string getLastPrdClseDate()
        {
            string strSql = "SELECT to_char(to_timestamp(period_close_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') " +
         "FROM accb.accb_period_close_dates " +
         "WHERE org_id = " + this.Org_id +
         " ORDER BY period_close_id DESC LIMIT 1 OFFSET 0";
            DataSet dtst = this.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return dtst.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "01-Jan-1900 00:00:00";
            }
        }

        public string getLtstPrdStrtDate()
        {
            string strSql = "SELECT b.pssbl_value " +
             "FROM gst.gen_stp_lov_names a, gst.gen_stp_lov_values b " +
             "WHERE(a.value_list_id = b.value_list_id and b.is_enabled = '1'" +
             " and  a.value_list_name= 'Transactions Date Limit 1') " +
             "ORDER BY b.pssbl_value_id DESC LIMIT 1 OFFSET 0";
            DataSet dtst = this.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                string rs = dtst.Tables[0].Rows[0][0].ToString();
                if (rs.Length <= 11)
                {
                    rs = rs + " 00:00:00";
                }
                return rs;
            }
            else
            {
                return DateTime.ParseExact(
            this.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
            System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy 00:00:00");
            }
        }

        public string getLtstOpenPrdAfterDate(string trnsDate)
        {
            string strSql = "SELECT a.period_start_date " +
             "FROM accb.accb_periods_det a " +
             "WHERE(a.period_start_date >='" + trnsDate + "' and a.period_status ='Open') ORDER BY a.period_start_date ASC LIMIT 1 OFFSET 0 ";
            DataSet dtst = this.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                string rs = dtst.Tables[0].Rows[0][0].ToString();
                return rs;
            }
            else
            {
                return DateTime.ParseExact(
            this.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
            System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy 00:00:00");
            }
        }

        public string getLtstPrdEndDate()
        {
            string strSql = "SELECT b.pssbl_value " +
             "FROM gst.gen_stp_lov_names a, gst.gen_stp_lov_values b " +
             "WHERE(a.value_list_id = b.value_list_id and b.is_enabled = '1'" +
             " and  a.value_list_name= 'Transactions Date Limit 2') " +
             "ORDER BY b.pssbl_value_id DESC LIMIT 1 OFFSET 0";
            DataSet dtst = this.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                string rs = dtst.Tables[0].Rows[0][0].ToString();
                if (rs.Length <= 11)
                {
                    rs = rs + " 23:59:59";
                }
                return rs;
            }
            else
            {
                return DateTime.ParseExact(
            this.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
            System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy 23:59:59");
            }
        }

        public long getUserID(string username)
        {
            //Example username 'admin'
            DataSet dtSt = new DataSet();
            string sqlStr = "select user_id from sec.sec_users where lower(user_name) = '" +
             username.Replace("'", "''").ToLower() + "'";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return Int64.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public long getUserPrsnID(long usrID)
        {
            //Example username 'admin'
            DataSet dtSt = new DataSet();
            string sqlStr = "select person_id from sec.sec_users where user_id = " +
             usrID + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return Int64.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public long getUserCstmrID(long usrID)
        {
            //Example username 'admin'
            DataSet dtSt = new DataSet();
            string sqlStr = "select customer_id from sec.sec_users where user_id = " +
             usrID + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return Int64.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public string getUsername(long usrid)
        {
            //Example username 'admin'
            DataSet dtSt = new DataSet();
            string sqlStr = "select user_name from sec.sec_users where user_id = " +
             usrid + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public int getPrsnOrgID(long usrid)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select a.org_id from prs.prsn_names_nos a, sec.sec_users b " +
             "where a.person_id = b.person_id and b.user_id = " +
             usrid + " and b.person_id>0";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                sqlStr = "select a.org_id from scm.scm_cstmr_suplr a, sec.sec_users b " +
               "where a.cust_sup_id = b.customer_id and b.user_id = " +
               usrid + " and b.customer_id > 0";
                dtSt = this.selectDataNoParams(sqlStr);
                if (dtSt.Tables[0].Rows.Count > 0)
                {
                    return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
                }
                else
                {
                    return -1;
                }
            }
        }

        //public int getPrsnOrgID(long usrid)
        //{
        //  DataSet dtSt = new DataSet();
        //  string sqlStr = "select a.org_id from prs.prsn_names_nos a, sec.sec_users b " +
        //   "where a.person_id = b.person_id and b.user_id = " +
        //   usrid + "";
        //  dtSt = this.selectDataNoParams(sqlStr);
        //  if (dtSt.Tables[0].Rows.Count > 0)
        //  {
        //    return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
        //  }
        //  else
        //  {
        //    return -1;
        //  }
        //}

        public string getLatestPrsnType(long prsnid)
        {
            string selSQL = "SELECT prsn_type " +
                  "FROM pasn.prsn_prsntyps WHERE ((person_id = " + prsnid +
                  ") and (now() between to_timestamp(valid_start_date || ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
                  "AND to_timestamp(valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS')))";
            DataSet dtst = this.selectDataNoParams(selSQL);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return dtst.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public long getPrsnID(string locid)
        {
            //Example username 'admin'
            DataSet dtSt = new DataSet();
            string sqlStr = "select a.person_id from prs.prsn_names_nos a where a.local_id_no = '" +
             locid.Replace("'", "''") + "'";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public string getPrsnName(long prsnid)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select trim(a.title || ' ' || a.sur_name || " +
              "', ' || a.first_name || ' ' || a.other_names) fullname from prs.prsn_names_nos a where a.person_id = " +
             prsnid + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public string getPrsnSurNameFrst(long prsnid)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select REPLACE(trim(a.sur_name || " +
              "' ' || a.first_name || ' ' || a.other_names || ' ' || a.title ),'  ',' ') fullname from prs.prsn_names_nos a where a.person_id = " +
             prsnid + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public string getPrsnLocID(long prsnid)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select local_id_no from prs.prsn_names_nos a where a.person_id = " +
             prsnid + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public long getPrsnLnkdFirmID(long prsnid)
        {
            //Example username 'admin'
            DataSet dtSt = new DataSet();
            string sqlStr = "select a.lnkd_firm_org_id from prs.prsn_names_nos a where a.person_id = " +
             prsnid + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public string getCstmrSpplrName(long cstSupID)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select cust_sup_name from scm.scm_cstmr_suplr a where a.cust_sup_id = " +
             cstSupID + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public string getCstmrSpplrEmails(long cstmrID)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select string_agg(a.email,',') from scm.scm_cstmr_suplr_sites a where a.cust_supplier_id = " +
             cstmrID + " and a.email IS NOT NULL and a.email !=''";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }
        public string getPrsnEmail(long prsnid)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select a.email from prs.prsn_names_nos a where a.person_id = " +
             prsnid + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public string getCstmrSpplrMobiles(long cstmrID)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select string_agg(a.contact_nos,',') from scm.scm_cstmr_suplr_sites a where a.cust_supplier_id = " +
             cstmrID + " and a.contact_nos IS NOT NULL and a.contact_nos !=''";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public string getPrsnMobile(long prsnid)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select a.cntct_no_mobl || ';' || a.cntct_no_tel from prs.prsn_names_nos a where a.person_id = " +
             prsnid + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public string getPrsnName(string locid)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select trim(a.title || ' ' || a.sur_name || " +
              "', ' || a.first_name || ' ' || a.other_names) fullname from prs.prsn_names_nos a where a.local_id_no = '" +
             locid + "'";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public string getJobName(int jobid)
        {
            //Example username 'admin'
            DataSet dtSt = new DataSet();
            string sqlStr = "select job_code_name from org.org_jobs where job_id = " +
             jobid + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public string getGrdName(int grdid)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select grade_code_name from org.org_grades where grade_id = " +
             grdid + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public int getGrdID(string grdname, int orgid)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select grade_id from org.org_grades where lower(grade_code_name) = '" +
             grdname.Replace("'", "''").ToLower() + "' and org_id = " + orgid;
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public string getPrsStName(int prsstid)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select prsn_set_hdr_name from pay.pay_prsn_sets_hdr where prsn_set_hdr_id = " +
             prsstid + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public string getPrsStSQL(int prsstid)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select sql_query from pay.pay_prsn_sets_hdr where prsn_set_hdr_id = " +
             prsstid + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public int getPrsStID(string prsstname, int orgid)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select prsn_set_hdr_id from pay.pay_prsn_sets_hdr where lower(prsn_set_hdr_name) = '" +
             prsstname.Replace("'", "''").ToLower() + "' and org_id = " + orgid;
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public string getItmStName(int itmstid)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select itm_set_name from pay.pay_itm_sets_hdr where hdr_id = " +
             itmstid + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public int getItmStID(string itmstname, int orgid)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select hdr_id from pay.pay_itm_sets_hdr where lower(itm_set_name) = '" +
             itmstname.Replace("'", "''").ToLower() + "' and org_id = " + orgid;
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public string getMsPyName(long mspyid)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select mass_pay_name from pay.pay_mass_pay_run_hdr where mass_pay_id = " +
             mspyid + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public long getMsPyID(string mspyname, int orgid)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select mass_pay_id from pay.pay_mass_pay_run_hdr where lower(mass_pay_name) = '" +
             mspyname.Replace("'", "''").ToLower() + "' and org_id = " + orgid;
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public double getItmValueAmnt(long itmvalid)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select pssbl_amount from org.org_pay_items_values where pssbl_value_id = " +
             itmvalid + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return double.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0.00;
            }
        }

        public string getItmValSQL(long itmvalid)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select pssbl_value_sql from org.org_pay_items_values where pssbl_value_id = " +
             itmvalid + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public string getItmValName(long itmvalid)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select pssbl_value_code_name from org.org_pay_items_values where pssbl_value_id = " +
             itmvalid + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public long getPrsBalItmID(int orgid)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select item_id from org.org_pay_items where is_take_home_pay = '1' and org_id = " + orgid;
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public string getItmName(long itmid)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select item_code_name from org.org_pay_items where item_id = " +
             itmid + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public long getItmID(string itmname, int orgid)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select item_id from org.org_pay_items where lower(item_code_name) = '" +
             itmname.Replace("'", "''").ToLower() + "' and org_id = " + orgid;
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public long getInvItmID(string itmname, int orgid)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select item_id from inv.inv_itm_list where lower(item_code) = '" +
             itmname.Replace("'", "''").ToLower() + "' and org_id = " + orgid;
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public string getItmMinType(long itmid)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select item_min_type from org.org_pay_items where item_id = " +
             itmid + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public string getItmMajType(long itmid)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select item_maj_type from org.org_pay_items where item_id = " +
             itmid + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public long getItmValID(string itmvalname, long itmid)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select pssbl_value_id from org.org_pay_items_values where lower(pssbl_value_code_name) = '" +
              itmvalname.Replace("'", "''").ToLower() + "' and item_id = " + itmid;
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public double exctItmValSQL(string itemSQL, long prsn_id, int org_id, string dateStr)
        {
            dateStr = DateTime.ParseExact(
         dateStr, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            if (dateStr.Length > 10)
            {
                dateStr = dateStr.Substring(0, 10);
            }
            DataSet dtSt = new DataSet();
            string nwSQL = itemSQL.Replace("{:person_id}", prsn_id.ToString()).Replace("{:org_id}", org_id.ToString()).Replace("{:pay_date}", dateStr);
            dtSt = this.selectDataNoParams(nwSQL);
            // this.showSQLNoPermsn(nwSQL);

            if (dtSt.Tables[0].Rows.Count > 0)
            {
                try
                {
                    return double.Parse(dtSt.Tables[0].Rows[0][0].ToString());
                }
                catch (Exception ex)
                {
                    return 0.00;
                }
            }
            else
            {
                return 0.00;
            }
        }

        public bool isItmValSQLValid(string itemSQL, long prsn_id, int org_id, string dateStr)
        {
            dateStr = DateTime.ParseExact(
         dateStr, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            if (dateStr.Length > 10)
            {
                dateStr = dateStr.Substring(0, 10);
            }
            DataSet dtSt = new DataSet();
            string nwSQL = itemSQL.Replace("{:person_id}", prsn_id.ToString()).Replace("{:org_id}", org_id.ToString()).Replace("{:pay_date}", dateStr);
            try
            {
                dtSt = this.selectDataNoParams(nwSQL);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string getPosName(int posid)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select position_code_name from org.org_positions where position_id = " +
             posid + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public int getPosID(string posname, int orgid)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select position_id from org.org_positions where lower(position_code_name) = '" +
             posname.Replace("'", "''").ToLower() + "' and org_id = " + orgid;
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public string getGathName(int gthid)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select gthrng_typ_name from org.org_gthrng_types where gthrng_typ_id = " +
             gthid + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public int getGathID(string gthname, int orgid)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select gthrng_typ_id from org.org_gthrng_types where lower(gthrng_typ_name) = '" +
             gthname.Replace("'", "''").ToLower() + "' and org_id = " + orgid;
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public string getWkhName(int wkhid)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select work_hours_name from org.org_wrkn_hrs where work_hours_id = " +
             wkhid + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public int getWkhID(string wkhname, int orgid)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select work_hours_id from org.org_wrkn_hrs where lower(work_hours_name) = '" +
             wkhname.Replace("'", "''").ToLower() + "' and org_id = " + orgid;
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public int getWkhDetID(string wkhDayname, int wkhrID)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select dflt_row_id from org.org_wrkn_hrs_details where lower(day_of_week) = '" +
             wkhDayname.Replace("'", "''").ToLower() + "' and work_hours_id = " + wkhrID;
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public int getAsgnTmpltID(string asgnTmpltNm, int OrgID)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select tmplt_id from pasn.prsn_assgnmnt_tmplts where lower(tmplt_name) = '" +
             asgnTmpltNm.Replace("'", "''").ToLower() + "' and org_id = " + OrgID;
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public int getOrgID(string orgname)
        {
            //Example username 'admin'
            DataSet dtSt = new DataSet();
            string sqlStr = "select org_id from org.org_details where lower(org_name) = '" +
             orgname.Replace("'", "''").ToLower() + "'";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public int getGrpOrgID()
        {
            //Example username 'admin'
            DataSet dtSt = new DataSet();
            string sqlStr = "select MIN(org_id) from org.org_details where parent_org_id<=0";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public int getJobID(string jobname, int orgid)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select job_id from org.org_jobs where lower(job_code_name) = '" +
             jobname.Replace("'", "''").ToLower() + "' and org_id = " + orgid;
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public string getOrgName(int orgid)
        {
            //Example username 'admin'
            DataSet dtSt = new DataSet();
            string sqlStr = "select org_name from org.org_details where org_id = " +
             orgid + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public string getOrgPstlAddrs(int orgid)
        {
            //Example username 'admin'
            DataSet dtSt = new DataSet();
            string sqlStr = "select pstl_addrs from org.org_details where org_id = " +
             orgid + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public string getOrgEmailAddrs(int orgid)
        {
            //Example username 'admin'
            DataSet dtSt = new DataSet();
            string sqlStr = "select email_addrsses from org.org_details where org_id = " +
             orgid + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public string getOrgContactNos(int orgid)
        {
            //Example username 'admin'
            DataSet dtSt = new DataSet();
            string sqlStr = "select cntct_nos from org.org_details where org_id = " +
             orgid + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public string getOrgWebsite(int orgid)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select websites from org.org_details where org_id = " +
             orgid + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public string getOrgSlogan(int orgid)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select org_slogan from org.org_details where org_id = " +
             orgid + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public int getOrgFuncCurID(int orgid)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select oprtnl_crncy_id from org.org_details where org_id = " +
             orgid + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public long getRptID(string rptname)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select report_id from rpt.rpt_reports where lower(report_name) = '" +
             rptname.Replace("'", "''").ToLower() + "'";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public string getRptName(long rptid)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select report_name from rpt.rpt_reports where report_id = " +
             rptid + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public int getDivID(string divname, int orgid)
        {
            //Example username 'admin'
            DataSet dtSt = new DataSet();
            string sqlStr = "select div_id from org.org_divs_groups where lower(div_code_name) = '" +
             divname.Replace("'", "''").ToLower() + "' and org_id = " + orgid;
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public string getDivName(int divid)
        {
            //Example username 'admin'
            DataSet dtSt = new DataSet();
            string sqlStr = "select div_code_name from org.org_divs_groups where div_id = " +
             divid + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public int getBdgtID(string bdgtname, int orgid)
        {
            //Example username 'admin'
            DataSet dtSt = new DataSet();
            string sqlStr = "select budget_id from accb.accb_budget_header where lower(budget_name) = '" +
             bdgtname.Replace("'", "''").ToLower() + "' and org_id = " + orgid;
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public string getBdgtName(int bdgtid)
        {
            //Example username 'admin'
            DataSet dtSt = new DataSet();
            string sqlStr = "select budget_name from accb.accb_budget_header where budget_id = " +
             bdgtid + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public long getTrnsBatchID(string batchname, int orgid)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select batch_id from accb.accb_trnsctn_batches where lower(batch_name) = '" +
             batchname.Replace("'", "''").ToLower() + "' and org_id = " + orgid;
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public string getTrnsBatchName(int batchid)
        {
            //Example username 'admin'
            DataSet dtSt = new DataSet();
            string sqlStr = "select batch_name from accb.accb_trnsctn_batches where batch_id = " +
             batchid + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public int getAccntID(string accntname, int orgid)
        {
            //Example username 'admin'
            DataSet dtSt = new DataSet();
            string sqlStr = "select accnt_id from accb.accb_chart_of_accnts where ((lower(accnt_name) = '" +
             accntname.Replace("'", "''").ToLower() + "' or lower(accnt_num) = '" +
             accntname.Replace("'", "''").ToLower() + "') and org_id = " + org_id + ")";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public string getAccntName(int accntid)
        {
            //Example username 'admin'
            DataSet dtSt = new DataSet();
            string sqlStr = "select accnt_name from accb.accb_chart_of_accnts where accnt_id = " +
             accntid + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public string getAccntNum(int accntid)
        {
            //Example username 'admin'
            DataSet dtSt = new DataSet();
            string sqlStr = "select accnt_num from accb.accb_chart_of_accnts where accnt_id = " +
             accntid + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public string getSegmentValDesc(int segmentValID)
        {
            //Example username 'admin'
            DataSet dtSt = new DataSet();
            string sqlStr = "select segment_description from org.org_segment_values where segment_value_id = " +
             segmentValID + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public string getSegmentVal(int segmentValID)
        {
            //Example username 'admin'
            DataSet dtSt = new DataSet();
            string sqlStr = "select segment_value from org.org_segment_values where segment_value_id = " +
             segmentValID + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public string getAccntType(int accntid)
        {
            //Example username 'admin'
            DataSet dtSt = new DataSet();
            string sqlStr = "select accnt_type from accb.accb_chart_of_accnts where accnt_id = " +
             accntid + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }
        public string getSgmntValAccntType(int sgmntValID)
        {
            //Example username 'admin'
            DataSet dtSt = new DataSet();
            string sqlStr = "select accnt_type from org.org_segment_values where segment_value_id = " +
             sgmntValID + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }
        public string isAccntContra(int accntid)
        {
            //Example username 'admin'
            DataSet dtSt = new DataSet();
            string sqlStr = "select is_contra from accb.accb_chart_of_accnts where accnt_id = " +
             accntid + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public int getSiteID(string sitename, int orgid)
        {
            //Example username 'admin'
            DataSet dtSt = new DataSet();
            string sqlStr = "select location_id from org.org_sites_locations where lower(location_code_name) = '" +
             sitename.Replace("'", "''").ToLower() + "' and org_id = " + orgid;
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public string getSiteName(int siteid)
        {
            //Example username 'admin'
            DataSet dtSt = new DataSet();
            string sqlStr = "select location_code_name from org.org_sites_locations where location_id = " +
             siteid + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public long getMdlGrpID(string sub_grp_name)
        {
            //Example priviledge 'View Security Module'
            DataSet dtSt = new DataSet();
            string sqlStr = "SELECT table_id from sec.sec_module_sub_groups where (sub_group_name = '" +
             sub_grp_name.Replace("'", "''") + "' AND module_id = " +
             this.getModuleID(this.ModuleName) + ")";
            //this.showSQLNoPermsn(sqlStr);
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public long getMdlGrpTblID(string sub_grp_name, int mdlID)
        {
            //Example priviledge 'View Security Module'
            DataSet dtSt = new DataSet();
            string sqlStr = "SELECT table_id from sec.sec_module_sub_groups where (sub_group_name = '" +
             sub_grp_name.Replace("'", "''") + "' AND module_id = " +
             mdlID + ")";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public int getLovID(string lovName)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "SELECT value_list_id from gst.gen_stp_lov_names where (value_list_name = '" +
             lovName.Replace("'", "''") + "')";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public int getEnbldLovID(string lovName)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "SELECT value_list_id from gst.gen_stp_lov_names where (upper(value_list_name) = upper('" +
             lovName.Replace("'", "''") + "') and is_enabled='1')";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public int getEnbldPssblValID(string pssblVal, int lovID)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "SELECT pssbl_value_id from gst.gen_stp_lov_values " +
             "where ((upper(pssbl_value) = upper('" +
             pssblVal.Replace("'", "''") + "')) AND (value_list_id = " + lovID +
             ") AND (is_enabled='1')) ORDER BY pssbl_value_id LIMIT 1";
            dtSt = this.selectDataNoParams(sqlStr);
            //this.showSQLNoPermsn(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public static bool isEmailValid(string emailString)
        {
            return Regex.IsMatch(emailString, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }

        public string getEnbldPssblValDesc(string pssblVal, int lovID)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "SELECT pssbl_value_desc from gst.gen_stp_lov_values " +
             "where ((upper(pssbl_value) = upper('" +
             pssblVal.Replace("'", "''") + "')) AND (value_list_id = " + lovID +
             ") AND (is_enabled='1')) ORDER BY pssbl_value_id LIMIT 1";
            dtSt = this.selectDataNoParams(sqlStr);
            //this.showSQLNoPermsn(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public string getLovNm(int lovID)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "SELECT value_list_name from gst.gen_stp_lov_names " +
              "where (value_list_id = " + lovID + ")";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public int getPssblValID(string pssblVal, int lovID)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "SELECT pssbl_value_id from gst.gen_stp_lov_values " +
             "where ((pssbl_value = '" +
             pssblVal.Replace("'", "''") + "') AND (value_list_id = " + lovID + ")) ORDER BY pssbl_value_id LIMIT 1";
            dtSt = this.selectDataNoParams(sqlStr);
            //this.showSQLNoPermsn(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public int getPssblValID(string pssblVal, int lovID, string pssblValDesc)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "SELECT pssbl_value_id from gst.gen_stp_lov_values " +
             "where ((pssbl_value = '" +
             pssblVal.Replace("'", "''") + "') AND (pssbl_value_desc = '" +
              pssblValDesc.Replace("'", "''") + "') AND (value_list_id = " + lovID + "))";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public string getPssblValNm(int pssblVlID)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "SELECT pssbl_value from gst.gen_stp_lov_values " +
             "where ((pssbl_value_id = " + pssblVlID + "))";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public string getPssblValDesc(int pssblVlID)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "SELECT pssbl_value_desc from gst.gen_stp_lov_values " +
             "where ((pssbl_value_id = " + pssblVlID + "))";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public int getPrvldgID(string prvldg_name)
        {
            //Example priviledge 'View Security Module'
            DataSet dtSt = new DataSet();
            string sqlStr = "SELECT prvldg_id from sec.sec_prvldgs where (prvldg_name = '" +
             prvldg_name.Replace("'", "''") + "' AND module_id = " +
             this.getModuleID(this.ModuleName) + ")";
            //MessageBox.Show(this.ModuleName);
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public bool hasRoleEvrHdThsPrvlg(int inp_role_id, int inp_prvldg_id)
        {
            //Checks whether a given role 'system administrator' has a given priviledge
            DataSet dtSt = new DataSet();
            string sqlStr = "SELECT role_id FROM sec.sec_roles_n_prvldgs WHERE ((prvldg_id = " +
                inp_prvldg_id + ") AND (role_id = " + inp_role_id + "))";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int getRoleID(string rolename)
        {
            //Example user role 'System Administrator'
            DataSet dtSt = new DataSet();
            string sqlStr = "select role_id from sec.sec_roles where role_name = '" +
             rolename.Replace("'", "''") + "'";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public int getModuleID(string mdl_name)
        {
            //Example module name 'Security'
            DataSet dtSt = new DataSet();
            string sqlStr = "select module_id from sec.sec_modules where module_name = '" +
             mdl_name.Replace("'", "''") + "'";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public string getModuleName(int mdlid)
        {
            //Example module name 'Security'
            DataSet dtSt = new DataSet();
            string sqlStr = "select module_name from sec.sec_modules where module_id = " +
             mdlid + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public int getTrnsTmpltID(string tmpltname, int orgid)
        {
            //Example username 'admin'
            DataSet dtSt = new DataSet();
            string sqlStr = "select template_id from accb.accb_trnsctn_templates_hdr where lower(template_name) = '" +
             tmpltname.Replace("'", "''").ToLower() + "' and org_id = " + orgid;
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public string getTrnsTmpltName(int tmpltid)
        {
            //Example username 'admin'
            DataSet dtSt = new DataSet();
            string sqlStr = "select template_name from accb.accb_trnsctn_templates_hdr where template_id = " +
             tmpltid + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }
        public bool doesCrPlcTrckThisActn(string actionTyp)
        {
            // Checks whether the current policy tracks a particular action in the current module
            string sqlStr = "SELECT policy_id FROM " +
         "sec.sec_audit_trail_tbls_to_enbl WHERE ((policy_id = " + this.getCurPlcyID() +
         ") AND (module_id = " + this.getModuleID(this.ModuleName) +
         ") AND (action_typs_to_track ilike '%" +
         actionTyp + "%') AND (enable_tracking = TRUE))";
            DataSet dtSt = new DataSet();
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool doesRoleHvThisPrvldg(int inp_role_id, int inp_prvldg_id)
        {
            //Checks whether a given role 'system administrator' has a given priviledge
            DataSet dtSt = new DataSet();
            string sqlStr = "SELECT role_id FROM sec.sec_roles_n_prvldgs WHERE ((prvldg_id = " +
                inp_prvldg_id + ") AND (role_id = " + inp_role_id +
                ") AND (now() between to_timestamp(valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
                  "AND to_timestamp(valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS')))";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool doesPrsnHvThisAccnt(long inp_prsn_id, int inp_accnt_id)
        {
            //Checks whether a given role 'system administrator' has a given priviledge
            DataSet dtSt = new DataSet();
            string sqlStr = "SELECT org.does_prsn_hv_accnt_id(" + inp_prsn_id + "," + inp_accnt_id + ")";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return (long.Parse(dtSt.Tables[0].Rows[0][0].ToString()) > 0);
            }
            else
            {
                return false;
            }
        }
        public bool doCurRolesHvThsPrvldgs(string[] prvldgnames)
        {
            bool[] chkRslts = new bool[prvldgnames.Length];
            for (int m = 0; m < chkRslts.Length; m++)
            {
                chkRslts[m] = false;
            }
            for (int i = 0; i < this.Role_Set_IDs.Length; i++)
            {
                for (int j = 0; j < prvldgnames.Length; j++)
                {
                    if (this.doesRoleHvThisPrvldg(this.Role_Set_IDs[i],
                     this.getPrvldgID(prvldgnames[j])) == true)
                    {
                        chkRslts[j] = true;
                    }
                }
            }
            for (int n = 0; n < chkRslts.Length; n++)
            {
                if (chkRslts[n] == false)
                {
                    return false;
                }
            }
            return true;
        }

        public bool doesPswdCmplxtyMeetPlcy(string pswd, string uname)
        {
            //Checks Whether a password meets the current password complexity policy
            int rqrmnts_met = 0;
            if (pswd.Length < this.get_CurPlcy_Min_Pwd_Len() ||
             pswd.Length > this.get_CurPlcy_Mx_Pwd_Len())
            {
                MessageBox.Show("Length of Password does not\nmeet current Security Policy!");
                return false;
            }
            if (this.allowUnameInPswd() == false)
            {
                if (pswd.ToLower().Contains(uname.ToLower()))
                {
                    MessageBox.Show("Password contains user name!");
                    return false;
                }
            }
            bool allwRpeatng = this.allowRepeatngChars();
            char[] pwd_arry = pswd.ToCharArray();
            bool seenCaps = false;
            bool seenSmall = false;
            bool seenDigit = false;
            bool seenWild = false;
            for (int i = 0; i < pwd_arry.Length; i++)
            {
                if (allwRpeatng == false && i > 0)
                {
                    if (pwd_arry[i] == pwd_arry[i - 1])
                    {
                        MessageBox.Show("Password contains Repeating Characters!");
                        return false;
                    }
                }
                if (Char.IsLetter(pwd_arry[i]))
                {
                    if (Char.IsLower(pwd_arry[i]) && this.isSmallLtrRequired() == true
                     && seenSmall == false)
                    {
                        rqrmnts_met += 1;
                        seenSmall = true;
                        continue;
                    }
                    if (Char.IsUpper(pwd_arry[i]) && this.isCapsRequired() == true
                     && seenCaps == false)
                    {
                        rqrmnts_met += 1;
                        seenCaps = true;
                        continue;
                    }
                }
                else if (Char.IsDigit(pwd_arry[i]) && this.isDigitRequired() == true
                 && seenDigit == false)
                {
                    rqrmnts_met += 1;
                    seenDigit = true;
                    continue;
                }
                else if (Char.IsLetterOrDigit(pwd_arry[i]) == false && this.isWildCharRequired() == true
                 && seenWild == false)
                {
                    rqrmnts_met += 1;
                    seenWild = true;
                    continue;
                }
            }
            if (this.get_CrPlc_Rqrmt_Cmbntn() == "NONE" || this.get_CrPlc_Rqrmt_Cmbntn() == "")
            {
                return true;
            }
            else if (this.get_CrPlc_Rqrmt_Cmbntn() == "ALL 4" && rqrmnts_met >= 4)
            {
                return true;
            }
            else if (this.get_CrPlc_Rqrmt_Cmbntn() == "ANY 3" && rqrmnts_met >= 3)
            {
                return true;
            }
            else if (this.get_CrPlc_Rqrmt_Cmbntn() == "ANY 2" && rqrmnts_met >= 2)
            {
                return true;
            }
            else if (this.get_CrPlc_Rqrmt_Cmbntn() == "ANY 1" && rqrmnts_met >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool isCapsRequired()
        {
            //Checks Whether caps is required in a password
            string sqlStr = "SELECT pswd_require_caps FROM sec.sec_security_policies " +
             "WHERE is_default = 't'";
            DataSet dtSt = new DataSet();
            dtSt = this.selectDataNoParams(sqlStr);
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

        public bool isSmallLtrRequired()
        {
            //Checks Whether small letter is required in a password
            string sqlStr = "SELECT pswd_require_small FROM sec.sec_security_policies " +
             "WHERE is_default = 't'";
            DataSet dtSt = new DataSet();
            dtSt = this.selectDataNoParams(sqlStr);
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

        public bool isDigitRequired()
        {
            //Checks Whether Digit is required in a password
            string sqlStr = "SELECT pswd_require_dgt FROM sec.sec_security_policies " +
             "WHERE is_default = 't'";
            DataSet dtSt = new DataSet();
            dtSt = this.selectDataNoParams(sqlStr);
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

        public bool isWildCharRequired()
        {
            //Checks Whether Wild Character is required in a password
            string sqlStr = "SELECT pswd_require_wild FROM sec.sec_security_policies " +
             "WHERE is_default = 't'";
            DataSet dtSt = new DataSet();
            dtSt = this.selectDataNoParams(sqlStr);
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

        public bool allowUnameInPswd()
        {
            //Checks Whether User name is allowed in a password
            string sqlStr = "SELECT allow_usrname_in_pswds FROM sec.sec_security_policies " +
             "WHERE is_default = 't'";
            DataSet dtSt = new DataSet();
            dtSt = this.selectDataNoParams(sqlStr);
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

        public bool allowRepeatngChars()
        {
            //Checks Whether Repeating Characters are allowed in a password
            string sqlStr = "SELECT allow_repeating_chars FROM sec.sec_security_policies " +
             "WHERE is_default = 't'";
            DataSet dtSt = new DataSet();
            dtSt = this.selectDataNoParams(sqlStr);
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

        public bool doesPwdHvRptngChars(string pwd)
        {
            for (int i = 0; i < pwd.Length; i++)
            {
                if (i > 0)
                {
                    if (pwd.Substring(i, 1) == pwd.Substring((i - 1), 1))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion
        #endregion

        #region "LIST OF VALUES..."
        public bool isVlLstDynamic(int lovID)
        {
            string strSql = "select is_list_dynamic from gst.gen_stp_lov_names " +
             "where value_list_id = " + lovID;
            DataSet dtst = this.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                if (dtst.Tables[0].Rows[0][0].ToString() == "0")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public string getSQLForDynamicVlLst(int lovID)
        {
            string strSql = "select sqlquery_if_dyn from gst.gen_stp_lov_names " +
             "where value_list_id = " + lovID;
            DataSet dtst = this.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return dtst.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        //public DataSet getLovValues(string searchWord, string searchIn,
        //          Int64 offset, int limit_size, ref string brghtsqlStr, int lovID,
        //          ref bool is_dynamic, int criteriaID)
        //{
        //  string strSql = "";
        //  is_dynamic = false;
        //  if (this.isVlLstDynamic(lovID) == true)
        //  {
        //    if (criteriaID <= 0)
        //    {
        //      strSql = "select * from (" + this.getSQLForDynamicVlLst(lovID) + ") tbl1 ORDER BY 1 LIMIT " + limit_size +
        //        " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
        //    }
        //    else
        //    {
        //      strSql = "select tbl1.a,tbl1.b,tbl1.c from (" + this.getSQLForDynamicVlLst(lovID) +
        //       ") tbl1 WHERE tbl1.d = " + criteriaID + " ORDER BY 1 LIMIT " + limit_size +
        //        " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
        //    }
        //    is_dynamic = true;
        //  }
        //  else
        //  {
        //    if (searchIn == "Value")
        //    {
        //      strSql = "SELECT pssbl_value, pssbl_value_desc, pssbl_value_id " +
        //       "FROM gst.gen_stp_lov_values WHERE ((is_enabled != '0') AND (pssbl_value ilike '" +
        //       searchWord.Replace("'", "''") + "') AND (value_list_id = " + lovID + ")) ORDER BY pssbl_value LIMIT " + limit_size +
        //       " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
        //    }
        //    else if (searchIn == "Description")
        //    {
        //      strSql = "SELECT pssbl_value, pssbl_value_desc, pssbl_value_id " +
        //       "FROM gst.gen_stp_lov_values WHERE ((is_enabled != '0') AND (pssbl_value_desc ilike '" +
        //       searchWord.Replace("'", "''") + "') AND (value_list_id = " + lovID + ")) ORDER BY pssbl_value_desc LIMIT " + limit_size +
        //       " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
        //    }
        //  }
        //  DataSet dtst = this.selectDataNoParams(strSql);
        //  brghtsqlStr = strSql;
        //  return dtst;
        //}

        public string[] checkNGetLOVValue(string srchFor, string srchIn, int lovID, int criteriaID,
               string criteriaID2, string criteriaID3, string addtnlWhere)
        {
            string[] rslts = { "-1", "" };
            string vwSQLStmnt = "";
            bool is_dynamic = false;
            DataSet dtst = this.getLovValues(srchFor,
                 srchIn, 0, 10, ref vwSQLStmnt,
                 lovID, ref is_dynamic, criteriaID,
                 criteriaID2, criteriaID3, addtnlWhere);
            if (dtst.Tables[0].Rows.Count == 1)
            {
                rslts[0] = dtst.Tables[0].Rows[0][0].ToString();
                rslts[1] = dtst.Tables[0].Rows[0][1].ToString();
            }
            return rslts;
        }

        public string checkNFormatDate(string inStr)
        {
            DateTime dte1 = DateTime.Now;
            bool sccs = DateTime.TryParse(inStr, out dte1);
            if (!sccs)
            {
                dte1 = DateTime.Now;
            }
            return dte1.ToString("dd-MMM-yyyy HH:mm:ss");
        }

        public DataSet getLovValues(string searchWord, string searchIn,
          Int64 offset, int limit_size, ref string brghtsqlStr, int lovID,
                  ref bool is_dynamic, int criteriaID, string criteriaID2, string criteriaID3, string addtnlWhere)
        {
            string lovNm = this.getLovNm(lovID);
            string strSql = "";
            is_dynamic = false;
            string extrWhere = "";
            //string addtnlWhere = "";
            string ordrBy = this.getGnrlRecNm("gst.gen_stp_lov_names", "value_list_id", "dflt_order_by", lovID);
            if (ordrBy == "")
            {
                ordrBy = "ORDER BY 1";
            }
            string selLst = "tbl1.a,tbl1.b,tbl1.c";
            if (lovNm == "Report/Process Runs")
            {
                ordrBy = "ORDER BY 6 DESC";
                selLst = "tbl1.a,tbl1.b,tbl1.c,tbl1.d,tbl1.e,tbl1.f";
            }
            if (searchIn == "Value")
            {
                extrWhere = "and tbl1.a ilike '" + searchWord.Replace("'", "''") + "'";
            }
            else if (searchIn == "Description")
            {
                extrWhere = "and tbl1.b ilike '" + searchWord.Replace("'", "''") + "'";
            }
            else
            {
                extrWhere = "and (tbl1.a ilike '" + searchWord.Replace("'", "''") + "' or tbl1.b ilike '" + searchWord.Replace("'", "''") + "')";
            }
            if (this.isVlLstDynamic(lovID) == true)
            {
                if (criteriaID <= 0 && criteriaID2 == "" && criteriaID3 == "")
                {
                    strSql = "select * from (" + this.getSQLForDynamicVlLst(lovID).Replace("{:prsn_id}", this.getUserPrsnID(this.User_id).ToString()) +
                      ") tbl1 WHERE 1=1 " + extrWhere + addtnlWhere + " " + ordrBy + " LIMIT " + limit_size +
                      " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
                }
                else if (criteriaID >= 0 && criteriaID2 == "" && criteriaID3 == "")
                {
                    strSql = "select * from (" + this.getSQLForDynamicVlLst(lovID).Replace("{:prsn_id}", this.getUserPrsnID(this.User_id).ToString()) +
                   ") tbl1 WHERE tbl1.d = " + criteriaID + " " + extrWhere + addtnlWhere + " " + ordrBy + " LIMIT " + limit_size +
                    " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
                }
                else if (criteriaID >= 0 && criteriaID2 != "" && criteriaID3 == "")
                {
                    strSql = "select * from (" + this.getSQLForDynamicVlLst(lovID).Replace("{:prsn_id}", this.getUserPrsnID(this.User_id).ToString()) +
                     ") tbl1 WHERE (tbl1.d = " + criteriaID + " and tbl1.e = '" +
                     criteriaID2.Replace("'", "''") + "' " + extrWhere + addtnlWhere + ") " + ordrBy + " LIMIT " + limit_size +
                      " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
                }
                else if (criteriaID >= 0 && criteriaID2 != "" && criteriaID3 != "")
                {
                    strSql = "select * from (" + this.getSQLForDynamicVlLst(lovID).Replace("{:prsn_id}", this.getUserPrsnID(this.User_id).ToString()) +
                ") tbl1 WHERE (tbl1.d = " + criteriaID + " and tbl1.e = '" +
                criteriaID2.Replace("'", "''") + "' and tbl1.f = '" + criteriaID3.Replace("'", "''") +
                "' " + extrWhere + addtnlWhere + ") " + ordrBy + " LIMIT " + limit_size +
                 " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
                }
                else
                {
                    strSql = "select * from (" + this.getSQLForDynamicVlLst(lovID).Replace("{:prsn_id}", this.getUserPrsnID(this.User_id).ToString()) +
                ") tbl1 WHERE 1=1 " + extrWhere + addtnlWhere + " " + ordrBy + " LIMIT " + limit_size +
                 " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
                }
                is_dynamic = true;
            }
            else
            {
                if (searchIn == "Value")
                {
                    strSql = "SELECT pssbl_value, pssbl_value_desc, pssbl_value_id " +
                     "FROM gst.gen_stp_lov_values WHERE ((is_enabled != '0') AND (pssbl_value ilike '" +
                     searchWord.Replace("'", "''") + "') AND (value_list_id = " + lovID + ")" + addtnlWhere + ") ORDER BY pssbl_value LIMIT " + limit_size +
                     " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
                }
                else if (searchIn == "Description")
                {
                    strSql = "SELECT pssbl_value, pssbl_value_desc, pssbl_value_id " +
                     "FROM gst.gen_stp_lov_values WHERE ((is_enabled != '0') AND (pssbl_value_desc ilike '" +
                     searchWord.Replace("'", "''") + "') AND (value_list_id = " + lovID + ")" + addtnlWhere + ") ORDER BY pssbl_value_desc LIMIT " + limit_size +
                     " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
                }
                else
                {
                    strSql = "SELECT pssbl_value, pssbl_value_desc, pssbl_value_id " +
                     "FROM gst.gen_stp_lov_values WHERE ((is_enabled != '0') AND (pssbl_value ilike '" +
                     searchWord.Replace("'", "''") + "' or pssbl_value_desc ilike '" +
                     searchWord.Replace("'", "''") + "') AND (value_list_id = " + lovID + ")" + addtnlWhere + ") ORDER BY pssbl_value_desc LIMIT " + limit_size +
                     " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
                }
            }
            brghtsqlStr = strSql;
            //this.showSQLNoPermsn(strSql);
            DataSet dtst = this.selectDataNoParams(strSql);
            return dtst;
        }

        public string getFromClauseGoing(string sqlStmnt)
        {
            string reslt = "";
            bool foundStart = false;
            for (int i = 0; i < sqlStmnt.Length; i++)
            {
                if ((i + 4) > sqlStmnt.Length && foundStart == true)
                {
                    reslt = reslt + sqlStmnt.Substring(i, 1);
                }
                else
                {
                    if (sqlStmnt.Substring(i, 4).ToUpper() == "FROM"
                    || foundStart == true)
                    {
                        foundStart = true;
                        reslt = reslt + sqlStmnt.Substring(i, 1);
                    }
                }
            }
            return reslt;
        }

        //public long getTotalLovValues(string searchWord, string searchIn,
        //          int lovID)
        //{
        //  string strSql = "";
        //  if (this.isVlLstDynamic(lovID) == true)
        //  {
        //    strSql = "select count(1) from (" + this.getSQLForDynamicVlLst(lovID) + ") tbl1";
        //  }
        //  else
        //  {
        //    if (searchIn == "Value")
        //    {
        //      strSql = "SELECT count(1) " +
        //       "FROM gst.gen_stp_lov_values WHERE ((is_enabled != '0') AND (pssbl_value ilike '" +
        //       searchWord.Replace("'", "''") + "') AND (value_list_id = " + lovID + "))";
        //    }
        //    else if (searchIn == "Description")
        //    {
        //      strSql = "SELECT count(1) " +
        //       "FROM gst.gen_stp_lov_values WHERE ((is_enabled != '0') AND (pssbl_value_desc ilike '" +
        //       searchWord.Replace("'", "''") + "') AND (value_list_id = " + lovID + "))";
        //    }
        //  }
        //  DataSet dtst = this.selectDataNoParams(strSql);
        //  if (dtst.Tables[0].Rows.Count > 0)
        //  {
        //    return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
        //  }
        //  else
        //  {
        //    return 0;
        //  }
        //}

        public long getTotalLovValues(string searchWord, string searchIn,
              ref string brghtsqlStr, int lovID,
                  ref bool is_dynamic, int criteriaID,
          string criteriaID2, string criteriaID3, string addtnlWhere)
        {
            string strSql = "";
            is_dynamic = false;
            string extrWhere = "";
            if (searchIn == "Value")
            {
                extrWhere = "and tbl1.a ilike '" + searchWord.Replace("'", "''") + "'";
            }
            else
            {
                extrWhere = "and tbl1.b ilike '" + searchWord.Replace("'", "''") + "'";
            }
            if (this.isVlLstDynamic(lovID) == true)
            {
                if (criteriaID <= 0 && criteriaID2 == "" && criteriaID3 == "")
                {
                    strSql = "select count(1) from (" + this.getSQLForDynamicVlLst(lovID).Replace("{:prsn_id}", this.getUserPrsnID(this.User_id).ToString()) +
                      ") tbl1 WHERE 1=1 " + extrWhere + addtnlWhere + "";
                }
                else if (criteriaID >= 0 && criteriaID2 == "" && criteriaID3 == "")
                {
                    strSql = "select count(1) from (" + this.getSQLForDynamicVlLst(lovID).Replace("{:prsn_id}", this.getUserPrsnID(this.User_id).ToString()) +
                   ") tbl1 WHERE tbl1.d = " + criteriaID + " " + extrWhere + addtnlWhere + "";
                }
                else if (criteriaID >= 0 && criteriaID2 != "" && criteriaID3 == "")
                {
                    strSql = "select count(1) from (" + this.getSQLForDynamicVlLst(lovID).Replace("{:prsn_id}", this.getUserPrsnID(this.User_id).ToString()) +
                     ") tbl1 WHERE (tbl1.d = " + criteriaID + " and tbl1.e = '" +
                     criteriaID2.Replace("'", "''") + "' " + extrWhere + addtnlWhere + ")";
                }
                else if (criteriaID >= 0 && criteriaID2 != "" && criteriaID3 != "")
                {
                    strSql = "select count(1) from (" + this.getSQLForDynamicVlLst(lovID).Replace("{:prsn_id}", this.getUserPrsnID(this.User_id).ToString()) +
                ") tbl1 WHERE (tbl1.d = " + criteriaID + " and tbl1.e = '" +
                criteriaID2.Replace("'", "''") + "' and tbl1.f = '" + criteriaID3.Replace("'", "''") +
                "' " + extrWhere + addtnlWhere + ")";
                }
                else
                {
                    strSql = "select count(1) from (" + this.getSQLForDynamicVlLst(lovID).Replace("{:prsn_id}", this.getUserPrsnID(this.User_id).ToString()) +
            ") tbl1 WHERE 1=1 " + extrWhere + addtnlWhere + "";
                }
                is_dynamic = true;
            }
            else
            {
                if (searchIn == "Value")
                {
                    strSql = "SELECT count(1) " +
                     "FROM gst.gen_stp_lov_values WHERE ((is_enabled != '0') AND (pssbl_value ilike '" +
                     searchWord.Replace("'", "''") + "') AND (value_list_id = " + lovID + ")" + addtnlWhere + ")";
                }
                else if (searchIn == "Description")
                {
                    strSql = "SELECT count(1) " +
                     "FROM gst.gen_stp_lov_values WHERE ((is_enabled != '0') AND (pssbl_value_desc ilike '" +
                     searchWord.Replace("'", "''") + "') AND (value_list_id = " + lovID + ")" + addtnlWhere + ")";
                }
                else
                {
                    strSql = "SELECT count(1) " +
                     "FROM gst.gen_stp_lov_values WHERE ((is_enabled != '0') AND (pssbl_value ilike '" +
                     searchWord.Replace("'", "''") + "' or pssbl_value_desc ilike '" +
                     searchWord.Replace("'", "''") + "') AND (value_list_id = " + lovID + ")" + addtnlWhere + ")";
                }
            }
            //this.showSQLNoPermsn(strSql);
            DataSet dtst = this.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }
        #endregion

        #region "ALLOWED EXTRA INFORMATION..."
        public string get_OthInf_Rec_Hstry(long rowID, string tblnm)
        {
            string strSQL = @"SELECT a.created_by, 
to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), 
a.last_update_by, 
to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') " +
            "FROM " + tblnm + " a WHERE(a.dflt_row_id  = " + rowID + ")";
            string fnl_str = "";
            DataSet dtst = this.selectDataNoParams(strSQL);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                fnl_str = "CREATED BY: " + this.get_user_name(long.Parse(dtst.Tables[0].Rows[0][0].ToString())) +
                 "\r\nCREATION DATE: " + dtst.Tables[0].Rows[0][1].ToString() + "\r\nLAST UPDATE BY:" +
                 this.get_user_name(long.Parse(dtst.Tables[0].Rows[0][2].ToString())) +
                 "\r\nLAST UPDATE DATE: " + dtst.Tables[0].Rows[0][3].ToString();
                return fnl_str;
            }
            else
            {
                return "";
            }
        }

        public long getTotalAllwdExtInf(string searchWord, string searchIn,
          long tblID, long row_id_val, string valTbl)
        {
            string strSql = "";
            string whrCls = "";

            if (searchIn == "Value")
            {
                whrCls = " AND (tbl1.othr_inf ilike '" +
                     searchWord.Replace("'", "''") + "' or tbl1.othr_inf IS NULL) ";
            }
            else if (searchIn == "Extra Info Label")
            {
                whrCls = " AND (tbl1.other_info_label ilike '" +
                     searchWord.Replace("'", "''") + "' or tbl1.other_info_category ilike '" +
                     searchWord.Replace("'", "''") + "') ";

            }
            strSql = @"SELECT count(1) FROM (SELECT b.pssbl_value other_info_category, 
          COALESCE((select c.other_info_label from " + valTbl + " c " +
               "where ((c.tbl_othr_inf_combntn_id = a.comb_info_id) AND (c.row_pk_id_val = " + row_id_val + @"))), b.pssbl_value) other_info_label, 
         COALESCE((select c.other_info_value from " + valTbl + " c " +
               "where ((c.tbl_othr_inf_combntn_id = a.comb_info_id) AND (c.row_pk_id_val = " + row_id_val + "))),'') othr_inf, " +
               "a.comb_info_id, a.table_id, COALESCE((select c.dflt_row_id from " + valTbl + " c " +
               "where ((c.tbl_othr_inf_combntn_id = a.comb_info_id) AND (c.row_pk_id_val = " + row_id_val + "))),-1) othr_inf_row_id " +
               "FROM sec.sec_allwd_other_infos a " +
               "LEFT OUTER JOIN gst.gen_stp_lov_values b ON (a.other_info_id = b.pssbl_value_id) " +
               "WHERE((a.is_enabled = '1')  AND (a.table_id = " + tblID + ") AND (b.allowed_org_ids like '%," + this.Org_id.ToString() + ",%') AND (((select c.other_info_value from " + valTbl + " c " +
               "where ((c.tbl_othr_inf_combntn_id = a.comb_info_id) AND (c.row_pk_id_val = " + row_id_val + "))) ilike '" +
               searchWord.Replace("'", "''") + "') OR ((select c.other_info_value from " + valTbl + " c " +
               "where ((c.tbl_othr_inf_combntn_id = a.comb_info_id) AND (c.row_pk_id_val = " + row_id_val + "))) is null))) " +
               @" UNION 
                  SELECT c.other_info_category, c.other_info_label, c.other_info_value othr_inf, 99999999 comb_info_id, -1 table_id, c.dflt_row_id from " + valTbl +
               " c  WHERE c.tbl_othr_inf_combntn_id<=0 and c.row_pk_id_val = " + row_id_val + ") tbl1 WHERE 1=1" + whrCls;

            DataSet dtst = this.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        public DataSet getAllwdExtInfosNVals(string searchWord, string searchIn,
      Int64 offset, int limit_size, ref string brghtsqlStr, long tblID,
          long row_id_val, string valTbl)
        {
            string strSql = "";
            string whrCls = "";

            if (searchIn == "Value")
            {
                whrCls = " AND (tbl1.othr_inf ilike '" +
                     searchWord.Replace("'", "''") + "' or tbl1.othr_inf IS NULL) ";
            }
            else if (searchIn == "Extra Info Label")
            {
                whrCls = " AND (tbl1.other_info_label ilike '" +
                     searchWord.Replace("'", "''") + "' or tbl1.other_info_category ilike '" +
                     searchWord.Replace("'", "''") + "') ";

            }
            strSql = @"SELECT tbl1.* FROM (SELECT b.pssbl_value other_info_category, 
          COALESCE((select c.other_info_label from " + valTbl + " c " +
               "where ((c.tbl_othr_inf_combntn_id = a.comb_info_id) AND (c.row_pk_id_val = " + row_id_val + @"))), b.pssbl_value) other_info_label, 
         COALESCE((select c.other_info_value from " + valTbl + " c " +
               "where ((c.tbl_othr_inf_combntn_id = a.comb_info_id) AND (c.row_pk_id_val = " + row_id_val + "))),'') othr_inf, " +
               "a.comb_info_id, a.table_id, COALESCE((select c.dflt_row_id from " + valTbl + " c " +
               "where ((c.tbl_othr_inf_combntn_id = a.comb_info_id) AND (c.row_pk_id_val = " + row_id_val + "))),-1) othr_inf_row_id " +
               "FROM sec.sec_allwd_other_infos a " +
               "LEFT OUTER JOIN gst.gen_stp_lov_values b ON (a.other_info_id = b.pssbl_value_id) " +
               "WHERE((a.is_enabled = '1')  AND (a.table_id = " + tblID + ") AND (b.allowed_org_ids like '%," + this.Org_id.ToString() +
               ",%') AND (((select c.other_info_value from " + valTbl + " c " +
               "where ((c.tbl_othr_inf_combntn_id = a.comb_info_id) AND (c.row_pk_id_val = " + row_id_val + "))) ilike '" +
               searchWord.Replace("'", "''") + "') OR ((select c.other_info_value from " + valTbl + " c " +
               "where ((c.tbl_othr_inf_combntn_id = a.comb_info_id) AND (c.row_pk_id_val = " + row_id_val + "))) is null))) " +
               @" UNION 
                  SELECT c.other_info_category, c.other_info_label, c.other_info_value othr_inf, 99999999 comb_info_id, -1 table_id, c.dflt_row_id from " + valTbl +
               " c  WHERE c.tbl_othr_inf_combntn_id<=0 and c.row_pk_id_val = " + row_id_val + ") tbl1 WHERE 1=1" + whrCls +
               " ORDER BY tbl1.comb_info_id LIMIT " + limit_size + " OFFSET " + (Math.Abs(offset * limit_size)).ToString();

            DataSet dtst = this.selectDataNoParams(strSql);
            brghtsqlStr = strSql;
            return dtst;
        }

        public string getOneExtInfosNVals(long tblID,
      long row_id_val, string valTbl, string psblVal)
        {
            string strSql = "";
            strSql = @"SELECT tbl1.* FROM (SELECT b.pssbl_value other_info_category, 
          COALESCE((select c.other_info_label from " + valTbl + " c " +
               "where ((c.tbl_othr_inf_combntn_id = a.comb_info_id) AND (c.row_pk_id_val = " + row_id_val + @"))), b.pssbl_value) other_info_label, 
(select c.other_info_value from " + valTbl + " c " +
         "where ((c.tbl_othr_inf_combntn_id = a.comb_info_id) AND (c.row_pk_id_val = " + row_id_val + "))) " +
         "othr_inf, a.comb_info_id, a.table_id, (select c.dflt_row_id from " + valTbl + " c " +
         "where ((c.tbl_othr_inf_combntn_id = a.comb_info_id) AND (c.row_pk_id_val = " + row_id_val + "))) " +
         "othr_inf_row_id " +
         "FROM sec.sec_allwd_other_infos a " +
         "LEFT OUTER JOIN gst.gen_stp_lov_values b ON (a.other_info_id = b.pssbl_value_id) " +
         "WHERE((a.is_enabled = '1')  AND (a.table_id = " + tblID + ") AND (b.allowed_org_ids like '%," + this.Org_id.ToString() + ",%')) " +
         @" UNION 
                  SELECT c.other_info_category, c.other_info_label, c.other_info_value othr_inf, 99999999 comb_info_id, -1 table_id, c.dflt_row_id from " + valTbl +
         " c  WHERE c.tbl_othr_inf_combntn_id<=0 and c.row_pk_id_val = " + row_id_val + ") tbl1 WHERE tbl1.other_info_label='" + psblVal.Replace("'", "''") + "'" +
         " ORDER BY tbl1.comb_info_id ";

            /*strSql = "SELECT b.pssbl_value, (select c.other_info_value from " + valTbl + " c " +
                     "where ((c.tbl_othr_inf_combntn_id = a.comb_info_id) AND (c.row_pk_id_val = " + row_id_val + "))) " +
                     "othr_inf, a.comb_info_id, a.table_id, (select c.dflt_row_id from " + valTbl + " c " +
                     "where ((c.tbl_othr_inf_combntn_id = a.comb_info_id) AND (c.row_pk_id_val = " + row_id_val + "))) " +
                     "othr_inf_row_id " +
                     "FROM sec.sec_allwd_other_infos a " +
                     "LEFT OUTER JOIN gst.gen_stp_lov_values b ON (a.other_info_id = b.pssbl_value_id) " +
                     "WHERE((a.is_enabled = '1')  AND (a.table_id = " + tblID +
                     ") AND (b.allowed_org_ids like '%," + this.Org_id.ToString() +
                     ",%') AND (b.pssbl_value='" + psblVal.Replace("'", "''") + "')) " +
                     "ORDER BY a.comb_info_id ";*/

            DataSet dtst = this.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return dtst.Tables[0].Rows[0][1].ToString();
            }
            return "";
        }

        public string getMainTableNm(long tblID)
        {
            string strSql = "SELECT main_table_name " +
           "FROM sec.sec_module_sub_groups WHERE (table_id = " + tblID + ")";
            DataSet dtst = this.selectDataNoParams(strSql);

            if (dtst.Tables[0].Rows.Count > 0)
            {
                return dtst.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public string getMainTableColNm(long tblID)
        {
            string strSql = "SELECT row_pk_col_name " +
           "FROM sec.sec_module_sub_groups WHERE (table_id = " + tblID + ")";
            DataSet dtst = this.selectDataNoParams(strSql);

            if (dtst.Tables[0].Rows.Count > 0)
            {
                return dtst.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public long doesRowHvOthrInfo(string valTbl, long cmbntnID, long rowValID)
        {
            string strSql = "SELECT a.dflt_row_id FROM " + valTbl + " " +
              "a WHERE((a.tbl_othr_inf_combntn_id = " + cmbntnID + " and a.tbl_othr_inf_combntn_id>0) AND (a.row_pk_id_val = " + rowValID + "))";
            DataSet dtst = this.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public string getRowOthrInfoVal(long dfltRowID, string valTbl)
        {
            string strSql = "SELECT a.other_info_value FROM " + valTbl + " " +
              "a WHERE((a.dflt_row_id = " + dfltRowID + "))";
            DataSet dtst = this.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return dtst.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region "CUSTOM FUNCTIONS..."
        #region "Storing Pictures in the Database..."
        public Byte[] GetImagesByteFormat(MemoryStream memoryPix)
        {
            try
            {
                Byte[] pixInByte;
                pixInByte = memoryPix.GetBuffer();
                memoryPix.Close();
                return pixInByte;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Invalid Image", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public MemoryStream GetOriginalImageStream(Byte[] pixInByte)
        {
            try
            {
                MemoryStream memoryPix = new MemoryStream(pixInByte);
                return memoryPix;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Invalid Image", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public Byte[] imgByteFormat(ref PictureBox picBox)
        {
            System.IO.MemoryStream memPix = new System.IO.MemoryStream();
            picBox.Image.Save(memPix, picBox.Image.RawFormat);
            return this.GetImagesByteFormat(memPix);
        }

        //this.bannrPictureBox.Image = Image.FromStream(imgPrcs.GetOriginalImageStream((Byte[]) dtst.Tables[0].Rows[0][10]));
        //this.logoPictureBox.Image = Image.FromStream(imgPrcs.GetOriginalImageStream((Byte[]) dtst.Tables[0].Rows[0][11]));

        private void initialize_images()
        {
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.imageList1 = new System.Windows.Forms.ImageList();

            this.openFileDialog1.FileName = "openFileDialog1";
            this.saveFileDialog1.FileName = "saveFileDialog1";
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
        }

        public bool pickAnImage(long id, ref PictureBox picBox, int folderTyp)
        {
            //If strict FTP is the case uploadload File to Server after

            //this.openFileDialog1.InitialDirectory = this.myComputer.FileSystem.SpecialDirectories.MyDocuments;
            this.openFileDialog1.RestoreDirectory = true;
            this.openFileDialog1.Filter = "All Files|*.*|Image Files|*.bmp;*.gif;*.jpg;*.png|Bitmaps|*.bmp|GIFs|*.gif|JPEGs|*.jpg|PNGs|*.png";
            this.openFileDialog1.FilterIndex = 2;
            this.openFileDialog1.Title = "Select a picture to Load...";
            this.openFileDialog1.FileName = "";
            if (this.openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    long meLong = this.myComputer.FileSystem.GetFileInfo(this.openFileDialog1.FileName).Length;
                    if (meLong > (1024 * 1024))
                    {
                        this.showMsg("Image size (" + Math.Round((double)((double)meLong / 1024), 2).ToString() + "KBytes) exceeds the limit of (1024KBytes)!\r\nFormat the image and try loading again!", 0);
                        return false;
                    }
                    picBox.Image.Dispose();
                    picBox.Image.Dispose();
                    picBox.Image.Dispose();
                    picBox.Image.Dispose();
                    picBox.Image.Dispose();
                    picBox.Image.Dispose();
                    picBox.Image.Dispose();
                    picBox.Image.Dispose();
                    picBox.Image.Dispose();
                    picBox.Image.Dispose();
                    picBox.Image = null;
                    Application.DoEvents();
                    Application.DoEvents();
                    Application.DoEvents();
                    //picBox.Image = global::CommonCode.Properties.Resources.blank;
                    picBox.Image = global::CommonCode.Properties.Resources.actions_document_preview;
                    System.Drawing.Image img = Image.FromFile(this.openFileDialog1.FileName);
                    picBox.Image = img;
                    String fileName = "";
                    string folderNm = "";
                    string storeFileNm = "";
                    if (folderTyp == 0)
                    {
                        fileName = this.getOrgImgsDrctry() + @"\" + id.ToString() + ".png";
                        folderNm = this.getOrgImgsDrctry();
                        storeFileNm = id.ToString() + ".png";
                        if (this.myComputer.FileSystem.DirectoryExists(this.getOrgImgsDrctry()) == false)
                        {
                            this.myComputer.FileSystem.CreateDirectory(this.getOrgImgsDrctry());
                        }
                    }
                    else if (folderTyp == 1)
                    {
                        fileName = this.getDivsImgsDrctry() + @"\" + id.ToString() + ".png";
                        folderNm = this.getDivsImgsDrctry();
                        storeFileNm = id.ToString() + ".png";
                        if (this.myComputer.FileSystem.DirectoryExists(this.getDivsImgsDrctry()) == false)
                        {
                            this.myComputer.FileSystem.CreateDirectory(this.getDivsImgsDrctry());
                        }
                    }
                    else if (folderTyp == 2)
                    {
                        fileName = this.getPrsnImgsDrctry() + @"\" + id.ToString() + ".png";
                        folderNm = this.getPrsnImgsDrctry();
                        storeFileNm = id.ToString() + ".png";
                        if (this.myComputer.FileSystem.DirectoryExists(this.getPrsnImgsDrctry()) == false)
                        {
                            this.myComputer.FileSystem.CreateDirectory(this.getPrsnImgsDrctry());
                        }
                    }
                    else if (folderTyp == 3)
                    {
                        folderNm = this.getPrdtImgsDrctry();
                        fileName = this.getPrdtImgsDrctry() + @"\" + id.ToString() + ".png";
                        storeFileNm = id.ToString() + ".png";
                        if (this.myComputer.FileSystem.DirectoryExists(this.getPrdtImgsDrctry()) == false)
                        {
                            this.myComputer.FileSystem.CreateDirectory(this.getPrdtImgsDrctry());
                        }
                    }
                    if (this.myComputer.FileSystem.FileExists(fileName))
                    {
                        this.myComputer.FileSystem.DeleteFile(fileName,
                          Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                         Microsoft.VisualBasic.FileIO.RecycleOption.DeletePermanently,
                         Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing);
                    }
                    Application.DoEvents();
                    Application.DoEvents();
                    picBox.Image.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
                    //img.Dispose();
                    //img.Dispose();
                    //img.Dispose();
                    //img.Dispose();
                    //img.Dispose();
                    //img.Dispose();
                    //img.Dispose();
                    //img.Dispose();
                    //picBox.Image.Dispose();
                    //picBox.Image.Dispose();
                    //picBox.Image.Dispose();
                    //picBox.Image.Dispose();
                    //picBox.Image.Dispose();
                    //picBox.Image.Dispose();
                    //picBox.Image.Dispose();
                    //picBox.Image.Dispose();
                    //picBox.Image.Dispose();
                    //picBox.Image.Dispose();
                    //picBox.Image.Dispose();
                    //picBox.Image = null;
                    Application.DoEvents();
                    Application.DoEvents();
                    Application.DoEvents();
                    this.upldImgsFTP(folderTyp, folderNm, storeFileNm);
                    //System.Drawing.Image img = Image.FromFile(fileName);
                    //picBox.Image = img;
                    //img.Dispose();
                    //this.upldImgsFTP(folderTyp, folderNm, storeFileNm);
                    return true;
                }
                catch (Exception ex)
                {
                    picBox.Image.Dispose();
                    picBox.Image.Dispose();
                    picBox.Image.Dispose();
                    picBox.Image.Dispose();
                    picBox.Image.Dispose();
                    picBox.Image.Dispose();
                    picBox.Image.Dispose();
                    picBox.Image.Dispose();
                    picBox.Image.Dispose();
                    picBox.Image.Dispose();
                    picBox.Image = null;
                    Application.DoEvents();
                    picBox.Image = global::CommonCode.Properties.Resources.actions_document_preview;
                    this.showMsg(ex.Message + "\r\nThe image is of an invalid format!", 4);
                    return false;
                }
            }
            return false;
        }

        public string pickAFile()
        {
            this.openFileDialog1.RestoreDirectory = true;
            this.openFileDialog1.Filter = "All Files|*.*|Image Files|*.bmp;*.gif;*.jpg;*.jpeg;*.tiff;*.exif;*.ico;*.png|Bitmaps|*.bmp|GIFs|*.gif|JPEGs|*.jpg;*.jpeg;|PNGs|*.png";
            this.openFileDialog1.FilterIndex = 1;
            this.openFileDialog1.Title = "Select an Image/File to Load...";
            this.openFileDialog1.FileName = "";
            if (this.openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return this.openFileDialog1.FileName;
            }
            return "";
        }

        public string pickAFile(string filterTxt)
        {
            if (filterTxt == "")
            {
                filterTxt = "All Files|*.*|Image Files|*.bmp;*.gif;*.jpg;*.jpeg;*.tiff;*.exif;*.ico;*.png|Bitmaps|*.bmp|GIFs|*.gif|JPEGs|*.jpg;*.jpeg;|PNGs|*.png";
            }
            this.openFileDialog1.RestoreDirectory = true;
            this.openFileDialog1.Filter = filterTxt;
            this.openFileDialog1.FilterIndex = 1;
            this.openFileDialog1.Title = "Select an Image/File to Load...";
            this.openFileDialog1.FileName = "";
            if (this.openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return this.openFileDialog1.FileName;
            }
            return "";
        }

        public bool copyAFile(long id, string destfolderNm, string srcFileNm)
        {
            //If strict FTP is the case uploadload File to Server after  

            try
            {
                string extnsn = this.myComputer.FileSystem.GetFileInfo(srcFileNm).Extension;
                String nwfileName = "";
                nwfileName = destfolderNm + @"\" + id.ToString() + extnsn;
                //storeFileNm = id.ToString() + "." + extnsn;
                if (srcFileNm == nwfileName)
                {
                    return true;
                }
                if (this.myComputer.FileSystem.DirectoryExists(destfolderNm) == false)
                {
                    this.myComputer.FileSystem.CreateDirectory(destfolderNm);
                }
                if (this.myComputer.FileSystem.FileExists(nwfileName))
                {
                    this.myComputer.FileSystem.DeleteFile(nwfileName, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                     Microsoft.VisualBasic.FileIO.RecycleOption.DeletePermanently, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing);
                }
                Application.DoEvents();
                if (this.myComputer.FileSystem.FileExists(srcFileNm))
                {
                    this.myComputer.FileSystem.CopyFile(
                      srcFileNm, nwfileName, true);
                    int folderTyp = -1;
                    if (destfolderNm == this.getOrgImgsDrctry())
                    {
                        folderTyp = 0;
                    }
                    else if (destfolderNm == this.getDivsImgsDrctry())
                    {
                        folderTyp = 1;
                    }
                    else if (destfolderNm == this.getPrsnImgsDrctry())
                    {
                        folderTyp = 2;
                    }
                    else if (destfolderNm == this.getPrdtImgsDrctry())
                    {
                        folderTyp = 3;
                    }
                    else if (destfolderNm == this.getPrsnDocsImgsDrctry())
                    {
                        folderTyp = 4;
                    }
                    else if (destfolderNm == this.getAcctngImgsDrctry())
                    {
                        folderTyp = 5;
                    }
                    else if (destfolderNm == this.getPrchsImgsDrctry())
                    {
                        folderTyp = 6;
                    }
                    else if (destfolderNm == this.getSalesImgsDrctry())
                    {
                        folderTyp = 7;
                    }
                    else if (destfolderNm == this.getRcptsImgsDrctry())
                    {
                        folderTyp = 8;
                    }
                    else if (destfolderNm == this.getRptDrctry())
                    {
                        folderTyp = 9;
                    }
                    else if (destfolderNm == this.getRptDrctry() + "\\jrxmls")
                    {
                        folderTyp = 15;
                    }
                    else if (destfolderNm == this.getRptDrctry() + "\\mail_attachments")
                    {
                        folderTyp = 17;
                    }
                    else if (destfolderNm == this.getAttnDocsImgsDrctry())
                    {
                        folderTyp = 10;
                    }
                    else if (destfolderNm == this.getAssetsImgsDrctry())
                    {
                        folderTyp = 11;
                    }
                    else if (destfolderNm == this.getPyblsImgsDrctry())
                    {
                        folderTyp = 12;
                    }
                    else if (destfolderNm == this.getRcvblsImgsDrctry())
                    {
                        folderTyp = 13;
                    }
                    else if (destfolderNm == this.getFirmsImgsDrctry())
                    {
                        folderTyp = 14;
                    }
                    else if (destfolderNm == this.getPtycshImgsDrctry())
                    {
                        folderTyp = 16;
                    }
                    //this.dwnldImgsFTP(2, folderNm, storeFileNm);
                    this.upldImgsFTP(folderTyp, destfolderNm, id.ToString() + extnsn);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Application.DoEvents();
                this.showMsg(ex.Message, 4);
                return false;
            }
        }

        public bool copyAFileSpcl(string destfolderNm, string srcFileNm)
        {
            //If strict FTP is the case uploadload File to Server after  

            try
            {
                string extnsn = this.myComputer.FileSystem.GetFileInfo(srcFileNm).Extension;
                String nwfileName = "";
                string baseNm = System.IO.Path.GetFileName(srcFileNm);
                nwfileName = destfolderNm + @"\" + baseNm;
                if (srcFileNm == nwfileName)
                {
                    return true;
                }
                if (this.myComputer.FileSystem.DirectoryExists(destfolderNm) == false)
                {
                    this.myComputer.FileSystem.CreateDirectory(destfolderNm);
                }
                if (this.myComputer.FileSystem.FileExists(nwfileName))
                {
                    this.myComputer.FileSystem.DeleteFile(nwfileName, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                     Microsoft.VisualBasic.FileIO.RecycleOption.DeletePermanently, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing);
                }
                Application.DoEvents();
                if (this.myComputer.FileSystem.FileExists(srcFileNm))
                {
                    this.myComputer.FileSystem.CopyFile(
                      srcFileNm, nwfileName, true);
                    int folderTyp = 17;

                    this.upldImgsFTP(folderTyp, destfolderNm, baseNm);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Application.DoEvents();
                this.showMsg(ex.Message, 4);
                return false;
            }
        }

        public bool deleteAFile(string srcFileNm)
        {
            //If strict FTP is the case uploadload File to Server after      
            try
            {
                if (this.myComputer.FileSystem.FileExists(srcFileNm))
                {
                    this.myComputer.FileSystem.DeleteFile(srcFileNm, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                     Microsoft.VisualBasic.FileIO.RecycleOption.DeletePermanently, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                this.showMsg(ex.Message, 4);
                return false;
            }
        }

        public void writeImgFile(Byte[] imgRead, string fileNm)
        {
            // System.IO.FileStream rs = new System.IO.FileStream(Application.StartupPath + @"\logo.png",
            //System.IO.FileMode.OpenOrCreate,
            //System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite);
            // Byte[] imgRead = new Byte[rs.Length];
            // rs.Read(imgRead, 0, Convert.ToInt32(rs.Length));
            System.IO.FileStream fs = new System.IO.FileStream(fileNm, System.IO.FileMode.OpenOrCreate,
             System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite);
            fs.Write(imgRead, 0, imgRead.Length);
        }

        public void getDBImageFile(string storeFileNm, int folderTyp, ref PictureBox pcBx)
        {
            try
            {
                //If strict FTP is the case Download Server File to locDir first
                string folderNm = "";
                Image img;
                if (folderTyp == 0)
                {
                    folderNm = this.getOrgImgsDrctry();
                }
                else if (folderTyp == 1)
                {
                    folderNm = this.getDivsImgsDrctry();
                }
                else if (folderTyp == 2)
                {
                    folderNm = this.getPrsnImgsDrctry();
                }
                else if (folderTyp == 3)
                {
                    folderNm = this.getPrdtImgsDrctry();
                }
                else if (folderTyp == 4)
                {
                    folderNm = this.getPrsnDocsImgsDrctry();
                }
                else if (folderTyp == 5)
                {
                    folderNm = this.getAcctngImgsDrctry();
                }
                else if (folderTyp == 6)
                {
                    folderNm = this.getPrchsImgsDrctry();
                }
                else if (folderTyp == 7)
                {
                    folderNm = this.getSalesImgsDrctry();
                }
                else if (folderTyp == 8)
                {
                    folderNm = this.getRcptsImgsDrctry();
                }
                else if (folderTyp == 9)
                {
                    folderNm = this.getRptDrctry();
                }
                else if (folderTyp == 10)
                {
                    folderNm = this.getAttnDocsImgsDrctry();
                }
                else if (folderTyp == 11)
                {
                    folderNm = this.getAssetsImgsDrctry();
                }
                else if (folderTyp == 12)
                {
                    folderNm = this.getPyblsImgsDrctry();
                }
                else if (folderTyp == 13)
                {
                    folderNm = this.getRcvblsImgsDrctry();
                }
                else if (folderTyp == 14)
                {
                    folderNm = this.getFirmsImgsDrctry();
                }
                else if (folderTyp == 16)
                {
                    folderNm = this.getPtycshImgsDrctry();
                }

                this.isDwnldDone = false;
                this.dwnldImgsFTP(folderTyp, folderNm, storeFileNm);
                this.globalPcBx = pcBx;
                this.globalPcBx1 = null;
                Thread thread = new Thread(() => loadImage(folderNm, storeFileNm));
                thread.Start();
            }
            catch (Exception ex)
            {
                pcBx.Image = global::CommonCode.Properties.Resources.actions_document_preview;
                //this.showMsg(ex.Message + "\r\n" + ex.StackTrace + "\r\n" + ex.InnerException, 0);
            }
            finally
            {

            }
        }

        public void getDBImageFile(string storeFileNm, int folderTyp, ref PictureBox pcBx, ref PictureBox pcBx1)
        {
            try
            {
                //If strict FTP is the case Download Server File to locDir first
                string folderNm = "";
                Image img;
                if (folderTyp == 0)
                {
                    folderNm = this.getOrgImgsDrctry();
                }
                else if (folderTyp == 1)
                {
                    folderNm = this.getDivsImgsDrctry();
                }
                else if (folderTyp == 2)
                {
                    folderNm = this.getPrsnImgsDrctry();
                }
                else if (folderTyp == 3)
                {
                    folderNm = this.getPrdtImgsDrctry();
                }
                else if (folderTyp == 4)
                {
                    folderNm = this.getPrsnDocsImgsDrctry();
                }
                else if (folderTyp == 5)
                {
                    folderNm = this.getAcctngImgsDrctry();
                }
                else if (folderTyp == 6)
                {
                    folderNm = this.getPrchsImgsDrctry();
                }
                else if (folderTyp == 7)
                {
                    folderNm = this.getSalesImgsDrctry();
                }
                else if (folderTyp == 8)
                {
                    folderNm = this.getRcptsImgsDrctry();
                }
                else if (folderTyp == 9)
                {
                    folderNm = this.getRptDrctry();
                }
                else if (folderTyp == 10)
                {
                    folderNm = this.getAttnDocsImgsDrctry();
                }
                else if (folderTyp == 11)
                {
                    folderNm = this.getAssetsImgsDrctry();
                }
                else if (folderTyp == 12)
                {
                    folderNm = this.getPyblsImgsDrctry();
                }
                else if (folderTyp == 13)
                {
                    folderNm = this.getRcvblsImgsDrctry();
                }
                else if (folderTyp == 14)
                {
                    folderNm = this.getFirmsImgsDrctry();
                }
                else if (folderTyp == 16)
                {
                    folderNm = this.getPtycshImgsDrctry();
                }

                this.isDwnldDone = false;
                this.dwnldImgsFTP(folderTyp, folderNm, storeFileNm);
                this.globalPcBx = pcBx;
                this.globalPcBx1 = pcBx1;
                Thread thread = new Thread(() => loadImage(folderNm, storeFileNm));
                thread.Start();
            }
            catch (Exception ex)
            {
                pcBx.Image = global::CommonCode.Properties.Resources.actions_document_preview;
                //this.showMsg(ex.Message + "\r\n" + ex.StackTrace + "\r\n" + ex.InnerException, 0);
            }
            finally
            {

            }
        }

        private PictureBox globalPcBx;
        private PictureBox globalPcBx1;
        private void loadImage(string folderNm, string storeFileNm)
        {
            try
            {
                Image img;
                do
                {
                    //do nothing
                    Thread.Sleep(200);
                }
                while (this.isDwnldDone == false);

                if (this.myComputer.FileSystem.FileExists(folderNm + @"\" + storeFileNm))
                {
                    System.IO.FileStream rs = new System.IO.FileStream(folderNm + @"\" + storeFileNm,
            System.IO.FileMode.Open,
            System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite);
                    Byte[] imgRead = new Byte[rs.Length];
                    rs.Read(imgRead, 0, Convert.ToInt32(rs.Length));
                    img = Image.FromStream(rs);
                    rs.Close();
                }
                else
                {
                    if (this.globalPcBx1 != null)
                    {
                        img = global::CommonCode.Properties.Resources.staffs;
                    }
                    else
                    {
                        img = global::CommonCode.Properties.Resources.actions_document_preview;
                    }
                }
                this.globalPcBx.Image = img;
                if (this.globalPcBx1 != null)
                {
                    this.globalPcBx1.Image = img;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {

            }
        }

        private void loadImage1(string fileNm)
        {
            try
            {
                Image img;
                do
                {
                    //do nothing
                    Thread.Sleep(200);
                }
                while (this.isDwnldDone == false);
                if (this.myComputer.FileSystem.FileExists(fileNm))
                {
                    string extnsn = this.myComputer.FileSystem.GetFileInfo(fileNm).Extension.Trim('.');
                    long meLong = this.myComputer.FileSystem.GetFileInfo(fileNm).Length;
                    if (meLong > (1.5 * 1024 * 1024))
                    {
                        img = global::CommonCode.Properties.Resources.actions_document_preview;
                    }
                    else if (!(extnsn.ToLower() == "bmp"
                      || extnsn.ToLower() == "gif"
                      || extnsn.ToLower() == "jpg"
                      || extnsn.ToLower() == "png"
                      || extnsn.ToLower() == "jpeg"
                      ))
                    {
                        /*|| extnsn == "tiff"
                        || extnsn == "exif"
                        || extnsn == "ico"*/
                        img = global::CommonCode.Properties.Resources.actions_document_preview;
                    }
                    else
                    {
                        System.IO.FileStream rs = new System.IO.FileStream(fileNm,
                       System.IO.FileMode.OpenOrCreate,
                       System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite);
                        Byte[] imgRead = new Byte[rs.Length];
                        rs.Read(imgRead, 0, Convert.ToInt32(rs.Length));
                        img = Image.FromStream(rs);
                        rs.Close();
                    }
                }
                else
                {
                    img = global::CommonCode.Properties.Resources.actions_document_preview;
                }
                this.globalPcBx.Image = img;
            }
            catch (Exception ex)
            {
            }
            finally
            {

            }
        }

        public Image getDBImageFile(string storeFileNm, int folderTyp)
        {
            try
            {
                //If strict FTP is the case Download Server File to locDir first
                string folderNm = "";
                Image img;
                if (folderTyp == 0)
                {
                    folderNm = this.getOrgImgsDrctry();
                }
                else if (folderTyp == 1)
                {
                    folderNm = this.getDivsImgsDrctry();
                }
                else if (folderTyp == 2)
                {
                    folderNm = this.getPrsnImgsDrctry();
                }
                else if (folderTyp == 3)
                {
                    folderNm = this.getPrdtImgsDrctry();
                }
                else if (folderTyp == 4)
                {
                    folderNm = this.getPrsnDocsImgsDrctry();
                }
                else if (folderTyp == 5)
                {
                    folderNm = this.getAcctngImgsDrctry();
                }
                else if (folderTyp == 6)
                {
                    folderNm = this.getPrchsImgsDrctry();
                }
                else if (folderTyp == 7)
                {
                    folderNm = this.getSalesImgsDrctry();
                }
                else if (folderTyp == 8)
                {
                    folderNm = this.getRcptsImgsDrctry();
                }
                else if (folderTyp == 9)
                {
                    folderNm = this.getRptDrctry();
                }
                else if (folderTyp == 10)
                {
                    folderNm = this.getAttnDocsImgsDrctry();
                }
                else if (folderTyp == 11)
                {
                    folderNm = this.getAssetsImgsDrctry();
                }
                else if (folderTyp == 12)
                {
                    folderNm = this.getPyblsImgsDrctry();
                }
                else if (folderTyp == 13)
                {
                    folderNm = this.getRcvblsImgsDrctry();
                }
                else if (folderTyp == 14)
                {
                    folderNm = this.getFirmsImgsDrctry();
                }
                else if (folderTyp == 16)
                {
                    folderNm = this.getPtycshImgsDrctry();
                }
                //this.dwnldImgsFTP(2, folderNm, storeFileNm);
                this.isDwnldDone = false;
                this.dwnldImgsFTP(folderTyp, folderNm, storeFileNm);
                do
                {
                    //do nothing
                    Thread.Sleep(200);
                }
                while (this.isDwnldDone == false);
                if (this.myComputer.FileSystem.FileExists(folderNm + @"\" + storeFileNm))
                {
                    System.IO.FileStream rs = new System.IO.FileStream(folderNm + @"\" + storeFileNm,
                   System.IO.FileMode.OpenOrCreate,
                   System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite);
                    Byte[] imgRead = new Byte[rs.Length];
                    rs.Read(imgRead, 0, Convert.ToInt32(rs.Length));
                    img = Image.FromStream(rs);
                    rs.Close();
                }
                else
                {
                    img = global::CommonCode.Properties.Resources.actions_document_preview;
                }
                return img;
            }
            catch (Exception ex)
            {
                return global::CommonCode.Properties.Resources.actions_document_preview;
            }
            finally
            {

            }
        }

        public void getDBImageFile(string storeFileNm, string folderNm, ref PictureBox pcBx)
        {
            try
            {
                //If strict FTP is the case Download Server File to locDir first
                Image img;
                string fileNm = folderNm + @"/" + storeFileNm;
                int folderTyp = -1;
                if (folderNm == this.getOrgImgsDrctry())
                {
                    folderTyp = 0;
                }
                else if (folderNm == this.getDivsImgsDrctry())
                {
                    folderTyp = 1;
                }
                else if (folderNm == this.getPrsnImgsDrctry())
                {
                    folderTyp = 2;
                }
                else if (folderNm == this.getPrdtImgsDrctry())
                {
                    folderTyp = 3;
                }
                else if (folderNm == this.getPrsnDocsImgsDrctry())
                {
                    folderTyp = 4;
                }
                else if (folderNm == this.getAcctngImgsDrctry())
                {
                    folderTyp = 5;
                }
                else if (folderNm == this.getPrchsImgsDrctry())
                {
                    folderTyp = 6;
                }
                else if (folderNm == this.getSalesImgsDrctry())
                {
                    folderTyp = 7;
                }
                else if (folderNm == this.getRcptsImgsDrctry())
                {
                    folderTyp = 8;
                }
                else if (folderNm == this.getRptDrctry())
                {
                    folderTyp = 9;
                }
                else if (folderNm == this.getAttnDocsImgsDrctry())
                {
                    folderTyp = 10;
                }
                else if (folderNm == this.getAssetsImgsDrctry())
                {
                    folderTyp = 11;
                }
                else if (folderNm == this.getPyblsImgsDrctry())
                {
                    folderTyp = 12;
                }
                else if (folderNm == this.getRcvblsImgsDrctry())
                {
                    folderTyp = 13;
                }
                else if (folderNm == this.getFirmsImgsDrctry())
                {
                    folderTyp = 14;
                }
                else if (folderNm == this.getPtycshImgsDrctry())
                {
                    folderTyp = 16;
                }
                //this.dwnldImgsFTP(2, folderNm, storeFileNm);
                this.isDwnldDone = false;
                this.dwnldImgsFTP(folderTyp, folderNm, storeFileNm);
                this.globalPcBx = pcBx;
                this.globalPcBx1 = null;
                Thread thread = new Thread(() => loadImage1(fileNm));
                thread.Start();
            }
            catch (Exception ex)
            {
                pcBx.Image = global::CommonCode.Properties.Resources.actions_document_preview;
                //this.showMsg(ex.Message, 0);
            }
            finally
            {

            }
        }

        public void saveImageToFile(ref PictureBox picBox)
        {
            this.saveFileDialog1.RestoreDirectory = true;
            this.saveFileDialog1.Filter = "PNGs|*.png";
            this.saveFileDialog1.FilterIndex = 1;
            this.saveFileDialog1.Title = "Select a picture to Load...";
            this.saveFileDialog1.FileName = "";
            if (this.saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                picBox.Image.Save(this.saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Png);
            }
            System.Diagnostics.Process.Start(this.saveFileDialog1.FileName);
        }

        public string getPGBinDrctry()
        {
            //\\172.25.10.96\bog_applsys project\RICHARD\Images\Org
            DataSet dtSt = new DataSet();
            string sqlStr = "select pssbl_value from gst.gen_stp_lov_values where ((value_list_id = " +
             this.getLovID("Postgre Bin Directory") + ") AND (is_enabled='1')) ORDER BY pssbl_value_id DESC LIMIT 1";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                if (this.myComputer.FileSystem.DirectoryExists(dtSt.Tables[0].Rows[0][0].ToString()))
                {
                    return dtSt.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    return Application.StartupPath + @"\Images\Logs";
                }
            }
            else
            {
                return Application.StartupPath + @"\Images\Logs";
            }
        }

        public string getBackupDrctry()
        {
            //\\172.25.10.96\bog_applsys project\RICHARD\Images\Org
            return Application.StartupPath + "\\Images\\" + CommonCodes.DatabaseNm + "\\DB_Backups";

            //DataSet dtSt = new DataSet();
            //string sqlStr = "select pssbl_value from gst.gen_stp_lov_values where ((value_list_id = " +
            // this.getLovID("Database Backup Directory") + ") AND (is_enabled='1')) ORDER BY pssbl_value_id DESC LIMIT 1";
            //dtSt = this.selectDataNoParams(sqlStr);
            //if (dtSt.Tables[0].Rows.Count > 0)
            //{
            //  if (this.myComputer.FileSystem.DirectoryExists(dtSt.Tables[0].Rows[0][0].ToString()))
            //  {
            //    return dtSt.Tables[0].Rows[0][0].ToString();
            //  }
            //  else
            //  {
            //    return Application.StartupPath + @"\Images\Logs";
            //  }
            //}
            //else
            //{
            //  return Application.StartupPath + @"\Images\Logs";
            //}
        }

        public string getLogsDrctry()
        {
            return Application.StartupPath + "\\Images\\" + CommonCodes.DatabaseNm + "\\Logs";
            //\\172.25.10.96\bog_applsys project\RICHARD\Images\Org
            //DataSet dtSt = new DataSet();
            //string sqlStr = "select pssbl_value from gst.gen_stp_lov_values where ((value_list_id = " +
            // this.getLovID("Audit Logs Directory") + ") AND (is_enabled='1')) ORDER BY pssbl_value_id DESC LIMIT 1";
            //dtSt = this.selectDataNoParams(sqlStr);
            //if (dtSt.Tables[0].Rows.Count > 0)
            //{
            //  if (this.myComputer.FileSystem.DirectoryExists(dtSt.Tables[0].Rows[0][0].ToString()))
            //  {
            //    return dtSt.Tables[0].Rows[0][0].ToString();
            //  }
            //  else
            //  {
            //    return Application.StartupPath + @"\Images\Logs";
            //  }
            //}
            //else
            //{
            //  return Application.StartupPath + @"\Images\Logs";
            //}
        }

        public string getRptDrctry()
        {
            return Application.StartupPath + "\\Images\\" + CommonCodes.DatabaseNm + "\\Rpts";
            //\\172.25.10.96\bog_applsys project\RICHARD\Images\Org
            //DataSet dtSt = new DataSet();
            //string sqlStr = "select pssbl_value from gst.gen_stp_lov_values where ((value_list_id = " +
            // this.getLovID("Reports Directory") + ") AND (is_enabled='1')) ORDER BY pssbl_value_id DESC LIMIT 1";
            //dtSt = this.selectDataNoParams(sqlStr);
            //if (dtSt.Tables[0].Rows.Count > 0)
            //{
            //  if (this.myComputer.FileSystem.DirectoryExists(dtSt.Tables[0].Rows[0][0].ToString()))
            //  {
            //    return dtSt.Tables[0].Rows[0][0].ToString();
            //  }
            //  else
            //  {
            //    return Application.StartupPath + @"\Images\Rpts";
            //  }
            //}
            //else
            //{
            //  return Application.StartupPath + @"\Images\Rpts";
            //}
        }

        public string getDivsImgsDrctry()
        {
            //\\172.25.10.96\bog_applsys project\RICHARD\Images\Divs
            return Application.StartupPath + "\\Images\\" + CommonCodes.DatabaseNm + "\\Divs";
            //DataSet dtSt = new DataSet();
            //string sqlStr = "select pssbl_value from gst.gen_stp_lov_values where ((value_list_id = " +
            // this.getLovID("Divisions Images Directory") + ") AND (is_enabled='1')) ORDER BY pssbl_value_id DESC LIMIT 1";
            //dtSt = this.selectDataNoParams(sqlStr);
            //if (dtSt.Tables[0].Rows.Count > 0)
            //{
            //  if (this.myComputer.FileSystem.DirectoryExists(dtSt.Tables[0].Rows[0][0].ToString()))
            //  {
            //    return dtSt.Tables[0].Rows[0][0].ToString();
            //  }
            //  else
            //  {
            //    return Application.StartupPath + @"\Images\Divs";
            //  }
            //}
            //else
            //{
            //  return Application.StartupPath + @"\Images\Divs";
            //}
        }

        public string getOrgImgsDrctry()
        {
            //\\172.25.10.96\bog_applsys project\RICHARD\Images\Org
            return Application.StartupPath + "\\Images\\" + CommonCodes.DatabaseNm + "\\Org";
            //DataSet dtSt = new DataSet();
            //string sqlStr = "select pssbl_value from gst.gen_stp_lov_values where ((value_list_id = " +
            // this.getLovID("Organization Images Directory") + ") AND (is_enabled='1')) ORDER BY pssbl_value_id DESC LIMIT 1";
            //dtSt = this.selectDataNoParams(sqlStr);
            //if (dtSt.Tables[0].Rows.Count > 0)
            //{
            //  if (this.myComputer.FileSystem.DirectoryExists(dtSt.Tables[0].Rows[0][0].ToString()))
            //  {
            //    return dtSt.Tables[0].Rows[0][0].ToString();
            //  }
            //  else
            //  {
            //    return Application.StartupPath + @"\Images\Org";
            //  }
            //}
            //else
            //{
            //  return Application.StartupPath + @"\Images\Org";
            //}
        }

        public string getPrsnImgsDrctry()
        {
            //\\172.25.10.96\bog_applsys project\RICHARD\Images\Person
            return Application.StartupPath + "\\Images\\" + CommonCodes.DatabaseNm + "\\Person";
            //DataSet dtSt = new DataSet();
            //string sqlStr = "select pssbl_value from gst.gen_stp_lov_values where ((value_list_id = " +
            // this.getLovID("Person Images Directory") + ") AND (is_enabled='1')) ORDER BY pssbl_value_id DESC LIMIT 1";
            //dtSt = this.selectDataNoParams(sqlStr);
            //if (dtSt.Tables[0].Rows.Count > 0)
            //{
            //  if (this.myComputer.FileSystem.DirectoryExists(dtSt.Tables[0].Rows[0][0].ToString()))
            //  {
            //    return dtSt.Tables[0].Rows[0][0].ToString();
            //  }
            //  else
            //  {
            //    return Application.StartupPath + @"\Images\Person";
            //  }
            //}
            //else
            //{
            //  return Application.StartupPath + @"\Images\Person";
            //}
        }

        public string getPrdtImgsDrctry()
        {
            //\\172.25.10.96\bog_applsys project\RICHARD\Images\Person
            return Application.StartupPath + "\\Images\\" + CommonCodes.DatabaseNm + "\\Inv";
            //DataSet dtSt = new DataSet();
            //string sqlStr = "select pssbl_value from gst.gen_stp_lov_values where ((value_list_id = " +
            //this.getLovID("Product Images Directory") + ") AND (is_enabled='1')) ORDER BY pssbl_value_id DESC LIMIT 1";
            //dtSt = this.selectDataNoParams(sqlStr);
            //if (dtSt.Tables[0].Rows.Count > 0)
            //{
            //  if (this.myComputer.FileSystem.DirectoryExists(dtSt.Tables[0].Rows[0][0].ToString()))
            //  {
            //    return dtSt.Tables[0].Rows[0][0].ToString();
            //  }
            //  else
            //  {
            //    return Application.StartupPath + @"\Images\Inv";
            //  }
            //}
            //else
            //{
            //  return Application.StartupPath + @"\Images\Inv";
            //}
        }

        public string getAcctngImgsDrctry()
        {
            return Application.StartupPath + "\\Images\\" + CommonCodes.DatabaseNm + "\\Accntn";
        }

        public string getAssetsImgsDrctry()
        {
            return Application.StartupPath + "\\Images\\" + CommonCodes.DatabaseNm + "\\AssetDocs";
        }

        public string getPyblsImgsDrctry()
        {
            return Application.StartupPath + "\\Images\\" + CommonCodes.DatabaseNm + "\\PyblDocs";
        }
        public string getPtycshImgsDrctry()
        {
            return Application.StartupPath + "\\Images\\" + CommonCodes.DatabaseNm + "\\PtyCshDocs";
        }
        public string getRcvblsImgsDrctry()
        {
            return Application.StartupPath + "\\Images\\" + CommonCodes.DatabaseNm + "\\RcvblDocs";
        }

        public string getFirmsImgsDrctry()
        {
            return Application.StartupPath + "\\Images\\" + CommonCodes.DatabaseNm + "\\FirmsDocs";
        }

        public string getSalesImgsDrctry()
        {
            //\\172.25.10.96\bog_applsys project\RICHARD\Images\Person
            return Application.StartupPath + "\\Images\\" + CommonCodes.DatabaseNm + "\\Sales";
            //DataSet dtSt = new DataSet();
            //string sqlStr = "select pssbl_value from gst.gen_stp_lov_values where ((value_list_id = " +
            //this.getLovID("Sales Images Directory") + ") AND (is_enabled='1')) ORDER BY pssbl_value_id DESC LIMIT 1";
            //dtSt = this.selectDataNoParams(sqlStr);
            //if (dtSt.Tables[0].Rows.Count > 0)
            //{
            //  if (this.myComputer.FileSystem.DirectoryExists(dtSt.Tables[0].Rows[0][0].ToString()))
            //  {
            //    return dtSt.Tables[0].Rows[0][0].ToString();
            //  }
            //  else
            //  {
            //    return Application.StartupPath + @"\Images\Sales";
            //  }
            //}
            //else
            //{
            //  return Application.StartupPath + @"\Images\Sales";
            //}
        }

        public string getPrchsImgsDrctry()
        {
            //\\172.25.10.96\bog_applsys project\RICHARD\Images\Person
            return Application.StartupPath + "\\Images\\" + CommonCodes.DatabaseNm + "\\Prchs";
            //DataSet dtSt = new DataSet();
            //string sqlStr = "select pssbl_value from gst.gen_stp_lov_values where ((value_list_id = " +
            //this.getLovID("Purchasing Images Directory") + ") AND (is_enabled='1')) ORDER BY pssbl_value_id DESC LIMIT 1";
            //dtSt = this.selectDataNoParams(sqlStr);
            //if (dtSt.Tables[0].Rows.Count > 0)
            //{
            //  if (this.myComputer.FileSystem.DirectoryExists(dtSt.Tables[0].Rows[0][0].ToString()))
            //  {
            //    return dtSt.Tables[0].Rows[0][0].ToString();
            //  }
            //  else
            //  {
            //    return Application.StartupPath + @"\Images\Prchs";
            //  }
            //}
            //else
            //{
            //  return Application.StartupPath + @"\Images\Prchs";
            //}
        }

        public string getRcptsImgsDrctry()
        {
            //\\172.25.10.96\bog_applsys project\RICHARD\Images\Person
            return Application.StartupPath + "\\Images\\" + CommonCodes.DatabaseNm + "\\Rcpts";
            //DataSet dtSt = new DataSet();
            //string sqlStr = "select pssbl_value from gst.gen_stp_lov_values where ((value_list_id = " +
            //this.getLovID("Receipts Images Directory") + ") AND (is_enabled='1')) ORDER BY pssbl_value_id DESC LIMIT 1";
            //dtSt = this.selectDataNoParams(sqlStr);
            //if (dtSt.Tables[0].Rows.Count > 0)
            //{
            //  if (this.myComputer.FileSystem.DirectoryExists(dtSt.Tables[0].Rows[0][0].ToString()))
            //  {
            //    return dtSt.Tables[0].Rows[0][0].ToString();
            //  }
            //  else
            //  {
            //    return Application.StartupPath + @"\Images\Rcpts";
            //  }
            //}
            //else
            //{
            //  return Application.StartupPath + @"\Images\Rcpts";
            //}
        }

        public string getPrsnDocsImgsDrctry()
        {
            //\\172.25.10.96\bog_applsys project\RICHARD\Images\Person
            return Application.StartupPath + "/Images/" + CommonCodes.DatabaseNm + "/PrsnDocs";
            //DataSet dtSt = new DataSet();
            //string sqlStr = "select pssbl_value from gst.gen_stp_lov_values where ((value_list_id = " +
            //this.getLovID("Person Documents Images Directory") + ") AND (is_enabled='1')) ORDER BY pssbl_value_id DESC LIMIT 1";
            //dtSt = this.selectDataNoParams(sqlStr);
            //if (dtSt.Tables[0].Rows.Count > 0)
            //{
            //  if (this.myComputer.FileSystem.DirectoryExists(dtSt.Tables[0].Rows[0][0].ToString()))
            //  {
            //    return dtSt.Tables[0].Rows[0][0].ToString();
            //  }
            //  else
            //  {
            //    return Application.StartupPath + @"\Images\PrsnDocs";
            //  }
            //}
            //else
            //{
            //  return Application.StartupPath + @"\Images\PrsnDocs";
            //}
        }

        public string getAttnDocsImgsDrctry()
        {
            return Application.StartupPath + "\\Images\\" + CommonCodes.DatabaseNm + "\\AttnDocs";
        }

        public string[] getFTPServerDet()
        {
            string selSQL = "select a.ftp_server_url, a.ftp_app_sub_directory, "
              + "a.ftp_user_name, a.ftp_user_pswd, a.ftp_port, a.enforce_ftp " +
              "from sec.sec_email_servers a where a.is_default='t'";
            DataSet dtst = this.selectDataNoParams(selSQL);
            string[] str = new string[6];
            str[0] = "";
            str[1] = "";
            str[2] = "";
            str[3] = "";
            str[4] = "";
            str[5] = "0";
            if (dtst.Tables[0].Rows.Count > 0)
            {
                for (int a = 0; a < dtst.Tables[0].Columns.Count; a++)
                {
                    str[a] = dtst.Tables[0].Rows[0][a].ToString();
                }
            }
            return str;
        }

        public void dwnldImgsFTP(int folderTyp, string locfolderNm, string locfileNm)
        {
            string[] srvr = this.getFTPServerDet();
            string subdir = "";
            if (srvr[5] == "0" || locfileNm == "")
            {
                this.isDwnldDone = true;
                return;
            }
            if (folderTyp == 0)
            {
                subdir = @"/Org";
            }
            else if (folderTyp == 1)
            {
                subdir = @"/Divs";
            }
            else if (folderTyp == 2)
            {
                subdir = @"/Person";
            }
            else if (folderTyp == 3)
            {
                subdir = @"/Inv";
            }
            else if (folderTyp == 4)
            {
                subdir = @"/PrsnDocs";
            }
            else if (folderTyp == 5)
            {
                subdir = @"/Accntn";
            }
            else if (folderTyp == 6)
            {
                subdir = @"/Prchs";
            }
            else if (folderTyp == 7)
            {
                subdir = @"/Sales";
            }
            else if (folderTyp == 8)
            {
                subdir = @"/Rcpts";
            }
            else if (folderTyp == 9)
            {
                subdir = @"/Rpts";
            }
            else if (folderTyp == 15)
            {
                subdir = @"/Rpts/jrxmls";
            }
            else if (folderTyp == 10)
            {
                subdir = @"/AttnDocs";
            }
            else if (folderTyp == 11)
            {
                subdir = @"/AssetDocs";
            }
            else if (folderTyp == 12)
            {
                subdir = @"/PyblDocs";
            }
            else if (folderTyp == 13)
            {
                subdir = @"/RcvblDocs";
            }
            else if (folderTyp == 14)
            {
                subdir = @"/FirmsDocs";
            }

            Thread thread = new Thread(() => startDownLoad(srvr[0] + srvr[1] + subdir + @"/" + locfileNm,
                    locfolderNm + @"/" + locfileNm, srvr[2],
               this.decrypt(srvr[3], CommonCodes.AppKey)));
            thread.Start();
        }

        private void startDownLoad(string fullFtpFileFUrl, string fullLocFileUrl,
                            string userName, string password)
        {
            this.DownloadFile(fullFtpFileFUrl, fullLocFileUrl, userName, password);
        }

        public void upldImgsFTP(int folderTyp, string locfolderNm, string locfileNm)
        {
            string subdir = "";
            string[] srvr = this.getFTPServerDet();
            if (srvr[5] == "0" || locfileNm == "")
            {
                return;
            }
            if (folderTyp == 0)
            {
                subdir = @"/Org";
            }
            else if (folderTyp == 1)
            {
                subdir = @"/Divs";
            }
            else if (folderTyp == 2)
            {
                subdir = @"/Person";
            }
            else if (folderTyp == 3)
            {
                subdir = @"/Inv";
            }
            else if (folderTyp == 4)
            {
                subdir = @"/PrsnDocs";
            }
            else if (folderTyp == 5)
            {
                subdir = @"/Accntn";
            }
            else if (folderTyp == 6)
            {
                subdir = @"/Prchs";
            }
            else if (folderTyp == 7)
            {
                subdir = @"/Sales";
            }
            else if (folderTyp == 8)
            {
                subdir = @"/Rcpts";
            }
            else if (folderTyp == 9)
            {
                subdir = @"/Rpts";
            }
            else if (folderTyp == 15)
            {
                subdir = @"/Rpts/jrxmls";
            }
            else if (folderTyp == 17)
            {
                subdir = @"/Rpts/mail_attachments";
            }
            else if (folderTyp == 10)
            {
                subdir = @"/AttnDocs";
            }
            else if (folderTyp == 11)
            {
                subdir = @"/AssetDocs";
            }
            else if (folderTyp == 12)
            {
                subdir = @"/PyblDocs";
            }
            else if (folderTyp == 13)
            {
                subdir = @"/RcvblDocs";
            }
            else if (folderTyp == 14)
            {
                subdir = @"/FirmsDocs";
            }

            Thread thread = new Thread(() => startUpload(srvr[0] + srvr[1] + subdir + @"/" + locfileNm,
              locfolderNm + @"/" + locfileNm, srvr[2], this.decrypt(srvr[3], CommonCodes.AppKey)));
            thread.Start();
        }

        private void startUpload(string fullFtpFileFUrl, string fullLocFileUrl,
                            string userName, string password)
        {
            this.UploadFile(fullFtpFileFUrl, fullLocFileUrl, userName, password);
        }

        public void dwnldImgsDir(int folderTyp, string in_dir)
        {
            string[] srvr = this.getFTPServerDet();
            if (srvr[5] == "0")
            {
                return;
            }
            string subdir = "";
            string locfolderNm = "";
            if (folderTyp == 9)
            {
                subdir = @"/Rpts";
            }
            string[] files = this.GetFileList(srvr[0] + srvr[1] + subdir + @"/", in_dir, srvr[2],
         this.decrypt(srvr[3], CommonCodes.AppKey));
            foreach (string file in files)
            {
                //this.showMsg(in_dir + file, 0);
                if (folderTyp == 9)
                {
                    locfolderNm = this.getRptDrctry();
                }
                this.isDwnldDone = false;
                this.dwnldImgsFTP(folderTyp, locfolderNm, in_dir + file);
            }
        }

        /* Upload File to Specified FTP Url with username and password and Upload Directory 
                    if need to upload in sub folders /// 
            ///Base FtpUrl of FTP Server
            ///Local Filename to Upload
            ///Username of FTP Server
            ///Password of FTP Server
            ///[Optional]Specify sub Folder if any
            /// Status String from Server*/
        public string UploadFile(string fullFtpFileFUrl, string fullLocFileUrl,
        string userName, string password)
        {
            try
            {
                //uploadUrl = ftpserverurl + serverFullAppDirectoryPath + purefilename
                //string PureFileName = new FileInfo(fileName).Name;
                String uploadUrl = fullFtpFileFUrl;
                FtpWebRequest req = (FtpWebRequest)FtpWebRequest.Create(uploadUrl);
                req.Proxy = null;
                req.Method = WebRequestMethods.Ftp.UploadFile;
                req.Credentials = new NetworkCredential(userName, password);
                req.UseBinary = true;
                req.UsePassive = true;
                req.KeepAlive = true;
                byte[] data = File.ReadAllBytes(fullLocFileUrl);
                req.ContentLength = data.Length;
                Stream stream = req.GetRequestStream();
                stream.Write(data, 0, data.Length);
                stream.Close();
                FtpWebResponse res = (FtpWebResponse)req.GetResponse();
                string rspnse = res.StatusDescription;
                //req.KeepAlive = false;
                req.Abort();
                res.Close();
                req = null;
                res = null;
                //fs = null;
                stream = null;
                return rspnse;
            }
            catch (WebException e)
            {
                String status = ((FtpWebResponse)e.Response).StatusDescription;
                //550 Failed to change directory.
                //550 Failed to open file
                if (status == null)
                {
                    status = "";
                }
                if (status.Contains("change directory"))
                {
                    this.SetMethodRequiresCWD();
                }
                else if (status.Contains("open file"))
                {
                }
                else
                {
                    //this.showSQLNoPermsn(fullFtpFileFUrl + "\r\n" + fullLocFileUrl + "\r\n" + status + "\r\n" + e.Message + "\r\n" + e.StackTrace);
                }
                return "";
            }
            catch (Exception ex)
            {
                //this.showSQLNoPermsn(fullFtpFileFUrl + "\r\n" + fullLocFileUrl + "\r\n" + ex.Message + "\r\n" + ex.StackTrace);
                return "";
            }
            finally
            {

            }
        }

        /* Download File From FTP Server /// 
        ///Base url of FTP Server
        ///if file is in root then write FileName Only if is in use like "subdir1/subdir2/filename.ext"
        ///Username of FTP Server
        ///Password of FTP Server
        ///Folderpath where you want to Download the File
        /// Status String from Server*/
        public string DownloadFile(string fullFtpFileFUrl, string fullLocFileUrl,
                            string userName, string password)
        {
            try
            {
                this.isDwnldDone = false;
                //if (System.IO.File.Exists(fullLocFileUrl) == true)
                //{
                //  if (System.IO.File.GetCreationTime(fullLocFileUrl) >= DateTime.Now.AddHours(-1))
                //  {
                //    return "";
                //  }
                //}
                //downloadUrl = ftpserverurl + serverFullAppDirectoryPath + purefilename
                //this.showMsg(fullFtpFileFUrl + "\r\n" + fullLocFileUrl + "\r\n" + userName + "\r\n" + password, 0);
                string ResponseDescription = "";
                //string PureFileName = new FileInfo(FileNameToDownload).Name;
                string DownloadedFilePath = fullLocFileUrl;
                string downloadUrl = fullFtpFileFUrl;
                FtpWebRequest req = (FtpWebRequest)FtpWebRequest.Create(downloadUrl);
                req.Method = WebRequestMethods.Ftp.DownloadFile;
                req.Credentials = new NetworkCredential(userName, password);
                req.UseBinary = true;
                req.UsePassive = true;
                req.Proxy = null;
                req.KeepAlive = true;
                //req.EnableSsl = true;
                FtpWebResponse response = (FtpWebResponse)req.GetResponse();
                Stream stream = response.GetResponseStream();
                byte[] buffer = new byte[2048];
                FileStream fs = new FileStream(DownloadedFilePath, FileMode.Create);
                int ReadCount = stream.Read(buffer, 0, buffer.Length);
                while (ReadCount > 0)
                {
                    fs.Write(buffer, 0, ReadCount);
                    ReadCount = stream.Read(buffer, 0, buffer.Length);
                }
                ResponseDescription = response.StatusDescription;
                fs.Close();
                stream.Close();
                //req.KeepAlive = false;
                req.Abort();
                response.Close();
                req = null;
                response = null;
                fs = null;
                stream = null;
                this.isDwnldDone = true;
                return ResponseDescription;
            }
            catch (WebException e)
            {
                String status = ((FtpWebResponse)e.Response).StatusDescription;
                //550 Failed to change directory.
                //550 Failed to open file
                if (status == null)
                {
                    status = "";
                }
                if (status.Contains("change directory"))
                {
                    this.SetMethodRequiresCWD();
                }
                else if (status.Contains("open file"))
                {
                }
                else
                {
                    //this.showSQLNoPermsn(fullFtpFileFUrl + "\r\n" + fullLocFileUrl + "\r\n" + status + "\r\n" + e.Message + "\r\n" + e.StackTrace);
                }
                this.isDwnldDone = true;
                return "";
            }
            catch (Exception ex)
            {
                //this.showSQLNoPermsn(fullFtpFileFUrl + "\r\n" + fullLocFileUrl + "\r\n" + ex.Message + "\r\n" + ex.StackTrace);
                this.isDwnldDone = true;
                return "";
            }
            finally
            {
                this.isDwnldDone = true;
            }
        }

        public void SetMethodRequiresCWD()
        {
            Type requestType = typeof(FtpWebRequest);
            FieldInfo methodInfoField = requestType.GetField("m_MethodInfo", BindingFlags.NonPublic | BindingFlags.Instance);
            Type methodInfoType = methodInfoField.FieldType;


            FieldInfo knownMethodsField = methodInfoType.GetField("KnownMethodInfo", BindingFlags.Static | BindingFlags.NonPublic);
            Array knownMethodsArray = (Array)knownMethodsField.GetValue(null);

            FieldInfo flagsField = methodInfoType.GetField("Flags", BindingFlags.NonPublic | BindingFlags.Instance);

            int MustChangeWorkingDirectoryToPath = 0x100;
            foreach (object knownMethod in knownMethodsArray)
            {
                int flags = (int)flagsField.GetValue(knownMethod);
                flags |= MustChangeWorkingDirectoryToPath;
                flagsField.SetValue(knownMethod, flags);
            }
        }

        public string[] GetFileList(string ftpServerAddrs, string dirName, string userName, string password)
        {
            string[] downloadFiles;
            StringBuilder result = new StringBuilder();
            WebResponse response = null;
            StreamReader reader = null;
            try
            {
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpServerAddrs + dirName));
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(userName, password);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
                reqFTP.Proxy = null;
                reqFTP.KeepAlive = true;
                reqFTP.UsePassive = false;
                response = reqFTP.GetResponse();
                reader = new StreamReader(response.GetResponseStream());
                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }
                // to remove the trailing '\n'
                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                return result.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
                downloadFiles = null;
                return downloadFiles;
            }
        }

        public bool checkFTP(string fullFtpFileFUrl,
                            string userName, string password)
        {
            FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(fullFtpFileFUrl);
            FtpWebResponse res;
            ftp.Credentials = new NetworkCredential(userName, password);
            ftp.KeepAlive = true;
            ftp.Method = WebRequestMethods.Ftp.GetDateTimestamp;
            ftp.UsePassive = false;

            try
            {
                res = (FtpWebResponse)ftp.GetResponse();
                res.Close();
                return true;
            }
            catch (Exception ex)
            {
                this.showMsg(ex.Message + "\r\n" + ex.InnerException + "\r\n" + ex.StackTrace, 0);
                return false;
                //Handling code here.
            }
        }
        public long getMsgBatchID()
        {
            //string strSql = "select nextval('accb.accb_trnsctn_batches_batch_id_seq'::regclass);";
            string strSql = "select nextval('alrt.bulk_msgs_batch_id_seq')";
            DataSet dtst = this.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public void createMessageQueue(long batchID, string mailTo, string mailCc, string mailBcc, string msgBody, string msgSbjct, string attachmnts, string msgType)
        {
            long uID = -1;
            if (this.User_id <= 0)
            {
                uID = this.getUserID("admin");
            }
            else
            {
                uID = this.User_id;
            }

            string dateStr = this.getDB_Date_time();
            string sqlStr = @"INSERT INTO alrt.bulk_msgs_sent(
            batch_id, to_list, cc_list, msg_body, date_sent, 
            msg_sbjct, bcc_list, created_by, creation_date, sending_status, 
            err_msg, attch_urls, msg_type) VALUES (" + batchID +
            ",'" + mailTo.Replace("'", "''") +
            "','" + mailCc.Replace("'", "''") +
            "','" + msgBody.Replace("'", "''") +
            "','" + dateStr +
            "','" + msgSbjct.Replace("'", "''") +
            "','" + mailBcc.Replace("'", "''") +
            "', " + uID +
            ", '" + dateStr +
            "','0','','" + attachmnts.Replace("'", "''") +
            "','" + msgType.Replace("'", "''") + "')";
            this.insertDataNoParams(sqlStr);
        }

        public bool isEmailValid(string emailString, int lovID)
        {
            bool isEmailValid = Regex.IsMatch(emailString, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            if (isEmailValid == false)
            {
                this.createSysLovsPssblVals1(emailString, lovID);
            }
            return isEmailValid;
        }

        public void createSysLovsPssblVals1(string pssblVals, int lovID)
        {
            if (this.getPssblValID(pssblVals, lovID) <= 0)
            {
                this.createPssblValsForLov1(lovID, pssblVals, pssblVals, "1", "");
            }
        }
        public void createPssblValsForLov1(int lovID, string pssblVal,
         string pssblValDesc, string isEnbld, string allwd)
        {
            string dateStr = this.getDB_Date_time();
            string sqlStr = "INSERT INTO gst.gen_stp_lov_values(" +
                  "value_list_id, pssbl_value, pssbl_value_desc, " +
                              "created_by, creation_date, last_update_by, " +
                              "last_update_date, is_enabled, allowed_org_ids) " +
              "VALUES (" + lovID + ", '" + pssblVal.Replace("'", "''") + "', '" +
              pssblValDesc.Replace("'", "''") +
              "', " + this.User_id + ", '" + dateStr + "', " + this.User_id +
              ", '" + dateStr + "', '" + isEnbld.Replace("'", "''") +
              "', '" + allwd.Replace("'", "''") + "')";
            this.insertDataNoParams(sqlStr);
        }

        public bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (var stream = client.OpenRead("http://www.google.com"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public bool sendEmail(string toEml, string ccEml,
          string bccEml, string attchmnt, string sbjct, string bdyTxt, string msgIdentifier, ref string errMsg)
        {
            try
            {
                string selSql = "SELECT smtp_client, mail_user_name, mail_password, smtp_port FROM sec.sec_email_servers WHERE (is_default = 't')";
                DataSet selDtSt = this.selectDataNoParams(selSql);
                int m = selDtSt.Tables[0].Rows.Count;
                string smtpClnt = "";
                string fromEmlNm = "";
                string fromPswd = "";
                int portNo = 0;
                if (selDtSt.Tables[0].Rows.Count > 0)
                {
                    smtpClnt = selDtSt.Tables[0].Rows[0][0].ToString();
                    fromEmlNm = selDtSt.Tables[0].Rows[0][1].ToString();
                    fromPswd = selDtSt.Tables[0].Rows[0][2].ToString();
                    portNo = int.Parse(selDtSt.Tables[0].Rows[0][3].ToString());
                }
                MailAddress fromAddress = new MailAddress(fromEmlNm.Trim());
                string fromPassword = this.decrypt(fromPswd, CommonCodes.AppKey);

                MailMessage mail = new MailMessage();
                //fromAddress.Address, toEmails[0].Trim(), sbjct.Trim(), bdyTxt.Trim()
                SmtpClient SmtpServer = new SmtpClient(smtpClnt);
                mail.IsBodyHtml = true;
                mail.From = new MailAddress(fromAddress.Address);
                string[] spltChars = { ";" };
                string[] toEmails = toEml.Trim().Split(spltChars, StringSplitOptions.RemoveEmptyEntries);
                string[] ccEmails = ccEml.Trim().Split(spltChars, StringSplitOptions.RemoveEmptyEntries);
                string[] bccEmails = bccEml.Trim().Split(spltChars, StringSplitOptions.RemoveEmptyEntries);
                string[] attchMnts = attchmnt.Trim().Split(spltChars, StringSplitOptions.RemoveEmptyEntries);
                int i = 0;
                int lovID = this.getLovID("Email Addresses to Ignore");
                int toMailsAdded = 0;
                for (i = 0; i < toEmails.Length; i++)
                {
                    if (this.isEmailValid(toEmails[i], lovID))
                    {
                        if (this.getEnbldPssblValID(toEmails[i], lovID) <= 0)
                        {
                            mail.To.Add(toEmails[i]);
                            toMailsAdded++;
                        }
                        else
                        {
                            errMsg += "Address:" + toEmails[i] + " blacklisted by Admin!\r\n";
                        }
                    }
                    else
                    {
                        errMsg += "Address:" + toEmails[i] + " is Invalid!\r\n";
                    }
                }
                if (toMailsAdded <= 0)
                {
                    return false;
                }

                for (i = 0; i < ccEmails.Length; i++)
                {
                    if (this.isEmailValid(ccEmails[i], lovID))
                    {
                        if (this.getEnbldPssblValID(ccEmails[i], lovID) <= 0)
                        {
                            mail.CC.Add(ccEmails[i]);
                        }
                        else
                        {
                            errMsg += "Address:" + ccEmails[i] + " blacklisted by Admin!\r\n";
                        }
                    }
                    else
                    {
                        errMsg += "Address:" + ccEmails[i] + " is Invalid!\r\n";
                    }
                }

                for (i = 0; i < bccEmails.Length; i++)
                {
                    if (this.isEmailValid(bccEmails[i], lovID))
                    {
                        if (this.getEnbldPssblValID(bccEmails[i], lovID) <= 0)
                        {
                            mail.Bcc.Add(bccEmails[i]);
                        }
                        else
                        {
                            errMsg += "Address:" + bccEmails[i] + " blacklisted by Admin!\r\n";
                        }
                    }
                    else
                    {
                        errMsg += "Address:" + bccEmails[i] + " is Invalid!\r\n";
                    }
                }
                for (i = 0; i < attchMnts.Length; i++)
                {
                    Attachment attch1 = new Attachment(attchMnts[i]);
                    mail.Attachments.Add(attch1);
                }
                List<LinkedResource> resources = new List<LinkedResource>();
                string[] imgLocation = new string[20];
                int mtcIdx = 0;
                string imgTagSrc = "";
                foreach (Match mtch in Regex.Matches(bdyTxt, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase | RegexOptions.Multiline))
                {
                    try
                    {
                        imgLocation[mtcIdx] = mtch.Groups[1].Value;
                        imgTagSrc = imgLocation[mtcIdx];
                        if (imgLocation[mtcIdx].ToLower().Contains("http://")
                            || imgLocation[mtcIdx].ToLower().Contains("https://"))
                        {
                            imgTagSrc = this.getRptDrctry() + @"\mail_attachments\http_file_dwnld_" + msgIdentifier + "_" + (mtcIdx + 1).ToString() + Path.GetExtension(imgLocation[mtcIdx]);
                            if (!System.IO.File.Exists(imgTagSrc))
                            {
                                WebClient Client = new WebClient();
                                Client.DownloadFile(imgLocation[mtcIdx], imgTagSrc);
                            }
                        }
                        if (imgLocation[mtcIdx].Contains("cid:"))
                        {
                            continue;
                        }
                        LinkedResource inline = new LinkedResource(imgLocation[mtcIdx].Replace("file:///", ""));
                        inline.ContentId = "LnkdResource" + (mtcIdx + 1).ToString();
                        bdyTxt = bdyTxt.Replace(imgLocation[mtcIdx], @"cid:" + inline.ContentId + @"");
                        resources.Add(inline);
                        mtcIdx++;
                        if (mtcIdx == 20)
                        {
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        errMsg += "Error Occured:" + ex.Message + "\r\nOldImgTagSrc" + imgLocation[mtcIdx] + "\r\nNewImgTagSrc:" + imgTagSrc;
                        mtcIdx++;
                        if (mtcIdx == 20)
                        {
                            break;
                        }
                    }
                }
                mail.Subject = sbjct;
                if (bdyTxt.Contains("<body") == false
                    || bdyTxt.Contains("</body>") == false)
                {
                    bdyTxt = "<body>" + bdyTxt + "</body>";
                }
                if (bdyTxt.Contains("<html") == false
                        || bdyTxt.Contains("</html>") == false)
                {
                    bdyTxt = "<!DOCTYPE html><html lang=\"en\">" + bdyTxt + "</html>";
                }
                AlternateView avImages = AlternateView.CreateAlternateViewFromString(bdyTxt, null, MediaTypeNames.Text.Html);
                resources.ForEach(x => avImages.LinkedResources.Add(x));
                mail.AlternateViews.Add(avImages);
                mail.Body = bdyTxt;
                mail.IsBodyHtml = true;
                //mail.BodyEncoding
                SmtpServer.Port = portNo;
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;

                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential(fromAddress.Address, fromPassword);
                //SmtpServer.Credentials = CredentialCache.DefaultNetworkCredentials;
                SmtpServer.EnableSsl = true;
                //System.Windows.Forms.Application.DoEvents();
                //this.showMsg("Test!\r\n" + SmtpServer.Host + "\r\n" + fromAddress.Address +
                //"\r\n" + fromPassword + "\r\n" + SmtpServer.Port + "\r\n" + mail.From.Address + "\r\nTo Email:" + mail.To.ToString() + "\r\n", 3);
                //System.Windows.Forms.Application.DoEvents();
                if (this.CheckForInternetConnection())
                {
                    SmtpServer.Send(mail);
                    return true;
                }
                errMsg += "No Internet Connection";
                return false;
            }
            catch (Exception ex)
            {
                errMsg += "Failed to send Email! " + ex.Message + "\r\n" + ex.StackTrace + "\r\n" + ex.InnerException + "\r\n";
                return false;
            }
        }
        public bool sendSMS(string msgBody, string rcpntNo, ref string errMsg)
        {
            if (!this.CheckForInternetConnection())
            {
                errMsg = "No Internet Connection";
                return false;
            }
            string response = "";
            msgBody = msgBody.Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ").Replace("|", "/");
            string succsTxt = "";
            char[] trmChrs = { '{', '}', '"' };
            char[] w = { ',' };
            char[] x = { ':' };
            char[] y = { '|' };
            System.Net.ServicePointManager.Expect100Continue = false;
            string url = "";// "http://txtconnect.co/api/send/";
            System.Net.WebClient client = new System.Net.WebClient();
            System.Collections.Specialized.NameValueCollection postData = new
            System.Collections.Specialized.NameValueCollection();
            DataSet dtst = this.selectDataNoParams(@"select sms_param1, sms_param2, sms_param3, 
                                                sms_param4, sms_param5, sms_param6, 
                                                sms_param7, sms_param8, sms_param9, sms_param10 
                                                from sec.sec_email_servers where is_default='t'");
            string[] nwMsgBdy;
            string rvsdMsgBdy = "";
            for (int z = 0; z < msgBody.Length; z++)
            {
                if (z > 0 && (z % 160) == 0)
                {
                    rvsdMsgBdy += msgBody.Substring(z, 1) + "|";
                }
                else
                {
                    rvsdMsgBdy += msgBody.Substring(z, 1);
                }
            }
            nwMsgBdy = rvsdMsgBdy.Split(y, StringSplitOptions.RemoveEmptyEntries);
            for (int z = 0; z < nwMsgBdy.Length; z++)
            {
                client = new System.Net.WebClient();
                postData = new
              System.Collections.Specialized.NameValueCollection();
                string[] paramNms = new string[10];
                string[] paramVals = new string[10];
                string tmpStr = "";
                string[] tmpArry;
                for (int i = 0; i < dtst.Tables[0].Columns.Count; i++)
                {
                    tmpStr = dtst.Tables[0].Rows[0][i].ToString().Trim().Trim(y).Trim();
                    tmpArry = tmpStr.Split(y, StringSplitOptions.RemoveEmptyEntries);

                    if (tmpStr == ""
                      || tmpArry.Length != 2)
                    {
                        paramNms[i] = "";
                        paramVals[i] = "";
                    }
                    else
                    {
                        paramNms[i] = tmpArry[0];
                        paramVals[i] = tmpArry[1];
                    }
                    if (paramNms[i] == "url")
                    {
                        url = paramVals[i];
                    }
                    else if (paramNms[i] == "success txt")
                    {
                        succsTxt = paramVals[i];
                    }
                    else if (paramNms[i] != "" && paramVals[i] != "")
                    {
                        postData.Add(paramNms[i], paramVals[i].Replace("{:msg}", nwMsgBdy[z]).Replace("{:to}", rcpntNo));
                    }
                }
                byte[] responseBytes = client.UploadValues(url, "POST", postData);
                //System.Threading.Thread.Sleep(500);
                response += System.Text.Encoding.ASCII.GetString(responseBytes);
            }
            if (response.ToLower().Contains(succsTxt.ToLower()))
            {
                errMsg += "SMS Successful";
                return true;
            }
            errMsg += response;
            return false;
        }


        public void updatePhoneNumbers()
        {
            //this.saveLabel.Text = "Reformating Contact Details...Please Wait...";
            //this.saveLabel.Visible = true;
            //System.Windows.Forms.Application.DoEvents();
            string strSQL = @"SELECT person_id, 
                           local_id_no,
                           email, 
                           cntct_no_tel, 
                           cntct_no_mobl,  
                           cntct_no_fax
                        FROM prs.prsn_names_nos 
                         WHERE 1=1 ORDER BY 1";
            DataSet dtst = this.selectDataNoParams(strSQL);
            int ttl = dtst.Tables[0].Rows.Count;
            for (int i = 0; i < ttl; i++)
            {
                //this.saveLabel.Text = "Reformating Contact Details(" + (i + 1).ToString() + "/" + ttl + ")...Please Wait...";
                //System.Windows.Forms.Application.DoEvents();
                string email = dtst.Tables[0].Rows[i][2].ToString();
                string cntcNo = dtst.Tables[0].Rows[i][3].ToString();
                string cntcMobl = dtst.Tables[0].Rows[i][4].ToString();
                string cntcFax = dtst.Tables[0].Rows[i][5].ToString();
                long prsnID = long.Parse(dtst.Tables[0].Rows[i][0].ToString());
                char[] w = { ',' };
                char[] trmChr = { ',', ' ' };
                email = email.Replace("/", ",").Replace(@"\", ",").Replace(",", ", ").Replace("  ", " ").Trim(trmChr);
                cntcNo = cntcNo.Replace("/", ",").Replace(@"\", ",").Replace(",", ", ").Replace("  ", " ").Trim(trmChr);
                cntcMobl = cntcMobl.Replace("/", ",").Replace(@"\", ",").Replace(",", ", ").Replace("  ", " ").Trim(trmChr);
                cntcFax = cntcFax.Replace("/", ",").Replace(@"\", ",").Replace(",", ", ").Replace("  ", " ").Trim(trmChr);

                string[] emails = email.Split(w, StringSplitOptions.RemoveEmptyEntries);
                string[] cntcNos = cntcNo.Split(w, StringSplitOptions.RemoveEmptyEntries);
                string[] cntcMobls = cntcMobl.Split(w, StringSplitOptions.RemoveEmptyEntries);
                for (int y = 0; y < cntcMobls.Length; y++)
                {
                    if (cntcMobls[y].Trim().Length == 10)
                    {
                        if (cntcMobls[y].Trim().Substring(0, 1) == "0")
                        {
                            cntcMobls[y] = "+233" + cntcMobls[y].Trim().Substring(1);
                        }
                    }
                }
                string[] cntcFaxs = cntcFax.Split(w, StringSplitOptions.RemoveEmptyEntries);

                string updtSQL = @"UPDATE prs.prsn_names_nos SET 
                           email='" + email.Replace("'", "''") + @"', 
                           cntct_no_tel='" + cntcNo.Replace("'", "''") + @"', 
                           cntct_no_mobl='" + string.Join(", ", cntcMobls).Replace("'", "''") + @"',  
                           cntct_no_fax='" + cntcFax.Replace("'", "''") + @"' WHERE person_id=" + prsnID;
                this.updateDataNoParams(updtSQL);
            }

            //this.saveLabel.Visible = false;
            //System.Windows.Forms.Application.DoEvents();
        }

        public string makeSMSRestCall(string msgBody, string rcpntNo)
        {
            //this.updatePhoneNumbers();
            var client = new RestClient();
            client.EndPoint = @"http://txtconnect.co/api/send/";
            client.Method = HttpVerb.POST;
            client.PostData = "{postData: value}";
            string token = "";
            string fromNm = "GhIE";
            var json = client.MakeRequest("?token=" + token + "&msg=" + msgBody.Replace("&", " and ").Replace("  ", " ") +
              "&from=" + fromNm +
              "&to=" + rcpntNo);
            return json;
        }
        #endregion

        public void clearPrvExclFiles()
        {
            try
            {
                this.dataRng = null;
                this.trgtSheets = new Excel.Worksheet[1];
                if (this.nwWrkBk != null)
                {
                    this.nwWrkBk = new Excel.Workbook();
                    this.nwWrkBk = null;
                }
                if (this.exclApp != null)
                {
                    this.exclApp.Quit();
                    this.exclApp = null;
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            catch
            {
            }
        }

        public int[] getQtNRem(int[] number)
        {
            int[] no = number;
            if (number[0] <= 26)
            {
                return number;
            }
            else
            {
                number[0] = number[0] - 26;
                number[1] = number[1] + 1;
                return getQtNRem(number);
            }
        }

        public string getExclColNm(int colNo)
        {
            //Eg. colNo 1580 = BHT  2  8 20
            //52
            if (colNo == 0)
            {
                return "";
            }
            string[] letters = {"A", "A","B","C","D","E","F","G","H","I",
   "J","K","L","M","N","O","P","Q","R","S","T","U",
   "V","W","X","Y","Z"};
            int quotientAns = colNo;
            int[] num = { quotientAns, 0 };
            string resStr = "";
            if (quotientAns <= 26)
            {
                resStr = letters[quotientAns] + resStr;
                return resStr;
            }
            do
            {
                num = getQtNRem(num);
                quotientAns = num[1];
                resStr = letters[num[0]] + resStr;
                num[0] = quotientAns;
                num[1] = 0;
            }
            while (quotientAns > 26);

            if (quotientAns <= 26)
            {
                resStr = letters[quotientAns] + resStr;
            }
            return resStr;
        }

        public string cnvrtBitStrToYN(string bitstr)
        {
            if (bitstr == "1")
            {
                return "YES";
            }
            return "NO";
        }

        public bool cnvrtYNToBool(string yesno)
        {
            if (yesno.ToUpper() == "YES")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<string> getPeriodDates(
        string startDte, string endDte, string periodTyp)
        {
            DateTime dte1 = DateTime.Parse(DateTime.Parse(startDte).ToString("dd-MMM-yyyy 00:00:00"));
            DateTime dte2 = DateTime.Parse(DateTime.Parse(endDte).ToString("dd-MMM-yyyy 23:59:50"));
            List<string> resArray = new List<string>();
            string nwstr = dte1.ToString("dd-MMM-yyyy 00:00:00");
            resArray.Add(nwstr);
            bool evenOdd = false;//false-begin date true - end date
            if (periodTyp == "Annually")
            {
                do
                {
                    evenOdd = !evenOdd;
                    if (evenOdd)
                    {
                        nwstr = DateTime.Parse(dte1.AddMonths(12).AddDays(-1).ToString("dd-MMM-yyyy 23:59:50")).ToString("dd-MMM-yyyy 23:59:50");
                        dte1 = DateTime.Parse(DateTime.Parse(nwstr).AddDays(1).ToString("dd-MMM-yyyy 00:00:00"));
                    }
                    else
                    {
                        nwstr = dte1.ToString("dd-MMM-yyyy 00:00:00");
                    }
                    if (DateTime.Parse(nwstr) < dte2)
                    {
                        resArray.Add(nwstr);
                    }
                    else
                    {
                        nwstr = dte2.ToString("dd-MMM-yyyy 23:59:50");
                        resArray.Add(nwstr);
                    }
                }
                while (DateTime.Parse(nwstr) < dte2);
            }
            else if (periodTyp == "Half Yearly")
            {
                do
                {
                    evenOdd = !evenOdd;
                    if (evenOdd)
                    {
                        nwstr = DateTime.Parse(dte1.AddMonths(6).AddDays(-1).ToString("dd-MMM-yyyy 23:59:50")).ToString("dd-MMM-yyyy 23:59:50");
                        dte1 = DateTime.Parse(DateTime.Parse(nwstr).AddDays(1).ToString("dd-MMM-yyyy 00:00:00"));
                    }
                    else
                    {
                        nwstr = dte1.ToString("dd-MMM-yyyy 00:00:00");
                    }
                    if (DateTime.Parse(nwstr) < dte2)
                    {
                        resArray.Add(nwstr);
                    }
                    else
                    {
                        nwstr = dte2.ToString("dd-MMM-yyyy 23:59:50");
                        resArray.Add(nwstr);
                    }
                }
                while (DateTime.Parse(nwstr) < dte2);
            }
            else if (periodTyp == "Quarterly")
            {
                do
                {
                    evenOdd = !evenOdd;
                    if (evenOdd)
                    {
                        nwstr = DateTime.Parse(dte1.AddMonths(3).AddDays(-1).ToString("dd-MMM-yyyy 23:59:50")).ToString("dd-MMM-yyyy 23:59:50");
                        dte1 = DateTime.Parse(DateTime.Parse(nwstr).AddDays(1).ToString("dd-MMM-yyyy 00:00:00"));
                    }
                    else
                    {
                        nwstr = dte1.ToString("dd-MMM-yyyy 00:00:00");
                    }
                    if (DateTime.Parse(nwstr) < dte2)
                    {
                        resArray.Add(nwstr);
                    }
                    else
                    {
                        nwstr = dte2.ToString("dd-MMM-yyyy 23:59:50");
                        resArray.Add(nwstr);
                    }
                }
                while (DateTime.Parse(nwstr) < dte2);
            }
            else if (periodTyp == "Monthly")
            {
                do
                {
                    evenOdd = !evenOdd;
                    if (evenOdd)
                    {
                        nwstr = DateTime.Parse(dte1.AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy 23:59:50")).ToString("dd-MMM-yyyy 23:59:50");
                        dte1 = DateTime.Parse(DateTime.Parse(nwstr).AddDays(1).ToString("dd-MMM-yyyy 00:00:00"));
                    }
                    else
                    {
                        nwstr = dte1.ToString("dd-MMM-yyyy 00:00:00");
                    }
                    if (DateTime.Parse(nwstr) < dte2)
                    {
                        resArray.Add(nwstr);
                    }
                    else
                    {
                        nwstr = dte2.ToString("dd-MMM-yyyy 23:59:50");
                        resArray.Add(nwstr);
                    }
                }
                while (DateTime.Parse(nwstr) < dte2);
            }
            else if (periodTyp == "Fortnightly")
            {
                do
                {
                    evenOdd = !evenOdd;
                    if (evenOdd)
                    {
                        nwstr = DateTime.Parse(dte1.AddDays(14).AddDays(-1).ToString("dd-MMM-yyyy 23:59:50")).ToString("dd-MMM-yyyy 23:59:50");
                        dte1 = DateTime.Parse(DateTime.Parse(nwstr).AddDays(1).ToString("dd-MMM-yyyy 00:00:00"));
                    }
                    else
                    {
                        nwstr = dte1.ToString("dd-MMM-yyyy 00:00:00");
                    }
                    if (DateTime.Parse(nwstr) < dte2)
                    {
                        resArray.Add(nwstr);
                    }
                    else
                    {
                        nwstr = dte2.ToString("dd-MMM-yyyy 23:59:50");
                        resArray.Add(nwstr);
                    }
                }
                while (DateTime.Parse(nwstr) < dte2);
            }
            else if (periodTyp == "Weekly")
            {
                do
                {
                    evenOdd = !evenOdd;
                    if (evenOdd)
                    {
                        nwstr = DateTime.Parse(dte1.AddDays(7).AddDays(-1).ToString("dd-MMM-yyyy 23:59:50")).ToString("dd-MMM-yyyy 23:59:50");
                        dte1 = DateTime.Parse(DateTime.Parse(nwstr).AddDays(1).ToString("dd-MMM-yyyy 00:00:00"));
                    }
                    else
                    {
                        nwstr = dte1.ToString("dd-MMM-yyyy 00:00:00");
                    }
                    if (DateTime.Parse(nwstr) < dte2)
                    {
                        resArray.Add(nwstr);
                    }
                    else
                    {
                        nwstr = dte2.ToString("dd-MMM-yyyy 23:59:50");
                        resArray.Add(nwstr);
                    }
                }
                while (DateTime.Parse(nwstr) < dte2);
            }
            return resArray;
        }

        public void listViewKeyDown(ListView lstvw, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A && e.Control)
            {
                lstvw.MultiSelect = true;
                foreach (ListViewItem item in lstvw.Items)
                {
                    item.Selected = true;
                }
            }
            else if (e.KeyCode == Keys.C && e.Control)
            {
                StringBuilder buffer = new StringBuilder();
                int colcnt = lstvw.Columns.Count;
                for (int i = 0; i < colcnt; i++)
                {
                    buffer.Append(lstvw.Columns[i].Name);
                    if (i < colcnt - 1)
                    {
                        buffer.Append("\t");
                    }
                    else
                    {
                        buffer.Append("\n");
                    }
                }

                foreach (ListViewItem curItem in lstvw.SelectedItems)
                {
                    buffer.Append(curItem.Text);
                    buffer.Append("\t");
                    for (int i = 1; i < colcnt; i++)
                    {
                        buffer.Append(curItem.SubItems[i].Text);
                        if (i < colcnt - 1)
                        {
                            buffer.Append("\t");
                        }
                        else
                        {
                            buffer.Append("\n");
                        }
                    }
                }
                Clipboard.SetDataObject(buffer.ToString());
            }
        }
        string seenDB = "";
        public Color[] getColors()
        {
            if (CommonCodes.myFrmClrs != null && this.seenDB == CommonCodes.Db_dbase)
            {
                if (CommonCodes.myFrmClrs.Length == 3)
                {
                    return CommonCodes.myFrmClrs;
                }
            }
            else
            {
                this.seenDB = CommonCodes.Db_dbase;
            }
            StreamReader fileReader;
            Color[] clrs = { Color.FromArgb(0, 102, 160), Color.FromArgb(0, 129, 206), Color.FromArgb(0, 255, 0) };
            CommonCodes.myFrmClrs = clrs;
            string fileLoc = "";
            if (CommonCodes.Db_dbase != "")
            {
                int dbaseLovID = this.getLovID("Per Database Background Themes");
                string dbaseBackColor = this.getEnbldPssblValDesc(
                  CommonCodes.Db_dbase, dbaseLovID);
                if (dbaseBackColor != "")
                {
                    fileLoc = @dbaseBackColor;
                }
            }
            if (fileLoc == "" || !this.myComputer.FileSystem.FileExists(fileLoc))
            {
                if (CommonCodes.Db_dbase.Contains("test")
              || CommonCodes.Db_dbase.Contains("try")
              || CommonCodes.Db_dbase.Contains("trial")
              || CommonCodes.Db_dbase.Contains("train")
              || CommonCodes.Db_dbase.Contains("sample"))
                {
                    fileLoc = @"DBInfo\Default_Test.rtheme";
                }
                else
                {
                    fileLoc = @"DBInfo\Default.rtheme";
                }
            }
            if (this.myComputer.FileSystem.FileExists(fileLoc))
            {
                fileReader = this.myComputer.FileSystem.OpenTextFileReader(fileLoc);
                try
                {
                    char[] cho = { ',' };
                    string[] bck = fileReader.ReadLine().Split(cho, StringSplitOptions.RemoveEmptyEntries);
                    CommonCodes.myFrmClrs[0] = Color.FromArgb(int.Parse(bck[0]), int.Parse(bck[1]), int.Parse(bck[2]));
                    string[] btm = fileReader.ReadLine().Split(cho, StringSplitOptions.RemoveEmptyEntries);
                    CommonCodes.myFrmClrs[1] = Color.FromArgb(int.Parse(btm[0]), int.Parse(btm[1]), int.Parse(btm[2]));
                    string[] btm1 = fileReader.ReadLine().Split(cho, StringSplitOptions.RemoveEmptyEntries);
                    CommonCodes.myFrmClrs[2] = Color.FromArgb(int.Parse(btm1[0]), int.Parse(btm1[1]), int.Parse(btm1[2]));
                    CommonCodes.AutoConnect = cnvrtBitStrToBool(fileReader.ReadLine());
                    string mdlsght = fileReader.ReadLine();
                    if (mdlsght == "")
                    {
                        mdlsght = CommonCode.CommonCodes.ModulesNeeded;
                    }
                    else
                    {
                        CommonCode.CommonCodes.ModulesNeeded = mdlsght;
                    }
                    fileReader.Close();
                    fileReader = null;
                    return CommonCodes.myFrmClrs;
                }
                catch
                {
                    fileReader.Close();
                    fileReader = null;
                    return CommonCodes.myFrmClrs;
                }
            }
            return CommonCodes.myFrmClrs;
        }

        public string breakTxtDownHTML(string inptTxt, int allwdWidth)
        {
            string res = "";
            int strtIdx = 0;
            for (int i = 0; i < inptTxt.Length; i++)
            {
                if (strtIdx >= allwdWidth - 1
                  && inptTxt.Substring(i, 1) == " ")
                {
                    res = res + inptTxt.Substring(i, 1) + "<br/>";
                    strtIdx = 0;
                }
                else
                {
                    res = res + inptTxt.Substring(i, 1);
                    strtIdx++;
                }
            }
            return res;
        }

        public string[] breakPDFTxtDown(string inptTxt, float allwdWidth, Font fnt, Graphics g)
        {
            List<string> nwstr = new List<string>();
            string nwln = "";
            float lnWidth = 0;
            int lnCntr = 0;
            inptTxt = inptTxt.Replace("\r\n", " ").Replace("\n", " ").Replace("\r", " ");
            char[] chstr = { ' ' };
            string[] nwInpt = inptTxt.Split(chstr, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < nwInpt.Length; i++)
            {
                SizeF sze = g.MeasureString(nwInpt[i] + " ", fnt);
                lnWidth += sze.Width;
                if (lnWidth > allwdWidth)
                {
                    nwstr.Add(nwln);
                    nwln = nwInpt[i] + " ";
                    //nwln = "";
                    lnWidth = sze.Width;
                }
                else
                {
                    nwln = nwln + nwInpt[i] + " ";
                }
                lnCntr++;
                if ((i == nwInpt.Length - 1) &&
                  (lnWidth <= allwdWidth) &&
                  (nwln != ""))
                {
                    nwstr.Add(nwln);
                }
            }
            string[] rslts = new string[nwstr.Count];
            rslts = nwstr.ToArray();
            return rslts;
        }

        public string insrtSpaces(string inptTxt, int allwdWidth)
        {
            string nwstr = "";
            for (int i = 0; i < inptTxt.Length; i++)
            {
                nwstr = nwstr + inptTxt.Substring(i, 1);
                if ((nwstr.Length >= allwdWidth) && (i % allwdWidth) == 0)
                {
                    nwstr = nwstr + " ";
                }
            }
            return nwstr;
        }

        public string[] breakPOSTxtDown(string inptTxt, float allwdWidth, Font fnt, Graphics g, int numChars)
        {
            inptTxt = this.insrtSpaces(inptTxt, numChars);
            List<string> nwstr = new List<string>();
            string nwln = "";
            float lnWidth = 0;
            int lnCntr = 0;
            inptTxt = inptTxt.Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
            char[] chstr = { ' ' };
            string[] nwInpt = inptTxt.Split(chstr, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < nwInpt.Length; i++)
            {
                SizeF sze = g.MeasureString(nwInpt[i] + " ", fnt);
                lnWidth += sze.Width;
                if (lnWidth >= allwdWidth && i > 0)
                {
                    nwstr.Add(nwln);
                    //if (i < nwInpt.Length - 1)
                    //{
                    //}
                    nwln = nwInpt[i] + " ";
                    //nwln = "";
                    lnWidth = sze.Width;
                }
                else
                {
                    nwln = nwln + nwInpt[i] + " ";
                }
                lnCntr++;
                if ((i == nwInpt.Length - 1) &&
                  (nwln != ""))
                {
                    nwstr.Add(nwln);
                }
                //(lnWidth <= allwdWidth) &&
            }
            string[] rslts = new string[nwstr.Count];
            rslts = nwstr.ToArray();
            return rslts;
        }

        public string[] breakTxtDown(string inptTxt, float allwdWidth, Font fnt, Graphics g)
        {
            List<string> nwstr = new List<string>();
            List<string> fnlstr = new List<string>();
            string nwln = "";
            float lnWidth = 0;
            int lnCntr = 0;
            string str1 = "A";

            int numchars = (int)((allwdWidth / (g.MeasureString(str1, fnt)).Width) * 1.4);

            inptTxt = inptTxt.Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
            char[] chstr = { ' ' };
            string[] nwInpt = inptTxt.Split(chstr, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < nwInpt.Length; i++)
            {
                SizeF sze = g.MeasureString(nwInpt[i] + " ", fnt);
                lnWidth += sze.Width;
                if (lnWidth >= allwdWidth && i > 0)
                {
                    nwstr.Add(nwln);
                    nwln = nwInpt[i] + " ";
                    lnWidth = sze.Width;
                }
                else
                {
                    nwln = nwln + nwInpt[i] + " ";
                }
                lnCntr++;
                if ((i == nwInpt.Length - 1) &&
                  (nwln != ""))
                {
                    nwstr.Add(nwln);
                }
            }

            for (int i = 0; i < nwstr.Count; i++)
            {
                if (g.MeasureString(nwstr[i], fnt).Width <= allwdWidth)
                {
                    fnlstr.Add(nwstr[i]);
                }
                else
                {
                    //if (numchars < nwstr[i].Length)
                    //{
                    //  fnlstr.Add(nwstr[i].Substring(0, numchars));
                    //}
                    //else
                    //{
                    //  fnlstr.Add(nwstr[i]);
                    //}
                    if (numchars < nwstr[i].Length && nwstr[i].Trim().Contains(" ") == false)
                    {
                        string[] nwnwStr = this.breakPOSTxtDown(nwstr[i], allwdWidth, fnt, g, numchars);
                        for (int j = 0; j < nwnwStr.Length; j++)
                        {
                            fnlstr.Add(nwnwStr[j]);
                        }
                    }
                    else
                    {
                        fnlstr.Add(nwstr[i]);
                    }
                }
            }
            string[] rslts = new string[fnlstr.Count];
            rslts = fnlstr.ToArray();
            return rslts;
        }

        public string[] breakTxtDownML(string inptTxt, float allwdWidth, Font fnt, Graphics g)
        {
            List<string> nwstr = new List<string>();
            List<string> fnlstr = new List<string>();
            string nwln = "";
            float lnWidth = 0;
            int lnCntr = 0;
            string str1 = "A";

            int numchars = (int)((allwdWidth / (g.MeasureString(str1, fnt)).Width) * 1.4);

            inptTxt = inptTxt.Replace("\r\n", " ~").Replace("\n", " ~").Replace("\r", " ~");
            char[] chstr = { ' ' };
            string[] nwInpt = inptTxt.Split(chstr, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < nwInpt.Length; i++)
            {
                SizeF sze = g.MeasureString(nwInpt[i] + " ", fnt);
                lnWidth += sze.Width;
                if ((lnWidth >= allwdWidth || nwInpt[i].StartsWith("~")) && i > 0)
                {
                    nwstr.Add(nwln);
                    nwln = nwInpt[i] + " ";
                    lnWidth = sze.Width;
                }
                else
                {
                    nwln = nwln + nwInpt[i] + " ";
                }
                lnCntr++;
                if (((i == nwInpt.Length - 1)) &&
                  (nwln != ""))
                {
                    nwstr.Add(nwln);
                }
            }

            for (int i = 0; i < nwstr.Count; i++)
            {
                if (g.MeasureString(nwstr[i], fnt).Width <= allwdWidth)
                {
                    fnlstr.Add(nwstr[i].Replace("~", ""));
                }
                else
                {
                    //.Replace("~", "\r\n")
                    if (numchars < nwstr[i].Length && nwstr[i].Trim().Contains(" ") == false)
                    {
                        string[] nwnwStr = this.breakPOSTxtDown(nwstr[i], allwdWidth, fnt, g, numchars);
                        for (int j = 0; j < nwnwStr.Length; j++)
                        {
                            fnlstr.Add(nwnwStr[j].Replace("~", ""));
                        }
                    }
                    else
                    {
                        fnlstr.Add(nwstr[i].Replace("~", ""));
                    }
                }
            }
            string[] rslts = new string[fnlstr.Count];
            rslts = fnlstr.ToArray();
            return rslts;
        }

        public string[] breakRptTxtDown(string inptTxt, float allwdWidth, Font fnt, Graphics g)
        {
            List<string> nwstr = new List<string>();
            string nwln = "";
            float lnWidth = 0;
            int lnCntr = 0;
            inptTxt = inptTxt.Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
            char[] chstr = { '|' };
            string[] nwInpt = inptTxt.Split(chstr, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < nwInpt.Length; i++)
            {
                SizeF sze = g.MeasureString(nwInpt[i] + " ", fnt);
                lnWidth += sze.Width;
                if (lnWidth > allwdWidth)
                {
                    nwstr.Add(nwln);
                    nwln = nwInpt[i] + " ";
                    //nwln = "";
                    lnWidth = sze.Width;
                }
                else
                {
                    nwln = nwln + nwInpt[i] + " ";
                }
                lnCntr++;
                if ((i == nwInpt.Length - 1) &&
                  (lnWidth <= allwdWidth) &&
                  (nwln != ""))
                {
                    nwstr.Add(nwln);
                }
            }
            string[] rslts = new string[nwstr.Count];
            rslts = nwstr.ToArray();
            return rslts;
        }

        public int findCharIndx(string inp_char, string[] inpArry)
        {
            for (int i = 0; i < inpArry.Length; i++)
            {
                if (inpArry[i] == inp_char)
                {
                    return i;
                }
            }
            return -1;
        }

        public string dBEncrypt(string inpt)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "SELECT MD5('" + inpt + "')";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public string encrypt(string inpt, string key)
        {
            char pdChr = '0';
            int numChars = 5433;// this.getRandomInt(1000, 5999);
            int numChars1 = 8279;// this.getRandomInt(6000, 9000);
            string encrptdLen = (inpt.Length + numChars).ToString().PadLeft(4, pdChr);
            string encrptdLen1 = (inpt.Length + numChars1).ToString().PadLeft(4, pdChr);

            inpt = numChars.ToString() + encrptdLen + inpt + numChars1.ToString() + encrptdLen1;

            string fnl_str = "";
            string[] charset1 = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L",
                                                        "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X",
                                                        "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
                                                        "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l",
                                                        "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x",
                                                        "y", "z"};
            /*string[] charset2 = { "e", "q", "0", "P", "3", "i", "D", "O", "V", "8", "E", "6", 
                                                                                                          "B", "Z", "A", "W", "5", "g", "G", "F", "H", "u", "t", "s",
                                                                                                          "C", "K", "d", "p", "r", "w", "z", "x", "a", "c", "1", "m",
                                                                                                          "I", "f", "Q", "L", "v", "Y", "j", "S", "R", "o", "J", "4",
                                                                                                          "9", "h", "7", "M", "b", "X", "k", "N", "l", "n", "2", "y",
                                                                                                          "T", "U"};*/
            string keyString = this.getNewKey(key);
            string[] charset2 = new string[keyString.Length];
            int cntr = keyString.Length;
            for (int i = 0; i < cntr; i++)
            {
                charset2[i] = keyString[i].ToString();
            }
            string[] wldChars = { "`", "¬", "!", "\"", "£", "$", "%", "^", "&", "*", "(", ")",
                                                                                                    "-",    "_", "=", "+",  "{",    "[",    "]",    "}",    ":",    ";",    "@",    "'",
                                                                                                    "#",    "~", "/", "?", ">", ".", "<", ",", "\\", "|" };
            for (int i = inpt.Length - 1; i >= 0; i--)
            {
                string tst_str = inpt.Substring(i, 1);
                int j = this.findCharIndx(tst_str, charset1);
                if (j == -1)
                {
                    int k = this.findCharIndx(tst_str, wldChars);
                    if (k == -1)
                    {
                        fnl_str += tst_str;
                    }
                    else
                    {
                        fnl_str += charset2[k] + "_";
                    }
                }
                else
                {
                    fnl_str += charset2[j];
                }
            }
            return fnl_str;
        }

        public string encrypt1(string inpt, string key)
        {
            char pdChr = '0';
            int numChars = this.getRandomInt(1000, 5999);
            int numChars1 = this.getRandomInt(6000, 9000);
            string encrptdLen = (inpt.Length + numChars).ToString().PadLeft(4, pdChr);
            string encrptdLen1 = (inpt.Length + numChars1).ToString().PadLeft(4, pdChr);

            inpt = numChars.ToString() + encrptdLen + inpt + numChars1.ToString() + encrptdLen1;

            string fnl_str = "";
            string[] charset1 = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L",
                                                                                                    "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X",
                                                                                                    "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
                                                                                                    "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l",
                                                                                                    "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x",
                                                                                                    "y", "z"};
            /*string[] charset2 = { "e", "q", "0", "P", "3", "i", "D", "O", "V", "8", "E", "6", 
                                                                                                          "B", "Z", "A", "W", "5", "g", "G", "F", "H", "u", "t", "s",
                                                                                                          "C", "K", "d", "p", "r", "w", "z", "x", "a", "c", "1", "m",
                                                                                                          "I", "f", "Q", "L", "v", "Y", "j", "S", "R", "o", "J", "4",
                                                                                                          "9", "h", "7", "M", "b", "X", "k", "N", "l", "n", "2", "y",
                                                                                                          "T", "U"};*/
            string keyString = this.getNewKey(key);
            string[] charset2 = new string[keyString.Length];
            int cntr = keyString.Length;
            for (int i = 0; i < cntr; i++)
            {
                charset2[i] = keyString[i].ToString();
            }
            string[] wldChars = { "`", "¬", "!", "\"", "£", "$", "%", "^", "&", "*", "(", ")",
                                                                                                    "-",    "_", "=", "+",  "{",    "[",    "]",    "}",    ":",    ";",    "@",    "'",
                                                                                                    "#",    "~", "/", "?", ">", ".", "<", ",", "\\", "|" };
            for (int i = inpt.Length - 1; i >= 0; i--)
            {
                string tst_str = inpt.Substring(i, 1);
                int j = this.findCharIndx(tst_str, charset1);
                if (j == -1)
                {
                    int k = this.findCharIndx(tst_str, wldChars);
                    if (k == -1)
                    {
                        fnl_str += tst_str;
                    }
                    else
                    {
                        fnl_str += charset2[k] + "_";
                    }
                }
                else
                {
                    fnl_str += charset2[j];
                }
            }
            return fnl_str;
        }

        public string getNewKey(string key)
        {
            string[] charset1 = new string[key.Length];
            int cntr = key.Length;
            for (int i = 0; i < cntr; i++)
            {
                charset1[i] = key[i].ToString();
            }
            string[] charset2 = {
        "e", "q", "0", "P", "3", "i", "D", "O", "V", "8", "E", "6",
        "B", "Z", "A", "W", "5", "g", "G", "F", "H", "u", "t", "s",
        "C", "K", "d", "p", "r", "w", "z", "x", "a", "c", "1", "m",
        "I", "f", "Q", "L", "v", "Y", "j", "S", "R", "o", "J", "4",
        "9", "h", "7", "M", "b", "X", "k", "N", "l", "n", "2", "y",
        "T", "U"};
            string[] wldChars = {"`", "¬", "!", "\"", "£", "$", "%", "^", "&", "*", "(", ")",
        "-", "_", "=", "+", "{", "[", "]", "}", ":", ";", "@", "'",
        "#", "~", "/", "?", ">", ".", "<", ",", "\\", "|", " "};
            int keyLength = charset1.Length;
            string newKey = "";
            for (int i = keyLength - 1; i >= 0; i--)
            {
                if (findCharIndx(charset1[i], wldChars) > -1)
                {
                    continue;
                }
                if (newKey.Contains(charset1[i]) == false)
                {
                    newKey += charset1[i];
                }
                if (newKey.Length >= 62)
                {
                    break;
                }
            }

            if (newKey.Length < 62)
            {
                keyLength = charset2.Length;
                for (int i = keyLength - 1; i >= 0; i--)
                {
                    if (newKey.Contains(charset2[i]) == false)
                    {
                        newKey += charset2[i];
                    }
                    if (newKey.Length >= 62)
                    {
                        break;
                    }
                }
            }
            return newKey;
        }

        public string decrypt(string inpt, string key)
        {
            try
            {
                string fnl_str = "";
                string[] charset1 = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L",
                                                                                                    "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X",
                                                                                                    "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
                                                                                                    "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l",
                                                                                                    "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x",
                                                                                                    "y", "z"};
                string keyString = this.getNewKey(key);
                string[] charset2 = new string[keyString.Length];
                int cntr = keyString.Length;
                for (int i = 0; i < cntr; i++)
                {
                    charset2[i] = keyString[i].ToString();
                }

                string[] wldChars = { "`", "¬", "!", "\"", "£", "$", "%", "^", "&", "*", "(", ")",
                                                                                                    "-",    "_", "=", "+",  "{",    "[",    "]",    "}",    ":",    ";",    "@",    "'",
                                                                                                    "#",    "~", "/", "?", ">", ".", "<", ",", "\\", "|" };
                int wldCharsLen = wldChars.Length;

                for (int i = inpt.Length - 1; i >= 0; i--)
                {
                    string tst_str = inpt.Substring(i, 1);
                    if (tst_str == "_")
                    {
                        continue;
                    }
                    int j = this.findCharIndx(tst_str, charset2);
                    if (j == -1)
                    {
                        fnl_str += tst_str;
                    }
                    else
                    {
                        if (i < inpt.Length - 1)
                        {
                            if (inpt.Substring(i + 1, 1) == "_" && j < wldCharsLen)
                            {
                                fnl_str += wldChars[j];
                            }
                            else
                            {
                                fnl_str += charset1[j];
                            }
                        }
                        else
                        {
                            fnl_str += charset1[j];
                        }
                    }
                }
                string nwStr1 = fnl_str.Substring(0, 4);
                string nwStr2 = fnl_str.Substring(4, 4);
                int stringLn = int.Parse(nwStr2) - int.Parse(nwStr1);
                string nwStr3 = fnl_str.Substring(8, stringLn);
                return nwStr3;
            }
            catch (Exception ex)
            {
                return inpt;
            }
        }

        public string getHardDriveNo()
        {
            string hdno = "None";
            ManagementObjectSearcher searcher = new
             ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");

            foreach (ManagementObject wmi_HD in searcher.Get())
            {
                // get the hard drive from collection
                // using index
                // HardDrive hd = (HardDrive)hdCollection[i];
                // get the hardware serial no.

                if (wmi_HD["SerialNumber"] == null)
                {
                    hdno = "None";
                }
                else
                {
                    hdno = wmi_HD["SerialNumber"].ToString();
                    return hdno;
                }
            }
            return hdno;
        }

        public string getRequestCode()
        {
            //NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            string macAddrs = "RHOMICOM" + this.getHardDriveNo();
            //foreach (NetworkInterface nic in nics)
            //{
            //  if (nic.NetworkInterfaceType != NetworkInterfaceType.Loopback
            //   && nic.NetworkInterfaceType != NetworkInterfaceType.Tunnel
            //   && nic.NetworkInterfaceType != NetworkInterfaceType.GenericModem
            //   && nic.Description.ToLower().Contains("virtual") == false
            //   && nic.Description.ToLower().Contains("mobile") == false
            //   && nic.Description.ToLower().Contains("loopback") == false)
            //  {
            //    macAddrs += nic.GetPhysicalAddress().ToString();//Mac Address of the Computer
            //  }
            //}
            string fnl_str = this.encrypt1(this.dBEncrypt(macAddrs.Replace("0", "")), CommonCodes.AppKey);
            if (fnl_str.Length > 25)
            {
                fnl_str = fnl_str.Substring(0, 25);
            }
            return fnl_str.ToUpper();
        }

        public string getExpctdActvtnKey(string rqstCode)
        {
            string fnl_str = this.decrypt(this.dBEncrypt(this.encrypt(rqstCode, CommonCodes.AppKey)), CommonCodes.AppKey).ToUpper();
            if (fnl_str.Length > 25)
            {
                fnl_str = fnl_str.Substring(0, 25);
            }
            return fnl_str;
        }

        public string getRegistryVal(string keyname, string prdctNm)
        {
            RegistryKey rk = Registry.LocalMachine;
            RegistryKey sk1 = rk.OpenSubKey(@"SOFTWARE\" + prdctNm);
            if (sk1 == null)
            {
                return "";
            }
            else
            {
                try
                {
                    return (string)sk1.GetValue(keyname);
                }
                catch (Exception ex)
                {
                    this.showMsg(ex.Message, 0);
                    return "";
                }
            }
        }

        public string get64RegistryVal(string keyname, string prdctNm)
        {
            RegistryKey rk = Registry.LocalMachine;
            RegistryKey sk1 = rk.OpenSubKey(@"SOFTWARE\Wow6432Node\" + prdctNm);
            if (sk1 == null)
            {
                return "";
            }
            else
            {
                try
                {
                    return (string)sk1.GetValue(keyname);
                }
                catch (Exception ex)
                {
                    this.showMsg(ex.Message, 0);
                    return "";
                }
            }
        }

        public static bool InternalCheckIsWow64()
        {
            if ((Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor >= 1) ||
                Environment.OSVersion.Version.Major >= 6)
            {
                using (System.Diagnostics.Process p = System.Diagnostics.Process.GetCurrentProcess())
                {
                    bool retVal;
                    if (!IsWow64Process(p.Handle, out retVal))
                    {
                        return false;
                    }
                    return retVal;
                }
            }
            else
            {
                return false;
            }
        }

        public bool writeValToRegstry(string keyname, object value)
        {
            try
            {
                RegistryKey rk = Registry.LocalMachine;
                RegistryKey sk1 = rk.CreateSubKey("SOFTWARE\\" + CommonCodes.AppName);
                sk1.SetValue(keyname, value);
                return true;
            }
            catch (Exception ex)
            {
                this.showMsg(ex.Message, 0);
                return false;
            }
        }

        public int getDOWNum(string weekday)
        {
            string[] dynms = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
            for (int i = 0; i < 7; i++)
            {
                if (dynms[i] == weekday)
                {
                    return i + 1;
                }
            }
            return -1;
        }

        public bool isThsMchnPrmtd()
        {
            //if (this.getRegistryVal("RHO_KEY", this.appName) ==
            // this.getExpctdActvtnKey(this.getRequestCode()))
            //{
            //  return true;
            //}
            //else
            //{
            //  activateDiag nwDiag = new activateDiag();
            //  nwDiag.con = this.pgSqlConn;
            //  DialogResult dgRes = nwDiag.ShowDialog();
            //  if (dgRes == DialogResult.OK)
            //  {
            //    if (nwDiag.actvated == true)
            //    {
            //      return true;
            //    }
            //  }
            //}
            //return false;
            return true;
        }

        public void showActvtnForm()
        {
            activateDiag nwDiag = new activateDiag();
            //nwDiag.con = CommonCode.GlobalSQLConn;
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        private string[] GetIP()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            string[] nameIP = new string[3];
            string strHostName = "";
            strHostName = System.Net.Dns.GetHostName();
            IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);


            foreach (NetworkInterface nic in nics)
            {
                if ((nic.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                    nic.NetworkInterfaceType != NetworkInterfaceType.Tunnel) &&
                    nic.Description.Contains("Loopback") == false)
                {
                    nameIP[0] = ipEntry.HostName;
                    nameIP[1] = nic.GetPhysicalAddress().ToString();
                    foreach (IPAddressInformation unicstAddr in nic.GetIPProperties().UnicastAddresses)
                    {
                        if (unicstAddr.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            nameIP[2] = unicstAddr.Address.ToString();
                            return nameIP;
                        }
                    }
                    return nameIP;
                }
            }
            nameIP[0] = strHostName;
            nameIP[1] = "Unknown";
            nameIP[2] = System.Net.IPAddress.Any.ToString();
            return nameIP;
        }

        public string[] getMachDetails()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            string[] nameIP = new string[3];
            nameIP[0] = "";
            nameIP[1] = "";
            nameIP[2] = "";
            string strHostName = "";
            strHostName = System.Net.Dns.GetHostName();
            IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);

            //First Check Connected Interfaces
            foreach (NetworkInterface nic in nics)
            {
                if ((nic.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                    nic.NetworkInterfaceType != NetworkInterfaceType.Tunnel) &&
                    nic.Description.Contains("Loopback") == false &&
                    nic.OperationalStatus == OperationalStatus.Up)
                {
                    nameIP[0] = ipEntry.HostName;//Host Name of the Computer
                    nameIP[1] = nic.GetPhysicalAddress().ToString();//Mac Address of the Computer
                    foreach (IPAddressInformation unicstAddr in nic.GetIPProperties().UnicastAddresses)
                    {
                        if (unicstAddr.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            if (unicstAddr.Address == null || unicstAddr.Address.ToString() == "")
                            {
                                continue;
                            }
                            nameIP[2] = unicstAddr.Address.ToString();//IP Address of the Computer
                            return nameIP;
                        }
                    }
                    return nameIP;
                }
            }

            //Then Check Disconnected Interfaces
            foreach (NetworkInterface nic in nics)
            {
                if ((nic.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                    nic.NetworkInterfaceType != NetworkInterfaceType.Tunnel) &&
                    nic.Description.Contains("Loopback") == false &&
                    nic.OperationalStatus != OperationalStatus.Up)
                {
                    nameIP[0] = ipEntry.HostName;//Host Name of the Computer
                    nameIP[1] = nic.GetPhysicalAddress().ToString();//Mac Address of the Computer
                    foreach (IPAddressInformation unicstAddr in nic.GetIPProperties().UnicastAddresses)
                    {
                        if (unicstAddr.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            if (unicstAddr.Address == null || unicstAddr.Address.ToString() == "")
                            {
                                continue;
                            }
                            nameIP[2] = unicstAddr.Address.ToString();//IP Address of the Computer
                            return nameIP;
                        }
                    }
                    return nameIP;
                }
            }

            nameIP[0] = strHostName;//Host Name of the Computer
            nameIP[1] = "Unknown";//Mac Address of the Computer
            nameIP[2] = System.Net.IPAddress.Any.ToString();//IP Address of the Computer
            return nameIP;
        }

        public string[] getMachMacAddrs()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            string[] nameIP = new string[3];
            string strHostName = "";
            strHostName = System.Net.Dns.GetHostName();
            IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);

            foreach (NetworkInterface nic in nics)
            {
                if ((nic.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                    nic.NetworkInterfaceType != NetworkInterfaceType.Tunnel) &&
                    nic.Description.Contains("Loopback") == false && nic.Name.Contains("RHO_LAN"))
                {
                    nameIP[0] = ipEntry.HostName;//Host Name of the Computer
                    nameIP[1] = nic.GetPhysicalAddress().ToString();//Mac Address of the Computer
                    nameIP[2] = nic.Name;
                    return nameIP;
                }
            }

            nameIP[0] = strHostName;//Host Name of the Computer
            nameIP[1] = "Unknown";//Mac Address of the Computer
            nameIP[2] = "Unknown";//IP Address of the Computer
            return nameIP;
        }

        public string cnvrtBoolToBitStr(bool testval)
        {
            if (testval)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }

        public bool cnvrtBitStrToBool(string testval)
        {
            if (testval == "0")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool test_prmssns(string testdata)
        {
            char[] dlmtrs = { '~' };
            string[] prldgs_to_test = testdata.Split(dlmtrs, StringSplitOptions.RemoveEmptyEntries);
            string slctdRoles = "";
            for (int i = 0; i < this.Role_Set_IDs.Length; i++)
            {
                slctdRoles = slctdRoles + this.Role_Set_IDs[i].ToString();
                if (i < this.Role_Set_IDs.Length - 1)
                {
                    slctdRoles = slctdRoles + ";";
                }
            }
            bool[] chkRslts = new bool[prldgs_to_test.Length];
            for (int m = 0; m < chkRslts.Length; m++)
            {
                chkRslts[m] = false;
            }

            for (int j = 0; j < prldgs_to_test.Length; j++)
            {
                if (this.doSlctdRolesHvThisPrvldg(
                 this.getPrvldgID(prldgs_to_test[j]), slctdRoles) == true)
                {
                    chkRslts[j] = true;
                }
            }

            for (int n = 0; n < chkRslts.Length; n++)
            {
                if (chkRslts[n] == false)
                {
                    return false;
                }
            }
            return true;
            /*if (this.doCurRolesHvThsPrvldgs(prldgs_to_test) == false)
            {
                return false;
            }
            else
            {
                return true;
            }*/
        }

        public bool doSlctdRolesHvThisPrvldg(int inp_prvldg_id, string inSlctdRl)
        {
            //Checks whether a given role 'system administrator' has a given priviledge
            string slctdRl = ";" + inSlctdRl + ";";
            string sqlStr = "SELECT role_id FROM sec.sec_roles_n_prvldgs WHERE ((prvldg_id = " +
                    inp_prvldg_id + ") AND (trim('" + slctdRl + @"') ilike trim('%;' || role_id || ';%')) 
                AND (now() between to_timestamp(valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') 
            AND to_timestamp(valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS')))";
            DataSet dtst = this.selectDataNoParams(sqlStr);
            //echo $sqlStr;
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool isDteTmeWthnIntrvl(string in_date, string intrval)
        {
            //
            string sqlStr = "SELECT age(now(), to_timestamp('" + in_date + "', 'DD-Mon-YYYY HH24:MI:SS')) " +
                   "<= interval '" + intrval + "'";
            DataSet dtst = this.selectDataNoParams(sqlStr);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                if (dtst.Tables[0].Rows[0][0].ToString() == "True")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public bool doesDteTmeExceedIntrvl(string in_date, string intrval)
        {
            //
            string sqlStr = "SELECT age(now(), to_timestamp('" +
                             in_date + "', 'DD-Mon-YYYY HH24:MI:SS')) " +
                            " > interval '" + intrval + "'";
            DataSet dtst = this.selectDataNoParams(sqlStr);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                //Global.mnFrm.cmCde.showMsg(dtst.Tables[0].Rows[0][0].ToString(), 0);
                if (dtst.Tables[0].Rows[0][0].ToString() == "True")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public void checkNAssignReqrmnts()
        {
            if (this.getModuleID(this.ModuleName) == -1)
            {
                this.registerThsModule();
            }
            if (this.getRoleID(this.SampleRole) == -1)
            {
                this.createSampleRole(this.SampleRole);
            }
            this.checkNCreatePrvldgs(this.DefaultPrvldgs, this.SampleRole);
            if (this.SubGrpNames != null)
            {
                this.checkNCreateSubGroups(this.SubGrpNames, this.MainTableNames, this.KeyColumnNames);
            }
        }

        public void checkNCreatePrvldgs(string[] brgtPrvldgs, string roleNm)
        {
            for (int i = 0; i < brgtPrvldgs.Length; i++)
            {
                if (this.getPrvldgID(brgtPrvldgs[i]) == -1)
                {
                    this.createPrvldg(brgtPrvldgs[i]);
                }
                if (this.hasRoleEvrHdThsPrvlg(this.getRoleID(roleNm),
              this.getPrvldgID(brgtPrvldgs[i])) == false)
                {
                    this.asgnPrvlgToSmplRole(this.getPrvldgID(brgtPrvldgs[i]), roleNm);
                }
            }
        }

        public void checkNCreateSubGroups(string[] brgtGrps, string[] brgtTbls, string[] brgtKeyCols)
        {
            int mdlID = this.getModuleID(this.ModuleName);
            for (int i = 0; i < brgtGrps.Length; i++)
            {
                if (this.getMdlGrpID(brgtGrps[i]) == -1)
                {
                    this.registerThsModulesSubgroups(brgtGrps[i], brgtTbls[i], brgtKeyCols[i], mdlID);
                }
                else
                {
                }
            }
        }

        public void selectDate(ref TextBox mytxt)
        {
            calendarDiag nwDiag = new calendarDiag();
            //nwDiag.Parent = myfrm;
            nwDiag.Location = new System.Drawing.Point(
             mytxt.Parent.Location.X + mytxt.Location.X + (int)(mytxt.Width),
             mytxt.Parent.Location.Y + mytxt.Location.Y + 22);
            nwDiag.selectedDateComboBox.Text = mytxt.Text;
            nwDiag.setDate();
            System.Windows.Forms.Application.DoEvents();
            Color[] clrs = this.getColors();
            nwDiag.BackColor = clrs[0];

            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == System.Windows.Forms.DialogResult.OK)
            {
                mytxt.Text = nwDiag.selectedDateComboBox.Text;
            }
        }

        /// <summary>
        /// MsgTyp - Message Types
        /// {0 = OK Btn, Warning Ico}
        /// {1 = YES/NO Btn, Warning Ico}
        /// {2 = YES/NO Btn, Question Ico}
        /// {3 = OK Btn, Info Ico}
        /// {4 = OK Btn, Error Ico}
        /// </summary>
        public DialogResult showMsg(string inpStr, int msgTyp)
        {
            DialogResult dgRes = DialogResult.OK;
            if (msgTyp == 0)
            {
                dgRes = MessageBox.Show(inpStr, "Rhomicom Message!",
             MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (msgTyp == 1)
            {
                dgRes = MessageBox.Show(inpStr, "Rhomicom Message!",
                 MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
            else if (msgTyp == 2)
            {
                dgRes = MessageBox.Show(inpStr, "Rhomicom Message!",
                 MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            }
            else if (msgTyp == 3)
            {
                dgRes = MessageBox.Show(inpStr, "Rhomicom Message!",
                 MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (msgTyp == 4)
            {
                dgRes = MessageBox.Show(inpStr, "Rhomicom Message!",
                 MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dgRes;
        }

        public void showSQL(string inpStr, int shwSqlPrvldgIndx)
        {
            if (this.test_prmssns(this.DefaultPrvldgs[shwSqlPrvldgIndx]) == false)
            {
                this.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            viewSQLDiag nwDiag = new viewSQLDiag();
            System.Windows.Forms.Application.DoEvents();
            Color[] clrs = this.getColors();
            nwDiag.BackColor = clrs[0];
            nwDiag.textBox1.Text = inpStr;
            DialogResult dgres = nwDiag.ShowDialog();
            if (dgres == DialogResult.OK)
            {
            }
        }

        public void showSQLNoPermsn(string inpStr)
        {
            viewSQLDiag nwDiag = new viewSQLDiag();
            nwDiag.textBox1.Text = inpStr;
            System.Windows.Forms.Application.DoEvents();
            Color[] clrs = this.getColors();
            nwDiag.BackColor = clrs[0];
            DialogResult dgres = nwDiag.ShowDialog();
            if (dgres == DialogResult.OK)
            {
            }
        }

        public void showTxtNoPermsn(ref string inpStr)
        {
            viewSQLDiag nwDiag = new viewSQLDiag();
            nwDiag.textBox1.ReadOnly = false;
            nwDiag.textBox1.BackColor = Color.White;
            nwDiag.textBox1.Text = inpStr;
            nwDiag.Width = 650;
            nwDiag.Height = 550;
            System.Windows.Forms.Application.DoEvents();
            Color[] clrs = this.getColors();
            nwDiag.BackColor = clrs[0];
            DialogResult dgres = nwDiag.ShowDialog();
            if (dgres == DialogResult.OK)
            {
                inpStr = nwDiag.textBox1.Text;
            }
        }

        public void showLogMsg(long msgid, string logTblNm)
        {
            vwLogMsgForm nwDiag = new vwLogMsgForm();
            nwDiag.richTextBox1.Text = this.getLogMsg(msgid, logTblNm);
            System.Windows.Forms.Application.DoEvents();
            Color[] clrs = this.getColors();
            nwDiag.BackColor = clrs[0];
            DialogResult dgres = nwDiag.ShowDialog();
            if (dgres == DialogResult.OK)
            {
            }
        }

        public void createLogMsg(string logmsg, string logTblNm,
          string procstyp, long procsID, string dateStr)
        {
            //string dateStr = this.getDB_Date_time();
            dateStr = DateTime.ParseExact(
         dateStr, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            string insSQL = "INSERT INTO " + logTblNm + "(" +
                  "log_messages, process_typ, process_id, created_by, creation_date, " +
                  "last_update_by, last_update_date) " +
                  "VALUES ('" + logmsg.Replace("'", "''") +
                  "','" + procstyp.Replace("'", "''") + "'," + procsID +
                  ", " + this.User_id + ", '" + dateStr +
                  "', " + this.User_id + ", '" + dateStr +
                  "')";
            this.insertDataNoParams(insSQL);
        }

        public void updateLogMsg(long msgid, string logmsg,
          string logTblNm, string dateStr)
        {
            dateStr = DateTime.ParseExact(
         dateStr, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            this.Extra_Adt_Trl_Info = "";
            //string dateStr = this.getDB_Date_time();
            string updtSQL = "UPDATE " + logTblNm + " " +
            "SET log_messages=log_messages || '" + logmsg.Replace("'", "''") +
            "', last_update_by=" + this.User_id +
            ", last_update_date='" + dateStr +
            "' WHERE msg_id = " + msgid;
            this.updateDataNoParams(updtSQL);
        }

        public string getLogMsg(long msgid, string logTblNm)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select log_messages from " + logTblNm + " where msg_id = " +
             msgid + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public long getLogMsgID(string logTblNm, string procstyp, long procsID)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select msg_id from " + logTblNm +
              " where process_typ = '" + procstyp.Replace("'", "''") +
              "' and process_id = " + procsID + "";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public void showRecHstry(string inpStr, int shwRcHstryPrvldgIndx)
        {
            if (this.test_prmssns(this.DefaultPrvldgs[shwRcHstryPrvldgIndx]) == false)
            {
                this.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            recHstryDiag nwDiag = new recHstryDiag();
            nwDiag.textBox1.Text = inpStr;
            System.Windows.Forms.Application.DoEvents();
            Color[] clrs = this.getColors();
            nwDiag.BackColor = clrs[0];
            DialogResult dgres = nwDiag.ShowDialog();
            if (dgres == DialogResult.OK)
            {
            }
        }

        public string get_Gnrl_Rec_Hstry(long rowID, string tblnm, string id_col_nm)
        {
            string strSQL = @"SELECT a.created_by, 
to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), 
a.last_update_by, 
to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') " +
            "FROM " + tblnm + " a WHERE(a." + id_col_nm + " = " + rowID + ")";
            string fnl_str = "";
            DataSet dtst = this.selectDataNoParams(strSQL);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                fnl_str = "CREATED BY: " + this.get_user_name(long.Parse(dtst.Tables[0].Rows[0][0].ToString())) +
                 "\r\nCREATION DATE: " + dtst.Tables[0].Rows[0][1].ToString() + "\r\nLAST UPDATE BY:" +
                 this.get_user_name(long.Parse(dtst.Tables[0].Rows[0][2].ToString())) +
                 "\r\nLAST UPDATE DATE: " + dtst.Tables[0].Rows[0][3].ToString();
                return fnl_str;
            }
            else
            {
                return "";
            }
        }

        public string get_Gnrl_Create_Hstry(long rowID, string tblnm, string id_col_nm)
        {
            string strSQL = @"SELECT a.created_by, 
to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') " +
            "FROM " + tblnm + " a WHERE(a." + id_col_nm + " = " + rowID + ")";
            string fnl_str = "";
            DataSet dtst = this.selectDataNoParams(strSQL);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                fnl_str = "CREATED BY: " + this.get_user_name(long.Parse(dtst.Tables[0].Rows[0][0].ToString())) +
                 "\r\nCREATION DATE: " + dtst.Tables[0].Rows[0][1].ToString();
                return fnl_str;
            }
            else
            {
                return "";
            }
        }

        public long getGnrlRecID(string tblNm, string srchcol, string rtrnCol, string recname, int orgid)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select " + rtrnCol + " from " + tblNm + " where lower(" + srchcol + ") = '" +
             recname.Replace("'", "''").ToLower() + "' and org_id = " + orgid;
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public long getRecCount(string tblNm, string srchcol, string colToCnt, string srchWrds)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select count(" + colToCnt + ") from " + tblNm + " where lower(" + srchcol + ") like '" +
             srchWrds.Replace("'", "''").ToLower() + "'";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public long getGnrlRecID(string tblNm, string srchcol, string rtrnCol, string recname)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select " + rtrnCol + " from " + tblNm + " where lower(" + srchcol + ") = '" +
             recname.Replace("'", "''").ToLower() + "'";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public string getGnrlRecNm(string tblNm, string srchcol, string rtrnCol, long recid)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select " + rtrnCol + " from " + tblNm + " where " + srchcol + " = " + recid;
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public string getGnrlRecNm(string tblNm, string srchcol, string rtrnCol, string srchword)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select " + rtrnCol + " from " + tblNm + " where " + srchcol + " = '" + srchword.Replace("'", "''") + "'";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public void createLovNm(string lovNm, string lovDesc, bool isDyn
          , string sqlQry, string dfndBy, bool isEnbld)
        {
            string dateStr = this.getDB_Date_time();
            string sqlStr = "INSERT INTO gst.gen_stp_lov_names(" +
                  "value_list_name, value_list_desc, is_list_dynamic, " +
                  "sqlquery_if_dyn, defined_by, created_by, creation_date, last_update_by, " +
                  "last_update_date, is_enabled) " +
              "VALUES ('" + lovNm.Replace("'", "''") + "', '" + lovDesc.Replace("'", "''") +
          "', '" + this.cnvrtBoolToBitStr(isDyn) + "', '" + sqlQry.Replace("'", "''") +
          "', '" + dfndBy.Replace("'", "''") +
              "', " + this.User_id + ", '" + dateStr + "', " + this.User_id +
              ", '" + dateStr + "', '" + this.cnvrtBoolToBitStr(isEnbld) + "')";
            this.insertDataNoParams(sqlStr);
        }

        public void updateLovNm(int lovID, bool isDyn
          , string sqlQry, string dfndBy, bool isEnbld)
        {
            string dateStr = this.getDB_Date_time();
            string sqlStr = "UPDATE gst.gen_stp_lov_names SET " +
                  "is_list_dynamic='" + this.cnvrtBoolToBitStr(isDyn) + "', " +
                  "sqlquery_if_dyn='" + sqlQry.Replace("'", "''") +
          "', defined_by='" + dfndBy.Replace("'", "''") +
              "', last_update_by=" + this.User_id + ", " +
                  "last_update_date='" + dateStr +
                  "', is_enabled='" + this.cnvrtBoolToBitStr(isEnbld) + "' WHERE value_list_id = " + lovID;
            this.updateDataNoParams(sqlStr);
        }

        public void createPssblValsForLov(int lovID, string pssblVal,
          string pssblValDesc, bool isEnbld, string allwd)
        {
            string dateStr = this.getDB_Date_time();
            string sqlStr = "INSERT INTO gst.gen_stp_lov_values(" +
                  "value_list_id, pssbl_value, pssbl_value_desc, " +
                              "created_by, creation_date, last_update_by, " +
                              "last_update_date, is_enabled, allowed_org_ids) " +
              "VALUES (" + lovID + ", '" + pssblVal.Replace("'", "''") + "', '" +
              pssblValDesc.Replace("'", "''") +
              "', " + this.User_id + ", '" + dateStr + "', " + this.User_id +
              ", '" + dateStr + "', '" +
              this.cnvrtBoolToBitStr(isEnbld) +
              "', '" + allwd.Replace("'", "''") + "')";
            this.insertDataNoParams(sqlStr);
        }

        public void updatePssblValsForLov(int pssblValID, string pssblVal,
          string pssblValDesc, bool isEnbld, string allwd)
        {
            string dateStr = this.getDB_Date_time();
            this.Extra_Adt_Trl_Info = "";
            string sqlStr = "UPDATE gst.gen_stp_lov_values SET " +
                  "pssbl_value='" + pssblVal.Replace("'", "''") + "', pssbl_value_desc='" +
              pssblValDesc.Replace("'", "''") +
              "', last_update_by=" + this.User_id + ", " +
                              "last_update_date='" + dateStr + "', is_enabled='" +
              this.cnvrtBoolToBitStr(isEnbld) +
              "', allowed_org_ids='" + allwd.Replace("'", "''") + "' WHERE pssbl_value_id = " + pssblValID;
            this.updateDataNoParams(sqlStr);
        }

        public void createSysLovs(string[] sysLovs, string[] sysLovsDynQrys, string[] sysLovsDesc)
        {
            for (int i = 0; i < sysLovs.Length; i++)
            {
                int lovID = this.getLovID(sysLovs[i]);
                if (lovID <= 0)
                {
                    if (sysLovsDynQrys[i] == "")
                    {
                        this.createLovNm(sysLovs[i],
                         sysLovsDesc[i], false, "", "SYS", true);
                    }
                    else
                    {
                        this.createLovNm(sysLovs[i],
                   sysLovsDesc[i], true, sysLovsDynQrys[i], "SYS", true);
                    }
                }
                else
                {
                    if (sysLovsDynQrys[i] != "")
                    {
                        this.updateLovNm(lovID, true, sysLovsDynQrys[i], "SYS", true);
                    }
                }
            }
        }

        public string get_all_OrgIDs()
        {
            string strSql = "";
            strSql = "SELECT distinct org_id FROM org.org_details";
            DataSet dtst = this.selectDataNoParams(strSql);
            string allwd = ",";
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                allwd += dtst.Tables[0].Rows[i][0].ToString() + ",";
            }
            return allwd;
        }

        public string concatCurRoleIDs()
        {
            string nwStr = "-1000000";
            int totl = this.Role_Set_IDs.Length;
            for (int i = 0; i < totl; i++)
            {
                nwStr = nwStr + "," + this.Role_Set_IDs[i].ToString();
                if (i < totl - 1)
                {
                    //nwStr = nwStr + ",";
                }
            }
            return nwStr;
        }

        public string get_Rpt_SQL(long rptID)
        {
            string strSql = "SELECT rpt_sql_query " +
       "FROM rpt.rpt_reports WHERE report_id = " + rptID;

            DataSet dtst = this.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return dtst.Tables[0].Rows[0][0].ToString();
            }
            return "";
        }

        public DataSet get_Rpt_ColsToAct(long rptID)
        {
            string strSql = "SELECT cols_to_group, cols_to_count, cols_to_sum, cols_to_average, cols_to_no_frmt " +
            "FROM rpt.rpt_reports WHERE report_id = " + rptID;

            DataSet dtst = this.selectDataNoParams(strSql);
            return dtst;
        }

        public double computeMathExprsn(string exprSn)
        {
            string strSql = "";
            strSql = "SELECT " + exprSn.Replace("/", "::float/").Replace("=", "").Replace(",", "").Replace("'", "''");

            DataSet dtst = this.selectDataNoParams1(strSql);
            if (dtst.Tables.Count <= 0)
            {
                return 0;
            }
            else if (dtst.Tables[0].Rows.Count > 0)
            {
                return double.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return 0;
        }

        public DataSet get_AllParams(long rptID)
        {
            string strSql = "SELECT parameter_id, parameter_name, paramtr_rprstn_nm_in_query, default_value, " +
       "is_required, lov_name_id, param_data_type, date_format FROM rpt.rpt_report_parameters WHERE report_id = " + rptID + " ORDER BY parameter_name";

            DataSet dtst = this.selectDataNoParams(strSql);
            return dtst;
        }

        public long getRptRnID(long rptID, long runBy, string runDate)
        {
            runDate = DateTime.ParseExact(
       runDate, "dd-MMM-yyyy HH:mm:ss",
       System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            DataSet dtSt = new DataSet();
            string sqlStr = "select rpt_run_id from rpt.rpt_report_runs where run_by = " +
              runBy + " and report_id = " + rptID + " and run_date = '" +
             runDate + "' order by rpt_run_id DESC";
            dtSt = this.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public string get_RptRnOutpt(long rptRnID)
        {
            string strSql = "SELECT rpt_run_output " +
       "FROM rpt.rpt_report_runs WHERE rpt_run_id = " + rptRnID;

            DataSet dtst = this.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return dtst.Tables[0].Rows[0][0].ToString();
            }
            return "";
        }

        public string get_LastPatchVrsn()
        {
            string strSql = "";
            strSql = "SELECT patch_version_nm FROM sec.sec_appld_patches ORDER BY patch_id DESC LIMIT 1 OFFSET 0";
            DataSet dtst = this.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return dtst.Tables[0].Rows[0][0].ToString();
            }
            return "";
        }

        public void createSysLovsPssblVals(string[] sysLovs, string[] pssblVals)
        {
            string allwd = this.get_all_OrgIDs();
            for (int i = 0; i < pssblVals.Length; i += 3)
            {
                if (this.getPssblValID(pssblVals[i + 1],
                  this.getLovID(sysLovs[int.Parse(pssblVals[i])]), pssblVals[i + 2]) <= 0)
                {
                    this.createPssblValsForLov(this.getLovID(sysLovs[int.Parse(pssblVals[i])]),
                      pssblVals[i + 1], pssblVals[i + 2], true, allwd);
                }
            }
        }

        public void exprtToExcel(DataSet dtst)
        {
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.dtst = dtst;
            nwDiag.data_source_id = 1;
            nwDiag.orgID = this.Org_id;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void openExcel(string filename)
        {
            System.Windows.Forms.Application.DoEvents();
            this.exclApp = new Microsoft.Office.Interop.Excel.Application();
            //this.exclApp.WindowState = Excel.XlWindowState.xlNormal;
            this.exclApp.Visible = true;
            this.exclApp.ShowWindowsInTaskbar = true;
            this.exclApp.ShowStartupDialog = true;
            this.exclApp.ScreenUpdating = true;
            this.exclApp.DisplayAlerts = true;
            //this.exclApp.DisplayFullScreen = true;
            //System.Windows.Forms.Application.DoEvents();
            // Call this way:

            SetWindowPos((IntPtr)this.exclApp.Hwnd, HWND_TOP, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_SHOWWINDOW);

            this.nwWrkBk = this.exclApp.Workbooks.Open(filename, 0, false, 5,
              "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
              "", true, false, 0, true, false, false);

            //SetWindowPos((IntPtr)this.exclApp.Hwnd, HWND_NOTOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_SHOWWINDOW);

            //this.trgtSheets = new Excel.Worksheet[1];

            //this.trgtSheets[0] = (Excel.Worksheet)this.nwWrkBk.Worksheets[1];
            //this.exclApp.DoubleClick();
        }

        public void exprtToExcel(DataSet dtst, string fileNm)
        {
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.dtst = dtst;
            nwDiag.data_source_id = 72;
            nwDiag.orgID = this.Org_id;
            nwDiag.cmnCde = this;
            nwDiag.exlfileNm = fileNm;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void exprtToHTML(DataSet dtst, string fileNm, string rptTitle)
        {
            //string dirStr = cmnCde.getRptDrctry();
            //@"\amcharts_2100\samples\"
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fileNm);
            // Do not change lines / spaces b/w words.
            StringBuilder strSB = new StringBuilder("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" " +
              "\"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"[]><html xmlns=\"http://www.w3.org/1999/xhtml\" dir=\"ltr\" lang=\"en-US\" xml:lang=\"en\"><head><meta http-equiv=\"Content-Type\" " +
                "content=\"text/html; charset=utf-8\"><title>" + rptTitle + "</title>" +
              "<link rel=\"stylesheet\" href=\"../amcharts/rpt.css\" type=\"text/css\"></head><body>");
            strSB.AppendLine("<table><caption align=\"top\">" + rptTitle + "</caption><thead>");

            int wdth = 0;
            for (int d = 0; d < dtst.Tables[0].Columns.Count; d++)
            {
                wdth = dtst.Tables[0].Columns[d].ColumnName.Length * 3;
                strSB.AppendLine("<th width=\"" + wdth + "px\">" + dtst.Tables[0].Columns[d].ColumnName.Replace(" ", "&nbsp;") + "</th>");
            }
            strSB.AppendLine("</thead><tbody>");

            for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
            {
                strSB.AppendLine("<tr>");
                for (int d = 0; d < dtst.Tables[0].Columns.Count; d++)
                {
                    wdth = dtst.Tables[0].Columns[d].ColumnName.Length * 3;
                    strSB.AppendLine("<td width=\"" + wdth + "px\">" + this.breakTxtDownHTML(dtst.Tables[0].Rows[a][d].ToString(),
                      dtst.Tables[0].Columns[d].ColumnName.Length).Replace(" ", "&nbsp;") + "</td>");//.Replace(" ", "&nbsp;")
                }
                strSB.AppendLine("</tr>");
            }

            strSB.AppendLine("</tbody></table>");
            strSB.AppendLine("</body></html>");
            sw.WriteLine(strSB);
            sw.Dispose();
            sw.Close();
            System.Windows.Forms.Application.DoEvents();
        }

        public void exprtToHTML(DataSet dtst, string fileNm, string rptTitle
          , string[] colsToGrp, string[] colsToCnt,
          string[] colsToSum, string[] colsToAvrg, string[] colsToFrmt)
        {
            int colCnt = dtst.Tables[0].Columns.Count;
            long totlLen = 0;
            for (int d = 0; d < colCnt; d++)
            {
                totlLen += dtst.Tables[0].Columns[d].ColumnName.Length;
            }
            long[] colcntVals = new long[colCnt];
            double[] colsumVals = new double[colCnt];
            double[] colavrgVals = new double[colCnt];
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fileNm);
            StringBuilder strSB = new StringBuilder("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" " +
              "\"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"[]><html xmlns=\"http://www.w3.org/1999/xhtml\" dir=\"ltr\" lang=\"en-US\" xml:lang=\"en\"><head><meta http-equiv=\"Content-Type\" " +
                "content=\"text/html; charset=utf-8\"><title>" + rptTitle + "</title>" +
              "<link rel=\"stylesheet\" href=\"../amcharts/rpt.css\" type=\"text/css\"></head><body>");
            Image img = this.getDBImageFile(this.Org_id.ToString() + ".png", 0);
            img.Save(this.getRptDrctry() + @"\amcharts_2100\images\" + this.Org_id.ToString() + ".png",
              System.Drawing.Imaging.ImageFormat.Png);
            img.Dispose();
            img = null;

            //Org Name
            string orgNm = this.getOrgName(this.Org_id);
            string pstl = this.getOrgPstlAddrs(this.Org_id);
            //Contacts Nos
            string cntcts = this.getOrgContactNos(this.Org_id);
            //Email Address
            string email = this.getOrgEmailAddrs(this.Org_id);

            strSB.AppendLine("<p><img src=\"../images/" + this.Org_id.ToString() + ".png\">" +
              orgNm + "<br/>" + pstl + "<br/>" + cntcts + "<br/>" + email + "<br/>" + "</p>");

            strSB.AppendLine("<table><caption align=\"top\">" + rptTitle + "</caption><thead>");

            int wdth = 0;
            string finalStr = " ";
            for (int d = 0; d < colCnt; d++)
            {
                string algn = "left";
                int colLen = dtst.Tables[0].Columns[d].ColumnName.Length;
                wdth = (int)Math.Round(((double)colLen / (double)totlLen) * 100, 0);
                if (colLen >= 3)
                {
                    if (this.mustColBeFrmtd(d.ToString(), colsToFrmt) == true)
                    {
                        algn = "right";
                        finalStr = dtst.Tables[0].Columns[d].ColumnName.Trim().PadLeft(colLen, ' ');
                    }
                    else
                    {
                        finalStr = dtst.Tables[0].Columns[d].ColumnName.Trim() + " ";
                    }
                    strSB.AppendLine("<th align=\"" + algn + "\" width=\"" + wdth +
                      "%\">" + finalStr.Replace(" ", "&nbsp;") + "</th>");
                }
            }
            strSB.AppendLine("</thead><tbody>");

            for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
            {
                strSB.AppendLine("<tr>");
                for (int d = 0; d < colCnt; d++)
                {
                    string algn = "left";
                    double nwval = 0;
                    bool mstgrp = this.mustColBeGrpd(d.ToString(), colsToGrp);
                    if (this.mustColBeCntd(d.ToString(), colsToCnt) == true)
                    {
                        if ((a > 0) && (dtst.Tables[0].Rows[a - 1][d].ToString()
                        == dtst.Tables[0].Rows[a][d].ToString())
                        && (mstgrp == true))
                        {
                        }
                        else
                        {
                            colcntVals[d] += 1;
                        }
                    }
                    else if (this.mustColBeSumd(d.ToString(), colsToSum) == true)
                    {
                        double.TryParse(dtst.Tables[0].Rows[a][d].ToString(), out nwval);
                        if ((a > 0) && (dtst.Tables[0].Rows[a - 1][d].ToString()
                  == dtst.Tables[0].Rows[a][d].ToString())
                  && (mstgrp == true))
                        {
                        }
                        else
                        {
                            colsumVals[d] += nwval;
                        }
                    }
                    else if (this.mustColBeAvrgd(d.ToString(), colsToAvrg) == true)
                    {
                        double.TryParse(dtst.Tables[0].Rows[a][d].ToString(), out nwval);
                        if ((a > 0) && (dtst.Tables[0].Rows[a - 1][d].ToString()
                  == dtst.Tables[0].Rows[a][d].ToString())
                  && (mstgrp == true))
                        {
                        }
                        else
                        {
                            colcntVals[d] += 1;
                            colsumVals[d] += nwval;
                        }
                    }

                    int colLen = dtst.Tables[0].Columns[d].ColumnName.Length;
                    if (colLen >= 3)
                    {
                        if ((a > 0) && (dtst.Tables[0].Rows[a - 1][d].ToString()
                          == dtst.Tables[0].Rows[a][d].ToString())
                          && (this.mustColBeGrpd(d.ToString(), colsToGrp) == true))
                        {
                            wdth = (int)Math.Round(((double)colLen / (double)totlLen) * 100, 0);
                            strSB.AppendLine("<td align=\"" + algn + "\"  width=\"" + wdth + "%\">" + " ".Replace(" ", "&nbsp;") + "</td>");//.Replace(" ", "&nbsp;")
                        }
                        else
                        {
                            wdth = (int)Math.Round(((double)colLen / (double)totlLen) * 100, 0);
                            string frsh = " ";
                            if (this.mustColBeFrmtd(d.ToString(), colsToFrmt) == true)
                            {
                                algn = "right";
                                double num = 0;
                                double.TryParse(dtst.Tables[0].Rows[a][d].ToString().Trim(), out num);
                                if (dtst.Tables[0].Rows[a][d].ToString() != "")
                                {
                                    frsh = num.ToString("#,##0.00");//.Trim().PadRight(60, ' ')
                                }
                                else
                                {
                                    frsh = dtst.Tables[0].Rows[a][d].ToString() + " ";
                                }
                            }
                            else
                            {
                                frsh = dtst.Tables[0].Rows[a][d].ToString() + " ";
                            }
                            strSB.AppendLine("<td align=\"" + algn + "\" width=\"" + wdth + "%\">" + this.breakTxtDownHTML(frsh,
                              dtst.Tables[0].Columns[d].ColumnName.Length).Replace(" ", "&nbsp;") + "</td>");//.Replace(" ", "&nbsp;")
                        }
                    }
                }
                strSB.AppendLine("</tr>");
            }
            //Populate Counts/Sums/Averages
            strSB.AppendLine("<tr>");

            for (int f = 0; f < colCnt; f++)
            {
                string algn = "left";
                int colLen = dtst.Tables[0].Columns[f].ColumnName.Length;
                finalStr = " ";
                if (colLen >= 3)
                {
                    if (this.mustColBeCntd(f.ToString(), colsToCnt) == true)
                    {
                        if (this.mustColBeFrmtd(f.ToString(), colsToFrmt) == true)
                        {
                            algn = "right";
                            finalStr = ("Count = " + colcntVals[f].ToString("#,##0"));
                        }
                        else
                        {
                            finalStr = ("Count = " + colcntVals[f].ToString());
                        }
                    }
                    else if (this.mustColBeSumd(f.ToString(), colsToSum) == true)
                    {
                        if (this.mustColBeFrmtd(f.ToString(), colsToFrmt) == true)
                        {
                            algn = "right";
                            finalStr = ("Sum = " + colsumVals[f].ToString("#,##0.00"));
                        }
                        else
                        {
                            finalStr = ("Sum = " + colsumVals[f].ToString());
                        }
                    }
                    else if (this.mustColBeAvrgd(f.ToString(), colsToAvrg) == true)
                    {
                        if (this.mustColBeFrmtd(f.ToString(), colsToFrmt) == true)
                        {
                            algn = "right";
                            finalStr = ("Average = " + (colsumVals[f] / colcntVals[f]).ToString("#,##0.00"));
                        }
                        else
                        {
                            finalStr = ("Average = " + (colsumVals[f] / colcntVals[f]).ToString());
                        }
                    }
                    else
                    {
                        finalStr = " ";
                    }
                    strSB.AppendLine("<td align=\"" + algn + "\" width=\"" + wdth + "%\">" + this.breakTxtDownHTML(finalStr,
                      dtst.Tables[0].Columns[f].ColumnName.Length).Replace(" ", "&nbsp;") + "</td>");//.Replace(" ", "&nbsp;")
                }
            }
            strSB.AppendLine("</tr>");
            strSB.AppendLine("</tbody></table>");
            strSB.AppendLine("</body></html>");
            sw.WriteLine(strSB);
            sw.Dispose();
            sw.Close();
            System.Windows.Forms.Application.DoEvents();
        }

        public void exprtToHTMLSCC(DataSet dtst, string fileNm,
          string rptTitle, string[] colsToGrp, string[] colsToUse)
        {
            //Simple Column Chart
            int colCnt = dtst.Tables[0].Columns.Count;

            System.IO.StreamWriter sw = new System.IO.StreamWriter(fileNm);
            StringBuilder strSB = new StringBuilder("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" " +
              "\"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"[]><html xmlns=\"http://www.w3.org/1999/xhtml\" dir=\"ltr\" lang=\"en-US\" xml:lang=\"en\"><head><meta http-equiv=\"Content-Type\" " +
                "content=\"text/html; charset=utf-8\"><title>" + rptTitle + "</title>" +
              "<link rel=\"stylesheet\" href=\"../amcharts/rpt.css\" type=\"text/css\">");
            strSB.AppendLine(@"<link rel=""stylesheet"" href=""style.css"" type=""text/css"">
        <script src=""../amcharts/amcharts.js"" type=""text/javascript""></script>         
        <script type=""text/javascript"">
            var chart;

            var chartData = [");

            for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
            {
                if (a < dtst.Tables[0].Rows.Count - 1)
                {
                    strSB.AppendLine(@"{
                ctgry: """ + dtst.Tables[0].Rows[a][int.Parse(colsToUse[0])].ToString() + @""",
                vals: " + dtst.Tables[0].Rows[a][int.Parse(colsToUse[1])].ToString() + @",
                color: ""#0D52D1""
            },");
                }
                else
                {
                    strSB.AppendLine(@"{
                ctgry: """ + dtst.Tables[0].Rows[a][int.Parse(colsToUse[0])].ToString() + @""",
                vals: " + dtst.Tables[0].Rows[a][int.Parse(colsToUse[1])].ToString() + @",
                color: ""#0D52D1""
            }];");
                }
            }

            //      strSB.AppendLine(@"{
            //                country: ""USA"",
            //                visits: 4025
            //            }, {
            //                country: ""China"",
            //                visits: 1882
            //            }];");


            strSB.AppendLine(@"AmCharts.ready(function () {
                // SERIAL CHART
                chart = new AmCharts.AmSerialChart();
                chart.dataProvider = chartData;
                chart.categoryField = ""ctgry"";
                chart.depth3D = 0;
                chart.angle = 0;
                //chart.startDuration = 1;

                // AXES
                // category
                var categoryAxis = chart.categoryAxis;
                categoryAxis.labelRotation = 90;
                categoryAxis.title = """ + dtst.Tables[0].Columns[int.Parse(colsToUse[0])].ColumnName + @""";
                categoryAxis.gridPosition = ""start"";

                // value
                // in case you don't want to change default settings of value axis,
                // you don't need to create it, as one value axis is created automatically.
                var valueAxis = new AmCharts.ValueAxis();
                valueAxis.title = """ + dtst.Tables[0].Columns[int.Parse(colsToUse[1])].ColumnName + @""";
                valueAxis.dashLength = 5;
                chart.addValueAxis(valueAxis);

                // GRAPH
                var graph = new AmCharts.AmGraph();
                graph.valueField = ""vals"";
                graph.colorField = ""color"";
                graph.balloonText = ""[[category]]: [[value]]"";
                graph.type = ""column"";
                graph.lineAlpha = 0;
                graph.fillAlphas = 1;
                chart.addGraph(graph);

                chart.write(""chartdiv"");
            });
        </script>");

            strSB.AppendLine("</head><body>");
            Image img = this.getDBImageFile(this.Org_id.ToString() + ".png", 0);
            img.Save(this.getRptDrctry() + @"\amcharts_2100\images\" + this.Org_id.ToString() + ".png",
              System.Drawing.Imaging.ImageFormat.Png);
            img.Dispose();
            img = null;

            //Org Name
            string orgNm = this.getOrgName(this.Org_id);
            string pstl = this.getOrgPstlAddrs(this.Org_id);
            //Contacts Nos
            string cntcts = this.getOrgContactNos(this.Org_id);
            //Email Address
            string email = this.getOrgEmailAddrs(this.Org_id);

            strSB.AppendLine("<p><img src=\"../images/" + this.Org_id.ToString() + ".png\">" +
              orgNm + "<br/>" + pstl + "<br/>" + cntcts + "<br/>" + email + "<br/>" + "</p>");
            strSB.AppendLine("<h2>" + rptTitle + "</h2>");
            strSB.AppendLine("<div id=\"chartdiv\" style=\"width: " + colsToGrp[0] + "px; height: " + colsToGrp[1] + "px;\"></div></body></html>");
            sw.WriteLine(strSB);
            sw.Dispose();
            sw.Close();
            System.Windows.Forms.Application.DoEvents();
        }

        public void exprtToHTMLPC(DataSet dtst, string fileNm,
        string rptTitle, string[] colsToGrp, string[] colsToUse)
        {
            //Pie Chart
            int colCnt = dtst.Tables[0].Columns.Count;

            System.IO.StreamWriter sw = new System.IO.StreamWriter(fileNm);
            StringBuilder strSB = new StringBuilder("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" " +
              "\"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"[]><html xmlns=\"http://www.w3.org/1999/xhtml\" dir=\"ltr\" lang=\"en-US\" xml:lang=\"en\"><head><meta http-equiv=\"Content-Type\" " +
                "content=\"text/html; charset=utf-8\"><title>" + rptTitle + "</title>" +
              "<link rel=\"stylesheet\" href=\"../amcharts/rpt.css\" type=\"text/css\">");
            strSB.AppendLine(@"<link rel=""stylesheet"" href=""style.css"" type=""text/css"">
        <script src=""../amcharts/amcharts.js"" type=""text/javascript""></script>         
        <script type=""text/javascript"">
            var chart;

            var chartData = [");

            for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
            {
                if (a < dtst.Tables[0].Rows.Count - 1)
                {
                    strSB.AppendLine(@"{
                ctgry: """ + dtst.Tables[0].Rows[a][int.Parse(colsToUse[0])].ToString() + @""",
                vals: " + dtst.Tables[0].Rows[a][int.Parse(colsToUse[1])].ToString() + @"
            },");
                }
                else
                {
                    strSB.AppendLine(@"{
                ctgry: """ + dtst.Tables[0].Rows[a][int.Parse(colsToUse[0])].ToString() + @""",
                vals: " + dtst.Tables[0].Rows[a][int.Parse(colsToUse[1])].ToString() + @"
            }];");
                }
            }

            //      strSB.AppendLine(@"{
            //                country: ""USA"",
            //                visits: 4025
            //            }, {
            //                country: ""China"",
            //                visits: 1882
            //            }];");


            strSB.AppendLine(@"AmCharts.ready(function () {
                // PIE CHART
                chart = new AmCharts.AmPieChart();
                chart.dataProvider = chartData;
                chart.titleField = ""ctgry"";
                chart.valueField = ""vals"";
                chart.outlineColor = ""#FFFFFF"";
                chart.outlineAlpha = 0.8;
                chart.outlineThickness = 2;
                // this makes the chart 3D
                chart.depth3D = 15;
                chart.angle = 30;

                chart.write(""chartdiv"");
            });
        </script>");

            strSB.AppendLine("</head><body>");
            Image img = this.getDBImageFile(this.Org_id.ToString() + ".png", 0);
            img.Save(this.getRptDrctry() + @"\amcharts_2100\images\" + this.Org_id.ToString() + ".png",
              System.Drawing.Imaging.ImageFormat.Png);
            img.Dispose();
            img = null;

            //Org Name
            string orgNm = this.getOrgName(this.Org_id);
            string pstl = this.getOrgPstlAddrs(this.Org_id);
            //Contacts Nos
            string cntcts = this.getOrgContactNos(this.Org_id);
            //Email Address
            string email = this.getOrgEmailAddrs(this.Org_id);

            strSB.AppendLine("<p><img src=\"../images/" + this.Org_id.ToString() + ".png\">" +
              orgNm + "<br/>" + pstl + "<br/>" + cntcts + "<br/>" + email + "<br/>" + "</p>");
            strSB.AppendLine("<h2>" + rptTitle + "</h2>");
            strSB.AppendLine("<div id=\"chartdiv\" style=\"width: " + colsToGrp[0] +
              "px; height: " + colsToGrp[1] + "px;\"></div></body></html>");
            sw.WriteLine(strSB);
            sw.Dispose();
            sw.Close();
            System.Windows.Forms.Application.DoEvents();
        }

        public void exprtToHTMLLC(DataSet dtst, string fileNm,
      string rptTitle, string[] colsToGrp, string[] colsToUse)
        {
            //Line Chart
            int colCnt = colsToUse.Length;

            System.IO.StreamWriter sw = new System.IO.StreamWriter(fileNm);
            StringBuilder strSB = new StringBuilder("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" " +
              "\"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"[]><html xmlns=\"http://www.w3.org/1999/xhtml\" dir=\"ltr\" lang=\"en-US\" xml:lang=\"en\"><head><meta http-equiv=\"Content-Type\" " +
                "content=\"text/html; charset=utf-8\"><title>" + rptTitle + "</title>" +
              "<link rel=\"stylesheet\" href=\"../amcharts/rpt.css\" type=\"text/css\">");
            strSB.AppendLine(@"<link rel=""stylesheet"" href=""style.css"" type=""text/css"">
        <script src=""../amcharts/amcharts.js"" type=""text/javascript""></script>         
        <script type=""text/javascript"">
            var chart;

            var chartData = [");

            for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
            {
                if (a < dtst.Tables[0].Rows.Count - 1)
                {
                    strSB.AppendLine(@"{
                ctgry: """ + dtst.Tables[0].Rows[a][int.Parse(colsToUse[0])].ToString() + @""",
                value: " + dtst.Tables[0].Rows[a][int.Parse(colsToUse[1])].ToString() + @"
            },");
                }
                else
                {
                    strSB.AppendLine(@"{
                ctgry: """ + dtst.Tables[0].Rows[a][int.Parse(colsToUse[0])].ToString() + @""",
                value: " + dtst.Tables[0].Rows[a][int.Parse(colsToUse[1])].ToString() + @"
            }];");
                }
            }

            //      strSB.AppendLine(@"{
            //                country: ""USA"",
            //                visits: 4025
            //            }, {
            //                country: ""China"",
            //                visits: 1882
            //            }];");


            strSB.AppendLine(@"AmCharts.ready(function () {
                // SERIAL CHART
                chart = new AmCharts.AmSerialChart();
                chart.pathToImages = ""../amcharts/images/"";
                chart.dataProvider = chartData;
                chart.marginLeft = 10;
                chart.categoryField = ""ctgry"";
                chart.zoomOutButton = {
                    backgroundColor: '#000000',
                    backgroundAlpha: 0.15
                };

                // listen for ""dataUpdated"" event (fired when chart is inited) and call zoomChart method when it happens
                chart.addListener(""dataUpdated"", zoomChart);

                // AXES
                // category
                var categoryAxis = chart.categoryAxis;
                //categoryAxis.parseDates = true; // as our data is date-based, we set parseDates to true
                //categoryAxis.minPeriod = ""DD""; // our data is ctgryly, so we set minPeriod to YYYY
                categoryAxis.title = """ + dtst.Tables[0].Columns[int.Parse(colsToUse[0])].ColumnName + @""";
                categoryAxis.gridAlpha = 0.5;
				        categoryAxis.labelRotation = 90;

                // value
                var valueAxis = new AmCharts.ValueAxis();
                valueAxis.axisAlpha = 0.5;
                valueAxis.title = """ + dtst.Tables[0].Columns[int.Parse(colsToUse[1])].ColumnName + @""";
                valueAxis.inside = true;
                chart.addValueAxis(valueAxis);

                // GRAPH                
                graph = new AmCharts.AmGraph();
                graph.type = ""line""; // this line makes the graph smoothed line.
                graph.lineColor = ""#0000FF"";
                graph.negativeLineColor = ""#637bb6""; // this line makes the graph to change color when it drops below 0
                graph.bullet = ""round"";
                graph.bulletSize = 5;
                graph.lineThickness = 1;
                graph.valueField = ""value"";
                chart.addGraph(graph);

                // CURSOR
                var chartCursor = new AmCharts.ChartCursor();
                chartCursor.cursorAlpha = 0;
                chartCursor.cursorPosition = ""mouse"";
                //chartCursor.categoryBalloonDateFormat = ""YYYY"";
                chart.addChartCursor(chartCursor);

                // SCROLLBAR
                var chartScrollbar = new AmCharts.ChartScrollbar();
                chartScrollbar.graph = graph;
                chartScrollbar.backgroundColor = ""#DDDDDD"";
                chartScrollbar.scrollbarHeight = 15;
                chartScrollbar.selectedBackgroundColor = ""#FFFFFF"";
                chart.addChartScrollbar(chartScrollbar);

                // WRITE
                chart.write(""chartdiv"");
            });

            // this method is called when chart is first inited as we listen for ""dataUpdated"" event
            function zoomChart() {
                // different zoom methods can be used - zoomToIndexes, zoomToDates, zoomToCategoryValues
                //chart.zoomToDates(new Date(1972, 0), new Date(1984, 0));
				chart.zoomToIndexes(0,100);
            }
        </script>");

            strSB.AppendLine("</head><body>");
            Image img = this.getDBImageFile(this.Org_id.ToString() + ".png", 0);
            img.Save(this.getRptDrctry() + @"\amcharts_2100\images\" + this.Org_id.ToString() + ".png",
              System.Drawing.Imaging.ImageFormat.Png);
            img.Dispose();
            img = null;

            //Org Name
            string orgNm = this.getOrgName(this.Org_id);
            string pstl = this.getOrgPstlAddrs(this.Org_id);
            //Contacts Nos
            string cntcts = this.getOrgContactNos(this.Org_id);
            //Email Address
            string email = this.getOrgEmailAddrs(this.Org_id);

            strSB.AppendLine("<p><img src=\"../images/" + this.Org_id.ToString() + ".png\">" +
              orgNm + "<br/>" + pstl + "<br/>" + cntcts + "<br/>" + email + "<br/>" + "</p>");
            strSB.AppendLine("<h2>" + rptTitle + "</h2>");
            strSB.AppendLine("<div id=\"chartdiv\" style=\"width: " + colsToGrp[0] +
              "px; height: " + colsToGrp[1] + "px;\"></div></body></html>");
            sw.WriteLine(strSB);
            sw.Dispose();
            sw.Close();
            System.Windows.Forms.Application.DoEvents();
        }

        private bool mustColBeGrpd(string colNo, string[] colsToGrp)
        {
            for (int i = 0; i < colsToGrp.Length; i++)
            {
                if (colNo == colsToGrp[i])
                {
                    return true;
                }
            }
            return false;
        }

        private bool mustColBeCntd(string colNo, string[] colsToCnt)
        {
            for (int i = 0; i < colsToCnt.Length; i++)
            {
                if (colNo == colsToCnt[i])
                {
                    return true;
                }
            }
            return false;
        }

        private bool mustColBeSumd(string colNo, string[] colsToSum)
        {
            for (int i = 0; i < colsToSum.Length; i++)
            {
                if (colNo == colsToSum[i])
                {
                    return true;
                }
            }
            return false;
        }

        private bool mustColBeAvrgd(string colNo, string[] colsToAvrg)
        {
            for (int i = 0; i < colsToAvrg.Length; i++)
            {
                if (colNo == colsToAvrg[i])
                {
                    return true;
                }
            }
            return false;
        }

        private bool mustColBeFrmtd(string colNo, string[] colsToFrmt)
        {
            for (int i = 0; i < colsToFrmt.Length; i++)
            {
                if (colNo == colsToFrmt[i])
                {
                    return true;
                }
            }
            return false;
        }

        public void exprtBdgtTmp(string startDte, string endDte, string periodTyp)
        {
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.data_source_id = 6;
            nwDiag.orgID = this.Org_id;
            nwDiag.strtDte = startDte;
            nwDiag.endDate = endDte;
            nwDiag.prdTyps = periodTyp;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void exprtBdgtTmp(string startDte, string endDte, string periodTyp, long bdgtid, long rcsNo)
        {
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.data_source_id = 6;
            nwDiag.orgID = this.Org_id;
            nwDiag.recsNo = rcsNo;
            nwDiag.strtDte = startDte;
            nwDiag.endDate = endDte;
            nwDiag.budget_id = bdgtid;
            nwDiag.prdTyps = periodTyp;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void imprtBdgtTmp(long bdgtIDin, string filename)
        {
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.data_source_id = 7;
            nwDiag.orgID = this.Org_id;
            nwDiag.bdgtID = bdgtIDin;
            nwDiag.fileNm = filename;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public double get_LtstExchRate(int fromCurrID, int toCurrID, string asAtDte)
        {
            int fnccurid = this.getOrgFuncCurID(this.Org_id);
            //this.curCode = cmnCde.getPssblValNm(this.curid);

            string strSql = "";
            strSql = @"SELECT CASE WHEN a.currency_from_id=" + fromCurrID +
              @" THEN a.multiply_from_by ELSE (1/a.multiply_from_by) END
      FROM accb.accb_exchange_rates a WHERE ((a.currency_from_id=" + fromCurrID +
              @" and a.currency_to_id=" + toCurrID +
              @") or (a.currency_to_id=" + fromCurrID +
              @" and a.currency_from_id=" + toCurrID +
              @")) and to_timestamp(a.conversion_date,'YYYY-MM-DD') <= to_timestamp('" + asAtDte +
              "','DD-Mon-YYYY HH24:MI:SS') ORDER BY to_timestamp(a.conversion_date,'YYYY-MM-DD') DESC LIMIT 1 OFFSET 0";
            DataSet dtst = this.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return double.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            if (fromCurrID == toCurrID)
            {
                return 1;
            }
            else if (fromCurrID != fnccurid && toCurrID != fnccurid)
            {
                double a = this.get_LtstExchRate(fromCurrID, fnccurid, asAtDte);
                double b = this.get_LtstExchRate(toCurrID, fnccurid, asAtDte);
                if (a != 0 && b != 0)
                {
                    return a / b;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        public void exprtTrnsTmp(long rcsNo, long btchID)
        {
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.data_source_id = 8;
            nwDiag.recsNo = rcsNo;
            nwDiag.orgID = this.Org_id;
            nwDiag.cmnCde = this;
            nwDiag.batchID = btchID;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void imprtTrnsTmp(long batchIDin, string filename)
        {
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.data_source_id = 9;
            nwDiag.orgID = this.Org_id;
            nwDiag.batchID = batchIDin;
            nwDiag.fileNm = filename;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void exprtTrnsTmpltTmp()
        {
            string rspnse = Interaction.InputBox("How many Records will you like to Export?" +
         "\r\n1=No Record(Empty Template)" +
         "\r\n2=All Records" +
         "\r\n3-Infinity=Specify the exact number of Records to Export\r\n",
         "Rhomicom", "1", (this.myComputer.Screen.Bounds.Width / 2) - 170,
         (this.myComputer.Screen.Bounds.Height / 2) - 100);
            if (rspnse.Equals(string.Empty) || rspnse.Equals(null))
            {
                this.showMsg("Operation Cancelled!", 4);
                return;
            }
            long rsponse = 0;
            bool rsps = long.TryParse(rspnse, out rsponse);
            if (rsps == false)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            if (rsponse < 1)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }

            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.data_source_id = 52;
            nwDiag.orgID = this.Org_id;
            nwDiag.recsNo = rsponse;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void imprtTrnsTmpltTmp(string filename)
        {
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.data_source_id = 53;
            nwDiag.orgID = this.Org_id;
            nwDiag.fileNm = filename;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public int getAcctTypID(string accntTyp)
        {
            if (accntTyp == "A")
            {
                return 1;
            }
            else if (accntTyp == "L")
            {
                return 2;
            }
            else if (accntTyp == "EQ")
            {
                return 3;
            }
            else if (accntTyp == "R")
            {
                return 4;
            }
            else if (accntTyp == "EX")
            {
                return 5;
            }
            return -1;
        }

        public void exprtChrtTmp(int chrtTyp)
        {
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.data_source_id = 10;
            nwDiag.orgID = this.Org_id;
            nwDiag.chrtTyp = chrtTyp;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void imprtChrtTmp(string filename)
        {
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.data_source_id = 11;
            nwDiag.orgID = this.Org_id;
            nwDiag.fileNm = filename;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void exprtOrgTmp()
        {
            string rspnse = Interaction.InputBox("How many Records will you like to Export?" +
         "\r\n1=No Record(Empty Template)" +
         "\r\n2=All Records" +
         "\r\n3-Infinity=Specify the exact number of Records to Export\r\n",
         "Rhomicom", "1", (this.myComputer.Screen.Bounds.Width / 2) - 170,
         (this.myComputer.Screen.Bounds.Height / 2) - 100);
            if (rspnse.Equals(string.Empty) || rspnse.Equals(null))
            {
                this.showMsg("Operation Cancelled!", 4);
                return;
            }
            long rsponse = 0;
            bool rsps = long.TryParse(rspnse, out rsponse);
            if (rsps == false)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            if (rsponse < 1)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.recsNo = rsponse;
            nwDiag.data_source_id = 12;
            nwDiag.orgID = this.Org_id;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void imprtOrgTmp(string filename)
        {
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.data_source_id = 13;
            nwDiag.orgID = this.Org_id;
            nwDiag.fileNm = filename;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void exprtDivTmp()
        {
            string rspnse = Interaction.InputBox("How many Records will you like to Export?" +
         "\r\n1=No Record(Empty Template)" +
         "\r\n2=All Records" +
         "\r\n3-Infinity=Specify the exact number of Records to Export\r\n",
         "Rhomicom", "1", (this.myComputer.Screen.Bounds.Width / 2) - 170,
         (this.myComputer.Screen.Bounds.Height / 2) - 100);
            if (rspnse.Equals(string.Empty) || rspnse.Equals(null))
            {
                this.showMsg("Operation Cancelled!", 4);
                return;
            }
            long rsponse = 0;
            bool rsps = long.TryParse(rspnse, out rsponse);
            if (rsps == false)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            if (rsponse < 1)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.data_source_id = 14;
            nwDiag.recsNo = rsponse;
            nwDiag.orgID = this.Org_id;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void imprtDivTmp(string filename)
        {
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.data_source_id = 15;
            nwDiag.orgID = this.Org_id;
            nwDiag.fileNm = filename;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void exprtSiteTmp()
        {
            string rspnse = Interaction.InputBox("How many Records will you like to Export?" +
            "\r\n1=No Record(Empty Template)" +
            "\r\n2=All Records" +
            "\r\n3-Infinity=Specify the exact number of Records to Export\r\n",
            "Rhomicom", "1", (this.myComputer.Screen.Bounds.Width / 2) - 170,
         (this.myComputer.Screen.Bounds.Height / 2) - 100);
            if (rspnse.Equals(string.Empty) || rspnse.Equals(null))
            {
                this.showMsg("Operation Cancelled!", 4);
                return;
            }
            long rsponse = 0;
            bool rsps = long.TryParse(rspnse, out rsponse);
            if (rsps == false)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            if (rsponse < 1)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.recsNo = rsponse;
            nwDiag.data_source_id = 16;
            nwDiag.orgID = this.Org_id;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void imprtSiteTmp(string filename)
        {
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.data_source_id = 17;
            nwDiag.orgID = this.Org_id;
            nwDiag.fileNm = filename;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void exprtJobsTmp()
        {
            string rspnse = Interaction.InputBox("How many Records will you like to Export?" +
         "\r\n1=No Record(Empty Template)" +
         "\r\n2=All Records" +
         "\r\n3-Infinity=Specify the exact number of Records to Export\r\n",
         "Rhomicom", "1", (this.myComputer.Screen.Bounds.Width / 2) - 170,
         (this.myComputer.Screen.Bounds.Height / 2) - 100);
            if (rspnse.Equals(string.Empty) || rspnse.Equals(null))
            {
                this.showMsg("Operation Cancelled!", 4);
                return;
            }
            long rsponse = 0;
            bool rsps = long.TryParse(rspnse, out rsponse);
            if (rsps == false)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            if (rsponse < 1)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.recsNo = rsponse;
            nwDiag.data_source_id = 18;
            nwDiag.orgID = this.Org_id;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void imprtJobsTmp(string filename)
        {
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.data_source_id = 19;
            nwDiag.orgID = this.Org_id;
            nwDiag.fileNm = filename;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void exprtGradesTmp()
        {
            string rspnse = Interaction.InputBox("How many Records will you like to Export?" +
         "\r\n1=No Record(Empty Template)" +
         "\r\n2=All Records" +
         "\r\n3-Infinity=Specify the exact number of Records to Export\r\n",
         "Rhomicom", "1", (this.myComputer.Screen.Bounds.Width / 2) - 170,
         (this.myComputer.Screen.Bounds.Height / 2) - 100);
            if (rspnse.Equals(string.Empty) || rspnse.Equals(null))
            {
                this.showMsg("Operation Cancelled!", 4);
                return;
            }
            long rsponse = 0;
            bool rsps = long.TryParse(rspnse, out rsponse);
            if (rsps == false)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            if (rsponse < 1)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.recsNo = rsponse;

            nwDiag.data_source_id = 20;
            nwDiag.orgID = this.Org_id;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void imprtGradesTmp(string filename)
        {
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.data_source_id = 21;
            nwDiag.orgID = this.Org_id;
            nwDiag.fileNm = filename;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void exprtPosTmp()
        {
            string rspnse = Interaction.InputBox("How many Records will you like to Export?" +
         "\r\n1=No Record(Empty Template)" +
         "\r\n2=All Records" +
         "\r\n3-Infinity=Specify the exact number of Records to Export\r\n",
         "Rhomicom", "1", (this.myComputer.Screen.Bounds.Width / 2) - 170,
         (this.myComputer.Screen.Bounds.Height / 2) - 100);
            if (rspnse.Equals(string.Empty) || rspnse.Equals(null))
            {
                this.showMsg("Operation Cancelled!", 4);
                return;
            }
            long rsponse = 0;
            bool rsps = long.TryParse(rspnse, out rsponse);
            if (rsps == false)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            if (rsponse < 1)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.recsNo = rsponse;

            nwDiag.data_source_id = 22;
            nwDiag.orgID = this.Org_id;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void imprtPosTmp(string filename)
        {
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.data_source_id = 23;
            nwDiag.orgID = this.Org_id;
            nwDiag.fileNm = filename;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void exprtItemsTmp()
        {
            string rspnse = Interaction.InputBox("How many Records will you like to Export?" +
         "\r\n1=No Record(Empty Template)" +
         "\r\n2=All Records" +
         "\r\n3-Infinity=Specify the exact number of Records to Export\r\n",
         "Rhomicom", "1", (this.myComputer.Screen.Bounds.Width / 2) - 170,
         (this.myComputer.Screen.Bounds.Height / 2) - 100);
            if (rspnse.Equals(string.Empty) || rspnse.Equals(null))
            {
                this.showMsg("Operation Cancelled!", 4);
                return;
            }
            long rsponse = 0;
            bool rsps = long.TryParse(rspnse, out rsponse);
            if (rsps == false)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            if (rsponse < 1)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.recsNo = rsponse;
            nwDiag.data_source_id = 24;
            nwDiag.orgID = this.Org_id;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void imprtItemsTmp(string filename)
        {
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.data_source_id = 25;
            nwDiag.orgID = this.Org_id;
            nwDiag.fileNm = filename;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void exprtItemsValTmp()
        {
            string rspnse = Interaction.InputBox("How many Records will you like to Export?" +
         "\r\n1=No Record(Empty Template)" +
         "\r\n2=All Records" +
         "\r\n3-Infinity=Specify the exact number of Records to Export\r\n",
         "Rhomicom", "1", (this.myComputer.Screen.Bounds.Width / 2) - 170,
         (this.myComputer.Screen.Bounds.Height / 2) - 100);
            if (rspnse.Equals(string.Empty) || rspnse.Equals(null))
            {
                this.showMsg("Operation Cancelled!", 4);
                return;
            }
            long rsponse = 0;
            bool rsps = long.TryParse(rspnse, out rsponse);
            if (rsps == false)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            if (rsponse < 1)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.recsNo = rsponse;
            nwDiag.data_source_id = 26;
            nwDiag.orgID = this.Org_id;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void imprtItemsValTmp(string filename)
        {
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.data_source_id = 27;
            nwDiag.orgID = this.Org_id;
            nwDiag.fileNm = filename;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void exprtWkHrTmp()
        {
            string rspnse = Interaction.InputBox("How many Records will you like to Export?" +
         "\r\n1=No Record(Empty Template)" +
         "\r\n2=All Records" +
         "\r\n3-Infinity=Specify the exact number of Records to Export\r\n",
         "Rhomicom", "1", (this.myComputer.Screen.Bounds.Width / 2) - 170,
         (this.myComputer.Screen.Bounds.Height / 2) - 100);
            if (rspnse.Equals(string.Empty) || rspnse.Equals(null))
            {
                this.showMsg("Operation Cancelled!", 4);
                return;
            }
            long rsponse = 0;
            bool rsps = long.TryParse(rspnse, out rsponse);
            if (rsps == false)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            if (rsponse < 1)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.recsNo = rsponse;
            nwDiag.data_source_id = 28;
            nwDiag.orgID = this.Org_id;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void imprtWkHrTmp(string filename)
        {
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.data_source_id = 29;
            nwDiag.orgID = this.Org_id;
            nwDiag.fileNm = filename;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void exprtGathTmp()
        {
            string rspnse = Interaction.InputBox("How many Records will you like to Export?" +
         "\r\n1=No Record(Empty Template)" +
         "\r\n2=All Records" +
         "\r\n3-Infinity=Specify the exact number of Records to Export\r\n",
         "Rhomicom", "1", (this.myComputer.Screen.Bounds.Width / 2) - 170,
         (this.myComputer.Screen.Bounds.Height / 2) - 100);
            if (rspnse.Equals(string.Empty) || rspnse.Equals(null))
            {
                this.showMsg("Operation Cancelled!", 4);
                return;
            }
            long rsponse = 0;
            bool rsps = long.TryParse(rspnse, out rsponse);
            if (rsps == false)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            if (rsponse < 1)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.recsNo = rsponse;
            nwDiag.data_source_id = 30;
            nwDiag.orgID = this.Org_id;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void imprtGathTmp(string filename)
        {
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.data_source_id = 31;
            nwDiag.orgID = this.Org_id;
            nwDiag.fileNm = filename;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void exprtPsnInfoTmp()
        {
            string rspnse = Interaction.InputBox("How many Records will you like to Export?" +
         "\r\n1=No Record(Empty Template)" +
         "\r\n2=All Records" +
         "\r\n3-Infinity=Specify the exact number of Records to Export\r\n",
         "Rhomicom", "1", (this.myComputer.Screen.Bounds.Width / 2) - 170,
         (this.myComputer.Screen.Bounds.Height / 2) - 100);
            if (rspnse.Equals(string.Empty) || rspnse.Equals(null))
            {
                this.showMsg("Operation Cancelled!", 4);
                return;
            }
            long rsponse = 0;
            bool rsps = long.TryParse(rspnse, out rsponse);
            if (rsps == false)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            if (rsponse < 1)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.recsNo = rsponse;
            nwDiag.data_source_id = 32;
            nwDiag.orgID = this.Org_id;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void imprtPsnInfoTmp(string filename)
        {
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.data_source_id = 33;
            nwDiag.orgID = this.Org_id;
            nwDiag.fileNm = filename;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void exprtPsnNtlIDsTmp()
        {
            string rspnse = Interaction.InputBox("How many Records will you like to Export?" +
         "\r\n1=No Record(Empty Template)" +
         "\r\n2=All Records" +
         "\r\n3-Infinity=Specify the exact number of Records to Export\r\n",
         "Rhomicom", "1", (this.myComputer.Screen.Bounds.Width / 2) - 170,
         (this.myComputer.Screen.Bounds.Height / 2) - 100);
            if (rspnse.Equals(string.Empty) || rspnse.Equals(null))
            {
                this.showMsg("Operation Cancelled!", 4);
                return;
            }
            long rsponse = 0;
            bool rsps = long.TryParse(rspnse, out rsponse);
            if (rsps == false)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            if (rsponse < 1)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.recsNo = rsponse;
            nwDiag.data_source_id = 34;
            nwDiag.orgID = this.Org_id;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void imprtPsnNtlIDsTmp(string filename)
        {
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.data_source_id = 35;
            nwDiag.orgID = this.Org_id;
            nwDiag.fileNm = filename;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void exprtPsnRltvsTmp()
        {
            string rspnse = Interaction.InputBox("How many Records will you like to Export?" +
         "\r\n1=No Record(Empty Template)" +
         "\r\n2=All Records" +
         "\r\n3-Infinity=Specify the exact number of Records to Export\r\n",
         "Rhomicom", "1", (this.myComputer.Screen.Bounds.Width / 2) - 170,
         (this.myComputer.Screen.Bounds.Height / 2) - 100);
            if (rspnse.Equals(string.Empty) || rspnse.Equals(null))
            {
                this.showMsg("Operation Cancelled!", 4);
                return;
            }
            long rsponse = 0;
            bool rsps = long.TryParse(rspnse, out rsponse);
            if (rsps == false)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            if (rsponse < 1)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.recsNo = rsponse;
            nwDiag.data_source_id = 36;
            nwDiag.orgID = this.Org_id;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void imprtPsnRltvsTmp(string filename)
        {
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.data_source_id = 37;
            nwDiag.orgID = this.Org_id;
            nwDiag.fileNm = filename;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void exprtPsnDivAsgmtsTmp()
        {
            string rspnse = Interaction.InputBox("How many Records will you like to Export?" +
         "\r\n1=No Record(Empty Template)" +
         "\r\n2=All Records" +
         "\r\n3-Infinity=Specify the exact number of Records to Export\r\n",
         "Rhomicom", "1", (this.myComputer.Screen.Bounds.Width / 2) - 170,
         (this.myComputer.Screen.Bounds.Height / 2) - 100);
            if (rspnse.Equals(string.Empty) || rspnse.Equals(null))
            {
                this.showMsg("Operation Cancelled!", 4);
                return;
            }
            long rsponse = 0;
            bool rsps = long.TryParse(rspnse, out rsponse);
            if (rsps == false)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            if (rsponse < 1)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.recsNo = rsponse;
            nwDiag.data_source_id = 38;
            nwDiag.orgID = this.Org_id;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void imprtPsnDivAsgmtsTmp(string filename)
        {
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.data_source_id = 39;
            nwDiag.orgID = this.Org_id;
            nwDiag.fileNm = filename;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void exprtPsnLocAsgmtsTmp()
        {
            string rspnse = Interaction.InputBox("How many Records will you like to Export?" +
         "\r\n1=No Record(Empty Template)" +
         "\r\n2=All Records" +
         "\r\n3-Infinity=Specify the exact number of Records to Export\r\n",
         "Rhomicom", "1", (this.myComputer.Screen.Bounds.Width / 2) - 170,
         (this.myComputer.Screen.Bounds.Height / 2) - 100);
            if (rspnse.Equals(string.Empty) || rspnse.Equals(null))
            {
                this.showMsg("Operation Cancelled!", 4);
                return;
            }
            long rsponse = 0;
            bool rsps = long.TryParse(rspnse, out rsponse);
            if (rsps == false)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            if (rsponse < 1)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.recsNo = rsponse;
            nwDiag.data_source_id = 54;
            nwDiag.orgID = this.Org_id;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void imprtPsnLocAsgmtsTmp(string filename)
        {
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.data_source_id = 55;
            nwDiag.orgID = this.Org_id;
            nwDiag.fileNm = filename;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void exprtPsnSpvsrAsgmtsTmp()
        {
            string rspnse = Interaction.InputBox("How many Records will you like to Export?" +
         "\r\n1=No Record(Empty Template)" +
         "\r\n2=All Records" +
         "\r\n3-Infinity=Specify the exact number of Records to Export\r\n",
         "Rhomicom", "1", (this.myComputer.Screen.Bounds.Width / 2) - 170,
         (this.myComputer.Screen.Bounds.Height / 2) - 100);
            if (rspnse.Equals(string.Empty) || rspnse.Equals(null))
            {
                this.showMsg("Operation Cancelled!", 4);
                return;
            }
            long rsponse = 0;
            bool rsps = long.TryParse(rspnse, out rsponse);
            if (rsps == false)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            if (rsponse < 1)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.recsNo = rsponse;
            nwDiag.data_source_id = 56;
            nwDiag.orgID = this.Org_id;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void imprtPsnSpvsrAsgmtsTmp(string filename)
        {
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.data_source_id = 57;
            nwDiag.orgID = this.Org_id;
            nwDiag.fileNm = filename;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void exprtPsnJobAsgmtsTmp()
        {
            string rspnse = Interaction.InputBox("How many Records will you like to Export?" +
         "\r\n1=No Record(Empty Template)" +
         "\r\n2=All Records" +
         "\r\n3-Infinity=Specify the exact number of Records to Export\r\n",
         "Rhomicom", "1", (this.myComputer.Screen.Bounds.Width / 2) - 170,
         (this.myComputer.Screen.Bounds.Height / 2) - 100);
            if (rspnse.Equals(string.Empty) || rspnse.Equals(null))
            {
                this.showMsg("Operation Cancelled!", 4);
                return;
            }
            long rsponse = 0;
            bool rsps = long.TryParse(rspnse, out rsponse);
            if (rsps == false)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            if (rsponse < 1)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.recsNo = rsponse;
            nwDiag.data_source_id = 58;
            nwDiag.orgID = this.Org_id;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void imprtPsnJobAsgmtsTmp(string filename)
        {
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.data_source_id = 59;
            nwDiag.orgID = this.Org_id;
            nwDiag.fileNm = filename;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void exprtPsnGrdAsgmtsTmp()
        {
            string rspnse = Interaction.InputBox("How many Records will you like to Export?" +
         "\r\n1=No Record(Empty Template)" +
         "\r\n2=All Records" +
         "\r\n3-Infinity=Specify the exact number of Records to Export\r\n",
         "Rhomicom", "1", (this.myComputer.Screen.Bounds.Width / 2) - 170,
         (this.myComputer.Screen.Bounds.Height / 2) - 100);
            if (rspnse.Equals(string.Empty) || rspnse.Equals(null))
            {
                this.showMsg("Operation Cancelled!", 4);
                return;
            }
            long rsponse = 0;
            bool rsps = long.TryParse(rspnse, out rsponse);
            if (rsps == false)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            if (rsponse < 1)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.recsNo = rsponse;
            nwDiag.data_source_id = 60;
            nwDiag.orgID = this.Org_id;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void imprtPsnGrdAsgmtsTmp(string filename)
        {
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.data_source_id = 61;
            nwDiag.orgID = this.Org_id;
            nwDiag.fileNm = filename;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void exprtPsnPosAsgmtsTmp()
        {
            string rspnse = Interaction.InputBox("How many Records will you like to Export?" +
         "\r\n1=No Record(Empty Template)" +
         "\r\n2=All Records" +
         "\r\n3-Infinity=Specify the exact number of Records to Export\r\n",
         "Rhomicom", "1", (this.myComputer.Screen.Bounds.Width / 2) - 170,
         (this.myComputer.Screen.Bounds.Height / 2) - 100);
            if (rspnse.Equals(string.Empty) || rspnse.Equals(null))
            {
                this.showMsg("Operation Cancelled!", 4);
                return;
            }
            long rsponse = 0;
            bool rsps = long.TryParse(rspnse, out rsponse);
            if (rsps == false)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            if (rsponse < 1)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.recsNo = rsponse;
            nwDiag.data_source_id = 62;
            nwDiag.orgID = this.Org_id;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void imprtPsnPosAsgmtsTmp(string filename)
        {
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.data_source_id = 63;
            nwDiag.orgID = this.Org_id;
            nwDiag.fileNm = filename;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void exprtPsnGathAsgmtsTmp()
        {
            string rspnse = Interaction.InputBox("How many Records will you like to Export?" +
         "\r\n1=No Record(Empty Template)" +
         "\r\n2=All Records" +
         "\r\n3-Infinity=Specify the exact number of Records to Export\r\n",
         "Rhomicom", "1", (this.myComputer.Screen.Bounds.Width / 2) - 170,
         (this.myComputer.Screen.Bounds.Height / 2) - 100);
            if (rspnse.Equals(string.Empty) || rspnse.Equals(null))
            {
                this.showMsg("Operation Cancelled!", 4);
                return;
            }
            long rsponse = 0;
            bool rsps = long.TryParse(rspnse, out rsponse);
            if (rsps == false)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            if (rsponse < 1)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.recsNo = rsponse;
            nwDiag.data_source_id = 64;
            nwDiag.orgID = this.Org_id;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void imprtPsnGathAsgmtsTmp(string filename)
        {
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.data_source_id = 65;
            nwDiag.orgID = this.Org_id;
            nwDiag.fileNm = filename;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void exprtPsnWkHrAsgmtsTmp()
        {
            string rspnse = Interaction.InputBox("How many Records will you like to Export?" +
         "\r\n1=No Record(Empty Template)" +
         "\r\n2=All Records" +
         "\r\n3-Infinity=Specify the exact number of Records to Export\r\n",
         "Rhomicom", "1", (this.myComputer.Screen.Bounds.Width / 2) - 170,
         (this.myComputer.Screen.Bounds.Height / 2) - 100);
            if (rspnse.Equals(string.Empty) || rspnse.Equals(null))
            {
                this.showMsg("Operation Cancelled!", 4);
                return;
            }
            long rsponse = 0;
            bool rsps = long.TryParse(rspnse, out rsponse);
            if (rsps == false)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            if (rsponse < 1)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.recsNo = rsponse;
            nwDiag.data_source_id = 66;
            nwDiag.orgID = this.Org_id;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void imprtPsnWkHrAsgmtsTmp(string filename)
        {
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.data_source_id = 67;
            nwDiag.orgID = this.Org_id;
            nwDiag.fileNm = filename;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void exprtPsnBanksTmp()
        {
            string rspnse = Interaction.InputBox("How many Records will you like to Export?" +
         "\r\n1=No Record(Empty Template)" +
         "\r\n2=All Records" +
         "\r\n3-Infinity=Specify the exact number of Records to Export\r\n",
         "Rhomicom", "1", (this.myComputer.Screen.Bounds.Width / 2) - 170,
         (this.myComputer.Screen.Bounds.Height / 2) - 100);
            if (rspnse.Equals(string.Empty) || rspnse.Equals(null))
            {
                this.showMsg("Operation Cancelled!", 4);
                return;
            }
            long rsponse = 0;
            bool rsps = long.TryParse(rspnse, out rsponse);
            if (rsps == false)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            if (rsponse < 1)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.recsNo = rsponse;
            nwDiag.data_source_id = 40;
            nwDiag.orgID = this.Org_id;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void imprtPsnBanksTmp(string filename)
        {
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.data_source_id = 41;
            nwDiag.orgID = this.Org_id;
            nwDiag.fileNm = filename;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void exprtPsnEducTmp()
        {
            string rspnse = Interaction.InputBox("How many Records will you like to Export?" +
         "\r\n1=No Record(Empty Template)" +
         "\r\n2=All Records" +
         "\r\n3-Infinity=Specify the exact number of Records to Export\r\n",
         "Rhomicom", "1", (this.myComputer.Screen.Bounds.Width / 2) - 170,
         (this.myComputer.Screen.Bounds.Height / 2) - 100);
            if (rspnse.Equals(string.Empty) || rspnse.Equals(null))
            {
                this.showMsg("Operation Cancelled!", 4);
                return;
            }
            long rsponse = 0;
            bool rsps = long.TryParse(rspnse, out rsponse);
            if (rsps == false)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            if (rsponse < 1)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.recsNo = rsponse;
            nwDiag.data_source_id = 42;
            nwDiag.orgID = this.Org_id;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void imprtPsnEducTmp(string filename)
        {
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.data_source_id = 43;
            nwDiag.orgID = this.Org_id;
            nwDiag.fileNm = filename;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void exprtPsnJobExpTmp()
        {
            string rspnse = Interaction.InputBox("How many Records will you like to Export?" +
         "\r\n1=No Record(Empty Template)" +
         "\r\n2=All Records" +
         "\r\n3-Infinity=Specify the exact number of Records to Export\r\n",
         "Rhomicom", "1", (this.myComputer.Screen.Bounds.Width / 2) - 170,
         (this.myComputer.Screen.Bounds.Height / 2) - 100);
            if (rspnse.Equals(string.Empty) || rspnse.Equals(null))
            {
                this.showMsg("Operation Cancelled!", 4);
                return;
            }
            long rsponse = 0;
            bool rsps = long.TryParse(rspnse, out rsponse);
            if (rsps == false)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            if (rsponse < 1)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.recsNo = rsponse;
            nwDiag.data_source_id = 44;
            nwDiag.orgID = this.Org_id;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void imprtPsnJobExpTmp(string filename)
        {
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.data_source_id = 45;
            nwDiag.orgID = this.Org_id;
            nwDiag.fileNm = filename;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void exprtPsnSkllNatrTmp()
        {
            string rspnse = Interaction.InputBox("How many Records will you like to Export?" +
         "\r\n1=No Record(Empty Template)" +
         "\r\n2=All Records" +
         "\r\n3-Infinity=Specify the exact number of Records to Export\r\n",
         "Rhomicom", "1", (this.myComputer.Screen.Bounds.Width / 2) - 170,
         (this.myComputer.Screen.Bounds.Height / 2) - 100);
            if (rspnse.Equals(string.Empty) || rspnse.Equals(null))
            {
                this.showMsg("Operation Cancelled!", 4);
                return;
            }
            long rsponse = 0;
            bool rsps = long.TryParse(rspnse, out rsponse);
            if (rsps == false)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            if (rsponse < 1)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.recsNo = rsponse;
            nwDiag.data_source_id = 46;
            nwDiag.orgID = this.Org_id;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void imprtPsnSkllNatrTmp(string filename)
        {
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.data_source_id = 47;
            nwDiag.orgID = this.Org_id;
            nwDiag.fileNm = filename;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void exprtPymntsTmp(int prsStID, int itmStID)
        {
            string rspnse = Interaction.InputBox("How many Records will you like to Export?" +
         "\r\n1=No Record(Empty Template)" +
         "\r\n2=All Records" +
         "\r\n3-Infinity=Specify the exact number of Records to Export\r\n",
         "Rhomicom", "1", (this.myComputer.Screen.Bounds.Width / 2) - 170,
         (this.myComputer.Screen.Bounds.Height / 2) - 100);
            if (rspnse.Equals(string.Empty) || rspnse.Equals(null))
            {
                this.showMsg("Operation Cancelled!", 4);
                return;
            }
            long rsponse = 0;
            bool rsps = long.TryParse(rspnse, out rsponse);
            if (rsps == false)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            if (rsponse < 1)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.recsNo = rsponse;
            nwDiag.data_source_id = 70;
            nwDiag.prsnStID = prsStID;
            nwDiag.itmStID = itmStID;
            nwDiag.orgID = this.Org_id;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void imprtPymntsTmp(string filename)
        {
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.data_source_id = 71;
            nwDiag.orgID = this.Org_id;
            nwDiag.fileNm = filename;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }
        public void exprtPssblValsTmp(int valstID)
        {
            string rspnse = Interaction.InputBox("How many Records will you like to Export?" +
         "\r\n1=No Record(Empty Template)" +
         "\r\n2=All Records" +
         "\r\n3-Infinity=Specify the exact number of Records to Export\r\n",
         "Rhomicom", "1", (this.myComputer.Screen.Bounds.Width / 2) - 170,
         (this.myComputer.Screen.Bounds.Height / 2) - 100);
            if (rspnse.Equals(string.Empty) || rspnse.Equals(null))
            {
                this.showMsg("Operation Cancelled!", 4);
                return;
            }
            long rsponse = 0;
            bool rsps = long.TryParse(rspnse, out rsponse);
            if (rsps == false)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            if (rsponse < 1)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.recsNo = rsponse;
            nwDiag.data_source_id = 48;
            nwDiag.orgID = this.Org_id;
            nwDiag.in_val_lst_ID = valstID;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void imprtPssblValsTmp(string filename)
        {
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.data_source_id = 49;
            nwDiag.orgID = this.Org_id;
            nwDiag.fileNm = filename;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void exprtUsersTmp()
        {
            string rspnse = Interaction.InputBox("How many Records will you like to Export?" +
         "\r\n1=No Record(Empty Template)" +
         "\r\n2=All Records" +
         "\r\n3-Infinity=Specify the exact number of Records to Export\r\n",
         "Rhomicom", "1", (this.myComputer.Screen.Bounds.Width / 2) - 170,
         (this.myComputer.Screen.Bounds.Height / 2) - 100);
            if (rspnse.Equals(string.Empty) || rspnse.Equals(null))
            {
                this.showMsg("Operation Cancelled!", 4);
                return;
            }
            long rsponse = 0;
            bool rsps = long.TryParse(rspnse, out rsponse);
            if (rsps == false)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            if (rsponse < 1)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.recsNo = rsponse;
            nwDiag.data_source_id = 50;
            nwDiag.orgID = this.Org_id;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void imprtUsersTmp(string filename)
        {
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.data_source_id = 51;
            nwDiag.orgID = this.Org_id;
            nwDiag.fileNm = filename;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void exprtPsnExtInfoTmp()
        {
            string rspnse = Interaction.InputBox("How many Records will you like to Export?" +
         "\r\n1=No Record(Empty Template)" +
         "\r\n2=All Records" +
         "\r\n3-Infinity=Specify the exact number of Records to Export\r\n",
         "Rhomicom", "1", (this.myComputer.Screen.Bounds.Width / 2) - 170,
         (this.myComputer.Screen.Bounds.Height / 2) - 100);
            if (rspnse.Equals(string.Empty) || rspnse.Equals(null))
            {
                this.showMsg("Operation Cancelled!", 4);
                return;
            }
            long rsponse = 0;
            bool rsps = long.TryParse(rspnse, out rsponse);
            if (rsps == false)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            if (rsponse < 1)
            {
                this.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.recsNo = rsponse;
            nwDiag.data_source_id = 68;
            nwDiag.orgID = this.Org_id;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void imprtPsnExtInfoTmp(string filename)
        {
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.data_source_id = 69;
            nwDiag.orgID = this.Org_id;
            nwDiag.fileNm = filename;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void exprtToExcel(ListView lstvw)
        {
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.lstvw = lstvw;
            nwDiag.data_source_id = 2;
            nwDiag.orgID = this.Org_id;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void exprtToExcel(DataGridView dgrvw)
        {
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.dgrdVw = dgrvw;
            nwDiag.data_source_id = 3;
            nwDiag.orgID = this.Org_id;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void exprtToExcelSelective(ListView lstvw, string rptTitle)
        {
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.lstvw = lstvw;
            nwDiag.isSelective = true;
            nwDiag.rptTitle = rptTitle;
            nwDiag.data_source_id = 2;
            nwDiag.orgID = this.Org_id;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void exprtToExcelSelective(DataGridView dgrvw, string rptTitle)
        {
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.dgrdVw = dgrvw;
            nwDiag.isSelective = true;
            nwDiag.rptTitle = rptTitle;
            nwDiag.data_source_id = 3;
            nwDiag.orgID = this.Org_id;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void exprtToWordPrsn(long prsnID)
        {
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.prsn_id = prsnID;
            nwDiag.data_source_id = 4;
            nwDiag.orgID = this.Org_id;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public void exprtToWordOrg(int org_id1)
        {
            exprtToExcelDiag nwDiag = new exprtToExcelDiag();
            nwDiag.data_source_id = 5;
            nwDiag.orgID = org_id1;
            nwDiag.cmnCde = this;
            System.Windows.Forms.Application.DoEvents();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        public int getRandomInt(int a, int b)
        {
            Random rnd = new Random((int)(DateTime.Now.Ticks % 1000000000) + (int)(this.Login_number % 1000000000));

            return rnd.Next(a, b); // creates a number between a and b
        }

        public string getRandomPswd()
        {
            Random rnd = new Random((int)(DateTime.Now.Ticks % 1000000000) + (int)(this.Login_number % 1000000000));

            string[] charset1 = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L",
                                                                                                    "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X",
                                                                                                    "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
                                                                                                    "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l",
                                                                                                    "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x",
                                                                                                    "y", "z"};
            string[] charset2 = { "e", "q", "0", "P", "3", "i", "D", "O", "V", "8", "E", "6",
                                                                                                    "B", "Z", "A", "W", "5", "g", "G", "F", "H", "u", "t", "s",
                                                                                                    "C", "K", "d", "p", "r", "w", "z", "x", "a", "c", "1", "m",
                                                                                                    "I", "f", "Q", "L", "v", "Y", "j", "S", "R", "o", "J", "4",
                                                                                                    "9", "h", "7", "M", "b", "X", "k", "N", "l", "n", "2", "y",
                                                                                                    "T", "U"};
            string[] wldChars = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            string pswd = "";
            //Random rnd = new Random();
            int idx = -1;
            for (int i = 1; i < 8; i++)
            {
                if (i == 1 || i == 4 || i == 7)
                {
                    idx = rnd.Next(0, charset1.Length);
                    pswd += charset1[idx];
                }
                else if (i == 2 || i == 5)
                {
                    idx = rnd.Next(0, charset2.Length);
                    pswd += charset2[idx];
                }
                else if (i == 3 || i == 6)
                {
                    idx = rnd.Next(0, wldChars.Length);
                    pswd += wldChars[idx];
                }
            }
            return pswd;
        }

        public string dbtOrCrdtAccnt(int accntid, string incrsDcrse)
        {
            string accntType = this.getAccntType(accntid);
            string isContra = this.isAccntContra(accntid);
            if (isContra == "0")
            {
                if ((accntType == "A" || accntType == "EX") && incrsDcrse == "I")
                {
                    return "Debit";
                }
                else if ((accntType == "A" || accntType == "EX") && incrsDcrse == "D")
                {
                    return "Credit";
                }
                else if ((accntType == "EQ" || accntType == "R" || accntType == "L") && incrsDcrse == "I")
                {
                    return "Credit";
                }
                else if ((accntType == "EQ" || accntType == "R" || accntType == "L") && incrsDcrse == "D")
                {
                    return "Debit";
                }
            }
            else
            {
                if ((accntType == "A" || accntType == "EX") && incrsDcrse == "I")
                {
                    return "Credit";
                }
                else if ((accntType == "A" || accntType == "EX") && incrsDcrse == "D")
                {
                    return "Debit";
                }
                else if ((accntType == "EQ" || accntType == "R" || accntType == "L") && incrsDcrse == "I")
                {
                    return "Debit";
                }
                else if ((accntType == "EQ" || accntType == "R" || accntType == "L") && incrsDcrse == "D")
                {
                    return "Credit";
                }
            }
            return "";
        }

        public int dbtOrCrdtAccntMultiplier(int accntid, string incrsDcrse)
        {
            string accntType = this.getAccntType(accntid);
            string isContra = this.isAccntContra(accntid);
            if (isContra == "0")
            {
                if ((accntType == "A" || accntType == "EX") && incrsDcrse == "I")
                {
                    return 1;
                }
                else if ((accntType == "A" || accntType == "EX") && incrsDcrse == "D")
                {
                    return -1;
                }
                else if ((accntType == "EQ" || accntType == "R" || accntType == "L") && incrsDcrse == "I")
                {
                    return 1;
                }
                else if ((accntType == "EQ" || accntType == "R" || accntType == "L") && incrsDcrse == "D")
                {
                    return -1;
                }
            }
            else
            {
                if ((accntType == "A" || accntType == "EX") && incrsDcrse == "I")
                {
                    return -1;
                }
                else if ((accntType == "A" || accntType == "EX") && incrsDcrse == "D")
                {
                    return 1;
                }
                else if ((accntType == "EQ" || accntType == "R" ||
                    accntType == "L") && incrsDcrse == "I")
                {
                    return -1;
                }
                else if ((accntType == "EQ" || accntType == "R" ||
                    accntType == "L") && incrsDcrse == "D")
                {
                    return 1;
                }
            }
            return 1;
        }

        public int drCrAccMltplr(int accntid, string drCrdt)
        {
            string accntType = this.getAccntType(accntid);
            string isContra = this.isAccntContra(accntid);
            if (isContra == "0")
            {
                if ((accntType == "A" || accntType == "EX") && drCrdt == "Dr")
                {
                    return 1;
                }
                else if ((accntType == "A" || accntType == "EX") && drCrdt == "Cr")
                {
                    return -1;
                }
                else if ((accntType == "EQ" || accntType == "R" || accntType == "L") && drCrdt == "Cr")
                {
                    return 1;
                }
                else if ((accntType == "EQ" || accntType == "R" || accntType == "L") && drCrdt == "Dr")
                {
                    return -1;
                }
            }
            else
            {
                if ((accntType == "A" || accntType == "EX") && drCrdt == "Cr")
                {
                    return -1;
                }
                else if ((accntType == "A" || accntType == "EX") && drCrdt == "Dr")
                {
                    return 1;
                }
                else if ((accntType == "EQ" || accntType == "R" || accntType == "L") && drCrdt == "Dr")
                {
                    return -1;
                }
                else if ((accntType == "EQ" || accntType == "R" || accntType == "L") && drCrdt == "Cr")
                {
                    return 1;
                }
            }
            return 1;
        }

        public DialogResult showDscntDiag(ref int dscntID, double unitPrice, CommonCodes cmnCde)
        {
            adhocDscntDiag nwDiag = new adhocDscntDiag();
            //nwDiag.cmnCde = cmnCde;
            String myName = "Accounting";
            string myDesc = "This module helps you to manage your organization's Accounting!";
            string audit_tbl_name = "accb.accb_audit_trail_tbl";
            String smplRoleName = "Accounting Administrator";
            nwDiag.cmnCde.DefaultPrvldgs = cmnCde.DefaultPrvldgs;
            nwDiag.itmIDTextBox.Text = dscntID.ToString();
            nwDiag.flatNumericUpDown.Maximum = (decimal)unitPrice;
            //nwDiag.cmnCde.pgSqlConn = cmnCde.pgSqlConn;
            nwDiag.cmnCde.Login_number = cmnCde.Login_number;
            nwDiag.cmnCde.Role_Set_IDs = cmnCde.Role_Set_IDs;
            nwDiag.cmnCde.User_id = cmnCde.User_id;
            nwDiag.cmnCde.Org_id = cmnCde.Org_id;

            nwDiag.cmnCde.ModuleAdtTbl = audit_tbl_name;
            nwDiag.cmnCde.ModuleDesc = myDesc;
            nwDiag.cmnCde.ModuleName = myName;
            nwDiag.cmnCde.SampleRole = smplRoleName;
            nwDiag.cmnCde.Extra_Adt_Trl_Info = "";

            DialogResult dgres = nwDiag.ShowDialog();
            if (dgres == DialogResult.OK)
            {
                dscntID = int.Parse(nwDiag.itmIDTextBox.Text);
            }
            return dgres;
        }
        //documentTitle
        public DialogResult showRptParamsDiag(long rptID, CommonCodes cmnCde)
        {
            fillParamsDiag nwDiag = new fillParamsDiag();
            //nwDiag.cmnCde = cmnCde;

            nwDiag.cmnCde.DefaultPrvldgs = cmnCde.DefaultPrvldgs;
            nwDiag.rpt_ID = rptID;
            //nwDiag.cmnCde.pgSqlConn = cmnCde.pgSqlConn;
            nwDiag.cmnCde.Login_number = cmnCde.Login_number;
            nwDiag.cmnCde.Role_Set_IDs = cmnCde.Role_Set_IDs;
            nwDiag.cmnCde.User_id = cmnCde.User_id;
            nwDiag.cmnCde.Org_id = cmnCde.Org_id;

            nwDiag.cmnCde.ModuleAdtTbl = cmnCde.ModuleAdtTbl;
            nwDiag.cmnCde.ModuleDesc = cmnCde.ModuleDesc;
            nwDiag.cmnCde.ModuleName = cmnCde.ModuleName;
            nwDiag.cmnCde.SampleRole = cmnCde.SampleRole;
            nwDiag.cmnCde.Extra_Adt_Trl_Info = "";
            nwDiag.Show();
            //DialogResult dgres =  Dialog
            //if (dgres == DialogResult.OK)
            //{
            //  //cstspplrID = int.Parse(nwDiag.idTextBox.Text);
            //  //siteID = int.Parse(nwDiag.siteIDTextBox.Text);
            //}
            //  = nwDiag.selectValIDs;
            return DialogResult.OK;
        }

        public DialogResult showRptParamsDiag(long rptID, CommonCodes cmnCde, string paramRepsNVals,
          string docTitle)
        {
            fillParamsDiag nwDiag = new fillParamsDiag();
            //nwDiag.cmnCde = cmnCde;

            nwDiag.cmnCde.DefaultPrvldgs = cmnCde.DefaultPrvldgs;
            nwDiag.rpt_ID = rptID;
            nwDiag.paramRepsNVals = paramRepsNVals;
            nwDiag.documentTitle = docTitle;
            //nwDiag.cmnCde.pgSqlConn = cmnCde.pgSqlConn;
            nwDiag.cmnCde.Login_number = cmnCde.Login_number;
            nwDiag.cmnCde.Role_Set_IDs = cmnCde.Role_Set_IDs;
            nwDiag.cmnCde.User_id = cmnCde.User_id;
            nwDiag.cmnCde.Org_id = cmnCde.Org_id;

            nwDiag.cmnCde.ModuleAdtTbl = cmnCde.ModuleAdtTbl;
            nwDiag.cmnCde.ModuleDesc = cmnCde.ModuleDesc;
            nwDiag.cmnCde.ModuleName = cmnCde.ModuleName;
            nwDiag.cmnCde.SampleRole = cmnCde.SampleRole;
            nwDiag.cmnCde.Extra_Adt_Trl_Info = "";
            nwDiag.Show();
            //DialogResult dgres =  Dialog
            //if (dgres == DialogResult.OK)
            //{
            //  //cstspplrID = int.Parse(nwDiag.idTextBox.Text);
            //  //siteID = int.Parse(nwDiag.siteIDTextBox.Text);
            //}
            //  = nwDiag.selectValIDs;
            return DialogResult.OK;
        }


        public DialogResult showRptParamsDiaglog(long rptID, CommonCodes cmnCde, string paramRepsNVals,
          string docTitle)
        {
            fillParamsDiag nwDiag = new fillParamsDiag();
            //nwDiag.cmnCde = cmnCde;

            nwDiag.cmnCde.DefaultPrvldgs = cmnCde.DefaultPrvldgs;
            nwDiag.rpt_ID = rptID;
            nwDiag.paramRepsNVals = paramRepsNVals;
            nwDiag.documentTitle = docTitle;
            //nwDiag.cmnCde.pgSqlConn = cmnCde.pgSqlConn;
            nwDiag.cmnCde.Login_number = cmnCde.Login_number;
            nwDiag.cmnCde.Role_Set_IDs = cmnCde.Role_Set_IDs;
            nwDiag.cmnCde.User_id = cmnCde.User_id;
            nwDiag.cmnCde.Org_id = cmnCde.Org_id;

            nwDiag.cmnCde.ModuleAdtTbl = cmnCde.ModuleAdtTbl;
            nwDiag.cmnCde.ModuleDesc = cmnCde.ModuleDesc;
            nwDiag.cmnCde.ModuleName = cmnCde.ModuleName;
            nwDiag.cmnCde.SampleRole = cmnCde.SampleRole;
            nwDiag.cmnCde.Extra_Adt_Trl_Info = "";
            return nwDiag.ShowDialog();
            //DialogResult dgres =  Dialog
            //if (dgres == DialogResult.OK)
            //{
            //  //cstspplrID = int.Parse(nwDiag.idTextBox.Text);
            //  //siteID = int.Parse(nwDiag.siteIDTextBox.Text);
            //}
            //  = nwDiag.selectValIDs;
            //return DialogResult.OK;
        }
        public DialogResult showSendMailDiag(long prsnID, CommonCodes cmnCde, string attchFiles)
        {
            XAML.sendMailNewDiag nwDiag = new XAML.sendMailNewDiag();
            nwDiag.cmnCde = cmnCde;

            nwDiag.cmnCde.DefaultPrvldgs = cmnCde.DefaultPrvldgs;
            nwDiag.prsnID = prsnID;
            nwDiag.attcFiles = attchFiles;
            //nwDiag.cmnCde.pgSqlConn = cmnCde.pgSqlConn;
            nwDiag.cmnCde.Login_number = cmnCde.Login_number;
            nwDiag.cmnCde.Role_Set_IDs = cmnCde.Role_Set_IDs;
            nwDiag.cmnCde.User_id = cmnCde.User_id;
            nwDiag.cmnCde.Org_id = cmnCde.Org_id;

            nwDiag.cmnCde.ModuleAdtTbl = cmnCde.ModuleAdtTbl;
            nwDiag.cmnCde.ModuleDesc = cmnCde.ModuleDesc;
            nwDiag.cmnCde.ModuleName = cmnCde.ModuleName;
            nwDiag.cmnCde.SampleRole = cmnCde.SampleRole;
            nwDiag.cmnCde.Extra_Adt_Trl_Info = "";
            //nwDiag.Show();
            nwDiag.Show();
            return DialogResult.OK;
        }

        public DialogResult showSupportDiag(CommonCodes cmnCde)
        {
            XAML.RegisterApp nwDiag = new XAML.RegisterApp();
            nwDiag.cmnCde = cmnCde;

            nwDiag.cmnCde.DefaultPrvldgs = cmnCde.DefaultPrvldgs;
            //nwDiag.cmnCde.pgSqlConn = cmnCde.pgSqlConn;
            nwDiag.cmnCde.Login_number = cmnCde.Login_number;
            nwDiag.cmnCde.Role_Set_IDs = cmnCde.Role_Set_IDs;
            nwDiag.cmnCde.User_id = cmnCde.User_id;
            nwDiag.cmnCde.Org_id = cmnCde.Org_id;

            nwDiag.cmnCde.ModuleAdtTbl = cmnCde.ModuleAdtTbl;
            nwDiag.cmnCde.ModuleDesc = cmnCde.ModuleDesc;
            nwDiag.cmnCde.ModuleName = cmnCde.ModuleName;
            nwDiag.cmnCde.SampleRole = cmnCde.SampleRole;
            nwDiag.cmnCde.Extra_Adt_Trl_Info = "";
            //nwDiag.Show();
            nwDiag.ShowDialog();
            return DialogResult.OK;
        }

        public DialogResult showGetAddresses(ref string selAddresses, ref string selNames)
        {
            getAddressesDiag nwDiag = new getAddressesDiag();
            //nwDiag.cmnCde = cmnCde;
            nwDiag.selNamesTextBox.Text = selNames;
            nwDiag.selNamesTextBox.ReadOnly = true;
            nwDiag.selAddrsTextBox.Text = selAddresses;
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
                selAddresses = nwDiag.selAddrsTextBox.Text;
                selNames = nwDiag.selNamesTextBox.Text;
            }
            return dgRes;
        }

        public DialogResult showIntnlPymntDiag(ref long payTrnsID, long pyItmID, long prsnID,
          long invcHdrID, string trnsDte, CommonCodes cmnCde, double amntToPay)
        {
            intnlPymntsDiag nwDiag = new intnlPymntsDiag();
            //nwDiag.cmnCde = cmnCde;
            String myName = "Internal Payments";
            string myDesc = "This module helps you to manage your organization's HR Payments to Personnel!";
            string audit_tbl_name = "pay.pay_audit_trail_tbl";
            String smplRoleName = "Internal Payments Administrator";
            nwDiag.payItmID = pyItmID;
            nwDiag.prsnID = prsnID;
            nwDiag.invcHdrID = invcHdrID;
            nwDiag.trnsDte = trnsDte;
            nwDiag.amntToPay = amntToPay;
            //nwDiag.cmnCde.DefaultPrvldgs = cmnCde.DefaultPrvldgs;
            //nwDiag.cstspplrID = cstspplrID;
            //nwDiag.siteID = siteID;
            //nwDiag.isReadOnly = isReadOnly;
            //nwDiag.searchForTextBox.Text = srchFor;
            //nwDiag.searchInComboBox.SelectedItem = srchIn;
            //nwDiag.autoLoad = autoLoadIfFnd;
            //nwDiag.cmnCde.SubGrpNames = subGrpNames;
            //nwDiag.cmnCde.MainTableNames = mainTableNames;
            //nwDiag.cmnCde.KeyColumnNames = keyColumnNames;
            //nwDiag.cmnCde.pgSqlConn = cmnCde.pgSqlConn;
            nwDiag.cmnCde.Login_number = cmnCde.Login_number;
            nwDiag.cmnCde.Role_Set_IDs = cmnCde.Role_Set_IDs;
            nwDiag.cmnCde.User_id = cmnCde.User_id;
            nwDiag.cmnCde.Org_id = cmnCde.Org_id;

            nwDiag.cmnCde.ModuleAdtTbl = audit_tbl_name;
            nwDiag.cmnCde.ModuleDesc = myDesc;
            nwDiag.cmnCde.ModuleName = myName;
            nwDiag.cmnCde.SampleRole = smplRoleName;
            nwDiag.cmnCde.Extra_Adt_Trl_Info = "";
            //nwDiag.brghtValLstID = valLstID;
            //nwDiag.con = this.pgSqlConn;
            //nwDiag.selectValIDs = selValIDs;
            //nwDiag.selOnlyOne = shdSelOne;
            //nwDiag.mustSelOne = mustSelctSth;
            DialogResult dgres = nwDiag.ShowDialog();
            if (dgres == DialogResult.OK)
            {
                payTrnsID = nwDiag.payTrnsID;
                //siteID = int.Parse(nwDiag.siteIDTextBox.Text);
            }
            //  = nwDiag.selectValIDs;
            return dgres;
        }

        public DialogResult showCstSpplrDiag(ref long cstspplrID, ref long siteID, bool shdSelOne,
          bool mustSelctSth, string srchFor, string srchIn, bool autoLoadIfFnd, bool isReadOnly, CommonCodes cmnCde, string docType)
        {
            cstSpplrDiag nwDiag = new cstSpplrDiag();
            //nwDiag.cmnCde = cmnCde;
            String myName = "Accounting";
            string myDesc = "This module helps you to manage your organization's Accounting!";
            string audit_tbl_name = "accb.accb_audit_trail_tbl";
            String smplRoleName = "Accounting Administrator";
            nwDiag.cmnCde.DefaultPrvldgs = cmnCde.DefaultPrvldgs;
            nwDiag.cstspplrID = cstspplrID;
            nwDiag.siteID = siteID;
            nwDiag.docType = docType;
            nwDiag.isReadOnly = isReadOnly;
            nwDiag.searchForTextBox.Text = srchFor;
            nwDiag.searchInComboBox.SelectedItem = srchIn;
            nwDiag.autoLoad = autoLoadIfFnd;
            //nwDiag.cmnCde.SubGrpNames = subGrpNames;
            //nwDiag.cmnCde.MainTableNames = mainTableNames;
            //nwDiag.cmnCde.KeyColumnNames = keyColumnNames;
            //nwDiag.cmnCde.pgSqlConn = cmnCde.pgSqlConn;
            nwDiag.cmnCde.Login_number = cmnCde.Login_number;
            nwDiag.cmnCde.Role_Set_IDs = cmnCde.Role_Set_IDs;
            nwDiag.cmnCde.User_id = cmnCde.User_id;
            nwDiag.cmnCde.Org_id = cmnCde.Org_id;

            nwDiag.cmnCde.ModuleAdtTbl = audit_tbl_name;
            nwDiag.cmnCde.ModuleDesc = myDesc;
            nwDiag.cmnCde.ModuleName = myName;
            nwDiag.cmnCde.SampleRole = smplRoleName;
            nwDiag.cmnCde.Extra_Adt_Trl_Info = "";
            //nwDiag.brghtValLstID = valLstID;
            //nwDiag.con = this.pgSqlConn;
            //nwDiag.selectValIDs = selValIDs;
            //nwDiag.selOnlyOne = shdSelOne;
            //nwDiag.mustSelOne = mustSelctSth;
            DialogResult dgres = nwDiag.ShowDialog();
            if (dgres == DialogResult.OK)
            {
                cstspplrID = long.Parse(nwDiag.idTextBox.Text);
                siteID = long.Parse(nwDiag.siteIDTextBox.Text);
            }
            //  = nwDiag.selectValIDs;
            return dgres;
        }

        public DialogResult showPymntDiag(bool createPrepay, bool dsablPayments, int X_Loc, int Y_Loc, double amntToPay, int entrdCurID, int PymntMthdID, string docTypes,
         long cstspplrID, long siteID, long srcDocID, string srcDocType, CommonCodes cmnCde)
        {
            addPymntDiag nwdiag = new addPymntDiag();
            //nwDiag.cmnCde = cmnCde;
            nwdiag.dsablPayments = dsablPayments;
            nwdiag.createPrepay = createPrepay;
            nwdiag.amntToPay = amntToPay;
            nwdiag.orgid = cmnCde.Org_id;
            nwdiag.entrdCurrID = entrdCurID;
            nwdiag.pymntMthdID = PymntMthdID;
            nwdiag.docTypes = docTypes;
            nwdiag.srcDocID = srcDocID;
            nwdiag.srcDocType = srcDocType;
            nwdiag.spplrID = cstspplrID;
            nwdiag.spplrSiteID = siteID;

            if (dsablPayments)
            {
                nwdiag.StartPosition = FormStartPosition.CenterParent;
                //nwdiag.WindowState = FormWindowState.Maximized;
                //System.Windows.Forms.Application.DoEvents();
            }
            else
            {
                nwdiag.Location = new Point(X_Loc, Y_Loc);
            }

            String myName = "Accounting";
            string myDesc = "This module helps you to manage your organization's Accounting!";
            string audit_tbl_name = "accb.accb_audit_trail_tbl";
            String smplRoleName = "Accounting Administrator";
            nwdiag.cmnCde.DefaultPrvldgs = nwdiag.dfltPrvldgs;
            //nwDiag.cstspplrID = cstspplrID;
            //nwDiag.siteID = siteID;
            //nwDiag.cmnCde.SubGrpNames = subGrpNames;
            //nwDiag.cmnCde.MainTableNames = mainTableNames;
            //nwDiag.cmnCde.KeyColumnNames = keyColumnNames;
            //nwdiag.cmnCde.pgSqlConn = cmnCde.pgSqlConn;
            nwdiag.cmnCde.Login_number = cmnCde.Login_number;
            nwdiag.cmnCde.Role_Set_IDs = cmnCde.Role_Set_IDs;
            nwdiag.cmnCde.User_id = cmnCde.User_id;
            nwdiag.cmnCde.Org_id = cmnCde.Org_id;

            nwdiag.cmnCde.ModuleAdtTbl = audit_tbl_name;
            nwdiag.cmnCde.ModuleDesc = myDesc;
            nwdiag.cmnCde.ModuleName = myName;
            nwdiag.cmnCde.SampleRole = smplRoleName;
            nwdiag.cmnCde.Extra_Adt_Trl_Info = "";
            //nwDiag.brghtValLstID = valLstID;
            //nwDiag.con = this.pgSqlConn;
            //nwDiag.selectValIDs = selValIDs;
            //nwDiag.selOnlyOne = shdSelOne;
            //nwDiag.mustSelOne = mustSelctSth;
            DialogResult dgres = nwdiag.ShowDialog();
            if (dgres == DialogResult.OK)
            {
                //cstspplrID = int.Parse(nwDiag.idTextBox.Text);
                //siteID = int.Parse(nwDiag.siteIDTextBox.Text);
            }
            //  = nwDiag.selectValIDs;
            return dgres;
        }

        public DialogResult showPymntDiag(bool createPrepay, bool dsablPayments, int X_Loc, int Y_Loc, double amntToPay, double amntGiven, int entrdCurID, int PymntMthdID, string docTypes,
         long cstspplrID, long siteID, long srcDocID, string srcDocType, CommonCodes cmnCde)
        {
            addPymntDiag nwdiag = new addPymntDiag();
            //nwDiag.cmnCde = cmnCde;
            nwdiag.dsablPayments = dsablPayments;
            nwdiag.createPrepay = createPrepay;
            nwdiag.amntToPay = amntToPay;
            nwdiag.orgid = cmnCde.Org_id;
            nwdiag.entrdCurrID = entrdCurID;
            nwdiag.pymntMthdID = PymntMthdID;
            nwdiag.docTypes = docTypes;
            nwdiag.srcDocID = srcDocID;
            nwdiag.srcDocType = srcDocType;
            nwdiag.spplrID = cstspplrID;
            nwdiag.spplrSiteID = siteID;
            nwdiag.amntGiven = amntGiven;
            if (dsablPayments)
            {
                nwdiag.StartPosition = FormStartPosition.CenterParent;
                //nwdiag.WindowState = FormWindowState.Maximized;
                //System.Windows.Forms.Application.DoEvents();
            }
            else
            {
                nwdiag.Location = new Point(X_Loc, Y_Loc);
            }

            String myName = "Accounting";
            string myDesc = "This module helps you to manage your organization's Accounting!";
            string audit_tbl_name = "accb.accb_audit_trail_tbl";
            String smplRoleName = "Accounting Administrator";
            nwdiag.cmnCde.DefaultPrvldgs = nwdiag.dfltPrvldgs;
            //nwDiag.cstspplrID = cstspplrID;
            //nwDiag.siteID = siteID;
            //nwDiag.cmnCde.SubGrpNames = subGrpNames;
            //nwDiag.cmnCde.MainTableNames = mainTableNames;
            //nwDiag.cmnCde.KeyColumnNames = keyColumnNames;
            //nwdiag.cmnCde.pgSqlConn = cmnCde.pgSqlConn;
            nwdiag.cmnCde.Login_number = cmnCde.Login_number;
            nwdiag.cmnCde.Role_Set_IDs = cmnCde.Role_Set_IDs;
            nwdiag.cmnCde.User_id = cmnCde.User_id;
            nwdiag.cmnCde.Org_id = cmnCde.Org_id;

            nwdiag.cmnCde.ModuleAdtTbl = audit_tbl_name;
            nwdiag.cmnCde.ModuleDesc = myDesc;
            nwdiag.cmnCde.ModuleName = myName;
            nwdiag.cmnCde.SampleRole = smplRoleName;
            nwdiag.cmnCde.Extra_Adt_Trl_Info = "";
            //nwDiag.brghtValLstID = valLstID;
            //nwDiag.con = this.pgSqlConn;
            //nwDiag.selectValIDs = selValIDs;
            //nwDiag.selOnlyOne = shdSelOne;
            //nwDiag.mustSelOne = mustSelctSth;
            DialogResult dgres = nwdiag.ShowDialog();
            if (dgres == DialogResult.OK)
            {
                //cstspplrID = int.Parse(nwDiag.idTextBox.Text);
                //siteID = int.Parse(nwDiag.siteIDTextBox.Text);
            }
            //  = nwDiag.selectValIDs;
            return dgres;
        }

        public DialogResult showPymntDiag(bool createPrepay, bool dsablPayments, int X_Loc, int Y_Loc, double amntToPay, int entrdCurID, int PymntMthdID, string docTypes,
        long cstspplrID, long siteID, long srcDocID, string srcDocType, CommonCodes cmnCde, long intnlPyTrnsID)
        {
            addPymntDiag nwdiag = new addPymntDiag();
            //nwDiag.cmnCde = cmnCde;
            nwdiag.dsablPayments = dsablPayments;
            nwdiag.createPrepay = createPrepay;
            nwdiag.amntToPay = amntToPay;
            nwdiag.orgid = cmnCde.Org_id;
            nwdiag.msPyID = intnlPyTrnsID;
            nwdiag.entrdCurrID = entrdCurID;
            nwdiag.pymntMthdID = PymntMthdID;
            nwdiag.docTypes = docTypes;
            nwdiag.srcDocID = srcDocID;
            nwdiag.srcDocType = srcDocType;
            nwdiag.spplrID = cstspplrID;
            nwdiag.spplrSiteID = siteID;

            if (dsablPayments)
            {
                nwdiag.StartPosition = FormStartPosition.CenterParent;
                //nwdiag.WindowState = FormWindowState.Maximized;
                //System.Windows.Forms.Application.DoEvents();
            }
            else
            {
                nwdiag.Location = new Point(X_Loc, Y_Loc);
            }

            String myName = "Accounting";
            string myDesc = "This module helps you to manage your organization's Accounting!";
            string audit_tbl_name = "accb.accb_audit_trail_tbl";
            String smplRoleName = "Accounting Administrator";
            nwdiag.cmnCde.DefaultPrvldgs = nwdiag.dfltPrvldgs;
            //nwDiag.cstspplrID = cstspplrID;
            //nwDiag.siteID = siteID;
            //nwDiag.cmnCde.SubGrpNames = subGrpNames;
            //nwDiag.cmnCde.MainTableNames = mainTableNames;
            //nwDiag.cmnCde.KeyColumnNames = keyColumnNames;
            //nwdiag.cmnCde.pgSqlConn = cmnCde.pgSqlConn;
            nwdiag.cmnCde.Login_number = cmnCde.Login_number;
            nwdiag.cmnCde.Role_Set_IDs = cmnCde.Role_Set_IDs;
            nwdiag.cmnCde.User_id = cmnCde.User_id;
            nwdiag.cmnCde.Org_id = cmnCde.Org_id;

            nwdiag.cmnCde.ModuleAdtTbl = audit_tbl_name;
            nwdiag.cmnCde.ModuleDesc = myDesc;
            nwdiag.cmnCde.ModuleName = myName;
            nwdiag.cmnCde.SampleRole = smplRoleName;
            nwdiag.cmnCde.Extra_Adt_Trl_Info = "";
            //nwDiag.brghtValLstID = valLstID;
            //nwDiag.con = this.pgSqlConn;
            //nwDiag.selectValIDs = selValIDs;
            //nwDiag.selOnlyOne = shdSelOne;
            //nwDiag.mustSelOne = mustSelctSth;
            DialogResult dgres = nwdiag.ShowDialog();
            if (dgres == DialogResult.OK)
            {
                //cstspplrID = int.Parse(nwDiag.idTextBox.Text);
                //siteID = int.Parse(nwDiag.siteIDTextBox.Text);
            }
            //  = nwDiag.selectValIDs;
            return dgres;
        }

        public DialogResult showPssblValDiag(int valLstID, ref int[] selValIDs, bool shdSelOne, bool mustSelctSth)
        {
            vwPssblValueDiag nwDiag = new vwPssblValueDiag();
            nwDiag.brghtValLstID = valLstID;
            //nwDiag.con = CommonCode.GlobalSQLConn;
            nwDiag.selectValIDs = selValIDs;
            nwDiag.selOnlyOne = shdSelOne;
            nwDiag.mustSelOne = mustSelctSth;

            nwDiag.cmnCde.Login_number = this.Login_number;
            nwDiag.cmnCde.Role_Set_IDs = this.Role_Set_IDs;
            nwDiag.cmnCde.User_id = this.User_id;
            nwDiag.cmnCde.Org_id = this.Org_id;

            DialogResult dgres = nwDiag.ShowDialog();
            selValIDs = nwDiag.selectValIDs;
            return dgres;
        }

        public DialogResult showPssblValDiag(int valLstID, ref int[] selValIDs, bool shdSelOne,
          bool mustSelctSth, string srchFor, string srchIn, bool autoLoadIfFnd)
        {
            vwPssblValueDiag nwDiag = new vwPssblValueDiag();
            nwDiag.brghtValLstID = valLstID;
            //nwDiag.con = CommonCode.GlobalSQLConn;
            nwDiag.selectValIDs = selValIDs;
            nwDiag.selOnlyOne = shdSelOne;
            nwDiag.mustSelOne = mustSelctSth;
            nwDiag.searchForTextBox.Text = srchFor;
            nwDiag.searchInComboBox.SelectedItem = srchIn;
            nwDiag.autoLoadIfFnd = autoLoadIfFnd;

            nwDiag.cmnCde.Login_number = this.Login_number;
            nwDiag.cmnCde.Role_Set_IDs = this.Role_Set_IDs;
            nwDiag.cmnCde.User_id = this.User_id;
            nwDiag.cmnCde.Org_id = this.Org_id;

            DialogResult dgres = nwDiag.ShowDialog();
            selValIDs = nwDiag.selectValIDs;
            return dgres;
        }

        public DialogResult showPssblValDiag(int valLstID, ref int[] selValIDs, bool shdSelOne,
           bool mustSelctSth, string srchFor, string srchIn, bool autoLoadIfFnd, string addtnlWhere)
        {
            vwPssblValueDiag nwDiag = new vwPssblValueDiag();
            nwDiag.brghtValLstID = valLstID;
            //nwDiag.con = CommonCode.GlobalSQLConn;
            nwDiag.selectValIDs = selValIDs;
            nwDiag.selOnlyOne = shdSelOne;
            nwDiag.addtnlWhere = addtnlWhere;
            nwDiag.mustSelOne = mustSelctSth;
            nwDiag.searchForTextBox.Text = srchFor;
            nwDiag.searchInComboBox.SelectedItem = srchIn;
            nwDiag.autoLoadIfFnd = autoLoadIfFnd;

            nwDiag.cmnCde.Login_number = this.Login_number;
            nwDiag.cmnCde.Role_Set_IDs = this.Role_Set_IDs;
            nwDiag.cmnCde.User_id = this.User_id;
            nwDiag.cmnCde.Org_id = this.Org_id;

            DialogResult dgres = nwDiag.ShowDialog();
            selValIDs = nwDiag.selectValIDs;
            return dgres;
        }
        public DialogResult showPssblValDiag(int valLstID, ref string[] selVals,
                  bool shdSelOne, bool mustSelctSth, string srchFor, string srchIn, bool autoLoadIfFnd)
        {
            vwPssblValueDiag nwDiag = new vwPssblValueDiag();
            nwDiag.brghtValLstID = valLstID;
            //nwDiag.con = CommonCode.GlobalSQLConn;
            nwDiag.selectValues = selVals;
            nwDiag.selOnlyOne = shdSelOne;
            nwDiag.mustSelOne = mustSelctSth;
            nwDiag.searchForTextBox.Text = srchFor;
            nwDiag.searchInComboBox.SelectedItem = srchIn;
            nwDiag.autoLoadIfFnd = autoLoadIfFnd;

            nwDiag.cmnCde.Login_number = this.Login_number;
            nwDiag.cmnCde.Role_Set_IDs = this.Role_Set_IDs;
            nwDiag.cmnCde.User_id = this.User_id;
            nwDiag.cmnCde.Org_id = this.Org_id;

            DialogResult dgres = nwDiag.ShowDialog();
            selVals = nwDiag.selectValues;
            return dgres;
        }

        public DialogResult showPssblValDiag(int valLstID, ref string[] selVals,
                 bool shdSelOne, bool mustSelctSth, int criteriaID, string srchFor, string srchIn, bool autoLoadIfFnd)
        {
            vwPssblValueDiag nwDiag = new vwPssblValueDiag();
            nwDiag.brghtValLstID = valLstID;
            nwDiag.criteriaID = criteriaID;
            //nwDiag.con = CommonCode.GlobalSQLConn;
            nwDiag.selectValues = selVals;
            nwDiag.selOnlyOne = shdSelOne;
            nwDiag.mustSelOne = mustSelctSth;
            nwDiag.searchForTextBox.Text = srchFor;
            nwDiag.searchInComboBox.SelectedItem = srchIn;
            nwDiag.autoLoadIfFnd = autoLoadIfFnd;

            nwDiag.cmnCde.Login_number = this.Login_number;
            nwDiag.cmnCde.Role_Set_IDs = this.Role_Set_IDs;
            nwDiag.cmnCde.User_id = this.User_id;
            nwDiag.cmnCde.Org_id = this.Org_id;

            DialogResult dgres = nwDiag.ShowDialog();
            selVals = nwDiag.selectValues;
            return dgres;
        }

        public DialogResult showPssblValDiag(int valLstID, ref string[] selVals,
          bool shdSelOne, bool mustSelctSth, int criteriaID1, string criteriaID2,
          string criteriaID3, string srchFor, string srchIn, bool autoLoadIfFnd)
        {
            vwPssblValueDiag nwDiag = new vwPssblValueDiag();
            nwDiag.brghtValLstID = valLstID;
            nwDiag.criteriaID = criteriaID1;
            nwDiag.criteriaID2 = criteriaID2;
            nwDiag.criteriaID3 = criteriaID3;
            //nwDiag.con = CommonCode.GlobalSQLConn;
            nwDiag.selectValues = selVals;
            nwDiag.selOnlyOne = shdSelOne;
            nwDiag.mustSelOne = mustSelctSth;
            nwDiag.searchForTextBox.Text = srchFor;
            nwDiag.searchInComboBox.SelectedItem = srchIn;
            nwDiag.autoLoadIfFnd = autoLoadIfFnd;

            nwDiag.cmnCde.Login_number = this.Login_number;
            nwDiag.cmnCde.Role_Set_IDs = this.Role_Set_IDs;
            nwDiag.cmnCde.User_id = this.User_id;
            nwDiag.cmnCde.Org_id = this.Org_id;

            DialogResult dgres = nwDiag.ShowDialog();
            selVals = nwDiag.selectValues;
            return dgres;
        }

        public DialogResult showPssblValDiag(int valLstID, ref string[] selVals,
           bool shdSelOne, bool mustSelctSth, int criteriaID1, string criteriaID2,
           string criteriaID3, string srchFor, string srchIn, bool autoLoadIfFnd, string addtnlWhere)
        {
            vwPssblValueDiag nwDiag = new vwPssblValueDiag();
            nwDiag.brghtValLstID = valLstID;
            nwDiag.criteriaID = criteriaID1;
            nwDiag.criteriaID2 = criteriaID2;
            nwDiag.criteriaID3 = criteriaID3;
            nwDiag.addtnlWhere = addtnlWhere;
            //nwDiag.con = CommonCode.GlobalSQLConn;
            nwDiag.selectValues = selVals;
            nwDiag.selOnlyOne = shdSelOne;
            nwDiag.mustSelOne = mustSelctSth;
            nwDiag.searchForTextBox.Text = srchFor;
            nwDiag.searchInComboBox.SelectedItem = srchIn;
            nwDiag.autoLoadIfFnd = autoLoadIfFnd;

            nwDiag.cmnCde.Login_number = this.Login_number;
            nwDiag.cmnCde.Role_Set_IDs = this.Role_Set_IDs;
            nwDiag.cmnCde.User_id = this.User_id;
            nwDiag.cmnCde.Org_id = this.Org_id;

            DialogResult dgres = nwDiag.ShowDialog();
            selVals = nwDiag.selectValues;
            return dgres;
        }

        public DialogResult showPssblValDiag(int valLstID, ref string[] selVals,
                  bool shdSelOne, bool mustSelctSth)
        {
            vwPssblValueDiag nwDiag = new vwPssblValueDiag();
            nwDiag.brghtValLstID = valLstID;
            //nwDiag.con = CommonCode.GlobalSQLConn;
            nwDiag.selectValues = selVals;
            nwDiag.selOnlyOne = shdSelOne;
            nwDiag.mustSelOne = mustSelctSth;

            nwDiag.cmnCde.Login_number = this.Login_number;
            nwDiag.cmnCde.Role_Set_IDs = this.Role_Set_IDs;
            nwDiag.cmnCde.User_id = this.User_id;
            nwDiag.cmnCde.Org_id = this.Org_id;

            DialogResult dgres = nwDiag.ShowDialog();
            selVals = nwDiag.selectValues;
            return dgres;
        }

        public DialogResult showPssblValDiag(int valLstID, ref string[] selVals,
                 bool shdSelOne, bool mustSelctSth, int criteriaID)
        {
            vwPssblValueDiag nwDiag = new vwPssblValueDiag();
            nwDiag.brghtValLstID = valLstID;
            nwDiag.criteriaID = criteriaID;
            //nwDiag.con = CommonCode.GlobalSQLConn;
            nwDiag.selectValues = selVals;
            nwDiag.selOnlyOne = shdSelOne;
            nwDiag.mustSelOne = mustSelctSth;

            nwDiag.cmnCde.Login_number = this.Login_number;
            nwDiag.cmnCde.Role_Set_IDs = this.Role_Set_IDs;
            nwDiag.cmnCde.User_id = this.User_id;
            nwDiag.cmnCde.Org_id = this.Org_id;

            DialogResult dgres = nwDiag.ShowDialog();
            selVals = nwDiag.selectValues;
            return dgres;
        }

        public DialogResult showPssblValDiag(int valLstID, ref string[] selVals,
          bool shdSelOne, bool mustSelctSth, int criteriaID1, string criteriaID2, string criteriaID3)
        {
            vwPssblValueDiag nwDiag = new vwPssblValueDiag();
            nwDiag.brghtValLstID = valLstID;
            nwDiag.criteriaID = criteriaID1;
            nwDiag.criteriaID2 = criteriaID2;
            nwDiag.criteriaID3 = criteriaID3;
            //nwDiag.con = CommonCode.GlobalSQLConn;
            nwDiag.selectValues = selVals;
            nwDiag.selOnlyOne = shdSelOne;
            nwDiag.mustSelOne = mustSelctSth;

            nwDiag.cmnCde.Login_number = this.Login_number;
            nwDiag.cmnCde.Role_Set_IDs = this.Role_Set_IDs;
            nwDiag.cmnCde.User_id = this.User_id;
            nwDiag.cmnCde.Org_id = this.Org_id;

            DialogResult dgres = nwDiag.ShowDialog();
            selVals = nwDiag.selectValues;
            return dgres;
        }

        public DialogResult showRowsExtInfDiag(long tblID, long row_pk_val,
              string ext_info_tbl_nm, string bannerText, bool canEdt,
          int vwSQLPmsnID, int rcHstryPrmsID, string ext_info_seq_nm)
        {
            dsplyARowsExtInfDiag nwDiag = new dsplyARowsExtInfDiag();
            nwDiag.canEdit = canEdt;
            nwDiag.table_id = tblID;
            nwDiag.vwSQLpmsn_id = vwSQLPmsnID;
            nwDiag.rcHstryPmsn_id = rcHstryPrmsID;
            nwDiag.row_pk_id = row_pk_val;
            nwDiag.ext_inf_tbl_name = ext_info_tbl_nm;
            nwDiag.Text = nwDiag.Text + " (" + bannerText + ")";
            nwDiag.cmnCde = this;
            nwDiag.ext_inf_seq_name = ext_info_seq_nm;
            //nwDiag.con = CommonCode.GlobalSQLConn;
            DialogResult dgres = nwDiag.ShowDialog();
            return dgres;
        }

        public DialogResult showRowsExtInfDiag(long tblID, long row_pk_val,
                  string ext_info_tbl_nm, string bannerText, bool canEdt,
                  string ext_info_seq_nm)
        {
            dsplyARowsExtInfDiag nwDiag = new dsplyARowsExtInfDiag();
            nwDiag.canEdit = canEdt;
            nwDiag.table_id = tblID;
            nwDiag.row_pk_id = row_pk_val;
            nwDiag.ext_inf_tbl_name = ext_info_tbl_nm;
            nwDiag.ext_inf_seq_name = ext_info_seq_nm;
            nwDiag.Text = nwDiag.Text + " (" + bannerText + ")";
            nwDiag.cmnCde = this;
            //nwDiag.con = CommonCode.GlobalSQLConn;
            DialogResult dgres = nwDiag.ShowDialog();
            return dgres;
        }

        public DialogResult showRowsExtInfDiag(long tblID, long row_pk_val,
                 string ext_info_tbl_nm, string bannerText, string ext_info_seq_nm)
        {
            dsplyARowsExtInfDiag nwDiag = new dsplyARowsExtInfDiag();
            nwDiag.canEdit = false;
            nwDiag.table_id = tblID;
            nwDiag.row_pk_id = row_pk_val;
            nwDiag.ext_inf_tbl_name = ext_info_tbl_nm;
            nwDiag.ext_inf_seq_name = ext_info_seq_nm;
            nwDiag.Text = nwDiag.Text + " (" + bannerText + ")";
            nwDiag.cmnCde = this;
            //nwDiag.con = CommonCode.GlobalSQLConn;
            DialogResult dgres = nwDiag.ShowDialog();
            return dgres;
        }
        #endregion
    }
}
