using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading;
using Npgsql;
using System.Data;
using System.Drawing;

namespace REMSProcessRunner
{
    class Program
    {
        static Thread threadOne = null;   //Updates Process Runner Status
        static Thread threadTwo = null;   //Thread for Generating Run Requests for Scheduled Programs/Reports
        static Thread threadThree = null;
        //Thread for Monitoring Scheduled Request Runs that are due but not running
        // and starting their appropriate process runners
        static Thread threadFour = null;  //Thread for Monitoring User Request Runs that are due but not running
                                          // and starting their appropriate process runners
        static Thread threadFive = null;  //Thread for running the actual Code behind the Request Run if this is the
        static Thread threadSix = null;  //Thread for Generating Run Requests for Alerts
        static Thread threadSeven = null;  //Thread for Monitoring Alert Request Runs that are due but not running
                                           // and starting their appropriate process runners
        static Thread threadEight = null;
        static Thread threadNine = null;
        static Thread threadTen = null;

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
                    string[] macDet = Global.getMachDetails();
                    Global.errorLog += "\r\n" + "PID: " + Global.pid + " Running on: " + macDet[0] + " / " + macDet[1] + " / " + macDet[2];
                    Global.runID = long.Parse(args[6]);
                    do_connection(args[0], args[1], args[2], args[3], args[4]);
                    Global.appStatPath = Global.rnnrsBasDir;
                    if (Global.runID > 0)
                    {
                        Global.rnUser_ID = long.Parse(Global.getGnrlRecNm("rpt.rpt_report_runs", "rpt_run_id", "run_by", Global.runID));
                        Global.UsrsOrg_ID = Global.getUsrOrgID(Global.rnUser_ID);
                    }
                    Global.writeToLog();

                    if (Global.globalSQLConn.State == ConnectionState.Open)
                    {
                        Global.globalSQLConn.Close();
                        bool isLstnrRnng = false;
                        if (Program.runnerName == "REQUESTS LISTENER PROGRAM")
                        {
                            int isIPAllwd = Global.getEnbldPssblValID(macDet[2],
                      Global.getEnbldLovID("Allowed IP Address for Request Listener"));
                            int isDBAllwd = Global.getEnbldPssblValID(Global.Dbase,
                       Global.getEnbldLovID("Allowed DB Name for Request Listener"));
                            Global.errorLog = macDet[2] + "/" + isIPAllwd + "/" + Global.Dbase + "/" + isDBAllwd;
                            Global.writeToLog();
                            if (isIPAllwd <= 0 || isDBAllwd <= 0)
                            {
                                Program.killThreads();
                                Thread.CurrentThread.Abort();
                                //Program.killThreads();
                                return;
                            }

                            isLstnrRnng = Global.isRunnrRnng(Program.runnerName);
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

                        if (isLstnrRnng == false && runnerName == "REQUESTS LISTENER PROGRAM")
                        {
                            Global.updatePrcsRnnrCmd(runnerName, "0", -1);

                            ThreadStart startDelegate1 = new ThreadStart(rqstLstnrUpdtrfunc);
                            threadOne = new Thread(startDelegate1);//Updates Process Runner Status
                            threadOne.Name = "ThreadOne";
                            threadOne.Priority = ThreadPriority.Lowest;

                            threadOne.Start();
                            Global.minimizeMemory();
                            if (runnerName == "REQUESTS LISTENER PROGRAM")
                            {
                                //Thread for Generating Run Requests for Scheduled Programs/Reports
                                ThreadStart startDelegate2 = new ThreadStart(gnrtSchldRnsfunc);
                                threadTwo = new Thread(startDelegate2);//() => gnrtSchldRnsfunc(-1, -1)
                                                                       //if Process Runner is Process Runner Launcher then Generate Scheduled Runs
                                threadTwo.Name = "ThreadTwo";
                                threadTwo.Priority = ThreadPriority.Lowest;
                                threadTwo.Start();

                                //Thread for Generating Run Requests for Scheduled Alerts
                                ThreadStart startDelegate6 = new ThreadStart(gnrtSchldAlertsfunc);
                                threadSix = new Thread(startDelegate6);//() => gnrtSchldRnsfunc(-1, -1)
                                                                       //if Process Runner is Process Runner Launcher then Generate Scheduled Runs
                                threadSix.Name = "ThreadSix";
                                threadSix.Priority = ThreadPriority.Lowest;
                                threadSix.Start();

                                //Thread for Monitoring Scheduled Request Runs that are due but not running
                                // and starting their appropriate process runners
                                ThreadStart startDelegate3 = new ThreadStart(mntrSchdldRqtsNtRnngfunc);
                                threadThree = new Thread(startDelegate3);
                                threadThree.Name = "ThreadThree";
                                threadThree.Priority = ThreadPriority.Lowest;
                                threadThree.Start();

                                //Thread for Monitoring User Request Runs that are due but not running
                                // and starting their appropriate process runners
                                ThreadStart startDelegate4 = new ThreadStart(mntrUsrInitRqtsNtRnngfunc);
                                threadFour = new Thread(startDelegate4);
                                threadFour.Name = "ThreadFour";
                                threadFour.Priority = ThreadPriority.Lowest;
                                threadFour.Start();

                                //Thread for Generating Run Requests for Scheduled Alerts
                                ThreadStart startDelegate7 = new ThreadStart(mntrSchdldAlertsNtRnngfunc);
                                threadSeven = new Thread(startDelegate7);
                                //if Process Runner is Process Runner Launcher then Generate Scheduled Runs
                                threadSeven.Name = "ThreadSeven";
                                threadSeven.Priority = ThreadPriority.Lowest;
                                threadSeven.Start();

                                //Thread for Running Requests for User Initiated Alerts
                                ThreadStart startDelegate8 = new ThreadStart(mntrUserAlertsNtRnngfunc);
                                threadEight = new Thread(startDelegate8);
                                //if Process Runner is Process Runner Launcher then Generate Scheduled Runs
                                threadEight.Name = "ThreadSeven";
                                threadEight.Priority = ThreadPriority.Lowest;
                                threadEight.Start();

                            }
                        }
                        else
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
                Global.errorLog += ex.Message + "\r\n\r\n" + ex.StackTrace + "\r\n\r\n" + ex.InnerException + "\r\n\r\n";
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
                Thread.Sleep(5000);
                Global.updatePrcsRnnr(prgmID, dtestr, "PID: " + Global.pid + " Running on: " + macDet[0] + " / " + macDet[1] + " / " + macDet[2]);
                if (Global.runID > 0)
                {
                    Global.updateRptRnActvTme(Global.runID, dtestr);
                }
            }
        }

