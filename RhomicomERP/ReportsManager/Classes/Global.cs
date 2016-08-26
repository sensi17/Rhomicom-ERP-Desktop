using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing.Imaging;
using ReportsAndProcesses.Forms;
using System.Windows.Forms;
using CommonCode;

namespace ReportsAndProcesses.Classes
{
    /// <summary>
    /// A  class containing variables and 
    /// functions we will like to call directly from 
    /// anywhere in the project without creating an instance first
    /// </summary>
    class Global
    {
        #region "GLOBAL DECLARATIONS..."
        public static ReportsAndProcesses myRpt = new ReportsAndProcesses();
        public static mainForm mnFrm = null;
        public static string[] dfltPrvldgs = { "View Reports And Processes", 
      /*1*/"View Report Definitions","View Report Runs","View SQL", "View Record History",
      /*5*/"Add Report/Process","Edit Report/Process","Delete Report/Process",
      /*8*/"Run Reports/Process","Delete Report/Process Runs", "View Runs from Others",
      /*11*/"Delete Run Output File"};
        public static string currentPanel = "";
        public static string[] sysParaIDs = { "-130", "-140", "-150", "-160", "-170", "-180", "-190", "-200" };
        public static string[] sysParaNames = { "Report Title:", "Cols Nos To Group or Width & Height (Px) for Charts:",
                                          "Cols Nos To Count or Use in Charts:", "Columns To Sum:", "Columns To Average:",
                                          "Columns To Format Numerically:", "Report Output Formats", "Report Orientations" };
        #endregion

        #region "INSERT STATEMENTS..."
        public static void createRptGrpng(long rptID, string grptitle, string colnos,
          string grpwidth, int noofcols, int grporder, string dsplytyp, int gminheight,
          string colhdrnms, string coldlmtr, string rowdlmtr, string grpborder, int labelwdth)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = @"INSERT INTO rpt.rpt_det_rpt_grps(
            title, col_nos, grp_width_desc, nof_cols_wthn, grp_order, 
            report_id, grp_dsply_type, grp_min_height_px, column_hdr_names, 
            delimiter_col_vals, delimiter_row_vals, created_by, creation_date, 
            last_update_by, last_update_date, grp_border, label_max_width) " +
           "VALUES ('" + grptitle.Replace("'", "''") +
           "', '" + colnos.Replace("'", "''") +
           "', '" + grpwidth.Replace("'", "''") +
           "', " + noofcols +
           ", " + grporder +
           ", " + rptID + ", '" + dsplytyp.Replace("'", "''") +
           "', " + gminheight +
           ", '" + colhdrnms.Replace("'", "''") +
           "', '" + coldlmtr.Replace("'", "''") +
           "', '" + rowdlmtr.Replace("'", "''") +
           "', " + Global.myRpt.user_id + ", '" + dateStr +
           "', " + Global.myRpt.user_id + ", '" + dateStr +
           "', '" + grpborder.Replace("'", "''") + "', " + labelwdth + ")";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void updtRptGrpng(long grpngID, string grptitle, string colnos,
          string grpwidth, int noofcols, int grporder, string dsplytyp, int gminheight,
          string colhdrnms, string coldlmtr, string rowdlmtr, string grpborder, int labelwdth)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = @"UPDATE rpt.rpt_det_rpt_grps 
   SET title='" + grptitle.Replace("'", "''") +
           "', col_nos='" + colnos.Replace("'", "''") +
           "', grp_width_desc='" + grpwidth.Replace("'", "''") +
           "', nof_cols_wthn=" + noofcols +
           ", grp_order=" + grporder +
           ", grp_dsply_type='" + dsplytyp.Replace("'", "''") +
           "', grp_min_height_px=" + gminheight +
           ", column_hdr_names='" + colhdrnms.Replace("'", "''") +
           "', delimiter_col_vals='" + coldlmtr.Replace("'", "''") +
           "', delimiter_row_vals='" + rowdlmtr.Replace("'", "''") +
           "', last_update_by=" + Global.myRpt.user_id + ", last_update_date='" + dateStr +
           "', grp_border='" + grpborder.Replace("'", "''") +
           "', label_max_width=" + labelwdth +
           " WHERE group_id=" + grpngID;
            Global.mnFrm.cmCde.updateDataNoParams(insSQL);
        }

        public static void createPrgmUnts(long rptStID, long prgnUntID)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = @"INSERT INTO rpt.rpt_set_prgrm_units(
            report_set_id, program_unit_id, created_by, creation_date) " +
           "VALUES (" + rptStID + ", " + prgnUntID + ", " +
           Global.myRpt.user_id + ", '" + dateStr +
           "')";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static long getPrgUntPkID(long rptStID, long prgnUntID)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select set_unit_id from rpt.rpt_set_prgrm_units where report_set_id = " +
              rptStID + " and program_unit_id = " + prgnUntID + "";
            dtSt = Global.mnFrm.cmCde.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public static long getRptGrpPkID(long rptID, string grpTitle)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select group_id from rpt.rpt_det_rpt_grps where report_id = " +
              rptID + " and title = '" + grpTitle.Replace("'", "''") + "'";
            dtSt = Global.mnFrm.cmCde.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public static long getSqlRepParamID(long rptID, string sqlRep)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select parameter_id from rpt.rpt_report_parameters where report_id = " +
              rptID + " and paramtr_rprstn_nm_in_query = '" + sqlRep.Replace("'", "''") + "'";
            dtSt = Global.mnFrm.cmCde.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public static long getParamNmID(long rptID, string paramNm)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select parameter_id from rpt.rpt_report_parameters where report_id = " +
              rptID + " and parameter_name = '" + paramNm.Replace("'", "''") + "'";
            dtSt = Global.mnFrm.cmCde.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }
        public static void createPrcsSchdl(long rptID, string strDteTm,
          string rptuom, int rptEvery, bool rnAtHr)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = @"INSERT INTO rpt.rpt_run_schdules(
            report_id, created_by, creation_date, last_update_by, 
            last_update_date, start_dte_tme, repeat_uom, repeat_every,
