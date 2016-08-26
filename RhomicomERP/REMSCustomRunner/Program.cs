using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading;
using Npgsql;
using System.Data;
using System.Drawing;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
//using Microsoft.VisualBasic;
using System.Text.RegularExpressions;
using System.Net;
using Newtonsoft.Json;

namespace REMSCustomRunner
{
  class Program
  {
    static Thread threadOne = null;   //Updates Process Runner Status
    static Thread threadFive = null;  //Thread for running the actual Code behind the Request Run if this is the

    static string runnerName = "";

    static Program()
    {
      //
      // Static constructor for the program class.
      // ... Also called a type initializer.
      // ... It throws an exception in runtime.
      //
    }

    static void Main(string[] args)
    {
      //1-Highest 2-AboveNormal 3-Normal 4-BelowNormal 5-Lowest
      //Every 10 seconds update is_runner_active status_time to now so
      //that it can be used to check whether there is already an active runner running
      //DateTime crTm = DateTime.Now;
      try
      {
        Global.pid = System.Diagnostics.Process.GetCurrentProcess().Id;

        if (args.Length >= 8)
        {
          Global.rnnrsBasDir = args[7].Trim('"');
          runnerName = args[5].Trim('"');
          Global.errorLog = args[0] + "\r\n" + args[1] + "\r\n" + args[2] + "\r\n" +
            "********************" + "\r\n" + args[4] + "\r\n" + args[5] +
            "\r\n" + args[6] + "\r\n" + Global.rnnrsBasDir + "\r\n";
          if (args.Length == 10)
          {
            Global.callngAppType = args[8].Trim('"');
            Global.dataBasDir = args[9].Trim('"');
            Global.errorLog += args[8] + "\r\n" + args[9] + "\r\n";
          }
          Console.WriteLine(Global.errorLog);
          Global.writeToLog();
          Global.runID = long.Parse(args[6]);
          do_connection(args[0], args[1], args[2], args[3], args[4]);
          Global.appStatPath = Global.rnnrsBasDir;

          if (Global.runID > 0)
          {
            Global.rnUser_ID = long.Parse(Global.getGnrlRecNm("rpt.rpt_report_runs", "rpt_run_id", "run_by", Global.runID));
            Global.UsrsOrg_ID = Global.getUsrOrgID(Global.rnUser_ID);
          }

          if (Global.globalSQLConn.State == ConnectionState.Open)
          {
            Global.globalSQLConn.Close();
            bool isLstnrRnng = false;
            if (Program.runnerName == "REQUESTS LISTENER PROGRAM")
            {
              int isIPAllwd = Global.getEnbldPssblValID(Global.getMachDetails()[2],
        Global.getEnbldLovID("Allowed IP Address for Request Listener"));

              if (args[0] != Global.getMachDetails()[2] && args[0].ToLower() != "localhost"
                && isIPAllwd <= 0)
              {
                Program.killThreads();
                Thread.CurrentThread.Abort();
                //Program.killThreads();
                return;
              }

              for (int i = 0; i < 1; i++)
              {
                isLstnrRnng = Global.isRunnrRnng(Program.runnerName);
                //Thread.Sleep(500);
              }
              if (isLstnrRnng == true)
              {
                Program.killThreads();
                Thread.CurrentThread.Abort();

                //Program.killThreads();
                return;
              }
            }
            Global.errorLog = "Successfully Connected to Database\r\n" + isLstnrRnng.ToString() + "\r\n";
            Global.writeToLog();
            string rnnPryty = Global.getGnrlRecNm("rpt.rpt_prcss_rnnrs", "rnnr_name", "crnt_rnng_priority", runnerName);

            if (1 == 1)
            {
              //Thread for running the actual Code behind the Request Run if this is the
              //Program supposed to run that request
              //i.e. if Global.runID >0
              Global.minimizeMemory();
              if (Global.runID > 0)
              {
                ThreadStart startDelegate1 = new ThreadStart(rqstLstnrUpdtrfunc);
                threadOne = new Thread(startDelegate1);//Updates Process Runner Status
                threadOne.Name = "ThreadOne";
                threadOne.Priority = ThreadPriority.Lowest;

                threadOne.Start();
                ThreadStart startDelegate5 = new ThreadStart(runActualRqtsfunc);
                threadFive = new Thread(startDelegate5);
                threadFive.Name = "ThreadFive";
                if (rnnPryty == "1-Highest")
                {
                  threadFive.Priority = ThreadPriority.Highest;
                }
                else if (rnnPryty == "2-AboveNormal")
                {
                  threadFive.Priority = ThreadPriority.AboveNormal;
                }
                else if (rnnPryty == "3-Normal")
                {
                  threadFive.Priority = ThreadPriority.Normal;
                }
                else if (rnnPryty == "4-BelowNormal")
                {
                  threadFive.Priority = ThreadPriority.BelowNormal;
                }
                else
                {
                  threadFive.Priority = ThreadPriority.Lowest;
                }
                threadFive.Start();
              }
              // Allow counting for 10 seconds.
              //Thread.Sleep(1000);
            }
          }
        }
      }
      catch (Exception ex)
      {
        Global.errorLog = ex.Message + "\r\n" + ex.StackTrace + "\r\n" + ex.InnerException + "\r\n";
        StreamWriter fileWriter;
        string fileLoc = Global.rnnrsBasDir + @"\log_files\";
        //string fileLoc =Global.rnnrsBasDir;
        fileLoc += "Global.errorLog" + DateTime.Now.ToString("ddMMMyyyyHHmmss") + ".rho";

        fileWriter = new StreamWriter(fileLoc, true);
        //fileWriter. = txt.(fileLoc);
        fileWriter.WriteLine(Global.errorLog);
        fileWriter.Close();
        fileWriter = null;
        killThreads();
      }
      finally
      {

      }
    }

    static void do_connection(string hostnm, string prtnum, string uname, string pwd, string dbase)
    {
      try
      {
        if (pwd.Contains("(E)"))
        {
          pwd = Global.decrypt(pwd.Replace("(E)", ""), Global.AppKey);
        }
        Global.connStr = String.Format("Server={0};Port={1};" +
        "User Id={2};Password={3};Database={4};Pooling=true;MinPoolSize=0;MaxPoolSize=100;Timeout={5};CommandTimeout={6};",
        hostnm, prtnum, uname,
        pwd, dbase, "60", "1200");

        Global.globalSQLConn.ConnectionString = Global.connStr;
        Global.globalSQLConn.Open();
        Global.Hostnme = hostnm;
        Global.Portnum = prtnum;
        Global.Uname = uname;
        Global.Pswd = pwd;
        Global.Dbase = dbase;

        int lvid = Global.getLovID("Security Keys");
        string apKey = Global.getEnbldPssblValDesc(
          "AppKey", lvid);

        if (apKey != "" && lvid > 0)
        {
          Global.AppKey = apKey;
        }
        else
        {
          Global.AppKey = "ROMeRRTRREMhbnsdGeneral KeyZzfor Rhomi|com Systems "
    + "Tech. !Ltd Enterpise/Organization @763542ERPorbjkSOFTWARE"
    + "asdbhi68103weuikTESTfjnsdfRSTLU../";
        }
      }
      catch (Exception ex)
      {
        Global.errorLog = ex.Message + "\r\n\r\n" + ex.StackTrace + "\r\n\r\n" + ex.InnerException + "\r\n\r\n";
        Global.writeToLog();
        killThreads();
      }
      finally
      {
      }
    }

    static void checkNClosePrgrm()
    {
      string shdRnnrStop = Global.getGnrlRecNm("rpt.rpt_prcss_rnnrs", "rnnr_name", "shld_rnnr_stop", runnerName);

      string shdRnIDStop = "0";
      if (Global.runID > 0)
      {
        shdRnIDStop = Global.getGnrlRecNm("rpt.rpt_report_runs",
          "trim(to_char(rpt_run_id,'999999999999999999999'))",
          "shld_run_stop", Global.runID.ToString());
      }
      if (shdRnnrStop == "1" || shdRnIDStop == "1")
      {
        Global.updateRptRn(Global.runID, "Cancelled!", 100);
        killThreads();
      }

    }

    static void updatePrgrm(long prgmID)
    {
      Global.minimizeMemory();

      string shdRnnrStop = Global.getGnrlRecNm("rpt.rpt_prcss_rnnrs", "rnnr_name", "shld_rnnr_stop", runnerName);

      string shdRnIDStop = "0";
      int rnnrStatusPcnt = 0;
      if (Global.runID > 0)
      {
        shdRnIDStop = Global.getGnrlRecNm("rpt.rpt_report_runs",
          "trim(to_char(rpt_run_id,'999999999999999999999'))",
          "shld_run_stop", Global.runID.ToString());
        rnnrStatusPcnt = int.Parse(Global.getGnrlRecNm("rpt.rpt_report_runs",
  "trim(to_char(rpt_run_id,'999999999999999999999'))",
  "run_status_prct", Global.runID.ToString()));

      }
      if (shdRnnrStop == "1" || shdRnIDStop == "1" || Global.mustStop == true)
      {
        Global.updateRptRn(Global.runID, "Cancelled!", 100);
        killThreads();
        return;
      }
      if (rnnrStatusPcnt >= 100)
      {
        killThreads();
        return;
      }
      if (prgmID > 0)
      {
        string dtestr = Global.getDB_Date_time();
        string[] macDet = Global.getMachDetails();
        //string hndle = System.Diagnostics.Process.GetCurrentProcess().Handle.ToString();
        //"Handle: " + hndle + 
        Thread.Sleep(2000);
        Global.updatePrcsRnnr(prgmID, dtestr, "PID: " + Global.pid + " Running on: " + macDet[0] + " / " + macDet[1] + " / " + macDet[2]);
        if (Global.runID > 0)
        {
          Global.updateRptRnActvTme(Global.runID, dtestr);
        }
      }
    }

    static void gnrtAlertMailerfunc(long rptID, long runBy, int alertID, long msgSentID,
     string prmIDs, string prmVals, string outputUsd, string orntnUsd)
    {
      try
      {
        //do
        //{
        //1. Get all enabled schedules
        //2. for each enabled schedule check last time it was run
        // if difference between last_time_active is >= schedule interval 
        //and time component is >= current time then generate another schedule run
        //Program.checkNClosePrgrm();
        //DataSet dtst = Global.get_AlertSchdules(rptID);
        //for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
        //{
        //  long prgmID = Global.getGnrlRecID("rpt.rpt_prcss_rnnrs", "rnnr_name", "prcss_rnnr_id", Program.runnerName);
        //  Program.updatePrgrm(prgmID);

        //  long rpt_id = long.Parse(dtst.Tables[0].Rows[i][1].ToString());
        //long schdlID = long.Parse(dtst.Tables[0].Rows[i][0].ToString());

        //if (Global.doesLstRnTmExcdIntvl(rpt_id,
        //  dtst.Tables[0].Rows[i][4].ToString() + " " + dtst.Tables[0].Rows[i][5].ToString(), -1) == true)
        //{
        string dateStr = Global.getDB_Date_time();
        TimeSpan tm = new TimeSpan(0, 0, 59);
        dateStr = (DateTime.ParseExact(
  dateStr, "yyyy-MM-dd HH:mm:ss",
  System.Globalization.CultureInfo.InvariantCulture) - tm).ToString("yyyy-MM-dd HH:mm:ss");

        //string outputUsd = Global.getGnrlRecNm("rpt.rpt_reports", "report_id", "output_type", rpt_id);
        //string orntnUsd = Global.getGnrlRecNm("rpt.rpt_reports", "report_id", "portrait_lndscp", rpt_id);


        Global.createSchdldRptRn(
          runBy, dateStr,
          rptID, prmIDs, prmVals, outputUsd, orntnUsd, alertID, msgSentID);

        //Thread.Sleep(5000);

        long rptRunID = Global.getRptRnID(rptID, runBy, dateStr);

        long msg_id = Global.getLogMsgID("rpt.rpt_run_msgs",
          "Process Run", rptRunID);
        if (msg_id <= 0)
        {
          Global.createLogMsg(dateStr +
          " .... Alert Run is about to Start...(Being run by " +
          Global.get_user_name(runBy) + ")",
          "rpt.rpt_run_msgs", "Process Run", rptRunID, dateStr);
        }
        //msg_id = Global.getLogMsgID("rpt.rpt_run_msgs", "Process Run", rptRunID);
        //}
        //}
        //long mxConns = 0;
        //long curCons = 0;
        //do
        //{
        //  mxConns = Global.getMxAllwdDBConns();
        //  curCons = Global.getCurDBConns();
        //  Global.errorLog = "Inside Generation of Scheduled Requests=> Current Connections: " + curCons + " Max Connections: " + mxConns;
        //  Global.writeToLog();

        //  Thread.Sleep(30000);
        //  long prgmID = Global.getGnrlRecID("rpt.rpt_prcss_rnnrs", "rnnr_name", "prcss_rnnr_id", runnerName);
        //  Program.updatePrgrm(prgmID);
        //}
        //while (curCons >= mxConns);
        //}
        //while (true);
      }
      catch (System.Threading.ThreadAbortException thex)
      {
        killThreads();
      }
      catch (Exception ex)
      {
        //write to log file
        Global.errorLog = ex.Message + "\r\n" + ex.StackTrace + "\r\n" + ex.InnerException + "\r\n";
        Global.writeToLog();
        if (threadOne.IsAlive)
        {
          threadOne.Abort();
        }
      }
      finally
      {
      }
    }

