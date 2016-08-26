using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.Net.NetworkInformation;
using System.Drawing;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.VisualBasic;
using System.IO;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Drawing.Layout;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;

namespace REMSProcessRunner
{
    class Global
    {
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetProcessWorkingSetSize(IntPtr process,
            UIntPtr minimumWorkingSetSize, UIntPtr maximumWorkingSetSize);

        [DllImport("psapi.dll")]
        static extern int EmptyWorkingSet(IntPtr hwProc);

        public static string Hostnme = "";
        public static string Portnum = "";
        public static string Uname = "";
        public static string Pswd = "";
        public static string Dbase = "";
        public static int pid = -1;
        public static bool mustStop = false;

        public static string errorLog = "";
        public static string rnnrsBasDir = "";
        public static string dataBasDir = "";
        public static string callngAppType = "DESKTOP";
        public static NpgsqlConnection globalSQLConn = new NpgsqlConnection();
        public static string AppKey = "ROMeRRTRREMhbnsdGeneral KeyZzfor Rhomi|com Systems "
    + "Tech. !Ltd Enterpise/Organization @763542ERPorbjkSOFTWARE"
    + "asdbhi68103weuikTESTfjnsdfRSTLU../";

        private static Microsoft.Office.Interop.Excel.Application exclApp = null;
        private static Excel.Workbook nwWrkBk = null;
        private static Excel.Worksheet[] trgtSheets = new Excel.Worksheet[1];
        private static Microsoft.Office.Interop.Excel.Range dataRng = null;
        public static int UsrsOrg_ID = -1;
        public static long runID = -1;
        public static long rnUser_ID = -1;
        public static long ovrllDataCnt = 0;
        public static float oldoffsetY = 0;
        public static float hgstOffsetY = 0;
        public static int pageNo = 1;

        public static long logMsgID = -1;
        public static string logTbl = "";
        public static string gnrlDateStr = "";

        public static Thread threadNine = null;
        public static Thread threadTen = null;
        public static StringBuilder strSB = new StringBuilder("");
        public static string connStr = "";
        public static string appStatPath = "";
        public static string[] sysParaIDs = { "-130", "-140", "-150", "-160", "-170", "-180", "-190", "-200" };
        public static string[] sysParaNames = { "Report Title:", "Cols Nos To Group or Width & Height (Px) for Charts:",
                                          "Cols Nos To Count or Use in Charts:", "Columns To Sum:", "Columns To Average:",
                                          "Columns To Format Numerically:", "Report Output Formats", "Report Orientations" };


        #region "GENERAL SQL FUNCTIONS..."
        /// <summary>
        /// Processes select statements passed to it
        /// </summary>
        ///
        public static DataSet selectDataNoParams(string selSql)
        {
            DataSet selDtSt = new DataSet();
            try
            {
                NpgsqlConnection mycon = new NpgsqlConnection();
                mycon.ConnectionString = Global.connStr;
                mycon.Open();
                NpgsqlDataAdapter selDtAdpt = new NpgsqlDataAdapter();
                NpgsqlCommand selCmd = new NpgsqlCommand(@selSql, mycon);
                selDtAdpt.SelectCommand = selCmd;
                selDtAdpt.Fill(selDtSt, "table_1");
                mycon.Close();
                return selDtSt;
            }
            catch (Exception ex)
            {
                Global.errorLog = selSql + "\r\n" + "\r\n\r\n";
                Global.writeToLog();
                return selDtSt;
            }
            finally
            {
            }
        }

        /// <summary>
        /// Processes delete statements passed to it
        /// </summary>
        public static void deleteDataNoParams(string delSql)
        {
            try
            {
                NpgsqlConnection mycon = new NpgsqlConnection();
                mycon.ConnectionString = Global.connStr;
                mycon.Open();
                NpgsqlDataAdapter delDtAdpt = new NpgsqlDataAdapter();
                NpgsqlCommand delCmd = new NpgsqlCommand(@delSql, mycon);
                delDtAdpt.DeleteCommand = delCmd;
                delCmd.ExecuteNonQuery();
                //Global.storeAdtTrailInfo(delSql, 1);
                mycon.Close();
                return;
            }
            catch (Exception ex)
            {
                Global.errorLog = delSql + "\r\n" + "\r\n\r\n";
                Global.writeToLog();
            }
            finally
            {
            }
        }

        /// <summary>
        /// Processes insert statements passed to it
        /// </summary>
        public static void insertDataNoParams(string insSql)
        {
            try
            {
                NpgsqlConnection mycon = new NpgsqlConnection();
                mycon.ConnectionString = Global.connStr;
                mycon.Open();
                NpgsqlDataAdapter insDtAdpt = new NpgsqlDataAdapter();
                NpgsqlCommand insCmd = new NpgsqlCommand(@insSql, mycon);
                insDtAdpt.InsertCommand = insCmd;
                insCmd.ExecuteNonQuery();
                mycon.Close();
                return;
            }
            catch (Exception ex)
            {
                Global.errorLog = insSql + "\r\n" + "\r\n\r\n";
                Global.writeToLog();
            }//.Replace(@"\", @"\\")
            finally
            {
            }
        }

        /// <summary>
        /// Processes update statements passed to it
        /// </summary>
        public static void updateDataNoParams(string updtSql)
        {
            try
            {
                NpgsqlConnection mycon = new NpgsqlConnection();
                mycon.ConnectionString = Global.connStr;
                mycon.Open();
                NpgsqlDataAdapter updtDtAdpt = new NpgsqlDataAdapter();
                NpgsqlCommand updtCmd = new NpgsqlCommand(@updtSql, mycon);
                updtDtAdpt.UpdateCommand = updtCmd;
                updtCmd.ExecuteNonQuery();
                mycon.Close();
                //Global.storeAdtTrailInfo(updtSql, 0);
                return;
            }
            catch (Exception ex)
            {
                Global.errorLog = updtSql + "\r\n" + "\r\n\r\n";
                Global.writeToLog();
            }//.Replace(@"\", @"\\")
            finally
            {
            }
        }

        public static void executeGnrlSQL(string genSql)
        {
            try
            {
                NpgsqlConnection mycon = new NpgsqlConnection();
                mycon.ConnectionString = Global.connStr;
                mycon.Notice += new NoticeEventHandler(mycon_Notice);
                mycon.Notification += new NotificationEventHandler(mycon_Notification);
                mycon.Open();
                NpgsqlCommand gnrlCmd = new NpgsqlCommand(@genSql, mycon);
                gnrlCmd.ExecuteNonQuery();
                mycon.Close();
                //Global.errorLog = genSql + "\r\n" + "\r\n\r\n";
                //Global.writeToLog();
                return;
            }
            catch (Exception ex)
            {
                Global.errorLog = genSql + "\r\n" + "\r\n\r\n";
                Global.writeToLog();
            }//.Replace(@"\", @"\\")
            finally
            {
            }
        }

