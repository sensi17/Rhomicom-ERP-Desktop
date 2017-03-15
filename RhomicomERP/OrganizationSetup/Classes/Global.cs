using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing.Imaging;
using OrganizationSetup.Forms;
using System.Windows.Forms;
using CommonCode;

namespace OrganizationSetup.Classes
{
    /// <summary>
    /// A  class containing variables and 
    /// functions we will like to call directly from 
    /// anywhere in the project without creating an instance first
    /// </summary>
    class Global
    {
        #region "GLOBAL DECLARATIONS..."
        public static OrganizationSetup myOrgStp = new OrganizationSetup();
        public static mainForm mnFrm = null;
        public static string[] cashFlowClsfctns ={"Cash and Cash Equivalents",
"Operating Activities.Sale of Goods",
"Operating Activities.Sale of Services",
"Operating Activities.Other Income Sources",
"Operating Activities.Cost of Sales",
"Operating Activities.Net Income",
"Operating Activities.Depreciation Expense",
"Operating Activities.Amortization Expense",
"Operating Activities.Gain on Sale of Asset"/*NEGATE*/,
"Operating Activities.Loss on Sale of Asset",
"Operating Activities.Other Non-Cash Expense",
"Operating Activities.Accounts Receivable"/*NEGATE*/,
"Operating Activities.Bad Debt Expense"/*NEGATE*/,
"Operating Activities.Prepaid Expenses"/*NEGATE*/,
"Operating Activities.Inventory"/*NEGATE*/,
"Operating Activities.Accounts Payable",
"Operating Activities.Accrued Expenses",
"Operating Activities.Taxes Payable",
"Operating Activities.Operating Expense"/*NEGATE*/,
"Operating Activities.General and Administrative Expense"/*NEGATE*/,
"Investing Activities.Asset Sales/Purchases"/*NEGATE*/,
"Investing Activities.Equipment Sales/Purchases"/*NEGATE*/,
"Financing Activities.Capital/Stock",
"Financing Activities.Long Term Debts",
"Financing Activities.Short Term Debts",
"Financing Activities.Equity Securities",
"Financing Activities.Dividends Declared"/*NEGATE*/,
""
};
        public static string[] dfltPrvldgs = { "View Organization Setup",
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
        public static string[] subGrpNames = { "Organization's Details", "Divisions/Groups",
        "Sites/Locations","Jobs", "Grades", "Positions", "Pay Items",
        "Working Hours", "Gathering Types"};
        public static string[] mainTableNames = {"org.org_details", "org.org_divs_groups",
        "org.org_sites_locations","org.org_jobs", "org.org_grades", "org.org_positions",
  "org.org_pay_items", "org.org_wrkn_hrs", "org.org_gthrng_types" };
        public static string[] keyColumnNames = {"org_id", "div_id",
        "location_id","job_id", "grade_id", "position_id", "item_id",
        "work_hours_id", "gthrng_typ_id" };
        public static string currentPanel = "";
        #endregion

        #region "INSERT STATEMENTS..."
        public static void createOrg(string orgnm, int prntID, string resAdrs, string pstlAdrs, string webste
            , int crncyid, string email, string contacts, int orgtypID, bool isenbld, string orgdesc,
            string orgslogan, int noOfSegmnts, string segDelimiter)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = "INSERT INTO org.org_details(" +
                     "org_name, parent_org_id, res_addrs, pstl_addrs, " +
                     "email_addrsses, websites, cntct_nos, org_typ_id, " +
                     "org_logo, is_enabled, created_by, creation_date, last_update_by, " +
                                 "last_update_date, oprtnl_crncy_id, org_desc, org_slogan, no_of_accnt_sgmnts, segment_delimiter) " +
             "VALUES ('" + orgnm.Replace("'", "''") + "', " + prntID + ", '" + resAdrs.Replace("'", "''") +
             "', '" + pstlAdrs.Replace("'", "''") + "', '" + email.Replace("'", "''") + "', " +
                     "'" + webste.Replace("'", "''") + "', '" + contacts.Replace("'", "''") +
                     "', " + orgtypID + ", '', '" +
                     Global.mnFrm.cmCde.cnvrtBoolToBitStr(isenbld) + "', " +
                     "" + Global.myOrgStp.user_id + ", '" + dateStr + "', " + Global.myOrgStp.user_id +
                     ", '" + dateStr + "', " + crncyid +
                     ", '" + orgdesc.Replace("'", "''") + "', '" + orgslogan.Replace("'", "''") +
                     ", " + noOfSegmnts + ", '" + segDelimiter.Replace("'", "''") + "')";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
            Global.updtOrgImg(Global.mnFrm.cmCde.getOrgID(orgnm));
        }

        public static void createDiv(int orgid, string divnm, int prntID, int divtypID, bool isenbld, string divdesc)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = "INSERT INTO org.org_divs_groups(" +
                     "org_id, div_code_name, prnt_div_id, div_typ_id, " +
                     "div_logo, is_enabled, created_by, creation_date, last_update_by, " +
                     "last_update_date, div_desc) " +
             "VALUES (" + orgid + ", '" + divnm.Replace("'", "''") + "', " + prntID + ", " + divtypID + ", '', '" +
                     Global.mnFrm.cmCde.cnvrtBoolToBitStr(isenbld) + "', " +
                     "" + Global.myOrgStp.user_id + ", '" + dateStr + "', " + Global.myOrgStp.user_id +
                     ", '" + dateStr + "', '" + divdesc.Replace("'", "''") + "')";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
            Global.updtDivImg(Global.mnFrm.cmCde.getDivID(divnm, Global.mnFrm.cmCde.Org_id));
        }

