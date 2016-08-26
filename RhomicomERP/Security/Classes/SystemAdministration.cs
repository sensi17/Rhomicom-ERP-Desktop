using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
//using RhoInterface;
using WeifenLuo.WinFormsUI.Docking;
using SystemAdministration.Forms;
using System.Windows.Forms;

namespace SystemAdministration.Classes
{
  public class SystemAdministration //: RhoModule
  {
    public SystemAdministration()
    {
    }

    //RhoModuleHost myHost = null;
    int putUnder = 1;
    String myName = "System Administration";
    string myDesc = "This module helps you to administer all the security features of this software!";
    string audit_tbl_name = "sec.sec_audit_trail_tbl";

    //WeifenLuo.WinFormsUI.Docking.DockContent myMainInterface = new mainForm();
    String vwroleName = "View System Administration";
    Int64 usr_id = -1;
    int[] role_st_id = new int[0];
    Int64 lgn_num = -1;
    int Og_id = -1;

    public int org_id
    {
      get { return Og_id; }
      set { Og_id = value; }
    }

    public Int64 user_id
    {
      get { return usr_id; }
      set { usr_id = value; }
    }


    public Int64 login_number
    {
      get { return lgn_num; }
      set { lgn_num = value; }
    }

    public int[] role_set_id
    {
      get { return role_st_id; }
      set { role_st_id = value; }
    }

    public String vwPrmssnName
    {
      get { return vwroleName; }
    }

    public String mdl_description
    {
      get { return myDesc; }
    }

    public string name
    {
      get { return myName; }
    }

    public string full_audit_trail_tbl_name
    {
      get { return audit_tbl_name; }
    }
    //public RhoModuleHost Host
    //  {
    //  get { return myHost; }
    //  set { myHost = value; }
    //  }

    public int whereToPut
    {
      get { return putUnder; }
    }

    //public WeifenLuo.WinFormsUI.Docking.DockContent mainInterface
    //  {
    //  get { return myMainInterface; }
    //  }

    public void loadMyRolesNMsgtyps()
    {
      /* 1. Check if Module is registered already
       * 2. if not register it
       * 3. Check if all the required priviledges exist else Create them
       * 4. Check if all the sample role set here exist else Create it
       * 5. Check if this sample role set has ever been 
       * given the required priviledges else let them have it
       * 6. 
       */
      Global.refreshRqrdVrbls();
      Global.myNwMainFrm.cmmnCode.checkNAssignReqrmnts();
    }

    public void loadOtherMdlsRoles()
    {
      System.Windows.Forms.Application.DoEvents();
      this.loadAccntngMdl();
      System.Windows.Forms.Application.DoEvents();
      this.loadPersonMdl();
      System.Windows.Forms.Application.DoEvents();
      this.loadGenStpMdl();
      System.Windows.Forms.Application.DoEvents();
      this.loadIntPymntsMdl();
      System.Windows.Forms.Application.DoEvents();
      this.loadEvntsAttndncMdl();
      this.loadGenericMdl();
      System.Windows.Forms.Application.DoEvents();
      this.loadOrgStpMdl();
      System.Windows.Forms.Application.DoEvents();
      this.loadRptMdl();
      System.Windows.Forms.Application.DoEvents();
      this.loadInventoryMdl();
      System.Windows.Forms.Application.DoEvents();
      this.loadSelfServiceMdl();
      string updtSQL = @"UPDATE prs.prsn_names_nos 
        SET first_name='SYSTEM'
        WHERE local_id_no = 'RHO0002012'";
      Global.myNwMainFrm.cmmnCode.updateDataNoParams(updtSQL);
    }

    public void loadAccntngMdl()
    {
      //For Accounting
      string[] dfltPrvldgs = { "View Accounting","View Chart of Accounts", 
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
    /*91*/"Add Customers/Suppliers", "Edit Customers/Suppliers", "Delete Customers/Suppliers",
    /*94*/"Add Fixed Assets","Edit Fixed Assets","Delete Fixed Assets"};

      string[] subGrpNames = { "Chart of Accounts", "Fixed Assets", "Customers/Suppliers", "Fixed Assets PM Records" };//, "Accounting Transactions"
      string[] mainTableNames = { "accb.accb_chart_of_accnts", "accb.accb_fa_assets_rgstr", "scm.scm_cstmr_suplr", "accb.accb_fa_assets_pm_recs" };//, "accb.accb_trnsctn_details"
      string[] keyColumnNames = { "accnt_id", "asset_id", "cust_sup_id", "asset_pm_rec_id" };//, "transctn_id" 
      String myName = "Accounting";
      string myDesc = "This module helps you to manage your organization's Accounting!";
      string audit_tbl_name = "accb.accb_audit_trail_tbl";
      String smplRoleName = "Accounting Administrator";

      Global.myNwMainFrm.cmmnCode.DefaultPrvldgs = dfltPrvldgs;
      Global.myNwMainFrm.cmmnCode.SubGrpNames = subGrpNames;
      Global.myNwMainFrm.cmmnCode.MainTableNames = mainTableNames;
      Global.myNwMainFrm.cmmnCode.KeyColumnNames = keyColumnNames;

      Global.myNwMainFrm.cmmnCode.ModuleAdtTbl = audit_tbl_name;
      Global.myNwMainFrm.cmmnCode.ModuleDesc = myDesc;
      Global.myNwMainFrm.cmmnCode.ModuleName = myName;
      Global.myNwMainFrm.cmmnCode.SampleRole = smplRoleName;
      Global.myNwMainFrm.cmmnCode.Extra_Adt_Trl_Info = "";
      Global.myNwMainFrm.cmmnCode.checkNAssignReqrmnts();
      this.createAcctngRqrdLOVs();
      this.createAcctngRqrdLOVs1();

      Global.myNwMainFrm.changeOrg();
      int orgID = int.Parse(Global.myNwMainFrm.crntOrgIDTextBox.Text);
      if (orgID > 0)
      {
        Global.updtOrgAccntCurrID(orgID, Global.myNwMainFrm.cmmnCode.getOrgFuncCurID(orgID));
      }

    }

