using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CommonCode
{
    public partial class addPymntDiag : Form
    {
        #region "GLOBAL VARIABLES..."
        public CommonCodes cmnCde = new CommonCodes();
        public string[] dfltPrvldgs = { "View Accounting","View Chart of Accounts", 
    /*2*/"View Account Transactions", "View Transactions Search",
    /*4*/"View/Generate Trial Balance", "View/Generate Profit & Loss Statement", 
    /*6*/"View/Generate Balance Sheet","View Budgets",
		/*8*/"View Transaction Templates", "View Record History", "View SQL",
    /*11*/"Add Chart of Accounts", "Edit Chart of Accounts", "Delete Chart of Accounts",
    /*14*/"Add Batch for Transactions","Edit Batch for Transactions","Void/Delete Batch for Transactions",
    /*17*/"Add Transactions Directly", "Edit Transactions","Delete Transactions",
    /*20*/"Add Transactions Using Template","Post Transactions",
    /*22*/"Add Budgets","Edit Budgets","Delete Budgets",
    /*25*/"Add Transaction Templates","Edit Transaction Templates","Delete Transaction Templates",
    /*28*/"View Only Self-Created Transaction Batches",
    /*29*/"View Financial Statements","View Accounting Periods","View Payables",
    /*32*/"View Receivables","View Customers/Suppliers","View Tax Codes",
    /*35*/"View Default Accounts","View Account Reconciliation",
    /*37*/"Add Accounting Periods","Edit Accounting Periods", "Delete Accounting Periods",
    /*40*/"View Fixed Assets","View Payments",
    /*42*/"Add Payment Methods", "Edit Payment Methods","Delete Payment Methods",
    /*45*/"Add Supplier Standard Payments", "Edit Supplier Standard Payments","Delete Supplier Standard Payments",
    /*48*/"Add Supplier Advance Payments", "Edit Supplier Advance Payments","Delete Supplier Advance Payments", 
    /*51*/"Setup Exchange Rates", "Setup Document Templates","Review/Approve Payables Documents","Review/Approve Receivables Documents",
    /*55*/"Add Direct Refund from Supplier", "Edit Direct Refund from Supplier","Delete Direct Refund from Supplier",
    /*58*/"Add Supplier Credit Memo (InDirect Refund)", "Edit Supplier Credit Memo (InDirect Refund)","Delete Supplier Credit Memo (InDirect Refund)",
    /*61*/"Add Direct Topup for Supplier", "Edit Direct Topup for Supplier","Delete Direct Topup for Supplier",
    /*64*/"Add Supplier Debit Memo (InDirect Topup)", "Edit Supplier Debit Memo (InDirect Topup)", "Delete Supplier Debit Memo (InDirect Topup)",
    /*67*/"Cancel Payables Documents", "Cancel Receivables Documents",
    /*69*/"Reject Payables Documents", "Reject Receivables Documents",
    /*71*/"Pay Payables Documents", "Pay Receivables Documents",
    /*73*/"Add Customer Standard Payments", "Edit Customer Standard Payments","Delete Customer Standard Payments",
    /*76*/"Add Customer Advance Payments", "Edit Customer Advance Payments","Delete Customer Advance Payments", 
    /*79*/"Add Direct Refund to Customer", "Edit Direct Refund to Customer","Delete Direct Refund to Customer",
    /*82*/"Add Customer Credit Memo (InDirect Topup)", "Edit Customer Credit Memo (InDirect Topup)","Delete Customer Credit Memo (InDirect Topup)",
    /*85*/"Add Direct Topup from Customer", "Edit Direct Topup from Customer","Delete Direct Topup from Customer",
    /*88*/"Add Customer Debit Memo (InDirect Refund)", "Edit Customer Debit Memo (InDirect Refund)", "Delete Customer Debit Memo (InDirect Refund)",
    };

        public int orgid = -1;
        public long batchid = -1;
        public int curid = -1;
        public string curCode = "";
        public long msPyID = -1;
        string docNum = "";
        string docStatus = "";
        string docDesc = "";

        public bool txtChngd = false;
        public int entrdCurrID = -1;
        public double amntToPay = 0;
        public int pymntMthdID = -1;
        public string docTypes = "";
        public string srcDocType = "";
        public long srcDocID = -1;
        long lnkdDocID = -1;
        public long spplrID = -1;
        public bool prcsngPay = false;
        public long spplrSiteID = -1;
        //public bool isrvrsal = false;
        public bool dsablPayments = false;
        public bool createPrepay = false;

        public long orgnlPymntID = -1;
        public long orgnlPymntBatchID = -1;
        public long orgnlGLBatchID = -1;
        //public long[] trnsIDS;
        int dfltRcvblAcntID = -1;
        int dfltLbltyAccnt = -1;
        string pymntsGvn_SQL = "";
        public bool obey_evnts = false;
        #endregion

        #region "RECEIVABLES..."
        public long getNewRcvblsLnID()
        {
            //string strSql = "select nextval('accb.accb_trnsctn_batches_batch_id_seq'::regclass);";
            string strSql = "select nextval('accb.accb_rcvbl_amnt_smmrys_rcvbl_smmry_id_seq')";
            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public string getLtstRcvblsIDNoInPrfx(string prfxTxt)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select count(rcvbls_invc_hdr_id) from accb.accb_rcvbls_invc_hdr WHERE org_id=" +
              cmnCde.Org_id + " and rcvbls_invc_number ilike '" + prfxTxt.Replace("'", "''") + "%'";
            dtSt = cmnCde.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return (long.Parse(dtSt.Tables[0].Rows[0][0].ToString()) + 1).ToString().PadLeft(4, '0');
            }
            else
            {
                return "0001";
            }
        }

        public void createRcvblsDocHdr(int orgid, string docDte, string docNum,
        string docType, string docDesc, long srcDocHdrID, long cstmrID, long cstmrSiteID,
          string apprvlStatus, string nxtApprvlActn, double invcAmnt, string pymntTrms,
          string srcDocType, int pymntMthdID, double amntPaid, long glBtchID,
          string cstmrDocNum, string docTmpltClsftn, int currID, double amntAppld, int dfltRcvblAcntID,
          long advcPayIfoDocId, string advcPayIfoDocTyp)
        {
            string dateStr = cmnCde.getDB_Date_time();
            docDte = DateTime.ParseExact(docDte, "dd-MMM-yyyy",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            string insSQL = @"INSERT INTO accb.accb_rcvbls_invc_hdr(
            rcvbls_invc_date, created_by, creation_date, 
            last_update_by, last_update_date, rcvbls_invc_number, rcvbls_invc_type, 
            comments_desc, src_doc_hdr_id, customer_id, customer_site_id, 
            approval_status, next_aproval_action, org_id, invoice_amount, 
            payment_terms, src_doc_type, pymny_method_id, amnt_paid, gl_batch_id, 
            cstmrs_doc_num, doc_tmplt_clsfctn, invc_curr_id, invc_amnt_appld_elswhr, 
            balancing_accnt_id, advc_pay_ifo_doc_id, advc_pay_ifo_doc_typ) " +
                  "VALUES ('" + docDte.Replace("'", "''") +
                  "', " + cmnCde.User_id + ", '" + dateStr +
                  "', " + cmnCde.User_id + ", '" + dateStr +
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
                  "', " + currID + ", " + amntAppld + ", " + dfltRcvblAcntID +
                  ", " + advcPayIfoDocId + ", '" + advcPayIfoDocTyp.Replace("'", "''") +
                  "')";
            cmnCde.insertDataNoParams(insSQL);
        }

        public void updtRcvblsDocHdr(long hdrID, string docDte, string docNum,
        string docType, string docDesc, long srcDocHdrID, int spplrID, int spplrSiteID,
          string apprvlStatus, string nxtApprvlActn, double invcAmnt, string pymntTrms,
          string srcDocType, int pymntMthdID, double amntPaid, long glBtchID,
          string spplrInvcNum, string docTmpltClsftn, int currID, double amntAppld)
        {
            cmnCde.Extra_Adt_Trl_Info = "";
            docDte = DateTime.ParseExact(docDte, "dd-MMM-yyyy",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

            string dateStr = cmnCde.getDB_Date_time();
            string insSQL = @"UPDATE accb.accb_rcvbls_invc_hdr
       SET rcvbls_invc_date='" + docDte.Replace("'", "''") +
                  "', last_update_by=" + cmnCde.User_id +
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
                  " WHERE rcvbls_invc_hdr_id = " + hdrID;
            cmnCde.updateDataNoParams(insSQL);
        }

        public void createRcvblsDocDet(long smmryID, long hdrID, string lineType, string lineDesc,
          double entrdAmnt, int entrdCurrID, int codeBhnd, string docType,
          bool autoCalc, string incrDcrs1, int costngID, string incrDcrs2, int blncgAccntID,
          long prepayDocHdrID, string vldyStatus, long orgnlLnID,
          int funcCurrID, int accntCurrID, double funcCurrRate, double accntCurrRate,
          double funcCurrAmnt, double accntCurrAmnt)
        {

            string dateStr = cmnCde.getDB_Date_time();
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
                  ", " + cmnCde.User_id + ", '" + dateStr +
                  "', " + cmnCde.User_id + ", '" + dateStr +
                  "', '" + cmnCde.cnvrtBoolToBitStr(autoCalc) +
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
            cmnCde.insertDataNoParams(insSQL);
        }

        public void updtRcvblsDocDet(long docDetID, long hdrID, string lineType, string lineDesc,
          double entrdAmnt, int entrdCurrID, int codeBhnd, string docType,
          bool autoCalc, string incrDcrs1, int costngID, string incrDcrs2, int blncgAccntID,
          long prepayDocHdrID, string vldyStatus, long orgnlLnID,
          int funcCurrID, int accntCurrID, double funcCurrRate, double accntCurrRate,
          double funcCurrAmnt, double accntCurrAmnt)
        {
            cmnCde.Extra_Adt_Trl_Info = "";
            string dateStr = cmnCde.getDB_Date_time();
            string insSQL = @"UPDATE accb.accb_rcvbl_amnt_smmrys
   SET rcvbl_smmry_type='" + lineType.Replace("'", "''") +
                  "', rcvbl_smmry_desc='" + lineDesc.Replace("'", "''") +
                  "', rcvbl_smmry_amnt=" + entrdAmnt +
                  ", code_id_behind=" + codeBhnd +
                  ", src_rcvbl_type='" + docType.Replace("'", "''") +
                  "', src_rcvbl_hdr_id=" + hdrID +
                  ", last_update_by=" + cmnCde.User_id +
                  ", last_update_date='" + dateStr +
                  "', auto_calc='" + cmnCde.cnvrtBoolToBitStr(autoCalc) +
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
            cmnCde.updateDataNoParams(insSQL);
        }

        public void createPyblsDocHdr(int orgid, string docDte, string docNum,
      string docType, string docDesc, long srcDocHdrID, long spplrID, long spplrSiteID,
        string apprvlStatus, string nxtApprvlActn, double invcAmnt, string pymntTrms,
        string srcDocType, int pymntMthdID, double amntPaid, long glBtchID,
        string spplrInvcNum, string docTmpltClsftn, int currID, double amntAppld,
        long rgstrID, string costCtgry, string evntType, int dfltPyblAcntID,
          long advcPayIfoDocId, string advcPayIfoDocTyp)
        {
            string dateStr = cmnCde.getDB_Date_time();
            docDte = DateTime.ParseExact(docDte, "dd-MMM-yyyy",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            string insSQL = @"INSERT INTO accb.accb_pybls_invc_hdr(
            pybls_invc_date, created_by, creation_date, 
            last_update_by, last_update_date, pybls_invc_number, pybls_invc_type, 
            comments_desc, src_doc_hdr_id, supplier_id, supplier_site_id, 
            approval_status, next_aproval_action, org_id, invoice_amount, 
            payment_terms, src_doc_type, pymny_method_id, amnt_paid, gl_batch_id, 
            spplrs_invc_num, doc_tmplt_clsfctn, invc_curr_id, invc_amnt_appld_elswhr,
            event_rgstr_id, evnt_cost_category, event_doc_type, balancing_accnt_id,
            advc_pay_ifo_doc_id, advc_pay_ifo_doc_typ) " +
                  "VALUES ('" + docDte.Replace("'", "''") +
                  "', " + cmnCde.User_id + ", '" + dateStr +
                  "', " + cmnCde.User_id + ", '" + dateStr +
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
                  ", '" + costCtgry.Replace("'", "''") + "', '" + evntType.Replace("'", "''") +
                  "', " + dfltPyblAcntID +
                  ", " + advcPayIfoDocId + ", '" + advcPayIfoDocTyp.Replace("'", "''") + "')";
            cmnCde.insertDataNoParams(insSQL);
        }

        public void updtPyblsDocHdr(long hdrID, string docDte, string docNum,
        string docType, string docDesc, long srcDocHdrID, int spplrID, int spplrSiteID,
          string apprvlStatus, string nxtApprvlActn, double invcAmnt, string pymntTrms,
          string srcDocType, int pymntMthdID, double amntPaid, long glBtchID,
          string spplrInvcNum, string docTmpltClsftn, int currID, double amntAppld,
          long rgstrID, string costCtgry, string evntType)
        {
            cmnCde.Extra_Adt_Trl_Info = "";
            docDte = DateTime.ParseExact(docDte, "dd-MMM-yyyy",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

            string dateStr = cmnCde.getDB_Date_time();
            string insSQL = @"UPDATE accb.accb_pybls_invc_hdr
       SET pybls_invc_date='" + docDte.Replace("'", "''") +
                  "', last_update_by=" + cmnCde.User_id +
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
            cmnCde.updateDataNoParams(insSQL);
        }

        public void createPyblsDocDet(long smmryID, long hdrID, string lineType, string lineDesc,
          double entrdAmnt, int entrdCurrID, int codeBhnd, string docType,
          bool autoCalc, string incrDcrs1, int costngID, string incrDcrs2, int blncgAccntID,
          long prepayDocHdrID, string vldyStatus, long orgnlLnID,
          int funcCurrID, int accntCurrID, double funcCurrRate, double accntCurrRate,
          double funcCurrAmnt, double accntCurrAmnt)
        {

            string dateStr = cmnCde.getDB_Date_time();
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
                  ", " + cmnCde.User_id + ", '" + dateStr +
                  "', " + cmnCde.User_id + ", '" + dateStr +
                  "', '" + cmnCde.cnvrtBoolToBitStr(autoCalc) +
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
            //cmnCde.showSQLNoPermsn(insSQL);
            cmnCde.insertDataNoParams(insSQL);
        }

        public void updtPyblsDocDet(long docDetID, long hdrID, string lineType, string lineDesc,
          double entrdAmnt, int entrdCurrID, int codeBhnd, string docType,
          bool autoCalc, string incrDcrs1, int costngID, string incrDcrs2, int blncgAccntID,
          long prepayDocHdrID, string vldyStatus, long orgnlLnID,
          int funcCurrID, int accntCurrID, double funcCurrRate, double accntCurrRate,
          double funcCurrAmnt, double accntCurrAmnt)
        {
            cmnCde.Extra_Adt_Trl_Info = "";
            string dateStr = cmnCde.getDB_Date_time();
            string insSQL = @"UPDATE accb.accb_pybls_amnt_smmrys
   SET pybls_smmry_type='" + lineType.Replace("'", "''") +
                  "', pybls_smmry_desc='" + lineDesc.Replace("'", "''") +
                  "', pybls_smmry_amnt=" + entrdAmnt +
                  ", code_id_behind=" + codeBhnd +
                  ", src_pybls_type='" + docType.Replace("'", "''") +
                  "', src_pybls_hdr_id=" + hdrID +
                  ", last_update_by=" + cmnCde.User_id +
                  ", last_update_date='" + dateStr +
                  "', auto_calc='" + cmnCde.cnvrtBoolToBitStr(autoCalc) +
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
            cmnCde.updateDataNoParams(insSQL);
        }

        public DataSet get_RcvblsDocDet(long docHdrID)
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
            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            //this.rcvblsFrm.recDt_SQL = strSql;
            return dtst;
        }

        public bool isTaxWthHldng(int codeID)
        {
            string strSql = "Select scm.istaxwthhldng(" + codeID + ")";
            DataSet dtst = cmnCde.selectDataNoParams(strSql);

            if (dtst.Tables[0].Rows.Count > 0)
            {
                if (dtst.Tables[0].Rows[0][0].ToString() == "1")
                {
                    return true;
                }
            }
            return false;
        }

        public double getRcvblsDocGrndAmnt(long dochdrID)
        {
            string strSql = @"select SUM(CASE WHEN y.rcvbl_smmry_type = '3Discount' 
or scm.istaxwthhldng(y.code_id_behind)='1' or y.rcvbl_smmry_type='5Applied Prepayment'
      THEN -1*y.rcvbl_smmry_amnt ELSE y.rcvbl_smmry_amnt END) amnt " +
              "from accb.accb_rcvbl_amnt_smmrys y " +
              "where y.src_rcvbl_hdr_id = " + dochdrID +
              " and y.rcvbl_smmry_type IN ('1Initial Amount','2Tax','3Discount','4Extra Charge','5Applied Prepayment')";
            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            double rs = 0;

            if (dtst.Tables[0].Rows.Count > 0)
            {
                double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rs);
            }
            return rs;
        }

        public int getRcvblsPrepayDocCnt(long dochdrID)
        {
            string strSql = @"select count(appld_prepymnt_doc_id) " +
              "from accb.accb_rcvbl_amnt_smmrys y " +
              "where y.src_rcvbl_hdr_id = " + dochdrID + " and y.appld_prepymnt_doc_id >0 " +
              "Group by y.appld_prepymnt_doc_id having count(y.appld_prepymnt_doc_id)>1";
            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            int rs = 0;

            if (dtst.Tables[0].Rows.Count > 0)
            {
                int.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rs);
                return rs;
            }
            return 0;
        }

        public bool isRcvblPrepayDocValid(long dochdrID, int crncyID, long cstmrID)
        {
            string strSql = @"select rcvbls_invc_hdr_id " +
              "from accb.accb_rcvbls_invc_hdr y " +
              "where y.rcvbls_invc_hdr_id = " + dochdrID +
              " and y.customer_id =" + cstmrID +
              " and y.invc_curr_id = " + crncyID;
            DataSet dtst = cmnCde.selectDataNoParams(strSql);

            if (dtst.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public double getRcvblsDocFuncAmnt(long dochdrID)
        {
            string strSql = @"select SUM(CASE WHEN y.rcvbl_smmry_type='3Discount' 
or scm.istaxwthhldng(y.code_id_behind)='1' or y.rcvbl_smmry_type='5Applied Prepayment'
      THEN -1*y.func_curr_amount ELSE y.func_curr_amount END) amnt " +
              "from accb.accb_rcvbl_amnt_smmrys y " +
              "where y.src_rcvbl_hdr_id=" + dochdrID +
              " and y.rcvbl_smmry_type IN ('1Initial Amount','2Tax','3Discount','4Extra Charge','5Applied Prepayment')";
            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            double rs = 0;

            if (dtst.Tables[0].Rows.Count > 0)
            {
                double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rs);
            }
            return rs;
        }

        public double getRcvblsDocAccntAmnt(long dochdrID)
        {
            string strSql = @"select SUM(CASE WHEN y.rcvbl_smmry_type='3Discount' 
or scm.istaxwthhldng(y.code_id_behind)='1' or y.rcvbl_smmry_type='5Applied Prepayment'
      THEN -1*y.accnt_curr_amnt ELSE y.accnt_curr_amnt END) amnt " +
              "from accb.accb_rcvbl_amnt_smmrys y " +
              "where y.src_rcvbl_hdr_id=" + dochdrID +
              " and y.rcvbl_smmry_type IN ('1Initial Amount','2Tax','3Discount','4Extra Charge','5Applied Prepayment')";
            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            double rs = 0;

            if (dtst.Tables[0].Rows.Count > 0)
            {
                double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rs);
            }
            return rs;
        }

        public long getRcvblsSmmryItmID(string smmryType, int codeBhnd,
          long srcDocID, string srcDocTyp, string smmryNm)
        {
            string strSql = "select y.rcvbl_smmry_id " +
              "from accb.accb_rcvbl_amnt_smmrys y " +
              "where y.rcvbl_smmry_type= '" + smmryType + "' and y.rcvbl_smmry_desc = '" + smmryNm +
              "' and y.code_id_behind= " + codeBhnd +
              " and y.src_rcvbl_type='" + srcDocTyp.Replace("'", "''") +
              "' and y.src_rcvbl_hdr_id=" + srcDocID + " ";
            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public void updtRcvblsDocApprvl(long docid,
      string apprvlSts, string nxtApprvl)
        {
            string extrCls = "";

            if (apprvlSts == "Cancelled")
            {
                extrCls = ", invoice_amount=0, invc_amnt_appld_elswhr=0";
            }
            cmnCde.Extra_Adt_Trl_Info = "";
            string dateStr = cmnCde.getDB_Date_time();
            string updtSQL = "UPDATE accb.accb_rcvbls_invc_hdr SET " +
                  "approval_status='" + apprvlSts.Replace("'", "''") +
                  "', last_update_by=" + cmnCde.User_id +
                  ", last_update_date='" + dateStr +
                  "', next_aproval_action='" + nxtApprvl.Replace("'", "''") +
                  "'" + extrCls + " WHERE (rcvbls_invc_hdr_id = " +
                  docid + ")";
            cmnCde.updateDataNoParams(updtSQL);
        }

        public void updtPyblsDocApprvl(long docid,
      string apprvlSts, string nxtApprvl)
        {
            string extrCls = "";
            if (apprvlSts == "Cancelled")
            {
                extrCls = ", invoice_amount=0, invc_amnt_appld_elswhr=0";
            }
            cmnCde.Extra_Adt_Trl_Info = "";
            string dateStr = cmnCde.getDB_Date_time();
            string updtSQL = "UPDATE accb.accb_pybls_invc_hdr SET " +
                  "approval_status='" + apprvlSts.Replace("'", "''") +
                  "', last_update_by=" + cmnCde.User_id +
                  ", last_update_date='" + dateStr +
                  "', next_aproval_action='" + nxtApprvl.Replace("'", "''") +
                  "'" + extrCls + " WHERE (pybls_invc_hdr_id = " +
                  docid + ")";
            cmnCde.updateDataNoParams(updtSQL);
        }

        public void updtRcvblsDocGLBatch(long docid,
      long glBatchID)
        {
            cmnCde.Extra_Adt_Trl_Info = "";
            string dateStr = cmnCde.getDB_Date_time();
            string updtSQL = "UPDATE accb.accb_rcvbls_invc_hdr SET " +
                  "gl_batch_id=" + glBatchID +
                  ", last_update_by=" + cmnCde.User_id +
                  ", last_update_date='" + dateStr +
                  "' WHERE (rcvbls_invc_hdr_id = " +
                  docid + ")";
            cmnCde.updateDataNoParams(updtSQL);
        }

        public double getRcvblsDocTtlPymnts(long dochdrID, string docType)
        {
            string strSql = "select SUM(y.amount_paid) amnt " +
              "from accb.accb_payments y " +
              "where y.src_doc_id = " + dochdrID + " and y.src_doc_typ = '" + docType.Replace("'", "''") + "'";
            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            double rs = 0;

            if (dtst.Tables[0].Rows.Count > 0)
            {
                double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rs);
            }
            return rs;
        }
        #endregion

        #region "PAYMENTS DONE..."
        public DataSet get_DocSmryLns(long dochdrID, string docTyp)
        {
            string strSql = "SELECT a.rcvbl_smmry_id, a.rcvbl_smmry_desc, " +
             "CASE WHEN substr(a.rcvbl_smmry_type,1,1) IN ('3','5') THEN -1 * a.rcvbl_smmry_amnt ELSE a.rcvbl_smmry_amnt END, a.code_id_behind, a.rcvbl_smmry_type, a.auto_calc " +
             "FROM accb.accb_rcvbl_amnt_smmrys a " +
             "WHERE((a.src_rcvbl_hdr_id = " + dochdrID +
             ") and (a.src_rcvbl_type='" + docTyp + "') and (substr(a.rcvbl_smmry_type,1,1) NOT IN ('6','7','8'))) ORDER BY a.rcvbl_smmry_type";
            DataSet dtst = cmnCde.selectDataNoParams(strSql);

            return dtst;
        }

        public DataSet get_PyblsDocSmryLns(long dochdrID, string docTyp)
        {
            string strSql = "SELECT a.pybls_smmry_id, a.pybls_smmry_desc, " +
             "CASE WHEN substr(a.pybls_smmry_type,1,1) IN ('3','5') THEN -1 * a.pybls_smmry_amnt ELSE a.pybls_smmry_amnt END, a.code_id_behind, a.pybls_smmry_type, a.auto_calc " +
             "FROM accb.accb_pybls_amnt_smmrys a " +
             "WHERE((a.src_pybls_hdr_id = " + dochdrID +
             ") and (a.src_pybls_type='" + docTyp +
             "') and (substr(a.pybls_smmry_type,1,1) NOT IN ('6','7','8'))) ORDER BY a.pybls_smmry_type";
            DataSet dtst = cmnCde.selectDataNoParams(strSql);

            return dtst;
        }

        public bool isPymntRvrsdB4(long orgnlPymntID)
        {
            string strSql = "";
            strSql = "SELECT a.pymnt_id FROM accb.accb_payments a " +
             "WHERE(a.orgnl_pymnt_id = " + orgnlPymntID + ") " +
             "ORDER BY a.pymnt_id LIMIT 1 " +
               " OFFSET 0";
            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public DataSet get_LastRcvblPay_Trns(long pymntID)
        {
            string strSql = "";
            strSql = @"SELECT a.pymnt_id, accb.get_pymnt_mthd_name(a.pymnt_mthd_id), 
      a.amount_paid, a.change_or_balance, a.pymnt_remark, 
           a.src_doc_typ, a.src_doc_id, a.created_by, 
      to_char(to_timestamp(a.pymnt_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), 
b.rcvbls_invc_number, c.user_name, a.entrd_curr_id " +
             "FROM accb.accb_payments a, accb.accb_rcvbls_invc_hdr b, sec.sec_users c " +
             "WHERE(a.pymnt_id = " + pymntID +
             " and a.src_doc_id = b.rcvbls_invc_hdr_id and a.created_by = c.user_id) " +
             "ORDER BY to_timestamp(a.pymnt_date,'YYYY-MM-DD HH24:MI:SS') DESC, a.pymnt_id DESC LIMIT 1 " +
               " OFFSET 0";
            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            return dtst;
        }

        public DataSet get_LastPyblPay_Trns(long pymntID)
        {
            string strSql = "";
            strSql = @"SELECT a.pymnt_id, accb.get_pymnt_mthd_name(a.pymnt_mthd_id), 
      a.amount_paid, a.change_or_balance, a.pymnt_remark, 
           a.src_doc_typ, a.src_doc_id, a.created_by, 
      to_char(to_timestamp(a.pymnt_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), 
b.pybls_invc_number, c.user_name, a.entrd_curr_id " +
             "FROM accb.accb_payments a, accb.accb_pybls_invc_hdr b, sec.sec_users c " +
             "WHERE(a.pymnt_id = " + pymntID +
             " and a.src_doc_id = b.pybls_invc_hdr_id and a.created_by = c.user_id) " +
             "ORDER BY to_timestamp(a.pymnt_date,'YYYY-MM-DD HH24:MI:SS') DESC, a.pymnt_id DESC LIMIT 1 " +
               " OFFSET 0";
            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            return dtst;
        }

        public DataSet get_Pay_Trns(string searchWord, string docTypes)
        {
            string strSql = "";
            string whereCls = "";
            if (docTypes == "Customer Payments")
            {
                whereCls = " and a.src_doc_typ ilike '%Customer%' and ((accb.get_src_doc_num(a.src_doc_id, a.src_doc_typ) ilike '" + searchWord.Replace("'", "''") +
                  @"') or (a.src_doc_id IN (select c.rcvbls_invc_hdr_id from accb.accb_rcvbls_invc_hdr c where (((c.rcvbls_invc_type = 'Customer Advance Payment' and (c.invoice_amount-c.amnt_paid)<=0) 
or c.rcvbls_invc_type = 'Customer Debit Memo (InDirect Refund)') and approval_status='Approved' 
and (c.invoice_amount-c.invc_amnt_appld_elswhr)>0) and (c.customer_id=" + this.spplrID +
             " and c.invc_curr_id=" + this.entrdCurrID +
              "))))";
            }
            else
            {
                whereCls = " and a.src_doc_typ ilike '%Supplier%' and ((accb.get_src_doc_num(a.src_doc_id, a.src_doc_typ) ilike '" + searchWord.Replace("'", "''") +
            @"') or (a.src_doc_id IN (select c.pybls_invc_hdr_id from accb.accb_pybls_invc_hdr c where (((c.pybls_invc_type = 'Supplier Advance Payment' and (c.invoice_amount-c.amnt_paid)<=0) 
or c.pybls_invc_type = 'Supplier Credit Memo (InDirect Refund)') and approval_status='Approved' 
and (c.invoice_amount-c.invc_amnt_appld_elswhr)>0) and (c.supplier_id=" + this.spplrID +
        " and c.invc_curr_id=" + this.entrdCurrID +
        "))))";
            }
            /*dte1 = DateTime.ParseExact(
           dte1, "dd-MMM-yyyy HH:mm:ss",
           System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

              dte2 = DateTime.ParseExact(
           dte2, "dd-MMM-yyyy HH:mm:ss",
           System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

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
              }*/
            strSql = @"SELECT a.pymnt_id, a.pymnt_mthd_id, accb.get_pymnt_mthd_name(a.pymnt_mthd_id), 
      a.amount_paid, a.change_or_balance, a.pymnt_remark, 
      a.src_doc_typ, a.src_doc_id, accb.get_src_doc_num(a.src_doc_id, a.src_doc_typ), 
      a.created_by, to_char(to_timestamp(a.pymnt_date, 'YYYY-MM-DD HH24:MI:SS'), 'DD-Mon-YYYY HH24:MI:SS'), 
      sec.get_usr_name(a.created_by), gl_batch_id, accb.get_gl_batch_name(gl_batch_id), 
b.pymnt_batch_name, a.pymnt_batch_id,a.prepay_doc_id, accb.get_src_doc_num(a.prepay_doc_id, a.prepay_doc_type),
a.pay_means_other_info, a.cheque_card_name, a.expiry_date, a.cheque_card_num, a.sign_code, a.bkgrd_actvty_status, a.bkgrd_actvty_gen_doc_name " +
             "FROM accb.accb_payments a, accb.accb_payments_batches b " +
             "WHERE((a.pymnt_batch_id = b.pymnt_batch_id)" + whereCls +
             ") ORDER BY a.pymnt_id DESC";
            /*and (to_timestamp(a.pymnt_date,'YYYY-MM-DD HH24:MI:SS') between to_timestamp('" + dte1 +
             "','YYYY-MM-DD HH24:MI:SS') AND to_timestamp('" + dte2 + "','YYYY-MM-DD HH24:MI:SS'))) " +
             "ORDER BY a.pymnt_id DESC LIMIT " + limit_size +
               " OFFSET " + (Math.Abs(offset * limit_size)).ToString();*/
            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            this.pymntsGvn_SQL = strSql;
            return dtst;
        }

        public long get_Total_Trns(string searchWord, string searchIn,
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
            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            long sumRes = 0;
            if (dtst.Tables[0].Rows.Count > 0)
            {
                long.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out sumRes);
            }
            return sumRes;
        }
        #endregion

        #region "PAYMENTS..."
        public long getNewPymntBatchID()
        {
            //string strSql = "select nextval('accb.accb_trnsctn_batches_batch_id_seq'::regclass);";
            string strSql = "select  last_value from accb.accb_payments_batches_pymnt_batch_id_seq";
            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString()) + 1;
            }
            return -1;
        }

        public string dbtOrCrdtAccnt(int accntid, string incrsDcrse)
        {
            string accntType = cmnCde.getAccntType(accntid);
            string isContra = cmnCde.isAccntContra(accntid);
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
            string accntType = cmnCde.getAccntType(accntid);
            string isContra = cmnCde.isAccntContra(accntid);
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

        public string incrsOrDcrsAccnt(int accntid, string dbtOrCrdt)
        {
            string accntType = cmnCde.getAccntType(accntid);
            string isContra = cmnCde.isAccntContra(accntid);
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

        public double get_LtstExchRate(string fromCurr, string toCurr, string asAtDte)
        {
            int funccurid = cmnCde.getOrgFuncCurID(cmnCde.Org_id);
            string funccurCode = cmnCde.getPssblValNm(funccurid);
            string strSql = "";
            strSql = @"SELECT CASE WHEN a.currency_from='" + fromCurr.Replace("'", "''") +
              @"' THEN a.multiply_from_by ELSE (1/a.multiply_from_by) END
      FROM accb.accb_exchange_rates a WHERE ((a.currency_from='" + fromCurr.Replace("'", "''") +
              @"' and a.currency_to='" + toCurr.Replace("'", "''") +
              @"') or (a.currency_to='" + fromCurr.Replace("'", "''") +
              @"' and a.currency_from='" + toCurr.Replace("'", "''") +
              @"')) and to_timestamp(a.conversion_date,'YYYY-MM-DD') <= to_timestamp('" + asAtDte +
              "','DD-Mon-YYYY HH24:MI:SS') ORDER BY to_timestamp(a.conversion_date,'YYYY-MM-DD') DESC LIMIT 1 OFFSET 0";
            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return double.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            if (fromCurr == toCurr)
            {
                return 1;
            }
            else if (fromCurr != funccurCode && toCurr != funccurCode)
            {
                double a = this.get_LtstExchRate(fromCurr, funccurCode, asAtDte);
                double b = this.get_LtstExchRate(toCurr, funccurCode, asAtDte);
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

        public double get_LtstExchRate(int fromCurrID, int toCurrID, string asAtDte)
        {
            int fnccurid = cmnCde.getOrgFuncCurID(cmnCde.Org_id);
            //this.curCode = cmnCde.getPssblValNm(this.curid);
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
            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return double.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            if (fromCurrID != fnccurid && toCurrID != fnccurid)
            {
                double a = this.get_LtstExchRate(fromCurrID, fnccurid, asAtDte);
                double b = this.get_LtstExchRate(toCurrID, fnccurid, asAtDte);
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

        public DataSet get_Batch_dateSums(long batchID)
        {
            string strSql = "";
            strSql = @"SELECT substring(a.trnsctn_date from 1 for 10), SUM(a.dbt_amount), SUM(a.crdt_amount) 
    FROM accb.accb_trnsctn_details a
    WHERE(a.batch_id = " + batchID + @") 
    GROUP BY substring(a.trnsctn_date from 1 for 10) 
    HAVING SUM(a.dbt_amount) != SUM(a.crdt_amount)
    ORDER BY 1";

            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            //this.mnFrm.trnsDet_SQL = strSql;
            return dtst;
        }

        public double get_Batch_DbtSum(long batchID)
        {
            string strSql = "";
            double sumRes = 0.00;
            strSql = "SELECT SUM(a.dbt_amount)" +
          "FROM accb.accb_trnsctn_details a " +
          "WHERE(a.batch_id = " + batchID + ")";

            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out sumRes);
            }
            return sumRes;
        }

        public double get_Batch_CrdtSum(long batchID)
        {
            string strSql = "";
            strSql = "SELECT SUM(a.crdt_amount)" +
          "FROM accb.accb_trnsctn_details a " +
          "WHERE(a.batch_id = " + batchID + ")";

            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            double sumRes = 0.00;
            if (dtst.Tables[0].Rows.Count > 0)
            {
                double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out sumRes);
            }
            return sumRes;
        }

        public long getBatchID(string batchname, int orgid)
        {
            string strSql = "";
            strSql = "SELECT a.batch_id " +
         "FROM accb.accb_trnsctn_batches a " +
            "WHERE ((a.batch_name ilike '" + batchname.Replace("'", "''") +
              "') AND (a.org_id = " + orgid + "))";

            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public long getNewTrnsID()
        {
            //string strSql = "select nextval('accb.accb_trnsctn_batches_batch_id_seq'::regclass);";
            string strSql = "select nextval('accb.accb_trnsctn_details_transctn_id_seq')";
            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }
        public long getNewBatchID()
        {
            //string strSql = "select nextval('accb.accb_trnsctn_batches_batch_id_seq'::regclass);";
            string strSql = "select  last_value from accb.accb_trnsctn_batches_batch_id_seq";
            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString()) + 1;
            }
            return -1;
        }
        public long getNewPymntLnID()
        {
            //string strSql = "select nextval('accb.accb_trnsctn_batches_batch_id_seq'::regclass);";
            string strSql = "select nextval('accb.accb_payments_pymnt_id_seq')";
            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public void updtPyblsDocAmntPaid(long docid,
      double amntPaid)
        {
            cmnCde.Extra_Adt_Trl_Info = "";
            string dateStr = cmnCde.getDB_Date_time();
            string updtSQL = "UPDATE accb.accb_pybls_invc_hdr SET " +
                  "amnt_paid=amnt_paid + " + amntPaid +
                  ", last_update_by=" + cmnCde.User_id +
                  ", last_update_date='" + dateStr +
                  "' WHERE (pybls_invc_hdr_id = " +
                  docid + ")";
            cmnCde.updateDataNoParams(updtSQL);
        }

        public void updtPyblsDocAmntAppld(long docid,
      double amntAppld)
        {
            cmnCde.Extra_Adt_Trl_Info = "";
            string dateStr = cmnCde.getDB_Date_time();
            string updtSQL = "UPDATE accb.accb_pybls_invc_hdr SET " +
                  "invc_amnt_appld_elswhr=invc_amnt_appld_elswhr + " + amntAppld +
                  ", last_update_by=" + cmnCde.User_id +
                  ", last_update_date='" + dateStr +
                  "' WHERE (pybls_invc_hdr_id = " +
                  docid + ")";
            cmnCde.updateDataNoParams(updtSQL);
        }

        public void updtRcvblsDocAmntPaid(long docid,
      double amntPaid)
        {
            cmnCde.Extra_Adt_Trl_Info = "";
            string dateStr = cmnCde.getDB_Date_time();
            string updtSQL = "UPDATE accb.accb_rcvbls_invc_hdr SET " +
                  "amnt_paid=amnt_paid + " + amntPaid +
                  ", last_update_by=" + cmnCde.User_id +
                  ", last_update_date='" + dateStr +
                  "' WHERE (rcvbls_invc_hdr_id = " +
                  docid + ")";
            cmnCde.updateDataNoParams(updtSQL);
        }

        public bool shdRcvblsDocBeCancelled(long docid)
        {
            string selSQL = @"SELECT rcvbls_invc_hdr_id       
      FROM accb.accb_rcvbls_invc_hdr  
      WHERE (rcvbls_invc_hdr_id = " +
                  docid + " and (amnt_paid=0 and invc_amnt_appld_elswhr=0))";
            DataSet dtst = cmnCde.selectDataNoParams(selSQL);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void updtRcvblsDocAmntAppld(long docid,
      double amntAppld)
        {
            cmnCde.Extra_Adt_Trl_Info = "";
            string dateStr = cmnCde.getDB_Date_time();
            string updtSQL = "UPDATE accb.accb_rcvbls_invc_hdr SET " +
                  "invc_amnt_appld_elswhr=invc_amnt_appld_elswhr + " + amntAppld +
                  ", last_update_by=" + cmnCde.User_id +
                  ", last_update_date='" + dateStr +
                  "' WHERE (rcvbls_invc_hdr_id = " +
                  docid + ")";
            cmnCde.updateDataNoParams(updtSQL);
        }
        public void updateBatchStatus(long batchid)
        {
            cmnCde.Extra_Adt_Trl_Info = "";
            string dateStr = cmnCde.getDB_Date_time();
            string updtSQL = "UPDATE accb.accb_trnsctn_batches " +
            "SET batch_status='1', avlbl_for_postng='0', last_update_by=" + cmnCde.User_id + ", last_update_date='" + dateStr +
            "' WHERE batch_id = " + batchid;
            cmnCde.updateDataNoParams(updtSQL);
        }

        public void updateBatchAvlblty(long batchid, string avlblty)
        {
            cmnCde.Extra_Adt_Trl_Info = "";
            string dateStr = cmnCde.getDB_Date_time();
            string updtSQL = "UPDATE accb.accb_trnsctn_batches " +
            "SET avlbl_for_postng='" + avlblty.Replace("'", "''") +
            "', last_update_by=" + cmnCde.User_id +
            ", last_update_date='" + dateStr +
            "' WHERE batch_id = " + batchid;
            cmnCde.updateDataNoParams(updtSQL);
        }

        public void updateBatchVldtyStatus(long batchid, string vldty)
        {
            cmnCde.Extra_Adt_Trl_Info = "";
            string dateStr = cmnCde.getDB_Date_time();
            string updtSQL = "UPDATE accb.accb_trnsctn_batches " +
            "SET batch_vldty_status='" + vldty.Replace("'", "''") +
            "', last_update_by=" + cmnCde.User_id +
            ", last_update_date='" + dateStr +
            "' WHERE batch_id = " + batchid;
            cmnCde.updateDataNoParams(updtSQL);
        }

        public void updateBatch(long batchid, string batchname,
         string batchdesc)
        {
            cmnCde.Extra_Adt_Trl_Info = "";
            string dateStr = cmnCde.getDB_Date_time();
            string updtSQL = "UPDATE accb.accb_trnsctn_batches " +
            "SET batch_name='" + batchname.Replace("'", "''") + "', batch_description='" + batchdesc.Replace("'", "''") +
            "', last_update_by=" + cmnCde.User_id + ", last_update_date='" + dateStr +
            "' WHERE batch_id = " + batchid;
            cmnCde.updateDataNoParams(updtSQL);
        }

        public void createTransaction(int accntid, string trnsDesc,
      double dbtAmnt, string trnsDate, int crncyid,
          long batchid, double crdtamnt, double netAmnt,
          double entrdAmt, int entrdCurrID, double acntAmnt, int acntCurrID,
          double funcExchRate, double acntExchRate, string dbtOrCrdt, string CheckNum)
        {
            trnsDate = DateTime.ParseExact(
         trnsDate, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            if (trnsDesc.Length > 500)
            {
                trnsDesc = trnsDesc.Substring(0, 500);
            }
            string dateStr = cmnCde.getDB_Date_time();
            string insSQL = "INSERT INTO accb.accb_trnsctn_details(" +
                              "accnt_id, transaction_desc, dbt_amount, trnsctn_date, " +
                              "func_cur_id, created_by, creation_date, batch_id, crdt_amount, " +
                              @"last_update_by, last_update_date, net_amount, 
            entered_amnt, entered_amt_crncy_id, accnt_crncy_amnt, accnt_crncy_id, 
            func_cur_exchng_rate, accnt_cur_exchng_rate, dbt_or_crdt, ref_doc_number) " +
                              "VALUES (" + accntid + ", '" + trnsDesc.Replace("'", "''") + "', " + dbtAmnt +
                              ", '" + trnsDate + "', " + crncyid + ", " + cmnCde.User_id + ", '" + dateStr +
                              "', " + batchid + ", " + crdtamnt + ", " + cmnCde.User_id +
                              ", '" + dateStr + "'," + netAmnt + ", " + entrdAmt +
                              ", " + entrdCurrID + ", " + acntAmnt +
                              ", " + acntCurrID + ", " + funcExchRate +
                              ", " + acntExchRate + ", '" + dbtOrCrdt + "', '" + CheckNum.Replace("'", "''") + "')";
            cmnCde.insertDataNoParams(insSQL);
        }

        public void createTransaction(long trnsID, int accntid, string trnsDesc,
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
            string dateStr = cmnCde.getDB_Date_time();
            string insSQL = "INSERT INTO accb.accb_trnsctn_details(" +
                              "transctn_id, accnt_id, transaction_desc, dbt_amount, trnsctn_date, " +
                              "func_cur_id, created_by, creation_date, batch_id, crdt_amount, " +
                              @"last_update_by, last_update_date, net_amount, 
            entered_amnt, entered_amt_crncy_id, accnt_crncy_amnt, accnt_crncy_id, 
            func_cur_exchng_rate, accnt_cur_exchng_rate, dbt_or_crdt) " +
                              "VALUES (" + trnsID + "," + accntid + ", '" + trnsDesc.Replace("'", "''") + "', " + dbtAmnt +
                              ", '" + trnsDate + "', " + crncyid + ", " + cmnCde.User_id + ", '" + dateStr +
                              "', " + batchid + ", " + crdtamnt + ", " + cmnCde.User_id +
                              ", '" + dateStr + "'," + netAmnt + ", " + entrdAmt +
                              ", " + entrdCurrID + ", " + acntAmnt +
                              ", " + acntCurrID + ", " + funcExchRate +
                              ", " + acntExchRate + ", '" + dbtOrCrdt + "')";
            cmnCde.insertDataNoParams(insSQL);
        }

        public void createBatch(int orgid, string batchname,
         string batchdesc, string btchsrc, string batchvldty, long srcbatchid, string avlblforPpstng)
        {
            string dateStr = cmnCde.getDB_Date_time();
            string insSQL = "INSERT INTO accb.accb_trnsctn_batches(" +
                              "batch_name, batch_description, created_by, creation_date, " +
                              "org_id, batch_status, last_update_by, last_update_date, " +
            "batch_source, batch_vldty_status, src_batch_id, avlbl_for_postng) " +
                              "VALUES ('" + batchname.Replace("'", "''") + "', '" + batchdesc.Replace("'", "''") +
                              "', " + cmnCde.User_id + ", '" + dateStr +
                              "', " + orgid + ", '0', " + cmnCde.User_id + ", '" + dateStr +
                              "', '" + btchsrc.Replace("'", "''") +
                              "', '" + batchvldty.Replace("'", "''") +
                              "', " + srcbatchid +
                              ",'" + avlblforPpstng + "')";
            cmnCde.insertDataNoParams(insSQL);
        }

        public void createPymntsBatch(int orgid, string strtDte,
          string endDte, string docType,
        string batchName, string batchDesc, long spplrID, int pymntMthdID,
          string batchSource, long orgnlBtchID,
          string vldtyStatus, string docTmpltClsftn, string batchStatus)
        {
            string dateStr = cmnCde.getDB_Date_time();
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
                  "', " + cmnCde.User_id + ", '" + dateStr +
                  "', " + cmnCde.User_id + ", '" + dateStr +
                  "', '" + vldtyStatus.Replace("'", "''") +
                  "', " + orgnlBtchID +
                  ", " + orgid + ", " + spplrID +
                  ")";
            cmnCde.insertDataNoParams(insSQL);
        }

        public void updtPymntsBatchVldty(long batchID, string vldtyStatus)
        {
            cmnCde.Extra_Adt_Trl_Info = "";
            string dateStr = cmnCde.getDB_Date_time();
            string insSQL = @"UPDATE accb.accb_payments_batches SET 
            last_update_by=" + cmnCde.User_id +
                  ", last_update_date='" + dateStr +
                  "', batch_vldty_status='" + vldtyStatus.Replace("'", "''") +
                  "' WHERE pymnt_batch_id = " + batchID;
            cmnCde.updateDataNoParams(insSQL);
        }

        public void updtPymntsLnVldty(long pymtLnID, string vldtyStatus)
        {
            cmnCde.Extra_Adt_Trl_Info = "";
            string dateStr = cmnCde.getDB_Date_time();
            string insSQL = @"UPDATE accb.accb_payments SET 
            last_update_by=" + cmnCde.User_id +
                  ", last_update_date='" + dateStr +
                  "', pymnt_vldty_status='" + vldtyStatus.Replace("'", "''") +
                  "' WHERE pymnt_id = " + pymtLnID;
            cmnCde.updateDataNoParams(insSQL);
        }

        public void updtPymntsBatch(long batchID, string strtDte,
          string endDte, string docType,
        string batchName, string batchDesc, int spplrID, int pymntMthdID,
          string batchSource, long orgnlBtchID,
          string vldtyStatus, string docTmpltClsftn, string batchStatus)
        {
            cmnCde.Extra_Adt_Trl_Info = "";
            string dateStr = cmnCde.getDB_Date_time();
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
                  "', last_update_by=" + cmnCde.User_id +
                  ", last_update_date='" + dateStr +
                  "', batch_vldty_status='" + vldtyStatus.Replace("'", "''") +
                  "', orgnl_batch_id=" + orgnlBtchID +
                  ", cust_spplr_id=" + spplrID +
                  " WHERE pymnt_batch_id = " + batchID;
            cmnCde.updateDataNoParams(insSQL);
        }

        public void createPymntDet(long pymntID, long pymntBatchID, int pymntMthdID,
          double amntPaid, int entrdCurrID, double chnge_bals, string pymntRemark,
          string srcDocType, long srcDocID, string pymntDte,
          string incrDcrs1, int blncgAccntID, string incrDcrs2, int chrgAccntID,
          long glBatchID, string vldyStatus, long orgnlLnID,
          int funcCurrID, int accntCurrID, double funcCurrRate, double accntCurrRate,
          double funcCurrAmnt, double accntCurrAmnt, long prepayDocID, string prepayDocType,
         string otherinfo, string cardNm, string expryDte, string cardNum, string sgnCode, string actvtyStatus,
         string actvtyDocName, long intnlPyTrnsID)
        {
            pymntDte = DateTime.ParseExact(pymntDte, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

            string dateStr = cmnCde.getDB_Date_time();
            string insSQL = @"INSERT INTO accb.accb_payments(
            pymnt_id, pymnt_mthd_id, amount_paid, change_or_balance, pymnt_remark, 
            src_doc_typ, src_doc_id, created_by, creation_date, last_update_by, 
            last_update_date, pymnt_date, incrs_dcrs1, rcvbl_lblty_accnt_id, 
            incrs_dcrs2, cash_or_suspns_acnt_id, gl_batch_id, orgnl_pymnt_id, 
            pymnt_vldty_status, entrd_curr_id, func_curr_id, accnt_curr_id, 
            func_curr_rate, accnt_curr_rate, func_curr_amount, accnt_curr_amnt, 
            pymnt_batch_id, prepay_doc_id, prepay_doc_type, pay_means_other_info, cheque_card_name, 
            expiry_date, cheque_card_num, sign_code, bkgrd_actvty_status, 
            bkgrd_actvty_gen_doc_name, intnl_pay_trns_id) " +
                  "VALUES (" + pymntID + ", " + pymntMthdID + "," + amntPaid + "," + chnge_bals +
                  ",'" + pymntRemark.Replace("'", "''") +
                  "', '" + srcDocType.Replace("'", "''") +
                  "', " + srcDocID +
                  ", " + cmnCde.User_id + ", '" + dateStr +
                  "', " + cmnCde.User_id + ", '" + dateStr +
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
                  ", " + prepayDocID +
                  ", '" + prepayDocType.Replace("'", "''") +
                  "', '" + otherinfo.Replace("'", "''") +
                  "', '" + cardNm.Replace("'", "''") +
                  "', '" + expryDte.Replace("'", "''") +
                  "', '" + cardNum.Replace("'", "''") +
                  "','" + cmnCde.encrypt1(sgnCode, CommonCodes.AppKey).Replace("'", "''") +
                  "', '" + actvtyStatus.Replace("'", "''") +
                  "', '" + actvtyDocName.Replace("'", "''") +
                  "', " + intnlPyTrnsID +
                  ")";

            //cmnCde.showSQLNoPermsn(glBatchName + "/" + glBatchID + "/" + cmnCde.Org_id);
            //cmnCde.showSQLNoPermsn(insSQL);
            cmnCde.insertDataNoParams(insSQL);
        }

        public void updtPymntDet(long pymntID, long pymntBatchID, int pymntMthdID,
          double amntPaid, int entrdCurrID, double chnge_bals, string pymntRemark,
          string srcDocType, long srcDocID, string pymntDte,
          string incrDcrs1, int blncgAccntID, string incrDcrs2, int chrgAccntID,
          long glBatchID, string vldyStatus, long orgnlLnID,
          int funcCurrID, int accntCurrID, double funcCurrRate, double accntCurrRate,
          double funcCurrAmnt, double accntCurrAmnt)
        {
            cmnCde.Extra_Adt_Trl_Info = "";
            pymntDte = DateTime.ParseExact(pymntDte, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

            string dateStr = cmnCde.getDB_Date_time();
            string insSQL = @"UPDATE accb.accb_payments SET 
            pymnt_mthd_id=" + pymntMthdID + ", amount_paid=" + amntPaid +
                  ", change_or_balance=" + chnge_bals +
                  ", pymnt_remark='" + pymntRemark.Replace("'", "''") +
                  "', src_doc_typ='" + srcDocType.Replace("'", "''") +
                  "', src_doc_id=" + srcDocID +
                  ", last_update_by=" + cmnCde.User_id +
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
            cmnCde.updateDataNoParams(insSQL);
        }

        public void deleteBatch(long batchid, string batchNm)
        {
            cmnCde.Extra_Adt_Trl_Info = "Batch Name = " + batchNm;
            string delSql = "DELETE FROM accb.accb_trnsctn_batches WHERE(batch_id = " + batchid + ")";
            cmnCde.deleteDataNoParams(delSql);
            string updtSQL = @"UPDATE accb.accb_trnsctn_batches SET batch_vldty_status='VALID' WHERE batch_id IN (SELECT h.batch_id
  FROM accb.accb_trnsctn_batches h where batch_vldty_status='VOID'
AND NOT EXISTS(Select g.batch_id from accb.accb_trnsctn_batches g where h.batch_id=g.src_batch_id))";
            cmnCde.updateDataNoParams(updtSQL);
        }

        public void deleteBatchTrns(long batchid)
        {
            cmnCde.Extra_Adt_Trl_Info = "";
            string delSql = "DELETE FROM accb.accb_trnsctn_details WHERE(batch_id = " + batchid + ")";
            cmnCde.deleteDataNoParams(delSql);
        }

        public void deletePymntsBatchNDet(long valLnid, string batchName)
        {
            cmnCde.Extra_Adt_Trl_Info = "Batch Name = " + batchName;
            string delSQL = "DELETE FROM accb.accb_payments WHERE pymnt_batch_id = " + valLnid;
            cmnCde.deleteDataNoParams(delSQL);
            delSQL = "DELETE FROM accb.accb_payments_batches WHERE pymnt_batch_id = " + valLnid;
            cmnCde.deleteDataNoParams(delSQL);
        }

        public void deletePymntsDet(long valLnid)
        {
            cmnCde.Extra_Adt_Trl_Info = "";
            string delSQL = "DELETE FROM accb.accb_payments WHERE pymnt_id = " + valLnid;
            cmnCde.deleteDataNoParams(delSQL);
        }

        public double getCstmrDpsts(long cstmrID, long invcurID)
        {
            string selSQL = @"select SUM(invoice_amount-invc_amnt_appld_elswhr) c, customer_id e, 
invc_curr_id f from accb.accb_rcvbls_invc_hdr where (((rcvbls_invc_type = 'Customer Advance Payment' and (invoice_amount-amnt_paid)<=0) 
or rcvbls_invc_type = 'Customer Debit Memo (InDirect Refund)') 
and approval_status='Approved' and (invoice_amount-invc_amnt_appld_elswhr)>0 and customer_id>0 and customer_id = " + cstmrID + " and invc_curr_id = " + invcurID + @") 
GROUP BY customer_id,invc_curr_id";
            DataSet dtst = cmnCde.selectDataNoParams(selSQL);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return double.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return 0;
        }

        public double getSpplrDpsts(long spplrID, long invcurID)
        {
            string selSQL = @"select SUM(invoice_amount-invc_amnt_appld_elswhr) c, supplier_id e, 
invc_curr_id f from accb.accb_pybls_invc_hdr where (((pybls_invc_type = 'Supplier Advance Payment' and (invoice_amount-amnt_paid)<=0) 
or pybls_invc_type = 'Supplier Credit Memo (InDirect Refund)') 
and approval_status='Approved' and (invoice_amount-invc_amnt_appld_elswhr)>0 and supplier_id = " + spplrID + " and invc_curr_id = " + invcurID + @") 
GROUP BY supplier_id,invc_curr_id";
            DataSet dtst = cmnCde.selectDataNoParams(selSQL);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return double.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return 0;
        }

        public DataSet get_One_PymntBatchHdr(long hdrID)
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

            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            //this.pyblsFrm.docTmplt_SQL = strSql;
            return dtst;
        }

        public DataSet get_PymntBatch(string searchWord, string searchIn, long offset,
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

            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            //this.pymntFrm.rec_SQL = strSql;
            return dtst;
        }

        public long get_Total_PymntBatch(string searchWord, string searchIn, long orgID, string startDte, string endDte)
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
              Batch Status
             */
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

            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        public DataSet get_PymntBatchLns(long offset,
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

            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            //this.pymntFrm.recDt_SQL = strSql;
            // cmnCde.showSQLNoPermsn(strSql);
            return dtst;
        }

        public void updtPymntBatchStatus(long docid,
      string batchStatus)
        {
            cmnCde.Extra_Adt_Trl_Info = "";
            string dateStr = cmnCde.getDB_Date_time();
            string updtSQL = "UPDATE accb.accb_payments_batches SET " +
                  "batch_status='" + batchStatus.Replace("'", "''") +
                  "', last_update_by=" + cmnCde.User_id +
                  ", last_update_date='" + dateStr +
                  "' WHERE (pymnt_batch_id = " +
                  docid + ")";
            cmnCde.updateDataNoParams(updtSQL);
        }

        public void updtPymntLnGLBatch(long docid,
      long glBatchID)
        {
            cmnCde.Extra_Adt_Trl_Info = "";
            string dateStr = cmnCde.getDB_Date_time();
            string updtSQL = "UPDATE accb.accb_payments SET " +
                  "gl_batch_id=" + glBatchID +
                  ", last_update_by=" + cmnCde.User_id +
                  ", last_update_date='" + dateStr +
                  "' WHERE (pymnt_id = " +
                  docid + ")";
            cmnCde.updateDataNoParams(updtSQL);
        }

        public int get_DfltRcvblAcnt(int orgID)
        {
            string strSql = "SELECT sales_rcvbl_acnt_id " +
             "FROM scm.scm_dflt_accnts a " +
             "WHERE(a.org_id = " + orgID + ")";

            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public int get_DfltPyblAcnt(int orgID)
        {
            string strSql = "SELECT rcpt_lblty_acnt_id " +
             "FROM scm.scm_dflt_accnts a " +
             "WHERE(a.org_id = " + orgID + ")";

            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public DataSet getPymntMthds(int orgID, string docType)
        {
            string selSQL = @"select 
        distinct trim(to_char(paymnt_mthd_id,'999999999999999999999999999999')) a, 
        pymnt_mthd_name b, '' c, org_id d, supported_doc_type e 
        from accb.accb_paymnt_mthds 
        where is_enabled = '1' and org_id = " + orgID +
              " and supported_doc_type = '" + docType.Replace("'", "''") +
              "' order by pymnt_mthd_name LIMIT 30 OFFSET 0";

            DataSet dtst = cmnCde.selectDataNoParams(selSQL);
            return dtst;
        }

        public int getPyblsDocBlncngAccnt(long srcDocID, string docType)
        {
            string whrcls = @" and (a.pybls_smmry_type !='6Grand Total' and 
a.pybls_smmry_type !='7Total Payments Made' and a.pybls_smmry_type !='8Outstanding Balance')";

            string selSQL = @"select 
        distinct liability_acnt_id, pybls_smmry_id 
        from accb.accb_pybls_amnt_smmrys a 
        where src_pybls_hdr_id = " + srcDocID +
              " and src_pybls_type = '" + docType.Replace("'", "''") +
              "'" + whrcls + " order by pybls_smmry_id LIMIT 1 OFFSET 0";
            //cmnCde.showSQLNoPermsn(selSQL);
            DataSet dtst = cmnCde.selectDataNoParams(selSQL);

            if (dtst.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public int getRcvblsDocBlncngAccnt(long srcDocID, string docType)
        {
            string whrcls = @" and (a.rcvbl_smmry_type !='6Grand Total' and 
a.rcvbl_smmry_type !='7Total Payments Made' and a.rcvbl_smmry_type !='8Outstanding Balance')";

            string selSQL = @"select 
        distinct rcvbl_acnt_id, rcvbl_smmry_id 
        from accb.accb_rcvbl_amnt_smmrys a 
        where src_rcvbl_hdr_id = " + srcDocID +
              " and src_rcvbl_type = '" + docType.Replace("'", "''") +
              "'" + whrcls + " order by rcvbl_smmry_id LIMIT 1 OFFSET 0";
            //cmnCde.showSQLNoPermsn(selSQL);
            DataSet dtst = cmnCde.selectDataNoParams(selSQL);

            if (dtst.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public int getPyblsDocAdvncAccnt(long srcDocID, string docType)
        {
            string whrcls = @" and (a.pybls_smmry_type ='1Initial Amount')";

            string selSQL = @"select 
        distinct asset_expns_acnt_id, pybls_smmry_id 
        from accb.accb_pybls_amnt_smmrys a 
        where src_pybls_hdr_id = " + srcDocID +
              " and src_pybls_type = '" + docType.Replace("'", "''") +
              "'" + whrcls + " order by pybls_smmry_id LIMIT 1 OFFSET 0";
            //cmnCde.showSQLNoPermsn(selSQL);
            DataSet dtst = cmnCde.selectDataNoParams(selSQL);

            if (dtst.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public int getRcvblsDocAdvncAccnt(long srcDocID, string docType)
        {
            string whrcls = @" and (a.rcvbl_smmry_type ='1Initial Amount')";

            string selSQL = @"select 
        distinct rvnu_acnt_id, rcvbl_smmry_id 
        from accb.accb_rcvbl_amnt_smmrys a 
        where src_rcvbl_hdr_id = " + srcDocID +
              " and src_rcvbl_type = '" + docType.Replace("'", "''") +
              "'" + whrcls + " order by rcvbl_smmry_id LIMIT 1 OFFSET 0";
            //cmnCde.showSQLNoPermsn(selSQL);
            DataSet dtst = cmnCde.selectDataNoParams(selSQL);

            if (dtst.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public int getPymntMthdChrgAccnt(long pymntMthdID)
        {
            string selSQL = @"select 
        distinct current_asst_acnt_id, paymnt_mthd_id 
        from accb.accb_paymnt_mthds 
        where paymnt_mthd_id = " + pymntMthdID +
              " order by paymnt_mthd_id LIMIT 1 OFFSET 0";
            DataSet dtst = cmnCde.selectDataNoParams(selSQL);

            if (dtst.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }
        #endregion

        #region "EVENT HANDLERS..."
        public addPymntDiag()
        {
            InitializeComponent();
        }

        private void checkNCreatePyblsHdr()
        {
            long cstmrID = this.spplrID;
            int cstmLblty = -1;
            int cstmRcvbl = -1;

            if (cstmrID > 0)
            {
                cstmLblty = int.Parse(cmnCde.getGnrlRecNm("scm.scm_cstmr_suplr", "cust_sup_id",
                  "dflt_pybl_accnt_id", cstmrID));
                cstmRcvbl = int.Parse(cmnCde.getGnrlRecNm("scm.scm_cstmr_suplr", "cust_sup_id",
                  "dflt_rcvbl_accnt_id", cstmrID));
            }
            if (cstmLblty > 0)
            {
                this.dfltLbltyAccnt = cstmLblty;
            }

            if (cstmRcvbl > 0)
            {
                this.dfltRcvblAcntID = cstmRcvbl;
            }
            //cmnCde.showSQLNoPermsn(cstmLblty + "/" + cstmRcvbl + "/" + this.dfltLbltyAccnt + "/" + this.dfltRcvblAcntID);

            string pyblDocNum = "";
            string pyblDocType = "";
            long pyblHdrID = -1;
            pyblDocNum = "SAP-" +
          DateTime.Parse(cmnCde.getFrmtdDB_Date_time()).ToString("yyMMdd-HHmmss")
                       + "-" + cmnCde.getRandomInt(10, 100);
            pyblDocType = "Supplier Advance Payment";
            this.createPyblsDocHdr(cmnCde.Org_id, this.dteRcvdTextBox.Text.Substring(0, 11),
                pyblDocNum, pyblDocType,
                this.pymntCmmntsTextBox.Text, -1,
                this.spplrID, this.spplrSiteID,
                "Not Validated", "Approve", (double)this.amntPaidNumUpDown.Value,
                "", "",
                this.pymntMthdID, 0, -1,
                "", "Supplier Payment",
                this.entrdCurrID, 0, -1, "", "", this.dfltLbltyAccnt, this.srcDocID, this.srcDocType);
            pyblHdrID = cmnCde.getGnrlRecID("accb.accb_pybls_invc_hdr", "pybls_invc_number", "pybls_invc_hdr_id", pyblDocNum);

            long curlnID = this.getNewPyblsLnID();
            string lineType = "1Initial Amount";
            string lineDesc = "Supplier Prepayment";
            double entrdAmnt = (double)this.amntPaidNumUpDown.Value;
            int codeBhnd = -1;
            string docType = pyblDocType;
            bool autoCalc = false;
            string incrDcrs1 = "Increase";
            int costngID = this.dfltRcvblAcntID;
            string incrDcrs2 = "Increase";
            int blncgAccntID = this.dfltLbltyAccnt;
            long prepayDocHdrID = -1;
            string vldyStatus = "VALID";
            long orgnlLnID = -1;
            int funcCurrID = int.Parse(this.funcCurrIDTextBox.Text);
            int accntCurrID = int.Parse(this.accntCurrIDTextBox.Text);
            double funcCurrRate = (double)this.funcCurRateNumUpDwn.Value;
            double accntCurrRate = (double)this.accntCurRateNumUpDwn.Value;
            double funcCurrAmnt = (double)this.funcCurAmntNumUpDwn.Value;
            double accntCurrAmnt = (double)this.accntCurrNumUpDwn.Value;

            this.createPyblsDocDet(curlnID, pyblHdrID, lineType,
              lineDesc, entrdAmnt, entrdCurrID, codeBhnd, docType, autoCalc, incrDcrs1,
              costngID, incrDcrs2, blncgAccntID, prepayDocHdrID, vldyStatus, orgnlLnID, funcCurrID,
              accntCurrID, funcCurrRate, accntCurrRate, funcCurrAmnt, accntCurrAmnt);

            this.reCalcPyblsSmmrys(pyblHdrID, pyblDocType);
            if (this.approvePyblsDoc(pyblHdrID, pyblDocNum))
            {
                this.updtPyblsDocApprvl(pyblHdrID, "Approved", "Cancel");
            }
            this.srcDocID = pyblHdrID;
            this.srcDocType = pyblDocType;
        }

        public void reCalcPyblsSmmrys(long srcDocID, string srcDocType)
        {
            double grndAmnt = this.getPyblsDocGrndAmnt(srcDocID);
            //Grand Total
            string smmryNm = "Grand Total";
            long smmryID = this.getPyblsSmmryItmID("6Grand Total", -1,
              srcDocID, srcDocType, smmryNm);
            if (smmryID <= 0)
            {
                long curlnID = this.getNewPyblsLnID();
                this.createPyblsDocDet(curlnID, srcDocID, "6Grand Total",
                  smmryNm, grndAmnt, this.entrdCurrID,
                  -1, srcDocType, true, "Increase",
                  -1, "Increase", -1, -1, "VALID", -1, -1,
                  -1, 0, 0, 0, 0);
            }
            else
            {
                this.updtPyblsDocDet(smmryID, srcDocID, "6Grand Total",
                  smmryNm, grndAmnt, this.entrdCurrID,
                  -1, srcDocType, true, "Increase",
                  -1, "Increase", -1, -1, "VALID", -1, -1,
                  -1, 0, 0, 0, 0);
            }

            //7Total Payments Received
            smmryNm = "Total Payments Made";
            smmryID = this.getPyblsSmmryItmID(
              "7Total Payments Made", -1,
              srcDocID, srcDocType, smmryNm);
            double pymntsAmnt = this.getPyblsDocTtlPymnts(srcDocID, srcDocType);

            if (smmryID <= 0)
            {
                long curlnID = this.getNewPyblsLnID();
                this.createPyblsDocDet(curlnID, srcDocID, "7Total Payments Made",
                  smmryNm, pymntsAmnt, this.entrdCurrID,
                  -1, srcDocType, true, "Increase",
                  -1, "Increase", -1, -1, "VALID", -1, -1,
                  -1, 0, 0, 0, 0);
            }
            else
            {
                this.updtPyblsDocDet(smmryID, srcDocID, "7Total Payments Made",
                  smmryNm, pymntsAmnt, this.entrdCurrID,
                  -1, srcDocType, true, "Increase",
                  -1, "Increase", -1, -1, "VALID", -1, -1,
                  -1, 0, 0, 0, 0);
            }
            smmryNm = "Outstanding Balance";
            smmryID = this.getPyblsSmmryItmID("8Outstanding Balance", -1,
              srcDocID, srcDocType, smmryNm);
            double outstndngAmnt = grndAmnt - pymntsAmnt;
            if (smmryID <= 0)
            {
                long curlnID = this.getNewPyblsLnID();
                this.createPyblsDocDet(curlnID, srcDocID, "8Outstanding Balance",
                  smmryNm, outstndngAmnt, this.entrdCurrID,
                  -1, srcDocType, true, "Increase",
                  -1, "Increase", -1, -1, "VALID", -1, -1,
                  -1, 0, 0, 0, 0);
            }
            else
            {
                this.updtPyblsDocDet(smmryID, srcDocID, "8Outstanding Balance",
                  smmryNm, outstndngAmnt, this.entrdCurrID,
                  -1, srcDocType, true, "Increase",
                  -1, "Increase", -1, -1, "VALID", -1, -1,
                  -1, 0, 0, 0, 0);
            }
        }

        public double getPyblsDocTtlPymnts(long dochdrID, string docType)
        {
            string strSql = "select SUM(y.amount_paid) amnt " +
              "from accb.accb_payments y " +
              "where y.src_doc_id = " + dochdrID + " and y.src_doc_typ = '" + docType.Replace("'", "''") + "'";
            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            double rs = 0;

            if (dtst.Tables[0].Rows.Count > 0)
            {
                double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rs);
            }
            return rs;
        }

        public double getPyblsDocGrndAmnt(long dochdrID)
        {
            string strSql = @"select SUM(CASE WHEN y.pybls_smmry_type='3Discount' 
or scm.istaxwthhldng(y.code_id_behind)='1' or y.pybls_smmry_type='5Applied Prepayment'
      THEN -1*y.pybls_smmry_amnt ELSE y.pybls_smmry_amnt END) amnt " +
              "from accb.accb_pybls_amnt_smmrys y " +
              "where y.src_pybls_hdr_id=" + dochdrID +
              " and y.pybls_smmry_type IN ('1Initial Amount','2Tax','3Discount','4Extra Charge','5Applied Prepayment')";
            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            double rs = 0;

            if (dtst.Tables[0].Rows.Count > 0)
            {
                double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rs);
            }
            return rs;
        }

        public double getPyblsDocFuncAmnt(long dochdrID)
        {
            string strSql = @"select SUM(CASE WHEN y.pybls_smmry_type='3Discount' 
or scm.istaxwthhldng(y.code_id_behind)='1' or y.pybls_smmry_type='5Applied Prepayment'
      THEN -1*y.func_curr_amount ELSE y.func_curr_amount END) amnt " +
              "from accb.accb_pybls_amnt_smmrys y " +
              "where y.src_pybls_hdr_id=" + dochdrID +
              " and y.pybls_smmry_type IN ('1Initial Amount','2Tax','3Discount','4Extra Charge','5Applied Prepayment')";
            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            double rs = 0;

            if (dtst.Tables[0].Rows.Count > 0)
            {
                double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rs);
            }
            return rs;
        }

        public double getPyblsDocAccntAmnt(long dochdrID)
        {
            string strSql = @"select SUM(CASE WHEN y.pybls_smmry_type='3Discount' 
or scm.istaxwthhldng(y.code_id_behind)='1' or y.pybls_smmry_type='5Applied Prepayment'
      THEN -1*y.accnt_curr_amnt ELSE y.accnt_curr_amnt END) amnt " +
              "from accb.accb_pybls_amnt_smmrys y " +
              "where y.src_pybls_hdr_id=" + dochdrID +
              " and y.pybls_smmry_type IN ('1Initial Amount','2Tax','3Discount','4Extra Charge','5Applied Prepayment')";
            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            double rs = 0;

            if (dtst.Tables[0].Rows.Count > 0)
            {
                double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rs);
            }
            return rs;
        }

        public long getPyblsSmmryItmID(string smmryType, int codeBhnd,
          long srcDocID, string srcDocTyp, string smmryNm)
        {
            string strSql = "select y.pybls_smmry_id " +
              "from accb.accb_pybls_amnt_smmrys y " +
              "where y.pybls_smmry_type= '" + smmryType + "' and y.pybls_smmry_desc = '" + smmryNm +
              "' and y.code_id_behind= " + codeBhnd +
              " and y.src_pybls_type='" + srcDocTyp.Replace("'", "''") +
              "' and y.src_pybls_hdr_id=" + srcDocID + " ";
            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public long getNewPyblsLnID()
        {
            //string strSql = "select nextval('accb.accb_trnsctn_batches_batch_id_seq'::regclass);";
            string strSql = "select nextval('accb.accb_pybls_amnt_smmrys_pybls_smmry_id_seq')";
            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public DataSet get_PyblsDocDet(long docHdrID)
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
            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            return dtst;
        }

        public bool approvePyblsDoc(long docHdrID, string docNum)
        {
            /* 1. Create a GL Batch and get all doc lines
             * 2. for each line create costing account transaction
             * 3. create one balancing account transaction using the grand total amount
             * 4. Check if created gl_batch is balanced.
             * 5. if balanced update docHdr else delete the gl batch created and throw error message
             */
            try
            {
                string glBatchName = "ACC_PYBL-" +
                 DateTime.Parse(cmnCde.getFrmtdDB_Date_time()).ToString("yyMMdd-HHmmss")
                          + "-" + cmnCde.getRandomInt(10, 100);
                long glBatchID = cmnCde.getGnrlRecID("accb.accb_trnsctn_batches",
                  "batch_name", "batch_id", glBatchName, cmnCde.Org_id);

                if (glBatchID <= 0)
                {
                    this.createBatch(cmnCde.Org_id, glBatchName,
                      this.pymntCmmntsTextBox.Text + " (" + docNum + ")",
                      "Payables Invoice Document", "VALID", -1, "0");
                }
                else
                {
                    cmnCde.showMsg("GL Batch Could not be Created!\r\n Try Again Later!", 0);
                    return false;
                }
                glBatchID = cmnCde.getGnrlRecID("accb.accb_trnsctn_batches",
                  "batch_name", "batch_id", glBatchName, cmnCde.Org_id);
                int pyblAccntID = -1;
                string lnDte = this.dteRcvdTextBox.Text;
                DataSet dtst = this.get_PyblsDocDet(docHdrID);
                for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
                {
                    string lineTypeNm = dtst.Tables[0].Rows[i][1].ToString();
                    int codeBhndID = -1;
                    int.TryParse(dtst.Tables[0].Rows[i][4].ToString(), out codeBhndID);

                    string incrDcrs1 = dtst.Tables[0].Rows[i][6].ToString().Substring(0, 1);
                    int accntID1 = -1;
                    int.TryParse(dtst.Tables[0].Rows[i][7].ToString(), out accntID1);
                    string isdbtCrdt1 = cmnCde.dbtOrCrdtAccnt(accntID1, incrDcrs1.Substring(0, 1));

                    string incrDcrs2 = dtst.Tables[0].Rows[i][8].ToString().Substring(0, 1);
                    int accntID2 = -1;
                    int.TryParse(dtst.Tables[0].Rows[i][9].ToString(), out accntID2);
                    pyblAccntID = accntID2;
                    string isdbtCrdt2 = cmnCde.dbtOrCrdtAccnt(accntID2, incrDcrs2.Substring(0, 1));

                    double lnAmnt = double.Parse(dtst.Tables[0].Rows[i][19].ToString());

                    System.Windows.Forms.Application.DoEvents();

                    double acntAmnt = 0;
                    double.TryParse(dtst.Tables[0].Rows[i][20].ToString(), out acntAmnt);
                    double entrdAmnt = 0;
                    double.TryParse(dtst.Tables[0].Rows[i][3].ToString(), out entrdAmnt);

                    string lneDesc = dtst.Tables[0].Rows[i][2].ToString();
                    int entrdCurrID = int.Parse(dtst.Tables[0].Rows[i][11].ToString());
                    int funcCurrID = int.Parse(dtst.Tables[0].Rows[i][13].ToString());
                    int accntCurrID = int.Parse(dtst.Tables[0].Rows[i][15].ToString());
                    double funcCurrRate = double.Parse(dtst.Tables[0].Rows[i][17].ToString());
                    double accntCurrRate = double.Parse(dtst.Tables[0].Rows[i][18].ToString());

                    if (accntID1 > 0 && (lnAmnt != 0 || acntAmnt != 0) && incrDcrs1 != "" && lneDesc != "")
                    {
                        double netAmnt = (double)this.dbtOrCrdtAccntMultiplier(accntID1, incrDcrs1) * (double)lnAmnt;

                        if (!cmnCde.isTransPrmttd(accntID1, lnDte, netAmnt))
                        {
                            return false;
                        }

                        if (this.dbtOrCrdtAccnt(accntID1,
                          incrDcrs1) == "Debit")
                        {
                            this.createTransaction(accntID1,
                              lneDesc, lnAmnt,
                              lnDte, funcCurrID, glBatchID, 0.00,
                              netAmnt, entrdAmnt, entrdCurrID, acntAmnt, accntCurrID, funcCurrRate, accntCurrRate, "D", "");
                        }
                        else
                        {
                            this.createTransaction(accntID1,
                              lneDesc, 0.00,
                              lnDte, funcCurrID,
                              glBatchID, lnAmnt, netAmnt,
                      entrdAmnt, entrdCurrID, acntAmnt, accntCurrID, funcCurrRate, accntCurrRate, "C", "");
                        }
                    }
                }
                //Liability Balancing Leg

                int accntCurrID1 = int.Parse(cmnCde.getGnrlRecNm(
            "accb.accb_chart_of_accnts", "accnt_id", "crncy_id", pyblAccntID));

                string slctdCurrID = this.entrdCurrID.ToString();
                double funcCurrRate1 = Math.Round(
            this.get_LtstExchRate(int.Parse(slctdCurrID), this.curid, lnDte), 15);
                double accntCurrRate1 = Math.Round(
                  this.get_LtstExchRate(int.Parse(slctdCurrID), accntCurrID1, lnDte), 15);
                System.Windows.Forms.Application.DoEvents();

                double grndAmnt = this.getPyblsDocGrndAmnt(docHdrID);

                double funcCurrAmnt = this.getPyblsDocFuncAmnt(docHdrID);// (funcCurrRate1 * grndAmnt);
                double accntCurrAmnt = (accntCurrRate1 * grndAmnt);
                System.Windows.Forms.Application.DoEvents();

                double netAmnt1 = (double)this.dbtOrCrdtAccntMultiplier(pyblAccntID,
            "I") * (double)funcCurrAmnt;


                if (!cmnCde.isTransPrmttd(pyblAccntID, lnDte, netAmnt1))
                {
                    return false;
                }

                if (this.dbtOrCrdtAccnt(pyblAccntID,
                  "I") == "Debit")
                {
                    this.createTransaction(pyblAccntID,
                      (this.pymntCmmntsTextBox.Text +
                      " (Balacing Leg for Payables Doc:-" +
                      docNum + ")" + " (" + cmnCde.getCstmrSpplrName(this.spplrID) + ")").Replace(" ()", ""), funcCurrAmnt,
                      lnDte, this.curid, glBatchID, 0.00,
                      netAmnt1, grndAmnt, this.entrdCurrID,
                      accntCurrAmnt, accntCurrID1, funcCurrRate1, accntCurrRate1, "D", "");
                }
                else
                {
                    this.createTransaction(pyblAccntID,
                      (this.pymntCmmntsTextBox.Text +
                      " (Balacing Leg for Payables Doc:-" +
                      docNum + ")" + " (" + cmnCde.getCstmrSpplrName(this.spplrID) + ")").Replace(" ()", ""), 0.00,
                      lnDte, this.curid,
                      glBatchID, funcCurrAmnt, netAmnt1,
               grndAmnt, this.entrdCurrID, accntCurrAmnt,
               accntCurrID1, funcCurrRate1, accntCurrRate1, "C", "");
                }
                if (this.get_Batch_CrdtSum(glBatchID) == this.get_Batch_DbtSum(glBatchID))
                {
                    this.updtPyblsDocGLBatch(docHdrID, glBatchID);
                    //this.updateAppldPrepayHdrs();
                    this.updateBatchAvlblty(glBatchID, "1");
                    return true;
                }
                else
                {
                    cmnCde.showMsg("The GL Batch created is not Balanced!\r\nTransactions created will be reversed and deleted!", 0);
                    this.deleteBatchTrns(glBatchID);
                    this.deleteBatch(glBatchID, glBatchName);
                    return false;
                }
            }
            catch (Exception ex)
            {
                cmnCde.showMsg("Document Approval Failed!\r\n" + ex.Message, 0);
                return false;
            }
        }

        public void updtPyblsDocGLBatch(long docid, long glBatchID)
        {
            cmnCde.Extra_Adt_Trl_Info = "";
            string dateStr = cmnCde.getDB_Date_time();
            string updtSQL = "UPDATE accb.accb_pybls_invc_hdr SET " +
                  "gl_batch_id=" + glBatchID +
                  ", last_update_by=" + cmnCde.User_id +
                  ", last_update_date='" + dateStr +
                  "' WHERE (pybls_invc_hdr_id = " +
                  docid + ")";
            cmnCde.updateDataNoParams(updtSQL);
        }

        private void checkNCreateRcvblsHdr()
        {
            //cmnCde.showMsg("Inside Rcvbl Hdr", 0);
            long cstmrID = this.spplrID;
            int cstmLblty = -1;
            int cstmRcvbl = -1;
            if (cstmrID > 0)
            {
                cstmLblty = int.Parse(cmnCde.getGnrlRecNm(
            "scm.scm_cstmr_suplr", "cust_sup_id", "dflt_pybl_accnt_id",
            cstmrID));
                cstmRcvbl = int.Parse(cmnCde.getGnrlRecNm(
            "scm.scm_cstmr_suplr", "cust_sup_id", "dflt_rcvbl_accnt_id",
            cstmrID));
            }

            if (cstmLblty > 0)
            {
                this.dfltLbltyAccnt = cstmLblty;
            }

            if (cstmRcvbl > 0)
            {
                this.dfltRcvblAcntID = cstmRcvbl;
            }
            //cmnCde.showMsg("Inside Rcvbl Hdr " + dfltRcvblAcntID, 0);

            //int curid = -1;

            string rcvblDocNum = "";
            string rcvblDocType = "";
            //string srcDocType = cmnCde.getGnrlRecNm("scm.scm_sales_invc_hdr", "invc_hdr_id", "invc_type", long.Parse(this.srcDocIDTextBox.Text));

            long rcvblHdrID = -1;/*this.get_ScmRcvblsDocHdrID(long.Parse(this.docIDTextBox.Text),
this.docTypeComboBox.Text, cmnCde.Org_id);*/

            rcvblDocNum = "CAP-" +
          DateTime.Parse(cmnCde.getFrmtdDB_Date_time()).ToString("yyMMdd-HHmmss")
                       + "-" + cmnCde.getRandomInt(10, 100);

            /*+"-" +
         cmnCde.getFrmtdDB_Date_time().Substring(12, 8).Replace(":", "") + "-" +
         this.getLtstRecPkID("accb.accb_rcvbls_invc_hdr",
         "rcvbls_invc_hdr_id");*/
            rcvblDocType = "Customer Advance Payment";
            this.createRcvblsDocHdr(cmnCde.Org_id, this.dteRcvdTextBox.Text.Substring(0, 11),
              rcvblDocNum, rcvblDocType, this.pymntCmmntsTextBox.Text,
              -1, this.spplrID, this.spplrSiteID, "Not Validated", "Approve",
              (double)this.amntPaidNumUpDown.Value, "", "",
              this.pymntMthdID, 0, -1, "",
              "Customer Prepayment", this.entrdCurrID, 0, dfltRcvblAcntID, this.srcDocID, this.srcDocType);
            //cmnCde.showMsg("Inside Rcvbl Hdr " + rcvblHdrID, 0);

            rcvblHdrID = cmnCde.getGnrlRecID("accb.accb_rcvbls_invc_hdr", "rcvbls_invc_number", "rcvbls_invc_hdr_id", rcvblDocNum);

            long curlnID = this.getNewRcvblsLnID();
            string lineType = "1Initial Amount";
            string lineDesc = "Customer Prepayment";
            double entrdAmnt = (double)this.amntPaidNumUpDown.Value;
            //int entrdCurrID = this.en;
            int codeBhnd = -1;
            string docType = rcvblDocType;
            bool autoCalc = false;
            string incrDcrs1 = "Increase";
            int costngID = this.dfltLbltyAccnt;
            string incrDcrs2 = "Increase";
            int blncgAccntID = this.dfltRcvblAcntID;
            long prepayDocHdrID = -1;
            string vldyStatus = "VALID";
            long orgnlLnID = -1;
            int funcCurrID = int.Parse(this.funcCurrIDTextBox.Text);
            int accntCurrID = int.Parse(this.accntCurrIDTextBox.Text);
            double funcCurrRate = (double)this.funcCurRateNumUpDwn.Value;
            double accntCurrRate = (double)this.accntCurRateNumUpDwn.Value;
            double funcCurrAmnt = (double)this.funcCurAmntNumUpDwn.Value;
            double accntCurrAmnt = (double)this.accntCurrNumUpDwn.Value;

            this.createRcvblsDocDet(curlnID, rcvblHdrID, lineType,
                            lineDesc, entrdAmnt, entrdCurrID, codeBhnd, docType, autoCalc, incrDcrs1,
                            costngID, incrDcrs2, blncgAccntID, prepayDocHdrID, vldyStatus, orgnlLnID, funcCurrID,
                            accntCurrID, funcCurrRate, accntCurrRate, funcCurrAmnt, accntCurrAmnt);

            this.reCalcRcvblsSmmrys(rcvblHdrID, rcvblDocType);

            if (this.approveRcvblsDoc(rcvblHdrID, rcvblDocNum))
            {
                this.updtRcvblsDocApprvl(rcvblHdrID, "Approved", "Cancel");
            }

            this.srcDocID = rcvblHdrID;
            this.srcDocType = rcvblDocType;
        }

        public void reCalcRcvblsSmmrys(long srcDocID, string srcDocType)
        {
            double grndAmnt = this.getRcvblsDocGrndAmnt(srcDocID);
            //Grand Total
            string smmryNm = "Grand Total";
            long smmryID = this.getRcvblsSmmryItmID("6Grand Total", -1,
              srcDocID, srcDocType, smmryNm);
            if (smmryID <= 0)
            {
                long curlnID = this.getNewRcvblsLnID();
                this.createRcvblsDocDet(curlnID, srcDocID, "6Grand Total",
                  smmryNm, grndAmnt, this.entrdCurrID,
                  -1, srcDocType, true, "Increase",
                  -1, "Increase", -1, -1, "VALID", -1, -1,
                  -1, 0, 0, 0, 0);
            }
            else
            {
                this.updtRcvblsDocDet(smmryID, srcDocID, "6Grand Total",
                  smmryNm, grndAmnt, this.entrdCurrID,
                  -1, srcDocType, true, "Increase",
                  -1, "Increase", -1, -1, "VALID", -1, -1,
                  -1, 0, 0, 0, 0);
            }

            //7Total Payments Received
            smmryNm = "Total Payments Made";
            smmryID = this.getRcvblsSmmryItmID("7Total Payments Made", -1,
              srcDocID, srcDocType, smmryNm);
            double pymntsAmnt = this.getRcvblsDocTtlPymnts(srcDocID, srcDocType);

            if (smmryID <= 0)
            {
                long curlnID = this.getNewRcvblsLnID();
                this.createRcvblsDocDet(curlnID, srcDocID, "7Total Payments Made",
                  smmryNm, pymntsAmnt, this.entrdCurrID,
                  -1, srcDocType, true, "Increase",
                  -1, "Increase", -1, -1, "VALID", -1, -1,
                  -1, 0, 0, 0, 0);
            }
            else
            {
                this.updtRcvblsDocDet(smmryID, srcDocID, "7Total Payments Made",
                  smmryNm, pymntsAmnt, this.entrdCurrID,
                  -1, srcDocType, true, "Increase",
                  -1, "Increase", -1, -1, "VALID", -1, -1,
                  -1, 0, 0, 0, 0);
            }

            //7Total Payments Received
            smmryNm = "Outstanding Balance";
            smmryID = this.getRcvblsSmmryItmID("8Outstanding Balance", -1,
              srcDocID, srcDocType, smmryNm);
            double outstndngAmnt = grndAmnt - pymntsAmnt;
            if (smmryID <= 0)
            {
                long curlnID = this.getNewRcvblsLnID();
                this.createRcvblsDocDet(curlnID, srcDocID, "8Outstanding Balance",
                  smmryNm, outstndngAmnt, this.entrdCurrID,
                  -1, srcDocType, true, "Increase",
                  -1, "Increase", -1, -1, "VALID", -1, -1,
                  -1, 0, 0, 0, 0);
            }
            else
            {
                this.updtRcvblsDocDet(smmryID, srcDocID, "8Outstanding Balance",
                  smmryNm, outstndngAmnt, this.entrdCurrID,
                  -1, srcDocType, true, "Increase",
                  -1, "Increase", -1, -1, "VALID", -1, -1,
                  -1, 0, 0, 0, 0);
            }
        }

        public bool approveRcvblsDoc(long docHdrID, string docNum)
        {
            /* 1. Create a GL Batch and get all doc lines
             * 2. for each line create costing account transaction
             * 3. create one balancing account transaction using the grand total amount
             * 4. Check if created gl_batch is balanced.
             * 5. if balanced update docHdr else delete the gl batch created and throw error message
             */
            try
            {
                string glBatchName = "ACC_RCVBL-" +
                 DateTime.Parse(cmnCde.getFrmtdDB_Date_time()).ToString("yyMMdd-HHmmss")
                          + "-" + cmnCde.getRandomInt(10, 100);

                /*+cmnCde.getDB_Date_time().Substring(11, 8).Replace(":", "").Replace("-", "").Replace(" ", "") + "-" +
            this.getNewBatchID().ToString().PadLeft(4, '0');*/

                long glBatchID = cmnCde.getGnrlRecID("accb.accb_trnsctn_batches",
                  "batch_name", "batch_id", glBatchName, cmnCde.Org_id);

                if (glBatchID <= 0)
                {
                    this.createBatch(cmnCde.Org_id, glBatchName,
                      this.pymntCmmntsTextBox.Text + " (" + docNum + ")",
                      "Receivables Invoice Document", "VALID", -1, "0");
                }
                else
                {
                    cmnCde.showMsg("GL Batch Could not be Created!\r\n Try Again Later!", 0);
                    return false;
                }
                glBatchID = cmnCde.getGnrlRecID("accb.accb_trnsctn_batches",
                  "batch_name", "batch_id", glBatchName, cmnCde.Org_id);
                int rcvblAccntID = -1;
                string lnDte = this.dteRcvdTextBox.Text;
                DataSet dtst = this.get_RcvblsDocDet(docHdrID);
                for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
                {
                    string lineTypeNm = dtst.Tables[0].Rows[i][1].ToString();
                    int codeBhndID = -1;
                    int.TryParse(dtst.Tables[0].Rows[i][4].ToString(), out codeBhndID);

                    string incrDcrs1 = dtst.Tables[0].Rows[i][6].ToString().Substring(0, 1);
                    int accntID1 = -1;
                    int.TryParse(dtst.Tables[0].Rows[i][7].ToString(), out accntID1);
                    string isdbtCrdt1 = cmnCde.dbtOrCrdtAccnt(accntID1, incrDcrs1.Substring(0, 1));

                    string incrDcrs2 = dtst.Tables[0].Rows[i][8].ToString().Substring(0, 1);
                    int accntID2 = -1;
                    int.TryParse(dtst.Tables[0].Rows[i][9].ToString(), out accntID2);
                    rcvblAccntID = accntID2;
                    string isdbtCrdt2 = cmnCde.dbtOrCrdtAccnt(accntID2, incrDcrs2.Substring(0, 1));

                    double lnAmnt = double.Parse(dtst.Tables[0].Rows[i][19].ToString());

                    System.Windows.Forms.Application.DoEvents();

                    double acntAmnt = 0;
                    double.TryParse(dtst.Tables[0].Rows[i][20].ToString(), out acntAmnt);
                    double entrdAmnt = 0;
                    double.TryParse(dtst.Tables[0].Rows[i][3].ToString(), out entrdAmnt);

                    string lneDesc = dtst.Tables[0].Rows[i][2].ToString();
                    int entrdCurrID = int.Parse(dtst.Tables[0].Rows[i][11].ToString());
                    int funcCurrID = int.Parse(dtst.Tables[0].Rows[i][13].ToString());
                    int accntCurrID = int.Parse(dtst.Tables[0].Rows[i][15].ToString());
                    double funcCurrRate = double.Parse(dtst.Tables[0].Rows[i][17].ToString());
                    double accntCurrRate = double.Parse(dtst.Tables[0].Rows[i][18].ToString());

                    if (accntID1 > 0 && (lnAmnt != 0 || acntAmnt != 0) && incrDcrs1 != "" && lneDesc != "")
                    {
                        double netAmnt = (double)this.dbtOrCrdtAccntMultiplier(accntID1,
                  incrDcrs1) * (double)lnAmnt;


                        //if (!cmnCde.isTransPrmttd(accntID1, lnDte, netAmnt))
                        //{
                        //  return false;
                        //}

                        if (this.dbtOrCrdtAccnt(accntID1,
                          incrDcrs1) == "Debit")
                        {
                            this.createTransaction(accntID1,
                              lneDesc, lnAmnt,
                              lnDte, funcCurrID, glBatchID, 0.00,
                              netAmnt, entrdAmnt, entrdCurrID, acntAmnt,
                              accntCurrID, funcCurrRate, accntCurrRate, "D", "");
                        }
                        else
                        {
                            this.createTransaction(accntID1,
                              lneDesc, 0.00,
                              lnDte, funcCurrID,
                              glBatchID, lnAmnt, netAmnt,
                      entrdAmnt, entrdCurrID, acntAmnt, accntCurrID,
                      funcCurrRate, accntCurrRate, "C", "");
                        }
                    }
                }
                //Receivable Balancing Leg

                int accntCurrID1 = int.Parse(cmnCde.getGnrlRecNm(
            "accb.accb_chart_of_accnts", "accnt_id", "crncy_id", rcvblAccntID));

                string slctdCurrID = this.entrdCurrID.ToString();
                double funcCurrRate1 = Math.Round(
            this.get_LtstExchRate(int.Parse(slctdCurrID), this.curid, lnDte), 15);
                double accntCurrRate1 = Math.Round(
                  this.get_LtstExchRate(int.Parse(slctdCurrID), accntCurrID1, lnDte), 15);
                System.Windows.Forms.Application.DoEvents();

                double grndAmnt = this.getRcvblsDocGrndAmnt(docHdrID);

                double funcCurrAmnt = this.getRcvblsDocFuncAmnt(docHdrID);// (funcCurrRate1 * grndAmnt);
                double accntCurrAmnt = (accntCurrRate1 * grndAmnt);
                System.Windows.Forms.Application.DoEvents();

                double netAmnt1 = (double)this.dbtOrCrdtAccntMultiplier(rcvblAccntID,
            "I") * (double)funcCurrAmnt;


                //if (!cmnCde.isTransPrmttd(rcvblAccntID, lnDte, netAmnt1))
                //{
                //  return false;
                //}

                if (this.dbtOrCrdtAccnt(rcvblAccntID,
                  "I") == "Debit")
                {
                    this.createTransaction(rcvblAccntID,
                      this.pymntCmmntsTextBox.Text +
                      " (Balacing Leg for Receivables Doc:-" +
                      docNum + ")", funcCurrAmnt,
                      lnDte, this.curid, glBatchID, 0.00,
                      netAmnt1, grndAmnt, this.entrdCurrID,
                      accntCurrAmnt, accntCurrID1, funcCurrRate1, accntCurrRate1, "D", "");
                }
                else
                {
                    this.createTransaction(rcvblAccntID,
                      this.pymntCmmntsTextBox.Text +
                      " (Balacing Leg for Receivables Doc:-" +
                      docNum + ")", 0.00,
                      lnDte, this.curid,
                      glBatchID, funcCurrAmnt, netAmnt1,
               grndAmnt, this.entrdCurrID, accntCurrAmnt,
               accntCurrID1, funcCurrRate1, accntCurrRate1, "C", "");
                }
                if (this.get_Batch_CrdtSum(glBatchID) == this.get_Batch_DbtSum(glBatchID))
                {
                    this.updtRcvblsDocGLBatch(docHdrID, glBatchID);
                    //this.updateAppldPrepayHdrs();
                    this.updateBatchAvlblty(glBatchID, "1");
                    return true;
                }
                else
                {
                    cmnCde.showMsg("The GL Batch created is not Balanced!\r\nTransactions created will be reversed and deleted!", 0);
                    this.deleteBatchTrns(glBatchID);
                    this.deleteBatch(glBatchID, glBatchName);
                    return false;
                }
            }
            catch (Exception ex)
            {
                cmnCde.showMsg("Receivables Document Approval Failed!", 0);
                return false;
            }
        }

        private bool isPayTrnsValid(int accntID, string incrsDcrs, double amnt, string date1)
        {
            double netamnt = 0;

            netamnt = cmnCde.dbtOrCrdtAccntMultiplier(accntID,
         incrsDcrs) * amnt;

            if (!cmnCde.isTransPrmttd(
         accntID, date1, netamnt))
            {
                return false;
            }
            return true;
        }

        private void addPymntDiag_Load(object sender, EventArgs e)
        {
            if (this.dsablPayments)
            {
                this.Size = new Size(1015, 490);
            }
            else
            {
                this.Size = new Size(505, 490);
            }
            string dateStr = cmnCde.getFrmtdDB_Date_time();

            this.dfltRcvblAcntID = this.get_DfltRcvblAcnt(cmnCde.Org_id);
            this.dfltLbltyAccnt = this.get_DfltPyblAcnt(cmnCde.Org_id);

            double invcAmnt = 20000;
            if (this.isPayTrnsValid(this.get_DfltRcvblAcnt(cmnCde.Org_id), "I", invcAmnt, dateStr))
            {
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }


            System.Windows.Forms.Application.DoEvents();
            Color[] clrs = cmnCde.getColors();
            this.BackColor = clrs[0];
            this.curid = cmnCde.getOrgFuncCurID(cmnCde.Org_id);
            this.curCode = cmnCde.getPssblValNm(this.curid);
            this.dteRcvdTextBox.Text = cmnCde.getFrmtdDB_Date_time();
            this.amntToPayNumUpDwn.Value = (decimal)this.amntToPay;

            this.crncyIDTextBox.Text = this.entrdCurrID.ToString();
            this.crncyTextBox.Text = cmnCde.getPssblValNm(this.entrdCurrID);
            this.curr1TextBox.Text = this.crncyTextBox.Text;
            this.curr2TextBox.Text = this.crncyTextBox.Text;
            this.curr3TextBox.Text = this.crncyTextBox.Text;
            this.funcCurrIDTextBox.Text = this.curid.ToString();
            this.funcCurrTextBox.Text = this.curCode;
            this.accntCurrIDTextBox.Text = this.curid.ToString();
            this.acntCurrTextBox.Text = this.curCode;

            if (docTypes == "Supplier Payments")
            {
                this.docNum = cmnCde.getGnrlRecNm(
            "accb.accb_pybls_invc_hdr", "pybls_invc_hdr_id", "pybls_invc_number",
            this.srcDocID);
                this.docStatus = cmnCde.getGnrlRecNm(
            "accb.accb_pybls_invc_hdr", "pybls_invc_hdr_id", "approval_status",
            this.srcDocID);
                this.docDesc = cmnCde.getGnrlRecNm(
            "accb.accb_pybls_invc_hdr", "pybls_invc_hdr_id", "comments_desc",
            this.srcDocID);
                this.lnkdDocID = -1;
            }
            else
            {
                this.docNum = cmnCde.getGnrlRecNm(
            "accb.accb_rcvbls_invc_hdr", "rcvbls_invc_hdr_id", "rcvbls_invc_number",
            this.srcDocID);
                this.docStatus = cmnCde.getGnrlRecNm(
            "accb.accb_rcvbls_invc_hdr", "rcvbls_invc_hdr_id", "approval_status",
            this.srcDocID);
                this.docDesc = cmnCde.getGnrlRecNm(
            "accb.accb_rcvbls_invc_hdr", "rcvbls_invc_hdr_id", "comments_desc",
            this.srcDocID);
                this.lnkdDocID = long.Parse(cmnCde.getGnrlRecNm(
            "accb.accb_rcvbls_invc_hdr", "rcvbls_invc_hdr_id", "src_doc_hdr_id",
            this.srcDocID));
            }
            if (this.docStatus == "Cancelled")
            {
                this.dsablPayments = true;
                this.createPrepay = false;
            }
            if (this.orgnlPymntID <= 0)
            {
                this.pymntCmmntsTextBox.Text = ("Payment for Invoice No. (" + this.docNum +
                  ") (" + this.docDesc + ")").Replace("()", "");
            }
            if (this.orgnlPymntID <= 0)
            {
                DataSet dtst = this.getPymntMthds(cmnCde.Org_id, docTypes);
                this.pymntTypeComboBox.Items.Clear();
                for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
                {
                    this.pymntTypeComboBox.Items.Add(dtst.Tables[0].Rows[i][0].ToString() +
                      "-" + dtst.Tables[0].Rows[i][1].ToString());
                    if (dtst.Tables[0].Rows[i][1].ToString().Contains("Prepayment Application")
                      && this.srcDocType.Contains("Advance") == false
                      && (this.orgnlPymntID <= 0))
                    {
                        if (docTypes == "Supplier Payments" && this.createPrepay == false
                        && this.getSpplrDpsts(this.spplrID, this.curid) > 0)
                        {
                            this.obey_evnts = true;
                            this.pymntTypeComboBox.SelectedItem = dtst.Tables[0].Rows[i][0].ToString() +
                            "-" + dtst.Tables[0].Rows[i][1].ToString();
                            this.obey_evnts = false;
                        }
                        else if (docTypes == "Customer Payments" && this.createPrepay == false
                        && this.getCstmrDpsts(this.spplrID, this.curid) > 0)
                        {
                            this.obey_evnts = true;
                            this.pymntTypeComboBox.SelectedItem = dtst.Tables[0].Rows[i][0].ToString() +
                            "-" + dtst.Tables[0].Rows[i][1].ToString();
                            this.obey_evnts = false;
                        }
                    }
                    else if (dtst.Tables[0].Rows[i][0].ToString() == this.pymntMthdID.ToString())
                    {
                        this.obey_evnts = true;
                        this.pymntTypeComboBox.SelectedItem = dtst.Tables[0].Rows[i][0].ToString() +
                        "-" + dtst.Tables[0].Rows[i][1].ToString();
                        this.obey_evnts = false;
                    }
                }
            }
            if (this.orgnlPymntID <= 0)
            {
                this.populateTrnsGridVw();
            }

            this.pymntTypeComboBox.Focus();
            System.Windows.Forms.Application.DoEvents();
            SendKeys.Send("{TAB}");
            SendKeys.Send("{TAB}");
            SendKeys.Send("{TAB}");
            SendKeys.Send("{TAB}");
            SendKeys.Send("{TAB}");
            SendKeys.Send("{TAB}");
            this.amntRcvdNumUpDown.Focus();
            SendKeys.Send("^(A)");

            if (this.dsablPayments)
            {
                this.groupBox1.Enabled = false;
                this.groupBox2.Enabled = false;
                this.groupBox4.Enabled = false;
                this.processPayButton.Enabled = false;
                this.splitContainer1.Panel1Collapsed = true;
                this.pymntHistoryButton.PerformClick();
                //System.Windows.Forms.Application.DoEvents();
            }
            else
            {
                this.processPayButton.Enabled = true;
                this.splitContainer1.Panel1Collapsed = false;
            }
            if (this.createPrepay)
            {
                if (this.spplrID <= 0)
                {
                    cmnCde.showMsg("Cannot Take Deposits from an Unknown Customer!\r\nPlease Check-Out and Use Customer's Bill Settlement Instead!", 0);
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    return;
                }
                if (docTypes == "Supplier Payments")
                {
                    this.Text = "Supplier Advance Payments (Deposits)";
                    this.pymntCmmntsTextBox.Text = "Deposit (Advance Payment) from Supplier (" + cmnCde.getGnrlRecNm(
                      "scm.scm_cstmr_suplr", "cust_sup_id", "cust_sup_name", this.spplrID) + ")";
                }
                else
                {
                    this.Text = "Customer Advance Payments (Deposits)";
                    this.pymntCmmntsTextBox.Text = "Deposit (Advance Payment) from Customer (" + cmnCde.getGnrlRecNm(
                      "scm.scm_cstmr_suplr", "cust_sup_id", "cust_sup_name", this.spplrID) + ")";
                }
                this.amntToPay = 0;
                this.amntToPayNumUpDwn.Value = 0;
                this.amntPaidNumUpDown.ReadOnly = false;
                this.amntPaidNumUpDown.Increment = 1;
                this.amntPaidNumUpDown.BackColor = Color.FromArgb(255, 255, 128);
            }
            if (this.dsablPayments == true)
            {
                //this.WindowState = FormWindowState.Maximized;
            }
            else if (this.prepayButton.Enabled == true)
            {
                this.prepayButton.PerformClick();
            }
            if (this.msPyID > 0)
            {
                this.obey_evnts = true;
                this.amntRcvdNumUpDown.Value = (decimal)this.amntToPay;
            }
            this.obey_evnts = true;
        }

        private void populateTrnsGridVw()
        {
            this.obey_evnts = false;
            DataSet dtst;

            dtst = this.get_Pay_Trns(this.docNum, this.docTypes);
            this.trnsSearchListView.Items.Clear();
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                string amntRcvd = "0.00";

                if ((double.Parse(dtst.Tables[0].Rows[i][3].ToString()) > 0
                  && double.Parse(dtst.Tables[0].Rows[i][4].ToString()) <= 0)
                  || (double.Parse(dtst.Tables[0].Rows[i][3].ToString()) < 0
                  && double.Parse(dtst.Tables[0].Rows[i][4].ToString()) >= 0))
                {
                    amntRcvd = ((double.Parse(dtst.Tables[0].Rows[i][3].ToString()) / Math.Abs(double.Parse(dtst.Tables[0].Rows[i][3].ToString()))) *
               Math.Abs(double.Parse(dtst.Tables[0].Rows[i][3].ToString()) - double.Parse(dtst.Tables[0].Rows[i][4].ToString()))).ToString("#,##0.00");
                }
                else if ((double.Parse(dtst.Tables[0].Rows[i][3].ToString()) > 0
                  && double.Parse(dtst.Tables[0].Rows[i][4].ToString()) > 0)
                  || (double.Parse(dtst.Tables[0].Rows[i][3].ToString()) < 0
                  && double.Parse(dtst.Tables[0].Rows[i][4].ToString()) < 0))
                {
                    amntRcvd = double.Parse(dtst.Tables[0].Rows[i][3].ToString()).ToString("#,##0.00");
                }
                //this.last_trns_num = cmnCde.navFuncts.startIndex() + i;
                ListViewItem nwItem = new ListViewItem(new string[] {
    (1 + i).ToString(),
    dtst.Tables[0].Rows[i][6].ToString(),
                dtst.Tables[0].Rows[i][8].ToString(),
    dtst.Tables[0].Rows[i][2].ToString(),
          amntRcvd,
    double.Parse(dtst.Tables[0].Rows[i][3].ToString()).ToString("#,##0.00"),
    double.Parse(dtst.Tables[0].Rows[i][4].ToString()).ToString("#,##0.00"),
          dtst.Tables[0].Rows[i][10].ToString(),
          dtst.Tables[0].Rows[i][5].ToString(),
    dtst.Tables[0].Rows[i][7].ToString(),
    dtst.Tables[0].Rows[i][0].ToString(),
    dtst.Tables[0].Rows[i][14].ToString(),
    dtst.Tables[0].Rows[i][15].ToString(),
    dtst.Tables[0].Rows[i][13].ToString(),
    dtst.Tables[0].Rows[i][12].ToString(),
    dtst.Tables[0].Rows[i][17].ToString(),
    dtst.Tables[0].Rows[i][16].ToString(),
    dtst.Tables[0].Rows[i][18].ToString(),
    dtst.Tables[0].Rows[i][19].ToString(),
    dtst.Tables[0].Rows[i][20].ToString(),
    dtst.Tables[0].Rows[i][21].ToString(),
    dtst.Tables[0].Rows[i][22].ToString(),
    dtst.Tables[0].Rows[i][23].ToString(),
    dtst.Tables[0].Rows[i][24].ToString(),
    dtst.Tables[0].Rows[i][1].ToString()});
                this.trnsSearchListView.Items.Add(nwItem);
            }
            /*
          this.get_GLBatch_Nm(long.Parse(dtst.Tables[0].Rows[i][8].ToString())),*/
            //this.correctTrnsNavLbls(dtst);
            this.obey_evnts = true;
        }

        private void dteRcvdTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!this.obey_evnts)
            {
                return;
            }
            this.txtChngd = true;

        }

        private void dteRcvdTextBox_Leave(object sender, EventArgs e)
        {
            if (this.txtChngd == false)
            {
                return;
            }
            this.txtChngd = false;
            TextBox mytxt = (TextBox)sender;
            this.obey_evnts = false;

            if (mytxt.Name == "dteRcvdTextBox")
            {
                this.trnsDteLOVSrch();
            }
            this.obey_evnts = true;
            this.txtChngd = false;
        }

        private void trnsDteLOVSrch()
        {
            DateTime dte1 = DateTime.Now;
            bool sccs = DateTime.TryParse(this.dteRcvdTextBox.Text, out dte1);
            if (!sccs)
            {
                dte1 = DateTime.Now;
            }
            this.dteRcvdTextBox.Text = dte1.ToString("dd-MMM-yyyy HH:mm:ss");

            this.updateRatesNAmnts();
        }

        private void updateRatesNAmnts()
        {
            this.obey_evnts = false;
            string slctdCurrID = this.crncyIDTextBox.Text;
            string accntCurrID = this.accntCurrIDTextBox.Text;
            string funcCurrID = this.funcCurrIDTextBox.Text;

            if (this.funcCurRateNumUpDwn.Value == 0 || (this.funcCurRateNumUpDwn.Value == 1 && int.Parse(slctdCurrID) != this.curid))
            {
                this.funcCurRateNumUpDwn.Value = (decimal)Math.Round(
                      this.get_LtstExchRate(int.Parse(slctdCurrID), int.Parse(funcCurrID),
                this.dteRcvdTextBox.Text), 15);
            }
            if (this.accntCurRateNumUpDwn.Value == 0 || (this.accntCurRateNumUpDwn.Value == 1 && int.Parse(slctdCurrID) != this.curid))
            {
                this.accntCurRateNumUpDwn.Value = (decimal)Math.Round(
                        this.get_LtstExchRate(int.Parse(slctdCurrID), int.Parse(accntCurrID),
                this.dteRcvdTextBox.Text), 15);
            }
            System.Windows.Forms.Application.DoEvents();

            double funcCurrRate = (double)this.funcCurRateNumUpDwn.Value;
            double accntCurrRate = (double)this.accntCurRateNumUpDwn.Value;
            double entrdAmnt = (double)this.amntPaidNumUpDown.Value;
            this.funcCurAmntNumUpDwn.Value = (decimal)(funcCurrRate * entrdAmnt);
            this.accntCurrNumUpDwn.Value = (decimal)(accntCurrRate * entrdAmnt);
            System.Windows.Forms.Application.DoEvents();
            this.obey_evnts = true;
        }

        private void dteRcvdButton_Click(object sender, EventArgs e)
        {
            cmnCde.selectDate(ref this.dteRcvdTextBox);
            this.updateRatesNAmnts();
        }

        private void pymntTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.obey_evnts == false
              || this.pymntTypeComboBox.SelectedIndex < 0
              || this.pymntTypeComboBox.Text == "")
            {
                return;
            }
            char[] w = { '-' };
            this.pymntMthdID = int.Parse(this.pymntTypeComboBox.Text.Split(w,
              StringSplitOptions.RemoveEmptyEntries)[0]);
            if ((this.pymntTypeComboBox.Text.Contains("Check")
                || this.pymntTypeComboBox.Text.Contains("Cheque")))
            {
                this.cardNameTextBox.BackColor = Color.FromArgb(255, 255, 128);
                this.cardNumTextBox.BackColor = Color.FromArgb(255, 255, 128);
            }
            else
            {
                this.cardNameTextBox.BackColor = Color.FromArgb(255, 255, 255);
                this.cardNumTextBox.BackColor = Color.FromArgb(255, 255, 255);
            }

            if (this.orgnlPymntID <= 0 && this.prcsngPay == false)
            {
                this.prepayDocNumTextBox.Text = "";
                this.prepayDocIDTextBox.Text = "-1";
                this.otherInfoTextBox.Text = "";
                this.cardNameTextBox.Text = "";
                this.cardNumTextBox.Text = "";
                this.expDateTextBox.Text = "00/00";
                this.sigCodeTextBox.Text = "";
            }

            if ((this.pymntTypeComboBox.Text.Contains("Check")
              || this.pymntTypeComboBox.Text.Contains("Cheque"))
              && (this.cardNameTextBox.Text == ""))
            {
                if (docTypes == "Supplier Payments")
                {
                    this.cardNameTextBox.Text = cmnCde.getOrgName(cmnCde.Org_id);
                }
                else
                {
                    this.cardNameTextBox.Text = cmnCde.getCstmrSpplrName(this.spplrID);
                }
            }
            if (this.docTypes == "Supplier Payments")
            {
                int blcngAccntID = this.getPyblsDocBlncngAccnt(this.srcDocID, this.srcDocType);
                int chrgAccntID = this.getPymntMthdChrgAccnt(this.pymntMthdID);
                string accntType = cmnCde.getAccntType(chrgAccntID);
                string accntType2 = cmnCde.getAccntType(blcngAccntID);

                if (this.pymntTypeComboBox.Text.Contains("Prepayment")
               || this.pymntTypeComboBox.Text.Contains("Advance"))
                {
                    this.amntGvnLabel.Text = "Actual Amount Applied:";
                    this.prepayButton.Enabled = true;
                    this.prepayDocNumTextBox.Enabled = true;
                }
                else
                {
                    this.amntGvnLabel.Text = "Actual Amount Sent:";
                    this.prepayButton.Enabled = false;
                    this.prepayDocNumTextBox.Enabled = false;
                }
                if (chrgAccntID > 0 && blcngAccntID > 0)
                {
                    string incrs1 = "INCREASE";
                    if (accntType == "A" && cmnCde.isAccntContra(chrgAccntID) != "1" && accntType2 == "L")
                    {
                        incrs1 = "DECREASE";
                    }
                    this.incrsDcrs1ComboBox.Items.Clear();
                    this.incrsDcrs1ComboBox.Items.Add(incrs1);
                    this.incrsDcrs1ComboBox.SelectedItem = incrs1;
                    this.chrgeAccntIDTextBox.Text = chrgAccntID.ToString();
                    this.chrgeAccntTextBox.Text = cmnCde.getAccntNum(chrgAccntID) +
                      "." + cmnCde.getAccntName(chrgAccntID);

                    this.incrsDcrs2ComboBox.Items.Clear();
                    this.incrsDcrs2ComboBox.Items.Add("DECREASE");
                    this.incrsDcrs2ComboBox.SelectedItem = "DECREASE";
                    this.blcngAccntIDTextBox.Text = blcngAccntID.ToString();
                    this.blncAccntTextBox.Text = cmnCde.getAccntNum(blcngAccntID) +
                      "." + cmnCde.getAccntName(blcngAccntID);

                }
            }
            else
            {
                if (this.pymntTypeComboBox.Text.Contains("Prepayment")
               || this.pymntTypeComboBox.Text.Contains("Advance"))
                {
                    this.amntGvnLabel.Text = "Actual Amount Applied:";
                    this.prepayButton.Enabled = true;
                    this.prepayDocNumTextBox.Enabled = true;
                }
                else
                {
                    this.amntGvnLabel.Text = "Actual Amount Received:";
                    this.prepayButton.Enabled = false;
                    this.prepayDocNumTextBox.Enabled = false;
                }
                int blcngAccntID = this.getRcvblsDocBlncngAccnt(this.srcDocID, this.srcDocType);
                int chrgAccntID = this.getPymntMthdChrgAccnt(this.pymntMthdID);
                string accntType = cmnCde.getAccntType(chrgAccntID);
                string accntType2 = cmnCde.getAccntType(blcngAccntID);
                if (chrgAccntID > 0 && blcngAccntID > 0)
                {
                    string incrs1 = "INCREASE";
                    if ((accntType == "L" && cmnCde.isAccntContra(chrgAccntID) != "1" && accntType2 == "A")
                      || (accntType == "A" && cmnCde.isAccntContra(chrgAccntID) != "1" && accntType2 == "L"))
                    {
                        incrs1 = "DECREASE";
                    }
                    this.incrsDcrs1ComboBox.Items.Clear();
                    this.incrsDcrs1ComboBox.Items.Add(incrs1);
                    this.incrsDcrs1ComboBox.SelectedItem = incrs1;
                    this.chrgeAccntIDTextBox.Text = chrgAccntID.ToString();
                    this.chrgeAccntTextBox.Text = cmnCde.getAccntNum(chrgAccntID) +
                      "." + cmnCde.getAccntName(chrgAccntID);

                    this.incrsDcrs2ComboBox.Items.Clear();
                    this.incrsDcrs2ComboBox.Items.Add("DECREASE");
                    this.incrsDcrs2ComboBox.SelectedItem = "DECREASE";
                    this.blcngAccntIDTextBox.Text = blcngAccntID.ToString();
                    this.blncAccntTextBox.Text = cmnCde.getAccntNum(blcngAccntID) +
                      "." + cmnCde.getAccntName(blcngAccntID);

                }
            }
            this.updateRatesNAmnts();
            this.amntRcvdNumUpDown.Focus();
            System.Windows.Forms.Application.DoEvents();
            /*SendKeys.Send("{TAB}");
            SendKeys.Send("{TAB}");
            SendKeys.Send("{TAB}");
            SendKeys.Send("{TAB}");
            SendKeys.Send("{TAB}");
            SendKeys.Send("{TAB}");*/
            this.amntRcvdNumUpDown.Focus();
            SendKeys.Send("^(A)");
        }

        private void amntRcvdNumUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (this.obey_evnts == false
          || this.pymntTypeComboBox.SelectedIndex < 0
          || this.pymntTypeComboBox.Text == "")
            {
                return;
            }
            this.obey_evnts = false;
            if (this.amntToPay < 0 && this.amntRcvdNumUpDown.Value > 0)
            {
                this.amntRcvdNumUpDown.Value = -1 * this.amntRcvdNumUpDown.Value;
            }

            if (Math.Abs(this.amntRcvdNumUpDown.Value) > Math.Abs(this.amntToPayNumUpDwn.Value)
             && this.createPrepay == false)
            {
                this.amntPaidNumUpDown.Value = this.amntToPayNumUpDwn.Value;
            }
            else
            {
                this.amntPaidNumUpDown.Value = this.amntRcvdNumUpDown.Value;
            }
            if (this.amntPaidNumUpDown.Value < this.amntToPayNumUpDwn.Value)
            {
                this.changeNumUpDown.Value = this.amntToPayNumUpDwn.Value
             - this.amntRcvdNumUpDown.Value;
            }
            else
            {
                this.changeNumUpDown.Value = this.amntPaidNumUpDown.Value
        - this.amntRcvdNumUpDown.Value;
            }

            this.updateRatesNAmnts();
            this.obey_evnts = true;
        }

        private void funcCurRateNumUpDwn_ValueChanged(object sender, EventArgs e)
        {
            if (this.obey_evnts == false
          || this.pymntTypeComboBox.SelectedIndex < 0
          || this.pymntTypeComboBox.Text == "")
            {
                return;
            }
            this.updateRatesNAmnts();
        }

        private void processPayButton_Click(object sender, EventArgs e)
        {
            if (!cmnCde.isTransPrmttd(
        cmnCde.get_DfltCashAcnt(cmnCde.Org_id), this.dteRcvdTextBox.Text, 200))
            {
                return;
            }
            if (this.docStatus == "Cancelled")
            {
                cmnCde.showMsg("Cannot Process Payments on Cancelled Documents!", 0);
                return;
            }
            this.prcsngPay = true;
            this.processPayButton.Enabled = false;
            System.Windows.Forms.Application.DoEvents();
            if (docTypes == "Supplier Payments")
            {
                if (cmnCde.test_prmssns(this.dfltPrvldgs[71]) == false)
                {
                    cmnCde.showMsg("You don't have permission to perform" +
                        " this action!\nContact your System Administrator!", 0);
                    this.processPayButton.Enabled = true;
                    this.prcsngPay = false;
                    return;
                }
            }
            else
            {
                if (cmnCde.test_prmssns(this.dfltPrvldgs[72]) == false)
                {
                    cmnCde.showMsg("You don't have permission to perform" +
                        " this action!\nContact your System Administrator!", 0);
                    this.processPayButton.Enabled = true;
                    this.prcsngPay = false;
                    return;
                }
            }
            double lnAmnt = (double)this.amntRcvdNumUpDown.Value;
            long prepayDocID = -1;
            string prepayDocType = "";
            if (this.pymntTypeComboBox.Text == "")
            {
                cmnCde.showMsg("Please indicate the payment Type!", 0);
                this.processPayButton.Enabled = true;
                this.prcsngPay = false;
                return;
            }
            if (this.pymntCmmntsTextBox.Text == "")
            {
                cmnCde.showMsg("Please indicate the Payment Remark/Comment!", 0);
                this.processPayButton.Enabled = true;
                this.prcsngPay = false;
                return;
            }
            if (this.orgnlPymntID <= 0)
            {
                if ((this.pymntTypeComboBox.Text.Contains("Check")
                  || this.pymntTypeComboBox.Text.Contains("Cheque"))
                  && (this.cardNumTextBox.Text == "" || this.cardNameTextBox.Text == ""))
                {
                    cmnCde.showMsg("Please Indicate the Card/Cheque Name and No. if Payment Type is Cheque!", 0);
                    this.processPayButton.Enabled = true;
                    this.prcsngPay = false;
                    return;
                }

                if (this.dteRcvdTextBox.Text == "")
                {
                    cmnCde.showMsg("Please indicate the Payment Date!", 0);
                    this.processPayButton.Enabled = true;
                    this.prcsngPay = false;
                    return;
                }
                if (this.amntRcvdNumUpDown.Value == 0)
                {
                    cmnCde.showMsg("Please indicate the amount Given!", 0);
                    this.processPayButton.Enabled = true;
                    this.prcsngPay = false;
                    return;
                }
                if ((this.pymntTypeComboBox.Text.Contains("Prepayment")
                || this.pymntTypeComboBox.Text.Contains("Advance")))
                {
                    if (this.prepayDocIDTextBox.Text == "" || this.prepayDocIDTextBox.Text == "-1")
                    {
                        cmnCde.showMsg("Please select the Prepayment you want to Apply First!", 0);
                        this.processPayButton.Enabled = true;
                        this.prcsngPay = false;
                        return;
                    }
                    else
                    {
                        decimal prepayAvlblAmnt = 0;
                        prepayDocID = long.Parse(this.prepayDocIDTextBox.Text);
                        if (docTypes == "Supplier Payments")
                        {
                            prepayAvlblAmnt = Decimal.Parse(cmnCde.getGnrlRecNm(
                       "accb.accb_pybls_invc_hdr", "pybls_invc_hdr_id",
                       "invoice_amount-invc_amnt_appld_elswhr",
                       long.Parse(this.prepayDocIDTextBox.Text)));
                            prepayDocType = cmnCde.getGnrlRecNm(
                      "accb.accb_pybls_invc_hdr", "pybls_invc_hdr_id", "pybls_invc_type",
                      long.Parse(this.prepayDocIDTextBox.Text));
                        }
                        else
                        {
                            prepayAvlblAmnt = Decimal.Parse(cmnCde.getGnrlRecNm(
                       "accb.accb_rcvbls_invc_hdr", "rcvbls_invc_hdr_id", "invoice_amount-invc_amnt_appld_elswhr",
                       long.Parse(this.prepayDocIDTextBox.Text)));
                            prepayDocType = cmnCde.getGnrlRecNm(
                      "accb.accb_rcvbls_invc_hdr", "rcvbls_invc_hdr_id", "rcvbls_invc_type",
                      long.Parse(this.prepayDocIDTextBox.Text));
                        }
                        if (this.amntRcvdNumUpDown.Value > prepayAvlblAmnt)
                        {
                            cmnCde.showMsg("Applied Prepayment Amount Exceeds the Available Amount \r\n on the selected Prepayment Document!", 0);
                            this.processPayButton.Enabled = true;
                            this.prcsngPay = false;
                            return;
                        }
                    }
                }
            }

            if (this.amntToPay == 0 && this.createPrepay == false)
            {
                cmnCde.showMsg("Cannot Repay a Fully Paid Document!", 0);
                this.processPayButton.Enabled = true;
                this.prcsngPay = false;
                return;
            }

            if (this.amntToPay < 0 && this.amntRcvdNumUpDown.Value > 0)
            {
                cmnCde.showMsg("Amount Given Must be Negative(Refund) \r\nif Amount to Pay is Negative(Refund)!", 0);
                this.processPayButton.Enabled = true;
                this.prcsngPay = false;
                return;
            }
            if (this.orgnlPymntID > 0)
            {
                if (this.isPymntRvrsdB4(this.orgnlPymntID))
                {
                    cmnCde.showMsg("This Payment has been Reversed Already!", 0);
                    this.processPayButton.Enabled = true;
                    this.prcsngPay = false;
                    return;
                }
            }

            if (this.createPrepay == true && this.spplrID <= 0)
            {
                cmnCde.showMsg("Cannot Create Advance Payment when Customer/Supplier is not Specified!", 0);
                this.processPayButton.Enabled = true;
                this.prcsngPay = false;
                return;
            }
            if (this.msPyID <= 0 && this.orgnlPymntID <= 0)
            {
                if (cmnCde.showMsg("Are you sure you want to PROCESS this Payment?" +
                "\r\nThis action cannot be undone!", 1) == DialogResult.No)
                {
                    //cmnCde.showMsg("Operation Cancelled!", 4);
                    this.processPayButton.Enabled = true;
                    this.prcsngPay = false;
                    return;
                }
            }

            if (this.msPyID <= 0 && this.orgnlPymntID > 0)
            {
                if (cmnCde.showMsg("Are you sure you want to REVERSE this Payment?" +
                "\r\nThis action cannot be undone!", 1) == DialogResult.No)
                {
                    //cmnCde.showMsg("Operation Cancelled!", 4);
                    this.processPayButton.Enabled = true;
                    this.prcsngPay = false;
                    return;
                }
            }
            if (this.orgnlPymntID > 0 && this.msPyID > 0)
            {
                this.rollBackMsPay(this.msPyID);
            }

            if (this.createPrepay == true && this.spplrID > 0 && this.orgnlPymntID <= 0)
            {
                if (docTypes == "Supplier Payments")
                {
                    this.checkNCreatePyblsHdr();
                }
                else
                {
                    this.checkNCreateRcvblsHdr();
                }
                this.dsablPayments = false;
                this.createPrepay = false;
                this.amntToPay = (double)this.amntPaidNumUpDown.Value;

                this.addPymntDiag_Load(this, e);
            }

            this.processPayButton.Enabled = false;
            double amntPaid = (double)this.amntPaidNumUpDown.Value;

            string dateStr = DateTime.ParseExact(
         cmnCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
            string dteRcvd = this.dteRcvdTextBox.Text;
            if (dteRcvd.Length <= 11)
            {
                dteRcvd = dteRcvd + " 12:00:00";
            }
            string pymntBatchName = "";
            string docClsftn = "";
            string docNum = "";
            long pymntBatchID = -1;
            string glBatchPrfx = "";
            string glBatchSrc = "";
            if (this.docTypes == "Supplier Payments")
            {
                glBatchPrfx = "PYMNT_SPPLR-";
                glBatchSrc = "Payment for Payables Invoice";
                pymntBatchName = "SPPLR_PYMNT-" +
                 DateTime.Parse(cmnCde.getFrmtdDB_Date_time()).ToString("yyMMdd-HHmmss")
                          + "-" + cmnCde.getRandomInt(10, 100);

                /*cmnCde.getDB_Date_time().Substring(11, 8).Replace(":", "").Replace("-", "").Replace(" ", "") + "-" +
               this.getNewPymntBatchID().ToString().PadLeft(4, '0');*/

                docClsftn = cmnCde.getGnrlRecNm(
                     "accb.accb_pybls_invc_hdr", "pybls_invc_hdr_id", "doc_tmplt_clsfctn",
                     this.srcDocID);

                docNum = cmnCde.getGnrlRecNm(
               "accb.accb_pybls_invc_hdr", "pybls_invc_hdr_id", "pybls_invc_number",
               this.srcDocID);
            }
            else
            {
                glBatchPrfx = "PYMNT_CSTMR-";
                glBatchSrc = "Payment for Receivables Invoice";
                pymntBatchName = "CSTMR_PYMNT-" +
                 DateTime.Parse(cmnCde.getFrmtdDB_Date_time()).ToString("yyMMdd-HHmmss")
                          + "-" + cmnCde.getRandomInt(10, 100);

                /*cmnCde.getDB_Date_time().Substring(11, 8).Replace(":", "").Replace("-", "").Replace(" ", "") + "-" +
            this.getNewPymntBatchID().ToString().PadLeft(4, '0');*/

                docClsftn = cmnCde.getGnrlRecNm(
                     "accb.accb_rcvbls_invc_hdr", "rcvbls_invc_hdr_id", "doc_tmplt_clsfctn",
                     this.srcDocID);

                docNum = cmnCde.getGnrlRecNm(
               "accb.accb_rcvbls_invc_hdr", "rcvbls_invc_hdr_id", "rcvbls_invc_number",
               this.srcDocID);
            }

            pymntBatchID = cmnCde.getGnrlRecID("accb.accb_payments_batches",
             "pymnt_batch_name", "pymnt_batch_id", pymntBatchName, cmnCde.Org_id);
            if (pymntBatchID <= 0)
            {
                this.createPymntsBatch(cmnCde.Org_id, dteRcvd, dteRcvd,
                  this.srcDocType, pymntBatchName, pymntBatchName, this.spplrID,
                  this.pymntMthdID, this.docTypes, this.orgnlPymntBatchID, "VALID", docClsftn, "Unprocessed");

                if (this.orgnlPymntBatchID > 0)
                {
                    this.updateBatchVldtyStatus(this.orgnlPymntBatchID, "VOID");
                }
            }
            else
            {
                cmnCde.showMsg("Payment Batch Could not be Created!\r\n Try Again Later!", 0);
                this.processPayButton.Enabled = true;
                this.prcsngPay = false;
                return;
            }
            pymntBatchID = cmnCde.getGnrlRecID("accb.accb_payments_batches",
              "pymnt_batch_name", "pymnt_batch_id", pymntBatchName, cmnCde.Org_id);
            string glBatchName = glBatchPrfx +
              DateTime.Parse(cmnCde.getFrmtdDB_Date_time()).ToString("yyMMdd-HHmmss")
                       + "-" + cmnCde.getRandomInt(10, 100);

            /*cmnCde.getDB_Date_time().Substring(11, 8).Replace(":", "").Replace("-", "").Replace(" ", "") + "-" +
         this.getNewBatchID().ToString().PadLeft(4, '0');*/
            long glBatchID = cmnCde.getGnrlRecID("accb.accb_trnsctn_batches",
              "batch_name", "batch_id", glBatchName, cmnCde.Org_id);

            if (glBatchID <= 0)
            {
                this.createBatch(cmnCde.Org_id, glBatchName,
                  this.pymntCmmntsTextBox.Text + " (" + docNum + ")",
                  glBatchSrc, "VALID", this.orgnlGLBatchID, "0");
                if (this.orgnlGLBatchID > 0)
                {
                    this.updateBatchVldtyStatus(this.orgnlGLBatchID, "VOID");
                }
            }
            else
            {
                cmnCde.showMsg("GL Batch Could not be Created!\r\n Try Again Later!", 0);
                this.processPayButton.Enabled = true;
                this.prcsngPay = false;
                return;
            }
            glBatchID = cmnCde.getGnrlRecID("accb.accb_trnsctn_batches",
              "batch_name", "batch_id", glBatchName, cmnCde.Org_id);

            long pymntID = -1;
            if (pymntBatchID > 0 && glBatchID > 0)
            {
                pymntID = this.getNewPymntLnID();

                this.createPymntDet(pymntID, pymntBatchID, this.pymntMthdID, amntPaid, this.entrdCurrID,
                  (double)this.changeNumUpDown.Value,
                  this.pymntCmmntsTextBox.Text, this.srcDocType, this.srcDocID, dteRcvd
                  , this.incrsDcrs2ComboBox.Text.Substring(0, 1), int.Parse(this.blcngAccntIDTextBox.Text)
                  , this.incrsDcrs1ComboBox.Text.Substring(0, 1), int.Parse(this.chrgeAccntIDTextBox.Text), glBatchID,
                  "VALID", this.orgnlPymntID, int.Parse(this.funcCurrIDTextBox.Text), int.Parse(this.accntCurrIDTextBox.Text),
                  (double)this.funcCurRateNumUpDwn.Value, (double)this.accntCurRateNumUpDwn.Value,
                  (double)this.funcCurAmntNumUpDwn.Value, (double)this.accntCurrNumUpDwn.Value, prepayDocID,
                  prepayDocType, this.otherInfoTextBox.Text, this.cardNameTextBox.Text, this.expDateTextBox.Text,
                  this.cardNumTextBox.Text, this.sigCodeTextBox.Text, this.bkgAtvtyStatusTextBox.Text, this.bkgDocNameTextBox.Text,
                  this.msPyID);

                if (this.orgnlPymntID > 0)
                {
                    this.updtPymntsLnVldty(this.orgnlPymntID, "VOID");
                }
            }
            this.CreatePymntAccntngTrns(int.Parse(this.chrgeAccntIDTextBox.Text), glBatchID, this.incrsDcrs1ComboBox.Text.Substring(0, 1));
            this.CreatePymntAccntngTrns(int.Parse(this.blcngAccntIDTextBox.Text), glBatchID, this.incrsDcrs2ComboBox.Text.Substring(0, 1));
            if (this.get_Batch_CrdtSum(glBatchID) == this.get_Batch_DbtSum(glBatchID))
            {
                //double pymntsAmnt = this.getPyblsDocTtlPymnts(this.srcDocID, this.srcDocType);
                if (this.docTypes == "Supplier Payments")
                {
                    this.updtPyblsDocAmntPaid(this.srcDocID, amntPaid);
                    if (prepayDocID > 0)
                    {
                        this.updtPyblsDocAmntAppld(prepayDocID, lnAmnt);
                        string pepyDocType = cmnCde.getGnrlRecNm(
                    "accb.accb_pybls_invc_hdr", "pybls_invc_hdr_id", "pybls_invc_type",
                    prepayDocID);
                        if (pepyDocType == "Supplier Credit Memo (InDirect Refund)"
                            || pepyDocType == "Supplier Debit Memo (InDirect Topup)")
                        {
                            this.updtPyblsDocAmntPaid(prepayDocID, lnAmnt);
                        }
                    }
                    //this.reCalcPsSmmrys(this.srcDocID, this.srcDocType);
                }
                else
                {
                    this.updtRcvblsDocAmntPaid(this.srcDocID, amntPaid);
                    if (prepayDocID > 0)
                    {
                        this.updtRcvblsDocAmntAppld(prepayDocID, lnAmnt);
                        string pepyDocType = cmnCde.getGnrlRecNm(
                    "accb.accb_rcvbls_invc_hdr", "rcvbls_invc_hdr_id", "rcvbls_invc_type",
                    prepayDocID);
                        if (pepyDocType == "Customer Credit Memo (InDirect Topup)"
                          || pepyDocType == "Customer Debit Memo (InDirect Refund)")
                        {
                            this.updtRcvblsDocAmntPaid(prepayDocID, lnAmnt);
                        }
                    }
                    this.reCalcRcvblsSmmrys(this.srcDocID, this.srcDocType);
                }
                if (this.srcDocType == "Supplier Credit Memo (InDirect Refund)"
                  || this.srcDocType == "Supplier Debit Memo (InDirect Topup)")
                {
                    this.updtPyblsDocAmntAppld(this.srcDocID, amntPaid);
                }
                else if (this.srcDocType == "Customer Credit Memo (InDirect Topup)"
                  || this.srcDocType == "Customer Debit Memo (InDirect Refund)")
                {
                    this.updtRcvblsDocAmntAppld(this.srcDocID, amntPaid);
                }
                this.updtPymntBatchStatus(pymntBatchID, "Processed");
                this.updateBatchAvlblty(glBatchID, "1");

                if (this.srcDocType.Contains("Advance") && this.orgnlPymntID > 0
          && this.shdRcvblsDocBeCancelled(this.srcDocID) == true)
                {
                    this.rcvblCanclltnProcess();
                }

                if ((this.srcDocType.Contains("Advance")
                  || this.lnkdDocID <= 0)
                  && this.pymntHistoryButton.Text.Contains("Show"))
                {
                    this.processPayButton.Enabled = false;
                    System.Windows.Forms.Application.DoEvents();
                    this.pymntHistoryButton.PerformClick();
                    this.processPayButton.Enabled = false;
                    this.groupBox1.Enabled = false;
                    this.groupBox2.Enabled = false;
                    this.groupBox4.Enabled = false;
                    this.prcsngPay = false;
                    return;
                }
                //cmnCde.showMsg(this.orgnlPymntID + "/" + this.srcDocType +
                //  "/" + this.shdRcvblsDocBeCancelled(this.srcDocID).ToString(), 0);
            }
            else
            {
                cmnCde.showMsg(@"The GL Batch created is not Balanced!\r\nTransactions created will be reversed and deleted!", 0);
                this.deleteBatchTrns(glBatchID);
                this.deleteBatch(glBatchID, glBatchName);
                this.deletePymntsBatchNDet(pymntBatchID, pymntBatchName);
                //this.deletePymntsDet(pymntID);
                this.processPayButton.Enabled = true;
                this.prcsngPay = false;
                return;
            }

            this.prcsngPay = false;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public double get_RcvblPrepayDocAppldAmnt(long prepayDocID)
        {
            string strSql = "SELECT invc_amnt_appld_elswhr " +
              "FROM accb.accb_rcvbls_invc_hdr a " +
              "WHERE(a.rcvbls_invc_hdr_id = " + prepayDocID +
              " and (invc_amnt_appld_elswhr)>0)";

            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return double.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return 0;
        }

        private void rcvblCanclltnProcess()
        {
            long rcvblHdrID = this.srcDocID;
            string rcvblDoctype = this.srcDocType;
            double pymntsAmnt = Math.Round(this.getRcvblsDocTtlPymnts(rcvblHdrID, rcvblDoctype), 2);
            double amntAppldEslwhr = this.get_RcvblPrepayDocAppldAmnt(rcvblHdrID);

            //double amntAppldEslwhr = 0;//invc_amnt_appld_elswhr
            if (pymntsAmnt != 0)
            {
                cmnCde.showMsg("Please Reverse all Payments on this Document First!" +
                 "\r\n(TOTAL AMOUNT PAID=" + pymntsAmnt.ToString("#,##0.00") + ")", 0);
                return;
            }

            if (amntAppldEslwhr > 0)
            {
                cmnCde.showMsg("Please Release this Document from all Other Documents it has been applied to First!", 0);
                return;
            }

            string dateStr = cmnCde.getFrmtdDB_Date_time();
            bool sccs = true;
            if (sccs)
            {
                sccs = this.voidAttachedBatch(rcvblHdrID, rcvblDoctype);
            }
            if (sccs)
            {
                this.updtRcvblsDocApprvl(this.srcDocID, "Cancelled", "None");
            }
        }

        private bool voidAttachedBatch(long rcvblHdrID, string rcvblDocType)
        {
            try
            {
                long glbatchID = long.Parse(cmnCde.getGnrlRecNm(
            "accb.accb_rcvbls_invc_hdr", "rcvbls_invc_hdr_id", "gl_batch_id", rcvblHdrID));
                //     string glbatchstatus = cmnCde.getGnrlRecNm(
                //"accb.accb_trnsctn_batches", "batch_id", "batch_status", glbatchID);
                string glbatchNm = cmnCde.getGnrlRecNm(
            "accb.accb_trnsctn_batches", "batch_id", "batch_name", glbatchID);
                string glbatchDesc = cmnCde.getGnrlRecNm(
            "accb.accb_trnsctn_batches", "batch_id", "batch_description", glbatchID);
                //Void Batch
                string dateStr = cmnCde.getFrmtdDB_Date_time();
                //Begin Process of voiding
                long beenPstdB4 = this.getSimlrPstdBatchID(
                 glbatchID, glbatchNm, cmnCde.Org_id);
                if (beenPstdB4 > 0)
                {
                    {
                        return true;
                    }
                }
                string glNwBatchName = glbatchNm + " (Receivables Document Cancellation@" + dateStr + ")";
                long nwbatchid = cmnCde.getGnrlRecID("accb.accb_trnsctn_batches",
                  "batch_name", "batch_id", glNwBatchName, cmnCde.Org_id);

                if (nwbatchid <= 0)
                {
                    this.createBatch(cmnCde.Org_id,
                     glNwBatchName,
                     glbatchDesc + " (Receivables Document Cancellation@" + dateStr + ")",
                     "Receivables Invoice",
                     "VALID", glbatchID, "0");
                    this.updateBatchVldtyStatus(glbatchID, "VOID");
                    nwbatchid = cmnCde.getGnrlRecID("accb.accb_trnsctn_batches",
                    "batch_name", "batch_id", glNwBatchName, cmnCde.Org_id);
                }
                //Get All Posted/Unposted Transactions in current batch
                DataSet dtst = this.get_Batch_Trns_NoStatus(glbatchID);
                long ttltrns = dtst.Tables[0].Rows.Count;
                for (int i = 0; i < ttltrns; i++)
                {
                    this.createTransaction(int.Parse(dtst.Tables[0].Rows[i][9].ToString()),
                    dtst.Tables[0].Rows[i][3].ToString() + " (Receivables Document Cancellation)",
                    -1 * double.Parse(dtst.Tables[0].Rows[i][4].ToString()),
                    dtst.Tables[0].Rows[i][6].ToString(), int.Parse(dtst.Tables[0].Rows[i][7].ToString()),
                    nwbatchid, -1 * double.Parse(dtst.Tables[0].Rows[i][5].ToString()),
                    -1 * double.Parse(dtst.Tables[0].Rows[i][10].ToString()),
               -1 * double.Parse(dtst.Tables[0].Rows[i][12].ToString()),
               int.Parse(dtst.Tables[0].Rows[i][13].ToString()),
               -1 * double.Parse(dtst.Tables[0].Rows[i][14].ToString()),
               int.Parse(dtst.Tables[0].Rows[i][15].ToString()),
               double.Parse(dtst.Tables[0].Rows[i][16].ToString()),
               double.Parse(dtst.Tables[0].Rows[i][17].ToString()),
               dtst.Tables[0].Rows[i][18].ToString(), dtst.Tables[0].Rows[i][19].ToString());
                }
                //}
                this.updateBatchAvlblty(nwbatchid, "1");
                //this.rvrsAppldPrepayHdrs();
                return true;
            }
            catch (Exception ex)
            {
                cmnCde.showMsg(ex.InnerException.ToString(), 0);
                return false;
            }
        }

        public long getSimlrPstdBatchID(long srcbatchid, string orgnlbatchname, int orgid)
        {
            string strSql = "";
            strSql = "SELECT a.batch_id " +
         "FROM accb.accb_trnsctn_batches a " +
            "WHERE (((a.src_batch_id = " + srcbatchid.ToString() +
              ") or (a.batch_name ilike '" + orgnlbatchname.Replace("'", "''") +
              "' AND a.batch_vldty_status = 'VOID')) AND (a.org_id = " + orgid + "))";// AND (a.batch_status='1')

            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public DataSet get_Batch_Trns_NoStatus(long batchID)
        {
            string strSql = "";
            strSql = "SELECT a.transctn_id, b.accnt_num, b.accnt_name, " +
              "a.transaction_desc, a.dbt_amount, a.crdt_amount, " +
                    "to_char(to_timestamp(a.trnsctn_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), a.func_cur_id, " +
                    "a.batch_id, a.accnt_id, a.net_amount, a.trns_status, a.entered_amnt, a.entered_amt_crncy_id, " +
                    "a.accnt_crncy_amnt, a.accnt_crncy_id, a.func_cur_exchng_rate, a.accnt_cur_exchng_rate, a.dbt_or_crdt, a.ref_doc_number " +
          "FROM accb.accb_trnsctn_details a LEFT OUTER JOIN " +
          "accb.accb_chart_of_accnts b on a.accnt_id = b.accnt_id " +
          "WHERE(a.batch_id = " + batchID + ") ORDER BY a.transctn_id";

            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            //this.mnFrm.trnsDet_SQL = strSql;
            return dtst;
        }

        //private void rvrsAppldPrepayHdrs()
        //{
        //  for (int i = 0; i < this.smmryDataGridView.Rows.Count; i++)
        //  {
        //    this.dfltFill(i);
        //    string lineTypeNm = this.smmryDataGridView.Rows[i].Cells[0].Value.ToString();
        //    long prepayDocID = -1;
        //    long.TryParse(this.smmryDataGridView.Rows[i].Cells[17].Value.ToString(), out prepayDocID);

        //    double lnAmnt = double.Parse(this.smmryDataGridView.Rows[i].Cells[2].Value.ToString());
        //    if (prepayDocID > 0 && lineTypeNm == "5Applied Prepayment")
        //    {
        //      this.updtRcvblsDocAmntAppld(prepayDocID, -1 * lnAmnt);
        //    }
        //    string pepyDocType = cmnCde.getGnrlRecNm(
        //"accb.accb_rcvbls_invc_hdr", "rcvbls_invc_hdr_id", "rcvbls_invc_type",
        //prepayDocID);
        //    if (pepyDocType == "Customer Credit Memo (InDirect Topup)"
        //      || pepyDocType == "Customer Debit Memo (InDirect Refund)")
        //    {
        //      this.updtRcvblsDocAmntPaid(prepayDocID, -1 * lnAmnt);
        //    }
        //  }
        //}

        private void CreatePymntAccntngTrns(int accntID, long glBatchID, string incrsdcrs)
        {
            //Create Accounting for Charge Account
            double netAmnt1 = (double)this.dbtOrCrdtAccntMultiplier(accntID,
              incrsdcrs) * (double)this.funcCurAmntNumUpDwn.Value;

            if (!cmnCde.isTransPrmttd(
              accntID, this.dteRcvdTextBox.Text, netAmnt1))
            {
                return;
            }
            char[] cw = { '-', ' ' };
            if (this.dbtOrCrdtAccnt(accntID, incrsdcrs) == "Debit")
            {
                this.createTransaction(accntID,
                  (this.pymntCmmntsTextBox.Text + " (" + cmnCde.getCstmrSpplrName(this.spplrID) + ")").Replace(" ()", ""),
                  (double)this.funcCurAmntNumUpDwn.Value,
                  this.dteRcvdTextBox.Text
                  , int.Parse(this.funcCurrIDTextBox.Text), glBatchID, 0.00, netAmnt1,
                (double)this.amntPaidNumUpDown.Value,
                int.Parse(this.crncyIDTextBox.Text),
                (double)this.accntCurrNumUpDwn.Value,
                int.Parse(this.accntCurrIDTextBox.Text),
                (double)this.funcCurRateNumUpDwn.Value,
                (double)this.accntCurRateNumUpDwn.Value, "D", (this.cardNumTextBox.Text + " - " + this.cardNameTextBox.Text).Trim(cw));
            }
            else
            {
                this.createTransaction(accntID,
                (this.pymntCmmntsTextBox.Text + " (" + cmnCde.getCstmrSpplrName(this.spplrID) + ")").Replace(" ()", ""), 0.00,
                this.dteRcvdTextBox.Text, int.Parse(this.funcCurrIDTextBox.Text),
                glBatchID, (double)this.funcCurAmntNumUpDwn.Value, netAmnt1,
            (double)this.amntPaidNumUpDown.Value,
            int.Parse(this.crncyIDTextBox.Text),
            (double)this.accntCurrNumUpDwn.Value,
            int.Parse(this.accntCurrIDTextBox.Text),
            (double)this.funcCurRateNumUpDwn.Value,
            (double)this.accntCurRateNumUpDwn.Value, "C", (this.cardNumTextBox.Text + " - " + this.cardNameTextBox.Text).Trim(cw));
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void changeNumUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (this.changeNumUpDown.Value <= 0)
            {
                this.changeNumUpDown.BackColor = Color.Lime;
            }
            else
            {
                this.changeNumUpDown.BackColor = Color.Red;
            }
        }

        private void dteRcvdTextBox_Click(object sender, EventArgs e)
        {
            this.dteRcvdTextBox.SelectAll();
        }

        private void prepayButton_Click(object sender, EventArgs e)
        {
            if (this.pymntTypeComboBox.Text.Contains("Prepayment Application") == false)
            {
                cmnCde.showMsg("Please select a Prepayment Application Payment Type First!", 0);
                return;
            }
            if (this.spplrID <= 0)
            {
                cmnCde.showMsg("Please select a Customer/Supplier First!", 0);
                return;
            }
            if (this.srcDocType == "Customer Advance Payment"
              || this.srcDocType == "Customer Credit Memo (InDirect Topup)"
               || this.srcDocType == "Customer Debit Memo (InDirect Refund)"
             || this.srcDocType == "Supplier Advance Payment"
              || this.srcDocType == "Supplier Credit Memo (InDirect Refund)"
               || this.srcDocType == "Supplier Debit Memo (InDirect Topup)")
            {
                cmnCde.showMsg("Cannot Apply Prepayments to this Document Type!", 0);
                return;
            }
            string[] selVals = new string[1];
            selVals[0] = this.prepayDocIDTextBox.Text;
            string lovNm = "Customer Prepayments";
            int advncChrgAccnt = -1;
            if (this.docTypes == "Supplier Payments")
            {
                lovNm = "Supplier Prepayments";
            }
            if (this.srcDocType == "Direct Refund to Customer")
            {
                lovNm = "Customer Credit Memos";
            }
            else if (this.srcDocType == "Direct Refund from Supplier")
            {
                lovNm = "Supplier Debit Memos";
            }
            string extrWhere = "";// " and (chartonumeric(tbl1.a) NOT IN (Select appld_prepymnt_doc_id FROM accb.accb_rcvbl_amnt_smmrys WHERE src_rcvbl_hdr_id =" + this.srcDocID + "))";
                                  //string extrWhere = " and (chartonumeric(tbl1.a) NOT IN (Select appld_prepymnt_doc_id FROM accb.accb_pybls_amnt_smmrys WHERE src_pybls_hdr_id =" + this.srcDocID + "))";

            DialogResult dgRes = cmnCde.showPssblValDiag(
                cmnCde.getLovID(lovNm), ref selVals,
                false, true, cmnCde.Org_id,
                this.spplrID.ToString(), this.entrdCurrID.ToString(), "%", "Both", false, extrWhere);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.prepayDocIDTextBox.Text = selVals[i];
                    if (docTypes == "Supplier Payments")
                    {
                        this.prepayDocNumTextBox.Text = cmnCde.getGnrlRecNm(
                   "accb.accb_pybls_invc_hdr", "pybls_invc_hdr_id", "pybls_invc_number",
                   long.Parse(selVals[i]));
                        string prpyDocType = cmnCde.getGnrlRecNm(
                   "accb.accb_pybls_invc_hdr", "pybls_invc_hdr_id", "pybls_invc_type",
                   long.Parse(selVals[i]));
                        advncChrgAccnt = this.getPyblsDocAdvncAccnt(long.Parse(selVals[i]), prpyDocType);

                        this.amntRcvdNumUpDown.Value = Decimal.Parse(cmnCde.getGnrlRecNm(
                   "accb.accb_pybls_invc_hdr", "pybls_invc_hdr_id", "invoice_amount-invc_amnt_appld_elswhr",
                   long.Parse(selVals[i])));
                    }
                    else
                    {
                        this.prepayDocNumTextBox.Text = cmnCde.getGnrlRecNm(
                   "accb.accb_rcvbls_invc_hdr", "rcvbls_invc_hdr_id", "rcvbls_invc_number",
                   long.Parse(selVals[i]));

                        string prpyDocType = cmnCde.getGnrlRecNm(
            "accb.accb_rcvbls_invc_hdr", "rcvbls_invc_hdr_id", "rcvbls_invc_type",
            long.Parse(selVals[i]));
                        advncChrgAccnt = this.getRcvblsDocAdvncAccnt(long.Parse(selVals[i]), prpyDocType);

                        this.amntRcvdNumUpDown.Value = Decimal.Parse(cmnCde.getGnrlRecNm(
                   "accb.accb_rcvbls_invc_hdr", "rcvbls_invc_hdr_id", "invoice_amount-invc_amnt_appld_elswhr",
                   long.Parse(selVals[i])));
                    }
                    this.amntRcvdNumUpDown.Focus();
                    this.amntRcvdNumUpDown.Select();
                    //string smmryNm = "Applied Prepayment";
                    //this.createRcvblsDocRows(1, "5Applied Prepayment", smmryNm, -1, );
                    this.chrgeAccntIDTextBox.Text = advncChrgAccnt.ToString();
                    this.chrgeAccntTextBox.Text = cmnCde.getAccntNum(advncChrgAccnt) +
                      "." + cmnCde.getAccntName(advncChrgAccnt);

                }
            }

            System.Windows.Forms.Application.DoEvents();
            this.amntRcvdNumUpDown.Focus();
            this.amntRcvdNumUpDown.Select();
            this.amntRcvdNumUpDown.Focus();
        }

        private void pymntHistoryButton_Click(object sender, EventArgs e)
        {
            if (this.pymntHistoryButton.Text.Contains("Hide"))
            {
                this.Size = new Size(505, 490);
                if (this.StartPosition != FormStartPosition.CenterParent)
                {
                    this.Location = new Point(this.Location.X + 510, this.Location.Y);
                }
                this.pymntHistoryButton.Text = "Show Payment History";
            }
            else
            {
                this.Size = new Size(1015, 490);
                if (this.StartPosition != FormStartPosition.CenterParent)
                {
                    this.Location = new Point(this.Location.X - 510, this.Location.Y);
                }
                this.pymntHistoryButton.Text = "Hide Payment History";
                this.populateTrnsGridVw();
                if (this.trnsSearchListView.Items.Count > 0)
                {
                    this.trnsSearchListView.Items[0].Selected = true;
                }
                this.processPayButton.Enabled = true;
            }
        }

        private void amntToPayNumUpDwn_ValueChanged(object sender, EventArgs e)
        {

        }

        private void rvrsPymntButton_Click(object sender, EventArgs e)
        {
            if (!cmnCde.isTransPrmttd(
        cmnCde.get_DfltCashAcnt(cmnCde.Org_id), this.dteRcvdTextBox.Text, 200))
            {
                return;
            }
            if (this.docStatus == "Cancelled")
            {
                cmnCde.showMsg("Cannot Reverse Payments on Cancelled Documents!", 0);
                return;
            }
            if (this.trnsSearchListView.SelectedItems.Count <= 0
              && this.trnsSearchListView.Items.Count >= 1)
            {
                this.trnsSearchListView.Items[0].Selected = true;
            }
            if (this.trnsSearchListView.SelectedItems.Count <= 0)
            {
                cmnCde.showMsg("Please select a Payment for Reversal!", 0);
                return;
            }

            string srcdocTyp = this.trnsSearchListView.SelectedItems[0].SubItems[1].Text;
            string pymntMthNm = this.trnsSearchListView.SelectedItems[0].SubItems[3].Text;
            if (pymntMthNm.ToLower().Contains("supplier")
              || pymntMthNm.ToLower().Contains("petty"))
            {
                this.docTypes = "Supplier Payments";
            }
            else
            {
                this.docTypes = "Customer Payments";
            }
            if (this.docTypes == "Supplier Payments")
            {
                if (cmnCde.test_prmssns(this.dfltPrvldgs[71]) == false)
                {
                    cmnCde.showMsg("You don't have permission to perform" +
                        " this action!\nContact your System Administrator!", 0);
                    return;
                }
            }
            else
            {
                if (cmnCde.test_prmssns(this.dfltPrvldgs[72]) == false)
                {
                    cmnCde.showMsg("You don't have permission to perform" +
                        " this action!\nContact your System Administrator!", 0);
                    return;
                }
            }
            long pymntID = long.Parse(this.trnsSearchListView.SelectedItems[0].SubItems[10].Text);
            long pymntBatchID = long.Parse(this.trnsSearchListView.SelectedItems[0].SubItems[12].Text);
            long glBatchID = long.Parse(this.trnsSearchListView.SelectedItems[0].SubItems[14].Text);
            this.pymntMthdID = int.Parse(this.trnsSearchListView.SelectedItems[0].SubItems[24].Text);
            if (this.isPymntRvrsdB4(pymntID))
            {
                cmnCde.showMsg("This Payment has been Reversed Already!", 0);
                return;
            }
            double amntGvn = -1 * double.Parse(this.trnsSearchListView.SelectedItems[0].SubItems[4].Text);
            double amntPaid = -1 * double.Parse(this.trnsSearchListView.SelectedItems[0].SubItems[5].Text);
            double chngAmnt = -1 * double.Parse(this.trnsSearchListView.SelectedItems[0].SubItems[6].Text);
            long srcdocHdrID = long.Parse(this.trnsSearchListView.SelectedItems[0].SubItems[9].Text);
            string dteRcvd = this.trnsSearchListView.SelectedItems[0].SubItems[7].Text;
            string pyTyp = this.trnsSearchListView.SelectedItems[0].SubItems[3].Text;
            string pyDesc = "(REVERSAL) " + this.trnsSearchListView.SelectedItems[0].SubItems[8].Text;

            string dateStr = cmnCde.getFrmtdDB_Date_time();

            this.splitContainer1.Panel1Collapsed = false;
            this.dsablPayments = false;
            this.createPrepay = false;
            //addPymntDiag nwdiag = new addPymntDiag();

            this.prepayDocIDTextBox.Text = this.trnsSearchListView.SelectedItems[0].SubItems[16].Text;
            this.prepayDocNumTextBox.Text = this.trnsSearchListView.SelectedItems[0].SubItems[15].Text; ;
            this.otherInfoTextBox.Text = this.trnsSearchListView.SelectedItems[0].SubItems[17].Text;
            this.cardNameTextBox.Text = this.trnsSearchListView.SelectedItems[0].SubItems[18].Text;
            this.expDateTextBox.Text = this.trnsSearchListView.SelectedItems[0].SubItems[19].Text;
            this.cardNumTextBox.Text = this.trnsSearchListView.SelectedItems[0].SubItems[20].Text;
            this.sigCodeTextBox.Text = cmnCde.decrypt(this.trnsSearchListView.SelectedItems[0].SubItems[21].Text, CommonCodes.AppKey);
            this.bkgAtvtyStatusTextBox.Text = this.trnsSearchListView.SelectedItems[0].SubItems[22].Text;
            this.bkgDocNameTextBox.Text = this.trnsSearchListView.SelectedItems[0].SubItems[23].Text;
            this.docNum = this.trnsSearchListView.SelectedItems[0].SubItems[2].Text;

            this.orgnlPymntID = pymntID;
            this.orgnlPymntBatchID = pymntBatchID;
            this.orgnlGLBatchID = glBatchID;
            this.pymntCmmntsTextBox.Text = pyDesc;
            this.amntToPay = amntPaid;
            this.amntRcvdNumUpDown.Value = (decimal)amntGvn;
            this.amntToPayNumUpDwn.Value = (decimal)amntPaid;
            this.amntPaidNumUpDown.Value = (decimal)amntPaid;
            this.changeNumUpDown.Value = (decimal)chngAmnt;
            this.funcCurRateNumUpDwn.Value = decimal.Parse(cmnCde.getGnrlRecNm("accb.accb_payments", "pymnt_id", "func_curr_rate", pymntID));
            this.accntCurRateNumUpDwn.Value = decimal.Parse(cmnCde.getGnrlRecNm("accb.accb_payments", "pymnt_id", "accnt_curr_rate", pymntID));
            this.funcCurAmntNumUpDwn.Value = -1 * decimal.Parse(cmnCde.getGnrlRecNm("accb.accb_payments", "pymnt_id", "func_curr_amount", pymntID));
            this.accntCurrNumUpDwn.Value = -1 * decimal.Parse(cmnCde.getGnrlRecNm("accb.accb_payments", "pymnt_id", "accnt_curr_amnt", pymntID));

            this.orgid = cmnCde.Org_id;
            this.entrdCurrID = int.Parse(cmnCde.getGnrlRecNm("accb.accb_payments", "pymnt_id", "entrd_curr_id", pymntID));
            this.pymntMthdID = int.Parse(cmnCde.getGnrlRecNm("accb.accb_payments", "pymnt_id", "pymnt_mthd_id", pymntID));
            this.msPyID = long.Parse(cmnCde.getGnrlRecNm("accb.accb_payments", "pymnt_id", "intnl_pay_trns_id", pymntID));

            this.srcDocID = srcdocHdrID;
            this.srcDocType = srcdocTyp;
            this.spplrID = int.Parse(cmnCde.getGnrlRecNm("accb.accb_payments_batches", "pymnt_batch_id", "cust_spplr_id", pymntBatchID));
            this.groupBox4.Enabled = false;
            this.groupBox1.Enabled = false;
            this.groupBox2.Enabled = false;
            //this.Location = new Point(this.rvrsPymntButton.Location.X + 135, this.rvrsPymntButton.Location.Y - 10);
            //this.StartPosition = FormStartPosition.CenterParent;
            //this.ShowDialog();
            //this.populateTrnsGridVw();
            this.StartPosition = FormStartPosition.CenterParent;
            //if (this.pymntHistoryButton.Text.Contains("Hide"))
            //{
            //  this.pymntHistoryButton.PerformClick();
            //}
            this.StartPosition = FormStartPosition.CenterParent;
            this.obey_evnts = false;
            this.pymntTypeComboBox.Items.Clear();
            this.pymntTypeComboBox.Items.Add(this.pymntMthdID + "-" + pymntMthNm);
            this.pymntTypeComboBox.SelectedIndex = 0;
            if ((this.pymntTypeComboBox.Text.Contains("Check")
               || this.pymntTypeComboBox.Text.Contains("Cheque")))
            {
                this.cardNameTextBox.BackColor = Color.FromArgb(255, 255, 128);
                this.cardNumTextBox.BackColor = Color.FromArgb(255, 255, 128);
            }
            else
            {
                this.cardNameTextBox.BackColor = Color.FromArgb(255, 255, 255);
                this.cardNumTextBox.BackColor = Color.FromArgb(255, 255, 255);
            }
            int blcngAccntID = int.Parse(cmnCde.getGnrlRecNm("accb.accb_payments", "pymnt_id", "rcvbl_lblty_accnt_id", pymntID));
            int chrgAccntID = int.Parse(cmnCde.getGnrlRecNm("accb.accb_payments", "pymnt_id", "cash_or_suspns_acnt_id", pymntID));

            if (chrgAccntID > 0 && blcngAccntID > 0)
            {
                string incrs1 = cmnCde.getGnrlRecNm("accb.accb_payments", "pymnt_id", "CASE WHEN incrs_dcrs2='I' THEN 'INCREASE' ELSE 'DECREASE' END", pymntID);
                this.incrsDcrs1ComboBox.Items.Clear();
                this.incrsDcrs1ComboBox.Items.Add(incrs1);
                this.incrsDcrs1ComboBox.SelectedItem = incrs1;
                this.chrgeAccntIDTextBox.Text = chrgAccntID.ToString();
                this.chrgeAccntTextBox.Text = cmnCde.getAccntNum(chrgAccntID) +
                  "." + cmnCde.getAccntName(chrgAccntID);

                string incrs2 = cmnCde.getGnrlRecNm("accb.accb_payments", "pymnt_id", "CASE WHEN incrs_dcrs1='I' THEN 'INCREASE' ELSE 'DECREASE' END", pymntID);
                this.incrsDcrs2ComboBox.Items.Clear();
                this.incrsDcrs2ComboBox.Items.Add("DECREASE");
                this.incrsDcrs2ComboBox.SelectedItem = "DECREASE";
                this.blcngAccntIDTextBox.Text = blcngAccntID.ToString();
                this.blncAccntTextBox.Text = cmnCde.getAccntNum(blcngAccntID) +
                  "." + cmnCde.getAccntName(blcngAccntID);
            }

            if (this.docTypes == "Supplier Payments")
            {
                if (this.pymntTypeComboBox.Text.Contains("Prepayment")
               || this.pymntTypeComboBox.Text.Contains("Advance"))
                {
                    this.amntGvnLabel.Text = "Actual Amount Applied:";
                    this.prepayButton.Enabled = true;
                    this.prepayDocNumTextBox.Enabled = true;
                }
                else
                {
                    this.amntGvnLabel.Text = "Actual Amount Sent:";
                    this.prepayButton.Enabled = false;
                    this.prepayDocNumTextBox.Enabled = false;
                }
            }
            else
            {
                if (this.pymntTypeComboBox.Text.Contains("Prepayment")
               || this.pymntTypeComboBox.Text.Contains("Advance"))
                {
                    this.amntGvnLabel.Text = "Actual Amount Applied:";
                    this.prepayButton.Enabled = true;
                    this.prepayDocNumTextBox.Enabled = true;
                }
                else
                {
                    this.amntGvnLabel.Text = "Actual Amount Received:";
                    this.prepayButton.Enabled = false;
                    this.prepayDocNumTextBox.Enabled = false;
                }
            }

            this.addPymntDiag_Load(this, e);
            //if (pymntMthNm.ToLower().Contains("supplier")
            // || pymntMthNm.ToLower().Contains("petty"))
            //{
            //  this.docTypes = "Supplier Payments";
            //}
            //else
            //{
            //  this.docTypes = "Customer Payments";
            //}
            //this.pymntTypeComboBox_SelectedIndexChanged(this.pymntTypeComboBox, e);
            this.dteRcvdTextBox.Text = dteRcvd;
            this.processPayButton.PerformClick();
            this.docNum = cmnCde.getGnrlRecNm("accb.accb_payments", "pymnt_id", "accb.get_src_doc_num(src_doc_id, src_doc_typ)", pymntID);
            if (srcdocTyp.ToLower().Contains("supplier"))
            {
                this.docTypes = "Supplier Payments";
            }
            else
            {
                this.docTypes = "Customer Payments";
            }
            this.populateTrnsGridVw();
        }

        public DataSet getMsPyToRllBck(long mspyid)
        {
            string strSql = @"SELECT a.pay_trns_id, a.person_id, a.item_id, a.amount_paid, 
to_char(to_timestamp(a.paymnt_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), a.paymnt_source, " +
                  "a.pay_trns_type, a.pymnt_desc, -1, a.crncy_id, c.local_id_no, trim(c.title || ' ' || c.sur_name || " +
               "', ' || c.first_name || ' ' || c.other_names) fullname, b.item_code_name, b.item_value_uom, b.item_maj_type, b.item_min_type " +
             "FROM (pay.pay_itm_trnsctns a LEFT OUTER JOIN org.org_pay_items b ON a.item_id = b.item_id) " +
             "LEFT OUTER JOIN prs.prsn_names_nos c on a.person_id = c.person_id " +
             "WHERE(a.mass_pay_id = " + mspyid + ") ORDER BY a.pay_trns_id ";

            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            return dtst;
        }

        public void createMsPy(int orgid, string mspyname,
       string mspydesc, string trnsdte, int prstid, int itmstid, string glDate)
        {
            trnsdte = DateTime.ParseExact(
         trnsdte, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

            glDate = DateTime.ParseExact(
         glDate, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

            string dateStr = cmnCde.getDB_Date_time();
            string insSQL = "INSERT INTO pay.pay_mass_pay_run_hdr(" +
                  "mass_pay_name, mass_pay_desc, created_by, creation_date, " +
                  "last_update_by, last_update_date, run_status, mass_pay_trns_date, " +
                  "prs_st_id, itm_st_id, org_id, sent_to_gl, gl_date) " +
                  "VALUES ('" + mspyname.Replace("'", "''") +
                  "', '" + mspydesc.Replace("'", "''") +
                  "', " + cmnCde.User_id + ", '" + dateStr +
                  "', " + cmnCde.User_id + ", '" + dateStr +
                  "', '0', '" + trnsdte.Replace("'", "''") + "', " +
                  prstid + ", " + itmstid + ", " + orgid + ", '0', '" + glDate +
                  "')";
            cmnCde.insertDataNoParams(insSQL);
        }

        private void rollBackMsPay(long mspID)
        {

            string msPyNmTextBox = cmnCde.getGnrlRecNm(
              "pay.pay_mass_pay_run_hdr", "mass_pay_id", "mass_pay_name", mspID);
            if (msPyNmTextBox == "")
            {
                return;
            }
            string msPyDescTextBox = cmnCde.getGnrlRecNm(
              "pay.pay_mass_pay_run_hdr", "mass_pay_id", "mass_pay_desc", mspID);
            string msPyPrsStIDTextBox = cmnCde.getGnrlRecNm(
              "pay.pay_mass_pay_run_hdr", "mass_pay_id", "prs_st_id", mspID);
            string msPyItmStIDTextBox = cmnCde.getGnrlRecNm(
              "pay.pay_mass_pay_run_hdr", "mass_pay_id", "itm_st_id", mspID);


            string dateStr = DateTime.ParseExact(cmnCde.getGnrlRecNm(
        "pay.pay_mass_pay_run_hdr", "mass_pay_id", "mass_pay_trns_date", mspID), "yyyy-MM-dd HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
            string gldateStr = DateTime.ParseExact(cmnCde.getGnrlRecNm(
              "pay.pay_mass_pay_run_hdr", "mass_pay_id", "gl_date", mspID), "yyyy-MM-dd HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");

            long nwmspyid = cmnCde.getMsPyID(
              msPyNmTextBox + " (Reversal)",
              cmnCde.Org_id);

            if (nwmspyid <= 0)
            {
                this.createMsPy(cmnCde.Org_id,
                  msPyNmTextBox + " (Reversal)",
                  "(Reversal) " + msPyDescTextBox,
                  dateStr, int.Parse(msPyPrsStIDTextBox)
                , int.Parse(msPyItmStIDTextBox), gldateStr);
            }

            nwmspyid = cmnCde.getMsPyID(
                    msPyNmTextBox + " (Reversal)",
                    cmnCde.Org_id);

            DataSet payDtSt = this.getMsPyToRllBck(mspID);
            int payCnt = payDtSt.Tables[0].Rows.Count;


            long msg_id = cmnCde.getLogMsgID("pay.pay_mass_pay_run_msgs", "Mass Pay Run Reversal", nwmspyid);
            if (msg_id <= 0)
            {
                cmnCde.createLogMsg(dateStr + " .... Mass Pay Run Reversal is about to Start...",
            "pay.pay_mass_pay_run_msgs", "Mass Pay Run Reversal", nwmspyid, dateStr);
            }
            msg_id = cmnCde.getLogMsgID("pay.pay_mass_pay_run_msgs", "Mass Pay Run Reversal", nwmspyid);
            string retmsg = "";
            //Loop through all payments to reverse them
            for (int i = 0; i < payCnt; i++)
            {
                if (i == 0)
                {
                    //if (!this.isMsPayTrnsValid(
                    // payDtSt.Tables[0].Rows[i][13].ToString(),
                    // payDtSt.Tables[0].Rows[i][14].ToString(),
                    // payDtSt.Tables[0].Rows[i][15].ToString(),
                    // int.Parse(payDtSt.Tables[0].Rows[i][2].ToString()), gldateStr, 1000))
                    //{
                    //  this.backgroundWorker2.CancelAsync();
                    //  worker.ReportProgress(Convert.ToInt32((i + 1) * (99.0 / payCnt)));
                    //  break;
                    //}
                }

                retmsg = this.rvrsMassPay(cmnCde.Org_id,
                   long.Parse(payDtSt.Tables[0].Rows[i][1].ToString()),
                   payDtSt.Tables[0].Rows[i][10].ToString(),
                   int.Parse(payDtSt.Tables[0].Rows[i][2].ToString()),
                   payDtSt.Tables[0].Rows[i][12].ToString(),
                   payDtSt.Tables[0].Rows[i][13].ToString(),
                   nwmspyid,
                   payDtSt.Tables[0].Rows[i][4].ToString(),
                   payDtSt.Tables[0].Rows[i][6].ToString(),
                   payDtSt.Tables[0].Rows[i][14].ToString(),
                   payDtSt.Tables[0].Rows[i][15].ToString(), msg_id,
                   "pay.pay_mass_pay_run_msgs", dateStr,
                   double.Parse(payDtSt.Tables[0].Rows[i][3].ToString()),
                   int.Parse(payDtSt.Tables[0].Rows[i][9].ToString()),
                   "(Reversal) " + payDtSt.Tables[0].Rows[i][7].ToString(),
                   long.Parse(payDtSt.Tables[0].Rows[i][0].ToString()), gldateStr);
                if (retmsg == "Stop")
                {
                    //this.backgroundWorker2.CancelAsync();
                    //worker.ReportProgress(Convert.ToInt32((i + 1) * (99.0 / payCnt)));
                    break;
                }
                //System.Threading.Thread.Sleep(500);
                //worker.ReportProgress(Convert.ToInt32((i + 1) * (99.0 / payCnt)));

            }
            //Do some summation checks before updating the Status
            //Function to check if sum of debits is equal sum of credits to sum of amnts in all these pay trns
            //if correct the set gone to gl to '1' else '0'
            double pytrnsamnt = this.getMsPyAmntSum(nwmspyid);
            double intfcDbtAmnt = this.getMsPyIntfcDbtSum(nwmspyid);
            double intfcCrdtAmnt = this.getMsPyIntfcCrdtSum(nwmspyid);
            if (pytrnsamnt == intfcCrdtAmnt
              && pytrnsamnt == intfcDbtAmnt && pytrnsamnt != 0)
            {
                this.updateMsPyStatus(nwmspyid, "1", "1");
            }
            else if (pytrnsamnt != 0)
            {
                this.updateMsPyStatus(nwmspyid, "1", "0");
            }
            else if (this.get_Total_MsPyDt(nwmspyid) > 0 && intfcCrdtAmnt == 0)
            {
                this.updateMsPyStatus(nwmspyid, "1", "1");
            }
        }

        public long get_Total_MsPyDt(long mspyid)
        {
            string strSql = "";
            strSql = "SELECT count(1) " +
          "FROM (pay.pay_itm_trnsctns a LEFT OUTER JOIN org.org_pay_items b ON a.item_id = b.item_id) " +
             "LEFT OUTER JOIN prs.prsn_names_nos c on a.person_id = c.person_id " +
             "WHERE(a.mass_pay_id = " + mspyid + ")";

            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        public double getMsPyAmntSum(long mspyid)
        {
            string strSql = "SELECT SUM(a.amount_paid) FROM pay.pay_itm_trnsctns a, org.org_pay_items b " +
      @"WHERE a.item_id = b.item_id and a.pay_trns_type !='Purely Informational' 
      and b.cost_accnt_id>0 and b.bals_accnt_id>0 and a.crncy_id > 0 and a.mass_pay_id = " + mspyid;
            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            double sumRes = 0.00;
            if (dtst.Tables[0].Rows.Count > 0)
            {
                double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out sumRes);
            }
            return sumRes;
        }

        public double getMsPyIntfcDbtSum(long mspyid)
        {
            string strSql = "SELECT SUM(a.dbt_amount) FROM pay.pay_gl_interface a " +
              "WHERE a.source_trns_id IN (select b.pay_trns_id from pay.pay_itm_trnsctns b WHERE b.mass_pay_id = " + mspyid + ") ";
            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            double sumRes = 0.00;
            if (dtst.Tables[0].Rows.Count > 0)
            {
                double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out sumRes);
            }
            return sumRes;
        }

        public double getMsPyIntfcCrdtSum(long mspyid)
        {
            string strSql = "SELECT SUM(a.crdt_amount) FROM pay.pay_gl_interface a " +
              "WHERE a.source_trns_id IN (select b.pay_trns_id from pay.pay_itm_trnsctns b WHERE b.mass_pay_id = " + mspyid + ") ";
            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            double sumRes = 0.00;
            if (dtst.Tables[0].Rows.Count > 0)
            {
                double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out sumRes);
            }
            return sumRes;
        }

        public void updateMsPyStatus(long mspyid, string run_cmpltd, string to_gl_intfc)
        {
            cmnCde.Extra_Adt_Trl_Info = "";
            string dateStr = cmnCde.getDB_Date_time();
            string updtSQL = "UPDATE pay.pay_mass_pay_run_hdr " +
            "SET run_status='" + run_cmpltd.Replace("'", "''") +
            "', sent_to_gl='" + to_gl_intfc.Replace("'", "''") +
            "', last_update_by=" + cmnCde.User_id +
            ", last_update_date='" + dateStr +
            "' WHERE mass_pay_id = " + mspyid;
            cmnCde.updateDataNoParams(updtSQL);
        }

        public long getPymntRvrslTrnsID(long paytrnsid)
        {
            string strSql = @"SELECT a.pay_trns_id " +
              "FROM pay.pay_itm_trnsctns a " +
              "WHERE ((a.src_py_trns_id = "
              + paytrnsid + ") or (a.pay_trns_id = "
              + paytrnsid + " AND a.src_py_trns_id>0))";

            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public void updateTrnsVldtyStatus(long paytrnsid, string vldty)
        {
            cmnCde.Extra_Adt_Trl_Info = "";
            string dateStr = cmnCde.getDB_Date_time();
            string updtSQL = "UPDATE pay.pay_itm_trnsctns " +
            "SET pymnt_vldty_status='" + vldty.Replace("'", "''") +
            "', last_update_by=" + cmnCde.User_id +
            ", last_update_date='" + dateStr +
            "' WHERE pay_trns_id = " + paytrnsid;
            cmnCde.updateDataNoParams(updtSQL);
        }

        public long getPaymntTrnsID(long prsnid, long itmid,
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
            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public void deletePymntGLInfcLns(long pyTrnsID)
        {
            cmnCde.Extra_Adt_Trl_Info = "";
            string delSQL = "DELETE FROM pay.pay_gl_interface WHERE source_trns_id = " +
              pyTrnsID + " and gl_batch_id = -1";
            cmnCde.deleteDataNoParams(delSQL);
        }

        private string rvrsMassPay(int org_id, long prsn_id, string loc_id_no, int itm_id,
    string itm_name, string itm_uom, long mspy_id, string trns_date,
    string trns_typ, string itm_maj_typ, string itm_min_typ,
    long msg_id, string log_tbl, string dateStr, double pay_amount, int crncy_id,
      string pay_trns_desc, long orgnlPyTrnsID, string glDate)
        {
            // check if item is not balance item
            //check if person has item actively
            //get and make sure trns type is not empty
            //Get trns date
            //Get amount to pay and make sure it is not zero
            //Form a payment description
            //Check if person hasn't been paid that item in this mass pay id
            //Check if pay transaction is valid

            if (itm_maj_typ.ToUpper() == "Balance Item".ToUpper())
            {
                return "Continue";
            }
            //if (this.doesPrsnHvItm(prsn_id, itm_id, trns_date) == false)
            //{
            // return "Continue";
            //}
            if (trns_typ == "")
            {
                cmnCde.updateLogMsg(msg_id,
                  "\r\nTransaction Type not Specified for Person:" +
                  loc_id_no + " Item: " + itm_name, log_tbl, dateStr);
                return "Continue";
            }

            if (this.getPymntRvrslTrnsID(orgnlPyTrnsID) > 0)
            {
                cmnCde.updateLogMsg(msg_id,
                  "\r\nThis Payment has been reversed already or is a reversal for another Transaction:- Person:" +
                  loc_id_no + " Item: " + itm_name, log_tbl, dateStr);
                return "Continue";
            }
            /*Processing a Payment
            * 1. Create Payment line pay.pay_itm_trnsctns for Pay Value Items
            * 2. Update Daily BalsItms for all balance items this Pay value Item feeds into
            * 3. Create Tmp GL Lines in a temp GL interface Table 
            * 4. Need to check whether any of its Balance Items disallows negative balance. 
            * If Not disallow this trans if it will lead to a negative balance on a Balance Item
            */
            string crncy_cde = itm_uom;
            if (itm_uom == "Money")
            {
                crncy_cde = cmnCde.getPssblValNm(crncy_id);
            }

            if (pay_amount == 0)
            {
                return "Continue";
            }
            pay_amount = -1 * pay_amount;
            /*if paid check if
              * 1. Prsn Itm Balances have been updated by this trns_id
              * 2. Check if the debit and credit legs for this trns_id have been created in gl_interface
              * 3. Do them all if any is not done else return continue if all is done
              */
            //return "Continue";


            //if (!this.isMsPayTrnsValid(itm_uom, itm_maj_typ,
            //  itm_min_typ, itm_id, glDate, pay_amount))
            //{
            //  return "Stop";
            //}

            //Check if a Balance Item will be negative if this trns is done    
            /*double nwAmnt = this.willItmBlsBeNgtv(prsn_id, itm_id, pay_amount, trns_date);
            if (nwAmnt < 0)
            {
             cmnCde.updateLogMsg(msg_id,
         "\r\nTransaction will cause a Balance Item " +
         "to Have Negative Balance and hence cannot be allowed! Person:" +
         loc_id_no + " Item: " + itm_name, log_tbl, dateStr);
             return "Continue";
            }*/

            long tstPyTrnsID = this.hsPrsnBnPaidItmMsPy(prsn_id, itm_id,
              trns_date, pay_amount);
            if (tstPyTrnsID <= 0)
            {
                this.createPaymntLine(prsn_id, itm_id, pay_amount,
             trns_date, "Mass Pay Run Reversal",
             trns_typ, mspy_id, pay_trns_desc, crncy_id, dateStr, "VALID", orgnlPyTrnsID, glDate, "");

                this.updateTrnsVldtyStatus(orgnlPyTrnsID, "VOID");
            }
            else
            {
                cmnCde.updateLogMsg(msg_id,
            "\r\nSame Payment has been made for this Person on the same Date already! Person:" +
            loc_id_no + " Item: " + itm_name, log_tbl, dateStr);
            }

            //Update Balance Items
            this.updtBlsItms(prsn_id, itm_id, pay_amount, trns_date, "Mass Pay Run Reversal", orgnlPyTrnsID);


            this.deletePymntGLInfcLns(orgnlPyTrnsID);

            long nwpaytrnsid = this.getPaymntTrnsID(
      prsn_id, itm_id,
      pay_amount, trns_date, orgnlPyTrnsID);


            bool res = this.rvrsImprtdPymntIntrfcTrns(orgnlPyTrnsID, nwpaytrnsid);
            /*this.sendToGLInterface(prsn_id, loc_id_no, itm_id, itm_name,
              itm_uom, pay_amount, trns_date, pay_trns_desc, crncy_id, msg_id, log_tbl,
              dateStr, "Mass Pay Run Reversal", glDate);*/
            if (res)
            {
                cmnCde.updateLogMsg(msg_id,
            "\r\nSuccessfully processed Payment Reversal for Person:" +
            loc_id_no + " Item: " + itm_name, log_tbl, dateStr);
            }
            else
            {
                cmnCde.updateLogMsg(msg_id,
            "\r\nProcessing Payment Reversal Failed for Person:" +
            loc_id_no + " Item: " + itm_name, log_tbl, dateStr);
            }
            return "";
        }

        public void createPaymntLine(long prsnid, long itmid, double amnt, string paydate,
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
             "', '" + trnsType.Replace("'", "''") + "', " + cmnCde.User_id + ", '" + dateStr + "', " +
                     cmnCde.User_id + ", '" + dateStr + "', " + msspyid +
                     ", '" + paydesc.Replace("'", "''") + "', " + crncyid +
                     ", '" + pymt_vldty.Replace("'", "''") + "', " + src_trns_id +
                     ", '" + glDate + "', '" + dteErnd + "')";
            cmnCde.insertDataNoParams(insSQL);
        }

        public long hsPrsnBnPaidItmMsPy(long prsnID, long itmID,
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
            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public DataSet getPymntGLInfcLns(long pyTrnsID)
        {
            string strSql = "SELECT * FROM pay.pay_gl_interface WHERE source_trns_id = " +
              pyTrnsID + "  and gl_batch_id != -1";
            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            return dtst;
        }

        private bool rvrsImprtdPymntIntrfcTrns(long orgnlPyTrnsID, long nwPyTrnsID)
        {
            //try
            //{
            DataSet dtst = this.getPymntGLInfcLns(orgnlPyTrnsID);
            string dateStr = cmnCde.getFrmtdDB_Date_time();
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                int accntID = -1;
                double dbtamount = 0;
                double crdtamount = 0;
                int crncy_id = -1;
                double netamnt = 0;

                int.TryParse(dtst.Tables[0].Rows[i][1].ToString(), out accntID);
                double.TryParse(dtst.Tables[0].Rows[i][3].ToString(), out dbtamount);
                double.TryParse(dtst.Tables[0].Rows[i][8].ToString(), out crdtamount);
                int.TryParse(dtst.Tables[0].Rows[i][5].ToString(), out crncy_id);
                double.TryParse(dtst.Tables[0].Rows[i][11].ToString(), out netamnt);
                //long.TryParse(dtst.Tables[0].Rows[i][12].ToString(), out srcDocLnID);

                string trnsdte = DateTime.ParseExact(
            dtst.Tables[0].Rows[i][4].ToString(), "yyyy-MM-dd HH:mm:ss",
            System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");

                this.createPymntGLIntFcLn(accntID,
            "(Reversal)" + dtst.Tables[0].Rows[i][2].ToString(),
            -1 * dbtamount, trnsdte,
            crncy_id, -1 * crdtamount,
            -1 * netamnt, nwPyTrnsID, dateStr);

            }
            return true;
            //}
            //catch (Exception ex)
            //{
            //  cmnCde.showMsg(ex.InnerException.ToString(), 0);
            //  return false;
            //}
        }

        public void createPymntGLIntFcLn(int accntid, string trnsdesc, double dbtamnt,
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
                     ", '" + trnsdte.Replace("'", "''") + "', " + crncyid + ", " + cmnCde.User_id +
                     ", '" + dateStr + "', " + crdtamnt + ", " +
                     cmnCde.User_id + ", '" + dateStr + "', " + netamnt +
                     ", " + srcid + ")";
            cmnCde.insertDataNoParams(insSQL);
        }

        public DataSet getAllItmFeeds1(long itmid)
        {
            string selSQL = "SELECT a.balance_item_id, a.adds_subtracts, b.balance_type, a.scale_factor, c.pssbl_value_id " +
            "FROM org.org_pay_itm_feeds a LEFT OUTER JOIN org.org_pay_items b " +
            "ON a.balance_item_id = b.item_id LEFT OUTER JOIN org.org_pay_items_values c " +
            "ON c.item_id = a.balance_item_id WHERE ((a.fed_by_itm_id = " + itmid +
            ")) ORDER BY a.feed_id ";
            DataSet dtst = cmnCde.selectDataNoParams(selSQL);
            return dtst;
        }

        public void updtBlsItms(long prsn_id, long itm_id,
          double pay_amount, string trns_date, string trns_src, long orgnlTrnsID)
        {
            DataSet dtst = this.getAllItmFeeds1(itm_id);
            double nwAmnt = 0;
            for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
            {
                double lstBals = 0;
                double scaleFctr = 1;
                double.TryParse(dtst.Tables[0].Rows[a][3].ToString(), out scaleFctr);
                if (dtst.Tables[0].Rows[a][2].ToString() == "Cumulative")
                {
                    lstBals = this.getBlsItmLtstDailyBals(
                      long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
                    prsn_id, trns_date);
                    if (dtst.Tables[0].Rows[a][1].ToString() == "Subtracts")
                    {
                        nwAmnt = -1 * pay_amount * scaleFctr;
                    }
                    else
                    {
                        nwAmnt = pay_amount * scaleFctr;
                    }
                }
                else
                {
                    lstBals = this.getBlsItmDailyBals(long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
               prsn_id, trns_date);
                    if (dtst.Tables[0].Rows[a][1].ToString() == "Subtracts")
                    {
                        nwAmnt = -1 * pay_amount * scaleFctr;
                    }
                    else
                    {
                        nwAmnt = pay_amount * scaleFctr;
                    }
                }
                //Check if prsn's balance has not been updated already
                long paytrnsid = this.getPaymntTrnsID(
                prsn_id, itm_id,
                pay_amount, trns_date, orgnlTrnsID);

                bool hsBlsBnUpdtd = this.hsPrsItmBlsBnUptd(paytrnsid,
                  trns_date, long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
                  prsn_id);
                long dailybalID = this.getItmDailyBalsID(
                  long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
                  trns_date, prsn_id);

                if (hsBlsBnUpdtd == false)
                {
                    if (dailybalID <= 0)
                    {
                        this.createItmBals(long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
                          lstBals, prsn_id, trns_date, -1);

                        if (dtst.Tables[0].Rows[a][2].ToString() == "Cumulative")
                        {
                            this.updtItmDailyBalsCum(trns_date,
                            long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
                           prsn_id,
                           nwAmnt, paytrnsid);
                        }
                        else
                        {
                            this.updtItmDailyBalsNonCum(trns_date,
                            long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
                           prsn_id,
                           nwAmnt, paytrnsid);
                        }

                    }
                    else
                    {
                        if (dtst.Tables[0].Rows[a][2].ToString() == "Cumulative")
                        {
                            this.updtItmDailyBalsCum(trns_date,
                            long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
                           prsn_id,
                           nwAmnt, paytrnsid);
                        }
                        else
                        {
                            this.updtItmDailyBalsNonCum(trns_date,
                            long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
                           prsn_id,
                           nwAmnt, paytrnsid);
                        }
                    }
                }
            }
        }

        public long getItmDailyBalsID(long balsItmID, string balsDate, long prsn_id)
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

            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public double getBlsItmDailyBals(long balsItmID, long prsn_id, string balsDate)
        {
            string orgnlDte = balsDate;
            balsDate = DateTime.ParseExact(
         balsDate, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            balsDate = balsDate.Substring(0, 10);
            double res = 0;
            string strSql = "";
            string usesSQL = cmnCde.getGnrlRecNm("org.org_pay_items",
              "item_id", "uses_sql_formulas", balsItmID);
            if (usesSQL != "1")
            {
                strSql = "SELECT a.bals_amount " +
              "FROM pay.pay_balsitm_bals a " +
              "WHERE(to_timestamp(a.bals_date,'YYYY-MM-DD') =  to_timestamp('" + balsDate +
              "','YYYY-MM-DD') and a.bals_itm_id = " + balsItmID + " and a.person_id = " + prsn_id + ")";

                DataSet dtst = cmnCde.selectDataNoParams(strSql);
                if (dtst.Tables[0].Rows.Count > 0)
                {
                    double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out res);
                }
            }
            else
            {
                string valSQL = cmnCde.getItmValSQL(this.getPrsnItmVlID(prsn_id, balsItmID, orgnlDte));
                if (valSQL == "")
                {
                }
                else
                {
                    try
                    {
                        res = cmnCde.exctItmValSQL(
                          valSQL, prsn_id,
                          cmnCde.Org_id, balsDate);
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            return res;
        }

        public long getPrsnItmVlID(long prsnID, long itmID, string trnsdte)
        {
            trnsdte = DateTime.ParseExact(trnsdte, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

            //string dateStr = cmnCde.getDB_Date_time();
            string strSql = "Select a.item_pssbl_value_id FROM pasn.prsn_bnfts_cntrbtns a where((a.person_id = " +
          prsnID + ") and (a.item_id = " + itmID + ") and (to_timestamp('" + trnsdte + "'," +
          "'YYYY-MM-DD HH24:MI:SS') between to_timestamp(valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
                  "AND to_timestamp(valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS')))";
            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -100000;
        }

        public double getBlsItmLtstDailyBals(long balsItmID, long prsn_id, string balsDate)
        {
            string orgnlDte = balsDate;
            balsDate = DateTime.ParseExact(
         balsDate, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            balsDate = balsDate.Substring(0, 10);

            double res = 0;
            string strSql = "";
            string usesSQL = cmnCde.getGnrlRecNm("org.org_pay_items",
         "item_id", "uses_sql_formulas", balsItmID);
            if (usesSQL != "1")
            {
                strSql = "SELECT a.bals_amount " +
                   "FROM pay.pay_balsitm_bals a " +
                   "WHERE(to_timestamp(a.bals_date,'YYYY-MM-DD') <=  to_timestamp('" + balsDate +
                   "','YYYY-MM-DD') and a.bals_itm_id = " + balsItmID + " and a.person_id = " + prsn_id +
                   ") ORDER BY to_timestamp(a.bals_date,'YYYY-MM-DD') DESC LIMIT 1 OFFSET 0";

                DataSet dtst = cmnCde.selectDataNoParams(strSql);
                if (dtst.Tables[0].Rows.Count > 0)
                {
                    double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out res);
                }
            }
            else
            {
                string valSQL = cmnCde.getItmValSQL(this.getPrsnItmVlID(prsn_id, balsItmID, orgnlDte));
                if (valSQL == "")
                {
                }
                else
                {
                    try
                    {
                        res = cmnCde.exctItmValSQL(
                          valSQL, prsn_id,
                          cmnCde.Org_id, balsDate);
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            return res;
        }

        public bool hsPrsItmBlsBnUptd(long pytrnsid,
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
            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public void updtItmDailyBalsCum(string balsDate, long blsItmID,
      long prsn_id, double netAmnt, long py_trns_id)
        {
            balsDate = DateTime.ParseExact(
         balsDate, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            balsDate = balsDate.Substring(0, 10);
            cmnCde.Extra_Adt_Trl_Info = "";
            string dateStr = cmnCde.getDB_Date_time();
            string updtSQL = "UPDATE pay.pay_balsitm_bals " +
            "SET last_update_by = " + cmnCde.User_id +
            ", last_update_date = '" + dateStr +
            "', bals_amount = bals_amount +" + netAmnt +
            ", source_trns_ids = source_trns_ids || '" + py_trns_id +
          ",' WHERE (to_timestamp(bals_date,'YYYY-MM-DD') >= to_timestamp('" + balsDate +
          "','YYYY-MM-DD') and bals_itm_id = " + blsItmID + " and person_id = " + prsn_id + ")";
            cmnCde.updateDataNoParams(updtSQL);
        }

        public void updtItmDailyBalsNonCum(string balsDate, long blsItmID,
      long prsn_id, double netAmnt, long py_trns_id)
        {
            balsDate = DateTime.ParseExact(
         balsDate, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            balsDate = balsDate.Substring(0, 10);
            cmnCde.Extra_Adt_Trl_Info = "";
            string dateStr = cmnCde.getDB_Date_time();
            string updtSQL = "UPDATE pay.pay_balsitm_bals " +
            "SET last_update_by = " + cmnCde.User_id +
            ", last_update_date = '" + dateStr +
            "', bals_amount = bals_amount +" + netAmnt +
            ", source_trns_ids = source_trns_ids || '" + py_trns_id +
            ",' WHERE (to_timestamp(bals_date,'YYYY-MM-DD') = to_timestamp('" + balsDate +
            "','YYYY-MM-DD') and bals_itm_id = " + blsItmID + " and person_id = " + prsn_id + ")";
            cmnCde.updateDataNoParams(updtSQL);
        }

        public void createItmBals(long blsitmid, double netbals,
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
            string dateStr = cmnCde.getDB_Date_time();
            string insSQL = "INSERT INTO pay.pay_balsitm_bals(" +
                  "bals_itm_id, bals_amount, person_id, bals_date, created_by, " +
                  "creation_date, last_update_by, last_update_date, source_trns_ids) " +
              "VALUES (" + blsitmid +
              ", " + netbals + ", " + prsn_id + ", '" + balsDate + "', " +
              cmnCde.User_id + ", '" + dateStr +
                              "', " + cmnCde.User_id + ", '" + dateStr + "', '" + src_trns.Replace("'", "''") + "')";
            cmnCde.insertDataNoParams(insSQL);
        }

        public long doesPrsnHvItmPrs(long prsnid, long itmid)
        {
            string selSQL = "SELECT row_id " +
                        "FROM pasn.prsn_bnfts_cntrbtns WHERE ((person_id = " + prsnid +
                        ") and (item_id = " + itmid + "))";
            DataSet dtst = cmnCde.selectDataNoParams(selSQL);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public bool doesPrsnHvItm(long prsnID, long itmID, string dateStr, ref string strtDte)
        {
            dateStr = DateTime.ParseExact(
         dateStr, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            string strSql = @"Select a.row_id, to_char(to_timestamp(valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS')
      FROM pasn.prsn_bnfts_cntrbtns a where((a.person_id = " +
          prsnID + ") and (a.item_id = " + itmID + ") and (to_timestamp('" + dateStr + "'," +
          "'YYYY-MM-DD HH24:MI:SS') between to_timestamp(valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
                  "AND to_timestamp(valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS')))";
            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                strtDte = dtst.Tables[0].Rows[0][1].ToString();
                return true;
            }
            strtDte = "";
            return false;
        }

        public void createBnftsPrs(long prsnid, long itmid, long itm_val_id,
      string strtdte, string enddte)
        {
            string dateStr = cmnCde.getDB_Date_time();
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
             "', " + cmnCde.User_id + ", '" + dateStr + "', " +
                     cmnCde.User_id + ", '" + dateStr + "')";
            cmnCde.insertDataNoParams(insSQL);
        }

        public bool doesPrsnHvItm(long prsnID, long itmID)
        {
            string strSql = @"Select a.row_id 
      FROM pasn.prsn_bnfts_cntrbtns a where((a.person_id = " +
          prsnID + ") and (a.item_id = " + itmID + "))";
            DataSet dtst = cmnCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                //strtDte = dtst.Tables[0].Rows[0][1].ToString();
                return true;
            }
            //strtDte = "";
            return false;
        }

        public double willItmBlsBeNgtv(long prsn_id, long itm_id,
          double pay_amount, string trns_date)
        {
            DataSet dtst = this.getAllItmFeeds1(itm_id);
            double nwAmnt = 0;
            for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
            {
                if (this.doesPrsnHvItmPrs(prsn_id,
                  long.Parse(dtst.Tables[0].Rows[a][0].ToString())) <= 0)
                {
                    string tstDte = "";
                    this.doesPrsnHvItm(prsn_id, itm_id, trns_date, ref tstDte);
                    if (tstDte == "")
                    {
                        tstDte = "01-Jan-1900 00:00:00";
                    }
                    this.createBnftsPrs(prsn_id,
                      long.Parse(dtst.Tables[0].Rows[a][0].ToString())
                        , long.Parse(dtst.Tables[0].Rows[a][4].ToString())
                        , "01-" + tstDte.Substring(3, 8), "31-Dec-4000");
                    //this.createBnftsPrs(prsn_id,
                    //  long.Parse(dtst.Tables[0].Rows[a][0].ToString())
                    //    , long.Parse(dtst.Tables[0].Rows[a][0].ToString())
                    //    , "01-" + trns_date.Substring(3, 8), "31-Dec-4000");
                }
                double scaleFctr = 1;
                double.TryParse(dtst.Tables[0].Rows[a][3].ToString(), out scaleFctr);
                if (dtst.Tables[0].Rows[a][2].ToString() == "Cumulative")
                {
                    if (dtst.Tables[0].Rows[a][1].ToString() == "Subtracts")
                    {
                        nwAmnt = this.getBlsItmLtstDailyBals(
                          long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
                          prsn_id, trns_date) - (pay_amount * scaleFctr);
                    }
                    else
                    {
                        nwAmnt = (pay_amount * scaleFctr)
                  + this.getBlsItmLtstDailyBals(long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
                  prsn_id, trns_date);
                    }
                }
                else
                {
                    if (dtst.Tables[0].Rows[a][1].ToString() == "Subtracts")
                    {
                        nwAmnt = this.getBlsItmDailyBals(long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
                          prsn_id, trns_date) - (pay_amount * scaleFctr);
                    }
                    else
                    {
                        nwAmnt = (pay_amount * scaleFctr)
                  + this.getBlsItmDailyBals(long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
                  prsn_id, trns_date);
                    }
                }
                if (nwAmnt < 0)
                {
                    return nwAmnt;
                }
            }
            return nwAmnt;
        }

        #endregion

        private void rfrshButton_Click(object sender, EventArgs e)
        {
            //this.processPayButton.Enabled = true;
            this.populateTrnsGridVw();
        }

        private void vwSQLTrnsButton_Click(object sender, EventArgs e)
        {
            cmnCde.showSQL(this.pymntsGvn_SQL, 10);
        }

        private void rcHstrySmryButton_Click(object sender, EventArgs e)
        {
            if (this.trnsSearchListView.SelectedItems.Count <= 0)
            {
                cmnCde.showMsg("Please select a Record First!", 0);
                return;
            }
            cmnCde.showRecHstry(
              cmnCde.get_Gnrl_Rec_Hstry(long.Parse(
              this.trnsSearchListView.SelectedItems[0].SubItems[10].Text),
              "accb.accb_payments", "pymnt_id"), 9);
        }

        private void printPrvwRcptButton_Click(object sender, EventArgs e)
        {
            //DataSet dtst = this.get_LastScmPay_Trns(
            //long.Parse(this.docIDTextBox.Text), this.docTypeComboBox.Text, cmnCde.Org_id);
            if (this.trnsSearchListView.SelectedItems.Count <= 0)
            {
                cmnCde.showMsg("Please select a Record First!", 0);
                return;
            }
            long rcvblHdrID = long.Parse(this.trnsSearchListView.SelectedItems[0].SubItems[9].Text);
            string rcvblDoctype = "";
            DataSet dtst;
            if (this.docTypes == "Customer Payments")
            {
                rcvblDoctype = cmnCde.getGnrlRecNm("accb.accb_rcvbls_invc_hdr",
                  "rcvbls_invc_hdr_id", "rcvbls_invc_type", rcvblHdrID);
                dtst = this.get_LastRcvblPay_Trns(long.Parse(this.trnsSearchListView.SelectedItems[0].SubItems[10].Text));
            }
            else
            {
                rcvblDoctype = cmnCde.getGnrlRecNm("accb.accb_pybls_invc_hdr",
             "pybls_invc_hdr_id", "pybls_invc_type", rcvblHdrID);
                dtst = this.get_LastPyblPay_Trns(long.Parse(this.trnsSearchListView.SelectedItems[0].SubItems[10].Text));
            }

            if (dtst.Tables[0].Rows.Count <= 0)
            {
                cmnCde.showMsg("Cannot Print a Receipt when no Payment has been made!", 0);
                return;
            }
            this.pageNo = 1;
            this.prntIdx = 0;
            this.prntIdx1 = 0;
            this.prntIdx2 = 0;
            this.ght = 0;
            this.prcWdth = 0;
            this.qntyWdth = 0;
            this.itmWdth = 0;
            this.qntyStartX = 0;
            this.prcStartX = 0;
            this.amntStartX = 0;
            this.amntWdth = 0;
            this.printPreviewDialog1 = new PrintPreviewDialog();

            this.printPreviewDialog1.Document = printDocument1;
            this.printPreviewDialog1.FormBorderStyle = FormBorderStyle.Fixed3D;
            //this.printPreviewDialog1.SetBounds(400, 400, 300, 600);
            this.printPreviewDialog1.PrintPreviewControl.Zoom = 1;

            //this.printPreviewDialog1.PrintPreviewControl.AutoZoom = true;
            this.printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Pos", 283, 1100);
            ((ToolStripButton)((ToolStrip)this.printPreviewDialog1.Controls[1]).Items[0]).Enabled = false;
            ((ToolStripButton)((ToolStrip)this.printPreviewDialog1.Controls[1]).Items[0]).Visible = false;
            //((ToolStripButton)((ToolStrip)this.printPreviewDialog1.Controls[1]).Items[0]).Click += new EventHandler(this.printRcptButton_Click);
            //this.printPreviewDialog1.MainMenuStrip = menuStrip1;
            //this.printPreviewDialog1.MainMenuStrip.Visible = true;
            this.printRcptButton1.Visible = true;
            ((ToolStrip)this.printPreviewDialog1.Controls[1]).Items.Add(this.printRcptButton1);
            this.printPreviewDialog1.FindForm().ShowIcon = false;
            this.printPreviewDialog1.FindForm().Height = this.Height;
            this.printPreviewDialog1.FindForm().StartPosition = FormStartPosition.CenterParent;
            //this.printPreviewDialog1.FindForm().Location = new Point(this.groupBox3.Location.X - 85, 20);
            this.printPreviewDialog1.ShowDialog();
        }
        int pageNo = 1;
        int prntIdx = 0;
        int prntIdx1 = 0;
        int prntIdx2 = 0;
        float ght = 0;
        int prcWdth = 0;
        int qntyWdth = 0;
        int itmWdth = 0;
        int qntyStartX = 0;
        int prcStartX = 0;
        int amntWdth = 0;
        int amntStartX = 0;

        private void printRcptButton_Click(object sender, EventArgs e)
        {
            if (this.trnsSearchListView.SelectedItems.Count <= 0)
            {
                cmnCde.showMsg("Please select a Record First!", 0);
                return;
            }
            long rcvblHdrID = long.Parse(this.trnsSearchListView.SelectedItems[0].SubItems[9].Text);
            string rcvblDoctype = "";
            DataSet dtst;
            if (this.docTypes == "Customer Payments")
            {
                rcvblDoctype = cmnCde.getGnrlRecNm("accb.accb_rcvbls_invc_hdr",
                  "rcvbls_invc_hdr_id", "rcvbls_invc_type", rcvblHdrID);
                dtst = this.get_LastRcvblPay_Trns(long.Parse(this.trnsSearchListView.SelectedItems[0].SubItems[10].Text));
            }
            else
            {
                rcvblDoctype = cmnCde.getGnrlRecNm("accb.accb_pybls_invc_hdr",
             "pybls_invc_hdr_id", "pybls_invc_type", rcvblHdrID);
                dtst = this.get_LastPyblPay_Trns(long.Parse(this.trnsSearchListView.SelectedItems[0].SubItems[10].Text));
            }

            if (dtst.Tables[0].Rows.Count <= 0)
            {
                cmnCde.showMsg("Cannot Print a Receipt when no Payment has been made!", 0);
                return;
            }
            this.pageNo = 1;
            this.prntIdx = 0;
            this.prntIdx1 = 0;
            this.prntIdx2 = 0;
            this.ght = 0;
            this.prcWdth = 0;
            this.qntyWdth = 0;
            this.itmWdth = 0;
            this.qntyStartX = 0;
            this.prcStartX = 0;
            this.amntStartX = 0;
            this.amntWdth = 0;

            this.printDialog1 = new PrintDialog();
            this.printDialog1.UseEXDialog = true;
            this.printDialog1.ShowNetwork = true;
            this.printDialog1.AllowCurrentPage = false;
            this.printDialog1.AllowPrintToFile = false;
            this.printDialog1.AllowSelection = false;
            this.printDialog1.AllowSomePages = false;
            this.printDialog1.PrinterSettings.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Pos", 283, 1100);
            this.printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Pos", 283, 1100);
            this.printDialog1.PrinterSettings.DefaultPageSettings.PaperSize.PaperName = "Pos";
            this.printDialog1.PrinterSettings.DefaultPageSettings.PaperSize.Height = 1100;
            this.printDialog1.PrinterSettings.DefaultPageSettings.PaperSize.Width = 283;

            printDialog1.Document = this.printDocument1;
            DialogResult res = printDialog1.ShowDialog(this);
            if (res == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            if (this.trnsSearchListView.SelectedItems.Count <= 0)
            {
                cmnCde.showMsg("Please select a Record First!", 0);
                return;
            }
            long rcvblHdrID = long.Parse(this.trnsSearchListView.SelectedItems[0].SubItems[9].Text);
            string rcvblDoctype = "";
            DataSet dtst;
            if (this.docTypes == "Customer Payments")
            {
                rcvblDoctype = cmnCde.getGnrlRecNm("accb.accb_rcvbls_invc_hdr",
                  "rcvbls_invc_hdr_id", "rcvbls_invc_type", rcvblHdrID);
                dtst = this.get_LastRcvblPay_Trns(long.Parse(this.trnsSearchListView.SelectedItems[0].SubItems[10].Text));
            }
            else
            {
                rcvblDoctype = cmnCde.getGnrlRecNm("accb.accb_pybls_invc_hdr",
             "pybls_invc_hdr_id", "pybls_invc_type", rcvblHdrID);
                dtst = this.get_LastPyblPay_Trns(long.Parse(this.trnsSearchListView.SelectedItems[0].SubItems[10].Text));
            }


            if (dtst.Tables[0].Rows.Count <= 0)
            {
                cmnCde.showMsg("Cannot Print a Receipt/Advice when no Payment has been made!", 0);
                return;
            }
            Pen aPen = new Pen(Brushes.Black, 1);
            Graphics g = e.Graphics;
            e.PageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Pos", 283, 1100);
            Font font1 = new Font("Tahoma", 8.25f, FontStyle.Bold);
            Font font2 = new Font("Tahoma", 8.25f, FontStyle.Bold);
            Font font4 = new Font("Tahoma", 8.25f, FontStyle.Bold);
            Font font3 = new Font("Lucida Console", 8.25f, FontStyle.Regular);
            Font font5 = new Font("Times New Roman", 6.0f, FontStyle.Italic);

            int font1Hght = font1.Height;
            int font2Hght = font2.Height;
            int font3Hght = font3.Height;
            int font4Hght = font4.Height;
            int font5Hght = font5.Height;

            float pageWidth = e.PageSettings.PaperSize.Width - 40;//e.PageSettings.PrintableArea.Width;
            float pageHeight = e.PageSettings.PaperSize.Height - 40;// e.PageSettings.PrintableArea.Height;
                                                                    //cmnCde.showMsg(pageWidth.ToString(), 0);
            int startX = 10;
            int startY = 20;
            int offsetY = 0;
            //StringBuilder strPrnt = new StringBuilder();
            //strPrnt.AppendLine("Received From");
            string[] nwLn;
            //DataSet dtst = this.get_LastScmPay_Trns(
            //  long.Parse(this.docIDTextBox.Text), this.docTypeComboBox.Text, cmnCde.Org_id);

            string rcptNo = "";

            if (this.pageNo == 1)
            {
                //Org Name
                nwLn = cmnCde.breakTxtDown(
                  cmnCde.getOrgName(cmnCde.Org_id),
                  pageWidth + 85, font2, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    g.DrawString(nwLn[i]
                    , font2, Brushes.Black, startX, startY + offsetY);
                    offsetY += font2Hght;
                }

                //Pstal Address
                g.DrawString(cmnCde.getOrgPstlAddrs(cmnCde.Org_id).Trim(),
                font2, Brushes.Black, startX, startY + offsetY);
                //offsetY += font2Hght;

                ght = g.MeasureString(
                 cmnCde.getOrgPstlAddrs(cmnCde.Org_id).Trim(), font2).Height;
                offsetY = offsetY + (int)ght;
                //Contacts Nos
                nwLn = cmnCde.breakTxtDown(
            cmnCde.getOrgContactNos(cmnCde.Org_id),
            pageWidth, font2, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    g.DrawString(nwLn[i]
                    , font2, Brushes.Black, startX, startY + offsetY);
                    offsetY += font2Hght;
                }
                //Email Address
                nwLn = cmnCde.breakTxtDown(
            cmnCde.getOrgEmailAddrs(cmnCde.Org_id),
            pageWidth, font2, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    g.DrawString(nwLn[i]
                    , font2, Brushes.Black, startX, startY + offsetY);
                    offsetY += font2Hght;
                }

                offsetY += 3;
                g.DrawLine(aPen, startX, startY + offsetY, startX + pageWidth + 25,
                  startY + offsetY);
                if (this.docTypes == "Customer Payments")
                {
                    g.DrawString("Payment Receipt", font2, Brushes.Black, startX, startY + offsetY);
                }
                else
                {
                    g.DrawString("Payment Advice", font2, Brushes.Black, startX, startY + offsetY);
                }
                offsetY += font2Hght;
                g.DrawLine(aPen, startX, startY + offsetY, startX + pageWidth + 25,
                startY + offsetY);
                offsetY += 3;
                g.DrawString("Doc. No: ", font4, Brushes.Black, startX, startY + offsetY);
                ght = g.MeasureString("Doc. No: ", font4).Width;
                //Receipt No: 
                g.DrawString(this.trnsSearchListView.SelectedItems[0].SubItems[2].Text,
            font3, Brushes.Black, startX + ght, startY + offsetY + 2);
                offsetY += font4Hght;


                if (this.docTypes == "Customer Payments")
                {
                    g.DrawString("Payment Receipt No: ", font4, Brushes.Black, startX, startY + offsetY);
                    //offsetY += font4Hght;
                    ght = g.MeasureString("Payment Receipt No: ", font4).Width;
                }
                else
                {
                    g.DrawString("Payment Advice No: ", font4, Brushes.Black, startX, startY + offsetY);
                    //offsetY += font4Hght;
                    ght = g.MeasureString("Payment Advice No: ", font4).Width;
                }
                //Get Last Payment
                if (dtst.Tables[0].Rows.Count > 0)
                {
                    rcptNo = dtst.Tables[0].Rows[0][0].ToString();
                }
                if (rcptNo.Length < 4)
                {
                    rcptNo = rcptNo.PadLeft(4, '0');
                }
                nwLn = cmnCde.breakTxtDown(
            rcptNo,
            startX + ght, font3, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    g.DrawString(nwLn[i]
                    , font3, Brushes.Black, startX + ght, startY + offsetY + 2);
                    offsetY += font3Hght;
                }
                offsetY += 2;

                string curcy = cmnCde.getPssblValNm(int.Parse(dtst.Tables[0].Rows[0][11].ToString()));
                if (this.docTypes == "Customer Payments")
                {
                    g.DrawString("Date Received: ", font4, Brushes.Black, startX, startY + offsetY);
                    ght = g.MeasureString("Date Received: ", font4).Width;
                }
                else
                {
                    g.DrawString("Date Paid: ", font4, Brushes.Black, startX, startY + offsetY);
                    ght = g.MeasureString("Date Paid: ", font4).Width;
                }
                //Receipt No: 
                g.DrawString(dtst.Tables[0].Rows[0][8].ToString().ToUpper(),
            font3, Brushes.Black, startX + ght, startY + offsetY + 3);
                offsetY += font4Hght;
                g.DrawString("Currency: ", font4, Brushes.Black, startX, startY + offsetY);
                ght = g.MeasureString("Currency: ", font4).Width;
                //Receipt No: 
                g.DrawString(curcy,
            font3, Brushes.Black, startX + ght, startY + offsetY + 3);
                offsetY += font4Hght;
                g.DrawString("Cashier: ", font4, Brushes.Black, startX, startY + offsetY);
                ght = g.MeasureString("Cashier: ", font4).Width;
                //Receipt No: 
                g.DrawString(dtst.Tables[0].Rows[0][10].ToString().ToUpper(),
            font3, Brushes.Black, startX + ght, startY + offsetY + 3);
                //offsetY += font4Hght;
                offsetY += font4Hght;
                string lbelTxt = "Customer:";
                if (this.docTypes.Contains("Supplier"))
                {
                    lbelTxt = "Supplier:";
                }
                g.DrawString(lbelTxt + " ", font4, Brushes.Black, startX, startY + offsetY);
                //offsetY += font4Hght;
                ght = g.MeasureString(lbelTxt + " ", font4).Width;
                //Get Last Payment
                nwLn = cmnCde.breakTxtDown(
                   cmnCde.getGnrlRecNm(
                  "scm.scm_cstmr_suplr", "cust_sup_id",
                  "cust_sup_name", this.spplrID),
            (float)(2.4 * ght), font3, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    g.DrawString(nwLn[i]
                    , font3, Brushes.Black, startX + ght, startY + offsetY);
                    if (i < nwLn.Length - 1)
                    {
                        offsetY += font4Hght;
                    }
                }
                offsetY += font4Hght;
                g.DrawString("Description: ", font4, Brushes.Black, startX, startY + offsetY);
                ght = g.MeasureString("Description: ", font4).Width;
                //Receipt No: 
                nwLn = cmnCde.breakTxtDown(
                    dtst.Tables[0].Rows[0][4].ToString(),
             (float)(2.4 * ght), font3, g);

                for (int i = 0; i < nwLn.Length; i++)
                {
                    g.DrawString(nwLn[i]
                    , font3, Brushes.Black, startX + ght, startY + offsetY + 3);
                    offsetY += font3Hght;
                    //ght += g.MeasureString(nwLn[i], font3).Width;
                }
                offsetY += 3;
                //offsetY += font3Hght;

                g.DrawLine(aPen, startX, startY + offsetY, startX + pageWidth + 25,
            startY + offsetY);
                offsetY += 2;
                g.DrawString("Item Description", font1, Brushes.Black, startX, startY + offsetY);
                /*//offsetY += font4Hght;*/
                ght = g.MeasureString("Item Description", font1).Width;
                itmWdth = (int)ght;
                qntyStartX = startX + (int)ght;
                /* g.DrawString("Quantity".PadLeft(15, ' '), font1, Brushes.Black, qntyStartX, startY + offsetY);
                 //offsetY += font4Hght;*/
                ght += g.MeasureString("Quantity".PadLeft(15, ' '), font1).Width;
                qntyWdth = (int)g.MeasureString("Quantity".PadLeft(15, ' '), font1).Width; ;
                prcStartX = startX + (int)ght;
                /**/
                g.DrawString("Amount".PadLeft(15, ' '), font1, Brushes.Black, prcStartX, startY + offsetY);
                ght = g.MeasureString("Amount".PadLeft(15, ' '), font1).Width;
                prcWdth = (int)ght;
                /*offsetY += font1Hght;
                g.DrawLine(aPen, startX, startY + offsetY, startX + pageWidth + 25,
             startY + offsetY);*/
                offsetY += 3;
            }
            /*DataSet lndtst = this.get_One_SalesDcLines(long.Parse(this.docIDTextBox.Text));
            //Line Items
            int orgOffstY = 0;
            int hgstOffst = offsetY;
            for (int a = this.prntIdx; a < lndtst.Tables[0].Rows.Count; a++)
            {
              orgOffstY = hgstOffst;
              offsetY = orgOffstY;
              ght = 0;
              nwLn = cmnCde.breakTxtDown(
          cmnCde.getGnrlRecNm("inv.inv_itm_list",
          "item_id", "item_desc",
          long.Parse(lndtst.Tables[0].Rows[a][1].ToString())).Trim() + "@"
          + double.Parse(lndtst.Tables[0].Rows[a][3].ToString()).ToString("#,##0.00"),
          itmWdth, font3, g);

              for (int i = 0; i < nwLn.Length; i++)
              {
                //breakPOSTxtDown
                if (g.MeasureString(nwLn[i], font3).Width > itmWdth)
                {
                  string[] nwnwLn;
                  nwnwLn = cmnCde.breakPOSTxtDown(nwLn[i],
            itmWdth, font3, g, 14);
                  for (int j = 0; j < nwnwLn.Length; j++)
                  {
                    g.DrawString(nwnwLn[j]
             , font3, Brushes.Black, startX, startY + offsetY);
                    offsetY += font3Hght;
                    ght += g.MeasureString(nwnwLn[j], font3).Width;
                  }
                }
                else
                {
                  g.DrawString(nwLn[i]
                  , font3, Brushes.Black, startX, startY + offsetY);
                  offsetY += font3Hght;
                  ght += g.MeasureString(nwLn[i], font3).Width;
                }
              }
              if (offsetY > hgstOffst)
              {
                hgstOffst = offsetY;
              }
              offsetY = orgOffstY;

              nwLn = cmnCde.breakTxtDown(
                double.Parse(lndtst.Tables[0].Rows[a][2].ToString()).ToString("#,##0.00"),
          qntyWdth, font3, g);
              for (int i = 0; i < nwLn.Length; i++)
              {
                if (i == 0)
                {
                  ght = g.MeasureString(nwLn[i], font3).Width;
                }
                g.DrawString(nwLn[i].PadLeft(15, ' ')
                , font3, Brushes.Black, qntyStartX - 22, startY + offsetY);
                offsetY += font3Hght;
              }
              if (offsetY > hgstOffst)
              {
                hgstOffst = offsetY;
              }
              offsetY = orgOffstY;

              nwLn = cmnCde.breakTxtDown(
                double.Parse(lndtst.Tables[0].Rows[a][4].ToString()).ToString("#,##0.00"),
          prcWdth, font3, g);
              for (int i = 0; i < nwLn.Length; i++)
              {
                if (i == 0)
                {
                  ght = g.MeasureString(nwLn[i], font3).Width;
                }
                g.DrawString(nwLn[i].PadLeft(15, ' ')
                , font3, Brushes.Black, prcStartX - 22, startY + offsetY);
                offsetY += font3Hght;
              }
              if (offsetY > hgstOffst)
              {
                hgstOffst = offsetY;
              }
              this.prntIdx++;
              if (hgstOffst >= pageHeight - 30)
              {
                e.HasMorePages = true;
                offsetY = 0;
                this.pageNo++;
                return;
              }
              //else
              //{
              //  e.HasMorePages = false;
              //}

            }*/
            int orgOffstY = 0;
            int hgstOffst = offsetY;
            if (this.prntIdx1 == 0)
            {
                offsetY = hgstOffst + font3Hght;
                g.DrawLine(aPen, startX, startY + offsetY, startX + pageWidth + 25,
                     startY + offsetY);
                offsetY += 3;
            }
            DataSet smmryDtSt;
            if (this.docTypes == "Customer Payments")
            {
                smmryDtSt = this.get_DocSmryLns(rcvblHdrID,
                rcvblDoctype);
            }
            else
            {
                smmryDtSt = this.get_PyblsDocSmryLns(rcvblHdrID,
                rcvblDoctype);
            }
            orgOffstY = 0;
            hgstOffst = offsetY;

            for (int b = this.prntIdx1; b < smmryDtSt.Tables[0].Rows.Count; b++)
            {
                orgOffstY = hgstOffst;
                offsetY = orgOffstY;
                ght = 0;
                if (hgstOffst >= pageHeight - 30)
                {
                    e.HasMorePages = true;
                    offsetY = 0;
                    this.pageNo++;
                    return;
                }
                //.PadRight(30, ' ')
                nwLn = cmnCde.breakTxtDown(
                  smmryDtSt.Tables[0].Rows[b][1].ToString(),
            2 * qntyWdth, font3, g);

                for (int i = 0; i < nwLn.Length; i++)
                {
                    //.PadRight(30, ' ')
                    g.DrawString(nwLn[i]
                    , font3, Brushes.Black, startX, startY + offsetY);
                    offsetY += font3Hght;
                    ght += g.MeasureString(nwLn[i], font3).Width;
                }
                if (offsetY > hgstOffst)
                {
                    hgstOffst = offsetY;
                }
                offsetY = orgOffstY;

                nwLn = cmnCde.breakTxtDown(
                  double.Parse(smmryDtSt.Tables[0].Rows[b][2].ToString()).ToString("#,##0.00"),
            prcWdth, font3, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    if (i == 0)
                    {
                        ght = g.MeasureString(nwLn[i], font3).Width;
                    }
                    g.DrawString(nwLn[i].PadLeft(15, ' ')
                    , font3, Brushes.Black, prcStartX - 22, startY + offsetY);
                    offsetY += font3Hght;
                }
                if (offsetY > hgstOffst)
                {
                    hgstOffst = offsetY;
                }
                this.prntIdx1++;
            }
            if (this.prntIdx2 == 0)
            {
                offsetY = hgstOffst + 3;
                g.DrawLine(aPen, startX, startY + offsetY, startX + pageWidth + 25,
              startY + offsetY);
                offsetY += 3;
            }
            orgOffstY = 0;
            hgstOffst = offsetY;

            for (int c = this.prntIdx2; c < 4; c++)
            {
                orgOffstY = hgstOffst;
                offsetY = orgOffstY;
                ght = 0;
                if (hgstOffst >= pageHeight - 30)
                {
                    e.HasMorePages = true;
                    offsetY = 0;
                    this.pageNo++;
                    return;
                }
                if (c == 0)
                {
                    nwLn = cmnCde.breakTxtDown(
                      "Receipt Amount:".PadLeft(30, ' '),
               2 * qntyWdth, font3, g);

                    for (int i = 0; i < nwLn.Length; i++)
                    {
                        g.DrawString(nwLn[i].PadLeft(30, ' ')
                        , font3, Brushes.Black, qntyStartX - 122, startY + offsetY);
                        offsetY += font3Hght;
                        ght += g.MeasureString(nwLn[i], font3).Width;
                    }
                    if (offsetY > hgstOffst)
                    {
                        hgstOffst = offsetY;
                    }
                    offsetY = orgOffstY;

                    string amntRcvd = "0.00";
                    if (double.Parse(dtst.Tables[0].Rows[0][2].ToString()) > 0
                      && double.Parse(dtst.Tables[0].Rows[0][3].ToString()) <= 0)
                    {
                        amntRcvd = (Math.Abs(double.Parse(dtst.Tables[0].Rows[0][2].ToString())) -
                        double.Parse(dtst.Tables[0].Rows[0][3].ToString())).ToString("#,##0.00");
                    }
                    else if (double.Parse(dtst.Tables[0].Rows[0][2].ToString()) > 0
                      && double.Parse(dtst.Tables[0].Rows[0][3].ToString()) > 0)
                    {
                        amntRcvd = double.Parse(dtst.Tables[0].Rows[0][2].ToString()).ToString("#,##0.00");
                    }

                    nwLn = cmnCde.breakTxtDown(
                      double.Parse(amntRcvd).ToString("#,##0.00"),
               prcWdth, font3, g);
                    for (int i = 0; i < nwLn.Length; i++)
                    {
                        if (i == 0)
                        {
                            ght = g.MeasureString(nwLn[i], font3).Width;
                        }
                        g.DrawString(nwLn[i].PadLeft(15, ' ')
                        , font3, Brushes.Black, prcStartX - 22, startY + offsetY);
                        offsetY += font3Hght;
                    }
                    if (offsetY > hgstOffst)
                    {
                        hgstOffst = offsetY;
                    }
                    this.prntIdx2++;
                }
                else if (c == 1)
                {
                    nwLn = cmnCde.breakTxtDown(
                      "Description:".PadLeft(30, ' '),
               2 * qntyWdth, font3, g);

                    for (int i = 0; i < nwLn.Length; i++)
                    {
                        g.DrawString(nwLn[i].PadLeft(30, ' ')
                        , font3, Brushes.Black, qntyStartX - 122, startY + offsetY);
                        offsetY += font3Hght;
                        ght += g.MeasureString(nwLn[i], font3).Width;
                    }
                    if (offsetY > hgstOffst)
                    {
                        hgstOffst = offsetY;
                    }
                    offsetY = orgOffstY;
                    string payDesc = "-Part Payment";
                    if (double.Parse(dtst.Tables[0].Rows[0][3].ToString()) <= 0)
                    {
                        payDesc = "-Full Payment";
                    }
                    nwLn = cmnCde.breakTxtDown(
                      dtst.Tables[0].Rows[0][1].ToString() + payDesc,
               prcWdth, font3, g);
                    for (int i = 0; i < nwLn.Length; i++)
                    {
                        if (i == 0)
                        {
                            ght = g.MeasureString(nwLn[i], font3).Width;
                        }
                        g.DrawString(nwLn[i]//.PadRight(25, ' ')
                        , font3, Brushes.Black, prcStartX + 3, startY + offsetY);
                        offsetY += font3Hght;
                    }
                    if (offsetY > hgstOffst)
                    {
                        hgstOffst = offsetY;
                    }
                    this.prntIdx2++;
                }
                else if (c == 2)
                {
                    nwLn = cmnCde.breakTxtDown(
                      "Change/Balance:".PadLeft(30, ' '),
               2 * qntyWdth, font3, g);

                    for (int i = 0; i < nwLn.Length; i++)
                    {
                        g.DrawString(nwLn[i].PadLeft(30, ' ')
                        , font3, Brushes.Black, qntyStartX - 122, startY + offsetY);
                        offsetY += font3Hght;
                        ght += g.MeasureString(nwLn[i], font3).Width;
                    }
                    if (offsetY > hgstOffst)
                    {
                        hgstOffst = offsetY;
                    }
                    offsetY = orgOffstY;

                    nwLn = cmnCde.breakTxtDown(
                      double.Parse(dtst.Tables[0].Rows[0][3].ToString()).ToString("#,##0.00"),
               prcWdth, font3, g);
                    for (int i = 0; i < nwLn.Length; i++)
                    {
                        if (i == 0)
                        {
                            ght = g.MeasureString(nwLn[i], font3).Width;
                        }
                        g.DrawString(nwLn[i].PadLeft(15, ' ')
                        , font3, Brushes.Black, prcStartX - 22, startY + offsetY);
                        offsetY += font3Hght;
                    }
                    if (offsetY > hgstOffst)
                    {
                        hgstOffst = offsetY;
                    }
                    this.prntIdx2++;
                }
                //      else if (c == 3)
                //      {
                //        nwLn = cmnCde.breakTxtDown(
                //          "Cashier:".PadLeft(30, ' '),
                //2 * qntyWdth, font3, g);

                //        for (int i = 0; i < nwLn.Length; i++)
                //        {
                //          g.DrawString(nwLn[i].PadLeft(30, ' ')
                //          , font3, Brushes.Black, qntyStartX - 122, startY + offsetY);
                //          offsetY += font3Hght;
                //          ght += g.MeasureString(nwLn[i], font3).Width;
                //        }
                //        if (offsetY > hgstOffst)
                //        {
                //          hgstOffst = offsetY;
                //        }
                //        offsetY = orgOffstY;
                //        nwLn = cmnCde.breakTxtDown(
                //          dtst.Tables[0].Rows[0][10].ToString().ToUpper(),
                //  prcWdth, font3, g);
                //        for (int i = 0; i < nwLn.Length; i++)
                //        {
                //          if (i == 0)
                //          {
                //            ght = g.MeasureString(nwLn[i], font3).Width;
                //          }
                //          g.DrawString(nwLn[i]//.PadRight(25, ' ')
                //          , font3, Brushes.Black, prcStartX, startY + offsetY);
                //          offsetY += font3Hght;
                //        }
                //        if (offsetY > hgstOffst)
                //        {
                //          hgstOffst = offsetY;
                //        }
                //        this.prntIdx2++;
                //      }
            }

            //Slogan: 
            offsetY += 3;
            //offsetY += 3;
            if (hgstOffst >= pageHeight - 30)
            {
                e.HasMorePages = true;
                offsetY = 0;
                this.pageNo++;
                return;
            }
            g.DrawLine(aPen, startX, startY + offsetY, startX + pageWidth + 25,
         startY + offsetY);
            nwLn = cmnCde.breakTxtDown(
              cmnCde.getOrgSlogan(cmnCde.Org_id),
         pageWidth - ght, font5, g);
            for (int i = 0; i < nwLn.Length; i++)
            {
                g.DrawString(nwLn[i]
                , font5, Brushes.Black, startX, startY + offsetY);
                offsetY += font5Hght;
            }
            offsetY += font5Hght;
            nwLn = cmnCde.breakTxtDown(
             "Software Developed by Rhomicom Systems Technologies Ltd.",
         pageWidth + 40, font5, g);
            for (int i = 0; i < nwLn.Length; i++)
            {
                g.DrawString(nwLn[i]
                , font5, Brushes.Black, startX, startY + offsetY);
                offsetY += font5Hght;
            }
            nwLn = cmnCde.breakTxtDown(
         "Website:www.rhomicomgh.com Mobile: 0544709501/0266245395",
         pageWidth + 40, font5, g);
            for (int i = 0; i < nwLn.Length; i++)
            {
                g.DrawString(nwLn[i]
                , font5, Brushes.Black, startX, startY + offsetY);
                offsetY += font5Hght;
            }
        }

    }
}