        public static void createSite(int orgid, string sitenm, string siteDesc, bool isenbld)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = "INSERT INTO org.org_sites_locations(" +
                     "location_code_name, org_id, is_enabled, created_by, " +
                     "creation_date, last_update_by, last_update_date, site_desc) " +
             "VALUES ('" + sitenm.Replace("'", "''") + "', " + orgid + ", '" +
                     Global.mnFrm.cmCde.cnvrtBoolToBitStr(isenbld) + "', " +
                     "" + Global.myOrgStp.user_id + ", '" + dateStr + "', " + Global.myOrgStp.user_id +
                     ", '" + dateStr + "', '" + siteDesc.Replace("'", "''") + "')";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void createJob(int orgid, string jobnm, int prntJobID, string jobDesc, bool isenbld)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = "INSERT INTO org.org_jobs(" +
                     "job_code_name, org_id, job_comments, is_enabled, created_by, " +
                     "creation_date, last_update_by, last_update_date, parnt_job_id) " +
             "VALUES ('" + jobnm.Replace("'", "''") + "', " + orgid + ", '" + jobDesc.Replace("'", "''") + "', '" +
                     Global.mnFrm.cmCde.cnvrtBoolToBitStr(isenbld) + "', " +
                     "" + Global.myOrgStp.user_id + ", '" + dateStr + "', " + Global.myOrgStp.user_id +
                     ", '" + dateStr + "', " + prntJobID + ")";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void createGrd(int orgid, string grdnm, int prntGrdID, string grdDesc, bool isenbld)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = "INSERT INTO org.org_grades(" +
                     "grade_code_name, org_id, grade_comments, is_enabled, " +
                     "created_by, creation_date, last_update_by, last_update_date, " +
                     "parnt_grade_id) " +
             "VALUES ('" + grdnm.Replace("'", "''") + "', " + orgid + ", '" + grdDesc.Replace("'", "''") + "', '" +
                     Global.mnFrm.cmCde.cnvrtBoolToBitStr(isenbld) + "', " +
                     "" + Global.myOrgStp.user_id + ", '" + dateStr + "', " + Global.myOrgStp.user_id +
                     ", '" + dateStr + "', " + prntGrdID + ")";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void createPos(int orgid, string posnm, int prntPosID, string posDesc, bool isenbld)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = "INSERT INTO org.org_positions(" +
                     "position_code_name, prnt_position_id, position_comments, " +
                     "is_enabled, created_by, creation_date, last_update_by, last_update_date, " +
                     "org_id) " +
             "VALUES ('" + posnm.Replace("'", "''") + "', " + prntPosID + ", '" + posDesc.Replace("'", "''") + "', '" +
                     Global.mnFrm.cmCde.cnvrtBoolToBitStr(isenbld) + "', " +
                     "" + Global.myOrgStp.user_id + ", '" + dateStr + "', " + Global.myOrgStp.user_id +
                     ", '" + dateStr + "', " + orgid + ")";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void createWkhr(int orgid, string wkhnm, string wkhDesc, bool isenbld)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = "INSERT INTO org.org_wrkn_hrs(" +
                  "org_id, work_hours_name, work_hours_desc, is_enabled, " +
                  "created_by, creation_date, last_update_by, last_update_date) " +
              "VALUES (" + orgid + ", '" + wkhnm.Replace("'", "''") + "',  '" + wkhDesc.Replace("'", "''") + "', '" +
                              Global.mnFrm.cmCde.cnvrtBoolToBitStr(isenbld) + "', " +
                              "" + Global.myOrgStp.user_id + ", '" + dateStr + "', " + Global.myOrgStp.user_id +
                              ", '" + dateStr + "')";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void createWkhrDet(int wkhid, string weekday, string strtTm, string endTm)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = "INSERT INTO org.org_wrkn_hrs_details(" +
                     "work_hours_id, day_of_week, dflt_nrml_start_time, dflt_nrml_close_time, " +
                     "created_by, creation_date, last_update_by, last_update_date, day_of_wk_no) " +
             "VALUES (" + wkhid + ", '" + weekday.Replace("'", "''") + "',  '" + strtTm.Replace("'", "''") + "', '" +
                     endTm.Replace("'", "''") + "', " +
                     Global.myOrgStp.user_id + ", '" + dateStr + "', " + Global.myOrgStp.user_id +
                     ", '" + dateStr + "', " + Global.mnFrm.cmCde.getDOWNum(weekday) + ")";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void createGath(int orgid, string gthnm, string gthDesc, bool isenbld, string strtTm, string endTm)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = "INSERT INTO org.org_gthrng_types(" +
                     "gthrng_typ_name, gthrng_typ_desc, org_id, is_enabled, " +
                     "created_by, creation_date, last_update_by, last_update_date, " +
                     "gath_start_time, gath_end_time) " +
             "VALUES ('" + gthnm.Replace("'", "''") + "',  '" + gthDesc.Replace("'", "''") +
             "', " + orgid + ", '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(isenbld) + "', " +
                     Global.myOrgStp.user_id + ", '" + dateStr + "', " + Global.myOrgStp.user_id +
                     ", '" + dateStr + "', '" + strtTm.Replace("'", "''") + "', '" + endTm.Replace("'", "''") + "')";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }
        #endregion

        #region "UPDATE STATEMENTS..."
        public static void updateGath(int gthid, string gthnm, string gthDesc, bool isenbld, string strtTm, string endTm)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE org.org_gthrng_types " +
            "SET gthrng_typ_name='" + gthnm.Replace("'", "''") +
            "', gthrng_typ_desc='" + gthDesc.Replace("'", "''") + "', " +
                "gath_start_time='" + strtTm.Replace("'", "''") +
                "', gath_end_time='" + endTm.Replace("'", "''") +
                "', last_update_by=" + Global.myOrgStp.user_id + ", " +
                "last_update_date='" + dateStr + "', is_enabled = '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(isenbld) +
          "' WHERE gthrng_typ_id=" + gthid;
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updateWkhrDet(int row_id, int wkhid,
         string weekday, string strtTm, string endTm)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE org.org_wrkn_hrs_details " +
            "SET work_hours_id=" + wkhid +
            ", day_of_week='" + weekday.Replace("'", "''") + "', " +
                "dflt_nrml_start_time='" + strtTm.Replace("'", "''") +
                "', dflt_nrml_close_time='" + endTm.Replace("'", "''") +
                "', last_update_by=" + Global.myOrgStp.user_id + ", " +
                "last_update_date='" + dateStr + "', day_of_wk_no = " + Global.mnFrm.cmCde.getDOWNum(weekday) +
          " WHERE dflt_row_id=" + row_id;
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updateWkhr(int wkhid,
          string wkhnm, string wkhDesc, bool isenbld)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE org.org_wrkn_hrs " +
            "SET work_hours_name='" + wkhnm.Replace("'", "''") +
            "', work_hours_desc='" + wkhDesc.Replace("'", "''") + "', " +
                    "is_enabled='" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(isenbld) +
                    "', last_update_by=" + Global.myOrgStp.user_id + ", " +
                    "last_update_date='" + dateStr + "' " +
        "WHERE work_hours_id=" + wkhid;
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updtPosPrntID(int posid, int prntID)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE org.org_positions SET prnt_position_id = " + prntID +
                     ", last_update_by = " + Global.myOrgStp.user_id + ", " +
                     "last_update_date = '" + dateStr + "' " +
             "WHERE (position_id = " + posid + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updatePos(int posid, string posnm, int prntPosID, string posDesc, bool isenbld)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE org.org_positions " +
            "SET position_code_name='" + posnm.Replace("'", "''") +
            "', prnt_position_id=" + prntPosID + ", position_comments='" + posDesc.Replace("'", "''") + "', " +
                "is_enabled='" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(isenbld) +
                "', last_update_by=" + Global.myOrgStp.user_id + ", " +
                "last_update_date='" + dateStr + "' " +
          "WHERE position_id=" + posid;
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updtGrdPrntID(int grdid, int prntID)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE org.org_grades SET parnt_grade_id = " + prntID +
                     ", last_update_by = " + Global.myOrgStp.user_id + ", " +
                     "last_update_date = '" + dateStr + "' " +
             "WHERE (grade_id = " + grdid + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updateGrd(int grdid, string grdnm, int prntGrdID, string grdDesc, bool isenbld)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE org.org_grades " +
                "SET grade_code_name='" + grdnm.Replace("'", "''") +
                "', grade_comments='" + grdDesc.Replace("'", "''") + "', is_enabled='" +
                     Global.mnFrm.cmCde.cnvrtBoolToBitStr(isenbld) + "', " +
                "last_update_by=" + Global.myOrgStp.user_id + ", last_update_date='" + dateStr + "', " +
                "parnt_grade_id=" + prntGrdID + " WHERE grade_id=" + grdid;
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updtJobPrntID(int jobID, int prntID)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE org.org_jobs SET parnt_job_id = " + prntID +
                     ", last_update_by = " + Global.myOrgStp.user_id + ", " +
                     "last_update_date = '" + dateStr + "' " +
             "WHERE (job_id = " + jobID + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updateJob(int jobid, string jobnm, int prntJobID, string jobDesc, bool isenbld)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE org.org_jobs " +
                "SET job_code_name='" + jobnm.Replace("'", "''") +
                "', job_comments='" + jobDesc.Replace("'", "''") +
                "', is_enabled='" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(isenbld) + "', " +
                "last_update_by=" + Global.myOrgStp.user_id +
                ", last_update_date='" + dateStr + "', " +
                "parnt_job_id=" + prntJobID + " WHERE job_id= " + jobid;
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updtOrgImg(int orgID)
        {
            if (Global.mnFrm.cmCde.myComputer.FileSystem.FileExists(Global.mnFrm.cmCde.getOrgImgsDrctry() + @"\" + orgID.ToString() + ".png"))
            {
                Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
                string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
                string sqlStr = "UPDATE org.org_details SET " +
                "org_logo = '" + orgID.ToString() + ".png', " +
                "last_update_by = " + Global.myOrgStp.user_id +
                ", last_update_date = '" + dateStr + "' " +
                "WHERE(org_id = " + orgID + ")";
                Global.mnFrm.cmCde.updateDataNoParams(sqlStr);
            }
        }

        public static void updtOrgPrntID(int orgID, int prntID)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE org.org_details SET parent_org_id = " + prntID +
                     ", last_update_by = " + Global.myOrgStp.user_id + ", " +
                     "last_update_date = '" + dateStr + "' " +
             "WHERE (org_id = " + orgID + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updtOrgCrncyID(int orgID, int crncyid)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE org.org_details SET last_update_by = " + Global.myOrgStp.user_id + ", " +
                     "last_update_date = '" + dateStr + "', oprtnl_crncy_id = " + crncyid + " " +
             "WHERE (org_id = " + orgID + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updtOrgTypID(int orgID, int orgtypID)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE org.org_details SET org_typ_id = " + orgtypID + ", last_update_by = " + Global.myOrgStp.user_id + ", " +
                     "last_update_date = '" + dateStr + "' " +
             "WHERE (org_id = " + orgID + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updateOrgDet(int orgid, string orgnm, int prntID, string resAdrs, string pstlAdrs, string webste
       , int crncyid, string email, string contacts, int orgtypID, bool isenbld, string orgdesc
            , string orgslogan, int noOfSegmnts, string segDelimiter)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE org.org_details SET " +
                     "org_name = '" + orgnm.Replace("'", "''") + "', parent_org_id = " + prntID +
                     ", res_addrs = '" + resAdrs.Replace("'", "''") + "', pstl_addrs = '" + pstlAdrs.Replace("'", "''") + "', " +
                     "email_addrsses = '" + email.Replace("'", "''") + "', websites = '" + webste.Replace("'", "''") +
                     "', cntct_nos = '" + contacts.Replace("'", "''") + "', org_typ_id = " + orgtypID + ", " +
                     "is_enabled = '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(isenbld) +
                     "', last_update_by = " + Global.myOrgStp.user_id + ", " +
                     "last_update_date = '" + dateStr + "', oprtnl_crncy_id = " + crncyid +
                     ", org_desc = '" + orgdesc.Replace("'", "''") +
                     "', org_slogan='" + orgslogan.Replace("'", "''") +
                     "', no_of_accnt_sgmnts = " + noOfSegmnts +
                     ", segment_delimiter = '" + segDelimiter.Replace("'", "''") + "' " +
             "WHERE (org_id = " + orgid + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updtDivImg(int divID)
        {
            if (Global.mnFrm.cmCde.myComputer.FileSystem.FileExists(Global.mnFrm.cmCde.getDivsImgsDrctry() + @"\" + divID.ToString() + ".png"))
            {
                Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
                string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
                string sqlStr = "UPDATE org.org_divs_groups SET " +
                "div_logo = '" + divID.ToString() + ".png', " +
                "last_update_by = " + Global.myOrgStp.user_id +
                ", last_update_date = '" + dateStr + "' " +
                "WHERE(div_id = " + divID + ")";
                Global.mnFrm.cmCde.updateDataNoParams(sqlStr);
            }
        }

        public static void updateDivDet(int divid, string divnm, int prntID, int divtypID, bool isenbld, string divdesc)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE org.org_divs_groups SET " +
                     "div_code_name = '" + divnm.Replace("'", "''") + "', prnt_div_id = " + prntID +
                     ", div_typ_id = " + divtypID + ", " +
                     "is_enabled = '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(isenbld) +
                     "', last_update_by = " + Global.myOrgStp.user_id + ", " +
                     "last_update_date = '" + dateStr + "', div_desc = '" + divdesc.Replace("'", "''") + "' " +
             "WHERE (div_id = " + divid + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updtDivPrntID(int divID, int prntID)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE org.org_divs_groups SET prnt_div_id = " + prntID +
                     ", last_update_by = " + Global.myOrgStp.user_id + ", " +
                     "last_update_date = '" + dateStr + "' " +
             "WHERE (div_id = " + divID + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updtDivTypID(int divID, int divtypID)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE org.org_divs_groups SET div_typ_id = " + divtypID + ", last_update_by = " + Global.myOrgStp.user_id + ", " +
                     "last_update_date = '" + dateStr + "' " +
             "WHERE (div_id = " + divID + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updateSiteDet(int siteid, string sitenm, string siteDesc, bool isenbld)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE org.org_sites_locations SET " +
                     "location_code_name = '" + sitenm.Replace("'", "''") + "', " +
                     "is_enabled = '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(isenbld) +
                     "', site_desc = '" + siteDesc.Replace("'", "''") + "', last_update_by = " + Global.myOrgStp.user_id + ", " +
                     "last_update_date = '" + dateStr + "' " +
             "WHERE (location_id = " + siteid + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        #endregion

        #region "DELETE STATEMENTS..."
        public static void deletePayItm(long itmid, string itmNm)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Pay Item Name = " + itmNm;
            string delSQL = "DELETE FROM org.org_pay_items WHERE item_id = " + itmid;
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

        public static void deleteWkhDet(long row_id, string wkhNm)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Work Hour Name = " + wkhNm;
            string delSQL = "DELETE FROM org.org_wrkn_hrs_details WHERE dflt_row_id = " + row_id;
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
        }

        public static void deleteWkh(long row_id, string wkhNm)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Work Hour Name = " + wkhNm;
            string delSQL = "DELETE FROM org.org_wrkn_hrs WHERE work_hours_id = " + row_id;
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
        }

        public static void deleteGth(long row_id, string gthnm)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Gathering Name = " + gthnm;
            string delSQL = "DELETE FROM org.org_gthrng_types WHERE gthrng_typ_id = " + row_id;
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
        }

        public static void deleteDiv(long divid, string divNm)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Division/Group Name = " + divNm;
            string delSQL = "DELETE FROM org.org_divs_groups WHERE div_id = " + divid;
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
        }
        public static void deleteOrg(long orgid, string orgNm)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Organisation Name = " + orgNm;
            string delSQL = "DELETE FROM org.org_divs_groups WHERE org_id = " + orgid;
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
            delSQL = "DELETE FROM org.org_grades WHERE org_id = " + orgid;
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
            delSQL = "DELETE FROM org.org_gthrng_types WHERE org_id = " + orgid;
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
            delSQL = "DELETE FROM org.org_jobs WHERE org_id = " + orgid;
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
            delSQL = "DELETE FROM org.org_pay_items WHERE org_id = " + orgid;
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
            delSQL = "DELETE FROM org.org_pay_items_values WHERE item_id NOT IN (select item_id from org.org_pay_items WHERE org_id = " + orgid + ")";
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
            delSQL = "DELETE FROM org.org_pay_itm_feeds WHERE balance_item_id NOT IN (select item_id from org.org_pay_items WHERE org_id = " + orgid + ")";
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
            delSQL = "DELETE FROM org.org_positions WHERE org_id = " + orgid;
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
            delSQL = "DELETE FROM org.org_sites_locations WHERE org_id = " + orgid;
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
            delSQL = "DELETE FROM org.org_wrkn_hrs WHERE org_id = " + orgid;
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
            delSQL = "DELETE FROM org.org_wrkn_hrs_details WHERE work_hours_id NOT IN (select work_hours_id from org.org_wrkn_hrs WHERE org_id = " + orgid + ")";
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
            delSQL = "DELETE FROM org.org_details WHERE org_id = " + orgid;
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
        }
        public static void deleteSite(long siteid, string siteNm)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Site/Location Name = " + siteNm;
            string delSQL = "DELETE FROM org.org_sites_locations WHERE location_id = " + siteid;
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
        }

        public static void deleteJob(long jobid, string jobNm)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Job Name = " + jobNm;
            string delSQL = "DELETE FROM org.org_jobs WHERE job_id = " + jobid;
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
        }

        public static void deletePos(long posid, string posNm)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Position Name = " + posNm;
            string delSQL = "DELETE FROM org.org_positions WHERE position_id = " + posid;
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
        }

        public static void deleteGrd(long grdid, string grdNm)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Grade Name = " + grdNm;
            string delSQL = "DELETE FROM org.org_grades WHERE grade_id = " + grdid;
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
        }
        #endregion

        #region "SELECT STATEMENTS..."
        #region "ORG DETAILS..."
        public static DataSet get_Hrchy_OrgDet(string searchWord, string searchIn,
         Int64 offset, int limit_size)
        {
            string strSql = "";
            strSql = @"WITH RECURSIVE suborg(org_id, parent_org_id, org_name, depth, path, cycle, space) AS
            ( 
            SELECT e.org_id, e.parent_org_id, e.org_name, 1, ARRAY[e.org_id], false, '' FROM org.org_details e WHERE e.parent_org_id = -1 
            UNION ALL 
            SELECT d.org_id, d.parent_org_id, d.org_name, sd.depth + 1, 
            path || d.org_id, 
            d.org_id = ANY(path), space || '   ' 
            FROM 
            org.org_details AS d, 
            suborg AS sd 
            WHERE d.parent_org_id = sd.org_id AND NOT cycle) 
            SELECT org_id, parent_org_id, org_name as org, depth, path, cycle 
            FROM suborg 
            ORDER BY path LIMIT " + limit_size +
           " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            Global.mnFrm.orgDetHrchy_SQL = strSql;
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static DataSet get_One_OrgDet(int orgid)
        {
            string strSql = "";
            strSql = "SELECT a.org_id, a.org_name, a.parent_org_id, (select b.org_name FROM " +
             "org.org_details b where b.org_id = a.parent_org_id) parnt_org, res_addrs, pstl_addrs, " +
             "email_addrsses, websites, cntct_nos, org_typ_id, (select c.pssbl_value from gst.gen_stp_lov_values " +
             "c where c.pssbl_value_id = a.org_typ_id) org_typ_nm, org_logo, is_enabled, oprtnl_crncy_id, " +
             "(select d.pssbl_value from gst.gen_stp_lov_values " +
                 "d where d.pssbl_value_id = a.oprtnl_crncy_id) crcy_code, org_desc, org_slogan, no_of_accnt_sgmnts, segment_delimiter FROM org.org_details a " +
          "WHERE ((a.org_id = " + orgid + ")) ORDER BY a.org_id";
            //Global.mnFrm.orgDet_SQL = strSql;
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static DataSet get_One_SegmentDet(int segNum, int orgid)
        {
            string strSql = "";
            strSql = @"SELECT segment_id, segment_name_prompt, system_clsfctn 
        FROM org.org_acnt_sgmnts a  WHERE((a.org_id = " + orgid + " and a.segment_number = " + segNum + "))";
            //Global.mnFrm.orgDet_SQL = strSql;
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static int get_SegmnetID(long orgid, int segNum)
        {
            string strSql = @"SELECT segment_id FROM org.org_acnt_sgmnts a  " +
             " WHERE((a.org_id = " + orgid + " and a.segment_number = " + segNum + "))";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public static void createAcntSegment(long orgID,
     int segmntNum, string segmntName, string sysClsfctn)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = @"INSERT INTO org.org_acnt_sgmnts (
            segment_number, segment_name_prompt, system_clsfctn, 
            created_by, creation_date, last_update_by, last_update_date, 
            org_id) " +
                  "VALUES (" + segmntNum + ", '" + segmntName.Replace("'", "''") + "', '" + sysClsfctn.Replace("'", "''") +
                  "', " + Global.myOrgStp.user_id + ", '" + dateStr +
                  "', " + Global.myOrgStp.user_id + ", '" + dateStr +
                  "', " + orgID + ")";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void updtAcntSegment(long segmntID,
     int segmntNum, string segmntName, string sysClsfctn)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = @"UPDATE org.org_acnt_sgmnts SET 
             segment_name_prompt='" + segmntName.Replace("'", "''") +
                  "', system_clsfctn='" + sysClsfctn.Replace("'", "''") +
                  "', last_update_by=" + Global.myOrgStp.user_id +
                  ", last_update_date='" + dateStr +
                  "' WHERE segment_id=" + segmntID + " ";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }


        public static DataSet get_Basic_OrgDet(string searchWord, string searchIn,
         Int64 offset, int limit_size)
        {
            string strSql = "";
            if (searchIn == "Organization Name")
            {
                strSql = "SELECT a.org_id, a.org_name, a.parent_org_id, (select b.org_name FROM " +
                 "org.org_details b where b.org_id = a.parent_org_id) parnt_org, res_addrs, pstl_addrs, " +
                 "email_addrsses, websites, cntct_nos, org_typ_id, (select c.pssbl_value from gst.gen_stp_lov_values " +
                 "c where c.pssbl_value_id = a.org_typ_id) org_typ_nm, org_logo, is_enabled, oprtnl_crncy_id, " +
                 "(select d.pssbl_value from gst.gen_stp_lov_values " +
                      "d where d.pssbl_value_id = a.oprtnl_crncy_id) crcy_code, org_desc, org_slogan, no_of_accnt_sgmnts, segment_delimiter FROM org.org_details a " +
              "WHERE ((a.org_name ilike '" + searchWord.Replace("'", "''") +
                 "')) ORDER BY a.org_id LIMIT " + limit_size +
                 " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            else if (searchIn == "Parent Organization Name")
            {
                strSql = "SELECT a.org_id, a.org_name, a.parent_org_id, (select b.org_name FROM " +
            "org.org_details b where b.org_id = a.parent_org_id) parnt_org, res_addrs, pstl_addrs, " +
            "email_addrsses, websites, cntct_nos, org_typ_id, (select c.pssbl_value from gst.gen_stp_lov_values " +
            "c where c.pssbl_value_id = a.org_typ_id) org_typ_nm, org_logo, is_enabled, oprtnl_crncy_id, " +
            "(select d.pssbl_value from gst.gen_stp_lov_values " +
            "d where d.pssbl_value_id = a.oprtnl_crncy_id) crcy_code, org_desc, org_slogan FROM org.org_details a " +
            "WHERE (((select b.org_name FROM " +
            "org.org_details b where b.org_id = a.parent_org_id) ilike '" + searchWord.Replace("'", "''") +
            "')) ORDER BY a.org_id LIMIT " + limit_size +
            " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            Global.mnFrm.orgDet_SQL = strSql;
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static long get_Total_OrgDet(string searchWord, string searchIn)
        {
            string strSql = "";
            if (searchIn == "Organization Name")
            {
                strSql = "SELECT count(1) FROM org.org_details a " +
              "WHERE ((a.org_name ilike '" + searchWord.Replace("'", "''") +
              "'))";
            }
            else if (searchIn == "Parent Organization Name")
            {
                strSql = "SELECT count(1) FROM org.org_details a " +
              "WHERE (((select b.org_name FROM " +
                 "org.org_details b where b.org_id = a.parent_org_id) ilike '" + searchWord.Replace("'", "''") +
              "'))";
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

        public static string get_Org_Rec_Hstry(int orgID)
        {
            string strSQL = @"SELECT a.created_by, 
      to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS'), 
      a.last_update_by, 
      to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS') " +
            "FROM org.org_details a WHERE(a.org_id = " + orgID + ")";
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

        #region "DIV DETAILS..."
        public static DataSet get_Hrchy_DivDet(string searchWord, string searchIn,
         Int64 offset, int limit_size, int orgID)
        {
            string strSql = "";
            strSql = "WITH RECURSIVE subdiv(div_id, prnt_div_id, div_code_name, depth, path, cycle, space, org_id) AS " +
             "( " +
             "SELECT e.div_id, e.prnt_div_id, e.div_code_name, 1, ARRAY[e.div_id], false, '', e.org_id FROM org.org_divs_groups e WHERE e.prnt_div_id = -1 " +
             "UNION ALL " +
             "SELECT d.div_id, d.prnt_div_id, d.div_code_name,sd.depth + 1, " +
             "path || d.div_id, " +
             "d.div_id = ANY(path), space || '.', d.org_id " +
             "FROM " +
             "org.org_divs_groups AS d, " +
             "subdiv AS sd " +
             "WHERE d.prnt_div_id = sd.div_id AND NOT cycle " +
             ") " +
             "SELECT div_id, prnt_div_id, div_code_name, depth, path, cycle " +
             "FROM subdiv " +
             "WHERE (org_id = " + orgID + ") ORDER BY path LIMIT " + limit_size +
             " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            Global.mnFrm.divDetHrchy_SQL = strSql;
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static DataSet get_One_DivDet_Det(int divID)
        {
            string strSql = "";
            strSql = "SELECT a.div_id, a.div_code_name, a.prnt_div_id, (select b.div_code_name FROM " +
             "org.org_divs_groups b where b.div_id = a.prnt_div_id) parnt_div, div_typ_id, " +
             "(select c.pssbl_value from gst.gen_stp_lov_values " +
             "c where c.pssbl_value_id = a.div_typ_id) div_typ_nm, div_logo, is_enabled, div_desc " +
             "FROM org.org_divs_groups a " +
             "WHERE(div_id = " + divID + ") ORDER BY a.div_id";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static DataSet get_Basic_DivDet(string searchWord, string searchIn,
         Int64 offset, int limit_size, int orgID)
        {
            string strSql = "";
            if (searchIn == "Division Name")
            {
                strSql = "SELECT a.div_id, a.div_code_name, a.prnt_div_id, (select b.div_code_name FROM " +
                "org.org_divs_groups b where b.div_id = a.prnt_div_id) parnt_div, div_typ_id, " +
                "(select c.pssbl_value from gst.gen_stp_lov_values " +
                "c where c.pssbl_value_id = a.div_typ_id) div_typ_nm, div_logo, is_enabled, div_desc " +
                "FROM org.org_divs_groups a " +
                "WHERE ((a.div_code_name ilike '" + searchWord.Replace("'", "''") +
                 "') AND (org_id = " + orgID + ")) ORDER BY a.div_code_name LIMIT " + limit_size +
                 " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            else if (searchIn == "Parent Division Name")
            {
                strSql = "SELECT a.div_id, a.div_code_name, a.prnt_div_id, (select b.div_code_name FROM " +
                "org.org_divs_groups b where b.div_id = a.prnt_div_id) parnt_div, div_typ_id, " +
                "(select c.pssbl_value from gst.gen_stp_lov_values " +
                "c where c.pssbl_value_id = a.div_typ_id) div_typ_nm, div_logo, is_enabled, div_desc " +
                "FROM org.org_divs_groups a " +
                "WHERE (((select b.div_code_name FROM " +
                "org.org_divs_groups b where b.div_id = a.prnt_div_id) ilike '" + searchWord.Replace("'", "''") +
                "') AND (org_id = " + orgID + ")) ORDER BY a.div_code_name LIMIT " + limit_size +
                " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            Global.mnFrm.divDet_SQL = strSql;
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static long get_Total_DivDet(string searchWord, string searchIn, int orgID)
        {
            string strSql = "";
            if (searchIn == "Division Name")
            {
                strSql = "SELECT count(1) FROM org.org_divs_groups a " +
              "WHERE ((a.div_code_name ilike '" + searchWord.Replace("'", "''") +
              "') AND (org_id = " + orgID + "))";
            }
            else if (searchIn == "Parent Division Name")
            {
                strSql = "SELECT count(1) FROM org.org_divs_groups a " +
              "WHERE (((select b.div_code_name FROM " +
                "org.org_divs_groups b where b.div_id = a.prnt_div_id) ilike '" + searchWord.Replace("'", "''") +
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

        public static string get_Div_Rec_Hstry(int divID)
        {
            string strSQL = @"SELECT a.created_by, 
      to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS'), 
      a.last_update_by, 
      to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS') " +
            "FROM org.org_divs_groups a WHERE(a.div_id = " + divID + ")";
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

        public static bool isOrgInUse(int orgID)
        {
            string strSql = "SELECT a.person_id " +
             "FROM prs.prsn_names_nos a " +
             "WHERE(a.org_id = " + orgID + ") ORDER BY 1 LIMIT 1 OFFSET 0";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            strSql = "SELECT a.accnt_id " +
             "FROM accb.accb_chart_of_accnts a " +
             "WHERE(a.org_id = " + orgID + ") ORDER BY 1 LIMIT 1 OFFSET 0";
            dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            strSql = "SELECT a.org_id " +
            "FROM org.org_details a " +
            "WHERE(a.parent_org_id = " + orgID + ") ORDER BY 1 LIMIT 1 OFFSET 0";
            dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public static bool isDivInUse(int divID)
        {
            string strSql = "SELECT a.prsn_div_id " +
             "FROM pasn.prsn_divs_groups a " +
             "WHERE(a.div_id = " + divID + ")";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public static bool isSiteInUse(int siteID)
        {
            string strSql = "SELECT a.prsn_loc_id " +
             "FROM pasn.prsn_locations a " +
             "WHERE(a.location_id = " + siteID + ")";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public static bool isJobInUse(int jobID)
        {
            string strSql = "SELECT a.row_id " +
             "FROM pasn.prsn_jobs a " +
             "WHERE(a.job_id = " + jobID + ")";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public static bool isGrdInUse(int grdID)
        {
            string strSql = "SELECT a.row_id " +
             "FROM pasn.prsn_grades a " +
             "WHERE(a.grade_id = " + grdID + ")";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public static bool isPosInUse(int posID)
        {
            string strSql = "SELECT a.row_id " +
             "FROM pasn.prsn_positions a " +
             "WHERE(a.position_id = " + posID + ")";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public static bool isItmInUse(int itmID)
        {
            string strSql = "SELECT a.row_id " +
             "FROM pasn.prsn_bnfts_cntrbtns a " +
             "WHERE(a.item_id = " + itmID + ")";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            strSql = "SELECT a.pay_trns_id " +
             "FROM pay.pay_itm_trnsctns a " +
             "WHERE(a.item_id = " + itmID + ")";
            dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            strSql = "SELECT a.pssbl_value_id " +
             "FROM org.org_pay_items_values a " +
             "WHERE(a.item_id = " + itmID + ")";
            dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            strSql = "SELECT a.feed_id " +
             "FROM org.org_pay_itm_feeds a " +
             "WHERE(a.fed_by_itm_id = " + itmID + " or a.balance_item_id = " + itmID + ")";
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

        public static bool isWkhInUse(int wkhID)
        {
            string strSql = "SELECT a.row_id " +
             "FROM pasn.prsn_work_id a " +
             "WHERE(a.work_hour_id = " + wkhID + ")";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public static bool isGthInUse(int gthID)
        {
            string strSql = "SELECT a.row_id " +
             "FROM pasn.prsn_gathering_typs a " +
             "WHERE(a.gatherng_typ_id = " + gthID + ")";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region "SITES DETAILS..."
        public static DataSet get_One_Site_Det(int siteID)
        {
            string strSql = "";
            strSql = "SELECT a.location_id, a.location_code_name, a.site_desc, a.is_enabled " +
             "FROM org.org_sites_locations a " +
             "WHERE(a.location_id = " + siteID + ")";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            Global.mnFrm.siteDet_SQL = strSql;
            return dtst;
        }

        public static DataSet get_Basic_Site(string searchWord, string searchIn,
         Int64 offset, int limit_size, int orgID)
        {
            string strSql = "";
            if (searchIn == "Site Name")
            {
                strSql = "SELECT a.location_id, a.location_code_name " +
                "FROM org.org_sites_locations a " +
                "WHERE ((a.location_code_name ilike '" + searchWord.Replace("'", "''") +
                 "') AND (org_id = " + orgID + ")) ORDER BY a.location_id LIMIT " + limit_size +
                 " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            else if (searchIn == "Site Description")
            {
                strSql = "SELECT a.location_id, a.location_code_name " +
                "FROM org.org_sites_locations a " +
                "WHERE ((a.site_desc ilike '" + searchWord.Replace("'", "''") +
                 "') AND (org_id = " + orgID + ")) ORDER BY a.location_id LIMIT " + limit_size +
                 " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            Global.mnFrm.site_SQL = strSql;
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static long get_Total_Sites(string searchWord, string searchIn, int orgID)
        {
            string strSql = "";
            if (searchIn == "Site Name")
            {
                strSql = "SELECT count(1) " +
                "FROM org.org_sites_locations a " +
                "WHERE ((a.location_code_name ilike '" + searchWord.Replace("'", "''") +
                 "') AND (org_id = " + orgID + "))";
            }
            else if (searchIn == "Site Description")
            {
                strSql = "SELECT count(1)  " +
                "FROM org.org_sites_locations a " +
                "WHERE ((a.site_desc ilike '" + searchWord.Replace("'", "''") +
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

        public static string get_Site_Rec_Hstry(int siteID)
        {
            string strSQL = @"SELECT a.created_by, 
      to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS'), 
      a.last_update_by, 
      to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS') " +
            "FROM org.org_sites_locations a WHERE(a.location_id = " + siteID + ")";
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

        #region "JOBS DETAILS..."
        public static DataSet get_Hrchy_Jobs(Int64 offset, int limit_size, int orgID)
        {
            string strSql = "";
            strSql = "WITH RECURSIVE subjob(job_id, parnt_job_id, job_code_name, depth, path, cycle, space, org_id) AS " +
             "( " +
             "SELECT e.job_id, e.parnt_job_id, e.job_code_name, 1, ARRAY[e.job_id], false, '', e.org_id FROM org.org_jobs e WHERE e.parnt_job_id = -1 " +
             "UNION ALL " +
             "SELECT d.job_id, d.parnt_job_id, d.job_code_name,sd.depth + 1, " +
             "path || d.job_id, " +
             "d.job_id = ANY(path), space || '.', d.org_id " +
             "FROM " +
             "org.org_jobs AS d, " +
             "subjob AS sd " +
             "WHERE d.parnt_job_id = sd.job_id AND NOT cycle " +
             ") " +
             "SELECT job_id, parnt_job_id, job_code_name, depth, path, cycle " +
             "FROM subjob " +
             "WHERE (org_id = " + orgID + ") ORDER BY path LIMIT " + limit_size +
             " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            Global.mnFrm.jobHrchy_SQL = strSql;
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static DataSet get_One_Job(int jobID)
        {
            string strSql = "";
            strSql = "SELECT a.job_id, a.job_code_name, a.parnt_job_id, (select b.job_code_name FROM " +
             "org.org_jobs b where b.job_id = a.parnt_job_id) parnt_job, job_comments, is_enabled " +
             "FROM org.org_jobs a " +
             "WHERE(job_id = " + jobID + ") ORDER BY a.job_id";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static DataSet get_Basic_Job(string searchWord, string searchIn,
         Int64 offset, int limit_size, int orgID)
        {
            string strSql = "";
            if (searchIn == "Job Name")
            {
                strSql = "SELECT a.job_id, a.job_code_name, a.parnt_job_id, (select b.job_code_name FROM " +
                 "org.org_jobs b where b.job_id = a.parnt_job_id) parnt_job, job_comments, is_enabled " +
                 "FROM org.org_jobs a " +
                 "WHERE ((a.job_code_name ilike '" + searchWord.Replace("'", "''") +
                 "') AND (org_id = " + orgID + ")) ORDER BY a.job_code_name LIMIT " + limit_size +
                 " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            else if (searchIn == "Parent Job Name")
            {
                strSql = "SELECT a.job_id, a.job_code_name, a.parnt_job_id, (select b.job_code_name FROM " +
                 "org.org_jobs b where b.job_id = a.parnt_job_id) parnt_job, job_comments, is_enabled " +
                 "FROM org.org_jobs a " +
                "WHERE (((select b.job_code_name FROM " +
                "org.org_jobs b where b.job_id = a.parnt_job_id) ilike '" + searchWord.Replace("'", "''") +
                "') AND (org_id = " + orgID + ")) ORDER BY a.job_code_name LIMIT " + limit_size +
                " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            Global.mnFrm.jobs_SQL = strSql;
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static long get_Total_Job(string searchWord, string searchIn, int orgID)
        {
            string strSql = "";
            if (searchIn == "Job Name")
            {
                strSql = "SELECT count(1) FROM org.org_jobs a " +
              "WHERE ((a.job_code_name ilike '" + searchWord.Replace("'", "''") +
              "') AND (org_id = " + orgID + "))";
            }
            else if (searchIn == "Parent Job Name")
            {
                strSql = "SELECT count(1) FROM org.org_jobs a " +
              "WHERE (((select b.job_code_name FROM " +
                "org.org_jobs b where b.job_id = a.parnt_job_id) ilike '" + searchWord.Replace("'", "''") +
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

        public static string get_Job_Rec_Hstry(int jobID)
        {
            string strSQL = @"SELECT a.created_by, 
to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS'), 
      a.last_update_by, 
      to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS') " +
            "FROM org.org_jobs a WHERE(a.job_id = " + jobID + ")";
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

        #region "GRADE DETAILS..."
        public static DataSet get_One_Grade_Det(int grdID)
        {
            string strSql = "";
            strSql = "SELECT a.grade_id, a.grade_code_name, a.grade_comments, a.is_enabled, a.parnt_grade_id " +
             "FROM org.org_grades a " +
             "WHERE(a.grade_id = " + grdID + ")";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            //Global.mnFrm.grd_SQL = strSql;
            return dtst;
        }

        public static DataSet get_Basic_Grade(string searchWord, string searchIn,
         Int64 offset, int limit_size, int orgID)
        {
            string strSql = "";
            if (searchIn == "Grade Name")
            {
                strSql = "SELECT a.grade_id, a.grade_code_name " +
                "FROM org.org_grades a " +
                "WHERE ((a.grade_code_name ilike '" + searchWord.Replace("'", "''") +
                 "') AND (org_id = " + orgID + ")) ORDER BY a.grade_code_name LIMIT " + limit_size +
                 " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            else if (searchIn == "Grade Description")
            {
                strSql = "SELECT a.grade_id, a.grade_code_name " +
                "FROM org.org_grades a " +
                "WHERE ((a.grade_comments ilike '" + searchWord.Replace("'", "''") +
                 "') AND (org_id = " + orgID + ")) ORDER BY a.grade_code_name LIMIT " + limit_size +
                 " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            Global.mnFrm.grd_SQL = strSql;
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static long get_Total_Grades(string searchWord, string searchIn, int orgID)
        {
            string strSql = "";
            if (searchIn == "Grade Name")
            {
                strSql = "SELECT count(1) " +
                "FROM org.org_grades a " +
                "WHERE ((a.grade_code_name ilike '" + searchWord.Replace("'", "''") +
                 "') AND (org_id = " + orgID + "))";
            }
            else if (searchIn == "Grade Description")
            {
                strSql = "SELECT count(1)  " +
                "FROM org.org_grades a " +
                "WHERE ((a.grade_comments ilike '" + searchWord.Replace("'", "''") +
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

        public static string get_Grd_Rec_Hstry(int grdID)
        {
            string strSQL = @"SELECT a.created_by, 
to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS'), 
      a.last_update_by, 
      to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS') " +
            "FROM org.org_grades a WHERE(a.grade_id = " + grdID + ")";
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

        #region "POSITION DETAILS..."
        public static DataSet get_One_Pos_Det(int posID)
        {
            string strSql = "";
            strSql = "SELECT a.position_id, a.position_code_name, " +
             "a.position_comments, a.is_enabled, a.prnt_position_id " +
             "FROM org.org_positions a " +
             "WHERE(a.position_id = " + posID + ")";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            //Global.mnFrm.grd_SQL = strSql;
            return dtst;
        }

        public static DataSet get_Basic_Pos(string searchWord, string searchIn,
         Int64 offset, int limit_size, int orgID)
        {
            string strSql = "";
            if (searchIn == "Position Name")
            {
                strSql = "SELECT a.position_id, a.position_code_name " +
                "FROM org.org_positions a " +
                "WHERE ((a.position_code_name ilike '" + searchWord.Replace("'", "''") +
                 "') AND (org_id = " + orgID + ")) ORDER BY a.position_code_name LIMIT " + limit_size +
                 " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            else if (searchIn == "Position Description")
            {
                strSql = "SELECT a.position_id, a.position_code_name " +
                "FROM org.org_positions a " +
                "WHERE ((a.position_comments ilike '" + searchWord.Replace("'", "''") +
                 "') AND (org_id = " + orgID + ")) ORDER BY a.position_code_name LIMIT " + limit_size +
                 " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            Global.mnFrm.pos_SQL = strSql;
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static long get_Total_Pos(string searchWord, string searchIn, int orgID)
        {
            string strSql = "";
            if (searchIn == "Position Name")
            {
                strSql = "SELECT count(1) " +
                "FROM org.org_positions a " +
                "WHERE ((a.position_code_name ilike '" + searchWord.Replace("'", "''") +
                 "') AND (org_id = " + orgID + "))";
            }
            else if (searchIn == "Position Description")
            {
                strSql = "SELECT count(1)  " +
                "FROM org.org_positions a " +
                "WHERE ((a.position_comments ilike '" + searchWord.Replace("'", "''") +
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

        public static string get_Pos_Rec_Hstry(int posID)
        {
            string strSQL = @"SELECT a.created_by, 
to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS'), 
      a.last_update_by, 
      to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS') " +
            "FROM org.org_positions a WHERE(a.position_id = " + posID + ")";
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

        #region "BENEFITS & CONTRIBUTIONS DETAILS..."
        public static void updateItmVal(long pssblvalid, long itmid, double amnt, string sqlFormula,
        string valNm)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE org.org_pay_items_values " +
            "SET item_id=" + itmid + ", pssbl_amount=" + amnt +
             ", pssbl_value_sql='" + sqlFormula.Replace("'", "''") + "', " +
                "last_update_by=" + Global.myOrgStp.user_id + ", last_update_date='" + dateStr + "', " +
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
                "last_update_by=" + Global.myOrgStp.user_id + ", last_update_date='" + dateStr + "', " +
                "scale_factor=" + scaleFctr +
          " WHERE feed_id = " + feedid;
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void clearTakeHomes()
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE org.org_pay_items SET is_take_home_pay = '0', last_update_by = " + Global.myOrgStp.user_id + ", " +
                     "last_update_date = '" + dateStr + "' " +
             "WHERE (is_take_home_pay = '1')";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void updateItm(int orgid, long itmid, string itnm, string itmDesc,
         string itmMajTyp, string itmMinTyp, string itmUOMTyp,
         bool useSQL, bool isenbld, int costAcnt, int balsAcnt,
            string freqncy, string locClass, int priorty,
            string inc_dc_cost, string inc_dc_bals, string balstyp, int itmMnID)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE org.org_pay_items " +
            "SET item_code_name='" + itnm.Replace("'", "''") + "', item_desc='" + itmDesc.Replace("'", "''") +
              "', item_maj_type='" + itmMajTyp.Replace("'", "''") + "', item_min_type='" + itmMinTyp.Replace("'", "''") +
              "', item_value_uom='" + itmUOMTyp.Replace("'", "''") + "', uses_sql_formulas='" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(useSQL) +
              "', cost_accnt_id=" + costAcnt +
              ", bals_accnt_id=" + balsAcnt + ", " +
                    "is_enabled='" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(isenbld) +
              "', org_id=" + orgid +
              ", last_update_by=" + Global.myOrgStp.user_id +
                              ", last_update_date='" + dateStr +
                              "', pay_frequency = '" + freqncy.Replace("'", "''") +
                              "', local_classfctn = '" + locClass.Replace("'", "''") +
                              "', pay_run_priority = " + priorty + ", incrs_dcrs_cost_acnt ='" + inc_dc_cost.Replace("'", "''") +
            "', incrs_dcrs_bals_acnt='" + inc_dc_bals.Replace("'", "''") + "', balance_type='" + balstyp.Replace("'", "''") + "', report_line_no= " + itmMnID +
        " WHERE item_id=" + itmid;
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void createItm(int orgid, string itnm, string itmDesc,
         string itmMajTyp, string itmMinTyp, string itmUOMTyp,
         bool useSQL, bool isenbld, int costAcnt, int balsAcnt,
            string freqncy, string locClass, int priorty,
            string inc_dc_cost, string inc_dc_bals, string balstyp, int itmMnID)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = "INSERT INTO org.org_pay_items(" +
                     "item_code_name, item_desc, item_maj_type, item_min_type, " +
                     "item_value_uom, uses_sql_formulas, cost_accnt_id, bals_accnt_id, " +
                     "is_enabled, org_id, created_by, creation_date, last_update_by, " +
                     "last_update_date, pay_frequency, local_classfctn, pay_run_priority, " +
                     "incrs_dcrs_cost_acnt, incrs_dcrs_bals_acnt, balance_type, report_line_no) " +
             "VALUES ('" + itnm.Replace("'", "''") + "', '" + itmDesc.Replace("'", "''") +
             "', '" + itmMajTyp.Replace("'", "''") + "', '" + itmMinTyp.Replace("'", "''") +
             "', '" + itmUOMTyp.Replace("'", "''") + "', '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(useSQL) + "', " + costAcnt +
             ", " + balsAcnt + ", '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(isenbld) +
             "', " + orgid + ", " + Global.myOrgStp.user_id + ", '" + dateStr + "', " + Global.myOrgStp.user_id +
             ", '" + dateStr + "', '" + freqncy.Replace("'", "''") + "', '" + locClass.Replace("'", "''") +
             "', " + priorty + ",'" + inc_dc_cost.Replace("'", "''") + "','" +
             inc_dc_bals.Replace("'", "''") + "','" + balstyp.Replace("'", "''") + "', " + itmMnID + ")";
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
             ", '" + sqlFormula.Replace("'", "''") + "', " + Global.myOrgStp.user_id + ", '" + dateStr + "', " +
                     Global.myOrgStp.user_id + ", '" + dateStr + "', '" + valNm.Replace("'", "''") + "')";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void createItmFeed(long itmid, long balsItmID, string addSub, double scaleFctr)
        {
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = "INSERT INTO org.org_pay_itm_feeds(" +
                   "balance_item_id, fed_by_itm_id, adds_subtracts, created_by, " +
                   "creation_date, last_update_by, last_update_date, scale_factor) " +
             "VALUES (" + balsItmID + ", " + itmid +
             ", '" + addSub.Replace("'", "''") + "', " + Global.myOrgStp.user_id + ", '" + dateStr + "', " +
                     Global.myOrgStp.user_id + ", '" + dateStr + "', " + scaleFctr + ")";
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
                         "a.pay_frequency, a.local_classfctn, a.pay_run_priority, a.incrs_dcrs_cost_acnt, a.incrs_dcrs_bals_acnt, a.balance_type " +
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
               "') AND (org_id = " + orgID + ")) ORDER BY a.pay_run_priority LIMIT " + limit_size +
               " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            else if (searchIn == "Item Description")
            {
                strSql = "SELECT a.item_id, a.item_code_name, a.item_maj_type " +
              "FROM org.org_pay_items a " +
              "WHERE ((a.item_desc ilike '" + searchWord.Replace("'", "''") +
                    "') AND (org_id = " + orgID + ")) ORDER BY a.pay_run_priority LIMIT " + limit_size +
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

        #region "WORK HOUR DETAILS..."
        public static DataSet get_One_Wkhr_Det(long wkhID)
        {
            string strSql = "";
            strSql = "SELECT a.day_of_week, " +
             "a.dflt_nrml_start_time, a.dflt_nrml_close_time, a.dflt_row_id " +
             "FROM org.org_wrkn_hrs_details a " +
             "WHERE(a.work_hours_id = " + wkhID + ") ORDER BY day_of_wk_no";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            Global.mnFrm.wkhDet_SQL = strSql;
            return dtst;
        }

        public static DataSet get_Basic_Wkhr(string searchWord, string searchIn,
         Int64 offset, int limit_size, int orgID)
        {
            string strSql = "";
            if (searchIn == "Work Hour Name")
            {
                strSql = "SELECT a.work_hours_id, a.work_hours_name, a.work_hours_desc, a.is_enabled " +
                "FROM org.org_wrkn_hrs a " +
                "WHERE ((a.work_hours_name ilike '" + searchWord.Replace("'", "''") +
                 "') AND (org_id = " + orgID + ")) ORDER BY a.work_hours_name LIMIT " + limit_size +
                 " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            else if (searchIn == "Work Hour Description")
            {
                strSql = "SELECT a.work_hours_id, a.work_hours_name, a.work_hours_desc, a.is_enabled " +
                "FROM org.org_wrkn_hrs a " +
                "WHERE ((a.work_hours_desc ilike '" + searchWord.Replace("'", "''") +
                 "') AND (org_id = " + orgID + ")) ORDER BY a.work_hours_name LIMIT " + limit_size +
                 " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            Global.mnFrm.wkh_SQL = strSql;
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static long get_Total_Wkhr(string searchWord, string searchIn, int orgID)
        {
            string strSql = "";
            if (searchIn == "Work Hour Name")
            {
                strSql = "SELECT count(1) " +
                "FROM org.org_wrkn_hrs a " +
                "WHERE ((a.work_hours_name ilike '" + searchWord.Replace("'", "''") +
                 "') AND (org_id = " + orgID + "))";
            }
            else if (searchIn == "Work Hour Description")
            {
                strSql = "SELECT count(1)  " +
                "FROM org.org_wrkn_hrs a " +
                "WHERE ((a.work_hours_desc ilike '" + searchWord.Replace("'", "''") +
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

        public static string get_Wkhr_Rec_Hstry(long wkhID)
        {
            string strSQL = @"SELECT a.created_by, 
to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS'), 
      a.last_update_by, 
      to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS') " +
            "FROM org.org_wrkn_hrs a WHERE(a.work_hours_id = " + wkhID + ")";
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

        public static string get_WkhrDt_Rec_Hstry(long detID)
        {
            string strSQL = @"SELECT a.created_by, 
to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS'), 
      a.last_update_by, 
      to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS') " +
            "FROM org.org_wrkn_hrs_details a WHERE(a.dflt_row_id = " + detID + ")";
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

        #region "GATHERING TYPES DETAILS..."
        public static DataSet get_One_Gth_Det(int gthID)
        {
            string strSql = "";
            strSql = "SELECT a.gthrng_typ_id, a.gthrng_typ_name, " +
             "a.gthrng_typ_desc, a.is_enabled, a.gath_start_time, a.gath_end_time " +
             "FROM org.org_gthrng_types a " +
             "WHERE(a.gthrng_typ_id = " + gthID + ")";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static DataSet get_Basic_Gth(string searchWord, string searchIn,
         Int64 offset, int limit_size, int orgID)
        {
            string strSql = "";
            if (searchIn == "Gathering Name")
            {
                strSql = "SELECT a.gthrng_typ_id, a.gthrng_typ_name, a.gath_start_time, a.gath_end_time " +
                "FROM org.org_gthrng_types a " +
                "WHERE ((a.gthrng_typ_name ilike '" + searchWord.Replace("'", "''") +
                 "') AND (org_id = " + orgID + ")) ORDER BY a.gthrng_typ_name DESC LIMIT " + limit_size +
                 " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            else if (searchIn == "Gathering Description")
            {
                strSql = "SELECT a.gthrng_typ_id, a.gthrng_typ_name, a.gath_start_time, a.gath_end_time " +
                "FROM org.org_gthrng_types a " +
                "WHERE ((a.gthrng_typ_desc ilike '" + searchWord.Replace("'", "''") +
                 "') AND (org_id = " + orgID + ")) ORDER BY a.gthrng_typ_desc LIMIT " + limit_size +
                 " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            Global.mnFrm.gth_SQL = strSql;
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static long get_Total_Gth(string searchWord, string searchIn, int orgID)
        {
            string strSql = "";
            if (searchIn == "Gathering Name")
            {
                strSql = "SELECT count(1) " +
                "FROM org.org_gthrng_types a " +
                "WHERE ((a.gthrng_typ_name ilike '" + searchWord.Replace("'", "''") +
                 "') AND (org_id = " + orgID + "))";
            }
            else if (searchIn == "Gathering Description")
            {
                strSql = "SELECT count(1)  " +
                "FROM org.org_gthrng_types a " +
                "WHERE ((a.gthrng_typ_desc ilike '" + searchWord.Replace("'", "''") +
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

        public static string get_Gth_Rec_Hstry(int gthID)
        {
            string strSQL = @"SELECT a.created_by, 
to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS'), 
      a.last_update_by, 
      to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS') " +
            "FROM org.org_gthrng_types a WHERE(a.gthrng_typ_id = " + gthID + ")";
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

        #region "SEGMENT VALUES..."
        public static int getAcctTypID(string accntTyp)
        {
            if (accntTyp == "A")
            {
                return 1;
            }
            else if (accntTyp == "L")
            {
                return 2;
            }
            else if (accntTyp == "EQ")
            {
                return 3;
            }
            else if (accntTyp == "R")
            {
                return 4;
            }
            else if (accntTyp == "EX")
            {
                return 5;
            }
            return -1;
        }


        public static void clearChrtRetEarns(int segmentID)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string updtSQL = "UPDATE org.org_segment_values " +
            "SET is_retained_earnings='0' WHERE segment_id = " + segmentID;
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void clearChrtNetIncome(int segmntID)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string updtSQL = "UPDATE org.org_segment_values " +
            "SET is_net_income='0' WHERE segment_id = " + segmntID;
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void clearChrtSuspns(int segmntID)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string updtSQL = "UPDATE org.org_segment_values " +
            "SET is_suspens_accnt='0' WHERE segment_id = " + segmntID;
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }


        public static void createSgmntVal(int orgid, int segmentID, string segmentVal, string segmentDesc,
    string allwdGrpTyp, string allwdGrpVal, bool isEnbld, int prntSegmentID,
    bool isContra, string accntType, bool isParent, bool isRetainedErngs,
    bool isNetIncome, int accntTypID, int reportLineNo, bool hsSubLdgrs,
    int contrlAcntID, int crncyID, bool isSuspenseAcnt, string acntClsfctn, int mappedAcntID)
        {
            if (isRetainedErngs == true)
            {
                Global.clearChrtRetEarns(segmentID);
            }
            if (isNetIncome == true)
            {
                Global.clearChrtNetIncome(segmentID);
            }
            if (isSuspenseAcnt == true)
            {
                Global.clearChrtSuspns(segmentID);
            }
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string insSQL = @"INSERT INTO org.org_segment_values(
            segment_id, segment_value, segment_description, 
            allwd_group_type, allwd_group_value, is_enabled, prnt_segment_value_id, 
            created_by, creation_date, last_update_by, last_update_date, 
            org_id, is_contra, accnt_type, is_prnt_accnt, is_retained_earnings, 
            is_net_income, accnt_typ_id, report_line_no, has_sub_ledgers, 
            control_account_id, crncy_id, is_suspens_accnt, account_clsfctn, 
            mapped_grp_accnt_id) " +
                  "VALUES (" + segmentID +
                  ",'" + segmentVal.Replace("'", "''") +
                  "', '" + segmentDesc.Replace("'", "''") +
                  "', '" + allwdGrpTyp.Replace("'", "''") +
                  "', '" + allwdGrpVal.Replace("'", "''") +
                  "', '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(isEnbld) +
                  "', " + prntSegmentID +
                  ", " + Global.myOrgStp.user_id +
                  ", '" + dateStr +
                  "', " + Global.myOrgStp.user_id +
                  ", '" + dateStr +
                  "', " + orgid +
                  ", '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(isContra) +
                  "', '" + accntType.Replace("'", "''") +
                  "', '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(isParent) +
                  "', '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(isRetainedErngs) +
                  "', '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(isNetIncome) +
                  "', " + accntTypID +
                  ", " + reportLineNo +
                  ", '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(hsSubLdgrs) +
                  "', " + contrlAcntID +
                  ", " + crncyID +
                  ", '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(isSuspenseAcnt) +
                  "', '" + acntClsfctn.Replace("'", "''") +
                  "', " + mappedAcntID +
                  ")";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void updateSgmntVal(int segmentValID, string segmentVal, string segmentDesc,
    string allwdGrpTyp, string allwdGrpVal, bool isEnbld, int prntSegmentID,
    bool isContra, string accntType, bool isParent, bool isRetainedErngs,
    bool isNetIncome, int accntTypID, int reportLineNo, bool hsSubLdgrs,
    int contrlAcntID, int crncyID, bool isSuspenseAcnt, string acntClsfctn, int mappedAcntID, int segmnetID)
        {
            if (isRetainedErngs == true)
            {
                Global.clearChrtRetEarns(segmnetID);
            }
            if (isNetIncome == true)
            {
                Global.clearChrtNetIncome(segmnetID);
            }
            if (isSuspenseAcnt == true)
            {
                Global.clearChrtSuspns(segmnetID);
            }
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = @"UPDATE org.org_segment_values
       SET segment_value ='" + segmentVal.Replace("'", "''") +
       "', segment_description ='" + segmentDesc.Replace("'", "''") +
       "', allwd_group_type ='" + allwdGrpTyp.Replace("'", "''") +
       "', allwd_group_value ='" + allwdGrpVal.Replace("'", "''") +
       "', is_enabled ='" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(isEnbld) +
       "', prnt_segment_value_id =" + prntSegmentID +
       ", created_by =" + Global.myOrgStp.user_id +
       ", creation_date ='" + dateStr +
       "', last_update_by =" + Global.myOrgStp.user_id +
       ", last_update_date ='" + dateStr +
       "', is_contra ='" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(isContra) +
       "', accnt_type ='" + accntType.Replace("'", "''") +
       "', is_prnt_accnt ='" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(isParent) +
       "', is_retained_earnings ='" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(isRetainedErngs) +
       "', is_net_income ='" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(isNetIncome) +
       "', accnt_typ_id =" + accntTypID +
       ", report_line_no =" + reportLineNo +
       ", has_sub_ledgers ='" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(hsSubLdgrs) +
       "', control_account_id =" + contrlAcntID +
       ", crncy_id =" + crncyID +
       ", is_suspens_accnt ='" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(isSuspenseAcnt) +
       "', account_clsfctn ='" + acntClsfctn.Replace("'", "''") +
       "', mapped_grp_accnt_id =" + mappedAcntID +
       " WHERE (segment_value_id =" + segmentValID + ")";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static void deleteSgmntVal(long segmentValID, string segmentValDesc)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Segment Value = " + segmentValDesc;
            string delSQL = "DELETE FROM org.org_segment_values WHERE segment_value_id = " + segmentValID;
            Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
        }

        public static void updateSegmentVal(int segmentValID, int segmentNum, int accntID)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE accb.accb_chart_of_accnts SET accnt_seg" + segmentNum + "_val_id = " + segmentValID +
                ", last_update_by =" + Global.myOrgStp.user_id +
                   ", last_update_date ='" + dateStr +
                   "' WHERE accnt_id = " + accntID;
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
        }

        public static bool isSgmntValInUse(int segmentValID, int segmentNum)
        {
            string strSql = "SELECT a.accnt_id " +
             "FROM accb.accb_chart_of_accnts a " +
             "WHERE(a.accnt_seg" + segmentNum + "_val_id = " + segmentValID + ")";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            strSql = "SELECT a.segment_value_id " +
             "FROM org.org_segment_values a " +
             "WHERE(a.prnt_segment_value_id= " + segmentValID + " or a.control_account_id= " + segmentValID + ")";
            dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public static int getSgmntValID(string segmentVal, int segmentID)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select segment_value_id from org.org_segment_values where lower(segment_value) = '" +
             segmentVal.Replace("'", "''").ToLower() + "' and segment_id = " + segmentID;
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

        public static int getSgmntValDescID(string segmentVal, int segmentID)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "select segment_value_id from org.org_segment_values where lower(segment_description) = '" +
             segmentVal.Replace("'", "''").ToLower() + "' and segment_id = " + segmentID;
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

        public static DataSet get_One_SgmntVals(string searchWord, string searchIn,
     Int64 offset, int limit_size, int segmentID)
        {
            string strSql = @"SELECT a.segment_value_id, a.segment_id, a.segment_value, a.segment_description, 
       a.allwd_group_type, a.allwd_group_value, a.is_enabled, a.prnt_segment_value_id, 
       a.created_by, a.creation_date, a.last_update_by, a.last_update_date, 
       a.org_id, a.is_contra, a.accnt_type, a.is_prnt_accnt, a.is_retained_earnings, 
       a.is_net_income, a.accnt_typ_id, a.report_line_no, a.has_sub_ledgers, 
       a.control_account_id, a.crncy_id, a.is_suspens_accnt, a.account_clsfctn, 
       a.mapped_grp_accnt_id, b.segment_number
  FROM org.org_segment_values a, org.org_acnt_sgmnts b " +
             "WHERE(a.segment_id = b.segment_id and b.segment_id = " + segmentID + ") ORDER BY a.segment_value LIMIT " + limit_size +
              " OFFSET " + (Math.Abs(offset * limit_size)).ToString(); ;

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            //Global.taxFrm.rec_SQL = strSql;
            return dtst;
        }

        public static DataSet get_One_SgmntValDet(int segmentValID)
        {
            string strSql = @"SELECT a.segment_value_id, a.segment_id, a.segment_value, a.segment_description, 
       a.allwd_group_type, a.allwd_group_value, a.is_enabled, a.prnt_segment_value_id, 
       a.created_by, a.creation_date, a.last_update_by, a.last_update_date, 
       a.org_id, a.is_contra, a.accnt_type, a.is_prnt_accnt, a.is_retained_earnings, 
       a.is_net_income, a.accnt_typ_id, a.report_line_no, a.has_sub_ledgers, 
       a.control_account_id, a.crncy_id, a.is_suspens_accnt, a.account_clsfctn, 
       a.mapped_grp_accnt_id, b.segment_number
  FROM org.org_segment_values a, org.org_acnt_sgmnts b " +
             "WHERE(a.segment_id = b.segment_id and a.segment_value_id = " + segmentValID + ")";

            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            //Global.taxFrm.rec_SQL = strSql;
            return dtst;
        }

        public static DataSet get_Basic_SgmntVals(string searchWord, string searchIn,
     Int64 offset, int limit_size, int segmentID)
        {
            string strSql = "";
            string whrcls = " AND (a.segment_value ilike '" + searchWord.Replace("'", "''") +
               "' or a.segment_description ilike '" + searchWord.Replace("'", "''") +
               "')";
            string subSql = @"SELECT segment_value_id,segment_value,segment_description,space||segment_value||'.'||segment_description account_number_name, is_prnt_accnt, accnt_type,accnt_typ_id, prnt_segment_value_id, control_account_id, depth, path, cycle 
      FROM suborg WHERE 1=1 ORDER BY accnt_typ_id, path";

            strSql = @"WITH RECURSIVE suborg(segment_value_id, segment_value, segment_description, is_prnt_accnt, accnt_type, accnt_typ_id, prnt_segment_value_id, control_account_id, depth, path, cycle, space) AS 
      ( 
      SELECT a.segment_value_id, a.segment_value, a.segment_description, a.is_prnt_accnt, a.accnt_type,a.accnt_typ_id, a.prnt_segment_value_id, a.control_account_id, 1, ARRAY[a.segment_value||'']::character varying[], false, '' opad 
      FROM org.org_segment_values a 
        WHERE ((CASE WHEN a.prnt_segment_value_id<=0 THEN a.control_account_id ELSE a.prnt_segment_value_id END)=-1 AND (a.segment_id = " + segmentID + ")" + whrcls + @") 
      UNION ALL        
      SELECT a.segment_value_id, a.segment_value, a.segment_description, a.is_prnt_accnt, a.accnt_type,a.accnt_typ_id, a.prnt_segment_value_id, a.control_account_id, sd.depth + 1, 
      path || a.segment_value, 
      a.segment_value = ANY(path), space || '      '
      FROM org.org_segment_values a, suborg AS sd 
      WHERE (((CASE WHEN a.prnt_segment_value_id<=0 THEN a.control_account_id ELSE a.prnt_segment_value_id END)=sd.segment_value_id AND NOT cycle) 
       AND (a.segment_id = " + segmentID + @"))) 
       " + subSql + " LIMIT " + limit_size +
              " OFFSET " + (Math.Abs(offset * limit_size)).ToString();

            Global.mnFrm.segmentValsSQL = strSql;
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            return dtst;
        }

        public static long get_Total_SgmntVals(string searchWord, string searchIn, int segmentID)
        {
            string strSql = "";
            string whrcls = " AND (a.segment_value ilike '" + searchWord.Replace("'", "''") +
               "' or a.segment_description ilike '" + searchWord.Replace("'", "''") +
               "')";
            string subSql = @"SELECT count(segment_value_id) 
      FROM suborg";

            strSql = @"WITH RECURSIVE suborg(segment_value_id, segment_value, segment_description, is_prnt_accnt, accnt_type, accnt_typ_id, prnt_segment_value_id, control_account_id, depth, path, cycle, space) AS 
      ( 
      SELECT a.segment_value_id, a.segment_value, a.segment_description, a.is_prnt_accnt, a.accnt_type,a.accnt_typ_id, a.prnt_segment_value_id, a.control_account_id, 1, ARRAY[a.segment_value||'']::character varying[], false, '' opad 
      FROM org.org_segment_values a 
        WHERE ((CASE WHEN a.prnt_segment_value_id<=0 THEN a.control_account_id ELSE a.prnt_segment_value_id END)=-1 AND (a.segment_id = " + segmentID + ")" + whrcls + @") 
      UNION ALL        
      SELECT a.segment_value_id, a.segment_value, a.segment_description, a.is_prnt_accnt, a.accnt_type,a.accnt_typ_id, a.prnt_segment_value_id, a.control_account_id, sd.depth + 1, 
      path || a.segment_value, 
      a.segment_value = ANY(path), space || '      '
      FROM org.org_segment_values a, suborg AS sd 
      WHERE (((CASE WHEN a.prnt_segment_value_id<=0 THEN a.control_account_id ELSE a.prnt_segment_value_id END)=sd.segment_value_id AND NOT cycle) 
       AND (a.segment_id = " + segmentID + @"))) 
       " + subSql;

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

        public static double get_SgmntValAcntBals(int segmentValID, int segmentNum)
        {
            string strSql = "SELECT sum(net_balance) " +
      "FROM accb.accb_chart_of_accnts a " +
      "WHERE (a.accnt_seg" + segmentNum + "_val_id = " + segmentValID + ")";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return double.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }
        #endregion
        #endregion

        #region "CUSTOM FUNCTIONS..."
        public static void refreshRqrdVrbls()
        {
            Global.mnFrm.cmCde.DefaultPrvldgs = Global.dfltPrvldgs;
            Global.mnFrm.cmCde.SubGrpNames = Global.subGrpNames;
            Global.mnFrm.cmCde.MainTableNames = Global.mainTableNames;
            Global.mnFrm.cmCde.KeyColumnNames = Global.keyColumnNames;
            //Global.mnFrm.cmCde.Login_number = Global.myOrgStp.login_number;
            Global.mnFrm.cmCde.ModuleAdtTbl = Global.myOrgStp.full_audit_trail_tbl_name;
            Global.mnFrm.cmCde.ModuleDesc = Global.myOrgStp.mdl_description;
            Global.mnFrm.cmCde.ModuleName = Global.myOrgStp.name;
            //Global.mnFrm.cmCde.pgSqlConn = Global.myOrgStp.Host.globalSQLConn;
            //Global.mnFrm.cmCde.Role_Set_IDs = Global.myOrgStp.role_set_id;
            Global.mnFrm.cmCde.SampleRole = "Organization Setup Administrator";
            //Global.mnFrm.cmCde.User_id = Global.myOrgStp.user_id;
            //Global.mnFrm.cmCde.Org_id = Global.myOrgStp.org_id;
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            Global.myOrgStp.user_id = Global.mnFrm.usr_id;
            Global.myOrgStp.login_number = Global.mnFrm.lgn_num;
            Global.myOrgStp.role_set_id = Global.mnFrm.role_st_id;
            Global.myOrgStp.org_id = Global.mnFrm.Og_id;

        }

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
                  orgid + ", " + Global.myOrgStp.user_id + ", '" + dateStr +
                  "', " + Global.myOrgStp.user_id + ", '" + dateStr +
                  "',-1)";
            Global.mnFrm.cmCde.insertDataNoParams(insSQL);
        }

        public static void updtOrgAccntCurrID(int orgID, int crncyID)
        {
            Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
            Global.mnFrm.cmCde.ignorAdtTrail = true;
            string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
            string updtSQL = "UPDATE accb.accb_chart_of_accnts SET crncy_id = " + crncyID +
                              ", last_update_by = " + Global.myOrgStp.user_id + ", " +
                              "last_update_date = '" + dateStr + "' " +
              "WHERE (org_id = " + orgID + " and crncy_id<=0)";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
            updtSQL = @"UPDATE accb.accb_trnsctn_details SET dbt_or_crdt='C' WHERE dbt_or_crdt='U' and dbt_amount=0 and crdt_amount !=0;
UPDATE accb.accb_trnsctn_details SET dbt_or_crdt='D' WHERE dbt_or_crdt='U' and dbt_amount!=0 and crdt_amount =0;";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
            updtSQL = @"UPDATE accb.accb_trnsctn_details SET entered_amnt=dbt_amount, accnt_crncy_amnt=dbt_amount WHERE dbt_amount!=0 and crdt_amount =0 and entered_amnt=0 and accnt_crncy_amnt=0;
UPDATE accb.accb_trnsctn_details SET entered_amnt=crdt_amount, accnt_crncy_amnt=crdt_amount WHERE dbt_amount=0 and crdt_amount!=0 and entered_amnt=0 and accnt_crncy_amnt=0";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
            updtSQL = @"UPDATE accb.accb_trnsctn_details SET entered_amt_crncy_id=func_cur_id WHERE entered_amt_crncy_id=-1;
UPDATE accb.accb_trnsctn_details SET accnt_crncy_id=func_cur_id WHERE accnt_crncy_id=-1";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
            updtSQL = @"UPDATE prs.prsn_names_nos SET org_id=" + orgID + " WHERE org_id=-1";
            Global.mnFrm.cmCde.updateDataNoParams(updtSQL);

            Global.mnFrm.cmCde.ignorAdtTrail = false;

        }

        #endregion
    }
}
