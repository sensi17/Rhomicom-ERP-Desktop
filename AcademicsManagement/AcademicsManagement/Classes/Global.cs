using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AcademicsManagement.Forms;
using System.Windows.Forms;
using CommonCode;
using Npgsql;

namespace AcademicsManagement.Classes
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
    public static AcademicsManagement myAca = new AcademicsManagement();
    public static mainForm mnFrm = null;
    public static wfnAcaSetupsForm wfnAcaStpFrm = null;
    public static wfnAssmntShtForm wfnAssShtFrm = null;
    public static wfnSmmryRptsForm wfnSmryRptFrm = null;
    public static wfnClassesForm wfnClssFrm = null;

    //public static wfnCoursesForm wfnCrseFrm = null;
    //public static wfnSbjctsForm wfnSbjctsFrm = null;
    public static wfnAthrtiesForm wfnAthrtiesFrm = null;
    public static wfnAcaPrdsForm wfnAcaPrdFrm = null;
    public static wfnAssTypesForm wfnAssTypFrm = null;

    public static leftMenuForm wfnLftMnu = null;
    public static string[] dfltPrvldgs = { "View Summary Reports", "View Learning/Performance Management", 
        /*1*/  "View Assessment Sheets", "View Task Assignment Setups", "View Groups/Courses/Subjects",
        /*4*/  "View Position Holders","View Assessment Periods","View Assessment Reports Types"
        /*7*/ };


    public static string currentPanel = "";
    public static string itms_SQL = "";
    public static int selectedStoreID = -1;

    public static string intFcSql = string.Empty;

    #endregion

    #region "DATA MANIPULATION FUNCTIONS..."

    #region "INSERT STATEMENTS..."
    #endregion

    #region "UPDATE STATEMENTS..."

    #endregion

    #region "DELETE STATEMENTS..."

    #endregion

    #region "SELECT STATEMENTS..."

    #endregion
    #endregion

    #region "CUSTOM FUNCTIONS..."
    #region "MISC..."

    public static void refreshRqrdVrbls()
    {
      Global.mnFrm.cmCde.DefaultPrvldgs = Global.dfltPrvldgs;
      //Global.mnFrm.cmCde.Login_number = Global.myInv.login_number;
      Global.mnFrm.cmCde.ModuleAdtTbl = Global.myAca.full_audit_trail_tbl_name;
      Global.mnFrm.cmCde.ModuleDesc = Global.myAca.mdl_description;
      Global.mnFrm.cmCde.ModuleName = Global.myAca.name;
      //Global.mnFrm.cmCde.pgSqlConn = Global.myInv.Host.globalSQLConn;
      //Global.mnFrm.cmCde.Role_Set_IDs = Global.myInv.role_set_id;
      Global.mnFrm.cmCde.SampleRole = "Learning/Performance Management Administrator";
      //Global.mnFrm.cmCde.User_id = Global.myInv.user_id;
      //Global.mnFrm.cmCde.Org_id = Global.myInv.org_id;
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      Global.myAca.user_id = Global.mnFrm.usr_id;
      Global.myAca.login_number = Global.mnFrm.lgn_num;
      Global.myAca.role_set_id = Global.mnFrm.role_st_id;
      Global.myAca.org_id = Global.mnFrm.Og_id;

    }

    public static void createRqrdLOVs()
    {
      string[] sysLovs = { "Cash Accounts", "Inventory/Asset Accounts", "Contra Expense Accounts",
      "Contra Revenue Accounts","Customer Classifications","Supplier Classifications",
        "Tax Codes","Discount Codes", "Extra Charges", "Approved Requisitions",
        "Suppliers", "Customer/Supplier Sites","Users' Sales Stores","Approved Pro-Forma Invoices",
        "Approved Sales Orders","Approved Internal Item Requests",
        "Customers","Approved Sales Invoices/Item Issues"};
      string[] sysLovsDesc = { "Cash Accounts", "Inventory/Asset Accounts", "Contra Expense Accounts",
      "Contra Revenue Accounts","Customer Classifications","Supplier Classifications",
        "Tax Codes","Discount Codes","Extra Charges","Approved Requisitions",
        "Suppliers", "Customer/Supplier Sites", "Users' Sales Stores","Approved Pro-Forma Invoices",
        "Approved Sales Orders","Approved Internal Item Requests",
        "Customers", "Approved Sales Invoices/Item Issues"};
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
        "select distinct trim(to_char(cust_sup_id,'999999999999999999999999999999')) a, cust_sup_name b, '' c, org_id d from scm.scm_cstmr_suplr where (cust_or_sup = 'Customer') order by 2",
        "select distinct trim(to_char(y.invc_hdr_id,'999999999999999999999999999999')) a, y.invc_number b, '' c, y.org_id d, y.invc_hdr_id g " +
        "from scm.scm_sales_invc_hdr y, scm.scm_sales_invc_det z " +
        "where ((y.invc_type = 'Item Issue-Unbilled' or y.invc_type = 'Sales Invoice') " +
        "and (y.approval_status = 'Approved') " +
        "and (z.invc_hdr_id = y.invc_hdr_id) and ((z.doc_qty - z.qty_trnsctd_in_dest_doc)>0)) order by y.invc_hdr_id DESC",
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
        "select distinct trim(to_char(cust_sup_id,'999999999999999999999999999999')) a, cust_sup_name b, '' c, org_id d from scm.scm_cstmr_suplr where (cust_or_sup = 'Supplier') order by 2",
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

    #endregion
    #endregion
  }
}