run_at_spcfd_hour) " +
           "VALUES (" + rptID + ", " + Global.myRpt.user_id + ", '" + dateStr +
           "', " + Global.myRpt.user_id + ", '" + dateStr +
           "', '" + strDteTm.Replace("'", "''") + "', '" + rptuom.Replace("'", "''") +
           "', " + rptEvery + ", '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(rnAtHr) + "')";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void updatePrcsSchdl(long schdlID, long rptID, string strDteTm,
          string rptuom, int rptEvery, bool rnAtHr)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string insSQL = @"UPDATE rpt.rpt_run_schdules SET 
            report_id=" + rptID + ", start_dte_tme='" + strDteTm.Replace("'", "''") +
           "', repeat_uom='" + rptuom.Replace("'", "''") +
           "', last_update_by=" + Global.myRpt.user_id + ", last_update_date='" + dateStr +
           "', repeat_every=" + rptEvery +
           ", run_at_spcfd_hour = '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(rnAtHr) + "' WHERE schedule_id = " + schdlID;
            Global.mnFrm.cmCde.updateDataNoParams(insSQL);
        }

        public static void createPrcsSchdlParms(long alertID, long schdlID, long paramID, string paramVal)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = @"INSERT INTO rpt.rpt_run_schdule_params(
            schedule_id, parameter_id, parameter_value, created_by, 
            creation_date, last_update_by, last_update_date, alert_id) " +
           "VALUES (" + schdlID + ", " + paramID + ", '" + paramVal.Replace("'", "''") + "', " + Global.myRpt.user_id + ", '" + dateStr +
           "', " + Global.myRpt.user_id + ", '" + dateStr + "', " + alertID + ")";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void updatePrcsSchdlParms(long schdlParamID, long paramID, string paramVal)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = @"UPDATE rpt.rpt_run_schdule_params SET 
            parameter_id=" + paramID + ", parameter_value='" + paramVal.Replace("'", "''") +
           "', last_update_by=" + Global.myRpt.user_id + ", last_update_date='" + dateStr +
           "' WHERE schdl_param_id = " + schdlParamID;
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void createPrcsRnnr(string rnnrNm, string rnnrDesc, string lstActvTm, string stats, string rnnPryty, string execFile)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = @"INSERT INTO rpt.rpt_prcss_rnnrs(
            rnnr_name, rnnr_desc, rnnr_lst_actv_dtetme, created_by, 
            creation_date, last_update_by, last_update_date, rnnr_status, 
            crnt_rnng_priority, executbl_file_nm) " +
           "VALUES ('" + rnnrNm.Replace("'", "''") + "', '" + rnnrDesc.Replace("'", "''") +
           "', '" + lstActvTm.Replace("'", "''") + "', " + Global.myRpt.user_id + ", '" + dateStr +
           "', " + Global.myRpt.user_id + ", '" + dateStr +
           "', '" + stats.Replace("'", "''") + "', '" + rnnPryty.Replace("'", "''") +
           "', '" + execFile.Replace("'", "''") + "')";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void updatePrcsRnnr(long rnnrID, string rnnrNm, string rnnrDesc, string lstActvTm, string stats, string rnnPryty, string execFile)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = @"UPDATE rpt.rpt_prcss_rnnrs SET 
            rnnr_name='" + rnnrNm.Replace("'", "''") + "', rnnr_desc='" + rnnrDesc.Replace("'", "''") +
           "', rnnr_lst_actv_dtetme='" + lstActvTm.Replace("'", "''") +
           "', last_update_by=" + Global.myRpt.user_id + ", last_update_date='" + dateStr +
           "', rnnr_status='" + stats.Replace("'", "''") +
           "', crnt_rnng_priority='" + rnnPryty.Replace("'", "''") +
           "', executbl_file_nm='" + execFile.Replace("'", "''") +
           "' WHERE prcss_rnnr_id = " + rnnrID;
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void updatePrcsRnnrNm(long rnnrID, string rnnrNm, string rnnrDesc, string execFile)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = @"UPDATE rpt.rpt_prcss_rnnrs SET 
            rnnr_name='" + rnnrNm.Replace("'", "''") + "', rnnr_desc='" + rnnrDesc.Replace("'", "''") +
           "', last_update_by=-1, last_update_date='" + dateStr +
           "', executbl_file_nm='" + execFile.Replace("'", "''") +
           "' WHERE prcss_rnnr_id = " + rnnrID;
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void updatePrcsRnnrCmd(string rnnrNm, string cmdStr)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = @"UPDATE rpt.rpt_prcss_rnnrs SET 
            shld_rnnr_stop='" + cmdStr.Replace("'", "''") +
           "', last_update_by=" + Global.myRpt.user_id + ", last_update_date='" + dateStr +
           "' WHERE rnnr_name = '" + rnnrNm.Replace("'", "''") + "'";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void createRptRn(long runBy, string runDate,
      long rptID, string paramIDs, string paramVals,
          string outptUsd, string orntUsd, int alertID)
        {
            runDate = DateTime.ParseExact(
      runDate, "dd-MMM-yyyy HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            string insSQL = @"INSERT INTO rpt.rpt_report_runs(
            run_by, run_date, rpt_run_output, run_status_txt, 
            run_status_prct, report_id, rpt_rn_param_ids, rpt_rn_param_vals, 
            output_used, orntn_used, last_actv_date_tme, is_this_from_schdler, alert_id) " +
                  "VALUES (" + runBy + ", '" + runDate +
                  "', '', 'Not Started!', 0, " + rptID + ", '" + paramIDs.Replace("'", "''") +
                  "', '" + paramVals.Replace("'", "''") +
                  "', '" + outptUsd.Replace("'", "''") +
                  "', '" + orntUsd.Replace("'", "''") +
                  "', '" + runDate + "', '0', " + alertID + ")";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void createRpt(string rptNm, string rptDesc,
          string ownrMdl, string rptPrcs, string rptSQL, bool isenbld,
          string colsGrp, string colsCnt, string colsSum, string colsAvrg
          , string colsNoFrmt, string outptTyp, string orntn,
          string prcRnnr, string rptLyout, string dlmtr, string img_cols, string jxrmlFileNm)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = "INSERT INTO rpt.rpt_reports(" +
                  "report_name, report_desc, rpt_sql_query, owner_module, " +
                  "created_by, creation_date, last_update_by, last_update_date, " +
                  "rpt_or_sys_prcs, is_enabled, cols_to_group, cols_to_count, " +
            @"cols_to_sum, cols_to_average, cols_to_no_frmt, output_type, portrait_lndscp, 
            rpt_layout, imgs_col_nos, csv_delimiter, process_runner, jrxml_file_name) " +
                  "VALUES ('" + rptNm.Replace("'", "''") + "', '" + rptDesc.Replace("'", "''") +
                  "', '" + rptSQL.Replace("'", "''") + "', '" + ownrMdl.Replace("'", "''") + "', " +
                     Global.myRpt.user_id + ", '" + dateStr +
                     "', " + Global.myRpt.user_id +
                     ", '" + dateStr + "', '" + rptPrcs.Replace("'", "''") + "', '" +
                     Global.mnFrm.cmCde.cnvrtBoolToBitStr(isenbld) +
                     "', '" + colsGrp.Replace("'", "''") +
                     "', '" + colsCnt.Replace("'", "''") +
                     "', '" + colsSum.Replace("'", "''") +
                     "', '" + colsAvrg.Replace("'", "''") +
                     "', '" + colsNoFrmt.Replace("'", "''") +
                     "', '" + outptTyp.Replace("'", "''") +
                     "', '" + orntn.Replace("'", "''") + "', '" + rptLyout.Replace("'", "''") +
                     "', '" + img_cols.Replace("'", "''") +
                     "', '" + dlmtr.Replace("'", "''") +
                     "', '" + prcRnnr.Replace("'", "''") +
                     "', '" + jxrmlFileNm.Replace("'", "''") +
                     "')";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void createAlert(string alertNm, string alertDesc,
          string toMail, string ccMail, string msgBody, bool isenbld,
          string alrtTyp, string sbjct, string bccMail, string paramsSQL
          , long rptID, bool runRpt, string strtDate,
          string rptUOM, int rptEvery, bool runOnHour, string attchUrls, int endHour)
        {
            strtDate = DateTime.ParseExact(
      strtDate, "dd-MMM-yyyy HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = @"INSERT INTO alrt.alrt_alerts(
            alert_name, alert_desc, to_mail_num_list_mnl, cc_mail_num_list_mnl, 
            alert_msg_body_mnl, alert_type, created_by, creation_date, last_update_by, 
            last_update_date, is_enabled, msg_sbjct_mnl, bcc_mail_num_list_mnl, 
            paramtr_sets_gnrtn_sql, report_id, shd_rpt_be_run, start_dte_tme, 
            repeat_uom, repeat_every, run_at_spcfd_hour, attchment_urls, end_hour) " +
                  "VALUES ('" + alertNm.Replace("'", "''") + "', '" + alertDesc.Replace("'", "''") +
                  "', '" + toMail.Replace("'", "''") + "', '" + ccMail.Replace("'", "''") +
                  "', '" + msgBody.Replace("'", "''") + "', '" + alrtTyp.Replace("'", "''") + "', " +
                     Global.myRpt.user_id + ", '" + dateStr +
                     "', " + Global.myRpt.user_id +
                     ", '" + dateStr + "', '" +
                     Global.mnFrm.cmCde.cnvrtBoolToBitStr(isenbld) +
                     "', '" + sbjct.Replace("'", "''") +
                     "', '" + bccMail.Replace("'", "''") +
                     "', '" + paramsSQL.Replace("'", "''") +
                     "', " + rptID +
                     ", '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(runRpt) +
                     "', '" + strtDate.Replace("'", "''") +
                     "', '" + rptUOM.Replace("'", "''") + "', " + rptEvery +
                     ", '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(runOnHour) +
                     "', '" + attchUrls.Replace("'", "''") +
                     "', " + endHour + ")";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void updateAlert(int alertID, string alertNm, string alertDesc,
        string toMail, string ccMail, string msgBody, bool isenbld,
        string alrtTyp, string sbjct, string bccMail, string paramsSQL
        , long rptID, bool runRpt, string strtDate,
        string rptUOM, int rptEvery, bool runOnHour, string attchUrls, int endHour)
        {
            strtDate = DateTime.ParseExact(
      strtDate, "dd-MMM-yyyy HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = @"UPDATE alrt.alrt_alerts SET 
            alert_name='" + alertNm.Replace("'", "''") + "', alert_desc='" + alertDesc.Replace("'", "''") +
                  "', to_mail_num_list_mnl='" + toMail.Replace("'", "''") +
                  "', cc_mail_num_list_mnl='" + ccMail.Replace("'", "''") +
                  "', alert_msg_body_mnl='" + msgBody.Replace("'", "''") +
                  "', alert_type='" + alrtTyp.Replace("'", "''") +
                  "', last_update_by=" + Global.myRpt.user_id + ", last_update_date='" + dateStr +
                     "', is_enabled='" +
                     Global.mnFrm.cmCde.cnvrtBoolToBitStr(isenbld) +
                     "', msg_sbjct_mnl='" + sbjct.Replace("'", "''") +
                     "', bcc_mail_num_list_mnl='" + bccMail.Replace("'", "''") +
                     "', paramtr_sets_gnrtn_sql='" + paramsSQL.Replace("'", "''") +
                     "', report_id=" + rptID +
                     ", shd_rpt_be_run='" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(runRpt) +
                     "', start_dte_tme='" + strtDate.Replace("'", "''") +
                     "', repeat_uom='" + rptUOM.Replace("'", "''") +
                     "', repeat_every=" + rptEvery +
                     ", run_at_spcfd_hour='" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(runOnHour) +
                     "', attchment_urls='" + attchUrls.Replace("'", "''") +
                     "', end_hour=" + endHour +
                     " WHERE alert_id = " + alertID;
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void createParam(long rptID, string paramNm,
      string qryRep, string dfltVal, bool isrqrd, string lov_name
          , string dataType, string datefrmt, string lovNm)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = "INSERT INTO rpt.rpt_report_parameters(" +
                  "report_id, parameter_name, paramtr_rprstn_nm_in_query, " +
                  "created_by, creation_date, last_update_by, last_update_date, " +
                  "default_value, is_required, lov_name_id, param_data_type, date_format, lov_name) " +
                  "VALUES (" + rptID + ", '" + paramNm.Replace("'", "''") +
                  "', '" + qryRep.Replace("'", "''") + "', " +
                     Global.myRpt.user_id + ", '" + dateStr +
                     "', " + Global.myRpt.user_id +
                     ", '" + dateStr + "', '" + dfltVal.Replace("'", "''") + "', '" +
                     Global.mnFrm.cmCde.cnvrtBoolToBitStr(isrqrd) +
                     "', '" + lov_name.Replace("'", "''") + "', '" + dataType.Replace("'", "''") +
                     "', '" + datefrmt.Replace("'", "''") + "', '" + lovNm.Replace("'", "''") + "')";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void createRptRole(long rptID, int roleID)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = "INSERT INTO rpt.rpt_reports_allwd_roles(" +
                  "report_id, user_role_id, created_by, creation_date) " +
                  "VALUES (" + rptID + ", " + roleID +
                  ", " +
                     Global.myRpt.user_id + ", '" + dateStr +
                     "')";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        #endregion

        #region "UPDATE STATEMENTS..."
        public static void updateRptRnParams(long rptrnid,
          string paramIDs, string paramVals, string outputUsd, string orntn)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE rpt.rpt_report_runs SET " +
                     "rpt_rn_param_ids = '" + paramIDs.Replace("'", "''") +
                     "', rpt_rn_param_vals = '" + paramVals.Replace("'", "''") +
             "', output_used = '" + outputUsd.Replace("'", "''") +
             "', orntn_used= '" + orntn.Replace("'", "''") +
             "' WHERE (rpt_run_id = " + rptrnid + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updateRptRn(long rptrnid, string statustxt, int statusprcnt)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE rpt.rpt_report_runs SET " +
                     "run_status_txt = '" + statustxt.Replace("'", "''") +
                     "', run_status_prct = " + statusprcnt +
             " WHERE (rpt_run_id = " + rptrnid + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updateRptRnActvTime(long rptrnid)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE rpt.rpt_report_runs SET " +
                     "last_actv_date_tme = '" + dateStr.Replace("'", "''") +
                     "' WHERE (rpt_run_id = " + rptrnid + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updateRptRnOutpt(long rptrnid, string outputTxt)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE rpt.rpt_report_runs SET " +
                     "rpt_run_output = '" + outputTxt.Replace("'", "''") +
             "' WHERE (rpt_run_id = " + rptrnid + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updateRptRnStopCmd(long rptrnid, string cmdStr)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE rpt.rpt_report_runs SET " +
                     "shld_run_stop = '" + cmdStr.Replace("'", "''") +
             "' WHERE (rpt_run_id = " + rptrnid + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updateRptJrxml(long rptid, string jxrmlFileNm)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE rpt.rpt_reports SET " +
                     "last_update_by = " + Global.myRpt.user_id + ", " +
                     "last_update_date = '" + dateStr +
                     "', jrxml_file_name='" + jxrmlFileNm.Replace("'", "''") +
                     "' " +
        "WHERE (report_id = " + rptid + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updateRpt(long rptid, string rptNm, string rptDesc,
          string ownrMdl, string rptPrcs, string rptSQL, bool isenbld,
          string colsGrp, string colsCnt, string colsSum, string colsAvrg,
          string colsNoFrmt, string outptTyp, string orntn,
          string prcRnnr, string rptLyout, string dlmtr, string img_cols, string jxrmlFileNm)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE rpt.rpt_reports SET " +
                     "report_name = '" + rptNm.Replace("'", "''") +
                     "', report_desc = '" + rptDesc.Replace("'", "''") +
                     "', rpt_sql_query = '" + rptSQL.Replace("'", "''") + "', " +
                     "owner_module = '" + ownrMdl.Replace("'", "''") + "', " +
                     "is_enabled = '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(isenbld) +
                     "', last_update_by = " + Global.myRpt.user_id + ", " +
                     "last_update_date = '" + dateStr +
                     "', rpt_or_sys_prcs = '" + rptPrcs.Replace("'", "''") +
                     "', cols_to_group = '" + colsGrp.Replace("'", "''") +
                     "', cols_to_count = '" + colsCnt.Replace("'", "''") +
                     "', cols_to_sum = '" + colsSum.Replace("'", "''") +
                     "', cols_to_average = '" + colsAvrg.Replace("'", "''") +
                     "', cols_to_no_frmt = '" + colsNoFrmt.Replace("'", "''") + "'" +
                     ", output_type = '" + outptTyp.Replace("'", "''") + "'" +
                     ", portrait_lndscp = '" + orntn.Replace("'", "''") +
                     "', rpt_layout='" + rptLyout.Replace("'", "''") +
                     "', imgs_col_nos='" + img_cols.Replace("'", "''") +
                     "', csv_delimiter='" + dlmtr.Replace("'", "''") +
                     "', process_runner='" + prcRnnr.Replace("'", "''") +
                     "', jrxml_file_name='" + jxrmlFileNm.Replace("'", "''") +
                     "' " +
        "WHERE (report_id = " + rptid + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updateParam(long paramid, string paramNm,
      string qryRep, string dfltVal, bool isrqrd, string lov_name,
    string dataType, string datefrmt, string lovNm)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE rpt.rpt_report_parameters SET " +
                     "parameter_name = '" + paramNm.Replace("'", "''") +
                     "', paramtr_rprstn_nm_in_query = '" + qryRep.Replace("'", "''") +
                     "', default_value = '" + dfltVal.Replace("'", "''") + "', " +
                     "is_required = '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(isrqrd) +
                     "', last_update_by = " + Global.myRpt.user_id + ", " +
                     "last_update_date = '" + dateStr +
                     "', lov_name_id = '" + lov_name.Replace("'", "''") +
                     "', param_data_type = '" + dataType.Replace("'", "''") +
                     "', date_format = '" + datefrmt.Replace("'", "''") +
                     "', lov_name = '" + lovNm.Replace("'", "''") + "' " +
          "WHERE (parameter_id = " + paramid + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }


        public static void updateParamLOV(long lovID, string paramNm,
      string qryRep)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE rpt.rpt_report_parameters SET " +
                     "lov_name_id = '" + lovID + "' " +
             "WHERE (paramtr_rprstn_nm_in_query = '" + qryRep.Replace("'", "''") +
                     "' and report_id >=1 and report_id <=500)";
            /*parameter_name = '" + paramNm.Replace("'", "''") +
                     "' and */
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        #endregion

        #region "DELETE STATEMENTS..."
        public static bool isRptInUse(long rptID)
        {
            /*string strSql = "SELECT a.parameter_id " +
             "FROM rpt.rpt_report_parameters a " +
             "WHERE(a.report_id  = " + rptID + ") LIMIT 1";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
              return true;
            }
            string strSql = "SELECT a.rpt_roles_id " +
             "FROM rpt.rpt_reports_allwd_roles a " +
             "WHERE(a.report_id  = " + rptID + ") LIMIT 1";
            dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
              return true;
            }*/
            string strSql = "SELECT a.rpt_run_id " +
             "FROM rpt.rpt_report_runs a " +
             "WHERE(a.report_id  = " + rptID + ") LIMIT 1";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return true;
            }

            return false;
        }

        public static double computeMathExprsn(string exprSn)
        {
            string strSql = "";
            strSql = "SELECT " + exprSn.Replace("/", "::float/").Replace("=", "").Replace(",", "").Replace("'", "''");

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams1(strSql);
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

        public static bool isAlertInUse(long alertID)
        {
            string strSql = "SELECT a.rpt_run_id " +
             "FROM rpt.rpt_report_runs a " +
             "WHERE(a.alert_id  = " + alertID + ") LIMIT 1";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return true;
            }

            return false;
        }
        #endregion

        #region "SELECT STATEMENTS..."
        #region "REPORT NAMES..."
        public static DataSet get_PrcsRnnrs()
        {
            string selSQL = @"SELECT prcss_rnnr_id, rnnr_name, rnnr_desc, rnnr_lst_actv_dtetme, rnnr_status, 
       executbl_file_nm, crnt_rnng_priority 
       FROM rpt.rpt_prcss_rnnrs 
       ORDER BY rnnr_name";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
            return dtst;
        }

        public static bool isRunnrRnng(string rnnrNm)
        {
            string selSQL = @"SELECT age(now(), 
to_timestamp(CASE WHEN rnnr_lst_actv_dtetme='' THEN '2013-01-01 00:00:00' ELSE rnnr_lst_actv_dtetme END, 'YYYY-MM-DD HH24:MI:SS')) " +
              @"<= interval '50 second' 
       FROM rpt.rpt_prcss_rnnrs WHERE rnnr_name='" + rnnrNm.Replace("'", "''") +
              "'";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
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

        public static DataSet get_Schdules(long USER_ID)
        {
            string whrcls = "";

            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[10]))
            {
                whrcls = " or a.created_by != " + USER_ID + "";
            }
            string selSQL = @"SELECT a.schedule_id, a.report_id, b.report_name, 
        to_char(to_timestamp(a.start_dte_tme,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') start_date, 
        a.repeat_every, a.repeat_uom, a.run_at_spcfd_hour 
        FROM rpt.rpt_run_schdules a, rpt.rpt_reports b 
        WHERE a.report_id=b.report_id and (a.created_by=" + USER_ID + whrcls + ") ORDER BY a.schedule_id DESC";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
            return dtst;
        }

        public static long get_SchduleID(long USER_ID, long rptID, string strtdte)
        {
            string selSQL = @"SELECT a.schedule_id 
       FROM rpt.rpt_run_schdules a, rpt.rpt_reports b 
        WHERE a.report_id=b.report_id and a.created_by=" + USER_ID +
        " and a.report_id=" + rptID + " and a.start_dte_tme='" + strtdte +
        "'";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public static long get_SchduleParamID(long schdlID, long paramID)
        {
            string selSQL = @"SELECT a.schdl_param_id 
       FROM rpt.rpt_run_schdule_params a 
        WHERE a.schedule_id=" + schdlID +
        " and a.parameter_id=" + paramID + " ";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public static long get_AlertParamID(long alertID, long paramID)
        {
            string selSQL = @"SELECT a.schdl_param_id 
       FROM rpt.rpt_run_schdule_params a 
        WHERE a.alert_id=" + alertID +
        " and a.parameter_id=" + paramID + " ";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }
        public static DataSet get_Schdules()
        {
            string selSQL = @"SELECT a.schedule_id, a.report_id, b.report_name, a.start_dte_tme, a.repeat_every, a.repeat_uom 
       FROM rpt.rpt_run_schdules a, rpt.rpt_reports b 
        WHERE a.report_id=b.report_id
       ORDER BY a.schedule_id DESC";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
            return dtst;
        }

        public static DataSet get_SchdulesParams(long schdlID)
        {
            string selSQL = @"SELECT a.schdl_param_id, a.parameter_id, b.parameter_name, a.parameter_value
      FROM rpt.rpt_run_schdule_params a, rpt.rpt_report_parameters b  
      WHERE a.parameter_id = b.parameter_id and a.schedule_id=" + schdlID + " ORDER BY a.parameter_id";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
            return dtst;
        }

        public static DataSet get_AlertParams(long alertID)
        {
            string selSQL = @"SELECT a.schdl_param_id, a.parameter_id, b.parameter_name, a.parameter_value
      FROM rpt.rpt_run_schdule_params a, rpt.rpt_report_parameters b  
      WHERE a.parameter_id = b.parameter_id and a.alert_id=" + alertID + " ORDER BY a.parameter_id";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
            return dtst;
        }

        public static string get_Rpt_SQL(long rptID)
        {
            string strSql = "SELECT rpt_sql_query " +
       "FROM rpt.rpt_reports WHERE report_id = " + rptID;

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return dtst.Tables[0].Rows[0][0].ToString();
            }
            return "";
        }

        public static string get_Alert_SQL(long rptID)
        {
            string strSql = "SELECT paramtr_sets_gnrtn_sql " +
       "FROM alrt.alrt_alerts WHERE alert_id = " + rptID;

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return dtst.Tables[0].Rows[0][0].ToString();
            }
            return "";
        }

        public static DataSet get_Rpt_ColsToAct(long rptID)
        {
            string strSql = "SELECT cols_to_group, cols_to_count, cols_to_sum, cols_to_average, cols_to_no_frmt " +
            "FROM rpt.rpt_reports WHERE report_id = " + rptID;

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static long doesRptHvRole(long rptID, int role_id)
        {
            string strSql = "SELECT rpt_roles_id FROM rpt.rpt_reports_allwd_roles " +
              "WHERE report_id = " + rptID + " and user_role_id = " + role_id;

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public static DataSet get_AllParams(long rptID)
        {
            string strSql = "SELECT parameter_id, parameter_name, paramtr_rprstn_nm_in_query, default_value, " +
       "is_required, lov_name_id, param_data_type, date_format FROM rpt.rpt_report_parameters WHERE report_id = " + rptID + " ORDER BY parameter_name";
            Global.mnFrm.params_SQL = strSql;

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static DataSet get_AllParams(Int64 offset, int limit_size)
        {
            string strSql = "SELECT parameter_id, parameter_name, paramtr_rprstn_nm_in_query, default_value, " +
       "is_required, lov_name_id, param_data_type, date_format, rpt.get_rpt_name(report_id), lov_name FROM rpt.rpt_report_parameters ORDER BY report_id, parameter_name  LIMIT " + limit_size +
        " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            Global.mnFrm.params_SQL = strSql;

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static DataSet get_AllGrpngs(long rptID)
        {
            string strSql = @"SELECT group_id, grp_dsply_type, title, col_nos, grp_width_desc, nof_cols_wthn, grp_order, 
       grp_min_height_px, grp_border, label_max_width, column_hdr_names, 
       delimiter_col_vals, delimiter_row_vals
  FROM rpt.rpt_det_rpt_grps WHERE report_id = " + rptID + " ORDER BY grp_order, group_id";
            //Global.mnFrm.params_SQL = strSql;

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static DataSet get_AllPrgmUnts(long rptID)
        {
            string strSql = @"SELECT set_unit_id, program_unit_id, 
rpt.get_rpt_name(program_unit_id) prg_nm
        FROM rpt.rpt_set_prgrm_units " +
              "WHERE report_set_id = " + rptID + "";

            //Global.mnFrm.roles_SQL = strSql;
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static bool isDteTmeWthnIntrvl(string in_date, string intrval)
        {
            //
            string sqlStr = "SELECT age(now(), to_timestamp('" + in_date + "', 'DD-Mon-YYYY HH24:MI:SS')) " +
                   "<= interval '" + intrval + "'";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(sqlStr);
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

        public static bool doesDteTmeExceedIntrvl(string in_date, string intrval)
        {
            //
            string sqlStr = "SELECT age(now(), to_timestamp('" +
                             in_date + "', 'DD-Mon-YYYY HH24:MI:SS')) " +
                            " > interval '" + intrval + "'";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(sqlStr);
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

        public static DataSet get_AllRoles(long rptID)
        {
            string strSql = "SELECT a.user_role_id, b.role_name, a.rpt_roles_id " +
              "FROM rpt.rpt_reports_allwd_roles a, sec.sec_roles b " +
              "WHERE a.report_id = " + rptID + " and a.user_role_id = b.role_id";

            Global.mnFrm.roles_SQL = strSql;
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static string concatCurRoleIDs()
        {
            string nwStr = "-1000000";
            int totl = Global.mnFrm.cmCde.Role_Set_IDs.Length;
            for (int i = 0; i < totl; i++)
            {
                nwStr = nwStr + "," + Global.mnFrm.cmCde.Role_Set_IDs[i].ToString();
                if (i < totl - 1)
                {
                    //nwStr = nwStr + ",";
                }
            }
            return nwStr;
        }

        //public static string concatCurRoleIDs()
        //{
        //  string nwStr = "-1000000";
        //  int totl = Global.mnFrm.cmCde.Role_Set_IDs.Length;
        //  for (int i = 0; i < totl; i++)
        //  {
        //    nwStr = nwStr + Global.mnFrm.cmCde.Role_Set_IDs[i].ToString();
        //    if (i < totl - 1)
        //    {
        //      nwStr = nwStr + ",";
        //    }
        //  }
        //  return nwStr;
        //}

        public static DataSet get_Rpt_Alerts(int rptID)
        {
            string strSql = "";

            strSql = @"SELECT alert_id, report_id, alert_name 
  FROM alrt.alrt_alerts a WHERE a.report_id = " + rptID +
        " ORDER BY 1 DESC";


            Global.mnFrm.alert_SQL = strSql;
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static long getAlertID(string alertname)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select alert_id from alrt.alrt_alerts where lower(alert_name) = '" +
             alertname.Replace("'", "''").ToLower() + "'";
            dtSt = Global.mnFrm.cmCde.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public static DataSet get_Alert_Det(int alertID)
        {
            string strSql = "";

            strSql = @"SELECT alert_id, alert_name, alert_desc, to_mail_num_list_mnl, cc_mail_num_list_mnl, 
       alert_msg_body_mnl, alert_type, is_enabled, msg_sbjct_mnl, bcc_mail_num_list_mnl, 
       paramtr_sets_gnrtn_sql, report_id, shd_rpt_be_run, to_char(to_timestamp(start_dte_tme,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), 
       repeat_uom, repeat_every, run_at_spcfd_hour, attchment_urls, end_hour
  FROM alrt.alrt_alerts a WHERE a.alert_id = " + alertID;


            //Global.mnFrm.alert_SQL = strSql;
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }
        public static DataSet get_Basic_Rpt(string searchWord, string searchIn,
          Int64 offset, int limit_size, string orderLvl)
        {
            string strSql = "";
            string orderBy = "ORDER BY a.report_id DESC";
            if (orderLvl == "ID DESC")
            {
                orderBy = "ORDER BY a.report_id DESC";
            }
            else if (orderLvl == "NAME ASC")
            {
                orderBy = "ORDER BY a.report_name";
            }
            else if (orderLvl == "OWNER MODULE, NAME ASC")
            {
                orderBy = "ORDER BY a.owner_module, a.report_name";
            }

            //    strSql = "SELECT a.report_id, a.report_name, a.report_desc, a.rpt_sql_query, " +
            //    "a.owner_module, a.rpt_or_sys_prcs, a.is_enabled FROM rpt.rpt_reports a, " +
            //    "rpt.rpt_reports_allwd_roles b " +
            //    "WHERE((a.report_id = b.report_id) and (b.user_role_id IN (" + this.concatCurRoleIDs() + ")) " +
            //"and (a.report_name ilike '" + searchWord.Replace("'", "''") +
            //"')) ORDER BY a.report_id DESC LIMIT " + limit_size +
            //" OFFSET " + (Math.Abs(offset * limit_size)).ToString();

            if (searchIn == "Report Name")
            {
                strSql = "SELECT distinct a.report_id, a.report_name, a.report_desc, a.rpt_sql_query, " +
              "a.owner_module, a.rpt_or_sys_prcs, a.is_enabled, a.cols_to_group, a.cols_to_count, " +
              @"a.cols_to_sum, a.cols_to_average, a.cols_to_no_frmt, a.output_type, a.portrait_lndscp
      ,a.process_runner , a.rpt_layout, a.imgs_col_nos, a.csv_delimiter, a.jrxml_file_name
      FROM rpt.rpt_reports a, " +
              "rpt.rpt_reports_allwd_roles b  " +
          "WHERE ((a.report_id = b.report_id) and (a.report_name ilike '" + searchWord.Replace("'", "''") +
          "') and (b.user_role_id IN (" + Global.concatCurRoleIDs() + "))) " + orderBy + " LIMIT " + limit_size +
          " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            else if (searchIn == "Report Description")
            {
                strSql = "SELECT distinct a.report_id, a.report_name, a.report_desc, a.rpt_sql_query, " +
              "a.owner_module, a.rpt_or_sys_prcs, a.is_enabled, a.cols_to_group, a.cols_to_count, " +
              @"a.cols_to_sum, a.cols_to_average, a.cols_to_no_frmt, a.output_type, a.portrait_lndscp
      ,a.process_runner , a.rpt_layout, a.imgs_col_nos, a.csv_delimiter, a.jrxml_file_name
      FROM rpt.rpt_reports a, " +
              "rpt.rpt_reports_allwd_roles b  " +
        "WHERE ((a.report_id = b.report_id) and (a.report_desc ilike '" + searchWord.Replace("'", "''") +
        "') and (b.user_role_id IN (" + Global.concatCurRoleIDs() + "))) " + orderBy + " LIMIT " + limit_size +
        " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            else if (searchIn == "Owner Module")
            {
                strSql = "SELECT distinct a.report_id, a.report_name, a.report_desc, a.rpt_sql_query, " +
              "a.owner_module, a.rpt_or_sys_prcs, a.is_enabled, a.cols_to_group, a.cols_to_count, " +
              @"a.cols_to_sum, a.cols_to_average, a.cols_to_no_frmt, a.output_type, a.portrait_lndscp
      ,a.process_runner , a.rpt_layout, a.imgs_col_nos, a.csv_delimiter, a.jrxml_file_name
      FROM rpt.rpt_reports a, " +
              "rpt.rpt_reports_allwd_roles b  " +
        "WHERE ((a.report_id = b.report_id) and (a.owner_module ilike '" + searchWord.Replace("'", "''") +
        "') and (b.user_role_id IN (" + Global.concatCurRoleIDs() + "))) " + orderBy + " LIMIT " + limit_size +
        " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }

            Global.mnFrm.rpt_SQL = strSql;
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static long get_total_Rpt(string searchWord, string searchIn)
        {
            string strSql = "";
            if (searchIn == "Report Name")
            {
                strSql = "SELECT count(distinct a.report_id) FROM rpt.rpt_reports a, " +
              "rpt.rpt_reports_allwd_roles b  " +
          "WHERE ((a.report_id = b.report_id) and (a.report_name ilike '" + searchWord.Replace("'", "''") +
          "') and (b.user_role_id IN (" + Global.concatCurRoleIDs() + ")))";
            }
            else if (searchIn == "Report Description")
            {
                strSql = "SELECT count(distinct a.report_id) FROM rpt.rpt_reports a, " +
              "rpt.rpt_reports_allwd_roles b  " +
         "WHERE ((a.report_id = b.report_id) and (a.report_desc ilike '" + searchWord.Replace("'", "''") +
         "') and (b.user_role_id IN (" + Global.concatCurRoleIDs() + ")))";
            }
            else if (searchIn == "Owner Module")
            {
                strSql = "SELECT count(distinct a.report_id) FROM rpt.rpt_reports a, " +
              "rpt.rpt_reports_allwd_roles b  " +
        "WHERE ((a.report_id = b.report_id) and (a.owner_module ilike '" + searchWord.Replace("'", "''") +
        "') and (b.user_role_id IN (" + Global.concatCurRoleIDs() + ")))";
            }
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return Int64.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }


        public static DataSet get_Basic_Rpt1(string searchWord, string searchIn,
          Int64 offset, int limit_size, string orderLvl)
        {
            string strSql = "";
            string orderBy = "ORDER BY report_id DESC";
            if (orderLvl == "ID DESC")
            {
                orderBy = "ORDER BY report_id DESC";
            }
            else if (orderLvl == "ID ASC")
            {
                orderBy = "ORDER BY report_id ASC";
            }
            else if (orderLvl == "NAME ASC")
            {
                orderBy = "ORDER BY report_name";
            }
            else if (orderLvl == "OWNER MODULE, NAME ASC")
            {
                orderBy = "ORDER BY owner_module, report_name";
            }
            /*        
             * strSql = "SELECT a.report_id, a.report_name, a.report_desc, a.rpt_sql_query, " +
            "a.owner_module, a.rpt_or_sys_prcs, a.is_enabled FROM rpt.rpt_reports a, " + 
            "rpt.rpt_reports_allwd_roles b " +
            WHERE((a.report_id = b.report_id) and (b.user_role_id IN ("+this.concatCurRoleIDs()+")) " +
        "and (a.report_name ilike '" + searchWord.Replace("'", "''") +
        "')) ORDER BY a.report_id DESC LIMIT " + limit_size +
        " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
      */
            if (searchIn == "Report Name")
            {
                strSql = "SELECT report_id, report_name, report_desc, rpt_sql_query, " +
              "owner_module, rpt_or_sys_prcs, is_enabled, cols_to_group, cols_to_count, " +
              @"a.cols_to_sum, a.cols_to_average, a.cols_to_no_frmt, a.output_type, a.portrait_lndscp
      ,a.process_runner , a.rpt_layout, a.imgs_col_nos, a.csv_delimiter, a.jrxml_file_name
      FROM rpt.rpt_reports a " +
          "WHERE ((report_name ilike '" + searchWord.Replace("'", "''") +
          "')) " + orderBy + " LIMIT " + limit_size +
          " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            else if (searchIn == "Report Description")
            {
                strSql = "SELECT report_id, report_name, report_desc, rpt_sql_query, " +
         "owner_module, rpt_or_sys_prcs, is_enabled, cols_to_group, cols_to_count, " +
              @"a.cols_to_sum, a.cols_to_average, a.cols_to_no_frmt, a.output_type, a.portrait_lndscp
      ,a.process_runner , a.rpt_layout, a.imgs_col_nos, a.csv_delimiter, a.jrxml_file_name
      FROM rpt.rpt_reports a " +
        "WHERE ((report_desc ilike '" + searchWord.Replace("'", "''") +
        "')) " + orderBy + " LIMIT " + limit_size +
        " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            else if (searchIn == "Owner Module")
            {
                strSql = "SELECT report_id, report_name, report_desc, rpt_sql_query, " +
         "owner_module, rpt_or_sys_prcs, is_enabled, cols_to_group, cols_to_count, " +
              @"a.cols_to_sum, a.cols_to_average, a.cols_to_no_frmt, a.output_type, a.portrait_lndscp
      ,a.process_runner , a.rpt_layout, a.imgs_col_nos, a.csv_delimiter, a.jrxml_file_name
      FROM rpt.rpt_reports a " +
        "WHERE ((owner_module ilike '" + searchWord.Replace("'", "''") +
        "')) " + orderBy + " LIMIT " + limit_size +
        " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }

            Global.mnFrm.rpt_SQL = strSql;
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static long get_total_Rpt1(string searchWord, string searchIn)
        {
            string strSql = "";
            if (searchIn == "Report Name")
            {
                strSql = "SELECT count(1) FROM rpt.rpt_reports " +
          "WHERE ((report_name ilike '" + searchWord.Replace("'", "''") +
          "'))";
            }
            else if (searchIn == "Report Description")
            {
                strSql = "SELECT count(1) FROM rpt.rpt_reports " +
         "WHERE ((report_desc ilike '" + searchWord.Replace("'", "''") +
         "'))";
            }
            else if (searchIn == "Owner Module")
            {
                strSql = "SELECT count(1) FROM rpt.rpt_reports " +
        "WHERE ((owner_module ilike '" + searchWord.Replace("'", "''") +
        "'))";
            }
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return Int64.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        public static string get_Rpt_Rec_Hstry(long rptID)
        {
            string strSQL = @"SELECT a.created_by, 
to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS'), 
      a.last_update_by, 
      to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS') " +
            "FROM rpt.rpt_reports a WHERE(a.report_id = " + rptID + ")";
            string fnl_str = "";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSQL);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                fnl_str = "CREATED BY: " + Global.mnFrm.cmCde.get_user_name(long.Parse(dtst.Tables[0].Rows[0][0].ToString())) +
                  "\r\nCREATION DATE: " + dtst.Tables[0].Rows[0][1].ToString() + "\r\nLAST UPDATE BY:" +
                  Global.mnFrm.cmCde.get_user_name(long.Parse(dtst.Tables[0].Rows[0][2].ToString())) +
                  "\r\nLAST UPDATE DATE: " + dtst.Tables[0].Rows[0][3].ToString();
                return fnl_str;
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region "REPORT RUNS..."
        public static long getRptRnID(long rptID, long runBy, string runDate)
        {
            runDate = DateTime.ParseExact(
       runDate, "dd-MMM-yyyy HH:mm:ss",
       System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            DataSet dtSt = new DataSet();
            string sqlStr = "select rpt_run_id from rpt.rpt_report_runs where run_by = " +
              runBy + " and report_id = " + rptID + " and run_date = '" +
             runDate + "' order by rpt_run_id DESC";
            dtSt = Global.mnFrm.cmCde.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public static string get_RptRnOutpt(long rptRnID)
        {
            string strSql = "SELECT rpt_run_output " +
       "FROM rpt.rpt_report_runs WHERE rpt_run_id = " + rptRnID;

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return dtst.Tables[0].Rows[0][0].ToString();
            }
            return "";
        }

        public static DataSet get_Basic_RptRn(string searchWord, string searchIn,
          Int64 offset, int limit_size, long rptID)
        {
            string strSql = "";
            string whrcls = "";
            string extrWhrcls = "";
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[10]) == false)
            {
                extrWhrcls = " and (a.run_by = " + Global.myRpt.user_id + ")";
            }
            if (searchIn == "Report Run ID")
            {
                whrcls = " and (trim(to_char(a.rpt_run_id,'99999999999999999999999999999999999999999999')) ilike '" + searchWord.Replace("'", "''") +
          "')";
            }
            else if (searchIn == "Run By")
            {
                whrcls = " and ((select b.user_name from " +
                  "sec.sec_users b where b.user_id = a.run_by) ilike '" + searchWord.Replace("'", "''") +
          "')";
            }
            else if (searchIn == "Run Date")
            {
                whrcls = " and (to_char(to_timestamp(a.run_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') ilike '" + searchWord.Replace("'", "''") +
          "')";
            }

            strSql = "SELECT a.rpt_run_id, a.run_by, (select b.user_name from " +
                @"sec.sec_users b where b.user_id = a.run_by) usrnm, 
to_char(to_timestamp(a.run_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), " +
                @"a.run_status_txt, a.run_status_prct, a.rpt_rn_param_ids, 
        a.rpt_rn_param_vals, a.output_used, a.orntn_used, 
  CASE WHEN a.last_actv_date_tme='' or a.last_actv_date_tme IS NULL THEN '' 
ELSE to_char(to_timestamp(a.last_actv_date_tme,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') END last_time_active, 
CASE WHEN alert_id>0 THEN 'ALERT' WHEN is_this_from_schdler='1' THEN 'SCHEDULER' ELSE 'USER' END run_src, alert_id, msg_sent_id " +
            "FROM rpt.rpt_report_runs a " +
        "WHERE ((a.report_id = " + rptID + ")" + whrcls + extrWhrcls + ") ORDER BY a.rpt_run_id DESC LIMIT " + limit_size +
        " OFFSET " + (Math.Abs(offset * limit_size)).ToString();

            Global.mnFrm.rn_SQL = strSql;
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static long get_total_RptRn(string searchWord, string searchIn, long rptID)
        {
            string strSql = "";
            string whrcls = "";
            string extrWhrcls = "";
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[10]) == false)
            {
                extrWhrcls = " and (a.run_by = " + Global.myRpt.user_id + ")";
            }
            if (searchIn == "Report Run ID")
            {
                whrcls = " and (trim(to_char(a.rpt_run_id,'99999999999999999999999999999999999999999999')) ilike '" + searchWord.Replace("'", "''") +
          "')";
            }
            else if (searchIn == "Run By")
            {
                whrcls = " and ((select b.user_name from " +
                  "sec.sec_users b where b.user_id = a.run_by) ilike '" + searchWord.Replace("'", "''") +
          "')";
            }
            else if (searchIn == "Run Date")
            {
                whrcls = " and (to_char(to_timestamp(a.run_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') ilike '" + searchWord.Replace("'", "''") +
          "')";
            }
            strSql = "SELECT count(1) FROM rpt.rpt_report_runs a " +
      "WHERE ((a.report_id = " + rptID + ")" + whrcls + extrWhrcls + ")";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return Int64.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }
        #endregion
        #endregion

        #region "CUSTOM FUNCTIONS..."
        public static void createRqrdLOVs()
        {

            string[] sysLovs = { "Report Output Formats", "Report Orientations",
                           "Report/Process Runs", "Reports and Processes",
                           "Background Process Runners","Alert Types",
                           "Max Allowed Concurrent Connections","Reference Numbers for Letters" };
            string[] sysLovsDesc = { "Report Output Formats", "Report Orientations",
                               "Report/Process Runs", "Reports and Processes",
                               "Background Process Runners","Alert Types",
                               "Max Allowed Concurrent Connections","Reference Numbers for Letters" };
            string[] sysLovsDynQrys = { "", "",
        "select distinct trim(to_char(rpt_run_id,'999999999999999999999999999999')) a, (run_status_txt || '-' || run_by || '-' || run_date) b, '' c, report_id d, run_by e, rpt_run_id f from rpt.rpt_report_runs order by rpt_run_id DESC",
        "select distinct trim(to_char(report_id,'999999999999999999999999999999')) a, report_name b, '' c from rpt.rpt_reports order by report_name",
        "select distinct trim(to_char(prcss_rnnr_id,'999999999999999999999999999999')) a, rnnr_name b, '' c from rpt.rpt_prcss_rnnrs where rnnr_name != 'REQUESTS LISTENER PROGRAM' order by rnnr_name",
        "","",""};
            string[] pssblVals = {
        "0", "None", "None"
           ,"0", "MICROSOFT EXCEL", "MICROSOFT EXCEL"
       ,"0", "HTML", "HTML"
           ,"0", "STANDARD", "STANDARD"
       ,"0", "PDF", "PDF"
       ,"0", "MICROSOFT WORD", "MICROSOFT WORD"
       ,"0", "CHARACTER SEPARATED FILE (CSV)", "DELIMITER SEPARATED FILE (CSV)"
       ,"0", "COLUMN CHART", "COLUMN CHART"
       ,"0", "PIE CHART", "PIE CHART"
       ,"0", "LINE CHART", "LINE CHART"
       ,"1", "Portrait", "Portrait"
           ,"1", "Landscape", "Landscape"
       ,"5", "EMAIL", "EMAIL"
           ,"5", "SMS", "SMS"
       ,"6", "5", "After 5 db connections don't launch any new process runner"
       ,"7", "RHO/PF", "00001"                           };

            Global.mnFrm.cmCde.createSysLovs(sysLovs, sysLovsDynQrys, sysLovsDesc);
            Global.mnFrm.cmCde.createSysLovsPssblVals(sysLovs, pssblVals);

            long prgmID = Global.mnFrm.cmCde.getGnrlRecID("rpt.rpt_prcss_rnnrs", "rnnr_name", "prcss_rnnr_id", "REQUESTS LISTENER PROGRAM");
            if (prgmID <= 0)
            {
                Global.createPrcsRnnr("REQUESTS LISTENER PROGRAM",
                                      "This is the main Program responsible for making sure that " +
                                      "your reports and background processes are run by their respective " +
                                      "programs when a request is submitted for them to be run.",
                                      "2013-01-01 00:00:00", "Not Running", "3-Normal", @"\bin\REMSProcessRunner.exe");
            }
            else
            {
                Global.updatePrcsRnnrNm(prgmID, "REQUESTS LISTENER PROGRAM",
                                      "This is the main Program responsible for making sure that " +
                                      "your reports and background processes are run by their respective " +
                                      "programs when a request is submitted for them to be run.",
                               @"\bin\REMSProcessRunner.exe");
            }

            prgmID = Global.mnFrm.cmCde.getGnrlRecID("rpt.rpt_prcss_rnnrs", "rnnr_name", "prcss_rnnr_id", "Standard Process Runner");
            if (prgmID <= 0)
            {
                Global.createPrcsRnnr("Standard Process Runner",
                                      "This is a standard runner that can run almost all kinds of reports and processes in the background.",
                                      "2013-01-01 00:00:00", "Not Running", "3-Normal", @"\bin\REMSProcessRunner.exe");
            }
            else
            {
                Global.updatePrcsRnnrNm(prgmID, "Standard Process Runner",
                                 "This is a standard runner that can run almost all kinds of reports and processes in the background.",
                                 @"\bin\REMSProcessRunner.exe");
            }
            prgmID = Global.mnFrm.cmCde.getGnrlRecID("rpt.rpt_prcss_rnnrs", "rnnr_name", "prcss_rnnr_id", "Customised Process Runner");
            if (prgmID <= 0)
            {
                Global.createPrcsRnnr("Customised Process Runner",
                                      "Customised Process Runner",
                                      "2013-01-01 00:00:00", "Not Running", "3-Normal", @"\bin\REMSCustomRunner.exe");
            }
            else
            {
                Global.updatePrcsRnnrNm(prgmID, "Customised Process Runner",
                                 "Customised Process Runner",
                                 @"\bin\REMSCustomRunner.exe");
            }
            prgmID = Global.mnFrm.cmCde.getGnrlRecID("rpt.rpt_prcss_rnnrs", "rnnr_name", "prcss_rnnr_id", "Jasper Process Runner");
            if (prgmID <= 0)
            {
                Global.createPrcsRnnr("Jasper Process Runner",
                                      "This is a standard runner that can run almost all kinds of jasper reports and other processes in the background.",
                                      "2013-01-01 00:00:00", "Not Running", "3-Normal", @"\bin\REMSProcessRunner.jar");
            }
            else
            {
                Global.updatePrcsRnnrNm(prgmID, "Jasper Process Runner",
                                 "This is a standard runner that can run almost all kinds of jasper reports and other processes in the background.",
                                 @"\bin\REMSProcessRunner.jar");
            }
        }

        public static void refreshRqrdVrbls()
        {
            Global.mnFrm.cmCde.DefaultPrvldgs = Global.dfltPrvldgs;
            //Global.mnFrm.cmCde.Login_number = Global.myRpt.login_number;
            Global.mnFrm.cmCde.ModuleAdtTbl = Global.myRpt.full_audit_trail_tbl_name;
            Global.mnFrm.cmCde.ModuleDesc = Global.myRpt.mdl_description;
            Global.mnFrm.cmCde.ModuleName = Global.myRpt.name;
            //Global.mnFrm.cmCde.pgSqlConn = Global.myRpt.Host.globalSQLConn;
            //Global.mnFrm.cmCde.Role_Set_IDs = Global.myRpt.role_set_id;
            Global.mnFrm.cmCde.SampleRole = "Reports And Processes Administrator";
            //Global.mnFrm.cmCde.User_id = Global.myRpt.user_id;
            //Global.mnFrm.cmCde.Org_id = Global.myRpt.org_id;
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            Global.myRpt.user_id = Global.mnFrm.usr_id;
            Global.myRpt.login_number = Global.mnFrm.lgn_num;
            Global.myRpt.role_set_id = Global.mnFrm.role_st_id;
            Global.myRpt.org_id = Global.mnFrm.Og_id;

        }
        #endregion
    }
}
