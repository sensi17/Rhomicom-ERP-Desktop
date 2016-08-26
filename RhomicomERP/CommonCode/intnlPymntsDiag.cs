using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CommonCode
{
  public partial class intnlPymntsDiag : Form
  {
    public intnlPymntsDiag()
    {
      InitializeComponent();
    }
    //Past Payments Panel Variables;
    public string[] dfltPrvldgs = { "View Internal Payments", 
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
    /*37*/"View Global Values", "Add Global Values","Edit Global Values","Delete Global Values"};

    string vwInfcSQLStmnt = "";
    long pst_cur_indx = 0;
    bool is_last_pst = false;
    long totl_pst = 0;
    long last_pst_num = 0;
    public string pst_SQL = "";
    bool obey_pst_evnts = false;
    bool addMnlPys = false;
    bool sndMnlPy = false;
    bool rvrsMnlPys = false;

    public CommonCodes cmnCde = new CommonCodes();
    public long invcHdrID = -1;
    int pyMthdID = -1;
    long cstmrID = -1;
    long cstmrSiteID = -1;
    public long prsnID = -1;
    public long payItmID = -1;
    public string trnsDte = "";

    public long payTrnsID = -1;
    public double amntToPay = 0;
    string itmMinType = "";
    string itmName = "";
    string itmMajType = "";
    string prsnNameNo = "";
    string itmUom = "";
    long payItmValID = -1;
    int curid = -1;
    string curCode = "";

    private void intnlPymntsDiag_Load(object sender, EventArgs e)
    {
      cmnCde.DefaultPrvldgs = this.dfltPrvldgs;
      Color[] clrs = cmnCde.getColors();
      this.BackColor = clrs[0];
      this.curid = int.Parse(cmnCde.getGnrlRecNm("scm.scm_sales_invc_hdr", "invc_hdr_id", "invc_curr_id", this.invcHdrID));

      this.pyMthdID = int.Parse(cmnCde.getGnrlRecNm("scm.scm_sales_invc_hdr", "invc_hdr_id", "pymny_method_id", this.invcHdrID));
      this.cstmrID = long.Parse(cmnCde.getGnrlRecNm("scm.scm_sales_invc_hdr", "invc_hdr_id", "customer_id", this.invcHdrID));
      this.cstmrSiteID = long.Parse(cmnCde.getGnrlRecNm("scm.scm_sales_invc_hdr", "invc_hdr_id", "customer_site_id", this.invcHdrID));

      this.curCode = cmnCde.getPssblValNm(this.curid);
      this.itmUom = cmnCde.getGnrlRecNm("org.org_pay_items", "item_id", "item_value_uom", this.payItmID);
      this.itmMajType = cmnCde.getItmMajType(this.payItmID);//cmnCde.getGnrlRecNm("org.org_pay_items", "item_id", "item_min_type", this.payItmID);
      this.itmMinType = cmnCde.getItmMinType(this.payItmID);//cmnCde.getGnrlRecNm("org.org_pay_items", "item_id", "item_min_type", this.payItmID);
      this.itmName = cmnCde.getItmName(this.payItmID);
      this.prsnNameNo = cmnCde.getPrsnSurNameFrst(this.prsnID) + " (" + cmnCde.getPrsnLocID(this.prsnID) + ")";
      this.paymntDateTextBox.Text = this.trnsDte;
      this.glDateTextBox.Text = DateTime.ParseExact(cmnCde.getGnrlRecNm("scm.scm_sales_invc_hdr", "invc_hdr_id", "invc_date", this.invcHdrID) + " 00:00:00",
        "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
      this.payItmValID = this.getPrsnItmVlID(this.prsnID, this.payItmID, this.trnsDte);
      this.populateTodyPymnts();
      this.loadPstPayPanel();
      this.amntNumericUpDown.Value = (decimal)this.amntToPay;
      System.Windows.Forms.Application.DoEvents();
      this.amntNumericUpDown.Focus();
      System.Windows.Forms.Application.DoEvents();
      this.amntNumericUpDown.Select(0, this.amntNumericUpDown.Value.ToString().Length);
      this.timer1.Interval = 200;
      this.timer1.Enabled = true;
    }

    private void nwPymntTxtBx_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
        || (e.Control && e.KeyCode == Keys.S))
      {
        this.processPayButton_Click(this.processPayButton, ex);
      }
    }

    #region "GL INTERFACE..."
    public long get_ScmRcvblsDocHdrID(long srchdrID, string srcHdrType, int orgID)
    {
      string strSql = "";

      strSql = @"SELECT rcvbls_invc_hdr_id
  FROM accb.accb_rcvbls_invc_hdr a " +
        "WHERE((a.src_doc_hdr_id = " + srchdrID +
        " and a.src_doc_type='" + srcHdrType.Replace("'", "''") + "' and a.org_id=" + orgID + "))";

      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return -1;
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

    public DataSet getItmVal1(long itmid)
    {
      string selSQL = "SELECT pssbl_value_id, pssbl_value_code_name, pssbl_amount, pssbl_value_sql, item_id " +
      "FROM org.org_pay_items_values WHERE ((item_id = " + itmid + ")) ORDER BY pssbl_value_id DESC";
      DataSet dtst = cmnCde.selectDataNoParams(selSQL);
      return dtst;
    }

    public DataSet getAllItmFeeds1(long itmid)
    {
      string selSQL = "SELECT a.balance_item_id, a.adds_subtracts, b.balance_type, a.scale_factor, c.pssbl_value_id " +
      "FROM org.org_pay_itm_feeds a LEFT OUTER JOIN org.org_pay_items b " +
      "ON a.balance_item_id = b.item_id LEFT OUTER JOIN org.org_pay_items_values c " +
      "ON c.item_id = a.balance_item_id WHERE ((a.fed_by_itm_id = " + itmid +
      ")) ORDER BY a.feed_id ";
      //cmnCde.showSQLNoPermsn(selSQL);
      DataSet dtst = cmnCde.selectDataNoParams(selSQL);
      return dtst;
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

    public void updateBnftsPrs(long prsnid, long rowid, long itm_val_id,
  string strtdte, string enddte)
    {
      cmnCde.Extra_Adt_Trl_Info = "";
      string dateStr = cmnCde.getDB_Date_time();
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
               cmnCde.User_id + ", last_update_date='" + dateStr + "' " +
       "WHERE row_id=" + rowid;
      cmnCde.updateDataNoParams(updtSQL);
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

    public double willItmBlsBeNgtv(long prsn_id, long itm_id,
      double pay_amount, string trns_date)
    {
      DataSet dtst = this.getAllItmFeeds1(itm_id);
      double nwAmnt = 0;
      for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
      {
        //if (this.doesPrsnHvItm(prsn_id, long.Parse(dtst.Tables[0].Rows[a][0].ToString()), trns_date) == false)
        //{
        //  string tstDte = "";
        //  this.doesPrsnHvItm(prsn_id, itm_id, trns_date, ref tstDte);
        //  if (tstDte == "")
        //  {
        //    tstDte = "01-Jan-1900 00:00:00";
        //  }
        //  this.createBnftsPrs(prsn_id,
        //    long.Parse(dtst.Tables[0].Rows[a][0].ToString())
        //      , long.Parse(dtst.Tables[0].Rows[a][4].ToString())
        //      , "01-" + tstDte.Substring(3, 8), "31-Dec-4000");
        //}
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
          //Global.createBnftsPrs(prsn_id,
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

    //public void updtMoneyBals(long moneybalid, double ttlpay, double ttlwthdrwl)
    //{
    //  string dateStr = cmnCde.getDB_Date_time();
    //  string updtSQL = "UPDATE pay.pay_prsn_money_bals " +
    //                                      "SET total_payments=total_payments + " + ttlpay +
    //                                      ", total_withdrawals = total_withdrawals + " + ttlwthdrwl +
    //  ", last_update_by=" + cmnCde.User_id + ", " +
    //  "last_update_date='" + dateStr + "' " +
    //                                      "WHERE money_bals_id = " + moneybalid;
    //  cmnCde.updateDataNoParams(updtSQL);
    //}

    public void updtGLIntrfcLnSpclOrg(int orgID)
    {
      //Used to update batch ids of interface lines that have gone to GL already
      string dateStr = cmnCde.getDB_Date_time();
      string updtSQL = "UPDATE pay.pay_gl_interface a " +
      "SET gl_batch_id = (select f.batch_id from accb.accb_trnsctn_details f, accb.accb_chart_of_accnts h " +
      "where f.batch_id IN (select g.batch_id from accb.accb_trnsctn_batches g " +
      "where g.batch_name ilike '%Internal Payments%' and " +
      "to_timestamp(g.creation_date,'YYYY-MM-DD HH24:MI:SS') between " +
      "(to_timestamp(a.trnsctn_date,'YYYY-MM-DD HH24:MI:SS') - interval '6 months') " +
      "and (to_timestamp(a.trnsctn_date,'YYYY-MM-DD HH24:MI:SS') + interval '6 months')) and " +
      "f.source_trns_ids like '%,' || a.interface_id || ',%' and " +
      "f.trnsctn_date=a.trnsctn_date and f.accnt_id= a.accnt_id and f.accnt_id= h.accnt_id and h.org_id = " + orgID + ")" +
      ", last_update_by=" + cmnCde.User_id + ", " +
      "last_update_date='" + dateStr + "' " +
      "WHERE a.gl_batch_id = -1 and EXISTS(select 1 from accb.accb_chart_of_accnts" +
      " m where a.accnt_id= m.accnt_id and m.org_id =" + orgID + ")";
      cmnCde.updateDataNoParams(updtSQL);
    }

    public void updtPymntAllGLIntrfcLnOrg(long glbatchid, int orgID)
    {
      string dateStr = cmnCde.getDB_Date_time();
      string updtSQL = "UPDATE pay.pay_gl_interface a " +
      "SET gl_batch_id = " + glbatchid +
      ", last_update_by=" + cmnCde.User_id + ", " +
      "last_update_date='" + dateStr + "' " +
      "WHERE a.gl_batch_id = -1 and EXISTS(select f.transctn_id from accb.accb_trnsctn_details f, accb.accb_chart_of_accnts g " +
      "where f.batch_id = " + glbatchid + " " +
      "and f.source_trns_ids like '%,' || a.interface_id || ',%' and " +
      "f.trnsctn_date=a.trnsctn_date and f.accnt_id= a.accnt_id and f.accnt_id= g.accnt_id and g.org_id = " + orgID + ") ";
      cmnCde.updateDataNoParams(updtSQL);
    }

    public void updtPymntMsPyGLIntrfcLn(long mspyid, long glbatchid)
    {
      string dateStr = cmnCde.getDB_Date_time();
      string updtSQL = "UPDATE pay.pay_gl_interface a " +
     "SET gl_batch_id = " + glbatchid +
      ", last_update_by=" + cmnCde.User_id + ", " +
      "last_update_date='" + dateStr + "' " +
      "WHERE a.gl_batch_id = -1 and a.source_trns_id IN " +
      "(select b.pay_trns_id from pay.pay_itm_trnsctns b where b.mass_pay_id = " +
      mspyid + ") and EXISTS(select f.transctn_id from accb.accb_trnsctn_details f " +
      "where f.batch_id = " + glbatchid + " " +
      "and f.source_trns_ids like '%,' || a.interface_id || ',%' and " +
      "f.trnsctn_date=a.trnsctn_date and f.accnt_id= a.accnt_id) ";
      cmnCde.updateDataNoParams(updtSQL);
    }

    public void updtPymntMnlGLIntrfcLn(long py_trns_id, long glbatchid)
    {
      string dateStr = cmnCde.getDB_Date_time();
      string updtSQL = "UPDATE pay.pay_gl_interface a " +
      "SET gl_batch_id = " + glbatchid +
      ", last_update_by=" + cmnCde.User_id + ", " +
      "last_update_date='" + dateStr + "' " +
      "WHERE a.gl_batch_id = -1 and a.source_trns_id IN " +
      "(select pay_trns_id from pay.pay_itm_trnsctns  where pay_trns_id = " +
      py_trns_id + ") and EXISTS(select f.transctn_id from accb.accb_trnsctn_details f " +
      "where f.batch_id = " + glbatchid + " " +
      "and f.source_trns_ids like '%,' || a.interface_id || ',%' and " +
      "f.trnsctn_date=a.trnsctn_date and f.accnt_id= a.accnt_id) ";
      cmnCde.updateDataNoParams(updtSQL);
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

    public long getSimlrPstdBatchID(string orgnlbatchname, int orgid)
    {
      long srcbatchid = this.getBatchID(orgnlbatchname, orgid);
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

    public DataSet get_WrongGLBatches(int orgID)
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

      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      return dtst;
    }

    public DataSet get_Batch_Trns_NoStatus(long batchID)
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

      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      return dtst;
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

    public void createTransaction(int accntid, string trnsDesc,
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
                        "accnt_id, transaction_desc, dbt_amount, trnsctn_date, " +
                        "func_cur_id, created_by, creation_date, batch_id, crdt_amount, " +
                        @"last_update_by, last_update_date, net_amount, 
            entered_amnt, entered_amt_crncy_id, accnt_crncy_amnt, accnt_crncy_id, 
            func_cur_exchng_rate, accnt_cur_exchng_rate, dbt_or_crdt) " +
                        "VALUES (" + accntid + ", '" + trnsDesc.Replace("'", "''") + "', " + dbtAmnt +
                        ", '" + trnsDate + "', " + crncyid + ", " + cmnCde.User_id + ", '" + dateStr +
                        "', " + batchid + ", " + crdtamnt + ", " + cmnCde.User_id +
                        ", '" + dateStr + "'," + netAmnt + ", " + entrdAmt +
                        ", " + entrdCurrID + ", " + acntAmnt +
                        ", " + acntCurrID + ", " + funcExchRate +
                        ", " + acntExchRate + ", '" + dbtOrCrdt + "')";
      cmnCde.insertDataNoParams(insSQL);
    }

    public void updtBatchTrnsSrcIDs(long batchID)
    {
      cmnCde.Extra_Adt_Trl_Info = "";
      string updtSQL = "UPDATE accb.accb_trnsctn_details SET source_trns_ids='' WHERE batch_id=" + batchID;
      cmnCde.updateDataNoParams(updtSQL);
    }

    public void updtIntrfcTrnsSrcBatchIDs(long batchID)
    {
      cmnCde.Extra_Adt_Trl_Info = "";
      string updtSQL = "UPDATE pay.pay_gl_interface SET gl_batch_id=-1 WHERE gl_batch_id=" + batchID;
      cmnCde.updateDataNoParams(updtSQL);
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

    public bool isGLIntrfcBlcdOrg(int orgID, ref double dffrce)
    {
      string strSql = @"SELECT COALESCE(SUM(a.dbt_amount),0) dbt_sum, 
COALESCE(SUM(a.crdt_amount),0) crdt_sum " +
   "FROM pay.pay_gl_interface a, accb.accb_chart_of_accnts b " +
   "WHERE a.gl_batch_id = -1 and a.accnt_id = b.accnt_id and b.org_id=" + orgID +
      " ";

      DataSet dtst = cmnCde.selectDataNoParams(strSql);
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

    public bool isGLIntrfcBlcd(long py_trns_id)
    {
      string strSql = "SELECT SUM(a.dbt_amount) dbt_sum, " +
      "SUM(a.crdt_amount) crdt_sum " +
      "FROM pay.pay_gl_interface a " +
      "WHERE a.gl_batch_id = -1 and a.source_trns_id IN " +
      "(select b.pay_trns_id from pay.pay_itm_trnsctns b where b.pay_trns_id = " + py_trns_id + ")";

      DataSet dtst = cmnCde.selectDataNoParams(strSql);
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

    public bool isMsPyGLIntrfcBlcd(long mspyid)
    {
      string strSql = "SELECT SUM(a.dbt_amount) dbt_sum, " +
      "SUM(a.crdt_amount) crdt_sum " +
      "FROM pay.pay_gl_interface a " +
      "WHERE a.gl_batch_id = -1 and a.source_trns_id IN " +
      "(select b.pay_trns_id from pay.pay_itm_trnsctns b where b.mass_pay_id = " + mspyid + ")";

      DataSet dtst = cmnCde.selectDataNoParams(strSql);
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

    //public string getIntfcTrnsGlBtchID()
    //{
    //  string strSQL = " and EXISTS(select f.transctn_id from accb.accb_trnsctn_details f " +
    //  "where f.batch_id = " + glbatchid + " " +
    //  "and f.source_trns_ids like '%,' || a.interface_id || ',%' and " +
    //  "f.trnsctn_date=a.trnsctn_date and f.accnt_id= a.accnt_id)";
    //}

    public DataSet getAllInGLIntrfcOrg(int orgID)
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
      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      return dtst;
    }

    public DataSet getAllInGLIntrfc(long py_trns_id)
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

      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      return dtst;
    }

    public DataSet getAllInMsPyGLIntrfc(long mspyid)
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

      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      return dtst;
    }

    public string getGLIntrfcIDs(int accntid, string trns_date, int crncy_id)
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
      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      string infc_ids = ",";
      for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
      {
        infc_ids = infc_ids + dtst.Tables[0].Rows[a][0].ToString() + ",";
      }
      return infc_ids;
    }

    public int get_Suspns_Accnt(int orgid)
    {
      string strSql = "";
      strSql = "SELECT a.accnt_id " +
        "FROM accb.accb_chart_of_accnts a " +
        "WHERE(a.is_suspens_accnt = '1' and a.org_id = " + orgid + ")";
      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count == 1)
      {
        return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return -1;
      }
    }

    //public string dbtOrCrdtAccnt(int accntid, string incrsDcrse)
    //{
    //  string accntType = cmnCde.getAccntType(accntid);
    //  string isContra = cmnCde.isAccntContra(accntid);
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

    //public int dbtOrCrdtAccntMultiplier(int accntid, string incrsDcrse)
    //{
    //  string accntType = cmnCde.getAccntType(accntid);
    //  string isContra = cmnCde.isAccntContra(accntid);
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

    public bool isGLIntrfcBlcdOrg(int orgID)
    {
      string strSql = @"SELECT COALESCE(SUM(a.dbt_amount),0) dbt_sum, 
COALESCE(SUM(a.crdt_amount),0) crdt_sum 
FROM pay.pay_gl_interface a, accb.accb_chart_of_accnts b 
WHERE a.gl_batch_id = -1 and a.accnt_id = b.accnt_id and b.org_id=" + orgID +
      " ";

      DataSet dtst = cmnCde.selectDataNoParams(strSql);
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

    public void deleteGLInfcLine(long intfcID)
    {
      cmnCde.Extra_Adt_Trl_Info = "";
      string delSQL = "DELETE FROM pay.pay_gl_interface WHERE interface_id = " +
        intfcID + " and gl_batch_id = -1";
      cmnCde.deleteDataNoParams(delSQL);
    }

    public double[] getGLIntrfcIDAmntSum(string intrfcids, int accntID)
    {
      double[] res = { 0, 0 };
      string strSql = @"SELECT COALESCE(SUM(a.dbt_amount),0), COALESCE(SUM(a.crdt_amount),0)
FROM pay.pay_gl_interface a
WHERE (a.accnt_id = " + accntID + @"
and '" + intrfcids + "' like '%,' || a.interface_id || ',%') ";

      DataSet dtst = cmnCde.selectDataNoParams(strSql);

      if (dtst.Tables[0].Rows.Count > 0)
      {
        res[0] = double.Parse(dtst.Tables[0].Rows[0][0].ToString());
        res[1] = double.Parse(dtst.Tables[0].Rows[0][1].ToString());
      }
      return res;
    }

    public string getGLIntrfcIDsMnl(int accntid, string trns_date, int crncy_id, long py_trns_id)
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

      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      string infc_ids = ",";
      for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
      {
        infc_ids = infc_ids + dtst.Tables[0].Rows[a][0].ToString() + ",";
      }
      return infc_ids;
    }

    public string getGLIntrfcIDsMsPy(int accntid, string trns_date, int crncy_id, long mspyid)
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

      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      string infc_ids = ",";
      for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
      {
        infc_ids = infc_ids + dtst.Tables[0].Rows[a][0].ToString() + ",";
      }
      return infc_ids;
    }

    public DataSet getDocGLInfcLns(long intrfcID)
    {
      string strSql = "SELECT * FROM pay.pay_gl_interface WHERE interface_id = " +
        intrfcID + "  and gl_batch_id != -1";
      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      return dtst;
    }

    public DataSet getPymntGLInfcLns(long pyTrnsID)
    {
      string strSql = "SELECT * FROM pay.pay_gl_interface WHERE source_trns_id = " +
        pyTrnsID + "  and gl_batch_id != -1";
      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      return dtst;
    }
    public void updtActnPrcss(int prcsID)
    {
      cmnCde.Extra_Adt_Trl_Info = "";
      cmnCde.ignorAdtTrail = true;
      string dtestr = cmnCde.getDB_Date_time();
      string strSql = @"UPDATE accb.accb_running_prcses SET
            last_active_time='" + dtestr + "' " +
            "WHERE which_process_is_rnng = " + prcsID + " ";
      cmnCde.updateDataNoParams(strSql);
      cmnCde.ignorAdtTrail = false;
    }

    public bool isThereANActvActnPrcss(string prcsIDs, string prcsIntrvl)
    {
      string strSql = @"SELECT age(now(), to_timestamp(last_active_time,'YYYY-MM-DD HH24:MI:SS')) <= interval '" + prcsIntrvl +
        "' FROM accb.accb_running_prcses WHERE which_process_is_rnng IN (" + prcsIDs + ")";

      //cmnCde.showMsg(strSql, 0);
      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return bool.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return false;
    }

    public DataSet get_Infc_Trns(string searchWord, string searchIn,
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

      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      this.vwInfcSQLStmnt = strSql;
      return dtst;
    }

    public long get_Total_Infc(string searchWord, string searchIn,
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

      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      long sumRes = 0;
      if (dtst.Tables[0].Rows.Count > 0)
      {
        long.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out sumRes);
      }
      return sumRes;
    }

    #endregion

    #region "PAST PAYMENTS..."
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

    public void createPaymntLine(long prsnid, long itmid, double amnt, string paydate,
    string paysource, string trnsType, long msspyid, string paydesc, int crncyid, string dateStr,
      string pymt_vldty, long src_trns_id, string glDate)
    {
      paydate = DateTime.ParseExact(
   paydate, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      dateStr = DateTime.ParseExact(
   dateStr, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      string insSQL = "INSERT INTO pay.pay_itm_trnsctns(" +
               "person_id, item_id, amount_paid, paymnt_date, paymnt_source, " +
               "pay_trns_type, created_by, creation_date, last_update_by, last_update_date, " +
               "mass_pay_id, pymnt_desc, crncy_id, pymnt_vldty_status, src_py_trns_id, gl_date) " +
       "VALUES (" + prsnid + ", " + itmid + ", " + amnt +
       ", '" + paydate.Replace("'", "''") + "', '" + paysource.Replace("'", "''") +
       "', '" + trnsType.Replace("'", "''") + "', " + cmnCde.User_id + ", '" + dateStr + "', " +
               cmnCde.User_id + ", '" + dateStr + "', " + msspyid +
               ", '" + paydesc.Replace("'", "''") + "', " + crncyid +
               ", '" + pymt_vldty.Replace("'", "''") + "', " + src_trns_id + ", '" + glDate + "')";
      cmnCde.insertDataNoParams(insSQL);
    }

    public void createTodaysGLBatch(int orgid, string batchnm,
    string batchdesc, string batchsource)
    {
      string dateStr = cmnCde.getDB_Date_time();
      string insSQL = "INSERT INTO accb.accb_trnsctn_batches(" +
               "batch_name, batch_description, created_by, creation_date, " +
               "org_id, batch_status, last_update_by, last_update_date, batch_source, avlbl_for_postng) " +
       "VALUES ('" + batchnm.Replace("'", "''") + "', '" + batchdesc.Replace("'", "''") +
       "', " + cmnCde.User_id + ", '" + dateStr + "', " + orgid + ", '0', " +
               cmnCde.User_id + ", '" + dateStr + "', '" +
               batchsource.Replace("'", "''") + "', '0')";
      cmnCde.insertDataNoParams(insSQL);
    }

    public void updtTodaysGLBatchPstngAvlblty(long batchid, string avlblty)
    {
      string dateStr = cmnCde.getDB_Date_time();
      cmnCde.Extra_Adt_Trl_Info = "";
      string insSQL = "UPDATE accb.accb_trnsctn_batches SET avlbl_for_postng='" + avlblty +
        "', last_update_by=" + cmnCde.User_id +
        ", last_update_date='" + dateStr +
        "' WHERE batch_id = " + batchid;
      cmnCde.updateDataNoParams(insSQL);
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
      return Math.Round(sumRes, 2);
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
      return Math.Round(sumRes, 2);
    }

    public void createScmGLIntFcLn(int accntid, string trnsdesc, double dbtamnt,
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
               ", '" + trnsdte.Replace("'", "''") + "', " + crncyid + ", " + cmnCde.User_id +
               ", '" + dateStr + "', " + crdtamnt + ", " +
               cmnCde.User_id + ", '" + dateStr + "', " + netamnt +
               ", -1, '" + srcDocTyp.Replace("'", "''") + "', " +
               srcDocID + ", " + srcDocLnID + ", '" + trnsSrc + "')";
      cmnCde.insertDataNoParams(insSQL);
    }

    public void deleteBrknDocGLInfcLns()
    {
      cmnCde.Extra_Adt_Trl_Info = "";
      string delSQL = @"DELETE FROM pay.pay_gl_interface 
WHERE scm.get_src_doc_num(src_doc_id,src_doc_typ) IS NULL 
or scm.get_src_doc_num(src_doc_id, src_doc_typ)=''";
      cmnCde.deleteDataNoParams(delSQL);
    }

    public void createPayGLIntFcLn(int accntid, string trnsdesc, double dbtamnt,
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
               ", '" + trnsdte.Replace("'", "''") + "', " + crncyid + ", " + cmnCde.User_id +
               ", '" + dateStr + "', " + crdtamnt + ", " +
               cmnCde.User_id + ", '" + dateStr + "', " + netamnt +
               ", -1, '" + trnsSrc + "')";
      cmnCde.insertDataNoParams(insSQL);
    }

    public long getIntrfcTrnsID(string intrfcTblNm, int accntID, double netAmnt, string trnsDte)
    {
      string selSQL = @"SELECT interface_id 
  FROM " + intrfcTblNm + " WHERE accnt_id=" + accntID + " and net_amount=" + netAmnt +
         " and trnsctn_date = '" + trnsDte + "'";
      DataSet dtst = cmnCde.selectDataNoParams(selSQL);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return -1;
    }

    public DataSet get_Intrfc_dateSums(string intrfcTblNm, int orgID)
    {
      string updtSQL = @"UPDATE " + intrfcTblNm + @" SET dbt_amount = round(dbt_amount,2),
    crdt_amount = round(dbt_amount,2), net_amount = round(net_amount,2)
    WHERE round(crdt_amount - round(crdt_amount,2))!=0 or round(dbt_amount - round(dbt_amount,2))!=0";
      cmnCde.updateDataNoParams(updtSQL);

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
      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      //this.mnFrm.trnsDet_SQL = strSql;
      return dtst;
    }

    public void deleteBatch(long batchid, string batchNm)
    {
      cmnCde.Extra_Adt_Trl_Info = "Batch Name = " + batchNm;
      string delSql = "DELETE FROM accb.accb_trnsctn_batches WHERE(batch_id = " + batchid + ")";
      cmnCde.deleteDataNoParams(delSql);
    }

    public void deleteBatchTrns(long batchid)
    {
      cmnCde.Extra_Adt_Trl_Info = "";
      string delSql = "DELETE FROM accb.accb_trnsctn_details WHERE(batch_id = " + batchid + ")";
      cmnCde.deleteDataNoParams(delSql);
    }

    public double get_LtstExchRate(int fromCurrID, int toCurrID, string asAtDte)
    {
      int fnccurid = cmnCde.getOrgFuncCurID(cmnCde.Org_id);
      //this.curCode = cmnCde.getPssblValNm(this.curid);

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
      if (fromCurrID == toCurrID)
      {
        return 1;
      }
      else if (fromCurrID != fnccurid && toCurrID != fnccurid)
      {
        double a = this.get_LtstExchRate(fromCurrID, fnccurid, asAtDte);
        double b = this.get_LtstExchRate(toCurrID, fnccurid, asAtDte);
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

    public void createPymntGLLine(int accntid, string trnsdesc, double dbtamnt,
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
               ", '" + trnsdte.Replace("'", "''") + "', " + crncyid + ", " + cmnCde.User_id +
               ", '" + dateStr + "', " + batchid + ", " + crdtamnt + ", " +
               cmnCde.User_id + ", '" + dateStr + "', " + netamnt +
               ", '0', '" + srcids + "', " + entrdAmt +
                        ", " + entrdCurrID + ", " + acntAmnt +
                        ", " + acntCurrID + ", " + funcExchRate +
                        ", " + acntExchRate + ", '" + dbtOrCrdt + "')";
      cmnCde.insertDataNoParams(insSQL);
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

    public void createPymntGLIntFcLn(int accntid, string trnsdesc, double dbtamnt,
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
               ", '" + trnsdte.Replace("'", "''") + "', " + crncyid + ", " + cmnCde.User_id +
               ", '" + dateStr + "', " + crdtamnt + ", " +
               cmnCde.User_id + ", '" + dateStr + "', " + netamnt +
               ", -1, " + srcid + ", '" + trnsSrc + "')";
      cmnCde.insertDataNoParams(insSQL);
    }

    public void createMoneyBal(long prsnid, double ttlpay, double ttlwthdrwl)
    {
      string dateStr = cmnCde.getDB_Date_time();
      string insSQL = "INSERT INTO pay.pay_prsn_money_bals(" +
      "person_id, total_payments, created_by, creation_date, " +
      "last_update_by, last_update_date, total_withdrawals) " +
          "VALUES (" + prsnid + ", " + ttlpay + ", " + cmnCde.User_id +
          ", '" + dateStr + "', " + cmnCde.User_id + ", '" +
          dateStr + "', " + ttlwthdrwl + ")";
      cmnCde.insertDataNoParams(insSQL);
    }

    public string get_GLBatch_Nm(long batchID)
    {
      string strSql = "";
      strSql = "SELECT a.batch_name " +
     "FROM accb.accb_trnsctn_batches a " +
     "WHERE(a.batch_id = " + batchID + ")";

      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return dtst.Tables[0].Rows[0][0].ToString();
      }
      else
      {
        return "";
      }
    }

    public long getTodaysGLBatchID(string batchnm, int orgid)
    {
      string strSql = "";
      strSql = "SELECT a.batch_id " +
     "FROM accb.accb_trnsctn_batches a " +
     "WHERE(a.batch_name ilike '%" + batchnm.Replace("'", "''") +
     "%' and org_id = " + orgid + " and batch_status = '0')";

      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return -1;
      }
    }

    public string get_InvItemNm(int itmID)
    {
      string strSql = "SELECT REPLACE(item_desc || ' (' || REPLACE(item_code,item_desc,'') || ')', ' ()','') " +
   "FROM inv.inv_itm_list a " +
   "WHERE item_id =" + itmID + "";

      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return dtst.Tables[0].Rows[0][0].ToString();
      }
      return "";
    }

    public string get_PayItemNm(int itmID)
    {
      string strSql = "SELECT item_code_name " +
   "FROM org.org_pay_items a " +
   "WHERE item_id =" + itmID + "";

      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return dtst.Tables[0].Rows[0][0].ToString();
      }
      return "";
    }

    public string[] get_ItmAccntInfo(long itmID)
    {
      string[] retSql = { "Q", "-123", "Q", "-123" };
      string strSql = "SELECT a.incrs_dcrs_cost_acnt, a.cost_accnt_id, a.incrs_dcrs_bals_acnt, a.bals_accnt_id " +
   "FROM org.org_pay_items a " +
   "WHERE(a.item_id = " + itmID + ")";

      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        retSql[0] = dtst.Tables[0].Rows[0][0].ToString();
        retSql[1] = dtst.Tables[0].Rows[0][1].ToString();
        retSql[2] = dtst.Tables[0].Rows[0][2].ToString();
        retSql[3] = dtst.Tables[0].Rows[0][3].ToString();
      }
      return retSql;
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

    public DataSet getPstPayDet(long paytrnsid)
    {
      string strSql = @"SELECT a.amount_paid, 
to_char(to_timestamp(a.paymnt_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS')
, a.pay_trns_type, a.crncy_id, a.pymnt_desc " +
       "FROM pay.pay_itm_trnsctns a " +
       "WHERE ((a.pay_trns_id = " + paytrnsid + "))";

      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      return dtst;
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
          "' and pymnt_vldty_status='VALID' and src_py_trns_id=" + orgnlTrnsID + ")";
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

    public DataSet get_Basic_Pst(string searchWord, string searchIn,
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
      this.pst_SQL = strSql;
      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      return dtst;
    }

    public long get_Total_Pst(string searchWord, string searchIn, long prsnID, long itmID)
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

    public DataSet get_Basic_PstBls(string searchWord, string searchIn,
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
      this.pst_SQL = strSql;
      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      return dtst;
    }

    public long get_Total_PstBls(string searchWord, string searchIn, long prsnID, long itmID)
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

    public string get_Pst_Rec_Hstry(long trnsID)
    {
      string strSQL = @"SELECT a.created_by, 
      to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS'), 
      a.last_update_by, 
      to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS') " +
      "FROM pay.pay_itm_trnsctns a WHERE(a.pay_trns_id = " + trnsID + ")";
      string fnl_str = "";
      DataSet dtst = cmnCde.selectDataNoParams(strSQL);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        fnl_str = "CREATED BY: " + cmnCde.get_user_name(long.Parse(dtst.Tables[0].Rows[0][0].ToString())) +
         "\r\nCREATION DATE: " + dtst.Tables[0].Rows[0][1].ToString() + "\r\nLAST UPDATE BY:" +
         cmnCde.get_user_name(long.Parse(dtst.Tables[0].Rows[0][2].ToString())) +
         "\r\nLAST UPDATE DATE: " + dtst.Tables[0].Rows[0][3].ToString();
        return fnl_str;
      }
      else
      {
        return "";
      }
    }

    public decimal get_ttl_paymnts(long prsnID, long itmID, string whopays)
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
      DataSet dtst = cmnCde.selectDataNoParams(strSql);
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

    public decimal get_ttl_withdrwls(long prsnID, long itmID)
    {
      /*string strSql = "Select SUM(a.amount_paid) FROM pay.pay_itm_trnsctns a where a.person_id = " +
       prsnID + " and a.item_id = " + itmID + " and a.pay_trns_type like '%Withdrawal%'";*/
      string strSql = "Select ttl_amnt_wthdrwn FROM pasn.prsn_bnfts_cntrbtns a where a.person_id = " +
   prsnID + " and a.item_id = " + itmID + "";

      DataSet dtst = cmnCde.selectDataNoParams(strSql);
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

    public string getPymntTyp(long py_trns_id)
    {
      string strSql = "SELECT a.paymnt_source FROM pay.pay_itm_trnsctns a WHERE a.pay_trns_id = " + py_trns_id;
      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return dtst.Tables[0].Rows[0][0].ToString();
      }
      return "";
    }

    public bool hsMsPyBnRun(long mspyid)
    {
      string strSql = "SELECT a.run_status FROM pay.pay_mass_pay_run_hdr a WHERE a.mass_pay_id = " + mspyid;
      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return cmnCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[0][0].ToString());
      }
      return false;
    }

    public bool hsMsPyGoneToGL(long mspyid)
    {
      string strSql = "SELECT a.sent_to_gl FROM pay.pay_mass_pay_run_hdr a WHERE a.mass_pay_id = " + mspyid;
      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return cmnCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[0][0].ToString());
      }
      return false;
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

    public void deletePymntGLInfcLns(long pyTrnsID)
    {
      cmnCde.Extra_Adt_Trl_Info = "";
      string delSQL = "DELETE FROM pay.pay_gl_interface WHERE source_trns_id = " +
        pyTrnsID + " and gl_batch_id = -1";
      cmnCde.deleteDataNoParams(delSQL);
    }

    public long getIntFcTrnsDbtLn(long pytrnsid, double pay_amnt)
    {
      string strSql = "SELECT a.interface_id FROM pay.pay_gl_interface a " +
              "WHERE a.source_trns_id = " + pytrnsid +
        " and a.dbt_amount = " + pay_amnt + " ";
      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return -1;
    }

    public long getIntFcTrnsCrdtLn(long pytrnsid, double pay_amnt)
    {
      string strSql = "SELECT a.interface_id FROM pay.pay_gl_interface a " +
              "WHERE a.source_trns_id = " + pytrnsid +
        " and a.crdt_amount = " + pay_amnt + " ";
      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return -1;
    }

    public double getMsPyAmntSum(long mspyid)
    {
      string strSql = "SELECT SUM(a.amount_paid) FROM pay.pay_itm_trnsctns a " +
        "WHERE a.pay_trns_type !='Purely Informational' and a.crncy_id > 0 and a.mass_pay_id = " + mspyid;
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

    public long getFirstItmValID(long itmID)
    {
      string strSql = @"Select a.pssbl_value_id FROM org.org_pay_items_values a 
      where((a.item_id = " + itmID + ")) ORDER BY 1 LIMIT 1 OFFSET 0";
      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return -1;
    }

    public bool doesPrsnHvItm(long prsnID, long itmID, string dateStr)
    {
      dateStr = DateTime.ParseExact(
   dateStr, "dd-MMM-yyyy HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
      string strSql = "Select a.row_id FROM pasn.prsn_bnfts_cntrbtns a where((a.person_id = " +
    prsnID + ") and (a.item_id = " + itmID + ") and (to_timestamp('" + dateStr + "'," +
    "'YYYY-MM-DD HH24:MI:SS') between to_timestamp(valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
            "AND to_timestamp(valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS')))";
      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return true;
      }
      return false;
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

    public bool hsPrsnBnPaidItmMnl(long prsnID, long itmID,
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
      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return true;
      }
      return false;
    }

    public bool doesPymntDteViolateFreq(long prsnID, long itmID,
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

      string pyFreq = cmnCde.getGnrlRecNm("org.org_pay_items", "item_id", "pay_frequency", itmID);
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
      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      //cmnCde.showSQLNoPermsn(pyFreq + "/" + strSql);
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

    public bool doesItmStHvItm(long hdrID, long itmID)
    {
      string strSql = "Select a.det_id FROM pay.pay_itm_sets_det a where((a.hdr_id = " +
    hdrID + ") and (a.item_id = " + itmID + "))";
      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return true;
      }
      return false;
    }

    public bool doesPrsStHvPrs(long hdrID, long prsnID)
    {
      string strSql = "Select a.prsn_set_det_id FROM pay.pay_prsn_sets_det a where((a.prsn_set_hdr_id = " +
    hdrID + ") and (a.person_id = " + prsnID + "))";
      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return true;
      }
      return false;
    }
    #endregion

    #region "Past Payments..."
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

    private void populateTodyPymnts()
    {
      /*Payment by Organisation
        Payment by Person
        Withdrawal by Person*/
      this.trnsTypComboBox.Enabled = true;
      this.amntNumericUpDown.Enabled = true;
      this.amntNumericUpDown.Value = 0;
      this.paymntDescTextBox.Enabled = true;
      this.paymntDescTextBox.Text = "";
      this.paymntDateButton.Enabled = true;
      this.paymntDateTextBox.Enabled = true;
      //this.paymntDateTextBox.Text = "";
      string dateStr = DateTime.ParseExact(
   cmnCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
      //this.glDateTextBox.Text = dateStr;
      if (this.itmMinType == "Earnings"
        || this.itmMinType == "Employer Charges")
      {
        this.trnsTypComboBox.Items.Clear();
        this.trnsTypComboBox.Items.Add("Payment by Organisation");
        this.trnsTypComboBox.SelectedIndex = 0;
        this.paymntDescTextBox.Text = "Payment of " +
    this.itmName +
    " for " + this.prsnNameNo;
      }
      else if (this.itmMinType == "Bills/Charges"
        || this.itmMinType == "Deductions"
        || this.itmMinType == "Deductions"
          || this.itmMinType == "Deductions")
      {
        this.trnsTypComboBox.Items.Clear();
        this.trnsTypComboBox.Items.Add("Payment by Person");
        this.trnsTypComboBox.SelectedIndex = 0;
        this.paymntDescTextBox.Text = "Payment of " +
    this.itmName +
    " by " + this.prsnNameNo;
      }
      else if (this.itmMajType.ToUpper() == "Balance Item".ToUpper())
      {
        this.trnsTypComboBox.Items.Clear();
      }
      else
      {
        this.trnsTypComboBox.Items.Clear();
        this.trnsTypComboBox.Items.Add("Purely Informational");
        this.trnsTypComboBox.SelectedIndex = 0;
        this.paymntDescTextBox.Text = "Running of Purely Informational Item " +
    this.itmName +
    " for " + this.prsnNameNo;
      }
      string valSQL = cmnCde.getItmValSQL(this.payItmValID);
      if (valSQL == "")
      {
        this.expctdAmntTextBox.Text = cmnCde.getItmValueAmnt(this.payItmValID).ToString("#,##0.00");
      }
      else
      {
        this.expctdAmntTextBox.Text = cmnCde.exctItmValSQL(
          valSQL, this.prsnID,
          cmnCde.Org_id, dateStr).ToString("#,##0.00");
      }
      if (this.itmUom == "Money")
      {
        this.crncyIDTextBox.Text = this.curid.ToString();//cmnCde.getOrgFuncCurID(cmnCde.Org_id).ToString();
        this.crncyTextBox.Text = this.curCode;//cmnCde.getPssblValNm(this.curid);
      }
      else
      {
        this.crncyIDTextBox.Text = "-1";
        this.crncyTextBox.Text = this.itmUom;
      }
    }

    private void loadPstPayPanel()
    {
      this.obey_pst_evnts = false;
      if (this.searchInPstComboBox.SelectedIndex < 0)
      {
        this.searchInPstComboBox.SelectedIndex = 0;
      }
      if (this.searchForPstTextBox.Text.Contains("%") == false)
      {
        this.searchForPstTextBox.Text = "%" + this.searchForPstTextBox.Text.Replace(" ", "%") + "%";
      }
      if (this.searchForPstTextBox.Text == "%%")
      {
        this.searchForPstTextBox.Text = "%";
      }
      int dsply = 0;
      if (this.dsplySizePstComboBox.Text == ""
        || int.TryParse(this.dsplySizePstComboBox.Text, out dsply) == false)
      {
        this.dsplySizePstComboBox.Text = cmnCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      }
      //this.groupBox8.Height = this.g.Bottom - this.toolStrip4.Bottom - 50;
      this.is_last_pst = false;
      this.totl_pst = cmnCde.Big_Val;
      this.getPstPnlData();
      this.obey_pst_evnts = true;
    }

    private void getPstPnlData()
    {
      this.updtPstTotals();
      this.populatePstListVw();
      this.updtPstNavLabels();
    }

    private void updtPstTotals()
    {
      cmnCde.navFuncts.FindNavigationIndices(
        long.Parse(this.dsplySizePstComboBox.Text), this.totl_pst);
      if (this.pst_cur_indx >= cmnCde.navFuncts.totalGroups)
      {
        this.pst_cur_indx = cmnCde.navFuncts.totalGroups - 1;
      }
      if (this.pst_cur_indx < 0)
      {
        this.pst_cur_indx = 0;
      }
      cmnCde.navFuncts.currentNavigationIndex = this.pst_cur_indx;
    }

    private void updtPstNavLabels()
    {
      this.moveFirstPstButton.Enabled = cmnCde.navFuncts.moveFirstBtnStatus();
      this.movePreviousPstButton.Enabled = cmnCde.navFuncts.movePrevBtnStatus();
      this.moveNextPstButton.Enabled = cmnCde.navFuncts.moveNextBtnStatus();
      this.moveLastPstButton.Enabled = cmnCde.navFuncts.moveLastBtnStatus();
      this.positionPstTextBox.Text = cmnCde.navFuncts.displayedRecordsNumbers();
      if (this.is_last_pst == true ||
        this.totl_pst != cmnCde.Big_Val)
      {
        this.totalRecsPstLabel.Text = cmnCde.navFuncts.totalRecordsLabel();
      }
      else
      {
        this.totalRecsPstLabel.Text = "of Total";
      }
    }

    private void populatePstListVw()
    {
      this.obey_pst_evnts = false;
      this.pstPayListView.Items.Clear();
      DataSet dtst;
      if (this.payItmID <= 0
        || this.prsnID <= 0)
      {
        this.is_last_pst = true;
        this.totl_pst = 0;
        this.last_pst_num = 0;
        this.pst_cur_indx = 0;
        this.updtPstTotals();
        this.updtPstNavLabels();
        return;
      }
      if (this.itmMajType.ToUpper() == "Balance Item".ToUpper())
      {
        dtst = this.get_Basic_PstBls(this.searchForPstTextBox.Text,
     this.searchInPstComboBox.Text, this.pst_cur_indx,
     int.Parse(this.dsplySizePstComboBox.Text),
     this.prsnID,
     this.payItmID);
      }
      else
      {
        dtst = this.get_Basic_Pst(this.searchForPstTextBox.Text,
         this.searchInPstComboBox.Text, this.pst_cur_indx,
         int.Parse(this.dsplySizePstComboBox.Text),
         this.prsnID,
         this.payItmID);
      }
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        this.last_pst_num = cmnCde.navFuncts.startIndex() + i;
        string uom = "Number";
        string amnt = "0.00";
        if (this.itmUom.ToUpper() == "Money".ToUpper())
        {
          if (this.itmMajType.ToUpper() == "Balance Item".ToUpper())
          {
            uom = "Money";
          }
          else
          {
            uom = cmnCde.getPssblValNm(int.Parse(dtst.Tables[0].Rows[i][4].ToString()));
          }
          amnt = double.Parse(dtst.Tables[0].Rows[i][1].ToString()).ToString("#,#0.00");
        }
        else
        {
          amnt = double.Parse(dtst.Tables[0].Rows[i][1].ToString()).ToString("#,#0");
        }
        ListViewItem nwItem = new ListViewItem(new string[] {
    (cmnCde.navFuncts.startIndex() + i).ToString(),
    amnt, uom,
    dtst.Tables[0].Rows[i][2].ToString(),
    dtst.Tables[0].Rows[i][3].ToString(),
    dtst.Tables[0].Rows[i][0].ToString(),
        dtst.Tables[0].Rows[i][5].ToString(),
        dtst.Tables[0].Rows[i][4].ToString(),
        dtst.Tables[0].Rows[i][6].ToString(),
        dtst.Tables[0].Rows[i][7].ToString(),
        dtst.Tables[0].Rows[i][8].ToString()});
        nwItem.UseItemStyleForSubItems = false;
        if (dtst.Tables[0].Rows[i][7].ToString() == "VALID")
        {
          nwItem.SubItems[9].BackColor = Color.Lime;
        }
        else
        {
          nwItem.SubItems[9].BackColor = Color.Red;
        }
        this.pstPayListView.Items.Add(nwItem);
      }
      if (dtst.Tables[0].Rows.Count <= 0
        && this.itmMajType.ToUpper() == "Balance Item".ToUpper())
      {
        string uom = "Number";
        string amnt = "0.00";
        if (this.itmUom.ToUpper() == "Money".ToUpper())
        {
          if (this.itmMajType.ToUpper() == "Balance Item".ToUpper())
          {
            uom = "Money";
          }
          amnt = double.Parse(this.expctdAmntTextBox.Text).ToString("#,#0.00");
        }
        else
        {
          amnt = double.Parse(this.expctdAmntTextBox.Text).ToString("#,#0");
        }
        string dateStr = DateTime.ParseExact(
    cmnCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
    System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
        ListViewItem nwItem = new ListViewItem(new string[] {
    (1).ToString(),
    amnt, uom,
    dateStr,
    "Balance Amount",
    "-1",
        "Balance",
        this.crncyIDTextBox.Text,
        "-1",
        "VALID","-1"});
        this.pstPayListView.Items.Add(nwItem);

      }
      this.correctPstNavLbls(dtst);
      if (this.pstPayListView.Items.Count > 0)
      {
        this.pstPayListView.Items[0].Selected = true;
      }
      this.obey_pst_evnts = true;
    }

    private void clearMnlPay()
    {
      this.trnsTypComboBox.Items.Clear();
      this.expctdAmntTextBox.Text = "";
      this.amntNumericUpDown.Value = 0;
      this.paymntDateTextBox.Text = "";
      this.paymntDescTextBox.Text = "";
    }

    private void correctPstNavLbls(DataSet dtst)
    {
      long totlRecs = dtst.Tables[0].Rows.Count;
      if (this.pst_cur_indx == 0 && totlRecs == 0)
      {
        this.is_last_pst = true;
        this.totl_pst = 0;
        this.last_pst_num = 0;
        this.pst_cur_indx = 0;
        this.updtPstTotals();
        this.updtPstNavLabels();
      }
      else if (this.totl_pst == cmnCde.Big_Val
     && totlRecs < long.Parse(this.dsplySizePstComboBox.Text))
      {
        this.totl_pst = this.last_pst_num;
        if (totlRecs == 0)
        {
          this.pst_cur_indx -= 1;
          this.updtPstTotals();
          this.populatePstListVw();
        }
        else
        {
          this.updtPstTotals();
        }
      }
    }

    private bool shdObeyPstEvts()
    {
      return this.obey_pst_evnts;
    }

    private void PstPnlNavButtons(object sender, System.EventArgs e)
    {
      System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
      this.totalRecsPstLabel.Text = "";
      if (sentObj.Name.ToLower().Contains("first"))
      {
        this.is_last_pst = false;
        this.pst_cur_indx = 0;
      }
      else if (sentObj.Name.ToLower().Contains("previous"))
      {
        this.is_last_pst = false;
        this.pst_cur_indx -= 1;
      }
      else if (sentObj.Name.ToLower().Contains("next"))
      {
        this.is_last_pst = false;
        this.pst_cur_indx += 1;
      }
      else if (sentObj.Name.ToLower().Contains("last"))
      {
        this.is_last_pst = true;
        if (this.itmMajType.ToUpper() == "Balance Item".ToUpper())
        {
          this.totl_pst = this.get_Total_PstBls(this.searchForPstTextBox.Text,
     this.searchInPstComboBox.Text,
     this.prsnID,
     this.payItmID);
        }
        else
        {
          this.totl_pst = this.get_Total_Pst(this.searchForPstTextBox.Text,
      this.searchInPstComboBox.Text,
     this.prsnID,
     this.payItmID);

        }
        this.updtPstTotals();
        this.pst_cur_indx = cmnCde.navFuncts.totalGroups - 1;
      }
      this.getPstPnlData();
    }

    private void searchForPstTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
      {
        this.searchForPstTextBox.Focus();
        this.goPstButton_Click(this.goPstButton, ex);
      }
    }

    private void positionPstTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
      {
        this.PstPnlNavButtons(this.movePreviousPstButton, ex);
      }
      else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
      {
        this.PstPnlNavButtons(this.moveNextPstButton, ex);
      }
    }

    private void exptPstMenuItem_Click(object sender, EventArgs e)
    {
      cmnCde.exprtToExcel(this.pstPayListView);
    }

    private void refreshPstMenuItem_Click(object sender, EventArgs e)
    {
      this.goPstButton_Click(this.goPstButton, e);
    }

    private void viewSQLPstMenuItem_Click(object sender, EventArgs e)
    {
      cmnCde.showSQL(this.pst_SQL, 8);
    }

    private void recHstryPstMenuItem_Click(object sender, EventArgs e)
    {
      if (this.pstPayListView.SelectedItems.Count <= 0)
      {
        cmnCde.showMsg("Please select a Record First!", 0);
        return;
      }
      cmnCde.showRecHstry(
        cmnCde.get_Gnrl_Rec_Hstry(long.Parse(
        this.pstPayListView.SelectedItems[0].SubItems[5].Text),
        "pay.pay_itm_trnsctns", "pay_trns_id"), 7);
    }

    private void printPstPyMenuItem_Click(object sender, EventArgs e)
    {
      if (this.pstPayListView.SelectedItems.Count <= 0)
      {
        cmnCde.showMsg("Please select a processed Payment First!", 0);
        return;
      }
      this.pageNo = 1;
      this.prntIdx = 0;
      this.printPreviewDialog1 = new PrintPreviewDialog();

      this.printPreviewDialog1.Document = printDocument1;
      this.printPreviewDialog1.FormBorderStyle = FormBorderStyle.Fixed3D;
      //this.printPreviewDialog1.SetBounds(400, 400, 300, 600);
      //this.printPreviewDialog1.PrintPreviewControl.Zoom = 1;

      this.printPreviewDialog1.PrintPreviewControl.AutoZoom = true;
      this.printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Pos", 283, 365);
      ((ToolStripButton)((ToolStrip)this.printPreviewDialog1.Controls[1]).Items[0]).Enabled = false;
      ((ToolStripButton)((ToolStrip)this.printPreviewDialog1.Controls[1]).Items[0]).Visible = false;
      //((ToolStripButton)((ToolStrip)this.printPreviewDialog1.Controls[1]).Items[0]).Click += new EventHandler(this.printRcptButton_Click);
      //this.printPreviewDialog1.MainMenuStrip = menuStrip1;
      //this.printPreviewDialog1.MainMenuStrip.Visible = true;
      this.printRcptButton1.Visible = true;
      ((ToolStrip)this.printPreviewDialog1.Controls[1]).Items.Add(this.printRcptButton1);

      this.printPreviewDialog1.FindForm().Height = this.Height;
      this.printPreviewDialog1.FindForm().StartPosition = FormStartPosition.Manual;
      this.printPreviewDialog1.FindForm().Location = new Point(800, 20);
      this.printPreviewDialog1.ShowDialog();
    }
    int pageNo = 1;
    int prntIdx = 0;

    private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
    {
      Pen aPen = new Pen(Brushes.Black, 1);
      Graphics g = e.Graphics;
      e.PageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Pos", 283, 1100);
      //Font font1 = new Font("Tahoma", 8.25f, FontStyle.Underline | FontStyle.Bold);
      //Font font2 = new Font("Tahoma", 8.25f, FontStyle.Bold);
      //Font font4 = new Font("Tahoma", 8.25f, FontStyle.Bold);
      //Font font3 = new Font("Courier New", 8.0f);
      //Font font5 = new Font("Times New Roman", 6.0f, FontStyle.Italic);
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

      if (this.pageNo == 1)
      {
        //Org Name
        nwLn = cmnCde.breakTxtDown(
          cmnCde.getOrgName(cmnCde.Org_id),
          pageWidth, font2, g);
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

        float ght = g.MeasureString(
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
        g.DrawLine(aPen, startX, startY + offsetY, startX + pageWidth,
          startY + offsetY);
        g.DrawString("Payment Receipt", font2, Brushes.Black, startX, startY + offsetY);
        offsetY += font2Hght;
        g.DrawLine(aPen, startX, startY + offsetY, startX + pageWidth,
        startY + offsetY);
        offsetY += font2Hght;
        g.DrawString("Receipt No: ", font4, Brushes.Black, startX, startY + offsetY);
        ght = g.MeasureString("Receipt No: ", font4).Width;
        //Receipt No: 
        g.DrawString(this.pstPayListView.SelectedItems[0].SubItems[5].Text.PadLeft(7, '0'),
    font3, Brushes.Black, startX + ght, startY + offsetY);
        offsetY += font4Hght;

        g.DrawString("Pay Item: ", font4, Brushes.Black, startX, startY + offsetY);
        //offsetY += font4Hght;
        ght = g.MeasureString("Pay Item: ", font4).Width;
        //Pay Item
        nwLn = cmnCde.breakTxtDown(
    this.itmName,
    pageWidth - ght - 20, font3, g);
        for (int i = 0; i < nwLn.Length; i++)
        {
          g.DrawString(nwLn[i]
          , font3, Brushes.Black, startX + ght, startY + offsetY);
          offsetY += font3Hght;
        }

        g.DrawString("Full Name: ", font4, Brushes.Black, startX, startY + offsetY);
        //offsetY += font4Hght;
        ght = g.MeasureString("Full Name: ", font4).Width;
        //Received From
        nwLn = cmnCde.breakTxtDown(
    this.prsnNameNo,
    pageWidth - ght - 20, font3, g);
        for (int i = 0; i < nwLn.Length; i++)
        {
          g.DrawString(nwLn[i]
          , font3, Brushes.Black, startX + ght, startY + offsetY);
          offsetY += font3Hght;
        }
        g.DrawString("Amount: ", font4, Brushes.Black, startX, startY + offsetY);
        //offsetY += font4Hght;
        ght = g.MeasureString("Amount: ", font4).Width;
        //Amount: 
        nwLn = cmnCde.breakTxtDown(
          this.pstPayListView.SelectedItems[0].SubItems[1].Text +
          " " + this.pstPayListView.SelectedItems[0].SubItems[2].Text,
    pageWidth - ght - 20, font3, g);
        for (int i = 0; i < nwLn.Length; i++)
        {
          g.DrawString(nwLn[i]
          , font3, Brushes.Black, startX + ght, startY + offsetY);
          offsetY += font3Hght;
        }
        g.DrawString("Date: ", font4, Brushes.Black, startX, startY + offsetY);
        ght = g.MeasureString("Date: ", font4).Width;
        //Date: 
        g.DrawString(this.pstPayListView.SelectedItems[0].SubItems[3].Text,
    font3, Brushes.Black, startX + ght, startY + offsetY);
        offsetY += font4Hght;
        g.DrawString("Being: ", font4, Brushes.Black, startX, startY + offsetY);
        ght = g.MeasureString("Being: ", font4).Width;
        //Being: 
        nwLn = cmnCde.breakTxtDown(
          this.pstPayListView.SelectedItems[0].SubItems[6].Text,
    pageWidth - ght - 20, font3, g);
        for (int i = 0; i < nwLn.Length; i++)
        {
          g.DrawString(nwLn[i]
          , font3, Brushes.Black, startX + ght, startY + offsetY);
          offsetY += font3Hght;
        }
        //Slogan: 
        offsetY += font3Hght;
        //offsetY += 5;
        //offsetY += font3Hght;
        g.DrawLine(aPen, startX, startY + offsetY, startX + pageWidth,
    startY + offsetY);
        offsetY += 1;

        nwLn = cmnCde.breakTxtDown(
          cmnCde.getOrgSlogan(cmnCde.Org_id),
    pageWidth - ght, font5, g);
        for (int i = 0; i < nwLn.Length; i++)
        {
          g.DrawString(nwLn[i]
          , font5, Brushes.Black, startX, startY + offsetY);
          offsetY += font5Hght;
        }


        //offsetY += font5Hght;

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
    "Website:www.rhomicomgh.com",
    pageWidth + 40, font5, g);
        for (int i = 0; i < nwLn.Length; i++)
        {
          g.DrawString(nwLn[i]
          , font5, Brushes.Black, startX, startY + offsetY);
          offsetY += font5Hght;
        }
      }
      //for (int i = this.prntIdx; i < 100; i++)
      //{
      //  g.DrawString(this.prsNamesListView.SelectedItems[0].SubItems[2].Text,
      //    font3, Brushes.Black, startX, startY + offsetY);
      //  offsetY += font3Hght;
      //  this.prntIdx++;
      //  if (offsetY >= pageHeight)
      //  {
      //    e.HasMorePages = true;
      //    offsetY = 0;
      //    this.pageNo++;
      //    return;
      //  }
      //  //else
      //  //{
      //  //  e.HasMorePages = false;
      //  //}
      //}
    }

    private void printPprPstMenuItem_Click(object sender, EventArgs e)
    {
      if (this.pstPayListView.SelectedItems.Count <= 0)
      {
        cmnCde.showMsg("Please select a processed Payment First!", 0);
        return;
      }
      this.pageNo = 1;
      this.prntIdx = 0;
      this.printDialog1 = new PrintDialog();
      this.printDialog1.UseEXDialog = true;
      this.printDialog1.ShowNetwork = true;
      this.printDialog1.AllowCurrentPage = true;
      this.printDialog1.AllowPrintToFile = true;
      this.printDialog1.AllowSelection = true;
      this.printDialog1.AllowSomePages = true;

      printDialog1.Document = this.printDocument1;
      DialogResult res = printDialog1.ShowDialog();
      if (res == DialogResult.OK)
      {
        printDocument1.Print();
      }
    }

    private void goPstButton_Click(object sender, EventArgs e)
    {
      this.loadPstPayPanel();
    }

    private void pstPayListView_ItemSelectionChanged(object sender, System.Windows.Forms.ListViewItemSelectionChangedEventArgs e)
    {
      if (this.shdObeyPstEvts() == false)
      {
        return;
      }
      if (e.IsSelected)
      {
        e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
      }
      else
      {
        e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Regular);
      }
    }

    #endregion

    #region "Process Payments..."
    private bool sendToGLInterface(
      long prsn_id, string loc_id_no, long itm_id,
      string itm_name, string itm_uom, double pay_amnt,
      string trns_date, string trns_desc,
      int crncy_id, long msg_id, string log_tbl,
      string dateStr, string trns_src, string glDate, long orgnlTrnsID)
    {
      try
      {
        //Get Todays GL Batch Name
        long paytrnsid = this.getPaymntTrnsID(
        prsn_id, itm_id,
        pay_amnt, trns_date, orgnlTrnsID);
        //Create GL Lines based on item's defined accounts
        string[] accntinf = new string[4];
        double netamnt = 0;
        accntinf = this.get_ItmAccntInfo(itm_id);

        if (itm_uom != "Number" && int.Parse(accntinf[1]) > 0 && int.Parse(accntinf[3]) > 0)
        {

          netamnt = cmnCde.dbtOrCrdtAccntMultiplier(
            int.Parse(accntinf[1]),
            accntinf[0].Substring(0, 1)) * pay_amnt;
          long py_dbt_ln = this.getIntFcTrnsDbtLn(paytrnsid, pay_amnt);
          long py_crdt_ln = this.getIntFcTrnsCrdtLn(paytrnsid, pay_amnt);

          if (cmnCde.dbtOrCrdtAccnt(int.Parse(accntinf[1]),
            accntinf[0].Substring(0, 1)) == "Debit")
          {
            if (py_dbt_ln <= 0)
            {
              this.createPymntGLIntFcLn(int.Parse(accntinf[1]),
                trns_desc,
                    pay_amnt, glDate,
                    crncy_id, 0,
                    netamnt, paytrnsid, dateStr);
            }
          }
          else
          {
            if (py_crdt_ln <= 0)
            {
              this.createPymntGLIntFcLn(int.Parse(accntinf[1]),
                trns_desc,
          0, glDate,
          crncy_id, pay_amnt,
          netamnt, paytrnsid, dateStr);
            }
          }
          //Repeat same for balancing leg
          netamnt = cmnCde.dbtOrCrdtAccntMultiplier(
              int.Parse(accntinf[3]),
              accntinf[2].Substring(0, 1)) * pay_amnt;
          if (cmnCde.dbtOrCrdtAccnt(int.Parse(accntinf[3]),
            accntinf[2].Substring(0, 1)) == "Debit")
          {
            if (py_dbt_ln <= 0)
            {
              this.createPymntGLIntFcLn(int.Parse(accntinf[3]),
                trns_desc,
                    pay_amnt, glDate,
                    crncy_id, 0,
                    netamnt, paytrnsid, dateStr);
            }
          }
          else
          {
            if (py_crdt_ln <= 0)
            {
              this.createPymntGLIntFcLn(int.Parse(accntinf[3]),
                trns_desc,
          0, glDate,
          crncy_id, pay_amnt,
          netamnt, paytrnsid, dateStr);
            }
          }
        }
        return true;
      }
      catch (Exception ex)
      {
        cmnCde.updateLogMsg(msg_id,
    "\r\nError Sending Payment to GL Interface for Person:" +
    loc_id_no + " Item: " + itm_name + " " + ex.Message, log_tbl, dateStr);
        return false;
      }
    }

    public bool sendToGLInterfaceMnl(
  long prsn_id, long itm_id, string itm_uom, double pay_amnt,
  string trns_date, string trns_desc,
  int crncy_id, string dateStr, string trns_src, string glDate, long orgnlTrnsID)
    {
      try
      {
        long paytrnsid = this.getPaymntTrnsID(
        prsn_id, itm_id,
        pay_amnt, trns_date, orgnlTrnsID);
        //Create GL Lines based on item's defined accounts
        string[] accntinf = new string[4];
        accntinf = this.get_ItmAccntInfo(itm_id);

        if (itm_uom != "Number" && int.Parse(accntinf[1]) > 0 && int.Parse(accntinf[3]) > 0)
        {
          double netamnt = 0;

          netamnt = cmnCde.dbtOrCrdtAccntMultiplier(
            int.Parse(accntinf[1]),
            accntinf[0].Substring(0, 1)) * pay_amnt;

          long py_dbt_ln = this.getIntFcTrnsDbtLn(paytrnsid, pay_amnt);
          long py_crdt_ln = this.getIntFcTrnsCrdtLn(paytrnsid, pay_amnt);
          if (cmnCde.dbtOrCrdtAccnt(int.Parse(accntinf[1]),
            accntinf[0].Substring(0, 1)) == "Debit")
          {
            if (py_dbt_ln <= 0)
            {
              this.createPymntGLIntFcLn(int.Parse(accntinf[1]),
                trns_desc,
                    pay_amnt, glDate,
                    crncy_id, 0,
                    netamnt, paytrnsid, dateStr);
            }
          }
          else
          {
            if (py_crdt_ln <= 0)
            {
              this.createPymntGLIntFcLn(int.Parse(accntinf[1]),
              trns_desc,
        0, glDate,
        crncy_id, pay_amnt,
        netamnt, paytrnsid, dateStr);
            }
          }
          //Repeat same for balancing leg
          netamnt = cmnCde.dbtOrCrdtAccntMultiplier(
              int.Parse(accntinf[3]),
              accntinf[2].Substring(0, 1)) * pay_amnt;
          if (cmnCde.dbtOrCrdtAccnt(int.Parse(accntinf[3]),
            accntinf[2].Substring(0, 1)) == "Debit")
          {
            if (py_dbt_ln <= 0)
            {
              this.createPymntGLIntFcLn(int.Parse(accntinf[3]),
               trns_desc,
                   pay_amnt, glDate,
                   crncy_id, 0,
                   netamnt, paytrnsid, dateStr);
            }
          }
          else
          {
            if (py_crdt_ln <= 0)
            {
              this.createPymntGLIntFcLn(int.Parse(accntinf[3]),
                trns_desc,
          0, glDate,
          crncy_id, pay_amnt,
          netamnt, paytrnsid, dateStr);
            }
          }
        }
        return true;
      }
      catch (Exception ex)
      {
        cmnCde.showMsg("Error Sending Payment to GL Interface" +
          " " + ex.Message, 0);
        return false;
      }
    }

    private bool sendToGL()
    {
      try
      {
        //Get Todays GL Batch Name
        string dateStr = DateTime.ParseExact(
    cmnCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
    System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
        string todaysGlBatch = "Internal Payments (" + dateStr + ")";
        long todbatchid = this.getTodaysGLBatchID(
          todaysGlBatch,
          cmnCde.Org_id);
        if (todbatchid <= 0)
        {
          this.createTodaysGLBatch(cmnCde.Org_id,
            todaysGlBatch, todaysGlBatch, "Internal Payments");
          todbatchid = this.getTodaysGLBatchID(
          todaysGlBatch,
          cmnCde.Org_id);
        }
        if (todbatchid > 0)
        {
          todaysGlBatch = this.get_GLBatch_Nm(todbatchid);
        }

        /*
         * 1. Get list of all accounts to transfer from the 
         * interface table and their total amounts.
         * 2. Loop through each and transfer
         */
        DataSet dtst = this.getAllInGLIntrfcOrg(cmnCde.Org_id);
        long cntr = dtst.Tables[0].Rows.Count;

        if (cntr > 0)
        {
          double dfrnce = 0;
          if (this.isGLIntrfcBlcdOrg(cmnCde.Org_id, ref dfrnce) == false)
          {
            cmnCde.showMsg("Cannot Transfer Transactions to GL because\r\n" +
              " Transactions in the GL Interface are not Balanced!" +
            "\r\nDIFFERENCE=" + dfrnce.ToString(), 0);
            return false;
          }
        }
        else
        {
          //cmnCde.showMsg("There is nothing in the GL Interface Table to Transfer!", 0);
          //return false;
        }

        //dateStr = cmnCde.getFrmtdDB_Date_time();
        for (int a = 0; a < cntr; a++)
        {
          string src_ids = this.getGLIntrfcIDs(int.Parse(dtst.Tables[0].Rows[a][0].ToString()),
            dtst.Tables[0].Rows[a][1].ToString(),
            int.Parse(dtst.Tables[0].Rows[a][5].ToString()));

          double entrdAmnt = double.Parse(dtst.Tables[0].Rows[a][2].ToString()) == 0 ? double.Parse(dtst.Tables[0].Rows[a][3].ToString()) : double.Parse(dtst.Tables[0].Rows[a][2].ToString());
          string dbtCrdt = double.Parse(dtst.Tables[0].Rows[a][3].ToString()) == 0 ? "D" : "C";
          int accntCurrID = int.Parse(cmnCde.getGnrlRecNm(
     "accb.accb_chart_of_accnts", "accnt_id", "crncy_id", int.Parse(dtst.Tables[0].Rows[a][0].ToString())));

          double accntCurrRate = Math.Round(
            this.get_LtstExchRate(int.Parse(dtst.Tables[0].Rows[a][5].ToString()), accntCurrID,
     dtst.Tables[0].Rows[a][1].ToString()), 15);

          double[] actlAmnts = this.getGLIntrfcIDAmntSum(src_ids, int.Parse(dtst.Tables[0].Rows[a][0].ToString()));

          if (actlAmnts[0] == double.Parse(dtst.Tables[0].Rows[a][2].ToString())
            && actlAmnts[1] == double.Parse(dtst.Tables[0].Rows[a][3].ToString()))
          {

            this.createPymntGLLine(int.Parse(dtst.Tables[0].Rows[a][0].ToString()),
      "Lumped sum of all payments (from the Internal Payments module) to this account",
        double.Parse(dtst.Tables[0].Rows[a][2].ToString()),
        dtst.Tables[0].Rows[a][1].ToString(),
        int.Parse(dtst.Tables[0].Rows[a][5].ToString()), todbatchid,
        double.Parse(dtst.Tables[0].Rows[a][3].ToString()),
        double.Parse(dtst.Tables[0].Rows[a][4].ToString()), src_ids, dateStr,
        entrdAmnt, int.Parse(dtst.Tables[0].Rows[a][5].ToString()),
        entrdAmnt * accntCurrRate, accntCurrID,
        1, accntCurrID, dbtCrdt);
          }
          else
          {
            cmnCde.showMsg("Interface Transaction Amounts DR:" + actlAmnts[0] + " CR:" + actlAmnts[1] +
      " \r\ndo not match Amount being sent to GL DR:" + double.Parse(dtst.Tables[0].Rows[a][2].ToString()) +
      " CR:" + double.Parse(dtst.Tables[0].Rows[a][3].ToString()) + "!\r\n Interface Line IDs:" + src_ids, 0);
            break;
          }
        }
        if (this.get_Batch_CrdtSum(todbatchid) == this.get_Batch_DbtSum(todbatchid))
        {
          this.updtPymntAllGLIntrfcLnOrg(todbatchid, cmnCde.Org_id);
          this.updtGLIntrfcLnSpclOrg(cmnCde.Org_id);
          this.updtTodaysGLBatchPstngAvlblty(todbatchid, "1");
          return true;
        }
        else
        {
          cmnCde.showMsg("The GL Batch created is not Balanced!\r\nTransactions created will be reversed and deleted!", 0);
          this.deleteBatchTrns(todbatchid);
          this.deleteBatch(todbatchid, todaysGlBatch);
          return false;
        }
        //this.updtPymntAllGLIntrfcLnOrg(todbatchid, cmnCde.Org_id);
        //this.updtGLIntrfcLnSpclOrg(cmnCde.Org_id);
        //return true;
      }
      catch (Exception ex)
      {
        cmnCde.showMsg("Error Sending Payment to GL!\r\n" + ex.Message, 0);
        return false;
      }
    }

    private bool sendToGL(long py_trns_id)
    {
      try
      {
        //Get Todays GL Batch Name
        string dateStr = DateTime.ParseExact(
    cmnCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
    System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
        string todaysGlBatch = "Internal Payments (" + dateStr + ")";
        long todbatchid = this.getTodaysGLBatchID(
          todaysGlBatch,
          cmnCde.Org_id);
        if (todbatchid <= 0)
        {
          this.createTodaysGLBatch(cmnCde.Org_id,
            todaysGlBatch, todaysGlBatch, "Internal Payments");
          todbatchid = this.getTodaysGLBatchID(
          todaysGlBatch,
          cmnCde.Org_id);
        }
        if (todbatchid > 0)
        {
          todaysGlBatch = this.get_GLBatch_Nm(todbatchid);
        }

        /*
         * 1. Get list of all accounts to transfer from the 
         * interface table and their total amounts.
         * 2. Loop through each and transfer
         */

        DataSet dtst = this.getAllInGLIntrfc(py_trns_id);
        long cntr = dtst.Tables[0].Rows.Count;

        if (cntr > 0)
        {
          if (this.isGLIntrfcBlcd(py_trns_id) == false)
          {
            cmnCde.showMsg("Cannot Transfer Transactions to GL because\r\n" +
              " Transactions in the GL Interface for this Payment are not Balanced!", 0);
            return false;
          }
        }
        else
        {
          //cmnCde.showMsg("There is nothing in the GL Interface Table to Transfer!", 0);
          //return false;
        }

        for (int a = 0; a < cntr; a++)
        {
          string src_ids = this.getGLIntrfcIDsMnl(
            int.Parse(dtst.Tables[0].Rows[a][0].ToString()),
            dtst.Tables[0].Rows[a][1].ToString(),
            int.Parse(dtst.Tables[0].Rows[a][5].ToString()), py_trns_id);

          double entrdAmnt = double.Parse(dtst.Tables[0].Rows[a][2].ToString()) == 0 ? double.Parse(dtst.Tables[0].Rows[a][3].ToString()) : double.Parse(dtst.Tables[0].Rows[a][2].ToString());
          string dbtCrdt = double.Parse(dtst.Tables[0].Rows[a][3].ToString()) == 0 ? "D" : "C";
          int accntCurrID = int.Parse(cmnCde.getGnrlRecNm(
     "accb.accb_chart_of_accnts", "accnt_id", "crncy_id", int.Parse(dtst.Tables[0].Rows[a][0].ToString())));

          double accntCurrRate = Math.Round(
            this.get_LtstExchRate(int.Parse(dtst.Tables[0].Rows[a][5].ToString()), accntCurrID,
     dtst.Tables[0].Rows[a][1].ToString()), 15);

          this.createPymntGLLine(int.Parse(dtst.Tables[0].Rows[a][0].ToString()),
            "Lumped sum of all payments (from the Internal Payments module) to this account",
                double.Parse(dtst.Tables[0].Rows[a][2].ToString()),
                dtst.Tables[0].Rows[a][1].ToString(),
                int.Parse(dtst.Tables[0].Rows[a][5].ToString()), todbatchid,
                double.Parse(dtst.Tables[0].Rows[a][3].ToString()),
                double.Parse(dtst.Tables[0].Rows[a][4].ToString()), src_ids, dateStr,
      entrdAmnt, int.Parse(dtst.Tables[0].Rows[a][5].ToString()),
      entrdAmnt * accntCurrRate, accntCurrID,
      1, accntCurrID, dbtCrdt);

        }
        if (this.get_Batch_CrdtSum(todbatchid) == this.get_Batch_DbtSum(todbatchid))
        {
          this.updtPymntMnlGLIntrfcLn(py_trns_id, todbatchid);
          //this.updtGLIntrfcLnSpclOrg(cmnCde.Org_id);
          this.updtTodaysGLBatchPstngAvlblty(todbatchid, "1");
          return true;
        }
        else
        {
          cmnCde.showMsg("The GL Batch created is not Balanced!\r\nTransactions created will be reversed and deleted!", 0);
          this.deleteBatchTrns(todbatchid);
          this.deleteBatch(todbatchid, todaysGlBatch);
          return false;
        }
        //return true;
      }
      catch (Exception ex)
      {
        cmnCde.showMsg("Error Sending Payment to GL!\r\n" + ex.Message, 0);
        return false;
      }
    }

    private bool sendMsPyToGL(long mspyid)
    {
      try
      {
        //Get Todays GL Batch Name
        string dateStr = DateTime.ParseExact(
    cmnCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
    System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
        string todaysGlBatch = "Internal Payments (" + dateStr + ")";
        long todbatchid = this.getTodaysGLBatchID(
          todaysGlBatch,
          cmnCde.Org_id);
        if (todbatchid <= 0)
        {
          this.createTodaysGLBatch(cmnCde.Org_id,
            todaysGlBatch, todaysGlBatch, "Internal Payments");
          todbatchid = this.getTodaysGLBatchID(
          todaysGlBatch,
          cmnCde.Org_id);
        }
        if (todbatchid > 0)
        {
          todaysGlBatch = this.get_GLBatch_Nm(todbatchid);
        }

        /*
         * 1. Get list of all accounts to transfer from the 
         * interface table and their total amounts.
         * 2. Loop through each and transfer
         */
        DataSet dtst = this.getAllInMsPyGLIntrfc(mspyid);
        long cntr = dtst.Tables[0].Rows.Count;

        if (cntr > 0)
        {
          if (this.isMsPyGLIntrfcBlcd(mspyid) == false)
          {
            cmnCde.showMsg("Cannot Transfer Transactions to GL because\r\n" +
              " this Mass Pay Run's Transactions in the \r\n GL Interface are not Balanced!", 0);
            return false;
          }
        }
        else
        {
          //cmnCde.showMsg("There is nothing in the GL Interface Table to Transfer!", 0);
          //return false;
        }

        for (int a = 0; a < cntr; a++)
        {
          string src_ids = this.getGLIntrfcIDsMsPy(int.Parse(dtst.Tables[0].Rows[a][0].ToString()),
     dtst.Tables[0].Rows[a][1].ToString(), int.Parse(dtst.Tables[0].Rows[a][5].ToString()), mspyid);

          double entrdAmnt = double.Parse(dtst.Tables[0].Rows[a][2].ToString()) == 0 ? double.Parse(dtst.Tables[0].Rows[a][3].ToString()) : double.Parse(dtst.Tables[0].Rows[a][2].ToString());
          string dbtCrdt = double.Parse(dtst.Tables[0].Rows[a][3].ToString()) == 0 ? "D" : "C";
          int accntCurrID = int.Parse(cmnCde.getGnrlRecNm(
     "accb.accb_chart_of_accnts", "accnt_id", "crncy_id", int.Parse(dtst.Tables[0].Rows[a][0].ToString())));

          double accntCurrRate = Math.Round(
            this.get_LtstExchRate(int.Parse(dtst.Tables[0].Rows[a][5].ToString()), accntCurrID,
     dtst.Tables[0].Rows[a][1].ToString()), 15);

          this.createPymntGLLine(int.Parse(dtst.Tables[0].Rows[a][0].ToString()),
     "Lumped sum of all payments (from the Internal Payments module) to this account",
      double.Parse(dtst.Tables[0].Rows[a][2].ToString()),
      dtst.Tables[0].Rows[a][1].ToString(),
      int.Parse(dtst.Tables[0].Rows[a][5].ToString()), todbatchid,
      double.Parse(dtst.Tables[0].Rows[a][3].ToString()),
      double.Parse(dtst.Tables[0].Rows[a][4].ToString()), src_ids, dateStr,
      entrdAmnt, int.Parse(dtst.Tables[0].Rows[a][5].ToString()),
      entrdAmnt * accntCurrRate, accntCurrID,
      1, accntCurrID, dbtCrdt);

        }
        if (this.get_Batch_CrdtSum(todbatchid) == this.get_Batch_DbtSum(todbatchid))
        {
          this.updtPymntMsPyGLIntrfcLn(mspyid, todbatchid);
          //this.updtGLIntrfcLnSpclOrg(cmnCde.Org_id);
          this.updtTodaysGLBatchPstngAvlblty(todbatchid, "1");
          return true;
        }
        else
        {
          cmnCde.showMsg("The GL Batch created is not Balanced!\r\nTransactions created will be reversed and deleted!", 0);
          this.deleteBatchTrns(todbatchid);
          this.deleteBatch(todbatchid, todaysGlBatch);
          return false;
        }
        //return true;
      }
      catch (Exception ex)
      {
        cmnCde.showMsg("Error Sending Payment to GL!\r\n" + ex.Message, 0);
        return false;
      }
    }

    private bool isPayTrnsValid()
    {
      if (this.itmUom != "Number"
        && this.itmMajType != "Balance Item"
        && this.itmMinType != "Purely Informational")
      {
        string[] accntinf = new string[4];
        double netamnt = 0;
        accntinf = this.get_ItmAccntInfo(this.payItmID);

        netamnt = cmnCde.dbtOrCrdtAccntMultiplier(int.Parse(accntinf[1]),
          accntinf[0].Substring(0, 1)) * (double)this.amntNumericUpDown.Value;

        if (!cmnCde.isTransPrmttd(
    int.Parse(accntinf[1]), this.glDateTextBox.Text, netamnt))
        {
          return false;
        }
      }
      return true;
    }

    private bool isMsPayTrnsValid(string itmuom, string itmmjtyp,
      string itmintyp, long itmid, string trnsdte, double pyamnt)
    {
      if (itmuom != "Number"
        && itmmjtyp != "Balance Item"
        && itmintyp != "Purely Informational")
      {
        string[] accntinf = new string[4];
        double netamnt = 0;
        accntinf = this.get_ItmAccntInfo(itmid);

        netamnt = cmnCde.dbtOrCrdtAccntMultiplier(int.Parse(accntinf[1]),
          accntinf[0].Substring(0, 1)) * pyamnt;

        if (!cmnCde.isTransPrmttd(
    int.Parse(accntinf[1]), trnsdte, netamnt))
        {
          return false;
        }
      }
      return true;
    }

    private void paymntDateButton_Click(object sender, EventArgs e)
    {
      cmnCde.selectDate(ref this.paymntDateTextBox);
      if (this.glDateTextBox.Text == "")
      {
        this.glDateTextBox.Text = this.paymntDateTextBox.Text;
      }
    }

    public bool isMnlpyInUse(long pyID)
    {
      string strSql = "SELECT a.pay_trns_id " +
       "FROM pay.pay_itm_trnsctns a, pay.pay_gl_interface b " +
       "WHERE a.pay_trns_id = " + pyID +
       @" and b.source_trns_id = a.pay_trns_id and (b.gl_batch_id > 0 or 
 (a.pymnt_vldty_status = 'VALID' and a.src_py_trns_id <= 0)) LIMIT 1";
      /* or a.pymnt_vldty_status = 'VOID'*/
      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return true;
      }
      return false;
    }

    public long getPymntRvrsal(long pyID)
    {
      string strSql = "SELECT a.pay_trns_id " +
       "FROM pay.pay_itm_trnsctns a " +
       "WHERE (a.src_py_trns_id = " + pyID +
       @" and a.pymnt_vldty_status = 'VALID')";
      /* or a.pymnt_vldty_status = 'VOID'*/
      DataSet dtst = cmnCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return long.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      return -1;
    }

    private void deletePayMenuItem_Click(object sender, EventArgs e)
    {
      if (cmnCde.test_prmssns(this.dfltPrvldgs[19]) == false)
      {
        cmnCde.showMsg("You don't have permission to perform" +
         " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.pstPayListView.SelectedItems.Count <= 0)
      {
        cmnCde.showMsg("Please select the Payment to DELETE!", 0);
        return;
      }
      if (long.Parse(this.pstPayListView.SelectedItems[0].SubItems[5].Text) <= 0)
      {
        cmnCde.showMsg("Invalid Payment Selected!", 0);
        return;
      }

      if (this.isMnlpyInUse(long.Parse(this.pstPayListView.SelectedItems[0].SubItems[5].Text)) == true)
      {
        cmnCde.showMsg("This Payment has been SENT to GL or has not been REVERSED hence cannot be DELETED!", 0);
        return;
      }
      long rvrslmnlpyid = this.getPymntRvrsal(long.Parse(this.pstPayListView.SelectedItems[0].SubItems[5].Text));

      if (rvrslmnlpyid <= 0)
      {
        cmnCde.showMsg("This Payment has not been REVERSED hence cannot be DELETED!", 0);
        return;
      }

      if (cmnCde.showMsg("Are you sure you want to DELETE " +
        "the selected Payment and its Reversal?" +
   "\r\nThis action cannot be undone!", 1) == DialogResult.No)
      {
        //cmnCde.showMsg("Operation Cancelled!", 4);
        return;
      }

      cmnCde.deleteGnrlRecs(rvrslmnlpyid,
this.pstPayListView.SelectedItems[0].SubItems[6].Text + " (Reversal)", "pay.pay_itm_trnsctns", "pay_trns_id");

      cmnCde.deleteGnrlRecs(long.Parse(this.pstPayListView.SelectedItems[0].SubItems[5].Text),
this.pstPayListView.SelectedItems[0].SubItems[6].Text, "pay.pay_itm_trnsctns", "pay_trns_id");

      this.populatePstListVw();
    }

    private void rvrsPymntButton_Click(object sender, EventArgs e)
    {
      if (cmnCde.test_prmssns(this.dfltPrvldgs[10]) == false)
      {
        cmnCde.showMsg("You don't have permission to perform" +
          " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.pstPayListView.SelectedItems.Count <= 0)
      {
        cmnCde.showMsg("Please select the Payment to Reverse!", 0);
        return;
      }
      if (this.itmMajType.ToUpper() == "Balance Item".ToUpper())
      {
        cmnCde.showMsg("Cannot reverse the Balance on a Balance Item!", 0);
        return;
      }
      bool beenrvsrdB4 = false;
      if (cmnCde.showMsg("NB: This transaction is not complete until you click" +
        "\r\n on the PROCESS PAYMENT Button above!\r\nAre you sure you want to Proceed?", 1) == DialogResult.No)
      {
        cmnCde.showMsg("Operation Cancelled!", 4);
        return;
      }
      DataSet dtst = this.getPstPayDet(long.Parse(this.pstPayListView.SelectedItems[0].SubItems[5].Text));
      this.trnsTypComboBox.SelectedItem = dtst.Tables[0].Rows[0][2].ToString();
      this.amntNumericUpDown.Value = -1 * Decimal.Parse(dtst.Tables[0].Rows[0][0].ToString());
      this.paymntDateTextBox.Text = dtst.Tables[0].Rows[0][1].ToString();
      this.paymntDescTextBox.Text = "(Reversal of Payment) " + dtst.Tables[0].Rows[0][4].ToString();
      this.trnsTypComboBox.Enabled = false;
      this.amntNumericUpDown.Enabled = false;
      this.paymntDescTextBox.Enabled = false;
      this.paymntDateButton.Enabled = false;
      this.paymntDateTextBox.Enabled = false;
    }

    private void processPayButton_Click(object sender, EventArgs e)
    {
      if (cmnCde.test_prmssns(this.dfltPrvldgs[9]) == false)
      {
        cmnCde.showMsg("You don't have permission to perform" +
          " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.payItmID <= 0)
      {
        cmnCde.showMsg("Please select a Pay Item First!", 0);
        return;
      }
      if (this.prsnID <= 0)
      {
        cmnCde.showMsg("Please select a Person First!", 0);
        return;
      }
      if (this.itmMajType.ToUpper() == "Balance Item".ToUpper())
      {
        cmnCde.showMsg("Cannot run Payment for Balance Items!", 0);
        return;
      }
      long prsnItmRwID = this.doesPrsnHvItmPrs(this.prsnID,
          this.payItmID);
      if (prsnItmRwID <= 0)
      {
        long dfltVal = this.getFirstItmValID(this.payItmID);
        if (dfltVal > 0)
        {
          this.createBnftsPrs(this.prsnID,
    this.payItmID
      , dfltVal
      , "01-Jan-1900", "31-Dec-4000");
        }
      }
      else if (this.doesPrsnHvItm(this.prsnID,
this.payItmID, this.paymntDateTextBox.Text) == false)
      {
        cmnCde.showMsg("The selected person does not have the \r\nselected Item as at the Payment Date Specified!", 0);
        return;
      }
      //if (this.doesPrsnHvItm(this.prsnID
      //    , this.payItmID
      //    , this.paymntDateTextBox.Text) == false && this.paymntDescTextBox.Enabled == true)
      //{
      //  cmnCde.showMsg("The selected person does not have the \r\nselected Item as at the Payment Date Specified!", 0);
      //  return;
      //}
      if (this.trnsTypComboBox.Text == "")
      {
        cmnCde.showMsg("Transaction Type cannot be empty!", 0);
        return;
      }
      if (this.amntNumericUpDown.Value == 0)
      {
        cmnCde.showMsg("Amount cannot be zero!", 0);
        return;
      }
      if (this.paymntDateTextBox.Text == "")
      {
        cmnCde.showMsg("Payment Date cannot be empty!", 0);
        return;
      }
      if (this.glDateTextBox.Text == "")
      {
        cmnCde.showMsg("GL Date cannot be empty!", 0);
        return;
      }

      if (this.paymntDescTextBox.Text == "")
      {
        cmnCde.showMsg("Payment Description cannot be empty!", 0);
        return;
      }
      if ((this.paymntDescTextBox.Text.Contains("Reversal")
   && this.paymntDescTextBox.Enabled == false
   && this.amntNumericUpDown.Enabled == false))
      {
        if (this.pstPayListView.SelectedItems.Count <= 0)
        {
          cmnCde.showMsg("Please select the Payment being Reversed First!", 0);
          return;

        }
      }
      /* Processing a Payment
      * 1. Create Payment line pay.pay_itm_trnsctns for Pay Value Items
      * 2. Update Daily BalsItms for all balance items this Pay value Item feeds into
      * 3. Create Tmp GL Lines in a temp GL interface Table 
      * 4. Need to check whether any of its Balance Items disallows negative balance. 
      * If Not disallow this trans if it will lead to a negative balance on a Balance Item
      */

      if (!(this.paymntDescTextBox.Text.Contains("Reversal")
   && this.paymntDescTextBox.Enabled == false
   && this.amntNumericUpDown.Enabled == false))
      {
        if (this.doesPymntDteViolateFreq(this.prsnID
          , this.payItmID
          , this.paymntDateTextBox.Text) == true)
        {
          cmnCde.showMsg("The Payment Date violates the Item's Defined Pay Frequency!", 0);
          return;
        }
      }
      else if (this.getPymntRvrslTrnsID(
        long.Parse(this.pstPayListView.SelectedItems[0].SubItems[5].Text)) > 0)
      {
        cmnCde.showMsg("This Payment has been reversed already or\r\n is a reversal for another Transaction !", 0);
        return;
      }

      if (this.hsPrsnBnPaidItmMnl(this.prsnID
   , this.payItmID
   , this.paymntDateTextBox.Text, (double)this.amntNumericUpDown.Value) == true)
      {
        cmnCde.showMsg("Same Payment has been made for this Person on the same Date Already!", 0);
        return;
      }

      //if (!this.isPayTrnsValid())
      //{
      //  return;
      //}

      string dateStr = DateTime.ParseExact(
   cmnCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
   System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
      double nwAmnt = this.willItmBlsBeNgtv(
        this.prsnID
        , this.payItmID
        , (double)this.amntNumericUpDown.Value, this.paymntDateTextBox.Text);
      if (nwAmnt < 0)
      {
        cmnCde.showMsg("This transaction will cause a Balance Item\r\n" +
          "to Have Negative Balance and hence cannot be allowed!", 0);
        return;
      }


      if (cmnCde.showMsg("Are you sure you want to process this payment?", 1)
        == DialogResult.No)
      {
        cmnCde.showMsg("Operation Cancelled!", 4);
        return;
      }
      bool res = false;
      if (this.paymntDescTextBox.Text.Contains("Reversal")
        && this.paymntDescTextBox.Enabled == false
        && this.amntNumericUpDown.Enabled == false)
      {
        this.createPaymntLine(this.prsnID,
              this.payItmID,
              (double)this.amntNumericUpDown.Value, this.paymntDateTextBox.Text,
              "Manual Reversal", this.trnsTypComboBox.Text, -1, this.paymntDescTextBox.Text,
              int.Parse(this.crncyIDTextBox.Text), dateStr
              , "VALID", long.Parse(this.pstPayListView.SelectedItems[0].SubItems[5].Text)
              , this.glDateTextBox.Text);

        this.updateTrnsVldtyStatus(long.Parse(this.pstPayListView.SelectedItems[0].SubItems[5].Text),
          "VOID");
        //Update Balance Items
        this.updtBlsItms(this.prsnID
          , this.payItmID
          , (double)this.amntNumericUpDown.Value
          , this.paymntDateTextBox.Text, "Manual Reversal",
          long.Parse(this.pstPayListView.SelectedItems[0].SubItems[5].Text));

        this.processPayButton.Enabled = false;
        System.Windows.Forms.Application.DoEvents();
        bool isAnyRnng = true;
        int witcntr = 0;
        do
        {
          witcntr++;
          isAnyRnng = this.isThereANActvActnPrcss("8", "10 second");//Payments Import Process
          System.Windows.Forms.Application.DoEvents();
        }
        while (isAnyRnng == true);

        this.processPayButton.Enabled = true;
        System.Windows.Forms.Application.DoEvents();

        this.deletePymntGLInfcLns(long.Parse(this.pstPayListView.SelectedItems[0].SubItems[5].Text));

        long nwpaytrnsid = this.getPaymntTrnsID(
  this.prsnID
  , this.payItmID,
  (double)this.amntNumericUpDown.Value, this.paymntDateTextBox.Text,
  long.Parse(this.pstPayListView.SelectedItems[0].SubItems[5].Text));

        res = this.rvrsImprtdPymntIntrfcTrns(long.Parse(this.pstPayListView.SelectedItems[0].SubItems[5].Text)
          , nwpaytrnsid);

        this.payTrnsID = nwpaytrnsid;
        long rcvblHdrID = this.get_ScmRcvblsDocHdrID(this.invcHdrID, "Sales Invoice",
  cmnCde.Org_id);

        string rcvblDoctype = cmnCde.getGnrlRecNm("accb.accb_rcvbls_invc_hdr",
       "rcvbls_invc_hdr_id", "rcvbls_invc_type", rcvblHdrID);

        DialogResult dgres = cmnCde.showPymntDiag(
         false, true,
         this.rvrsPymntButton.Location.X + 15,
         180,
         (double)this.amntNumericUpDown.Value, this.curid,
         this.pyMthdID, "Customer Payments",
         this.cstmrID,
         this.cstmrSiteID,
         rcvblHdrID,
         rcvblDoctype, cmnCde, this.payTrnsID);
        res = true;
        /*res = this.sendToGLInterfaceMnl(this.prsnID,
    this.payItmID, this.crncyTextBox.Text,
         (double)this.amntNumericUpDown.Value, this.paymntDateTextBox.Text,
         this.paymntDescTextBox.Text,
         int.Parse(this.crncyIDTextBox.Text), dateStr, "Manual Reversal", this.glDateTextBox.Text);*/

      }
      else
      {
        this.createPaymntLine(this.prsnID,
      this.payItmID,
      (double)this.amntNumericUpDown.Value, this.paymntDateTextBox.Text,
      "Manual", this.trnsTypComboBox.Text, -1, this.paymntDescTextBox.Text,
      int.Parse(this.crncyIDTextBox.Text), dateStr
      , "VALID", -1, this.glDateTextBox.Text);
        this.payTrnsID = this.getPaymntTrnsID(this.prsnID,
      this.payItmID,
      (double)this.amntNumericUpDown.Value,
      this.paymntDateTextBox.Text, -1);
        //Update Balance Items
        this.updtBlsItms(this.prsnID
          , this.payItmID
          , (double)this.amntNumericUpDown.Value
          , this.paymntDateTextBox.Text, "Manual", -1);

        long rcvblHdrID = this.get_ScmRcvblsDocHdrID(this.invcHdrID, "Sales Invoice",
          cmnCde.Org_id);

        string rcvblDoctype = cmnCde.getGnrlRecNm("accb.accb_rcvbls_invc_hdr",
       "rcvbls_invc_hdr_id", "rcvbls_invc_type", rcvblHdrID);

        DialogResult dgres = cmnCde.showPymntDiag(
         false, false,
         this.rvrsPymntButton.Location.X + 15,
         180,
         (double)this.amntNumericUpDown.Value, this.curid,
         this.pyMthdID, "Customer Payments",
         this.cstmrID,
         this.cstmrSiteID,
         rcvblHdrID,
         rcvblDoctype, cmnCde, this.payTrnsID);
        res = true;
        //    res = this.sendToGLInterfaceMnl(this.prsnID,
        //this.payItmID, this.crncyTextBox.Text,
        //      (double)this.amntNumericUpDown.Value, this.paymntDateTextBox.Text,
        //      this.paymntDescTextBox.Text,
        //      int.Parse(this.crncyIDTextBox.Text), dateStr, "Manual", this.glDateTextBox.Text, -1);
      }

      if (res)
      {
        //cmnCde.showMsg("Payment Successfully Processed!", 3);
        //this.clearMnlPay();
        //this.populateTodyPymnts();
        //this.DialogResult = DialogResult.Cancel;
        //this.Close();
      }
      else
      {
        cmnCde.showMsg("Processing Payment Failed!", 4);
      }
      this.loadPstPayPanel();
    }

  //  bool prcsngPay = false;
  //  private void processRcvblsPymnt(string docTypes, double lnAmnt, string pymntTypeComboBox,
  //    string pymntCmmntsTextBox, long orgnlPymntID, string cardNumTextBox, string cardNameTextBox,
  //    string dteRcvdTextBox)
  //  {
  //    this.prcsngPay = true;
  //    //this.processPayButton.Enabled = false;
  //    //System.Windows.Forms.Application.DoEvents();
      
  //    //double lnAmnt = (double)this.amntRcvdNumUpDown.Value;
  //    long prepayDocID = -1;
  //    string prepayDocType = "";
  //    if (pymntTypeComboBox == "")
  //    {
  //      cmnCde.showMsg("Please indicate the payment Type!", 0);
  //      //this.processPayButton.Enabled = true;
  //      this.prcsngPay = false;
  //      return;
  //    }
  //    if (pymntCmmntsTextBox == "")
  //    {
  //      cmnCde.showMsg("Please indicate the Payment Remark/Comment!", 0);
  //      //this.processPayButton.Enabled = true;
  //      this.prcsngPay = false;
  //      return;
  //    }
  //    if (orgnlPymntID <= 0)
  //    {
  //      if ((pymntTypeComboBox.Contains("Check")
  //        || pymntTypeComboBox.Contains("Cheque"))
  //        && (cardNumTextBox == "" || cardNameTextBox == ""))
  //      {
  //        cmnCde.showMsg("Please Indicate the Card/Cheque Name and No. if Payment Type is Cheque!", 0);
  //        //this.processPayButton.Enabled = true;
  //        this.prcsngPay = false;
  //        return;
  //      }

  //      if (dteRcvdTextBox == "")
  //      {
  //        cmnCde.showMsg("Please indicate the Payment Date!", 0);
  //        //this.processPayButton.Enabled = true;
  //        this.prcsngPay = false;
  //        return;
  //      }
  //      if (this.amntRcvdNumUpDown.Value == 0)
  //      {
  //        cmnCde.showMsg("Please indicate the amount Given!", 0);
  //        //this.processPayButton.Enabled = true;
  //        this.prcsngPay = false;
  //        return;
  //      }
  //      if ((pymntTypeComboBox.Contains("Prepayment")
  //      || pymntTypeComboBox.Contains("Advance")))
  //      {
  //        if (this.prepayDocIDTextBox.Text == "" || this.prepayDocIDTextBox.Text == "-1")
  //        {
  //          cmnCde.showMsg("Please select the Prepayment you want to Apply First!", 0);
  //          //this.processPayButton.Enabled = true;
  //          this.prcsngPay = false;
  //          return;
  //        }
  //        else
  //        {
  //          decimal prepayAvlblAmnt = 0;
  //          prepayDocID = long.Parse(this.prepayDocIDTextBox.Text);
  //          if (docTypes == "Supplier Payments")
  //          {
  //            prepayAvlblAmnt = Decimal.Parse(cmnCde.getGnrlRecNm(
  //       "accb.accb_pybls_invc_hdr", "pybls_invc_hdr_id", "invoice_amount-invc_amnt_appld_elswhr",
  //       long.Parse(this.prepayDocIDTextBox.Text)));
  //            prepayDocType = cmnCde.getGnrlRecNm(
  //      "accb.accb_pybls_invc_hdr", "pybls_invc_hdr_id", "pybls_invc_type",
  //      long.Parse(this.prepayDocIDTextBox.Text));
  //          }
  //          else
  //          {
  //            prepayAvlblAmnt = Decimal.Parse(cmnCde.getGnrlRecNm(
  //       "accb.accb_rcvbls_invc_hdr", "rcvbls_invc_hdr_id", "invoice_amount-invc_amnt_appld_elswhr",
  //       long.Parse(this.prepayDocIDTextBox.Text)));
  //            prepayDocType = cmnCde.getGnrlRecNm(
  //      "accb.accb_rcvbls_invc_hdr", "rcvbls_invc_hdr_id", "rcvbls_invc_type",
  //      long.Parse(this.prepayDocIDTextBox.Text));
  //          }
  //          if (this.amntRcvdNumUpDown.Value > prepayAvlblAmnt)
  //          {
  //            cmnCde.showMsg("Applied Prepayment Amount Exceeds the Available Amount \r\n on the selected Prepayment Document!", 0);
  //            this.processPayButton.Enabled = true;
  //            this.prcsngPay = false;
  //            return;
  //          }
  //        }
  //      }
  //    }

  //    if (this.amntToPay == 0 && this.createPrepay == false)
  //    {
  //      cmnCde.showMsg("Cannot Repay a Fully Paid Document!", 0);
  //      this.processPayButton.Enabled = true;
  //      this.prcsngPay = false;
  //      return;
  //    }

  //    if (this.amntToPay < 0 && this.amntRcvdNumUpDown.Value > 0)
  //    {
  //      cmnCde.showMsg("Amount Given Must be Negative(Refund) \r\nif Amount to Pay is Negative(Refund)!", 0);
  //      this.processPayButton.Enabled = true;
  //      this.prcsngPay = false;
  //      return;
  //    }
  //    if (orgnlPymntID > 0)
  //    {
  //      if (this.isPymntRvrsdB4(orgnlPymntID))
  //      {
  //        cmnCde.showMsg("This Payment has been Reversed Already!", 0);
  //        this.processPayButton.Enabled = true;
  //        this.prcsngPay = false;
  //        return;
  //      }
  //    }

  //    if (this.createPrepay == true && this.spplrID <= 0)
  //    {
  //      cmnCde.showMsg("Cannot Create Advance Payment when Customer is not Specified!", 0);
  //      this.processPayButton.Enabled = true;
  //      this.prcsngPay = false;
  //      return;
  //    }
  //    if (this.intlPyTrnsID <= 0)
  //    {
  //      if (cmnCde.showMsg("Are you sure you want to PROCESS this Payment?" +
  //      "\r\nThis action cannot be undone!", 1) == DialogResult.No)
  //      {
  //        //cmnCde.showMsg("Operation Cancelled!", 4);
  //        this.processPayButton.Enabled = true;
  //        this.prcsngPay = false;
  //        return;
  //      }
  //    }

  //    if (this.createPrepay == true && this.spplrID > 0 && orgnlPymntID <= 0)
  //    {
  //      this.checkNCreateRcvblsHdr();
  //      this.dsablPayments = false;
  //      this.createPrepay = false;
  //      this.amntToPay = (double)this.amntPaidNumUpDown.Value;

  //      this.addPymntDiag_Load(this, e);
  //    }

  //    this.processPayButton.Enabled = false;
  //    double amntPaid = (double)this.amntPaidNumUpDown.Value;

  //    string dateStr = DateTime.ParseExact(
  // cmnCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
  // System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
  //    string dteRcvd = dteRcvdTextBox;
  //    if (dteRcvd.Length <= 11)
  //    {
  //      dteRcvd = dteRcvd + " 12:00:00";
  //    }
  //    string pymntBatchName = "";
  //    string docClsftn = "";
  //    string docNum = "";
  //    long pymntBatchID = -1;
  //    string glBatchPrfx = "";
  //    string glBatchSrc = "";
  //    if (this.docTypes == "Supplier Payments")
  //    {
  //      glBatchPrfx = "PYMNT_SPPLR-";
  //      glBatchSrc = "Payment for Payables Invoice";
  //      pymntBatchName = "SPPLR_PYMNT-" +
  //       DateTime.Parse(cmnCde.getFrmtdDB_Date_time()).ToString("yyMMdd-HHmmss")
  //                + "-" + cmnCde.getRandomInt(10, 100);

  //      /*cmnCde.getDB_Date_time().Substring(11, 8).Replace(":", "").Replace("-", "").Replace(" ", "") + "-" +
  //     this.getNewPymntBatchID().ToString().PadLeft(4, '0');*/

  //      docClsftn = cmnCde.getGnrlRecNm(
  //           "accb.accb_pybls_invc_hdr", "pybls_invc_hdr_id", "doc_tmplt_clsfctn",
  //           this.srcDocID);

  //      docNum = cmnCde.getGnrlRecNm(
  //     "accb.accb_pybls_invc_hdr", "pybls_invc_hdr_id", "pybls_invc_number",
  //     this.srcDocID);
  //    }
  //    else
  //    {
  //      glBatchPrfx = "PYMNT_CSTMR-";
  //      glBatchSrc = "Payment for Receivables Invoice";
  //      pymntBatchName = "CSTMR_PYMNT-" +
  //       DateTime.Parse(cmnCde.getFrmtdDB_Date_time()).ToString("yyMMdd-HHmmss")
  //                + "-" + cmnCde.getRandomInt(10, 100);

  //      /*cmnCde.getDB_Date_time().Substring(11, 8).Replace(":", "").Replace("-", "").Replace(" ", "") + "-" +
  //  this.getNewPymntBatchID().ToString().PadLeft(4, '0');*/

  //      docClsftn = cmnCde.getGnrlRecNm(
  //           "accb.accb_rcvbls_invc_hdr", "rcvbls_invc_hdr_id", "doc_tmplt_clsfctn",
  //           this.srcDocID);

  //      docNum = cmnCde.getGnrlRecNm(
  //     "accb.accb_rcvbls_invc_hdr", "rcvbls_invc_hdr_id", "rcvbls_invc_number",
  //     this.srcDocID);
  //    }

  //    pymntBatchID = cmnCde.getGnrlRecID("accb.accb_payments_batches",
  //     "pymnt_batch_name", "pymnt_batch_id", pymntBatchName, cmnCde.Org_id);
  //    if (pymntBatchID <= 0)
  //    {
  //      this.createPymntsBatch(cmnCde.Org_id, dteRcvd, dteRcvd,
  //        this.srcDocType, pymntBatchName, pymntBatchName, this.spplrID,
  //        this.pymntMthdID, this.docTypes, this.orgnlPymntBatchID, "VALID", docClsftn, "Unprocessed");

  //      if (this.orgnlPymntBatchID > 0)
  //      {
  //        this.updateBatchVldtyStatus(this.orgnlPymntBatchID, "VOID");
  //      }
  //    }
  //    else
  //    {
  //      cmnCde.showMsg("Payment Batch Could not be Created!\r\n Try Again Later!", 0);
  //      this.processPayButton.Enabled = true;
  //      this.prcsngPay = false;
  //      return;
  //    }
  //    pymntBatchID = cmnCde.getGnrlRecID("accb.accb_payments_batches",
  //      "pymnt_batch_name", "pymnt_batch_id", pymntBatchName, cmnCde.Org_id);

  //    string glBatchName = glBatchPrfx +
  //      DateTime.Parse(cmnCde.getFrmtdDB_Date_time()).ToString("yyMMdd-HHmmss")
  //               + "-" + cmnCde.getRandomInt(10, 100);

  //    /*cmnCde.getDB_Date_time().Substring(11, 8).Replace(":", "").Replace("-", "").Replace(" ", "") + "-" +
  // this.getNewBatchID().ToString().PadLeft(4, '0');*/
  //    long glBatchID = cmnCde.getGnrlRecID("accb.accb_trnsctn_batches",
  //      "batch_name", "batch_id", glBatchName, cmnCde.Org_id);

  //    if (glBatchID <= 0)
  //    {
  //      this.createBatch(cmnCde.Org_id, glBatchName,
  //        pymntCmmntsTextBox + " (" + docNum + ")",
  //        glBatchSrc, "VALID", this.orgnlGLBatchID, "0");
  //      if (this.orgnlGLBatchID > 0)
  //      {
  //        this.updateBatchVldtyStatus(this.orgnlGLBatchID, "VOID");
  //      }
  //    }
  //    else
  //    {
  //      cmnCde.showMsg("GL Batch Could not be Created!\r\n Try Again Later!", 0);
  //      this.processPayButton.Enabled = true;
  //      this.prcsngPay = false;
  //      return;
  //    }
  //    glBatchID = cmnCde.getGnrlRecID("accb.accb_trnsctn_batches",
  //      "batch_name", "batch_id", glBatchName, cmnCde.Org_id);

  //    long pymntID = -1;
  //    if (pymntBatchID > 0 && glBatchID > 0)
  //    {
  //      pymntID = this.getNewPymntLnID();
  //      this.createPymntDet(pymntID, pymntBatchID, this.pymntMthdID, amntPaid, this.entrdCurrID,
  //        (double)this.changeNumUpDown.Value,
  //        pymntCmmntsTextBox, this.srcDocType, this.srcDocID, dteRcvd
  //        , this.incrsDcrs2ComboBox.Text.Substring(0, 1), int.Parse(this.blcngAccntIDTextBox.Text)
  //        , this.incrsDcrs1ComboBox.Text.Substring(0, 1), int.Parse(this.chrgeAccntIDTextBox.Text), glBatchID,
  //        "VALID", orgnlPymntID, int.Parse(this.funcCurrIDTextBox.Text), int.Parse(this.accntCurrIDTextBox.Text),
  //        (double)this.funcCurRateNumUpDwn.Value, (double)this.accntCurRateNumUpDwn.Value,
  //        (double)this.funcCurAmntNumUpDwn.Value, (double)this.accntCurrNumUpDwn.Value, prepayDocID,
  //        prepayDocType, this.otherInfoTextBox.Text, cardNameTextBox, this.expDateTextBox.Text,
  //        cardNumTextBox, this.sigCodeTextBox.Text, this.bkgAtvtyStatusTextBox.Text, this.bkgDocNameTextBox.Text,
  //        this.intlPyTrnsID);

  //      if (orgnlPymntID > 0)
  //      {
  //        this.updtPymntsLnVldty(orgnlPymntID, "VOID");
  //      }
  //    }
  //    this.CreatePymntAccntngTrns(int.Parse(this.chrgeAccntIDTextBox.Text), glBatchID, this.incrsDcrs1ComboBox.Text.Substring(0, 1));
  //    this.CreatePymntAccntngTrns(int.Parse(this.blcngAccntIDTextBox.Text), glBatchID, this.incrsDcrs2ComboBox.Text.Substring(0, 1));
  //    if (this.get_Batch_CrdtSum(glBatchID) == this.get_Batch_DbtSum(glBatchID))
  //    {
  //      //double pymntsAmnt = this.getPyblsDocTtlPymnts(this.srcDocID, this.srcDocType);
  //      if (this.docTypes == "Supplier Payments")
  //      {
  //        this.updtPyblsDocAmntPaid(this.srcDocID, amntPaid);
  //        if (prepayDocID > 0)
  //        {
  //          this.updtPyblsDocAmntAppld(prepayDocID, lnAmnt);
  //          string pepyDocType = cmnCde.getGnrlRecNm(
  //      "accb.accb_pybls_invc_hdr", "pybls_invc_hdr_id", "pybls_invc_type",
  //      prepayDocID);
  //          if (pepyDocType == "Supplier Credit Memo (InDirect Refund)"
  //              || pepyDocType == "Supplier Debit Memo (InDirect Topup)")
  //          {
  //            this.updtPyblsDocAmntPaid(prepayDocID, lnAmnt);
  //          }
  //        }
  //        //this.reCalcPsSmmrys(this.srcDocID, this.srcDocType);
  //      }
  //      else
  //      {
  //        this.updtRcvblsDocAmntPaid(this.srcDocID, amntPaid);
  //        if (prepayDocID > 0)
  //        {
  //          this.updtRcvblsDocAmntAppld(prepayDocID, lnAmnt);
  //          string pepyDocType = cmnCde.getGnrlRecNm(
  //      "accb.accb_rcvbls_invc_hdr", "rcvbls_invc_hdr_id", "rcvbls_invc_type",
  //      prepayDocID);
  //          if (pepyDocType == "Customer Credit Memo (InDirect Topup)"
  //            || pepyDocType == "Customer Debit Memo (InDirect Refund)")
  //          {
  //            this.updtRcvblsDocAmntPaid(prepayDocID, lnAmnt);
  //          }
  //        }
  //        this.reCalcRcvblsSmmrys(this.srcDocID, this.srcDocType);
  //      }
  //      if (this.srcDocType == "Supplier Credit Memo (InDirect Refund)"
  //        || this.srcDocType == "Supplier Debit Memo (InDirect Topup)")
  //      {
  //        this.updtPyblsDocAmntAppld(this.srcDocID, amntPaid);
  //      }
  //      else if (this.srcDocType == "Customer Credit Memo (InDirect Topup)"
  //        || this.srcDocType == "Customer Debit Memo (InDirect Refund)")
  //      {
  //        this.updtRcvblsDocAmntAppld(this.srcDocID, amntPaid);
  //      }
  //      this.updtPymntBatchStatus(pymntBatchID, "Processed");
  //      this.updateBatchAvlblty(glBatchID, "1");

  //      if (this.srcDocType.Contains("Advance") && orgnlPymntID > 0
  //&& this.shdRcvblsDocBeCancelled(this.srcDocID) == true)
  //      {
  //        this.rcvblCanclltnProcess();
  //      }

  //      if (this.srcDocType.Contains("Advance")
  //        && this.pymntHistoryButton.Text.Contains("Show"))
  //      {
  //        this.processPayButton.Enabled = false;
  //        System.Windows.Forms.Application.DoEvents();
  //        this.pymntHistoryButton.PerformClick();
  //        this.processPayButton.Enabled = false;
  //        this.groupBox1.Enabled = false;
  //        this.groupBox2.Enabled = false;
  //        this.groupBox4.Enabled = false;
  //        this.prcsngPay = false;
  //        return;
  //      }
  //      //cmnCde.showMsg(orgnlPymntID + "/" + this.srcDocType +
  //      //  "/" + this.shdRcvblsDocBeCancelled(this.srcDocID).ToString(), 0);
  //    }
  //    else
  //    {
  //      cmnCde.showMsg(@"The GL Batch created is not Balanced!\r\nTransactions created will be reversed and deleted!", 0);
  //      this.deleteBatchTrns(glBatchID);
  //      this.deleteBatch(glBatchID, glBatchName);
  //      this.deletePymntsBatchNDet(pymntBatchID, pymntBatchName);
  //      //this.deletePymntsDet(pymntID);
  //      this.processPayButton.Enabled = true;
  //      this.prcsngPay = false;
  //      return;
  //    }

  //    this.prcsngPay = false;
  //  }
    #endregion

    private void glDateButton_Click(object sender, EventArgs e)
    {
      cmnCde.selectDate(ref this.glDateTextBox);
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      this.timer1.Enabled = false;
      System.Windows.Forms.Application.DoEvents();
      this.amntNumericUpDown.Focus();
      System.Windows.Forms.Application.DoEvents();
      this.amntNumericUpDown.Select(0, this.amntNumericUpDown.Value.ToString().Length);
    }

  }
}