        public static void mycon_Notification(object sender, NpgsqlNotificationEventArgs e)
        {
            try
            {
                Global.updateLogMsg(Global.logMsgID,
            "\r\n" + e.Condition + ": " + e.AdditionalInformation + "\r\n",
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

        public static void mycon_Notice(object sender, NpgsqlNoticeEventArgs e)
        {
            try
            {
                Global.updateLogMsg(Global.logMsgID,
        "\r\n" + e.Notice.Message + ": " + e.Notice.Detail + "\r\n",
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

        #endregion

        static void MinimizeFootprint()
        {
            EmptyWorkingSet(Process.GetCurrentProcess().Handle);
        }

        public static void minimizeMemory()
        {
            try
            {
                GC.Collect(GC.MaxGeneration);
                GC.WaitForPendingFinalizers();
                SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle,
                    (UIntPtr)0xFFFFFFFF, (UIntPtr)0xFFFFFFFF);
                MinimizeFootprint();
            }
            catch (Exception ex)
            {
            }
        }

        public static void writeToLog()
        {
            try
            {
                //Global.errorLog = ex.Message + "\r\n" + ex.StackTrace + "\r\n" + ex.InnerException;
                StreamWriter fileWriter;
                string fileLoc = Global.rnnrsBasDir + @"\log_files\";
                //string fileLoc =Global.rnnrsBasDir;
                fileLoc += "ErrorLog" + Global.runID.ToString().Replace("-", "Neg") + "_" + DateTime.Now.ToString("yyyyMMddHH") + ".rho";

                fileWriter = new StreamWriter(fileLoc, true);
                //fileWriter. = txt.(fileLoc);
                fileWriter.WriteLine(Global.errorLog);
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

        public static void updateLogMsg(long msgid, string logmsg,
            string logTblNm, string dateStr, long userID)
        {
            string updtSQL = "UPDATE " + logTblNm + " " +
            "SET log_messages=log_messages || '" + logmsg.Replace("'", "''") +
            "', last_update_by=" + userID +
            ", last_update_date='" + dateStr +
            "' WHERE msg_id = " + msgid;
            Global.updateDataNoParams(updtSQL);
        }

        public static int findArryIdx(string[] in_arry1, string srch)
        {
            for (int i = 0; i < in_arry1.Length; i++)
            {
                if (in_arry1[i] == srch)
                {
                    return i;
                }
            }
            return -1;
        }

        public static long getNewMsgSentID()
        {
            //string strSql = "select nextval('accb.accb_trnsctn_batches_batch_id_seq'::regclass);";
            string strSql = "select nextval('alrt.alrt_msgs_sent_msg_sent_id_seq')";
            DataSet dtst = Global.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public static DataSet get_RptRun_Det(long rptRunID)
        {
            string strSql = @"SELECT run_by, run_date, rpt_run_output, run_status_txt, 
       run_status_prct, report_id, rpt_rn_param_ids, rpt_rn_param_vals, 
       output_used, orntn_used, last_actv_date_tme, is_this_from_schdler, 
       shld_run_stop, alert_id, msg_sent_id
  FROM rpt.rpt_report_runs WHERE rpt_run_id = " + rptRunID;
            DataSet dtst = Global.selectDataNoParams(strSql);
            return dtst;
        }

        public static DataSet get_RptDet(long rptID)
        {
            string strSql = @"SELECT report_name, report_desc, rpt_sql_query, owner_module, 
       created_by, rpt_or_sys_prcs, is_enabled, cols_to_group, cols_to_count, cols_to_sum, 
       cols_to_average, cols_to_no_frmt, output_type, portrait_lndscp, 
       rpt_layout, imgs_col_nos, csv_delimiter, process_runner, is_seeded_rpt " +
       "FROM rpt.rpt_reports WHERE report_id = " + rptID;
            DataSet dtst = Global.selectDataNoParams(strSql);
            return dtst;
        }

        public static DataSet get_AlertDet(long alertID)
        {
            string strSql = @"SELECT alert_name, alert_desc, to_mail_num_list_mnl, cc_mail_num_list_mnl, 
       alert_msg_body_mnl, alert_type, created_by, is_enabled, msg_sbjct_mnl, bcc_mail_num_list_mnl, 
       paramtr_sets_gnrtn_sql, report_id, shd_rpt_be_run, start_dte_tme, 
       repeat_uom, repeat_every, run_at_spcfd_hour, attchment_urls, 
       end_hour " +
       "FROM alrt.alrt_alerts WHERE alert_id = " + alertID;
            DataSet dtst = Global.selectDataNoParams(strSql);
            return dtst;
        }

        public static DataSet get_MsgSentDet(long msgSentID)
        {
            string strSql = @"SELECT to_list, cc_list, msg_body, date_sent, msg_sbjct, 
       report_id, bcc_list, person_id, cstmr_spplr_id, created_by, creation_date, 
       alert_id, sending_status, err_msg, attch_urls, msg_type  
  FROM alrt.alrt_msgs_sent WHERE msg_sent_id = " + msgSentID;
            DataSet dtst = Global.selectDataNoParams(strSql);
            return dtst;
        }

        public static string get_Rpt_SQL(long rptID)
        {
            string strSql = "SELECT rpt_sql_query " +
       "FROM rpt.rpt_reports WHERE report_id = " + rptID;

            DataSet dtst = Global.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return dtst.Tables[0].Rows[0][0].ToString();
            }
            return "";
        }

        public static string get_Alert_SQL(long alertID)
        {
            string strSql = "SELECT paramtr_sets_gnrtn_sql " +
       "FROM alrt.alrt_alerts WHERE alert_id = " + alertID;

            DataSet dtst = Global.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return dtst.Tables[0].Rows[0][0].ToString();
            }
            return "";
        }

        public static void updateRptRn(long rptrnid, string statustxt, int statusprcnt)
        {
            string dateStr = Global.getDB_Date_time();
            string updtSQL = "UPDATE rpt.rpt_report_runs SET " +
                     "run_status_txt = '" + statustxt.Replace("'", "''") +
                     "', run_status_prct = " + statusprcnt +
             " WHERE (rpt_run_id = " + rptrnid + ")";
            Global.updateDataNoParams(updtSQL);
        }

        public static void updateRptRnActvTme(long rptrnid, string lstAtvTme)
        {
            //string dateStr = Global.getDB_Date_time();
            string updtSQL = "UPDATE rpt.rpt_report_runs SET " +
                     "last_actv_date_tme = '" + lstAtvTme.Replace("'", "''") +
                     "' WHERE (rpt_run_id = " + rptrnid + ")";
            Global.updateDataNoParams(updtSQL);
        }

        public static void updateRptRnOutpt(long rptrnid, string outputTxt)
        {
            string dateStr = Global.getDB_Date_time();
            string updtSQL = "UPDATE rpt.rpt_report_runs SET " +
                     "rpt_run_output = '" + outputTxt.Replace("'", "''") +
             "' WHERE (rpt_run_id = " + rptrnid + ")";
            Global.updateDataNoParams(updtSQL);
        }

        public static int getLovID(string lovName)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = @"SELECT value_list_id from gst.gen_stp_lov_names 
      where (value_list_name = '" +
             lovName.Replace("'", "''") + "')";
            dtSt = Global.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public static string getOrgName(int orgid)
        {
            //Example username 'admin'
            DataSet dtSt = new DataSet();
            string sqlStr = "select org_name from org.org_details where org_id = " +
             orgid + "";
            dtSt = Global.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public static string getOrgPstlAddrs(int orgid)
        {
            //Example username 'admin'
            DataSet dtSt = new DataSet();
            string sqlStr = "select pstl_addrs from org.org_details where org_id = " +
             orgid + "";
            dtSt = Global.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public static string getOrgEmailAddrs(int orgid)
        {
            //Example username 'admin'
            DataSet dtSt = new DataSet();
            string sqlStr = "select email_addrsses from org.org_details where org_id = " +
             orgid + "";
            dtSt = Global.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public static string getOrgContactNos(int orgid)
        {
            //Example username 'admin'
            DataSet dtSt = new DataSet();
            string sqlStr = "select cntct_nos from org.org_details where org_id = " +
             orgid + "";
            dtSt = Global.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public static string getOrgWebsite(int orgid)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select websites from org.org_details where org_id = " +
             orgid + "";
            dtSt = Global.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public static string getOrgSlogan(int orgid)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select org_slogan from org.org_details where org_id = " +
             orgid + "";
            dtSt = Global.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public static int getOrgFuncCurID(int orgid)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select oprtnl_crncy_id from org.org_details where org_id = " +
             orgid + "";
            dtSt = Global.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public static string getOrgImgsDrctry()
        {
            return Global.dataBasDir + @"/Org";
            //\\172.25.10.96\bog_applsys project\RICHARD\Images\Org
            //if (Global.callngAppType == "DESKTOP")
            ////{
            //DataSet dtSt = new DataSet();
            //string sqlStr = "select pssbl_value from gst.gen_stp_lov_values where ((value_list_id = " +
            // Global.getLovID("Organization Images Directory") + ") AND (is_enabled='1')) ORDER BY pssbl_value_id DESC LIMIT 1";
            //dtSt = Global.selectDataNoParams(sqlStr);
            //if (dtSt.Tables[0].Rows.Count > 0)
            //{
            //  if (System.IO.Directory.Exists(dtSt.Tables[0].Rows[0][0].ToString()))
            //  {
            //    return dtSt.Tables[0].Rows[0][0].ToString().Replace("\\", "/");
            //  }
            //  else
            //  {
            //    return Global.appStatPath + @"/log_files";
            //  }
            //}
            //else
            //{
            //  return Global.appStatPath + @"/log_files";
            //}
            //}
            //else
            //{
            //  return Global.dataBasDir + @"/Org";
            //}
        }

        public static string getRptDrctry()
        {
            return Global.dataBasDir + @"/Rpts";
            ////if (Global.callngAppType == "DESKTOP")
            ////{
            //\\172.25.10.96\bog_applsys project\RICHARD\Images\Org
            //DataSet dtSt = new DataSet();
            //string sqlStr = "select pssbl_value from gst.gen_stp_lov_values where ((value_list_id = " +
            // Global.getLovID("Reports Directory") + ") AND (is_enabled='1')) ORDER BY pssbl_value_id DESC LIMIT 1";
            //dtSt = Global.selectDataNoParams(sqlStr);
            //if (dtSt.Tables[0].Rows.Count > 0)
            //{
            //  if (System.IO.Directory.Exists(dtSt.Tables[0].Rows[0][0].ToString()))
            //  {
            //    return dtSt.Tables[0].Rows[0][0].ToString().Replace("\\", "/");
            //  }
            //  else
            //  {
            //    return Global.appStatPath + @"/log_files";
            //  }
            //}
            //else
            //{
            //  return Global.appStatPath + @"/log_files";
            //}
            //}
            //else
            //{
            //  return Global.dataBasDir + @"/Rpts";
            //}
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

        public static string breakTxtDownHTML(string inptTxt, int allwdWidth)
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

        public static string[] breakTxtDown(string inptTxt, float allwdWidth, Font fnt, Graphics g)
        {
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

        public static string[] breakPDFTxtDown(string inptTxt, float allwdWidth, Font fnt, Graphics g)
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

        public static string[] breakRptTxtDown(string inptTxt, float allwdWidth, Font fnt, Graphics g)
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

        public static void clearPrvExclFiles()
        {
            try
            {
                Global.dataRng = null;
                Global.trgtSheets = new Excel.Worksheet[1];
                if (Global.nwWrkBk != null)
                {
                    Global.nwWrkBk.Close(false, Type.Missing, Type.Missing);
                    //Global.nwWrkBk = new Excel.Workbook();
                    Global.nwWrkBk = null;
                }
                if (Global.exclApp != null)
                {
                    Global.exclApp.Quit();
                    Global.exclApp = null;
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                Global.minimizeMemory();
            }
            catch
            {
            }
        }

        public static void exprtDtStSaved(DataSet dtst, string exlfileNm, string rptTitle
          , string[] colsToGrp, string[] colsToCnt,
          string[] colsToSum, string[] colsToAvrg, string[] colsToFrmt
          , bool isfirst, bool islast, bool shdAppnd, string orntnUsd)
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
            string cption = "";

            if (isfirst)
            {
                Global.clearPrvExclFiles();
                Global.exclApp = new Microsoft.Office.Interop.Excel.Application();
                //Global.exclApp.WindowState = Excel.XlWindowState.xlNormal;
                Global.exclApp.AlertBeforeOverwriting = false;
                Global.exclApp.Visible = false;
                Global.exclApp.ScreenUpdating = false;
                Global.exclApp.DisplayAlerts = false;

                Global.nwWrkBk = Global.exclApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                Global.nwWrkBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
                Global.trgtSheets = new Excel.Worksheet[1];

                Global.trgtSheets[0] = (Excel.Worksheet)Global.nwWrkBk.Worksheets[1];

                try
                {

                    Global.trgtSheets[0].PageSetup.CenterVertically = false;
                    Global.trgtSheets[0].PageSetup.CenterHorizontally = true;
                    Global.trgtSheets[0].PageSetup.TopMargin = Global.exclApp.CentimetersToPoints(0.70);
                    Global.trgtSheets[0].PageSetup.LeftMargin = Global.exclApp.CentimetersToPoints(0.20);
                    Global.trgtSheets[0].PageSetup.RightMargin = Global.exclApp.CentimetersToPoints(0.20);
                    Global.trgtSheets[0].PageSetup.BottomMargin = Global.exclApp.CentimetersToPoints(0.20);

                    //Footer and Header Margins

                    Global.trgtSheets[0].PageSetup.HeaderMargin = Global.exclApp.CentimetersToPoints(0.05);
                    Global.trgtSheets[0].PageSetup.FooterMargin = Global.exclApp.CentimetersToPoints(0.05);
                    //Global.trgtSheets[0].PageSetup.PrintArea = "$A$1:$U$" + (rowsDtSt.Tables[0].Rows.Count + 16).ToString();
                    //Global.trgtSheets[0].PageSetup.TopMargin = 20.5;
                    //Global.trgtSheets[0].PageSetup.BottomMargin = 20.5;
                    //Global.trgtSheets[0].PageSetup.LeftMargin = 4.5;
                    //Global.trgtSheets[0].PageSetup.RightMargin = 4.5;
                    if (orntnUsd.ToLower().Contains("landscape"))
                    {
                        Global.trgtSheets[0].PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape;
                    }
                    else
                    {
                        Global.trgtSheets[0].PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait;
                    }
                    Global.trgtSheets[0].PageSetup.FitToPagesWide = 1;
                    //Global.trgtSheets[0].PageSetup.FitToPagesTall = 1000;
                    Global.trgtSheets[0].PageSetup.ScaleWithDocHeaderFooter = true;
                    // Global.trgtSheets[0].PageSetup.Zoom = 50;
                    Global.trgtSheets[0].PageSetup.PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperA4;
                }
                catch (Exception ex)
                {
                }

                Global.trgtSheets[0].get_Range("B1:E1", Type.Missing).MergeCells = true;
                Global.trgtSheets[0].get_Range("B1:E1", Type.Missing).Value2 = Global.getOrgName(Global.UsrsOrg_ID);
                Global.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Bold = true;
                Global.trgtSheets[0].get_Range("B1:E1", Type.Missing).Font.Size = 13;
                //Global.trgtSheets[0].get_Range("B1:E1", Type.Missing).HorizontalAlignment
                Global.trgtSheets[0].get_Range("B1:E1", Type.Missing).WrapText = true;

                Global.trgtSheets[0].get_Range("B2:E2", Type.Missing).MergeCells = true;
                Global.trgtSheets[0].get_Range("B2:E2", Type.Missing).Value2 = Global.getOrgPstlAddrs(Global.UsrsOrg_ID).ToUpper().Replace("\r\n", " ");
                Global.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Bold = true;
                Global.trgtSheets[0].get_Range("B2:E2", Type.Missing).Font.Size = 13;
                Global.trgtSheets[0].get_Range("B2:E2", Type.Missing).WrapText = true;

                Global.trgtSheets[0].get_Range("B3:E3", Type.Missing).MergeCells = true;
                Global.trgtSheets[0].get_Range("B3:E3", Type.Missing).Value2 = Global.getOrgContactNos(Global.UsrsOrg_ID).ToUpper().Replace("\r\n", " ");
                Global.trgtSheets[0].get_Range("B3:E3", Type.Missing).Font.Bold = true;
                Global.trgtSheets[0].get_Range("B3:E3", Type.Missing).Font.Size = 13;
                Global.trgtSheets[0].get_Range("B3:E3", Type.Missing).WrapText = true;

                Global.trgtSheets[0].get_Range("A4:F4", Type.Missing).MergeCells = true;

                Global.trgtSheets[0].get_Range("A4:F4", Type.Missing).Value2 = rptTitle.ToUpper();
                //Global.trgtSheets[0].get_Range("C3:Q3", Type.Missing).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
                Global.trgtSheets[0].get_Range("A4:F4", Type.Missing).Font.Bold = true;
                Global.trgtSheets[0].get_Range("A4:F4", Type.Missing).Font.Size = 14;
                Global.trgtSheets[0].get_Range("A4:F4", Type.Missing).WrapText = false;
                Global.trgtSheets[0].get_Range("A4:F4", Type.Missing).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                Global.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 9;
                Global.trgtSheets[0].Shapes.AddPicture(Global.getOrgImgsDrctry() + @"\" + Global.UsrsOrg_ID + ".png",
                    Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1, 50, 50);
            }
            int totl = dtst.Tables[0].Rows.Count;
            int offst = 7;
            if (isfirst)
            {
                offst = 5;
            }
            int colIdx = 0;
            for (int a = 0; a < dtst.Tables[0].Columns.Count; a++)
            {
                int colLen = dtst.Tables[0].Columns[a].ColumnName.Length;
                if (colLen >= 3)
                {
                    ((Microsoft.Office.Interop.Excel.Range)Global.trgtSheets[0].Cells[Global.ovrllDataCnt + offst, (colIdx + 1)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
                    ((Microsoft.Office.Interop.Excel.Range)Global.trgtSheets[0].Cells[Global.ovrllDataCnt + offst, (colIdx + 1)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
                    ((Microsoft.Office.Interop.Excel.Range)Global.trgtSheets[0].Cells[Global.ovrllDataCnt + offst, (colIdx + 1)]).Font.Bold = true;
                    ((Microsoft.Office.Interop.Excel.Range)Global.trgtSheets[0].Cells[Global.ovrllDataCnt + offst, (colIdx + 1)]).Value2 = dtst.Tables[0].Columns[a].ColumnName.ToUpper();
                    ((Microsoft.Office.Interop.Excel.Range)Global.trgtSheets[0].Cells[Global.ovrllDataCnt + offst, (colIdx + 1)]).ColumnWidth = dtst.Tables[0].Columns[a].ColumnName.ToUpper().Length;
                    colIdx++;
                }
            }
            for (int i = 0; i < totl; i++)
            {
                colIdx = 0;
                for (int a = 0; a < dtst.Tables[0].Columns.Count; a++)
                {
                    int colLen = dtst.Tables[0].Columns[a].ColumnName.Length;
                    if (colLen < 3)
                    {
                        continue;
                    }
                    double nwval = 0;
                    bool mstgrp = Global.mustColBeGrpd(a.ToString(), colsToGrp);
                    if (Global.mustColBeCntd(a.ToString(), colsToCnt) == true)
                    {
                        if (Global.mustColBeFrmtd(a.ToString(), colsToFrmt) == true)
                        {
                            ((Microsoft.Office.Interop.Excel.Range)Global.trgtSheets[0].Cells[(i + Global.ovrllDataCnt + offst + 1), (colIdx + 1)]).NumberFormat = "#,##0.00_);[Red](#,##0.00)";
                        }

                        if ((i > 0) && (dtst.Tables[0].Rows[i - 1][a].ToString()
                        == dtst.Tables[0].Rows[i][a].ToString())
                        && (mstgrp == true))
                        {
                        }
                        else
                        {
                            colcntVals[a] += 1;
                            ((Microsoft.Office.Interop.Excel.Range)Global.trgtSheets[0].Cells[(i + Global.ovrllDataCnt + offst + 1), (colIdx + 1)]).Value2 = dtst.Tables[0].Rows[i][a].ToString();
                        }
                    }
                    else if (Global.mustColBeSumd(a.ToString(), colsToSum) == true)
                    {
                        if (Global.mustColBeFrmtd(a.ToString(), colsToFrmt) == true)
                        {
                            ((Microsoft.Office.Interop.Excel.Range)Global.trgtSheets[0].Cells[(i + Global.ovrllDataCnt + offst + 1), (colIdx + 1)]).NumberFormat = "#,##0.00_);[Red](#,##0.00)";
                        }
                        double.TryParse(dtst.Tables[0].Rows[i][a].ToString(), out nwval);
                        if ((i > 0) && (dtst.Tables[0].Rows[i - 1][a].ToString()
              == dtst.Tables[0].Rows[i][a].ToString())
              && (mstgrp == true))
                        {
                        }
                        else
                        {
                            colsumVals[a] += nwval;
                            ((Microsoft.Office.Interop.Excel.Range)Global.trgtSheets[0].Cells[(i + Global.ovrllDataCnt + offst + 1), (colIdx + 1)]).Value2 = dtst.Tables[0].Rows[i][a].ToString();
                        }
                    }
                    else if (Global.mustColBeAvrgd(a.ToString(), colsToAvrg) == true)
                    {
                        if (Global.mustColBeFrmtd(a.ToString(), colsToFrmt) == true)
                        {
                            ((Microsoft.Office.Interop.Excel.Range)Global.trgtSheets[0].Cells[(i + Global.ovrllDataCnt + offst + 1), (colIdx + 1)]).NumberFormat = "#,##0.00_);[Red](#,##0.00)";
                        }

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
                            ((Microsoft.Office.Interop.Excel.Range)Global.trgtSheets[0].Cells[(i + Global.ovrllDataCnt + offst + 1), (colIdx + 1)]).Value2 = dtst.Tables[0].Rows[i][a].ToString();
                        }
                    }
                    else
                    {
                        if (Global.mustColBeFrmtd(a.ToString(), colsToFrmt) == true)
                        {
                            ((Microsoft.Office.Interop.Excel.Range)Global.trgtSheets[0].Cells[(i + Global.ovrllDataCnt + offst + 1), (colIdx + 1)]).NumberFormat = "#,##0.00_);[Red](#,##0.00)";
                        }

                      ((Microsoft.Office.Interop.Excel.Range)Global.trgtSheets[0].Cells[(i + Global.ovrllDataCnt + offst + 1), (colIdx + 1)]).Value2 = dtst.Tables[0].Rows[i][a].ToString();
                    }
                    colIdx++;
                }
            }

            Global.ovrllDataCnt += totl;
            string finalStr = "";
            colIdx = 0;
            for (int f = 0; f < colCnt; f++)
            {

                string algn = "left";
                int colLen = dtst.Tables[0].Columns[f].ColumnName.Length;
                finalStr = " ";
                if (colLen >= 3)
                {
                    if (Global.mustColBeCntd(f.ToString(), colsToCnt) == true)
                    {
                        if (Global.mustColBeFrmtd(f.ToString(), colsToFrmt) == true)
                        {
                            algn = "right";
                            finalStr = ("Count = " + colcntVals[f].ToString("#,##0"));
                        }
                        else
                        {
                            finalStr = ("Count = " + colcntVals[f].ToString());
                        }
                    }
                    else if (Global.mustColBeSumd(f.ToString(), colsToSum) == true)
                    {
                        if (Global.mustColBeFrmtd(f.ToString(), colsToFrmt) == true)
                        {
                            algn = "right";
                            finalStr = ("Sum = " + colsumVals[f].ToString("#,##0.00"));
                        }
                        else
                        {
                            finalStr = ("Sum = " + colsumVals[f].ToString());
                        }
                    }
                    else if (Global.mustColBeAvrgd(f.ToString(), colsToAvrg) == true)
                    {
                        if (Global.mustColBeFrmtd(f.ToString(), colsToFrmt) == true)
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
                  ((Microsoft.Office.Interop.Excel.Range)Global.trgtSheets[0].Cells[(1 + Global.ovrllDataCnt + offst + 1), (colIdx + 1)]).Value2 = finalStr;
                    colIdx++;
                }
            }
            Global.ovrllDataCnt += 2;
            if (islast)
            {
                if (colCnt > 5)
                {
                    Global.trgtSheets[0].get_Range("A6:Z65535", Type.Missing).WrapText = true;
                }
                else
                {
                    //Global.trgtSheets[0].get_Range("A1:A65535", Type.Missing).WrapText = true;
                }
                Global.trgtSheets[0].get_Range("A1:Z65535", Type.Missing).Columns.AutoFit();
                Global.trgtSheets[0].get_Range("A1:Z65535", Type.Missing).Rows.AutoFit();

                //if (int.Parse(Global.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth.ToString()) < 9)
                //{
                //}
                Global.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 9;
                if (exlfileNm.Contains(".pdf"))
                {
                    Global.nwWrkBk.ExportAsFixedFormat(Microsoft.Office.Interop.Excel.XlFixedFormatType.xlTypePDF,
                      exlfileNm, Type.Missing, Type.Missing,
                          Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                }
                else
                {
                    Global.nwWrkBk.SaveAs(exlfileNm, Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel8,
                      Type.Missing, Type.Missing,
                          false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                          Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                }
                if (Global.callngAppType == "DESKTOP")
                {
                    if (exlfileNm.Contains(".pdf"))
                    {
                        Global.upldImgsFTP(9, Global.getRptDrctry(), Global.runID.ToString() + ".pdf");
                    }
                    else
                    {
                        Global.upldImgsFTP(9, Global.getRptDrctry(), Global.runID.ToString() + ".xls");
                    }
                }
                //Global.nwWrkBk.SaveAs(exlfileNm, Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel8,
                //  Type.Missing, Type.Missing,
                //      false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                //      Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                //if (Global.callngAppType == "DESKTOP")
                //{
                //  Global.upldImgsFTP(9, Global.getRptDrctry(), Global.runID.ToString() + ".xls");
                //}
                Global.clearPrvExclFiles();
                Global.exclApp = null;
                Global.nwWrkBk = null;
            }
        }

        public static void exprtPDFTblr(DataSet dtst, string pdffileNm, string[] colsToGrp, string[] colsToCnt,
          string[] colsToSum, string[] colsToAvrg, string[] colsToFrmt
          , bool isfirst, bool islast, bool shdAppnd, string rptTitle, string orntnUsd)
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

            System.Drawing.Image imgGrhpc = Image.FromFile(Global.appStatPath + "/staffs.png");
            System.Drawing.Font nwFont = new Font("Lucida Console", 11, FontStyle.Regular);
            Graphics g = Graphics.FromImage(imgGrhpc);

            XPen aPen = new XPen(XColor.FromArgb(Color.Black), 1);
            PdfDocument document = null;
            PdfPage page0 = null;
            XGraphics gfx0 = null;
            if (isfirst)
            {
                // Create a new PDF document
                document = new PdfDocument();
                document.Info.Title = rptTitle.ToUpper();
                // Create first page for basic person details
                page0 = document.AddPage();
                if (orntnUsd == "Portrait")
                {
                    page0.Orientation = PageOrientation.Portrait;
                    page0.Height = XUnit.FromInch(11);
                    page0.Width = XUnit.FromInch(8.5);
                }
                else
                {
                    page0.Orientation = PageOrientation.Landscape;
                    page0.Height = XUnit.FromInch(8.5);
                    page0.Width = XUnit.FromInch(11);
                }
                gfx0 = XGraphics.FromPdfPage(page0);

                pageNo = 1;
            }
            XFont xfont0 = new XFont("Verdana", 20, XFontStyle.BoldItalic);
            //gfx0.DrawString("Hello, World!" + Global.locIDTextBox.Text, xfont0, XBrushes.Black,
            //new XRect(0, 0, page0.Width, page0.Height),
            //  XStringFormats.TopLeft);

            XFont xfont1 = new XFont("Times New Roman", 10.25f, XFontStyle.Underline | XFontStyle.Bold);
            XFont xfont11 = new XFont("Times New Roman", 10.25f, XFontStyle.Bold);
            XFont xfont2 = new XFont("Lucida Console", 10.25f, XFontStyle.Bold);
            XFont xfont4 = new XFont("Lucida Console", 10.25f, XFontStyle.Bold);
            XFont xfont41 = new XFont("Lucida Console", 10.25f);
            XFont xfont3 = new XFont("Lucida Console", 10.25f);
            XFont xfont31 = new XFont("Lucida Console", 10.5f, XFontStyle.Bold);
            XFont xfont5 = new XFont("Times New Roman", 6.0f, XFontStyle.Italic);

            Font font1 = new Font("Times New Roman", 10.25f, FontStyle.Underline | FontStyle.Bold);
            Font font11 = new Font("Times New Roman", 10.25f, FontStyle.Bold);
            Font font2 = new Font("Lucida Console", 10.25f, FontStyle.Bold);
            Font font4 = new Font("Lucida Console", 10.25f, FontStyle.Bold);
            Font font41 = new Font("Lucida Console", 10.25f);
            Font font3 = new Font("Lucida Console", 10.25f);
            Font font31 = new Font("Lucida Console", 10.5f, FontStyle.Bold);
            Font font5 = new Font("Times New Roman", 6.0f, FontStyle.Italic);

            float font1Hght = (float)gfx0.MeasureString("ABCDEFGHIJKLMNOP", xfont1).Height;
            float font2Hght = (float)gfx0.MeasureString("ABCDEFGHIJKLMNOP", xfont2).Height;
            float font3Hght = (float)gfx0.MeasureString("ABCDEFGHIJKLMNOP", xfont3).Height;
            float font4Hght = (float)gfx0.MeasureString("ABCDEFGHIJKLMNOP", xfont41).Height;
            float font5Hght = (float)gfx0.MeasureString("ABCDEFGHIJKLMNOP", xfont5).Height;

            float pageWidth = 590 - 40;//e.PageSettings.PrintableArea.Width;
            float pageHeight = 760 - 40;// e.PageSettings.PrintableArea.Height;
            if (orntnUsd != "Portrait")
            {
                pageHeight = 590 - 40;
                pageWidth = 760 - 40;
            }
            float txtwdth = pageWidth - 40;
            //Global.showMsg(pageWidth.ToString(), 0);
            float startX = 40;
            float startY = 40;
            float offsetY = 0;
            float ght = 0;
            float gwdth = 0;
            //StringBuilder strPrnt = new StringBuilder();
            //strPrnt.AppendLine("Received From");
            string[] nwLn;


            if (pageNo == 1)
            {
                //Org Logo
                //RectangleF srcRect = new Rectangle(0, 0, Global.BackgroundImage.Width,
                //BackgroundImage.Height);
                //RectangleF destRect = new Rectangle(0, 0, nWidth, nHeight);
                //Rectangle destRect = new Rectangle(0, 0, nWidth, nHeight);
                XImage img = (XImage)Image.FromFile(Global.getOrgImgsDrctry() + @"\" + Global.UsrsOrg_ID + ".png");
                float picWdth = 80.00F;
                float picHght = (float)(picWdth / img.PixelWidth) * (float)img.PixelHeight;


                gfx0.DrawImage(img, startX - 10, startY + offsetY - 15, picWdth, picHght);
                //g.DrawImage(Global.LargerImage, destRect, srcRect, GraphicsUnit.Pixel);

                //Org Name
                nwLn = Global.breakRptTxtDown(
                  Global.getOrgName(Global.UsrsOrg_ID),
                  pageWidth + 85, font2, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    gfx0.DrawString(nwLn[i]
                    , xfont2, XBrushes.Black, startX + picWdth + 5, startY + offsetY);
                    offsetY += font2Hght;
                }

                ght = (float)gfx0.MeasureString(
                  Global.getOrgPstlAddrs(Global.UsrsOrg_ID).Trim(), xfont2).Height;
                //offsetY = offsetY + (int)ght;

                //Pstal Address
                XTextFormatter tf = new XTextFormatter(gfx0);
                XRect rect = new XRect(startX + picWdth + 5, startY + offsetY - 7, 125, ght);
                gfx0.DrawRectangle(XBrushes.White, rect);
                tf.DrawString(Global.getOrgPstlAddrs(Global.UsrsOrg_ID).Trim()
                  , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                //gfx0.DrawString(,
                //xfont2, XBrushes.Black, startX + picWdth, startY + offsetY);
                offsetY += ght + 5;

                //Contacts Nos
                nwLn = Global.breakRptTxtDown(
          Global.getOrgContactNos(Global.UsrsOrg_ID),
          pageWidth, font2, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    gfx0.DrawString(nwLn[i]
                    , xfont2, XBrushes.Black, startX + picWdth + 5, startY + offsetY);
                    offsetY += font2Hght;
                }
                //Email Address
                nwLn = Global.breakRptTxtDown(
          Global.getOrgEmailAddrs(Global.UsrsOrg_ID),
          pageWidth, font2, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    gfx0.DrawString(nwLn[i]
                    , xfont2, XBrushes.Black, startX + picWdth + 5, startY + offsetY);
                    offsetY += font2Hght;
                }
                offsetY += font2Hght;
                if (offsetY < picHght)
                {
                    offsetY = picHght;
                }
                gfx0.DrawLine(aPen, startX, startY + offsetY - 8, startX + pageWidth - 20,
        startY + offsetY - 8);

            }

            //Tabular Data
            //offsetY += 2;
            offsetY -= 6;
            startX = 40;
            for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
            {
                startX = 40;
                if (a == 0)
                {
                    hgstOffsetY = 0;
                    ght = (float)gfx0.MeasureString(
                    rptTitle.ToUpper(), xfont2).Height;
                    //lblght = ght;
                    XTextFormatter tf = new XTextFormatter(gfx0);
                    XRect rect = new XRect(startX, startY + offsetY, pageWidth - 20, ght);
                    gfx0.DrawRectangle(XBrushes.LightGray, rect);
                    tf.DrawString(rptTitle.ToUpper()
                      , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                    offsetY += (int)ght + 5;
                    for (int j = 0; j < dtst.Tables[0].Columns.Count; j++)
                    {
                        int colLen = dtst.Tables[0].Columns[j].ColumnName.Length;
                        if (colLen >= 3)
                        {
                            string strTxt = "";
                            if (Global.mustColBeFrmtd(j.ToString(), colsToFrmt) == true)
                            {
                                strTxt = dtst.Tables[0].Columns[j].Caption.Trim().PadLeft(colLen, ' ') + " ";
                            }
                            else
                            {
                                strTxt = " " + dtst.Tables[0].Columns[j].Caption;
                            }

                            XSize sze = gfx0.MeasureString(
                         strTxt, xfont2);
                            ght = (float)sze.Height;
                            float wdth = (float)sze.Width;
                            if (wdth < (float)(dtst.Tables[0].Columns[j].Caption.Length * 5))
                            {
                                wdth = (float)(dtst.Tables[0].Columns[j].Caption.Length * 5);
                            }
                            tf = new XTextFormatter(gfx0);
                            rect = new XRect(startX, startY + offsetY, wdth + 5, ght);
                            gfx0.DrawRectangle(XBrushes.LightGray, rect);
                            tf.DrawString(strTxt
                              , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                            startX += wdth + 10;
                        }
                    }
                    offsetY += (int)ght + 5;
                    startX = 40;
                }
                float hghstght = 0;
                for (int j = 0; j < dtst.Tables[0].Columns.Count; j++)
                {
                    double nwval = 0;

                    bool mstgrp = Global.mustColBeGrpd(j.ToString(), colsToGrp);
                    if (Global.mustColBeCntd(j.ToString(), colsToCnt) == true)
                    {
                        if ((a > 0) && (dtst.Tables[0].Rows[a - 1][j].ToString()
                        == dtst.Tables[0].Rows[a][j].ToString())
                        && (mstgrp == true))
                        {
                        }
                        else
                        {
                            colcntVals[j] += 1;
                        }
                    }
                    else if (Global.mustColBeSumd(j.ToString(), colsToSum) == true)
                    {
                        double.TryParse(dtst.Tables[0].Rows[a][j].ToString(), out nwval);
                        if ((a > 0) && (dtst.Tables[0].Rows[a - 1][j].ToString()
              == dtst.Tables[0].Rows[a][j].ToString())
              && (mstgrp == true))
                        {
                        }
                        else
                        {
                            colsumVals[j] += nwval;
                        }
                    }
                    else if (Global.mustColBeAvrgd(j.ToString(), colsToAvrg) == true)
                    {
                        double.TryParse(dtst.Tables[0].Rows[a][j].ToString(), out nwval);
                        if ((a > 0) && (dtst.Tables[0].Rows[a - 1][j].ToString()
            == dtst.Tables[0].Rows[a][j].ToString())
            && (mstgrp == true))
                        {
                        }
                        else
                        {
                            colcntVals[j] += 1;
                            colsumVals[j] += nwval;
                        }
                    }

                    int colLen = dtst.Tables[0].Columns[j].ColumnName.Length;
                    if (colLen >= 3)
                    {
                        string strTxt = "";
                        if ((a > 0) && (dtst.Tables[0].Rows[a - 1][j].ToString()
                          == dtst.Tables[0].Rows[a][j].ToString())
                          && (Global.mustColBeGrpd(j.ToString(), colsToGrp) == true))
                        {
                            strTxt = " ";
                        }
                        else
                        {
                            if (Global.mustColBeFrmtd(j.ToString(), colsToFrmt) == true)
                            {
                                double num = 0;
                                double.TryParse(dtst.Tables[0].Rows[a][j].ToString().Trim(), out num);
                                if (dtst.Tables[0].Rows[a][j].ToString() != "")
                                {
                                    strTxt = num.ToString("#,##0.00").Trim();
                                }
                                else
                                {
                                    //dtst.Tables[0].Rows[a][j].ToString()
                                    strTxt = dtst.Tables[0].Rows[a][j].ToString() + " ";
                                }
                            }
                            else
                            {
                                strTxt = dtst.Tables[0].Rows[a][j].ToString() + " ";
                            }
                        }

                        XSize sze = gfx0.MeasureString(dtst.Tables[0].Columns[j].Caption, xfont2);
                        ght = (float)sze.Height;
                        float wdth = (float)(sze.Width);
                        if (wdth < (float)(dtst.Tables[0].Columns[j].Caption.Length * 5))
                        {
                            wdth = (float)(dtst.Tables[0].Columns[j].Caption.Length * 5);
                        }
                        nwLn = Global.breakPDFTxtDown(
                          strTxt,
                          (int)(wdth * 1.0), font41, g);
                        string dsplyStr = "";
                        if (Global.mustColBeFrmtd(j.ToString(), colsToFrmt) == true)
                        {
                            dsplyStr = string.Join("\n", nwLn).PadLeft(dtst.Tables[0].Columns[j].Caption.Length, ' ');
                        }
                        else
                        {
                            dsplyStr = string.Join("\n", nwLn);
                        }
                        ght = (float)gfx0.MeasureString(dsplyStr, xfont41).Height;

                        XTextFormatter tf = new XTextFormatter(gfx0);
                        XRect rect = new XRect(startX, startY + offsetY, wdth + 5, ght);
                        gfx0.DrawRectangle(XBrushes.White, rect);
                        //nwLn.Length + "-" + 
                        tf.DrawString(dsplyStr
                          , xfont41, XBrushes.Black, rect, XStringFormats.TopLeft);

                        startX += wdth + 10;
                        if (hghstght < ght)
                        {
                            hghstght = ght;
                        }
                    }
                }
                if (hghstght < 10)
                {
                    hghstght = 10;
                }
                offsetY += hghstght + 5;
                if (hgstOffsetY < offsetY)
                {
                    hgstOffsetY = offsetY;
                }
                if ((startY + offsetY) >= (pageHeight + 20))
                {
                    page0 = document.AddPage();
                    if (orntnUsd == "Portrait")
                    {
                        page0.Orientation = PageOrientation.Portrait;
                        page0.Height = XUnit.FromInch(11);
                        page0.Width = XUnit.FromInch(8.5);
                    }
                    else
                    {
                        page0.Orientation = PageOrientation.Landscape;
                        page0.Height = XUnit.FromInch(8.5);
                        page0.Width = XUnit.FromInch(11);
                    }
                    gfx0 = XGraphics.FromPdfPage(page0);
                    offsetY = 0;
                    hgstOffsetY = 0;
                }
            }

            startX = 40;
            offsetY += 3;
            if ((startY + offsetY) >= (pageHeight + 20))
            {
                page0 = document.AddPage();
                if (orntnUsd == "Portrait")
                {
                    page0.Orientation = PageOrientation.Portrait;
                    page0.Height = XUnit.FromInch(11);
                    page0.Width = XUnit.FromInch(8.5);
                }
                else
                {
                    page0.Orientation = PageOrientation.Landscape;
                    page0.Height = XUnit.FromInch(8.5);
                    page0.Width = XUnit.FromInch(11);
                }
                gfx0 = XGraphics.FromPdfPage(page0);
                offsetY = 0;
                hgstOffsetY = 0;
            }

            for (int f = 0; f < dtst.Tables[0].Columns.Count; f++)
            {
                string finalStr = " ";
                int colLen = dtst.Tables[0].Columns[f].ColumnName.Length;
                if (colLen >= 3)
                {
                    if (Global.mustColBeCntd(f.ToString(), colsToCnt) == true)
                    {
                        if (Global.mustColBeFrmtd(f.ToString(), colsToFrmt) == true)
                        {
                            finalStr = ("Count = " + colcntVals[f].ToString("#,##0"));
                        }
                        else
                        {
                            finalStr = ("Count = " + colcntVals[f].ToString());
                        }
                    }
                    else if (Global.mustColBeSumd(f.ToString(), colsToSum) == true)
                    {
                        if (Global.mustColBeFrmtd(f.ToString(), colsToFrmt) == true)
                        {
                            finalStr = ("Sum = " + colsumVals[f].ToString("#,##0.00"));
                        }
                        else
                        {
                            finalStr = ("Sum = " + colsumVals[f].ToString());
                        }
                    }
                    else if (Global.mustColBeAvrgd(f.ToString(), colsToAvrg) == true)
                    {
                        if (Global.mustColBeFrmtd(f.ToString(), colsToFrmt) == true)
                        {
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
                    XSize sze = gfx0.MeasureString(dtst.Tables[0].Columns[f].Caption, xfont2);
                    ght = (float)sze.Height;
                    float wdth = (float)(sze.Width);
                    if (wdth < (float)(dtst.Tables[0].Columns[f].Caption.Length * 5))
                    {
                        wdth = (float)(dtst.Tables[0].Columns[f].Caption.Length * 5);
                    }
                    nwLn = Global.breakPDFTxtDown(
                      finalStr,
                      (int)(wdth * 1.5), font41, g);
                    ght = (float)gfx0.MeasureString(string.Join("\n", nwLn), xfont41).Height;

                    XTextFormatter tf = new XTextFormatter(gfx0);
                    XRect rect = new XRect(startX, startY + offsetY, wdth + 5, ght);
                    gfx0.DrawRectangle(XBrushes.White, rect);

                    tf.DrawString(string.Join("\n", nwLn)
                      , xfont41, XBrushes.Black, rect, XStringFormats.TopLeft);

                    startX += wdth + 10;
                }
            }

            if (islast)
            {
                //Slogan: 
                startX = 40;
                offsetY = pageHeight - 5;
                gfx0.DrawLine(aPen, startX, startY + offsetY, startX + pageWidth - 20,
          startY + offsetY);
                offsetY += font3Hght;
                nwLn = Global.breakRptTxtDown(
                  Global.getOrgName(Global.UsrsOrg_ID) + "..." +
                  Global.getOrgSlogan(Global.UsrsOrg_ID),
          pageWidth - ght, font5, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    gfx0.DrawString(nwLn[i]
                    , xfont5, XBrushes.Black, startX, startY + offsetY);
                    offsetY += font5Hght;
                }
                //offsetY += font5Hght;
                nwLn = Global.breakRptTxtDown(
                 "Software Developed by Rhomicom Systems Technologies Ltd.",
          pageWidth + 40, font5, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    gfx0.DrawString(nwLn[i]
                    , xfont5, XBrushes.Black, startX, startY + offsetY);
                    offsetY += font5Hght;
                }
                nwLn = Global.breakRptTxtDown(
          "Website:www.rhomicomgh.com",
          pageWidth + 40, font5, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    gfx0.DrawString(nwLn[i]
                    , xfont5, XBrushes.Black, startX, startY + offsetY);
                    offsetY += font5Hght;
                }
                // Save the document...
                document.Save(pdffileNm);

                if (Global.callngAppType == "DESKTOP")
                {
                    Global.upldImgsFTP(9, Global.getRptDrctry(), Global.runID.ToString() + ".pdf");
                }
            }
        }

        public static void exprtToPDFDet(DataSet recsdtst, DataSet grpsdtst, string fileNm, string rptTitle
       , bool isfirst, bool islast, bool shdAppnd, string orntnUsd, string imgCols)
        {

            System.Drawing.Image imgGrhpc = Image.FromFile(Global.appStatPath + "/staffs.png");
            System.Drawing.Font nwFont = new Font("Lucida Console", 11, FontStyle.Regular);
            Graphics g = Graphics.FromImage(imgGrhpc);

            XPen aPen = new XPen(XColor.FromArgb(Color.Black), 1);
            PdfDocument document = null;
            PdfPage page0 = null;
            XGraphics gfx0 = null;

            imgCols = "," + imgCols.Trim(',') + ",";
            string cption = "";
            if (isfirst)
            {
                // Create a new PDF document
                document = new PdfDocument();
                document.Info.Title = rptTitle.ToUpper();

                pageNo = 0;
            }

            XFont xfont0 = new XFont("Verdana", 20, XFontStyle.BoldItalic);
            //gfx0.DrawString("Hello, World!" + Global.locIDTextBox.Text, xfont0, XBrushes.Black,
            //new XRect(0, 0, page0.Width, page0.Height),
            //  XStringFormats.TopLeft);

            XFont xfont1 = new XFont("Times New Roman", 10.25f, XFontStyle.Underline | XFontStyle.Bold);
            XFont xfont11 = new XFont("Times New Roman", 10.25f, XFontStyle.Bold);
            XFont xfont2 = new XFont("Lucida Console", 9.25f, XFontStyle.Bold);
            XFont xfont4 = new XFont("Lucida Console", 9.25f, XFontStyle.Bold);
            XFont xfont41 = new XFont("Lucida Console", 9.25f);
            XFont xfont3 = new XFont("Lucida Console", 8.25f);
            XFont xfont31 = new XFont("Lucida Console", 10.5f, XFontStyle.Bold);
            XFont xfont5 = new XFont("Times New Roman", 6.0f, XFontStyle.Italic);

            Font font1 = new Font("Times New Roman", 10.25f, FontStyle.Underline | FontStyle.Bold);
            Font font11 = new Font("Times New Roman", 10.25f, FontStyle.Bold);
            Font font2 = new Font("Lucida Console", 9.25f, FontStyle.Bold);
            Font font4 = new Font("Lucida Console", 9.25f, FontStyle.Bold);
            Font font41 = new Font("Lucida Console", 9.25f);
            Font font3 = new Font("Lucida Console", 8.25f);
            Font font31 = new Font("Lucida Console", 10.5f, FontStyle.Bold);
            Font font5 = new Font("Times New Roman", 6.0f, FontStyle.Italic);

            float font1Hght = 0;
            float font2Hght = 0;
            float font3Hght = 0;
            float font4Hght = 0;
            float font5Hght = 0;

            float pageWidth = 590 - 40;//e.PageSettings.PrintableArea.Width;
            float pageHeight = 760 - 40;// e.PageSettings.PrintableArea.Height;
            if (orntnUsd != "Portrait")
            {
                pageHeight = 590 - 40;
                pageWidth = 760 - 40;
            }
            float txtwdth = pageWidth - 40;
            //Global.showMsg(pageWidth.ToString(), 0);
            float startX = 40;
            float startY = 40;
            float offsetY = 0;
            float offsetX = 0;
            float strtoffsetY = 0;
            float strtoffsetX = 0;
            float endoffsetY = 0;
            float endoffsetX = 0;
            float ght = 0;
            float gwdth = 0;
            //StringBuilder strPrnt = new StringBuilder();
            //strPrnt.AppendLine("Received From");
            string[] nwLn;



            int lblwdth = 0;
            string finalStr = " ";
            string algn = "left";
            string[] rptGrpVals = {"Group Title","Group Page Width Type","Group Min-Height",
                             "Show Group Border","Group Display Type","No of Vertical Divs In Group",
                             "Comma Separated Col Nos", "Data Label Max Width%",
                             "Comma Separated Hdr Nms","Column Delimiter","Row Delimiter"};

            string grpTitle = "";
            string curgrpPgWdth = "";
            string prvsgrpPgWdth = "";
            int grpMinHght = 0;
            string shwBrdr = "Show";
            string grpDsplyTyp = "Details";
            int grpColDvsns = 4;//Use 1 for Images others 2 or 4
            int nxtgrpColDvsns = 4;
            string colnums = "";
            string lblmaxwdthprcnt = "35";
            string tblrHdrs = "";
            string clmDlmtrs = "";
            string rwDlmtrs = "";

            int divwdth = 0;

            /* 1. For each detail group create a div and fieldset with legend & border based on group settings
             * 2a. if detail display then create required no of td in tr1 of a table, create new tr if no of columns is not exhausted
             *      i.e if no of vertical divs=4 no rows=math.ceil(no cols*0.5)/
             *      else no rows=no cols
             *      for each col display label and data if vrtcl divs is 2 or 4 else display only data
             * 2b. if tabular create table with headers according to defined headers
             *      split data according to rows and cols and display them in this table
             * 2. Get all column nos within the group and create their labels and data using settings
             * 3. if col nos is image then use full defined page width else create no of defined columns count
             * 4. if 
             * 
             */
            int grpdtcnt = grpsdtst.Tables[0].Rows.Count;
            int rowsdtcnt = recsdtst.Tables[0].Rows.Count;
            for (int a = 0; a < rowsdtcnt; a++)
            {
                XTextFormatter tf;
                XRect rect;
                XImage img;
                float picWdth;
                float picHght;

                page0 = document.AddPage();
                if (orntnUsd == "Portrait")
                {
                    page0.Orientation = PageOrientation.Portrait;
                    page0.Height = XUnit.FromInch(11);
                    page0.Width = XUnit.FromInch(8.5);
                }
                else
                {
                    page0.Orientation = PageOrientation.Landscape;
                    page0.Height = XUnit.FromInch(8.5);
                    page0.Width = XUnit.FromInch(11);
                }
                gfx0 = XGraphics.FromPdfPage(page0);
                font1Hght = (float)gfx0.MeasureString("ABCDEFGHIJKLMNOP", xfont1).Height;
                font2Hght = (float)gfx0.MeasureString("ABCDEFGHIJKLMNOP", xfont2).Height;
                font3Hght = (float)gfx0.MeasureString("ABCDEFGHIJKLMNOP", xfont3).Height;
                font4Hght = (float)gfx0.MeasureString("ABCDEFGHIJKLMNOP", xfont41).Height;
                font5Hght = (float)gfx0.MeasureString("ABCDEFGHIJKLMNOP", xfont5).Height;

                offsetY = 0;
                hgstOffsetY = 0;

                img = (XImage)Image.FromFile(Global.getOrgImgsDrctry() + @"\" + Global.UsrsOrg_ID + ".png");
                picWdth = 80.00F;
                picHght = (float)(picWdth / img.PixelWidth) * (float)img.PixelHeight;


                gfx0.DrawImage(img, startX - 10, startY + offsetY - 15, picWdth, picHght);
                //g.DrawImage(Global.LargerImage, destRect, srcRect, GraphicsUnit.Pixel);

                //Org Name
                nwLn = Global.breakRptTxtDown(
                  Global.getOrgName(Global.UsrsOrg_ID),
                  pageWidth + 85, font2, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    gfx0.DrawString(nwLn[i]
                    , xfont2, XBrushes.Black, startX + picWdth + 5, startY + offsetY);
                    offsetY += font2Hght;
                }

                ght = (float)gfx0.MeasureString(
                  Global.getOrgPstlAddrs(Global.UsrsOrg_ID).Trim(), xfont2).Height;
                //offsetY = offsetY + (int)ght;

                //Pstal Address
                tf = new XTextFormatter(gfx0);
                rect = new XRect(startX + picWdth + 5, startY + offsetY - 7, 125, ght);
                gfx0.DrawRectangle(XBrushes.White, rect);
                tf.DrawString(Global.getOrgPstlAddrs(Global.UsrsOrg_ID).Trim()
                  , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                //gfx0.DrawString(,
                //xfont2, XBrushes.Black, startX + picWdth, startY + offsetY);
                offsetY += ght + 5;

                //Contacts Nos
                nwLn = Global.breakRptTxtDown(
          Global.getOrgContactNos(Global.UsrsOrg_ID),
          pageWidth, font2, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    gfx0.DrawString(nwLn[i]
                    , xfont2, XBrushes.Black, startX + picWdth + 5, startY + offsetY);
                    offsetY += font2Hght;
                }
                //Email Address
                nwLn = Global.breakRptTxtDown(
          Global.getOrgEmailAddrs(Global.UsrsOrg_ID),
          pageWidth, font2, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    gfx0.DrawString(nwLn[i]
                    , xfont2, XBrushes.Black, startX + picWdth + 5, startY + offsetY);
                    offsetY += font2Hght;
                }
                offsetY += font2Hght;
                if (offsetY < picHght)
                {
                    offsetY = picHght;
                }
                gfx0.DrawLine(aPen, startX, startY + offsetY - 8, startX + pageWidth - 20,
        startY + offsetY - 8);


                //Tabular Data
                //offsetY += 2;
                pageNo += 1;

                offsetY -= 6;
                startX = 40;
                hgstOffsetY = 0;
                ght = (float)gfx0.MeasureString(
                rptTitle.ToUpper(), xfont2).Height;
                //lblght = ght;
                tf = new XTextFormatter(gfx0);
                rect = new XRect(startX, startY + offsetY, pageWidth - 20, ght);
                gfx0.DrawRectangle(XBrushes.LightGray, rect);
                tf.DrawString(rptTitle.ToUpper()
                  , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                offsetY += (int)ght + 5;
                int grpPgPostn = 0;
                endoffsetY = 0;

                for (int d = 0; d < grpdtcnt; d++)
                {
                    lblwdth = 35;
                    grpTitle = grpsdtst.Tables[0].Rows[d][0].ToString();
                    curgrpPgWdth = grpsdtst.Tables[0].Rows[d][1].ToString();
                    grpMinHght = int.Parse(grpsdtst.Tables[0].Rows[d][2].ToString());
                    shwBrdr = grpsdtst.Tables[0].Rows[d][3].ToString();
                    grpDsplyTyp = grpsdtst.Tables[0].Rows[d][4].ToString();
                    grpColDvsns = int.Parse(grpsdtst.Tables[0].Rows[d][5].ToString());//Use 1 for Images others 2 or 4
                    if (d > 0)
                    {
                        //nxtgrpColDvsns = int.Parse(grpsdtst.Tables[0].Rows[d + 1][5].ToString());
                        prvsgrpPgWdth = grpsdtst.Tables[0].Rows[d - 1][1].ToString();
                    }
                    else
                    {
                        //nxtgrpColDvsns = grpColDvsns;
                        prvsgrpPgWdth = "Unknown";
                    }
                    colnums = grpsdtst.Tables[0].Rows[d][6].ToString();
                    lblmaxwdthprcnt = grpsdtst.Tables[0].Rows[d][7].ToString();
                    tblrHdrs = grpsdtst.Tables[0].Rows[d][8].ToString();
                    clmDlmtrs = grpsdtst.Tables[0].Rows[d][9].ToString();
                    rwDlmtrs = grpsdtst.Tables[0].Rows[d][10].ToString();

                    int.TryParse(lblmaxwdthprcnt, out lblwdth);

                    if (curgrpPgWdth == "Half Page Width")
                    {
                        divwdth = (int)(pageWidth / 2);
                    }
                    else
                    {
                        divwdth = (int)(pageWidth / 1);
                    }
                    divwdth -= 10;
                    lblwdth = (divwdth * lblwdth) / 100;
                    oldoffsetY = offsetY;
                    if (d > 0)
                    {
                        if (curgrpPgWdth == "Full Page Width" || prvsgrpPgWdth == "Full Page Width")
                        {
                            offsetY = endoffsetY;
                            offsetX = 0;
                        }
                        else if (curgrpPgWdth == "Half Page Width" && prvsgrpPgWdth == "Half Page Width" && endoffsetX < (divwdth + 20))
                        {
                            offsetY = strtoffsetY;
                            offsetX = endoffsetX;
                        }
                        else if (curgrpPgWdth == "Half Page Width" && prvsgrpPgWdth == "Half Page Width" && endoffsetX >= (divwdth + 20))
                        {
                            offsetY = endoffsetY;
                            offsetX = 0;
                        }
                        else
                        {
                            offsetX = 0;
                        }
                    }
                    else
                    {
                        offsetX = 0;
                    }
                    strtoffsetY = offsetY;

                    if (shwBrdr == "Show")
                    {
                        hgstOffsetY = 0;
                        nwLn = Global.breakPDFTxtDown(
                                  grpTitle.ToUpper(),
                                  divwdth, font2, g);
                        string dsplystr = string.Join("\n", nwLn);
                        ght = (float)gfx0.MeasureString(dsplystr, xfont2).Height;
                        float wdth = (float)gfx0.MeasureString(dsplystr, xfont2).Width;

                        tf = new XTextFormatter(gfx0);
                        rect = new XRect(startX + offsetX, startY + offsetY, divwdth - 5, ght);

                        gfx0.DrawRectangle(XBrushes.LightGray, rect);
                        tf.DrawString(dsplystr
                          , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                        offsetY += (int)ght + 5;
                    }
                    strtoffsetX = offsetX;
                    endoffsetX = offsetX + divwdth;
                    char[] w = { ',' };
                    string[] colNumbers = colnums.Split(w, StringSplitOptions.RemoveEmptyEntries);
                    int noofRws = 1;
                    //lblwdth = ((divwdth - 90) * lblwdth) / 100;
                    long[] colcntVals = new long[colNumbers.Length];
                    double[] colsumVals = new double[colNumbers.Length];
                    double[] colavrgVals = new double[colNumbers.Length];
                    float hghstght = 0;
                    float hgstwidth = 0;
                    if (curgrpPgWdth == "Half Page Width")
                    {
                        grpPgPostn += 1;
                    }
                    else if (curgrpPgWdth == "Full Page Width")
                    {
                        grpPgPostn += 2;
                    }

                    if (grpDsplyTyp == "DETAIL")
                    {
                        if (grpColDvsns == 4)
                        {
                            noofRws = (int)Math.Ceiling((double)colNumbers.Length / (double)2);
                        }
                        else
                        {
                            noofRws = colNumbers.Length;
                        }
                        if (grpColDvsns == 4)
                        {
                            for (int h = 0; h < colNumbers.Length; h++)
                            {
                                if ((h % 2) == 0)
                                {
                                    //New Row
                                    hghstght = 0;
                                }
                                int clnm = -1;
                                bool isNmint = int.TryParse(colNumbers[h], out clnm);
                                if (isNmint == true)
                                {
                                    string frsh = "";
                                    frsh = recsdtst.Tables[0].Columns[clnm].Caption.Trim() + ": ";
                                    nwLn = Global.breakPDFTxtDown(
                                        frsh,
                                        (int)((lblwdth / 2) * 1.2), font2, g);
                                    string dsplystr = string.Join("\n", nwLn);
                                    ght = (float)gfx0.MeasureString(dsplystr, xfont2).Height;
                                    float wdth = (float)gfx0.MeasureString(dsplystr, xfont2).Width;

                                    tf = new XTextFormatter(gfx0);
                                    rect = new XRect(startX + offsetX, startY + offsetY, (lblwdth / 2) - 5, ght);
                                    gfx0.DrawRectangle(XBrushes.White, rect);
                                    //nwLn.Length + "-" + 
                                    tf.DrawString(dsplystr
                                      , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);

                                    offsetX += (lblwdth / 2);
                                    if (hghstght < ght)
                                    {
                                        hghstght = ght;
                                    }
                                    if (hgstwidth < wdth)
                                    {
                                        hgstwidth = wdth;
                                    }

                                    frsh = recsdtst.Tables[0].Rows[a][clnm].ToString().Trim();
                                    if (imgCols.Contains("," + clnm + ","))
                                    {
                                        if (System.IO.File.Exists(Global.dataBasDir + frsh))
                                        {
                                            System.IO.FileInfo finf = new FileInfo(Global.dataBasDir + frsh);

                                            string extnsn = finf.Extension;

                                            img = (XImage)Image.FromFile(Global.dataBasDir + frsh);
                                            picWdth = (float)(((divwdth - lblwdth) / 2) * 0.7);
                                            picHght = (float)(picWdth / img.PixelWidth) * (float)img.PixelHeight;

                                            gfx0.DrawImage(img, startX + offsetX + 15, startY + offsetY, picWdth, picHght);

                                            if (hghstght < picHght)
                                            {
                                                hghstght = picHght;
                                            }
                                            if (hgstwidth < picWdth)
                                            {
                                                hgstwidth = picWdth;
                                            }
                                            offsetX += ((divwdth - lblwdth) / 2);
                                        }
                                    }
                                    else
                                    {
                                        nwLn = Global.breakPDFTxtDown(
                                          frsh,
                                          (int)(((divwdth - lblwdth) / 2) * 1.2), font41, g);
                                        dsplystr = string.Join("\n", nwLn);
                                        ght = (float)gfx0.MeasureString(dsplystr, xfont41).Height;
                                        wdth = (float)gfx0.MeasureString(dsplystr, xfont41).Width;

                                        tf = new XTextFormatter(gfx0);
                                        rect = new XRect(startX + offsetX, startY + offsetY, ((divwdth - lblwdth) / 2) - 5, ght);
                                        gfx0.DrawRectangle(XBrushes.White, rect);
                                        //nwLn.Length + "-" + 
                                        tf.DrawString(dsplystr
                                          , xfont41, XBrushes.Black, rect, XStringFormats.TopLeft);

                                        offsetX += ((divwdth - lblwdth) / 2);
                                        if (hghstght < ght)
                                        {
                                            hghstght = ght;
                                        }
                                        if (hgstwidth < wdth)
                                        {
                                            hgstwidth = wdth;
                                        }

                                    }
                                }
                                if (hghstght < 10)
                                {
                                    hghstght = 10;
                                }

                                if ((h % 2) == 1)
                                {
                                    hgstwidth = 0;
                                    offsetX = strtoffsetX;
                                    offsetY += (int)hghstght + 5;
                                    if (endoffsetY < (offsetY))
                                    {
                                        endoffsetY = offsetY;
                                    }
                                }
                                else
                                {
                                    //offsetY += (int)hghstght + 5;
                                    //hgstwidth = 0;
                                    //offsetX = (int)hgstwidth;
                                }
                                if (hgstOffsetY < offsetY)
                                {
                                    hgstOffsetY = offsetY;
                                }
                                if ((startY + offsetY) >= (pageHeight + 20))
                                {
                                    page0 = document.AddPage();
                                    if (orntnUsd == "Portrait")
                                    {
                                        page0.Orientation = PageOrientation.Portrait;
                                        page0.Height = XUnit.FromInch(11);
                                        page0.Width = XUnit.FromInch(8.5);
                                    }
                                    else
                                    {
                                        page0.Orientation = PageOrientation.Landscape;
                                        page0.Height = XUnit.FromInch(8.5);
                                        page0.Width = XUnit.FromInch(11);
                                    }
                                    gfx0 = XGraphics.FromPdfPage(page0);
                                    offsetY = 0;
                                    hgstOffsetY = 0;
                                    endoffsetY = 0;

                                    //offsetX = 0;
                                }
                            }
                            if (endoffsetY < offsetY)
                            {
                                endoffsetY = offsetY;
                            }
                        }
                        else if (grpColDvsns == 2)
                        {
                            for (int h = 0; h < colNumbers.Length; h++)
                            {
                                //New Row
                                hghstght = 0;
                                int clnm = -1;
                                bool isNmint = int.TryParse(colNumbers[h], out clnm);
                                if (isNmint == true)
                                {
                                    string frsh = "";
                                    frsh = recsdtst.Tables[0].Columns[clnm].Caption.Trim() + ": ";
                                    nwLn = Global.breakPDFTxtDown(
                                        frsh,
                                        (int)(lblwdth * 1.2), font2, g);
                                    string dsplystr = string.Join("\n", nwLn);
                                    ght = (float)gfx0.MeasureString(dsplystr, xfont2).Height;
                                    float wdth = (float)gfx0.MeasureString(dsplystr, xfont2).Width;

                                    tf = new XTextFormatter(gfx0);
                                    rect = new XRect(startX + offsetX, startY + offsetY, lblwdth - 5, ght);
                                    gfx0.DrawRectangle(XBrushes.White, rect);
                                    //nwLn.Length + "-" + 
                                    tf.DrawString(dsplystr
                                      , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);

                                    offsetX += lblwdth;
                                    if (hghstght < ght)
                                    {
                                        hghstght = ght;
                                    }
                                    if (hgstwidth < wdth)
                                    {
                                        hgstwidth = wdth;
                                    }

                                    frsh = recsdtst.Tables[0].Rows[a][clnm].ToString().Trim();
                                    if (imgCols.Contains("," + clnm + ","))
                                    {
                                        if (System.IO.File.Exists(Global.dataBasDir + frsh))
                                        {
                                            System.IO.FileInfo finf = new FileInfo(Global.dataBasDir + frsh);

                                            string extnsn = finf.Extension;

                                            img = (XImage)Image.FromFile(Global.dataBasDir + frsh);
                                            picWdth = (float)((divwdth - lblwdth) * 0.7);
                                            picHght = (float)(picWdth / img.PixelWidth) * (float)img.PixelHeight;

                                            gfx0.DrawImage(img, startX + offsetX + 15, startY + offsetY, picWdth, picHght);
                                            if (hghstght < picHght)
                                            {
                                                hghstght = picHght;
                                            }
                                            if (hgstwidth < picWdth)
                                            {
                                                hgstwidth = picWdth;
                                            }
                                            offsetX += (divwdth - lblwdth);
                                        }
                                    }
                                    else
                                    {
                                        nwLn = Global.breakPDFTxtDown(
                                          frsh,
                                          (int)((divwdth - lblwdth) * 1.2), font41, g);
                                        dsplystr = string.Join("\n", nwLn);
                                        ght = (float)gfx0.MeasureString(dsplystr, xfont41).Height;
                                        wdth = (float)gfx0.MeasureString(dsplystr, xfont41).Width;

                                        tf = new XTextFormatter(gfx0);
                                        rect = new XRect(startX + offsetX, startY + offsetY, (divwdth - lblwdth) - 5, ght);
                                        gfx0.DrawRectangle(XBrushes.White, rect);
                                        //nwLn.Length + "-" + 
                                        tf.DrawString(dsplystr
                                          , xfont41, XBrushes.Black, rect, XStringFormats.TopLeft);

                                        offsetX += (divwdth - lblwdth);
                                        if (hghstght < ght)
                                        {
                                            hghstght = ght;
                                        }
                                        if (hgstwidth < wdth)
                                        {
                                            hgstwidth = wdth;
                                        }
                                    }
                                }
                                if (hghstght < 10)
                                {
                                    hghstght = 10;
                                }

                                hgstwidth = 0;
                                offsetX = strtoffsetX;
                                offsetY += (int)hghstght + 5;
                                if (endoffsetY < (offsetY))
                                {
                                    endoffsetY = offsetY;
                                }

                                if (hgstOffsetY < offsetY)
                                {
                                    hgstOffsetY = offsetY;
                                }
                                if ((startY + offsetY) >= (pageHeight + 20))
                                {
                                    page0 = document.AddPage();
                                    if (orntnUsd == "Portrait")
                                    {
                                        page0.Orientation = PageOrientation.Portrait;
                                        page0.Height = XUnit.FromInch(11);
                                        page0.Width = XUnit.FromInch(8.5);
                                    }
                                    else
                                    {
                                        page0.Orientation = PageOrientation.Landscape;
                                        page0.Height = XUnit.FromInch(8.5);
                                        page0.Width = XUnit.FromInch(11);
                                    }
                                    gfx0 = XGraphics.FromPdfPage(page0);
                                    offsetY = 0;
                                    hgstOffsetY = 0;
                                    endoffsetY = 0;

                                }
                            }
                            if (endoffsetY < offsetY)
                            {
                                endoffsetY = offsetY;
                            }
                        }
                        else if (grpColDvsns == 1)
                        {
                            for (int h = 0; h < colNumbers.Length; h++)
                            {
                                //New Row
                                hghstght = 0;
                                int clnm = -1;
                                bool isNmint = int.TryParse(colNumbers[h], out clnm);
                                if (isNmint == true)
                                {
                                    string frsh = "";
                                    string dsplystr = "";
                                    float wdth = 0;

                                    frsh = recsdtst.Tables[0].Rows[a][clnm].ToString().Trim();
                                    if (imgCols.Contains("," + clnm + ","))
                                    {
                                        if (System.IO.File.Exists(Global.dataBasDir + frsh))
                                        {
                                            System.IO.FileInfo finf = new FileInfo(Global.dataBasDir + frsh);

                                            string extnsn = finf.Extension;

                                            img = (XImage)Image.FromFile(Global.dataBasDir + frsh);
                                            picWdth = (float)((divwdth) * 0.5);
                                            picHght = (float)(picWdth / img.PixelWidth) * (float)img.PixelHeight;

                                            gfx0.DrawImage(img, startX + offsetX + 15, startY + offsetY, picWdth, picHght);
                                            if (hghstght < picHght)
                                            {
                                                hghstght = picHght;
                                            }
                                            if (hgstwidth < picWdth)
                                            {
                                                hgstwidth = picWdth;
                                            }
                                            offsetX += (divwdth);
                                        }
                                    }
                                    else
                                    {
                                        nwLn = Global.breakPDFTxtDown(
                                          frsh,
                                          (int)((divwdth) * 1.2), font41, g);
                                        dsplystr = string.Join("\n", nwLn);
                                        ght = (float)gfx0.MeasureString(dsplystr, xfont41).Height;
                                        wdth = (float)gfx0.MeasureString(dsplystr, xfont41).Width;

                                        tf = new XTextFormatter(gfx0);
                                        rect = new XRect(startX + offsetX, startY + offsetY, (divwdth) - 5, ght);
                                        gfx0.DrawRectangle(XBrushes.White, rect);
                                        //nwLn.Length + "-" + 
                                        tf.DrawString(dsplystr
                                          , xfont41, XBrushes.Black, rect, XStringFormats.TopLeft);

                                        offsetX += (divwdth);
                                        if (hghstght < ght)
                                        {
                                            hghstght = ght;
                                        }
                                        if (hgstwidth < wdth)
                                        {
                                            hgstwidth = wdth;
                                        }

                                    }
                                }
                                if (hghstght < 10)
                                {
                                    hghstght = 10;
                                }

                                hgstwidth = 0;
                                offsetX = strtoffsetX;
                                offsetY += (int)hghstght + 5;
                                if (endoffsetY < (offsetY))
                                {
                                    endoffsetY = offsetY;
                                }

                                if (hgstOffsetY < offsetY)
                                {
                                    hgstOffsetY = offsetY;
                                }
                                if ((startY + offsetY) >= (pageHeight + 20))
                                {
                                    page0 = document.AddPage();
                                    if (orntnUsd == "Portrait")
                                    {
                                        page0.Orientation = PageOrientation.Portrait;
                                        page0.Height = XUnit.FromInch(11);
                                        page0.Width = XUnit.FromInch(8.5);
                                    }
                                    else
                                    {
                                        page0.Orientation = PageOrientation.Landscape;
                                        page0.Height = XUnit.FromInch(8.5);
                                        page0.Width = XUnit.FromInch(11);
                                    }
                                    gfx0 = XGraphics.FromPdfPage(page0);
                                    offsetY = 0;
                                    hgstOffsetY = 0;
                                    endoffsetY = 0;
                                }
                            }
                            if (endoffsetY < offsetY)
                            {
                                endoffsetY = offsetY;
                            }
                        }
                    }
                    else
                    {
                    }
                }
            }


            if (islast)
            {
                if (islast)
                {
                    //Slogan: 
                    startX = 40;
                    offsetY = pageHeight - 5;
                    gfx0.DrawLine(aPen, startX, startY + offsetY, startX + pageWidth - 20,
              startY + offsetY);
                    offsetY += font3Hght;
                    nwLn = Global.breakRptTxtDown(
                      Global.getOrgName(Global.UsrsOrg_ID) + "..." +
                      Global.getOrgSlogan(Global.UsrsOrg_ID),
              pageWidth - ght, font5, g);
                    for (int i = 0; i < nwLn.Length; i++)
                    {
                        gfx0.DrawString(nwLn[i]
                        , xfont5, XBrushes.Black, startX, startY + offsetY);
                        offsetY += font5Hght;
                    }
                    //offsetY += font5Hght;
                    nwLn = Global.breakRptTxtDown(
                     "Software Developed by Rhomicom Systems Technologies Ltd.",
              pageWidth + 40, font5, g);
                    for (int i = 0; i < nwLn.Length; i++)
                    {
                        gfx0.DrawString(nwLn[i]
                        , xfont5, XBrushes.Black, startX, startY + offsetY);
                        offsetY += font5Hght;
                    }
                    nwLn = Global.breakRptTxtDown(
              "Website:www.rhomicomgh.com",
              pageWidth + 40, font5, g);
                    for (int i = 0; i < nwLn.Length; i++)
                    {
                        gfx0.DrawString(nwLn[i]
                        , xfont5, XBrushes.Black, startX, startY + offsetY);
                        offsetY += font5Hght;
                    }
                    // Save the document...
                    document.Save(fileNm);

                    if (Global.callngAppType == "DESKTOP")
                    {
                        Global.upldImgsFTP(9, Global.getRptDrctry(), Global.runID.ToString() + ".pdf");
                    }
                }
            }
        }

        public static void exprtDtStToCSV(DataSet dtst, string csvfileNm,
          bool isfirst, bool islast, bool shdAppnd, string rptdlmtr)
        {
            if (isfirst)
            {
            }
            int totl = dtst.Tables[0].Rows.Count;
            string hdrNms = "";
            string lineVals = "";
            string dlmtr = "";
            if (isfirst)
            {
            }
            /*None
      Comma (,)
      Semi-Colon(;)
      Pipe(|)
      Tab
      Tilde(~)*/
            if (rptdlmtr == "None" || rptdlmtr == "Pipe(|)")
            {
                dlmtr = "|";
            }
            else if (rptdlmtr == "Comma (,)")
            {
                dlmtr = ",";
            }
            else if (rptdlmtr == "Semi-Colon(;)")
            {
                dlmtr = ";";
            }
            else if (rptdlmtr == "Tab")
            {
                dlmtr = "\t";
            }
            else if (rptdlmtr == "Tilde(~)")
            {
                dlmtr = "~";
            }
            else
            {
                dlmtr = "|";
            }

            int collen = 0;
            int colcnt = dtst.Tables[0].Columns.Count;
            for (int a = 0; a < colcnt; a++)
            {
                collen = dtst.Tables[0].Columns[a].Caption.Length;
                if (collen >= 3)
                {
                    hdrNms += dtst.Tables[0].Columns[a].Caption + dlmtr;
                }
            }
            if (hdrNms.Length > 0)
            {
                Global.strSB.AppendLine(hdrNms.Remove(hdrNms.Length - 1, 1));
            }

            //Global.strSB.AppendLine(hdrNms);
            for (int i = 0; i < totl; i++)
            {
                lineVals = "";
                for (int a = 0; a < colcnt; a++)
                {
                    collen = dtst.Tables[0].Columns[a].Caption.Length;
                    if (collen >= 3)
                    {
                        lineVals += dtst.Tables[0].Rows[i][a].ToString() + dlmtr;
                    }
                }
                if (lineVals.Length > 0)
                {
                    Global.strSB.AppendLine(lineVals.Remove(lineVals.Length - 1, 1));
                }
            }
            if (islast)
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(csvfileNm, shdAppnd);
                sw.WriteLine(Global.strSB);
                sw.Dispose();
                sw.Close();
                if (Global.callngAppType == "DESKTOP")
                {
                    Global.upldImgsFTP(9, Global.getRptDrctry(), "/" + Global.runID.ToString() + ".csv");
                }

            }
        }

        public static void exprtToHTMLTblr(
            DataSet dtst, string fileNm, string rptTitle
            , string[] colsToGrp, string[] colsToCnt,
            string[] colsToSum, string[] colsToAvrg, string[] colsToFrmt
            , bool isfirst, bool islast, bool shdAppnd)
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
            string cption = "";
            if (isfirst)
            {
                cption = "<caption align=\"top\">" + rptTitle + "</caption>";
                Global.strSB.AppendLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" " +
                  "\"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"[]><html xmlns=\"http://www.w3.org/1999/xhtml\" dir=\"ltr\" lang=\"en-US\" xml:lang=\"en\"><head><meta http-equiv=\"Content-Type\" " +
                    "content=\"text/html; charset=utf-8\"><title>" + rptTitle + "</title>" +
                  "<link rel=\"stylesheet\" href=\"../amcharts/rpt.css\" type=\"text/css\"></head><body>");
                System.IO.File.Copy(Global.getOrgImgsDrctry() + @"\" + Global.UsrsOrg_ID.ToString() + ".png",
                  Global.getRptDrctry() + @"\amcharts_2100\images\" + Global.UsrsOrg_ID.ToString() + ".png", true);

                if (Global.callngAppType == "DESKTOP")
                {
                    Global.upldImgsFTP(9, Global.getRptDrctry(), @"\amcharts_2100\images\" + Global.UsrsOrg_ID.ToString() + ".png");
                }
                //Org Name
                string orgNm = Global.getOrgName(Global.UsrsOrg_ID);
                string pstl = Global.getOrgPstlAddrs(Global.UsrsOrg_ID);
                //Contacts Nos
                string cntcts = Global.getOrgContactNos(Global.UsrsOrg_ID);
                //Email Address
                string email = Global.getOrgEmailAddrs(Global.UsrsOrg_ID);

                Global.strSB.AppendLine("<p><img src=\"../images/" + Global.UsrsOrg_ID.ToString() + ".png\">" +
                  orgNm + "<br/>" + pstl + "<br/>" + cntcts + "<br/>" + email + "<br/>" + "</p>");
            }

            Global.strSB.AppendLine("<table style=\"margin-top:5px;\">" + cption + "<thead>");

            int wdth = 0;
            string finalStr = " ";
            for (int d = 0; d < colCnt; d++)
            {
                string algn = "left";
                int colLen = dtst.Tables[0].Columns[d].ColumnName.Length;
                wdth = (int)Math.Round(((double)colLen / (double)totlLen) * 100, 0);
                if (colLen >= 3)
                {
                    if (Global.mustColBeFrmtd(d.ToString(), colsToFrmt) == true)
                    {
                        algn = "right";
                        finalStr = dtst.Tables[0].Columns[d].ColumnName.Trim().PadLeft(colLen, ' ');
                    }
                    else
                    {
                        finalStr = dtst.Tables[0].Columns[d].ColumnName.Trim() + " ";
                    }
                    Global.strSB.AppendLine("<th align=\"" + algn + "\" width=\"" + wdth +
                      "%\">" + finalStr.Replace(" ", "&nbsp;") + "</th>");
                }
            }

            Global.strSB.AppendLine("</thead><tbody>");

            for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
            {
                Global.strSB.AppendLine("<tr>");
                for (int d = 0; d < colCnt; d++)
                {
                    string algn = "left";
                    double nwval = 0;
                    bool mstgrp = Global.mustColBeGrpd(d.ToString(), colsToGrp);
                    if (Global.mustColBeCntd(d.ToString(), colsToCnt) == true)
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
                    else if (Global.mustColBeSumd(d.ToString(), colsToSum) == true)
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
                    else if (Global.mustColBeAvrgd(d.ToString(), colsToAvrg) == true)
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
                          && (Global.mustColBeGrpd(d.ToString(), colsToGrp) == true))
                        {
                            wdth = (int)Math.Round(((double)colLen / (double)totlLen) * 100, 0);
                            Global.strSB.AppendLine("<td align=\"" + algn + "\"  width=\"" + wdth + "%\">" + " ".Replace(" ", "&nbsp;") + "</td>");//.Replace(" ", "&nbsp;")
                        }
                        else
                        {
                            wdth = (int)Math.Round(((double)colLen / (double)totlLen) * 100, 0);
                            string frsh = " ";
                            if (Global.mustColBeFrmtd(d.ToString(), colsToFrmt) == true)
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
                            Global.strSB.AppendLine("<td align=\"" + algn + "\" width=\"" + wdth + "%\">" + Global.breakTxtDownHTML(frsh,
                              dtst.Tables[0].Columns[d].ColumnName.Length).Replace(" ", "&nbsp;") + "</td>");//.Replace(" ", "&nbsp;")
                        }
                    }
                }
                Global.strSB.AppendLine("</tr>");
            }
            //Populate Counts/Sums/Averages
            Global.strSB.AppendLine("<tr>");

            for (int f = 0; f < colCnt; f++)
            {
                string algn = "left";
                int colLen = dtst.Tables[0].Columns[f].ColumnName.Length;
                finalStr = " ";
                if (colLen >= 3)
                {
                    if (Global.mustColBeCntd(f.ToString(), colsToCnt) == true)
                    {
                        if (Global.mustColBeFrmtd(f.ToString(), colsToFrmt) == true)
                        {
                            algn = "right";
                            finalStr = ("Count = " + colcntVals[f].ToString("#,##0"));
                        }
                        else
                        {
                            finalStr = ("Count = " + colcntVals[f].ToString());
                        }
                    }
                    else if (Global.mustColBeSumd(f.ToString(), colsToSum) == true)
                    {
                        if (Global.mustColBeFrmtd(f.ToString(), colsToFrmt) == true)
                        {
                            algn = "right";
                            finalStr = ("Sum = " + colsumVals[f].ToString("#,##0.00"));
                        }
                        else
                        {
                            finalStr = ("Sum = " + colsumVals[f].ToString());
                        }
                    }
                    else if (Global.mustColBeAvrgd(f.ToString(), colsToAvrg) == true)
                    {
                        if (Global.mustColBeFrmtd(f.ToString(), colsToFrmt) == true)
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
                    Global.strSB.AppendLine("<td align=\"" + algn + "\" width=\"" + wdth + "%\">" + Global.breakTxtDownHTML(finalStr,
                      dtst.Tables[0].Columns[f].ColumnName.Length).Replace(" ", "&nbsp;") + "</td>");//.Replace(" ", "&nbsp;")
                }
            }
            Global.strSB.AppendLine("</tr>");
            Global.strSB.AppendLine("</tbody></table>");
            if (islast)
            {
                Global.strSB.AppendLine("</body></html>");
                System.IO.StreamWriter sw = new System.IO.StreamWriter(fileNm, shdAppnd);
                sw.WriteLine(Global.strSB);
                sw.Dispose();
                sw.Close();
                if (Global.callngAppType == "DESKTOP")
                {
                    Global.upldImgsFTP(9, Global.getRptDrctry(), @"\amcharts_2100\samples\" + Global.runID.ToString() + ".html");
                }
            }
        }

        public static void exprtToHTMLDet(DataSet recsdtst, DataSet grpsdtst, string fileNm, string rptTitle
        , bool isfirst, bool islast, bool shdAppnd, string orntnUsd, string imgCols)
        {
            imgCols = "," + imgCols.Trim(',') + ",";
            string cption = "";
            if (isfirst)
            {
                cption = "<caption align=\"top\">" + rptTitle + "</caption>";
                Global.strSB.AppendLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" " +
                  "\"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"[]><html xmlns=\"http://www.w3.org/1999/xhtml\" dir=\"ltr\" lang=\"en-US\" xml:lang=\"en\"><head><meta http-equiv=\"Content-Type\" " +
                  "content=\"text/html; charset=utf-8\"><title>" + rptTitle + "</title>" +
                  "<link rel=\"stylesheet\" href=\"../amcharts/rpt.css\" type=\"text/css\"></head><body>");
                System.IO.File.Copy(Global.getOrgImgsDrctry() + @"\" + Global.UsrsOrg_ID.ToString() + ".png",
                  Global.getRptDrctry() + @"\amcharts_2100\images\" + Global.UsrsOrg_ID.ToString() + ".png", true);

                if (Global.callngAppType == "DESKTOP")
                {
                    Global.upldImgsFTP(9, Global.getRptDrctry(), @"\amcharts_2100\images\" + Global.UsrsOrg_ID.ToString() + ".png");
                }
                //Org Name
                string orgNm = Global.getOrgName(Global.UsrsOrg_ID);
                string pstl = Global.getOrgPstlAddrs(Global.UsrsOrg_ID);
                //Contacts Nos
                string cntcts = Global.getOrgContactNos(Global.UsrsOrg_ID);
                //Email Address
                string email = Global.getOrgEmailAddrs(Global.UsrsOrg_ID);

                Global.strSB.AppendLine("<p><img src=\"../images/" + Global.UsrsOrg_ID.ToString() + ".png\">" +
                  orgNm + "<br/>" + pstl + "<br/>" + cntcts + "<br/>" + email + "<br/>" + "</p>");
            }

            int fullPgWdthVal = 800;
            if (orntnUsd == "Portrait")
            {
                fullPgWdthVal = 700;
            }


            int wdth = 0;
            string finalStr = " ";
            string algn = "left";
            string[] rptGrpVals = {"Group Title","Group Page Width Type","Group Min-Height",
                             "Show Group Border","Group Display Type","No of Vertical Divs In Group",
                             "Comma Separated Col Nos", "Data Label Max Width%",
                             "Comma Separated Hdr Nms","Column Delimiter","Row Delimiter"};

            string grpTitle = "";
            string grpPgWdth = "";
            int grpMinHght = 0;
            string shwBrdr = "Show";
            string grpDsplyTyp = "Details";
            int grpColDvsns = 4;//Use 1 for Images others 2 or 4
            string colnums = "";
            string lblmaxwdthprcnt = "35";
            string tblrHdrs = "";
            string clmDlmtrs = "";
            string rwDlmtrs = "";

            int divwdth = 0;

            /* 1. For each detail group create a div and fieldset with legend & border based on group settings
             * 2a. if detail display then create required no of td in tr1 of a table, create new tr if no of columns is not exhausted
             *      i.e if no of vertical divs=4 no rows=math.ceil(no cols*0.5)/
             *      else no rows=no cols
             *      for each col display label and data if vrtcl divs is 2 or 4 else display only data
             * 2b. if tabular create table with headers according to defined headers
             *      split data according to rows and cols and display them in this table
             * 2. Get all column nos within the group and create their labels and data using settings
             * 3. if col nos is image then use full defined page width else create no of defined columns count
             * 4. if 
             * 
             */
            int grpdtcnt = grpsdtst.Tables[0].Rows.Count;
            int rowsdtcnt = recsdtst.Tables[0].Rows.Count;
            for (int a = 0; a < rowsdtcnt; a++)
            {
                Global.strSB.AppendLine("<table style=\"margin-top:5px;min-width:" + (fullPgWdthVal + 50).ToString() + "px;\">" + cption + "<tbody>");
                Global.strSB.AppendLine("<tr><td>");
                for (int d = 0; d < grpdtcnt; d++)
                {
                    wdth = 35;
                    grpTitle = grpsdtst.Tables[0].Rows[d][0].ToString();
                    grpPgWdth = grpsdtst.Tables[0].Rows[d][1].ToString();
                    grpMinHght = int.Parse(grpsdtst.Tables[0].Rows[d][2].ToString());
                    shwBrdr = grpsdtst.Tables[0].Rows[d][3].ToString();
                    grpDsplyTyp = grpsdtst.Tables[0].Rows[d][4].ToString();
                    grpColDvsns = int.Parse(grpsdtst.Tables[0].Rows[d][5].ToString());//Use 1 for Images others 2 or 4
                    colnums = grpsdtst.Tables[0].Rows[d][6].ToString();
                    lblmaxwdthprcnt = grpsdtst.Tables[0].Rows[d][7].ToString();
                    tblrHdrs = grpsdtst.Tables[0].Rows[d][8].ToString();
                    clmDlmtrs = grpsdtst.Tables[0].Rows[d][9].ToString();
                    rwDlmtrs = grpsdtst.Tables[0].Rows[d][10].ToString();

                    int.TryParse(lblmaxwdthprcnt, out wdth);

                    if (grpPgWdth == "Half Page Width")
                    {
                        divwdth = (int)(fullPgWdthVal / 2);
                    }
                    else
                    {
                        divwdth = (int)(fullPgWdthVal / 1);
                    }

                    Global.strSB.AppendLine("<div style=\"float:left;min-width:" +
                      (divwdth - 50).ToString() + "px;padding:10px;\">");//min-height:" + (grpMinHght + 20).ToString() + "px;
                    if (shwBrdr == "Show")
                    {
                        Global.strSB.AppendLine("<fieldset style=\"min-width:" + (divwdth - 80).ToString() + "px;\">");//min-height:" + (grpMinHght).ToString() + "px;
                        Global.strSB.AppendLine("<legend>" + grpTitle + "</legend>");
                    }
                    char[] w = { ',' };
                    string[] colNumbers = colnums.Split(w, StringSplitOptions.RemoveEmptyEntries);
                    int noofRws = 1;
                    wdth = ((divwdth - 90) * wdth) / 100;
                    if (grpDsplyTyp == "DETAIL")
                    {
                        if (grpColDvsns == 4)
                        {
                            noofRws = (int)Math.Ceiling((double)colNumbers.Length / (double)2);
                        }
                        else
                        {
                            noofRws = colNumbers.Length;
                        }
                        Global.strSB.AppendLine("<table style=\"min-width:" + (divwdth - 90).ToString() + "px;margin-top:5px;border:none;\" border=\"0\"><tbody>");
                        if (grpColDvsns == 4)
                        {
                            for (int h = 0; h < colNumbers.Length; h++)
                            {
                                if ((h % 2) == 0)
                                {
                                    Global.strSB.AppendLine("<tr>");
                                }
                                int clnm = -1;
                                bool isNmint = int.TryParse(colNumbers[h], out clnm);
                                if (isNmint == true)
                                {
                                    string frsh = "";
                                    Global.strSB.AppendLine("<td style=\"border-bottom:none;border-left:none;font-weight:bolder;\" align=\"" + algn + "\" width=\"" + wdth + "px\">");
                                    frsh = recsdtst.Tables[0].Columns[clnm].Caption.Trim() + ": ";
                                    Global.strSB.AppendLine(Global.breakTxtDownHTML(frsh,
                                      (wdth / 7)).Replace(" ", "&nbsp;"));
                                    Global.strSB.AppendLine("</td>");

                                    Global.strSB.AppendLine("<td style=\"border-bottom:none;border-left:none;\" align=\"" + algn + "\" width=\"" + (divwdth - 90 - wdth) + "px\">");
                                    if (imgCols.Contains("," + clnm + ","))
                                    {
                                        frsh = recsdtst.Tables[0].Rows[a][clnm].ToString().Trim();
                                        if (System.IO.File.Exists(Global.dataBasDir + frsh))
                                        {
                                            System.IO.FileInfo finf = new FileInfo(Global.dataBasDir + frsh);

                                            string extnsn = finf.Extension;
                                            System.IO.File.Copy(Global.dataBasDir + frsh,
                          Global.getRptDrctry() + "/amcharts_2100/images/" + Global.runID.ToString() + "_" + a.ToString() + clnm.ToString() + extnsn, true);

                                            Global.strSB.AppendLine("<p><img src=\"../images/" + Global.runID.ToString() + "_" + a.ToString() + clnm.ToString() + extnsn + "\" style=\"width:auto;height::" + grpMinHght + "px;\">" + "</p>");
                                        }
                                    }
                                    else
                                    {
                                        frsh = recsdtst.Tables[0].Rows[a][clnm].ToString().Trim() + " ";
                                        Global.strSB.AppendLine(Global.breakTxtDownHTML(frsh,
                                          ((divwdth - 90 - wdth) / 7)).Replace(" ", "&nbsp;"));
                                    }
                                    Global.strSB.AppendLine("</td>");
                                }

                                if ((h % 2) == 1)
                                {
                                    Global.strSB.AppendLine("</tr>");
                                }

                            }

                        }
                        else if (grpColDvsns == 2)
                        {
                            for (int h = 0; h < colNumbers.Length; h++)
                            {
                                Global.strSB.AppendLine("<tr>");
                                int clnm = -1;
                                bool isNmint = int.TryParse(colNumbers[h], out clnm);
                                if (isNmint == true)
                                {
                                    string frsh = "";
                                    Global.strSB.AppendLine("<td style=\"border-bottom:none;border-left:none;font-weight:bold;\" align=\"" + algn + "\" width=\"" + wdth + "px\">");
                                    frsh = recsdtst.Tables[0].Columns[clnm].Caption.Trim() + ": ";
                                    Global.strSB.AppendLine(Global.breakTxtDownHTML(frsh,
                                      ((wdth) / 7)).Replace(" ", "&nbsp;"));
                                    Global.strSB.AppendLine("</td>");

                                    Global.strSB.AppendLine("<td style=\"border-bottom:none;border-left:none;\" align=\"" + algn + "\" width=\"" + (divwdth - 90 - wdth) + "px\">");
                                    if (imgCols.Contains("," + clnm + ","))
                                    {
                                        frsh = recsdtst.Tables[0].Rows[a][clnm].ToString().Trim();
                                        if (System.IO.File.Exists(Global.dataBasDir + frsh))
                                        {
                                            System.IO.FileInfo finf = new FileInfo(Global.dataBasDir + frsh);

                                            string extnsn = finf.Extension;
                                            System.IO.File.Copy(Global.dataBasDir + frsh,
                          Global.getRptDrctry() + "/amcharts_2100/images/" + Global.runID.ToString() + "_" + a.ToString() + clnm.ToString() + extnsn, true);

                                            Global.strSB.AppendLine("<p><img src=\"../images/" + Global.runID.ToString() + "_" + a.ToString() + clnm.ToString() + extnsn + "\" style=\"width:auto;height:" + grpMinHght + "px;\">" + "</p>");
                                        }
                                    }
                                    else
                                    {
                                        frsh = recsdtst.Tables[0].Rows[a][clnm].ToString().Trim() + " ";
                                        Global.strSB.AppendLine(Global.breakTxtDownHTML(frsh,
                                          ((divwdth - 90 - wdth) / 7)).Replace(" ", "&nbsp;"));
                                    }
                                    Global.strSB.AppendLine("</td>");
                                }
                                Global.strSB.AppendLine("</tr>");

                            }
                        }
                        else if (grpColDvsns == 1)
                        {
                            for (int h = 0; h < colNumbers.Length; h++)
                            {
                                Global.strSB.AppendLine("<tr>");
                                int clnm = -1;
                                bool isNmint = int.TryParse(colNumbers[h], out clnm);
                                if (isNmint == true)
                                {
                                    string frsh = "";
                                    Global.strSB.AppendLine("<td style=\"border-bottom:none;border-left:none;\" align=\"" + algn + "\" width=\"" + (divwdth - 90) + "px\">");
                                    if (imgCols.Contains("," + clnm + ","))
                                    {
                                        frsh = recsdtst.Tables[0].Rows[a][clnm].ToString().Trim();
                                        if (System.IO.File.Exists(Global.dataBasDir + frsh))
                                        {
                                            System.IO.FileInfo finf = new FileInfo(Global.dataBasDir + frsh);

                                            string extnsn = finf.Extension;
                                            System.IO.File.Copy(Global.dataBasDir + frsh,
                          Global.getRptDrctry() + "/amcharts_2100/images/" + Global.runID.ToString() + "_" + a.ToString() + clnm.ToString() + extnsn, true);

                                            Global.strSB.AppendLine("<p><img src=\"../images/" + Global.runID.ToString() + "_" + a.ToString() + clnm.ToString() + extnsn + "\" style=\"width:auto;height:" + grpMinHght + "px;\">" + "</p>");
                                        }
                                    }
                                    else
                                    {
                                        frsh = recsdtst.Tables[0].Rows[a][clnm].ToString().Trim() + " ";
                                        Global.strSB.AppendLine(Global.breakTxtDownHTML(frsh,
                                          ((divwdth - 90) / 7)).Replace(" ", "&nbsp;"));
                                    }
                                    Global.strSB.AppendLine("</td>");
                                }
                                Global.strSB.AppendLine("</tr>");

                            }
                        }

                        Global.strSB.AppendLine("</tbody></table>");

                    }
                    else
                    {
                    }
                    if (shwBrdr == "Show")
                    {
                        Global.strSB.AppendLine("</fieldset>");
                    }

                    Global.strSB.AppendLine("</div>");
                }
                Global.strSB.AppendLine("</td></tr>");
                Global.strSB.AppendLine("</tbody></table><br/><br/>");
            }


            if (islast)
            {
                Global.strSB.AppendLine("</body></html>");
                System.IO.StreamWriter sw = new System.IO.StreamWriter(fileNm, shdAppnd);
                sw.WriteLine(Global.strSB);
                sw.Dispose();
                sw.Close();
                if (Global.callngAppType == "DESKTOP")
                {
                    Global.upldImgsFTP(9, Global.getRptDrctry(), "/amcharts_2100/samples/" + Global.runID.ToString() + ".html");
                }
            }
        }

        public static void exprtToHTMLSCC(DataSet dtst, string fileNm,
          string rptTitle, string[] colsToGrp, string[] colsToUse
          , bool isfirst, bool islast, bool shdAppnd)
        {
            //Simple Column Chart
            int colCnt = dtst.Tables[0].Columns.Count;

            string cption = "";
            if (isfirst)
            {
                cption = "<caption align=\"top\">" + rptTitle + "</caption>";
                Global.strSB.AppendLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" " +
                "\"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"[]><html xmlns=\"http://www.w3.org/1999/xhtml\" dir=\"ltr\" lang=\"en-US\" xml:lang=\"en\"><head><meta http-equiv=\"Content-Type\" " +
                  "content=\"text/html; charset=utf-8\"><title>" + rptTitle + "</title>");
                Global.strSB.AppendLine("<link rel=\"stylesheet\" href=\"../amcharts/rpt.css\" type=\"text/css\">");
                Global.strSB.AppendLine(@"<link rel=""stylesheet"" href=""style.css"" type=""text/css"">");
                Global.strSB.AppendLine("<script src=\"../amcharts/amcharts.js\" type=\"text/javascript\"></script>");
                Global.strSB.AppendLine("</head><body>");
                System.IO.File.Copy(Global.getOrgImgsDrctry() + @"\" + Global.UsrsOrg_ID.ToString() + ".png",
            Global.getRptDrctry() + @"\amcharts_2100\images\" + Global.UsrsOrg_ID.ToString() + ".png", true);

                if (Global.callngAppType == "DESKTOP")
                {
                    Global.upldImgsFTP(9, Global.getRptDrctry(), @"\amcharts_2100\images\" + Global.UsrsOrg_ID.ToString() + ".png");
                }

                //Org Name
                string orgNm = Global.getOrgName(Global.UsrsOrg_ID);
                string pstl = Global.getOrgPstlAddrs(Global.UsrsOrg_ID);
                //Contacts Nos
                string cntcts = Global.getOrgContactNos(Global.UsrsOrg_ID);
                //Email Address
                string email = Global.getOrgEmailAddrs(Global.UsrsOrg_ID);

                Global.strSB.AppendLine("<p><img src=\"../images/" + Global.UsrsOrg_ID.ToString() + ".png\">" +
                  orgNm + "<br/>" + pstl + "<br/>" + cntcts + "<br/>" + email + "<br/>" + "</p>");
            }

            Global.strSB.AppendLine(@"
        <script type=""text/javascript"">
            var chart;

            var chartData = [");

            for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
            {
                if (a < dtst.Tables[0].Rows.Count - 1)
                {
                    Global.strSB.AppendLine(@"{
                ctgry: """ + dtst.Tables[0].Rows[a][int.Parse(colsToUse[0])].ToString() + @""",
                vals: " + dtst.Tables[0].Rows[a][int.Parse(colsToUse[1])].ToString() + @",
                color: ""#0D52D1""
            },");
                }
                else
                {
                    Global.strSB.AppendLine(@"{
                ctgry: """ + dtst.Tables[0].Rows[a][int.Parse(colsToUse[0])].ToString() + @""",
                vals: " + dtst.Tables[0].Rows[a][int.Parse(colsToUse[1])].ToString() + @",
                color: ""#0D52D1""
            }];");
                }
            }

            //      Global.strSB.AppendLine(@"{
            //                country: ""USA"",
            //                visits: 4025
            //            }, {
            //                country: ""China"",
            //                visits: 1882
            //            }];");


            Global.strSB.AppendLine(@"AmCharts.ready(function () {
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


            Global.strSB.AppendLine("<h2>" + rptTitle + "</h2>");
            Global.strSB.AppendLine("<div id=\"chartdiv\" style=\"width: " + colsToGrp[0] + "px; height: " + colsToGrp[1] + "px;\"></div>");
            if (islast)
            {
                Global.strSB.AppendLine("</body></html>");
                System.IO.StreamWriter sw = new System.IO.StreamWriter(fileNm, shdAppnd);
                sw.WriteLine(Global.strSB);
                sw.Dispose();
                sw.Close();
                if (Global.callngAppType == "DESKTOP")
                {
                    Global.upldImgsFTP(9, Global.getRptDrctry(), @"\amcharts_2100\samples\" + Global.runID.ToString() + ".html");
                }
            }
        }

        public static void exprtToHTMLPC(DataSet dtst, string fileNm,
        string rptTitle, string[] colsToGrp, string[] colsToUse
          , bool isfirst, bool islast, bool shdAppnd)
        {
            //Pie Chart
            //int colCnt = dtst.Tables[0].Columns.Count;
            //for (int p = 0; p < colsToGrp.Length; p++)
            //{
            //  Global.errorLog = "colsToGrp[" + p + "] = " + colsToGrp[p];
            //}
            //for (int p = 0; p < colsToUse.Length; p++)
            //{
            //  Global.errorLog = "colsToUse[" + p + "] = " + colsToUse[p];
            //}
            //Global.writeToLog();

            string cption = "";
            if (isfirst)
            {
                cption = "<caption align=\"top\">" + rptTitle + "</caption>";
                Global.strSB.AppendLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" " +
                "\"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"[]><html xmlns=\"http://www.w3.org/1999/xhtml\" dir=\"ltr\" lang=\"en-US\" xml:lang=\"en\"><head><meta http-equiv=\"Content-Type\" " +
                  "content=\"text/html; charset=utf-8\"><title>" + rptTitle + "</title>" +
                "<link rel=\"stylesheet\" href=\"../amcharts/rpt.css\" type=\"text/css\">");
                Global.strSB.AppendLine(@"<link rel=""stylesheet"" href=""style.css"" type=""text/css"">
        <script src=""../amcharts/amcharts.js"" type=""text/javascript""></script> ");
                Global.strSB.AppendLine("</head><body>");
                System.IO.File.Copy(Global.getOrgImgsDrctry() + @"\" + Global.UsrsOrg_ID.ToString() + ".png",
            Global.getRptDrctry() + @"\amcharts_2100\images\" + Global.UsrsOrg_ID.ToString() + ".png", true);

                if (Global.callngAppType == "DESKTOP")
                {
                    Global.upldImgsFTP(9, Global.getRptDrctry(), @"\amcharts_2100\images\" + Global.UsrsOrg_ID.ToString() + ".png");
                }

                //Org Name
                string orgNm = Global.getOrgName(Global.UsrsOrg_ID);
                string pstl = Global.getOrgPstlAddrs(Global.UsrsOrg_ID);
                //Contacts Nos
                string cntcts = Global.getOrgContactNos(Global.UsrsOrg_ID);
                //Email Address
                string email = Global.getOrgEmailAddrs(Global.UsrsOrg_ID);

                Global.strSB.AppendLine("<p><img src=\"../images/" + Global.UsrsOrg_ID.ToString() + ".png\">" +
                  orgNm + "<br/>" + pstl + "<br/>" + cntcts + "<br/>" + email + "<br/>" + "</p>");
            }
            Global.strSB.AppendLine(@"<script type=""text/javascript"">
            var chart;

            var chartData = [");

            for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
            {
                if (a < dtst.Tables[0].Rows.Count - 1)
                {
                    Global.strSB.AppendLine(@"{
                ctgry: """ + dtst.Tables[0].Rows[a][int.Parse(colsToUse[0])].ToString() + @""",
                vals: " + dtst.Tables[0].Rows[a][int.Parse(colsToUse[1])].ToString() + @"
            },");
                }
                else
                {
                    Global.strSB.AppendLine(@"{
                ctgry: """ + dtst.Tables[0].Rows[a][int.Parse(colsToUse[0])].ToString() + @""",
                vals: " + dtst.Tables[0].Rows[a][int.Parse(colsToUse[1])].ToString() + @"
            }];");
                }
            }

            Global.strSB.AppendLine(@"AmCharts.ready(function () {
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


            Global.strSB.AppendLine("<h2>" + rptTitle + "</h2>");
            Global.strSB.AppendLine("<div id=\"chartdiv\" style=\"width: " + colsToGrp[0] +
              "px; height: " + colsToGrp[1] + "px;\"></div>");
            if (islast)
            {
                Global.strSB.AppendLine("</body></html>");
                System.IO.StreamWriter sw = new System.IO.StreamWriter(fileNm, shdAppnd);
                sw.WriteLine(Global.strSB);
                sw.Dispose();
                sw.Close();
                if (Global.callngAppType == "DESKTOP")
                {
                    Global.upldImgsFTP(9, Global.getRptDrctry(), @"\amcharts_2100\samples\" + Global.runID.ToString() + ".html");
                }
            }
        }

        public static void exprtToHTMLLC(DataSet dtst, string fileNm,
    string rptTitle, string[] colsToGrp, string[] colsToUse
            , bool isfirst, bool islast, bool shdAppnd)
        {
            //Line Chart
            int colCnt = colsToUse.Length;

            string cption = "";
            if (isfirst)
            {
                cption = "<caption align=\"top\">" + rptTitle + "</caption>";
                Global.strSB.AppendLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" " +
                "\"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"[]><html xmlns=\"http://www.w3.org/1999/xhtml\" dir=\"ltr\" lang=\"en-US\" xml:lang=\"en\"><head><meta http-equiv=\"Content-Type\" " +
                  "content=\"text/html; charset=utf-8\"><title>" + rptTitle + "</title>" +
                "<link rel=\"stylesheet\" href=\"../amcharts/rpt.css\" type=\"text/css\">");
                Global.strSB.AppendLine(@"<link rel=""stylesheet"" href=""style.css"" type=""text/css"">
        <script src=""../amcharts/amcharts.js"" type=""text/javascript""></script>");
                Global.strSB.AppendLine("</head><body>");
                System.IO.File.Copy(Global.getOrgImgsDrctry() + @"\" + Global.UsrsOrg_ID.ToString() + ".png",
            Global.getRptDrctry() + @"\amcharts_2100\images\" + Global.UsrsOrg_ID.ToString() + ".png", true);

                if (Global.callngAppType == "DESKTOP")
                {
                    Global.upldImgsFTP(9, Global.getRptDrctry(), @"\amcharts_2100\images\" + Global.UsrsOrg_ID.ToString() + ".png");
                }
                //Org Name
                string orgNm = Global.getOrgName(Global.UsrsOrg_ID);
                string pstl = Global.getOrgPstlAddrs(Global.UsrsOrg_ID);
                //Contacts Nos
                string cntcts = Global.getOrgContactNos(Global.UsrsOrg_ID);
                //Email Address
                string email = Global.getOrgEmailAddrs(Global.UsrsOrg_ID);

                Global.strSB.AppendLine("<p><img src=\"../images/" + Global.UsrsOrg_ID.ToString() + ".png\">" +
                  orgNm + "<br/>" + pstl + "<br/>" + cntcts + "<br/>" + email + "<br/>" + "</p>");
            }
            Global.strSB.AppendLine(@"<script type=""text/javascript"">
            var chart;

            var chartData = [");

            for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
            {
                if (a < dtst.Tables[0].Rows.Count - 1)
                {
                    Global.strSB.AppendLine(@"{
                ctgry: """ + dtst.Tables[0].Rows[a][int.Parse(colsToUse[0])].ToString() + @""",
                value: " + dtst.Tables[0].Rows[a][int.Parse(colsToUse[1])].ToString() + @"
            },");
                }
                else
                {
                    Global.strSB.AppendLine(@"{
                ctgry: """ + dtst.Tables[0].Rows[a][int.Parse(colsToUse[0])].ToString() + @""",
                value: " + dtst.Tables[0].Rows[a][int.Parse(colsToUse[1])].ToString() + @"
            }];");
                }
            }


            Global.strSB.AppendLine(@"AmCharts.ready(function () {
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

            Global.strSB.AppendLine("<h2>" + rptTitle + "</h2>");
            Global.strSB.AppendLine("<div id=\"chartdiv\" style=\"width: " + colsToGrp[0] +
              "px; height: " + colsToGrp[1] + "px;\"></div>");
            if (islast)
            {
                Global.strSB.AppendLine("</body></html>");
                System.IO.StreamWriter sw = new System.IO.StreamWriter(fileNm, shdAppnd);
                sw.WriteLine(Global.strSB);
                sw.Dispose();
                sw.Close();
                if (Global.callngAppType == "DESKTOP")
                {
                    Global.upldImgsFTP(9, Global.getRptDrctry(), @"\amcharts_2100\samples\" + Global.runID.ToString() + ".html");
                }
            }
        }

        public static string get_user_name(long userID)
        {
            //Gets the last password change date 
            string sqlStr = "SELECT user_name FROM " +
            "sec.sec_users WHERE user_id = " + userID + "";
            DataSet dtSt = new DataSet();
            dtSt = Global.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public static string get_Gnrl_Rec_Hstry(long rowID, string tblnm, string id_col_nm)
        {
            string strSQL = @"SELECT a.created_by, 
to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), 
a.last_update_by, 
to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') " +
            "FROM " + tblnm + " a WHERE(a." + id_col_nm + " = " + rowID + ")";
            string fnl_str = "";
            DataSet dtst = Global.selectDataNoParams(strSQL);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                fnl_str = "CREATED BY: " + Global.get_user_name(long.Parse(dtst.Tables[0].Rows[0][0].ToString())) +
                 "\r\nCREATION DATE: " + dtst.Tables[0].Rows[0][1].ToString() + "\r\nLAST UPDATE BY:" +
                 Global.get_user_name(long.Parse(dtst.Tables[0].Rows[0][2].ToString())) +
                 "\r\nLAST UPDATE DATE: " + dtst.Tables[0].Rows[0][3].ToString();
                return fnl_str;
            }
            else
            {
                return "";
            }
        }

        public static string get_Gnrl_Create_Hstry(long rowID, string tblnm, string id_col_nm)
        {
            string strSQL = @"SELECT a.created_by, 
to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') " +
            "FROM " + tblnm + " a WHERE(a." + id_col_nm + " = " + rowID + ")";
            string fnl_str = "";
            DataSet dtst = Global.selectDataNoParams(strSQL);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                fnl_str = "CREATED BY: " + Global.get_user_name(long.Parse(dtst.Tables[0].Rows[0][0].ToString())) +
                 "\r\nCREATION DATE: " + dtst.Tables[0].Rows[0][1].ToString();
                return fnl_str;
            }
            else
            {
                return "";
            }
        }

        public static int getUsrOrgID(long usrID)
        {
            //sec.get_usr_prsn_id(
            string sqlStr = "SELECT org_id FROM " +
            "prs.prsn_names_nos WHERE person_id = sec.get_usr_prsn_id(" + usrID + ")";
            DataSet dtSt = new DataSet();
            dtSt = Global.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public static long getGnrlRecID(string tblNm, string srchcol, string rtrnCol,
          string recname, int orgid)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select " + rtrnCol + " from " + tblNm + " where lower(" + srchcol + ") = '" +
             recname.Replace("'", "''").ToLower() + "' and org_id = " + orgid;
            dtSt = Global.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public static long getGnrlRecID(string tblNm, string srchcol, string rtrnCol, string recname)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select " + rtrnCol + " from " + tblNm + " where lower(" + srchcol + ") = '" +
             recname.Replace("'", "''").ToLower() + "'";
            dtSt = Global.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public static long getGnrlRecID(string tblNm, string srchcolForNM, string srchcolForID, string rtrnCol, string recname, long recID)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select " + rtrnCol + " from " + tblNm + " where lower(" + srchcolForNM + ") = '" +
             recname.Replace("'", "''").ToLower() + "' and " + srchcolForID + " = " + recID;
            dtSt = Global.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public static string getGnrlRecNm(string tblNm, string srchcol, string rtrnCol, long recid)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select " + rtrnCol + " from " + tblNm + " where " + srchcol + " = " + recid;
            dtSt = Global.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public static string getGnrlRecNm(string tblNm, string srchcol, string rtrnCol, string srchword)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select " + rtrnCol + " from " + tblNm + " where " + srchcol + " = '" + srchword.Replace("'", "''") + "'";
            dtSt = Global.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public static string getDB_Date_time()
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select to_char(now(), 'YYYY-MM-DD HH24:MI:SS')";
            dtSt = Global.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public static string getFrmtdDB_Date_time()
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select to_char(now(), 'DD-Mon-YYYY HH24:MI:SS')";
            dtSt = Global.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public static string[] getMachDetails()
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

        public static void updatePrcsRnnr(long rnnrID, string lstActvTm, string stats)
        {
            //string dateStr = Global.getDB_Date_time();
            string insSQL = @"UPDATE rpt.rpt_prcss_rnnrs SET 
rnnr_lst_actv_dtetme='" + lstActvTm.Replace("'", "''") +
           "', last_update_by=-1, last_update_date='" + lstActvTm +
           "', rnnr_status='" + stats.Replace("'", "''") +
           "' WHERE prcss_rnnr_id = " + rnnrID;
            Global.updateDataNoParams(insSQL);
        }

        public static bool isRunnrRnng(string rnnrNm)
        {
            string selSQL = @"SELECT 1 
       FROM rpt.rpt_prcss_rnnrs WHERE rnnr_name='" + rnnrNm.Replace("'", "''") + @"' and age(now(), 
        to_timestamp(CASE WHEN rnnr_lst_actv_dtetme='' THEN '2013-01-01 00:00:00' ELSE rnnr_lst_actv_dtetme END, 'YYYY-MM-DD HH24:MI:SS')) " +
              @"<= interval '300 second'";
            DataSet dtst = Global.selectDataNoParams(selSQL);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool isRunnrRnngOnThsMch(string rnnrNm, string macAddrs, string ipAddrs)
        {
            string selSQL = @"SELECT prcss_rnnr_id 
       FROM rpt.rpt_prcss_rnnrs WHERE rnnr_name='" + rnnrNm.Replace("'", "''") +
             "' and rnnr_status ilike '%" + macAddrs + "%' and rnnr_status ilike '%" + ipAddrs + "%'";
            DataSet dtst = Global.selectDataNoParams(selSQL);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static DataSet get_AllPrgmUnts(long rptID)
        {
            string strSql = @"SELECT program_unit_id, 
rpt.get_rpt_name(program_unit_id) prg_nm
        FROM rpt.rpt_set_prgrm_units " +
              "WHERE report_set_id = " + rptID + "";

            //Global.mnFrm.roles_SQL = strSql;
            DataSet dtst = Global.selectDataNoParams(strSql);
            return dtst;
        }

        public static DataSet get_AllGrpngs(long rptID)
        {
            string strSql = @"SELECT title, grp_width_desc, grp_min_height_px, grp_border, 
       grp_dsply_type, nof_cols_wthn, col_nos, label_max_width,  
       column_hdr_names, delimiter_col_vals, delimiter_row_vals, 
        grp_order, group_id
  FROM rpt.rpt_det_rpt_grps WHERE report_id = " + rptID + " ORDER BY grp_order, group_id";
            //Global.mnFrm.params_SQL = strSql;

            DataSet dtst = Global.selectDataNoParams(strSql);
            return dtst;
        }

        public static void updatePrcsRnnrCmd(string rnnrNm, string cmdStr, long uid)
        {
            string dateStr = Global.getDB_Date_time();
            string insSQL = @"UPDATE rpt.rpt_prcss_rnnrs SET 
            shld_rnnr_stop='" + cmdStr.Replace("'", "''") +
           "', last_update_by=" + uid + ", last_update_date='" + dateStr +
           "' WHERE rnnr_name = '" + rnnrNm.Replace("'", "''") + "'";
            Global.insertDataNoParams(insSQL);
        }

        public static void updateRptRnStopCmd(long rptrnid, string cmdStr)
        {
            string dateStr = Global.getDB_Date_time();
            string updtSQL = "UPDATE rpt.rpt_report_runs SET " +
                     "shld_run_stop = '" + cmdStr.Replace("'", "''") +
             "' WHERE (rpt_run_id = " + rptrnid + ")";
            Global.updateDataNoParams(updtSQL);
        }

        public static DataSet get_UsrRunsNtRnng()
        {
            string selSQL = @"SELECT MIN(a.rpt_run_id), a.report_id, a.run_by  
        FROM rpt.rpt_report_runs a 
        WHERE a.is_this_from_schdler = '0' and a.run_status_txt != 'Completed!'
        and a.run_status_txt != 'Error!' and a.shld_run_stop = '0' 
        and a.run_status_prct < 100 and a.last_actv_date_tme != ''
        and age(now(), to_timestamp(a.last_actv_date_tme, 'YYYY-MM-DD HH24:MI:SS'))
        > interval '50 second' 
        GROUP BY a.report_id, a.run_by 
        ORDER BY 1 ASC";
            DataSet dtst = Global.selectDataNoParams(selSQL);
            return dtst;
        }

        public static DataSet get_SchdldRunsNtRnng()
        {
            string selSQL = @"SELECT MIN(a.rpt_run_id), a.report_id, a.run_by  
        FROM rpt.rpt_report_runs a 
        WHERE a.is_this_from_schdler = '1' and a.alert_id<=0 and a.run_status_txt != 'Completed!'
        and a.run_status_txt != 'Error!' and a.shld_run_stop = '0' 
        and a.run_status_prct < 100 and a.last_actv_date_tme != ''
        and age(now(), to_timestamp(a.last_actv_date_tme, 'YYYY-MM-DD HH24:MI:SS'))
        > interval '50 second' 
and a.report_id IN (SELECT  a.report_id
       FROM rpt.rpt_run_schdules a, rpt.rpt_reports b 
        WHERE a.report_id=b.report_id and a.repeat_every >0 and 
        (CASE WHEN run_at_spcfd_hour='0' and age(now(), to_timestamp(to_char(now(),'YYYY-MM-DD')|| ' ' || 
        to_char(to_timestamp(start_dte_tme, 'YYYY-MM-DD HH24:MI:SS'),'HH24:MI:SS'), 'YYYY-MM-DD HH24:MI:SS'))>= interval '1 second' THEN
        1 
        WHEN run_at_spcfd_hour='1' and to_char(now(),'HH24:00:00')=to_char(to_timestamp(start_dte_tme, 'YYYY-MM-DD HH24:MI:SS'),'HH24:00:00') THEN
        1 
        ELSE
        0
	END) =1)
        GROUP BY a.report_id, a.run_by 
        ORDER BY 1 ASC";
            DataSet dtst = Global.selectDataNoParams(selSQL);
            return dtst;
        }

        public static DataSet get_SchdldAlertsNtRnng()
        {
            string selSQL = @"SELECT MIN(a.rpt_run_id), a.report_id, a.run_by  
        FROM rpt.rpt_report_runs a 
        WHERE /*a.is_this_from_schdler = '1' and*/ a.alert_id>0 and a.run_status_txt != 'Completed!'
        and a.run_status_txt != 'Error!' and a.shld_run_stop = '0' 
        and a.run_status_prct < 100 and a.last_actv_date_tme != ''
        and age(now(), to_timestamp(a.last_actv_date_tme, 'YYYY-MM-DD HH24:MI:SS'))
        > interval '50 second' 
and a.report_id IN (SELECT  a.report_id
       FROM alrt.alrt_alerts a, rpt.rpt_reports b 
        WHERE a.report_id=b.report_id and a.repeat_every >0 and 
        (CASE WHEN run_at_spcfd_hour='0' and age(now(), to_timestamp(to_char(now(),'YYYY-MM-DD')|| ' ' || 
        to_char(to_timestamp(start_dte_tme, 'YYYY-MM-DD HH24:MI:SS'),'HH24:MI:SS'), 'YYYY-MM-DD HH24:MI:SS'))>= interval '1 second' THEN
        1 
        WHEN run_at_spcfd_hour='1' and chartoint(to_char(now(),'HH24')) >= chartoint(to_char(to_timestamp(start_dte_tme, 'YYYY-MM-DD HH24:MI:SS'),'HH24')) and chartoint(to_char(now(),'HH24'))<end_hour THEN
        1 
        ELSE
        0
	END) =1)
        GROUP BY a.report_id, a.run_by 
        ORDER BY 1 ASC";
            DataSet dtst = Global.selectDataNoParams(selSQL);
            return dtst;
        }

        public static DataSet get_UserAlertsNtRnng()
        {
            string selSQL = @"SELECT MIN(a.rpt_run_id), a.report_id, a.run_by  
        FROM rpt.rpt_report_runs a 
        WHERE /*a.is_this_from_schdler = '1' and*/ a.alert_id>0 and a.run_status_txt != 'Completed!'
        and a.run_status_txt != 'Error!' and a.shld_run_stop = '0' 
        and a.run_status_prct < 100 and a.last_actv_date_tme != ''
        and age(now(), to_timestamp(a.last_actv_date_tme, 'YYYY-MM-DD HH24:MI:SS'))
        > interval '50 second' 
      and a.report_id NOT IN (SELECT  a.report_id
       FROM alrt.alrt_alerts a, rpt.rpt_reports b 
        WHERE a.report_id=b.report_id and a.repeat_every >0 and 
        (CASE WHEN run_at_spcfd_hour='0' and age(now(), to_timestamp(to_char(now(),'YYYY-MM-DD')|| ' ' || 
        to_char(to_timestamp(start_dte_tme, 'YYYY-MM-DD HH24:MI:SS'),'HH24:MI:SS'), 'YYYY-MM-DD HH24:MI:SS'))>= interval '1 second' THEN
        1 
        WHEN run_at_spcfd_hour='1' and chartoint(to_char(now(),'HH24')) >= chartoint(to_char(to_timestamp(start_dte_tme, 'YYYY-MM-DD HH24:MI:SS'),'HH24')) and chartoint(to_char(now(),'HH24'))<end_hour THEN
        1 
        ELSE
        0
	END) =1)
        GROUP BY a.report_id, a.run_by 
        ORDER BY 1 ASC";
            DataSet dtst = Global.selectDataNoParams(selSQL);
            return dtst;
        }

        public static DataSet get_Schdules()
        {
            string selSQL = @"SELECT a.schedule_id, a.report_id, b.report_name, a.start_dte_tme, 
        a.repeat_every, trim(lower(trim(both '(s)' from a.repeat_uom))) uom, a.created_by 
       FROM rpt.rpt_run_schdules a, rpt.rpt_reports b 
        WHERE a.report_id=b.report_id and a.repeat_every >0 and 
        (CASE WHEN run_at_spcfd_hour='0' and age(now(), to_timestamp(to_char(now(),'YYYY-MM-DD')|| ' ' || 
        to_char(to_timestamp(start_dte_tme, 'YYYY-MM-DD HH24:MI:SS'),'HH24:MI:SS'), 'YYYY-MM-DD HH24:MI:SS'))>= interval '1 second' THEN
        1 
        WHEN run_at_spcfd_hour='1' and to_char(now(),'HH24:00:00')=to_char(to_timestamp(start_dte_tme, 'YYYY-MM-DD HH24:MI:SS'),'HH24:00:00') THEN
        1 
        ELSE
        0
	END) =1
         ORDER BY a.schedule_id DESC";
            DataSet dtst = Global.selectDataNoParams(selSQL);
            return dtst;
        }

        public static DataSet get_AlertSchdules()
        {
            string selSQL = @"SELECT a.alert_id, a.report_id, b.report_name, a.start_dte_tme, 
        a.repeat_every, trim(lower(trim(both '(s)' from a.repeat_uom))) uom, a.created_by,
        a.to_mail_num_list_mnl, a.cc_mail_num_list_mnl, a.bcc_mail_num_list_mnl, 
        a.msg_sbjct_mnl, a.alert_msg_body_mnl, a.attchment_urls, a.alert_type, a.end_hour  
       FROM alrt.alrt_alerts a, rpt.rpt_reports b 
        WHERE a.report_id = b.report_id and a.repeat_every >0 and 
        (CASE WHEN run_at_spcfd_hour='0' and age(now(), to_timestamp(to_char(now(),'YYYY-MM-DD')|| ' ' || 
        to_char(to_timestamp(start_dte_tme, 'YYYY-MM-DD HH24:MI:SS'),'HH24:MI:SS'), 'YYYY-MM-DD HH24:MI:SS'))>= interval '1 second' THEN
        1 
        WHEN run_at_spcfd_hour='1' and to_char(now(),'HH24:00:00')=to_char(to_timestamp(start_dte_tme, 'YYYY-MM-DD HH24:MI:SS'),'HH24:00:00') THEN
        1 
        ELSE
        0
	END) =1
         ORDER BY a.alert_id DESC";
            DataSet dtst = Global.selectDataNoParams(selSQL);
            return dtst;
        }

        public static DataSet get_AlertSchdules(long rptID)
        {
            string selSQL = @"SELECT a.alert_id, a.report_id, b.report_name, a.start_dte_tme, 
        a.repeat_every, trim(lower(trim(both '(s)' from a.repeat_uom))) uom, a.created_by,
        a.to_mail_num_list_mnl, a.cc_mail_num_list_mnl, a.bcc_mail_num_list_mnl, 
        a.msg_sbjct_mnl, a.alert_msg_body_mnl, a.attchment_urls, a.alert_type, a.end_hour  
       FROM alrt.alrt_alerts a, rpt.rpt_reports b 
        WHERE a.report_id = b.report_id and a.repeat_every >0 and 
        (CASE WHEN run_at_spcfd_hour='0' and age(now(), to_timestamp(to_char(now(),'YYYY-MM-DD')|| ' ' || 
        to_char(to_timestamp(start_dte_tme, 'YYYY-MM-DD HH24:MI:SS'),'HH24:MI:SS'), 'YYYY-MM-DD HH24:MI:SS'))>= interval '1 second' THEN
        1 
        WHEN run_at_spcfd_hour='1' and to_char(now(),'HH24:00:00')=to_char(to_timestamp(start_dte_tme, 'YYYY-MM-DD HH24:MI:SS'),'HH24:00:00') THEN
        1 
        ELSE
        0
	END) =1 and a.report_id = " + rptID + " ORDER BY a.alert_id DESC";
            DataSet dtst = Global.selectDataNoParams(selSQL);
            return dtst;
        }

        public static long getCurDBConns()
        {
            string selSQL = "select count(1) from pg_stat_activity";
            DataSet dtst = Global.selectDataNoParams(selSQL);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return 0;
        }

        public static long getMxAllwdDBConns()
        {
            int lovID = Global.getLovID("Max Allowed Concurrent Connections");
            long rslt = 0;
            bool scs = long.TryParse(Global.getEnbldPssblVal(lovID), out rslt);

            if (scs)
            {
                return rslt;
            }
            else
            {
                return 4;
            }
        }

        public static long getRptRnID(long rptID, long runBy, string runDate)
        {
            //     runDate = DateTime.ParseExact(
            //runDate, "dd-MMM-yyyy HH:mm:ss",
            //System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            DataSet dtSt = new DataSet();
            string sqlStr = "select rpt_run_id from rpt.rpt_report_runs where run_by = " +
              runBy + " and report_id = " + rptID + " and run_date = '" +
             runDate + "' order by rpt_run_id DESC";
            dtSt = Global.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public static DataSet get_SchdulesParams(long schdlID)
        {
            string selSQL = @"SELECT a.schdl_param_id, a.parameter_id, b.parameter_name, a.parameter_value
      FROM rpt.rpt_run_schdule_params a, rpt.rpt_report_parameters b  
      WHERE a.parameter_id = b.parameter_id and a.schedule_id=" + schdlID + " ORDER BY a.parameter_id";
            DataSet dtst = Global.selectDataNoParams(selSQL);
            return dtst;
        }

        public static DataSet get_AlertParams(long alertID)
        {
            string selSQL = @"SELECT a.schdl_param_id, a.parameter_id, b.parameter_name, a.parameter_value
      FROM rpt.rpt_run_schdule_params a, rpt.rpt_report_parameters b  
      WHERE a.parameter_id = b.parameter_id and a.alert_id=" + alertID + " ORDER BY a.parameter_id";
            DataSet dtst = Global.selectDataNoParams(selSQL);
            return dtst;
        }

        public static DataSet get_RptParams(long rptID)
        {
            string selSQL = @"SELECT a.parameter_id, a.parameter_name, a.paramtr_rprstn_nm_in_query,a.default_value
      FROM rpt.rpt_report_parameters a WHERE a.report_id = " + rptID + " ORDER BY a.parameter_id";
            DataSet dtst = Global.selectDataNoParams(selSQL);
            return dtst;
        }

        public static void createSchdldRptRn(long runBy, string runDate,
     long rptID, string paramIDs, string paramVals,
         string outptUsd, string orntUsd, int alertID, long msgSentID)
        {
            string insSQL = @"INSERT INTO rpt.rpt_report_runs(
            run_by, run_date, rpt_run_output, run_status_txt, 
            run_status_prct, report_id, rpt_rn_param_ids, rpt_rn_param_vals, 
            output_used, orntn_used, last_actv_date_tme, is_this_from_schdler, alert_id, msg_sent_id) " +
                  "VALUES (" + runBy + ", '" + runDate +
                  "', '', 'Not Started!', 0, " + rptID + ", '" + paramIDs.Replace("'", "''") +
                  "', '" + paramVals.Replace("'", "''") +
                  "', '" + outptUsd.Replace("'", "''") +
                  "', '" + orntUsd.Replace("'", "''") +
                  "', '" + runDate + "', '1'," + alertID + "," + msgSentID + ")";
            Global.insertDataNoParams(insSQL);
        }

        public static void createAlertMsgSent(long msgSntID, string toList,
     string ccLst, string msgBdy, string dteSent,
         string sbjct, long rptID, string bccLst,
          long prsnID, long cstmrSupID, int alertID, string attchMns, string msg_type)
        {
            string runDate = Global.getDB_Date_time();
            string insSQL = @"INSERT INTO alrt.alrt_msgs_sent(
            msg_sent_id, to_list, cc_list, msg_body, date_sent, msg_sbjct, 
            report_id, bcc_list, person_id, cstmr_spplr_id, created_by, creation_date, 
            alert_id, sending_status, err_msg, attch_urls, msg_type) " +
                  "VALUES (" + msgSntID + ", '" + toList.Replace("'", "''") +
                  "', '" + ccLst.Replace("'", "''") +
                  "', '" + msgBdy.Replace("'", "''") +
                  "', '" + runDate.Replace("'", "''") +
                  "', '" + sbjct.Replace("'", "''") +
                  "', " + rptID +
                  ", '" + bccLst.Replace("'", "''") +
                  "', " + prsnID +
                  ", " + cstmrSupID +
                  ", " + Global.rnUser_ID +
                  ", '" + runDate.Replace("'", "''") +
                  "', " + alertID + ",'0','','" + attchMns.Replace("'", "''") + "','" + msg_type.Replace("'", "''") + "')";
            Global.insertDataNoParams(insSQL);
        }

        public static void updateAlertMsgSent(long msgSntID, string dteSent,
         string sentStatus, string errMsg)
        {
            //string runDate = Global.getDB_Date_time();
            string updateSQL = @"UPDATE alrt.alrt_msgs_sent SET 
            date_sent='" + dteSent.Replace("'", "''") +
                  "', sending_status='" + sentStatus + "', err_msg='" + errMsg + "' " +
                  "WHERE msg_sent_id = " + msgSntID + "";
            Global.updateDataNoParams(updateSQL);
        }

        public static bool doesLstRnTmExcdIntvl(long rptID, string intrvl, long rn_ID)
        {
            string sqlStr = @"select age(now(), to_timestamp(CASE WHEN last_actv_date_tme='' 
        THEN '2013-01-01 00:00:00' ELSE last_actv_date_tme END, 'YYYY-MM-DD HH24:MI:SS'))
        >= interval '" + intrvl + "' from rpt.rpt_report_runs where report_id = " + rptID +
            " and rpt_run_id != " + rn_ID + " and last_actv_date_tme !='' " +
            "ORDER BY last_actv_date_tme DESC, rpt_run_id DESC LIMIT 1 OFFSET 0";
            //and is_this_from_schdler = '1' and is_this_from_schdler='1' 
            DataSet dtst = Global.selectDataNoParams(sqlStr);
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
                return true;
            }
        }

        public static string[] getFTPServerDet()
        {
            string selSQL = "select a.ftp_server_url, a.ftp_app_sub_directory, "
              + "a.ftp_user_name, a.ftp_user_pswd, a.ftp_port, a.enforce_ftp " +
              "from sec.sec_email_servers a where a.is_default='t'";
            DataSet dtst = Global.selectDataNoParams(selSQL);
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

        public static void dwnldImgsFTP(int folderTyp, string locfolderNm, string locfileNm)
        {
            string[] srvr = Global.getFTPServerDet();
            string subdir = "";
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
                subdir = "/Rpts/jrxmls";
            }
            else if (folderTyp == 10)
            {
                subdir = "/AttnDocs";
            }
            else if (folderTyp == 11)
            {
                subdir = "/AssetDocs";
            }
            else if (folderTyp == 12)
            {
                subdir = "/PyblDocs";
            }
            else if (folderTyp == 13)
            {
                subdir = "/RcvblDocs";
            }
            else if (folderTyp == 14)
            {
                subdir = "/FirmsDocs";
            }
            string fullFtpFileFUrl = srvr[0] + srvr[1] + subdir + @"/" + locfileNm;
            string fullLocFileUrl = locfolderNm + @"/" + locfileNm;
            string userName = srvr[2];
            string password = Global.decrypt(srvr[3], Global.AppKey);
            Global.threadTen = new Thread(() => Global.Uploadfunc(fullFtpFileFUrl, fullLocFileUrl,
            userName, password));
            Global.threadTen.Name = "ThreadTen";
            Global.threadTen.Priority = ThreadPriority.Lowest;
            Global.threadTen.Start();
            //    Global.DownloadFile(srvr[0] + srvr[1] + subdir + @"/" + locfileNm,
            //      locfolderNm + @"/" + locfileNm, srvr[2],
            //Global.decrypt(srvr[3], Global.AppKey));
        }

        static void Downloadfunc(string fullFtpFileFUrl, string fullLocFileUrl,
        string userName, string password)
        {
            try
            {
                Global.DownloadFile(fullFtpFileFUrl, fullLocFileUrl,
            userName, password);
            }
            catch (System.Threading.ThreadAbortException thex)
            {
                Program.killThreads();
            }
            catch (Exception ex)
            {
                //write to log file
                Global.errorLog = ex.Message + "\r\n" + ex.StackTrace + "\r\n" + ex.InnerException + "\r\n";
                Global.writeToLog();
                if (threadTen.IsAlive)
                {
                    threadTen.Abort();
                }
            }
            finally
            {
            }
        }

        public static void upldImgsFTP(int folderTyp, string locfolderNm, string locfileNm)
        {
            string subdir = "";
            string[] srvr = Global.getFTPServerDet();
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
                subdir = "/Rpts/jrxmls";
            }
            else if (folderTyp == 10)
            {
                subdir = "/AttnDocs";
            }
            else if (folderTyp == 11)
            {
                subdir = "/AssetDocs";
            }
            else if (folderTyp == 12)
            {
                subdir = "/PyblDocs";
            }
            else if (folderTyp == 13)
            {
                subdir = "/RcvblDocs";
            }
            else if (folderTyp == 14)
            {
                subdir = "/FirmsDocs";
            }
            //    Global.showMsg(srvr[0] + srvr[1] + subdir + @"/" + locfileNm +
            //      locfolderNm + @"\" + locfileNm + srvr[2] +
            //Global.decrypt(srvr[3]), 0);

            string fullFtpFileFUrl = srvr[0] + srvr[1] + subdir + @"/" + locfileNm;
            string fullLocFileUrl = locfolderNm + @"/" + locfileNm;
            string userName = srvr[2];
            string password = Global.decrypt(srvr[3], Global.AppKey);
            Global.threadNine = new Thread(() => Global.Uploadfunc(fullFtpFileFUrl, fullLocFileUrl,
            userName, password));
            Global.threadNine.Name = "ThreadNine";
            Global.threadNine.Priority = ThreadPriority.Lowest;
            Global.threadNine.Start();
        }

        static void Uploadfunc(string fullFtpFileFUrl, string fullLocFileUrl,
        string userName, string password)
        {
            try
            {
                Global.UploadFile(fullFtpFileFUrl, fullLocFileUrl,
            userName, password);
            }
            catch (System.Threading.ThreadAbortException thex)
            {
                Program.killThreads();
            }
            catch (Exception ex)
            {
                //write to log file
                Global.errorLog = ex.Message + "\r\n" + ex.StackTrace + "\r\n" + ex.InnerException + "\r\n";
                Global.writeToLog();
                if (threadNine.IsAlive)
                {
                    threadNine.Abort();
                }
            }
            finally
            {
            }
        }

        public static void dwnldImgsDir(int folderTyp, string in_dir)
        {
            string[] srvr = Global.getFTPServerDet();
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
            string[] files = Global.GetFileList(srvr[0] + srvr[1] + subdir + @"/", in_dir, srvr[2],
        Global.decrypt(srvr[3], Global.AppKey));
            foreach (string file in files)
            {
                //Global.showMsg(in_dir + file, 0);

                if (folderTyp == 9)
                {
                    locfolderNm = Global.getRptDrctry();
                }
                Global.dwnldImgsFTP(folderTyp, locfolderNm, in_dir + file);
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
        public static string UploadFile(string fullFtpFileFUrl, string fullLocFileUrl,
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
                req.KeepAlive = false;
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
            catch (Exception ex)
            {
                //Global.showMsg(fullFtpFileFUrl + "\r\n" + fullLocFileUrl + "\r\n" + ex.Message + "\r\n" + ex.StackTrace, 0);
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

        public static string DownloadFile(string fullFtpFileFUrl, string fullLocFileUrl,
                            string userName, string password)
        {
            try
            {
                if (System.IO.File.Exists(fullLocFileUrl) == true)
                {
                    if (System.IO.File.GetCreationTime(fullLocFileUrl) >= DateTime.Now.AddHours(-1))
                    {
                        return "";
                    }
                }
                //downloadUrl = ftpserverurl + serverFullAppDirectoryPath + purefilename
                string ResponseDescription = "";
                //string PureFileName = new FileInfo(FileNameToDownload).Name;
                string DownloadedFilePath = fullLocFileUrl;
                string downloadUrl = fullFtpFileFUrl;
                FtpWebRequest req = (FtpWebRequest)FtpWebRequest.Create(downloadUrl);
                req.Method = WebRequestMethods.Ftp.DownloadFile;
                req.Credentials = new NetworkCredential(userName, password);
                req.UseBinary = true;
                req.Proxy = null;
                req.KeepAlive = false;
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
                return ResponseDescription;
            }
            catch (Exception ex)
            {
                //Global.showMsg(fullFtpFileFUrl ="\r\n" + fullLocFileUrl + "\r\n" + ex.Message + "\r\n" + ex.StackTrace, 0);
                return "";
            }
            finally
            {
            }
        }

        public static string[] GetFileList(string ftpServerAddrs, string dirName, string userName, string password)
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
                reqFTP.KeepAlive = false;
                reqFTP.UsePassive = true;
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
            finally
            {
            }
        }

        public static bool checkFTP(string fullFtpFileFUrl,
                            string userName, string password)
        {
            FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(fullFtpFileFUrl);
            FtpWebResponse res;
            ftp.Credentials = new NetworkCredential(userName, password);
            ftp.KeepAlive = false;
            ftp.Method = WebRequestMethods.Ftp.GetDateTimestamp;
            ftp.UsePassive = true;

            try
            {
                res = (FtpWebResponse)ftp.GetResponse();
                res.Close();
                return true;
            }
            catch (Exception ex)
            {
                //Global.showMsg(ex.Message + "\r\n" + ex.InnerException + "\r\n" + ex.StackTrace, 0);
                return false;
                //Handling code here.
            }
            finally
            {
            }
        }


        public static int findCharIndx(string inp_char, string[] inpArry)
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

        public static string dBEncrypt(string inpt)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "SELECT MD5('" + inpt + "')";
            dtSt = Global.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public static string getNewKey(string key)
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

        public static string decrypt(string inpt, string key)
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
                string keyString = Global.getNewKey(key);
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
                    int j = Global.findCharIndx(tst_str, charset2);
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

        public static bool sendEmail(string toEml, string ccEml,
         string bccEml, string attchmnt, string sbjct,
          string bdyTxt, ref string errMsgs)
        {
            try
            {
                string selSql = "SELECT smtp_client, mail_user_name, mail_password, smtp_port FROM sec.sec_email_servers WHERE (is_default = 't')";
                DataSet selDtSt = Global.selectDataNoParams(selSql);
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
                string fromPassword = Global.decrypt(fromPswd, Global.AppKey);

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
                for (i = 0; i < toEmails.Length; i++)
                {
                    mail.To.Add(toEmails[i]);
                }
                for (i = 0; i < ccEmails.Length; i++)
                {
                    mail.CC.Add(ccEmails[i]);
                }
                for (i = 0; i < bccEmails.Length; i++)
                {
                    mail.Bcc.Add(bccEmails[i]);
                }
                for (i = 0; i < attchMnts.Length; i++)
                {
                    Attachment attch1 = new Attachment(attchMnts[i]);
                    mail.Attachments.Add(attch1);
                }
                mail.Subject = sbjct;
                mail.Body = bdyTxt;
                //mail.BodyEncoding
                SmtpServer.Port = portNo;
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;

                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential(fromAddress.Address, fromPassword);
                //SmtpServer.Credentials = CredentialCache.DefaultNetworkCredentials;
                SmtpServer.EnableSsl = true;
                //      System.Windows.Forms.Application.DoEvents();
                //      this.showMsg("Test!\r\n" + SmtpServer.Host + "\r\n" + fromAddress.Address +
                //"\r\n" + fromPassword + "\r\n" + SmtpServer.Port + "\r\n" + mail.From.Address + "\r\nTo Email:" + mail.To.ToString() + "\r\n", 3);
                //      System.Windows.Forms.Application.DoEvents();
                SmtpServer.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                errMsgs += "Failed to send Email!\r\n" + ex.Message;
                return false;
            }
        }

        public static bool sendSMS(string msgBody, string rcpntNo, ref string errMsg)
        {
            //{"error":0,"response":1}
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
            DataSet dtst = Global.selectDataNoParams(@"select sms_param1, sms_param2, sms_param3, 
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

        public static void imprtTrnsTmp(string filename, string rptSQL)
        {
            Global.clearPrvExclFiles();
            Global.exclApp = new Microsoft.Office.Interop.Excel.Application();
            Global.exclApp.WindowState = Excel.XlWindowState.xlNormal;
            Global.exclApp.Visible = true;

            Global.nwWrkBk = Global.exclApp.Workbooks.Open(filename, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

            Global.trgtSheets = new Excel.Worksheet[1];

            Global.trgtSheets[0] = (Excel.Worksheet)Global.nwWrkBk.Worksheets[1];
            string orgnlValColA = "";
            string newValColB = "";

            int rownum = 1;
            do
            {
                try
                {
                    orgnlValColA = ((Microsoft.Office.Interop.Excel.Range)Global.trgtSheets[0].Cells[rownum, 1]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    orgnlValColA = "";
                }
                try
                {
                    newValColB = ((Microsoft.Office.Interop.Excel.Range)Global.trgtSheets[0].Cells[rownum, 2]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    newValColB = "";
                }

                if (orgnlValColA != "")
                {
                    string nwSQL = rptSQL;
                    nwSQL = nwSQL.Replace("{:orgnValColA}", orgnlValColA);
                    nwSQL = nwSQL.Replace("{:newValColB}", newValColB);
                    Global.executeGnrlSQL(nwSQL);
                    Global.trgtSheets[0].get_Range("A" + rownum + ":B" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 255, 0));
                }
                else
                {
                    Global.trgtSheets[0].get_Range("A" + rownum + ":B" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 0, 0));
                    //this.trgtSheets[0].get_Range("M" + rownum + ":M" + rownum + "", Type.Missing).Value2 = errMsg;
                }

                rownum++;
            }
            while (orgnlValColA != "");
        }

        #region "POST TRANSACTIONS..."
        public static void createLogMsg(string logmsg, string logTblNm,
         string procstyp, long procsID, string dateStr)
        {
            //string dateStr = Global.getDB_Date_time();
            //      dateStr = DateTime.ParseExact(
            //dateStr, "dd-MMM-yyyy HH:mm:ss",
            //System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            string insSQL = "INSERT INTO " + logTblNm + "(" +
                  "log_messages, process_typ, process_id, created_by, creation_date, " +
                  "last_update_by, last_update_date) " +
                  "VALUES ('" + logmsg.Replace("'", "''") +
                  "','" + procstyp.Replace("'", "''") + "'," + procsID +
                  ", " + Global.rnUser_ID + ", '" + dateStr +
                  "', " + Global.rnUser_ID + ", '" + dateStr +
                  "')";
            Global.insertDataNoParams(insSQL);
        }

        public static long getLogMsgID(string logTblNm, string procstyp, long procsID)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select msg_id from " + logTblNm +
              " where process_typ = '" + procstyp.Replace("'", "''") +
              "' and process_id = " + procsID + "";
            dtSt = Global.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public static bool isThereANActvActnPrcss(string prcsIDs, string prcsIntrvl)
        {
            string strSql = "SELECT age(now(), to_timestamp(last_active_time,'YYYY-MM-DD HH24:MI:SS')) <= interval '" + prcsIntrvl +
              "' FROM accb.accb_running_prcses WHERE which_process_is_rnng IN (" + prcsIDs +
              ") and age(now(), to_timestamp(last_active_time,'YYYY-MM-DD HH24:MI:SS')) <= interval '" + prcsIntrvl +
              "'";

            //Global.showMsg(strSql, 0);
            DataSet dtst = Global.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return bool.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return false;
        }

        public static void updtActnPrcss(int prcsID, int secondsAhead)
        {
            string dtestr = Global.getDB_Date_time();
            string strSql = @"UPDATE accb.accb_running_prcses SET
            last_active_time=to_char(to_timestamp('" + dtestr + "','YYYY-MM-DD HH24:MI:SS') + interval '" + secondsAhead + " second','YYYY-MM-DD HH24:MI:SS') " +
                  "WHERE which_process_is_rnng = " + prcsID + " ";
            Global.updateDataNoParams(strSql);
        }

        public static void updtActnPrcss(int prcsID)
        {
            string dtestr = Global.getDB_Date_time();
            string strSql = @"UPDATE accb.accb_running_prcses SET
            last_active_time='" + dtestr + "' " +
                  "WHERE which_process_is_rnng = " + prcsID + " ";
            Global.updateDataNoParams(strSql);
        }

        public static DataSet get_All_Chrt_Det(int orgid)
        {
            string strSql = "";
            strSql = @"SELECT a.accnt_id, a.debit_balance , a.credit_balance , a.net_balance ,
to_char(to_timestamp(a.balance_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') bsldte " +
              "FROM accb.accb_chart_of_accnts a WHERE a.org_id = " + orgid + " ORDER BY a.accnt_typ_id, a.report_line_no, a.accnt_num";
            DataSet dtst = Global.selectDataNoParams(strSql);
            return dtst;
        }

        public static bool isTransPrmttd(int accntID, string trnsdate, double amnt, ref string outptMsg)
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
                DateTime dte1 = DateTime.ParseExact(Global.getLtstPrdStrtDate(), "dd-MMM-yyyy HH:mm:ss",
        System.Globalization.CultureInfo.InvariantCulture);
                DateTime dte1Or = DateTime.ParseExact(Global.getLastPrdClseDate(), "dd-MMM-yyyy HH:mm:ss",
        System.Globalization.CultureInfo.InvariantCulture);
                DateTime dte2 = DateTime.ParseExact(Global.getLtstPrdEndDate(), "dd-MMM-yyyy HH:mm:ss",
        System.Globalization.CultureInfo.InvariantCulture);
                if (trnsDte <= dte1Or)
                {
                    outptMsg += "Transaction Date cannot be On or Before " + dte1Or.ToString("dd-MMM-yyyy HH:mm:ss");
                    return false;
                }
                if (trnsDte < dte1)
                {
                    outptMsg += "Transaction Date cannot be before " + dte1.ToString("dd-MMM-yyyy HH:mm:ss");
                    return false;
                }
                if (trnsDte > dte2)
                {
                    outptMsg += "Transaction Date cannot be after " + dte2.ToString("dd-MMM-yyyy HH:mm:ss");
                    return false;
                }
                //Check if trnsDate exists in an Open Period
                long prdHdrID = Global.getPrdHdrID(Global.UsrsOrg_ID);
                //Global.showMsg(Global.Org_id.ToString() + "-" + prdHdrID.ToString(), 0);
                if (prdHdrID > 0)
                {
                    //Global.showMsg(trnsDte.ToString("yyyy-MM-dd HH:mm:ss") + "-" + prdHdrID.ToString(), 0);

                    if (Global.getTrnsDteOpenPrdLnID(prdHdrID, trnsDte.ToString("yyyy-MM-dd HH:mm:ss")) < 0)
                    {
                        outptMsg += "Cannot use a Transaction Date (" + trnsDte.ToString("dd-MMM-yyyy HH:mm:ss") + ") which does not exist in any OPEN period!";
                        return false;
                    }
                    //Check if Date is not in Disallowed Dates
                    string noTrnsDatesLov = Global.getGnrlRecNm("accb.accb_periods_hdr", "periods_hdr_id", "no_trns_dates_lov_nm", prdHdrID);
                    string noTrnsDayLov = Global.getGnrlRecNm("accb.accb_periods_hdr", "periods_hdr_id", "no_trns_wk_days_lov_nm", prdHdrID);
                    //Global.showMsg(noTrnsDatesLov + "-" + noTrnsDayLov + "-" + trnsDte.ToString("dddd").ToUpper() + "-" + trnsDte.ToString("dd-MMM-yyyy").ToUpper(), 0);

                    if (noTrnsDatesLov != "")
                    {
                        if (Global.getEnbldPssblValID(trnsDte.ToString("dd-MMM-yyyy").ToUpper(), Global.getEnbldLovID(noTrnsDatesLov)) > 0)
                        {
                            outptMsg += "Transactions on this Date (" + trnsDte.ToString("dd-MMM-yyyy HH:mm:ss") + ") have been banned on this system!";
                            return false;
                        }
                    }
                    //Check if Day of Week is not in Disaalowed days
                    if (noTrnsDatesLov != "")
                    {
                        if (Global.getEnbldPssblValID(trnsDte.ToString("dddd").ToUpper(), Global.getEnbldLovID(noTrnsDayLov)) > 0)
                        {
                            outptMsg += "Transactions on this Day of Week (" + trnsDte.ToString("dddd") + ") have been banned on this system!";
                            return false;
                        }
                    }
                }

                //Amount must not disobey budget settings on that account
                long actvBdgtID = Global.getActiveBdgtID(Global.UsrsOrg_ID);
                double amntLmt = Global.getAcntsBdgtdAmnt(actvBdgtID,
                  accntID, trnsDte.ToString("dd-MMM-yyyy HH:mm:ss"));
                DateTime bdte1 = DateTime.ParseExact(
                  Global.getAcntsBdgtStrtDte(actvBdgtID, accntID,
                  trnsDte.ToString("dd-MMM-yyyy HH:mm:ss")), "dd-MMM-yyyy HH:mm:ss",
        System.Globalization.CultureInfo.InvariantCulture);
                DateTime bdte2 = DateTime.ParseExact(
                  Global.getAcntsBdgtEndDte(actvBdgtID, accntID,
                  trnsDte.ToString("dd-MMM-yyyy HH:mm:ss")), "dd-MMM-yyyy HH:mm:ss",
        System.Globalization.CultureInfo.InvariantCulture);
                double crntBals = Global.getTrnsSum(accntID, bdte1.ToString("dd-MMM-yyyy HH:mm:ss")
                  , bdte2.ToString("dd-MMM-yyyy HH:mm:ss"), "1");
                string actn = Global.getAcntsBdgtLmtActn(actvBdgtID, accntID, trnsdate);
                //Global.showMsg(amntLmt + "-" + crntBals + "-" + amnt + "-" + bdte1.ToString("dd-MMM-yyyy HH:mm:ss").ToUpper() + "-" + bdte2.ToString("dd-MMM-yyyy").ToUpper(), 0);

                if ((amnt + crntBals) > amntLmt)
                {
                    if (actn == "Disallow")
                    {
                        outptMsg += "This transaction will cause budget on \r\nthe chosen account to be exceeded! ";
                        return false;
                    }
                    else if (actn == "Warn")
                    {
                        outptMsg += "This is just to WARN you that the budget on \r\nthe chosen account will be exceeded!";
                        return true;
                    }
                    else if (actn == "Congratulate")
                    {
                        outptMsg += "This is just to CONGRATULATE you for exceeding the targetted Amount! ";
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
                outptMsg += ex.InnerException + "\r\n" + ex.StackTrace + "\r\n" + ex.Message;
                return false;
            }
        }

        public static int getEnbldLovID(string lovName)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "SELECT value_list_id from gst.gen_stp_lov_names where (upper(value_list_name) = upper('" +
             lovName.Replace("'", "''") + "') and is_enabled='1')";
            dtSt = Global.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public static int getEnbldPssblValID(string pssblVal, int lovID)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "SELECT pssbl_value_id from gst.gen_stp_lov_values " +
             "where ((upper(pssbl_value) = upper('" +
             pssblVal.Replace("'", "''") + "')) AND (value_list_id = " + lovID +
             ") AND (is_enabled='1')) ORDER BY pssbl_value_id LIMIT 1";
            dtSt = Global.selectDataNoParams(sqlStr);
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

        public static string getEnbldPssblVal(int lovID)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "SELECT pssbl_value from gst.gen_stp_lov_values " +
             "where ((value_list_id = " + lovID +
             ") AND (is_enabled='1')) ORDER BY pssbl_value_id DESC LIMIT 1 OFFSET 0";
            dtSt = Global.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public static string getEnbldPssblValDesc(string pssblVal, int lovID)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "SELECT pssbl_value_desc from gst.gen_stp_lov_values " +
             "where ((upper(pssbl_value) = upper('" +
             pssblVal.Replace("'", "''") + "')) AND (value_list_id = " + lovID +
             ") AND (is_enabled='1')) ORDER BY pssbl_value_id LIMIT 1";
            dtSt = Global.selectDataNoParams(sqlStr);
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

        public static double getTrnsSum(int accntid, string strDte, string endDte, string ispsted)
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
            DataSet dtst = Global.selectDataNoParams(strSql);
            double res = 0;
            if (dtst.Tables[0].Rows.Count > 0)
            {
                double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out res);
            }
            return res;
        }

        public static long getTrnsDteOpenPrdLnID(long prdHdrID, string trnsDte)
        {
            string strSql = "SELECT a.period_det_id " +
             "FROM accb.accb_periods_det a " +
             "WHERE((a.period_hdr_id = " + prdHdrID +
             ") and (a.period_status='Open') and (to_timestamp('" + trnsDte + "','YYYY-MM-DD HH24:MI:SS') " +
      @"between to_timestamp(a.period_start_date,'YYYY-MM-DD HH24:MI:SS')
       and to_timestamp(a.period_end_date,'YYYY-MM-DD HH24:MI:SS')))";
            DataSet dtst = Global.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count == 1)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public static long getPrdHdrID(int orgId)
        {
            string strSql = "SELECT a.periods_hdr_id " +
             "FROM accb.accb_periods_hdr a " +
             "WHERE(a.use_periods_for_org = '1' and a.org_id = " + orgId + ")";
            DataSet dtst = Global.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count == 1)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public static long getActiveBdgtID(int orgId)
        {
            string strSql = "SELECT a.budget_id " +
             "FROM accb.accb_budget_header a " +
             "WHERE(a.is_the_active_one = '1' and a.org_id = " + orgId + ")";
            DataSet dtst = Global.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count == 1)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public static string getAcntsBdgtdAmnt(long bdgtID, int accntID, string strtdate, string enddate)
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
            DataSet dtst = Global.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count == 1)
            {
                return dtst.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "0.00";
            }
        }

        public static double getAcntsBdgtdAmnt(long bdgtID, int accntID, string trnsdate)
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
            DataSet dtst = Global.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count == 1)
            {
                return double.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0.00;
            }
        }

        public static string getAcntsBdgtLmtActn(long bdgtID, int accntID, string trnsdate)
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
            DataSet dtst = Global.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count == 1)
            {
                return dtst.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "None";
            }
        }

        public static string getAcntsBdgtStrtDte(long bdgtID, int accntID, string trnsdate)
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
            DataSet dtst = Global.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count == 1)
            {
                return dtst.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return DateTime.ParseExact(
        Global.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
        System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy 00:00:00");
            }
        }

        public static string getAcntsBdgtEndDte(long bdgtID, int accntID, string trnsdate)
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
            DataSet dtst = Global.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count == 1)
            {
                return dtst.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return DateTime.ParseExact(
        Global.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
        System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy 23:59:59");
            }
        }

        public static string getLastPrdClseDate()
        {
            string strSql = "SELECT to_char(to_timestamp(period_close_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') " +
        "FROM accb.accb_period_close_dates " +
        "WHERE org_id = " + Global.UsrsOrg_ID +
        " ORDER BY period_close_id DESC LIMIT 1 OFFSET 0";
            DataSet dtst = Global.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return dtst.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "01-Jan-1900 00:00:00";
            }
        }

        public static string getLtstPrdStrtDate()
        {
            string strSql = "SELECT b.pssbl_value " +
             "FROM gst.gen_stp_lov_names a, gst.gen_stp_lov_values b " +
             "WHERE(a.value_list_id = b.value_list_id and b.is_enabled = '1'" +
             " and  a.value_list_name= 'Transactions Date Limit 1') " +
             "ORDER BY b.pssbl_value_id DESC LIMIT 1 OFFSET 0";
            DataSet dtst = Global.selectDataNoParams(strSql);
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
        Global.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
        System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy 00:00:00");
            }
        }

        public static string getLtstPrdEndDate()
        {
            string strSql = "SELECT b.pssbl_value " +
             "FROM gst.gen_stp_lov_names a, gst.gen_stp_lov_values b " +
             "WHERE(a.value_list_id = b.value_list_id and b.is_enabled = '1'" +
             " and  a.value_list_name= 'Transactions Date Limit 2') " +
             "ORDER BY b.pssbl_value_id DESC LIMIT 1 OFFSET 0";
            DataSet dtst = Global.selectDataNoParams(strSql);
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
        Global.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
        System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy 23:59:59");
            }
        }

        public static DataSet get_Batch_Trns(long batchID)
        {
            string strSql = "";
            strSql = "SELECT a.transctn_id, b.accnt_num, b.accnt_name, " +
            "a.transaction_desc, a.dbt_amount, a.crdt_amount, " +
            "to_char(to_timestamp(a.trnsctn_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), a.func_cur_id, " +
            "a.batch_id, a.accnt_id, a.net_amount, a.trns_status, a.entered_amnt, gst.get_pssbl_val(a.entered_amt_crncy_id), a.entered_amt_crncy_id, " +
            "a.accnt_crncy_amnt, gst.get_pssbl_val(a.accnt_crncy_id), a.accnt_crncy_id, a.func_cur_exchng_rate, a.accnt_cur_exchng_rate, a.src_trns_id_reconciled " +
            "FROM accb.accb_trnsctn_details a LEFT OUTER JOIN " +
            "accb.accb_chart_of_accnts b on a.accnt_id = b.accnt_id " +
            "WHERE(a.batch_id = " + batchID + " and a.trns_status='0') ORDER BY a.transctn_id";

            DataSet dtst = Global.selectDataNoParams(strSql);
            return dtst;
        }

        public static int get_Rtnd_Erngs_Accnt(int orgid)
        {
            string strSql = "";
            strSql = "SELECT a.accnt_id " +
              "FROM accb.accb_chart_of_accnts a " +
              "WHERE(a.is_retained_earnings = '1' and a.org_id = " + orgid + ")";
            DataSet dtst = Global.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count == 1)
            {
                return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public static int get_Net_Income_Accnt(int orgid)
        {
            string strSql = "";
            strSql = "SELECT a.accnt_id " +
              "FROM accb.accb_chart_of_accnts a " +
              "WHERE(a.is_net_income = '1' and a.org_id = " + orgid + ")";
            DataSet dtst = Global.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count == 1)
            {
                return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public static DataSet get_Batch_Accnts(long batchID)
        {
            string strSql = "";
            strSql = "SELECT a.accnt_id " +
          "FROM accb.accb_trnsctn_details a LEFT OUTER JOIN " +
          "accb.accb_chart_of_accnts b on a.accnt_id = b.accnt_id " +
          "WHERE(a.batch_id = " + batchID + ") ORDER BY a.transctn_id";

            DataSet dtst = Global.selectDataNoParams(strSql);
            return dtst;
        }

        public static void updateBatchStatus(long batchid)
        {
            string dateStr = Global.getDB_Date_time();
            string updtSQL = "UPDATE accb.accb_trnsctn_batches " +
            "SET batch_status='1', avlbl_for_postng='0', last_update_by=" + Global.rnUser_ID + ", last_update_date='" + dateStr +
            "' WHERE batch_id = " + batchid;
            Global.updateDataNoParams(updtSQL);
        }

        public static DataSet get_Batch_Trns_NoStatus(long batchID)
        {
            string strSql = "";
            strSql = "SELECT a.transctn_id, b.accnt_num, b.accnt_name, " +
              "a.transaction_desc, a.dbt_amount, a.crdt_amount, " +
                    "to_char(to_timestamp(a.trnsctn_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), a.func_cur_id, " +
                    "a.batch_id, a.accnt_id, a.net_amount, a.trns_status, a.entered_amnt, a.entered_amt_crncy_id, " +
                    "a.accnt_crncy_amnt, a.accnt_crncy_id, a.func_cur_exchng_rate, a.accnt_cur_exchng_rate, a.dbt_or_crdt " +
          "FROM accb.accb_trnsctn_details a LEFT OUTER JOIN " +
          "accb.accb_chart_of_accnts b on a.accnt_id = b.accnt_id " +
          "WHERE(a.batch_id = " + batchID + ") ORDER BY a.transctn_id";
            //Global.errorLog = strSql;
            //Global.writeToLog();
            DataSet dtst = Global.selectDataNoParams(strSql);
            return dtst;
        }

        public static void createScmGLIntFcLn(int accntid, string trnsdesc, double dbtamnt,
     string trnsdte, int crncyid, double crdtamnt, double netamnt, string srcDocTyp,
     long srcDocID, long srcDocLnID, string dateStr, string trnsSrc)
        {
            if (accntid <= 0)
            {
                return;
            }
            trnsdte = DateTime.ParseExact(
         trnsdte, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

            dateStr = DateTime.ParseExact(
         dateStr, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            string insSQL = "INSERT INTO scm.scm_gl_interface (" +
                  "accnt_id, transaction_desc, dbt_amount, trnsctn_date, " +
                  "func_cur_id, created_by, creation_date, crdt_amount, last_update_by, " +
                  "last_update_date, net_amount, gl_batch_id, src_doc_typ, src_doc_id, " +
                  "src_doc_line_id, trns_source) " +
                     "VALUES (" + accntid + ", '" + trnsdesc.Replace("'", "''") + "', " + dbtamnt +
                     ", '" + trnsdte.Replace("'", "''") + "', " + crncyid + ", " + Global.rnUser_ID +
                     ", '" + dateStr + "', " + crdtamnt + ", " +
                     Global.rnUser_ID + ", '" + dateStr + "', " + netamnt +
                     ", -1, '" + srcDocTyp.Replace("'", "''") + "', " +
                     srcDocID + ", " + srcDocLnID + ", '" + trnsSrc + "')";
            Global.insertDataNoParams(insSQL);
        }


        public static void createPayGLIntFcLn(int accntid, string trnsdesc, double dbtamnt,
     string trnsdte, int crncyid, double crdtamnt, double netamnt, string dateStr, string trnsSrc)
        {
            if (accntid <= 0)
            {
                return;
            }
            trnsdte = DateTime.ParseExact(
         trnsdte, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

            dateStr = DateTime.ParseExact(
         dateStr, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            string insSQL = "INSERT INTO pay.pay_gl_interface (" +
                  "accnt_id, transaction_desc, dbt_amount, trnsctn_date, " +
                  "func_cur_id, created_by, creation_date, crdt_amount, last_update_by, " +
                  "last_update_date, net_amount, gl_batch_id, trns_source) " +
                     "VALUES (" + accntid + ", '" + trnsdesc.Replace("'", "''") + "', " + dbtamnt +
                     ", '" + trnsdte.Replace("'", "''") + "', " + crncyid + ", " + Global.rnUser_ID +
                     ", '" + dateStr + "', " + crdtamnt + ", " +
                     Global.rnUser_ID + ", '" + dateStr + "', " + netamnt +
                     ", -1, '" + trnsSrc + "')";
            Global.insertDataNoParams(insSQL);
        }

        public static long getIntrfcTrnsID(string intrfcTblNm, int accntID, double netAmnt, string trnsDte)
        {
            string selSQL = @"SELECT interface_id 
  FROM " + intrfcTblNm + " WHERE accnt_id=" + accntID + " and net_amount=" + netAmnt +
               " and trnsctn_date = '" + trnsDte + "'";
            DataSet dtst = Global.selectDataNoParams(selSQL);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public static DataSet get_Intrfc_dateSums(string intrfcTblNm, int orgID)
        {
            string updtSQL = @"UPDATE " + intrfcTblNm + @" SET dbt_amount = round(dbt_amount,2),
    crdt_amount = round(dbt_amount,2), net_amount = round(net_amount,2)
    WHERE round(crdt_amount - round(crdt_amount,2))!=0 or round(dbt_amount - round(dbt_amount,2))!=0";
            Global.updateDataNoParams(updtSQL);

            string strSql = "";
            strSql = @"SELECT substring(a.trnsctn_date from 1 for 10), 
round(SUM(a.dbt_amount),2), round(SUM(a.crdt_amount),2) 
    FROM " + intrfcTblNm + @" a, accb.accb_chart_of_accnts b 
    WHERE(a.gl_batch_id <=0 and a.accnt_id = b.accnt_id and b.org_id=" + orgID + @" and 
age(now(),to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS')) > interval '5 minute') 
    GROUP BY substring(a.trnsctn_date from 1 for 10) 
    HAVING SUM(a.dbt_amount) != SUM(a.crdt_amount)
    ORDER BY 1;";
            /**/
            DataSet dtst = Global.selectDataNoParams(strSql);
            //Global.mnFrm.trnsDet_SQL = strSql;
            return dtst;
        }

        public static DataSet get_Batch_dateSums(long batchID)
        {
            string strSql = "";
            strSql = @"SELECT substring(a.trnsctn_date from 1 for 10), SUM(a.dbt_amount), SUM(a.crdt_amount) 
    FROM accb.accb_trnsctn_details a
    WHERE(a.batch_id = " + batchID + @") 
    GROUP BY substring(a.trnsctn_date from 1 for 10) 
    HAVING round(SUM(a.dbt_amount),2) != round(SUM(a.crdt_amount),2)
    ORDER BY 1";

            DataSet dtst = Global.selectDataNoParams(strSql);
            //Global.mnFrm.trnsDet_SQL = strSql;
            return dtst;
        }

        public static double get_Batch_DbtSum(long batchID)
        {
            string strSql = "";
            double sumRes = 0.00;
            strSql = "SELECT SUM(a.dbt_amount)" +
          "FROM accb.accb_trnsctn_details a " +
          "WHERE(a.batch_id = " + batchID + ")";

            DataSet dtst = Global.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out sumRes);
            }
            return Math.Round(sumRes, 2);
        }

        public static double get_Batch_CrdtSum(long batchID)
        {
            string strSql = "";
            strSql = "SELECT SUM(a.crdt_amount)" +
          "FROM accb.accb_trnsctn_details a " +
          "WHERE(a.batch_id = " + batchID + ")";

            DataSet dtst = Global.selectDataNoParams(strSql);
            double sumRes = 0.00;
            if (dtst.Tables[0].Rows.Count > 0)
            {
                double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out sumRes);
            }
            return Math.Round(sumRes, 2);
        }
        public static bool hsTrnsUptdAcntBls(long actrnsid,
     string trnsdate, int accnt_id)
        {
            trnsdate = DateTime.ParseExact(
      trnsdate, "dd-MMM-yyyy HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            trnsdate = trnsdate.Substring(0, 10);

            string strSql = "SELECT a.daily_bals_id FROM accb.accb_accnt_daily_bals a " +
              "WHERE a.accnt_id = " + accnt_id +
              " and a.as_at_date = '" + trnsdate + "' and a.src_trns_ids like '%," + actrnsid + ",%'";
            DataSet dtst = Global.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public static double get_Accnt_BalsTrnsSum(int accntID, string amntCol, string balsDte)
        {
            balsDte = DateTime.ParseExact(
      balsDte, "dd-MMM-yyyy HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

            string strSql = "";
            strSql = "SELECT SUM(a." + amntCol + ") " +
              "FROM accb.accb_trnsctn_details a, accb.accb_chart_of_accnts b " +
              "WHERE ((a.accnt_id=b.accnt_id) and (a.accnt_id = " + accntID + " or b.control_account_id=" + accntID + ") and (to_timestamp(a.trnsctn_date, " +
              "'YYYY-MM-DD HH24:MI:SS') <= to_timestamp('" + balsDte +
              "', 'YYYY-MM-DD HH24:MI:SS')) and " +
              "(a.trns_status = '1'))";
            DataSet dtst = Global.selectDataNoParams(strSql);
            double sumRes = 0.00;
            if (dtst.Tables[0].Rows.Count > 0)
            {
                double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out sumRes);
            }
            return sumRes;
        }

        //    public static DataSet get_WrongNetBalncs(int orgID)
        //    {
        //      string selSQL = @"select a.transctn_id, a.accnt_id, b.accnt_type, a.transaction_desc, a.trnsctn_date, 
        //a.dbt_amount, a.crdt_amount, a.net_amount, CASE WHEN b.accnt_type='A' or b.accnt_type='EX'  
        //   THEN (dbt_amount-crdt_amount)  
        //   ELSE (crdt_amount-dbt_amount) END actual_net 
        //   from accb.accb_trnsctn_details a, accb.accb_chart_of_accnts b
        //where a.accnt_id=b.accnt_id and a.trns_status='1' and b.org_id=" + orgID + @"
        //and CASE WHEN b.accnt_type='A' or b.accnt_type='EX'  
        //   THEN (dbt_amount-crdt_amount)  
        //   ELSE (crdt_amount-dbt_amount) END <> (net_amount)";
        //      return Global.selectDataNoParams(selSQL);
        //    }

        //    public static DataSet get_WrongBalncs(int orgID)
        //    {
        //      string selSQL = @"SELECT * FROM (SELECT a.daily_bals_id, a.accnt_id, b.accnt_name, b.accnt_type, 
        //round(accb.get_accnt_trnsSum(a.accnt_id,'dbt_amount',as_at_date||' 23:59:59'),2)-a.dbt_bal nw_dbbt_diff, 
        //round(accb.get_accnt_trnsSum(a.accnt_id,'crdt_amount',as_at_date||' 23:59:59'),2)-a.crdt_bal nw_crdt_diff,
        //round(accb.get_accnt_trnsSum(a.accnt_id,'net_amount',as_at_date||' 23:59:59'),2)-a.net_balance nw_net_diff, 
        //to_char(to_timestamp(a.as_at_date||' 23:59:00','YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') trns_date
        //FROM accb.accb_accnt_daily_bals a, accb.accb_chart_of_accnts b 
        //  where a.accnt_id=b.accnt_id and b.org_id=" + orgID + @" and b.is_net_income!='1' and b.has_sub_ledgers!='1'  
        //  and a.as_at_date=(SELECT MAX(as_at_date)
        //  FROM accb.accb_accnt_daily_bals d
        //  where d.accnt_id=a.accnt_id)) tbl1 WHERE tbl1.nw_dbbt_diff !=0 or tbl1.nw_crdt_diff !=0 or tbl1.nw_net_diff !=0";
        //      //  and b.is_retained_earnings!='1'
        //      return Global.selectDataNoParams(selSQL);
        //    }

        //    public static DataSet get_WrongNetIncmBalncs(int orgID)
        //    {
        //      string selSQL = @"SELECT a.daily_bals_id, a.accnt_id, b.accnt_name, b.accnt_type, 
        //round(accb.get_accnttype_trnsSum(" + orgID + @",'R','dbt_amount',as_at_date||' 23:59:59'),2)+round(accb.get_accnttype_trnsSum(" + orgID + @",'EX','dbt_amount',as_at_date||' 23:59:59'),2)-a.dbt_bal nw_dbbt_diff, 
        //round(accb.get_accnttype_trnsSum(" + orgID + @",'R','crdt_amount',as_at_date||' 23:59:59'),2)+round(accb.get_accnttype_trnsSum(" + orgID + @",'EX','crdt_amount',as_at_date||' 23:59:59'),2)-a.crdt_bal nw_crdt_diff,
        //round(accb.get_accnttype_trnsSum(" + orgID + @",'R','net_amount',as_at_date||' 23:59:59'),2)-round(accb.get_accnttype_trnsSum(" + orgID + @",'EX','net_amount',as_at_date||' 23:59:59'),2)-a.net_balance nw_net_diff, 
        //to_char(to_timestamp(a.as_at_date||' 23:59:00','YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') trns_date 
        //FROM accb.accb_accnt_daily_bals a, accb.accb_chart_of_accnts b 
        //  where a.accnt_id=b.accnt_id and b.org_id=" + orgID + @" and b.is_net_income='1' and b.has_sub_ledgers!='1'
        //  and a.as_at_date=(SELECT MAX(as_at_date)
        //  FROM accb.accb_accnt_daily_bals d
        //  where d.accnt_id=a.accnt_id)";
        //      //  and b.is_retained_earnings!='1'
        //      return Global.selectDataNoParams(selSQL);
        //    }

        public static DataSet get_WrongNetBalncs(int orgID)
        {
            string updtSQL = @"UPDATE accb.accb_trnsctn_details 
      SET dbt_amount=round(dbt_amount,2), crdt_amount=round(crdt_amount,2) 
      WHERE dbt_amount!=round(dbt_amount,2) or crdt_amount!=round(crdt_amount,2)";
            Global.updateDataNoParams(updtSQL);
            System.Threading.Thread.Sleep(2000);
            string selSQL = @"select a.transctn_id, a.accnt_id, b.accnt_type, a.transaction_desc, a.trnsctn_date, 
a.dbt_amount, a.crdt_amount, a.net_amount, CASE WHEN b.accnt_type='A' or b.accnt_type='EX'  
   THEN (dbt_amount-crdt_amount)  
   ELSE (crdt_amount-dbt_amount) END actual_net 
   from accb.accb_trnsctn_details a, accb.accb_chart_of_accnts b
where a.accnt_id=b.accnt_id and a.trns_status='1' and b.org_id=" + orgID + @"
and CASE WHEN b.accnt_type='A' or b.accnt_type='EX'  
   THEN (dbt_amount-crdt_amount)  
   ELSE (crdt_amount-dbt_amount) END <> (net_amount)";
            return Global.selectDataNoParams(selSQL);
        }

        public static DataSet get_WrongBalncs(int orgID)
        {
            string selSQL = @"SELECT * FROM (SELECT a.daily_bals_id, a.accnt_id, b.accnt_name, b.accnt_type, 
round(accb.get_accnt_trnsSum(a.accnt_id,'dbt_amount',as_at_date||' 23:59:59'),2)-a.dbt_bal nw_dbbt_diff, 
round(accb.get_accnt_trnsSum(a.accnt_id,'crdt_amount',as_at_date||' 23:59:59'),2)-a.crdt_bal nw_crdt_diff,
round(accb.get_accnt_trnsSum(a.accnt_id,'net_amount',as_at_date||' 23:59:59'),2)-a.net_balance nw_net_diff, 
to_char(to_timestamp(a.as_at_date||' 23:59:00','YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') trns_date
FROM accb.accb_accnt_daily_bals a, accb.accb_chart_of_accnts b 
  where a.accnt_id=b.accnt_id and b.org_id=" + orgID + @" and b.is_net_income!='1' and b.has_sub_ledgers!='1'  
  and a.as_at_date=(SELECT MAX(as_at_date)
  FROM accb.accb_accnt_daily_bals d
  where d.accnt_id=a.accnt_id)) tbl1 WHERE tbl1.nw_dbbt_diff !=0 or tbl1.nw_crdt_diff !=0 or tbl1.nw_net_diff !=0";
            //  and b.is_retained_earnings!='1'
            //Global.errorLog = "Wrong Balances SQL = " + selSQL;
            //Global.writeToLog();
            return Global.selectDataNoParams(selSQL);
        }

        public static DataSet get_WrongNetIncmBalncs(int orgID)
        {
            string selSQL = @"SELECT a.daily_bals_id, a.accnt_id, b.accnt_name, b.accnt_type, 
round(accb.get_accnttype_trnsSum(" + orgID + @",'R','dbt_amount',as_at_date||' 23:59:59'),2)+round(accb.get_accnttype_trnsSum(" + orgID + @",'EX','dbt_amount',as_at_date||' 23:59:59'),2)-a.dbt_bal nw_dbbt_diff, 
round(accb.get_accnttype_trnsSum(" + orgID + @",'R','crdt_amount',as_at_date||' 23:59:59'),2)+round(accb.get_accnttype_trnsSum(" + orgID + @",'EX','crdt_amount',as_at_date||' 23:59:59'),2)-a.crdt_bal nw_crdt_diff,
round(accb.get_accnttype_trnsSum(" + orgID + @",'R','net_amount',as_at_date||' 23:59:59'),2)-round(accb.get_accnttype_trnsSum(" + orgID + @",'EX','net_amount',as_at_date||' 23:59:59'),2)-a.net_balance nw_net_diff, 
to_char(to_timestamp(a.as_at_date||' 23:59:00','YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') trns_date 
FROM accb.accb_accnt_daily_bals a, accb.accb_chart_of_accnts b 
  where a.accnt_id=b.accnt_id and b.org_id=" + orgID + @" and b.is_net_income='1' and b.has_sub_ledgers!='1'
  and a.as_at_date=(SELECT MAX(as_at_date)
  FROM accb.accb_accnt_daily_bals d
  where d.accnt_id=a.accnt_id)";
            //  and b.is_retained_earnings!='1'
            return Global.selectDataNoParams(selSQL);
        }

        public static bool hsTrnsUptdAcntCurrBls(long actrnsid,
    string trnsdate, int accnt_id)
        {
            trnsdate = DateTime.ParseExact(
      trnsdate, "dd-MMM-yyyy HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            trnsdate = trnsdate.Substring(0, 10);

            string strSql = "SELECT a.daily_cbals_id FROM accb.accb_accnt_crncy_daily_bals a " +
              "WHERE a.accnt_id = " + accnt_id +
              " and a.as_at_date = '" + trnsdate + "' and a.src_trns_ids like '%," + actrnsid + ",%'";
            DataSet dtst = Global.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public static void updtAcntChrtBals(int accntid,
          double dbtAmnt, double crdtAmnt, double netAmnt, string trnsDate)
        {
            trnsDate = DateTime.ParseExact(
      trnsDate, "dd-MMM-yyyy HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            trnsDate = trnsDate.Substring(0, 10);
            string dateStr = Global.getDB_Date_time();
            string updtSQL = "UPDATE accb.accb_chart_of_accnts " +
                  "SET last_update_by = " + Global.rnUser_ID +
                  ", last_update_date = '" + dateStr +
                          "', balance_date = '" + trnsDate + "', " +
                          "debit_balance = " + dbtAmnt +
                          ", credit_balance = " + crdtAmnt +
                          ", net_balance = " + netAmnt +
              " WHERE accnt_id = " + accntid;
            Global.updateDataNoParams(updtSQL);
        }

        public static void createDailyBals(int accntid, double netbals,
         double dbtbals, double crdtbals, string balsDate)
        {
            balsDate = DateTime.ParseExact(
      balsDate, "dd-MMM-yyyy HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            balsDate = balsDate.Substring(0, 10);

            string dateStr = Global.getDB_Date_time();
            string insSQL = "INSERT INTO accb.accb_accnt_daily_bals(" +
                              "accnt_id, net_balance, dbt_bal, crdt_bal, as_at_date, " +
                              "created_by, creation_date, last_update_by, last_update_date, src_trns_ids) " +
              "VALUES (" + accntid +
              ", " + netbals + ", " + dbtbals + ", " + crdtbals + ", '" + balsDate +
              "', " + Global.rnUser_ID + ", '" + dateStr +
                              "', " + Global.rnUser_ID + ", '" + dateStr + "', ',')";
            Global.insertDataNoParams(insSQL);
        }

        public static void createDailyAccntCurrBals(int accntid, double netbals,
          double dbtbals, double crdtbals, string balsDate, int currID)
        {
            balsDate = DateTime.ParseExact(
      balsDate, "dd-MMM-yyyy HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            balsDate = balsDate.Substring(0, 10);

            string dateStr = Global.getDB_Date_time();
            string insSQL = "INSERT INTO accb.accb_accnt_crncy_daily_bals(" +
                              "accnt_id, net_balance, dbt_bal, crdt_bal, as_at_date, " +
                              "created_by, creation_date, last_update_by, last_update_date, src_trns_ids, crncy_id) " +
              "VALUES (" + accntid +
              ", " + netbals + ", " + dbtbals + ", " + crdtbals + ", '" + balsDate + "', " + Global.rnUser_ID + ", '" + dateStr +
                              "', " + Global.rnUser_ID + ", '" + dateStr + "', ',', " + currID + ")";
            Global.insertDataNoParams(insSQL);
        }

        public static double getSign(double inptAMnt)
        {
            if (inptAMnt != 0)
            {
                return inptAMnt / Math.Abs(inptAMnt);
            }
            return 0;
        }

        public static void postTransaction(int accntid,
          double dbtAmnt, double crdtAmnt, double netAmnt,
          string trnsDate, long src_trsID)
        {
            long dailybalID = Global.getAccntDailyBalsID(accntid, trnsDate);
            //Get dailybalid for accnt on this date
            //if doesn't exist get last accnt bals be4 this date
            //add new amount to it and insert record
            if (dailybalID <= 0)
            {
                double lstNetBals = Global.getAccntLstDailyNetBals(accntid, trnsDate);
                double lstDbtBals = Global.getAccntLstDailyDbtBals(accntid, trnsDate);
                double lstCrdtBals = Global.getAccntLstDailyCrdtBals(accntid, trnsDate);
                Global.createDailyBals(accntid, lstNetBals, lstDbtBals, lstCrdtBals, trnsDate);
                Global.updtAccntDailyBals(trnsDate, accntid, dbtAmnt,
                  crdtAmnt, netAmnt, src_trsID, "Do");
            }
            else
            {
                Global.updtAccntDailyBals(trnsDate, accntid, dbtAmnt,
                  crdtAmnt, netAmnt, src_trsID, "Do");
            }
        }

        public static void updtAccntDailyBals(string balsDate, int accntID,
      double dbtAmnt, double crdtAmnt, double netAmnt, long src_trnsID,
          string act_typ)
        {
            balsDate = DateTime.ParseExact(
      balsDate, "dd-MMM-yyyy HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            balsDate = balsDate.Substring(0, 10);

            string dateStr = Global.getDB_Date_time();
            string updtSQL = "";
            if (act_typ == "Undo")
            {
                updtSQL = "UPDATE accb.accb_accnt_daily_bals " +
          "SET last_update_by = " + Global.rnUser_ID +
          ", last_update_date = '" + dateStr +
                  "', dbt_bal = dbt_bal - " + dbtAmnt +
                  ", crdt_bal = crdt_bal - " + crdtAmnt +
                  ", net_balance = net_balance - " + netAmnt +
                  ", src_trns_ids = replace(src_trns_ids, '," + src_trnsID + ",', ',')" +
        " WHERE (to_timestamp(as_at_date,'YYYY-MM-DD') >=  to_timestamp('" + balsDate +
        "','YYYY-MM-DD') and accnt_id = " + accntID + ")";
            }
            else
            {
                updtSQL = "UPDATE accb.accb_accnt_daily_bals " +
          "SET last_update_by = " + Global.rnUser_ID +
          ", last_update_date = '" + dateStr +
                  "', dbt_bal = dbt_bal + " + dbtAmnt +
                  ", crdt_bal = crdt_bal + " + crdtAmnt +
                  ", net_balance = net_balance +" + netAmnt +
                  ", src_trns_ids = src_trns_ids || '" + src_trnsID + ",'" +
        " WHERE (to_timestamp(as_at_date,'YYYY-MM-DD') >=  to_timestamp('" + balsDate +
        "','YYYY-MM-DD') and accnt_id = " + accntID + ")";
            }
            Global.updateDataNoParams(updtSQL);
        }

        public static void undoPostTransaction(int accntid, double dbtAmnt,
          double crdtAmnt, double netAmnt, string trnsDate, long src_trsID)
        {
            long dailybalID = Global.getAccntDailyBalsID(accntid, trnsDate);
            //Get dailybalid for accnt on this date
            //if doesn't exist get last accnt bals be4 this date
            //subtract new amount from it and insert record
            if (dailybalID <= 0)
            {
                //double lstNetBals = Global.getAccntLstDailyNetBals(accntid, trnsDate);
                //double lstDbtBals = Global.getAccntLstDailyDbtBals(accntid, trnsDate);
                //double lstCrdtBals = Global.getAccntLstDailyCrdtBals(accntid, trnsDate);
                //Global.createDailyBals(accntid, lstNetBals, lstDbtBals, lstCrdtBals, trnsDate);
                //Global.updtAccntDailyBals(trnsDate, accntid, dbtAmnt,
                //  crdtAmnt, netAmnt, src_trsID, "Undo");
            }
            else
            {
                Global.updtAccntDailyBals(trnsDate, accntid, dbtAmnt,
                  crdtAmnt, netAmnt, src_trsID, "Undo");
            }
        }


        public static void postAccntCurrTransaction(int accntid,
         double dbtAmnt, double crdtAmnt, double netAmnt,
         string trnsDate, long src_trsID, int currID)
        {
            if (dbtAmnt == 0 && crdtAmnt == 0 && netAmnt == 0)
            {
                double acntCurrAmnt = double.Parse(Global.getGnrlRecNm(
        "accb.accb_trnsctn_details", "transctn_id", "accnt_crncy_amnt", src_trsID));
                string dbtCrdt = Global.getGnrlRecNm(
           "accb.accb_trnsctn_details", "transctn_id", "dbt_or_crdt", src_trsID);
                string incrdcrs = "";
                if (dbtCrdt == "C")
                {
                    incrdcrs = Global.incrsOrDcrsAccnt(accntid, "Credit");
                    dbtAmnt = 0;
                    crdtAmnt = acntCurrAmnt;
                    netAmnt = (double)Global.dbtOrCrdtAccntMultiplier(accntid,
          incrdcrs.Substring(0, 1)) * acntCurrAmnt;

                }
                else
                {
                    incrdcrs = Global.incrsOrDcrsAccnt(accntid, "Debit");
                    dbtAmnt = acntCurrAmnt;
                    crdtAmnt = 0;
                    netAmnt = (double)Global.dbtOrCrdtAccntMultiplier(accntid,
          incrdcrs.Substring(0, 1)) * acntCurrAmnt;
                }
            }
            long dailybalID = Global.getAccntDailyCurrBalsID(accntid, trnsDate);
            //Get dailybalid for accnt on this date
            //if doesn't exist get last accnt bals be4 this date
            //add new amount to it and insert record
            if (dailybalID <= 0)
            {
                double lstNetBals = Global.getAccntLstDailyNetCurrBals(accntid, trnsDate);
                double lstDbtBals = Global.getAccntLstDailyDbtCurrBals(accntid, trnsDate);
                double lstCrdtBals = Global.getAccntLstDailyCrdtCurrBals(accntid, trnsDate);
                Global.createDailyAccntCurrBals(accntid, lstNetBals, lstDbtBals, lstCrdtBals, trnsDate, currID);
                Global.updtAccntDailyCurrBals(trnsDate, accntid, dbtAmnt,
                  crdtAmnt, netAmnt, src_trsID, "Do", currID);
            }
            else
            {
                Global.updtAccntDailyCurrBals(trnsDate, accntid, dbtAmnt,
                  crdtAmnt, netAmnt, src_trsID, "Do", currID);
            }
        }

        public static void updtAccntDailyCurrBals(string balsDate, int accntID,
      double dbtAmnt, double crdtAmnt, double netAmnt, long src_trnsID,
          string act_typ, int currID)
        {
            balsDate = DateTime.ParseExact(
      balsDate, "dd-MMM-yyyy HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            balsDate = balsDate.Substring(0, 10);

            string dateStr = Global.getDB_Date_time();
            string updtSQL = "";
            if (act_typ == "Undo")
            {
                updtSQL = "UPDATE accb.accb_accnt_crncy_daily_bals " +
          "SET last_update_by = " + Global.rnUser_ID +
          ", last_update_date = '" + dateStr +
                  "', dbt_bal = dbt_bal - " + dbtAmnt +
                  ", crdt_bal = crdt_bal - " + crdtAmnt +
                  ", net_balance = net_balance - " + netAmnt +
                  ", src_trns_ids = replace(src_trns_ids, '," + src_trnsID + ",', ',')" +
                  ", crncy_id = " + currID + " " +
        " WHERE (to_timestamp(as_at_date,'YYYY-MM-DD') >=  to_timestamp('" + balsDate +
        "','YYYY-MM-DD') and accnt_id = " + accntID + ")";
            }
            else
            {
                updtSQL = "UPDATE accb.accb_accnt_crncy_daily_bals " +
          "SET last_update_by = " + Global.rnUser_ID +
          ", last_update_date = '" + dateStr +
                  "', dbt_bal = dbt_bal + " + dbtAmnt +
                  ", crdt_bal = crdt_bal + " + crdtAmnt +
                  ", net_balance = net_balance +" + netAmnt +
                  ", src_trns_ids = src_trns_ids || '" + src_trnsID + ",'" +
                  ", crncy_id = " + currID + " " +
        " WHERE (to_timestamp(as_at_date,'YYYY-MM-DD') >=  to_timestamp('" + balsDate +
        "','YYYY-MM-DD') and accnt_id = " + accntID + ")";
            }
            Global.updateDataNoParams(updtSQL);
        }

        public static void undoPostAccntCurrTransaction(int accntid, double dbtAmnt,
          double crdtAmnt, double netAmnt, string trnsDate, long src_trsID, int currID)
        {
            if (dbtAmnt == 0 && crdtAmnt == 0 && netAmnt == 0)
            {
                double acntCurrAmnt = double.Parse(Global.getGnrlRecNm(
        "accb.accb_trnsctn_details", "transctn_id", "accnt_crncy_amnt", src_trsID));
                string dbtCrdt = Global.getGnrlRecNm(
           "accb.accb_trnsctn_details", "transctn_id", "dbt_or_crdt", src_trsID);
                string incrdcrs = "";
                if (dbtCrdt == "C")
                {
                    incrdcrs = Global.incrsOrDcrsAccnt(accntid, "Credit");
                    dbtAmnt = 0;
                    crdtAmnt = acntCurrAmnt;
                    netAmnt = (double)Global.dbtOrCrdtAccntMultiplier(accntid,
          incrdcrs.Substring(0, 1)) * acntCurrAmnt;

                }
                else
                {
                    incrdcrs = Global.incrsOrDcrsAccnt(accntid, "Debit");
                    dbtAmnt = acntCurrAmnt;
                    crdtAmnt = 0;
                    netAmnt = (double)Global.dbtOrCrdtAccntMultiplier(accntid,
          incrdcrs.Substring(0, 1)) * acntCurrAmnt;
                }
            }
            long dailybalID = Global.getAccntDailyCurrBalsID(accntid, trnsDate);
            //Get dailybalid for accnt on this date
            //if doesn't exist get last accnt bals be4 this date
            //subtract new amount from it and insert record
            if (dailybalID <= 0)
            {
                //double lstNetBals = Global.getAccntLstDailyNetBals(accntid, trnsDate);
                //double lstDbtBals = Global.getAccntLstDailyDbtBals(accntid, trnsDate);
                //double lstCrdtBals = Global.getAccntLstDailyCrdtBals(accntid, trnsDate);
                //Global.createDailyBals(accntid, lstNetBals, lstDbtBals, lstCrdtBals, trnsDate);
                //Global.updtAccntDailyBals(trnsDate, accntid, dbtAmnt,
                //  crdtAmnt, netAmnt, src_trsID, "Undo");
            }
            else
            {
                Global.updtAccntDailyCurrBals(trnsDate, accntid, dbtAmnt,
                  crdtAmnt, netAmnt, src_trsID, "Undo", currID);
            }
        }

        public static double get_Accnt_Net_Bals(int accntID)
        {
            string strSql = "";
            strSql = "SELECT a.net_balance " +
              "FROM accb.accb_chart_of_accnts a " +
              "WHERE(a.accnt_id = " + accntID + ")";
            DataSet dtst = Global.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count == 1)
            {
                return double.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0.00;
            }
        }

        public static double get_Accnt_Bls_Bals(int accntID, long blsID)
        {
            string strSql = "";
            strSql = "SELECT a.net_balance " +
              "FROM accb.accb_balsheet_details a " +
              "WHERE(a.accnt_id = " + accntID + " and a.balsheet_header_id = " + blsID + ")";
            DataSet dtst = Global.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count == 1)
            {
                return double.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0.00;
            }
        }

        public static long getTodaysGLBatchID(string batchnm, int orgid)
        {
            string strSql = "";
            strSql = "SELECT a.batch_id " +
          "FROM accb.accb_trnsctn_batches a " +
          "WHERE(a.batch_name ilike '%" + batchnm.Replace("'", "''") +
          "%' and org_id = " + orgid + " and batch_status = '0')";

            DataSet dtst = Global.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public static double getAccntDailyNetBals(int accntID, string balsDate)
        {
            balsDate = DateTime.ParseExact(
      balsDate, "dd-MMM-yyyy HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            balsDate = balsDate.Substring(0, 10);

            string strSql = "";
            strSql = "SELECT a.net_balance " +
          "FROM accb.accb_accnt_daily_bals a " +
          "WHERE(to_timestamp(a.as_at_date,'YYYY-MM-DD') =  to_timestamp('" + balsDate +
          "','YYYY-MM-DD') and a.accnt_id = " + accntID + ")";

            DataSet dtst = Global.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return double.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0.00;
            }
        }

        public static string[] getAccntLstDailyBalsInfo(int accntID, string balsDate)
        {
            string dateStr = balsDate;
            balsDate = DateTime.ParseExact(
      balsDate, "dd-MMM-yyyy HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            balsDate = balsDate.Substring(0, 10);

            string strSql = "";
            strSql = @"SELECT a.dbt_bal, a.crdt_bal, a.net_balance, 
to_char(to_timestamp(a.as_at_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') " +
          "FROM accb.accb_accnt_daily_bals a " +
          "WHERE(to_timestamp(a.as_at_date,'YYYY-MM-DD') <=  to_timestamp('" + balsDate +
          "','YYYY-MM-DD') and a.accnt_id = " + accntID +
          ") ORDER BY to_timestamp(a.as_at_date,'YYYY-MM-DD') DESC LIMIT 1 OFFSET 0";

            DataSet dtst = Global.selectDataNoParams(strSql);
            string[] rslt = { "0", "0", "0", dateStr };
            if (dtst.Tables[0].Rows.Count > 0)
            {
                rslt[0] = dtst.Tables[0].Rows[0][0].ToString();
                rslt[1] = dtst.Tables[0].Rows[0][1].ToString();
                rslt[2] = dtst.Tables[0].Rows[0][2].ToString();
                rslt[3] = dtst.Tables[0].Rows[0][3].ToString();
                return rslt;
            }
            else
            {
                return rslt;
            }
        }

        public static double getAccntLstDailyNetBals(int accntID, string balsDate)
        {
            balsDate = DateTime.ParseExact(
      balsDate, "dd-MMM-yyyy HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            balsDate = balsDate.Substring(0, 10);

            string strSql = "";
            strSql = "SELECT a.net_balance " +
          "FROM accb.accb_accnt_daily_bals a " +
          "WHERE(to_timestamp(a.as_at_date,'YYYY-MM-DD') <=  to_timestamp('" + balsDate +
          "','YYYY-MM-DD') and a.accnt_id = " + accntID +
          ") ORDER BY to_timestamp(a.as_at_date,'YYYY-MM-DD') DESC LIMIT 1 OFFSET 0";

            DataSet dtst = Global.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return double.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0.00;
            }
        }

        public static long getAccntDailyBalsID(int accntID, string balsDate)
        {
            balsDate = DateTime.ParseExact(
      balsDate, "dd-MMM-yyyy HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            balsDate = balsDate.Substring(0, 10);

            string strSql = "";
            strSql = "SELECT a.daily_bals_id " +
          "FROM accb.accb_accnt_daily_bals a " +
          "WHERE(to_timestamp(a.as_at_date,'YYYY-MM-DD') =  to_timestamp('" + balsDate +
          "','YYYY-MM-DD') and a.accnt_id = " + accntID + ")";

            DataSet dtst = Global.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public static double getAccntLstDailyCrdtBals(int accntID, string balsDate)
        {
            balsDate = DateTime.ParseExact(
      balsDate, "dd-MMM-yyyy HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            balsDate = balsDate.Substring(0, 10);

            string strSql = "";
            strSql = "SELECT a.crdt_bal " +
          "FROM accb.accb_accnt_daily_bals a " +
          "WHERE(to_timestamp(a.as_at_date,'YYYY-MM-DD') <=  to_timestamp('" + balsDate +
          "','YYYY-MM-DD') and a.accnt_id = " + accntID + ") ORDER BY to_timestamp(a.as_at_date,'YYYY-MM-DD') DESC LIMIT 1 OFFSET 0";

            DataSet dtst = Global.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return double.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0.00;
            }
        }

        public static double getAccntLstDailyDbtBals(int accntID, string balsDate)
        {
            balsDate = DateTime.ParseExact(
      balsDate, "dd-MMM-yyyy HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            balsDate = balsDate.Substring(0, 10);

            string strSql = "";
            strSql = "SELECT a.dbt_bal " +
          "FROM accb.accb_accnt_daily_bals a " +
          "WHERE(to_timestamp(a.as_at_date,'YYYY-MM-DD') <=  to_timestamp('" + balsDate +
          "','YYYY-MM-DD') and a.accnt_id = " + accntID + ") ORDER BY to_timestamp(a.as_at_date,'YYYY-MM-DD') DESC LIMIT 1 OFFSET 0";

            DataSet dtst = Global.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return double.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0.00;
            }
        }

        public static double getAccntDailyDbtBals(int accntID, string balsDate)
        {
            balsDate = DateTime.ParseExact(
      balsDate, "dd-MMM-yyyy HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            balsDate = balsDate.Substring(0, 10);

            string strSql = "";
            strSql = "SELECT a.dbt_bal " +
          "FROM accb.accb_accnt_daily_bals a " +
          "WHERE(to_timestamp(a.as_at_date,'YYYY-MM-DD') =  to_timestamp('" + balsDate +
          "','YYYY-MM-DD') and a.accnt_id = " + accntID + ")";

            DataSet dtst = Global.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return double.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0.00;
            }
        }

        public static double getAccntDailyCrdtBals(int accntID, string balsDate)
        {
            balsDate = DateTime.ParseExact(
      balsDate, "dd-MMM-yyyy HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            balsDate = balsDate.Substring(0, 10);

            string strSql = "";
            strSql = "SELECT a.crdt_bal " +
          "FROM accb.accb_accnt_daily_bals a " +
          "WHERE(to_timestamp(a.as_at_date,'YYYY-MM-DD') =  to_timestamp('" + balsDate +
          "','YYYY-MM-DD') and a.accnt_id = " + accntID + ")";

            DataSet dtst = Global.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return double.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0.00;
            }
        }

        public static DataSet get_Bals_Prnt_Accnts(int prntAccntID)
        {
            string strSql = "";
            strSql = "WITH RECURSIVE subaccnt(accnt_id, prnt_accnt_id, accnt_num, accnt_name, debit_balance, credit_balance, net_balance, depth, path, cycle, space) AS " +
      "( " +
        "   SELECT e.accnt_id, e.prnt_accnt_id, e.accnt_num, e.accnt_name, e.debit_balance, e.credit_balance, e.net_balance, 1, ARRAY[e.accnt_id], false, '' FROM accb.accb_chart_of_accnts e WHERE e.prnt_accnt_id = " + prntAccntID +
        "   UNION ALL " +
          "  SELECT d.accnt_id, d.prnt_accnt_id, d.accnt_num, d.accnt_name, d.debit_balance, d.credit_balance, d.net_balance, sd.depth + 1, " +
          "        path || d.accnt_id, " +
          "        d.accnt_id = ANY(path), space || '.' " +
            " FROM " +
              "    accb.accb_chart_of_accnts AS d, " +
                "   subaccnt AS sd " +
                  "  WHERE d.prnt_accnt_id = sd.accnt_id AND NOT cycle " +
      ") " +
      "SELECT SUM(debit_balance), SUM(credit_balance), SUM(net_balance) " +
      "FROM subaccnt " +
      "WHERE accnt_num ilike '%'";
            DataSet dtst = Global.selectDataNoParams(strSql);
            return dtst;
        }

        public static long getAccntDailyCurrBalsID(int accntID, string balsDate)
        {
            balsDate = DateTime.ParseExact(
      balsDate, "dd-MMM-yyyy HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            balsDate = balsDate.Substring(0, 10);

            string strSql = "";
            strSql = "SELECT a.daily_cbals_id " +
          "FROM accb.accb_accnt_crncy_daily_bals a " +
          "WHERE(to_timestamp(a.as_at_date,'YYYY-MM-DD') =  to_timestamp('" + balsDate +
          "','YYYY-MM-DD') and a.accnt_id = " + accntID + ")";

            DataSet dtst = Global.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public static double getAccntLstDailyNetCurrBals(int accntID, string balsDate)
        {
            balsDate = DateTime.ParseExact(
      balsDate, "dd-MMM-yyyy HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            balsDate = balsDate.Substring(0, 10);

            string strSql = "";
            strSql = "SELECT a.net_balance " +
          "FROM accb.accb_accnt_crncy_daily_bals a " +
          "WHERE(to_timestamp(a.as_at_date,'YYYY-MM-DD') <=  to_timestamp('" + balsDate +
          "','YYYY-MM-DD') and a.accnt_id = " + accntID +
          ") ORDER BY to_timestamp(a.as_at_date,'YYYY-MM-DD') DESC LIMIT 1 OFFSET 0";

            DataSet dtst = Global.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return double.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0.00;
            }
        }

        public static double getAccntLstDailyCrdtCurrBals(int accntID, string balsDate)
        {
            balsDate = DateTime.ParseExact(
      balsDate, "dd-MMM-yyyy HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            balsDate = balsDate.Substring(0, 10);

            string strSql = "";
            strSql = "SELECT a.crdt_bal " +
          "FROM accb.accb_accnt_crncy_daily_bals a " +
          "WHERE(to_timestamp(a.as_at_date,'YYYY-MM-DD') <=  to_timestamp('" + balsDate +
          "','YYYY-MM-DD') and a.accnt_id = " + accntID + ") ORDER BY to_timestamp(a.as_at_date,'YYYY-MM-DD') DESC LIMIT 1 OFFSET 0";

            DataSet dtst = Global.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return double.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0.00;
            }
        }

        public static double getAccntLstDailyDbtCurrBals(int accntID, string balsDate)
        {
            balsDate = DateTime.ParseExact(
      balsDate, "dd-MMM-yyyy HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            balsDate = balsDate.Substring(0, 10);

            string strSql = "";
            strSql = "SELECT a.dbt_bal " +
          "FROM accb.accb_accnt_crncy_daily_bals a " +
          "WHERE(to_timestamp(a.as_at_date,'YYYY-MM-DD') <=  to_timestamp('" + balsDate +
          "','YYYY-MM-DD') and a.accnt_id = " + accntID + ") ORDER BY to_timestamp(a.as_at_date,'YYYY-MM-DD') DESC LIMIT 1 OFFSET 0";

            DataSet dtst = Global.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return double.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0.00;
            }
        }

        public static double getAccntDailyDbtCurrBals(int accntID, string balsDate)
        {
            balsDate = DateTime.ParseExact(
      balsDate, "dd-MMM-yyyy HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            balsDate = balsDate.Substring(0, 10);

            string strSql = "";
            strSql = "SELECT a.dbt_bal " +
          "FROM accb.accb_accnt_crncy_daily_bals a " +
          "WHERE(to_timestamp(a.as_at_date,'YYYY-MM-DD') =  to_timestamp('" + balsDate +
          "','YYYY-MM-DD') and a.accnt_id = " + accntID + ")";

            DataSet dtst = Global.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return double.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0.00;
            }
        }

        public static DataSet get_CurrBals_Prnt_Accnts(int prntAccntID, int CurrID)
        {
            string dtestr = Global.getDB_Date_time();
            string strSql = "";
            strSql = @"select SUM(g.dbt_bal), SUM(g.crdt_bal), SUM(g.net_balance)
      from accb.accb_accnt_crncy_daily_bals g, accb.accb_chart_of_accnts h,
      (SELECT  MAX(a.as_at_date) dte1, a.accnt_id accnt1
          from accb.accb_accnt_crncy_daily_bals a, accb.accb_chart_of_accnts b 
          where a.accnt_id=b.accnt_id 
          and a.crncy_id = " + CurrID +
                @" and b.prnt_accnt_id = " + prntAccntID + @"
          and to_timestamp(a.as_at_date,'YYYY-MM-DD') <= to_timestamp('" +
                dtestr.Substring(0, 10) + @"','YYYY-MM-DD') 
          GROUP BY a.accnt_id) tbl1           
          where g.accnt_id=h.accnt_id 
          and g.crncy_id = " + CurrID +
                @" and h.prnt_accnt_id = " + prntAccntID + @"
          and g.as_at_date =tbl1.dte1 
          and g.accnt_id =tbl1.accnt1";
            //      strSql = @"select  SUM(a.dbt_bal), SUM(a.crdt_bal), SUM(a.net_balance), to_timestamp(a.as_at_date,'YYYY-MM-DD')
            //          from accb.accb_accnt_crncy_daily_bals a, accb.accb_chart_of_accnts b 
            //          where a.accnt_id=b.accnt_id and a.crncy_id = " + CurrID + 
            //          " and b.prnt_accnt_id = " + prntAccntID + @"
            //          and to_timestamp(a.as_at_date,'YYYY-MM-DD') <= to_timestamp('" +
            //          dtestr.Substring(0, 10) + @"','YYYY-MM-DD') GROUP BY to_timestamp(a.as_at_date,'YYYY-MM-DD')
            //          ORDER BY to_timestamp(a.as_at_date,'YYYY-MM-DD') DESC LIMIT 1 OFFSET 0;";
            DataSet dtst = Global.selectDataNoParams(strSql);
            return dtst;
        }

        public static DataSet get_CurrBals_Cntrl_Accnts(int cntrlAccntID, int CurrID)
        {
            string dtestr = Global.getDB_Date_time();
            string strSql = "";
            strSql = @"select SUM(g.dbt_bal), SUM(g.crdt_bal), SUM(g.net_balance)
      from accb.accb_accnt_crncy_daily_bals g, accb.accb_chart_of_accnts h,
      (SELECT  MAX(a.as_at_date) dte1, a.accnt_id accnt1
          from accb.accb_accnt_crncy_daily_bals a, accb.accb_chart_of_accnts b 
          where a.accnt_id=b.accnt_id 
          and a.crncy_id = " + CurrID +
                @" and b.control_account_id = " + cntrlAccntID + @"
          and to_timestamp(a.as_at_date,'YYYY-MM-DD') <= to_timestamp('" +
                dtestr.Substring(0, 10) + @"','YYYY-MM-DD') 
          GROUP BY a.accnt_id) tbl1           
          where g.accnt_id=h.accnt_id 
          and g.crncy_id = " + CurrID +
                @" and h.control_account_id = " + cntrlAccntID + @"
          and g.as_at_date =tbl1.dte1 
          and g.accnt_id =tbl1.accnt1";
            //      strSql = @"select  SUM(a.dbt_bal), SUM(a.crdt_bal), SUM(a.net_balance), to_timestamp(a.as_at_date,'YYYY-MM-DD')
            //          from accb.accb_accnt_crncy_daily_bals a, accb.accb_chart_of_accnts b 
            //          where a.accnt_id=b.accnt_id and a.crncy_id = " + CurrID + 
            //          " and b.prnt_accnt_id = " + prntAccntID + @"
            //          and to_timestamp(a.as_at_date,'YYYY-MM-DD') <= to_timestamp('" +
            //          dtestr.Substring(0, 10) + @"','YYYY-MM-DD') GROUP BY to_timestamp(a.as_at_date,'YYYY-MM-DD')
            //          ORDER BY to_timestamp(a.as_at_date,'YYYY-MM-DD') DESC LIMIT 1 OFFSET 0;";
            DataSet dtst = Global.selectDataNoParams(strSql);
            return dtst;
        }

        public static DataSet get_CurrBals_Accnts(int accntID)
        {
            string dtestr = Global.getDB_Date_time();
            string strSql = "";
            strSql = @"select  a.dbt_bal, a.crdt_bal, a.net_balance, to_char(to_timestamp(a.as_at_date,'YYYY-MM-DD'),'DD-Mon-YYYY') 
          from accb.accb_accnt_crncy_daily_bals a
          where a.accnt_id= " + accntID +
                @" and to_timestamp(a.as_at_date,'YYYY-MM-DD') <= to_timestamp('" + dtestr.Substring(0, 10) + @"','YYYY-MM-DD') 
          ORDER BY to_timestamp(a.as_at_date,'YYYY-MM-DD') DESC LIMIT 1 OFFSET 0;";
            DataSet dtst = Global.selectDataNoParams(strSql);
            return dtst;
        }

        public static double get_COA_CRLSum(int orgID)
        {
            string strSql = "";
            strSql = "SELECT SUM(a.net_balance) " +
              "FROM accb.accb_chart_of_accnts a " +
              "WHERE ((a.org_id = " + orgID + ") and " +
              "(a.is_net_income = '0') and (a.control_account_id <=0) " +
              "and (a.accnt_type IN ('EQ','R', 'L')))";
            //(a.is_retained_earnings = '0') and 
            DataSet dtst = Global.selectDataNoParams(strSql);
            double sumRes = 0.00;
            if (dtst.Tables[0].Rows.Count > 0)
            {
                double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out sumRes);
            }
            return Math.Round(sumRes, 2);
        }

        public static double get_COA_AESum(int orgID)
        {
            string strSql = "";
            strSql = "SELECT SUM(a.net_balance) " +
              "FROM accb.accb_chart_of_accnts a " +
              "WHERE ((a.org_id = " + orgID + ") and " +
              "(a.is_net_income = '0') and (a.control_account_id <=0) " +
              "and (a.accnt_type IN ('A','EX')))";
            //(a.is_retained_earnings = '0') 
            DataSet dtst = Global.selectDataNoParams(strSql);
            double sumRes = 0.00;
            if (dtst.Tables[0].Rows.Count > 0)
            {
                double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out sumRes);
            }
            return Math.Round(sumRes, 2);
        }

        public static void chngeTrnsStatus(long trnsid, string status)
        {
            string dateStr = Global.getDB_Date_time();
            string updtSQL = "UPDATE accb.accb_trnsctn_details " +
            "SET last_update_by = " + Global.rnUser_ID + ", last_update_date = '" + dateStr +
                    "', trns_status = '" + status + "'" +
        " WHERE transctn_id = " + trnsid;
            Global.updateDataNoParams(updtSQL);
        }

        public static void changeReconciledStatus(long trnsID, string nwStatus)
        {
            if (trnsID <= 0)
            {
                return;
            }
            //Global.Extra_Adt_Trl_Info = "";
            string updtSQL = "UPDATE accb.accb_trnsctn_details SET is_reconciled = '" +
              nwStatus.Replace("'", "''") + "' WHERE transctn_id=" + trnsID + " or src_trns_id_reconciled = " + trnsID;
            Global.updateDataNoParams(updtSQL);
        }

        public static string incrsOrDcrsAccnt(int accntid, string dbtOrCrdt)
        {
            string accntType = Global.getAccntType(accntid);
            string isContra = Global.isAccntContra(accntid);
            if (isContra == "0")
            {
                if ((accntType == "A" || accntType == "EX") && dbtOrCrdt == "Debit")
                {
                    return "INCREASE";
                }
                else if ((accntType == "A" || accntType == "EX") && dbtOrCrdt == "Credit")
                {
                    return "DECREASE";
                }
                else if ((accntType == "EQ" || accntType == "R" || accntType == "L") && dbtOrCrdt == "Credit")
                {
                    return "INCREASE";
                }
                else if ((accntType == "EQ" || accntType == "R" || accntType == "L") && dbtOrCrdt == "Debit")
                {
                    return "DECREASE";
                }
            }
            else
            {
                if ((accntType == "A" || accntType == "EX") && dbtOrCrdt == "Debit")
                {
                    return "DECREASE";
                }
                else if ((accntType == "A" || accntType == "EX") && dbtOrCrdt == "Credit")
                {
                    return "INCREASE";
                }
                else if ((accntType == "EQ" || accntType == "R" || accntType == "L") && dbtOrCrdt == "Credit")
                {
                    return "DECREASE";
                }
                else if ((accntType == "EQ" || accntType == "R" || accntType == "L") && dbtOrCrdt == "Debit")
                {
                    return "INCREASE";
                }
            }
            return "";
        }

        public static string dbtOrCrdtAccnt(int accntid, string incrsDcrse)
        {
            string accntType = Global.getAccntType(accntid);
            string isContra = Global.isAccntContra(accntid);
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

        public static int dbtOrCrdtAccntMultiplier(int accntid, string incrsDcrse)
        {
            string accntType = Global.getAccntType(accntid);
            string isContra = Global.isAccntContra(accntid);
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
                else if ((accntType == "EQ" || accntType == "R" || accntType == "L") && incrsDcrse == "I")
                {
                    return -1;
                }
                else if ((accntType == "EQ" || accntType == "R" || accntType == "L") && incrsDcrse == "D")
                {
                    return 1;
                }
            }
            return 1;
        }

        public static int getAccntID(string accntname, int orgid)
        {
            //Example username 'admin'
            DataSet dtSt = new DataSet();
            string sqlStr = "select accnt_id from accb.accb_chart_of_accnts where ((lower(accnt_name) = '" +
             accntname.Replace("'", "''").ToLower() + "' or lower(accnt_num) = '" +
             accntname.Replace("'", "''").ToLower() + "') and org_id = " + orgid + ")";
            dtSt = Global.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public static string getAccntName(int accntid)
        {
            //Example username 'admin'
            DataSet dtSt = new DataSet();
            string sqlStr = "select accnt_name from accb.accb_chart_of_accnts where accnt_id = " +
             accntid + "";
            dtSt = Global.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public static string getAccntNum(int accntid)
        {
            //Example username 'admin'
            DataSet dtSt = new DataSet();
            string sqlStr = "select accnt_num from accb.accb_chart_of_accnts where accnt_id = " +
             accntid + "";
            dtSt = Global.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public static string getAccntType(int accntid)
        {
            //Example username 'admin'
            DataSet dtSt = new DataSet();
            string sqlStr = "select accnt_type from accb.accb_chart_of_accnts where accnt_id = " +
             accntid + "";
            dtSt = Global.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public static string isAccntContra(int accntid)
        {
            //Example username 'admin'
            DataSet dtSt = new DataSet();
            string sqlStr = "select is_contra from accb.accb_chart_of_accnts where accnt_id = " +
             accntid + "";
            dtSt = Global.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public static void createTodaysGLBatch(int orgid, string batchnm,
          string batchdesc, string batchsource)
        {
            string dateStr = Global.getDB_Date_time();
            string insSQL = "INSERT INTO accb.accb_trnsctn_batches(" +
                              "batch_name, batch_description, created_by, creation_date, " +
                              "org_id, batch_status, last_update_by, last_update_date, batch_source) " +
              "VALUES ('" + batchnm.Replace("'", "''") + "', '" + batchdesc.Replace("'", "''") +
              "', " + Global.rnUser_ID + ", '" + dateStr + "', " + orgid + ", '0', " +
                              Global.rnUser_ID + ", '" + dateStr + "', '" +
                              batchsource.Replace("'", "''") + "')";
            Global.insertDataNoParams(insSQL);
        }

        public static int get_Suspns_Accnt(int orgid)
        {
            string strSql = "";
            strSql = "SELECT a.accnt_id " +
              "FROM accb.accb_chart_of_accnts a " +
              "WHERE(a.is_suspens_accnt = '1' and a.org_id = " + orgid + ")";
            DataSet dtst = Global.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count == 1)
            {
                return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public static void createTransaction(int accntid, string trnsDesc,
    double dbtAmnt, string trnsDate, int crncyid,
      long batchid, double crdtamnt, double netAmnt,
      double entrdAmt, int entrdCurrID, double acntAmnt, int acntCurrID,
      double funcExchRate, double acntExchRate, string dbtOrCrdt)
        {
            trnsDate = DateTime.ParseExact(
      trnsDate, "dd-MMM-yyyy HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            if (trnsDesc.Length > 500)
            {
                trnsDesc = trnsDesc.Substring(0, 500);
            }
            string dateStr = Global.getDB_Date_time();
            string insSQL = "INSERT INTO accb.accb_trnsctn_details(" +
                              "accnt_id, transaction_desc, dbt_amount, trnsctn_date, " +
                              "func_cur_id, created_by, creation_date, batch_id, crdt_amount, " +
                              @"last_update_by, last_update_date, net_amount, 
            entered_amnt, entered_amt_crncy_id, accnt_crncy_amnt, accnt_crncy_id, 
            func_cur_exchng_rate, accnt_cur_exchng_rate, dbt_or_crdt) " +
                              "VALUES (" + accntid + ", '" + trnsDesc.Replace("'", "''") + "', " + dbtAmnt +
                              ", '" + trnsDate + "', " + crncyid + ", " + Global.rnUser_ID + ", '" + dateStr +
                              "', " + batchid + ", " + crdtamnt + ", " + Global.rnUser_ID +
                              ", '" + dateStr + "'," + netAmnt + ", " + entrdAmt +
                              ", " + entrdCurrID + ", " + acntAmnt +
                              ", " + acntCurrID + ", " + funcExchRate +
                              ", " + acntExchRate + ", '" + dbtOrCrdt + "')";
            Global.insertDataNoParams(insSQL);
        }

        public static string get_GLBatch_Nm(long batchID)
        {
            string strSql = "";
            strSql = "SELECT a.batch_name " +
           "FROM accb.accb_trnsctn_batches a " +
           "WHERE(a.batch_id = " + batchID + ")";

            DataSet dtst = Global.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return dtst.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public static void deleteBatch(long batchid, string batchNm)
        {
            string delSql = "DELETE FROM accb.accb_trnsctn_batches WHERE(batch_id = " + batchid + ")";
            Global.deleteDataNoParams(delSql);
        }

        public static void deleteBatchTrns(long batchid)
        {
            string delSql = "DELETE FROM accb.accb_trnsctn_details WHERE(batch_id = " + batchid + ")";
            Global.deleteDataNoParams(delSql);
        }

        #endregion

        #region "JOURNAL IMPORTS..."
        public static string getGLIntrfcIDs(int accntid, string trns_date, int crncy_id, string tblNme)
        {
            trns_date = DateTime.ParseExact(
       trns_date, "dd-MMM-yyyy HH:mm:ss",
       System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            string strSql = "select distinct a.interface_id from " + tblNme + " a " +
            "where a.accnt_id = " + accntid + " and a.trnsctn_date = '" + trns_date +
            "' and a.func_cur_id = " + crncy_id + " and a.gl_batch_id = -1  " +
            "ORDER BY a.interface_id";
            /*and " +
            "NOT EXISTS(select f.transctn_id from accb.accb_trnsctn_details f " +
            "where f.batch_id IN (select g.batch_id from accb.accb_trnsctn_batches g " +
            "where g.batch_name ilike '%Internal Payments%' and " +
            "to_timestamp(g.creation_date,'YYYY-MM-DD HH24:MI:SS') between " +
            "(to_timestamp(a.trnsctn_date,'YYYY-MM-DD HH24:MI:SS') - interval '6 months') " +
            "and (to_timestamp(a.trnsctn_date,'YYYY-MM-DD HH24:MI:SS') + interval '6 months')) " +
            "and f.source_trns_ids like '%,' || a.interface_id || ',%' and " +
            "f.trnsctn_date=a.trnsctn_date and f.accnt_id= a.accnt_id)*/
            DataSet dtst = Global.selectDataNoParams(strSql);
            string infc_ids = ",";
            for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
            {
                infc_ids = infc_ids + dtst.Tables[0].Rows[a][0].ToString() + ",";
            }
            return infc_ids;
        }

        public static double[] getGLIntrfcIDAmntSum(string intrfcids, string tblNme, int accntID)
        {
            double[] res = { 0, 0 };
            string strSql = @"SELECT COALESCE(SUM(a.dbt_amount),0), COALESCE(SUM(a.crdt_amount),0)
FROM " + tblNme + @" a
WHERE (a.accnt_id = " + accntID + @"
and '" + intrfcids + "' like '%,' || a.interface_id || ',%') ";

            DataSet dtst = Global.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                res[0] = double.Parse(dtst.Tables[0].Rows[0][0].ToString());
                res[1] = double.Parse(dtst.Tables[0].Rows[0][1].ToString());
            }
            return res;
        }

        public static double get_LtstExchRate(int fromCurrID, int toCurrID, string asAtDte)
        {
            int fnccurid = Global.getOrgFuncCurID(Global.UsrsOrg_ID);
            //this.curCode = Global.getPssblValNm(this.curid);
            if (fromCurrID == toCurrID)
            {
                return 1;
            }
            string strSql = "";
            strSql = @"SELECT CASE WHEN a.currency_from_id=" + fromCurrID +
              @" THEN a.multiply_from_by ELSE (1/a.multiply_from_by) END
      FROM accb.accb_exchange_rates a WHERE ((a.currency_from_id=" + fromCurrID +
              @" and a.currency_to_id=" + toCurrID +
              @") or (a.currency_to_id=" + fromCurrID +
              @" and a.currency_from_id=" + toCurrID +
              @")) and to_timestamp(a.conversion_date,'YYYY-MM-DD') <= to_timestamp('" + asAtDte +
              "','DD-Mon-YYYY HH24:MI:SS') ORDER BY to_timestamp(a.conversion_date,'YYYY-MM-DD') DESC LIMIT 1 OFFSET 0";
            DataSet dtst = Global.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return double.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }

            if (fromCurrID != fnccurid && toCurrID != fnccurid)
            {
                double a = Global.get_LtstExchRate(fromCurrID, fnccurid, asAtDte);
                double b = Global.get_LtstExchRate(toCurrID, fnccurid, asAtDte);
                if (a != 0 && b != 0)
                {
                    return a / b;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return 1;
            }
        }

        public static void createPymntGLLine(int accntid, string trnsdesc, double dbtamnt,
        string trnsdte, int crncyid, long batchid, double crdtamnt, double netamnt,
          string srcids, string dateStr,
          double entrdAmt, int entrdCurrID, double acntAmnt, int acntCurrID,
          double funcExchRate, double acntExchRate, string dbtOrCrdt)
        {
            if (accntid <= 0)
            {
                return;
            }
            trnsdte = DateTime.ParseExact(
      trnsdte, "dd-MMM-yyyy HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            dateStr = DateTime.ParseExact(
      dateStr, "dd-MMM-yyyy HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            string insSQL = "INSERT INTO accb.accb_trnsctn_details(" +
                     "accnt_id, transaction_desc, dbt_amount, trnsctn_date, " +
                     "func_cur_id, created_by, creation_date, batch_id, crdt_amount, " +
                     @"last_update_by, last_update_date, net_amount, trns_status, source_trns_ids, 
            entered_amnt, entered_amt_crncy_id, accnt_crncy_amnt, accnt_crncy_id, 
            func_cur_exchng_rate, accnt_cur_exchng_rate, dbt_or_crdt) " +
                     "VALUES (" + accntid + ", '" + trnsdesc.Replace("'", "''") + "', " + dbtamnt +
                     ", '" + trnsdte.Replace("'", "''") + "', " + crncyid + ", " + Global.rnUser_ID +
                     ", '" + dateStr + "', " + batchid + ", " + crdtamnt + ", " +
                     Global.rnUser_ID + ", '" + dateStr + "', " + netamnt +
                     ", '0', '" + srcids + "', " + entrdAmt +
                              ", " + entrdCurrID + ", " + acntAmnt +
                              ", " + acntCurrID + ", " + funcExchRate +
                              ", " + acntExchRate + ", '" + dbtOrCrdt + "')";
            Global.insertDataNoParams(insSQL);
        }

        public static void updtGLIntrfcLnSpclOrg(int orgID, string tblNme, string btchPrfx)
        {
            //Used to update batch ids of interface lines that have gone to GL already
            string dateStr = Global.getDB_Date_time();
            string updtSQL = "UPDATE " + tblNme + " a " +
            "SET gl_batch_id = (select f.batch_id from accb.accb_trnsctn_details f, accb.accb_chart_of_accnts h " +
            "where f.batch_id IN (select g.batch_id from accb.accb_trnsctn_batches g " +
            "where g.batch_name ilike '%" + btchPrfx.Replace(" ", "%") + "%' and " +
            "to_timestamp(g.creation_date,'YYYY-MM-DD HH24:MI:SS') between " +
            "(to_timestamp(a.trnsctn_date,'YYYY-MM-DD HH24:MI:SS') - interval '6 months') " +
            "and (to_timestamp(a.trnsctn_date,'YYYY-MM-DD HH24:MI:SS') + interval '6 months')) and " +
            "f.source_trns_ids like '%,' || a.interface_id || ',%' and " +
            "f.trnsctn_date=a.trnsctn_date and f.accnt_id= a.accnt_id and f.accnt_id= h.accnt_id and h.org_id = " + orgID + ")" +
            ", last_update_by=" + Global.rnUser_ID + ", " +
            "last_update_date='" + dateStr + "' " +
            "WHERE a.gl_batch_id = -1 and EXISTS(select 1 from accb.accb_chart_of_accnts" +
            " m where a.accnt_id= m.accnt_id and m.org_id =" + orgID + ")";
            Global.updateDataNoParams(updtSQL);
        }

        public static void updtPymntAllGLIntrfcLnOrg(long glbatchid, int orgID, string tblNme)
        {
            string dateStr = Global.getDB_Date_time();
            string updtSQL = "UPDATE " + tblNme + " a " +
            "SET gl_batch_id = " + glbatchid +
            ", last_update_by=" + Global.rnUser_ID + ", " +
            "last_update_date='" + dateStr + "' " +
            "WHERE a.gl_batch_id = -1 and EXISTS(select f.transctn_id from accb.accb_trnsctn_details f, accb.accb_chart_of_accnts g " +
            "where f.batch_id = " + glbatchid + " " +
            "and f.source_trns_ids like '%,' || a.interface_id || ',%' and " +
            "f.trnsctn_date=a.trnsctn_date and f.accnt_id= a.accnt_id and f.accnt_id= g.accnt_id and g.org_id = " + orgID + ") ";
            Global.updateDataNoParams(updtSQL);
        }

        public static void updtTodaysGLBatchPstngAvlblty(long batchid, string avlblty)
        {
            string dateStr = Global.getDB_Date_time();
            string insSQL = "UPDATE accb.accb_trnsctn_batches SET avlbl_for_postng='" + avlblty +
              "', last_update_by=" + Global.rnUser_ID +
              ", last_update_date='" + dateStr +
              "' WHERE batch_id = " + batchid;
            Global.updateDataNoParams(insSQL);
        }
        #endregion

        #region "EXCHANGE RATES..."
        public static void createRate(string rate_dte, string curFrom,
          int curFrmID, string curTo, int curToID, double scalefactor)
        {
            rate_dte = DateTime.ParseExact(rate_dte, "dd-MMM-yyyy",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            string dateStr = Global.getDB_Date_time();
            string insSQL = @"INSERT INTO accb.accb_exchange_rates(
            conversion_date, currency_from, currency_from_id, currency_to, 
            currency_to_id, multiply_from_by, created_by, creation_date, 
            last_update_by, last_update_date) " +
                  "VALUES ('" + rate_dte.Replace("'", "''") +
                  "', '" + curFrom.Replace("'", "''") +
                  "', " + curFrmID +
                  ", '" + curTo.Replace("'", "''") +
                  "', " + curToID +
                  ", " + scalefactor +
                  ", " + Global.rnUser_ID + ", '" + dateStr +
                  "', " + Global.rnUser_ID + ", '" + dateStr +
                  "')";
            Global.insertDataNoParams(insSQL);
        }

        public static void updtRate(long rateID, string rate_dte, string curFrom,
          int curFrmID, string curTo, int curToID, double scalefactor)
        {
            //   rate_dte = DateTime.ParseExact(rate_dte, "dd-MMM-yyyy",
            //System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

            //Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.getDB_Date_time();
            string insSQL = @"UPDATE accb.accb_exchange_rates SET 
            conversion_date='" + rate_dte.Replace("'", "''") +
                  "', currency_from='" + curFrom.Replace("'", "''") +
                  "', currency_from_id=" + curFrmID +
                  ", last_update_by=" + Global.rnUser_ID + ", last_update_date='" + dateStr +
                  "', currency_to='" + curTo.Replace("'", "''") +
                  "', currency_to_id=" + curToID +
                  ", multiply_from_by = " + scalefactor +
                  " WHERE rate_id = " + rateID;
            Global.updateDataNoParams(insSQL);
        }

        public static void updtRateValue(long rateID, double scalefactor)
        {
            //Global.Extra_Adt_Trl_Info = "";
            string dateStr = Global.getDB_Date_time();
            string insSQL = @"UPDATE accb.accb_exchange_rates SET 
            last_update_by=" + Global.rnUser_ID +
                  ", last_update_date='" + dateStr +
                  "', multiply_from_by = " + scalefactor +
                  " WHERE rate_id = " + rateID;
            Global.updateDataNoParams(insSQL);
        }

        public static DataSet get_Currencies(string funcCurCode)
        {
            string strSql = "";
            strSql = @"SELECT pssbl_value_id, pssbl_value, pssbl_value_desc,
       is_enabled, allowed_org_ids
  FROM gst.gen_stp_lov_values WHERE pssbl_value != '" +
         funcCurCode.Replace("'", "''") + "' and is_enabled='1' and value_list_id=" + Global.getLovID("Currencies");

            DataSet dtst = Global.selectDataNoParams(strSql);
            return dtst;
        }

        public static int getPssblValID(string pssblVal, int lovID)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "SELECT pssbl_value_id from gst.gen_stp_lov_values " +
             "where ((pssbl_value = '" +
             pssblVal.Replace("'", "''") + "') AND (value_list_id = " + lovID + ")) ORDER BY pssbl_value_id LIMIT 1";
            dtSt = Global.selectDataNoParams(sqlStr);
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
        public static long doesRateExst(string rateDte, string fromCur, string toCur)
        {
            //   rateDte = DateTime.ParseExact(rateDte, "dd-MMM-yyyy",
            //System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            string strSql = "";
            strSql = @"SELECT rate_id 
  FROM accb.accb_exchange_rates WHERE currency_from='" + fromCur.Replace("'", "''") +
                  "' and currency_to='" + toCur.Replace("'", "''") +
                  "' and conversion_date='" + rateDte.Replace("'", "''") +
                  "'";

            DataSet dtst = Global.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }
        #endregion
    }
}
