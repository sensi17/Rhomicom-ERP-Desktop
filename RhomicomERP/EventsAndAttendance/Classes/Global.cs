using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing.Imaging;
using EventsAndAttendance.Forms;
using EventsAndAttendance.Dialogs;
using System.Windows.Forms;
using CommonCode;

namespace EventsAndAttendance.Classes
{
  /// <summary>
  /// A  class containing variables and 
  /// functions we will like to call directly from 
  /// anywhere in the project without creating an instance first
  /// </summary>
  class Global
  {
    #region "GLOBAL DECLARATIONS..."
    public static EventsAndAttendance myEvnt = new EventsAndAttendance();
    public static mainForm mnFrm = null;
    public static attndRecsForm attndFrm = null;
    public static timeDetailsDiag attndDiag = null;
    public static tmetblForm tmtblFrm = null;
    public static eventsForm evntFrm = null;
    public static venuesForm vnuFrm = null;
    public static srchAttndForm srchAttndFrm = null;
    public static complaintsForm wfnCmplntsFrm = null;
    public static checkinsForm wfnCheckinsFrm = null;
    public static checkinsForm wfnCheckinsDiag = null;
    //public static adhocAttndncForm adhcAttndFrm = null;
    public static string[] dfltPrvldgs = { "View Events And Attendance", 
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
    public static string currentPanel = "";
    public static string itms_SQL = "";
    public static int selectedStoreID = -1;

    public static string intFcSql = string.Empty;
    public static int serv_type_hdrID = 0;
    public static int room_id = 0;

    #endregion

    #region "SQL STATEMENTS..."
    #region "SELECT STATEMENTS..."

    #region "ATTACHMENTS..."
    //  public static long getAttchmntID(string attchname, long hdrID)
    //  {
    //    string strSql = "";
    //    strSql = "SELECT a.attchmnt_id " +
    //"FROM attn.attn_doc_attchmnts a " +
    //    "WHERE ((a.attchmnt_desc = '" + attchname.Replace("'", "''") +
    //      "') AND (a.attnd_rec_id = " + hdrID + "))";

    //    DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
    //    if (dtst.Tables[0].Rows.Count > 0)
    //    {
    //      return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
    //    }
    //    else
    //    {
    //      return -1;
    //    }
    //  }

    public static long getAttchmntID(string attchname, long recID, long hdrID)
    {
      string strSql = "";
      strSql = "SELECT a.attchmnt_id " +
  "FROM attn.attn_doc_attchmnts a " +
      "WHERE ((a.attchmnt_desc = '" + attchname.Replace("'", "''") +
        "') AND (a.attnd_rec_id = " + recID + ") AND (a.recs_hdr_id = " + hdrID + "))";

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

    public static void createAttachment(long hdrID, long recID, string attchDesc,
     string filNm)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = "INSERT INTO attn.attn_doc_attchmnts(" +
            "attnd_rec_id, recs_hdr_id, attchmnt_desc, file_name, created_by, " +
            "creation_date, last_update_by, last_update_date) " +
                        "VALUES (" + recID +
                        "," + hdrID +
                        ", '" + attchDesc.Replace("'", "''") +
                        "', '" + filNm.Replace("'", "''") +
                        "', " + Global.myEvnt.user_id + ", '" + dateStr +
                        "', " + Global.myEvnt.user_id + ", '" + dateStr + "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void updateAttachment(long attchID, long recID, long hdrID, string attchDesc,
   string filNm)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE attn.attn_doc_attchmnts SET " +
            "attnd_rec_id=" + recID +
                        ", recs_hdr_id=" + hdrID +
                        ",attchmnt_desc='" + attchDesc.Replace("'", "''") +
                        "', file_name='" + filNm.Replace("'", "''") +
                        "', last_update_by=" + Global.myEvnt.user_id +
                        ", last_update_date='" + dateStr + "' " +
                         "WHERE attchmnt_id = " + attchID;
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void deleteAttchmnt(long attchid, string attchNm)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Attachment Name = " + attchNm;
      string delSql = "DELETE FROM attn.attn_doc_attchmnts WHERE(attchmnt_id = " + attchid + ")";
      Global.mnFrm.cmCde.deleteDataNoParams(delSql);
    }

    public static DataSet get_Attachments(string searchWord, string searchIn,
   Int64 offset, int limit_size, long hdrID, ref string attchSQL)
    {
      string strSql = "";
      if (searchIn == "Attachment Name/Description")
      {
        strSql = @"SELECT a.attchmnt_id, 
        CASE WHEN a.attnd_rec_id <=0 THEN a.recs_hdr_id ELSE a.attnd_rec_id END, 
      a.attchmnt_desc, a.file_name " +
      "FROM attn.attn_doc_attchmnts a " +
      "WHERE(a.attchmnt_desc ilike '" + searchWord.Replace("'", "''") +
      "' and (CASE WHEN a.attnd_rec_id <=0 THEN a.recs_hdr_id ELSE a.attnd_rec_id END) = " + hdrID + ") ORDER BY a.attchmnt_id LIMIT " + limit_size +
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
      "FROM attn.attn_doc_attchmnts a " +
      "WHERE(a.attchmnt_desc ilike '" + searchWord.Replace("'", "''") +
      "' and (CASE WHEN a.attnd_rec_id <=0 THEN a.recs_hdr_id ELSE a.attnd_rec_id END) = " + hdrID + ")";
      }
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      long sumRes = 0;
      if (dtst.Tables[0].Rows.Count > 0)
      {
        long.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out sumRes);
      }
      return sumRes;
    }
    #endregion

    #region "VENUES..."
    public static void createVenue(int orgid, string vnuname,
string vnudesc, string vnuClssf, bool isEnbld, int noofprsns)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = @"INSERT INTO attn.attn_event_venues(
            venue_name, venue_desc, max_no_persons, venue_classification, 
            created_by, creation_date, last_update_by, last_update_date, 
            org_id, is_enabled) " +
            "VALUES ('" + vnuname.Replace("'", "''") +
            "', '" + vnudesc.Replace("'", "''") +
            "', " + noofprsns + ", '" + vnuClssf.Replace("'", "''") +
            "', " + Global.myEvnt.user_id + ", '" + dateStr +
            "', " + Global.myEvnt.user_id + ", '" + dateStr +
            "', " + orgid + ", '" +
            Global.mnFrm.cmCde.cnvrtBoolToBitStr(isEnbld) + "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void updateVenue(int vnuid, string vnuname,
