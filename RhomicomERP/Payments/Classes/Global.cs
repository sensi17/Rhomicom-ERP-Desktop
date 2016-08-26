using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using InternalPayments.Forms;
using System.Windows.Forms;
using CommonCode;

namespace InternalPayments.Classes
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
    public static InternalPayments myPay = new InternalPayments();
    public static mainForm mnFrm = null;

    public static string[] dfltPrvldgs = { "View Internal Payments", 
		/*1*/"View Manual Payments","View Pay Item Sets","View Person Sets",
		/*4*/"View Mass Pay Runs","View Payment Transactions","View GL Interface Table",
		/*7*/"View Record History", "View SQL",
    /*9*/"Add Manual Payments","Reverse Manual Payments",
    /*11*/"Add Pay Item Sets","Edit Pay Item Sets","Delete Pay Item Sets",
    /*14*/"Add Person Sets","Edit Person Sets","Delete Person Sets",
    /*17*/"Add Mass Pay","Edit Mass Pay","Delete Mass Pay", "Send Mass Pay Transactions to Actual GL",
    /*21*/"Send All Transactions to Actual GL", "Run Mass Pay",
    /*23*/"Rollback Mass Pay Run","Send Selected Transactions to Actual GL",
    /*25*/"View Pay Items", "Add Pay Items","Edit Pay Items","Delete Pay Items",
    /*29*/"View Person Pay Item Assignments", "View Banks", "Add Pay Item Assignments", 
    /*32*/"Edit Pay Item Assignments", "Delete Pay Item Assignments",
    /*34*/"Add Pay Item Assignments", "Edit Pay Item Assignments", "Delete Pay Item Assignments",
    /*37*/"View Global Values", "Add Global Values","Edit Global Values","Delete Global Values",
    /*41*/"View other User's Mass Pays"                                         };
    public static string currentPanel = "";

    #endregion

    #region "DATA MANIPULATION FUNCTIONS..."
    #region "INSERT STATEMENTS..."
    public static void createMsPy(int orgid, string mspyname,
    string mspydesc, string trnsdte, int prstid, int itmstid, string glDate)
    {
      trnsdte = DateTime.ParseExact(
   trnsdte, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

      glDate = DateTime.ParseExact(
   glDate, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = "INSERT INTO pay.pay_mass_pay_run_hdr(" +
            "mass_pay_name, mass_pay_desc, created_by, creation_date, " +
            "last_update_by, last_update_date, run_status, mass_pay_trns_date, " +
            "prs_st_id, itm_st_id, org_id, sent_to_gl, gl_date) " +
            "VALUES ('" + mspyname.Replace("'", "''") +
            "', '" + mspydesc.Replace("'", "''") +
            "', " + Global.myPay.user_id + ", '" + dateStr +
            "', " + Global.myPay.user_id + ", '" + dateStr +
            "', '0', '" + trnsdte.Replace("'", "''") + "', " +
            prstid + ", " + itmstid + ", " + orgid + ", '0', '" + glDate +
            "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void createPrsSt(int orgid, string prssetname,
    string prsstdesc, bool isenbled, string sqlQry, bool isdflt, bool usesSQL)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = "INSERT INTO pay.pay_prsn_sets_hdr(" +
            "prsn_set_hdr_name, prsn_set_hdr_desc, is_enabled, " +
            "created_by, creation_date, last_update_by, last_update_date, " +
            "sql_query, org_id, is_default, uses_sql) " +
            "VALUES ('" + prssetname.Replace("'", "''") +
            "', '" + prsstdesc.Replace("'", "''") +
            "', '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(isenbled) +
            "', " + Global.myPay.user_id + ", '" + dateStr +
            "', " + Global.myPay.user_id + ", '" + dateStr +
            "', '" + sqlQry.Replace("'", "''") + "', " + orgid +
            ", '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(isdflt) +
            "', '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(usesSQL) +
            "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void createPrsStDet(int hdrID, long prsID)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = "INSERT INTO pay.pay_prsn_sets_det(" +
            "prsn_set_hdr_id, person_id, created_by, creation_date, " +
            "last_update_by, last_update_date) " +
            "VALUES (" + hdrID +
            ", " + prsID +
            ", " + Global.myPay.user_id + ", '" + dateStr +
            "', " + Global.myPay.user_id + ", '" + dateStr +
            "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void createItmStDet(int hdrID, int itmID,
    string trnsTyp)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = "INSERT INTO pay.pay_itm_sets_det(" +
            "hdr_id, item_id, to_do_trnsctn_type, created_by, creation_date, " +
            "last_update_by, last_update_date) " +
            "VALUES (" + hdrID +
            ", " + itmID +
            ", '" + trnsTyp.Replace("'", "''") +
            "', " + Global.myPay.user_id + ", '" + dateStr +
            "', " + Global.myPay.user_id + ", '" + dateStr +
            "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void createItmSt(int orgid, string itmsetname,
    string itmstdesc, bool isenbled, bool isdflt, bool usesSQL, string sqlTxt)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = "INSERT INTO pay.pay_itm_sets_hdr(" +
            "itm_set_name, itm_set_desc, is_enabled, created_by, creation_date, " +
            "last_update_by, last_update_date, org_id, is_default, uses_sql, sql_query) " +
            "VALUES ('" + itmsetname.Replace("'", "''") +
            "', '" + itmstdesc.Replace("'", "''") +
            "', '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(isenbled) +
            "', " + Global.myPay.user_id + ", '" + dateStr +
            "', " + Global.myPay.user_id + ", '" + dateStr +
            "', " + orgid + ", '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(isdflt) +
            "', '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(usesSQL) +
            "', '" + sqlTxt.Replace("'", "''") +
            "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void createItmBals(long blsitmid, double netbals,
    long prsn_id,
    string balsDate, long py_trns_id)
    {
      balsDate = DateTime.ParseExact(
   balsDate, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      if (balsDate.Length > 10)
      {
        balsDate = balsDate.Substring(0, 10);
      }
      string src_trns = ",";
      if (py_trns_id > 0)
      {
        src_trns = "," + py_trns_id + ",";
      }
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = "INSERT INTO pay.pay_balsitm_bals(" +
            "bals_itm_id, bals_amount, person_id, bals_date, created_by, " +
            "creation_date, last_update_by, last_update_date, source_trns_ids) " +
        "VALUES (" + blsitmid +
        ", " + netbals + ", " + prsn_id + ", '" + balsDate + "', " +
        Global.myPay.user_id + ", '" + dateStr +
                        "', " + Global.myPay.user_id + ", '" + dateStr + "', '" + src_trns.Replace("'", "''") + "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void createItmBalsRetro(long blsitmid, double netbals,
   long prsn_id,
   string balsDate, long py_trns_id)
    {
      balsDate = DateTime.ParseExact(
   balsDate, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      if (balsDate.Length > 10)
      {
        balsDate = balsDate.Substring(0, 10);
      }
      string src_trns = ",";
      if (py_trns_id > 0)
      {
        src_trns = "," + py_trns_id + ",";
      }
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = "INSERT INTO pay.pay_balsitm_bals_retro(" +
            "bals_itm_id, bals_amount, person_id, bals_date, created_by, " +
            "creation_date, last_update_by, last_update_date, source_trns_ids) " +
        "VALUES (" + blsitmid +
        ", " + netbals + ", " + prsn_id + ", '" + balsDate + "', " +
        Global.myPay.user_id + ", '" + dateStr +
                        "', " + Global.myPay.user_id + ", '" + dateStr + "', '" + src_trns.Replace("'", "''") + "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }


    public static void createPaymntLine(long prsnid, long itmid, double amnt, string paydate,
    string paysource, string trnsType, long msspyid, string paydesc, int crncyid, string dateStr,
      string pymt_vldty, long src_trns_id, string glDate, string dteErnd)
    {
      paydate = DateTime.ParseExact(
   paydate, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      dateStr = DateTime.ParseExact(
   dateStr, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      if (dteErnd == "")
      {
        dteErnd = paydate;
      }
      string insSQL = "INSERT INTO pay.pay_itm_trnsctns(" +
               "person_id, item_id, amount_paid, paymnt_date, paymnt_source, " +
               "pay_trns_type, created_by, creation_date, last_update_by, last_update_date, " +
               "mass_pay_id, pymnt_desc, crncy_id, pymnt_vldty_status, src_py_trns_id, gl_date, date_earned) " +
       "VALUES (" + prsnid + ", " + itmid + ", " + amnt +
       ", '" + paydate.Replace("'", "''") + "', '" + paysource.Replace("'", "''") +
       "', '" + trnsType.Replace("'", "''") + "', " + Global.myPay.user_id + ", '" + dateStr + "', " +
               Global.myPay.user_id + ", '" + dateStr + "', " + msspyid +
               ", '" + paydesc.Replace("'", "''") + "', " + crncyid +
               ", '" + pymt_vldty.Replace("'", "''") + "', " + src_trns_id +
               ", '" + glDate + "', '" + dteErnd + "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void createPaymntLineRetro(long prsnid, long itmid, double amnt, string paydate,
   string paysource, string trnsType, long msspyid, string paydesc, int crncyid, string dateStr,
     string pymt_vldty, long src_trns_id, string glDate)
    {
      paydate = DateTime.ParseExact(
   paydate, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      dateStr = DateTime.ParseExact(
   dateStr, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      string insSQL = "INSERT INTO pay.pay_itm_trnsctns_retro(" +
               "person_id, item_id, amount_paid, paymnt_date, paymnt_source, " +
               "pay_trns_type, created_by, creation_date, last_update_by, last_update_date, " +
               "mass_pay_id, pymnt_desc, crncy_id, pymnt_vldty_status, src_py_trns_id, gl_date) " +
       "VALUES (" + prsnid + ", " + itmid + ", " + amnt +
       ", '" + paydate.Replace("'", "''") + "', '" + paysource.Replace("'", "''") +
       "', '" + trnsType.Replace("'", "''") + "', " + Global.myPay.user_id + ", '" + dateStr + "', " +
               Global.myPay.user_id + ", '" + dateStr + "', " + msspyid +
               ", '" + paydesc.Replace("'", "''") + "', " + crncyid +
               ", '" + pymt_vldty.Replace("'", "''") + "', " + src_trns_id + ", '" + glDate + "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void createTodaysGLBatch(int orgid, string batchnm,
    string batchdesc, string batchsource)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = "INSERT INTO accb.accb_trnsctn_batches(" +
               "batch_name, batch_description, created_by, creation_date, " +
               "org_id, batch_status, last_update_by, last_update_date, batch_source, avlbl_for_postng) " +
       "VALUES ('" + batchnm.Replace("'", "''") + "', '" + batchdesc.Replace("'", "''") +
       "', " + Global.myPay.user_id + ", '" + dateStr + "', " + orgid + ", '0', " +
               Global.myPay.user_id + ", '" + dateStr + "', '" +
               batchsource.Replace("'", "''") + "', '0')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void updtTodaysGLBatchPstngAvlblty(long batchid, string avlblty)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string insSQL = "UPDATE accb.accb_trnsctn_batches SET avlbl_for_postng='" + avlblty +
        "', last_update_by=" + Global.myPay.user_id +
        ", last_update_date='" + dateStr +
        "' WHERE batch_id = " + batchid;
      Global.mnFrm.cmCde.updateDataNoParams(insSQL);
    }

    public static double get_Batch_DbtSum(long batchID)
    {
      string strSql = "";
      double sumRes = 0.00;
      strSql = "SELECT SUM(a.dbt_amount)" +
    "FROM accb.accb_trnsctn_details a " +
    "WHERE(a.batch_id = " + batchID + ")";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
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

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      double sumRes = 0.00;
      if (dtst.Tables[0].Rows.Count > 0)
      {
        double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out sumRes);
      }
      return Math.Round(sumRes, 2);
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
               ", '" + trnsdte.Replace("'", "''") + "', " + crncyid + ", " + Global.myPay.user_id +
               ", '" + dateStr + "', " + crdtamnt + ", " +
               Global.myPay.user_id + ", '" + dateStr + "', " + netamnt +
               ", -1, '" + srcDocTyp.Replace("'", "''") + "', " +
               srcDocID + ", " + srcDocLnID + ", '" + trnsSrc + "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void deleteBrknDocGLInfcLns()
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string delSQL = @"DELETE FROM pay.pay_gl_interface 
WHERE scm.get_src_doc_num(src_doc_id,src_doc_typ) IS NULL 
or scm.get_src_doc_num(src_doc_id, src_doc_typ)=''";
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
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
               ", '" + trnsdte.Replace("'", "''") + "', " + crncyid + ", " + Global.myPay.user_id +
               ", '" + dateStr + "', " + crdtamnt + ", " +
               Global.myPay.user_id + ", '" + dateStr + "', " + netamnt +
               ", -1, '" + trnsSrc + "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static long getIntrfcTrnsID(string intrfcTblNm, int accntID, double netAmnt, string trnsDte)
    {
      string selSQL = @"SELECT interface_id 
  FROM " + intrfcTblNm + " WHERE accnt_id=" + accntID + " and net_amount=" + netAmnt +
         " and trnsctn_date = '" + trnsDte + "'";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
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
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);

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
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      //Global.mnFrm.trnsDet_SQL = strSql;
      return dtst;
    }

    public static void deleteBatch(long batchid, string batchNm)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Batch Name = " + batchNm;
      string delSql = "DELETE FROM accb.accb_trnsctn_batches WHERE(batch_id = " + batchid + ")";
      Global.mnFrm.cmCde.deleteDataNoParams(delSql);
    }

    public static void deleteBatchTrns(long batchid)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string delSql = "DELETE FROM accb.accb_trnsctn_details WHERE(batch_id = " + batchid + ")";
      Global.mnFrm.cmCde.deleteDataNoParams(delSql);
    }

    public static double get_LtstExchRate(int fromCurrID, int toCurrID, string asAtDte)
    {
      int fnccurid = Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id);
      //this.curCode = Global.mnFrm.cmCde.getPssblValNm(this.curid);

      string strSql = "";
      strSql = @"SELECT CASE WHEN a.currency_from_id=" + fromCurrID +
        @" THEN a.multiply_from_by ELSE (1/a.multiply_from_by) END
      FROM accb.accb_exchange_rates a WHERE ((a.currency_from_id=" + fromCurrID +
        @" and a.currency_to_id=" + toCurrID +
        @") or (a.currency_to_id=" + fromCurrID +
        @" and a.currency_from_id=" + toCurrID +
        @")) and to_timestamp(a.conversion_date,'YYYY-MM-DD') <= to_timestamp('" + asAtDte +
        "','DD-Mon-YYYY HH24:MI:SS') ORDER BY to_timestamp(a.conversion_date,'YYYY-MM-DD') DESC LIMIT 1 OFFSET 0";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
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
        double a = Global.get_LtstExchRate(fromCurrID, fnccurid, asAtDte);
        double b = Global.get_LtstExchRate(toCurrID, fnccurid, asAtDte);
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
               ", '" + trnsdte.Replace("'", "''") + "', " + crncyid + ", " + Global.myPay.user_id +
               ", '" + dateStr + "', " + batchid + ", " + crdtamnt + ", " +
               Global.myPay.user_id + ", '" + dateStr + "', " + netamnt +
               ", '0', '" + srcids + "', " + entrdAmt +
                        ", " + entrdCurrID + ", " + acntAmnt +
                        ", " + acntCurrID + ", " + funcExchRate +
                        ", " + acntExchRate + ", '" + dbtOrCrdt + "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void createPymntGLIntFcLn(int accntid, string trnsdesc, double dbtamnt,
  string trnsdte, int crncyid, double crdtamnt, double netamnt, long srcid, string dateStr)
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
      string insSQL = "INSERT INTO pay.pay_gl_interface(" +
            "accnt_id, transaction_desc, dbt_amount, trnsctn_date, " +
            "func_cur_id, created_by, creation_date, crdt_amount, last_update_by, " +
            "last_update_date, net_amount, source_trns_id) " +
               "VALUES (" + accntid + ", '" + trnsdesc.Replace("'", "''") + "', " + dbtamnt +
               ", '" + trnsdte.Replace("'", "''") + "', " + crncyid + ", " + Global.myPay.user_id +
               ", '" + dateStr + "', " + crdtamnt + ", " +
               Global.myPay.user_id + ", '" + dateStr + "', " + netamnt +
               ", " + srcid + ")";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void createPymntGLIntFcLn(int accntid, string trnsdesc, double dbtamnt,
  string trnsdte, int crncyid, double crdtamnt, double netamnt, long srcid, string dateStr, string trnsSrc)
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
      string insSQL = "INSERT INTO pay.pay_gl_interface(" +
            "accnt_id, transaction_desc, dbt_amount, trnsctn_date, " +
            "func_cur_id, created_by, creation_date, crdt_amount, last_update_by, " +
            "last_update_date, net_amount, gl_batch_id, source_trns_id, trns_source) " +
               "VALUES (" + accntid + ", '" + trnsdesc.Replace("'", "''") + "', " + dbtamnt +
               ", '" + trnsdte.Replace("'", "''") + "', " + crncyid + ", " + Global.myPay.user_id +
               ", '" + dateStr + "', " + crdtamnt + ", " +
               Global.myPay.user_id + ", '" + dateStr + "', " + netamnt +
               ", -1, " + srcid + ", '" + trnsSrc + "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void createMoneyBal(long prsnid, double ttlpay, double ttlwthdrwl)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = "INSERT INTO pay.pay_prsn_money_bals(" +
      "person_id, total_payments, created_by, creation_date, " +
      "last_update_by, last_update_date, total_withdrawals) " +
          "VALUES (" + prsnid + ", " + ttlpay + ", " + Global.myPay.user_id +
          ", '" + dateStr + "', " + Global.myPay.user_id + ", '" +
          dateStr + "', " + ttlwthdrwl + ")";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }
    #endregion

    #region "UPDATE STATEMENTS..."
    public static void updateMsPyStatus(long mspyid, string run_cmpltd, string to_gl_intfc)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE pay.pay_mass_pay_run_hdr " +
      "SET run_status='" + run_cmpltd.Replace("'", "''") +
      "', sent_to_gl='" + to_gl_intfc.Replace("'", "''") +
      "', last_update_by=" + Global.myPay.user_id +
      ", last_update_date='" + dateStr +
      "' WHERE mass_pay_id = " + mspyid;
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void updateMsPy(long mspyid, string mspyname,
  string mspydesc, string trnsdte, int prstid, int itmstid, string glDate)
    {
      trnsdte = DateTime.ParseExact(
   trnsdte, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

      glDate = DateTime.ParseExact(
   glDate, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE pay.pay_mass_pay_run_hdr " +
      "SET mass_pay_name='" + mspyname.Replace("'", "''") +
      "', mass_pay_desc='" + mspydesc.Replace("'", "''") +
      "', mass_pay_trns_date = '" + trnsdte.Replace("'", "''") +
      "', gl_date = '" + glDate.Replace("'", "''") +
      "', last_update_by=" + Global.myPay.user_id +
      ", last_update_date='" + dateStr +
      "', prs_st_id = " + prstid + ", itm_st_id = " + itmstid +
      " WHERE mass_pay_id = " + mspyid;
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void updatePrsSt(long hdrid, string prssetname,
  string prsstdesc, bool isenbled, string sqlQry, bool isdflt, bool usesSQL)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE pay.pay_prsn_sets_hdr " +
      "SET prsn_set_hdr_name='" + prssetname.Replace("'", "''") +
      "', prsn_set_hdr_desc='" + prsstdesc.Replace("'", "''") +
      "', is_enabled = '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(isenbled) +
      "', sql_query = '" + sqlQry.Replace("'", "''") +
      "', is_default = '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(isdflt) +
      "', uses_sql = '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(usesSQL) +
      "', last_update_by=" + Global.myPay.user_id +
      ", last_update_date='" + dateStr +
      "' WHERE prsn_set_hdr_id = " + hdrid;
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void updateItmSt(long hdrid, string itmsetname,
  string itmstdesc, bool isenbled, bool isdflt, bool usesSQL, string sqlTxt)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE pay.pay_itm_sets_hdr " +
      "SET itm_set_name='" + itmsetname.Replace("'", "''") +
      "', itm_set_desc='" + itmstdesc.Replace("'", "''") +
      "', is_enabled = '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(isenbled) +
      "', is_default = '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(isdflt) +
      "', last_update_by=" + Global.myPay.user_id +
      ", last_update_date='" + dateStr +
      "', uses_sql = '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(usesSQL) +
      "', sql_query = '" + sqlTxt.Replace("'", "''") +
      "' WHERE hdr_id = " + hdrid;
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    //public static void undfltAllItmSt(int orgID)
    //{
    //  Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
    //  string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
    //  string updtSQL = "UPDATE pay.pay_itm_sets_hdr " +
    //  "SET is_default = '0', last_update_by=" + Global.myPay.user_id +
    //  ", last_update_date='" + dateStr +
    //  "' WHERE is_default='1' and org_id = " + orgID;
    //  Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    //}

    //public static void undfltAllPrsSt(int orgID)
    //{
    //  Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
    //  string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
    //  string updtSQL = "UPDATE pay.pay_prsn_sets_hdr " +
    //  "SET is_default = '0', last_update_by=" + Global.myPay.user_id +
    //  ", last_update_date='" + dateStr +
    //  "' WHERE is_default='1' and org_id = " + orgID;
    //  Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    //}

    public static void updtItmDailyBalsCum(string balsDate, long blsItmID,
  long prsn_id, double netAmnt, long py_trns_id)
    {
      balsDate = DateTime.ParseExact(
   balsDate, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      balsDate = balsDate.Substring(0, 10);
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE pay.pay_balsitm_bals " +
      "SET last_update_by = " + Global.myPay.user_id +
      ", last_update_date = '" + dateStr +
      "', bals_amount = bals_amount +" + netAmnt +
      ", source_trns_ids = source_trns_ids || '" + py_trns_id +
    ",' WHERE (to_timestamp(bals_date,'YYYY-MM-DD') >= to_timestamp('" + balsDate +
    "','YYYY-MM-DD') and bals_itm_id = " + blsItmID + " and person_id = " + prsn_id + ")";
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void updtItmDailyBalsNonCum(string balsDate, long blsItmID,
  long prsn_id, double netAmnt, long py_trns_id)
    {
      balsDate = DateTime.ParseExact(
   balsDate, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      balsDate = balsDate.Substring(0, 10);
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE pay.pay_balsitm_bals " +
      "SET last_update_by = " + Global.myPay.user_id +
      ", last_update_date = '" + dateStr +
      "', bals_amount = bals_amount +" + netAmnt +
      ", source_trns_ids = source_trns_ids || '" + py_trns_id +
      ",' WHERE (to_timestamp(bals_date,'YYYY-MM-DD') = to_timestamp('" + balsDate +
      "','YYYY-MM-DD') and bals_itm_id = " + blsItmID + " and person_id = " + prsn_id + ")";
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void updtItmDailyBalsCumRetro(string balsDate, long blsItmID,
 long prsn_id, double netAmnt, long py_trns_id)
    {
      balsDate = DateTime.ParseExact(
   balsDate, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      balsDate = balsDate.Substring(0, 10);
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE pay.pay_balsitm_bals_retro " +
      "SET last_update_by = " + Global.myPay.user_id +
      ", last_update_date = '" + dateStr +
      "', bals_amount = bals_amount +" + netAmnt +
      ", source_trns_ids = source_trns_ids || '" + py_trns_id +
    ",' WHERE (to_timestamp(bals_date,'YYYY-MM-DD') >= to_timestamp('" + balsDate +
    "','YYYY-MM-DD') and bals_itm_id = " + blsItmID + " and person_id = " + prsn_id + ")";
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void updtItmDailyBalsNonCumRetro(string balsDate, long blsItmID,
  long prsn_id, double netAmnt, long py_trns_id)
    {
      balsDate = DateTime.ParseExact(
   balsDate, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      balsDate = balsDate.Substring(0, 10);
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE pay.pay_balsitm_bals_retro " +
      "SET last_update_by = " + Global.myPay.user_id +
      ", last_update_date = '" + dateStr +
      "', bals_amount = bals_amount +" + netAmnt +
      ", source_trns_ids = source_trns_ids || '" + py_trns_id +
      ",' WHERE (to_timestamp(bals_date,'YYYY-MM-DD') = to_timestamp('" + balsDate +
      "','YYYY-MM-DD') and bals_itm_id = " + blsItmID + " and person_id = " + prsn_id + ")";
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }
    //public static void updtMoneyBals(long moneybalid, double ttlpay, double ttlwthdrwl)
    //{
    //  string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
    //  string updtSQL = "UPDATE pay.pay_prsn_money_bals " +
    //                                      "SET total_payments=total_payments + " + ttlpay +
    //                                      ", total_withdrawals = total_withdrawals + " + ttlwthdrwl +
    //  ", last_update_by=" + Global.myPay.user_id + ", " +
    //  "last_update_date='" + dateStr + "' " +
    //                                      "WHERE money_bals_id = " + moneybalid;
    //  Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    //}

    public static void updtGLIntrfcLnSpclOrg(int orgID)
    {
      //Used to update batch ids of interface lines that have gone to GL already
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE pay.pay_gl_interface a " +
      "SET gl_batch_id = (select f.batch_id from accb.accb_trnsctn_details f, accb.accb_chart_of_accnts h " +
      "where f.batch_id IN (select g.batch_id from accb.accb_trnsctn_batches g " +
      "where g.batch_name ilike '%Internal Payments%' and " +
      "to_timestamp(g.creation_date,'YYYY-MM-DD HH24:MI:SS') between " +
      "(to_timestamp(a.trnsctn_date,'YYYY-MM-DD HH24:MI:SS') - interval '6 months') " +
      "and (to_timestamp(a.trnsctn_date,'YYYY-MM-DD HH24:MI:SS') + interval '6 months')) and " +
      "f.source_trns_ids like '%,' || a.interface_id || ',%' and " +
      "f.trnsctn_date=a.trnsctn_date and f.accnt_id= a.accnt_id and f.accnt_id= h.accnt_id and h.org_id = " + orgID + ")" +
      ", last_update_by=" + Global.myPay.user_id + ", " +
      "last_update_date='" + dateStr + "' " +
      "WHERE a.gl_batch_id = -1 and EXISTS(select 1 from accb.accb_chart_of_accnts" +
      " m where a.accnt_id= m.accnt_id and m.org_id =" + orgID + ")";
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void updtPymntAllGLIntrfcLnOrg(long glbatchid, int orgID)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE pay.pay_gl_interface a " +
      "SET gl_batch_id = " + glbatchid +
      ", last_update_by=" + Global.myPay.user_id + ", " +
      "last_update_date='" + dateStr + "' " +
      "WHERE a.gl_batch_id = -1 and EXISTS(select f.transctn_id from accb.accb_trnsctn_details f, accb.accb_chart_of_accnts g " +
      "where f.batch_id = " + glbatchid + " " +
      "and f.source_trns_ids like '%,' || a.interface_id || ',%' and " +
      "f.trnsctn_date=a.trnsctn_date and f.accnt_id= a.accnt_id and f.accnt_id= g.accnt_id and g.org_id = " + orgID + ") ";
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void updtPymntMsPyGLIntrfcLn(long mspyid, long glbatchid)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE pay.pay_gl_interface a " +
     "SET gl_batch_id = " + glbatchid +
      ", last_update_by=" + Global.myPay.user_id + ", " +
      "last_update_date='" + dateStr + "' " +
      "WHERE a.gl_batch_id = -1 and a.source_trns_id IN " +
      "(select b.pay_trns_id from pay.pay_itm_trnsctns b where b.mass_pay_id = " +
      mspyid + ") and EXISTS(select f.transctn_id from accb.accb_trnsctn_details f " +
      "where f.batch_id = " + glbatchid + " " +
      "and f.source_trns_ids like '%,' || a.interface_id || ',%' and " +
      "f.trnsctn_date=a.trnsctn_date and f.accnt_id= a.accnt_id) ";
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void updtPymntMnlGLIntrfcLn(long py_trns_id, long glbatchid)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE pay.pay_gl_interface a " +
      "SET gl_batch_id = " + glbatchid +
      ", last_update_by=" + Global.myPay.user_id + ", " +
      "last_update_date='" + dateStr + "' " +
      "WHERE a.gl_batch_id = -1 and a.source_trns_id IN " +
      "(select pay_trns_id from pay.pay_itm_trnsctns  where pay_trns_id = " +
      py_trns_id + ") and EXISTS(select f.transctn_id from accb.accb_trnsctn_details f " +
      "where f.batch_id = " + glbatchid + " " +
      "and f.source_trns_ids like '%,' || a.interface_id || ',%' and " +
      "f.trnsctn_date=a.trnsctn_date and f.accnt_id= a.accnt_id) ";
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }


    //  public static void updatePrsnItmBals(long prsnid, long itmid, double amnt,
    //string trnsType)
    //  {
    //    Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
    //    string ttlamnt = "";
    //    string lastamnt = "";
    //    if (trnsType == "Payment by Organisation")
    //    {
    //      ttlamnt = "ttl_amnt_given_prsn=ttl_amnt_given_prsn + " + amnt + ", ";
    //      lastamnt = "lst_amnt_given_prsn=" + amnt + ", ";
    //    }
    //    else if (trnsType == "Payment by Person")
    //    {
    //      ttlamnt = "ttl_amnt_prsn_hs_paid=ttl_amnt_prsn_hs_paid + " + amnt + ", ";
    //      lastamnt = "lst_amnt_prsn_paid=" + amnt + ", ";
    //    }
    //    else if (trnsType == "Withdrawal by Person")
    //    {
    //      ttlamnt = "ttl_amnt_wthdrwn=ttl_amnt_wthdrwn + " + amnt + ", ";
    //      lastamnt = "lst_amnt_wthdrwn=" + amnt + ", ";
    //    }

    //    string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
    //    string updtSQL = "UPDATE pasn.prsn_bnfts_cntrbtns " +
    //    "SET " + ttlamnt + "" + lastamnt + "" +
    //    "last_update_by=" + Global.myPay.user_id + ", " +
    //    "last_update_date='" + dateStr + "' " +
    //    "WHERE person_id=" + prsnid + " and item_id=" + itmid;
    //    Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    //  }

    #endregion

    #region "DELETE STATEMENTS..."
    public static void deletePrsStDet(long detid)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string sqlStr = "DELETE FROM pay.pay_prsn_sets_det WHERE(prsn_set_det_id = " + detid + ")";
      Global.mnFrm.cmCde.deleteDataNoParams(sqlStr);
    }

    public static void deleteItmStDet(int detid)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string sqlStr = "DELETE FROM pay.pay_itm_sets_det WHERE(det_id = " + detid + ")";
      Global.mnFrm.cmCde.deleteDataNoParams(sqlStr);
    }

    public static bool isItmStInUse(int itmstID)
    {
      string strSql = "SELECT a.mass_pay_id " +
       "FROM pay.pay_mass_pay_run_hdr a " +
       "WHERE(a.itm_st_id = " + itmstID + ") LIMIT 1";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return true;
      }
      return false;
    }

    public static bool isPrsStInUse(int prsstID)
    {
      string strSql = "SELECT a.mass_pay_id " +
       "FROM pay.pay_mass_pay_run_hdr a " +
       "WHERE(a.prs_st_id = " + prsstID + ") LIMIT 1";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return true;
      }
      return false;
    }

    public static bool isMspyInUse(long mspyID)
    {
      string strSql = "SELECT a.mass_pay_id " +
       "FROM pay.pay_itm_trnsctns a, pay.pay_gl_interface b " +
       "WHERE a.mass_pay_id = " + mspyID +
       @" and b.source_trns_id = a.pay_trns_id and (b.gl_batch_id > 0 or 
 (a.pymnt_vldty_status = 'VALID' and a.src_py_trns_id <= 0)) LIMIT 1";
      /* or a.pymnt_vldty_status = 'VOID'*/
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return true;
      }
      return false;
    }

    public static bool isMnlpyInUse(long pyID)
    {
      string strSql = "SELECT a.pay_trns_id " +
       "FROM pay.pay_itm_trnsctns a, pay.pay_gl_interface b " +
       "WHERE a.pay_trns_id = " + pyID +
       @" and b.source_trns_id = a.pay_trns_id and (b.gl_batch_id > 0 or 
 (a.pymnt_vldty_status = 'VALID' and a.src_py_trns_id <= 0)) LIMIT 1";
      /* or a.pymnt_vldty_status = 'VOID'*/
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return true;
      }
      return false;
    }

    public static long getPymntRvrsal(long pyID)
    {
      string strSql = "SELECT a.pay_trns_id " +
       "FROM pay.pay_itm_trnsctns a " +
       "WHERE (a.src_py_trns_id = " + pyID +
       @" and a.pymnt_vldty_status = 'VALID')";
      /* or a.pymnt_vldty_status = 'VOID'*/
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return -1;
    }

    public static bool isItmInUse(int itmID)
    {
      /*= "SELECT a.row_id " +
       "FROM pasn.prsn_bnfts_cntrbtns a " +
       "WHERE(a.item_id = " + itmID + ")";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return true;
      }
      strSql*/
      string strSql = "SELECT a.pay_trns_id " +
       "FROM pay.pay_itm_trnsctns a " +
       "WHERE(a.item_id = " + itmID + ") ORDER BY 1 LIMIT 1 OFFSET 0";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return true;
      }
      //strSql = "SELECT a.pssbl_value_id " +
      // "FROM org.org_pay_items_values a " +
      // "WHERE(a.item_id = " + itmID + ")";
      //dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      //if (dtst.Tables[0].Rows.Count > 0)
      //{
      //  return true;
      //}
      //strSql = "SELECT a.feed_id " +
      // "FROM org.org_pay_itm_feeds a " +
      // "WHERE(a.fed_by_itm_id = " + itmID + " or a.balance_item_id = " + itmID + ")";
      //dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      //if (dtst.Tables[0].Rows.Count > 0)
      //{
      //  return true;
      //}
      strSql = "SELECT a.bals_id " +
       "FROM pay.pay_balsitm_bals a " +
       "WHERE(a.bals_itm_id = " + itmID + ") ORDER BY 1 LIMIT 1 OFFSET 0";
      dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return true;
      }

      return false;
    }

    public static bool isItmValInUse(int valID)
    {
      string strSql = "SELECT a.row_id " +
       "FROM pasn.prsn_bnfts_cntrbtns a " +
       "WHERE(a.item_pssbl_value_id = " + valID + ")";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return true;
      }
      return false;
    }

    public static bool isPrsnItmInUse(int itmID, long prsnID)
    {
      string strSql = "SELECT a.pay_trns_id " +
       "FROM pay.pay_itm_trnsctns a " +
       "WHERE(a.item_id = " + itmID + " and a.person_id = " + prsnID + ")";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return true;
      }
      return false;
    }

    public static void deletePayItm(long itmid, string itmNm)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Pay Item Name = " + itmNm;
      string delSQL = "DELETE FROM org.org_pay_items_values WHERE item_id = " + itmid;
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);

      delSQL = "DELETE FROM org.org_pay_itm_feeds a " +
 "WHERE(a.fed_by_itm_id = " + itmid + " or a.balance_item_id = " + itmid + ")";
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);

      delSQL = "DELETE FROM pasn.prsn_bnfts_cntrbtns a " +
       "WHERE(a.item_id = " + itmid + ")";
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);

      delSQL = "DELETE FROM org.org_pay_items WHERE item_id = " + itmid;
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }

    public static void deleteAllItmVals(long itmID, string itmNm)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Item Name = " + itmNm;
      string delSQL = "DELETE FROM org.org_pay_items_values WHERE item_id = " + itmID;
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }

    public static void deleteItmVals(long row_id, string pssblNm)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Possible Value Name = " + pssblNm;
      string delSQL = "DELETE FROM org.org_pay_items_values WHERE pssbl_value_id = " + row_id;
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }

    public static void deleteItmFeeds(long row_id, string itmNm)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Item Name = " + itmNm;
      string delSQL = "DELETE FROM org.org_pay_itm_feeds WHERE feed_id = " + row_id;
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }
    #endregion

    #region "SELECT STATEMENTS..."
    #region "PAY ITEMS..."
    public static bool doesPrsnHvItm(long prsnID, long itmID, string dateStr, ref string strtDte)
    {
      dateStr = DateTime.ParseExact(
   dateStr, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      string strSql = @"Select a.row_id, to_char(to_timestamp(valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS')
      FROM pasn.prsn_bnfts_cntrbtns a where((a.person_id = " +
    prsnID + ") and (a.item_id = " + itmID + ") and (to_timestamp('" + dateStr + "'," +
    "'YYYY-MM-DD HH24:MI:SS') between to_timestamp(valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
            "AND to_timestamp(valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS')))";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        strtDte = dtst.Tables[0].Rows[0][1].ToString();
        return true;
      }
      strtDte = "";
      return false;
    }

    public static bool doesPrsnHvItm(long prsnID, long itmID)
    {
      string strSql = @"Select a.row_id 
      FROM pasn.prsn_bnfts_cntrbtns a where((a.person_id = " +
    prsnID + ") and (a.item_id = " + itmID + "))";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        //strtDte = dtst.Tables[0].Rows[0][1].ToString();
        return true;
      }
      //strtDte = "";
      return false;
    }

    public static DataSet getItmVal1(long itmid)
    {
      string selSQL = "SELECT pssbl_value_id, pssbl_value_code_name, pssbl_amount, pssbl_value_sql, item_id " +
      "FROM org.org_pay_items_values WHERE ((item_id = " + itmid + ")) ORDER BY pssbl_value_id DESC";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      return dtst;
    }

    public static DataSet getAllItmFeeds1(long itmid)
    {
      string selSQL = "SELECT a.balance_item_id, a.adds_subtracts, b.balance_type, a.scale_factor, c.pssbl_value_id " +
      "FROM org.org_pay_itm_feeds a LEFT OUTER JOIN org.org_pay_items b " +
      "ON a.balance_item_id = b.item_id LEFT OUTER JOIN org.org_pay_items_values c " +
      "ON c.item_id = a.balance_item_id WHERE ((a.fed_by_itm_id = " + itmid +
      ")) ORDER BY a.feed_id ";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      return dtst;
    }

    public static DataSet get_One_Itm_Det1(long itmID)
    {
      string strSql = "";
      strSql = "SELECT a.item_id, a.item_code_name, " +
       "a.item_desc, a.item_maj_type, a.item_min_type, a.item_value_uom, " +
        "a.uses_sql_formulas, a.cost_accnt_id, a.bals_accnt_id, a.is_enabled, a.org_id, " +
        "a.pay_frequency, a.pay_run_priority, a.incrs_dcrs_cost_acnt, a.incrs_dcrs_bals_acnt, a.balance_type " +
       "FROM org.org_pay_items a " +
       "WHERE(a.item_id = " + itmID + ")";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      //Global.mnFrm.grd_SQL = strSql;
      return dtst;
    }

    public static DataSet get_Basic_Itm1(string searchWord, string searchIn,
     Int64 offset, int limit_size, long prsnID, long itmStID)
    {
      string itmSQL = Global.mnFrm.cmCde.getGnrlRecNm("pay.pay_itm_sets_hdr",
        "hdr_id", "sql_query", itmStID);
      string strSql = "";
      string mnlSQL = "";
      string whereCls = "";
      if (searchIn == "Item Name")
      {
        whereCls = "(a.item_code_name ilike '" + searchWord.Replace("'", "''") +
       "') and ";
      }
      else if (searchIn == "Pay Frequency")
      {
        whereCls = "(a.pay_frequency ilike '" + searchWord.Replace("'", "''") +
       "') and ";
      }

      mnlSQL = "SELECT a.item_id, a.item_code_name, a.pay_frequency, a.item_maj_type, a.item_min_type, a.item_value_uom, COALESCE(b.item_pssbl_value_id,-1) " +
               "FROM org.org_pay_items a LEFT OUTER JOIN pasn.prsn_bnfts_cntrbtns b ON (a.item_id=b.item_id AND (b.person_id = " + prsnID +
     ") and (now() between to_timestamp(b.valid_start_date," +
     "'YYYY-MM-DD HH24:MI:SS') AND to_timestamp(b.valid_end_date,'YYYY-MM-DD HH24:MI:SS')))" +
               "WHERE (" + whereCls + "(a.is_enabled='1') AND (a.item_id IN " +
     "(select g.item_id from pay.pay_itm_sets_det g where g.hdr_id=" + itmStID + "))) " +
     "ORDER BY a.item_maj_type DESC, a.pay_run_priority, a.item_code_name LIMIT " + limit_size +
     " OFFSET " + (Math.Abs(offset * limit_size)).ToString();

      strSql = "SELECT a.item_id, a.item_code_name, a.pay_frequency, a.item_maj_type, a.item_min_type, a.item_value_uom, COALESCE(b.item_pssbl_value_id,-1) " +
               "FROM org.org_pay_items a LEFT OUTER JOIN pasn.prsn_bnfts_cntrbtns b ON (a.item_id=b.item_id AND (b.person_id = " + prsnID +
     ") and (now() between to_timestamp(b.valid_start_date," +
     "'YYYY-MM-DD HH24:MI:SS') AND to_timestamp(b.valid_end_date,'YYYY-MM-DD HH24:MI:SS')))" +
               "WHERE (" + whereCls + "(a.is_enabled='1') AND (a.item_id IN " +
     "(select tbl1.item_id from (" + itmSQL + ") tbl1 ))) " +
     "ORDER BY a.item_maj_type DESC, a.pay_run_priority, a.item_code_name LIMIT " + limit_size +
     " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
      if (itmSQL == "")
      {
        strSql = mnlSQL;
      }
      Global.mnFrm.itm_SQL1 = strSql;
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      return dtst;
    }

    public static long get_Total_Itm1(string searchWord, string searchIn, long prsnID, long itmStID)
    {
      string itmSQL = Global.mnFrm.cmCde.getGnrlRecNm("pay.pay_itm_sets_hdr",
        "hdr_id", "sql_query", itmStID);
      string strSql = "";
      string mnlSQL = "";
      string whereCls = "";
      if (searchIn == "Item Name")
      {
        whereCls = "(a.item_code_name ilike '" + searchWord.Replace("'", "''") +
       "') and ";
      }
      else if (searchIn == "Pay Frequency")
      {
        whereCls = "(a.pay_frequency ilike '" + searchWord.Replace("'", "''") +
       "') and ";
      }
      mnlSQL = "SELECT count(1) " +
               "FROM org.org_pay_items a LEFT OUTER JOIN pasn.prsn_bnfts_cntrbtns b ON (a.item_id=b.item_id AND (b.person_id = " + prsnID +
     ") and (now() between to_timestamp(b.valid_start_date," +
     "'YYYY-MM-DD HH24:MI:SS') AND to_timestamp(b.valid_end_date,'YYYY-MM-DD HH24:MI:SS')))" +
               "WHERE (" + whereCls + "(a.is_enabled='1') AND (a.item_id IN " +
     "(select g.item_id from pay.pay_itm_sets_det g where g.hdr_id=" + itmStID + ")))";

      strSql = "SELECT count(1) " +
               "FROM org.org_pay_items a LEFT OUTER JOIN pasn.prsn_bnfts_cntrbtns b ON (a.item_id=b.item_id AND (b.person_id = " + prsnID +
     ") and (now() between to_timestamp(b.valid_start_date," +
     "'YYYY-MM-DD HH24:MI:SS') AND to_timestamp(b.valid_end_date,'YYYY-MM-DD HH24:MI:SS')))" +
               "WHERE (" + whereCls + "(a.is_enabled='1') AND (a.item_id IN " +
     "(select tbl1.item_id from (" + itmSQL + ") tbl1 )))";
      if (itmSQL == "")
      {
        strSql = mnlSQL;
      }
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return 0;
      }
    }

    public static string get_Itm_Rec_Hstry1(int itmID)
    {
      string strSQL = @"SELECT a.created_by, 
      to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS'), 
      a.last_update_by, 
      to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS') " +
      "FROM org.org_pay_items a WHERE(a.item_id = " + itmID + ")";
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

    #region "ITEM PERSONS..."
    public static string[] get_Org_DfltPrsSt(int orgID)
    {
      string[] res = { "-1", "" };
      string strSql = "";
      strSql = "SELECT a.prsn_set_hdr_id, a.prsn_set_hdr_name " +
               "FROM pay.pay_prsn_sets_hdr a, pay.pay_sets_allwd_roles b " +
               "WHERE (a.prsn_set_hdr_id = b.prsn_set_id and (a.org_id = " + orgID +
               ") and (a.is_default = '1') and (is_enabled = '1')) ORDER BY a.prsn_set_hdr_id LIMIT 1 OFFSET 0";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        res[0] = dtst.Tables[0].Rows[0][0].ToString();
        res[1] = dtst.Tables[0].Rows[0][1].ToString();
      }
      return res;
    }

    public static DataSet get_Org_Persons(string searchWord, string searchIn,
   Int64 offset, int limit_size, int orgID, int prsStID)
    {
      string prsSQL = Global.mnFrm.cmCde.getPrsStSQL(prsStID);
      string strSql = "";
      string mnlSQL = "";
      if (searchIn == "ID")
      {
        mnlSQL = "Select distinct a.person_id, a.local_id_no, trim(a.title || ' ' || a.sur_name || " +
         "', ' || a.first_name || ' ' || a.other_names) full_name, b.prsn_set_det_id " +
        "from prs.prsn_names_nos a, pay.pay_prsn_sets_det b " +
        "WHERE ((a.person_id = b.person_id) and (b.prsn_set_hdr_id = " + prsStID +
        ") and (a.local_id_no ilike '" + searchWord.Replace("'", "''") +
         "') AND (a.org_id = " + orgID + ")) ORDER BY a.local_id_no DESC LIMIT " + limit_size +
         " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
        strSql = "select * from (" + prsSQL + ") tbl1 where tbl1.local_id_no ilike '" + searchWord.Replace("'", "''") +
         "' ORDER BY tbl1.local_id_no ASC LIMIT " + limit_size +
         " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
      }
      else if (searchIn == "Full Name")
      {
        mnlSQL = "Select distinct a.person_id, a.local_id_no, trim(a.title || ' ' || a.sur_name || " +
         "', ' || a.first_name || ' ' || a.other_names) full_name, b.prsn_set_det_id " +
        "from prs.prsn_names_nos a, pay.pay_prsn_sets_det b " +
        "WHERE ((a.person_id = b.person_id) and (b.prsn_set_hdr_id = " + prsStID +
        ") and (trim(a.title || ' ' || a.sur_name || " +
         "', ' || a.first_name || ' ' || a.other_names) ilike '" + searchWord.Replace("'", "''") +
         "') AND (a.org_id = " + orgID + ")) ORDER BY a.local_id_no DESC LIMIT " + limit_size +
         " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
        strSql = "select * from (" + prsSQL + ") tbl1 where tbl1.full_name ilike '" + searchWord.Replace("'", "''") +
    "' ORDER BY tbl1.local_id_no ASC LIMIT " + limit_size +
    " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
      }
      if (prsSQL == "")
      {
        strSql = mnlSQL;
      }
      Global.mnFrm.prs_SQL = strSql;
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      return dtst;
    }

    public static long get_Total_OrgPrs(string searchWord,
     string searchIn, int orgID, int prsStID)
    {
      string prsSQL = Global.mnFrm.cmCde.getPrsStSQL(prsStID);
      string strSql = "";
      string mnlSQL = "";
      if (searchIn == "ID")
      {
        mnlSQL = "Select count(distinct a.person_id) " +
        "from prs.prsn_names_nos a, pay.pay_prsn_sets_det b " +
        "WHERE ((a.person_id = b.person_id) and (b.prsn_set_hdr_id = " + prsStID +
        ") and (a.local_id_no ilike '" + searchWord.Replace("'", "''") +
         "') AND (a.org_id = " + orgID + "))";
        strSql = "select count(distinct tbl1.person_id) from (" + prsSQL +
          ") tbl1 where tbl1.local_id_no ilike '" + searchWord.Replace("'", "''") +
         "'";
      }
      else if (searchIn == "Full Name")
      {
        mnlSQL = "Select count(distinct a.person_id) " +
        "from prs.prsn_names_nos a, pay.pay_prsn_sets_det b " +
        "WHERE ((a.person_id = b.person_id) and (b.prsn_set_hdr_id = " + prsStID +
        ") and (trim(a.title || ' ' || a.sur_name || " +
         "', ' || a.first_name || ' ' || a.other_names) ilike '" + searchWord.Replace("'", "''") +
         "') AND (a.org_id = " + orgID + "))";
        strSql = "select count(distinct tbl1.person_id) from (" + prsSQL +
          ") tbl1 where tbl1.full_name ilike '" + searchWord.Replace("'", "''") +
    "'";
      }
      if (prsSQL == "")
      {
        strSql = mnlSQL;
      }
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return 0;
      }
    }

    public static string get_Prs_Rec_Hstry(int prsID)
    {
      string strSQL = @"SELECT a.created_by, 
      to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS'), 
      a.last_update_by, 
      to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS') " +
      "FROM prs.prsn_names_nos a WHERE(a.person_id  = " + prsID + ")";
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

    #region "PAST PAYMENTS..."
    public static DataSet getPstPayDet(long paytrnsid)
    {
      string strSql = @"SELECT a.amount_paid, 
to_char(to_timestamp(a.paymnt_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS')
, a.pay_trns_type, a.crncy_id, a.pymnt_desc " +
       "FROM pay.pay_itm_trnsctns a " +
       "WHERE ((a.pay_trns_id = " + paytrnsid + "))";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      return dtst;
    }

    public static long getPymntRvrslTrnsID(long paytrnsid)
    {
      string strSql = @"SELECT a.pay_trns_id " +
        "FROM pay.pay_itm_trnsctns a " +
        "WHERE ((a.src_py_trns_id = "
        + paytrnsid + ") or (a.pay_trns_id = "
        + paytrnsid + " AND a.src_py_trns_id>0))";

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

    public static void updateTrnsVldtyStatus(long paytrnsid, string vldty)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE pay.pay_itm_trnsctns " +
      "SET pymnt_vldty_status='" + vldty.Replace("'", "''") +
      "', last_update_by=" + Global.myPay.user_id +
      ", last_update_date='" + dateStr +
      "' WHERE pay_trns_id = " + paytrnsid;
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static long getPaymntTrnsID(long prsnid, long itmid,
      double amnt, string paydate, long orgnlTrnsID)
    {
      //, string vldty, long srcTrnsID
      paydate = DateTime.ParseExact(
   paydate, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      string strSql = "SELECT pay_trns_id FROM pay.pay_itm_trnsctns WHERE (person_id = " +
          prsnid + " and item_id = " + itmid + " and amount_paid = " + amnt +
          " and paymnt_date = '" + paydate.Replace("'", "''") +
          "' and pymnt_vldty_status = 'VALID' and src_py_trns_id=" + orgnlTrnsID + ")";
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

    public static long getPaymntTrnsIDREtro(long prsnid, long itmid,
      double amnt, string paydate, long orgnlTrnsID)
    {
      //, string vldty, long srcTrnsID
      paydate = DateTime.ParseExact(
   paydate, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      string strSql = "SELECT pay_trns_id FROM pay.pay_itm_trnsctns_retro WHERE (person_id = " +
          prsnid + " and item_id = " + itmid + " and amount_paid = " + amnt +
          " and paymnt_date = '" + paydate.Replace("'", "''") +
          "' and pymnt_vldty_status = 'VALID' and src_py_trns_id=" + orgnlTrnsID + ")";
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

    public static long getPaymntTrnsIDREtro(long prsnid, long itmid,
     double amnt, string paydate, string dteEarned, long orgnlTrnsID)
    {
      //, string vldty, long srcTrnsID
      paydate = DateTime.ParseExact(
   paydate, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      string strSql = "SELECT pay_trns_id FROM pay.pay_itm_trnsctns WHERE (person_id = " +
          prsnid + " and item_id = " + itmid + " and amount_paid = " + amnt +
          " and date_earned = '" + dteEarned.Replace("'", "''") +
          "' and paymnt_date = '" + paydate.Replace("'", "''") +
          "' and pymnt_vldty_status='VALID' and src_py_trns_id=" + orgnlTrnsID + ")";
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

    public static DataSet get_PstPays(long prsnID, long itmID, string retroStrtDte, string retroEndDte)
    {
      retroStrtDte = DateTime.ParseExact(
retroStrtDte, "dd-MMM-yyyy HH:mm:ss",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

      retroEndDte = DateTime.ParseExact(
retroEndDte, "dd-MMM-yyyy HH:mm:ss",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

      string strSql = "";
      strSql = @"SELECT a.pay_trns_id, a.amount_paid, a.paymnt_date
        , a.pay_trns_type, a.crncy_id, a.pymnt_desc, a.paymnt_source, a.pymnt_vldty_status, a.src_py_trns_id " +
      "FROM pay.pay_itm_trnsctns a " +
      @"WHERE ((to_timestamp(a.paymnt_date,'YYYY-MM-DD HH24:MI:SS') between 
      to_timestamp('" + retroStrtDte + @"','YYYY-MM-DD HH24:MI:SS') AND 
      to_timestamp('" + retroEndDte + "','YYYY-MM-DD HH24:MI:SS')) AND (person_id = " + prsnID +
      ") AND (item_id = " + itmID +
       ") AND a.pymnt_vldty_status='VALID' AND a.src_py_trns_id <= 0) ORDER BY to_timestamp(a.paymnt_date,'YYYY-MM-DD HH24:MI:SS') ASC, a.pay_trns_id ASC";

      //Global.mnFrm.pst_SQL = strSql;
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      return dtst;
    }

    public static double get_PstRetroPaysSum(long prsnID, long itmID, string dteEarned)
    {
      dteEarned = DateTime.ParseExact(
dteEarned, "dd-MMM-yyyy HH:mm:ss",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

      string strSql = "";
      strSql = @"SELECT COALESCE(SUM(a.amount_paid),0) FROM pay.pay_itm_trnsctns a " +
      @"WHERE ((substr(a.date_earned,1,10) = substr('" + dteEarned + @"',1,10)) AND (person_id = " + prsnID +
      ") AND (item_id = " + itmID +
       ") AND a.pymnt_vldty_status='VALID' AND a.src_py_trns_id <= 0)";

      //Global.mnFrm.pst_SQL = strSql;
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return double.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return 0;
    }

    public static DataSet get_Basic_Pst(string searchWord, string searchIn,
     Int64 offset, int limit_size, long prsnID, long itmID)
    {
      string strSql = "";
      if (searchIn == "Date")
      {
        strSql = @"SELECT a.pay_trns_id, a.amount_paid, 
        to_char(to_timestamp(a.paymnt_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS')
        , a.pay_trns_type, a.crncy_id, a.pymnt_desc, a.paymnt_source, a.pymnt_vldty_status, a.src_py_trns_id " +
        "FROM pay.pay_itm_trnsctns a " +
        "WHERE ((to_char(to_timestamp(a.paymnt_date,'YYYY-MM-DD HH24:MI:SS'), 'DD-Mon-YYYY HH24:MI:SS') ilike '" + searchWord.Replace("'", "''") +
         "') AND (person_id = " + prsnID + ") AND (item_id = " + itmID +
         ")) ORDER BY to_timestamp(a.paymnt_date,'YYYY-MM-DD HH24:MI:SS') DESC, a.pay_trns_id DESC LIMIT " + limit_size +
         " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
      }
      else if (searchIn == "Description")
      {
        strSql = @"SELECT a.pay_trns_id, a.amount_paid, 
to_char(to_timestamp(a.paymnt_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS')
, a.pay_trns_type, a.crncy_id, a.pymnt_desc, a.paymnt_source, a.pymnt_vldty_status, a.src_py_trns_id " +
        "FROM pay.pay_itm_trnsctns a " +
        "WHERE ((a.pymnt_desc ilike '" + searchWord.Replace("'", "''") +
         "') AND (person_id = " + prsnID + ") AND (item_id = " + itmID +
         ")) ORDER BY to_timestamp(a.paymnt_date,'YYYY-MM-DD HH24:MI:SS') DESC, a.pay_trns_id DESC LIMIT " + limit_size +
         " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
      }
      Global.mnFrm.pst_SQL = strSql;
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      return dtst;
    }

    public static long get_Total_Pst(string searchWord, string searchIn, long prsnID, long itmID)
    {
      string strSql = "";
      if (searchIn == "Date")
      {
        strSql = "SELECT count(1) " +
        "FROM pay.pay_itm_trnsctns a " +
        @"WHERE ((to_char(to_timestamp(a.paymnt_date,'YYYY-MM-DD HH24:MI:SS'),
        'DD-Mon-YYYY HH24:MI:SS') ilike '" + searchWord.Replace("'", "''") +
         "') AND (person_id = " + prsnID + ") AND (item_id = " + itmID + "))";
      }
      else if (searchIn == "Description")
      {
        strSql = "SELECT count(1) " +
        "FROM pay.pay_itm_trnsctns a " +
        "WHERE ((a.pymnt_desc ilike '" + searchWord.Replace("'", "''") +
         "') AND (person_id = " + prsnID + ") AND (item_id = " + itmID + "))";
      }
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return 0;
      }
    }

    public static DataSet get_Basic_PstBls(string searchWord, string searchIn,
  Int64 offset, int limit_size, long prsnID, long itmID)
    {
      string strSql = "";
      if (searchIn == "Date")
      {
        strSql = @"SELECT a.bals_id, a.bals_amount, 
        to_char(to_timestamp(a.bals_date,'YYYY-MM-DD'),
        'DD-Mon-YYYY'), 
        'Balance Amount', b.item_value_uom,'Balance','Balance', 'VALID',-1 " +
        "FROM pay.pay_balsitm_bals a LEFT OUTER JOIN org.org_pay_items b ON a.bals_itm_id = b.item_id " +
        @"WHERE ((to_char(to_timestamp(a.bals_date,'YYYY-MM-DD'),
        'DD-Mon-YYYY') ilike '" + searchWord.Replace("'", "''") +
         "') AND (a.person_id = " + prsnID + ") AND (a.bals_itm_id = " + itmID +
         ")) ORDER BY to_timestamp(a.bals_date,'YYYY-MM-DD') DESC, a.bals_id DESC LIMIT " + limit_size +
         " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
      }
      else if (searchIn == "Description")
      {
        strSql = @"SELECT a.bals_id, a.bals_amount, 
        to_char(to_timestamp(a.bals_date,'YYYY-MM-DD'),
        'DD-Mon-YYYY'), 'Balance Amount', b.item_value_uom,'Balance','Balance', 'VALID',-1 " +
        "FROM pay.pay_balsitm_bals a LEFT OUTER JOIN org.org_pay_items b ON a.bals_itm_id = b.item_id " +
        "WHERE ((b.item_desc ilike '" + searchWord.Replace("'", "''") +
         "') AND (a.person_id = " + prsnID + ") AND (a.bals_itm_id = " + itmID +
         ")) ORDER BY to_timestamp(a.bals_date,'YYYY-MM-DD') DESC, a.bals_id DESC LIMIT " + limit_size +
         " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
      }
      Global.mnFrm.pst_SQL = strSql;
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      return dtst;
    }

    public static long get_Total_PstBls(string searchWord, string searchIn, long prsnID, long itmID)
    {
      string strSql = "";
      if (searchIn == "Date")
      {
        strSql = "SELECT count(1) " +
        "FROM pay.pay_balsitm_bals a LEFT OUTER JOIN org.org_pay_items b ON a.bals_itm_id = b.item_id " +
        @"WHERE ((to_char(to_timestamp(a.bals_date,'YYYY-MM-DD'),
        'DD-Mon-YYYY') ilike '" + searchWord.Replace("'", "''") +
         "') AND (a.person_id = " + prsnID + ") AND (a.bals_itm_id = " + itmID + "))";
      }
      else if (searchIn == "Description")
      {
        strSql = "SELECT count(1) " +
        "FROM pay.pay_balsitm_bals a LEFT OUTER JOIN org.org_pay_items b ON a.bals_itm_id = b.item_id " +
        "WHERE ((b.item_desc ilike '" + searchWord.Replace("'", "''") +
         "') AND (a.person_id = " + prsnID + ") AND (a.bals_itm_id = " + itmID + "))";
      }
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return 0;
      }
    }

    public static string get_Pst_Rec_Hstry(long trnsID)
    {
      string strSQL = @"SELECT a.created_by, 
      to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS'), 
      a.last_update_by, 
      to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS') " +
      "FROM pay.pay_itm_trnsctns a WHERE(a.pay_trns_id = " + trnsID + ")";
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

    public static decimal get_ttl_paymnts(long prsnID, long itmID, string whopays)
    {
      string colnm = "ttl_amnt_given_prsn";
      if (whopays == "Person")
      {
        colnm = "ttl_amnt_prsn_hs_paid";
      }
      /*string strSql = "Select SUM(a.amount_paid) FROM pay.pay_itm_trnsctns a where a.person_id = " + 
       prsnID + " and a.item_id = " + itmID + " and a.pay_trns_type like '%Payment%'";*/
      string strSql = "Select " + colnm + " FROM pasn.prsn_bnfts_cntrbtns a where a.person_id = " +
          prsnID + " and a.item_id = " + itmID + "";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      Decimal fnl_val = 0;
      bool res = false;
      if (dtst.Tables[0].Rows.Count > 0)
      {
        res = decimal.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out fnl_val);
        if (res)
        {
          return fnl_val;
        }
      }
      return fnl_val;
    }

    public static decimal get_ttl_withdrwls(long prsnID, long itmID)
    {
      /*string strSql = "Select SUM(a.amount_paid) FROM pay.pay_itm_trnsctns a where a.person_id = " +
       prsnID + " and a.item_id = " + itmID + " and a.pay_trns_type like '%Withdrawal%'";*/
      string strSql = "Select ttl_amnt_wthdrwn FROM pasn.prsn_bnfts_cntrbtns a where a.person_id = " +
   prsnID + " and a.item_id = " + itmID + "";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      Decimal fnl_val = 0;
      bool res = false;
      if (dtst.Tables[0].Rows.Count > 0)
      {
        res = decimal.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out fnl_val);
        if (res)
        {
          return fnl_val;
        }
      }
      return fnl_val;
    }

    public static string getPymntTyp(long py_trns_id)
    {
      string strSql = "SELECT a.paymnt_source FROM pay.pay_itm_trnsctns a WHERE a.pay_trns_id = " + py_trns_id;
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return dtst.Tables[0].Rows[0][0].ToString();
      }
      return "";
    }

    public static bool hsMsPyBnRun(long mspyid)
    {
      string strSql = "SELECT a.run_status FROM pay.pay_mass_pay_run_hdr a WHERE a.mass_pay_id = " + mspyid;
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[0][0].ToString());
      }
      return false;
    }

    public static bool hsMsPyGoneToGL(long mspyid)
    {
      string strSql = "SELECT a.sent_to_gl FROM pay.pay_mass_pay_run_hdr a WHERE a.mass_pay_id = " + mspyid;
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[0][0].ToString());
      }
      return false;
    }

    public static bool hsPrsItmBlsBnUptd(long pytrnsid,
      string trnsdate, long bals_itm_id, long prsn_id)
    {
      trnsdate = DateTime.ParseExact(
   trnsdate, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      if (trnsdate.Length > 10)
      {
        trnsdate = trnsdate.Substring(0, 10);
      }

      string strSql = "SELECT a.bals_id FROM pay.pay_balsitm_bals a WHERE a.bals_itm_id = " + bals_itm_id +
        " and a.person_id = " + prsn_id + " and a.bals_date = '" + trnsdate + "' and a.source_trns_ids like '%," + pytrnsid + ",%'";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return true;
      }
      return false;
    }

    public static bool hsPrsItmBlsBnUptdRetro(long pytrnsid,
     string trnsdate, long bals_itm_id, long prsn_id)
    {
      trnsdate = DateTime.ParseExact(
   trnsdate, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      if (trnsdate.Length > 10)
      {
        trnsdate = trnsdate.Substring(0, 10);
      }

      string strSql = "SELECT a.bals_id FROM pay.pay_balsitm_bals_retro a WHERE a.bals_itm_id = " + bals_itm_id +
        " and a.person_id = " + prsn_id + " and a.bals_date = '" + trnsdate + "' and a.source_trns_ids like '%," + pytrnsid + ",%'";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return true;
      }
      return false;
    }

    public static void deletePymntGLInfcLns(long pyTrnsID)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string delSQL = "DELETE FROM pay.pay_gl_interface WHERE source_trns_id = " +
        pyTrnsID + " and gl_batch_id = -1";
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }

    public static long getIntFcTrnsDbtLn(long pytrnsid, double pay_amnt)
    {
      string strSql = "SELECT a.interface_id FROM pay.pay_gl_interface a " +
              "WHERE a.source_trns_id = " + pytrnsid +
        " and a.dbt_amount = " + pay_amnt + " ";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return -1;
    }

    public static long getIntFcTrnsCrdtLn(long pytrnsid, double pay_amnt)
    {
      string strSql = "SELECT a.interface_id FROM pay.pay_gl_interface a " +
              "WHERE a.source_trns_id = " + pytrnsid +
        " and a.crdt_amount = " + pay_amnt + " ";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return -1;
    }

    public static double getMsPyAmntSum(long mspyid)
    {
      string strSql = "SELECT SUM(a.amount_paid) FROM pay.pay_itm_trnsctns a, org.org_pay_items b " +
@"WHERE a.item_id = b.item_id and a.pay_trns_type !='Purely Informational' 
      and b.cost_accnt_id>0 and b.bals_accnt_id>0 and a.crncy_id > 0 and a.mass_pay_id = " + mspyid;
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      double sumRes = 0.00;
      if (dtst.Tables[0].Rows.Count > 0)
      {
        double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out sumRes);
      }
      return sumRes;
    }

    public static double getMsPyIntfcDbtSum(long mspyid)
    {
      string strSql = "SELECT SUM(a.dbt_amount) FROM pay.pay_gl_interface a " +
        "WHERE a.source_trns_id IN (select b.pay_trns_id from pay.pay_itm_trnsctns b WHERE b.mass_pay_id = " + mspyid + ") ";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      double sumRes = 0.00;
      if (dtst.Tables[0].Rows.Count > 0)
      {
        double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out sumRes);
      }
      return sumRes;
    }

    public static double getMsPyIntfcCrdtSum(long mspyid)
    {
      string strSql = "SELECT SUM(a.crdt_amount) FROM pay.pay_gl_interface a " +
        "WHERE a.source_trns_id IN (select b.pay_trns_id from pay.pay_itm_trnsctns b WHERE b.mass_pay_id = " + mspyid + ") ";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      double sumRes = 0.00;
      if (dtst.Tables[0].Rows.Count > 0)
      {
        double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out sumRes);
      }
      return sumRes;
    }

    public static bool doesPrsnHvItm(long prsnID, long itmID, string dateStr)
    {
      dateStr = DateTime.ParseExact(
   dateStr, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      string strSql = "Select a.row_id FROM pasn.prsn_bnfts_cntrbtns a where((a.person_id = " +
    prsnID + ") and (a.item_id = " + itmID + ") and (to_timestamp('" + dateStr + "'," +
    "'YYYY-MM-DD HH24:MI:SS') between to_timestamp(valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
            "AND to_timestamp(valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS')))";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return true;
      }
      return false;
    }

    public static long getFirstItmValID(long itmID)
    {
      string strSql = @"Select a.pssbl_value_id FROM org.org_pay_items_values a 
      where((a.item_id = " + itmID + ")) ORDER BY 1 LIMIT 1 OFFSET 0";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return -1;
    }

    public static long hsPrsnBnPaidItmMsPy(long prsnID, long itmID,
      string trns_date, double amnt)
    {
      trns_date = DateTime.ParseExact(
      trns_date, "dd-MMM-yyyy HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      //if (trns_date.Length > 10)
      //{
      //  trns_date = trns_date.Substring(0, 10);
      //}
      string strSql = "Select a.pay_trns_id FROM pay.pay_itm_trnsctns a where((a.person_id = " +
    prsnID + ") and (a.item_id = " + itmID + ") and (paymnt_date ilike '%" + trns_date +
    "%') and (amount_paid=" + amnt + ") and (a.pymnt_vldty_status='VALID' and a.src_py_trns_id < 0))";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return -1;
    }

    public static long hsPrsnBnPaidItmMsPyRetro(long prsnID, long itmID,
     string trns_date, double amnt)
    {
      trns_date = DateTime.ParseExact(
      trns_date, "dd-MMM-yyyy HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      //if (trns_date.Length > 10)
      //{
      //  trns_date = trns_date.Substring(0, 10);
      //}
      string strSql = "Select a.pay_trns_id FROM pay.pay_itm_trnsctns_retro a where((a.person_id = " +
    prsnID + ") and (a.item_id = " + itmID + ") and (paymnt_date ilike '%" + trns_date +
    "%') and (amount_paid=" + amnt + ") and (a.pymnt_vldty_status='VALID' and a.src_py_trns_id < 0))";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return -1;
    }

    public static bool hsPrsnBnPaidItmInInvc(long prsnID, long itmID, ref long rcvblInvcID, ref string rcvblInvcTyp)
    {
      string selSQL = @"select a.pymnt_id, a.amount_paid, 
      b.rcvbls_invc_number,b.rcvbls_invc_type,b.rcvbls_invc_hdr_id, 
      a.intnl_pay_trns_id, c.person_id, c.item_id
      from accb.accb_payments a, accb.accb_rcvbls_invc_hdr b, pay.pay_itm_trnsctns c
      WHERE a.src_doc_id = b.rcvbls_invc_hdr_id and a.src_doc_typ = b.rcvbls_invc_type
      and a.intnl_pay_trns_id = c.pay_trns_id and c.person_id=" + prsnID +
      @" and c.item_id=" + itmID + @"
      ORDER BY 3";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        rcvblInvcID = long.Parse(dtst.Tables[0].Rows[0][4].ToString());
        rcvblInvcTyp = dtst.Tables[0].Rows[0][3].ToString();
        return true;
      }
      return false;
    }

    public static bool hsPrsnBnPaidItmMnl(long prsnID, long itmID,
      string trns_date, double amnt)
    {
      trns_date = DateTime.ParseExact(
      trns_date, "dd-MMM-yyyy HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      //if (trns_date.Length > 10)
      //{
      //  trns_date = trns_date.Substring(0, 10);
      //}
      string strSql = "Select a.pay_trns_id FROM pay.pay_itm_trnsctns a where((a.person_id = " +
    prsnID + ") and (a.item_id = " + itmID + ") and (paymnt_date like '%" + trns_date +
    "%') and (amount_paid=" + amnt + ") and (a.pymnt_vldty_status='VALID' and a.src_py_trns_id < 0))";
      // and (paymnt_source = '" + py_src.Replace("'", "''") + "')
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return true;
      }
      return false;
    }

    public static bool doesPymntDteViolateFreq(long prsnID, long itmID,
      string trns_date)
    {
      /*Daily
   Weekly
   Fortnightly
   Semi-Monthly
   Monthly
   Quarterly
   Half-Yearly
   Annually
   Adhoc
   None*/
      trns_date = DateTime.ParseExact(
      trns_date, "dd-MMM-yyyy HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

      string pyFreq = Global.mnFrm.cmCde.getGnrlRecNm("org.org_pay_items", "item_id", "pay_frequency", itmID);
      string intrvlCls = "";
      string whrCls = "";
      if (pyFreq == "Daily")
      {
        intrvlCls = "1 day";
      }
      else if (pyFreq == "Weekly")
      {
        intrvlCls = "7 day";
      }
      else if (pyFreq == "Fortnightly")
      {
        intrvlCls = "14 day";
      }
      else if (pyFreq == "Semi-Monthly")
      {
        intrvlCls = "14 day";
      }
      else if (pyFreq == "Monthly")
      {
        intrvlCls = "28 day";
      }
      else if (pyFreq == "Quarterly")
      {
        intrvlCls = "90 day";
      }
      else if (pyFreq == "Half-Yearly")
      {
        intrvlCls = "182 day";
      }
      else if (pyFreq == "Annually")
      {
        intrvlCls = "365 day";
      }
      else if (pyFreq == "Adhoc")
      {
        intrvlCls = "1 second";
        return false;
      }
      else if (pyFreq == "None")
      {
        intrvlCls = "1 second";
        return false;
      }
      else
      {
        intrvlCls = "1 second";
        if (pyFreq == "Once a Month" || pyFreq == "Twice a Month")
        {
          whrCls = @" and (substr(a.paymnt_date,1,7) = substr('" + trns_date +
    "',1,7))";
        }
      }
      if (whrCls == "")
      {
        whrCls = " and (age(GREATEST(paymnt_date::TIMESTAMP,'" + trns_date +
    "'::TIMESTAMP),LEAST(paymnt_date::TIMESTAMP, '" + trns_date +
    "'::TIMESTAMP)) < interval '" + intrvlCls + "')";
      }

      string strSql = "Select count(1) FROM pay.pay_itm_trnsctns a where((a.person_id = " +
    prsnID + ") and (a.item_id = " + itmID + @") and (a.pymnt_vldty_status='VALID' and 
      a.src_py_trns_id <= 0)" + whrCls + ")";
      // and (paymnt_source = '" + py_src.Replace("'", "''") + "')
      /*a.pay_trns_id, a.paymnt_date*/
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      //Global.mnFrm.cmCde.showSQLNoPermsn(pyFreq + "/" + strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        if (pyFreq == "Once a Month" && long.Parse(dtst.Tables[0].Rows[0][0].ToString()) >= 1)
        {
          return true;
        }
        else if (pyFreq == "Twice a Month" && long.Parse(dtst.Tables[0].Rows[0][0].ToString()) >= 2)
        {
          return true;
        }
        else if (!(pyFreq == "Once a Month" || pyFreq == "Twice a Month")
          && (long.Parse(dtst.Tables[0].Rows[0][0].ToString()) > 0))
        {
          return true;
        }
      }
      return false;
    }

    public static long getPrsnItmVlID(long prsnID, long itmID, string trnsdte)
    {
      trnsdte = DateTime.ParseExact(trnsdte, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

      //string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string strSql = "Select a.item_pssbl_value_id FROM pasn.prsn_bnfts_cntrbtns a where((a.person_id = " +
    prsnID + ") and (a.item_id = " + itmID + ") and (to_timestamp('" + trnsdte + "'," +
    "'YYYY-MM-DD HH24:MI:SS') between to_timestamp(valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
            "AND to_timestamp(valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS')))";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return -100000;
    }

    public static bool doesItmStHvItm(long hdrID, long itmID)
    {
      string strSql = "Select a.det_id FROM pay.pay_itm_sets_det a where((a.hdr_id = " +
    hdrID + ") and (a.item_id = " + itmID + "))";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return true;
      }
      return false;
    }

    public static bool doesPrsStHvPrs(long hdrID, long prsnID)
    {
      string strSql = "Select a.prsn_set_det_id FROM pay.pay_prsn_sets_det a where((a.prsn_set_hdr_id = " +
    hdrID + ") and (a.person_id = " + prsnID + "))";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return true;
      }
      return false;
    }
    #endregion

    #region "PAY ITEM SETS..."
    public static string[] get_Org_DfltItmSt(int orgID)
    {
      string[] res = { "-1", "" };
      string strSql = "";
      strSql = "SELECT a.hdr_id, a.itm_set_name, a.itm_set_desc, a.is_enabled " +
               "FROM pay.pay_itm_sets_hdr a , pay.pay_sets_allwd_roles b " +
               "WHERE (a.hdr_id = b.itm_set_id and (a.org_id = " + orgID +
               ") and (a.is_default = '1') and (a.is_enabled = '1')) ORDER BY a.hdr_id LIMIT 1 OFFSET 0";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        res[0] = dtst.Tables[0].Rows[0][0].ToString();
        res[1] = dtst.Tables[0].Rows[0][1].ToString();
      }
      return res;
    }

    public static DataSet get_One_ItmStDet(int itmStID, long offset, int limit_size)
    {
      string itmSQL = Global.mnFrm.cmCde.getGnrlRecNm("pay.pay_itm_sets_hdr",
        "hdr_id", "sql_query", itmStID);
      string strSql = "";
      string mnlSQL = "";
      string whereCls = "";
      mnlSQL = "SELECT a.item_id, b.item_code_name, b.item_value_uom, " +
        "a.to_do_trnsctn_type, a.det_id " +
    "FROM pay.pay_itm_sets_det a , org.org_pay_items b " +
    "WHERE((a.hdr_id = " + itmStID + ") and (a.item_id = b.item_id) and (b.is_enabled = '1')) ORDER BY b.pay_run_priority LIMIT " + limit_size +
     " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
      strSql = "SELECT tbl1.item_id, tbl1.item_code_name, tbl1.item_value_uom, tbl1.trns_typ, -1 " +
              "FROM (" + itmSQL + ") tbl1, org.org_pay_items a " +
              "WHERE ((tbl1.item_id = a.item_id) and (a.is_enabled = '1')) " +
    "ORDER BY a.pay_run_priority LIMIT " + limit_size +
     " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
      if (itmSQL == "")
      {
        strSql = mnlSQL;
      }

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      Global.mnFrm.idet_SQL = strSql;
      return dtst;
    }

    public static DataSet get_One_ItmStDet(int itmStID)
    {
      string itmSQL = Global.mnFrm.cmCde.getGnrlRecNm("pay.pay_itm_sets_hdr",
        "hdr_id", "sql_query", itmStID);
      string strSql = "";
      string mnlSQL = "";
      string whereCls = "";
      mnlSQL = "SELECT a.det_id, b.item_code_name, b.item_value_uom, " +
        "a.to_do_trnsctn_type, a.item_id, pay.get_first_itmval_id(a.item_id), b.item_maj_type, b.item_min_type, b.allow_value_editing " +
    "FROM pay.pay_itm_sets_det a , org.org_pay_items b " +
    "WHERE((a.hdr_id = " + itmStID + ") and (a.item_id = b.item_id) and (b.is_enabled = '1')) ORDER BY b.pay_run_priority ";

      strSql = @"SELECT -1, tbl1.item_code_name, tbl1.item_value_uom, tbl1.trns_typ, 
     tbl1.item_id, pay.get_first_itmval_id(tbl1.item_id), a.item_maj_type, a.item_min_type, a.allow_value_editing " +
              "FROM (" + itmSQL + ") tbl1, org.org_pay_items a " +
              "WHERE ((tbl1.item_id = a.item_id) and (a.is_enabled = '1')) " +
    "ORDER BY a.pay_run_priority ";
      if (itmSQL == "")
      {
        strSql = mnlSQL;
      }
      //Global.mnFrm.cmCde.showSQLNoPermsn(strSql);
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      //Global.mnFrm.idet_SQL = strSql;
      return dtst;
    }

    public static DataSet get_OnePrs_ItmStDet(long prsnID, long itmStID)
    {
      string itmSQL = Global.mnFrm.cmCde.getGnrlRecNm("pay.pay_itm_sets_hdr",
              "hdr_id", "sql_query", itmStID);

      string strSql = "";
      string mnlSQL = "";
      mnlSQL = "SELECT a.det_id, b.item_code_name, b.item_value_uom, " +
        "a.to_do_trnsctn_type, a.item_id, c.item_pssbl_value_id, b.item_maj_type, b.item_min_type, b.allow_value_editing " +
    "FROM pay.pay_itm_sets_det a , org.org_pay_items b, pasn.prsn_bnfts_cntrbtns c " +
    "WHERE(a.hdr_id = " + itmStID + ") and (a.item_id = b.item_id) and (b.is_enabled = '1') and " +
    "(a.item_id = c.item_id) AND (c.person_id = " + prsnID +
       ") and (now() between to_timestamp(c.valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
            "AND to_timestamp(c.valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS')) ORDER BY b.pay_run_priority, b.item_code_name LIMIT 100 OFFSET 0";

      strSql = "SELECT -1, tbl1.item_code_name, tbl1.item_value_uom, tbl1.trns_typ, tbl1.item_id, b.item_pssbl_value_id, a.item_maj_type, a.item_min_type, a.allow_value_editing " +
              "FROM (" + itmSQL + ") tbl1, org.org_pay_items a, pasn.prsn_bnfts_cntrbtns b " +
              "WHERE ((tbl1.item_id = a.item_id) and (a.item_id=b.item_id ) and (a.is_enabled = '1') AND (b.person_id = " + prsnID +
    ") and (now() between to_timestamp(b.valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
            "AND to_timestamp(b.valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS'))) " +
    "ORDER BY a.item_maj_type DESC, a.pay_run_priority, a.item_code_name";
      if (itmSQL == "")
      {
        strSql = mnlSQL;
      }

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      Global.mnFrm.prsnitm_SQL1 = strSql;
      return dtst;
    }

    public static DataSet get_AllItmStDet(int itmStID)
    {
      string itmSQL = Global.mnFrm.cmCde.getGnrlRecNm("pay.pay_itm_sets_hdr",
             "hdr_id", "sql_query", itmStID);

      string strSql = "";
      string mnlSQL = "";
      mnlSQL = "SELECT a.item_id, b.item_code_name, b.item_value_uom, " +
        "a.to_do_trnsctn_type, b.item_maj_type, b.item_min_type " +
    "FROM pay.pay_itm_sets_det a , org.org_pay_items b " +
    "WHERE((a.hdr_id = " + itmStID + ") and (a.item_id = b.item_id) and (b.is_enabled = '1')) ORDER BY b.pay_run_priority";

      strSql = "SELECT tbl1.item_id, tbl1.item_code_name, tbl1.item_value_uom, tbl1.trns_typ, a.item_maj_type, a.item_min_type " +
              "FROM (" + itmSQL + ") tbl1, org.org_pay_items a " +
              "WHERE ((tbl1.item_id = a.item_id) and (a.is_enabled = '1')) " +
    "ORDER BY a.pay_run_priority";
      if (itmSQL == "")
      {
        strSql = mnlSQL;
      }

      //  strSql = "SELECT a.item_id, b.item_code_name, b.item_value_uom, " +
      //    "a.to_do_trnsctn_type, b.item_maj_type, b.item_min_type " +
      //"FROM pay.pay_itm_sets_det a LEFT OUTER JOIN " +
      //"org.org_pay_items b on a.item_id = b.item_id " +
      //"WHERE(a.hdr_id = " + itmStID + " and b.is_enabled = '1') ORDER BY b.pay_run_priority ";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      return dtst;
    }

    public static DataSet get_AllEditblItmStDet(int itmStID)
    {
      string itmSQL = Global.mnFrm.cmCde.getGnrlRecNm("pay.pay_itm_sets_hdr",
             "hdr_id", "sql_query", itmStID);

      string strSql = "";
      string mnlSQL = "";
      mnlSQL = "SELECT a.item_id, b.item_code_name, b.item_value_uom, " +
        "a.to_do_trnsctn_type, b.item_maj_type, b.item_min_type, b.uses_sql_formulas, b.is_retro_element " +
    "FROM pay.pay_itm_sets_det a , org.org_pay_items b " +
    "WHERE((a.hdr_id = " + itmStID + ") and (a.item_id = b.item_id) and (b.is_enabled = '1' and b.allow_value_editing='1')) ORDER BY b.pay_run_priority";

      strSql = "SELECT tbl1.item_id, tbl1.item_code_name, tbl1.item_value_uom, tbl1.trns_typ, a.item_maj_type, a.item_min_type, a.uses_sql_formulas, a.is_retro_element " +
              "FROM (" + itmSQL + ") tbl1, org.org_pay_items a " +
              "WHERE ((tbl1.item_id = a.item_id) and (a.is_enabled = '1' and a.allow_value_editing='1')) " +
    "ORDER BY a.pay_run_priority";
      if (itmSQL == "")
      {
        strSql = mnlSQL;
      }

      //  strSql = "SELECT a.item_id, b.item_code_name, b.item_value_uom, " +
      //    "a.to_do_trnsctn_type, b.item_maj_type, b.item_min_type " +
      //"FROM pay.pay_itm_sets_det a LEFT OUTER JOIN " +
      //"org.org_pay_items b on a.item_id = b.item_id " +
      //"WHERE(a.hdr_id = " + itmStID + " and b.is_enabled = '1') ORDER BY b.pay_run_priority ";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      return dtst;
    }

    public static long get_Total_ItmsDet(long itmStID)
    {
      string itmSQL = Global.mnFrm.cmCde.getGnrlRecNm("pay.pay_itm_sets_hdr",
        "hdr_id", "sql_query", itmStID);
      string strSql = "";
      string mnlSQL = "";
      string whereCls = "";
      mnlSQL = "SELECT count(1) " +
    "FROM pay.pay_itm_sets_det a , org.org_pay_items b " +
    "WHERE((a.hdr_id = " + itmStID + ") and (a.item_id = b.item_id) and (b.is_enabled = '1'))";
      strSql = "SELECT count(1) " +
              "FROM (" + itmSQL + ") tbl1, org.org_pay_items a " +
              "WHERE ((tbl1.item_id = a.item_id) and (a.is_enabled = '1'))";
      if (itmSQL == "")
      {
        strSql = mnlSQL;
      }

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return 0;
      }
    }

    public static DataSet get_Basic_ItmSt(string searchWord, string searchIn,
  Int64 offset, int limit_size, int orgID)
    {
      string strSql = "";
      if (searchIn == "Item Set Name")
      {
        strSql = "SELECT a.hdr_id, a.itm_set_name, a.itm_set_desc, a.is_enabled, a.is_default, a.uses_sql , a.sql_query " +
                      "FROM pay.pay_itm_sets_hdr a " +
                      "WHERE ((a.itm_set_name ilike '" + searchWord.Replace("'", "''") +
       "') AND (org_id = " + orgID + ")) ORDER BY a.hdr_id DESC LIMIT " + limit_size +
       " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
      }
      else if (searchIn == "Item Set Description")
      {
        strSql = "SELECT a.hdr_id, a.itm_set_name, a.itm_set_desc, a.is_enabled, a.is_default, a.uses_sql , a.sql_query " +
      "FROM pay.pay_itm_sets_hdr a " +
      "WHERE ((a.itm_set_desc ilike '" + searchWord.Replace("'", "''") +
       "') AND (org_id = " + orgID + ")) ORDER BY a.hdr_id DESC LIMIT " + limit_size +
       " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
      }
      Global.mnFrm.itmst_SQL = strSql;
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      return dtst;
    }

    public static long get_Total_ItmSt(string searchWord, string searchIn, int orgID)
    {
      string strSql = "";
      if (searchIn == "Item Set Name")
      {
        strSql = "SELECT count(1) " +
        "FROM pay.pay_itm_sets_hdr a " +
        "WHERE ((a.itm_set_name ilike '" + searchWord.Replace("'", "''") +
         "') AND (org_id = " + orgID + "))";
      }
      else if (searchIn == "Item Set Description")
      {
        strSql = "SELECT count(1)  " +
        "FROM pay.pay_itm_sets_hdr a " +
        "WHERE ((a.itm_set_desc ilike '" + searchWord.Replace("'", "''") +
         "') AND (org_id = " + orgID + "))";
      }
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return 0;
      }
    }

    public static string get_ItmSt_Rec_Hstry(int hdrID)
    {
      string strSQL = @"SELECT a.created_by,
      to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS'), 
      a.last_update_by, 
      to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS') " +
      "FROM pay.pay_itm_sets_hdr a WHERE(a.hdr_id = " + hdrID + ")";
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

    public static long doesItmSetHvRole(long setID, int role_id)
    {
      string strSql = "SELECT pay_roles_id FROM pay.pay_sets_allwd_roles " +
        "WHERE itm_set_id = " + setID + " and user_role_id = " + role_id;

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

    public static long doesPrsnSetHvRole(long setID, int role_id)
    {
      string strSql = "SELECT pay_roles_id FROM pay.pay_sets_allwd_roles " +
        "WHERE prsn_set_id = " + setID + " and user_role_id = " + role_id;

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

    public static void createPayRole(long itmSetID, long prsSetID, int roleID)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = "INSERT INTO pay.pay_sets_allwd_roles(" +
            "prsn_set_id, itm_set_id, user_role_id, created_by, creation_date) " +
            "VALUES (" + prsSetID + ", " + itmSetID + ", " + roleID +
            ", " + Global.myPay.user_id + ", '" + dateStr +
               "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static DataSet get_AllRoles(long itmsetID)
    {
      string strSql = "SELECT a.user_role_id, b.role_name, a.pay_roles_id " +
        "FROM pay.pay_sets_allwd_roles a, sec.sec_roles b " +
        "WHERE a.itm_set_id = " + itmsetID + " and a.user_role_id = b.role_id";

      //Global.mnFrm.roles_SQL = strSql;
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      return dtst;
    }

    public static DataSet get_AllRoles1(long prssetID)
    {
      string strSql = "SELECT a.user_role_id, b.role_name, a.pay_roles_id " +
        "FROM pay.pay_sets_allwd_roles a, sec.sec_roles b " +
        "WHERE a.prsn_set_id = " + prssetID + " and a.user_role_id = b.role_id";

      //Global.mnFrm.roles_SQL = strSql;
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

    #endregion

    #region "GLOBAL VALUES..."
    public static int get_CriteriaID(string criteriaNm, string criteriaType)
    {
      string strSql = "";
      strSql = @"SELECT org.get_criteria_id('" + criteriaNm.Replace("'", "''") +
       "', '" + criteriaType.Replace("'", "''") + "') ";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      //Global.mnFrm.gbvdt_SQL = strSql;
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return -1;
    }

    public static int get_One_GBVDetID(int hdrID, int valID, string criteriaType, string startDte)
    {
      /*startDte = DateTime.ParseExact(
   startDte, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");*/

      string strSql = "";
      strSql = @"SELECT a.value_det_id FROM pay.pay_global_values_det a " +
    "WHERE((a.global_value_hdr_id = " + hdrID +
    ") AND (a.criteria_type='" + criteriaType.Replace("'", "''") +
    "') AND (a.criteria_val_id= " + valID +
    ") AND to_timestamp(a.valid_start_date,'YYYY-MM-DD HH24:MI:SS')" +
    "<=to_timestamp('" + startDte + "', 'DD-Mon-YYYY HH24:MI:SS') AND to_timestamp(a.valid_end_date,'YYYY-MM-DD HH24:MI:SS')" +
    ">=to_timestamp('" + startDte + "', 'DD-Mon-YYYY HH24:MI:SS'))";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      //Global.mnFrm.gbvdt_SQL = strSql;
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return -1;
    }

    public static DataSet get_One_GBVDet(int hdrID, long offset, int limit_size)
    {
      string strSql = "";
      strSql = @"SELECT a.value_det_id, a.criteria_type, a.criteria_val_id, 
   org.get_criteria_name(a.criteria_val_id,a.criteria_type)," +
        "a.num_value, to_char(to_timestamp(a.valid_start_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), " +
      "to_char(to_timestamp(a.valid_end_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') " +
    "FROM pay.pay_global_values_det a " +
    "WHERE((a.global_value_hdr_id = " + hdrID + ")) ORDER BY a.criteria_val_id LIMIT " + limit_size +
     " OFFSET " + (Math.Abs(offset * limit_size)).ToString();

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      Global.mnFrm.gbvdt_SQL = strSql;
      return dtst;
    }

    public static long get_Total_GBVDet(long hdrID)
    {
      string strSql = "";
      strSql = @"SELECT count(1) " +
    "FROM pay.pay_global_values_det a " +
    "WHERE((a.global_value_hdr_id = " + hdrID + "))";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return 0;
      }
    }

    public static DataSet get_Basic_GBV(string searchWord, string searchIn,
  Int64 offset, int limit_size, int orgID)
    {
      string strSql = "";
      if (searchIn == "Global Value Name")
      {
        strSql = @"SELECT a.global_val_id, a.global_value_name, a.global_value_desc, a.is_enabled, a.dflt_criteria_type
       FROM pay.pay_global_values_hdr a " +
                      "WHERE ((a.global_value_name ilike '" + searchWord.Replace("'", "''") +
       "') AND (org_id = " + orgID + ")) ORDER BY a.global_val_id DESC LIMIT " + limit_size +
       " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
      }
      else if (searchIn == "Global Value Description")
      {
        strSql = @"SELECT a.global_val_id, a.global_value_name, a.global_value_desc, a.is_enabled, a.dflt_criteria_type
       FROM pay.pay_global_values_hdr a " +
      "WHERE ((a.global_value_desc ilike '" + searchWord.Replace("'", "''") +
       "') AND (org_id = " + orgID + ")) ORDER BY a.global_val_id DESC LIMIT " + limit_size +
       " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
      }
      Global.mnFrm.gbv_SQL = strSql;
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      return dtst;
    }

    public static long get_Total_GBV(string searchWord, string searchIn, int orgID)
    {
      string strSql = "";
      if (searchIn == "Global Value Name")
      {
        strSql = "SELECT count(1) " +
        "FROM pay.pay_global_values_hdr a " +
        "WHERE ((a.global_value_name ilike '" + searchWord.Replace("'", "''") +
         "') AND (org_id = " + orgID + "))";
      }
      else if (searchIn == "Global Value Description")
      {
        strSql = "SELECT count(1)  " +
        "FROM pay.pay_global_values_hdr a " +
        "WHERE ((a.global_value_desc ilike '" + searchWord.Replace("'", "''") +
         "') AND (org_id = " + orgID + "))";
      }
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return 0;
      }
    }

    public static string get_GBV_Rec_Hstry(int hdrID)
    {
      string strSQL = @"SELECT a.created_by, 
to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS'), 
      a.last_update_by, 
      to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS') " +
      "FROM pay.pay_global_values_hdr a WHERE(a.global_val_id = " + hdrID + ")";
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

    public static string get_GBVDT_Rec_Hstry(int dteID)
    {
      string strSQL = @"SELECT a.created_by, 
to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS'), 
      a.last_update_by, 
      to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS') " +
      "FROM pay.pay_global_values_det a WHERE(a.value_det_id = " + dteID + ")";
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

    public static void deleteGBV(long hdrID, string gbvNm)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Global Value Name = " + gbvNm;
      string delSQL = "DELETE FROM pay.pay_global_values_det WHERE global_value_hdr_id = " + hdrID;
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);

      delSQL = "DELETE FROM pay.pay_global_values_hdr WHERE global_val_id = " + hdrID;
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }

    public static void deleteGBVLn(long gbvLnid, string critrNm)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Global Value Line Name = " + critrNm;
      string delSQL = "DELETE FROM pay.pay_global_values_det WHERE value_det_id = " + gbvLnid;
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }

    public static int getGBVID(string gbvname, int orgid)
    {
      DataSet dtSt = new DataSet();
      string sqlStr = "select global_val_id from pay.pay_global_values_hdr where lower(global_value_name) = '" +
       gbvname.Replace("'", "''").ToLower() + "' and org_id = " + orgid;
      dtSt = Global.mnFrm.cmCde.selectDataNoParams(sqlStr);
      if (dtSt.Tables[0].Rows.Count > 0)
      {
        return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return -1;
      }
    }

    public static void createGBVHdr(int orgid, string gbvname,
  string gbvdesc, string dfltCrtria, bool isEnbld)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = @"INSERT INTO pay.pay_global_values_hdr(
            global_value_name, global_value_desc, is_enabled, 
            dflt_criteria_type, created_by, creation_date, last_update_by, 
            last_update_date, org_id) " +
            "VALUES ('" + gbvname.Replace("'", "''") +
            "', '" + gbvdesc.Replace("'", "''") +
            "', '" +
            Global.mnFrm.cmCde.cnvrtBoolToBitStr(isEnbld) +
            "', '" + dfltCrtria.Replace("'", "''") +
            "', " + Global.myPay.user_id + ", '" + dateStr +
            "', " + Global.myPay.user_id + ", '" + dateStr +
            "', " + orgid + ")";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void updateGBVHdr(int gbvHdrId, string gbvname,
  string gbvdesc, string dfltCrtria, bool isEnbld)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE pay.pay_global_values_hdr SET " +
            "global_value_name='" + gbvname.Replace("'", "''") +
            "', global_value_desc='" + gbvdesc.Replace("'", "''") +
            "', dflt_criteria_type='" + dfltCrtria.Replace("'", "''") +
            "', last_update_by=" + Global.myPay.user_id + ", " +
            "last_update_date='" + dateStr +
            "', is_enabled='" +
            Global.mnFrm.cmCde.cnvrtBoolToBitStr(isEnbld) +
            "' " +
            "WHERE (global_val_id =" + gbvHdrId + ")";
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static long getGBVLnID(int gbvhdrid, int crtriaID,
 string crtriaTyp, string startDte)
    {
      startDte = DateTime.ParseExact(
   startDte, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string strSQL = @"SELECT value_det_id FROM pay.pay_global_values_det WHERE
            global_value_hdr_id = " + gbvhdrid + " and criteria_val_id=" + crtriaID +
            " and criteria_type='" + crtriaTyp.Replace("'", "''") +
            "' and valid_start_date='" + startDte.Replace("'", "''") +
            "'";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSQL);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return -1;
    }

    public static void createGBVLn(int gbvhdrid, int crtriaID,
  string crtriaTyp, string startDte, string endDte, double amnt)
    {
      startDte = DateTime.ParseExact(
   startDte, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

      endDte = DateTime.ParseExact(
   endDte, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = @"INSERT INTO pay.pay_global_values_det(
            global_value_hdr_id, criteria_val_id, criteria_type, 
            num_value, valid_start_date, valid_end_date, created_by, creation_date, 
            last_update_by, last_update_date) " +
            "VALUES (" + gbvhdrid + ", " + crtriaID + ", '" + crtriaTyp.Replace("'", "''") +
            "', " + amnt + ", '" + startDte.Replace("'", "''") +
            "', '" + endDte.Replace("'", "''") +
            "', " + Global.myPay.user_id + ", '" + dateStr +
            "', " + Global.myPay.user_id + ", '" + dateStr +
            "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void updateGBVLn(int gbvlnrid, int crtriaID,
  string crtriaTyp, string startDte, string endDte, double amnt)
    {
      startDte = DateTime.ParseExact(
   startDte, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

      endDte = DateTime.ParseExact(
   endDte, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = @"UPDATE pay.pay_global_values_det SET 
            criteria_val_id=" + crtriaID + ", num_value=" + amnt +
            ", valid_start_date='" + startDte.Replace("'", "''") +
            "', valid_end_date='" + endDte.Replace("'", "''") +
            "', criteria_type='" + crtriaTyp.Replace("'", "''") + "', last_update_by=" + Global.myPay.user_id +
            ", last_update_date='" + dateStr +
            "' " +
            "WHERE value_det_id=" + gbvlnrid + " ";
      Global.mnFrm.cmCde.updateDataNoParams(insSQL);
    }

    #endregion

    #region "PERSON SETS..."
    public static DataSet get_One_PrsStDet(int prsStID, long offset, int limit_size)
    {
      string prsSQL = Global.mnFrm.cmCde.getPrsStSQL(prsStID);
      string strSql = "";
      string mnlSQL = "";
      mnlSQL = "Select distinct a.person_id, a.local_id_no, trim(a.title || ' ' || a.sur_name || " +
         "', ' || a.first_name || ' ' || a.other_names) full_name, b.prsn_set_det_id " +
        "from prs.prsn_names_nos a, pay.pay_prsn_sets_det b " +
        "WHERE ((a.person_id = b.person_id) and (b.prsn_set_hdr_id = " + prsStID +
        " )) ORDER BY a.local_id_no DESC LIMIT " + limit_size +
         " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
      strSql = "select * from (" + prsSQL + ") tbl1 ORDER BY tbl1.local_id_no DESC LIMIT " + limit_size +
       " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
      if (prsSQL == "")
      {
        strSql = mnlSQL;
      }

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      Global.mnFrm.prsdet_SQL = strSql;
      return dtst;
    }

    public static DataSet get_AllPrsStDet(int prsStID)
    {
      string prsSQL = Global.mnFrm.cmCde.getPrsStSQL(prsStID);
      string strSql = "";
      string mnlSQL = "";
      mnlSQL = "Select distinct a.person_id, a.local_id_no, trim(a.title || ' ' || a.sur_name || " +
         "', ' || a.first_name || ' ' || a.other_names) full_name, b.prsn_set_det_id " +
        "from prs.prsn_names_nos a, pay.pay_prsn_sets_det b " +
        "WHERE ((a.person_id = b.person_id) and (b.prsn_set_hdr_id = " + prsStID +
        " )) ORDER BY a.local_id_no";
      strSql = "select * from (" + prsSQL + ") tbl1 ORDER BY tbl1.local_id_no";
      if (prsSQL == "")
      {
        strSql = mnlSQL;
      }
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      return dtst;
    }

    public static long get_Total_PrsDet(int prsStID)
    {
      string prsSQL = Global.mnFrm.cmCde.getPrsStSQL(prsStID);
      string strSql = "";
      string mnlSQL = "";
      mnlSQL = "Select count(distinct a.person_id) " +
        "from prs.prsn_names_nos a, pay.pay_prsn_sets_det b " +
        "WHERE ((a.person_id = b.person_id) and (b.prsn_set_hdr_id = " + prsStID +
        " ))";
      strSql = "select count(distinct tbl1.person_id) from (" + prsSQL + ") tbl1";
      if (prsSQL == "")
      {
        strSql = mnlSQL;
      }

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return 0;
      }
    }

    public static DataSet get_Basic_PrsSt(string searchWord, string searchIn,
  Int64 offset, int limit_size, int orgID)
    {
      string strSql = "";
      if (searchIn == "Person Set Name")
      {
        strSql = "SELECT a.prsn_set_hdr_id, a.prsn_set_hdr_name, a.prsn_set_hdr_desc, a.is_enabled, a.sql_query, a.is_default, a.uses_sql " +
                      "FROM pay.pay_prsn_sets_hdr a " +
                      "WHERE ((a.prsn_set_hdr_name ilike '" + searchWord.Replace("'", "''") +
       "') AND (org_id = " + orgID + ")) ORDER BY a.prsn_set_hdr_id DESC LIMIT " + limit_size +
       " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
      }
      else if (searchIn == "Person Set Description")
      {
        strSql = "SELECT a.prsn_set_hdr_id, a.prsn_set_hdr_name, a.prsn_set_hdr_desc, a.is_enabled, a.sql_query, a.is_default, a.uses_sql " +
      "FROM pay.pay_prsn_sets_hdr a " +
      "WHERE ((a.prsn_set_hdr_desc ilike '" + searchWord.Replace("'", "''") +
       "') AND (org_id = " + orgID + ")) ORDER BY a.prsn_set_hdr_id DESC LIMIT " + limit_size +
       " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
      }
      Global.mnFrm.prsst_SQL = strSql;
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      return dtst;
    }

    public static long get_Total_PrsSt(string searchWord, string searchIn, int orgID)
    {
      string strSql = "";
      if (searchIn == "Person Set Name")
      {
        strSql = "SELECT count(1) " +
        "FROM pay.pay_prsn_sets_hdr a " +
        "WHERE ((a.prsn_set_hdr_name ilike '" + searchWord.Replace("'", "''") +
         "') AND (org_id = " + orgID + "))";
      }
      else if (searchIn == "Person Set Description")
      {
        strSql = "SELECT count(1)  " +
        "FROM pay.pay_prsn_sets_hdr a " +
        "WHERE ((a.prsn_set_hdr_desc ilike '" + searchWord.Replace("'", "''") +
         "') AND (org_id = " + orgID + "))";
      }
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return 0;
      }
    }

    public static string get_PrsSt_Rec_Hstry(int hdrID)
    {
      string strSQL = @"SELECT a.created_by, 
      to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS'), 
      a.last_update_by, 
      to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS') " +
      "FROM pay.pay_prsn_sets_hdr a WHERE(a.prsn_set_hdr_id = " + hdrID + ")";
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

    #region "MASS PAY RUNS..."
    //public static double computeMathExprsn(string exprSn)
    //{
    //  string strSql = "";
    //  strSql = "SELECT " + exprSn.Replace("=", "").Replace(",", "").Replace("'", "''");

    //  DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams1(strSql);
    //  if (dtst.Tables.Count <= 0)
    //  {
    //    return 0;
    //  }
    //  else if (dtst.Tables[0].Rows.Count > 0)
    //  {
    //    return double.Parse(dtst.Tables[0].Rows[0][0].ToString());
    //  }
    //  return 0;
    //}

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

    public static long getNewMsPyID()
    {
      //string strSql = "select nextval('accb.accb_trnsctn_batches_batch_id_seq'::regclass);";
      string strSql = "select  last_value from pay.pay_mass_pay_run_hdr_mass_pay_id_seq";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString()) + 1;
      }
      return -1;
    }

    public static void createMsPayAtchdVal(long mspyid, long psrnID,
  long itmid, double amnt, long pssblvalid, string dteErnd)
    {

      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = @"INSERT INTO pay.pay_value_sets_det(
            mass_pay_id, person_id, item_id, value_to_use, 
            created_by, creation_date, last_update_by, last_update_date, 
            itm_pssbl_val_id, date_earned) " +
            "VALUES (" + mspyid + ", " + psrnID + ", " + itmid +
            ", " + amnt + ", " + Global.myPay.user_id + ", '" + dateStr +
            "', " + Global.myPay.user_id + ", '" + dateStr +
            "', " + pssblvalid + ", '" + dteErnd + "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void updtMsPayAtchdVal(long valstdetid, double amnt)
    {

      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = @"UPDATE pay.pay_value_sets_det SET 
            value_to_use = " + amnt + ", last_update_by= " + Global.myPay.user_id +
           ", last_update_date = '" + dateStr +
            "' WHERE value_set_det_id=" + valstdetid;
      Global.mnFrm.cmCde.updateDataNoParams(insSQL);
    }

    public static long doesAtchdValHvPrsn(long prsnid, long mspyid, long itmid, string dteEarned)
    {
      string selSQL = "SELECT value_set_det_id " +
                  "FROM pay.pay_value_sets_det WHERE ((person_id = " + prsnid +
                  ") and (mass_pay_id = " + mspyid + ") and (item_id = " + itmid + ") and date_earned='" + dteEarned + "')";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return -1;
      }
    }

    public static double getAtchdValPrsnAmnt(long prsnid, long mspyid, long itmid, ref string dteErnd)
    {
      string selSQL = "SELECT value_to_use, date_earned " +
                  "FROM pay.pay_value_sets_det WHERE ((person_id = " + prsnid +
                  ") and (mass_pay_id = " + mspyid + ") and (item_id = " + itmid + "))";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        dteErnd = dtst.Tables[0].Rows[0][1].ToString();
        return double.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return 0;
      }
    }

    public static DataSet getAtchdValPrsnAmnt(long prsnid, long mspyid, long itmid)
    {
      string selSQL = "SELECT value_to_use, date_earned " +
                  "FROM pay.pay_value_sets_det WHERE ((person_id = " + prsnid +
                  ") and (mass_pay_id = " + mspyid + ") and (item_id = " + itmid + "))";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      return dtst;
    }

    public static void deleteMsPayAtchdVal(long valLnid, string PrsnNm)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Person Name = " + PrsnNm;
      string delSQL = "DELETE FROM pay.pay_value_sets_det WHERE value_set_det_id = " + valLnid;
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }

    public static DataSet get_One_MsPayAtchdVals(string searchWord, string searchIn, long offset,
      int limit_size, long mspyID)
    {
      string strSql = "";
      string whrcls = "";

      if (searchIn == "Person Name/ID")
      {
        whrcls = " AND (prs.get_prsn_name(a.person_id) ilike '" + searchWord.Replace("'", "''") +
       "' or prs.get_prsn_loc_id(a.person_id) ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Item Name")
      {
        whrcls = " AND (org.get_payitm_nm(a.item_id) ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Item Value Name")
      {
        whrcls = " AND (org.get_payitm_valnm(a.itm_pssbl_val_id) ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Amount")
      {
        whrcls = " AND (trim(to_char(a.value_to_use, '9999999999999999999999999D99S')) ilike '" + searchWord.Replace("'", "''") +
       "')";
      }

      strSql = @"SELECT a.value_set_det_id, a.mass_pay_id, 
      a.person_id, prs.get_prsn_loc_id(a.person_id), prs.get_prsn_name(a.person_id), 
      a.item_id, org.get_payitm_nm(a.item_id),
      a.itm_pssbl_val_id, org.get_payitm_valnm(a.itm_pssbl_val_id), a.value_to_use
      FROM pay.pay_value_sets_det a " +
        "WHERE((a.mass_pay_id = " + mspyID + ")" + whrcls + ") ORDER BY 4, 1 LIMIT " + limit_size +
        " OFFSET " + (Math.Abs(offset * limit_size)).ToString();

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      Global.mnFrm.mspyAtchdVals_SQL1 = strSql;
      return dtst;
    }

    public static long get_Total_MsPayAtchdVals(
      string searchWord, string searchIn, long mspyID)
    {
      string strSql = "";
      string whrcls = "";

      if (searchIn == "Person Name/ID")
      {
        whrcls = " AND (prs.get_prsn_name(a.person_id) ilike '" + searchWord.Replace("'", "''") +
       "' or prs.get_prsn_loc_id(a.person_id) ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Item Name")
      {
        whrcls = " AND (org.get_payitm_nm(a.item_id) ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Item Value Name")
      {
        whrcls = " AND (org.get_payitm_valnm(a.itm_pssbl_val_id) ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Amount")
      {
        whrcls = " AND (trim(to_char(a.value_to_use, '9999999999999999999999999D99S')) ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      strSql = @"SELECT count(1)
  FROM pay.pay_value_sets_det a " +
        "WHERE((a.mass_pay_id = " + mspyID + ")" + whrcls + ")";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return 0;
      }
    }

    public static long get_Last_MsPyID(long prsnID, long itmSetID)
    {
      string strSql = @"SELECT z.mass_pay_id 
 FROM pay.pay_mass_pay_run_hdr z, pay.pay_itm_trnsctns a " +
       "WHERE(z.mass_pay_id = a.mass_pay_id and a.person_id = " + prsnID + " and z.itm_st_id =" + itmSetID + ") " +
       "ORDER BY z.mass_pay_trns_date DESC LIMIT 1 OFFSET 0 ";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      //Global.mnFrm.mspydt_SQL = strSql;
      return -1;
    }

    public static DataSet get_One_MsPyDet(long mspyid, long prsnID)
    {
      string whCls = "";
      if (prsnID > 0)
      {
        whCls = " and a.person_id = " + prsnID;
      }
      string strSql = @"SELECT a.pay_trns_id, a.person_id, a.item_id, a.amount_paid, 
to_char(to_timestamp(a.paymnt_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), a.paymnt_source, " +
            "a.pay_trns_type, a.pymnt_desc, -1, a.crncy_id, c.local_id_no, trim(c.title || ' ' || c.sur_name || " +
         @"', ' || c.first_name || ' ' || c.other_names) fullname, b.item_code_name, b.item_min_type, 
      org.get_grade_name(pasn.get_prsn_grdid(a.person_id)) grade_nm,
      org.get_job_name(pasn.get_prsn_jobid(a.person_id)) job_nm,
      org.get_pos_name(pasn.get_prsn_posid(a.person_id)) pos_nm,
      COALESCE(e.id_number,'-') ssnit_num,
      COALESCE(d.bank_name || ' (' || d.bank_branch || ')', '-') bank_brnch,
      COALESCE(d.account_number,'-') bank_acc_num
   FROM (pay.pay_itm_trnsctns a LEFT OUTER JOIN org.org_pay_items b ON a.item_id = b.item_id) 
   LEFT OUTER JOIN prs.prsn_names_nos c on a.person_id = c.person_id 
   LEFT OUTER JOIN pasn.prsn_bank_accounts d on a.person_id = d.person_id 
   LEFT OUTER JOIN prs.prsn_national_ids e on a.person_id = e.person_id and e.national_id_typ='SSNIT'
   WHERE(a.mass_pay_id = " + mspyid + " and b.item_value_uom ='Money'" + whCls + ") " +
   "ORDER BY c.local_id_no, b.report_line_no, b.item_min_type, b.pay_run_priority, a.pay_trns_id ";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      Global.mnFrm.mspydt_SQL = strSql;
      return dtst;
    }

    public static DataSet get_One_MsPyDetSmmry(long mspyid, long prsnID)
    {
      string whCls = "";
      if (prsnID > 0)
      {
        whCls = " and a.person_id = " + prsnID;
      }

      string strSql = @"SELECT -1, a.person_id, a.item_id, SUM(a.amount_paid), 
to_char(to_timestamp(a.paymnt_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY'), a.paymnt_source, " +
            "a.pay_trns_type, '', -1, a.crncy_id, c.local_id_no, trim(c.title || ' ' || c.sur_name || " +
         @"', ' || c.first_name || ' ' || c.other_names) fullname, b.item_code_name, b.item_min_type, 
      org.get_grade_name(pasn.get_prsn_grdid(a.person_id)) grade_nm,
      org.get_job_name(pasn.get_prsn_jobid(a.person_id)) job_nm,
      org.get_pos_name(pasn.get_prsn_posid(a.person_id)) pos_nm,
      COALESCE(e.id_number,'-') ssnit_num,
      COALESCE(d.bank_name || ' (' || d.bank_branch || ')', '-') bank_brnch,
      COALESCE(d.account_number,'-') bank_acc_num, b.report_line_no, b.pay_run_priority, 
      substring(b.local_classfctn from position('.' in b.local_classfctn) + 1) clsfctn 
   FROM (pay.pay_itm_trnsctns a LEFT OUTER JOIN org.org_pay_items b ON a.item_id = b.item_id) 
   LEFT OUTER JOIN prs.prsn_names_nos c on a.person_id = c.person_id 
   LEFT OUTER JOIN pasn.prsn_bank_accounts d on a.person_id = d.person_id 
   LEFT OUTER JOIN prs.prsn_national_ids e on a.person_id = e.person_id and e.national_id_typ='SSNIT'
   WHERE(a.amount_paid>=0 and a.mass_pay_id = " + mspyid + " and b.item_value_uom ='Money'" + whCls + ") " +
   @"GROUP BY 1,2,3,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23
   ORDER BY c.local_id_no, b.report_line_no, b.item_min_type, b.pay_run_priority";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      //Global.mnFrm.mspydt_SQL = strSql;
      return dtst;
    }

    public static DataSet get_One_MsPyDet(long offset, int limit_size, long mspyid)
    {
      string strSql = @"SELECT a.pay_trns_id, a.person_id, a.item_id, a.amount_paid, 
to_char(to_timestamp(a.paymnt_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS')
, a.paymnt_source, " +
            "a.pay_trns_type, a.pymnt_desc, -1, a.crncy_id, c.local_id_no, trim(c.title || ' ' || c.sur_name || " +
         "', ' || c.first_name || ' ' || c.other_names) fullname, b.item_code_name, a.pymnt_vldty_status " +
       "FROM (pay.pay_itm_trnsctns a LEFT OUTER JOIN org.org_pay_items b ON a.item_id = b.item_id) " +
       "LEFT OUTER JOIN prs.prsn_names_nos c on a.person_id = c.person_id " +
       "WHERE(a.mass_pay_id = " + mspyid + ") ORDER BY a.pay_trns_id LIMIT " + limit_size +
          " OFFSET " + (Math.Abs(offset * limit_size)).ToString();

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      Global.mnFrm.mspydt_SQL = strSql;
      return dtst;
    }

    public static DataSet getMsPyToRllBck(long mspyid)
    {
      string strSql = @"SELECT a.pay_trns_id, a.person_id, a.item_id, a.amount_paid, 
to_char(to_timestamp(a.paymnt_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), a.paymnt_source, " +
            "a.pay_trns_type, a.pymnt_desc, -1, a.crncy_id, c.local_id_no, trim(c.title || ' ' || c.sur_name || " +
         "', ' || c.first_name || ' ' || c.other_names) fullname, b.item_code_name, b.item_value_uom, b.item_maj_type, b.item_min_type " +
       "FROM (pay.pay_itm_trnsctns a LEFT OUTER JOIN org.org_pay_items b ON a.item_id = b.item_id) " +
       "LEFT OUTER JOIN prs.prsn_names_nos c on a.person_id = c.person_id " +
       "WHERE(a.mass_pay_id = " + mspyid + ") ORDER BY a.pay_trns_id ";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      return dtst;
    }

    public static long get_Total_MsPyDt(long mspyid)
    {
      string strSql = "";
      strSql = "SELECT count(1) " +
    "FROM (pay.pay_itm_trnsctns a LEFT OUTER JOIN org.org_pay_items b ON a.item_id = b.item_id) " +
       "LEFT OUTER JOIN prs.prsn_names_nos c on a.person_id = c.person_id " +
       "WHERE(a.mass_pay_id = " + mspyid + ")";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return 0;
      }
    }

    public static DataSet get_Basic_QuickPy(
    string searchWord, string searchIn,
    Int64 offset, int limit_size, long prsnID)
    {
      string strSql = "";
      if (searchIn == "Mass Pay Run Name")
      {
        strSql = @"SELECT a.mass_pay_id, CASE WHEN a.mass_pay_id<=0 THEN 'Manual/Direct Payment' ELSE a.mass_pay_name END, a.mass_pay_desc, a.run_status, 
        to_char(to_timestamp(a.mass_pay_trns_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), 
        a.prs_st_id, a.itm_st_id, a.sent_to_gl, a.gl_date " +
                      "FROM pay.pay_mass_pay_run_hdr a " +
                      "WHERE (((a.mass_pay_name ilike '" + searchWord.Replace("'", "''") +
       "' or a.mass_pay_id<=0)and (Select count(1) from pay.pay_itm_trnsctns z where z.person_id = " + prsnID +
       " and z.mass_pay_id = a.mass_pay_id)>=1) AND (org_id = " + Global.mnFrm.cmCde.Org_id
       + ") AND (prs_st_id<=0)) ORDER BY a.mass_pay_id DESC LIMIT " + limit_size +
       " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
      }
      else if (searchIn == "Mass Pay Run Description")
      {
        strSql = @"SELECT a.mass_pay_id, CASE WHEN a.mass_pay_id<=0 THEN 'Manual/Direct Payment' ELSE a.mass_pay_name END, a.mass_pay_desc, a.run_status, 
        to_char(to_timestamp(a.mass_pay_trns_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS')
      , a.prs_st_id, a.itm_st_id, a.sent_to_gl, to_char(to_timestamp(a.gl_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') " +
      "FROM pay.pay_mass_pay_run_hdr a " +
      "WHERE (((a.mass_pay_desc ilike '" + searchWord.Replace("'", "''") +
       "' or a.mass_pay_id<=0) and (Select count(1) from pay.pay_itm_trnsctns z where z.person_id = " + prsnID +
       " and z.mass_pay_id = a.mass_pay_id)>=1) AND (org_id = " + Global.mnFrm.cmCde.Org_id +
       ") AND (prs_st_id<=0)) ORDER BY a.mass_pay_id DESC LIMIT " + limit_size +
       " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
      }
      //Global.mnFrm.mspy_SQL = strSql;
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      return dtst;
    }

    public static DataSet get_Basic_MsPy(string searchWord, string searchIn,
  Int64 offset, int limit_size, int orgID, bool vwSelf)
    {
      string strSql = "";
      string extrWhr = "";
      if (vwSelf)
      {
        extrWhr = " and a.created_by=" + Global.mnFrm.cmCde.User_id;
      }
      if (searchIn == "Mass Pay Run Name")
      {
        strSql = @"SELECT a.mass_pay_id, a.mass_pay_name, a.mass_pay_desc, a.run_status, 
        to_char(to_timestamp(a.mass_pay_trns_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), a.prs_st_id, a.itm_st_id, a.sent_to_gl, a.gl_date " +
                      "FROM pay.pay_mass_pay_run_hdr a " +
                      "WHERE ((a.mass_pay_name ilike '" + searchWord.Replace("'", "''") +
       "') AND (org_id = " + orgID + ")" + extrWhr + ") ORDER BY a.mass_pay_id DESC LIMIT " + limit_size +
       " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
      }
      else if (searchIn == "Mass Pay Run Description")
      {
        strSql = @"SELECT a.mass_pay_id, a.mass_pay_name, a.mass_pay_desc, a.run_status, 
        to_char(to_timestamp(a.mass_pay_trns_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS')
, a.prs_st_id, a.itm_st_id, a.sent_to_gl, to_char(to_timestamp(a.gl_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') " +
      "FROM pay.pay_mass_pay_run_hdr a " +
      "WHERE ((a.mass_pay_desc ilike '" + searchWord.Replace("'", "''") +
       "') AND (org_id = " + orgID + ")" + extrWhr + ") ORDER BY a.mass_pay_id DESC LIMIT " + limit_size +
       " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
      }
      Global.mnFrm.mspy_SQL = strSql;
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      return dtst;
    }

    public static long get_Total_MsPy(string searchWord, string searchIn, int orgID, bool vwSelf)
    {
      string strSql = "";
      string extrWhr = "";
      if (vwSelf)
      {
        extrWhr = " and a.created_by=" + Global.mnFrm.cmCde.User_id;
      }
      if (searchIn == "Mass Pay Run Name")
      {
        strSql = "SELECT count(1) " +
        "FROM pay.pay_mass_pay_run_hdr a " +
        "WHERE ((a.mass_pay_name ilike '" + searchWord.Replace("'", "''") +
         "') AND (org_id = " + orgID + ")" + extrWhr + ")";
      }
      else if (searchIn == "Mass Pay Run Description")
      {
        strSql = "SELECT count(1)  " +
        "FROM pay.pay_mass_pay_run_hdr a " +
        "WHERE ((a.mass_pay_desc ilike '" + searchWord.Replace("'", "''") +
         "') AND (org_id = " + orgID + ")" + extrWhr + ")";
      }
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return 0;
      }
    }

    public static string get_MsPy_Rec_Hstry(long hdrID)
    {
      string strSQL = @"SELECT a.created_by, 
      to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS'), 
      a.last_update_by, 
      to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS') " +
      "FROM pay.pay_mass_pay_run_hdr a WHERE(a.mass_pay_id = " + hdrID + ")";
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

    public static DataSet get_BankAdvice(string msPyRunNm)
    {
      string strSql = @"SELECT row_number() OVER (ORDER BY tbl1.local_id_no) AS ""No.  ""
, tbl1.local_id_no ""ID No.        "", tbl1.fullname ""Full Name                     "", 
round(tbl1.total_earnings - tbl1.total_bills_charges- tbl1.total_deductions,2) ""Take Home   "", 
tbl2.bank_name || ' (' || tbl2.bank_branch || ')' ""Bank           "", tbl2.account_name || ' / ' || tbl2.account_number ||' / '||
tbl2.account_type ""Account          "", tbl2.net_pay_portion ||' ' || tbl2.portion_uom ""Portion    "", 
CASE WHEN portion_uom='Percent' THEN round(chartonumeric(to_char((net_pay_portion/100.00) * 
(tbl1.total_earnings - tbl1.total_bills_charges- tbl1.total_deductions),
'999999999999999999999999999999999999999999999D99')),2) 
 ELSE net_pay_portion END ""Amount to Transfer"" 
from pay.get_payment_summrys(" + Global.mnFrm.cmCde.Org_id + ",'" + msPyRunNm.Replace("'", "''") +
  @"','2') tbl1 
LEFT OUTER JOIN 
pasn.prsn_bank_accounts tbl2 ON (tbl1.person_id = tbl2.person_id and tbl2.net_pay_portion !=0) ORDER BY tbl1.local_id_no";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      return dtst;
    }

    public static DataSet get_PayRunSmmry(string msPyRunNm)
    {
      string strSql = @"SELECT row_number() OVER (ORDER BY tbl1.local_id_no) AS ""No.  ""
        , tbl1.local_id_no ""ID No.     "", tbl1.fullname ""Full Name             "", 
round(tbl1.total_earnings,2) ""Total Earnings "", 
round(tbl1.total_employer_charges,2)  ""Employer Charges"", 
round(tbl1.total_bills_charges+ tbl1.total_deductions,2) ""Deductions      "",
round(tbl1.total_earnings - tbl1.total_bills_charges- tbl1.total_deductions,2) ""Take Home       "" 
from pay.get_payment_summrys(" + Global.mnFrm.cmCde.Org_id + ",'" + msPyRunNm.Replace("'", "''") +
  @"','2') tbl1
        ORDER BY tbl1.local_id_no";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      return dtst;
    }

    #endregion

    #region "GL INTERFACE..."
    public static long getBatchID(string batchname, int orgid)
    {
      string strSql = "";
      strSql = "SELECT a.batch_id " +
   "FROM accb.accb_trnsctn_batches a " +
      "WHERE ((a.batch_name ilike '" + batchname.Replace("'", "''") +
        "') AND (a.org_id = " + orgid + "))";

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

    public static long getSimlrPstdBatchID(string orgnlbatchname, int orgid)
    {
      long srcbatchid = Global.getBatchID(orgnlbatchname, orgid);
      string strSql = "";
      strSql = "SELECT a.batch_id " +
   "FROM accb.accb_trnsctn_batches a " +
      "WHERE (((a.src_batch_id = " + srcbatchid.ToString() +
        ") or (a.batch_name ilike '" + orgnlbatchname.Replace("'", "''") +
        "' AND a.batch_vldty_status = 'VOID')) AND (a.org_id = " + orgid + "))";// AND (a.batch_status='1')

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

    public static DataSet get_WrongGLBatches(int orgID)
    {
      string strSql = "";
      strSql = @"select distinct batch_name, batch_id from (
SELECT b.transctn_id, c.batch_name, c.batch_id, b.trnsctn_date, b.source_trns_ids, 
d.accnt_id, d.accnt_name, b.dbt_amount, b.crdt_amount, COALESCE(round(SUM(a.dbt_amount),2),0), 
COALESCE(round(SUM(a.crdt_amount),2),0)
FROM pay.pay_gl_interface a, accb.accb_trnsctn_details b, accb.accb_trnsctn_batches c, accb.accb_chart_of_accnts d
WHERE (a.accnt_id = d.accnt_id and a.accnt_id = b.accnt_id and b.batch_id=c.batch_id and 
d.org_id=" + orgID + @" and c.batch_source ilike 'Inventory%'
and b.source_trns_ids like '%,' || a.interface_id || ',%') 
GROUP BY b.transctn_id, c.batch_name, c.batch_id, b.trnsctn_date, 
d.accnt_id, d.accnt_name, b.dbt_amount, b.crdt_amount
HAVING b.dbt_amount <> COALESCE(round(SUM(a.dbt_amount),2),0) or COALESCE(round(SUM(a.crdt_amount),2),0) <>  b.crdt_amount
) tbl1";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      return dtst;
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

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      return dtst;
    }

    public static void updateBatchVldtyStatus(long batchid, string vldty)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE accb.accb_trnsctn_batches " +
      "SET batch_vldty_status='" + vldty.Replace("'", "''") +
      "', last_update_by=" + Global.myPay.user_id +
      ", last_update_date='" + dateStr +
      "' WHERE batch_id = " + batchid;
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void updateBatchAvlblty(long batchid, string avlblty)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE accb.accb_trnsctn_batches " +
      "SET avlbl_for_postng='" + avlblty.Replace("'", "''") +
      "', last_update_by=" + Global.myPay.user_id +
      ", last_update_date='" + dateStr +
      "' WHERE batch_id = " + batchid;
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
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
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = "INSERT INTO accb.accb_trnsctn_details(" +
                        "accnt_id, transaction_desc, dbt_amount, trnsctn_date, " +
                        "func_cur_id, created_by, creation_date, batch_id, crdt_amount, " +
                        @"last_update_by, last_update_date, net_amount, 
            entered_amnt, entered_amt_crncy_id, accnt_crncy_amnt, accnt_crncy_id, 
            func_cur_exchng_rate, accnt_cur_exchng_rate, dbt_or_crdt) " +
                        "VALUES (" + accntid + ", '" + trnsDesc.Replace("'", "''") + "', " + dbtAmnt +
                        ", '" + trnsDate + "', " + crncyid + ", " + Global.myPay.user_id + ", '" + dateStr +
                        "', " + batchid + ", " + crdtamnt + ", " + Global.myPay.user_id +
                        ", '" + dateStr + "'," + netAmnt + ", " + entrdAmt +
                        ", " + entrdCurrID + ", " + acntAmnt +
                        ", " + acntCurrID + ", " + funcExchRate +
                        ", " + acntExchRate + ", '" + dbtOrCrdt + "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void updtBatchTrnsSrcIDs(long batchID)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string updtSQL = "UPDATE accb.accb_trnsctn_details SET source_trns_ids='' WHERE batch_id=" + batchID;
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void updtIntrfcTrnsSrcBatchIDs(long batchID)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string updtSQL = "UPDATE pay.pay_gl_interface SET gl_batch_id=-1 WHERE gl_batch_id=" + batchID;
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static string dbtOrCrdtAccnt(int accntid, string incrsDcrse)
    {
      string accntType = Global.mnFrm.cmCde.getAccntType(accntid);
      string isContra = Global.mnFrm.cmCde.isAccntContra(accntid);
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
      string accntType = Global.mnFrm.cmCde.getAccntType(accntid);
      string isContra = Global.mnFrm.cmCde.isAccntContra(accntid);
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

    public static void createBatch(int orgid, string batchname,
     string batchdesc, string btchsrc, string batchvldty, long srcbatchid, string avlblforPpstng)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = "INSERT INTO accb.accb_trnsctn_batches(" +
                        "batch_name, batch_description, created_by, creation_date, " +
                        "org_id, batch_status, last_update_by, last_update_date, " +
      "batch_source, batch_vldty_status, src_batch_id, avlbl_for_postng) " +
                        "VALUES ('" + batchname.Replace("'", "''") + "', '" + batchdesc.Replace("'", "''") +
                        "', " + Global.myPay.user_id + ", '" + dateStr +
                        "', " + orgid + ", '0', " + Global.myPay.user_id + ", '" + dateStr +
                        "', '" + btchsrc.Replace("'", "''") +
                        "', '" + batchvldty.Replace("'", "''") +
                        "', " + srcbatchid +
                        ",'" + avlblforPpstng + "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static bool isGLIntrfcBlcdOrg(int orgID, ref double dffrce)
    {
      string strSql = @"SELECT COALESCE(SUM(a.dbt_amount),0) dbt_sum, 
COALESCE(SUM(a.crdt_amount),0) crdt_sum " +
   "FROM pay.pay_gl_interface a, accb.accb_chart_of_accnts b " +
   "WHERE a.gl_batch_id = -1 and a.accnt_id = b.accnt_id and b.org_id=" + orgID +
      " ";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        dffrce = double.Parse(dtst.Tables[0].Rows[0][0].ToString()) -
    double.Parse(dtst.Tables[0].Rows[0][1].ToString());
        dffrce = Math.Round(dffrce, 2);

        try
        {
          if (dffrce == 0)
          {
            return true;
          }
          else
          {
            return false;
          }
        }
        catch (Exception ex)
        {
          return false;
        }
      }
      else
      {
        return false;
      }

    }

    public static bool isGLIntrfcBlcd(long py_trns_id)
    {
      string strSql = "SELECT SUM(a.dbt_amount) dbt_sum, " +
      "SUM(a.crdt_amount) crdt_sum " +
      "FROM pay.pay_gl_interface a " +
      "WHERE a.gl_batch_id = -1 and a.source_trns_id IN " +
      "(select b.pay_trns_id from pay.pay_itm_trnsctns b where b.pay_trns_id = " + py_trns_id + ")";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        try
        {
          if (double.Parse(dtst.Tables[0].Rows[0][0].ToString()) ==
            double.Parse(dtst.Tables[0].Rows[0][1].ToString()))
          {
            return true;
          }
          else
          {
            return false;
          }
        }
        catch (Exception ex)
        {
          return false;
        }
      }
      else
      {
        return false;
      }

    }

    public static bool isMsPyGLIntrfcBlcd(long mspyid)
    {
      string strSql = "SELECT SUM(a.dbt_amount) dbt_sum, " +
      "SUM(a.crdt_amount) crdt_sum " +
      "FROM pay.pay_gl_interface a " +
      "WHERE a.gl_batch_id = -1 and a.source_trns_id IN " +
      "(select b.pay_trns_id from pay.pay_itm_trnsctns b where b.mass_pay_id = " + mspyid + ")";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        try
        {
          if (double.Parse(dtst.Tables[0].Rows[0][0].ToString()) ==
            double.Parse(dtst.Tables[0].Rows[0][1].ToString()))
          {
            return true;
          }
          else
          {
            return false;
          }
        }
        catch (Exception ex)
        {
          return false;
        }
      }
      else
      {
        return false;
      }

    }

    //public static string getIntfcTrnsGlBtchID()
    //{
    //  string strSQL = " and EXISTS(select f.transctn_id from accb.accb_trnsctn_details f " +
    //  "where f.batch_id = " + glbatchid + " " +
    //  "and f.source_trns_ids like '%,' || a.interface_id || ',%' and " +
    //  "f.trnsctn_date=a.trnsctn_date and f.accnt_id= a.accnt_id)";
    //}

    public static DataSet getAllInGLIntrfcOrg(int orgID)
    {
      string strSql = @"SELECT a.accnt_id, 
      to_char(to_timestamp(a.trnsctn_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS')
      , SUM(a.dbt_amount) dbt_sum, " +
      "SUM(a.crdt_amount) crdt_sum, SUM(a.net_amount) net_sum, a.func_cur_id " +
      "FROM pay.pay_gl_interface a, accb.accb_chart_of_accnts b " +
      "WHERE a.gl_batch_id = -1 and a.accnt_id = b.accnt_id and b.org_id=" + orgID +
      " " +
      "GROUP BY a.accnt_id, a.trnsctn_date, func_cur_id " +
      "ORDER BY to_timestamp(a.trnsctn_date,'YYYY-MM-DD HH24:MI:SS')";
      /*and NOT EXISTS(select f.transctn_id from accb.accb_trnsctn_details f " +
      "where f.batch_id IN (select g.batch_id from accb.accb_trnsctn_batches g " +
      "where g.batch_name ilike '%Internal Payments%' and " +
      "to_timestamp(g.creation_date,'YYYY-MM-DD HH24:MI:SS') between " +
      "(to_timestamp(a.trnsctn_date,'YYYY-MM-DD HH24:MI:SS') - interval '6 months') " +
      "and (to_timestamp(a.trnsctn_date,'YYYY-MM-DD HH24:MI:SS') + interval '6 months')) " +
      "and f.source_trns_ids like '%,' || a.interface_id || ',%' and " +
      "f.trnsctn_date=a.trnsctn_date and f.accnt_id= a.accnt_id)*/
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      return dtst;
    }

    public static DataSet getAllInGLIntrfc(long py_trns_id)
    {
      string strSql = @"SELECT a.accnt_id, 
      to_char(to_timestamp(a.trnsctn_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS')
, SUM(a.dbt_amount) dbt_sum, " +
      "SUM(a.crdt_amount) crdt_sum, SUM(a.net_amount) net_sum, a.func_cur_id " +
      "FROM pay.pay_gl_interface a " +
      "WHERE a.gl_batch_id = -1 and a.source_trns_id IN " +
      "(select b.pay_trns_id from pay.pay_itm_trnsctns b where b.pay_trns_id = " + py_trns_id +
      ") and NOT EXISTS(select f.transctn_id from accb.accb_trnsctn_details f " +
      "where f.batch_id IN (select g.batch_id from accb.accb_trnsctn_batches g " +
      "where g.batch_name ilike '%Internal Payments%' and " +
      "to_timestamp(g.creation_date,'YYYY-MM-DD HH24:MI:SS') between " +
      "(to_timestamp(a.trnsctn_date,'YYYY-MM-DD HH24:MI:SS') - interval '6 months') " +
      "and (to_timestamp(a.trnsctn_date,'YYYY-MM-DD HH24:MI:SS') + interval '6 months')) " +
      "and f.source_trns_ids like '%,' || a.interface_id || ',%' and " +
      "f.trnsctn_date=a.trnsctn_date and f.accnt_id= a.accnt_id) " +
      "GROUP BY a.accnt_id, a.trnsctn_date, func_cur_id " +
      "ORDER BY to_timestamp(a.trnsctn_date,'YYYY-MM-DD HH24:MI:SS')";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      return dtst;
    }

    public static DataSet getAllInMsPyGLIntrfc(long mspyid)
    {
      string strSql = @"SELECT a.accnt_id, 
      to_char(to_timestamp(a.trnsctn_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS')
, SUM(a.dbt_amount) dbt_sum, " +
      "SUM(a.crdt_amount) crdt_sum, SUM(a.net_amount) net_sum, a.func_cur_id " +
      "FROM pay.pay_gl_interface a " +
      "WHERE a.gl_batch_id = -1 and a.source_trns_id IN " +
      "(select b.pay_trns_id from pay.pay_itm_trnsctns b where b.mass_pay_id = " +
      mspyid + ") and NOT EXISTS(select f.transctn_id from accb.accb_trnsctn_details f " +
      "where f.batch_id IN (select g.batch_id from accb.accb_trnsctn_batches g " +
      "where g.batch_name ilike '%Internal Payments%' and " +
      "to_timestamp(g.creation_date,'YYYY-MM-DD HH24:MI:SS') between " +
      "(to_timestamp(a.trnsctn_date,'YYYY-MM-DD HH24:MI:SS') - interval '6 months') " +
      "and (to_timestamp(a.trnsctn_date,'YYYY-MM-DD HH24:MI:SS') + interval '6 months')) " +
      "and f.source_trns_ids like '%,' || a.interface_id || ',%' and " +
      "f.trnsctn_date=a.trnsctn_date and f.accnt_id= a.accnt_id) " +
      "GROUP BY a.accnt_id, a.trnsctn_date, func_cur_id " +
      "ORDER BY to_timestamp(a.trnsctn_date,'YYYY-MM-DD HH24:MI:SS')";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      return dtst;
    }

    public static string getGLIntrfcIDs(int accntid, string trns_date, int crncy_id)
    {
      trns_date = DateTime.ParseExact(
   trns_date, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      string strSql = "select distinct a.interface_id from pay.pay_gl_interface a " +
      "where a.accnt_id = " + accntid + " and a.trnsctn_date = '" + trns_date +
      "' and a.func_cur_id = " + crncy_id + " and a.gl_batch_id = -1 " +
      "ORDER BY a.interface_id";
      /*and NOT EXISTS(select f.transctn_id from accb.accb_trnsctn_details f " +
      "where f.batch_id IN (select g.batch_id from accb.accb_trnsctn_batches g " +
      "where g.batch_name ilike '%Internal Payments%' and " +
      "to_timestamp(g.creation_date,'YYYY-MM-DD HH24:MI:SS') between " +
      "(to_timestamp(a.trnsctn_date,'YYYY-MM-DD HH24:MI:SS') - interval '6 months') " +
      "and (to_timestamp(a.trnsctn_date,'YYYY-MM-DD HH24:MI:SS') + interval '6 months')) " +
      "and f.source_trns_ids like '%,' || a.interface_id || ',%' and " +
      "f.trnsctn_date=a.trnsctn_date and f.accnt_id= a.accnt_id)*/
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      string infc_ids = ",";
      for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
      {
        infc_ids = infc_ids + dtst.Tables[0].Rows[a][0].ToString() + ",";
      }
      return infc_ids;
    }

    public static int get_Suspns_Accnt(int orgid)
    {
      string strSql = "";
      strSql = "SELECT a.accnt_id " +
        "FROM accb.accb_chart_of_accnts a " +
        "WHERE(a.is_suspens_accnt = '1' and a.org_id = " + orgid + ")";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count == 1)
      {
        return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return -1;
      }
    }

    //public static string dbtOrCrdtAccnt(int accntid, string incrsDcrse)
    //{
    //  string accntType = Global.mnFrm.cmCde.getAccntType(accntid);
    //  string isContra = Global.mnFrm.cmCde.isAccntContra(accntid);
    //  if (isContra == "0")
    //  {
    //    if ((accntType == "A" || accntType == "EX") && incrsDcrse == "I")
    //    {
    //      return "Debit";
    //    }
    //    else if ((accntType == "A" || accntType == "EX") && incrsDcrse == "D")
    //    {
    //      return "Credit";
    //    }
    //    else if ((accntType == "EQ" || accntType == "R" || accntType == "L") && incrsDcrse == "I")
    //    {
    //      return "Credit";
    //    }
    //    else if ((accntType == "EQ" || accntType == "R" || accntType == "L") && incrsDcrse == "D")
    //    {
    //      return "Debit";
    //    }
    //  }
    //  else
    //  {
    //    if ((accntType == "A" || accntType == "EX") && incrsDcrse == "I")
    //    {
    //      return "Credit";
    //    }
    //    else if ((accntType == "A" || accntType == "EX") && incrsDcrse == "D")
    //    {
    //      return "Debit";
    //    }
    //    else if ((accntType == "EQ" || accntType == "R" || accntType == "L") && incrsDcrse == "I")
    //    {
    //      return "Debit";
    //    }
    //    else if ((accntType == "EQ" || accntType == "R" || accntType == "L") && incrsDcrse == "D")
    //    {
    //      return "Credit";
    //    }
    //  }
    //  return "";
    //}

    //public static int dbtOrCrdtAccntMultiplier(int accntid, string incrsDcrse)
    //{
    //  string accntType = Global.mnFrm.cmCde.getAccntType(accntid);
    //  string isContra = Global.mnFrm.cmCde.isAccntContra(accntid);
    //  if (isContra == "0")
    //  {
    //    if ((accntType == "A" || accntType == "EX") && incrsDcrse == "I")
    //    {
    //      return 1;
    //    }
    //    else if ((accntType == "A" || accntType == "EX") && incrsDcrse == "D")
    //    {
    //      return -1;
    //    }
    //    else if ((accntType == "EQ" || accntType == "R" || accntType == "L") && incrsDcrse == "I")
    //    {
    //      return 1;
    //    }
    //    else if ((accntType == "EQ" || accntType == "R" || accntType == "L") && incrsDcrse == "D")
    //    {
    //      return -1;
    //    }
    //  }
    //  else
    //  {
    //    if ((accntType == "A" || accntType == "EX") && incrsDcrse == "I")
    //    {
    //      return -1;
    //    }
    //    else if ((accntType == "A" || accntType == "EX") && incrsDcrse == "D")
    //    {
    //      return 1;
    //    }
    //    else if ((accntType == "EQ" || accntType == "R" || accntType == "L") && incrsDcrse == "I")
    //    {
    //      return -1;
    //    }
    //    else if ((accntType == "EQ" || accntType == "R" || accntType == "L") && incrsDcrse == "D")
    //    {
    //      return 1;
    //    }
    //  }
    //  return 1;
    //}

    public static bool isGLIntrfcBlcdOrg(int orgID)
    {
      string strSql = @"SELECT COALESCE(SUM(a.dbt_amount),0) dbt_sum, 
COALESCE(SUM(a.crdt_amount),0) crdt_sum 
FROM pay.pay_gl_interface a, accb.accb_chart_of_accnts b 
WHERE a.gl_batch_id = -1 and a.accnt_id = b.accnt_id and b.org_id=" + orgID +
      " ";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        double dffrce = double.Parse(dtst.Tables[0].Rows[0][0].ToString()) -
    double.Parse(dtst.Tables[0].Rows[0][1].ToString());
        dffrce = Math.Round(dffrce, 2);
        try
        {
          if (dffrce == 0)
          {
            return true;
          }
          else
          {
            return false;
          }
        }
        catch (Exception ex)
        {
          return false;
        }
      }
      else
      {
        return false;
      }

    }

    public static void deleteGLInfcLine(long intfcID)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string delSQL = "DELETE FROM pay.pay_gl_interface WHERE interface_id = " +
        intfcID + " and gl_batch_id = -1";
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }

    public static double[] getGLIntrfcIDAmntSum(string intrfcids, int accntID)
    {
      double[] res = { 0, 0 };
      string strSql = @"SELECT COALESCE(SUM(a.dbt_amount),0), COALESCE(SUM(a.crdt_amount),0)
FROM pay.pay_gl_interface a
WHERE (a.accnt_id = " + accntID + @"
and '" + intrfcids + "' like '%,' || a.interface_id || ',%') ";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);

      if (dtst.Tables[0].Rows.Count > 0)
      {
        res[0] = double.Parse(dtst.Tables[0].Rows[0][0].ToString());
        res[1] = double.Parse(dtst.Tables[0].Rows[0][1].ToString());
      }
      return res;
    }

    public static string getGLIntrfcIDsMnl(int accntid, string trns_date, int crncy_id, long py_trns_id)
    {
      trns_date = DateTime.ParseExact(
   trns_date, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      string strSql = "select distinct a.interface_id from pay.pay_gl_interface a " +
      "where a.accnt_id = " + accntid + " and a.trnsctn_date = '" + trns_date +
      "' and a.func_cur_id = " + crncy_id + " and a.gl_batch_id = -1 and a.source_trns_id IN " +
      "(select b.pay_trns_id from pay.pay_itm_trnsctns b where b.pay_trns_id = " + py_trns_id +
      ") and NOT EXISTS(select f.transctn_id from accb.accb_trnsctn_details f " +
      "where f.batch_id IN (select g.batch_id from accb.accb_trnsctn_batches g " +
      "where g.batch_name ilike '%Internal Payments%' and " +
      "to_timestamp(g.creation_date,'YYYY-MM-DD HH24:MI:SS') between " +
      "(to_timestamp(a.trnsctn_date,'YYYY-MM-DD HH24:MI:SS') - interval '6 months') " +
      "and (to_timestamp(a.trnsctn_date,'YYYY-MM-DD HH24:MI:SS') + interval '6 months')) " +
      "and f.source_trns_ids like '%,' || a.interface_id || ',%' and " +
      "f.trnsctn_date=a.trnsctn_date and f.accnt_id= a.accnt_id) " +
      "ORDER BY a.interface_id";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      string infc_ids = ",";
      for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
      {
        infc_ids = infc_ids + dtst.Tables[0].Rows[a][0].ToString() + ",";
      }
      return infc_ids;
    }

    public static string getGLIntrfcIDsMsPy(int accntid, string trns_date, int crncy_id, long mspyid)
    {
      trns_date = DateTime.ParseExact(
   trns_date, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      string strSql = "select distinct a.interface_id from pay.pay_gl_interface a " +
      "where a.accnt_id = " + accntid + " and a.trnsctn_date = '" + trns_date +
      "' and a.func_cur_id = " + crncy_id + " and a.gl_batch_id = -1 and a.source_trns_id IN " +
      "(select b.pay_trns_id from pay.pay_itm_trnsctns b where b.mass_pay_id = " +
      mspyid + ") and NOT EXISTS(select f.transctn_id from accb.accb_trnsctn_details f " +
      "where f.batch_id IN (select g.batch_id from accb.accb_trnsctn_batches g " +
      "where g.batch_name ilike '%Internal Payments%' and " +
      "to_timestamp(g.creation_date,'YYYY-MM-DD HH24:MI:SS') between " +
      "(to_timestamp(a.trnsctn_date,'YYYY-MM-DD HH24:MI:SS') - interval '6 months') " +
      "and (to_timestamp(a.trnsctn_date,'YYYY-MM-DD HH24:MI:SS') + interval '6 months')) " +
      "and f.source_trns_ids like '%,' || a.interface_id || ',%' and " +
      "f.trnsctn_date=a.trnsctn_date and f.accnt_id= a.accnt_id) " +
      "ORDER BY a.interface_id";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      string infc_ids = ",";
      for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
      {
        infc_ids = infc_ids + dtst.Tables[0].Rows[a][0].ToString() + ",";
      }
      return infc_ids;
    }

    public static DataSet getDocGLInfcLns(long intrfcID)
    {
      string strSql = "SELECT * FROM pay.pay_gl_interface WHERE interface_id = " +
        intrfcID + "  and gl_batch_id != -1";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      return dtst;
    }

    public static DataSet getPymntGLInfcLns(long pyTrnsID)
    {
      string strSql = "SELECT * FROM pay.pay_gl_interface WHERE source_trns_id = " +
        pyTrnsID + "  and gl_batch_id != -1";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      return dtst;
    }
    public static void updtActnPrcss(int prcsID)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      Global.mnFrm.cmCde.ignorAdtTrail = true;
      string dtestr = Global.mnFrm.cmCde.getDB_Date_time();
      string strSql = @"UPDATE accb.accb_running_prcses SET
            last_active_time='" + dtestr + "' " +
            "WHERE which_process_is_rnng = " + prcsID + " ";
      Global.mnFrm.cmCde.updateDataNoParams(strSql);
      Global.mnFrm.cmCde.ignorAdtTrail = false;
    }

    public static bool isThereANActvActnPrcss(string prcsIDs, string prcsIntrvl)
    {
      string strSql = @"SELECT age(now(), to_timestamp(last_active_time,'YYYY-MM-DD HH24:MI:SS')) <= interval '" + prcsIntrvl +
        "' FROM accb.accb_running_prcses WHERE which_process_is_rnng IN (" + prcsIDs + ")";

      //Global.mnFrm.cmCde.showMsg(strSql, 0);
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return bool.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return false;
    }

    public static DataSet get_Infc_Trns(string searchWord, string searchIn,
  Int64 offset, int limit_size, int orgID, string dte1, string dte2,
      bool notgonetogl, bool imblcnTrns, bool usrTrns, decimal lowVal, decimal highVal)
    {
      dte1 = DateTime.ParseExact(
   dte1, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      dte2 = DateTime.ParseExact(
   dte2, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      string strSql = "";
      string to_gl = "";
      string imblnce_trns = "";
      string whereCls = "";
      string usrTrnsSql = "";
      string amntCls = "";
      if (lowVal != 0 || highVal != 0)
      {
        amntCls = " and ((dbt_amount !=0 and dbt_amount between " + lowVal + " and " + highVal +
          ") or (crdt_amount !=0 and crdt_amount between " + lowVal + " and " + highVal + "))";
      }
      if (usrTrns)
      {
        usrTrnsSql = " and trns_source !='SYS'";
      }

      if (imblcnTrns)
      {
        imblnce_trns = @" and (a.interface_id IN (select MAX(v.interface_id)
      from  pay.pay_gl_interface v
      group by abs(v.net_amount), v.source_trns_id
      having count(v.source_trns_id) %2 != 0 or v.source_trns_id<=0 or v.source_trns_id IS NULL 
      or (select CASE WHEN z.mass_pay_id<=0 THEN z.pay_trns_id  
      ELSE z.mass_pay_id END from pay.pay_itm_trnsctns z where z.pay_trns_id=v.source_trns_id) IS NULL))";

      }

      if (notgonetogl)
      {
        to_gl = " and (gl_batch_id <= 0)";
      }


      if (searchIn == "Account Name")
      {
        whereCls = "(b.accnt_name ilike '" + searchWord.Replace("'", "''") +
         "') and ";
      }
      else if (searchIn == "Account Number")
      {
        whereCls = "(b.accnt_num ilike '" + searchWord.Replace("'", "''") +
       "') and ";
      }
      else if (searchIn == "Source")
      {
        whereCls = "((select c.paymnt_source from pay.pay_itm_trnsctns c where c.pay_trns_id=a.source_trns_id) ilike '" + searchWord.Replace("'", "''") +
       "') and ";
      }
      else if (searchIn == "Transaction Date")
      {
        whereCls = "(to_char(to_timestamp(a.trnsctn_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') ilike '" + searchWord.Replace("'", "''") +
       "') and ";
      }
      else if (searchIn == "Transaction Description")
      {
        whereCls = "(a.transaction_desc ilike '" + searchWord.Replace("'", "''") +
    "') and ";
      }

      strSql = @"SELECT a.accnt_id, b.accnt_num, b.accnt_name, a.transaction_desc, 
to_char(to_timestamp(a.trnsctn_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS')
, a.dbt_amount, 
a.crdt_amount, a.source_trns_id, (select c.paymnt_source from pay.pay_itm_trnsctns c where c.pay_trns_id=a.source_trns_id) sourc, a.gl_batch_id, 
(select d.batch_name from accb.accb_trnsctn_batches d where d.batch_id = a.gl_batch_id) btch_nm, a.interface_id, a.func_cur_id, 
(select CASE WHEN f.mass_pay_id<=0 THEN trim(to_char(f.pay_trns_id,'9999999999999999999999999')) 
 ELSE (select g.mass_pay_name from pay.pay_mass_pay_run_hdr g where g.mass_pay_id = f.mass_pay_id) END from pay.pay_itm_trnsctns f where f.pay_trns_id=a.source_trns_id) sourc_doc_num
 FROM pay.pay_gl_interface a, accb.accb_chart_of_accnts b  
WHERE ((a.accnt_id = b.accnt_id) and " + whereCls + "(b.org_id = " + orgID + ")" + to_gl +
   imblnce_trns + usrTrnsSql + amntCls + " and (to_timestamp(a.trnsctn_date,'YYYY-MM-DD HH24:MI:SS') between to_timestamp('" + dte1 +
   "','YYYY-MM-DD HH24:MI:SS') AND to_timestamp('" + dte2 + "','YYYY-MM-DD HH24:MI:SS'))) " +
   "ORDER BY a.interface_id DESC LIMIT " + limit_size +
   " OFFSET " + (Math.Abs(offset * limit_size)).ToString();

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      Global.mnFrm.vwInfcSQLStmnt = strSql;
      return dtst;
    }

    public static long get_Total_Infc(string searchWord, string searchIn,
     int orgID, string dte1, string dte2, bool notgonetogl, bool imblcnTrns,
      bool usrTrns, decimal lowVal, decimal highVal)
    {
      dte1 = DateTime.ParseExact(
   dte1, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      dte2 = DateTime.ParseExact(
   dte2, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      string strSql = "";
      string to_gl = "";
      string imblnce_trns = "";
      string whereCls = "";
      string usrTrnsSql = "";
      string amntCls = "";
      if (lowVal != 0 || highVal != 0)
      {
        amntCls = " and ((dbt_amount !=0 and dbt_amount between " + lowVal + " and " + highVal +
          ") or (crdt_amount !=0 and crdt_amount between " + lowVal + " and " + highVal + "))";
      }
      if (usrTrns)
      {
        usrTrnsSql = " and trns_source !='SYS'";
      }

      if (imblcnTrns)
      {
        imblnce_trns = @" and (a.interface_id IN (select MAX(v.interface_id)
      from  pay.pay_gl_interface v
      group by abs(v.net_amount), v.source_trns_id
      having count(v.source_trns_id) %2 != 0 or v.source_trns_id<=0 or v.source_trns_id IS NULL 
      or (select CASE WHEN z.mass_pay_id<=0 THEN z.pay_trns_id  
      ELSE z.mass_pay_id END from pay.pay_itm_trnsctns z where z.pay_trns_id=v.source_trns_id) IS NULL))";
      }

      if (notgonetogl)
      {
        to_gl = " and (gl_batch_id <= 0)";
      }


      if (searchIn == "Account Name")
      {
        whereCls = "(b.accnt_name ilike '" + searchWord.Replace("'", "''") +
         "') and ";
      }
      else if (searchIn == "Account Number")
      {
        whereCls = "(b.accnt_num ilike '" + searchWord.Replace("'", "''") +
       "') and ";
      }
      else if (searchIn == "Source")
      {
        whereCls = "((select c.paymnt_source from pay.pay_itm_trnsctns c where c.pay_trns_id=a.source_trns_id) ilike '" + searchWord.Replace("'", "''") +
       "') and ";
      }
      else if (searchIn == "Transaction Date")
      {
        whereCls = "(to_char(to_timestamp(a.trnsctn_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') ilike '" + searchWord.Replace("'", "''") +
       "') and ";
      }
      else if (searchIn == "Transaction Description")
      {
        whereCls = "(a.transaction_desc ilike '" + searchWord.Replace("'", "''") +
    "') and ";
      }

      strSql = @"SELECT count(1) FROM pay.pay_gl_interface a, accb.accb_chart_of_accnts b " +
   "WHERE ((a.accnt_id = b.accnt_id) and " + whereCls + "(b.org_id = " + orgID + ")" + to_gl +
   imblnce_trns + usrTrnsSql + amntCls + " and (to_timestamp(a.trnsctn_date,'YYYY-MM-DD HH24:MI:SS') between to_timestamp('" + dte1 +
   "','YYYY-MM-DD HH24:MI:SS') AND to_timestamp('" + dte2 + "','YYYY-MM-DD HH24:MI:SS'))) ";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      long sumRes = 0;
      if (dtst.Tables[0].Rows.Count > 0)
      {
        long.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out sumRes);
      }
      return sumRes;
    }

    #endregion

    #region "BENEFITS & CONTRIBUTIONS DETAILS..."
    public static void updateItmVal(long pssblvalid, long itmid, double amnt, string sqlFormula,
    string valNm)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE org.org_pay_items_values " +
      "SET item_id=" + itmid + ", pssbl_amount=" + amnt +
       ", pssbl_value_sql='" + sqlFormula.Replace("'", "''") + "', " +
          "last_update_by=" + Global.myPay.user_id + ", last_update_date='" + dateStr + "', " +
          "pssbl_value_code_name='" + valNm.Replace("'", "''") + "' " +
    "WHERE pssbl_value_id = " + pssblvalid;
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void updateItmFeed(long feedid, long itmid, long balsItmID,
      string addSub, double scaleFctr)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE org.org_pay_itm_feeds " +
      "SET balance_item_id=" + balsItmID + ", fed_by_itm_id=" + itmid +
       ", adds_subtracts='" + addSub.Replace("'", "''") + "', " +
          "last_update_by=" + Global.myPay.user_id + ", last_update_date='" + dateStr + "', " +
          "scale_factor=" + scaleFctr +
    " WHERE feed_id = " + feedid;
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void clearTakeHomes()
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE org.org_pay_items SET is_take_home_pay = '0', last_update_by = " + Global.myPay.user_id + ", " +
               "last_update_date = '" + dateStr + "' " +
       "WHERE (is_take_home_pay = '1')";
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void updateItm(int orgid, long itmid, string itnm, string itmDesc,
     string itmMajTyp, string itmMinTyp, string itmUOMTyp,
     bool useSQL, bool isenbld, int costAcnt, int balsAcnt,
        string freqncy, string locClass, double priorty,
        string inc_dc_cost, string inc_dc_bals, string balstyp,
      int itmMnID, bool isRetro, int retroID, int invItmID, bool allwEdit, bool createsAcctng)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE org.org_pay_items " +
      "SET item_code_name='" + itnm.Replace("'", "''") + "', item_desc='" + itmDesc.Replace("'", "''") +
        "', item_maj_type='" + itmMajTyp.Replace("'", "''") + "', item_min_type='" + itmMinTyp.Replace("'", "''") +
        "', item_value_uom='" + itmUOMTyp.Replace("'", "''") +
        "', uses_sql_formulas='" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(useSQL) +
        "', cost_accnt_id=" + costAcnt +
        ", bals_accnt_id=" + balsAcnt + ", " +
              "is_enabled='" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(isenbld) +
        "', org_id=" + orgid +
        ", last_update_by=" + Global.myPay.user_id +
                        ", last_update_date='" + dateStr +
                        "', pay_frequency = '" + freqncy.Replace("'", "''") +
                        "', local_classfctn = '" + locClass.Replace("'", "''") +
                        "', pay_run_priority = " + priorty + ", incrs_dcrs_cost_acnt ='" + inc_dc_cost.Replace("'", "''") +
      "', incrs_dcrs_bals_acnt='" + inc_dc_bals.Replace("'", "''") +
      "', balance_type='" + balstyp.Replace("'", "''") +
      "', report_line_no= " + itmMnID +
   ", is_retro_element='" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(isRetro) +
        "', retro_item_id= " + retroID +
        ", inv_item_id= " + invItmID +
        ", allow_value_editing='" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(allwEdit) +
        "', creates_accounting='" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(createsAcctng) +
        "' WHERE item_id=" + itmid;
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void createItm(int orgid, string itnm, string itmDesc,
     string itmMajTyp, string itmMinTyp, string itmUOMTyp,
     bool useSQL, bool isenbld, int costAcnt, int balsAcnt,
        string freqncy, string locClass, double priorty,
        string inc_dc_cost, string inc_dc_bals, string balstyp, int itmMnID,
      bool isRetro, long retroID, long invItmID, bool allwEdit, bool createsAcctng)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = "INSERT INTO org.org_pay_items(" +
               "item_code_name, item_desc, item_maj_type, item_min_type, " +
               "item_value_uom, uses_sql_formulas, cost_accnt_id, bals_accnt_id, " +
               "is_enabled, org_id, created_by, creation_date, last_update_by, " +
               "last_update_date, pay_frequency, local_classfctn, pay_run_priority, " +
               "incrs_dcrs_cost_acnt, incrs_dcrs_bals_acnt, balance_type, report_line_no," +
               " is_retro_element,retro_item_id,inv_item_id, allow_value_editing, creates_accounting) " +
       "VALUES ('" + itnm.Replace("'", "''") + "', '" + itmDesc.Replace("'", "''") +
       "', '" + itmMajTyp.Replace("'", "''") + "', '" + itmMinTyp.Replace("'", "''") +
       "', '" + itmUOMTyp.Replace("'", "''") + "', '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(useSQL) + "', " + costAcnt +
       ", " + balsAcnt + ", '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(isenbld) +
       "', " + orgid + ", " + Global.myPay.user_id + ", '" + dateStr + "', " + Global.myPay.user_id +
       ", '" + dateStr + "', '" + freqncy.Replace("'", "''") + "', '" + locClass.Replace("'", "''") +
       "', " + priorty + ",'" + inc_dc_cost.Replace("'", "''") + "','" +
       inc_dc_bals.Replace("'", "''") + "','" + balstyp.Replace("'", "''") + "', " + itmMnID +
       ", '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(isRetro) +
        "', " + retroID + ", " + invItmID + ",'" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(allwEdit) +
        "','" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(createsAcctng) +
        "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void createItmVal(long itmid, double amnt, string sqlFormula,
    string valNm)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = "INSERT INTO org.org_pay_items_values(" +
               "item_id, pssbl_amount, pssbl_value_sql, created_by, " +
               "creation_date, last_update_by, last_update_date, pssbl_value_code_name) " +
       "VALUES (" + itmid + ", " + amnt +
       ", '" + sqlFormula.Replace("'", "''") + "', " + Global.myPay.user_id + ", '" + dateStr + "', " +
               Global.myPay.user_id + ", '" + dateStr + "', '" + valNm.Replace("'", "''") + "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void createItmFeed(long itmid, long balsItmID, string addSub, double scaleFctr)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = "INSERT INTO org.org_pay_itm_feeds(" +
             "balance_item_id, fed_by_itm_id, adds_subtracts, created_by, " +
             "creation_date, last_update_by, last_update_date, scale_factor) " +
       "VALUES (" + balsItmID + ", " + itmid +
       ", '" + addSub.Replace("'", "''") + "', " + Global.myPay.user_id + ", '" + dateStr + "', " +
               Global.myPay.user_id + ", '" + dateStr + "', " + scaleFctr + ")";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static bool doesItmFeedExists(long itmid, long blsItmID)
    {
      string selSQL = "SELECT a.feed_id " +
      "FROM org.org_pay_itm_feeds a WHERE ((a.fed_by_itm_id = " + itmid +
      ") and (a.balance_item_id = " + blsItmID +
      ")) ORDER BY a.feed_id ";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return true;
      }
      return false;
    }

    public static DataSet getAllItmFeeds(long offset, int limit_size, long itmid)
    {
      string selSQL = "SELECT balance_item_id, fed_by_itm_id, adds_subtracts, feed_id, scale_factor " +
      "FROM org.org_pay_itm_feeds WHERE ((balance_item_id = " + itmid +
      ") or (fed_by_itm_id = " + itmid + ")) ORDER BY feed_id LIMIT " + limit_size +
        " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      Global.mnFrm.itmFeed_SQL = selSQL;
      return dtst;
    }

    public static long get_Total_Feeds(long itmid)
    {
      string strSql = "";
      strSql = "SELECT count(1) " +
      "FROM org.org_pay_itm_feeds WHERE ((balance_item_id = " + itmid +
      ") or (fed_by_itm_id = " + itmid + "))";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return 0;
      }
    }

    public static DataSet getAllItmVals(long offset, int limit_size, long itmid)
    {
      string selSQL = "SELECT pssbl_value_id, pssbl_value_code_name, pssbl_amount, pssbl_value_sql, item_id " +
      "FROM org.org_pay_items_values WHERE ((item_id = " + itmid + ")) ORDER BY pssbl_value_id DESC LIMIT " + limit_size +
        " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      Global.mnFrm.itmPval_SQL = selSQL;
      return dtst;
    }

    public static long get_Total_Psbl_Vl(long itmID)
    {
      string strSql = "";
      strSql = "SELECT count(1) " +
      "FROM org.org_pay_items_values WHERE ((item_id = " + itmID + "))";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return 0;
      }
    }

    public static DataSet get_One_Itm_Det(long itmID)
    {
      string strSql = "";
      strSql = "SELECT a.item_id, a.item_code_name, " +
       "a.item_desc, a.item_maj_type, a.item_min_type, a.item_value_uom, " +
             "a.uses_sql_formulas, a.cost_accnt_id, a.bals_accnt_id, a.is_enabled, a.org_id, " +
                   @"a.pay_frequency, a.local_classfctn, a.pay_run_priority, a.incrs_dcrs_cost_acnt, 
  a.incrs_dcrs_bals_acnt, a.balance_type, a.is_retro_element, a.retro_item_id, a.inv_item_id, 
  a.allow_value_editing, a.creates_accounting " +
       "FROM org.org_pay_items a " +
       "WHERE(a.item_id = " + itmID + ")";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      //Global.mnFrm.grd_SQL = strSql;
      return dtst;
    }


    public static DataSet get_Basic_Itm(string searchWord, string searchIn,
     Int64 offset, int limit_size, int orgID)
    {
      string strSql = "";
      if (searchIn == "Item Name")
      {
        strSql = "SELECT a.item_id, a.item_code_name, a.item_maj_type " +
      "FROM org.org_pay_items a " +
      "WHERE ((a.item_code_name ilike '" + searchWord.Replace("'", "''") +
       "') AND (org_id = " + orgID + ")) ORDER BY a.pay_run_priority, 2 LIMIT " + limit_size +
       " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
      }
      else if (searchIn == "Item Description")
      {
        strSql = "SELECT a.item_id, a.item_code_name, a.item_maj_type " +
      "FROM org.org_pay_items a " +
      "WHERE ((a.item_desc ilike '" + searchWord.Replace("'", "''") +
            "' or a.local_classfctn ilike '" + searchWord.Replace("'", "''") +
            "') AND (org_id = " + orgID + ")) ORDER BY a.pay_run_priority, 2 LIMIT " + limit_size +
       " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
      }
      Global.mnFrm.itm_SQL = strSql;
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      return dtst;
    }

    public static long get_Total_Itm(string searchWord, string searchIn, int orgID)
    {
      string strSql = "";
      if (searchIn == "Item Name")
      {
        strSql = "SELECT count(1) " +
        "FROM org.org_pay_items a " +
        "WHERE ((a.item_code_name ilike '" + searchWord.Replace("'", "''") +
         "') AND (org_id = " + orgID + "))";
      }
      else if (searchIn == "Item Description")
      {
        strSql = "SELECT count(1)  " +
        "FROM org.org_pay_items a " +
        "WHERE ((a.item_desc ilike '" + searchWord.Replace("'", "''") +
         "' or a.local_classfctn ilike '" + searchWord.Replace("'", "''") +
            "') AND (org_id = " + orgID + "))";
      }
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return 0;
      }
    }

    public static string get_Itm_Rec_Hstry(int itmID)
    {
      string strSQL = @"SELECT a.created_by, 
to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS'), 
      a.last_update_by, 
      to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS') " +
      "FROM org.org_pay_items a WHERE(a.item_id = " + itmID + ")";
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

    public static string get_Pval_Rec_Hstry(long pvalID)
    {
      string strSQL = @"SELECT a.created_by, 
to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS'), 
      a.last_update_by, 
      to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS') " +
      "FROM org.org_pay_items_values a WHERE(a.pssbl_value_id = " + pvalID + ")";
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

    public static string get_Feed_Rec_Hstry(long feedID)
    {
      string strSQL = @"SELECT a.created_by, 
to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS'), 
      a.last_update_by, 
      to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS') " +
      "FROM org.org_pay_itm_feeds a WHERE(a.feed_id = " + feedID + ")";
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

    #region "PAYMENTS SEARCH..."
    public static DataSet get_Pay_Trns(string searchWord, string searchIn,
  Int64 offset, int limit_size, int orgID, string dte1, string dte2)
    {
      dte1 = DateTime.ParseExact(
   dte1, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      dte2 = DateTime.ParseExact(
   dte2, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      string strSql = "";
      string to_gl = "";
      //if (gonetogl)
      //{
      //  to_gl = " and (gl_batch_id > 0)";
      //}
      if (searchIn == "Person No.")
      {
        strSql = @"SELECT a.pay_trns_id, a.person_id, a.item_id, a.amount_paid, 
to_char(to_timestamp(a.paymnt_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS')
, a.paymnt_source, " +
            "a.pay_trns_type, a.pymnt_desc, -1, a.crncy_id, c.local_id_no, trim(c.title || ' ' || c.sur_name || " +
         "', ' || c.first_name || ' ' || c.other_names) fullname, b.item_code_name, a.pymnt_vldty_status " +
       "FROM (pay.pay_itm_trnsctns a LEFT OUTER JOIN org.org_pay_items b ON a.item_id = b.item_id) " +
       "LEFT OUTER JOIN prs.prsn_names_nos c on a.person_id = c.person_id " +
       "WHERE((c.local_id_no ilike '" + searchWord.Replace("'", "''") +
       "') and (b.org_id = " + orgID + ")" + to_gl + " and (to_timestamp(a.paymnt_date,'YYYY-MM-DD HH24:MI:SS') between to_timestamp('" + dte1 +
       "','YYYY-MM-DD HH24:MI:SS') AND to_timestamp('" + dte2 + "','YYYY-MM-DD HH24:MI:SS'))) " +
       "ORDER BY a.pay_trns_id DESC LIMIT " + limit_size +
         " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
      }
      else if (searchIn == "Person Name")
      {
        strSql = @"SELECT a.pay_trns_id, a.person_id, a.item_id, a.amount_paid, 
        to_char(to_timestamp(a.paymnt_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS')
, a.paymnt_source, " +
            "a.pay_trns_type, a.pymnt_desc, -1, a.crncy_id, c.local_id_no, trim(c.title || ' ' || c.sur_name || " +
         "', ' || c.first_name || ' ' || c.other_names) fullname, b.item_code_name, a.pymnt_vldty_status " +
       "FROM (pay.pay_itm_trnsctns a LEFT OUTER JOIN org.org_pay_items b ON a.item_id = b.item_id) " +
       "LEFT OUTER JOIN prs.prsn_names_nos c on a.person_id = c.person_id " +
       "WHERE((trim(c.title || ' ' || c.sur_name || " +
         "', ' || c.first_name || ' ' || c.other_names) ilike '" + searchWord.Replace("'", "''") +
       "') and (b.org_id = " + orgID + ")" + to_gl + " and (to_timestamp(a.paymnt_date,'YYYY-MM-DD HH24:MI:SS') between to_timestamp('" + dte1 +
       "','YYYY-MM-DD HH24:MI:SS') AND to_timestamp('" + dte2 + "','YYYY-MM-DD HH24:MI:SS'))) " +
       "ORDER BY a.pay_trns_id DESC LIMIT " + limit_size +
         " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
      }
      else if (searchIn == "Item Name")
      {
        strSql = @"SELECT a.pay_trns_id, a.person_id, a.item_id, a.amount_paid, 
to_char(to_timestamp(a.paymnt_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS')
, a.paymnt_source, " +
            "a.pay_trns_type, a.pymnt_desc, -1, a.crncy_id, c.local_id_no, trim(c.title || ' ' || c.sur_name || " +
         "', ' || c.first_name || ' ' || c.other_names) fullname, b.item_code_name, a.pymnt_vldty_status " +
       "FROM (pay.pay_itm_trnsctns a LEFT OUTER JOIN org.org_pay_items b ON a.item_id = b.item_id) " +
       "LEFT OUTER JOIN prs.prsn_names_nos c on a.person_id = c.person_id " +
       "WHERE((b.item_code_name ilike '" + searchWord.Replace("'", "''") +
       "') and (b.org_id = " + orgID + ")" + to_gl + " and (to_timestamp(a.paymnt_date,'YYYY-MM-DD HH24:MI:SS') between to_timestamp('" + dte1 +
       "','YYYY-MM-DD HH24:MI:SS') AND to_timestamp('" + dte2 + "','YYYY-MM-DD HH24:MI:SS'))) " +
       "ORDER BY a.pay_trns_id DESC LIMIT " + limit_size +
         " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
      }
      else if (searchIn == "Transaction Date")
      {
        strSql = @"SELECT a.pay_trns_id, a.person_id, a.item_id, a.amount_paid, 
to_char(to_timestamp(a.paymnt_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS')
, a.paymnt_source, " +
            "a.pay_trns_type, a.pymnt_desc, -1, a.crncy_id, c.local_id_no, trim(c.title || ' ' || c.sur_name || " +
         "', ' || c.first_name || ' ' || c.other_names) fullname, b.item_code_name, a.pymnt_vldty_status " +
       "FROM (pay.pay_itm_trnsctns a LEFT OUTER JOIN org.org_pay_items b ON a.item_id = b.item_id) " +
       "LEFT OUTER JOIN prs.prsn_names_nos c on a.person_id = c.person_id " +
       "WHERE((a.paymnt_date ilike '" + searchWord.Replace("'", "''") +
       "') and (b.org_id = " + orgID + ")" + to_gl + " and (to_timestamp(a.paymnt_date,'YYYY-MM-DD HH24:MI:SS') between to_timestamp('" + dte1 +
       "','YYYY-MM-DD HH24:MI:SS') AND to_timestamp('" + dte2 + "','YYYY-MM-DD HH24:MI:SS'))) " +
       "ORDER BY a.pay_trns_id DESC LIMIT " + limit_size +
         " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
      }
      else if (searchIn == "Transaction Description")
      {
        strSql = @"SELECT a.pay_trns_id, a.person_id, a.item_id, a.amount_paid, 
        to_char(to_timestamp(a.paymnt_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS')
        , a.paymnt_source, " +
            "a.pay_trns_type, a.pymnt_desc, -1, a.crncy_id, c.local_id_no, trim(c.title || ' ' || c.sur_name || " +
         "', ' || c.first_name || ' ' || c.other_names) fullname, b.item_code_name, a.pymnt_vldty_status " +
       "FROM (pay.pay_itm_trnsctns a LEFT OUTER JOIN org.org_pay_items b ON a.item_id = b.item_id) " +
       "LEFT OUTER JOIN prs.prsn_names_nos c on a.person_id = c.person_id " +
       "WHERE((a.pymnt_desc ilike '" + searchWord.Replace("'", "''") +
       "') and (b.org_id = " + orgID + ")" + to_gl + " and (to_timestamp(a.paymnt_date,'YYYY-MM-DD HH24:MI:SS') between to_timestamp('" + dte1 +
       "','YYYY-MM-DD HH24:MI:SS') AND to_timestamp('" + dte2 + "','YYYY-MM-DD HH24:MI:SS'))) " +
       "ORDER BY a.pay_trns_id DESC LIMIT " + limit_size +
         " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
      }
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      Global.mnFrm.trnsDet_SQL1 = strSql;
      return dtst;
    }

    public static long get_Total_Trns(string searchWord, string searchIn,
     int orgID, string dte1, string dte2)
    {
      dte1 = DateTime.ParseExact(
   dte1, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      dte2 = DateTime.ParseExact(
   dte2, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      string strSql = "";
      string to_gl = "";
      //if (gonetogl)
      //{
      //  to_gl = " and (gl_batch_id > 0)";
      //}
      if (searchIn == "Person No.")
      {
        strSql = "SELECT count(1) " +
       "FROM (pay.pay_itm_trnsctns a LEFT OUTER JOIN org.org_pay_items b ON a.item_id = b.item_id) " +
       "LEFT OUTER JOIN prs.prsn_names_nos c on a.person_id = c.person_id " +
       "WHERE((c.local_id_no ilike '" + searchWord.Replace("'", "''") +
       "') and (b.org_id = " + orgID + ")" + to_gl + " and (to_timestamp(a.paymnt_date,'YYYY-MM-DD HH24:MI:SS') between to_timestamp('" + dte1 +
       "','YYYY-MM-DD HH24:MI:SS') AND to_timestamp('" + dte2 + "','YYYY-MM-DD HH24:MI:SS'))) ";
      }
      else if (searchIn == "Person Name")
      {
        strSql = "SELECT count(1) " +
       "FROM (pay.pay_itm_trnsctns a LEFT OUTER JOIN org.org_pay_items b ON a.item_id = b.item_id) " +
       "LEFT OUTER JOIN prs.prsn_names_nos c on a.person_id = c.person_id " +
       "WHERE((trim(c.title || ' ' || c.sur_name || " +
         "', ' || c.first_name || ' ' || c.other_names) ilike '" + searchWord.Replace("'", "''") +
       "') and (b.org_id = " + orgID + ")" + to_gl + " and (to_timestamp(a.paymnt_date,'YYYY-MM-DD HH24:MI:SS') between to_timestamp('" + dte1 +
       "','YYYY-MM-DD HH24:MI:SS') AND to_timestamp('" + dte2 + "','YYYY-MM-DD HH24:MI:SS'))) ";
      }
      else if (searchIn == "Item Name")
      {
        strSql = "SELECT count(1) " +
       "FROM (pay.pay_itm_trnsctns a LEFT OUTER JOIN org.org_pay_items b ON a.item_id = b.item_id) " +
       "LEFT OUTER JOIN prs.prsn_names_nos c on a.person_id = c.person_id " +
       "WHERE((b.item_code_name ilike '" + searchWord.Replace("'", "''") +
       "') and (b.org_id = " + orgID + ")" + to_gl + " and (to_timestamp(a.paymnt_date,'YYYY-MM-DD HH24:MI:SS') between to_timestamp('" + dte1 +
       "','YYYY-MM-DD HH24:MI:SS') AND to_timestamp('" + dte2 + "','YYYY-MM-DD HH24:MI:SS'))) ";
      }
      else if (searchIn == "Transaction Date")
      {
        strSql = "SELECT count(1) " +
       "FROM (pay.pay_itm_trnsctns a LEFT OUTER JOIN org.org_pay_items b ON a.item_id = b.item_id) " +
       "LEFT OUTER JOIN prs.prsn_names_nos c on a.person_id = c.person_id " +
       @"WHERE((
to_char(to_timestamp(a.paymnt_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') ilike '" + searchWord.Replace("'", "''") +
       "') and (b.org_id = " + orgID + ")" + to_gl + " and (to_timestamp(a.paymnt_date,'YYYY-MM-DD HH24:MI:SS') between to_timestamp('" + dte1 +
       "','YYYY-MM-DD HH24:MI:SS') AND to_timestamp('" + dte2 + "','YYYY-MM-DD HH24:MI:SS'))) ";
      }
      else if (searchIn == "Transaction Description")
      {
        strSql = "SELECT count(1) " +
       "FROM (pay.pay_itm_trnsctns a LEFT OUTER JOIN org.org_pay_items b ON a.item_id = b.item_id) " +
       "LEFT OUTER JOIN prs.prsn_names_nos c on a.person_id = c.person_id " +
       "WHERE((a.pymnt_desc ilike '" + searchWord.Replace("'", "''") +
       "') and (b.org_id = " + orgID + ")" + to_gl + " and (to_timestamp(a.paymnt_date,'YYYY-MM-DD HH24:MI:SS') between to_timestamp('" + dte1 +
       "','YYYY-MM-DD HH24:MI:SS') AND to_timestamp('" + dte2 + "','YYYY-MM-DD HH24:MI:SS'))) ";
      }
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      long sumRes = 0;
      if (dtst.Tables[0].Rows.Count > 0)
      {
        long.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out sumRes);
      }
      return sumRes;
    }

    public static string get_GLBatch_Nm(long batchID)
    {
      string strSql = "";
      strSql = "SELECT a.batch_name " +
     "FROM accb.accb_trnsctn_batches a " +
     "WHERE(a.batch_id = " + batchID + ")";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return dtst.Tables[0].Rows[0][0].ToString();
      }
      else
      {
        return "";
      }
    }

    public static long getTodaysGLBatchID(string batchnm, int orgid)
    {
      string strSql = "";
      strSql = "SELECT a.batch_id " +
     "FROM accb.accb_trnsctn_batches a " +
     "WHERE(a.batch_name ilike '%" + batchnm.Replace("'", "''") +
     "%' and org_id = " + orgid + " and batch_status = '0')";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return -1;
      }
    }

    public static string get_InvItemNm(int itmID)
    {
      string strSql = "SELECT REPLACE(item_desc || ' (' || REPLACE(item_code,item_desc,'') || ')', ' ()','') " +
   "FROM inv.inv_itm_list a " +
   "WHERE item_id =" + itmID + "";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return dtst.Tables[0].Rows[0][0].ToString();
      }
      return "";
    }

    public static string get_PayItemNm(int itmID)
    {
      string strSql = "SELECT item_code_name " +
   "FROM org.org_pay_items a " +
   "WHERE item_id =" + itmID + "";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return dtst.Tables[0].Rows[0][0].ToString();
      }
      return "";
    }

    public static string[] get_ItmAccntInfo(long itmID)
    {
      string[] retSql = { "Q", "-123", "Q", "-123" };
      string strSql = "SELECT a.incrs_dcrs_cost_acnt, a.cost_accnt_id, a.incrs_dcrs_bals_acnt, a.bals_accnt_id " +
   "FROM org.org_pay_items a " +
   "WHERE(a.item_id = " + itmID + ")";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        retSql[0] = dtst.Tables[0].Rows[0][0].ToString();
        retSql[1] = dtst.Tables[0].Rows[0][1].ToString();
        retSql[2] = dtst.Tables[0].Rows[0][2].ToString();
        retSql[3] = dtst.Tables[0].Rows[0][3].ToString();
      }
      return retSql;
    }

    public static long getItmDailyBalsID(long balsItmID, string balsDate, long prsn_id)
    {
      balsDate = DateTime.ParseExact(
   balsDate, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      balsDate = balsDate.Substring(0, 10);
      string strSql = "";
      strSql = "SELECT a.bals_id " +
   "FROM pay.pay_balsitm_bals a " +
   "WHERE(to_timestamp(a.bals_date,'YYYY-MM-DD') =  to_timestamp('" + balsDate +
   "','YYYY-MM-DD') and a.bals_itm_id = " + balsItmID +
   " and a.person_id = " + prsn_id + ")";

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

    public static double getBlsItmDailyBals(long balsItmID, long prsn_id, string balsDate)
    {
      string orgnlDte = balsDate;
      balsDate = DateTime.ParseExact(
   balsDate, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      balsDate = balsDate.Substring(0, 10);
      double res = 0;
      string strSql = "";
      string usesSQL = Global.mnFrm.cmCde.getGnrlRecNm("org.org_pay_items",
        "item_id", "uses_sql_formulas", balsItmID);
      if (usesSQL != "1")
      {
        strSql = "SELECT a.bals_amount " +
      "FROM pay.pay_balsitm_bals a " +
      "WHERE(to_timestamp(a.bals_date,'YYYY-MM-DD') =  to_timestamp('" + balsDate +
      "','YYYY-MM-DD') and a.bals_itm_id = " + balsItmID + " and a.person_id = " + prsn_id + ")";

        DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
        if (dtst.Tables[0].Rows.Count > 0)
        {
          double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out res);
        }
      }
      else
      {
        string valSQL = Global.mnFrm.cmCde.getItmValSQL(Global.getPrsnItmVlID(prsn_id, balsItmID, orgnlDte));
        if (valSQL == "")
        {
        }
        else
        {
          try
          {
            res = Global.mnFrm.cmCde.exctItmValSQL(
              valSQL, prsn_id,
              Global.mnFrm.cmCde.Org_id, balsDate);
          }
          catch (Exception ex)
          {
          }
        }
      }
      return res;
    }

    public static double getBlsItmLtstDailyBals(long balsItmID, long prsn_id, string balsDate)
    {
      string orgnlDte = balsDate;
      balsDate = DateTime.ParseExact(
   balsDate, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      balsDate = balsDate.Substring(0, 10);

      double res = 0;
      string strSql = "";
      string usesSQL = Global.mnFrm.cmCde.getGnrlRecNm("org.org_pay_items",
   "item_id", "uses_sql_formulas", balsItmID);
      if (usesSQL != "1")
      {
        strSql = "SELECT a.bals_amount " +
           "FROM pay.pay_balsitm_bals a " +
           "WHERE(to_timestamp(a.bals_date,'YYYY-MM-DD') <=  to_timestamp('" + balsDate +
           "','YYYY-MM-DD') and a.bals_itm_id = " + balsItmID + " and a.person_id = " + prsn_id +
           ") ORDER BY to_timestamp(a.bals_date,'YYYY-MM-DD') DESC LIMIT 1 OFFSET 0";
        //Global.mnFrm.cmCde.showSQLNoPermsn(strSql);
        DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
        if (dtst.Tables[0].Rows.Count > 0)
        {
          double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out res);
        }
      }
      else
      {
        string valSQL = Global.mnFrm.cmCde.getItmValSQL(Global.getPrsnItmVlID(prsn_id, balsItmID, orgnlDte));
        if (valSQL == "")
        {
        }
        else
        {
          try
          {
            res = Global.mnFrm.cmCde.exctItmValSQL(
              valSQL, prsn_id,
              Global.mnFrm.cmCde.Org_id, balsDate);
          }
          catch (Exception ex)
          {
          }
        }
      }
      return res;
    }

    public static long getItmDailyBalsIDRetro(long balsItmID, string balsDate, long prsn_id)
    {
      balsDate = DateTime.ParseExact(
   balsDate, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      balsDate = balsDate.Substring(0, 10);
      string strSql = "";
      strSql = "SELECT a.bals_id " +
   "FROM pay.pay_balsitm_bals_retro a " +
   "WHERE(to_timestamp(a.bals_date,'YYYY-MM-DD') =  to_timestamp('" + balsDate +
   "','YYYY-MM-DD') and a.bals_itm_id = " + balsItmID +
   " and a.person_id = " + prsn_id + ")";

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

    public static double getBlsItmDailyBalsRetro(long balsItmID, long prsn_id, string balsDate)
    {
      string orgnlDte = balsDate;
      balsDate = DateTime.ParseExact(
   balsDate, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      balsDate = balsDate.Substring(0, 10);
      double res = 0;
      string strSql = "";
      string usesSQL = Global.mnFrm.cmCde.getGnrlRecNm("org.org_pay_items",
        "item_id", "uses_sql_formulas", balsItmID);
      if (usesSQL != "1")
      {
        strSql = "SELECT a.bals_amount " +
      "FROM pay.pay_balsitm_bals_retro a " +
      "WHERE(to_timestamp(a.bals_date,'YYYY-MM-DD') =  to_timestamp('" + balsDate +
      "','YYYY-MM-DD') and a.bals_itm_id = " + balsItmID + " and a.person_id = " + prsn_id + ")";

        DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
        if (dtst.Tables[0].Rows.Count > 0)
        {
          double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out res);
        }
      }
      else
      {
        string valSQL = Global.mnFrm.cmCde.getItmValSQL(Global.getPrsnItmVlID(prsn_id, balsItmID, orgnlDte));
        if (valSQL == "")
        {
        }
        else
        {
          try
          {
            res = Global.mnFrm.cmCde.exctItmValSQL(
              valSQL, prsn_id,
              Global.mnFrm.cmCde.Org_id, balsDate);
          }
          catch (Exception ex)
          {
          }
        }
      }
      return res;
    }

    public static double getBlsItmLtstDailyBalsRetro(long balsItmID, long prsn_id, string balsDate)
    {
      string orgnlDte = balsDate;
      balsDate = DateTime.ParseExact(
   balsDate, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      balsDate = balsDate.Substring(0, 10);

      double res = 0;
      string strSql = "";
      string usesSQL = Global.mnFrm.cmCde.getGnrlRecNm("org.org_pay_items",
   "item_id", "uses_sql_formulas", balsItmID);
      if (usesSQL != "1")
      {
        strSql = "SELECT a.bals_amount " +
           "FROM pay.pay_balsitm_bals_retro a " +
           "WHERE(to_timestamp(a.bals_date,'YYYY-MM-DD') <=  to_timestamp('" + balsDate +
           "','YYYY-MM-DD') and a.bals_itm_id = " + balsItmID + " and a.person_id = " + prsn_id +
           ") ORDER BY to_timestamp(a.bals_date,'YYYY-MM-DD') DESC LIMIT 1 OFFSET 0";

        DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
        if (dtst.Tables[0].Rows.Count > 0)
        {
          double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out res);
        }
      }
      else
      {
        string valSQL = Global.mnFrm.cmCde.getItmValSQL(Global.getPrsnItmVlID(prsn_id, balsItmID, orgnlDte));
        if (valSQL == "")
        {
        }
        else
        {
          try
          {
            res = Global.mnFrm.cmCde.exctItmValSQL(
              valSQL, prsn_id,
              Global.mnFrm.cmCde.Org_id, balsDate);
          }
          catch (Exception ex)
          {
          }
        }
      }
      return res;
    }

    #endregion

    #region "PERSON PAY ITEMS AND BANKS"
    public static void createBank(long prsnid, string brnch, string bnknm,
     string accntnm, string accntno, string accntyp, double netportion, string uom)
    {
      if (bnknm.Length > 200)
      {
        bnknm = bnknm.Substring(0, 200);
      }
      if (brnch.Length > 200)
      {
        brnch = brnch.Substring(0, 200);
      }
      if (accntno.Length > 200)
      {
        accntno = accntno.Substring(0, 200);
      }
      if (accntnm.Length > 200)
      {
        accntnm = accntnm.Substring(0, 200);
      }
      if (accntyp.Length > 100)
      {
        accntyp = accntyp.Substring(0, 100);
      }
      if (uom.Length > 10)
      {
        uom = uom.Substring(0, 10);
      }
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = "INSERT INTO pasn.prsn_bank_accounts(" +
               "account_name, account_number, net_pay_portion, " +
               "portion_uom, created_by, creation_date, last_update_by, last_update_date, " +
               "person_id, bank_name, bank_branch, account_type) " +
       "VALUES ('" + accntnm.Replace("'", "''") + "', '" + accntno.Replace("'", "''") + "'" +
       ", " + netportion + ", '" + uom.Replace("'", "''") +
       "', " + Global.myPay.user_id + ", '" + dateStr + "', " +
               Global.myPay.user_id + ", '" + dateStr + "', " + prsnid +
               ", '" + bnknm.Replace("'", "''") + "', '" + brnch.Replace("'", "''") + "', '" + accntyp.Replace("'", "''") + "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void createBnftsPrs(long prsnid, long itmid, long itm_val_id,
  string strtdte, string enddte)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      strtdte = DateTime.ParseExact(
   strtdte, "dd-MMM-yyyy",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

      enddte = DateTime.ParseExact(
   enddte, "dd-MMM-yyyy",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      string insSQL = "INSERT INTO pasn.prsn_bnfts_cntrbtns(" +
               "person_id, item_id, item_pssbl_value_id, valid_start_date, valid_end_date, " +
               "created_by, creation_date, last_update_by, last_update_date) " +
       "VALUES (" + prsnid + ", " + itmid +
       ", " + itm_val_id + ", '" + strtdte.Replace("'", "''") + "', '" + enddte.Replace("'", "''") +
       "', " + Global.myPay.user_id + ", '" + dateStr + "', " +
               Global.myPay.user_id + ", '" + dateStr + "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void updateBnftsPrs(long prsnid, long rowid, long itm_val_id,
  string strtdte, string enddte)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      strtdte = DateTime.ParseExact(
   strtdte, "dd-MMM-yyyy",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

      enddte = DateTime.ParseExact(
   enddte, "dd-MMM-yyyy",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      string updtSQL = "UPDATE pasn.prsn_bnfts_cntrbtns " +
          "SET person_id=" + prsnid + ", item_pssbl_value_id=" + itm_val_id +
       ", valid_start_date='" + strtdte.Replace("'", "''") +
       "', valid_end_date='" + enddte.Replace("'", "''") + "', " +
          "last_update_by=" +
               Global.myPay.user_id + ", last_update_date='" + dateStr + "' " +
       "WHERE row_id=" + rowid;
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void updateItmValsPrs(long rowid, long itm_val_id)
    {
      //, string enddte
      //   enddte = DateTime.ParseExact(
      //enddte, "dd-MMM-yyyy",
      //System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      //, valid_end_date='" + enddte.Replace("'", "''") + "'
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE pasn.prsn_bnfts_cntrbtns " +
              "SET item_pssbl_value_id=" + itm_val_id +
        ", last_update_by=" +
                        Global.myPay.user_id + ", last_update_date='" + dateStr + "' " +
        "WHERE row_id=" + rowid;
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void updateAccount(long prsnid,
     long prsn_accntid, string brnch, string bnknm,
     string accntnm, string accntno, string accntyp, double netportion, string uom)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE pasn.prsn_bank_accounts " +
      "SET account_name ='" + accntnm.Replace("'", "''") +
      "', account_number ='" + accntno.Replace("'", "''") + "' , bank_name = '" + bnknm.Replace("'", "''") +
      "', bank_branch ='" + brnch.Replace("'", "''") + "' , account_type ='" + accntyp.Replace("'", "''") +
      "' , person_id=" + prsnid +
       ", net_pay_portion=" + netportion + ", portion_uom='" + uom.Replace("'", "''") +
       "', last_update_by=" + Global.myPay.user_id + ", last_update_date='" + dateStr + "' " +
       "WHERE prsn_accnt_id=" + prsn_accntid;
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void deletePayItmPrs(long row_id, string locID)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Person's Local ID = " + locID;
      string delSQL = "DELETE FROM pasn.prsn_bnfts_cntrbtns WHERE row_id = " + row_id;
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }

    public static void deleteAccount(long prsn_accntid, string locID)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Person's Local ID = " + locID;
      string delSQL = "DELETE FROM pasn.prsn_bank_accounts WHERE prsn_accnt_id = " + prsn_accntid;
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }

    public static DataSet getAllBnftsPrs(long offset, int limit_size, long prsnid)
    {
      string selSQL = @"SELECT a.item_id, a.item_pssbl_value_id, 
to_char(to_timestamp(a.valid_start_date,'YYYY-MM-DD'),'DD-Mon-YYYY'), 
to_char(to_timestamp(a.valid_end_date,'YYYY-MM-DD'),'DD-Mon-YYYY'), 
a.row_id, b.item_maj_type, b.pay_run_priority, b.item_code_name " +
      "FROM pasn.prsn_bnfts_cntrbtns a, org.org_pay_items b WHERE ((a.item_id=b.item_id) and (a.person_id = " + prsnid +
      ")) ORDER BY b.item_maj_type, b.pay_run_priority, b.item_code_name LIMIT " + limit_size +
        " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      Global.mnFrm.pyitm_SQLPrs = selSQL;
      return dtst;
    }

    //public static double getBlsItmLtstDailyBals(long balsItmID, long prsn_id, string balsDate)
    //{
    //  string strSql = "";
    //  strSql = "SELECT a.bals_amount " +
    //"FROM pay.pay_balsitm_bals a " +
    //"WHERE(to_timestamp(a.bals_date,'YYYY-MM-DD') <=  to_timestamp('" + balsDate +
    //"','YYYY-MM-DD') and a.bals_itm_id = " + balsItmID + " and a.person_id = " + prsn_id +
    //") ORDER BY to_timestamp(a.bals_date,'YYYY-MM-DD') DESC LIMIT 1 OFFSET 0";

    //  DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
    //  if (dtst.Tables[0].Rows.Count > 0)
    //  {
    //    return double.Parse(dtst.Tables[0].Rows[0][0].ToString());
    //  }
    //  else
    //  {
    //    return 0.00;
    //  }
    //}


    public static double getBlsItmLtstDailyBalsPrs(long balsItmID, long prsn_id, string balsDate)
    {
      balsDate = DateTime.ParseExact(
   balsDate, "dd-MMM-yyyy",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      double res = 0;
      string strSql = "";
      string usesSQL = Global.mnFrm.cmCde.getGnrlRecNm("org.org_pay_items",
   "item_id", "uses_sql_formulas", balsItmID);
      if (usesSQL != "1")
      {
        strSql = "SELECT a.bals_amount " +
           "FROM pay.pay_balsitm_bals a " +
           "WHERE(to_timestamp(a.bals_date,'YYYY-MM-DD') <=  to_timestamp('" + balsDate +
           "','YYYY-MM-DD') and a.bals_itm_id = " + balsItmID + " and a.person_id = " + prsn_id +
           ") ORDER BY to_timestamp(a.bals_date,'YYYY-MM-DD') DESC LIMIT 1 OFFSET 0";

        DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
        if (dtst.Tables[0].Rows.Count > 0)
        {
          double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out res);
        }
      }
      else
      {
        string valSQL = Global.mnFrm.cmCde.getItmValSQL(Global.getPrsnItmVlIDPrs(prsn_id, balsItmID));
        if (valSQL == "")
        {
        }
        else
        {
          try
          {
            res = Global.mnFrm.cmCde.exctItmValSQL(
              valSQL, prsn_id,
              Global.mnFrm.cmCde.Org_id, balsDate);
          }
          catch (Exception ex)
          {
          }
        }
      }
      return res;
    }

    public static long getPrsnItmVlIDPrs(long prsnID, long itmID)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string strSql = "Select a.item_pssbl_value_id FROM pasn.prsn_bnfts_cntrbtns a where((a.person_id = " +
    prsnID + ") and (a.item_id = " + itmID + ") and (to_timestamp('" + dateStr + "'," +
    "'YYYY-MM-DD HH24:MI:SS') between to_timestamp(valid_start_date," +
    "'YYYY-MM-DD 00:00:00') AND to_timestamp(valid_end_date,'YYYY-MM-DD 23:59:59')))";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return -100000;
    }

    public static long get_Total_BnftsPrs(long prsnid)
    {
      string strSql = "";
      strSql = "SELECT count(1) " +
      "FROM pasn.prsn_bnfts_cntrbtns WHERE ((person_id = " + prsnid +
      "))";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return 0;
      }
    }

    public static DataSet getAllAccounts(long prsnid)
    {
      string selSQL = "SELECT bank_name, bank_branch, account_name, account_number, " +
       "account_type, net_pay_portion, portion_uom, prsn_accnt_id " +
            "FROM pasn.prsn_bank_accounts WHERE ((person_id = " + prsnid +
            ")) ORDER BY prsn_accnt_id DESC";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      Global.mnFrm.bank_SQL = selSQL;
      return dtst;
    }
    public static string get_PyItm_Rec_HstryPrs(long rowID)
    {
      string strSQL = "SELECT a.created_by, to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), a.last_update_by, to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') " +
      "FROM pasn.prsn_bnfts_cntrbtns a WHERE(a.row_id = " + rowID + ")";
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

    public static string get_Bank_Rec_Hstry(long rowID)
    {
      string strSQL = "SELECT a.created_by, to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), a.last_update_by, to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') " +
      "FROM pasn.prsn_bank_accounts a WHERE(a.prsn_accnt_id  = " + rowID + ")";
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

    public static long doesPrsnHvItmPrs(long prsnid, long itmid)
    {
      string selSQL = "SELECT row_id " +
                  "FROM pasn.prsn_bnfts_cntrbtns WHERE ((person_id = " + prsnid +
                  ") and (item_id = " + itmid + "))";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return -1;
      }
    }

    #endregion
    #endregion
    #endregion

    #region "CUSTOM FUNCTIONS..."
    public static void refreshRqrdVrbls()
    {
      Global.mnFrm.cmCde.DefaultPrvldgs = Global.dfltPrvldgs;
      //Global.mnFrm.cmCde.Login_number = Global.myPay.login_number;
      Global.mnFrm.cmCde.ModuleAdtTbl = Global.myPay.full_audit_trail_tbl_name;
      Global.mnFrm.cmCde.ModuleDesc = Global.myPay.mdl_description;
      Global.mnFrm.cmCde.ModuleName = Global.myPay.name;
      //Global.mnFrm.cmCde.pgSqlConn = Global.myPay.Host.globalSQLConn;
      //Global.mnFrm.cmCde.Role_Set_IDs = Global.myPay.role_set_id;
      Global.mnFrm.cmCde.SampleRole = "Internal Payments Administrator";
      //Global.mnFrm.cmCde.User_id = Global.myPay.user_id;
      //Global.mnFrm.cmCde.Org_id = Global.myPay.org_id;
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      Global.myPay.user_id = Global.mnFrm.usr_id;
      Global.myPay.login_number = Global.mnFrm.lgn_num;
      Global.myPay.role_set_id = Global.mnFrm.role_st_id;
      Global.myPay.org_id = Global.mnFrm.Og_id;
    }

    public static void createRqrdLOVs()
    {
      string[] sysLovs = { "Item Sets for Payments(Enabled)", "Person Sets for Payments(Enabled)", 
                           "Pay Run Names/Numbers", "Retro Pay Items","Pay Balance Items" };
      string[] sysLovsDesc = { "Item Sets for Payments(Enabled)", "Person Sets for Payments(Enabled)", 
                               "Quick/Mass Pay Run Names/Numbers", "Retro Pay Items","Pay Balance Items" };
      string[] sysLovsDynQrys = { "select distinct trim(to_char(z.hdr_id,'999999999999999999999999999999')) a, z.itm_set_name b, '' c, z.org_id d, '' e, '' f, y.user_role_id g from pay.pay_itm_sets_hdr z, pay.pay_sets_allwd_roles y where z.hdr_id = y.itm_set_id and z.is_enabled='1' order by z.itm_set_name",
                                  "select distinct trim(to_char(z.prsn_set_hdr_id,'999999999999999999999999999999')) a, z.prsn_set_hdr_name b, '' c, z.org_id d, '' e, '' f, y.user_role_id g  from pay.pay_prsn_sets_hdr z, pay.pay_sets_allwd_roles y where z.prsn_set_hdr_id = y.prsn_set_id and z.is_enabled='1' order by z.prsn_set_hdr_name",
                                  "select distinct mass_pay_name a, mass_pay_desc b, '' c, org_id d, mass_pay_id e from pay.pay_mass_pay_run_hdr where run_status='1' order by mass_pay_id DESC",
                                  "select '' || item_id a, item_code_name b, '' c, org_id d from org.org_pay_items where is_retro_element='1' and (item_id NOT IN (select distinct z.retro_item_id from org.org_pay_items z)) order by item_code_name",
                                  "select item_code_name a, item_desc||' ('|| item_id||')' b, '' c, org_id d from org.org_pay_items where item_maj_type='Balance Item' order by item_code_name"};
      string[] pssblVals = { };

      Global.mnFrm.cmCde.createSysLovs(sysLovs, sysLovsDynQrys, sysLovsDesc);
      Global.mnFrm.cmCde.createSysLovsPssblVals(sysLovs, pssblVals);
    }

    public static void createDfltSets()
    {
      if (Global.mnFrm.cmCde.getItmStID("Bill Items SQL", Global.mnFrm.cmCde.Org_id)
   <= 0)
      {
        string query1 = "/* Created on 12/27/2012 10:18:43 AM By Rhomicom */ " +
    "SELECT a.item_id, a.item_code_name, a.item_value_uom, " +
    "(CASE WHEN a.item_min_type='Earnings' or a.item_min_type='Employer Charges' " +
    "THEN 'Payment by Organisation' WHEN a.item_min_type='Bills/Charges' or " +
    "a.item_min_type='Deductions' THEN 'Payment by Person' ELSE 'Purely Informational' END) trns_typ " +
    "FROM org.org_pay_items a " +
    "WHERE a.local_classfctn = 'Bill Item' AND a.org_id = (SELECT org_id " +
                    "FROM org.org_details " +
                    "WHERE org_name = '" + Global.mnFrm.cmCde.getOrgName(Global.mnFrm.cmCde.Org_id).Replace("'", "''") + "') ";
        Global.createItmSt(Global.mnFrm.cmCde.Org_id,
          "Bill Items SQL",
          "Bill Items SQL", true, false, true, query1);
      }

      if (Global.mnFrm.cmCde.getPrsStID("One Person SQL", Global.mnFrm.cmCde.Org_id)
   <= 0)
      {
        string query1 = "/* Created on 12/27/2012 10:18:43 AM By Rhomicom */ " +
    "SELECT DISTINCT " +
     "a.person_id, " +
     "a.local_id_no, " +
     "trim(a.title || ' ' || a.sur_name || ', ' || a.first_name || ' ' || a.other_names) full_name, a.img_location " +
    "FROM prs.prsn_names_nos a, pasn.prsn_prsntyps b " +
    "WHERE     a.person_id = b.person_id " +
    "AND a.org_id = (SELECT org_id " +
                    "FROM org.org_details " +
                    "WHERE org_name = '" + Global.mnFrm.cmCde.getOrgName(Global.mnFrm.cmCde.Org_id).Replace("'", "''") + "') " +
    "AND a.local_id_no IN ('M0001') " +
    "AND (now () BETWEEN TO_TIMESTAMP (b.valid_start_date || ' 00:00:00', " +
                                      "'YYYY-MM-DD HH24:MI:SS' " +
                        ") " +
                    "AND  TO_TIMESTAMP (b.valid_end_date || ' 23:59:59', " +
                                       "'YYYY-MM-DD HH24:MI:SS' " +
                         "))";
        Global.createPrsSt(Global.mnFrm.cmCde.Org_id,
          "One Person SQL",
          "One Person SQL", true, query1, false, true);
      }

      if (Global.mnFrm.cmCde.getPrsStID("All Active Employees", Global.mnFrm.cmCde.Org_id)
   <= 0)
      {
        string query1 = "/* Created on 12/27/2012 10:18:43 AM By Rhomicom */ " +
    "SELECT DISTINCT " +
     "a.person_id, " +
     "a.local_id_no, " +
     "trim(a.title || ' ' || a.sur_name || ', ' || a.first_name || ' ' || a.other_names) full_name, a.img_location " +
    "FROM prs.prsn_names_nos a, pasn.prsn_prsntyps b " +
    "WHERE     a.person_id = b.person_id " +
    "AND a.org_id = (SELECT org_id " +
                    "FROM org.org_details " +
                    "WHERE org_name = '" + Global.mnFrm.cmCde.getOrgName(Global.mnFrm.cmCde.Org_id).Replace("'", "''") + "') " +
    "AND b.prsn_type = 'Employee' " +
    "AND (now () BETWEEN TO_TIMESTAMP (b.valid_start_date || ' 00:00:00', " +
                                      "'YYYY-MM-DD HH24:MI:SS' " +
                        ") " +
                    "AND  TO_TIMESTAMP (b.valid_end_date || ' 23:59:59', " +
                                       "'YYYY-MM-DD HH24:MI:SS' " +
                         "))";
        Global.createPrsSt(Global.mnFrm.cmCde.Org_id,
          "All Active Employees",
          "All Active Employees", true, query1, false, true);
      }
      if (Global.mnFrm.cmCde.getPrsStID("All Persons", Global.mnFrm.cmCde.Org_id)
        <= 0)
      {
        string query1 = "/* Created on 12/27/2012 10:18:43 AM By Rhomicom */ " +
    "SELECT DISTINCT " +
     "a.person_id, " +
     "a.local_id_no, " +
     "trim(a.title || ' ' || a.sur_name || ', ' || a.first_name || ' ' || a.other_names) full_name, a.img_location " +
    "FROM prs.prsn_names_nos a " +
    "WHERE 1 = 1";
        Global.createPrsSt(Global.mnFrm.cmCde.Org_id,
          "All Persons",
          "All Persons", true, query1, false, true);
      }

    }

    public static void createRqrdItems()
    {
      string[] itmNm = { "Total Advance Payments Balance", "Advance Payments Amount Kept", "Advance Payments Amount Applied" };
      string[] itmDesc = { "Total Advance Payments Balance", "Advance Payments Amount Kept", "Advance Payments Amount Applied" };
      string[] itmMajTyp = { "Balance Item", "Pay Value Item", "Pay Value Item" };
      string[] itmMinTyp = { "Purely Informational", "Deductions", "Earnings" };
      string[] itmUOM = { "Money", "Money", "Money" };
      string[] payFreq = { "Adhoc", "Adhoc", "Adhoc" };
      string[] payPryty = { "99999999.97", "99999999.98", "99999999.99" };
      string[] usesSQL = { "NO", "NO", "YES" };
      string[] localClass = { "Advance Items", "Advance Items", "Advance Items" };
      string[] balsTyp = { "Cumulative", "", "" };
      string[] inc_dc_cost = { "", "Increase", "Decrease" };
      string[] costAccntNo = { "", "", "" };
      string[] inc_dc_bals = { "", "Increase", "Decrease" };
      string[] balsAccntNo = { "", "", "" };
      string[] feedIntoNM = { "", "Total Advance Payments Balance", "Total Advance Payments Balance" };
      string[] add_subtract = { "", "Adds", "Subtracts" };
      string[] scale_fctr = { "", "1.00", "1.00" };
      string[] isRetro = { "NO", "NO", "NO" };
      string[] retroItmNm = { "", "", "" };
      string[] invItmCode = { "", "", "" };
      string[] allwEdit = { "YES", "YES", "YES" };
      string[] creatsAcctng = { "NO", "YES", "YES" };
      string[] valNm = { "Total Advance Payments Balance Value", "Advance Payments Amount Kept Value", "Advance Payments Amount Applied Value" };
      string[] amnt = { "0", "0", "0" };
      string[] valSQL = { "", "", @"select pay.get_ltst_blsitm_bals({:person_id},org.get_payitm_id('Total Advance Payments Balance'),'{:pay_date}')" };
      for (int i = 0; i < itmNm.Length; i++)
      {
        long itm_id_in = Global.mnFrm.cmCde.getItmID(itmNm[i], Global.mnFrm.cmCde.Org_id);
        double pryty = 500;
        double.TryParse(payPryty[i], out pryty);
        int itmMnTypID = -1;
        double scl = 1;
        double.TryParse(scale_fctr[i], out scl);
        if (itmMinTyp[i] == "Earnings")
        {
          itmMnTypID = 1;
        }
        else if (itmMinTyp[i] == "Employer Charges")
        {
          itmMnTypID = 2;
        }
        else if (itmMinTyp[i] == "Deductions")
        {
          itmMnTypID = 3;
        }
        else if (itmMinTyp[i] == "Bills/Charges")
        {
          itmMnTypID = 4;
        }
        else if (itmMinTyp[i] == "Purely Informational")
        {
          itmMnTypID = 5;
        }
        bool isRetroElmnt = false;
        bool allwEditing = false;
        bool creatsActng = false;
        long retrItmID = Global.mnFrm.cmCde.getItmID(retroItmNm[i], Global.mnFrm.cmCde.Org_id);
        long invItmID = Global.mnFrm.cmCde.getInvItmID(invItmCode[i], Global.mnFrm.cmCde.Org_id);
        if (isRetro[i] == "YES")
        {
          isRetroElmnt = true;
        }
        if (allwEdit[i] == "YES")
        {
          allwEditing = true;
        }
        if (creatsAcctng[i] == "YES")
        {
          creatsActng = true;
        }
        if (itm_id_in <= 0 && Global.mnFrm.cmCde.Org_id > 0)
        {
          Global.createItm(Global.mnFrm.cmCde.Org_id, itmNm[i], itmDesc[i], itmMajTyp[i], itmMinTyp[i], itmUOM[i],
            Global.mnFrm.cmCde.cnvrtYNToBool(usesSQL[i]), true, Global.mnFrm.cmCde.getAccntID(costAccntNo[i], Global.mnFrm.cmCde.Org_id),
            Global.mnFrm.cmCde.getAccntID(balsAccntNo[i], Global.mnFrm.cmCde.Org_id), payFreq[i],
            localClass[i], pryty, inc_dc_cost[i], inc_dc_bals[i], balsTyp[i], itmMnTypID,
            isRetroElmnt, retrItmID, invItmID, allwEditing, creatsActng);
          long nwItmID = Global.mnFrm.cmCde.getItmID(itmNm[i], Global.mnFrm.cmCde.Org_id);
          long feedIntoItmID = Global.mnFrm.cmCde.getItmID(feedIntoNM[i], Global.mnFrm.cmCde.Org_id);
          string feedItmMayTyp = Global.mnFrm.cmCde.getGnrlRecNm("org.org_pay_items",
            "item_id", "item_maj_type", feedIntoItmID);
          if (itmMajTyp[i] == "Balance Item")
          {
            Global.createItmVal(nwItmID, 0, "", itmNm[i] + " Value");
          }
          else if (feedItmMayTyp == "Balance Item")
          {
            if (feedIntoItmID > 0)
            {
              if (add_subtract[i] != "Adds" && add_subtract[i] != "Subtracts")
              {
                add_subtract[i] = "Adds";
              }
              Global.createItmFeed(nwItmID, feedIntoItmID, add_subtract[i], scl);
            }
          }
        }

        long val_id_in = Global.mnFrm.cmCde.getItmValID(valNm[i], itm_id_in);
        double amntFig = 0;
        double.TryParse(amnt[i], out amntFig);

        if (itm_id_in > 0 && val_id_in <= 0)
        {
          Global.createItmVal(itm_id_in, amntFig, valSQL[i], valNm[i]);
        }
        else if (val_id_in > 0)
        {
          Global.updateItmVal(val_id_in, itm_id_in, amntFig, valSQL[i], valNm[i]);
        }
      }
    }
    #endregion
  }
}
