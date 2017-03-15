using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using StoresAndInventoryManager.Forms;
using System.Windows.Forms;
using CommonCode;
using Npgsql;

namespace StoresAndInventoryManager.Classes
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
        public static StoresAndInventoryManager myInv = new StoresAndInventoryManager();
        public static mainForm mnFrm = null;
        public static wfnInvoiceForm wfnInvcFrm = null;
        public static wfnPrchOrdrForm wfnPOrdrFrm = null;
        public static wfnItmLstForm wfnItemListFrm = null;

        public static wfnPrdtCatForm wfnCatFrm = null;
        public static wfnStrsWhseForm wfnStoresFrm = null;
        public static wfnRcptsForm wfnRcptFrm = null;
        public static wfnRcpRtrnForm wfnRtrnFrm = null;
        public static wfnItmTmpltsForm wfnTmpltsFrm = null;
        public static wfnItmBalsForm wfnBalsFrm = null;
        public static wfnUomForm wfnUOMFrm = null;
        public static pyblsDocsForm wfnPyblsForm = null;
        public static wfnStckTrnsfrsForm wfnTrnsfrsFrm = null;
        public static wfnMiscAdjstForm wfnAdjstFrm = null;
        public static wfnGLIntfcForm wfnIntFcFrm = null;
        public static productionForm wfnProdFrm = null;
        public static leftMenuForm wfnLftMnu = null;
        public static string[] dfltPrvldgs = { "View Inventory Manager", 
      /*1*/ "View Item List", "View Product Categories", "View Stores/Warehouses"
      /*4*/,"View Receipts", "View Receipt Returns", "View Item Type Templates",
      /*7*/ "View Item Balances",
      /*8*/ "Add Items","Update Items",
      /*10*/ "Add Item Stores","Update Item Stores","Delete Item Stores",
      /*13*/"Add Product Category","Update Product Category",
      /*15*/"Add Stores","Update Stores",
      /*17*/"Add Store Users","Update Store Users","Delete Store Users",
      /*20*/"Add Store Shelves","Delete Store Shelves",
      /*22*/"Add Receipt","Delete Receipt",
      /*24*/"Add Receipt Return","Delete Receipt Return",
      /*26*/"Add Item Template","Update Item Template",
      /*28*/"Add Template Stores","Update Template Stores",
      /*30*/"View GL Interface",
      /*31*/"View SQL","View Record History","Send To GL Interface Table",
      /*34*/"View Purchases","View Sales/Item Issues", "View Sales Returns", 
      /*37*/"View Payments Received",
      /*38*/"View Purchase Requisitions", "Add Purchase Requisitions", "Edit Purchase Requisitions","Delete Purchase Requisitions",
      /*42*/"View Purchase Orders", "Add Purchase Orders", "Edit Purchase Orders","Delete Purchase Orders",
      /*46*/"View Pro-Forma Invoices", "Add Pro-Forma Invoices", "Edit Pro-Forma Invoices","Delete Pro-Forma Invoices",
      /*50*/"View Sales Orders", "Add Sales Orders", "Edit Sales Orders","Delete Sales Orders",
      /*54*/"View Sales Invoices", "Add Sales Invoices", "Edit Sales Invoices","Delete Sales Invoices",
      /*58*/"View Internal Item Requests", "Add Internal Item Requests", "Edit Internal Item Requests","Delete Internal Item Requests",
      /*62*/"View Item Issues-Unbilled", "Add Item Issues-Unbilled", "Edit Item Issues-Unbilled","Delete Item Issues-Unbilled",
      /*66*/"View Sales Returns", "Add Sales Return", "Edit Sales Return","Delete Sales Return",
      /*70*/"Send GL Interface Records to GL","Cancel Documents","View only Self-Created Documents",
      /*73*/"View UOM", "Add UOM", "Edit UOM", "Delete UOM","Make Payments","Delete Product Category",
      /*79*/"View UOM Conversion", "Add UOM Conversion", "Edit UOM Conversion", "Delete UOM Conversion",
      /*83*/"View Drug Interactions", "Add Drug Interactions", "Edit Drug Interactions", "Delete Drug Interactions",
      /*87*/"Edit Receipt","Edit Returns","Edit Store Transfers","Edit Adjustments",
      /*91*/"Clear Stock Balance", "Do Quick Receipt",
      /*93*/"View Item Production", "Add Item Production", "Edit Item Production", "Delete Item Production",
      /*97*/"Setup Production Processes","Apply Adhoc Discounts",
      /*99*/"View Production Runs", "Add Production Runs", "Edit Production Runs", "Delete Production Runs",
      /*103*/"Can Edit Unit Price"};


        public static string currentPanel = "";
        public static glIntrfcForm glFrm = null;
        public static prchseOrdrForm pOdrFrm = null;
        public static invoiceForm invcFrm = null;
        //public static pymntsGvnForm pymntFrm = null;
        public static itemListForm itmLstFrm = null;

        public static prdtCategories catgryFrm = null;
        public static storeHouses storesFrm = null;
        public static consgmtRcpt rcptFrm = null;

        public static consgmtRecReturns rtrnFrm = null;
        public static itemTypeTmplts tmpltsFrm = null;
        public static itmBals balsFrm = null;

        public static storeHseTransfers trnsfrsFrm = null;
        public static unitOfMeasures uomFrm = null;
        public static invAdjstmnt adjstmntFrm = null;

        public static string itms_SQL = "";
        public static int selectedStoreID = -1;
        public static string intFcSql = string.Empty;

        #endregion

        #region "DATA MANIPULATION FUNCTIONS..."

        #region "INSERT STATEMENTS..."
        public static void createDfltAcnts(int orgid)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = "INSERT INTO scm.scm_dflt_accnts(" +
                  "itm_inv_asst_acnt_id, cost_of_goods_acnt_id, expense_acnt_id, " +
                  "prchs_rtrns_acnt_id, rvnu_acnt_id, sales_rtrns_acnt_id, sales_cash_acnt_id, " +
                  "sales_check_acnt_id, sales_rcvbl_acnt_id, rcpt_cash_acnt_id, " +
                  "rcpt_lblty_acnt_id, rho_name, org_id, created_by, creation_date, " +
                  "last_update_by, last_update_date, inv_adjstmnts_lblty_acnt_id) " +
                  "VALUES (-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,'Default Accounts', " +
                  orgid + ", " + Global.myInv.user_id + ", '" + dateStr +
                  "', " + Global.myInv.user_id + ", '" + dateStr +
                  "',-1)";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void createPrchsDocHdr(int orgid, string docNum,
          string desc, string docTyp, string docdte, string needbyDte,
          int spplrID, int spplrSiteID, string apprvlSts, string nxtApprvl,
          long reqID, int prntdInvCur, decimal exchRate, string pyTrms)
        {
            docdte = DateTime.ParseExact(
         docdte, "dd-MMM-yyyy",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            needbyDte = DateTime.ParseExact(
         needbyDte, "dd-MMM-yyyy",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = "INSERT INTO scm.scm_prchs_docs_hdr(" +
                  "prchs_doc_date, need_by_date, supplier_id, " +
                  "supplier_site_id, comments_desc, approval_status, created_by, " +
                  "creation_date, last_update_by, last_update_date, next_aproval_action, " +
                  "purchase_doc_num, purchase_doc_type, requisition_id, org_id, prntd_doc_curr_id, " +
                  "exchng_rate, payment_terms) " +
                  "VALUES ('" + docdte.Replace("'", "''") +
                  "', '" + needbyDte.Replace("'", "''") +
                  "', " + spplrID + ", " + spplrSiteID + ", '" + desc.Replace("'", "''") +
                  "', '" + apprvlSts.Replace("'", "''") + "', " + Global.myInv.user_id + ", '" + dateStr +
                  "', " + Global.myInv.user_id + ", '" + dateStr +
                  "', '" + nxtApprvl.Replace("'", "''") +
                  "', '" + docNum.Replace("'", "''") + "', '" +
                  docTyp.Replace("'", "''") + "', " + reqID + ", " +
                  orgid + ", " + prntdInvCur + ", " + exchRate + ", '" +
                  pyTrms.Replace("'", "''") + "')";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void createPrchsDocLn(long docID, int itmID,
          double qty, double untPrice, int storeID, int crncyID, long srclnID, string altrntNm)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = "INSERT INTO scm.scm_prchs_docs_det(" +
                  "prchs_doc_hdr_id, itm_id, quantity, unit_price, " +
                  "created_by, creation_date, last_update_by, last_update_date, " +
                  "store_id, crncy_id, src_line_id, alternate_item_name) " +
                  "VALUES (" + docID +
                  ", " + itmID +
                  ", " + qty + ", " + untPrice + ", " + Global.myInv.user_id + ", '" + dateStr +
                  "', " + Global.myInv.user_id + ", '" + dateStr +
                  "', " + storeID +
                  ", " + crncyID + ", " + srclnID + ", '" + altrntNm.Replace("'", "''") +
                  "')";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static double getCstmrDpsts(int cstmrID, int invcurID)
        {
            string selSQL = @"select SUM(invoice_amount-invc_amnt_appld_elswhr) c, customer_id e, 
invc_curr_id f from accb.accb_rcvbls_invc_hdr where (((rcvbls_invc_type = 'Customer Advance Payment' and (invoice_amount-amnt_paid)<=0) 
or rcvbls_invc_type = 'Customer Debit Memo (InDirect Refund)') 
and approval_status='Approved' and (invoice_amount-invc_amnt_appld_elswhr)>0 and customer_id>0 and customer_id = " + cstmrID + " and invc_curr_id = " + invcurID + @") 
GROUP BY customer_id,invc_curr_id";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return double.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return 0;
        }

        public static void createSmmryItm(string smmryTyp,
          string smmryNm, double amnt, long codeBehind, string srcDocTyp,
          long srcDocHdrID, bool autoCalc)
        {
            if (smmryTyp == "3Discount")
            {
                amnt = -1 * Math.Abs(amnt);
            }
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = "INSERT INTO scm.scm_doc_amnt_smmrys(" +
                  "smmry_type, smmry_name, smmry_amnt, code_id_behind, " +
                  "src_doc_type, src_doc_hdr_id, created_by, creation_date, last_update_by, " +
                  "last_update_date, auto_calc) " +
                  "VALUES ('" + smmryTyp.Replace("'", "''") +
                  "', '" + smmryNm.Replace("'", "''") +
                  "', " + amnt + ", " + codeBehind + ", '" + srcDocTyp.Replace("'", "''") +
                  "', " + srcDocHdrID + ", " + Global.myInv.user_id + ", '" + dateStr +
                  "', " + Global.myInv.user_id + ", '" + dateStr + "', '" +
                  Global.mnFrm.cmCde.cnvrtBoolToBitStr(autoCalc) + "')";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void createSalesDocHdr(int orgid, string docNum,
         string desc, string docTyp, string docdte, string pymntTrms,
         int cstmrID, int siteID, string apprvlSts,
         string nxtApprvl, long srcDocID, int rcvblAcntID,
         int pymntID, int invcCurrID, double exchRate,
         long chckInID, string chckInType, bool enblAutoChrg,
         long event_rgstr_id, string evntCtgry, bool allwDues, string evntType)
        {
            docdte = DateTime.ParseExact(
         docdte, "dd-MMM-yyyy",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = "INSERT INTO scm.scm_sales_invc_hdr(" +
                  "invc_date, payment_terms, customer_id, " +
                  "customer_site_id, comments_desc, approval_status, created_by, " +
                  "creation_date, last_update_by, last_update_date, next_aproval_action, " +
                  "invc_number, invc_type, src_doc_hdr_id, org_id, receivables_accnt_id, " +
                  "pymny_method_id, invc_curr_id, exchng_rate, " +
                  "other_mdls_doc_id, other_mdls_doc_type, enbl_auto_misc_chrges, " +
                  "event_rgstr_id, evnt_cost_category, allow_dues, event_doc_type) " +
                  "VALUES ('" + docdte.Replace("'", "''") +
                  "', '" + pymntTrms.Replace("'", "''") +
                  "', " + cstmrID + ", " + siteID + ", '" + desc.Replace("'", "''") +
                  "', '" + apprvlSts.Replace("'", "''") + "', " + Global.myInv.user_id + ", '" + dateStr +
                  "', " + Global.myInv.user_id + ", '" + dateStr +
                  "', '" + nxtApprvl.Replace("'", "''") +
                  "', '" + docNum.Replace("'", "''") + "', '" +
                  docTyp.Replace("'", "''") + "', " + srcDocID + ", " +
                  orgid + ", " + rcvblAcntID + ", " + pymntID + ", "
                  + invcCurrID + ", " + exchRate + "," + chckInID + ",'" + chckInType +
                  "','" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(enblAutoChrg) +
                  "'," + event_rgstr_id + ", '" + evntCtgry.Replace("'", "''") +
                  "','" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(allwDues) +
                  "', '" + evntType.Replace("'", "''") +
                  "')";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void createSalesDocLn(long lineid, long docID, int itmID,
          double qty, double untPrice, int storeID,
          int crncyID, long srclnID, int txCode, int dscntCde,
          int chrgeCde, string rtrnRsn, string cnsgmntIDs, double orgnlPrice,
          bool dlvrd, long prsnID, string altrntNm, int cogsID, int salesRevID,
          int salesRetID, int purcRetID, int expnsID)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = "INSERT INTO scm.scm_sales_invc_det(invc_det_ln_id, " +
                  "invc_hdr_id, itm_id, doc_qty, unit_selling_price, " +
                  "created_by, creation_date, last_update_by, last_update_date, " +
                  "store_id, crncy_id, src_line_id, tax_code_id, " +
                  @"dscnt_code_id, chrg_code_id, qty_trnsctd_in_dest_doc, 
      rtrn_reason, consgmnt_ids, orgnl_selling_price,is_itm_delivered, lnkd_person_id, alternate_item_name,  
            cogs_acct_id, sales_rev_accnt_id, sales_ret_accnt_id, purch_ret_accnt_id, expense_accnt_id) " +
                  "VALUES (" + lineid +
                  "," + docID +
                  ", " + itmID +
                  ", " + qty + ", " + untPrice + ", " + Global.myInv.user_id + ", '" + dateStr +
                  "', " + Global.myInv.user_id + ", '" + dateStr +
                  "', " + storeID +
                  ", " + crncyID + ", " + srclnID + ", " + txCode +
                  ", " + dscntCde + ", " + chrgeCde + ", 0, '" +
                  rtrnRsn.Replace("'", "''") + "', '" +
                  cnsgmntIDs.Replace("'", "''") + "', " + orgnlPrice +
                  ",'" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(dlvrd) + "', " + prsnID +
                  ",'" + altrntNm.Replace("'", "''") + "'," + cogsID +
                  "," + salesRevID +
                  "," + salesRetID +
                  "," + purcRetID +
                  "," + expnsID +
                  ")";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void createStckDailyBals(long skckId, double totQty,
         double rsvdQty, double avlblQty, string balsDate)
        {
            balsDate = DateTime.ParseExact(
         balsDate, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            if (balsDate.Length > 10)
            {
                balsDate = balsDate.Substring(0, 10);
            }
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = "INSERT INTO inv.inv_stock_daily_bals(" +
                              "stock_id, stock_tot_qty, reservations, available_balance, bals_date, " +
                              "created_by, creation_date, last_update_by, last_update_date, source_trns_ids) " +
              "VALUES (" + skckId +
              ", " + totQty + ", " + rsvdQty + ", " + avlblQty + ", '" + balsDate + "', " + Global.myInv.user_id + ", '" + dateStr +
                              "', " + Global.myInv.user_id + ", '" + dateStr + "', ',')";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void createCnsgmtDailyBals(long CnsgmId, double totQty,
         double rsvdQty, double avlblQty, string balsDate)
        {
            balsDate = DateTime.ParseExact(
         balsDate, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            if (balsDate.Length > 10)
            {
                balsDate = balsDate.Substring(0, 10);
            }
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = "INSERT INTO inv.inv_consgmt_daily_bals(" +
                              "consgmt_id, consgmt_tot_qty, reservations, available_balance, bals_date, " +
                              "created_by, creation_date, last_update_by, last_update_date, source_trns_ids) " +
              "VALUES (" + CnsgmId +
              ", " + totQty + ", " + rsvdQty + ", " + avlblQty + ", '" + balsDate +
              "', " + Global.myInv.user_id + ", '" + dateStr +
                              "', " + Global.myInv.user_id + ", '" + dateStr + "', ',')";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }


        public static void createPaymntLine(string pymtTyp, double amnt, double curBals,
          string payRmrk, string srcDocTyp, long srcDocID, string dateStr, string dateRcvd)
        {
            dateRcvd = DateTime.ParseExact(
         dateRcvd, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            dateStr = DateTime.ParseExact(
         dateStr, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            string insSQL = "INSERT INTO scm.scm_payments(" +
                  "pymnt_type, amount_paid, custmrs_balance, pymnt_remark, " +
                  "src_doc_typ, src_doc_id, created_by, creation_date, last_update_by, " +
                  "last_update_date, date_rcvd) " +
             "VALUES ('" + pymtTyp.Replace("'", "''") + "', " + amnt + ", " + curBals +
             ", '" + payRmrk.Replace("'", "''") + "', '" + srcDocTyp.Replace("'", "''") +
             "', " + srcDocID + ", " + Global.myInv.user_id + ", '" + dateStr + "', " +
                     Global.myInv.user_id + ", '" + dateStr + "', '" + dateRcvd + "')";
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
             "', " + Global.myInv.user_id + ", '" + dateStr + "', " + orgid + ", '0', " +
                     Global.myInv.user_id + ", '" + dateStr + "', '" +
                     batchsource.Replace("'", "''") + "', '0')";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void updtTodaysGLBatchPstngAvlblty(long batchid, string avlblty)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string insSQL = "UPDATE accb.accb_trnsctn_batches SET avlbl_for_postng='" + avlblty +
              "', last_update_by=" + Global.myInv.user_id +
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
                              ", '" + trnsDate + "', " + crncyid + ", " + Global.myInv.user_id + ", '" + dateStr +
                              "', " + batchid + ", " + crdtamnt + ", " + Global.myInv.user_id +
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
            string updtSQL = "UPDATE scm.scm_gl_interface SET gl_batch_id=-1 WHERE gl_batch_id=" + batchID;
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
                              "', " + Global.myInv.user_id + ", '" + dateStr +
                              "', " + orgid + ", '0', " + Global.myInv.user_id + ", '" + dateStr +
                              "', '" + btchsrc.Replace("'", "''") +
                              "', '" + batchvldty.Replace("'", "''") +
                              "', " + srcbatchid +
                              ",'" + avlblforPpstng + "')";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static long getNewBatchID()
        {
            //string strSql = "select nextval('accb.accb_trnsctn_batches_batch_id_seq'::regclass);";
            string strSql = "select  last_value from accb.accb_trnsctn_batches_batch_id_seq";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString()) + 1;
            }
            return -1;
        }

        public static double get_LtstExchRate(int fromCurrID, int toCurrID, string asAtDte)
        {
            int fnccurid = Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id);
            //this.curCode = Global.mnFrm.cmCde.getPssblValNm(this.curid);
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
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
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
                     ", '" + trnsdte.Replace("'", "''") + "', " + crncyid + ", " + Global.myInv.user_id +
                     ", '" + dateStr + "', " + batchid + ", " + crdtamnt + ", " +
                     Global.myInv.user_id + ", '" + dateStr + "', " + netamnt +
                     ", '0', '" + srcids + "', " + entrdAmt +
                              ", " + entrdCurrID + ", " + acntAmnt +
                              ", " + acntCurrID + ", " + funcExchRate +
                              ", " + acntExchRate + ", '" + dbtOrCrdt + "')";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
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
                     ", '" + trnsdte.Replace("'", "''") + "', " + crncyid + ", " + Global.myInv.user_id +
                     ", '" + dateStr + "', " + crdtamnt + ", " +
                     Global.myInv.user_id + ", '" + dateStr + "', " + netamnt +
                     ", -1, '" + srcDocTyp.Replace("'", "''") + "', " +
                     srcDocID + ", " + srcDocLnID + ", '" + trnsSrc + "')";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
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
                     ", '" + trnsdte.Replace("'", "''") + "', " + crncyid + ", " + Global.myInv.user_id +
                     ", '" + dateStr + "', " + crdtamnt + ", " +
                     Global.myInv.user_id + ", '" + dateStr + "', " + netamnt +
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

        public static void createPymntGLIntFcLn(int accntid, string trnsdesc, double dbtamnt,
      string trnsdte, int crncyid, double crdtamnt, double netamnt, string srcDocTyp,
          long srcDocID, long srcDocLnID, string dateStr)
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
            string insSQL = "INSERT INTO scm.scm_gl_interface(" +
                  "accnt_id, transaction_desc, dbt_amount, trnsctn_date, " +
                  "func_cur_id, created_by, creation_date, crdt_amount, last_update_by, " +
                  "last_update_date, net_amount, gl_batch_id, src_doc_typ, src_doc_id, " +
                  "src_doc_line_id) " +
                     "VALUES (" + accntid + ", '" + trnsdesc.Replace("'", "''") + "', " + dbtamnt +
                     ", '" + trnsdte.Replace("'", "''") + "', " + crncyid + ", " + Global.myInv.user_id +
                     ", '" + dateStr + "', " + crdtamnt + ", " +
                     Global.myInv.user_id + ", '" + dateStr + "', " + netamnt +
                     ", -1, '" + srcDocTyp.Replace("'", "''") + "', " + srcDocID + ", " + srcDocLnID + ")";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void createPymntGLIntFcLn(int accntid, string trnsdesc, double dbtamnt,
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
            string insSQL = "INSERT INTO scm.scm_gl_interface(" +
                  "accnt_id, transaction_desc, dbt_amount, trnsctn_date, " +
                  "func_cur_id, created_by, creation_date, crdt_amount, last_update_by, " +
                  "last_update_date, net_amount, gl_batch_id, src_doc_typ, src_doc_id, " +
                  "src_doc_line_id, trns_source) " +
                     "VALUES (" + accntid + ", '" + trnsdesc.Replace("'", "''") + "', " + dbtamnt +
                     ", '" + trnsdte.Replace("'", "''") + "', " + crncyid + ", " + Global.myInv.user_id +
                     ", '" + dateStr + "', " + crdtamnt + ", " +
                     Global.myInv.user_id + ", '" + dateStr + "', " + netamnt +
                     ", -1, '" + srcDocTyp.Replace("'", "''") + "', " +
                     srcDocID + ", " + srcDocLnID + ", '" + trnsSrc + "')";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        //    public static void createPaymntLine(string pymtTyp, double amnt, double curBals,
        //string payRmrk, string srcDocTyp, long srcDocID, string dateStr, string dateRcvd)
        //    {
        //        string insSQL = "INSERT INTO scm.scm_payments(" +
        //              "pymnt_type, amount_paid, custmrs_balance, pymnt_remark, " +
        //              "src_doc_typ, src_doc_id, created_by, creation_date, last_update_by, " +
        //              "last_update_date, date_rcvd) " +
        //         "VALUES ('" + pymtTyp.Replace("'", "''") + "', " + amnt + ", " + curBals +
        //         ", '" + payRmrk.Replace("'", "''") + "', '" + srcDocTyp.Replace("'", "''") +
        //         "', " + srcDocID + ", " + Global.myInv.user_id + ", '" + dateStr + "', " +
        //                 Global.myInv.user_id + ", '" + dateStr + "', '" + dateRcvd + "')";
        //        Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        //    }

        //    public static void createTodaysGLBatch(int orgid, string batchnm,
        //    string batchdesc, string batchsource)
        //    {
        //        string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
        //        string insSQL = "INSERT INTO accb.accb_trnsctn_batches(" +
        //                 "batch_name, batch_description, created_by, creation_date, " +
        //                 "org_id, batch_status, last_update_by, last_update_date, batch_source) " +
        //         "VALUES ('" + batchnm.Replace("'", "''") + "', '" + batchdesc.Replace("'", "''") +
        //         "', " + Global.myInv.user_id + ", '" + dateStr + "', " + orgid + ", '0', " +
        //                 Global.myInv.user_id + ", '" + dateStr + "', '" +
        //                 batchsource.Replace("'", "''") + "')";
        //        Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        //    }

        //    public static void createPymntGLLine(int accntid, string trnsdesc, double dbtamnt,
        //    string trnsdte, int crncyid, long batchid, double crdtamnt, double netamnt,
        //      string srcids, string dateStr)
        //    {
        //        if (accntid <= 0)
        //        {
        //            return;
        //        }
        //        string insSQL = "INSERT INTO accb.accb_trnsctn_details(" +
        //                 "accnt_id, transaction_desc, dbt_amount, trnsctn_date, " +
        //                 "func_cur_id, created_by, creation_date, batch_id, crdt_amount, " +
        //                 "last_update_by, last_update_date, net_amount, trns_status, source_trns_ids) " +
        //                 "VALUES (" + accntid + ", '" + trnsdesc.Replace("'", "''") + "', " + dbtamnt +
        //                 ", '" + trnsdte.Replace("'", "''") + "', " + crncyid + ", " + Global.myInv.user_id +
        //                 ", '" + dateStr + "', " + batchid + ", " + crdtamnt + ", " +
        //                 Global.myInv.user_id + ", '" + dateStr + "', " + netamnt +
        //                 ", '0', '" + srcids + "')";
        //        Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        //    }

        //    public static void createPymntGLIntFcLn(int accntid, string trnsdesc, double dbtamnt,
        //string trnsdte, int crncyid, double crdtamnt, double netamnt, string srcDocTyp,
        //long srcDocID, long srcDocLnID, string dateStr)
        //    {
        //        if (accntid <= 0)
        //        {
        //            return;
        //        }
        //        string insSQL = "INSERT INTO scm.scm_gl_interface(" +
        //              "accnt_id, transaction_desc, dbt_amount, trnsctn_date, " +
        //              "func_cur_id, created_by, creation_date, crdt_amount, last_update_by, " +
        //              "last_update_date, net_amount, gl_batch_id, src_doc_typ, src_doc_id, " +
        //              "src_doc_line_id) " +
        //                 "VALUES (" + accntid + ", '" + trnsdesc.Replace("'", "''") + "', " + dbtamnt +
        //                 ", '" + trnsdte.Replace("'", "''") + "', " + crncyid + ", " + Global.myInv.user_id +
        //                 ", '" + dateStr + "', " + crdtamnt + ", " +
        //                 Global.myInv.user_id + ", '" + dateStr + "', " + netamnt +
        //                 ", -1, '" + srcDocTyp.Replace("'", "''") + "', " + srcDocID + ", " + srcDocLnID + ")";
        //        Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        //    }
        #endregion

        #region "UPDATE STATEMENTS..."

        public static void updtGLIntrfcLnSpclOrg(int orgID)
        {
            //Used to update batch ids of interface lines that have gone to GL already
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE scm.scm_gl_interface a " +
            "SET gl_batch_id = (select f.batch_id from accb.accb_trnsctn_details f, accb.accb_chart_of_accnts h " +
            "where f.batch_id IN (select g.batch_id from accb.accb_trnsctn_batches g " +
            "where g.batch_name ilike '%Inventory%' and " +
            "to_timestamp(g.creation_date,'YYYY-MM-DD HH24:MI:SS') between " +
            "(to_timestamp(a.trnsctn_date,'YYYY-MM-DD HH24:MI:SS') - interval '6 months') " +
            "and (to_timestamp(a.trnsctn_date,'YYYY-MM-DD HH24:MI:SS') + interval '6 months')) and " +
            "f.source_trns_ids like '%,' || a.interface_id || ',%' and " +
            "f.trnsctn_date=a.trnsctn_date and f.accnt_id= a.accnt_id and f.accnt_id= h.accnt_id and h.org_id = " + orgID + ")" +
            ", last_update_by=" + Global.myInv.user_id + ", " +
            "last_update_date='" + dateStr + "' " +
            "WHERE a.gl_batch_id = -1 and EXISTS(select 1 from accb.accb_chart_of_accnts" +
            " m where a.accnt_id= m.accnt_id and m.org_id =" + orgID + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updtPymntAllGLIntrfcLnOrg(long glbatchid, int orgID)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE scm.scm_gl_interface a " +
            "SET gl_batch_id = " + glbatchid +
            ", last_update_by=" + Global.myInv.user_id + ", " +
            "last_update_date='" + dateStr + "' " +
            "WHERE a.gl_batch_id = -1 and EXISTS(select f.transctn_id from accb.accb_trnsctn_details f, accb.accb_chart_of_accnts g " +
            "where f.batch_id = " + glbatchid + " " +
            "and f.source_trns_ids like '%,' || a.interface_id || ',%' and " +
            "f.trnsctn_date=a.trnsctn_date and f.accnt_id= a.accnt_id and f.accnt_id= g.accnt_id and g.org_id = " + orgID + ") ";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updtCnsgmtDailyBals(long CnsgmId, double totQty,
           double rsvdQty, double avlblQty, string balsDate,
            string act_typ, string src_trnsID)
        {
            balsDate = DateTime.ParseExact(
         balsDate, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            balsDate = balsDate.Substring(0, 10);
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "";
            if (act_typ == "Undo")
            {
                updtSQL = "UPDATE inv.inv_consgmt_daily_bals " +
            "SET last_update_by = " + Global.myInv.user_id +
            ", last_update_date = '" + dateStr +
                  "', consgmt_tot_qty = COALESCE(consgmt_tot_qty,0) - " + totQty +
                  ", reservations = COALESCE(reservations,0) - " + rsvdQty +
                  ", available_balance = COALESCE(available_balance,0) - " + avlblQty +
                  ", source_trns_ids = COALESCE(replace(source_trns_ids, '," + src_trnsID + ",', ','),',')" +
            " WHERE (to_timestamp(bals_date,'YYYY-MM-DD') >=  to_timestamp('" + balsDate +
            "','YYYY-MM-DD') and consgmt_id = " + CnsgmId + ")";
            }
            else
            {
                updtSQL = "UPDATE inv.inv_consgmt_daily_bals " +
            "SET last_update_by = " + Global.myInv.user_id +
            ", last_update_date = '" + dateStr +
                  "', consgmt_tot_qty = COALESCE(consgmt_tot_qty,0) + " + totQty +
                  ", reservations = COALESCE(reservations,0) + " + rsvdQty +
                  ", available_balance = COALESCE(available_balance,0) + " + avlblQty +
                  ", source_trns_ids = COALESCE(source_trns_ids,',') || '" + src_trnsID + ",'" +
            " WHERE (to_timestamp(bals_date,'YYYY-MM-DD') >=  to_timestamp('" + balsDate +
            "','YYYY-MM-DD') and consgmt_id = " + CnsgmId + ")";
            }
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updtStckDailyBals(long skckId, double totQty,
         double rsvdQty, double avlblQty, string balsDate,
          string act_typ, string src_trnsID)
        {
            balsDate = DateTime.ParseExact(
         balsDate, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            balsDate = balsDate.Substring(0, 10);
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "";
            if (act_typ == "Undo")
            {
                updtSQL = "UPDATE inv.inv_stock_daily_bals " +
            "SET last_update_by = " + Global.myInv.user_id +
            ", last_update_date = '" + dateStr +
                  "', stock_tot_qty = COALESCE(stock_tot_qty,0) - " + totQty +
                  ", reservations = COALESCE(reservations,0) - " + rsvdQty +
                  ", available_balance = COALESCE(available_balance,0) - " + avlblQty +
                  ", source_trns_ids = COALESCE(replace(source_trns_ids, '," + src_trnsID + ",', ','),',')" +
            " WHERE (to_timestamp(bals_date,'YYYY-MM-DD') >=  to_timestamp('" + balsDate +
            "','YYYY-MM-DD') and stock_id = " + skckId + ")";
            }
            else
            {
                updtSQL = "UPDATE inv.inv_stock_daily_bals " +
            "SET last_update_by = " + Global.myInv.user_id +
            ", last_update_date = '" + dateStr +
                  "', stock_tot_qty = COALESCE(stock_tot_qty,0) + " + totQty +
                  ", reservations = COALESCE(reservations,0) + " + rsvdQty +
                  ", available_balance = COALESCE(available_balance,0) + " + avlblQty +
                  ", source_trns_ids = COALESCE(source_trns_ids,',') || '" + src_trnsID + ",'" +
            " WHERE (to_timestamp(bals_date,'YYYY-MM-DD') >=  to_timestamp('" + balsDate +
            "','YYYY-MM-DD') and stock_id = " + skckId + ")";
            }
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        /*public static void updtCnsgmtDailyBals(long CnsgmId, double totQty,
         double rsvdQty, double avlblQty, string balsDate,
          string act_typ, string src_trnsID)
        {
          balsDate = DateTime.ParseExact(
       balsDate, "dd-MMM-yyyy HH:mm:ss",
       System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
          balsDate = balsDate.Substring(0, 10);
          Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
          string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
          string updtSQL = "";
          if (act_typ == "Undo")
          {
            updtSQL = "UPDATE inv.inv_consgmt_daily_bals " +
        "SET last_update_by = " + Global.myInv.user_id +
        ", last_update_date = '" + dateStr +
              "', consgmt_tot_qty = consgmt_tot_qty - " + totQty +
              ", reservations = reservations - " + rsvdQty +
              ", available_balance = available_balance - " + avlblQty +
              ", source_trns_ids = replace(source_trns_ids, '," + src_trnsID + ",', ',')" +
        " WHERE (to_timestamp(bals_date,'YYYY-MM-DD') >=  to_timestamp('" + balsDate +
        "','YYYY-MM-DD') and consgmt_id = " + CnsgmId + ")";
          }
          else
          {
            updtSQL = "UPDATE inv.inv_consgmt_daily_bals " +
        "SET last_update_by = " + Global.myInv.user_id +
        ", last_update_date = '" + dateStr +
              "', consgmt_tot_qty = consgmt_tot_qty + " + totQty +
              ", reservations = reservations + " + rsvdQty +
              ", available_balance = available_balance + " + avlblQty +
              ", source_trns_ids = source_trns_ids || '" + src_trnsID + ",'" +
        " WHERE (to_timestamp(bals_date,'YYYY-MM-DD') >=  to_timestamp('" + balsDate +
        "','YYYY-MM-DD') and consgmt_id = " + CnsgmId + ")";
          }
          Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updtStckDailyBals(long skckId, double totQty,
         double rsvdQty, double avlblQty, string balsDate,
          string act_typ, string src_trnsID)
        {
          balsDate = DateTime.ParseExact(
       balsDate, "dd-MMM-yyyy HH:mm:ss",
       System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
          balsDate = balsDate.Substring(0, 10);
          Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
          string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
          string updtSQL = "";
          if (act_typ == "Undo")
          {
            updtSQL = "UPDATE inv.inv_stock_daily_bals " +
        "SET last_update_by = " + Global.myInv.user_id +
        ", last_update_date = '" + dateStr +
              "', stock_tot_qty = stock_tot_qty - " + totQty +
              ", reservations = reservations - " + rsvdQty +
              ", available_balance = available_balance - " + avlblQty +
              ", source_trns_ids = replace(source_trns_ids, '," + src_trnsID + ",', ',')" +
        " WHERE (to_timestamp(bals_date,'YYYY-MM-DD') >=  to_timestamp('" + balsDate +
        "','YYYY-MM-DD') and stock_id = " + skckId + ")";
          }
          else
          {
            updtSQL = "UPDATE inv.inv_stock_daily_bals " +
        "SET last_update_by = " + Global.myInv.user_id +
        ", last_update_date = '" + dateStr +
              "', stock_tot_qty = stock_tot_qty + " + totQty +
              ", reservations = reservations + " + rsvdQty +
              ", available_balance = available_balance + " + avlblQty +
              ", source_trns_ids = source_trns_ids || '" + src_trnsID + ",'" +
        " WHERE (to_timestamp(bals_date,'YYYY-MM-DD') >=  to_timestamp('" + balsDate +
        "','YYYY-MM-DD') and stock_id = " + skckId + ")";
          }
          Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }
          */
        public static void updtSrcDocTrnsctdQty(long src_lnid,
         double qty)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE scm.scm_sales_invc_det SET " +
                  "qty_trnsctd_in_dest_doc=qty_trnsctd_in_dest_doc+" + qty +
                  ", last_update_by=" + Global.myInv.user_id +
                  ", last_update_date='" + dateStr +
                  "' WHERE (invc_det_ln_id = " +
                  src_lnid + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updtSalesDocApprvl(long docid,
          string apprvlSts, string nxtApprvl)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE scm.scm_sales_invc_hdr SET " +
                  "approval_status='" + apprvlSts + "', last_update_by=" + Global.myInv.user_id +
                  ", last_update_date='" + dateStr +
                  "', next_aproval_action='" + nxtApprvl.Replace("'", "''") +
                  "' WHERE (invc_hdr_id = " +
                  docid + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updateSalesDocLn(long lnID, int itmID,
          double qty, double untPrice, int storeID,
          int crncyID, long srclnID, int txCode, int dscntCde,
          int chrgeCde, string rtrnRsn, string cnsgmntIDs, double orgnlPrice,
          bool dlvrd, long prsnID, string altrntNm, int cogsID, int salesRevID,
          int salesRetID, int purcRetID, int expnsID)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE scm.scm_sales_invc_det SET " +
                  "itm_id=" + itmID +
                  ", doc_qty =" + qty +
                  ", unit_selling_price= " + untPrice +
                  ", orgnl_selling_price= " + orgnlPrice + ", " +
                  "last_update_by = " + Global.myInv.user_id +
                  ", last_update_date= '" + dateStr + "', " +
                  "store_id=" + storeID +
                  ", crncy_id =" + crncyID + ", src_line_id = " + srclnID +
                  ", tax_code_id = " + txCode +
                  ", dscnt_code_id = " + dscntCde +
                  ", chrg_code_id = " + chrgeCde +
                  ", rtrn_reason = '" + rtrnRsn.Replace("'", "''") +
                  "', consgmnt_ids ='" + cnsgmntIDs.Replace("'", "''") +
                  "', is_itm_delivered ='" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(dlvrd) +
                  "', lnkd_person_id = " + prsnID +
                  ", alternate_item_name = '" + altrntNm.Replace("'", "''") +
                  "', cogs_acct_id=" + cogsID +
                  ", sales_rev_accnt_id=" + salesRevID +
                  ", sales_ret_accnt_id =" + salesRetID +
                  ", purch_ret_accnt_id =" + purcRetID +
                  ", expense_accnt_id =" + expnsID +
                  " WHERE (invc_det_ln_id = " + lnID + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        //public static void updateSalesLnCsgmtDist(long lnID, string cnsgmntQtys)
        //{
        //  Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
        //  string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
        //  string updtSQL = "UPDATE scm.scm_sales_invc_det SET " +
        //        "cnsgmnt_qty_dist ='" + cnsgmntQtys.Replace("'", "''") +
        //        "' WHERE (invc_det_ln_id = " + lnID + ")";
        //  Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        //}

        public static void updateSalesLnCsgmtDist(long lnID, string cnsgmntQtys)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            //string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE scm.scm_sales_invc_det SET " +
                  "cnsgmnt_qty_dist ='" + cnsgmntQtys.Replace("'", "''") +
                  "', is_itm_delivered='1' WHERE (invc_det_ln_id = " + lnID + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updateSalesLnDlvrd(long lnID, bool dlvrd)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            //string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE scm.scm_sales_invc_det SET " +
                  "is_itm_delivered='" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(dlvrd) +
                  "' WHERE (invc_det_ln_id = " + lnID + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updateSalesLnCsgmtIDs(long lnID, string cnsgmntIDs)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE scm.scm_sales_invc_det SET " +
                  "consgmnt_ids ='" + cnsgmntIDs.Replace("'", "''") +
                  "' WHERE (invc_det_ln_id = " + lnID + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }
        // public static void updtSalesDocHdr(long docid, string docNum,
        //   string desc, string docTyp, string docdte, string pymntTerms,
        //   int spplrID, int spplrSiteID, string apprvlSts,
        //   string nxtApprvl, long srcDocID,
        //   int pymntID, int invcCurrID, double exchRate, bool enblAutoChrg)
        // {
        //   docdte = DateTime.ParseExact(
        //docdte, "dd-MMM-yyyy",
        //System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
        //   Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
        //   string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
        //   string updtSQL = "UPDATE scm.scm_sales_invc_hdr SET " +
        //         "invc_date='" + docdte.Replace("'", "''") +
        //         "', payment_terms='" + pymntTerms.Replace("'", "''") +
        //         "', customer_id=" + spplrID + ", " +
        //         "customer_site_id=" + spplrSiteID + ", comments_desc='" + desc.Replace("'", "''") +
        //         "', approval_status='" + apprvlSts.Replace("'", "''") + "', last_update_by=" + Global.myInv.user_id +
        //         ", last_update_date='" + dateStr +
        //         "', next_aproval_action='" + nxtApprvl.Replace("'", "''") +
        //         "', invc_number='" + docNum.Replace("'", "''") + "', invc_type='" +
        //         docTyp.Replace("'", "''") + "', src_doc_hdr_id=" + srcDocID +
        //         ", pymny_method_id=" + pymntID + ", invc_curr_id=" + invcCurrID + ", exchng_rate=" + exchRate + " " +
        //         ", enbl_auto_misc_chrges='" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(enblAutoChrg) + "' " +
        //         "WHERE (invc_hdr_id = " +
        //         docid + ")";
        //   Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        // }

        public static void updtSalesDocHdr(long docid, string docNum,
        string desc, string docTyp, string docdte, string pymntTerms,
        int spplrID, int spplrSiteID, string apprvlSts,
        string nxtApprvl, long srcDocID,
        int pymntID, int invcCurrID, double exchRate, long chckInID,
        string chckInType, bool enblAutoChrg,
        long event_rgstr_id, string evntCtgry,
          bool allwDues, string evntType)
        {
            docdte = DateTime.ParseExact(docdte, "dd-MMM-yyyy",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE scm.scm_sales_invc_hdr SET " +
                  "invc_date='" + docdte.Replace("'", "''") +
                  "', payment_terms='" + pymntTerms.Replace("'", "''") +
                  "', customer_id=" + spplrID + ", " +
                  "customer_site_id=" + spplrSiteID +
                  ", comments_desc='" + desc.Replace("'", "''") +
                  "', approval_status='" + apprvlSts.Replace("'", "''") +
                  "', last_update_by=" + Global.myInv.user_id +
                  ", last_update_date = '" + dateStr +
                  "', next_aproval_action = '" + nxtApprvl.Replace("'", "''") +
                  "', invc_number = '" + docNum.Replace("'", "''") +
                  "', invc_type = '" + docTyp.Replace("'", "''") + "', src_doc_hdr_id=" + srcDocID +
                  ", pymny_method_id = " + pymntID + ", invc_curr_id=" + invcCurrID +
                  ", exchng_rate=" + exchRate +
                  ", other_mdls_doc_id=" + chckInID +
                  ", other_mdls_doc_type='" + chckInType.Replace("'", "''") + "' " +
                  ", enbl_auto_misc_chrges='" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(enblAutoChrg) + "' " +
                  ", event_rgstr_id = " + event_rgstr_id +
                  ", evnt_cost_category = '" + evntCtgry.Replace("'", "''") + "' " +
                  ", allow_dues = '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(allwDues) +
                  "', event_doc_type = '" + evntType.Replace("'", "''") + "' " +
                  "WHERE (invc_hdr_id = " + docid + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updtReqOrdrdQty(long src_lnid,
         double qty)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE scm.scm_prchs_docs_det SET " +
                  "rqstd_qty_ordrd=rqstd_qty_ordrd+" + qty +
                  ", last_update_by=" + Global.myInv.user_id +
                  ", last_update_date='" + dateStr +
                  "' WHERE (prchs_doc_line_id = " +
                  src_lnid + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updtPrchsDocApprvl(long docid,
          string apprvlSts, string nxtApprvl)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE scm.scm_prchs_docs_hdr SET " +
                  "approval_status='" + apprvlSts.Replace("'", "''") + "', last_update_by=" + Global.myInv.user_id +
                  ", last_update_date='" + dateStr +
                  "', next_aproval_action='" + nxtApprvl.Replace("'", "''") +
                  "' WHERE (prchs_doc_hdr_id = " +
                  docid + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updatePrchsDocLn(long lnID, int itmID,
          double qty, double untPrice, int storeID, int crncyID, long srclnID, string altrntNm)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE scm.scm_prchs_docs_det SET " +
                  "itm_id=" + itmID +
                  ", quantity =" + qty + ", unit_price= " + untPrice + ", " +
                  "last_update_by = " + Global.myInv.user_id +
                  ", last_update_date= '" + dateStr + "', " +
                  "store_id=" + storeID +
                  ", crncy_id =" + crncyID + ", src_line_id = " + srclnID +
                  ", alternate_item_name= '" + altrntNm.Replace("'", "''") + "' WHERE (prchs_doc_line_id = " + lnID + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updtPrchsDocHdr(long docid, string docNum,
          string desc, string docTyp, string docdte, string needbyDte,
          int spplrID, int spplrSiteID, string apprvlSts, string nxtApprvl, long reqID,
          int prntdInvCur, decimal exchRate, string pyTrms)
        {
            docdte = DateTime.ParseExact(
         docdte, "dd-MMM-yyyy",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

            needbyDte = DateTime.ParseExact(
         needbyDte, "dd-MMM-yyyy",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE scm.scm_prchs_docs_hdr SET " +
                  "prchs_doc_date='" + docdte.Replace("'", "''") +
                  "', need_by_date='" + needbyDte.Replace("'", "''") +
                  "', supplier_id=" + spplrID + ", " +
                  "supplier_site_id=" + spplrSiteID + ", comments_desc='" + desc.Replace("'", "''") +
                  "', approval_status='" + apprvlSts.Replace("'", "''") + "', last_update_by=" + Global.myInv.user_id +
                  ", last_update_date='" + dateStr +
                  "', next_aproval_action='" + nxtApprvl.Replace("'", "''") +
                  "', purchase_doc_num='" + docNum.Replace("'", "''") + "', purchase_doc_type='" +
                  docTyp.Replace("'", "''") + "', requisition_id=" + reqID +
                  ", prntd_doc_curr_id=" + prntdInvCur + ", exchng_rate=" + exchRate +
                  ", payment_terms='" + pyTrms.Replace("'", "''") + "' " +
                  "WHERE (prchs_doc_hdr_id = " +
                  docid + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updateSmmryItm(long smmryID, string smmryTyp,
          double amnt, bool autoCalc, string smmryNm)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            if (smmryTyp == "3Discount")
            {
                amnt = -1 * Math.Abs(amnt);
            }
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE scm.scm_doc_amnt_smmrys SET " +
                  "smmry_amnt = " + amnt +
                  ", last_update_by = " + Global.myInv.user_id + ", " +
                  "auto_calc = '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(autoCalc) +
                  "', last_update_date = '" + dateStr +
                  "', smmry_name='" + smmryNm.Replace("'", "''") + "' WHERE (smmry_id = " + smmryID + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void roundSmmryItms(long docHdrID, string docType)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string updtSQL = "UPDATE scm.scm_doc_amnt_smmrys SET " +
                  "smmry_amnt = ROUND(smmry_amnt,2) WHERE (src_doc_hdr_id = " + docHdrID +
                  " and src_doc_type='" + docType.Replace("'", "''") + "')";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }
        #endregion

        #region "DELETE STATEMENTS..."
        public static void deleteSalesSmmryItm(long docID, string docType, string smmryTyp)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string delSQL = "DELETE FROM scm.scm_doc_amnt_smmrys WHERE src_doc_hdr_id = " +
              docID + " and src_doc_type = '" + docType + "' and smmry_type = '" + smmryTyp + "' and code_id_behind = -1";
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
        }

        public static void deleteSalesSmmryItm(long docID, string docType, string smmryTyp, long codBhnd)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string delSQL = "DELETE FROM scm.scm_doc_amnt_smmrys WHERE src_doc_hdr_id = " +
              docID + " and src_doc_type = '" + docType + "' and smmry_type = '" + smmryTyp + "' and  code_id_behind= " + codBhnd;
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
        }

        public static void deleteDocSmmryItms(long docID, string docType)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string delSQL = "DELETE FROM scm.scm_doc_amnt_smmrys WHERE src_doc_hdr_id = " +
              docID + " and src_doc_type = '" + docType + "'";
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
        }

        public static void deletePrchsSmmryItm(long docID, string docType, long smmryID)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string delSQL = "DELETE FROM scm.scm_doc_amnt_smmrys WHERE src_doc_hdr_id = " +
              docID + " and src_doc_type = '" + docType + "' and smmry_id = " + smmryID + "";
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
        }

        public static void deleteSalesLnItm(long lnID)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string delSQL = "DELETE FROM scm.scm_sales_invc_det WHERE invc_det_ln_id = " +
              lnID + "";
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
        }

        public static void deleteInptRunItm(long lnID)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string delSQL = "DELETE FROM scm.scm_process_run_inpts WHERE inpt_id = " +
              lnID + "";
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
        }

        public static void deleteInptDefItm(long lnID)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string delSQL = "DELETE FROM scm.scm_process_def_inpts WHERE inpt_id = " +
              lnID + "";
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
        }

        public static void deleteOutptRunItm(long lnID)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string delSQL = "DELETE FROM scm.scm_process_run_outpts WHERE inpt_id = " +
              lnID + "";
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
        }

        public static void deleteOutptDefItm(long lnID)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string delSQL = "DELETE FROM scm.scm_process_def_outpts WHERE inpt_id = " +
              lnID + "";
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
        }

        public static void deleteStagesRunItm(long lnID)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string delSQL = "DELETE FROM scm.scm_process_run_stages WHERE run_stage_id = " +
              lnID + "";
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
        }

        public static void deleteStagesDefItm(long lnID)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string delSQL = "DELETE FROM scm.scm_process_def_outpts WHERE stage_id = " +
              lnID + "";
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
        }

        public static void deleteSalesDoc(long docID)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Deleting a Sales Document and all its Lines";
            string delSQL = "DELETE FROM scm.scm_sales_invc_det WHERE invc_hdr_id = " +
              docID + "";
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
            delSQL = "DELETE FROM scm.scm_sales_invc_hdr WHERE invc_hdr_id = " +
           docID + "";
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
        }


        public static void deletePrcsDef(long docID)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Deleting a Process Definition and all its Lines";
            string delSQL = "DELETE FROM scm.scm_process_def_stages WHERE process_def_id = " +
              docID + "";
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
            delSQL = "DELETE FROM scm.scm_process_def_outpts WHERE process_def_id = " +
               docID + "";
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
            delSQL = "DELETE FROM scm.scm_process_def_inpts WHERE process_def_id = " +
              docID + "";
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
            delSQL = "DELETE FROM scm.scm_process_definition WHERE process_def_id = " +
           docID + "";
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
        }

        public static void deletePrcsRun(long docID)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Deleting a Process Run and all its Lines";
            string delSQL = "DELETE FROM scm.scm_process_run_stages WHERE process_run_id = " +
              docID + "";
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
            delSQL = "DELETE FROM scm.scm_process_run_outpts WHERE process_run_id = " +
               docID + "";
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
            delSQL = "DELETE FROM scm.scm_process_run_inpts WHERE process_run_id = " +
              docID + "";
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
            delSQL = "DELETE FROM scm.scm_process_run WHERE process_run_id = " +
           docID + "";
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
        }

        public static void deletePrchsDoc(long docID)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Deleting a Purchase Document and all its Lines";
            string delSQL = "DELETE FROM scm.scm_prchs_docs_det WHERE prchs_doc_hdr_id = " +
              docID + "";
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
            delSQL = "DELETE FROM scm.scm_prchs_docs_hdr WHERE prchs_doc_hdr_id = " +
           docID + "";
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
        }

        public static void deletePrchsLnItm(long lnID)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string delSQL = "DELETE FROM scm.scm_prchs_docs_det WHERE prchs_doc_line_id = " +
              lnID + "";
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
        }

        public static void deleteDocGLInfcLns(long docID, string srcDocType)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string delSQL = "DELETE FROM scm.scm_gl_interface WHERE src_doc_id = " +
              docID + " and src_doc_typ ilike '%" + srcDocType.Replace("'", "''") + "%' and gl_batch_id = -1";
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
        }

        public static void deleteBrknDocGLInfcLns()
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string delSQL = @"DELETE FROM scm.scm_gl_interface 
WHERE scm.get_src_doc_num(src_doc_id,src_doc_typ) IS NULL 
or scm.get_src_doc_num(src_doc_id, src_doc_typ)=''";
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
        }

        public static void deleteGLInfcLine(long intfcID)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string delSQL = "DELETE FROM scm.scm_gl_interface WHERE interface_id = " +
              intfcID + " and gl_batch_id = -1";
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
        }

        public static void deletePymntLn(long pymntID)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string delSQL = "DELETE FROM scm.scm_payments WHERE pymnt_id = " +
              pymntID;
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
        }

        public static void deletePymtGLInfcLns(long docID, string docType, long pymntID)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string delSQL = "DELETE FROM scm.scm_gl_interface WHERE src_doc_id = " +
              docID + " and gl_batch_id = -1 and src_doc_typ = '" +
              (docType + " (Payment)").Replace("'", "''") + "' and src_doc_line_id = " + pymntID;
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
        }
        #endregion

        #region "SELECT STATEMENTS..."
        #region "ITEMS..."
        public static string getItmUOM(string parItmCode)
        {
            string qryItmUOM = "SELECT uom_name FROM inv.unit_of_measure WHERE uom_id = " +
                " (SELECT base_uom_id FROM inv.inv_itm_list WHERE item_code = '" + parItmCode.Replace("'", "''")
                + "' AND org_id = " + Global.mnFrm.cmCde.Org_id + ")";

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryItmUOM);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public static string getItmCode(int itmID)
        {
            string qryItmUOM = "SELECT item_code FROM inv.inv_itm_list WHERE item_id = " + itmID + "";

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryItmUOM);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public static int getItmUOMID(string parItmCode)
        {
            string qryItmUOM = "SELECT uom_id FROM inv.unit_of_measure WHERE uom_id = " +
                " (SELECT base_uom_id FROM inv.inv_itm_list WHERE item_code = '" + parItmCode.Replace("'", "''")
                + "' AND org_id = " + Global.mnFrm.cmCde.Org_id + ")";

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryItmUOM);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return int.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public static DataSet get_ItemExtInf(long itmID)
        {
            string strSql = "";

            strSql = @"SELECT a.image, a.extra_info, a.other_desc, generic_name, trade_name, drug_usual_dsge, drug_max_dsge, 
       contraindications, food_interactions " +
          "FROM inv.inv_itm_list a WHERE a.item_id = " + itmID;
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static string getStoreNm(long storeID)
        {
            string strSql = "";
            strSql = "SELECT a.subinv_name " +
          "FROM inv.inv_itm_subinventories a " +
          "WHERE(a.subinv_id = " + storeID + ")";
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

        public static DataSet get_AllConsignments(string searchWord, string searchIn,
        Int64 offset, int limit_size, int orgID, long cstmrSiteID)
        {
            string strSql = "";
            string wherecls = "";
            string invCls = "";
            string extInvCls = "";
            string itmTyp = " AND (a.item_type != 'Expense Item') AND (a.item_type != 'Services')";

            if (searchIn == "Item Code/Name")
            {
                wherecls = "(a.item_code ilike '" + searchWord.Replace("'", "''") +
               "') AND ";
            }
            else if (searchIn == "Item Description")
            {
                wherecls = "(a.item_desc ilike '" + searchWord.Replace("'", "''") +
               "') AND ";
            }

            strSql = "SELECT distinct a.item_id, a.item_code, a.item_desc, " +
              "a.selling_price, a.category_id, b.stock_id, b.subinv_id, b.shelves, " +
              "a.tax_code_id, CASE WHEN scm.get_cstmr_splr_dscntid("
               + cstmrSiteID + ") != -1 THEN scm.get_cstmr_splr_dscntid("
               + cstmrSiteID + @") ELSE a.dscnt_code_id END , a.extr_chrg_id, c.consgmt_id, c.cost_price, c.expiry_date " +
            "FROM inv.inv_itm_list a, inv.inv_stock b, inv.inv_consgmt_rcpt_det c " +
            "WHERE (" + wherecls + "(a.item_id = b.itm_id and b.stock_id = c.stock_id " +
            "and a.item_id = c.itm_id and b.subinv_id = c.subinv_id and a.enabled_flag='1')" + invCls +
            " AND (a.org_id = " + orgID +
            ")" + extInvCls + itmTyp + ") ORDER BY c.consgmt_id ASC, a.item_code LIMIT " + limit_size +
            " OFFSET " + (Math.Abs(offset * limit_size)).ToString();


            Global.itms_SQL = strSql;
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static DataSet get_StoreItems(string searchWord, string searchIn,
        Int64 offset, int limit_size, int orgID, int storeID, string docTyp,
          bool cnsgmtsOnly, long itmID, long cstmrSiteID)
        {
            string strSql = "";
            string wherecls = "";
            string invCls = "";
            string extInvCls = "";
            string itmTyp = "";
            if (docTyp == "Sales Invoice"
              || docTyp == "Pro-Forma Invoice"
              || docTyp == "Sales Order")
            {
                itmTyp = " AND ((a.item_type = 'Merchandise Inventory' AND b.subinv_id = " + storeID + ") OR a.item_type = 'Services')";
                invCls = "";
                extInvCls = " AND (now() between to_timestamp(b.start_date, " +
              "'YYYY-MM-DD HH24:MI:SS') AND to_timestamp(CASE WHEN b.end_date='' " +
              "THEN '4000-12-31 23:59:59' ELSE b.end_date END, " +
              "'YYYY-MM-DD HH24:MI:SS'))";
            }
            else if (docTyp == "Internal Item Request")
            {
                //itmTyp = " AND (a.item_type != 'Expense Item') AND (a.item_type != 'Services')";
            }
            else if (docTyp == "Item Issue-Unbilled")
            {
                //itmTyp = " AND (a.item_type != 'Expense Item') AND (a.item_type != 'Services')";
                invCls = " AND (b.subinv_id = " + storeID + ")";
                extInvCls = " AND (now() between to_timestamp(b.start_date, " +
                "'YYYY-MM-DD HH24:MI:SS') AND to_timestamp(CASE WHEN b.end_date='' THEN '4000-12-31 23:59:59' ELSE b.end_date END, " +
                "'YYYY-MM-DD HH24:MI:SS'))";
            }
            else if (docTyp == "")
            {
                invCls = " AND (b.subinv_id = " + storeID + ")";
            }
            if (searchIn == "Item Code/Name")
            {
                wherecls = "(a.item_code ilike '" + searchWord.Replace("'", "''") +
               "' or a.item_desc ilike '" + searchWord.Replace("'", "''") +
               "') AND ";
            }
            else if (searchIn == "Item Description")
            {
                wherecls = "(a.item_code ilike '" + searchWord.Replace("'", "''") +
               "' or a.item_desc ilike '" + searchWord.Replace("'", "''") +
               "') AND ";
            }
            if (cnsgmtsOnly == true)
            {
                strSql = "SELECT distinct a.item_id, a.item_code, a.item_desc, " +
                  "a.selling_price, a.category_id, b.stock_id, b.subinv_id, b.shelves, " +
                  "a.tax_code_id, CASE WHEN scm.get_cstmr_splr_dscntid("
                  + cstmrSiteID + ") != -1 THEN scm.get_cstmr_splr_dscntid("
                  + cstmrSiteID + @") ELSE a.dscnt_code_id END , a.extr_chrg_id, c.consgmt_id, c.cost_price, c.expiry_date " +
                "FROM inv.inv_itm_list a, inv.inv_stock b, inv.inv_consgmt_rcpt_det c " +
                "WHERE (" + wherecls + "(a.item_id = b.itm_id and b.stock_id = c.stock_id " +
                "and a.item_id = c.itm_id and b.subinv_id = c.subinv_id and a.enabled_flag='1' and a.item_id=" + itmID + ")" + invCls +
                " AND (a.org_id = " + orgID +
                ")" + extInvCls + itmTyp + ") ORDER BY c.consgmt_id ASC, a.item_code LIMIT " + limit_size +
                " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            else
            {
                strSql = "SELECT distinct a.item_id, a.item_code, a.item_desc, " +
              "a.selling_price, a.category_id, COALESCE(b.stock_id,-1), COALESCE(b.subinv_id,-1), b.shelves, " +
              "a.tax_code_id, CASE WHEN scm.get_cstmr_splr_dscntid("
                  + cstmrSiteID + ") != -1 THEN scm.get_cstmr_splr_dscntid("
                  + cstmrSiteID + @") ELSE a.dscnt_code_id END , a.extr_chrg_id " +
            "FROM inv.inv_itm_list a LEFT OUTER JOIN inv.inv_stock b ON a.item_id = b.itm_id " + extInvCls +
            " WHERE (" + wherecls + "(a.enabled_flag='1')" + invCls +
            " AND (a.org_id = " + orgID +
            ")" + itmTyp + ") ORDER BY a.item_code LIMIT " + limit_size +
            " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            Global.itms_SQL = strSql;
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static long get_Total_StoreItms(
          string searchWord, string searchIn,
          int orgID, int storeID, string docTyp, bool cnsgmtsOnly, long itmID)
        {
            string strSql = "";
            string wherecls = "";
            string invCls = "";
            string extInvCls = "";
            string itmTyp = "";
            if (docTyp == "Sales Invoice"
              || docTyp == "Pro-Forma Invoice"
              || docTyp == "Sales Order")
            {
                itmTyp = " AND ((a.item_type = 'Merchandise Inventory' AND b.subinv_id = " + storeID + ") OR a.item_type = 'Services')";
                invCls = "";
                extInvCls = " AND (now() between to_timestamp(b.start_date, " +
              "'YYYY-MM-DD HH24:MI:SS') AND to_timestamp(CASE WHEN b.end_date='' " +
              "THEN '4000-12-31 23:59:59' ELSE b.end_date END, " +
              "'YYYY-MM-DD HH24:MI:SS'))";
            }
            else if (docTyp == "Internal Item Request")
            {
                itmTyp = " AND (a.item_type != 'Expense Item') AND (a.item_type != 'Services')";
            }
            else if (docTyp == "Item Issue-Unbilled")
            {
                itmTyp = " AND (a.item_type != 'Expense Item') AND (a.item_type != 'Services')";
                invCls = " AND (b.subinv_id = " + storeID + ")";
                extInvCls = " AND (now() between to_timestamp(b.start_date, " +
                "'YYYY-MM-DD HH24:MI:SS') AND to_timestamp(CASE WHEN b.end_date='' THEN '4000-12-31 23:59:59' ELSE b.end_date END, " +
                "'YYYY-MM-DD HH24:MI:SS'))";
            }
            if (searchIn == "Item Code/Name")
            {
                wherecls = "(a.item_code ilike '" + searchWord.Replace("'", "''") +
               "') AND ";
            }
            else if (searchIn == "Item Description")
            {
                wherecls = "(a.item_desc ilike '" + searchWord.Replace("'", "''") +
               "') AND ";
            }
            if (cnsgmtsOnly == true)
            {
                strSql = "SELECT count(distinct c.consgmt_id) " +
                "FROM inv.inv_itm_list a, inv.inv_stock b, inv.inv_consgmt_rcpt_det c " +
                "WHERE (" + wherecls + "(a.item_id = b.itm_id and b.stock_id = c.stock_id " +
                "and a.item_id = c.itm_id and b.subinv_id = c.subinv_id and a.enabled_flag='1' and a.item_id=" + itmID + ")" + invCls +
                " AND (a.org_id = " + orgID +
                ")" + extInvCls + itmTyp + ")";
            }
            else
            {
                strSql = "SELECT count(1)" +
            "FROM inv.inv_itm_list a LEFT OUTER JOIN inv.inv_stock b ON a.item_id = b.itm_id " + extInvCls +
            " WHERE (" + wherecls + "(a.enabled_flag='1')" + invCls +
            " AND (a.org_id = " + orgID +
            ")" + itmTyp + ")";
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

        public static string getOldstItmCnsgmts(long itmID, double qnty)
        {
            string res = ",";
            string strSql = "SELECT distinct c.consgmt_id, inv.get_csgmt_lst_avlbl_bls(c.consgmt_id) " +
              "FROM inv.inv_consgmt_rcpt_det c " +
              "WHERE ((c.itm_id=" + itmID + ") and (c.subinv_id =" + Global.selectedStoreID + ") and  (inv.get_csgmt_lst_avlbl_bls(c.consgmt_id)>0)) ORDER BY c.consgmt_id ASC";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            double curAvlbQty = 0;
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                if (curAvlbQty < qnty)
                {
                    res = res + dtst.Tables[0].Rows[i][0].ToString() + ",";
                    curAvlbQty = curAvlbQty + double.Parse(dtst.Tables[0].Rows[i][1].ToString());
                }
                else
                {
                    return res.Trim(',');
                }
            }
            return res.Trim(',');
        }

        public static string getOldstItmCnsgmtsForStock(long itmID, double qnty, long storeID)
        {
            string res = ",";
            string strSql = "SELECT distinct c.consgmt_id, inv.get_csgmt_lst_avlbl_bls(c.consgmt_id) " +
              "FROM inv.inv_consgmt_rcpt_det c " +
              "WHERE ((c.itm_id=" + itmID + ") and (c.subinv_id =" + storeID + ") and (inv.get_csgmt_lst_avlbl_bls(c.consgmt_id)>0)) ORDER BY c.consgmt_id ASC";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            double curAvlbQty = 0;
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                if (curAvlbQty < qnty)
                {
                    res = res + dtst.Tables[0].Rows[i][0].ToString() + ",";
                    curAvlbQty = curAvlbQty + double.Parse(dtst.Tables[0].Rows[i][1].ToString());
                }
                else
                {
                    return res.Trim(',');
                }
            }
            return res.Trim(',');
        }

        public static List<string> getOldstItmCnsgmtsNCstPrcLstForStock(long itmID, double qnty, int storeID)
        {
            List<string> result = new List<string>();
            string resCnsgmntIDs = ",";
            string resCnsgmntIDCstPrce = ",";
            string strSql = "SELECT distinct c.consgmt_id, cost_price, inv.get_csgmt_lst_avlbl_bls(c.consgmt_id) " +
              "FROM inv.inv_consgmt_rcpt_det c " +
              "WHERE ((c.itm_id=" + itmID + ") and (c.subinv_id =" + storeID + ") and (inv.get_csgmt_lst_avlbl_bls(c.consgmt_id)>0)) ORDER BY c.consgmt_id ASC";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            double curAvlbQty = 0;
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                if (curAvlbQty < qnty)
                {
                    resCnsgmntIDs = resCnsgmntIDs + dtst.Tables[0].Rows[i][0].ToString() + ",";
                    resCnsgmntIDCstPrce = resCnsgmntIDCstPrce + dtst.Tables[0].Rows[i][1].ToString() + ",";
                    curAvlbQty = curAvlbQty + double.Parse(dtst.Tables[0].Rows[i][2].ToString());
                }
                else
                {
                    result.Add(resCnsgmntIDs.Trim(','));
                    result.Add(resCnsgmntIDCstPrce.Trim(','));
                    return result;
                }
            }
            result.Add(resCnsgmntIDs.Trim(','));
            result.Add(resCnsgmntIDCstPrce.Trim(','));
            return result;
        }

        public static double getCnsgmtsQtySum(string cnsgmtIDs)
        {
            //MessageBox.Show(cnsgmtIDs);
            cnsgmtIDs = cnsgmtIDs.Replace(",,", ",").Replace(",,", ",").Replace(",,", ",").Trim(',');
            if (cnsgmtIDs == "")
            {
                cnsgmtIDs = "-123412";
            }
            string strSql = "SELECT distinct c.consgmt_id, inv.get_csgmt_lst_avlbl_bls(c.consgmt_id) " +
              "FROM inv.inv_consgmt_rcpt_det c " +
              "WHERE ((c.consgmt_id IN (" + cnsgmtIDs.Trim(',') +
              ")) and (inv.get_csgmt_lst_avlbl_bls(c.consgmt_id)>0)) ORDER BY c.consgmt_id ASC";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            double ttlQty = 0;
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                ttlQty = ttlQty + double.Parse(dtst.Tables[0].Rows[i][1].ToString());
            }
            return ttlQty;
        }

        public static double getCnsgmtsRsvdSum(string cnsgmtIDs)
        {
            if (cnsgmtIDs == "")
            {
                cnsgmtIDs = "-123412";
            }
            string strSql = "SELECT distinct c.consgmt_id, inv.get_csgmt_lst_rsvd_bls(c.consgmt_id) " +
              "FROM inv.inv_consgmt_rcpt_det c " +
              "WHERE ((c.consgmt_id IN (" + cnsgmtIDs.Trim(',') + ")) and (inv.get_csgmt_lst_rsvd_bls(c.consgmt_id)>0)) ORDER BY c.consgmt_id ASC";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            double ttlQty = 0;
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                ttlQty = ttlQty + double.Parse(dtst.Tables[0].Rows[i][1].ToString());
            }
            return ttlQty;
        }

        public static double getHgstUnitCostPrice(int itmID)
        {
            string strSql = "SELECT c.cost_price " +
         "FROM inv.inv_consgmt_rcpt_det c " +
         "WHERE (c.itm_id =" + itmID + ") ORDER BY c.consgmt_id DESC LIMIT 1 OFFSET 0";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return double.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return 0;
        }

        public static void updateSellingPrice(int itemID, double nwPrice, double orgnlPrice)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE inv.inv_itm_list SET " +
                  "selling_price =" + nwPrice +
                  ",orgnl_selling_price =" + orgnlPrice +
                  " WHERE (item_id = " + itemID + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);

        }

        public static void clearHistoricalBalances()
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Clear Balance History";
            string delSQL = @"DELETE FROM inv.inv_consgmt_daily_bals WHERE bal_id 
NOT IN (SELECT a.bal_id FROM inv.inv_consgmt_daily_bals a, (SELECT consgmt_id, max(bals_date) baldate
   FROM inv.inv_consgmt_daily_bals
   GROUP BY consgmt_id) b where a.consgmt_id=b.consgmt_id and a.bals_date=b.baldate
   and a.consgmt_tot_qty !=0);

   DELETE FROM inv.inv_stock_daily_bals WHERE bal_id NOT IN (SELECT a.bal_id FROM inv.inv_stock_daily_bals a, (SELECT stock_id, max(bals_date) baldate
  FROM inv.inv_stock_daily_bals GROUP BY stock_id) b where a.stock_id=b.stock_id and a.bals_date=b.baldate
   and a.stock_tot_qty !=0);";
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);

        }

        public static DataSet getIncorrectBalances()
        {
            string selSQL = @"Select tbl1.itm_id, tbl1.subinv_id, SUM(tbl1.col2) consg_tot_qty, 
SUM(tbl1.col3) consg_rsrv, SUM(tbl1.col4) consg_avlbl, 
    tbl2.stock_tot_qty, tbl2.reservations stco_rsrv, tbl2.available_balance stoc_avlbl, 
tbl1.CODE, tbl2.stock_id, tbl2.bals_date, tbl2.bal_id
    from (select distinct a.itm_id,
    (select item_code from inv.inv_itm_list where item_id = a.itm_id AND org_id = "
              + Global.mnFrm.cmCde.Org_id + @") CODE, 
    (select item_desc from inv.inv_itm_list where item_id = a.itm_id AND org_id = "
              + Global.mnFrm.cmCde.Org_id + @") DESCRIPTION, 
    a.subinv_id, 
    (select subinv_name from inv.inv_itm_subinventories where subinv_id = a.subinv_id AND org_id = "
              + Global.mnFrm.cmCde.Org_id + @") STORE, 
    a.expiry_date, 
    a.consgmt_id, 
    max(to_date(bals_date,'YYYY-MM-DD')) baldte, 
    a.cost_price ,
    b.consgmt_tot_qty col2,
    COALESCE(b.reservations,0) col3,
    b.available_balance col4
    from inv.inv_consgmt_rcpt_det a 
    inner join inv.inv_consgmt_daily_bals b on a.consgmt_id = b.consgmt_id
    inner join inv.inv_itm_list c on a.itm_id = c.item_id 
    WHERE c.org_id = "
              + Global.mnFrm.cmCde.Org_id + @"
    GROUP BY 1,2,3,4,5,6,7,9,10,11,12 
    HAVING max(to_date(bals_date,'YYYY-MM-DD'))=(select max(to_date(w.bals_date,'YYYY-MM-DD')) 
    from inv.inv_consgmt_daily_bals w where w.consgmt_id = a.consgmt_id)
    ORDER BY 2,7) tbl1, inv.inv_stock_daily_bals tbl2, inv.inv_stock tbl3
    WHERE tbl2.stock_id=tbl3.stock_id and tbl3.itm_id=tbl1.itm_id and tbl3.subinv_id = tbl1.subinv_id
    GROUP BY 1,2,6,7,8,9,10,11,12
    HAVING (SUM(tbl1.col2)!=tbl2.stock_tot_qty or SUM(tbl1.col3) != tbl2.reservations or SUM(tbl1.col4) != tbl2.available_balance)
    /*,tbl1.baldte,13AND to_date(tbl2.bals_date,'YYYY-MM-DD')=tbl1.baldte*/
    AND max(to_date(tbl2.bals_date,'YYYY-MM-DD'))=(select max(to_date(c.bals_date,'YYYY-MM-DD')) 
from inv.inv_stock_daily_bals c where c.stock_id = tbl2.stock_id)
    ORDER BY 9,11;";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
            return dtst;
        }

        public static void delInvalidBals()
        {
            /* and b.creation_date  ilike a.bals_date || '%' and b.creation_date  ilike a.bals_date || '%'*/
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Auto Correct Latest Stock/Balances Using Consignment Balances";
            string delSQL = @"
DELETE FROM inv.inv_consgmt_daily_bals WHERE bal_id IN (select distinct a.bal_id 
from inv.inv_consgmt_daily_bals a, inv.inv_consgmt_rcpt_det b where a.consgmt_id =b.consgmt_id
and (b.stock_id<=0 or b.subinv_id <=0));   

DELETE FROM inv.inv_stock_daily_bals WHERE bal_id IN (select distinct a.bal_id 
from inv.inv_stock_daily_bals a, inv.inv_consgmt_rcpt_det b where a.stock_id =b.stock_id
and (b.stock_id<=0 or b.subinv_id <=0));   
";
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
        }

        public static void zeroInterfaceValues(int orgID)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Clearing GL-Inventory Interface Values";
            string updtSQL = @"UPDATE scm.scm_gl_interface
   SET dbt_amount=0, crdt_amount=0, net_amount=0 
 WHERE gl_batch_id<=0 and accnt_id IN (select b.accnt_id from accb.accb_chart_of_accnts b where b.org_id=" + orgID + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void correct_Cnsg_Stck_QtyImbals()
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Auto Correct Latest Stock/Balances Using Consignment Balances";

            string updtSQL = @"
UPDATE inv.inv_stock_daily_bals SET stock_tot_qty =(COALESCE(reservations,0)+available_balance) where stock_tot_qty != (COALESCE(reservations,0)+available_balance);
UPDATE inv.inv_consgmt_daily_bals SET consgmt_tot_qty =(COALESCE(reservations,0)+available_balance) where consgmt_tot_qty != (COALESCE(reservations,0)+available_balance);

UPDATE inv.inv_stock_daily_bals SET stock_tot_qty=0, reservations=0, available_balance=0 where stock_tot_qty<0 or COALESCE(reservations,0)<0 or available_balance<0;
UPDATE inv.inv_consgmt_daily_bals SET consgmt_tot_qty=0, reservations=0, available_balance=0 where consgmt_tot_qty<0 or COALESCE(reservations,0)<0 or available_balance<0;

UPDATE inv.inv_stock_daily_bals k SET stock_tot_qty=(SELECT tbl5.cns_tot FROM (Select tbl1.itm_id, tbl1.subinv_id, SUM(tbl1.col2) cns_tot, SUM(tbl1.col3) cons_rsrv, SUM(tbl1.col4) cons_avlbl, 
tbl2.stock_tot_qty, tbl2.reservations, tbl2.available_balance, tbl1.CODE, tbl2.stock_id, tbl2.bals_date, tbl2.bal_id
from (select distinct a.itm_id,
(select item_code from inv.inv_itm_list where item_id = a.itm_id AND org_id = "
              + Global.mnFrm.cmCde.Org_id + @") CODE, 
(select item_desc from inv.inv_itm_list where item_id = a.itm_id AND org_id = "
              + Global.mnFrm.cmCde.Org_id + @") DESCRIPTION, 
a.subinv_id, 
(select subinv_name from inv.inv_itm_subinventories where subinv_id = a.subinv_id AND org_id = "
              + Global.mnFrm.cmCde.Org_id + @") STORE, 
a.expiry_date, 
a.consgmt_id, 
max(to_date(bals_date,'YYYY-MM-DD')) baldte, 
a.cost_price ,
b.consgmt_tot_qty col2,
COALESCE(b.reservations,0) col3,
b.available_balance col4
from inv.inv_consgmt_rcpt_det a 
inner join inv.inv_consgmt_daily_bals b on a.consgmt_id = b.consgmt_id
inner join inv.inv_itm_list c on a.itm_id = c.item_id 
WHERE c.org_id = "
              + Global.mnFrm.cmCde.Org_id + @"
GROUP BY 1,2,3,4,5,6,7,9,10,11,12 
HAVING max(to_date(bals_date,'YYYY-MM-DD'))=(select max(to_date(w.bals_date,'YYYY-MM-DD')) 
from inv.inv_consgmt_daily_bals w where w.consgmt_id = a.consgmt_id)
ORDER BY 2,7) tbl1, inv.inv_stock_daily_bals tbl2, inv.inv_stock tbl3
WHERE tbl2.stock_id=tbl3.stock_id and tbl3.itm_id=tbl1.itm_id and tbl3.subinv_id = tbl1.subinv_id
GROUP BY 1,2,6,7,8,9,10,11,12
HAVING (SUM(tbl1.col2)!=tbl2.stock_tot_qty or SUM(tbl1.col3) != tbl2.reservations or SUM(tbl1.col4) != tbl2.available_balance)
/*AND to_date(tbl2.bals_date,'YYYY-MM-DD')=tbl1.baldte*/
AND max(to_date(tbl2.bals_date,'YYYY-MM-DD'))=(select max(to_date(c.bals_date,'YYYY-MM-DD')) 
from inv.inv_stock_daily_bals c where c.stock_id = tbl2.stock_id)
ORDER BY 9,11) tbl5
WHERE tbl5.bal_id = k.bal_id), reservations=(SELECT tbl5.cons_rsrv FROM (Select tbl1.itm_id, tbl1.subinv_id, 
SUM(tbl1.col2) cns_tot, SUM(tbl1.col3) cons_rsrv, SUM(tbl1.col4) cons_avlbl, 
tbl2.stock_tot_qty, tbl2.reservations, tbl2.available_balance, tbl1.CODE, tbl2.stock_id, tbl2.bals_date, tbl2.bal_id
from (select distinct a.itm_id,
(select item_code from inv.inv_itm_list where item_id = a.itm_id AND org_id = "
              + Global.mnFrm.cmCde.Org_id + @") CODE, 
(select item_desc from inv.inv_itm_list where item_id = a.itm_id AND org_id = "
              + Global.mnFrm.cmCde.Org_id + @") DESCRIPTION, 
a.subinv_id, 
(select subinv_name from inv.inv_itm_subinventories where subinv_id = a.subinv_id AND org_id = "
              + Global.mnFrm.cmCde.Org_id + @") STORE, 
a.expiry_date, 
a.consgmt_id, 
max(to_date(bals_date,'YYYY-MM-DD')) baldte, 
a.cost_price ,
b.consgmt_tot_qty col2,
COALESCE(b.reservations,0) col3,
b.available_balance col4
from inv.inv_consgmt_rcpt_det a 
inner join inv.inv_consgmt_daily_bals b on a.consgmt_id = b.consgmt_id
inner join inv.inv_itm_list c on a.itm_id = c.item_id 
WHERE c.org_id = "
              + Global.mnFrm.cmCde.Org_id + @"
GROUP BY 1,2,3,4,5,6,7,9,10,11,12 
HAVING max(to_date(bals_date,'YYYY-MM-DD'))=(select max(to_date(w.bals_date,'YYYY-MM-DD')) 
from inv.inv_consgmt_daily_bals w where w.consgmt_id = a.consgmt_id) 
ORDER BY 2,7) tbl1, inv.inv_stock_daily_bals tbl2, inv.inv_stock tbl3
WHERE tbl2.stock_id=tbl3.stock_id and tbl3.itm_id=tbl1.itm_id and tbl3.subinv_id = tbl1.subinv_id
GROUP BY 1,2,6,7,8,9,10,11,12
HAVING (SUM(tbl1.col2)!=tbl2.stock_tot_qty or SUM(tbl1.col3) != tbl2.reservations or SUM(tbl1.col4) != tbl2.available_balance)
/*AND to_date(tbl2.bals_date,'YYYY-MM-DD')=tbl1.baldte*/
AND max(to_date(tbl2.bals_date,'YYYY-MM-DD'))=(select max(to_date(c.bals_date,'YYYY-MM-DD')) 
from inv.inv_stock_daily_bals c where c.stock_id = tbl2.stock_id)
ORDER BY 9,11) tbl5
WHERE tbl5.bal_id = k.bal_id), available_balance =(SELECT tbl5.cons_avlbl FROM (Select tbl1.itm_id, tbl1.subinv_id, 
SUM(tbl1.col2) cns_tot, SUM(tbl1.col3) cons_rsrv, SUM(tbl1.col4) cons_avlbl, 
tbl2.stock_tot_qty, tbl2.reservations, tbl2.available_balance, tbl1.CODE, tbl2.stock_id, tbl2.bals_date, tbl2.bal_id
from (select distinct a.itm_id,
(select item_code from inv.inv_itm_list where item_id = a.itm_id AND org_id = "
              + Global.mnFrm.cmCde.Org_id + @") CODE, 
(select item_desc from inv.inv_itm_list where item_id = a.itm_id AND org_id = "
              + Global.mnFrm.cmCde.Org_id + @") DESCRIPTION, 
a.subinv_id, 
(select subinv_name from inv.inv_itm_subinventories where subinv_id = a.subinv_id AND org_id = "
              + Global.mnFrm.cmCde.Org_id + @") STORE, 
a.expiry_date, 
a.consgmt_id, 
max(to_date(bals_date,'YYYY-MM-DD')) baldte, 
a.cost_price ,
b.consgmt_tot_qty col2,
COALESCE(b.reservations,0) col3,
b.available_balance col4
from inv.inv_consgmt_rcpt_det a 
inner join inv.inv_consgmt_daily_bals b on a.consgmt_id = b.consgmt_id
inner join inv.inv_itm_list c on a.itm_id = c.item_id 
WHERE c.org_id = "
              + Global.mnFrm.cmCde.Org_id + @"
GROUP BY 1,2,3,4,5,6,7,9,10,11,12 
HAVING max(to_date(bals_date,'YYYY-MM-DD'))=(select max(to_date(w.bals_date,'YYYY-MM-DD')) 
from inv.inv_consgmt_daily_bals w where w.consgmt_id = a.consgmt_id) 
ORDER BY 2,7) tbl1, inv.inv_stock_daily_bals tbl2, inv.inv_stock tbl3
WHERE tbl2.stock_id=tbl3.stock_id and tbl3.itm_id=tbl1.itm_id and tbl3.subinv_id = tbl1.subinv_id
GROUP BY 1,2,6,7,8,9,10,11,12
HAVING (SUM(tbl1.col2)!=tbl2.stock_tot_qty or SUM(tbl1.col3) != tbl2.reservations or SUM(tbl1.col4) != tbl2.available_balance)
/*AND to_date(tbl2.bals_date,'YYYY-MM-DD')=tbl1.baldte*/
AND max(to_date(tbl2.bals_date,'YYYY-MM-DD'))=(select max(to_date(c.bals_date,'YYYY-MM-DD')) 
from inv.inv_stock_daily_bals c where c.stock_id = tbl2.stock_id)
ORDER BY 9,11) tbl5
WHERE tbl5.bal_id = k.bal_id)
WHERE bal_id IN (SELECT DISTINCT tbl5.bal_id FROM (Select tbl1.itm_id, tbl1.subinv_id, SUM(tbl1.col2) cns_tot, SUM(tbl1.col3) cons_rsrv, SUM(tbl1.col4) cons_avlbl, 
tbl2.stock_tot_qty, tbl2.reservations, tbl2.available_balance, tbl1.CODE, tbl2.stock_id, tbl2.bals_date, tbl2.bal_id
from (select distinct a.itm_id,
(select item_code from inv.inv_itm_list where item_id = a.itm_id AND org_id = "
              + Global.mnFrm.cmCde.Org_id + @") CODE, 
(select item_desc from inv.inv_itm_list where item_id = a.itm_id AND org_id = "
              + Global.mnFrm.cmCde.Org_id + @") DESCRIPTION, 
a.subinv_id, 
(select subinv_name from inv.inv_itm_subinventories where subinv_id = a.subinv_id AND org_id = "
              + Global.mnFrm.cmCde.Org_id + @") STORE, 
a.expiry_date, 
a.consgmt_id, 
max(to_date(bals_date,'YYYY-MM-DD')) baldte, 
a.cost_price ,
b.consgmt_tot_qty col2,
COALESCE(b.reservations,0) col3,
b.available_balance col4
from inv.inv_consgmt_rcpt_det a 
inner join inv.inv_consgmt_daily_bals b on a.consgmt_id = b.consgmt_id
inner join inv.inv_itm_list c on a.itm_id = c.item_id 
WHERE c.org_id = "
              + Global.mnFrm.cmCde.Org_id + @"
GROUP BY 1,2,3,4,5,6,7,9,10,11,12
HAVING max(to_date(bals_date,'YYYY-MM-DD'))=(select max(to_date(w.bals_date,'YYYY-MM-DD')) 
from inv.inv_consgmt_daily_bals w where w.consgmt_id = a.consgmt_id)  
ORDER BY 2,7) tbl1, inv.inv_stock_daily_bals tbl2, inv.inv_stock tbl3
WHERE tbl2.stock_id=tbl3.stock_id and tbl3.itm_id=tbl1.itm_id and tbl3.subinv_id = tbl1.subinv_id
GROUP BY 1,2,6,7,8,9,10,11,12
HAVING (SUM(tbl1.col2)!=tbl2.stock_tot_qty or SUM(tbl1.col3) != tbl2.reservations or SUM(tbl1.col4) != tbl2.available_balance)
/*AND to_date(tbl2.bals_date,'YYYY-MM-DD')=tbl1.baldte*/
AND max(to_date(tbl2.bals_date,'YYYY-MM-DD'))=(select max(to_date(c.bals_date,'YYYY-MM-DD')) 
from inv.inv_stock_daily_bals c where c.stock_id = tbl2.stock_id)
ORDER BY 9,11) tbl5);
";

            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }
        public static void updateOrgnlSellingPrice()
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE inv.inv_itm_list SET " +
                  "orgnl_selling_price = selling_price  WHERE (orgnl_selling_price = 0 and selling_price IS NOT NULL)";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updateUOMPrices()
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = @"UPDATE inv.itm_uoms SET 
            selling_price = scm.get_item_unit_sllng_price(item_id, 1)*cnvsn_factor, 
            price_less_tax=scm.get_item_unit_price_ls_tx(item_id, 1)*cnvsn_factor
      WHERE (selling_price = 0 and price_less_tax =0)";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static double getUOMPriceLsTx(long itmID, double qty)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string strSql = "SELECT scm.get_item_unit_price_ls_tx(" + itmID + ", " + qty + ")";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return Math.Round(double.Parse(dtst.Tables[0].Rows[0][0].ToString()), 4);
            }
            return 0;
        }

        public static double getUOMSllngPrice(long itmID, double qty)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string strSql = "SELECT scm.get_item_unit_sllng_price(" + itmID + ", " + qty + ")";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return Math.Round(double.Parse(dtst.Tables[0].Rows[0][0].ToString()), 4);
            }
            return 0;
        }

        public static bool accountForStockClearing(double parTtlCost, int parInvAcctID, int parExpAcctID, string parDocType, long parDocID,
            long parLineID, int parCurncyID)
        {
            try
            {
                consgmtRcpt cnsgmtRcp = new consgmtRcpt();

                string dateStr = DateTime.ParseExact(
                    Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");

                bool succs = true;

                succs = cnsgmtRcp.sendToGLInterfaceMnl(parInvAcctID, "D", parTtlCost, /*nwfrmt*/ dateStr,
                     "Clear Stock Balance", parCurncyID, dateStr,
                     parDocType, parDocID, parLineID);
                if (!succs)
                {
                    return succs;
                }

                //if (cnsgmtRcp.isPayTrnsValid(parInvAcctID, "D", parTtlCost, dateStr))
                //{
                //}
                //else
                //{
                //  return false;
                //}

                succs = cnsgmtRcp.sendToGLInterfaceMnl(parExpAcctID, "I", parTtlCost, /*nwfrmt*/ dateStr,
                  "Clear Stock Balance", parCurncyID, dateStr,
                  parDocType, parDocID, parLineID);
                if (!succs)
                {
                    return succs;
                }
                //if (cnsgmtRcp.isPayTrnsValid(parExpAcctID, "I", parTtlCost, dateStr))
                // {
                // }
                // else
                // {
                //   return false;
                // }
                return succs;

            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return false;
            }
        }

        //    public static bool accountForStockAdjustment(string parPaymtStatus, string parAdjstUpDownStatus, double parTtlCost, int parInvAcctID,
        //      int parInvAccrlID, int parCashAccID, string parDocType, long parDocID, long parLineID, int parCurncyID, string transDte)
        //    {
        //      try
        //      {
        //        consgmtRcpt cnsgmtRcp = new consgmtRcpt();

        //        string incrsDecrs = "I";
        //        string trnsDesc = "Upward Adjustment of Consignment";

        //        if (parAdjstUpDownStatus == "Down")
        //        {
        //          incrsDecrs = "D";
        //          trnsDesc = "Downward Adjustment of Consignment";
        //        }

        //        //dateStr = Global.mnFrm.cmCde.getDB_Date_time();
        //        string dateStr = DateTime.ParseExact(
        //            Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
        //            System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");

        //        bool succs = true;
        //        //string transDte = this.hdrTrnxDatetextBox.Text;

        //        transDte = DateTime.ParseExact(
        //            transDte + " 12:00:00", "yyyy-MM-dd HH:mm:ss",
        //            System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");

        //        //transDte = transDte + " 12:00:00";
        //        if (parPaymtStatus == "Unpaid")
        //        {
        //          succs = cnsgmtRcp.sendToGLInterfaceMnl(parInvAcctID, incrsDecrs, parTtlCost, /*nwfrmt*/ transDte,
        //             trnsDesc, parCurncyID, dateStr, parDocType, parDocID, parLineID);
        //          if (!succs)
        //          {
        //            return succs;
        //          }
        //          //if (cnsgmtRcp.isPayTrnsValid(parInvAcctID, incrsDecrs, parTtlCost, transDte))
        //          //{
        //          //}
        //          //else
        //          //{
        //          //  return false;
        //          //}
        //          succs = cnsgmtRcp.sendToGLInterfaceMnl(parInvAccrlID, incrsDecrs, parTtlCost, /*nwfrmt*/ transDte,
        //             trnsDesc, parCurncyID, dateStr, parDocType, parDocID, parLineID);
        //          if (!succs)
        //          {
        //            return succs;
        //          }
        //          //string nwInDc = "Increase";
        ////          string nwInDc1 = "Decrease";
        ////          if (incrsDecrs == "D")
        ////          {
        ////            //nwInDc = "Decrease";
        ////            nwInDc1 = "Increase";
        ////          }

        ////          Global.createScmPyblsDocDet(parDocID, "1Initial Amount",
        ////"Initial Cost of Goods Adjusted (ADJST No.:" + parDocID + ")",
        ////parTtlCost, parCurncyID, -1, parDocType, false, nwInDc1, parInvAccrlID,
        ////nwInDc1, dfltRcvblAccnt, -1, "VALID", -1, parCurncyID, parCurncyID,
        ////1, 1, Math.Round(1 * parTtlCost, 2),
        ////Math.Round(1 * parTtlCost, 2));
        ////          return true;
        //          //if (cnsgmtRcp.isPayTrnsValid(parAcctPayblID, incrsDecrs, parTtlCost, transDte))
        //          //{
        //          //}
        //          //else
        //          //{
        //          //  return false;
        //          //}
        //        }
        //        else
        //        {
        //          //succs = cnsgmtRcp.sendToGLInterfaceMnl(parAcctPayblID, incrsDecrs, parTtlCost, /*nwfrmt*/ transDte,
        //          //   trnsDesc, parCurncyID, dateStr, parDocType, parDocID, parLineID);
        //          //if (!succs)
        //          //{
        //          //  return succs;
        //          //}
        //          ////if (cnsgmtRcp.isPayTrnsValid(parAcctPayblID, incrsDecrs, parTtlCost, transDte))
        //          ////{
        //          ////}
        //          ////else
        //          ////{
        //          ////  return false;
        //          ////}
        //          //succs = cnsgmtRcp.sendToGLInterfaceMnl(parCashAccID, incrsDecrs, parTtlCost, /*nwfrmt*/ transDte,
        //          //   trnsDesc, parCurncyID, dateStr, parDocType, parDocID, parLineID);
        //          //if (!succs)
        //          //{
        //          //  return succs;
        //          //}
        //          ////if (cnsgmtRcp.isPayTrnsValid(parCashAccID, incrsDecrs, parTtlCost, transDte))
        //          ////{
        //          ////}
        //          ////else
        //          ////{
        //          ////  return false;
        //          ////}
        //        }
        //        return succs;

        //      }
        //      catch (Exception ex)
        //      {
        //        Global.mnFrm.cmCde.showMsg(ex.Message, 0);
        //        return false;
        //      }
        //    }

        public static int getStoreID(string parStore)
        {
            string qryGetStoreID = "SELECT subinv_id from inv.inv_itm_subinventories where subinv_name = '" + parStore.Replace("'", "''")
            + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

            DataSet ds = new DataSet();
            ds.Reset();
            ds = Global.fillDataSetFxn(qryGetStoreID);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return int.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        public static List<string[]> getItmCnsgmtVals(long itmID)
        {
            List<string[]> res = new List<string[]>();

            string strSql = "SELECT distinct c.consgmt_id, inv.get_csgmt_lst_tot_bls(c.consgmt_id), c.cost_price " +
              "FROM inv.inv_consgmt_rcpt_det c " +
              "WHERE ((c.itm_id=" + itmID + ") and (c.subinv_id =" + Global.selectedStoreID + ")) ORDER BY c.consgmt_id ASC";
            // and  (inv.get_csgmt_lst_tot_bls(c.consgmt_id)>0)
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);

            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                string[] rec = new string[3];

                rec[0] = dtst.Tables[0].Rows[i][0].ToString();
                rec[1] = dtst.Tables[0].Rows[i][1].ToString();
                rec[2] = dtst.Tables[0].Rows[i][2].ToString();
                res.Add(rec);
            }
            return res;
        }

        public static List<string[]> getItmCnsgmtVals(double qnty, string cnsgmtIDs)
        {

            cnsgmtIDs = cnsgmtIDs.Replace(",,", ",").Replace(",,", ",").Replace(",,", ",").Trim(',');
            if (cnsgmtIDs == "")
            {
                cnsgmtIDs = "-1234456789";
            }
            List<string[]> res = new List<string[]>();
            string strSql = "SELECT distinct c.consgmt_id, inv.get_csgmt_lst_avlbl_bls(c.consgmt_id), c.cost_price " +
              "FROM inv.inv_consgmt_rcpt_det c " +
              "WHERE ((c.consgmt_id IN (" + cnsgmtIDs.Trim(',') + ")) and (inv.get_csgmt_lst_avlbl_bls(c.consgmt_id)>0)) ORDER BY c.consgmt_id ASC";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            double remQty = qnty;
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                double crQty = double.Parse(dtst.Tables[0].Rows[i][1].ToString());
                string[] rec = new string[3];
                if (crQty <= remQty)
                {
                    rec[0] = dtst.Tables[0].Rows[i][0].ToString();
                    rec[1] = dtst.Tables[0].Rows[i][1].ToString();
                    rec[2] = dtst.Tables[0].Rows[i][2].ToString();
                    remQty -= crQty;
                    res.Add(rec);
                }
                else if (remQty > 0)
                {
                    rec[0] = dtst.Tables[0].Rows[i][0].ToString();
                    rec[1] = remQty.ToString();
                    rec[2] = dtst.Tables[0].Rows[i][2].ToString();
                    //remQty -= crQty;
                    res.Add(rec);
                    return res;
                }
                else
                {
                    return res;
                }
            }
            return res;
        }

        public static List<string[]> getSRItmCnsgmtVals(long lnID, double qnty, string cnsgmtIDs, long srcDocLnID)
        {
            List<string[]> res = new List<string[]>();
            List<string[]> oldres = Global.getCsgmtsDist(srcDocLnID, cnsgmtIDs);
            double remQty = qnty;
            for (int i = oldres.Count - 1; i >= 0; i--)
            {
                string[] ary = oldres[i];
                long figID = 0;
                long.TryParse(ary[0], out figID);
                double fig1Qty = 0;
                double fig2Prc = 0;
                double.TryParse(ary[1], out fig1Qty);
                double.TryParse(ary[2], out fig2Prc);
                double crQty = fig1Qty;
                string[] rec = new string[3];
                //Global.mnFrm.cmCde.showMsg(ary[0] + ary[1] + ary[2], 0);
                if (crQty <= remQty)
                {
                    rec[0] = figID.ToString();
                    rec[1] = fig1Qty.ToString();
                    rec[2] = fig2Prc.ToString();
                    remQty -= crQty;
                    res.Add(rec);
                }
                else if (remQty > 0)
                {
                    rec[0] = figID.ToString();
                    rec[1] = remQty.ToString();
                    rec[2] = fig2Prc.ToString();
                    //remQty -= crQty;
                    res.Add(rec);
                    return res;
                }
                else
                {
                    return res;
                }
            }
            return res;
        }

        public static List<string[]> getCsgmtsDist(long lnID, string cnsgmtIDs)
        {
            List<string[]> res = new List<string[]>();
            string strSql = "SELECT distinct c.cnsgmnt_qty_dist " +
         "FROM scm.scm_sales_invc_det c " +
         "WHERE ((c.invc_det_ln_id =" + lnID + ") and (consgmnt_ids='" + cnsgmtIDs + "'))";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                char[] mw = { ',' };
                string[] ary = dtst.Tables[0].Rows[0][0].ToString().Split(mw, StringSplitOptions.RemoveEmptyEntries);
                string[] ary1 = cnsgmtIDs.Split(mw, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < ary1.Length; i++)
                {
                    string[] rec = new string[3];
                    try
                    {
                        rec[0] = ary1[i];
                        rec[1] = ary[i];
                        rec[2] = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_consgmt_rcpt_det", "consgmt_id", "cost_price", long.Parse(ary1[i]));
                        res.Add(rec);
                    }
                    catch (Exception ex)
                    {
                        rec[0] = ary1[i];
                        rec[1] = "0";
                        rec[2] = "0";
                        res.Add(rec);
                    }
                }
            }
            return res;
        }

        public static double getItmTrnsfTtlCost(double qnty, string cnsgmtIDs)
        {
            cnsgmtIDs = cnsgmtIDs.Replace(",,", ",").Replace(",,", ",").Replace(",,", ",").Trim(',');
            if (cnsgmtIDs == "")
            {
                cnsgmtIDs = "-1234456789";
            }
            string strSql = "SELECT distinct c.consgmt_id, inv.get_csgmt_lst_avlbl_bls(c.consgmt_id), c.cost_price " +
              "FROM inv.inv_consgmt_rcpt_det c " +
              "WHERE ((c.consgmt_id IN (" + cnsgmtIDs.Trim(',') + ")) and (inv.get_csgmt_lst_avlbl_bls(c.consgmt_id)>0)) ORDER BY c.consgmt_id ASC";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            double ttlCst = 0;
            double remQty = qnty;
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                double crQty = double.Parse(dtst.Tables[0].Rows[i][1].ToString());
                //string[] rec = new string[3];
                if (crQty <= remQty)
                {
                    ttlCst += double.Parse(dtst.Tables[0].Rows[i][1].ToString()) * double.Parse(dtst.Tables[0].Rows[i][2].ToString());
                    remQty -= crQty;
                }
                else if (remQty > 0)
                {
                    ttlCst += remQty * double.Parse(dtst.Tables[0].Rows[i][2].ToString());
                    return ttlCst;
                }
                else
                {
                    return ttlCst;
                }
            }
            return ttlCst;
        }
        #endregion

        #region "SALES DOCUMENTS..."
        public static void deleteZeroSmmryItms(long docID, string docType)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string delSQL = "DELETE FROM scm.scm_doc_amnt_smmrys WHERE src_doc_hdr_id = " +
              docID + " and src_doc_type = '" + docType +
              "' and round(smmry_amnt,2) = 0 and (code_id_behind>0 or substr(smmry_type,1,1) IN ('2','3','4'))";
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
        }

        public static void updateResetSmmryItm(long docID, string docType)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string updtSQL = "UPDATE scm.scm_doc_amnt_smmrys SET " +
                  "smmry_amnt = 0 WHERE (src_doc_type = '" + docType.Replace("'", "''") +
                  "' and src_doc_hdr_id = " + docID + " and (code_id_behind>0 or substr(smmry_type,1,1) IN ('2','3','4')))";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updateSmmryItmAddOn(long smmryID, string smmryTyp,
          double amnt, bool autoCalc, string smmryNm)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            if (smmryTyp == "3Discount")
            {
                amnt = -1 * Math.Abs(amnt);
            }
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE scm.scm_doc_amnt_smmrys SET " +
                  "smmry_amnt = COALESCE(smmry_amnt,0) + " + amnt +
                  ", last_update_by = " + Global.myInv.user_id + ", " +
                  "auto_calc = '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(autoCalc) +
                  "', last_update_date = '" + dateStr +
                  "', smmry_name='" + smmryNm.Replace("'", "''") + "' WHERE (smmry_id = " + smmryID + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void createAttachment(long batchid, string attchDesc,
        string filNm, string tblNm, string pkNm)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = "INSERT INTO " + tblNm + "(" +
                  pkNm + ", attchmnt_desc, file_name, created_by, " +
                  "creation_date, last_update_by, last_update_date) " +
                              "VALUES (" + batchid +
                              ", '" + attchDesc.Replace("'", "''") +
                              "', '" + filNm.Replace("'", "''") +
                              "', " + Global.myInv.user_id + ", '" + dateStr +
                              "', " + Global.myInv.user_id + ", '" + dateStr + "')";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void updateAttachment(long attchID, long batchid, string attchDesc,
        string filNm, string tblNm, string pkNm)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE " + tblNm + " SET " +
                  pkNm + "=" + batchid +
                              ", attchmnt_desc='" + attchDesc.Replace("'", "''") +
                              "', file_name='" + filNm.Replace("'", "''") +
                              "', last_update_by=" + Global.myInv.user_id +
                              ", last_update_date='" + dateStr + "' " +
                               "WHERE attchmnt_id = " + attchID;
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static long getAttchmntID(string attchname, long batchID, string tblNm, string pkName)
        {
            string strSql = "";
            strSql = "SELECT a.attchmnt_id " +
         "FROM " + tblNm + " a " +
            "WHERE ((a.attchmnt_desc = '" + attchname.Replace("'", "''") +
              "') AND (a." + pkName + " = " + batchID + "))";

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

        public static long getAttchmntID(string attchname, long hdrID)
        {
            string strSql = "";
            strSql = "SELECT a.attchmnt_id " +
         "FROM scm.scm_sales_doc_attchmnts a " +
            "WHERE ((a.attchmnt_desc = '" + attchname.Replace("'", "''") +
              "') AND (a.doc_hdr_id = " + hdrID + "))";

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

        public static void createAttachment(long hdrID, string attchDesc,
         string filNm)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = "INSERT INTO scm.scm_sales_doc_attchmnts(" +
                  "doc_hdr_id, attchmnt_desc, file_name, created_by, " +
                  "creation_date, last_update_by, last_update_date) " +
                              "VALUES (" + hdrID +
                              ", '" + attchDesc.Replace("'", "''") +
                              "', '" + filNm.Replace("'", "''") +
                              "', " + Global.myInv.user_id + ", '" + dateStr +
                              "', " + Global.myInv.user_id + ", '" + dateStr + "')";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void deleteAttchmnt(long attchid, string attchNm, string tblNm)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Attachment Name = " + attchNm;
            string delSql = "DELETE FROM " + tblNm + " WHERE(attchmnt_id = " + attchid + ")";
            Global.mnFrm.cmCde.deleteDataNoParams(delSql);
        }

        public static void updateAttachment(long attchID, long hdrID, string attchDesc,
       string filNm)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE scm.scm_sales_doc_attchmnts SET " +
                  "doc_hdr_id=" + hdrID +
                              ", attchmnt_desc='" + attchDesc.Replace("'", "''") +
                              "', file_name='" + filNm.Replace("'", "''") +
                              "', last_update_by=" + Global.myInv.user_id +
                              ", last_update_date='" + dateStr + "' " +
                               "WHERE attchmnt_id = " + attchID;
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void deleteAttchmnt(long attchid, string attchNm)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Attachment Name = " + attchNm;
            string delSql = "DELETE FROM scm.scm_sales_doc_attchmnts WHERE(attchmnt_id = " + attchid + ")";
            Global.mnFrm.cmCde.deleteDataNoParams(delSql);
        }

        public static DataSet get_Attachments(string searchWord, string searchIn,
       Int64 offset, int limit_size, long hdrID, ref string attchSQL)
        {
            string strSql = "";
            if (searchIn == "Attachment Name/Description")
            {
                strSql = "SELECT a.attchmnt_id, a.doc_hdr_id, a.attchmnt_desc, a.file_name " +
              "FROM scm.scm_sales_doc_attchmnts a " +
              "WHERE(a.attchmnt_desc ilike '" + searchWord.Replace("'", "''") +
              "' and a.doc_hdr_id = " + hdrID + ") ORDER BY a.attchmnt_id LIMIT " + limit_size +
                  " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            attchSQL = strSql;
            return dtst;
        }

        public static long get_Total_Attachments(string searchWord,
          string searchIn, long hdrID)
        {
            string strSql = "";
            if (searchIn == "Attachment Name/Description")
            {
                strSql = "SELECT COUNT(1) " +
              "FROM scm.scm_sales_doc_attchmnts a " +
              "WHERE(a.attchmnt_desc ilike '" + searchWord.Replace("'", "''") +
              "' and a.doc_hdr_id = " + hdrID + ")";
            }
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            long sumRes = 0;
            if (dtst.Tables[0].Rows.Count > 0)
            {
                long.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out sumRes);
            }
            return sumRes;
        }

        public static long getSalesDocLnID(int itmID,
          int storeID, long srcDocID)
        {
            string strSql = "select y.invc_det_ln_id " +
              "from scm.scm_sales_invc_det y " +
              "where y.itm_id= " + itmID +
              " and y.store_id=" + storeID +
              " and y.invc_hdr_id=" + srcDocID + " ";
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

        public static long getSalesSmmryItmID(string smmryType, long codeBhnd,
         long srcDocID, string srcDocTyp)
        {
            string strSql = "select y.smmry_id " +
              "from scm.scm_doc_amnt_smmrys y " +
              "where y.smmry_type= '" + smmryType + "' and y.code_id_behind= " + codeBhnd +
              " and y.src_doc_type='" + srcDocTyp +
              "' and y.src_doc_hdr_id=" + srcDocID + " ";
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

        //public static double getSalesDocFnlGrndAmnt(long dochdrID, string docTyp)
        //{
        //  string strSql = "select SUM(y.smmry_amnt) amnt " +
        //    "from scm.scm_doc_amnt_smmrys y " +
        //    "where y.src_doc_hdr_id=" + dochdrID +
        //    " and y.src_doc_type='" + docTyp + "' and y.smmry_type != '1Initial Amount' " +
        //    " and y.smmry_type != '6Total Payments Received' and y.smmry_type != " +
        //    "'7Change/Balance' and smmry_type!='4Extra Charge' and smmry_type!='2Tax'";
        //  DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
        //  double rs = 0;

        //  if (dtst.Tables[0].Rows.Count > 0)
        //  {
        //    double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rs);
        //  }
        //  return rs;
        //}

        //public static double getSalesDocBscAmnt(long dochdrID, string docTyp)
        //{
        // string strSql = "select SUM(CASE WHEN (smmry_type='2Tax') THEN -1*y.smmry_amnt ELSE y.smmry_amnt END) amnt " +
        //   "from scm.scm_doc_amnt_smmrys y " +
        //   "where y.src_doc_hdr_id=" + dochdrID +
        //   " and y.src_doc_type='" + docTyp + "' and substr(y.smmry_type,1,1) IN ('2','5')";
        // DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
        // double rs = 0;

        // if (dtst.Tables[0].Rows.Count > 0)
        // {
        //  double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rs);
        // }
        // return rs;
        //}

        public static double getSalesDocCodesAmnt(int codeID, double unitAmnt, double qnty)
        {
            string codeSQL = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes",
              "code_id", "sql_formular", codeID);
            codeSQL = codeSQL.Replace("{:qty}", qnty.ToString()).Replace("{:unit_price}", unitAmnt.ToString());
            if (codeSQL != "")
            {
                DataSet d1 = Global.mnFrm.cmCde.selectDataNoParams(codeSQL);
                double rs1 = 0;

                if (d1.Tables[0].Rows.Count > 0)
                {
                    double.TryParse(d1.Tables[0].Rows[0][0].ToString(), out rs1);
                }
                return rs1 * qnty;
            }
            else
            {
                return 0.00;
            }
        }

        public static double getSalesDocGrndAmnt(long dochdrID)
        {
            string strSql = "select SUM(y.doc_qty*orgnl_selling_price) amnt " +
             "from scm.scm_sales_invc_det y " +
             "where y.invc_hdr_id=" + dochdrID + " ";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            double rs = 0;

            if (dtst.Tables[0].Rows.Count > 0)
            {
                double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rs);
            }
            return rs;
        }

        public static double getSalesDocRcvdPymnts(long dochdrID, string docType)
        {
            string strSql = "select SUM(y.amount_paid) amnt " +
              "from scm.scm_payments y " +
              "where y.src_doc_id=" + dochdrID + " and y.src_doc_typ = '" + docType.Replace("'", "''") + "'";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            double rs = 0;

            if (dtst.Tables[0].Rows.Count > 0)
            {
                double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rs);
            }
            return rs;
        }

        public static double get_One_AvlblSrcLnQty(long srcLnID)
        {
            string strSql = "SELECT (a.doc_qty - a.qty_trnsctd_in_dest_doc) avlbl_qty " +
             "FROM scm.scm_sales_invc_det a " +
             "WHERE(a.invc_det_ln_id = " + srcLnID +
             ") ORDER BY a.invc_det_ln_id";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            double rs = 0;
            if (dtst.Tables[0].Rows.Count > 0)
            {
                double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rs);
            }
            return rs;
        }

        public static double get_One_LnTrnsctdQty(long dochdrID, long srcLnID)
        {
            string strSql = "SELECT SUM(a.doc_qty) trnsctd_qty " +
             "FROM scm.scm_sales_invc_det a " +
             "WHERE(a.invc_hdr_id IN(select b.invc_hdr_id " +
             "from scm.scm_sales_invc_hdr b where b.src_doc_hdr_id = " + dochdrID +
             " and b.src_doc_hdr_id>0) and a.src_line_id = "
             + srcLnID + ")";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            double rs = 0;
            if (dtst.Tables[0].Rows.Count > 0)
            {
                double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rs);
            }
            return rs;
        }

        public static string getDocSgntryCols(string doctype)
        {
            string selSQL = @"select a.pssbl_value_desc from gst.gen_stp_lov_values a, gst.gen_stp_lov_names b
WHERE a.value_list_id = b.value_list_id and a.pssbl_value = '" + doctype.Replace("'", "''") + @"' 
and b.value_list_name = 'Document Signatory Columns'
and a.is_enabled='1' ORDER BY a.pssbl_value_id LIMIT 1 OFFSET 0";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
            if (dtst.Tables.Count <= 0)
            {
                return "";
            }
            else if (dtst.Tables[0].Rows.Count > 0)
            {
                return dtst.Tables[0].Rows[0][0].ToString();
            }
            return "";
        }

        public static DataSet get_One_SalesDcLines(long dochdrID)
        {
            string strSql = "SELECT a.invc_det_ln_id, a.itm_id, " +
              "a.doc_qty, a.unit_selling_price, (a.doc_qty * a.unit_selling_price) amnt, " +
              "a.store_id, a.crncy_id, (a.doc_qty - a.qty_trnsctd_in_dest_doc) avlbl_qty, " +
              "a.src_line_id, a.tax_code_id, a.dscnt_code_id, a.chrg_code_id, a.rtrn_reason, " +
              @"a.consgmnt_ids, a.orgnl_selling_price, b.base_uom_id, b.item_code, b.item_desc, 
      c.uom_name, a.is_itm_delivered, REPLACE(a.extra_desc || ' (' || a.other_mdls_doc_type || ')',' ()','')
        , a.other_mdls_doc_id, a.other_mdls_doc_type, a.lnkd_person_id, 
      REPLACE(prs.get_prsn_surname(a.lnkd_person_id) || ' (' 
      || prs.get_prsn_loc_id(a.lnkd_person_id) || ')', ' ()', '') fullnm, 
      CASE WHEN a.alternate_item_name='' THEN b.item_desc ELSE a.alternate_item_name END, d.cat_name,
        REPLACE(a.cogs_acct_id || ',' || a.sales_rev_accnt_id || ',' || a.sales_ret_accnt_id || ',' || a.purch_ret_accnt_id || ',' || a.expense_accnt_id,
'-1,-1,-1,-1,-1', b.cogs_acct_id || ',' || b.sales_rev_accnt_id || ',' || b.sales_ret_accnt_id || ',' || b.purch_ret_accnt_id || ',' || b.expense_accnt_id) itm_accnts,
      b.item_type " +
             "FROM scm.scm_sales_invc_det a, inv.inv_itm_list b, inv.unit_of_measure c, inv.inv_product_categories d " +
             "WHERE(a.invc_hdr_id = " + dochdrID +
             " and a.invc_hdr_id>0 and a.itm_id = b.item_id and b.base_uom_id = c.uom_id and d.cat_id = b.category_id) ORDER BY a.invc_det_ln_id, b.category_id";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            Global.invcFrm.recDt_SQL = strSql;
            return dtst;
        }

        public static string get_One_ItmAccnts(long itmID)
        {
            string strSql = "SELECT (b.cogs_acct_id || ',' || b.sales_rev_accnt_id || ',' || b.sales_ret_accnt_id || ',' || b.purch_ret_accnt_id || ',' || b.expense_accnt_id) itm_accnts " +
             "FROM inv.inv_itm_list b " +
             "WHERE(b.item_id = " + itmID + ")";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return dtst.Tables[0].Rows[0][0].ToString();
            }
            return "-1,-1,-1,-1,-1";
        }

        public static DataSet get_One_SalesDcLinesReq(long dochdrID)
        {
            string strSql = "SELECT a.invc_det_ln_id, a.itm_id, " +
              "a.doc_qty, a.unit_selling_price, (a.doc_qty * a.unit_selling_price) amnt, " +
              "a.store_id, a.crncy_id, (a.doc_qty - a.qty_trnsctd_in_dest_doc) avlbl_qty, " +
              "a.src_line_id, a.tax_code_id, a.dscnt_code_id, a.chrg_code_id, a.rtrn_reason, " +
              @"a.consgmnt_ids, a.orgnl_selling_price, b.base_uom_id, b.item_code, 
        b.item_desc, 
      c.uom_name, a.is_itm_delivered, REPLACE(a.extra_desc || ' (' || a.other_mdls_doc_type || ')',' ()','')
        , a.other_mdls_doc_id, a.other_mdls_doc_type, a.lnkd_person_id, 
      REPLACE(prs.get_prsn_surname(a.lnkd_person_id) || ' (' 
      || prs.get_prsn_loc_id(a.lnkd_person_id) || ')', ' ()', '') fullnm, 
      CASE WHEN a.alternate_item_name='' THEN b.item_desc ELSE a.alternate_item_name END, d.cat_name " +
             "FROM scm.scm_sales_invc_det a, inv.inv_itm_list b, inv.unit_of_measure c, inv.inv_product_categories d " +
             "WHERE(a.invc_hdr_id = " + dochdrID +
             " and a.invc_hdr_id>0 and a.itm_id = b.item_id and b.base_uom_id = c.uom_id and d.cat_id = b.category_id) ORDER BY b.category_id, a.invc_det_ln_id";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            Global.invcFrm.recDt_SQL = strSql;
            return dtst;
        }

        public static void updtOrgInvoiceCurrID(int orgID, int crncyID, long pymtMthdID)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE scm.scm_sales_invc_hdr SET invc_curr_id = " + crncyID +
                              ", last_update_by = " + Global.myInv.user_id + ", " +
                              "last_update_date = '" + dateStr + "' " +
              "WHERE (org_id = " + orgID + " and invc_curr_id<=0)";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
            updtSQL = "UPDATE scm.scm_sales_invc_hdr SET pymny_method_id = " + pymtMthdID +
                              ", last_update_by = " + Global.myInv.user_id + ", " +
                              "last_update_date = '" + dateStr + "' " +
              "WHERE (org_id = " + orgID + " and pymny_method_id<=0)";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);

        }

        public static void updtOrgPOCurrID(int orgID, int crncyID)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            Global.mnFrm.cmCde.ignorAdtTrail = true;
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE scm.scm_prchs_docs_hdr SET prntd_doc_curr_id = " + crncyID +
                              ", exchng_rate = 1 " +
              "WHERE (org_id = " + orgID + " and prntd_doc_curr_id<=0)";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
            Global.mnFrm.cmCde.ignorAdtTrail = false;
        }

        //    public static DataSet get_SalesMoneyRcvd(long UsrID, string doctype, string strtDte, string endDte, int orgID, string ordrBy)
        //    {
        //      /*
        //   y.user_name ""Sales Agent"",*/
        //      string usrCls = "";
        //      if (UsrID > 0)
        //      {
        //        usrCls = " and (y.user_id = " + UsrID + ")";
        //      }
        //      if (ordrBy == "OUTSTANDING AMOUNT")
        //      {
        //        ordrBy = @"tbl1.col5 DESC, tbl1.col7, tbl1.col1 ASC";
        //      }
        //      else if (ordrBy == "TOTAL AMOUNT")
        //      {
        //        ordrBy = @"tbl1.col2 DESC, tbl1.col7, tbl1.col1 ASC";
        //      }
        //      else
        //      {
        //        ordrBy = "tbl1.col7, tbl1.col1 ASC";
        //      }

        //      string strSql = @"SELECT row_number() OVER (ORDER BY " + ordrBy + @") AS ""No.  ""
        //, tbl1.col1 ""Document No.                     "", tbl1.col2 ""  Invoice Amount"", tbl1.col3 "" Discount Amount"",
        //tbl1.col4 ""     Amount Paid"", tbl1.col5 ""Outstanding Amt."", tbl1.col6 ""Creation Date           "", tbl1.col7 ""mt""
        //FROM (SELECT REPLACE(a.invc_number || ' (' || COALESCE(scm.get_cstmr_splr_name(a.customer_id),'Unspecified') 
        //|| ')' || ' (' || hotl.get_invc_room_num(a.invc_hdr_id) || ')-' || gst.get_pssbl_val(a.invc_curr_id),' ()','') col1, 
        //scm.get_doc_smry_typ_amnt(a.invc_hdr_id, a.invc_type, '5Grand Total') + 
        //abs(scm.get_doc_smry_typ_amnt(a.invc_hdr_id, a.invc_type, '3Discount')) col2, 
        //scm.get_doc_smry_typ_amnt(a.invc_hdr_id, a.invc_type, '3Discount') col3,
        //scm.get_doc_smry_typ_amnt(a.invc_hdr_id, a.invc_type, '6Total Payments Received') col4, 
        //scm.get_doc_smry_typ_amnt(a.invc_hdr_id, a.invc_type, '7Change/Balance') col5, 
        //to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),
        //'DD-Mon-YYYY HH24:MI:SS') col6,
        //a.creation_date col7 
        //FROM scm.scm_sales_invc_hdr a, 
        //sec.sec_users y WHERE ((a.approval_status ilike 'Approved' or 
        //(Select count(q.invc_det_ln_id) from scm.scm_sales_invc_det q 
        //where q.invc_hdr_id = a.invc_hdr_id and q.is_itm_delivered='1')>0) AND (a.org_id = " + orgID + @") AND 
        //(a.created_by=y.user_id)" + usrCls + " and (a.invc_type ilike '" + doctype.Replace("'", "''") + @"') 
        //and (to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS') between 
        //to_timestamp('" + strtDte + @"','DD-Mon-YYYY HH24:MI:SS') AND 
        //to_timestamp('" + endDte + @"','DD-Mon-YYYY HH24:MI:SS'))) 
        //UNION
        //SELECT a.rcvbls_invc_number  || ' (' || COALESCE(scm.get_cstmr_splr_name(a.customer_id),'Unspecified') || ')-' || gst.get_pssbl_val(a.invc_curr_id) col1, 
        //CASE WHEN a.advc_pay_ifo_doc_id<=0 THEN accb.get_rcvbl_smry_typ_amnt(a.rcvbls_invc_hdr_id, a.rcvbls_invc_type, '6Grand Total') + 
        //abs(accb.get_rcvbl_smry_typ_amnt(a.rcvbls_invc_hdr_id, a.rcvbls_invc_type, '3Discount')) ELSE 0 END col2, 
        //accb.get_rcvbl_smry_typ_amnt(a.rcvbls_invc_hdr_id, a.rcvbls_invc_type, '3Discount') col3,
        //accb.get_rcvbl_smry_typ_amnt(a.rcvbls_invc_hdr_id, a.rcvbls_invc_type, '7Total Payments Made') col4, 
        //accb.get_rcvbl_smry_typ_amnt(a.rcvbls_invc_hdr_id, a.rcvbls_invc_type, '8Outstanding Balance') col5, 
        //to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),
        //'DD-Mon-YYYY HH24:MI:SS') col6, a.creation_date col7 
        //FROM accb.accb_rcvbls_invc_hdr a, 
        //sec.sec_users y WHERE ((a.approval_status ilike 'Approved') AND (a.org_id = " + orgID + @") AND 
        //(a.created_by=y.user_id)" + usrCls + @" and ((a.src_doc_hdr_id||'.'||a.src_doc_type) " +
        //"NOT IN (Select v.invc_hdr_id||'.'||v.invc_type from scm.scm_sales_invc_hdr v where v.org_id = " + orgID +
        //@" and v.invc_type ilike '" + doctype.Replace("'", "''") + @"'))  
        //and a.invc_amnt_appld_elswhr<=0 
        ///*(a.rcvbls_invc_type ilike '%Advance%Payment%')*/ 
        //and (to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS') between 
        //to_timestamp('" + strtDte + @"','DD-Mon-YYYY HH24:MI:SS') AND 
        //to_timestamp('" + endDte + @"','DD-Mon-YYYY HH24:MI:SS')))
        //UNION
        //SELECT a.mass_pay_name col1, 
        //pay.get_intrnlpay_salesamnt(a.mass_pay_id) col2, 
        //0 col3,
        //pay.get_intrnlpay_salesamnt(a.mass_pay_id) col4, 
        //0 col5, 
        //to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'), 'DD-Mon-YYYY HH24:MI:SS') col6, 
        //a.creation_date col7 
        //FROM pay.pay_mass_pay_run_hdr a, 
        //sec.sec_users y WHERE ((a.run_status = '1' and a.sent_to_gl = '1') AND (a.org_id = " + orgID + @") AND 
        //(a.created_by=y.user_id)" + usrCls + @" and pay.get_intrnlpay_salesamnt(a.mass_pay_id)!=0 
        //and (to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS') between 
        //to_timestamp('" + strtDte + @"','DD-Mon-YYYY HH24:MI:SS') AND 
        //to_timestamp('" + endDte + @"','DD-Mon-YYYY HH24:MI:SS')))) tbl1 
        //ORDER BY " + ordrBy + @"";

        //      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
        //      return dtst;
        //    }

        public static DataSet get_SalesMoneyRcvd(long UsrID, string doctype,
       string strtDte, string endDte, int orgID, string ordrBy, bool useCreatnDte)
        {
            /*y.user_name ""Sales Agent"",*/
            string usrCls = "";
            string usrNmSect = " ||' ('||y.user_name||')'";
            if (UsrID > 0)
            {
                usrCls = " and (y.user_id = " + UsrID + ")";
                usrNmSect = "";
            }
            if (ordrBy == "OUTSTANDING AMOUNT")
            {
                ordrBy = @"tbl1.col5 DESC, tbl1.col7, tbl1.col1 ASC";
            }
            else if (ordrBy == "TOTAL AMOUNT")
            {
                ordrBy = @"tbl1.col2 DESC, tbl1.col7, tbl1.col1 ASC";
            }
            else
            {
                ordrBy = "tbl1.col7, tbl1.col1 ASC";
            }

            string strSql = "";
            if (useCreatnDte)
            {
                strSql = @"SELECT row_number() OVER (ORDER BY " + ordrBy + @") AS ""No.  ""
, tbl1.col1 ""Document No.                     "", tbl1.col2 ""  Invoice Amount"", tbl1.col3 "" Discount Amount"",
tbl1.col4 ""     Amount Paid"", tbl1.col5 ""Outstanding Amt."", tbl1.col6 ""Creation Date           "", tbl1.col7 ""mt""
FROM (SELECT REPLACE(a.invc_number || ' (' || COALESCE(scm.get_cstmr_splr_name(a.customer_id),'Unspecified') 
|| ')' || ' (' || hotl.get_invc_room_num(a.invc_hdr_id) || ')-' || gst.get_pssbl_val(a.invc_curr_id),' ()','') col1, 
scm.get_doc_smry_typ_amnt(a.invc_hdr_id, a.invc_type, '5Grand Total') + 
abs(scm.get_doc_smry_typ_amnt(a.invc_hdr_id, a.invc_type, '3Discount')) col2, 
scm.get_doc_smry_typ_amnt(a.invc_hdr_id, a.invc_type, '3Discount') col3,
scm.get_doc_smry_typ_amnt(a.invc_hdr_id, a.invc_type, '6Total Payments Received') col4, 
scm.get_doc_smry_typ_amnt(a.invc_hdr_id, a.invc_type, '7Change/Balance') col5, 
to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),
'DD-Mon-YYYY HH24:MI:SS')" + usrNmSect + @" col6,
a.creation_date col7 
FROM scm.scm_sales_invc_hdr a, 
sec.sec_users y WHERE ((a.approval_status ilike 'Approved' or 
(Select count(q.invc_det_ln_id) from scm.scm_sales_invc_det q 
where q.invc_hdr_id = a.invc_hdr_id and q.is_itm_delivered='1')>0) AND (a.org_id = " + orgID + @") AND 
(a.created_by=y.user_id)" + usrCls + " and (a.invc_type ilike '" + doctype.Replace("'", "''") + @"') 
and (to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS') between 
to_timestamp('" + strtDte + @"','DD-Mon-YYYY HH24:MI:SS') AND 
to_timestamp('" + endDte + @"','DD-Mon-YYYY HH24:MI:SS'))) 
UNION
SELECT a.rcvbls_invc_number  || ' (' || COALESCE(scm.get_cstmr_splr_name(a.customer_id),'Unspecified') || ')-' || gst.get_pssbl_val(a.invc_curr_id) col1, 
CASE WHEN a.advc_pay_ifo_doc_id<=0 THEN accb.get_rcvbl_smry_typ_amnt(a.rcvbls_invc_hdr_id, a.rcvbls_invc_type, '6Grand Total') + 
abs(accb.get_rcvbl_smry_typ_amnt(a.rcvbls_invc_hdr_id, a.rcvbls_invc_type, '3Discount')) ELSE 0 END col2, 
accb.get_rcvbl_smry_typ_amnt(a.rcvbls_invc_hdr_id, a.rcvbls_invc_type, '3Discount') col3,
accb.get_rcvbl_smry_typ_amnt(a.rcvbls_invc_hdr_id, a.rcvbls_invc_type, '7Total Payments Made') col4, 
accb.get_rcvbl_smry_typ_amnt(a.rcvbls_invc_hdr_id, a.rcvbls_invc_type, '8Outstanding Balance') col5, 
to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),
'DD-Mon-YYYY HH24:MI:SS')" + usrNmSect + @" col6, a.creation_date col7 
FROM accb.accb_rcvbls_invc_hdr a, 
sec.sec_users y WHERE ((a.approval_status ilike 'Approved') AND (a.org_id = " + orgID + @") AND 
(a.created_by=y.user_id)" + usrCls + @" and ((a.src_doc_hdr_id||'.'||a.src_doc_type) " +
        "NOT IN (Select v.invc_hdr_id||'.'||v.invc_type from scm.scm_sales_invc_hdr v where v.org_id = " + orgID +
        @" and v.invc_type ilike '" + doctype.Replace("'", "''") + @"')) 
and a.invc_amnt_appld_elswhr <= 0 
/*(a.rcvbls_invc_type ilike '%Advance%Payment%')*/ 
and (to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS') between 
to_timestamp('" + strtDte + @"','DD-Mon-YYYY HH24:MI:SS') AND 
to_timestamp('" + endDte + @"','DD-Mon-YYYY HH24:MI:SS')))
UNION
SELECT a.mass_pay_name col1, 
pay.get_intrnlpay_salesamnt(a.mass_pay_id) col2, 
0 col3,
pay.get_intrnlpay_salesamnt(a.mass_pay_id) col4, 
0 col5, 
to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'), 'DD-Mon-YYYY HH24:MI:SS')" + usrNmSect + @" col6, 
a.creation_date col7 
FROM pay.pay_mass_pay_run_hdr a, 
sec.sec_users y WHERE ((a.run_status = '1' and a.sent_to_gl = '1') AND (a.org_id = " + orgID + @") AND 
(a.created_by=y.user_id)" + usrCls + @" and pay.get_intrnlpay_salesamnt(a.mass_pay_id)!=0 
and (to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS') between 
to_timestamp('" + strtDte + @"','DD-Mon-YYYY HH24:MI:SS') AND 
to_timestamp('" + endDte + @"','DD-Mon-YYYY HH24:MI:SS')))) tbl1 
ORDER BY " + ordrBy + @"";
            }
            else
            {
                strSql = @"SELECT row_number() OVER (ORDER BY " + ordrBy + @") AS ""No.  ""
, tbl1.col1 ""Document No.                     "", tbl1.col2 ""  Invoice Amount"", tbl1.col3 "" Discount Amount"",
tbl1.col4 ""     Amount Paid"", tbl1.col5 ""Outstanding Amt."", tbl1.col6 ""Document Date           "", tbl1.col7 ""mt""
FROM (SELECT REPLACE(a.invc_number || ' (' || COALESCE(scm.get_cstmr_splr_name(a.customer_id),'Unspecified') 
|| ')' || ' (' || hotl.get_invc_room_num(a.invc_hdr_id) || ')-' || gst.get_pssbl_val(a.invc_curr_id),' ()','') col1, 
scm.get_doc_smry_typ_amnt(a.invc_hdr_id, a.invc_type, '5Grand Total') + 
abs(scm.get_doc_smry_typ_amnt(a.invc_hdr_id, a.invc_type, '3Discount')) col2, 
scm.get_doc_smry_typ_amnt(a.invc_hdr_id, a.invc_type, '3Discount') col3,
scm.get_doc_smry_typ_amnt(a.invc_hdr_id, a.invc_type, '6Total Payments Received') col4, 
scm.get_doc_smry_typ_amnt(a.invc_hdr_id, a.invc_type, '7Change/Balance') col5, 
to_char(to_timestamp(a.invc_date || ' 00:00:00','YYYY-MM-DD HH24:MI:SS'),
'DD-Mon-YYYY HH24:MI:SS')" + usrNmSect + @" col6,
a.invc_date || ' 00:00:00' col7 
FROM scm.scm_sales_invc_hdr a, 
sec.sec_users y WHERE ((a.approval_status ilike 'Approved' or 
(Select count(q.invc_det_ln_id) from scm.scm_sales_invc_det q 
where q.invc_hdr_id = a.invc_hdr_id and q.is_itm_delivered='1')>0) AND (a.org_id = " + orgID + @") AND 
(a.created_by=y.user_id)" + usrCls + " and (a.invc_type ilike '" + doctype.Replace("'", "''") + @"') 
and (to_timestamp(a.invc_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') between 
to_timestamp('" + strtDte + @"','DD-Mon-YYYY HH24:MI:SS') AND 
to_timestamp('" + endDte + @"','DD-Mon-YYYY HH24:MI:SS'))) 
UNION
SELECT a.rcvbls_invc_number  || ' (' || COALESCE(scm.get_cstmr_splr_name(a.customer_id),'Unspecified') || ')-' || gst.get_pssbl_val(a.invc_curr_id) col1, 
CASE WHEN a.advc_pay_ifo_doc_id<=0 THEN accb.get_rcvbl_smry_typ_amnt(a.rcvbls_invc_hdr_id, a.rcvbls_invc_type, '6Grand Total') + 
abs(accb.get_rcvbl_smry_typ_amnt(a.rcvbls_invc_hdr_id, a.rcvbls_invc_type, '3Discount')) ELSE 0 END col2, 
accb.get_rcvbl_smry_typ_amnt(a.rcvbls_invc_hdr_id, a.rcvbls_invc_type, '3Discount') col3,
accb.get_rcvbl_smry_typ_amnt(a.rcvbls_invc_hdr_id, a.rcvbls_invc_type, '7Total Payments Made') col4, 
accb.get_rcvbl_smry_typ_amnt(a.rcvbls_invc_hdr_id, a.rcvbls_invc_type, '8Outstanding Balance') col5, 
to_char(to_timestamp(a.rcvbls_invc_date || ' 00:00:00','YYYY-MM-DD HH24:MI:SS'),
'DD-Mon-YYYY HH24:MI:SS')||' ('||y.user_name||')' col6, a.rcvbls_invc_date || ' 00:00:00' col7 
FROM accb.accb_rcvbls_invc_hdr a, 
sec.sec_users y WHERE ((a.approval_status ilike 'Approved') AND (a.org_id = " + orgID + @") AND 
(a.created_by=y.user_id)" + usrCls + @" and ((a.src_doc_hdr_id||'.'||a.src_doc_type) " +
        "NOT IN (Select v.invc_hdr_id||'.'||v.invc_type from scm.scm_sales_invc_hdr v where v.org_id = " + orgID +
        @" and v.invc_type ilike '" + doctype.Replace("'", "''") + @"')) 
and a.invc_amnt_appld_elswhr <= 0 
/*(a.rcvbls_invc_type ilike '%Advance%Payment%')*/ 
and (to_timestamp(a.rcvbls_invc_date || ' 00:00:00','YYYY-MM-DD HH24:MI:SS') between 
to_timestamp('" + strtDte + @"','DD-Mon-YYYY HH24:MI:SS') AND 
to_timestamp('" + endDte + @"','DD-Mon-YYYY HH24:MI:SS')))
UNION
SELECT a.mass_pay_name col1, 
pay.get_intrnlpay_salesamnt(a.mass_pay_id) col2, 
0 col3,
pay.get_intrnlpay_salesamnt(a.mass_pay_id) col4, 
0 col5, 
to_char(to_timestamp(a.mass_pay_trns_date,'YYYY-MM-DD HH24:MI:SS'), 'DD-Mon-YYYY HH24:MI:SS')" + usrNmSect + @" col6, 
a.mass_pay_trns_date col7 
FROM pay.pay_mass_pay_run_hdr a, 
sec.sec_users y WHERE ((a.run_status = '1' and a.sent_to_gl = '1') AND (a.org_id = " + orgID + @") AND 
(a.created_by=y.user_id)" + usrCls + @" and pay.get_intrnlpay_salesamnt(a.mass_pay_id)!=0 
and (to_timestamp(a.mass_pay_trns_date,'YYYY-MM-DD HH24:MI:SS') between 
to_timestamp('" + strtDte + @"','DD-Mon-YYYY HH24:MI:SS') AND 
to_timestamp('" + endDte + @"','DD-Mon-YYYY HH24:MI:SS')))) tbl1 
ORDER BY " + ordrBy + @"";
            }

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static DataSet get_PymtsMoneyRcvd(long UsrID, string doctype,
       string strtDte, string endDte, int orgID, string ordrBy, bool useCreatnDte)
        {
            /*y.user_name ""Sales Agent"",*/
            string usrCls = "";
            string usrNmSect = " ||' ('||y.user_name||')'";
            if (UsrID > 0)
            {
                usrCls = " and (y.user_id = " + UsrID + ")";
                usrNmSect = "";
            }
            if (ordrBy == "OUTSTANDING AMOUNT")
            {
                ordrBy = @"tbl1.col5 DESC, tbl1.col7, tbl1.col1 ASC";
            }
            else if (ordrBy == "TOTAL AMOUNT")
            {
                ordrBy = @"tbl1.col2 DESC, tbl1.col7, tbl1.col1 ASC";
            }
            else
            {
                ordrBy = "tbl1.col7, tbl1.col1 ASC";
            }

            string strSql = "";
            string dateClause = "";
            string dateClauseR = "";
            string dateClauseM = "";
            if (useCreatnDte)
            {
                dateClause = "(CASE WHEN z.creation_date IS NULL THEN a.creation_date ELSE z.creation_date END)";
                dateClauseR = "(CASE WHEN z.creation_date IS NULL THEN a.creation_date ELSE z.creation_date END)";
                dateClauseM = "a.creation_date";
            }
            else
            {
                dateClause = "(CASE WHEN z.pymnt_date IS NULL THEN a.invc_date || ' 00:00:00' ELSE z.pymnt_date END)";
                dateClauseR = "(CASE WHEN z.pymnt_date IS NULL THEN a.rcvbls_invc_date || ' 00:00:00' ELSE z.pymnt_date END)";
                dateClauseM = "a.mass_pay_trns_date";
            }

            strSql = @"SELECT row_number() OVER (ORDER BY " + ordrBy + @") AS ""No.  ""
, tbl1.col1 ""Document No.                     "", tbl1.col2 ""  Invoice Amount"", tbl1.col3 "" Discount Amount"",
tbl1.col4 ""     Amount Paid"", tbl1.col5 ""Outstanding Amt."", tbl1.col6 "" Date                      "", tbl1.col7 ""mt""
FROM (SELECT REPLACE(a.invc_number || ' (' || COALESCE(scm.get_cstmr_splr_name(a.customer_id),'Unspecified') 
|| ')' || ' (' || hotl.get_invc_room_num(a.invc_hdr_id) || ')-' || gst.get_pssbl_val(a.invc_curr_id),' ()','') col1, 
scm.get_doc_smry_typ_amnt(a.invc_hdr_id, a.invc_type, '5Grand Total') + 
abs(scm.get_doc_smry_typ_amnt(a.invc_hdr_id, a.invc_type, '3Discount')) col2, 
scm.get_doc_smry_typ_amnt(a.invc_hdr_id, a.invc_type, '3Discount') col3,
COALESCE(z.amount_paid,0) col4, 
scm.get_doc_smry_typ_amnt(a.invc_hdr_id, a.invc_type, '7Change/Balance') col5, 
to_char(to_timestamp(" + dateClause + @",'YYYY-MM-DD HH24:MI:SS'),
'DD-Mon-YYYY HH24:MI:SS')" + usrNmSect + @" col6, " + dateClause + @" col7 
FROM scm.scm_sales_invc_hdr a 
LEFT OUTER JOIN accb.accb_rcvbls_invc_hdr x ON (x.src_doc_type=a.invc_type and x.src_doc_hdr_id = a.invc_hdr_id)
LEFT OUTER JOIN accb.accb_payments z ON (z.src_doc_typ=x.rcvbls_invc_type and z.src_doc_id=x.rcvbls_invc_hdr_id and z.orgnl_pymnt_id<=0 and z.pymnt_vldty_status='VALID')
LEFT OUTER JOIN sec.sec_users y ON (z.created_by=y.user_id)
WHERE ((a.approval_status ilike 'Approved' or 
(Select count(q.invc_det_ln_id) from scm.scm_sales_invc_det q 
where q.invc_hdr_id = a.invc_hdr_id and q.is_itm_delivered='1') > 0) AND (a.org_id = " + orgID + @") " + usrCls + " and (a.invc_type ilike '" + doctype.Replace("'", "''") + @"') 
and (to_timestamp(" + dateClause + @", 'YYYY-MM-DD HH24:MI:SS') between 
to_timestamp('" + strtDte + @"', 'DD-Mon-YYYY HH24:MI:SS') AND 
to_timestamp('" + endDte + @"', 'DD-Mon-YYYY HH24:MI:SS')) AND COALESCE(z.created_by,-123)=y.user_id 
AND COALESCE(z.prepay_doc_id, -123)<0) 
UNION
SELECT a.rcvbls_invc_number  || ' (' || COALESCE(scm.get_cstmr_splr_name(a.customer_id),'Unspecified') || ')-' || gst.get_pssbl_val(a.invc_curr_id) col1, 
CASE WHEN a.advc_pay_ifo_doc_id<=0 THEN accb.get_rcvbl_smry_typ_amnt(a.rcvbls_invc_hdr_id, a.rcvbls_invc_type, '6Grand Total') + 
abs(accb.get_rcvbl_smry_typ_amnt(a.rcvbls_invc_hdr_id, a.rcvbls_invc_type, '3Discount')) ELSE 0 END col2, 
accb.get_rcvbl_smry_typ_amnt(a.rcvbls_invc_hdr_id, a.rcvbls_invc_type, '3Discount') col3,
COALESCE(z.amount_paid,0) col4, 
accb.get_rcvbl_smry_typ_amnt(a.rcvbls_invc_hdr_id, a.rcvbls_invc_type, '8Outstanding Balance') col5, 
to_char(to_timestamp(" + dateClauseR + @",'YYYY-MM-DD HH24:MI:SS'),
'DD-Mon-YYYY HH24:MI:SS')" + usrNmSect + @" col6, " + dateClauseR + @" col7 
FROM accb.accb_rcvbls_invc_hdr a
LEFT OUTER JOIN accb.accb_payments z ON (z.src_doc_typ=a.rcvbls_invc_type and z.src_doc_id=a.rcvbls_invc_hdr_id and z.orgnl_pymnt_id<=0 and z.pymnt_vldty_status='VALID') 
LEFT OUTER JOIN sec.sec_users y ON (z.created_by=y.user_id) 
WHERE ((a.approval_status ilike 'Approved') AND (a.org_id = " + orgID + @") " + usrCls + @" and ((a.src_doc_hdr_id||'.'||a.src_doc_type) " +
"NOT IN (Select v.invc_hdr_id||'.'||v.invc_type from scm.scm_sales_invc_hdr v where v.org_id = " + orgID +
@" and v.invc_type ilike '" + doctype.Replace("'", "''") + @"')) 
/*and a.invc_amnt_appld_elswhr <= 0*/ 
and (to_timestamp(" + dateClauseR + @",'YYYY-MM-DD HH24:MI:SS') between 
to_timestamp('" + strtDte + @"','DD-Mon-YYYY HH24:MI:SS') AND 
to_timestamp('" + endDte + @"','DD-Mon-YYYY HH24:MI:SS')) AND COALESCE(z.created_by,-123)=y.user_id 
AND COALESCE(z.prepay_doc_id, -123)<0)
UNION
SELECT a.mass_pay_name col1, 
pay.get_intrnlpay_salesamnt(a.mass_pay_id) col2, 
0 col3,
pay.get_intrnlpay_salesamnt(a.mass_pay_id) col4, 
0 col5, 
to_char(to_timestamp(" + dateClauseM + @",'YYYY-MM-DD HH24:MI:SS'), 'DD-Mon-YYYY HH24:MI:SS')" + usrNmSect + @" col6, 
" + dateClauseM + @" col7 
FROM pay.pay_mass_pay_run_hdr a, 
sec.sec_users y WHERE ((a.run_status = '1' and a.sent_to_gl = '1') AND (a.org_id = " + orgID + @") AND 
(a.created_by=y.user_id)" + usrCls + @" and pay.get_intrnlpay_salesamnt(a.mass_pay_id)!=0 
and (to_timestamp(" + dateClauseM + @",'YYYY-MM-DD HH24:MI:SS') between 
to_timestamp('" + strtDte + @"','DD-Mon-YYYY HH24:MI:SS') AND 
to_timestamp('" + endDte + @"','DD-Mon-YYYY HH24:MI:SS')))) tbl1 
ORDER BY " + ordrBy + @"";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static DataSet get_ItemsSold(long UsrID, string doctype, string strtDte, string endDte, int orgID, string ordrBy)
        {
            string usrCls = "";

            if (UsrID > 0)
            {
                usrCls = " and (y.user_id = " + UsrID + ")";
            }
            if (ordrBy == "QTY")
            {
                ordrBy = "SUM(b.doc_qty)  DESC,";
            }
            else if (ordrBy == "TOTAL AMOUNT")
            {
                ordrBy = "SUM(b.doc_qty * b.unit_selling_price)  DESC,";
            }
            else
            {
                ordrBy = "";
            }
            //trim(REPLACE(a.invc_number || ' (' || a.comments_desc || ')','()','')) 
            //, a.invc_number, a.comments_desc a.approval_status ilike 'Approved' or 
            string strSql = @"SELECT row_number() OVER (ORDER BY " + ordrBy + @" c.item_desc ASC) AS ""No.  ""
        , CASE WHEN b.alternate_item_name = '' THEN 
          trim(c.item_code || ' ' || REPLACE(c.item_desc,c.item_code,'')) ELSE b.alternate_item_name END ""Item Code/Desc.  "", 
        array_to_string(array_agg(distinct REPLACE(a.invc_number || ' (' || hotl.get_invc_room_num(a.invc_hdr_id) || ')','()','')),', ') ""Document Numbers           "", 
        SUM(b.doc_qty) ""QTY      "", 
        d.uom_name ""UOM     "", 
        b.unit_selling_price ""Sales Price "", 
        SUM(b.doc_qty * b.unit_selling_price) ""Total Amount  "",
        c.item_desc mt,
        gst.get_pssbl_val(b.crncy_id) ""Curr. ""
        FROM scm.scm_sales_invc_hdr a, sec.sec_users y, scm.scm_sales_invc_det b, inv.inv_itm_list c, inv.unit_of_measure d
        WHERE ((a.invc_hdr_id = b.invc_hdr_id AND b.itm_id = c.item_id AND c.base_uom_id = d.uom_id) 
        AND (b.is_itm_delivered ='1') AND (a.org_id = " + orgID + @") AND 
        (b.created_by=y.user_id)" + usrCls + " and (a.invc_type ilike '" + doctype.Replace("'", "''") + @"') 
        and (to_timestamp(b.creation_date,'YYYY-MM-DD HH24:MI:SS') between 
        to_timestamp('" + strtDte + @"','DD-Mon-YYYY HH24:MI:SS') AND 
        to_timestamp('" + endDte + @"','DD-Mon-YYYY HH24:MI:SS'))) 
        GROUP BY b.alternate_item_name, c.item_desc, b.itm_id, c.item_code, d.uom_name, b.unit_selling_price, b.crncy_id
        ORDER BY " + ordrBy + @" c.item_desc ASC, b.alternate_item_name ASC";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static double getSalesSmmryItmAmnt(string smmryType, long codeBhnd,
      long srcDocID, string srcDocTyp)
        {
            string strSql = "select COALESCE(SUM(y.smmry_amnt),0) " +
              "from scm.scm_doc_amnt_smmrys y " +
              "where y.smmry_type= '" + smmryType + "' and y.src_doc_type='" + srcDocTyp +
              "' and y.src_doc_hdr_id=" + srcDocID + " ";
            /* and y.code_id_behind= " + codeBhnd +
              "*/
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return double.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0.00;
            }
        }

        public static double getSalesDocTtlAmnt(long dochdrID)
        {
            string strSql = "select SUM(y.doc_qty*unit_selling_price) amnt " +
              "from scm.scm_sales_invc_det y " +
              "where y.invc_hdr_id=" + dochdrID + " ";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            double rs = 0;

            if (dtst.Tables[0].Rows.Count > 0)
            {
                double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rs);
            }
            return rs;
        }

        public static DataSet get_One_SalesDcDt(long dochdrID)
        {
            string strSql = "SELECT a.invc_hdr_id, a.invc_number, " +
              @"a.invc_type, a.src_doc_hdr_id, 
      to_char(to_timestamp(a.invc_date,'YYYY-MM-DD'),'DD-Mon-YYYY'), " +
             "a.customer_id, a.customer_site_id, a.comments_desc, a.payment_terms, " +
             "a.approval_status, a.next_aproval_action, " +
             "a.created_by, a.pymny_method_id, accb.get_pymnt_mthd_name(a.pymny_method_id), " +
             "a.invc_curr_id, gst.get_pssbl_val(a.invc_curr_id), a.exchng_rate, " +
             "a.other_mdls_doc_id,scm.get_src_doc_num(a.other_mdls_doc_id,a.other_mdls_doc_type) doc_no, " +
             "a.other_mdls_doc_type, a.enbl_auto_misc_chrges, a.event_rgstr_id, " +
             "a.evnt_cost_category, a.allow_dues, a.event_doc_type " +
             "FROM scm.scm_sales_invc_hdr a " +
             "WHERE(a.invc_hdr_id = " + dochdrID +
             ")";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static long getPrsnItmVlID(long prsnID, long itmID, string trnsdte)
        {
            trnsdte = DateTime.ParseExact(trnsdte, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

            //string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string strSql = "Select a.item_pssbl_value_id FROM pasn.prsn_bnfts_cntrbtns a where((a.person_id = " +
          prsnID + ") and (a.item_id = " + itmID + ")) ORDER BY 1 DESC LIMIT 1 OFFSET 0";
            /*and (to_timestamp('" + trnsdte + "'," +
          "'YYYY-MM-DD HH24:MI:SS') between to_timestamp(valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
                  "AND to_timestamp(valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS')))*/
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -100000;
        }

        public static DataSet get_Basic_SalesDoc(
         string searchWord, string searchIn,
      Int64 offset, int limit_size, int orgID, bool onlySelf, bool shwUnpstdOnly)
        {
            string strSql = "";
            string whereClause = "";
            string crtdByClause = "";
            string unpstdCls = "";
            if (shwUnpstdOnly)
            {
                unpstdCls = @" AND EXISTS (SELECT f.src_doc_hdr_id 
FROM scm.scm_doc_amnt_smmrys f WHERE f.smmry_type='7Change/Balance' 
and round(f.smmry_amnt,2)>0 and a.invc_hdr_id=f.src_doc_hdr_id and f.src_doc_type=a.invc_type)";
                //unpstdCls = " AND (a.approval_status!='Approved')";
            }
            if (onlySelf == true)
            {
                crtdByClause = " AND (created_by=" + Global.mnFrm.cmCde.User_id + ")";
            }
            if (searchIn == "Document Number")
            {
                whereClause = "(a.invc_number ilike '" + searchWord.Replace("'", "''") +
              "') AND ";
            }
            else if (searchIn == "Document Description")
            {
                whereClause = "(a.comments_desc ilike '" + searchWord.Replace("'", "''") +
            "') AND ";
            }
            else if (searchIn == "Customer Name")
            {
                whereClause = "(a.customer_id IN (select c.cust_sup_id from scm.scm_cstmr_suplr c where c.cust_sup_name ilike '" + searchWord.Replace("'", "''") +
            "')) AND ";
            }
            else if (searchIn == "Source Doc. Number")
            {
                whereClause = "(a.src_doc_hdr_id IN (select c.invc_hdr_id from scm.scm_sales_invc_hdr c where c.invc_number ilike '" + searchWord.Replace("'", "''") +
            "') or scm.get_src_doc_num(a.other_mdls_doc_id, a.other_mdls_doc_type) ilike '" + searchWord.Replace("'", "''") +
            "') AND ";
            }
            else if (searchIn == "Approval Status")
            {
                whereClause = "(a.approval_status ilike '" + searchWord.Replace("'", "''") +
            "') AND ";
            }
            else if (searchIn == "Created By")
            {
                whereClause = "(a.created_by IN (select c.user_id from sec.sec_users c where c.user_name ilike '" + searchWord.Replace("'", "''") +
            "')) AND ";
            }

            strSql = "SELECT a.invc_hdr_id, a.invc_number, a.invc_type " +
         "FROM scm.scm_sales_invc_hdr a " +
         "WHERE (" + whereClause + "(a.org_id = " + orgID +
         ")" + crtdByClause + unpstdCls + ") ORDER BY a.invc_hdr_id DESC LIMIT " + limit_size +
         " OFFSET " + (Math.Abs(offset * limit_size)).ToString();

            Global.invcFrm.rec_SQL = strSql;
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static long get_Total_SalesDoc(string searchWord, string searchIn, int orgID, bool onlySelf, bool shwUnpstdOnly)
        {
            string strSql = "";
            string whereClause = "";
            string crtdByClause = "";
            string unpstdCls = "";
            if (shwUnpstdOnly)
            {
                unpstdCls = @" AND EXISTS (SELECT f.src_doc_hdr_id 
FROM scm.scm_doc_amnt_smmrys f WHERE f.smmry_type='7Change/Balance' 
and round(f.smmry_amnt,2)>0 and a.invc_hdr_id=f.src_doc_hdr_id and f.src_doc_type=a.invc_type) ";
                //unpstdCls = " AND (a.approval_status!='Approved')";
            }
            if (onlySelf == true)
            {
                crtdByClause = " AND (created_by=" + Global.mnFrm.cmCde.User_id + ")";
            }
            if (searchIn == "Document Number")
            {
                whereClause = "(a.invc_number ilike '" + searchWord.Replace("'", "''") +
              "') AND ";
            }
            else if (searchIn == "Document Description")
            {
                whereClause = "(a.comments_desc ilike '" + searchWord.Replace("'", "''") +
            "') AND ";
            }
            else if (searchIn == "Customer Name")
            {
                whereClause = "(a.customer_id IN (select c.cust_sup_id from scm.scm_cstmr_suplr c where c.cust_sup_name ilike '" + searchWord.Replace("'", "''") +
            "')) AND ";
            }
            else if (searchIn == "Source Doc. Number")
            {
                whereClause = "(a.src_doc_hdr_id IN (select c.invc_hdr_id from scm.scm_sales_invc_hdr c where c.invc_number ilike '" + searchWord.Replace("'", "''") +
            "')) AND ";
            }
            else if (searchIn == "Approval Status")
            {
                whereClause = "(a.approval_status ilike '" + searchWord.Replace("'", "''") +
            "') AND ";
            }
            else if (searchIn == "Created By")
            {
                whereClause = "(a.created_by IN (select c.user_id from sec.sec_users c where c.user_name ilike '" + searchWord.Replace("'", "''") +
            "')) AND ";
            }
            strSql = "SELECT count(1) " +
            "FROM scm.scm_sales_invc_hdr a " +
          "WHERE (" + whereClause + "(a.org_id = " + orgID + ")" + crtdByClause + unpstdCls + ")";
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
        #endregion

        #region "PURCHASE DOCUMENTS..."
        public static long getP_AttchmntID(string attchname, long hdrID)
        {
            string strSql = "";
            strSql = "SELECT a.attchmnt_id " +
         "FROM scm.scm_prchs_doc_attchmnts a " +
            "WHERE ((a.attchmnt_desc = '" + attchname.Replace("'", "''") +
              "') AND (a.doc_hdr_id = " + hdrID + "))";

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

        public static void createP_Attachment(long hdrID, string attchDesc,
         string filNm)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = "INSERT INTO scm.scm_prchs_doc_attchmnts(" +
                  "doc_hdr_id, attchmnt_desc, file_name, created_by, " +
                  "creation_date, last_update_by, last_update_date) " +
                              "VALUES (" + hdrID +
                              ", '" + attchDesc.Replace("'", "''") +
                              "', '" + filNm.Replace("'", "''") +
                              "', " + Global.myInv.user_id + ", '" + dateStr +
                              "', " + Global.myInv.user_id + ", '" + dateStr + "')";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void updateP_Attachment(long attchID, long hdrID, string attchDesc,
       string filNm)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE scm.scm_prchs_doc_attchmnts SET " +
                  "doc_hdr_id=" + hdrID +
                              ", attchmnt_desc='" + attchDesc.Replace("'", "''") +
                              "', file_name='" + filNm.Replace("'", "''") +
                              "', last_update_by=" + Global.myInv.user_id +
                              ", last_update_date='" + dateStr + "' " +
                               "WHERE attchmnt_id = " + attchID;
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void deleteP_Attchmnt(long attchid, string attchNm)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Attachment Name = " + attchNm;
            string delSql = "DELETE FROM scm.scm_prchs_doc_attchmnts WHERE(attchmnt_id = " + attchid + ")";
            Global.mnFrm.cmCde.deleteDataNoParams(delSql);
        }

        public static DataSet get_Pybls_Attachments(string searchWord, string searchIn,
     Int64 offset, int limit_size, long batchID, ref string attchSQL)
        {
            string strSql = "";
            if (searchIn == "Attachment Name/Description")
            {
                strSql = "SELECT a.attchmnt_id, a.doc_hdr_id, a.attchmnt_desc, a.file_name " +
              "FROM accb.accb_pybl_doc_attchmnts a " +
              "WHERE(a.attchmnt_desc ilike '" + searchWord.Replace("'", "''") +
              "' and a.doc_hdr_id = " + batchID + ") ORDER BY a.attchmnt_id LIMIT " + limit_size +
                  " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            attchSQL = strSql;
            return dtst;
        }

        public static long get_Total_Pybls_Attachments(string searchWord,
          string searchIn, long batchID)
        {
            string strSql = "";
            if (searchIn == "Attachment Name/Description")
            {
                strSql = "SELECT COUNT(1) " +
              "FROM accb.accb_pybl_doc_attchmnts a " +
              "WHERE(a.attchmnt_desc ilike '" + searchWord.Replace("'", "''") +
              "' and a.doc_hdr_id = " + batchID + ")";
            }
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            long sumRes = 0;
            if (dtst.Tables[0].Rows.Count > 0)
            {
                long.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out sumRes);
            }
            return sumRes;
        }

        public static DataSet get_P_Attachments(string searchWord, string searchIn,
       Int64 offset, int limit_size, long hdrID, ref string attchSQL)
        {
            string strSql = "";
            if (searchIn == "Attachment Name/Description")
            {
                strSql = "SELECT a.attchmnt_id, a.doc_hdr_id, a.attchmnt_desc, a.file_name " +
              "FROM scm.scm_prchs_doc_attchmnts a " +
              "WHERE(a.attchmnt_desc ilike '" + searchWord.Replace("'", "''") +
              "' and a.doc_hdr_id = " + hdrID + ") ORDER BY a.attchmnt_id LIMIT " + limit_size +
                  " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            attchSQL = strSql;
            return dtst;
        }

        public static long get_Total_P_Attachments(string searchWord,
          string searchIn, long hdrID)
        {
            string strSql = "";
            if (searchIn == "Attachment Name/Description")
            {
                strSql = "SELECT COUNT(1) " +
              "FROM scm.scm_prchs_doc_attchmnts a " +
              "WHERE(a.attchmnt_desc ilike '" + searchWord.Replace("'", "''") +
              "' and a.doc_hdr_id = " + hdrID + ")";
            }
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            long sumRes = 0;
            if (dtst.Tables[0].Rows.Count > 0)
            {
                long.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out sumRes);
            }
            return sumRes;
        }

        public static double getPrchsDocBscAmnt(long dochdrID, string docTyp)
        {
            string strSql = "select SUM(CASE WHEN code_id_behind >0 THEN -1*y.smmry_amnt ELSE y.smmry_amnt END) amnt " +
              "from scm.scm_doc_amnt_smmrys y " +
              "where y.src_doc_hdr_id=" + dochdrID +
              " and y.src_doc_type='" + docTyp + "' and y.smmry_type != '1Initial Amount' and y.smmry_type != '3Discount'";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            double rs = 0;

            if (dtst.Tables[0].Rows.Count > 0)
            {
                double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rs);
            }
            return rs;
        }

        public static double getPrchsDocFnlGrndAmnt(long dochdrID, string docTyp)
        {
            string strSql = "select SUM(y.smmry_amnt) amnt " +
              "from scm.scm_doc_amnt_smmrys y " +
              "where y.src_doc_hdr_id=" + dochdrID +
              " and y.src_doc_type='" + docTyp + "' and y.smmry_type != '5Grand Total'";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            double rs = 0;

            if (dtst.Tables[0].Rows.Count > 0)
            {
                double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rs);
            }
            return rs;
        }

        public static double getPrchsDocCodeAmnt(long dochdrID, int codeID, double grndAmnt)
        {
            string codeSQL = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes",
              "code_id", "sql_formular", codeID);
            codeSQL = codeSQL.Replace("{:qty}", "1").Replace("{:unit_price}", grndAmnt.ToString());
            if (codeSQL != "")
            {
                DataSet d1 = Global.mnFrm.cmCde.selectDataNoParams(codeSQL);
                double rs1 = 0;

                if (d1.Tables[0].Rows.Count > 0)
                {
                    double.TryParse(d1.Tables[0].Rows[0][0].ToString(), out rs1);
                }
                return rs1;
            }
            else
            {
                return 0.00;
            }
        }

        public static double getPrchDocGrndAmnt(long dochdrID)
        {
            string strSql = "select SUM(y.quantity*unit_price) amnt " +
              "from scm.scm_prchs_docs_det y " +
              "where y.prchs_doc_hdr_id=" + dochdrID + " ";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            double rs = 0;

            if (dtst.Tables[0].Rows.Count > 0)
            {
                double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rs);
            }
            return rs;
        }

        public static long getPrchDocLnID(int itmID,
          int storeID, long srcDocID)
        {
            string strSql = "select y.prchs_doc_line_id " +
              "from scm.scm_prchs_docs_det y " +
              "where y.itm_id= " + itmID +
              " and y.store_id=" + storeID +
              " and y.prchs_doc_hdr_id=" + srcDocID + " ";
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

        public static long getPrchsSmmryItmID(string smmryType, int codeBhnd,
          long srcDocID, string srcDocTyp, string smmryNm)
        {
            string strSql = "select y.smmry_id " +
              "from scm.scm_doc_amnt_smmrys y " +
              "where y.smmry_type= '" + smmryType + "' and y.smmry_name = '" + smmryNm +
              "' and y.code_id_behind= " + codeBhnd +
              " and y.src_doc_type='" + srcDocTyp +
              "' and y.src_doc_hdr_id=" + srcDocID + " ";
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

        public static int getUserStoreID()
        {
            string strSql = "select y.subinv_id " +
              "from inv.inv_itm_subinventories y, inv.inv_user_subinventories z " +
              "where y.subinv_id=z.subinv_id and " +
              "y.allow_sales = '1' and z.user_id = " + Global.myInv.user_id +
              " and y.org_id= " + Global.mnFrm.cmCde.Org_id + " order by 1 LIMIT 1 OFFSET 0 ";
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

        public static DataSet get_PyblsDocSmryLns(long dochdrID, string docTyp)
        {
            string strSql = "SELECT a.pybls_smmry_id, a.pybls_smmry_desc, " +
             "a.pybls_smmry_amnt, a.code_id_behind, a.pybls_smmry_type, a.auto_calc " +
             "FROM accb.accb_pybls_amnt_smmrys a " +
             "WHERE((a.src_pybls_hdr_id = " + dochdrID +
             ") and (a.src_pybls_type='" + docTyp + "')) ORDER BY a.pybls_smmry_type";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static DataSet get_DocSmryLns(long dochdrID, string docTyp)
        {
            string strSql = "SELECT a.smmry_id, CASE WHEN a.smmry_type='3Discount' THEN 'Discount' ELSE a.smmry_name END, " +
             "a.smmry_amnt, a.code_id_behind, a.smmry_type, a.auto_calc,REPLACE(REPLACE(a.smmry_type,'2Tax','3Tax'),'3Discount','2Discount') smtyp " +
             "FROM scm.scm_doc_amnt_smmrys a " +
             "WHERE((a.src_doc_hdr_id = " + dochdrID +
             ") and (a.src_doc_type='" + docTyp + "')) ORDER BY 7";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (Global.pOdrFrm != null)
            {
                Global.pOdrFrm.smmry_SQL = strSql;
            }
            if (Global.invcFrm != null)
            {
                Global.invcFrm.smmry_SQL = strSql;
            }
            return dtst;
        }

        public static double get_DocSmryGrndTtl(long dochdrID, string docTyp)
        {
            string strSql = "SELECT a.smmry_amnt " +
             "FROM scm.scm_doc_amnt_smmrys a " +
             "WHERE((a.src_doc_hdr_id = " + dochdrID +
             ") and (a.src_doc_type='" + docTyp +
             "') and (a.smmry_type='5Grand Total'))";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            double res = 0;
            if (dtst.Tables[0].Rows.Count > 0)
            {
                double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out res);
            }
            return res;
        }

        public static double get_DocSmryOutsbls(long dochdrID, string docTyp)
        {
            string strSql = "SELECT a.smmry_amnt " +
             "FROM scm.scm_doc_amnt_smmrys a " +
             "WHERE((a.src_doc_hdr_id = " + dochdrID +
             ") and (a.src_doc_type='" + docTyp +
             "') and (a.smmry_type='7Change/Balance'))";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            double res = 0;
            if (dtst.Tables[0].Rows.Count > 0)
            {
                double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out res);
            }
            return res;
        }

        public static double getCodeAmnt(int codeID, double grndAmnt)
        {
            string codeSQL = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes",
              "code_id", "sql_formular", codeID);
            codeSQL = codeSQL.Replace("{:qty}", "1").Replace("{:unit_price}", grndAmnt.ToString());
            if (codeSQL != "")
            {
                DataSet d1 = Global.mnFrm.cmCde.selectDataNoParams(codeSQL);
                double rs1 = 0;

                if (d1.Tables[0].Rows.Count > 0)
                {
                    double.TryParse(d1.Tables[0].Rows[0][0].ToString(), out rs1);
                }
                return rs1;
            }
            else
            {
                return 0.00;
            }
        }

        public static bool isTaxWthHldng(int codeID)
        {
            string strSql = "Select scm.istaxwthhldng(" + codeID + ")";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);

            if (dtst.Tables[0].Rows.Count > 0)
            {
                if (dtst.Tables[0].Rows[0][0].ToString() == "1")
                {
                    return true;
                }
            }
            return false;
        }

        public static DataSet get_One_CnsgnmntLines(long dochdrID)
        {
            string strSql = @"select c.line_id, c.itm_id, c.quantity_rcvd, c.cost_price, 
                (c.quantity_rcvd * c.cost_price) amnt,
                  c.subinv_id, c.stock_id, 
                 c.lifespan, c.tag_number, c.serial_number, 
                 c.consignmt_condition, c.remarks, " +
             "c.consgmt_id, c.po_line_id, b.base_uom_id, b.item_code, b.item_desc, a.uom_name " +
             "from inv.inv_consgmt_rcpt_det c, inv.inv_itm_list b, inv.unit_of_measure a " +
             "where c.rcpt_id = " + dochdrID + " and c.itm_id = b.item_id and b.base_uom_id=a.uom_id";

            //string strSql = "SELECT a.prchs_doc_line_id, a.itm_id, " +
            //  "a.quantity, a.unit_price, (a.quantity * a.unit_price) amnt, " +
            //  "a.store_id, a.crncy_id, (a.quantity - a.rqstd_qty_ordrd) avlbl_qty, a.src_line_id " +
            // "FROM scm.scm_prchs_docs_det a " +
            // "WHERE(a.prchs_doc_hdr_id = " + dochdrID +
            // " and a.prchs_doc_hdr_id > 0) ORDER BY a.prchs_doc_line_id";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            //Global.pOdrFrm.recDt_SQL = strSql;
            return dtst;
        }

        public static DataSet get_One_CnsgnmntRtrnLines(long dochdrID)
        {
            string strSql = @"select c.line_id, c.itm_id, c.qty_rtnd, d.cost_price, 
                (c.qty_rtnd * d.cost_price) amnt,
                  d.subinv_id, d.stock_id, 
                 d.lifespan, d.tag_number, d.serial_number, 
                 c.rtnd_reason, c.remarks, " +
             "d.consgmt_id, d.po_line_id, b.base_uom_id, b.item_code, b.item_desc, a.uom_name " +
             "from inv.inv_consgmt_rcpt_rtns_det c,inv.inv_consgmt_rcpt_det d, inv.inv_itm_list b, inv.unit_of_measure a " +
             "where c.rtns_hdr_id = " + dochdrID + " and d.line_id = c.rcpt_line_id and c.itm_id = b.item_id and b.base_uom_id=a.uom_id";

            //string strSql = "SELECT a.prchs_doc_line_id, a.itm_id, " +
            //  "a.quantity, a.unit_price, (a.quantity * a.unit_price) amnt, " +
            //  "a.store_id, a.crncy_id, (a.quantity - a.rqstd_qty_ordrd) avlbl_qty, a.src_line_id " +
            // "FROM scm.scm_prchs_docs_det a " +
            // "WHERE(a.prchs_doc_hdr_id = " + dochdrID +
            // " and a.prchs_doc_hdr_id > 0) ORDER BY a.prchs_doc_line_id";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            //Global.pOdrFrm.recDt_SQL = strSql;
            return dtst;
        }

        public static DataSet get_One_PrchsDcLines(long dochdrID)
        {
            string strSql = "SELECT a.prchs_doc_line_id, a.itm_id, " +
              "a.quantity, a.unit_price, (a.quantity * a.unit_price) amnt, " +
              "a.store_id, a.crncy_id, (a.quantity - a.rqstd_qty_ordrd) avlbl_qty, " +
              "a.src_line_id, b.base_uom_id, b.item_code, b.item_desc, c.uom_name, " +
            "CASE WHEN a.alternate_item_name='' THEN b.item_desc ELSE a.alternate_item_name END, d.cat_name " +
             "FROM scm.scm_prchs_docs_det a, inv.inv_itm_list b, inv.unit_of_measure c, inv.inv_product_categories d " +
             "WHERE(a.prchs_doc_hdr_id = " + dochdrID +
             " and a.prchs_doc_hdr_id > 0 and a.itm_id = b.item_id and b.base_uom_id=c.uom_id and d.cat_id = b.category_id) ORDER BY b.category_id, a.prchs_doc_line_id";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            Global.pOdrFrm.recDt_SQL = strSql;
            return dtst;
        }

        public static double get_One_ReqLnQty(long dochdrID, int itmID, int storeID)
        {
            string strSql = "SELECT (a.quantity - a.rqstd_qty_ordrd) avlbl_qty " +
             "FROM scm.scm_prchs_docs_det a " +
             "WHERE(a.prchs_doc_hdr_id = " + dochdrID +
             " and a.itm_id = " + itmID + " and a.store_id = "
             + storeID + ") ORDER BY a.prchs_doc_line_id";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            double rs = 0;
            if (dtst.Tables[0].Rows.Count > 0)
            {
                double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rs);
            }
            return rs;
        }

        public static double get_One_POLnQty(long reqhdrID, int itmID, int storeID)
        {
            string strSql = "SELECT SUM(a.quantity) ordrd_qty " +
             "FROM scm.scm_prchs_docs_det a " +
             "WHERE(a.prchs_doc_hdr_id IN(select b.prchs_doc_hdr_id " +
             "from scm.scm_prchs_docs_hdr b where b.requisition_id = " + reqhdrID +
             " and b.requisition_id>0) and a.itm_id = " + itmID + " and a.store_id = "
             + storeID + ")";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            double rs = 0;
            if (dtst.Tables[0].Rows.Count > 0)
            {
                double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rs);
            }
            return rs;
        }

        public static DataSet get_One_PrchsDcDt(long dochdrID)
        {
            string strSql = @"SELECT a.prchs_doc_hdr_id, a.purchase_doc_num, 
        a.purchase_doc_type, a.requisition_id, 
      to_char(to_timestamp(a.prchs_doc_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY'), 
      to_char(to_timestamp(a.need_by_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY'), " +
             "a.supplier_id, a.supplier_site_id, a.comments_desc, " +
             "a.approval_status, a.next_aproval_action, " +
             "a.created_by, a.prntd_doc_curr_id, a.exchng_rate, a.payment_terms  " +
             "FROM scm.scm_prchs_docs_hdr a " +
             "WHERE(a.prchs_doc_hdr_id = " + dochdrID +
             ") ORDER BY a.purchase_doc_type, a.purchase_doc_num";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static DataSet get_Basic_PrchsDoc(
          string searchWord, string searchIn,
      Int64 offset, int limit_size, int orgID, bool onlySelf)
        {
            string strSql = "";
            string whereClause = "";
            string crtdByClause = "";
            if (onlySelf == true)
            {
                crtdByClause = " AND (created_by=" + Global.mnFrm.cmCde.User_id + ")";
            }
            if (searchIn == "Document Number")
            {
                whereClause = "(a.purchase_doc_num ilike '" + searchWord.Replace("'", "''") +
              "') AND ";
            }
            else if (searchIn == "Document Description")
            {
                whereClause = "(a.comments_desc ilike '" + searchWord.Replace("'", "''") +
            "') AND ";
            }
            else if (searchIn == "Supplier Name")
            {
                whereClause = "(a.supplier_id IN (select c.cust_sup_id from scm.scm_cstmr_suplr c where c.cust_sup_name ilike '" + searchWord.Replace("'", "''") +
            "')) AND ";
            }
            else if (searchIn == "Requisition Number")
            {
                whereClause = "(a.requisition_id IN (select c.prchs_doc_hdr_id from scm.scm_prchs_docs_hdr c where c.purchase_doc_num ilike '" + searchWord.Replace("'", "''") +
            "')) AND ";
            }
            else if (searchIn == "Approval Status")
            {
                whereClause = "(a.approval_status ilike '" + searchWord.Replace("'", "''") +
            "') AND ";
            }
            else if (searchIn == "Created By")
            {
                whereClause = "(a.created_by IN (select c.user_id from sec.sec_users c where c.user_name ilike '" + searchWord.Replace("'", "''") +
            "')) AND ";
            }

            strSql = "SELECT a.prchs_doc_hdr_id, a.purchase_doc_num, a.purchase_doc_type " +
         "FROM scm.scm_prchs_docs_hdr a " +
         "WHERE (" + whereClause + "(a.org_id = " + orgID +
         ")" + crtdByClause + ") ORDER BY a.prchs_doc_hdr_id DESC LIMIT " + limit_size +
         " OFFSET " + (Math.Abs(offset * limit_size)).ToString();

            Global.pOdrFrm.rec_SQL = strSql;
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static long get_Total_PrchsDoc(string searchWord, string searchIn, int orgID, bool onlySelf)
        {
            string strSql = "";
            string whereClause = "";
            string crtdByClause = "";
            if (onlySelf == true)
            {
                crtdByClause = " AND (created_by=" + Global.mnFrm.cmCde.User_id + ")";
            }
            if (searchIn == "Document Number")
            {
                whereClause = "(a.purchase_doc_num ilike '" + searchWord.Replace("'", "''") +
              "') AND ";
            }
            else if (searchIn == "Document Description")
            {
                whereClause = "(a.comments_desc ilike '" + searchWord.Replace("'", "''") +
              "') AND ";
            }
            else if (searchIn == "Supplier Name")
            {
                whereClause = "(a.supplier_id IN (select c.cust_sup_id from " +
                  "scm.scm_cstmr_suplr c where c.cust_sup_name ilike '" + searchWord.Replace("'", "''") +
              "')) AND ";
            }
            else if (searchIn == "Requisition Number")
            {
                whereClause = "(a.requisition_id IN (select c.prchs_doc_hdr_id from scm.scm_prchs_docs_hdr c where c.purchase_doc_num ilike '" + searchWord.Replace("'", "''") +
              "')) AND ";
            }
            else if (searchIn == "Approval Status")
            {
                whereClause = "(a.approval_status ilike '" + searchWord.Replace("'", "''") +
              "') AND ";
            }
            else if (searchIn == "Created By")
            {
                whereClause = "(a.created_by IN (select c.user_id from sec.sec_users c where c.user_name ilike '" + searchWord.Replace("'", "''") +
              "')) AND ";
            }
            strSql = "SELECT count(1) " +
            "FROM scm.scm_prchs_docs_hdr a " +
            "WHERE (" + whereClause + "(a.org_id = " + orgID + ")" + crtdByClause + ")";
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
        #endregion

        #region "GL INTERFACE..."
        public static DataSet getDocGLInfcLns(long docID, string srcDocType)
        {
            string strSql = "SELECT * FROM scm.scm_gl_interface WHERE src_doc_id = " +
              docID + " and src_doc_typ ilike '%" + srcDocType.Replace("'", "''") + "%' and gl_batch_id != -1";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static DataSet getDocGLInfcLns(long intrfcID)
        {
            string strSql = "SELECT * FROM scm.scm_gl_interface WHERE interface_id = " +
              intrfcID + "  and gl_batch_id != -1";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static long getIntFcTrnsDbtLn(long srcLnID, string srcDocType,
          double amount, int accntID, string trns_desc)
        {
            string strSql = "SELECT a.interface_id FROM scm.scm_gl_interface a " +
                    "WHERE a.src_doc_line_id = " + srcLnID +
              " and a.src_doc_typ = '" + srcDocType.Replace("'", "''") +
              "' and a.dbt_amount = " + amount + " and a.accnt_id = " + accntID +
              " and a.transaction_desc = '" + trns_desc.Replace("'", "''") + "'";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public static long getIntFcTrnsCrdtLn(long srcLnID, string srcDocType,
          double amount, int accntID, string trns_desc)
        {
            string strSql = "SELECT a.interface_id FROM scm.scm_gl_interface a " +
               "WHERE a.src_doc_line_id = " + srcLnID +
         " and a.src_doc_typ = '" + srcDocType.Replace("'", "''") +
         "' and a.crdt_amount = " + amount + " and a.accnt_id = " + accntID +
         " and a.transaction_desc = '" + trns_desc.Replace("'", "''") + "'";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public static long getPymntRcvdID(long srcDocID, string srcDocType, string dteRcvd, double amount)
        {
            //if (dteRcvd.Length > 11)
            //{
            //  dteRcvd = dteRcvd.Substring(0, 11);
            //}
            string strSql = "SELECT a.pymnt_id FROM scm.scm_payments a " +
               "WHERE a.src_doc_id = " + srcDocID +
         " and a.src_doc_typ = '" + srcDocType.Replace("'", "''") +
         "' and a.amount_paid = " + amount +
         " and to_char(to_timestamp(date_rcvd,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') ilike '%" + dteRcvd.Replace("'", "''") +
         "%'";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
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

        public static double[] getGLIntrfcIDAmntSum(string intrfcids, int accntID)
        {
            double[] res = { 0, 0 };
            string strSql = @"SELECT COALESCE(SUM(a.dbt_amount),0), COALESCE(SUM(a.crdt_amount),0)
FROM scm.scm_gl_interface a
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

        public static bool isGLIntrfcBlcdOrg(int orgID, ref double dffrce)
        {
            string strSql = @"SELECT COALESCE(SUM(a.dbt_amount),0) dbt_sum, 
COALESCE(SUM(a.crdt_amount),0) crdt_sum 
FROM scm.scm_gl_interface a, accb.accb_chart_of_accnts b 
WHERE a.gl_batch_id = -1 and a.accnt_id = b.accnt_id and b.org_id=" + orgID +
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

        public static DataSet getAllInGLIntrfcOrg(int orgID)
        {
            string strSql = @"SELECT a.accnt_id, 
to_char(to_timestamp(a.trnsctn_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS')
, SUM(a.dbt_amount) dbt_sum, " +
            "SUM(a.crdt_amount) crdt_sum, SUM(a.net_amount) net_sum, a.func_cur_id " +
            "FROM scm.scm_gl_interface a, accb.accb_chart_of_accnts b " +
            "WHERE a.gl_batch_id = -1 and a.accnt_id = b.accnt_id and b.org_id=" + orgID +
            " /*and NOT EXISTS(select f.transctn_id from accb.accb_trnsctn_details f " +
            "where f.batch_id IN (select g.batch_id from accb.accb_trnsctn_batches g " +
            "where g.batch_name ilike '%Inventory%' and " +
            "to_timestamp(g.creation_date,'YYYY-MM-DD HH24:MI:SS') between " +
            "(to_timestamp(a.trnsctn_date,'YYYY-MM-DD HH24:MI:SS') - interval '6 months') " +
            "and (to_timestamp(a.trnsctn_date,'YYYY-MM-DD HH24:MI:SS') + interval '6 months')) " +
            "and f.source_trns_ids like '%,' || a.interface_id || ',%' and " +
            "f.trnsctn_date=a.trnsctn_date and f.accnt_id= a.accnt_id)*/ " +
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
            string strSql = "select distinct a.interface_id from scm.scm_gl_interface a " +
                 "where a.accnt_id = " + accntid + " and a.trnsctn_date = '" + trns_date +
                 "' and a.func_cur_id = " + crncy_id + " and a.gl_batch_id = -1  " +
                 "ORDER BY a.interface_id";
            /*and NOT EXISTS(select f.transctn_id from accb.accb_trnsctn_details f " +
                 "where f.batch_id IN (select g.batch_id from accb.accb_trnsctn_batches g " +
                 "where g.batch_name ilike '%Sales & Purchasing%' and " +
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
                usrTrnsSql = " and (trns_source !='SYS') ";
            }

            if (imblcnTrns)
            {
                //where gl_batch_id = -1
                imblnce_trns = @" and (a.interface_id IN (select MAX(v.interface_id)
      from  scm.scm_gl_interface v
      group by v.src_doc_typ, v.src_doc_id, abs(v.net_amount), v.src_doc_line_id
      having count(v.src_doc_line_id) %2 != 0 or v.src_doc_id<=0 or v.src_doc_id IS NULL 
or scm.get_src_doc_num(v.src_doc_id,v.src_doc_typ) IS NULL 
or scm.get_src_doc_num(v.src_doc_id,v.src_doc_typ)=''))";
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
                whereCls = "(a.src_doc_typ ilike '" + searchWord.Replace("'", "''") +
               "') and ";
            }
            else if (searchIn == "Transaction Description")
            {
                whereCls = "(a.transaction_desc ilike '" + searchWord.Replace("'", "''") +
            "') and ";
            }
            strSql = @"SELECT a.accnt_id, b.accnt_num, b.accnt_name, a.transaction_desc, 
to_char(to_timestamp(a.trnsctn_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), a.dbt_amount, " +
         "a.crdt_amount, a.src_doc_line_id, a.src_doc_typ, a.gl_batch_id, " +
         "(select d.batch_name from accb.accb_trnsctn_batches d where d.batch_id = a.gl_batch_id) btch_nm, a.interface_id, a.func_cur_id " +
         ", a.src_doc_id, scm.get_src_doc_num(a.src_doc_id,a.src_doc_typ) " +
         "FROM scm.scm_gl_interface a, accb.accb_chart_of_accnts b " +
         "WHERE ((a.accnt_id = b.accnt_id) and " + whereCls + "(b.org_id = " + orgID + ")" + to_gl +
         imblnce_trns + usrTrnsSql + amntCls + " and (to_timestamp(a.trnsctn_date,'YYYY-MM-DD HH24:MI:SS') between to_timestamp('" + dte1 +
         "','YYYY-MM-DD HH24:MI:SS') AND to_timestamp('" + dte2 + "','YYYY-MM-DD HH24:MI:SS'))) " +
         "ORDER BY a.interface_id DESC LIMIT " + limit_size +
         " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            Global.glFrm.vwInfcSQLStmnt = strSql;
            return dtst;
        }

        public static long get_Total_Infc(string searchWord, string searchIn,
         int orgID, string dte1, string dte2, bool notgonetogl,
          bool imblcnTrns, bool usrTrns, decimal lowVal, decimal highVal)
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
                usrTrnsSql = " and (trns_source !='SYS')";
            }

            if (imblcnTrns)
            {
                //where gl_batch_id = -1
                imblnce_trns = @" and (a.interface_id IN (select MAX(v.interface_id)
      from  scm.scm_gl_interface v
      group by v.src_doc_typ, v.src_doc_id, abs(v.net_amount), v.src_doc_line_id
      having count(v.src_doc_line_id) %2 != 0 or v.src_doc_id<=0 or v.src_doc_id IS NULL 
or scm.get_src_doc_num(v.src_doc_id,v.src_doc_typ) IS NULL 
or scm.get_src_doc_num(v.src_doc_id,v.src_doc_typ)=''))";
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
                whereCls = "(a.src_doc_typ ilike '" + searchWord.Replace("'", "''") +
               "') and ";
            }
            else if (searchIn == "Transaction Description")
            {
                whereCls = "(a.transaction_desc ilike '" + searchWord.Replace("'", "''") +
            "') and ";
            }
            strSql = "SELECT count(1) " +
         "FROM scm.scm_gl_interface a, accb.accb_chart_of_accnts b " +
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

        #endregion

        #region "PAYMENTS DONE..."
        public static DataSet get_LastScmPay_Trns(long docID, string docType, int orgID)
        {
            string strSql = "";
            strSql = "SELECT a.pymnt_id, a.pymnt_type, a.amount_paid, a.custmrs_balance, a.pymnt_remark, " +
                  "a.src_doc_typ, a.src_doc_id, a.created_by, to_char(to_timestamp(a.date_rcvd,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), b.invc_number, c.user_name " +
             "FROM scm.scm_payments a, scm.scm_sales_invc_hdr b, sec.sec_users c " +
             "WHERE(a.src_doc_id = " + docID +
             " and a.src_doc_typ = '" + docType.Replace("'", "''") +
             "') and (a.src_doc_id = b.invc_hdr_id and b.org_id = " + orgID +
             " and a.created_by = c.user_id) " +
             "ORDER BY to_timestamp(a.date_rcvd,'YYYY-MM-DD HH24:MI:SS') DESC, a.pymnt_id DESC LIMIT 1 " +
               " OFFSET 0";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static DataSet get_LastRcvblPay_Trns(long docID, string docType, int orgID)
        {
            string strSql = "";
            strSql = "SELECT a.pymnt_id, accb.get_pymnt_mthd_name(a.pymnt_mthd_id), a.amount_paid, a.change_or_balance, a.pymnt_remark, " +
                  "a.src_doc_typ, a.src_doc_id, a.created_by, to_char(to_timestamp(a.pymnt_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), b.rcvbls_invc_number, c.user_name " +
             "FROM accb.accb_payments a, accb.accb_rcvbls_invc_hdr b, sec.sec_users c " +
             "WHERE(a.src_doc_id = " + docID +
             " and a.src_doc_typ = '" + docType.Replace("'", "''") +
             "') and (a.src_doc_id = b.rcvbls_invc_hdr_id and b.org_id = " + orgID +
             " and a.created_by = c.user_id) " +
             "ORDER BY to_timestamp(a.pymnt_date,'YYYY-MM-DD HH24:MI:SS') DESC, a.pymnt_id DESC LIMIT 1 " +
               " OFFSET 0";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static DataSet get_ScmPay_Trns(string searchWord, string searchIn,
      Int64 offset, int limit_size, int orgID, string dte1, string dte2)
        {
            dte1 = DateTime.ParseExact(
         dte1, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

            dte2 = DateTime.ParseExact(
         dte2, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            string strSql = "";
            string whereCls = "";

            if (searchIn == "Document No.")
            {
                whereCls = "(b.invc_number ilike '" + searchWord.Replace("'", "''") +
               "') and ";
            }
            else if (searchIn == "Document Type")
            {
                whereCls = "(a.src_doc_typ ilike '" + searchWord.Replace("'", "''") +
            "') and ";
            }
            else if (searchIn == "Payment Type")
            {
                whereCls = "(a.pymnt_type ilike '" + searchWord.Replace("'", "''") +
            "') and ";
            }
            else if (searchIn == "Received By")
            {
                whereCls = "(c.user_name ilike '" + searchWord.Replace("'", "''") +
            "') and ";
            }
            else if (searchIn == "Transaction Description")
            {
                whereCls = "(a.pymnt_remark ilike '" + searchWord.Replace("'", "''") +
            "') and ";
            }
            strSql = "SELECT a.pymnt_id, a.pymnt_type, a.amount_paid, a.custmrs_balance, a.pymnt_remark, " +
                  "a.src_doc_typ, a.src_doc_id, a.created_by, a.date_rcvd, b.invc_number, c.user_name " +
             "FROM scm.scm_payments a, scm.scm_sales_invc_hdr b, sec.sec_users c " +
             "WHERE(" + whereCls + "(a.src_doc_id = b.invc_hdr_id and b.org_id = " + orgID +
             " and a.created_by = c.user_id)" +
             " and (to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS') between to_timestamp('" + dte1 +
             "','YYYY-MM-DD HH24:MI:SS') AND to_timestamp('" + dte2 + "','YYYY-MM-DD HH24:MI:SS'))) " +
             "ORDER BY a.pymnt_id DESC LIMIT " + limit_size +
               " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            Global.mnFrm.trnsDet_SQL = strSql;
            return dtst;
        }

        public static long get_Total_ScmTrns(string searchWord, string searchIn,
         int orgID, string dte1, string dte2)
        {
            dte1 = DateTime.ParseExact(
         dte1, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

            dte2 = DateTime.ParseExact(
         dte2, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            string strSql = "";
            string whereCls = "";

            if (searchIn == "Document No.")
            {
                whereCls = "(b.invc_number ilike '" + searchWord.Replace("'", "''") +
               "') and ";
            }
            else if (searchIn == "Document Type")
            {
                whereCls = "(a.src_doc_typ ilike '" + searchWord.Replace("'", "''") +
            "') and ";
            }
            else if (searchIn == "Payment Type")
            {
                whereCls = "(a.pymnt_type ilike '" + searchWord.Replace("'", "''") +
            "') and ";
            }
            else if (searchIn == "Received By")
            {
                whereCls = "(c.user_name ilike '" + searchWord.Replace("'", "''") +
            "') and ";
            }
            else if (searchIn == "Transaction Description")
            {
                whereCls = "(a.pymnt_remark ilike '" + searchWord.Replace("'", "''") +
            "') and ";
            }
            strSql = "SELECT count(1) " +
             "FROM scm.scm_payments a, scm.scm_sales_invc_hdr b, sec.sec_users c " +
             "WHERE(" + whereCls + "(a.src_doc_id = b.invc_hdr_id and b.org_id = " + orgID +
             " and a.created_by = c.user_id)" +
             " and (to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS') between to_timestamp('" + dte1 +
             "','YYYY-MM-DD HH24:MI:SS') AND to_timestamp('" + dte2 + "','YYYY-MM-DD HH24:MI:SS'))) ";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            long sumRes = 0;
            if (dtst.Tables[0].Rows.Count > 0)
            {
                long.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out sumRes);
            }
            return sumRes;
        }


        public static DataSet get_Pay_Trns(string searchWord, string searchIn,
      Int64 offset, int limit_size, string dte1, string dte2)
        {
            dte1 = DateTime.ParseExact(
         dte1, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

            dte2 = DateTime.ParseExact(
         dte2, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            string strSql = "";
            string whereCls = "";

            if (searchIn == "Source Document No.")
            {
                whereCls = " and (accb.get_src_doc_num(a.src_doc_id, a.src_doc_typ) ilike '" + searchWord.Replace("'", "''") +
               "')";
            }
            else if (searchIn == "Source Document Type")
            {
                whereCls = " and (a.src_doc_typ ilike '" + searchWord.Replace("'", "''") +
            "')";
            }
            else if (searchIn == "Payment Method")
            {
                whereCls = " and (accb.get_pymnt_mthd_name(a.pymnt_mthd_id) ilike '" + searchWord.Replace("'", "''") +
            "')";
            }
            else if (searchIn == "Cashier")
            {
                whereCls = " and (sec.get_usr_name(a.created_by) ilike '" + searchWord.Replace("'", "''") +
            "')";
            }
            else if (searchIn == "Payment Description")
            {
                whereCls = " and (a.pymnt_remark ilike '" + searchWord.Replace("'", "''") +
            "')";
            }
            strSql = @"SELECT a.pymnt_id, a.pymnt_mthd_id, accb.get_pymnt_mthd_name(a.pymnt_mthd_id), 
      a.amount_paid, a.change_or_balance, a.pymnt_remark, 
      a.src_doc_typ, a.src_doc_id, accb.get_src_doc_num(a.src_doc_id, a.src_doc_typ), 
      a.created_by, to_char(to_timestamp(a.pymnt_date, 'YYYY-MM-DD HH24:MI:SS'), 'DD-Mon-YYYY HH24:MI:SS'), 
      sec.get_usr_name(a.created_by), gl_batch_id, accb.get_gl_batch_name(gl_batch_id), b.pymnt_batch_name, a.pymnt_batch_id " +
             "FROM accb.accb_payments a, accb.accb_payments_batches b " +
             "WHERE((a.pymnt_batch_id = b.pymnt_batch_id)" + whereCls +
             " and (to_timestamp(a.pymnt_date,'YYYY-MM-DD HH24:MI:SS') between to_timestamp('" + dte1 +
             "','YYYY-MM-DD HH24:MI:SS') AND to_timestamp('" + dte2 + "','YYYY-MM-DD HH24:MI:SS'))) " +
             "ORDER BY a.pymnt_id DESC LIMIT " + limit_size +
               " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            Global.mnFrm.pymntsGvn_SQL = strSql;
            return dtst;
        }

        public static long get_Total_Trns(string searchWord, string searchIn,
         string dte1, string dte2)
        {
            dte1 = DateTime.ParseExact(
         dte1, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

            dte2 = DateTime.ParseExact(
         dte2, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            string strSql = "";
            string whereCls = "";

            if (searchIn == "Source Document No.")
            {
                whereCls = " and (accb.get_src_doc_num(a.src_doc_id, a.src_doc_typ) ilike '" + searchWord.Replace("'", "''") +
               "')";
            }
            else if (searchIn == "Source Document Type")
            {
                whereCls = " and (a.src_doc_typ ilike '" + searchWord.Replace("'", "''") +
            "')";
            }
            else if (searchIn == "Payment Method")
            {
                whereCls = " and (accb.get_pymnt_mthd_name(a.pymnt_mthd_id) ilike '" + searchWord.Replace("'", "''") +
            "')";
            }
            else if (searchIn == "Cashier")
            {
                whereCls = " and (sec.get_usr_name(a.created_by) ilike '" + searchWord.Replace("'", "''") +
            "')";
            }
            else if (searchIn == "Payment Description")
            {
                whereCls = " and (a.pymnt_remark ilike '" + searchWord.Replace("'", "''") +
            "')";
            }
            strSql = @"SELECT count(1) " +
             "FROM accb.accb_payments a, accb.accb_payments_batches b " +
             "WHERE((a.pymnt_batch_id = b.pymnt_batch_id)" + whereCls +
             " and (to_timestamp(a.pymnt_date,'YYYY-MM-DD HH24:MI:SS') between to_timestamp('" + dte1 +
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

        #region "DUES PAYMENTS..."
        public static DataSet get_One_AdvcItmDet(string ItemName)
        {
            string itmSQL = @"SELECT a.item_id, a.item_code_name, a.item_value_uom, 
(CASE WHEN a.item_min_type='Earnings' or a.item_min_type='Employer Charges' THEN 'Payment by Organisation' 
WHEN a.item_min_type='Bills/Charges' or a.item_min_type='Deductions' THEN 'Payment by Person' 
ELSE 'Purely Informational' END) trns_typ 
FROM org.org_pay_items a 
WHERE a.item_code_name = '" + ItemName.Replace("'", "''") + "' AND a.org_id = " + Global.mnFrm.cmCde.Org_id + @"";

            string strSql = "";
            strSql = @"SELECT -1, tbl1.item_code_name, tbl1.item_value_uom, tbl1.trns_typ, 
     tbl1.item_id, pay.get_first_itmval_id(tbl1.item_id), 
      b.item_maj_type, b.item_min_type, b.inv_item_id, inv.get_invitm_name(b.inv_item_id), 
      inv.get_uom_name(c.base_uom_id), c.item_type " +
                    "FROM (" + itmSQL + ") tbl1, org.org_pay_items b, inv.inv_itm_list c " +
                    "WHERE ((tbl1.item_id = b.item_id and b.inv_item_id = c.item_id) and (b.is_enabled = '1')) " +
          "ORDER BY b.pay_run_priority ";
            //Global.mnFrm.cmCde.showSQLNoPermsn(strSql);
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
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

        public static DataSet get_One_ItmStDet(int itmStID)
        {
            string itmSQL = Global.mnFrm.cmCde.getGnrlRecNm("pay.pay_itm_sets_hdr",
              "hdr_id", "sql_query", itmStID);
            string strSql = "";
            string mnlSQL = "";
            string whereCls = "";
            mnlSQL = "SELECT a.det_id, b.item_code_name, b.item_value_uom, " +
              @"a.to_do_trnsctn_type, a.item_id, pay.get_first_itmval_id(a.item_id), 
      b.item_maj_type, b.item_min_type, b.inv_item_id, 
      inv.get_invitm_name(b.inv_item_id), inv.get_uom_name(c.base_uom_id), c.item_type " +
          "FROM pay.pay_itm_sets_det a , org.org_pay_items b, inv.inv_itm_list c " +
          "WHERE((a.hdr_id = " + itmStID + ") and (a.item_id = b.item_id and b.inv_item_id = c.item_id) and (b.is_enabled = '1')) ORDER BY b.pay_run_priority ";

            strSql = @"SELECT -1, tbl1.item_code_name, tbl1.item_value_uom, tbl1.trns_typ, 
     tbl1.item_id, pay.get_first_itmval_id(tbl1.item_id), 
      b.item_maj_type, b.item_min_type, b.inv_item_id, inv.get_invitm_name(b.inv_item_id), 
      inv.get_uom_name(c.base_uom_id), c.item_type " +
                    "FROM (" + itmSQL + ") tbl1, org.org_pay_items b, inv.inv_itm_list c " +
                    "WHERE ((tbl1.item_id = b.item_id and b.inv_item_id = c.item_id) and (b.is_enabled = '1')) " +
          "ORDER BY b.pay_run_priority ";
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
              @"a.to_do_trnsctn_type, a.item_id, c.item_pssbl_value_id, b.item_maj_type,
b.item_min_type, b.inv_item_id, inv.get_invitm_name(b.inv_item_id), 
      inv.get_uom_name(d.base_uom_id), d.item_type " +
          "FROM pay.pay_itm_sets_det a , org.org_pay_items b, pasn.prsn_bnfts_cntrbtns c, inv.inv_itm_list d " +
          "WHERE(a.hdr_id = " + itmStID + ") and (a.item_id = b.item_id and b.inv_item_id = d.item_id) and (b.is_enabled = '1') and " +
          "(a.item_id = c.item_id) AND (c.person_id = " + prsnID +
             ") and (now() between to_timestamp(c.valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
                  "AND to_timestamp(c.valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS')) ORDER BY b.pay_run_priority, b.item_code_name LIMIT 100 OFFSET 0";

            strSql = @"SELECT -1, tbl1.item_code_name, tbl1.item_value_uom, tbl1.trns_typ, 
      tbl1.item_id, b.item_pssbl_value_id, a.item_maj_type, a.item_min_type, 
      a.inv_item_id, inv.get_invitm_name(a.inv_item_id), 
      inv.get_uom_name(d.base_uom_id), d.item_type " +
                    "FROM (" + itmSQL + ") tbl1, org.org_pay_items a, pasn.prsn_bnfts_cntrbtns b, inv.inv_itm_list d " +
                    "WHERE ((tbl1.item_id = a.item_id) and (a.item_id=b.item_id and a.inv_item_id = d.item_id) and (a.is_enabled = '1') AND (b.person_id = " + prsnID +
          ") and (now() between to_timestamp(b.valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
                  "AND to_timestamp(b.valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS'))) " +
          "ORDER BY a.item_maj_type DESC, a.pay_run_priority, a.item_code_name";
            if (itmSQL == "")
            {
                strSql = mnlSQL;
            }

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            //Global.mnFrm.prsnitm_SQL1 = strSql;
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

        public static void updateMsPyStatus(long mspyid, string run_cmpltd, string to_gl_intfc)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE pay.pay_mass_pay_run_hdr " +
            "SET run_status='" + run_cmpltd.Replace("'", "''") +
            "', sent_to_gl='" + to_gl_intfc.Replace("'", "''") +
            "', last_update_by=" + Global.myInv.user_id +
            ", last_update_date='" + dateStr +
            "' WHERE mass_pay_id = " + mspyid;
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static DataSet getPayItemDet(long pyItmID)
        {
            string selSQL = @"Select item_id, item_code_name, item_maj_type, item_min_type 
From org.org_pay_items Where item_id=" + pyItmID.ToString();
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
            return dtst;
        }

        public static long getIvcDetID(long invcID, long prsnID, long itmID)
        {
            string selSQL = @"Select invc_det_ln_id  
      From scm.scm_sales_invc_det Where invc_hdr_id=" + invcID.ToString() +
            " and lnkd_person_id = " + prsnID.ToString() + " and itm_id=" + itmID.ToString();
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

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
                  "', " + Global.myInv.user_id + ", '" + dateStr +
                  "', " + Global.myInv.user_id + ", '" + dateStr +
                  "', '0', '" + trnsdte.Replace("'", "''") + "', " +
                  prstid + ", " + itmstid + ", " + orgid + ", '0', '" + glDate +
                  "')";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        #endregion
        #region "PAYMENTS..."
        public static void updtPyblsDocAmnt(long docid, double invAmnt)
        {
            string extrCls = ", invoice_amount=" + invAmnt + "";

            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE accb.accb_pybls_invc_hdr SET " +
                  "last_update_by=" + Global.myInv.user_id +
                  ", last_update_date='" + dateStr +
                  "'" + extrCls + " WHERE (pybls_invc_hdr_id = " +
                  docid + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static double getRcptCost(string parRecNo)
        {
            string selSQL = @"select COALESCE(SUM(c.quantity_rcvd *c.cost_price),0) " +
                    " from inv.inv_consgmt_rcpt_det c where c.rcpt_id = " + long.Parse(parRecNo) + " order by 1";

            DataSet Ds = Global.fillDataSetFxn(selSQL);
            if (Ds.Tables[0].Rows.Count <= 0)
            {
                return 0.00;
            }
            else
            {
                return double.Parse(Ds.Tables[0].Rows[0][0].ToString());
            }
        }

        //public static double getRcptRtrnCost(string parRecNo)
        //{
        //  string selSQL = @"select COALESCE(SUM(c.quantity_rcvd *c.cost_price),0) " +
        //          " from inv.inv_consgmt_rcpt_det c where c.rcpt_id = " + long.Parse(parRecNo) + " order by 1";

        //  DataSet Ds = Global.fillDataSetFxn(selSQL);
        //  if (Ds.Tables[0].Rows.Count <= 0)
        //  {
        //    return 0.00;
        //  }
        //  else
        //  {
        //    return double.Parse(Ds.Tables[0].Rows[0][0].ToString());
        //  }
        //}

        public static double getTtlPaymnt(string parRcptNo, int dfltAcntPyblID)
        {
            //abs(
            string qryGetTtlPaymnts = "SELECT coalesce(sum(net_amount),0) FROM scm.scm_gl_interface where src_doc_id = " + long.Parse(parRcptNo) +
                  " and (transaction_desc ilike '%Payment%' or transaction_desc ilike '%refund%') and accnt_id = " + dfltAcntPyblID + " order by 1 ";

            DataSet Ds = new DataSet();
            Ds.Reset();
            Ds = Global.fillDataSetFxn(qryGetTtlPaymnts);
            if (double.Parse(Ds.Tables[0].Rows[0][0].ToString()) == 0)
            {
                return 0.00;
            }
            else
            {
                if (double.Parse(Ds.Tables[0].Rows[0][0].ToString()) < 0)
                {
                    return -1 * double.Parse(Ds.Tables[0].Rows[0][0].ToString());
                }
                else
                {
                    return double.Parse(Ds.Tables[0].Rows[0][0].ToString());
                }
            }
        }

        public static long getNewPymntBatchID()
        {
            //string strSql = "select nextval('accb.accb_trnsctn_batches_batch_id_seq'::regclass);";
            string strSql = "select  last_value from accb.accb_payments_batches_pymnt_batch_id_seq";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString()) + 1;
            }
            return -1;
        }

        public static long getNewPymntLnID()
        {
            //string strSql = "select nextval('accb.accb_trnsctn_batches_batch_id_seq'::regclass);";
            string strSql = "select nextval('accb.accb_payments_pymnt_id_seq')";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public static bool isPymntRvrsdB4(long orgnlPymntID)
        {
            string strSql = "";
            strSql = "SELECT a.pymnt_id FROM accb.accb_payments a " +
             "WHERE(a.orgnl_pymnt_id = " + orgnlPymntID + ") " +
             "ORDER BY a.pymnt_id LIMIT 1 " +
               " OFFSET 0";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public static void createPymntsBatch(int orgid, string strtDte,
          string endDte, string docType,
        string batchName, string batchDesc, int spplrID, int pymntMthdID,
          string batchSource, long orgnlBtchID,
          string vldtyStatus, string docTmpltClsftn, string batchStatus)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            strtDte = DateTime.ParseExact(strtDte, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            endDte = DateTime.ParseExact(endDte, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            string insSQL = @"INSERT INTO accb.accb_payments_batches(
            pymnt_batch_name, pymnt_batch_desc, pymnt_mthd_id, 
            doc_type, doc_clsfctn, docs_start_date, docs_end_date, batch_status, 
            batch_source, created_by, creation_date, last_update_by, last_update_date, 
            batch_vldty_status, orgnl_batch_id, org_id, cust_spplr_id) " +
                  "VALUES ('" + batchName.Replace("'", "''") +
                  "', '" + batchDesc.Replace("'", "''") +
                  "', " + pymntMthdID +
                  ", '" + docType.Replace("'", "''") +
                  "', '" + docTmpltClsftn.Replace("'", "''") +
                  "', '" + strtDte.Replace("'", "''") +
                  "', '" + endDte.Replace("'", "''") +
                  "', '" + batchStatus.Replace("'", "''") +
                  "', '" + batchSource.Replace("'", "''") +
                  "', " + Global.myInv.user_id + ", '" + dateStr +
                  "', " + Global.myInv.user_id + ", '" + dateStr +
                  "', '" + vldtyStatus.Replace("'", "''") +
                  "', " + orgnlBtchID +
                  ", " + orgid + ", " + spplrID +
                  ")";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void updateBatchVldtyStatus(long batchid, string vldty)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE accb.accb_trnsctn_batches " +
            "SET batch_vldty_status='" + vldty.Replace("'", "''") +
            "', last_update_by=" + Global.myInv.user_id +
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
            "', last_update_by=" + Global.myInv.user_id +
            ", last_update_date='" + dateStr +
            "' WHERE batch_id = " + batchid;
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updtPymntsBatchVldty(long batchID, string vldtyStatus)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = @"UPDATE accb.accb_payments_batches SET 
            last_update_by=" + Global.myInv.user_id +
                  ", last_update_date='" + dateStr +
                  "', batch_vldty_status='" + vldtyStatus.Replace("'", "''") +
                  "' WHERE pymnt_batch_id = " + batchID;
            Global.mnFrm.cmCde.updateDataNoParams(insSQL);
        }

        public static void updtPymntsLnVldty(long pymtLnID, string vldtyStatus)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = @"UPDATE accb.accb_payments SET 
            last_update_by=" + Global.myInv.user_id +
                  ", last_update_date='" + dateStr +
                  "', pymnt_vldty_status='" + vldtyStatus.Replace("'", "''") +
                  "' WHERE pymnt_id = " + pymtLnID;
            Global.mnFrm.cmCde.updateDataNoParams(insSQL);
        }

        public static void updtPymntsBatch(long batchID, string strtDte,
          string endDte, string docType,
        string batchName, string batchDesc, int spplrID, int pymntMthdID,
          string batchSource, long orgnlBtchID,
          string vldtyStatus, string docTmpltClsftn, string batchStatus)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            strtDte = DateTime.ParseExact(strtDte, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            endDte = DateTime.ParseExact(endDte, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            string insSQL = @"UPDATE accb.accb_payments_batches SET 
            pymnt_batch_name='" + batchName.Replace("'", "''") +
                  "', pymnt_batch_desc='" + batchDesc.Replace("'", "''") +
                  "', pymnt_mthd_id=" + pymntMthdID +
                  ", doc_type='" + docType.Replace("'", "''") +
                  "', doc_clsfctn='" + docTmpltClsftn.Replace("'", "''") +
                  "', docs_start_date='" + strtDte.Replace("'", "''") +
                  "', docs_end_date='" + endDte.Replace("'", "''") +
                  "', batch_status='" + batchStatus.Replace("'", "''") +
                  "', batch_source='" + batchSource.Replace("'", "''") +
                  "', last_update_by=" + Global.myInv.user_id +
                  ", last_update_date='" + dateStr +
                  "', batch_vldty_status='" + vldtyStatus.Replace("'", "''") +
                  "', orgnl_batch_id=" + orgnlBtchID +
                  ", cust_spplr_id=" + spplrID +
                  " WHERE pymnt_batch_id = " + batchID;
            Global.mnFrm.cmCde.updateDataNoParams(insSQL);
        }

        public static void createPymntDet(long pymntID, long pymntBatchID, int pymntMthdID,
          double amntPaid, int entrdCurrID, double chnge_bals, string pymntRemark,
          string srcDocType, long srcDocID, string pymntDte,
          string incrDcrs1, int blncgAccntID, string incrDcrs2, int chrgAccntID,
          long glBatchID, string vldyStatus, long orgnlLnID,
          int funcCurrID, int accntCurrID, double funcCurrRate, double accntCurrRate,
          double funcCurrAmnt, double accntCurrAmnt)
        {
            pymntDte = DateTime.ParseExact(pymntDte, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = @"INSERT INTO accb.accb_payments(
            pymnt_id, pymnt_mthd_id, amount_paid, change_or_balance, pymnt_remark, 
            src_doc_typ, src_doc_id, created_by, creation_date, last_update_by, 
            last_update_date, pymnt_date, incrs_dcrs1, rcvbl_lblty_accnt_id, 
            incrs_dcrs2, cash_or_suspns_acnt_id, gl_batch_id, orgnl_pymnt_id, 
            pymnt_vldty_status, entrd_curr_id, func_curr_id, accnt_curr_id, 
            func_curr_rate, accnt_curr_rate, func_curr_amount, accnt_curr_amnt, 
            pymnt_batch_id) " +
                  "VALUES (" + pymntID + ", " + pymntMthdID + "," + amntPaid + "," + chnge_bals +
                  ",'" + pymntRemark.Replace("'", "''") +
                  "', '" + srcDocType.Replace("'", "''") +
                  "', " + srcDocID +
                  ", " + Global.myInv.user_id + ", '" + dateStr +
                  "', " + Global.myInv.user_id + ", '" + dateStr +
                  "','" + pymntDte.Replace("'", "''") +
                  "', '" + incrDcrs1.Replace("'", "''") +
                  "', " + blncgAccntID +
                  ", '" + incrDcrs2.Replace("'", "''") +
                  "', " + chrgAccntID +
                  ", " + glBatchID +
                  ", " + orgnlLnID +
                  ", '" + vldyStatus.Replace("'", "''") +
                  "', " + entrdCurrID +
                  ", " + funcCurrID +
                  ", " + accntCurrID +
                  ", " + funcCurrRate +
                  ", " + accntCurrRate +
                  ", " + funcCurrAmnt +
                  ", " + accntCurrAmnt +
                  ", " + pymntBatchID +
                  ")";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void updtPymntDet(long pymntID, long pymntBatchID, int pymntMthdID,
          double amntPaid, int entrdCurrID, double chnge_bals, string pymntRemark,
          string srcDocType, long srcDocID, string pymntDte,
          string incrDcrs1, int blncgAccntID, string incrDcrs2, int chrgAccntID,
          long glBatchID, string vldyStatus, long orgnlLnID,
          int funcCurrID, int accntCurrID, double funcCurrRate, double accntCurrRate,
          double funcCurrAmnt, double accntCurrAmnt)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            pymntDte = DateTime.ParseExact(pymntDte, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = @"UPDATE accb.accb_payments SET 
            pymnt_mthd_id=" + pymntMthdID + ", amount_paid=" + amntPaid +
                  ", change_or_balance=" + chnge_bals +
                  ", pymnt_remark='" + pymntRemark.Replace("'", "''") +
                  "', src_doc_typ='" + srcDocType.Replace("'", "''") +
                  "', src_doc_id=" + srcDocID +
                  ", last_update_by=" + Global.myInv.user_id +
                  ", last_update_date='" + dateStr +
                  "', pymnt_date='" + pymntDte.Replace("'", "''") +
                  "', incrs_dcrs1='" + incrDcrs1.Replace("'", "''") +
                  "', rcvbl_lblty_accnt_id=" + blncgAccntID +
                  ", incrs_dcrs2='" + incrDcrs2.Replace("'", "''") +
                  "', cash_or_suspns_acnt_id=" + chrgAccntID +
                  ", gl_batch_id=" + glBatchID +
                  ", orgnl_pymnt_id=" + orgnlLnID +
                  ", pymnt_vldty_status='" + vldyStatus.Replace("'", "''") +
                  "', entrd_curr_id=" + entrdCurrID +
                  ", func_curr_id=" + funcCurrID +
                  ", accnt_curr_id=" + accntCurrID +
                  ", func_curr_rate=" + funcCurrRate +
                  ", accnt_curr_rate=" + accntCurrRate +
                  ", func_curr_amount=" + funcCurrAmnt +
                  ", accnt_curr_amnt=" + accntCurrAmnt +
                  ", pymnt_batch_id=" + pymntBatchID +
                  " WHERE pymnt_id = " + pymntID;
            Global.mnFrm.cmCde.updateDataNoParams(insSQL);
        }

        public static void deletePymntsBatchNDet(long valLnid, string batchName)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Batch Name = " + batchName;
            string delSQL = "DELETE FROM accb.accb_payments WHERE pymnt_batch_id = " + valLnid;
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
            delSQL = "DELETE FROM accb.accb_payments_batches WHERE pymnt_batch_id = " + valLnid;
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
        }

        public static void deletePymntsDet(long valLnid)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string delSQL = "DELETE FROM accb.accb_payments WHERE pymnt_id = " + valLnid;
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
        }

        public static DataSet get_One_PymntBatchHdr(long hdrID)
        {
            string strSql = "";

            strSql = @"SELECT pymnt_batch_id, pymnt_batch_name, pymnt_batch_desc, 
      pymnt_mthd_id, accb.get_pymnt_mthd_name(a.pymnt_mthd_id), 
       doc_type, doc_clsfctn, to_char(to_timestamp(docs_start_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), 
to_char(to_timestamp(docs_end_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), batch_status, 
       batch_source, cust_spplr_id, scm.get_cstmr_splr_name(cust_spplr_id),
       batch_vldty_status, orgnl_batch_id, org_id
      FROM accb.accb_payments_batches a " +
              "WHERE((a.pymnt_batch_id = " + hdrID + "))";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            //Global.pyblsFrm.docTmplt_SQL = strSql;
            return dtst;
        }

        public static DataSet get_PymntBatch(string searchWord, string searchIn, long offset,
          int limit_size, long orgID, string startDte, string endDte)
        {
            startDte = DateTime.ParseExact(startDte, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            endDte = DateTime.ParseExact(endDte, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            string strSql = "";
            string whrcls = "";
            string dteCls = @" and (a.pymnt_batch_id IN (select f.pymnt_batch_id from accb.accb_payments f where 
to_timestamp(f.pymnt_date,'YYYY-MM-DD HH24:MI:SS') between to_timestamp('" + startDte + @"','YYYY-MM-DD HH24:MI:SS') 
and to_timestamp('" + endDte + "','YYYY-MM-DD HH24:MI:SS')))";
            /*Batch Name
         Batch Description
         Payment Method
         Document Type
         Document Classification
         Supplier Name
         Batch Source
         Batch Status*/
            if (searchIn == "Batch Name")
            {
                whrcls = " and (a.pymnt_batch_name ilike '" + searchWord.Replace("'", "''") + "')";
            }
            else if (searchIn == "Batch Description")
            {
                whrcls = " and (a.pymnt_batch_desc ilike '" + searchWord.Replace("'", "''") + "')";
            }
            else if (searchIn == "Document Classification")
            {
                whrcls = " and (a.doc_clsfctn ilike '" + searchWord.Replace("'", "''") + "')";
            }
            else if (searchIn == "Supplier Name")
            {
                whrcls = @" and (a.supplier_id IN (select c.cust_sup_id from 
scm.scm_cstmr_suplr c where c.cust_sup_name ilike '" + searchWord.Replace("'", "''") +
            "'))";
            }
            else if (searchIn == "Payment Method")
            {
                whrcls = " and (accb.get_pymnt_mthd_name(a.pymnt_mthd_id) ilike '" + searchWord.Replace("'", "''") + "')";
            }
            else if (searchIn == "Source Doc Number")
            {
                whrcls = @" and (a.pymnt_batch_id IN (select y.pymnt_batch_id from accb.accb_payments y where accb.get_src_doc_num(y.src_doc_id,y.src_doc_typ) ilike '" + searchWord.Replace("'", "''") +
            "'))";
            }
            else if (searchIn == "Document Type")
            {
                whrcls = " and (a.doc_type ilike '" + searchWord.Replace("'", "''") + "')";
            }
            else if (searchIn == "Batch Source")
            {
                whrcls = " and a.batch_source ilike '" + searchWord.Replace("'", "''") + "')";
            }
            else if (searchIn == "Batch Status")
            {
                whrcls = " and a.batch_status ilike '" + searchWord.Replace("'", "''") + "')";
            }
            strSql = @"SELECT pymnt_batch_id, pymnt_batch_name, pymnt_batch_desc 
        FROM accb.accb_payments_batches a 
        WHERE((a.org_id = " + orgID + ")" + whrcls + dteCls +
              ") ORDER BY pymnt_batch_id DESC LIMIT " + limit_size +
              " OFFSET " + (Math.Abs(offset * limit_size)).ToString();

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            //Global.pymntFrm.rec_SQL = strSql;
            return dtst;
        }

        public static long get_Total_PymntBatch(string searchWord, string searchIn, long orgID, string startDte, string endDte)
        {
            startDte = DateTime.ParseExact(startDte, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            endDte = DateTime.ParseExact(endDte, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            string strSql = "";
            string whrcls = "";
            string dteCls = @" and (a.pymnt_batch_id IN (select f.pymnt_batch_id from accb.accb_payments f where 
to_timestamp(f.pymnt_date,'YYYY-MM-DD HH24:MI:SS') between to_timestamp('" + startDte + @"','YYYY-MM-DD HH24:MI:SS') 
and to_timestamp('" + endDte + "','YYYY-MM-DD HH24:MI:SS')))";
            /*Batch Name
         Batch Description
         Payment Method
         Document Type
         Document Classification
         Supplier Name
         Batch Source
         Batch Status*/
            if (searchIn == "Batch Name")
            {
                whrcls = " and (a.pymnt_batch_name ilike '" + searchWord.Replace("'", "''") + "')";
            }
            else if (searchIn == "Batch Description")
            {
                whrcls = " and (a.pymnt_batch_desc ilike '" + searchWord.Replace("'", "''") + "')";
            }
            else if (searchIn == "Document Classification")
            {
                whrcls = " and (a.doc_clsfctn ilike '" + searchWord.Replace("'", "''") + "')";
            }
            else if (searchIn == "Supplier Name")
            {
                whrcls = @" and (a.supplier_id IN (select c.cust_sup_id from 
scm.scm_cstmr_suplr c where c.cust_sup_name ilike '" + searchWord.Replace("'", "''") +
            "'))";
            }
            else if (searchIn == "Payment Method")
            {
                whrcls = " and (accb.get_pymnt_mthd_name(a.pymnt_mthd_id) ilike '" + searchWord.Replace("'", "''") + "')";
            }
            else if (searchIn == "Source Doc Number")
            {
                whrcls = @" and (a.pymnt_batch_id IN (select y.pymnt_batch_id from accb.accb_payments y where accb.get_src_doc_num(y.src_doc_id,y.src_doc_typ) ilike '" + searchWord.Replace("'", "''") +
            "'))";
            }
            else if (searchIn == "Document Type")
            {
                whrcls = " and (a.doc_type ilike '" + searchWord.Replace("'", "''") + "')";
            }
            else if (searchIn == "Batch Source")
            {
                whrcls = " and a.batch_source ilike '" + searchWord.Replace("'", "''") + "')";
            }
            else if (searchIn == "Batch Status")
            {
                whrcls = " and a.batch_status ilike '" + searchWord.Replace("'", "''") + "')";
            }
            strSql = @"SELECT count(1) 
        FROM accb.accb_payments_batches a 
        WHERE((a.org_id = " + orgID + ")" + whrcls + dteCls +
              ")";

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

        public static DataSet get_PymntBatchLns(long offset,
          int limit_size, long docHdrID)
        {
            string strSql = "";

            strSql = @"SELECT pymnt_id, pymnt_mthd_id, amount_paid, change_or_balance, pymnt_remark, 
       src_doc_typ, src_doc_id, accb.get_src_doc_num(a.src_doc_id, a.src_doc_typ), 
       to_char(to_timestamp(pymnt_date, 'YYYY-MM-DD HH24:MI:SS'), 'DD-Mon-YYYY HH24:MI:SS'), 
       incrs_dcrs1, rcvbl_lblty_accnt_id, 
       incrs_dcrs2, cash_or_suspns_acnt_id, 
       gl_batch_id, accb.get_gl_batch_name(gl_batch_id), 
       orgnl_pymnt_id, pymnt_vldty_status, 
       entrd_curr_id, gst.get_pssbl_val(a.entrd_curr_id), 
       func_curr_id, gst.get_pssbl_val(a.func_curr_id), 
       accnt_curr_id, gst.get_pssbl_val(a.accnt_curr_id), 
       func_curr_rate, accnt_curr_rate, func_curr_amount, accnt_curr_amnt, 
       pymnt_batch_id
       FROM accb.accb_payments a " +
              "WHERE((a.pymnt_batch_id = " + docHdrID + ")) ORDER BY pymnt_id ASC LIMIT " + limit_size +
              " OFFSET " + (Math.Abs(offset * limit_size)).ToString();

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            //Global.pymntFrm.recDt_SQL = strSql;
            // Global.mnFrm.cmCde.showSQLNoPermsn(strSql);
            return dtst;
        }

        public static void updtPymntBatchStatus(long docid,
      string batchStatus)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE accb.accb_payments_batches SET " +
                  "batch_status='" + batchStatus.Replace("'", "''") +
                  "', last_update_by=" + Global.myInv.user_id +
                  ", last_update_date='" + dateStr +
                  "' WHERE (pymnt_batch_id = " +
                  docid + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updtPymntLnGLBatch(long docid,
      long glBatchID)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE accb.accb_payments SET " +
                  "gl_batch_id=" + glBatchID +
                  ", last_update_by=" + Global.myInv.user_id +
                  ", last_update_date='" + dateStr +
                  "' WHERE (pymnt_id = " +
                  docid + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static DataSet getPymntMthds(int orgID, string docType)
        {
            string selSQL = @"select 
        distinct trim(to_char(paymnt_mthd_id,'999999999999999999999999999999')) a, 
        pymnt_mthd_name b, '' c, org_id d, supported_doc_type e 
        from accb.accb_paymnt_mthds 
        where is_enabled = '1' and org_id = " + orgID +
              " and supported_doc_type = '" + docType.Replace("'", "''") +
              "' order by pymnt_mthd_name LIMIT 30 OFFSET 0";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
            return dtst;
        }

        public static int getPymntMthdID(int orgID, string pyMthdNm)
        {
            string selSQL = @"select paymnt_mthd_id
        from accb.accb_paymnt_mthds 
        where org_id = " + orgID +
              " and pymnt_mthd_name = '" + pyMthdNm.Replace("'", "''") +
              "'";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public static int getPyblsDocBlncngAccnt(long srcDocID, string docType)
        {
            string whrcls = @" and (a.pybls_smmry_type !='6Grand Total' and 
a.pybls_smmry_type !='7Total Payments Made' and a.pybls_smmry_type !='8Outstanding Balance')";

            string selSQL = @"select 
        distinct liability_acnt_id, pybls_smmry_id 
        from accb.accb_pybls_amnt_smmrys a 
        where src_pybls_hdr_id = " + srcDocID +
              " and src_pybls_type = '" + docType.Replace("'", "''") +
              "'" + whrcls + " order by pybls_smmry_id LIMIT 1 OFFSET 0";
            //Global.mnFrm.cmCde.showSQLNoPermsn(selSQL);
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);

            if (dtst.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public static int getRcvblsDocBlncngAccnt(long srcDocID, string docType)
        {
            string whrcls = @" and (a.rcvbl_smmry_type !='6Grand Total' and 
a.rcvbl_smmry_type !='7Total Payments Made' and a.rcvbl_smmry_type !='8Outstanding Balance')";

            string selSQL = @"select 
        distinct rcvbl_acnt_id, rcvbl_smmry_id 
        from accb.accb_rcvbl_amnt_smmrys a 
        where src_rcvbl_hdr_id = " + srcDocID +
              " and src_rcvbl_type = '" + docType.Replace("'", "''") +
              "'" + whrcls + " order by rcvbl_smmry_id LIMIT 1 OFFSET 0";
            //Global.mnFrm.cmCde.showSQLNoPermsn(selSQL);
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);

            if (dtst.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public static int getPymntMthdChrgAccnt(long pymntMthdID)
        {
            string selSQL = @"select 
        distinct current_asst_acnt_id, paymnt_mthd_id 
        from accb.accb_paymnt_mthds 
        where paymnt_mthd_id = " + pymntMthdID +
              " order by paymnt_mthd_id LIMIT 1 OFFSET 0";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);

            if (dtst.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }
        public static void updtPyblsDocAmntPaid(long docid,
      double amntPaid)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE accb.accb_pybls_invc_hdr SET " +
                  "amnt_paid=amnt_paid + " + amntPaid +
                  ", last_update_by=" + Global.myInv.user_id +
                  ", last_update_date='" + dateStr +
                  "' WHERE (pybls_invc_hdr_id = " +
                  docid + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updtPyblsDocAmntAppld(long docid,
      double amntAppld)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE accb.accb_pybls_invc_hdr SET " +
                  "invc_amnt_appld_elswhr=invc_amnt_appld_elswhr + " + amntAppld +
                  ", last_update_by=" + Global.myInv.user_id +
                  ", last_update_date='" + dateStr +
                  "' WHERE (pybls_invc_hdr_id = " +
                  docid + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        #endregion

        #region "DEFAULT ACCOUNTS..."
        public static DataSet get_One_DfltAcnt(int orgID)
        {
            string strSql = "SELECT row_id, itm_inv_asst_acnt_id, cost_of_goods_acnt_id, expense_acnt_id, " +
                  "prchs_rtrns_acnt_id, rvnu_acnt_id, sales_rtrns_acnt_id, sales_cash_acnt_id, " +
                  "sales_check_acnt_id, sales_rcvbl_acnt_id, rcpt_cash_acnt_id, " +
                  "rcpt_lblty_acnt_id " +
             "FROM scm.scm_dflt_accnts a " +
             "WHERE(a.org_id = " + orgID + ")";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static double get_PyblPrepayDocAppldAmnt(long prepayDocID)
        {
            string strSql = "SELECT invc_amnt_appld_elswhr " +
              "FROM accb.accb_pybls_invc_hdr a " +
              "WHERE(a.pybls_invc_hdr_id = " + prepayDocID +
              " and (invc_amnt_appld_elswhr)>0)";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return double.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return 0;
        }

        public static int getPyblsPrepayDocCnt(long dochdrID)
        {
            string strSql = @"select count(appld_prepymnt_doc_id) " +
              "from accb.accb_pybls_amnt_smmrys y " +
              "where y.src_pybls_hdr_id = " + dochdrID + " and y.appld_prepymnt_doc_id >0 " +
              "Group by y.appld_prepymnt_doc_id having count(y.appld_prepymnt_doc_id)>1";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            int rs = 0;

            if (dtst.Tables[0].Rows.Count > 0)
            {
                int.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rs);
                return rs;
            }
            return 0;
        }

        public static int get_PyblPrepayDocAcntID(long prepayDocID)
        {
            string strSql = "SELECT asset_expns_acnt_id, pybls_smmry_id " +
              "FROM accb.accb_pybls_amnt_smmrys a " +
              "WHERE(a.src_pybls_hdr_id = " + prepayDocID +
              " and pybls_smmry_type = '1Initial Amount') ORDER BY pybls_smmry_id ASC LIMIT 1 OFFSET 0";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public static int get_DfltBadDbtAcnt(int orgID)
        {
            string strSql = "SELECT bad_debt_acnt_id " +
             "FROM scm.scm_dflt_accnts a " +
             "WHERE(a.org_id = " + orgID + ")";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public static int get_DfltRcvblAcnt(int orgID)
        {
            string strSql = "SELECT sales_rcvbl_acnt_id " +
             "FROM scm.scm_dflt_accnts a " +
             "WHERE(a.org_id = " + orgID + ")";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public static int get_DfltInvAcnt(int orgID)
        {
            string strSql = "SELECT itm_inv_asst_acnt_id " +
             "FROM scm.scm_dflt_accnts a " +
             "WHERE(a.org_id = " + orgID + ")";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public static int get_DfltCSGAcnt(int orgID)
        {
            string strSql = "SELECT cost_of_goods_acnt_id " +
             "FROM scm.scm_dflt_accnts a " +
             "WHERE(a.org_id = " + orgID + ")";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public static int get_DfltExpnsAcnt(int orgID)
        {
            string strSql = "SELECT expense_acnt_id " +
             "FROM scm.scm_dflt_accnts a " +
             "WHERE(a.org_id = " + orgID + ")";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public static int get_DfltRvnuAcnt(int orgID)
        {
            string strSql = "SELECT rvnu_acnt_id " +
             "FROM scm.scm_dflt_accnts a " +
             "WHERE(a.org_id = " + orgID + ")";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public static int get_DfltSRAcnt(int orgID)
        {
            string strSql = "SELECT sales_rtrns_acnt_id " +
             "FROM scm.scm_dflt_accnts a " +
             "WHERE(a.org_id = " + orgID + ")";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public static long get_InvoiceMsPyID(long invcID)
        {
            string strSql = "SELECT mass_pay_id " +
             "FROM pay.pay_itm_trnsctns a " +
             "WHERE(a.sales_invoice_id = " + invcID +
             " and a.sales_invoice_id>0 and a.pymnt_vldty_status='VALID' and a.src_py_trns_id<=0) ORDER BY mass_pay_id DESC LIMIT 1 OFFSET 0";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public static int get_DfltCashAcnt(int orgID)
        {
            string strSql = "SELECT sales_cash_acnt_id " +
             "FROM scm.scm_dflt_accnts a " +
             "WHERE(a.org_id = " + orgID + ")";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public static int get_DfltCheckAcnt(int orgID)
        {
            string strSql = "SELECT sales_check_acnt_id " +
             "FROM scm.scm_dflt_accnts a " +
             "WHERE(a.org_id = " + orgID + ")";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        ////public static int get_DfltCashAcnt(int orgID)
        //  {
        //      string strSql = "SELECT rcpt_cash_acnt_id " +
        //       "FROM scm.scm_dflt_accnts a " +
        //       "WHERE(a.org_id = " + orgID + ")";

        //      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
        //      if (dtst.Tables[0].Rows.Count > 0)
        //      {
        //          return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
        //      }
        //      return -1;
        //  }

        public static int get_DfltAdjstLbltyAcnt(int orgID)
        {
            string strSql = "SELECT inv_adjstmnts_lblty_acnt_id " +
             "FROM scm.scm_dflt_accnts a " +
             "WHERE(a.org_id = " + orgID + ")";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public static int get_DfltAccPyblAcnt(int orgID)
        {
            string strSql = "SELECT rcpt_lblty_acnt_id " +
             "FROM scm.scm_dflt_accnts a " +
             "WHERE(a.org_id = " + orgID + ")";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public static int get_DfltPurchRtrnAcnt(int orgID)
        {
            string strSql = "SELECT prchs_rtrns_acnt_id " +
             "FROM scm.scm_dflt_accnts a " +
             "WHERE(a.org_id = " + orgID + ")";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public static double get_PyblPrepayDocAvlblAmnt(long prepayDocID)
        {
            string strSql = "SELECT invoice_amount-invc_amnt_appld_elswhr " +
              "FROM accb.accb_pybls_invc_hdr a " +
              "WHERE(a.pybls_invc_hdr_id = " + prepayDocID +
              " and (invoice_amount-invc_amnt_appld_elswhr)>0)";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return double.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return 0;
        }

        public static DataSet get_DocTmpltsDet(long tmpltHdrID)
        {
            string strSql = "";

            strSql = @"SELECT doc_tmplt_det_id, line_item_type, line_description, 
incrs_dcrs, costing_accnt_id, auto_calc, code_behind_id
  FROM accb.accb_doc_tmplts_det a " +
              "WHERE((a.doc_tmplts_hdr_id = " + tmpltHdrID + ")) ORDER BY line_item_type ASC ";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static int get_DfltPyblAcnt(int orgID)
        {
            string strSql = "SELECT rcpt_lblty_acnt_id " +
             "FROM scm.scm_dflt_accnts a " +
             "WHERE(a.org_id = " + orgID + ")";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public static int get_PyblPrepayDocLbltyAcntID(long prepayDocID)
        {
            string strSql = "SELECT liability_acnt_id, pybls_smmry_id " +
              "FROM accb.accb_pybls_amnt_smmrys a " +
              "WHERE(a.src_pybls_hdr_id = " + prepayDocID +
              " and pybls_smmry_type = '1Initial Amount') ORDER BY pybls_smmry_id ASC LIMIT 1 OFFSET 0";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }
        #endregion

        #region "RECEIVABLES..."
        public static long getNewRcvblsLnID()
        {
            //string strSql = "select nextval('accb.accb_trnsctn_batches_batch_id_seq'::regclass);";
            string strSql = "select nextval('accb.accb_rcvbl_amnt_smmrys_rcvbl_smmry_id_seq')";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public static long getNewInvcLnID()
        {
            //string strSql = "select nextval('accb.accb_trnsctn_batches_batch_id_seq'::regclass);";
            string strSql = "select nextval('scm.scm_itm_sales_ordrs_det_trnstn_id_seq')";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public static string getLtstRcvblsIDNoInPrfx(string prfxTxt)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select count(rcvbls_invc_hdr_id) from accb.accb_rcvbls_invc_hdr WHERE org_id=" +
              Global.mnFrm.cmCde.Org_id + " and rcvbls_invc_number ilike '" + prfxTxt.Replace("'", "''") + "%'";
            dtSt = Global.mnFrm.cmCde.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return (long.Parse(dtSt.Tables[0].Rows[0][0].ToString()) + 1).ToString().PadLeft(4, '0');
            }
            else
            {
                return "0001";
            }
        }

        public static string getLtstInvcIDNoInPrfx(string prfxTxt)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select count(invc_hdr_id) from scm.scm_sales_invc_hdr WHERE org_id=" +
              Global.mnFrm.cmCde.Org_id + " and invc_number ilike '" + prfxTxt.Replace("'", "''") + "%'";
            dtSt = Global.mnFrm.cmCde.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return (long.Parse(dtSt.Tables[0].Rows[0][0].ToString()) + 1).ToString().PadLeft(4, '0');
            }
            else
            {
                return "0001";
            }
        }

        public static long getScmRcvblsSmmryItmID(string smmryType, long codeBhnd,
        long srcDocID, string srcDocTyp)
        {
            string strSql = "select y.rcvbl_smmry_id " +
              "from scm.scm_rcvbl_amnt_smmrys y " +
              "where y.rcvbl_smmry_type= '" + smmryType + "' and y.code_id_behind = " + codeBhnd +
              " and y.src_rcvbl_type='" + srcDocTyp +
              "' and y.src_rcvbl_hdr_id=" + srcDocID + " ";
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

        public static void createScmRcvblsDocDet(long hdrID, string lineType, string lineDesc,
          double entrdAmnt, int entrdCurrID, int codeBhnd, string docType,
          bool autoCalc, string incrDcrs1, int costngID, string incrDcrs2, int blncgAccntID,
          long prepayDocHdrID, string vldyStatus, long orgnlLnID,
          int funcCurrID, int accntCurrID, double funcCurrRate, double accntCurrRate,
          double funcCurrAmnt, double accntCurrAmnt)
        {

            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            //rcvbl_smmry_id, " + smmryID + ", 
            string insSQL = @"INSERT INTO scm.scm_rcvbl_amnt_smmrys(
            rcvbl_smmry_type, rcvbl_smmry_desc, rcvbl_smmry_amnt, 
            code_id_behind, src_rcvbl_type, src_rcvbl_hdr_id, created_by, 
            creation_date, last_update_by, last_update_date, auto_calc, incrs_dcrs1, 
            rvnu_acnt_id, incrs_dcrs2, rcvbl_acnt_id, appld_prepymnt_doc_id, 
            orgnl_line_id, validty_status, entrd_curr_id, func_curr_id, accnt_curr_id, 
            func_curr_rate, accnt_curr_rate, func_curr_amount, accnt_curr_amnt) " +
                  "VALUES ('" + lineType.Replace("'", "''") +
                  "', '" + lineDesc.Replace("'", "''") +
                  "', " + entrdAmnt +
                  ", " + codeBhnd +
                  ", '" + docType.Replace("'", "''") +
                  "', " + hdrID +
                  ", " + Global.myInv.user_id + ", '" + dateStr +
                  "', " + Global.myInv.user_id + ", '" + dateStr +
                  "', '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(autoCalc) +
                  "', '" + incrDcrs1.Replace("'", "''") +
                  "', " + costngID +
                  ", '" + incrDcrs2.Replace("'", "''") +
                  "', " + blncgAccntID +
                  ", " + prepayDocHdrID +
                  ", " + orgnlLnID +
                  ", '" + vldyStatus.Replace("'", "''") +
                  "', " + entrdCurrID +
                  ", " + funcCurrID +
                  ", " + accntCurrID +
                  ", " + funcCurrRate +
                  ", " + accntCurrRate +
                  ", " + funcCurrAmnt +
                  ", " + accntCurrAmnt +
                  ")";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void updtScmRcvblsDocDet(long docDetID, long hdrID, string lineType, string lineDesc,
          double entrdAmnt, int entrdCurrID, int codeBhnd, string docType,
          bool autoCalc, string incrDcrs1, int costngID, string incrDcrs2, int blncgAccntID,
          long prepayDocHdrID, string vldyStatus, long orgnlLnID,
          int funcCurrID, int accntCurrID, double funcCurrRate, double accntCurrRate,
          double funcCurrAmnt, double accntCurrAmnt)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = @"UPDATE scm.scm_rcvbl_amnt_smmrys
   SET rcvbl_smmry_type='" + lineType.Replace("'", "''") +
                  "', rcvbl_smmry_desc='" + lineDesc.Replace("'", "''") +
                  "', rcvbl_smmry_amnt=" + entrdAmnt +
                  ", code_id_behind=" + codeBhnd +
                  ", src_rcvbl_type='" + docType.Replace("'", "''") +
                  "', src_rcvbl_hdr_id=" + hdrID +
                  ", last_update_by=" + Global.myInv.user_id +
                  ", last_update_date='" + dateStr +
                  "', auto_calc='" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(autoCalc) +
                  "', incrs_dcrs1='" + incrDcrs1.Replace("'", "''") +
                  "', rvnu_acnt_id=" + costngID +
                  ", incrs_dcrs2='" + incrDcrs2.Replace("'", "''") +
                  "', rcvbl_acnt_id=" + blncgAccntID +
                  ", appld_prepymnt_doc_id=" + prepayDocHdrID +
                  ", validty_status='" + vldyStatus.Replace("'", "''") +
                  "', orgnl_line_id=" + orgnlLnID +
                  ", entrd_curr_id=" + entrdCurrID +
                  ", func_curr_id=" + funcCurrID +
                  ", accnt_curr_id=" + accntCurrID +
                  ", func_curr_rate=" + funcCurrRate +
                  ", accnt_curr_rate=" + accntCurrRate +
                  ", func_curr_amount=" + funcCurrAmnt +
                  ", accnt_curr_amnt=" + accntCurrAmnt +
                  " WHERE rcvbl_smmry_id = " + docDetID;
            Global.mnFrm.cmCde.updateDataNoParams(insSQL);
        }

        public static void roundScmRcvblsDocAmnts(long hdrID, string docType)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = @"UPDATE scm.scm_rcvbl_amnt_smmrys
   SET rcvbl_smmry_amnt = ROUND(rcvbl_smmry_amnt, 2), func_curr_amount=ROUND(func_curr_amount,2), accnt_curr_amnt=ROUND(func_curr_amount,2) " +
                  " WHERE src_rcvbl_hdr_id = " + hdrID + " and src_rcvbl_type='" + docType.Replace("'", "''") + "'";
            Global.mnFrm.cmCde.updateDataNoParams(insSQL);
        }

        public static void deleteScmRcvblsDocDets(long valLnid, int cdeBhnd)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";

            string delSQL = "DELETE FROM scm.scm_rcvbl_amnt_smmrys WHERE src_rcvbl_hdr_id = " + valLnid +
              " and code_id_behind = " + cdeBhnd;

            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
        }

        public static void deleteScmRcvblsDocDets(long valLnid, string docNum)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Document Number = " + docNum;
            string delSQL = "DELETE FROM scm.scm_rcvbl_amnt_smmrys WHERE src_rcvbl_hdr_id = " + valLnid;
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
        }

        public static void createRcvblsDocHdr(int orgid, string docDte, string docNum,
        string docType, string docDesc, long srcDocHdrID, int cstmrID, int cstmrSiteID,
          string apprvlStatus, string nxtApprvlActn, double invcAmnt, string pymntTrms,
          string srcDocType, int pymntMthdID, double amntPaid, long glBtchID,
          string cstmrDocNum, string docTmpltClsftn, int currID, double amntAppld, int blcngAccntID)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            docDte = DateTime.ParseExact(docDte, "dd-MMM-yyyy",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            string insSQL = @"INSERT INTO accb.accb_rcvbls_invc_hdr(
            rcvbls_invc_date, created_by, creation_date, 
            last_update_by, last_update_date, rcvbls_invc_number, rcvbls_invc_type, 
            comments_desc, src_doc_hdr_id, customer_id, customer_site_id, 
            approval_status, next_aproval_action, org_id, invoice_amount, 
            payment_terms, src_doc_type, pymny_method_id, amnt_paid, gl_batch_id, 
            cstmrs_doc_num, doc_tmplt_clsfctn, invc_curr_id, invc_amnt_appld_elswhr, balancing_accnt_id) " +
                  "VALUES ('" + docDte.Replace("'", "''") +
                  "', " + Global.myInv.user_id + ", '" + dateStr +
                  "', " + Global.myInv.user_id + ", '" + dateStr +
                  "', '" + docNum.Replace("'", "''") +
                  "', '" + docType.Replace("'", "''") +
                  "', '" + docDesc.Replace("'", "''") +
                  "', " + srcDocHdrID +
                  ", " + cstmrID +
                  ", " + cstmrSiteID +
                  ", '" + apprvlStatus.Replace("'", "''") +
                  "', '" + nxtApprvlActn.Replace("'", "''") +
                  "', " + orgid +
                  ", " + invcAmnt +
                  ", '" + pymntTrms.Replace("'", "''") +
                  "', '" + srcDocType.Replace("'", "''") +
                  "', " + pymntMthdID +
                  ", " + amntPaid +
                  ", " + glBtchID +
                  ", '" + cstmrDocNum.Replace("'", "''") +
                  "', '" + docTmpltClsftn.Replace("'", "''") +
                  "', " + currID + ", " + amntAppld + ", " + blcngAccntID + ")";
            //Global.mnFrm.cmCde.showSQLNoPermsn(insSQL);
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void updtRcvblsDocHdr(long hdrID, string docDte, string docNum,
        string docType, string docDesc, long srcDocHdrID, int spplrID, int spplrSiteID,
          string apprvlStatus, string nxtApprvlActn, double invcAmnt, string pymntTrms,
          string srcDocType, int pymntMthdID, double amntPaid, long glBtchID,
          string spplrInvcNum, string docTmpltClsftn, int currID, double amntAppld, int blcngAccntID)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            docDte = DateTime.ParseExact(docDte, "dd-MMM-yyyy",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = @"UPDATE accb.accb_rcvbls_invc_hdr
       SET rcvbls_invc_date='" + docDte.Replace("'", "''") +
                  "', last_update_by=" + Global.myInv.user_id +
                  ", last_update_date='" + dateStr +
                  "', rcvbls_invc_number='" + docNum.Replace("'", "''") +
                  "', rcvbls_invc_type='" + docType.Replace("'", "''") +
                  "', comments_desc='" + docDesc.Replace("'", "''") +
                  "', src_doc_hdr_id=" + srcDocHdrID +
                  ", customer_id=" + spplrID +
                  ", customer_site_id=" + spplrSiteID +
                  ", approval_status='" + apprvlStatus.Replace("'", "''") +
                  "', next_aproval_action='" + nxtApprvlActn.Replace("'", "''") +
                  "', invoice_amount=" + invcAmnt +
                  ", payment_terms='" + pymntTrms.Replace("'", "''") +
                  "', src_doc_type='" + srcDocType.Replace("'", "''") +
                  "', pymny_method_id=" + pymntMthdID +
                  ", amnt_paid=" + amntPaid +
                  ", gl_batch_id=" + glBtchID +
                  ", cstmrs_doc_num='" + spplrInvcNum.Replace("'", "''") +
                  "', doc_tmplt_clsfctn='" + docTmpltClsftn.Replace("'", "''") +
                  "', invc_curr_id=" + currID +
                  ", balancing_accnt_id=" + blcngAccntID +
                  " WHERE rcvbls_invc_hdr_id = " + hdrID;
            //Global.mnFrm.cmCde.showSQLNoPermsn(insSQL);
            Global.mnFrm.cmCde.updateDataNoParams(insSQL);
        }

        public static void createRcvblsDocDet(long smmryID, long hdrID, string lineType, string lineDesc,
          double entrdAmnt, int entrdCurrID, int codeBhnd, string docType,
          bool autoCalc, string incrDcrs1, int costngID, string incrDcrs2, int blncgAccntID,
          long prepayDocHdrID, string vldyStatus, long orgnlLnID,
          int funcCurrID, int accntCurrID, double funcCurrRate, double accntCurrRate,
          double funcCurrAmnt, double accntCurrAmnt)
        {

            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = @"INSERT INTO accb.accb_rcvbl_amnt_smmrys(
            rcvbl_smmry_id, rcvbl_smmry_type, rcvbl_smmry_desc, rcvbl_smmry_amnt, 
            code_id_behind, src_rcvbl_type, src_rcvbl_hdr_id, created_by, 
            creation_date, last_update_by, last_update_date, auto_calc, incrs_dcrs1, 
            rvnu_acnt_id, incrs_dcrs2, rcvbl_acnt_id, appld_prepymnt_doc_id, 
            orgnl_line_id, validty_status, entrd_curr_id, func_curr_id, accnt_curr_id, 
            func_curr_rate, accnt_curr_rate, func_curr_amount, accnt_curr_amnt) " +
                  "VALUES (" + smmryID + ", '" + lineType.Replace("'", "''") +
                  "', '" + lineDesc.Replace("'", "''") +
                  "', " + entrdAmnt +
                  ", " + codeBhnd +
                  ", '" + docType.Replace("'", "''") +
                  "', " + hdrID +
                  ", " + Global.myInv.user_id + ", '" + dateStr +
                  "', " + Global.myInv.user_id + ", '" + dateStr +
                  "', '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(autoCalc) +
                  "', '" + incrDcrs1.Replace("'", "''") +
                  "', " + costngID +
                  ", '" + incrDcrs2.Replace("'", "''") +
                  "', " + blncgAccntID +
                  ", " + prepayDocHdrID +
                  ", " + orgnlLnID +
                  ", '" + vldyStatus.Replace("'", "''") +
                  "', " + entrdCurrID +
                  ", " + funcCurrID +
                  ", " + accntCurrID +
                  ", " + funcCurrRate +
                  ", " + accntCurrRate +
                  ", " + funcCurrAmnt +
                  ", " + accntCurrAmnt +
                  ")";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void updtRcvblsDocDet(long docDetID, long hdrID, string lineType, string lineDesc,
          double entrdAmnt, int entrdCurrID, int codeBhnd, string docType,
          bool autoCalc, string incrDcrs1, int costngID, string incrDcrs2, int blncgAccntID,
          long prepayDocHdrID, string vldyStatus, long orgnlLnID,
          int funcCurrID, int accntCurrID, double funcCurrRate, double accntCurrRate,
          double funcCurrAmnt, double accntCurrAmnt)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = @"UPDATE accb.accb_rcvbl_amnt_smmrys
   SET rcvbl_smmry_type='" + lineType.Replace("'", "''") +
                  "', rcvbl_smmry_desc='" + lineDesc.Replace("'", "''") +
                  "', rcvbl_smmry_amnt=" + entrdAmnt +
                  ", code_id_behind=" + codeBhnd +
                  ", src_rcvbl_type='" + docType.Replace("'", "''") +
                  "', src_rcvbl_hdr_id=" + hdrID +
                  ", last_update_by=" + Global.myInv.user_id +
                  ", last_update_date='" + dateStr +
                  "', auto_calc='" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(autoCalc) +
                  "', incrs_dcrs1='" + incrDcrs1.Replace("'", "''") +
                  "', rvnu_acnt_id=" + costngID +
                  ", incrs_dcrs2='" + incrDcrs2.Replace("'", "''") +
                  "', rcvbl_acnt_id=" + blncgAccntID +
                  ", appld_prepymnt_doc_id=" + prepayDocHdrID +
                  ", validty_status='" + vldyStatus.Replace("'", "''") +
                  "', orgnl_line_id=" + orgnlLnID +
                  ", entrd_curr_id=" + entrdCurrID +
                  ", func_curr_id=" + funcCurrID +
                  ", accnt_curr_id=" + accntCurrID +
                  ", func_curr_rate=" + funcCurrRate +
                  ", accnt_curr_rate=" + accntCurrRate +
                  ", func_curr_amount=" + funcCurrAmnt +
                  ", accnt_curr_amnt=" + accntCurrAmnt +
                  " WHERE rcvbl_smmry_id = " + docDetID;
            Global.mnFrm.cmCde.updateDataNoParams(insSQL);
        }

        public static void deleteScmRcvblsDocDet(long valLnid)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string delSQL = "DELETE FROM scm.scm_rcvbl_amnt_smmrys WHERE src_rcvbl_hdr_id = " + valLnid;
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
        }

        public static void deleteRcvblsDocHdrNDet(long valLnid, string docNum)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Document Number = " + docNum;
            string delSQL = "DELETE FROM accb.accb_rcvbl_amnt_smmrys WHERE src_rcvbl_hdr_id = " + valLnid;
            //Global.mnFrm.cmCde.showSQLNoPermsn(delSQL);
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
            delSQL = "DELETE FROM accb.accb_rcvbls_invc_hdr WHERE rcvbls_invc_hdr_id = " + valLnid;
            //Global.mnFrm.cmCde.showSQLNoPermsn(delSQL);
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
        }

        public static string getRcvblsDocLastUpdate(long dochdrID, string docType)
        {
            string strSql = "select to_char(to_timestamp(MAX(y.last_update_date),'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') dte " +
              "from accb.accb_payments y " +
              "where y.src_doc_id = " + dochdrID + " and y.src_doc_typ = '" + docType.Replace("'", "''") + "'";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);

            if (dtst.Tables[0].Rows.Count > 0)
            {
                return dtst.Tables[0].Rows[0][0].ToString();
            }
            return Global.mnFrm.cmCde.getFrmtdDB_Date_time();
        }

        public static void deleteRcvblsDocDetails(long valLnid, string docNum)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Document Number = " + docNum;
            string delSQL = "DELETE FROM accb.accb_rcvbl_amnt_smmrys WHERE src_rcvbl_hdr_id = " + valLnid;
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
        }
        public static void deleteRcvblsDocDet(long valLnid)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string delSQL = "DELETE FROM accb.accb_rcvbl_amnt_smmrys WHERE rcvbl_smmry_id = " + valLnid;
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
        }

        public static DataSet get_One_RcvblsDocHdr(long hdrID)
        {
            string strSql = "";

            strSql = @"SELECT rcvbls_invc_hdr_id, to_char(to_timestamp(rcvbls_invc_date,'YYYY-MM-DD'),'DD-Mon-YYYY'), 
       created_by, sec.get_usr_name(a.created_by), rcvbls_invc_number, rcvbls_invc_type, 
       comments_desc, src_doc_hdr_id, customer_id, scm.get_cstmr_splr_name(a.customer_id),
       customer_site_id, scm.get_cstmr_splr_site_name(a.customer_site_id), 
       approval_status, next_aproval_action, invoice_amount, 
       payment_terms, src_doc_type, pymny_method_id, accb.get_pymnt_mthd_name(a.pymny_method_id), 
       amnt_paid, gl_batch_id, accb.get_gl_batch_name(a.gl_batch_id),
       cstmrs_doc_num, doc_tmplt_clsfctn, invc_curr_id, gst.get_pssbl_val(a.invc_curr_id)
  FROM accb.accb_rcvbls_invc_hdr a " +
              "WHERE((a.rcvbls_invc_hdr_id = " + hdrID + "))";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            //Global.pyblsFrm.docTmplt_SQL = strSql;
            return dtst;
        }

        public static string get_ScmRcvblsDocHdrNum(long srchdrID, string srcHdrType, int orgID)
        {
            string strSql = "";

            strSql = @"SELECT rcvbls_invc_number
  FROM accb.accb_rcvbls_invc_hdr a " +
              "WHERE((a.src_doc_hdr_id = " + srchdrID +
              " and a.src_doc_type='" + srcHdrType.Replace("'", "''") + "' and a.org_id=" + orgID + "))";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return dtst.Tables[0].Rows[0][0].ToString();
            }
            return "";
        }

        public static long get_ScmRcvblsDocHdrID(long srchdrID, string srcHdrType, int orgID)
        {
            string strSql = "";

            strSql = @"SELECT rcvbls_invc_hdr_id
  FROM accb.accb_rcvbls_invc_hdr a " +
              "WHERE((a.src_doc_hdr_id = " + srchdrID +
              " and a.src_doc_type='" + srcHdrType.Replace("'", "''") + "' and a.org_id=" + orgID + "))";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public static DataSet get_RcvblsDocHdr(string searchWord, string searchIn, long offset,
          int limit_size, long orgID)
        {
            string strSql = "";
            string whrcls = "";
            /*Document Number
         Document Description
         Document Classification
         Customer Name
         Customer's Doc. Number
         Source Doc Number
         Approval Status
         Created By
         Currency*/
            if (searchIn == "Document Number")
            {
                whrcls = " and (a.rcvbls_invc_number ilike '" + searchWord.Replace("'", "''") + "')";
            }
            else if (searchIn == "Document Description")
            {
                whrcls = " and (a.comments_desc ilike '" + searchWord.Replace("'", "''") + "')";
            }
            else if (searchIn == "Document Classification")
            {
                whrcls = " and (a.doc_tmplt_clsfctn ilike '" + searchWord.Replace("'", "''") + "')";
            }
            else if (searchIn == "Customer Name")
            {
                whrcls = @" and (a.customer_id IN (select c.cust_sup_id from 
scm.scm_cstmr_suplr c where c.cust_sup_name ilike '" + searchWord.Replace("'", "''") +
            "'))";
            }
            else if (searchIn == "Customer's Doc. Number")
            {
                whrcls = " and (a.cstmrs_doc_num b ilike '" + searchWord.Replace("'", "''") + "')";
            }
            else if (searchIn == "Source Doc Number")
            {
                whrcls = @" and (a.src_doc_hdr_id IN (select d.invc_hdr_id from scm.scm_sales_invc_hdr d 
where trim(to_char(d.invc_hdr_id, '9999999999999999999999999')) ilike '" + searchWord.Replace("'", "''") +
            @"') or a.src_doc_hdr_id IN (select f.rcvbls_invc_hdr_id from accb.accb_rcvbls_invc_hdr f
where f.rcvbls_invc_number ilike '" + searchWord.Replace("'", "''") +
            @"'))";
            }
            else if (searchIn == "Approval Status")
            {
                whrcls = " and (a.approval_status ilike '" + searchWord.Replace("'", "''") + "')";
            }
            else if (searchIn == "Created By")
            {
                whrcls = " and (sec.get_usr_name(a.created_by) ilike '" + searchWord.Replace("'", "''") + "')";
            }
            else if (searchIn == "Currency")
            {
                whrcls = " and (gst.get_pssbl_val(a.invc_curr_id) ilike '" + searchWord.Replace("'", "''") + "')";
            }
            strSql = @"SELECT rcvbls_invc_hdr_id, rcvbls_invc_number, rcvbls_invc_type 
        FROM accb.accb_rcvbls_invc_hdr a 
        WHERE((a.org_id = " + orgID + ")" + whrcls +
              ") ORDER BY rcvbls_invc_hdr_id DESC LIMIT " + limit_size +
              " OFFSET " + (Math.Abs(offset * limit_size)).ToString();

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            //Global.rcvblsFrm.rec_SQL = strSql;
            return dtst;
        }

        public static long get_Total_RcvblsDoc(string searchWord, string searchIn, long orgID)
        {
            string strSql = "";
            string whrcls = "";
            /*Document Number
         Document Description
         Document Classification
         Customer Name
         Customer's Doc. Number
         Source Doc Number
         Approval Status
         Created By
         Currency*/
            if (searchIn == "Document Number")
            {
                whrcls = " and (a.rcvbls_invc_number ilike '" + searchWord.Replace("'", "''") + "')";
            }
            else if (searchIn == "Document Description")
            {
                whrcls = " and (a.comments_desc ilike '" + searchWord.Replace("'", "''") + "')";
            }
            else if (searchIn == "Document Classification")
            {
                whrcls = " and (a.doc_tmplt_clsfctn ilike '" + searchWord.Replace("'", "''") + "')";
            }
            else if (searchIn == "Customer Name")
            {
                whrcls = @" and (a.customer_id IN (select c.cust_sup_id from 
scm.scm_cstmr_suplr c where c.cust_sup_name ilike '" + searchWord.Replace("'", "''") +
            "'))";
            }
            else if (searchIn == "Customer's Doc. Number")
            {
                whrcls = " and (a.cstmrs_doc_num b ilike '" + searchWord.Replace("'", "''") + "')";
            }
            else if (searchIn == "Source Doc Number")
            {
                whrcls = @" and (a.src_doc_hdr_id IN (select d.invc_hdr_id from scm.scm_sales_invc_hdr d 
where trim(to_char(d.invc_hdr_id, '9999999999999999999999999')) ilike '" + searchWord.Replace("'", "''") +
            @"') or a.src_doc_hdr_id IN (select f.rcvbls_invc_hdr_id from accb.accb_rcvbls_invc_hdr f
where f.rcvbls_invc_number ilike '" + searchWord.Replace("'", "''") +
            @"'))";
            }
            else if (searchIn == "Approval Status")
            {
                whrcls = " and (a.approval_status ilike '" + searchWord.Replace("'", "''") + "')";
            }
            else if (searchIn == "Created By")
            {
                whrcls = " and (sec.get_usr_name(a.created_by) ilike '" + searchWord.Replace("'", "''") + "')";
            }
            else if (searchIn == "Currency")
            {
                whrcls = " and (gst.get_pssbl_val(a.invc_curr_id) ilike '" + searchWord.Replace("'", "''") + "')";
            }
            strSql = @"SELECT count(1) 
        FROM accb.accb_rcvbls_invc_hdr a 
        WHERE((a.org_id = " + orgID + ")" + whrcls + ")";


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

        public static DataSet get_RcvblsDocDet(long docHdrID)
        {
            string strSql = "";
            string whrcls = @" and (a.rcvbl_smmry_type !='6Grand Total' and 
a.rcvbl_smmry_type !='7Total Payments Made' and a.rcvbl_smmry_type !='8Outstanding Balance')";
            //if (aprvlStatus != "Not Validated")
            //{
            //  //whrcls = "";, string aprvlStatus
            //}
            strSql = @"SELECT rcvbl_smmry_id, rcvbl_smmry_type, rcvbl_smmry_desc, rcvbl_smmry_amnt, 
       code_id_behind, auto_calc, incrs_dcrs1, 
       rvnu_acnt_id, incrs_dcrs2, rcvbl_acnt_id, appld_prepymnt_doc_id, 
       entrd_curr_id, gst.get_pssbl_val(a.entrd_curr_id), 
       func_curr_id, gst.get_pssbl_val(a.func_curr_id), 
      accnt_curr_id, gst.get_pssbl_val(a.accnt_curr_id), 
      func_curr_rate, accnt_curr_rate, 
       func_curr_amount, accnt_curr_amnt
  FROM accb.accb_rcvbl_amnt_smmrys a " +
              "WHERE((a.src_rcvbl_hdr_id = " + docHdrID + ")" + whrcls + ") ORDER BY rcvbl_smmry_type ASC ";

            //MessageBox.Show(strSql);
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            //Global.rcvblsFrm.recDt_SQL = strSql;
            return dtst;
        }

        public static DataSet get_ScmRcvblsDocDets(long docHdrID)
        {
            string strSql = "";
            strSql = @"SELECT rcvbl_smmry_type, rcvbl_smmry_desc, SUM(rcvbl_smmry_amnt), 
       code_id_behind, auto_calc, incrs_dcrs1, 
       rvnu_acnt_id, incrs_dcrs2, rcvbl_acnt_id, appld_prepymnt_doc_id, 
       entrd_curr_id, func_curr_id,accnt_curr_id, func_curr_rate, accnt_curr_rate, 
       SUM(func_curr_amount), SUM(accnt_curr_amnt)
  FROM scm.scm_rcvbl_amnt_smmrys a " +
              "WHERE((a.src_rcvbl_hdr_id = " + docHdrID +
              @")) GROUP BY rcvbl_smmry_type, rcvbl_smmry_desc, 
       code_id_behind, auto_calc, incrs_dcrs1, 
       rvnu_acnt_id, incrs_dcrs2, rcvbl_acnt_id, appld_prepymnt_doc_id, 
       entrd_curr_id, func_curr_id,accnt_curr_id, func_curr_rate, accnt_curr_rate 
      ORDER BY rcvbl_smmry_type ASC ";

            //MessageBox.Show(strSql);
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            //Global.rcvblsFrm.recDt_SQL = strSql;
            return dtst;
        }

        public static double getRcvblsDocGrndAmnt(long dochdrID)
        {
            string strSql = @"select SUM(CASE WHEN y.rcvbl_smmry_type = '3Discount' 
or scm.istaxwthhldng(y.code_id_behind)='1' or y.rcvbl_smmry_type='5Applied Prepayment'
      THEN -1*y.rcvbl_smmry_amnt ELSE y.rcvbl_smmry_amnt END) amnt " +
              "from accb.accb_rcvbl_amnt_smmrys y " +
              "where y.src_rcvbl_hdr_id = " + dochdrID +
              " and y.rcvbl_smmry_type IN ('1Initial Amount','2Tax','3Discount','4Extra Charge','5Applied Prepayment')";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            double rs = 0;

            if (dtst.Tables[0].Rows.Count > 0)
            {
                double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rs);
            }
            return rs;
        }

        public static double getScmRcvblsDocGrndAmnt(long dochdrID)
        {
            string strSql = @"select SUM(CASE WHEN y.rcvbl_smmry_type = '3Discount' 
or scm.istaxwthhldng(y.code_id_behind)='1' or y.rcvbl_smmry_type='5Applied Prepayment'
      THEN -1*y.rcvbl_smmry_amnt ELSE y.rcvbl_smmry_amnt END) amnt " +
              "from scm.scm_rcvbl_amnt_smmrys y " +
              "where y.src_rcvbl_hdr_id = " + dochdrID +
              " and y.rcvbl_smmry_type IN ('1Initial Amount','2Tax','3Discount','4Extra Charge','5Applied Prepayment')";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            double rs = 0;

            if (dtst.Tables[0].Rows.Count > 0)
            {
                double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rs);
            }
            return rs;
        }

        public static double getScmRcvblsDocFuncAmnt(long dochdrID)
        {
            string strSql = @"select SUM(CASE WHEN y.rcvbl_smmry_type='3Discount' 
or scm.istaxwthhldng(y.code_id_behind)='1' or y.rcvbl_smmry_type='5Applied Prepayment'
      THEN -1*y.func_curr_amount ELSE y.func_curr_amount END) amnt " +
              "from scm.scm_rcvbl_amnt_smmrys y " +
              "where y.src_rcvbl_hdr_id=" + dochdrID +
              " and y.rcvbl_smmry_type IN ('1Initial Amount','2Tax','3Discount','4Extra Charge','5Applied Prepayment')";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            double rs = 0;

            if (dtst.Tables[0].Rows.Count > 0)
            {
                double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rs);
            }
            return rs;
        }

        public static double getRcvblsDocFuncAmnt(long dochdrID)
        {
            string strSql = @"select SUM(CASE WHEN y.rcvbl_smmry_type='3Discount' 
or scm.istaxwthhldng(y.code_id_behind)='1' or y.rcvbl_smmry_type='5Applied Prepayment'
      THEN -1*y.func_curr_amount ELSE y.func_curr_amount END) amnt " +
              "from accb.accb_rcvbl_amnt_smmrys y " +
              "where y.src_rcvbl_hdr_id=" + dochdrID +
              " and y.rcvbl_smmry_type IN ('1Initial Amount','2Tax','3Discount','4Extra Charge','5Applied Prepayment')";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            double rs = 0;

            if (dtst.Tables[0].Rows.Count > 0)
            {
                double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rs);
            }
            return rs;
        }

        public static double getRcvblsDocAccntAmnt(long dochdrID)
        {
            string strSql = @"select SUM(CASE WHEN y.rcvbl_smmry_type='3Discount' 
or scm.istaxwthhldng(y.code_id_behind)='1' or y.rcvbl_smmry_type='5Applied Prepayment'
      THEN -1*y.accnt_curr_amnt ELSE y.accnt_curr_amnt END) amnt " +
              "from accb.accb_rcvbl_amnt_smmrys y " +
              "where y.src_rcvbl_hdr_id=" + dochdrID +
              " and y.rcvbl_smmry_type IN ('1Initial Amount','2Tax','3Discount','4Extra Charge','5Applied Prepayment')";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            double rs = 0;

            if (dtst.Tables[0].Rows.Count > 0)
            {
                double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rs);
            }
            return rs;
        }

        public static long getRcvblsSmmryItmID(string smmryType, int codeBhnd,
          long srcDocID, string srcDocTyp, string smmryNm)
        {
            string strSql = "select y.rcvbl_smmry_id " +
              "from accb.accb_rcvbl_amnt_smmrys y " +
              "where y.rcvbl_smmry_type= '" + smmryType + "' and y.rcvbl_smmry_desc = '" + smmryNm +
              "' and y.code_id_behind= " + codeBhnd +
              " and y.src_rcvbl_type='" + srcDocTyp.Replace("'", "''") +
              "' and y.src_rcvbl_hdr_id=" + srcDocID + " ";
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

        public static void updtRcvblsDocApprvl(long docid,
      string apprvlSts, string nxtApprvl, double invcAmnt)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE accb.accb_rcvbls_invc_hdr SET " +
                  "approval_status='" + apprvlSts.Replace("'", "''") +
                  "', invoice_amount=" + invcAmnt + ", last_update_by=" + Global.myInv.user_id +
                  ", last_update_date='" + dateStr +
                  "', next_aproval_action='" + nxtApprvl.Replace("'", "''") +
                  "' WHERE (rcvbls_invc_hdr_id = " +
                  docid + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updtRcvblsDocApprvl(long docid,
      string apprvlSts, string nxtApprvl)
        {
            string extrCls = "";

            if (apprvlSts == "Cancelled" || apprvlSts == "Declared Bad Debt")
            {
                extrCls = ", invoice_amount=0, invc_amnt_appld_elswhr=0";
            }
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE accb.accb_rcvbls_invc_hdr SET " +
                  "approval_status='" + apprvlSts.Replace("'", "''") +
                  "', last_update_by=" + Global.myInv.user_id +
                  ", last_update_date='" + dateStr +
                  "', next_aproval_action='" + nxtApprvl.Replace("'", "''") +
                  "'" + extrCls + " WHERE (rcvbls_invc_hdr_id = " +
                  docid + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        //  public static void updtRcvblsDocApprvl(long docid,
        //string apprvlSts, string nxtApprvl)
        //  {
        //   Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
        //   string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
        //   string updtSQL = "UPDATE accb.accb_rcvbls_invc_hdr SET " +
        //         "approval_status='" + apprvlSts.Replace("'", "''") +
        //         "', last_update_by=" + Global.myInv.user_id +
        //         ", last_update_date='" + dateStr +
        //         "', next_aproval_action='" + nxtApprvl.Replace("'", "''") +
        //         "' WHERE (rcvbls_invc_hdr_id = " +
        //         docid + ")";
        //   Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        //  }

        public static void updtRcvblsDocGLBatch(long docid,
      long glBatchID)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE accb.accb_rcvbls_invc_hdr SET " +
                  "gl_batch_id=" + glBatchID +
                  ", last_update_by=" + Global.myInv.user_id +
                  ", last_update_date='" + dateStr +
                  "' WHERE (rcvbls_invc_hdr_id = " +
                  docid + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updtRcvblsDocBadDbtGLBatch(long docid,
    long glBatchID)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE accb.accb_rcvbls_invc_hdr SET " +
                  "debt_gl_batch_id=" + glBatchID +
                  ", last_update_by=" + Global.myInv.user_id +
                  ", last_update_date='" + dateStr +
                  "' WHERE (rcvbls_invc_hdr_id = " +
                  docid + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updtRcvblsDocAmntPaid(long docid,
      double amntPaid)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE accb.accb_rcvbls_invc_hdr SET " +
                  "amnt_paid=amnt_paid + " + amntPaid +
                  ", last_update_by=" + Global.myInv.user_id +
                  ", last_update_date='" + dateStr +
                  "' WHERE (rcvbls_invc_hdr_id = " +
                  docid + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updtRcvblsDocAmntAppld(long docid,
      double amntAppld)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE accb.accb_rcvbls_invc_hdr SET " +
                  "invc_amnt_appld_elswhr=invc_amnt_appld_elswhr + " + amntAppld +
                  ", last_update_by=" + Global.myInv.user_id +
                  ", last_update_date='" + dateStr +
                  "' WHERE (rcvbls_invc_hdr_id = " +
                  docid + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static double getRcvblsDocTtlPymnts(long dochdrID, string docType)
        {
            string strSql = "select SUM(y.amount_paid) amnt " +
              "from accb.accb_payments y " +
              "where y.src_doc_id = " + dochdrID + " and y.src_doc_typ = '" + docType.Replace("'", "''") + "'";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            double rs = 0;

            if (dtst.Tables[0].Rows.Count > 0)
            {
                double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rs);
            }
            return rs;
        }

        public static DataSet get_Batch_Attachments(long batchID)
        {
            string strSql = "";

            strSql = "SELECT a.attchmnt_id, a.batch_id, a.attchmnt_desc, a.file_name " +
          "FROM accb.accb_batch_trns_attchmnts a " +
          "WHERE(a.batch_id = " + batchID + ") ORDER BY a.attchmnt_id";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static long getSimlrPstdBatchID(long srcbatchid, string orgnlbatchname, int orgid)
        {
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
FROM scm.scm_gl_interface a, accb.accb_trnsctn_details b, accb.accb_trnsctn_batches c, accb.accb_chart_of_accnts d
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

        #endregion

        #region "PAYABLES..."
        public static long getNewPyblsLnID()
        {
            //string strSql = "select nextval('accb.accb_trnsctn_batches_batch_id_seq'::regclass);";
            string strSql = "select nextval('accb.accb_pybls_amnt_smmrys_pybls_smmry_id_seq')";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public static void createScmPyblsDocDet(long hdrID, string lineType, string lineDesc,
          double entrdAmnt, int entrdCurrID, int codeBhnd, string docType,
          bool autoCalc, string incrDcrs1, int costngID, string incrDcrs2, int blncgAccntID,
          long prepayDocHdrID, string vldyStatus, long orgnlLnID,
          int funcCurrID, int accntCurrID, double funcCurrRate, double accntCurrRate,
          double funcCurrAmnt, double accntCurrAmnt)
        {

            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            //rcvbl_smmry_id, " + smmryID + ", 
            string insSQL = @"INSERT INTO scm.scm_pybls_amnt_smmrys(
            pybls_smmry_type, pybls_smmry_desc, pybls_smmry_amnt, 
            code_id_behind, src_pybls_type, src_pybls_hdr_id, created_by, 
            creation_date, last_update_by, last_update_date, auto_calc, incrs_dcrs1, 
            asset_expns_acnt_id, incrs_dcrs2, liability_acnt_id, appld_prepymnt_doc_id, 
            orgnl_line_id, validty_status, entrd_curr_id, func_curr_id, accnt_curr_id, 
            func_curr_rate, accnt_curr_rate, func_curr_amount, accnt_curr_amnt) " +
                  "VALUES ('" + lineType.Replace("'", "''") +
                  "', '" + lineDesc.Replace("'", "''") +
                  "', " + entrdAmnt +
                  ", " + codeBhnd +
                  ", '" + docType.Replace("'", "''") +
                  "', " + hdrID +
                  ", " + Global.myInv.user_id + ", '" + dateStr +
                  "', " + Global.myInv.user_id + ", '" + dateStr +
                  "', '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(autoCalc) +
                  "', '" + incrDcrs1.Replace("'", "''") +
                  "', " + costngID +
                  ", '" + incrDcrs2.Replace("'", "''") +
                  "', " + blncgAccntID +
                  ", " + prepayDocHdrID +
                  ", " + orgnlLnID +
                  ", '" + vldyStatus.Replace("'", "''") +
                  "', " + entrdCurrID +
                  ", " + funcCurrID +
                  ", " + accntCurrID +
                  ", " + funcCurrRate +
                  ", " + accntCurrRate +
                  ", " + funcCurrAmnt +
                  ", " + accntCurrAmnt +
                  ")";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void updtScmPyblsDocDet(long docDetID, long hdrID, string lineType, string lineDesc,
          double entrdAmnt, int entrdCurrID, int codeBhnd, string docType,
          bool autoCalc, string incrDcrs1, int costngID, string incrDcrs2, int blncgAccntID,
          long prepayDocHdrID, string vldyStatus, long orgnlLnID,
          int funcCurrID, int accntCurrID, double funcCurrRate, double accntCurrRate,
          double funcCurrAmnt, double accntCurrAmnt)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = @"UPDATE scm.scm_pybls_amnt_smmrys
   SET pybls_smmry_type='" + lineType.Replace("'", "''") +
                  "', pybls_smmry_desc='" + lineDesc.Replace("'", "''") +
                  "', pybls_smmry_amnt=" + entrdAmnt +
                  ", code_id_behind=" + codeBhnd +
                  ", src_pybls_type='" + docType.Replace("'", "''") +
                  "', src_pybls_hdr_id=" + hdrID +
                  ", last_update_by=" + Global.myInv.user_id +
                  ", last_update_date='" + dateStr +
                  "', auto_calc='" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(autoCalc) +
                  "', incrs_dcrs1='" + incrDcrs1.Replace("'", "''") +
                  "', asset_expns_acnt_id=" + costngID +
                  ", incrs_dcrs2='" + incrDcrs2.Replace("'", "''") +
                  "', liability_acnt_id=" + blncgAccntID +
                  ", appld_prepymnt_doc_id=" + prepayDocHdrID +
                  ", validty_status='" + vldyStatus.Replace("'", "''") +
                  "', orgnl_line_id=" + orgnlLnID +
                  ", entrd_curr_id=" + entrdCurrID +
                  ", func_curr_id=" + funcCurrID +
                  ", accnt_curr_id=" + accntCurrID +
                  ", func_curr_rate=" + funcCurrRate +
                  ", accnt_curr_rate=" + accntCurrRate +
                  ", func_curr_amount=" + funcCurrAmnt +
                  ", accnt_curr_amnt=" + accntCurrAmnt +
                  " WHERE pybls_smmry_id = " + docDetID;
            Global.mnFrm.cmCde.updateDataNoParams(insSQL);
        }

        public static void deleteScmPyblsDocDets(long valLnid, string docNum)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Document Number = " + docNum;
            string delSQL = "DELETE FROM scm.scm_pybls_amnt_smmrys WHERE src_pybls_hdr_id = " + valLnid;
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
        }

        public static void createPyblsDocHdr(int orgid, string docDte, string docNum,
      string docType, string docDesc, long srcDocHdrID, int spplrID, int spplrSiteID,
        string apprvlStatus, string nxtApprvlActn, double invcAmnt, string pymntTrms,
        string srcDocType, int pymntMthdID, double amntPaid, long glBtchID,
        string spplrInvcNum, string docTmpltClsftn, int currID, double amntAppld)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            docDte = DateTime.ParseExact(docDte, "dd-MMM-yyyy",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            string insSQL = @"INSERT INTO accb.accb_pybls_invc_hdr(
            pybls_invc_date, created_by, creation_date, 
            last_update_by, last_update_date, pybls_invc_number, pybls_invc_type, 
            comments_desc, src_doc_hdr_id, supplier_id, supplier_site_id, 
            approval_status, next_aproval_action, org_id, invoice_amount, 
            payment_terms, src_doc_type, pymny_method_id, amnt_paid, gl_batch_id, 
            spplrs_invc_num, doc_tmplt_clsfctn, invc_curr_id, invc_amnt_appld_elswhr) " +
                  "VALUES ('" + docDte.Replace("'", "''") +
                  "', " + Global.myInv.user_id + ", '" + dateStr +
                  "', " + Global.myInv.user_id + ", '" + dateStr +
                  "', '" + docNum.Replace("'", "''") +
                  "', '" + docType.Replace("'", "''") +
                  "', '" + docDesc.Replace("'", "''") +
                  "', " + srcDocHdrID +
                  ", " + spplrID +
                  ", " + spplrSiteID +
                  ", '" + apprvlStatus.Replace("'", "''") +
                  "', '" + nxtApprvlActn.Replace("'", "''") +
                  "', " + orgid +
                  ", " + invcAmnt +
                  ", '" + pymntTrms.Replace("'", "''") +
                  "', '" + srcDocType.Replace("'", "''") +
                  "', " + pymntMthdID +
                  ", " + amntPaid +
                  ", " + glBtchID +
                  ", '" + spplrInvcNum.Replace("'", "''") +
                  "', '" + docTmpltClsftn.Replace("'", "''") +
                  "', " + currID + ", " + amntAppld + ")";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void updtPyblsDocHdr(long hdrID, string docDte, string docNum,
        string docType, string docDesc, long srcDocHdrID, int spplrID, int spplrSiteID,
          string apprvlStatus, string nxtApprvlActn, double invcAmnt, string pymntTrms,
          string srcDocType, int pymntMthdID, double amntPaid, long glBtchID,
          string spplrInvcNum, string docTmpltClsftn, int currID, double amntAppld)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            docDte = DateTime.ParseExact(docDte, "dd-MMM-yyyy",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = @"UPDATE accb.accb_pybls_invc_hdr
       SET pybls_invc_date='" + docDte.Replace("'", "''") +
                  "', last_update_by=" + Global.myInv.user_id +
                  ", last_update_date='" + dateStr +
                  "', pybls_invc_number='" + docNum.Replace("'", "''") +
                  "', pybls_invc_type='" + docType.Replace("'", "''") +
                  "', comments_desc='" + docDesc.Replace("'", "''") +
                  "', src_doc_hdr_id=" + srcDocHdrID +
                  ", supplier_id=" + spplrID +
                  ", supplier_site_id=" + spplrSiteID +
                  ", approval_status='" + apprvlStatus.Replace("'", "''") +
                  "', next_aproval_action='" + nxtApprvlActn.Replace("'", "''") +
                  "', invoice_amount=" + invcAmnt +
                  ", payment_terms='" + pymntTrms.Replace("'", "''") +
                  "', src_doc_type='" + srcDocType.Replace("'", "''") +
                  "', pymny_method_id=" + pymntMthdID +
                  ", amnt_paid=" + amntPaid +
                  ", gl_batch_id=" + glBtchID +
                  ", spplrs_invc_num='" + spplrInvcNum.Replace("'", "''") +
                  "', doc_tmplt_clsfctn='" + docTmpltClsftn.Replace("'", "''") +
                  "', invc_curr_id=" + currID +
                  ", invc_amnt_appld_elswhr=" + amntAppld +
                  " WHERE pybls_invc_hdr_id = " + hdrID;
            Global.mnFrm.cmCde.updateDataNoParams(insSQL);
        }

        public static void createPyblsDocDet(long smmryID, long hdrID, string lineType, string lineDesc,
          double entrdAmnt, int entrdCurrID, int codeBhnd, string docType,
          bool autoCalc, string incrDcrs1, int costngID, string incrDcrs2, int blncgAccntID,
          long prepayDocHdrID, string vldyStatus, long orgnlLnID,
          int funcCurrID, int accntCurrID, double funcCurrRate, double accntCurrRate,
          double funcCurrAmnt, double accntCurrAmnt)
        {

            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = @"INSERT INTO accb.accb_pybls_amnt_smmrys(
            pybls_smmry_id, pybls_smmry_type, pybls_smmry_desc, pybls_smmry_amnt, 
            code_id_behind, src_pybls_type, src_pybls_hdr_id, created_by, 
            creation_date, last_update_by, last_update_date, auto_calc, incrs_dcrs1, 
            asset_expns_acnt_id, incrs_dcrs2, liability_acnt_id, appld_prepymnt_doc_id, 
            validty_status, orgnl_line_id, entrd_curr_id, 
            func_curr_id, accnt_curr_id, func_curr_rate, accnt_curr_rate, 
            func_curr_amount, accnt_curr_amnt) " +
                  "VALUES (" + smmryID + ", '" + lineType.Replace("'", "''") +
                  "', '" + lineDesc.Replace("'", "''") +
                  "', " + entrdAmnt +
                  ", " + codeBhnd +
                  ", '" + docType.Replace("'", "''") +
                  "', " + hdrID +
                  ", " + Global.myInv.user_id + ", '" + dateStr +
                  "', " + Global.myInv.user_id + ", '" + dateStr +
                  "', '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(autoCalc) +
                  "', '" + incrDcrs1.Replace("'", "''") +
                  "', " + costngID +
                  ", '" + incrDcrs2.Replace("'", "''") +
                  "', " + blncgAccntID +
                  ", " + prepayDocHdrID +
                  ", '" + vldyStatus.Replace("'", "''") +
                  "', " + orgnlLnID +
                  ", " + entrdCurrID +
                  ", " + funcCurrID +
                  ", " + accntCurrID +
                  ", " + funcCurrRate +
                  ", " + accntCurrRate +
                  ", " + funcCurrAmnt +
                  ", " + accntCurrAmnt +
                  ")";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void updtPyblsDocDet(long docDetID, long hdrID, string lineType, string lineDesc,
          double entrdAmnt, int entrdCurrID, int codeBhnd, string docType,
          bool autoCalc, string incrDcrs1, int costngID, string incrDcrs2, int blncgAccntID,
          long prepayDocHdrID, string vldyStatus, long orgnlLnID,
          int funcCurrID, int accntCurrID, double funcCurrRate, double accntCurrRate,
          double funcCurrAmnt, double accntCurrAmnt)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = @"UPDATE accb.accb_pybls_amnt_smmrys
   SET pybls_smmry_type='" + lineType.Replace("'", "''") +
                  "', pybls_smmry_desc='" + lineDesc.Replace("'", "''") +
                  "', pybls_smmry_amnt=" + entrdAmnt +
                  ", code_id_behind=" + codeBhnd +
                  ", src_pybls_type='" + docType.Replace("'", "''") +
                  "', src_pybls_hdr_id=" + hdrID +
                  ", last_update_by=" + Global.myInv.user_id +
                  ", last_update_date='" + dateStr +
                  "', auto_calc='" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(autoCalc) +
                  "', incrs_dcrs1='" + incrDcrs1.Replace("'", "''") +
                  "', asset_expns_acnt_id=" + costngID +
                  ", incrs_dcrs2='" + incrDcrs2.Replace("'", "''") +
                  "', liability_acnt_id=" + blncgAccntID +
                  ", appld_prepymnt_doc_id=" + prepayDocHdrID +
                  ", validty_status='" + vldyStatus.Replace("'", "''") +
                  "', orgnl_line_id=" + orgnlLnID +
                  ", entrd_curr_id=" + entrdCurrID +
                  ", func_curr_id=" + funcCurrID +
                  ", accnt_curr_id=" + accntCurrID +
                  ", func_curr_rate=" + funcCurrRate +
                  ", accnt_curr_rate=" + accntCurrRate +
                  ", func_curr_amount=" + funcCurrAmnt +
                  ", accnt_curr_amnt=" + accntCurrAmnt +
                  " WHERE pybls_smmry_id = " + docDetID;
            Global.mnFrm.cmCde.updateDataNoParams(insSQL);
        }

        public static void deletePyblsDocHdrNDet(long valLnid, string docNum)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Document Number = " + docNum;
            string delSQL = "DELETE FROM accb.accb_pybls_amnt_smmrys WHERE src_pybls_hdr_id = " + valLnid;
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
            delSQL = "DELETE FROM accb.accb_pybls_invc_hdr WHERE pybls_invc_hdr_id = " + valLnid;
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
        }

        public static void deletePyblsDocDet(long valLnid)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string delSQL = "DELETE FROM accb.accb_pybls_amnt_smmrys WHERE pybls_smmry_id = " + valLnid;
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
        }

        public static void deletePyblsDocDetails(long valLnid, string docNum)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Document Number = " + docNum;
            string delSQL = "DELETE FROM accb.accb_pybls_amnt_smmrys WHERE src_pybls_hdr_id = " + valLnid;
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
        }
        public static DataSet get_One_PyblsDocHdr(long hdrID)
        {
            string strSql = "";

            strSql = @"SELECT pybls_invc_hdr_id, to_char(to_timestamp(pybls_invc_date,'YYYY-MM-DD'),'DD-Mon-YYYY'), 
       created_by, sec.get_usr_name(a.created_by), pybls_invc_number, pybls_invc_type, 
       comments_desc, src_doc_hdr_id, supplier_id, scm.get_cstmr_splr_name(a.supplier_id),
       supplier_site_id, scm.get_cstmr_splr_site_name(a.supplier_site_id), 
       approval_status, next_aproval_action, invoice_amount, 
       payment_terms, src_doc_type, pymny_method_id, accb.get_pymnt_mthd_name(a.pymny_method_id), 
       amnt_paid, gl_batch_id, accb.get_gl_batch_name(a.gl_batch_id),
       spplrs_invc_num, doc_tmplt_clsfctn, invc_curr_id, gst.get_pssbl_val(a.invc_curr_id)
  FROM accb.accb_pybls_invc_hdr a " +
              "WHERE((a.pybls_invc_hdr_id = " + hdrID + "))";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            //Global.pyblsFrm.docTmplt_SQL = strSql;
            return dtst;
        }

        public static string get_ScmPyblsDocHdrNum(long srchdrID, string srcHdrType, int orgID)
        {
            string strSql = "";

            strSql = @"SELECT pybls_invc_number
  FROM accb.accb_pybls_invc_hdr a " +
              "WHERE((a.src_doc_hdr_id = " + srchdrID +
              " and a.src_doc_type='" + srcHdrType.Replace("'", "''") + "' and a.org_id=" + orgID + "))";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return dtst.Tables[0].Rows[0][0].ToString();
            }
            return "";
        }

        public static long get_ScmPyblsDocHdrID(long srchdrID, string srcHdrType, int orgID)
        {
            string strSql = "";

            strSql = @"SELECT pybls_invc_hdr_id
  FROM accb.accb_pybls_invc_hdr a " +
              "WHERE((a.src_doc_hdr_id = " + srchdrID +
              " and a.src_doc_type='" + srcHdrType.Replace("'", "''") + "' and a.org_id=" + orgID + "))";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public static DataSet get_ScmPyblsDocDets(long docHdrID, string docType)
        {
            string strSql = "";
            strSql = @"SELECT pybls_smmry_type, pybls_smmry_desc, SUM(pybls_smmry_amnt), 
       code_id_behind, auto_calc, incrs_dcrs1, 
       asset_expns_acnt_id, incrs_dcrs2, liability_acnt_id, appld_prepymnt_doc_id, 
       entrd_curr_id, func_curr_id,accnt_curr_id, func_curr_rate, accnt_curr_rate, 
       SUM(func_curr_amount), SUM(accnt_curr_amnt)
  FROM scm.scm_pybls_amnt_smmrys a " +
              "WHERE((a.src_pybls_hdr_id = " + docHdrID +
              @" and a.src_pybls_type='" + docType.Replace("'", "''") +
              @"')) GROUP BY pybls_smmry_type, pybls_smmry_desc, 
       code_id_behind, auto_calc, incrs_dcrs1, 
       asset_expns_acnt_id, incrs_dcrs2, liability_acnt_id, appld_prepymnt_doc_id, 
       entrd_curr_id, func_curr_id,accnt_curr_id, func_curr_rate, accnt_curr_rate 
      ORDER BY pybls_smmry_type ASC ";

            //Global.mnFrm.cmCde.showSQLNoPermsn(strSql);
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            //Global.rcvblsFrm.recDt_SQL = strSql;
            return dtst;
        }

        public static void flagDsplyDocLineInRcpt(string parPOID, string parPOLine, string parValue)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            //update details
            string qryUpdatePODet = "UPDATE scm.scm_prchs_docs_det SET last_update_by = " + Global.myInv.user_id +
                ", last_update_date = '" + dateStr +
                "', dsply_doc_line_in_rcpt = " + parValue +
                " WHERE prchs_doc_hdr_id = " + long.Parse(parPOID) +
                " AND prchs_doc_line_id = " + long.Parse(parPOLine);

            Global.mnFrm.cmCde.updateDataNoParams(qryUpdatePODet);
        }

        public static void updatePOHdr(string parPOID, string parRecStatus)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            //update header
            string qryUpdatePOHdr = "UPDATE scm.scm_prchs_docs_hdr SET last_update_by = " + Global.myInv.user_id +
                ", last_update_date = '" + dateStr +
                "', po_rec_status = '" + parRecStatus.Replace("'", "''") +
                "' WHERE prchs_doc_hdr_id = " + long.Parse(parPOID) +
                " AND org_id = " + Global.mnFrm.cmCde.Org_id;

            Global.mnFrm.cmCde.updateDataNoParams(qryUpdatePOHdr);
        }

        public static void updatePODet(string parPOID, string parPOLine, double parQtyRcvd)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            //update details
            string qryUpdatePODet = "UPDATE scm.scm_prchs_docs_det SET last_update_by = " + Global.myInv.user_id +
                ", last_update_date = '" + dateStr +
                "', qty_rcvd = (qty_rcvd + " + parQtyRcvd +
                ") WHERE prchs_doc_hdr_id = " + long.Parse(parPOID) +
                " AND prchs_doc_line_id = " + long.Parse(parPOLine);

            Global.mnFrm.cmCde.updateDataNoParams(qryUpdatePODet);
        }

        public static double getPyblsDocGrndAmnt(long dochdrID)
        {
            string strSql = @"select SUM(CASE WHEN y.pybls_smmry_type = '3Discount' 
or scm.istaxwthhldng(y.code_id_behind)='1' or y.pybls_smmry_type='5Applied Prepayment'
      THEN -1*y.pybls_smmry_amnt ELSE y.pybls_smmry_amnt END) amnt " +
              "from accb.accb_pybls_amnt_smmrys y " +
              "where y.src_pybls_hdr_id = " + dochdrID +
              " and y.pybls_smmry_type IN ('1Initial Amount','2Tax','3Discount','4Extra Charge','5Applied Prepayment')";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            double rs = 0;

            if (dtst.Tables[0].Rows.Count > 0)
            {
                double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rs);
            }
            return rs;
        }

        public static double getPyblsDocOutstAmnt(long dochdrID)
        {
            string strSql = @"select SUM(y.pybls_smmry_amnt) amnt " +
              "from accb.accb_pybls_amnt_smmrys y " +
              "where y.src_pybls_hdr_id = " + dochdrID +
              " and y.pybls_smmry_type IN ('8Outstanding Balance')";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            double rs = 0;

            if (dtst.Tables[0].Rows.Count > 0)
            {
                double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rs);
            }
            return rs;
        }

        public static double getScmPyblsDocGrndAmnt(long dochdrID)
        {
            string strSql = @"select SUM(CASE WHEN y.pybls_smmry_type = '3Discount' 
or scm.istaxwthhldng(y.code_id_behind)='1' or y.pybls_smmry_type='5Applied Prepayment'
      THEN -1*y.pybls_smmry_amnt ELSE y.pybls_smmry_amnt END) amnt " +
              "from scm.scm_pybls_amnt_smmrys y " +
              "where y.src_pybls_hdr_id = " + dochdrID +
              " and y.pybls_smmry_type IN ('1Initial Amount','2Tax','3Discount','4Extra Charge','5Applied Prepayment')";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            double rs = 0;

            if (dtst.Tables[0].Rows.Count > 0)
            {
                double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rs);
            }
            return rs;
        }

        public static double getScmPyblsDocFuncAmnt(long dochdrID)
        {
            string strSql = @"select SUM(CASE WHEN y.pybls_smmry_type='3Discount' 
or scm.istaxwthhldng(y.code_id_behind)='1' or y.pybls_smmry_type='5Applied Prepayment'
      THEN -1*y.func_curr_amount ELSE y.func_curr_amount END) amnt " +
              "from scm.scm_pybls_amnt_smmrys y " +
              "where y.src_pybls_hdr_id=" + dochdrID +
              " and y.pybls_smmry_type IN ('1Initial Amount','2Tax','3Discount','4Extra Charge','5Applied Prepayment')";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            double rs = 0;

            if (dtst.Tables[0].Rows.Count > 0)
            {
                double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rs);
            }
            return rs;
        }

        public static void updtPyblsDocApprvl(long docid,
      string apprvlSts, string nxtApprvl, double invcAmnt)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE accb.accb_pybls_invc_hdr SET " +
                  "approval_status='" + apprvlSts.Replace("'", "''") +
                  "', invoice_amount=" + invcAmnt + ", last_update_by=" + Global.myInv.user_id +
                  ", last_update_date='" + dateStr +
                  "', next_aproval_action='" + nxtApprvl.Replace("'", "''") +
                  "' WHERE (pybls_invc_hdr_id = " +
                  docid + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static double getPyblsDocFuncAmnt(long dochdrID)
        {
            string strSql = @"select SUM(CASE WHEN y.pybls_smmry_type='3Discount' 
or scm.istaxwthhldng(y.code_id_behind)='1' or y.pybls_smmry_type='5Applied Prepayment'
      THEN -1*y.func_curr_amount ELSE y.func_curr_amount END) amnt " +
              "from accb.accb_pybls_amnt_smmrys y " +
              "where y.src_pybls_hdr_id=" + dochdrID +
              " and y.pybls_smmry_type IN ('1Initial Amount','2Tax','3Discount','4Extra Charge','5Applied Prepayment')";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            double rs = 0;

            if (dtst.Tables[0].Rows.Count > 0)
            {
                double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rs);
            }
            return rs;
        }

        public static double getPyblsDocAccntAmnt(long dochdrID)
        {
            string strSql = @"select SUM(CASE WHEN y.pybls_smmry_type='3Discount' 
or scm.istaxwthhldng(y.code_id_behind)='1' or y.pybls_smmry_type='5Applied Prepayment'
      THEN -1*y.accnt_curr_amnt ELSE y.accnt_curr_amnt END) amnt " +
              "from accb.accb_pybls_amnt_smmrys y " +
              "where y.src_pybls_hdr_id=" + dochdrID +
              " and y.pybls_smmry_type IN ('1Initial Amount','2Tax','3Discount','4Extra Charge','5Applied Prepayment')";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            double rs = 0;

            if (dtst.Tables[0].Rows.Count > 0)
            {
                double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rs);
            }
            return rs;
        }

        public static long getPyblsSmmryItmID(string smmryType, int codeBhnd,
          long srcDocID, string srcDocTyp, string smmryNm)
        {
            string strSql = "select y.pybls_smmry_id " +
              "from accb.accb_pybls_amnt_smmrys y " +
              "where y.pybls_smmry_type= '" + smmryType + "' and y.pybls_smmry_desc = '" + smmryNm +
              "' and y.code_id_behind= " + codeBhnd +
              " and y.src_pybls_type='" + srcDocTyp.Replace("'", "''") +
              "' and y.src_pybls_hdr_id=" + srcDocID + " ";
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

        public static void updtPyblsDocApprvl(long docid,
      string apprvlSts, string nxtApprvl)
        {
            string extrCls = "";
            if (apprvlSts == "Cancelled")
            {
                extrCls = ", invoice_amount=0, invc_amnt_appld_elswhr=0";
            }
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE accb.accb_pybls_invc_hdr SET " +
                  "approval_status='" + apprvlSts.Replace("'", "''") +
                  "', last_update_by=" + Global.myInv.user_id +
                  ", last_update_date='" + dateStr +
                  "', next_aproval_action='" + nxtApprvl.Replace("'", "''") +
                  "'" + extrCls + " WHERE (pybls_invc_hdr_id = " +
                  docid + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updtPyblsDocGLBatch(long docid,
      long glBatchID)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE accb.accb_pybls_invc_hdr SET " +
                  "gl_batch_id=" + glBatchID +
                  ", last_update_by=" + Global.myInv.user_id +
                  ", last_update_date='" + dateStr +
                  "' WHERE (pybls_invc_hdr_id = " +
                  docid + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static double getPyblsDocTtlPymnts(long dochdrID, string docType)
        {
            string strSql = "select SUM(y.amount_paid) amnt " +
              "from accb.accb_payments y " +
              "where y.src_doc_id = " + dochdrID + " and y.src_doc_typ = '" + docType.Replace("'", "''") + "'";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            double rs = 0;

            if (dtst.Tables[0].Rows.Count > 0)
            {
                double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rs);
            }
            return rs;
        }

        #endregion

        #region "PAYABLES..."
        public static string getLtstPyblsIDNoInPrfx(string prfxTxt)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select count(pybls_invc_hdr_id) from accb.accb_pybls_invc_hdr WHERE org_id=" +
              Global.mnFrm.cmCde.Org_id + " and pybls_invc_number ilike '" + prfxTxt.Replace("'", "''") + "%'";
            dtSt = Global.mnFrm.cmCde.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return (long.Parse(dtSt.Tables[0].Rows[0][0].ToString()) + 1).ToString().PadLeft(4, '0');
            }
            else
            {
                return "0001";
            }
        }

        public static void createPyblsDocHdr(int orgid, string docDte, string docNum,
        string docType, string docDesc, long srcDocHdrID, int spplrID, int spplrSiteID,
          string apprvlStatus, string nxtApprvlActn, double invcAmnt, string pymntTrms,
          string srcDocType, int pymntMthdID, double amntPaid, long glBtchID,
          string spplrInvcNum, string docTmpltClsftn, int currID, double amntAppld,
          long rgstrID, string costCtgry, string evntType)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            docDte = DateTime.ParseExact(docDte, "dd-MMM-yyyy",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            string insSQL = @"INSERT INTO accb.accb_pybls_invc_hdr(
            pybls_invc_date, created_by, creation_date, 
            last_update_by, last_update_date, pybls_invc_number, pybls_invc_type, 
            comments_desc, src_doc_hdr_id, supplier_id, supplier_site_id, 
            approval_status, next_aproval_action, org_id, invoice_amount, 
            payment_terms, src_doc_type, pymny_method_id, amnt_paid, gl_batch_id, 
            spplrs_invc_num, doc_tmplt_clsfctn, invc_curr_id, invc_amnt_appld_elswhr,
            event_rgstr_id, evnt_cost_category, event_doc_type) " +
                  "VALUES ('" + docDte.Replace("'", "''") +
                  "', " + Global.myInv.user_id + ", '" + dateStr +
                  "', " + Global.myInv.user_id + ", '" + dateStr +
                  "', '" + docNum.Replace("'", "''") +
                  "', '" + docType.Replace("'", "''") +
                  "', '" + docDesc.Replace("'", "''") +
                  "', " + srcDocHdrID +
                  ", " + spplrID +
                  ", " + spplrSiteID +
                  ", '" + apprvlStatus.Replace("'", "''") +
                  "', '" + nxtApprvlActn.Replace("'", "''") +
                  "', " + orgid +
                  ", " + invcAmnt +
                  ", '" + pymntTrms.Replace("'", "''") +
                  "', '" + srcDocType.Replace("'", "''") +
                  "', " + pymntMthdID +
                  ", " + amntPaid +
                  ", " + glBtchID +
                  ", '" + spplrInvcNum.Replace("'", "''") +
                  "', '" + docTmpltClsftn.Replace("'", "''") +
                  "', " + currID + ", " + amntAppld + ", " + rgstrID +
                  ", '" + costCtgry.Replace("'", "''") + "', '" + evntType.Replace("'", "''") + "')";
            //Global.mnFrm.cmCde.showSQLNoPermsn(insSQL);
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void updtPyblsDocHdr(long hdrID, string docDte, string docNum,
        string docType, string docDesc, long srcDocHdrID, int spplrID, int spplrSiteID,
          string apprvlStatus, string nxtApprvlActn, double invcAmnt, string pymntTrms,
          string srcDocType, int pymntMthdID, double amntPaid, long glBtchID,
          string spplrInvcNum, string docTmpltClsftn, int currID, double amntAppld,
          long rgstrID, string costCtgry, string evntType)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            docDte = DateTime.ParseExact(docDte, "dd-MMM-yyyy",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = @"UPDATE accb.accb_pybls_invc_hdr
       SET pybls_invc_date='" + docDte.Replace("'", "''") +
                  "', last_update_by=" + Global.myInv.user_id +
                  ", last_update_date='" + dateStr +
                  "', pybls_invc_number='" + docNum.Replace("'", "''") +
                  "', pybls_invc_type='" + docType.Replace("'", "''") +
                  "', comments_desc='" + docDesc.Replace("'", "''") +
                  "', src_doc_hdr_id=" + srcDocHdrID +
                  ", supplier_id=" + spplrID +
                  ", supplier_site_id=" + spplrSiteID +
                  ", approval_status='" + apprvlStatus.Replace("'", "''") +
                  "', next_aproval_action='" + nxtApprvlActn.Replace("'", "''") +
                  "', invoice_amount=" + invcAmnt +
                  ", payment_terms='" + pymntTrms.Replace("'", "''") +
                  "', src_doc_type='" + srcDocType.Replace("'", "''") +
                  "', pymny_method_id=" + pymntMthdID +
                  ", amnt_paid=" + amntPaid +
                  ", gl_batch_id=" + glBtchID +
                  ", spplrs_invc_num='" + spplrInvcNum.Replace("'", "''") +
                  "', doc_tmplt_clsfctn='" + docTmpltClsftn.Replace("'", "''") +
                  "', invc_curr_id=" + currID +
                  ", invc_amnt_appld_elswhr=" + amntAppld +
                     ", event_rgstr_id=" + rgstrID +
                  ", evnt_cost_category='" + costCtgry.Replace("'", "''") +
                  "', event_doc_type='" + evntType.Replace("'", "''") +
               "' WHERE pybls_invc_hdr_id = " + hdrID;
            Global.mnFrm.cmCde.updateDataNoParams(insSQL);
        }

        public static DataSet get_PyblsDocHdr(string searchWord, string searchIn, long offset,
          int limit_size, long orgID, bool shwUnpstdOnly)
        {
            string strSql = "";
            string whrcls = "";
            /*Document Number
         Document Description
         Document Classification
         Supplier Name
         Supplier's Invoice Number
         Source Doc Number
         Approval Status
         Created By*/
            string unpstdCls = "";
            if (shwUnpstdOnly)
            {
                unpstdCls = " AND (round(a.invoice_amount-a.amnt_paid,2)>0 or a.approval_status IN ('Not Validated','Validated','Reviewed'))";
                // AND (a.approval_status='Approved')
                //        unpstdCls = @" AND EXISTS (SELECT f.src_pybls_hdr_id 
                //FROM accb.accb_pybls_amnt_smmrys f WHERE f.pybls_smmry_type='8Outstanding Balance' 
                //and round(f.pybls_smmry_amnt,2)>0 and a.pybls_invc_hdr_id=f.src_pybls_hdr_id and f.src_pybls_type=a.pybls_invc_type)";
                //        //unpstdCls = " AND (a.approval_status!='Approved')";
            }
            if (searchIn == "Document Number")
            {
                whrcls = " and (a.pybls_invc_number ilike '" + searchWord.Replace("'", "''") + "')";
            }
            else if (searchIn == "Document Description")
            {
                whrcls = " and (a.comments_desc ilike '" + searchWord.Replace("'", "''") + "')";
            }
            else if (searchIn == "Document Classification")
            {
                whrcls = " and (a.doc_tmplt_clsfctn ilike '" + searchWord.Replace("'", "''") + "')";
            }
            else if (searchIn == "Supplier Name")
            {
                whrcls = @" and (a.supplier_id IN (select c.cust_sup_id from 
scm.scm_cstmr_suplr c where c.cust_sup_name ilike '" + searchWord.Replace("'", "''") +
            "'))";
            }
            else if (searchIn == "Supplier's Invoice Number")
            {
                whrcls = " and (a.spplrs_invc_num ilike '" + searchWord.Replace("'", "''") + "')";
            }
            else if (searchIn == "Source Doc Number")
            {
                whrcls = @" and (trim(to_char(a.src_doc_hdr_id, '9999999999999999999999999')) 
IN (select trim(to_char(d.rcpt_id, '9999999999999999999999999')) from inv.inv_consgmt_rcpt_hdr d 
where trim(to_char(d.rcpt_id, '9999999999999999999999999')) ilike '" + searchWord.Replace("'", "''") +
            @"') or trim(to_char(a.src_doc_hdr_id, '9999999999999999999999999')) 
IN (select trim(to_char(e.rcpt_rtns_id, '9999999999999999999999999')) from inv.inv_consgmt_rcpt_rtns_hdr e 
where trim(to_char(e.rcpt_rtns_id, '9999999999999999999999999')) ilike '" + searchWord.Replace("'", "''") +
            @"') or a.src_doc_hdr_id IN (select f.pybls_invc_hdr_id from accb.accb_pybls_invc_hdr f
where f.pybls_invc_number ilike '" + searchWord.Replace("'", "''") +
            @"'))";
            }
            else if (searchIn == "Approval Status")
            {
                whrcls = " and (a.approval_status ilike '" + searchWord.Replace("'", "''") + "')";
            }
            else if (searchIn == "Created By")
            {
                whrcls = " and (sec.get_usr_name(a.created_by) ilike '" + searchWord.Replace("'", "''") + "')";
            }
            else if (searchIn == "Currency")
            {
                whrcls = " and (gst.get_pssbl_val(a.invc_curr_id) ilike '" + searchWord.Replace("'", "''") + "')";
            }
            strSql = @"SELECT pybls_invc_hdr_id, pybls_invc_number, pybls_invc_type
, round(a.invoice_amount-a.amnt_paid,2),
 a.approval_status 
        FROM accb.accb_pybls_invc_hdr a 
        WHERE((a.org_id = " + orgID + ")" + whrcls + unpstdCls +
              ") ORDER BY pybls_invc_hdr_id DESC LIMIT " + limit_size +
              " OFFSET " + (Math.Abs(offset * limit_size)).ToString();

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            Global.wfnPyblsForm.rec_SQL = strSql;
            return dtst;
        }

        public static long get_Total_PyblsDoc(string searchWord, string searchIn, long orgID, bool shwUnpstdOnly)
        {
            string strSql = "";
            string whrcls = "";
            /*Document Number
         Document Description
         Document Classification
         Supplier Name
         Supplier's Invoice Number
         Source Doc Number
         Approval Status
         Created By*/
            string unpstdCls = "";
            if (shwUnpstdOnly)
            {
                // AND (a.approval_status='Approved')
                //        unpstdCls = @" AND EXISTS (SELECT f.src_pybls_hdr_id 
                //FROM accb.accb_pybls_amnt_smmrys f WHERE f.pybls_smmry_type='8Outstanding Balance' 
                //and round(f.pybls_smmry_amnt,2)>0 and a.pybls_invc_hdr_id=f.src_pybls_hdr_id and f.src_pybls_type=a.pybls_invc_type)";
                unpstdCls = " AND (round(a.invoice_amount-a.amnt_paid,2)>0 or a.approval_status IN ('Not Validated','Validated','Reviewed'))";
                //unpstdCls = " AND (a.approval_status!='Approved')";
            }
            if (searchIn == "Document Number")
            {
                whrcls = " and (a.pybls_invc_number ilike '" + searchWord.Replace("'", "''") + "')";
            }
            else if (searchIn == "Document Description")
            {
                whrcls = " and (a.comments_desc ilike '" + searchWord.Replace("'", "''") + "')";
            }
            else if (searchIn == "Document Classification")
            {
                whrcls = " and (a.doc_tmplt_clsfctn ilike '" + searchWord.Replace("'", "''") + "')";
            }
            else if (searchIn == "Supplier Name")
            {
                whrcls = @" and (a.supplier_id IN (select c.cust_sup_id from 
scm.scm_cstmr_suplr c where c.cust_sup_name ilike '" + searchWord.Replace("'", "''") +
            "'))";
            }
            else if (searchIn == "Supplier's Invoice Number")
            {
                whrcls = " and (a.spplrs_invc_num ilike '" + searchWord.Replace("'", "''") + "')";
            }
            else if (searchIn == "Source Doc Number")
            {
                whrcls = @" and (trim(to_char(a.src_doc_hdr_id, '9999999999999999999999999')) 
IN (select trim(to_char(d.rcpt_id, '9999999999999999999999999')) from inv.inv_consgmt_rcpt_hdr d 
where trim(to_char(d.rcpt_id, '9999999999999999999999999')) ilike '" + searchWord.Replace("'", "''") +
            @"') or trim(to_char(a.src_doc_hdr_id, '9999999999999999999999999')) 
IN (select trim(to_char(e.rcpt_rtns_id, '9999999999999999999999999')) from inv.inv_consgmt_rcpt_rtns_hdr e 
where trim(to_char(e.rcpt_rtns_id, '9999999999999999999999999')) ilike '" + searchWord.Replace("'", "''") +
            @"') or a.src_doc_hdr_id IN (select f.pybls_invc_hdr_id from accb.accb_pybls_invc_hdr f
where f.pybls_invc_number ilike '" + searchWord.Replace("'", "''") +
            @"'))";
            }
            else if (searchIn == "Approval Status")
            {
                whrcls = " and (a.approval_status ilike '" + searchWord.Replace("'", "''") + "')";
            }
            else if (searchIn == "Created By")
            {
                whrcls = " and (sec.get_usr_name(a.created_by) ilike '" + searchWord.Replace("'", "''") + "')";
            }
            else if (searchIn == "Currency")
            {
                whrcls = " and (gst.get_pssbl_val(a.invc_curr_id) ilike '" + searchWord.Replace("'", "''") + "')";
            }
            strSql = @"SELECT count(1) FROM accb.accb_pybls_invc_hdr a  
        WHERE((a.org_id = " + orgID + ")" + whrcls + unpstdCls +
              ")";

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

        public static DataSet get_PyblsDocDet(long docHdrID)
        {
            string strSql = "";
            string whrcls = @" and (a.pybls_smmry_type !='6Grand Total' and 
a.pybls_smmry_type !='7Total Payments Made' and a.pybls_smmry_type !='8Outstanding Balance')";
            //if (aprvlStatus != "Not Validated")
            //{
            //  //whrcls = "";, string aprvlStatus
            //}
            strSql = @"SELECT pybls_smmry_id, pybls_smmry_type, pybls_smmry_desc, pybls_smmry_amnt, 
       code_id_behind, auto_calc, incrs_dcrs1, 
       asset_expns_acnt_id, incrs_dcrs2, liability_acnt_id, appld_prepymnt_doc_id, 
       entrd_curr_id, gst.get_pssbl_val(a.entrd_curr_id), 
       func_curr_id, gst.get_pssbl_val(a.func_curr_id), 
      accnt_curr_id, gst.get_pssbl_val(a.accnt_curr_id), 
      func_curr_rate, accnt_curr_rate, 
       func_curr_amount, accnt_curr_amnt
  FROM accb.accb_pybls_amnt_smmrys a " +
              "WHERE((a.src_pybls_hdr_id = " + docHdrID + ")" + whrcls + ") ORDER BY pybls_smmry_type ASC ";

            //MessageBox.Show(strSql);
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            Global.wfnPyblsForm.recDt_SQL = strSql;
            return dtst;
        }

        #endregion

        #region "PRODUCTION/MANUFACTURING..."
        public static long getInptsInvcID(long prcsRunID)
        {
            string strSql = "";
            strSql = "SELECT COALESCE(MAX(a.invoice_hdr_id),-1) " +
         "FROM scm.scm_process_run_inpts a, scm.scm_sales_invc_hdr b " +
            "WHERE ((a.process_run_id = " + prcsRunID + ") and a.invoice_hdr_id=b.invc_hdr_id and b.approval_status='Not Validated')";

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

        public static long getApprvdInptsInvcID(long prcsRunID)
        {
            string strSql = "";
            strSql = "SELECT COALESCE(MAX(a.invoice_hdr_id),-1) " +
         "FROM scm.scm_process_run_inpts a, scm.scm_sales_invc_hdr b " +
            "WHERE ((a.process_run_id = " + prcsRunID + ") and a.invoice_hdr_id=b.invc_hdr_id " +
            "and b.approval_status IN ('Approved','Validated'))";

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

        public static long getUnInvoicedInpts(long prcsRunID)
        {
            string strSql = "";
            strSql = "SELECT inv_itm_id, actual_qty, uom_id, unit_cost_price, crncy_id, store_id " +
         "FROM scm.scm_process_run_inpts a " +
            "WHERE ((a.process_run_id = " + prcsRunID + ") and a.invoice_hdr_id<=0)";

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

        public static long getOutptsInvcID(long prcsRunID)
        {
            string strSql = "";
            strSql = "SELECT COALESCE(MAX(a.rcpt_hdr_id),-1) " +
         "FROM scm.scm_process_run_outpts a, inv.inv_consgmt_rcpt_hdr b " +
            "WHERE ((a.process_run_id = " + prcsRunID + ") and a.rcpt_hdr_id = b.rcpt_id)";
            // and b.approval_status='Incomplete'
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

        public static long getUnInvoicedOutpts(long prcsRunID)
        {
            string strSql = "";
            strSql = "SELECT inv_itm_id, actual_qty, uom_id, unit_cost_price, crncy_id, store_id " +
         "FROM scm.scm_process_run_outpts a " +
            "WHERE ((a.process_run_id = " + prcsRunID + ") and a.rcpt_hdr_id<=0)";

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
        public static double[] exctProcessSQL(string prcssSQL, long prcss_run_id, long prcss_def_id, long prcss_itm_id)
        {
            double[] res = { 0.00, 0.00 };
            DataSet dtSt = new DataSet();
            string nwSQL = prcssSQL.Replace("{:process_run_id}", prcss_run_id.ToString()).Replace("{:process_def_id}", prcss_def_id.ToString()).Replace("{:inv_itm_id}", prcss_itm_id.ToString());
            dtSt = Global.mnFrm.cmCde.selectDataNoParams(nwSQL);
            // this.showSQLNoPermsn(nwSQL);

            if (dtSt.Tables[0].Rows.Count > 0)
            {
                try
                {
                    res[0] = double.Parse(dtSt.Tables[0].Rows[0][0].ToString());
                    res[1] = double.Parse(dtSt.Tables[0].Rows[0][1].ToString());
                    return res;
                }
                catch (Exception ex)
                {
                    return res;
                }
            }
            else
            {
                return res;
            }
        }

        public static bool isProcessSQLValid(string prcssSQL, long prcss_run_id, long prcss_def_id, long prcss_itm_id)
        {
            DataSet dtSt = new DataSet();
            string nwSQL = prcssSQL.Replace("{:process_run_id}", prcss_run_id.ToString()).Replace("{:process_def_id}", prcss_def_id.ToString()).Replace("{:inv_itm_id}", prcss_itm_id.ToString());
            try
            {
                dtSt = Global.mnFrm.cmCde.selectDataNoParams(nwSQL);
                if (dtSt.Tables.Count > 0)
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

        public static DataSet get_One_PrcsInpts(long prcsID, bool isDeftn)
        {
            string strSql = "";
            if (isDeftn)
            {
                strSql = @"SELECT a.inpt_id, a.inv_itm_id, inv.get_invitm_name(a.inv_itm_id), 
        a.required_qty, a.uom_id, inv.get_uom_name(a.uom_id), b.orgnl_selling_price, a.store_id, c.subinv_name " +
               "FROM (scm.scm_process_def_inpts a LEFT OUTER JOIN inv.inv_itm_list b ON(a.inv_itm_id = b.item_id )) " +
               "LEFT OUTER JOIN inv.inv_itm_subinventories c ON (a.store_id=c.subinv_id) " +
               "WHERE(a.process_def_id = " + prcsID +
               " and a.process_def_id > 0) ORDER BY a.inpt_id";
            }
            else
            {
                strSql = @"SELECT a.run_inpt_id, a.inv_itm_id, inv.get_invitm_name(a.inv_itm_id), 
        a.actual_qty, a.uom_id, inv.get_uom_name(a.uom_id), 
        a.unit_cost_price, (a.actual_qty*a.unit_cost_price), a.crncy_id, a.store_id, 
        a.remarks_cmnts, c.subinv_name, a.invoice_hdr_id, a.invoice_line_id, d.consgmnt_ids  
        FROM scm.scm_process_run_inpts a " +
               "LEFT OUTER JOIN inv.inv_itm_subinventories c ON (a.store_id=c.subinv_id) " +
               "LEFT OUTER JOIN scm.scm_sales_invc_det d ON (d.invc_det_ln_id = a.invoice_line_id)" +
            "WHERE(a.process_run_id = " + prcsID +
            " and a.process_run_id > 0) ORDER BY a.run_inpt_id";
            }
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            Global.wfnProdFrm.inpts_SQL = strSql;
            return dtst;
        }

        public static void cancelPrcsInpts(long prcRunID)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string updtSql = @"UPDATE scm.scm_process_run_inpts
   SET invoice_hdr_id=-1, 
       invoice_line_id=-1
 WHERE process_run_id=" + prcRunID;
            Global.mnFrm.cmCde.selectDataNoParams(updtSql);
        }
        public static DataSet get_One_PrcsOutpts(long prcsID, bool isDeftn)
        {
            string strSql = "";
            if (isDeftn)
            {
                strSql = @"SELECT a.outpt_id, a.inv_itm_id, inv.get_invitm_name(a.inv_itm_id), 
        a.required_qty, a.uom_id, inv.get_uom_name(a.uom_id), a.sql_formula,a.store_id, c.subinv_name " +
               "FROM scm.scm_process_def_outpts a " +
               "LEFT OUTER JOIN inv.inv_itm_subinventories c ON (a.store_id=c.subinv_id) " +
               "WHERE(a.process_def_id = " + prcsID +
               " and a.process_def_id > 0) ORDER BY a.outpt_id";
            }
            else
            {
                strSql = @"SELECT a.run_outpt_id, a.inv_itm_id, inv.get_invitm_name(a.inv_itm_id), 
        a.actual_qty, a.uom_id, inv.get_uom_name(a.uom_id), a.sql_formula, a.unit_cost_price, 
        (a.actual_qty*a.unit_cost_price), a.crncy_id, a.store_id, a.remarks_cmnts, c.subinv_name, a.rcpt_hdr_id, a.rcpt_line_id " +
               "FROM scm.scm_process_run_outpts a " +
               "LEFT OUTER JOIN inv.inv_itm_subinventories c ON (a.store_id=c.subinv_id) " +
            "WHERE(a.process_run_id = " + prcsID +
            " and a.process_run_id > 0) ORDER BY a.run_outpt_id";
            }
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            Global.wfnProdFrm.outputs_SQL = strSql;
            return dtst;
        }

        public static DataSet get_One_PrcsStages(long prcsID, bool isDeftn)
        {
            string strSql = "";
            if (isDeftn)
            {
                strSql = @"SELECT a.stage_id, a.stage_code_name, a.stage_code_desc, 
        a.stage_cost, a.cost_reason " +
               "FROM scm.scm_process_def_stages a " +
               "WHERE(a.process_def_id = " + prcsID +
               " and a.process_def_id > 0) ORDER BY a.stage_code_name";
            }
            else
            {
                strSql = @"SELECT a.stage_id, a.stage_code_name, a.stage_code_desc, 
        b.stage_cost, b.cost_reason, b.run_stage_id, b.cost_crncy_id,
        to_char(to_timestamp(b.start_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'),
        to_char(to_timestamp(b.end_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'),
        b.stage_status 
    FROM scm.scm_process_def_stages a, scm.scm_process_run_stages b " +
            "WHERE(b.process_run_id = " + prcsID +
            " and b.process_run_id > 0 and a.stage_id=b.def_stage_id) ORDER BY a.stage_code_name";
            }
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            Global.wfnProdFrm.recDt_SQL = strSql;
            return dtst;
        }

        public static DataSet get_One_PrcsDt(long prcsID, bool isDeftn)
        {
            string strSql = "";
            if (isDeftn)
            {
                strSql = @"SELECT a.process_def_id, a.process_def_name, a.process_def_description,
        a.process_def_clsfctn, a.is_enabled 
      FROM scm.scm_process_definition a " +
                 "WHERE(a.process_def_id = " + prcsID +
                 ")";
            }
            else
            {
                strSql = @"SELECT a.process_def_id, a.process_def_name, a.process_def_description,
        a.process_def_clsfctn, a.is_enabled, b.process_run_id, 
      to_char(to_timestamp(b.start_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), 
      to_char(to_timestamp(b.end_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), " +
         "b.batch_code_num, b.remarks_desc, " +
         @"b.process_status, b.created_by, 
(SELECT SUM(z.actual_qty*z.unit_cost_price) 
FROM scm.scm_process_run_inpts z 
WHERE z.process_run_id=b.process_run_id) inpts_cost, 
(SELECT SUM(z.stage_cost) 
FROM scm.scm_process_run_stages z 
WHERE z.process_run_id=b.process_run_id) stages_cost, 
(SELECT SUM(z.actual_qty*z.unit_cost_price) 
FROM scm.scm_process_run_outpts z 
WHERE z.process_run_id=b.process_run_id) outputs_cost  " +
         "FROM scm.scm_process_definition a, scm.scm_process_run b " +
         "WHERE(b.process_run_id = " + prcsID + " and a.process_def_id = b.process_def_id)";

            }
            //Global.mnFrm.cmCde.showSQLNoPermsn(strSql);
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static bool isPrcsDefInUse(long recID)
        {
            string strSql = "SELECT a.process_run_id " +
             "FROM scm.scm_process_run a " +
             "WHERE(a.process_def_id = " + recID + ") ORDER BY 1 LIMIT 1 OFFSET 0";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public static DataSet get_Basic_Process(
          string searchWord, string searchIn,
      Int64 offset, int limit_size, int orgID, bool isDeftn)
        {
            /*Run Status-R
              Batch Number-R
              Classification-D/R
              Created By-D/R
              Description-D/R
              Process Code/Name-D/R
              Start Date-R*/
            string strSql = "";
            string whereClause = "";
            string crtdByClause = "";
            if (isDeftn == false)
            {
                if (searchIn == "Batch Number")
                {
                    whereClause = "(a.batch_code_num ilike '" + searchWord.Replace("'", "''") +
                  "') AND ";
                }
                else if (searchIn == "Run Status")
                {
                    whereClause = "(a.process_status ilike '" + searchWord.Replace("'", "''") +
                           "') AND ";
                }
                else if (searchIn == "Start Date")
                {
                    whereClause = "(to_char(to_timestamp(a.start_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') ilike '" + searchWord.Replace("'", "''") +
                 "') AND ";
                }
            }

            if (searchIn == "Description")
            {
                if (isDeftn)
                {
                    whereClause = "(a.process_def_description ilike '" + searchWord.Replace("'", "''") +
                "') AND ";
                }
                else
                {
                    whereClause = "(a.remarks_desc ilike '" + searchWord.Replace("'", "''") +
          "') AND ";
                }
            }
            else if (searchIn == "Classification")
            {
                whereClause = "((Select b.process_def_clsfctn from scm.scm_process_definition b where b.process_def_id = a.process_def_id) ilike '" + searchWord.Replace("'", "''") +
            "') AND ";
            }
            else if (searchIn == "Process Code/Name")
            {
                whereClause = "((Select b.process_def_name from scm.scm_process_definition b where b.process_def_id = a.process_def_id) ilike '" + searchWord.Replace("'", "''") +
              "') AND ";
            }
            else if (searchIn == "Created By")
            {
                whereClause = "(a.created_by IN (select c.user_id from sec.sec_users c where c.user_name ilike '" + searchWord.Replace("'", "''") +
            "')) AND ";
            }

            if (isDeftn)
            {
                strSql = "SELECT a.process_def_id, a.process_def_name, a.process_def_clsfctn " +
             "FROM scm.scm_process_definition a " +
             "WHERE (" + whereClause + "(a.org_id = " + orgID +
             ")" + crtdByClause + ") ORDER BY a.process_def_id DESC LIMIT " + limit_size +
             " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            else
            {
                strSql = @"SELECT a.process_run_id, a.batch_code_num, z.process_def_clsfctn clsftn " +
         "FROM scm.scm_process_run a, scm.scm_process_definition z " +
         "WHERE (" + whereClause + "(z.org_id = " + orgID +
         " and z.process_def_id = a.process_def_id)" + crtdByClause + ") ORDER BY a.process_run_id DESC LIMIT " + limit_size +
         " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            Global.wfnProdFrm.rec_SQL = strSql;
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static long get_Total_Process(string searchWord, string searchIn, int orgID, bool isDeftn)
        {
            string strSql = "";
            string whereClause = "";
            string crtdByClause = "";
            if (isDeftn == false)
            {
                if (searchIn == "Batch Number")
                {
                    whereClause = "(a.batch_code_num ilike '" + searchWord.Replace("'", "''") +
                  "') AND ";
                }
                else if (searchIn == "Run Status")
                {
                    whereClause = "(a.process_status ilike '" + searchWord.Replace("'", "''") +
                           "') AND ";
                }
                else if (searchIn == "Start Date")
                {
                    whereClause = "(to_char(to_timestamp(a.start_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') ilike '" + searchWord.Replace("'", "''") +
                 "') AND ";
                }
            }

            if (searchIn == "Description")
            {
                if (isDeftn)
                {
                    whereClause = "(a.process_def_description ilike '" + searchWord.Replace("'", "''") +
                "') AND ";
                }
                else
                {
                    whereClause = "(a.remarks_desc ilike '" + searchWord.Replace("'", "''") +
          "') AND ";
                }
            }
            else if (searchIn == "Classification")
            {
                whereClause = "((Select b.process_def_clsfctn from scm.scm_process_definition b where b.process_def_id = a.process_def_id) ilike '" + searchWord.Replace("'", "''") +
            "') AND ";
            }
            else if (searchIn == "Process Code/Name")
            {
                whereClause = "((Select b.process_def_name from scm.scm_process_definition b where b.process_def_id = a.process_def_id) ilike '" + searchWord.Replace("'", "''") +
              "') AND ";
            }
            else if (searchIn == "Created By")
            {
                whereClause = "(a.created_by IN (select c.user_id from sec.sec_users c where c.user_name ilike '" + searchWord.Replace("'", "''") +
            "')) AND ";
            }

            if (isDeftn)
            {
                strSql = "SELECT count(1) " +
             "FROM scm.scm_process_definition a " +
             "WHERE (" + whereClause + "(a.org_id = " + orgID +
             ")" + crtdByClause + ")";
            }
            else
            {
                strSql = @"SELECT count(1) FROM scm.scm_process_run a, scm.scm_process_definition z " +
         "WHERE (" + whereClause + "(z.org_id = " + orgID +
         " and z.process_def_id = a.process_def_id)" + crtdByClause + ")";
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

        public static long getNewPrcsDefInptID()
        {
            string strSql = "select nextval('scm.scm_process_def_inpts_inpt_id_seq')";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public static long getNewPrcsDefOutptID()
        {
            //string strSql = "select nextval('accb.accb_trnsctn_batches_batch_id_seq'::regclass);";
            string strSql = "select nextval('scm.scm_process_def_outpts_outpt_id_seq')";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public static long getNewPrcsDefStageID()
        {
            //string strSql = "select nextval('accb.accb_trnsctn_batches_batch_id_seq'::regclass);";
            string strSql = "select nextval('scm.scm_process_def_stages_stage_id_seq')";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public static long getNewPrcsRunInptID()
        {
            string strSql = "select nextval('scm.scm_process_run_inpts_run_inpt_id_seq')";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }
        public static long getNewPrcsRunOutptID()
        {
            //string strSql = "select nextval('accb.accb_trnsctn_batches_batch_id_seq'::regclass);";
            string strSql = "select nextval('scm.scm_process_run_outpts_run_outpt_id_seq')";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }
        public static long getNewPrcsRunStageID()
        {
            //string strSql = "select nextval('accb.accb_trnsctn_batches_batch_id_seq'::regclass);";
            string strSql = "select nextval('scm.scm_process_run_stages_run_stage_id_seq')";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }
        public static void createProcessDeftn(int orgid, string prcssName,
          string desc, string prcssClsfctn, bool isEnabled)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = @"INSERT INTO scm.scm_process_definition(
            process_def_name, process_def_description, process_def_clsfctn, 
            is_enabled, created_by, creation_date, last_update_by, last_update_date, 
            org_id) " +
                  "VALUES ('" + prcssName.Replace("'", "''") +
                  "', '" + desc.Replace("'", "''") +
                  "', '" + prcssClsfctn.Replace("'", "''") +
                  "', '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(isEnabled) +
                  "', " + Global.myInv.user_id + ", '" + dateStr +
                  "', " + Global.myInv.user_id + ", '" + dateStr +
                  "', " + orgid + ")";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void updateProcessDeftn(long prcssDefID, string prcssName,
          string desc, string prcssClsfctn, bool isEnabled)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = @"UPDATE scm.scm_process_definition
   SET process_def_name='" + prcssName.Replace("'", "''") +
                  "', process_def_description='" + desc.Replace("'", "''") +
                  "', process_def_clsfctn='" + prcssClsfctn.Replace("'", "''") +
                  "', is_enabled='" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(isEnabled) +
                  "', last_update_by=" + Global.myInv.user_id + ", last_update_date='" + dateStr +
                  "' WHERE process_def_id=" + prcssDefID + "";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void createProcess(long prcssDefID, string prcssBatchName,
          string prcssdesc, string prcssStatus, string strtDte, string endDte)
        {
            strtDte = DateTime.ParseExact(
      strtDte, "dd-MMM-yyyy HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            endDte = DateTime.ParseExact(
         endDte, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = @"INSERT INTO scm.scm_process_run(
            batch_code_num, remarks_desc, start_date, end_date, 
            process_status, process_def_id, created_by, creation_date, last_update_by, 
            last_update_date) " +
                  "VALUES ('" + prcssBatchName.Replace("'", "''") +
                  "', '" + prcssdesc.Replace("'", "''") +
                  "', '" + strtDte.Replace("'", "''") +
                  "', '" + endDte.Replace("'", "''") +
                  "', '" + prcssStatus.Replace("'", "''") +
                  "', " + prcssDefID + ", " + Global.myInv.user_id + ", '" + dateStr +
                  "', " + Global.myInv.user_id + ", '" + dateStr +
                  "')";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void updateProcessRunStatus(long prcssRunID, string prcssStatus)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = @"UPDATE scm.scm_process_run
   SET end_date='" + dateStr.Replace("'", "''") +
                  "', process_status='" + prcssStatus.Replace("'", "''") +
                  "', last_update_by=" + Global.myInv.user_id +
                  ", last_update_date='" + dateStr +
                  "' WHERE process_run_id = " + prcssRunID + "";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }
        public static void updateProcess(long prcssRunID, string prcssBatchName,
        string prcssdesc, string prcssStatus, string strtDte, string endDte)
        {
            strtDte = DateTime.ParseExact(
      strtDte, "dd-MMM-yyyy HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            endDte = DateTime.ParseExact(
         endDte, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = @"UPDATE scm.scm_process_run
   SET batch_code_num='" + prcssBatchName.Replace("'", "''") +
                  "', remarks_desc='" + prcssdesc.Replace("'", "''") +
                  "', start_date='" + strtDte.Replace("'", "''") +
                  "', end_date='" + endDte.Replace("'", "''") +
                  "', process_status='" + prcssStatus.Replace("'", "''") +
                  "', last_update_by=" + Global.myInv.user_id +
                  ", last_update_date='" + dateStr +
                  "' WHERE process_run_id = " + prcssRunID + "";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void createProcessDeftnInpts(long inputID, int itemID,
          double qty, int uomID, long prcssDefID, int storeID)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = @"INSERT INTO scm.scm_process_def_inpts(
            inpt_id, inv_itm_id, required_qty, uom_id, created_by, creation_date, 
            last_update_by, last_update_date, process_def_id, store_id) " +
                  "VALUES (" + inputID + ", " + itemID +
                  ", " + qty + ", " + uomID +
                  ", " + Global.myInv.user_id + ", '" + dateStr +
                  "', " + Global.myInv.user_id + ", '" + dateStr +
                  "', " + prcssDefID + "," + storeID +
                  ")";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void updateProcessDeftnInpts(long inputID, int itemID,
          double qty, int uomID, int storeID)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = @"UPDATE scm.scm_process_def_inpts
   SET inv_itm_id=" + itemID +
                  ", required_qty=" + qty +
                  ", uom_id=" + uomID +
                  ", store_id=" + storeID +
                  ", last_update_by=" + Global.myInv.user_id +
                  ", last_update_date='" + dateStr +
                  "' WHERE inpt_id=" + inputID + "";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void createProcessDeftnOutpts(long outputID, int itemID,
          double qty, int uomID, long prcssDefID, string sqlFrmlr, int storeID)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = @"INSERT INTO scm.scm_process_def_outpts(
            outpt_id, inv_itm_id, required_qty, uom_id, created_by, creation_date, 
            last_update_by, last_update_date, process_def_id, sql_formula, store_id) " +
                  "VALUES (" + outputID + ", " + itemID +
                  ", " + qty + ", " + uomID +
                  ", " + Global.myInv.user_id + ", '" + dateStr +
                  "', " + Global.myInv.user_id + ", '" + dateStr +
                  "', " + prcssDefID + ", '" + sqlFrmlr.Replace("'", "''") + "'," + storeID +
                  ")";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void updateProcessDeftnOutpts(long outputID, int itemID,
          double qty, int uomID, string sqlFrmlr, int storeID)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = @"UPDATE scm.scm_process_def_outpts
   SET inv_itm_id=" + itemID +
                  ", required_qty=" + qty +
                  ", uom_id=" + uomID +
                  ", store_id=" + storeID +
                  ", sql_formula='" + sqlFrmlr.Replace("'", "''") +
                  "', last_update_by=" + Global.myInv.user_id +
                  ", last_update_date='" + dateStr +
                  "' WHERE outpt_id=" + outputID + "";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void createProcessDeftnStages(long stageID, long prcssDefID, string stageCdNm,
          string stageDesc, double stgeCost, string costReason)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = @"INSERT INTO scm.scm_process_def_stages(
            stage_id, process_def_id, stage_code_name, stage_code_desc, stage_cost, 
            cost_reason, created_by, creation_date, last_update_by, last_update_date) " +
                  "VALUES (" + stageID + ", " + prcssDefID +
                  ", '" + stageCdNm.Replace("'", "''") +
                  "', '" + stageDesc.Replace("'", "''") +
                  "', " + stgeCost +
                  ", '" + costReason.Replace("'", "''") +
                  "', " + Global.myInv.user_id + ", '" + dateStr +
                  "', " + Global.myInv.user_id + ", '" + dateStr +
                  "')";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void updateProcessDeftnStages(long stageID, string stageCdNm,
          string stageDesc, double stgeCost, string costReason)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = @"UPDATE scm.scm_process_def_stages
   SET stage_code_name='" + stageCdNm.Replace("'", "''") +
                  "', stage_code_desc='" + stageDesc.Replace("'", "''") +
                  "', stage_cost=" + stgeCost +
                  ", cost_reason='" + costReason.Replace("'", "''") +
                  "', last_update_by=" + Global.myInv.user_id +
                  ", last_update_date='" + dateStr +
                  "' WHERE stage_id=" + stageID + "";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void createProcessRunInpts(long inputID, int itemID,
          double qty, int uomID, double costPrice, long processRunID, int crncyID,
          int storeID, string inptRmrks, long invoiceID)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = @"INSERT INTO scm.scm_process_run_inpts(
            run_inpt_id, inv_itm_id, actual_qty, uom_id, unit_cost_price, 
            created_by, creation_date, last_update_by, last_update_date, 
            process_run_id, crncy_id, store_id, remarks_cmnts, invoice_hdr_id) " +
                  "VALUES (" + inputID + ", " + itemID +
                  ", " + qty + ", " + uomID +
                  ", " + costPrice +
                  ", " + Global.myInv.user_id + ", '" + dateStr +
                  "', " + Global.myInv.user_id + ", '" + dateStr +
                  "', " + processRunID + ", " + crncyID +
                  ", " + storeID +
                  ", '" + inptRmrks +
                  "', " + invoiceID +
                  ")";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void updateProcessRunInpts(long inputID, int itemID,
          double qty, int uomID, double costPrice, int crncyID,
          int storeID, string inptRmrks, long invoiceID)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = @"UPDATE scm.scm_process_run_inpts 
             SET inv_itm_id = " + itemID +
                  ", actual_qty=" + qty +
                  ", uom_id=" + uomID +
                  ", unit_cost_price=" + costPrice +
                  ", crncy_id=" + crncyID +
                  ", store_id=" + storeID +
                  ", invoice_hdr_id=" + invoiceID +
                  ", remarks_cmnts='" + inptRmrks.Replace("'", "''") +
                  "', last_update_by=" + Global.myInv.user_id +
                  ", last_update_date='" + dateStr +
                  "' WHERE run_inpt_id=" + inputID + "";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void createProcessRunOutpts(long outputID, int itemID,
          double qty, int uomID, long prcssRunID, double costPrice, string sqlFrmlr, int crncyID,
          int storeID, string inptRmrks, long rcptHdrID)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = @"INSERT INTO scm.scm_process_run_outpts(
            run_outpt_id, process_run_id, inv_itm_id, actual_qty, uom_id, 
            unit_cost_price, created_by, creation_date, last_update_by, last_update_date, 
            sql_formula, crncy_id, store_id, remarks_cmnts, rcpt_hdr_id) " +
                  "VALUES (" + outputID + ", " + prcssRunID +
                  ", " + itemID +
                  ", " + qty + ", " + uomID +
                  ", " + costPrice +
                  ", " + Global.myInv.user_id + ", '" + dateStr +
                  "', " + Global.myInv.user_id + ", '" + dateStr +
                  "', '" + sqlFrmlr.Replace("'", "''") + "', " + crncyID + ", " + storeID +
                  ", '" + inptRmrks.Replace("'", "''") + "', " + rcptHdrID + ")";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void updateProcessRunOutpts(long outputID, int itemID,
          double qty, int uomID, long prcssRunID, double costPrice, string sqlFrmlr, int crncyID,
          int storeID, string inptRmrks, long rcptHdrID)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = @"UPDATE scm.scm_process_run_outpts
   SET inv_itm_id=" + itemID +
                  ", actual_qty=" + qty +
                  ", uom_id=" + uomID +
                  ", unit_cost_price=" + costPrice +
                  ", crncy_id=" + crncyID +
                  ", store_id=" + storeID +
                  ", rcpt_hdr_id=" + rcptHdrID +
                  ", sql_formula='" + sqlFrmlr.Replace("'", "''") +
                  "', remarks_cmnts='" + inptRmrks.Replace("'", "''") +
                  "', last_update_by=" + Global.myInv.user_id +
                  ", last_update_date='" + dateStr +
                  "' WHERE run_outpt_id=" + outputID + "";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }
        public static void updateProcessRunOutptsQty(long outputID,
          double qty, double costPrice)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = @"UPDATE scm.scm_process_run_outpts
   SET actual_qty=" + qty +
                  ", unit_cost_price=" + costPrice +
                  ", last_update_by=" + Global.myInv.user_id +
                  ", last_update_date='" + dateStr +
                  "' WHERE run_outpt_id=" + outputID + "";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updateProcessRunOutpts(long outputID, long rcptHdrID, long rcptLineID)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = @"UPDATE scm.scm_process_run_outpts
   SET rcpt_hdr_id=" + rcptHdrID +
                  ", rcpt_line_id=" + rcptLineID +
                  ", last_update_by=" + Global.myInv.user_id +
                  ", last_update_date='" + dateStr +
                  "' WHERE run_outpt_id=" + outputID + "";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }
        public static long getProcessRunOutptsID(long rcptHdrID, long rcptLineID)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string selSQL = @"SELECT run_outpt_id FROM scm.scm_process_run_outpts
   WHERE rcpt_hdr_id=" + rcptHdrID +
                  " and rcpt_line_id=" + rcptLineID + "";
            DataSet dtdt = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
            if (dtdt.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtdt.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }
        public static void createProcessRunStages(long defStageID, long runStageID, long prcssRunID,
          double stgeCost, string costReason, int crncyID, string strtDte, string endDte, string status,
          long pyblsHdrID)
        {
            strtDte = DateTime.ParseExact(
      strtDte, "dd-MMM-yyyy HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            endDte = DateTime.ParseExact(
         endDte, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = @"INSERT INTO scm.scm_process_run_stages(
            def_stage_id, run_stage_id, process_run_id, stage_cost, cost_reason, 
            cost_crncy_id, start_date, end_date, stage_status, created_by, 
            creation_date, last_update_by, last_update_date, pybls_doc_hdr_id) " +
                  "VALUES (" + defStageID + ", " + runStageID + ", " + prcssRunID +
                  ", " + stgeCost +
                  ", '" + costReason.Replace("'", "''") +
                  "', " + crncyID +
                  ", '" + strtDte.Replace("'", "''") +
                  "', '" + endDte.Replace("'", "''") +
                  "', '" + status.Replace("'", "''") +
                  "', " + Global.myInv.user_id + ", '" + dateStr +
                  "', " + Global.myInv.user_id + ", '" + dateStr +
                  "', " + pyblsHdrID +
                  ")";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void updateProcessRunStages(long defStageID, long runStageID, long prcssRunID,
          double stgeCost, string costReason, int crncyID, string strtDte, string endDte, string status,
          long pyblsHdrID)
        {
            strtDte = DateTime.ParseExact(
      strtDte, "dd-MMM-yyyy HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            endDte = DateTime.ParseExact(
         endDte, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = @"UPDATE scm.scm_process_run_stages
            SET stage_cost=" + stgeCost +
                  ", cost_reason='" + costReason.Replace("'", "''") +
                  "', cost_crncy_id=" + crncyID +
                  ", start_date='" + strtDte.Replace("'", "''") +
                  "', end_date='" + endDte.Replace("'", "''") +
                  "', stage_status='" + status.Replace("'", "''") +
                  "', pybls_doc_hdr_id=" + pyblsHdrID +
                  ", last_update_by=" + Global.myInv.user_id +
                  ", last_update_date='" + dateStr +
                  "' WHERE run_stage_id=" + runStageID + "";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }
        public static void updateProcessAllRunStages(long prcssRunID, string status)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = @"UPDATE scm.scm_process_run_stages
            SET stage_status='" + status.Replace("'", "''") +
                  "', last_update_by=" + Global.myInv.user_id +
                  ", last_update_date='" + dateStr +
                  "' WHERE process_run_id=" + prcssRunID + "";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updtInptInvcLineID(long inptID, long invLineID, long invHdrID)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string insSQL = "UPDATE scm.scm_process_run_inpts " +
              "SET invoice_line_id=" + invLineID +
              ",invoice_hdr_id=" + invHdrID +
              ", last_update_by=" + Global.myInv.user_id +
              ", last_update_date='" + dateStr +
              "' WHERE run_inpt_id = " + inptID;
            Global.mnFrm.cmCde.updateDataNoParams(insSQL);
        }

        public static void updtOutptRcptLineID(long inptID, long invLineID, long rcptID)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string insSQL = "UPDATE scm.scm_process_run_outpts " +
              "SET rcpt_line_id=" + invLineID +
              ",  rcpt_hdr_id=" + rcptID +
              ",last_update_by=" + Global.myInv.user_id +
              ", last_update_date='" + dateStr +
              "' WHERE run_outpt_id = " + inptID;
            Global.mnFrm.cmCde.updateDataNoParams(insSQL);
        }
        #endregion
        #endregion
        #endregion

        #region "CUSTOM FUNCTIONS..."
        #region "MISC..."
        public static void getCurrentRecord(System.Windows.Forms.TextBox srcCntrl, System.Windows.Forms.ToolStripTextBox destCntrl)
        {
            destCntrl.Text = srcCntrl.Text;
        }

        public static string getItmCodeFrmStckID(long stockID)
        {
            string strSql = "";
            strSql = "SELECT item_code FROM inv.inv_itm_list WHERE item_id = (" +
            " SELECT itm_id FROM inv.inv_stock WHERE stock_id = " + stockID + ")";

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

        public static string getItmCodeFrmCnsgmntID(long cnsgmntID)
        {
            string strSql = "";
            strSql = "SELECT item_code FROM inv.inv_itm_list WHERE item_id = (" +
            " SELECT itm_id FROM inv.inv_consgmt_rcpt_det WHERE consgmt_id = " + cnsgmntID + ")";

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

        public static void updateItemBalances(string parItemCode, double parQtyRcvd)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();

            string qryUpdateItemBals = "UPDATE inv.inv_itm_list SET total_qty = (COALESCE(total_qty,0) + " + parQtyRcvd
                    + "), available_balance = (COALESCE(total_qty,0) - COALESCE(reservations,0) + " + parQtyRcvd
                    + "), last_update_date = '" + dateStr + "', last_update_by = " + Global.myInv.user_id +
                    " WHERE item_code = '" + parItemCode.Replace("'", "''") + "' AND org_id = " + Global.mnFrm.cmCde.Org_id;

            Global.mnFrm.cmCde.updateDataNoParams(qryUpdateItemBals);
        }

        public static string getLtstRecPkID(string tblNm, string pkeyCol)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select " + pkeyCol + " from " + tblNm + " ORDER BY 1 DESC LIMIT 1 OFFSET 0";
            dtSt = Global.mnFrm.cmCde.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                long num = long.Parse(dtSt.Tables[0].Rows[0][0].ToString()) + 1;
                if (num.ToString().Length < 4)
                {
                    return num.ToString().PadLeft(4, '0');
                }
                else
                {
                    return num.ToString();
                }
            }
            else
            {
                return "0001";
            }
        }

        public static Form isFormAlreadyOpen(Type formType)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.GetType() == formType)
                    return openForm;
            }
            return null;
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
        public static double getStockLstAvlblBls(long stockID, string balsDate)
        {
            balsDate = DateTime.ParseExact(
         balsDate, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            balsDate = balsDate.Substring(0, 10);
            string strSql = "";
            strSql = "SELECT COALESCE(a.available_balance,0) " +
          "FROM inv.inv_stock_daily_bals a " +
          "WHERE(to_timestamp(a.bals_date,'YYYY-MM-DD') <=  to_timestamp('" + balsDate +
          "','YYYY-MM-DD') and a.stock_id = " + stockID +
          ") ORDER BY to_timestamp(a.bals_date, 'YYYY-MM-DD') DESC LIMIT 1 OFFSET 0";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return double.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0.00;
            }
        }

        public static double getStoreLstTotBls(long itmID, long storID, string balsDate)
        {
            //    balsDate = DateTime.ParseExact(
            //balsDate, "dd-MMM-yyyy HH:mm:ss",
            //System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            balsDate = balsDate.Substring(0, 10);
            string strSql = "";
            strSql = @"SELECT scm.get_ltst_stock_bals(a.stock_id, '" + balsDate + @"')
 FROM inv.inv_stock a 
 WHERE(a.itm_id = " + itmID + " and a.subinv_id = " + storID + @")";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return double.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0.00;
            }
        }

        public static double getStoreLstTotBls(long itmID, long storID)
        {
            //    balsDate = DateTime.ParseExact(
            //balsDate, "dd-MMM-yyyy HH:mm:ss",
            //System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            string strSql = "";//
            strSql = @"SELECT scm.get_ltst_stock_bals(a.stock_id)
 FROM inv.inv_stock a 
 WHERE(a.itm_id = " + itmID + " and a.subinv_id = " + storID + @")";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return double.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0.00;
            }
        }

        public static double getStockLstTotBls(long stockID, string balsDate)
        {
            balsDate = DateTime.ParseExact(
         balsDate, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            balsDate = balsDate.Substring(0, 10);
            string strSql = "";
            strSql = "SELECT COALESCE(a.stock_tot_qty,0) " +
          "FROM inv.inv_stock_daily_bals a " +
          "WHERE(to_timestamp(a.bals_date,'YYYY-MM-DD') <=  to_timestamp('" + balsDate +
          "','YYYY-MM-DD') and a.stock_id = " + stockID +
          ") ORDER BY to_timestamp(a.bals_date,'YYYY-MM-DD') DESC LIMIT 1 OFFSET 0";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return double.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0.00;
            }
        }

        public static double getStockLstRsvdBls(long stockID, string balsDate)
        {
            balsDate = DateTime.ParseExact(
         balsDate, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            balsDate = balsDate.Substring(0, 10);
            string strSql = "";
            strSql = "SELECT COALESCE(a.reservations,0) " +
          "FROM inv.inv_stock_daily_bals a " +
          "WHERE(to_timestamp(a.bals_date,'YYYY-MM-DD') <=  to_timestamp('" + balsDate +
          "','YYYY-MM-DD') and a.stock_id = " + stockID +
          ") ORDER BY to_timestamp(a.bals_date,'YYYY-MM-DD') DESC LIMIT 1 OFFSET 0";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return double.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0.00;
            }
        }

        public static double getCsgmtLstAvlblBls(long csgmtID, string balsDate)
        {
            balsDate = DateTime.ParseExact(
         balsDate, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            balsDate = balsDate.Substring(0, 10);
            string strSql = "";
            strSql = "SELECT COALESCE(a.available_balance,0) " +
          "FROM inv.inv_consgmt_daily_bals a " +
          "WHERE(to_timestamp(a.bals_date,'YYYY-MM-DD') <=  to_timestamp('" + balsDate +
          "','YYYY-MM-DD') and a.consgmt_id = " + csgmtID +
          ") ORDER BY to_timestamp(a.bals_date, 'YYYY-MM-DD') DESC LIMIT 1 OFFSET 0";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return double.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0.00;
            }
        }

        public static double getCsgmtLstTotBls(long csgmtID, string balsDate)
        {
            balsDate = DateTime.ParseExact(
         balsDate, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            balsDate = balsDate.Substring(0, 10);
            string strSql = "";
            strSql = "SELECT COALESCE(a.consgmt_tot_qty,0) " +
          "FROM inv.inv_consgmt_daily_bals a " +
          "WHERE(to_timestamp(a.bals_date,'YYYY-MM-DD') <=  to_timestamp('" + balsDate +
          "','YYYY-MM-DD') and a.consgmt_id = " + csgmtID +
          ") ORDER BY to_timestamp(a.bals_date,'YYYY-MM-DD') DESC LIMIT 1 OFFSET 0";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return double.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0.00;
            }
        }

        public static double getCsgmtLstRsvdBls(long csgmtID, string balsDate)
        {
            balsDate = DateTime.ParseExact(
         balsDate, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            balsDate = balsDate.Substring(0, 10);
            string strSql = "";
            strSql = "SELECT COALESCE(a.reservations,0) " +
          "FROM inv.inv_consgmt_daily_bals a " +
          "WHERE(to_timestamp(a.bals_date,'YYYY-MM-DD') <=  to_timestamp('" + balsDate +
          "','YYYY-MM-DD') and a.consgmt_id = " + csgmtID +
          ") ORDER BY to_timestamp(a.bals_date,'YYYY-MM-DD') DESC LIMIT 1 OFFSET 0";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return double.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0.00;
            }
        }

        public static long getCsgmtDailyBalsID(long csgmtID, string balsDate)
        {
            balsDate = DateTime.ParseExact(
         balsDate, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            balsDate = balsDate.Substring(0, 10);
            string strSql = "";
            strSql = "SELECT a.bal_id " +
          "FROM inv.inv_consgmt_daily_bals a " +
          "WHERE(to_timestamp(a.bals_date,'YYYY-MM-DD') =  to_timestamp('" + balsDate +
          "','YYYY-MM-DD') and a.consgmt_id = " + csgmtID + ")";
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

        public static long getStockDailyBalsID(long stockID, string balsDate)
        {
            balsDate = DateTime.ParseExact(
         balsDate, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            balsDate = balsDate.Substring(0, 10);
            string strSql = "";
            strSql = "SELECT a.bal_id " +
          "FROM inv.inv_stock_daily_bals a " +
          "WHERE(to_timestamp(a.bals_date,'YYYY-MM-DD') =  to_timestamp('" + balsDate +
          "','YYYY-MM-DD') and a.stock_id = " + stockID + ")";
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

        public static long getItemStockID(long itmID, long storeID)
        {
            string strSql = "";
            strSql = "SELECT a.stock_id " +
          "FROM inv.inv_stock a " +
          "WHERE(a.itm_id = " + itmID + " and a.subinv_id = " + storeID + ")";
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

        public static void postCnsgnmntQty(long csgmtID,
        double totQty, double rsvdQty, double avblQty,
        string trnsDate, string src_trsID)
        {
            long dailybalID = Global.getCsgmtDailyBalsID(csgmtID, trnsDate);
            //Get dailybalid for accnt on this date
            //if doesn't exist get last accnt bals be4 this date
            //add new amount to it and insert record
            if (dailybalID <= 0)
            {
                double lstTotBals = Global.getCsgmtLstTotBls(csgmtID, trnsDate);
                double lstRsvdBals = Global.getCsgmtLstRsvdBls(csgmtID, trnsDate);
                double lstAvblBals = Global.getCsgmtLstAvlblBls(csgmtID, trnsDate);
                Global.createCnsgmtDailyBals(csgmtID, lstTotBals, lstRsvdBals, lstAvblBals, trnsDate);
                Global.updtCnsgmtDailyBals(csgmtID, totQty,
                  rsvdQty, avblQty, trnsDate, "Do", src_trsID);
            }
            else
            {
                Global.updtCnsgmtDailyBals(csgmtID, totQty,
                  rsvdQty, avblQty, trnsDate, "Do", src_trsID);
            }

            //Global.updateItemBalances(getItmCodeFrmCnsgmntID(csgmtID), totQty);
        }

        public static void postStockQty(long stockID,
      double totQty, double rsvdQty, double avblQty,
      string trnsDate, string src_trsID)
        {
            long dailybalID = Global.getStockDailyBalsID(stockID, trnsDate);
            //Get dailybalid for accnt on this date
            //if doesn't exist get last accnt bals be4 this date
            //add new amount to it and insert record
            if (dailybalID <= 0)
            {
                double lstTotBals = Global.getStockLstTotBls(stockID, trnsDate);
                double lstRsvdBals = Global.getStockLstRsvdBls(stockID, trnsDate);
                double lstAvblBals = Global.getStockLstAvlblBls(stockID, trnsDate);
                Global.createStckDailyBals(stockID, lstTotBals, lstRsvdBals, lstAvblBals, trnsDate);
                Global.updtStckDailyBals(stockID, totQty,
                  rsvdQty, avblQty, trnsDate, "Do", src_trsID);
            }
            else
            {
                Global.updtStckDailyBals(stockID, totQty,
                  rsvdQty, avblQty, trnsDate, "Do", src_trsID);
            }

            //Global.updateItemBalances(getItmCodeFrmStckID(stockID), totQty);
        }

        public static bool hsTrnsUptdStockBls(string srctrnsid,
      string trnsdate, long stockID)
        {
            trnsdate = DateTime.ParseExact(
              trnsdate, "dd-MMM-yyyy HH:mm:ss",
              System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            if (trnsdate.Length > 10)
            {
                trnsdate = trnsdate.Substring(0, 10);
            }

            string strSql = "SELECT a.bal_id FROM inv.inv_stock_daily_bals a " +
              "WHERE a.stock_id = " + stockID +
              " and a.bals_date = '" + trnsdate + "' and a.source_trns_ids like '%," + srctrnsid + ",%'";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public static string getStockBlsTrnsDte(string srctrnsid,
      string trnsdate, long stockID)
        {
            //trnsdate = DateTime.ParseExact(
            //  trnsdate, "dd-MMM-yyyy HH:mm:ss",
            //  System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            //if (trnsdate.Length > 10)
            //{
            //  trnsdate = trnsdate.Substring(0, 10);
            //}

            string strSql = "SELECT to_char(to_timestamp(a.bals_date,'YYYY-MM-DD'),'DD-Mon-YYYY 00:00:00') FROM inv.inv_stock_daily_bals a " +
              "WHERE a.stock_id = " + stockID +
              " and a.source_trns_ids like '%," + srctrnsid + ",%' ORDER BY a.bals_date DESC";
            // and a.bals_date = '" + trnsdate + "' 
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return dtst.Tables[0].Rows[0][0].ToString();
            }
            return "";
        }

        public static string getCsgmntBlsTrnsDte(string srctrnsid,
      string trnsdate, long csgnmtID)
        {
            //trnsdate = DateTime.ParseExact(
            //       trnsdate, "dd-MMM-yyyy HH:mm:ss",
            //       System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

            //if (trnsdate.Length > 10)
            //{
            //  trnsdate = trnsdate.Substring(0, 10);
            //}

            string strSql = "SELECT to_char(to_timestamp(a.bals_date,'YYYY-MM-DD'),'DD-Mon-YYYY 00:00:00') FROM inv.inv_consgmt_daily_bals a " +
              "WHERE a.consgmt_id = " + csgnmtID +
              " and a.source_trns_ids like '%," + srctrnsid + ",%' ORDER BY a.bals_date DESC";
            //and a.bals_date = '" + trnsdate + "'
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return dtst.Tables[0].Rows[0][0].ToString();
            }
            return "";
        }

        public static bool hsTrnsUptdCsgmntBls(string srctrnsid,
      string trnsdate, long csgnmtID)
        {
            trnsdate = DateTime.ParseExact(
                   trnsdate, "dd-MMM-yyyy HH:mm:ss",
                   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

            if (trnsdate.Length > 10)
            {
                trnsdate = trnsdate.Substring(0, 10);
            }

            string strSql = "SELECT a.bal_id FROM inv.inv_consgmt_daily_bals a " +
              "WHERE a.consgmt_id = " + csgnmtID +
              " and a.bals_date = '" + trnsdate + "' and a.source_trns_ids like '%," + srctrnsid + ",%'";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public static void undoPostCnsgnmntQty(long csgmtID,
       double totQty, double rsvdQty, double avblQty,
       string trnsDate, string src_trsID)
        {
            long dailybalID = Global.getCsgmtDailyBalsID(csgmtID, trnsDate);
            //Get dailybalid for accnt on this date
            //if doesn't exist get last accnt bals be4 this date
            //add new amount to it and insert record
            if (dailybalID <= 0)
            {
                //double lstTotBals = Global.getCsgmtLstTotBls(csgmtID, trnsDate);
                //double lstRsvdBals = Global.getCsgmtLstRsvdBls(csgmtID, trnsDate);
                //double lstAvblBals = Global.getCsgmtLstAvlblBls(csgmtID, trnsDate);
                //Global.createCnsgmtDailyBals(csgmtID, lstTotBals, lstRsvdBals, lstAvblBals, trnsDate);
                //Global.updtCnsgmtDailyBals(csgmtID, totQty,
                //  rsvdQty, avblQty, trnsDate, "Do", src_trsID);
            }
            else
            {
                Global.updtCnsgmtDailyBals(csgmtID, totQty,
                  rsvdQty, avblQty, trnsDate, "Undo", src_trsID);
            }
        }

        public static void undoPostStockQty(long stockID,
      double totQty, double rsvdQty, double avblQty,
      string trnsDate, string src_trsID)
        {
            long dailybalID = Global.getStockDailyBalsID(stockID, trnsDate);
            //Get dailybalid for accnt on this date
            //if doesn't exist get last accnt bals be4 this date
            //add new amount to it and insert record
            if (dailybalID <= 0)
            {
                //double lstTotBals = Global.getStockLstTotBls(stockID, trnsDate);
                //double lstRsvdBals = Global.getStockLstRsvdBls(stockID, trnsDate);
                //double lstAvblBals = Global.getStockLstAvlblBls(stockID, trnsDate);
                //Global.createStckDailyBals(stockID, lstTotBals, lstRsvdBals, lstAvblBals, trnsDate);
                //Global.updtStckDailyBals(stockID, totQty,
                //  rsvdQty, avblQty, trnsDate, "Do", src_trsID);
            }
            else
            {
                Global.updtStckDailyBals(stockID, totQty,
                  rsvdQty, avblQty, trnsDate, "Undo", src_trsID);
            }
        }

        public static void refreshRqrdVrbls()
        {
            Global.mnFrm.cmCde.DefaultPrvldgs = Global.dfltPrvldgs;
            //Global.mnFrm.cmCde.Login_number = Global.myInv.login_number;
            Global.mnFrm.cmCde.ModuleAdtTbl = Global.myInv.full_audit_trail_tbl_name;
            Global.mnFrm.cmCde.ModuleDesc = Global.myInv.mdl_description;
            Global.mnFrm.cmCde.ModuleName = Global.myInv.name;
            //Global.mnFrm.cmCde.pgSqlConn = Global.myInv.Host.globalSQLConn;
            //Global.mnFrm.cmCde.Role_Set_IDs = Global.myInv.role_set_id;
            Global.mnFrm.cmCde.SampleRole = "Stores And Inventory Manager Administrator";
            //Global.mnFrm.cmCde.User_id = Global.myInv.user_id;
            //Global.mnFrm.cmCde.Org_id = Global.myInv.org_id;
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            Global.myInv.user_id = Global.mnFrm.usr_id;
            Global.myInv.login_number = Global.mnFrm.lgn_num;
            Global.myInv.role_set_id = Global.mnFrm.role_st_id;
            Global.myInv.org_id = Global.mnFrm.Og_id;

        }

        public static void createRqrdLOVs()
        {
            string[] sysLovs = { "Cash Accounts", "Inventory/Asset Accounts", "Contra Expense Accounts",
      "Contra Revenue Accounts","Customer Classifications","Supplier Classifications",
        "Tax Codes","Discount Codes", "Extra Charges", "Approved Requisitions",
        "Suppliers", "Customer/Supplier Sites","Users' Sales Stores","Approved Pro-Forma Invoices",
        "Approved Sales Orders","Approved Internal Item Requests",
        "Customers","Approved Sales Invoices/Item Issues", "Customer Names for Reports","Supplier Names for Reports"};
            string[] sysLovsDesc = { "Cash Accounts", "Inventory/Asset Accounts", "Contra Expense Accounts",
      "Contra Revenue Accounts","Customer Classifications","Supplier Classifications",
        "Tax Codes","Discount Codes","Extra Charges","Approved Requisitions",
        "Suppliers", "Customer/Supplier Sites", "Users' Sales Stores","Approved Pro-Forma Invoices",
        "Approved Sales Orders","Approved Internal Item Requests",
        "Customers", "Approved Sales Invoices/Item Issues", "Customer Names for Reports","Supplier Names for Reports"};
            string[] sysLovsDynQrys = { "", "",
        "select distinct trim(to_char(accnt_id,'999999999999999999999999999999')) a, accnt_name b, '' c, org_id d, accnt_num e from accb.accb_chart_of_accnts where (accnt_type = 'EX' and is_prnt_accnt = '0' and is_enabled = '1' and is_contra = '1') order by accnt_num",
        "select distinct trim(to_char(accnt_id,'999999999999999999999999999999')) a, accnt_name b, '' c, org_id d, accnt_num e from accb.accb_chart_of_accnts where (accnt_type = 'R' and is_prnt_accnt = '0' and is_enabled = '1' and is_contra = '1') order by accnt_num",
        "", "",
        "select distinct trim(to_char(code_id,'999999999999999999999999999999')) a, code_name b, '' c, org_id d from scm.scm_tax_codes where (itm_type = 'Tax' and is_enabled = '1') order by code_name",
        "select distinct trim(to_char(code_id,'999999999999999999999999999999')) a, code_name b, '' c, org_id d from scm.scm_tax_codes where (itm_type = 'Discount' and is_enabled = '1') order by code_name",
        "select distinct trim(to_char(code_id,'999999999999999999999999999999')) a, code_name b, '' c, org_id d from scm.scm_tax_codes where (itm_type = 'Extra Charge' and is_enabled = '1') order by code_name",
        "select distinct trim(to_char(y.prchs_doc_hdr_id,'999999999999999999999999999999')) a, y.purchase_doc_num b, '' c, y.org_id d, y.prchs_doc_hdr_id g " +
        "from scm.scm_prchs_docs_hdr y, scm.scm_prchs_docs_det z " +
        "where (y.purchase_doc_type = 'Purchase Requisition' " +
        "and y.approval_status = 'Approved' " +
        "and z.prchs_doc_hdr_id = y.prchs_doc_hdr_id and (z.quantity - z.rqstd_qty_ordrd)>0) order by y.prchs_doc_hdr_id DESC",
        "select distinct trim(to_char(cust_sup_id,'999999999999999999999999999999')) a, cust_sup_name b, '' c, org_id d from scm.scm_cstmr_suplr where (cust_or_sup = 'Supplier') order by 2",
        "select distinct trim(to_char(cust_sup_site_id,'999999999999999999999999999999')) a, site_name b, '' c, cust_supplier_id d from scm.scm_cstmr_suplr_sites order by 2",
        "select distinct trim(to_char(y.subinv_id,'999999999999999999999999999999')) a, y.subinv_name b, '' c, y.org_id d, trim(to_char(z.user_id,'999999999999999999999999999999')) e from inv.inv_itm_subinventories y, inv.inv_user_subinventories z where y.subinv_id=z.subinv_id and y.allow_sales = '1' order by 2",
        "select distinct trim(to_char(y.invc_hdr_id,'999999999999999999999999999999')) a, y.invc_number b, '' c, y.org_id d, y.invc_hdr_id g " +
        "from scm.scm_sales_invc_hdr y, scm.scm_sales_invc_det z " +
        "where (y.invc_type = 'Pro-Forma Invoice' " +
        "and y.approval_status = 'Approved' " +
        "and z.invc_hdr_id = y.invc_hdr_id and (z.doc_qty - z.qty_trnsctd_in_dest_doc)>0) order by y.invc_hdr_id DESC",
        "select distinct trim(to_char(y.invc_hdr_id,'999999999999999999999999999999')) a, y.invc_number b, '' c, y.org_id d, y.invc_hdr_id g " +
        "from scm.scm_sales_invc_hdr y, scm.scm_sales_invc_det z " +
        "where (y.invc_type = 'Sales Order' " +
        "and y.approval_status = 'Approved' " +
        "and z.invc_hdr_id = y.invc_hdr_id and (z.doc_qty - z.qty_trnsctd_in_dest_doc)>0) order by y.invc_hdr_id DESC",
        "select distinct trim(to_char(y.invc_hdr_id,'999999999999999999999999999999')) a, y.invc_number b, '' c, y.org_id d, y.invc_hdr_id g " +
        "from scm.scm_sales_invc_hdr y, scm.scm_sales_invc_det z " +
        "where (y.invc_type = 'Internal Item Request' " +
        "and y.approval_status = 'Approved' " +
        "and z.invc_hdr_id = y.invc_hdr_id and (z.doc_qty - z.qty_trnsctd_in_dest_doc)>0) order by y.invc_hdr_id DESC",
        "select distinct trim(to_char(cust_sup_id,'999999999999999999999999999999')) a, cust_sup_name b, '' c, org_id d from scm.scm_cstmr_suplr where (cust_or_sup ilike '%Customer%') order by 2",
        "select distinct trim(to_char(y.invc_hdr_id,'999999999999999999999999999999')) a, y.invc_number b, '' c, y.org_id d, y.invc_hdr_id g " +
        "from scm.scm_sales_invc_hdr y, scm.scm_sales_invc_det z " +
        "where ((y.invc_type = 'Item Issue-Unbilled' or y.invc_type = 'Sales Invoice') " +
        "and (y.approval_status = 'Approved') " +
        "and (z.invc_hdr_id = y.invc_hdr_id) and ((z.doc_qty - z.qty_trnsctd_in_dest_doc)>0)) order by y.invc_hdr_id DESC",
        "select distinct cust_sup_name a, cust_sup_name b, '' c, org_id d from scm.scm_cstmr_suplr where (cust_or_sup ilike '%Customer%') order by 2",
        "select distinct cust_sup_name a, cust_sup_name b, '' c, org_id d from scm.scm_cstmr_suplr where (cust_or_sup ilike '%Supplier%') order by 2"
        };
            string[] pssblVals = {
        "4", "Retail Customer", "Retail Customer"
           ,"4", "Wholesale customer", "Wholesale customer",
        "4", "Individual", "Individual Person"
           ,"4", "Organisation", "Company/Organisation",
        "5", "Service Provider", "Service Provider"
           ,"5", "Goods Provider", "Goods Provider",
        "5", "Service and Goods Provider", "Service and Goods Provider"
           ,"5", "Consultant", "Consultant"
      ,"5", "Training Provider", "Training Provider"};

            Global.mnFrm.cmCde.createSysLovs(sysLovs, sysLovsDynQrys, sysLovsDesc);
            Global.mnFrm.cmCde.createSysLovsPssblVals(sysLovs, pssblVals);

            string[] sysLovs1 = { "Cash Accounts", "Inventory/Asset Accounts", "Contra Expense Accounts",
      "Contra Revenue Accounts","Customer Classifications","Supplier Classifications",
        "Tax Codes","Discount Codes", "Extra Charges", "Approved Requisitions","Suppliers", "Supplier Sites",
          "Shelves","Categories","Stores", "Item Templates","Purchase Orders","Items Stores","Consignment Conditions",
          "Receipt Return Reasons", "Unit Of Measures", "Store Shelves", "Inventory Items"};
            string[] sysLovsDesc1 = { "Cash Accounts", "Inventory/Asset Accounts", "Contra Expense Accounts",
      "Contra Revenue Accounts","Customer Classifications","Supplier Classifications",
        "Tax Codes","Discount Codes","Extra Charges","Approved Requisitions","Suppliers", "Supplier Sites",
          "Shelves","Categories","Stores","Item Templates","Purchase Orders","Items Stores","Consignment Conditions",
          "Receipt Return Reasons", "Unit Of Measures", "Store Shelves", "Inventory Items"};
            string[] sysLovsDynQrys1 = { "", "",
        "select distinct trim(to_char(accnt_id,'999999999999999999999999999999')) a, accnt_name b, '' c, org_id d, accnt_num e from accb.accb_chart_of_accnts where (accnt_type = 'EX' and is_prnt_accnt = '0' and is_enabled = '1' and is_contra = '1') order by accnt_num",
        "select distinct trim(to_char(accnt_id,'999999999999999999999999999999')) a, accnt_name b, '' c, org_id d, accnt_num e from accb.accb_chart_of_accnts where (accnt_type = 'R' and is_prnt_accnt = '0' and is_enabled = '1' and is_contra = '1') order by accnt_num",
        "", "",
        "select distinct trim(to_char(code_id,'999999999999999999999999999999')) a, code_name b, '' c, org_id d from scm.scm_tax_codes where (itm_type = 'Tax' and is_enabled = '1') order by code_name",
        "select distinct trim(to_char(code_id,'999999999999999999999999999999')) a, code_name b, '' c, org_id d from scm.scm_tax_codes where (itm_type = 'Discount' and is_enabled = '1') order by code_name",
        "select distinct trim(to_char(code_id,'999999999999999999999999999999')) a, code_name b, '' c, org_id d from scm.scm_tax_codes where (itm_type = 'Extra Charge' and is_enabled = '1') order by code_name",
        "select distinct trim(to_char(prchs_doc_hdr_id,'999999999999999999999999999999')) a, purchase_doc_num b, '' c, org_id d from scm.scm_prchs_docs_hdr where (purchase_doc_type = 'Purchase Requisition' and approval_status = 'Approved') order by purchase_doc_num DESC",
        "select distinct trim(to_char(cust_sup_id,'999999999999999999999999999999')) a, cust_sup_name b, '' c, org_id d from scm.scm_cstmr_suplr where (cust_or_sup ilike '%Supplier%') order by 2",
        "select distinct trim(to_char(cust_sup_site_id,'999999999999999999999999999999')) a, site_name b, '' c, cust_supplier_id d from scm.scm_cstmr_suplr_sites order by 2",
          "",
          "select distinct trim(to_char(cat_id,'999999999999999999999999999999')) a, cat_name b, '' c, org_id d from inv.inv_product_categories where (enabled_flag = '1') order by cat_name",
          "select distinct trim(to_char(subinv_id,'999999999999999999999999999999')) a, subinv_name b, '' c, org_id d from inv.inv_itm_subinventories where (enabled_flag = '1') order by subinv_name",
          "select distinct trim(to_char(item_type_id,'999999999999999999999999999999')) a, item_type_name b, '' c, org_id d from inv.inv_itm_type_templates where (is_tmplt_enabled_flag = '1') order by item_type_name",
          "select distinct trim(to_char(prchs_doc_hdr_id,'999999999999999999999999999999')) a, purchase_doc_num b, '' c, org_id d from scm.scm_prchs_docs_hdr where approval_status = 'Approved' order by purchase_doc_num",
          "select distinct trim(to_char(y.subinv_id,'999999999999999999999999999999')) a, y.subinv_name b, '' c, y.org_id d, trim(to_char(z.itm_id,'999999999999999999999999999999')) e from inv.inv_itm_subinventories y, inv.inv_stock z " +
              " where y.subinv_id = z.subinv_id and to_date(z.start_date,'YYYY-MM-DD') <= now()::Date and (to_date(z.end_date,'YYYY-MM-DD') >= now()::Date or end_date = '')  order by 2",
          "","",
          "select distinct trim(to_char(uom_id,'999999999999999999999999999999')) a, uom_name b, '' c, org_id d from inv.unit_of_measure where (enabled_flag = '1') order by uom_name",
          "select distinct trim(to_char(y.shelf_id,'999999999999999999999999999999')) a, (SELECT pssbl_value ||' ('||pssbl_value_desc||') ' FROM gst.gen_stp_lov_values " +
          "WHERE pssbl_value_id = y.shelf_id) b, '' c, store_id d from inv.inv_shelf y order by 1",
          "SELECT distinct trim(to_char(item_id,'999999999999999999999999999999')) a, item_desc || '(' || item_code || ')' b, '' c, org_id d FROM inv.inv_itm_list order by 2"};

            string[] pssblVals1 = {
        "4", "Retail Customer", "Retail Customer"
           ,"4", "Wholesale customer", "Wholesale customer",
        "4", "Individual", "Individual Person"
           ,"4", "Organisation", "Company/Organisation",
        "5", "Service Provider", "Service Provider"
           ,"5", "Goods Provider", "Goods Provider",
        "5", "Service and Goods Provider", "Service and Goods Provider"
           ,"5", "Consultant", "Consultant"
      ,"5", "Training Provider", "Training Provider",
          "12", "Shelf 1A", "First Floor shelf A"
           ,"12", "Shelf 1B", "First Floor shelf B",
        "12", "Shelf 1C", "First Floor shelf C"
           ,"12", "Shelf 2A", "Second Floor shelf A",
        "12", "Shelf 2B", "Second Floor shelf B"
           ,"12", "Shelf 2C", "Second Floor shelf C",
       "12", "Shelf 3A", "Third Floor shelf A"
           ,"12", "Shelf 3B", "Third Floor shelf B"
        ,"12", "Shelf 3C", "Third Floor shelf C"
          ,"18", "Excellent", "In Execellent Condition"
          ,"18", "Very Good", "In Very Good Condition"
          ,"18", "Good", "In Good Condition"
          ,"18", "Bad", "In Poor Condition"
          ,"18", "Defective", "Defective"
          ,"19", "Expired", "Expired"
          ,"19", "Defective", "Defective"
          ,"19", "Malfunctioning", "Malfunctioning"
          ,"19", "Wrong Receipt", "Wrong Receipt"
          ,"19", "Over Receipt", "Over Receipt"};

            Global.mnFrm.cmCde.createSysLovs(sysLovs1, sysLovsDynQrys1, sysLovsDesc1);
            Global.mnFrm.cmCde.createSysLovsPssblVals(sysLovs1, pssblVals1);
        }

        public static DataSet fillDataSetFxn(string selSQL)
        {
            //Global.mnFrm.cmCde.showSQLNoPermsn(selSQL);
            //NpgsqlCommand selectCustomer = new NpgsqlCommand(selSQL, Global.myInv.Host.globalSQLConn);

            //DataSet ds = new DataSet();

            //NpgsqlDataAdapter selCustAdp = new NpgsqlDataAdapter();

            //selCustAdp.SelectCommand = selectCustomer;

            //selectCustomer.ExecuteNonQuery();

            //selCustAdp.Fill(ds, "cust_table");

            return Global.mnFrm.cmCde.selectDataNoParams(selSQL);
        }

        public static void validateIntegerTextField(System.Windows.Forms.TextBox fieldInput)
        {
            string varFieldData = fieldInput.Text.Trim();

            //variable for text output
            int num;

            //parse the input string
            bool isNum = int.TryParse(varFieldData, out num);

            if (!isNum)
            {
                fieldInput.Text = "";
            }
        }

        public static void validateDoubleTextField(System.Windows.Forms.TextBox fieldInput)
        {
            string varFieldData = fieldInput.Text.Trim();

            if (varFieldData.Contains(","))
            {
                fieldInput.Text = "";
            }

            //variable for text output
            double num;

            //parse the input string
            bool isNum = double.TryParse(varFieldData, out num);

            if (!isNum)
            {
                fieldInput.Text = "";
            }
        }

        public static int checkControlsContent(Control c)
        {
            if (c.Text == "")
            {
                return 0;
            }
            else
            {
                return int.Parse(c.Text);
            }
        }

        public static int checkStringValue(string parString)
        {
            if (parString == "")
            {
                return 0;
            }
            else
            {
                return int.Parse(parString);
            }
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

        #endregion
        #endregion
    }
}