        static void runActualRqtsfunc()
        {
            string dateStr = Global.getDB_Date_time();
            //string dateStr = Global.getDB_Date_time();
            string log_tbl = "rpt.rpt_run_msgs";
            try
            {
                long prgmID = Global.getGnrlRecID("rpt.rpt_prcss_rnnrs", "rnnr_name", "prcss_rnnr_id", runnerName);
                Global.errorLog = "Successfully Started Thread Five\r\nProgram ID:" + prgmID + ": Program Name: " + runnerName + "\r\n";
                string[] macDet = Global.getMachDetails();
                Global.errorLog += "\r\n" + "PID: " + Global.pid + " Running on: " + macDet[0] + " / " + macDet[1] + " / " + macDet[2];
                Global.writeToLog();

                string rptTitle = "";
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
                    Global.errorLog += "\r\nRun ID: " + Global.runID + " Report ID:" + locRptID + "\r\n";
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

                    Global.ovrllDataCnt = 0;
                    Global.strSB = new StringBuilder("");
                    //Program.updatePrgrm(prgmID);
                    for (int q = 0; q < prgUntsCnt + 1; q++)
                    {
                        bool isfirst = true;
                        bool islast = true;
                        bool shdAppnd = false;
                        string rqrdParamVal = "";
                        string exclFileName = "";
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
                                if (paramSqlRep == "{:alert_type}" && rptType.Contains("Alert"))
                                {
                                    //alertType = arry2[i];
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
                                else if (paramSqlRep == "{:excl_file_name}")
                                {
                                    exclFileName = arry2[i];
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
                            Global.executeGnrlSQL(rpt_SQL.Replace("\r\n", " ").Replace("\n", " ").Replace("\r", " "));
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
                        else if (rptType == "Import/Overwrite Data from Excel"
                          && exclFileName != "")
                        {
                            //Check if  {:alert_type} EMAIL/SMS parameter was set
                            //NB sql first column is address and 2nd col is message body
                            Global.imprtTrnsTmp(exclFileName, rpt_SQL.Replace("\r\n", " ").Replace("\n", " ").Replace("\r", " "));
                            rpt_SQL = rpt_SQL.Replace("{:orgnValColA}", "");
                        }
                        else
                        {
                            dtst = Global.selectDataNoParams(rpt_SQL.Replace("\r\n", " ").Replace("\n", " ").Replace("\r", " "));
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
                                          bccMails.Replace(",", ";").Trim(seps1), attchMns.Replace(",", ";").Trim(seps1), sbjct, msgBdy, nwMsgSntID.ToString()+"Alrt", ref errMsg) == false)
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
                        else if (rptType == "Posting of GL Trns. Batches")
                        {
                            //NB sql col0=batch_id, col1=batch_name, col2=batch_source, col3=batch_status, col4=batch_status_meaning
                            //i.e SQL Must Contain accb.accb_trnsctn_batches and all the colnames above
                            //
                            DataSet wrngDtst = Global.get_WrongBalncs(Global.UsrsOrg_ID);
                            if (wrngDtst.Tables[0].Rows.Count > 0)
                            {
                                Global.updateLogMsg(msg_id,
                    "\r\n\r\nCannot Post this Batch Since Some Accounts have wrong Balances!" +
                    "\r\nPlease correct the Imbalance First!!\r\nUser Org ID=" + Global.UsrsOrg_ID +
                    "\r\nNumber of Records Involved=" + wrngDtst.Tables[0].Rows.Count + "\r\n\r\n",
                    log_tbl, dateStr, Global.rnUser_ID);

                                Program.correctImblns();

                                Global.updateRptRnStopCmd(Global.runID, "1");
                                Program.checkNClosePrgrm();
                                return;
                            }
                            else
                            {
                                //Check if no other accounting process is running
                                bool isAnyRnng = true;
                                int witcntr = 0;
                                do
                                {
                                    witcntr++;
                                    isAnyRnng = Global.isThereANActvActnPrcss("1,2,3,4,5,6", "10 second");
                                    if (witcntr > 8)
                                    {
                                        Global.updateRptRnStopCmd(Global.runID, "1");
                                    }
                                    Program.checkNClosePrgrm();
                                    Thread.Sleep(5000);
                                }
                                while (isAnyRnng == true);

                                for (int rh = 0; rh < dtst.Tables[0].Rows.Count; rh++)
                                {
                                    Global.updtActnPrcss(5);
                                    Program.validateBatchNPost(long.Parse(dtst.Tables[0].Rows[rh][0].ToString()),
                                      dtst.Tables[0].Rows[rh][3].ToString(), dtst.Tables[0].Rows[rh][2].ToString(),
                                      msg_id, log_tbl, dateStr);
                                    Thread.Sleep(200);
                                }
                            }
                        }
                        else if (rptType == "Journal Import")
                        {
                            //check if {:intrfc_tbl_name} parameter was set
                            /*NB sql col0=accnt_id, col1=trnsctn_date(DD-Mon-YYYY HH24:MI:SS), 
                             * col2=dbt_amount, col3=crdt_amount, col4=net_amount, col5=func_cur_id*/
                            //
                            string errmsg = "";

                            int prcID = 8;//Internal Payments Import Process
                            if (rqrdParamVal == "scm.scm_gl_interface")
                            {
                                prcID = 7;
                            }
                            bool isAnyRnng = true;
                            int witcntr = 0;
                            do
                            {
                                witcntr++;
                                isAnyRnng = Global.isThereANActvActnPrcss(prcID.ToString(), "10 second");
                                if (witcntr > 8)
                                {
                                    Global.updateRptRnStopCmd(Global.runID, "1");
                                    Program.killThreads();
                                }
                                Program.updatePrgrm(prgmID);
                                Thread.Sleep(5000);
                            }
                            while (isAnyRnng == true);

                            Global.updtActnPrcss(prcID);

                            if (Program.sendJournalsToGL(dtst, rqrdParamVal, prcID, ref errmsg))
                            {
                                Global.updateLogMsg(msg_id,
                                  "\r\n\r\nJournals Successfully Sent to GL!\r\n" + errmsg, log_tbl, dateStr, Global.rnUser_ID);
                            }
                            else
                            {
                                Global.updateLogMsg(msg_id,
                                  "\r\n\r\nFailed to send Journals to GL!\r\n" + errmsg, log_tbl, dateStr, Global.rnUser_ID);
                            }
                        }
                        else if (rpt_id == Global.getRptID("Send Outstanding Bulk Messages")
                            || rpt_id == Global.getRptID("Send Outstanding Bulk Messages-Scheduled"))
                        {
                            string lastTimeChckd = Global.getDB_Date_time();
                            int lstChckCnt = 0;
                            int row_cntr = 0;
                            errMsg = "";
                            bool tmeUp = false;
                            Char[] seprs = { ';' };
                            do
                            {
                                dateStr = lastTimeChckd;
                                if (lstChckCnt > 0)
                                {
                                    dtst = Global.selectDataNoParams(rpt_SQL.Replace("\r\n", " ").Replace("\n", " ").Replace("\r", " "));
                                }
                                row_cntr = dtst.Tables[0].Rows.Count;
                                for (int v = 0; v < row_cntr; v++)
                                {
                                    errMsg = "";
                                    attchMns = dtst.Tables[0].Rows[v][12].ToString().Replace(",", ";");
                                    if (attchMns != "")
                                    {
                                        string[] atchs = attchMns.Split(seprs, StringSplitOptions.RemoveEmptyEntries);
                                        for (int q1 = 0; q1 < atchs.Length; q1++)
                                        {
                                            string fullLocFileUrl = Global.getRptDrctry() + @"\mail_attachments\" + atchs[q1];
                                            if (System.IO.File.Exists(fullLocFileUrl) == true)
                                            {
                                                if (System.IO.File.GetCreationTime(fullLocFileUrl) >= DateTime.Now.AddHours(-1))
                                                {
                                                    Global.updateLogMsg(msg_id, "\r\n\r\nFile: " + (fullLocFileUrl).Replace(";", ",") + " Exists hence won't be downloaded again!\r\n" + errMsg, log_tbl, dateStr, Global.rnUser_ID);
                                                }
                                                else
                                                {
                                                    Global.dwnldImgsFTP(9, Global.getRptDrctry() + @"\mail_attachments\", atchs[q1]);
                                                }
                                            }
                                            else
                                            {
                                                Global.dwnldImgsFTP(9, Global.getRptDrctry() + @"\mail_attachments\", atchs[q1]);
                                            }
                                            atchs[q1] = Global.getRptDrctry() + @"\mail_attachments\" + atchs[q1];
                                        }
                                        attchMns = string.Join(";", atchs);
                                    }
                                    string msgTyp = dtst.Tables[0].Rows[v][13].ToString();
                                    toMails = dtst.Tables[0].Rows[v][2].ToString();
                                    ccMails = dtst.Tables[0].Rows[v][3].ToString();
                                    bccMails = dtst.Tables[0].Rows[v][7].ToString();
                                    sbjct = dtst.Tables[0].Rows[v][6].ToString();
                                    msgBdy = dtst.Tables[0].Rows[v][4].ToString();
                                    nwMsgSntID = long.Parse(dtst.Tables[0].Rows[v][0].ToString());
                                    if (msgTyp == "Email")
                                    {
                                        if (Global.sendEmail(toMails.Replace(";", ",").Trim(seps1), ccMails.Replace(",", ";").Trim(seps1),
                                          bccMails.Replace(",", ";").Trim(seps1), attchMns.Replace(",", ";").Trim(seps1), sbjct, msgBdy, nwMsgSntID.ToString() + "Bulk", ref errMsg) == false)
                                        {
                                            Global.updateBulkMsgSent(nwMsgSntID, dateStr, "0", errMsg);
                                            Global.updateLogMsg(msg_id, "\r\n\r\nMessage to " + (toMails + ";" + ccMails + ";" + bccMails).Replace(";", ",") + " Failed!\r\n" + errMsg, log_tbl, dateStr, Global.rnUser_ID);
                                        }
                                        else
                                        {
                                            Global.updateBulkMsgSent(nwMsgSntID, dateStr, "1", "");
                                            Global.updateLogMsg(msg_id,
                                                    "\r\n\r\nMessage to " + (toMails + ";" + ccMails + ";" + bccMails).Replace(";", ",") + " Successfully Sent!\r\n", log_tbl, dateStr, Global.rnUser_ID);
                                        }
                                    }
                                    else if (msgTyp == "SMS")
                                    {
                                        if (Global.sendSMS(msgBdy, (toMails + ";" + ccMails + ";" + bccMails).Replace(";", ",").Trim(seps1), ref errMsg) == false)
                                        {
                                            Global.updateBulkMsgSent(nwMsgSntID, dateStr, "0", errMsg);
                                            Global.updateLogMsg(msg_id, "\r\n\r\nMessage to " + (toMails + ";" + ccMails + ";" + bccMails).Replace(";", ",") + " Failed!\r\n" + errMsg, log_tbl, dateStr, Global.rnUser_ID);
                                        }
                                        else
                                        {
                                            Global.updateBulkMsgSent(nwMsgSntID, dateStr, "1", "");
                                            Global.updateLogMsg(msg_id, "\r\n\r\nMessage to " + (toMails + ";" + ccMails + ";" + bccMails).Replace(";", ",") + " Successfully Sent!\r\n", log_tbl, dateStr, Global.rnUser_ID);
                                        }
                                    }
                                    else
                                    {

                                    }
                                    if (v == (row_cntr - 1))
                                    {
                                        lastTimeChckd = Global.getDB_Date_time();
                                    }
                                    Thread.Sleep(25);
                                    Global.errorLog = "\r\nMessages to " + (toMails + ";" + ccMails + ";" + bccMails) + " worked on";
                                    Global.writeToLog();
                                }
                                lstChckCnt++;
                                Thread.Sleep(5000);
                                tmeUp = Global.doesDteTmExcdIntvl("30 second", lastTimeChckd);
                            } while (tmeUp == false);
                            Global.updateLogMsg(msg_id,
                                    "\r\n\r\nFinished Sending all Messages!\r\n", log_tbl, dateStr, Global.rnUser_ID);
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
                            if (outputUsd == "MICROSOFT EXCEL" || outputUsd == "PDF")
                            {
                                if (outputUsd == "MICROSOFT EXCEL")
                                {
                                    Global.exprtDtStSaved(dtst,
                                      Global.getRptDrctry() + @"\" + rpt_run_id.ToString() + ".xls",
                                    rptTitle, colsToGrp, colsToCnt, colsToSum, colsToAvrg, colsToFrmt
                                      , isfirst, islast, shdAppnd, orntnUsd);
                                    uptFileUrl = Global.getRptDrctry() + @"\" + rpt_run_id.ToString() + ".xls";
                                }
                                else
                                {
                                    Global.exprtDtStSaved(dtst,
                        Global.getRptDrctry() + @"\" + rpt_run_id.ToString() + ".pdf",
                      rptTitle, colsToGrp, colsToCnt, colsToSum, colsToAvrg, colsToFrmt
                        , isfirst, islast, shdAppnd, orntnUsd);
                                    uptFileUrl = Global.getRptDrctry() + @"\" + rpt_run_id.ToString() + ".pdf";
                                }
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
                                    Global.exprtPDFTblr(dtst,
                    Global.getRptDrctry() + @"\" + rpt_run_id.ToString() + ".pdf"
                  , colsToGrp, colsToCnt, colsToSum, colsToAvrg, colsToFrmt
                        , isfirst, islast, shdAppnd, rptTitle, orntnUsd);
                                }
                                else if (rptLyout == "DETAIL")
                                {
                                    //Show detail PDF Report
                                    DataSet grpngsDtSt = Global.get_AllGrpngs(rpt_id);
                                    Global.exprtToPDFDet(dtst, grpngsDtSt,
                    Global.getRptDrctry() + @"\" + rpt_run_id.ToString() + ".pdf",
                    rptTitle, isfirst, islast, shdAppnd, orntnUsd, imgCols);
                                }
                                uptFileUrl = Global.getRptDrctry() + @"\" + rpt_run_id.ToString() + ".pdf";
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
                                uptFileUrl = Global.getRptDrctry() + "/" + rpt_run_id.ToString() + ".csv";
                            }

                            Global.updateRptRn(rpt_run_id, "Storing Output...", 80);
                            //worker.ReportProgress(80);
                            Global.updateLogMsg(msg_id,
                  "\r\n\r\nSaving Report Output...", log_tbl, dateStr, Global.rnUser_ID);
                            Global.updateRptRnOutpt(rpt_run_id, rptOutpt);
                            Global.updateLogMsg(msg_id,
                  "\r\n\r\nSuccessfully Saved Report Output...", log_tbl, dateStr, Global.rnUser_ID);
                            if (System.IO.File.Exists(uptFileUrl))
                            {
                                Global.upldImgsFTP(9, System.IO.Path.GetDirectoryName(uptFileUrl), System.IO.Path.GetFileName(uptFileUrl));
                            }
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
                                                       sbjct, msgBdy, nwMsgSntID.ToString() + "Alrt", ref errMsg) == false)
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
                if (threadEight.IsAlive)
                {
                    threadEight.Abort();
                }
                if (threadSeven.IsAlive)
                {
                    threadSeven.Abort();
                }
                if (threadSix.IsAlive)
                {
                    threadSix.Abort();
                }
                if (threadFive.IsAlive)
                {
                    threadFive.Abort();
                }
                if (threadFour.IsAlive)
                {
                    threadFour.Abort();
                }
                if (threadThree.IsAlive)
                {
                    threadThree.Abort();
                }
                if (threadTwo.IsAlive)
                {
                    threadTwo.Abort();
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
                if (threadEight.IsAlive)
                {
                    threadEight.Abort();
                }
                if (threadSeven.IsAlive)
                {
                    threadSeven.Abort();
                }
                if (threadSix.IsAlive)
                {
                    threadSix.Abort();
                }
                if (threadFive.IsAlive)
                {
                    threadFive.Abort();
                }
                if (threadFour.IsAlive)
                {
                    threadFour.Abort();
                }
                if (threadThree.IsAlive)
                {
                    threadThree.Abort();
                }
                if (threadTwo.IsAlive)
                {
                    threadTwo.Abort();
                }
                if (Thread.CurrentThread.IsAlive)
                {
                    Thread.CurrentThread.Abort();
                }
            }
        }

        static void mntrUsrInitRqtsNtRnngfunc()
        {
            try
            {
                do
                {
                    //Get all rquest runs not running
                    //Launch appropriate process runner

                    Program.checkNClosePrgrm();
                    DataSet dtst = Global.get_UsrRunsNtRnng();
                    for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
                    {
                        long rptid = long.Parse(dtst.Tables[0].Rows[i][1].ToString());
                        long rptrnid = long.Parse(dtst.Tables[0].Rows[i][0].ToString());
                        long rptrnnrid = long.Parse(dtst.Tables[0].Rows[i][2].ToString());
                        string rptRnnrNm = Global.getGnrlRecNm("rpt.rpt_reports", "report_id", "process_runner", rptid);
                        string rnnrPrcsFile = Global.getGnrlRecNm("rpt.rpt_prcss_rnnrs", "rnnr_name", "executbl_file_nm", rptRnnrNm);
                        if (rptRnnrNm == "")
                        {
                            rptRnnrNm = "Standard Process Runner";
                        }
                        if (rnnrPrcsFile == "")
                        {
                            rnnrPrcsFile = @"\bin\REMSProcessRunner.exe";
                        }
                        rnnrPrcsFile = rnnrPrcsFile.Replace("/bin", "").Replace("\\bin", "");

                        if (Global.doesLstRnTmExcdIntvl(rptid, "65 second", rptrnid) == true)
                        {
                            Global.updatePrcsRnnrCmd(rptRnnrNm, "0", rptrnnrid);
                            Global.updateRptRnStopCmd(rptrnid, "0");
                            string[] args = { "\"" + Global.Hostnme + "\"",
                          Global.Portnum,
                          "\"" + Global.Uname + "\"",
                          "\"" + Global.Pswd + "\"",
                          "\"" + Global.Dbase + "\"",
                          "\"" + rptRnnrNm + "\"",
                          (rptrnid).ToString(),
                          "\""+ System.IO.Path.GetDirectoryName(Global.appStatPath + "/" +rnnrPrcsFile) + "\"",
                          "DESKTOP",
                          "\""+ Global.dataBasDir + "\""};
                            //Global.showMsg(String.Join(" ", args), 0);
                            //Replace("/bin", "").Replace("\\bin", "")
                            if (rptRnnrNm.Contains("Jasper"))
                            {
                                //Global.mnFrm.cmCde.showSQLNoPermsn("C:\\Windows\\System32\\cmd.exe /C java -jar " + Application.StartupPath + rnnrPrcsFile + " " + String.Join(" ", args));
                                System.Diagnostics.Process jarPrcs = new System.Diagnostics.Process();
                                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                                startInfo.FileName = "cmd.exe";
                                startInfo.Arguments = "/C javaw -jar -Xms1024m -Xmx1024m \"" +
                                  Global.appStatPath + "/" + rnnrPrcsFile + "\" " + String.Join(" ", args);
                                jarPrcs.StartInfo = startInfo;
                                jarPrcs.Start();
                                //System.Diagnostics.Process.Start("javaw", "-jar -Xms1024m -Xmx1024m " + Global.appStatPath + "/" + rnnrPrcsFile + " " + String.Join(" ", args));
                            }
                            else
                            {
                                System.Diagnostics.Process.Start(Global.appStatPath + "/" + rnnrPrcsFile, String.Join(" ", args));
                            }
                            //System.Diagnostics.Process.Start(Global.appStatPath + "/" + rnnrPrcsFile, String.Join(" ", args));
                        }
                        long mxConns = 0;
                        long curCons = 0;
                        do
                        {
                            mxConns = Global.getMxAllwdDBConns();
                            curCons = Global.getCurDBConns();
                            Global.errorLog = "Inside Running of User Requests=> Current Connections: " + curCons + " Max Connections: " + mxConns;
                            Global.writeToLog();
                            Program.checkNClosePrgrm();
                            Thread.Sleep(10000);
                        }
                        while (curCons >= mxConns);
                    }
                    Thread.Sleep(30000);
                    long prgmID = Global.getGnrlRecID("rpt.rpt_prcss_rnnrs", "rnnr_name", "prcss_rnnr_id", runnerName);
                    Program.updatePrgrm(prgmID);

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
                if (threadFour.IsAlive)
                {
                    threadFour.Abort();
                }
            }
            finally
            {
            }
        }

        static void mntrSchdldRqtsNtRnngfunc()
        {
            try
            {
                do
                {
                    //Get all rquest runs not running
                    //Launch appropriate process runner
                    Program.checkNClosePrgrm();
                    DataSet dtst = Global.get_SchdldRunsNtRnng();
                    for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
                    {
                        long rptid = long.Parse(dtst.Tables[0].Rows[i][1].ToString());
                        long rptrnid = long.Parse(dtst.Tables[0].Rows[i][0].ToString());
                        long rptrnnrid = long.Parse(dtst.Tables[0].Rows[i][2].ToString());
                        string rptRnnrNm = Global.getGnrlRecNm("rpt.rpt_reports", "report_id", "process_runner", rptid);
                        string rnnrPrcsFile = Global.getGnrlRecNm("rpt.rpt_prcss_rnnrs", "rnnr_name", "executbl_file_nm", rptRnnrNm);
                        if (rptRnnrNm == "")
                        {
                            rptRnnrNm = "Standard Process Runner";
                        }
                        if (rnnrPrcsFile == "")
                        {
                            rnnrPrcsFile = @"\bin\REMSProcessRunner.exe";
                        }

                        rnnrPrcsFile = rnnrPrcsFile.Replace("/bin", "").Replace("\\bin", "");

                        if (Global.doesLstRnTmExcdIntvl(rptid, "65 second", rptrnid) == true)
                        {
                            Global.updatePrcsRnnrCmd(rptRnnrNm, "0", rptrnnrid);
                            Global.updateRptRnStopCmd(rptrnid, "0");
                            string[] args = { "\"" + Global.Hostnme + "\"",
                          Global.Portnum,
                          "\"" + Global.Uname + "\"",
                          "\"" + Global.Pswd + "\"",
                          "\"" + Global.Dbase + "\"",
                          "\"" + rptRnnrNm + "\"",
                          (rptrnid).ToString(),
                          "\""+ System.IO.Path.GetDirectoryName(Global.appStatPath + "/" +rnnrPrcsFile) + "\"",
                          "DESKTOP",
                          "\""+ Global.dataBasDir + "\""};
                            //Global.showMsg(String.Join(" ", args), 0);
                            //Replace("/bin", "").Replace("\\bin", "")
                            if (rptRnnrNm.Contains("Jasper"))
                            {
                                //Global.mnFrm.cmCde.showSQLNoPermsn("C:\\Windows\\System32\\cmd.exe /C java -jar " + Application.StartupPath + rnnrPrcsFile + " " + String.Join(" ", args));
                                System.Diagnostics.Process jarPrcs = new System.Diagnostics.Process();
                                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                                startInfo.FileName = "cmd.exe";
                                startInfo.Arguments = "/C javaw -jar -Xms1024m -Xmx1024m \"" +
                                  Global.appStatPath + "/" + rnnrPrcsFile + "\" " + String.Join(" ", args);
                                jarPrcs.StartInfo = startInfo;
                                jarPrcs.Start();
                                //System.Diagnostics.Process.Start("javaw", "-jar -Xms1024m -Xmx1024m " + Global.appStatPath + "/" + rnnrPrcsFile + " " + String.Join(" ", args));
                            }
                            else
                            {
                                System.Diagnostics.Process.Start(Global.appStatPath + "/" + rnnrPrcsFile, String.Join(" ", args));
                            }
                            //System.Diagnostics.Process.Start(Global.appStatPath + "/" + rnnrPrcsFile, String.Join(" ", args));
                        }
                        long mxConns = 0;
                        long curCons = 0;
                        do
                        {
                            mxConns = Global.getMxAllwdDBConns();
                            curCons = Global.getCurDBConns();
                            Global.errorLog = "Inside Running of Scheduled Requests=> Current Connections: " + curCons + " Max Connections: " + mxConns;
                            Global.writeToLog();
                            Program.checkNClosePrgrm();
                            Thread.Sleep(10000);
                        }
                        while (curCons >= mxConns);
                    }
                    Thread.Sleep(40000);
                    long prgmID = Global.getGnrlRecID("rpt.rpt_prcss_rnnrs", "rnnr_name", "prcss_rnnr_id", runnerName);
                    Program.updatePrgrm(prgmID);
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
                if (threadThree.IsAlive)
                {
                    threadThree.Abort();
                }
            }
            finally
            {
            }
        }

        static void mntrSchdldAlertsNtRnngfunc()
        {
            try
            {
                do
                {
                    //Get all rquest runs not running
                    //Launch appropriate process runner
                    Program.checkNClosePrgrm();
                    DataSet dtst = Global.get_SchdldAlertsNtRnng();
                    long mxConns = 0;
                    long curCons = 0;
                    mxConns = Global.getMxAllwdDBConns();
                    for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
                    {
                        long rptid = long.Parse(dtst.Tables[0].Rows[i][1].ToString());
                        long rptrnid = long.Parse(dtst.Tables[0].Rows[i][0].ToString());
                        long rptrnnrid = long.Parse(dtst.Tables[0].Rows[i][2].ToString());
                        string rptRnnrNm = Global.getGnrlRecNm("rpt.rpt_reports", "report_id", "process_runner", rptid);
                        string rnnrPrcsFile = Global.getGnrlRecNm("rpt.rpt_prcss_rnnrs", "rnnr_name", "executbl_file_nm", rptRnnrNm);
                        if (rptRnnrNm == "")
                        {
                            rptRnnrNm = "Standard Process Runner";
                        }
                        if (rnnrPrcsFile == "")
                        {
                            rnnrPrcsFile = @"\bin\REMSProcessRunner.exe";
                        }

                        rnnrPrcsFile = rnnrPrcsFile.Replace("/bin", "").Replace("\\bin", "");

                        if (Global.doesLstRnTmExcdIntvl(rptid, "1 second", rptrnid) == true)
                        {
                            Global.updatePrcsRnnrCmd(rptRnnrNm, "0", rptrnnrid);
                            Global.updateRptRnStopCmd(rptrnid, "0");
                            string[] args = { "\"" + Global.Hostnme + "\"",
                          Global.Portnum,
                          "\"" + Global.Uname + "\"",
                          "\"" + Global.Pswd + "\"",
                          "\"" + Global.Dbase + "\"",
                          "\"" + rptRnnrNm + "\"",
                          (rptrnid).ToString(),
                          "\""+ System.IO.Path.GetDirectoryName(Global.appStatPath + "/" +rnnrPrcsFile) + "\"",
                          "DESKTOP",
                          "\""+ Global.dataBasDir + "\""};
                            //Global.showMsg(String.Join(" ", args), 0);
                            //Replace("/bin", "").Replace("\\bin", "")
                            if (rptRnnrNm.Contains("Jasper"))
                            {
                                //Global.mnFrm.cmCde.showSQLNoPermsn("C:\\Windows\\System32\\cmd.exe /C java -jar " + Application.StartupPath + rnnrPrcsFile + " " + String.Join(" ", args));
                                System.Diagnostics.Process jarPrcs = new System.Diagnostics.Process();
                                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                                startInfo.FileName = "cmd.exe";
                                startInfo.Arguments = "/C javaw -jar -Xms1024m -Xmx1024m \"" +
                                  Global.appStatPath + "/" + rnnrPrcsFile + "\" " + String.Join(" ", args);
                                jarPrcs.StartInfo = startInfo;
                                jarPrcs.Start();
                                //System.Diagnostics.Process.Start("javaw", "-jar -Xms1024m -Xmx1024m " + Global.appStatPath + "/" + rnnrPrcsFile + " " + String.Join(" ", args));
                            }
                            else
                            {
                                System.Diagnostics.Process.Start(Global.appStatPath + "/" + rnnrPrcsFile, String.Join(" ", args));
                            }
                            //System.Diagnostics.Process.Start(Global.appStatPath + "/" + rnnrPrcsFile, String.Join(" ", args));
                        }
                        do
                        {
                            curCons = Global.getCurDBConns();
                            Global.errorLog = "Inside Running of Scheduled Alerts=> Current Connections: " + curCons + " Max Connections: " + mxConns;
                            Global.writeToLog();
                            Program.checkNClosePrgrm();
                            Thread.Sleep(50);
                            if (curCons >= mxConns)
                            {
                                Thread.Sleep(50000);
                            }
                        }
                        while (curCons >= mxConns);
                    }
                    Thread.Sleep(10000);
                    long prgmID = Global.getGnrlRecID("rpt.rpt_prcss_rnnrs", "rnnr_name", "prcss_rnnr_id", runnerName);
                    Program.updatePrgrm(prgmID);
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
                if (threadSeven.IsAlive)
                {
                    threadSeven.Abort();
                }
            }
            finally
            {
            }
        }

        static void mntrUserAlertsNtRnngfunc()
        {
            try
            {
                do
                {
                    //Get all rquest runs not running
                    //Launch appropriate process runner
                    Program.checkNClosePrgrm();
                    DataSet dtst = Global.get_UserAlertsNtRnng();
                    long mxConns = 0;
                    long curCons = 0;
                    mxConns = Global.getMxAllwdDBConns();
                    for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
                    {
                        long rptid = long.Parse(dtst.Tables[0].Rows[i][1].ToString());
                        long rptrnid = long.Parse(dtst.Tables[0].Rows[i][0].ToString());
                        long rptrnnrid = long.Parse(dtst.Tables[0].Rows[i][2].ToString());
                        string rptRnnrNm = Global.getGnrlRecNm("rpt.rpt_reports", "report_id", "process_runner", rptid);
                        string rnnrPrcsFile = Global.getGnrlRecNm("rpt.rpt_prcss_rnnrs", "rnnr_name", "executbl_file_nm", rptRnnrNm);
                        if (rptRnnrNm == "")
                        {
                            rptRnnrNm = "Standard Process Runner";
                        }
                        if (rnnrPrcsFile == "")
                        {
                            rnnrPrcsFile = @"\bin\REMSProcessRunner.exe";
                        }

                        rnnrPrcsFile = rnnrPrcsFile.Replace("/bin", "").Replace("\\bin", "");

                        if (Global.doesLstRnTmExcdIntvl(rptid, "1 second", rptrnid) == true)
                        {
                            Global.updatePrcsRnnrCmd(rptRnnrNm, "0", rptrnnrid);
                            Global.updateRptRnStopCmd(rptrnid, "0");
                            string[] args = { "\"" + Global.Hostnme + "\"",
                          Global.Portnum,
                          "\"" + Global.Uname + "\"",
                          "\"" + Global.Pswd + "\"",
                          "\"" + Global.Dbase + "\"",
                          "\"" + rptRnnrNm + "\"",
                          (rptrnid).ToString(),
                          "\""+ System.IO.Path.GetDirectoryName(Global.appStatPath + "/" +rnnrPrcsFile) + "\"",
                          "DESKTOP",
                          "\""+ Global.dataBasDir + "\""};
                            //Global.showMsg(String.Join(" ", args), 0);
                            //Replace("/bin", "").Replace("\\bin", "")]
                            if (rptRnnrNm.Contains("Jasper"))
                            {
                                //Global.mnFrm.cmCde.showSQLNoPermsn("C:\\Windows\\System32\\cmd.exe /C java -jar " + Application.StartupPath + rnnrPrcsFile + " " + String.Join(" ", args));
                                System.Diagnostics.Process jarPrcs = new System.Diagnostics.Process();
                                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                                startInfo.FileName = "cmd.exe";
                                startInfo.Arguments = "/C javaw -jar -Xms1024m -Xmx1024m \"" +
                                  Global.appStatPath + "/" + rnnrPrcsFile + "\" " + String.Join(" ", args);
                                jarPrcs.StartInfo = startInfo;
                                jarPrcs.Start();
                                //System.Diagnostics.Process.Start("javaw", "-jar -Xms1024m -Xmx1024m " + Global.appStatPath + "/" + rnnrPrcsFile + " " + String.Join(" ", args));
                            }
                            else
                            {
                                System.Diagnostics.Process.Start(Global.appStatPath + "/" + rnnrPrcsFile, String.Join(" ", args));
                            }
                            //System.Diagnostics.Process.Start(Global.appStatPath + "/" + rnnrPrcsFile, String.Join(" ", args));
                        }
                        do
                        {
                            curCons = Global.getCurDBConns();
                            Global.errorLog = "Inside Running of User Initiated Alerts=> Current Connections: " + curCons + " Max Connections: " + mxConns;
                            Global.writeToLog();
                            Program.checkNClosePrgrm();
                            Thread.Sleep(50);
                            if (curCons >= mxConns)
                            {
                                Thread.Sleep(50000);
                            }
                        }
                        while (curCons >= mxConns);
                    }
                    Thread.Sleep(10000);
                    long prgmID = Global.getGnrlRecID("rpt.rpt_prcss_rnnrs", "rnnr_name", "prcss_rnnr_id", runnerName);
                    Program.updatePrgrm(prgmID);
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
                if (threadSeven.IsAlive)
                {
                    threadSeven.Abort();
                }
            }
            finally
            {
            }
        }

        static void gnrtSchldRnsfunc()
        {
            try
            {
                do
                {
                    //1. Get all enabled schedules
                    //2. for each enabled schedule check last time it was run
                    // if difference between last_time_active is >= schedule interval 
                    //and time component is >= current time then generate another schedule run
                    Program.checkNClosePrgrm();
                    DataSet dtst = Global.get_Schdules();
                    for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
                    {
                        long rpt_id = long.Parse(dtst.Tables[0].Rows[i][1].ToString());
                        long schdlID = long.Parse(dtst.Tables[0].Rows[i][0].ToString());

                        if (Global.doesLstRnTmExcdIntvl(rpt_id,
                          dtst.Tables[0].Rows[i][4].ToString() + " " + dtst.Tables[0].Rows[i][5].ToString(), -1) == true)
                        {
                            string dateStr = Global.getDB_Date_time();
                            TimeSpan tm = new TimeSpan(0, 0, 59);
                            dateStr = (DateTime.ParseExact(
                      dateStr, "yyyy-MM-dd HH:mm:ss",
                      System.Globalization.CultureInfo.InvariantCulture) - tm).ToString("yyyy-MM-dd HH:mm:ss");

                            string outputUsd = Global.getGnrlRecNm("rpt.rpt_reports", "report_id", "output_type", rpt_id);
                            string orntnUsd = Global.getGnrlRecNm("rpt.rpt_reports", "report_id", "portrait_lndscp", rpt_id);
                            string prmIDs = "";
                            string prmVals = "";
                            DataSet dtstPrm = Global.get_SchdulesParams(schdlID);
                            for (int y = 0; y < dtstPrm.Tables[0].Rows.Count; y++)
                            {
                                prmVals += dtstPrm.Tables[0].Rows[y][3].ToString() + "|";
                                prmIDs += dtstPrm.Tables[0].Rows[y][1].ToString() + "|";
                            }
                            string colsToGrp = Global.getGnrlRecNm("rpt.rpt_reports", "report_id", "cols_to_group", rpt_id);
                            string colsToCnt = Global.getGnrlRecNm("rpt.rpt_reports", "report_id", "cols_to_count", rpt_id);
                            string colsToSu = Global.getGnrlRecNm("rpt.rpt_reports", "report_id", "cols_to_sum", rpt_id);
                            string colsToAvrg = Global.getGnrlRecNm("rpt.rpt_reports", "report_id", "cols_to_average", rpt_id);
                            string colsToFrmt = Global.getGnrlRecNm("rpt.rpt_reports", "report_id", "cols_to_no_frmt", rpt_id);
                            string rpTitle = Global.getGnrlRecNm("rpt.rpt_reports", "report_id", "report_name", rpt_id);

                            //Report Title
                            prmVals += rpTitle + "|";
                            prmIDs += Global.sysParaIDs[0] + "|";
                            //Cols To Group
                            prmVals += colsToGrp + "|";
                            prmIDs += Global.sysParaIDs[1] + "|";
                            //Cols To Count
                            prmVals += colsToCnt + "|";
                            prmIDs += Global.sysParaIDs[2] + "|";
                            //Cols To Sum
                            prmVals += colsToSu + "|";
                            prmIDs += Global.sysParaIDs[3] + "|";
                            //colsToAvrg
                            prmVals += colsToAvrg + "|";
                            prmIDs += Global.sysParaIDs[4] + "|";
                            //colsToFrmt
                            prmVals += colsToFrmt + "|";
                            prmIDs += Global.sysParaIDs[5] + "|";

                            //outputUsd
                            prmVals += outputUsd + "|";
                            prmIDs += Global.sysParaIDs[6] + "|";

                            //orntnUsd
                            prmVals += orntnUsd + "|";
                            prmIDs += Global.sysParaIDs[7] + "|";

                            Global.createSchdldRptRn(
                              long.Parse(dtst.Tables[0].Rows[i][6].ToString()), dateStr,
                              rpt_id, prmIDs, prmVals, outputUsd, orntnUsd, -1, -1);

                            Thread.Sleep(5000);

                            long rptRunID = Global.getRptRnID(rpt_id,
                long.Parse(dtst.Tables[0].Rows[i][6].ToString()), dateStr);

                            long msg_id = Global.getLogMsgID("rpt.rpt_run_msgs",
                              "Process Run", rptRunID);
                            if (msg_id <= 0)
                            {
                                Global.createLogMsg(dateStr +
                                " .... Report/Process Run is about to Start...(Being run by " +
                                Global.get_user_name(long.Parse(dtst.Tables[0].Rows[i][6].ToString())) + ")",
                                "rpt.rpt_run_msgs", "Process Run", rptRunID, dateStr);
                            }
                            //msg_id = Global.getLogMsgID("rpt.rpt_run_msgs", "Process Run", rptRunID);
                        }
                    }
                    long mxConns = 0;
                    long curCons = 0;
                    do
                    {
                        mxConns = Global.getMxAllwdDBConns();
                        curCons = Global.getCurDBConns();
                        Global.errorLog = "Inside Generation of Scheduled Requests=> Current Connections: " + curCons + " Max Connections: " + mxConns;
                        Global.writeToLog();

                        Thread.Sleep(30000);
                        long prgmID = Global.getGnrlRecID("rpt.rpt_prcss_rnnrs", "rnnr_name", "prcss_rnnr_id", runnerName);
                        Program.updatePrgrm(prgmID);
                    }
                    while (curCons >= mxConns);
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
                if (threadTwo.IsAlive)
                {
                    threadTwo.Abort();
                }
            }
            finally
            {
            }
        }

        static void gnrtSchldAlertsfunc()
        {
            try
            {
                do
                {
                    //1. Get all enabled schedules
                    //2. for each enabled schedule check last time it was run
                    // if difference between last_time_active is >= schedule interval 
                    //and time component is >= current time then generate another schedule run
                    Program.checkNClosePrgrm();
                    DataSet dtst = Global.get_AlertSchdules();
                    for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
                    {
                        long rpt_id = long.Parse(dtst.Tables[0].Rows[i][1].ToString());
                        long alertID = long.Parse(dtst.Tables[0].Rows[i][0].ToString());

                        if (Global.doesLstRnTmExcdIntvl(rpt_id,
                          dtst.Tables[0].Rows[i][4].ToString() + " " + dtst.Tables[0].Rows[i][5].ToString(), -1) == true)
                        {
                            string dateStr = Global.getDB_Date_time();
                            TimeSpan tm = new TimeSpan(0, 1, 59);
                            dateStr = (DateTime.ParseExact(
                      dateStr, "yyyy-MM-dd HH:mm:ss",
                      System.Globalization.CultureInfo.InvariantCulture) - tm).ToString("yyyy-MM-dd HH:mm:ss");

                            string outputUsd = Global.getGnrlRecNm("rpt.rpt_reports", "report_id", "output_type", rpt_id);
                            string orntnUsd = Global.getGnrlRecNm("rpt.rpt_reports", "report_id", "portrait_lndscp", rpt_id);
                            string prmIDs = "";
                            string prmVals = "";
                            DataSet dtstPrm = Global.get_AlertParams(alertID);
                            for (int y = 0; y < dtstPrm.Tables[0].Rows.Count; y++)
                            {
                                prmVals += dtstPrm.Tables[0].Rows[y][3].ToString() + "|";
                                prmIDs += dtstPrm.Tables[0].Rows[y][1].ToString() + "|";
                            }
                            string colsToGrp = Global.getGnrlRecNm("rpt.rpt_reports", "report_id", "cols_to_group", rpt_id);
                            string colsToCnt = Global.getGnrlRecNm("rpt.rpt_reports", "report_id", "cols_to_count", rpt_id);
                            string colsToSu = Global.getGnrlRecNm("rpt.rpt_reports", "report_id", "cols_to_sum", rpt_id);
                            string colsToAvrg = Global.getGnrlRecNm("rpt.rpt_reports", "report_id", "cols_to_average", rpt_id);
                            string colsToFrmt = Global.getGnrlRecNm("rpt.rpt_reports", "report_id", "cols_to_no_frmt", rpt_id);
                            string rpTitle = Global.getGnrlRecNm("rpt.rpt_reports", "report_id", "report_name", rpt_id);

                            //Report Title
                            prmVals += rpTitle + "|";
                            prmIDs += Global.sysParaIDs[0] + "|";
                            //Cols To Group
                            prmVals += colsToGrp + "|";
                            prmIDs += Global.sysParaIDs[1] + "|";
                            //Cols To Count
                            prmVals += colsToCnt + "|";
                            prmIDs += Global.sysParaIDs[2] + "|";
                            //Cols To Sum
                            prmVals += colsToSu + "|";
                            prmIDs += Global.sysParaIDs[3] + "|";
                            //colsToAvrg
                            prmVals += colsToAvrg + "|";
                            prmIDs += Global.sysParaIDs[4] + "|";
                            //colsToFrmt
                            prmVals += colsToFrmt + "|";
                            prmIDs += Global.sysParaIDs[5] + "|";

                            //outputUsd
                            prmVals += outputUsd + "|";
                            prmIDs += Global.sysParaIDs[6] + "|";

                            //orntnUsd
                            prmVals += orntnUsd + "|";
                            prmIDs += Global.sysParaIDs[7] + "|";

                            Global.createSchdldRptRn(
                              long.Parse(dtst.Tables[0].Rows[i][6].ToString()), dateStr,
                              rpt_id, prmIDs, prmVals, outputUsd, orntnUsd, int.Parse(dtst.Tables[0].Rows[i][0].ToString()), -1);

                            Thread.Sleep(5000);

                            long rptRunID = Global.getRptRnID(rpt_id,
                long.Parse(dtst.Tables[0].Rows[i][6].ToString()), dateStr);

                            long msg_id = Global.getLogMsgID("rpt.rpt_run_msgs",
                              "Process Run", rptRunID);
                            if (msg_id <= 0)
                            {
                                Global.createLogMsg(dateStr +
                                " .... Alert Run is about to Start...(Being run by " +
                                Global.get_user_name(long.Parse(dtst.Tables[0].Rows[i][6].ToString())) + ")",
                                "rpt.rpt_run_msgs", "Process Run", rptRunID, dateStr);
                            }
                            //msg_id = Global.getLogMsgID("rpt.rpt_run_msgs", "Process Run", rptRunID);
                        }
                    }
                    long mxConns = 0;
                    long curCons = 0;
                    do
                    {
                        mxConns = Global.getMxAllwdDBConns();
                        curCons = Global.getCurDBConns();
                        Global.errorLog = "Inside Generation of Scheduled Requests=> Current Connections: " + curCons + " Max Connections: " + mxConns;
                        Global.writeToLog();

                        Thread.Sleep(30000);
                        long prgmID = Global.getGnrlRecID("rpt.rpt_prcss_rnnrs", "rnnr_name", "prcss_rnnr_id", runnerName);
                        Program.updatePrgrm(prgmID);
                    }
                    while (curCons >= mxConns);
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
                if (threadSix.IsAlive)
                {
                    threadSix.Abort();
                }
            }
            finally
            {
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
                TimeSpan tm = new TimeSpan(0, 1, 59);
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
                if (threadSix.IsAlive)
                {
                    threadSix.Abort();
                }
            }
            finally
            {
            }
        }

        static void rqstLstnrUpdtrfunc()
        {
            try
            {
                long prgmID = Global.getGnrlRecID("rpt.rpt_prcss_rnnrs", "rnnr_name", "prcss_rnnr_id", runnerName);
                Global.errorLog = "Successfully Started Thread One\r\nProgram ID:" + prgmID + "\r\n";
                Global.writeToLog();
                do
                {
                    Program.updatePrgrm(prgmID);
                    Global.minimizeMemory();
                    Thread.Sleep(40000);
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

        static void doSthing(long b, string str)
        {
            StreamWriter fileWriter;
            string fileLoc = @"C:\Users\rhemitech_gh\Desktop\REMSProcessRunnerFiles\";
            fileLoc += str + DateTime.Now.ToString("ddMMMyyyyHHmmss") + b.ToString() + ".rho";


            fileWriter = new StreamWriter(fileLoc, true);
            //fileWriter. = txt.(fileLoc);
            fileWriter.WriteLine(str + b.ToString());
            fileWriter.WriteLine(Global.errorLog);
            fileWriter.Close();
            fileWriter = null;

        }

        private static void validateBatchNPost(long glBatchID, string batchStatus,
          string batchSource,
          long msg_id, string log_tbl, string dateStr)
        {
            Global.updtActnPrcss(5);
            batchStatus = Global.getGnrlRecNm("accb.accb_trnsctn_batches",
              "batch_id", "batch_status", glBatchID);
            if (batchStatus == "1")
            {
                Global.updateLogMsg(msg_id,
        "\r\nCannot Post an already Posted Batch of Transactions!\r\n",
        log_tbl, dateStr, Global.rnUser_ID);

                return;
            }

            int suspns_accnt = Global.get_Suspns_Accnt(Global.UsrsOrg_ID);


            DataSet dteDtSt = Global.get_Batch_dateSums(glBatchID);
            if (dteDtSt.Tables[0].Rows.Count > 0 && suspns_accnt > 0)
            {
                string msg1 = @"";
                for (int i = 0; i < dteDtSt.Tables[0].Rows.Count; i++)
                {
                    double dlyDbtAmnt = double.Parse(dteDtSt.Tables[0].Rows[i][1].ToString());
                    double dlyCrdtAmnt = double.Parse(dteDtSt.Tables[0].Rows[i][2].ToString());
                    int orgID = Global.UsrsOrg_ID;
                    if (dlyDbtAmnt
                     != dlyCrdtAmnt)
                    {
                        long suspns_batch_id = glBatchID;
                        int funcCurrID = Global.getOrgFuncCurID(orgID);
                        decimal dffrnc = (decimal)(dlyDbtAmnt - dlyCrdtAmnt);
                        string incrsDcrs = "D";
                        if (dffrnc < 0)
                        {
                            incrsDcrs = "I";
                        }
                        decimal imbalAmnt = Math.Abs(dffrnc);
                        double netAmnt = (double)Global.dbtOrCrdtAccntMultiplier(suspns_accnt,
                   incrsDcrs) * (double)imbalAmnt;
                        string dateStr1 = DateTime.ParseExact(dteDtSt.Tables[0].Rows[i][0].ToString(), "yyyy-MM-dd",
            System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy") + " 00:00:00";
                        //if (!Global.mnFrm.cmCde.isTransPrmttd(suspns_accnt,
                        //      dateStr, netAmnt))
                        //{
                        //  return; ;
                        //}

                        if (Global.dbtOrCrdtAccnt(suspns_accnt, incrsDcrs) == "Debit")
                        {
                            Global.createTransaction(suspns_accnt,
                                "Correction of Imbalance in GL Batch " + Global.getGnrlRecNm("accb.accb_trnsctn_batches",
                                "batch_id", "batch_name", glBatchID) + " as at " + dateStr1, (double)imbalAmnt,
                                dateStr1
                                , funcCurrID, suspns_batch_id, 0.00, netAmnt,
                              (double)imbalAmnt,
                              funcCurrID,
                              (double)imbalAmnt,
                              funcCurrID,
                              (double)1,
                              (double)1, "D");
                        }
                        else
                        {
                            Global.createTransaction(suspns_accnt,
                            "Correction of Imbalance in GL Batch " + Global.getGnrlRecNm("accb.accb_trnsctn_batches",
                                "batch_id", "batch_name", glBatchID) + " as at " + dateStr1, 0.00,
                            dateStr1, funcCurrID,
                            suspns_batch_id, (double)imbalAmnt, netAmnt,
                        (double)imbalAmnt,
                        funcCurrID,
                        (double)imbalAmnt,
                        funcCurrID,
                        (double)1,
                        (double)1, "C");
                        }
                    }

                    //msg1 = msg1 + dteDtSt.Tables[0].Rows[i][0].ToString() + "\t DR=" + 
                    //dteDtSt.Tables[0].Rows[i][1].ToString() + "\t CR=" + 
                    //dteDtSt.Tables[0].Rows[i][2].ToString() + "\r\n";
                }
                //Global.mnFrm.cmCde.showMsg(msg1, 4);
                //return;
            }
            else
            {
                //Global.mnFrm.cmCde.showMsg("There's no Imbalance to correct!", 0);
                //return;
            }

            if (Global.get_Batch_CrdtSum(glBatchID) != Global.get_Batch_DbtSum(glBatchID))
            {
                Global.updateLogMsg(msg_id,
        "\r\nCannot Post an Unbalanced Batch of Transactions!\r\n",
        log_tbl, dateStr, Global.rnUser_ID);
                return;
            }

            Global.updtActnPrcss(5);
            DataSet dtst = Global.get_Batch_Trns_NoStatus(glBatchID);
            long ttltrns = dtst.Tables[0].Rows.Count;
            if (ttltrns <= 0 && batchSource != "Period Close Process")
            {
                Global.updateLogMsg(msg_id,
        "\r\nOnly Period Close Process Batches can be posted when the batch has no transactions!\r\n",
        log_tbl, dateStr, Global.rnUser_ID);
                return;
            }
            int ret_accnt = Global.get_Rtnd_Erngs_Accnt(Global.UsrsOrg_ID);
            int net_accnt = Global.get_Net_Income_Accnt(Global.UsrsOrg_ID);
            if (ret_accnt == -1)
            {
                Global.updateLogMsg(msg_id,
        "\r\nUntil a Retained Earnings Account is defined\r\n no Transaction can be posted into the Accounting!\r\n",
        log_tbl, dateStr, Global.rnUser_ID);
                return;
            }
            if (net_accnt == -1)
            {
                Global.updateLogMsg(msg_id,
        "\r\nUntil a Net Income Account is defined\r\n no Transaction can be posted into the Accounting!\r\n",
        log_tbl, dateStr, Global.rnUser_ID);
                return;
            }
            Global.updtActnPrcss(5);

            DataSet dteDtSt1 = Global.get_Batch_dateSums(glBatchID);
            if (dteDtSt1.Tables[0].Rows.Count > 0)
            {
                string msg1 = @"Your transactions will cause your Balance Sheet to become Unbalanced on some Days!
        Please make sure each day has equal debits and credits. Check the ff Days:" + "\r\n";
                for (int i = 0; i < dteDtSt1.Tables[0].Rows.Count; i++)
                {
                    msg1 = msg1 + dteDtSt1.Tables[0].Rows[i][0].ToString() + "\t DR=" +
                      dteDtSt1.Tables[0].Rows[i][1].ToString() + "\t CR=" +
                      dteDtSt1.Tables[0].Rows[i][2].ToString() + "\r\n";
                }
                Global.updateLogMsg(msg_id,
        "\r\n" + msg1 + "!\r\n",
        log_tbl, dateStr, Global.rnUser_ID);
                return;
            }
            int funCurID = -1;
            funCurID = Global.getOrgFuncCurID(Global.UsrsOrg_ID);
            Global.updtActnPrcss(5);
            Program.postGLBatch(glBatchID,
             batchSource,
             msg_id, log_tbl, dateStr, net_accnt, funCurID);
        }

        //  private static bool postIntoSuspnsAccnt(decimal aeVal, decimal crlVal, int orgID, ref string errmsg)
        //  {
        //    return false;
        //    try
        //    {
        //      int suspns_accnt = Global.get_Suspns_Accnt(orgID);
        //      int net_accnt = Global.get_Net_Income_Accnt(orgID);

        //      if (suspns_accnt == -1)
        //      {
        //        errmsg += "Please define a suspense Account First before imbalance can be Auto-Corrected!";
        //        return false;
        //      }
        //      long suspns_batch_id = -999999991;
        //      int funcCurrID = Global.getOrgFuncCurID(orgID);
        //      decimal dffrnc = Math.Round(aeVal - crlVal, 2);
        //      string incrsDcrs = "D";
        //      if (dffrnc < 0)
        //      {
        //        incrsDcrs = "I";
        //      }
        //      decimal imbalAmnt = Math.Abs(dffrnc);
        //      double netAmnt = (double)Global.dbtOrCrdtAccntMultiplier(suspns_accnt,
        //incrsDcrs) * (double)imbalAmnt;
        //      string dateStr = Global.getFrmtdDB_Date_time();

        //      //if (!Global.isTransPrmttd(suspns_accnt,
        //      //      dateStr, netAmnt))
        //      //{
        //      //  return false;
        //      //}

        //      if (Global.dbtOrCrdtAccnt(suspns_accnt, incrsDcrs) == "Debit")
        //      {
        //        Global.createTransaction(suspns_accnt,
        //            "Correction of Imbalance as at " + dateStr, (double)imbalAmnt,
        //            dateStr
        //            , funcCurrID, suspns_batch_id, 0.00, netAmnt,
        //          (double)imbalAmnt,
        //          funcCurrID,
        //          (double)imbalAmnt,
        //          funcCurrID,
        //          (double)1,
        //          (double)1, "D");
        //      }
        //      else
        //      {
        //        Global.createTransaction(suspns_accnt,
        //        "Correction of Imbalance as at " + dateStr, 0.00,
        //        dateStr, funcCurrID,
        //        suspns_batch_id, (double)imbalAmnt, netAmnt,
        //    (double)imbalAmnt,
        //    funcCurrID,
        //    (double)imbalAmnt,
        //    funcCurrID,
        //    (double)1,
        //    (double)1, "C");
        //      }

        //      DataSet dtst = Global.get_Batch_Trns(suspns_batch_id);

        //      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
        //      {
        //        bool hsBnUpdt = Global.hsTrnsUptdAcntBls(
        //              long.Parse(dtst.Tables[0].Rows[i][0].ToString()),
        //            dtst.Tables[0].Rows[i][6].ToString(),
        //              int.Parse(dtst.Tables[0].Rows[i][9].ToString()));
        //        if (hsBnUpdt == false)
        //        {
        //          double dbt1 = double.Parse(dtst.Tables[0].Rows[i][4].ToString());
        //          double crdt1 = double.Parse(dtst.Tables[0].Rows[i][5].ToString());
        //          double net1 = double.Parse(dtst.Tables[0].Rows[i][10].ToString());

        //          Global.postTransaction(int.Parse(dtst.Tables[0].Rows[i][9].ToString()),
        //           dbt1,
        //           crdt1,
        //           net1,
        //           dtst.Tables[0].Rows[i][6].ToString(),
        //           long.Parse(dtst.Tables[0].Rows[i][0].ToString()));
        //          Global.chngeTrnsStatus(long.Parse(dtst.Tables[0].Rows[i][0].ToString()), "1");
        //        }
        //      }
        //      Program.reloadAcntChrtBals(suspns_batch_id, net_accnt);

        //      return true;
        //    }
        //    catch (Exception ex)
        //    {
        //      errmsg += ex.Message + "\r\n\r\n" + ex.InnerException.ToString();
        //      return false;
        //    }
        //  }

        private static bool postIntoSuspnsAccnt(decimal aeVal, decimal crlVal, int orgID, bool isspcl, ref string errmsg)
        {
            try
            {
                int suspns_accnt = Global.get_Suspns_Accnt(orgID);
                int net_accnt = Global.get_Net_Income_Accnt(orgID);
                int ret_accnt = Global.get_Rtnd_Erngs_Accnt(orgID);

                if (suspns_accnt == -1)
                {
                    errmsg += "Please define a suspense Account First before imbalance can be Auto-Corrected!";
                    return false;
                }
                long suspns_batch_id = -999999991;
                int funcCurrID = Global.getOrgFuncCurID(orgID);
                decimal dffrnc = Math.Round(aeVal - crlVal, 2);
                string incrsDcrs = "D";
                if (dffrnc < 0)
                {
                    incrsDcrs = "I";
                }
                decimal imbalAmnt = Math.Abs(dffrnc);
                double netAmnt = (double)Global.dbtOrCrdtAccntMultiplier(suspns_accnt,
          incrsDcrs) * (double)imbalAmnt;
                string dateStr = Global.getFrmtdDB_Date_time();
                if (!Global.isTransPrmttd(suspns_accnt,
                      dateStr, netAmnt, ref errmsg))
                {
                    return false;
                }

                if (Global.dbtOrCrdtAccnt(suspns_accnt, incrsDcrs) == "Debit")
                {
                    Global.createTransaction(suspns_accnt,
                        "Correction of Imbalance as at " + dateStr, (double)imbalAmnt,
                        dateStr
                        , funcCurrID, suspns_batch_id, 0.00, netAmnt,
                      (double)imbalAmnt,
                      funcCurrID,
                      (double)imbalAmnt,
                      funcCurrID,
                      (double)1,
                      (double)1, "D");
                    //if (isspcl)
                    //{
                    //  Global.createTransaction(ret_accnt,
                    //   "Correction of Imbalance as at " + dateStr, (double)imbalAmnt,
                    //   dateStr
                    //   , funcCurrID, suspns_batch_id, 0.00, netAmnt,
                    // (double)imbalAmnt,
                    // funcCurrID,
                    // (double)imbalAmnt,
                    // funcCurrID,
                    // (double)1,
                    // (double)1, "D");
                    //}
                }
                else
                {
                    Global.createTransaction(suspns_accnt,
                    "Correction of Imbalance as at " + dateStr, 0.00,
                    dateStr, funcCurrID,
                    suspns_batch_id, (double)imbalAmnt, netAmnt,
                (double)imbalAmnt,
                funcCurrID,
                (double)imbalAmnt,
                funcCurrID,
                (double)1,
                (double)1, "C");
                }

                DataSet dtst = Global.get_Batch_Trns(suspns_batch_id);

                for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
                {
                    bool hsBnUpdt = Global.hsTrnsUptdAcntBls(
                          long.Parse(dtst.Tables[0].Rows[i][0].ToString()),
                        dtst.Tables[0].Rows[i][6].ToString(),
                          int.Parse(dtst.Tables[0].Rows[i][9].ToString()));
                    if (hsBnUpdt == false)
                    {
                        double dbt1 = double.Parse(dtst.Tables[0].Rows[i][4].ToString());
                        double crdt1 = double.Parse(dtst.Tables[0].Rows[i][5].ToString());
                        double net1 = double.Parse(dtst.Tables[0].Rows[i][10].ToString());

                        Global.postTransaction(int.Parse(dtst.Tables[0].Rows[i][9].ToString()),
                         dbt1,
                         crdt1,
                         net1,
                         dtst.Tables[0].Rows[i][6].ToString(),
                         long.Parse(dtst.Tables[0].Rows[i][0].ToString()));
                        Global.chngeTrnsStatus(long.Parse(dtst.Tables[0].Rows[i][0].ToString()), "1");
                    }
                }

                Program.reloadAcntChrtBals(suspns_batch_id, net_accnt);

                return true;
            }
            catch (Exception ex)
            {
                errmsg += ex.Message + "\r\n\r\n" + ex.InnerException.ToString();
                return false;
            }
        }


        private static void postGLBatch(long glBatchID,
          string btchSrc,
          long msg_id, string log_tbl, string dateStr, int net_accnt, int funCurID)
        {
            try
            {
                //string dateStr1 = Global.getFrmtdDB_Date_time();
                string log_tbl1 = "accb.accb_post_trns_msgs";
                long msg_id1 = Global.getLogMsgID(log_tbl1,
                  "Posting Batch of Transactions", glBatchID);
                if (msg_id1 <= 0)
                {
                    Global.createLogMsg(dateStr + " ....Automatic Posting Batch of Transactions is about to Start...",
              log_tbl1, "Posting Batch of Transactions", glBatchID, dateStr);
                }
                msg_id1 = Global.getLogMsgID(log_tbl1, "Posting Batch of Transactions",
                  glBatchID);

                Global.updateLogMsg(msg_id,
        "\r\n\r\n ....Automatic Posting Batch of Transactions is about to Start...!\r\n",
        log_tbl, dateStr, Global.rnUser_ID);

                double aesum = Global.get_COA_AESum(Global.UsrsOrg_ID);
                double crlsum = Global.get_COA_CRLSum(Global.UsrsOrg_ID);
                if (aesum
                 != crlsum)
                {
                    Global.updateLogMsg(msg_id1,
           "\r\nCannot Post this Batch Since Current GL is not Balanced!Please correct the Imbalance First!\r\n",
           log_tbl1, dateStr, Global.rnUser_ID);

                    Global.updateLogMsg(msg_id,
           "\r\nCannot Post this Batch Since Current GL is not Balanced!Please correct the Imbalance First!\r\n",
          log_tbl, dateStr, Global.rnUser_ID);

                    Program.correctImblns();

                    Global.updateRptRnStopCmd(Global.runID, "1");
                    Program.checkNClosePrgrm();

                    return;
                }
                Global.updtActnPrcss(5);

                DataSet dtst = Global.get_Batch_Trns(glBatchID);
                long ttltrns = dtst.Tables[0].Rows.Count;
                //string btchSrc = Global.getGnrlRecNm(
                //  "accb.accb_trnsctn_batches", "batch_id", "batch_source", glBatchID);
                Global.updtActnPrcss(5);

                //Validating Entries
                if (btchSrc != "Period Close Process")
                {
                    for (int i = 0; i < ttltrns; i++)
                    {
                        Global.updtActnPrcss(5);
                        int accntid = int.Parse(dtst.Tables[0].Rows[i][9].ToString());
                        double netAmnt = double.Parse(dtst.Tables[0].Rows[i][10].ToString());
                        string lnDte = dtst.Tables[0].Rows[i][6].ToString();
                        string errmsg = "";
                        if (!Global.isTransPrmttd(accntid, lnDte, netAmnt, ref errmsg))
                        {
                            Global.updateLogMsg(msg_id1,
                            "\r\n\r\n" + errmsg + "\r\n\r\nOperation Cancelled because the line with the\r\n ff details was detected as an INVALID Transaction!" +
                            "\r\nACCOUNT: " + dtst.Tables[0].Rows[i][1].ToString() + "." + dtst.Tables[0].Rows[i][2].ToString() +
                            "\r\nAMOUNT: " + netAmnt +
                            "\r\nDATE: " + lnDte,
                            log_tbl1, dateStr, Global.rnUser_ID);

                            Global.updateLogMsg(msg_id,
                            "\r\n\r\n" + errmsg + "\r\n\r\nOperation Cancelled because the line with the\r\n ff details was detected as an INVALID Transaction!" +
                            "\r\nACCOUNT: " + dtst.Tables[0].Rows[i][1].ToString() + "." + dtst.Tables[0].Rows[i][2].ToString() +
                            "\r\nAMOUNT: " + netAmnt +
                            "\r\nDATE: " + lnDte,
              log_tbl, dateStr, Global.rnUser_ID);
                            return;
                        }
                    }
                }

                for (int i = 0; i < ttltrns; i++)
                {
                    Global.updtActnPrcss(5);
                    //Update the corresponding account balance and 
                    //update net income balance as well if type is R or EX
                    //update control account if any
                    //update accnt curr bals if different from 
                    int accntCurrID = int.Parse(dtst.Tables[0].Rows[i][17].ToString());
                    int funcCurr = int.Parse(dtst.Tables[0].Rows[i][7].ToString());
                    double accntCurrAmnt = double.Parse(dtst.Tables[0].Rows[i][15].ToString());

                    string acctyp = Global.getAccntType(
                     int.Parse(dtst.Tables[0].Rows[i][9].ToString()));
                    bool hsBnUpdt = Global.hsTrnsUptdAcntBls(
                      long.Parse(dtst.Tables[0].Rows[i][0].ToString()),
                    dtst.Tables[0].Rows[i][6].ToString(),
                      int.Parse(dtst.Tables[0].Rows[i][9].ToString()));
                    if (hsBnUpdt == false)
                    {
                        double dbt1 = double.Parse(dtst.Tables[0].Rows[i][4].ToString());
                        double crdt1 = double.Parse(dtst.Tables[0].Rows[i][5].ToString());
                        double net1 = double.Parse(dtst.Tables[0].Rows[i][10].ToString());

                        if (funCurID != accntCurrID)
                        {
                            Global.postAccntCurrTransaction(int.Parse(dtst.Tables[0].Rows[i][9].ToString()),
                             Global.getSign(dbt1) * accntCurrAmnt,
                             Global.getSign(crdt1) * accntCurrAmnt,
                             Global.getSign(net1) * accntCurrAmnt,
                             dtst.Tables[0].Rows[i][6].ToString(),
                             long.Parse(dtst.Tables[0].Rows[i][0].ToString()), accntCurrID);
                        }

                        Global.postTransaction(int.Parse(dtst.Tables[0].Rows[i][9].ToString()),
                         dbt1,
                         crdt1,
                         net1,
                         dtst.Tables[0].Rows[i][6].ToString(),
                         long.Parse(dtst.Tables[0].Rows[i][0].ToString()));
                    }

                    hsBnUpdt = Global.hsTrnsUptdAcntBls(
               long.Parse(dtst.Tables[0].Rows[i][0].ToString()),
             dtst.Tables[0].Rows[i][6].ToString(),
               net_accnt);

                    if (hsBnUpdt == false)
                    {
                        if (acctyp == "R")
                        {
                            Global.postTransaction(net_accnt,
                        double.Parse(dtst.Tables[0].Rows[i][4].ToString()),
                            double.Parse(dtst.Tables[0].Rows[i][5].ToString()),
                            double.Parse(dtst.Tables[0].Rows[i][10].ToString()),
                            dtst.Tables[0].Rows[i][6].ToString(),
                            long.Parse(dtst.Tables[0].Rows[i][0].ToString()));
                        }
                        else if (acctyp == "EX")
                        {
                            Global.postTransaction(net_accnt,
                        double.Parse(dtst.Tables[0].Rows[i][4].ToString()),
                        double.Parse(dtst.Tables[0].Rows[i][5].ToString()),
                        (double)(-1) * double.Parse(dtst.Tables[0].Rows[i][10].ToString()),
                            dtst.Tables[0].Rows[i][6].ToString(),
                            long.Parse(dtst.Tables[0].Rows[i][0].ToString()));
                        }
                    }

                    //get control accnt id
                    int cntrlAcntID = int.Parse(Global.getGnrlRecNm("accb.accb_chart_of_accnts", "accnt_id", "control_account_id", int.Parse(dtst.Tables[0].Rows[i][9].ToString())));
                    if (cntrlAcntID > 0)
                    {
                        hsBnUpdt = Global.hsTrnsUptdAcntBls(
                          long.Parse(dtst.Tables[0].Rows[i][0].ToString()),
                        dtst.Tables[0].Rows[i][6].ToString(),
                          cntrlAcntID);

                        if (hsBnUpdt == false)
                        {
                            int cntrlAcntCurrID = int.Parse(Global.getGnrlRecNm(
                       "accb.accb_chart_of_accnts", "accnt_id", "crncy_id", cntrlAcntID));

                            double dbt1 = double.Parse(dtst.Tables[0].Rows[i][4].ToString());
                            double crdt1 = double.Parse(dtst.Tables[0].Rows[i][5].ToString());
                            double net1 = double.Parse(dtst.Tables[0].Rows[i][10].ToString());

                            if (funCurID != cntrlAcntCurrID && cntrlAcntCurrID == accntCurrID)
                            {
                                Global.postAccntCurrTransaction(cntrlAcntID,
                                 Global.getSign(dbt1) * accntCurrAmnt,
                                 Global.getSign(crdt1) * accntCurrAmnt,
                                 Global.getSign(net1) * accntCurrAmnt,
                                 dtst.Tables[0].Rows[i][6].ToString(),
                                 long.Parse(dtst.Tables[0].Rows[i][0].ToString()), accntCurrID);
                            }
                            Global.postTransaction(cntrlAcntID,
                             double.Parse(dtst.Tables[0].Rows[i][4].ToString()),
                             double.Parse(dtst.Tables[0].Rows[i][5].ToString()),
                             double.Parse(dtst.Tables[0].Rows[i][10].ToString()),
                             dtst.Tables[0].Rows[i][6].ToString(),
                             long.Parse(dtst.Tables[0].Rows[i][0].ToString()));
                        }
                    }
                    Global.chngeTrnsStatus(long.Parse(dtst.Tables[0].Rows[i][0].ToString()), "1");
                    Global.changeReconciledStatus(long.Parse(dtst.Tables[0].Rows[i][20].ToString()), "1");
                    Global.updateLogMsg(msg_id,
            "\r\nSuccessfully posted transaction ID= " + dtst.Tables[0].Rows[i][0].ToString()
            , log_tbl, dateStr, Global.rnUser_ID);
                    Global.updateLogMsg(msg_id1,
            "\r\nSuccessfully posted transaction ID= " + dtst.Tables[0].Rows[i][0].ToString()
            , log_tbl1, dateStr, Global.rnUser_ID);

                }
                //Call Accnts Chart Bals Update
                Program.reloadAcntChrtBals(glBatchID, net_accnt);
                Global.updateLogMsg(msg_id,
          "\r\nSuccessfully Reloaded Chart of Account Balances!"
          , log_tbl, dateStr, Global.rnUser_ID);

                Global.updateLogMsg(msg_id1,
        "\r\nSuccessfully Reloaded Chart of Account Balances!"
        , log_tbl1, dateStr, Global.rnUser_ID);

                aesum = Global.get_COA_AESum(Global.UsrsOrg_ID);
                crlsum = Global.get_COA_CRLSum(Global.UsrsOrg_ID);
                if (aesum
                 != crlsum)
                {
                    Global.updateLogMsg(msg_id,
              "\r\nBatch of Transactions caused an " +
                      "IMBALANCE in the Accounting! A+E=" + aesum +
                      "\r\nC+R+L=" + crlsum + "\r\nDiff=" + (aesum - crlsum) + " will be pushed to suspense Account", log_tbl, dateStr, Global.rnUser_ID);
                    Program.correctImblns();
                    Program.correctImblns();

                    /*string errmsg = "";
                    //if (Program.postIntoSuspnsAccnt((decimal)aesum, (decimal)crlsum, Global.UsrsOrg_ID, ref errmsg) == false)
                    //{
                    Global.updateLogMsg(msg_id,
            "\r\n" + errmsg + "\r\nProcess to undo the posted transactions " +
              "is about to start...!", log_tbl, dateStr, Global.rnUser_ID);
                    ////  "IMBALANCE in the Accounting !", 0);
                    for (int i = 0; i < ttltrns; i++)
                    {
                      Global.updtActnPrcss(5);
                      int accntCurrID = int.Parse(dtst.Tables[0].Rows[i][17].ToString());
                      int funcCurr = int.Parse(dtst.Tables[0].Rows[i][7].ToString());
                      double accntCurrAmnt = double.Parse(dtst.Tables[0].Rows[i][15].ToString());
                      string acctyp = Global.getAccntType(
                       int.Parse(dtst.Tables[0].Rows[i][9].ToString()));
                      bool hsBnUpdt = Global.hsTrnsUptdAcntBls(
                        long.Parse(dtst.Tables[0].Rows[i][0].ToString()),
                      dtst.Tables[0].Rows[i][6].ToString(),
                        int.Parse(dtst.Tables[0].Rows[i][9].ToString()));

                      if (hsBnUpdt == true)
                      {
                        double dbt1 = double.Parse(dtst.Tables[0].Rows[i][4].ToString());
                        double crdt1 = double.Parse(dtst.Tables[0].Rows[i][5].ToString());
                        double net1 = double.Parse(dtst.Tables[0].Rows[i][10].ToString());

                        if (funCurID != accntCurrID)
                        {
                          Global.undoPostAccntCurrTransaction(int.Parse(dtst.Tables[0].Rows[i][9].ToString()),
                           Global.getSign(dbt1) * accntCurrAmnt,
                           Global.getSign(crdt1) * accntCurrAmnt,
                           Global.getSign(net1) * accntCurrAmnt,
                           dtst.Tables[0].Rows[i][6].ToString(),
                           long.Parse(dtst.Tables[0].Rows[i][0].ToString()), accntCurrID);
                        }
                        Global.undoPostTransaction(int.Parse(dtst.Tables[0].Rows[i][9].ToString()),
                         double.Parse(dtst.Tables[0].Rows[i][4].ToString()),
                         double.Parse(dtst.Tables[0].Rows[i][5].ToString()),
                         double.Parse(dtst.Tables[0].Rows[i][10].ToString()),
                        dtst.Tables[0].Rows[i][6].ToString(),
                            long.Parse(dtst.Tables[0].Rows[i][0].ToString()));
                      }
                      hsBnUpdt = Global.hsTrnsUptdAcntBls(
                      long.Parse(dtst.Tables[0].Rows[i][0].ToString()),
                    dtst.Tables[0].Rows[i][6].ToString(),
                      net_accnt);
                      if (hsBnUpdt == true)
                      {
                        if (acctyp == "R")
                        {
                          Global.undoPostTransaction(net_accnt,
                      double.Parse(dtst.Tables[0].Rows[i][4].ToString()),
                      double.Parse(dtst.Tables[0].Rows[i][5].ToString()),
                      double.Parse(dtst.Tables[0].Rows[i][10].ToString()),
                         dtst.Tables[0].Rows[i][6].ToString(),
                             long.Parse(dtst.Tables[0].Rows[i][0].ToString()));
                        }
                        else if (acctyp == "EX")
                        {
                          Global.undoPostTransaction(net_accnt,
                      double.Parse(dtst.Tables[0].Rows[i][4].ToString()),
                      double.Parse(dtst.Tables[0].Rows[i][5].ToString()),
                      (double)(-1) * double.Parse(dtst.Tables[0].Rows[i][10].ToString()),
                         dtst.Tables[0].Rows[i][6].ToString(),
                             long.Parse(dtst.Tables[0].Rows[i][0].ToString()));
                        }
                      }

                      //get control accnt id
                      int cntrlAcntID = int.Parse(Global.getGnrlRecNm("accb.accb_chart_of_accnts", "accnt_id", "control_account_id", int.Parse(dtst.Tables[0].Rows[i][9].ToString())));
                      if (cntrlAcntID > 0)
                      {
                        hsBnUpdt = Global.hsTrnsUptdAcntBls(
                          long.Parse(dtst.Tables[0].Rows[i][0].ToString()),
                        dtst.Tables[0].Rows[i][6].ToString(),
                          cntrlAcntID);

                        if (hsBnUpdt == true)
                        {
                          int cntrlAcntCurrID = int.Parse(Global.getGnrlRecNm(
                "accb.accb_chart_of_accnts", "accnt_id", "crncy_id", cntrlAcntID));

                          double dbt1 = double.Parse(dtst.Tables[0].Rows[i][4].ToString());
                          double crdt1 = double.Parse(dtst.Tables[0].Rows[i][5].ToString());
                          double net1 = double.Parse(dtst.Tables[0].Rows[i][10].ToString());

                          if (funCurID != cntrlAcntCurrID && cntrlAcntCurrID == accntCurrID)
                          {
                            Global.undoPostAccntCurrTransaction(cntrlAcntID,
                             Global.getSign(dbt1) * accntCurrAmnt,
                             Global.getSign(crdt1) * accntCurrAmnt,
                             Global.getSign(net1) * accntCurrAmnt,
                             dtst.Tables[0].Rows[i][6].ToString(),
                             long.Parse(dtst.Tables[0].Rows[i][0].ToString()), accntCurrID);
                          }
                          Global.undoPostTransaction(cntrlAcntID,
                           double.Parse(dtst.Tables[0].Rows[i][4].ToString()),
                           double.Parse(dtst.Tables[0].Rows[i][5].ToString()),
                           double.Parse(dtst.Tables[0].Rows[i][10].ToString()),
                          dtst.Tables[0].Rows[i][6].ToString(),
                              long.Parse(dtst.Tables[0].Rows[i][0].ToString()));
                        }
                      }
                      Global.chngeTrnsStatus(long.Parse(dtst.Tables[0].Rows[i][0].ToString()), "0");
                      Global.updateLogMsg(msg_id,
            "\r\nSuccessfully unposted transaction ID= " + dtst.Tables[0].Rows[i][0].ToString()
            , log_tbl, dateStr, Global.rnUser_ID);
                    }
                    //}
                    //Call Accnts Chart Bals Update
                    Program.reloadAcntChrtBals(glBatchID, net_accnt);
                    Global.updateLogMsg(msg_id,
            "\r\nSuccessfully Reloaded Original Chart of Account Balances!"
            , log_tbl, dateStr, Global.rnUser_ID);
                    Program.correctImblns();

                    Global.updateRptRnStopCmd(Global.runID, "1");
                    Program.checkNClosePrgrm();*/
                }
                else
                {
                    Global.updateBatchStatus(glBatchID);
                    Global.updateLogMsg(msg_id,
            "\r\nBatch of Transactions POSTED SUCCESSFULLY!"
            , log_tbl, dateStr, Global.rnUser_ID);
                }
            }
            catch (Exception ex)
            {
                Global.updateLogMsg(msg_id,
        "\r\nError!" + ex.Message + "\r\n\r\n" + ex.InnerException + "\r\n\r\n" + ex.StackTrace
        , log_tbl, dateStr, Global.rnUser_ID);
                Global.errorLog = "\r\nError!" + ex.Message + "\r\n\r\n" + ex.InnerException + "\r\n\r\n" + ex.StackTrace;
                Global.writeToLog();
            }
        }

        private static void reloadAcntChrtBals(int netaccntid)
        {
            string dateStr = DateTime.ParseExact(
      Global.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
            DataSet dtst = Global.get_All_Chrt_Det(Global.UsrsOrg_ID);
            //DataSet dtst = Global.get_Batch_Accnts(btchid);
            //if (dateStr.Length > 10)
            //{
            //  dateStr = dateStr.Substring(0, 10);
            //}
            for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
            {
                string[] rslt = Global.getAccntLstDailyBalsInfo(
                  int.Parse(dtst.Tables[0].Rows[a][0].ToString()), dateStr);
                double lstNetBals = double.Parse(rslt[2]);
                double lstDbtBals = double.Parse(rslt[0]);
                double lstCrdtBals = double.Parse(rslt[1]);

                //Global.showMsg("Testing!" + rslt[2] + "\r\n" + rslt[3] + "\r\n" + dateStr, 0);
                Global.updtAcntChrtBals(int.Parse(dtst.Tables[0].Rows[a][0].ToString()),
                  lstDbtBals, lstCrdtBals, lstNetBals, rslt[3]);

                //get control accnt id
                int cntrlAcntID = int.Parse(Global.getGnrlRecNm("accb.accb_chart_of_accnts", "accnt_id", "control_account_id", int.Parse(dtst.Tables[0].Rows[a][0].ToString())));
                if (cntrlAcntID > 0)
                {
                    rslt = Global.getAccntLstDailyBalsInfo(
                 cntrlAcntID, dateStr);
                    lstNetBals = double.Parse(rslt[2]);
                    lstDbtBals = double.Parse(rslt[0]);
                    lstCrdtBals = double.Parse(rslt[1]);

                    //Global.showMsg("Testing!" + rslt[2] + "\r\n" + rslt[3] + "\r\n" + dateStr, 0);
                    Global.updtAcntChrtBals(cntrlAcntID,
                     lstDbtBals, lstCrdtBals, lstNetBals, rslt[3]);
                }
            }
            if (netaccntid > 0)
            {
                string[] rslt = Global.getAccntLstDailyBalsInfo(
                  netaccntid, dateStr);
                double lstNetBals = double.Parse(rslt[2]);
                double lstDbtBals = double.Parse(rslt[0]);
                double lstCrdtBals = double.Parse(rslt[1]);

                //Global.showMsg("Testing!" + rslt[2] + "\r\n" + rslt[3] + "\r\n" + dateStr, 0);
                Global.updtAcntChrtBals(netaccntid,
                  lstDbtBals, lstCrdtBals, lstNetBals, rslt[3]);
            }
        }

        private static void reloadOneAcntChrtBals(int accntID, int netaccntid)
        {
            string dateStr = DateTime.ParseExact(
      Global.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
            //DataSet dtst = Global.get_All_Chrt_Det(Global.Org_id);
            //DataSet dtst = Global.get_Batch_Accnts(btchid);
            //if (dateStr.Length > 10)
            //{
            //  dateStr = dateStr.Substring(0, 10);
            //}
            //for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
            //{
            string[] rslt = Global.getAccntLstDailyBalsInfo(accntID, dateStr);
            double lstNetBals = double.Parse(rslt[2]);
            double lstDbtBals = double.Parse(rslt[0]);
            double lstCrdtBals = double.Parse(rslt[1]);

            //Global.showMsg("Testing!" + rslt[2] + "\r\n" + rslt[3] + "\r\n" + dateStr, 0);
            Global.updtAcntChrtBals(accntID,
              lstDbtBals, lstCrdtBals, lstNetBals, rslt[3]);

            //get control accnt id
            int cntrlAcntID = int.Parse(Global.getGnrlRecNm("accb.accb_chart_of_accnts", "accnt_id", "control_account_id", accntID));
            if (cntrlAcntID > 0)
            {
                rslt = Global.getAccntLstDailyBalsInfo(
             cntrlAcntID, dateStr);
                lstNetBals = double.Parse(rslt[2]);
                lstDbtBals = double.Parse(rslt[0]);
                lstCrdtBals = double.Parse(rslt[1]);

                //Global.showMsg("Testing!" + rslt[2] + "\r\n" + rslt[3] + "\r\n" + dateStr, 0);
                Global.updtAcntChrtBals(cntrlAcntID,
                 lstDbtBals, lstCrdtBals, lstNetBals, rslt[3]);
            }
            //}
            if (netaccntid > 0)
            {
                rslt = Global.getAccntLstDailyBalsInfo(
                 netaccntid, dateStr);
                lstNetBals = double.Parse(rslt[2]);
                lstDbtBals = double.Parse(rslt[0]);
                lstCrdtBals = double.Parse(rslt[1]);

                //Global.showMsg("Testing!" + rslt[2] + "\r\n" + rslt[3] + "\r\n" + dateStr, 0);
                Global.updtAcntChrtBals(netaccntid,
                  lstDbtBals, lstCrdtBals, lstNetBals, rslt[3]);
            }
        }

        private static void reloadAcntChrtBals(long btchid, int netaccntid)
        {
            string dateStr = DateTime.ParseExact(
      Global.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
            //DataSet dtst = Global.get_All_Chrt_Det(Global.Org_id);
            DataSet dtst = Global.get_Batch_Accnts(btchid);
            //if (dateStr.Length > 10)
            //{
            //  dateStr = dateStr.Substring(0, 10);
            //}
            for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
            {
                string[] rslt = Global.getAccntLstDailyBalsInfo(
                  int.Parse(dtst.Tables[0].Rows[a][0].ToString()), dateStr);
                double lstNetBals = double.Parse(rslt[2]);
                double lstDbtBals = double.Parse(rslt[0]);
                double lstCrdtBals = double.Parse(rslt[1]);

                //Global.showMsg("Testing!" + rslt[2] + "\r\n" + rslt[3] + "\r\n" + dateStr, 0);
                Global.updtAcntChrtBals(int.Parse(dtst.Tables[0].Rows[a][0].ToString()),
                  lstDbtBals, lstCrdtBals, lstNetBals, rslt[3]);

                //get control accnt id
                int cntrlAcntID = int.Parse(Global.getGnrlRecNm("accb.accb_chart_of_accnts", "accnt_id", "control_account_id", int.Parse(dtst.Tables[0].Rows[a][0].ToString())));
                if (cntrlAcntID > 0)
                {
                    rslt = Global.getAccntLstDailyBalsInfo(
                 cntrlAcntID, dateStr);
                    lstNetBals = double.Parse(rslt[2]);
                    lstDbtBals = double.Parse(rslt[0]);
                    lstCrdtBals = double.Parse(rslt[1]);

                    //Global.showMsg("Testing!" + rslt[2] + "\r\n" + rslt[3] + "\r\n" + dateStr, 0);
                    Global.updtAcntChrtBals(cntrlAcntID,
                     lstDbtBals, lstCrdtBals, lstNetBals, rslt[3]);
                }
            }
            if (netaccntid > 0)
            {
                string[] rslt = Global.getAccntLstDailyBalsInfo(
                  netaccntid, dateStr);
                double lstNetBals = double.Parse(rslt[2]);
                double lstDbtBals = double.Parse(rslt[0]);
                double lstCrdtBals = double.Parse(rslt[1]);

                //Global.showMsg("Testing!" + rslt[2] + "\r\n" + rslt[3] + "\r\n" + dateStr, 0);
                Global.updtAcntChrtBals(netaccntid,
                  lstDbtBals, lstCrdtBals, lstNetBals, rslt[3]);
            }
        }

        private static bool sendJournalsToGL(DataSet dtst, string intrfcTblNme, int prcID, ref string errmsg)
        {
            try
            {
                if (Global.getEnbldPssblValID("NO", Global.getLovID("Allow Inventory to be Costed")) > 0)
                {
                    Global.zeroInterfaceValues(Global.UsrsOrg_ID);
                }

                Program.correctIntrfcImbals(intrfcTblNme);
                //Check if Dataset Trns are balanced before calling this func
                Global.updtActnPrcss(prcID);
                long cntr = dtst.Tables[0].Rows.Count;
                double dbtsum = 0;
                double crdtsum = 0;

                for (int y = 0; y < cntr; y++)
                {
                    dbtsum += double.Parse(dtst.Tables[0].Rows[y][2].ToString());
                    crdtsum += double.Parse(dtst.Tables[0].Rows[y][3].ToString());
                    Global.updtActnPrcss(prcID);
                }
                dbtsum = Math.Round(dbtsum, 2);
                crdtsum = Math.Round(crdtsum, 2);

                if (cntr == 0)
                {
                    errmsg += "Cannot Transfer Transactions to GL because\r\n" +
               " No Interface Transactions were found!";
                    return false;
                }

                if (dbtsum != crdtsum)
                {
                    errmsg += "Cannot Transfer Transactions to GL because\r\n" +
                      " Transactions in the GL Interface are not Balanced! Difference=" + Math.Abs(dbtsum - crdtsum).ToString();
                    return false;
                }
                //Get Todays GL Batch Name
                string dateStr = Global.getFrmtdDB_Date_time();
                string btchPrfx = "Internal Payments";
                if (intrfcTblNme == "scm.scm_gl_interface")
                {
                    btchPrfx = "Inventory";
                }
                Global.updtActnPrcss(prcID);
                string todaysGlBatch = btchPrfx + " (" + dateStr + ")";
                long todbatchid = Global.getTodaysGLBatchID(
                  todaysGlBatch, Global.UsrsOrg_ID);
                if (todbatchid <= 0)
                {
                    Global.createTodaysGLBatch(Global.UsrsOrg_ID,
                      todaysGlBatch, todaysGlBatch, btchPrfx);
                    todbatchid = Global.getTodaysGLBatchID(
                    todaysGlBatch,
                    Global.UsrsOrg_ID);
                    Global.updtActnPrcss(prcID);
                }
                if (todbatchid > 0)
                {
                    todaysGlBatch = Global.get_GLBatch_Nm(todbatchid);
                }

                /*
                 * 1. Get list of all accounts to transfer from the 
                 * interface table and their total amounts.
                 * 2. Loop through each and transfer
                 */
                //DataSet dtst = Global.getAllInGLIntrfcOrg(Global.UsrsOrg_ID);

                //dateStr = Global.getFrmtdDB_Date_time();
                Global.updtActnPrcss(prcID);
                for (int a = 0; a < cntr; a++)
                {
                    Global.updtActnPrcss(prcID);
                    string src_ids = Global.getGLIntrfcIDs(int.Parse(dtst.Tables[0].Rows[a][0].ToString()),
                      dtst.Tables[0].Rows[a][1].ToString(),
                      int.Parse(dtst.Tables[0].Rows[a][5].ToString()), intrfcTblNme);

                    double entrdAmnt = double.Parse(dtst.Tables[0].Rows[a][2].ToString()) == 0 ? double.Parse(dtst.Tables[0].Rows[a][3].ToString()) : double.Parse(dtst.Tables[0].Rows[a][2].ToString());
                    string dbtCrdt = double.Parse(dtst.Tables[0].Rows[a][3].ToString()) == 0 ? "D" : "C";
                    int accntCurrID = int.Parse(Global.getGnrlRecNm(
          "accb.accb_chart_of_accnts", "accnt_id", "crncy_id", int.Parse(dtst.Tables[0].Rows[a][0].ToString())));

                    double accntCurrRate = Math.Round(
                      Global.get_LtstExchRate(int.Parse(dtst.Tables[0].Rows[a][5].ToString()), accntCurrID,
              dtst.Tables[0].Rows[a][1].ToString()), 15);

                    //Check if dbtsum in intrfcids matchs the dbt amount been sent to gl

                    double[] actlAmnts = Global.getGLIntrfcIDAmntSum(src_ids, intrfcTblNme, int.Parse(dtst.Tables[0].Rows[a][0].ToString()));

                    if (actlAmnts[0] == double.Parse(dtst.Tables[0].Rows[a][2].ToString())
                      && actlAmnts[1] == double.Parse(dtst.Tables[0].Rows[a][3].ToString()))
                    {
                        Global.createPymntGLLine(int.Parse(dtst.Tables[0].Rows[a][0].ToString()),
                "Lumped sum of all payments (from the " + btchPrfx + " module) to this account",
                    double.Parse(dtst.Tables[0].Rows[a][2].ToString()),
                    dtst.Tables[0].Rows[a][1].ToString(),
                    int.Parse(dtst.Tables[0].Rows[a][5].ToString()), todbatchid,
                    double.Parse(dtst.Tables[0].Rows[a][3].ToString()),
                    double.Parse(dtst.Tables[0].Rows[a][4].ToString()), src_ids, dateStr,
                    entrdAmnt, int.Parse(dtst.Tables[0].Rows[a][5].ToString()),
                    entrdAmnt * accntCurrRate, accntCurrID,
                    1, accntCurrID, dbtCrdt);
                    }
                    else
                    {
                        errmsg += "Interface Transaction Amounts DR:" + actlAmnts[0] + " CR:" + actlAmnts[1] +
                          " \r\ndo not match Amount being sent to GL DR:" + double.Parse(dtst.Tables[0].Rows[a][2].ToString()) +
                          " CR:" + double.Parse(dtst.Tables[0].Rows[a][3].ToString()) + "!\r\n Interface Line IDs:" + src_ids;
                        break;
                    }
                }
                if (Global.get_Batch_CrdtSum(todbatchid) == Global.get_Batch_DbtSum(todbatchid))
                {
                    Global.updtActnPrcss(prcID);
                    Global.updtPymntAllGLIntrfcLnOrg(todbatchid, Global.UsrsOrg_ID, intrfcTblNme);
                    Global.updtActnPrcss(prcID);
                    Global.updtGLIntrfcLnSpclOrg(Global.UsrsOrg_ID, intrfcTblNme, btchPrfx);
                    Global.updtActnPrcss(prcID);
                    Global.updtTodaysGLBatchPstngAvlblty(todbatchid, "1");
                    return true;
                }
                else
                {
                    errmsg += "The GL Batch created is not Balanced!\r\nTransactions created will be reversed and deleted!";
                    Global.deleteBatchTrns(todbatchid);
                    Global.deleteBatch(todbatchid, todaysGlBatch);
                    return false;
                }
                //Global.updtPymntAllGLIntrfcLnOrg(todbatchid, Global.UsrsOrg_ID);
                //Global.updtGLIntrfcLnSpclOrg(Global.UsrsOrg_ID);
                //return true;
            }
            catch (Exception ex)
            {
                errmsg += "Error Sending Payment to GL!\r\n" + ex.Message;
                return false;
            }
        }

        //    private bool doSalesItmAccntng(long itmID, double qnty, string cnsgmntIDs,
        //   int txCodeID, int dscntCodeID, int chrgCodeID,
        //   string docTyp, long docID, long srcDocID, int dfltRcvblAcntID,
        //   int dfltInvAcntID, int dfltCGSAcntID, int dfltExpnsAcntID, int dfltRvnuAcntID,
        //   long stckID, double unitSllgPrc, int crncyID, long docLnID,
        //   int dfltSRAcntID, int dfltCashAcntID, int dfltCheckAcntID, long srcDocLnID,
        //   string dateStr, string docIDNum, int entrdCurrID, decimal exchngRate, int dfltLbltyAccnt, string strSrcDocType)
        //    {
        //      try
        //      {
        //        bool succs = true;
        //        /*For each Item in a Sales Invoice
        //         * 1. Get Items Consgnmnt Cost Prices using all selected consignments and their used qtys
        //         * 2. Decrease Inv Account by Cost Price --0Inventory
        //         * 3. Increase Cost of Goods Sold by Cost Price --0Inventory
        //         * 4. Get Selling Price, Taxes, Extra Charges, Discounts
        //         * 5. Get Net Selling Price = (Selling Price - Taxes - Extra Charges + Discounts)*Qty
        //         * 6. Increase Revenue Account by Net Selling Price --1Initial Amount
        //         * 7. Increase Receivables account by Net Selling price --1Initial Amount
        //         * 8. Increase Taxes Payable by Taxes  --2Tax
        //         * 9. Increase Receivables account by Taxes --2Tax
        //         * 10.Increase Extra Charges Revenue by Extra Charges --4Extra Charge
        //         * 11.Increase Receivables account by Extra Charges --4Extra Charge
        //         * 12.Increase Sales Discounts by Discounts --3Discount
        //         * 13.Decrease Receivables by Discounts --3Discount
        //         */
        //        int itmInvAcntID = -1;
        //        int itmCGSAcntID = -1;
        //        //For Sales Return, Item Issues-Unbilled Docs get the ff
        //        int itmExpnsAcntID = -1;
        //        //For Sales Invoice, Sales Return get the ff
        //        int itmRvnuAcntID = -1;
        //        //Genral
        //        int txPyblAcntID = -1;
        //        int chrgRvnuAcntID = -1;
        //        int salesDscntAcntID = -1;

        //        /*int.TryParse(Global.getGnrlRecNm("inv.inv_itm_list", "item_id", "inv_asset_acct_id", itmID), out itmInvAcntID);
        //        int.TryParse(Global.getGnrlRecNm("inv.inv_itm_list", "item_id", "cogs_acct_id", itmID), out itmCGSAcntID);
        //        //For Sales Return, Item Issues-Unbilled Docs get the ff
        //        int.TryParse(Global.getGnrlRecNm("inv.inv_itm_list", "item_id", "expense_accnt_id", itmID), out itmExpnsAcntID);
        //        //For Sales Invoice, Sales Return get the ff
        //        int.TryParse(Global.getGnrlRecNm("inv.inv_itm_list", "item_id", "sales_rev_accnt_id", itmID), out itmRvnuAcntID);
        //         * */
        //        //Genral
        //        int.TryParse(Global.getGnrlRecNm("scm.scm_tax_codes", "code_id", "taxes_payables_accnt_id", txCodeID), out txPyblAcntID);
        //        int.TryParse(Global.getGnrlRecNm("scm.scm_tax_codes", "code_id", "chrge_revnu_accnt_id", chrgCodeID), out chrgRvnuAcntID);
        //        int.TryParse(Global.getGnrlRecNm("scm.scm_tax_codes", "code_id", "dscount_expns_accnt_id", dscntCodeID), out salesDscntAcntID);
        //        if (itmInvAcntID > 0)
        //        {
        //          dfltInvAcntID = itmInvAcntID;
        //        }
        //        if (itmCGSAcntID > 0)
        //        {
        //          dfltCGSAcntID = itmCGSAcntID;
        //        }
        //        if (itmExpnsAcntID > 0)
        //        {
        //          dfltExpnsAcntID = itmExpnsAcntID;
        //        }
        //        if (itmRvnuAcntID > 0)
        //        {
        //          dfltRvnuAcntID = itmRvnuAcntID;
        //        }

        //        if (dfltRcvblAcntID <= 0
        //  || dfltInvAcntID <= 0
        //  || dfltCGSAcntID <= 0
        //  || dfltExpnsAcntID <= 0
        //  || dfltRvnuAcntID <= 0)
        //        {
        //          Global.showMsg("You must first Setup all Default " +
        //            "Accounts before Accounting can be Created!", 0);
        //          return false;
        //        }

        //        string itmType = Global.getGnrlRecNm("inv.inv_itm_list", "item_id", "item_type", itmID);
        //        //        string dateStr = DateTime.ParseExact(
        //        //Global.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
        //        //System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
        //        //     long SIDocID = -1;
        //        //     long.TryParse(Global.getGnrlRecNm("scm.scm_sales_invc_hdr",
        //        //"invc_hdr_id", "src_doc_hdr_id", docID),out SIDocID);
        //        //Create a List of Consignment IDs, Quantity Used in this doc, Cost Price
        //        //Get ttlSllngPrc, ttlTxAmnt, ttlChrgAmnt, ttlDsctAmnt for this item only

        //        double funcCurrrate = Math.Round((double)1 / (double)exchngRate, 15);

        //        double ttlSllngPrc = Math.Round(qnty * unitSllgPrc, 2);
        //        double orgnlSllngPrce = Math.Round(double.Parse(Global.getGnrlRecNm(
        //          "scm.scm_sales_invc_det", "invc_det_ln_id", "orgnl_selling_price", docLnID)), 2);
        //        double snglDscnt = Math.Round(Global.getSalesDocCodesAmnt(
        //          dscntCodeID, orgnlSllngPrce, 1), 2);

        //        double ttlTxAmnt = Math.Round(Global.getSalesDocCodesAmnt(txCodeID, orgnlSllngPrce - snglDscnt, qnty), 2);
        //        double ttlChrgAmnt = Math.Round(Global.getSalesDocCodesAmnt(chrgCodeID, orgnlSllngPrce, qnty), 2);
        //        double ttlDsctAmnt = Math.Round(Global.getSalesDocCodesAmnt(
        //          dscntCodeID, orgnlSllngPrce, qnty), 2);
        //        string txCodeNm = Global.getGnrlRecNm(
        //                "scm.scm_tax_codes", "code_id", "code_name",
        //                txCodeID);
        //        string chrgCodeNm = Global.getGnrlRecNm(
        //            "scm.scm_tax_codes", "code_id", "code_name",
        //            chrgCodeID);
        //        string dscntCodeNm = Global.getGnrlRecNm(
        //          "scm.scm_tax_codes", "code_id", "code_name",
        //          dscntCodeID);
        //        //Get Net Selling Price = Selling Price - Taxes - Extra Charges + Discounts
        //        double ttlRvnuAmnt = ttlSllngPrc - ttlTxAmnt - ttlChrgAmnt;// +ttlDsctAmnt;
        //        //For Sales Invoice, Sales Return, Item Issues-Unbilled Docs get the ff
        //        if (itmType.Contains("Inventory")
        //          || itmType.Contains("Fixed Assets"))
        //        {
        //          List<string[]> csngmtData;
        //          if (docTyp != "Sales Return")
        //          {
        //            csngmtData = Global.getItmCnsgmtVals(qnty, cnsgmntIDs);
        //          }
        //          else
        //          {
        //            csngmtData = Global.getSRItmCnsgmtVals(
        //              docLnID, qnty, cnsgmntIDs, srcDocLnID);
        //          }
        //          //From the List get Total Cost Price of the Item
        //          string csgmtQtyDists = ",";
        //          for (int i = 0; i < csngmtData.Count; i++)
        //          {
        //            string[] ary = csngmtData[i];
        //            long figID = 0;
        //            long.TryParse(ary[0], out figID);
        //            double fig1Qty = 0;
        //            double fig2Prc = 0;
        //            double.TryParse(ary[1], out fig1Qty);
        //            double.TryParse(ary[2], out fig2Prc);
        //            csgmtQtyDists = csgmtQtyDists + fig1Qty.ToString() + ",";
        //            if (docTyp == "Sales Order")
        //            {
        //              Global.postCnsgnmntQty(figID, 0, fig1Qty, -1 * fig1Qty, dateStr, "SO" + docLnID.ToString());
        //              Global.postStockQty(stckID, 0, fig1Qty, -1 * fig1Qty, dateStr, "SO" + docLnID.ToString());
        //            }
        //            else if (docTyp == "Sales Invoice")
        //            {
        //              Global.postCnsgnmntQty(figID, -1 * fig1Qty, 0, -1 * fig1Qty, dateStr, "SI" + docLnID.ToString());
        //              Global.postStockQty(stckID, -1 * fig1Qty, 0, -1 * fig1Qty, dateStr, "SI" + docLnID.ToString());
        //            }
        //            else if (docTyp == "Item Issue-Unbilled")
        //            {
        //              Global.postCnsgnmntQty(figID, -1 * fig1Qty, 0, -1 * fig1Qty, dateStr, "IU" + docLnID.ToString());
        //              Global.postStockQty(stckID, -1 * fig1Qty, 0, -1 * fig1Qty, dateStr, "IU" + docLnID.ToString());
        //            }
        //            else if (docTyp == "Sales Return")
        //            {
        //              Global.postCnsgnmntQty(figID, fig1Qty, 0, fig1Qty, dateStr, "SR" + docLnID.ToString());
        //              Global.postStockQty(stckID, fig1Qty, 0, fig1Qty, dateStr, "SR" + docLnID.ToString());
        //            }
        //          }
        //          Global.updateSalesLnCsgmtDist(docLnID, csgmtQtyDists.Trim(','));
        //          double ttlCstPrice = 0;
        //          for (int i = 0; i < csngmtData.Count; i++)
        //          {
        //            string[] ary = csngmtData[i];
        //            double fig1Qty = 0;
        //            double fig2Prc = 0;
        //            double.TryParse(ary[1], out fig1Qty);
        //            double.TryParse(ary[2], out fig2Prc);
        //            ttlCstPrice += fig1Qty * fig2Prc;
        //          }
        //          if (dfltInvAcntID > 0 && dfltCGSAcntID > 0 && docTyp == "Sales Invoice")
        //          {
        //            succs = this.sendToGLInterfaceMnl(
        //              dfltInvAcntID, "D", ttlCstPrice, dateStr,
        //               "Sale of Inventory Items", crncyID, dateStr,
        //               docTyp, docID, docLnID);
        //            if (!succs)
        //            {
        //              return succs;
        //            }
        //            succs = this.sendToGLInterfaceMnl(dfltCGSAcntID, "I", ttlCstPrice, dateStr,
        //                "Sale of Inventory Items", crncyID, dateStr,
        //                docTyp, docID, docLnID);
        //            if (!succs)
        //            {
        //              return succs;
        //            }
        //          }
        //          else if (dfltInvAcntID > 0 && dfltCGSAcntID > 0 && docTyp == "Sales Return" && strSrcDocType == "Sales Invoice")
        //          {
        //            succs = this.sendToGLInterfaceMnl(dfltInvAcntID, "I", ttlCstPrice, dateStr,
        //              "Return of Sold Inventory Items", crncyID, dateStr,
        //              docTyp, docID, docLnID);
        //            if (!succs)
        //            {
        //              return succs;
        //            }
        //            succs = this.sendToGLInterfaceMnl(dfltCGSAcntID, "D", ttlCstPrice, dateStr,
        //              "Return of Sold Inventory Items", crncyID, dateStr,
        //              docTyp, docID, docLnID);
        //            if (!succs)
        //            {
        //              return succs;
        //            }
        //          }
        //          else if (docTyp == "Item Issue-Unbilled")
        //          {
        //            if (dfltInvAcntID > 0 && dfltExpnsAcntID > 0)
        //            {
        //              succs = this.sendToGLInterfaceMnl(dfltInvAcntID, "D", ttlCstPrice, dateStr,
        //                "Issue Out of Inventory Items", crncyID, dateStr,
        //                docTyp, docID, docLnID);
        //              if (!succs)
        //              {
        //                return succs;
        //              }
        //              succs = this.sendToGLInterfaceMnl(dfltExpnsAcntID, "I", ttlCstPrice, dateStr,
        //                "Issue Out of Inventory Items", crncyID, dateStr,
        //                docTyp, docID, docLnID);
        //              if (!succs)
        //              {
        //                return succs;
        //              }
        //            }
        //          }
        //          else if (docTyp == "Sales Return" && strSrcDocType == "Item Issue-Unbilled")
        //          {
        //            if (dfltInvAcntID > 0 && dfltExpnsAcntID > 0)
        //            {
        //              succs = this.sendToGLInterfaceMnl(dfltInvAcntID, "I", ttlCstPrice, dateStr,
        //                "Return of Inventory Items Issued Out", crncyID, dateStr,
        //                docTyp, docID, docLnID);
        //              if (!succs)
        //              {
        //                return succs;
        //              }
        //              succs = this.sendToGLInterfaceMnl(dfltExpnsAcntID, "D", ttlCstPrice, dateStr,
        //                "Return of Inventory Items Issued Out", crncyID, dateStr,
        //                docTyp, docID, docLnID);
        //              if (!succs)
        //              {
        //                return succs;
        //              }
        //            }
        //          }
        //        }

        //        if (docTyp == "Sales Invoice")
        //        {
        //          if (dfltRvnuAcntID > 0 && dfltRcvblAcntID > 0)
        //          {
        //            int accntCurrID = int.Parse(Global.getGnrlRecNm(
        //"accb.accb_chart_of_accnts", "accnt_id", "crncy_id", dfltRvnuAcntID));
        //            double accntCurrRate = Math.Round(
        //            Global.get_LtstExchRate(entrdCurrID, accntCurrID,
        //            this.docDteTextBox.Text + " 00:00:00"), 15);

        //            Global.createScmRcvblsDocDet(docID, "1Initial Amount",
        //    "Revenue from Sales Invoice (" + docIDNum + ")",
        //    ttlRvnuAmnt, entrdCurrID, -1, docTyp, false, "Increase", dfltRvnuAcntID,
        //    "Increase", dfltRcvblAcntID, -1, "VALID", -1, this.curid, accntCurrID,
        //    funcCurrrate, accntCurrRate, Math.Round(funcCurrrate * ttlRvnuAmnt, 2),
        //    Math.Round(accntCurrRate * ttlRvnuAmnt, 2));
        //          }
        //          if (txPyblAcntID > 0 && dfltRcvblAcntID > 0)
        //          {
        //            int accntCurrID = int.Parse(Global.getGnrlRecNm(
        // "accb.accb_chart_of_accnts", "accnt_id", "crncy_id", txPyblAcntID));
        //            double accntCurrRate = Math.Round(
        //            Global.get_LtstExchRate(entrdCurrID, accntCurrID,
        //            this.docDteTextBox.Text + " 00:00:00"), 15);

        //            Global.createScmRcvblsDocDet(docID, "2Tax",
        //    "Taxes (" + txCodeNm + ") on Sales Invoice (" + docIDNum + ")",
        //    ttlTxAmnt, entrdCurrID, txCodeID, docTyp, false, "Increase", txPyblAcntID,
        //    "Increase", dfltRcvblAcntID, -1, "VALID", -1, this.curid, accntCurrID,
        //    funcCurrrate, accntCurrRate, Math.Round(funcCurrrate * ttlTxAmnt, 2),
        //    Math.Round(accntCurrRate * ttlTxAmnt, 2));
        //          }

        //          if (chrgRvnuAcntID > 0 && dfltRcvblAcntID > 0)
        //          {
        //            int accntCurrID = int.Parse(Global.getGnrlRecNm(
        // "accb.accb_chart_of_accnts", "accnt_id", "crncy_id", chrgRvnuAcntID));
        //            double accntCurrRate = Math.Round(
        //            Global.get_LtstExchRate(entrdCurrID, accntCurrID,
        //            this.docDteTextBox.Text + " 00:00:00"), 15);

        //            Global.createScmRcvblsDocDet(docID, "4Extra Charge",
        //    "Extra Charges (" + chrgCodeNm + ") on Sales Invoice (" + docIDNum + ")",
        //    ttlChrgAmnt, entrdCurrID, chrgCodeID, docTyp, false, "Increase", chrgRvnuAcntID,
        //    "Increase", dfltRcvblAcntID, -1, "VALID", -1, this.curid, accntCurrID,
        //    funcCurrrate, accntCurrRate, Math.Round(funcCurrrate * ttlChrgAmnt, 2),
        //    Math.Round(accntCurrRate * ttlChrgAmnt, 2));
        //          }

        //          if (salesDscntAcntID > 0 && dfltRcvblAcntID > 0)
        //          {
        //            int accntCurrID = int.Parse(Global.getGnrlRecNm(
        //    "accb.accb_chart_of_accnts", "accnt_id", "crncy_id", salesDscntAcntID));
        //            double accntCurrRate = Math.Round(
        //            Global.get_LtstExchRate(entrdCurrID, accntCurrID,
        //            this.docDteTextBox.Text + " 00:00:00"), 15);

        //            Global.createScmRcvblsDocDet(docID, "3Discount",
        //    "Discounts (" + dscntCodeNm + ") on Sales Invoice (" + docIDNum + ")",
        //    ttlDsctAmnt, entrdCurrID, dscntCodeID, docTyp, false, "Increase", salesDscntAcntID,
        //    "Decrease", dfltRcvblAcntID, -1, "VALID", -1, this.curid, accntCurrID,
        //    funcCurrrate, accntCurrRate, Math.Round(funcCurrrate * ttlDsctAmnt, 2),
        //    Math.Round(accntCurrRate * ttlDsctAmnt, 2));
        //          }
        //        }
        //        else if (docTyp == "Sales Return" && strSrcDocType == "Sales Invoice")
        //        {
        //          if (dfltRvnuAcntID > 0 && dfltLbltyAccnt > 0)
        //          {
        //            int accntCurrID = int.Parse(Global.getGnrlRecNm(
        //"accb.accb_chart_of_accnts", "accnt_id", "crncy_id", dfltRvnuAcntID));
        //            double accntCurrRate = Math.Round(
        //            Global.get_LtstExchRate(entrdCurrID, accntCurrID,
        //            this.docDteTextBox.Text + " 00:00:00"), 15);

        //            Global.createScmRcvblsDocDet(docID, "1Initial Amount",
        //    "Refund from Sales Return (" + docIDNum + ")",
        //    ttlRvnuAmnt, entrdCurrID, -1, docTyp, false, "Decrease", dfltRvnuAcntID,
        //    "Increase", dfltLbltyAccnt, -1, "VALID", -1, this.curid, accntCurrID,
        //    funcCurrrate, accntCurrRate, Math.Round(funcCurrrate * ttlRvnuAmnt, 2),
        //    Math.Round(accntCurrRate * ttlRvnuAmnt, 2));
        //          }
        //          if (txPyblAcntID > 0 && dfltLbltyAccnt > 0)
        //          {
        //            int accntCurrID = int.Parse(Global.getGnrlRecNm(
        // "accb.accb_chart_of_accnts", "accnt_id", "crncy_id", txPyblAcntID));
        //            double accntCurrRate = Math.Round(
        //            Global.get_LtstExchRate(entrdCurrID, accntCurrID,
        //            this.docDteTextBox.Text + " 00:00:00"), 15);

        //            Global.createScmRcvblsDocDet(docID, "2Tax",
        //    "Refund Taxes (" + txCodeNm + ") on Sales Return (" + docIDNum + ")",
        //    ttlTxAmnt, entrdCurrID, txCodeID, docTyp, false, "Decrease", txPyblAcntID,
        //    "Increase", dfltLbltyAccnt, -1, "VALID", -1, this.curid, accntCurrID,
        //    funcCurrrate, accntCurrRate, Math.Round(funcCurrrate * ttlTxAmnt, 2),
        //    Math.Round(accntCurrRate * ttlTxAmnt, 2));
        //          }

        //          if (chrgRvnuAcntID > 0 && dfltRcvblAcntID > 0)
        //          {
        //            int accntCurrID = int.Parse(Global.getGnrlRecNm(
        // "accb.accb_chart_of_accnts", "accnt_id", "crncy_id", chrgRvnuAcntID));
        //            double accntCurrRate = Math.Round(
        //            Global.get_LtstExchRate(entrdCurrID, accntCurrID,
        //            this.docDteTextBox.Text + " 00:00:00"), 15);

        //            Global.createScmRcvblsDocDet(docID, "4Extra Charge",
        //    "Refund Extra Charges (" + chrgCodeNm + ") on Sales Return (" + docIDNum + ")",
        //    ttlChrgAmnt, entrdCurrID, chrgCodeID, docTyp, false, "Decrease", chrgRvnuAcntID,
        //    "Increase", dfltLbltyAccnt, -1, "VALID", -1, this.curid, accntCurrID,
        //    funcCurrrate, accntCurrRate, Math.Round(funcCurrrate * ttlChrgAmnt, 2),
        //    Math.Round(accntCurrRate * ttlChrgAmnt, 2));
        //          }

        //          if (salesDscntAcntID > 0 && dfltRcvblAcntID > 0)
        //          {
        //            int accntCurrID = int.Parse(Global.getGnrlRecNm(
        //    "accb.accb_chart_of_accnts", "accnt_id", "crncy_id", salesDscntAcntID));
        //            double accntCurrRate = Math.Round(
        //            Global.get_LtstExchRate(entrdCurrID, accntCurrID,
        //            this.docDteTextBox.Text + " 00:00:00"), 15);

        //            Global.createScmRcvblsDocDet(docID, "3Discount",
        //    "Take Back Discounts (" + dscntCodeNm + ") on Sales Return (" + docIDNum + ")",
        //    ttlDsctAmnt, entrdCurrID, dscntCodeID, docTyp, false, "Decrease", salesDscntAcntID,
        //    "Decrease", dfltLbltyAccnt, -1, "VALID", -1, this.curid, accntCurrID,
        //    funcCurrrate, accntCurrRate, Math.Round(funcCurrrate * ttlDsctAmnt, 2),
        //    Math.Round(accntCurrRate * ttlDsctAmnt, 2));
        //          }
        //        }
        //        return succs;
        //      }
        //      catch (Exception ex)
        //      {
        //        Global.showMsg(ex.InnerException + "\r\n" + ex.StackTrace + "\r\n" + ex.Message, 0);
        //        return false;
        //      }
        //    }

        //    private void approveSalesInvoice()
        //    {
        //      invcAmnt = Global.getScmRcvblsDocGrndAmnt(long.Parse(this.docIDTextBox.Text));

        //        if (accounted)
        //        {
        //          bool apprvlSccs = true;
        //          string rcvblDocNum = "";
        //          string rcvblDocType = "";
        //          //string srcDocType = Global.getGnrlRecNm("scm.scm_sales_invc_hdr", "invc_hdr_id", "invc_type", long.Parse(this.srcDocIDTextBox.Text));

        //          if (this.docTypeComboBox.Text == "Sales Invoice")
        //          {
        //            rcvblDocNum = "CSP" +
        //          Global.getLtstRcvblsIDNoInPrfx("CSP") + "-" +
        //Global.getFrmtdDB_Date_time().Substring(12, 8).Replace(":", "") + "-" +
        //        Global.getLtstRecPkID("accb.accb_rcvbls_invc_hdr",
        //        "rcvbls_invc_hdr_id");
        //            rcvblDocType = "Customer Standard Payment";
        //            Global.createRcvblsDocHdr(Global.Org_id, this.docDteTextBox.Text,
        //              rcvblDocNum, rcvblDocType, this.docCommentsTextBox.Text,
        //              long.Parse(this.docIDTextBox.Text), int.Parse(this.cstmrIDTextBox.Text),
        //              int.Parse(this.siteIDTextBox.Text), "Not Validated", "Approve",
        //              invcAmnt, this.payTermsTextBox.Text, this.docTypeComboBox.Text,
        //              int.Parse(this.pymntMthdIDTextBox.Text), 0, -1, "",
        //              "Payment of Customer Goods Delivered", int.Parse(this.invcCurrIDTextBox.Text), 0);
        //          }
        //          else if (this.docTypeComboBox.Text == "Sales Return" && srcDocType == "Sales Invoice")
        //          {
        //            rcvblDocNum = "CDM-IR" +
        // Global.getLtstRcvblsIDNoInPrfx("CDM-IR") + "-" +
        //Global.getFrmtdDB_Date_time().Substring(12, 8).Replace(":", "") + "-" +
        //Global.getLtstRecPkID("accb.accb_rcvbls_invc_hdr",
        //"rcvbls_invc_hdr_id");
        //            rcvblDocType = "Customer Debit Memo (InDirect Refund)";

        //            Global.createRcvblsDocHdr(Global.Org_id, this.docDteTextBox.Text,
        //              rcvblDocNum, rcvblDocType, this.docCommentsTextBox.Text,
        //              long.Parse(this.docIDTextBox.Text), int.Parse(this.cstmrIDTextBox.Text),
        //              int.Parse(this.siteIDTextBox.Text), "Not Validated", "Approve",
        //              invcAmnt, this.payTermsTextBox.Text, this.docTypeComboBox.Text,
        //              int.Parse(this.pymntMthdIDTextBox.Text), 0, -1, "",
        //              "Refund-Return of Goods Supplied", int.Parse(this.invcCurrIDTextBox.Text), 0);
        //          }
        //          long rcvblDocID = Global.getGnrlRecID("accb.accb_rcvbls_invc_hdr",
        //            "rcvbls_invc_number", "rcvbls_invc_hdr_id", rcvblDocNum, Global.Org_id);
        //          if (rcvblDocID > 0 && rcvblDocType != "")
        //          {
        //            DataSet dtstSmmry = Global.get_ScmRcvblsDocDets(long.Parse(this.docIDTextBox.Text));
        //            for (int i = 0; i < dtstSmmry.Tables[0].Rows.Count; i++)
        //            {
        //              long curlnID = Global.getNewRcvblsLnID();
        //              string lineType = dtstSmmry.Tables[0].Rows[i][0].ToString();
        //              string lineDesc = dtstSmmry.Tables[0].Rows[i][1].ToString();
        //              double entrdAmnt = double.Parse(dtstSmmry.Tables[0].Rows[i][2].ToString());
        //              int entrdCurrID = int.Parse(dtstSmmry.Tables[0].Rows[i][10].ToString());
        //              int codeBhnd = int.Parse(dtstSmmry.Tables[0].Rows[i][3].ToString());
        //              string docType = rcvblDocType;
        //              bool autoCalc = Global.cnvrtBitStrToBool(dtstSmmry.Tables[0].Rows[i][4].ToString());
        //              string incrDcrs1 = dtstSmmry.Tables[0].Rows[i][5].ToString();
        //              int costngID = int.Parse(dtstSmmry.Tables[0].Rows[i][6].ToString());
        //              string incrDcrs2 = dtstSmmry.Tables[0].Rows[i][7].ToString();
        //              int blncgAccntID = int.Parse(dtstSmmry.Tables[0].Rows[i][8].ToString());
        //              long prepayDocHdrID = long.Parse(dtstSmmry.Tables[0].Rows[i][9].ToString());
        //              string vldyStatus = "VALID";
        //              long orgnlLnID = -1;
        //              int funcCurrID = int.Parse(dtstSmmry.Tables[0].Rows[i][11].ToString());
        //              int accntCurrID = int.Parse(dtstSmmry.Tables[0].Rows[i][12].ToString());
        //              double funcCurrRate = double.Parse(dtstSmmry.Tables[0].Rows[i][13].ToString());
        //              double accntCurrRate = double.Parse(dtstSmmry.Tables[0].Rows[i][14].ToString());
        //              double funcCurrAmnt = double.Parse(dtstSmmry.Tables[0].Rows[i][15].ToString());
        //              double accntCurrAmnt = double.Parse(dtstSmmry.Tables[0].Rows[i][16].ToString());
        //              Global.createRcvblsDocDet(curlnID, rcvblDocID, lineType,
        //                            lineDesc, entrdAmnt, entrdCurrID, codeBhnd, docType, autoCalc, incrDcrs1,
        //                            costngID, incrDcrs2, blncgAccntID, prepayDocHdrID, vldyStatus, orgnlLnID, funcCurrID,
        //                            accntCurrID, funcCurrRate, accntCurrRate, funcCurrAmnt, accntCurrAmnt);
        //            }
        //            this.reCalcRcvblsSmmrys(rcvblDocID, rcvblDocType);
        //            apprvlSccs = this.approveRcvblsDoc(rcvblDocID, rcvblDocNum);
        //          }
        //          if (apprvlSccs)
        //          {
        //            Global.updtRcvblsDocApprvl(rcvblDocID, "Approved", "Cancel");
        //            Global.updtSalesDocApprvl(long.Parse(this.docIDTextBox.Text), "Approved", "Cancel");
        //            this.apprvlStatusTextBox.Text = "Approved";
        //            this.nxtApprvlStatusButton.Text = "Cancel";
        //            this.nxtApprvlStatusButton.ImageKey = "90.png";
        //            this.disableDetEdit();
        //            this.disableLnsEdit();
        //            this.populateDet(long.Parse(this.docIDTextBox.Text));
        //            this.populateLines(long.Parse(this.docIDTextBox.Text), this.docTypeComboBox.Text);
        //            this.populateSmmry(long.Parse(this.docIDTextBox.Text), this.docTypeComboBox.Text);
        //          }
        //          else
        //          {
        //            this.rvrsApprval(dateStr);
        //            Global.deleteRcvblsDocHdrNDet(rcvblDocID, rcvblDocNum);
        //            this.saveLabel.Visible = false;
        //            System.Windows.Forms.Application.DoEvents();
        //            return;
        //          }
        //        }
        //        else
        //        {
        //          this.rvrsApprval(dateStr);
        //          this.saveLabel.Visible = false;
        //          System.Windows.Forms.Application.DoEvents();
        //          return;
        //        }

        //        this.saveLabel.Visible = false;
        //        System.Windows.Forms.Application.DoEvents();
        //        if (this.payDocs && this.docTypeComboBox.Text == "Sales Invoice")
        //        {
        //          this.processPayButton_Click(this.processPayButton, e);
        //        }

        //    }
        // 

        private static void correctIntrfcImbals(string intrfcTblNm)
        {

            int suspns_accnt = Global.get_Suspns_Accnt(Global.UsrsOrg_ID);
            DataSet dteDtSt = Global.get_Intrfc_dateSums(intrfcTblNm, Global.UsrsOrg_ID);
            if (dteDtSt.Tables[0].Rows.Count > 0 && suspns_accnt > 0)
            {
                string msg1 = @"";
                for (int i = 0; i < dteDtSt.Tables[0].Rows.Count; i++)
                {
                    double dlyDbtAmnt = double.Parse(dteDtSt.Tables[0].Rows[i][1].ToString());
                    double dlyCrdtAmnt = double.Parse(dteDtSt.Tables[0].Rows[i][2].ToString());
                    int orgID = Global.UsrsOrg_ID;
                    if (dlyDbtAmnt
                     != dlyCrdtAmnt)
                    {
                        //long suspns_batch_id = glBatchID;
                        int funcCurrID = Global.getOrgFuncCurID(orgID);
                        decimal dffrnc = (decimal)(dlyDbtAmnt - dlyCrdtAmnt);
                        string incrsDcrs = "D";
                        if (dffrnc < 0)
                        {
                            incrsDcrs = "I";
                        }
                        decimal imbalAmnt = Math.Abs(dffrnc);
                        double netAmnt = (double)Global.dbtOrCrdtAccntMultiplier(suspns_accnt,
                   incrsDcrs) * (double)imbalAmnt;
                        string dateStr1 = DateTime.ParseExact(dteDtSt.Tables[0].Rows[i][0].ToString(), "yyyy-MM-dd",
            System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy") + " 00:00:00";
                        //if (!Global.mnFrm.cmCde.isTransPrmttd(suspns_accnt,
                        //      dateStr, netAmnt))
                        //{
                        //  return; ;
                        //}

                        /*double netamnt = 0;

                        netamnt = Global.mnFrm.cmCde.dbtOrCrdtAccntMultiplier(
                          int.Parse(this.accntIDTextBox.Text),
                          this.incrsDcrsComboBox.Text.Substring(0, 1)) * (double)this.funcCurAmntNumUpDwn.Value;
                        */
                        string dateStr = Global.getFrmtdDB_Date_time();

                        if (Global.getIntrfcTrnsID(intrfcTblNm, suspns_accnt, netAmnt,
                          dteDtSt.Tables[0].Rows[i][0].ToString() + " 00:00:00") > 0)
                        {
                            continue;
                        }

                        if (Global.dbtOrCrdtAccnt(suspns_accnt,
                          incrsDcrs) == "Debit")
                        {
                            if (intrfcTblNm == "scm.scm_gl_interface")
                            {
                                Global.createScmGLIntFcLn(suspns_accnt,
                        "Correction of Imbalance in GL Interface Table as at " + dateStr1,
                            (double)imbalAmnt, dateStr1,
                            funcCurrID, 0,
                            netAmnt, "Imbalance Correction", -1, -1, dateStr, "USR");
                            }
                            else
                            {
                                Global.createPayGLIntFcLn(suspns_accnt,
                        "Correction of Imbalance in GL Interface Table as at " + dateStr1,
                            (double)imbalAmnt, dateStr1,
                            funcCurrID, 0,
                            netAmnt, dateStr, "USR");
                            }

                        }
                        else
                        {

                            if (intrfcTblNm == "scm.scm_gl_interface")
                            {
                                Global.createScmGLIntFcLn(suspns_accnt,
                                       "Correction of Imbalance in GL Interface Table as at " + dateStr1,
                                     0, dateStr1,
                                     funcCurrID, (double)imbalAmnt,
                                     netAmnt, "Imbalance Correction", -1, -1, dateStr, "USR");
                            }
                            else
                            {
                                Global.createPayGLIntFcLn(suspns_accnt,
                        "Correction of Imbalance in GL Interface Table as at " + dateStr1,
                            (double)imbalAmnt, dateStr1,
                            funcCurrID, 0,
                            netAmnt, dateStr, "USR");
                            }
                        }

                        /*if (Global.dbtOrCrdtAccnt(suspns_accnt, incrsDcrs) == "Debit")
                        {
                          Global.createTransaction(suspns_accnt,
                              "Correction of Imbalance in GL Batch " + Global.getGnrlRecNm("accb.accb_trnsctn_batches",
                              "batch_id", "batch_name", glBatchID) + " as at " + dateStr1, (double)imbalAmnt,
                              dateStr1
                              , funcCurrID, suspns_batch_id, 0.00, netAmnt,
                            (double)imbalAmnt,
                            funcCurrID,
                            (double)imbalAmnt,
                            funcCurrID,
                            (double)1,
                            (double)1, "D");
                        }
                        else
                        {
                          Global.createTransaction(suspns_accnt,
                          "Correction of Imbalance in GL Batch " + Global.getGnrlRecNm("accb.accb_trnsctn_batches",
                              "batch_id", "batch_name", glBatchID) + " as at " + dateStr1, 0.00,
                          dateStr1, funcCurrID,
                          suspns_batch_id, (double)imbalAmnt, netAmnt,
                      (double)imbalAmnt,
                      funcCurrID,
                      (double)imbalAmnt,
                      funcCurrID,
                      (double)1,
                      (double)1, "C");
                        }*/
                    }

                    //msg1 = msg1 + dteDtSt.Tables[0].Rows[i][0].ToString() + "\t DR=" + 
                    //dteDtSt.Tables[0].Rows[i][1].ToString() + "\t CR=" + 
                    //dteDtSt.Tables[0].Rows[i][2].ToString() + "\r\n";
                }
                //Global.mnFrm.cmCde.showMsg(msg1, 4);
                //return;
            }
            else
            {
                //Global.mnFrm.cmCde.showMsg("There's no Imbalance to correct!", 0);
                //return;
            }
        }

        private static void correctImblns()
        {
            try
            {
                int suspns_accnt = Global.get_Suspns_Accnt(Global.UsrsOrg_ID);
                if (suspns_accnt <= -1)
                {
                    //Global.showMsg("Please define a suspense Account First!", 0);
                    return;
                }
                int ret_accnt = Global.get_Rtnd_Erngs_Accnt(Global.UsrsOrg_ID);
                int net_accnt = Global.get_Net_Income_Accnt(Global.UsrsOrg_ID);
                if (ret_accnt == -1)
                {
                    //Global.showMsg("Until a Retained Earnings Account is defined\r\n no Transaction can be posted into the Accounting!", 0);
                    return;
                }
                if (net_accnt == -1)
                {
                    //Global.showMsg("Until a Net Income Account is defined\r\n no Transaction can be posted into the Accounting!", 0);
                    return;
                }
                //bool isAnyRnng = true;
                //do
                //{
                //  isAnyRnng = Global.isThereANActvActnPrcss("1,2,3,4,5,6", "10 second");
                //}
                //while (isAnyRnng == true);
                //bool isAnyRnng = true;
                //int witcntr = 0;
                //do
                //{
                //  witcntr++;
                //  isAnyRnng = Global.isThereANActvActnPrcss("1,2,3,4,5,6", "10 second");
                //  System.Windows.Forms.Application.DoEvents();
                //}
                //while (isAnyRnng == true);

                /*PROCEDURE FOR RELOADING ACCOUNT BALANCES
          1. Correct all Trns Det Net Balance Amount
          2. Get all wrong daily bals values
                 */
                Global.updtActnPrcss(5, 90);
                DataSet dtst = Global.get_WrongNetBalncs(Global.UsrsOrg_ID);

                for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
                {
                    double netAmnt = double.Parse(dtst.Tables[0].Rows[i][8].ToString());
                    long trnsID = long.Parse(dtst.Tables[0].Rows[i][0].ToString());
                    string updtSQL = @"UPDATE accb.accb_trnsctn_details 
SET net_amount=" + netAmnt + @" 
   WHERE transctn_id=" + trnsID;
                    Global.updateDataNoParams(updtSQL);

                    Global.updtActnPrcss(5, 90);
                }

                dtst = Global.get_WrongBalncs(Global.UsrsOrg_ID);
                for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
                {
                    Global.updtActnPrcss(5, 30);

                    string acctyp = Global.getAccntType(
                     int.Parse(dtst.Tables[0].Rows[i][1].ToString()));

                    double dbt1 = double.Parse(dtst.Tables[0].Rows[i][4].ToString());
                    double crdt1 = double.Parse(dtst.Tables[0].Rows[i][5].ToString());
                    double net1 = double.Parse(dtst.Tables[0].Rows[i][6].ToString());


                    Global.postTransaction(int.Parse(dtst.Tables[0].Rows[i][1].ToString()),
                     dbt1,
                     crdt1,
                     net1,
                     dtst.Tables[0].Rows[i][7].ToString(), -993);


                    if (acctyp == "R")
                    {
                        Global.postTransaction(net_accnt,
                       dbt1,
                       crdt1,
                       net1,
                        dtst.Tables[0].Rows[i][7].ToString(), -993);
                    }
                    else if (acctyp == "EX")
                    {
                        Global.postTransaction(net_accnt,
                       dbt1,
                       crdt1,
                    (double)(-1) * net1,
                        dtst.Tables[0].Rows[i][7].ToString(), -993);
                    }


                    //get control accnt id
                    int cntrlAcntID = int.Parse(Global.getGnrlRecNm("accb.accb_chart_of_accnts", "accnt_id", "control_account_id",
                      int.Parse(dtst.Tables[0].Rows[i][1].ToString())));
                    if (cntrlAcntID > 0)
                    {
                        Global.postTransaction(cntrlAcntID,
                       dbt1,
                       crdt1,
                       net1,
                         dtst.Tables[0].Rows[i][7].ToString(), -993);

                    }
                    //this.reloadOneAcntChrtBals(int.Parse(dtst.Tables[0].Rows[i][1].ToString()), net_accnt);
                }

                Global.updtActnPrcss(5, 50);
                Program.reloadAcntChrtBals(net_accnt);

                dtst = Global.get_WrongNetIncmBalncs(Global.UsrsOrg_ID);
                for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
                {
                    Global.updtActnPrcss(5, 30);
                    string acctyp = Global.getAccntType(
                     int.Parse(dtst.Tables[0].Rows[i][1].ToString()));

                    double dbt1 = double.Parse(dtst.Tables[0].Rows[i][4].ToString());
                    double crdt1 = double.Parse(dtst.Tables[0].Rows[i][5].ToString());
                    double net1 = double.Parse(dtst.Tables[0].Rows[i][6].ToString());


                    Global.postTransaction(int.Parse(dtst.Tables[0].Rows[i][1].ToString()),
                     dbt1,
                     crdt1,
                     net1,
                     dtst.Tables[0].Rows[i][7].ToString(), -993);



                    //this.reloadOneAcntChrtBals(int.Parse(dtst.Tables[0].Rows[i][1].ToString()), net_accnt);
                }

                Global.updtActnPrcss(5, 50);
                Program.reloadOneAcntChrtBals(net_accnt, net_accnt);

                string errmsg = "";
                decimal aesum = (decimal)Global.get_COA_AESum(Global.UsrsOrg_ID);
                decimal crlsum = (decimal)Global.get_COA_CRLSum(Global.UsrsOrg_ID);
                if (aesum
                 != crlsum)
                {
                    Global.updtActnPrcss(5, 10);
                    if (Program.postIntoSuspnsAccnt(aesum,
                      crlsum, Global.UsrsOrg_ID, false, ref errmsg) == false
                      && errmsg != "")
                    {
                        // Global.showMsg(errmsg, 0);
                    }
                }

                Program.reloadOneAcntChrtBals(suspns_accnt, net_accnt);


                Global.updtActnPrcss(5, 1);
            }
            catch (Exception ex)
            {

            }
        }

    }
}
