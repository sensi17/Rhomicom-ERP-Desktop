using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BasicPersonData.Classes;
using BasicPersonData.Dialogs;
using Npgsql;
using System.IO;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Drawing.Layout;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.VisualBasic;

using System.Drawing.Imaging;

using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using ThoughtWorks.QRCode.Codec.Util;

namespace BasicPersonData.Forms
{
    public partial class mainForm : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        #region "GLOBAL VARIABLES..."
        public CommonCode.CommonCodes cmCde = new CommonCode.CommonCodes();
        cadmaFunctions.NavFuncs myNav = new cadmaFunctions.NavFuncs();
        //public NpgsqlConnection gnrlSQLConn = new NpgsqlConnection();
        public Int64 usr_id = -1;
        public int[] role_st_id = new int[0];
        public Int64 lgn_num = -1;
        public int Og_id = -1;
        public string prsnitm_SQL1 = "";
        public string mspydt_SQL = "";
        string[] menuItems = { "Basic Person Data", "Curriculum Vitae",
        "Assignments-Basic", "Pay Items" };
        string[] menuImages = { "groupings.png", "staffs.png", "groupings.png", "staffs.png" };
        string curTabIndx = "";
        //Org Persons Panel Variables;
        Int64 prs_cur_indx = 0;
        bool is_last_prs = false;
        Int64 totl_prs = 0;
        long last_prs_num = 0;
        public string prs_SQL = "";
        public string prsDet_SQL = "";
        bool obey_prs_evnts = false;
        bool addPrsn = false;
        bool editPrsn = false;
        bool addPrsns = false;
        bool editPrsns = false;
        bool delPrsns = false;

        bool addBscs = false;
        bool editBscs = false;
        bool delBscs = false;

        bool vwPrs = false;
        bool vwCV = false;
        bool vwBscs = false;

        public string tmplt_SQL = "";
        public string tmpltDet_SQL = "";
        public string rltvs_SQL = "";
        public string prsntyp_SQL = "";
        public string ntnlty_SQL = "";
        public string educ_SQL = "";
        public string wrkExp_SQL = "";
        public string skill_SQL = "";
        public string div_SQL = "";
        public string site_SQL = "";
        public string spvsr_SQL = "";
        public string job_SQL = "";
        public string grd_SQL = "";
        public string pos_SQL = "";
        public string wkHr_SQL = "";
        public string gath_SQL = "";

        private string prsnTypeRsn = "";
        private string prsnTypeFurDet = "";
        private string vldDte1 = "";
        private string vldDte2 = "";
        //Payitems
        public string bank_SQL = "";
        bool vwBanks = false;
        bool addPyItmsPrs = false;
        bool editPyItmsPrs = false;
        bool delPyItmsPrs = false;
        bool vwPyItmsPrs = false;
        long pyitm_cur_indxPrs = 0;
        bool is_last_pyitmPrs = false;
        long totl_pyitmPrs = 0;
        long last_pyitm_numPrs = 0;
        public string pyitm_SQLPrs = "";
        bool obey_pyitm_evntsPrs = false;

        public string ext_inf_tbl_name = "";
        public string ext_inf_seq_name = "";
        public long row_pk_id = 0;
        public long table_id = 0;
        public bool canEdit = false;
        private long totl_vals = 0;
        private long cur_vals_idx = 0;
        private string vwSQLStmnt = "";
        private bool is_last_val = false;
        bool obeyEvnts = false;
        long last_vals_num = 0;

        bool obey_evnts = false;
        public bool txtChngd = false;
        public string srchWrd = "%";

        #endregion

        public mainForm()
        {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            this.obey_prs_evnts = false;
            this.accDndLabel.Visible = false;
            Global.myPrsn.Initialize();
            Global.mnFrm = this;
            //Global.mnFrm.cmCde.pgSqlConn = this.gnrlSQLConn;
            Global.mnFrm.cmCde.Login_number = this.lgn_num;
            Global.mnFrm.cmCde.Role_Set_IDs = this.role_st_id;
            Global.mnFrm.cmCde.User_id = this.usr_id;
            Global.mnFrm.cmCde.Org_id = this.Og_id;

            CommonCode.CommonCodes.lgnNum = this.lgn_num;
            CommonCode.CommonCodes.rlSetIDS = this.role_st_id;
            CommonCode.CommonCodes.uID = this.usr_id;
            CommonCode.CommonCodes.ogID = this.Og_id;

            Global.refreshRqrdVrbls();
            System.Windows.Forms.Application.DoEvents();
            System.Drawing.Color[] clrs = Global.mnFrm.cmCde.getColors();
            this.BackColor = clrs[0];
            for (int i = 0; i < this.prsnTabControl.TabPages.Count; i++)
            {
                this.prsnTabControl.TabPages[i].BackColor = clrs[0];
            }
            this.prsnInfoTabPage.BackColor = clrs[0];
            this.educPanel.BackColor = clrs[0];
            this.wrkExpPanel.BackColor = clrs[0];
            this.skillsPanel.BackColor = clrs[0];
            this.basicAsgnmntsPanel.BackColor = clrs[0];
            this.basicAsgnmnts1Panel.BackColor = clrs[0];
            this.basicAsgnmnts2Panel.BackColor = clrs[0];
            this.basicAsgnmnts3Panel.BackColor = clrs[0];
            this.moneyAsgnmntsPanel.BackColor = clrs[0];
            this.bankDetailsPanel.BackColor = clrs[0];
            this.othrInfoPanel.BackColor = clrs[0];
            this.extraDataTabPage.BackColor = clrs[0];

            //this.glsLabel1.TopFill = clrs[0];
            //this.glsLabel1.BackColor = clrs[0];
            //this.glsLabel1.BottomFill = clrs[1];
            //this.glsLabel2.TopFill = clrs[0];
            //this.glsLabel2.BackColor = clrs[0];
            //this.glsLabel2.BottomFill = clrs[1];
            //this.glsLabel3.TopFill = clrs[0];
            //this.glsLabel3.BackColor = clrs[0];
            //this.glsLabel3.BottomFill = clrs[1];
            //this.glsLabel4.TopFill = clrs[0];
            //this.glsLabel4.BackColor = clrs[0];
            //this.glsLabel4.BottomFill = clrs[1];
            //this.glsLabel5.TopFill = clrs[0];
            //this.glsLabel5.BackColor = clrs[0];
            //this.glsLabel5.BottomFill = clrs[1];
            //this.glsLabel6.TopFill = clrs[0];
            //this.glsLabel6.BackColor = clrs[0];
            //this.glsLabel6.BottomFill = clrs[1];
            //this.glsLabel7.TopFill = clrs[0];
            //this.glsLabel7.BackColor = clrs[0];
            //this.glsLabel7.BottomFill = clrs[1];
            this.glsLabel8.TopFill = clrs[0];
            this.glsLabel8.BackColor = clrs[0];
            this.glsLabel8.BottomFill = clrs[1];
            //this.glsLabel9.TopFill = clrs[0];
            //this.glsLabel9.BackColor = clrs[0];
            //this.glsLabel9.BottomFill = clrs[1];
            //this.glsLabel10.TopFill = clrs[0];
            //this.glsLabel10.BackColor = clrs[0];
            //this.glsLabel10.BottomFill = clrs[1];
            //this.glsLabel11.TopFill = clrs[0];
            //this.glsLabel11.BackColor = clrs[0];
            //this.glsLabel11.BottomFill = clrs[1];
            this.glsLabel12.TopFill = clrs[0];
            this.glsLabel12.BackColor = clrs[0];
            this.glsLabel12.BottomFill = clrs[1];
            System.Windows.Forms.Application.DoEvents();
            Global.myPrsn.loadMyRolesNMsgtyps();
            this.changeOrg();
            this.disableFormButtons();
            Global.createRqrdLOVs();
            System.Windows.Forms.Application.DoEvents();
            bool vwAct = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[0]);
            if (!vwAct)
            {
                this.Controls.Clear();
                this.Controls.Add(this.accDndLabel);
                this.accDndLabel.Visible = true;
                return;
            }
            this.createPrsExtrDataDsbl();
            this.fltrByComboBox.SelectedIndex = 4;

            this.prsnTypComboBox.Items.Clear();
            string aldPrsTyp = Global.getAllwdPrsnTyps();
            string extra3 = "";
            char[] t = { '\'' };
            aldPrsTyp = "'" + aldPrsTyp.Trim(t) + "'";
            if (aldPrsTyp != "'All'")
            {
                extra3 = @" and pssbl_value IN (" + aldPrsTyp + ")";
            }
            DataSet dtst = Global.getAllEnbldPssblVals("Person Types", extra3);
            this.prsnTypComboBox.Items.Add("All");
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.prsnTypComboBox.Items.Add(dtst.Tables[0].Rows[i][0].ToString());
            }

            this.prsnTypComboBox.SelectedIndex = 0;
            this.loadOrgPersons();
            this.obey_prs_evnts = true;
        }

        #region "GENERAL..."
        public void updtBlsItms(long prsn_id, long itm_id,
         double pay_amount, string trns_date, string trns_src, long orgnlTrnsID)
        {
            DataSet dtst = Global.getAllItmFeeds1(itm_id);
            double nwAmnt = 0;
            for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
            {
                double lstBals = 0;
                double scaleFctr = 1;
                double.TryParse(dtst.Tables[0].Rows[a][3].ToString(), out scaleFctr);
                if (dtst.Tables[0].Rows[a][2].ToString() == "Cumulative")
                {
                    lstBals = Global.getBlsItmLtstDailyBals(
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
                    lstBals = Global.getBlsItmDailyBals(long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
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
                long paytrnsid = Global.getPaymntTrnsID(
                prsn_id, itm_id,
                pay_amount, trns_date, orgnlTrnsID);

                bool hsBlsBnUpdtd = Global.hsPrsItmBlsBnUptd(paytrnsid,
                  trns_date, long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
                  prsn_id);
                long dailybalID = Global.getItmDailyBalsID(
                  long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
                  trns_date, prsn_id);

                if (hsBlsBnUpdtd == false)
                {
                    if (dailybalID <= 0)
                    {
                        Global.createItmBals(long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
                          lstBals, prsn_id, trns_date, -1);

                        if (dtst.Tables[0].Rows[a][2].ToString() == "Cumulative")
                        {
                            Global.updtItmDailyBalsCum(trns_date,
                            long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
                           prsn_id,
                           nwAmnt, paytrnsid);
                        }
                        else
                        {
                            Global.updtItmDailyBalsNonCum(trns_date,
                            long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
                           prsn_id,
                           nwAmnt, paytrnsid);
                        }

                    }
                    else
                    {
                        if (dtst.Tables[0].Rows[a][2].ToString() == "Cumulative")
                        {
                            Global.updtItmDailyBalsCum(trns_date,
                            long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
                           prsn_id,
                           nwAmnt, paytrnsid);
                        }
                        else
                        {
                            Global.updtItmDailyBalsNonCum(trns_date,
                            long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
                           prsn_id,
                           nwAmnt, paytrnsid);
                        }
                    }
                }
            }
        }

        public bool sendToGLInterfaceMnl(
    long prsn_id, long itm_id, string itm_uom, double pay_amnt,
    string trns_date, string trns_desc,
    int crncy_id, string dateStr, string trns_src, string glDate, long orgnlTrnsID)
        {
            try
            {
                long paytrnsid = Global.getPaymntTrnsID(
                prsn_id, itm_id,
                pay_amnt, trns_date, orgnlTrnsID);
                //Create GL Lines based on item's defined accounts
                string[] accntinf = new string[4];
                accntinf = Global.get_ItmAccntInfo(itm_id);

                if (itm_uom != "Number" && int.Parse(accntinf[1]) > 0 && int.Parse(accntinf[3]) > 0)
                {
                    double netamnt = 0;

                    netamnt = Global.mnFrm.cmCde.dbtOrCrdtAccntMultiplier(
                      int.Parse(accntinf[1]),
                      accntinf[0].Substring(0, 1)) * pay_amnt;

                    long py_dbt_ln = Global.getIntFcTrnsDbtLn(paytrnsid, pay_amnt);
                    long py_crdt_ln = Global.getIntFcTrnsCrdtLn(paytrnsid, pay_amnt);
                    if (Global.mnFrm.cmCde.dbtOrCrdtAccnt(int.Parse(accntinf[1]),
                      accntinf[0].Substring(0, 1)) == "Debit")
                    {
                        if (py_dbt_ln <= 0)
                        {
                            Global.createPymntGLIntFcLn(int.Parse(accntinf[1]),
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
                            Global.createPymntGLIntFcLn(int.Parse(accntinf[1]),
                            trns_desc,
                      0, glDate,
                      crncy_id, pay_amnt,
                      netamnt, paytrnsid, dateStr);
                        }
                    }
                    //Repeat same for balancing leg
                    netamnt = Global.mnFrm.cmCde.dbtOrCrdtAccntMultiplier(
                        int.Parse(accntinf[3]),
                        accntinf[2].Substring(0, 1)) * pay_amnt;
                    if (Global.mnFrm.cmCde.dbtOrCrdtAccnt(int.Parse(accntinf[3]),
                      accntinf[2].Substring(0, 1)) == "Debit")
                    {
                        if (py_dbt_ln <= 0)
                        {
                            Global.createPymntGLIntFcLn(int.Parse(accntinf[3]),
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
                            Global.createPymntGLIntFcLn(int.Parse(accntinf[3]),
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
                Global.mnFrm.cmCde.showMsg("Error Sending Payment to GL Interface" +
                  " " + ex.Message, 0);
                return false;
            }
        }

        public double willItmBlsBeNgtv(long prsn_id, long itm_id,
          double pay_amount, string trns_date)
        {
            DataSet dtst = Global.getAllItmFeeds1(itm_id);
            double nwAmnt = 0;
            for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
            {
                if (Global.doesPrsnHvItmPrs(prsn_id, long.Parse(dtst.Tables[0].Rows[a][0].ToString())) <= 0)
                {
                    string tstDte = "";
                    Global.doesPrsnHvItm(prsn_id, itm_id, trns_date, ref tstDte);
                    if (tstDte == "")
                    {
                        tstDte = "01-Jan-1900 00:00:00";
                    }
                    Global.createBnftsPrs(prsn_id,
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
                        nwAmnt = Global.getBlsItmLtstDailyBals(
                          long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
                          prsn_id, trns_date) - (pay_amount * scaleFctr);
                    }
                    else
                    {
                        nwAmnt = (pay_amount * scaleFctr)
                  + Global.getBlsItmLtstDailyBals(long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
                  prsn_id, trns_date);
                    }
                }
                else
                {
                    if (dtst.Tables[0].Rows[a][1].ToString() == "Subtracts")
                    {
                        nwAmnt = Global.getBlsItmDailyBals(long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
                          prsn_id, trns_date) - (pay_amount * scaleFctr);
                    }
                    else
                    {
                        nwAmnt = (pay_amount * scaleFctr)
                  + Global.getBlsItmDailyBals(long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
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

        public void updtBlsItmsRetro(long prsn_id, long itm_id,
         double pay_amount, string trns_date, string trns_src, long orgnlTrnsID)
        {
            DataSet dtst = Global.getAllItmFeeds1(itm_id);
            double nwAmnt = 0;
            for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
            {
                double lstBals = 0;
                double scaleFctr = 1;
                double.TryParse(dtst.Tables[0].Rows[a][3].ToString(), out scaleFctr);
                if (dtst.Tables[0].Rows[a][2].ToString() == "Cumulative")
                {
                    lstBals = Global.getBlsItmLtstDailyBalsRetro(
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
                    lstBals = Global.getBlsItmDailyBalsRetro(long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
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
                long paytrnsid = Global.getPaymntTrnsIDREtro(
                prsn_id, itm_id,
                pay_amount, trns_date, orgnlTrnsID);

                bool hsBlsBnUpdtd = Global.hsPrsItmBlsBnUptdRetro(paytrnsid,
                  trns_date, long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
                  prsn_id);
                long dailybalID = Global.getItmDailyBalsIDRetro(
                  long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
                  trns_date, prsn_id);

                if (hsBlsBnUpdtd == false)
                {
                    if (dailybalID <= 0)
                    {
                        Global.createItmBalsRetro(long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
                          lstBals, prsn_id, trns_date, -1);

                        if (dtst.Tables[0].Rows[a][2].ToString() == "Cumulative")
                        {
                            Global.updtItmDailyBalsCumRetro(trns_date,
                            long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
                           prsn_id,
                           nwAmnt, paytrnsid);
                        }
                        else
                        {
                            Global.updtItmDailyBalsNonCumRetro(trns_date,
                            long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
                           prsn_id,
                           nwAmnt, paytrnsid);
                        }

                    }
                    else
                    {
                        if (dtst.Tables[0].Rows[a][2].ToString() == "Cumulative")
                        {
                            Global.updtItmDailyBalsCumRetro(trns_date,
                            long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
                           prsn_id,
                           nwAmnt, paytrnsid);
                        }
                        else
                        {
                            Global.updtItmDailyBalsNonCumRetro(trns_date,
                            long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
                           prsn_id,
                           nwAmnt, paytrnsid);
                        }
                    }
                }
            }
        }

        public double willItmBlsBeNgtvRetro(long prsn_id, long itm_id,
        double pay_amount, string trns_date)
        {
            DataSet dtst = Global.getAllItmFeeds1(itm_id);
            double nwAmnt = 0;
            for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
            {
                if (Global.doesPrsnHvItmPrs(prsn_id, long.Parse(dtst.Tables[0].Rows[a][0].ToString())) <= 0)
                {
                    string tstDte = "";
                    Global.doesPrsnHvItm(prsn_id, itm_id, trns_date, ref tstDte);
                    if (tstDte == "")
                    {
                        tstDte = "01-Jan-1900 00:00:00";
                    }
                    Global.createBnftsPrs(prsn_id,
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
                        nwAmnt = Global.getBlsItmLtstDailyBalsRetro(
                          long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
                          prsn_id, trns_date) - (pay_amount * scaleFctr);
                    }
                    else
                    {
                        nwAmnt = (pay_amount * scaleFctr)
                  + Global.getBlsItmLtstDailyBalsRetro(long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
                  prsn_id, trns_date);
                    }
                }
                else
                {
                    if (dtst.Tables[0].Rows[a][1].ToString() == "Subtracts")
                    {
                        nwAmnt = Global.getBlsItmDailyBalsRetro(long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
                          prsn_id, trns_date) - (pay_amount * scaleFctr);
                    }
                    else
                    {
                        nwAmnt = (pay_amount * scaleFctr)
                  + Global.getBlsItmDailyBalsRetro(long.Parse(dtst.Tables[0].Rows[a][0].ToString()),
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

        private void loadCorrectPanel()
        {
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            if (this.curTabIndx == "prsnInfoTabPage")
            {
                string orgType = Global.mnFrm.cmCde.getPssblValNm(int.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
                  "org.org_details", "org_id", "org_typ_id", Global.mnFrm.cmCde.Org_id)));
                if (orgType.ToUpper() == "CHURCH")
                {
                    this.religionLabel.Text = "Place of Worship / Name of Service";
                }
                else
                {
                    this.religionLabel.Text = "Religion / Place of Worship";
                }
                this.loadPersInfPanel();
                this.loadPersExtDataPanel();
            }
            else if (this.curTabIndx == "extraDataTabPage")
            {
                this.loadPersExtDataPanel();
                this.loadPersInfPanel();
            }
            else if (this.curTabIndx == "educTabPage" || this.curTabIndx == "wrkExpTabPage" || this.curTabIndx == "skillTabPage")
            {
                this.loadPersCVPanel();
            }
            else if (this.curTabIndx == "bscAsgnTabPage" || this.curTabIndx == "bscAsgn1TabPage" || this.curTabIndx == "bscAsgn2TabPage" || this.curTabIndx == "bscAsgn3TabPage")
            {
                this.loadPersBscAssgmnts();
            }
            else if (this.curTabIndx == "otherInfoTabPage")
            {
                this.loadOthrInfPanel();
                //this.loadPersInfPanel();
            }
        }

        private void changeOrg()
        {
            //  if (this.crntOrgIDTextBox.Text == "-1"
            //|| this.crntOrgIDTextBox.Text == "")
            //  {
            //    this.crntOrgIDTextBox.Text = Global.mnFrm.cmCde.Org_id.ToString();
            //    this.crntOrgTextBox.Text = Global.mnFrm.cmCde.getOrgName(Global.mnFrm.cmCde.Org_id);
            //    Global.mnFrm.cmCde.getDBImageFile(this.crntOrgIDTextBox.Text + ".png",
            //     0, ref this.curOrgPictureBox);

            //    //   if (this.crntOrgIDTextBox.Text == "-1"
            //    //|| this.crntOrgIDTextBox.Text == "")
            //    //   {
            //    //     EventArgs e = new EventArgs();
            //    //     this.crntOrgButton_Click(this.crntOrgButton, e);
            //    //   }
            //  }
        }

        private void disableFormButtons()
        {
            this.vwPrs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[1]);
            this.vwCV = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[2]);
            this.vwBscs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[3]);
            this.vwPyItmsPrs = false;//Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[4]);
            this.vwBanks = false;//Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[16]);

            this.prsnTabControl.Controls.Clear();
            if (this.vwPrs == true)
            {
                this.prsnTabControl.Controls.Add(this.prsnInfoTabPage);
                this.prsnTabControl.Controls.Add(this.extraDataTabPage);
            }
            if (this.vwBscs == true)
            {
                this.prsnTabControl.Controls.Add(this.bscAsgnTabPage);
                //this.prsnTabControl.Controls.Add(this.bscAsgn1TabPage);
                //this.prsnTabControl.Controls.Add(this.bscAsgn2TabPage);
                //this.prsnTabControl.Controls.Add(this.bscAsgn3TabPage);
            }
            if (this.vwCV == true)
            {
                this.prsnTabControl.Controls.Add(this.educTabPage);
                //this.prsnTabControl.Controls.Add(this.wrkExpTabPage);
                //this.prsnTabControl.Controls.Add(this.skillTabPage);
            }

            if (this.vwPrs == true)
            {
                this.prsnTabControl.Controls.Add(this.otherInfoTabPage);
            }
            if (this.vwPyItmsPrs == true)
            {
                //this.prsnTabControl.Controls.Add(this.payItemsTabPage);
            }
            if (this.vwBanks == true)
            {
                //this.prsnTabControl.Controls.Add(this.banksTabPage);
            }
            if (this.prsnTabControl.TabPages.Count > 0)
            {
                this.curTabIndx = this.prsnTabControl.TabPages[0].Name;
            }
            bool vwSQL = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[5]);
            bool rcHstry = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[6]);

            this.prsnInfoTabPage.Enabled = this.vwPrs;
            this.educPanel.Enabled = this.vwCV;
            this.wrkExpPanel.Enabled = this.vwCV;
            this.skillsPanel.Enabled = this.vwCV;

            this.basicAsgnmnts3Panel.Enabled = this.vwBscs;
            this.basicAsgnmnts2Panel.Enabled = this.vwBscs;
            this.basicAsgnmnts1Panel.Enabled = this.vwBscs;
            this.basicAsgnmntsPanel.Enabled = this.vwBscs;

            this.moneyAsgnmntsPanel.Enabled = this.vwPyItmsPrs;
            this.bankDetailsPanel.Enabled = this.vwBanks;
            this.othrInfoPanel.Enabled = this.vwPrs;

            this.prsnInfoTabPage.Visible = this.vwPrs;
            this.educPanel.Visible = this.vwCV;
            this.wrkExpPanel.Visible = this.vwCV;
            this.skillsPanel.Visible = this.vwCV;

            this.basicAsgnmnts3Panel.Visible = this.vwBscs;
            this.basicAsgnmnts2Panel.Visible = this.vwBscs;
            this.basicAsgnmnts1Panel.Visible = this.vwBscs;
            this.basicAsgnmntsPanel.Visible = this.vwBscs;
            this.moneyAsgnmntsPanel.Visible = this.vwPyItmsPrs;
            this.bankDetailsPanel.Visible = this.vwBanks;
            this.othrInfoPanel.Visible = this.vwPrs;

            //Person's Details
            this.savePrsButton.Enabled = false;
            this.addPrsns = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[7]);
            this.addPrsButton.Enabled = this.addPrsns;
            this.addPrsnMenuItem.Enabled = this.addPrsns;
            this.imptPrsnExclTmpltMenuItem.Enabled = this.addPrsns;
            this.applyTmpltButton.Enabled = this.addPrsns;
            this.imprtNtnltyTmpMenuItem.Enabled = this.addPrsns;
            this.imprtRltvsMenuItem.Enabled = this.addPrsns;
            this.addNtnltyMenuItem.Enabled = this.addPrsns;
            this.applyAsgnTmpltButton.Enabled = this.addPrsns;
            this.rltvsButton.Enabled = this.addPrsns;

            this.editPrsns = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[8]);
            this.editPrsButton.Enabled = this.editPrsns;
            this.editPrsnMenuItem.Enabled = this.editPrsns;
            this.editNtnltyMenuItem.Enabled = this.editPrsns;
            this.changePrsPicButton.Enabled = this.editPrsns;
            this.canEdit = this.editPrsns;

            this.delPrsns = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[9]);
            this.deletePrsButton.Enabled = this.delPrsns;
            this.delPrsnMenuItem.Enabled = this.delPrsns;
            this.deleteNtnltyMenuItem.Enabled = this.delPrsns;

            this.viewSQLPrsnMenuItem.Enabled = vwSQL;
            this.recHstryPrsnMenuItem.Enabled = rcHstry;
            //CV Details
            this.saveEducButton.Enabled = false;
            this.saveWrkButton.Enabled = false;
            this.saveSkillButton.Enabled = false;

            this.addPrsns = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[7]);
            this.addEducButton.Enabled = this.addPrsns;
            this.addEducMenuItem.Enabled = this.addPrsns;
            this.imprtEducBkgMenuItem.Enabled = this.addPrsns;

            this.addWrkButton.Enabled = this.addPrsns;
            this.addWkExpMenuItem.Enabled = this.addPrsns;
            this.imprtWkExpMenuItem.Enabled = this.addPrsns;

            this.addSkillButton.Enabled = this.addPrsns;
            this.addSkllMenuItem.Enabled = this.addPrsns;
            this.imprtSkllTmpMenuItem.Enabled = this.addPrsns;

            this.editPrsns = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[8]);
            this.editEducButton.Enabled = this.editPrsns;
            this.editEducMenuItem.Enabled = this.editPrsns;

            this.editWrkButton.Enabled = this.editPrsns;
            this.editWkExpMenuItem.Enabled = this.editPrsns;

            this.editSkillButton.Enabled = this.editPrsns;
            this.editSkllMenuItem.Enabled = this.editPrsns;


            this.delPrsns = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[9]);
            this.delEducButton.Enabled = this.delPrsns;
            this.delEducMenuItem.Enabled = this.delPrsns;

            this.delWrkButton.Enabled = this.delPrsns;
            this.delWkExpMenuItem.Enabled = this.delPrsns;

            this.delSkillButton.Enabled = this.delPrsns;
            this.delSkllMenuItem.Enabled = this.delPrsns;

            this.viewSQLPrsnMenuItem.Enabled = vwSQL;
            this.recHstryPrsnMenuItem.Enabled = rcHstry;
            //Basic Assignments
            this.saveDivButton.Enabled = false;
            this.saveLocButton.Enabled = false;
            this.saveSprvsrButton.Enabled = false;
            this.saveJobButton.Enabled = false;
            this.saveGradeButton.Enabled = false;
            this.savePostnButton.Enabled = false;

            this.addBscs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[10]);
            this.addDivButton.Enabled = this.addBscs;
            this.addDivMenuItem.Enabled = this.addBscs;
            this.addLocButton.Enabled = this.addBscs;
            this.addSiteMenuItem.Enabled = this.addBscs;
            this.addSprvsrButton.Enabled = this.addBscs;
            this.addSpvsrMenuItem.Enabled = this.addBscs;
            this.addJobButton.Enabled = this.addBscs;
            this.addJobMenuItem.Enabled = this.addBscs;
            this.addGradeButton.Enabled = this.addBscs;
            this.addGradeMenuItem.Enabled = this.addBscs;
            this.addPostnButton.Enabled = this.addBscs;
            this.addPosMenuItem.Enabled = this.addBscs;
            this.grpsImptExclMenuItem.Enabled = this.addBscs;
            this.siteImptExclMenuItem.Enabled = this.addBscs;
            this.spvsrImptExclMenuItem.Enabled = this.addBscs;
            this.jobImptExclMenuItem.Enabled = this.addBscs;
            this.posImptExclMenuItem.Enabled = this.addBscs;
            this.gradeImptExclMenuItem.Enabled = this.addBscs;

            this.editBscs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[11]);
            this.editDivButton.Enabled = this.editBscs;
            this.editDivMenuItem.Enabled = this.editBscs;
            this.editLocButton.Enabled = this.editBscs;
            this.editSiteMenuItem.Enabled = this.editBscs;
            this.editSprvsrButton.Enabled = this.editBscs;
            this.editSpvsrMenuItem.Enabled = this.editBscs;
            this.editJobButton.Enabled = this.editBscs;
            this.editJobMenuItem.Enabled = this.editBscs;
            this.editGradeButton.Enabled = this.editBscs;
            this.editGradeMenuItem.Enabled = this.editBscs;
            this.editPostnButton.Enabled = this.editBscs;
            this.editPosMenuItem.Enabled = this.editBscs;

            this.delBscs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[12]);
            this.delDivButton.Enabled = this.delBscs;
            this.delDivMenuItem.Enabled = this.delBscs;
            this.delLocButton.Enabled = this.delBscs;
            this.delSiteMenuItem.Enabled = this.delBscs;
            this.delSprvsrButton.Enabled = this.delBscs;
            this.delSpvsrMenuItem.Enabled = this.delBscs;
            this.delJobButton.Enabled = this.delBscs;
            this.delJobMenuItem.Enabled = this.delBscs;
            this.delGradeButton.Enabled = this.delBscs;
            this.delGradeMenuItem.Enabled = this.delBscs;
            this.delPostnButton.Enabled = this.delBscs;
            this.delPosMenuItem.Enabled = this.delBscs;

            this.vwSQLDivMenuItem.Enabled = vwSQL;
            this.vwSQLSiteMenuItem.Enabled = vwSQL;
            this.vwSQLSpvsrMenuItem.Enabled = vwSQL;
            this.vwSQLJobMenuItem.Enabled = vwSQL;
            this.vwSQLGradeMenuItem.Enabled = vwSQL;
            this.vwSQLPosMenuItem.Enabled = vwSQL;

            this.rcHstryDivMenuItem.Enabled = rcHstry;
            this.rcHstrySiteMenuItem.Enabled = rcHstry;
            this.rcHstrySpvsrMenuItem.Enabled = rcHstry;
            this.rcHstryJobMenuItem.Enabled = rcHstry;
            this.rcHstryGradeMenuItem.Enabled = rcHstry;
            this.rcHstryPosMenuItem.Enabled = rcHstry;

            this.addPyItmsPrs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[13]);

            this.editPyItmsPrs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[14]);

            this.delPyItmsPrs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[15]);


            this.addPyItmsPrs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[13]);

            this.editPyItmsPrs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[14]);

            this.delPyItmsPrs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[15]);

        }
        #endregion

        #region "ORGANIZATION'S PERSONS..."
        private void loadPersInfPanel()
        {
            if (this.prsNamesListView.SelectedItems.Count == 1)
            {
                this.populatePrsNames(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
            }
            else
            {
                //this.populatePrsNames(-10000000010);
            }
        }

        private void loadPersExtDataPanel()
        {
            if (this.prsNamesListView.SelectedItems.Count == 1)
            {
                this.populatePrsExtrData(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
            }
            else
            {
                //this.populatePrsExtrData(-10000000010);
            }
        }
        private void loadPersCVPanel()
        {
            //if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[2]) == false)
            //{
            //  Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            //   " this action!\nContact your System Administrator!", 0);
            //  return;
            //}
            if (this.prsNamesListView.SelectedItems.Count == 1)
            {
                this.populateEduc(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
                this.populateWrkExp(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
                this.populateSkills(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
            }
            else
            {
                //this.populateEduc(-10000000010);
                //this.populateWrkExp(-10000000010);
                //this.populateSkills(-10000000010);
            }
        }

        private void loadPersBscAssgmnts()
        {
            //if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[3]) == false)
            //{
            //  Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            //   " this action!\nContact your System Administrator!", 0);
            //  return;
            //}
            if (this.prsNamesListView.SelectedItems.Count == 1)
            {
                this.populateDivs(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
                this.populateSites(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
                this.populateSpvsr(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
                this.populateJob(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
                this.populateGrades(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
                this.populatePositions(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
                //this.populateWkHrs(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
                //this.populateGath(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
            }
            else
            {
                //this.populateDivs(-10000000010);
                //this.populateSites(-10000000010);
                //this.populateSpvsr(-10000000010);
                //this.populateJob(-10000000010);
                //this.populateGrades(-10000000010);
                //this.populatePositions(-10000000010);
                //this.populateWkHrs(-10000000010);
                //this.populateGath(-10000000010);
            }
        }

        private void loadPersBnftsPanel()
        {
            //if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[4]) == false)
            //{
            //  Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            //   " this action!\nContact your System Administrator!", 0);
            //  return;
            //}
        }


        private void loadOthrInfPanel()
        {
            //if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[1]) == false)
            //{
            //  Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            //   " this action!\nContact your System Administrator!", 0);
            //  return;
            //}
            this.loadValPanel();
        }

        private void loadOrgPersons()
        {
            this.obey_prs_evnts = false;
            //if (!Global.mnFrm.cmCde.isThsMchnPrmtd())
            //{
            //  Global.mnFrm.cmCde.showMsg("This Machine is not Permitted to run this software!\r\nContact the Vendor for Assistance!", 4);
            //  return;
            //}
            if (this.searchInPrsComboBox.SelectedIndex < 0)
            {
                this.searchInPrsComboBox.SelectedIndex = 5;
            }
            if (this.searchForPrsTextBox.Text.Contains("%") == false)
            {
                this.searchForPrsTextBox.Text = "%" + this.searchForPrsTextBox.Text.Replace(" ", "%") + "%";
            }
            if (this.searchForPrsTextBox.Text == "%%")
            {
                this.searchForPrsTextBox.Text = "%";
            }
            if (this.orderByComboBox.SelectedIndex < 0)
            {
                this.orderByComboBox.SelectedIndex = 3;
            }

            int dsply = 0;
            if (this.dsplySizePrsComboBox.Text == ""
             || int.TryParse(this.dsplySizePrsComboBox.Text, out dsply) == false)
            {
                this.dsplySizePrsComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            this.is_last_prs = false;
            this.totl_prs = Global.mnFrm.cmCde.Big_Val;
            this.getOrgPrsData();
            this.obey_prs_evnts = true;
        }

        private void getOrgPrsData()
        {
            this.updtPrsTotals();
            this.populatePrs();
            this.updtPrsNavLabels();
        }

        private void updtPrsTotals()
        {
            Global.mnFrm.cmCde.navFuncts.FindNavigationIndices(
             int.Parse(this.dsplySizePrsComboBox.Text), this.totl_prs);
            if (this.prs_cur_indx >= Global.mnFrm.cmCde.navFuncts.totalGroups)
            {
                this.prs_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            if (this.prs_cur_indx < 0)
            {
                this.prs_cur_indx = 0;
            }
            Global.mnFrm.cmCde.navFuncts.currentNavigationIndex = this.prs_cur_indx;
        }

        private void updtPrsNavLabels()
        {
            this.moveFirstPrsButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveFirstBtnStatus();
            this.movePreviousPrsButton.Enabled = Global.mnFrm.cmCde.navFuncts.movePrevBtnStatus();
            this.moveNextPrsButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveNextBtnStatus();
            this.moveLastPrsButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveLastBtnStatus();
            this.positionPrsTextBox.Text = Global.mnFrm.cmCde.navFuncts.displayedRecordsNumbers();
            if (this.is_last_prs == true ||
             this.totl_prs != Global.mnFrm.cmCde.Big_Val)
            {
                this.totalRecPrsLabel.Text = Global.mnFrm.cmCde.navFuncts.totalRecordsLabel();
            }
            else
            {
                this.totalRecPrsLabel.Text = "of Total";
            }
        }

        private void populatePrs()
        {
            this.clearPrsInfo();
            this.disablePrsEdit();
            this.obey_prs_evnts = false;
            DataSet dtst = Global.get_Org_Persons(this.searchForPrsTextBox.Text,
             this.searchInPrsComboBox.Text, this.prs_cur_indx,
             int.Parse(this.dsplySizePrsComboBox.Text)
             , Global.mnFrm.cmCde.Org_id,
            this.searchAllOrgCheckBox.Checked, this.orderByComboBox.Text,
            this.prsnTypComboBox.Text, this.fltrByComboBox.Text);

            this.obey_prs_evnts = false;
            this.prsNamesListView.Items.Clear();
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.last_prs_num = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
                ListViewItem nwItem = new ListViewItem(new string[] {
        (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
        dtst.Tables[0].Rows[i][1].ToString(), dtst.Tables[0].Rows[i][2].ToString(),
        dtst.Tables[0].Rows[i][0].ToString(), dtst.Tables[0].Rows[i][3].ToString()});
                this.prsNamesListView.Items.Add(nwItem);
            }

            this.correctPrsNavLbls(dtst);
            if (this.prsNamesListView.Items.Count > 0)
            {
                this.obey_prs_evnts = true;
                this.prsNamesListView.Items[0].Selected = true;
            }
            //else
            //{
            //  this.clearPrsInfo();
            //  this.disablePrsEdit();
            //}
            this.obey_prs_evnts = true;
        }

        private void populatePrsNames(long prsnID)
        {
            this.clearPrsInfo();
            if (this.editPrsn == false && this.addPrsn == false)
            {
                this.disablePrsEdit();
            }
            this.obey_prs_evnts = false;
            DataSet dtst = Global.get_Prs_Names_Nos(prsnID);
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.prsnIDTextBox.Text = prsnID.ToString();
                this.locIDTextBox.Text = dtst.Tables[0].Rows[i][1].ToString();
                this.titleTextBox.Text = dtst.Tables[0].Rows[i][2].ToString();
                this.firstNameTextBox.Text = dtst.Tables[0].Rows[i][3].ToString();
                this.surnameTextBox.Text = dtst.Tables[0].Rows[i][4].ToString();
                this.otherNamesTextBox.Text = dtst.Tables[0].Rows[i][5].ToString();
                this.prsOrgIDTextBox.Text = dtst.Tables[0].Rows[i][6].ToString();
                this.prsOrgTextBox.Text = Global.mnFrm.cmCde.getOrgName(int.Parse(this.prsOrgIDTextBox.Text));

                this.linkedFirmIDTextBox.Text = dtst.Tables[0].Rows[i][21].ToString();
                this.linkedFirmTextBox.Text = dtst.Tables[0].Rows[i][22].ToString();

                this.linkedSiteIDTextBox.Text = dtst.Tables[0].Rows[i][23].ToString();
                this.linkedSiteTextBox.Text = dtst.Tables[0].Rows[i][24].ToString();

                this.resAddrsTextBox.Text = dtst.Tables[0].Rows[i][12].ToString();
                this.pstlAddrsTextBox.Text = dtst.Tables[0].Rows[i][13].ToString();
                this.emailTextBox.Text = dtst.Tables[0].Rows[i][14].ToString();
                this.telTextBox.Text = dtst.Tables[0].Rows[i][15].ToString();
                this.moblTextBox.Text = dtst.Tables[0].Rows[i][16].ToString();
                this.faxTextBox.Text = dtst.Tables[0].Rows[i][17].ToString();
                this.genderTextBox.Text = dtst.Tables[0].Rows[i][7].ToString();
                this.maritalStatusTextBox.Text = dtst.Tables[0].Rows[i][8].ToString();
                this.dobTextBox.Text = dtst.Tables[0].Rows[i][9].ToString();
                this.ageLabel.Text = Global.computePrsnAge(this.dobTextBox.Text);
                this.ntnltyTextBox.Text = dtst.Tables[0].Rows[i][20].ToString();
                this.religionTextBox.Text = dtst.Tables[0].Rows[i][11].ToString();
                this.pobTextBox.Text = dtst.Tables[0].Rows[i][10].ToString();
                this.hometownTextBox.Text = dtst.Tables[0].Rows[i][19].ToString();
                this.qrCodePictureBox.Image = BasicPersonData.Properties.Resources.actions_document_preview;
                this.imgTextBox.Text = dtst.Tables[0].Rows[i][18].ToString();
            }
            this.populatePrsnType(prsnID);
            this.populateNatnlty(prsnID);
            this.obey_prs_evnts = true;
        }

        //string[] extrData = new string[50];
        string[] colDsplyTyp = new string[50];//T or D
        int[] tblrNoCols = new int[50];
        Button[] btnButtons = new Button[50];
        TextBox[] txtBxes = new TextBox[50];
        Label[] lbls = new Label[50];
        ListView[] lstvws = new ListView[50];
        DataGridView[] grdvw = new DataGridView[50];
        object[] cntrlsCreated = new object[50];

        private void fillWithNull()
        {
            for (int i = 0; i < 50; i++)
            {
                txtBxes[i] = null;
                lstvws[i] = null;
                grdvw[i] = null;
                btnButtons[i] = null;
            }
        }

        private void populatePrsExtrData(long prsnID)
        {
            if (this.editPrsn == false && this.addPrsn == false)
            {

            }
            this.clearPrsExtrData();
            this.obey_prs_evnts = false;
            DataSet dtst = Global.get_PrsExtrData(prsnID);
            int cnt = dtst.Tables[0].Rows.Count;
            if (cnt > 0)
            {
                for (int i = 0; i < 50; i++)
                {
                    if (this.txtBxes[i] != null)
                    {
                        this.txtBxes[i].Text = dtst.Tables[0].Rows[0][i + 1].ToString();
                        if (this.lbls[i] != null)
                        {
                            if (this.lbls[i].Tag.ToString() == "Tabular")
                            {
                                this.grdvw[i].BringToFront();
                                this.grdvw[i].Rows.Clear();
                                char[] w = { '|' };
                                char[] v = { '~' };
                                string[] arry1 = this.txtBxes[i].Text.Split(w, StringSplitOptions.RemoveEmptyEntries);
                                for (int y = 0; y < arry1.Length; y++)
                                {
                                    int ttlrws = this.grdvw[i].Rows.Count;
                                    this.grdvw[i].Rows.Insert(ttlrws - 1, 1);
                                    string[] arry2 = arry1[y].Split(v, StringSplitOptions.RemoveEmptyEntries);
                                    for (int z = 0; z < arry2.Length; z++)
                                    {
                                        this.grdvw[i].Rows[ttlrws - 1].Cells[z].Value = arry2[z];
                                    }
                                }

                            }
                        }
                    }
                }
            }
            this.obey_prs_evnts = true;
        }

        private void populatePrsnType(long prsnID)
        {
            this.prsnTypTextBox.Text = "";
            this.prsnTypeRsn = "";
            this.prsnTypeFurDet = "";
            this.vldDte1 = "";
            this.vldDte2 = "";
            this.reasonTextBox.Text = "";
            this.furtherDetTextBox.Text = "";
            this.vldStrtDteTextBox.Text = "";
            this.vldEndDteTextBox.Text = "";

            DataSet dtst = Global.getLatestPrsnType(prsnID);
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.prsnTypTextBox.Text = dtst.Tables[0].Rows[i][0].ToString();
                this.prsnTypeRsn = dtst.Tables[0].Rows[i][1].ToString();
                this.prsnTypeFurDet = dtst.Tables[0].Rows[i][2].ToString();
                this.vldDte1 = dtst.Tables[0].Rows[i][3].ToString();
                this.vldDte2 = dtst.Tables[0].Rows[i][4].ToString();
                this.reasonTextBox.Text = this.prsnTypeRsn;
                this.furtherDetTextBox.Text = this.prsnTypeFurDet;
                this.vldStrtDteTextBox.Text = this.vldDte1;
                if (this.vldDte2 == "31-Dec-4000")
                {
                    this.vldEndDteTextBox.Text = "";
                }
                else
                {
                    this.vldEndDteTextBox.Text = this.vldDte2;
                }
            }
        }

        private void correctPrsNavLbls(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.prs_cur_indx == 0 && totlRecs == 0)
            {
                this.is_last_prs = true;
                this.totl_prs = 0;
                this.last_prs_num = 0;
                this.prs_cur_indx = 0;
                this.updtPrsTotals();
                this.updtPrsNavLabels();
            }
            else if (this.totl_prs == Global.mnFrm.cmCde.Big_Val
          && totlRecs < int.Parse(this.dsplySizePrsComboBox.Text))
            {
                this.totl_prs = this.last_prs_num;
                if (totlRecs == 0)
                {
                    this.prs_cur_indx -= 1;
                    this.updtPrsTotals();
                    this.populatePrs();
                }
                else
                {
                    this.updtPrsTotals();
                }
            }
        }

        private void clearPrsInfo()
        {
            this.obey_prs_evnts = false;
            //this.savePrsButton.Enabled = false;
            this.addPrsButton.Enabled = this.addPrsns;
            this.editPrsButton.Enabled = this.editPrsns;
            this.deletePrsButton.Enabled = this.delPrsns;
            this.prsnIDTextBox.Text = "-1";
            this.locIDTextBox.Text = "";
            this.titleTextBox.Text = "";
            this.firstNameTextBox.Text = "";
            this.surnameTextBox.Text = "";
            this.otherNamesTextBox.Text = "";

            this.resAddrsTextBox.Text = "";
            this.pstlAddrsTextBox.Text = "";
            this.emailTextBox.Text = "";
            this.telTextBox.Text = "";
            this.moblTextBox.Text = "";
            this.faxTextBox.Text = "";

            //this.prsnTypeTextBox1.Text = "";
            this.prsnTypeFurDet = "";
            this.prsnTypeRsn = "";
            this.vldDte1 = "";
            this.vldDte2 = "";

            this.prsnTypTextBox.Text = "";
            this.reasonTextBox.Text = "";
            this.furtherDetTextBox.Text = "";
            this.vldStrtDteTextBox.Text = "";
            this.vldEndDteTextBox.Text = "";

            this.prsOrgIDTextBox.Text = "-1";
            this.prsOrgTextBox.Text = "";
            this.linkedFirmIDTextBox.Text = "-1";
            this.linkedFirmTextBox.Text = "";
            this.linkedSiteIDTextBox.Text = "-1";
            this.linkedSiteTextBox.Text = "";
            this.genderTextBox.Text = "";
            this.maritalStatusTextBox.Text = "";
            this.dobTextBox.Text = "";
            this.pobTextBox.Text = "";
            this.ntnltyTextBox.Text = "";
            this.imgTextBox.Text = "";
            this.religionTextBox.Text = "";
            this.hometownTextBox.Text = "";
            this.nationalityListView.Items.Clear();
            this.iDPrfxComboBox.Items.Clear();

            this.qrCodePictureBox.Image = BasicPersonData.Properties.Resources.staffs;
            this.obey_prs_evnts = true;
        }

        private void prpareForPrsEdit()
        {
            this.savePrsButton.Enabled = true;
            this.locIDTextBox.ReadOnly = false;
            this.locIDTextBox.BackColor = System.Drawing.Color.FromArgb(255, 255, 118);
            this.titleTextBox.ReadOnly = false;
            this.titleTextBox.BackColor = System.Drawing.Color.FromArgb(255, 255, 118);
            this.firstNameTextBox.ReadOnly = false;
            this.firstNameTextBox.BackColor = System.Drawing.Color.FromArgb(255, 255, 118);
            this.surnameTextBox.ReadOnly = false;
            this.surnameTextBox.BackColor = System.Drawing.Color.FromArgb(255, 255, 118);
            this.otherNamesTextBox.ReadOnly = false;
            this.otherNamesTextBox.BackColor = System.Drawing.Color.White;

            this.resAddrsTextBox.ReadOnly = false;
            this.resAddrsTextBox.BackColor = System.Drawing.Color.White;
            this.pstlAddrsTextBox.ReadOnly = false;
            this.pstlAddrsTextBox.BackColor = System.Drawing.Color.White;
            this.emailTextBox.ReadOnly = false;
            this.emailTextBox.BackColor = System.Drawing.Color.White;
            this.telTextBox.ReadOnly = false;
            this.telTextBox.BackColor = System.Drawing.Color.White;
            this.moblTextBox.ReadOnly = false;
            this.moblTextBox.BackColor = System.Drawing.Color.White;
            this.faxTextBox.ReadOnly = false;
            this.faxTextBox.BackColor = System.Drawing.Color.White;

            this.genderTextBox.ReadOnly = false;
            this.genderTextBox.BackColor = System.Drawing.Color.FromArgb(255, 255, 118);
            this.maritalStatusTextBox.ReadOnly = false;
            this.maritalStatusTextBox.BackColor = System.Drawing.Color.FromArgb(255, 255, 118);
            this.dobTextBox.ReadOnly = false;
            this.dobTextBox.BackColor = System.Drawing.Color.FromArgb(255, 255, 118);
            this.pobTextBox.ReadOnly = false;
            this.pobTextBox.BackColor = System.Drawing.Color.White;
            this.ntnltyTextBox.ReadOnly = false;
            this.ntnltyTextBox.BackColor = System.Drawing.Color.FromArgb(255, 255, 118);
            this.imgTextBox.ReadOnly = false;
            this.imgTextBox.BackColor = System.Drawing.Color.White;
            this.hometownTextBox.ReadOnly = false;
            this.hometownTextBox.BackColor = System.Drawing.Color.White;
            this.religionTextBox.ReadOnly = false;
            this.religionTextBox.BackColor = System.Drawing.Color.White;
            //this.prsnTypeTextBox1.ReadOnly = true;
            //this.prsnTypeTextBox1.BackColor = System.Drawing.Color.FromArgb(255, 255, 118);

            this.prsnTypTextBox.ReadOnly = false;
            this.prsnTypTextBox.BackColor = System.Drawing.Color.FromArgb(255, 255, 118);

            this.reasonTextBox.ReadOnly = false;
            this.reasonTextBox.BackColor = System.Drawing.Color.FromArgb(255, 255, 118);

            this.furtherDetTextBox.ReadOnly = false;
            this.furtherDetTextBox.BackColor = System.Drawing.Color.White;

            this.vldStrtDteTextBox.ReadOnly = false;
            this.vldStrtDteTextBox.BackColor = System.Drawing.Color.FromArgb(255, 255, 118);

            this.vldEndDteTextBox.ReadOnly = false;
            this.vldEndDteTextBox.BackColor = System.Drawing.Color.White;

            this.prsOrgTextBox.ReadOnly = true;
            this.linkedFirmTextBox.ReadOnly = true;
            this.linkedFirmTextBox.BackColor = System.Drawing.Color.White;
            this.linkedSiteTextBox.ReadOnly = true;
            this.linkedSiteTextBox.BackColor = System.Drawing.Color.White;

            this.iDPrfxComboBox.Items.Clear();
            DataSet dtst = Global.getAllEnbldPssblVals("Person ID No. Prefix", "");
            this.iDPrfxComboBox.Items.Add("");
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.iDPrfxComboBox.Items.Add(dtst.Tables[0].Rows[i][0].ToString());
            }
            if (this.editPrsns == false)
            {
                this.extInfoDataGridView.ReadOnly = true;
                this.extInfoDataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.Gainsboro;
            }
            else
            {
                this.extInfoDataGridView.ReadOnly = false;
                this.extInfoDataGridView.Columns[0].ReadOnly = true;
                this.extInfoDataGridView.Columns[2].ReadOnly = false;
                this.extInfoDataGridView.Columns[0].DefaultCellStyle.BackColor = System.Drawing.Color.Gainsboro;
                this.extInfoDataGridView.Columns[2].DefaultCellStyle.BackColor = System.Drawing.Color.White;

                this.extInfoDataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            }
            this.enblDsblGridVws(true);
            if (this.addPrsn == true)
            {
                if (this.iDPrfxComboBox.Items.Count > 0)
                {
                    this.iDPrfxComboBox.SelectedIndex = 0;
                }
            }
        }

        private void disablePrsEdit()
        {
            this.addPrsn = false;
            this.editPrsn = false;
            this.locIDTextBox.ReadOnly = true;
            this.locIDTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.titleTextBox.ReadOnly = true;
            this.titleTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.firstNameTextBox.ReadOnly = true;
            this.firstNameTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.surnameTextBox.ReadOnly = true;
            this.surnameTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.otherNamesTextBox.ReadOnly = true;
            this.otherNamesTextBox.BackColor = System.Drawing.Color.WhiteSmoke;

            this.resAddrsTextBox.ReadOnly = true;
            this.resAddrsTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pstlAddrsTextBox.ReadOnly = true;
            this.pstlAddrsTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.emailTextBox.ReadOnly = true;
            this.emailTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.telTextBox.ReadOnly = true;
            this.telTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.moblTextBox.ReadOnly = true;
            this.moblTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.faxTextBox.ReadOnly = true;
            this.faxTextBox.BackColor = System.Drawing.Color.WhiteSmoke;

            this.genderTextBox.ReadOnly = true;
            this.genderTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.maritalStatusTextBox.ReadOnly = true;
            this.maritalStatusTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dobTextBox.ReadOnly = true;
            this.dobTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pobTextBox.ReadOnly = true;
            this.pobTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ntnltyTextBox.ReadOnly = true;
            this.ntnltyTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.imgTextBox.ReadOnly = true;
            this.imgTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.hometownTextBox.ReadOnly = true;
            this.hometownTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.religionTextBox.ReadOnly = true;
            this.religionTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            //this.prsnTypeTextBox1.ReadOnly = true;
            //this.prsnTypeTextBox1.BackColor = System.Drawing.Color.WhiteSmoke;

            this.prsnTypTextBox.ReadOnly = true;
            this.prsnTypTextBox.BackColor = System.Drawing.Color.WhiteSmoke;

            this.reasonTextBox.ReadOnly = true;
            this.reasonTextBox.BackColor = System.Drawing.Color.WhiteSmoke;

            this.furtherDetTextBox.ReadOnly = true;
            this.furtherDetTextBox.BackColor = System.Drawing.Color.WhiteSmoke;

            this.vldStrtDteTextBox.ReadOnly = true;
            this.vldStrtDteTextBox.BackColor = System.Drawing.Color.WhiteSmoke;

            this.vldEndDteTextBox.ReadOnly = true;
            this.vldEndDteTextBox.BackColor = System.Drawing.Color.WhiteSmoke;

            this.prsOrgTextBox.ReadOnly = true;
            this.linkedFirmTextBox.ReadOnly = true;
            this.linkedFirmTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.linkedSiteTextBox.ReadOnly = true;
            this.linkedSiteTextBox.BackColor = System.Drawing.Color.WhiteSmoke;

            this.extInfoDataGridView.ReadOnly = true;
            this.extInfoDataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.savePrsButton.Enabled = false;
            this.addPrsn = false;
            this.editPrsn = false;
            this.editPrsButton.Enabled = this.editPrsns;
            this.addPrsButton.Enabled = this.addPrsns;
            this.deletePrsButton.Enabled = this.delPrsns;
            this.enblDsblGridVws(false);
            this.editPrsButton.Text = "EDIT";
            this.editPrsnMenuItem.Text = "Edit Person";
        }

        private void clearPrsExtrData()
        {
            this.obey_prs_evnts = false;
            for (int i = 0; i < 50; i++)
            {
                if (this.txtBxes[i] != null)
                {
                    this.txtBxes[i].Text = "";
                }
                if (this.lbls[i] != null)
                {
                    if (this.lbls[i].Tag.ToString() == "Tabular")
                    {
                        this.grdvw[i].Rows.Clear();
                    }
                }
            }
            this.obey_prs_evnts = true;
        }

        private void savePrnsExtrData(long prsnID)
        {
            long extrDataID = -1;
            long.TryParse(Global.mnFrm.cmCde.getGnrlRecNm("prs.prsn_extra_data", "person_id", "extra_data_id", prsnID), out extrDataID);
            string[] exdta = new string[50];
            for (int i = 0; i < 50; i++)
            {
                if (this.txtBxes[i] != null)
                {
                    if (this.txtBxes[i].BackColor == System.Drawing.Color.FromArgb(255, 255, 128))
                    {
                        if (this.txtBxes[i].Text == "")
                        {
                            Global.mnFrm.cmCde.showMsg("Please fill all Required Fields!", 0);
                            this.txtBxes[i].Focus();
                            return;
                        }
                    }
                    exdta[i] = this.txtBxes[i].Text;
                }
                else
                {
                    exdta[i] = "";
                }
            }
            if (extrDataID > 0)
            {
                //Update
                Global.updatePrsnExtrData(prsnID, exdta);
            }
            else
            {
                //Insert
                Global.createPrsnExtrData(prsnID, exdta);
            }
            Global.mnFrm.cmCde.showMsg("Record Saved!", 3);
        }

        private void trnsfrDataToCntrls()
        {

        }

        private void disableExtrDataEdit()
        {
            for (int i = 0; i < 50; i++)
            {
                if (this.txtBxes[i] != null)
                {
                    this.txtBxes[i].ReadOnly = true;
                    this.txtBxes[i].BackColor = System.Drawing.Color.WhiteSmoke;
                    if (this.lbls[i] != null)
                    {
                        if (this.lbls[i].Tag.ToString() == "Tabular")
                        {
                            this.txtBxes[i].SendToBack();
                            this.grdvw[i].BringToFront();
                            this.grdvw[i].ReadOnly = true;
                        }
                    }
                }
            }
        }

        private void prpareForExtrDataEdit()
        {
            for (int i = 0; i < 50; i++)
            {
                if (this.txtBxes[i] != null)
                {
                    int colno = int.Parse(this.txtBxes[i].Tag.ToString());
                    string lovnm = Global.get_PrsExtrDataPrpty("attchd_lov_name", colno, Global.mnFrm.cmCde.Org_id);
                    string datatyp = Global.get_PrsExtrDataPrpty("column_data_type", colno, Global.mnFrm.cmCde.Org_id);
                    string isRqrd = Global.get_PrsExtrDataPrpty("is_required", colno, Global.mnFrm.cmCde.Org_id);
                    this.txtBxes[i].ReadOnly = false;
                    if (isRqrd == "1")
                    {
                        this.txtBxes[i].BackColor = System.Drawing.Color.FromArgb(255, 255, 128);
                    }
                    else
                    {
                        this.txtBxes[i].BackColor = System.Drawing.Color.White;
                    }
                    if (this.lbls[i] != null)
                    {
                        if (this.lbls[i].Tag.ToString() == "Tabular")
                        {
                            this.txtBxes[i].ReadOnly = true;
                            this.txtBxes[i].BackColor = System.Drawing.Color.WhiteSmoke;
                            this.txtBxes[i].SendToBack();
                            this.grdvw[i].BringToFront();
                            this.grdvw[i].ReadOnly = false;
                        }
                    }
                }
            }
        }

        private void myBtnEvents(object sender, EventArgs e)
        {
            if (this.editPrsButton.Text == "EDIT")
            {
                this.editPrsButton.PerformClick();
            }
            if (this.editPrsn == false && this.addPrsn == false)
            {
                Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
                return;
            }
            Button mybtn = (Button)sender;
            int colno = int.Parse(mybtn.Tag.ToString());
            string lovnm = Global.get_PrsExtrDataPrpty("attchd_lov_name", colno, Global.mnFrm.cmCde.Org_id);
            string datatyp = Global.get_PrsExtrDataPrpty("column_data_type", colno, Global.mnFrm.cmCde.Org_id);
            int dtlen = int.Parse(Global.get_PrsExtrDataPrpty("data_length", colno, Global.mnFrm.cmCde.Org_id));
            if (lovnm != "")
            {
                int lovid = Global.mnFrm.cmCde.getLovID(lovnm);
                string islovdynmc = Global.mnFrm.cmCde.getGnrlRecNm(
                  "gst.gen_stp_lov_names", "value_list_id", "is_list_dynamic", lovid);
                if (islovdynmc == "1")
                {
                    string[] selVals = new string[1];
                    selVals[0] = this.txtBxes[colno - 1].Text;
                    DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                        Global.mnFrm.cmCde.getLovID(lovnm), ref selVals,
                        true, false,
                 this.srchWrd, "Both", true);
                    if (dgRes == DialogResult.OK)
                    {
                        for (int i = 0; i < selVals.Length; i++)
                        {
                            this.txtBxes[colno - 1].Text = selVals[i];
                        }
                    }
                }
                else
                {
                    int[] selVals = new int[1];
                    selVals[0] = Global.mnFrm.cmCde.getPssblValID(this.txtBxes[colno - 1].Text,
                      Global.mnFrm.cmCde.getLovID(lovnm));
                    DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                     Global.mnFrm.cmCde.getLovID(lovnm), ref selVals, true, false,
                 this.srchWrd, "Both", true);
                    if (dgRes == DialogResult.OK)
                    {
                        for (int i = 0; i < selVals.Length; i++)
                        {
                            this.txtBxes[colno - 1].Text = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
                        }
                    }
                }
            }
            else if (datatyp == "Date")
            {
                //Date
                Global.mnFrm.cmCde.selectDate(ref this.txtBxes[colno - 1]);
            }
            if (this.txtBxes[colno - 1].Text.Length > dtlen)
            {
                this.txtBxes[colno - 1].Text = this.txtBxes[colno - 1].Text.Substring(0, dtlen);
            }
        }

        private void myTxtBxLeaveEvents(object sender, EventArgs e)
        {
            if (this.txtChngd == false)
            {
                return;
            }
            if (this.editPrsButton.Text == "EDIT")
            {
                this.editPrsButton.PerformClick();
            }
            if (this.editPrsn == false && this.addPrsn == false)
            {
                Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
                return;
            }

            this.txtChngd = false;
            TextBox mytxt = (TextBox)sender;
            int colno = int.Parse(mytxt.Tag.ToString());
            string datatyp = Global.get_PrsExtrDataPrpty("column_data_type", colno, Global.mnFrm.cmCde.Org_id);
            string lovnm = Global.get_PrsExtrDataPrpty("attchd_lov_name", colno, Global.mnFrm.cmCde.Org_id);
            int dtlen = int.Parse(Global.get_PrsExtrDataPrpty("data_length", colno, Global.mnFrm.cmCde.Org_id));
            this.obey_evnts = false;
            this.srchWrd = mytxt.Text;
            if (!mytxt.Text.Contains("%"))
            {
                this.srchWrd = "%" + this.srchWrd.Replace(" ", "%") + "%";
            }

            if (lovnm != "")
            {
                mytxt.Text = "";
                this.myBtnEvents(this.btnButtons[colno - 1], e);
            }
            else if (datatyp == "Date")
            {
                //Date
                if (this.txtBxes[colno - 1].Text != "")
                {
                    this.txtBxes[colno - 1].Text = Global.mnFrm.cmCde.checkNFormatDate(this.txtBxes[colno - 1].Text);
                    if (this.txtBxes[colno - 1].Text.Length > dtlen)
                    {
                        this.txtBxes[colno - 1].Text = this.txtBxes[colno - 1].Text.Substring(0, dtlen);
                    }
                }
            }

            this.srchWrd = "%";
            this.obey_evnts = true;
            this.txtChngd = false;
        }

        private void myTxtClickEvnts(object sender, EventArgs e)
        {
            TextBox mytxt = (TextBox)sender;
            int colno = int.Parse(mytxt.Tag.ToString());

            this.txtBxes[colno - 1].SelectAll();
        }

        private void myTxtBxTextChangedEvents(object sender, EventArgs e)
        {
            if (!this.obey_prs_evnts)
            {
                return;
            }
            this.txtChngd = true;
        }

        private void myTxtBxEvents(object sender, EventArgs e)
        {
            if (this.editPrsButton.Text == "EDIT")
            {
                this.editPrsButton.PerformClick();
            }
            if (this.editPrsn == false && this.addPrsn == false)
            {
                //Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
                return;
            }
            Button mytxt = (Button)sender;
            int colno = int.Parse(mytxt.Tag.ToString());
            int dtlen = int.Parse(Global.get_PrsExtrDataPrpty("data_length", colno, Global.mnFrm.cmCde.Org_id));
            //this.dobTextBox.TextChanged += new System.EventHandler(this.dobTextBox_TextChanged);
        }

        private void mygrdvw_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (this.shdObeyPrsEvts() == false)
            {
                return;
            }
            if (this.editPrsButton.Text == "EDIT")
            {
                this.editPrsButton.PerformClick();
            }
            if (this.editPrsn == false && this.addPrsn == false)
            {
                Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
                return;
            }
            DataGridView mygrd = (DataGridView)sender;
            int colno = int.Parse(mygrd.Tag.ToString());
            string fnlData = "";
            for (int i = 0; i < mygrd.Rows.Count; i++)
            {
                for (int j = 0; j < mygrd.Columns.Count; j++)
                {
                    if (mygrd.Rows[i].Cells[j].Value == null)
                    {
                        mygrd.Rows[i].Cells[j].Value = "";
                    }
                    fnlData += mygrd.Rows[i].Cells[j].Value.ToString().Replace("~", "").Replace("|", "");
                    if (j < mygrd.Columns.Count - 1)
                    {
                        fnlData += "~";
                    }
                }
                if (i < mygrd.Rows.Count - 1)
                {
                    fnlData += "|";
                }
            }

            this.txtBxes[colno - 1].Text = fnlData.Trim('|').Trim('~');

        }

        private void createPrsExtrDataDsbl()
        {
            /*
             * Get Groups
             * For each group create groupbox
             * Get Group Fields
             * For each field create label then create textbox (& button if has lov) if detail 
             * else listview if tabular with designated tabular columns
             */
            this.panel13.Visible = false;
            //System.Windows.Forms.Application.DoEvents();
            this.fillWithNull();
            DataSet dtst = Global.get_PrsExtrDataGrps(Global.mnFrm.cmCde.Org_id);
            this.panel13.Controls.Clear();
            int curYPostn = 2;
            int curXPostn = 5;
            //int curLine = 1;
            int curInnrYPostn = 20;
            int curInnrXPostn = 5;
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                if (curYPostn >= this.prsNamesListView.Height - 50)
                {
                    curYPostn = 2;
                    curXPostn += 345;
                }
                GroupBox myBox = new System.Windows.Forms.GroupBox();
                myBox.SuspendLayout();
                this.panel13.Controls.Add(myBox);

                myBox.ForeColor = System.Drawing.Color.White;
                myBox.Location = new System.Drawing.Point(curXPostn, curYPostn);
                myBox.Name = "myGrpBox" + (i + 1).ToString();
                myBox.Size = new System.Drawing.Size(340, 200);
                myBox.TabIndex = 10;
                myBox.TabStop = false;
                myBox.Text = dtst.Tables[0].Rows[i][0].ToString();

                DataSet fldDtSt = Global.get_PrsExtrDataGrpCols(
                  dtst.Tables[0].Rows[i][0].ToString(),
                  Global.mnFrm.cmCde.Org_id);
                curInnrYPostn = 20;
                for (int j = 0; j < fldDtSt.Tables[0].Rows.Count; j++)
                {
                    curInnrXPostn = 5;
                    int dtlen = 0;
                    int.TryParse(fldDtSt.Tables[0].Rows[j][6].ToString(), out dtlen);
                    int colnum = int.Parse(fldDtSt.Tables[0].Rows[j][1].ToString());
                    Label myLbl = new System.Windows.Forms.Label();
                    myLbl.ForeColor = System.Drawing.Color.White;
                    myLbl.Location = new System.Drawing.Point(curInnrXPostn, curInnrYPostn);
                    myLbl.Name = "myLbl" + (colnum).ToString();
                    myLbl.TabIndex = j;
                    myLbl.AutoSize = false;
                    myLbl.Text = fldDtSt.Tables[0].Rows[j][2].ToString();
                    //NB: Separate Column Values with ~ (i.e tilde) and Different Rows with | (i.e pipe)
                    myLbl.Tag = fldDtSt.Tables[0].Rows[j][7].ToString();
                    if (dtlen <= 200)
                    {
                        myLbl.Size = new System.Drawing.Size(120, 27);
                    }
                    else
                    {
                        myLbl.Size = new System.Drawing.Size(120, 42);
                    }

                    if (fldDtSt.Tables[0].Rows[j][7].ToString() == "Tabular")
                    {
                        myLbl.Visible = false;
                        DataGridView mygrdvw = new DataGridView();
                        mygrdvw.Location = new System.Drawing.Point(curInnrXPostn, curInnrYPostn);
                        mygrdvw.Name = "myGrdVw" + (colnum).ToString();
                        int grdhgt = (dtlen / 200) * 30;
                        if (grdhgt < 70)
                        {
                            grdhgt = 70;
                        }
                        mygrdvw.Size = new System.Drawing.Size(330, grdhgt);
                        mygrdvw.TabIndex = j;
                        int noCols = 1;
                        int.TryParse(fldDtSt.Tables[0].Rows[j][9].ToString(), out noCols);
                        mygrdvw.Tag = colnum;

                        mygrdvw.AllowUserToAddRows = true;
                        mygrdvw.AllowUserToDeleteRows = true;
                        mygrdvw.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                    | System.Windows.Forms.AnchorStyles.Left)
                                    | System.Windows.Forms.AnchorStyles.Right)));
                        mygrdvw.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
                        mygrdvw.BackgroundColor = System.Drawing.Color.White;
                        mygrdvw.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                        mygrdvw.ColumnHeadersHeight = 25;
                        mygrdvw.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                        //this.itmPssblValDataGridView.ContextMenuStrip = this.payItmsContextMenuStrip;
                        DataGridViewCellStyle dataGridViewCellStyle16 = new DataGridViewCellStyle();
                        dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
                        dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Window;
                        dataGridViewCellStyle16.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        dataGridViewCellStyle16.ForeColor = System.Drawing.Color.White;
                        dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
                        dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
                        dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
                        mygrdvw.DefaultCellStyle = dataGridViewCellStyle16;
                        mygrdvw.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
                        mygrdvw.ReadOnly = true;
                        mygrdvw.RowHeadersWidth = 5;
                        mygrdvw.RowHeadersVisible = true;
                        mygrdvw.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                        DataGridViewCellStyle dataGridViewCellStyle17 = new DataGridViewCellStyle();
                        dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
                        dataGridViewCellStyle17.ForeColor = System.Drawing.Color.Black;
                        dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
                        mygrdvw.RowsDefaultCellStyle = dataGridViewCellStyle17;
                        int untWdth = 320 / noCols;
                        if (untWdth < 70)
                        {
                            untWdth = 100;
                        }
                        char[] w = { ',' };
                        string[] arry1 = fldDtSt.Tables[0].Rows[j][11].ToString().Split(w, StringSplitOptions.RemoveEmptyEntries);
                        DataGridViewColumn[] grdCols = new DataGridViewColumn[noCols];
                        for (int z = 0; z < noCols; z++)
                        {
                            DataGridViewTextBoxColumn myGrdCol = new DataGridViewTextBoxColumn();
                            myGrdCol.Name = "myGrdCol" + (z + 1).ToString();
                            myGrdCol.DefaultCellStyle = dataGridViewCellStyle17;

                            if (z < arry1.Length)
                            {
                                myGrdCol.HeaderText = arry1[z];
                            }
                            else
                            {
                                myGrdCol.HeaderText = (z + 1).ToString();
                            }
                            if (myGrdCol.HeaderText.Length >= 2)
                            {
                                untWdth = myGrdCol.HeaderText.Length * 5;
                            }
                            myGrdCol.Width = untWdth;

                            grdCols[z] = myGrdCol;
                        }
                        mygrdvw.Columns.AddRange(grdCols);
                        mygrdvw.CellValueChanged += new DataGridViewCellEventHandler(mygrdvw_CellValueChanged);
                        myBox.Controls.Add(mygrdvw);
                        this.grdvw[colnum - 1] = mygrdvw;
                        curInnrYPostn += mygrdvw.Height + 5;
                    }
                    else
                    {
                        curInnrXPostn += myLbl.Width;
                    }

                    myBox.Controls.Add(myLbl);

                    this.lbls[colnum - 1] = myLbl;


                    TextBox myTxtBx = new System.Windows.Forms.TextBox();
                    myTxtBx.BackColor = System.Drawing.Color.WhiteSmoke;
                    myTxtBx.Location = new System.Drawing.Point(curInnrXPostn, curInnrYPostn);
                    myTxtBx.MaxLength = dtlen;
                    myTxtBx.Name = "myTxtBx" + (colnum).ToString();
                    myTxtBx.Tag = colnum;
                    myTxtBx.ReadOnly = true;
                    if (dtlen <= 200)
                    {
                        myTxtBx.Multiline = false;
                        myTxtBx.Size = new System.Drawing.Size(180, 21);
                    }
                    else
                    {
                        myTxtBx.Multiline = true;
                        myTxtBx.ScrollBars = ScrollBars.Vertical;
                        myTxtBx.Size = new System.Drawing.Size(180, 42);
                    }

                    myTxtBx.TabIndex = j;

                    if (fldDtSt.Tables[0].Rows[j][3].ToString() != ""
                      || fldDtSt.Tables[0].Rows[j][4].ToString() == "Date")
                    {
                        myTxtBx.TextChanged += new System.EventHandler(this.myTxtBxTextChangedEvents);
                        myTxtBx.Leave += new System.EventHandler(this.myTxtBxLeaveEvents);
                        myTxtBx.Click += new EventHandler(this.myTxtClickEvnts);
                        myTxtBx.Enter += new EventHandler(this.myTxtClickEvnts);
                    }

                    myBox.Controls.Add(myTxtBx);
                    this.txtBxes[colnum - 1] = myTxtBx;

                    if (fldDtSt.Tables[0].Rows[j][3].ToString() != ""
                      || fldDtSt.Tables[0].Rows[j][4].ToString() == "Date")
                    {
                        curInnrXPostn += myTxtBx.Width;
                        Button myBtn = new System.Windows.Forms.Button();
                        myBtn.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        myBtn.ForeColor = System.Drawing.Color.Black;
                        myBtn.Location = new System.Drawing.Point(curInnrXPostn, curInnrYPostn - 1);
                        myBtn.Name = "titleButton" + (colnum).ToString();
                        myBtn.Size = new System.Drawing.Size(28, 23);
                        myBtn.TabIndex = j;
                        myBtn.Text = "...";
                        myBtn.Tag = colnum;
                        myBtn.UseVisualStyleBackColor = true;
                        myBtn.Click += new System.EventHandler(this.myBtnEvents);
                        // 
                        this.btnButtons[colnum - 1] = myBtn;
                        myBox.Controls.Add(myBtn);
                    }
                    if (fldDtSt.Tables[0].Rows[j][7].ToString() == "Tabular")
                    {
                        myTxtBx.Visible = false;
                    }
                    else
                    {
                        curInnrYPostn += myTxtBx.Height + 5;
                    }
                }
                myBox.Height = curInnrYPostn + 5;
                curYPostn += myBox.Height + 3;
                //curYPostn = curInnrYPostn + 5;
                //myBox.Controls.Add(this.rltvsButton);
            }
            this.panel13.Visible = true;
            //System.Windows.Forms.Application.DoEvents();
        }

        private bool shdObeyPrsEvts()
        {
            return this.obey_prs_evnts;
        }

        private void prsPnlNavButtons(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecPrsLabel.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.is_last_prs = false;
                this.prs_cur_indx = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.is_last_prs = false;
                this.prs_cur_indx -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.is_last_prs = false;
                this.prs_cur_indx += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.is_last_prs = true;
                this.totl_prs = Global.get_Total_OrgPrs(this.searchForPrsTextBox.Text,
                 this.searchInPrsComboBox.Text, Global.mnFrm.cmCde.Org_id
                 , this.searchAllOrgCheckBox.Checked,
              this.prsnTypComboBox.Text, this.fltrByComboBox.Text);
                this.updtPrsTotals();
                this.prs_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            this.getOrgPrsData();
        }

        private void recreatePicBoxes()
        {
            this.groupBox2.Controls.Remove(this.prsnDetPictureBox);
            PictureBox pcbx = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)(pcbx)).BeginInit();

            this.prsnDetPictureBox = pcbx;
            this.prsnDetPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.prsnDetPictureBox.Image = global::BasicPersonData.Properties.Resources.staffs;
            this.prsnDetPictureBox.Location = new System.Drawing.Point(9, 29);
            this.prsnDetPictureBox.Name = "prsnDetPictureBox";
            this.prsnDetPictureBox.Size = new System.Drawing.Size(167, 142);
            this.prsnDetPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.prsnDetPictureBox.TabIndex = 91;
            this.prsnDetPictureBox.TabStop = false;

            this.groupBox2.Controls.Add(this.prsnDetPictureBox);

            this.splitContainer2.Panel1.Controls.Remove(this.prsPictureBox);
            PictureBox pcbx1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)(pcbx1)).BeginInit();
            this.prsPictureBox = pcbx1;

            this.prsPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.prsPictureBox.Image = global::BasicPersonData.Properties.Resources.staffs;
            this.prsPictureBox.Location = new System.Drawing.Point(204, 31);
            this.prsPictureBox.Name = "prsPictureBox";
            this.prsPictureBox.Size = new System.Drawing.Size(125, 108);
            this.prsPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.prsPictureBox.TabIndex = 88;
            this.prsPictureBox.TabStop = false;
            this.splitContainer2.Panel1.Controls.Add(this.prsPictureBox);

            this.groupBox10.Controls.Remove(this.qrCodePictureBox);
            PictureBox pcbx2 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)(pcbx2)).BeginInit();
            this.qrCodePictureBox = pcbx2;

            this.qrCodePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.qrCodePictureBox.Image = global::BasicPersonData.Properties.Resources.staffs;
            this.qrCodePictureBox.Location = new System.Drawing.Point(4, 31);
            this.qrCodePictureBox.Name = "qrCodePictureBox";
            this.qrCodePictureBox.Size = new System.Drawing.Size(161, 131);
            this.qrCodePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.qrCodePictureBox.TabIndex = 91;
            this.qrCodePictureBox.TabStop = false;
            this.groupBox10.Controls.Add(this.qrCodePictureBox);


        }

        private void goPrsButton_Click(object sender, EventArgs e)
        {
            //this.recreatePicBoxes(); 
            this.disablePrsEdit();
            this.disableExtrDataEdit();

            //System.Windows.Forms.Application.DoEvents();
            this.prs_cur_indx = 0;
            this.loadOrgPersons();
        }

        private void prsNamesListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.shdObeyPrsEvts() == false || this.prsNamesListView.SelectedItems.Count > 1)
                {
                    return;
                }
                if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
                {
                    //Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                    return;
                }
                if (this.addPrsn == true)
                {
                    if (this.prsNamesListView.Items.Count > 0)
                    {
                        bool rs = this.shdObeyPrsEvts();
                        this.obey_prs_evnts = false;
                        this.prsNamesListView.SelectedItems.Clear();
                        this.prsNamesListView.Items[0].Selected = true;
                        //System.Windows.Forms.Application.DoEvents();
                        this.obey_prs_evnts = rs;
                    }
                    return;
                }

                if (this.prsPictureBox.Image == this.prsPictureBox.ErrorImage
                  || this.prsnDetPictureBox.Image == this.prsnDetPictureBox.ErrorImage
                  || this.qrCodePictureBox.Image == this.qrCodePictureBox.ErrorImage)
                {
                    this.recreatePicBoxes();
                }
                this.prsPictureBox.Image = BasicPersonData.Properties.Resources.staffs;
                this.prsnDetPictureBox.Image = BasicPersonData.Properties.Resources.staffs;
                if (this.prsNamesListView.SelectedItems.Count >= 1)
                {
                    Global.mnFrm.cmCde.getDBImageFile(
                                      this.prsNamesListView.SelectedItems[0].SubItems[4].Text, 2, ref this.prsPictureBox, ref this.prsnDetPictureBox);
                    this.imgTextBox.Text = this.prsNamesListView.SelectedItems[0].SubItems[4].Text;
                    //         Global.mnFrm.cmCde.getDBImageFile(dtst.Tables[0].Rows[i][18].ToString(),
                    //2, ref this.prsnDetPictureBox);
                }

                if (this.prsNamesListView.SelectedItems.Count >= 1)
                {
                    this.prsnIDTextBox.Text = this.prsNamesListView.SelectedItems[0].SubItems[3].Text;
                    this.locIDTextBox.Text = this.prsNamesListView.SelectedItems[0].SubItems[1].Text;
                }
                this.loadCorrectPanel();
                System.Windows.Forms.Application.DoEvents();
                //System.Threading.Thread.Sleep(100);
                //this.prsnDetPictureBox.Image = this.prsPictureBox.Image;
            }
            catch (Exception Ex)
            {
                this.recreatePicBoxes();
            }
        }

        private void prsNamesListView_ItemSelectionChanged(object sender, System.Windows.Forms.ListViewItemSelectionChangedEventArgs e)
        {
            if (this.shdObeyPrsEvts() == false)
            {
                return;
            }
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                //Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            if (e.IsSelected)
            {
                e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
                this.prsnDetPictureBox.Image = this.prsPictureBox.Image;
            }
            else
            {
                e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Regular);
            }
        }

        private void titleButton_Click(object sender, EventArgs e)
        {
            if (this.editPrsButton.Text == "EDIT")
            {
                this.editPrsButton.PerformClick();
            }

            if (this.editPrsn == false && this.addPrsn == false)
            {
                Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
                return;
            }
            //Titles
            int[] selVals = new int[1];
            selVals[0] = Global.mnFrm.cmCde.getPssblValID(this.titleTextBox.Text, Global.mnFrm.cmCde.getLovID("Person Titles"));
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
             Global.mnFrm.cmCde.getLovID("Person Titles"), ref selVals, true, true,
             this.srchWrd, "Both", true);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.titleTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
                }
            }
        }

        private void genderButton_Click(object sender, EventArgs e)
        {
            if (this.editPrsButton.Text == "EDIT")
            {
                this.editPrsButton.PerformClick();
            }
            if (this.editPrsn == false && this.addPrsn == false)
            {
                Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
                return;
            }
            //Gender
            int[] selVals = new int[1];
            selVals[0] = Global.mnFrm.cmCde.getPssblValID(this.genderTextBox.Text,
             Global.mnFrm.cmCde.getLovID("Gender"));
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
             Global.mnFrm.cmCde.getLovID("Gender"), ref selVals, true, true,
             this.srchWrd, "Both", true);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.genderTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
                }
            }
        }

        private void maritalStatusButton_Click(object sender, EventArgs e)
        {
            if (this.editPrsButton.Text == "EDIT")
            {
                this.editPrsButton.PerformClick();
            }

            if (this.editPrsn == false && this.addPrsn == false)
            {
                Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
                return;
            }
            //Marital Status
            int[] selVals = new int[1];
            selVals[0] = Global.mnFrm.cmCde.getPssblValID(this.maritalStatusTextBox.Text,
             Global.mnFrm.cmCde.getLovID("Marital Status"));
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
             Global.mnFrm.cmCde.getLovID("Marital Status"), ref selVals, true, true,
             this.srchWrd, "Both", true);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.maritalStatusTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
                }
            }
        }

        private void dobButton_Click(object sender, EventArgs e)
        {
            if (this.editPrsButton.Text == "EDIT")
            {
                this.editPrsButton.PerformClick();
            }

            if (this.editPrsn == false && this.addPrsn == false)
            {
                Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
                return;
            }
            Global.mnFrm.cmCde.selectDate(ref this.dobTextBox);
            if (this.dobTextBox.Text.Length > 11)
            {
                this.dobTextBox.Text = this.dobTextBox.Text.Substring(0, 11);
                this.ageLabel.Text = Global.computePrsnAge(this.dobTextBox.Text);
            }
        }

        private void prsnTypeButton_Click1(object sender, EventArgs e)
        {
            if (this.editPrsButton.Text == "EDIT")
            {
                this.editPrsButton.PerformClick();
            }

            if (this.editPrsn == false && this.addPrsn == false)
            {
                Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
                return;
            }
            prsnTypeDiag nwDiag = new prsnTypeDiag();
            nwDiag.prsnTypTextBox.Text = this.prsnTypTextBox.Text;
            nwDiag.reasonTextBox.Text = this.prsnTypeRsn;
            nwDiag.furtherDetTextBox.Text = this.prsnTypeFurDet;
            nwDiag.vldStrtDteTextBox.Text = this.vldDte1;
            nwDiag.vldEndDteTextBox.Text = this.vldDte2;
            string dateStr = DateTime.ParseExact(
      Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
            if (this.vldDte1 == "")
            {
                nwDiag.vldStrtDteTextBox.Text = dateStr.Substring(0, 11);
            }
            if (this.vldDte2 == "")
            {
                nwDiag.vldEndDteTextBox.Text = "31-Dec-4000";
            }
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
                this.prsnTypTextBox.Text = nwDiag.prsnTypTextBox.Text;
                this.prsnTypeRsn = nwDiag.reasonTextBox.Text;
                this.prsnTypeFurDet = nwDiag.furtherDetTextBox.Text;
                this.vldDte1 = nwDiag.vldStrtDteTextBox.Text;
                this.vldDte2 = nwDiag.vldEndDteTextBox.Text;
            }
        }

        private void prsOrgButton_Click(object sender, EventArgs e)
        {
            if (this.editPrsButton.Text == "EDIT")
            {
                this.editPrsButton.PerformClick();
            }
            if (this.editPrsn == false && this.addPrsn == false)
            {
                Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
                return;
            }
            string[] selVals = new string[1];
            selVals[0] = this.prsOrgIDTextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
             Global.mnFrm.cmCde.getLovID("Organisations"), ref selVals, true, false,
             this.srchWrd, "Both", true);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.prsOrgIDTextBox.Text = selVals[i];
                    this.prsOrgTextBox.Text = Global.mnFrm.cmCde.getOrgName(int.Parse(selVals[i]));
                }
            }
        }

        private void addPrsButton_Click(object sender, EventArgs e)
        {
            if (this.addPrsButton.Text == "ADD")
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[7]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                     " this action!\nContact your System Administrator!", 0);
                    return;
                }
                this.clearPrsInfo();
                this.prsnDetPictureBox.Image = BasicPersonData.Properties.Resources.staffs;

                this.addPrsn = true;
                this.editPrsn = false;
                this.educDataGridView.Rows.Clear();
                this.wrkExpDataGridView.Rows.Clear();
                this.skillsDataGridView.Rows.Clear();

                this.divsDataGridView.Rows.Clear();
                this.sitesDataGridView.Rows.Clear();
                this.sprvisrDataGridView.Rows.Clear();
                this.jobsDataGridView.Rows.Clear();
                this.gradesDataGridView.Rows.Clear();
                this.positionDataGridView.Rows.Clear();

                this.extInfoDataGridView.Rows.Clear();

                this.prsnTabControl.SelectedTab = this.prsnInfoTabPage;
                this.prpareForPrsEdit();
                this.prsOrgIDTextBox.Text = Global.mnFrm.cmCde.Org_id.ToString();
                this.prsOrgTextBox.Text = Global.mnFrm.cmCde.getOrgName(Global.mnFrm.cmCde.Org_id);
                string dateStr = DateTime.ParseExact(
        Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
        System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
                if (this.vldDte1 == "")
                {
                    this.vldStrtDteTextBox.Text = dateStr.Substring(0, 11);
                    this.vldDte1 = this.vldStrtDteTextBox.Text;
                }
                if (this.vldDte2 == "")
                {
                    this.vldEndDteTextBox.Text = "31-Dec-4000";
                    this.vldDte2 = this.vldEndDteTextBox.Text;
                }
                this.addPrsButton.Enabled = false;
                this.editPrsButton.Enabled = false;
                this.deletePrsButton.Enabled = false;

                //this.editPrsButton.Text = "STOP";
                //this.editPrsnMenuItem.Text = "STOP ADDING";
            }
            else
            {
            }
        }

        private void enblDsblGridVws(bool enblDsbl)
        {
            System.Drawing.Color bckColr = System.Drawing.Color.Gainsboro;
            if (enblDsbl)
            {
                bckColr = System.Drawing.Color.White;
            }
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[8]) == false && enblDsbl == true)
            {
                //Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                // " this action!\nContact your System Administrator!", 0);
            }
            else
            {
                this.educDataGridView.DefaultCellStyle.BackColor = bckColr;
                this.educDataGridView.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
                this.educDataGridView.ReadOnly = !enblDsbl;
                this.educDataGridView.Columns[0].ReadOnly = true;
                this.educDataGridView.Columns[2].ReadOnly = true;
                this.educDataGridView.Columns[4].ReadOnly = true;
                this.educDataGridView.Columns[8].ReadOnly = true;
                this.educDataGridView.Columns[10].ReadOnly = true;
                this.saveEducButton.Enabled = enblDsbl;

                this.wrkExpDataGridView.DefaultCellStyle.BackColor = bckColr;
                this.wrkExpDataGridView.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
                this.wrkExpDataGridView.ReadOnly = !enblDsbl;
                this.wrkExpDataGridView.Columns[0].ReadOnly = true;
                this.wrkExpDataGridView.Columns[2].ReadOnly = true;
                this.wrkExpDataGridView.Columns[4].ReadOnly = true;
                this.saveWrkButton.Enabled = enblDsbl;

                this.skillsDataGridView.DefaultCellStyle.BackColor = bckColr;
                this.skillsDataGridView.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
                this.skillsDataGridView.ReadOnly = !enblDsbl;
                this.skillsDataGridView.Columns[0].ReadOnly = true;
                this.skillsDataGridView.Columns[2].ReadOnly = true;
                this.skillsDataGridView.Columns[4].ReadOnly = true;
                this.skillsDataGridView.Columns[6].ReadOnly = true;
                this.skillsDataGridView.Columns[8].ReadOnly = true;
                this.saveSkillButton.Enabled = enblDsbl;

                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[11]) == false && enblDsbl == true)
                {
                    //Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    // " this action!\nContact your System Administrator!", 0);
                }
                else
                {
                    this.sitesDataGridView.DefaultCellStyle.BackColor = bckColr;
                    this.sitesDataGridView.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
                    this.sitesDataGridView.ReadOnly = !enblDsbl;
                    this.sitesDataGridView.Columns[0].ReadOnly = true;
                    this.saveLocButton.Enabled = enblDsbl;

                    this.sprvisrDataGridView.DefaultCellStyle.BackColor = bckColr;
                    this.sprvisrDataGridView.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
                    this.sprvisrDataGridView.ReadOnly = !enblDsbl;
                    this.sprvisrDataGridView.Columns[0].ReadOnly = true;
                    this.saveSprvsrButton.Enabled = enblDsbl;

                    this.divsDataGridView.DefaultCellStyle.BackColor = bckColr;
                    this.divsDataGridView.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
                    this.divsDataGridView.ReadOnly = !enblDsbl;
                    this.divsDataGridView.Columns[0].ReadOnly = true;
                    this.divsDataGridView.Columns[2].ReadOnly = true;
                    this.saveDivButton.Enabled = enblDsbl;

                    this.jobsDataGridView.DefaultCellStyle.BackColor = bckColr;
                    this.jobsDataGridView.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
                    this.jobsDataGridView.ReadOnly = !enblDsbl;
                    this.jobsDataGridView.Columns[0].ReadOnly = true;
                    this.saveJobButton.Enabled = enblDsbl;

                    this.gradesDataGridView.DefaultCellStyle.BackColor = bckColr;
                    this.gradesDataGridView.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
                    this.gradesDataGridView.ReadOnly = !enblDsbl;
                    this.gradesDataGridView.Columns[0].ReadOnly = true;
                    this.saveGradeButton.Enabled = enblDsbl;

                    this.positionDataGridView.DefaultCellStyle.BackColor = bckColr;
                    this.positionDataGridView.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
                    this.positionDataGridView.ReadOnly = !enblDsbl;
                    this.positionDataGridView.Columns[6].ReadOnly = true;
                    this.positionDataGridView.Columns[0].ReadOnly = true;
                    this.savePostnButton.Enabled = enblDsbl;

                }
            }

        }

        private void saveGridVws()
        {
            if (this.prsnTabControl.SelectedTab == this.educTabPage)
            {
                if (this.saveEducButton.Enabled == true)
                {
                    this.saveEducButton.PerformClick();
                }
                if (this.saveWrkButton.Enabled == true)
                {
                    this.saveWrkButton.PerformClick();
                }
                if (this.saveSkillButton.Enabled == true)
                {
                    this.saveSkillButton.PerformClick();
                }
            }

            if (this.prsnTabControl.SelectedTab == this.bscAsgnTabPage)
            {
                if (this.saveLocButton.Enabled == true)
                {
                    this.saveLocButton.PerformClick();
                }

                if (this.saveSprvsrButton.Enabled == true)
                {
                    this.saveSprvsrButton.PerformClick();
                }

                if (this.saveDivButton.Enabled == true)
                {
                    this.saveDivButton.PerformClick();
                }

                if (this.saveJobButton.Enabled == true)
                {
                    this.saveJobButton.PerformClick();
                }

                if (this.saveGradeButton.Enabled == true)
                {
                    this.saveGradeButton.PerformClick();
                }

                if (this.savePostnButton.Enabled == true)
                {
                    this.saveGradeButton.PerformClick();
                }
            }
        }

        private void editPrsButton_Click(object sender, EventArgs e)
        {
            if (this.editPrsButton.Text == "EDIT")
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[8]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                     " this action!\nContact your System Administrator!", 0);
                    return;
                }
                if (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1")
                {
                    Global.mnFrm.cmCde.showMsg("No record to Edit!", 0);
                    return;
                }
                //this.prsnTabControl.SelectedTab = this.prsnInfoTabPage;
                this.addPrsn = false;
                this.editPrsn = true;
                this.prpareForPrsEdit();
                this.prpareForExtrDataEdit();
                this.addPrsButton.Enabled = false;
                //this.editPrsButton.Enabled = false;
                this.deletePrsButton.Enabled = false;
                this.editPrsButton.Text = "STOP";
                this.editPrsnMenuItem.Text = "STOP EDITING";
            }
            else
            {
                this.disablePrsEdit();
                this.disableExtrDataEdit();
                //System.Windows.Forms.Application.DoEvents();
                this.loadOrgPersons();
            }
        }

        private void deletePrsButton_Click(object sender, EventArgs e)
        {
            /*To Delete Person First Check
             * 1. Payments in person's name/id
             * 2. Attendance Records having the Person
             * 3. Customers/Suppliers linked to this person
             * 4. Is person a relative to some one or person has been assigned to a user
             * 5. if all of above does not apply then
             * a. delete persons payitems
             * b. persons banks
             * c. manual person sets lines
             * d. update prsn behind user name to -1
             * e. 
             */
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[9]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.prsNamesListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the PERSON to DELETE!", 0);
                return;
            }
            if (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select a saved Person First!", 0);
                return;
            }
            long prsnID = long.Parse(this.prsnIDTextBox.Text);
            long rslts = 0;
            DataSet dtst = new DataSet();
            //1. Get payments in Persons name
            dtst = new DataSet();
            rslts = 0;
            string strSQL = @"Select count(1) from pay.pay_itm_trnsctns where person_id = " + prsnID.ToString();
            dtst = Global.mnFrm.cmCde.selectDataNoParams(strSQL);
            long.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rslts);
            if (rslts > 0)
            {
                Global.mnFrm.cmCde.showMsg("Cannot Delete Persons with Payments in their Name!", 0);
                return;
            }
            //2. Get Attendance Recs in Persons name
            dtst = new DataSet();
            rslts = 0;
            strSQL = @"Select count(1) from attn.attn_attendance_recs where person_id = " + prsnID.ToString();
            dtst = Global.mnFrm.cmCde.selectDataNoParams(strSQL);
            long.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rslts);
            if (rslts > 0)
            {
                Global.mnFrm.cmCde.showMsg("Cannot Delete Persons with Attendance Records in their Name!", 0);
                return;
            }

            //3. Get Customers/Suppliers in Persons name
            //dtst = new DataSet();
            //rslts = 0;
            //strSQL = @"Select count(1) from attn.attn_attendance_recs where person_id = " + prsnID.ToString();
            //dtst = Global.mnFrm.cmCde.selectDataNoParams(strSQL);
            //long.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rslts);
            //if (rslts > 0)
            //{
            //  Global.mnFrm.cmCde.showMsg("Cannot Delete Persons with Attendance Records in their Name!", 0);
            //  return;
            //}

            //4a. Is Person Relative to Some One
            dtst = new DataSet();
            rslts = 0;
            strSQL = @"Select count(1) from prs.prsn_relatives where relative_prsn_id = " + prsnID.ToString();
            dtst = Global.mnFrm.cmCde.selectDataNoParams(strSQL);
            long.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rslts);
            if (rslts > 0)
            {
                Global.mnFrm.cmCde.showMsg("Cannot Delete Persons attached as Relatives to Others!", 0);
                return;
            }
            //4b. Is Person Supervisor to Some One
            dtst = new DataSet();
            rslts = 0;
            strSQL = @"Select count(1) from pasn.prsn_supervisors where supervisor_prsn_id = " + prsnID.ToString();
            dtst = Global.mnFrm.cmCde.selectDataNoParams(strSQL);
            long.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rslts);
            if (rslts > 0)
            {
                Global.mnFrm.cmCde.showMsg("Cannot Delete Persons assigned as Supervisor to Others!", 0);
                return;
            }
            //4c. Is Person Host of an Event
            dtst = new DataSet();
            rslts = 0;
            strSQL = @"Select count(1) from attn.attn_attendance_events where host_prsn_id = " + prsnID.ToString();
            dtst = Global.mnFrm.cmCde.selectDataNoParams(strSQL);
            long.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rslts);
            if (rslts > 0)
            {
                Global.mnFrm.cmCde.showMsg("Cannot Delete Persons assigned as Host of an Event!", 0);
                return;
            }
            //5. Is Person Behind a User Name
            dtst = new DataSet();
            rslts = 0;
            strSQL = @"Select count(1) from sec.sec_users where person_id = " + prsnID.ToString();
            dtst = Global.mnFrm.cmCde.selectDataNoParams(strSQL);
            long.TryParse(dtst.Tables[0].Rows[0][0].ToString(), out rslts);
            if (rslts > 0)
            {
                Global.mnFrm.cmCde.showMsg("Cannot Delete Persons who are behind some User Names!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected PERSON \r\nand ALL OTHER DATA related to this PERSON?" +
      "\r\nThis action CANNOT be UNDONE!", 1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }

            //6. Delete all data related to the person
            strSQL = @"DELETE FROM pay.pay_prsn_sets_det WHERE person_id={:prsnID};
DELETE FROM prs.prsn_doc_attchmnts WHERE person_id={:prsnID};
DELETE FROM prs.prsn_education WHERE person_id={:prsnID};
DELETE FROM prs.prsn_extra_data WHERE person_id={:prsnID};
DELETE FROM prs.prsn_national_ids WHERE person_id={:prsnID};
DELETE FROM prs.prsn_relatives WHERE person_id={:prsnID};
DELETE FROM prs.prsn_skills_nature WHERE person_id={:prsnID};
DELETE FROM prs.prsn_work_experience WHERE person_id={:prsnID};
DELETE FROM pasn.prsn_bank_accounts WHERE person_id={:prsnID};
DELETE FROM pasn.prsn_bnfts_cntrbtns WHERE person_id={:prsnID};
DELETE FROM pasn.prsn_divs_groups WHERE person_id={:prsnID};
DELETE FROM pasn.prsn_grades WHERE person_id={:prsnID};
DELETE FROM pasn.prsn_jobs WHERE person_id={:prsnID};
DELETE FROM pasn.prsn_locations WHERE person_id={:prsnID};
DELETE FROM pasn.prsn_positions WHERE person_id={:prsnID};
DELETE FROM pasn.prsn_prsntyps WHERE person_id={:prsnID};
DELETE FROM pasn.prsn_supervisors WHERE person_id={:prsnID};
DELETE FROM prs.prsn_names_nos WHERE person_id={:prsnID};";

            strSQL = strSQL.Replace("{:prsnID}", prsnID.ToString());
            Global.mnFrm.cmCde.deleteDataNoParams(strSQL);
            this.goPrsButton_Click(this.goPrsButton, e);
        }

        private void savePrsButton_Click(object sender, EventArgs e)
        {
            if (this.addPrsn == true)
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[7]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                     " this action!\nContact your System Administrator!", 0);
                    return;
                }
            }
            else
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[8]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                     " this action!\nContact your System Administrator!", 0);
                    return;
                }
            }
            //this.prsnTabControl.SelectedTab = this.prsnInfoTabPage;
            //if (this.curTabIndx == "prsnInfoTabPage")
            //{
            char[] trmChr = { ',', ' ' };
            char[] w = { ',' };
            this.emailTextBox.Text = removeInvalidChars(this.emailTextBox.Text, "").Replace(":", ",").Replace(";", ",").Replace("/", ",").Replace(@"\", ",").Replace(",", ", ").Trim(trmChr).ToLower();
            this.telTextBox.Text = removeInvalidChars(this.telTextBox.Text, "").Replace(":", ",").Replace(";", ",").Replace("/", ",").Replace(@"\", ",").Replace(",", ", ").Trim(trmChr);
            this.moblTextBox.Text = removeInvalidChars(this.moblTextBox.Text, "").Replace(":", ",").Replace(";", ",").Replace("/", ",").Replace(@"\", ",").Replace(",", ", ").Trim(trmChr);
            this.faxTextBox.Text = removeInvalidChars(this.faxTextBox.Text, "").Replace(":", ",").Replace(";", ",").Replace("/", ",").Replace(@"\", ",").Replace(",", ", ").Trim(trmChr);

            string[] emails = this.emailTextBox.Text.Split(w, StringSplitOptions.RemoveEmptyEntries);

            for (int y = 0; y < emails.Length; y++)
            {
                if (IsValidEmail(emails[y].Trim(trmChr)) == false)
                {
                    Global.mnFrm.cmCde.showMsg("Invalid Email (" + emails[y].Trim(trmChr) + ") detected!\r\nPlease Correct it First!", 0);
                    return;
                }
            }

            string[] cntcNos = this.telTextBox.Text.Split(w, StringSplitOptions.RemoveEmptyEntries);
            string[] cntcMobls = this.moblTextBox.Text.Split(w, StringSplitOptions.RemoveEmptyEntries);
            for (int y = 0; y < cntcMobls.Length; y++)
            {
                if (cntcMobls[y].Trim(trmChr).Length == 10)
                {
                    if (cntcMobls[y].Trim(trmChr).Substring(0, 1) == "0")
                    {
                        cntcMobls[y] = "+233" + cntcMobls[y].Trim(trmChr).Substring(1);
                    }
                }
            }
            for (int y = 0; y < cntcNos.Length; y++)
            {
                if (cntcNos[y].Trim(trmChr).Length == 10)
                {
                    if (cntcNos[y].Trim(trmChr).Substring(0, 1) == "0")
                    {
                        cntcNos[y] = "+233" + cntcNos[y].Trim(trmChr).Substring(1);
                    }
                }
            }
            this.telTextBox.Text = string.Join(", ", cntcNos).Replace("   ", " ").Replace("  ", " ").Replace("'", "''").Trim(trmChr);
            this.moblTextBox.Text = string.Join(", ", cntcMobls).Replace("   ", " ").Replace("  ", " ").Replace("'", "''").Trim(trmChr);

            this.locIDTextBox.Focus();
            System.Windows.Forms.Application.DoEvents();

            if (this.locIDTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please enter an ID Number!", 0);
                return;
            }
            if (this.titleTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please enter a Title!", 0);
                return;
            }
            if (this.firstNameTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please enter a first name!", 0);
                return;
            }
            if (this.surnameTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please enter a surname!", 0);
                return;
            }
            if (this.genderTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please indicate Gender!", 0);
                return;
            }
            if (this.maritalStatusTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please indicate Marital Status!", 0);
                return;
            }
            if (this.dobTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please enter Date of Birth", 0);
                return;
            }
            if (this.ntnltyTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please enter Nationality", 0);
                return;
            }
            if (this.prsnTypTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please indicate Org Relation Type!", 0);
                return;
            }
            if (this.reasonTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please indicate Cause of Org Relation Type!", 0);
                return;
            }
            long oldPrsID = Global.mnFrm.cmCde.getPrsnID(this.locIDTextBox.Text);
            if (oldPrsID > 0
             && this.addPrsn == true)
            {
                Global.mnFrm.cmCde.showMsg("ID No. is already in Use!", 0);
                return;
            }
            if (oldPrsID > 0
             && this.editPrsn == true
             && oldPrsID.ToString() != this.prsnIDTextBox.Text)
            {
                Global.mnFrm.cmCde.showMsg("New ID No. is already in Use!", 0);
                return;
            }
            //}
            this.vldDte1 = this.vldStrtDteTextBox.Text;
            this.vldDte2 = this.vldEndDteTextBox.Text;
            if (this.vldDte2 == "")
            {
                this.vldDte2 = "31-Dec-4000";
            }

            if (this.addPrsn == true)
            {
                Global.createPrsnBasic(this.firstNameTextBox.Text, this.surnameTextBox.Text, this.otherNamesTextBox.Text,
                 this.titleTextBox.Text, this.locIDTextBox.Text, int.Parse(this.prsOrgIDTextBox.Text),
                 this.genderTextBox.Text, this.maritalStatusTextBox.Text, this.dobTextBox.Text,
                 this.pobTextBox.Text, this.religionTextBox.Text, this.resAddrsTextBox.Text,
                 this.pstlAddrsTextBox.Text, this.emailTextBox.Text, this.telTextBox.Text,
                 this.moblTextBox.Text, this.faxTextBox.Text, this.hometownTextBox.Text,
                 this.ntnltyTextBox.Text, this.imgTextBox.Text, long.Parse(this.linkedFirmIDTextBox.Text),
                 long.Parse(this.linkedSiteIDTextBox.Text));

                Global.createPrsnsType(Global.mnFrm.cmCde.getPrsnID(this.locIDTextBox.Text),
                 this.prsnTypeRsn, this.vldDte1,
                 this.vldDte2, this.prsnTypeFurDet, this.prsnTypTextBox.Text);

                //oldPrsID = Global.mnFrm.cmCde.getPrsnID(this.locIDTextBox.Text);
                //ListViewItem nwItem = new ListViewItem(new string[] {
                //"New",
                //this.locIDTextBox.Text,this.titleTextBox.Text + " " + 
                //    this.surnameTextBox.Text + ", " + this.firstNameTextBox.Text + " " + 
                //    this.otherNamesTextBox.Text,
                //oldPrsID.ToString(), oldPrsID.ToString() + ".png"});
                //this.prsNamesListView.Items.Insert(0,nwItem);
                //this.prsnIDTextBox.Text = oldPrsID.ToString();
                //bool rs = this.shdObeyPrsEvts();
                //this.obey_prs_evnts = false;
                //this.prsNamesListView.SelectedItems.Clear();
                //this.prsNamesListView.Items[0].Selected = true;
                //System.Windows.Forms.Application.DoEvents();
                //this.obey_prs_evnts = rs;
                System.Windows.Forms.Application.DoEvents();
                this.prsnIDTextBox.Text = Global.mnFrm.cmCde.getGnrlRecID(
                  "prs.prsn_names_nos",
                  "local_id_no", "person_id",
                  this.locIDTextBox.Text, Global.mnFrm.cmCde.Org_id).ToString();
                bool prv = this.obey_prs_evnts;
                this.obey_prs_evnts = false;
                ListViewItem nwItem = new ListViewItem(new string[] {
    "New",
    this.locIDTextBox.Text,
      this.titleTextBox.Text + " "+  this.surnameTextBox.Text+
          ", "+ this.firstNameTextBox.Text + " "+ this.otherNamesTextBox.Text,
          this.prsnIDTextBox.Text, this.imgTextBox.Text});
                this.prsNamesListView.Items.Insert(0, nwItem);
                for (int i = 0; i < this.prsNamesListView.SelectedItems.Count; i++)
                {
                    this.prsNamesListView.SelectedItems[i].Font = new Font("Tahoma", 8.25f, FontStyle.Regular);
                    this.prsNamesListView.SelectedItems[i].Selected = false;
                }
                this.prsNamesListView.Items[0].Selected = true;
                this.prsNamesListView.Items[0].Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
                this.obey_prs_evnts = prv;
                System.Windows.Forms.Application.DoEvents();
                //this.savePrnsExtrData(Global.mnFrm.cmCde.getPrsnID(this.locIDTextBox.Text));
                Global.mnFrm.cmCde.showMsg("Record Saved!", 3);

                this.savePrsButton.Enabled = true;
                this.addPrsn = false;
                this.editPrsn = true;
                this.addPrsButton.Enabled = false;
                if (this.editPrsns == true)
                {
                    this.editPrsButton.Enabled = true;
                    this.editPrsButton_Click(this.editPrsButton, e);
                }
                else
                {
                    this.disablePrsEdit();
                    System.Windows.Forms.Application.DoEvents();
                    this.loadOrgPersons();
                }
            }
            else if (this.editPrsn == true)
            {
                //if (this.curTabIndx == "prsnInfoTabPage")
                //{
                if (this.prsnTabControl.SelectedTab == this.prsnInfoTabPage)
                {
                    Global.updatePrsnBasic(long.Parse(this.prsnIDTextBox.Text), this.firstNameTextBox.Text,
                     this.surnameTextBox.Text, this.otherNamesTextBox.Text,
                     this.titleTextBox.Text, this.locIDTextBox.Text, int.Parse(this.prsOrgIDTextBox.Text),
                     this.genderTextBox.Text, this.maritalStatusTextBox.Text, this.dobTextBox.Text,
                     this.pobTextBox.Text, this.religionTextBox.Text, this.resAddrsTextBox.Text,
                     this.pstlAddrsTextBox.Text, this.emailTextBox.Text, this.telTextBox.Text,
                     this.moblTextBox.Text, this.faxTextBox.Text, this.hometownTextBox.Text,
                     this.ntnltyTextBox.Text, this.imgTextBox.Text, long.Parse(this.linkedFirmIDTextBox.Text),
                   long.Parse(this.linkedSiteIDTextBox.Text));
                    long prsntypRowID = -1;//this.prsnTypeRsn, this.prsnTypeFurDet,
                    if (Global.checkPrsnType(long.Parse(this.prsnIDTextBox.Text),
                       this.prsnTypTextBox.Text, this.vldDte1, ref prsntypRowID) == false)
                    {
                        Global.endOldPrsnTypes(long.Parse(this.prsnIDTextBox.Text), this.vldDte1);
                        Global.createPrsnsType(long.Parse(this.prsnIDTextBox.Text),
                    this.prsnTypeRsn, this.vldDte1,
                    this.vldDte2, this.prsnTypeFurDet, this.prsnTypTextBox.Text);
                    }
                    else if (prsntypRowID > 0)
                    {
                        Global.updtPrsnsType(prsntypRowID, long.Parse(this.prsnIDTextBox.Text),
                   this.prsnTypeRsn, this.vldDte1,
                   this.vldDte2, this.prsnTypeFurDet, this.prsnTypTextBox.Text);
                    }
                }
                if (this.prsNamesListView.SelectedItems.Count == 1)
                {
                    this.prsNamesListView.SelectedItems[0].SubItems[1].Text = this.locIDTextBox.Text;
                    this.prsNamesListView.SelectedItems[0].SubItems[2].Text = this.titleTextBox.Text + " " +
                      this.surnameTextBox.Text + ", " + this.firstNameTextBox.Text + " " +
                      this.otherNamesTextBox.Text;
                    this.prsNamesListView.SelectedItems[0].SubItems[4].Text = this.imgTextBox.Text;
                }
                if (this.prsPictureBox.Image == this.prsPictureBox.ErrorImage
                  || this.prsnDetPictureBox.Image == this.prsnDetPictureBox.ErrorImage)
                {
                    this.recreatePicBoxes();
                }
                if (this.prsNamesListView.SelectedItems.Count == 1)
                {
                    Global.mnFrm.cmCde.getDBImageFile(this.prsNamesListView.SelectedItems[0].SubItems[4].Text,
                 2, ref this.prsPictureBox);
                    Global.mnFrm.cmCde.getDBImageFile(this.prsNamesListView.SelectedItems[0].SubItems[4].Text,
                 2, ref this.prsnDetPictureBox);
                }
                //}

                if (this.prsnTabControl.SelectedTab == this.extraDataTabPage)
                {
                    this.savePrnsExtrData(Global.mnFrm.cmCde.getPrsnID(this.locIDTextBox.Text));
                }
                else
                {
                    this.saveGridVws();
                    Global.mnFrm.cmCde.showMsg("Record Saved!", 3);
                }
            }
        }

        private void changePrsPicButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[8]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Can only change the Picture of a saved Person!", 0);
                return;
            }
            //this.prsPictureBox.Image.Dispose();
            if (this.prsPictureBox.Image == this.prsPictureBox.ErrorImage
              || this.prsnDetPictureBox.Image == this.prsnDetPictureBox.ErrorImage)
            {
                this.recreatePicBoxes();
            }
            if (Global.mnFrm.cmCde.pickAnImage(long.Parse(this.prsnIDTextBox.Text),
             ref this.prsnDetPictureBox, 2) == true)
            {
                Global.updtPrsnImg(long.Parse(this.prsnIDTextBox.Text));
            }

            if (this.prsNamesListView.SelectedItems.Count == 1)
            {
                this.prsNamesListView.SelectedItems[0].SubItems[4].Text = this.prsnIDTextBox.Text + ".png";
                this.imgTextBox.Text = this.prsnIDTextBox.Text + ".png";
                //   Global.mnFrm.cmCde.getDBImageFile(this.prsNamesListView.SelectedItems[0].SubItems[4].Text,
                //2, ref this.prsPictureBox);
                System.Threading.Thread.Sleep(50);
                this.prsPictureBox.Image = this.prsnDetPictureBox.Image;
            }
            //this.populatePrsNames(long.Parse(this.prsnIDTextBox.Text));
        }

        private void savePrsPicButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.saveImageToFile(ref this.prsnDetPictureBox);
        }

        private void prsTypHstryButton_Click(object sender, EventArgs e)
        {
            if (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select a saved Record First!", 0);
                return;
            }
            prsnTypHstyDiag nwDiag = new prsnTypHstyDiag();
            nwDiag.prsnID = long.Parse(this.prsnIDTextBox.Text);
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
        }

        private void otherInfoButton_Click(object sender, EventArgs e)
        {
            if (this.prsnIDTextBox.Text == "" ||
             this.prsnIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("No record to View!", 0);
                return;
            }
            DialogResult dgres = this.cmCde.showRowsExtInfDiag(this.cmCde.getMdlGrpID("Person Data"),
             long.Parse(this.prsnIDTextBox.Text), "prs.prsn_all_other_info_table",
             this.locIDTextBox.Text, this.editPrsns, 5, 6, "prs.prsn_all_other_info_table_dflt_row_id_seq");
            if (dgres == DialogResult.OK)
            {
            }
        }

        private void rltvsButton_Click(object sender, EventArgs e)
        {
            if (this.prsnIDTextBox.Text == "" ||
          this.prsnIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("No record to View!", 0);
                return;
            }
            bool pv = this.searchAllOrgCheckBox.Checked;
            rltvsDiag nwDiag = new rltvsDiag();
            nwDiag.person_id = long.Parse(this.prsnIDTextBox.Text);
            DialogResult dgres = nwDiag.ShowDialog();
            if (dgres == DialogResult.OK)
            {
                if (this.searchForPrsTextBox.Text != "" && this.searchForPrsTextBox.Text != "%")
                {
                    this.goPrsButton_Click(this.goPrsButton, e);
                    this.searchAllOrgCheckBox.Checked = pv;
                }
            }
        }

        private void applyTmpltMenuItem_Click(object sender, EventArgs e)
        {
            this.applyTmpltButton_Click(this.applyTmpltButton, e);
        }

        private void applyTmpltButton_Click(object sender, EventArgs e)
        {
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[20]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            long[] selPrsnIDs = new long[1];
            if (this.prsNamesListView.SelectedItems.Count <= 1)
            {
                massAsgnTmpltsDiag msDiag = new massAsgnTmpltsDiag();
                msDiag.orgID = Global.mnFrm.cmCde.Org_id;
                msDiag.Location = new Point(this.applyTmpltButton.Location.X + 30,
                  this.applyTmpltButton.Location.Y + msDiag.Height + 30);
                DialogResult dgres1 = msDiag.ShowDialog();
                if (dgres1 == DialogResult.OK)
                {
                    selPrsnIDs = msDiag.prsnIDs;
                }
                else
                {
                    Global.mnFrm.cmCde.showMsg("Please select the saved Persons first!", 0);
                    return;
                }
            }
            else if (this.prsNamesListView.SelectedItems.Count > 1)
            {
                selPrsnIDs = new long[this.prsNamesListView.SelectedItems.Count];
                for (int i = 0; i < this.prsNamesListView.SelectedItems.Count; i++)
                {
                    selPrsnIDs[i] = long.Parse(this.prsNamesListView.SelectedItems[i].SubItems[3].Text);
                }
            }
            else
            {
                Global.mnFrm.cmCde.showMsg("Please select the saved Persons first!", 0);
                return;
            }

            asgnTmpltDiag nwDiag = new asgnTmpltDiag();
            nwDiag.orgID = Global.mnFrm.cmCde.Org_id;
            nwDiag.prsnIDs = selPrsnIDs;
            DialogResult dgres = nwDiag.ShowDialog();
            if (dgres == DialogResult.OK)
            {
                this.loadCorrectPanel();
            }
        }

        private void applyAsgnTmpltButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[20]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person's Record first!", 0);
                return;
            }
            asgnTmpltDiag nwDiag = new asgnTmpltDiag();
            nwDiag.orgID = Global.mnFrm.cmCde.Org_id;
            nwDiag.prsnIDs = new long[1];
            nwDiag.prsnIDs[0] = long.Parse(this.prsnIDTextBox.Text);
            DialogResult dgres = nwDiag.ShowDialog();
            if (dgres == DialogResult.OK)
            {
                this.loadCorrectPanel();
            }
        }

        private void exptPrsnMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.prsNamesListView);
        }

        private void searchForPrsTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.prs_cur_indx = 0;
                this.goPrsButton_Click(this.goPrsButton, ex);
            }
        }

        private void positionPrsTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                this.prsPnlNavButtons(this.movePreviousPrsButton, ex);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
            {
                this.prsPnlNavButtons(this.moveNextPrsButton, ex);
            }
        }

        private void prsnTabControl_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (this.shdObeyPrsEvts() == false)
            {
                return;
            }
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                //Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                this.prsnTabControl.SelectedTab = this.prsnInfoTabPage;
                return;
            }
            this.curTabIndx = this.prsnTabControl.SelectedTab.Name;
            this.loadCorrectPanel();
        }

        private void addPrsnMenuItem_Click(object sender, EventArgs e)
        {
            this.addPrsButton_Click(this.addPrsButton, e);
        }

        private void editPrsnMenuItem_Click(object sender, EventArgs e)
        {
            this.editPrsButton_Click(this.editPrsButton, e);
        }

        private void delPrsnMenuItem_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[9]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
        }

        private void refreshPrsnMenuItem_Click(object sender, EventArgs e)
        {
            //this.disableFormButtons();
            this.updatePhoneNumbers();
            this.goPrsButton_Click(this.goPrsButton, e);
        }

        bool IsValidEmail(string strIn)
        {
            // Return true if strIn is in valid e-mail format.
            return System.Text.RegularExpressions.Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        string removeInvalidChars(string s, string replaceWith)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                byte b = (byte)c;
                //Global.mnFrm.cmCde.showMsg(b.ToString() + "/" + c.ToString(), 0);
                if (b <= 32 || b >= 127)
                    result.Append(replaceWith);
                else
                    result.Append(c);
            }
            return result.ToString();
        }

        public void updatePhoneNumbers()
        {
            this.saveLabel.Text = "Reformating Contact Details...Please Wait...";
            this.saveLabel.Visible = true;
            System.Windows.Forms.Application.DoEvents();
            string strSQL = @"SELECT person_id, 
                           local_id_no,
                           email, 
                           cntct_no_tel, 
                           cntct_no_mobl,  
                           cntct_no_fax
                        FROM prs.prsn_names_nos 
                         WHERE 1=1 ORDER BY 1";
            DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSQL);
            int ttl = dtst.Tables[0].Rows.Count;
            for (int i = 0; i < ttl; i++)
            {
                this.saveLabel.Text = "Reformating Contact Details(" + (i + 1).ToString() + "/" + ttl + ")...Please Wait...";
                System.Windows.Forms.Application.DoEvents();
                string email = dtst.Tables[0].Rows[i][2].ToString();
                string cntcNo = dtst.Tables[0].Rows[i][3].ToString();
                string cntcMobl = dtst.Tables[0].Rows[i][4].ToString();
                string cntcFax = dtst.Tables[0].Rows[i][5].ToString();
                long prsnID = long.Parse(dtst.Tables[0].Rows[i][0].ToString());
                char[] w = { ',' };
                char[] trmChr = { ',', ' ' };
                email = removeInvalidChars(email, "").Replace(":", ",").Replace(";", ",").Replace("/", ",").Replace(@"\", ",").Replace(",", ", ").Trim(trmChr).ToLower();
                cntcNo = removeInvalidChars(cntcNo, "").Replace(":", ",").Replace(";", ",").Replace("/", ",").Replace(@"\", ",").Replace(",", ", ").Trim(trmChr);
                cntcMobl = removeInvalidChars(cntcMobl, "").Replace(":", ",").Replace(";", ",").Replace("/", ",").Replace(@"\", ",").Replace(",", ", ").Trim(trmChr);
                cntcFax = removeInvalidChars(cntcFax, "").Replace(":", ",").Replace(";", ",").Replace("/", ",").Replace(@"\", ",").Replace(",", ", ").Trim(trmChr);

                string[] emails = email.Split(w, StringSplitOptions.RemoveEmptyEntries);
                string[] cntcNos = cntcNo.Split(w, StringSplitOptions.RemoveEmptyEntries);
                string[] cntcMobls = cntcMobl.Split(w, StringSplitOptions.RemoveEmptyEntries);
                for (int y = 0; y < cntcMobls.Length; y++)
                {
                    if (cntcMobls[y].Trim().Length == 10)
                    {
                        if (cntcMobls[y].Trim().Substring(0, 1) == "0")
                        {
                            cntcMobls[y] = "+233" + cntcMobls[y].Trim().Substring(1);
                        }
                    }
                }
                for (int y = 0; y < cntcNos.Length; y++)
                {
                    if (cntcNos[y].Trim().Length == 10)
                    {
                        if (cntcNos[y].Trim().Substring(0, 1) == "0")
                        {
                            cntcNos[y] = "+233" + cntcNos[y].Trim().Substring(1);
                        }
                    }
                }
                string[] cntcFaxs = cntcFax.Split(w, StringSplitOptions.RemoveEmptyEntries);

                string updtSQL = @"UPDATE prs.prsn_names_nos SET 
                           email='" + email.Replace("'", "''") + @"', 
                           cntct_no_tel='" + string.Join(", ", cntcNos).Replace("   ", " ").Replace("  ", " ").Replace("'", "''").Trim(trmChr) + @"', 
                           cntct_no_mobl='" + string.Join(", ", cntcMobls).Replace("   ", " ").Replace("  ", " ").Replace("'", "''").Trim(trmChr) + @"',  
                           cntct_no_fax='" + cntcFax.Replace("\r\n", "").Replace("'", "''") + @"' WHERE person_id=" + prsnID;
                Global.mnFrm.cmCde.updateDataNoParams(updtSQL);
            }

            this.saveLabel.Visible = false;
            System.Windows.Forms.Application.DoEvents();
        }

        private void viewSQLPrsnMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(this.prs_SQL, 5);
        }

        private void recHstryPrsnMenuItem_Click(object sender, EventArgs e)
        {
            if (this.prsnIDTextBox.Text == "-1"
      || this.prsnIDTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(Global.get_Prs_Rec_Hstry(long.Parse(this.prsnIDTextBox.Text)), 6);
        }

        private void exptPrsnExclTmpMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtPsnInfoTmp();
        }

        private void imptPrsnExclTmpltMenuItem_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[7]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            this.openFileDialog1.RestoreDirectory = true;
            this.openFileDialog1.Filter = "All Files|*.*|Excel Files|*.xls;*.xlsx";
            this.openFileDialog1.FilterIndex = 2;
            this.openFileDialog1.Title = "Select an Excel File to Upload...";
            this.openFileDialog1.FileName = "";
            if (this.openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Global.mnFrm.cmCde.imprtPsnInfoTmp(this.openFileDialog1.FileName);
            }
            this.populatePrs();
        }

        private void exptRltvsMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtPsnRltvsTmp();
        }

        private void imprtRltvsMenuItem_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[7]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            this.openFileDialog1.RestoreDirectory = true;
            this.openFileDialog1.Filter = "All Files|*.*|Excel Files|*.xls;*.xlsx";
            this.openFileDialog1.FilterIndex = 2;
            this.openFileDialog1.Title = "Select an Excel File to Upload...";
            this.openFileDialog1.FileName = "";
            if (this.openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Global.mnFrm.cmCde.imprtPsnRltvsTmp(this.openFileDialog1.FileName);
            }
        }
        #endregion

        #region "NATIONALITY..."
        private void populateNatnlty(long prsnID)
        {
            this.obeyEvnts = false;
            DataSet dtst = Global.getAllNtnlty(prsnID);
            this.nationalityListView.Items.Clear();
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                ListViewItem nwItem = new ListViewItem(new string[] {
    (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][0].ToString(), dtst.Tables[0].Rows[i][1].ToString(),
    dtst.Tables[0].Rows[i][2].ToString()
                , dtst.Tables[0].Rows[i][3].ToString()
        , dtst.Tables[0].Rows[i][4].ToString()
        , dtst.Tables[0].Rows[i][5].ToString()
        , dtst.Tables[0].Rows[i][6].ToString()});
                this.nationalityListView.Items.Add(nwItem);
            }
            this.obeyEvnts = true;
        }

        private void addNationalityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.addNtnltyButton_Click(this.addNtnltyButton, e);
        }

        private void editNationalityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.editNtnltyButton_Click(this.editNtnltyButton, e);
        }

        private void exptNtnltyMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.nationalityListView);
        }

        private void exptNtnltyTmpMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtPsnNtlIDsTmp();
        }

        private void imprtNtnltyTmpMenuItem_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[7]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            this.openFileDialog1.RestoreDirectory = true;
            this.openFileDialog1.Filter = "All Files|*.*|Excel Files|*.xls;*.xlsx";
            this.openFileDialog1.FilterIndex = 2;
            this.openFileDialog1.Title = "Select an Excel File to Upload...";
            this.openFileDialog1.FileName = "";
            if (this.openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Global.mnFrm.cmCde.imprtPsnNtlIDsTmp(this.openFileDialog1.FileName);
            }
            this.populateNatnlty(long.Parse(this.prsnIDTextBox.Text));
        }

        private void deleteNtnltyMenuItem_Click(object sender, EventArgs e)
        {
            this.deleteNtnltyButton_Click(this.deleteNtnltyButton, e);
        }

        private void vwSQLNtnltyMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(Global.mnFrm.ntnlty_SQL, 5);
        }

        private void rcHstryNtnltyMenuItem_Click(object sender, EventArgs e)
        {
            if (this.nationalityListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(Global.get_Ntnlty_Rec_Hstry(long.Parse(this.nationalityListView.SelectedItems[0].SubItems[4].Text)), 6);
        }
        #endregion

        #region "EDUCATIONAL BACKGROUND..."
        private void populateEduc(long prsnID)
        {
            this.obeyEvnts = false;
            //if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[1]) == false)
            //{
            //  Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            //   " this action!\nContact your System Administrator!", 0);
            //  return;
            //}
            DataSet dtst = Global.getAllEduc(prsnID);
            this.educDataGridView.Rows.Clear();
            this.educDataGridView.RowCount = dtst.Tables[0].Rows.Count;
            //this.educDataGridView.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.educDataGridView.ReadOnly = true;
            this.educDataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.educDataGridView.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.saveEducButton.Enabled = false;
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.educDataGridView.Rows[i].HeaderCell.Value = (i + 1).ToString();
                Object[] cellDesc = new Object[15];
                cellDesc[0] = dtst.Tables[0].Rows[i][0].ToString();
                cellDesc[1] = "...";
                cellDesc[2] = dtst.Tables[0].Rows[i][1].ToString();
                cellDesc[3] = "...";
                cellDesc[4] = dtst.Tables[0].Rows[i][2].ToString();
                cellDesc[5] = "...";
                cellDesc[6] = dtst.Tables[0].Rows[i][3].ToString();
                string fendDte = dtst.Tables[0].Rows[i][4].ToString();
                if (fendDte == "31-Dec-4000")
                {
                    fendDte = "";
                }
                cellDesc[7] = fendDte;// dtst.Tables[0].Rows[i][4].ToString();
                cellDesc[8] = dtst.Tables[0].Rows[i][5].ToString();
                cellDesc[9] = "...";
                cellDesc[10] = dtst.Tables[0].Rows[i][6].ToString();
                cellDesc[11] = "...";
                cellDesc[12] = dtst.Tables[0].Rows[i][7].ToString();
                cellDesc[13] = dtst.Tables[0].Rows[i][8].ToString();
                cellDesc[14] = prsnID;
                this.educDataGridView.Rows[i].SetValues(cellDesc);
            }
            this.obeyEvnts = true;
        }

        private void addEducMenuItem_Click(object sender, EventArgs e)
        {
            this.addEducButton_Click(this.addEducButton, e);
        }

        private void editEducMenuItem_Click(object sender, EventArgs e)
        {
            this.editEducButton_Click(this.editEducButton, e);
        }

        private void delEducMenuItem_Click(object sender, EventArgs e)
        {
            this.delEducButton_Click(this.delEducButton, e);
        }

        private void refreshEducButton_Click(object sender, EventArgs e)
        {
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            if (this.prsNamesListView.SelectedItems.Count == 1)
            {
                this.populateEduc(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
            }
        }

        private void addEducButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[7]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            if (this.prsNamesListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Person First!", 0);
                return;
            }
            this.saveEducButton_Click(this.saveEducButton, e);
            string dateStr = DateTime.ParseExact(
      Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
            Global.createEduc(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text),
             "", "", "", "", dateStr.Substring(0, 11), dateStr.Substring(0, 11), "", "");
            this.populateEduc(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
            this.educDataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.educDataGridView.ReadOnly = false;
            this.saveEducButton.Enabled = true;
        }

        private void editEducButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[8]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            //this.educDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.educDataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.educDataGridView.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.educDataGridView.ReadOnly = false;
            this.educDataGridView.Columns[0].ReadOnly = true;
            this.educDataGridView.Columns[2].ReadOnly = true;
            this.educDataGridView.Columns[4].ReadOnly = true;
            this.educDataGridView.Columns[8].ReadOnly = true;
            this.educDataGridView.Columns[10].ReadOnly = true;
            this.saveEducButton.Enabled = true;
        }

        private void saveEducButton_Click(object sender, EventArgs e)
        {
            if (this.addPrsn == true)
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            for (int i = 0; i < this.educDataGridView.Rows.Count; i++)
            {
                Global.updateEduc(long.Parse(this.educDataGridView.Rows[i].Cells[13].Value.ToString()),
                 long.Parse(this.educDataGridView.Rows[i].Cells[14].Value.ToString()),
                this.educDataGridView.Rows[i].Cells[0].Value.ToString(),
                this.educDataGridView.Rows[i].Cells[2].Value.ToString(),
                 this.educDataGridView.Rows[i].Cells[4].Value.ToString(),
                 this.educDataGridView.Rows[i].Cells[8].Value.ToString(),
                 this.educDataGridView.Rows[i].Cells[6].Value.ToString(),
                 this.educDataGridView.Rows[i].Cells[7].Value.ToString(),
                 this.educDataGridView.Rows[i].Cells[12].Value.ToString(),
                 this.educDataGridView.Rows[i].Cells[10].Value.ToString());
            }
            this.educDataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.educDataGridView.ReadOnly = true;
            this.saveEducButton.Enabled = false;
        }

        private void educDataGridView_CellBeginEdit(object sender, System.Windows.Forms.DataGridViewCellCancelEventArgs e)
        {
            if (e == null /*|| this.obeyEvnts == false*/ || this.saveEducButton.Enabled == false)
            {
                return;
            }
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            this.educDataGridView.EndEdit();
            System.Windows.Forms.Application.DoEvents();
            if (e.ColumnIndex == 6)
            {
                this.GridDte1TextBox.Text = this.educDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                Global.mnFrm.cmCde.selectDate(ref this.GridDte1TextBox);
                if (this.GridDte1TextBox.Text.Length > 11)
                {
                    this.educDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = this.GridDte1TextBox.Text.Substring(0, 11);
                }
                this.educDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
                //this.educDataGridView.CurrentCell = this.educDataGridView.Rows[e.RowIndex].Cells[0];
            }
            else if (e.ColumnIndex == 7)
            {
                this.GridDte2TextBox.Text = this.educDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                Global.mnFrm.cmCde.selectDate(ref this.GridDte2TextBox);
                if (this.GridDte2TextBox.Text.Length > 11)
                {
                    this.educDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = this.GridDte2TextBox.Text.Substring(0, 11);
                }
                this.educDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
            }
            else if (e.ColumnIndex == 12)
            {
                this.GridDte3TextBox.Text = this.educDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                Global.mnFrm.cmCde.selectDate(ref this.GridDte3TextBox);
                if (this.GridDte3TextBox.Text.Length > 11)
                {
                    this.educDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = this.GridDte3TextBox.Text.Substring(0, 11);
                }
                this.educDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
            }
        }

        private void delEducButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[9]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            if (this.educDataGridView.CurrentCell != null && this.educDataGridView.SelectedRows.Count <= 0)
            {
                this.educDataGridView.Rows[this.educDataGridView.CurrentCell.RowIndex].Selected = true;
            }
            if (this.educDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the Row(s) to delete!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to delete the\r\nselected Educational Background(s)?", 1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            for (int i = 0; i < this.educDataGridView.SelectedRows.Count; i++)
            {
                Global.deleteEduc(
                  long.Parse(this.educDataGridView.SelectedRows[i].Cells[13].Value.ToString()),
                  this.locIDTextBox.Text);
            }
            this.populateEduc(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
        }

        private void exptEducMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.educDataGridView);
        }

        private void exprtEducBkgMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtPsnEducTmp();
        }

        private void imprtEducBkgMenuItem_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[7]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            this.openFileDialog1.RestoreDirectory = true;
            this.openFileDialog1.Filter = "All Files|*.*|Excel Files|*.xls;*.xlsx";
            this.openFileDialog1.FilterIndex = 2;
            this.openFileDialog1.Title = "Select an Excel File to Upload...";
            this.openFileDialog1.FileName = "";
            if (this.openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Global.mnFrm.cmCde.imprtPsnEducTmp(this.openFileDialog1.FileName);
            }
            this.populateEduc(long.Parse(this.prsnIDTextBox.Text));
        }

        private void rfrshEducMenuItem_Click(object sender, EventArgs e)
        {
            this.refreshEducButton_Click(this.refreshEducButton, e);
        }

        private void rcHstryEducMenuItem_Click(object sender, EventArgs e)
        {
            if (this.educDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(Global.get_Educ_Rec_Hstry(
              long.Parse(this.educDataGridView.SelectedRows[0].Cells[13].Value.ToString())), 6);
        }

        private void vwSQLEducMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(Global.mnFrm.educ_SQL, 5);
        }
        #endregion

        #region "WORK EXPERIENCE..."
        private void populateWrkExp(long prsnID)
        {
            //if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[1]) == false)
            //{
            //  Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            //   " this action!\nContact your System Administrator!", 0);
            //  return;
            //} 
            this.obeyEvnts = false;
            DataSet dtst = Global.getAllWrkExp(prsnID);
            this.wrkExpDataGridView.Rows.Clear();
            this.wrkExpDataGridView.RowCount = dtst.Tables[0].Rows.Count;
            //this.educDataGridView.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.wrkExpDataGridView.ReadOnly = true;
            this.wrkExpDataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.wrkExpDataGridView.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.saveWrkButton.Enabled = false;

            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.wrkExpDataGridView.Rows[i].HeaderCell.Value = (i + 1).ToString();
                Object[] cellDesc = new Object[12];
                cellDesc[0] = dtst.Tables[0].Rows[i][0].ToString();
                cellDesc[1] = "...";
                cellDesc[2] = dtst.Tables[0].Rows[i][1].ToString();
                cellDesc[3] = "...";
                cellDesc[4] = dtst.Tables[0].Rows[i][2].ToString();
                cellDesc[5] = "...";
                cellDesc[6] = dtst.Tables[0].Rows[i][3].ToString();
                string fendDte = dtst.Tables[0].Rows[i][4].ToString();
                if (fendDte == "31-Dec-4000")
                {
                    fendDte = "";
                }
                cellDesc[7] = fendDte;// dtst.Tables[0].Rows[i][4].ToString();
                cellDesc[8] = dtst.Tables[0].Rows[i][5].ToString();

                cellDesc[9] = dtst.Tables[0].Rows[i][6].ToString();
                cellDesc[10] = dtst.Tables[0].Rows[i][7].ToString();
                cellDesc[11] = prsnID;
                this.wrkExpDataGridView.Rows[i].SetValues(cellDesc);
            }

            this.obeyEvnts = true;
        }

        private void addWrkButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[7]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            if (this.prsNamesListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Person First!", 0);
                return;
            }
            this.saveWrkButton_Click(this.saveWrkButton, e);
            string dateStr = DateTime.ParseExact(
      Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
            Global.createWrkExp(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text),
             "", "", "", "", dateStr.Substring(0, 11), dateStr.Substring(0, 11), "");
            this.populateWrkExp(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
            this.wrkExpDataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.wrkExpDataGridView.ReadOnly = false;
            this.saveWrkButton.Enabled = true;
        }

        private void editWrkButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[8]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            this.wrkExpDataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.wrkExpDataGridView.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.wrkExpDataGridView.ReadOnly = false;
            this.wrkExpDataGridView.Columns[0].ReadOnly = true;
            this.wrkExpDataGridView.Columns[2].ReadOnly = true;
            this.wrkExpDataGridView.Columns[4].ReadOnly = true;
            this.saveWrkButton.Enabled = true;
        }

        private void delWrkButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[9]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            if (this.wrkExpDataGridView.CurrentCell != null && this.wrkExpDataGridView.SelectedRows.Count <= 0)
            {
                this.wrkExpDataGridView.Rows[this.wrkExpDataGridView.CurrentCell.RowIndex].Selected = true;
            }
            if (this.wrkExpDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the Row(s) to delete!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to delete the\r\nselected Work Experience(s)?", 1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            for (int i = 0; i < this.wrkExpDataGridView.SelectedRows.Count; i++)
            {
                Global.deleteWrkExp(
                  long.Parse(this.wrkExpDataGridView.SelectedRows[i].Cells[10].Value.ToString()),
                  this.locIDTextBox.Text);
            }
            this.populateWrkExp(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
        }

        private void saveWrkButton_Click(object sender, EventArgs e)
        {
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            for (int i = 0; i < this.wrkExpDataGridView.Rows.Count; i++)
            {
                Global.updateWrkExp(long.Parse(this.wrkExpDataGridView.Rows[i].Cells[11].Value.ToString()),
                 long.Parse(this.wrkExpDataGridView.Rows[i].Cells[10].Value.ToString()),
                this.wrkExpDataGridView.Rows[i].Cells[0].Value.ToString(),
                this.wrkExpDataGridView.Rows[i].Cells[2].Value.ToString(),
                 this.wrkExpDataGridView.Rows[i].Cells[4].Value.ToString(),
                 this.wrkExpDataGridView.Rows[i].Cells[8].Value.ToString(),
                 this.wrkExpDataGridView.Rows[i].Cells[6].Value.ToString(),
                 this.wrkExpDataGridView.Rows[i].Cells[7].Value.ToString(),
                 this.wrkExpDataGridView.Rows[i].Cells[9].Value.ToString());
            }
            this.wrkExpDataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.wrkExpDataGridView.ReadOnly = true;
            this.saveWrkButton.Enabled = false;
        }

        private void refreshWrkButton_Click(object sender, EventArgs e)
        {
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            if (this.prsNamesListView.SelectedItems.Count == 1)
            {
                this.populateWrkExp(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
            }
        }

        private void wrkExpDataGridView_CellBeginEdit(object sender,
         System.Windows.Forms.DataGridViewCellCancelEventArgs e)
        {
            if (e == null /*|| this.obeyEvnts == false*/ || this.saveWrkButton.Enabled == false)
            {
                return;
            }
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == 6)
            {
                this.GridDte1TextBox.Text = this.wrkExpDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                Global.mnFrm.cmCde.selectDate(ref this.GridDte1TextBox);
                if (this.GridDte1TextBox.Text.Length > 11)
                {
                    this.wrkExpDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = this.GridDte1TextBox.Text.Substring(0, 11);
                }
                this.wrkExpDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
            }
            else if (e.ColumnIndex == 7)
            {
                this.GridDte2TextBox.Text = this.wrkExpDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                Global.mnFrm.cmCde.selectDate(ref this.GridDte2TextBox);
                if (this.GridDte2TextBox.Text.Length > 11)
                {
                    this.wrkExpDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = this.GridDte2TextBox.Text.Substring(0, 11);
                }
                this.wrkExpDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
            }
        }

        private void exptWkExpMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.wrkExpDataGridView);
        }

        private void exprtWkExpMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtPsnJobExpTmp();
        }

        private void imprtWkExpMenuItem_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[7]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            this.openFileDialog1.RestoreDirectory = true;
            this.openFileDialog1.Filter = "All Files|*.*|Excel Files|*.xls;*.xlsx";
            this.openFileDialog1.FilterIndex = 2;
            this.openFileDialog1.Title = "Select an Excel File to Upload...";
            this.openFileDialog1.FileName = "";
            if (this.openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Global.mnFrm.cmCde.imprtPsnJobExpTmp(this.openFileDialog1.FileName);
            }
            this.populateWrkExp(long.Parse(this.prsnIDTextBox.Text));
        }

        private void addWkExpMenuItem_Click(object sender, EventArgs e)
        {
            this.addWrkButton_Click(this.addWrkButton, e);
        }

        private void editWkExpMenuItem_Click(object sender, EventArgs e)
        {
            this.editWrkButton_Click(this.editWrkButton, e);
        }

        private void delWkExpMenuItem_Click(object sender, EventArgs e)
        {
            this.delWrkButton_Click(this.delWrkButton, e);
        }

        private void rfrshWkExpMenuItem_Click(object sender, EventArgs e)
        {
            this.refreshWrkButton_Click(this.refreshWrkButton, e);
        }

        private void rcHstryWkExpMenuItem_Click(object sender, EventArgs e)
        {
            if (this.wrkExpDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(Global.get_WrkExp_Rec_Hstry(
              long.Parse(this.wrkExpDataGridView.SelectedRows[0].Cells[12].Value.ToString())), 6);
        }

        private void vwSQLWkExpMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(Global.mnFrm.wrkExp_SQL, 5);
        }
        #endregion

        #region "SKILLS/NATURE..."
        private void populateSkills(long prsnID)
        {
            this.obeyEvnts = false;
            //if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[1]) == false)
            //{
            //  Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            //   " this action!\nContact your System Administrator!", 0);
            //  return;
            //}
            DataSet dtst = Global.getAllSkills(prsnID);
            this.skillsDataGridView.Rows.Clear();
            this.skillsDataGridView.RowCount = dtst.Tables[0].Rows.Count;
            this.skillsDataGridView.ReadOnly = true;
            this.skillsDataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.skillsDataGridView.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.saveSkillButton.Enabled = false;
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.skillsDataGridView.Rows[i].HeaderCell.Value = (i + 1).ToString();
                Object[] cellDesc = new Object[14];
                cellDesc[0] = dtst.Tables[0].Rows[i][0].ToString();
                cellDesc[1] = "...";
                cellDesc[2] = dtst.Tables[0].Rows[i][1].ToString();
                cellDesc[3] = "...";
                cellDesc[4] = dtst.Tables[0].Rows[i][2].ToString();
                cellDesc[5] = "...";
                cellDesc[6] = dtst.Tables[0].Rows[i][3].ToString();
                cellDesc[7] = "...";
                cellDesc[8] = dtst.Tables[0].Rows[i][4].ToString();
                cellDesc[9] = "...";
                cellDesc[10] = dtst.Tables[0].Rows[i][5].ToString();
                string fendDte = dtst.Tables[0].Rows[i][6].ToString();
                if (fendDte == "31-Dec-4000")
                {
                    fendDte = "";
                }
                cellDesc[11] = fendDte;
                cellDesc[12] = dtst.Tables[0].Rows[i][7].ToString();
                cellDesc[13] = prsnID;
                this.skillsDataGridView.Rows[i].SetValues(cellDesc);
            }
            this.obeyEvnts = true;
        }

        private void addSkillButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[7]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            if (this.prsNamesListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Person First!", 0);
                return;
            }
            this.saveSkillButton_Click(this.saveSkillButton, e);
            string dateStr = DateTime.ParseExact(
      Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
            Global.createSkill(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text),
             "", "", "", "", "", dateStr.Substring(0, 11), "31-Dec-4000");
            this.populateSkills(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
            this.skillsDataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.skillsDataGridView.ReadOnly = false;
            this.saveSkillButton.Enabled = true;
        }

        private void editSkillButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[8]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            this.skillsDataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.skillsDataGridView.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.skillsDataGridView.ReadOnly = false;
            this.skillsDataGridView.Columns[0].ReadOnly = true;
            this.skillsDataGridView.Columns[2].ReadOnly = true;
            this.skillsDataGridView.Columns[4].ReadOnly = true;
            this.skillsDataGridView.Columns[6].ReadOnly = true;
            this.skillsDataGridView.Columns[8].ReadOnly = true;
            this.saveSkillButton.Enabled = true;
        }

        private void delSkillButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[9]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            if (this.skillsDataGridView.CurrentCell != null && this.skillsDataGridView.SelectedRows.Count <= 0)
            {
                this.skillsDataGridView.Rows[this.skillsDataGridView.CurrentCell.RowIndex].Selected = true;
            }
            if (this.skillsDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the Row(s) to delete!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to delete the\r\nselected Work Experience(s)?", 1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            for (int i = 0; i < this.skillsDataGridView.SelectedRows.Count; i++)
            {
                Global.deleteSkill(
                  long.Parse(this.skillsDataGridView.SelectedRows[i].Cells[12].Value.ToString()),
                  this.locIDTextBox.Text);
            }
            this.populateSkills(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
        }

        private void saveSkillButton_Click(object sender, EventArgs e)
        {
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            for (int i = 0; i < this.skillsDataGridView.Rows.Count; i++)
            {
                Global.updateSkill(long.Parse(this.skillsDataGridView.Rows[i].Cells[13].Value.ToString()),
                 long.Parse(this.skillsDataGridView.Rows[i].Cells[12].Value.ToString()),
                this.skillsDataGridView.Rows[i].Cells[0].Value.ToString(),
                this.skillsDataGridView.Rows[i].Cells[2].Value.ToString(),
                 this.skillsDataGridView.Rows[i].Cells[4].Value.ToString(),
                 this.skillsDataGridView.Rows[i].Cells[6].Value.ToString(),
                 this.skillsDataGridView.Rows[i].Cells[8].Value.ToString(),
                 this.skillsDataGridView.Rows[i].Cells[10].Value.ToString(),
                 this.skillsDataGridView.Rows[i].Cells[11].Value.ToString());
            }
            this.skillsDataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.skillsDataGridView.ReadOnly = true;
            this.saveSkillButton.Enabled = false;
        }

        private void refreshSkillButton_Click(object sender, EventArgs e)
        {
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            if (this.prsNamesListView.SelectedItems.Count == 1)
            {
                this.populateSkills(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
            }
        }

        private void skillsDataGridView_CellBeginEdit(object sender, System.Windows.Forms.DataGridViewCellCancelEventArgs e)
        {
            if (e == null /*|| this.obeyEvnts == false*/ || this.saveSkillButton.Enabled == false)
            {
                return;
            }
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == 10)
            {
                this.GridDte1TextBox.Text = this.skillsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                Global.mnFrm.cmCde.selectDate(ref this.GridDte1TextBox);
                if (this.GridDte1TextBox.Text.Length > 11)
                {
                    this.skillsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = this.GridDte1TextBox.Text.Substring(0, 11);
                }
                this.skillsDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
            }
            else if (e.ColumnIndex == 11)
            {
                this.GridDte2TextBox.Text = this.skillsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                Global.mnFrm.cmCde.selectDate(ref this.GridDte2TextBox);
                if (this.GridDte2TextBox.Text.Length > 11)
                {
                    this.skillsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = this.GridDte2TextBox.Text.Substring(0, 11);
                }
                this.skillsDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
            }
        }

        private void exptSkllMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.skillsDataGridView);
        }

        private void exprtSkllTmpMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtPsnSkllNatrTmp();
        }

        private void imprtSkllTmpMenuItem_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[7]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            this.openFileDialog1.RestoreDirectory = true;
            this.openFileDialog1.Filter = "All Files|*.*|Excel Files|*.xls;*.xlsx";
            this.openFileDialog1.FilterIndex = 2;
            this.openFileDialog1.Title = "Select an Excel File to Upload...";
            this.openFileDialog1.FileName = "";
            if (this.openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Global.mnFrm.cmCde.imprtPsnSkllNatrTmp(this.openFileDialog1.FileName);
            }
            this.populateSkills(long.Parse(this.prsnIDTextBox.Text));
        }

        private void addSkllMenuItem_Click(object sender, EventArgs e)
        {
            this.addSkillButton_Click(this.addSkillButton, e);
        }

        private void editSkllMenuItem_Click(object sender, EventArgs e)
        {
            this.editSkillButton_Click(this.editSkillButton, e);
        }

        private void delSkllMenuItem_Click(object sender, EventArgs e)
        {
            this.delSkillButton_Click(this.delSkillButton, e);
        }

        private void rfrshSkllMenuItem_Click(object sender, EventArgs e)
        {
            this.refreshSkillButton_Click(this.refreshSkillButton, e);
        }

        private void rcHstrySkllMenuItem_Click(object sender, EventArgs e)
        {
            if (this.skillsDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(Global.get_Skill_Rec_Hstry(
              long.Parse(this.skillsDataGridView.SelectedRows[0].Cells[12].Value.ToString())), 6);
        }

        private void vwSQLSkllMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(Global.mnFrm.skill_SQL, 5);
        }
        #endregion

        #region "ASSIGNED DIVISIONS..."
        private void populateDivs(long prsnID)
        {
            this.obeyEvnts = false;
            DataSet dtst = Global.getAllDivs(prsnID);
            this.divsDataGridView.Rows.Clear();
            this.divsDataGridView.RowCount = dtst.Tables[0].Rows.Count;
            this.divsDataGridView.ReadOnly = true;
            this.divsDataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.divsDataGridView.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.saveDivButton.Enabled = false;
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.divsDataGridView.Rows[i].HeaderCell.Value = (i + 1).ToString();
                Object[] cellDesc = new Object[9];
                cellDesc[0] = Global.mnFrm.cmCde.getDivName(int.Parse(dtst.Tables[0].Rows[i][0].ToString()));
                cellDesc[1] = "...";
                cellDesc[2] = Global.mnFrm.cmCde.getPssblValNm(int.Parse(dtst.Tables[0].Rows[i][4].ToString()));
                cellDesc[3] = "...";
                cellDesc[4] = dtst.Tables[0].Rows[i][1].ToString();
                string fendDte = dtst.Tables[0].Rows[i][2].ToString();
                if (fendDte == "31-Dec-4000")
                {
                    fendDte = "";
                }
                cellDesc[5] = fendDte;// dtst.Tables[0].Rows[i][2].ToString();
                cellDesc[6] = dtst.Tables[0].Rows[i][0].ToString();
                cellDesc[7] = dtst.Tables[0].Rows[i][3].ToString();
                cellDesc[8] = prsnID;
                this.divsDataGridView.Rows[i].SetValues(cellDesc);
            }
            this.obeyEvnts = true;
        }

        private void addDivButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[10]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            if (this.prsNamesListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Person First!", 0);
                return;
            }
            this.saveDivButton_Click(this.saveDivButton, e);
            string dateStr = DateTime.ParseExact(
      Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
            Global.createDiv(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text),
             -1, dateStr.Substring(0, 11), "31-Dec-4000");
            this.populateDivs(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
            this.divsDataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.divsDataGridView.ReadOnly = false;
            this.divsDataGridView.Columns[0].ReadOnly = true;
            this.divsDataGridView.Columns[2].ReadOnly = true;
            this.saveDivButton.Enabled = true;
        }

        private void editDivButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[11]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            this.divsDataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.divsDataGridView.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.divsDataGridView.ReadOnly = false;
            this.divsDataGridView.Columns[0].ReadOnly = true;
            this.divsDataGridView.Columns[2].ReadOnly = true;
            this.saveDivButton.Enabled = true;

        }

        private void delDivButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[12]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            if (this.divsDataGridView.CurrentCell != null && this.divsDataGridView.SelectedRows.Count <= 0)
            {
                this.divsDataGridView.Rows[this.divsDataGridView.CurrentCell.RowIndex].Selected = true;
            }
            if (this.divsDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the Row(s) to delete!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to delete the\r\nselected Assigned Group(s)?", 1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            for (int i = 0; i < this.divsDataGridView.SelectedRows.Count; i++)
            {
                Global.deleteDiv(
                  long.Parse(this.divsDataGridView.SelectedRows[i].Cells[7].Value.ToString()),
                  this.locIDTextBox.Text);
            }
            this.populateDivs(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
        }

        private void refreshDivButton_Click(object sender, EventArgs e)
        {
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            if (this.prsNamesListView.SelectedItems.Count == 1)
            {
                this.populateDivs(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
            }
        }

        private void saveDivButton_Click(object sender, EventArgs e)
        {
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            for (int i = 0; i < this.divsDataGridView.Rows.Count; i++)
            {
                Global.updateDiv(long.Parse(this.divsDataGridView.Rows[i].Cells[8].Value.ToString()),
                 long.Parse(this.divsDataGridView.Rows[i].Cells[7].Value.ToString()),
                int.Parse(this.divsDataGridView.Rows[i].Cells[6].Value.ToString()),
                this.divsDataGridView.Rows[i].Cells[4].Value.ToString(),
                 this.divsDataGridView.Rows[i].Cells[5].Value.ToString());
            }
            this.divsDataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.divsDataGridView.ReadOnly = true;
            this.saveDivButton.Enabled = false;
        }

        private void divsDataGridView_CellBeginEdit(object sender,
         System.Windows.Forms.DataGridViewCellCancelEventArgs e)
        {
            if (e == null /*|| this.obeyEvnts == false*/ || this.saveDivButton.Enabled == false)
            {
                return;
            }
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == 4)
            {
                this.bscDte1TextBox.Text = this.divsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                Global.mnFrm.cmCde.selectDate(ref this.bscDte1TextBox);
                if (this.bscDte1TextBox.Text.Length > 11)
                {
                    this.divsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = this.bscDte1TextBox.Text.Substring(0, 11);
                }
                this.divsDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
            }
            else if (e.ColumnIndex == 5)
            {
                this.bscDte1TextBox.Text = this.divsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                Global.mnFrm.cmCde.selectDate(ref this.bscDte1TextBox);
                if (this.bscDte1TextBox.Text.Length > 11)
                {
                    this.divsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = this.bscDte1TextBox.Text.Substring(0, 11);
                }
                this.divsDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
            }

        }

        private void exptDivMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.divsDataGridView);
        }

        private void editDivMenuItem_Click(object sender, EventArgs e)
        {
            this.editDivButton_Click(this.editDivButton, e);
        }

        private void addDivMenuItem_Click(object sender, EventArgs e)
        {
            this.addDivButton_Click(this.addDivButton, e);
        }

        private void delDivMenuItem_Click(object sender, EventArgs e)
        {
            this.delDivButton_Click(this.delDivButton, e);
        }

        private void rfrshDivMenuItem_Click(object sender, EventArgs e)
        {
            this.refreshDivButton_Click(this.refreshDivButton, e);
        }

        private void rcHstryDivMenuItem_Click(object sender, EventArgs e)
        {
            if (this.divsDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(Global.get_Div_Rec_Hstry(
              long.Parse(this.divsDataGridView.SelectedRows[0].Cells[7].Value.ToString())), 6);
        }

        private void vwSQLDivMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(Global.mnFrm.div_SQL, 5);
        }

        private void grpsExptExclMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtPsnDivAsgmtsTmp();
        }

        private void grpsImptExclMenuItem_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[10]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            this.openFileDialog1.RestoreDirectory = true;
            this.openFileDialog1.Filter = "All Files|*.*|Excel Files|*.xls;*.xlsx";
            this.openFileDialog1.FilterIndex = 2;
            this.openFileDialog1.Title = "Select an Excel File to Upload...";
            this.openFileDialog1.FileName = "";
            if (this.openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Global.mnFrm.cmCde.imprtPsnDivAsgmtsTmp(this.openFileDialog1.FileName);
            }
            this.loadPersBscAssgmnts();
        }
        #endregion

        #region "ASSIGNED SITES..."
        private void populateSites(long prsnID)
        {
            this.obeyEvnts = false;
            DataSet dtst = Global.getAllSites(prsnID);
            this.sitesDataGridView.Rows.Clear();
            this.sitesDataGridView.RowCount = dtst.Tables[0].Rows.Count;
            this.sitesDataGridView.ReadOnly = true;
            this.sitesDataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.sitesDataGridView.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.saveLocButton.Enabled = false;
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.sitesDataGridView.Rows[i].HeaderCell.Value = (i + 1).ToString();
                Object[] cellDesc = new Object[7];
                cellDesc[0] = Global.mnFrm.cmCde.getSiteName(int.Parse(dtst.Tables[0].Rows[i][0].ToString()));
                cellDesc[1] = "...";
                cellDesc[2] = dtst.Tables[0].Rows[i][1].ToString();

                string fendDte = dtst.Tables[0].Rows[i][2].ToString();
                if (fendDte == "31-Dec-4000")
                {
                    fendDte = "";
                }
                cellDesc[3] = fendDte;// dtst.Tables[0].Rows[i][2].ToString();
                cellDesc[4] = dtst.Tables[0].Rows[i][0].ToString();
                cellDesc[5] = dtst.Tables[0].Rows[i][3].ToString();
                cellDesc[6] = prsnID;
                this.sitesDataGridView.Rows[i].SetValues(cellDesc);
            }
            this.obeyEvnts = true;
        }

        private void addLocButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[10]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            if (this.prsNamesListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Person First!", 0);
                return;
            }
            this.saveLocButton_Click(this.saveLocButton, e);
            string dateStr = DateTime.ParseExact(
      Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
            Global.createLoc(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text),
             -1, dateStr.Substring(0, 11), "31-Dec-4000");
            this.populateSites(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
            this.sitesDataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.sitesDataGridView.ReadOnly = false;
            this.sitesDataGridView.Columns[0].ReadOnly = true;
            this.saveLocButton.Enabled = true;
        }

        private void editLocButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[11]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            this.sitesDataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.sitesDataGridView.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.sitesDataGridView.ReadOnly = false;
            this.sitesDataGridView.Columns[0].ReadOnly = true;
            this.saveLocButton.Enabled = true;
        }

        private void delLocButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[12]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            if (this.sitesDataGridView.CurrentCell != null && this.sitesDataGridView.SelectedRows.Count <= 0)
            {
                this.sitesDataGridView.Rows[this.sitesDataGridView.CurrentCell.RowIndex].Selected = true;
            }
            if (this.sitesDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the Row(s) to delete!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to delete the" +
             "\r\nselected Assigned Location(s)?", 1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            for (int i = 0; i < this.sitesDataGridView.SelectedRows.Count; i++)
            {
                Global.deleteLoc(
                  long.Parse(this.sitesDataGridView.SelectedRows[i].Cells[5].Value.ToString()),
                  this.locIDTextBox.Text);
            }
            this.populateSites(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
        }

        private void refreshLocButton_Click(object sender, EventArgs e)
        {
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            if (this.prsNamesListView.SelectedItems.Count == 1)
            {
                this.populateSites(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
            }
        }

        private void saveLocButton_Click(object sender, EventArgs e)
        {
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            for (int i = 0; i < this.sitesDataGridView.Rows.Count; i++)
            {
                Global.updateLoc(long.Parse(this.sitesDataGridView.Rows[i].Cells[6].Value.ToString()),
                 long.Parse(this.sitesDataGridView.Rows[i].Cells[5].Value.ToString()),
                int.Parse(this.sitesDataGridView.Rows[i].Cells[4].Value.ToString()),
                this.sitesDataGridView.Rows[i].Cells[2].Value.ToString(),
                 this.sitesDataGridView.Rows[i].Cells[3].Value.ToString());
            }
            this.sitesDataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.sitesDataGridView.ReadOnly = true;
            this.saveLocButton.Enabled = false;
        }

        private void sitesDataGridView_CellBeginEdit(object sender,
         System.Windows.Forms.DataGridViewCellCancelEventArgs e)
        {
            if (e == null || this.saveLocButton.Enabled == false)
            {
                return;
            }
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == 2)
            {
                this.bscDte1TextBox.Text = this.sitesDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                Global.mnFrm.cmCde.selectDate(ref this.bscDte1TextBox);
                if (this.bscDte1TextBox.Text.Length > 11)
                {
                    this.sitesDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = this.bscDte1TextBox.Text.Substring(0, 11);
                }
                this.sitesDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
            }
            else if (e.ColumnIndex == 3)
            {
                this.bscDte1TextBox.Text = this.sitesDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                Global.mnFrm.cmCde.selectDate(ref this.bscDte1TextBox);
                if (this.bscDte1TextBox.Text.Length > 11)
                {
                    this.sitesDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = this.bscDte1TextBox.Text.Substring(0, 11);
                }
                this.sitesDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
            }
        }

        private void exptSiteMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.sitesDataGridView);
        }

        private void addSiteMenuItem_Click(object sender, EventArgs e)
        {
            this.addLocButton_Click(this.addLocButton, e);
        }

        private void editSiteMenuItem_Click(object sender, EventArgs e)
        {
            this.editLocButton_Click(this.editLocButton, e);
        }

        private void delSiteMenuItem_Click(object sender, EventArgs e)
        {
            this.delLocButton_Click(this.delLocButton, e);
        }

        private void rfrshSiteMenuItem_Click(object sender, EventArgs e)
        {
            this.refreshLocButton_Click(this.refreshLocButton, e);
        }

        private void rcHstrySiteMenuItem_Click(object sender, EventArgs e)
        {
            if (this.sitesDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(Global.get_Site_Rec_Hstry(
              long.Parse(this.sitesDataGridView.SelectedRows[0].Cells[5].Value.ToString())), 6);
        }

        private void vwSQLSiteMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(Global.mnFrm.site_SQL, 5);
        }

        private void siteImptExclMenuItem_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[10]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            this.openFileDialog1.RestoreDirectory = true;
            this.openFileDialog1.Filter = "All Files|*.*|Excel Files|*.xls;*.xlsx";
            this.openFileDialog1.FilterIndex = 2;
            this.openFileDialog1.Title = "Select an Excel File to Upload...";
            this.openFileDialog1.FileName = "";
            if (this.openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Global.mnFrm.cmCde.imprtPsnLocAsgmtsTmp(this.openFileDialog1.FileName);
            }
            this.loadPersBscAssgmnts();
        }

        private void siteExptExclMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtPsnLocAsgmtsTmp();
        }
        #endregion

        #region "ASSIGNED SUPERVISORS..."
        private void populateSpvsr(long prsnID)
        {
            this.obeyEvnts = false;
            DataSet dtst = Global.getAllSpvsr(prsnID);
            this.sprvisrDataGridView.Rows.Clear();
            this.sprvisrDataGridView.RowCount = dtst.Tables[0].Rows.Count;
            this.sprvisrDataGridView.ReadOnly = true;
            this.sprvisrDataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.sprvisrDataGridView.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.saveSprvsrButton.Enabled = false;
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.sprvisrDataGridView.Rows[i].HeaderCell.Value = (i + 1).ToString();
                Object[] cellDesc = new Object[7];
                cellDesc[0] = Global.mnFrm.cmCde.getPrsnName(long.Parse(dtst.Tables[0].Rows[i][0].ToString()));
                cellDesc[1] = "...";
                cellDesc[2] = dtst.Tables[0].Rows[i][1].ToString();
                string fendDte = dtst.Tables[0].Rows[i][2].ToString();
                if (fendDte == "31-Dec-4000")
                {
                    fendDte = "";
                }
                cellDesc[3] = fendDte;
                //cellDesc[3] = dtst.Tables[0].Rows[i][2].ToString();
                cellDesc[4] = dtst.Tables[0].Rows[i][0].ToString();
                cellDesc[5] = dtst.Tables[0].Rows[i][3].ToString();
                cellDesc[6] = prsnID;
                this.sprvisrDataGridView.Rows[i].SetValues(cellDesc);
            }
            this.obeyEvnts = true;
        }

        private void addSprvsrButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[10]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            if (this.prsNamesListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Person First!", 0);
                return;
            }
            this.saveSprvsrButton_Click(this.saveSprvsrButton, e);
            string dateStr = DateTime.ParseExact(
      Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
            Global.createSpvsr(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text),
             -1, dateStr.Substring(0, 11), "31-Dec-4000");
            this.populateSpvsr(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
            this.sprvisrDataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.sprvisrDataGridView.ReadOnly = false;
            this.sprvisrDataGridView.Columns[0].ReadOnly = true;
            this.saveSprvsrButton.Enabled = true;
        }

        private void editSprvsrButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[11]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            this.sprvisrDataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.sprvisrDataGridView.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.sprvisrDataGridView.ReadOnly = false;
            this.sprvisrDataGridView.Columns[0].ReadOnly = true;
            this.saveSprvsrButton.Enabled = true;
        }

        private void delSprvsrButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[12]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            if (this.sprvisrDataGridView.CurrentCell != null && this.sprvisrDataGridView.SelectedRows.Count <= 0)
            {
                this.sprvisrDataGridView.Rows[this.sprvisrDataGridView.CurrentCell.RowIndex].Selected = true;
            }
            if (this.sprvisrDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the Row(s) to delete!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to delete the" +
             "\r\nselected Assigned Supervisor(s)?", 1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            for (int i = 0; i < this.sprvisrDataGridView.SelectedRows.Count; i++)
            {
                Global.deleteSpvsr(
                  long.Parse(this.sprvisrDataGridView.SelectedRows[i].Cells[5].Value.ToString()),
                  this.locIDTextBox.Text);
            }
            this.populateSpvsr(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
        }

        private void saveSprvsrButton_Click(object sender, EventArgs e)
        {
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            for (int i = 0; i < this.sprvisrDataGridView.Rows.Count; i++)
            {
                Global.updtSpvsr(long.Parse(this.sprvisrDataGridView.Rows[i].Cells[6].Value.ToString()),
                 long.Parse(this.sprvisrDataGridView.Rows[i].Cells[5].Value.ToString()),
                int.Parse(this.sprvisrDataGridView.Rows[i].Cells[4].Value.ToString()),
                this.sprvisrDataGridView.Rows[i].Cells[2].Value.ToString(),
                 this.sprvisrDataGridView.Rows[i].Cells[3].Value.ToString());
            }
            this.sprvisrDataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.sprvisrDataGridView.ReadOnly = true;
            this.saveSprvsrButton.Enabled = false;
        }

        private void sprvisrDataGridView_CellBeginEdit(object sender,
         System.Windows.Forms.DataGridViewCellCancelEventArgs e)
        {
            if (e == null /*|| this.obeyEvnts == false*/ || this.saveSprvsrButton.Enabled == false)
            {
                return;
            }
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == 2)
            {
                this.bscDte1TextBox.Text = this.sprvisrDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                Global.mnFrm.cmCde.selectDate(ref this.bscDte1TextBox);
                if (this.bscDte1TextBox.Text.Length > 11)
                {
                    this.sprvisrDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = this.bscDte1TextBox.Text.Substring(0, 11);
                }
                this.sprvisrDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
            }
            else if (e.ColumnIndex == 3)
            {
                this.bscDte1TextBox.Text = this.sprvisrDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                Global.mnFrm.cmCde.selectDate(ref this.bscDte1TextBox);
                if (this.bscDte1TextBox.Text.Length > 11)
                {
                    this.sprvisrDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = this.bscDte1TextBox.Text.Substring(0, 11);
                }
                this.sprvisrDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
            }
        }

        private void refreshSprvsrButton_Click(object sender, EventArgs e)
        {
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            if (this.prsNamesListView.SelectedItems.Count == 1)
            {
                this.populateSpvsr(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
            }
        }

        private void exptSpvsrMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.sprvisrDataGridView);
        }

        private void addSpvsrMenuItem_Click(object sender, EventArgs e)
        {
            this.addSprvsrButton_Click(this.addSprvsrButton, e);
        }

        private void delSpvsrMenuItem_Click(object sender, EventArgs e)
        {
            this.delSprvsrButton_Click(this.delSprvsrButton, e);
        }

        private void editSpvsrMenuItem_Click(object sender, EventArgs e)
        {
            this.editSprvsrButton_Click(this.editSprvsrButton, e);
        }

        private void rfrshSpvsrMenuItem_Click(object sender, EventArgs e)
        {
            this.refreshSprvsrButton_Click(this.refreshSprvsrButton, e);
        }

        private void rcHstrySpvsrMenuItem_Click(object sender, EventArgs e)
        {
            if (this.sprvisrDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(Global.get_Spvsr_Rec_Hstry(
              long.Parse(this.sprvisrDataGridView.SelectedRows[0].Cells[5].Value.ToString())), 6);
        }

        private void vwSQLSpvsrMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(Global.mnFrm.spvsr_SQL, 5);
        }

        private void spvsrExptExclMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtPsnSpvsrAsgmtsTmp();
        }

        private void spvsrImptExclMenuItem_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[10]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            this.openFileDialog1.RestoreDirectory = true;
            this.openFileDialog1.Filter = "All Files|*.*|Excel Files|*.xls;*.xlsx";
            this.openFileDialog1.FilterIndex = 2;
            this.openFileDialog1.Title = "Select an Excel File to Upload...";
            this.openFileDialog1.FileName = "";
            if (this.openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Global.mnFrm.cmCde.imprtPsnSpvsrAsgmtsTmp(this.openFileDialog1.FileName);
            }
            this.loadPersBscAssgmnts();
        }
        #endregion

        #region "ASSIGNED JOBS..."
        private void populateJob(long prsnID)
        {
            this.obeyEvnts = false;
            DataSet dtst = Global.getAllJobs(prsnID);
            this.jobsDataGridView.Rows.Clear();
            this.jobsDataGridView.RowCount = dtst.Tables[0].Rows.Count;
            this.jobsDataGridView.ReadOnly = true;
            this.jobsDataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.jobsDataGridView.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.saveJobButton.Enabled = false;
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.jobsDataGridView.Rows[i].HeaderCell.Value = (i + 1).ToString();
                Object[] cellDesc = new Object[7];
                cellDesc[0] = Global.mnFrm.cmCde.getJobName(int.Parse(dtst.Tables[0].Rows[i][0].ToString()));
                cellDesc[1] = "...";
                cellDesc[2] = dtst.Tables[0].Rows[i][1].ToString();
                string fendDte = dtst.Tables[0].Rows[i][2].ToString();
                if (fendDte == "31-Dec-4000")
                {
                    fendDte = "";
                }
                cellDesc[3] = fendDte;// dtst.Tables[0].Rows[i][2].ToString();
                cellDesc[4] = dtst.Tables[0].Rows[i][0].ToString();
                cellDesc[5] = dtst.Tables[0].Rows[i][3].ToString();
                cellDesc[6] = prsnID;
                this.jobsDataGridView.Rows[i].SetValues(cellDesc);
            }
            this.obeyEvnts = true;
        }

        private void addJobButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[10]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            if (this.prsNamesListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Person First!", 0);
                return;
            }
            this.saveJobButton_Click(this.saveJobButton, e);
            string dateStr = DateTime.ParseExact(
      Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
            Global.createJob(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text),
             -1, dateStr.Substring(0, 11), "31-Dec-4000");
            this.populateJob(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
            this.jobsDataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.jobsDataGridView.ReadOnly = false;
            this.jobsDataGridView.Columns[0].ReadOnly = true;
            this.saveJobButton.Enabled = true;
        }

        private void editJobButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[11]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            this.jobsDataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.jobsDataGridView.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.jobsDataGridView.ReadOnly = false;
            this.jobsDataGridView.Columns[0].ReadOnly = true;
            this.saveJobButton.Enabled = true;
        }

        private void delJobButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[12]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            if (this.jobsDataGridView.CurrentCell != null && this.jobsDataGridView.SelectedRows.Count <= 0)
            {
                this.jobsDataGridView.Rows[this.jobsDataGridView.CurrentCell.RowIndex].Selected = true;
            }
            if (this.jobsDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the Row(s) to delete!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to delete the" +
             "\r\nselected Assigned Job(s)?", 1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            for (int i = 0; i < this.jobsDataGridView.SelectedRows.Count; i++)
            {
                Global.deleteJob(
                  long.Parse(this.jobsDataGridView.SelectedRows[i].Cells[5].Value.ToString()),
                  this.locIDTextBox.Text);
            }
            this.populateJob(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
        }

        private void saveJobButton_Click(object sender, EventArgs e)
        {
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            for (int i = 0; i < this.jobsDataGridView.Rows.Count; i++)
            {
                Global.updateJob(long.Parse(this.jobsDataGridView.Rows[i].Cells[6].Value.ToString()),
                 long.Parse(this.jobsDataGridView.Rows[i].Cells[5].Value.ToString()),
                int.Parse(this.jobsDataGridView.Rows[i].Cells[4].Value.ToString()),
                this.jobsDataGridView.Rows[i].Cells[2].Value.ToString(),
                 this.jobsDataGridView.Rows[i].Cells[3].Value.ToString());
            }
            this.jobsDataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.jobsDataGridView.ReadOnly = true;
            this.saveJobButton.Enabled = false;
        }

        private void refreshJobButton_Click(object sender, EventArgs e)
        {
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            if (this.prsNamesListView.SelectedItems.Count == 1)
            {
                this.populateJob(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
            }
        }

        private void jobsDataGridView_CellBeginEdit(object sender, System.Windows.Forms.DataGridViewCellCancelEventArgs e)
        {
            if (e == null /*|| this.obeyEvnts == false*/ || this.saveJobButton.Enabled == false)
            {
                return;
            }
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == 2)
            {
                this.bscDte1TextBox.Text = this.jobsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                Global.mnFrm.cmCde.selectDate(ref this.bscDte1TextBox);
                if (this.bscDte1TextBox.Text.Length > 11)
                {
                    this.jobsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = this.bscDte1TextBox.Text.Substring(0, 11);
                }
                this.jobsDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
            }
            else if (e.ColumnIndex == 3)
            {
                this.bscDte1TextBox.Text = this.jobsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                Global.mnFrm.cmCde.selectDate(ref this.bscDte1TextBox);
                if (this.bscDte1TextBox.Text.Length > 11)
                {
                    this.jobsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = this.bscDte1TextBox.Text.Substring(0, 11);
                }
                this.jobsDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
            }

        }

        private void exptJobMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.jobsDataGridView);
        }

        private void addJobMenuItem_Click(object sender, EventArgs e)
        {
            this.addJobButton_Click(this.addJobButton, e);
        }

        private void editJobMenuItem_Click(object sender, EventArgs e)
        {
            this.editJobButton_Click(this.editJobButton, e);
        }

        private void delJobMenuItem_Click(object sender, EventArgs e)
        {
            this.delJobButton_Click(this.delJobButton, e);
        }

        private void rfrshJobMenuItem_Click(object sender, EventArgs e)
        {
            this.refreshJobButton_Click(this.refreshJobButton, e);
        }

        private void rcHstryJobMenuItem_Click(object sender, EventArgs e)
        {
            if (this.jobsDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(Global.get_Job_Rec_Hstry(
              long.Parse(this.jobsDataGridView.SelectedRows[0].Cells[5].Value.ToString())), 6);
        }

        private void vwSQLJobMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(Global.mnFrm.job_SQL, 5);
        }

        private void jobExptExclMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtPsnJobAsgmtsTmp();
        }

        private void jobImptExclMenuItem_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[10]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            this.openFileDialog1.RestoreDirectory = true;
            this.openFileDialog1.Filter = "All Files|*.*|Excel Files|*.xls;*.xlsx";
            this.openFileDialog1.FilterIndex = 2;
            this.openFileDialog1.Title = "Select an Excel File to Upload...";
            this.openFileDialog1.FileName = "";
            if (this.openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Global.mnFrm.cmCde.imprtPsnJobAsgmtsTmp(this.openFileDialog1.FileName);
            }
            this.loadPersBscAssgmnts();
        }
        #endregion

        #region "ASSIGNED GRADES..."
        private void populateGrades(long prsnID)
        {
            this.obeyEvnts = false;
            DataSet dtst = Global.getAllGrades(prsnID);
            this.gradesDataGridView.Rows.Clear();
            this.gradesDataGridView.RowCount = dtst.Tables[0].Rows.Count;
            this.gradesDataGridView.ReadOnly = true;
            this.gradesDataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.gradesDataGridView.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.saveGradeButton.Enabled = false;
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.gradesDataGridView.Rows[i].HeaderCell.Value = (i + 1).ToString();
                Object[] cellDesc = new Object[7];
                cellDesc[0] = Global.mnFrm.cmCde.getGrdName(int.Parse(dtst.Tables[0].Rows[i][0].ToString()));
                cellDesc[1] = "...";
                cellDesc[2] = dtst.Tables[0].Rows[i][1].ToString();
                string fendDte = dtst.Tables[0].Rows[i][2].ToString();
                if (fendDte == "31-Dec-4000")
                {
                    fendDte = "";
                }
                cellDesc[3] = fendDte;// dtst.Tables[0].Rows[i][2].ToString();
                cellDesc[4] = dtst.Tables[0].Rows[i][0].ToString();
                cellDesc[5] = dtst.Tables[0].Rows[i][3].ToString();
                cellDesc[6] = prsnID;
                this.gradesDataGridView.Rows[i].SetValues(cellDesc);
            }
            this.obeyEvnts = true;
        }

        private void addGradeButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[10]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            if (this.prsNamesListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Person First!", 0);
                return;
            }
            this.saveGradeButton_Click(this.saveGradeButton, e);
            string dateStr = DateTime.ParseExact(
      Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
            Global.createGrade(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text),
             -1, dateStr.Substring(0, 11), "31-Dec-4000");
            this.populateGrades(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
            this.gradesDataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.gradesDataGridView.ReadOnly = false;
            this.gradesDataGridView.Columns[0].ReadOnly = true;
            this.saveGradeButton.Enabled = true;
        }

        private void editGradeButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[11]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            this.gradesDataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.gradesDataGridView.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.gradesDataGridView.ReadOnly = false;
            this.gradesDataGridView.Columns[0].ReadOnly = true;
            this.saveGradeButton.Enabled = true;
        }

        private void delGradeButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[12]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            if (this.gradesDataGridView.CurrentCell != null && this.gradesDataGridView.SelectedRows.Count <= 0)
            {
                this.gradesDataGridView.Rows[this.gradesDataGridView.CurrentCell.RowIndex].Selected = true;
            }
            if (this.gradesDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the Row(s) to delete!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to delete the" +
             "\r\nselected Assigned Grade(s)?", 1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            for (int i = 0; i < this.gradesDataGridView.SelectedRows.Count; i++)
            {
                Global.deleteGrade(
                  long.Parse(this.gradesDataGridView.SelectedRows[i].Cells[5].Value.ToString()),
                  this.locIDTextBox.Text);
            }
            this.populateGrades(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
        }

        private void saveGradeButton_Click(object sender, EventArgs e)
        {
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            for (int i = 0; i < this.gradesDataGridView.Rows.Count; i++)
            {
                Global.updateGrade(long.Parse(this.gradesDataGridView.Rows[i].Cells[6].Value.ToString()),
                 long.Parse(this.gradesDataGridView.Rows[i].Cells[5].Value.ToString()),
                int.Parse(this.gradesDataGridView.Rows[i].Cells[4].Value.ToString()),
                this.gradesDataGridView.Rows[i].Cells[2].Value.ToString(),
                 this.gradesDataGridView.Rows[i].Cells[3].Value.ToString());
            }
            this.gradesDataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.gradesDataGridView.ReadOnly = true;
            this.saveGradeButton.Enabled = false;
        }

        private void refreshGradeButton_Click(object sender, EventArgs e)
        {
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            if (this.prsNamesListView.SelectedItems.Count == 1)
            {
                this.populateGrades(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
            }
        }

        private void gradesDataGridView_CellBeginEdit(object sender, System.Windows.Forms.DataGridViewCellCancelEventArgs e)
        {
            if (e == null /*|| this.obeyEvnts == false*/ || this.saveGradeButton.Enabled == false)
            {
                return;
            }
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == 2)
            {
                this.bscDte1TextBox.Text = this.gradesDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                Global.mnFrm.cmCde.selectDate(ref this.bscDte1TextBox);
                if (this.bscDte1TextBox.Text.Length > 11)
                {
                    this.gradesDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = this.bscDte1TextBox.Text.Substring(0, 11);
                }
                this.gradesDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
            }
            else if (e.ColumnIndex == 3)
            {
                this.bscDte1TextBox.Text = this.gradesDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                Global.mnFrm.cmCde.selectDate(ref this.bscDte1TextBox);
                if (this.bscDte1TextBox.Text.Length > 11)
                {
                    this.gradesDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = this.bscDte1TextBox.Text.Substring(0, 11);
                }
                this.gradesDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
            }

        }

        private void exptGradeMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.gradesDataGridView);
        }

        private void addGradeMenuItem_Click(object sender, EventArgs e)
        {
            this.addGradeButton_Click(this.addGradeButton, e);
        }

        private void editGradeMenuItem_Click(object sender, EventArgs e)
        {
            this.editGradeButton_Click(this.editGradeButton, e);
        }

        private void delGradeMenuItem_Click(object sender, EventArgs e)
        {
            this.delGradeButton_Click(this.delGradeButton, e);
        }

        private void rfrshGradeMenuItem_Click(object sender, EventArgs e)
        {
            this.refreshGradeButton_Click(this.refreshGradeButton, e);
        }

        private void rcHstryGradeMenuItem_Click(object sender, EventArgs e)
        {
            if (this.gradesDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(Global.get_Grd_Rec_Hstry(
              long.Parse(this.gradesDataGridView.SelectedRows[0].Cells[5].Value.ToString())), 6);
        }

        private void vwSQLGradeMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(Global.mnFrm.grd_SQL, 5);
        }

        private void gradeExptExclMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtPsnGrdAsgmtsTmp();

        }

        private void gradeImptExclMenuItem_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[10]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            this.openFileDialog1.RestoreDirectory = true;
            this.openFileDialog1.Filter = "All Files|*.*|Excel Files|*.xls;*.xlsx";
            this.openFileDialog1.FilterIndex = 2;
            this.openFileDialog1.Title = "Select an Excel File to Upload...";
            this.openFileDialog1.FileName = "";
            if (this.openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Global.mnFrm.cmCde.imprtPsnGrdAsgmtsTmp(this.openFileDialog1.FileName);
            }
            this.loadPersBscAssgmnts();
        }
        #endregion

        #region "ASSIGNED POSITIONS..."
        private void populatePositions(long prsnID)
        {
            this.obeyEvnts = false;
            DataSet dtst = Global.getAllPositions(prsnID);
            this.positionDataGridView.Rows.Clear();
            this.positionDataGridView.RowCount = dtst.Tables[0].Rows.Count;
            this.positionDataGridView.ReadOnly = true;
            this.positionDataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.positionDataGridView.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.savePostnButton.Enabled = false;
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.positionDataGridView.Rows[i].HeaderCell.Value = (i + 1).ToString();
                Object[] cellDesc = new Object[10];
                cellDesc[0] = Global.mnFrm.cmCde.getPosName(int.Parse(dtst.Tables[0].Rows[i][0].ToString()));
                cellDesc[1] = "...";
                cellDesc[2] = dtst.Tables[0].Rows[i][1].ToString();
                string fendDte = dtst.Tables[0].Rows[i][2].ToString();
                if (fendDte == "31-Dec-4000")
                {
                    fendDte = "";
                }
                cellDesc[3] = fendDte;// dtst.Tables[0].Rows[i][2].ToString();
                cellDesc[4] = dtst.Tables[0].Rows[i][0].ToString();
                cellDesc[5] = dtst.Tables[0].Rows[i][3].ToString();
                cellDesc[6] = prsnID;
                cellDesc[7] = Global.mnFrm.cmCde.getDivName(int.Parse(dtst.Tables[0].Rows[i][4].ToString()));
                cellDesc[8] = dtst.Tables[0].Rows[i][4].ToString();
                cellDesc[9] = "...";
                this.positionDataGridView.Rows[i].SetValues(cellDesc);
            }
            this.obeyEvnts = true;
        }

        private void addPostnButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[10]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            if (this.prsNamesListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Person First!", 0);
                return;
            }
            this.savePostnButton_Click(this.savePostnButton, e);
            string dateStr = DateTime.ParseExact(
      Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
      System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
            Global.createPosition(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text),
             -1, dateStr.Substring(0, 11), "31-Dec-4000");
            this.populatePositions(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
            this.positionDataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.positionDataGridView.ReadOnly = false;
            this.positionDataGridView.Columns[6].ReadOnly = true;
            this.positionDataGridView.Columns[0].ReadOnly = true;
            this.savePostnButton.Enabled = true;
        }

        private void editPostnButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[11]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            this.positionDataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.positionDataGridView.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.positionDataGridView.ReadOnly = false;
            this.positionDataGridView.Columns[6].ReadOnly = true;
            this.positionDataGridView.Columns[0].ReadOnly = true;
            this.savePostnButton.Enabled = true;
        }

        private void delPostnButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[12]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            if (this.positionDataGridView.CurrentCell != null && this.positionDataGridView.SelectedRows.Count <= 0)
            {
                this.positionDataGridView.Rows[this.positionDataGridView.CurrentCell.RowIndex].Selected = true;
            }
            if (this.positionDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the Row(s) to delete!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to delete the" +
             "\r\nselected Assigned Position(s)?", 1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            for (int i = 0; i < this.positionDataGridView.SelectedRows.Count; i++)
            {
                Global.deletePosition(
                  long.Parse(this.positionDataGridView.SelectedRows[i].Cells[5].Value.ToString()),
                  this.locIDTextBox.Text);
            }
            this.populatePositions(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
        }

        private void savePostnButton_Click(object sender, EventArgs e)
        {
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            for (int i = 0; i < this.positionDataGridView.Rows.Count; i++)
            {
                Global.updatePosition(long.Parse(this.positionDataGridView.Rows[i].Cells[6].Value.ToString()),
                 long.Parse(this.positionDataGridView.Rows[i].Cells[5].Value.ToString()),
                int.Parse(this.positionDataGridView.Rows[i].Cells[4].Value.ToString()),
                this.positionDataGridView.Rows[i].Cells[2].Value.ToString(),
                 this.positionDataGridView.Rows[i].Cells[3].Value.ToString(),
                int.Parse(this.positionDataGridView.Rows[i].Cells[8].Value.ToString()));
            }
            this.positionDataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.positionDataGridView.ReadOnly = true;
            this.savePostnButton.Enabled = false;
        }

        private void refreshPostnButton_Click(object sender, EventArgs e)
        {
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            if (this.prsNamesListView.SelectedItems.Count == 1)
            {
                this.populatePositions(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
            }
        }

        private void positionDataGridView_CellBeginEdit(object sender,
          System.Windows.Forms.DataGridViewCellCancelEventArgs e)
        {
            if (e == null /*|| this.obeyEvnts == false*/ || this.savePostnButton.Enabled == false)
            {
                return;
            }
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == 2)
            {
                this.bscDte1TextBox.Text = this.positionDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                Global.mnFrm.cmCde.selectDate(ref this.bscDte1TextBox);
                if (this.bscDte1TextBox.Text.Length > 11)
                {
                    this.positionDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = this.bscDte1TextBox.Text.Substring(0, 11);
                }
                this.positionDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
            }
            else if (e.ColumnIndex == 3)
            {
                this.bscDte1TextBox.Text = this.positionDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                Global.mnFrm.cmCde.selectDate(ref this.bscDte1TextBox);
                if (this.bscDte1TextBox.Text.Length > 11)
                {
                    this.positionDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = this.bscDte1TextBox.Text.Substring(0, 11);
                }
                this.positionDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
            }
        }

        private void exptPosMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.positionDataGridView);
        }

        private void addPosMenuItem_Click(object sender, EventArgs e)
        {
            this.addPostnButton_Click(this.addPostnButton, e);
        }

        private void editPosMenuItem_Click(object sender, EventArgs e)
        {
            this.editPostnButton_Click(this.editPostnButton, e);
        }

        private void delPosMenuItem_Click(object sender, EventArgs e)
        {
            this.delPostnButton_Click(this.delPostnButton, e);
        }

        private void rfrshPosMenuItem_Click(object sender, EventArgs e)
        {
            this.refreshPostnButton_Click(this.refreshPostnButton, e);
        }

        private void rcHstryPosMenuItem_Click(object sender, EventArgs e)
        {
            if (this.positionDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(Global.get_Pos_Rec_Hstry(
              long.Parse(this.positionDataGridView.SelectedRows[0].Cells[5].Value.ToString())), 6);
        }

        private void vwSQLPosMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(Global.mnFrm.pos_SQL, 5);
        }

        private void posExptExclMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtPsnPosAsgmtsTmp();
        }

        private void posImptExclMenuItem_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[10]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            this.openFileDialog1.RestoreDirectory = true;
            this.openFileDialog1.Filter = "All Files|*.*|Excel Files|*.xls;*.xlsx";
            this.openFileDialog1.FilterIndex = 2;
            this.openFileDialog1.Title = "Select an Excel File to Upload...";
            this.openFileDialog1.FileName = "";
            if (this.openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Global.mnFrm.cmCde.imprtPsnPosAsgmtsTmp(this.openFileDialog1.FileName);
            }
            this.loadPersBscAssgmnts();
        }
        #endregion

        #region "EXTRA INFO LABELS..."
        private void loadValPanel()
        {
            this.obeyEvnts = false;
            if (this.searchInComboBox.SelectedIndex < 0)
            {
                this.searchInComboBox.SelectedIndex = 0;
            }
            int dsply = 0;
            if (this.dsplySizeComboBox.Text == ""
             || int.TryParse(this.dsplySizeComboBox.Text, out dsply) == false)
            {
                this.dsplySizeComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            if (this.searchForTextBox.Text == "")
            {
                this.searchForTextBox.Text = "%";
            }
            if (this.addPrsn == false && this.editPrsn == false)
            {
                this.extInfoDataGridView.ReadOnly = true;
                this.extInfoDataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.Gainsboro;
            }
            this.is_last_val = false;
            this.totl_vals = Global.mnFrm.cmCde.Big_Val;
            this.getValPnlData();
            this.obeyEvnts = true;
        }

        private void getValPnlData()
        {
            //if (Global.mnFrm.cmCde.pgSqlConn.State == ConnectionState.Closed)
            //{
            //  Global.mnFrm.cmCde.pgSqlConn.Open();
            //}
            this.updtValTotals();
            this.table_id = Global.mnFrm.cmCde.getMdlGrpID("Person Data");
            this.ext_inf_tbl_name = "prs.prsn_all_other_info_table";
            this.ext_inf_seq_name = "prs.prsn_all_other_info_table_dflt_row_id_seq";
            if (this.prsNamesListView.SelectedItems.Count == 1)
            {
                this.row_pk_id = long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text);
            }
            //else
            //{
            //  this.row_pk_id = -10000000010;
            //}
            this.populateValGridVw();
            this.updtValNavLabels();
        }

        private void updtValTotals()
        {
            this.myNav.FindNavigationIndices(int.Parse(this.dsplySizeComboBox.Text),
            this.totl_vals);

            if (this.cur_vals_idx >= this.myNav.totalGroups)
            {
                this.cur_vals_idx = this.myNav.totalGroups - 1;
            }
            if (this.cur_vals_idx < 0)
            {
                this.cur_vals_idx = 0;
            }
            this.myNav.currentNavigationIndex = this.cur_vals_idx;
        }

        private void updtValNavLabels()
        {
            this.moveFirstButton.Enabled = this.myNav.moveFirstBtnStatus();
            this.movePreviousButton.Enabled = this.myNav.movePrevBtnStatus();
            this.moveNextButton.Enabled = this.myNav.moveNextBtnStatus();
            this.moveLastButton.Enabled = this.myNav.moveLastBtnStatus();
            this.positionTextBox.Text = this.myNav.displayedRecordsNumbers();
            if (this.is_last_val == true ||
             this.totl_vals != Global.mnFrm.cmCde.Big_Val)
            {
                this.totalRecLabel.Text = this.myNav.totalRecordsLabel();
            }
            else
            {
                this.totalRecLabel.Text = "of Total";
            }
        }

        private void populateValGridVw()
        {
            this.obeyEvnts = false;
            DataSet dtst = Global.mnFrm.cmCde.getAllwdExtInfosNVals(this.searchForTextBox.Text,
             this.searchInComboBox.Text, this.cur_vals_idx,
             int.Parse(this.dsplySizeComboBox.Text), ref this.vwSQLStmnt,
             this.table_id, this.row_pk_id, this.ext_inf_tbl_name);
            this.extInfoDataGridView.Rows.Clear();
            this.extInfoDataGridView.RowCount = dtst.Tables[0].Rows.Count;
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.last_vals_num = this.myNav.startIndex() + i;
                this.extInfoDataGridView.Rows[i].HeaderCell.Value = (this.myNav.startIndex() + i).ToString();
                Object[] cellDesc = new Object[6];
                cellDesc[0] = dtst.Tables[0].Rows[i][0].ToString();
                cellDesc[1] = "...";
                cellDesc[2] = dtst.Tables[0].Rows[i][1].ToString();
                cellDesc[3] = dtst.Tables[0].Rows[i][2].ToString();
                cellDesc[4] = dtst.Tables[0].Rows[i][3].ToString();
                cellDesc[5] = dtst.Tables[0].Rows[i][5].ToString();
                this.extInfoDataGridView.Rows[i].SetValues(cellDesc);
            }
            this.correctValsNavLbls(dtst);
            this.obeyEvnts = true;
            if (this.extInfoDataGridView.Rows.Count > 0)
            {
                this.extInfoDataGridView.Rows[0].Selected = true;
            }
            this.obeyEvnts = true;
        }

        private void correctValsNavLbls(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.cur_vals_idx == 0 && totlRecs == 0)
            {
                this.is_last_val = true;
                this.totl_vals = 0;
                this.last_vals_num = 0;
                this.cur_vals_idx = 0;
                this.updtValTotals();
                this.updtValNavLabels();
            }
            else if (this.totl_vals == Global.mnFrm.cmCde.Big_Val
          && totlRecs < long.Parse(this.dsplySizeComboBox.Text))
            {
                this.totl_vals = this.last_vals_num;
                if (totlRecs == 0)
                {
                    this.cur_vals_idx -= 1;
                    this.updtValTotals();
                    this.populateValGridVw();
                }
                else
                {
                    this.updtValTotals();
                }
            }
        }

        private void valPnlNavButtons(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripButton sentObj =
             (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecLabel.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.cur_vals_idx = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.cur_vals_idx -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.cur_vals_idx += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.totl_vals = Global.mnFrm.cmCde.getTotalAllwdExtInf(this.searchForTextBox.Text,
                 this.searchInComboBox.Text, this.table_id, this.row_pk_id, this.ext_inf_tbl_name);
                this.is_last_val = true;
                this.updtValTotals();
                this.cur_vals_idx = this.myNav.totalGroups - 1;
            }
            this.getValPnlData();
        }

        private void gotoButton_Click(object sender, EventArgs e)
        {
            this.loadValPanel();
        }

        private void vwSQLButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(this.vwSQLStmnt, 5);
        }

        private void extInfoDataGridView_CellValueChanged(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (e.Equals(null) || this.obeyEvnts == false
              || this.canEdit == false)
            {
                return;
            }
            if (e.RowIndex < 0)
            {
                return;
            }
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[8]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.extInfoDataGridView.Rows[e.RowIndex].Cells[4].Value == null)
            {
                this.extInfoDataGridView.Rows[e.RowIndex].Cells[4].Value = "-1";
            }

            if (this.extInfoDataGridView.Rows[e.RowIndex].Cells[5].Value == null)
            {
                this.extInfoDataGridView.Rows[e.RowIndex].Cells[5].Value = "-1";
            }

            if (this.extInfoDataGridView.Rows[e.RowIndex].Cells[0].Value == null)
            {
                this.extInfoDataGridView.Rows[e.RowIndex].Cells[0].Value = string.Empty;
            }

            if (this.extInfoDataGridView.Rows[e.RowIndex].Cells[2].Value == null)
            {
                this.extInfoDataGridView.Rows[e.RowIndex].Cells[2].Value = string.Empty;
            }
            if (this.extInfoDataGridView.Rows[e.RowIndex].Cells[3].Value == null)
            {
                this.extInfoDataGridView.Rows[e.RowIndex].Cells[3].Value = string.Empty;
            }
            if (e.ColumnIndex == 2 || e.ColumnIndex == 3)
            {
                if (long.Parse(
                 this.extInfoDataGridView.Rows[e.RowIndex].Cells[5].Value.ToString()) > 0)
                {
                    Global.mnFrm.cmCde.updateRowOthrInfVal(this.ext_inf_tbl_name, long.Parse(
               this.extInfoDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString()),
               this.row_pk_id,
               this.extInfoDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString(),
               this.extInfoDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString()
             , this.extInfoDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString()
             , long.Parse(this.extInfoDataGridView.Rows[e.RowIndex].Cells[5].Value.ToString()));
                }
                else
                {
                    if (Global.mnFrm.cmCde.doesRowHvOthrInfo(this.ext_inf_tbl_name, long.Parse(
                     this.extInfoDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString()), this.row_pk_id) > 0)
                    {
                        Global.mnFrm.cmCde.updateRowOthrInfVal(this.ext_inf_tbl_name, long.Parse(
                             this.extInfoDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString()),
                             this.row_pk_id,
                             this.extInfoDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString(),
                             this.extInfoDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString()
                           , this.extInfoDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString()
                           , long.Parse(this.extInfoDataGridView.Rows[e.RowIndex].Cells[5].Value.ToString()));
                    }
                    else
                    {
                        long rwID = Global.mnFrm.cmCde.getNewExtInfoID(this.ext_inf_seq_name);
                        //Global.mnFrm.cmCde.showMsg(rwID.ToString(),0);
                        Global.mnFrm.cmCde.createRowOthrInfVal(this.ext_inf_tbl_name, long.Parse(
                        this.extInfoDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString()),
                        this.row_pk_id,
                        this.extInfoDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString(),
                        this.extInfoDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString()
                           , this.extInfoDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString(), rwID);
                        this.obeyEvnts = false;
                        this.extInfoDataGridView.EndEdit();
                        this.extInfoDataGridView.Rows[e.RowIndex].Cells[5].Value = rwID;
                        this.obeyEvnts = true;
                    }
                }
            }
        }

        private void searchForTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.loadValPanel();
            }
        }

        private void positionTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                this.valPnlNavButtons(this.movePreviousButton, ex);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
            {
                this.valPnlNavButtons(this.moveNextButton, ex);
            }
        }

        private void rfrshOthInfMenuItem_Click(object sender, EventArgs e)
        {
            this.gotoButton_Click(this.gotoButton, e);
        }

        private void exprtOthInfMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.extInfoDataGridView);
        }

        private void rcHstryOthInfMenuItem_Click(object sender, EventArgs e)
        {
            if (this.extInfoDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(Global.get_OthInf_Rec_Hstry(
              long.Parse(this.extInfoDataGridView.SelectedRows[0].Cells[3].Value.ToString()),
              this.ext_inf_tbl_name), 6);
        }

        private void vwSQLOthInfMenuItem_Click(object sender, EventArgs e)
        {
            this.vwSQLButton_Click(this.vwSQLButton, e);
        }
        #endregion

        private void imprtExtInfTmpMenuItem_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[13]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            this.openFileDialog1.RestoreDirectory = true;
            this.openFileDialog1.Filter = "All Files|*.*|Excel Files|*.xls;*.xlsx";
            this.openFileDialog1.FilterIndex = 2;
            this.openFileDialog1.Title = "Select an Excel File to Upload...";
            this.openFileDialog1.FileName = "";
            if (this.openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Global.mnFrm.cmCde.imprtPsnExtInfoTmp(this.openFileDialog1.FileName);
            }
            this.loadOthrInfPanel();
        }

        private void exptExtInfTmpMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtPsnExtInfoTmp();
        }

        private void iDPrfxComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.locIDTextBox.Text.Contains(this.iDPrfxComboBox.Text)
              || this.iDPrfxComboBox.Text == "")
            {
                //string tst = Global.getLtstRecPkID("prs.prsn_names_nos",
                //"person_id").ToString();
                string tst = "0001";
                if (Global.mnFrm.cmCde.getEnbldPssblValID("Yes",
                  Global.mnFrm.cmCde.getEnbldLovID("Person ID No. Prefix Determines ID Serial No.")) > 0)
                {
                    if (this.iDPrfxComboBox.Text == "")
                    {
                        tst = Global.getLastPrsnIDNo();
                    }
                    else
                    {
                        tst = Global.getLtstPrsnIDNoInPrfx(this.iDPrfxComboBox.Text);
                    }
                }
                else
                {
                    if (this.iDPrfxComboBox.Text == "")
                    {
                        tst = Global.getLastPrsnIDNo();
                    }
                    else
                    {
                        tst = Global.getLtstPrsnIDNo();
                    }
                }
                if (tst.Length < 4)
                {
                    tst = tst.PadLeft(4, '0');
                }
                this.locIDTextBox.Text = this.iDPrfxComboBox.Text + tst;
            }
        }

        private void addNtnltyButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[7]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select a saved Person First!", 0);
                return;
            }
            ntnltyDiag nwDiag = new ntnltyDiag();
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
                Global.createNatnlty(long.Parse(this.prsnIDTextBox.Text), nwDiag.ntnltyTextBox.Text,
                 nwDiag.idTypeTextBox.Text, nwDiag.idNumTextBox.Text, nwDiag.dateIssuedTextBox.Text,
                 nwDiag.expryDateTextBox.Text, nwDiag.otherInfoTextBox.Text);
                this.populateNatnlty(long.Parse(this.prsnIDTextBox.Text));
            }
        }

        private void editNtnltyButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[8]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.nationalityListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("No Record to Edit!", 0);
                return;
            }
            ntnltyDiag nwDiag = new ntnltyDiag();
            nwDiag.ntnltyTextBox.Text = this.nationalityListView.SelectedItems[0].SubItems[1].Text;
            nwDiag.idTypeTextBox.Text = this.nationalityListView.SelectedItems[0].SubItems[2].Text;
            nwDiag.idNumTextBox.Text = this.nationalityListView.SelectedItems[0].SubItems[3].Text;
            nwDiag.dateIssuedTextBox.Text = this.nationalityListView.SelectedItems[0].SubItems[5].Text;
            nwDiag.expryDateTextBox.Text = this.nationalityListView.SelectedItems[0].SubItems[6].Text;
            nwDiag.otherInfoTextBox.Text = this.nationalityListView.SelectedItems[0].SubItems[7].Text;
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
                Global.updateNatnlty(long.Parse(this.nationalityListView.SelectedItems[0].SubItems[4].Text), long.Parse(this.prsnIDTextBox.Text), nwDiag.ntnltyTextBox.Text,
                 nwDiag.idTypeTextBox.Text, nwDiag.idNumTextBox.Text, nwDiag.dateIssuedTextBox.Text,
                 nwDiag.expryDateTextBox.Text, nwDiag.otherInfoTextBox.Text);
                this.populateNatnlty(long.Parse(this.prsnIDTextBox.Text));
                this.populateNatnlty(long.Parse(this.prsnIDTextBox.Text));
            }
        }

        private void deleteNtnltyButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[8]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.nationalityListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the record to DELETE!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Record?" +
      "\r\nThis action cannot be undone!", 1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            Global.deleteNtnlty(long.Parse(this.nationalityListView.SelectedItems[0].SubItems[4].Text),
              this.nationalityListView.SelectedItems[0].SubItems[2].Text, this.locIDTextBox.Text);
            this.populateNatnlty(long.Parse(this.prsnIDTextBox.Text));
        }

        private void vwAttchmntsButton_Click(object sender, EventArgs e)
        {
            if (this.prsnIDTextBox.Text == "" ||
          this.prsnIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select a saved Person Record First!", 0);
                return;
            }

            attchmntsDiag nwDiag = new attchmntsDiag();
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[8]) == false)
            {
                nwDiag.addButton.Enabled = false;
                nwDiag.addButton.Visible = false;
                nwDiag.editButton.Enabled = false;
                nwDiag.editButton.Visible = false;
                nwDiag.delButton.Enabled = false;
                nwDiag.delButton.Visible = false;
            }
            //Global.mnFrm.cmCde.showMsg("Cannot add Transactions to already Posted Batch of Transactions!", 0);
            //return;
            nwDiag.batchid = long.Parse(this.prsnIDTextBox.Text);
            DialogResult dgres = nwDiag.ShowDialog();
            if (dgres == DialogResult.OK)
            {
            }
        }

        private void ntnltyButton_Click(object sender, EventArgs e)
        {
            if (this.editPrsButton.Text == "EDIT")
            {
                this.editPrsButton.PerformClick();
            }
            if (this.editPrsn == false && this.addPrsn == false)
            {
                Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
                return;
            }
            //Nationality
            int[] selVals = new int[1];
            selVals[0] = Global.mnFrm.cmCde.getPssblValID(this.ntnltyTextBox.Text,
             Global.mnFrm.cmCde.getLovID("Nationalities"));
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
             Global.mnFrm.cmCde.getLovID("Nationalities"), ref selVals, true, true,
             this.srchWrd, "Both", true);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.ntnltyTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
                }
            }
        }

        private void fieldsStpButton_Click(object sender, EventArgs e)
        {
            extrDataStpDiag nwdiag = new extrDataStpDiag();
            DialogResult dgres = nwdiag.ShowDialog();
            if (dgres == DialogResult.OK)
            {
            }
            this.createPrsExtrDataDsbl();
            this.goPrsButton_Click(this.goPrsButton, e);
        }

        private void pdfRptButton_Click(object sender, EventArgs e)
        {
            //Global.mnFrm.cmCde.exprtToWordPrsn(long.Parse(this.prsnIDTextBox.Text));
            //return;
            try
            {
                if (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1")
                {
                    Global.mnFrm.cmCde.showMsg("Please select a Person First!", 0);
                    return;
                }
                this.pdfRptButton.Enabled = false;
                System.Windows.Forms.Application.DoEvents();
                // Create a new PDF document
                Graphics g = Graphics.FromHwnd(this.Handle);
                XPen aPen = new XPen(XColor.FromArgb(System.Drawing.Color.Black), 1);
                PdfDocument document = new PdfDocument();
                document.Info.Title = "BASIC PERSON DATA";
                //document.PageLayout = PdfPageLayout.OneColumn;
                //document.ViewerPreferences.FitWindow = true;
                //document.ViewerPreferences.CenterWindow = true;
                document.ViewerPreferences.DisplayDocTitle = true;
                document.ViewerPreferences.HideMenubar = true;
                document.ViewerPreferences.HideToolbar = true;
                document.ViewerPreferences.HideWindowUI = true;
                //document.PageMode = PdfPageMode.UseNone;
                // Create first page for basic person details
                PdfPage page0 = document.AddPage();
                page0.Orientation = PageOrientation.Portrait;
                page0.Height = XUnit.FromInch(11);
                page0.Width = XUnit.FromInch(8.5);
                XGraphics gfx0 = XGraphics.FromPdfPage(page0);
                XFont xfont0 = new XFont("Verdana", 20, XFontStyle.BoldItalic);
                //gfx0.DrawString("Hello, World!" + this.locIDTextBox.Text, xfont0, XBrushes.Black,
                //new XRect(0, 0, page0.Width, page0.Height),
                //  XStringFormats.TopLeft);

                XFont xfont1 = new XFont("Tahoma", 10.25f, XFontStyle.Underline | XFontStyle.Bold);
                XFont xfont11 = new XFont("Tahoma", 10.25f, XFontStyle.Bold);
                XFont xfont2 = new XFont("Tahoma", 10.25f, XFontStyle.Bold);
                XFont xfont4 = new XFont("Tahoma", 10.0f, XFontStyle.Bold);
                XFont xfont41 = new XFont("Tahoma", 10.0f);
                XFont xfont3 = new XFont("Lucida Console", 8.25f);
                XFont xfont31 = new XFont("Lucida Console", 10.5f, XFontStyle.Bold);
                XFont xfont5 = new XFont("Tahoma", 6.0f, XFontStyle.Italic);

                Font font1 = new Font("Tahoma", 10.25f, FontStyle.Underline | FontStyle.Bold);
                Font font11 = new Font("Tahoma", 10.25f, FontStyle.Bold);
                Font font2 = new Font("Tahoma", 10.25f, FontStyle.Bold);
                Font font4 = new Font("Tahoma", 10.0f, FontStyle.Bold);
                Font font41 = new Font("Tahoma", 10.0f);
                Font font3 = new Font("Lucida Console", 8.25f);
                Font font31 = new Font("Lucida Console", 10.5f, FontStyle.Bold);
                Font font5 = new Font("Times New Roman", 6.0f, FontStyle.Italic);

                float font1Hght = (float)gfx0.MeasureString("ABCDEFGHIJKLMNOP", xfont1).Height;
                float font2Hght = (float)gfx0.MeasureString("ABCDEFGHIJKLMNOP", xfont2).Height;
                float font3Hght = (float)gfx0.MeasureString("ABCDEFGHIJKLMNOP", xfont3).Height;
                float font4Hght = (float)gfx0.MeasureString("ABCDEFGHIJKLMNOP", xfont41).Height;
                float font5Hght = (float)gfx0.MeasureString("ABCDEFGHIJKLMNOP", xfont5).Height;

                float pageWidth = 590 - 40;//e.PageSettings.PrintableArea.Width;
                float pageHeight = 760 - 40;// e.PageSettings.PrintableArea.Height;
                float txtwdth = pageWidth - 40;
                //Global.mnFrm.cmCde.showMsg(pageWidth.ToString(), 0);
                float startX = 40;
                float startY = 40;
                float offsetY = 0;
                float ght = 0;
                float gwdth = 0;
                //StringBuilder strPrnt = new StringBuilder();
                //strPrnt.AppendLine("Received From");
                string[] nwLn;
                int pageNo = 1;
                XImage img = (XImage)Global.mnFrm.cmCde.getDBImageFile(Global.mnFrm.cmCde.Org_id.ToString() + ".png", 0);
                float picWdth = 70.00F;
                float picHght = (float)(picWdth / img.PixelWidth) * (float)img.PixelHeight;
                if (pageNo == 1)
                { //Org Logo
                  //RectangleF srcRect = new Rectangle(0, 0, this.BackgroundImage.Width,
                  //BackgroundImage.Height);
                  //RectangleF destRect = new Rectangle(0, 0, nWidth, nHeight);
                  //Rectangle destRect = new Rectangle(0, 0, nWidth, nHeight);


                    gfx0.DrawImage(img, startX - 10, startY + offsetY - 15, picWdth, picHght);
                    //g.DrawImage(this.LargerImage, destRect, srcRect, GraphicsUnit.Pixel);

                    //Org Name
                    nwLn = Global.mnFrm.cmCde.breakRptTxtDown(
                      Global.mnFrm.cmCde.getOrgName(Global.mnFrm.cmCde.Org_id),
                      pageWidth + 85, font2, g);
                    for (int i = 0; i < nwLn.Length; i++)
                    {
                        gfx0.DrawString(nwLn[i]
                        , xfont2, XBrushes.Black, startX + picWdth + 5, startY + offsetY);
                        offsetY += font2Hght;
                    }

                    ght = (float)gfx0.MeasureString(
                      Global.mnFrm.cmCde.getOrgPstlAddrs(Global.mnFrm.cmCde.Org_id).Trim(), xfont2).Height;
                    //offsetY = offsetY + (int)ght;

                    //Pstal Address
                    XTextFormatter tf = new XTextFormatter(gfx0);
                    XRect rect = new XRect(startX + picWdth + 5, startY + offsetY - 7, 500, ght);
                    gfx0.DrawRectangle(XBrushes.White, rect);
                    tf.DrawString(Global.mnFrm.cmCde.getOrgPstlAddrs(Global.mnFrm.cmCde.Org_id).Trim()
                      , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                    //gfx0.DrawString(,
                    //xfont2, XBrushes.Black, startX + picWdth, startY + offsetY);
                    offsetY += ght + 5;

                    //Contacts Nos
                    nwLn = Global.mnFrm.cmCde.breakTxtDown(
              Global.mnFrm.cmCde.getOrgContactNos(Global.mnFrm.cmCde.Org_id),
              pageWidth, font2, g);
                    for (int i = 0; i < nwLn.Length; i++)
                    {
                        gfx0.DrawString(nwLn[i]
                        , xfont2, XBrushes.Black, startX + picWdth + 5, startY + offsetY);
                        offsetY += font2Hght;
                    }
                    //Email Address
                    nwLn = Global.mnFrm.cmCde.breakRptTxtDown(
              Global.mnFrm.cmCde.getOrgEmailAddrs(Global.mnFrm.cmCde.Org_id),
              pageWidth, font2, g);
                    for (int i = 0; i < nwLn.Length; i++)
                    {
                        gfx0.DrawString(nwLn[i]
                        , xfont2, XBrushes.Black, startX + picWdth + 5, startY + offsetY);
                        offsetY += font2Hght;
                    }
                    offsetY += font2Hght;
                    if (offsetY < picHght)
                    {
                        offsetY = picHght;
                    }
                    gfx0.DrawLine(aPen, startX, startY + offsetY - 8, startX + 510,
            startY + offsetY - 8);

                }
                string orgType = Global.mnFrm.cmCde.getPssblValNm(int.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
        "org.org_details", "org_id", "org_typ_id", Global.mnFrm.cmCde.Org_id)));

                DataSet dtst = Global.get_Prs_Names_NosRpt(long.Parse(this.prsnIDTextBox.Text));
                //Title
                float oldoffsetY = offsetY;
                float oldoffsetY1 = offsetY;
                float hgstOffsetY = 0;
                for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
                {
                    ght = (float)gfx0.MeasureString(
                      "Picture: ".ToUpper(), xfont2).Height;
                    //lblght = ght;
                    XTextFormatter tf = new XTextFormatter(gfx0);
                    XRect rect = new XRect(startX, startY + offsetY, 245, ght);
                    gfx0.DrawRectangle(XPens.Black, XBrushes.LightGray, rect);
                    tf.DrawString(" Picture: ".ToUpper()
                      , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                    offsetY += (int)ght + 10;

                    img = (XImage)Global.mnFrm.cmCde.getDBImageFile(dtst.Tables[0].Rows[a][20].ToString(), 2);
                    picWdth = 100.00F;
                    picHght = (float)(picWdth / img.PixelWidth) * (float)img.PixelHeight;

                    gfx0.DrawImage(img, startX + 40, startY + offsetY, picWdth, picHght);
                    offsetY += (int)picHght + 15;
                    oldoffsetY1 = offsetY;

                    startX = 300;
                    offsetY = oldoffsetY;
                    ght = (float)gfx0.MeasureString(
                      " Basic Data: ".ToUpper(), xfont2).Height;
                    //lblght = ght;
                    tf = new XTextFormatter(gfx0);
                    rect = new XRect(startX, startY + offsetY, 250, ght);
                    gfx0.DrawRectangle(XPens.Black, XBrushes.LightGray, rect);
                    tf.DrawString(" Basic Data: ".ToUpper()
                      , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                    offsetY += (int)ght + 15;

                    for (int j = 0; j < dtst.Tables[0].Columns.Count; j++)
                    {
                        if (dtst.Tables[0].Columns[j].Caption.StartsWith("mt") == false)
                        {
                            if (j == 7)
                            {
                                startX = 40;
                                offsetY = oldoffsetY1;
                                ght = (float)gfx0.MeasureString(" Other Basic Data: ".ToUpper(), xfont2).Height;
                                //lblght = ght;
                                tf = new XTextFormatter(gfx0);
                                rect = new XRect(startX, startY + offsetY, 245, ght);
                                gfx0.DrawRectangle(XPens.Black, XBrushes.LightGray, rect);
                                tf.DrawString(" Other Basic Data: ".ToUpper()
                                  , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                                offsetY += (int)ght + 15;
                            }
                            else if (j == 14)
                            {
                                startX = 300;
                                offsetY = oldoffsetY1;
                                ght = (float)gfx0.MeasureString(" Contact Information: ".ToUpper(), xfont2).Height;
                                //lblght = ght;
                                tf = new XTextFormatter(gfx0);
                                rect = new XRect(startX, startY + offsetY, 250, ght);
                                gfx0.DrawRectangle(XPens.Black, XBrushes.LightGray, rect);
                                tf.DrawString(" Contact Information: ".ToUpper()
                                  , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                                offsetY += (int)ght + 15;
                            }

                            if (dtst.Tables[0].Columns[j].Caption == "Religion")
                            {
                                if (orgType.ToUpper() == "CHURCH")
                                {
                                    dtst.Tables[0].Columns[j].Caption = "Place of Worship / Name of Service";
                                }
                            }
                            float lblght = 0;
                            nwLn = Global.mnFrm.cmCde.breakPDFTxtDown(
                             dtst.Tables[0].Columns[j].Caption + ": ",
                             140, font2, g);

                            ght = (float)gfx0.MeasureString(
                           string.Join("\n", nwLn), xfont2).Height;
                            lblght = ght;
                            tf = new XTextFormatter(gfx0);
                            rect = new XRect(startX, startY + offsetY - 7, 105, ght);
                            gfx0.DrawRectangle(XBrushes.White, rect);

                            tf.DrawString(string.Join("\n", nwLn)
                              , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);

                            nwLn = Global.mnFrm.cmCde.breakPDFTxtDown(
                              dtst.Tables[0].Rows[a][j].ToString(),
                              200, font41, g);
                            ght = (float)gfx0.MeasureString(
                           string.Join("\n", nwLn), xfont41).Height;
                            if (ght < 8)
                            {
                                ght = 8;
                            }

                            tf = new XTextFormatter(gfx0);
                            rect = new XRect(startX + 110, startY + offsetY - 7, 150, ght);
                            gfx0.DrawRectangle(XBrushes.White, rect);

                            tf.DrawString(string.Join("\n", nwLn)
                              , xfont41, XBrushes.Black, rect, XStringFormats.TopLeft);
                            if (ght < lblght)
                            {
                                ght = lblght;
                            }
                            offsetY += ght + 5;
                            if (hgstOffsetY < offsetY)
                            {
                                hgstOffsetY = offsetY;
                            }
                            if ((startY + offsetY) >= 700)
                            {
                                page0 = document.AddPage();
                                page0.Orientation = PageOrientation.Portrait;
                                page0.Height = XUnit.FromInch(11);
                                page0.Width = XUnit.FromInch(8.5);
                                gfx0 = XGraphics.FromPdfPage(page0);
                                offsetY = 0;
                                hgstOffsetY = 0;
                            }
                        }
                    }
                }

                //Person Types
                dtst = Global.getAllPrsnTypsRpt(long.Parse(this.prsnIDTextBox.Text));
                offsetY = hgstOffsetY + 5;
                oldoffsetY = offsetY;

                startX = 40;
                for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
                {
                    startX = 40;
                    if (a == 0)
                    {
                        hgstOffsetY = 0;
                        ght = (float)gfx0.MeasureString(
                        " Person's Relationship with this Organisation: ".ToUpper(), xfont2).Height;
                        //lblght = ght;
                        XTextFormatter tf = new XTextFormatter(gfx0);
                        XRect rect = new XRect(startX, startY + offsetY, pageWidth - startX, ght);
                        gfx0.DrawRectangle(XPens.Black, XBrushes.LightGray, rect);
                        tf.DrawString(" Person's Relationship with this Organisation: ".ToUpper()
                          , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                        offsetY += (int)ght + 5;
                        for (int j = 0; j < dtst.Tables[0].Columns.Count; j++)
                        {
                            if (dtst.Tables[0].Columns[j].Caption.StartsWith("mt") == false)
                            {
                                XSize sze = gfx0.MeasureString(
                             dtst.Tables[0].Columns[j].Caption, xfont2);
                                ght = (float)sze.Height;
                                float wdth = (float)sze.Width;
                                if (wdth < (float)(dtst.Tables[0].Columns[j].Caption.Length * 5))
                                {
                                    wdth = (float)(dtst.Tables[0].Columns[j].Caption.Length * 5);
                                }
                                tf = new XTextFormatter(gfx0);
                                rect = new XRect(startX, startY + offsetY, wdth + 5, ght);
                                gfx0.DrawRectangle(XPens.White, XBrushes.White, rect);
                                tf.DrawString(" " + dtst.Tables[0].Columns[j].Caption
                                  , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                                startX += wdth + 10;
                            }
                        }
                        offsetY += (int)ght + 5;
                        startX = 40;
                    }
                    float hghstght = 0;
                    for (int j = 0; j < dtst.Tables[0].Columns.Count; j++)
                    {
                        if (dtst.Tables[0].Columns[j].Caption.StartsWith("mt") == false)
                        {
                            XSize sze = gfx0.MeasureString(dtst.Tables[0].Columns[j].Caption, xfont2);
                            ght = (float)sze.Height;
                            float wdth = (float)(sze.Width);
                            if (wdth < (float)(dtst.Tables[0].Columns[j].Caption.Length * 5))
                            {
                                wdth = (float)(dtst.Tables[0].Columns[j].Caption.Length * 5);
                            }
                            nwLn = Global.mnFrm.cmCde.breakPDFTxtDown(
                              dtst.Tables[0].Rows[a][j].ToString(),
                              (int)(wdth * 1.5), font41, g);
                            ght = (float)gfx0.MeasureString(
                           string.Join("\n", nwLn), xfont41).Height;

                            XTextFormatter tf = new XTextFormatter(gfx0);
                            XRect rect = new XRect(startX + 7, startY + offsetY, wdth + 5, ght);
                            gfx0.DrawRectangle(XBrushes.White, rect);

                            tf.DrawString(string.Join("\n", nwLn)
                              , xfont41, XBrushes.Black, rect, XStringFormats.TopLeft);

                            startX += wdth + 10;
                            if (hghstght < ght)
                            {
                                hghstght = ght;
                            }
                        }
                    }
                    if (hghstght < 10)
                    {
                        hghstght = 10;
                    }
                    offsetY += hghstght + 5;
                    if (hgstOffsetY < offsetY)
                    {
                        hgstOffsetY = offsetY;
                    }
                    if ((startY + offsetY) >= 700)
                    {
                        page0 = document.AddPage();
                        page0.Orientation = PageOrientation.Portrait;
                        page0.Height = XUnit.FromInch(11);
                        page0.Width = XUnit.FromInch(8.5);
                        gfx0 = XGraphics.FromPdfPage(page0);
                        offsetY = 0;
                        hgstOffsetY = 0;
                    }
                }

                //Person's National IDs
                dtst = Global.getAllNtnltyRpt(long.Parse(this.prsnIDTextBox.Text));
                offsetY = hgstOffsetY + 5;
                oldoffsetY = offsetY;
                startX = 40;
                for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
                {
                    startX = 40;
                    if (a == 0)
                    {
                        hgstOffsetY = 0;
                        ght = (float)gfx0.MeasureString(
                        " National IDs: ".ToUpper(), xfont2).Height;
                        //lblght = ght;
                        XTextFormatter tf = new XTextFormatter(gfx0);
                        XRect rect = new XRect(startX, startY + offsetY, pageWidth - startX, ght);
                        gfx0.DrawRectangle(XPens.Black, XBrushes.LightGray, rect);
                        tf.DrawString(" National IDs: ".ToUpper()
                          , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                        offsetY += (int)ght + 5;
                        for (int j = 0; j < dtst.Tables[0].Columns.Count; j++)
                        {
                            if (dtst.Tables[0].Columns[j].Caption.StartsWith("mt") == false)
                            {
                                XSize sze = gfx0.MeasureString(
                             dtst.Tables[0].Columns[j].Caption, xfont2);
                                ght = (float)sze.Height;
                                float wdth = (float)sze.Width;
                                if (wdth < (float)(dtst.Tables[0].Columns[j].Caption.Length * 5))
                                {
                                    wdth = (float)(dtst.Tables[0].Columns[j].Caption.Length * 5);
                                }
                                tf = new XTextFormatter(gfx0);
                                rect = new XRect(startX, startY + offsetY, wdth + 5, ght);
                                gfx0.DrawRectangle(XPens.White, XBrushes.White, rect);
                                tf.DrawString(" " + dtst.Tables[0].Columns[j].Caption
                                  , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                                startX += wdth + 10;
                            }
                        }
                        offsetY += (int)ght + 5;
                        startX = 40;
                    }
                    float hghstght = 0;
                    for (int j = 0; j < dtst.Tables[0].Columns.Count; j++)
                    {
                        if (dtst.Tables[0].Columns[j].Caption.StartsWith("mt") == false)
                        {
                            XSize sze = gfx0.MeasureString(dtst.Tables[0].Columns[j].Caption, xfont2);
                            ght = (float)sze.Height;
                            float wdth = (float)(sze.Width);
                            if (wdth < (float)(dtst.Tables[0].Columns[j].Caption.Length * 5))
                            {
                                wdth = (float)(dtst.Tables[0].Columns[j].Caption.Length * 5);
                            }
                            nwLn = Global.mnFrm.cmCde.breakPDFTxtDown(
                              dtst.Tables[0].Rows[a][j].ToString(),
                              (int)(wdth * 1.33), font41, g);
                            ght = (float)gfx0.MeasureString(
                           string.Join("\n", nwLn), xfont41).Height;

                            XTextFormatter tf = new XTextFormatter(gfx0);
                            XRect rect = new XRect(startX + 7, startY + offsetY, wdth + 5, ght);
                            gfx0.DrawRectangle(XBrushes.White, rect);

                            tf.DrawString(string.Join("\n", nwLn)
                              , xfont41, XBrushes.Black, rect, XStringFormats.TopLeft);

                            startX += wdth + 10;
                            if (hghstght < ght)
                            {
                                hghstght = ght;
                            }
                        }
                    }
                    if (hghstght < 10)
                    {
                        hghstght = 10;
                    }
                    offsetY += hghstght + 5;
                    if (hgstOffsetY < offsetY)
                    {
                        hgstOffsetY = offsetY;
                    }
                    if ((startY + offsetY) >= 700)
                    {
                        page0 = document.AddPage();
                        page0.Orientation = PageOrientation.Portrait;
                        page0.Height = XUnit.FromInch(11);
                        page0.Width = XUnit.FromInch(8.5);
                        gfx0 = XGraphics.FromPdfPage(page0);
                        offsetY = 0;
                        hgstOffsetY = 0;
                    }
                }

                //Person's Relatives
                dtst = Global.getAllRltvsRpt(long.Parse(this.prsnIDTextBox.Text));
                offsetY = hgstOffsetY + 5;
                oldoffsetY = offsetY;
                startX = 40;
                for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
                {
                    startX = 40;
                    if (a == 0)
                    {
                        hgstOffsetY = 0;
                        ght = (float)gfx0.MeasureString(
                        " Relatives: ".ToUpper(), xfont2).Height;
                        //lblght = ght;
                        XTextFormatter tf = new XTextFormatter(gfx0);
                        XRect rect = new XRect(startX, startY + offsetY, pageWidth - startX, ght);
                        gfx0.DrawRectangle(XPens.Black, XBrushes.LightGray, rect);
                        tf.DrawString(" Relatives: ".ToUpper()
                          , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                        offsetY += (int)ght + 5;
                        for (int j = 0; j < dtst.Tables[0].Columns.Count; j++)
                        {
                            if (dtst.Tables[0].Columns[j].Caption.StartsWith("mt") == false)
                            {
                                XSize sze = gfx0.MeasureString(
                             dtst.Tables[0].Columns[j].Caption, xfont2);
                                ght = (float)sze.Height;
                                float wdth = (float)sze.Width;
                                if (wdth < (float)(dtst.Tables[0].Columns[j].Caption.Length * 5))
                                {
                                    wdth = (float)(dtst.Tables[0].Columns[j].Caption.Length * 5);
                                }
                                tf = new XTextFormatter(gfx0);
                                rect = new XRect(startX, startY + offsetY, wdth + 5, ght);
                                gfx0.DrawRectangle(XPens.White, XBrushes.White, rect);
                                tf.DrawString(" " + dtst.Tables[0].Columns[j].Caption
                                  , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                                startX += wdth + 10;
                            }
                        }
                        offsetY += (int)ght + 5;
                        startX = 40;
                    }
                    float hghstght = 0;
                    for (int j = 0; j < dtst.Tables[0].Columns.Count; j++)
                    {
                        if (dtst.Tables[0].Columns[j].Caption.StartsWith("mt") == false)
                        {
                            XSize sze = gfx0.MeasureString(" " + dtst.Tables[0].Columns[j].Caption, xfont2);
                            ght = (float)sze.Height;
                            float wdth = (float)(sze.Width);
                            if (wdth < (float)(dtst.Tables[0].Columns[j].Caption.Length * 5))
                            {
                                wdth = (float)(dtst.Tables[0].Columns[j].Caption.Length * 5);
                            }
                            nwLn = Global.mnFrm.cmCde.breakPDFTxtDown(
                              dtst.Tables[0].Rows[a][j].ToString(),
                              (int)(wdth * 1.33), font41, g);
                            ght = (float)gfx0.MeasureString(
                           string.Join("\n", nwLn), xfont41).Height;

                            XTextFormatter tf = new XTextFormatter(gfx0);
                            XRect rect = new XRect(startX + 7, startY + offsetY, wdth + 5, ght);
                            gfx0.DrawRectangle(XBrushes.White, rect);

                            tf.DrawString(string.Join("\n", nwLn)
                              , xfont41, XBrushes.Black, rect, XStringFormats.TopLeft);

                            startX += wdth + 10;
                            if (hghstght < ght)
                            {
                                hghstght = ght;
                            }
                        }
                    }
                    if (hghstght < 10)
                    {
                        hghstght = 10;
                    }
                    offsetY += hghstght + 5;
                    if (hgstOffsetY < offsetY)
                    {
                        hgstOffsetY = offsetY;
                    }
                    if ((startY + offsetY) >= 700)
                    {
                        page0 = document.AddPage();
                        page0.Orientation = PageOrientation.Portrait;
                        page0.Height = XUnit.FromInch(11);
                        page0.Width = XUnit.FromInch(8.5);
                        gfx0 = XGraphics.FromPdfPage(page0);
                        offsetY = 0;
                        hgstOffsetY = 0;
                    }
                }

                //Person's Educational Background
                dtst = Global.getAllEducRpt(long.Parse(this.prsnIDTextBox.Text));
                offsetY = hgstOffsetY + 5;
                oldoffsetY = offsetY;
                startX = 40;
                for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
                {
                    startX = 40;
                    if (a == 0)
                    {
                        hgstOffsetY = 0;
                        ght = (float)gfx0.MeasureString(
                        " Educational Background: ".ToUpper(), xfont2).Height;
                        //lblght = ght;
                        XTextFormatter tf = new XTextFormatter(gfx0);
                        XRect rect = new XRect(startX, startY + offsetY, pageWidth - startX, ght);
                        gfx0.DrawRectangle(XPens.Black, XBrushes.LightGray, rect);
                        tf.DrawString(" Educational Background: ".ToUpper()
                          , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                        offsetY += (int)ght + 5;
                        for (int j = 0; j < dtst.Tables[0].Columns.Count; j++)
                        {
                            if (dtst.Tables[0].Columns[j].Caption.StartsWith("mt") == false)
                            {
                                XSize sze = gfx0.MeasureString(
                             dtst.Tables[0].Columns[j].Caption, xfont2);
                                ght = (float)sze.Height;
                                float wdth = (float)sze.Width;
                                if (wdth < (float)(dtst.Tables[0].Columns[j].Caption.Length * 5))
                                {
                                    wdth = (float)(dtst.Tables[0].Columns[j].Caption.Length * 5);
                                }
                                tf = new XTextFormatter(gfx0);
                                rect = new XRect(startX, startY + offsetY, wdth + 5, ght);
                                gfx0.DrawRectangle(XPens.White, XBrushes.White, rect);
                                tf.DrawString(" " + dtst.Tables[0].Columns[j].Caption
                                  , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                                startX += wdth + 10;
                            }
                        }
                        offsetY += (int)ght + 5;
                        startX = 40;
                    }
                    float hghstght = 0;
                    for (int j = 0; j < dtst.Tables[0].Columns.Count; j++)
                    {
                        if (dtst.Tables[0].Columns[j].Caption.StartsWith("mt") == false)
                        {
                            XSize sze = gfx0.MeasureString(" " + dtst.Tables[0].Columns[j].Caption, xfont2);
                            ght = (float)sze.Height;
                            float wdth = (float)(sze.Width);
                            if (wdth < (float)(dtst.Tables[0].Columns[j].Caption.Length * 5))
                            {
                                wdth = (float)(dtst.Tables[0].Columns[j].Caption.Length * 5);
                            }
                            nwLn = Global.mnFrm.cmCde.breakPDFTxtDown(
                              dtst.Tables[0].Rows[a][j].ToString(),
                              (int)(wdth * 1.33), font41, g);
                            ght = (float)gfx0.MeasureString(
                           string.Join("\n", nwLn), xfont41).Height;

                            XTextFormatter tf = new XTextFormatter(gfx0);
                            XRect rect = new XRect(startX + 7, startY + offsetY, wdth + 5, ght);
                            gfx0.DrawRectangle(XBrushes.White, rect);

                            tf.DrawString(string.Join("\n", nwLn)
                              , xfont41, XBrushes.Black, rect, XStringFormats.TopLeft);

                            startX += wdth + 10;
                            if (hghstght < ght)
                            {
                                hghstght = ght;
                            }
                        }
                    }
                    if (hghstght < 10)
                    {
                        hghstght = 10;
                    }
                    offsetY += hghstght + 5;
                    if (hgstOffsetY < offsetY)
                    {
                        hgstOffsetY = offsetY;
                    }
                    if ((startY + offsetY) >= 700)
                    {
                        page0 = document.AddPage();
                        page0.Orientation = PageOrientation.Portrait;
                        page0.Height = XUnit.FromInch(11);
                        page0.Width = XUnit.FromInch(8.5);
                        gfx0 = XGraphics.FromPdfPage(page0);
                        offsetY = 0;
                        hgstOffsetY = 0;
                    }
                }

                //Person's Working Experience
                dtst = Global.getAllWrkExpRpt(long.Parse(this.prsnIDTextBox.Text));
                offsetY = hgstOffsetY + 5;
                oldoffsetY = offsetY;
                startX = 40;
                for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
                {
                    startX = 40;
                    if (a == 0)
                    {
                        hgstOffsetY = 0;
                        ght = (float)gfx0.MeasureString(
                        " Working Experience: ".ToUpper(), xfont2).Height;
                        //lblght = ght;
                        XTextFormatter tf = new XTextFormatter(gfx0);
                        XRect rect = new XRect(startX, startY + offsetY, pageWidth - startX, ght);
                        gfx0.DrawRectangle(XPens.Black, XBrushes.LightGray, rect);
                        tf.DrawString(" Working Experience: ".ToUpper()
                          , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                        offsetY += (int)ght + 5;
                        for (int j = 0; j < dtst.Tables[0].Columns.Count; j++)
                        {
                            if (dtst.Tables[0].Columns[j].Caption.StartsWith("mt") == false)
                            {
                                XSize sze = gfx0.MeasureString(
                             dtst.Tables[0].Columns[j].Caption, xfont2);
                                ght = (float)sze.Height;
                                float wdth = (float)sze.Width;
                                if (wdth < (float)(dtst.Tables[0].Columns[j].Caption.Length * 5))
                                {
                                    wdth = (float)(dtst.Tables[0].Columns[j].Caption.Length * 5);
                                }
                                tf = new XTextFormatter(gfx0);
                                rect = new XRect(startX, startY + offsetY, wdth + 5, ght);
                                gfx0.DrawRectangle(XPens.White, XBrushes.White, rect);
                                tf.DrawString(" " + dtst.Tables[0].Columns[j].Caption
                                  , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                                startX += wdth + 10;
                            }
                        }
                        offsetY += (int)ght + 5;
                        startX = 40;
                    }
                    float hghstght = 0;
                    for (int j = 0; j < dtst.Tables[0].Columns.Count; j++)
                    {
                        if (dtst.Tables[0].Columns[j].Caption.StartsWith("mt") == false)
                        {
                            XSize sze = gfx0.MeasureString(" " + dtst.Tables[0].Columns[j].Caption, xfont2);
                            ght = (float)sze.Height;
                            float wdth = (float)(sze.Width);
                            if (wdth < (float)(dtst.Tables[0].Columns[j].Caption.Length * 5))
                            {
                                wdth = (float)(dtst.Tables[0].Columns[j].Caption.Length * 5);
                            }
                            nwLn = Global.mnFrm.cmCde.breakPDFTxtDown(
                              dtst.Tables[0].Rows[a][j].ToString(),
                              (int)(wdth * 1.33), font41, g);
                            ght = (float)gfx0.MeasureString(
                           string.Join("\n", nwLn), xfont41).Height;

                            XTextFormatter tf = new XTextFormatter(gfx0);
                            XRect rect = new XRect(startX + 7, startY + offsetY, wdth + 5, ght);
                            gfx0.DrawRectangle(XBrushes.White, rect);

                            tf.DrawString(string.Join("\n", nwLn)
                              , xfont41, XBrushes.Black, rect, XStringFormats.TopLeft);

                            startX += wdth + 10;
                            if (hghstght < ght)
                            {
                                hghstght = ght;
                            }
                        }
                    }
                    if (hghstght < 10)
                    {
                        hghstght = 10;
                    }
                    offsetY += hghstght + 5;
                    if (hgstOffsetY < offsetY)
                    {
                        hgstOffsetY = offsetY;
                    }
                    if ((startY + offsetY) >= 700)
                    {
                        page0 = document.AddPage();
                        page0.Orientation = PageOrientation.Portrait;
                        page0.Height = XUnit.FromInch(11);
                        page0.Width = XUnit.FromInch(8.5);
                        gfx0 = XGraphics.FromPdfPage(page0);
                        offsetY = 0;
                        hgstOffsetY = 0;
                    }
                }

                //Person's Skills/Nature
                dtst = Global.getAllSkillsRpt(long.Parse(this.prsnIDTextBox.Text));
                offsetY = hgstOffsetY + 5;
                oldoffsetY = offsetY;
                startX = 40;
                for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
                {
                    startX = 40;
                    if (a == 0)
                    {
                        hgstOffsetY = 0;
                        ght = (float)gfx0.MeasureString(
                        " Skills/Nature: ".ToUpper(), xfont2).Height;
                        //lblght = ght;
                        XTextFormatter tf = new XTextFormatter(gfx0);
                        XRect rect = new XRect(startX, startY + offsetY, pageWidth - startX, ght);
                        gfx0.DrawRectangle(XPens.Black, XBrushes.LightGray, rect);
                        tf.DrawString(" Skills/Nature: ".ToUpper()
                          , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                        offsetY += (int)ght + 5;
                        for (int j = 0; j < dtst.Tables[0].Columns.Count; j++)
                        {
                            if (dtst.Tables[0].Columns[j].Caption.StartsWith("mt") == false)
                            {
                                XSize sze = gfx0.MeasureString(
                             dtst.Tables[0].Columns[j].Caption, xfont2);
                                ght = (float)sze.Height;
                                float wdth = (float)sze.Width;
                                if (wdth < (float)(dtst.Tables[0].Columns[j].Caption.Length * 5))
                                {
                                    wdth = (float)(dtst.Tables[0].Columns[j].Caption.Length * 5);
                                }
                                tf = new XTextFormatter(gfx0);
                                rect = new XRect(startX, startY + offsetY, wdth + 5, ght);
                                gfx0.DrawRectangle(XPens.White, XBrushes.White, rect);
                                tf.DrawString(" " + dtst.Tables[0].Columns[j].Caption
                                  , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                                startX += wdth + 10;
                            }
                        }
                        offsetY += (int)ght + 5;
                        startX = 40;
                    }
                    float hghstght = 0;
                    for (int j = 0; j < dtst.Tables[0].Columns.Count; j++)
                    {
                        if (dtst.Tables[0].Columns[j].Caption.StartsWith("mt") == false)
                        {
                            XSize sze = gfx0.MeasureString(" " + dtst.Tables[0].Columns[j].Caption, xfont2);
                            ght = (float)sze.Height;
                            float wdth = (float)(sze.Width);
                            if (wdth < (float)(dtst.Tables[0].Columns[j].Caption.Length * 5))
                            {
                                wdth = (float)(dtst.Tables[0].Columns[j].Caption.Length * 5);
                            }
                            nwLn = Global.mnFrm.cmCde.breakPDFTxtDown(
                              dtst.Tables[0].Rows[a][j].ToString(),
                              (int)(wdth * 1.33), font41, g);
                            ght = (float)gfx0.MeasureString(
                           string.Join("\n", nwLn), xfont41).Height;

                            XTextFormatter tf = new XTextFormatter(gfx0);
                            XRect rect = new XRect(startX + 7, startY + offsetY, wdth + 5, ght);
                            gfx0.DrawRectangle(XBrushes.White, rect);

                            tf.DrawString(string.Join("\n", nwLn)
                              , xfont41, XBrushes.Black, rect, XStringFormats.TopLeft);

                            startX += wdth + 10;
                            if (hghstght < ght)
                            {
                                hghstght = ght;
                            }
                        }
                    }
                    if (hghstght < 10)
                    {
                        hghstght = 10;
                    }
                    offsetY += hghstght + 5;
                    if (hgstOffsetY < offsetY)
                    {
                        hgstOffsetY = offsetY;
                    }
                    if ((startY + offsetY) >= 700)
                    {
                        page0 = document.AddPage();
                        page0.Orientation = PageOrientation.Portrait;
                        page0.Height = XUnit.FromInch(11);
                        page0.Width = XUnit.FromInch(8.5);
                        gfx0 = XGraphics.FromPdfPage(page0);
                        offsetY = 0;
                        hgstOffsetY = 0;
                    }
                }

                //Person's Divisions/Groups
                dtst = Global.getAllDivsRpts(long.Parse(this.prsnIDTextBox.Text));
                offsetY = hgstOffsetY + 5;
                oldoffsetY = offsetY;
                startX = 40;
                for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
                {
                    startX = 40;
                    if (a == 0)
                    {
                        hgstOffsetY = 0;
                        ght = (float)gfx0.MeasureString(
                        " Groups/Associations: ".ToUpper(), xfont2).Height;
                        //lblght = ght;
                        XTextFormatter tf = new XTextFormatter(gfx0);
                        XRect rect = new XRect(startX, startY + offsetY, pageWidth - startX, ght);
                        gfx0.DrawRectangle(XPens.Black, XBrushes.LightGray, rect);
                        tf.DrawString(" Groups/Associations: ".ToUpper()
                          , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                        offsetY += (int)ght + 5;
                        for (int j = 0; j < dtst.Tables[0].Columns.Count; j++)
                        {
                            if (dtst.Tables[0].Columns[j].Caption.StartsWith("mt") == false)
                            {
                                XSize sze = gfx0.MeasureString(
                             dtst.Tables[0].Columns[j].Caption, xfont2);
                                ght = (float)sze.Height;
                                float wdth = (float)sze.Width;
                                if (wdth < (float)(dtst.Tables[0].Columns[j].Caption.Length * 5))
                                {
                                    wdth = (float)(dtst.Tables[0].Columns[j].Caption.Length * 5);
                                }
                                tf = new XTextFormatter(gfx0);
                                rect = new XRect(startX, startY + offsetY, wdth + 5, ght);
                                gfx0.DrawRectangle(XPens.White, XBrushes.White, rect);
                                tf.DrawString(" " + dtst.Tables[0].Columns[j].Caption
                                  , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                                startX += wdth + 10;
                            }
                        }
                        offsetY += (int)ght + 5;
                        startX = 40;
                    }
                    float hghstght = 0;
                    for (int j = 0; j < dtst.Tables[0].Columns.Count; j++)
                    {
                        if (dtst.Tables[0].Columns[j].Caption.StartsWith("mt") == false)
                        {
                            XSize sze = gfx0.MeasureString(" " + dtst.Tables[0].Columns[j].Caption, xfont2);
                            ght = (float)sze.Height;
                            float wdth = (float)(sze.Width);
                            if (wdth < (float)(dtst.Tables[0].Columns[j].Caption.Length * 5))
                            {
                                wdth = (float)(dtst.Tables[0].Columns[j].Caption.Length * 5);
                            }
                            nwLn = Global.mnFrm.cmCde.breakPDFTxtDown(
                              dtst.Tables[0].Rows[a][j].ToString(),
                              (int)(wdth * 1.33), font41, g);
                            ght = (float)gfx0.MeasureString(
                           string.Join("\n", nwLn), xfont41).Height;

                            XTextFormatter tf = new XTextFormatter(gfx0);
                            XRect rect = new XRect(startX + 7, startY + offsetY, wdth + 5, ght);
                            gfx0.DrawRectangle(XBrushes.White, rect);

                            tf.DrawString(string.Join("\n", nwLn)
                              , xfont41, XBrushes.Black, rect, XStringFormats.TopLeft);

                            startX += wdth + 10;
                            if (hghstght < ght)
                            {
                                hghstght = ght;
                            }
                        }
                    }
                    if (hghstght < 10)
                    {
                        hghstght = 10;
                    }
                    offsetY += hghstght + 5;
                    if (hgstOffsetY < offsetY)
                    {
                        hgstOffsetY = offsetY;
                    }
                    if ((startY + offsetY) >= 700)
                    {
                        page0 = document.AddPage();
                        page0.Orientation = PageOrientation.Portrait;
                        page0.Height = XUnit.FromInch(11);
                        page0.Width = XUnit.FromInch(8.5);
                        gfx0 = XGraphics.FromPdfPage(page0);
                        offsetY = 0;
                        hgstOffsetY = 0;
                    }
                }

                //Person's Branches/Locations
                dtst = Global.getAllSitesRpts(long.Parse(this.prsnIDTextBox.Text));
                offsetY = hgstOffsetY + 5;
                oldoffsetY = offsetY;
                startX = 40;
                for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
                {
                    startX = 40;
                    if (a == 0)
                    {
                        hgstOffsetY = 0;
                        ght = (float)gfx0.MeasureString(
                        " This Organisation's Branches/Sites Assigned: ".ToUpper(), xfont2).Height;
                        //lblght = ght;
                        XTextFormatter tf = new XTextFormatter(gfx0);
                        XRect rect = new XRect(startX, startY + offsetY, pageWidth - startX, ght);
                        gfx0.DrawRectangle(XPens.Black, XBrushes.LightGray, rect);
                        tf.DrawString(" This Organisation's Branches/Sites Assigned: ".ToUpper()
                          , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                        offsetY += (int)ght + 5;
                        for (int j = 0; j < dtst.Tables[0].Columns.Count; j++)
                        {
                            if (dtst.Tables[0].Columns[j].Caption.StartsWith("mt") == false)
                            {
                                XSize sze = gfx0.MeasureString(
                             dtst.Tables[0].Columns[j].Caption, xfont2);
                                ght = (float)sze.Height;
                                float wdth = (float)sze.Width;
                                if (wdth < (float)(dtst.Tables[0].Columns[j].Caption.Length * 5))
                                {
                                    wdth = (float)(dtst.Tables[0].Columns[j].Caption.Length * 5);
                                }
                                tf = new XTextFormatter(gfx0);
                                rect = new XRect(startX, startY + offsetY, wdth + 5, ght);
                                gfx0.DrawRectangle(XPens.White, XBrushes.White, rect);
                                tf.DrawString(" " + dtst.Tables[0].Columns[j].Caption
                                  , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                                startX += wdth + 10;
                            }
                        }
                        offsetY += (int)ght + 5;
                        startX = 40;
                    }
                    float hghstght = 0;
                    for (int j = 0; j < dtst.Tables[0].Columns.Count; j++)
                    {
                        if (dtst.Tables[0].Columns[j].Caption.StartsWith("mt") == false)
                        {
                            XSize sze = gfx0.MeasureString(" " + dtst.Tables[0].Columns[j].Caption, xfont2);
                            ght = (float)sze.Height;
                            float wdth = (float)(sze.Width);
                            if (wdth < (float)(dtst.Tables[0].Columns[j].Caption.Length * 5))
                            {
                                wdth = (float)(dtst.Tables[0].Columns[j].Caption.Length * 5);
                            }
                            nwLn = Global.mnFrm.cmCde.breakPDFTxtDown(
                              dtst.Tables[0].Rows[a][j].ToString(),
                              (int)(wdth * 1.33), font41, g);
                            ght = (float)gfx0.MeasureString(
                           string.Join("\n", nwLn), xfont41).Height;

                            XTextFormatter tf = new XTextFormatter(gfx0);
                            XRect rect = new XRect(startX + 7, startY + offsetY, wdth + 5, ght);
                            gfx0.DrawRectangle(XBrushes.White, rect);

                            tf.DrawString(string.Join("\n", nwLn)
                              , xfont41, XBrushes.Black, rect, XStringFormats.TopLeft);

                            startX += wdth + 10;
                            if (hghstght < ght)
                            {
                                hghstght = ght;
                            }
                        }
                    }
                    if (hghstght < 10)
                    {
                        hghstght = 10;
                    }
                    offsetY += hghstght + 5;
                    if (hgstOffsetY < offsetY)
                    {
                        hgstOffsetY = offsetY;
                    }
                    if ((startY + offsetY) >= 700)
                    {
                        page0 = document.AddPage();
                        page0.Orientation = PageOrientation.Portrait;
                        page0.Height = XUnit.FromInch(11);
                        page0.Width = XUnit.FromInch(8.5);
                        gfx0 = XGraphics.FromPdfPage(page0);
                        offsetY = 0;
                        hgstOffsetY = 0;
                    }
                }

                //Person's Jobs
                dtst = Global.getAllJobsRpt(long.Parse(this.prsnIDTextBox.Text));
                offsetY = hgstOffsetY + 5;
                oldoffsetY = offsetY;
                startX = 40;
                for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
                {
                    startX = 40;
                    if (a == 0)
                    {
                        hgstOffsetY = 0;
                        ght = (float)gfx0.MeasureString(
                        " Jobs: ".ToUpper(), xfont2).Height;
                        //lblght = ght;
                        XTextFormatter tf = new XTextFormatter(gfx0);
                        XRect rect = new XRect(startX, startY + offsetY, pageWidth - startX, ght);
                        gfx0.DrawRectangle(XPens.Black, XBrushes.LightGray, rect);
                        tf.DrawString(" Jobs: ".ToUpper()
                          , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                        offsetY += (int)ght + 5;
                        for (int j = 0; j < dtst.Tables[0].Columns.Count; j++)
                        {
                            if (dtst.Tables[0].Columns[j].Caption.StartsWith("mt") == false)
                            {
                                XSize sze = gfx0.MeasureString(
                             dtst.Tables[0].Columns[j].Caption, xfont2);
                                ght = (float)sze.Height;
                                float wdth = (float)sze.Width;
                                if (wdth < (float)(dtst.Tables[0].Columns[j].Caption.Length * 5))
                                {
                                    wdth = (float)(dtst.Tables[0].Columns[j].Caption.Length * 5);
                                }
                                tf = new XTextFormatter(gfx0);
                                rect = new XRect(startX, startY + offsetY, wdth + 5, ght);
                                gfx0.DrawRectangle(XPens.White, XBrushes.White, rect);
                                tf.DrawString(" " + dtst.Tables[0].Columns[j].Caption
                                  , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                                startX += wdth + 10;
                            }
                        }
                        offsetY += (int)ght + 5;
                        startX = 40;
                    }
                    float hghstght = 0;
                    for (int j = 0; j < dtst.Tables[0].Columns.Count; j++)
                    {
                        if (dtst.Tables[0].Columns[j].Caption.StartsWith("mt") == false)
                        {
                            XSize sze = gfx0.MeasureString(" " + dtst.Tables[0].Columns[j].Caption, xfont2);
                            ght = (float)sze.Height;
                            float wdth = (float)(sze.Width);
                            if (wdth < (float)(dtst.Tables[0].Columns[j].Caption.Length * 5))
                            {
                                wdth = (float)(dtst.Tables[0].Columns[j].Caption.Length * 5);
                            }
                            nwLn = Global.mnFrm.cmCde.breakPDFTxtDown(
                              dtst.Tables[0].Rows[a][j].ToString(),
                              (int)(wdth * 1.33), font41, g);
                            ght = (float)gfx0.MeasureString(
                           string.Join("\n", nwLn), xfont41).Height;

                            XTextFormatter tf = new XTextFormatter(gfx0);
                            XRect rect = new XRect(startX + 7, startY + offsetY, wdth + 5, ght);
                            gfx0.DrawRectangle(XBrushes.White, rect);

                            tf.DrawString(string.Join("\n", nwLn)
                              , xfont41, XBrushes.Black, rect, XStringFormats.TopLeft);

                            startX += wdth + 10;
                            if (hghstght < ght)
                            {
                                hghstght = ght;
                            }
                        }
                    }
                    if (hghstght < 10)
                    {
                        hghstght = 10;
                    }
                    offsetY += hghstght + 5;
                    if (hgstOffsetY < offsetY)
                    {
                        hgstOffsetY = offsetY;
                    }
                    if ((startY + offsetY) >= 700)
                    {
                        page0 = document.AddPage();
                        page0.Orientation = PageOrientation.Portrait;
                        page0.Height = XUnit.FromInch(11);
                        page0.Width = XUnit.FromInch(8.5);
                        gfx0 = XGraphics.FromPdfPage(page0);
                        offsetY = 0;
                        hgstOffsetY = 0;
                    }
                }

                //Person's Grades
                dtst = Global.getAllGradesRpt(long.Parse(this.prsnIDTextBox.Text));
                offsetY = hgstOffsetY + 5;
                oldoffsetY = offsetY;
                startX = 40;
                for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
                {
                    startX = 40;
                    if (a == 0)
                    {
                        hgstOffsetY = 0;
                        ght = (float)gfx0.MeasureString(
                        " Grades: ".ToUpper(), xfont2).Height;
                        //lblght = ght;
                        XTextFormatter tf = new XTextFormatter(gfx0);
                        XRect rect = new XRect(startX, startY + offsetY, pageWidth - startX, ght);
                        gfx0.DrawRectangle(XPens.Black, XBrushes.LightGray, rect);
                        tf.DrawString(" Grades: ".ToUpper()
                          , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                        offsetY += (int)ght + 5;
                        for (int j = 0; j < dtst.Tables[0].Columns.Count; j++)
                        {
                            if (dtst.Tables[0].Columns[j].Caption.StartsWith("mt") == false)
                            {
                                XSize sze = gfx0.MeasureString(
                             dtst.Tables[0].Columns[j].Caption, xfont2);
                                ght = (float)sze.Height;
                                float wdth = (float)sze.Width;
                                if (wdth < (float)(dtst.Tables[0].Columns[j].Caption.Length * 5))
                                {
                                    wdth = (float)(dtst.Tables[0].Columns[j].Caption.Length * 5);
                                }
                                tf = new XTextFormatter(gfx0);
                                rect = new XRect(startX, startY + offsetY, wdth + 5, ght);
                                gfx0.DrawRectangle(XPens.White, XBrushes.White, rect);
                                tf.DrawString(" " + dtst.Tables[0].Columns[j].Caption
                                  , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                                startX += wdth + 10;
                            }
                        }
                        offsetY += (int)ght + 5;
                        startX = 40;
                    }
                    float hghstght = 0;
                    for (int j = 0; j < dtst.Tables[0].Columns.Count; j++)
                    {
                        if (dtst.Tables[0].Columns[j].Caption.StartsWith("mt") == false)
                        {
                            XSize sze = gfx0.MeasureString(" " + dtst.Tables[0].Columns[j].Caption, xfont2);
                            ght = (float)sze.Height;
                            float wdth = (float)(sze.Width);
                            if (wdth < (float)(dtst.Tables[0].Columns[j].Caption.Length * 5))
                            {
                                wdth = (float)(dtst.Tables[0].Columns[j].Caption.Length * 5);
                            }
                            nwLn = Global.mnFrm.cmCde.breakPDFTxtDown(
                              dtst.Tables[0].Rows[a][j].ToString(),
                              (int)(wdth * 1.33), font41, g);
                            ght = (float)gfx0.MeasureString(
                           string.Join("\n", nwLn), xfont41).Height;

                            XTextFormatter tf = new XTextFormatter(gfx0);
                            XRect rect = new XRect(startX + 7, startY + offsetY, wdth + 5, ght);
                            gfx0.DrawRectangle(XBrushes.White, rect);

                            tf.DrawString(string.Join("\n", nwLn)
                              , xfont41, XBrushes.Black, rect, XStringFormats.TopLeft);

                            startX += wdth + 10;
                            if (hghstght < ght)
                            {
                                hghstght = ght;
                            }
                        }
                    }
                    if (hghstght < 10)
                    {
                        hghstght = 10;
                    }
                    offsetY += hghstght + 5;
                    if (hgstOffsetY < offsetY)
                    {
                        hgstOffsetY = offsetY;
                    }
                    if ((startY + offsetY) >= 700)
                    {
                        page0 = document.AddPage();
                        page0.Orientation = PageOrientation.Portrait;
                        page0.Height = XUnit.FromInch(11);
                        page0.Width = XUnit.FromInch(8.5);
                        gfx0 = XGraphics.FromPdfPage(page0);
                        offsetY = 0;
                        hgstOffsetY = 0;
                    }
                }

                //Person's Positions
                dtst = Global.getAllPositionsRpt(long.Parse(this.prsnIDTextBox.Text));
                offsetY = hgstOffsetY + 5;
                oldoffsetY = offsetY;
                startX = 40;
                for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
                {
                    startX = 40;
                    if (a == 0)
                    {
                        hgstOffsetY = 0;
                        ght = (float)gfx0.MeasureString(
                        " Positions: ".ToUpper(), xfont2).Height;
                        //lblght = ght;
                        XTextFormatter tf = new XTextFormatter(gfx0);
                        XRect rect = new XRect(startX, startY + offsetY, pageWidth - startX, ght);
                        gfx0.DrawRectangle(XPens.Black, XBrushes.LightGray, rect);
                        tf.DrawString(" Positions: ".ToUpper()
                          , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                        offsetY += (int)ght + 5;
                        for (int j = 0; j < dtst.Tables[0].Columns.Count; j++)
                        {
                            if (dtst.Tables[0].Columns[j].Caption.StartsWith("mt") == false)
                            {
                                XSize sze = gfx0.MeasureString(
                             dtst.Tables[0].Columns[j].Caption, xfont2);
                                ght = (float)sze.Height;
                                float wdth = (float)sze.Width;
                                if (wdth < (float)(dtst.Tables[0].Columns[j].Caption.Length * 5))
                                {
                                    wdth = (float)(dtst.Tables[0].Columns[j].Caption.Length * 5);
                                }
                                tf = new XTextFormatter(gfx0);
                                rect = new XRect(startX, startY + offsetY, wdth + 5, ght);
                                gfx0.DrawRectangle(XPens.White, XBrushes.White, rect);
                                tf.DrawString(" " + dtst.Tables[0].Columns[j].Caption
                                  , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                                startX += wdth + 10;
                            }
                        }
                        offsetY += (int)ght + 5;
                        startX = 40;
                    }
                    float hghstght = 0;
                    for (int j = 0; j < dtst.Tables[0].Columns.Count; j++)
                    {
                        if (dtst.Tables[0].Columns[j].Caption.StartsWith("mt") == false)
                        {
                            XSize sze = gfx0.MeasureString(" " + dtst.Tables[0].Columns[j].Caption, xfont2);
                            ght = (float)sze.Height;
                            float wdth = (float)(sze.Width);
                            if (wdth < (float)(dtst.Tables[0].Columns[j].Caption.Length * 5))
                            {
                                wdth = (float)(dtst.Tables[0].Columns[j].Caption.Length * 5);
                            }
                            nwLn = Global.mnFrm.cmCde.breakPDFTxtDown(
                              dtst.Tables[0].Rows[a][j].ToString(),
                              (int)(wdth * 1.33), font41, g);
                            ght = (float)gfx0.MeasureString(
                           string.Join("\n", nwLn), xfont41).Height;

                            XTextFormatter tf = new XTextFormatter(gfx0);
                            XRect rect = new XRect(startX + 7, startY + offsetY, wdth + 5, ght);
                            gfx0.DrawRectangle(XBrushes.White, rect);

                            tf.DrawString(string.Join("\n", nwLn)
                              , xfont41, XBrushes.Black, rect, XStringFormats.TopLeft);

                            startX += wdth + 10;
                            if (hghstght < ght)
                            {
                                hghstght = ght;
                            }
                        }
                    }
                    if (hghstght < 10)
                    {
                        hghstght = 10;
                    }
                    offsetY += hghstght + 5;
                    if (hgstOffsetY < offsetY)
                    {
                        hgstOffsetY = offsetY;
                    }
                    if ((startY + offsetY) >= 700)
                    {
                        page0 = document.AddPage();
                        page0.Orientation = PageOrientation.Portrait;
                        page0.Height = XUnit.FromInch(11);
                        page0.Width = XUnit.FromInch(8.5);
                        gfx0 = XGraphics.FromPdfPage(page0);
                        offsetY = 0;
                        hgstOffsetY = 0;
                    }
                }

                //Additionnal Person Data
                dtst = Global.get_PrsExtrDataGrps(Global.mnFrm.cmCde.Org_id);
                offsetY = hgstOffsetY + 5;
                oldoffsetY = offsetY;
                startX = 40;
                for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
                {
                    offsetY = hgstOffsetY + 5;
                    oldoffsetY = offsetY;
                    startX = 40;
                    hgstOffsetY = 0;
                    ght = (float)gfx0.MeasureString(
                    " " + dtst.Tables[0].Rows[a][0].ToString().ToUpper(), xfont2).Height;
                    //lblght = ght;
                    XTextFormatter tf = new XTextFormatter(gfx0);
                    XRect rect = new XRect(startX, startY + offsetY, pageWidth - startX, ght);
                    gfx0.DrawRectangle(XPens.Black, XBrushes.LightGray, rect);
                    tf.DrawString(" " + dtst.Tables[0].Rows[a][0].ToString().ToUpper()
                      , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                    offsetY += ght + 10;
                    startX = 40;

                    DataSet fldDtSt = Global.get_PrsExtrDataGrpCols(
                   dtst.Tables[0].Rows[a][0].ToString(),
                   Global.mnFrm.cmCde.Org_id);
                    float hghstght = 0;
                    float lblght = 0;
                    int j = 0;
                    for (j = 0; j < fldDtSt.Tables[0].Rows.Count; j++)
                    {
                        int mdlr = j % 2;
                        if ((j % 2) == 0)
                        {
                            hghstght = 0;
                            lblght = 0;
                            startX = 40;
                        }
                        else
                        {
                            startX = 280;
                        }
                        nwLn = Global.mnFrm.cmCde.breakPDFTxtDown(
                         fldDtSt.Tables[0].Rows[j][2].ToString() + ": ",
                         120, font2, g);

                        ght = (float)(nwLn.Length * font2Hght);//gfx0.MeasureString("  "+string.Join("\n", nwLn), xfont2).Height;
                        lblght = ght;
                        tf = new XTextFormatter(gfx0);
                        rect = new XRect(startX + 7, startY + offsetY, 100, ght);
                        gfx0.DrawRectangle(XBrushes.White, rect);
                        //nwLn.Length.ToString() + "--" + ght.ToString() +
                        tf.DrawString(string.Join("\n", nwLn)
                          , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);

                        string colData = Global.get_OnePrsExtrData("data_col" + fldDtSt.Tables[0].Rows[j][1].ToString(), long.Parse(this.prsnIDTextBox.Text));
                        if (fldDtSt.Tables[0].Rows[j][7].ToString() == "Tabular")
                        {
                            char[] trm = { '|' };
                            colData = colData.Trim(trm).Replace("~", "-").Replace("|", "\r\n");
                            ght = (float)gfx0.MeasureString(colData, xfont41).Height;
                            if (ght < 8)
                            {
                                ght = 8;
                            }

                            tf = new XTextFormatter(gfx0);
                            rect = new XRect(startX + 105, startY + offsetY, 130, ght);
                            gfx0.DrawRectangle(XBrushes.White, rect);

                            tf.DrawString(colData
                              , xfont41, XBrushes.Black, rect, XStringFormats.TopLeft);
                        }
                        else
                        {
                            nwLn = Global.mnFrm.cmCde.breakPDFTxtDown(
                              colData,
                              150, font41, g);
                            ght = (float)(nwLn.Length * font4Hght);//gfx0.MeasureString("  "+string.Join("\n", nwLn), xfont41).Height;
                            if (ght < 8)
                            {
                                ght = 8;
                            }

                            tf = new XTextFormatter(gfx0);
                            rect = new XRect(startX + 105, startY + offsetY, 130, ght);
                            gfx0.DrawRectangle(XBrushes.White, rect);

                            tf.DrawString(string.Join("\n", nwLn)
                              , xfont41, XBrushes.Black, rect, XStringFormats.TopLeft);
                        }
                        if (ght < lblght)
                        {
                            ght = lblght;
                        }
                        if (hghstght < ght)
                        {
                            hghstght = ght;
                        }

                        if (hghstght < 10)
                        {
                            hghstght = 10;
                        }

                        if ((j % 2) == 1)
                        {
                            offsetY += hghstght + 5;
                            if (hgstOffsetY < offsetY)
                            {
                                hgstOffsetY = offsetY;
                            }
                            else
                            {
                                offsetY = hgstOffsetY;
                            }
                            startX = 280;
                        }
                        if ((startY + hgstOffsetY) >= 700)
                        {
                            page0 = document.AddPage();
                            page0.Orientation = PageOrientation.Portrait;
                            page0.Height = XUnit.FromInch(11);
                            page0.Width = XUnit.FromInch(8.5);
                            gfx0 = XGraphics.FromPdfPage(page0);
                            offsetY = 0;
                            hgstOffsetY = 0;
                        }
                    }
                    if ((j % 2) == 1)
                    {
                        offsetY += hghstght + 5;
                        if (hgstOffsetY < offsetY)
                        {
                            hgstOffsetY = offsetY;
                        }
                        else
                        {
                            offsetY = hgstOffsetY;
                        }
                        //startX = 280;
                    }
                    if ((startY + hgstOffsetY) >= 700)
                    {
                        page0 = document.AddPage();
                        page0.Orientation = PageOrientation.Portrait;
                        page0.Height = XUnit.FromInch(11);
                        page0.Width = XUnit.FromInch(8.5);
                        gfx0 = XGraphics.FromPdfPage(page0);
                        offsetY = 0;
                        hgstOffsetY = 0;
                    }
                }

                //Slogan: 
                startX = 40;
                offsetY = 705;
                gfx0.DrawLine(aPen, startX, startY + offsetY, startX + 510,
          startY + offsetY);
                offsetY += font3Hght;
                nwLn = Global.mnFrm.cmCde.breakRptTxtDown(
                  Global.mnFrm.cmCde.getOrgName(Global.mnFrm.cmCde.Org_id) + "..." +
                  Global.mnFrm.cmCde.getOrgSlogan(Global.mnFrm.cmCde.Org_id),
          pageWidth - ght, font5, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    gfx0.DrawString(nwLn[i]
                    , xfont5, XBrushes.Black, startX, startY + offsetY);
                    offsetY += font5Hght;
                }
                offsetY += font5Hght;
                nwLn = Global.mnFrm.cmCde.breakRptTxtDown(
                 "Software Developed by Rhomicom Systems Technologies Ltd.",
          pageWidth + 40, font5, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    gfx0.DrawString(nwLn[i]
                    , xfont5, XBrushes.Black, startX, startY + offsetY);
                    offsetY += font5Hght;
                }
                nwLn = Global.mnFrm.cmCde.breakRptTxtDown(
          "Website:www.rhomicomgh.com",
          pageWidth + 40, font5, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    gfx0.DrawString(nwLn[i]
                    , xfont5, XBrushes.Black, startX, startY + offsetY);
                    offsetY += font5Hght;
                }
                // Create second page for additional person details
                /*PdfPage page1 = document.AddPage();
                XGraphics gfx1 = XGraphics.FromPdfPage(page1);
                XFont xfont1 = new XFont("Verdana", 20, XFontStyle.BoldItalic);
                gfx0.DrawString("Page 2!" + this.locIDTextBox.Text, xfont1, XBrushes.Black,
                  new XRect(100, 100, page1.Width, page1.Height),
                  XStringFormats.TopLeft);*/



                // Save the document...
                string filename = Global.mnFrm.cmCde.getRptDrctry() + @"\PersonDetRpt_" + this.prsnIDTextBox.Text + ".pdf";
                document.Save(filename);
                // ...and start a viewer.
                System.Diagnostics.Process.Start(filename);
                this.pdfRptButton.Enabled = true;
                //Global.mnFrm.cmCde.upldImgsFTP(9, Global.mnFrm.cmCde.getRptDrctry(), @"\PersonDetRpt_" + this.locIDTextBox.Text + ".pdf");
                System.Windows.Forms.Application.DoEvents();
            }
            catch (Exception ex)
            {
                this.pdfRptButton.Enabled = true;
                System.Windows.Forms.Application.DoEvents();
                Global.mnFrm.cmCde.showMsg(ex.Message + "\r\n\r\n" + ex.InnerException + "\r\n\r\n" + ex.StackTrace, 0);
            }
        }

        private void prsNamesTxtbx_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();

            if (e.Control && e.KeyCode == Keys.S)
            {
                if (this.savePrsButton.Enabled == true)
                {
                    this.savePrsButton_Click(this.savePrsButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                if (this.addPrsButton.Enabled == true)
                {
                    this.addPrsButton_Click(this.addPrsButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                if (this.editPrsButton.Enabled == true)
                {
                    this.editPrsButton_Click(this.editPrsButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                this.resetButton.PerformClick();
            }
            else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)
            {
                if (this.goPrsButton.Enabled == true)
                {
                    this.goPrsButton_Click(this.goPrsButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                e.Handled = false;
                e.SuppressKeyPress = false;
            }
        }

        private void prsNamesListView_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();

            if (e.Control && e.KeyCode == Keys.S)
            {
                if (this.savePrsButton.Enabled == true)
                {
                    this.savePrsButton_Click(this.savePrsButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                if (this.addPrsButton.Enabled == true)
                {
                    this.addPrsButton_Click(this.addPrsButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                if (this.editPrsButton.Enabled == true)
                {
                    this.editPrsButton_Click(this.editPrsButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                this.resetButton.PerformClick();
            }
            else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)
            {
                if (this.goPrsButton.Enabled == true)
                {
                    this.goPrsButton_Click(this.goPrsButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.Delete)
            {
                if (this.deletePrsButton.Enabled == true)
                {
                    this.deletePrsButton_Click(this.deletePrsButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                Global.mnFrm.cmCde.listViewKeyDown(this.prsNamesListView, e);
            }
        }

        private void nationalityListView_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();

            if (e.Control && e.KeyCode == Keys.S)
            {
                if (this.savePrsButton.Enabled == true)
                {
                    this.savePrsButton_Click(this.savePrsButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                if (this.addNtnltyButton.Enabled == true)
                {
                    this.addNtnltyButton_Click(this.addNtnltyButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                if (this.editNtnltyButton.Enabled == true)
                {
                    this.editNtnltyButton_Click(this.editNtnltyButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                this.resetButton.PerformClick();
            }
            else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)
            {
                if (this.goPrsButton.Enabled == true)
                {
                    this.goPrsButton_Click(this.goPrsButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                Global.mnFrm.cmCde.listViewKeyDown(this.nationalityListView, e);
            }
        }

        private void mainForm_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();

            if (e.Control && e.KeyCode == Keys.S)
            {
                if (this.savePrsButton.Enabled == true)
                {
                    this.savePrsButton_Click(this.savePrsButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                if (this.addPrsButton.Enabled == true)
                {
                    this.addPrsButton_Click(this.addPrsButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                if (this.editPrsButton.Enabled == true)
                {
                    this.editPrsButton_Click(this.editPrsButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                this.resetButton.PerformClick();
            }
            else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)
            {
                if (this.goPrsButton.Enabled == true)
                {
                    this.goPrsButton_Click(this.goPrsButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.Delete)
            {
                if (this.deletePrsButton.Enabled == true)
                {
                    this.deletePrsButton_Click(this.deletePrsButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                e.Handled = false;
                e.SuppressKeyPress = false;
                if (this.prsNamesListView.Focused)
                {
                    Global.mnFrm.cmCde.listViewKeyDown(this.prsNamesListView, e);
                }
            }
        }

        private void exprtPrsnExtrDataTmp(int exprtTyp)
        {
            System.Windows.Forms.Application.DoEvents();
            Global.mnFrm.cmCde.clearPrvExclFiles();
            Global.mnFrm.cmCde.exclApp = new Microsoft.Office.Interop.Excel.Application();
            Global.mnFrm.cmCde.exclApp.WindowState = Excel.XlWindowState.xlNormal;
            Global.mnFrm.cmCde.exclApp.Visible = true;
            CommonCode.CommonCodes.SetWindowPos((IntPtr)Global.mnFrm.cmCde.exclApp.Hwnd, CommonCode.CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCode.CommonCodes.SWP_NOMOVE | CommonCode.CommonCodes.SWP_NOSIZE | CommonCode.CommonCodes.SWP_SHOWWINDOW);

            Global.mnFrm.cmCde.nwWrkBk = Global.mnFrm.cmCde.exclApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
            Global.mnFrm.cmCde.nwWrkBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
            Global.mnFrm.cmCde.trgtSheets = new Excel.Worksheet[1];

            Global.mnFrm.cmCde.trgtSheets[0] = (Excel.Worksheet)Global.mnFrm.cmCde.nwWrkBk.Worksheets[1];

            Global.mnFrm.cmCde.trgtSheets[0].get_Range("B2:C3", Type.Missing).MergeCells = true;
            Global.mnFrm.cmCde.trgtSheets[0].get_Range("B2:C3", Type.Missing).Value2 = Global.mnFrm.cmCde.getOrgName(Global.mnFrm.cmCde.Org_id).ToUpper();
            Global.mnFrm.cmCde.trgtSheets[0].get_Range("B2:C3", Type.Missing).Font.Bold = true;
            Global.mnFrm.cmCde.trgtSheets[0].get_Range("B2:C3", Type.Missing).Font.Size = 13;
            Global.mnFrm.cmCde.trgtSheets[0].get_Range("B2:C3", Type.Missing).WrapText = true;
            Global.mnFrm.cmCde.trgtSheets[0].Shapes.AddPicture(Global.mnFrm.cmCde.getOrgImgsDrctry() + @"\" + Global.mnFrm.cmCde.Org_id + ".png",
                Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1, 50, 50);

            ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, 1]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
            ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
            ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, 1]).Font.Bold = true;
            ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, 1]).Value2 = "No.";
            DataSet colsDtSt = Global.get_PrsExtrDataCols(Global.mnFrm.cmCde.Org_id);
            string[] hdngs = new string[52];
            string[] hdngs1 = { "Person's ID No.**", "Full Name" };
            int fldcnt = colsDtSt.Tables[0].Rows.Count;
            for (int a = 0; a < hdngs.Length; a++)
            {
                if (a < 2)
                {
                    hdngs[a] = hdngs1[a];
                }
                else if (a < (fldcnt + 2))
                {
                    if (colsDtSt.Tables[0].Rows[a - 2][1].ToString() == (a - 1).ToString())
                    {
                        hdngs[a] = "'" + colsDtSt.Tables[0].Rows[a - 2][1].ToString() + ". " + colsDtSt.Tables[0].Rows[a - 2][2].ToString();
                    }
                    else
                    {
                        hdngs[a] = "'" + (a - 1).ToString() + ". ";
                    }
                }
                else
                {
                    hdngs[a] = "'" + (a - 1).ToString() + ". ";
                }
              ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Font.Bold = true;
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Value2 = hdngs[a].ToUpper();
            }
            if (exprtTyp == 2)
            {
                DataSet dtst = Global.get_AllPrsExtrData(Global.mnFrm.cmCde.Org_id, 10000000);
                for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
                {
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 2]).Value2 = dtst.Tables[0].Rows[a][1].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dtst.Tables[0].Rows[a][2].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dtst.Tables[0].Rows[a][3].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 5]).Value2 = dtst.Tables[0].Rows[a][4].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 6]).Value2 = dtst.Tables[0].Rows[a][5].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 7]).Value2 = dtst.Tables[0].Rows[a][6].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 8]).Value2 = dtst.Tables[0].Rows[a][7].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 9]).Value2 = dtst.Tables[0].Rows[a][8].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 10]).Value2 = dtst.Tables[0].Rows[a][9].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 11]).Value2 = dtst.Tables[0].Rows[a][10].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 12]).Value2 = dtst.Tables[0].Rows[a][11].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 13]).Value2 = dtst.Tables[0].Rows[a][12].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 14]).Value2 = dtst.Tables[0].Rows[a][13].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 15]).Value2 = dtst.Tables[0].Rows[a][14].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 16]).Value2 = dtst.Tables[0].Rows[a][15].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 17]).Value2 = dtst.Tables[0].Rows[a][16].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 18]).Value2 = dtst.Tables[0].Rows[a][17].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 19]).Value2 = dtst.Tables[0].Rows[a][18].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 20]).Value2 = dtst.Tables[0].Rows[a][19].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 21]).Value2 = dtst.Tables[0].Rows[a][20].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 22]).Value2 = dtst.Tables[0].Rows[a][21].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 23]).Value2 = dtst.Tables[0].Rows[a][22].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 24]).Value2 = dtst.Tables[0].Rows[a][23].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 25]).Value2 = dtst.Tables[0].Rows[a][24].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 26]).Value2 = dtst.Tables[0].Rows[a][25].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 27]).Value2 = dtst.Tables[0].Rows[a][26].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 28]).Value2 = dtst.Tables[0].Rows[a][27].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 29]).Value2 = dtst.Tables[0].Rows[a][28].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 30]).Value2 = dtst.Tables[0].Rows[a][29].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 31]).Value2 = dtst.Tables[0].Rows[a][30].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 32]).Value2 = dtst.Tables[0].Rows[a][31].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 33]).Value2 = dtst.Tables[0].Rows[a][32].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 34]).Value2 = dtst.Tables[0].Rows[a][33].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 35]).Value2 = dtst.Tables[0].Rows[a][34].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 36]).Value2 = dtst.Tables[0].Rows[a][35].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 37]).Value2 = dtst.Tables[0].Rows[a][36].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 38]).Value2 = dtst.Tables[0].Rows[a][37].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 39]).Value2 = dtst.Tables[0].Rows[a][38].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 40]).Value2 = dtst.Tables[0].Rows[a][39].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 41]).Value2 = dtst.Tables[0].Rows[a][40].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 42]).Value2 = dtst.Tables[0].Rows[a][41].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 43]).Value2 = dtst.Tables[0].Rows[a][42].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 44]).Value2 = dtst.Tables[0].Rows[a][43].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 45]).Value2 = dtst.Tables[0].Rows[a][44].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 46]).Value2 = dtst.Tables[0].Rows[a][45].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 47]).Value2 = dtst.Tables[0].Rows[a][46].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 48]).Value2 = dtst.Tables[0].Rows[a][47].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 49]).Value2 = dtst.Tables[0].Rows[a][48].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 50]).Value2 = dtst.Tables[0].Rows[a][49].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 51]).Value2 = dtst.Tables[0].Rows[a][50].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 52]).Value2 = dtst.Tables[0].Rows[a][51].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 53]).Value2 = dtst.Tables[0].Rows[a][52].ToString();
                }
            }
            else if (exprtTyp >= 3)
            {
                DataSet dtst = Global.get_AllPrsExtrData(Global.mnFrm.cmCde.Org_id, exprtTyp);
                for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
                {
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 2]).Value2 = dtst.Tables[0].Rows[a][1].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dtst.Tables[0].Rows[a][2].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dtst.Tables[0].Rows[a][3].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 5]).Value2 = dtst.Tables[0].Rows[a][4].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 6]).Value2 = dtst.Tables[0].Rows[a][5].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 7]).Value2 = dtst.Tables[0].Rows[a][6].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 8]).Value2 = dtst.Tables[0].Rows[a][7].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 9]).Value2 = dtst.Tables[0].Rows[a][8].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 10]).Value2 = dtst.Tables[0].Rows[a][9].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 11]).Value2 = dtst.Tables[0].Rows[a][10].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 12]).Value2 = dtst.Tables[0].Rows[a][11].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 13]).Value2 = dtst.Tables[0].Rows[a][12].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 14]).Value2 = dtst.Tables[0].Rows[a][13].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 15]).Value2 = dtst.Tables[0].Rows[a][14].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 16]).Value2 = dtst.Tables[0].Rows[a][15].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 17]).Value2 = dtst.Tables[0].Rows[a][16].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 18]).Value2 = dtst.Tables[0].Rows[a][17].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 19]).Value2 = dtst.Tables[0].Rows[a][18].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 20]).Value2 = dtst.Tables[0].Rows[a][19].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 21]).Value2 = dtst.Tables[0].Rows[a][20].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 22]).Value2 = dtst.Tables[0].Rows[a][21].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 23]).Value2 = dtst.Tables[0].Rows[a][22].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 24]).Value2 = dtst.Tables[0].Rows[a][23].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 25]).Value2 = dtst.Tables[0].Rows[a][24].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 26]).Value2 = dtst.Tables[0].Rows[a][25].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 27]).Value2 = dtst.Tables[0].Rows[a][26].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 28]).Value2 = dtst.Tables[0].Rows[a][27].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 29]).Value2 = dtst.Tables[0].Rows[a][28].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 30]).Value2 = dtst.Tables[0].Rows[a][29].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 31]).Value2 = dtst.Tables[0].Rows[a][30].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 32]).Value2 = dtst.Tables[0].Rows[a][31].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 33]).Value2 = dtst.Tables[0].Rows[a][32].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 34]).Value2 = dtst.Tables[0].Rows[a][33].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 35]).Value2 = dtst.Tables[0].Rows[a][34].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 36]).Value2 = dtst.Tables[0].Rows[a][35].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 37]).Value2 = dtst.Tables[0].Rows[a][36].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 38]).Value2 = dtst.Tables[0].Rows[a][37].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 39]).Value2 = dtst.Tables[0].Rows[a][38].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 40]).Value2 = dtst.Tables[0].Rows[a][39].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 41]).Value2 = dtst.Tables[0].Rows[a][40].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 42]).Value2 = dtst.Tables[0].Rows[a][41].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 43]).Value2 = dtst.Tables[0].Rows[a][42].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 44]).Value2 = dtst.Tables[0].Rows[a][43].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 45]).Value2 = dtst.Tables[0].Rows[a][44].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 46]).Value2 = dtst.Tables[0].Rows[a][45].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 47]).Value2 = dtst.Tables[0].Rows[a][46].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 48]).Value2 = dtst.Tables[0].Rows[a][47].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 49]).Value2 = dtst.Tables[0].Rows[a][48].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 50]).Value2 = dtst.Tables[0].Rows[a][49].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 51]).Value2 = dtst.Tables[0].Rows[a][50].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 52]).Value2 = dtst.Tables[0].Rows[a][51].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 53]).Value2 = dtst.Tables[0].Rows[a][52].ToString();
                }
            }
            else
            {
            }

            Global.mnFrm.cmCde.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 10;
            Global.mnFrm.cmCde.trgtSheets[0].get_Range("A1:A65535", Type.Missing).WrapText = true;

            Global.mnFrm.cmCde.trgtSheets[0].get_Range("B1:BZ65535", Type.Missing).Columns.AutoFit();
            Global.mnFrm.cmCde.trgtSheets[0].get_Range("B1:BZ65535", Type.Missing).Rows.AutoFit();
        }

        private void exptAdtnlDataTmpButton_Click(object sender, EventArgs e)
        {
            string rspnse = Interaction.InputBox("How many Additional Data Records will you like to Export?" +
              "\r\n1=No Additional Data Records(Empty Template)" +
              "\r\n2=All Additional Data Records" +
              "\r\n3-Infinity=Specify the exact number of Additional Data Records to Export\r\n",
              "Rhomicom", "1", (Global.mnFrm.cmCde.myComputer.Screen.Bounds.Width / 2) - 170,
              (Global.mnFrm.cmCde.myComputer.Screen.Bounds.Height / 2) - 100);
            if (rspnse.Equals(string.Empty) || rspnse.Equals(null))
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            int rsponse = 0;
            bool rsps = int.TryParse(rspnse, out rsponse);
            if (rsps == false)
            {
                Global.mnFrm.cmCde.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            if (rsponse < 1)
            {
                Global.mnFrm.cmCde.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
                return;
            }
            this.exprtPrsnExtrDataTmp(rsponse);
        }

        private void imprtPrsnExtrDataTmp(string filename)
        {
            System.Windows.Forms.Application.DoEvents();
            Global.mnFrm.cmCde.clearPrvExclFiles();
            Global.mnFrm.cmCde.exclApp = new Microsoft.Office.Interop.Excel.Application();
            Global.mnFrm.cmCde.exclApp.WindowState = Excel.XlWindowState.xlNormal;
            Global.mnFrm.cmCde.exclApp.Visible = true;
            CommonCode.CommonCodes.SetWindowPos((IntPtr)Global.mnFrm.cmCde.exclApp.Hwnd, CommonCode.CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCode.CommonCodes.SWP_NOMOVE | CommonCode.CommonCodes.SWP_NOSIZE | CommonCode.CommonCodes.SWP_SHOWWINDOW);

            Global.mnFrm.cmCde.nwWrkBk = Global.mnFrm.cmCde.exclApp.Workbooks.Open(filename, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

            Global.mnFrm.cmCde.trgtSheets = new Excel.Worksheet[1];

            Global.mnFrm.cmCde.trgtSheets[0] = (Excel.Worksheet)Global.mnFrm.cmCde.nwWrkBk.Worksheets[1];

            DataSet colsDtSt = Global.get_PrsExtrDataCols(Global.mnFrm.cmCde.Org_id);
            string[] hdngs = new string[52];
            string[] hdngs1 = { "Person's ID No.**", "Full Name" };
            int fldcnt = colsDtSt.Tables[0].Rows.Count;
            for (int a = 0; a < hdngs.Length; a++)
            {
                if (a < 2)
                {
                    hdngs[a] = hdngs1[a];
                }
                else if (a < (fldcnt + 2))
                {
                    if (colsDtSt.Tables[0].Rows[a - 2][1].ToString() == (a - 1).ToString())
                    {
                        hdngs[a] = colsDtSt.Tables[0].Rows[a - 2][1].ToString() + ". " + colsDtSt.Tables[0].Rows[a - 2][2].ToString();
                    }
                    else
                    {
                        hdngs[a] = (a - 1).ToString() + ". ";
                    }
                }
                else
                {
                    hdngs[a] = (a - 1).ToString() + ". ";
                }
            }

            string prsnLocIDNo = "";
            string dataCol1 = "";
            string dataCol2 = "";
            string dataCol3 = "";
            string dataCol4 = "";
            string dataCol5 = "";
            string dataCol6 = "";
            string dataCol7 = "";
            string dataCol8 = "";
            string dataCol9 = "";
            string dataCol10 = "";
            string dataCol11 = "";
            string dataCol12 = "";
            string dataCol13 = "";
            string dataCol14 = "";
            string dataCol15 = "";
            string dataCol16 = "";
            string dataCol17 = "";
            string dataCol18 = "";
            string dataCol19 = "";
            string dataCol20 = "";
            string dataCol21 = "";
            string dataCol22 = "";
            string dataCol23 = "";
            string dataCol24 = "";
            string dataCol25 = "";
            string dataCol26 = "";
            string dataCol27 = "";
            string dataCol28 = "";
            string dataCol29 = "";
            string dataCol30 = "";
            string dataCol31 = "";
            string dataCol32 = "";
            string dataCol33 = "";
            string dataCol34 = "";
            string dataCol35 = "";
            string dataCol36 = "";
            string dataCol37 = "";
            string dataCol38 = "";
            string dataCol39 = "";
            string dataCol40 = "";
            string dataCol41 = "";
            string dataCol42 = "";
            string dataCol43 = "";
            string dataCol44 = "";
            string dataCol45 = "";
            string dataCol46 = "";
            string dataCol47 = "";
            string dataCol48 = "";
            string dataCol49 = "";
            string dataCol50 = "";
            int rownum = 5;
            do
            {
                try
                {
                    prsnLocIDNo = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 2]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    prsnLocIDNo = "";
                }
                try
                {
                    dataCol1 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 4]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol1 = "";
                }
                try
                {
                    dataCol2 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 5]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol2 = "";
                }
                try
                {
                    dataCol3 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 6]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol3 = "";
                }
                try
                {
                    dataCol4 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 7]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol4 = "";
                }
                try
                {
                    dataCol5 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 8]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol5 = "";
                }
                try
                {
                    dataCol6 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 9]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol6 = "";
                }
                try
                {
                    dataCol7 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 10]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol7 = "";
                }
                try
                {
                    dataCol8 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 11]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol8 = "";
                }
                try
                {
                    dataCol9 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 12]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol9 = "";
                }
                try
                {
                    dataCol10 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 13]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol10 = "";
                }
                try
                {
                    dataCol11 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 14]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol11 = "";
                }
                try
                {
                    dataCol12 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 15]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol12 = "";
                }
                try
                {
                    dataCol13 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 16]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol13 = "";
                }
                try
                {
                    dataCol14 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 17]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol14 = "";
                }
                try
                {
                    dataCol15 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 18]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol15 = "";
                }
                try
                {
                    dataCol16 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 19]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol16 = "";
                }
                try
                {
                    dataCol17 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 20]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol17 = "";
                }
                try
                {
                    dataCol18 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 21]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol18 = "";
                }
                try
                {
                    dataCol19 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 22]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol19 = "";
                }
                try
                {
                    dataCol20 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 23]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol20 = "";
                }
                try
                {
                    dataCol21 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 24]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol21 = "";
                }
                try
                {
                    dataCol22 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 25]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol22 = "";
                }
                try
                {
                    dataCol23 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 26]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol23 = "";
                }
                try
                {
                    dataCol24 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 27]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol24 = "";
                }
                try
                {
                    dataCol25 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 28]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol25 = "";
                }
                try
                {
                    dataCol26 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 29]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol26 = "";
                }
                try
                {
                    dataCol27 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 30]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol27 = "";
                }
                try
                {
                    dataCol28 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 31]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol28 = "";
                }
                try
                {
                    dataCol29 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 32]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol29 = "";
                }
                try
                {
                    dataCol30 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 33]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol30 = "";
                }
                try
                {
                    dataCol31 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 34]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol31 = "";
                }
                try
                {
                    dataCol32 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 35]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol32 = "";
                }
                try
                {
                    dataCol33 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 36]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol33 = "";
                }
                try
                {
                    dataCol34 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 37]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol34 = "";
                }
                try
                {
                    dataCol35 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 38]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol35 = "";
                }
                try
                {
                    dataCol36 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 39]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol36 = "";
                }
                try
                {
                    dataCol37 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 40]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol37 = "";
                }
                try
                {
                    dataCol38 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 41]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol38 = "";
                }
                try
                {
                    dataCol39 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 42]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol39 = "";
                }
                try
                {
                    dataCol40 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 43]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol40 = "";
                }
                try
                {
                    dataCol41 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 44]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol41 = "";
                }
                try
                {
                    dataCol42 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 45]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol42 = "";
                }
                try
                {
                    dataCol43 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 46]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol43 = "";
                }
                try
                {
                    dataCol44 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 47]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol44 = "";
                }
                try
                {
                    dataCol45 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 48]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol45 = "";
                }
                try
                {
                    dataCol46 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 49]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol46 = "";
                }
                try
                {
                    dataCol47 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 50]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol47 = "";
                }
                try
                {
                    dataCol48 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 51]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol48 = "";
                }
                try
                {
                    dataCol49 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 52]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol49 = "";
                }
                try
                {
                    dataCol50 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 53]).Text.ToString();
                }
                catch (Exception ex)
                {
                    dataCol50 = "";
                }

                if (rownum == 5)
                {
                    if (prsnLocIDNo != hdngs[0].ToUpper()
                      || dataCol1 != hdngs[2].ToUpper()
                      || dataCol2 != hdngs[3].ToUpper()
                      || dataCol3 != hdngs[4].ToUpper()
                      || dataCol4 != hdngs[5].ToUpper()
                      || dataCol5 != hdngs[6].ToUpper()
                      || dataCol6 != hdngs[7].ToUpper()
                      || dataCol7 != hdngs[8].ToUpper()
                      || dataCol8 != hdngs[9].ToUpper()
                      || dataCol9 != hdngs[10].ToUpper()
                      || dataCol10 != hdngs[11].ToUpper() || dataCol11 != hdngs[12].ToUpper()
                      || dataCol12 != hdngs[13].ToUpper()
                      || dataCol13 != hdngs[14].ToUpper()
                      || dataCol14 != hdngs[15].ToUpper()
                      || dataCol15 != hdngs[16].ToUpper()
                      || dataCol16 != hdngs[17].ToUpper()
                      || dataCol17 != hdngs[18].ToUpper()
                      || dataCol18 != hdngs[19].ToUpper()
                      || dataCol19 != hdngs[20].ToUpper()
                      || dataCol20 != hdngs[21].ToUpper() || dataCol21 != hdngs[22].ToUpper()
                      || dataCol22 != hdngs[23].ToUpper()
                      || dataCol23 != hdngs[24].ToUpper()
                      || dataCol24 != hdngs[25].ToUpper()
                      || dataCol25 != hdngs[26].ToUpper()
                      || dataCol26 != hdngs[27].ToUpper()
                      || dataCol27 != hdngs[28].ToUpper()
                      || dataCol28 != hdngs[29].ToUpper()
                      || dataCol29 != hdngs[30].ToUpper()
                      || dataCol30 != hdngs[31].ToUpper() || dataCol31 != hdngs[32].ToUpper()
                      || dataCol32 != hdngs[33].ToUpper()
                      || dataCol33 != hdngs[34].ToUpper()
                      || dataCol34 != hdngs[35].ToUpper()
                      || dataCol35 != hdngs[36].ToUpper()
                      || dataCol36 != hdngs[37].ToUpper()
                      || dataCol37 != hdngs[38].ToUpper()
                      || dataCol38 != hdngs[39].ToUpper()
                      || dataCol39 != hdngs[40].ToUpper()
                      || dataCol40 != hdngs[41].ToUpper() || dataCol41 != hdngs[42].ToUpper()
                      || dataCol42 != hdngs[43].ToUpper()
                      || dataCol43 != hdngs[44].ToUpper()
                      || dataCol44 != hdngs[45].ToUpper()
                      || dataCol45 != hdngs[46].ToUpper()
                      || dataCol46 != hdngs[47].ToUpper()
                      || dataCol47 != hdngs[48].ToUpper()
                      || dataCol48 != hdngs[49].ToUpper()
                      || dataCol49 != hdngs[50].ToUpper()
                      || dataCol50 != hdngs[51].ToUpper())
                    {
                        Global.mnFrm.cmCde.showMsg("The Excel File you Selected is not a Valid Template\r\nfor importing records here.", 0);
                        return;
                    }
                    rownum++;
                    continue;
                }
                if (prsnLocIDNo != "")
                {
                    long prsnID = Global.mnFrm.cmCde.getPrsnID(prsnLocIDNo);
                    long extrDataID = -1;
                    long.TryParse(Global.mnFrm.cmCde.getGnrlRecNm("prs.prsn_extra_data", "person_id", "extra_data_id", prsnID), out extrDataID);
                    string[] exdta = new string[50];
                    exdta[0] = dataCol1;
                    exdta[1] = dataCol2;
                    exdta[2] = dataCol3;
                    exdta[3] = dataCol4;
                    exdta[4] = dataCol5;
                    exdta[5] = dataCol6;
                    exdta[6] = dataCol7;
                    exdta[7] = dataCol8;
                    exdta[8] = dataCol9;
                    exdta[9] = dataCol10;
                    exdta[10] = dataCol11;
                    exdta[11] = dataCol12;
                    exdta[12] = dataCol13;
                    exdta[13] = dataCol14;
                    exdta[14] = dataCol15;
                    exdta[15] = dataCol16;
                    exdta[16] = dataCol17;
                    exdta[17] = dataCol18;
                    exdta[18] = dataCol19;
                    exdta[19] = dataCol20;
                    exdta[20] = dataCol21;
                    exdta[21] = dataCol22;
                    exdta[22] = dataCol23;
                    exdta[23] = dataCol24;
                    exdta[24] = dataCol25;
                    exdta[25] = dataCol26;
                    exdta[26] = dataCol27;
                    exdta[27] = dataCol28;
                    exdta[28] = dataCol29;
                    exdta[29] = dataCol30;
                    exdta[30] = dataCol31;
                    exdta[31] = dataCol32;
                    exdta[32] = dataCol33;
                    exdta[33] = dataCol34;
                    exdta[34] = dataCol35;
                    exdta[35] = dataCol36;
                    exdta[36] = dataCol37;
                    exdta[37] = dataCol38;
                    exdta[38] = dataCol39;
                    exdta[39] = dataCol40;
                    exdta[40] = dataCol41;
                    exdta[41] = dataCol42;
                    exdta[42] = dataCol43;
                    exdta[43] = dataCol44;
                    exdta[44] = dataCol45;
                    exdta[45] = dataCol46;
                    exdta[46] = dataCol47;
                    exdta[47] = dataCol48;
                    exdta[48] = dataCol49;
                    exdta[49] = dataCol50;
                    if (extrDataID > 0 && prsnID > 0)
                    {
                        //Update
                        Global.updatePrsnExtrData(prsnID, exdta);
                        Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + rownum + ":BE" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen);
                    }
                    else if (prsnID > 0)
                    {
                        //Insert
                        Global.createPrsnExtrData(prsnID, exdta);
                        Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + rownum + ":BE" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 255, 0));
                    }
                    else
                    {
                        Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + rownum + ":BE" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 0, 0));
                        //this.trgtSheets[0].get_Range("M" + rownum + ":M" + rownum + "", Type.Missing).Value2 = errMsg;
                    }
                }
                rownum++;
            }
            while (prsnLocIDNo != "");
        }


        private void imptAdtnlDataTmpButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[8]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }

            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to Import Additional Person Data\r\n to Overwrite the existing Data shown here?", 1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }

            this.openFileDialog1.RestoreDirectory = true;
            this.openFileDialog1.Filter = "All Files|*.*|Excel Files|*.xls;*.xlsx";
            this.openFileDialog1.FilterIndex = 2;
            this.openFileDialog1.Title = "Select an Excel File to Upload...";
            this.openFileDialog1.FileName = "";
            if (this.openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.imprtPrsnExtrDataTmp(this.openFileDialog1.FileName);
            }
            this.loadPersExtDataPanel();
        }

        private void dobTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.dobTextBox.Text != "")
            {
                this.ageLabel.Text = Global.computePrsnAge(this.dobTextBox.Text);
            }
        }

        private void searchInPrsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.searchInPrsComboBox.SelectedIndex >= 0)
            {
                this.prs_cur_indx = 0;
                this.goPrsButton_Click(this.goPrsButton, e);
            }
        }

        private void orderByComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.orderByComboBox.SelectedIndex >= 0)
            {
                this.prs_cur_indx = 0;
                this.goPrsButton_Click(this.goPrsButton, e);
            }
        }

        private void titleTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!this.obey_prs_evnts)
            {
                return;
            }
            //TextBox mytxt = (TextBox)sender;
            //if (mytxt.Name == "vldEndDteTextBox"
            //  && mytxt.Text == "31-Dec-4000")
            //{
            //  this.vldEndDteTextBox.Text = "";
            //}
            this.txtChngd = true;
        }

        private void titleTextBox_Leave(object sender, EventArgs e)
        {
            if (this.txtChngd == false)
            {
                return;
            }
            if (this.editPrsButton.Text == "EDIT")
            {
                this.editPrsButton.PerformClick();
            }
            if (this.editPrsn == false && this.addPrsn == false)
            {
                Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
                return;
            }

            this.txtChngd = false;
            TextBox mytxt = (TextBox)sender;
            this.obey_prs_evnts = false;
            this.srchWrd = mytxt.Text;
            if (!mytxt.Text.Contains("%"))
            {
                this.srchWrd = "%" + this.srchWrd.Replace(" ", "%") + "%";
            }

            if (mytxt.Name == "titleTextBox")
            {
                this.titleTextBox.Text = "";
                this.titleButton_Click(this.titleButton, e);
            }
            else if (mytxt.Name == "genderTextBox")
            {
                this.genderTextBox.Text = "";
                this.genderButton_Click(this.genderButton, e);
            }
            else if (mytxt.Name == "maritalStatusTextBox")
            {
                this.maritalStatusTextBox.Text = "";
                this.maritalStatusButton_Click(this.maritalStatusButton, e);
            }
            else if (mytxt.Name == "prsOrgTextBox")
            {
                this.prsOrgTextBox.Text = "";
                this.prsOrgIDTextBox.Text = "-1";
                this.prsOrgButton_Click(this.prsOrgButton, e);
            }
            else if (mytxt.Name == "dobTextBox")
            {
                this.dobTextBox.Text = Global.mnFrm.cmCde.checkNFormatDate(this.dobTextBox.Text).Substring(0, 11);
                this.ageLabel.Text = Global.computePrsnAge(this.dobTextBox.Text);
            }
            else if (mytxt.Name == "ntnltyTextBox")
            {
                this.ntnltyTextBox.Text = "";
                this.ntnltyButton_Click(this.ntnltyButton, e);
            }
            else if (mytxt.Name == "prsnTypTextBox")
            {
                this.prsnTypTextBox.Text = "";
                this.prsnTypeButton_Click(this.prsnTypeButton, e);
            }
            else if (mytxt.Name == "reasonTextBox")
            {
                this.reasonTextBox.Text = "";
                this.reasonButton_Click(this.reasonButton, e);
            }
            else if (mytxt.Name == "vldStrtDteTextBox")
            {
                this.vldStrtDteTextBox.Text = Global.mnFrm.cmCde.checkNFormatDate(this.vldStrtDteTextBox.Text).Substring(0, 11);
                this.vldDte1 = this.vldStrtDteTextBox.Text;
            }
            else if (mytxt.Name == "vldEndDteTextBox")
            {
                this.vldEndDteTextBox.Text = Global.mnFrm.cmCde.checkNFormatDate(this.vldEndDteTextBox.Text).Substring(0, 11);
                this.vldDte2 = this.vldEndDteTextBox.Text;
            }
            this.srchWrd = "%";
            this.obey_prs_evnts = true;
            this.txtChngd = false;
        }

        private void prsnTypeButton_Click(object sender, EventArgs e)
        {
            if (this.editPrsButton.Text == "EDIT")
            {
                this.editPrsButton.PerformClick();
            }
            if (this.editPrsn == false && this.addPrsn == false)
            {
                Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
                return;
            }
            //Person Types
            int[] selVals = new int[1];
            selVals[0] = Global.mnFrm.cmCde.getPssblValID(this.prsnTypTextBox.Text,
              Global.mnFrm.cmCde.getLovID("Person Types"));
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
              Global.mnFrm.cmCde.getLovID("Person Types"), ref selVals, true, false,
             this.srchWrd, "Both", true);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    string nwVal = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
                    if (nwVal != this.prsnTypTextBox.Text)
                    {
                        this.reasonTextBox.Text = "";
                        this.vldDte1 = Global.mnFrm.cmCde.getFrmtdDB_Date_time().Substring(0, 11);
                        this.vldStrtDteTextBox.Text = this.vldDte1;
                    }
                    this.prsnTypTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
                }
            }
        }

        private void reasonButton_Click(object sender, EventArgs e)
        {
            if (this.editPrsButton.Text == "EDIT")
            {
                this.editPrsButton.PerformClick();
            }
            if (this.editPrsn == false && this.addPrsn == false)
            {
                Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
                return;
            }
            //Person Type Change Reasons
            int[] selVals = new int[1];
            selVals[0] = Global.mnFrm.cmCde.getPssblValID(this.reasonTextBox.Text,
              Global.mnFrm.cmCde.getLovID("Person Type Change Reasons"));
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
              Global.mnFrm.cmCde.getLovID("Person Type Change Reasons"), ref selVals, true, false,
             this.srchWrd, "Both", true);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.reasonTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
                    this.prsnTypeRsn = this.reasonTextBox.Text;
                }
            }
        }

        private void futhDetButton_Click(object sender, EventArgs e)
        {
            if (this.editPrsButton.Text == "EDIT")
            {
                this.editPrsButton.PerformClick();
            }
            if (this.editPrsn == false && this.addPrsn == false)
            {
                Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
                return;
            }
            //Person Types-Further Details
            int[] selVals = new int[1];
            selVals[0] = Global.mnFrm.cmCde.getPssblValID(this.furtherDetTextBox.Text,
              Global.mnFrm.cmCde.getLovID("Person Types-Further Details"));
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
              Global.mnFrm.cmCde.getLovID("Person Types-Further Details"), ref selVals, true, false,
             this.srchWrd, "Both", true);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.furtherDetTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
                    this.prsnTypeFurDet = this.furtherDetTextBox.Text;
                }
            }
        }

        private void dte1Button_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.selectDate(ref this.vldStrtDteTextBox);
            if (this.vldStrtDteTextBox.Text.Length > 11)
            {
                this.vldStrtDteTextBox.Text = this.vldStrtDteTextBox.Text.Substring(0, 11);
                this.vldDte1 = this.vldStrtDteTextBox.Text;
            }
        }

        private void dte2Button_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.selectDate(ref this.vldEndDteTextBox);
            if (this.vldEndDteTextBox.Text.Length > 11)
            {
                this.vldEndDteTextBox.Text = this.vldEndDteTextBox.Text.Substring(0, 11);
                this.vldDte2 = this.vldEndDteTextBox.Text;
            }
        }

        private void searchForPrsTextBox_Click(object sender, EventArgs e)
        {
            this.searchForPrsTextBox.SelectAll();
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.minimizeMemory();
            this.recreatePicBoxes();
            this.searchInComboBox.SelectedIndex = 0;
            this.searchInPrsComboBox.SelectedIndex = 5;
            this.orderByComboBox.SelectedIndex = 3;
            this.searchForTextBox.Text = "%";
            this.searchForPrsTextBox.Text = "%";
            this.prs_cur_indx = 0;

            this.dsplySizePrsComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            this.disablePrsEdit();
            this.disableExtrDataEdit();
            this.prs_cur_indx = 0;
            if (this.fltrByComboBox.SelectedIndex != 4)
            {
                this.fltrByComboBox.SelectedIndex = 4;
            }
            else if (this.prsnTypComboBox.Text != "All")
            {
                this.prsnTypComboBox.SelectedItem = "All";
            }
            else
            {
                this.goPrsButton_Click(this.goPrsButton, e);
            }
        }

        private void titleTextBox_Click(object sender, EventArgs e)
        {
            TextBox mytxt = (TextBox)sender;

            if (mytxt.Name == "titleTextBox")
            {
                this.titleTextBox.SelectAll();
            }
            else if (mytxt.Name == "genderTextBox")
            {
                this.genderTextBox.SelectAll();
            }
            else if (mytxt.Name == "maritalStatusTextBox")
            {
                this.maritalStatusTextBox.SelectAll();
            }
            else if (mytxt.Name == "prsOrgTextBox")
            {
                this.prsOrgTextBox.SelectAll();
            }
            else if (mytxt.Name == "dobTextBox")
            {
                this.dobTextBox.SelectAll();
            }
            else if (mytxt.Name == "ntnltyTextBox")
            {
                this.ntnltyTextBox.SelectAll();
            }
            else if (mytxt.Name == "prsnTypTextBox")
            {
                this.prsnTypTextBox.SelectAll();
            }
            else if (mytxt.Name == "reasonTextBox")
            {
                this.reasonTextBox.SelectAll();
            }
            else if (mytxt.Name == "vldStrtDteTextBox")
            {
                this.vldStrtDteTextBox.SelectAll();
            }
            else if (mytxt.Name == "vldEndDteTextBox")
            {
                this.vldEndDteTextBox.SelectAll();
            }
        }

        private void educDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e == null /*|| this.obeyEvnts == false*/ || this.saveEducButton.Enabled == false)
            {
                return;
            }
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            bool prv = this.obeyEvnts;
            this.obeyEvnts = false;

            if (this.educDataGridView.Rows[e.RowIndex].Cells[0].Value == null)
            {
                this.educDataGridView.Rows[e.RowIndex].Cells[0].Value = "";
            }
            if (this.educDataGridView.Rows[e.RowIndex].Cells[2].Value == null)
            {
                this.educDataGridView.Rows[e.RowIndex].Cells[2].Value = "";
            }
            if (this.educDataGridView.Rows[e.RowIndex].Cells[4].Value == null)
            {
                this.educDataGridView.Rows[e.RowIndex].Cells[4].Value = "";
            }
            if (this.educDataGridView.Rows[e.RowIndex].Cells[10].Value == null)
            {
                this.educDataGridView.Rows[e.RowIndex].Cells[10].Value = "";
            }
            if (this.educDataGridView.Rows[e.RowIndex].Cells[8].Value == null)
            {
                this.educDataGridView.Rows[e.RowIndex].Cells[8].Value = "";
            }

            if (e.ColumnIndex == 1)
            {
                int[] selVals = new int[1];
                selVals[0] = Global.mnFrm.cmCde.getPssblValID(
                  this.educDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString(),
                 Global.mnFrm.cmCde.getLovID("CV Courses"));
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                 Global.mnFrm.cmCde.getLovID("CV Courses"), ref selVals, true, false);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.educDataGridView.Rows[e.RowIndex].Cells[0].Value = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
                    }
                }
            }
            else if (e.ColumnIndex == 3)
            {
                int[] selVals = new int[1];
                selVals[0] = Global.mnFrm.cmCde.getPssblValID(this.educDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString(),
                 Global.mnFrm.cmCde.getLovID("Schools/Organisations/Institutions"));
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                 Global.mnFrm.cmCde.getLovID("Schools/Organisations/Institutions"), ref selVals, true, false);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.educDataGridView.Rows[e.RowIndex].Cells[2].Value = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
                    }
                }
            }
            else if (e.ColumnIndex == 5)
            {
                int[] selVals = new int[1];
                selVals[0] = Global.mnFrm.cmCde.getPssblValID(this.educDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString(),
                 Global.mnFrm.cmCde.getLovID("Other Locations"));
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                 Global.mnFrm.cmCde.getLovID("Other Locations"), ref selVals, true, false);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.educDataGridView.Rows[e.RowIndex].Cells[4].Value = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
                    }
                }
            }
            else if (e.ColumnIndex == 9)
            {
                int[] selVals = new int[1];
                selVals[0] = Global.mnFrm.cmCde.getPssblValID(
                  this.educDataGridView.Rows[e.RowIndex].Cells[8].Value.ToString(),
                 Global.mnFrm.cmCde.getLovID("Certificate Names"));
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                 Global.mnFrm.cmCde.getLovID("Certificate Names"), ref selVals, true, false);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.educDataGridView.Rows[e.RowIndex].Cells[8].Value = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
                    }
                }
            }
            else if (e.ColumnIndex == 11)
            {
                //Person Type Change Reasons
                this.GridCertTypTextBox.Text = this.educDataGridView.Rows[e.RowIndex].Cells[10].Value.ToString();
                int[] selVals = new int[1];
                selVals[0] = Global.mnFrm.cmCde.getPssblValID(this.GridCertTypTextBox.Text,
                 Global.mnFrm.cmCde.getLovID("Qualification Types"));
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                 Global.mnFrm.cmCde.getLovID("Qualification Types"), ref selVals, true, false);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.GridCertTypTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
                    }
                }
                this.educDataGridView.Rows[e.RowIndex].Cells[10].Value = this.GridCertTypTextBox.Text;
            }
            this.educDataGridView.EndEdit();
            System.Windows.Forms.Application.DoEvents();


            this.obeyEvnts = prv;
        }

        private void sitesDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e == null || this.obeyEvnts == false)
            {
                return;
            }
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            bool prv = this.obeyEvnts;
            this.obeyEvnts = false;


            if (this.sitesDataGridView.Rows[e.RowIndex].Cells[2].Value == null)
            {
                this.sitesDataGridView.Rows[e.RowIndex].Cells[2].Value = "";
            }

            if (this.sitesDataGridView.Rows[e.RowIndex].Cells[3].Value == null)
            {
                this.sitesDataGridView.Rows[e.RowIndex].Cells[3].Value = "";
            }
            if (e.ColumnIndex == 2
              || e.ColumnIndex == 3)
            {
                this.sitesDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
                string dtetmin = this.sitesDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
                string dtetmout = this.sitesDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
                if (e.ColumnIndex == 2 && dtetmin != "")
                {
                    dtetmin = Global.mnFrm.cmCde.checkNFormatDate(dtetmin).Substring(0, 11);
                    this.sitesDataGridView.Rows[e.RowIndex].Cells[2].Value = dtetmin;
                    this.sitesDataGridView.EndEdit();
                    System.Windows.Forms.Application.DoEvents();
                }
                if (e.ColumnIndex == 3 && dtetmout != "")
                {
                    dtetmout = Global.mnFrm.cmCde.checkNFormatDate(dtetmout).Substring(0, 11);
                    this.sitesDataGridView.Rows[e.RowIndex].Cells[3].Value = dtetmout;
                    this.sitesDataGridView.EndEdit();
                    System.Windows.Forms.Application.DoEvents();
                }
            }
            this.obeyEvnts = prv;

        }

        private void sprvisrDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e == null || this.obeyEvnts == false)
            {
                return;
            }
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            bool prv = this.obeyEvnts;
            this.obeyEvnts = false;


            if (this.sprvisrDataGridView.Rows[e.RowIndex].Cells[2].Value == null)
            {
                this.sprvisrDataGridView.Rows[e.RowIndex].Cells[2].Value = "";
            }

            if (this.sprvisrDataGridView.Rows[e.RowIndex].Cells[3].Value == null)
            {
                this.sprvisrDataGridView.Rows[e.RowIndex].Cells[3].Value = "";
            }
            if (e.ColumnIndex == 2
              || e.ColumnIndex == 3)
            {
                this.sprvisrDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
                string dtetmin = this.sprvisrDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
                string dtetmout = this.sprvisrDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
                if (e.ColumnIndex == 2 && dtetmin != "")
                {
                    dtetmin = Global.mnFrm.cmCde.checkNFormatDate(dtetmin).Substring(0, 11);
                    this.sprvisrDataGridView.Rows[e.RowIndex].Cells[2].Value = dtetmin;
                    this.sprvisrDataGridView.EndEdit();
                    System.Windows.Forms.Application.DoEvents();
                }
                if (e.ColumnIndex == 3 && dtetmout != "")
                {
                    dtetmout = Global.mnFrm.cmCde.checkNFormatDate(dtetmout).Substring(0, 11);
                    this.sprvisrDataGridView.Rows[e.RowIndex].Cells[3].Value = dtetmout;
                    this.sprvisrDataGridView.EndEdit();
                    System.Windows.Forms.Application.DoEvents();
                }
            }
            this.obeyEvnts = prv;
        }

        private void divsDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e == null || this.obeyEvnts == false)
            {
                return;
            }
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            bool prv = this.obeyEvnts;
            this.obeyEvnts = false;


            if (this.divsDataGridView.Rows[e.RowIndex].Cells[4].Value == null)
            {
                this.divsDataGridView.Rows[e.RowIndex].Cells[4].Value = "";
            }

            if (this.divsDataGridView.Rows[e.RowIndex].Cells[5].Value == null)
            {
                this.divsDataGridView.Rows[e.RowIndex].Cells[5].Value = "";
            }
            if (e.ColumnIndex == 4
              || e.ColumnIndex == 5)
            {
                this.divsDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
                string dtetmin = this.divsDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
                string dtetmout = this.divsDataGridView.Rows[e.RowIndex].Cells[5].Value.ToString();
                if (e.ColumnIndex == 4 && dtetmin != "")
                {
                    dtetmin = Global.mnFrm.cmCde.checkNFormatDate(dtetmin).Substring(0, 11);
                    this.divsDataGridView.Rows[e.RowIndex].Cells[4].Value = dtetmin;
                    this.divsDataGridView.EndEdit();
                    System.Windows.Forms.Application.DoEvents();
                }
                if (e.ColumnIndex == 5 && dtetmout != "")
                {
                    dtetmout = Global.mnFrm.cmCde.checkNFormatDate(dtetmout).Substring(0, 11);
                    this.divsDataGridView.Rows[e.RowIndex].Cells[5].Value = dtetmout;
                    this.divsDataGridView.EndEdit();
                    System.Windows.Forms.Application.DoEvents();
                }
            }
            this.obeyEvnts = prv;
        }

        private void jobsDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e == null || this.obeyEvnts == false)
            {
                return;
            }
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            bool prv = this.obeyEvnts;
            this.obeyEvnts = false;


            if (this.jobsDataGridView.Rows[e.RowIndex].Cells[2].Value == null)
            {
                this.jobsDataGridView.Rows[e.RowIndex].Cells[2].Value = "";
            }

            if (this.jobsDataGridView.Rows[e.RowIndex].Cells[3].Value == null)
            {
                this.jobsDataGridView.Rows[e.RowIndex].Cells[3].Value = "";
            }
            if (e.ColumnIndex == 2
              || e.ColumnIndex == 3)
            {
                this.jobsDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
                string dtetmin = this.jobsDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
                string dtetmout = this.jobsDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
                if (e.ColumnIndex == 2 && dtetmin != "")
                {
                    dtetmin = Global.mnFrm.cmCde.checkNFormatDate(dtetmin).Substring(0, 11);
                    this.jobsDataGridView.Rows[e.RowIndex].Cells[2].Value = dtetmin;
                    this.jobsDataGridView.EndEdit();
                    System.Windows.Forms.Application.DoEvents();
                }
                if (e.ColumnIndex == 3 && dtetmout != "")
                {
                    dtetmout = Global.mnFrm.cmCde.checkNFormatDate(dtetmout).Substring(0, 11);
                    this.jobsDataGridView.Rows[e.RowIndex].Cells[3].Value = dtetmout;
                    this.jobsDataGridView.EndEdit();
                    System.Windows.Forms.Application.DoEvents();
                }
            }
            this.obeyEvnts = prv;
        }

        private void gradesDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e == null || this.obeyEvnts == false)
            {
                return;
            }
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            bool prv = this.obeyEvnts;
            this.obeyEvnts = false;

            if (this.gradesDataGridView.Rows[e.RowIndex].Cells[2].Value == null)
            {
                this.gradesDataGridView.Rows[e.RowIndex].Cells[2].Value = "";
            }

            if (this.gradesDataGridView.Rows[e.RowIndex].Cells[3].Value == null)
            {
                this.gradesDataGridView.Rows[e.RowIndex].Cells[3].Value = "";
            }
            if (e.ColumnIndex == 2
              || e.ColumnIndex == 3)
            {
                this.gradesDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
                string dtetmin = this.gradesDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
                string dtetmout = this.gradesDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
                if (e.ColumnIndex == 2 && dtetmin != "")
                {
                    dtetmin = Global.mnFrm.cmCde.checkNFormatDate(dtetmin).Substring(0, 11);
                    this.gradesDataGridView.Rows[e.RowIndex].Cells[2].Value = dtetmin;
                    this.gradesDataGridView.EndEdit();
                    System.Windows.Forms.Application.DoEvents();
                }
                if (e.ColumnIndex == 3 && dtetmout != "")
                {
                    dtetmout = Global.mnFrm.cmCde.checkNFormatDate(dtetmout).Substring(0, 11);
                    this.gradesDataGridView.Rows[e.RowIndex].Cells[3].Value = dtetmout;
                    this.gradesDataGridView.EndEdit();
                    System.Windows.Forms.Application.DoEvents();
                }
            }
            this.obeyEvnts = prv;
        }

        private void positionDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e == null || this.obeyEvnts == false)
            {
                return;
            }
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            bool prv = this.obeyEvnts;
            this.obeyEvnts = false;


            if (this.positionDataGridView.Rows[e.RowIndex].Cells[2].Value == null)
            {
                this.positionDataGridView.Rows[e.RowIndex].Cells[2].Value = "";
            }

            if (this.positionDataGridView.Rows[e.RowIndex].Cells[3].Value == null)
            {
                this.positionDataGridView.Rows[e.RowIndex].Cells[3].Value = "";
            }
            if (e.ColumnIndex == 2
              || e.ColumnIndex == 3)
            {
                this.positionDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
                string dtetmin = this.positionDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
                string dtetmout = this.positionDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
                if (e.ColumnIndex == 2 && dtetmin != "")
                {
                    dtetmin = Global.mnFrm.cmCde.checkNFormatDate(dtetmin).Substring(0, 11);
                    this.positionDataGridView.Rows[e.RowIndex].Cells[2].Value = dtetmin;
                    this.positionDataGridView.EndEdit();
                    System.Windows.Forms.Application.DoEvents();
                }
                if (e.ColumnIndex == 3 && dtetmout != "")
                {
                    dtetmout = Global.mnFrm.cmCde.checkNFormatDate(dtetmout).Substring(0, 11);
                    this.positionDataGridView.Rows[e.RowIndex].Cells[3].Value = dtetmout;
                    this.positionDataGridView.EndEdit();
                    System.Windows.Forms.Application.DoEvents();
                }
            }
            this.obeyEvnts = prv;
        }

        private void positionDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e == null /*|| this.obeyEvnts == false*/ || this.savePostnButton.Enabled == false)
            {
                return;
            }
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            bool prv = this.obeyEvnts;
            this.obeyEvnts = false;


            if (this.positionDataGridView.Rows[e.RowIndex].Cells[7].Value == null)
            {
                this.positionDataGridView.Rows[e.RowIndex].Cells[7].Value = "";
            }

            if (this.positionDataGridView.Rows[e.RowIndex].Cells[8].Value == null)
            {
                this.positionDataGridView.Rows[e.RowIndex].Cells[8].Value = "-1";
            }
            if (e.ColumnIndex == 9)
            {
                //Divisions/Groups
                string[] selVals = new string[1];
                selVals[0] = this.positionDataGridView.Rows[e.RowIndex].Cells[8].Value.ToString();
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                 Global.mnFrm.cmCde.getLovID("Divisions/Groups"), ref selVals, true,
                 false, Global.mnFrm.cmCde.Org_id);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.positionDataGridView.Rows[e.RowIndex].Cells[8].Value = selVals[i];
                        this.positionDataGridView.Rows[e.RowIndex].Cells[7].Value = Global.mnFrm.cmCde.getDivName(int.Parse(selVals[i]));
                    }
                }
                this.positionDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
            }
            else if (e.ColumnIndex == 1)
            {
                //Grades
                string[] selVals = new string[1];
                selVals[0] = this.positionDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                 Global.mnFrm.cmCde.getLovID("Positions"), ref selVals, true,
                 false, Global.mnFrm.cmCde.Org_id);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.positionDataGridView.Rows[e.RowIndex].Cells[4].Value = selVals[i];
                        this.positionDataGridView.Rows[e.RowIndex].Cells[0].Value =
                         Global.mnFrm.cmCde.getPosName(int.Parse(selVals[i]));
                    }
                }
                this.positionDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
            }
            this.obeyEvnts = prv;
        }

        private void sitesDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e == null /*|| this.obeyEvnts == false*/ || this.saveLocButton.Enabled == false)
            {
                return;
            }
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            bool prv = this.obeyEvnts;
            this.obeyEvnts = false;

            if (this.sitesDataGridView.Rows[e.RowIndex].Cells[0].Value == null)
            {
                this.sitesDataGridView.Rows[e.RowIndex].Cells[0].Value = "";
            }

            if (this.sitesDataGridView.Rows[e.RowIndex].Cells[4].Value == null)
            {
                this.sitesDataGridView.Rows[e.RowIndex].Cells[4].Value = "-1";
            }
            if (e.ColumnIndex == 1)
            {
                //Divisions/Groups
                string[] selVals = new string[1];
                selVals[0] = this.sitesDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                 Global.mnFrm.cmCde.getLovID("Sites/Locations"), ref selVals, true,
                 false, Global.mnFrm.cmCde.Org_id);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.sitesDataGridView.Rows[e.RowIndex].Cells[4].Value = selVals[i];
                        this.sitesDataGridView.Rows[e.RowIndex].Cells[0].Value =
                         Global.mnFrm.cmCde.getSiteName(int.Parse(selVals[i]));
                    }
                }
                this.sitesDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
            }
            this.obeyEvnts = prv;
        }

        private void sprvisrDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e == null /*|| this.obeyEvnts == false*/ || this.saveSprvsrButton.Enabled == false)
            {
                return;
            }
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            bool prv = this.obeyEvnts;
            this.obeyEvnts = false;

            if (this.sprvisrDataGridView.Rows[e.RowIndex].Cells[0].Value == null)
            {
                this.sprvisrDataGridView.Rows[e.RowIndex].Cells[0].Value = "";
            }

            if (this.sprvisrDataGridView.Rows[e.RowIndex].Cells[4].Value == null)
            {
                this.sprvisrDataGridView.Rows[e.RowIndex].Cells[4].Value = "-1";
            }
            if (e.ColumnIndex == 1)
            {
                //Divisions/Groups
                string[] selVals = new string[1];
                selVals[0] = Global.mnFrm.cmCde.getPrsnLocID(
                  long.Parse(this.sprvisrDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString()));
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                 Global.mnFrm.cmCde.getLovID("Active Persons"), ref selVals, true,
                 false, Global.mnFrm.cmCde.Org_id);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.sprvisrDataGridView.Rows[e.RowIndex].Cells[4].Value = Global.mnFrm.cmCde.getPrsnID(selVals[i]);
                        this.sprvisrDataGridView.Rows[e.RowIndex].Cells[0].Value =
                         Global.mnFrm.cmCde.getPrsnName(selVals[i]);
                    }
                }
                this.sprvisrDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
            }
            this.obeyEvnts = prv;
        }

        private void divsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e == null /*|| this.obeyEvnts == false*/ || this.saveDivButton.Enabled == false)
            {
                return;
            }
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            bool prv = this.obeyEvnts;
            this.obeyEvnts = false;

            if (this.divsDataGridView.Rows[e.RowIndex].Cells[0].Value == null)
            {
                this.divsDataGridView.Rows[e.RowIndex].Cells[0].Value = "";
            }

            if (this.divsDataGridView.Rows[e.RowIndex].Cells[4].Value == null)
            {
                this.divsDataGridView.Rows[e.RowIndex].Cells[4].Value = "-1";
            }
            if (e.ColumnIndex == 1)
            {
                //Divisions/Groups
                string[] selVals = new string[1];
                selVals[0] = this.divsDataGridView.Rows[e.RowIndex].Cells[6].Value.ToString();
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                 Global.mnFrm.cmCde.getLovID("Divisions/Groups"), ref selVals, true,
                 false, Global.mnFrm.cmCde.Org_id);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        int divTyp = -1;
                        int.TryParse(Global.mnFrm.cmCde.getGnrlRecNm("org.org_divs_groups",
                          "div_id", "div_typ_id", int.Parse(selVals[i])), out divTyp);
                        this.divsDataGridView.Rows[e.RowIndex].Cells[6].Value = selVals[i];
                        this.divsDataGridView.Rows[e.RowIndex].Cells[0].Value = Global.mnFrm.cmCde.getDivName(int.Parse(selVals[i]));
                        this.divsDataGridView.Rows[e.RowIndex].Cells[2].Value = Global.mnFrm.cmCde.getPssblValNm(divTyp);
                    }
                }
                this.divsDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
            }
            this.obeyEvnts = prv;
        }

        private void jobsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e == null /*|| this.obeyEvnts == false*/ || this.saveJobButton.Enabled == false)
            {
                return;
            }
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            bool prv = this.obeyEvnts;
            this.obeyEvnts = false;

            if (this.jobsDataGridView.Rows[e.RowIndex].Cells[0].Value == null)
            {
                this.jobsDataGridView.Rows[e.RowIndex].Cells[0].Value = "";
            }

            if (this.jobsDataGridView.Rows[e.RowIndex].Cells[4].Value == null)
            {
                this.jobsDataGridView.Rows[e.RowIndex].Cells[4].Value = "-1";
            }
            if (e.ColumnIndex == 1)
            {
                //org_jobs
                string[] selVals = new string[1];
                selVals[0] = this.jobsDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                 Global.mnFrm.cmCde.getLovID("Jobs"), ref selVals, true,
                 false, Global.mnFrm.cmCde.Org_id);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.jobsDataGridView.Rows[e.RowIndex].Cells[4].Value = selVals[i];
                        this.jobsDataGridView.Rows[e.RowIndex].Cells[0].Value =
                         Global.mnFrm.cmCde.getJobName(int.Parse(selVals[i]));
                    }
                }
                this.jobsDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
            }
            this.obeyEvnts = prv;
        }

        private void gradesDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e == null /*|| this.obeyEvnts == false*/ || this.saveGradeButton.Enabled == false)
            {
                return;
            }
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            bool prv = this.obeyEvnts;
            this.obeyEvnts = false;

            if (this.gradesDataGridView.Rows[e.RowIndex].Cells[0].Value == null)
            {
                this.gradesDataGridView.Rows[e.RowIndex].Cells[0].Value = "";
            }

            if (this.gradesDataGridView.Rows[e.RowIndex].Cells[4].Value == null)
            {
                this.gradesDataGridView.Rows[e.RowIndex].Cells[4].Value = "-1";
            }
            if (e.ColumnIndex == 1)
            {
                //Grades
                string[] selVals = new string[1];
                selVals[0] = this.gradesDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                 Global.mnFrm.cmCde.getLovID("Grades"), ref selVals, true,
                 false, Global.mnFrm.cmCde.Org_id);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.gradesDataGridView.Rows[e.RowIndex].Cells[4].Value = selVals[i];
                        this.gradesDataGridView.Rows[e.RowIndex].Cells[0].Value =
                         Global.mnFrm.cmCde.getGrdName(int.Parse(selVals[i]));
                    }
                }
                this.gradesDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
            }
            this.obeyEvnts = prv;
        }

        private void skillsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e == null /*|| this.obeyEvnts == false*/ || this.saveSkillButton.Enabled == false)
            {
                return;
            }
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            bool prv = this.obeyEvnts;
            this.obeyEvnts = false;

            if (this.skillsDataGridView.Rows[e.RowIndex].Cells[0].Value == null)
            {
                this.skillsDataGridView.Rows[e.RowIndex].Cells[0].Value = "";
            }
            if (this.skillsDataGridView.Rows[e.RowIndex].Cells[2].Value == null)
            {
                this.skillsDataGridView.Rows[e.RowIndex].Cells[2].Value = "";
            }
            if (this.skillsDataGridView.Rows[e.RowIndex].Cells[4].Value == null)
            {
                this.skillsDataGridView.Rows[e.RowIndex].Cells[4].Value = "";
            }
            if (this.skillsDataGridView.Rows[e.RowIndex].Cells[6].Value == null)
            {
                this.skillsDataGridView.Rows[e.RowIndex].Cells[6].Value = "";
            }
            if (this.skillsDataGridView.Rows[e.RowIndex].Cells[8].Value == null)
            {
                this.skillsDataGridView.Rows[e.RowIndex].Cells[8].Value = "";
            }
            char[] w = { ',' };
            int lngth = 1;
            string res = "";
            if (e.ColumnIndex == 1)
            {
                string[] selVals1 = this.skillsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString().Split(w, StringSplitOptions.RemoveEmptyEntries);
                if (selVals1.Length > 0)
                {
                    lngth = selVals1.Length;
                }
                int[] selVals = new int[lngth];
                for (int a = 0; a < selVals1.Length; a++)
                {
                    selVals[a] = Global.mnFrm.cmCde.getPssblValID(
                      selVals1[a],
                     Global.mnFrm.cmCde.getLovID("Languages"));
                }
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                 Global.mnFrm.cmCde.getLovID("Languages"), ref selVals, false, false);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        res += Global.mnFrm.cmCde.getPssblValNm(selVals[i]) + ",";
                    }
                    this.skillsDataGridView.Rows[e.RowIndex].Cells[0].Value = res.Trim(w);
                }
            }
            else if (e.ColumnIndex == 3)
            {
                string[] selVals1 = this.skillsDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString().Split(w, StringSplitOptions.RemoveEmptyEntries);
                if (selVals1.Length > 0)
                {
                    lngth = selVals1.Length;
                }
                int[] selVals = new int[lngth];
                for (int a = 0; a < selVals1.Length; a++)
                {
                    selVals[a] = Global.mnFrm.cmCde.getPssblValID(
                      selVals1[a],
                     Global.mnFrm.cmCde.getLovID("Hobbies"));
                }
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                 Global.mnFrm.cmCde.getLovID("Hobbies"), ref selVals, false, false);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        res += Global.mnFrm.cmCde.getPssblValNm(selVals[i]) + ",";
                    }
                    this.skillsDataGridView.Rows[e.RowIndex].Cells[2].Value = res.Trim(w);
                }
            }
            else if (e.ColumnIndex == 5)
            {
                string[] selVals1 = this.skillsDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString().Split(w, StringSplitOptions.RemoveEmptyEntries);
                if (selVals1.Length > 0)
                {
                    lngth = selVals1.Length;
                }
                int[] selVals = new int[lngth];
                for (int a = 0; a < selVals1.Length; a++)
                {
                    selVals[a] = Global.mnFrm.cmCde.getPssblValID(
                      selVals1[a],
                     Global.mnFrm.cmCde.getLovID("Interests"));
                }
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                 Global.mnFrm.cmCde.getLovID("Interests"), ref selVals, false, false);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        res += Global.mnFrm.cmCde.getPssblValNm(selVals[i]) + ",";
                    }
                    this.skillsDataGridView.Rows[e.RowIndex].Cells[4].Value = res.Trim(w);
                }
            }
            else if (e.ColumnIndex == 7)
            {
                string[] selVals1 = this.skillsDataGridView.Rows[e.RowIndex].Cells[6].Value.ToString().Split(w, StringSplitOptions.RemoveEmptyEntries);
                if (selVals1.Length > 0)
                {
                    lngth = selVals1.Length;
                }
                int[] selVals = new int[lngth];
                for (int a = 0; a < selVals1.Length; a++)
                {
                    selVals[a] = Global.mnFrm.cmCde.getPssblValID(
                      selVals1[a],
                     Global.mnFrm.cmCde.getLovID("Conduct"));
                }
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                 Global.mnFrm.cmCde.getLovID("Conduct"), ref selVals, false, false);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        res += Global.mnFrm.cmCde.getPssblValNm(selVals[i]) + ",";
                    }
                    this.skillsDataGridView.Rows[e.RowIndex].Cells[6].Value = res.Trim(w);
                }
            }
            else if (e.ColumnIndex == 9)
            {
                string[] selVals1 = this.skillsDataGridView.Rows[e.RowIndex].Cells[8].Value.ToString().Split(w, StringSplitOptions.RemoveEmptyEntries);
                if (selVals1.Length > 0)
                {
                    lngth = selVals1.Length;
                }
                int[] selVals = new int[lngth];
                for (int a = 0; a < selVals1.Length; a++)
                {
                    selVals[a] = Global.mnFrm.cmCde.getPssblValID(
                      selVals1[a],
                     Global.mnFrm.cmCde.getLovID("Attitudes"));
                }
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                 Global.mnFrm.cmCde.getLovID("Attitudes"), ref selVals, false, false);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        res += Global.mnFrm.cmCde.getPssblValNm(selVals[i]) + ",";
                    }
                    this.skillsDataGridView.Rows[e.RowIndex].Cells[8].Value = res.Trim(w);
                }
            }

            this.skillsDataGridView.EndEdit();
            System.Windows.Forms.Application.DoEvents();


            this.obeyEvnts = prv;
        }

        private void educDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e == null || this.obeyEvnts == false)
            {
                return;
            }
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            bool prv = this.obeyEvnts;
            this.obeyEvnts = false;

            if (this.educDataGridView.Rows[e.RowIndex].Cells[6].Value == null)
            {
                this.educDataGridView.Rows[e.RowIndex].Cells[6].Value = "";
            }

            if (this.educDataGridView.Rows[e.RowIndex].Cells[7].Value == null)
            {
                this.educDataGridView.Rows[e.RowIndex].Cells[7].Value = "";
            }
            if (this.educDataGridView.Rows[e.RowIndex].Cells[12].Value == null)
            {
                this.educDataGridView.Rows[e.RowIndex].Cells[12].Value = "";
            }
            if (e.ColumnIndex == 6
              || e.ColumnIndex == 7
              || e.ColumnIndex == 12)
            {
                this.educDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
                string dtetmin = this.educDataGridView.Rows[e.RowIndex].Cells[6].Value.ToString();
                string dtetmout = this.educDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString();
                string dtetmout1 = this.educDataGridView.Rows[e.RowIndex].Cells[12].Value.ToString();
                if (e.ColumnIndex == 6 && dtetmin != "")
                {
                    dtetmin = Global.mnFrm.cmCde.checkNFormatDate(dtetmin).Substring(0, 11);
                    this.educDataGridView.Rows[e.RowIndex].Cells[6].Value = dtetmin;
                    this.educDataGridView.EndEdit();
                    System.Windows.Forms.Application.DoEvents();
                }
                if (e.ColumnIndex == 7 && dtetmout != "")
                {
                    dtetmout = Global.mnFrm.cmCde.checkNFormatDate(dtetmout).Substring(0, 11);
                    this.educDataGridView.Rows[e.RowIndex].Cells[7].Value = dtetmout;
                    this.educDataGridView.EndEdit();
                    System.Windows.Forms.Application.DoEvents();
                }
                if (e.ColumnIndex == 12 && dtetmout1 != "")
                {
                    dtetmout1 = Global.mnFrm.cmCde.checkNFormatDate(dtetmout1).Substring(0, 11);
                    this.educDataGridView.Rows[e.RowIndex].Cells[12].Value = dtetmout1;
                    this.educDataGridView.EndEdit();
                    System.Windows.Forms.Application.DoEvents();
                }
            }
            this.obeyEvnts = prv;
        }

        private void wrkExpDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e == null /*|| this.obeyEvnts == false*/ || this.saveWrkButton.Enabled == false)
            {
                return;
            }
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            bool prv = this.obeyEvnts;
            this.obeyEvnts = false;

            if (this.wrkExpDataGridView.Rows[e.RowIndex].Cells[0].Value == null)
            {
                this.wrkExpDataGridView.Rows[e.RowIndex].Cells[0].Value = "";
            }
            if (this.wrkExpDataGridView.Rows[e.RowIndex].Cells[2].Value == null)
            {
                this.wrkExpDataGridView.Rows[e.RowIndex].Cells[2].Value = "";
            }
            if (this.wrkExpDataGridView.Rows[e.RowIndex].Cells[4].Value == null)
            {
                this.wrkExpDataGridView.Rows[e.RowIndex].Cells[4].Value = "";
            }


            if (e.ColumnIndex == 1)
            {
                int[] selVals = new int[1];
                selVals[0] = Global.mnFrm.cmCde.getPssblValID(
                  this.wrkExpDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString(),
                 Global.mnFrm.cmCde.getLovID("Jobs/Professions/Occupations"));
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                 Global.mnFrm.cmCde.getLovID("Jobs/Professions/Occupations"), ref selVals, true, false);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.wrkExpDataGridView.Rows[e.RowIndex].Cells[0].Value = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
                    }
                }
            }
            else if (e.ColumnIndex == 3)
            {
                int[] selVals = new int[1];
                selVals[0] = Global.mnFrm.cmCde.getPssblValID(this.wrkExpDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString(),
                 Global.mnFrm.cmCde.getLovID("Schools/Organisations/Institutions"));
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                 Global.mnFrm.cmCde.getLovID("Schools/Organisations/Institutions"), ref selVals, true, false);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.wrkExpDataGridView.Rows[e.RowIndex].Cells[2].Value = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
                    }
                }
            }
            else if (e.ColumnIndex == 5)
            {
                int[] selVals = new int[1];
                selVals[0] = Global.mnFrm.cmCde.getPssblValID(this.wrkExpDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString(),
                 Global.mnFrm.cmCde.getLovID("Other Locations"));
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                 Global.mnFrm.cmCde.getLovID("Other Locations"), ref selVals, true, false);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.wrkExpDataGridView.Rows[e.RowIndex].Cells[4].Value = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
                    }
                }
            }

            this.wrkExpDataGridView.EndEdit();
            System.Windows.Forms.Application.DoEvents();


            this.obeyEvnts = prv;
        }

        private void wrkExpDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e == null || this.obeyEvnts == false)
            {
                return;
            }
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            bool prv = this.obeyEvnts;
            this.obeyEvnts = false;


            if (this.wrkExpDataGridView.Rows[e.RowIndex].Cells[6].Value == null)
            {
                this.wrkExpDataGridView.Rows[e.RowIndex].Cells[6].Value = "";
            }

            if (this.wrkExpDataGridView.Rows[e.RowIndex].Cells[7].Value == null)
            {
                this.wrkExpDataGridView.Rows[e.RowIndex].Cells[7].Value = "";
            }
            if (e.ColumnIndex == 6
              || e.ColumnIndex == 7)
            {
                this.wrkExpDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
                string dtetmin = this.wrkExpDataGridView.Rows[e.RowIndex].Cells[6].Value.ToString();
                string dtetmout = this.wrkExpDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString();
                if (e.ColumnIndex == 6 && dtetmin != "")
                {
                    dtetmin = Global.mnFrm.cmCde.checkNFormatDate(dtetmin).Substring(0, 11);
                    this.wrkExpDataGridView.Rows[e.RowIndex].Cells[6].Value = dtetmin;
                    this.wrkExpDataGridView.EndEdit();
                    System.Windows.Forms.Application.DoEvents();
                }
                if (e.ColumnIndex == 7 && dtetmout != "")
                {
                    dtetmout = Global.mnFrm.cmCde.checkNFormatDate(dtetmout).Substring(0, 11);
                    this.wrkExpDataGridView.Rows[e.RowIndex].Cells[7].Value = dtetmout;
                    this.wrkExpDataGridView.EndEdit();
                    System.Windows.Forms.Application.DoEvents();
                }
            }
            this.obeyEvnts = prv;
        }

        private void skillsDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e == null || this.obeyEvnts == false)
            {
                return;
            }
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            bool prv = this.obeyEvnts;
            this.obeyEvnts = false;


            if (this.skillsDataGridView.Rows[e.RowIndex].Cells[10].Value == null)
            {
                this.skillsDataGridView.Rows[e.RowIndex].Cells[10].Value = "";
            }

            if (this.skillsDataGridView.Rows[e.RowIndex].Cells[11].Value == null)
            {
                this.skillsDataGridView.Rows[e.RowIndex].Cells[11].Value = "";
            }
            if (e.ColumnIndex == 10
              || e.ColumnIndex == 11)
            {
                this.skillsDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
                string dtetmin = this.skillsDataGridView.Rows[e.RowIndex].Cells[10].Value.ToString();
                string dtetmout = this.skillsDataGridView.Rows[e.RowIndex].Cells[11].Value.ToString();
                if (e.ColumnIndex == 10 && dtetmin != "")
                {
                    dtetmin = Global.mnFrm.cmCde.checkNFormatDate(dtetmin).Substring(0, 11);
                    this.skillsDataGridView.Rows[e.RowIndex].Cells[10].Value = dtetmin;
                    this.skillsDataGridView.EndEdit();
                    System.Windows.Forms.Application.DoEvents();
                }
                if (e.ColumnIndex == 11 && dtetmout != "")
                {
                    dtetmout = Global.mnFrm.cmCde.checkNFormatDate(dtetmout).Substring(0, 11);
                    this.skillsDataGridView.Rows[e.RowIndex].Cells[11].Value = dtetmout;
                    this.skillsDataGridView.EndEdit();
                    System.Windows.Forms.Application.DoEvents();
                }
            }
            this.obeyEvnts = prv;
        }

        private void prsnTypComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.prsnTypComboBox.SelectedIndex >= 0 && this.obey_prs_evnts == true)
            {
                this.prs_cur_indx = 0;
                this.goPrsButton_Click(this.goPrsButton, e);
            }

        }

        private void fltrByComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.fltrByComboBox.SelectedIndex < 0 || this.obey_prs_evnts == false)
            {
                return;
            }
            bool prv = this.obey_prs_evnts;

            this.obey_prs_evnts = false;
            this.prsnTypComboBox.Items.Clear();
            DataSet dtst;
            if (this.fltrByComboBox.SelectedIndex == 3)
            {
                //Positions
                dtst = Global.mnFrm.cmCde.selectDataNoParams("Select position_code_name from org.org_positions where org_id=" + Global.mnFrm.cmCde.Org_id);
            }
            else if (this.fltrByComboBox.SelectedIndex == 0)
            {
                //Div Groups
                dtst = Global.mnFrm.cmCde.selectDataNoParams("Select div_code_name from org.org_divs_groups where org_id=" + Global.mnFrm.cmCde.Org_id);
            }
            else if (this.fltrByComboBox.SelectedIndex == 1)
            {
                //Grade
                dtst = Global.mnFrm.cmCde.selectDataNoParams("Select grade_code_name from org.org_grades where org_id=" + Global.mnFrm.cmCde.Org_id);
            }
            else if (this.fltrByComboBox.SelectedIndex == 2)
            {
                //Job
                dtst = Global.mnFrm.cmCde.selectDataNoParams("Select job_code_name from org.org_jobs where org_id=" + Global.mnFrm.cmCde.Org_id);
            }
            else
            {
                //Person Types
                string aldPrsTyp = Global.getAllwdPrsnTyps();
                string extra3 = "";
                char[] t = { '\'' };
                aldPrsTyp = "'" + aldPrsTyp.Trim(t) + "'";
                if (aldPrsTyp != "'All'")
                {
                    extra3 = @" and pssbl_value IN (" + aldPrsTyp + ")";
                }
                dtst = Global.getAllEnbldPssblVals("Person Types", extra3);
            }
            this.prsnTypComboBox.Items.Add("All");
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.prsnTypComboBox.Items.Add(dtst.Tables[0].Rows[i][0].ToString());
            }

            this.obey_prs_evnts = prv;
            this.prsnTypComboBox.SelectedIndex = 0;
        }

        private void runRptButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showRptParamsDiag(-1, Global.mnFrm.cmCde);
        }

        private void assgnPayItemSetButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[13]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            massAsgnItmsDiag nwDiag = new massAsgnItmsDiag();
            nwDiag.orgID = Global.mnFrm.cmCde.Org_id;
            nwDiag.prsnIDs[0] = long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text);

            long dsply1 = 0;
            string[] vl = Global.get_Org_DfltItmSt(Global.mnFrm.cmCde.Org_id);
            //this.itmStIDMnlTextBox.Text = vl[0];
            //this.itmStNmMnlTextBox.Text = vl[1];
            nwDiag.pyItmSetID = int.Parse(vl[0]);

            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
                //if (this.pymntTabControl.SelectedTab == this.payTabPage)
                //{
                //  this.goItmButtonNw.PerformClick();
                //}
                //else
                //{
                //  this.refreshValButton_ClickPrs(this.refreshValButtonPrs, e);
                //}
            }
        }

        private void runQckPyButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[9]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                  " this action!\nContact your System Administrator!", 0);
                return;
            }
            quickPayForm nwDiag = new quickPayForm();
            if (this.prsNamesListView.SelectedItems.Count > 0)
            {
                nwDiag.locIDTextBox.Text = this.prsNamesListView.SelectedItems[0].SubItems[1].Text;
            }
            string[] vl = Global.get_Org_DfltItmSt(Global.mnFrm.cmCde.Org_id);

            nwDiag.msPyItmStIDTextBox.Text = vl[0];
            nwDiag.msPyItmStNmTextBox.Text = vl[1];

            DialogResult dgres = nwDiag.ShowDialog();
            if (dgres == DialogResult.OK)
            {
            }
            else if (dgres == DialogResult.Ignore)
            {
                if (this.prsNamesListView.SelectedItems.Count <= 0)
                {
                    Global.mnFrm.cmCde.showMsg("Please select a Person First!", 0);
                    return;
                }
                this.payRunSlipPDF(nwDiag.mspID,
                  long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
            }
        }
        int pageNo = 1;
        int prntIdx = 0;

        public void payRunSlipPDF(long msPyID, long prsnID)
        {
            if (msPyID <= 0)
            {
                Global.mnFrm.cmCde.showMsg("No Valid Pay Run Selected!", 0);
                return;
            }

            Graphics g = Graphics.FromHwnd(this.Handle);
            XPen aPen = new XPen(XColor.FromArgb(System.Drawing.Color.Black), 1);
            PdfDocument document = new PdfDocument();
            document.Info.Title = "PAY RUN RESULTS REPORT";
            // Create first page for basic person details
            PdfPage page0 = document.AddPage();
            page0.Orientation = PageOrientation.Portrait;
            page0.Height = XUnit.FromInch(11);
            page0.Width = XUnit.FromInch(8.5);
            XGraphics gfx0 = XGraphics.FromPdfPage(page0);
            XFont xfont0 = new XFont("Verdana", 20, XFontStyle.BoldItalic);
            //gfx0.DrawString("Hello, World!" + this.locIDTextBox.Text, xfont0, XBrushes.Black,
            //new XRect(0, 0, page0.Width, page0.Height),
            //  XStringFormats.TopLeft);

            XFont xfont1 = new XFont("Verdana", 10.25f, XFontStyle.Underline | XFontStyle.Bold);
            XFont xfont11 = new XFont("Verdana", 10.25f, XFontStyle.Bold);
            XFont xfont2 = new XFont("Verdana", 10.25f, XFontStyle.Bold);
            XFont xfont4 = new XFont("Verdana", 10.0f, XFontStyle.Bold);
            XFont xfont41 = new XFont("Lucida Console", 10.0f);
            XFont xfont3 = new XFont("Lucida Console", 8.25f);
            XFont xfont31 = new XFont("Lucida Console", 10.5f, XFontStyle.Bold);
            XFont xfont5 = new XFont("Times New Roman", 6.0f, XFontStyle.Italic);

            Font font1 = new Font("Verdana", 10.25f, FontStyle.Underline | FontStyle.Bold);
            Font font11 = new Font("Verdana", 10.25f, FontStyle.Bold);
            Font font2 = new Font("Verdana", 10.25f, FontStyle.Bold);
            Font font4 = new Font("Verdana", 10.0f, FontStyle.Bold);
            Font font41 = new Font("Lucida Console", 10.0f);
            Font font3 = new Font("Lucida Console", 8.25f);
            Font font31 = new Font("Lucida Console", 10.5f, FontStyle.Bold);
            Font font5 = new Font("Times New Roman", 6.0f, FontStyle.Italic);

            float font1Hght = (float)gfx0.MeasureString("ABCDEFGHIJKLMNOP", xfont1).Height;
            float font2Hght = (float)gfx0.MeasureString("ABCDEFGHIJKLMNOP", xfont2).Height;
            float font3Hght = (float)gfx0.MeasureString("ABCDEFGHIJKLMNOP", xfont3).Height;
            float font4Hght = (float)gfx0.MeasureString("ABCDEFGHIJKLMNOP", xfont41).Height;
            float font5Hght = (float)gfx0.MeasureString("ABCDEFGHIJKLMNOP", xfont5).Height;

            float startX = 25;
            float startXNw = 35;
            float endX = 560;
            float startY = 40;
            float offsetY = 0;
            float ght = 0;
            float wdth = 0;
            XTextFormatter tf;
            XRect rect;
            string finlStr = "";

            float pageWidth = 590 - startX;//e.PageSettings.PrintableArea.Width;
                                           //float pageHeight = 760 - 40;// e.PageSettings.PrintableArea.Height;
            float txtwdth = pageWidth - startX;
            //Global.mnFrm.cmCde.showMsg(pageWidth.ToString(), 0);
            //float gwdth = 0;
            //StringBuilder strPrnt = new StringBuilder();
            //strPrnt.AppendLine("Received From");
            string[] nwLn;
            int pageNo = 1;
            XImage img = (XImage)Global.mnFrm.cmCde.getDBImageFile(Global.mnFrm.cmCde.Org_id.ToString() + ".png", 0);
            float picWdth = 80.00F;
            float picHght = (float)(picWdth / img.PixelWidth) * (float)img.PixelHeight;

            if (this.pageNo == 1)
            { }//Org Logo
               //RectangleF srcRect = new Rectangle(0, 0, this.BackgroundImage.Width,
               //BackgroundImage.Height);
               //RectangleF destRect = new Rectangle(0, 0, nWidth, nHeight);
               //Rectangle destRect = new Rectangle(0, 0, nWidth, nHeight);

            DataSet dtst = Global.get_One_MsPyDetSmmry(msPyID, prsnID);
            string orgType = Global.mnFrm.cmCde.getPssblValNm(int.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
              "org.org_details", "org_id", "org_typ_id", Global.mnFrm.cmCde.Org_id)));

            float oldoffsetY = offsetY;
            float hgstOffsetY = 0;
            float hghstght = 0;
            XSize sze;
            bool hsErn = false;
            bool hsDeduct = false;
            float orgoffsetY = 0;

            string[] itmTypes = new string[7];
            double[] itmTypeTtls = new double[7];
            double netPay = 0;
            int itmTypIdx = 0;
            string lastItmTyp = "";
            this.pageNo = 1;
            this.prntIdx = 0;
            string[] hdrs = {"Item                                        ","Amount (" + Global.mnFrm.cmCde.getPssblValNm(
     Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id)) +
     ")"};
            for (int a = this.prntIdx; a < dtst.Tables[0].Rows.Count; a++)
            {
                if (this.pageNo == 1)
                {
                    startX = 25;
                    startY = 40;
                    offsetY = 0;
                    ght = 0;
                    wdth = 0;
                    oldoffsetY = offsetY;
                    hgstOffsetY = 0;
                    hghstght = 0;
                    hsErn = false;
                    hsDeduct = false;
                    orgoffsetY = 0;

                    itmTypes = new string[7];
                    itmTypeTtls = new double[7];
                    netPay = 0;
                    itmTypIdx = 0;
                    lastItmTyp = "";
                    endX = 0;
                    endX = startX + (float)(pageWidth * 0.7);

                    gfx0.DrawImage(img, startX - 10, startY + offsetY - 15, picWdth, picHght);
                    //g.DrawImage(this.LargerImage, destRect, srcRect, GraphicsUnit.Pixel);

                    //Org Name
                    nwLn = Global.mnFrm.cmCde.breakRptTxtDown(
                      Global.mnFrm.cmCde.getOrgName(Global.mnFrm.cmCde.Org_id),
                      pageWidth, font2, g);
                    for (int i = 0; i < nwLn.Length; i++)
                    {
                        gfx0.DrawString(nwLn[i]
                        , xfont2, XBrushes.Black, startX + picWdth + 5, startY + offsetY);
                        offsetY += font2Hght;
                    }

                    //     nwLn = Global.mnFrm.cmCde.breakRptTxtDown(
                    //Global.mnFrm.cmCde.getOrgPstlAddrs(Global.mnFrm.cmCde.Org_id).Trim(),
                    //pageWidth, font2, g);
                    ght = (float)gfx0.MeasureString(
                      Global.mnFrm.cmCde.getOrgPstlAddrs(Global.mnFrm.cmCde.Org_id).Trim(), xfont2).Height;
                    //offsetY = offsetY + (int)ght;

                    //Pstal Address
                    tf = new XTextFormatter(gfx0);
                    rect = new XRect(startX + picWdth + 5, startY + offsetY - 7, pageWidth, ght);
                    gfx0.DrawRectangle(XBrushes.White, rect);
                    tf.DrawString(Global.mnFrm.cmCde.getOrgPstlAddrs(Global.mnFrm.cmCde.Org_id).Trim()
                      , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                    //gfx0.DrawString(,
                    //xfont2, XBrushes.Black, startX + picWdth, startY + offsetY);
                    offsetY += ght + 5;

                    //Contacts Nos
                    //Contacts Nos
                    nwLn = Global.mnFrm.cmCde.breakPDFTxtDown(
               Global.mnFrm.cmCde.getOrgContactNos(Global.mnFrm.cmCde.Org_id),
               pageWidth, font2, g);
                    for (int i = 0; i < nwLn.Length; i++)
                    {
                        gfx0.DrawString(nwLn[i]
                        , xfont2, XBrushes.Black, startX + picWdth + 5, startY + offsetY);
                        offsetY += font2Hght;
                    }
                    //          nwLn = Global.mnFrm.cmCde.breakRptTxtDown(
                    //Global.mnFrm.cmCde.getOrgContactNos(Global.mnFrm.cmCde.Org_id).Trim(),
                    //200, font2, g);
                    //          ght = (float)gfx0.MeasureString(
                    //            string.Join(" ", nwLn), xfont2).Height;
                    //          tf = new XTextFormatter(gfx0);
                    //          rect = new XRect(startX + picWdth + 5, startY + offsetY - 7, pageWidth, ght);
                    //          gfx0.DrawRectangle(XBrushes.White, rect);
                    //          tf.DrawString(string.Join(" ", nwLn)
                    //            , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                    //          offsetY += ght + 5;

                    //Email Address
                    nwLn = Global.mnFrm.cmCde.breakRptTxtDown(
               Global.mnFrm.cmCde.getOrgEmailAddrs(Global.mnFrm.cmCde.Org_id),
               pageWidth, font2, g);
                    for (int i = 0; i < nwLn.Length; i++)
                    {
                        gfx0.DrawString(nwLn[i]
                        , xfont2, XBrushes.Black, startX + picWdth + 5, startY + offsetY);
                        offsetY += font2Hght;
                    }
                    offsetY += font2Hght;
                    if (offsetY < picHght)
                    {
                        offsetY = picHght;
                    }
                    gfx0.DrawLine(aPen, startX, startY + offsetY - 8, startX + pageWidth - 40,
               startY + offsetY - 8);
                    //Person Types
                    //Title
                    ght = (float)gfx0.MeasureString(
                          ("ITEM RUN RESULTS SLIP").ToUpper(), xfont2).Height;
                    //lblght = ght;
                    tf = new XTextFormatter(gfx0);
                    rect = new XRect(startX, startY + offsetY, pageWidth, ght);
                    gfx0.DrawRectangle(XBrushes.White, rect);
                    tf.DrawString(("ITEM RUN RESULTS SLIP").ToUpper()
                      , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                    offsetY += (int)ght + 5;

                    //offsetY += font1Hght;
                    //Loop Through Records


                    gfx0.DrawLine(aPen, startX, startY + offsetY, startX + pageWidth - 40,
                     startY + offsetY);
                    offsetY += font1Hght;

                    ght = (float)gfx0.MeasureString(
                      ("Name(ID): ").ToUpper(), xfont2).Height;
                    tf = new XTextFormatter(gfx0);
                    rect = new XRect(startX, startY + offsetY, pageWidth, ght);
                    gfx0.DrawRectangle(XBrushes.White, rect);
                    tf.DrawString(("Name(ID): ").ToUpper()
                      , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                    wdth = (float)gfx0.MeasureString("Name(ID): ".ToUpper(), xfont2).Width;

                    //Full Name
                    ght = (float)gfx0.MeasureString(
                      dtst.Tables[0].Rows[a][11].ToString() +
                      " (" + dtst.Tables[0].Rows[a][10].ToString() + ")", xfont41).Height;
                    tf = new XTextFormatter(gfx0);
                    rect = new XRect(startX + wdth + 10, startY + offsetY + 2, pageWidth, ght);
                    gfx0.DrawRectangle(XBrushes.White, rect);
                    tf.DrawString(dtst.Tables[0].Rows[a][11].ToString() +
                      " (" + dtst.Tables[0].Rows[a][10].ToString() + ")"
                      , xfont41, XBrushes.Black, rect, XStringFormats.TopLeft);

                    offsetY += (int)ght + 5;

                    ght = (float)gfx0.MeasureString(
                      ("Date: ").ToUpper(), xfont2).Height;
                    tf = new XTextFormatter(gfx0);
                    rect = new XRect(startX, startY + offsetY, pageWidth, ght);
                    gfx0.DrawRectangle(XBrushes.White, rect);
                    tf.DrawString(("Date: ").ToUpper()
                      , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                    wdth = (float)gfx0.MeasureString("Date: ".ToUpper(), xfont2).Width;

                    //Date
                    ght = (float)gfx0.MeasureString(
                      dtst.Tables[0].Rows[a][4].ToString(), xfont41).Height;
                    tf = new XTextFormatter(gfx0);
                    rect = new XRect(startX + wdth + 10, startY + offsetY + 2, pageWidth, ght);
                    gfx0.DrawRectangle(XBrushes.White, rect);
                    tf.DrawString(dtst.Tables[0].Rows[a][4].ToString()
                      , xfont41, XBrushes.Black, rect, XStringFormats.TopLeft);

                    if (dtst.Tables[0].Rows[a][15].ToString() != "-"
                      && dtst.Tables[0].Rows[a][15].ToString() != "")
                    {
                        offsetY += (int)ght + 5;
                        ght = (float)gfx0.MeasureString(
                        ("Job: ").ToUpper(), xfont2).Height;
                        tf = new XTextFormatter(gfx0);
                        rect = new XRect(startX, startY + offsetY, pageWidth, ght);
                        gfx0.DrawRectangle(XBrushes.White, rect);
                        tf.DrawString(("Job: ").ToUpper()
                          , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                        wdth = (float)gfx0.MeasureString("Job: ".ToUpper(), xfont2).Width;

                        //Full Name
                        ght = (float)gfx0.MeasureString(
                          dtst.Tables[0].Rows[a][15].ToString(), xfont41).Height;
                        tf = new XTextFormatter(gfx0);
                        rect = new XRect(startX + wdth + 10, startY + offsetY + 2, pageWidth, ght);
                        gfx0.DrawRectangle(XBrushes.White, rect);
                        tf.DrawString(dtst.Tables[0].Rows[a][15].ToString()
                          , xfont41, XBrushes.Black, rect, XStringFormats.TopLeft);

                    }
                    if (dtst.Tables[0].Rows[a][14].ToString() != "-"
                      && dtst.Tables[0].Rows[a][14].ToString() != "")
                    {
                        offsetY += (int)ght + 5;
                        ght = (float)gfx0.MeasureString(
                        ("Grade: ").ToUpper(), xfont2).Height;
                        tf = new XTextFormatter(gfx0);
                        rect = new XRect(startX, startY + offsetY, pageWidth, ght);
                        gfx0.DrawRectangle(XBrushes.White, rect);
                        tf.DrawString(("Grade: ").ToUpper()
                          , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                        wdth = (float)gfx0.MeasureString("Grade: ".ToUpper(), xfont2).Width;

                        //Full Name
                        ght = (float)gfx0.MeasureString(
                          dtst.Tables[0].Rows[a][14].ToString(), xfont41).Height;
                        tf = new XTextFormatter(gfx0);
                        rect = new XRect(startX + wdth + 10, startY + offsetY + 2, pageWidth, ght);
                        gfx0.DrawRectangle(XBrushes.White, rect);
                        tf.DrawString(dtst.Tables[0].Rows[a][14].ToString()
                          , xfont41, XBrushes.Black, rect, XStringFormats.TopLeft);
                    }

                    if (dtst.Tables[0].Rows[a][16].ToString() != "-"
                      && dtst.Tables[0].Rows[a][16].ToString() != "")
                    {
                        offsetY += (int)ght + 5;
                        ght = (float)gfx0.MeasureString(
                        ("Position: ").ToUpper(), xfont2).Height;
                        tf = new XTextFormatter(gfx0);
                        rect = new XRect(startX, startY + offsetY, pageWidth, ght);
                        gfx0.DrawRectangle(XBrushes.White, rect);
                        tf.DrawString(("Position: ").ToUpper()
                          , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                        wdth = (float)gfx0.MeasureString("Position: ".ToUpper(), xfont2).Width;

                        //Full Name
                        ght = (float)gfx0.MeasureString(
                          dtst.Tables[0].Rows[a][16].ToString(), xfont41).Height;
                        tf = new XTextFormatter(gfx0);
                        rect = new XRect(startX + wdth + 10, startY + offsetY + 2, pageWidth, ght);
                        gfx0.DrawRectangle(XBrushes.White, rect);
                        tf.DrawString(dtst.Tables[0].Rows[a][16].ToString()
                          , xfont41, XBrushes.Black, rect, XStringFormats.TopLeft);

                    }

                    if (dtst.Tables[0].Rows[a][17].ToString() != "-"
                      && dtst.Tables[0].Rows[a][17].ToString() != "")
                    {
                        offsetY += (int)ght + 5;
                        ght = (float)gfx0.MeasureString(
                        ("SSNIT No.: ").ToUpper(), xfont2).Height;
                        tf = new XTextFormatter(gfx0);
                        rect = new XRect(startX, startY + offsetY, pageWidth, ght);
                        gfx0.DrawRectangle(XBrushes.White, rect);
                        tf.DrawString(("SSNIT No.: ").ToUpper()
                          , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                        wdth = (float)gfx0.MeasureString("SSNIT No.: ".ToUpper(), xfont2).Width;

                        //Full Name
                        ght = (float)gfx0.MeasureString(
                          dtst.Tables[0].Rows[a][17].ToString(), xfont41).Height;
                        tf = new XTextFormatter(gfx0);
                        rect = new XRect(startX + wdth + 10, startY + offsetY + 2, pageWidth, ght);
                        gfx0.DrawRectangle(XBrushes.White, rect);
                        tf.DrawString(dtst.Tables[0].Rows[a][17].ToString()
                          , xfont41, XBrushes.Black, rect, XStringFormats.TopLeft);

                    }
                    if (dtst.Tables[0].Rows[a][18].ToString() != "-"
                      && dtst.Tables[0].Rows[a][18].ToString() != "")
                    {
                        offsetY += (int)ght + 5;
                        ght = (float)gfx0.MeasureString(
                        ("Bank (Branch): ").ToUpper(), xfont2).Height;
                        tf = new XTextFormatter(gfx0);
                        rect = new XRect(startX, startY + offsetY, pageWidth, ght);
                        gfx0.DrawRectangle(XBrushes.White, rect);
                        tf.DrawString(("Bank (Branch): ").ToUpper()
                          , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                        wdth = (float)gfx0.MeasureString("Bank (Branch): ".ToUpper(), xfont2).Width;

                        //Full Name
                        ght = (float)gfx0.MeasureString(
                          dtst.Tables[0].Rows[a][18].ToString(), xfont41).Height;
                        tf = new XTextFormatter(gfx0);
                        rect = new XRect(startX + wdth + 10, startY + offsetY + 2, pageWidth, ght);
                        gfx0.DrawRectangle(XBrushes.White, rect);
                        tf.DrawString(dtst.Tables[0].Rows[a][18].ToString()
                          , xfont41, XBrushes.Black, rect, XStringFormats.TopLeft);
                    }
                    if (dtst.Tables[0].Rows[a][19].ToString() != "-"
                      && dtst.Tables[0].Rows[a][19].ToString() != "")
                    {
                        offsetY += (int)ght + 5;
                        ght = (float)gfx0.MeasureString(
                        ("Account: ").ToUpper(), xfont2).Height;
                        tf = new XTextFormatter(gfx0);
                        rect = new XRect(startX, startY + offsetY, pageWidth, ght);
                        gfx0.DrawRectangle(XBrushes.White, rect);
                        tf.DrawString(("Account: ").ToUpper()
                          , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                        wdth = (float)gfx0.MeasureString("Account: ".ToUpper(), xfont2).Width;

                        //Full Name
                        ght = (float)gfx0.MeasureString(
                          dtst.Tables[0].Rows[a][19].ToString(), xfont41).Height;
                        tf = new XTextFormatter(gfx0);
                        rect = new XRect(startX + wdth + 10, startY + offsetY + 2, pageWidth, ght);
                        gfx0.DrawRectangle(XBrushes.White, rect);
                        tf.DrawString(dtst.Tables[0].Rows[a][19].ToString()
                          , xfont41, XBrushes.Black, rect, XStringFormats.TopLeft);
                    }
                    offsetY += (int)ght + 5;
                    gfx0.DrawLine(aPen, startX, startY + offsetY, startX + pageWidth - 40,
               startY + offsetY);
                    offsetY += font2Hght;
                    //offsetY += font2Hght;

                    ght = (float)gfx0.MeasureString(
                      (hdrs[0]).ToUpper(), xfont2).Height;
                    wdth = (float)(hdrs[0].Length * 5);
                    tf = new XTextFormatter(gfx0);
                    rect = new XRect(startX, startY + offsetY, pageWidth, ght);
                    gfx0.DrawRectangle(XBrushes.White, rect);
                    tf.DrawString((hdrs[0]).ToUpper()
                      , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                    //wdth = (float)g.MeasureString(hdrs[0].ToUpper(), font2).Width;

                    ght = (float)gfx0.MeasureString(
                      hdrs[1].ToUpper(), xfont2).Height;
                    tf = new XTextFormatter(gfx0);
                    rect = new XRect(startX + wdth + 10, startY + offsetY, pageWidth, ght);
                    gfx0.DrawRectangle(XBrushes.White, rect);
                    tf.DrawString(hdrs[1].ToUpper()
                      , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);

                    offsetY += (int)ght + 5;
                }

                if (dtst.Tables[0].Rows[a][13].ToString() != lastItmTyp)
                {
                    if (lastItmTyp != "")
                    {
                        itmTypIdx++;
                    }
                    if (itmTypIdx > 0)
                    {
                        startX = startXNw;
                        orgoffsetY = offsetY;
                        string txt = itmTypes[itmTypIdx - 1];
                        if (txt == "Purely Informational")
                        {
                            txt = "Amount";
                        }

                        wdth = (float)(hdrs[0].Length * 5);
                        nwLn = Global.mnFrm.cmCde.breakTxtDown(
             "Total " + txt, (int)(wdth * 1.3), font31, g);
                        ght = (float)gfx0.MeasureString(
                       finlStr, xfont31).Height;

                        tf = new XTextFormatter(gfx0);
                        rect = new XRect(startX, startY + offsetY, wdth + 5, ght);
                        gfx0.DrawRectangle(XBrushes.White, rect);
                        tf.DrawString("Total " + txt
                          , xfont31, XBrushes.Black, rect, XStringFormats.TopLeft);
                        startX += wdth + 10;

                        //wdth = (float)(hdrs[0].Length * 5);
                        nwLn = Global.mnFrm.cmCde.breakTxtDown(
            itmTypeTtls[itmTypIdx - 1].ToString("#,#0.00"), (int)(wdth * 1.3), font31, g);
                        finlStr = "";
                        finlStr = string.Join("\n", nwLn).PadLeft(10);
                        ght = (float)gfx0.MeasureString(
                       finlStr, xfont31).Height;

                        tf = new XTextFormatter(gfx0);
                        rect = new XRect(startX, startY + offsetY, wdth + 5, ght);
                        gfx0.DrawRectangle(XBrushes.White, rect);
                        tf.DrawString(" = " + finlStr
                          , xfont31, XBrushes.Black, rect, XStringFormats.TopLeft);

                        offsetY += (int)ght + 5;
                        //itmTypIdx++;
                        //itmTypeTtls[itmTypIdx] = 0;
                    }
                    startX = startXNw;

                    wdth = (float)(hdrs[0].Length * 5);
                    nwLn = Global.mnFrm.cmCde.breakTxtDown(
          dtst.Tables[0].Rows[a][13].ToString(), (int)(wdth * 1.3), font11, g);
                    finlStr = "";
                    finlStr = string.Join("\n", nwLn);
                    ght = (float)gfx0.MeasureString(
                   finlStr, xfont11).Height;

                    tf = new XTextFormatter(gfx0);
                    rect = new XRect(startX, startY + offsetY, wdth + 5, ght);
                    gfx0.DrawRectangle(XBrushes.White, rect);
                    tf.DrawString(finlStr
                      , xfont11, XBrushes.Black, rect, XStringFormats.TopLeft);

                    offsetY += (int)ght + 5;

                    itmTypes[itmTypIdx] = dtst.Tables[0].Rows[a][13].ToString();
                    lastItmTyp = dtst.Tables[0].Rows[a][13].ToString();
                    itmTypeTtls[itmTypIdx] += double.Parse(dtst.Tables[0].Rows[a][3].ToString());
                }
                else
                {
                    itmTypes[itmTypIdx] = dtst.Tables[0].Rows[a][13].ToString();
                    lastItmTyp = dtst.Tables[0].Rows[a][13].ToString();
                    itmTypeTtls[itmTypIdx] += double.Parse(dtst.Tables[0].Rows[a][3].ToString());
                }
                //Item
                startX = 45;
                orgoffsetY = offsetY;
                wdth = (float)(hdrs[0].Length * 5);
                nwLn = Global.mnFrm.cmCde.breakTxtDown(
        dtst.Tables[0].Rows[a][12].ToString(), (int)(wdth * 1.3), font41, g);
                finlStr = "";
                finlStr = string.Join("\n", nwLn);
                ght = (float)gfx0.MeasureString(
               finlStr, xfont41).Height;

                tf = new XTextFormatter(gfx0);
                rect = new XRect(startX, startY + offsetY, wdth + 5, ght);
                gfx0.DrawRectangle(XBrushes.White, rect);
                tf.DrawString(finlStr
                  , xfont41, XBrushes.Black, rect, XStringFormats.TopLeft);

                nwLn = Global.mnFrm.cmCde.breakTxtDown(
        double.Parse(dtst.Tables[0].Rows[a][3].ToString()).ToString("#,#0.00"), (int)(wdth * 1.3), font41, g);
                finlStr = "";
                finlStr = string.Join("\n", nwLn).PadLeft(12);
                ght = (float)gfx0.MeasureString(
               finlStr, xfont41).Height;

                tf = new XTextFormatter(gfx0);
                rect = new XRect(startX + wdth + 10, startY + offsetY, wdth + 5, ght);
                gfx0.DrawRectangle(XBrushes.White, rect);
                tf.DrawString(finlStr
                  , xfont41, XBrushes.Black, rect, XStringFormats.TopLeft);

                offsetY += (int)(ght * 1.0F) + 10;

                this.prntIdx++;
                this.pageNo++;
                //if (a > this.prntIdx)
                //{
                if (this.prntIdx < dtst.Tables[0].Rows.Count)
                {
                    if (dtst.Tables[0].Rows[this.prntIdx - 1][10].ToString() !=
                      dtst.Tables[0].Rows[this.prntIdx][10].ToString())
                    {
                        if (lastItmTyp != "")
                        {
                            itmTypIdx++;
                        }
                        if (itmTypIdx > 0)
                        {
                            orgoffsetY = offsetY;
                            string txt = itmTypes[itmTypIdx - 1];
                            if (txt == "Purely Informational")
                            {
                                txt = "Amount";
                            }
                            wdth = (float)(hdrs[0].Length * 5);
                            startX = startXNw;
                            nwLn = Global.mnFrm.cmCde.breakTxtDown(
                 "Total " + txt, (int)(wdth * 1.3), font31, g);
                            ght = (float)gfx0.MeasureString(
                           finlStr, xfont31).Height;

                            tf = new XTextFormatter(gfx0);
                            rect = new XRect(startX, startY + offsetY, wdth + 5, ght);
                            gfx0.DrawRectangle(XBrushes.White, rect);
                            tf.DrawString("Total " + txt
                              , xfont31, XBrushes.Black, rect, XStringFormats.TopLeft);
                            startX += wdth + 10;

                            //wdth = (float)(hdrs[0].Length * 5);
                            nwLn = Global.mnFrm.cmCde.breakTxtDown(
                itmTypeTtls[itmTypIdx - 1].ToString("#,#0.00"), (int)(wdth * 1.3), font31, g);
                            finlStr = "";
                            finlStr = string.Join("\n", nwLn).PadLeft(10);
                            ght = (float)gfx0.MeasureString(
                           finlStr, xfont31).Height;

                            tf = new XTextFormatter(gfx0);
                            rect = new XRect(startX, startY + offsetY, wdth + 5, ght);
                            gfx0.DrawRectangle(XBrushes.White, rect);
                            tf.DrawString(" = " + finlStr
                              , xfont31, XBrushes.Black, rect, XStringFormats.TopLeft);

                            offsetY += (int)ght + 5;

                            //itmTypeTtls[itmTypIdx] = 0;
                            for (int y = 0; y < 7; y++)
                            {
                                if (itmTypes[y] == "Earnings")
                                {
                                    netPay += itmTypeTtls[y];
                                    hsErn = true;
                                }
                                else if (itmTypes[y] == "Deductions"
                                  || itmTypes[y] == "Deductions"
                                  || itmTypes[y] == "Bills/Charges"
                                  || itmTypes[y] == "Deductions")
                                {
                                    netPay -= itmTypeTtls[y];
                                    hsDeduct = true;
                                }
                            }
                            if (hsErn == true || hsDeduct == true && itmTypIdx > 1)
                            {
                                string ttlStr = "Overall Total Amount";
                                if (hsErn == true && hsDeduct == true)
                                {
                                    ttlStr = "Net Payment";
                                }
                                offsetY += font3Hght;
                                orgoffsetY = offsetY;

                                wdth = (float)(hdrs[0].Length * 5);
                                startX = startXNw;
                                nwLn = Global.mnFrm.cmCde.breakTxtDown(
                     ttlStr, (int)(wdth * 1.3), font31, g);
                                ght = (float)gfx0.MeasureString(
                               finlStr, xfont31).Height;

                                tf = new XTextFormatter(gfx0);
                                rect = new XRect(startX, startY + offsetY, wdth + 5, ght);
                                gfx0.DrawRectangle(XBrushes.White, rect);
                                tf.DrawString(ttlStr
                                  , xfont31, XBrushes.Black, rect, XStringFormats.TopLeft);
                                startX += wdth + 10;

                                //wdth = (float)(hdrs[0].Length * 5);
                                nwLn = Global.mnFrm.cmCde.breakTxtDown(
                    netPay.ToString("#,#0.00"), (int)(wdth * 1.3), font31, g);
                                finlStr = "";
                                finlStr = string.Join("\n", nwLn).PadLeft(10);
                                ght = (float)gfx0.MeasureString(
                               finlStr, xfont31).Height;

                                tf = new XTextFormatter(gfx0);
                                rect = new XRect(startX, startY + offsetY, wdth + 5, ght);
                                gfx0.DrawRectangle(XBrushes.White, rect);
                                tf.DrawString(" = " + finlStr
                                  , xfont31, XBrushes.Black, rect, XStringFormats.TopLeft);

                                offsetY += (int)ght + 5;

                            }
                        }
                        //this.prntIdx = a;
                        //Slogan: 
                        offsetY += font3Hght;
                        offsetY += font3Hght;
                        if (hghstght < 10)
                        {
                            hghstght = 10;
                        }
                        offsetY += hghstght + 5;
                        if (hgstOffsetY < offsetY)
                        {
                            hgstOffsetY = offsetY;
                        }
                        //if ((startY + offsetY) >= 750)
                        //{

                        //Slogan: 
                        startX = startXNw;
                        if (offsetY < 705)
                        {
                            offsetY = 705;
                        }
                        gfx0.DrawLine(aPen, startX, startY + offsetY, startX + pageWidth - 40,
                    startY + offsetY);
                        offsetY += font3Hght;
                        nwLn = Global.mnFrm.cmCde.breakRptTxtDown(
                          Global.mnFrm.cmCde.getOrgName(Global.mnFrm.cmCde.Org_id) + "..." +
                          Global.mnFrm.cmCde.getOrgSlogan(Global.mnFrm.cmCde.Org_id),
                    pageWidth - ght, font5, g);
                        for (int i = 0; i < nwLn.Length; i++)
                        {
                            gfx0.DrawString(nwLn[i]
                            , xfont5, XBrushes.Black, startX, startY + offsetY);
                            offsetY += font5Hght;
                        }
                        offsetY += font5Hght;
                        nwLn = Global.mnFrm.cmCde.breakRptTxtDown(
                         "Software Developed by Rhomicom Systems Technologies Ltd.",
                    pageWidth + 40, font5, g);
                        for (int i = 0; i < nwLn.Length; i++)
                        {
                            gfx0.DrawString(nwLn[i]
                            , xfont5, XBrushes.Black, startX, startY + offsetY);
                            offsetY += font5Hght;
                        }
                        nwLn = Global.mnFrm.cmCde.breakRptTxtDown(
                    "Website:www.rhomicomgh.com",
                    pageWidth + 40, font5, g);
                        for (int i = 0; i < nwLn.Length; i++)
                        {
                            gfx0.DrawString(nwLn[i]
                            , xfont5, XBrushes.Black, startX, startY + offsetY);
                            offsetY += font5Hght;
                        }

                        page0 = document.AddPage();
                        page0.Orientation = PageOrientation.Portrait;
                        page0.Height = XUnit.FromInch(11);
                        page0.Width = XUnit.FromInch(8.5);
                        gfx0 = XGraphics.FromPdfPage(page0);
                        offsetY = 0;
                        hgstOffsetY = 0;
                        this.pageNo = 1;
                        itmTypIdx = 0;
                        if (hghstght < 10)
                        {
                            hghstght = 10;
                        }
                        //offsetY += hghstght + 5;
                        if (hgstOffsetY < offsetY)
                        {
                            hgstOffsetY = offsetY;
                        }
                        if ((startY + offsetY) >= 750)
                        {
                            page0 = document.AddPage();
                            page0.Orientation = PageOrientation.Portrait;
                            page0.Height = XUnit.FromInch(11);
                            page0.Width = XUnit.FromInch(8.5);
                            gfx0 = XGraphics.FromPdfPage(page0);
                            offsetY = 0;
                            hgstOffsetY = 0;
                        }
                        continue;
                        //}            
                    }
                }
                //}
                if (hghstght < 10)
                {
                    hghstght = 10;
                }
                //offsetY += hghstght + 5;
                if (hgstOffsetY < offsetY)
                {
                    hgstOffsetY = offsetY;
                }
                if ((startY + offsetY) >= 750)
                {
                    page0 = document.AddPage();
                    page0.Orientation = PageOrientation.Portrait;
                    page0.Height = XUnit.FromInch(11);
                    page0.Width = XUnit.FromInch(8.5);
                    gfx0 = XGraphics.FromPdfPage(page0);
                    offsetY = 0;
                    hgstOffsetY = 0;
                }
            }

            if (lastItmTyp != "")
            {
                itmTypIdx++;
            }

            if (itmTypIdx > 0)
            {
                orgoffsetY = offsetY;
                string txt = itmTypes[itmTypIdx - 1];
                if (txt == "Purely Informational")
                {
                    txt = "Amount";
                }
                wdth = (float)(hdrs[0].Length * 5);
                startX = startXNw;
                nwLn = Global.mnFrm.cmCde.breakTxtDown(
        "Total " + txt, (int)(wdth * 1.3), font31, g);
                ght = (float)gfx0.MeasureString(
               finlStr, xfont31).Height;

                tf = new XTextFormatter(gfx0);
                rect = new XRect(startX, startY + offsetY, wdth + 5, ght);
                gfx0.DrawRectangle(XBrushes.White, rect);
                tf.DrawString("Total " + txt
                  , xfont31, XBrushes.Black, rect, XStringFormats.TopLeft);
                startX += wdth + 10;

                //wdth = (float)(hdrs[0].Length * 5);
                nwLn = Global.mnFrm.cmCde.breakTxtDown(
        itmTypeTtls[itmTypIdx - 1].ToString("#,#0.00"), (int)(wdth * 1.3), font31, g);
                finlStr = "";
                finlStr = string.Join("\n", nwLn).PadLeft(10);
                ght = (float)gfx0.MeasureString(
               finlStr, xfont31).Height;

                tf = new XTextFormatter(gfx0);
                rect = new XRect(startX, startY + offsetY, wdth + 5, ght);
                gfx0.DrawRectangle(XBrushes.White, rect);
                tf.DrawString(" = " + finlStr
                  , xfont31, XBrushes.Black, rect, XStringFormats.TopLeft);

                offsetY += (int)ght + 5;
                //itmTypeTtls[itmTypIdx] = 0;
                for (int y = 0; y < 7; y++)
                {
                    if (itmTypes[y] == "Earnings")
                    {
                        netPay += itmTypeTtls[y];
                        hsErn = true;
                    }
                    else if (itmTypes[y] == "Deductions"
                      || itmTypes[y] == "Deductions"
                      || itmTypes[y] == "Bills/Charges"
                      || itmTypes[y] == "Deductions")
                    {
                        netPay -= itmTypeTtls[y];
                        hsDeduct = true;
                    }
                }
                if (hsErn == true || hsDeduct == true && itmTypIdx > 1)
                {
                    string ttlStr = "Overall Total Amount";
                    if (hsErn == true && hsDeduct == true)
                    {
                        ttlStr = "Net Payment";
                    }
                    offsetY += font3Hght;
                    orgoffsetY = offsetY;
                    startX = startXNw;

                    wdth = (float)(hdrs[0].Length * 5);
                    nwLn = Global.mnFrm.cmCde.breakTxtDown(
          ttlStr, (int)(wdth * 1.3), font31, g);
                    ght = (float)gfx0.MeasureString(
                   finlStr, xfont31).Height;

                    tf = new XTextFormatter(gfx0);
                    rect = new XRect(startX, startY + offsetY, wdth + 5, ght);
                    gfx0.DrawRectangle(XBrushes.White, rect);
                    tf.DrawString(ttlStr
                      , xfont31, XBrushes.Black, rect, XStringFormats.TopLeft);
                    startX += wdth + 10;

                    //wdth = (float)(hdrs[0].Length * 5);
                    nwLn = Global.mnFrm.cmCde.breakTxtDown(
          netPay.ToString("#,#0.00"), (int)(wdth * 1.3), font31, g);
                    finlStr = "";
                    finlStr = string.Join("\n", nwLn).PadLeft(10);
                    ght = (float)gfx0.MeasureString(
                   finlStr, xfont31).Height;

                    tf = new XTextFormatter(gfx0);
                    rect = new XRect(startX, startY + offsetY, wdth + 5, ght);
                    gfx0.DrawRectangle(XBrushes.White, rect);
                    tf.DrawString(" = " + finlStr
                      , xfont31, XBrushes.Black, rect, XStringFormats.TopLeft);

                    offsetY += (int)ght + 5;
                }
            }
            offsetY += hghstght + 5;
            //Slogan: 
            startX = startXNw;
            if (offsetY < 705)
            {
                offsetY = 705;
            }
            gfx0.DrawLine(aPen, startX, startY + offsetY, startX + pageWidth - 40,
        startY + offsetY);
            offsetY += font3Hght;
            nwLn = Global.mnFrm.cmCde.breakRptTxtDown(
              Global.mnFrm.cmCde.getOrgName(Global.mnFrm.cmCde.Org_id) + "..." +
              Global.mnFrm.cmCde.getOrgSlogan(Global.mnFrm.cmCde.Org_id),
        pageWidth - ght, font5, g);
            for (int i = 0; i < nwLn.Length; i++)
            {
                gfx0.DrawString(nwLn[i]
                , xfont5, XBrushes.Black, startX, startY + offsetY);
                offsetY += font5Hght;
            }
            offsetY += font5Hght;
            nwLn = Global.mnFrm.cmCde.breakRptTxtDown(
             "Software Developed by Rhomicom Systems Technologies Ltd.",
        pageWidth + 40, font5, g);
            for (int i = 0; i < nwLn.Length; i++)
            {
                gfx0.DrawString(nwLn[i]
                , xfont5, XBrushes.Black, startX, startY + offsetY);
                offsetY += font5Hght;
            }
            nwLn = Global.mnFrm.cmCde.breakRptTxtDown(
        "Website:www.rhomicomgh.com",
        pageWidth + 40, font5, g);
            for (int i = 0; i < nwLn.Length; i++)
            {
                gfx0.DrawString(nwLn[i]
                , xfont5, XBrushes.Black, startX, startY + offsetY);
                offsetY += font5Hght;
            }
            // Create second page for additional person details
            /*PdfPage page1 = document.AddPage();
            XGraphics gfx1 = XGraphics.FromPdfPage(page1);
            XFont xfont1 = new XFont("Verdana", 20, XFontStyle.BoldItalic);
            gfx0.DrawString("Page 2!" + this.locIDTextBox.Text, xfont1, XBrushes.Black,
              new XRect(100, 100, page1.Width, page1.Height),
              XStringFormats.TopLeft);*/



            // Save the document...
            string filename = Global.mnFrm.cmCde.getRptDrctry() + @"\PayRunResults_" + Global.mnFrm.cmCde.getDB_Date_time().Replace(" ", "").Replace(":", "").Replace("-", "") + ".pdf";
            document.Save(filename);
            // ...and start a viewer.
            System.Diagnostics.Process.Start(filename);
            //Global.mnFrm.cmCde.upldImgsFTP(9, Global.mnFrm.cmCde.getRptDrctry(), @"\PayRunResults_" + Global.mnFrm.cmCde.getDB_Date_time().Replace(" ", "").Replace(":", "").Replace("-", "") + ".pdf");
            System.Windows.Forms.Application.DoEvents();

        }

        private void linkedFirmButton_Click(object sender, EventArgs e)
        {
            if (this.editPrsButton.Text == "EDIT")
            {
                this.editPrsButton.PerformClick();
            }
            if (this.editPrsn == false && this.addPrsn == false)
            {
                Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
                return;
            }
            long cstspplID = long.Parse(this.linkedFirmIDTextBox.Text);
            long siteID = long.Parse(this.linkedSiteIDTextBox.Text);
            bool isReadOnly = true;
            if (this.addPrsn || this.editPrsn)
            {
                isReadOnly = false;
            }
            Global.mnFrm.cmCde.showCstSpplrDiag(ref cstspplID, ref siteID, true, false, this.srchWrd,
              "Customer/Supplier Name", false, isReadOnly, Global.mnFrm.cmCde, "");
            this.linkedFirmIDTextBox.Text = cstspplID.ToString();
            this.linkedSiteIDTextBox.Text = siteID.ToString();
            this.linkedFirmTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm(
                "scm.scm_cstmr_suplr", "cust_sup_id", "cust_sup_name",
                cstspplID);

            this.linkedSiteTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm(
                  "scm.scm_cstmr_suplr_sites", "cust_sup_site_id", "site_name",
                  siteID);

            if (this.resAddrsTextBox.Text == "")
            {
                this.resAddrsTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm(
                "scm.scm_cstmr_suplr_sites", "cust_sup_site_id", "ship_to_address",
                siteID);
            }
            if (this.pstlAddrsTextBox.Text == "")
            {
                this.pstlAddrsTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm(
                "scm.scm_cstmr_suplr_sites", "cust_sup_site_id", "billing_address",
                siteID);
            }
            //string[] selVals = new string[1];
            //selVals[0] = this.linkedFirmIDTextBox.Text;
            //string extrWhr = " and tbl1.e <=0";
            //DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
            // Global.mnFrm.cmCde.getLovID("All Customers and Suppliers"), ref selVals, true, false,
            // Global.mnFrm.cmCde.Org_id, "", "",
            // this.srchWrd, "Both", true, extrWhr);
            //if (dgRes == DialogResult.OK)
            //{
            //  for (int i = 0; i < selVals.Length; i++)
            //  {
            //    this.linkedFirmIDTextBox.Text = selVals[i];
            //    this.linkedFirmTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_cstmr_suplr",
            //      "cust_sup_id", "cust_sup_name", long.Parse(selVals[i]));
            //    this.linkedSiteIDTextBox.Text = "-1";
            //    this.linkedSiteTextBox.Text = "";
            //  }
            //}
        }

        private void linkedSiteButton_Click(object sender, EventArgs e)
        {
            //Customer/Supplier Sites
            if (this.editPrsButton.Text == "EDIT")
            {
                this.editPrsButton.PerformClick();
            }
            if (this.editPrsn == false && this.addPrsn == false)
            {
                Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
                return;
            }
            if (this.linkedFirmIDTextBox.Text == "" || this.linkedFirmIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please pick a Firm/Workplace First!", 0);
                return;
            }

            string[] selVals = new string[1];
            selVals[0] = this.linkedSiteIDTextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
              Global.mnFrm.cmCde.getLovID("Customer/Supplier Sites"), ref selVals,
              true, true, int.Parse(this.linkedFirmIDTextBox.Text),
             srchWrd, "Both", true);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.linkedSiteIDTextBox.Text = selVals[i];
                    this.linkedSiteTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm(
                      "scm.scm_cstmr_suplr_sites", "cust_sup_site_id", "site_name",
                      long.Parse(selVals[i]));
                    if (this.resAddrsTextBox.Text == "")
                    {
                        this.resAddrsTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm(
                        "scm.scm_cstmr_suplr_sites", "cust_sup_site_id", "ship_to_address",
                        long.Parse(selVals[i]));
                    }
                    if (this.pstlAddrsTextBox.Text == "")
                    {
                        this.pstlAddrsTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm(
                        "scm.scm_cstmr_suplr_sites", "cust_sup_site_id", "billing_address",
                        long.Parse(selVals[i]));
                    }
                }
            }
        }

        private void emailButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSendMailDiag(long.Parse(this.prsnIDTextBox.Text), Global.mnFrm.cmCde, "");
            //sendMailDiag nwDiag = new sendMailDiag();
            //if (nwDiag.ShowDialog() == DialogResult.OK)
            //{
            //}
        }

        private void qrCodeButton_Click(object sender, EventArgs e)
        {
            qrCodeDiag nwDiag = new qrCodeDiag();
            string txtEncodeData = "";
            if (this.bizCrdRadioButton.Checked)
            {
                txtEncodeData = "BEGIN:VCARD" + "\n";
                txtEncodeData += "VERSION:2.1" + "\n";
                txtEncodeData += "N:" + this.surnameTextBox.Text + "\n";
                txtEncodeData += "FN:" + this.titleTextBox.Text + " " + this.firstNameTextBox.Text + " " +
                  this.otherNamesTextBox.Text + " " + this.surnameTextBox.Text + " (" + this.locIDTextBox.Text + ") \n";
                txtEncodeData += "ORG:" + this.linkedFirmTextBox.Text + "\n";

                txtEncodeData += "TEL;WORK;VOICE:" + this.telTextBox.Text + "\n";
                txtEncodeData += "TEL;TYPE=cell:" + this.moblTextBox.Text + "\n";

                txtEncodeData += "ADR;TYPE=work;" +
                    "LABEL=\"Our Office:" +
                    this.pstlAddrsTextBox.Text.Replace(Environment.NewLine, " ") + ";" +
                    this.resAddrsTextBox.Text.Replace(Environment.NewLine, " ") + "\n";

                txtEncodeData += "EMAIL:" + this.emailTextBox.Text + "\n";
                txtEncodeData += "URL;TYPE=work:" + Global.mnFrm.cmCde.getEnbldPssblValDesc("QR Code Validation URL",
        Global.mnFrm.cmCde.getEnbldLovID("Universal Resource Locators (URLs)")) + this.locIDTextBox.Text + "\n";
                txtEncodeData += "END:VCARD";

            }
            else
            {
                txtEncodeData = Global.mnFrm.cmCde.getEnbldPssblValDesc("QR Code Validation URL",
                Global.mnFrm.cmCde.getEnbldLovID("Universal Resource Locators (URLs)")) + this.locIDTextBox.Text;
            }
            //Global.mnFrm.cmCde.showSQLNoPermsn(txtEncodeData);
            if (txtEncodeData.Trim() == String.Empty)
            {
                MessageBox.Show("Data must not be empty.");
                return;
            }

            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            String encoding = "Byte";
            if (encoding == "Byte")
            {
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            }
            else if (encoding == "AlphaNumeric")
            {
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
            }
            else if (encoding == "Numeric")
            {
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.NUMERIC;
            }

            try
            {
                int scale = Convert.ToInt16("4");
                qrCodeEncoder.QRCodeScale = scale;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid size!");
                return;
            }
            try
            {
                int version = Convert.ToInt16("15");
                qrCodeEncoder.QRCodeVersion = version;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid version !");
            }

            string errorCorrect = "M";
            if (errorCorrect == "L")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
            else if (errorCorrect == "M")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            else if (errorCorrect == "Q")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
            else if (errorCorrect == "H")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;

            Image image;
            String data = txtEncodeData;
            image = qrCodeEncoder.Encode(data);
            this.qrCodePictureBox.Image = image;
            nwDiag.qrCodePictureBox.Image = image;
            nwDiag.ShowDialog();
            System.Windows.Forms.Application.DoEvents();
            //this.groupBox10.Size = new Size(this.qrCodePictureBox.Width + 22, this.qrCodePictureBox.Height + 32);
        }

        private void saveQRButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.saveImageToFile(ref this.qrCodePictureBox);
        }

        private void extInfoDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.Equals(null)
              || this.obeyEvnts == false
              || this.canEdit == false)
            {
                return;
            }

            if (e.RowIndex < 0)
            {
                return;
            }

            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[8]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }

            if (this.extInfoDataGridView.Rows[e.RowIndex].Cells[4].Value == null)
            {
                this.extInfoDataGridView.Rows[e.RowIndex].Cells[4].Value = "-1";
            }
            if (this.extInfoDataGridView.Rows[e.RowIndex].Cells[5].Value == null)
            {
                this.extInfoDataGridView.Rows[e.RowIndex].Cells[5].Value = "-1";
            }
            if (this.extInfoDataGridView.Rows[e.RowIndex].Cells[0].Value == null)
            {
                this.extInfoDataGridView.Rows[e.RowIndex].Cells[0].Value = string.Empty;
            }

            if (this.extInfoDataGridView.Rows[e.RowIndex].Cells[2].Value == null)
            {
                this.extInfoDataGridView.Rows[e.RowIndex].Cells[2].Value = string.Empty;
            }
            if (this.extInfoDataGridView.Rows[e.RowIndex].Cells[3].Value == null)
            {
                this.extInfoDataGridView.Rows[e.RowIndex].Cells[3].Value = string.Empty;
            }
            if (e.ColumnIndex == 1)
            {
                int[] selVals = new int[1];
                selVals[0] = Global.mnFrm.cmCde.getPssblValID(
                  this.extInfoDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString(),
                 Global.mnFrm.cmCde.getLovID("Extra Information Labels"));
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                 Global.mnFrm.cmCde.getLovID("Extra Information Labels"), ref selVals, true, false);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.extInfoDataGridView.Rows[e.RowIndex].Cells[0].Value = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
                    }
                }
            }
        }

        private void addExtraInfoButton_Click(object sender, EventArgs e)
        {
            this.obeyEvnts = false;
            this.extInfoDataGridView.Rows.Insert(0, 1);
            int idx = 0;

            this.extInfoDataGridView.Rows[idx].HeaderCell.Value = (idx + 1).ToString();
            Object[] cellDesc = new Object[6];
            cellDesc[0] = "";
            cellDesc[1] = "...";
            cellDesc[2] = "";
            cellDesc[3] = "";
            cellDesc[4] = "-1";
            cellDesc[5] = "-1";
            this.extInfoDataGridView.Rows[idx].SetValues(cellDesc);
            this.obeyEvnts = true;
        }

        private void delExtraInfoButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[9]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.addPrsn == true && (this.prsnIDTextBox.Text == "" || this.prsnIDTextBox.Text == "-1"))
            {
                Global.mnFrm.cmCde.showMsg("Please Save this Person First!", 0);
                return;
            }
            if (this.extInfoDataGridView.CurrentCell != null && this.extInfoDataGridView.SelectedRows.Count <= 0)
            {
                this.extInfoDataGridView.Rows[this.extInfoDataGridView.CurrentCell.RowIndex].Selected = true;
            }
            if (this.extInfoDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the Row(s) to delete!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to delete the\r\nselected Extra Information Record(s)?", 1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            for (int i = 0; i < this.extInfoDataGridView.SelectedRows.Count; i++)
            {
                Global.deleteExtraInfo(
                  long.Parse(this.extInfoDataGridView.SelectedRows[i].Cells[5].Value.ToString()),
                  this.locIDTextBox.Text);
            }
            this.populateValGridVw();
        }
    }
}