    static void runActualRqtsfunc()
    {
      string dateStr = Global.getDB_Date_time();
      string log_tbl = "rpt.rpt_run_msgs";
      try
      {
        long prgmID = Global.getGnrlRecID("rpt.rpt_prcss_rnnrs", "rnnr_name", "prcss_rnnr_id", runnerName);
        Global.errorLog = "Successfully Started Thread Five\r\nProgram ID:" + prgmID + ": Program Name: " + runnerName + "\r\n";
        Global.writeToLog();

        string rptTitle = "";
        string yrStr = "";
        string fromDate = "";
        string toDate = "";
        string[] colsToGrp = { "" };
        string[] colsToCnt = { "" };
        string[] colsToSum = { "" };
        string[] colsToAvrg = { "" };
        string[] colsToFrmt = { "" };
        string toMails = "";
        string ccMails = "";
        string bccMails = "";
        string sbjct = "";
        string msgBdy = "";
        string attchMns = "";
        long nwMsgSntID = -1;
        long toPrsnID = -1;
        long msPayID = -1;
        string msPayStrtDte = "";
        string msPayEndDte = "";
        long toCstmrSpplrID = -1;
        string errMsg = "";

        if (Global.runID > 0)
        {
          DataSet runDtSt = Global.get_RptRun_Det(Global.runID);
          long locRptID = long.Parse(runDtSt.Tables[0].Rows[0][5].ToString());
          DataSet rptDtSt = Global.get_RptDet(locRptID);
          int alertID = int.Parse(runDtSt.Tables[0].Rows[0][13].ToString());
          //string runAlertRpt = Global.getGnrlRecNm("alrt.alrt_alerts", "alert_id", "alert_id", Global.runID);
          long msgSentID = long.Parse(runDtSt.Tables[0].Rows[0][14].ToString());

          DataSet alrtDtSt = Global.get_AlertDet(alertID);

          string alertType = "";
          if (alertID > 0)
          {
            alertType = alrtDtSt.Tables[0].Rows[0][5].ToString();
          }

          DataSet prgmUntsDtSt = Global.get_AllPrgmUnts(locRptID);
          long prgUntsCnt = prgmUntsDtSt.Tables[0].Rows.Count;

          Global.rnUser_ID = long.Parse(runDtSt.Tables[0].Rows[0][0].ToString());
          Global.errorLog = "Run ID: " + Global.runID + " Report ID:" + locRptID + "\r\n";
          Global.writeToLog();
          long msg_id = Global.getGnrlRecID("rpt.rpt_run_msgs", "process_typ", "process_id", "msg_id", "Process Run", Global.runID);

          Global.updateLogMsg(msg_id,
"\r\n\r\n\r\nLog Messages ==>\r\n\r\n" + Global.errorLog,
log_tbl, dateStr, Global.rnUser_ID);

          Global.updateRptRn(Global.runID, "Preparing to Start...", 20);

          Global.logMsgID = msg_id;
          Global.logTbl = log_tbl;
          Global.gnrlDateStr = dateStr;

          long rpt_run_id = Global.runID;
          long rpt_id = locRptID;

          string paramIDs = runDtSt.Tables[0].Rows[0][6].ToString();
          string paramVals = runDtSt.Tables[0].Rows[0][7].ToString();
          char[] w = { '|' };
          char[] seps = { ',' };
          char[] seps1 = { ';', ',' };
          string[] arry1 = paramIDs.Split(w);
          string[] arry2 = paramVals.Split(w);
          string outputUsd = runDtSt.Tables[0].Rows[0][8].ToString();
          string orntnUsd = runDtSt.Tables[0].Rows[0][9].ToString();
          string imgCols = rptDtSt.Tables[0].Rows[0][15].ToString();
          string rptLyout = rptDtSt.Tables[0].Rows[0][14].ToString();
          string rptOutpt = "";
          string rptdlmtr = rptDtSt.Tables[0].Rows[0][16].ToString();
          //string rptType = Global.getGnrlRecNm("rpt.rpt_reports", "report_id", "rpt_or_sys_prcs", rpt_id);
          string rptType = rptDtSt.Tables[0].Rows[0][5].ToString();
          string rptName = rptDtSt.Tables[0].Rows[0][0].ToString();

          Global.ovrllDataCnt = 0;
          Global.strSB = new StringBuilder("");
          //Program.updatePrgrm(prgmID);
          for (int q = 0; q < prgUntsCnt + 1; q++)
          {
            bool isfirst = true;
            bool islast = true;
            bool shdAppnd = false;
            string rqrdParamVal = "";
            if (q == prgUntsCnt)
            {
              islast = true;
            }
            else
            {
              islast = false;
            }
            if (prgUntsCnt > 0)
            {
              shdAppnd = true;
            }
            else
            {
              shdAppnd = false;
            }
            if (q == 0)
            {
              isfirst = true;
              //rpt_id = rpt_id;
            }
            else
            {
              isfirst = false;
              rpt_id = long.Parse(prgmUntsDtSt.Tables[0].Rows[q - 1][0].ToString());
              rptDtSt = Global.get_RptDet(rpt_id);
              outputUsd = rptDtSt.Tables[0].Rows[0][12].ToString();
              orntnUsd = rptDtSt.Tables[0].Rows[0][13].ToString();
              //rptdlmtr = Global.getGnrlRecNm("rpt.rpt_reports", "report_id", "csv_delimiter", rpt_id);
              rptLyout = rptDtSt.Tables[0].Rows[0][14].ToString();
              rptType = rptDtSt.Tables[0].Rows[0][5].ToString();
              colsToGrp = rptDtSt.Tables[0].Rows[0][7].ToString().Split(seps);
              colsToCnt = rptDtSt.Tables[0].Rows[0][8].ToString().Split(seps);
              colsToSum = rptDtSt.Tables[0].Rows[0][9].ToString().Split(seps);
              colsToAvrg = rptDtSt.Tables[0].Rows[0][10].ToString().Split(seps);
              colsToFrmt = rptDtSt.Tables[0].Rows[0][11].ToString().Split(seps);
            }

            String rpt_SQL = "";
            if (alertID > 0 && msgSentID <= 0)
            {
              rpt_SQL = Global.get_Alert_SQL(alertID);
            }
            else
            {
              rpt_SQL = Global.get_Rpt_SQL(rpt_id);
            }
            //Program.updatePrgrm(prgmID);
            for (int i = 0; i < arry1.Length; i++)
            {
              long pID = -1;
              long.TryParse(arry1[i], out pID);
              int h1 = Global.findArryIdx(Global.sysParaIDs, arry1[i]);
              if (h1 >= 0)
              {
                if (arry1[i] == "-130" && i < arry2.Length)
                {
                  rptTitle = arry2[i];
                }
                else if (arry1[i] == "-140" && i < arry2.Length)
                {
                  if (q == 0)
                  {
                    colsToGrp = arry2[i].Split(seps);
                  }
                }
                else if (arry1[i] == "-150" && i < arry2.Length)
                {
                  if (q == 0)
                  {
                    colsToCnt = arry2[i].Split(seps);
                  }
                }
                else if (arry1[i] == "-160" && i < arry2.Length)
                {
                  if (q == 0)
                  {
                    colsToSum = arry2[i].Split(seps);
                  }
                }
                else if (arry1[i] == "-170" && i < arry2.Length)
                {
                  if (q == 0)
                  {
                    colsToAvrg = arry2[i].Split(seps);
                  }
                }
                else if (arry1[i] == "-180" && i < arry2.Length)
                {
                  if (q == 0)
                  {
                    colsToFrmt = arry2[i].Split(seps);
                  }
                }
                else if (arry1[i] == "-190" && i < arry2.Length)
                {
                  //colsToGrp = arry2[i].Split(seps);
                }
                else if (arry1[i] == "-200" && i < arry2.Length)
                {
                  //colsToGrp = arry2[i].Split(seps);
                }
              }
              else if (pID > 0 && i < arry2.Length - 1)
              {
                string paramSqlRep = Global.getGnrlRecNm("rpt.rpt_report_parameters",
                  "parameter_id", "paramtr_rprstn_nm_in_query", pID);
                rpt_SQL = rpt_SQL.Replace(paramSqlRep,
        arry2[i]);
                if (paramSqlRep == "{:toPrsnID}")
                {
                  toPrsnID = long.Parse(arry2[i]);
                }
                else if (paramSqlRep == "{:msPayNameNumber}")
                {
                  msPayID = Global.getMsPyID(arry2[i], Global.UsrsOrg_ID);
                }
                else if (paramSqlRep == "{:msPayStartDate}")
                {
                  msPayStrtDte = arry2[i];
                }
                else if (paramSqlRep == "{:msPayEndDate}")
                {
                  msPayEndDte = arry2[i];
                }
                else if (paramSqlRep == "{:toPrsnLocID}")
                {
                  toPrsnID = Global.getPrsnID(arry2[i]);
                }
                else if (paramSqlRep == "{:alert_type}" && rptType.Contains("Alert"))
                {
                  //alertType = arry2[i];
                }
                else if (paramSqlRep == "{:billPrdStr}")
                {
                  yrStr = arry2[i];
                }
                if (paramSqlRep == "{:msg_body}" && rptType == "Alert(SQL Mail List)")
                {
                  rqrdParamVal = arry2[i];
                }
                else if (paramSqlRep == "{:to_mail_list}" && rptType == "Alert(SQL Message)")
                {
                  rqrdParamVal = arry2[i];
                }
                else if (paramSqlRep == "{:intrfc_tbl_name}" && rptType == "Journal Import")
                {
                  rqrdParamVal = arry2[i];
                }
                else if (paramSqlRep == "{:orgID}")
                {
                  if (int.Parse(arry2[i]) > 0)
                  {
                    Global.UsrsOrg_ID = int.Parse(arry2[i]);
                  }
                }
                else if (paramSqlRep == "{:alert_type}")
                {
                  //alertType = arry2[i];
                }
                else if (paramSqlRep == "{:fromDate}")
                {
                  fromDate = arry2[i];
                }
                else if (paramSqlRep == "{:toDate}")
                {
                  toDate = arry2[i];
                }
              }
            }

            rpt_SQL = rpt_SQL.Replace("{:usrID}", Global.rnUser_ID.ToString());
            rpt_SQL = rpt_SQL.Replace("{:msgID}", msg_id.ToString());
            rpt_SQL = rpt_SQL.Replace("{:orgID}", Global.UsrsOrg_ID.ToString());

            if (rptType == "Command Line Script")
            {
              rpt_SQL = rpt_SQL.Replace("{:host_name}", Global.Hostnme);
              rpt_SQL = rpt_SQL.Replace("{:portnum}", Global.Portnum);
            }

            //NB. Be updating all report run statuses and percentages in the table
            Global.updateLogMsg(msg_id,
    "\r\n\r\n\r\nReport/Process SQL being executed is ==>\r\n\r\n" + rpt_SQL,
    log_tbl, dateStr, Global.rnUser_ID);

            //1. Execute SQL to get a dataset
            Global.updateRptRn(rpt_run_id, "Running SQL...", 40);
            //Program.updatePrgrm(prgmID);

            //worker.ReportProgress(40);
            DataSet dtst = null;
            if (rptType == "Database Function")
            {
              Global.executeGnrlSQL(rpt_SQL.Replace("\r\n", "").Replace("\n", "").Replace("\r", ""));
              if (rptName == "Data Integrity Corrections")
              {
                Program.updatePhoneNumbers(prgmID);
              }
            }
            else if (rptType == "Command Line Script")
            {
              rpt_SQL = rpt_SQL.Replace("{:db_password}", Global.Pswd);

              string batchFilnm = Global.appStatPath + "/" + "REM_DBBackup" + rpt_run_id.ToString() + ".bat";
              System.IO.StreamWriter sw = new System.IO.StreamWriter(batchFilnm);
              // Do not change lines / spaces b/w words.
              StringBuilder strSB = new StringBuilder("\r\n\r\n");

              strSB.Append(rpt_SQL);
              //strSB.Append("pg_dump.exe --host localhost" +
              //  " --port " + Global.Portnum +
              //  " --username postgres --format tar --blobs --verbose --file ");
              //strSB.Append("\"" + this.bckpFileDirTextBox.Text + "\\" + dbnm + timeStr + ".backup\"");
              //strSB.Append(" \"" + dbnm + "\"\r\n\r\n");
              ////strSB.Append("\r\n\r\nPAUSE");
              sw.WriteLine(strSB);
              sw.Dispose();
              sw.Close();

              System.Diagnostics.Process processDB = new System.Diagnostics.Process();
              System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
              startInfo.CreateNoWindow = true;
              startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
              startInfo.FileName = batchFilnm;
              startInfo.RedirectStandardError = true;
              startInfo.RedirectStandardOutput = true;
              startInfo.UseShellExecute = false;
              //startInfo.Arguments = "/C xcopy \"" + srcpath + "\" \"" + destpath + "\" /E /I /Q /Y /C";
              processDB.StartInfo = startInfo;
              processDB.EnableRaisingEvents = true;

              processDB.ErrorDataReceived += new System.Diagnostics.DataReceivedEventHandler(processDB_ErrorDataReceived);
              processDB.OutputDataReceived += new System.Diagnostics.DataReceivedEventHandler(processDB_OutputDataReceived);
              processDB.Start();
              processDB.BeginOutputReadLine();
              processDB.BeginErrorReadLine();
              //string output = processDB.StandardOutput.ReadToEnd();
              processDB.WaitForExit();
              if (processDB.ExitCode != 0)
              {
                Global.updateLogMsg(msg_id,
  "\r\n\r\nCommand Line Script Successfully Run!\r\n\r\n",
  log_tbl, dateStr, Global.rnUser_ID);
              }
              else
              {
                Global.updateLogMsg(msg_id,
  "\r\n\r\nCommand Line Script Successfully Run!\r\n\r\n",
  log_tbl, dateStr, Global.rnUser_ID);
              }
              //System.Diagnostics.Process processDB = System.Diagnostics.Process.Start(@"REM_DBBackup.bat");
              do
              {
                //dont perform anything

              }
              while (!processDB.HasExited);

              System.IO.File.Delete(batchFilnm);
            }
            else if (rptName != "Pay Run Master Sheet Report")
            {
              dtst = Global.selectDataNoParams(rpt_SQL.Replace("\r\n", "").Replace("\n", "").Replace("\r", ""));
            }


            //Report Title is Message Title if Alert
            string uptFileUrl = "";
            if (alertID > 0 && msgSentID <= 0)
            {
              DataSet dtstPrm = Global.get_RptParams(rpt_id);
              int ttlRws = dtst.Tables[0].Rows.Count;
              int ttlCols = dtst.Tables[0].Columns.Count;
              for (int z = 0; z < ttlRws; z++)
              {
                toPrsnID = -1;
                toCstmrSpplrID = -1;
                toMails = alrtDtSt.Tables[0].Rows[0][2].ToString();
                ccMails = alrtDtSt.Tables[0].Rows[0][3].ToString();
                bccMails = alrtDtSt.Tables[0].Rows[0][9].ToString();
                sbjct = alrtDtSt.Tables[0].Rows[0][8].ToString();
                msgBdy = alrtDtSt.Tables[0].Rows[0][4].ToString();
                attchMns = alrtDtSt.Tables[0].Rows[0][17].ToString();

                for (int y = 0; y < ttlCols; y++)
                {
                  toMails = toMails.Replace("{:" + dtst.Tables[0].Columns[y].Caption + "}", dtst.Tables[0].Rows[z][y].ToString());
                  ccMails = ccMails.Replace("{:" + dtst.Tables[0].Columns[y].Caption + "}", dtst.Tables[0].Rows[z][y].ToString());
                  bccMails = bccMails.Replace("{:" + dtst.Tables[0].Columns[y].Caption + "}", dtst.Tables[0].Rows[z][y].ToString());
                  sbjct = sbjct.Replace("{:" + dtst.Tables[0].Columns[y].Caption + "}", dtst.Tables[0].Rows[z][y].ToString());
                  msgBdy = msgBdy.Replace("{:" + dtst.Tables[0].Columns[y].Caption + "}", dtst.Tables[0].Rows[z][y].ToString());
                  attchMns = attchMns.Replace("{:" + dtst.Tables[0].Columns[y].Caption + "}", dtst.Tables[0].Rows[z][y].ToString());
                  if (dtst.Tables[0].Columns[y].Caption == "toPrsnID")
                  {
                    toPrsnID = long.Parse(dtst.Tables[0].Rows[z][y].ToString());
                  }
                  if (dtst.Tables[0].Columns[y].Caption == "toCstmrSpplrID")
                  {
                    toCstmrSpplrID = long.Parse(dtst.Tables[0].Rows[z][y].ToString());
                  }
                }
                Thread.Sleep(1000);
                nwMsgSntID = Global.getNewMsgSentID();
                Global.createAlertMsgSent(nwMsgSntID, toMails, ccMails, msgBdy, dateStr,
                  sbjct, rpt_id, bccMails, toPrsnID, toCstmrSpplrID, alertID,
                  attchMns, alertType);
                if (alrtDtSt.Tables[0].Rows[0][12].ToString() == "1")
                {
                  string prmIDs = "";
                  string prmVals = "";
                  string prmValsFnd = "";
                  for (int x = 0; x < dtstPrm.Tables[0].Rows.Count; x++)
                  {
                    prmIDs += dtstPrm.Tables[0].Rows[x][0].ToString() + "|";
                    prmValsFnd = "";
                    for (int r = 0; r < ttlCols; r++)
                    {
                      if (dtstPrm.Tables[0].Rows[x][2].ToString()
                        == "{:" + dtst.Tables[0].Columns[r].Caption + "}")
                      {
                        prmValsFnd = dtst.Tables[0].Rows[z][r].ToString();
                        break;
                      }
                    }
                    prmVals += prmValsFnd + "|";
                  }

                  string colsToGrp1 = rptDtSt.Tables[0].Rows[0][7].ToString();
                  string colsToCnt1 = rptDtSt.Tables[0].Rows[0][8].ToString();
                  string colsToSum1 = rptDtSt.Tables[0].Rows[0][9].ToString();
                  string colsToAvrg1 = rptDtSt.Tables[0].Rows[0][10].ToString();
                  string colsToFrmt1 = rptDtSt.Tables[0].Rows[0][11].ToString();
                  string rpTitle = rptDtSt.Tables[0].Rows[0][0].ToString();

                  //Report Title
                  prmVals += rpTitle + "|";
                  prmIDs += Global.sysParaIDs[0] + "|";
                  //Cols To Group
                  prmVals += colsToGrp1 + "|";
                  prmIDs += Global.sysParaIDs[1] + "|";
                  //Cols To Count
                  prmVals += colsToCnt1 + "|";
                  prmIDs += Global.sysParaIDs[2] + "|";
                  //Cols To Sum
                  prmVals += colsToSum1 + "|";
                  prmIDs += Global.sysParaIDs[3] + "|";
                  //colsToAvrg
                  prmVals += colsToAvrg1 + "|";
                  prmIDs += Global.sysParaIDs[4] + "|";
                  //colsToFrmt
                  prmVals += colsToFrmt1 + "|";
                  prmIDs += Global.sysParaIDs[5] + "|";

                  //outputUsd
                  prmVals += outputUsd + "|";
                  prmIDs += Global.sysParaIDs[6] + "|";

                  //orntnUsd
                  prmVals += orntnUsd + "|";
                  prmIDs += Global.sysParaIDs[7] + "|";

                  Program.gnrtAlertMailerfunc(rpt_id, Global.rnUser_ID, alertID,
                    nwMsgSntID, prmIDs, prmVals, outputUsd, orntnUsd);
                }
                else
                {
                  errMsg = "";
                  if (alertType == "Email")
                  {
                    if (Global.sendEmail(toMails.Replace(";", ",").Trim(seps1), ccMails.Replace(",", ";").Trim(seps1),
                      bccMails.Replace(",", ";").Trim(seps1), attchMns.Replace(",", ";").Trim(seps1), sbjct, msgBdy, ref errMsg) == false)
                    {
                      Global.updateAlertMsgSent(nwMsgSntID, dateStr, "0", errMsg);
                    }
                    else
                    {
                      Global.updateAlertMsgSent(nwMsgSntID, dateStr, "1", "");
                    }
                  }
                  else if (alertType == "SMS")
                  {
                    if (Global.sendSMS(msgBdy, (toMails + ";" + ccMails + ";" + bccMails).Replace(";", ",").Trim(seps1), ref errMsg) == false)
                    {
                      Global.updateAlertMsgSent(nwMsgSntID, dateStr, "0", errMsg);
                    }
                    else
                    {
                      Global.updateAlertMsgSent(nwMsgSntID, dateStr, "1", "");
                    }
                  }
                  else
                  {
                  }
                }
                //Program.updatePrgrm(prgmID);
                //Global.minimizeMemory();
                if ((z % 100) == 0)
                {
                  Thread.Sleep(60000);
                }
              }
            }
            else if (rptType == "System Process")
            {

            }
            else if (rptType == "Alert(SQL Mail List)")
            {
              //check if {:msg_body} and {:alert_type} parameter was set
              //NB sql first column must be valid email address
            }
            else if (rptType == "Alert(SQL Mail List & Message)")
            {
              //Check if  {:alert_type} EMAIL/SMS parameter was set
              //NB sql first column is address and 2nd col is message body
            }
            else if (rptName.Contains("Addresses of Persons"))
            {
              Global.updateRptRn(rpt_run_id, "Formatting Output...", 60);
              if (outputUsd == "PDF")
              {
                Program.AddressesOfPersons(dtst, Global.getRptDrctry() + @"\" + rpt_run_id.ToString() + ".pdf");
                uptFileUrl = Global.getRptDrctry() + @"\" + rpt_run_id.ToString() + ".pdf";
              }
              else
              {
                Program.AddressesOfPersons(dtst, Global.getRptDrctry() + @"\" + rpt_run_id.ToString() + ".doc");
                uptFileUrl = Global.getRptDrctry() + @"\" + rpt_run_id.ToString() + ".doc";
              }
            }
            else if (rptName == "Pay Run Master Sheet Report")
            {
              DataSet hdrdtst = Global.selectDataNoParams(
                "Select distinct tbl1.item_name, tbl1.pay_run_priority, tbl1.payment_date from (" +
                rpt_SQL.Replace("\r\n", "").Replace("\n", "").Replace("\r", "") + ") tbl1 order by 2, 1");

              DataSet prsnDtSt = Global.selectDataNoParams("Select distinct '''' || tbl1.id_num staff_id, tbl1.full_name from (" +
  rpt_SQL.Replace("\r\n", "").Replace("\n", "").Replace("\r", "") + ") tbl1 order by 1, 2");

              Global.updateRptRn(rpt_run_id, "Formatting Output...", 60);
              if (outputUsd == "PDF")
              {
                Global.exprtMasterPayroll(prsnDtSt, hdrdtst, rpt_SQL,
                  Global.getRptDrctry() + @"\" + rpt_run_id.ToString() + ".pdf",
                rptTitle, colsToGrp, colsToCnt, colsToSum, colsToAvrg, colsToFrmt
                  , isfirst, islast, shdAppnd);
                uptFileUrl = Global.getRptDrctry() + @"\" + rpt_run_id.ToString() + ".pdf";
              }
              else
              {
                Global.exprtMasterPayroll(prsnDtSt, hdrdtst, rpt_SQL,
         Global.getRptDrctry() + @"\" + rpt_run_id.ToString() + ".xls",
       rptTitle, colsToGrp, colsToCnt, colsToSum, colsToAvrg, colsToFrmt
         , isfirst, islast, shdAppnd);
                uptFileUrl = Global.getRptDrctry() + @"\" + rpt_run_id.ToString() + ".xls";
              }
            }
            else if (rptName == "Pay Run PAYE Returns Report")
            {
              //            DataSet hdrdtst = Global.selectDataNoParams(
              //              "Select distinct tbl1.item_name, tbl1.pay_run_priority, tbl1.payment_date from (" +
              //              rpt_SQL.Replace("\r\n", "").Replace("\n", "").Replace("\r", "") + ") tbl1 order by 2, 1");

              //            DataSet prsnDtSt = Global.selectDataNoParams("Select distinct '''' || tbl1.id_num staff_id, tbl1.full_name from (" +
              //rpt_SQL.Replace("\r\n", "").Replace("\n", "").Replace("\r", "") + ") tbl1 order by 1, 2");

              Global.updateRptRn(rpt_run_id, "Formatting Output...", 60);
              if (outputUsd == "PDF")
              {
                Global.exprtPAYEPayroll(dtst,
                  Global.getRptDrctry() + @"\" + rpt_run_id.ToString() + ".pdf",
                rptTitle, colsToGrp, colsToCnt, colsToSum, colsToAvrg, colsToFrmt
                  , isfirst, islast, shdAppnd);
                uptFileUrl = Global.getRptDrctry() + @"\" + rpt_run_id.ToString() + ".pdf";
              }
              else
              {
                Global.exprtPAYEPayroll(dtst,
    Global.getRptDrctry() + @"\" + rpt_run_id.ToString() + ".xls",
  rptTitle, colsToGrp, colsToCnt, colsToSum, colsToAvrg, colsToFrmt
    , isfirst, islast, shdAppnd);
                uptFileUrl = Global.getRptDrctry() + @"\" + rpt_run_id.ToString() + ".xls";
              }
            }
            else if (rptName == "Mass Bookings/Reservations Report")
            {
              //            DataSet hdrdtst = Global.selectDataNoParams(
              //              "Select distinct tbl1.item_name, tbl1.pay_run_priority, tbl1.payment_date from (" +
              //              rpt_SQL.Replace("\r\n", "").Replace("\n", "").Replace("\r", "") + ") tbl1 order by 2, 1");

              //            DataSet prsnDtSt = Global.selectDataNoParams("Select distinct '''' || tbl1.id_num staff_id, tbl1.full_name from (" +
              //rpt_SQL.Replace("\r\n", "").Replace("\n", "").Replace("\r", "") + ") tbl1 order by 1, 2");

              Global.updateRptRn(rpt_run_id, "Formatting Output...", 60);
              if (outputUsd == "PDF")
              {
                Global.exprtMassBookings(dtst,
   Global.getRptDrctry() + @"\" + rpt_run_id.ToString() + ".pdf",
 rptTitle, colsToGrp, colsToCnt, colsToSum, colsToAvrg, colsToFrmt
   , isfirst, islast, shdAppnd, fromDate, toDate);
                uptFileUrl = Global.getRptDrctry() + @"\" + rpt_run_id.ToString() + ".pdf";
              }
              else
              {
                Global.exprtMassBookings(dtst,
               Global.getRptDrctry() + @"\" + rpt_run_id.ToString() + ".xls",
             rptTitle, colsToGrp, colsToCnt, colsToSum, colsToAvrg, colsToFrmt
               , isfirst, islast, shdAppnd, fromDate, toDate);
                uptFileUrl = Global.getRptDrctry() + @"\" + rpt_run_id.ToString() + ".xls";
              }
            }
            else if (rptName == "Pay Run SSF Returns Report")
            {
              DataSet hdrdtst = Global.selectDataNoParams(
                "Select distinct tbl1.item_name, tbl1.pay_run_priority, tbl1.payment_date from (" +
                rpt_SQL.Replace("\r\n", "").Replace("\n", "").Replace("\r", "") + ") tbl1 order by 2, 1");

              DataSet prsnDtSt = Global.selectDataNoParams("Select distinct '''' || tbl1.id_num staff_id,'''' || tbl1.ssnit_number social_security_number, tbl1.full_name from (" +
  rpt_SQL.Replace("\r\n", "").Replace("\n", "").Replace("\r", "") + ") tbl1 order by 1, 2");

              Global.updateRptRn(rpt_run_id, "Formatting Output...", 60);
              if (outputUsd == "PDF")
              {
                Global.exprtSSFPayroll(prsnDtSt, hdrdtst, rpt_SQL,
                  Global.getRptDrctry() + @"\" + rpt_run_id.ToString() + ".pdf",
                rptTitle, colsToGrp, colsToCnt, colsToSum, colsToAvrg, colsToFrmt
                  , isfirst, islast, shdAppnd);
                uptFileUrl = Global.getRptDrctry() + @"\" + rpt_run_id.ToString() + ".pdf";
              }
              else
              {
                Global.exprtSSFPayroll(prsnDtSt, hdrdtst, rpt_SQL,
        Global.getRptDrctry() + @"\" + rpt_run_id.ToString() + ".xls",
      rptTitle, colsToGrp, colsToCnt, colsToSum, colsToAvrg, colsToFrmt
        , isfirst, islast, shdAppnd);
                uptFileUrl = Global.getRptDrctry() + @"\" + rpt_run_id.ToString() + ".xls";
              }
            }
            else if (rptName == "Company Bills (Dues/Levies) Report")
            {
              DataSet hdrdtst = Global.selectDataNoParams(
                "Select distinct tbl1.item_code_name, tbl1.cmpny, SUM(tbl1.amnt) from (" +
                rpt_SQL.Replace("\r\n", "").Replace("\n", "").Replace("\r", "") + ") tbl1 group by 1,2 order by 1");

              DataSet prsnDtSt = Global.selectDataNoParams("Select distinct '''' || tbl1.id_no \"GhIE No. \"," +
              "tbl1.fullnm \"Full Name\", tbl1.Grade \"Class\", tbl1.Division from (" +
  rpt_SQL.Replace("\r\n", "").Replace("\n", "").Replace("\r", "") + ") tbl1 order by 1, 2");

              Global.updateRptRn(rpt_run_id, "Formatting Output...", 60);
              if (outputUsd == "PDF")
              {
                Global.exprtCompanyBills(prsnDtSt, hdrdtst, rpt_SQL,
                  Global.getRptDrctry() + @"\" + rpt_run_id.ToString() + ".pdf",
                rptTitle, colsToGrp, colsToCnt, colsToSum, colsToAvrg, colsToFrmt
                  , isfirst, islast, shdAppnd);
                uptFileUrl = Global.getRptDrctry() + @"\" + rpt_run_id.ToString() + ".pdf";
              }
              else
              {
                Global.exprtCompanyBills(prsnDtSt, hdrdtst, rpt_SQL,
          Global.getRptDrctry() + @"\" + rpt_run_id.ToString() + ".xls",
        rptTitle, colsToGrp, colsToCnt, colsToSum, colsToAvrg, colsToFrmt
          , isfirst, islast, shdAppnd);
                uptFileUrl = Global.getRptDrctry() + @"\" + rpt_run_id.ToString() + ".xls";
              }
            }
            else if (rptName == "Pay Run Results Slip")
            {
              Global.exprtPayRunSlipPDF(msPayID, toPrsnID, ref errMsg,
Global.getRptDrctry() + @"\" + rpt_run_id.ToString() + ".pdf", rptTitle);
              uptFileUrl = Global.getRptDrctry() + @"\" + rpt_run_id.ToString() + ".pdf";
              if (errMsg != "")
              {
                Global.updateLogMsg(msg_id,
"\r\n\r\n" + errMsg, log_tbl, dateStr, Global.rnUser_ID);
              }
            }
            else if (rptName == "Bill Run Results Slip")
            {
              Global.exprtBillRunSlipPDF(msPayID, toPrsnID, ref errMsg,
Global.getRptDrctry() + @"\" + rpt_run_id.ToString() + ".pdf", rptTitle, yrStr);
              uptFileUrl = Global.getRptDrctry() + @"\" + rpt_run_id.ToString() + ".pdf";
              if (errMsg != "")
              {
                Global.updateLogMsg(msg_id,
"\r\n\r\n" + errMsg, log_tbl, dateStr, Global.rnUser_ID);
              }
            }
            else if (rptName == "Personal Profile Report")
            {
              Global.exprtPrsnlProfilePDF(toPrsnID, ref errMsg,
Global.getRptDrctry() + @"\" + rpt_run_id.ToString() + ".pdf");
              uptFileUrl = Global.getRptDrctry() + @"\" + rpt_run_id.ToString() + ".pdf";
              if (errMsg != "")
              {
                Global.updateLogMsg(msg_id,
"\r\n\r\n" + errMsg, log_tbl, dateStr, Global.rnUser_ID);
              }
            }

            int totl = 0;
            if (dtst != null)
            {
              totl = dtst.Tables[0].Rows.Count;
            }
            if (totl > 0)
            {
              Global.updateLogMsg(msg_id,
    "\r\n\r\nSQL Statement successfully run! Total Records = " + totl, log_tbl, dateStr, Global.rnUser_ID);

              //2. Check and Format Output in the dataset if Required
              //Based on the 4 Output types decide what to do
              //None|MICROSOFT EXCEL|HTML|STANDARD
              Global.updateRptRn(rpt_run_id, "Formatting Output...", 60);
              //Program.updatePrgrm(prgmID);
              //worker.ReportProgress(60);
              //string outputFileName = "";
              if (outputUsd == "MICROSOFT EXCEL")
              {
                //Global.exprtDtStSaved(dtst,
                //  Global.getRptDrctry() + @"\" + rpt_run_id.ToString() + ".xls",
                //rptTitle, colsToGrp, colsToCnt, colsToSum, colsToAvrg, colsToFrmt
                //  , isfirst, islast, shdAppnd);
              }
              else if (outputUsd == "HTML")
              {
                if (rptLyout == "None" || rptLyout == "TABULAR")
                {
                  Global.exprtToHTMLTblr(dtst,
                   Global.getRptDrctry() +
                @"\amcharts_2100\samples\" + rpt_run_id.ToString() + ".html",
                rptTitle, colsToGrp, colsToCnt, colsToSum, colsToAvrg, colsToFrmt
                , isfirst, islast, shdAppnd);
                }
                else if (rptLyout == "DETAIL")
                {
                  //Show detail HTML Report
                  DataSet grpngsDtSt = Global.get_AllGrpngs(rpt_id);
                  Global.exprtToHTMLDet(dtst, grpngsDtSt,
                  Global.getRptDrctry() +
                  @"\amcharts_2100\samples\" + rpt_run_id.ToString() + ".html",
                  rptTitle, isfirst, islast, shdAppnd, orntnUsd, imgCols);
                }
                uptFileUrl = Global.getRptDrctry() +
           @"\amcharts_2100\samples\" + rpt_run_id.ToString() + ".html";
              }
              else if (outputUsd == "COLUMN CHART")//
              {
                Global.exprtToHTMLSCC(dtst,
    Global.getRptDrctry() +
    @"\amcharts_2100\samples\" + rpt_run_id.ToString() + ".html",
    rptTitle, colsToGrp, colsToCnt, isfirst, islast, shdAppnd);
                uptFileUrl = Global.getRptDrctry() +
      @"\amcharts_2100\samples\" + rpt_run_id.ToString() + ".html";
              }
              else if (outputUsd == "PIE CHART")//
              {
                Global.exprtToHTMLPC(dtst,
    Global.getRptDrctry() +
    @"\amcharts_2100\samples\" + rpt_run_id.ToString() + ".html",
    rptTitle, colsToGrp, colsToCnt, isfirst, islast, shdAppnd);
                uptFileUrl = Global.getRptDrctry() +
    @"\amcharts_2100\samples\" + rpt_run_id.ToString() + ".html";
              }
              else if (outputUsd == "LINE CHART")//
              {
                Global.exprtToHTMLLC(dtst,
    Global.getRptDrctry() +
    @"\amcharts_2100\samples\" + rpt_run_id.ToString() + ".html",
    rptTitle, colsToGrp, colsToCnt, isfirst, islast, shdAppnd);
                uptFileUrl = Global.getRptDrctry() +
     @"\amcharts_2100\samples\" + rpt_run_id.ToString() + ".html";
              }
              else if (outputUsd == "STANDARD")
              {
                if (rptLyout == "None" || rptLyout == "TABULAR")
                {
                  if (totl == 1 && dtst.Tables[0].Columns.Count == 1)
                  {
                    rptOutpt += dtst.Tables[0].Rows[0][0].ToString();
                  }
                  else
                  {
                    rptOutpt += formatDtSt(dtst, rptTitle, colsToGrp, colsToCnt,
                      colsToSum, colsToAvrg, colsToFrmt);
                  }
                }
                else if (rptLyout == "DETAIL")
                {
                  //Show detail STANDARD Report
                }
                if (islast)
                {
                  writeAFile(Global.getRptDrctry() + @"\" + rpt_run_id.ToString() + ".txt", rptOutpt);
                  if (Global.callngAppType == "DESKTOP")
                  {
                    Global.upldImgsFTP(9, Global.getRptDrctry(), Global.runID.ToString() + ".txt");
                  }
                  uptFileUrl = Global.getRptDrctry() + @"\" + rpt_run_id.ToString() + ".txt";
                }
              }
              else if (outputUsd == "PDF")
              {
                if (rptLyout == "None" || rptLyout == "TABULAR")
                {
                  //                  Global.exprtPDFTblr(dtst,
                  //  Global.getRptDrctry() + @"\" + rpt_run_id.ToString() + ".pdf"
                  //, colsToGrp, colsToCnt, colsToSum, colsToAvrg, colsToFrmt
                  //      , isfirst, islast, shdAppnd, rptTitle, orntnUsd);
                }
                else if (rptLyout == "DETAIL")
                {
                  //                //Show detail PDF Report
                  //                DataSet grpngsDtSt = Global.get_AllGrpngs(rpt_id);
                  //                Global.exprtToPDFDet(dtst, grpngsDtSt,
                  //Global.getRptDrctry() + @"\" + rpt_run_id.ToString() + ".pdf",
                  //rptTitle, isfirst, islast, shdAppnd, orntnUsd, imgCols);
                }

              }
              else if (outputUsd == "MICROSOFT WORD")
              {
                if (rptLyout == "None" || rptLyout == "TABULAR")
                {
                }
                else if (rptLyout == "DETAIL")
                {
                  //Show detail MICROSOFT WORD Report
                }
              }
              else if (outputUsd == "CHARACTER SEPARATED FILE (CSV)")
              {
                //Only Tabular Display
                //Get the Delimiter Specified

                Global.exprtDtStToCSV(dtst,
  Global.getRptDrctry() + "/" + rpt_run_id.ToString() + ".csv"
  , isfirst, islast, shdAppnd, rptdlmtr);
                uptFileUrl = Global.getRptDrctry() + @"\" + rpt_run_id.ToString() + ".txt";
              }

              Global.updateRptRn(rpt_run_id, "Storing Output...", 80);
              //worker.ReportProgress(80);
              Global.updateLogMsg(msg_id,
    "\r\n\r\nSaving Report Output...", log_tbl, dateStr, Global.rnUser_ID);
              Global.updateRptRnOutpt(rpt_run_id, rptOutpt);
              Global.updateLogMsg(msg_id,
    "\r\n\r\nSuccessfully Saved Report Output...", log_tbl, dateStr, Global.rnUser_ID);

              if (msgSentID > 0)
              {
                Global.updateRptRn(rpt_run_id, "Sending Output...", 81);
                Global.updateLogMsg(msg_id,
"\r\n\r\nSending Report Via Mail/SMS...", log_tbl, dateStr, Global.rnUser_ID);
                DataSet msgDtSt = Global.get_MsgSentDet(msgSentID);
                toMails = msgDtSt.Tables[0].Rows[0][0].ToString();
                ccMails = msgDtSt.Tables[0].Rows[0][1].ToString();
                bccMails = msgDtSt.Tables[0].Rows[0][6].ToString();
                sbjct = msgDtSt.Tables[0].Rows[0][4].ToString();
                msgBdy = msgDtSt.Tables[0].Rows[0][2].ToString();
                attchMns = msgDtSt.Tables[0].Rows[0][14].ToString() + ";" + uptFileUrl;
                toPrsnID = long.Parse(msgDtSt.Tables[0].Rows[0][7].ToString());
                toCstmrSpplrID = long.Parse(msgDtSt.Tables[0].Rows[0][8].ToString());
                alertType = msgDtSt.Tables[0].Rows[0][15].ToString();

                errMsg = "";
                if (alertType == "Email")
                {
                  if (Global.sendEmail(toMails.Replace(";", ",").Trim(seps1), ccMails.Replace(",", ";").Trim(seps1),
                                     bccMails.Replace(",", ";").Trim(seps1), attchMns.Replace(",", ";").Trim(seps1),
                                     sbjct, msgBdy, ref errMsg) == false)
                  {
                    Global.updateAlertMsgSent(nwMsgSntID, dateStr, "0", errMsg);
                  }
                  else
                  {
                    Global.updateAlertMsgSent(nwMsgSntID, dateStr, "1", "");
                  }
                }
                else if (alertType == "SMS")
                {
                  if (Global.sendSMS(msgBdy, (toMails + ";" + ccMails + ";" + bccMails).Replace(";", ",").Trim(seps1), ref errMsg) == false)
                  {
                    Global.updateAlertMsgSent(nwMsgSntID, dateStr, "0", errMsg);
                  }
                  else
                  {
                    Global.updateAlertMsgSent(nwMsgSntID, dateStr, "1", "");
                  }
                }
                else
                {
                }
                Thread.Sleep(1500);
              }

              Global.updateLogMsg(msg_id,
    "\r\n\r\nSuccessfully Completed Process/Report Run...", log_tbl, dateStr, Global.rnUser_ID);
              Global.updateRptRn(rpt_run_id, "Completed!", 100);

              if (rptType == "Alert(SQL Message)")
              {
                //check if {:to_mail_list} and {:alert_type}  parameter was set
                //NB entire sql output is message body 
                //Report Output file must be added as attachment
              }
            }
            else
            {
              Global.updateLogMsg(msg_id,
    "\r\n\r\nSQL Statement yielded no Results!", log_tbl, dateStr, Global.rnUser_ID);
              Global.updateLogMsg(msg_id,
    "\r\n\r\nSuccessfully Completed Process/Report Run...", log_tbl, dateStr, Global.rnUser_ID);
              Global.updateRptRn(rpt_run_id, "Completed!", 100);
            }
          }
          killThreads();
        }
        killThreads();
      }
      catch (System.Threading.ThreadAbortException thex)
      {
        killThreads();
      }
      catch (Exception ex)
      {
        Global.errorLog = ex.Source + "---" + ex.Message + "\r\n\r\n" + ex.StackTrace + "\r\n\r\n" + ex.InnerException + "\r\n\r\n";
        Global.writeToLog();
        Global.updateRptRn(Global.runID, "Error!", 100);

        long msg_id = Global.getGnrlRecID("rpt.rpt_run_msgs", "process_typ", "process_id", "msg_id", "Process Run", Global.runID);
        Global.updateLogMsg(msg_id,
"\r\n\r\n\r\nThe Program has Errored Out ==>\r\n\r\n" + Global.errorLog,
log_tbl, dateStr, Global.rnUser_ID);
        killThreads();
      }
      finally
      {
      }
    }

    static void processDB_OutputDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
    {
      try
      {
        Global.updateLogMsg(Global.logMsgID,
    "\r\n" + e.Data + "\r\n",
    Global.logTbl, Global.gnrlDateStr, Global.rnUser_ID);
      }
      catch (Exception ex)
      {
        Global.errorLog = "\r\n" + "\r\n\r\n";
        Global.writeToLog();
      }//.Replace(@"\", @"\\")
      finally
      {
      }
    }

    static void processDB_ErrorDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
    {
      try
      {
        Global.updateLogMsg(Global.logMsgID,
    "\r\n" + e.Data + "\r\n",
    Global.logTbl, Global.gnrlDateStr, Global.rnUser_ID);
      }
      catch (Exception ex)
      {
        Global.errorLog = "\r\n" + "\r\n\r\n";
        Global.writeToLog();
      }//.Replace(@"\", @"\\")
      finally
      {
      }
    }

    public static void killThreads()
    {
      try
      {
        Global.mustStop = true;
        Global.minimizeMemory();
        if (threadOne.IsAlive)
        {
          threadOne.Abort();
        }
        if (threadFive.IsAlive)
        {
          threadFive.Abort();
        }
        if (Thread.CurrentThread.IsAlive)
        {
          Thread.CurrentThread.Abort();
        }
        System.Diagnostics.Process.GetProcessById(Global.pid).Kill();
      }
      catch (Exception ex)
      {
        System.Diagnostics.Process.GetProcessById(Global.pid).Kill();
      }
      finally
      {
        if (threadOne.IsAlive)
        {
          threadOne.Abort();
        }
        if (threadFive.IsAlive)
        {
          threadFive.Abort();
        }
        if (Thread.CurrentThread.IsAlive)
        {
          Thread.CurrentThread.Abort();
        }
      }
    }

    static void rqstLstnrUpdtrfunc()
    {
      try
      {
        long prgmID = Global.getGnrlRecID("rpt.rpt_prcss_rnnrs",
          "rnnr_name", "prcss_rnnr_id", runnerName);
        Global.errorLog = "Successfully Started Thread One\r\nProgram ID:" + prgmID + "\r\n";
        Global.writeToLog();
        do
        {
          Program.updatePrgrm(prgmID);
          Global.minimizeMemory();
          Thread.Sleep(4000);
        }
        while (true);
      }
      catch (System.Threading.ThreadAbortException thex)
      {
        killThreads();
      }
      catch (Exception ex)
      {
        //write to log file
        Global.errorLog = ex.Message + "\r\n" + ex.StackTrace + "\r\n" + ex.InnerException + "\r\n";
        Global.writeToLog();
        if (threadOne.IsAlive)
        {
          threadOne.Abort();
        }
      }
      finally
      {
      }
    }

    private static string[] breakDownStr(string inStr, int maxWidth, int maxHeight, Graphics g, float mxTxtWdth)
    {
      string[] nwStr = new string[maxHeight];
      int hghtCntr = 0;
      if (maxWidth < 3 && maxWidth > 1)
      {
        maxWidth = 3;
      }
      else if (maxWidth == 1)
      {
        maxWidth = 1;
        for (int c = 0; c < maxHeight; c++)
        {
          nwStr[c] += "".PadRight(maxWidth, ' ');
        }
        return nwStr;
      }

      inStr = inStr.Replace("\r\n", "");
      inStr = inStr.Replace("\n", "");
      //string steps = "";
      for (int c = 0; c < maxHeight; c++)
      {
        nwStr[c] += "".PadRight(maxWidth, ' ');
      }
      System.Drawing.Font nwFont = new Font("Courier New", 11, FontStyle.Regular);

      string[] mystr = Global.breakTxtDown(inStr,
        mxTxtWdth, nwFont, g);
      for (int c = 0; c < mystr.Length; c++)
      {
        nwStr[c] = mystr[c].PadRight(maxWidth, ' ');
        if (c >= maxHeight - 1)
        {
          return nwStr;
        }
      }
      return nwStr;
    }

    private static bool mustColBeGrpd(string colNo, string[] colsToGrp)
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

    private static bool mustColBeCntd(string colNo, string[] colsToCnt)
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

    private static bool mustColBeSumd(string colNo, string[] colsToSum)
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

    private static bool mustColBeAvrgd(string colNo, string[] colsToAvrg)
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

    private static bool mustColBeFrmtd(string colNo, string[] colsToFrmt)
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

    private static string formatDtSt(DataSet dtst, string rptTitle
      , string[] colsToGrp, string[] colsToCnt,
      string[] colsToSum, string[] colsToAvrg, string[] colsToFrmt)
    {
      string finalStr = rptTitle.ToUpper();
      finalStr += "\r\n\r\n";
      int colCnt = dtst.Tables[0].Columns.Count;

      long[] colcntVals = new long[colCnt];
      double[] colsumVals = new double[colCnt];
      double[] colavrgVals = new double[colCnt];
      finalStr += "|";
      for (int f = 0; f < colCnt; f++)
      {
        int colLen = dtst.Tables[0].Columns[f].ColumnName.Length;
        if (colLen >= 3)
        {
          finalStr += "=".PadRight(colLen, '=');
          finalStr += "|";
        }
      }
      finalStr += "\r\n";
      finalStr += "|";
      for (int e = 0; e < colCnt; e++)
      {
        int colLen = dtst.Tables[0].Columns[e].ColumnName.Length;
        if (colLen >= 3)
        {
          if (mustColBeFrmtd(e.ToString(), colsToFrmt) == true)
          {
            finalStr += dtst.Tables[0].Columns[e].ColumnName.Substring(0, colLen - 2).Trim().PadLeft(colLen, ' ');
          }
          else
          {
            finalStr += dtst.Tables[0].Columns[e].ColumnName.Substring(0, colLen - 2).PadRight(colLen, ' ');
          }
          finalStr += "|";
        }
      }
      finalStr += "\r\n";
      finalStr += "|";
      for (int f = 0; f < colCnt; f++)
      {
        int colLen = dtst.Tables[0].Columns[f].ColumnName.Length;
        if (colLen >= 3)
        {
          finalStr += "=".PadRight(colLen, '=');
          finalStr += "|";
        }
      }
      finalStr += "\r\n";
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        string[][] lineFormat = new string[colCnt][];
        for (int a = 0; a < colCnt; a++)
        {
          double nwval = 0;
          bool mstgrp = mustColBeGrpd(a.ToString(), colsToGrp);
          if (mustColBeCntd(a.ToString(), colsToCnt) == true)
          {
            if ((i > 0) && (dtst.Tables[0].Rows[i - 1][a].ToString()
            == dtst.Tables[0].Rows[i][a].ToString())
            && (mstgrp == true))
            {
            }
            else
            {
              colcntVals[a] += 1;
            }
          }
          else if (mustColBeSumd(a.ToString(), colsToSum) == true)
          {
            double.TryParse(dtst.Tables[0].Rows[i][a].ToString(), out nwval);
            if ((i > 0) && (dtst.Tables[0].Rows[i - 1][a].ToString()
  == dtst.Tables[0].Rows[i][a].ToString())
  && (mstgrp == true))
            {
            }
            else
            {
              colsumVals[a] += nwval;
            }
          }
          else if (mustColBeAvrgd(a.ToString(), colsToAvrg) == true)
          {
            double.TryParse(dtst.Tables[0].Rows[i][a].ToString(), out nwval);
            if ((i > 0) && (dtst.Tables[0].Rows[i - 1][a].ToString()
== dtst.Tables[0].Rows[i][a].ToString())
&& (mstgrp == true))
            {
            }
            else
            {
              colcntVals[a] += 1;
              colsumVals[a] += nwval;
            }
          }

          int colLen = dtst.Tables[0].Columns[a].ColumnName.Length;
          string[] arry;
          if (colLen >= 3)
          {
            if ((i > 0) && (dtst.Tables[0].Rows[i - 1][a].ToString()
              == dtst.Tables[0].Rows[i][a].ToString())
              && (mustColBeGrpd(a.ToString(), colsToGrp) == true))
            {
              System.Drawing.Image img = Image.FromFile(Global.appStatPath + "/staffs.png");
              System.Drawing.Font nwFont = new Font("Courier New", 11, FontStyle.Regular);
              Graphics g = Graphics.FromImage(img);
              float ght = g.MeasureString(dtst.Tables[0].Columns[a].ColumnName.Trim().PadRight(colLen, '=')
                , nwFont).Width;
              float ght1 = g.MeasureString("="
   , nwFont).Width;
              arry = breakDownStr("    ", colLen, 25, g, ght - ght1);
            }
            else
            {
              System.Drawing.Image img = Image.FromFile(Global.appStatPath + "/staffs.png");
              System.Drawing.Font nwFont = new Font("Courier New", 11, FontStyle.Regular);
              Graphics g = Graphics.FromImage(img);
              float ght = g.MeasureString(dtst.Tables[0].Columns[a].ColumnName.Trim().PadRight(colLen, '=')
                , nwFont).Width;
              float ght1 = g.MeasureString("="
               , nwFont).Width;
              arry = breakDownStr(dtst.Tables[0].Rows[i][a].ToString(),
                colLen, 25, g, ght - ght1);
            }
            lineFormat[a] = arry;
          }
        }
        string frshLn = "";
        for (int c = 0; c < 25; c++)
        {
          string frsh = "|";
          for (int b = 0; b < colCnt; b++)
          {
            int colLen = dtst.Tables[0].Columns[b].ColumnName.Length;
            if (colLen >= 3)
            {
              if (mustColBeFrmtd(b.ToString(), colsToFrmt) == true)
              {
                double num = 0;
                double.TryParse(lineFormat[b][c].Trim(), out num);
                if (lineFormat[b][c].Trim() != "")
                {
                  frsh += num.ToString("#,##0.00").PadLeft(colLen, ' ').Substring(0, colLen);//.Trim().PadRight(60, ' ')
                }
                else
                {
                  frsh += lineFormat[b][c].Substring(0, colLen); //.Trim().PadRight(60, ' ')
                }
              }
              else
              {
                frsh += lineFormat[b][c].Substring(0, colLen); //.Trim().PadRight(60, ' ')
              }
              frsh += "|";
            }
          }
          string nwtst = frsh;
          frsh += "\r\n";
          if (nwtst.Replace("|", " ").Trim() == "")
          {
            c = 24;
          }
          else
          {
            frshLn += frsh;
          }
        }
        finalStr += frshLn;
      }
      finalStr += "\r\n";
      finalStr += "|";
      for (int f = 0; f < colCnt; f++)
      {
        int colLen = dtst.Tables[0].Columns[f].ColumnName.Length;
        if (colLen >= 3)
        {
          finalStr += "=".PadRight(colLen, '=');
          finalStr += "|";
        }
      }
      finalStr += "\r\n";
      finalStr += "|";
      //Populate Counts/Sums/Averages
      for (int f = 0; f < colCnt; f++)
      {
        int colLen = dtst.Tables[0].Columns[f].ColumnName.Length;
        if (colLen >= 3)
        {
          if (mustColBeCntd(f.ToString(), colsToCnt) == true)
          {
            if (mustColBeFrmtd(f.ToString(), colsToFrmt) == true)
            {
              finalStr += ("Count = " + colcntVals[f].ToString("#,##0")).PadLeft(colLen, ' ').Substring(0, colLen); ;
            }
            else
            {
              finalStr += ("Count = " + colcntVals[f].ToString()).PadRight(colLen, ' ').Substring(0, colLen); ;
            }
          }
          else if (mustColBeSumd(f.ToString(), colsToSum) == true)
          {
            if (mustColBeFrmtd(f.ToString(), colsToFrmt) == true)
            {
              finalStr += ("Sum = " + colsumVals[f].ToString("#,##0.00")).PadLeft(colLen, ' ').Substring(0, colLen); ;
            }
            else
            {
              finalStr += ("Sum = " + colsumVals[f].ToString()).PadRight(colLen, ' ').Substring(0, colLen); ;
            }
          }
          else if (mustColBeAvrgd(f.ToString(), colsToAvrg) == true)
          {
            if (mustColBeFrmtd(f.ToString(), colsToFrmt) == true)
            {
              finalStr += ("Average = " + (colsumVals[f] / colcntVals[f]).ToString("#,##0.00")).PadLeft(colLen, ' ').Substring(0, colLen); ;
            }
            else
            {
              finalStr += ("Average = " + (colsumVals[f] / colcntVals[f]).ToString()).PadRight(colLen, ' ').Substring(0, colLen); ;
            }
          }
          else
          {
            finalStr += " ".PadRight(colLen, ' ').Substring(0, colLen); ;
          }
          finalStr += "|";
        }

      }
      finalStr += "\r\n";
      finalStr += "|";
      for (int f = 0; f < colCnt; f++)
      {
        int colLen = dtst.Tables[0].Columns[f].ColumnName.Length;
        if (colLen >= 3)
        {
          finalStr += "-".PadRight(colLen, '-').Substring(0, colLen); ;
          finalStr += "|";
        }
      }
      finalStr += "\r\n";
      return finalStr;
    }

    static void doSthing(long b, string str)
    {
      StreamWriter fileWriter;
      string fileLoc = @"C:\Users\rhemitech_gh\Desktop\REMSCustomRunnerFiles\";
      fileLoc += str + DateTime.Now.ToString("ddMMMyyyyHHmmss") + b.ToString() + ".rho";


      fileWriter = new StreamWriter(fileLoc, true);
      //fileWriter. = txt.(fileLoc);
      fileWriter.WriteLine(str + b.ToString());
      fileWriter.WriteLine(Global.errorLog);
      fileWriter.Close();
      fileWriter = null;

    }

    static void writeAFile(string fullfilenm, string cntnt)
    {
      try
      {
        StreamWriter fileWriter;
        string fileLoc = fullfilenm;
        fileWriter = new StreamWriter(fileLoc, true);
        fileWriter.WriteLine(cntnt);
        fileWriter.Close();
        fileWriter = null;
      }
      catch (Exception ex)
      {
      }
      finally
      {
      }
    }

    private static void AddressesOfPersons(DataSet mnDtSt, string wrdFileNm)
    {
      try
      {
        //this.cancelButton.Text = "Cancel";
        //this.progressLabel.Text = "Exporting Report to Word Document...---0% Complete";
        ////this.progressBar1.Value = (int)(((Decimal)j / (Decimal)l) * 100);
        System.Windows.Forms.Application.DoEvents();
        Global.errorLog += "Inside Letter";
        object oMissing = System.Reflection.Missing.Value;
        object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */
        //Global.writeToLog();
        Microsoft.Office.Interop.Word.Application oWord = new Microsoft.Office.Interop.Word.Application();
        oWord.Visible = false;
        //oWord.Activate();
        //oWord.ShowMe();
        object s_missing = System.Reflection.Missing.Value;
        object lnkToFile = false;
        object saveWithDoc = true;
        object oFalse = false;
        object oTrue = true;

        Microsoft.Office.Interop.Word.Document oDoc = oWord.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oMissing);


        //SETTING FOCUES ON THE PAGE HEADER TO EMBED THE WATERMARK

        //oWord.ActiveWindow.ActivePane.View.SeekView = Word.WdSeekView.wdSeekCurrentPageHeader;
        //THE LOGO IS ASSIGNED TO A SHAPE OBJECT SO THAT WE CAN USE ALL THE
        //SHAPE FORMATTING OPTIONS PRESENT FOR THE SHAPE OBJECT
        //Word.InlineShape logoCustom = null;
        //THE PATH OF THE LOGO FILE TO BE EMBEDDED IN THE HEADER
        //String logoPath = Global.getRptDrctry() + @"\amcharts_2100\images\" + Global.UsrsOrg_ID.ToString() + ".png";
        //System.IO.File.Copy(Global.getOrgImgsDrctry() + @"\" + Global.UsrsOrg_ID.ToString() + ".png",
        //  Global.getRptDrctry() + @"\amcharts_2100\images\" + Global.UsrsOrg_ID.ToString() + ".png", true);
        //Global.errorLog += logoPath;
        //Global.writeToLog();
        //if (Global.callngAppType == "DESKTOP")
        //{
        //  Global.upldImgsFTP(9, Global.getRptDrctry(), @"\amcharts_2100\images\" + Global.UsrsOrg_ID.ToString() + ".png");
        //}
        Color c = Color.FromArgb(0, 112, 155);
        var myWdColor = (Microsoft.Office.Interop.Word.WdColor)(c.R + 0x100 * c.G + 0x10000 * c.B);
        //Org Name
        //string orgNm = Global.getOrgName(Global.UsrsOrg_ID);
        //string resAddrs = Global.getOrgResAddrs(Global.UsrsOrg_ID);
        //string pstl = Global.getOrgPstlAddrs(Global.UsrsOrg_ID);
        ////Contacts Nos
        //string cntcts = Global.getOrgContactNos(Global.UsrsOrg_ID);
        ////Email Address
        //string email = Global.getOrgEmailAddrs(Global.UsrsOrg_ID);
        //string webste = Global.getOrgWebsite(Global.UsrsOrg_ID);
        //string prfx = "GhIE/PF";
        ////int refNum = 0;


        //Add header into the document
        //foreach (Microsoft.Office.Interop.Word.Section section in oDoc.Sections)
        //{
        //  //Get the header range and add the header details.
        //  Microsoft.Office.Interop.Word.Range headerRange = section.Headers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
        //  headerRange.Fields.Add(headerRange, ref oMissing, ref oMissing, ref oMissing);
        //  headerRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
        //  headerRange.Font.Color = myWdColor;
        //  headerRange.Font.Size = 8;
        //  //headerRange.Text = "Header text goes here";

        //  //Create a 5X5 table and insert some dummy record
        //  Word.Table firstTable = oDoc.Tables.Add(headerRange, 1, 2, ref s_missing, ref s_missing);

        //  firstTable.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
        //  firstTable.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom].Color = myWdColor;
        //  firstTable.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom].LineWidth = Microsoft.Office.Interop.Word.WdLineWidth.wdLineWidth150pt;

        //  //firstTable.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
        //  //firstTable.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop].Color = myWdColor;
        //  //firstTable.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop].LineWidth = Microsoft.Office.Interop.Word.WdLineWidth.wdLineWidth150pt;


        //  foreach (Word.Row row in firstTable.Rows)
        //  {
        //    foreach (Word.Cell cell in row.Cells)
        //    {
        //      //Header row
        //      cell.Range.Font.Bold = 1;
        //      //other format properties goes here
        //      cell.Range.Font.Name = "Tahoma";
        //      cell.Range.Font.Size = 8;
        //      cell.Range.Font.Color = myWdColor;
        //      //cell.Shading.BackgroundPatternColor = WdColor.wdColorGray25;
        //      //Center alignment for the Header cells
        //      cell.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
        //      cell.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
        //      if (cell.ColumnIndex == 1)
        //      {
        //        cell.Width = 70;
        //        logoCustom = cell.Range.InlineShapes.AddPicture(logoPath, ref oFalse, ref oTrue, ref oMissing);
        //        logoCustom.Select();
        //        logoCustom.ScaleWidth = 23;
        //        logoCustom.ScaleHeight = 23;
        //      }
        //      //Data row
        //      else
        //      {
        //        Word.Table firstTable1 = oDoc.Tables.Add(cell.Range, 2, 2, ref s_missing, ref s_missing);
        //        //object bfrRow = Microsoft.Office.Interop.Word.Row;
        //        firstTable1.Rows.Add(ref s_missing);
        //        firstTable1.Rows[1].Cells[1].Width = 400;
        //        firstTable1.Rows[1].Cells[1].Range.Font.Size = 15;
        //        firstTable1.Rows[1].Cells[1].Range.Text = orgNm.Replace("\r\n", "");
        //        firstTable1.Rows[1].Cells[1].Borders.Enable = 0;
        //        firstTable1.Rows[2].Cells[1].Width = 400;
        //        firstTable1.Rows[2].Cells[1].Range.Font.Size = 8;
        //        //firstTable1.Rows[2].Cells[1].Range.Text = resAddrs.Replace("\r\n", "");
        //        firstTable1.Rows[2].Cells[1].Range.InsertAfter("  " + resAddrs.Replace("\r\n", ""));
        //        firstTable1.Rows[2].Cells[1].Range.InsertAfter("  " + "\r\n");
        //        firstTable1.Rows[2].Cells[1].Range.InsertAfter("  " + pstl.Replace("\r\n", ""));
        //        firstTable1.Rows[2].Cells[1].Range.InsertAfter(" " + "\r\n");
        //        firstTable1.Rows[2].Cells[1].Range.InsertAfter("  " + cntcts.Replace("\r\n", ""));
        //        firstTable1.Rows[2].Cells[1].Range.InsertAfter(" " + "\r\n");
        //        firstTable1.Rows[2].Cells[1].Range.InsertAfter("  " + email.Replace("\r\n", ""));
        //        firstTable1.Rows[2].Cells[1].Range.InsertAfter(" ");
        //        firstTable1.Rows[2].Cells[1].Range.InsertAfter(", Website: " + webste.Replace("\r\n", ""));

        //        firstTable1.Rows[2].Cells[1].Borders.Enable = 0;

        //        //firstTable1.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
        //        //firstTable1.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop].ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdBlue;
        //        //firstTable1.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop].LineWidth = Microsoft.Office.Interop.Word.WdLineWidth.wdLineWidth150pt;
        //        cell.Width = 430;
        //        //cell.Range.InsertAfter("\r\n");
        //        //cell.Range.InsertAfter(" " + orgNm.Replace("\r\n", ""));
        //        //cell.Range.InsertAfter(" " + "\r\n");
        //        //cell.Range.InsertAfter("\r\n");
        //      }
        //    }
        //  }
        //}

        //Add the footers into the document
        //foreach (Microsoft.Office.Interop.Word.Section wordSection in oDoc.Sections)
        //{
        //  //Get the footer range and add the footer details.
        //  Microsoft.Office.Interop.Word.Range footerRange = wordSection.Footers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
        //  footerRange.Font.Color = myWdColor;
        //  footerRange.Font.Size = 10;
        //  //footerRange.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
        //  //footerRange.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop].Color = myWdColor;
        //  //footerRange.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop].LineWidth = Microsoft.Office.Interop.Word.WdLineWidth.wdLineWidth150pt;


        //  Word.Table footerTable = oDoc.Tables.Add(footerRange, 1, 2, ref s_missing, ref s_missing);

        //  footerTable.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
        //  footerTable.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop].Color = myWdColor;
        //  footerTable.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop].LineWidth = Microsoft.Office.Interop.Word.WdLineWidth.wdLineWidth150pt;

        //  //firstTable.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
        //  //firstTable.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop].Color = myWdColor;
        //  //firstTable.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop].LineWidth = Microsoft.Office.Interop.Word.WdLineWidth.wdLineWidth150pt;
        //  string prsdnt = "President-" + Global.getPosHldrName("President");
        //  string execSec = "Executive Secretary-" + Global.getPosHldrName("Executive Secretary");
        //  int prsPortn = (int)(((double)prsdnt.Length / (double)(prsdnt.Length + execSec.Length)) * 500);

        //  foreach (Word.Row row in footerTable.Rows)
        //  {
        //    foreach (Word.Cell cell in row.Cells)
        //    {
        //      //Header row
        //      cell.Range.Font.Bold = 1;
        //      //other format properties goes here
        //      cell.Range.Font.Name = "Tahoma";
        //      cell.Range.Font.Size = 8;
        //      cell.Range.Font.Color = myWdColor;
        //      //cell.Shading.BackgroundPatternColor = WdColor.wdColorGray25;
        //      //Center alignment for the Header cells
        //      cell.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
        //      cell.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
        //      if (cell.ColumnIndex == 1)
        //      {
        //        cell.Width = prsPortn;
        //        cell.Range.Text = prsdnt;
        //        cell.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
        //      }
        //      //Data row
        //      else
        //      {
        //        cell.Width = 500 - prsPortn;
        //        cell.Range.Text = execSec;
        //        cell.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
        //      }
        //    }
        //  }
        //}

        //SETTING FOCUES BACK TO DOCUMENT
        oWord.ActiveWindow.ActivePane.View.SeekView = Word.WdSeekView.wdSeekMainDocument;
        oWord.ActiveWindow.Selection.ParagraphFormat.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceSingle;
        oWord.ActiveWindow.Selection.ParagraphFormat.SpaceAfter = 0.2F;
        oDoc.PageSetup.Orientation = Microsoft.Office.Interop.Word.WdOrientation.wdOrientPortrait;
        /*
        1. Select overall score and overall rating
        2. select all sub items to know the number
        3. select all sub sub observations etc
        4. loop through the sub-major items and fill the table accordingly
        5. within this loop, go through the sub sub findings and display them as well*/

        int a = 0;
        int majCnter = 0;
        for (a = 0; a < 1; a++)
        {
          majCnter += 1;

          int ttl = mnDtSt.Tables[0].Rows.Count;
          double ttl1 = 0;

          Microsoft.Office.Interop.Word.Paragraph oPara4;
          oPara4 = oDoc.Paragraphs.Add(ref oMissing);
          oPara4.Format.SpaceAfter = 1;
          oPara4.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
          oPara4.Range.Font.Bold = 0;
          oPara4.Range.Font.Name = "Arial";
          oPara4.Range.Font.Size = 12;
          oPara4.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;

          Word.Table scndTable = oDoc.Tables.Add(oPara4.Range, 1, 2, ref s_missing, ref s_missing);
          scndTable.BottomPadding = 0;
          scndTable.Spacing = 0;

          scndTable.Borders.Enable = 1;
          string[] itms = new string[ttl + 1];
          double[] amnt = new double[ttl + 1];
          //Word.Row row in scndTable.Rows
          for (int k = 0; k < ttl + 1; k++)
          {
            if (k == 0)
            {
              itms[k] = "No.";
              amnt[k] = 0;
            }
            else if (k <= ttl)
            {
              itms[k] = "";// mnDtSt.Tables[0].Rows[k - 1][11].ToString();
              amnt[k] = 0;// double.Parse(mnDtSt.Tables[0].Rows[k - 1][13].ToString());
              //Global.errorLog += itms[k] + "/" + amnt[k].ToString();
              //Global.writeToLog();
            }
            if (k > 0)
            {
              scndTable.Rows.Add(ref s_missing);
            }
            //scndTable.Rows[k + 1].Height = 0.2F;
            foreach (Word.Cell cell in scndTable.Rows[k + 1].Cells)
            {
              //Header row
              cell.Range.Font.Bold = 0;
              //other format properties goes here
              cell.Range.Font.Name = "Arial";
              cell.Range.Font.Size = 13;
              cell.Range.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdAuto;
              //Center alignment for the Header cells
              cell.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
              cell.Range.ParagraphFormat.SpaceAfter = 1;
              if (k == 0)
              {
                cell.Range.Font.Bold = 1;
                cell.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorWhite;
                if (cell.ColumnIndex == 1)
                {
                  cell.Width = 200;
                  cell.Range.Text = "No.";
                  cell.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                }
                else
                {
                  cell.Width = 300;
                  cell.Range.Text = "Address";
                  cell.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                }
              }
              else
              {
                cell.Range.Font.Bold = 0;
                cell.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorWhite;
                if (k <= ttl)
                {
                  if (cell.ColumnIndex == 1)
                  {
                    cell.Width = 200;
                    cell.Range.Text = mnDtSt.Tables[0].Rows[k - 1][0].ToString();
                    cell.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                  }
                  else
                  {
                    for (int y = 1; y < mnDtSt.Tables[0].Columns.Count; y++)
                    {
                      cell.Width = 300;
                      cell.Range.InsertAfter(mnDtSt.Tables[0].Rows[k - 1][y].ToString());
                      cell.Range.InsertAfter(Environment.NewLine);
                      //ttl1 += amnt[k];
                      cell.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    }
                  }
                }
              }
            }
            //if (row.Index > 1)
            //{
            //  k++;
            //}
            //if (k == ttl + 2 - 1)
            //{
            //  //scndTable.Rows.Add(ref s_missing);
            //  scndTable.Cell((k + 1), 1).Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorWhite;
            //  scndTable.Cell((k + 1), 1).Range.Font.Bold = 1;
            //  scndTable.Cell((k + 1), 1).Range.Font.Size = 12;
            //  scndTable.Cell((k + 1), 1).Width = 250;
            //  scndTable.Cell((k + 1), 1).Borders.Enable = 0;
            //  scndTable.Cell((k + 1), 1).Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
            //  scndTable.Cell((k + 1), 1).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
            //  scndTable.Cell((k + 1), 1).Range.Text = "TOTAL";

            //  scndTable.Cell((k + 1), 2).Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorWhite;
            //  scndTable.Cell((k + 1), 2).Range.Font.Bold = 1;
            //  scndTable.Cell((k + 1), 2).Range.Font.Size = 12;
            //  scndTable.Cell((k + 1), 2).Borders.Enable = 1;
            //  scndTable.Cell((k + 1), 2).Width = 100;
            //  scndTable.Cell((k + 1), 2).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
            //  scndTable.Cell((k + 1), 2).Range.Text = "GH₵" + ttl1.ToString("#,##0.00");
            //  //}
            //  break;
            //}
          }

          oPara4.Range.InsertParagraphAfter();
          //oPara4.Range.InsertParagraphAfter();


        }


        //end p;

        //this.progressLabel.Text = "Exporting Report to Word Document---....100% Complete";
        //this.progressBar1.Value = 100;
        //this.cancelButton.Text = "Finish";
        if (true)
        {
          object svFleNm = (object)wrdFileNm;
          //Global.errorLog += svFleNm.ToString();
          Global.updateRptRn(Global.runID, "Storing Output...", 80);
          object flFrmt = s_missing;
          if (wrdFileNm.Contains(".pdf"))
          {
            flFrmt = (object)Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF;
          }
          else
          {
            flFrmt = (object)Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocument;
          }
          object nllVal = null;
          oDoc.SaveAs(ref svFleNm, ref flFrmt,
              ref s_missing, ref s_missing, ref s_missing, ref s_missing, ref s_missing, ref s_missing,
              ref s_missing, ref s_missing, ref s_missing, ref s_missing, ref s_missing, ref s_missing,
              ref s_missing, ref s_missing);
          if (Global.callngAppType == "DESKTOP")
          {
            if (wrdFileNm.Contains(".pdf"))
            {
              Global.upldImgsFTP(9, Global.getRptDrctry(), Global.runID.ToString() + ".pdf");
            }
            else
            {
              Global.upldImgsFTP(9, Global.getRptDrctry(), Global.runID.ToString() + ".doc");
            }
          }
          //worker.ReportProgress(80);
          //          Global.updateLogMsg(msg_id,
          //"\r\n\r\nSaving Report Output...", log_tbl, dateStr, Global.rnUser_ID);
          //          Global.updateRptRnOutpt(rpt_run_id, rptOutpt);
          //          Global.updateLogMsg(msg_id,
          //"\r\n\r\nSuccessfully Saved Report Output...", log_tbl, dateStr, Global.rnUser_ID);
          //          Global.updateLogMsg(msg_id,
          //"\r\n\r\nSuccessfully Completed Process/Report Run...", log_tbl, dateStr, Global.rnUser_ID);
          object savChngs = Word.WdSaveOptions.wdDoNotSaveChanges;
          oDoc.Close(ref savChngs, ref s_missing, ref s_missing);
          oDoc = null;
          oWord.Quit(ref savChngs, ref s_missing, ref s_missing);
          oWord = null;

          Global.updateRptRn(Global.runID, "Completed!", 100);

          Global.minimizeMemory();
        }
      }
      catch (Exception ex)
      {
        Global.errorLog += ex.InnerException + "\r\n" + ex.StackTrace;
        Global.writeToLog();
      }
    }

    private static T _download_serialized_json_data<T>(string url) where T : new()
    {
      using (var w = new WebClient())
      {
        var json_data = string.Empty;
        // attempt to download JSON data as a string
        try
        {
          json_data = w.DownloadString(url);
        }
        catch (Exception) { }
        // if string with JSON data is not empty, deserialize it to class and return its instance 
        return !string.IsNullOrEmpty(json_data) ? JsonConvert.DeserializeObject<T>(json_data) : new T();
      }
    }

    //public static void updateRates(string dateStr)
    //{
    //  // = Global.getDB_Date_time().Substring(0, 10);
    //  var url = "https://openexchangerates.org/api/historical/" + dateStr + ".json?app_id=5dba57b2d47b4a11b4e5a020522de567";
    //  var currencyRates = _download_serialized_json_data<CurrencyRates>(url);
    //  string baseCur = currencyRates.Base;
    //  long rateID = -1;
    //  double rateVal = 0;
    //  string funcCurCode = "GHS";
    //  DataSet dtst = Global.get_Currencies(funcCurCode);
    //  double baseToFuncCurRate = 0;
    //  int fromCurID = -1;
    //  int toCurID = Global.getPssblValID(funcCurCode, Global.getLovID("Currencies"));
    //  for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
    //  {
    //    fromCurID = Global.getPssblValID(dtst.Tables[0].Rows[i][1].ToString(), Global.getLovID("Currencies"));
    //    if (i == 0)
    //    {
    //      rateID = Global.doesRateExst(dateStr, baseCur, funcCurCode);
    //      double.TryParse(currencyRates.Rates[funcCurCode].ToString(), out rateVal);
    //      baseToFuncCurRate = rateVal;
    //    }
    //    if (baseCur != dtst.Tables[0].Rows[i][1].ToString())
    //    {
    //      double.TryParse(currencyRates.Rates[dtst.Tables[0].Rows[i][1].ToString()].ToString(), out rateVal);
    //      if (rateVal > 0)
    //      {
    //        rateVal = baseToFuncCurRate / rateVal;
    //      }
    //    }
    //    if (rateVal > 0)
    //    {
    //      if (rateID <= 0)
    //      {
    //        Global.createRate(dateStr, dtst.Tables[0].Rows[i][1].ToString(), fromCurID, funcCurCode, toCurID,
    //          rateVal);
    //      }
    //      else
    //      {
    //        Global.updtRateValue(rateID, rateVal);
    //      }
    //    }
    //  }

    //}
    public void updateRates(string dateStr)
    {
      // = Global.getDB_Date_time().Substring(0, 10);
      if (Global.UsrsOrg_ID <= 0)
      {
        return;
      }
      string rateDte = DateTime.ParseExact(dateStr, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

      var url = "https://openexchangerates.org/api/historical/" + rateDte + ".json?app_id=5dba57b2d47b4a11b4e5a020522de567";
      var currencyRates = _download_serialized_json_data<CurrencyRates>(url);
      string baseCur = currencyRates.Base.Trim();
      //Global.mnFrm.cmCde.showMsg(baseCur,0);
      long rateID = -1;
      double rateVal = 0;

      int toCurID = Global.getOrgFuncCurID(Global.UsrsOrg_ID);

      string funcCurCode = Global.getPssblValNm(toCurID);
      DataSet dtst = Global.get_Currencies(funcCurCode);
      double baseToFuncCurRate = 0;
      int fromCurID = -1;
      double.TryParse(currencyRates.Rates[funcCurCode].ToString(), out rateVal);
      baseToFuncCurRate = rateVal;

      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        fromCurID = Global.getPssblValID(dtst.Tables[0].Rows[i][1].ToString(), Global.getLovID("Currencies"));
        rateID = Global.doesRateExst(dateStr, dtst.Tables[0].Rows[i][1].ToString(), funcCurCode);
        double.TryParse(currencyRates.Rates[dtst.Tables[0].Rows[i][1].ToString()].ToString(), out rateVal);

        if (rateVal > 0)
        {
          rateVal = (baseToFuncCurRate / rateVal);
          if (rateID <= 0)
          {
            Global.createRate(dateStr, dtst.Tables[0].Rows[i][1].ToString(), fromCurID, funcCurCode, toCurID,
              rateVal);
          }
          else
          {
            Global.updtRateValue(rateID, rateVal);
          }
        }
      }

    }

    static string removeInvalidChars(string s, string replaceWith)
    {
      StringBuilder result = new StringBuilder();
      for (int i = 0; i < s.Length; i++)
      {
        char c = s[i];
        byte b = (byte)c;
        //Global.mnFrm.cmCde.showMsg(b.ToString() + "/" + c.ToString(), 0);
        if (b <= 32 || b >= 127)
          result.Append(replaceWith);
        else
          result.Append(c);
      }
      return result.ToString();
    }

    public static void updatePhoneNumbers(long prgmID)
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
      DataSet dtst = Global.selectDataNoParams(strSQL);
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
        email = removeInvalidChars(email, "").Replace(":", ",").Replace(";", ",").Replace("/", ",").Replace(@"\", ",").Replace(",", ", ").Replace("   ", " ").Replace("  ", " ").Trim(trmChr);
        cntcNo = removeInvalidChars(cntcNo, "").Replace(":", ",").Replace(";", ",").Replace("/", ",").Replace(@"\", ",").Replace(",", ", ").Replace("   ", " ").Replace("  ", " ").Trim(trmChr);
        cntcMobl = removeInvalidChars(cntcMobl, "").Replace(":", ",").Replace(";", ",").Replace("/", ",").Replace(@"\", ",").Replace(",", ", ").Replace("   ", " ").Replace("  ", " ").Trim(trmChr);
        cntcFax = removeInvalidChars(cntcFax, "").Replace(":", ",").Replace(";", ",").Replace("/", ",").Replace(@"\", ",").Replace(",", ", ").Replace("   ", " ").Replace("  ", " ").Trim(trmChr);

        string[] emails = email.Split(w, StringSplitOptions.RemoveEmptyEntries);
        string[] cntcNos = cntcNo.Split(w, StringSplitOptions.RemoveEmptyEntries);
        string[] cntcMobls = cntcMobl.Split(w, StringSplitOptions.RemoveEmptyEntries);
        for (int y = 0; y < cntcMobls.Length; y++)
        {
          if (cntcMobls[y].Trim(trmChr).Length == 10)
          {
            if (cntcMobls[y].Trim(trmChr).Substring(0, 1) == "0")
            {
              cntcMobls[y] = "+233" + cntcMobls[y].Trim(trmChr).Substring(1);
            }
          }
        }
        for (int y = 0; y < cntcNos.Length; y++)
        {
          if (cntcNos[y].Trim(trmChr).Length == 10)
          {
            if (cntcNos[y].Trim(trmChr).Substring(0, 1) == "0")
            {
              cntcNos[y] = "+233" + cntcNos[y].Trim(trmChr).Substring(1);
            }
          }
        }
        string[] cntcFaxs = cntcFax.Split(w, StringSplitOptions.RemoveEmptyEntries);

        string updtSQL = @"UPDATE prs.prsn_names_nos SET 
                           email='" + email.Replace("'", "''") + @"', 
                           cntct_no_tel='" + string.Join(", ", cntcNos).Replace("   ", " ").Replace("  ", " ").Replace("'", "''").Trim(trmChr) + @"', 
                           cntct_no_mobl='" + string.Join(", ", cntcMobls).Replace("   ", " ").Replace("  ", " ").Replace("'", "''").Trim(trmChr) + @"',  
                           cntct_no_fax='" + cntcFax.Replace("\r\n", "").Replace("'", "''") + @"' WHERE person_id=" + prsnID;
        Global.updateDataNoParams(updtSQL);
        if ((i % 50) == 0)
        {
          Program.updatePrgrm(prgmID);
        }
      }

      //this.saveLabel.Visible = false;
      //System.Windows.Forms.Application.DoEvents();
    }

    public void makeSMSRestCall(string msgBody, string rcpntNo)
    {
      var client = new RestClient();
      client.EndPoint = @"http://62.129.149.58:5005/http_access.php";
      client.Method = HttpVerb.POST;
      client.PostData = "{postData: value}";
      string cmpny = "";
      string ccode = "";
      var json = client.MakeRequest("?company=" + cmpny + "&ccode=" + ccode +
        "&message=" + msgBody.Replace("&", " and ").Replace("  ", " ") + "&recipient=" + rcpntNo);
    }

    private void exprtPrsnDetForm(long prsnID)
    {
      //this.cancelButton.Text = "Cancel";
      //this.progressLabel.Text = "Exporting Report to Word Document...---0% Complete";
      //System.Windows.Forms.Application.DoEvents();
      object oMissing = System.Reflection.Missing.Value;
      object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

      Microsoft.Office.Interop.Word.Application oWord = new Microsoft.Office.Interop.Word.Application();
      oWord.Visible = true;
      oWord.Activate();
      oWord.ShowMe();
      object lnkToFile = false;
      object saveWithDoc = true;
      object oFalse = false;
      object oTrue = true;
      string selSql = "SELECT '''' || local_id_no, " +
              "title, first_name, sur_name, other_names, " +
              "gender, marital_status, to_char(to_timestamp(date_of_birth,'YYYY-MM-DD'),'DD-Mon-YYYY'), place_of_birth, " +
              "res_address, pstl_addrs, email, '''' || cntct_no_tel, '''' || cntct_no_mobl, " +
              "'''' || cntct_no_fax, img_location " +
              "FROM prs.prsn_names_nos WHERE person_id = " + prsnID;
      DataSet dtSt = Global.selectDataNoParams(selSql);
      int j = dtSt.Tables[0].Rows.Count;

      Microsoft.Office.Interop.Word.Document oDoc = oWord.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oMissing);

      Microsoft.Office.Interop.Word.Paragraph oParaB;
      Microsoft.Office.Interop.Word.Paragraph oParaH;
      Microsoft.Office.Interop.Word.Paragraph oPara0;
      Microsoft.Office.Interop.Word.Paragraph oPara1;

      //EMBEDDING LOGOS IN THE DOCUMENT

      //SETTING FOCUES ON THE PAGE HEADER TO EMBED THE WATERMARK

      oWord.ActiveWindow.ActivePane.View.SeekView = Word.WdSeekView.wdSeekCurrentPageHeader;
      //THE LOGO IS ASSIGNED TO A SHAPE OBJECT SO THAT WE CAN USE ALL THE
      //SHAPE FORMATTING OPTIONS PRESENT FOR THE SHAPE OBJECT
      Word.Shape logoCustom = null;
      Word.Range logoName = null;
      Word.Shape logoLine = null;
      //THE PATH OF THE LOGO FILE TO BE EMBEDDED IN THE HEADER
      String logoPath = Global.getOrgImgsDrctry() + @"\" + Global.UsrsOrg_ID + ".png";
      //if (!Global.myComputer.FileSystem.FileExists(logoPath))
      //{
      //  logoPath = Application.StartupPath + @"\logo.png";
      //}
      logoName = oWord.Selection.HeaderFooter.Range;//oWord.Selection.HeaderFooter.Shapes.AddTextbox(Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal, 120, 163, 248, 25, ref oMissing);
      //oParaI = logoName.Paragraphs.Add(ref oMissing);

      //oParaI.Range.InsertParagraphAfter();
      logoName.Paragraphs.Indent();
      logoName.Paragraphs.Indent();
      //logoName.Paragraphs.WordWrap = 1;
      oParaH = logoName.Paragraphs.Add(ref oMissing);
      oParaH.Range.Text = Global.getOrgName(Global.UsrsOrg_ID) +
            "                                                                                      " +
            "                                                                                      " +
        Global.getOrgPstlAddrs(Global.UsrsOrg_ID).Replace("\r\n",
        "                                                                                          " +
        "                                                                                          ")
    + "\r\nWeb:" + Global.getOrgWebsite(Global.UsrsOrg_ID)
          + "  Email:" + Global.getOrgEmailAddrs(Global.UsrsOrg_ID)
          + "  Tel:" + Global.getOrgContactNos(Global.UsrsOrg_ID);
      oParaH.Range.InsertParagraphAfter();


      logoCustom = oWord.Selection.HeaderFooter.Shapes.AddPicture(logoPath, ref oFalse, ref oTrue, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
      logoCustom.Select(ref oMissing);
      logoCustom.Name = "customLogo";
      //logoCustom.Left = (float)Word.WdShapePosition.wdShapeLeft;
      logoCustom.Top = 0;
      logoCustom.Left = 0;
      logoCustom.Height = 50;
      logoCustom.Width = 50;

      logoLine = oWord.Selection.HeaderFooter.Shapes.AddLine(60, 53, 500, 53, ref oMissing);
      logoLine.Select(ref oMissing);
      logoLine.Name = "CompanyLine";
      logoLine.TopRelative = 8;
      logoLine.Line.Weight = 2;
      logoLine.Width = 550;
      //logoName.Line.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;

      oWord.ActiveWindow.ActivePane.View.SeekView = Word.WdSeekView.wdSeekCurrentPageFooter;
      //THE LOGO IS ASSIGNED TO A SHAPE OBJECT SO THAT WE CAN USE ALL THE
      //SHAPE FORMATTING OPTIONS PRESENT FOR THE SHAPE OBJECT
      Word.Shape bottomLine = null;
      Word.Shape bottomText = null;

      //oParaB = logoName.Paragraphs.Add(ref oMissing);
      bottomText = oWord.Selection.HeaderFooter.Shapes.AddLabel(
        Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal,
        60, 400, 450, 25, ref oMissing);
      bottomText.Select(ref oMissing);
      bottomText.Name = "bottomName";
      bottomText.Left = (float)Word.WdShapePosition.wdShapeRight;
      bottomText.TopRelative = 108;
      bottomText.Height = 25;
      bottomText.Width = 450;
      bottomText.Line.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
      bottomText.TextFrame.TextRange.Text = Global.getOrgSlogan(Global.UsrsOrg_ID);
      //oParaB.Range.Text = Global.getOrgSlogan(Global.UsrsOrg_ID);
      //oParaB.Range.InsertParagraphAfter();

      bottomLine = oWord.Selection.HeaderFooter.Shapes.AddLine(60, 390, 500, 390, ref oMissing);
      bottomLine.Select(ref oMissing);
      bottomLine.Name = "bottomLine";
      bottomLine.TopRelative = 107;
      bottomLine.Line.Weight = 1;
      bottomLine.Width = 550;
      //oWord.Selection.HeaderFooter.PageNumbers.Add(ref oMissing, ref oMissing).Alignment = Microsoft.Office.Interop.Word.WdPageNumberAlignment.wdAlignPageNumberRight;


      oWord.ActiveWindow.ActivePane.View.SeekView = Word.WdSeekView.wdSeekMainDocument;
      oDoc.PageSetup.Orientation = Microsoft.Office.Interop.Word.WdOrientation.wdOrientPortrait;
      oPara0 = oDoc.Paragraphs.Add(ref oMissing);
      oPara0.Format.SpaceAfter = 1;
      oPara0.Range.Font.Bold = 1;
      oPara0.Range.Font.Name = "Times New Roman";
      oPara0.Range.Font.Size = 12;
      oPara0.Range.Text = "PERSON DETAILS FORM\r\n";
      String prsnImgPath = Global.getPrsnImgsDrctry() + @"\" + prsnID + ".png";
      if (!System.IO.File.Exists(prsnImgPath))
      {
        prsnImgPath = Global.rnnrsBasDir + @"\staffs.png";
      }
      Word.InlineShape picShape = oPara0.Range.InlineShapes.AddPicture(
        prsnImgPath, ref oFalse, ref oTrue, ref oMissing);
      picShape.Width = (float)((picShape.Width / picShape.Height) * 100);
      picShape.Height = (float)(100);
      picShape.Borders.Enable = 1;
      picShape.Borders.OutsideColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkBlue;
      oPara0.Range.InsertParagraphAfter();


      if (j <= 0)
      {
        //this.progressLabel.Text = "Exporting Report to Word Document---....100% Complete";
        //this.progressBar1.Value = 100;
        //this.cancelButton.Text = "Finish";
        return;
      }

      oPara1 = oDoc.Paragraphs.Add(ref oMissing);
      oPara1.Format.SpaceAfter = 1;
      oPara1.Range.Font.Bold = 1;
      oPara1.Range.Font.Name = "Times New Roman";
      oPara1.Range.Font.Size = 12;
      oPara1.Range.Text = "";// Global.getPrsnName(prsnID);
      oPara1.Range.InsertParagraphAfter();

      Word.Table oTable4;
      Word.Range wrdRng4 = oPara1.Range;//oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;

      oTable4 = oDoc.Tables.Add(wrdRng4, 8, 4, ref oMissing, ref oMissing);
      oTable4.Range.ParagraphFormat.SpaceAfter = 1;
      oTable4.Columns[1].Width = 85;
      oTable4.Columns[2].Width = 150;
      oTable4.Columns[3].Width = 70;
      oTable4.Columns[4].Width = 170;

      oTable4.Rows[1].Range.Font.Name = "Times New Roman";
      oTable4.Rows[1].Range.Font.Size = 11;
      oTable4.Rows[2].Range.Font.Name = "Times New Roman";
      oTable4.Rows[2].Range.Font.Size = 11;
      oTable4.Rows[3].Range.Font.Name = "Times New Roman";
      oTable4.Rows[3].Range.Font.Size = 11;
      oTable4.Rows[4].Range.Font.Name = "Times New Roman";
      oTable4.Rows[4].Range.Font.Size = 11;
      oTable4.Rows[5].Range.Font.Name = "Times New Roman";
      oTable4.Rows[5].Range.Font.Size = 11;
      oTable4.Rows[6].Range.Font.Name = "Times New Roman";
      oTable4.Rows[6].Range.Font.Size = 11;
      oTable4.Rows[7].Range.Font.Name = "Times New Roman";
      oTable4.Rows[7].Range.Font.Size = 11;
      oTable4.Rows[8].Range.Font.Name = "Times New Roman";
      oTable4.Rows[8].Range.Font.Size = 11;

      //oTable4.Rows[1].Alignment = Microsoft.Office.Interop.Word.WdRowAlignment.wdAlignRowCenter;
      oTable4.Rows.Alignment = Microsoft.Office.Interop.Word.WdRowAlignment.wdAlignRowCenter;

      oTable4.Cell(1, 1).Range.Text = "ID No.:";
      oTable4.Cell(1, 1).Range.Font.Bold = 1;
      oTable4.Cell(1, 2).Range.Text = dtSt.Tables[0].Rows[0][0].ToString();

      oTable4.Cell(2, 1).Range.Text = "Title:";
      oTable4.Cell(2, 1).Range.Font.Bold = 1;
      oTable4.Cell(2, 2).Range.Text = dtSt.Tables[0].Rows[0][1].ToString();

      oTable4.Cell(3, 1).Range.Text = "First Name:";
      oTable4.Cell(3, 1).Range.Font.Bold = 1;
      oTable4.Cell(3, 2).Range.Text = dtSt.Tables[0].Rows[0][2].ToString();

      oTable4.Cell(4, 1).Range.Text = "Surname:";
      oTable4.Cell(4, 1).Range.Font.Bold = 1;
      oTable4.Cell(4, 2).Range.Text = dtSt.Tables[0].Rows[0][3].ToString();

      oTable4.Cell(5, 1).Range.Text = "Other Names:";
      oTable4.Cell(5, 1).Range.Font.Bold = 1;
      oTable4.Cell(5, 2).Range.Text = dtSt.Tables[0].Rows[0][4].ToString();

      oTable4.Cell(6, 1).Range.Text = "Gender:";
      oTable4.Cell(6, 1).Range.Font.Bold = 1;
      oTable4.Cell(6, 2).Range.Text = dtSt.Tables[0].Rows[0][5].ToString();

      oTable4.Cell(7, 1).Range.Text = "Marital Status:";
      oTable4.Cell(7, 1).Range.Font.Bold = 1;
      oTable4.Cell(7, 2).Range.Text = dtSt.Tables[0].Rows[0][6].ToString();

      oTable4.Cell(8, 1).Range.Text = "Date of Birth:";
      oTable4.Cell(8, 1).Range.Font.Bold = 1;
      oTable4.Cell(8, 2).Range.Text = dtSt.Tables[0].Rows[0][7].ToString();

      oTable4.Cell(1, 3).Range.Text = "Place of Birth:";
      oTable4.Cell(1, 3).Range.Font.Bold = 1;
      oTable4.Cell(1, 4).Range.Text = dtSt.Tables[0].Rows[0][8].ToString();

      oTable4.Cell(2, 3).Range.Text = "Residential Address:";
      oTable4.Cell(2, 3).Range.Font.Bold = 1;
      oTable4.Cell(2, 4).Range.Text = dtSt.Tables[0].Rows[0][9].ToString();

      oTable4.Cell(3, 3).Range.Text = "Postal Address:";
      oTable4.Cell(3, 3).Range.Font.Bold = 1;
      oTable4.Cell(3, 4).Range.Text = dtSt.Tables[0].Rows[0][10].ToString();

      oTable4.Cell(4, 3).Range.Text = "Email:";
      oTable4.Cell(4, 3).Range.Font.Bold = 1;
      oTable4.Cell(4, 4).Range.Text = dtSt.Tables[0].Rows[0][11].ToString();

      oTable4.Cell(5, 3).Range.Text = "Tel:";
      oTable4.Cell(5, 3).Range.Font.Bold = 1;
      oTable4.Cell(5, 4).Range.Text = dtSt.Tables[0].Rows[0][12].ToString();

      oTable4.Cell(6, 3).Range.Text = "Mob:";
      oTable4.Cell(6, 3).Range.Font.Bold = 1;
      oTable4.Cell(6, 4).Range.Text = dtSt.Tables[0].Rows[0][13].ToString();

      oTable4.Cell(7, 3).Range.Text = "Fax:";
      oTable4.Cell(7, 3).Range.Font.Bold = 1;
      oTable4.Cell(7, 4).Range.Text = dtSt.Tables[0].Rows[0][14].ToString();

      oTable4.Cell(8, 3).Range.Text = "Person Type:";
      oTable4.Cell(8, 3).Range.Font.Bold = 1;
      oTable4.Cell(8, 4).Range.Text = "";// Global.getLatestPrsnType(prsnID);

      oTable4.Cell(1, 1).VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
      oTable4.Cell(1, 2).VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
      oTable4.Cell(1, 3).VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
      oTable4.Cell(1, 4).VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

      oTable4.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone;
      oTable4.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;

      //this.progressLabel.Text = "Exporting Report to Word Document---....100% Complete";
      //this.progressBar1.Value = 100;
      //this.cancelButton.Text = "Finish";
    }

  }
}