string vnudesc, string vnuClssf, bool isEnbld, int noofprsns)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE attn.attn_event_venues SET " +
            "venue_name='" + vnuname.Replace("'", "''") +
            "', venue_desc='" + vnudesc.Replace("'", "''") +
            "', last_update_by=" + Global.myEvnt.user_id + ", " +
            "last_update_date='" + dateStr +
            "', venue_classification='" + vnuClssf.Replace("'", "''") + "', is_enabled='" +
            Global.mnFrm.cmCde.cnvrtBoolToBitStr(isEnbld) +
            "', max_no_persons=" + noofprsns + " " +
            "WHERE (venue_id =" + vnuid + ")";
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void deleteVenue(long vnuid, string vnuNm)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Venue Name = " + vnuNm;
      string delSQL = "DELETE FROM attn.attn_event_venues WHERE venue_id = " + vnuid;
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }

    public static bool isVnuInUse(int vnuID)
    {
      string strSql = "SELECT a.event_id " +
       "FROM attn.attn_attendance_events a " +
       "WHERE(a.preffrd_venue_id = " + vnuID + ")";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return true;
      }

      strSql = "SELECT a.time_table_det_id " +
       "FROM attn.attn_time_table_details a " +
       "WHERE(a.assgnd_venue_id = " + vnuID + ")";
      dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return true;
      }
      strSql = "SELECT a.time_table_det_id " +
       "FROM attn.temp_time_table_details a " +
       "WHERE(a.assgnd_venue_id = " + vnuID + ")";
      dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return true;
      }
      return false;
    }

    public static int getVenueID(string vnuname, int orgid)
    {
      DataSet dtSt = new DataSet();
      string sqlStr = "select venue_id from attn.attn_event_venues where lower(venue_name) = '" +
       vnuname.Replace("'", "''").ToLower() + "' and org_id = " + orgid;
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

    public static DataSet get_One_VnuDet(int codeID)
    {
      string strSql = "SELECT a.venue_id, a.venue_name, a.venue_desc, a.venue_classification, a.max_no_persons, a.is_enabled " +
       "FROM attn.attn_event_venues a " +
       "WHERE(a.venue_id = " + codeID + ")";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      //Global.taxFrm.rec_SQL = strSql;
      return dtst;
    }

    public static DataSet get_Basic_Venues(string searchWord, string searchIn,
 Int64 offset, int limit_size, int orgID)
    {
      string strSql = "";
      string whrcls = "";
      if (searchIn == "Venue Name")
      {
        whrcls = " AND (a.venue_name ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Venue Description")
      {
        whrcls = " AND (a.venue_desc ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      strSql = "SELECT a.venue_id, a.venue_name " +
"FROM attn.attn_event_venues a " +
"WHERE ((a.org_id = " + orgID + ")" + whrcls + ") ORDER BY a.venue_name LIMIT " + limit_size +
" OFFSET " + (Math.Abs(offset * limit_size)).ToString();

      Global.vnuFrm.rec_SQL = strSql;
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      return dtst;
    }

    public static long get_Total_Venues(string searchWord, string searchIn, int orgID)
    {
      string strSql = "";
      string whrcls = "";
      if (searchIn == "Venue Name")
      {
        whrcls = " AND (a.venue_name ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Venue Description")
      {
        whrcls = " AND (a.venue_desc ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      strSql = "SELECT count(1) " +
"FROM attn.attn_event_venues a " +
"WHERE ((a.org_id = " + orgID + ")" + whrcls + ")";
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

    #region "EVENTS..."
    public static void deleteActvtyRslt(long lnID)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string delSQL = "DELETE FROM attn.attn_attendance_events_rslts WHERE evnt_rslt_id = " +
        lnID + "";
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }

    public static DataSet get_One_ActvtyRslts(string searchWord, string searchIn, long offset,
      int limit_size, long evntID)
    {
      string strSql = "";
      string whrcls = "";
      /*
       *  Start Date
End Date
Event Result
Metric Name
Comment
       * 
       */
      if (searchIn == "Metric Name")
      {
        whrcls = " AND (b.rslt_metric_name_desc ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Start Date")
      {
        whrcls = " AND (to_char(to_timestamp(a.rstl_start_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "End Date")
      {
        whrcls = " AND (to_char(to_timestamp(a.rslt_end_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Event Result")
      {
        whrcls = " AND (a.event_result ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Comment")
      {
        whrcls = " AND (a.rslt_desc ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      strSql = @"SELECT a.evnt_rslt_id, a.event_id, a.evnt_metric_id, b.rslt_metric_name_desc, a.event_result, 
      CASE WHEN a.rstl_start_date != '' THEN to_char(to_timestamp(a.rstl_start_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') ELSE '' END, 
      CASE WHEN a.rslt_end_date != '' THEN to_char(to_timestamp(a.rslt_end_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') ELSE '' END,
      a.rslt_desc, a.auto_calc
  FROM attn.attn_attendance_events_rslts a, attn.attn_attendance_events_mtrcs b " +
        "WHERE((a.evnt_metric_id = b.rslt_metric_id) and (a.event_id = " + evntID + ")" + whrcls + ") ORDER BY 4, 1 LIMIT " + limit_size +
        " OFFSET " + (Math.Abs(offset * limit_size)).ToString();

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      Global.evntFrm.rec_det_SQL = strSql;
      return dtst;
    }

    public static long get_Total_ActvtyRslts(string searchWord, string searchIn, long evntID)
    {
      string strSql = "";
      string whrcls = "";
      /*
       *  Start Date
End Date
Event Result
Metric Name
Comment
       * 
       */
      if (searchIn == "Metric Name")
      {
        whrcls = " AND (b.rslt_metric_name_desc ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Start Date")
      {
        whrcls = " AND (to_char(to_timestamp(a.rstl_start_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "End Date")
      {
        whrcls = " AND (to_char(to_timestamp(a.rslt_end_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Event Result")
      {
        whrcls = " AND (a.event_result ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Comment")
      {
        whrcls = " AND (a.rslt_desc ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      strSql = @"SELECT count(1)
  FROM attn.attn_attendance_events_rslts a, attn.attn_attendance_events_mtrcs b " +
        "WHERE((a.evnt_metric_id = b.rslt_metric_id) and (a.event_id = " + evntID + ")" + whrcls + ")";

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

    public static void createActvtyRslt(long rstlID, int evntID, int evntMtrcID,
string rsltCmmnt, string rslt, string strtDte, string endDte, bool autoCalc, long rgstrID)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = @"INSERT INTO attn.attn_attendance_events_rslts(
            evnt_rslt_id, event_id, evnt_metric_id, rslt_desc, event_result, 
            rstl_start_date, rslt_end_date, created_by, creation_date, last_update_by, 
            last_update_date, auto_calc, lnkd_rgstr_id) " +
            "VALUES (" + rstlID + ", " + evntID + ", " + evntMtrcID +
            ", '" + rsltCmmnt.Replace("'", "''") +
            "', '" + rslt.Replace("'", "''") +
            "', '" + strtDte.Replace("'", "''") +
            "', '" + endDte.Replace("'", "''") +
            "', " + Global.myEvnt.user_id + ", '" + dateStr +
            "', " + Global.myEvnt.user_id + ", '" + dateStr +
            "', '" +
            Global.mnFrm.cmCde.cnvrtBoolToBitStr(autoCalc) + "', " + rgstrID + ")";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void updateActvtyRslt(long rstlID, int evntID, int evntMtrcID,
string rsltCmmnt, string rslt, string strtDte, string endDte, bool autoCalc, long rgstrID)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE attn.attn_attendance_events_rslts SET " +
            "rslt_desc='" + rsltCmmnt.Replace("'", "''") +
            "', event_result='" + rslt.Replace("'", "''") +
            "', rstl_start_date='" + strtDte.Replace("'", "''") +
            "', rslt_end_date='" + endDte.Replace("'", "''") +
            "', evnt_metric_id=" + evntMtrcID +
            ", last_update_by = " + Global.myEvnt.user_id + ", " +
            "last_update_date = '" + dateStr +
            "', auto_calc='" +
            Global.mnFrm.cmCde.cnvrtBoolToBitStr(autoCalc) +
            "', lnkd_rgstr_id= " + rgstrID +
            " WHERE (evnt_rslt_id =" + rstlID + ")";
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void createEvntMtrc(long mtrcID, string mtrcName,
string mtrcCmmnt, string rsltTyp, bool isEnbld, string strSQL, int evntID)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = @"INSERT INTO attn.attn_attendance_events_mtrcs(
            rslt_metric_id, rslt_metric_name_desc, rslt_type, rslt_comment, 
            rslt_query, created_by, creation_date, last_update_by, last_update_date, 
            event_id, is_enabled) " +
            "VALUES (" + mtrcID + ", '" + mtrcName.Replace("'", "''") +
            "', '" + rsltTyp.Replace("'", "''") +
            "', '" + mtrcCmmnt.Replace("'", "''") +
            "', '" + strSQL.Replace("'", "''") +
            "', " + Global.myEvnt.user_id + ", '" + dateStr +
            "', " + Global.myEvnt.user_id + ", '" + dateStr +
            "', " + evntID + ", '" +
            Global.mnFrm.cmCde.cnvrtBoolToBitStr(isEnbld) + "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void updateEvntMtrc(long mtrcID, string mtrcName,
string mtrcCmmnt, string rsltTyp, bool isEnbld, string strSQL, int evntID)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE attn.attn_attendance_events_mtrcs SET " +
            "rslt_metric_name_desc='" + mtrcName.Replace("'", "''") +
            "', rslt_type='" + rsltTyp.Replace("'", "''") +
            "', rslt_comment='" + mtrcCmmnt.Replace("'", "''") +
            "', rslt_query='" + strSQL.Replace("'", "''") +
            "', event_id=" + evntID +
            ", last_update_by = " + Global.myEvnt.user_id + ", " +
            "last_update_date = '" + dateStr +
            "', is_enabled='" +
            Global.mnFrm.cmCde.cnvrtBoolToBitStr(isEnbld) +
            "' " +
            "WHERE (rslt_metric_id =" + mtrcID + ")";
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void createEvntPrice(long pricID, string ctgrName,
 bool isEnbld, int itmID, int evntID)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = @"INSERT INTO attn.event_price_categories(
            price_ctgry_id, event_id, price_category, inv_itm_id, created_by, 
            creation_date, last_update_by, last_update_date, is_enabled) " +
            "VALUES (" + pricID + ", " + evntID + ", '" + ctgrName.Replace("'", "''") +
            "', " + itmID + ", " + Global.myEvnt.user_id + ", '" + dateStr +
            "', " + Global.myEvnt.user_id + ", '" + dateStr +
            "', '" +
            Global.mnFrm.cmCde.cnvrtBoolToBitStr(isEnbld) + "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void updateEvntPrice(long pricID, string ctgrName,
 bool isEnbld, int itmID, int evntID)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE attn.event_price_categories SET " +
            "price_category='" + ctgrName.Replace("'", "''") +
            "', inv_itm_id=" + itmID +
            ", last_update_by = " + Global.myEvnt.user_id + ", " +
            "last_update_date = '" + dateStr +
            "', is_enabled='" +
            Global.mnFrm.cmCde.cnvrtBoolToBitStr(isEnbld) +
            "' " +
            "WHERE (price_ctgry_id =" + pricID + ")";
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void createEvent(int orgid, string evntname,
string evntdesc, string evntTyp, bool isEnbld, long hostprsnid, string grpType,
      int grpID, int tmtblSessins, int hgstCntns, int slotprty,
      string evntClsfctn, int prfrdvnuID, string grpnm, string mtrcLOV, string pointsLov)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = @"INSERT INTO attn.attn_attendance_events(
            event_name, event_desc, event_typ, host_prsn_id, allwd_grp_typ, 
            allwd_grp_id, ttl_tmtbl_sessn_mins, hghst_cntnuous_mins, slot_priority, 
            created_by, creation_date, last_update_by, last_update_date, 
            event_classification, org_id, preffrd_venue_id, is_enabled, 
            allwd_group_nm, attnd_metric_lov_nm, attnd_points_lov_nm) " +
            "VALUES ('" + evntname.Replace("'", "''") +
            "', '" + evntdesc.Replace("'", "''") +
            "', '" + evntTyp.Replace("'", "''") +
            "', " + hostprsnid + ", '" + grpType.Replace("'", "''") +
            "', " + grpID + ", " + tmtblSessins + ", " + hgstCntns + ", " + slotprty +
            ", " + Global.myEvnt.user_id + ", '" + dateStr +
            "', " + Global.myEvnt.user_id + ", '" + dateStr +
            "', '" + evntClsfctn.Replace("'", "''") +
            "', " + orgid + ", " + prfrdvnuID + ", '" +
            Global.mnFrm.cmCde.cnvrtBoolToBitStr(isEnbld) + "', '" + grpnm.Replace("'", "''") +
            "', '" + mtrcLOV.Replace("'", "''") +
            "', '" + pointsLov.Replace("'", "''") +
            "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void updateEvent(int evntid, string evntname,
string evntdesc, string evntTyp, bool isEnbld, long hostprsnid, string grpType,
      int grpID, int tmtblSessins, int hgstCntns, int slotprty,
      string evntClsfctn, int prfrdvnuID, string grpnm, string mtrcLOV, string pointsLov)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE attn.attn_attendance_events SET " +
            "event_name='" + evntname.Replace("'", "''") +
            "', event_desc='" + evntdesc.Replace("'", "''") +
            "', event_typ='" + evntTyp.Replace("'", "''") +
            "', host_prsn_id=" + hostprsnid +
            ", allwd_grp_typ='" + grpType.Replace("'", "''") +
            "', allwd_grp_id=" + grpID +
            ", ttl_tmtbl_sessn_mins=" + tmtblSessins +
            ", hghst_cntnuous_mins=" + hgstCntns +
            ", slot_priority=" + slotprty +
            ", last_update_by = " + Global.myEvnt.user_id + ", " +
            "last_update_date = '" + dateStr +
            "', event_classification='" + evntClsfctn.Replace("'", "''") + "', is_enabled='" +
            Global.mnFrm.cmCde.cnvrtBoolToBitStr(isEnbld) +
            "', preffrd_venue_id=" + prfrdvnuID + ", allwd_group_nm = '" + grpnm.Replace("'", "''") +
            "', attnd_metric_lov_nm= '" + mtrcLOV.Replace("'", "''") +
            "', attnd_points_lov_nm = '" + pointsLov.Replace("'", "''") +
            "' " +
            "WHERE (event_id =" + evntid + ")";
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void updateEvent(int evntid, string evntname,
string evntdesc, string evntTyp, bool isEnbld, string grpType,
    int grpID, int tmtblSessins, int hgstCntns, int slotprty,
    string evntClsfctn, string grpnm, string mtrcLOV, string pointsLov)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE attn.attn_attendance_events SET " +
            "event_name='" + evntname.Replace("'", "''") +
            "', event_desc='" + evntdesc.Replace("'", "''") +
            "', event_typ='" + evntTyp.Replace("'", "''") +
            "', allwd_grp_typ='" + grpType.Replace("'", "''") +
            "', allwd_grp_id=" + grpID +
            ", ttl_tmtbl_sessn_mins=" + tmtblSessins +
            ", hghst_cntnuous_mins=" + hgstCntns +
            ", slot_priority=" + slotprty +
            ", last_update_by = " + Global.myEvnt.user_id + ", " +
            "last_update_date = '" + dateStr +
            "', event_classification='" + evntClsfctn.Replace("'", "''") + "', is_enabled='" +
            Global.mnFrm.cmCde.cnvrtBoolToBitStr(isEnbld) +
            "', attnd_metric_lov_nm= '" + mtrcLOV.Replace("'", "''") +
            "', attnd_points_lov_nm = '" + pointsLov.Replace("'", "''") +
            "' " +
            "WHERE (event_id =" + evntid + ")";
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void deleteEvent(long evntid, string evntname)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Event Name = " + evntname;
      string delSQL = "DELETE FROM attn.attn_attendance_events WHERE event_id = " + evntid;
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }

    public static bool isEvntInUse(int evntid)
    {
      string strSql = "SELECT a.time_table_det_id " +
       "FROM attn.attn_time_table_details a " +
       "WHERE(a.event_id = " + evntid + ")";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return true;
      }

      return false;
    }

    public static int getEventID(string evntname, int orgid)
    {
      DataSet dtSt = new DataSet();
      string sqlStr = "select event_id from attn.attn_attendance_events where lower(event_name) = '" +
       evntname.Replace("'", "''").ToLower() + "' and org_id = " + orgid;
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

    public static long getEventCostID(long rgstrID, long src_doc_id, string src_doc_type)
    {
      DataSet dtSt = new DataSet();
      string sqlStr = "select attnd_cost_id from attn.attn_attendance_costs where lower(src_doc_type) = '" +
       src_doc_type.Replace("'", "''").ToLower() + "' and recs_hdr_id = " + rgstrID + " and src_doc_id = " + src_doc_id;
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

    public static string getMtrcSQL(int mtrcID)
    {
      DataSet dtSt = new DataSet();
      string sqlStr = "select rslt_query from attn.attn_attendance_events_mtrcs where rslt_metric_id = " + mtrcID;
      dtSt = Global.mnFrm.cmCde.selectDataNoParams(sqlStr);

      if (dtSt.Tables[0].Rows.Count > 0)
      {
        return dtSt.Tables[0].Rows[0][0].ToString();
      }
      else
      {
        return "";
      }
    }

    public static string computMtrcSQL(string strSQL, int evntID, string dte1, string dte2)
    {
      if (strSQL == "")
      {
        strSQL = "select 'N/A'";
      }
      strSQL = strSQL.Replace("{:evntID}", evntID.ToString()).Replace("{:strtDte}", dte1).Replace("{:endDte}", dte2);
      DataSet dtSt = new DataSet();
      dtSt = Global.mnFrm.cmCde.selectDataNoParams(strSQL);

      if (dtSt.Tables[0].Rows.Count > 0)
      {
        return dtSt.Tables[0].Rows[0][0].ToString();
      }
      else
      {
        return "";
      }
    }

    public static DataSet get_One_EvntDet(int evntid)
    {
      string strSql = @"SELECT a.event_id, a.event_name, a.event_desc, 
CASE WHEN a.event_typ='R' THEN ' R:RECURRING' ELSE 'NR:NON-RECURRING' END, a.host_prsn_id, 
      a.allwd_grp_typ, a.allwd_grp_id, a.ttl_tmtbl_sessn_mins, a.hghst_cntnuous_mins, a.slot_priority, 
      a.event_classification, a.preffrd_venue_id, a.is_enabled, a.attnd_metric_lov_nm, a.attnd_points_lov_nm " +
       "FROM attn.attn_attendance_events a " +
       "WHERE(a.event_id = " + evntid + ")";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      //Global.evntFrm.rec_SQL = strSql;
      return dtst;
    }

    public static DataSet get_One_EvntMtrcs(int evntid)
    {
      string strSql = @"SELECT rslt_metric_id, rslt_metric_name_desc, rslt_type, rslt_comment, 
       rslt_query, event_id, is_enabled
       FROM attn.attn_attendance_events_mtrcs a WHERE(a.event_id = " + evntid + ") ORDER BY 1";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      Global.evntFrm.recDt_SQL = strSql;
      return dtst;
    }

    public static DataSet get_One_EvntPrices(int evntid)
    {
      string strSql = @"SELECT price_ctgry_id, price_category, inv_itm_id, event_id, is_enabled
       FROM attn.event_price_categories a WHERE(a.event_id = " + evntid + ") ORDER BY 1";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      Global.evntFrm.recPrc_SQL = strSql;
      return dtst;
    }

    public static DataSet get_One_EvntEnbldMtrcs(int evntid)
    {
      string strSql = @"SELECT rslt_metric_id, rslt_metric_name_desc, rslt_type, rslt_comment, 
       rslt_query, event_id, is_enabled
       FROM attn.attn_attendance_events_mtrcs a WHERE(a.event_id = " + evntid + " and is_enabled='1') ORDER BY 1";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      Global.evntFrm.recDt_SQL = strSql;
      return dtst;
    }

    public static long getNewMtrcLnID()
    {
      string strSql = "select nextval('attn.attn_attendance_events_mtrcs_rslt_metric_id_seq')";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return -1;
    }

    public static long getNewPriceLnID()
    {
      string strSql = "select nextval('attn.event_price_categories_price_ctgry_id_seq')";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return -1;
    }

    public static long getNewRsltLnID()
    {
      string strSql = "select nextval('attn.attn_attendance_events_rslts_evnt_rslt_id_seq')";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return -1;
    }

    public static void deleteEvntMtrc(long lnID)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string delSQL = "DELETE FROM attn.attn_attendance_events_mtrcs WHERE rslt_metric_id = " +
        lnID + "";
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }

    public static void deletePriceMtrc(long lnID)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string delSQL = "DELETE FROM attn.event_price_categories WHERE price_ctgry_id = " +
        lnID + "";
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }

    public static void deleteAtndncMtrc(long lnID)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string delSQL = "DELETE FROM attn.attn_attendance_recs_cntr WHERE cntr_id = " +
        lnID + "";
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }

    public static long getAtndncMtrcCnt(string mrtcNm, long rgstrID)
    {
      if (mrtcNm == "Male Attendance")
      {
        mrtcNm = "Male%";
      }
      else if (mrtcNm == "Female Attendance")
      {
        mrtcNm = "Female%";
      }
      else
      {
        mrtcNm = "%";
      }
      string selSQL = @"select count(a.person_id) 
from attn.attn_attendance_recs a, prs.prsn_names_nos b
WHERE is_present='1' and a.person_id=b.person_id
and a.recs_hdr_id=" + rgstrID + " and b.gender ilike '" + mrtcNm + "'";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return 0;
    }

    public static DataSet get_Basic_Events(string searchWord, string searchIn,
 Int64 offset, int limit_size, int orgID)
    {
      string strSql = "";
      string whrcls = "";
      if (searchIn == "Event Name")
      {
        whrcls = " AND (a.event_name ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Event Description")
      {
        whrcls = " AND (a.event_desc ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      strSql = "SELECT a.event_id, a.event_name " +
"FROM attn.attn_attendance_events a " +
"WHERE ((a.org_id = " + orgID + ")" + whrcls + ") ORDER BY a.event_name LIMIT " + limit_size +
" OFFSET " + (Math.Abs(offset * limit_size)).ToString();

      Global.evntFrm.rec_SQL = strSql;
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      return dtst;
    }

    public static long get_Total_Events(string searchWord, string searchIn, int orgID)
    {
      string strSql = "";
      string whrcls = "";
      if (searchIn == "Event Name")
      {
        whrcls = " AND (a.event_name ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Event Description")
      {
        whrcls = " AND (a.event_desc ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      strSql = "SELECT count(1) " +
"FROM attn.attn_attendance_events a " +
"WHERE ((a.org_id = " + orgID + ")" + whrcls + ")";

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

    #region "TIME TABLES..."
    public static void createTimeTable(int orgid, string tmetblname,
string tmetbldesc, string eventClssf, bool isEnbld, int smllstslotdrtn,
      string mjrDivTyp, string mjrDivStrtV, string mjrDivEndV,
      string mnrDivTyp, string mnrDivStrtV, string mnrDivEndV)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = @"INSERT INTO attn.attn_time_table_hdrs(
            time_table_name, time_table_desc, smllst_slot_duratn_mins, 
            maj_tme_div_typ, maj_tme_div_start_val, maj_tme_div_end_val, 
            min_tme_div_typ, min_tme_div_start_val, min_tme_div_end_val, 
            created_by, creation_date, last_update_by, last_update_date, 
            events_classifction_usd, org_id, is_enabled) " +
            "VALUES ('" + tmetblname.Replace("'", "''") +
            "', '" + tmetbldesc.Replace("'", "''") +
            "', " + smllstslotdrtn +
            ", '" + mjrDivTyp.Replace("'", "''") +
            "', '" + mjrDivStrtV.Replace("'", "''") +
            "', '" + mjrDivEndV.Replace("'", "''") +
            "', '" + mnrDivTyp.Replace("'", "''") +
            "', '" + mnrDivStrtV.Replace("'", "''") +
            "', '" + mnrDivEndV.Replace("'", "''") +
            "', " + Global.myEvnt.user_id + ", '" + dateStr +
            "', " + Global.myEvnt.user_id + ", '" + dateStr +
            "', '" + eventClssf.Replace("'", "''") +
            "', " + orgid + ", '" +
            Global.mnFrm.cmCde.cnvrtBoolToBitStr(isEnbld) + "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void updateTimeTable(int tmtblid, string tmetblname,
string tmetbldesc, string eventClssf, bool isEnbld, int smllstslotdrtn,
      string mjrDivTyp, string mjrDivStrtV, string mjrDivEndV,
      string mnrDivTyp, string mnrDivStrtV, string mnrDivEndV)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE attn.attn_time_table_hdrs SET " +
            "time_table_name='" + tmetblname.Replace("'", "''") +
            "', time_table_desc='" + tmetbldesc.Replace("'", "''") +
            "', maj_tme_div_typ='" + mjrDivTyp.Replace("'", "''") +
            "', maj_tme_div_start_val='" + mjrDivStrtV.Replace("'", "''") +
            "', maj_tme_div_end_val='" + mjrDivEndV.Replace("'", "''") +
            "', min_tme_div_typ='" + mnrDivTyp.Replace("'", "''") +
            "', min_tme_div_start_val='" + mnrDivStrtV.Replace("'", "''") +
            "', min_tme_div_end_val='" + mnrDivEndV.Replace("'", "''") +
            "', smllst_slot_duratn_mins=" + smllstslotdrtn + " " +
            ", last_update_by=" + Global.myEvnt.user_id + ", " +
            "last_update_date='" + dateStr +
            "', events_classifction_usd='" + eventClssf.Replace("'", "''") + "', is_enabled='" +
            Global.mnFrm.cmCde.cnvrtBoolToBitStr(isEnbld) +
            "' " +
            "WHERE (time_table_id =" + tmtblid + ")";
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static long getNewTmTblDtID()
    {
      //string strSql = "select nextval('accb.accb_trnsctn_batches_batch_id_seq'::regclass);";
      string strSql = "select nextval('attn.time_table_details_time_table_det_id_seq')";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return -1;
    }

    public static long getTmTblDtID(long tmTblID, int evntID, string mjrTm, string mnrTm, long vnuID)
    {
      string strSql = @"select time_table_det_id from attn.attn_time_table_details 
      where time_table_id=" + tmTblID + " and event_id=" + evntID +
                            " and time_maj_div='" + mjrTm.Replace("'", "''") +
                            "' and time_min_div='" + mnrTm.Replace("'", "''") + "' and assgnd_venue_id=" + vnuID;
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return -1;
    }

    public static void createTimeTableDetLn(long tmtblDetID, int tmtblid, int evntID,
string tmeMajDiv, string tmeMinDiv, bool isEnbld, long hostID, int vnuID,
string tmeMajDivEnd, string tmeMinDivEnd)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = @"INSERT INTO attn.attn_time_table_details(
            time_table_det_id, time_table_id, event_id, time_maj_div, time_min_div, 
            assgnd_host_id, created_by, creation_date, last_update_by, last_update_date, 
            assgnd_venue_id, is_enabled, time_maj_div_end, time_min_div_end) " +
            "VALUES (" + tmtblDetID + ", " + tmtblid + ", " + evntID + ", '" + tmeMajDiv.Replace("'", "''") +
            "', '" + tmeMinDiv.Replace("'", "''") +
            "', " + hostID + ", " + Global.myEvnt.user_id + ", '" + dateStr +
            "', " + Global.myEvnt.user_id + ", '" + dateStr +
            "', " + vnuID + ", '" +
            Global.mnFrm.cmCde.cnvrtBoolToBitStr(isEnbld) + "', '" + tmeMajDivEnd.Replace("'", "''") +
            "', '" + tmeMinDivEnd.Replace("'", "''") +
            "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void updtTimeTableDetLn(long tmtblDetLnid, int tmtblid, int evntID,
string tmeMajDiv, string tmeMinDiv, bool isEnbld, long hostID, int vnuID,
string tmeMajDivEnd, string tmeMinDivEnd)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = @"UPDATE attn.attn_time_table_details SET 
            time_table_id=" + tmtblid + ", event_id=" + evntID +
            ", time_maj_div='" + tmeMajDiv.Replace("'", "''") +
            "', time_min_div='" + tmeMinDiv.Replace("'", "''") +
            "', assgnd_host_id=" + hostID + ", last_update_by=" + Global.myEvnt.user_id +
            ", last_update_date='" + dateStr +
            "', assgnd_venue_id=" + vnuID + ", is_enabled='" +
            Global.mnFrm.cmCde.cnvrtBoolToBitStr(isEnbld) +
            "', time_maj_div_end='" + tmeMajDivEnd.Replace("'", "''") +
            "', time_min_div_end='" + tmeMinDivEnd.Replace("'", "''") +
            "' " +
            "WHERE time_table_det_id=" + tmtblDetLnid + " ";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void deleteTimeTable(long tmetblid, string tmtblNm)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Time Table Name = " + tmtblNm;
      string delSQL = "DELETE FROM attn.attn_time_table_hdrs WHERE time_table_id = " + tmetblid;
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }

    public static bool isTmeTblInUse(int tmetblid)
    {
      string strSql = "SELECT a.time_table_det_id " +
       "FROM attn.attn_time_table_details a " +
       "WHERE(a.time_table_id = " + tmetblid + ")";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return true;
      }

      strSql = "SELECT a.time_table_det_id " +
       "FROM attn.temp_time_table_details a " +
       "WHERE(a.time_table_id = " + tmetblid + ")";
      dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return true;
      }
      strSql = "SELECT a.recs_hdr_id " +
       "FROM attn.attn_attendance_recs_hdr a " +
       "WHERE(a.time_table_id = " + tmetblid + ")";
      dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return true;
      }
      return false;
    }

    public static void deleteTimeTableDLn(long tmetblLnid, string EventNm)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Time Table Event Name = " + EventNm;
      string delSQL = "DELETE FROM attn.attn_time_table_details WHERE time_table_det_id = " + tmetblLnid;
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }

    public static bool isTmeTblLnInUse(long tmetblLnid)
    {
      string strSql = "SELECT a.recs_hdr_id " +
       "FROM attn.attn_attendance_recs_hdr a " +
       "WHERE(a.time_table_det_id = " + tmetblLnid + ")";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return true;
      }
      return false;
    }

    public static int getTmeTblID(string tmeTblname, int orgid)
    {
      DataSet dtSt = new DataSet();
      string sqlStr = "select time_table_id from attn.attn_time_table_hdrs where lower(time_table_name) = '" +
       tmeTblname.Replace("'", "''").ToLower() + "' and org_id = " + orgid;
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

    public static DataSet get_One_TmeTblHdrNEvnts(int tmetblid, int lmit)
    {
      string extrWhr = "";

      if (lmit >= 0)
      {
        extrWhr = " LIMIT " + lmit + @" OFFSET 0";
      }
      else if (lmit < 0)
      {
        extrWhr = "";
      }

      string strSql = @"SELECT b.event_name, 
        b.event_desc, b.event_typ, b.allwd_grp_typ,
       CASE WHEN b.allwd_grp_typ='Divisions/Groups' THEN 
       org.get_div_name(b.allwd_grp_id)
        WHEN b.allwd_grp_typ='Grade' THEN 
               org.get_div_name(b.allwd_grp_id)
        WHEN b.allwd_grp_typ='Job' THEN 
               org.get_div_name(b.allwd_grp_id)
        WHEN b.allwd_grp_typ='Position' THEN 
               org.get_div_name(b.allwd_grp_id)
        WHEN b.allwd_grp_typ='Site/Location' THEN 
               org.get_div_name(b.allwd_grp_id)
        WHEN b.allwd_grp_typ='Person Type' THEN 
               org.get_div_name(b.allwd_grp_id)
        ELSE 
            ''
        END grp_nm
      , b.ttl_tmtbl_sessn_mins, b.hghst_cntnuous_mins, b.slot_priority, 
       b.event_classification, b.allwd_group_nm, 
       b.attnd_metric_lov_nm, b.attnd_points_lov_nm, 
       a.event_id, a.time_maj_div, a.time_min_div, 
       prs.get_prsn_loc_id(a.assgnd_host_id), 
       (SELECT venue_name FROM attn.attn_event_venues d WHERE d.venue_id = a.assgnd_venue_id), 
       CASE WHEN a.is_enabled='1' THEN 'YES' ELSE 'NO' END, a.time_maj_div_end, a.time_min_div_end
       FROM attn.attn_time_table_details a, attn.attn_attendance_events b 
       WHERE(a.event_id = b.event_id and a.time_table_id = " + tmetblid + ")" + extrWhr;

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      //Global.taxFrm.rec_SQL = strSql;
      return dtst;
    }

    public static int get_GroupID(string grpTyp, string grpNm)
    {
      string strSql = @"SELECT org.get_criteria_id('" + grpNm.Replace("'", "''") + @"','" + grpTyp.Replace("'", "''") + @"') grp_id";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      //Global.taxFrm.rec_SQL = strSql;
      return -1;
    }

    public static int get_PriceCtgryID(string ctgryNm, int eventID)
    {
      string strSql = @"SELECT price_ctgry_id from attn.event_price_categories where event_id=" + eventID +
        " and price_category='" + ctgryNm.Replace("'", "''") + "'";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      //Global.taxFrm.rec_SQL = strSql;
      return -1;
    }
    public static int get_EventMtrcID(string mtrcNm, int eventID)
    {
      string strSql = @"SELECT rslt_metric_id from attn.attn_attendance_events_mtrcs where event_id=" + eventID +
        " and rslt_metric_name_desc='" + mtrcNm.Replace("'", "''") + "'";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      //Global.taxFrm.rec_SQL = strSql;
      return -1;
    }

    public static DataSet get_One_TmeTblHdrDet(int tmetblid)
    {
      string strSql = @"SELECT a.time_table_id, a.time_table_name, 
      a.time_table_desc, a.smllst_slot_duratn_mins, 
       a.maj_tme_div_typ, a.maj_tme_div_start_val, a.maj_tme_div_end_val, 
       a.min_tme_div_typ, a.min_tme_div_start_val, a.min_tme_div_end_val, 
       a.events_classifction_usd, a.is_enabled
  FROM attn.attn_time_table_hdrs a " +
       "WHERE(a.time_table_id = " + tmetblid + ")";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      //Global.taxFrm.rec_SQL = strSql;
      return dtst;
    }

    public static DataSet get_Basic_TmeTbl(string searchWord, string searchIn,
 Int64 offset, int limit_size, int orgID)
    {
      string strSql = "";
      string whrcls = "";
      if (searchIn == "Time Table Name")
      {
        whrcls = " AND (a.time_table_name ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Time Table Description")
      {
        whrcls = " AND (a.time_table_desc ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      strSql = "SELECT a.time_table_id, a.time_table_name " +
"FROM attn.attn_time_table_hdrs a " +
"WHERE ((a.org_id = " + orgID + ")" + whrcls + ") ORDER BY a.time_table_name LIMIT " + limit_size +
" OFFSET " + (Math.Abs(offset * limit_size)).ToString();

      Global.tmtblFrm.rec_SQL = strSql;
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      return dtst;
    }

    public static long get_Total_TmeTbl(string searchWord, string searchIn, int orgID)
    {
      string strSql = "";
      string whrcls = "";
      if (searchIn == "Time Table Name")
      {
        whrcls = " AND (a.time_table_name ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Time Table Description")
      {
        whrcls = " AND (a.time_table_desc ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      strSql = "SELECT count(1) " +
"FROM attn.attn_time_table_hdrs a " +
"WHERE ((a.org_id = " + orgID + ")" + whrcls + ")";
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

    public static DataSet get_One_TmeTbl_DetLns(string searchWord, string searchIn, long offset, int limit_size, long tmtblID)
    {
      string strSql = "";
      string whrcls = "";
      /*
       *  Event Name
          Host Person Name
          Venue Name
          Major Time Division
          Minor Time Division
       * 
       */
      if (searchIn == "Event Name")
      {
        whrcls = " AND ((SELECT event_name FROM attn.attn_attendance_events d WHERE d.event_id = a.event_id) ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Host Person Name")
      {
        whrcls = " AND (prs.get_prsn_name(a.assgnd_host_id) ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Venue Name")
      {
        whrcls = " AND ((SELECT venue_name FROM attn.attn_event_venues d WHERE d.venue_id = a.assgnd_venue_id) ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Major Time Division")
      {
        whrcls = " AND (a.time_maj_div ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Minor Time Division")
      {
        whrcls = " AND (a.time_min_div ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      strSql = "SELECT a.time_table_det_id, a.event_id, " +
        "(SELECT event_name FROM attn.attn_attendance_events d WHERE d.event_id = a.event_id) evntnm, a.time_maj_div, " +
        "a.time_min_div, a.assgnd_host_id, prs.get_prsn_name(a.assgnd_host_id) hstnm, a.assgnd_venue_id, " +
        "(SELECT venue_name FROM attn.attn_event_venues d WHERE d.venue_id = a.assgnd_venue_id) vnunm, is_enabled, a.time_maj_div_end, " +
        "a.time_min_div_end " +
        "FROM attn.attn_time_table_details a " +
        "WHERE((a.time_table_id = " + tmtblID + ")" + whrcls + ") ORDER BY a.time_table_det_id DESC LIMIT " + limit_size +
        " OFFSET " + (Math.Abs(offset * limit_size)).ToString();

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      Global.tmtblFrm.rec_det_SQL = strSql;
      return dtst;
    }

    public static string getMonthNum(string mnth)
    {
      string reslts = "";
      switch (mnth)
      {
        case "JAN":
          reslts = "01";
          break;
        case "FEB":
          reslts = "02";
          break;
        case "MAR":
          reslts = "03";
          break;
        case "APR":
          reslts = "04";
          break;
        case "MAY":
          reslts = "05";
          break;
        case "JUN":
          reslts = "06";
          break;
        case "JUL":
          reslts = "07";
          break;
        case "AUG":
          reslts = "08";
          break;
        case "SEP":
          reslts = "09";
          break;
        case "OCT":
          reslts = "10";
          break;
        case "NOV":
          reslts = "11";
          break;
        case "DEC":
          reslts = "12";
          break;
        default:
          reslts = mnth;
          break;
      }
      return reslts + "-" + mnth;
    }

    public static long get_Total_TmeTbl_DetLns(string searchWord, string searchIn, long tmtblID)
    {
      string strSql = "";
      string whrcls = "";
      /*
       *  Event Name
          Host Person Name
          Venue Name
          Major Time Division
          Minor Time Division
       * 
       */
      if (searchIn == "Event Name")
      {
        whrcls = " AND ((SELECT event_name FROM attn.attn_attendance_events d WHERE d.event_id = a.event_id) ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Host Person Name")
      {
        whrcls = " AND (prs.get_prsn_name(a.assgnd_host_id) ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Venue Name")
      {
        whrcls = " AND ((SELECT venue_name FROM attn.attn_event_venues d WHERE d.venue_id = a.assgnd_venue_id) ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Major Time Division")
      {
        whrcls = " AND (a.time_maj_div ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Minor Time Division")
      {
        whrcls = " AND (a.time_min_div ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      strSql = "SELECT count(1) " +
        "FROM attn.attn_time_table_details a " +
        "WHERE((a.time_table_id = " + tmtblID + ")" + whrcls + ")";

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

    #region "EVENT COSTS..."
    public static void createAttnCostLn(long rgstrid, long srcDcID,
string srcDocType, string attnCmmnts, int noPrsns, int noDays,
      double unitCost, string costClsf)
    {
      //visitor_name_desc, no_of_adults, no_of_chldrn, customer_id, visitor_classfctn
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = @"INSERT INTO attn.attn_attendance_costs(
            recs_hdr_id, src_doc_id, src_doc_type, created_by, 
            creation_date, last_update_by, last_update_date, cost_comments, 
            no_of_persons, no_of_days, unit_cost, cost_classfctn) " +
            "VALUES (" + rgstrid + ", " + srcDcID + ", '" + srcDocType.Replace("'", "''") +
            "', " + Global.myEvnt.user_id + ", '" + dateStr +
            "', " + Global.myEvnt.user_id + ", '" + dateStr +
            "', '" + attnCmmnts.Replace("'", "''") +
            "', " + noPrsns +
            ", " + noDays +
            ", " + unitCost +
            ", '" + costClsf.Replace("'", "''") +
            "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void updtAttnCostLn(long costLnid, long rgstrid, long srcDcID,
string srcDocType, string attnCmmnts, int noPrsns, int noDays,
      double unitCost, string costClsf)
    {
      //visitor_name_desc, no_of_adults, no_of_chldrn, customer_id, visitor_classfctn
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string insSQL = @"UPDATE attn.attn_attendance_costs SET 
            recs_hdr_id=" + rgstrid + ", src_doc_id=" + srcDcID +
                          ", src_doc_type='" + srcDocType.Replace("'", "''") +
            "', last_update_by=" + Global.myEvnt.user_id +
            ", last_update_date='" + dateStr +
            "', cost_comments='" + attnCmmnts.Replace("'", "''") +
            "', no_of_persons=" + noPrsns + ", no_of_days=" + noDays +
            ", unit_cost=" + unitCost + ", cost_classfctn='" + costClsf.Replace("'", "''") +
            "' " +
            "WHERE attnd_cost_id=" + costLnid + " ";
      Global.mnFrm.cmCde.updateDataNoParams(insSQL);
    }

    public static void deleteAttnCostLn(long costLnid, string costDesc)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Cost Description = " + costDesc;
      string delSQL = "DELETE FROM attn.attn_attendance_costs WHERE attnd_cost_id = " + costLnid;
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }

    public static bool isAttnCostLnInUse(long tmetblLnid)
    {
      return false;
    }

    public static DataSet getEvntInvoices(long rgstrID)
    {

      string selSQL = @"SELECT invc_hdr_id, invc_number, invc_type, comments_desc, 
scm.get_doc_smry_typ_amnt(invc_hdr_id, invc_type,'5Grand Total'), evnt_cost_category, COALESCE(a.no_of_adults,1)
  FROM scm.scm_sales_invc_hdr y LEFT OUTER JOIN hotl.checkins_hdr a ON ((a.check_in_id = y.other_mdls_doc_id 
or (a.prnt_chck_in_id=y.other_mdls_doc_id and y.other_mdls_doc_id>0))
and (a.doc_type=y.other_mdls_doc_type or (a.prnt_doc_typ=y.other_mdls_doc_type and a.prnt_doc_typ != '')))
  WHERE approval_status='Approved' and event_doc_type='Attendance Register' and event_rgstr_id=" + rgstrID;
      return Global.mnFrm.cmCde.selectDataNoParams(selSQL);
    }

    public static void deleteCancelled(long rgstrID)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string delSQL = @"DELETE FROM attn.attn_attendance_costs WHERE src_doc_id IN (SELECT invc_hdr_id
 FROM scm.scm_sales_invc_hdr y LEFT OUTER JOIN hotl.checkins_hdr a ON ((a.check_in_id = y.other_mdls_doc_id 
or (a.prnt_chck_in_id=y.other_mdls_doc_id and y.other_mdls_doc_id>0))
and (a.doc_type=y.other_mdls_doc_type or (a.prnt_doc_typ=y.other_mdls_doc_type and a.prnt_doc_typ != '')))
  WHERE approval_status!='Approved' and event_doc_type='Attendance Register' and event_rgstr_id=" + rgstrID + ") and src_doc_type='Sales Invoice'";
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      delSQL = @"DELETE FROM attn.attn_attendance_costs WHERE src_doc_id IN (SELECT pybls_invc_hdr_id 
  FROM accb.accb_pybls_invc_hdr 
  WHERE event_rgstr_id=" + rgstrID + @" and event_doc_type='Attendance Register' 
  and approval_status != 'Approved') and src_doc_type ilike '%Supplier%'";
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      delSQL = @"DELETE FROM attn.attn_attendance_costs WHERE src_doc_id IN (SELECT rcvbls_invc_hdr_id 
  FROM accb.accb_rcvbls_invc_hdr 
  WHERE event_rgstr_id=" + rgstrID + @" and event_doc_type='Attendance Register' 
  and approval_status != 'Approved') and src_doc_type ilike '%Customer%'";
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }
    public static double getEvntAmntPaid(long rgstrID, long cstmrID)
    {
      string selSQL = @"SELECT SUM(a.smmry_amnt)
FROM scm.scm_doc_amnt_smmrys a 
WHERE((a.src_doc_hdr_id IN (SELECT DISTINCT y.invc_hdr_id 
FROM hotl.checkins_hdr a 
LEFT OUTER JOIN hotl.service_types d ON (a.service_type_id=d.service_type_id )
LEFT OUTER JOIN hotl.rooms b ON (a.service_det_id = b.room_id)
LEFT OUTER JOIN scm.scm_sales_invc_hdr y ON ((a.check_in_id = y.other_mdls_doc_id or (a.prnt_chck_in_id=y.other_mdls_doc_id and y.other_mdls_doc_id>0))
and (a.doc_type=y.other_mdls_doc_type or (a.prnt_doc_typ=y.other_mdls_doc_type and a.prnt_doc_typ != ''))) WHERE (a.sponsor_id IN (select c.cust_sup_id from scm.scm_cstmr_suplr c where c.cust_sup_name ilike '%') or a.customer_id IN (select c.cust_sup_id from scm.scm_cstmr_suplr c where c.cust_sup_name ilike '%')) and COALESCE(d.org_id, 1)=1 and a.doc_type IN ('Booking','Check-In') and a.fclty_type IN ('Event') 
and a.customer_id = " + cstmrID + " and y.event_doc_type='Attendance Register' and y.event_rgstr_id=" + rgstrID + @")) and (a.src_doc_type='Sales Invoice')
 and a.smmry_name = 'Total Payments Received')";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      if (dtst.Tables[0].Rows.Count >= 1)
      {
        return double.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return 0.00;
    }

    public static DataSet getEvntPayables(long rgstrID)
    {

      string selSQL = @"SELECT pybls_invc_hdr_id, pybls_invc_number, pybls_invc_type, comments_desc, 
invoice_amount, evnt_cost_category, 1 
  FROM accb.accb_pybls_invc_hdr 
  WHERE event_rgstr_id=" + rgstrID + " and event_doc_type='Attendance Register' and approval_status = 'Approved'";

      return Global.mnFrm.cmCde.selectDataNoParams(selSQL);
    }

    public static DataSet getEvntReceivables(long rgstrID)
    {

      string selSQL = @"SELECT rcvbls_invc_hdr_id, rcvbls_invc_number, rcvbls_invc_type, comments_desc, 
invoice_amount, evnt_cost_category, 1 
  FROM accb.accb_rcvbls_invc_hdr 
  WHERE event_rgstr_id=" + rgstrID + " and event_doc_type='Attendance Register' and approval_status = 'Approved'";

      return Global.mnFrm.cmCde.selectDataNoParams(selSQL);
    }

    public static DataSet get_One_AttnCostLns(string searchWord, string searchIn, long offset,
      int limit_size, long rgstrHdrID)
    {
      string strSql = "";
      string whrcls = "";
      /*
       *  Description
Source Document No.
Category
       * 
       */
      if (searchIn == "Description")
      {
        whrcls = " AND (a.cost_comments ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Source Document No.")
      {
        whrcls = " AND (scm.get_src_doc_num(a.src_doc_id, a.src_doc_type) ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Category")
      {
        whrcls = " AND (a.cost_classfctn ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      strSql = @"SELECT a.attnd_cost_id, a.recs_hdr_id, scm.get_src_doc_num(a.src_doc_id, a.src_doc_type), 
       a.src_doc_id, a.src_doc_type, a.cost_comments, 
       a.no_of_persons, a.no_of_days, a.unit_cost, (a.no_of_persons*a.unit_cost) ttlamount, 
       cost_classfctn, a.gl_batch_id, a.incrs_dcrs1, a.asset_expns_acnt_id, a.incrs_dcrs2, a.asset_lblty_acnt_id,
       attn.isEvntCostHanging(a.recs_hdr_id,a.attnd_cost_id) 
       FROM attn.attn_attendance_costs a " +
       "WHERE((a.recs_hdr_id = " + rgstrHdrID + ")" + whrcls + ") ORDER BY 11, 1 LIMIT " + limit_size +
       " OFFSET " + (Math.Abs(offset * limit_size)).ToString();

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      Global.attndFrm.rec_Cost_SQL = strSql;
      return dtst;
    }

    public static string getBatchNm(long batchid)
    {
      string strSql = "";
      strSql = "SELECT a.batch_name " +
   "FROM accb.accb_trnsctn_batches a " +
      "WHERE ((a.batch_id = " + batchid + "))";

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
    public static long get_Total_AttnCostLns(string searchWord, string searchIn, long rgstrHdrID)
    {
      string strSql = "";
      string whrcls = "";
      /*
       *  Description
Source Document No.
Category
       * 
       */
      if (searchIn == "Description")
      {
        whrcls = " AND (a.cost_comments ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Source Document No.")
      {
        whrcls = " AND (scm.get_src_doc_num(a.src_doc_id, a.src_doc_type) ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Category")
      {
        whrcls = " AND (a.cost_classfctn ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      strSql = @"SELECT count(1)
  FROM attn.attn_attendance_costs a " +
        "WHERE((a.recs_hdr_id = " + rgstrHdrID + ")" + whrcls + ")";

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

    #region "ATTENDANCE REGISTERS..."
    public static void createAttnMtrcCnt(long cntrID, long rgstrid, string mtrcname,
string cmmntdesc, long rsltVal, int pssblValID)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = @"INSERT INTO attn.attn_attendance_recs_cntr(
            cntr_id, recs_hdr_id, metric_name_desc, cntr_result, 
            rslt_comment, created_by, creation_date, last_update_by, last_update_date, 
            lnkd_pssbl_val_id) " +
            "VALUES (" + cntrID +
            "," + rgstrid +
            ", '" + mtrcname.Replace("'", "''") +
            "', " + rsltVal +
            ", '" + cmmntdesc.Replace("'", "''") +
            "', " + Global.myEvnt.user_id + ", '" + dateStr +
            "', " + Global.myEvnt.user_id + ", '" + dateStr +
            "', " + pssblValID + ")";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void updateAttnMtrcCnt(long cntrid, string mtrcname,
string cmmntdesc, long rsltVal, int pssblValID)
    {

      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE attn.attn_attendance_recs_cntr SET " +
            "metric_name_desc='" + mtrcname.Replace("'", "''") +
            "', rslt_comment='" + cmmntdesc.Replace("'", "''") +
            "', cntr_result=" + rsltVal +
            ", lnkd_pssbl_val_id=" + pssblValID +
            ", last_update_by=" + Global.myEvnt.user_id + ", " +
            "last_update_date='" + dateStr +
            "' WHERE (cntr_id =" + cntrid + ")";
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void createAttnRgstr(int orgid, string rgstrname,
string rgstrdesc, int tmtblID, long tmtblDetID,
      string evntDate, string endDte)
    {
      evntDate = DateTime.ParseExact(
evntDate, "dd-MMM-yyyy HH:mm:ss",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

      endDte = DateTime.ParseExact(
endDte, "dd-MMM-yyyy HH:mm:ss",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = @"INSERT INTO attn.attn_attendance_recs_hdr(
            recs_hdr_name, recs_hdr_desc, time_table_id, time_table_det_id, 
            created_by, creation_date, last_update_by, last_update_date, 
            org_id, event_date, end_date) " +
            "VALUES ('" + rgstrname.Replace("'", "''") +
            "', '" + rgstrdesc.Replace("'", "''") +
            "', " + tmtblID +
            ", " + tmtblDetID +
            ", " + Global.myEvnt.user_id + ", '" + dateStr +
            "', " + Global.myEvnt.user_id + ", '" + dateStr +
            "', " + orgid + ", '" +
            evntDate + "', '" +
            endDte + "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void updateAttnRgstr(long rgstrid, string rgstrname,
string rgstrdesc, int tmtblID, long tmtblDetID,
      string evntDate, string endDte)
    {
      evntDate = DateTime.ParseExact(
evntDate, "dd-MMM-yyyy HH:mm:ss",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      endDte = DateTime.ParseExact(
endDte, "dd-MMM-yyyy HH:mm:ss",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE attn.attn_attendance_recs_hdr SET " +
            "recs_hdr_name='" + rgstrname.Replace("'", "''") +
            "', recs_hdr_desc='" + rgstrdesc.Replace("'", "''") +
            "', time_table_id=" + tmtblID +
            ", time_table_det_id=" + tmtblDetID +
            ", last_update_by=" + Global.myEvnt.user_id + ", " +
            "last_update_date='" + dateStr +
            "', event_date='" + evntDate + "' " +
            ", end_date='" + endDte + "' " +
            "WHERE (recs_hdr_id =" + rgstrid + ")";
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void createAttnRgstrDetLn(long rgstrid, long psrnID,
string dtetmein, string dtetmeout, bool isPresent, string attnCmmnts, string name_desc,
      int noAdlts, long cstmrID, string vstrClsf, long sponsorID)
    {
      //visitor_name_desc, no_of_adults, no_of_chldrn, customer_id, visitor_classfctn
      if (dtetmein != "")
      {
        dtetmein = DateTime.ParseExact(
  dtetmein, "dd-MMM-yyyy HH:mm:ss",
  System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      }
      if (dtetmeout != "")
      {
        dtetmeout = DateTime.ParseExact(
  dtetmeout, "dd-MMM-yyyy HH:mm:ss",
  System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      }
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = @"INSERT INTO attn.attn_attendance_recs(
            recs_hdr_id, person_id, date_time_in, date_time_out, 
            created_by, creation_date, last_update_by, last_update_date, 
            is_present, attn_comments,visitor_name_desc, no_of_persons, customer_id, visitor_classfctn, sponsor_id) " +
            "VALUES (" + rgstrid + ", " + psrnID + ", '" + dtetmein.Replace("'", "''") +
            "', '" + dtetmeout.Replace("'", "''") +
            "', " + Global.myEvnt.user_id + ", '" + dateStr +
            "', " + Global.myEvnt.user_id + ", '" + dateStr +
            "', '" +
            Global.mnFrm.cmCde.cnvrtBoolToBitStr(isPresent) + "', '" + attnCmmnts.Replace("'", "''") +
            "', '" + name_desc.Replace("'", "''") +
            "', " + noAdlts +
        //", " + noChdn +
            ", " + cstmrID +
            ", '" + vstrClsf.Replace("'", "''") +
            "'," + sponsorID + ")";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void updtAttnRgstrDetLn(long rgstrDetLnid, long rgstrid, long psrnID,
string dtetmein, string dtetmeout, bool isPresent, string attnCmmnts, string name_desc,
      int noAdlts, long cstmrID, string vstrClsf, long sponsor_id)
    {
      //visitor_name_desc, no_of_adults, no_of_chldrn, customer_id, visitor_classfctn
      if (dtetmein != "")
      {
        dtetmein = DateTime.ParseExact(
  dtetmein, "dd-MMM-yyyy HH:mm:ss",
  System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      }
      if (dtetmeout != "")
      {
        dtetmeout = DateTime.ParseExact(
  dtetmeout, "dd-MMM-yyyy HH:mm:ss",
  System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      }
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string insSQL = @"UPDATE attn.attn_attendance_recs SET 
            recs_hdr_id=" + rgstrid + ", person_id=" + psrnID + ", date_time_in='" + dtetmein.Replace("'", "''") +
            "', date_time_out='" + dtetmeout.Replace("'", "''") +
            "', last_update_by=" + Global.myEvnt.user_id +
            ", last_update_date='" + dateStr +
            "', is_present='" +
            Global.mnFrm.cmCde.cnvrtBoolToBitStr(isPresent) +
            "', attn_comments='" + attnCmmnts.Replace("'", "''") +
            "',visitor_name_desc='" + name_desc.Replace("'", "''") +
            "', no_of_persons=" + noAdlts + ", customer_id=" + cstmrID + ", visitor_classfctn='" + vstrClsf.Replace("'", "''") +
            "', sponsor_id = " + sponsor_id +
            " WHERE attnd_rec_id=" + rgstrDetLnid + " ";
      Global.mnFrm.cmCde.updateDataNoParams(insSQL);
    }

    public static void updtAttnRgstrDetLn1(long attnRecID,
string dtetmein, string dtetmeout, bool isPresent, string attnCmmnts)
    {
      //visitor_name_desc, no_of_adults, no_of_chldrn, customer_id, visitor_classfctn
      if (dtetmein != "")
      {
        dtetmein = DateTime.ParseExact(
  dtetmein, "dd-MMM-yyyy HH:mm:ss",
  System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      }
      if (dtetmeout != "")
      {
        dtetmeout = DateTime.ParseExact(
  dtetmeout, "dd-MMM-yyyy HH:mm:ss",
  System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      }
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string insSQL = @"UPDATE attn.attn_attendance_recs SET 
            date_time_in='" + dtetmein.Replace("'", "''") +
            "', date_time_out='" + dtetmeout.Replace("'", "''") +
            "', last_update_by=" + Global.myEvnt.user_id +
            ", last_update_date='" + dateStr +
            "', is_present='" +
            Global.mnFrm.cmCde.cnvrtBoolToBitStr(isPresent) +
            "', attn_comments='" + attnCmmnts.Replace("'", "''") +
            "' WHERE attnd_rec_id=" + attnRecID + " ";
      Global.mnFrm.cmCde.updateDataNoParams(insSQL);
    }
    public static void createAttnRgstrTimeLn(long attnRecID,
string dtetmein, string dtetmeout, bool isPresent, string attnCmmnts)
    {
      //visitor_name_desc, no_of_adults, no_of_chldrn, customer_id, visitor_classfctn
      if (dtetmein != "")
      {
        dtetmein = DateTime.ParseExact(
  dtetmein, "dd-MMM-yyyy HH:mm:ss",
  System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      }
      if (dtetmeout != "")
      {
        dtetmeout = DateTime.ParseExact(
  dtetmeout, "dd-MMM-yyyy HH:mm:ss",
  System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      }
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = @"INSERT INTO attn.attn_attendance_recs_times(
            attnd_rec_id, date_time_in, date_time_out, 
            created_by, creation_date, last_update_by, last_update_date, 
            is_present, attn_comments) " +
            "VALUES (" + attnRecID + ", '" + dtetmein.Replace("'", "''") +
            "', '" + dtetmeout.Replace("'", "''") +
            "', " + Global.myEvnt.user_id + ", '" + dateStr +
            "', " + Global.myEvnt.user_id + ", '" + dateStr +
            "', '" +
            Global.mnFrm.cmCde.cnvrtBoolToBitStr(isPresent) + "', '" + attnCmmnts.Replace("'", "''") +
            "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }
    public static void updtAttnRgstrTimeLn(long attnDetLnid,
string dtetmein, string dtetmeout, bool isPresent, string attnCmmnts)
    {
      //visitor_name_desc, no_of_adults, no_of_chldrn, customer_id, visitor_classfctn
      if (dtetmein != "")
      {
        dtetmein = DateTime.ParseExact(
  dtetmein, "dd-MMM-yyyy HH:mm:ss",
  System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      }
      if (dtetmeout != "")
      {
        dtetmeout = DateTime.ParseExact(
  dtetmeout, "dd-MMM-yyyy HH:mm:ss",
  System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      }
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string insSQL = @"UPDATE attn.attn_attendance_recs_times SET 
            date_time_in='" + dtetmein.Replace("'", "''") +
            "', date_time_out='" + dtetmeout.Replace("'", "''") +
            "', last_update_by=" + Global.myEvnt.user_id +
            ", last_update_date='" + dateStr +
            "', is_present='" +
            Global.mnFrm.cmCde.cnvrtBoolToBitStr(isPresent) +
            "', attn_comments='" + attnCmmnts.Replace("'", "''") +
            "' WHERE attnd_det_rec_id=" + attnDetLnid + " ";
      Global.mnFrm.cmCde.updateDataNoParams(insSQL);
    }

    public static void createCstSplrRec(int orgid, string cstmrname,
  string cstmrdesc, string cstmrTyp, string clssfctn,
    int pyblAccntID, int rcvblAccntID, long prsnID, string gender, string dob)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      dob = DateTime.ParseExact(
          dob, "dd-MMM-yyyy",
          System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      string insSQL = "INSERT INTO scm.scm_cstmr_suplr(" +
            "cust_sup_name, cust_sup_desc, created_by, creation_date, last_update_by, last_update_date, " +
            "cust_sup_clssfctn, cust_or_sup, org_id, dflt_pybl_accnt_id, dflt_rcvbl_accnt_id, " +
            "lnkd_prsn_id,person_gender,dob_estblshmnt) " +
            "VALUES ('" + cstmrname.Replace("'", "''") +
            "', '" + cstmrdesc.Replace("'", "''") +
            "', " + Global.myEvnt.user_id + ", '" + dateStr +
            "', " + Global.myEvnt.user_id + ", '" + dateStr +
            "', '" + clssfctn.Replace("'", "''") +
            "', '" + cstmrTyp.Replace("'", "''") + "', " +
            orgid + ", " +
            pyblAccntID + ", " +
            rcvblAccntID + ", " + prsnID + ",'" + gender.Replace("'", "''") + "','" + dob.Replace("'", "''") + "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void createCstSplrSiteRec(long cstmrID, string sitename,
  string sitedesc, string cntctPrsn, string cntctNos, string email,
      string bankNm, string bnkBrnch, string accNum, string blngAddrs,
      string shpngAddrs, int taxCode, int dscntCode, string swift_code,
             string nationality, string national_id_typ,
     string id_number, string date_issued, string expiry_date,
             string other_info)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
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
            "', " + Global.myEvnt.user_id + ", '" + dateStr +
            "', " + Global.myEvnt.user_id + ", '" + dateStr +
            "', '" + sitename.Replace("'", "''") +
            "', '" + sitedesc.Replace("'", "''") + "', '" +
            bankNm.Replace("'", "''") + "', '" + bnkBrnch.Replace("'", "''") +
            "', '" + accNum.Replace("'", "''") + "', " + taxCode + ", " + dscntCode +
            ", '" + blngAddrs.Replace("'", "''") + "', '" + shpngAddrs.Replace("'", "''") +
            "', '" + swift_code.Replace("'", "''") + "', '" + nationality.Replace("'", "''") +
            "', '" + national_id_typ.Replace("'", "''") + "', '" + id_number.Replace("'", "''") +
            "', '" + date_issued.Replace("'", "''") + "', '" + expiry_date.Replace("'", "''") +
            "', '" + other_info.Replace("'", "''") + "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static long get_One_CstmrID(string cstmrNm)
    {
      string strSql = "SELECT a.cust_sup_id " +
       "FROM scm.scm_cstmr_suplr a " +
       "WHERE(a.cust_sup_name = '" + cstmrNm.Replace("'", "''") + "')";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      //Global.taxFrm.rec_SQL = strSql;
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return -1;
    }

    public static string get_One_CstmrNm(long cstmrID)
    {
      string strSql = "SELECT a.cust_sup_name " +
       "FROM scm.scm_cstmr_suplr a " +
       "WHERE(a.cust_sup_id = " + cstmrID + ")";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      //Global.taxFrm.rec_SQL = strSql;
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return dtst.Tables[0].Rows[0][0].ToString();
      }
      return "";
    }

    public static string get_One_CstmrSiteNm(int siteID)
    {
      string strSql = "SELECT a.site_name " +
       "FROM scm.scm_cstmr_suplr_sites a " +
       "WHERE(a.cust_sup_site_id = " + siteID + ")";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      //Global.taxFrm.rec_SQL = strSql;
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return dtst.Tables[0].Rows[0][0].ToString();
      }
      return "";
    }

    public static int get_One_CstmrSiteID(long cstmrID)
    {
      string strSql = "SELECT a.cust_sup_site_id " +
       "FROM scm.scm_cstmr_suplr_sites a " +
       "WHERE(a.cust_supplier_id = " + cstmrID + ") ORDER BY a.cust_sup_site_id DESC";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      //Global.taxFrm.rec_SQL = strSql;
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return -1;
    }

    public static void deleteAttnRgstr(long rgstrid, string rgstrNm)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Register Name = " + rgstrNm;
      string delSQL = "DELETE FROM attn.attn_attendance_recs_hdr WHERE recs_hdr_id = " + rgstrid;
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }

    public static bool isAttnRgstrInUse(long rgstrid)
    {
      string strSql = "SELECT a.attnd_rec_id " +
       "FROM attn.attn_attendance_recs a " +
       "WHERE(a.recs_hdr_id = " + rgstrid + ")";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return true;
      }
      return false;
    }

    public static void deleteAttnTimeLn(long rgtstrLnid, string PrsnNm)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Person Name = " + PrsnNm;
      string delSQL = "DELETE FROM attn.attn_attendance_recs_times WHERE attnd_det_rec_id = " + rgtstrLnid;
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }
    public static void deleteAttnRgstrDLn(long rgtstrLnid, string PrsnNm)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Person Name = " + PrsnNm;
      string delSQL = "DELETE FROM attn.attn_attendance_recs WHERE attnd_rec_id = " + rgtstrLnid;
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }
    public static bool isAttnRgstrLnInUse(long Lnid)
    {
      string selSQL = @"Select attn.getEvntAmntBilled(a.recs_hdr_id, a.customer_id), a.visitor_name_desc  
      from attn.attn_attendance_recs a where a.attnd_rec_id=" + Lnid;
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        if (double.Parse(dtst.Tables[0].Rows[0][0].ToString()) > 0)
        {
          Global.mnFrm.cmCde.showMsg("Cannot Delete " + dtst.Tables[0].Rows[0][1].ToString() + " due to an Undeleted Invoice in this Name!", 0);
          return true;
        }
      }
      selSQL = "SELECT a.attnd_det_rec_id " +
       "FROM attn.attn_attendance_recs_times a " +
       "WHERE(a.attnd_rec_id = " + Lnid + ")";
      dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        Global.mnFrm.cmCde.showMsg("Cannot Delete " + dtst.Tables[0].Rows[0][1].ToString() + " due to Undeleted Extra Time Records!", 0);
        return true;
      }
      return false;
    }

    public static long getAttnRgstrID(string rgstrname, int orgid)
    {
      DataSet dtSt = new DataSet();
      string sqlStr = "select recs_hdr_id from attn.attn_attendance_recs_hdr where lower(recs_hdr_name) = '" +
       rgstrname.Replace("'", "''").ToLower() + "' and org_id = " + orgid;
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

    public static long getNewMtrcCntLnID()
    {
      //string strSql = "select nextval('accb.accb_trnsctn_batches_batch_id_seq'::regclass);";
      string strSql = "select nextval('attn.attn_attendance_recs_cntr_cntr_id_seq')";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return -1;
    }

    public static string getEvntPointsLovNm(long tmtblDetID)
    {
      string strSql = @"SELECT e.attnd_points_lov_nm, d.event_id 
 FROM attn.attn_time_table_details d, attn.attn_attendance_events e 
 WHERE d.event_id = e.event_id and d.time_table_det_id = " + tmtblDetID + "";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      //Global.taxFrm.rec_SQL = strSql;
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return dtst.Tables[0].Rows[0][0].ToString();
      }
      return "";

    }

    public static DataSet get_One_AttnRgstrDet(long rgstrid)
    {
      string strSql = @"SELECT recs_hdr_id, recs_hdr_name, recs_hdr_desc, 
time_table_id, (SELECT time_table_name FROM attn.attn_time_table_hdrs d WHERE d.time_table_id = a.time_table_id) tmtblnm, 
time_table_det_id, (SELECT 'EVENT: ' || COALESCE(attn.get_event_name(d.event_id),'') || 
' VENUE: ' || COALESCE(attn.get_venue_name(d.assgnd_venue_id),'') || 
' HOST: ' || COALESCE(prs.get_prsn_name(d.assgnd_host_id),'') 
FROM attn.attn_time_table_details d WHERE d.time_table_det_id = a.time_table_det_id) evntdec, 
       to_char(to_timestamp(event_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') evdte, 
       to_char(to_timestamp(end_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') enddte
  FROM attn.attn_attendance_recs_hdr a " +
       "WHERE(a.recs_hdr_id = " + rgstrid + ")";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      //Global.taxFrm.rec_SQL = strSql;
      return dtst;
    }

    public static DataSet get_Basic_AttnRgstr(string searchWord, string searchIn,
 Int64 offset, int limit_size, int orgID, bool isAdhc)
    {
      string strSql = "";
      string whrcls = "";
      string extrWhr = "";
      if (isAdhc)
      {
        extrWhr = " and a.time_table_id <= 0";
      }
      if (searchIn == "Register Name")
      {
        whrcls = " AND (a.recs_hdr_name ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Register Description")
      {
        whrcls = " AND (a.recs_hdr_desc ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Register Number")
      {
        whrcls = " AND (trim(to_char(a.recs_hdr_id, '999999999999999999999999999')) ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Event Date")
      {
        whrcls = " AND (to_char(to_timestamp(a.event_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      strSql = "SELECT a.recs_hdr_id, a.recs_hdr_name " +
"FROM attn.attn_attendance_recs_hdr a " +
"WHERE ((a.org_id = " + orgID + ")" + whrcls + extrWhr + ") ORDER BY a.recs_hdr_id DESC LIMIT " + limit_size +
" OFFSET " + (Math.Abs(offset * limit_size)).ToString();

      Global.attndFrm.rec_SQL = strSql;
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      return dtst;
    }

    public static long get_Total_AttnRgstr(string searchWord, string searchIn, int orgID, bool isAdhc)
    {
      string strSql = "";
      string whrcls = "";
      string extrWhr = "";
      if (isAdhc)
      {
        extrWhr = " and a.time_table_id <= 0";
      }
      if (searchIn == "Register Name")
      {
        whrcls = " AND (a.recs_hdr_name ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Register Description")
      {
        whrcls = " AND (a.recs_hdr_desc ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Register Number")
      {
        whrcls = " AND (trim(to_char(a.recs_hdr_id, '999999999999999999999999999')) ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Event Date")
      {
        whrcls = " AND (to_char(to_timestamp(a.event_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      strSql = "SELECT count(1) " +
"FROM attn.attn_attendance_recs_hdr a " +
"WHERE ((a.org_id = " + orgID + ")" + whrcls + extrWhr + ")";
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

    public static DataSet get_One_AttnRgstr_DetLns(string searchWord, string searchIn, long offset,
      int limit_size, long rgstrID)
    {
      string strSql = "";
      string whrcls = "";
      /*
       *  Person Name
          Date/Time In
          Date/Time Out
          Is Present?
          Attendance Comments
       * 
       */
      if (searchIn == "Person Name/ID")
      {
        whrcls = @" AND ((CASE WHEN a.customer_id <= 0 and a.person_id <= 0 THEN a.visitor_name_desc 
           WHEN a.person_id>0 THEN prs.get_prsn_surname(a.person_id) || ' (' || prs.get_prsn_loc_id(a.person_id) || ')' 
            ELSE scm.get_cstmr_splr_name(a.customer_id)||scm.get_cstmr_splr_name(a.sponsor_id) END) ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Date/Time In")
      {
        whrcls = " AND (to_char(to_timestamp(a.date_time_in,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Date/Time Out")
      {
        whrcls = " AND (to_char(to_timestamp(a.date_time_out,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Is Present?")
      {
        whrcls = " AND ((CASE WHEN a.is_present='1' THEN 'TRUE' ELSE 'FALSE' END) ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Attendance Comments")
      {
        whrcls = " AND (a.attn_comments ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      strSql = @"SELECT attnd_rec_id, recs_hdr_id, person_id, prs.get_prsn_loc_id(a.person_id), 
      CASE WHEN a.customer_id <= 0 and a.person_id <= 0 THEN a.visitor_name_desc 
           WHEN a.person_id>0 THEN prs.get_prsn_surname(a.person_id) || ' (' || prs.get_prsn_loc_id(a.person_id) || ')' 
            ELSE scm.get_cstmr_splr_name(a.customer_id) END, 
      CASE WHEN a.date_time_in != '' THEN to_char(to_timestamp(a.date_time_in,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') ELSE '' END, 
      CASE WHEN a.date_time_out != '' THEN to_char(to_timestamp(a.date_time_out,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') ELSE '' END,
      CASE WHEN a.is_present='1' THEN 'TRUE' ELSE 'FALSE' END, a.attn_comments,
      a.visitor_classfctn, a.no_of_persons, 0 no_of_chldrn, a.customer_id, a.sponsor_id, scm.get_cstmr_splr_name(a.sponsor_id),
 attn.getevntamntbilled(a.recs_hdr_id, a.customer_id), attn.getEvntAmntPaid(a.recs_hdr_id, a.customer_id)
  FROM attn.attn_attendance_recs a " +
        "WHERE((a.recs_hdr_id = " + rgstrID + ")" + whrcls + ") ORDER BY 5, 1 LIMIT " + limit_size +
        " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
      //CASE WHEN a.sponsor_id>0 THEN attn.getEvntAmntPaid(a.recs_hdr_id, a.sponsor_id) ELSE  END
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      Global.attndFrm.rec_det_SQL = strSql;
      return dtst;
    }

    public static long get_Total_AttnRgstr_DetLns(string searchWord, string searchIn, long rgstrID)
    {
      string strSql = "";
      string whrcls = "";
      /*
       *  Person Name
          Date/Time In
          Date/Time Out
          Is Present?
          Attendance Comments
       * 
       */
      if (searchIn == "Person Name/ID")
      {
        whrcls = @" AND ((CASE WHEN a.customer_id <= 0 and a.person_id <= 0 THEN a.visitor_name_desc 
           WHEN a.person_id>0 THEN prs.get_prsn_surname(a.person_id) || ' (' || prs.get_prsn_loc_id(a.person_id) || ')' 
            ELSE scm.get_cstmr_splr_name(a.customer_id)||scm.get_cstmr_splr_name(a.sponsor_id) END) ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Date/Time In")
      {
        whrcls = " AND (to_char(to_timestamp(a.date_time_in,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Date/Time Out")
      {
        whrcls = " AND (to_char(to_timestamp(a.date_time_out,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Is Present?")
      {
        whrcls = " AND ((CASE WHEN a.is_present='1' THEN 'TRUE' ELSE 'FALSE' END) ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Attendance Comments")
      {
        whrcls = " AND (a.attn_comments ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      strSql = @"SELECT count(1) 
  FROM attn.attn_attendance_recs a " +
        "WHERE((a.recs_hdr_id = " + rgstrID + ")" + whrcls + ")";

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

    public static DataSet get_One_AttnRgstr_Times(string searchWord, string searchIn, long offset,
      int limit_size, long attnRecID)
    {
      string strSql = "";
      string whrcls = "";
      /*
       *  Person Name
          Date/Time In
          Date/Time Out
          Is Present?
          Attendance Comments
       * 
       */
      if (searchIn == "Date/Time In")
      {
        whrcls = " AND (to_char(to_timestamp(date_time_in,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Date/Time Out")
      {
        whrcls = " AND (to_char(to_timestamp(date_time_out,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Is Present?")
      {
        whrcls = " AND ((CASE WHEN is_present='1' THEN 'TRUE' ELSE 'FALSE' END) ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Attendance Comments")
      {
        whrcls = " AND (attn_comments ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      strSql = @"Select tbl1.* FROM (SELECT attnd_rec_id,  
      CASE WHEN a.date_time_in != '' THEN to_char(to_timestamp(a.date_time_in,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') ELSE '' END, 
      CASE WHEN a.date_time_out != '' THEN to_char(to_timestamp(a.date_time_out,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') ELSE '' END,
      CASE WHEN a.is_present='1' THEN 'TRUE' ELSE 'FALSE' END, 
      a.attn_comments, a.date_time_in, 'YES' from_main, -1  
     FROM attn.attn_attendance_recs a " +
        @"WHERE((a.attnd_rec_id = " + attnRecID + ")" + whrcls + @") 
UNION
SELECT b.attnd_rec_id,  
      CASE WHEN b.date_time_in != '' THEN to_char(to_timestamp(b.date_time_in,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') ELSE '' END, 
      CASE WHEN b.date_time_out != '' THEN to_char(to_timestamp(b.date_time_out,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') ELSE '' END,
      CASE WHEN b.is_present='1' THEN 'TRUE' ELSE 'FALSE' END, 
      b.attn_comments, b.date_time_in, 'NO' from_main, b.attnd_det_rec_id 
     FROM attn.attn_attendance_recs_times b " +
        "WHERE((b.attnd_rec_id = " + attnRecID + ")" + whrcls + @")
      ) tbl1 ORDER BY 6 DESC, 8 DESC LIMIT " + limit_size +
        " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      Global.attndDiag.rec_det_SQL = strSql;
      return dtst;
    }

    public static long get_Total_AttnRgstr_Times(string searchWord, string searchIn, long attnRecID)
    {
      string strSql = "";
      string whrcls = "";
      /*
       *  Person Name
          Date/Time In
          Date/Time Out
          Is Present?
          Attendance Comments
       * 
       */
      if (searchIn == "Date/Time In")
      {
        whrcls = " AND (to_char(to_timestamp(date_time_in,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Date/Time Out")
      {
        whrcls = " AND (to_char(to_timestamp(date_time_out,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Is Present?")
      {
        whrcls = " AND ((CASE WHEN is_present='1' THEN 'TRUE' ELSE 'FALSE' END) ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Attendance Comments")
      {
        whrcls = " AND (attn_comments ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      strSql = @"Select count(1) FROM (SELECT attnd_rec_id,  
      CASE WHEN a.date_time_in != '' THEN to_char(to_timestamp(a.date_time_in,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') ELSE '' END, 
      CASE WHEN a.date_time_out != '' THEN to_char(to_timestamp(a.date_time_out,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') ELSE '' END,
      CASE WHEN a.is_present='1' THEN 'TRUE' ELSE 'FALSE' END, 
      a.attn_comments, a.date_time_in, 'YES' from_main, -1  
     FROM attn.attn_attendance_recs a " +
        @"WHERE((a.attnd_rec_id = " + attnRecID + ")" + whrcls + @") 
UNION
SELECT b.attnd_rec_id,  
      CASE WHEN b.date_time_in != '' THEN to_char(to_timestamp(b.date_time_in,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') ELSE '' END, 
      CASE WHEN b.date_time_out != '' THEN to_char(to_timestamp(b.date_time_out,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') ELSE '' END,
      CASE WHEN b.is_present='1' THEN 'TRUE' ELSE 'FALSE' END, 
      b.attn_comments, b.date_time_in, 'NO' from_main, b.attnd_det_rec_id 
     FROM attn.attn_attendance_recs_times b " +
        "WHERE((b.attnd_rec_id = " + attnRecID + ")" + whrcls + @")
      ) tbl1";

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

    public static DataSet get_One_AttnRgstr_MtrcLns(long rgstrID, string lovNm, bool editMode, int evntID)
    {
      string strSql = "";
      if (lovNm == "")
      {
        lovNm = "Attendance HeadCount Metrics";
      }
      int lovID = Global.mnFrm.cmCde.getLovID(lovNm);
      if (editMode)
      {
        strSql = @"  SELECT   *
    FROM   (SELECT   a.cntr_id,
                     a.metric_name_desc,
                     trim(to_char(a.cntr_result,'9999999999999999999999999')) rslt,
                     a.rslt_comment,
                     a.lnkd_pssbl_val_id,
                     'NUMBER',
                     -1
              FROM   attn.attn_attendance_recs_cntr a
             WHERE   (a.recs_hdr_id = " + rgstrID + @")
            UNION
              SELECT -1,
                     a.rslt_metric_name_desc,
                     '0',
                     a.rslt_comment,
                     -1,
                     a.rslt_type,
                     a.rslt_metric_id
              FROM   attn.attn_attendance_events_mtrcs a
             WHERE   (a.event_id = " + evntID + @"
                      AND a.is_enabled = '1'
                      AND a.rslt_metric_id NOT IN
                              (SELECT   c.evnt_metric_id
                                 FROM   attn.attn_attendance_events_rslts c
                                WHERE   (c.lnkd_rgstr_id = " + rgstrID + @")))
            UNION
              SELECT a.evnt_rslt_id,
                     (SELECT  g.rslt_metric_name_desc FROM attn.attn_attendance_events_mtrcs g WHERE a.evnt_metric_id=g.rslt_metric_id) mtrcnm,
                     a.event_result,
                     a.rslt_desc,
                     -1,
                     (SELECT  g.rslt_type FROM attn.attn_attendance_events_mtrcs g WHERE a.evnt_metric_id=g.rslt_metric_id) rslttype,
                     a.evnt_metric_id
              FROM   attn.attn_attendance_events_rslts a
             WHERE   (a.lnkd_rgstr_id = " + rgstrID + @")
            UNION
            SELECT   -1,
                     b.pssbl_value,
                     '0',
                     '',
                     b.pssbl_value_id,
                     'NUMBER',
                      -1
              FROM   gst.gen_stp_lov_values b
             WHERE   b.value_list_id = " + lovID + @"
                     AND b.is_enabled='1'
                     AND b.pssbl_value_id NOT IN
                              (SELECT   c.lnkd_pssbl_val_id
                                 FROM   attn.attn_attendance_recs_cntr c
                                WHERE   (c.recs_hdr_id = " + rgstrID + @"))) tbl1
ORDER BY   7, 2, 5";
      }
      else
      {
        strSql = @"SELECT * FROM (SELECT   a.cntr_id,
                     a.metric_name_desc,
                     trim(to_char(a.cntr_result,'9999999999999999999999999')) rslt,
                     a.rslt_comment,
                     a.lnkd_pssbl_val_id,
                     'NUMBER',
                      -1
              FROM   attn.attn_attendance_recs_cntr a
             WHERE   (a.recs_hdr_id = " + rgstrID + @") 
          UNION
              SELECT a.evnt_rslt_id,
                     (SELECT  g.rslt_metric_name_desc FROM attn.attn_attendance_events_mtrcs g WHERE a.evnt_metric_id=g.rslt_metric_id) mtrcnm,
                     a.event_result,
                     a.rslt_desc,
                     -1,
                     (SELECT  g.rslt_type FROM attn.attn_attendance_events_mtrcs g WHERE a.evnt_metric_id=g.rslt_metric_id) rslttype,
                     a.evnt_metric_id
              FROM   attn.attn_attendance_events_rslts a
             WHERE   (a.lnkd_rgstr_id = " + rgstrID + @")) tbl1           
    ORDER BY   7, 2, 5";
      }
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      Global.attndFrm.rec_mtrc_SQL = strSql;
      return dtst;
    }

    public static long getNewRgstrID()
    {
      //string strSql = "select nextval('accb.accb_trnsctn_batches_batch_id_seq'::regclass);";
      string strSql = "select  last_value from attn.attn_attendance_recs_hdr_recs_hdr_id_seq";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString()) + 1;
      }
      return -1;
    }

    public static long doesRgstrHvPrsn(long prsnid, long rgstrid)
    {
      string selSQL = "SELECT attnd_rec_id " +
                  "FROM attn.attn_attendance_recs WHERE ((person_id = " + prsnid +
                  ") and (recs_hdr_id = " + rgstrid + "))";
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

    public static long doesRgstrHvCstmr(long cstmrID, long rgstrid)
    {
      string selSQL = "SELECT attnd_rec_id " +
                  "FROM attn.attn_attendance_recs WHERE ((customer_id = " + cstmrID +
                  ") and (recs_hdr_id = " + rgstrid + "))";
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

    public static DataSet get_AttnRgstr_SrchLns(
      string searchWord, string searchIn, long offset,
   int limit_size, int orgid, string dte1, string dte2)
    {
      dte1 = DateTime.ParseExact(
dte1, "dd-MMM-yyyy HH:mm:ss",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      dte2 = DateTime.ParseExact(
dte2, "dd-MMM-yyyy HH:mm:ss",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

      string strSql = "";
      string whrcls = "";
      /*
       *  Person Name
          Date/Time In
          Date/Time Out
          Is Present?
          Attendance Comments
       * 
       */
      if (searchIn == "Register Name")
      {
        whrcls = " AND (b.recs_hdr_name ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Person Name/ID")
      {
        whrcls = @" AND ((CASE WHEN a.customer_id <= 0 and a.person_id <= 0 THEN a.visitor_name_desc 
           WHEN a.person_id>0 THEN prs.get_prsn_surname(a.person_id) || ' (' || prs.get_prsn_loc_id(a.person_id) || ')' 
            ELSE scm.get_cstmr_splr_name(a.customer_id) END) ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Date/Time In")
      {
        whrcls = " AND (to_char(to_timestamp(a.date_time_in,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Date/Time Out")
      {
        whrcls = " AND (to_char(to_timestamp(a.date_time_out,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Is Present?")
      {
        whrcls = " AND ((CASE WHEN a.is_present='1' THEN 'TRUE' ELSE 'FALSE' END) ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Attendance Comments")
      {
        whrcls = " AND (a.attn_comments ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      strSql = @"SELECT a.attnd_rec_id, a.recs_hdr_id, a.person_id, 
CASE WHEN a.customer_id <= 0 and a.person_id <= 0 THEN a.visitor_classfctn 
           WHEN a.person_id>0 THEN prs.get_prsn_loc_id(a.person_id) 
            ELSE a.visitor_classfctn || ' (' || a.customer_id || ')' END, 
      CASE WHEN a.customer_id <= 0 and a.person_id <= 0 THEN a.visitor_name_desc 
           WHEN a.person_id>0 THEN prs.get_prsn_surname(a.person_id) || ' (' || prs.get_prsn_loc_id(a.person_id) || ')' 
            ELSE scm.get_cstmr_splr_name(a.customer_id) END, b.recs_hdr_name,
      CASE WHEN a.is_present='1' THEN 'TRUE' ELSE 'FALSE' END, 
      CASE WHEN a.date_time_in != '' THEN to_char(to_timestamp(a.date_time_in,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') ELSE '' END, 
      CASE WHEN a.date_time_out != '' THEN to_char(to_timestamp(a.date_time_out,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') ELSE '' END,
      a.attn_comments
      FROM attn.attn_attendance_recs a, attn.attn_attendance_recs_hdr b " +
        "WHERE((a.recs_hdr_id=b.recs_hdr_id) and (b.org_id = " + orgid + ") and (to_timestamp(b.event_date,'YYYY-MM-DD HH24:MI:SS') between to_timestamp('" + dte1 +
       "','YYYY-MM-DD HH24:MI:SS') AND to_timestamp('" + dte2 + "','YYYY-MM-DD HH24:MI:SS'))" + whrcls + ") ORDER BY 4, 1 LIMIT " + limit_size +
        " OFFSET " + (Math.Abs(offset * limit_size)).ToString();

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      Global.srchAttndFrm.rec_SQL = strSql;
      return dtst;
    }

    public static long get_Total_AttnRgstr_SrchLns(
      string searchWord, string searchIn, int orgid, string dte1, string dte2)
    {
      dte1 = DateTime.ParseExact(
dte1, "dd-MMM-yyyy HH:mm:ss",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      dte2 = DateTime.ParseExact(
dte2, "dd-MMM-yyyy HH:mm:ss",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

      string strSql = "";
      string whrcls = "";
      /*
       *  Person Name
          Date/Time In
          Date/Time Out
          Is Present?
          Attendance Comments
       * 
       */
      if (searchIn == "Register Name")
      {
        whrcls = " AND (b.recs_hdr_name ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Person Name/ID")
      {
        whrcls = @" AND ((CASE WHEN a.customer_id <= 0 and a.person_id <= 0 THEN a.visitor_name_desc 
           WHEN a.person_id>0 THEN prs.get_prsn_surname(a.person_id) || ' (' || prs.get_prsn_loc_id(a.person_id) || ')' 
            ELSE scm.get_cstmr_splr_name(a.customer_id) END) ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Date/Time In")
      {
        whrcls = " AND (to_char(to_timestamp(a.date_time_in,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Date/Time Out")
      {
        whrcls = " AND (to_char(to_timestamp(a.date_time_out,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Is Present?")
      {
        whrcls = " AND ((CASE WHEN a.is_present='1' THEN 'TRUE' ELSE 'FALSE' END) ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Attendance Comments")
      {
        whrcls = " AND (a.attn_comments ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      strSql = @"SELECT count(1)
  FROM attn.attn_attendance_recs a, attn.attn_attendance_recs_hdr b " +
        "WHERE((a.recs_hdr_id=b.recs_hdr_id) and (b.org_id = " + orgid + ") and (to_timestamp(b.event_date,'YYYY-MM-DD HH24:MI:SS') between to_timestamp('" + dte1 +
       "','YYYY-MM-DD HH24:MI:SS') AND to_timestamp('" + dte2 + "','YYYY-MM-DD HH24:MI:SS'))" + whrcls + ")";

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

    #region "BOOKINGS/CHECKINS..."
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

    public static DataSet get_Checkins(
   string searchWord, string searchIn,
  Int64 offset, int limit_size, int orgID
     , bool shwActive, bool shwUnsettled, string extrWhere)
    {
      /*Doc. Status
   Created By
   Customer
   Purpose of Visit
   Document Number
   Facility Number
   Start Date*/
      string strSql = "";
      string whereClause = "";
      string activeDocClause = "";
      string unstldBillClause = "";
      if (shwUnsettled)
      {
        unstldBillClause = @" AND EXISTS (SELECT f.src_doc_hdr_id 
FROM scm.scm_doc_amnt_smmrys f WHERE f.smmry_type='7Change/Balance' 
and round(f.smmry_amnt,2)>0 and y.invc_hdr_id=f.src_doc_hdr_id and f.src_doc_type=y.invc_type
 and y.approval_status != 'Cancelled')";
      }
      if (shwActive)
      {
        activeDocClause = " AND (a.doc_status='Reserved' or a.doc_status='Checked-In' or a.doc_status='Ordered')";
      }
      if (searchIn == "Doc. Status")
      {
        whereClause = "(a.doc_status ilike '" + searchWord.Replace("'", "''") +
    "')";
      }
      else if (searchIn == "Created By")
      {
        whereClause = "(a.created_by IN (select c.user_id from sec.sec_users c where c.user_name ilike '" + searchWord.Replace("'", "''") +
     "'))";
      }
      else if (searchIn == "Customer")
      {
        whereClause = "(a.sponsor_id IN (select c.cust_sup_id from scm.scm_cstmr_suplr c where c.cust_sup_name ilike '" + searchWord.Replace("'", "''") +
    "') or a.customer_id IN (select c.cust_sup_id from scm.scm_cstmr_suplr c where c.cust_sup_name ilike '" + searchWord.Replace("'", "''") +
    "'))";
      }
      else if (searchIn == "Facility Number" || searchIn == "Table/Room Number")
      {
        whereClause = "(b.room_name ilike '" + searchWord.Replace("'", "''") +
    @"' or (Select p.room_name from hotl.rooms p, hotl.checkins_hdr k 
        where p.room_id = k.service_det_id and a.prnt_chck_in_id=k.check_in_id 
and a.prnt_doc_typ = k.doc_type ORDER BY 1 LIMIT 1 OFFSET 0) ilike '" + searchWord.Replace("'", "''") +
    "')";
      }
      else if (searchIn == "Purpose of Visit")
      {
        whereClause = "(a.purpose_of_visit ilike '" + searchWord.Replace("'", "''") +
    "')";
      }
      else if (searchIn == "Document Number")
      {
        whereClause = "(a.doc_num ilike '" + searchWord.Replace("'", "''") +
    "' or y.invc_number ilike '" + searchWord.Replace("'", "''") +
    "')";
      }
      else if (searchIn == "Start Date")
      {
        whereClause = "(to_char(to_timestamp(a.start_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') ilike '" + searchWord.Replace("'", "''") +
    "')";
      }
      strSql = @"SELECT DISTINCT a.check_in_id, a.doc_num, y.invc_number 
FROM hotl.checkins_hdr a 
LEFT OUTER JOIN hotl.service_types d ON (a.service_type_id=d.service_type_id )
LEFT OUTER JOIN hotl.rooms b ON (a.service_det_id = b.room_id)
LEFT OUTER JOIN scm.scm_sales_invc_hdr y ON ((a.check_in_id = y.other_mdls_doc_id or (a.prnt_chck_in_id=y.other_mdls_doc_id and y.other_mdls_doc_id>0))
and (a.doc_type=y.other_mdls_doc_type or (a.prnt_doc_typ=y.other_mdls_doc_type and a.prnt_doc_typ != ''))) " +
   "WHERE " + whereClause + activeDocClause + unstldBillClause + " and COALESCE(d.org_id, " + orgID + ")=" + orgID +
   @"" + extrWhere +
   " ORDER BY a.check_in_id DESC LIMIT " + limit_size +
   " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
      if (Global.wfnCheckinsFrm != null)
      {
        Global.wfnCheckinsFrm.rec_SQL = strSql;
      }
      if (Global.wfnCheckinsDiag != null)
      {
        Global.wfnCheckinsDiag.rec_SQL = strSql;
      }
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      return dtst;
    }

    public static long get_Ttl_Checkins(string searchWord, string searchIn,
     int orgID, bool shwActive, bool shwUnsettled, string extrWhere)
    {
      /*Doc. Status
  Created By
  Customer
  Purpose of Visit
  Document Number
  Facility Number
  Start Date*/
      string strSql = "";
      string whereClause = "";
      string activeDocClause = "";
      string unstldBillClause = "";
      if (shwUnsettled)
      {
        unstldBillClause = @" AND EXISTS (SELECT f.src_doc_hdr_id 
FROM scm.scm_doc_amnt_smmrys f WHERE f.smmry_type='7Change/Balance' 
and round(f.smmry_amnt,2)>0 and y.invc_hdr_id=f.src_doc_hdr_id and f.src_doc_type=y.invc_type
 and y.approval_status != 'Cancelled')";
        //unpstdCls = " AND (a.approval_status!='Approved')";
      }
      if (shwActive)
      {
        activeDocClause = " AND (a.doc_status='Reserved' or a.doc_status='Checked-In' or a.doc_status='Ordered')";
      }

      if (searchIn == "Doc. Status")
      {
        whereClause = "(a.doc_status ilike '" + searchWord.Replace("'", "''") +
    "')";
      }
      else if (searchIn == "Created By")
      {
        whereClause = "(a.created_by IN (select c.user_id from sec.sec_users c where c.user_name ilike '" + searchWord.Replace("'", "''") +
     "'))";
      }
      else if (searchIn == "Customer")
      {
        whereClause = "(a.sponsor_id IN (select c.cust_sup_id from scm.scm_cstmr_suplr c where c.cust_sup_name ilike '" + searchWord.Replace("'", "''") +
    "') or a.customer_id IN (select c.cust_sup_id from scm.scm_cstmr_suplr c where c.cust_sup_name ilike '" + searchWord.Replace("'", "''") +
    "'))";
      }
      else if (searchIn == "Facility Number" || searchIn == "Table/Room Number")
      {
        whereClause = "(b.room_name ilike '" + searchWord.Replace("'", "''") +
    @"' or (Select p.room_name from hotl.rooms p, hotl.checkins_hdr k 
        where p.room_id = k.service_det_id and a.prnt_chck_in_id=k.check_in_id 
and a.prnt_doc_typ = k.doc_type ORDER BY 1 LIMIT 1 OFFSET 0) ilike '" + searchWord.Replace("'", "''") +
    "')";
      }
      else if (searchIn == "Purpose of Visit")
      {
        whereClause = "(a.purpose_of_visit ilike '" + searchWord.Replace("'", "''") +
    "')";
      }
      else if (searchIn == "Document Number")
      {
        whereClause = "(a.doc_num ilike '" + searchWord.Replace("'", "''") +
    "' or y.invc_number ilike '" + searchWord.Replace("'", "''") +
    "')";
      }
      else if (searchIn == "Start Date")
      {
        whereClause = "(to_char(to_timestamp(a.start_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') ilike '" + searchWord.Replace("'", "''") +
    "')";
      }
      strSql = "SELECT count(1) " +
   @"FROM hotl.checkins_hdr a 
LEFT OUTER JOIN hotl.service_types d ON (a.service_type_id=d.service_type_id )
LEFT OUTER JOIN hotl.rooms b ON (a.service_det_id = b.room_id)
LEFT OUTER JOIN scm.scm_sales_invc_hdr y ON ((a.check_in_id = y.other_mdls_doc_id or (a.prnt_chck_in_id=y.other_mdls_doc_id and y.other_mdls_doc_id>0))
and (a.doc_type=y.other_mdls_doc_type or (a.prnt_doc_typ=y.other_mdls_doc_type and a.prnt_doc_typ != ''))) " +
   "WHERE " + whereClause + activeDocClause + unstldBillClause + " and COALESCE(d.org_id, " + orgID + ")=" + orgID +
   @"" + extrWhere;
      /* and a.service_type_id=d.service_type_id and a.service_det_id = b.room_id 
      and (a.check_in_id = y.other_mdls_doc_id or (a.prnt_chck_in_id=y.other_mdls_doc_id and y.other_mdls_doc_id>0)) 
and (a.doc_type=y.other_mdls_doc_type or (a.prnt_doc_typ=y.other_mdls_doc_type and a.prnt_doc_typ != ''))*/
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
    public static DataSet get_One_CheckinDt(long checkInID)
    {
      string strSql = @"Select a.check_in_id, a.doc_num, a.doc_type, a.fclty_type, 
to_char(to_timestamp(a.start_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), 
to_char(to_timestamp(a.end_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), 
a.service_type_id, d.service_type_name,
       a.service_det_id, b.room_name, a.no_of_adults, a.no_of_children, a.sponsor_id, a.sponsor_site_id, 
       a.customer_id, a.customer_site_id, a.arriving_from, a.proceeding_to, 
       a.other_info, a.created_by, a.doc_status, COALESCE(y.invc_hdr_id,-1), y.invc_number, 
y.pymny_method_id, accb.get_pymnt_mthd_name(y.pymny_method_id), y.invc_curr_id, 
gst.get_pssbl_val(COALESCE(y.invc_curr_id,-1)), COALESCE(y.exchng_rate,1), y.approval_status, 
y.invc_type, a.prnt_chck_in_id,a.prnt_doc_typ, COALESCE(y.enbl_auto_misc_chrges,'0'), y.event_rgstr_id " +
   @"FROM hotl.checkins_hdr a 
LEFT OUTER JOIN hotl.service_types d ON (a.service_type_id=d.service_type_id )
LEFT OUTER JOIN hotl.rooms b ON (a.service_det_id = b.room_id)
LEFT OUTER JOIN scm.scm_sales_invc_hdr y ON ((a.check_in_id = y.other_mdls_doc_id or (a.prnt_chck_in_id=y.other_mdls_doc_id and y.other_mdls_doc_id>0))
and (a.doc_type=y.other_mdls_doc_type or (a.prnt_doc_typ=y.other_mdls_doc_type and a.prnt_doc_typ != ''))) " +
   "WHERE a.check_in_id=" + checkInID +
   @" ";
      /* and a.service_type_id=d.service_type_id and a.service_det_id = b.room_id and 
   (a.check_in_id = y.other_mdls_doc_id or (a.prnt_chck_in_id=y.other_mdls_doc_id and y.other_mdls_doc_id>0)) 
and (a.doc_type=y.other_mdls_doc_type or (a.prnt_doc_typ=y.other_mdls_doc_type and a.prnt_doc_typ != '')) */
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      return dtst;
    }

    public static string get_ChckInRec_Hstry(long hdrID)
    {
      string strSQL = @"SELECT a.created_by, 
to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), 
      a.last_update_by, 
      to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') " +
      "FROM hotl.checkins_hdr a WHERE(a.check_in_id = " + hdrID + ")";
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

    public static string get_SalesDT_Rec_Hstry(long dteID)
    {
      string strSQL = @"SELECT a.created_by, 
to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), 
      a.last_update_by, 
      to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') " +
      "FROM scm.scm_sales_invc_det a WHERE(a.invc_det_ln_id = " + dteID + ")";
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

    public static void deleteDocSmmryItms(long docID, string docType)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string delSQL = "DELETE FROM scm.scm_doc_amnt_smmrys WHERE src_doc_hdr_id = " +
        docID + " and src_doc_type = '" + docType + "'";
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }

    public static void deleteSalesLnItm(long lnID)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string delSQL = "DELETE FROM scm.scm_sales_invc_det WHERE invc_det_ln_id = " +
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

    public static void deleteSalesDocLns(long docID, long othMdlID)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Deleting Lines from a Sales Doc.";
      string delSQL = "DELETE FROM scm.scm_sales_invc_det WHERE invc_hdr_id = " +
        docID + " and other_mdls_doc_id = " + othMdlID +
        " and other_mdls_doc_type IN ('Restaurant Order','Pool Subscription','Gym Subscription')";
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }

    public static void deleteDocGLInfcLns(long docID, string srcDocType)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string delSQL = "DELETE FROM scm.scm_gl_interface WHERE src_doc_id = " +
        docID + " and src_doc_typ ilike '%" + srcDocType.Replace("'", "''") + "%' and gl_batch_id = -1";
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }

    public static void deleteCheckIn(long chckInID, string salesChckInNum)
    {
      //
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Sales/CheckIn Number = " + salesChckInNum;
      string delSQL = "DELETE FROM hotl.checkins_hdr WHERE check_in_id = " + chckInID;
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }

    public static void deleteSalesLn(long Lnid, string lnDesc)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "Line Desc = " + lnDesc;
      string delSQL = "DELETE FROM scm.scm_sales_invc_det WHERE invc_det_ln_id = " + Lnid;
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }

    public static int getCheckInID(int cstmrID, string startDte, string endDte)
    {
      DataSet dtSt = new DataSet();
      string sqlStr = "select check_in_id from hotl.checkins_hdr where customer_id = " +
       cstmrID + " and (to_timestamp('" + startDte + "','DD-Mon-YYYY HH24:MI:SS') " +
       "between to_timestmp(start_date,'YYYY-MM-DD HH24:MI:SS') and to_timestmp(end_date,'YYYY-MM-DD HH24:MI:SS') or to_timestamp('"
       + endDte + "','DD-Mon-YYYY HH24:MI:SS') " +
       "between to_timestmp(start_date,'YYYY-MM-DD HH24:MI:SS') and to_timestmp(end_date,'YYYY-MM-DD HH24:MI:SS'))";
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

    public static void createCheckIn(string docNum,
  string docType, string strtDte, string endDte, int srvsTypID,
     int srvsDteID, int noAdlts, int NoChldrn, int spnsID, int spnsSiteID,
     int cstmrID, int cstmrSiteID, string srcPlace, string destPlace, string otherInfo,
     string fcltyType, string docStatus, long prntChckIn, string prntDocType)
    {
      strtDte = DateTime.ParseExact(
   strtDte, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

      endDte = DateTime.ParseExact(
   endDte, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

      if (otherInfo.Length > 400)
      {
        otherInfo = otherInfo.Substring(0, 400);
      }

      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = @"INSERT INTO hotl.checkins_hdr(
            doc_num, doc_type, start_date, end_date, service_type_id, 
            service_det_id, no_of_adults, no_of_children, sponsor_id, sponsor_site_id, 
            customer_id, customer_site_id, arriving_from, proceeding_to, 
            other_info, created_by, creation_date, last_update_by, last_update_date, 
            fclty_type, doc_status, prnt_chck_in_id, prnt_doc_typ) " +
            "VALUES ('" + docNum.Replace("'", "''") +
            "', '" + docType.Replace("'", "''") +
            "', '" + strtDte.Replace("'", "''") +
            "', '" + endDte.Replace("'", "''") +
            "', " + srvsTypID + ", " + srvsDteID + ", " + noAdlts +
            ", " + NoChldrn + ", " + spnsID + ", " + spnsSiteID +
            ", " + cstmrID + ", " + cstmrSiteID + ", '" + srcPlace.Replace("'", "''") +
            "', '" + destPlace.Replace("'", "''") +
            "', '" + otherInfo.Replace("'", "''") +
            "', " + Global.myEvnt.user_id + ", '" + dateStr +
            "', " + Global.myEvnt.user_id + ", '" + dateStr +
            "', '" + fcltyType.Replace("'", "''") +
            "', '" + docStatus.Replace("'", "''") +
            "', " + prntChckIn + ", '" + prntDocType.Replace("'", "''") +
            "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void updateCheckIn(long chckInID, string docNum,
  string docType, string strtDte, string endDte, int srvsTypID,
     int srvsDteID, int noAdlts, int NoChldrn, int spnsID, int spnsSiteID,
     int cstmrID, int cstmrSiteID, string srcPlace, string destPlace, string otherInfo,
     string fcltyType, string docStatus, long prntChckIn, string prntDocType)
    {
      strtDte = DateTime.ParseExact(
   strtDte, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

      endDte = DateTime.ParseExact(
   endDte, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

      if (otherInfo.Length > 400)
      {
        otherInfo = otherInfo.Substring(0, 400);
      }
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = @"UPDATE hotl.checkins_hdr
   SET doc_num='" + docNum.Replace("'", "''") +
            "', doc_type='" + docType.Replace("'", "''") +
            "', start_date='" + strtDte.Replace("'", "''") +
            "', end_date='" + endDte.Replace("'", "''") +
            "', service_type_id=" + srvsTypID +
            ", service_det_id=" + srvsDteID +
            ", no_of_adults=" + noAdlts +
            ", no_of_children=" + NoChldrn +
            ", sponsor_id=" + spnsID +
            ", sponsor_site_id=" + spnsSiteID +
            ", customer_id=" + cstmrID +
            ", customer_site_id=" + cstmrSiteID +
            ", arriving_from='" + srcPlace.Replace("'", "''") +
            "', proceeding_to='" + destPlace.Replace("'", "''") +
            "', other_info='" + otherInfo.Replace("'", "''") +
            "', last_update_by=" + Global.myEvnt.user_id + ", last_update_date='" + dateStr +
            "', fclty_type='" + fcltyType.Replace("'", "''") +
            "', doc_status='" + docStatus.Replace("'", "''") +
            "', prnt_chck_in_id = " + prntChckIn +
            ", prnt_doc_typ='" + prntDocType.Replace("'", "''") + "' WHERE (check_in_id =" + chckInID + ")";
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void updatePrntCheckIn(long chckInID, long prntChckIn)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = @"UPDATE hotl.checkins_hdr
   SET prnt_chck_in_id=" + chckInID +
            ", prnt_doc_typ='Check-In', last_update_by=" + Global.myEvnt.user_id + ", last_update_date='" + dateStr +
            "' WHERE (check_in_id =" + chckInID + ")";
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    #endregion

    #region "RECEIVABLES..."
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

    public static int get_DfltSalesLbltyAcnt(int orgID)
    {
      string strSql = "SELECT sales_lblty_acnt_id " +
       "FROM scm.scm_dflt_accnts a " +
       "WHERE(a.org_id = " + orgID + ")";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return -1;
    }

    public static DataSet get_PrsnCstmrDet(long prsnID)
    {
      string strSql = @"SELECT sur_name || ' ' || first_name || ' ' || other_names 
|| ' (' || title || ') (' || local_id_no || ') ' fullname,
       gender, to_char(to_timestamp(date_of_birth,'YYYY-MM-DD'),'DD-Mon-YYYY') dob,
       cntct_no_mobl, email, pstl_addrs, res_address, nationality 
       FROM prs.prsn_names_nos a
       WHERE(a.person_id = " + prsnID + ")";

      return Global.mnFrm.cmCde.selectDataNoParams(strSql);

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
            ", " + Global.myEvnt.user_id + ", '" + dateStr +
            "', " + Global.myEvnt.user_id + ", '" + dateStr +
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
            ", last_update_by=" + Global.myEvnt.user_id +
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
            "', " + Global.myEvnt.user_id + ", '" + dateStr +
            "', " + Global.myEvnt.user_id + ", '" + dateStr +
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
            "', last_update_by=" + Global.myEvnt.user_id +
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
            ", " + Global.myEvnt.user_id + ", '" + dateStr +
            "', " + Global.myEvnt.user_id + ", '" + dateStr +
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
            ", last_update_by=" + Global.myEvnt.user_id +
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
            "', invoice_amount=" + invcAmnt + ", last_update_by=" + Global.myEvnt.user_id +
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
            "', last_update_by=" + Global.myEvnt.user_id +
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
    //         "', last_update_by=" + Global.myEvnt.user_id +
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
            ", last_update_by=" + Global.myEvnt.user_id +
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
            ", last_update_by=" + Global.myEvnt.user_id +
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
            ", last_update_by=" + Global.myEvnt.user_id +
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
            ", last_update_by=" + Global.myEvnt.user_id +
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

    #region "SALES DOCUMENTS..."
    public static void deleteSalesSmmryItm(long docID, string docType, string smmryTyp)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string delSQL = "DELETE FROM scm.scm_doc_amnt_smmrys WHERE src_doc_hdr_id = " +
        docID + " and src_doc_type = '" + docType + "' and smmry_type = '" + smmryTyp +
        "' and code_id_behind = -1";
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }

    public static void deleteSalesSmmryItm(long docID, string docType, string smmryTyp, long codBhnd)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string delSQL = "DELETE FROM scm.scm_doc_amnt_smmrys WHERE src_doc_hdr_id = " +
        docID + " and src_doc_type = '" + docType + "' and smmry_type = '" + smmryTyp + "' and  code_id_behind= " + codBhnd;
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
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
            ", last_update_by = " + Global.myEvnt.user_id + ", " +
            "auto_calc = '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(autoCalc) +
            "', last_update_date = '" + dateStr +
            "', smmry_name='" + smmryNm.Replace("'", "''") + "' WHERE (smmry_id = " + smmryID + ")";
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

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
            ", last_update_by = " + Global.myEvnt.user_id + ", " +
            "auto_calc = '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(autoCalc) +
            "', last_update_date = '" + dateStr +
            "', smmry_name='" + smmryNm.Replace("'", "''") + "' WHERE (smmry_id = " + smmryID + ")";
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static int getUserStoreID()
    {
      string strSql = "select y.subinv_id " +
        "from inv.inv_itm_subinventories y, inv.inv_user_subinventories z " +
        "where y.subinv_id=z.subinv_id and " +
        "y.allow_sales = '1' and z.user_id = " + Global.myEvnt.user_id +
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

    public static DataSet get_DocSmryLns(long dochdrID, string docTyp)
    {
      string strSql = "SELECT a.smmry_id, a.smmry_name, " +
       "a.smmry_amnt, a.code_id_behind, a.smmry_type, a.auto_calc " +
       "FROM scm.scm_doc_amnt_smmrys a " +
       "WHERE((a.src_doc_hdr_id = " + dochdrID +
       ") and (a.src_doc_type='" + docTyp + "')) ORDER BY a.smmry_type";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (Global.wfnCheckinsFrm != null)
      {
        Global.wfnCheckinsFrm.smmry_SQL = strSql;
      }
      if (Global.wfnCheckinsDiag != null)
      {
        Global.wfnCheckinsDiag.smmry_SQL = strSql;
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
                        "', " + Global.myEvnt.user_id + ", '" + dateStr +
                        "', " + Global.myEvnt.user_id + ", '" + dateStr + "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
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
                        "', last_update_by=" + Global.myEvnt.user_id +
                        ", last_update_date='" + dateStr + "' " +
                         "WHERE attchmnt_id = " + attchID;
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
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
        "where y.smmry_type= '" + smmryType + "' and y.code_id_behind = " + codeBhnd +
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

    public static double getSalesSmmryItmAmnt(string smmryType, long codeBhnd,
   long srcDocID, string srcDocTyp)
    {
      //" + codeBhnd +"
      string strSql = "select COALESCE(SUM(y.smmry_amnt),0) " +
        "from scm.scm_doc_amnt_smmrys y " +
        "where y.smmry_type= '" + smmryType +
        "' and y.code_id_behind= y.code_id_behind and y.src_doc_type='" + srcDocTyp +
        "' and y.src_doc_hdr_id=" + srcDocID + " ";
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

    public static double getSalesChrgsSum(
   long srcDocID, string srcDocTyp)
    {
      string strSql = "select COALESCE(SUM(y.smmry_amnt),0) " +
        "from scm.scm_doc_amnt_smmrys y " +
        "where y.smmry_type= '4Extra Charge' and y.code_id_behind >0 and y.src_doc_type='" + srcDocTyp +
        "' and y.src_doc_hdr_id=" + srcDocID + " ";
      Global.mnFrm.cmCde.showSQLNoPermsn(strSql);
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
    //  //CASE WHEN (smmry_type='2Tax') THEN -1*y.smmry_amnt ELSE 
    //  string strSql = "select SUM(y.smmry_amnt) amnt " +
    //    "from scm.scm_doc_amnt_smmrys y " +
    //    "where y.src_doc_hdr_id=" + dochdrID +
    //    " and y.src_doc_type='" + docTyp + "' and substr(y.smmry_type,1,1) IN ('2','5')";
    //  /* != '1Initial Amount' " +
    //    " and y.smmry_type != '6Total Payments Received' and y.smmry_type != " +
    //    "'7Change/Balance' and smmry_type!='3Discount' and smmry_type!='4Extra Charge'*/
    //  DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
    //  double rs = 0;

    //  if (dtst.Tables[0].Rows.Count > 0)
    //  {
    //    double.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rs);
    //  }
    //  return rs;
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

    public static DataSet get_One_SalesDcLines(long dochdrID)
    {
      //      string extrWhere = "";
      //      if (orgnlChckInID > 0)
      //      {
      //        extrWhere = @"and 
      //   (a.other_mdls_doc_id =" + orgnlChckInID + " and a.other_mdls_doc_type='" + orgnDocType.Replace("'", "''") + "')";
      //      }
      string strSql = "SELECT a.invc_det_ln_id, a.itm_id, " +
        "a.doc_qty, a.unit_selling_price, (a.doc_qty * a.unit_selling_price) amnt, " +
        "a.store_id, a.crncy_id, (a.doc_qty - a.qty_trnsctd_in_dest_doc) avlbl_qty, " +
        "a.src_line_id, a.tax_code_id, a.dscnt_code_id, a.chrg_code_id, a.rtrn_reason, " +
        "a.consgmnt_ids, a.orgnl_selling_price, b.base_uom_id, b.item_code, b.item_desc, " +
        @"c.uom_name, a.is_itm_delivered, REPLACE(a.extra_desc || ' (' || a.other_mdls_doc_type || ') " +
        @"(' || scm.get_src_doc_num(a.other_mdls_doc_id, a.other_mdls_doc_type) || ')',' ()','')
        , a.other_mdls_doc_id, a.other_mdls_doc_type, scm.get_src_doc_num(a.other_mdls_doc_id, a.other_mdls_doc_type)  " +
       "FROM scm.scm_sales_invc_det a, inv.inv_itm_list b, inv.unit_of_measure c  " +
       "WHERE(a.invc_hdr_id = " + dochdrID +
       " and a.invc_hdr_id >0 and a.itm_id = b.item_id and b.base_uom_id=c.uom_id) ORDER BY a.invc_det_ln_id";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (Global.wfnCheckinsFrm != null)
      {
        Global.wfnCheckinsFrm.recDt_SQL = strSql;
      }
      if (Global.wfnCheckinsDiag != null)
      {
        Global.wfnCheckinsDiag.recDt_SQL = strSql;
      }
      return dtst;
    }

    public static void updtOrgInvoiceCurrID(int orgID, int crncyID, long pymtMthdID)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE scm.scm_sales_invc_hdr SET invc_curr_id = " + crncyID +
                        ", last_update_by = " + Global.myEvnt.user_id + ", " +
                        "last_update_date = '" + dateStr + "' " +
        "WHERE (org_id = " + orgID + " and invc_curr_id<=0)";
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
      updtSQL = "UPDATE scm.scm_sales_invc_hdr SET pymny_method_id = " + pymtMthdID +
                        ", last_update_by = " + Global.myEvnt.user_id + ", " +
                        "last_update_date = '" + dateStr + "' " +
        "WHERE (org_id = " + orgID + " and pymny_method_id<=0)";
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);

    }

    public static DataSet get_RoomsToClean(int orgID)
    {
      //Global.updateRoomOccpntCnt();
      string strSql = @"SELECT row_number() OVER (ORDER BY tbl1.col2, tbl1.col1) AS ""No.  "", 
                          tbl1.col1 ""Room/Facility No. "", 
                          tbl1.col2 ""Facility Type             "", 
                          tbl1.col3 ""Current No. of Check-Ins "", 
                          COALESCE(tbl1.col4,0) ""No. of Occupants ""
                          FROM (Select a.room_name col1, b.service_type_name col2, 
                          a.crnt_no_occpnts col3, (Select SUM(no_of_adults+no_of_children) 
                          FROM hotl.checkins_hdr y Where y.service_det_id=a.room_id 
                          and y.doc_status = 'Checked-In') col4 FROM hotl.rooms a, hotl.service_types b 
                          where a.service_type_id = b.service_type_id and b.org_id = " + orgID + @" and 
                          (a.needs_hse_keeping='1' or a.crnt_no_occpnts>0) and b.type_of_facility NOT IN ('Restaurant Table')) tbl1 
                          ORDER BY tbl1.col2, tbl1.col1";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      return dtst;
    }

    //    public static long get_OccpntRoomsCnt(int cstmrID, ref string cstmrNm)
    //    {
    //      //Global.updateRoomOccpntCnt();
    //      string whrcls = "";
    //      if (cstmrID > 0)
    //      {
    //        whrcls = " and y.customer_id=" + cstmrID;
    //      }
    //      string strSql = @"SELECT Count(a.room_name), scm.get_cstmr_splr_name(y.customer_id)
    //                          FROM hotl.rooms a, hotl.service_types b, hotl.checkins_hdr y 
    //                          where a.service_type_id = b.service_type_id  and 
    //                          y.service_det_id=a.room_id and y.doc_status = 'Checked-In' 
    //                          and b.type_of_facility IN ('Room/Hall') and (now() between to_timestamp(start_date,'YYYY-MM-DD HH24:MI:SS') 
    //                          and to_timestamp(end_date,'YYYY-MM-DD HH24:MI:SS'))" + whrcls + @"
    //                          GROUP BY y.customer_id
    //                          HAVING Count(a.room_name)>1";

    //      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
    //      if (dtst.Tables[0].Rows.Count > 0)
    //      {
    //        cstmrNm = dtst.Tables[0].Rows[0][1].ToString();
    //        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
    //      }
    //      return 0;
    //    }

    public static DataSet get_SalesMoneyRcvd(long UsrID, string doctype, string strtDte, string endDte, int orgID, string ordrBy)
    {
      /*
   y.user_name ""Sales Agent"",*/
      string usrCls = "";
      if (UsrID > 0)
      {
        usrCls = " and (y.user_id = " + UsrID + ")";
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

      string strSql = @"SELECT row_number() OVER (ORDER BY " + ordrBy + @") AS ""No.  ""
, tbl1.col1 ""Document No.                     "", tbl1.col2 ""  Invoice Amount"", tbl1.col3 "" Discount Amount"",
tbl1.col4 ""     Amount Paid"", tbl1.col5 ""Outstanding Amt."", tbl1.col6 ""Creation Date           "", tbl1.col7 ""mt""
FROM (SELECT a.invc_number || ' (' || COALESCE(scm.get_cstmr_splr_name(a.customer_id),'Unknown') || ')' col1, 
scm.get_doc_smry_typ_amnt(a.invc_hdr_id, a.invc_type, '5Grand Total') + 
abs(scm.get_doc_smry_typ_amnt(a.invc_hdr_id, a.invc_type, '3Discount')) col2, 
scm.get_doc_smry_typ_amnt(a.invc_hdr_id, a.invc_type, '3Discount') col3,
scm.get_doc_smry_typ_amnt(a.invc_hdr_id, a.invc_type, '6Total Payments Received') col4, 
scm.get_doc_smry_typ_amnt(a.invc_hdr_id, a.invc_type, '7Change/Balance') col5, 
to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),
'DD-Mon-YYYY HH24:MI:SS') col6,
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
SELECT a.rcvbls_invc_number  || ' (' || COALESCE(scm.get_cstmr_splr_name(a.customer_id),'Unknown') || ')' col1, 
accb.get_rcvbl_smry_typ_amnt(a.rcvbls_invc_hdr_id, a.rcvbls_invc_type, '6Grand Total') + 
abs(accb.get_rcvbl_smry_typ_amnt(a.rcvbls_invc_hdr_id, a.rcvbls_invc_type, '3Discount')) col2, 
accb.get_rcvbl_smry_typ_amnt(a.rcvbls_invc_hdr_id, a.rcvbls_invc_type, '3Discount') col3,
accb.get_rcvbl_smry_typ_amnt(a.rcvbls_invc_hdr_id, a.rcvbls_invc_type, '7Total Payments Made') col4, 
accb.get_rcvbl_smry_typ_amnt(a.rcvbls_invc_hdr_id, a.rcvbls_invc_type, '8Outstanding Balance') col5, 
to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),
'DD-Mon-YYYY HH24:MI:SS') col6, a.creation_date col7 
FROM accb.accb_rcvbls_invc_hdr a, 
sec.sec_users y WHERE ((a.approval_status ilike 'Approved') AND (a.org_id = " + orgID + @") AND 
(a.created_by=y.user_id)" + usrCls + @" and (a.rcvbls_invc_type ilike '%Advance%Payment%') 
and (to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS') between 
to_timestamp('" + strtDte + @"','DD-Mon-YYYY HH24:MI:SS') AND 
to_timestamp('" + endDte + @"','DD-Mon-YYYY HH24:MI:SS')))) tbl1 
ORDER BY " + ordrBy + @"";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      return dtst;
    }

    public static DataSet get_ItemsSold(long UsrID, string doctype, string strtDte, string endDte, int orgID, string ordrBy)
    {
      /*
   y.user_name ""Sales Agent"",*/
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
      //REPLACE( || ' (' || a.comments_desc || ')','()','')
      //, a.invc_number, a.comments_desc
      string strSql = @"SELECT row_number() OVER (ORDER BY " + ordrBy + @" c.item_desc ASC) AS ""No.  ""
        , trim(c.item_code || ' ' || REPLACE(c.item_desc,c.item_code,'')) ""Item Code/Desc.  "", 
        array_to_string(array_agg(distinct a.invc_number),', ') ""Document Numbers           "", 
        SUM(b.doc_qty) ""QTY      "", 
        d.uom_name ""UOM     "", 
        b.unit_selling_price ""Sales Price   "", 
        SUM(b.doc_qty * b.unit_selling_price) ""Total Amount      "",
        c.item_desc mt
        FROM scm.scm_sales_invc_hdr a, sec.sec_users y, scm.scm_sales_invc_det b, inv.inv_itm_list c, inv.unit_of_measure d
        WHERE ((a.invc_hdr_id = b.invc_hdr_id AND b.itm_id = c.item_id AND c.base_uom_id = d.uom_id) 
        AND (a.approval_status ilike 'Approved' or b.is_itm_delivered ='1') AND (a.org_id = " + orgID + @") AND 
        (a.created_by=y.user_id)" + usrCls + " and (a.invc_type ilike '" + doctype.Replace("'", "''") + @"') 
        and (to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS') between 
        to_timestamp('" + strtDte + @"','DD-Mon-YYYY HH24:MI:SS') AND 
        to_timestamp('" + endDte + @"','DD-Mon-YYYY HH24:MI:SS'))) 
        GROUP BY c.item_desc, b.itm_id, c.item_code, d.uom_name, b.unit_selling_price
        ORDER BY " + ordrBy + @" c.item_desc ASC";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      return dtst;
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
       "a.other_mdls_doc_id,scm.get_src_doc_num(a.other_mdls_doc_id,a.other_mdls_doc_type) doc_no, a.other_mdls_doc_type " +
       "FROM scm.scm_sales_invc_hdr a " +
       "WHERE(a.invc_hdr_id = " + dochdrID +
       ")";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      return dtst;
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

      strSql = "SELECT a.invc_hdr_id, a.invc_number, a.invc_type " +
   "FROM scm.scm_sales_invc_hdr a " +
   "WHERE (" + whereClause + "(a.org_id = " + orgID +
   ")" + crtdByClause + unpstdCls + ") ORDER BY a.invc_hdr_id DESC LIMIT " + limit_size +
   " OFFSET " + (Math.Abs(offset * limit_size)).ToString();

      //Global.invcFrm.rec_SQL = strSql;
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
               ", '" + trnsdte.Replace("'", "''") + "', " + crncyid + ", " + Global.myEvnt.user_id +
               ", '" + dateStr + "', " + batchid + ", " + crdtamnt + ", " +
               Global.myEvnt.user_id + ", '" + dateStr + "', " + netamnt +
               ", '0', '" + srcids + "', " + entrdAmt +
                        ", " + entrdCurrID + ", " + acntAmnt +
                        ", " + acntCurrID + ", " + funcExchRate +
                        ", " + acntExchRate + ", '" + dbtOrCrdt + "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
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
               ", '" + trnsdte.Replace("'", "''") + "', " + crncyid + ", " + Global.myEvnt.user_id +
               ", '" + dateStr + "', " + crdtamnt + ", " +
               Global.myEvnt.user_id + ", '" + dateStr + "', " + netamnt +
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
               ", '" + trnsdte.Replace("'", "''") + "', " + crncyid + ", " + Global.myEvnt.user_id +
               ", '" + dateStr + "', " + crdtamnt + ", " +
               Global.myEvnt.user_id + ", '" + dateStr + "', " + netamnt +
               ", -1, '" + srcDocTyp.Replace("'", "''") + "', " +
               srcDocID + ", " + srcDocLnID + ", '" + trnsSrc + "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }
    #endregion

    #region "ITEMS..."
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
            "', " + srcDocHdrID + ", " + Global.myEvnt.user_id + ", '" + dateStr +
            "', " + Global.myEvnt.user_id + ", '" + dateStr + "', '" +
            Global.mnFrm.cmCde.cnvrtBoolToBitStr(autoCalc) + "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void createSalesDocHdr(int orgid, string docNum,
      string desc, string docTyp, string docdte, string pymntTrms,
      int cstmrID, int siteID, string apprvlSts,
      string nxtApprvl, long srcDocID, int rcvblAcntID,
      int pymntID, int invcCurrID, double exchRate,
      long chckInID, string chckInType, bool enblAutoChrg,
      long event_rgstr_id, string evntCtgry, string evntType)
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
            "event_rgstr_id, evnt_cost_category, event_doc_type) " +
            "VALUES ('" + docdte.Replace("'", "''") +
            "', '" + pymntTrms.Replace("'", "''") +
            "', " + cstmrID + ", " + siteID + ", '" + desc.Replace("'", "''") +
            "', '" + apprvlSts.Replace("'", "''") + "', " + Global.myEvnt.user_id + ", '" + dateStr +
            "', " + Global.myEvnt.user_id + ", '" + dateStr +
            "', '" + nxtApprvl.Replace("'", "''") +
            "', '" + docNum.Replace("'", "''") + "', '" +
            docTyp.Replace("'", "''") + "', " + srcDocID + ", " +
            orgid + ", " + rcvblAcntID + ", " + pymntID + ", "
            + invcCurrID + ", " + exchRate + "," + chckInID + ",'" + chckInType +
            "','" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(enblAutoChrg) +
            "'," + event_rgstr_id + ", '" + evntCtgry.Replace("'", "''") + "', '" + evntType.Replace("'", "''") +
            "')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void createSalesDocLn(long lineid, long docID, int itmID,
      double qty, double untPrice, int storeID,
      int crncyID, long srclnID, int txCode, int dscntCde,
      int chrgeCde, string rtrnRsn, string cnsgmntIDs, double orgnlPrice,
      bool isDlvrd, long otherMdlID, string otherMdlType, string extrDesc)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = "INSERT INTO scm.scm_sales_invc_det(invc_det_ln_id, " +
            "invc_hdr_id, itm_id, doc_qty, unit_selling_price, " +
            "created_by, creation_date, last_update_by, last_update_date, " +
            "store_id, crncy_id, src_line_id, tax_code_id, " +
            "dscnt_code_id, chrg_code_id, qty_trnsctd_in_dest_doc, " +
            "rtrn_reason, consgmnt_ids, orgnl_selling_price, is_itm_delivered, " +
            "other_mdls_doc_id, other_mdls_doc_type, extra_desc) " +
            "VALUES (" + lineid +
            "," + docID +
            ", " + itmID +
            ", " + qty + ", " + untPrice + ", " + Global.myEvnt.user_id + ", '" + dateStr +
            "', " + Global.myEvnt.user_id + ", '" + dateStr +
            "', " + storeID +
            ", " + crncyID + ", " + srclnID + ", " + txCode +
            ", " + dscntCde + ", " + chrgeCde + ", 0, '" +
            rtrnRsn.Replace("'", "''") + "', '" + cnsgmntIDs.Replace("'", "''") +
            "', " + orgnlPrice + ", " + Global.mnFrm.cmCde.cnvrtBoolToBitStr(isDlvrd) +
            ", " + otherMdlID + ", '" + otherMdlType.Replace("'", "''") +
            "', '" + extrDesc.Replace("'", "''") +
            "')";
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
        ", " + totQty + ", " + rsvdQty + ", " + avlblQty + ", '" + balsDate + "', " + Global.myEvnt.user_id + ", '" + dateStr +
                        "', " + Global.myEvnt.user_id + ", '" + dateStr + "', ',')";
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
        "', " + Global.myEvnt.user_id + ", '" + dateStr +
                        "', " + Global.myEvnt.user_id + ", '" + dateStr + "', ',')";
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
       "', " + srcDocID + ", " + Global.myEvnt.user_id + ", '" + dateStr + "', " +
               Global.myEvnt.user_id + ", '" + dateStr + "', '" + dateRcvd + "')";
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
       "', " + Global.myEvnt.user_id + ", '" + dateStr + "', " + orgid + ", '0', " +
               Global.myEvnt.user_id + ", '" + dateStr + "', '" +
               batchsource.Replace("'", "''") + "', '0')";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void updtTodaysGLBatchPstngAvlblty(long batchid, string avlblty)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string insSQL = "UPDATE accb.accb_trnsctn_batches SET avlbl_for_postng='" + avlblty +
        "', last_update_by=" + Global.myEvnt.user_id +
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
      return sumRes;
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
      return sumRes;
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

    public static void updtEventCostGLBatch(long asstCostID,
long glBatchID)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE attn.attn_attendance_costs SET " +
            "gl_batch_id=" + glBatchID +
            ", last_update_by=" + Global.myEvnt.user_id +
            ", last_update_date='" + dateStr +
            "' WHERE (attnd_cost_id = " +
            asstCostID + ")";
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static long getTrnsID(string trsDesc, int accntID, double entrdAmnt, int entrdCurID, string trnsDate)
    {
      string selSql = @"Select transctn_id from accb.accb_trnsctn_details
   where accnt_id=" + accntID + " and transaction_desc='" + trsDesc.Replace("'", "''") +
                       "' and entered_amnt =" + entrdAmnt + " and " +
      "entered_amt_crncy_id=" + entrdCurID + " and trnsctn_date = '" + trnsDate.Replace("'", "''") + "'";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSql);

      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return -1;
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
                        ", '" + trnsDate + "', " + crncyid + ", " + Global.myEvnt.user_id + ", '" + dateStr +
                        "', " + batchid + ", " + crdtamnt + ", " + Global.myEvnt.user_id +
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
                        "', " + Global.myEvnt.user_id + ", '" + dateStr +
                        "', " + orgid + ", '0', " + Global.myEvnt.user_id + ", '" + dateStr +
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
    "SET last_update_by = " + Global.myEvnt.user_id +
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
    "SET last_update_by = " + Global.myEvnt.user_id +
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
    "SET last_update_by = " + Global.myEvnt.user_id +
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
    "SET last_update_by = " + Global.myEvnt.user_id +
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

    public static void updtSrcDocTrnsctdQty(long src_lnid,
     double qty)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE scm.scm_sales_invc_det SET " +
            "qty_trnsctd_in_dest_doc=qty_trnsctd_in_dest_doc+" + qty +
            ", last_update_by=" + Global.myEvnt.user_id +
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
            "approval_status='" + apprvlSts + "', last_update_by=" + Global.myEvnt.user_id +
            ", last_update_date='" + dateStr +
            "', next_aproval_action='" + nxtApprvl.Replace("'", "''") +
            "' WHERE (invc_hdr_id = " +
            docid + ")";
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void updtCheckInStatus(long docid,
      string apprvlSts)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE hotl.checkins_hdr SET " +
            "doc_status='" + apprvlSts + "', last_update_by=" + Global.myEvnt.user_id +
            ", last_update_date='" + dateStr +
            "' WHERE (check_in_id = " +
            docid + " or prnt_chck_in_id = " + docid + ")";
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void updtRoomDirtyStatus(long roomID, bool isDirty)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE hotl.rooms SET " +
            "needs_hse_keeping='" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(isDirty) +
            "', last_update_by=" + Global.myEvnt.user_id +
            ", last_update_date='" + dateStr +
            "' WHERE (room_id = " +
            roomID + ")";
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void updateSalesDocLn(long lnID, long nwSalesDocID)
    {
      //long othrMdlID, string othMdlType,
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE scm.scm_sales_invc_det SET " +
            "last_update_by = " + Global.myEvnt.user_id +
            ", last_update_date= '" + dateStr +
            "', invc_hdr_id = " + nwSalesDocID +
            " WHERE (invc_det_ln_id = " + lnID + ")";
      /*,             "', other_mdls_doc_id = " + otherMdlID +
other_mdls_doc_type = '" + otherMdlType.Replace("'", "''") +
            "' */
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void updateSalesDocLn(long lnID, int itmID,
      double qty, double untPrice, int storeID,
      int crncyID, long srclnID, int txCode, int dscntCde,
      int chrgeCde, string rtrnRsn, string cnsgmntIDs,
      double orgnlPrice, long otherMdlID,
      string otherMdlType, string extrDesc)
    {
      /* long otherMdlID,
      string otherMdlType, */
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE scm.scm_sales_invc_det SET " +
            "itm_id=" + itmID +
            ", doc_qty =" + qty +
            ", unit_selling_price= " + untPrice +
            ", orgnl_selling_price= " + orgnlPrice + ", " +
            "last_update_by = " + Global.myEvnt.user_id +
            ", last_update_date= '" + dateStr + "', " +
            "store_id=" + storeID +
            ", crncy_id =" + crncyID + ", src_line_id = " + srclnID +
            ", tax_code_id = " + txCode +
            ", dscnt_code_id = " + dscntCde +
            ", chrg_code_id = " + chrgeCde +
            ", rtrn_reason = '" + rtrnRsn.Replace("'", "''") +
            "', consgmnt_ids ='" + cnsgmntIDs.Replace("'", "''") +
        "', other_mdls_doc_id = " + otherMdlID +
        ", other_mdls_doc_type = '" + otherMdlType.Replace("'", "''") +
            "', extra_desc ='" + extrDesc.Replace("'", "''") +
            "' WHERE (invc_det_ln_id = " + lnID + ")";
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

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

    public static long getSalesLnsDlvrd(long docID)
    {
      string updtSQL = "SELECT count(1) from scm.scm_sales_invc_det " +
            " WHERE (is_itm_delivered = '1' and invc_hdr_id = " + docID + ")";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(updtSQL);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return 0;
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

    public static void updtSalesDocHdr(long docid, string docNum,
      string desc, string docTyp, string docdte, string pymntTerms,
      int spplrID, int spplrSiteID, string apprvlSts,
      string nxtApprvl, long srcDocID,
      int pymntID, int invcCurrID, double exchRate, long chckInID,
      string chckInType, bool enblAutoChrg,
      long event_rgstr_id, string evntCtgry, string evntType)
    {
      docdte = DateTime.ParseExact(docdte, "dd-MMM-yyyy",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE scm.scm_sales_invc_hdr SET " +
            "invc_date='" + docdte.Replace("'", "''") +
            "', payment_terms='" + pymntTerms.Replace("'", "''") +
            "', customer_id=" + spplrID + ", " +
            "customer_site_id=" + spplrSiteID + ", comments_desc='" + desc.Replace("'", "''") +
            "', approval_status='" + apprvlSts.Replace("'", "''") + "', last_update_by=" + Global.myEvnt.user_id +
            ", last_update_date='" + dateStr +
            "', next_aproval_action='" + nxtApprvl.Replace("'", "''") +
            "', invc_number='" + docNum.Replace("'", "''") + "', invc_type='" +
            docTyp.Replace("'", "''") + "', src_doc_hdr_id=" + srcDocID +
            ", pymny_method_id=" + pymntID + ", invc_curr_id=" + invcCurrID +
            ", exchng_rate=" + exchRate +
            ", other_mdls_doc_id=" + chckInID +
            ", other_mdls_doc_type='" + chckInType.Replace("'", "''") + "' " +
            ", enbl_auto_misc_chrges='" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(enblAutoChrg) + "' " +
            ", event_rgstr_id=" + event_rgstr_id +
            ", evnt_cost_category='" + evntCtgry.Replace("'", "''") +
            "', event_doc_type = '" + evntType.Replace("'", "''") + "' " +
            "WHERE (invc_hdr_id = " + docid + ")";
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
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
      //Global.mnFrm.cmCde.showMsg(dailybalID + "/" + csgmtID + "/" + trnsDate, 0);
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

    public static DataSet get_ItemExtInf(long itmID)
    {
      string strSql = "";

      strSql = @"SELECT a.image, a.extra_info, a.other_desc, generic_name, trade_name, drug_usual_dsge, drug_max_dsge, 
       contraindications, food_interactions " +
    "FROM inv.inv_itm_list a WHERE a.item_id = " + itmID;
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      return dtst;
    }

    public static DataSet get_ItemInf(long itmID, long cstmrSiteID)
    {
      string strSql = "";

      strSql = @"SELECT a.item_code, a.item_desc, 
a.selling_price, a.tax_code_id, CASE WHEN scm.get_cstmr_splr_dscntid("
      + cstmrSiteID + ") != -1 THEN scm.get_cstmr_splr_dscntid("
      + cstmrSiteID + @") ELSE a.dscnt_code_id END, a.extr_chrg_id, 
       a.item_type, a.base_uom_id, a.orgnl_selling_price " +
    "FROM inv.inv_itm_list a WHERE a.item_id = " + itmID;
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      return dtst;
    }

    public static DataSet fillDataSetFxn(string selSQL)
    {
      return Global.mnFrm.cmCde.selectDataNoParams(selSQL);
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
      + cstmrSiteID + ") ELSE a.dscnt_code_id END , a.extr_chrg_id, c.consgmt_id, c.cost_price, c.expiry_date " +
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
      + cstmrSiteID + ") ELSE a.dscnt_code_id END , a.extr_chrg_id, c.consgmt_id, c.cost_price, c.expiry_date " +
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
      + cstmrSiteID + ") ELSE a.dscnt_code_id END, a.extr_chrg_id " +
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
      int orgID, int storeID, string docTyp,
      bool cnsgmtsOnly, long itmID)
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
        "WHERE ((c.itm_id=" + itmID + ") and (c.subinv_id =" + Global.selectedStoreID +
        ") and  (inv.get_csgmt_lst_avlbl_bls(c.consgmt_id)>0)) ORDER BY c.consgmt_id ASC";

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

    public static string getOldstItmCnsgmtsForStock(long itmID, double qnty, int storeID)
    {
      string res = ",";
      string strSql = "SELECT distinct c.consgmt_id, inv.get_csgmt_lst_avlbl_bls(c.consgmt_id) " +
        "FROM inv.inv_consgmt_rcpt_det c " +
        "WHERE ((c.itm_id=" + itmID + ") and (c.subinv_id =" + storeID +
        ") and (inv.get_csgmt_lst_avlbl_bls(c.consgmt_id)>0)) ORDER BY c.consgmt_id ASC";

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
        "WHERE ((c.itm_id=" + itmID + ") and (c.subinv_id =" + storeID +
        ") and (inv.get_csgmt_lst_avlbl_bls(c.consgmt_id)>0)) ORDER BY c.consgmt_id ASC";

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
      if (cnsgmtIDs == "")
      {
        cnsgmtIDs = "-123412";
      }
      string strSql = "SELECT distinct c.consgmt_id, inv.get_csgmt_lst_avlbl_bls(c.consgmt_id) " +
        "FROM inv.inv_consgmt_rcpt_det c " +
        "WHERE ((c.consgmt_id IN (" + cnsgmtIDs.Trim(',') + ")) and (inv.get_csgmt_lst_avlbl_bls(c.consgmt_id)>0)) ORDER BY c.consgmt_id ASC";

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


    public static List<string[]> getItmCnsgmtVals(long itmID)
    {
      List<string[]> res = new List<string[]>();

      string strSql = "SELECT distinct c.consgmt_id, inv.get_csgmt_lst_tot_bls(c.consgmt_id), c.cost_price " +
        "FROM inv.inv_consgmt_rcpt_det c " +
        "WHERE ((c.itm_id=" + itmID + ") and (c.subinv_id =" + Global.selectedStoreID +
        ") and  (inv.get_csgmt_lst_tot_bls(c.consgmt_id)>0)) ORDER BY c.consgmt_id ASC";

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
      List<string[]> res = new List<string[]>();
      if (cnsgmtIDs == "")
      {
        cnsgmtIDs = "-123412";
      }

      string strSql = "SELECT distinct c.consgmt_id, inv.get_csgmt_lst_avlbl_bls(c.consgmt_id), c.cost_price " +
        "FROM inv.inv_consgmt_rcpt_det c " +
        "WHERE ((c.consgmt_id IN (" + cnsgmtIDs.Trim(',') + "))) ORDER BY c.consgmt_id ASC";
      // and (inv.get_csgmt_lst_avlbl_bls(c.consgmt_id)>0)
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
          string[] rec = new string[3];//Very very important to avoid same values entering List several times
          try
          {
            rec[0] = ary1[i];
            rec[1] = ary[i];
            rec[2] = Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_consgmt_rcpt_det", "consgmt_id", "cost_price", long.Parse(ary1[i]));
            res.Add(rec);
            //Global.mnFrm.cmCde.showMsg(rec[0] + "/" + rec[1] + "/" + rec[2], 0);

          }
          catch (Exception ex)
          {
            rec[0] = ary1[i];
            rec[1] = "0";
            rec[2] = "0";
            res.Add(rec);
            //Global.mnFrm.cmCde.showMsg(rec[0] + "/" + rec[1] + "/" + rec[2], 0);
          }
        }
      }
      //Global.mnFrm.cmCde.showMsg(res[0][0] + "/" + res[0][1] + "/" + res[0][2], 0);
      //if (res.Count == 2)
      //{
      //  Global.mnFrm.cmCde.showMsg(res[1][0] + "/" + res[1][1] + "/" + res[1][2], 0);
      //}
      return res;
    }

    public static double getItmTrnsfTtlCost(double qnty, string cnsgmtIDs)
    {
      if (cnsgmtIDs == "")
      {
        cnsgmtIDs = "-123412";
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

    #region "PAYMENTS..."
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
            "', " + Global.myEvnt.user_id + ", '" + dateStr +
            "', " + Global.myEvnt.user_id + ", '" + dateStr +
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
      "', last_update_by=" + Global.myEvnt.user_id +
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
      "', last_update_by=" + Global.myEvnt.user_id +
      ", last_update_date='" + dateStr +
      "' WHERE batch_id = " + batchid;
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
    }

    public static void updtPymntsBatchVldty(long batchID, string vldtyStatus)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = @"UPDATE accb.accb_payments_batches SET 
            last_update_by=" + Global.myEvnt.user_id +
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
            last_update_by=" + Global.myEvnt.user_id +
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
            "', last_update_by=" + Global.myEvnt.user_id +
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
            ", " + Global.myEvnt.user_id + ", '" + dateStr +
            "', " + Global.myEvnt.user_id + ", '" + dateStr +
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
            ", last_update_by=" + Global.myEvnt.user_id +
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
            "', last_update_by=" + Global.myEvnt.user_id +
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
            ", last_update_by=" + Global.myEvnt.user_id +
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
            ", last_update_by=" + Global.myEvnt.user_id +
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
            ", last_update_by=" + Global.myEvnt.user_id +
            ", last_update_date='" + dateStr +
            "' WHERE (pybls_invc_hdr_id = " +
            docid + ")";
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
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
                        "', " + Global.myEvnt.user_id + ", '" + dateStr +
                        "', " + Global.myEvnt.user_id + ", '" + dateStr + "')";
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
                        "', last_update_by=" + Global.myEvnt.user_id +
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
      " and NOT EXISTS(select f.transctn_id from accb.accb_trnsctn_details f " +
      "where f.batch_id IN (select g.batch_id from accb.accb_trnsctn_batches g " +
      "where g.batch_name ilike '%Inventory%' and " +
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
      string strSql = "select distinct a.interface_id from scm.scm_gl_interface a " +
           "where a.accnt_id = " + accntid + " and a.trnsctn_date = '" + trns_date +
           "' and a.func_cur_id = " + crncy_id + " and a.gl_batch_id = -1 and NOT EXISTS(select f.transctn_id from accb.accb_trnsctn_details f " +
           "where f.batch_id IN (select g.batch_id from accb.accb_trnsctn_batches g " +
           "where g.batch_name ilike '%Sales & Purchasing%' and " +
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

    #region "EVENTS..."
    public static void deleteComplaint(long lnID)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string delSQL = "DELETE FROM hotl.cmplnts_obsvrtns WHERE complaint_id = " +
        lnID + "";
      Global.mnFrm.cmCde.deleteDataNoParams(delSQL);
    }

    public static DataSet get_Complaints(string searchWord, string searchIn, long offset,
      int limit_size, long chkInID)
    {
      string strSql = "";
      string whrcls = "";
      string extrWhere = "";
      if (chkInID > 0)
      {
        extrWhere = " AND (a.src_doc_id = " + chkInID + ")";
      }
      /*Complaint/Observation Type
Customer
Description
Status
Person to Resolve
       */
      if (searchIn == "Complaint/Observation Type")
      {
        whrcls = " AND (a.classification ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Date Created")
      {
        whrcls = " AND (to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Customer")
      {
        whrcls = " AND (COALESCE(scm.get_cstmr_splr_name(a.customer_id),'') ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Description")
      {
        whrcls = " AND (a.description ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Person to Resolve")
      {
        whrcls = " AND (COALESCE(prs.get_prsn_name(a.person_to_resolve),'') ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Status")
      {
        whrcls = " AND ((CASE WHEN a.is_resolved='1' THEN 'RESOLVED' ELSE 'PENDING' END) ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      strSql = @"SELECT a.complaint_id, 
a.classification, a.description,a.suggestion_solution, a.customer_id, scm.get_cstmr_splr_name(a.customer_id),
a.person_to_resolve, prs.get_prsn_name(a.person_to_resolve),a.is_resolved,
      (CASE WHEN a.is_resolved='1' THEN 'RESOLVED' ELSE 'PENDING' END), 
      to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS')
|| ' (' || hotl.get_doc_num(a.src_doc_id) || ')'
  FROM hotl.cmplnts_obsvrtns a " +
        "WHERE(a.org_id = " + Global.mnFrm.cmCde.Org_id + whrcls + extrWhere +
        ") ORDER BY a.creation_date, 1 LIMIT " + limit_size +
        " OFFSET " + (Math.Abs(offset * limit_size)).ToString();

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      Global.wfnCmplntsFrm.rec_SQL = strSql;
      return dtst;
    }

    public static long get_Total_Complaints(string searchWord, string searchIn, long chkInID)
    {
      string strSql = "";
      string whrcls = "";
      string extrWhere = "";
      if (chkInID > 0)
      {
        extrWhere = " AND (a.src_doc_id = " + chkInID + ")";
      }
      /*Complaint/Observation Type
Customer
Description
Status
Person to Resolve
       */
      if (searchIn == "Complaint/Observation Type")
      {
        whrcls = " AND (a.classification ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Date Created")
      {
        whrcls = " AND (to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Customer")
      {
        whrcls = " AND (COALESCE(scm.get_cstmr_splr_name(a.customer_id),'') ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Description")
      {
        whrcls = " AND (a.description ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Person to Resolve")
      {
        whrcls = " AND (COALESCE(prs.get_prsn_name(a.person_to_resolve),'') ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      else if (searchIn == "Status")
      {
        whrcls = " AND ((CASE WHEN a.is_resolved='1' THEN 'RESOLVED' ELSE 'PENDING' END) ilike '" + searchWord.Replace("'", "''") +
       "')";
      }
      strSql = @"SELECT count(1) 
  FROM hotl.cmplnts_obsvrtns a, hotl.checkins_hdr b " +
        "WHERE(a.org_id = " + Global.mnFrm.cmCde.Org_id + whrcls + extrWhere +
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

    public static long getNewCmplntID()
    {
      string strSql = "select nextval('hotl.cmplnts_obsvrtns_complaint_id_seq')";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return -1;
    }

    public static void createComplaint(long complaint_id, long prsnID, long srcDocID, long cstmrID,
string srcDocType, string clssfctn, string descptn, string sltn, bool isRslvd)
    {
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string insSQL = @"INSERT INTO hotl.cmplnts_obsvrtns(
            complaint_id, classification, description, suggestion_solution, 
            customer_id, src_doc_id, src_doc_type, is_resolved, created_by, 
            creation_date, last_update_by, last_update_date, person_to_resolve, org_id) " +
            "VALUES (" + complaint_id + ", '" + clssfctn.Replace("'", "''") +
            "', '" + descptn.Replace("'", "''") +
            "', '" + sltn.Replace("'", "''") +
            "', " + cstmrID + ", " + srcDocID + ", '" + srcDocType.Replace("'", "''") +
            "',  '" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(isRslvd) +
            "', " + Global.myEvnt.user_id + ", '" + dateStr +
            "', " + Global.myEvnt.user_id + ", '" + dateStr +
            "', " + prsnID +
            ", " + Global.mnFrm.cmCde.Org_id +
            ")";
      Global.mnFrm.cmCde.insertDataNoParams(insSQL);
    }

    public static void updateComplaint(long cplntID, long prsnID, long srcDocID, long cstmrID,
string srcDocType, string clssfctn, string descptn, string sltn, bool isRslvd)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
      string updtSQL = "UPDATE hotl.cmplnts_obsvrtns SET " +
            "classification='" + clssfctn.Replace("'", "''") +
            "', description='" + descptn.Replace("'", "''") +
            "', suggestion_solution='" + sltn.Replace("'", "''") +
            "', is_resolved='" + Global.mnFrm.cmCde.cnvrtBoolToBitStr(isRslvd) +
            "', person_to_resolve=" + prsnID +
            ", last_update_by = " + Global.myEvnt.user_id + ", " +
            "last_update_date = '" + dateStr +
            "' WHERE (complaint_id =" + cplntID + ")";
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
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

    public static double get_InvItemPriceLsTx(int itmID)
    {
      string strSql = "SELECT orgnl_selling_price " +
   "FROM inv.inv_itm_list a " +
   "WHERE item_id =" + itmID + "";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return double.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return 0.00;
    }

    public static double get_InvItemPrice(int itmID)
    {
      string strSql = "SELECT selling_price " +
   "FROM inv.inv_itm_list a " +
   "WHERE item_id =" + itmID + "";

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return double.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return 0.00;
    }

    public static double getCstmrDpsts(int cstmrID, int invcurID)
    {
      string selSQL = @"select SUM(invoice_amount-invc_amnt_appld_elswhr) c, customer_id e, 
invc_curr_id f from accb.accb_rcvbls_invc_hdr where (((rcvbls_invc_type = 'Customer Advance Payment' and (invoice_amount-amnt_paid)<=0) 
or rcvbls_invc_type = 'Customer Debit Memo (InDirect Refund)') 
and approval_status='Approved' and (invoice_amount-invc_amnt_appld_elswhr)>0 and customer_id = " + cstmrID + " and customer_id>0 and invc_curr_id = " + invcurID + @") 
GROUP BY customer_id,invc_curr_id";
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return double.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return 0;
    }

    #endregion
    #endregion
    #endregion

    #region "CUSTOM FUNCTIONS..."
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

    public static void createRqrdLOVs()
    {
      string[] sysLovs = { "Venue Classifications", "Event Classifications", 
                           "Event Venues", "Attendance Events","Time Table Event Lines",
                           "Time Tables", "Attendance HeadCount Metrics",
                               "Visitor Classifications", "Ad hoc Visitors",
                               "Labels for Attendance Points", "Event Cost Categories",
                               "Event Price Categories","Attendance Registers" };
      string[] sysLovsDesc = { "Venue Classifications", "Event Classifications", 
                               "Event Venues", "Attendance Events","Time Table Event Lines",
                               "Time Tables", "Attendance HeadCount Metrics",
                               "Visitor Classifications", "Ad hoc Visitors",
                               /*9*/"Labels for Attendance Points", "Event Cost Categories",
                               "Event Price Categories", "Attendance Registers" };
      string[] sysLovsDynQrys = { "", "",
      "select distinct trim(to_char(venue_id,'999999999999999999999999999999')) a, venue_name b, '' c, org_id d from attn.attn_event_venues where is_enabled='1' order by venue_name",
      "select distinct trim(to_char(event_id,'999999999999999999999999999999')) a, event_name b, '' c, org_id d from attn.attn_attendance_events where is_enabled='1' order by event_name",
      "SELECT distinct trim(to_char(time_table_det_id,'999999999999999999999999999999')) a, 'EVENT: ' || COALESCE(attn.get_event_name(event_id),'') || ' VENUE: ' || COALESCE(attn.get_venue_name(assgnd_venue_id),'') || ' HOST: ' || COALESCE(prs.get_prsn_name(assgnd_host_id),'') b, '' c, 1 d, time_table_id e FROM attn.attn_time_table_details where is_enabled='1' order by 2",
      "select distinct trim(to_char(time_table_id,'999999999999999999999999999999')) a, time_table_name b, '' c, org_id d from attn.attn_time_table_hdrs where is_enabled='1' order by time_table_name",
      "","","","","",
      "select distinct '' || price_ctgry_id a, price_category b, '' c, event_id d from attn.event_price_categories where is_enabled='1' order by price_category",
      "select distinct '' || recs_hdr_id a, recs_hdr_name || '(' || recs_hdr_desc || ')' b, '' c, org_id d from attn.attn_attendance_recs_hdr"};
      string[] pssblVals = { 
        "0", "Standard Size Venue", "Standard Size Venue",
        "0", "Medium Size Venue", "Medium Size Venue",
        "0", "Large Size Venue", "Large Size Venue",
        "0", "Small Size Venue", "Small Size Venue",
        "0", "Rented Venue", "Rented Venue",
        "0", "Own Venue", "Own Venue",
        "1", "All-Inclusive Event", "All-Inclusive Event",
        "1", "Group-Specific Event", "Group-Specific Event",
        "6", "Male Attendance","Male Attendance",
        "6", "Female Attendance","Female Attendance",
        "6", "Total Attendance","Total Attendance",
        "7", "Existing Person","Existing Person",
        "7", "Customer","Customer",
        "7", "Vehicle","Vehicle",
        "7", "Visitor","Visitor",
        "9", "1. CPD","Continuous Professional Development",
        "9", "2. Exam Score","Scores Obtained if Examinations were taken",
        "10", "1. Event Fee","1Income",
        "10", "2. Facility Hiring","2Expenditure",
        "10", "3. Event Materials","2Expenditure",
        "10", "4. Advertisement","2Expenditure",
        "10", "5. Photocopy","2Expenditure",
        "10", "6. Photographs","2Expenditure",
        "10", "7. Catering","2Expenditure",
        "10", "8. Resource Persons","2Expenditure",
        "10", "9. Administrative Expenses","2Expenditure"};

      Global.mnFrm.cmCde.createSysLovs(sysLovs, sysLovsDynQrys, sysLovsDesc);
      Global.mnFrm.cmCde.createSysLovsPssblVals(sysLovs, pssblVals);

    }

    public static DataSet getStaticLOVValues(string lovNm)
    {
      string selSQL = @"SELECT a.pssbl_value, a.pssbl_value_desc, a.allowed_org_ids
  FROM gst.gen_stp_lov_values a, gst.gen_stp_lov_names b
  WHERE a.value_list_id = b.value_list_id and b.value_list_name = '" + lovNm.Replace("'", "''") + @"' 
  and a.is_enabled='1' and a.allowed_org_ids ilike '%," + Global.mnFrm.cmCde.Org_id + @",%' 
  ORDER BY a.pssbl_value";

      return Global.mnFrm.cmCde.selectDataNoParams(selSQL);
    }

    public static double getPointScored(int colNo, long recLineID)
    {
      string colNm = "point_scored" + colNo;

      string selSQL = @"SELECT " + colNm + @" 
   FROM attn.attn_attendance_recs a
  WHERE a.attnd_rec_id = " + recLineID;

      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(selSQL);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return double.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return 0;
    }

    public static void updtPointScored(int colNo, long recLineID, double pntval)
    {
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      string colNm = "point_scored" + colNo;

      string updtSQL = @"UPDATE attn.attn_attendance_recs SET " + colNm + @" 
  =" + pntval + @"  WHERE attnd_rec_id = " + recLineID;
      //Global.mnFrm.cmCde.showSQLNoPermsn(updtSQL);
      Global.mnFrm.cmCde.updateDataNoParams(updtSQL);

    }

    public static void refreshRqrdVrbls()
    {
      Global.mnFrm.cmCde.DefaultPrvldgs = Global.dfltPrvldgs;
      //Global.mnFrm.cmCde.Login_number = Global.myRpt.login_number;
      Global.mnFrm.cmCde.ModuleAdtTbl = Global.myEvnt.full_audit_trail_tbl_name;
      Global.mnFrm.cmCde.ModuleDesc = Global.myEvnt.mdl_description;
      Global.mnFrm.cmCde.ModuleName = Global.myEvnt.name;
      //Global.mnFrm.cmCde.pgSqlConn = Global.myRpt.Host.globalSQLConn;
      //Global.mnFrm.cmCde.Role_Set_IDs = Global.myRpt.role_set_id;
      Global.mnFrm.cmCde.SampleRole = "Events And Attendance Administrator";
      //Global.mnFrm.cmCde.User_id = Global.myRpt.user_id;
      //Global.mnFrm.cmCde.Org_id = Global.myRpt.org_id;
      Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
      Global.myEvnt.user_id = Global.mnFrm.usr_id;
      Global.myEvnt.login_number = Global.mnFrm.lgn_num;
      Global.myEvnt.role_set_id = Global.mnFrm.role_st_id;
      Global.myEvnt.org_id = Global.mnFrm.Og_id;

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
    #endregion
  }
}