    public void createAcctngRqrdLOVs()
    {
      string[] sysLovs = { "Control Accounts", "Transactions not Allowed Days", 
                           "Transactions not Allowed Dates", "Account Transaction Templates",
                           "Currencies","Payment Document Templates","Payment Methods",
                           "Supplier Prepayments","Supplier Debit Memos","Supplier Standard Payments",
                           "Customer Prepayments","Customer Credit Memos","Customer Standard Payments",
                         "Transaction Amount Breakdown Parameters","Receivables Docs. with Prepayments Applied",
                         "Payables Docs. with Prepayments Applied","Unposted Batches"};
      string[] sysLovsDesc = { "Control Accounts", "Transactions not Allowed Days", 
                               "Transactions not Allowed Dates", "Account Transaction Templates",
                               "Currencies", "Payment Document Templates", "Payment Methods",
                               "Supplier Prepayments","Supplier Debit Memos","Supplier Standard Payments",
                           "Customer Prepayments","Customer Credit Memos","Customer Standard Payments",
                             "Transaction Amount Breakdown Parameters","Receivables Docs. with Prepayments Applied",
                         "Payables Docs. with Prepayments Applied","Unposted Batches"};
      string[] sysLovsDynQrys = { "select distinct trim(to_char(accnt_id,'999999999999999999999999999999')) a, accnt_num || '.' || accnt_name b, '' c, org_id d, accnt_type e, accnt_num f from accb.accb_chart_of_accnts where (has_sub_ledgers = '1' and is_enabled = '1') order by accnt_num",
                                "","",
                            @"SELECT distinct trim(to_char(z.template_id,'999999999999999999999999999999')) a, z.template_name b,'' c, z.org_id d, trim(to_char(w.user_id,'999999999999999999999999999999')) e
                            FROM accb.accb_trnsctn_templates_hdr z 
                            LEFT OUTER JOIN accb.accb_trnsctn_templates_usrs w
                            ON ((z.template_id=w.template_id) and (now() between to_timestamp(w.valid_start_date,'YYYY-MM-DD HH24:MI:SS')
                            AND to_timestamp(w.valid_end_date,'YYYY-MM-DD HH24:MI:SS')))
                            ORDER BY z.template_name","",
                            "select distinct trim(to_char(doc_tmplts_hdr_id,'999999999999999999999999999999')) a, doc_tmplt_name b, '' c, org_id d, doc_type e from accb.accb_doc_tmplts_hdr where (is_enabled = '1') order by doc_tmplt_name",
                            "select distinct trim(to_char(paymnt_mthd_id,'999999999999999999999999999999')) a, pymnt_mthd_name b, '' c, org_id d, supported_doc_type e from accb.accb_paymnt_mthds where (is_enabled = '1') order by pymnt_mthd_name",
                            "select distinct trim(to_char(pybls_invc_hdr_id,'999999999999999999999999999999')) a, pybls_invc_number ||' ('||(invoice_amount-invc_amnt_appld_elswhr)||')' b, '' c, org_id d, trim(to_char(supplier_id,'999999999999999999999999999999')) e, trim(to_char(invc_curr_id,'999999999999999999999999999999')) f, pybls_invc_hdr_id g from accb.accb_pybls_invc_hdr where (((pybls_invc_type = 'Supplier Advance Payment' and (invoice_amount-amnt_paid)<=0) or pybls_invc_type = 'Supplier Credit Memo (InDirect Refund)') and approval_status='Approved' and (invoice_amount-invc_amnt_appld_elswhr)>0) order by pybls_invc_hdr_id DESC",
                            "select distinct trim(to_char(pybls_invc_hdr_id,'999999999999999999999999999999')) a, pybls_invc_number b, '' c, org_id d, trim(to_char(supplier_id,'999999999999999999999999999999')) e, trim(to_char(invc_curr_id,'999999999999999999999999999999')) f, pybls_invc_hdr_id g from accb.accb_pybls_invc_hdr where ((pybls_invc_type = 'Supplier Debit Memo (InDirect Topup)') and approval_status='Approved' and (invoice_amount-invc_amnt_appld_elswhr)>0) order by pybls_invc_hdr_id DESC",
                            "select distinct trim(to_char(pybls_invc_hdr_id,'999999999999999999999999999999')) a, pybls_invc_number b, '' c, org_id d, trim(to_char(supplier_id,'999999999999999999999999999999')) e, trim(to_char(invc_curr_id,'999999999999999999999999999999')) f, pybls_invc_hdr_id g from accb.accb_pybls_invc_hdr where ((pybls_invc_type = 'Supplier Standard Payment') and approval_status='Approved' and (invoice_amount-amnt_paid)<=0) order by pybls_invc_hdr_id DESC",
                            "select distinct trim(to_char(rcvbls_invc_hdr_id,'999999999999999999999999999999')) a, rcvbls_invc_number ||' ('||(invoice_amount-invc_amnt_appld_elswhr)||')' b, '' c, org_id d, trim(to_char(customer_id,'999999999999999999999999999999')) e, trim(to_char(invc_curr_id,'999999999999999999999999999999')) f, rcvbls_invc_hdr_id g from accb.accb_rcvbls_invc_hdr where (((rcvbls_invc_type = 'Customer Advance Payment' and (invoice_amount-amnt_paid)<=0) or rcvbls_invc_type = 'Customer Debit Memo (InDirect Refund)') and approval_status='Approved' and (invoice_amount-invc_amnt_appld_elswhr)>0) order by rcvbls_invc_hdr_id DESC",
                            "select distinct trim(to_char(rcvbls_invc_hdr_id,'999999999999999999999999999999')) a, rcvbls_invc_number b, '' c, org_id d, trim(to_char(customer_id,'999999999999999999999999999999')) e, trim(to_char(invc_curr_id,'999999999999999999999999999999')) f, rcvbls_invc_hdr_id g from accb.accb_rcvbls_invc_hdr where ((rcvbls_invc_type = 'Customer Credit Memo (InDirect Topup)') and approval_status='Approved' and (invoice_amount-invc_amnt_appld_elswhr)>0) order by rcvbls_invc_hdr_id DESC",
                            "select distinct trim(to_char(rcvbls_invc_hdr_id,'999999999999999999999999999999')) a, rcvbls_invc_number b, '' c, org_id d, trim(to_char(customer_id,'999999999999999999999999999999')) e, trim(to_char(invc_curr_id,'999999999999999999999999999999')) f, rcvbls_invc_hdr_id g from accb.accb_rcvbls_invc_hdr where ((rcvbls_invc_type = 'Customer Standard Payment') and approval_status='Approved' and (invoice_amount-amnt_paid)<=0) order by rcvbls_invc_hdr_id DESC","",
                                "SELECT y.rcvbls_invc_number a, z.rcvbl_smmry_amnt || ' (' || y.approval_status || ')' b, '' c, 1 d, z.appld_prepymnt_doc_id||'' e, accb.get_src_doc_type(z.appld_prepymnt_doc_id,'Customer') f FROM accb.accb_rcvbls_invc_hdr y,accb.accb_rcvbl_amnt_smmrys z WHERE y.rcvbls_invc_hdr_id =z.src_rcvbl_hdr_id and z.appld_prepymnt_doc_id > 0 UNION Select accb.get_src_doc_num(w.src_doc_id, w.src_doc_typ) a, CASE WHEN (w.amount_paid>0 and w.change_or_balance <=0) or (w.amount_paid<0 and w.change_or_balance >=0) THEN Round(((w.amount_paid/abs(w.amount_paid))*w.amount_paid)-w.change_or_balance,2)|| ' (' || w.pymnt_vldty_status || ')' ELSE w.amount_paid || ' (' || w.pymnt_vldty_status || ')' END b, '' c, 1 d, w.prepay_doc_id||'' e, prepay_doc_type f FROM accb.accb_payments w WHERE w.prepay_doc_id>0 and prepay_doc_type ilike '%Customer%'",
                                "SELECT y.pybls_invc_number a, z.pybls_smmry_amnt || ' (' || y.approval_status || ')' b, '' c, 1 d, z.appld_prepymnt_doc_id||'' e, accb.get_src_doc_type(z.appld_prepymnt_doc_id,'Supplier') f FROM accb.accb_pybls_invc_hdr y,accb.accb_pybls_amnt_smmrys z WHERE y.pybls_invc_hdr_id =z.src_pybls_hdr_id and z.appld_prepymnt_doc_id > 0 UNION Select accb.get_src_doc_num(w.src_doc_id, w.src_doc_typ) a, CASE WHEN (w.amount_paid>0 and w.change_or_balance <=0) or (w.amount_paid<0 and w.change_or_balance >=0) THEN Round(((w.amount_paid/abs(w.amount_paid))*w.amount_paid)-w.change_or_balance,2)|| ' (' || w.pymnt_vldty_status || ')' ELSE w.amount_paid || ' (' || w.pymnt_vldty_status || ')' END b, '' c, 1 d, w.prepay_doc_id||'' e, prepay_doc_type f FROM accb.accb_payments w WHERE w.prepay_doc_id>0 and prepay_doc_type ilike '%Supplier%'",
                            @"SELECT distinct '' || z.batch_id a, z.batch_name b,'' c, z.org_id d, ''||z.last_update_by e, z.batch_status f, z.batch_id g 
                            FROM accb.accb_trnsctn_batches z 
                            ORDER BY z.batch_id DESC"};

      string[] pssblVals = {"2", "01-JAN-1901", "Sample Holiday Date Disallowed",
                           "2", "01-JAN-2014", "Sample Holiday Date Disallowed",
                           "1", "SUNDAY", "No Weekend Transactions",
                           "1", "SATURDAY", "No Weekend Transactions",
                           "4", "EUR", "European Euro",
                           "4", "CNY", "Chinese Yuan",
                           "4", "ZAR", "South African Rand",
                           "4", "XAF", "CFA Franc (BEAC)",
                           "4", "XOF", "CFA Franc (BCEAO)",
                           "4", "NGN", "Nigerian Naira",
                           "13","GHS 50","GHS 50",
                           "13","GHS 20","GHS 20",
                           "13","GHS 10","GHS 10",
                           "13","GHS 5","GHS 5",
                           "13","GHS 2","GHS 2",
                           "13","GHS 1","GHS 1",
                           "13","GHS 0.50","GHS 0.50",
                           "13","GHS 0.20","GHS 0.20",
                           "13","GHS 0.10","GHS 0.10",
                           "13","GHS 0.05","GHS 0.05",
                           "13","GHS 0.01","GHS 0.01"};

      Global.myNwMainFrm.cmmnCode.createSysLovs(sysLovs, sysLovsDynQrys, sysLovsDesc);
      Global.myNwMainFrm.cmmnCode.createSysLovsPssblVals(sysLovs, pssblVals);
      string[] prcsstyps = { "Trial Balance Report", "Profit and Loss Report", 
                             "Balance Sheet Report", "Subledger Balance Report", 
                             "Post GL Batch", "Open/Close Periods",
                             "Inventory Journal Import", "Internal Payments Journal Import" };
      for (int i = 1; i < 9; i++)
      {
        if (Global.getActnPrcssID(i.ToString()) <= 0)
        {
          Global.createActnPrcss(i, prcsstyps[i - 1]);
        }
        else
        {
          Global.updtActnPrcss(i, prcsstyps[i - 1]);
        }
      }
    }

    public void createAcctngRqrdLOVs1()
    {
      string[] sysLovs = { "Cash Accounts", "Inventory/Asset Accounts", "Contra Expense Accounts",
      "Contra Revenue Accounts","Customer Classifications","Supplier Classifications",
        "Tax Codes","Discount Codes", "Extra Charges", "Approved Requisitions",
        "Suppliers", "Customer/Supplier Sites","Users' Sales Stores","Approved Pro-Forma Invoices",
        /*14*/"Approved Sales Orders","Approved Internal Item Requests",
        /*16*/"Customers","Approved Sales Invoices/Item Issues", "Customer/Supplier Classifications",
        /*19*/"Unlinked Persons (Customers/Suppliers)", "All Accounts", 
        /*21*/"All Asset Accounts", "All Liability Accounts", "All Equity Accounts", "All Revenue Accounts", 
        /*25*/"All Expense Accounts", "All Memo Accounts","Asset Classifications", 
        /*28*/"Asset Categories","Asset Building Names","Asset Room Names", "Asset Numbers", 
        /*32*/"PM Measurement Types", "PM Measurement Units", "PM Actions Taken"};
      string[] sysLovsDesc = { "Cash Accounts", "Inventory/Asset Accounts", "Contra Expense Accounts",
      "Contra Revenue Accounts","Customer Classifications","Supplier Classifications",
        "Tax Codes","Discount Codes","Extra Charges","Approved Requisitions",
        "Suppliers", "Customer/Supplier Sites", "Users' Sales Stores","Approved Pro-Forma Invoices",
        "Approved Sales Orders","Approved Internal Item Requests",
        "Customers", "Approved Sales Invoices/Item Issues", "Simultaneous Customer/Supplier Classifications",
        "Persons not Linked as Customers/Suppliers", "All Accounts", 
        "All Asset Accounts", "All Liability Accounts", "All Equity Accounts", "All Revenue Accounts"
        , "All Expense Accounts", "All Memo Accounts", "Asset Classifications"
        , "Asset Categories", "Asset Building Names", "Asset Room Names", "Asset Numbers", 
        /*32*/"PM Measurement Types", "PM Measurement Units", "PM Actions Taken"};
      string[] sysLovsDynQrys = { "", "", 
        "select distinct trim(to_char(accnt_id,'999999999999999999999999999999')) a, accnt_name b, '' c, org_id d, accnt_num e from accb.accb_chart_of_accnts where (accnt_type = 'EX' and is_prnt_accnt = '0' and is_enabled = '1' and is_contra = '1') order by accnt_num", 
        "select distinct trim(to_char(accnt_id,'999999999999999999999999999999')) a, accnt_name b, '' c, org_id d, accnt_num e from accb.accb_chart_of_accnts where (accnt_type = 'R' and is_prnt_accnt = '0' and is_enabled = '1' and is_contra = '1') order by accnt_num", 
        "", "", 
        "select distinct trim(to_char(code_id,'999999999999999999999999999999')) a, code_name b, '' c, org_id d, is_parent e from scm.scm_tax_codes where (itm_type = 'Tax' and is_enabled = '1') order by code_name", 
        "select distinct trim(to_char(code_id,'999999999999999999999999999999')) a, code_name b, '' c, org_id d, is_parent e from scm.scm_tax_codes where (itm_type = 'Discount' and is_enabled = '1') order by code_name",
        "select distinct trim(to_char(code_id,'999999999999999999999999999999')) a, code_name b, '' c, org_id d, is_parent e from scm.scm_tax_codes where (itm_type = 'Extra Charge' and is_enabled = '1') order by code_name",
        "select distinct trim(to_char(y.prchs_doc_hdr_id,'999999999999999999999999999999')) a, y.purchase_doc_num b, '' c, y.org_id d, y.prchs_doc_hdr_id g " +
        "from scm.scm_prchs_docs_hdr y, scm.scm_prchs_docs_det z " +
        "where (y.purchase_doc_type = 'Purchase Requisition' " +
        "and y.approval_status = 'Approved' " +
        "and z.prchs_doc_hdr_id = y.prchs_doc_hdr_id and (z.quantity - z.rqstd_qty_ordrd)>0) order by y.prchs_doc_hdr_id DESC",
        "select distinct trim(to_char(cust_sup_id,'999999999999999999999999999999')) a, cust_sup_name b, '' c, org_id d from scm.scm_cstmr_suplr where (cust_or_sup ilike '%Supplier%' and is_enabled='1') order by 2",
        "select distinct trim(to_char(cust_sup_site_id,'999999999999999999999999999999')) a, site_name b, '' c, cust_supplier_id d from scm.scm_cstmr_suplr_sites where is_enabled='1' order by 2",
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
        "select distinct trim(to_char(cust_sup_id,'999999999999999999999999999999')) a, cust_sup_name b, '' c, org_id d from scm.scm_cstmr_suplr where (cust_or_sup ilike '%Customer%' and is_enabled='1') order by 2",
        "select distinct trim(to_char(y.invc_hdr_id,'999999999999999999999999999999')) a, y.invc_number b, '' c, y.org_id d, y.invc_hdr_id g " +
        "from scm.scm_sales_invc_hdr y, scm.scm_sales_invc_det z " +
        "where ((y.invc_type = 'Item Issue-Unbilled' or y.invc_type = 'Sales Invoice') " +
        "and (y.approval_status = 'Approved') " +
        "and (z.invc_hdr_id = y.invc_hdr_id) and ((z.doc_qty - z.qty_trnsctd_in_dest_doc)>0)) order by y.invc_hdr_id DESC",
        "",
  "SELECT distinct local_id_no a, trim(title || ' ' || sur_name || "+
		"', ' || first_name || ' ' || other_names) b, '' c, org_id d " +
		"FROM prs.prsn_names_nos a where a.person_id NOT IN (Select lnkd_prsn_id from scm.scm_cstmr_suplr where lnkd_prsn_id>0) order by local_id_no DESC",
    "select distinct trim(to_char(accnt_id,'999999999999999999999999999999')) a, (CASE WHEN prnt_accnt_id>0 THEN accnt_num || '.' || accnt_name || ' ('|| accb.get_accnt_num(prnt_accnt_id)||'.'||accb.get_accnt_name(prnt_accnt_id)|| ')' WHEN control_account_id>0 THEN accnt_num || '.' || accnt_name || ' ('|| accb.get_accnt_num(control_account_id)||'.'||accb.get_accnt_name(control_account_id)|| ')' ELSE accnt_num || '.' || accnt_name END) b, '' c, org_id d, accnt_num e from accb.accb_chart_of_accnts where (is_enabled = '1') order by accnt_num", 
        "select distinct trim(to_char(accnt_id,'999999999999999999999999999999')) a, accnt_name b, '' c, org_id d, accnt_num e from accb.accb_chart_of_accnts where (accnt_type = 'A') order by accnt_num", 
        "select distinct trim(to_char(accnt_id,'999999999999999999999999999999')) a, accnt_name b, '' c, org_id d, accnt_num e from accb.accb_chart_of_accnts where (accnt_type = 'L') order by accnt_num", 
        "select distinct trim(to_char(accnt_id,'999999999999999999999999999999')) a, accnt_name b, '' c, org_id d, accnt_num e from accb.accb_chart_of_accnts where (accnt_type = 'EQ') order by accnt_num", 
        "select distinct trim(to_char(accnt_id,'999999999999999999999999999999')) a, accnt_name b, '' c, org_id d, accnt_num e from accb.accb_chart_of_accnts where (accnt_type = 'R') order by accnt_num", 
        "select distinct trim(to_char(accnt_id,'999999999999999999999999999999')) a, accnt_name b, '' c, org_id d, accnt_num e from accb.accb_chart_of_accnts where (accnt_type = 'EX') order by accnt_num", 
        "select distinct trim(to_char(memo_accnt_id,'999999999999999999999999999999')) a, memo_accnt_name b, '' c, org_id d, memo_accnt_num e from accb.accb_memo_accounts where (is_enabled = '1') order by memo_accnt_num",
        "","","","",
        "select distinct '' || asset_id a, trim(asset_code_name || ' ' || REPLACE(asset_desc, asset_code_name, '')) b, '' c, org_id d from accb.accb_fa_assets_rgstr order by 2",
        "","",""
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
       ,"5", "Training Provider", "Training Provider"
       ,"18", "Customer/Service Provider Organisation", "Customer/Service Provider Organisation"
       ,"18", "Customer/Service Provider Individual", "Customer/Service Provider Individual"
       ,"18", "Customer/Provider of Goods & Services", "Customer/Provider of Goods & Services"
       ,"27", "Major Fixed Asset", "Major Fixed Asset"
       ,"27", "Minor Fixed Asset", "Minor Fixed Asset"
       ,"27", "Financial Instrument", "Financial Instrument"
       ,"27", "Other Investment", "Other Investment"
       ,"28", "Computers", "Computers"
       ,"28", "Computers Accessories", "Computers Accessories"
       ,"28", "Office Equipment", "Office Equipment"
       ,"28", "Plant & Other Equipment", "Plant & Other Equipment"
       ,"28", "Land", "Land"
       ,"28", "Building", "Building"
       ,"28", "Treasury Bill Investment", "Treasury Bill Investment"
       ,"28", "Fixed Deposit Investment", "Fixed Deposit Investment"
       ,"29", "Head Office Building", "Head Office Building"
       ,"30", "Ground Floor Room 1", "Ground Floor Room 1"
       ,"32", "Mileage", "Distance Covered"
       ,"32", "Hours Worked", "Hours being turned on"
       ,"32", "Oil Level", "Oil Level"
       ,"33", "KM", "Kilometers"
       ,"33", "Hours", "Hours"
       ,"33", "Miles", "Miles"
       ,"33", "m", "meters"
       ,"33", "Percent", "Percent"
       ,"34", "General Servicing", "General Servicing"
       ,"34", "Oil Change", "Oil Change"
       ,"34", "General Cleaning", "General Cleaning"};

      Global.myNwMainFrm.cmmnCode.createSysLovs(sysLovs, sysLovsDynQrys, sysLovsDesc);
      Global.myNwMainFrm.cmmnCode.createSysLovsPssblVals(sysLovs, pssblVals);
    }

    public void loadInventoryMdl()
    {
      //For Accounting
      string[] dfltPrvldgs = { "View Inventory Manager", 
        /*1*/  "View Item List", "View Product Categories", "View Stores/Warehouses"
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


      string[] subGrpNames = new string[0];
      string[] mainTableNames = new string[0];
      string[] keyColumnNames = new string[0];
      String myName = "Stores And Inventory Manager";
      string myDesc = "This module helps you to manage your organization's Inventory System!";
      string audit_tbl_name = "inv.inv_audit_trail_tbl";

      String smplRoleName = "Stores And Inventory Manager Administrator";

      Global.myNwMainFrm.cmmnCode.DefaultPrvldgs = dfltPrvldgs;
      Global.myNwMainFrm.cmmnCode.SubGrpNames = subGrpNames;
      Global.myNwMainFrm.cmmnCode.MainTableNames = mainTableNames;
      Global.myNwMainFrm.cmmnCode.KeyColumnNames = keyColumnNames;

      Global.myNwMainFrm.cmmnCode.ModuleAdtTbl = audit_tbl_name;
      Global.myNwMainFrm.cmmnCode.ModuleDesc = myDesc;
      Global.myNwMainFrm.cmmnCode.ModuleName = myName;
      Global.myNwMainFrm.cmmnCode.SampleRole = smplRoleName;
      Global.myNwMainFrm.cmmnCode.Extra_Adt_Trl_Info = "";
      Global.myNwMainFrm.cmmnCode.checkNAssignReqrmnts();
      this.createInvntryRqrdLOVs();
      if (Global.myNwMainFrm.cmmnCode.Org_id > 0)
      {
        long rowid = Global.myNwMainFrm.cmmnCode.getGnrlRecID("scm.scm_dflt_accnts", "rho_name",
  "row_id", "Default Accounts", Global.myNwMainFrm.cmmnCode.Org_id);
        if (rowid <= 0)
        {
          Global.createDfltAcnts(Global.myNwMainFrm.cmmnCode.Org_id);
        }
        long pymntID = Global.myNwMainFrm.cmmnCode.getGnrlRecID("accb.accb_paymnt_mthds", "pymnt_mthd_name",
  "paymnt_mthd_id", "Customer Cash", Global.myNwMainFrm.cmmnCode.Org_id);

        Global.updtOrgInvoiceCurrID(Global.myNwMainFrm.cmmnCode.Org_id,
          Global.myNwMainFrm.cmmnCode.getOrgFuncCurID(Global.myNwMainFrm.cmmnCode.Org_id),
          pymntID);
      }
      Global.updateOrgnlSellingPrice();
      Global.updateUOMPrices();
    }

    public void createInvntryRqrdLOVs()
    {
      string[] sysLovs = { "Cash Accounts", "Inventory/Asset Accounts", "Contra Expense Accounts",
      "Contra Revenue Accounts","Customer Classifications","Supplier Classifications",
        "Tax Codes","Discount Codes", "Extra Charges", "Approved Requisitions",
        "Suppliers", "Customer/Supplier Sites","Users' Sales Stores","Approved Pro-Forma Invoices",
        "Approved Sales Orders","Approved Internal Item Requests",
        "Customers","Approved Sales Invoices/Item Issues", "Customer Names for Reports",
        /*19*/"Supplier Names for Reports", "Allow Dues on Invoices", "All Customers and Suppliers",
        /*22*/"Production Process Runs", "Production Process Run Stages","Production Process Classifications",
        /*25*/"Default POS Paper Size","Default Document Notes", "Document Custom Print Process Names",
        /*28*/"All Sales Documents","Production Cost Explanations",
        /*30*/"All Receivables Documents", "All Payables Documents"};
      string[] sysLovsDesc = {"Cash Accounts", "Inventory/Asset Accounts", "Contra Expense Accounts",
      "Contra Revenue Accounts","Customer Classifications","Supplier Classifications",
        "Tax Codes","Discount Codes","Extra Charges","Approved Requisitions",
        "Suppliers", "Customer/Supplier Sites", "Users' Sales Stores","Approved Pro-Forma Invoices",
        "Approved Sales Orders","Approved Internal Item Requests",
        "Customers", "Approved Sales Invoices/Item Issues", "Customer Names for Reports",
        /*19*/"Supplier Names for Reports", "Allow Dues on Invoices", "All Customers and Suppliers",
                         "Production Process Runs",
                         "Production Process Run Stages","Production Process Classifications",
                         "Default POS Paper Size","Default Document Notes",
                         "Document Custom Print Process Names",
        /*28*/"All Sales Documents","Production Cost Explanations",
        /*30*/"All Receivables Documents", "All Payables Documents"};
      string[] sysLovsDynQrys = { "", "", 
        "select distinct trim(to_char(accnt_id,'999999999999999999999999999999')) a, accnt_name b, '' c, org_id d, accnt_num e from accb.accb_chart_of_accnts where (accnt_type = 'EX' and is_prnt_accnt = '0' and is_enabled = '1' and is_contra = '1') order by accnt_num", 
        "select distinct trim(to_char(accnt_id,'999999999999999999999999999999')) a, accnt_name b, '' c, org_id d, accnt_num e from accb.accb_chart_of_accnts where (accnt_type = 'R' and is_prnt_accnt = '0' and is_enabled = '1' and is_contra = '1') order by accnt_num", 
        "", "", 
        "select distinct trim(to_char(code_id,'999999999999999999999999999999')) a, code_name b, '' c, org_id d, is_parent e from scm.scm_tax_codes where (itm_type = 'Tax' and is_enabled = '1') order by code_name", 
        "select distinct trim(to_char(code_id,'999999999999999999999999999999')) a, code_name b, '' c, org_id d, is_parent e from scm.scm_tax_codes where (itm_type = 'Discount' and is_enabled = '1') order by code_name",
        "select distinct trim(to_char(code_id,'999999999999999999999999999999')) a, code_name b, '' c, org_id d, is_parent e from scm.scm_tax_codes where (itm_type = 'Extra Charge' and is_enabled = '1') order by code_name",
        "select distinct trim(to_char(y.prchs_doc_hdr_id,'999999999999999999999999999999')) a, y.purchase_doc_num b, '' c, y.org_id d, y.prchs_doc_hdr_id g " +
        "from scm.scm_prchs_docs_hdr y, scm.scm_prchs_docs_det z " +
        "where (y.purchase_doc_type = 'Purchase Requisition' " +
        "and y.approval_status = 'Approved' " +
        "and z.prchs_doc_hdr_id = y.prchs_doc_hdr_id and (z.quantity - z.rqstd_qty_ordrd)>0) order by y.prchs_doc_hdr_id DESC",
        "select distinct trim(to_char(cust_sup_id,'999999999999999999999999999999')) a, cust_sup_name b, '' c, org_id d from scm.scm_cstmr_suplr where (cust_or_sup ilike '%Supplier%' and is_enabled='1') order by 2",
        "select distinct trim(to_char(cust_sup_site_id,'999999999999999999999999999999')) a, site_name b, '' c, cust_supplier_id d from scm.scm_cstmr_suplr_sites where (is_enabled='1') order by 2",
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
        "select distinct trim(to_char(cust_sup_id,'999999999999999999999999999999')) a, cust_sup_name b, '' c, org_id d from scm.scm_cstmr_suplr where (cust_or_sup ilike '%Customer%' and is_enabled='1') order by 2",
        "select distinct trim(to_char(y.invc_hdr_id,'999999999999999999999999999999')) a, y.invc_number b, '' c, y.org_id d, y.invc_hdr_id g " +
        "from scm.scm_sales_invc_hdr y, scm.scm_sales_invc_det z " +
        "where ((y.invc_type = 'Item Issue-Unbilled' or y.invc_type = 'Sales Invoice') " +
        "and (y.approval_status = 'Approved') " +
        "and (z.invc_hdr_id = y.invc_hdr_id) and ((z.doc_qty - z.qty_trnsctd_in_dest_doc)>0)) order by y.invc_hdr_id DESC",
        "select distinct cust_sup_name a, cust_sup_name b, '' c, org_id d from scm.scm_cstmr_suplr where (cust_or_sup ilike '%Customer%' and is_enabled='1') order by 2",
        "select distinct cust_sup_name a, cust_sup_name b, '' c, org_id d from scm.scm_cstmr_suplr where (cust_or_sup ilike '%Supplier%' and is_enabled='1') order by 2",
        "",
        "select distinct trim(to_char(cust_sup_id,'999999999999999999999999999999')) a, cust_sup_name b, '' c, org_id d, lnkd_prsn_id e from scm.scm_cstmr_suplr where (is_enabled='1') order by 2",
        "select distinct '' || y.process_run_id a, y.batch_code_num b, '' c, z.org_id d, y.process_def_id e from scm.scm_process_run y, scm.scm_process_definition z where (z.process_def_id = y.process_def_id) order by 2",
        "select distinct z.stage_code_name a, z.stage_code_desc b, '' c, -1 d, y.process_run_id e from scm.scm_process_run_stages y, scm.scm_process_def_stages z where (z.stage_id = y.def_stage_id and y.process_run_id>0) order by 2",
        "","","","",
        "select distinct ''||y.invc_hdr_id a, y.invc_number b, '' c, y.org_id d, y.invc_hdr_id g " +
        "from scm.scm_sales_invc_hdr y " +
        "where (1=1) order by y.invc_hdr_id DESC","",
        "select distinct ''||y.rcvbls_invc_hdr_id a, y.rcvbls_invc_number b, '' c, y.org_id d, y.rcvbls_invc_hdr_id g " +
        "from accb.accb_rcvbls_invc_hdr y " +
        "where (1=1) order by y.rcvbls_invc_hdr_id DESC",
        "select distinct ''||y.pybls_invc_hdr_id a, y.pybls_invc_number b, '' c, y.org_id d, y.pybls_invc_hdr_id g " +
        "from accb.accb_pybls_invc_hdr y " +
        "where (1=1) order by y.pybls_invc_hdr_id DESC"
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
       ,"5", "Training Provider", "Training Provider"
       ,"20", "NO", "Allow Internal Payments on Invoices"
       ,"24", "Category 1", "Category 1 Production Process"
       ,"24", "Category 2", "Category 2 Production Process"
       ,"24", "Category 3", "Category 3 Production Process"
       ,"25", "80mm", "Large Width POS Paper"
       ,"25", "58mm", "Small Width POS Paper"
       ,"26", "Sales Invoice", ""
       ,"26", "Sales Invoice - Dues", ""
       ,"26", "Receivables Invoice", ""
       ,"26", "Internal Item Request",""
       ,"26", "Item Issues", ""
       ,"26", "Payables Invoice", ""
       ,"26", "Restaurant Invoice", ""
       ,"26", "Check-Ins Invoice", ""
       ,"26", "Appointments Invoice", ""
       ,"26", "Events Invoice", ""
       ,"27", "Sales Invoice", "Sales Invoice"
       ,"27", "Sales Invoice - Dues", "Sales Invoice - No Qty & Unit Price"
       ,"27", "Receivables Invoice", "Receivables Invoice"
       ,"27", "Item Issues", "Item Issues-Unbilled"
       ,"27", "Internal Item Request","Item Issues-Unbilled"
       ,"27", "Payables Invoice", "Payables Invoice"
       ,"27", "Restaurant Invoice", "Sales Invoice"
       ,"27", "Check-Ins Invoice", "Sales Invoice"
       ,"27", "Appointments Invoice", "Sales Invoice"
       ,"27", "Events Invoice", "Sales Invoice"
       ,"27", "Pay Slip", "Customized Pay Slip (Sample 1)"
       ,"29", "Labour Costs", "Labour Costs"
       ,"29", "Rental Costs", "Rental Costs"
       ,"29", "Utility Costs", "Utility Costs"};

      Global.myNwMainFrm.cmmnCode.createSysLovs(sysLovs, sysLovsDynQrys, sysLovsDesc);
      Global.myNwMainFrm.cmmnCode.createSysLovsPssblVals(sysLovs, pssblVals);

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
        "select distinct trim(to_char(code_id,'999999999999999999999999999999')) a, code_name b, '' c, org_id d, is_parent e from scm.scm_tax_codes where (itm_type = 'Tax' and is_enabled = '1') order by code_name", 
        "select distinct trim(to_char(code_id,'999999999999999999999999999999')) a, code_name b, '' c, org_id d, is_parent e from scm.scm_tax_codes where (itm_type = 'Discount' and is_enabled = '1') order by code_name",
        "select distinct trim(to_char(code_id,'999999999999999999999999999999')) a, code_name b, '' c, org_id d, is_parent e from scm.scm_tax_codes where (itm_type = 'Extra Charge' and is_enabled = '1') order by code_name",
        "select distinct trim(to_char(prchs_doc_hdr_id,'999999999999999999999999999999')) a, purchase_doc_num b, '' c, org_id d from scm.scm_prchs_docs_hdr where (purchase_doc_type = 'Purchase Requisition' and approval_status = 'Approved') order by purchase_doc_num DESC",
        "select distinct trim(to_char(cust_sup_id,'999999999999999999999999999999')) a, cust_sup_name b, '' c, org_id d from scm.scm_cstmr_suplr where (cust_or_sup ilike '%Supplier%' and is_enabled='1') order by 2",
        "select distinct trim(to_char(cust_sup_site_id,'999999999999999999999999999999')) a, site_name b, '' c, cust_supplier_id d from scm.scm_cstmr_suplr_sites where is_enabled='1' order by 2",
          "",
          "select distinct trim(to_char(cat_id,'999999999999999999999999999999')) a, cat_name b, '' c, org_id d from inv.inv_product_categories where (enabled_flag = '1') order by cat_name",
          "select distinct trim(to_char(subinv_id,'999999999999999999999999999999')) a, subinv_name b, '' c, org_id d from inv.inv_itm_subinventories where (enabled_flag = '1') order by subinv_name",
          "select distinct trim(to_char(item_type_id,'999999999999999999999999999999')) a, item_type_name b, '' c, org_id d from inv.inv_itm_type_templates where (is_tmplt_enabled_flag = '1') order by item_type_name",
          "select distinct trim(to_char(prchs_doc_hdr_id,'999999999999999999999999999999')) a, purchase_doc_num b, '' c, org_id d, purchase_doc_type e, po_rec_status f from scm.scm_prchs_docs_hdr where approval_status = 'Approved' order by purchase_doc_num",
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

      Global.myNwMainFrm.cmmnCode.createSysLovs(sysLovs1, sysLovsDynQrys1, sysLovsDesc1);
      Global.myNwMainFrm.cmmnCode.createSysLovsPssblVals(sysLovs1, pssblVals1);
    }

    public void loadPersonMdl()
    {
      //For Accounting
      string[] dfltPrvldgs = {"View Person", "View Basic Person Data", 
		/*2*/ "View Curriculum Vitae", "View Basic Person Assignments", 
    /*4*/ "View Person Pay Item Assignments", "View SQL", "View Record History",
    /*7*/ "Add Person Info","Edit Person Info","Delete Person Info",
    /*10*/"Add Basic Assignments", "Edit Basic Assignments", "Delete Basic Assignments",
    /*13*/"Add Pay Item Assignments", "Edit Pay Item Assignments", "Delete Pay Item Assignments",
      "View Banks",
    /*17*/"Define Assignment Templates", "Edit Assignment Templates", "Delete Assignment Templates",
      "View Assignment Templates"};

      string[] subGrpNames = { "Person Data" };
      string[] mainTableNames = { "prs.prsn_names_nos" };
      string[] keyColumnNames = { "person_id" };
      String myName = "Basic Person Data";
      string myDesc = "This module helps you to setup basic information " +
      "about people in your organization!";
      string audit_tbl_name = "prs.prsn_audit_trail_tbl";
      String smplRoleName = "Basic Person Data Administrator";

      Global.myNwMainFrm.cmmnCode.DefaultPrvldgs = dfltPrvldgs;
      Global.myNwMainFrm.cmmnCode.SubGrpNames = subGrpNames;
      Global.myNwMainFrm.cmmnCode.MainTableNames = mainTableNames;
      Global.myNwMainFrm.cmmnCode.KeyColumnNames = keyColumnNames;

      Global.myNwMainFrm.cmmnCode.ModuleAdtTbl = audit_tbl_name;
      Global.myNwMainFrm.cmmnCode.ModuleDesc = myDesc;
      Global.myNwMainFrm.cmmnCode.ModuleName = myName;
      Global.myNwMainFrm.cmmnCode.SampleRole = smplRoleName;
      Global.myNwMainFrm.cmmnCode.Extra_Adt_Trl_Info = "";
      Global.myNwMainFrm.cmmnCode.checkNAssignReqrmnts();
    }

    public void loadGenStpMdl()
    {
      string[] dfltPrvldgs = { "View General Setup", "View Value List Names"
		, "View possible values", /*3*/"Add Value List Names", "Edit Value List Names"
		, "Delete Value List Names", /*6*/"Add Possible Values", "Edit Possible Values"
		, "Delete Possible Values", "View Record History", "View SQL"};

      string[] subGrpNames = new string[0];
      string[] mainTableNames = new string[0];
      string[] keyColumnNames = new string[0];
      String myName = "General Setup";
      string myDesc = "This module helps you to setup basic information " +
        "to be used by the software later!";
      string audit_tbl_name = "gst.gen_stp_audit_trail_tbl";

      String smplRoleName = "General Setup Administrator";

      Global.myNwMainFrm.cmmnCode.DefaultPrvldgs = dfltPrvldgs;
      Global.myNwMainFrm.cmmnCode.SubGrpNames = subGrpNames;
      Global.myNwMainFrm.cmmnCode.MainTableNames = mainTableNames;
      Global.myNwMainFrm.cmmnCode.KeyColumnNames = keyColumnNames;

      Global.myNwMainFrm.cmmnCode.ModuleAdtTbl = audit_tbl_name;
      Global.myNwMainFrm.cmmnCode.ModuleDesc = myDesc;
      Global.myNwMainFrm.cmmnCode.ModuleName = myName;
      Global.myNwMainFrm.cmmnCode.SampleRole = smplRoleName;
      Global.myNwMainFrm.cmmnCode.Extra_Adt_Trl_Info = "";
      Global.myNwMainFrm.cmmnCode.checkNAssignReqrmnts();
    }

    public void loadIntPymntsMdl()
    {
      string[] dfltPrvldgs = { "View Internal Payments", 
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

      string[] subGrpNames = { "Pay Items" };
      //"Pay Items",
      //"org.org_pay_items",
      //"item_id", 
      string[] mainTableNames = { "org.org_pay_items" };
      string[] keyColumnNames = { "item_id" };

      String myName = "Internal Payments";
      string myDesc = "This module helps you to manage your organization's HR Payments to Personnel!";
      string audit_tbl_name = "pay.pay_audit_trail_tbl";

      String smplRoleName = "Internal Payments Administrator";

      Global.myNwMainFrm.cmmnCode.DefaultPrvldgs = dfltPrvldgs;
      Global.myNwMainFrm.cmmnCode.SubGrpNames = subGrpNames;
      Global.myNwMainFrm.cmmnCode.MainTableNames = mainTableNames;
      Global.myNwMainFrm.cmmnCode.KeyColumnNames = keyColumnNames;

      Global.myNwMainFrm.cmmnCode.ModuleAdtTbl = audit_tbl_name;
      Global.myNwMainFrm.cmmnCode.ModuleDesc = myDesc;
      Global.myNwMainFrm.cmmnCode.ModuleName = myName;
      Global.myNwMainFrm.cmmnCode.SampleRole = smplRoleName;
      Global.myNwMainFrm.cmmnCode.Extra_Adt_Trl_Info = "";
      Global.myNwMainFrm.cmmnCode.checkNAssignReqrmnts();
    }

    public void loadEvntsAttndncMdl()
    {
      string[] dfltPrvldgs = { "View Events And Attendance", 
      /*1*/"View Attendance Records" ,	"View Time Tables", "View Events", 
      /*4*/"View Venues", "View Attendance Search", "View SQL", "View Record History",
      /*8*/"Add Attendance Records","Edit Attendance Records","Delete Attendance Records",
      /*11*/"Add Time Tables","Edit Time Tables","Delete Time Tables", 
      /*14*/"Add Events","Edit Events","Delete Events",
      /*17*/"Add Venues","Edit Venues","Delete Venues",
      /*20*/"Add Event Results","Edit Event Results","Delete Event Results",
/*23*/"View Adhoc Registers","Add Adhoc Registers","Edit Adhoc Registers","Delete Adhoc Registers",
/*27*/"View Event Cost","Add Event Cost","Edit Event Cost","Delete Event Cost",
        /*31*/"View Complaints/Observations","Add Complaints/Observations","Edit Complaints/Observations","Delete Complaints/Observations",
        /*35*/"View only Self-Created Sales","Cancel Documents","Take Payments","Apply Adhoc Discounts", "Apply Pre-defined Discounts", 
        /*40*/"Can Edit Unit Price"};

      string[] subGrpNames = new string[0];
      string[] mainTableNames = new string[0];
      string[] keyColumnNames = new string[0];
      String myName = "Events And Attendance";
      string myDesc = "This module helps you to manage your organization's Events And Attendance!";
      string audit_tbl_name = "attn.attn_audit_trail_tbl";

      String smplRoleName = "Events And Attendance Administrator";

      Global.myNwMainFrm.cmmnCode.DefaultPrvldgs = dfltPrvldgs;
      Global.myNwMainFrm.cmmnCode.SubGrpNames = subGrpNames;
      Global.myNwMainFrm.cmmnCode.MainTableNames = mainTableNames;
      Global.myNwMainFrm.cmmnCode.KeyColumnNames = keyColumnNames;

      Global.myNwMainFrm.cmmnCode.ModuleAdtTbl = audit_tbl_name;
      Global.myNwMainFrm.cmmnCode.ModuleDesc = myDesc;
      Global.myNwMainFrm.cmmnCode.ModuleName = myName;
      Global.myNwMainFrm.cmmnCode.SampleRole = smplRoleName;
      Global.myNwMainFrm.cmmnCode.Extra_Adt_Trl_Info = "";
      Global.myNwMainFrm.cmmnCode.checkNAssignReqrmnts();
    }

    public void loadGenericMdl()
    {
      string[] dfltPrvldgs = { "View Generic Module"};

      string[] subGrpNames = new string[0];
      string[] mainTableNames = new string[0];
      string[] keyColumnNames = new string[0];
      String myName = "Generic Module";
      string myDesc = "This module is a mere place holder for categorising reports and processes!";
      string audit_tbl_name = "sec.sec_audit_trail_tbl";

      String smplRoleName = "Generic Module Administrator";

      Global.myNwMainFrm.cmmnCode.DefaultPrvldgs = dfltPrvldgs;
      Global.myNwMainFrm.cmmnCode.SubGrpNames = subGrpNames;
      Global.myNwMainFrm.cmmnCode.MainTableNames = mainTableNames;
      Global.myNwMainFrm.cmmnCode.KeyColumnNames = keyColumnNames;

      Global.myNwMainFrm.cmmnCode.ModuleAdtTbl = audit_tbl_name;
      Global.myNwMainFrm.cmmnCode.ModuleDesc = myDesc;
      Global.myNwMainFrm.cmmnCode.ModuleName = myName;
      Global.myNwMainFrm.cmmnCode.SampleRole = smplRoleName;
      Global.myNwMainFrm.cmmnCode.Extra_Adt_Trl_Info = "";
      Global.myNwMainFrm.cmmnCode.checkNAssignReqrmnts();
    }

    public void loadSelfServiceMdl()
    {
      string[] dfltPrvldgs = { "View Self-Service",
    /* 1 */ "View Membership Payments", "View Staff Payments", "View Leave of Absence",
    /* 4 */ "View Invoice Documents", "View Events/Attendances", "View Elections", "View Forums",
    /* 8 */ "View Elections Administration", "View Leave Administration", "View Forum Administration",
    /* 11 */ "View Self-Service Administration",
    /* 12 */ "View SQL", "View Record History",
    /* 14 */ "Administer Elections",
    /* 15 */ "Administer Leave",
    /* 16 */ "Administer Self-Service", "Make Requests for Others"};

      string[] subGrpNames = new string[0];
      string[] mainTableNames = new string[0];
      string[] keyColumnNames = new string[0];
      String myName = "Self Service";
      string myDesc = "This module helps your Registered Persons to view and manage their Individual Records when approved!";
      string audit_tbl_name = "self.self_prsn_audit_trail_tbl";

      String smplRoleName = "Self-Service Administrator";

      Global.myNwMainFrm.cmmnCode.DefaultPrvldgs = dfltPrvldgs;
      Global.myNwMainFrm.cmmnCode.SubGrpNames = subGrpNames;
      Global.myNwMainFrm.cmmnCode.MainTableNames = mainTableNames;
      Global.myNwMainFrm.cmmnCode.KeyColumnNames = keyColumnNames;

      Global.myNwMainFrm.cmmnCode.ModuleAdtTbl = audit_tbl_name;
      Global.myNwMainFrm.cmmnCode.ModuleDesc = myDesc;
      Global.myNwMainFrm.cmmnCode.ModuleName = myName;
      Global.myNwMainFrm.cmmnCode.SampleRole = smplRoleName;
      Global.myNwMainFrm.cmmnCode.Extra_Adt_Trl_Info = "";
      Global.myNwMainFrm.cmmnCode.checkNAssignReqrmnts();
    }

    public void loadOrgStpMdl()
    {
      string[] dfltPrvldgs = { "View Organization Setup", 
  "View Org Details", "View Divisions/Groups", "View Sites/Locations", 
    /*4*/"View Jobs", "View Grades", "View Positions", "View Benefits", 
  /*8*/"View Pay Items", "View Remunerations", "View Working Hours", 
    /*11*/"View Gathering Types", "View SQL", "View Record History",
  /*14*/"Add Org Details","Edit Org Details",
  /*16*/"Add Divisions/Groups","Edit Divisions/Groups","Delete Divisions/Groups",
  /*19*/"Add Sites/Locations","Edit Sites/Locations","Delete Sites/Locations",
  /*22*/"Add Jobs","Edit Jobs","Delete Jobs",
  /*25*/"Add Grades","Edit Grades","Delete Grades",
  /*28*/"Add Positions","Edit Positions","Delete Positions",
  /*31*/"Add Pay Items","Edit Pay Items","Delete Pay Items",
  /*34*/"Add Working Hours","Edit Working Hours","Delete Working Hours",
  /*37*/"Add Gathering Types","Edit Gathering Types","Delete Gathering Types"};
      string[] subGrpNames = { "Organization's Details", "Divisions/Groups", 
		"Sites/Locations","Jobs", "Grades", "Positions",  
		"Working Hours", "Gathering Types"};
      //"Pay Items",
      //"org.org_pay_items",
      //"item_id", 
      string[] mainTableNames = {"org.org_details", "org.org_divs_groups", 
		"org.org_sites_locations","org.org_jobs", "org.org_grades", "org.org_positions", 
   "org.org_wrkn_hrs", "org.org_gthrng_types" };
      string[] keyColumnNames = {"org_id", "div_id", 
		"location_id","job_id", "grade_id", "position_id", 
		"work_hours_id", "gthrng_typ_id" };
      String myName = "Organization Setup";
      string myDesc = "This module helps you to setup basic information " +
        "about your organization!";
      string audit_tbl_name = "org.org_audit_trail_tbl";

      String smplRoleName = "Organization Setup Administrator";

      Global.myNwMainFrm.cmmnCode.DefaultPrvldgs = dfltPrvldgs;
      Global.myNwMainFrm.cmmnCode.SubGrpNames = subGrpNames;
      Global.myNwMainFrm.cmmnCode.MainTableNames = mainTableNames;
      Global.myNwMainFrm.cmmnCode.KeyColumnNames = keyColumnNames;

      Global.myNwMainFrm.cmmnCode.ModuleAdtTbl = audit_tbl_name;
      Global.myNwMainFrm.cmmnCode.ModuleDesc = myDesc;
      Global.myNwMainFrm.cmmnCode.ModuleName = myName;
      Global.myNwMainFrm.cmmnCode.SampleRole = smplRoleName;
      Global.myNwMainFrm.cmmnCode.Extra_Adt_Trl_Info = "";
      Global.myNwMainFrm.cmmnCode.checkNAssignReqrmnts();
    }

    public void loadRptMdl()
    {
      string[] dfltPrvldgs = { "View Reports And Processes", 
      /*1*/"View Report Definitions","View Report Runs","View SQL", "View Record History",
      /*5*/"Add Report/Process","Edit Report/Process","Delete Report/Process",
      /*8*/"Run Reports/Process","Delete Report/Process Runs"};

      string[] subGrpNames = new string[0];
      string[] mainTableNames = new string[0];
      string[] keyColumnNames = new string[0];

      String myName = "Reports And Processes";
      string myDesc = "This module helps you to manage all reports in the software!";
      string audit_tbl_name = "rpt.rpt_audit_trail_tbl";

      String smplRoleName = "Reports And Processes Administrator";

      Global.myNwMainFrm.cmmnCode.DefaultPrvldgs = dfltPrvldgs;
      Global.myNwMainFrm.cmmnCode.SubGrpNames = subGrpNames;
      Global.myNwMainFrm.cmmnCode.MainTableNames = mainTableNames;
      Global.myNwMainFrm.cmmnCode.KeyColumnNames = keyColumnNames;

      Global.myNwMainFrm.cmmnCode.ModuleAdtTbl = audit_tbl_name;
      Global.myNwMainFrm.cmmnCode.ModuleDesc = myDesc;
      Global.myNwMainFrm.cmmnCode.ModuleName = myName;
      Global.myNwMainFrm.cmmnCode.SampleRole = smplRoleName;
      Global.myNwMainFrm.cmmnCode.Extra_Adt_Trl_Info = "";
      Global.myNwMainFrm.cmmnCode.checkNAssignReqrmnts();
    }

    public void createExcelTemplate()
    {
      MessageBox.Show("Not yet implemented!");
    }

    public void importDataFromExcel()
    {
      MessageBox.Show("Not yet implemented!");
    }

    public void exprtDataToExcel()
    {
      MessageBox.Show("Not yet implemented!");
    }

    public void creatWordReport()
    {
      MessageBox.Show("Not yet implemented!");
    }

    public void refreshData()
    {
      MessageBox.Show("Not yet implemented!");
    }

    public void viewCurSQL()
    {
      MessageBox.Show("Not yet implemented!");
    }

    public void Initialize()
    {
      //This is the first Function called by the host...
      //Put anything needed to start with here first
      Global.mySecurity = this;
      //Global.myNwMainFrm = (mainForm)this.mainInterface;
    }

    public void Dispose()
    {
      //Put any cleanup code in here for when the program is stopped
      //this.user_id = -1;
      //this.role_set_id = new int[0];
      //this.login_number = -1;
      //this.Host = null;
      //this.myMainInterface = null;
      //Global.mySecurity = null;
      //Global.myNwMainFrm = null;
    }
  }
}
