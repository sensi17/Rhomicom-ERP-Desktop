using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BasicPersonData.Forms;
using System.Windows.Forms;
using CommonCode;
using Microsoft.VisualBasic.Devices;

namespace BasicPersonData.Classes
{
  /// <summary>
  /// A  class containing variables and 
  /// functions we will like to call directly from 
  /// anywhere in the project without creating an instance first
  /// </summary>
  class Global
  {
    #region "GLOBAL DECLARATIONS..."
    public static BasicPersonData myPrsn = new BasicPersonData();
    public static mainForm mnFrm = null;
    public static string[] dfltPrvldgs = {"View Person", "View Basic Person Data", 
		/*2*/ "View Curriculum Vitae", "View Basic Person Assignments", 
    /*4*/ "View Person Pay Item Assignments", "View SQL", "View Record History",
    /*7*/ "Add Person Info","Edit Person Info","Delete Person Info",
    /*10*/"Add Basic Assignments", "Edit Basic Assignments", "Delete Basic Assignments",
    /*13*/"Add Pay Item Assignments", "Edit Pay Item Assignments", "Delete Pay Item Assignments","View Banks",
    /*17*/"Define Assignment Templates", "Edit Assignment Templates", "Delete Assignment Templates",
    /*20*/"View Assignment Templates"};

    public static string[] subGrpNames = { "Person Data" };
    public static string[] mainTableNames = { "prs.prsn_names_nos" };
    public static string[] keyColumnNames = { "person_id" };
    public static string currentPanel = "";
    #endregion

