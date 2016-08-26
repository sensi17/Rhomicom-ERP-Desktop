using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CommonCode
{
  public partial class cstSpplrDiag : Form
  {
    #region "GLOBAL VARIABLES..."
    //Records;
    public long cstspplrID = -1;
    public long siteID = -1;
    public string docType = "";//Supplier/Customer

    long rec_cur_indx = 0;
    bool is_last_rec = false;
    long totl_rec = 0;
    long last_rec_num = 0;
    public string rec_SQL = "";
    public string recDt_SQL = "";
    bool obey_evnts = false;
    public bool txtChngd = false;
    public bool autoLoad = false;
    public bool isReadOnly = false;
    public bool shdSelOne = false;
    public bool mustSelctSth = false;
    string srchWrd = "%";
    private string selItemTxt = "";

    bool addRec = false;
    bool editRec = false;
    bool addDtRec = false;
    bool editDtRec = false;
    bool isClosing = false;
    bool addRecsP = false;
    bool editRecsP = false;
    bool delRecsP = false;
    //bool beenToCheckBx = false;
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
    /*91*/"Add Customers/Suppliers", "Edit Customers/Suppliers", "Delete Customers/Suppliers"
    };
    #endregion

    #region "CUSTOMERS & SUPPLIERS..."
    private void createCstSplrRec(int orgid, string cstmrname,
  string cstmrdesc, string cstmrTyp, string clssfctn,
    int pyblAccntID, int rcvblAccntID, long prsnID, string gender, string dob)
    {
      string dateStr = cmnCde.getDB_Date_time();
      dob = DateTime.ParseExact(
          dob, "dd-MMM-yyyy",
          System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      string insSQL = "INSERT INTO scm.scm_cstmr_suplr(" +
            "cust_sup_name, cust_sup_desc, created_by, creation_date, last_update_by, last_update_date, " +
            "cust_sup_clssfctn, cust_or_sup, org_id, dflt_pybl_accnt_id, dflt_rcvbl_accnt_id, " +
            "lnkd_prsn_id,person_gender,dob_estblshmnt) " +
            "VALUES ('" + cstmrname.Replace("'", "''") +
            "', '" + cstmrdesc.Replace("'", "''") +
            "', " + cmnCde.User_id + ", '" + dateStr +
            "', " + cmnCde.User_id + ", '" + dateStr +
            "', '" + clssfctn.Replace("'", "''") +
            "', '" + cstmrTyp.Replace("'", "''") + "', " +
            orgid + ", " +
            pyblAccntID + ", " +
            rcvblAccntID + ", " + prsnID + ",'" + gender.Replace("'", "''") + "','" + dob.Replace("'", "''") + "')";
      cmnCde.insertDataNoParams(insSQL);
    }

    private void createCstSplrSiteRec(int cstmrID, string sitename,
  string sitedesc, string cntctPrsn, string cntctNos, string email,
      string bankNm, string bnkBrnch, string accNum, string blngAddrs,
      string shpngAddrs, int taxCode, int dscntCode, string swift_code,
             string nationality, string national_id_typ,
     string id_number, string date_issued, string expiry_date,
             string other_info)
    {
      string dateStr = cmnCde.getDB_Date_time();
      string insSQL = "INSERT INTO scm.scm_cstmr_suplr_sites(" +
            "cust_supplier_id, contact_person_name, contact_nos, email, created_by, " +
            "creation_date, last_update_by, last_update_date, site_name, site_desc, " +
            "bank_name, bank_branch, bank_accnt_number, wth_tax_code_id, discount_code_id, " +
            @"billing_address, ship_to_address, swift_code, 
            nationality, national_id_typ, id_number, date_issued, expiry_date, 
            other_info) " +
            "VALUES (" + cstmrID + ", '" + cntctPrsn.Replace("'", "''") +
            "', '" + cntctNos.Replace("'", "''") +
            "', '" + email.Replace("'", "''") +
            "', " + cmnCde.User_id + ", '" + dateStr +
            "', " + cmnCde.User_id + ", '" + dateStr +
            "', '" + sitename.Replace("'", "''") +
            "', '" + sitedesc.Replace("'", "''") + "', '" +
            bankNm.Replace("'", "''") + "', '" + bnkBrnch.Replace("'", "''") +
            "', '" + accNum.Replace("'", "''") + "', " + taxCode + ", " + dscntCode +
            ", '" + blngAddrs.Replace("'", "''") + "', '" + shpngAddrs.Replace("'", "''") +
            "', '" + swift_code.Replace("'", "''") + "', '" + nationality.Replace("'", "''") +
            "', '" + national_id_typ.Replace("'", "''") + "', '" + id_number.Replace("'", "''") +
            "', '" + date_issued.Replace("'", "''") + "', '" + expiry_date.Replace("'", "''") +
            "', '" + other_info.Replace("'", "''") + "')";
      cmnCde.insertDataNoParams(insSQL);
    }


    private void updtCstSplrRec(int spplrid, string cstmrname,
  string cstmrdesc, string cstmrTyp, string clssfctn, int pyblAccntID,
     int rcvblAccntID, long prsnID, string gender, string dob)
    {
      cmnCde.Extra_Adt_Trl_Info = "";
      string dateStr = cmnCde.getDB_Date_time();
      dob = DateTime.ParseExact(
          dob, "dd-MMM-yyyy",
          System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

      string updtSQL = "UPDATE scm.scm_cstmr_suplr SET " +
            "cust_sup_name = '" + cstmrname.Replace("'", "''") +
            "', cust_sup_desc = '" + cstmrdesc.Replace("'", "''") +
            "', last_update_by = " + cmnCde.User_id +
            ", last_update_date = '" + dateStr +
            "', cust_sup_clssfctn='" + clssfctn.Replace("'", "''") +
            "', cust_or_sup='" + cstmrTyp.Replace("'", "''") +
            "', dflt_pybl_accnt_id=" + pyblAccntID +
            ", lnkd_prsn_id=" + prsnID +
            ", person_gender='" + gender.Replace("'", "''") +
            "', dob_estblshmnt='" + dob.Replace("'", "''") +
            "' WHERE (cust_sup_id = " + spplrid + ")";
      /*, dflt_rcvbl_accnt_id=" + rcvblAccntID +
            "*/
      cmnCde.updateDataNoParams(updtSQL);
    }

    private void updtCstSplrSiteRec(int siteID, string sitename,
  string sitedesc, string cntctPrsn, string cntctNos, string email,
      string bankNm, string bnkBrnch, string accNum, string blngAddrs,
      string shpngAddrs, int taxCode, int dscntCode, string swift_code,
             string nationality, string national_id_typ,
     string id_number, string date_issued, string expiry_date,
             string other_info)
    {
      string dateStr = cmnCde.getDB_Date_time();
      string updtSQL = "UPDATE scm.scm_cstmr_suplr_sites " +
   "SET contact_person_name='" + cntctPrsn.Replace("'", "''") +
            "', contact_nos='" + cntctNos.Replace("'", "''") +
            "', email='" + email.Replace("'", "''") +
            "', last_update_by=" + cmnCde.User_id + ", last_update_date='" + dateStr +
            "', site_name='" + sitename.Replace("'", "''") +
            "', site_desc='" + sitedesc.Replace("'", "''") + "', bank_name='" +
            bankNm.Replace("'", "''") + "', bank_branch='" + bnkBrnch.Replace("'", "''") +
            "', bank_accnt_number='" + accNum.Replace("'", "''") +
            "', wth_tax_code_id=" + taxCode + ", discount_code_id=" + dscntCode +
            ", billing_address='" + blngAddrs.Replace("'", "''") + "', " +
       "ship_to_address='" + shpngAddrs.Replace("'", "''") + "', " +
       "swift_code='" + swift_code.Replace("'", "''") + "', " +
       "nationality='" + nationality.Replace("'", "''") + "', " +
       "national_id_typ='" + national_id_typ.Replace("'", "''") + "', " +
       "id_number='" + id_number.Replace("'", "''") + "', " +
       "date_issued='" + date_issued.Replace("'", "''") + "', " +
       "expiry_date='" + expiry_date.Replace("'", "''") + "', " +
       "other_info='" + other_info.Replace("'", "''") + "' " +
   "WHERE cust_sup_site_id = " + siteID + "";
      cmnCde.updateDataNoParams(updtSQL);
    }

    private bool isCstSplrSiteInUse(int recID)
    {
      string strSql = "SELECT a.supplier_site_id " +
       "FROM scm.scm_prchs_docs_hdr a " +
       "WHERE(a.supplier_site_id = " + recID + ")";
      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return true;
      }
      strSql = "SELECT a.customer_site_id " +
       "FROM scm.scm_sales_invc_hdr a " +
       "WHERE(a.customer_site_id = " + recID + ")";
      dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return true;
      }
      return false;
    }

    private bool isCstSplrInUse(int recID)
    {
      //string strSql = "SELECT a.cust_sup_site_id " +
      // "FROM scm.scm_cstmr_suplr_sites a " +
      // "WHERE(a.cust_supplier_id = " + recID + ")";
      //DataSet dtst = cmnCde.selectDataNoParams(strSql);
      //if (dtst.Tables[0].Rows.Count > 0)
      //{
      //  return true;
      //}
      string strSql = "SELECT a.supplier_id " +
       "FROM scm.scm_prchs_docs_hdr a " +
       "WHERE(a.supplier_id = " + recID + ")";
      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return true;
      }
      strSql = "SELECT a.customer_id " +
       "FROM scm.scm_sales_invc_hdr a " +
       "WHERE(a.customer_id = " + recID + ")";
      dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return true;
      }
      strSql = "SELECT a.customer_id " +
      "FROM accb.accb_rcvbls_invc_hdr a " +
      "WHERE(a.customer_id = " + recID + ")";
      dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return true;
      }
      strSql = "SELECT a.supplier_id " +
      "FROM accb.accb_pybls_invc_hdr a " +
      "WHERE(a.supplier_id = " + recID + ")";
      dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return true;
      }
      return false;
    }

    public int get_DfltSalesLbltyAcnt(int orgID)
    {
      string strSql = "SELECT sales_lblty_acnt_id " +
       "FROM scm.scm_dflt_accnts a " +
       "WHERE(a.org_id = " + orgID + ")";

      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return -1;
    }

    private int get_DfltPyblAcnt(int orgID)
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

    public int get_DfltRcptRcvblAcnt(int orgID)
    {
      string strSql = "SELECT rcpt_rcvbl_acnt_id " +
       "FROM scm.scm_dflt_accnts a " +
       "WHERE(a.org_id = " + orgID + ")";

      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return -1;
    }

    private int get_DfltRcvblAcnt(int orgID)
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
    private int getCstmrSplrID(string cstmrname, int orgid)
    {
      DataSet dtSt = new DataSet();
      string sqlStr = "select cust_sup_id from scm.scm_cstmr_suplr where lower(cust_sup_name) = '" +
       cstmrname.Replace("'", "''").ToLower() + "' and org_id = " + orgid;
      dtSt = cmnCde.selectDataNoParams(sqlStr);
      if (dtSt.Tables[0].Rows.Count > 0)
      {
        return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return -1;
      }
    }

    private int getCstmrSplrSiteID(string cstmrsitename, int cstmrid)
    {
      DataSet dtSt = new DataSet();
      string sqlStr = "select cust_sup_site_id from scm.scm_cstmr_suplr_sites where lower(site_name) = '" +
       cstmrsitename.Replace("'", "''").ToLower() + "' and cust_supplier_id = " + cstmrid;
      dtSt = cmnCde.selectDataNoParams(sqlStr);
      if (dtSt.Tables[0].Rows.Count > 0)
      {
        return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return -1;
      }
    }

    private void updtDOBs()
    {
      string dateStr = cmnCde.getDB_Date_time();
      string updtSQL = "UPDATE scm.scm_cstmr_suplr SET dob_estblshmnt=substr(creation_date,1,10), " +
       "last_update_by=" + cmnCde.User_id + ", last_update_date='" + dateStr +
            "'  WHERE dob_estblshmnt='' or dob_estblshmnt IS NULL";
      cmnCde.updateDataNoParams(updtSQL);
    }

    private DataSet get_One_CstmrDet(int cstmrID)
    {
      string strSql = "SELECT a.cust_sup_id, a.cust_or_sup, a.cust_sup_name, a.cust_sup_desc, " +
      @"a.cust_sup_clssfctn, a.dflt_pybl_accnt_id, a.dflt_rcvbl_accnt_id, a.lnkd_prsn_id, 
   a.person_gender, to_char(to_timestamp(a.dob_estblshmnt,'YYYY-MM-DD'),'DD-Mon-YYYY') " +
       "FROM scm.scm_cstmr_suplr a " +
       "WHERE(a.cust_sup_id = " + cstmrID + ") ORDER BY a.cust_or_sup, a.cust_sup_name";

      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      //Global.taxFrm.rec_SQL = strSql;
      return dtst;
    }

    private DataSet get_One_CstmrSitesDt(int cstmrSiteID)
    {
      string strSql = "SELECT a.cust_sup_site_id, a.site_name, a.site_desc, " +
       "a.bank_name, a.bank_branch, a.bank_accnt_number, a.wth_tax_code_id, " +
       "a.discount_code_id, a.billing_address, a.ship_to_address, " +
       @"a.contact_person_name, a.contact_nos, a.email, a.swift_code, 
       a.nationality, a.national_id_typ, a.id_number, a.date_issued, a.expiry_date, 
       a.other_info " +
       "FROM scm.scm_cstmr_suplr_sites a " +
       "WHERE(a.cust_sup_site_id = " + cstmrSiteID +
       ") ORDER BY a.site_name";

      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      //Global.taxFrm.rec_SQL = strSql;
      return dtst;
    }

    private DataSet get_One_CstmrBscSites(long cstmrID, long siteID)
    {
      string whrcls = "";
      if (siteID > 0 && this.cstspplrID == cstmrID)
      {
        whrcls = " and a.cust_sup_site_id=" + siteID;
      }
      string strSql = "SELECT a.cust_sup_site_id, a.site_name " +
       "FROM scm.scm_cstmr_suplr_sites a " +
       "WHERE(a.cust_supplier_id = " + cstmrID + whrcls +
       ") ORDER BY a.cust_sup_site_id DESC LIMIT 1 OFFSET 0 ";

      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      this.recDt_SQL = strSql;
      return dtst;
    }

    private DataSet get_Basic_Cstmr(string searchWord, string searchIn,
  Int64 offset, int limit_size, int orgID)
    {
      string strSql = "";
      if (searchIn == "Customer/Supplier Name")
      {
        strSql = "SELECT a.cust_sup_id, a.cust_sup_name, a.cust_or_sup " +
       "FROM scm.scm_cstmr_suplr a " +
       "WHERE ((a.cust_sup_name ilike '" + searchWord.Replace("'", "''") +
       "') AND (a.org_id = " + orgID + ") AND (a.cust_or_sup ilike '%" + this.docType.Replace("'", "''") + "%' and is_enabled='1')) ORDER BY a.cust_sup_id DESC LIMIT " + limit_size +
       " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
      }
      else if (searchIn == "Customer/Supplier Description")
      {
        strSql = "SELECT a.cust_sup_id, a.cust_sup_name, a.cust_or_sup " +
       "FROM scm.scm_cstmr_suplr a " +
      "WHERE ((a.cust_sup_desc ilike '" + searchWord.Replace("'", "''") +
       "') AND (a.org_id = " + orgID + ") AND (a.cust_or_sup ilike '%" + this.docType.Replace("'", "''") + "%' and is_enabled='1')) ORDER BY a.cust_sup_id DESC LIMIT " + limit_size +
       " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
      }
      else if (searchIn == "Customer/Supplier Type")
      {
        strSql = "SELECT a.cust_sup_id, a.cust_sup_name, a.cust_or_sup " +
       "FROM scm.scm_cstmr_suplr a " +
      "WHERE ((a.cust_or_sup ilike '" + searchWord.Replace("'", "''") +
       "') AND (a.org_id = " + orgID + ") AND (a.cust_or_sup ilike '%" + this.docType.Replace("'", "''") + "%' and is_enabled='1')) ORDER BY a.cust_sup_id DESC LIMIT " + limit_size +
       " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
      }
      this.rec_SQL = strSql;
      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      return dtst;
    }

    private long get_Total_Cstmr(string searchWord, string searchIn, int orgID)
    {
      string strSql = "";
      if (searchIn == "Customer/Supplier Name")
      {
        strSql = "SELECT count(1) " +
        "FROM scm.scm_cstmr_suplr a " +
       "WHERE ((a.cust_sup_name ilike '" + searchWord.Replace("'", "''") +
       "') AND (a.org_id = " + orgID + ") AND (a.cust_or_sup ilike '%" + this.docType.Replace("'", "''") + "%' and is_enabled='1'))";
      }
      else if (searchIn == "Customer/Supplier Description")
      {
        strSql = "SELECT count(1)  " +
        "FROM scm.scm_cstmr_suplr a " +
      "WHERE ((a.cust_sup_desc ilike '" + searchWord.Replace("'", "''") +
       "') AND (a.org_id = " + orgID + ") AND (a.cust_or_sup ilike '%" + this.docType.Replace("'", "''") + "%' and is_enabled='1'))";
      }
      else if (searchIn == "Customer/Supplier Type")
      {
        strSql = "SELECT count(1)  " +
        "FROM scm.scm_cstmr_suplr a " +
      "WHERE ((a.cust_or_sup ilike '" + searchWord.Replace("'", "''") +
       "') AND (a.org_id = " + orgID + ") AND (a.cust_or_sup ilike '%" + this.docType.Replace("'", "''") + "%' and is_enabled='1'))";
      }
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
    #endregion

    #region "FORM EVENTS..."
    public cstSpplrDiag()
    {
      InitializeComponent();
    }

    public void cstSpplrDiag_Load(object sender, EventArgs e)
    {
      Color[] clrs = cmnCde.getColors();
      this.BackColor = clrs[0];
      cmnCde.DefaultPrvldgs = this.dfltPrvldgs;

      this.disableFormButtons();
      this.loadPanel();

      System.Windows.Forms.Application.DoEvents();
      this.searchForTextBox.Select();
      System.Windows.Forms.Application.DoEvents();
      this.searchForTextBox.Focus();
      this.searchForTextBox.SelectAll();
    }

    public void disableFormButtons()
    {
      bool vwSQL = cmnCde.test_prmssns(this.dfltPrvldgs[9]);
      bool rcHstry = cmnCde.test_prmssns(this.dfltPrvldgs[10]);
      this.addRecsP = cmnCde.test_prmssns(this.dfltPrvldgs[91]);
      this.editRecsP = cmnCde.test_prmssns(this.dfltPrvldgs[92]);
      this.delRecsP = cmnCde.test_prmssns(this.dfltPrvldgs[93]);

      this.saveButton.Enabled = false;
      if (this.isReadOnly == false)
      {
        this.addButton.Enabled = this.addRecsP;
        this.editButton.Enabled = this.editRecsP;
        this.delButton.Enabled = this.delRecsP;
      }
      else
      {
        this.addButton.Enabled = false;
        this.editButton.Enabled = false;
        this.delButton.Enabled = false;
        this.okButton.Enabled = false;
      }


    }

    #endregion

    #region "CUSTOMERS & SUPPLIERS..."
    public void loadPanel()
    {
      this.obey_evnts = false;
      if (this.searchInComboBox.SelectedIndex < 0)
      {
        this.searchInComboBox.SelectedIndex = 0;
      }
      if (this.searchForTextBox.Text.Contains("%") == false)
      {
        this.searchForTextBox.Text = "%" + this.searchForTextBox.Text.Replace(" ", "%") + "%";
      }
      if (this.searchForTextBox.Text == "%%")
      {
        this.searchForTextBox.Text = "%";
      }
      int dsply = 0;
      if (this.dsplySizeComboBox.Text == ""
        || int.TryParse(this.dsplySizeComboBox.Text, out dsply) == false)
      {
        this.dsplySizeComboBox.Text = cmnCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      }
      this.is_last_rec = false;
      this.totl_rec = cmnCde.Big_Val;
      this.getPnlData();
      this.obey_evnts = true;
    }

    public void getPnlData()
    {
      this.updtTotals();
      this.populateListVw();
      this.updtNavLabels();
    }

    public void updtTotals()
    {
      cmnCde.navFuncts.FindNavigationIndices(
        long.Parse(this.dsplySizeComboBox.Text), this.totl_rec);
      if (this.rec_cur_indx >= cmnCde.navFuncts.totalGroups)
      {
        this.rec_cur_indx = cmnCde.navFuncts.totalGroups - 1;
      }
      if (this.rec_cur_indx < 0)
      {
        this.rec_cur_indx = 0;
      }
      cmnCde.navFuncts.currentNavigationIndex = this.rec_cur_indx;
    }

    public void updtNavLabels()
    {
      this.moveFirstButton.Enabled = cmnCde.navFuncts.moveFirstBtnStatus();
      this.movePreviousButton.Enabled = cmnCde.navFuncts.movePrevBtnStatus();
      this.moveNextButton.Enabled = cmnCde.navFuncts.moveNextBtnStatus();
      this.moveLastButton.Enabled = cmnCde.navFuncts.moveLastBtnStatus();
      this.positionTextBox.Text = cmnCde.navFuncts.displayedRecordsNumbers();
      if (this.is_last_rec == true ||
        this.totl_rec != cmnCde.Big_Val)
      {
        this.totalRecsLabel.Text = cmnCde.navFuncts.totalRecordsLabel();
      }
      else
      {
        this.totalRecsLabel.Text = "of Total";
      }
    }


    public void populateListVw()
    {
      this.obey_evnts = false;
      DataSet dtst = this.get_Basic_Cstmr(this.searchForTextBox.Text,
        this.searchInComboBox.Text, this.rec_cur_indx,
        int.Parse(this.dsplySizeComboBox.Text), cmnCde.Org_id);
      this.cstSplrListView.Items.Clear();

      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        this.last_rec_num = cmnCde.navFuncts.startIndex() + i;
        ListViewItem nwItem = new ListViewItem(new string[] {
    (cmnCde.navFuncts.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),
    dtst.Tables[0].Rows[i][0].ToString(),
    dtst.Tables[0].Rows[i][2].ToString()});

        if (int.Parse(dtst.Tables[0].Rows[i][0].ToString()) == this.cstspplrID)
        {
          nwItem.Checked = true;
        }
        this.cstSplrListView.Items.Add(nwItem);
      }
      this.correctNavLbls(dtst);
      if (this.cstSplrListView.CheckedItems.Count > 0)
      {
        this.obey_evnts = true;
        this.cstSplrListView.CheckedItems[0].Selected = true;
        if (this.autoLoad && this.isReadOnly == false)
        {
          this.okButton.PerformClick();
        }
      }
      else if (this.cstSplrListView.Items.Count > 0)
      {
        this.obey_evnts = true;
        this.cstSplrListView.Items[0].Selected = true;
        if (this.autoLoad && this.isReadOnly == false)
        {
          this.okButton.PerformClick();
        }
      }
      else
      {
        this.populateDet(-10000);
      }
      this.obey_evnts = true;
    }

    public void populateSitesListVw(int cstmrID)
    {
      this.obey_evnts = false;
      DataSet dtst = this.get_One_CstmrBscSites(cstmrID, this.siteID);

      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        this.siteIDTextBox.Text = dtst.Tables[0].Rows[i][0].ToString();
        this.siteNameTextbox.Text = dtst.Tables[0].Rows[i][1].ToString();
      }
      if (this.siteIDTextBox.Text != "" && this.siteIDTextBox.Text != "-1")
      {
        this.obey_evnts = true;
        this.populateSiteDet(int.Parse(this.siteIDTextBox.Text));
      }
      else
      {
        this.populateSiteDet(-10000);
      }
      this.obey_evnts = true;
    }

    public void populateSiteDet(int cstmrSiteID)
    {
      this.clearSiteDetInfo();
      if (this.editDtRec == false)
      {
        this.disableSiteDetEdit();
      }
      this.obey_evnts = false;
      DataSet dtst = this.get_One_CstmrSitesDt(cstmrSiteID);
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        this.siteIDTextBox.Text = dtst.Tables[0].Rows[i][0].ToString();
        this.siteNameTextbox.Text = dtst.Tables[0].Rows[i][1].ToString();
        this.bnkNmTextBox.Text = dtst.Tables[0].Rows[i][3].ToString();
        this.brnchNmTextBox.Text = dtst.Tables[0].Rows[i][4].ToString();
        this.acntNumTextBox.Text = dtst.Tables[0].Rows[i][5].ToString();
        this.swiftCodeTextBox.Text = dtst.Tables[0].Rows[i][13].ToString();

        this.bllngAddrsTextBox.Text = dtst.Tables[0].Rows[i][8].ToString();
        this.shipAddrsTextBox.Text = dtst.Tables[0].Rows[i][9].ToString();
        this.cntctPrsnTextBox.Text = dtst.Tables[0].Rows[i][10].ToString();
        this.cntctNosTextBox.Text = dtst.Tables[0].Rows[i][11].ToString();

        this.emailTextBox.Text = dtst.Tables[0].Rows[i][12].ToString();

        this.ntnltyTextBox.Text = dtst.Tables[0].Rows[i][14].ToString();
        this.idTypeTextBox.Text = dtst.Tables[0].Rows[i][15].ToString();
        this.idNumTextBox.Text = dtst.Tables[0].Rows[i][16].ToString();
        this.dateIssuedTextBox.Text = dtst.Tables[0].Rows[i][17].ToString();
        this.expryDateTextBox.Text = dtst.Tables[0].Rows[i][18].ToString();
        this.otherInfoTextBox.Text = dtst.Tables[0].Rows[i][19].ToString();

      }
      this.obey_evnts = true;
    }

    public void populateDet(int cstmrID)
    {
      this.clearDetInfo();
      this.clearSiteDetInfo();
      if (this.editRec == false)
      {
        this.disableDetEdit();
        this.disableSiteDetEdit();
      }
      this.obey_evnts = false;
      this.updtDOBs();
      DataSet dtst = this.get_One_CstmrDet(cstmrID);
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        this.idTextBox.Text = dtst.Tables[0].Rows[i][0].ToString();//this.taxListView.SelectedItems[0].SubItems[2].Text;
        this.nameTextBox.Text = dtst.Tables[0].Rows[i][2].ToString();//this.taxListView.SelectedItems[0].SubItems[1].Text;
        //this.descTextBox.Text = dtst.Tables[0].Rows[i][3].ToString();//this.taxListView.SelectedItems[0].SubItems[3].Text;
        if (this.editRec == false && this.addRec == false)
        {
          this.typeComboBox.Items.Clear();
          this.typeComboBox.Items.Add(dtst.Tables[0].Rows[i][1].ToString());
        }
        this.typeComboBox.SelectedItem = dtst.Tables[0].Rows[i][1].ToString();//;

        this.lnkdPrsnIDTextBox.Text = dtst.Tables[0].Rows[i][7].ToString();
        //this.lnkdPersonNoTextBox.Text = cmnCde.getPrsnLocID(long.Parse(dtst.Tables[0].Rows[i][7].ToString()))
        //  + " - " + cmnCde.getPrsnName(long.Parse(dtst.Tables[0].Rows[i][7].ToString()));

        this.lnkdPersonNoTextBox.Text =
  (cmnCde.getPrsnName(long.Parse(dtst.Tables[0].Rows[i][7].ToString()))
  + " (" + cmnCde.getPrsnLocID(long.Parse(dtst.Tables[0].Rows[i][7].ToString())) + ")").Replace(" ()", "");

        this.classfctnTextBox.Text = dtst.Tables[0].Rows[i][4].ToString();
        //this.taxListView.SelectedItems[0].SubItems[11].Text;
        this.genderTextBox.Text = dtst.Tables[0].Rows[i][8].ToString();
        this.dobTextBox.Text = dtst.Tables[0].Rows[i][9].ToString();
      }
      this.populateSitesListVw(cstmrID);

      this.obey_evnts = true;
    }

    public void correctNavLbls(DataSet dtst)
    {
      long totlRecs = dtst.Tables[0].Rows.Count;
      if (this.rec_cur_indx == 0 && totlRecs == 0)
      {
        this.is_last_rec = true;
        this.totl_rec = 0;
        this.last_rec_num = 0;
        this.rec_cur_indx = 0;
        this.updtTotals();
        this.updtNavLabels();
      }
      else if (this.totl_rec == cmnCde.Big_Val
     && totlRecs < long.Parse(this.dsplySizeComboBox.Text))
      {
        this.totl_rec = this.last_rec_num;
        if (totlRecs == 0)
        {
          this.rec_cur_indx -= 1;
          this.updtTotals();
          this.populateListVw();
        }
        else
        {
          this.updtTotals();
        }
      }
    }

    public bool shdObeyEvts()
    {
      return this.obey_evnts;
    }

    public void PnlNavButtons(object sender, System.EventArgs e)
    {
      System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
      this.totalRecsLabel.Text = "";
      if (sentObj.Name.ToLower().Contains("first"))
      {
        this.is_last_rec = false;
        this.rec_cur_indx = 0;
      }
      else if (sentObj.Name.ToLower().Contains("previous"))
      {
        this.is_last_rec = false;
        this.rec_cur_indx -= 1;
      }
      else if (sentObj.Name.ToLower().Contains("next"))
      {
        this.is_last_rec = false;
        this.rec_cur_indx += 1;
      }
      else if (sentObj.Name.ToLower().Contains("last"))
      {
        this.is_last_rec = true;
        this.totl_rec = this.get_Total_Cstmr(this.searchForTextBox.Text,
          this.searchInComboBox.Text, cmnCde.Org_id);
        this.updtTotals();
        this.rec_cur_indx = cmnCde.navFuncts.totalGroups - 1;
      }
      this.getPnlData();
    }

    public void clearDetInfo()
    {
      this.obey_evnts = false;
      //this.saveButton.Enabled = false;
      this.idTextBox.Text = "-1";
      this.nameTextBox.Text = "";
      //this.typeComboBox.Items.Clear();
      //this.descTextBox.Text = "";
      this.classfctnTextBox.Text = "";
      //this.lbltyAcntIDTextBox.Text = "-1";
      //this.lbltyAcntTextBox.Text = "";
      this.lnkdPrsnIDTextBox.Text = "-1";
      this.lnkdPersonNoTextBox.Text = "";

      //this.rcvblAccntIDTextBox.Text = "-1";
      //this.rcvblAccntTextBox.Text = "";
      this.genderTextBox.Text = "Not Applicable";
      this.dobTextBox.Text = cmnCde.getFrmtdDB_Date_time().Substring(0, 11);
      this.obey_evnts = true;
    }

    public void prpareForDetEdit()
    {
      this.saveButton.Enabled = true;
      this.nameTextBox.ReadOnly = false;
      this.nameTextBox.BackColor = Color.FromArgb(255, 255, 128);
      //this.descTextBox.ReadOnly = false;
      //this.descTextBox.BackColor = Color.White;

      this.lnkdPersonNoTextBox.ReadOnly = false;
      this.lnkdPersonNoTextBox.BackColor = Color.White;

      this.classfctnTextBox.ReadOnly = false;
      this.classfctnTextBox.BackColor = Color.FromArgb(255, 255, 128);

      this.genderTextBox.ReadOnly = false;
      this.genderTextBox.BackColor = Color.FromArgb(255, 255, 128);

      this.dobTextBox.ReadOnly = false;
      this.dobTextBox.BackColor = Color.FromArgb(255, 255, 128);

      //this.lbltyAcntTextBox.ReadOnly = false;
      //this.lbltyAcntTextBox.BackColor = Color.FromArgb(255, 255, 128);

      //this.rcvblAccntTextBox.ReadOnly = false;
      //this.rcvblAccntTextBox.BackColor = Color.FromArgb(255, 255, 128);

      string selItm = this.typeComboBox.Text;
      this.typeComboBox.Items.Clear();
      if (this.docType == "")
      {
        this.typeComboBox.Items.Add("Customer");
        this.typeComboBox.Items.Add("Supplier");
      }
      else
      {
        this.typeComboBox.Items.Add(this.docType);
      }
      this.typeComboBox.Items.Add("Customer/Supplier");
      if (this.editRec == true)
      {
        this.typeComboBox.SelectedItem = selItm;
      }
    }

    public void disableDetEdit()
    {
      this.addRec = false;
      this.editRec = false;
      this.saveButton.Enabled = false;
      if (this.isReadOnly == false)
      {
        this.addButton.Enabled = this.addRecsP;
        this.editButton.Enabled = this.editRecsP;
        this.delButton.Enabled = this.delRecsP;
      }
      else
      {
        this.addButton.Enabled = false;
        this.editButton.Enabled = false;
        this.delButton.Enabled = false;
        this.okButton.Enabled = false;
      }

      this.editButton.Text = "EDIT";
      this.nameTextBox.ReadOnly = true;
      this.nameTextBox.BackColor = Color.WhiteSmoke;
      //this.descTextBox.ReadOnly = true;
      //this.descTextBox.BackColor = Color.WhiteSmoke;
      this.classfctnTextBox.ReadOnly = true;
      this.classfctnTextBox.BackColor = Color.WhiteSmoke;

      this.genderTextBox.ReadOnly = true;
      this.genderTextBox.BackColor = Color.WhiteSmoke;

      this.dobTextBox.ReadOnly = true;
      this.dobTextBox.BackColor = Color.WhiteSmoke;

      this.lnkdPersonNoTextBox.ReadOnly = true;
      this.lnkdPersonNoTextBox.BackColor = Color.WhiteSmoke;

      //this.lbltyAcntTextBox.ReadOnly = true;
      //this.lbltyAcntTextBox.BackColor = Color.WhiteSmoke;

      //this.rcvblAccntTextBox.ReadOnly = true;
      //this.rcvblAccntTextBox.BackColor = Color.WhiteSmoke;
    }

    public void clearSiteDetInfo()
    {
      this.obey_evnts = false;
      //this.saveDtButton.Enabled = false;
      //this.addDtButton.Enabled = this.addRecsP;
      //this.editDtButton.Enabled = this.editRecsP;
      //this.delDtButton.Enabled = this.delRecsP;
      this.siteIDTextBox.Text = "-1";
      this.siteNameTextbox.Text = "";
      //this.siteDescTextBox.Text = "";
      this.bnkNmTextBox.Text = "";
      this.brnchNmTextBox.Text = "";
      this.acntNumTextBox.Text = "";
      //this.wthldngTaxTexBox.Text = "";
      //this.wthTaxIDTextBox.Text = "-1";
      //this.dscntTextBox.Text = "";
      //this.dscntIDTextBox.Text = "-1";
      this.bllngAddrsTextBox.Text = "";
      this.shipAddrsTextBox.Text = "";
      this.cntctPrsnTextBox.Text = "";
      this.cntctNosTextBox.Text = "";
      this.emailTextBox.Text = "";

      //this.swiftCodeTextBox.Text = "";
      this.ntnltyTextBox.Text = "";
      this.idTypeTextBox.Text = "";
      this.idNumTextBox.Text = "";
      this.dateIssuedTextBox.Text = "";
      this.expryDateTextBox.Text = "";
      this.otherInfoTextBox.Text = "";
      this.obey_evnts = true;
    }

    public void prpareForSiteDetEdit()
    {
      //this.saveDtButton.Enabled = true;
      this.siteNameTextbox.ReadOnly = false;
      this.siteNameTextbox.BackColor = Color.FromArgb(255, 255, 128);

      //this.siteDescTextBox.ReadOnly = false;
      //this.siteDescTextBox.BackColor = Color.White;

      this.swiftCodeTextBox.ReadOnly = false;
      this.swiftCodeTextBox.BackColor = Color.White;

      this.ntnltyTextBox.ReadOnly = false;
      this.ntnltyTextBox.BackColor = Color.White;

      this.idTypeTextBox.ReadOnly = false;
      this.idTypeTextBox.BackColor = Color.White;

      this.idNumTextBox.ReadOnly = false;
      this.idNumTextBox.BackColor = Color.White;

      this.dateIssuedTextBox.ReadOnly = false;
      this.dateIssuedTextBox.BackColor = Color.White;

      this.expryDateTextBox.ReadOnly = false;
      this.expryDateTextBox.BackColor = Color.White;

      this.otherInfoTextBox.ReadOnly = false;
      this.otherInfoTextBox.BackColor = Color.White;

      this.bnkNmTextBox.ReadOnly = false;
      this.bnkNmTextBox.BackColor = Color.White;

      this.brnchNmTextBox.ReadOnly = false;
      this.brnchNmTextBox.BackColor = Color.White;

      this.acntNumTextBox.ReadOnly = false;
      this.acntNumTextBox.BackColor = Color.White;

      this.bllngAddrsTextBox.ReadOnly = false;
      this.bllngAddrsTextBox.BackColor = Color.White;

      this.shipAddrsTextBox.ReadOnly = false;
      this.shipAddrsTextBox.BackColor = Color.White;

      this.cntctPrsnTextBox.ReadOnly = false;
      this.cntctPrsnTextBox.BackColor = Color.FromArgb(255, 255, 128);

      this.cntctNosTextBox.ReadOnly = false;
      this.cntctNosTextBox.BackColor = Color.White;

      this.emailTextBox.ReadOnly = false;
      this.emailTextBox.BackColor = Color.White;

      //if (this.wthTaxButton.Enabled == true)
      //{
      // this.wthldngTaxTexBox.ReadOnly = false;
      // this.wthldngTaxTexBox.BackColor = Color.White;
      //}
      //else
      //{
      // this.wthldngTaxTexBox.ReadOnly = true;
      // this.wthldngTaxTexBox.BackColor = Color.WhiteSmoke;
      //}
      //if (this.dscntButton.Enabled == true)
      //{
      // //this.dscntTextBox.ReadOnly = false;
      // //this.dscntTextBox.BackColor = Color.White;
      //}
      //else
      //{
      // //this.dscntTextBox.ReadOnly = true;
      // //this.dscntTextBox.BackColor = Color.WhiteSmoke;
      //}
    }

    public void disableSiteDetEdit()
    {
      this.addDtRec = false;
      this.editDtRec = false;
      //this.saveDtButton.Enabled = false;
      //this.editDtButton.Text = "EDIT";
      this.siteNameTextbox.ReadOnly = true;
      this.siteNameTextbox.BackColor = Color.WhiteSmoke;

      //this.siteDescTextBox.ReadOnly = true;
      //this.siteDescTextBox.BackColor = Color.WhiteSmoke;

      this.swiftCodeTextBox.ReadOnly = true;
      this.swiftCodeTextBox.BackColor = Color.WhiteSmoke;

      this.ntnltyTextBox.ReadOnly = true;
      this.ntnltyTextBox.BackColor = Color.WhiteSmoke;

      this.idTypeTextBox.ReadOnly = true;
      this.idTypeTextBox.BackColor = Color.WhiteSmoke;

      this.idNumTextBox.ReadOnly = true;
      this.idNumTextBox.BackColor = Color.WhiteSmoke;

      this.dateIssuedTextBox.ReadOnly = true;
      this.dateIssuedTextBox.BackColor = Color.WhiteSmoke;

      this.expryDateTextBox.ReadOnly = true;
      this.expryDateTextBox.BackColor = Color.WhiteSmoke;

      this.otherInfoTextBox.ReadOnly = true;
      this.otherInfoTextBox.BackColor = Color.WhiteSmoke;

      this.bnkNmTextBox.ReadOnly = true;
      this.bnkNmTextBox.BackColor = Color.WhiteSmoke;

      this.brnchNmTextBox.ReadOnly = true;
      this.brnchNmTextBox.BackColor = Color.WhiteSmoke;

      this.acntNumTextBox.ReadOnly = true;
      this.acntNumTextBox.BackColor = Color.WhiteSmoke;

      this.bllngAddrsTextBox.ReadOnly = true;
      this.bllngAddrsTextBox.BackColor = Color.WhiteSmoke;

      this.shipAddrsTextBox.ReadOnly = true;
      this.shipAddrsTextBox.BackColor = Color.WhiteSmoke;

      this.cntctPrsnTextBox.ReadOnly = true;
      this.cntctPrsnTextBox.BackColor = Color.WhiteSmoke;

      this.cntctNosTextBox.ReadOnly = true;
      this.cntctNosTextBox.BackColor = Color.WhiteSmoke;

      this.emailTextBox.ReadOnly = true;
      this.emailTextBox.BackColor = Color.WhiteSmoke;

      //this.wthldngTaxTexBox.ReadOnly = true;
      //this.wthldngTaxTexBox.BackColor = Color.WhiteSmoke;
      //this.dscntTextBox.ReadOnly = true;
      //this.dscntTextBox.BackColor = Color.WhiteSmoke;

    }
    #endregion

    public void genderButton_Click(object sender, EventArgs e)
    {
      this.genderLOVSrch("%", false);
    }

    public void genderLOVSrch(string srchWrd, bool autoLoad)
    {
      if (this.editRec == false && this.addRec == false)
      {
        cmnCde.showMsg("You must be in ADD/EDIT mode first!", 0);
        return;
      }
      //Gender
      int[] selVals = new int[1];
      selVals[0] = cmnCde.getPssblValID(this.genderTextBox.Text,
       cmnCde.getLovID("Gender"));
      DialogResult dgRes = cmnCde.showPssblValDiag(
       cmnCde.getLovID("Gender"), ref selVals, true, true,
       srchWrd, "Both", autoLoad);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.genderTextBox.Text = cmnCde.getPssblValNm(selVals[i]);
        }
      }
    }

    public void dobButton_Click(object sender, EventArgs e)
    {
      if (this.editRec == false && this.addRec == false)
      {
        cmnCde.showMsg("You must be in ADD/EDIT mode first!", 0);
        return;
      }
      cmnCde.selectDate(ref this.dobTextBox);
      if (this.dobTextBox.Text.Length > 11)
      {
        this.dobTextBox.Text = this.dobTextBox.Text.Substring(0, 11);
        //this.ageLabel.Text = Global.computePrsnAge(this.dobTextBox.Text);
      }
    }

    public void ntnltyButton_Click(object sender, EventArgs e)
    {
      this.ntnltyLOVSrch("%", false);
    }

    public void ntnltyLOVSrch(string srchWrd, bool autoLoad)
    {
      if (this.editRec == false && this.addRec == false)
      {
        cmnCde.showMsg("You must be in ADD/EDIT mode first!", 0);
        return;
      }
      //Nationalities
      int[] selVals = new int[1];
      selVals[0] = cmnCde.getPssblValID(this.ntnltyTextBox.Text,
        cmnCde.getLovID("Countries"));
      DialogResult dgRes = cmnCde.showPssblValDiag(
        cmnCde.getLovID("Countries"), ref selVals, true, false, srchWrd, "Both", autoLoad);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.ntnltyTextBox.Text = cmnCde.getPssblValNm(selVals[i]);
        }
      }
    }

    public void idTypLOVSrch(string srchWrd, bool autoLoad)
    {
      if (this.editRec == false && this.addRec == false)
      {
        cmnCde.showMsg("You must be in ADD/EDIT mode first!", 0);
        return;
      }
      //National ID Types
      int[] selVals = new int[1];
      selVals[0] = cmnCde.getPssblValID(this.idTypeTextBox.Text,
        cmnCde.getLovID("National ID Types"));
      DialogResult dgRes = cmnCde.showPssblValDiag(
        cmnCde.getLovID("National ID Types"), ref selVals,
        true, false, srchWrd, "Both", autoLoad);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.idTypeTextBox.Text = cmnCde.getPssblValNm(selVals[i]);
        }
      }
    }

    public void idTypeButton_Click(object sender, EventArgs e)
    {
      this.idTypLOVSrch("%", false);
    }

    public void dteIssuedButton_Click(object sender, EventArgs e)
    {
      if (this.editRec == false && this.addRec == false)
      {
        cmnCde.showMsg("You must be in ADD/EDIT mode first!", 0);
        return;
      }

      cmnCde.selectDate(ref this.dateIssuedTextBox);
      if (this.dateIssuedTextBox.Text.Length > 11)
      {
        this.dateIssuedTextBox.Text = this.dateIssuedTextBox.Text.Substring(0, 11);
      }
    }

    public void expryDateButton_Click(object sender, EventArgs e)
    {
      if (this.editRec == false && this.addRec == false)
      {
        cmnCde.showMsg("You must be in ADD/EDIT mode first!", 0);
        return;
      }

      cmnCde.selectDate(ref this.expryDateTextBox);
      if (this.expryDateTextBox.Text.Length > 11)
      {
        this.expryDateTextBox.Text = this.expryDateTextBox.Text.Substring(0, 11);
      }
    }

    public void clssfctnButton_Click(object sender, EventArgs e)
    {
      if (this.addRec == false && this.editRec == false)
      {
        cmnCde.showMsg("Must be in ADD/EDIT mode First!", 0);
        return;
      }
      string lovNm = "";
      if (this.typeComboBox.Text == "Customer")
      {
        lovNm = "Customer Classifications";
      }
      else if (this.typeComboBox.Text == "Supplier")
      {
        lovNm = "Supplier Classifications";
      }
      else
      {
        lovNm = "Customer/Supplier Classifications";
      }
      int[] selVals = new int[1];
      selVals[0] = cmnCde.getPssblValID(
        this.classfctnTextBox.Text, cmnCde.getLovID(lovNm));
      DialogResult dgRes = cmnCde.showPssblValDiag(
          cmnCde.getLovID(lovNm), ref selVals,
          true, false);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.classfctnTextBox.Text = cmnCde.getPssblValNm(selVals[i]);
        }
      }
    }

    public void bnkNmButton_Click(object sender, EventArgs e)
    {
      if (this.addDtRec == false && this.editDtRec == false)
      {
        cmnCde.showMsg("Must be in ADD/EDIT mode First!", 0);
        return;
      }
      string lovNm = "Banks";
      int[] selVals = new int[1];
      selVals[0] = cmnCde.getPssblValID(
        this.bnkNmTextBox.Text, cmnCde.getLovID(lovNm));
      DialogResult dgRes = cmnCde.showPssblValDiag(
          cmnCde.getLovID(lovNm), ref selVals,
          true, false);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.bnkNmTextBox.Text = cmnCde.getPssblValNm(selVals[i]);
        }
      }
    }

    public void brnchNmButton_Click(object sender, EventArgs e)
    {
      if (this.addDtRec == false && this.editDtRec == false)
      {
        cmnCde.showMsg("Must be in ADD/EDIT mode First!", 0);
        return;
      }
      string lovNm = "Bank Branches";
      int[] selVals = new int[1];
      selVals[0] = cmnCde.getPssblValID(
        this.brnchNmTextBox.Text, cmnCde.getLovID(lovNm));
      DialogResult dgRes = cmnCde.showPssblValDiag(
          cmnCde.getLovID(lovNm), ref selVals,
          true, false);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.brnchNmTextBox.Text = cmnCde.getPssblValNm(selVals[i]);
        }
      }
    }

    public void siteNameButton_Click(object sender, EventArgs e)
    {
      this.cstmrSiteLOVSearch("%");
    }

    private void cstmrSiteLOVSearch(string srchWrd)
    {
      //this.txtChngd = false;
      //if (this.addRec == false && this.editRec == false)
      //{
      // cmnCde.showMsg("Must be in ADD/EDIT mode First!", 0);
      // return;
      //}
      if (this.idTextBox.Text == "" || this.idTextBox.Text == "-1")
      {
        cmnCde.showMsg("Please pick a Customer/Supplier First!", 0);
        return;
      }

      string[] selVals = new string[1];
      selVals[0] = this.siteIDTextBox.Text;
      DialogResult dgRes = cmnCde.showPssblValDiag(
        cmnCde.getLovID("Customer/Supplier Sites"), ref selVals,
        true, false, int.Parse(this.idTextBox.Text),
       srchWrd, "Both", false);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.siteIDTextBox.Text = selVals[i];
          this.siteNameTextbox.Text = cmnCde.getGnrlRecNm(
            "scm.scm_cstmr_suplr_sites", "cust_sup_site_id", "site_name",
            long.Parse(selVals[i]));
        }

        if (this.siteIDTextBox.Text != "" && this.siteIDTextBox.Text != "-1")
        {
          //this.obey_evnts = true;
          this.populateSiteDet(int.Parse(this.siteIDTextBox.Text));
        }
        else
        {
          this.populateSiteDet(-10000);
          if (this.saveButton.Enabled == true)
          {
            EventArgs e = new EventArgs();
            this.addSiteButton_Click(this.addButton, e);
          }
        }
      }
      //this.txtChngd = false;
    }

    public void classfctnTextBox_TextChanged(object sender, EventArgs e)
    {
      if (!this.obey_evnts)
      {
        return;
      }
      this.txtChngd = true;

    }

    public void classfctnTextBox_Leave(object sender, EventArgs e)
    {
      if (this.txtChngd == false)
      {
        return;
      }
      this.txtChngd = false;
      TextBox mytxt = (TextBox)sender;
      this.obey_evnts = false;
      string srchWrd = mytxt.Text;
      if (!mytxt.Text.Contains("%"))
      {
        srchWrd = "%" + srchWrd.Replace(" ", "%") + "%";
      }

      if (mytxt.Name == "classfctnTextBox")
      {
        this.clsfctnLOVSearch(srchWrd);
      }
      else if (mytxt.Name == "bnkNmTextBox")
      {
        string lovNm = "Banks";
        string[] rslts = cmnCde.checkNGetLOVValue(srchWrd, "Both", cmnCde.getLovID(lovNm), -1, "", "", "");
        if (rslts[1] == "" && rslts[0] == "-1")
        {
          this.bnkNmTextBox.Text = "";
          this.bnkNmButton_Click(this.bnkNmButton, e);
        }
        else
        {
          this.bnkNmTextBox.Text = rslts[1];
        }
      }
      else if (mytxt.Name == "brnchNmTextBox")
      {
        string lovNm = "Bank Branches";
        string[] rslts = cmnCde.checkNGetLOVValue(srchWrd, "Both", cmnCde.getLovID(lovNm), -1, "", "", "");
        if (rslts[1] == "" && rslts[0] == "-1")
        {
          this.brnchNmTextBox.Text = "";
          this.brnchNmButton_Click(this.brnchNmButton, e);
        }
        else
        {
          this.brnchNmTextBox.Text = rslts[1];
        }
      }
      else if (mytxt.Name == "genderTextBox")
      {
        this.genderLOVSrch(srchWrd, true);
      }
      else if (mytxt.Name == "ntnltyTextBox")
      {
        this.ntnltyLOVSrch(srchWrd, true);
      }
      else if (mytxt.Name == "idTypeTextBox")
      {
        this.idTypLOVSrch(srchWrd, true);
      }
      else if (mytxt.Name == "dobTextBox")
      {
        this.dobTextBox.Text = cmnCde.checkNFormatDate(this.dobTextBox.Text).Substring(0, 11);
        //this.ageLabel.Text = Global.computePrsnAge(this.dobTextBox.Text);
      }
      this.obey_evnts = true;
      this.txtChngd = false;
    }

    public void clsfctnLOVSearch(string srchWrd)
    {
      if (this.addRec == false && this.editRec == false)
      {
        cmnCde.showMsg("Must be in ADD/EDIT mode First!", 0);
        return;
      }

      if (this.classfctnTextBox.Text == "")
      {
        this.classfctnTextBox.Text = "";
        return;
      }

      string lovNm = "";
      if (this.typeComboBox.Text == "Customer")
      {
        lovNm = "Customer Classifications";
      }
      else if (this.typeComboBox.Text == "Supplier")
      {
        lovNm = "Supplier Classifications";
      }
      else
      {
        lovNm = "Customer/Supplier Classifications";
      }
      int[] selVals = new int[1];
      selVals[0] = cmnCde.getPssblValID(
        this.classfctnTextBox.Text, cmnCde.getLovID(lovNm));
      DialogResult dgRes = cmnCde.showPssblValDiag(
          cmnCde.getLovID(lovNm), ref selVals,
          true, false, srchWrd, "Both", true);

      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.classfctnTextBox.Text = cmnCde.getPssblValNm(selVals[i]);
        }
      }
    }

    private void cstSplrListView_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.shdObeyEvts() == false)
      {
        return;
      }
      if (this.cstSplrListView.SelectedItems.Count > 0)
      {
        this.populateDet(int.Parse(this.cstSplrListView.SelectedItems[0].SubItems[2].Text));
      }
      else
      {
        this.populateDet(-100000);
      }
    }

    private void cstSplrListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
    {
      if (this.shdObeyEvts() == false)
      {
        return;
      }
      if (e.IsSelected)
      {
        e.Item.Checked = true;
        e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
      }
      else
      {
        e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Regular);
      }
    }

    private void rcHstryButton_Click(object sender, EventArgs e)
    {
      if (this.cstSplrListView.SelectedItems.Count <= 0)
      {
        cmnCde.showMsg("Please select a Record First!", 0);
        return;
      }
      cmnCde.showRecHstry(
        cmnCde.get_Gnrl_Rec_Hstry(long.Parse(
        this.cstSplrListView.SelectedItems[0].SubItems[2].Text),
        "scm.scm_cstmr_suplr", "cust_sup_id"), 9);
    }

    private void vwSQLButton_Click(object sender, EventArgs e)
    {
      cmnCde.showSQL(this.rec_SQL, 10);
    }

    private void delButton_Click(object sender, EventArgs e)
    {
      if (cmnCde.test_prmssns(this.dfltPrvldgs[93]) == false)
      {
        cmnCde.showMsg("You don't have permission to perform" +
         " this action!\nContact your System Administrator!", 0);
        return;
      }

      if (this.idTextBox.Text == "" || this.idTextBox.Text == "-1")
      {
        cmnCde.showMsg("Please select the Record to DELETE!", 0);
        return;
      }
      if (this.isCstSplrInUse(int.Parse(this.idTextBox.Text)) == true)
      {
        cmnCde.showMsg("This Record is in Use!", 0);
        return;
      }
      if (cmnCde.showMsg("Are you sure you want to DELETE the selected Record?" +
   "\r\nThis action cannot be undone!", 1) == DialogResult.No)
      {
        //cmnCde.showMsg("Operation Cancelled!", 4);
        return;
      }
      cmnCde.deleteGnrlRecs(long.Parse(this.idTextBox.Text),
        "Customer/Supplier Name=" + this.nameTextBox.Text, "scm.scm_cstmr_suplr", "cust_sup_id");
      cmnCde.deleteGnrlRecs(long.Parse(this.idTextBox.Text),
        "Customer/Supplier Name=" + this.nameTextBox.Text, "scm.scm_cstmr_suplr_sites", "cust_supplier_id");
      this.loadPanel();
    }

    bool errorOcrd = false;
    private void saveButton_Click(object sender, EventArgs e)
    {
      //try
      //{
      if (this.addRec == true)
      {
        if (cmnCde.test_prmssns(this.dfltPrvldgs[91]) == false)
        {
          cmnCde.showMsg("You don't have permission to perform" +
           " this action!\nContact your System Administrator!", 0);
          return;
        }
        //this.saveDtButton.Enabled = true;
      }
      else
      {
        if (cmnCde.test_prmssns(this.dfltPrvldgs[92]) == false)
        {
          cmnCde.showMsg("You don't have permission to perform" +
           " this action!\nContact your System Administrator!", 0);
          return;
        }
      }
      this.errorOcrd = false;

      if (!this.checkRqrmnts())
      {
        this.errorOcrd = true;
        return;
      }
      if (!this.checkDtRqrmnts(int.Parse(this.idTextBox.Text)))
      {
        this.errorOcrd = true;
        return;
      }
      int llbltyAccID = -1;
      int rcvblAccID = -1;

      if (this.typeComboBox.Text.Contains("Customer"))
      {
        llbltyAccID = this.get_DfltSalesLbltyAcnt(cmnCde.Org_id);
        rcvblAccID = this.get_DfltRcvblAcnt(cmnCde.Org_id);
      }
      else
      {
        llbltyAccID = this.get_DfltPyblAcnt(cmnCde.Org_id);
        rcvblAccID = this.get_DfltRcptRcvblAcnt(cmnCde.Org_id);
      }
      if (this.addRec == true)
      {
        this.createCstSplrRec(cmnCde.Org_id, this.nameTextBox.Text,
          this.nameTextBox.Text, this.typeComboBox.Text, this.classfctnTextBox.Text,
          llbltyAccID, rcvblAccID,
          long.Parse(this.lnkdPrsnIDTextBox.Text), this.genderTextBox.Text, this.dobTextBox.Text);

        //this.saveButton.Enabled = false;
        this.addRec = false;
        this.editRec = true;
        this.editButton.Enabled = this.addRecsP;
        this.addButton.Enabled = this.editRecsP;

        //cmnCde.showMsg("Record Saved!", 3);
        this.idTextBox.Text = this.getCstmrSplrID(this.nameTextBox.Text, cmnCde.Org_id).ToString();
        System.Windows.Forms.Application.DoEvents();
        this.saveButton.Enabled = false;
        this.saveSiteButton_Click(this.saveButton, e);

        ListViewItem nwItem = new ListViewItem(new string[] {
    "New",
    this.nameTextBox.Text,
    this.idTextBox.Text,
    this.typeComboBox.Text});

        while (this.cstSplrListView.SelectedItems.Count > 0)
        {
          this.cstSplrListView.SelectedItems[0].Font = new Font("Tahoma", 8.25f, FontStyle.Regular);
          this.cstSplrListView.SelectedItems[0].Checked = false;
          this.cstSplrListView.SelectedItems[0].Selected = false;
        };

        nwItem.Selected = true;
        nwItem.Checked = true;
        this.cstSplrListView.Items.Insert(0, nwItem);
        //if (this.cstSplrListView.Items.Count > 0)
        //{
        //  this.cstSplrListView.Items[0].Selected = true;
        //  this.cstSplrListView.Items[0].Checked = true;
        //}
        //this.saveSiteButton_Click(this.saveButton, e);
        this.editButton.PerformClick();
        //this.addDtButton.PerformClick();

        //this.saveButton.Enabled = true;
        System.Windows.Forms.Application.DoEvents();
        //this.loadPanel();
      }
      else if (this.editRec == true)
      {
        this.updtCstSplrRec(int.Parse(this.idTextBox.Text), this.nameTextBox.Text,
          this.nameTextBox.Text, this.typeComboBox.Text, this.classfctnTextBox.Text,
          llbltyAccID, rcvblAccID,
          long.Parse(this.lnkdPrsnIDTextBox.Text), this.genderTextBox.Text, this.dobTextBox.Text);

        if (this.cstSplrListView.SelectedItems.Count > 0)
        {
          this.cstSplrListView.SelectedItems[0].SubItems[1].Text = this.nameTextBox.Text;
        }
        //this.saveButton.Enabled = false;
        this.saveSiteButton_Click(this.saveButton, e);

        //this.saveButton.Enabled = true;
      }
      //}
      //catch (Exception ex)
      //{
      //  cmnCde.showMsg(ex.Message, 4);
      //}
    }

    private void saveSiteButton_Click(object sender, EventArgs e)
    {
      this.errorOcrd = false;
      if (!this.checkDtRqrmnts(int.Parse(this.idTextBox.Text)))
      {
        this.errorOcrd = true;
        return;
      }
      if (this.addDtRec == true || (this.siteIDTextBox.Text == "" || this.siteIDTextBox.Text == "-1"))
      {
        this.createCstSplrSiteRec(int.Parse(this.idTextBox.Text), this.siteNameTextbox.Text,
          this.siteNameTextbox.Text, this.cntctPrsnTextBox.Text, this.cntctNosTextBox.Text,
          this.emailTextBox.Text, this.bnkNmTextBox.Text, this.brnchNmTextBox.Text,
          this.acntNumTextBox.Text, this.bllngAddrsTextBox.Text, this.shipAddrsTextBox.Text,
          -1, -1,
          this.swiftCodeTextBox.Text, this.ntnltyTextBox.Text, this.idTypeTextBox.Text,
          this.idNumTextBox.Text, this.dateIssuedTextBox.Text, this.expryDateTextBox.Text, this.otherInfoTextBox.Text);

        this.addDtRec = false;
        this.editDtRec = true;

        if (isClosing == false)
        {
          cmnCde.showMsg("Record Saved!", 3);
        }
        //System.Windows.Forms.Application.DoEvents();
        //this.populateSitesListVw(int.Parse(this.idTextBox.Text));
      }
      else if (this.editDtRec == true && (this.siteIDTextBox.Text != "" || this.siteIDTextBox.Text != "-1"))
      {
        this.updtCstSplrSiteRec(int.Parse(this.siteIDTextBox.Text), this.siteNameTextbox.Text,
          this.siteNameTextbox.Text, this.cntctPrsnTextBox.Text, this.cntctNosTextBox.Text,
          this.emailTextBox.Text, this.bnkNmTextBox.Text, this.brnchNmTextBox.Text,
          this.acntNumTextBox.Text, this.bllngAddrsTextBox.Text, this.shipAddrsTextBox.Text,
          -1, -1,
          this.swiftCodeTextBox.Text, this.ntnltyTextBox.Text, this.idTypeTextBox.Text,
          this.idNumTextBox.Text, this.dateIssuedTextBox.Text, this.expryDateTextBox.Text, this.otherInfoTextBox.Text);

        if (isClosing == false)
        {
          cmnCde.showMsg("Record Saved!", 3);
        }
      }
    }

    private bool checkRqrmnts()
    {
      if (this.nameTextBox.Text == "")
      {
        cmnCde.showMsg("Please enter a Customer/Supplier Name!", 0);
        return false;
      }

      long oldRecID = this.getCstmrSplrID(this.nameTextBox.Text,
          cmnCde.Org_id);
      if (oldRecID > 0
       && this.addRec == true)
      {
        cmnCde.showMsg("Customer/Supplier Name is already in use in this Organisation!", 0);
        return false;
      }
      if (oldRecID > 0
       && this.editRec == true
       && oldRecID.ToString() !=
       this.idTextBox.Text)
      {
        cmnCde.showMsg("New Customer/Supplier Name is already in use in this Organisation!", 0);
        return false;
      }

      if (this.typeComboBox.Text == "")
      {
        cmnCde.showMsg("Customer/Supplier Type cannot be empty!", 0);
        return false;
      }
      if (this.classfctnTextBox.Text == "")
      {
        cmnCde.showMsg("Customer/Supplier Classification cannot be empty!", 0);
        return false;
      }

      return true;
    }

    private bool checkDtRqrmnts(int cstmrID)
    {
      if (this.siteNameTextbox.Text == "")
      {
        cmnCde.showMsg("Please enter a Site Name!", 0);
        return false;
      }
      if (this.cntctPrsnTextBox.Text == "")
      {
        cmnCde.showMsg("Contact Person cannot be empty!", 0);
        return false;
      }
      //if (this.cntctNosTextBox.Text == "")
      //{
      //  cmnCde.showMsg("Contact Numbers cannot be empty!", 0);
      //  return false;
      //}

      if (cstmrID <= 0)
      {
        //cmnCde.showMsg("Please Choose a Saved Customer/Supplier First!", 0);
        return true;
      }

      long oldRecID = this.getCstmrSplrSiteID(this.siteNameTextbox.Text,
          cstmrID);
      if (oldRecID > 0
       && this.addDtRec == true)
      {
        cmnCde.showMsg("Site Name is already in use by this Customer/Supplier!", 0);
        return false;
      }
      if (oldRecID > 0
       && this.editDtRec == true
       && oldRecID.ToString() !=
       this.siteIDTextBox.Text)
      {
        cmnCde.showMsg("New Site Name is already in use by this Customer/Supplier!", 0);
        return false;
      }

      //if (this.bllngAddrsTextBox.Text == "")
      //{
      // cmnCde.showMsg("Billing Address cannot be empty!", 0);
      // return false;
      //}
      //if (this.shipAddrsTextBox.Text == "")
      //{
      // cmnCde.showMsg("Shipping Address cannot be empty!", 0);
      // return false;
      //}
      return true;
    }

    private void addSiteButton_Click(object sender, EventArgs e)
    {
      //if (this.idTextBox.Text == "" || this.idTextBox.Text == "-1")
      //{
      // cmnCde.showMsg("Please select a saved Customer/Supplier First!", 0);
      // return;
      //}
      if (cmnCde.test_prmssns(this.dfltPrvldgs[91]) == false)
      {
        cmnCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      this.clearSiteDetInfo();
      this.addDtRec = true;
      this.editDtRec = false;
      this.prpareForSiteDetEdit();
      //this.addDtButton.Enabled = false;
      //this.editDtButton.Enabled = false;
    }

    private void editSiteButton_Click(object sender, EventArgs e)
    {
      if (this.editButton.Text == "EDIT")
      {
        if (cmnCde.test_prmssns(this.dfltPrvldgs[92]) == false)
        {
          cmnCde.showMsg("You don't have permission to perform" +
              " this action!\nContact your System Administrator!", 0);
          return;
        }
        //if (this.siteIDTextBox.Text == "" || this.siteIDTextBox.Text == "-1")
        //{
        // cmnCde.showMsg("No record to Edit!", 0);
        // return;
        //}
        this.addDtRec = false;
        this.editDtRec = true;
        this.prpareForSiteDetEdit();
        //this.addDtButton.Enabled = false;
        //this.editDtButton.Text = "STOP";
        //this.editMenuItem.Text = "STOP EDITING";
      }
      else
      {
        if (this.editButton.Text == "STOP")
        {
          this.editButton.PerformClick();
        }

        //this.saveDtButton.Enabled = false;
        this.addDtRec = false;
        this.editDtRec = false;
        //this.editDtButton.Enabled = this.addRecsP;
        //this.addDtButton.Enabled = this.editRecsP;
        //this.editDtButton.Text = "EDIT";
        //this.editMenuItem.Text = "Edit Item";
        this.disableSiteDetEdit();
        System.Windows.Forms.Application.DoEvents();

        this.populateSitesListVw(int.Parse(this.idTextBox.Text));
      }
    }

    private void editButton_Click(object sender, EventArgs e)
    {
      if (this.editButton.Text == "EDIT")
      {
        if (cmnCde.test_prmssns(this.dfltPrvldgs[92]) == false)
        {
          cmnCde.showMsg("You don't have permission to perform" +
              " this action!\nContact your System Administrator!", 0);
          return;
        }
        if (this.idTextBox.Text == "" || this.idTextBox.Text == "-1")
        {
          cmnCde.showMsg("No record to Edit!", 0);
          return;
        }
        this.addRec = false;
        this.editRec = true;
        this.prpareForDetEdit();
        //this.addButton.Enabled = false;
        this.editSiteButton_Click(sender, e);
        this.editButton.Text = "STOP";
        //this.editMenuItem.Text = "STOP EDITING";
        this.nameTextBox.Focus();
        this.nameTextBox.SelectAll();
      }
      else
      {
        this.saveButton.Enabled = false;
        this.addRec = false;
        this.editRec = false;
        this.editButton.Enabled = this.addRecsP;
        this.addButton.Enabled = this.editRecsP;
        this.disableDetEdit();
        this.editSiteButton_Click(sender, e);
        this.editButton.Text = "EDIT";
        //this.editMenuItem.Text = "Edit Item";
        System.Windows.Forms.Application.DoEvents();

        this.loadPanel();
      }
    }

    private void addButton_Click(object sender, EventArgs e)
    {
      if (cmnCde.test_prmssns(this.dfltPrvldgs[91]) == false)
      {
        cmnCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      this.clearDetInfo();
      this.clearSiteDetInfo();
      //this.siteListView.Items.Clear();

      this.addRec = true;
      this.editRec = false;
      this.prpareForDetEdit();
      this.addButton.Enabled = false;
      this.editButton.Enabled = false;
      this.addSiteButton_Click(sender, e);
      this.typeComboBox.SelectedIndex = 0;
      this.nameTextBox.Focus();
      this.nameTextBox.SelectAll();
    }

    private void go1Button_Click(object sender, EventArgs e)
    {
      this.loadPanel();
    }

    private void clearSiteButton_Click(object sender, EventArgs e)
    {
      this.siteIDTextBox.Text = "-1";
      this.siteNameTextbox.Text = "";
      this.populateSiteDet(-123456);
      if (this.saveButton.Enabled == true)
      {
        this.addSiteButton_Click(this.addButton, e);
      }
    }

    private void cstSpplrDiag_KeyDown(object sender, KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();

      if (e.Control && e.KeyCode == Keys.S)
      {
        if (this.saveButton.Enabled == true)
        {
          this.saveButton_Click(this.saveButton, ex);
        }
        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else if (e.Control && e.KeyCode == Keys.N)
      {
        if (this.addButton.Enabled == true)
        {
          this.addButton_Click(this.addButton, ex);
        }
        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else if (e.Control && e.KeyCode == Keys.E)
      {
        if (this.editButton.Enabled == true)
        {
          this.editButton_Click(this.editButton, ex);
        }
        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else if (e.Control && e.KeyCode == Keys.R)
      {
        this.resetTrnsButton.PerformClick();
      }
      else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)
      {
        if (this.go1Button.Enabled == true)
        {
          this.go1Button_Click(this.go1Button, ex);
        }
        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else if (e.Control && e.KeyCode == Keys.Delete)
      {
        if (this.delButton.Enabled == true)
        {
          this.delButton_Click(this.delButton, ex);
        }
        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else
      {
        e.Handled = false;
        e.SuppressKeyPress = false;
        if (this.cstSplrListView.Focused)
        {
          if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
          {
            this.okButton_Click(this.okButton, e);
          }
          else
          {
            cmnCde.listViewKeyDown(this.cstSplrListView, e);
          }
        }

      }
    }

    private void resetTrnsButton_Click(object sender, EventArgs e)
    {
      this.searchInComboBox.SelectedIndex = 0;
      this.searchForTextBox.Text = "%";
      this.dsplySizeComboBox.Text = cmnCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      this.disableDetEdit();
      this.disableSiteDetEdit();
      this.rec_cur_indx = 0;
      this.go1Button_Click(this.go1Button, e);
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      if (this.isReadOnly == true)
      {
        cmnCde.showMsg("Must be ADD/EDIT mode First!", 0);
        return;
      }
      if (this.saveButton.Enabled == true)
      {
        isClosing = true;
        this.saveButton.PerformClick();
        isClosing = false;
      }
      if (this.errorOcrd == true)
      {
        return;
      }
      if (this.cstSplrListView.CheckedItems.Count > 0)
      {
        //this.idTextBox.Text = this.cstSplrListView.CheckedItems[0].SubItems[2].Text;
      }
      else
      {
        this.idTextBox.Text = "-1";
        this.siteIDTextBox.Text = "-1";
      }
      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void cstSplrListView_DoubleClick(object sender, EventArgs e)
    {
      this.cstSplrListView.SelectedItems[0].Checked = true;
      if (this.isReadOnly)
      {
        return;
      }
      this.okButton_Click(this.okButton, e);
    }

    private void cstSplrListView_ItemChecked(object sender, ItemCheckedEventArgs e)
    {
      if (this.shdObeyEvts() == false)
      {
        return;
      }
      if (e != null)
      {
        this.selItemTxt = "";
        if (e.Item.Checked == true)
        {
          this.selItemTxt = e.Item.Text;
          e.Item.Selected = true;
        }
      }
      this.uncheckAllBtOne();
    }

    private void uncheckAllBtOne()
    {
      this.obey_evnts = false;
      for (int i = 0; i < this.cstSplrListView.Items.Count; i++)
      {
        if (this.cstSplrListView.Items[i].Text != this.selItemTxt)
        {
          this.cstSplrListView.Items[i].Checked = false;
        }
      }
      this.obey_evnts = true;
    }

    private void addMenuItem_Click(object sender, EventArgs e)
    {
      this.addButton.PerformClick();
    }

    private void editMenuItem_Click(object sender, EventArgs e)
    {
      this.editButton.PerformClick();
    }

    private void delMenuItem_Click(object sender, EventArgs e)
    {
      this.delButton.PerformClick();
    }

    private void exptExMenuItem_Click(object sender, EventArgs e)
    {
      cmnCde.exprtToExcel(this.cstSplrListView);
    }

    private void rfrshMenuItem_Click(object sender, EventArgs e)
    {
      this.go1Button.PerformClick();
    }

    private void vwSQLMenuItem_Click(object sender, EventArgs e)
    {
      this.vwSQLButton.PerformClick();
    }

    private void rcHstryMenuItem_Click(object sender, EventArgs e)
    {
      this.rcHstryButton.PerformClick();
    }

    private void searchForTextBox_Click(object sender, EventArgs e)
    {
      this.searchForTextBox.SelectAll();
    }

    private void searchInComboBox_KeyDown(object sender, KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
      {
        this.go1Button_Click(this.go1Button, ex);
      }
    }

    private void lnkdPersonButton_Click(object sender, EventArgs e)
    {
      if (this.addRec == false && this.editRec == false)
      {
        cmnCde.showMsg("Must be in ADD/EDIT mode First!", 0);
        return;
      }
      string lovNm = "Unlinked Persons (Customers/Suppliers)";
      string[] selVals = new string[1];
      selVals[0] = this.lnkdPrsnIDTextBox.Text;
      DialogResult dgRes = cmnCde.showPssblValDiag(
          cmnCde.getLovID(lovNm), ref selVals,
          true, false, cmnCde.Org_id);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          long prsnID = cmnCde.getPrsnID(selVals[i]);
          this.lnkdPersonNoTextBox.Text = cmnCde.getPrsnSurNameFrst(prsnID)
          + " (" + cmnCde.getPrsnLocID(prsnID) + ")";
          this.nameTextBox.Text = this.lnkdPersonNoTextBox.Text;
          //this.descTextBox.Text = this.lnkdPersonNoTextBox.Text;
          this.genderTextBox.Text = cmnCde.getGnrlRecNm("prs.prsn_names_nos",
            "person_id", "gender", prsnID);

          this.dobTextBox.Text = cmnCde.getGnrlRecNm("prs.prsn_names_nos",
  "person_id", "to_char(to_timestamp(date_of_birth,'YYYY-MM-DD'),'DD-Mon-YYYY')", prsnID);

          this.lnkdPrsnIDTextBox.Text = prsnID.ToString();
          if (this.siteIDTextBox.Text == "-1"
            || this.siteIDTextBox.Text == "")
          {
            this.cntctPrsnTextBox.Text = this.nameTextBox.Text;
            //long prsnID = long.Parse(this.lnkdPrsnIDTextBox.Text);
            if (prsnID > 0)
            {
              this.cntctNosTextBox.Text = cmnCde.getGnrlRecNm("prs.prsn_names_nos",
          "person_id", "cntct_no_mobl", prsnID);//
              this.emailTextBox.Text = cmnCde.getGnrlRecNm("prs.prsn_names_nos",
          "person_id", "email", prsnID);//email  res_address
              this.siteNameTextbox.Text = cmnCde.getOrgName(cmnCde.Org_id);
              //this.siteDescTextBox.Text = this.siteNmTextBox.Text;
              this.bllngAddrsTextBox.Text = cmnCde.getGnrlRecNm("prs.prsn_names_nos",
          "person_id", "pstl_addrs", prsnID);
              this.shipAddrsTextBox.Text = cmnCde.getGnrlRecNm("prs.prsn_names_nos",
      "person_id", "res_address", prsnID);
              this.ntnltyTextBox.Text = cmnCde.getGnrlRecNm("prs.prsn_names_nos",
"person_id", "nationality", prsnID);

            }

          }
        }
      }
    }

    private void nameTextBox_Leave(object sender, EventArgs e)
    {
      if (this.cntctPrsnTextBox.Text == "")
      {
        this.cntctPrsnTextBox.Text = this.nameTextBox.Text;
      }
      if (this.siteNameTextbox.Text == "")
      {
        this.siteNameTextbox.Text = "Unknown";
      }
      if (this.classfctnTextBox.Text == "")
      {
        this.classfctnTextBox.Text = "Individual";
      }
      if (this.cntctNosTextBox.Text == "")
      {
        this.cntctNosTextBox.Text = "Unknown";
      }
    }
  }
}