    #region "SQL STATEMENTS..."
    #region "INSERT STATEMENTS..."
    public static void createPrsnBasic(string frstnm, string surname, string othnm, string title
     , string loc_id, int orgid, string gender, string marsts, string dob, string pob, string birthcert,
     string resaddrs, string pstladrs, string email, string tel, string mobl, string fax, string homtwn
        , string ntlty, string imgNm, long firmID, long siteID)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      dob = DateTime.ParseExact(
            dob, "dd-MMM-yyyy",
            System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      string insSQL = "INSERT INTO prs.prsn_names_nos(" +
             "created_by, creation_date, last_update_by, last_update_date, " +
             "first_name, sur_name, other_names, title, local_id_no, org_id, " +
             "gender, marital_status, date_of_birth, place_of_birth, religion, " +
             "res_address, pstl_addrs, email, cntct_no_tel, cntct_no_mobl, " +
             "cntct_no_fax, hometown, nationality, img_location,lnkd_firm_org_id,lnkd_firm_site_id)" +
     "VALUES (" + Global.myPrsn.user_id + ", '" + dateStr + "', " +
     Global.myPrsn.user_id + ", '" + dateStr + "', '" + frstnm.Replace("'", "''") + "', " +
             "'" + surname.Replace("'", "''") + "', '" + othnm.Replace("'", "''") +
             "', '" + title.Replace("'", "''") + "', '" + loc_id.Replace("'", "''") +
             "', " + orgid + ", '" + gender.Replace("'", "''") + "', " +
             "'" + marsts.Replace("'", "''") + "', '" + dob.Replace("'", "''") +
             "', '" + pob.Replace("'", "''") + "', '" + birthcert.Replace("'", "''") +
             "', '" + resaddrs.Replace("'", "''") + "', " +
             "'" + pstladrs.Replace("'", "''") + "', '" + email.Replace("'", "''") +
             "', '" + tel.Replace("'", "''") + "', '" + mobl.Replace("'", "''") +
             "', '" + fax.Replace("'", "''") + "', '" + homtwn.Replace("'", "''") +
             "', '" + ntlty.Replace("'", "''") + "', '" + imgNm.Replace("'", "''") + "'," + firmID + "," + siteID + ")";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void createPrsnExtrData(long prsnID, string[] colData)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = @"INSERT INTO prs.prsn_extra_data(
            person_id, data_col1, data_col2, data_col3, data_col4, 
            data_col5, data_col6, data_col7, data_col8, data_col9, data_col10, 
            data_col11, data_col12, data_col13, data_col14, data_col15, data_col16, 
            data_col17, data_col18, data_col19, data_col20, data_col21, data_col22, 
            data_col23, data_col24, data_col25, data_col26, data_col27, data_col28, 
            data_col29, data_col30, data_col31, data_col32, data_col33, data_col34, 
            data_col35, data_col36, data_col37, data_col38, data_col39, data_col40, 
            data_col41, data_col42, data_col43, data_col44, data_col45, data_col46, 
            data_col47, data_col48, data_col49, data_col50, created_by, creation_date, 
            last_update_by, last_update_date)" +
     "VALUES (" + prsnID + ", '" + colData[0].Replace("'", "''") +
     "', '" + colData[1].Replace("'", "''") + "', '" + colData[2].Replace("'", "''") +
     "', '" + colData[3].Replace("'", "''") + "', '" + colData[4].Replace("'", "''") +
     "', '" + colData[5].Replace("'", "''") + "', '" + colData[6].Replace("'", "''") +
     "', '" + colData[7].Replace("'", "''") + "', '" + colData[8].Replace("'", "''") +
     "', '" + colData[9].Replace("'", "''") + "', '" + colData[10].Replace("'", "''") +
     "', '" + colData[11].Replace("'", "''") + "', '" + colData[12].Replace("'", "''") +
     "', '" + colData[13].Replace("'", "''") + "', '" + colData[14].Replace("'", "''") +
     "', '" + colData[15].Replace("'", "''") + "', '" + colData[16].Replace("'", "''") +
     "', '" + colData[17].Replace("'", "''") + "', '" + colData[18].Replace("'", "''") +
     "', '" + colData[19].Replace("'", "''") + "', '" + colData[20].Replace("'", "''") +
     "', '" + colData[21].Replace("'", "''") + "', '" + colData[22].Replace("'", "''") +
     "', '" + colData[23].Replace("'", "''") + "', '" + colData[24].Replace("'", "''") +
     "', '" + colData[25].Replace("'", "''") + "', '" + colData[26].Replace("'", "''") +
     "', '" + colData[27].Replace("'", "''") + "', '" + colData[28].Replace("'", "''") +
     "', '" + colData[29].Replace("'", "''") + "', '" + colData[30].Replace("'", "''") +
     "', '" + colData[31].Replace("'", "''") + "', '" + colData[32].Replace("'", "''") +
     "', '" + colData[33].Replace("'", "''") + "', '" + colData[34].Replace("'", "''") +
     "', '" + colData[35].Replace("'", "''") + "', '" + colData[36].Replace("'", "''") +
     "', '" + colData[37].Replace("'", "''") + "', '" + colData[38].Replace("'", "''") +
     "', '" + colData[39].Replace("'", "''") + "', '" + colData[40].Replace("'", "''") +
     "', '" + colData[41].Replace("'", "''") + "', '" + colData[42].Replace("'", "''") +
     "', '" + colData[43].Replace("'", "''") + "', '" + colData[44].Replace("'", "''") +
     "', '" + colData[45].Replace("'", "''") + "', '" + colData[46].Replace("'", "''") +
     "', '" + colData[47].Replace("'", "''") + "', '" + colData[48].Replace("'", "''") +
     "', '" + colData[49].Replace("'", "''") + "', " + Global.myPrsn.user_id +
     ", '" + dateStr + "', " + Global.myPrsn.user_id + ", '" + dateStr + "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void updatePrsnExtrData(long prsnID, string[] colData)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = @"UPDATE prs.prsn_extra_data 
   SET data_col1='" + colData[0].Replace("'", "''") +
     "', data_col2='" + colData[1].Replace("'", "''") + "', data_col3='" + colData[2].Replace("'", "''") +
     "', data_col4='" + colData[3].Replace("'", "''") + "', data_col5='" + colData[4].Replace("'", "''") +
     "', data_col6='" + colData[5].Replace("'", "''") + "', data_col7='" + colData[6].Replace("'", "''") +
     "', data_col8='" + colData[7].Replace("'", "''") + "', data_col9='" + colData[8].Replace("'", "''") +
     "', data_col10='" + colData[9].Replace("'", "''") + "', data_col11='" + colData[10].Replace("'", "''") +
     "', data_col12='" + colData[11].Replace("'", "''") + "', data_col13='" + colData[12].Replace("'", "''") +
     "', data_col14='" + colData[13].Replace("'", "''") + "', data_col15='" + colData[14].Replace("'", "''") +
     "', data_col16='" + colData[15].Replace("'", "''") + "', data_col17='" + colData[16].Replace("'", "''") +
     "', data_col18='" + colData[17].Replace("'", "''") + "', data_col19='" + colData[18].Replace("'", "''") +
     "', data_col20='" + colData[19].Replace("'", "''") + "', data_col21='" + colData[20].Replace("'", "''") +
     "', data_col22='" + colData[21].Replace("'", "''") + "', data_col23='" + colData[22].Replace("'", "''") +
     "', data_col24='" + colData[23].Replace("'", "''") + "', data_col25='" + colData[24].Replace("'", "''") +
     "', data_col26='" + colData[25].Replace("'", "''") + "', data_col27='" + colData[26].Replace("'", "''") +
     "', data_col28='" + colData[27].Replace("'", "''") + "', data_col29='" + colData[28].Replace("'", "''") +
     "', data_col30='" + colData[29].Replace("'", "''") + "', data_col31='" + colData[30].Replace("'", "''") +
     "', data_col32='" + colData[31].Replace("'", "''") + "', data_col33='" + colData[32].Replace("'", "''") +
     "', data_col34='" + colData[33].Replace("'", "''") + "', data_col35='" + colData[34].Replace("'", "''") +
     "', data_col36='" + colData[35].Replace("'", "''") + "', data_col37='" + colData[36].Replace("'", "''") +
     "', data_col38='" + colData[37].Replace("'", "''") + "', data_col39='" + colData[38].Replace("'", "''") +
     "', data_col40='" + colData[39].Replace("'", "''") + "', data_col41='" + colData[40].Replace("'", "''") +
     "', data_col42='" + colData[41].Replace("'", "''") + "', data_col43='" + colData[42].Replace("'", "''") +
     "', data_col44='" + colData[43].Replace("'", "''") + "', data_col45='" + colData[44].Replace("'", "''") +
     "', data_col46='" + colData[45].Replace("'", "''") + "', data_col47='" + colData[46].Replace("'", "''") +
     "', data_col48='" + colData[47].Replace("'", "''") + "', data_col49='" + colData[48].Replace("'", "''") +
     "', data_col50='" + colData[49].Replace("'", "''") + "', last_update_by=" + Global.myPrsn.user_id +
     ",  last_update_date='" + dateStr + "' WHERE person_id=" + prsnID;
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void createExtrDataCol(int colno, string collabel, string lovnm, string datatyp
  , string catgry, int lngth, string dsplytyp, int orgid, int tblrnumcols, int ordr,
      string csvTblColNms, bool isrqrd)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = @"INSERT INTO prs.prsn_extra_data_cols(
            column_no, column_label, attchd_lov_name, 
            column_data_type, column_data_category, data_length, data_dsply_type, 
            org_id, no_cols_tblr_dsply, col_order, csv_tblr_col_nms, created_by, creation_date, 
            last_update_by, last_update_date,is_required)" +
     "VALUES (" + colno + ", '" + collabel.Replace("'", "''") + "', '" + lovnm.Replace("'", "''") + "', " +
             "'" + datatyp.Replace("'", "''") + "', '" + catgry.Replace("'", "''") +
             "', " + lngth + ", '" + dsplytyp.Replace("'", "''") +
             "', " + orgid + ", " + tblrnumcols + ", " + ordr + ", '" + csvTblColNms.Replace("'", "''") +
             "', " + Global.myPrsn.user_id +
     ", '" + dateStr + "', " + Global.myPrsn.user_id + ", '" + dateStr +
     "', '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(isrqrd) + "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void updateExtrDataCol(int colno, string collabel, string lovnm, string datatyp
, string catgry, int lngth, string dsplytyp, int orgid, int tblrnumcols,
      long rowid, int ordr, string csvTblColNms, bool isrqrd)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = @"UPDATE prs.prsn_extra_data_cols SET 
            column_no=" + colno + ", column_label='" + collabel.Replace("'", "''") +
                      "', attchd_lov_name='" + lovnm.Replace("'", "''") +
                      "', column_data_type='" + datatyp.Replace("'", "''") +
                      "', column_data_category='" + catgry.Replace("'", "''") +
             "', data_length=" + lngth + ", data_dsply_type='" + dsplytyp.Replace("'", "''") +
             "', org_id=" + orgid + ", no_cols_tblr_dsply=" + tblrnumcols +
             ", col_order=" + ordr + ", csv_tblr_col_nms='" + csvTblColNms.Replace("'", "''") +
             "', last_update_by=" + Global.myPrsn.user_id +
              ", last_update_date='" + dateStr +
              "', is_required='" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(isrqrd) + "' WHERE extra_data_cols_id = " + rowid;
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void createPrsnsType(long prsnid, string rsn, string date1, string date2,
     string futhDet, string prsntyp)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      date1 = DateTime.ParseExact(
      date1, "dd-MMM-yyyy",
      System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

      date2 = DateTime.ParseExact(
      date2, "dd-MMM-yyyy",
      System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

      string insSQL = "INSERT INTO pasn.prsn_prsntyps(" +
               "person_id, prn_typ_asgnmnt_rsn, valid_start_date, valid_end_date, " +
               "created_by, creation_date, last_update_by, last_update_date, " +
               "further_details, prsn_type)" +
       "VALUES (" + prsnid + ", '" + rsn.Replace("'", "''") +
       "', '" + date1.Replace("'", "''") + "', '" + date2.Replace("'", "''") + "', " +
               "" + Global.myPrsn.user_id + ", '" + dateStr + "', " + Global.myPrsn.user_id + ", '" + dateStr + "', " +
               "'" + futhDet.Replace("'", "''") + "', '" + prsntyp.Replace("'", "''") + "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void createNatnlty(long prsnid, string ntnlty,
     string ntnlty_typ, string idnum, string detIssd, string expryDte, string otherinfo)
    {
      if (ntnlty.Length > 100)
      {
        ntnlty = ntnlty.Substring(0, 100);
      }
      if (idnum.Length > 100)
      {
        idnum = idnum.Substring(0, 100);
      }
      if (ntnlty_typ.Length > 100)
      {
        ntnlty_typ = ntnlty_typ.Substring(0, 100);
      }
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = "INSERT INTO prs.prsn_national_ids(" +
               "person_id, nationality, national_id_typ, id_number, created_by, " +
               @"creation_date, last_update_by, last_update_date, 
            date_issued, expiry_date, other_info) " +
       "VALUES (" + prsnid + ", '" + ntnlty.Replace("'", "''") +
       "', '" + ntnlty_typ.Replace("'", "''") + "', '" + idnum.Replace("'", "''") + "', " +
               "" + Global.myPrsn.user_id + ", '" + dateStr + "', " +
               Global.myPrsn.user_id + ", '" + dateStr +
               "','" + detIssd.Replace("'", "''") +
      "','" + expryDte.Replace("'", "''") +
      "','" + otherinfo.Replace("'", "''") + "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void updateNatnlty(long ntnlty_id, long prsnid, string ntnlty,
     string ntnlty_typ, string idnum, string detIssd, string expryDte, string otherinfo)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE prs.prsn_national_ids " +
      "SET person_id=" + prsnid + ", nationality='" + ntnlty.Replace("'", "''") +
      "', national_id_typ='" + ntnlty_typ.Replace("'", "''") + "', id_number='" + idnum.Replace("'", "''") + "', " +
      "last_update_by=" + Global.myPrsn.user_id +
      ", last_update_date='" + dateStr +
      "', date_issued='" + detIssd.Replace("'", "''") +
      "', expiry_date='" + expryDte.Replace("'", "''") +
      "', other_info='" + otherinfo.Replace("'", "''") + "' " +
      "WHERE ntnlty_id=" + ntnlty_id;
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void createRltv(long prsnid, long rltvprsnid, string rltnTyp)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = "INSERT INTO prs.prsn_relatives(" +
               "person_id, relative_prsn_id, relationship_type, created_by, creation_date, " +
               "last_update_by, last_update_date) " +
       "VALUES (" + prsnid + ", " + rltvprsnid + ", '" + rltnTyp.Replace("'", "''") +
       "', " + Global.myPrsn.user_id + ", '" + dateStr + "', " +
               Global.myPrsn.user_id + ", '" + dateStr + "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void createEduc(long prsnid, string crsnm,
   string schnm, string schloc, string certnm, string strtdte,
     string enddte, string certdte, string certype)
    {
      if (crsnm.Length > 200)
      {
        crsnm = crsnm.Substring(0, 200);
      }
      if (schnm.Length > 200)
      {
        schnm = schnm.Substring(0, 200);
      }
      if (schloc.Length > 200)
      {
        schloc = schloc.Substring(0, 200);
      }
      if (certnm.Length > 200)
      {
        certnm = certnm.Substring(0, 200);
      }
      if (certype.Length > 200)
      {
        certype = certype.Substring(0, 200);
      }
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      strtdte = DateTime.ParseExact(
strtdte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

      enddte = DateTime.ParseExact(
enddte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

      string insSQL = "INSERT INTO prs.prsn_education(" +
               "person_id, course_name, school_institution, school_location, " +
               "cert_obtained, course_start_date, course_end_date, date_cert_awarded, " +
               "created_by, creation_date, last_update_by, last_update_date, " +
               "cert_type) " +
       "VALUES (" + prsnid + ", '" + crsnm.Replace("'", "''") +
       "', '" + schnm.Replace("'", "''") + "', '" + schloc.Replace("'", "''") +
       "', '" + certnm.Replace("'", "''") + "', '" + strtdte.Replace("'", "''") +
       "', '" + enddte.Replace("'", "''") + "', '" + certdte.Replace("'", "''") +
       "', " + Global.myPrsn.user_id + ", '" + dateStr + "', " +
               Global.myPrsn.user_id + ", '" + dateStr + "', '" + certype.Replace("'", "''") + "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void createWrkExp(long prsnid, string jobnm,
  string instnm, string jobloc, string jobdesc, string strtdte,
   string enddte, string feats)
    {
      if (jobnm.Length > 200)
      {
        jobnm = jobnm.Substring(0, 200);
      }
      if (instnm.Length > 200)
      {
        instnm = instnm.Substring(0, 200);
      }
      if (jobloc.Length > 200)
      {
        jobloc = jobloc.Substring(0, 200);
      }
      if (jobdesc.Length > 300)
      {
        jobdesc = jobdesc.Substring(0, 300);
      }
      if (feats.Length > 300)
      {
        feats = feats.Substring(0, 300);
      }
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      strtdte = DateTime.ParseExact(
strtdte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

      enddte = DateTime.ParseExact(
enddte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      string insSQL = "INSERT INTO prs.prsn_work_experience(" +
               "person_id, job_name_title, institution_name, job_location, job_description, " +
               "job_start_date, job_end_date, feats_achvments, created_by, creation_date, " +
               "last_update_by, last_update_date) " +
       "VALUES (" + prsnid + ", '" + jobnm.Replace("'", "''") +
       "', '" + instnm.Replace("'", "''") + "', '" + jobloc.Replace("'", "''") +
       "', '" + jobdesc.Replace("'", "''") + "', '" + strtdte.Replace("'", "''") +
       "', '" + enddte.Replace("'", "''") + "', '" + feats.Replace("'", "''") +
       "', " + Global.myPrsn.user_id + ", '" + dateStr + "', " +
               Global.myPrsn.user_id + ", '" + dateStr + "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void createSkill(long prsnid, string langs,
  string hobbs, string intrsts, string cndct, string attde,
   string strtdte, string enddte)
    {
      if (langs.Length > 300)
      {
        langs = langs.Substring(0, 300);
      }
      if (hobbs.Length > 300)
      {
        hobbs = hobbs.Substring(0, 300);
      }
      if (intrsts.Length > 300)
      {
        intrsts = intrsts.Substring(0, 300);
      }
      if (cndct.Length > 300)
      {
        cndct = cndct.Substring(0, 300);
      }
      if (attde.Length > 300)
      {
        attde = attde.Substring(0, 300);
      }
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      strtdte = DateTime.ParseExact(
strtdte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

      enddte = DateTime.ParseExact(
enddte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      string insSQL = "INSERT INTO prs.prsn_skills_nature(" +
               "person_id, languages, hobbies, interests, conduct, attitude, " +
               "valid_start_date, valid_end_date, created_by, creation_date, " +
               "last_update_by, last_update_date) " +
       "VALUES (" + prsnid + ", '" + langs.Replace("'", "''") +
       "', '" + hobbs.Replace("'", "''") + "', '" + intrsts.Replace("'", "''") +
       "', '" + cndct.Replace("'", "''") + "', '" + attde.Replace("'", "''") +
       "', '" + strtdte.Replace("'", "''") + "', '" + enddte.Replace("'", "''") +
       "', " + Global.myPrsn.user_id + ", '" + dateStr + "', " +
               Global.myPrsn.user_id + ", '" + dateStr + "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void createDiv(long prsnid, int divid,
  string strtdte, string enddte)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      strtdte = DateTime.ParseExact(
strtdte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

      enddte = DateTime.ParseExact(
enddte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      string insSQL = "INSERT INTO pasn.prsn_divs_groups(" +
               "person_id, div_id, valid_start_date, valid_end_date, created_by, " +
               "creation_date, last_update_by, last_update_date) " +
       "VALUES (" + prsnid + ", " + divid +
       ", '" + strtdte.Replace("'", "''") + "', '" + enddte.Replace("'", "''") +
       "', " + Global.myPrsn.user_id + ", '" + dateStr + "', " +
               Global.myPrsn.user_id + ", '" + dateStr + "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void createLoc(long prsnid, int locid,
  string strtdte, string enddte)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      strtdte = DateTime.ParseExact(
strtdte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

      enddte = DateTime.ParseExact(
enddte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      string insSQL = "INSERT INTO pasn.prsn_locations(" +
               "person_id, location_id, valid_start_date, valid_end_date, created_by, " +
               "creation_date, last_update_by, last_update_date) " +
       "VALUES (" + prsnid + ", " + locid +
       ", '" + strtdte.Replace("'", "''") + "', '" + enddte.Replace("'", "''") +
       "', " + Global.myPrsn.user_id + ", '" + dateStr + "', " +
               Global.myPrsn.user_id + ", '" + dateStr + "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void createSpvsr(long prsnid, long spvsrid,
  string strtdte, string enddte)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      strtdte = DateTime.ParseExact(
strtdte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

      enddte = DateTime.ParseExact(
enddte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      string insSQL = "INSERT INTO pasn.prsn_supervisors(" +
               "person_id, supervisor_prsn_id, valid_start_date, valid_end_date, " +
               "created_by, creation_date, last_update_by, last_update_date) " +
       "VALUES (" + prsnid + ", " + spvsrid +
       ", '" + strtdte.Replace("'", "''") + "', '" + enddte.Replace("'", "''") +
       "', " + Global.myPrsn.user_id + ", '" + dateStr + "', " +
               Global.myPrsn.user_id + ", '" + dateStr + "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void createJob(long prsnid, int jobid,
  string strtdte, string enddte)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      strtdte = DateTime.ParseExact(
strtdte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

      enddte = DateTime.ParseExact(
enddte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      string insSQL = "INSERT INTO pasn.prsn_jobs(" +
               "person_id, job_id, valid_start_date, valid_end_date, created_by, " +
               "creation_date, last_update_by, last_update_date) " +
       "VALUES (" + prsnid + ", " + jobid +
       ", '" + strtdte.Replace("'", "''") + "', '" + enddte.Replace("'", "''") +
       "', " + Global.myPrsn.user_id + ", '" + dateStr + "', " +
               Global.myPrsn.user_id + ", '" + dateStr + "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void createGrade(long prsnid, int grdid,
  string strtdte, string enddte)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      strtdte = DateTime.ParseExact(
strtdte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

      enddte = DateTime.ParseExact(
enddte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      string insSQL = "INSERT INTO pasn.prsn_grades( " +
               "person_id, grade_id, valid_start_date, valid_end_date, created_by, " +
               "creation_date, last_update_by, last_update_date) " +
       "VALUES (" + prsnid + ", " + grdid +
       ", '" + strtdte.Replace("'", "''") + "', '" + enddte.Replace("'", "''") +
       "', " + Global.myPrsn.user_id + ", '" + dateStr + "', " +
               Global.myPrsn.user_id + ", '" + dateStr + "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void createPosition(long prsnid, int posid,
  string strtdte, string enddte)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      strtdte = DateTime.ParseExact(
strtdte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

      enddte = DateTime.ParseExact(
enddte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      string insSQL = "INSERT INTO pasn.prsn_positions(" +
               "person_id, position_id, valid_start_date, valid_end_date, created_by, " +
               "creation_date, last_update_by, last_update_date) " +
       "VALUES (" + prsnid + ", " + posid +
       ", '" + strtdte.Replace("'", "''") + "', '" + enddte.Replace("'", "''") +
       "', " + Global.myPrsn.user_id + ", '" + dateStr + "', " +
               Global.myPrsn.user_id + ", '" + dateStr + "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void createWkHrs(long prsnid, int wkid,
  string strtdte, string enddte)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      strtdte = DateTime.ParseExact(
strtdte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

      enddte = DateTime.ParseExact(
enddte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      string insSQL = "INSERT INTO pasn.prsn_work_id(" +
               "person_id, work_hour_id, valid_start_date, valid_end_date, created_by, " +
               "creation_date, last_update_by, last_update_date) " +
       "VALUES (" + prsnid + ", " + wkid +
       ", '" + strtdte.Replace("'", "''") + "', '" + enddte.Replace("'", "''") +
       "', " + Global.myPrsn.user_id + ", '" + dateStr + "', " +
               Global.myPrsn.user_id + ", '" + dateStr + "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void createGath(long prsnid, int gthid,
  string strtdte, string enddte)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      strtdte = DateTime.ParseExact(
strtdte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

      enddte = DateTime.ParseExact(
enddte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      string insSQL = "INSERT INTO pasn.prsn_gathering_typs(" +
               "person_id, gatherng_typ_id, valid_start_date, valid_end_date, created_by, " +
               "creation_date, last_update_by, last_update_date) " +
       "VALUES (" + prsnid + ", " + gthid +
       ", '" + strtdte.Replace("'", "''") + "', '" + enddte.Replace("'", "''") +
       "', " + Global.myPrsn.user_id + ", '" + dateStr + "', " +
               Global.myPrsn.user_id + ", '" + dateStr + "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void createTmplt(string tmpltnm,
string tmpltdesc, bool isenbl, string divids, int gradeid,
int jobid, int locid, int posid, long sprvsid, int wkhrid,
      string prsntyp, string prsntyprsn, string prsntypfuthdet,
      string gathtyps, string payitms, string payitmvals, int orgid)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = "INSERT INTO pasn.prsn_assgnmnt_tmplts(" +
                        "tmplt_name, tmplt_desc, is_enabled, created_by, creation_date, " +
                        "last_update_by, last_update_date, div_ids, grade_id, job_id, " +
                        "loc_id, pos_id, sprvsor_id, wkhr_id, prsn_typ, prsn_typ_asgn_rsn, " +
                        "prsn_typ_futh_det, gath_typ_ids, pay_item_ids, pay_item_val_ids, " +
                        "org_id) " +
        "VALUES ('" + tmpltnm.Replace("'", "''") +
        "', '" + tmpltdesc.Replace("'", "''") + "', '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(isenbl) +
        "', " + Global.myPrsn.user_id + ", '" + dateStr + "', " +
                        Global.myPrsn.user_id + ", '" + dateStr +
        "', '" + divids.Replace("'", "''") + "', " + gradeid +
        ", " + jobid + ", " + locid + ", " + posid + ", " + sprvsid + ", " + wkhrid +
        ", '" + prsntyp.Replace("'", "''") + "', '" + prsntyprsn.Replace("'", "''") +
        "', '" + prsntypfuthdet.Replace("'", "''") + "', '" + gathtyps.Replace("'", "''") +
        "', '" + payitms.Replace("'", "''") + "', '" + payitmvals.Replace("'", "''") +
        "', " + orgid + ")";

      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    #endregion

    #region "UPDATE STATEMENTS..."
    public static void updtJob(long rowid, string enddte)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      if (enddte == "")
      {
        enddte = "31-Dec-4000";
      }
      enddte = DateTime.ParseExact(
enddte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      string updtSQL = "UPDATE pasn.prsn_jobs " +
        "SET valid_end_date='" + enddte +
              "', last_update_by=" + Global.myPrsn.user_id +
              ", last_update_date='" + dateStr + "' " +
        "WHERE row_id=" + rowid;
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void updtPos(long rowid, string enddte)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      if (enddte == "")
      {
        enddte = "31-Dec-4000";
      }
      enddte = DateTime.ParseExact(
enddte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      string updtSQL = "UPDATE pasn.prsn_positions " +
        "SET valid_end_date='" + enddte +
              "', last_update_by=" + Global.myPrsn.user_id +
              ", last_update_date='" + dateStr + "' " +
        "WHERE row_id=" + rowid;
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void updtWkh(long rowid, string enddte)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      if (enddte == "")
      {
        enddte = "31-Dec-4000";
      }
      enddte = DateTime.ParseExact(
enddte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      string updtSQL = "UPDATE pasn.prsn_work_id " +
        "SET valid_end_date='" + enddte +
              "', last_update_by=" + Global.myPrsn.user_id +
              ", last_update_date='" + dateStr + "' " +
        "WHERE row_id=" + rowid;
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void updtGrade(long rowid, string enddte)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      if (enddte == "")
      {
        enddte = "31-Dec-4000";
      }
      enddte = DateTime.ParseExact(
enddte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      string updtSQL = "UPDATE pasn.prsn_grades " +
        "SET valid_end_date='" + enddte +
              "', last_update_by=" + Global.myPrsn.user_id +
              ", last_update_date='" + dateStr + "' " +
        "WHERE row_id=" + rowid;
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void updtLoc(long rowid, string enddte)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      if (enddte == "")
      {
        enddte = "31-Dec-4000";
      }
      enddte = DateTime.ParseExact(
enddte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      string updtSQL = "UPDATE pasn.prsn_locations " +
        "SET valid_end_date='" + enddte +
              "', last_update_by=" + Global.myPrsn.user_id + ", last_update_date='" + dateStr + "' " +
        "WHERE prsn_loc_id=" + rowid;
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void updtSpvsr(long rowid, string enddte)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      if (enddte == "")
      {
        enddte = "31-Dec-4000";
      }
      enddte = DateTime.ParseExact(
enddte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      string updtSQL = "UPDATE pasn.prsn_supervisors " +
        "SET valid_end_date='" + enddte +
              "', last_update_by=" + Global.myPrsn.user_id + ", last_update_date='" + dateStr + "' " +
        "WHERE row_id=" + rowid;
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void updtGath(long rowid, string enddte)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();

      enddte = DateTime.ParseExact(
enddte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      string updtSQL = "UPDATE pasn.prsn_gathering_typs " +
        "SET valid_end_date='" + enddte +
              "', last_update_by=" + Global.myPrsn.user_id + ", last_update_date='" + dateStr + "' " +
        "WHERE row_id=" + rowid;
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void updtDiv(long rowid, string enddte)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      if (enddte == "")
      {
        enddte = "31-Dec-4000";
      }
      enddte = DateTime.ParseExact(
enddte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      string updtSQL = "UPDATE pasn.prsn_divs_groups " +
        "SET valid_end_date='" + enddte +
              "', last_update_by=" + Global.myPrsn.user_id + ", last_update_date='" + dateStr + "' " +
        "WHERE prsn_div_id=" + rowid;
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void updtPrsnsType(long rowid, long prsnid, string rsn, string date2,
  string futhDet, string prsntyp)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();

      date2 = DateTime.ParseExact(
date2, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      string updtSQL = "UPDATE pasn.prsn_prsntyps " +
        "SET person_id=" + prsnid + ", prn_typ_asgnmnt_rsn='" + rsn.Replace("'", "''") +
        "', valid_end_date='" + date2.Replace("'", "''") + "', " +
        "last_update_by=" + Global.myPrsn.user_id + ", last_update_date='" + dateStr + "', " +
        "further_details='" + futhDet.Replace("'", "''") +
        "', prsn_type='" + prsntyp.Replace("'", "''") + "' " +
        "WHERE prsntype_id= " + rowid;
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void updtPrsnsType(long rowid, long prsnid, string rsn, string date1, string date2,
  string futhDet, string prsntyp)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();

      date1 = DateTime.ParseExact(
date1, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

      date2 = DateTime.ParseExact(
date2, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      string updtSQL = "UPDATE pasn.prsn_prsntyps " +
        "SET person_id=" + prsnid + ", prn_typ_asgnmnt_rsn='" + rsn.Replace("'", "''") +
        "', valid_start_date='" + date1.Replace("'", "''") + "', valid_end_date='" + date2.Replace("'", "''") + "', " +
        "last_update_by=" + Global.myPrsn.user_id + ", last_update_date='" + dateStr + "', " +
        "further_details='" + futhDet.Replace("'", "''") +
        "', prsn_type='" + prsntyp.Replace("'", "''") + "' " +
        "WHERE prsntype_id= " + rowid;
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void updateTmplt(int tmpltid, string tmpltnm,
string tmpltdesc, bool isenbl, string divids, int gradeid,
int jobid, int locid, int posid, long sprvsid, int wkhrid,
      string prsntyp, string prsntyprsn, string prsntypfuthdet,
      string gathtyps, string payitms, string payitmvals, int orgid)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE pasn.prsn_assgnmnt_tmplts " +
      "SET tmplt_name='" + tmpltnm.Replace("'", "''") +
        "', tmplt_desc='" + tmpltdesc.Replace("'", "''") + "', is_enabled='" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(isenbl) +
        "', last_update_by=" + Global.myPrsn.user_id + ", last_update_date='" + dateStr + "', div_ids='" + divids.Replace("'", "''") + "', " +
              "grade_id=" + gradeid +
        ", job_id=" + jobid + ", loc_id=" + locid + ", pos_id=" + posid + ", sprvsor_id=" + sprvsid + ", wkhr_id=" + wkhrid +
        ", prsn_typ='" + prsntyp.Replace("'", "''") + "', prsn_typ_asgn_rsn='" + prsntyprsn.Replace("'", "''") +
        "', prsn_typ_futh_det='" + prsntypfuthdet.Replace("'", "''") + "', gath_typ_ids='" + gathtyps.Replace("'", "''") +
        "', pay_item_ids='" + payitms.Replace("'", "''") + "', pay_item_val_ids='" + payitmvals.Replace("'", "''") +
        "', org_id=" + orgid + " " +
        "WHERE tmplt_id= " + tmpltid;
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void updateGath(long prsnid, long row_id, int gthid,
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
      string updtSQL = "UPDATE pasn.prsn_gathering_typs " +
          "SET person_id=" + prsnid + ", gatherng_typ_id=" + gthid +
       ", valid_start_date='" + strtdte.Replace("'", "''") +
       "', valid_end_date='" + enddte.Replace("'", "''") + "', " +
          "last_update_by=" +
               Global.myPrsn.user_id + ", last_update_date='" + dateStr + "' " +
       "WHERE row_id=" + row_id;
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void updateWkHrs(long prsnid, long row_id, int wkid,
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
      string updtSQL = "UPDATE pasn.prsn_work_id " +
          "SET person_id=" + prsnid + ", work_hour_id=" + wkid +
       ", valid_start_date='" + strtdte.Replace("'", "''") +
       "', valid_end_date='" + enddte.Replace("'", "''") + "', " +
          "last_update_by=" +
               Global.myPrsn.user_id + ", last_update_date='" + dateStr + "' " +
       "WHERE row_id=" + row_id;
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void updatePosition(long prsnid, long row_id, int posid,
  string strtdte, string enddte, int divID)
    {
      strtdte = DateTime.ParseExact(
strtdte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      if (enddte == "")
      {
        enddte = "31-Dec-4000";
      }
      enddte = DateTime.ParseExact(
enddte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE pasn.prsn_positions " +
          "SET person_id=" + prsnid + ", position_id=" + posid +
       ", valid_start_date='" + strtdte.Replace("'", "''") +
       "', valid_end_date='" + enddte.Replace("'", "''") + "', " +
       "last_update_by=" + Global.myPrsn.user_id +
       ", last_update_date='" + dateStr +
       "', div_id= " + divID +
       " WHERE row_id=" + row_id;
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void updateGrade(long prsnid, long row_id, int grdid,
  string strtdte, string enddte)
    {
      strtdte = DateTime.ParseExact(
strtdte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      if (enddte == "")
      {
        enddte = "31-Dec-4000";
      }
      enddte = DateTime.ParseExact(
enddte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE pasn.prsn_grades " +
          "SET person_id=" + prsnid + ", grade_id=" + grdid +
       ", valid_start_date='" + strtdte.Replace("'", "''") +
       "', valid_end_date='" + enddte.Replace("'", "''") + "', " +
          "last_update_by=" +
               Global.myPrsn.user_id + ", last_update_date='" + dateStr + "' " +
       "WHERE row_id=" + row_id;
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void updateJob(long prsnid, long row_id, int jobid,
  string strtdte, string enddte)
    {
      strtdte = DateTime.ParseExact(
strtdte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      if (enddte == "")
      {
        enddte = "31-Dec-4000";
      }
      enddte = DateTime.ParseExact(
enddte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE pasn.prsn_jobs " +
          "SET person_id=" + prsnid + ", job_id=" + jobid +
          ", valid_start_date='" + strtdte.Replace("'", "''") +
          "', valid_end_date='" + enddte.Replace("'", "''") +
          "', last_update_by=" + Global.myPrsn.user_id + ", last_update_date='" + dateStr + "' " +
          "WHERE row_id = " + row_id;
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void updtSpvsr(long prsnid, long row_id, int spvsrid,
   string strtdte, string enddte)
    {
      strtdte = DateTime.ParseExact(
strtdte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      if (enddte == "")
      {
        enddte = "31-Dec-4000";
      }
      enddte = DateTime.ParseExact(
enddte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE pasn.prsn_supervisors " +
          "SET person_id=" + prsnid + ", supervisor_prsn_id=" + spvsrid +
       ", valid_start_date='" + strtdte.Replace("'", "''") + "', valid_end_date='" + enddte.Replace("'", "''") +
       "', last_update_by=" + Global.myPrsn.user_id + ", last_update_date='" + dateStr + "' " +
       "WHERE row_id=" + row_id;
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void updateLoc(long prsnid,
   long row_id, int locid, string strtdte, string enddte)
    {
      strtdte = DateTime.ParseExact(
strtdte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      if (enddte == "")
      {
        enddte = "31-Dec-4000";
      }
      enddte = DateTime.ParseExact(
enddte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE pasn.prsn_locations " +
      "SET person_id=" + prsnid + ", location_id = " + locid +
       ", valid_start_date='" + strtdte.Replace("'", "''") + "', valid_end_date='" + enddte.Replace("'", "''") +
       "', last_update_by=" + Global.myPrsn.user_id + ", last_update_date='" + dateStr + "' " +
       "WHERE prsn_loc_id=" + row_id;
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void updateDiv(long prsnid,
     long prsn_divid, int divid, string strtdte, string enddte)
    {
      strtdte = DateTime.ParseExact(
strtdte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      if (enddte == "")
      {
        enddte = "31-Dec-4000";
      }
      enddte = DateTime.ParseExact(
enddte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE pasn.prsn_divs_groups " +
      "SET person_id=" + prsnid + ", div_id = " + divid +
       ", valid_start_date='" + strtdte.Replace("'", "''") + "', valid_end_date='" + enddte.Replace("'", "''") +
       "', last_update_by=" + Global.myPrsn.user_id + ", last_update_date='" + dateStr + "' " +
       "WHERE prsn_div_id=" + prsn_divid;
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void updateSkill(long prsnid, long skillid, string langs,
  string hobbs, string intrsts, string cndct, string attde,
  string strtdte, string enddte)
    {
      strtdte = DateTime.ParseExact(
strtdte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      if (enddte == "")
      {
        enddte = "31-Dec-4000";
      }
      enddte = DateTime.ParseExact(
enddte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = "UPDATE prs.prsn_skills_nature " +
          "SET person_id=" + prsnid + ", languages='" + langs.Replace("'", "''") +
       "', hobbies='" + hobbs.Replace("'", "''") + "', interests='" + intrsts.Replace("'", "''") +
       "', conduct='" + cndct.Replace("'", "''") + "', " +
          "attitude='" + attde.Replace("'", "''") +
       "', valid_start_date='" + strtdte.Replace("'", "''") + "', valid_end_date='" + enddte.Replace("'", "''") +
       "', last_update_by=" +
               Global.myPrsn.user_id + ", last_update_date='" + dateStr + "' " +
       "WHERE skills_id=" + skillid;
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void updateWrkExp(long prsnid, long wrkexpid, string jobnm,
  string instnm, string jobloc, string jobdesc, string strtdte,
  string enddte, string feats)
    {
      strtdte = DateTime.ParseExact(
strtdte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      if (enddte == "")
      {
        enddte = "31-Dec-4000";
      }
      enddte = DateTime.ParseExact(
enddte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE prs.prsn_work_experience " +
      "SET person_id=" + prsnid + ", job_name_title='" + jobnm.Replace("'", "''") +
       "', institution_name='" + instnm.Replace("'", "''") + "', job_location='" + jobloc.Replace("'", "''") +
       "', job_description='" + jobdesc.Replace("'", "''") + "', feats_achvments='" + feats.Replace("'", "''") +
       "', job_start_date='" + strtdte.Replace("'", "''") +
       "', job_end_date='" + enddte.Replace("'", "''") + "', " +
          "last_update_by=" + Global.myPrsn.user_id + ", last_update_date='" + dateStr + "' " +
    "WHERE wrk_exprnc_id=" + wrkexpid;
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void updateEduc(long educid, long prsnid, string crsnm,
   string schnm, string schloc, string certnm, string strtdte,
     string enddte, string certdte, string certype)
    {
      strtdte = DateTime.ParseExact(
strtdte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      if (enddte == "")
      {
        enddte = "31-Dec-4000";
      }
      enddte = DateTime.ParseExact(
enddte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE prs.prsn_education " +
          "SET person_id=" + prsnid + ", course_name='" + crsnm.Replace("'", "''") +
       "', school_institution='" + schnm.Replace("'", "''") + "', school_location='" + schloc.Replace("'", "''") +
       "', " +
          "cert_obtained='" + certnm.Replace("'", "''") + "', course_start_date='" + strtdte.Replace("'", "''") +
       "', course_end_date='" + enddte.Replace("'", "''") + "', date_cert_awarded='" + certdte.Replace("'", "''") +
       "', " +
          "last_update_by=" +
               Global.myPrsn.user_id + ", last_update_date='" + dateStr + "', " +
          "cert_type='" + certype.Replace("'", "''") + "' " +
       "WHERE educ_id = " + educid + "";
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void updateRltv(long rltv_id, long prsnid, long rltvprsnid, string rltnTyp)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE prs.prsn_relatives " +
             "SET person_id=" + prsnid + ", relative_prsn_id=" + rltvprsnid +
             ", relationship_type='" + rltnTyp.Replace("'", "''") +
           "', last_update_by=" + Global.myPrsn.user_id + ", last_update_date='" + dateStr + "' " +
           "WHERE rltv_id = " + rltv_id + "";
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void updatePrsnBasic(long prsnid, string frstnm, string surname, string othnm, string title
     , string loc_id, int orgid, string gender, string marsts, string dob, string pob, string birthcert,
     string resaddrs, string pstladrs, string email, string tel, string mobl, string fax, string homtwn
        , string ntlty, string imgNm, long firmID, long siteID)
    {
      dob = DateTime.ParseExact(
dob, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE prs.prsn_names_nos " +
          "SET last_update_by=" + Global.myPrsn.user_id + ", " +
          "last_update_date='" + dateStr + "', first_name='" + frstnm.Replace("'", "''") +
          "', sur_name='" + surname.Replace("'", "''") + "', other_names='" + othnm.Replace("'", "''") +
             "', " +
          "title='" + title.Replace("'", "''") + "', local_id_no='" + loc_id.Replace("'", "''") +
             "', org_id=" + orgid + ", gender='" + gender.Replace("'", "''") +
             "', marital_status='" + marsts.Replace("'", "''") + "', " +
          "date_of_birth='" + dob.Replace("'", "''") +
             "', place_of_birth='" + pob.Replace("'", "''") + "', religion='" + birthcert.Replace("'", "''") +
             "', res_address='" + resaddrs.Replace("'", "''") + "', " +
          "pstl_addrs='" + pstladrs.Replace("'", "''") + "', email='" + email.Replace("'", "''") +
             "', cntct_no_tel='" + tel.Replace("'", "''") + "', cntct_no_mobl='" + mobl.Replace("'", "''") +
             "', cntct_no_fax='" + fax.Replace("'", "''") + "', hometown='" + homtwn.Replace("'", "''") +
             "', nationality='" + ntlty.Replace("'", "''") + "', img_location='" + imgNm.Replace("'", "''") +
             "', lnkd_firm_org_id=" + firmID + ", lnkd_firm_site_id=" + siteID + " " +
          "WHERE person_id=" + prsnid;
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void endOldPrsnTypes(long prsnid, string nwStrtDte)
    {
      nwStrtDte = DateTime.ParseExact(
nwStrtDte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).AddDays(-1).ToString("yyyy-MM-dd");
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE pasn.prsn_prsntyps " +
          "SET last_update_by=" + Global.myPrsn.user_id + ", " +
          "last_update_date='" + dateStr + "', valid_end_date='" + nwStrtDte + "' " +
          "WHERE ((person_id=" + prsnid +
          ") and (to_timestamp(valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS') " +
          ">= to_timestamp('" + nwStrtDte + " 00:00:00','YYYY-MM-DD HH24:MI:SS')))";
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void updtPrsnImg(long prsnid)
    {
      if (Global.mnFrm.cmCde.myComputer.FileSystem.FileExists(
       Global.mnFrm.cmCde.getPrsnImgsDrctry() + @"\" + prsnid.ToString() + ".png"))
      {
        Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
        string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
        string sqlStr = "UPDATE prs.prsn_names_nos SET " +
        "img_location = '" + prsnid.ToString() + ".png', " +
        "last_update_by = " + Global.myPrsn.user_id +
        ", last_update_date = '" + dateStr + "' " +
        "WHERE(person_id = " + prsnid + ")";
        Global.mnFrm.cmCde.updateDataNoParams(sqlStr);
      }
    }

    public static void updtPrsnOrg(long prsnid, int orgid)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string sqlStr = "UPDATE prs.prsn_names_nos SET " +
      "org_id = " + orgid + ", " +
      "last_update_by = " + Global.myPrsn.user_id +
      ", last_update_date = '" + dateStr + "' " +
      "WHERE(person_id = " + prsnid + ")";
      Global.mnFrm.cmCde.updateDataNoParams(sqlStr);
    }

    #endregion

    #region "DELETE STATEMENTS..."
    public static void deleteNtnlty(long ntnltyrcid, string ntnltyTypNm, string locID)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Nationality Type = " + ntnltyTypNm + "; Person Loc ID No = " + locID;
      string delSQL = "DELETE FROM prs.prsn_national_ids WHERE ntnlty_id = " + ntnltyrcid;
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }

    public static void deleteRltv(long rltvrcid, string RltvNm)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Relative's Full Name = " + RltvNm;
      string delSQL = "DELETE FROM prs.prsn_relatives WHERE rltv_id = " + rltvrcid;
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }

    public static void deletePrsTyp(long prstypid, string PrsTypNm)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Person Type = " + PrsTypNm;
      string delSQL = "DELETE FROM pasn.prsn_prsntyps WHERE prsntype_id = " + prstypid;
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }

    public static void deleteTmplt(long tmpltid, string tmpltNm)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Assignment Template Name = " + tmpltNm;
      string delSQL = "DELETE FROM pasn.prsn_assgnmnt_tmplts WHERE tmplt_id = " + tmpltid;
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }

    public static void deleteEduc(long educid, string locID)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Person's Local ID = " + locID;
      string delSQL = "DELETE FROM prs.prsn_education WHERE educ_id = " + educid;
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }

    public static void deleteExtraInfo(long extInfoID, string locID)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Person's Local ID = " + locID;
      string delSQL = "DELETE FROM prs.prsn_all_other_info_table WHERE dflt_row_id = " + extInfoID;
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }

    public static void deleteWrkExp(long wrkExpid, string locID)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Person's Local ID = " + locID;
      string delSQL = "DELETE FROM prs.prsn_work_experience WHERE wrk_exprnc_id = " + wrkExpid;
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }

    public static void deleteSkill(long skllid, string locID)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Person's Local ID = " + locID;
      string delSQL = "DELETE FROM prs.prsn_skills_nature WHERE skills_id = " + skllid;
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }

    public static void deleteDiv(long prsn_divid, string locID)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Person's Local ID = " + locID;
      string delSQL = "DELETE FROM pasn.prsn_divs_groups WHERE prsn_div_id = " + prsn_divid;
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }

    public static void deleteLoc(long row_id, string locID)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Person's Local ID = " + locID;
      string delSQL = "DELETE FROM pasn.prsn_locations WHERE prsn_loc_id = " + row_id;
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }

    public static void deleteSpvsr(long row_id, string locID)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Person's Local ID = " + locID;
      string delSQL = "DELETE FROM pasn.prsn_supervisors WHERE row_id = " + row_id;
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }

    public static void deleteJob(long row_id, string locID)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Person's Local ID = " + locID;
      string delSQL = "DELETE FROM pasn.prsn_jobs WHERE row_id = " + row_id;
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }

    public static void deleteGrade(long row_id, string locID)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Person's Local ID = " + locID;
      string delSQL = "DELETE FROM pasn.prsn_grades WHERE row_id = " + row_id;
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }

    public static void deletePosition(long row_id, string locID)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Person's Local ID = " + locID;
      string delSQL = "DELETE FROM pasn.prsn_positions WHERE row_id = " + row_id;
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }

    public static void deleteWkHrs(long row_id, string locID)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Person's Local ID = " + locID;
      string delSQL = "DELETE FROM pasn.prsn_work_id WHERE row_id = " + row_id;
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }

    public static void deleteGath(long row_id, string locID)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Person's Local ID = " + locID;
      string delSQL = "DELETE FROM pasn.prsn_gathering_typs WHERE row_id = " + row_id;
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }
    #endregion

    #region "SELECT STATEMENTS..."
    #region "ORG PERSONS..."
    public static long getAttchmntID(string attchname, long hdrID)
    {
      string strSql = "";
      strSql = "SELECT a.attchmnt_id " +
  "FROM prs.prsn_doc_attchmnts a " +
      "WHERE ((a.attchmnt_desc = '" + attchname.Replace("'", "''") +
        "') AND (a.person_id = " + hdrID + "))";

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
      string insSQL = "INSERT INTO prs.prsn_doc_attchmnts(" +
            "person_id, attchmnt_desc, file_name, created_by, " +
            "creation_date, last_update_by, last_update_date) " +
                        "VALUES (" + hdrID +
                        ", '" + attchDesc.Replace("'", "''") +
                        "', '" + filNm.Replace("'", "''") +
                        "', " + Global.myPrsn.user_id + ", '" + dateStr +
                        "', " + Global.myPrsn.user_id + ", '" + dateStr + "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void updateAttachment(long attchID, long hdrID, string attchDesc,
   string filNm)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE prs.prsn_doc_attchmnts SET " +
            "person_id=" + hdrID +
                        ", attchmnt_desc='" + attchDesc.Replace("'", "''") +
                        "', file_name='" + filNm.Replace("'", "''") +
                        "', last_update_by=" + Global.myPrsn.user_id +
                        ", last_update_date='" + dateStr + "' " +
                         "WHERE attchmnt_id = " + attchID;
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void deleteAttchmnt(long attchid, string attchNm)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Attachment Name = " + attchNm;
      string delSql = "DELETE FROM prs.prsn_doc_attchmnts WHERE(attchmnt_id = " + attchid + ")";
      Global.mnFrm.cmCde.deleteDataNoParams(delSql);
    }

    public static DataSet get_Attachments(string searchWord, string searchIn,
   Int64 offset, int limit_size, long hdrID, ref string attchSQL)
    {
      string strSql = "";
      if (searchIn == "Attachment Name/Description")
      {
        strSql = "SELECT a.attchmnt_id, a.person_id, a.attchmnt_desc, a.file_name " +
      "FROM prs.prsn_doc_attchmnts a " +
      "WHERE(a.attchmnt_desc ilike '" + searchWord.Replace("'", "''") +
      "' and a.person_id = " + hdrID + ") ORDER BY a.attchmnt_id LIMIT " + limit_size +
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
      "FROM prs.prsn_doc_attchmnts a " +
      "WHERE(a.attchmnt_desc ilike '" + searchWord.Replace("'", "''") +
      "' and a.person_id = " + hdrID + ")";
      }
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      long sumRes = 0;
      if (dtst.Tables[0].Rows.Count > 0)
      {
        long.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out sumRes);
      }
      return sumRes;
    }

    public static DataSet getAllRltvs(string searchWord, string searchIn,
     Int64 offset, int limit_size, long prsnid)
    {
      string selSQL = "";
      if (searchIn == "Relationship Type")
      {
        selSQL = "SELECT a.local_id_no, trim(a.title || ' ' || a.sur_name || " +
           "', ' || a.first_name || ' ' || a.other_names) fullname, b.relationship_type, b.relative_prsn_id, b.rltv_id " +
               "FROM prs.prsn_relatives b LEFT OUTER JOIN prs.prsn_names_nos a ON b.relative_prsn_id = a.person_id WHERE ((b.person_id = " + prsnid +
               ") and (b.relationship_type ilike '" + searchWord.Replace("'", "''") +
         "')) ORDER BY a.local_id_no DESC LIMIT " + limit_size +
         " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
      }
      else if (searchIn == "Relative's Name")
      {
        selSQL = "SELECT a.local_id_no, trim(a.title || ' ' || a.sur_name || " +
           "', ' || a.first_name || ' ' || a.other_names) fullname, b.relationship_type, b.relative_prsn_id, b.rltv_id " +
               "FROM prs.prsn_relatives b LEFT OUTER JOIN prs.prsn_names_nos a ON b.relative_prsn_id = a.person_id WHERE ((b.person_id = " + prsnid +
               ") and (trim(a.title || ' ' || a.sur_name || " +
           "', ' || a.first_name || ' ' || a.other_names) ilike '" + searchWord.Replace("'", "''") +
         "')) ORDER BY a.local_id_no DESC LIMIT " + limit_size +
         " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
      }
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      Global.mnFrm.rltvs_SQL = selSQL;
      return dtst;
    }



    public static long getTotalRltvs(string searchWord, string searchIn,
      long prsnid)
    {
      string selSQL = "";
      if (searchIn == "Relationship Type")
      {
        selSQL = "SELECT count(1) " +
               "FROM prs.prsn_relatives b LEFT OUTER JOIN prs.prsn_names_nos a ON b.relative_prsn_id = a.person_id WHERE ((b.person_id = " + prsnid +
               ") and (b.relationship_type ilike '" + searchWord.Replace("'", "''") +
         "'))";
      }
      else if (searchIn == "Relative's Name")
      {
        selSQL = "SELECT count(1) " +
               "FROM prs.prsn_relatives b LEFT OUTER JOIN prs.prsn_names_nos a ON b.relative_prsn_id = a.person_id WHERE ((b.person_id = " + prsnid +
               ") and (trim(a.title || ' ' || a.sur_name || " +
           "', ' || a.first_name || ' ' || a.other_names) ilike '" + searchWord.Replace("'", "''") +
         "'))";
      }
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return 0;
      }
    }

    public static DataSet getAllPositions(long prsnid)
    {
      string selSQL = @"SELECT position_id, 
to_char(to_timestamp(valid_start_date,'YYYY-MM-DD'),'DD-Mon-YYYY'), 
to_char(to_timestamp(valid_end_date,'YYYY-MM-DD'),'DD-Mon-YYYY'), row_id, div_id " +
      "FROM pasn.prsn_positions WHERE ((person_id = " + prsnid +
@")) ORDER BY valid_end_date DESC, valid_start_date DESC, row_id DESC";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      Global.mnFrm.pos_SQL = selSQL;
      return dtst;
    }

    public static DataSet getAllGath(long prsnid)
    {
      string selSQL = @"SELECT gatherng_typ_id, to_char(to_timestamp(valid_start_date,'YYYY-MM-DD'),'DD-Mon-YYYY'), 
to_char(to_timestamp(valid_end_date,'YYYY-MM-DD'),'DD-Mon-YYYY'), row_id " +
      "FROM pasn.prsn_gathering_typs WHERE ((person_id = " + prsnid + ")) ORDER BY valid_end_date DESC, valid_start_date DESC,row_id DESC";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      Global.mnFrm.gath_SQL = selSQL;
      return dtst;
    }

    public static DataSet getAllWkHrs(long prsnid)
    {
      string selSQL = @"SELECT work_hour_id, to_char(to_timestamp(valid_start_date,'YYYY-MM-DD'),'DD-Mon-YYYY'), 
to_char(to_timestamp(valid_end_date,'YYYY-MM-DD'),'DD-Mon-YYYY'), row_id " +
      "FROM pasn.prsn_work_id WHERE ((person_id = " + prsnid + ")) ORDER BY valid_end_date DESC, valid_start_date DESC,row_id DESC";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      Global.mnFrm.wkHr_SQL = selSQL;
      return dtst;
    }

    public static DataSet getAllGrades(long prsnid)
    {
      string selSQL = @"SELECT grade_id, to_char(to_timestamp(valid_start_date,'YYYY-MM-DD'),'DD-Mon-YYYY'), 
to_char(to_timestamp(valid_end_date,'YYYY-MM-DD'),'DD-Mon-YYYY'), row_id " +
      "FROM pasn.prsn_grades WHERE ((person_id = " + prsnid + ")) ORDER BY valid_end_date DESC, valid_start_date DESC,row_id DESC";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      Global.mnFrm.grd_SQL = selSQL;
      return dtst;
    }

    public static DataSet getAllJobs(long prsnid)
    {
      string selSQL = @"SELECT job_id, to_char(to_timestamp(valid_start_date,'YYYY-MM-DD'),'DD-Mon-YYYY'), 
to_char(to_timestamp(valid_end_date,'YYYY-MM-DD'),'DD-Mon-YYYY'), row_id " +
      "FROM pasn.prsn_jobs WHERE ((person_id = " + prsnid + ")) ORDER BY valid_end_date DESC, valid_start_date DESC,row_id DESC";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      Global.mnFrm.job_SQL = selSQL;
      return dtst;
    }

    public static DataSet getAllSpvsr(long prsnid)
    {
      string selSQL = @"SELECT supervisor_prsn_id, to_char(to_timestamp(valid_start_date,'YYYY-MM-DD'),'DD-Mon-YYYY'), 
to_char(to_timestamp(valid_end_date,'YYYY-MM-DD'),'DD-Mon-YYYY'), row_id " +
      "FROM pasn.prsn_supervisors WHERE ((person_id = " + prsnid + ")) ORDER BY valid_end_date DESC, valid_start_date DESC,row_id DESC";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      Global.mnFrm.spvsr_SQL = selSQL;
      return dtst;
    }

    public static DataSet getAllSites(long prsnid)
    {
      string selSQL = @"SELECT location_id, to_char(to_timestamp(valid_start_date,'YYYY-MM-DD'),'DD-Mon-YYYY'), 
to_char(to_timestamp(valid_end_date,'YYYY-MM-DD'),'DD-Mon-YYYY'), prsn_loc_id " +
            "FROM pasn.prsn_locations WHERE ((person_id = " + prsnid +
            ")) ORDER BY valid_end_date DESC, valid_start_date DESC, prsn_loc_id DESC";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      Global.mnFrm.site_SQL = selSQL;
      return dtst;
    }

    public static DataSet getAllDivs(long prsnid)
    {
      string selSQL = @"SELECT a.div_id, 
to_char(to_timestamp(valid_start_date,'YYYY-MM-DD'),'DD-Mon-YYYY'), 
to_char(to_timestamp(valid_end_date,'YYYY-MM-DD'),'DD-Mon-YYYY'), a.prsn_div_id, 
COALESCE((select b.div_typ_id from org.org_divs_groups b where a.div_id = b.div_id),-1) div_typ " +
          "FROM pasn.prsn_divs_groups a WHERE ((a.person_id = " + prsnid +
          @")) ORDER BY valid_end_date DESC, valid_start_date DESC, a.prsn_div_id DESC";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      Global.mnFrm.div_SQL = selSQL;
      return dtst;
    }

    public static DataSet getAllSkills(long prsnid)
    {
      string selSQL = @"SELECT languages, hobbies, interests, 
       conduct, attitude, to_char(to_timestamp(valid_start_date,'YYYY-MM-DD'),'DD-Mon-YYYY'), 
to_char(to_timestamp(valid_end_date,'YYYY-MM-DD'),'DD-Mon-YYYY'), skills_id " +
            "FROM prs.prsn_skills_nature WHERE ((person_id = " + prsnid +
            ")) ORDER BY valid_end_date DESC, valid_start_date DESC, skills_id DESC";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      Global.mnFrm.skill_SQL = selSQL;
      return dtst;
    }

    public static DataSet getAllWrkExp(long prsnid)
    {
      string selSQL = @"SELECT job_name_title, institution_name, job_location, 
       to_char(to_timestamp(job_start_date,'YYYY-MM-DD'),'DD-Mon-YYYY'), 
to_char(to_timestamp(job_end_date,'YYYY-MM-DD'),'DD-Mon-YYYY'), job_description, feats_achvments, wrk_exprnc_id " +
            "FROM prs.prsn_work_experience WHERE ((person_id = " + prsnid +
            ")) ORDER BY job_end_date DESC, job_start_date DESC, wrk_exprnc_id DESC";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      Global.mnFrm.wrkExp_SQL = selSQL;
      return dtst;
    }

    public static DataSet getAllSkillsRpt(long prsnid)
    {
      string selSQL = @"SELECT languages ""Languages     "", 
        trim(hobbies || ', ' || interests, ', ') ""Hobbies/Interests     "", 
        trim(conduct || ', ' || attitude, ', ') ""Conduct/Attitude     "", 
        to_char(to_timestamp(valid_start_date,'YYYY-MM-DD'),'DD-Mon-YYYY') ""From        "", 
        REPLACE(to_char(to_timestamp(valid_end_date,'YYYY-MM-DD'),'DD-Mon-YYYY'), '31-Dec-4000','') ""To          "", skills_id mt " +
            "FROM prs.prsn_skills_nature WHERE ((person_id = " + prsnid +
            ")) ORDER BY valid_end_date DESC, valid_start_date DESC, skills_id DESC";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      //Global.mnFrm.skill_SQL = selSQL;
      return dtst;
    }

    public static DataSet getAllWrkExpRpt(long prsnid)
    {
      string selSQL = @"SELECT job_name_title ""Job Title         "", 
        institution_name || ' ' || job_location ""Institution                    "", 
       to_char(to_timestamp(job_start_date,'YYYY-MM-DD'),'DD-Mon-YYYY') "" Start Date    "", 
REPLACE(to_char(to_timestamp(job_end_date,'YYYY-MM-DD'),'DD-Mon-YYYY'), '31-Dec-4000','') "" End Date    "", 
job_description || ' ' || feats_achvments ""Remarks           "", wrk_exprnc_id mt " +
            "FROM prs.prsn_work_experience WHERE ((person_id = " + prsnid +
            ")) ORDER BY job_end_date DESC, job_start_date DESC, wrk_exprnc_id DESC";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      //Global.mnFrm.wrkExp_SQL = selSQL;
      return dtst;
    }
    public static DataSet getAllNtnltyRpt(long prsnid)
    {
      string selSQL = "SELECT nationality \" Country   \", national_id_typ \" ID Type      \", id_number \" ID Number       \", " +
"ntnlty_id mt, date_issued \" Date Issued \", expiry_date \" Expiry Date \", other_info \" Other Information     \"" +
            "FROM prs.prsn_national_ids WHERE ((person_id = " + prsnid +
            "))";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      Global.mnFrm.ntnlty_SQL = selSQL;
      return dtst;
    }
    public static DataSet getAllPrsnTypsRpt(long prsnid)
    {
      string selSQL = "SELECT distinct prsn_type \" Relationship Type \", prn_typ_asgnmnt_rsn \" Relationship Type Reason \", further_details \" Further Details   \", " +
      "to_char(to_timestamp(valid_start_date,'YYYY-MM-DD'),'DD-Mon-YYYY') \"Start Date  \", " +
"REPLACE(to_char(to_timestamp(valid_end_date,'YYYY-MM-DD'),'DD-Mon-YYYY'),'31-Dec-4000','') \"End Date    \", " +
"valid_end_date mt, valid_start_date mt " +
            "FROM pasn.prsn_prsntyps WHERE ((person_id = " + prsnid +
            ") and (now() between to_timestamp(valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
            "AND to_timestamp(valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS'))) ORDER BY valid_end_date DESC, valid_start_date DESC";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      return dtst;
    }
    public static DataSet getAllEducRpt(long prsnid)
    {
      string selSQL = @"SELECT course_name "" Course Name       "", school_institution || ' ' || school_location "" School/Institution                     "", 
       to_char(to_timestamp(course_start_date,'YYYY-MM-DD'),'DD-Mon-YYYY') mt, 
       to_char(to_timestamp(course_end_date,'YYYY-MM-DD'),'DD-Mon-YYYY') mt1, 
       cert_obtained || ' (' || cert_type || ')' "" Certificate Obtained  "", date_cert_awarded "" Date Obtained  "", educ_id mt " +
            "FROM prs.prsn_education WHERE ((person_id = " + prsnid +
            ")) ORDER BY course_end_date DESC, course_start_date DESC, educ_id DESC";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      //Global.mnFrm.educ_SQL = selSQL;
      return dtst;
    }
    public static DataSet getAllSitesRpts(long prsnid)
    {
      string selSQL = @"SELECT a.location_id mt, b.location_code_name "" Branch Name                  "", 
to_char(to_timestamp(a.valid_start_date,'YYYY-MM-DD'),'DD-Mon-YYYY') "" Start Date    "", 
REPLACE(to_char(to_timestamp(a.valid_end_date,'YYYY-MM-DD'),'DD-Mon-YYYY'),'31-Dec-4000','') "" End Date    "", 
a.prsn_loc_id mt " +
            "FROM pasn.prsn_locations a, org.org_sites_locations b WHERE ((a.location_id = b.location_id) and (a.person_id = " + prsnid +
            ")) ORDER BY a.valid_end_date DESC, a.valid_start_date DESC, a.prsn_loc_id DESC";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      //Global.mnFrm.site_SQL = selSQL;
      return dtst;
    }
    public static DataSet getAllDivsRpts(long prsnid)
    {
      string selSQL = @"SELECT org.get_div_name(a.div_id) "" Group Name                   "", 
to_char(to_timestamp(valid_start_date,'YYYY-MM-DD'),'DD-Mon-YYYY') "" Start Date    "", 
REPLACE(to_char(to_timestamp(valid_end_date,'YYYY-MM-DD'),'DD-Mon-YYYY'),'31-Dec-4000','') "" End Date    "", a.prsn_div_id mt, 
org.get_div_type(a.div_id) "" Group Type              "" " +
          "FROM pasn.prsn_divs_groups a WHERE ((person_id = " + prsnid +
            ") and (now() between to_timestamp(valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
            "AND to_timestamp(valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS'))) ORDER BY valid_end_date DESC, valid_start_date DESC";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      //Global.mnFrm.div_SQL = selSQL;
      return dtst;
    }
    public static DataSet getAllJobsRpt(long prsnid)
    {
      string selSQL = @"SELECT a.job_id mt, b.job_code_name "" Job                          "", 
to_char(to_timestamp(a.valid_start_date,'YYYY-MM-DD'),'DD-Mon-YYYY') "" Start Date    "", 
REPLACE(to_char(to_timestamp(a.valid_end_date,'YYYY-MM-DD'),'DD-Mon-YYYY'),'31-Dec-4000','') "" End Date    "", row_id mt1 " +
      "FROM pasn.prsn_jobs a, org.org_jobs b WHERE ((a.job_id = b.job_id) and (a.person_id = " + prsnid +
      ")) ORDER BY a.valid_end_date DESC, a.valid_start_date DESC,a.row_id DESC";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      //Global.mnFrm.job_SQL = selSQL;
      return dtst;
    }
    public static DataSet getAllGradesRpt(long prsnid)
    {
      string selSQL = @"SELECT a.grade_id mt, b.grade_code_name "" Grade                        "",
to_char(to_timestamp(a.valid_start_date, 'YYYY-MM-DD'),'DD-Mon-YYYY') "" Start Date    "", 
REPLACE(to_char(to_timestamp(a.valid_end_date,'YYYY-MM-DD'),'DD-Mon-YYYY'),'31-Dec-4000','') "" End Date    "", row_id mt1 " +
      "FROM pasn.prsn_grades a, org.org_grades b WHERE ((a.grade_id = b.grade_id) and (a.person_id = " + prsnid +
      ")) ORDER BY a.valid_end_date DESC, a.valid_start_date DESC,a.row_id DESC";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      //Global.mnFrm.grd_SQL = selSQL;
      return dtst;
    }
    public static DataSet getAllPositionsRpt(long prsnid)
    {
      string selSQL = @"SELECT a.position_id mt, b.position_code_name "" Position                     "",
to_char(to_timestamp(a.valid_start_date,'YYYY-MM-DD'),'DD-Mon-YYYY') "" Start Date    "", 
REPLACE(to_char(to_timestamp(a.valid_end_date,'YYYY-MM-DD'),'DD-Mon-YYYY'),'31-Dec-4000','') "" End Date    "", a.row_id mt1, a.div_id mt2 " +
      "FROM pasn.prsn_positions a, org.org_positions b WHERE ((a.position_id = b.position_id) and (a.person_id = " + prsnid +
  @")) ORDER BY a.valid_end_date DESC, a.valid_start_date DESC, a.row_id DESC";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      //Global.mnFrm.pos_SQL = selSQL;
      return dtst;
    }
    public static DataSet getAllRltvsRpt(long prsnid)
    {
      string selSQL = "";
      selSQL = "SELECT a.local_id_no \" Relative's ID No. \", trim(a.title || ' ' || a.sur_name || " +
          "', ' || a.first_name || ' ' || a.other_names) \" Relative's Full Name                 \", " +
          "b.relationship_type \" Relation Type           \", b.relative_prsn_id mt, b.rltv_id mt " +
              " FROM prs.prsn_relatives b LEFT OUTER JOIN prs.prsn_names_nos a ON b.relative_prsn_id = a.person_id WHERE ((b.person_id = " + prsnid +
              ")) ORDER BY a.local_id_no DESC ";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      //Global.mnFrm.rltvs_SQL = selSQL;
      return dtst;
    }
    public static DataSet get_Prs_Names_NosRpt(long prsnID)
    {
      string selSQL = "SELECT person_id mt, local_id_no \"ID No.\", " +
          "title \"Title\", first_name \"First Name\", sur_name \"Surname\", other_names \"Other Names\", " +
          "gender \"Gender\", marital_status \"Marital Status\",  " +
          "to_char(to_timestamp(date_of_birth,'YYYY-MM-DD'),'DD-Mon-YYYY') \"Date of Birth\",  " +
          "place_of_birth \"Place of Birth\", hometown \"Hometown\", nationality \"Nationality\", religion \"Religion\", " +
          "REPLACE(scm.get_cstmr_splr_name(lnkd_firm_org_id)||' (' || scm.get_cstmr_splr_site_name(lnkd_firm_site_id) || " +
          "')',' ()','') \"Linked Firm/ Workplace \", " +
          "res_address \"Residential Address\", pstl_addrs \"Postal Address\", email \"Email\", " +
          "cntct_no_tel \"Tel\", cntct_no_mobl \"Mobile\", " +
          "cntct_no_fax \"Fax\", img_location mt " +//org.get_org_name(org_id) \"Organisation\"
          "FROM prs.prsn_names_nos WHERE person_id = " + prsnID;
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      Global.mnFrm.prsDet_SQL = selSQL;
      return dtst;
    }

    public static DataSet getAllEduc(long prsnid)
    {
      string selSQL = @"SELECT course_name, school_institution, school_location, 
       to_char(to_timestamp(course_start_date,'YYYY-MM-DD'),'DD-Mon-YYYY'), 
to_char(to_timestamp(course_end_date,'YYYY-MM-DD'),'DD-Mon-YYYY'), cert_obtained, cert_type, date_cert_awarded, educ_id " +
            "FROM prs.prsn_education WHERE ((person_id = " + prsnid +
            ")) ORDER BY course_end_date DESC, course_start_date DESC, educ_id DESC";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      Global.mnFrm.educ_SQL = selSQL;
      return dtst;
    }

    public static DataSet getAllNtnlty(long prsnid)
    {
      string selSQL = @"SELECT nationality, national_id_typ, id_number, 
ntnlty_id, date_issued, expiry_date, other_info " +
            "FROM prs.prsn_national_ids WHERE ((person_id = " + prsnid +
            "))";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      Global.mnFrm.ntnlty_SQL = selSQL;
      return dtst;
    }

    public static DataSet getAllPrsnTyps(long prsnid)
    {
      string selSQL = @"SELECT prsn_type, prn_typ_asgnmnt_rsn, further_details, 
      to_char(to_timestamp(valid_start_date,'YYYY-MM-DD'),'DD-Mon-YYYY'), 
to_char(to_timestamp(valid_end_date,'YYYY-MM-DD'),'DD-Mon-YYYY'), prsntype_id " +
            "FROM pasn.prsn_prsntyps WHERE ((person_id = " + prsnid +
            ")) ORDER BY valid_end_date DESC, valid_start_date DESC";
      Global.mnFrm.prsntyp_SQL = selSQL;
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);

      return dtst;
    }

    public static DataSet getLatestPrsnType(long prsnid)
    {
      string selSQL = @"SELECT prsn_type, prn_typ_asgnmnt_rsn, further_details, 
to_char(to_timestamp(valid_start_date,'YYYY-MM-DD'),'DD-Mon-YYYY'), 
to_char(to_timestamp(valid_end_date,'YYYY-MM-DD'),'DD-Mon-YYYY') " +
"FROM pasn.prsn_prsntyps WHERE ((person_id = " + prsnid +
")) ORDER BY valid_end_date DESC, valid_start_date DESC LIMIT 1 OFFSET 0";

      /* and (now() between to_timestamp(valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
            "AND to_timestamp(valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS'))*/
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      return dtst;
    }

    public static bool isPrsnTypeActive(string dtastr, long prsntypeid)
    {
      string selSQL = @"SELECT prsntype_id " +
            "FROM pasn.prsn_prsntyps WHERE ((prsntype_id = " + prsntypeid +
            ") and (to_timestamp('" + dtastr + "','YYYY-MM-DD HH24:MI:SS') between to_timestamp(valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
            "AND to_timestamp(valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS')))";
      //MessageBox.Show(selSQL);
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return true;
      }

      return false;
    }

    public static bool checkPrsnType(long prsnid, string prsntyp, string nwStrtDte, ref long rowID)
    {
      /*string rsn,
     string futhDet,  and (prn_typ_asgnmnt_rsn = '" + rsn.Replace("'", "''") +
            "') and (further_details ='" + futhDet.Replace("'", "''") +
            "')*/
      nwStrtDte = DateTime.ParseExact(
nwStrtDte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd") + " 00:00:00"; ;

      string selSQL = "SELECT prsntype_id " +
            "FROM pasn.prsn_prsntyps WHERE ((person_id = " + prsnid +
            ") and (((prsn_type = '" + prsntyp.Replace("'", "''") +
            "') and (to_timestamp(valid_start_date || ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
          ">= to_timestamp('" + nwStrtDte + "','YYYY-MM-DD HH24:MI:SS'))) or (to_timestamp(valid_start_date || ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
          "= to_timestamp('" + nwStrtDte + "','YYYY-MM-DD HH24:MI:SS'))))";
      //Global.mnFrm.cmCde.showSQLNoPermsn(selSQL);
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        rowID = long.Parse(dtst.Tables[0].Rows[0][0].ToString());
        return true;
      }
      else
      {
        return false;
      }
    }

    public static long doesPrsnHvType(long prsnid, string prsntyp, string nwStrtDte)
    {
      /*string rsn,
  string futhDet,*/
      nwStrtDte = DateTime.ParseExact(
nwStrtDte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

      string selSQL = "SELECT prsntype_id " +
                  "FROM pasn.prsn_prsntyps WHERE ((person_id = " + prsnid +
            ") and (prsn_type = '" + prsntyp.Replace("'", "''") +
            "') and (to_timestamp(valid_start_date,'YYYY-MM-DD HH24:MI:SS') " +
          ">= to_timestamp('" + nwStrtDte + "','YYYY-MM-DD HH24:MI:SS')))";

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

    public static DataSet get_Prs_Names_Nos(long prsnID)
    {
      string selSQL = "SELECT person_id, local_id_no, " +
          "title, first_name, sur_name, other_names, org_id, " +
          @"gender, marital_status, 
          to_char(to_timestamp(date_of_birth,'YYYY-MM-DD'),'DD-Mon-YYYY'), 
          place_of_birth, religion, " +
          "res_address, pstl_addrs, email, cntct_no_tel, cntct_no_mobl, " +
          @"cntct_no_fax, img_location, hometown, nationality, 
          lnkd_firm_org_id, scm.get_cstmr_splr_name(lnkd_firm_org_id), 
          lnkd_firm_site_id, scm.get_cstmr_splr_site_name(lnkd_firm_site_id) " +
          "FROM prs.prsn_names_nos WHERE person_id = " + prsnID;
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      Global.mnFrm.prsDet_SQL = selSQL;
      return dtst;
    }

    public static DataSet get_PrsExtrDataCols(int orgID)
    {
      string selSQL = @"SELECT extra_data_cols_id, column_no, column_label, attchd_lov_name, 
       column_data_type, column_data_category, data_length, CASE WHEN data_dsply_type='T' THEN 'Tabular' ELSE 'Detail' END, 
       org_id, no_cols_tblr_dsply, col_order, csv_tblr_col_nms, is_required 
        FROM prs.prsn_extra_data_cols 
        WHERE org_id = " + orgID + " ORDER BY column_no";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      return dtst;
    }

    public static DataSet get_PrsExtrDataCols(int orgID, int lmtsze)
    {
      string selSQL = @"SELECT extra_data_cols_id, column_no, column_label, attchd_lov_name, 
       column_data_type, column_data_category, data_length, CASE WHEN data_dsply_type='T' THEN 'Tabular' ELSE 'Detail' END, 
       org_id, no_cols_tblr_dsply, col_order, csv_tblr_col_nms, is_required 
        FROM prs.prsn_extra_data_cols 
        WHERE org_id = " + orgID + " ORDER BY column_no LIMIT " + lmtsze + " OFFSET 0";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      return dtst;
    }
    public static DataSet get_PrsExtrData(long prsnID)
    {
      string selSQL = @"SELECT person_id, data_col1, data_col2, data_col3, data_col4, 
       data_col5, data_col6, data_col7, data_col8, data_col9, data_col10, 
       data_col11, data_col12, data_col13, data_col14, data_col15, data_col16, 
       data_col17, data_col18, data_col19, data_col20, data_col21, data_col22, 
       data_col23, data_col24, data_col25, data_col26, data_col27, data_col28, 
       data_col29, data_col30, data_col31, data_col32, data_col33, data_col34, 
       data_col35, data_col36, data_col37, data_col38, data_col39, data_col40, 
       data_col41, data_col42, data_col43, data_col44, data_col45, data_col46, 
       data_col47, data_col48, data_col49, data_col50, extra_data_id 
  FROM prs.prsn_extra_data 
        WHERE person_id = " + prsnID + " ";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      return dtst;
    }

    public static DataSet get_AllPrsExtrData(int OrgID, int lmtsze)
    {
      string selSQL = @"SELECT a.person_id, b.local_id_no, trim(b.title || ' ' || b.sur_name || 
       ', ' || b.first_name || ' ' || b.other_names) fullname, a.data_col1, a.data_col2, a.data_col3, a.data_col4, 
       a.data_col5, a.data_col6, a.data_col7, a.data_col8, a.data_col9, a.data_col10, 
       a.data_col11, a.data_col12, a.data_col13, a.data_col14, a.data_col15, a.data_col16, 
       a.data_col17, a.data_col18, a.data_col19, a.data_col20, a.data_col21, a.data_col22, 
       a.data_col23, a.data_col24, a.data_col25, a.data_col26, a.data_col27, a.data_col28, 
       a.data_col29, a.data_col30, a.data_col31, a.data_col32, a.data_col33, a.data_col34, 
       a.data_col35, a.data_col36, a.data_col37, a.data_col38, a.data_col39, a.data_col40, 
       a.data_col41, a.data_col42, a.data_col43, a.data_col44, a.data_col45, a.data_col46, 
       a.data_col47, a.data_col48, a.data_col49, a.data_col50, a.extra_data_id 
  FROM prs.prsn_names_nos b LEFT OUTER JOIN prs.prsn_extra_data a ON (a.person_id = b.person_id)
        WHERE b.org_id = " + OrgID + " ORDER BY b.local_id_no LIMIT " + lmtsze + " OFFSET 0";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      return dtst;
    }

    public static string get_OnePrsExtrData(string colNm, long prsnID)
    {
      string selSQL = "SELECT " + colNm + " FROM prs.prsn_extra_data WHERE person_id = " + prsnID + " ";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return dtst.Tables[0].Rows[0][0].ToString();
      }
      return "";
    }

    public static DataSet get_PrsExtrDataGrpCols(string grpnm, int orgID)
    {
      string selSQL = @"SELECT extra_data_cols_id, column_no, column_label, attchd_lov_name, 
       column_data_type, column_data_category, data_length, 
CASE WHEN data_dsply_type='T' THEN 'Tabular' ELSE 'Detail' END, 
       org_id, no_cols_tblr_dsply, col_order, csv_tblr_col_nms 
        FROM prs.prsn_extra_data_cols 
        WHERE column_data_category= '" + grpnm.Replace("'", "''") +
       "' and org_id = " + orgID + " and column_label !='' ORDER BY col_order, column_no, extra_data_cols_id";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      return dtst;
    }

    public static string get_PrsExtrDataPrpty(string prprtyNm, int colno, int orgID)
    {
      string selSQL = "SELECT " + prprtyNm +
        @" FROM prs.prsn_extra_data_cols 
        WHERE column_no= " + colno + " and org_id = " + orgID;
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return dtst.Tables[0].Rows[0][0].ToString();
      }
      return "";
    }

    public static DataSet get_PrsExtrDataGrps(int orgID)
    {
      string selSQL = @"SELECT column_data_category, MIN(extra_data_cols_id) , MIN(col_order)  
        FROM prs.prsn_extra_data_cols 
        WHERE org_id = " + orgID + "  and column_label !='' GROUP BY column_data_category ORDER BY 3, 2, 1";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      return dtst;
    }

    public static string computePrsnAge(string dateOB)
    {
      string strSql = "";
      strSql = "SELECT extract('years' from age(now(), to_timestamp('" + dateOB + "', 'DD-Mon-YYYY'))) || ' yr(s) ' " +
        "|| extract('months' from age(now(), to_timestamp('" + dateOB + "', 'DD-Mon-YYYY'))) || ' mon(s) ' " +
        "|| extract('days' from age(now(), to_timestamp('" + dateOB + "', 'DD-Mon-YYYY'))) || ' day(s) ' ";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams1(strSql);
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

    public static string getAllwdPrsnTyps()
    {
      string selSQL = @"select a.pssbl_value_desc from gst.gen_stp_lov_values a, gst.gen_stp_lov_names b, sec.sec_roles c
WHERE a.value_list_id = b.value_list_id and a.pssbl_value = c.role_name 
and b.value_list_name = 'Allowed Person Types for Roles' and a.is_enabled='1' 
and c.role_id IN (" + Global.concatCurRoleIDs() + ") ORDER BY a.pssbl_value_id LIMIT 1 OFFSET 0";
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

    public static DataSet get_Org_Persons(string searchWord, string searchIn,
     Int64 offset, int limit_size, int orgID, bool searchAll, string sortBy
      , string prsnTyp, string fltrTyp)
    {
      string extra1 = "";
      string extra2 = "";
      string extra3 = "";
      string aldPrsTyp = Global.getAllwdPrsnTyps();
      char[] t = { '\'' };
      aldPrsTyp = "'" + aldPrsTyp.Trim(t) + "'";
      if (aldPrsTyp != "'All'")
      {
        extra3 = @" and ((SELECT z.prsn_type FROM pasn.prsn_prsntyps z WHERE (z.person_id = a.person_id) 
ORDER BY z.valid_end_date DESC, z.valid_start_date DESC LIMIT 1 OFFSET 0) IN (" + aldPrsTyp + "))";
      }

      if (searchAll == true)
      {
        extra1 = "or 1 = 1";
      }
      if (prsnTyp == "All")
      {
        extra2 = " and 1 = 1";
      }
      else
      {
        if (fltrTyp == "Relation Type")
        {
          extra2 = @" and ((SELECT z.prsn_type FROM pasn.prsn_prsntyps z WHERE (z.person_id = a.person_id) 
ORDER BY z.valid_end_date DESC, z.valid_start_date DESC LIMIT 1 OFFSET 0)='" + prsnTyp + "')";
        }
        else if (fltrTyp == "Division/Group")
        {
          extra2 = @" and (EXISTS(SELECT w.div_code_name FROM pasn.prsn_divs_groups z, org.org_divs_groups w 
WHERE (z.person_id = a.person_id and w.div_id = z.div_id and w.div_code_name='" + prsnTyp + @"'  
and now() between to_timestamp(z.valid_start_date,'YYYY-MM-DD HH24:MI:SS') and 
to_timestamp(z.valid_end_date,'YYYY-MM-DD HH24:MI:SS'))))";
        }
        else if (fltrTyp == "Job")
        {
          extra2 = @" and (EXISTS(SELECT w.job_code_name FROM pasn.prsn_jobs z, org.org_jobs w 
WHERE (z.person_id = a.person_id and w.job_id = z.job_id and w.job_code_name='" + prsnTyp + @"'  
and now() between to_timestamp(z.valid_start_date,'YYYY-MM-DD HH24:MI:SS') and 
to_timestamp(z.valid_end_date,'YYYY-MM-DD HH24:MI:SS'))))";
        }
        else if (fltrTyp == "Grade")
        {
          extra2 = @" and (EXISTS(SELECT w.grade_code_name FROM pasn.prsn_grades z, org.org_grades w 
WHERE (z.person_id = a.person_id and w.grade_id = z.grade_id and w.grade_code_name='" + prsnTyp + @"'  
and now() between to_timestamp(z.valid_start_date,'YYYY-MM-DD HH24:MI:SS') and 
to_timestamp(z.valid_end_date,'YYYY-MM-DD HH24:MI:SS'))))";
        }
        else if (fltrTyp == "Position")
        {
          extra2 = @" and (EXISTS(SELECT w.position_code_name FROM pasn.prsn_positions z, org.org_positions w 
WHERE (z.person_id = a.person_id and w.position_id = z.position_id and w.position_code_name='" + prsnTyp + @"'  
and now() between to_timestamp(z.valid_start_date,'YYYY-MM-DD HH24:MI:SS') and 
to_timestamp(z.valid_end_date,'YYYY-MM-DD HH24:MI:SS'))))";
        }
      }
      string strSql = "";
      string whrcls = "";
      string ordrBy = "";
      if (searchIn == "ID")
      {
        whrcls = " AND (a.local_id_no ilike '" + searchWord.Replace("'", "''") +
         "')";
      }
      else if (searchIn == "Full Name")
      {
        whrcls = " AND (trim(a.title || ' ' || a.sur_name || " +
         "', ' || a.first_name || ' ' || a.other_names) ilike '" + searchWord.Replace("'", "''") +
    "')";
      }
      else if (searchIn == "Residential Address")
      {
        whrcls = " AND (a.res_address ilike '" + searchWord.Replace("'", "''") +
         "')";
      }
      else if (searchIn == "Contact Information")
      {
        whrcls = " AND (a.pstl_addrs ilike '" + searchWord.Replace("'", "''") +
         "' or a.email ilike '" + searchWord.Replace("'", "''") +
         "' or a.cntct_no_tel ilike '" + searchWord.Replace("'", "''") +
         "' or a.cntct_no_mobl ilike '" + searchWord.Replace("'", "''") +
         "' or a.cntct_no_fax ilike '" + searchWord.Replace("'", "''") +
         "')";
      }
      else if (searchIn == "Linked Firm/Workplace")
      {
        whrcls = " AND (scm.get_cstmr_splr_name(a.lnkd_firm_org_id) ilike '" + searchWord.Replace("'", "''") +
         "' or scm.get_cstmr_splr_site_name(a.lnkd_firm_site_id) ilike '" + searchWord.Replace("'", "''") +
         "')";
      }
      else if (searchIn == "Person Type")
      {
        whrcls = " AND ((Select g.prsn_type || ' ' || g.prn_typ_asgnmnt_rsn || ' ' || g.further_details from pasn.prsn_prsntyps g where g.person_id=a.person_id ORDER BY g.valid_start_date DESC LIMIT 1 OFFSET 0) ilike '" + searchWord.Replace("'", "''") +
         "')";
      }
      else if (searchIn == "Date of Birth")
      {
        whrcls = " AND (to_char(to_timestamp(a.date_of_birth,'YYYY-MM-DD'),'DD-Mon-YYYY') ilike '" + searchWord.Replace("'", "''") +
         "')";
      }
      else if (searchIn == "Home Town")
      {
        whrcls = " AND (a.hometown ilike '" + searchWord.Replace("'", "''") +
         "')";
      }
      else if (searchIn == "Gender")
      {
        whrcls = " AND (a.gender ilike '" + searchWord.Replace("'", "''") +
         "')";
      }
      else if (searchIn == "Marital Status")
      {
        whrcls = " AND (a.marital_status ilike '" + searchWord.Replace("'", "''") +
         "')";
      }

      if (sortBy == "Date Added DESC")
      {
        ordrBy = "a.creation_date DESC";
      }
      else if (sortBy == "Date of Birth")
      {
        ordrBy = "a.date_of_birth ASC";
      }
      else if (sortBy == "Full Name")
      {
        ordrBy = "trim(a.sur_name || " +
       "', ' || a.first_name || ' ' || a.other_names) ASC";
      }
      else if (sortBy == "ID ASC")
      {
        ordrBy = "a.local_id_no ASC";
      }
      else if (sortBy == "ID DESC")
      {
        ordrBy = "a.local_id_no DESC";
      }

      strSql = "SELECT a.person_id, a.local_id_no, trim(a.title || ' ' || a.sur_name || " +
       "', ' || a.first_name || ' ' || a.other_names) fullname, img_location " +
      "FROM prs.prsn_names_nos a " +
      "WHERE ((a.org_id = " + orgID + " " + extra1 + ")" + whrcls + extra2 + extra3 +
      ") ORDER BY " + ordrBy + " LIMIT " + limit_size +
       " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
      Global.mnFrm.prs_SQL = strSql;
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      return dtst;
    }

    public static long get_Total_OrgPrs(string searchWord,
     string searchIn, int orgID, bool searchAll
      , string prsnTyp, string fltrTyp)
    {
      string extra1 = "";
      string extra2 = "";
      string extra3 = "";
      string aldPrsTyp = Global.getAllwdPrsnTyps();
      char[] t = { '\'' };
      aldPrsTyp = "'" + aldPrsTyp.Trim(t) + "'";
      if (aldPrsTyp != "'All'")
      {
        extra3 = @" and ((SELECT z.prsn_type FROM pasn.prsn_prsntyps z WHERE (z.person_id = a.person_id) 
ORDER BY z.valid_end_date DESC, z.valid_start_date DESC LIMIT 1 OFFSET 0) IN (" + aldPrsTyp + "))";
      }
      if (searchAll == true)
      {
        extra1 = "or 1 = 1";
      }
      if (prsnTyp == "All")
      {
        extra2 = " and 1 = 1";
      }
      else
      {
        if (fltrTyp == "Relation Type")
        {
          extra2 = @" and ((SELECT z.prsn_type FROM pasn.prsn_prsntyps z WHERE (z.person_id = a.person_id) 
ORDER BY z.valid_end_date DESC, z.valid_start_date DESC LIMIT 1 OFFSET 0)='" + prsnTyp + "')";
        }
        else if (fltrTyp == "Division/Group")
        {
          extra2 = @" and (EXISTS(SELECT w.div_code_name FROM pasn.prsn_divs_groups z, org.org_divs_groups w 
WHERE (z.person_id = a.person_id and w.div_id = z.div_id and w.div_code_name='" + prsnTyp + @"'  
and now() between to_timestamp(z.valid_start_date,'YYYY-MM-DD HH24:MI:SS') and 
to_timestamp(z.valid_end_date,'YYYY-MM-DD HH24:MI:SS'))))";
        }
        else if (fltrTyp == "Job")
        {
          extra2 = @" and (EXISTS(SELECT w.job_code_name FROM pasn.prsn_jobs z, org.org_jobs w 
WHERE (z.person_id = a.person_id and w.job_id = z.job_id and w.job_code_name='" + prsnTyp + @"'  
and now() between to_timestamp(z.valid_start_date,'YYYY-MM-DD HH24:MI:SS') and 
to_timestamp(z.valid_end_date,'YYYY-MM-DD HH24:MI:SS'))))";
        }
        else if (fltrTyp == "Grade")
        {
          extra2 = @" and (EXISTS(SELECT w.grade_code_name FROM pasn.prsn_grades z, org.org_grades w 
WHERE (z.person_id = a.person_id and w.grade_id = z.grade_id and w.grade_code_name='" + prsnTyp + @"'  
and now() between to_timestamp(z.valid_start_date,'YYYY-MM-DD HH24:MI:SS') and 
to_timestamp(z.valid_end_date,'YYYY-MM-DD HH24:MI:SS'))))";
        }
        else if (fltrTyp == "Position")
        {
          extra2 = @" and (EXISTS(SELECT w.position_code_name FROM pasn.prsn_positions z, org.org_positions w 
WHERE (z.person_id = a.person_id and w.position_id = z.position_id and w.position_code_name='" + prsnTyp + @"'  
and now() between to_timestamp(z.valid_start_date,'YYYY-MM-DD HH24:MI:SS') and 
to_timestamp(z.valid_end_date,'YYYY-MM-DD HH24:MI:SS'))))";
        }
      }
      string whrcls = "";
      string strSql = "";
      if (searchIn == "ID")
      {
        whrcls = " AND (a.local_id_no ilike '" + searchWord.Replace("'", "''") +
         "')";
      }
      else if (searchIn == "Full Name")
      {
        whrcls = " AND (trim(a.title || ' ' || a.sur_name || " +
         "', ' || a.first_name || ' ' || a.other_names) ilike '" + searchWord.Replace("'", "''") +
    "')";
      }
      else if (searchIn == "Residential Address")
      {
        whrcls = " AND (a.res_address ilike '" + searchWord.Replace("'", "''") +
         "')";
      }
      else if (searchIn == "Contact Information")
      {
        whrcls = " AND (a.pstl_addrs ilike '" + searchWord.Replace("'", "''") +
         "' or a.email ilike '" + searchWord.Replace("'", "''") +
         "' or a.cntct_no_tel ilike '" + searchWord.Replace("'", "''") +
         "' or a.cntct_no_mobl ilike '" + searchWord.Replace("'", "''") +
         "' or a.cntct_no_fax ilike '" + searchWord.Replace("'", "''") +
         "')";
      }
      else if (searchIn == "Linked Firm/Workplace")
      {
        whrcls = " AND (scm.get_cstmr_splr_name(a.lnkd_firm_org_id) ilike '" + searchWord.Replace("'", "''") +
         "' or scm.get_cstmr_splr_site_name(a.lnkd_firm_site_id) ilike '" + searchWord.Replace("'", "''") +
         "')";
      }
      else if (searchIn == "Person Type")
      {
        whrcls = " AND ((Select g.prsn_type || ' ' || g.prn_typ_asgnmnt_rsn || ' ' || g.further_details from pasn.prsn_prsntyps g where g.person_id=a.person_id ORDER BY g.valid_start_date DESC LIMIT 1 OFFSET 0) ilike '" + searchWord.Replace("'", "''") +
         "')";
      }
      else if (searchIn == "Date of Birth")
      {
        whrcls = " AND (to_char(to_timestamp(a.date_of_birth,'YYYY-MM-DD'),'DD-Mon-YYYY') ilike '" + searchWord.Replace("'", "''") +
         "')";
      }
      else if (searchIn == "Home Town")
      {
        whrcls = " AND (a.hometown ilike '" + searchWord.Replace("'", "''") +
         "')";
      }
      else if (searchIn == "Gender")
      {
        whrcls = " AND (a.gender ilike '" + searchWord.Replace("'", "''") +
         "')";
      }
      else if (searchIn == "Marital Status")
      {
        whrcls = " AND (a.marital_status ilike '" + searchWord.Replace("'", "''") +
         "')";
      }
      strSql = "SELECT count(1) " +
      "FROM prs.prsn_names_nos a " +
      "WHERE ((a.org_id = " + orgID + " " + extra1 + ")" + whrcls + extra2 + extra3 + ")";

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

    public static string get_Prs_Rec_Hstry(long prsID)
    {
      string strSQL = @"SELECT a.created_by, 
to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), 
a.last_update_by, 
to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') " +
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

    public static string get_Rltv_Rec_Hstry(long rlvtrcID)
    {
      string strSQL = "SELECT a.created_by, to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), a.last_update_by, to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') " +
      "FROM prs.prsn_relatives a WHERE(a.rltv_id  = " + rlvtrcID + ")";
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

    public static string get_Ntnlty_Rec_Hstry(long ntltyrcID)
    {
      string strSQL = "SELECT a.created_by, to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), a.last_update_by, to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') " +
      "FROM prs.prsn_national_ids a WHERE(a.ntnlty_id  = " + ntltyrcID + ")";
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

    public static string get_Educ_Rec_Hstry(long rowID)
    {
      string strSQL = "SELECT a.created_by, to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), a.last_update_by, to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') " +
      "FROM prs.prsn_education a WHERE(a.educ_id  = " + rowID + ")";
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

    public static string get_WrkExp_Rec_Hstry(long rowID)
    {
      string strSQL = "SELECT a.created_by, to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), a.last_update_by, to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') " +
      "FROM prs.prsn_work_experience a WHERE(a.wrk_exprnc_id  = " + rowID + ")";
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

    public static string get_Skill_Rec_Hstry(long rowID)
    {
      string strSQL = "SELECT a.created_by, to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), a.last_update_by, to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') " +
      "FROM prs.prsn_skills_nature a WHERE(a.skills_id  = " + rowID + ")";
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

    public static string get_Div_Rec_Hstry(long rowID)
    {
      string strSQL = "SELECT a.created_by, to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), a.last_update_by, to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') " +
      "FROM pasn.prsn_divs_groups a WHERE(a.prsn_div_id  = " + rowID + ")";
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

    public static string get_Site_Rec_Hstry(long rowID)
    {
      string strSQL = "SELECT a.created_by, to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), a.last_update_by, to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') " +
      "FROM pasn.prsn_locations a WHERE(a.prsn_loc_id  = " + rowID + ")";
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

    public static string get_Spvsr_Rec_Hstry(long rowID)
    {
      string strSQL = "SELECT a.created_by, to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), a.last_update_by, to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') " +
      "FROM pasn.prsn_supervisors a WHERE(a.row_id  = " + rowID + ")";
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

    public static string get_Job_Rec_Hstry(long rowID)
    {
      string strSQL = "SELECT a.created_by, to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), a.last_update_by, to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') " +
      "FROM pasn.prsn_jobs a WHERE(a.row_id  = " + rowID + ")";
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

    public static string get_Grd_Rec_Hstry(long rowID)
    {
      string strSQL = "SELECT a.created_by, to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), a.last_update_by, to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') " +
      "FROM pasn.prsn_grades a WHERE(a.row_id  = " + rowID + ")";
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

    public static string get_Pos_Rec_Hstry(long rowID)
    {
      string strSQL = "SELECT a.created_by, to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), a.last_update_by, to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') " +
      "FROM pasn.prsn_positions a WHERE(a.row_id  = " + rowID + ")";
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

    public static string get_WkHr_Rec_Hstry(long rowID)
    {
      string strSQL = "SELECT a.created_by, to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), a.last_update_by, to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') " +
      "FROM pasn.prsn_work_id a WHERE(a.row_id  = " + rowID + ")";
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

    public static string get_Gath_Rec_Hstry(long rowID)
    {
      string strSQL = "SELECT a.created_by, to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), a.last_update_by, to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') " +
      "FROM pasn.prsn_gathering_typs a WHERE(a.row_id  = " + rowID + ")";
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

    public static string get_OthInf_Rec_Hstry(long rowID, string tblnm)
    {
      string strSQL = "SELECT a.created_by, to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), a.last_update_by, to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') " +
      "FROM " + tblnm + " a WHERE(a.dflt_row_id  = " + rowID + ")";
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

    #region "PERSON PAY ITEMS AND BANKS"
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
       "', " + Global.myPrsn.user_id + ", '" + dateStr + "', " +
               Global.myPrsn.user_id + ", '" + dateStr + "', " + prsnid +
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
       "', " + Global.myPrsn.user_id + ", '" + dateStr + "', " +
               Global.myPrsn.user_id + ", '" + dateStr + "')";
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
               Global.myPrsn.user_id + ", last_update_date='" + dateStr + "' " +
       "WHERE row_id=" + rowid;
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
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
      //Global.mnFrm.idet_SQL = strSql;
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
                        Global.myPrsn.user_id + ", last_update_date='" + dateStr + "' " +
        "WHERE row_id=" + rowid;
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void updateItmValsPrs(long rowid, long itm_val_id, string enddte)
    {

      enddte = DateTime.ParseExact(
enddte, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE pasn.prsn_bnfts_cntrbtns " +
              "SET item_pssbl_value_id=" + itm_val_id +
        ", valid_end_date='" + enddte.Replace("'", "''") + "', " +
              "last_update_by=" +
                        Global.myPrsn.user_id + ", last_update_date='" + dateStr + "' " +
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
       "', last_update_by=" + Global.myPrsn.user_id + ", last_update_date='" + dateStr + "' " +
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
      COALESCE(d.account_number,'-') bank_acc_num,b.report_line_no,b.pay_run_priority
   FROM (pay.pay_itm_trnsctns a LEFT OUTER JOIN org.org_pay_items b ON a.item_id = b.item_id) 
   LEFT OUTER JOIN prs.prsn_names_nos c on a.person_id = c.person_id 
   LEFT OUTER JOIN pasn.prsn_bank_accounts d on a.person_id = d.person_id 
   LEFT OUTER JOIN prs.prsn_national_ids e on a.person_id = e.person_id and e.national_id_typ='SSNIT'
   WHERE(a.amount_paid>=0 and a.mass_pay_id = " + mspyid + " and b.item_value_uom ='Money'" + whCls + ") " +
   @"GROUP BY 1,2,3,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22
   ORDER BY c.local_id_no, b.report_line_no, b.item_min_type, b.pay_run_priority";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      //Global.mnFrm.mspydt_SQL = strSql;
      return dtst;
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

    public static DataSet get_Advance_ItmDet(string itmName)
    {
      string itmSQL = @"SELECT a.item_id, a.item_code_name, a.item_value_uom, 
(CASE WHEN a.item_min_type='Earnings' or a.item_min_type='Employer Charges' THEN 'Payment by Organisation' 
WHEN a.item_min_type='Bills/Charges' or a.item_min_type='Deductions' THEN 'Payment by Person' 
ELSE 'Purely Informational' END) trns_typ 
FROM org.org_pay_items a 
WHERE a.local_classfctn = 'Advance Items' AND a.org_id = " + Global.mnFrm.cmCde.Org_id + @"";
      string strSql = "";
      string whereCls = "";
      strSql = @"SELECT -1, tbl1.item_code_name, tbl1.item_value_uom, tbl1.trns_typ, 
     tbl1.item_id, pay.get_first_itmval_id(tbl1.item_id), a.item_maj_type, a.item_min_type, a.allow_value_editing " +
              "FROM (" + itmSQL + ") tbl1, org.org_pay_items a " +
              "WHERE ((tbl1.item_id = a.item_id) and (a.is_enabled = '1') and a.item_code_name='" + itmName + @"') " +
    "ORDER BY a.pay_run_priority ";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
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
      "', last_update_by=" + Global.myPrsn.user_id +
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
        Global.myPrsn.user_id + ", '" + dateStr +
                        "', " + Global.myPrsn.user_id + ", '" + dateStr + "', '" + src_trns.Replace("'", "''") + "')";
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
        Global.myPrsn.user_id + ", '" + dateStr +
                        "', " + Global.myPrsn.user_id + ", '" + dateStr + "', '" + src_trns.Replace("'", "''") + "')";
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
       "', '" + trnsType.Replace("'", "''") + "', " + Global.myPrsn.user_id + ", '" + dateStr + "', " +
               Global.myPrsn.user_id + ", '" + dateStr + "', " + msspyid +
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
       "', '" + trnsType.Replace("'", "''") + "', " + Global.myPrsn.user_id + ", '" + dateStr + "', " +
               Global.myPrsn.user_id + ", '" + dateStr + "', " + msspyid +
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
       "', " + Global.myPrsn.user_id + ", '" + dateStr + "', " + orgid + ", '0', " +
               Global.myPrsn.user_id + ", '" + dateStr + "', '" +
               batchsource.Replace("'", "''") + "', '0')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void updtTodaysGLBatchPstngAvlblty(long batchid, string avlblty)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string insSQL = "UPDATE accb.accb_trnsctn_batches SET avlbl_for_postng='" + avlblty +
        "', last_update_by=" + Global.myPrsn.user_id +
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
               ", '" + trnsdte.Replace("'", "''") + "', " + crncyid + ", " + Global.myPrsn.user_id +
               ", '" + dateStr + "', " + crdtamnt + ", " +
               Global.myPrsn.user_id + ", '" + dateStr + "', " + netamnt +
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
               ", '" + trnsdte.Replace("'", "''") + "', " + crncyid + ", " + Global.myPrsn.user_id +
               ", '" + dateStr + "', " + crdtamnt + ", " +
               Global.myPrsn.user_id + ", '" + dateStr + "', " + netamnt +
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
               ", '" + trnsdte.Replace("'", "''") + "', " + crncyid + ", " + Global.myPrsn.user_id +
               ", '" + dateStr + "', " + batchid + ", " + crdtamnt + ", " +
               Global.myPrsn.user_id + ", '" + dateStr + "', " + netamnt +
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
               ", '" + trnsdte.Replace("'", "''") + "', " + crncyid + ", " + Global.myPrsn.user_id +
               ", '" + dateStr + "', " + crdtamnt + ", " +
               Global.myPrsn.user_id + ", '" + dateStr + "', " + netamnt +
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
               ", '" + trnsdte.Replace("'", "''") + "', " + crncyid + ", " + Global.myPrsn.user_id +
               ", '" + dateStr + "', " + crdtamnt + ", " +
               Global.myPrsn.user_id + ", '" + dateStr + "', " + netamnt +
               ", -1, " + srcid + ", '" + trnsSrc + "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void createMoneyBal(long prsnid, double ttlpay, double ttlwthdrwl)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = "INSERT INTO pay.pay_prsn_money_bals(" +
      "person_id, total_payments, created_by, creation_date, " +
      "last_update_by, last_update_date, total_withdrawals) " +
          "VALUES (" + prsnid + ", " + ttlpay + ", " + Global.myPrsn.user_id +
          ", '" + dateStr + "', " + Global.myPrsn.user_id + ", '" +
          dateStr + "', " + ttlwthdrwl + ")";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

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
      "SET last_update_by = " + Global.myPrsn.user_id +
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
      "SET last_update_by = " + Global.myPrsn.user_id +
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
      "SET last_update_by = " + Global.myPrsn.user_id +
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
      "SET last_update_by = " + Global.myPrsn.user_id +
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
    //  ", last_update_by=" + Global.myPrsn.user_id + ", " +
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
      ", last_update_by=" + Global.myPrsn.user_id + ", " +
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
      ", last_update_by=" + Global.myPrsn.user_id + ", " +
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
      ", last_update_by=" + Global.myPrsn.user_id + ", " +
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
      ", last_update_by=" + Global.myPrsn.user_id + ", " +
      "last_update_date='" + dateStr + "' " +
      "WHERE a.gl_batch_id = -1 and a.source_trns_id IN " +
      "(select pay_trns_id from pay.pay_itm_trnsctns  where pay_trns_id = " +
      py_trns_id + ") and EXISTS(select f.transctn_id from accb.accb_trnsctn_details f " +
      "where f.batch_id = " + glbatchid + " " +
      "and f.source_trns_ids like '%,' || a.interface_id || ',%' and " +
      "f.trnsctn_date=a.trnsctn_date and f.accnt_id= a.accnt_id) ";
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
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
    public static void updateMsPyStatus(long mspyid, string run_cmpltd, string to_gl_intfc)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE pay.pay_mass_pay_run_hdr " +
      "SET run_status='" + run_cmpltd.Replace("'", "''") +
      "', sent_to_gl='" + to_gl_intfc.Replace("'", "''") +
      "', last_update_by=" + Global.myPrsn.user_id +
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
      "', last_update_by=" + Global.myPrsn.user_id +
      ", last_update_date='" + dateStr +
      "', prs_st_id = " + prstid + ", itm_st_id = " + itmstid +
      " WHERE mass_pay_id = " + mspyid;
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
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
            "', " + Global.myPrsn.user_id + ", '" + dateStr +
            "', " + Global.myPrsn.user_id + ", '" + dateStr +
            "', '0', '" + trnsdte.Replace("'", "''") + "', " +
            prstid + ", " + itmstid + ", " + orgid + ", '0', '" + glDate +
            "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
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
    #endregion

    #region "ASSIGNMENT TEMPLATES..."
    public static DataSet get_One_Tmplt_Det(int tmpltID)
    {
      string strSql = "";
      strSql = "SELECT a.div_ids, a.grade_id, a.job_id, a.loc_id, a.pos_id, " +
            "a.sprvsor_id, a.wkhr_id, a.prsn_typ, a.prsn_typ_asgn_rsn, a.prsn_typ_futh_det, " +
                     "a.gath_typ_ids, a.pay_item_ids, a.pay_item_val_ids , a.org_id " +
                     "FROM pasn.prsn_assgnmnt_tmplts a " +
            "WHERE(a.tmplt_id = " + tmpltID + ")";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      Global.mnFrm.tmpltDet_SQL = strSql;
      return dtst;
    }

    public static DataSet get_Basic_Tmplt(string searchWord, string searchIn,
     Int64 offset, int limit_size)
    {
      string strSql = "";
      if (searchIn == "Template Name")
      {
        strSql = "SELECT a.tmplt_id, a.tmplt_name, a.tmplt_desc, a.is_enabled " +
        "FROM pasn.prsn_assgnmnt_tmplts a " +
        "WHERE ((a.tmplt_name ilike '" + searchWord.Replace("'", "''") +
         "')) ORDER BY a.tmplt_id DESC LIMIT " + limit_size +
         " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
      }
      else if (searchIn == "Template Description")
      {
        strSql = "SELECT a.tmplt_id, a.tmplt_name, a.tmplt_desc, a.is_enabled " +
        "FROM pasn.prsn_assgnmnt_tmplts a " +
        "WHERE ((a.tmplt_desc ilike '" + searchWord.Replace("'", "''") +
         "')) ORDER BY a.tmplt_id DESC LIMIT " + limit_size +
         " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
      }
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      Global.mnFrm.tmplt_SQL = strSql;
      return dtst;
    }

    public static long get_Total_Tmplts(string searchWord, string searchIn)
    {
      string strSql = "";
      if (searchIn == "Template Name")
      {
        strSql = "SELECT count(1) FROM pasn.prsn_assgnmnt_tmplts a " +
        "WHERE ((a.tmplt_desc ilike '" + searchWord.Replace("'", "''") +
         "'))";
      }
      else if (searchIn == "Template Description")
      {
        strSql = "SELECT count(1) FROM pasn.prsn_assgnmnt_tmplts a " +
        "WHERE ((a.tmplt_desc ilike '" + searchWord.Replace("'", "''") +
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

    public static long doesPrsnHvDiv(long prsnid, long divid)
    {
      string selSQL = "SELECT prsn_div_id " +
                  "FROM pasn.prsn_divs_groups WHERE ((person_id = " + prsnid +
                  ") and (div_id = " + divid + "))";
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

    public static long doesPrsnHvGath(long prsnid, long gathid)
    {
      string selSQL = "SELECT row_id " +
                  "FROM pasn.prsn_gathering_typs WHERE ((person_id = " + prsnid +
                  ") and (gatherng_typ_id = " + gathid + "))";
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

    public static long doesPrsnHvSpvsr(long prsnid, long spvsrid)
    {
      string selSQL = "SELECT row_id " +
                  "FROM pasn.prsn_supervisors WHERE ((person_id = " + prsnid +
                  ") and (supervisor_prsn_id = " + spvsrid + "))";
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

    public static long doesPrsnHvLoc(long prsnid, long locid)
    {
      string selSQL = "SELECT prsn_loc_id " +
                  "FROM pasn.prsn_locations WHERE ((person_id = " + prsnid +
                  ") and (location_id = " + locid + "))";
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

    public static long doesPrsnHvGrade(long prsnid, long grdid)
    {
      string selSQL = "SELECT row_id " +
                  "FROM pasn.prsn_grades WHERE ((person_id = " + prsnid +
                  ") and (grade_id = " + grdid + "))";
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

    public static long doesPrsnHvJob(long prsnid, long jobid)
    {
      string selSQL = "SELECT row_id " +
                  "FROM pasn.prsn_jobs WHERE ((person_id = " + prsnid +
                  ") and (job_id = " + jobid + "))";
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

    public static long doesPrsnHvPos(long prsnid, long posid)
    {
      string selSQL = "SELECT row_id " +
                  "FROM pasn.prsn_positions WHERE ((person_id = " + prsnid +
                  ") and (position_id = " + posid + "))";
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

    public static long doesPrsnHvWkh(long prsnid, long wkhid)
    {
      string selSQL = "SELECT row_id " +
                  "FROM pasn.prsn_work_id WHERE ((person_id = " + prsnid +
                  ") and (work_hour_id = " + wkhid + "))";
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

    public static string get_Tmplt_Rec_Hstry(long tmpltID)
    {
      string strSQL = "SELECT a.created_by, to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), a.last_update_by, to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') " +
      "FROM pasn.prsn_assgnmnt_tmplts a WHERE(a.tmplt_id  = " + tmpltID + ")";
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

    #endregion
    #endregion

    #region "CUSTOM FUNCTIONS..."
    public static long getLtstRecPkID(string tblNm, string pkeyCol)
    {
      DataSet dtSt = new DataSet();
      string sqlStr = "select " + pkeyCol + " from " + tblNm + " ORDER BY 1 DESC LIMIT 1 OFFSET 0";
      dtSt = Global.mnFrm.cmCde.selectDataNoParams(sqlStr);
      if (dtSt.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtSt.Tables[0].Rows[0][0].ToString()) + 1;
      }
      else
      {
        return 1000;
      }
    }

    public static string getLtstPrsnIDNo()
    {
      DataSet dtSt = new DataSet();
      string sqlStr = "select count(person_id) from prs.prsn_names_nos WHERE org_id=" + Global.mnFrm.cmCde.Org_id + "";
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

    public static string getLtstPrsnIDNoInPrfx(string prfxTxt)
    {
      DataSet dtSt = new DataSet();
      string sqlStr = "select count(person_id) from prs.prsn_names_nos WHERE org_id=" +
        Global.mnFrm.cmCde.Org_id + " and local_id_no ilike '" + prfxTxt.Replace("'", "''") + "%'";
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

    public static string getLastPrsnIDNo()
    {
      DataSet dtSt = new DataSet();
      string sqlStr = @"select (chartonumeric(local_id_no) + 1) from prs.prsn_names_nos 
        WHERE org_id=" + Global.mnFrm.cmCde.Org_id + @"
      ORDER BY chartonumeric(local_id_no) DESC LIMIT 1 OFFSET 0";
      dtSt = Global.mnFrm.cmCde.selectDataNoParams(sqlStr);
      if (dtSt.Tables[0].Rows.Count > 0)
      {
        return (long.Parse(dtSt.Tables[0].Rows[0][0].ToString()) + 1).ToString().PadLeft(5, '0');
      }
      else
      {
        return "00001";
      }
    }

    public static DataSet getAllEnbldPssblVals(string lovNm, string extrWhr)
    {
      DataSet dtSt = new DataSet();
      string sqlStr = "select pssbl_value from gst.gen_stp_lov_values " +
        "WHERE is_enabled='1' and value_list_id = " + Global.mnFrm.cmCde.getLovID(lovNm) +
        " and allowed_org_ids ilike '%," + Global.mnFrm.cmCde.Org_id +
        ",%'" + extrWhr + " ORDER BY 1";
      //Global.mnFrm.cmCde.showSQLNoPermsn(sqlStr);
      dtSt = Global.mnFrm.cmCde.selectDataNoParams(sqlStr);
      return dtSt;
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

    public static void createRqrdLOVs()
    {
      string[] sysLovs = { "Person ID No. Prefix", "YesNo", "Alive/Dead", 
                           "Person ID No. Prefix Determines ID Serial No.", 
                           "Marital Status" };
      string[] sysLovsDesc = { "Person ID No. Prefix", "YesNo", "Alive/Dead", 
                               "Person ID No. Prefix Determines ID Serial No.", 
                               "Marital Status" };
      string[] sysLovsDynQrys = { "", "", "", "", "" };
      string[] pssblVals = { 
        "0", "E", "Employee"
		   ,"0", "C", "Contact Person",
        "0", "M", "Member"
       ,"1", "Yes", "Yes"
       ,"1", "No", "No"
       ,"2", "Alive", "Alive"
       ,"2", "Dead", "Dead"
       ,"3", "Yes", "Yes"
       ,"4", "Not Formalized (Cohabiting)", "Not Formalized (Informal) Marriage"};
      Global.mnFrm.cmCde.createSysLovs(sysLovs, sysLovsDynQrys, sysLovsDesc);
      Global.mnFrm.cmCde.createSysLovsPssblVals(sysLovs, pssblVals);
    }

    public static void refreshRqrdVrbls()
    {
      Global.mnFrm.cmCde.DefaultPrvldgs = Global.dfltPrvldgs;
      Global.mnFrm.cmCde.SubGrpNames = Global.subGrpNames;
      Global.mnFrm.cmCde.MainTableNames = Global.mainTableNames;
      Global.mnFrm.cmCde.KeyColumnNames = Global.keyColumnNames;
      //Global.mnFrm.cmCde.Login_number = Global.myPrsn.login_number;
      Global.mnFrm.cmCde.ModuleAdtTbl = Global.myPrsn.full_audit_trail_tbl_name;
      Global.mnFrm.cmCde.ModuleDesc = Global.myPrsn.mdl_description;
      Global.mnFrm.cmCde.ModuleName = Global.myPrsn.name;
      //Global.mnFrm.cmCde.pgSqlConn = Global.myPrsn.Host.globalSQLConn;
      //Global.mnFrm.cmCde.Role_Set_IDs = Global.myPrsn.role_set_id;
      //Global.mnFrm.cmCde.Org_id = Global.myPrsn.org_id;
      Global.mnFrm.cmCde.SampleRole = "Basic Person Data Administrator";
      //Global.mnFrm.cmCde.User_id = Global.myPrsn.user_id;
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      Global.myPrsn.user_id = Global.mnFrm.usr_id;
      Global.myPrsn.login_number = Global.mnFrm.lgn_num;
      Global.myPrsn.role_set_id = Global.mnFrm.role_st_id;
      Global.myPrsn.org_id = Global.mnFrm.Og_id;

    }
    #endregion
  }
}
