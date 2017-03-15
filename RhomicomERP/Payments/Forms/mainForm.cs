using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using InternalPayments.Classes;
using InternalPayments.Dialogs;
using Npgsql;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.VisualBasic;
using System.IO;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Drawing.Layout;

namespace InternalPayments.Forms
{
    public partial class mainForm : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        #region "GLOBAL VARIABLES..."
        public CommonCode.CommonCodes cmCde = new CommonCode.CommonCodes();
        public CommonCode.CommonCodes cmCde1 = new CommonCode.CommonCodes();
        cadmaFunctions.NavFuncs myNav = new cadmaFunctions.NavFuncs();
        cadmaFunctions.NavFuncs myNav1 = new cadmaFunctions.NavFuncs();
        //public NpgsqlConnection gnrlSQLConn = new NpgsqlConnection();
        public Int64 usr_id = -1;
        public int[] role_st_id = new int[0];
        public Int64 lgn_num = -1;
        public int Og_id = -1;
        //string[] menuItems = {"Manual Payments", "Pay Item Sets", 
        //"Person Sets", "Mass Pay Runs", "Payment Transactions", "GL Interface Table"};
        //string[] menuImages = {"groupings.png", "staffs.png", "shield_64.png"
        //,"SecurityLock.png", "73.ico", "GeneralLedgerIcon1.png"};
        bool obey_evnts = false;
        public bool txtChngd = false;
        public string srchWrd = "%";

        string[] menuItems = {"Manual Payments", "Pay Item Sets",
        "Person Sets", "Mass Pay Runs", "Pay Transactions", "GL Interface Table", "Pay Items", "Global Values"};
        string[] menuImages = {"images (11).jpg", "download (12).jpg", "download (13).jpg"
        ,"download (6).jpg", "search_64.png", "GeneralLedgerIcon1.png", "images (13).jpg", "world_48.png"};

        //Benefits & Contributions Panel Variables;
        long itm_cur_indx1 = 0;
        bool is_last_itm1 = false;
        long totl_itm1 = 0;
        long last_itm_num1 = 0;
        public string itm_SQL1 = "";
        public string prsnitm_SQL1 = "";
        public string trnsDet_SQL1 = "";
        public string mspyAtchdVals_SQL1 = "";
        bool obey_itm_evnts1 = false;
        string curTabIndx = "";
        //Benefits & Contributions Panel Variables;
        long itm_cur_indx = 0;
        bool is_last_itm = false;
        long totl_itm = 0;
        long last_itm_num = 0;
        public string itm_SQL = "";
        public string itmPval_SQL = "";
        public string itmFeed_SQL = "";
        bool obey_itm_evnts = false;
        bool additm = false;
        bool edititm = false;
        bool additms = false;
        bool edititms = false;
        bool delitms = false;
        //Payitems
        long pyitm_cur_indx = 0;
        bool is_last_pyitm = false;
        long totl_pyitm = 0;
        long last_pyitm_num = 0;
        public string pyitm_SQL = "";
        bool obey_pyitm_evnts = false;
        //feeditems
        long feed_cur_indx = 0;
        bool is_last_feed = false;
        long totl_feed = 0;
        long last_feed_num = 0;
        public string feed_SQL = "";
        bool obey_feed_evnts = false;
        //Payitems
        public string bank_SQL = "";
        bool vwBanks = false;
        bool vwAllMsPays = false;
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

        //Org Persons Panel Variables;
        Int64 prs_cur_indx = 0;
        bool is_last_prs = false;
        Int64 totl_prs = 0;
        long last_prs_num = 0;
        public string prs_SQL = "";
        public string prsDet_SQL = "";
        bool obey_prs_evnts = false;
        //Past Payments Panel Variables;
        long pst_cur_indx = 0;
        bool is_last_pst = false;
        long totl_pst = 0;
        long last_pst_num = 0;
        public string pst_SQL = "";
        bool obey_pst_evnts = false;
        bool addMnlPys = false;
        bool sndMnlPy = false;
        bool rvrsMnlPys = false;
        //Transactions Search
        private long totl_trns = 0;
        private long cur_trns_idx = 0;
        //private string vwtrnsSQLStmnt = "";
        private bool is_last_trns = false;
        bool obeytrnsEvnts = false;
        long last_trns_num = 0;

        //Pay Item Sets;
        long itmst_cur_indx = 0;
        bool is_last_itmst = false;
        long totl_itmst = 0;
        long last_itmst_num = 0;
        public string itmst_SQL = "";
        bool obey_itmst_evnts = false;
        bool addItmSt = false;
        bool editItmSt = false;
        bool beenToCheckBx = false;

        bool addItmSts = false;
        bool editItmSts = false;
        bool delItmSts = false;

        //Pay Item Sets Details
        long idet_cur_indx = 0;
        bool is_last_idet = false;
        long totl_idet = 0;
        long last_idet_num = 0;
        public string idet_SQL = "";
        bool obey_idet_evnts = false;


        //Person Sets;
        long prsst_cur_indx = 0;
        bool is_last_prsst = false;
        long totl_prsst = 0;
        long last_prsst_num = 0;
        public string prsst_SQL = "";
        bool obey_prsst_evnts = false;
        bool addPrsSt = false;
        bool editPrsSt = false;

        bool addPrsSts = false;
        bool editPrsSts = false;
        bool delPrsSts = false;
        //Person Sets Details
        long prsdet_cur_indx = 0;
        bool is_last_prsdet = false;
        long totl_prsdet = 0;
        long last_prsdet_num = 0;
        public string prsdet_SQL = "";
        bool obey_prsdet_evnts = false;

        //Mass Pay Run;
        long mspy_cur_indx = 0;
        bool is_last_mspy = false;
        long totl_mspy = 0;
        long last_mspy_num = 0;
        public string mspy_SQL = "";
        bool obey_mspy_evnts = false;
        bool addMsPy = false;
        bool editMsPy = false;

        bool addMsPys = false;
        bool editMsPys = false;
        bool delMsPys = false;
        bool runMsPys = false;
        bool rollMsPys = false;
        bool sendMsPys = false;
        //Mass Pay Run Details
        long mspydt_cur_indx = 0;
        bool is_last_mspydt = false;
        long totl_mspydt = 0;
        long last_mspydt_num = 0;
        public string mspydt_SQL = "";
        bool obey_mspydt_evnts = false;

        //Transactions Search
        private long totl_Infc = 0;
        private long cur_Infc_idx = 0;
        public string vwInfcSQLStmnt = "";
        private bool is_last_Infc = false;
        bool obeyInfcEvnts = false;
        long last_Infc_num = 0;

        //Global Values;
        long gbv_cur_indx = 0;
        bool is_last_gbv = false;
        long totl_gbv = 0;
        long last_gbv_num = 0;
        public string gbv_SQL = "";
        bool obey_gbv_evnts = false;
        bool addgbv = false;
        bool editgbv = false;

        bool addgbvs = false;
        bool editgbvs = false;
        bool delgbvs = false;

        //Global Value Details
        long gbvdt_cur_indx = 0;
        bool is_last_gbvdt = false;
        long totl_gbvdt = 0;
        long last_gbvdt_num = 0;
        public string gbvdt_SQL = "";
        bool obey_gbvdt_evnts = false;

        #endregion
        #region "FORM EVENTS..."
        public mainForm()
        {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            this.accDndLabel.Visible = false;
            Global.myPay.Initialize();
            Global.mnFrm = this;
            //Global.mnFrm.cmCde.pgSqlConn = this.gnrlSQLConn;
            Global.mnFrm.cmCde.Login_number = this.lgn_num;
            Global.mnFrm.cmCde.Role_Set_IDs = this.role_st_id;
            Global.mnFrm.cmCde.User_id = this.usr_id;
            Global.mnFrm.cmCde.Org_id = this.Og_id;
            this.hideAllPanels();
            Global.refreshRqrdVrbls();

            Global3.myInv.Initialize();
            Global3.mnFrm = this;
            //Global.mnFrm.cmCde.pgSqlConn = this.gnrlSQLConn;
            Global3.mnFrm.cmCde1.Login_number = this.lgn_num;
            Global3.mnFrm.cmCde1.Role_Set_IDs = this.role_st_id;
            Global3.mnFrm.cmCde1.User_id = this.usr_id;
            Global3.mnFrm.cmCde1.Org_id = this.Og_id;
            Global3.refreshRqrdVrbls();

            this.storeIDTextBox.Text = Global3.getUserStoreID().ToString();
            Global3.selectedStoreID = int.Parse(this.storeIDTextBox.Text);

            this.storeNmTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm(
              "inv.inv_itm_subinventories", "subinv_id", "subinv_name",
              long.Parse(this.storeIDTextBox.Text));

            System.Windows.Forms.Application.DoEvents();
            Color[] clrs = Global.mnFrm.cmCde.getColors();
            this.BackColor = clrs[0];
            this.payTabPage.BackColor = clrs[0];
            this.asgdPyItmsTabPage.BackColor = clrs[0];
            this.prsBanksTabPage.BackColor = clrs[0];
            this.tabPage1.BackColor = clrs[0];
            this.tabPage2.BackColor = clrs[0];
            this.tabPage3.BackColor = clrs[0];
            this.tabPage4.BackColor = clrs[0];
            this.tabPage5.BackColor = clrs[0];
            this.tabPage6.BackColor = clrs[0];
            this.tabPage7.BackColor = clrs[0];
            this.tabPage8.BackColor = clrs[0];

            this.glsLabel1.TopFill = clrs[0];
            this.glsLabel1.BackColor = clrs[0];
            this.glsLabel1.BottomFill = clrs[1];
            this.glsLabel2.TopFill = clrs[0];
            this.glsLabel2.BackColor = clrs[0];
            this.glsLabel2.BottomFill = clrs[1];
            this.glsLabel3.TopFill = clrs[0];
            this.glsLabel3.BackColor = clrs[0];
            this.glsLabel3.BottomFill = clrs[1];
            this.glsLabel4.TopFill = clrs[0];
            this.glsLabel4.BackColor = clrs[0];
            this.glsLabel4.BottomFill = clrs[1];
            this.glsLabel5.TopFill = clrs[0];
            this.glsLabel5.BackColor = clrs[0];
            this.glsLabel5.BottomFill = clrs[1];
            this.glsLabel6.TopFill = clrs[0];
            this.glsLabel6.BackColor = clrs[0];
            this.glsLabel6.BottomFill = clrs[1];
            this.glsLabel7.TopFill = clrs[0];
            this.glsLabel7.BackColor = clrs[0];
            this.glsLabel7.BottomFill = clrs[1];
            this.glsLabel8.TopFill = clrs[0];
            this.glsLabel8.BackColor = clrs[0];
            this.glsLabel8.BottomFill = clrs[1];
            this.glsLabel9.TopFill = clrs[0];
            this.glsLabel9.BackColor = clrs[0];
            this.glsLabel9.BottomFill = clrs[1];
            this.glsLabel10.TopFill = clrs[0];
            this.glsLabel10.BackColor = clrs[0];
            this.glsLabel10.BottomFill = clrs[1];
            this.glsLabel11.TopFill = clrs[0];
            this.glsLabel11.BackColor = clrs[0];
            this.glsLabel11.BottomFill = clrs[1];
            this.glsLabel12.TopFill = clrs[0];
            this.glsLabel12.BackColor = clrs[0];
            this.glsLabel12.BottomFill = clrs[1];
            this.glsLabel13.TopFill = clrs[0];
            this.glsLabel13.BackColor = clrs[0];
            this.glsLabel13.BottomFill = clrs[1];
            System.Windows.Forms.Application.DoEvents();
            Global.myPay.loadMyRolesNMsgtyps();
            bool vwAct = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[0]);
            if (!vwAct)
            {
                this.Controls.Clear();
                this.Controls.Add(this.accDndLabel);
                this.accDndLabel.Visible = true;
                return;
            }
            this.disableFormButtons();
            this.showAllPanels();
            Global.createDfltSets();
            Global.createRqrdLOVs();
            Global.createRqrdItems();
            this.populateTreeView();
            this.vldStrtDteTextBox.Text = DateTime.ParseExact(
          Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
          System.Globalization.CultureInfo.InvariantCulture).AddMonths(-24).ToString("dd-MMM-yyyy HH:mm:ss");
            this.vldEndDteTextBox.Text = DateTime.ParseExact(
          Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
          System.Globalization.CultureInfo.InvariantCulture).AddMonths(24).ToString("dd-MMM-yyyy 00:00:00");
            this.infcDte1TextBox.Text = DateTime.ParseExact(
          Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
          System.Globalization.CultureInfo.InvariantCulture).AddMonths(-24).ToString("dd-MMM-yyyy HH:mm:ss");
            this.infcDte2TextBox.Text = DateTime.ParseExact(
          Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
          System.Globalization.CultureInfo.InvariantCulture).AddMonths(24).ToString("dd-MMM-yyyy 00:00:00");
            this.infoToolTip.SetToolTip(this.locClassTextBox, "This is the classification given to \r\nthe Pay Item in the Organisation");
            this.infoToolTip.SetToolTip(this.balsTypComboBox, "Cumulative Balances keep figures from \r\nprevious pay runs whereas non-cumulative\r\n ba" +
                    "lances don't");
            this.infoToolTip.SetToolTip(this.priorityNumUpDown, "Indicates which Pay Items are run first\r\n during Mass Pay Runs. \r\nLowest Numbers are " +
                    "run First");
            this.infoToolTip.SetToolTip(this.balsAcntNmTextBox, "For Earnings/Employer Charges:\r\nDecrease a Cash Account or Increase a Liability (Credit Transaction) \r\nFo" +
                    "r Deductions/Bills/Charges:\r\nIncrease a Cash Account or Decrease a Liability Account (Debit Transaction)\r\n");
            this.infoToolTip.SetToolTip(this.costAcntNmTextBox, "For Earnings/Employer Charges:\r\nIncrease an Expense Account or Decrease a\r\n Liability or Increase a Receivable Account (Debit Transaction)" +
                    "\r\nFor Deductions/Bills/Charges:\r\nIncrease a Revenue Account or Decrease a Receivable Account" +
                    " or Increase a Liability Account (Credit Transaction)\r\n");
            this.infoToolTip.SetToolTip(this.itmMinTypComboBox, "Indicates whether the Item is Purely \r\nInformational or has Financial Value");
            this.infoToolTip.SetToolTip(this.itmMajTypComboBox, "There are two types here: Items that are \r\nPaid and Items that just store balances");
            System.Windows.Forms.Application.DoEvents();
            if (this.leftTreeView.Nodes.Count > 0 &&
              Global.currentPanel == "")
            {
                TreeViewEventArgs ex = new TreeViewEventArgs(this.leftTreeView.Nodes[0], TreeViewAction.ByMouse);
                this.leftTreeView_AfterSelect(this.leftTreeView, ex);
            }
            if (this.tabControl1.Controls.Count <= 0
              && this.leftTreeView.Nodes.Count > 0)
            {
                this.loadCorrectPanel(this.leftTreeView.Nodes[0].Text);
            }
        }
        #endregion
        #region "GENERAL..."
        private void populateTreeView()
        {
            this.leftTreeView.Nodes.Clear();
            if (!Global.mnFrm.cmCde.isThsMchnPrmtd())
            {
                Global.mnFrm.cmCde.showMsg("This Machine is not Permitted to run this software!\r\nContact the Vendor for Assistance!", 4);
                return;
            }
            this.tabControl1.Controls.Clear();
            for (int i = 0; i < menuItems.Length; i++)
            {
                if (i < menuItems.Length - 2)
                {
                    if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[0] +
                      "~" + Global.dfltPrvldgs[i + 1]) == false)
                    {
                        continue;
                    }
                }
                else if (i == menuItems.Length - 2)
                {
                    if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[0] +
                      "~" + Global.dfltPrvldgs[25]) == false)
                    {
                        continue;
                    }
                }
                else if (i == (menuItems.Length - 1))
                {
                    if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[0] +
                      "~" + Global.dfltPrvldgs[37]) == false)
                    {
                        continue;
                    }
                }
                TreeNode nwNode = new TreeNode();
                nwNode.Name = "myNode" + i.ToString();
                nwNode.Text = menuItems[i];
                nwNode.ImageKey = menuImages[i];
                this.leftTreeView.Nodes.Add(nwNode);
            }
            if (this.leftTreeView.Nodes.Count > 0)
            {
                this.leftTreeView.SelectedNode = this.leftTreeView.Nodes[0];
            }
        }

        private void showATab(ref TabPage my_tab)
        {
            //my_panel.Dock = DockStyle.Fill;
            //System.Windows.Forms.Application.DoEvents();
            //my_panel.Enabled = true;
            //my_panel.Visible = true;
            bool found = false;
            foreach (TabPage tab1 in this.tabControl1.TabPages)
            {
                if (tab1 == my_tab)
                {
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                this.tabControl1.Controls.Add(my_tab);
            }
            this.tabControl1.SelectedTab = my_tab;
            my_tab.Select();
            my_tab.Show();
            System.Windows.Forms.Application.DoEvents();
        }

        private void showAllPanels()
        {
            this.manualPayPanel.Visible = true;
            this.manualPayPanel.Enabled = true;
            System.Windows.Forms.Application.DoEvents();
            this.payItmsPanel.Visible = true;
            this.payItmsPanel.Enabled = true;
            System.Windows.Forms.Application.DoEvents();
            this.prsnSetPanel.Visible = true;
            this.prsnSetPanel.Enabled = true;
            System.Windows.Forms.Application.DoEvents();
            this.massPayPanel.Visible = true;
            this.massPayPanel.Enabled = true;
            System.Windows.Forms.Application.DoEvents();
            this.payTrnsPanel.Visible = true;
            this.payTrnsPanel.Enabled = true;
            System.Windows.Forms.Application.DoEvents();
            this.glIntrfcPanel.Visible = true;
            this.glIntrfcPanel.Enabled = true;
            System.Windows.Forms.Application.DoEvents();
            this.benefitsPanel.Visible = true;
            this.benefitsPanel.Enabled = true;
            System.Windows.Forms.Application.DoEvents();
        }

        private void loadCorrectPanel(string inpt_name)
        {
            if (inpt_name == menuItems[0])
            {
                this.showATab(ref this.tabPage1);
                this.changeOrg();
                this.loadOrgPersons();
                //this.loadPayItmsPanel();
            }
            else if (inpt_name == menuItems[1])
            {
                this.showATab(ref this.tabPage2);
                this.changeOrg();
                this.loadItmStPanel();
            }
            else if (inpt_name == menuItems[2])
            {
                this.showATab(ref this.tabPage3);
                this.changeOrg();
                this.loadPrsStPanel();
            }
            else if (inpt_name == menuItems[3])
            {
                this.showATab(ref this.tabPage4);
                this.changeOrg();
                this.loadMsPyPanel();
            }
            else if (inpt_name == menuItems[4])
            {
                this.showATab(ref this.tabPage5);
                this.changeOrg();
                this.loadTrnsPanel();
            }
            else if (inpt_name == menuItems[5])
            {
                this.showATab(ref this.tabPage6);
                this.changeOrg();
                this.loadInfcPanel();
            }
            else if (inpt_name == menuItems[6])
            {
                this.showATab(ref this.tabPage7);
                this.changeOrg();
                this.loadBnftsPanel();
            }
            else if (inpt_name == menuItems[7])
            {
                this.showATab(ref this.tabPage8);
                this.changeOrg();
                this.loadGBVPanel();
            }
            Global.currentPanel = inpt_name;
        }

        private void changeOrg()
        {
            //   if (this.crntOrgIDTextBox.Text == "-1"
            //|| this.crntOrgIDTextBox.Text == "")
            //   {
            //     this.crntOrgIDTextBox.Text = Global.mnFrm.cmCde.Org_id.ToString();
            //     this.crntOrgTextBox.Text = Global.mnFrm.cmCde.getOrgName(Global.mnFrm.cmCde.Org_id);
            //     Global.mnFrm.cmCde.getDBImageFile(this.crntOrgIDTextBox.Text + ".png",
            //       0, ref this.curOrgPictureBox);

            //     if (this.crntOrgIDTextBox.Text == "-1"
            // || this.crntOrgIDTextBox.Text == "")
            //     {
            //       this.crntOrgIDTextBox.Text = "-1";
            //     }
            //   }
        }

        private void hideAllPanels()
        {
            this.manualPayPanel.Visible = false;
            this.manualPayPanel.Enabled = false;
            System.Windows.Forms.Application.DoEvents();
            this.payItmsPanel.Visible = false;
            this.payItmsPanel.Enabled = false;
            System.Windows.Forms.Application.DoEvents();
            this.prsnSetPanel.Visible = false;
            this.prsnSetPanel.Enabled = false;
            System.Windows.Forms.Application.DoEvents();
            this.massPayPanel.Visible = false;
            this.massPayPanel.Enabled = false;
            System.Windows.Forms.Application.DoEvents();
            this.payTrnsPanel.Visible = false;
            this.payTrnsPanel.Enabled = false;
            System.Windows.Forms.Application.DoEvents();
            this.glIntrfcPanel.Visible = false;
            this.glIntrfcPanel.Enabled = false;
            System.Windows.Forms.Application.DoEvents();
            this.benefitsPanel.Visible = false;
            this.benefitsPanel.Enabled = false;
            System.Windows.Forms.Application.DoEvents();
        }

        private void showAPanel(ref Panel my_panel)
        {
            my_panel.Dock = DockStyle.Fill;
            System.Windows.Forms.Application.DoEvents();
            my_panel.Enabled = true;
            my_panel.Visible = true;
            System.Windows.Forms.Application.DoEvents();
        }

        private void leftTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //this.hideAllPanels();
            if (e.Node == null)
            {
                return;
            }
            this.loadCorrectPanel(e.Node.Text);
        }

        //private void crntOrgButton_Click(object sender, EventArgs e)
        //{
        //  string[] selVals = new string[1];
        //  selVals[0] = this.crntOrgIDTextBox.Text;
        //  DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        //    Global.mnFrm.cmCde.getLovID("Organisations"), ref selVals, true, true);
        //  if (dgRes == DialogResult.OK)
        //  {
        //    this.curOrgPictureBox.Image.Dispose();
        //    this.curOrgPictureBox.Image = InternalPayments.Properties.Resources.blank;
        //    for (int i = 0; i < selVals.Length; i++)
        //    {
        //      bool shdLd = false;
        //      if (this.crntOrgIDTextBox.Text == "" || this.crntOrgIDTextBox.Text == "-1")
        //      {
        //        shdLd = true;
        //      }
        //      this.crntOrgIDTextBox.Text = selVals[i];
        //      this.crntOrgTextBox.Text = Global.mnFrm.cmCde.getOrgName(int.Parse(selVals[i]));
        //      Global.mnFrm.cmCde.getDBImageFile(this.crntOrgIDTextBox.Text + ".png",
        //        0, ref this.curOrgPictureBox);
        //      if (shdLd == false)
        //      {
        //        //this.last_chrt_num = 0;
        //        //this.chrt_cur_indx = 0;
        //        //this.last_site_num = 0;
        //        //this.site_cur_indx = 0;

        //        this.loadCorrectPanel(this.leftTreeView.SelectedNode.Text);
        //      }
        //    }
        //  }
        //}

        private void disableFormButtons()
        {
            bool vwSQL = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[8]);
            bool rcHstry = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[7]);
            bool vwMnlPay = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[1]);
            this.vwPyItmsPrs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[29]);
            this.vwBanks = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[30]);
            this.vwAllMsPays = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[41]);

            this.pymntTabControl.Controls.Clear();
            if (vwMnlPay == true)
            {
                this.pymntTabControl.Controls.Add(this.payTabPage);
            }
            if (this.vwPyItmsPrs == true)
            {
                this.pymntTabControl.Controls.Add(this.asgdPyItmsTabPage);
            }
            if (this.vwBanks == true)
            {
                this.pymntTabControl.Controls.Add(this.prsBanksTabPage);
            }
            if (this.pymntTabControl.TabPages.Count > 0)
            {
                this.curTabIndx = this.pymntTabControl.TabPages[0].Name;
            }
            //Manual Payments
            this.addMnlPys = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[9]);
            this.sndMnlPy = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[24]);
            this.processPayButton.Enabled = this.addMnlPys;
            this.printPstPyMenuItem.Enabled = this.addMnlPys;
            this.sendMnlPyToGLMenuItem.Enabled = this.sndMnlPy;
            this.vwSelfCheckBox.Checked = !this.vwAllMsPays;

            this.rvrsMnlPys = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[10]);
            this.reversePaymentMenuItem.Enabled = this.rvrsMnlPys;

            this.vwSQLPayMenuItem.Enabled = vwSQL;
            this.vwSQLPrsnMenuItem.Enabled = vwSQL;
            this.viewSQLPstMenuItem.Enabled = vwSQL;

            this.rcHstryPayMenuItem.Enabled = rcHstry;
            this.rcHstryPrsnMenuItem.Enabled = rcHstry;
            this.recHstryPstMenuItem.Enabled = rcHstry;
            //Pay Item Sets
            this.addItmSts = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[11]);
            this.addItmStButton.Enabled = this.addItmSts;
            this.addItmStMenuItem.Enabled = this.addItmSts;
            this.addItmMenuItem.Enabled = this.addItmSts;
            this.addItmStDtButton.Enabled = this.addItmSts;

            this.editItmSts = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[12]);
            this.editItmStButton.Enabled = this.editItmSts;
            this.editItmStMenuItem.Enabled = this.editItmSts;
            this.rmvPayItmMenuItem.Enabled = this.editItmSts;
            this.delItmStDtButton.Enabled = this.editItmSts;

            this.delItmSts = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[13]);
            this.deleteItmStButton.Enabled = this.delItmSts;
            this.delItmStMenuItem.Enabled = this.delItmSts;

            this.vwSQLItmStButton.Enabled = vwSQL;
            this.vwSQLItmStDtMenuItem.Enabled = vwSQL;
            this.vwSQLItmStMenuItem.Enabled = vwSQL;

            this.rcHstryItmStMenuItem.Enabled = rcHstry;
            this.recHstryItmStDtMenuItem.Enabled = rcHstry;
            this.recHstryItmStButton.Enabled = rcHstry;
            //Person Sets 
            this.addPrsSts = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[14]);
            this.addPrsStButton.Enabled = this.addPrsSts;
            this.addPrsStMenuItem.Enabled = this.addPrsSts;

            this.editPrsSts = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[15]);
            this.editPrsStButton.Enabled = this.editPrsSts;
            this.editPrsStMenuItem.Enabled = this.editPrsSts;

            this.delPrsSts = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[16]);
            this.delPrsStButton.Enabled = this.delPrsSts;
            this.delPrsStMenuItem.Enabled = this.delPrsSts;

            this.vwSQLPrsStButton.Enabled = vwSQL;
            this.vwSQLPrsStMenuItem.Enabled = vwSQL;

            this.rcHstryPrsStButton.Enabled = rcHstry;
            this.rcHstryPrsStMenuItem.Enabled = rcHstry;
            //Mass Pay Runs
            this.addMsPys = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]);
            this.runMsPys = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[22]);
            this.rollMsPys = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[23]);
            this.sendMsPys = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[20]);
            this.addMsPyButton.Enabled = this.addMsPys;
            this.addMsPyMenuItem.Enabled = this.addMsPys;
            this.msPayActRnButton.Enabled = this.runMsPys;
            this.prntPySlipMenuItem.Enabled = this.runMsPys;
            this.prntAdvcMenuItem.Enabled = this.runMsPys; ;
            this.rllbckMsPyRnButton.Enabled = this.rollMsPys;
            this.sndMsPyToGLButton.Enabled = this.sendMsPys;
            this.attchedValsButton.Enabled = this.runMsPys;

            this.editMsPys = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[18]);
            this.editMsPyButton.Enabled = this.editMsPys;
            this.editMsPyMenuItem.Enabled = this.editMsPys;

            this.delMsPys = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[19]);
            this.delMsPyButton.Enabled = this.delMsPys;
            this.delMsPyMenuItem.Enabled = this.delMsPys;

            this.vwSQLMsPyButton.Enabled = vwSQL;
            this.vwSQLMsPyMenuItem.Enabled = vwSQL;
            this.vwSQLMsPyDtMenuItem.Enabled = vwSQL;

            this.rcHstryMsPyMenuItem.Enabled = rcHstry;
            this.rcHstryMsPyDtMenuItem.Enabled = rcHstry;
            this.recHstryMsPyButton.Enabled = rcHstry;
            //Transactions Search
            this.vwSQLTrnsButton.Enabled = vwSQL;
            this.vwSQLPySrchMenuItem.Enabled = vwSQL;

            this.recHstryTrnsButton.Enabled = rcHstry;
            this.rcHstryPySrchMenuItem.Enabled = rcHstry;

            //GL Interface
            this.vwSQLInfcButton.Enabled = vwSQL;
            this.vwSQLIntFcMenuItem.Enabled = vwSQL;

            this.recHstryInfcButton.Enabled = rcHstry;
            this.rcHstryGlIntfcMenuItem.Enabled = rcHstry;
            //Pay Items
            this.saveItmButton.Enabled = false;
            this.additms = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[26]);
            this.addItmButton.Enabled = this.additms;
            this.addItmMenuItem.Enabled = this.additms;
            this.imprtItemsButton.Enabled = this.additms;
            this.addValButton.Enabled = this.additms;
            this.addValMenuItem.Enabled = this.additms;
            this.imptValExclTmpltMenuItem.Enabled = this.additms;

            this.edititms = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[27]);
            this.editItmButton.Enabled = this.edititms;
            this.editItmMenuItem.Enabled = this.edititms;
            this.editValButton.Enabled = this.edititms;
            this.editValMenuItem.Enabled = this.edititms;

            this.delitms = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[28]);
            this.delItmButton.Enabled = this.delitms;
            this.delItmMenuItem.Enabled = this.delitms;
            this.delValButton.Enabled = this.delitms;
            this.delValMenuItem.Enabled = this.delitms;

            this.vwSQLItmButton.Enabled = vwSQL;
            this.rcHstryItmMenuItem.Enabled = rcHstry;
            this.vwSQLItmMenuItem.Enabled = vwSQL;
            this.recHstryItmButton.Enabled = rcHstry;
            this.recHstryPValsMenuItem.Enabled = rcHstry;
            this.vwSQLPValsMenuItem.Enabled = vwSQL;
            //Global Values
            this.addgbvs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[38]);
            this.addGBVButton.Enabled = this.addgbvs;
            this.importGBVButton.Enabled = this.addgbvs;
            //this.exportGBVButton.Enabled = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[38]);

            this.editgbvs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[39]);
            this.editGBVButton.Enabled = this.editgbvs;
            this.addGBVDTButton.Enabled = this.editgbvs;
            //this.editPrsStMenuItem.Enabled = this.editgbvs;
            this.delGBVDTButton.Enabled = this.editgbvs;

            this.delgbvs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[40]);
            this.delGBVButton.Enabled = this.delgbvs;
            //this.delPrsStMenuItem.Enabled = this.delgbvs;

            this.vwSQLButton.Enabled = vwSQL;
            this.vwSQLDTButton.Enabled = vwSQL;
            // this.vwSQLPrsStMenuItem.Enabled = vwSQL;

            this.rcHstryButton.Enabled = rcHstry;
            this.rcHstryDTButton.Enabled = rcHstry;
            //this.rcHstryPrsStMenuItem.Enabled = rcHstry;

        }

        private void hideTreevwMenuItem_Click(object sender, EventArgs e)
        {
            if (this.hideTreevwMenuItem.Text.Contains("Hide"))
            {
                this.splitContainer1.Panel1Collapsed = true;
                this.hideTreevwMenuItem.Text = "Show Tree View";
            }
            else
            {
                this.splitContainer1.Panel1Collapsed = false;
                this.hideTreevwMenuItem.Text = "Hide Tree View";
            }
        }

        #endregion
        #region "Manual Payments..."
        private void loadPayItmsPanel()
        {
            this.obey_itm_evnts1 = false;
            if (this.searchInItmComboBoxNw.SelectedIndex < 0)
            {
                this.searchInItmComboBoxNw.SelectedIndex = 0;
            }
            if (this.searchForItmTextBoxNw.Text.Contains("%") == false)
            {
                this.searchForItmTextBoxNw.Text = "%" + this.searchForItmTextBoxNw.Text.Replace(" ", "%") + "%";
            }
            if (this.searchForItmTextBoxNw.Text == "%%")
            {
                this.searchForItmTextBoxNw.Text = "%";
            }
            int dsply = 0;
            if (this.dsplySizeItmComboBoxNw.Text == ""
              || int.TryParse(this.dsplySizeItmComboBoxNw.Text, out dsply) == false)
            {
                this.dsplySizeItmComboBoxNw.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }

            long dsply1 = 0;
            if (this.itmStIDMnlTextBox.Text == "" || this.itmStIDMnlTextBox.Text == "-1"
              || long.TryParse(this.itmStIDMnlTextBox.Text, out dsply1) == false)
            {
                string[] vl = Global.get_Org_DfltItmSt(Global.mnFrm.cmCde.Org_id);
                this.itmStIDMnlTextBox.Text = vl[0];
                this.itmStNmMnlTextBox.Text = vl[1];
            }

            //this.groupBox8.Height = this.g.Bottom - this.toolStrip4.Bottom - 50;
            this.is_last_itm1 = false;
            this.totl_itm1 = Global.mnFrm.cmCde.Big_Val;
            this.getItmPnlData1();
            this.obey_itm_evnts1 = true;
        }

        private void getItmPnlData1()
        {
            this.updtItmTotals1();
            if (this.prsNamesListView.SelectedItems.Count > 0)
            {
                this.populateItmListVw(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
            }
            else
            {
                this.populateItmListVw(-100000017);
            }
            this.updtItmNavLabels1();
        }

        private void updtItmTotals1()
        {
            Global.mnFrm.cmCde.navFuncts.FindNavigationIndices(
              long.Parse(this.dsplySizeItmComboBoxNw.Text), this.totl_itm1);
            if (this.itm_cur_indx1 >= Global.mnFrm.cmCde.navFuncts.totalGroups)
            {
                this.itm_cur_indx1 = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            if (this.itm_cur_indx1 < 0)
            {
                this.itm_cur_indx1 = 0;
            }
            Global.mnFrm.cmCde.navFuncts.currentNavigationIndex = this.itm_cur_indx1;
        }

        private void updtItmNavLabels1()
        {
            this.moveFirstItmButtonNew.Enabled = Global.mnFrm.cmCde.navFuncts.moveFirstBtnStatus();
            this.movePreviousItmButtonNw.Enabled = Global.mnFrm.cmCde.navFuncts.movePrevBtnStatus();
            this.moveNextItmButtonNw.Enabled = Global.mnFrm.cmCde.navFuncts.moveNextBtnStatus();
            this.moveLastItmButtonNw.Enabled = Global.mnFrm.cmCde.navFuncts.moveLastBtnStatus();
            this.positionItmTextBoxNw.Text = Global.mnFrm.cmCde.navFuncts.displayedRecordsNumbers();
            if (this.is_last_itm1 == true ||
              this.totl_itm1 != Global.mnFrm.cmCde.Big_Val)
            {
                this.totalRecsItmLabelNw.Text = Global.mnFrm.cmCde.navFuncts.totalRecordsLabel();
            }
            else
            {
                this.totalRecsItmLabelNw.Text = "of Total";
            }
        }

        private void populateItmListVw(long prsnID)
        {
            this.obey_itm_evnts1 = false;
            DataSet dtst = Global.get_Basic_Itm1(this.searchForItmTextBoxNw.Text,
              this.searchInItmComboBoxNw.Text, this.itm_cur_indx1,
              int.Parse(this.dsplySizeItmComboBoxNw.Text), prsnID,
              long.Parse(this.itmStIDMnlTextBox.Text));
            this.itmListViewPymnt.Items.Clear();

            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.last_itm_num1 = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
                ListViewItem nwItem;
                if (dtst.Tables[0].Rows[i][3].ToString().ToUpper() == "Balance Item".ToUpper())
                {
                    nwItem = new ListViewItem(new string[] {
    (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][1].ToString().ToUpper(),
    dtst.Tables[0].Rows[i][2].ToString().ToUpper(),
    dtst.Tables[0].Rows[i][3].ToString().ToUpper(),
    dtst.Tables[0].Rows[i][0].ToString(),
    dtst.Tables[0].Rows[i][4].ToString().ToUpper(),
    dtst.Tables[0].Rows[i][5].ToString().ToUpper(),
    dtst.Tables[0].Rows[i][6].ToString()});
                }
                else
                {
                    nwItem = new ListViewItem(new string[] {
    (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),
    dtst.Tables[0].Rows[i][2].ToString(),
    dtst.Tables[0].Rows[i][3].ToString(),
    dtst.Tables[0].Rows[i][0].ToString(),
    dtst.Tables[0].Rows[i][4].ToString(),
    dtst.Tables[0].Rows[i][5].ToString(),
    dtst.Tables[0].Rows[i][6].ToString()});
                }
                this.itmListViewPymnt.Items.Add(nwItem);
            }
            this.correctItmNavLbls1(dtst);
            if (this.itmListViewPymnt.Items.Count > 0)
            {
                this.obey_itm_evnts1 = true;
                this.itmListViewPymnt.Items[0].Selected = true;
            }
            else
            {
                int dsply = 0;
                this.pstPayListView.Items.Clear();
                this.clearMnlPay();
                this.pst_cur_indx = 0;
                this.totl_pst = 0;
                this.last_pst_num = 0;
                if (this.dsplySizePstComboBox.Text == ""
            || int.TryParse(this.dsplySizePstComboBox.Text, out dsply) == false)
                {
                    this.dsplySizePstComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
                }
                this.updtPstTotals();
                this.updtPstNavLabels();
            }
            this.obey_itm_evnts1 = true;
        }

        private void correctItmNavLbls1(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.itm_cur_indx1 == 0 && totlRecs == 0)
            {
                this.is_last_itm1 = true;
                this.totl_itm1 = 0;
                this.last_itm_num1 = 0;
                this.itm_cur_indx1 = 0;
                this.updtItmTotals1();
                this.updtItmNavLabels1();
            }
            else if (this.totl_itm1 == Global.mnFrm.cmCde.Big_Val
           && totlRecs < long.Parse(this.dsplySizeItmComboBoxNw.Text))
            {
                this.totl_itm1 = this.last_itm_num1;
                if (totlRecs == 0)
                {
                    this.itm_cur_indx1 -= 1;
                    this.updtItmTotals1();
                    if (this.prsNamesListView.SelectedItems.Count > 0)
                    {
                        this.populateItmListVw(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
                    }
                    else
                    {
                        this.populateItmListVw(-100000017);
                    }
                }
                else
                {
                    this.updtItmTotals1();
                }
            }
        }

        private bool shdObeyItmEvts1()
        {
            return this.obey_itm_evnts1;
        }

        private void ItmPnlNavButtons1(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecsItmLabelNw.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.is_last_itm1 = false;
                this.itm_cur_indx1 = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.is_last_itm1 = false;
                this.itm_cur_indx1 -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.is_last_itm1 = false;
                this.itm_cur_indx1 += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.is_last_itm1 = true;
                if (this.prsNamesListView.SelectedItems.Count > 0)
                {
                    this.totl_itm1 = Global.get_Total_Itm1(this.searchForItmTextBoxNw.Text,
                      this.searchInItmComboBoxNw.Text,
                      long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text),
                      long.Parse(this.itmStIDMnlTextBox.Text));
                }
                else
                {
                    this.totl_itm1 = 0;
                }
                this.updtItmTotals1();
                this.itm_cur_indx1 = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            this.getItmPnlData1();
        }

        private void goItmButton_Click1(object sender, EventArgs e)
        {
            this.loadPayItmsPanel();
        }

        private void itmListView_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (this.shdObeyItmEvts1() == false || this.itmListViewPymnt.SelectedItems.Count > 1)
            {
                return;
            }
            if (this.itmListViewPymnt.SelectedItems.Count > 0)
            {
                this.populateTodyPymnts();
                this.loadPstPayPanel();
            }
            else
            {

                int dsply = 0;
                this.pst_cur_indx = 0;
                this.totl_pst = 0;
                this.last_pst_num = 0;
                if (this.dsplySizePstComboBox.Text == ""
            || int.TryParse(this.dsplySizePstComboBox.Text, out dsply) == false)
                {
                    this.dsplySizePstComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
                }
                this.updtPstTotals();
                this.updtPstNavLabels();
            }
        }

        private void positionItmTextBox_KeyDown1(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                this.ItmPnlNavButtons1(this.movePreviousPrsButton, ex);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
            {
                this.ItmPnlNavButtons1(this.moveNextPrsButton, ex);
            }
        }

        private void searchForItmTextBox_KeyDown1(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.searchForItmTextBoxNw.Focus();
                this.goItmButton_Click1(this.goItmButtonNw, ex);
            }
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

        private void rfrshPayMenuItem_Click(object sender, EventArgs e)
        {
            this.goItmButton_Click1(this.goItmButtonNw, e);
        }

        private void rcHstryPayMenuItem_Click(object sender, EventArgs e)
        {
            if (this.itmListViewPymnt.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(
              Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(
              this.itmListViewPymnt.SelectedItems[0].SubItems[4].Text),
              "org.org_pay_items", "item_id"), 7);
        }

        private void vwSQLPayMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(this.itm_SQL1, 8);
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
                nwDiag.grpComboBox.SelectedItem = "Single Person";
            }

            nwDiag.msPyItmStIDTextBox.Text = this.itmStIDMnlTextBox.Text;
            nwDiag.msPyItmStNmTextBox.Text = this.itmStNmMnlTextBox.Text;
            nwDiag.Show();
            //DialogResult dgres = 
            //if (dgres == DialogResult.OK)
            //{
            //}
            //else if (dgres == DialogResult.Ignore)
            //{
            //  if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[4]) == true
            //    && nwDiag.mspID > 0)
            //  {
            //    TreeNode nd = null;
            //    for (int i = 0; i < this.leftTreeView.Nodes.Count; i++)
            //    {
            //      if (this.leftTreeView.Nodes[i].Text == this.menuItems[3])
            //      {
            //        nd = this.leftTreeView.Nodes[i];
            //        break;
            //      }
            //    }
            //    if (nd != null)
            //    {
            //      this.leftTreeView.SelectedNode = nd;
            //      System.Windows.Forms.Application.DoEvents();
            //      if (nd.IsSelected == false)
            //      {
            //        TreeViewEventArgs tv = new TreeViewEventArgs(nd, TreeViewAction.ByMouse);
            //        this.leftTreeView_AfterSelect(this.leftTreeView, tv);
            //      }
            //    }
            //    //this.loadCorrectPanel(this.menuItems[3]);
            //  }
            //  else
            //  {
            //    this.itmStIDMnlTextBox.Text = nwDiag.msPyItmStIDTextBox.Text;
            //    this.itmStNmMnlTextBox.Text = nwDiag.msPyItmStNmTextBox.Text;
            //    this.goItmButton_Click1(this.goItmButtonNw, e);
            //  }
            //}
            //this.goItmButton_Click1(this.goItmButtonNw, e);

        }

        public void openQuickPay(long mspID)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[4]) == true
                && mspID > 0)
            {
                TreeNode nd = null;
                for (int i = 0; i < this.leftTreeView.Nodes.Count; i++)
                {
                    if (this.leftTreeView.Nodes[i].Text == this.menuItems[3])
                    {
                        nd = this.leftTreeView.Nodes[i];
                        break;
                    }
                }
                if (nd != null)
                {
                    this.leftTreeView.SelectedNode = nd;
                    System.Windows.Forms.Application.DoEvents();
                    if (nd.IsSelected == false)
                    {
                        TreeViewEventArgs tv = new TreeViewEventArgs(nd, TreeViewAction.ByMouse);
                        this.leftTreeView_AfterSelect(this.leftTreeView, tv);
                    }
                }
                //this.loadCorrectPanel(this.menuItems[3]);
            }
        }

        private void quickPayButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[9]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                  " this action!\nContact your System Administrator!", 0);
                return;
            }
            quickPayForm nwDiag = new quickPayForm();
            //if (this.prsNamesListView.SelectedItems.Count > 0)
            //{
            //  nwDiag.locIDTextBox.Text = this.prsNamesListView.SelectedItems[0].SubItems[1].Text;
            //}

            nwDiag.msPyItmStIDTextBox.Text = Global.get_Org_DfltItmSt(Global.mnFrm.cmCde.Org_id)[0];
            nwDiag.msPyItmStNmTextBox.Text = Global.get_Org_DfltItmSt(Global.mnFrm.cmCde.Org_id)[1];

            DialogResult dgres = nwDiag.ShowDialog();
            if (dgres == DialogResult.OK)
            {
            }
            this.goMsPyButton_Click(this.goMsPyButton, e);
        }

        private void runQuickPayMenuItem_Click(object sender, EventArgs e)
        {
            this.quickPayButton_Click(this.quickPayButton, e);
        }

        private void prntActPySlpMenuItem_Click(object sender, EventArgs e)
        {
            this.pageNo = 1;
            this.prntIdx = 0;
            this.printDialog1 = new PrintDialog();
            printDialog1.Document = this.printDocument2;
            this.printDocument2.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", 850, 1100);
            DialogResult res = printDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                printDocument2.Print();
            }
        }

        private void prntActAdvcMenuItem_Click(object sender, EventArgs e)
        {
            //this.pageNo = 1;
            //this.prntIdx = 0;
            //this.printDialog1 = new PrintDialog();
            //printDialog1.Document = this.printDocument3;
            //this.printDocument3.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", 850, 1100);
            //DialogResult res = printDialog1.ShowDialog();
            //if (res == DialogResult.OK)
            //{
            //  printDocument3.Print();
            //}
        }

        private void bankAdvicePdfButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.msPyListView.SelectedItems.Count <= 0)
                {
                    Global.mnFrm.cmCde.showMsg("Please select a Pay Run First!", 0);
                    return;
                }
                // Create a new PDF document
                Graphics g = Graphics.FromHwnd(this.Handle);
                XPen aPen = new XPen(XColor.FromArgb(Color.Black), 1);
                PdfDocument document = new PdfDocument();
                document.Info.Title = "PAY RUN BANK ADVICE REPORT";
                // Create first page for basic person details
                PdfPage page0 = document.AddPage();
                page0.Orientation = PageOrientation.Landscape;
                page0.Height = XUnit.FromInch(8.5);
                page0.Width = XUnit.FromInch(11);
                XGraphics gfx0 = XGraphics.FromPdfPage(page0);
                XFont xfont0 = new XFont("Verdana", 20, XFontStyle.BoldItalic);
                //gfx0.DrawString("Hello, World!" + this.locIDTextBox.Text, xfont0, XBrushes.Black,
                //new XRect(0, 0, page0.Width, page0.Height),
                //  XStringFormats.TopLeft);

                XFont xfont1 = new XFont("Times New Roman", 10.25f, XFontStyle.Underline | XFontStyle.Bold);
                XFont xfont11 = new XFont("Times New Roman", 10.25f, XFontStyle.Bold);
                XFont xfont2 = new XFont("Times New Roman", 10.25f, XFontStyle.Bold);
                XFont xfont4 = new XFont("Times New Roman", 10.0f, XFontStyle.Bold);
                XFont xfont41 = new XFont("Lucida Console", 10.0f);
                XFont xfont3 = new XFont("Lucida Console", 8.25f);
                XFont xfont31 = new XFont("Lucida Console", 10.5f, XFontStyle.Bold);
                XFont xfont5 = new XFont("Times New Roman", 6.0f, XFontStyle.Italic);

                Font font1 = new Font("Times New Roman", 10.25f, FontStyle.Underline | FontStyle.Bold);
                Font font11 = new Font("Times New Roman", 10.25f, FontStyle.Bold);
                Font font2 = new Font("Times New Roman", 10.25f, FontStyle.Bold);
                Font font4 = new Font("Times New Roman", 10.0f, FontStyle.Bold);
                Font font41 = new Font("Lucida Console", 10.0f);
                Font font3 = new Font("Lucida Console", 8.25f);
                Font font31 = new Font("Lucida Console", 10.5f, FontStyle.Bold);
                Font font5 = new Font("Times New Roman", 6.0f, FontStyle.Italic);

                float font1Hght = (float)gfx0.MeasureString("ABCDEFGHIJKLMNOP", xfont1).Height;
                float font2Hght = (float)gfx0.MeasureString("ABCDEFGHIJKLMNOP", xfont2).Height;
                float font3Hght = (float)gfx0.MeasureString("ABCDEFGHIJKLMNOP", xfont3).Height;
                float font4Hght = (float)gfx0.MeasureString("ABCDEFGHIJKLMNOP", xfont41).Height;
                float font5Hght = (float)gfx0.MeasureString("ABCDEFGHIJKLMNOP", xfont5).Height;

                float startX = 40;
                float startXNw = 40;
                float endX = 680;
                float startY = 40;
                float offsetY = 0;
                float ght = 0;

                float pageWidth = 760 - startX;//e.PageSettings.PrintableArea.Width;
                                               //float pageHeight = 590 - startX;// e.PageSettings.PrintableArea.Height;
                float txtwdth = pageWidth - startX;
                //Global.mnFrm.cmCde.showMsg(pageWidth.ToString(), 0);
                float gwdth = 0;
                //StringBuilder strPrnt = new StringBuilder();
                //strPrnt.AppendLine("Received From");
                string[] nwLn;
                int pageNo = 1;
                XImage img = (XImage)Global.mnFrm.cmCde.getDBImageFile(Global.mnFrm.cmCde.Org_id.ToString() + ".png", 0);
                float picWdth = 80.00F;
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
                    XRect rect = new XRect(startX + picWdth + 5, startY + offsetY - 7, 125, ght);
                    gfx0.DrawRectangle(XBrushes.White, rect);
                    tf.DrawString(Global.mnFrm.cmCde.getOrgPstlAddrs(Global.mnFrm.cmCde.Org_id).Trim()
                      , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                    //gfx0.DrawString(,
                    //xfont2, XBrushes.Black, startX + picWdth, startY + offsetY);
                    offsetY += ght + 5;

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
                    gfx0.DrawLine(aPen, startX, startY + offsetY - 8, startX + endX,
               startY + offsetY - 8);

                }
                string orgType = Global.mnFrm.cmCde.getPssblValNm(int.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
            "org.org_details", "org_id", "org_typ_id", Global.mnFrm.cmCde.Org_id)));

                //Person Types
                float oldoffsetY = offsetY;
                float hgstOffsetY = 0;
                float hghstght = 0;

                DataSet dtst = Global.get_BankAdvice(this.msPyListView.SelectedItems[0].SubItems[1].Text);

                oldoffsetY = offsetY;
                offsetY = oldoffsetY + 5;

                double outstndngAmnt = 0;

                startX = startXNw;
                for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
                {
                    startX = startXNw;
                    if (a == 0)
                    {
                        hgstOffsetY = 0;
                        ght = (float)gfx0.MeasureString(
                        ("PAY RUN BANK ADVICE REPORT (" + this.trnsDateTextBox.Text + ")").ToUpper(), xfont2).Height;
                        //lblght = ght;
                        XTextFormatter tf = new XTextFormatter(gfx0);
                        XRect rect = new XRect(startX, startY + offsetY, pageWidth - startX, ght);
                        gfx0.DrawRectangle(XBrushes.LightGray, rect);
                        tf.DrawString(("PAY RUN BANK ADVICE REPORT (" + this.trnsDateTextBox.Text + ")").ToUpper()
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
                                gfx0.DrawRectangle(XBrushes.LightGray, rect);
                                tf.DrawString(" " + dtst.Tables[0].Columns[j].Caption
                                  , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                                startX += wdth + 10;
                            }
                        }
                        offsetY += (int)ght + 5;
                        startX = startXNw;
                    }
                    hghstght = 0;
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
                            string strToBreak = dtst.Tables[0].Rows[a][j].ToString();

                            if (j == 3 || j == 7)
                            {
                                double tst = 0;
                                if (double.TryParse(strToBreak, out tst) == false)
                                {
                                    strToBreak = "0";
                                }
                                strToBreak = double.Parse(strToBreak).ToString("#,##0.00");
                                if (j == 7)
                                {
                                    outstndngAmnt += double.Parse(strToBreak);
                                }
                            }
                            nwLn = Global.mnFrm.cmCde.breakTxtDown(
                              strToBreak,
                              (int)(wdth * 1.5), font41, g);

                            string finlStr = "";
                            if (j == 3 || j == 7)
                            {
                                finlStr = string.Join("\n", nwLn).PadLeft(10);
                            }
                            else
                            {
                                finlStr = string.Join("\n", nwLn);
                            }
                            ght = (float)gfx0.MeasureString(
                           finlStr, xfont41).Height;

                            XTextFormatter tf = new XTextFormatter(gfx0);
                            XRect rect = new XRect(startX, startY + offsetY, wdth + 5, ght);
                            gfx0.DrawRectangle(XBrushes.White, rect);


                            tf.DrawString(finlStr
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
                    if ((startY + offsetY) >= 580)
                    {
                        page0 = document.AddPage();
                        page0.Orientation = PageOrientation.Portrait;
                        page0.Height = XUnit.FromInch(8.5);
                        page0.Width = XUnit.FromInch(11);
                        gfx0 = XGraphics.FromPdfPage(page0);
                        offsetY = 0;
                        hgstOffsetY = 0;
                    }
                }

                //offsetY += hghstght + 5;
                hghstght = 0;
                startX = startXNw;
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
                        string strToBreak = " ";
                        if (j == 6)
                        {
                            strToBreak = "TOTALS = ";
                        }
                        if (j == 3 || j == 7)
                        {
                            if (j == 7)
                            {
                                strToBreak = (outstndngAmnt).ToString("#,##0.00");
                            }
                        }
                        nwLn = Global.mnFrm.cmCde.breakTxtDown(
                          strToBreak,
                          (int)(wdth * 1.5), font31, g);

                        string finlStr = "";
                        if (j == 3 || j == 7)
                        {
                            finlStr = string.Join("\n", nwLn).PadLeft(10);
                        }
                        else
                        {
                            finlStr = string.Join("\n", nwLn);
                        }
                        ght = (float)gfx0.MeasureString(
                       finlStr, xfont31).Height;

                        XTextFormatter tf = new XTextFormatter(gfx0);
                        XRect rect = new XRect(startX, startY + offsetY, wdth + 5, ght);
                        gfx0.DrawRectangle(XBrushes.White, rect);


                        tf.DrawString(finlStr
                          , xfont31, XBrushes.Black, rect, XStringFormats.TopLeft);

                        startX += wdth + 10;
                        if (hghstght < ght)
                        {
                            hghstght = ght;
                        }
                    }
                }
                offsetY += hghstght + 5;
                //Slogan: 
                startX = startXNw;
                if (offsetY < 535)
                {
                    offsetY = 535;
                }
                gfx0.DrawLine(aPen, startX, startY + offsetY, startX + endX,
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
                string filename = Global.mnFrm.cmCde.getRptDrctry() + @"\BankAdviceRpt_" + Global.mnFrm.cmCde.getDB_Date_time().Replace(" ", "").Replace(":", "").Replace("-", "") + ".pdf";
                document.Save(filename);
                // ...and start a viewer.
                System.Diagnostics.Process.Start(filename);
                //Global.mnFrm.cmCde.upldImgsFTP(9, Global.mnFrm.cmCde.getRptDrctry(), @"\BankAdviceRpt_" + Global.mnFrm.cmCde.getDB_Date_time().Replace(" ", "").Replace(":", "").Replace("-", "") + ".pdf");
                System.Windows.Forms.Application.DoEvents();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.Application.DoEvents();
                Global.mnFrm.cmCde.showMsg(ex.Message + "\r\n\r\n" + ex.InnerException + "\r\n\r\n" + ex.StackTrace, 0);
            }
        }

        private void payRunSmmryPdfButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.msPyListView.SelectedItems.Count <= 0)
                {
                    Global.mnFrm.cmCde.showMsg("Please select a Pay Run First!", 0);
                    return;
                }
                // Create a new PDF document
                Graphics g = Graphics.FromHwnd(this.Handle);
                XPen aPen = new XPen(XColor.FromArgb(Color.Black), 1);
                PdfDocument document = new PdfDocument();
                document.Info.Title = "PAY RUN SUMMARY REPORT";
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

                XFont xfont1 = new XFont("Times New Roman", 10.25f, XFontStyle.Underline | XFontStyle.Bold);
                XFont xfont11 = new XFont("Times New Roman", 10.25f, XFontStyle.Bold);
                XFont xfont2 = new XFont("Times New Roman", 10.25f, XFontStyle.Bold);
                XFont xfont4 = new XFont("Times New Roman", 10.0f, XFontStyle.Bold);
                XFont xfont41 = new XFont("Lucida Console", 10.0f);
                XFont xfont3 = new XFont("Lucida Console", 8.25f);
                XFont xfont31 = new XFont("Lucida Console", 10.5f, XFontStyle.Bold);
                XFont xfont5 = new XFont("Times New Roman", 6.0f, XFontStyle.Italic);

                Font font1 = new Font("Times New Roman", 10.25f, FontStyle.Underline | FontStyle.Bold);
                Font font11 = new Font("Times New Roman", 10.25f, FontStyle.Bold);
                Font font2 = new Font("Times New Roman", 10.25f, FontStyle.Bold);
                Font font4 = new Font("Times New Roman", 10.0f, FontStyle.Bold);
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
                float startXNw = 25;
                float endX = 560;
                float startY = 40;
                float offsetY = 0;
                float ght = 0;

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
                      pageWidth, font2, g);
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
                    XRect rect = new XRect(startX + picWdth + 5, startY + offsetY - 7, 125, ght);
                    gfx0.DrawRectangle(XBrushes.White, rect);
                    tf.DrawString(Global.mnFrm.cmCde.getOrgPstlAddrs(Global.mnFrm.cmCde.Org_id).Trim()
                      , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                    //gfx0.DrawString(,
                    //xfont2, XBrushes.Black, startX + picWdth, startY + offsetY);
                    offsetY += ght + 5;

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
                    gfx0.DrawLine(aPen, startX, startY + offsetY - 8, startX + endX,
               startY + offsetY - 8);

                }
                string orgType = Global.mnFrm.cmCde.getPssblValNm(int.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
            "org.org_details", "org_id", "org_typ_id", Global.mnFrm.cmCde.Org_id)));

                //Person Types
                float oldoffsetY = offsetY;
                float hgstOffsetY = 0;
                float hghstght = 0;

                DataSet dtst = Global.get_PayRunSmmry(this.msPyListView.SelectedItems[0].SubItems[1].Text);

                oldoffsetY = offsetY;
                offsetY = oldoffsetY + 5;

                double earngsAmnt = 0;
                double empChrgAmnt = 0;
                double deductAmnt = 0;
                double tkHmAmnt = 0;

                startX = startXNw;

                for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
                {
                    startX = startXNw;
                    if (a == 0)
                    {
                        hgstOffsetY = 0;
                        ght = (float)gfx0.MeasureString(
                        ("PAY RUN SUMMARY REPORT (" + this.trnsDateTextBox.Text + ")").ToUpper(), xfont2).Height;
                        //lblght = ght;
                        XTextFormatter tf = new XTextFormatter(gfx0);
                        XRect rect = new XRect(startX, startY + offsetY, pageWidth, ght);
                        gfx0.DrawRectangle(XBrushes.LightGray, rect);
                        tf.DrawString(("PAY RUN SUMMARY REPORT (" + this.trnsDateTextBox.Text + ")").ToUpper()
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
                                gfx0.DrawRectangle(XBrushes.LightGray, rect);
                                tf.DrawString(" " + dtst.Tables[0].Columns[j].Caption
                                  , xfont2, XBrushes.Black, rect, XStringFormats.TopLeft);
                                startX += wdth + 10;
                            }
                        }
                        offsetY += (int)ght + 5;
                        startX = startXNw;
                    }
                    hghstght = 0;
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
                            string strToBreak = dtst.Tables[0].Rows[a][j].ToString();

                            if (j >= 3 && j <= 6)
                            {
                                double tst = 0;
                                if (double.TryParse(strToBreak, out tst) == false)
                                {
                                    strToBreak = "0";
                                }
                                strToBreak = double.Parse(strToBreak).ToString("#,##0.00");
                                if (j == 3)
                                {
                                    earngsAmnt += double.Parse(strToBreak);
                                }
                                if (j == 4)
                                {
                                    empChrgAmnt += double.Parse(strToBreak);
                                }
                                if (j == 5)
                                {
                                    deductAmnt += double.Parse(strToBreak);
                                }
                                if (j == 6)
                                {
                                    tkHmAmnt += double.Parse(strToBreak);
                                }
                            }

                            nwLn = Global.mnFrm.cmCde.breakTxtDown(
                     strToBreak, (int)(wdth * 1.3), font41, g);
                            //    if (j == 1 || j == 2)
                            //    {
                            //      nwLn = Global.mnFrm.cmCde.breakPOSTxtDown(strToBreak,
                            //(int)(wdth * 1.2), font41, g, 14);
                            //    }
                            //    else
                            //    {
                            //    }
                            string finlStr = "";
                            if (j >= 3 && j <= 6)
                            {
                                finlStr = string.Join("\n", nwLn).PadLeft(10);
                                //if (j == 3)
                                //{
                                //  finlStr = string.Join("\n", nwLn).PadLeft(15);
                                //}
                                //else
                                //{
                                //}
                            }
                            else
                            {
                                finlStr = string.Join("\n", nwLn);
                            }
                            ght = (float)gfx0.MeasureString(
                           finlStr, xfont41).Height;

                            XTextFormatter tf = new XTextFormatter(gfx0);
                            XRect rect = new XRect(startX, startY + offsetY, wdth + 5, ght);
                            gfx0.DrawRectangle(XBrushes.White, rect);


                            tf.DrawString(finlStr
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

                //offsetY += hghstght + 5;
                hghstght = 0;
                startX = startXNw;
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
                        string strToBreak = " ";
                        if (j == 2)
                        {
                            strToBreak = "TOTALS = ";
                        }
                        if (j >= 3 && j <= 6)
                        {
                            if (j == 3)
                            {
                                strToBreak = (earngsAmnt).ToString("#,##0.00");
                            }
                            else if (j == 4)
                            {
                                strToBreak = (empChrgAmnt).ToString("#,##0.00");
                            }
                            else if (j == 5)
                            {
                                strToBreak = (deductAmnt).ToString("#,##0.00");
                            }
                            else if (j == 6)
                            {
                                strToBreak = (tkHmAmnt).ToString("#,##0.00");
                            }
                        }
                        nwLn = Global.mnFrm.cmCde.breakPDFTxtDown(
                          strToBreak,
                          (int)(wdth * 1.2), font31, g);

                        string finlStr = "";
                        if (j >= 3 && j <= 6)
                        {
                            finlStr = string.Join("\n", nwLn).PadLeft(10);
                            //if (j == 3)
                            //{
                            //  finlStr = string.Join("\n", nwLn).PadLeft(5);
                            //}
                            //else if (j == 6)
                            //{
                            //  finlStr = string.Join("\n", nwLn).PadLeft(15);
                            //}
                            //else
                            //{
                            //}
                        }
                        else
                        {
                            finlStr = string.Join("\n", nwLn);
                        }
                        ght = (float)gfx0.MeasureString(
                       finlStr, xfont31).Height;

                        XTextFormatter tf = new XTextFormatter(gfx0);
                        XRect rect = new XRect(startX, startY + offsetY, wdth + 5, ght);
                        gfx0.DrawRectangle(XBrushes.White, rect);


                        tf.DrawString(finlStr
                          , xfont31, XBrushes.Black, rect, XStringFormats.TopLeft);

                        startX += wdth + 10;
                        if (hghstght < ght)
                        {
                            hghstght = ght;
                        }
                    }
                }
                offsetY += hghstght + 5;
                //Slogan: 
                startX = startXNw;
                if (offsetY < 705)
                {
                    offsetY = 705;
                }
                gfx0.DrawLine(aPen, startX, startY + offsetY, startX + endX,
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
                string filename = Global.mnFrm.cmCde.getRptDrctry() + @"\PayRunSmmry_" + Global.mnFrm.cmCde.getDB_Date_time().Replace(" ", "").Replace(":", "").Replace("-", "") + ".pdf";
                document.Save(filename);
                // ...and start a viewer.
                System.Diagnostics.Process.Start(filename);
                //Global.mnFrm.cmCde.upldImgsFTP(9, Global.mnFrm.cmCde.getRptDrctry(), @"\PayRunSmmry_" + Global.mnFrm.cmCde.getDB_Date_time().Replace(" ", "").Replace(":", "").Replace("-", "") + ".pdf");
                System.Windows.Forms.Application.DoEvents();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.Application.DoEvents();
                Global.mnFrm.cmCde.showMsg(ex.Message + "\r\n\r\n" + ex.InnerException + "\r\n\r\n" + ex.StackTrace, 0);
            }
        }

        private void exprtPymntsMenuItem_Click(object sender, EventArgs e)
        {
            this.exptPymtsTmpButton_Click(this.exptPymtsTmpButton, e);
        }

        private void imprtPymntsExclMenuItem_Click(object sender, EventArgs e)
        {
            this.imptPymtsTmpButton_Click(this.imptPymtsTmpButton, e);
        }

        private void exptPymtsTmpButton_Click(object sender, EventArgs e)
        {
            if (this.prsStIDMnlTextBox.Text == "" || this.prsStIDMnlTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select a Person Set First!", 0);
                return;
            }
            if (this.itmStIDMnlTextBox.Text == "" || this.itmStIDMnlTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select an Item Set First!", 0);
                return;
            }
            Global.mnFrm.cmCde.exprtPymntsTmp(int.Parse(this.prsStIDMnlTextBox.Text),
              int.Parse(this.itmStIDMnlTextBox.Text));
        }

        private void imptPymtsTmpButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[9]) == false)
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
                Global.mnFrm.cmCde.imprtPymntsTmp(this.openFileDialog1.FileName);
            }
            this.loadOrgPersons();
        }

        private void rvrsPymntButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[10]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                  " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.pstPayListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the Payment to Reverse!", 0);
                return;
            }
            if (this.itmListViewPymnt.SelectedItems[0].SubItems[3].Text.ToUpper() == "Balance Item".ToUpper())
            {
                Global.mnFrm.cmCde.showMsg("Cannot reverse the Balance on a Balance Item!", 0);
                return;
            }
            bool beenrvsrdB4 = false;
            if (Global.mnFrm.cmCde.showMsg("NB: This transaction is not complete until you click" +
              "\r\n on the PROCESS PAYMENT Button above!\r\nAre you sure you want to Proceed?", 1) == DialogResult.No)
            {
                Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            DataSet dtst = Global.getPstPayDet(long.Parse(this.pstPayListView.SelectedItems[0].SubItems[5].Text));
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

        private void prsNamesListView_KeyDown(object sender, KeyEventArgs e)
        {
            this.obey_prs_evnts = false;
            Global.mnFrm.cmCde.listViewKeyDown(this.prsNamesListView, e);
            this.obey_prs_evnts = true;
        }

        private void itmListView_KeyDown(object sender, KeyEventArgs e)
        {
            this.obey_itm_evnts1 = false;
            Global.mnFrm.cmCde.listViewKeyDown(this.itmListViewPymnt, e);
            this.obey_itm_evnts1 = true;
        }

        private void pstPayListView_KeyDown(object sender, KeyEventArgs e)
        {
            Global.mnFrm.cmCde.listViewKeyDown(this.pstPayListView, e);
        }
        #endregion
        #region "Item Persons..."
        private void loadOrgPersons()
        {
            this.obey_prs_evnts = false;
            if (this.searchInPrsComboBox.SelectedIndex < 0)
            {
                this.searchInPrsComboBox.SelectedIndex = 1;
            }
            if (this.searchForPrsTextBox.Text.Contains("%") == false)
            {
                this.searchForPrsTextBox.Text = "%" + this.searchForPrsTextBox.Text.Replace(" ", "%") + "%";
            }
            if (this.searchForPrsTextBox.Text == "%%")
            {
                this.searchForPrsTextBox.Text = "%";
            }
            int dsply = 0;
            if (this.dsplySizePrsComboBox.Text == ""
              || int.TryParse(this.dsplySizePrsComboBox.Text, out dsply) == false)
            {
                this.dsplySizePrsComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            long dsply1 = 0;
            if (this.prsStIDMnlTextBox.Text == "" || this.prsStIDMnlTextBox.Text == "-1"
              || long.TryParse(this.prsStIDMnlTextBox.Text, out dsply1) == false)
            {
                string[] vl = Global.get_Org_DfltPrsSt(Global.mnFrm.cmCde.Org_id);
                this.prsStIDMnlTextBox.Text = vl[0];
                this.prsStNmMnlTextBox.Text = vl[1];
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
            myNav.FindNavigationIndices(
              int.Parse(this.dsplySizePrsComboBox.Text), this.totl_prs);
            if (this.prs_cur_indx >= myNav.totalGroups)
            {
                this.prs_cur_indx = myNav.totalGroups - 1;
            }
            if (this.prs_cur_indx < 0)
            {
                this.prs_cur_indx = 0;
            }
            myNav.currentNavigationIndex = this.prs_cur_indx;
        }

        private void updtPrsNavLabels()
        {
            this.moveFirstPrsButton.Enabled = myNav.moveFirstBtnStatus();
            this.movePreviousPrsButton.Enabled = myNav.movePrevBtnStatus();
            this.moveNextPrsButton.Enabled = myNav.moveNextBtnStatus();
            this.moveLastPrsButton.Enabled = myNav.moveLastBtnStatus();
            this.positionPrsTextBox.Text = myNav.displayedRecordsNumbers();
            if (this.is_last_prs == true ||
              this.totl_prs != Global.mnFrm.cmCde.Big_Val)
            {
                this.totalRecPrsLabel.Text = myNav.totalRecordsLabel();
            }
            else
            {
                this.totalRecPrsLabel.Text = "of Total";
            }
        }

        private void populatePrs()
        {
            this.obey_prs_evnts = false;
            this.prsNamesListView.Items.Clear();
            DataSet dtst = Global.get_Org_Persons(
              this.searchForPrsTextBox.Text,
              this.searchInPrsComboBox.Text, this.prs_cur_indx,
              int.Parse(this.dsplySizePrsComboBox.Text)
              , Global.mnFrm.cmCde.Org_id, int.Parse(this.prsStIDMnlTextBox.Text));
            this.obey_prs_evnts = false;
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.last_prs_num = myNav.startIndex() + i;
                ListViewItem nwItem = new ListViewItem(new string[] {
    (myNav.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),dtst.Tables[0].Rows[i][2].ToString(),
    dtst.Tables[0].Rows[i][0].ToString(), dtst.Tables[0].Rows[i][3].ToString()});
                this.prsNamesListView.Items.Add(nwItem);
            }
            this.correctPrsNavLbls(dtst);
            if (this.prsNamesListView.Items.Count > 0)
            {
                this.obey_prs_evnts = true;
                this.prsNamesListView.Items[0].Selected = true;
            }
            else
            {
                int dsply = 0;
                this.itmListViewPymnt.Items.Clear();
                this.prsPictureBox.Image = InternalPayments.Properties.Resources.staffs;
                this.clearMnlPay();
                this.itm_cur_indx1 = 0;
                this.totl_itm1 = 0;
                this.last_itm_num1 = 0;
                if (this.dsplySizeItmComboBoxNw.Text == ""
            || int.TryParse(this.dsplySizeItmComboBoxNw.Text, out dsply) == false)
                {
                    this.dsplySizeItmComboBoxNw.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
                }
                this.updtItmTotals1();
                this.updtItmNavLabels1();

                this.pstPayListView.Items.Clear();
                this.clearMnlPay();
                this.pst_cur_indx = 0;
                this.totl_pst = 0;
                this.last_pst_num = 0;
                if (this.dsplySizePstComboBox.Text == ""
            || int.TryParse(this.dsplySizePstComboBox.Text, out dsply) == false)
                {
                    this.dsplySizePstComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
                }
                this.updtPstTotals();
                this.updtPstNavLabels();
            }
            this.obey_prs_evnts = true;
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
                  , int.Parse(this.prsStIDMnlTextBox.Text));
                this.updtPrsTotals();
                this.prs_cur_indx = myNav.totalGroups - 1;
            }
            this.getOrgPrsData();
        }

        private void loadCorrectPanel()
        {
            this.obey_evnts = false;
            if (this.curTabIndx == "payTabPage")
            {
                this.loadPayItmsPanel();
            }
            else if (this.curTabIndx == "asgdPyItmsTabPage")
            {
                this.loadPersBnftsPanel();
            }
            else if (this.curTabIndx == "prsBanksTabPage")
            {
                this.loadPersBanksPanel();
            }
            this.obey_evnts = true;
        }

        private void loadPersBnftsPanel()
        {
            //if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[4]) == false)
            //{
            //  Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            //   " this action!\nContact your System Administrator!", 0);
            //  return;
            //}
            this.loadPyItmsPanelPrs();
        }

        private void loadPersBanksPanel()
        {
            //if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[16]) == false)
            //{
            //  Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            //   " this action!\nContact your System Administrator!", 0);
            //  return;
            //}
            if (this.prsNamesListView.SelectedItems.Count > 0)
            {
                this.populateAccounts(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
            }
            else
            {
                this.populateAccounts(-10000000010);
            }
        }

        private void prsNamesListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.shdObeyPrsEvts() == false || this.prsNamesListView.SelectedItems.Count > 1)
            {
                return;
            }
            this.prsPictureBox.Image = InternalPayments.Properties.Resources.staffs;
            this.clearMnlPay();
            if (this.prsNamesListView.SelectedItems.Count > 0)
            {
                Global.mnFrm.cmCde.getDBImageFile(this.prsNamesListView.SelectedItems[0].SubItems[4].Text,
            2, ref this.prsPictureBox);
                loadCorrectPanel();
            }
            else
            {
                int dsply = 0;
                this.itmListViewPymnt.Items.Clear();
                this.prsPictureBox.Image = InternalPayments.Properties.Resources.staffs;
                this.clearMnlPay();
                this.itm_cur_indx1 = 0;
                this.totl_itm1 = 0;
                this.last_itm_num1 = 0;
                if (this.dsplySizeItmComboBoxNw.Text == ""
            || int.TryParse(this.dsplySizeItmComboBoxNw.Text, out dsply) == false)
                {
                    this.dsplySizeItmComboBoxNw.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
                }
                this.updtItmTotals1();
                this.updtItmNavLabels1();

                this.pstPayListView.Items.Clear();
                this.clearMnlPay();
                this.pst_cur_indx = 0;
                this.totl_pst = 0;
                this.last_pst_num = 0;
                if (this.dsplySizePstComboBox.Text == ""
            || int.TryParse(this.dsplySizePstComboBox.Text, out dsply) == false)
                {
                    this.dsplySizePstComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
                }
                this.updtPstTotals();
                this.updtPstNavLabels();
            }
        }

        private void exptPayMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.itmListViewPymnt);
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
                this.searchForPrsTextBox.Focus();
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

        private void rfrshPrsnMenuItem_Click(object sender, EventArgs e)
        {
            this.goPrsButton_Click(this.goPrsButton, e);
        }

        private void vwSQLPrsnMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(this.prs_SQL, 8);
        }

        private void rcHstryPrsnMenuItem_Click(object sender, EventArgs e)
        {
            if (this.prsNamesListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(
              Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(
              this.prsNamesListView.SelectedItems[0].SubItems[3].Text),
              "prs.prsn_names_nos", "person_id"), 7);
        }

        private void goPrsButton_Click(object sender, EventArgs e)
        {
            this.loadOrgPersons();
        }

        private void itmListView_ItemSelectionChanged(object sender, System.Windows.Forms.ListViewItemSelectionChangedEventArgs e)
        {
            if (this.shdObeyItmEvts1() == false)
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

        private void prsNamesListView_ItemSelectionChanged(object sender, System.Windows.Forms.ListViewItemSelectionChangedEventArgs e)
        {
            if (this.shdObeyPrsEvts() == false)
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
        #region "Past Payments..."
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
            this.paymntDateTextBox.Text = "";
            string dateStr = DateTime.ParseExact(
         Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
            this.glDateTextBox.Text = dateStr;
            if (this.itmListViewPymnt.SelectedItems[0].SubItems[5].Text == "Earnings"
              || this.itmListViewPymnt.SelectedItems[0].SubItems[5].Text == "Employer Charges")
            {
                this.trnsTypComboBox.Items.Clear();
                this.trnsTypComboBox.Items.Add("Payment by Organisation");
                this.trnsTypComboBox.SelectedIndex = 0;
                this.paymntDescTextBox.Text = "Payment of " +
            this.itmListViewPymnt.SelectedItems[0].SubItems[1].Text +
            " for " + this.prsNamesListView.SelectedItems[0].SubItems[2].Text
            + " (" + this.prsNamesListView.SelectedItems[0].SubItems[1].Text + ")";
            }
            else if (this.itmListViewPymnt.SelectedItems[0].SubItems[5].Text == "Bills/Charges"
              || this.itmListViewPymnt.SelectedItems[0].SubItems[5].Text == "Deductions"
              || this.itmListViewPymnt.SelectedItems[0].SubItems[5].Text == "Deductions"
                || this.itmListViewPymnt.SelectedItems[0].SubItems[5].Text == "Deductions")
            {
                this.trnsTypComboBox.Items.Clear();
                this.trnsTypComboBox.Items.Add("Payment by Person");
                this.trnsTypComboBox.SelectedIndex = 0;
                this.paymntDescTextBox.Text = "Payment of " +
            this.itmListViewPymnt.SelectedItems[0].SubItems[1].Text +
            " by " + this.prsNamesListView.SelectedItems[0].SubItems[2].Text
            + " (" + this.prsNamesListView.SelectedItems[0].SubItems[1].Text + ")";
            }
            else if (this.itmListViewPymnt.SelectedItems[0].SubItems[3].Text.ToUpper() == "Balance Item".ToUpper())
            {
                this.trnsTypComboBox.Items.Clear();
            }
            else
            {
                this.trnsTypComboBox.Items.Clear();
                this.trnsTypComboBox.Items.Add("Purely Informational");
                this.trnsTypComboBox.SelectedIndex = 0;
                this.paymntDescTextBox.Text = "Running of Purely Informational Item " +
            this.itmListViewPymnt.SelectedItems[0].SubItems[1].Text +
            " for " + this.prsNamesListView.SelectedItems[0].SubItems[2].Text
            + " (" + this.prsNamesListView.SelectedItems[0].SubItems[1].Text + ")";
            }
            string valSQL = Global.mnFrm.cmCde.getItmValSQL(int.Parse(this.itmListViewPymnt.SelectedItems[0].SubItems[7].Text));
            if (valSQL == "")
            {
                this.expctdAmntTextBox.Text = Global.mnFrm.cmCde.getItmValueAmnt(int.Parse(this.itmListViewPymnt.SelectedItems[0].SubItems[7].Text)).ToString("#,##0.00");
            }
            else
            {
                this.expctdAmntTextBox.Text = Global.mnFrm.cmCde.exctItmValSQL(
                  valSQL, long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text),
                  Global.mnFrm.cmCde.Org_id, dateStr).ToString("#,##0.00");
            }
            if (this.itmListViewPymnt.SelectedItems[0].SubItems[6].Text == "Money")
            {
                this.crncyIDTextBox.Text = Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id).ToString();
                this.crncyTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(int.Parse(this.crncyIDTextBox.Text));
            }
            else
            {
                this.crncyIDTextBox.Text = "-1";
                this.crncyTextBox.Text = this.itmListViewPymnt.SelectedItems[0].SubItems[6].Text;
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
                this.dsplySizePstComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            //this.groupBox8.Height = this.g.Bottom - this.toolStrip4.Bottom - 50;
            this.is_last_pst = false;
            this.totl_pst = Global.mnFrm.cmCde.Big_Val;
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
            this.myNav1.FindNavigationIndices(
              long.Parse(this.dsplySizePstComboBox.Text), this.totl_pst);
            if (this.pst_cur_indx >= this.myNav1.totalGroups)
            {
                this.pst_cur_indx = this.myNav1.totalGroups - 1;
            }
            if (this.pst_cur_indx < 0)
            {
                this.pst_cur_indx = 0;
            }
            this.myNav1.currentNavigationIndex = this.pst_cur_indx;
        }

        private void updtPstNavLabels()
        {
            this.moveFirstPstButton.Enabled = this.myNav1.moveFirstBtnStatus();
            this.movePreviousPstButton.Enabled = this.myNav1.movePrevBtnStatus();
            this.moveNextPstButton.Enabled = this.myNav1.moveNextBtnStatus();
            this.moveLastPstButton.Enabled = this.myNav1.moveLastBtnStatus();
            this.positionPstTextBox.Text = this.myNav1.displayedRecordsNumbers();
            if (this.is_last_pst == true ||
              this.totl_pst != Global.mnFrm.cmCde.Big_Val)
            {
                this.totalRecsPstLabel.Text = this.myNav1.totalRecordsLabel();
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
            if (this.itmListViewPymnt.SelectedItems.Count <= 0
              || this.prsNamesListView.SelectedItems.Count <= 0)
            {
                this.is_last_pst = true;
                this.totl_pst = 0;
                this.last_pst_num = 0;
                this.pst_cur_indx = 0;
                this.updtPstTotals();
                this.updtPstNavLabels();
                return;
            }
            if (this.itmListViewPymnt.SelectedItems[0].SubItems[3].Text.ToUpper() == "Balance Item".ToUpper())
            {
                dtst = Global.get_Basic_PstBls(this.searchForPstTextBox.Text,
             this.searchInPstComboBox.Text, this.pst_cur_indx,
             int.Parse(this.dsplySizePstComboBox.Text),
             long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text),
             long.Parse(this.itmListViewPymnt.SelectedItems[0].SubItems[4].Text));
            }
            else
            {
                dtst = Global.get_Basic_Pst(this.searchForPstTextBox.Text,
                 this.searchInPstComboBox.Text, this.pst_cur_indx,
                 int.Parse(this.dsplySizePstComboBox.Text),
                 long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text),
                 long.Parse(this.itmListViewPymnt.SelectedItems[0].SubItems[4].Text));
            }
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.last_pst_num = this.myNav1.startIndex() + i;
                string uom = "Number";
                string amnt = "0.00";
                if (this.itmListViewPymnt.SelectedItems[0].SubItems[6].Text.ToUpper() == "Money".ToUpper())
                {
                    if (this.itmListViewPymnt.SelectedItems[0].SubItems[3].Text.ToUpper() == "Balance Item".ToUpper())
                    {
                        uom = "Money";
                    }
                    else
                    {
                        uom = Global.mnFrm.cmCde.getPssblValNm(int.Parse(dtst.Tables[0].Rows[i][4].ToString()));
                    }
                    amnt = double.Parse(dtst.Tables[0].Rows[i][1].ToString()).ToString("#,#0.00");
                }
                else
                {
                    amnt = double.Parse(dtst.Tables[0].Rows[i][1].ToString()).ToString("#,#0");
                }
                ListViewItem nwItem = new ListViewItem(new string[] {
    (this.myNav1.startIndex() + i).ToString(),
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
              && this.itmListViewPymnt.SelectedItems[0].SubItems[3].Text.ToUpper() == "Balance Item".ToUpper())
            {
                string uom = "Number";
                string amnt = "0.00";
                if (this.itmListViewPymnt.SelectedItems[0].SubItems[6].Text.ToUpper() == "Money".ToUpper())
                {
                    if (this.itmListViewPymnt.SelectedItems[0].SubItems[3].Text.ToUpper() == "Balance Item".ToUpper())
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
            Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
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
            else if (this.totl_pst == Global.mnFrm.cmCde.Big_Val
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
                if (this.itmListViewPymnt.SelectedItems[0].SubItems[3].Text.ToUpper() == "Balance Item".ToUpper())
                {
                    this.totl_pst = Global.get_Total_PstBls(this.searchForPstTextBox.Text,
               this.searchInPstComboBox.Text,
               long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text),
               long.Parse(this.itmListViewPymnt.SelectedItems[0].SubItems[4].Text));
                }
                else
                {
                    this.totl_pst = Global.get_Total_Pst(this.searchForPstTextBox.Text,
                this.searchInPstComboBox.Text,
               long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text),
               long.Parse(this.itmListViewPymnt.SelectedItems[0].SubItems[4].Text));

                }
                this.updtPstTotals();
                this.pst_cur_indx = this.myNav1.totalGroups - 1;
            }
            this.getPstPnlData();
        }

        private void reversePaymentMenuItem_Click(object sender, EventArgs e)
        {
            this.rvrsPymntButton_Click(this.rvrsPymntButton, e);
        }

        private void exptPstMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.pstPayListView);
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

        private void refreshPstMenuItem_Click(object sender, EventArgs e)
        {
            this.goPstButton_Click(this.goPstButton, e);
        }

        private void viewSQLPstMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(this.pst_SQL, 8);
        }

        private void recHstryPstMenuItem_Click(object sender, EventArgs e)
        {
            if (this.pstPayListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(
              Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(
              this.pstPayListView.SelectedItems[0].SubItems[5].Text),
              "pay.pay_itm_trnsctns", "pay_trns_id"), 7);
        }

        private void printPstPyMenuItem_Click(object sender, EventArgs e)
        {
            if (this.pstPayListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a processed Payment First!", 0);
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

            this.printPreviewDialog1.FindForm().Height = Global.mnFrm.Height;
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
                                                                    //Global.mnFrm.cmCde.showMsg(pageWidth.ToString(), 0);
            int startX = 10;
            int startY = 20;
            int offsetY = 0;
            //StringBuilder strPrnt = new StringBuilder();
            //strPrnt.AppendLine("Received From");
            string[] nwLn;

            if (this.pageNo == 1)
            {
                //Org Name
                nwLn = Global.mnFrm.cmCde.breakTxtDown(
                  Global.mnFrm.cmCde.getOrgName(Global.mnFrm.cmCde.Org_id),
                  pageWidth, font2, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    g.DrawString(nwLn[i]
                    , font2, Brushes.Black, startX, startY + offsetY);
                    offsetY += font2Hght;
                }

                //Pstal Address
                g.DrawString(Global.mnFrm.cmCde.getOrgPstlAddrs(Global.mnFrm.cmCde.Org_id).Trim(),
                font2, Brushes.Black, startX, startY + offsetY);
                //offsetY += font2Hght;

                float ght = g.MeasureString(
                  Global.mnFrm.cmCde.getOrgPstlAddrs(Global.mnFrm.cmCde.Org_id).Trim(), font2).Height;
                offsetY = offsetY + (int)ght;
                //Contacts Nos
                nwLn = Global.mnFrm.cmCde.breakTxtDown(
            Global.mnFrm.cmCde.getOrgContactNos(Global.mnFrm.cmCde.Org_id),
            pageWidth, font2, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    g.DrawString(nwLn[i]
                    , font2, Brushes.Black, startX, startY + offsetY);
                    offsetY += font2Hght;
                }
                //Email Address
                nwLn = Global.mnFrm.cmCde.breakTxtDown(
            Global.mnFrm.cmCde.getOrgEmailAddrs(Global.mnFrm.cmCde.Org_id),
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
                nwLn = Global.mnFrm.cmCde.breakTxtDown(
            this.itmListViewPymnt.SelectedItems[0].SubItems[1].Text,
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
                nwLn = Global.mnFrm.cmCde.breakTxtDown(
            this.prsNamesListView.SelectedItems[0].SubItems[2].Text +
                  " (" + this.prsNamesListView.SelectedItems[0].SubItems[1].Text + ")",
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
                nwLn = Global.mnFrm.cmCde.breakTxtDown(
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
                nwLn = Global.mnFrm.cmCde.breakTxtDown(
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

                nwLn = Global.mnFrm.cmCde.breakTxtDown(
                  Global.mnFrm.cmCde.getOrgSlogan(Global.mnFrm.cmCde.Org_id),
            pageWidth - ght, font5, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    g.DrawString(nwLn[i]
                    , font5, Brushes.Black, startX, startY + offsetY);
                    offsetY += font5Hght;
                }


                //offsetY += font5Hght;

                nwLn = Global.mnFrm.cmCde.breakTxtDown(
                 "Software Developed by Rhomicom Systems Technologies Ltd.",
            pageWidth + 40, font5, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    g.DrawString(nwLn[i]
                    , font5, Brushes.Black, startX, startY + offsetY);
                    offsetY += font5Hght;
                }
                nwLn = Global.mnFrm.cmCde.breakTxtDown(
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
                Global.mnFrm.cmCde.showMsg("Please select a processed Payment First!", 0);
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
                long paytrnsid = Global.getPaymntTrnsID(
                prsn_id, itm_id,
                pay_amnt, trns_date, orgnlTrnsID);
                //Create GL Lines based on item's defined accounts
                string[] accntinf = new string[4];
                double netamnt = 0;
                accntinf = Global.get_ItmAccntInfo(itm_id);

                if (itm_uom != "Number" && int.Parse(accntinf[1]) > 0 && int.Parse(accntinf[3]) > 0)
                {

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
                Global.mnFrm.cmCde.updateLogMsg(msg_id,
            "\r\nError Sending Payment to GL Interface for Person:" +
            loc_id_no + " Item: " + itm_name + " " + ex.Message, log_tbl, dateStr);
                return false;
            }
        }

        private bool sendToGLInterfaceRetro(
      long prsn_id, string loc_id_no, long itm_id,
      string itm_name, string itm_uom, double pay_amnt,
      string trns_date, string trns_desc,
      int crncy_id, long msg_id, string log_tbl,
      string dateStr, string trns_src, string glDate, long orgnlTrnsID, string dteEarned)
        {
            try
            {
                //Get Todays GL Batch Name
                long paytrnsid = Global.getPaymntTrnsIDREtro(
                prsn_id, itm_id,
                pay_amnt, trns_date, dteEarned, orgnlTrnsID);
                //Create GL Lines based on item's defined accounts
                string[] accntinf = new string[4];
                double netamnt = 0;
                accntinf = Global.get_ItmAccntInfo(itm_id);

                if (itm_uom != "Number" && int.Parse(accntinf[1]) > 0 && int.Parse(accntinf[3]) > 0)
                {
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
                Global.mnFrm.cmCde.updateLogMsg(msg_id,
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

        private bool sendToGL()
        {
            try
            {
                //Get Todays GL Batch Name
                string dateStr = DateTime.ParseExact(
            Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
            System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
                string todaysGlBatch = "Internal Payments (" + dateStr + ")";
                long todbatchid = Global.getTodaysGLBatchID(
                  todaysGlBatch,
                  Global.mnFrm.cmCde.Org_id);
                if (todbatchid <= 0)
                {
                    Global.createTodaysGLBatch(Global.mnFrm.cmCde.Org_id,
                      todaysGlBatch, todaysGlBatch, "Internal Payments");
                    todbatchid = Global.getTodaysGLBatchID(
                    todaysGlBatch,
                    Global.mnFrm.cmCde.Org_id);
                }
                if (todbatchid > 0)
                {
                    todaysGlBatch = Global.get_GLBatch_Nm(todbatchid);
                }

                /*
                 * 1. Get list of all accounts to transfer from the 
                 * interface table and their total amounts.
                 * 2. Loop through each and transfer
                 */
                DataSet dtst = Global.getAllInGLIntrfcOrg(Global.mnFrm.cmCde.Org_id);
                long cntr = dtst.Tables[0].Rows.Count;

                if (cntr > 0)
                {
                    double dfrnce = 0;
                    if (Global.isGLIntrfcBlcdOrg(Global.mnFrm.cmCde.Org_id, ref dfrnce) == false)
                    {
                        Global.mnFrm.cmCde.showMsg("Cannot Transfer Transactions to GL because\r\n" +
                          " Transactions in the GL Interface are not Balanced!" +
                        "\r\nDIFFERENCE=" + dfrnce.ToString(), 0);
                        return false;
                    }
                }
                else
                {
                    //Global.mnFrm.cmCde.showMsg("There is nothing in the GL Interface Table to Transfer!", 0);
                    //return false;
                }

                //dateStr = Global.mnFrm.cmCde.getFrmtdDB_Date_time();
                for (int a = 0; a < cntr; a++)
                {
                    string src_ids = Global.getGLIntrfcIDs(int.Parse(dtst.Tables[0].Rows[a][0].ToString()),
                      dtst.Tables[0].Rows[a][1].ToString(),
                      int.Parse(dtst.Tables[0].Rows[a][5].ToString()));

                    double entrdAmnt = double.Parse(dtst.Tables[0].Rows[a][2].ToString()) == 0 ? double.Parse(dtst.Tables[0].Rows[a][3].ToString()) : double.Parse(dtst.Tables[0].Rows[a][2].ToString());
                    string dbtCrdt = double.Parse(dtst.Tables[0].Rows[a][3].ToString()) == 0 ? "D" : "C";
                    int accntCurrID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
               "accb.accb_chart_of_accnts", "accnt_id", "crncy_id", int.Parse(dtst.Tables[0].Rows[a][0].ToString())));

                    double accntCurrRate = Math.Round(
                      Global.get_LtstExchRate(int.Parse(dtst.Tables[0].Rows[a][5].ToString()), accntCurrID,
               dtst.Tables[0].Rows[a][1].ToString()), 15);

                    double[] actlAmnts = Global.getGLIntrfcIDAmntSum(src_ids, int.Parse(dtst.Tables[0].Rows[a][0].ToString()));

                    if (actlAmnts[0] == double.Parse(dtst.Tables[0].Rows[a][2].ToString())
                      && actlAmnts[1] == double.Parse(dtst.Tables[0].Rows[a][3].ToString()))
                    {

                        Global.createPymntGLLine(int.Parse(dtst.Tables[0].Rows[a][0].ToString()),
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
                        Global.mnFrm.cmCde.showMsg("Interface Transaction Amounts DR:" + actlAmnts[0] + " CR:" + actlAmnts[1] +
                  " \r\ndo not match Amount being sent to GL DR:" + double.Parse(dtst.Tables[0].Rows[a][2].ToString()) +
                  " CR:" + double.Parse(dtst.Tables[0].Rows[a][3].ToString()) + "!\r\n Interface Line IDs:" + src_ids, 0);
                        break;
                    }
                }
                if (Global.get_Batch_CrdtSum(todbatchid) == Global.get_Batch_DbtSum(todbatchid))
                {
                    Global.updtPymntAllGLIntrfcLnOrg(todbatchid, Global.mnFrm.cmCde.Org_id);
                    Global.updtGLIntrfcLnSpclOrg(Global.mnFrm.cmCde.Org_id);
                    Global.updtTodaysGLBatchPstngAvlblty(todbatchid, "1");
                    return true;
                }
                else
                {
                    Global.mnFrm.cmCde.showMsg("The GL Batch created is not Balanced!\r\nTransactions created will be reversed and deleted!", 0);
                    Global.deleteBatchTrns(todbatchid);
                    Global.deleteBatch(todbatchid, todaysGlBatch);
                    return false;
                }
                //Global.updtPymntAllGLIntrfcLnOrg(todbatchid, Global.mnFrm.cmCde.Org_id);
                //Global.updtGLIntrfcLnSpclOrg(Global.mnFrm.cmCde.Org_id);
                //return true;
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg("Error Sending Payment to GL!\r\n" + ex.Message, 0);
                return false;
            }
        }

        private bool sendToGL(long py_trns_id)
        {
            try
            {
                //Get Todays GL Batch Name
                string dateStr = DateTime.ParseExact(
            Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
            System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
                string todaysGlBatch = "Internal Payments (" + dateStr + ")";
                long todbatchid = Global.getTodaysGLBatchID(
                  todaysGlBatch,
                  Global.mnFrm.cmCde.Org_id);
                if (todbatchid <= 0)
                {
                    Global.createTodaysGLBatch(Global.mnFrm.cmCde.Org_id,
                      todaysGlBatch, todaysGlBatch, "Internal Payments");
                    todbatchid = Global.getTodaysGLBatchID(
                    todaysGlBatch,
                    Global.mnFrm.cmCde.Org_id);
                }
                if (todbatchid > 0)
                {
                    todaysGlBatch = Global.get_GLBatch_Nm(todbatchid);
                }

                /*
                 * 1. Get list of all accounts to transfer from the 
                 * interface table and their total amounts.
                 * 2. Loop through each and transfer
                 */

                DataSet dtst = Global.getAllInGLIntrfc(py_trns_id);
                long cntr = dtst.Tables[0].Rows.Count;

                if (cntr > 0)
                {
                    if (Global.isGLIntrfcBlcd(py_trns_id) == false)
                    {
                        Global.mnFrm.cmCde.showMsg("Cannot Transfer Transactions to GL because\r\n" +
                          " Transactions in the GL Interface for this Payment are not Balanced!", 0);
                        return false;
                    }
                }
                else
                {
                    //Global.mnFrm.cmCde.showMsg("There is nothing in the GL Interface Table to Transfer!", 0);
                    //return false;
                }

                for (int a = 0; a < cntr; a++)
                {
                    string src_ids = Global.getGLIntrfcIDsMnl(
                      int.Parse(dtst.Tables[0].Rows[a][0].ToString()),
                      dtst.Tables[0].Rows[a][1].ToString(),
                      int.Parse(dtst.Tables[0].Rows[a][5].ToString()), py_trns_id);

                    double entrdAmnt = double.Parse(dtst.Tables[0].Rows[a][2].ToString()) == 0 ? double.Parse(dtst.Tables[0].Rows[a][3].ToString()) : double.Parse(dtst.Tables[0].Rows[a][2].ToString());
                    string dbtCrdt = double.Parse(dtst.Tables[0].Rows[a][3].ToString()) == 0 ? "D" : "C";
                    int accntCurrID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
               "accb.accb_chart_of_accnts", "accnt_id", "crncy_id", int.Parse(dtst.Tables[0].Rows[a][0].ToString())));

                    double accntCurrRate = Math.Round(
                      Global.get_LtstExchRate(int.Parse(dtst.Tables[0].Rows[a][5].ToString()), accntCurrID,
               dtst.Tables[0].Rows[a][1].ToString()), 15);

                    Global.createPymntGLLine(int.Parse(dtst.Tables[0].Rows[a][0].ToString()),
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
                if (Global.get_Batch_CrdtSum(todbatchid) == Global.get_Batch_DbtSum(todbatchid))
                {
                    Global.updtPymntMnlGLIntrfcLn(py_trns_id, todbatchid);
                    //Global.updtGLIntrfcLnSpclOrg(Global.mnFrm.cmCde.Org_id);
                    Global.updtTodaysGLBatchPstngAvlblty(todbatchid, "1");
                    return true;
                }
                else
                {
                    Global.mnFrm.cmCde.showMsg("The GL Batch created is not Balanced!\r\nTransactions created will be reversed and deleted!", 0);
                    Global.deleteBatchTrns(todbatchid);
                    Global.deleteBatch(todbatchid, todaysGlBatch);
                    return false;
                }
                //return true;
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg("Error Sending Payment to GL!\r\n" + ex.Message, 0);
                return false;
            }
        }

        private bool sendMsPyToGL(long mspyid)
        {
            try
            {
                //Get Todays GL Batch Name
                string dateStr = DateTime.ParseExact(
            Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
            System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
                string todaysGlBatch = "Internal Payments (" + dateStr + ")";
                long todbatchid = Global.getTodaysGLBatchID(
                  todaysGlBatch,
                  Global.mnFrm.cmCde.Org_id);
                if (todbatchid <= 0)
                {
                    Global.createTodaysGLBatch(Global.mnFrm.cmCde.Org_id,
                      todaysGlBatch, todaysGlBatch, "Internal Payments");
                    todbatchid = Global.getTodaysGLBatchID(
                    todaysGlBatch,
                    Global.mnFrm.cmCde.Org_id);
                }
                if (todbatchid > 0)
                {
                    todaysGlBatch = Global.get_GLBatch_Nm(todbatchid);
                }

                /*
                 * 1. Get list of all accounts to transfer from the 
                 * interface table and their total amounts.
                 * 2. Loop through each and transfer
                 */
                DataSet dtst = Global.getAllInMsPyGLIntrfc(mspyid);
                long cntr = dtst.Tables[0].Rows.Count;

                if (cntr > 0)
                {
                    if (Global.isMsPyGLIntrfcBlcd(mspyid) == false)
                    {
                        Global.mnFrm.cmCde.showMsg("Cannot Transfer Transactions to GL because\r\n" +
                          " this Mass Pay Run's Transactions in the \r\n GL Interface are not Balanced!", 0);
                        return false;
                    }
                }
                else
                {
                    //Global.mnFrm.cmCde.showMsg("There is nothing in the GL Interface Table to Transfer!", 0);
                    //return false;
                }

                for (int a = 0; a < cntr; a++)
                {
                    string src_ids = Global.getGLIntrfcIDsMsPy(int.Parse(dtst.Tables[0].Rows[a][0].ToString()),
               dtst.Tables[0].Rows[a][1].ToString(), int.Parse(dtst.Tables[0].Rows[a][5].ToString()), mspyid);

                    double entrdAmnt = double.Parse(dtst.Tables[0].Rows[a][2].ToString()) == 0 ? double.Parse(dtst.Tables[0].Rows[a][3].ToString()) : double.Parse(dtst.Tables[0].Rows[a][2].ToString());
                    string dbtCrdt = double.Parse(dtst.Tables[0].Rows[a][3].ToString()) == 0 ? "D" : "C";
                    int accntCurrID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
               "accb.accb_chart_of_accnts", "accnt_id", "crncy_id", int.Parse(dtst.Tables[0].Rows[a][0].ToString())));

                    double accntCurrRate = Math.Round(
                      Global.get_LtstExchRate(int.Parse(dtst.Tables[0].Rows[a][5].ToString()), accntCurrID,
               dtst.Tables[0].Rows[a][1].ToString()), 15);

                    Global.createPymntGLLine(int.Parse(dtst.Tables[0].Rows[a][0].ToString()),
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
                if (Global.get_Batch_CrdtSum(todbatchid) == Global.get_Batch_DbtSum(todbatchid))
                {
                    Global.updtPymntMsPyGLIntrfcLn(mspyid, todbatchid);
                    //Global.updtGLIntrfcLnSpclOrg(Global.mnFrm.cmCde.Org_id);
                    Global.updtTodaysGLBatchPstngAvlblty(todbatchid, "1");
                    return true;
                }
                else
                {
                    Global.mnFrm.cmCde.showMsg("The GL Batch created is not Balanced!\r\nTransactions created will be reversed and deleted!", 0);
                    Global.deleteBatchTrns(todbatchid);
                    Global.deleteBatch(todbatchid, todaysGlBatch);
                    return false;
                }
                //return true;
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg("Error Sending Payment to GL!\r\n" + ex.Message, 0);
                return false;
            }
        }

        private bool isPayTrnsValid()
        {
            if (this.itmListViewPymnt.SelectedItems[0].SubItems[6].Text != "Number"
              && this.itmListViewPymnt.SelectedItems[0].SubItems[3].Text != "Balance Item"
              && this.itmListViewPymnt.SelectedItems[0].SubItems[5].Text != "Purely Informational")
            {
                string[] accntinf = new string[4];
                double netamnt = 0;
                accntinf = Global.get_ItmAccntInfo(long.Parse(
            this.itmListViewPymnt.SelectedItems[0].SubItems[4].Text));

                netamnt = Global.mnFrm.cmCde.dbtOrCrdtAccntMultiplier(int.Parse(accntinf[1]),
                  accntinf[0].Substring(0, 1)) * (double)this.amntNumericUpDown.Value;

                if (!Global.mnFrm.cmCde.isTransPrmttd(
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
                accntinf = Global.get_ItmAccntInfo(itmid);
                if (int.Parse(accntinf[1]) > 0 && int.Parse(accntinf[3]) > 0)
                {
                    netamnt = Global.mnFrm.cmCde.dbtOrCrdtAccntMultiplier(int.Parse(accntinf[1]),
                      accntinf[0].Substring(0, 1)) * pyamnt;

                    if (!Global.mnFrm.cmCde.isTransPrmttd(
                int.Parse(accntinf[1]), trnsdte, netamnt))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void paymntDateButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.selectDate(ref this.paymntDateTextBox);
            if (this.glDateTextBox.Text == "")
            {
                this.glDateTextBox.Text = this.paymntDateTextBox.Text;
            }
        }

        private void processPayButton_Click(object sender, EventArgs e)
        {

        }
        #endregion
        #region "Pay Item Sets"
        private void loadItmStPanel()
        {
            this.obey_itmst_evnts = false;
            if (this.searchInItmStComboBox.SelectedIndex < 0)
            {
                this.searchInItmStComboBox.SelectedIndex = 0;
            }
            if (this.searchForItmStTextBox.Text.Contains("%") == false)
            {
                this.searchForItmStTextBox.Text = "%" + this.searchForItmStTextBox.Text.Replace(" ", "%") + "%";
            }
            if (this.searchForItmStTextBox.Text == "%%")
            {
                this.searchForItmStTextBox.Text = "%";
            }
            int dsply = 0;
            if (this.dsplySizeItmStComboBox.Text == ""
              || int.TryParse(this.dsplySizeItmStComboBox.Text, out dsply) == false)
            {
                this.dsplySizeItmStComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            //this.groupBox8.Height = this.g.Bottom - this.toolStrip4.Bottom - 50;
            this.is_last_itmst = false;
            this.totl_itmst = Global.mnFrm.cmCde.Big_Val;
            this.getItmStPnlData();
            this.obey_itmst_evnts = true;
        }

        private void getItmStPnlData()
        {
            this.updtItmStTotals();
            this.populateItmStListVw();
            this.updtItmStNavLabels();
        }

        private void updtItmStTotals()
        {
            Global.mnFrm.cmCde.navFuncts.FindNavigationIndices(
              long.Parse(this.dsplySizeItmStComboBox.Text), this.totl_itmst);
            if (this.itmst_cur_indx >= Global.mnFrm.cmCde.navFuncts.totalGroups)
            {
                this.itmst_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            if (this.itmst_cur_indx < 0)
            {
                this.itmst_cur_indx = 0;
            }
            Global.mnFrm.cmCde.navFuncts.currentNavigationIndex = this.itmst_cur_indx;
        }

        private void updtItmStNavLabels()
        {
            this.moveFirstItmStButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveFirstBtnStatus();
            this.movePreviousItmStButton.Enabled = Global.mnFrm.cmCde.navFuncts.movePrevBtnStatus();
            this.moveNextItmStButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveNextBtnStatus();
            this.moveLastItmStButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveLastBtnStatus();
            this.positionItmStTextBox.Text = Global.mnFrm.cmCde.navFuncts.displayedRecordsNumbers();
            if (this.is_last_itmst == true ||
              this.totl_itmst != Global.mnFrm.cmCde.Big_Val)
            {
                this.totalRecsItmStLabel.Text = Global.mnFrm.cmCde.navFuncts.totalRecordsLabel();
            }
            else
            {
                this.totalRecsItmStLabel.Text = "of Total";
            }
        }

        private void populateItmStListVw()
        {
            this.obey_itmst_evnts = false;
            DataSet dtst = Global.get_Basic_ItmSt(this.searchForItmStTextBox.Text,
              this.searchInItmStComboBox.Text, this.itmst_cur_indx,
              int.Parse(this.dsplySizeItmStComboBox.Text), Global.mnFrm.cmCde.Org_id);
            this.itmSetListView.Items.Clear();
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.last_itmst_num = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
                ListViewItem nwItem = new ListViewItem(new string[] {
    (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),
    dtst.Tables[0].Rows[i][2].ToString(),
    dtst.Tables[0].Rows[i][3].ToString(),
    dtst.Tables[0].Rows[i][0].ToString(),
    dtst.Tables[0].Rows[i][4].ToString(),
    dtst.Tables[0].Rows[i][5].ToString(),
    dtst.Tables[0].Rows[i][6].ToString()});
                this.itmSetListView.Items.Add(nwItem);
            }
            this.correctItmStNavLbls(dtst);
            if (this.itmSetListView.Items.Count > 0)
            {
                this.obey_itmst_evnts = true;
                this.itmSetListView.Items[0].Selected = true;
            }
            else
            {
                this.clearItmStDetInfo();
                this.loadItmStDetPanel();
            }
            this.obey_itmst_evnts = true;
        }

        private void correctItmStNavLbls(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.itmst_cur_indx == 0 && totlRecs == 0)
            {
                this.is_last_itmst = true;
                this.totl_itmst = 0;
                this.last_itmst_num = 0;
                this.itmst_cur_indx = 0;
                this.updtItmStTotals();
                this.updtItmStNavLabels();
            }
            else if (this.totl_itmst == Global.mnFrm.cmCde.Big_Val
           && totlRecs < long.Parse(this.dsplySizeItmStComboBox.Text))
            {
                this.totl_itmst = this.last_itmst_num;
                if (totlRecs == 0)
                {
                    this.itmst_cur_indx -= 1;
                    this.updtItmStTotals();
                    this.populateItmStListVw();
                }
                else
                {
                    this.updtItmStTotals();
                }
            }
        }

        private bool shdObeyItmStEvts()
        {
            return this.obey_itmst_evnts;
        }

        private void ItmStPnlNavButtons(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecsItmStLabel.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.is_last_itmst = false;
                this.itmst_cur_indx = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.is_last_itmst = false;
                this.itmst_cur_indx -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.is_last_itmst = false;
                this.itmst_cur_indx += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.is_last_itmst = true;
                this.totl_itmst = Global.get_Total_ItmSt(this.searchForItmStTextBox.Text,
                  this.searchInItmStComboBox.Text, Global.mnFrm.cmCde.Org_id);
                this.updtItmStTotals();
                this.itmst_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            this.getItmStPnlData();
        }

        private void clearItmStDetInfo()
        {
            this.obey_itmst_evnts = false;
            this.saveItmStButton.Enabled = false;
            this.addItmStButton.Enabled = this.addItmSts;
            this.editItmStButton.Enabled = this.editItmSts;
            this.deleteItmStButton.Enabled = this.delItmSts;
            this.isEnabledItmSetCheckBox.Checked = false;
            this.isDfltItmStCheckBox.Checked = false;
            this.usesSQLItmStCheckBox.Checked = false;
            this.itmStSQLTextBox.Text = "";
            this.itmStSQLTextBox.Enabled = false;
            this.itmStSQLButton.Enabled = false;
            this.addItmStDtButton.Enabled = false;
            this.delItmStDtButton.Enabled = false;
            this.itmSetIDTextBox.Text = "-1";
            this.itmSetNmTextBox.Text = "";
            this.itmSetDescTextBox.Text = "";
            this.itmSetDetListView.Items.Clear();
            this.obey_itmst_evnts = true;
        }

        private void prpareForItmStDetEdit()
        {
            this.saveItmStButton.Enabled = true;
            this.itmSetNmTextBox.ReadOnly = false;
            this.itmSetNmTextBox.BackColor = Color.FromArgb(255, 255, 128);
            this.itmSetDescTextBox.ReadOnly = false;
            this.itmSetDescTextBox.BackColor = Color.White;
            if (this.usesSQLItmStCheckBox.Checked == false)
            {
                this.itmStSQLTextBox.ReadOnly = true;
                this.itmStSQLTextBox.BackColor = Color.WhiteSmoke;
            }
            else
            {
                this.itmStSQLTextBox.ReadOnly = false;
                this.itmStSQLTextBox.BackColor = Color.FromArgb(255, 255, 128);
            }
        }

        private void disableItmStDetEdit()
        {
            this.addItmSt = false;
            this.editItmSt = false;
            this.itmSetNmTextBox.ReadOnly = true;
            this.itmSetNmTextBox.BackColor = Color.WhiteSmoke;
            this.itmSetDescTextBox.ReadOnly = true;
            this.itmSetDescTextBox.BackColor = Color.WhiteSmoke;
            this.itmStSQLTextBox.ReadOnly = true;
            this.itmStSQLTextBox.BackColor = Color.WhiteSmoke;
            this.itmStSQLTextBox.ReadOnly = true;
            this.itmStSQLTextBox.BackColor = Color.WhiteSmoke;
        }

        private void itmSetListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.shdObeyItmStEvts() == false || this.itmSetListView.SelectedItems.Count > 1)
            {
                return;
            }
            if (this.itmSetListView.SelectedItems.Count > 0)
            {
                this.populateItmStDet();
            }
            else
            {
                this.clearItmStDetInfo();
                this.disableItmStDetEdit();
                this.loadItmStDetPanel();
            }
        }

        private void populateItmStDet()
        {
            this.clearItmStDetInfo();
            this.disableItmStDetEdit();
            this.obey_itmst_evnts = false;
            this.itmSetDetListView.Items.Clear();
            this.itmSetIDTextBox.Text = this.itmSetListView.SelectedItems[0].SubItems[4].Text;
            this.itmSetNmTextBox.Text = this.itmSetListView.SelectedItems[0].SubItems[1].Text;
            this.itmSetDescTextBox.Text = this.itmSetListView.SelectedItems[0].SubItems[2].Text;
            this.isEnabledItmSetCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(
              this.itmSetListView.SelectedItems[0].SubItems[3].Text);
            this.isDfltItmStCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(
              this.itmSetListView.SelectedItems[0].SubItems[5].Text);
            this.usesSQLItmStCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(
               this.itmSetListView.SelectedItems[0].SubItems[6].Text);
            this.itmStSQLTextBox.Text = this.itmSetListView.SelectedItems[0].SubItems[7].Text;
            if (this.usesSQLItmStCheckBox.Checked == false)
            {
                this.itmStSQLTextBox.Text = "";
                this.itmStSQLTextBox.Enabled = false;
                this.itmStSQLButton.Enabled = false;
                this.addItmStDtButton.Enabled = true;
                this.delItmStDtButton.Enabled = true;
            }
            else
            {
                this.itmStSQLTextBox.Enabled = true;
                this.itmStSQLButton.Enabled = true;
                this.addItmStDtButton.Enabled = false;
                this.delItmStDtButton.Enabled = false;
            }
            this.populateRolesLstVw();
            this.loadItmStDetPanel();
            this.obey_itmst_evnts = true;
        }

        private void loadItmStDetPanel()
        {
            this.obey_idet_evnts = false;
            int dsply = 0;
            if (this.dsplySizeItmsDetComboBox.Text == ""
             || int.TryParse(this.dsplySizeItmsDetComboBox.Text, out dsply) == false)
            {
                this.dsplySizeItmsDetComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            //this.groupBox8.Height = this.g.Bottom - this.toolStrip4.Bottom - 50;
            this.idet_cur_indx = 0;
            this.is_last_idet = false;
            this.last_idet_num = 0;
            this.totl_idet = Global.mnFrm.cmCde.Big_Val;
            this.getIdetPnlData();
            this.obey_idet_evnts = true;
        }

        private void getIdetPnlData()
        {
            this.updtIdetTotals();
            this.populateIdetListVw();
            this.updtIdetNavLabels();
        }

        private void updtIdetTotals()
        {
            int dsply = 0;
            if (this.dsplySizeItmsDetComboBox.Text == ""
              || int.TryParse(this.dsplySizeItmsDetComboBox.Text, out dsply) == false)
            {
                this.dsplySizeItmsDetComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            this.myNav.FindNavigationIndices(
          long.Parse(this.dsplySizeItmsDetComboBox.Text), this.totl_idet);
            if (this.idet_cur_indx >= this.myNav.totalGroups)
            {
                this.idet_cur_indx = this.myNav.totalGroups - 1;
            }
            if (this.idet_cur_indx < 0)
            {
                this.idet_cur_indx = 0;
            }
            this.myNav.currentNavigationIndex = this.idet_cur_indx;
        }

        private void updtIdetNavLabels()
        {
            this.moveFirstItmsDetButton.Enabled = this.myNav.moveFirstBtnStatus();
            this.movePreviousItmsDetButton.Enabled = this.myNav.movePrevBtnStatus();
            this.moveNextItmsDetButton.Enabled = this.myNav.moveNextBtnStatus();
            this.moveLastItmsDetButton.Enabled = this.myNav.moveLastBtnStatus();
            this.positionItmsDetTextBox.Text = this.myNav.displayedRecordsNumbers();
            if (this.is_last_idet == true ||
             this.totl_idet != Global.mnFrm.cmCde.Big_Val)
            {
                this.totalRecsItmsDetLabel.Text = this.myNav.totalRecordsLabel();
            }
            else
            {
                this.totalRecsItmsDetLabel.Text = "of Total";
            }
        }

        private void populateIdetListVw()
        {
            this.obey_idet_evnts = false;

            DataSet dtst = Global.get_One_ItmStDet(int.Parse(this.itmSetIDTextBox.Text),
              this.idet_cur_indx,
              int.Parse(this.dsplySizeItmsDetComboBox.Text));
            this.itmSetDetListView.Items.Clear();
            int cols = dtst.Tables[0].Columns.Count;

            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                long dtID = -1;
                if (cols == 5)
                {
                    long.TryParse(dtst.Tables[0].Rows[i][4].ToString(), out dtID);
                }
                this.last_idet_num = this.myNav.startIndex() + i;
                ListViewItem nwItem = new ListViewItem(new string[] {
        (this.myNav.startIndex() + i).ToString(),
        dtst.Tables[0].Rows[i][1].ToString(),
          dtst.Tables[0].Rows[i][2].ToString(),
        dtst.Tables[0].Rows[i][3].ToString(),
        dtst.Tables[0].Rows[i][0].ToString(),
        dtID.ToString()});
                this.itmSetDetListView.Items.Add(nwItem);
            }
            this.correctIdetNavLbls(dtst);
            this.obey_idet_evnts = true;
        }

        private void correctIdetNavLbls(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.totl_idet == Global.mnFrm.cmCde.Big_Val
          && totlRecs < long.Parse(this.dsplySizeItmsDetComboBox.Text))
            {
                this.totl_idet = this.last_idet_num;
                if (totlRecs == 0)
                {
                    this.idet_cur_indx -= 1;
                    this.updtIdetTotals();
                    this.populateIdetListVw();
                }
                else
                {
                    this.updtIdetTotals();
                }
            }
        }

        private bool shdObeyIdetEvts()
        {
            return this.obey_idet_evnts;
        }

        private void IdetPnlNavButtons(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecsItmsDetLabel.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.is_last_idet = false;
                this.idet_cur_indx = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.is_last_idet = false;
                this.idet_cur_indx -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.is_last_idet = false;
                this.idet_cur_indx += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.is_last_idet = true;
                this.totl_idet = Global.get_Total_ItmsDet(long.Parse(this.itmSetIDTextBox.Text));
                this.updtIdetTotals();
                this.idet_cur_indx = this.myNav.totalGroups - 1;
            }
            this.getIdetPnlData();
        }

        private void goItmStButton_Click(object sender, EventArgs e)
        {
            this.loadItmStPanel();
        }

        private void addItmStButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[11]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                  " this action!\nContact your System Administrator!", 0);
                return;
            }
            this.clearItmStDetInfo();
            this.addItmSt = true;
            this.editItmSt = false;
            this.prpareForItmStDetEdit();
            this.addItmStButton.Enabled = false;
            this.editItmStButton.Enabled = false;
            this.deleteItmStButton.Enabled = false;
            this.itmSetDetListView.Items.Clear();
        }

        private void editItmStButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[12]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                  " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.itmSetIDTextBox.Text == "" || this.itmSetIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("No record to Edit!", 0);
                return;
            }
            this.addItmSt = false;
            this.editItmSt = true;

            this.prpareForItmStDetEdit();
            this.addItmStButton.Enabled = false;
            this.editItmStButton.Enabled = false;
            this.deleteItmStButton.Enabled = false;
        }

        private void saveItmStButton_Click(object sender, EventArgs e)
        {
            if (this.addItmSt == true)
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[11]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                     " this action!\nContact your System Administrator!", 0);
                    return;
                }
            }
            else
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[12]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                     " this action!\nContact your System Administrator!", 0);
                    return;
                }
            }
            if (this.itmSetNmTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please enter an Item Set Name!", 0);
                return;
            }
            long oldItmStID = Global.mnFrm.cmCde.getItmStID(this.itmSetNmTextBox.Text, Global.mnFrm.cmCde.Org_id);
            if (oldItmStID > 0
             && this.addItmSt == true)
            {
                Global.mnFrm.cmCde.showMsg("Item Set's Name is already in use in this Organisation!", 0);
                return;
            }
            if (oldItmStID > 0
             && this.editItmSt == true
             && oldItmStID.ToString() != this.itmSetIDTextBox.Text)
            {
                Global.mnFrm.cmCde.showMsg("New Item Set's Name is already in use in this Organisation!", 0);
                return;
            }
            if (this.usesSQLItmStCheckBox.Checked == false)
            {
                if (this.itmStSQLTextBox.Text != "")
                {
                    Global.mnFrm.cmCde.showMsg("SQL Query must be Empty!", 0);
                    return;
                }
            }
            else
            {
                if (this.itmStSQLTextBox.Text == "")
                {
                    Global.mnFrm.cmCde.showMsg("SQL Query cannot be Empty!", 0);
                    return;
                }
                try
                {
                    string testSQL = "SELECT a.item_id, a.item_code_name, a.item_value_uom, " +
                  "(CASE WHEN a.item_min_type='Earnings' or a.item_min_type='Employer Charges' " +
                  "THEN 'Payment by Organisation' WHEN a.item_min_type='Bills/Charges' or " +
                  "a.item_min_type='Deductions' THEN 'Payment by Person' ELSE 'Purely Informational' END) trns_typ";
                    if (!(this.itmStSQLTextBox.Text.Replace("\r\n", "").Replace("\r", "").ToLower().Replace(" ", "").Contains(testSQL.ToLower().Replace(" ", ""))))
                    {
                        Global.mnFrm.cmCde.showMsg("Item Set SQL Query must start with\r\n " + testSQL, 0);
                        return;
                    }
                    Global.mnFrm.cmCde.selectDataNoParams(this.itmStSQLTextBox.Text);
                }
                catch (Exception ex)
                {
                    Global.mnFrm.cmCde.showMsg(ex.Message + "\r\nPlease enter Valid Item Set SQL Query!", 0);
                    return;
                }
            }
            //if (this.isDfltItmStCheckBox.Checked == true)
            //{
            //  Global.undfltAllItmSt(Global.mnFrm.cmCde.Org_id);
            //}
            if (this.addItmSt == true)
            {
                Global.createItmSt(Global.mnFrm.cmCde.Org_id,
                 this.itmSetNmTextBox.Text, this.itmSetDescTextBox.Text,
                 this.isEnabledItmSetCheckBox.Checked,
                 this.isDfltItmStCheckBox.Checked,
                 this.usesSQLItmStCheckBox.Checked, this.itmStSQLTextBox.Text);
                this.saveItmStButton.Enabled = false;
                this.addItmSt = false;
                this.editItmSt = false;
                this.editItmStButton.Enabled = this.editItmSts;
                this.addItmStButton.Enabled = this.addItmSts;
                this.deleteItmStButton.Enabled = this.delItmSts;
                System.Windows.Forms.Application.DoEvents();
                this.loadItmStPanel();
            }
            else if (this.editItmSt == true)
            {
                Global.updateItmSt(int.Parse(this.itmSetIDTextBox.Text),
                 this.itmSetNmTextBox.Text, this.itmSetDescTextBox.Text,
                 this.isEnabledItmSetCheckBox.Checked,
                 this.isDfltItmStCheckBox.Checked,
                 this.usesSQLItmStCheckBox.Checked, this.itmStSQLTextBox.Text);
                this.saveItmStButton.Enabled = false;
                this.editItmSt = false;
                this.editItmStButton.Enabled = this.editItmSts;
                this.addItmStButton.Enabled = this.addItmSts;
                this.loadItmStPanel();
            }
        }

        private void addItmSt1MenuItem_Click(object sender, EventArgs e)
        {
            this.addItmStDtButton_Click(this.addItmStDtButton, e);
        }

        private void rmvPayItmMenuItem_Click(object sender, EventArgs e)
        {
            this.delItmStDtButton_Click(this.delItmStDtButton, e);
        }

        private void exptItmStDtMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.itmSetDetListView);
        }

        private void refreshItmStDtMenuItem_Click(object sender, EventArgs e)
        {
            this.loadItmStDetPanel();
        }

        private void deleteItmStButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[13]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.itmSetIDTextBox.Text == "" || this.itmSetIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select the Item Set to DELETE!", 0);
                return;
            }
            if (Global.isItmStInUse(int.Parse(this.itmSetIDTextBox.Text)) == true)
            {
                Global.mnFrm.cmCde.showMsg("This Item Set has been assigned to a Mass Pay hence cannot be DELETED!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Item Set?" +
         "\r\nThis action cannot be undone!", 1) == DialogResult.No)
            {
                Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            Global.mnFrm.cmCde.deleteGnrlRecs(int.Parse(this.itmSetIDTextBox.Text),
           "Item Set Name = " + this.itmSetNmTextBox.Text, "pay.pay_itm_sets_det", "hdr_id");

            Global.mnFrm.cmCde.deleteGnrlRecs(int.Parse(this.itmSetIDTextBox.Text),
              "Item Set Name = " + this.itmSetNmTextBox.Text, "pay.pay_itm_sets_hdr", "hdr_id");
            this.loadItmStPanel();
        }

        private void vwSQLItmStButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(this.itmst_SQL, 8);
        }

        private void recHstryItmStButton_Click(object sender, EventArgs e)
        {
            if (this.itmSetListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(
              Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(
              this.itmSetListView.SelectedItems[0].SubItems[4].Text),
              "pay.pay_itm_sets_hdr", "hdr_id"), 7);

        }

        private void editItmStMenuItem_Click(object sender, EventArgs e)
        {
            this.editItmStButton_Click(this.editItmStButton, e);
        }

        private void addItmStMenuItem_Click(object sender, EventArgs e)
        {
            this.addItmStButton_Click(this.addItmStButton, e);
        }

        private void delItmStMenuItem_Click(object sender, EventArgs e)
        {
            this.deleteItmStButton_Click(this.deleteItmStButton, e);
        }

        private void exptItmStMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.itmSetListView);
        }

        private void rfrshItmStMenuItem_Click(object sender, EventArgs e)
        {
            this.goItmStButton_Click(this.goItmStButton, e);
        }

        private void vwSQLItmStMenuItem_Click(object sender, EventArgs e)
        {
            this.vwSQLItmStButton_Click(this.vwSQLItmStButton, e);
        }

        private void rcHstryItmStMenuItem_Click(object sender, EventArgs e)
        {
            this.recHstryItmStButton_Click(this.recHstryItmStButton, e);
        }

        private void vwSQLItmStDtMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(this.idet_SQL, 8);
        }

        private void recHstryItmStDtMenuItem_Click(object sender, EventArgs e)
        {
            if (this.itmSetDetListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(
              Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(
              this.itmSetDetListView.SelectedItems[0].SubItems[5].Text),
              "pay.pay_itm_sets_det", "det_id"), 7);
        }

        private void isEnabledItmSetCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shdObeyItmStEvts() == false
            || beenToCheckBx == true)
            {
                beenToCheckBx = false;
                return;
            }
            beenToCheckBx = true;
            if (this.addItmSt == false && this.editItmSt == false)
            {
                this.isEnabledItmSetCheckBox.Checked = !this.isEnabledItmSetCheckBox.Checked;
            }
        }

        private void searchForItmStTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.goItmStButton_Click(this.goItmStButton, ex);
            }
        }

        private void positionItmsDetTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                this.IdetPnlNavButtons(this.movePreviousItmsDetButton, ex);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
            {
                this.IdetPnlNavButtons(this.moveNextItmsDetButton, ex);
            }
        }

        private void positionItmStTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                this.ItmStPnlNavButtons(this.movePreviousItmStButton, ex);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
            {
                this.ItmStPnlNavButtons(this.moveNextItmStButton, ex);
            }
        }

        private void itmStMnlButton_Click(object sender, EventArgs e)
        {
            string[] selVals = new string[1];
            selVals[0] = this.itmStIDMnlTextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Item Sets for Payments(Enabled)"), ref selVals,
                true, true, Global.mnFrm.cmCde.Org_id, "", "",
             this.srchWrd, "Both", true, " and (tbl1.g IN (" + Global.concatCurRoleIDs() + "))");
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.itmStIDMnlTextBox.Text = selVals[i];
                    this.itmStNmMnlTextBox.Text = Global.mnFrm.cmCde.getItmStName(int.Parse(selVals[i]));
                }
                this.goItmButton_Click1(this.goItmButtonNw, e);
            }
        }

        private void isDfltItmStCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shdObeyItmStEvts() == false
            || beenToCheckBx == true)
            {
                beenToCheckBx = false;
                return;
            }
            beenToCheckBx = true;
            if (this.addItmSt == false && this.editItmSt == false)
            {
                this.isDfltItmStCheckBox.Checked = !this.isDfltItmStCheckBox.Checked;
            }
        }

        private void itmSetListView_ItemSelectionChanged(object sender, System.Windows.Forms.ListViewItemSelectionChangedEventArgs e)
        {
            if (this.shdObeyItmStEvts() == false)
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

        private void addItmStDtButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[12]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.itmSetIDTextBox.Text == ""
         || this.itmSetIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please Select a Saved Item Set First!", 0);
                return;
            }

            string[] selValuesIDs = new string[this.itmSetDetListView.Items.Count];
            for (int j = 0; j < this.itmSetDetListView.Items.Count; j++)
            {
                selValuesIDs[j] = this.itmSetDetListView.Items[j].SubItems[4].Text;
            }
            //Non-Balance Items
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
             Global.mnFrm.cmCde.getLovID("Pay Items"), ref selValuesIDs, false, true, Global.mnFrm.cmCde.Org_id);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selValuesIDs.Length; i++)
                {
                    if (Global.doesItmStHvItm(long.Parse(this.itmSetIDTextBox.Text),
                      int.Parse(selValuesIDs[i])) == false)
                    {
                        string trnsTyp = "";
                        string itmTyp = Global.mnFrm.cmCde.getItmMinType(int.Parse(selValuesIDs[i]));

                        if (itmTyp == "Earnings"
                  || itmTyp == "Employer Charges")
                        {
                            trnsTyp = "Payment by Organisation";
                        }
                        else if (itmTyp == "Bills/Charges"
                  || itmTyp == "Deductions")
                        {
                            trnsTyp = "Payment by Person";
                        }
                        else
                        {
                            trnsTyp = "Purely Informational";
                        }

                        Global.createItmStDet(int.Parse(this.itmSetIDTextBox.Text),
                         int.Parse(selValuesIDs[i]), trnsTyp);
                    }
                }
            }
            this.loadItmStDetPanel();
        }

        private void delItmStDtButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[12]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.itmSetDetListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the Item(s) to Delete", 0);
                return;
            }
            int cnt = this.itmSetDetListView.SelectedItems.Count;
            for (int i = 0; i < cnt; i++)
            {
                Global.deleteItmStDet(int.Parse(
                  this.itmSetDetListView.SelectedItems[i].SubItems[5].Text));
            }
            this.loadItmStDetPanel();
        }

        private void usesSQLItmStCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shdObeyItmStEvts() == false
            || (beenToCheckBx == true && this.addItmSt == false && this.editItmSt == false))
            {
                beenToCheckBx = false;
                return;
            }
            beenToCheckBx = true;
            if (this.addItmSt == false && this.editItmSt == false)
            {
                this.usesSQLItmStCheckBox.Checked = !this.usesSQLItmStCheckBox.Checked;
            }
            else
            {
                if (this.usesSQLItmStCheckBox.Checked == false)
                {
                    this.itmStSQLTextBox.Text = "";
                    this.itmStSQLTextBox.Enabled = false;
                    this.itmStSQLTextBox.ReadOnly = true;
                    this.itmStSQLTextBox.BackColor = Color.WhiteSmoke;
                    this.itmStSQLButton.Enabled = false;
                    this.addItmStDtButton.Enabled = true;
                    this.delItmStDtButton.Enabled = true;
                }
                else
                {
                    this.itmStSQLTextBox.Enabled = true;
                    this.itmStSQLTextBox.ReadOnly = false;
                    this.itmStSQLTextBox.BackColor = Color.FromArgb(255, 255, 128);
                    this.itmStSQLButton.Enabled = true;
                    this.addItmStDtButton.Enabled = false;
                    this.delItmStDtButton.Enabled = false;
                }
            }
        }

        private void itmStSQLButton_Click(object sender, EventArgs e)
        {
            try
            {
                string testSQL = "SELECT a.item_id, a.item_code_name, a.item_value_uom, " +
                "(CASE WHEN a.item_min_type='Earnings' or a.item_min_type='Employer Charges' " +
                "THEN 'Payment by Organisation' WHEN a.item_min_type='Bills/Charges' or " +
                "a.item_min_type='Deductions' THEN 'Payment by Person' ELSE 'Purely Informational' END) trns_typ";
                if (!(this.itmStSQLTextBox.Text.Replace("\r\n", "").Replace("\r", "").ToLower().Replace(" ", "").Contains(testSQL.ToLower().Replace(" ", ""))))
                {
                    Global.mnFrm.cmCde.showMsg("Item Set SQL Query must start with\r\n" + testSQL, 0);
                    return;
                }
                Global.mnFrm.cmCde.selectDataNoParams(this.itmStSQLTextBox.Text);
                Global.mnFrm.cmCde.showMsg("Item Set SQL Query is Valid!", 3);
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message + "\r\nInvalid Item Set SQL Query!", 0);
                return;
            }
        }

        private void itmSetListView_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.Control && e.KeyCode == Keys.S)
            {
                if (this.saveItmStButton.Enabled == true)
                {
                    this.saveItmStButton_Click(this.saveItmStButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                if (this.addItmStButton.Enabled == true)
                {
                    this.addItmStButton_Click(this.addItmStButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                if (this.editItmStButton.Enabled == true)
                {
                    this.editItmStButton_Click(this.editItmStButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                this.resetItmStButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)
            {
                if (this.goItmStButton.Enabled == true)
                {
                    this.goItmStButton_Click(this.goItmStButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.Delete)
            {
                if (this.deleteItmStButton.Enabled == true)
                {
                    this.deleteItmStButton_Click(this.deleteItmStButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                Global.mnFrm.cmCde.listViewKeyDown(this.itmSetListView, e);
            }
        }

        private void itmStTxtBx_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.Control && e.KeyCode == Keys.S)
            {
                if (this.saveItmStButton.Enabled == true)
                {
                    this.saveItmStButton_Click(this.saveItmStButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                if (this.addItmStButton.Enabled == true)
                {
                    this.addItmStButton_Click(this.addItmStButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                if (this.editItmStButton.Enabled == true)
                {
                    this.editItmStButton_Click(this.editItmStButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                this.resetItmStButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)
            {
                if (this.goItmStButton.Enabled == true)
                {
                    this.goItmStButton_Click(this.goItmStButton, ex);
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

        private void itmSetDetListView_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.Control && e.KeyCode == Keys.S)
            {
                if (this.saveItmStButton.Enabled == true)
                {
                    this.saveItmStButton_Click(this.saveItmStButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                if (this.addItmStDtButton.Enabled == true)
                {
                    this.addItmStDtButton_Click(this.addItmStDtButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                if (this.editItmStButton.Enabled == true)
                {
                    this.editItmStButton_Click(this.editItmStButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if ((e.Control && e.KeyCode == Keys.R) || e.KeyCode == Keys.F5)
            {
                //if (this.goItmStButton.Enabled == true)
                //{
                //  this.goItmStButton_Click(this.goItmStButton, ex);
                //}
                this.loadItmStDetPanel();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.Delete)
            {
                if (this.delItmStDtButton.Enabled == true)
                {
                    this.delItmStDtButton_Click(this.delItmStDtButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                Global.mnFrm.cmCde.listViewKeyDown(this.itmSetDetListView, e);
            }
        }
        #endregion
        #region "Person Sets..."
        private void loadPrsStPanel()
        {
            this.obey_prsst_evnts = false;
            if (this.searchInPrsStComboBox.SelectedIndex < 0)
            {
                this.searchInPrsStComboBox.SelectedIndex = 0;
            }
            if (this.searchForPrsStTextBox.Text.Contains("%") == false)
            {
                this.searchForPrsStTextBox.Text = "%" + this.searchForPrsStTextBox.Text.Replace(" ", "%") + "%";
            }
            if (this.searchForPrsStTextBox.Text == "%%")
            {
                this.searchForPrsStTextBox.Text = "%";
            }
            int dsply = 0;
            if (this.dsplySizePrsStComboBox.Text == ""
              || int.TryParse(this.dsplySizePrsStComboBox.Text, out dsply) == false)
            {
                this.dsplySizePrsStComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            //this.groupBox8.Height = this.g.Bottom - this.toolStrip4.Bottom - 50;
            this.is_last_prsst = false;
            this.totl_prsst = Global.mnFrm.cmCde.Big_Val;
            this.getPrsStPnlData();
            this.obey_prsst_evnts = true;
        }

        private void getPrsStPnlData()
        {
            this.updtPrsStTotals();
            this.populatePrsStListVw();
            this.updtPrsStNavLabels();
        }

        private void updtPrsStTotals()
        {
            Global.mnFrm.cmCde.navFuncts.FindNavigationIndices(
              long.Parse(this.dsplySizePrsStComboBox.Text), this.totl_prsst);
            if (this.prsst_cur_indx >= Global.mnFrm.cmCde.navFuncts.totalGroups)
            {
                this.prsst_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            if (this.prsst_cur_indx < 0)
            {
                this.prsst_cur_indx = 0;
            }
            Global.mnFrm.cmCde.navFuncts.currentNavigationIndex = this.prsst_cur_indx;
        }

        private void updtPrsStNavLabels()
        {
            this.moveFirstPrsStButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveFirstBtnStatus();
            this.movePreviousPrsStButton.Enabled = Global.mnFrm.cmCde.navFuncts.movePrevBtnStatus();
            this.moveNextPrsStButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveNextBtnStatus();
            this.moveLastPrsStButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveLastBtnStatus();
            this.positionPrsStTextBox.Text = Global.mnFrm.cmCde.navFuncts.displayedRecordsNumbers();
            if (this.is_last_prsst == true ||
              this.totl_prsst != Global.mnFrm.cmCde.Big_Val)
            {
                this.totalRecsPrsStLabel.Text = Global.mnFrm.cmCde.navFuncts.totalRecordsLabel();
            }
            else
            {
                this.totalRecsPrsStLabel.Text = "of Total";
            }
        }

        private void populatePrsStListVw()
        {
            this.obey_prsst_evnts = false;
            DataSet dtst = Global.get_Basic_PrsSt(this.searchForPrsStTextBox.Text,
              this.searchInPrsStComboBox.Text, this.prsst_cur_indx,
              int.Parse(this.dsplySizePrsStComboBox.Text), Global.mnFrm.cmCde.Org_id);
            this.prsStListView.Items.Clear();

            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.last_prsst_num = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
                ListViewItem nwItem = new ListViewItem(new string[] {
    (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),
    dtst.Tables[0].Rows[i][2].ToString(),
    dtst.Tables[0].Rows[i][3].ToString(),
    dtst.Tables[0].Rows[i][4].ToString(),
    dtst.Tables[0].Rows[i][0].ToString(),
    dtst.Tables[0].Rows[i][5].ToString(),
    dtst.Tables[0].Rows[i][6].ToString()});
                this.prsStListView.Items.Add(nwItem);
            }
            this.correctPrsStNavLbls(dtst);
            if (this.prsStListView.Items.Count > 0)
            {
                this.obey_prsst_evnts = true;
                this.prsStListView.Items[0].Selected = true;
            }
            else
            {
                this.clearPrsStDetInfo();
                this.loadPrsStDetPanel();
            }
            this.obey_prsst_evnts = true;
        }

        private void correctPrsStNavLbls(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.prsst_cur_indx == 0 && totlRecs == 0)
            {
                this.is_last_prsst = true;
                this.totl_prsst = 0;
                this.last_prsst_num = 0;
                this.prsst_cur_indx = 0;
                this.updtPrsStTotals();
                this.updtPrsStNavLabels();
            }
            else if (this.totl_prsst == Global.mnFrm.cmCde.Big_Val
           && totlRecs < long.Parse(this.dsplySizePrsStComboBox.Text))
            {
                this.totl_prsst = this.last_prsst_num;
                if (totlRecs == 0)
                {
                    this.prsst_cur_indx -= 1;
                    this.updtPrsStTotals();
                    this.populatePrsStListVw();
                }
                else
                {
                    this.updtPrsStTotals();
                }
            }
        }

        private bool shdObeyPrsStEvts()
        {
            return this.obey_prsst_evnts;
        }

        private void PrsStPnlNavButtons(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecsPrsStLabel.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.is_last_prsst = false;
                this.prsst_cur_indx = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.is_last_prsst = false;
                this.prsst_cur_indx -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.is_last_prsst = false;
                this.prsst_cur_indx += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.is_last_prsst = true;
                this.totl_prsst = Global.get_Total_PrsSt(this.searchForPrsStTextBox.Text,
                  this.searchInPrsStComboBox.Text, Global.mnFrm.cmCde.Org_id);
                this.updtPrsStTotals();
                this.prsst_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            this.getPrsStPnlData();
        }

        private void clearPrsStDetInfo()
        {
            this.obey_prsst_evnts = false;
            this.savePrsStButton.Enabled = false;
            this.addPrsStButton.Enabled = this.addPrsSts;
            this.editPrsStButton.Enabled = this.editPrsSts;
            this.delPrsStButton.Enabled = this.delPrsSts;
            this.prsStIDTextBox.Text = "-1";
            this.prsStNmTextBox.Text = "";
            this.prsStDescTextBox.Text = "";
            this.prsStSQLTextBox.Text = "";
            this.isEnbldPrsStCheckBox.Checked = false;
            this.isDfltPrsnStCheckBox.Checked = false;
            this.useSQLPrsStCheckBox.Checked = false;
            this.prsStSQLTextBox.Enabled = false;
            this.prsStSQLButton.Enabled = false;
            this.addPrsButton.Enabled = false;
            this.removePrsButton.Enabled = false;
            this.prsStDtListView.Items.Clear();

            this.obey_prsst_evnts = true;
        }

        private void prpareForPrsStDetEdit()
        {
            this.savePrsStButton.Enabled = true;
            this.prsStNmTextBox.ReadOnly = false;
            this.prsStNmTextBox.BackColor = Color.FromArgb(255, 255, 128);
            this.prsStDescTextBox.ReadOnly = false;
            this.prsStDescTextBox.BackColor = Color.White;
            if (this.useSQLPrsStCheckBox.Checked == true)
            {
                this.prsStSQLTextBox.ReadOnly = false;
                this.prsStSQLTextBox.BackColor = Color.FromArgb(255, 255, 128);
            }
        }

        private void disablePrsStDetEdit()
        {
            this.addPrsSt = false;
            this.editPrsSt = false;
            this.prsStNmTextBox.ReadOnly = true;
            this.prsStNmTextBox.BackColor = Color.WhiteSmoke;
            this.prsStDescTextBox.ReadOnly = true;
            this.prsStDescTextBox.BackColor = Color.WhiteSmoke;
            this.prsStSQLTextBox.ReadOnly = true;
            this.prsStSQLTextBox.BackColor = Color.WhiteSmoke;
        }

        private void prsStListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.shdObeyPrsStEvts() == false || this.prsStListView.SelectedItems.Count > 1)
            {
                return;
            }
            if (this.prsStListView.SelectedItems.Count > 0)
            {
                this.populatePrsStDet();
            }
            else
            {
                this.clearPrsStDetInfo();
                this.disablePrsStDetEdit();
                this.loadPrsStDetPanel();
            }
        }

        private void populatePrsStDet()
        {
            this.clearPrsStDetInfo();
            this.disablePrsStDetEdit();
            this.obey_prsst_evnts = false;
            this.prsStDtListView.Items.Clear();
            this.prsStIDTextBox.Text = this.prsStListView.SelectedItems[0].SubItems[5].Text;
            this.prsStNmTextBox.Text = this.prsStListView.SelectedItems[0].SubItems[1].Text;
            this.prsStDescTextBox.Text = this.prsStListView.SelectedItems[0].SubItems[2].Text;
            this.isEnbldPrsStCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(
              this.prsStListView.SelectedItems[0].SubItems[3].Text);
            this.isDfltPrsnStCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(
              this.prsStListView.SelectedItems[0].SubItems[6].Text);
            this.useSQLPrsStCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(
            this.prsStListView.SelectedItems[0].SubItems[7].Text);
            this.prsStSQLTextBox.Text = this.prsStListView.SelectedItems[0].SubItems[4].Text;

            if (this.useSQLPrsStCheckBox.Checked == false)
            {
                this.prsStSQLTextBox.Text = "";
                this.prsStSQLTextBox.Enabled = false;
                this.prsStSQLButton.Enabled = false;
                this.addPrsButton.Enabled = true;
                this.removePrsButton.Enabled = true;
            }
            else
            {
                this.prsStSQLTextBox.Enabled = true;
                this.prsStSQLButton.Enabled = true;
                this.addPrsButton.Enabled = false;
                this.removePrsButton.Enabled = false;
            }
            this.populateRolesLstVw1();
            this.loadPrsStDetPanel();
            this.obey_prsst_evnts = true;
        }

        private void loadPrsStDetPanel()
        {
            this.obey_prsdet_evnts = false;
            int dsply = 0;
            if (this.dsplySizePrsStDtComboBox.Text == ""
             || int.TryParse(this.dsplySizePrsStDtComboBox.Text, out dsply) == false)
            {
                this.dsplySizePrsStDtComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            //this.groupBox8.Height = this.g.Bottom - this.toolStrip4.Bottom - 50;
            this.prsdet_cur_indx = 0;
            this.is_last_prsdet = false;
            this.last_prsdet_num = 0;
            this.totl_prsdet = Global.mnFrm.cmCde.Big_Val;
            this.getPrsdetPnlData();
            this.obey_prsdet_evnts = true;
        }

        private void getPrsdetPnlData()
        {
            this.updtPrsdetTotals();
            this.populatePrsdetListVw();
            this.updtPrsdetNavLabels();
        }

        private void updtPrsdetTotals()
        {
            int dsply = 0;
            if (this.dsplySizePrsStDtComboBox.Text == ""
              || int.TryParse(this.dsplySizePrsStDtComboBox.Text, out dsply) == false)
            {
                this.dsplySizePrsStDtComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            this.myNav.FindNavigationIndices(
          long.Parse(this.dsplySizePrsStDtComboBox.Text), this.totl_prsdet);
            if (this.prsdet_cur_indx >= this.myNav.totalGroups)
            {
                this.prsdet_cur_indx = this.myNav.totalGroups - 1;
            }
            if (this.prsdet_cur_indx < 0)
            {
                this.prsdet_cur_indx = 0;
            }
            this.myNav.currentNavigationIndex = this.prsdet_cur_indx;
        }

        private void updtPrsdetNavLabels()
        {
            this.moveFirstPrsStDtButton.Enabled = this.myNav.moveFirstBtnStatus();
            this.movePreviousPrsStDtButton.Enabled = this.myNav.movePrevBtnStatus();
            this.moveNextPrsStDtButton.Enabled = this.myNav.moveNextBtnStatus();
            this.moveLastPrsStDtButton.Enabled = this.myNav.moveLastBtnStatus();
            this.positionPrsStDtTextBox.Text = this.myNav.displayedRecordsNumbers();
            if (this.is_last_prsdet == true ||
             this.totl_prsdet != Global.mnFrm.cmCde.Big_Val)
            {
                this.totalRecsPrsStDtLabel.Text = this.myNav.totalRecordsLabel();
            }
            else
            {
                this.totalRecsPrsStDtLabel.Text = "of Total";
            }
        }

        private void populatePrsdetListVw()
        {
            this.obey_prsdet_evnts = false;

            DataSet dtst = Global.get_One_PrsStDet(int.Parse(this.prsStIDTextBox.Text),
              this.prsdet_cur_indx,
             int.Parse(this.dsplySizePrsStDtComboBox.Text));
            this.prsStDtListView.Items.Clear();
            int cols = dtst.Tables[0].Columns.Count;
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                long dtID = -1;
                if (cols == 4)
                {
                    long.TryParse(dtst.Tables[0].Rows[i][3].ToString(), out dtID);
                }
                this.last_prsdet_num = this.myNav.startIndex() + i;
                ListViewItem nwItem = new ListViewItem(new string[] {
        (this.myNav.startIndex() + i).ToString(),
        dtst.Tables[0].Rows[i][1].ToString(),
        dtst.Tables[0].Rows[i][2].ToString(),
        dtst.Tables[0].Rows[i][0].ToString(),
        dtID.ToString()});
                this.prsStDtListView.Items.Add(nwItem);
            }
            this.correctPrsdetNavLbls(dtst);
            this.obey_prsdet_evnts = true;
        }

        private void correctPrsdetNavLbls(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.totl_prsdet == Global.mnFrm.cmCde.Big_Val
          && totlRecs < long.Parse(this.dsplySizePrsStDtComboBox.Text))
            {
                this.totl_prsdet = this.last_prsdet_num;
                if (totlRecs == 0)
                {
                    this.prsdet_cur_indx -= 1;
                    this.updtPrsdetTotals();
                    this.populatePrsdetListVw();
                }
                else
                {
                    this.updtPrsdetTotals();
                }
            }
        }

        private bool shdObeyPrsdetEvts()
        {
            return this.obey_prsdet_evnts;
        }

        private void PrsdetPnlNavButtons(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecsPrsStDtLabel.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.is_last_prsdet = false;
                this.prsdet_cur_indx = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.is_last_prsdet = false;
                this.prsdet_cur_indx -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.is_last_prsdet = false;
                this.prsdet_cur_indx += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.is_last_prsdet = true;
                this.totl_prsdet = Global.get_Total_PrsDet(int.Parse(this.prsStIDTextBox.Text));
                this.updtPrsdetTotals();
                this.prsdet_cur_indx = this.myNav.totalGroups - 1;
            }
            this.getPrsdetPnlData();
        }

        private void goPrsStButton_Click(object sender, EventArgs e)
        {
            this.loadPrsStPanel();
        }

        private void addPrsStButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[14]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            this.clearPrsStDetInfo();
            this.addPrsSt = true;
            this.editPrsSt = false;
            this.prpareForPrsStDetEdit();
            this.addPrsStButton.Enabled = false;
            this.editPrsStButton.Enabled = false;
            this.delPrsStButton.Enabled = false;
            this.prsStDtListView.Items.Clear();
        }

        private void editPrsStButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[15]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.prsStIDTextBox.Text == "" || this.prsStIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("No record to Edit!", 0);
                return;
            }

            this.prpareForPrsStDetEdit();
            this.addPrsStButton.Enabled = false;
            this.editPrsStButton.Enabled = false;
            this.delPrsStButton.Enabled = false;
            this.addPrsSt = false;
            this.editPrsSt = true;
        }

        private void savePrsStButton_Click(object sender, EventArgs e)
        {
            if (this.addPrsSt == true)
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[14]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                     " this action!\nContact your System Administrator!", 0);
                    return;
                }
            }
            else
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[15]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                     " this action!\nContact your System Administrator!", 0);
                    return;
                }
            }
            if (this.prsStNmTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please enter a Person Set Name!", 0);
                return;
            }
            long oldPrsStID = Global.mnFrm.cmCde.getPrsStID(this.prsStNmTextBox.Text, Global.mnFrm.cmCde.Org_id);
            if (oldPrsStID > 0
             && this.addPrsSt == true)
            {
                Global.mnFrm.cmCde.showMsg("Person Set's Name is already in use in this Organisation!", 0);
                return;
            }
            if (oldPrsStID > 0
             && this.editPrsSt == true
             && oldPrsStID.ToString() != this.prsStIDTextBox.Text)
            {
                Global.mnFrm.cmCde.showMsg("New Person Set's Name is already in use in this Organisation!", 0);
                return;
            }
            if (this.useSQLPrsStCheckBox.Checked == true)
            {
                if (this.prsStSQLTextBox.Text == "")
                {
                    Global.mnFrm.cmCde.showMsg("Please enter a Person Set SQL Query!", 0);
                    return;
                }
                try
                {
                    string testSQL = "SELECT DISTINCT " +
               "a.person_id, " +
               "a.local_id_no, " +
               "trim(a.title || ' ' || a.sur_name || ', ' || a.first_name || ' ' || a.other_names) full_name, a.img_location";
                    if (!(this.prsStSQLTextBox.Text.Replace("\r\n", "").Replace("\r", "").ToLower().Replace(" ", "").Contains(testSQL.ToLower().Replace(" ", ""))))
                    {
                        Global.mnFrm.cmCde.showMsg("Person Set SQL Query must start with\r\n " + testSQL, 0);
                        return;
                    }
                    Global.mnFrm.cmCde.selectDataNoParams(this.prsStSQLTextBox.Text);
                }
                catch (Exception ex)
                {
                    Global.mnFrm.cmCde.showMsg(ex.Message + "\r\nPlease enter Valid Person Set SQL Query!", 0);
                    return;
                }
            }
            else
            {
                if (this.prsStSQLTextBox.Text != "")
                {
                    Global.mnFrm.cmCde.showMsg("SQL Query must be Empty!", 0);
                    return;
                }
            }
            //if (this.isDfltPrsnStCheckBox.Checked == true)
            //{
            //  Global.undfltAllPrsSt(Global.mnFrm.cmCde.Org_id);
            //}
            if (this.addPrsSt == true)
            {
                Global.createPrsSt(Global.mnFrm.cmCde.Org_id,
                 this.prsStNmTextBox.Text, this.prsStDescTextBox.Text,
                 this.isEnbldPrsStCheckBox.Checked, this.prsStSQLTextBox.Text
                 , this.isDfltPrsnStCheckBox.Checked, this.useSQLPrsStCheckBox.Checked);
                this.savePrsStButton.Enabled = false;
                this.addPrsSt = false;
                this.editPrsSt = false;
                this.editPrsStButton.Enabled = true;
                this.addPrsStButton.Enabled = true;
                this.delPrsStButton.Enabled = true;
                System.Windows.Forms.Application.DoEvents();
                this.loadPrsStPanel();
            }
            else if (this.editPrsSt == true)
            {
                Global.updatePrsSt(int.Parse(this.prsStIDTextBox.Text),
                 this.prsStNmTextBox.Text, this.prsStDescTextBox.Text,
                 this.isEnbldPrsStCheckBox.Checked, this.prsStSQLTextBox.Text
                 , this.isDfltPrsnStCheckBox.Checked, this.useSQLPrsStCheckBox.Checked);
                this.savePrsStButton.Enabled = false;
                this.editPrsSt = false;
                this.editPrsStButton.Enabled = true;
                this.addPrsStButton.Enabled = true;
                this.loadPrsStPanel();
            }
        }

        private void prsStSQLButton_Click(object sender, EventArgs e)
        {
            try
            {
                string testSQL = "SELECT DISTINCT " +
            "a.person_id, " +
            "a.local_id_no, " +
            "trim(a.title || ' ' || a.sur_name || ', ' || a.first_name || ' ' || a.other_names) full_name, a.img_location";
                //this.prsStSQLTextBox.Text = this.prsStSQLTextBox.Text.Replace("\r\n", "").Replace("\r", "").ToLower().Replace(" ", "");
                if (!(this.prsStSQLTextBox.Text.Replace("\r\n", "").Replace("\r", "").ToLower().Replace(" ", "").Contains(testSQL.ToLower().Replace(" ", ""))))
                {
                    Global.mnFrm.cmCde.showMsg("Person Set SQL Query must start with\r\n" + testSQL, 0);
                    return;
                }
                Global.mnFrm.cmCde.selectDataNoParams(this.prsStSQLTextBox.Text);
                Global.mnFrm.cmCde.showMsg("Person Set SQL Query is Valid!", 3);
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message + "\r\nInvalid Person Set SQL Query!", 0);
                return;
            }
        }

        private void positionPrsStDtTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                this.PrsdetPnlNavButtons(this.movePreviousPrsStDtButton, ex);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
            {
                this.PrsdetPnlNavButtons(this.moveNextPrsStDtButton, ex);
            }
        }

        private void positionPrsStTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                this.PrsStPnlNavButtons(this.movePreviousPrsStButton, ex);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
            {
                this.PrsStPnlNavButtons(this.moveNextPrsStButton, ex);
            }
        }

        private void searchForPrsStTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.goPrsStButton_Click(this.goPrsStButton, ex);
            }
        }

        private void delPrsStButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[16]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.prsStIDTextBox.Text == "" || this.prsStIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select the Person Set to DELETE!", 0);
                return;
            }
            if (Global.isPrsStInUse(int.Parse(this.prsStIDTextBox.Text)) == true)
            {
                Global.mnFrm.cmCde.showMsg("This Person Set has been assigned to a Mass Pay hence cannot be DELETED!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Person Set?" +
         "\r\nThis action cannot be undone!", 1) == DialogResult.No)
            {
                Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            Global.mnFrm.cmCde.deleteGnrlRecs(int.Parse(this.prsStIDTextBox.Text),
           "Person Set Name = " + this.prsStNmTextBox.Text, "pay.pay_prsn_sets_hdr", "prsn_set_hdr_id");

            this.loadPrsStPanel();
        }

        private void vwSQLPrsStButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(this.prsst_SQL, 8);
        }

        private void rcHstryPrsStButton_Click(object sender, EventArgs e)
        {
            if (this.prsStListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(
              Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(
              this.prsStListView.SelectedItems[0].SubItems[5].Text),
              "pay.pay_prsn_sets_hdr", "prsn_set_hdr_id"), 7);
        }

        private void exptPrsStDetMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.prsStDtListView);
        }

        private void rfrshPrsStDetMenuItem_Click(object sender, EventArgs e)
        {
            this.loadPrsStDetPanel();
        }

        private void addPrsStMenuItem_Click(object sender, EventArgs e)
        {
            this.addPrsStButton_Click(this.addPrsStButton, e);
        }

        private void editPrsStMenuItem_Click(object sender, EventArgs e)
        {
            this.editPrsStButton_Click(this.editPrsStButton, e);
        }

        private void delPrsStMenuItem_Click(object sender, EventArgs e)
        {
            this.delPrsStButton_Click(this.delPrsStButton, e);
        }

        private void exptPrsStMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.prsStListView);
        }

        private void vwSQLPrsStMenuItem_Click(object sender, EventArgs e)
        {
            this.vwSQLPrsStButton_Click(this.vwSQLPrsStButton, e);
        }

        private void rcHstryPrsStMenuItem_Click(object sender, EventArgs e)
        {
            this.rcHstryPrsStButton_Click(this.rcHstryPrsStButton, e);
        }

        private void rfrshPrsStMenuItem_Click(object sender, EventArgs e)
        {
            this.goPrsStButton_Click(this.goPrsStButton, e);
        }

        private void isEnbldPrsStCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shdObeyPrsStEvts() == false
            || beenToCheckBx == true)
            {
                beenToCheckBx = false;
                return;
            }
            beenToCheckBx = true;
            if (this.addPrsSt == false && this.editPrsSt == false)
            {
                this.isEnbldPrsStCheckBox.Checked = !this.isEnbldPrsStCheckBox.Checked;
            }
        }

        private void isDfltPrsnStCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shdObeyPrsStEvts() == false
            || beenToCheckBx == true)
            {
                beenToCheckBx = false;
                return;
            }
            beenToCheckBx = true;
            if (this.addPrsSt == false && this.editPrsSt == false)
            {
                this.isDfltPrsnStCheckBox.Checked = !this.isDfltPrsnStCheckBox.Checked;
            }
        }

        private void prsStMnlButton_Click(object sender, EventArgs e)
        {
            string[] selVals = new string[1];
            selVals[0] = this.prsStIDMnlTextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Person Sets for Payments(Enabled)"), ref selVals,
                true, true, Global.mnFrm.cmCde.Org_id, "", "",
             this.srchWrd, "Both", true, " and (tbl1.g IN (" + Global.concatCurRoleIDs() + "))");
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.prsStIDMnlTextBox.Text = selVals[i];
                    this.prsStNmMnlTextBox.Text = Global.mnFrm.cmCde.getPrsStName(int.Parse(selVals[i]));
                }
                this.goPrsButton_Click(this.goPrsButton, e);
            }
        }

        private void prsStListView_ItemSelectionChanged(object sender, System.Windows.Forms.ListViewItemSelectionChangedEventArgs e)
        {
            if (this.shdObeyPrsStEvts() == false)
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
        private void useSQLPrsStCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shdObeyPrsStEvts() == false
            || (beenToCheckBx == true && this.addPrsSt == false && this.editPrsSt == false))
            {
                beenToCheckBx = false;
                return;
            }
            beenToCheckBx = true;
            if (this.addPrsSt == false && this.editPrsSt == false)
            {
                this.useSQLPrsStCheckBox.Checked = !this.useSQLPrsStCheckBox.Checked;
            }
            else
            {
                if (this.useSQLPrsStCheckBox.Checked == false)
                {
                    this.prsStSQLTextBox.Text = "";
                    this.prsStSQLTextBox.Enabled = false;
                    this.prsStSQLTextBox.ReadOnly = true;
                    this.prsStSQLTextBox.BackColor = Color.WhiteSmoke;
                    this.prsStSQLButton.Enabled = false;
                    this.addPrsButton.Enabled = true;
                    this.removePrsButton.Enabled = true;
                }
                else
                {
                    this.prsStSQLTextBox.Enabled = true;
                    this.prsStSQLTextBox.ReadOnly = false;
                    this.prsStSQLTextBox.BackColor = Color.FromArgb(255, 255, 128);
                    this.prsStSQLButton.Enabled = true;
                    this.addPrsButton.Enabled = false;
                    this.removePrsButton.Enabled = false;
                }
            }
        }

        private void addPrsButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[15]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.prsStIDTextBox.Text == ""
              || this.prsStIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please Select a Saved Person Set First!", 0);
                return;
            }
            string[] selValuesIDs = new string[1];
            //for (int j = 0; j < this.itmSetDetListView.Items.Count; j++)
            //{
            //  selValuesIDs[j] = this.itmSetDetListView.Items[j].SubItems[4].Text;
            //}
            //All Org Persons
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
             Global.mnFrm.cmCde.getLovID("Active Persons"), ref selValuesIDs, false, true,
             Global.mnFrm.cmCde.Org_id);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selValuesIDs.Length; i++)
                {
                    if (Global.doesPrsStHvPrs(long.Parse(this.prsStIDTextBox.Text),
                      Global.mnFrm.cmCde.getPrsnID(selValuesIDs[i])) == false)
                    {
                        Global.createPrsStDet(int.Parse(this.prsStIDTextBox.Text),
                         Global.mnFrm.cmCde.getPrsnID(selValuesIDs[i]));
                    }
                }
            }
            this.loadPrsStDetPanel();
        }

        private void removePrsButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[15]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.prsStDtListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the Person(s) to Delete", 0);
                return;
            }
            int cnt = this.prsStDtListView.SelectedItems.Count;
            for (int i = 0; i < cnt; i++)
            {
                Global.deletePrsStDet(long.Parse(
                  this.prsStDtListView.SelectedItems[i].SubItems[4].Text));
            }
            this.loadPrsStDetPanel();
        }
        private void prsStListView_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.Control && e.KeyCode == Keys.S)
            {
                if (this.savePrsStButton.Enabled == true)
                {
                    this.savePrsStButton_Click(this.savePrsStButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                if (this.addPrsStButton.Enabled == true)
                {
                    this.addPrsStButton_Click(this.addPrsStButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                if (this.editPrsStButton.Enabled == true)
                {
                    this.editPrsStButton_Click(this.editPrsStButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                this.resetPrsSetButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)
            {
                if (this.goPrsStButton.Enabled == true)
                {
                    this.goPrsStButton_Click(this.goPrsStButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.Delete)
            {
                if (this.delPrsStButton.Enabled == true)
                {
                    this.delPrsStButton_Click(this.delPrsStButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                Global.mnFrm.cmCde.listViewKeyDown(this.prsStListView, e);
            }
        }

        private void prsStTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.Control && e.KeyCode == Keys.S)
            {
                if (this.savePrsStButton.Enabled == true)
                {
                    this.savePrsStButton_Click(this.savePrsStButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                if (this.addPrsStButton.Enabled == true)
                {
                    this.addPrsStButton_Click(this.addPrsStButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                if (this.editPrsStButton.Enabled == true)
                {
                    this.editPrsStButton_Click(this.editPrsStButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                this.resetPrsSetButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)
            {
                if (this.goPrsStButton.Enabled == true)
                {
                    this.goPrsStButton_Click(this.goPrsStButton, ex);
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

        private void prsStDtListView_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.Control && e.KeyCode == Keys.S)
            {
                if (this.savePrsStButton.Enabled == true)
                {
                    this.savePrsStButton_Click(this.savePrsStButton, ex);
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
                if (this.editPrsStButton.Enabled == true)
                {
                    this.editPrsStButton_Click(this.editPrsStButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                this.resetPrsSetButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)
            {
                //if (this.goPrsStButton.Enabled == true)
                //{
                //  this.goPrsStButton_Click(this.goPrsStButton, ex);
                //}
                this.loadPrsStDetPanel();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.Delete)
            {
                if (this.removePrsButton.Enabled == true)
                {
                    this.removePrsButton_Click(this.removePrsButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                Global.mnFrm.cmCde.listViewKeyDown(this.prsStDtListView, e);
            }
        }
        #endregion
        #region "Mass Pay Run..."
        private void loadMsPyPanel()
        {
            this.obey_mspy_evnts = false;
            if (this.searchInMsPyComboBox.SelectedIndex < 0)
            {
                this.searchInMsPyComboBox.SelectedIndex = 0;
            }
            if (this.searchForMsPyTextBox.Text.Contains("%") == false)
            {
                this.searchForMsPyTextBox.Text = "%" + this.searchForMsPyTextBox.Text.Replace(" ", "%") + "%";
            }
            if (this.searchForMsPyTextBox.Text == "%%")
            {
                this.searchForMsPyTextBox.Text = "%";
            }
            int dsply = 0;
            if (this.dsplySizeMsPyComboBox.Text == ""
              || int.TryParse(this.dsplySizeMsPyComboBox.Text, out dsply) == false)
            {
                this.dsplySizeMsPyComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            //this.groupBox8.Height = this.g.Bottom - this.toolStrip4.Bottom - 50;
            this.is_last_mspy = false;
            this.totl_mspy = Global.mnFrm.cmCde.Big_Val;
            this.getMsPyPnlData();
            this.obey_mspy_evnts = true;
        }

        private void getMsPyPnlData()
        {
            this.updtMsPyTotals();
            this.populateMsPyListVw();
            this.updtMsPyNavLabels();
        }

        private void updtMsPyTotals()
        {
            Global.mnFrm.cmCde.navFuncts.FindNavigationIndices(
              long.Parse(this.dsplySizeMsPyComboBox.Text), this.totl_mspy);
            if (this.mspy_cur_indx >= Global.mnFrm.cmCde.navFuncts.totalGroups)
            {
                this.mspy_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            if (this.mspy_cur_indx < 0)
            {
                this.mspy_cur_indx = 0;
            }
            Global.mnFrm.cmCde.navFuncts.currentNavigationIndex = this.mspy_cur_indx;
        }

        private void updtMsPyNavLabels()
        {
            this.moveFirstMsPyButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveFirstBtnStatus();
            this.movePreviousMsPyButton.Enabled = Global.mnFrm.cmCde.navFuncts.movePrevBtnStatus();
            this.moveNextMsPyButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveNextBtnStatus();
            this.moveLastMsPyButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveLastBtnStatus();
            this.positionMsPyTextBox.Text = Global.mnFrm.cmCde.navFuncts.displayedRecordsNumbers();
            if (this.is_last_mspy == true ||
              this.totl_mspy != Global.mnFrm.cmCde.Big_Val)
            {
                this.totalRecsMsPyLabel.Text = Global.mnFrm.cmCde.navFuncts.totalRecordsLabel();
            }
            else
            {
                this.totalRecsMsPyLabel.Text = "of Total";
            }
        }

        private void populateMsPyListVw()
        {
            this.obey_mspy_evnts = false;
            DataSet dtst = Global.get_Basic_MsPy(this.searchForMsPyTextBox.Text,
              this.searchInMsPyComboBox.Text, this.mspy_cur_indx,
              int.Parse(this.dsplySizeMsPyComboBox.Text), Global.mnFrm.cmCde.Org_id,
              this.vwSelfCheckBox.Checked);
            this.msPyListView.Items.Clear();

            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.last_mspy_num = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
                ListViewItem nwItem = new ListViewItem(new string[] {
    (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),
    dtst.Tables[0].Rows[i][2].ToString(),
    dtst.Tables[0].Rows[i][3].ToString(),
    dtst.Tables[0].Rows[i][4].ToString(),
    dtst.Tables[0].Rows[i][5].ToString(),
    dtst.Tables[0].Rows[i][6].ToString(),
    dtst.Tables[0].Rows[i][0].ToString(),
    dtst.Tables[0].Rows[i][7].ToString(),
    dtst.Tables[0].Rows[i][8].ToString()});
                this.msPyListView.Items.Add(nwItem);
            }
            this.correctMsPyNavLbls(dtst);
            if (this.msPyListView.Items.Count > 0)
            {
                this.obey_mspy_evnts = true;
                this.msPyListView.Items[0].Selected = true;
            }
            else
            {
                this.clearMsPyDetInfo();
                this.loadMsPyDetPanel();
            }
            this.obey_mspy_evnts = true;
        }

        private void correctMsPyNavLbls(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.mspy_cur_indx == 0 && totlRecs == 0)
            {
                this.is_last_mspy = true;
                this.totl_mspy = 0;
                this.last_mspy_num = 0;
                this.mspy_cur_indx = 0;
                this.updtMsPyTotals();
                this.updtMsPyNavLabels();
            }
            else if (this.totl_mspy == Global.mnFrm.cmCde.Big_Val
           && totlRecs < long.Parse(this.dsplySizeMsPyComboBox.Text))
            {
                this.totl_mspy = this.last_mspy_num;
                if (totlRecs == 0)
                {
                    this.mspy_cur_indx -= 1;
                    this.updtMsPyTotals();
                    this.populateMsPyListVw();
                }
                else
                {
                    this.updtMsPyTotals();
                }
            }
        }

        private bool shdObeyMsPyEvts()
        {
            return this.obey_mspy_evnts;
        }

        private void MsPyPnlNavButtons(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecsMsPyLabel.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.is_last_mspy = false;
                this.mspy_cur_indx = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.is_last_mspy = false;
                this.mspy_cur_indx -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.is_last_mspy = false;
                this.mspy_cur_indx += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.is_last_mspy = true;
                this.totl_mspy = Global.get_Total_MsPy(this.searchForMsPyTextBox.Text,
                  this.searchInMsPyComboBox.Text, Global.mnFrm.cmCde.Org_id,
                this.vwSelfCheckBox.Checked);
                this.updtMsPyTotals();
                this.mspy_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            this.getMsPyPnlData();
        }

        private void clearMsPyDetInfo()
        {
            this.obey_mspy_evnts = false;
            this.saveMsPyButton.Enabled = false;
            this.addMsPyButton.Enabled = this.addMsPys;
            this.editMsPyButton.Enabled = this.editMsPys;
            this.delMsPyButton.Enabled = this.delMsPys;
            this.msPyIDTextBox.Text = "-1000000";
            this.msPyNmTextBox.Text = "";
            this.msPyDescTextBox.Text = "";
            this.msPyPrsStIDTextBox.Text = "-1";
            this.msPyPrsStNmTextBox.Text = "";
            this.msPyItmStIDTextBox.Text = "-1";
            this.msPyItmStNmTextBox.Text = "";
            this.trnsDateTextBox.Text = "";
            this.msPyGLDateTextBox.Text = "";
            this.msPyDtListView.Items.Clear();
            this.hsBnRnMsPyCheckBox.Checked = false;
            this.hsGoneToGLCheckBox.Checked = false;
            this.progressBar1.Value = 0;
            this.progressLabel.Text = "0%";
            this.obey_mspy_evnts = true;
        }

        private void prpareForMsPyDetEdit()
        {
            this.saveMsPyButton.Enabled = true;
            this.msPyNmTextBox.ReadOnly = false;
            this.msPyNmTextBox.BackColor = Color.FromArgb(255, 255, 128);
            this.msPyDescTextBox.ReadOnly = false;
            this.msPyDescTextBox.BackColor = Color.White;
            this.msPyPrsStNmTextBox.ReadOnly = false;
            this.msPyPrsStNmTextBox.BackColor = Color.FromArgb(255, 255, 128);
            this.msPyItmStNmTextBox.ReadOnly = false;
            this.msPyItmStNmTextBox.BackColor = Color.FromArgb(255, 255, 128);
            this.trnsDateTextBox.ReadOnly = false;
            this.trnsDateTextBox.BackColor = Color.FromArgb(255, 255, 128);
            this.msPyGLDateTextBox.ReadOnly = false;
            this.msPyGLDateTextBox.BackColor = Color.FromArgb(255, 255, 128);
            this.hsBnRnMsPyCheckBox.Enabled = false;
            this.hsGoneToGLCheckBox.Enabled = false;
        }

        private void disableMsPyDetEdit()
        {
            this.addMsPy = false;
            this.editMsPy = false;
            this.msPyNmTextBox.ReadOnly = true;
            this.msPyNmTextBox.BackColor = Color.WhiteSmoke;
            this.msPyDescTextBox.ReadOnly = true;
            this.msPyDescTextBox.BackColor = Color.WhiteSmoke;
            this.msPyPrsStNmTextBox.ReadOnly = true;
            this.msPyPrsStNmTextBox.BackColor = Color.WhiteSmoke;
            this.msPyItmStNmTextBox.ReadOnly = true;
            this.msPyItmStNmTextBox.BackColor = Color.WhiteSmoke;
            this.trnsDateTextBox.ReadOnly = true;
            this.trnsDateTextBox.BackColor = Color.WhiteSmoke;
            this.msPyGLDateTextBox.ReadOnly = true;
            this.msPyGLDateTextBox.BackColor = Color.WhiteSmoke;

            this.hsBnRnMsPyCheckBox.Enabled = true;
            this.hsGoneToGLCheckBox.Enabled = true;
        }

        private void msPyListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.shdObeyMsPyEvts() == false || this.msPyListView.SelectedItems.Count > 1)
            {
                return;
            }
            if (this.msPyListView.SelectedItems.Count > 0)
            {
                this.populateMsPyDet();
            }
            else
            {
                this.clearMsPyDetInfo();
                this.disableMsPyDetEdit();
                this.loadMsPyDetPanel();
            }
        }

        private void populateMsPyDet()
        {
            this.clearMsPyDetInfo();
            this.disableMsPyDetEdit();
            this.obey_mspy_evnts = false;
            this.msPyDtListView.Items.Clear();
            this.msPyIDTextBox.Text = this.msPyListView.SelectedItems[0].SubItems[7].Text;
            this.msPyNmTextBox.Text = this.msPyListView.SelectedItems[0].SubItems[1].Text;
            this.msPyDescTextBox.Text = this.msPyListView.SelectedItems[0].SubItems[2].Text;
            this.hsBnRnMsPyCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(
              this.msPyListView.SelectedItems[0].SubItems[3].Text);
            this.hsGoneToGLCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(
              this.msPyListView.SelectedItems[0].SubItems[8].Text);
            this.trnsDateTextBox.Text = this.msPyListView.SelectedItems[0].SubItems[4].Text;
            this.msPyGLDateTextBox.Text = this.msPyListView.SelectedItems[0].SubItems[9].Text;
            this.msPyPrsStIDTextBox.Text = this.msPyListView.SelectedItems[0].SubItems[5].Text;
            this.msPyPrsStNmTextBox.Text = Global.mnFrm.cmCde.getPrsStName(int.Parse(this.msPyPrsStIDTextBox.Text));
            this.msPyItmStIDTextBox.Text = this.msPyListView.SelectedItems[0].SubItems[6].Text;
            this.msPyItmStNmTextBox.Text = Global.mnFrm.cmCde.getItmStName(int.Parse(this.msPyItmStIDTextBox.Text));
            this.loadMsPyDetPanel();
            this.obey_mspy_evnts = true;
        }

        private void loadMsPyDetPanel()
        {
            this.obey_mspydt_evnts = false;
            int dsply = 0;
            if (this.dsplySizeMsPyDtComboBox.Text == ""
             || int.TryParse(this.dsplySizeMsPyDtComboBox.Text, out dsply) == false)
            {
                this.dsplySizeMsPyDtComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            //this.groupBox8.Height = this.g.Bottom - this.toolStrip4.Bottom - 50;
            this.mspydt_cur_indx = 0;
            this.is_last_mspydt = false;
            this.last_mspydt_num = 0;
            this.totl_mspydt = Global.mnFrm.cmCde.Big_Val;
            this.getMsPyDtPnlData();
            this.obey_mspydt_evnts = true;
        }

        private void getMsPyDtPnlData()
        {
            this.updtMsPyDtTotals();
            this.populateMsPyDtListVw();
            this.updtMsPyDtNavLabels();
        }

        private void updtMsPyDtTotals()
        {
            int dsply = 0;
            if (this.dsplySizeMsPyDtComboBox.Text == ""
              || int.TryParse(this.dsplySizeMsPyDtComboBox.Text, out dsply) == false)
            {
                this.dsplySizeMsPyDtComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            this.myNav.FindNavigationIndices(
          long.Parse(this.dsplySizeMsPyDtComboBox.Text), this.totl_mspydt);
            if (this.mspydt_cur_indx >= this.myNav.totalGroups)
            {
                this.mspydt_cur_indx = this.myNav.totalGroups - 1;
            }
            if (this.mspydt_cur_indx < 0)
            {
                this.mspydt_cur_indx = 0;
            }
            this.myNav.currentNavigationIndex = this.mspydt_cur_indx;
        }

        private void updtMsPyDtNavLabels()
        {
            this.moveFirstMsPyDtButton.Enabled = this.myNav.moveFirstBtnStatus();
            this.movePreviousMsPyDtButton.Enabled = this.myNav.movePrevBtnStatus();
            this.moveNextMsPyDtButton.Enabled = this.myNav.moveNextBtnStatus();
            this.moveLastMsPyDtButton.Enabled = this.myNav.moveLastBtnStatus();
            this.positionMsPyDtTextBox.Text = this.myNav.displayedRecordsNumbers();
            if (this.is_last_mspydt == true ||
             this.totl_mspydt != Global.mnFrm.cmCde.Big_Val)
            {
                this.totalRecsMsPyDtLabel.Text = this.myNav.totalRecordsLabel();
            }
            else
            {
                this.totalRecsMsPyDtLabel.Text = "of Total";
            }
        }

        private void populateMsPyDtListVw()
        {
            this.obey_mspydt_evnts = false;

            DataSet dtst = Global.get_One_MsPyDet(
              this.mspydt_cur_indx,
             int.Parse(this.dsplySizeMsPyDtComboBox.Text),
             long.Parse(this.msPyIDTextBox.Text));

            this.msPyDtListView.Items.Clear();
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.last_mspydt_num = this.myNav.startIndex() + i;
                string uom = "Number";
                if (dtst.Tables[0].Rows[i][9].ToString() != "-1")
                {
                    uom = Global.mnFrm.cmCde.getPssblValNm(int.Parse(dtst.Tables[0].Rows[i][9].ToString()));
                }
                ListViewItem nwItem = new ListViewItem(new string[] {
    (this.myNav.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][10].ToString(),
                dtst.Tables[0].Rows[i][11].ToString(),
    dtst.Tables[0].Rows[i][12].ToString(),
    double.Parse(dtst.Tables[0].Rows[i][3].ToString()).ToString("#,##0.00"),
    uom,
    dtst.Tables[0].Rows[i][6].ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),
    dtst.Tables[0].Rows[i][2].ToString(),
    dtst.Tables[0].Rows[i][0].ToString(),
    dtst.Tables[0].Rows[i][13].ToString()});
                nwItem.UseItemStyleForSubItems = false;
                if (dtst.Tables[0].Rows[i][13].ToString() == "VALID")
                {
                    nwItem.SubItems[10].BackColor = Color.Lime;
                }
                else
                {
                    nwItem.SubItems[10].BackColor = Color.Red;
                }
                this.msPyDtListView.Items.Add(nwItem);
            }
            /*
          Global.get_GLBatch_Nm(long.Parse(dtst.Tables[0].Rows[i][8].ToString())),*/
            this.correctMsPyDtNavLbls(dtst);
            this.obey_mspydt_evnts = true;
        }

        private void correctMsPyDtNavLbls(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.totl_mspydt == Global.mnFrm.cmCde.Big_Val
          && totlRecs < long.Parse(this.dsplySizeMsPyDtComboBox.Text))
            {
                this.totl_mspydt = this.last_mspydt_num;
                if (totlRecs == 0)
                {
                    this.mspydt_cur_indx -= 1;
                    this.updtMsPyDtTotals();
                    this.populateMsPyDtListVw();
                }
                else
                {
                    this.updtMsPyDtTotals();
                }
            }
        }

        private bool shdObeyMsPyDtEvts()
        {
            return this.obey_mspydt_evnts;
        }

        private void MsPyDtPnlNavButtons(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecsMsPyDtLabel.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.is_last_mspydt = false;
                this.mspydt_cur_indx = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.is_last_mspydt = false;
                this.mspydt_cur_indx -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.is_last_mspydt = false;
                this.mspydt_cur_indx += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.is_last_mspydt = true;
                this.totl_mspydt = Global.get_Total_MsPyDt(long.Parse(this.msPyIDTextBox.Text));
                this.updtMsPyDtTotals();
                this.mspydt_cur_indx = this.myNav.totalGroups - 1;
            }
            this.getMsPyDtPnlData();
        }

        private void goMsPyButton_Click(object sender, EventArgs e)
        {
            this.loadMsPyPanel();
        }

        private void trnsDateButton_Click(object sender, EventArgs e)
        {
            if (this.addMsPy == false && this.editMsPy == false)
            {
                Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                return;
            }
            Global.mnFrm.cmCde.selectDate(ref this.trnsDateTextBox);
            if (this.msPyGLDateTextBox.Text != this.trnsDateTextBox.Text)
            {
                this.msPyGLDateTextBox.Text = this.trnsDateTextBox.Text;
            }
        }

        private void msPyPrsStButton_Click(object sender, EventArgs e)
        {
            if (this.addMsPy == false && this.editMsPy == false)
            {
                Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                return;
            }
            string[] selVals = new string[1];
            selVals[0] = this.msPyPrsStIDTextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Person Sets for Payments(Enabled)"), ref selVals,
                true, true, Global.mnFrm.cmCde.Org_id,
             this.srchWrd, "Both", true);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.msPyPrsStIDTextBox.Text = selVals[i];
                    this.msPyPrsStNmTextBox.Text = Global.mnFrm.cmCde.getPrsStName(int.Parse(selVals[i]));
                }
            }
        }

        private void msPyItmStButton_Click(object sender, EventArgs e)
        {
            if (this.addMsPy == false && this.editMsPy == false)
            {
                Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                return;
            }
            string[] selVals = new string[1];
            selVals[0] = this.msPyItmStIDTextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Item Sets for Payments(Enabled)"), ref selVals,
          true, true, Global.mnFrm.cmCde.Org_id, "", "",
       this.srchWrd, "Both", true, " and (tbl1.g IN (" + Global.concatCurRoleIDs() + "))");

            //DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
            //    Global.mnFrm.cmCde.getLovID("Item Sets for Payments(Enabled)"), ref selVals,
            //    true, true, Global.mnFrm.cmCde.Org_id,
            // this.srchWrd, "Both", true, " and (tbl1.g IN (" + Global.concatCurRoleIDs() + "))");
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.msPyItmStIDTextBox.Text = selVals[i];
                    this.msPyItmStNmTextBox.Text = Global.mnFrm.cmCde.getItmStName(int.Parse(selVals[i]));
                }
            }
        }

        private void addMsPyButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            this.clearMsPyDetInfo();
            this.addMsPy = true;
            this.editMsPy = false;
            this.prpareForMsPyDetEdit();
            this.addMsPyButton.Enabled = false;
            this.editMsPyButton.Enabled = false;
            this.delMsPyButton.Enabled = false;
            this.msPyDtListView.Items.Clear();

            this.msPyNmTextBox.Text = Global.mnFrm.cmCde.getUsername(Global.myPay.user_id).ToUpper()
         + "-" + Global.mnFrm.cmCde.getDB_Date_time().Replace(":", "").Replace("-", "").Replace(" ", "") + "-" +
         Global.getNewMsPyID().ToString().PadLeft(4, '0');
            this.msPyGLDateTextBox.Text = Global.mnFrm.cmCde.getFrmtdDB_Date_time();
            //this.msPyGLDateTextBox.Text = this.trnsDateTextBox.Text;
            //this.saveButton_Click(this.saveButton, e);
        }

        private void editMsPyButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[18]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.msPyIDTextBox.Text == "" || this.msPyIDTextBox.Text == "-1000000")
            {
                Global.mnFrm.cmCde.showMsg("No record to Edit!", 0);
                return;
            }
            if (this.msPyPrsStIDTextBox.Text == "-1000010")
            {
                Global.mnFrm.cmCde.showMsg("Cannot Edit Quick Pay Runs!", 0);
                return;
            }
            if (this.hsBnRnMsPyCheckBox.Checked == true)
            {
                Global.mnFrm.cmCde.showMsg("Cannot Edit a Mass Pay that has been run!", 0);
                return;
            }
            this.addMsPy = false;
            this.editMsPy = true;

            this.prpareForMsPyDetEdit();
            this.addMsPyButton.Enabled = false;
            this.editMsPyButton.Enabled = false;
            this.delMsPyButton.Enabled = false;
        }

        private void saveMsPyButton_Click(object sender, EventArgs e)
        {
            if (this.addMsPy == true)
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                     " this action!\nContact your System Administrator!", 0);
                    return;
                }
            }
            else
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[18]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                     " this action!\nContact your System Administrator!", 0);
                    return;
                }
            }
            if (this.msPyNmTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please enter a Mass Pay Run Name!", 0);
                return;
            }
            long oldMsPyID = Global.mnFrm.cmCde.getMsPyID(this.msPyNmTextBox.Text, Global.mnFrm.cmCde.Org_id);
            if (oldMsPyID > 0
             && this.addMsPy == true)
            {
                Global.mnFrm.cmCde.showMsg("Mass Pay Run Name is already in use in this Organisation!", 0);
                return;
            }
            if (oldMsPyID > 0
             && this.editMsPy == true
             && oldMsPyID.ToString() != this.msPyIDTextBox.Text)
            {
                Global.mnFrm.cmCde.showMsg("New Mass Pay Run Name is already in use in this Organisation!", 0);
                return;
            }
            if (this.trnsDateTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please enter a Mass Pay Run Date!", 0);
                return;
            }

            if (this.msPyGLDateTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please enter a Mass Pay Run GL Date!", 0);
                return;
            }

            if (this.msPyPrsStIDTextBox.Text == "" || this.msPyPrsStIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select a Mass Pay Person Set!", 0);
                return;
            }
            if (this.msPyItmStIDTextBox.Text == "" || this.msPyItmStIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please enter a Mass Pay Item Set!", 0);
                return;
            }

            if (this.addMsPy == true)
            {
                Global.createMsPy(Global.mnFrm.cmCde.Org_id,
                 this.msPyNmTextBox.Text, this.msPyDescTextBox.Text, this.trnsDateTextBox.Text,
                 int.Parse(this.msPyPrsStIDTextBox.Text), int.Parse(this.msPyItmStIDTextBox.Text),
                 this.msPyGLDateTextBox.Text);
                this.saveMsPyButton.Enabled = false;
                this.addMsPy = false;
                this.editMsPy = false;
                this.editMsPyButton.Enabled = this.editMsPys;
                this.addMsPyButton.Enabled = this.addMsPys;
                this.delMsPyButton.Enabled = this.delMsPys;
                System.Windows.Forms.Application.DoEvents();
                this.loadMsPyPanel();
                this.editMsPyButton_Click(this.editMsPyButton, e);
            }
            else if (this.editMsPy == true)
            {
                Global.updateMsPy(long.Parse(this.msPyIDTextBox.Text),
                 this.msPyNmTextBox.Text, this.msPyDescTextBox.Text, this.trnsDateTextBox.Text,
                 int.Parse(this.msPyPrsStIDTextBox.Text), int.Parse(this.msPyItmStIDTextBox.Text),
                 this.msPyGLDateTextBox.Text);
                this.saveMsPyButton.Enabled = false;
                this.editMsPy = false;
                this.editMsPyButton.Enabled = this.editMsPys;
                this.addMsPyButton.Enabled = this.addMsPys;
                this.loadMsPyPanel();
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
            }
            else if (e.Error != null)
            {
                Global.mnFrm.cmCde.showMsg("Error: " + e.Error.Message, 4);
            }
            else
            {
                Global.mnFrm.cmCde.showMsg("Operation Completed Successfully!", 3);
            }
            this.msPayActRnButton.Enabled = true;
            this.rllbckMsPyRnButton.Enabled = true;
            this.sndMsPyToGLButton.Enabled = true;
            this.cancelRunButton.Enabled = false;
            this.attchedValsButton.Enabled = true;
            this.loadMsPyPanel();
        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;
            this.progressLabel.Text = e.ProgressPercentage.ToString() + "%";
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            Object[] myargs = (Object[])e.Argument;

            //Get dataset for Pay Items to pay
            //Get dataset for persons to be paid
            //loop through persons and for each person loop through all pay items
            DataSet itmsDtSt = Global.get_AllItmStDet(int.Parse((string)myargs[3]));
            DataSet prsDtSt = Global.get_AllPrsStDet(int.Parse((string)myargs[2]));
            int prsCnt = prsDtSt.Tables[0].Rows.Count;
            int itmCnt = itmsDtSt.Tables[0].Rows.Count;
            string dateStr = (string)myargs[5];
            string gldateStr = (string)myargs[6];
            bool shdSkip = (bool)myargs[7];
            string itmAssgnDte = (string)myargs[8];

            long msg_id = Global.mnFrm.cmCde.getLogMsgID("pay.pay_mass_pay_run_msgs", "Mass Pay Run", long.Parse((string)myargs[0]));
            if (msg_id <= 0)
            {
                Global.mnFrm.cmCde.createLogMsg(dateStr + " .... Mass Pay Run is about to Start...",
            "pay.pay_mass_pay_run_msgs", "Mass Pay Run", long.Parse((string)myargs[0]), dateStr);
            }
            msg_id = Global.mnFrm.cmCde.getLogMsgID("pay.pay_mass_pay_run_msgs", "Mass Pay Run", long.Parse((string)myargs[0]));
            string retmsg = "";
            decimal outstandgAdvcAmnt = 0;
            long advBlsItmID = Global.mnFrm.cmCde.getItmID("Total Advance Payments Balance", Global.mnFrm.cmCde.Org_id);
            long advApplyItmID = Global.mnFrm.cmCde.getItmID("Advance Payments Amount Applied", Global.mnFrm.cmCde.Org_id);
            long advApplyItmValID = Global.getFirstItmValID(advApplyItmID);
            decimal payItmAmnt = 0;
            string trDte = (string)myargs[1];
            for (int i = 0; i < prsCnt; i++)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    this.msPayActRnButton.Enabled = true;
                    this.rllbckMsPyRnButton.Enabled = true;
                    this.sndMsPyToGLButton.Enabled = true;
                    this.cancelRunButton.Enabled = false;
                    this.attchedValsButton.Enabled = true;
                    this.loadMsPyPanel();
                    break;
                }
                else
                {
                    //Loop through all items to pay them for this person only
                    outstandgAdvcAmnt = (decimal)Global.getBlsItmLtstDailyBals(advBlsItmID,
                            long.Parse(prsDtSt.Tables[0].Rows[i][0].ToString()), trDte);

                    for (int j = 0; j < itmCnt; j++)
                    {
                        if (i == 0)
                        {
                            if (!this.isMsPayTrnsValid(
                              itmsDtSt.Tables[0].Rows[j][2].ToString(),
                              itmsDtSt.Tables[0].Rows[j][4].ToString(),
                              itmsDtSt.Tables[0].Rows[j][5].ToString(),
                              int.Parse(itmsDtSt.Tables[0].Rows[j][0].ToString()), gldateStr, 1000))
                            {
                                this.backgroundWorker1.CancelAsync();
                                worker.ReportProgress(Convert.ToInt32((i + 1) * (99.0 / prsCnt)));
                                break;
                            }
                        }
                        payItmAmnt = 0;
                        retmsg = this.runMassPay(int.Parse((string)myargs[4]),
                           long.Parse(prsDtSt.Tables[0].Rows[i][0].ToString()),
                           prsDtSt.Tables[0].Rows[i][2].ToString() + " (" + prsDtSt.Tables[0].Rows[i][1].ToString() + ")",
                           int.Parse(itmsDtSt.Tables[0].Rows[j][0].ToString()),
                           itmsDtSt.Tables[0].Rows[j][1].ToString(),
                           itmsDtSt.Tables[0].Rows[j][2].ToString(),
                           long.Parse((string)myargs[0]), (string)myargs[1],
                           itmsDtSt.Tables[0].Rows[j][3].ToString(),
                           itmsDtSt.Tables[0].Rows[j][4].ToString(),
                           itmsDtSt.Tables[0].Rows[j][5].ToString(), msg_id,
                           "pay.pay_mass_pay_run_msgs", dateStr, gldateStr,
                           shdSkip, itmAssgnDte, "", ref payItmAmnt);
                        if (retmsg == "Stop")
                        {
                            this.backgroundWorker1.CancelAsync();
                            worker.ReportProgress(Convert.ToInt32((i + 1) * (99.0 / prsCnt)));
                            break;
                        }
                        if (outstandgAdvcAmnt > 0 && itmsDtSt.Tables[0].Rows[j][5].ToString() == "Bills/Charges")
                        {
                            decimal advPymnt = 0;
                            if (payItmAmnt > outstandgAdvcAmnt)
                            {
                                advPymnt = Math.Round(outstandgAdvcAmnt, 4);
                                outstandgAdvcAmnt = 0;
                            }
                            else
                            {
                                advPymnt = payItmAmnt;
                                outstandgAdvcAmnt -= payItmAmnt;
                            }
                            string trnsDesc = "Advance Payments Amount Applied for " + prsDtSt.Tables[0].Rows[i][1].ToString() + " in settlement of " + itmsDtSt.Tables[0].Rows[j][1].ToString();

                            retmsg = this.runMassPay(int.Parse((string)myargs[4]),
                             long.Parse(prsDtSt.Tables[0].Rows[i][0].ToString()),
                             prsDtSt.Tables[0].Rows[i][2].ToString() + " (" + prsDtSt.Tables[0].Rows[i][1].ToString() + ")",
                             advApplyItmID,
                             "Advance Payments Amount Applied",
                             "Money",
                             long.Parse((string)myargs[0]),
                             trDte.Substring(0, 12) + trDte.Substring(12, 3) + (j % 60).ToString().PadLeft(2, '0') + ":" + (j % 60).ToString().PadLeft(2, '0'),
                             "Payment by Organisation",
                             "Pay Value Item",
                             "Earnings", msg_id,
                             "pay.pay_mass_pay_run_msgs", dateStr, gldateStr,
                             shdSkip, itmAssgnDte, trnsDesc, ref advPymnt);
                            if (retmsg == "Stop")
                            {
                                this.backgroundWorker1.CancelAsync();
                                worker.ReportProgress(Convert.ToInt32((i + 1) * (99.0 / prsCnt)));
                                break;
                            }
                            advPymnt = (-1 * advPymnt);
                            retmsg = this.runMassPay(int.Parse((string)myargs[4]),
                             long.Parse(prsDtSt.Tables[0].Rows[i][0].ToString()),
                             prsDtSt.Tables[0].Rows[i][2].ToString() + " (" + prsDtSt.Tables[0].Rows[i][1].ToString() + ")",
                             int.Parse(itmsDtSt.Tables[0].Rows[j][0].ToString()),
                             itmsDtSt.Tables[0].Rows[j][1].ToString(),
                             itmsDtSt.Tables[0].Rows[j][2].ToString(),
                             long.Parse((string)myargs[0]),
                             trDte.Substring(0, 12) + trDte.Substring(12, 3) + (j % 60).ToString().PadLeft(2, '0') + ":" + (j % 60).ToString().PadLeft(2, '0'),
                             itmsDtSt.Tables[0].Rows[j][3].ToString(),
                             itmsDtSt.Tables[0].Rows[j][4].ToString(),
                             itmsDtSt.Tables[0].Rows[j][5].ToString(), msg_id,
                             "pay.pay_mass_pay_run_msgs", dateStr, gldateStr,
                             shdSkip, itmAssgnDte, trnsDesc, ref advPymnt);
                            if (retmsg == "Stop")
                            {
                                this.backgroundWorker1.CancelAsync();
                                worker.ReportProgress(Convert.ToInt32((i + 1) * (99.0 / prsCnt)));
                                break;
                            }
                        }

                        if (retmsg == "Stop")
                        {
                            this.backgroundWorker1.CancelAsync();
                            worker.ReportProgress(Convert.ToInt32((i + 1) * (99.0 / prsCnt)));
                            break;
                        }
                    }
                    //System.Threading.Thread.Sleep(500);
                    worker.ReportProgress(Convert.ToInt32((i + 1) * (99.0 / prsCnt)));
                }
            }
            //Do some summation checks before updating the Status
            //Function to check if sum of debits is equal sum of credits to sum of amnts in all these pay trns
            //if correct the set gone to gl to '1' else '0'
            double pytrnsamnt = Global.getMsPyAmntSum(long.Parse((string)myargs[0]));
            double intfcDbtAmnt = Global.getMsPyIntfcDbtSum(long.Parse((string)myargs[0]));
            double intfcCrdtAmnt = Global.getMsPyIntfcCrdtSum(long.Parse((string)myargs[0]));
            //Global.mnFrm.cmCde.showSQLNoPermsn(pytrnsamnt + "/" + intfcDbtAmnt + "/" + intfcCrdtAmnt);

            if (pytrnsamnt == intfcCrdtAmnt
              && pytrnsamnt == intfcDbtAmnt && pytrnsamnt != 0)
            {
                Global.updateMsPyStatus(long.Parse((string)myargs[0]), "1", "1");
            }
            else if (pytrnsamnt != 0)
            {
                Global.updateMsPyStatus(long.Parse((string)myargs[0]), "1", "0");
            }
            else if (Global.get_Total_MsPyDt(long.Parse((string)myargs[0])) > 0 && intfcCrdtAmnt == 0)
            {
                Global.updateMsPyStatus(long.Parse((string)myargs[0]), "1", "1");
            }
            worker.ReportProgress(100);
        }

        private void msPayActRnButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[22]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;

            if (this.msPyIDTextBox.Text == "" || this.msPyIDTextBox.Text == "-1000000")
            {
                Global.mnFrm.cmCde.showMsg("Please select a Mass Pay Run First!", 0);
                return;
            }
            if (!Global.mnFrm.cmCde.isTransPrmttd(
              Global.mnFrm.cmCde.get_DfltCashAcnt(Global.mnFrm.cmCde.Org_id),
              this.msPyGLDateTextBox.Text, 200))
            {
                return;
            }
            if (Global.hsMsPyBnRun(long.Parse(this.msPyIDTextBox.Text)) == true
   && Global.hsMsPyGoneToGL(long.Parse(this.msPyIDTextBox.Text)) == true)
            {
                Global.mnFrm.cmCde.showMsg("Cannot rerun a Mass Pay that has been fully run already!", 0);
                return;
            }
            if (this.trnsDateTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please enter a Mass Pay Run Date!", 0);
                return;
            }
            if (this.msPyGLDateTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please enter a Mass Pay Run GL Date!", 0);
                return;
            }
            int prsn = -1;
            int.TryParse(this.msPyPrsStIDTextBox.Text, out prsn);
            if (this.msPyPrsStIDTextBox.Text == "" || this.msPyPrsStIDTextBox.Text == "-1"
              || prsn <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Mass Pay Person Set!", 0);
                return;
            }
            if (this.msPyItmStIDTextBox.Text == "" || this.msPyItmStIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select a Mass Pay Item Set!", 0);
                return;
            }

            questionMassPayDiag nwDiag = new questionMassPayDiag();
            string itmAssgnDte = "";
            bool shdSkip = true;
            nwDiag.radioButton1.Checked = true;
            if (nwDiag.ShowDialog() == DialogResult.Cancel)
            {
                //Global.mnFrm.cmCde.showMsg("Please select a Mass Pay Item Set!", 0);
                return;
            }
            else
            {
                shdSkip = nwDiag.radioButton1.Checked;
                if (shdSkip == false)
                {
                    itmAssgnDte = nwDiag.vldStrtDteTextBox.Text;
                }
                else
                {
                    itmAssgnDte = this.trnsDateTextBox.Text;
                }
            }

            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to Run this Mass Pay?", 1)
            == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }

            this.msPayActRnButton.Enabled = false;
            this.rllbckMsPyRnButton.Enabled = false;
            this.sndMsPyToGLButton.Enabled = false;
            this.cancelRunButton.Enabled = true;
            this.attchedValsButton.Enabled = false;
            string dateStr = DateTime.ParseExact(
         Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
            Object[] args = {this.msPyIDTextBox.Text, this.trnsDateTextBox.Text,
        this.msPyPrsStIDTextBox.Text, this.msPyItmStIDTextBox.Text,
            Global.mnFrm.cmCde.Org_id.ToString(), dateStr, this.msPyGLDateTextBox.Text,
                      shdSkip,itmAssgnDte};
            this.backgroundWorker1.RunWorkerAsync(args);
        }

        private string runMassPay(int org_id, long prsn_id, string loc_id_no, long itm_id,
          string itm_name, string itm_uom, long mspy_id, string trns_date,
          string trns_typ, string itm_maj_typ, string itm_min_typ,
          long msg_id, string log_tbl, string dateStr, string glDate, bool shdSkip,
          string itmAssgnDte, string trnsDesc, ref decimal payItmAmnt)
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
            /*, trns_date*/
            long prsnItmRwID = Global.doesPrsnHvItmPrs(prsn_id,
               itm_id);
            if (prsnItmRwID <= 0
       && shdSkip == true)
            {
                return "Continue";
            }
            else if (prsnItmRwID <= 0 && shdSkip == false && itmAssgnDte != "")
            {
                long dfltVal = Global.getFirstItmValID(itm_id);
                if (dfltVal > 0)
                {
                    Global.createBnftsPrs(prsn_id,
              itm_id
                , dfltVal
                , "01-" + itmAssgnDte.Substring(3, 8), "31-Dec-4000");
                }
            }
            else if (Global.doesPrsnHvItm(prsn_id, itm_id, trns_date) == false)
            {
                Global.mnFrm.cmCde.updateLogMsg(msg_id,
                  "\r\nPerson:" +
                  loc_id_no + " does not have Item:" + itm_name + " as at " + trns_date, log_tbl, dateStr);
                return "Continue";
            }

            if (trns_typ == "")
            {
                Global.mnFrm.cmCde.updateLogMsg(msg_id,
                  "\r\nTransaction Type not Specified for Person:" +
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
            double pay_amount = 0;
            long prs_itm_val_id = Global.getPrsnItmVlID(prsn_id, itm_id, trns_date);
            int crncy_id = -1;
            string crncy_cde = itm_uom;
            if (itm_uom == "Money")
            {
                crncy_id = Global.mnFrm.cmCde.getOrgFuncCurID(org_id);
                crncy_cde = Global.mnFrm.cmCde.getPssblValNm(crncy_id);
            }
            string isRetroElmnt = Global.mnFrm.cmCde.getGnrlRecNm(
              "org.org_pay_items", "item_id", "is_retro_element", itm_id);
            string dteEarned = "";
            string valSQL = Global.mnFrm.cmCde.getItmValSQL(prs_itm_val_id);

            if (isRetroElmnt == "1")
            {
                DataSet retroDtSt = Global.getAtchdValPrsnAmnt(prsn_id, mspy_id, itm_id);
                for (int z = 0; z < retroDtSt.Tables[0].Rows.Count; z++)
                {
                    pay_amount = double.Parse(retroDtSt.Tables[0].Rows[z][0].ToString());
                    dteEarned = retroDtSt.Tables[0].Rows[z][1].ToString();
                    if (pay_amount == 0)
                    {
                        return "Continue";
                    }

                    //Check if a Balance Item will be negative if this trns is done    
                    double nwAmnt = this.willItmBlsBeNgtv(prsn_id, itm_id, pay_amount, trns_date);
                    if (nwAmnt < 0)
                    {
                        //    Global.mnFrm.cmCde.showSQLNoPermsn("\r\nTransaction will cause a Balance Item " +
                        //"to Have Negative Balance and hence cannot be allowed! Person:" +
                        //loc_id_no + " Item: " + itm_name + "Amount:" + nwAmnt + "/" + pay_amount + "/" + trns_date);
                        Global.mnFrm.cmCde.updateLogMsg(msg_id,
                    "\r\nTransaction will cause a Balance Item " +
                    "to Have Negative Balance and hence cannot be allowed! Person:" +
                    loc_id_no + " Item: " + itm_name + "Amount:" + nwAmnt + "/" + pay_amount + "/" + trns_date, log_tbl, dateStr);
                        return "Continue";
                    }

                    string pay_trns_desc = "";//"Payment of " + itm_name + " for " + loc_id_no;
                    if (itm_min_typ == "Earnings"
                      || itm_min_typ == "Employer Charges")
                    {
                        if (trnsDesc != "")
                        {
                            pay_trns_desc = trnsDesc;
                        }
                        else
                        {
                            pay_trns_desc = "Payment of " + itm_name + " for " + loc_id_no + " Source Date:" + dteEarned;
                        }
                    }
                    else if (itm_min_typ == "Bills/Charges"
                      || itm_min_typ == "Deductions")
                    {
                        if (trnsDesc != "")
                        {
                            pay_trns_desc = trnsDesc;
                        }
                        else
                        {
                            pay_trns_desc = "Payment of " + itm_name + " by " + loc_id_no + " Source Date:" + dteEarned;
                        }
                    }
                    else
                    {
                        if (trnsDesc != "")
                        {
                            pay_trns_desc = trnsDesc;
                        }
                        else
                        {
                            pay_trns_desc = "Running of Purely Informational Item " + itm_name + " for " + loc_id_no + " Source Date:" + dteEarned;
                        }
                    }

                    long tstPyTrnsID = -1;

                    if (tstPyTrnsID <= 0)
                    {
                        Global.createPaymntLine(prsn_id, itm_id, pay_amount,
                     trns_date, "Mass Pay Run",
                     trns_typ, mspy_id, pay_trns_desc, crncy_id, dateStr, "VALID",
                     -1, glDate, dteEarned);
                    }
                    else
                    {
                        Global.mnFrm.cmCde.updateLogMsg(msg_id,
                    "\r\nSame Payment has been made for this Person on the same Date already! Person:" +
                    loc_id_no + " Item: " + itm_name, log_tbl, dateStr);
                    }
                    //Update Balance Items
                    this.updtBlsItms(prsn_id, itm_id, pay_amount, trns_date, "Mass Pay Run", -1);

                    bool res = this.sendToGLInterfaceRetro(prsn_id, loc_id_no, itm_id, itm_name,
                      itm_uom, pay_amount, trns_date, pay_trns_desc, crncy_id, msg_id, log_tbl,
                      dateStr, "Mass Pay Run", glDate, -1, dteEarned);

                    if (res)
                    {
                        Global.mnFrm.cmCde.updateLogMsg(msg_id,
                    "\r\nSuccessfully processed Payment for Person:" +
                    loc_id_no + " Item: " + itm_name, log_tbl, dateStr);
                    }
                    else
                    {
                        Global.mnFrm.cmCde.updateLogMsg(msg_id,
                    "\r\nProcessing Payment Failed for Person:" +
                    loc_id_no + " Item: " + itm_name, log_tbl, dateStr);
                    }
                }
            }
            else if (payItmAmnt != 0 || itm_name == "Advance Payments Amount Applied")
            {
                pay_amount = (double)payItmAmnt;
                //payItmAmnt = 0;
            }
            else if (valSQL == "")
            {
                pay_amount = Global.getAtchdValPrsnAmnt(prsn_id, mspy_id, itm_id, ref dteEarned);
                if (pay_amount == 0)
                {
                    pay_amount = Global.mnFrm.cmCde.getItmValueAmnt(prs_itm_val_id);
                }
                payItmAmnt = (decimal)pay_amount;
            }
            else
            {
                pay_amount = Global.mnFrm.cmCde.exctItmValSQL(valSQL, prsn_id,
                  org_id, trns_date);
                payItmAmnt = (decimal)pay_amount;
            }

            if (isRetroElmnt != "1")
            {
                if (pay_amount == 0)
                {
                    return "Continue";
                }

                //Check if a Balance Item will be negative if this trns is done    
                double nwAmnt = this.willItmBlsBeNgtv(prsn_id, itm_id, pay_amount, trns_date);
                if (nwAmnt < 0)
                {
                    //    Global.mnFrm.cmCde.showSQLNoPermsn("\r\nTransaction will cause a Balance Item " +
                    //"to Have Negative Balance and hence cannot be allowed! Person:" +
                    //loc_id_no + " Item: " + itm_name + "Amount:" + nwAmnt + "/" + pay_amount + "/" + trns_date);
                    Global.mnFrm.cmCde.updateLogMsg(msg_id,
                "\r\nTransaction will cause a Balance Item " +
                "to Have Negative Balance and hence cannot be allowed! Person:" +
                loc_id_no + " Item: " + itm_name + "Amount:" + nwAmnt + "/" + pay_amount + "/" + trns_date, log_tbl, dateStr);
                    return "Continue";
                }

                if (Global.doesPymntDteViolateFreq(prsn_id, itm_id, trns_date) == true)
                {
                    Global.mnFrm.cmCde.updateLogMsg(msg_id, "\r\nThe Payment Date violates the " +
                "Item's Defined Pay Frequency! Person:" +
                loc_id_no + " Item: " + itm_name + " Payment Date:" + trns_date, log_tbl, dateStr);
                    return "Continue";
                }

                string pay_trns_desc = "";//"Payment of " + itm_name + " for " + loc_id_no;
                if (trnsDesc == "")
                {
                    if (itm_min_typ == "Earnings"
                      || itm_min_typ == "Employer Charges")
                    {
                        pay_trns_desc = "Payment of " + itm_name + " for " + loc_id_no;
                    }
                    else if (itm_min_typ == "Bills/Charges"
                      || itm_min_typ == "Deductions")
                    {
                        pay_trns_desc = "Payment of " + itm_name + " by " + loc_id_no;
                    }
                    else
                    {
                        pay_trns_desc = "Running of Purely Informational Item " + itm_name + " for " + loc_id_no;
                    }
                }
                else
                {
                    pay_trns_desc = trnsDesc;
                }
                long tstPyTrnsID = -1;
                tstPyTrnsID = Global.hsPrsnBnPaidItmMsPy(prsn_id, itm_id,
                         trns_date, pay_amount);
                if (tstPyTrnsID <= 0)
                {
                    Global.createPaymntLine(prsn_id, itm_id, pay_amount,
                 trns_date, "Mass Pay Run",
                 trns_typ, mspy_id, pay_trns_desc, crncy_id, dateStr, "VALID",
                 -1, glDate, dteEarned);
                }
                else
                {
                    Global.mnFrm.cmCde.updateLogMsg(msg_id,
                "\r\nSame Payment has been made for this Person on the same Date already! Person:" +
                loc_id_no + " Item: " + itm_name, log_tbl, dateStr);
                }
                //Update Balance Items
                this.updtBlsItms(prsn_id, itm_id, pay_amount, trns_date, "Mass Pay Run", -1);

                bool res = this.sendToGLInterface(prsn_id, loc_id_no, itm_id, itm_name,
                  itm_uom, pay_amount, trns_date, pay_trns_desc, crncy_id, msg_id, log_tbl,
                  dateStr, "Mass Pay Run", glDate, -1);
                if (res)
                {
                    Global.mnFrm.cmCde.updateLogMsg(msg_id,
                "\r\nSuccessfully processed Payment for Person:" +
                loc_id_no + " Item: " + itm_name, log_tbl, dateStr);
                }
                else
                {
                    Global.mnFrm.cmCde.updateLogMsg(msg_id,
                "\r\nProcessing Payment Failed for Person:" +
                loc_id_no + " Item: " + itm_name, log_tbl, dateStr);
                }
            }

            return "";
        }

        public string runRetroMassPay(int org_id, long prsn_id, string loc_id_no, int itm_id, int retroItmID,
         string itm_name, string itm_uom, long mspy_id, string trns_date_ernd, string cur_pay_dte,
         string trns_typ, string itm_maj_typ, string itm_min_typ,
         long msg_id, string log_tbl, string dateStr, string glDate, ref double pay_amount)
        {
            // check if item is not balance item
            //check if person has item actively
            //get and make sure trns type is not empty
            //Get trns date
            //Get amount to pay and make sure it is not zero
            //Form a payment description
            //Check if person hasn't been paid that item in this mass pay id
            //Check if pay transaction is valid
            pay_amount = 0;
            if (itm_maj_typ.ToUpper() == "Balance Item".ToUpper())
            {
                return "Continue";
            }
            if (Global.doesPrsnHvItm(prsn_id, itm_id) == false)
            {
                return "Continue";
            }
            if (trns_typ == "")
            {
                Global.mnFrm.cmCde.updateLogMsg(msg_id,
                  "\r\nTransaction Type not Specified for Person:" +
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
            //double pay_amount = 0;
            //int retroItmID = long.Parse(cmnCde.getGnrlRecNm("scm.scm_sales_invc_hdr", "invc_hdr_id", "customer_id", this.invcHdrID));

            //get Retro Item Val ID at this stage
            long prs_itm_val_id = Global.getPrsnItmVlID(prsn_id, retroItmID, trns_date_ernd);
            if (prs_itm_val_id <= 0)
            {
                prs_itm_val_id = Global.getFirstItmValID(retroItmID);
            }
            int crncy_id = -1;
            string crncy_cde = itm_uom;
            if (itm_uom == "Money")
            {
                crncy_id = Global.mnFrm.cmCde.getOrgFuncCurID(org_id);
                crncy_cde = Global.mnFrm.cmCde.getPssblValNm(crncy_id);
            }
            string valSQL = Global.mnFrm.cmCde.getItmValSQL(prs_itm_val_id);
            if (valSQL == "")
            {
                pay_amount = 0;// Global.getAtchdValPrsnAmnt(prsn_id, mspy_id, itm_id);
                if (pay_amount == 0)
                {
                    pay_amount = Global.mnFrm.cmCde.getItmValueAmnt(prs_itm_val_id);
                }
            }
            else
            {
                pay_amount = Global.mnFrm.cmCde.exctItmValSQL(valSQL, prsn_id,
                  org_id, trns_date_ernd);
            }


            if (pay_amount == 0)
            {
                return "Continue";
            }
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
            //Use the willItmBlsBeNgtvRetro Function
            double nwAmnt = this.willItmBlsBeNgtvRetro(prsn_id, itm_id, pay_amount, trns_date_ernd);
            if (nwAmnt < 0)
            {
                //    Global.mnFrm.cmCde.showSQLNoPermsn("\r\nTransaction will cause a Balance Item " +
                //"to Have Negative Balance and hence cannot be allowed! Person:" +
                //loc_id_no + " Item: " + itm_name + "Amount:" + nwAmnt + "/" + pay_amount + "/" + trns_date);
                Global.mnFrm.cmCde.updateLogMsg(msg_id,
            "\r\nTransaction will cause a Balance Item " +
            "to Have Negative Balance and hence cannot be allowed! Person:" +
            loc_id_no + " Item: " + itm_name + "Amount:" + nwAmnt + "/" + pay_amount + "/" + trns_date_ernd, log_tbl, dateStr);
                return "Continue";
            }

            //Use doesPymntDteViolateFreqRetro Function
            //  if (Global.doesPymntDteViolateFreq(prsn_id, itm_id, trns_date) == true)
            //  {
            //    Global.mnFrm.cmCde.updateLogMsg(msg_id, "\r\nThe Payment Date violates the " +
            //"Item's Defined Pay Frequency! Person:" +
            //loc_id_no + " Item: " + itm_name + " Payment Date:" + trns_date, log_tbl, dateStr);
            //    return "Continue";
            //  }

            string pay_trns_desc = "";//"Payment of " + itm_name + " for " + loc_id_no;
            if (itm_min_typ == "Earnings"
              || itm_min_typ == "Employer Charges")
            {
                pay_trns_desc = "Payment of " + itm_name + " for " + loc_id_no;
            }
            else if (itm_min_typ == "Bills/Charges"
              || itm_min_typ == "Deductions")
            {
                pay_trns_desc = "Payment of " + itm_name + " by " + loc_id_no;
            }
            else
            {
                pay_trns_desc = "Running of Purely Informational Item " + itm_name + " for " + loc_id_no;
            }
            //Use hsPrsnBnPaidItmMsPyRetro Function
            long tstPyTrnsID = Global.hsPrsnBnPaidItmMsPyRetro(prsn_id, itm_id,
              trns_date_ernd, pay_amount);
            if (tstPyTrnsID <= 0)
            {
                //Use createPaymntLineRetro
                Global.createPaymntLineRetro(prsn_id, itm_id, pay_amount,
             trns_date_ernd, "Mass Pay Run",
             trns_typ, mspy_id, pay_trns_desc, crncy_id, dateStr, "VALID", -1, glDate);
            }
            else
            {
                Global.mnFrm.cmCde.updateLogMsg(msg_id,
            "\r\nSame Payment has been made for this Person on the same Date already! Person:" +
            loc_id_no + " Item: " + itm_name, log_tbl, dateStr);
            }
            //Update Balance Items
            //Use updtBlsItmsRetro
            this.updtBlsItmsRetro(prsn_id, itm_id, pay_amount, trns_date_ernd, "Mass Pay Run", -1);

            bool res = true; /*this.sendToGLInterface(prsn_id, loc_id_no, itm_id, itm_name,
        itm_uom, pay_amount, trns_date, pay_trns_desc, crncy_id, msg_id, log_tbl,
        dateStr, "Mass Pay Run", glDate, -1);*/
            if (res)
            {
                Global.mnFrm.cmCde.updateLogMsg(msg_id,
            "\r\nSuccessfully processed Payment for Person:" +
            loc_id_no + " Item: " + itm_name, log_tbl, dateStr);
            }
            else
            {
                Global.mnFrm.cmCde.updateLogMsg(msg_id,
            "\r\nProcessing Payment Failed for Person:" +
            loc_id_no + " Item: " + itm_name, log_tbl, dateStr);
            }
            return "";
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
            Global.mnFrm.cmCde.updateLogMsg(msg_id,
        "\r\nReversing Transaction for Person:" +
        loc_id_no + " Item: " + itm_name, log_tbl, dateStr);

            if (itm_maj_typ.ToUpper() == "Balance Item".ToUpper())
            {
                return "Continue";
            }
            //if (Global.doesPrsnHvItm(prsn_id, itm_id, trns_date) == false)
            //{
            // return "Continue";
            //}
            if (trns_typ == "")
            {
                Global.mnFrm.cmCde.updateLogMsg(msg_id,
                  "\r\nTransaction Type not Specified for Person:" +
                  loc_id_no + " Item: " + itm_name, log_tbl, dateStr);
                return "Continue";
            }

            if (Global.getPymntRvrslTrnsID(orgnlPyTrnsID) > 0)
            {
                Global.mnFrm.cmCde.updateLogMsg(msg_id,
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
                crncy_cde = Global.mnFrm.cmCde.getPssblValNm(crncy_id);
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


            if (!this.isMsPayTrnsValid(itm_uom, itm_maj_typ,
              itm_min_typ, itm_id, glDate, pay_amount))
            {
                return "Stop";
            }

            //Check if a Balance Item will be negative if this trns is done    
            /*double nwAmnt = this.willItmBlsBeNgtv(prsn_id, itm_id, pay_amount, trns_date);
            if (nwAmnt < 0)
            {
             Global.mnFrm.cmCde.updateLogMsg(msg_id,
         "\r\nTransaction will cause a Balance Item " +
         "to Have Negative Balance and hence cannot be allowed! Person:" +
         loc_id_no + " Item: " + itm_name, log_tbl, dateStr);
             return "Continue";
            }*/

            long tstPyTrnsID = Global.hsPrsnBnPaidItmMsPy(prsn_id, itm_id,
              trns_date, pay_amount);
            if (tstPyTrnsID <= 0)
            {
                Global.createPaymntLine(prsn_id, itm_id, pay_amount,
             trns_date, "Mass Pay Run Reversal",
             trns_typ, mspy_id, pay_trns_desc, crncy_id, dateStr, "VALID", orgnlPyTrnsID, glDate, "");

                Global.updateTrnsVldtyStatus(orgnlPyTrnsID, "VOID");
            }
            else
            {
                Global.mnFrm.cmCde.updateLogMsg(msg_id,
            "\r\nSame Payment has been made for this Person on the same Date already! Person:" +
            loc_id_no + " Item: " + itm_name, log_tbl, dateStr);
            }

            //Update Balance Items
            this.updtBlsItms(prsn_id, itm_id, pay_amount, trns_date, "Mass Pay Run Reversal", orgnlPyTrnsID);

            Global.deletePymntGLInfcLns(orgnlPyTrnsID);

            long nwpaytrnsid = Global.getPaymntTrnsID(
      prsn_id, itm_id,
      pay_amount, trns_date, orgnlPyTrnsID);


            bool res = this.rvrsImprtdPymntIntrfcTrns(orgnlPyTrnsID, nwpaytrnsid);
            /*this.sendToGLInterface(prsn_id, loc_id_no, itm_id, itm_name,
              itm_uom, pay_amount, trns_date, pay_trns_desc, crncy_id, msg_id, log_tbl,
              dateStr, "Mass Pay Run Reversal", glDate);*/
            if (res)
            {
                Global.mnFrm.cmCde.updateLogMsg(msg_id,
            "\r\nSuccessfully processed Payment Reversal for Person:" +
            loc_id_no + " Item: " + itm_name, log_tbl, dateStr);
            }
            else
            {
                Global.mnFrm.cmCde.updateLogMsg(msg_id,
            "\r\nProcessing Payment Reversal Failed for Person:" +
            loc_id_no + " Item: " + itm_name, log_tbl, dateStr);
            }
            return "";
        }

        private bool rvrsImprtdPymntIntrfcTrns(long orgnlPyTrnsID, long nwPyTrnsID)
        {
            //try
            //{
            DataSet dtst = Global.getPymntGLInfcLns(orgnlPyTrnsID);
            string dateStr = Global.mnFrm.cmCde.getFrmtdDB_Date_time();
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

                Global.createPymntGLIntFcLn(accntID,
            "(Reversal)" + dtst.Tables[0].Rows[i][2].ToString(),
            -1 * dbtamount, trnsdte,
            crncy_id, -1 * crdtamount,
            -1 * netamnt, nwPyTrnsID, dateStr);

            }
            return true;
            //}
            //catch (Exception ex)
            //{
            //  Global.mnFrm.cmCde.showMsg(ex.InnerException.ToString(), 0);
            //  return false;
            //}
        }

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

        public double willItmBlsBeNgtv(long prsn_id, long itm_id,
          double pay_amount, string trns_date)
        {
            DataSet dtst = Global.getAllItmFeeds1(itm_id);
            double nwAmnt = 0;
            for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
            {
                if (Global.doesPrsnHvItmPrs(prsn_id,
                  long.Parse(dtst.Tables[0].Rows[a][0].ToString())) <= 0)
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

        private void cancelRunButton_Click(object sender, EventArgs e)
        {
            if (this.backgroundWorker1.IsBusy == true)
            {
                this.backgroundWorker1.CancelAsync();
            }
            if (this.backgroundWorker2.IsBusy == true)
            {
                this.backgroundWorker2.CancelAsync();
            }
        }

        private void vwLogMsgButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[7]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.msPyNmTextBox.Text.Contains("Reversal"))
            {
                Global.mnFrm.cmCde.showLogMsg(
                  Global.mnFrm.cmCde.getLogMsgID("pay.pay_mass_pay_run_msgs",
                  "Mass Pay Run Reversal", long.Parse(this.msPyIDTextBox.Text)), "pay.pay_mass_pay_run_msgs");
            }
            else
            {
                Global.mnFrm.cmCde.showLogMsg(
            Global.mnFrm.cmCde.getLogMsgID("pay.pay_mass_pay_run_msgs",
            "Mass Pay Run", long.Parse(this.msPyIDTextBox.Text)), "pay.pay_mass_pay_run_msgs");
            }
        }

        private void rllbckMsPyRnButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[23]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            this.backgroundWorker2.WorkerReportsProgress = true;
            this.backgroundWorker2.WorkerSupportsCancellation = true;

            if (this.msPyIDTextBox.Text == "" || this.msPyIDTextBox.Text == "-1000000")
            {
                Global.mnFrm.cmCde.showMsg("Please select a Mass Pay Run First!", 0);
                return;
            }
            if (this.trnsDateTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please enter a Mass Pay Run Date!", 0);
                return;
            }
            if (this.msPyGLDateTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please enter a Mass Pay Run GL Date!", 0);
                return;
            }
            if (Global.get_MsPyInvoiceID(long.Parse(this.msPyIDTextBox.Text)) > 0)
            {
                Global.mnFrm.cmCde.showMsg("Cannot Roll Back a Pay Run that was Generated from Sales!\r\nCancel the Source Sales Document Instead!", 0);
                return;
            }
            if (this.msPyItmStIDTextBox.Text == "" || this.msPyItmStIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select a Mass Pay Item Set!", 0);
                return;
            }
            //if (Global.hsMsPyBnRun(long.Parse(this.msPyIDTextBox.Text)) == false
            //  || Global.hsMsPyGoneToGL(long.Parse(this.msPyIDTextBox.Text)) == false)
            //{
            //  Global.mnFrm.cmCde.showMsg("Cannot rollback an incompletely run mass pay!" +
            //    "\r\nPlease complete the mass pay run first!", 0);
            //  return;
            //}

            if (!Global.mnFrm.cmCde.isTransPrmttd(
                              Global.mnFrm.cmCde.get_DfltCashAcnt(Global.mnFrm.cmCde.Org_id),
                              this.msPyGLDateTextBox.Text, 200))
            {
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to Roll Back this Mass Pay Run?", 1)
            == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            this.rllbckMsPyRnButton.Enabled = false;
            this.rmvPayItmMenuItem.Enabled = false;
            System.Windows.Forms.Application.DoEvents();
            bool isAnyRnng = true;
            int witcntr = 0;
            do
            {
                witcntr++;
                isAnyRnng = Global.isThereANActvActnPrcss("8", "10 second");//Payments Import Process
                System.Windows.Forms.Application.DoEvents();
            }
            while (isAnyRnng == true);

            this.rllbckMsPyRnButton.Enabled = true;
            this.rmvPayItmMenuItem.Enabled = true;
            System.Windows.Forms.Application.DoEvents();

            long nwmspyid = Global.mnFrm.cmCde.getMsPyID(
              this.msPyNmTextBox.Text + " (Reversal)",
              Global.mnFrm.cmCde.Org_id);

            if (nwmspyid <= 0)
            {
                Global.createMsPy(Global.mnFrm.cmCde.Org_id,
                  this.msPyNmTextBox.Text + " (Reversal)",
                  "(Reversal) " + this.msPyDescTextBox.Text,
                  this.trnsDateTextBox.Text, int.Parse(this.msPyPrsStIDTextBox.Text)
                , int.Parse(this.msPyItmStIDTextBox.Text), this.msPyGLDateTextBox.Text);
            }

            System.Windows.Forms.Application.DoEvents();
            this.msPayActRnButton.Enabled = false;
            this.rllbckMsPyRnButton.Enabled = false;
            this.sndMsPyToGLButton.Enabled = false;
            this.cancelRunButton.Enabled = true;
            this.attchedValsButton.Enabled = false;

            System.Windows.Forms.Application.DoEvents();
            string dateStr = DateTime.ParseExact(
         Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");

            nwmspyid = Global.mnFrm.cmCde.getMsPyID(
                    this.msPyNmTextBox.Text + " (Reversal)",
                    Global.mnFrm.cmCde.Org_id);

            Object[] args = {this.msPyIDTextBox.Text,
            Global.mnFrm.cmCde.Org_id.ToString(), dateStr,
            nwmspyid.ToString(), this.msPyGLDateTextBox.Text};
            if (nwmspyid > 0)
            {
                this.backgroundWorker2.RunWorkerAsync(args);
            }
            else
            {
                Global.mnFrm.cmCde.showMsg("Failed to create Mass Pay run Reversal Batch!\r\nPlease try again Later!", 4);
                return;
            }

        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
            }
            else if (e.Error != null)
            {
                Global.mnFrm.cmCde.showMsg("Error: " + e.Error.Message, 4);
            }
            else
            {
                Global.mnFrm.cmCde.showMsg("Operation Completed Successfully!", 3);
            }
            this.msPayActRnButton.Enabled = true;
            this.rllbckMsPyRnButton.Enabled = true;
            this.sndMsPyToGLButton.Enabled = true;
            this.cancelRunButton.Enabled = false;
            this.attchedValsButton.Enabled = true;
            this.loadMsPyPanel();
            this.delMsPyButton.PerformClick();
        }

        private void backgroundWorker2_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;
            this.progressLabel.Text = e.ProgressPercentage.ToString() + "%";
        }

        private void backgroundWorker2_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            Object[] myargs = (Object[])e.Argument;

            //Get dataset for Payments to reverse
            //loop through such payments reversing them
            DataSet payDtSt = Global.getMsPyToRllBck(long.Parse((string)myargs[0]));
            int payCnt = payDtSt.Tables[0].Rows.Count;
            string dateStr = (string)myargs[2];
            string gldateStr = (string)myargs[4];
            long msg_id = Global.mnFrm.cmCde.getLogMsgID("pay.pay_mass_pay_run_msgs", "Mass Pay Run Reversal", long.Parse((string)myargs[3]));
            if (msg_id <= 0)
            {
                Global.mnFrm.cmCde.createLogMsg(dateStr + " .... Mass Pay Run Reversal is about to Start...",
            "pay.pay_mass_pay_run_msgs", "Mass Pay Run Reversal", long.Parse((string)myargs[3]), dateStr);
            }
            msg_id = Global.mnFrm.cmCde.getLogMsgID("pay.pay_mass_pay_run_msgs", "Mass Pay Run Reversal", long.Parse((string)myargs[3]));
            string retmsg = "";
            //Loop through all payments to reverse them
            for (int i = 0; i < payCnt; i++)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    this.msPayActRnButton.Enabled = true;
                    this.rllbckMsPyRnButton.Enabled = true;
                    this.sndMsPyToGLButton.Enabled = true;
                    this.cancelRunButton.Enabled = false;
                    this.attchedValsButton.Enabled = true;
                    this.loadMsPyPanel();
                    break;
                }
                else
                {
                    if (i == 0)
                    {
                        if (!this.isMsPayTrnsValid(
                         payDtSt.Tables[0].Rows[i][13].ToString(),
                         payDtSt.Tables[0].Rows[i][14].ToString(),
                         payDtSt.Tables[0].Rows[i][15].ToString(),
                         int.Parse(payDtSt.Tables[0].Rows[i][2].ToString()), gldateStr, 1000))
                        {
                            this.backgroundWorker2.CancelAsync();
                            worker.ReportProgress(Convert.ToInt32((i + 1) * (99.0 / payCnt)));
                            break;
                        }
                    }

                    retmsg = this.rvrsMassPay(int.Parse((string)myargs[1]),
                       long.Parse(payDtSt.Tables[0].Rows[i][1].ToString()),
                       payDtSt.Tables[0].Rows[i][10].ToString(),
                       int.Parse(payDtSt.Tables[0].Rows[i][2].ToString()),
                       payDtSt.Tables[0].Rows[i][12].ToString(),
                       payDtSt.Tables[0].Rows[i][13].ToString(),
                       long.Parse((string)myargs[3]),
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
                        this.backgroundWorker2.CancelAsync();
                        worker.ReportProgress(Convert.ToInt32((i + 1) * (99.0 / payCnt)));
                        break;
                    }
                    //System.Threading.Thread.Sleep(500);
                    worker.ReportProgress(Convert.ToInt32((i + 1) * (99.0 / payCnt)));
                }
            }
            //Do some summation checks before updating the Status
            //Function to check if sum of debits is equal sum of credits to sum of amnts in all these pay trns
            //if correct the set gone to gl to '1' else '0'
            double pytrnsamnt = Global.getMsPyAmntSum(long.Parse((string)myargs[3]));
            double intfcDbtAmnt = Global.getMsPyIntfcDbtSum(long.Parse((string)myargs[3]));
            double intfcCrdtAmnt = Global.getMsPyIntfcCrdtSum(long.Parse((string)myargs[3]));
            if (pytrnsamnt == intfcCrdtAmnt
              && pytrnsamnt == intfcDbtAmnt && pytrnsamnt != 0)
            {
                Global.updateMsPyStatus(long.Parse((string)myargs[3]), "1", "1");
            }
            else if (pytrnsamnt != 0)
            {
                Global.updateMsPyStatus(long.Parse((string)myargs[3]), "1", "0");
            }
            else if (Global.get_Total_MsPyDt(long.Parse((string)myargs[3])) > 0 && intfcCrdtAmnt == 0)
            {
                Global.updateMsPyStatus(long.Parse((string)myargs[3]), "1", "1");
            }
            string gnrUpdate = "UPDATE pay.pay_balsitm_bals SET bals_amount=0 WHERE bals_amount<0";
            Global.mnFrm.cmCde.updateDataNoParams(gnrUpdate);

            worker.ReportProgress(100);
        }

        private void searchForMsPyTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.goMsPyButton_Click(this.goMsPyButton, ex);
            }
        }

        private void positionMsPyDtTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                this.MsPyDtPnlNavButtons(this.movePreviousMsPyDtButton, ex);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
            {
                this.MsPyDtPnlNavButtons(this.moveNextMsPyDtButton, ex);
            }
        }

        private void positionMsPyTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                this.MsPyPnlNavButtons(this.movePreviousMsPyButton, ex);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
            {
                this.MsPyPnlNavButtons(this.moveNextMsPyButton, ex);
            }
        }

        private void addMsPyMenuItem_Click(object sender, EventArgs e)
        {
            this.addMsPyButton_Click(this.addMsPyButton, e);
        }

        private void editMsPyMenuItem_Click(object sender, EventArgs e)
        {
            this.editMsPyButton_Click(this.editMsPyButton, e);
        }

        private void delMsPyMenuItem_Click(object sender, EventArgs e)
        {
            this.delMsPyButton_Click(this.delMsPyButton, e);
        }

        private void exptMsPyMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.msPyListView);
        }

        private void rfrshMsPyMenuItem_Click(object sender, EventArgs e)
        {
            this.goMsPyButton_Click(this.goMsPyButton, e);
        }

        private void vwSQLMsPyMenuItem_Click(object sender, EventArgs e)
        {
            this.vwSQLMsPyButton_Click(this.vwSQLMsPyButton, e);
        }

        private void rcHstryMsPyMenuItem_Click(object sender, EventArgs e)
        {
            this.recHstryMsPyButton_Click(this.recHstryMsPyButton, e);
        }

        private void delMsPyButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[19]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.msPyIDTextBox.Text == "" || this.msPyIDTextBox.Text == "-1000000")
            {
                Global.mnFrm.cmCde.showMsg("Please select the Mass Pay to DELETE!", 0);
                return;
            }
            long orgnlMsPyID = -1;
            long rvrslmspyid = -1;
            if (this.msPyNmTextBox.Text.Contains(" (Reversal)"))
            {
                rvrslmspyid = long.Parse(this.msPyIDTextBox.Text);
                orgnlMsPyID = Global.mnFrm.cmCde.getMsPyID(
             this.msPyNmTextBox.Text.Replace(" (Reversal)", ""),
             Global.mnFrm.cmCde.Org_id);
            }
            else
            {
                orgnlMsPyID = long.Parse(this.msPyIDTextBox.Text);
                rvrslmspyid = Global.mnFrm.cmCde.getMsPyID(
             this.msPyNmTextBox.Text + " (Reversal)",
             Global.mnFrm.cmCde.Org_id);
            }
            if (Global.isMspyInUse(orgnlMsPyID) == true)
            {
                Global.mnFrm.cmCde.showMsg("This Mass Pay has been SENT to GL or has not been REVERSED hence cannot be DELETED!", 0);
                return;
            }
            if (rvrslmspyid <= 0 && orgnlMsPyID > 0 && this.hsBnRnMsPyCheckBox.Checked)
            {
                Global.mnFrm.cmCde.showMsg("This Mass Pay has not been REVERSED hence cannot be DELETED!", 0);
                return;
            }

            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Mass Pay?" +
         "\r\nThis action cannot be undone!", 1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }

            Global.mnFrm.cmCde.deleteGnrlRecs(rvrslmspyid,
      "Mass Pay Name = " + this.msPyNmTextBox.Text + " (Reversal)", "pay.pay_itm_trnsctns", "mass_pay_id");

            Global.mnFrm.cmCde.deleteGnrlRecs(rvrslmspyid,
           "Mass Pay Name = " + this.msPyNmTextBox.Text + " (Reversal)", "pay.pay_mass_pay_run_hdr", "mass_pay_id");

            Global.mnFrm.cmCde.deleteGnrlRecs(orgnlMsPyID,
      "Mass Pay Name = " + this.msPyNmTextBox.Text, "pay.pay_itm_trnsctns", "mass_pay_id");

            Global.mnFrm.cmCde.deleteGnrlRecs(orgnlMsPyID,
           "Mass Pay Name = " + this.msPyNmTextBox.Text, "pay.pay_mass_pay_run_hdr", "mass_pay_id");


            this.loadMsPyPanel();
        }

        private void vwSQLMsPyButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(this.mspy_SQL, 8);
        }

        private void recHstryMsPyButton_Click(object sender, EventArgs e)
        {
            if (this.msPyListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(
              Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(
              this.msPyListView.SelectedItems[0].SubItems[7].Text),
              "pay.pay_mass_pay_run_hdr", "mass_pay_id"), 7);
        }

        private void exptMsPyDtMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.msPyDtListView);
        }

        private void rfrshMsPyDtMenuItem_Click(object sender, EventArgs e)
        {
            this.loadMsPyDetPanel();
        }

        private void vwSQLMsPyDtMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(this.mspydt_SQL, 8);
        }

        private void rcHstryMsPyDtMenuItem_Click(object sender, EventArgs e)
        {
            if (this.msPyDtListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(
              Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(
              this.msPyDtListView.SelectedItems[0].SubItems[9].Text),
              "pay.pay_itm_trnsctns", "pay_trns_id"), 7);
        }

        private void hsBnRnMsPyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shdObeyMsPyEvts() == false
            || beenToCheckBx == true)
            {
                beenToCheckBx = false;
                return;
            }
            beenToCheckBx = true;
            if (this.addMsPy == false && this.editMsPy == false)
            {
                this.hsBnRnMsPyCheckBox.Checked = !this.hsBnRnMsPyCheckBox.Checked;
            }
        }

        private void hsGoneToGLCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shdObeyMsPyEvts() == false
            || beenToCheckBx == true)
            {
                beenToCheckBx = false;
                return;
            }
            beenToCheckBx = true;
            if (this.addMsPy == false && this.editMsPy == false)
            {
                this.hsGoneToGLCheckBox.Checked = !this.hsGoneToGLCheckBox.Checked;
            }
        }

        private void prntPySlipMenuItem_Click(object sender, EventArgs e)
        {
            //Associate PrintPreviewDialog with PrintDocument.
            this.printPreviewDialog1 = new PrintPreviewDialog();

            this.printPreviewDialog1.Document = printDocument2;
            this.printPreviewDialog1.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.pageNo = 1;
            this.prntIdx = 0;
            this.printPreviewDialog1.PrintPreviewControl.AutoZoom = true;
            //this.printPreviewDialog1.PrintPreviewControl.Zoom = 1;
            this.printDocument2.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", 850, 1100);
            //this.printPreviewDialog1.FindForm().Height = Global.mnFrm.Height;
            //this.printPreviewDialog1.FindForm().Width = Global.mnFrm.Height;
            //this.printPreviewDialog1.FindForm().StartPosition = FormStartPosition.Manual;
            //this.printPreviewDialog1.FindForm().Location = new Point(800, 200);
            this.printPreviewDialog1.FindForm().WindowState = FormWindowState.Maximized;
            this.printPreviewDialog1.ShowDialog();

        }

        private void prntAdvcMenuItem_Click(object sender, EventArgs e)
        {
            ////Associate PrintPreviewDialog with PrintDocument.
            //this.printPreviewDialog1 = new PrintPreviewDialog();
            //this.printPreviewDialog1.Document = printDocument3;
            //this.printPreviewDialog1.FormBorderStyle = FormBorderStyle.Fixed3D;
            //this.pageNo = 1;
            //this.prntIdx = 0;
            //this.printPreviewDialog1.PrintPreviewControl.AutoZoom = true;
            ////this.printPreviewDialog1.PrintPreviewControl.Zoom = 1;
            //this.printDocument2.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", 850, 1100);
            //this.printPreviewDialog1.FindForm().WindowState = FormWindowState.Maximized;
            ////this.printPreviewDialog1.FindForm().StartPosition = FormStartPosition.Manual;
            ////this.printPreviewDialog1.FindForm().Location = new Point(800, 200);
            //this.printPreviewDialog1.ShowDialog();
        }

        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Pen aPen = new Pen(Brushes.Black, 1);
            Graphics g = e.Graphics;
            e.PageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", 850, 1100);
            //e.PageSettings.
            Font font1 = new Font("Verdana", 12.25f, FontStyle.Underline | FontStyle.Bold);
            Font font11 = new Font("Verdana", 12.25f, FontStyle.Bold);
            Font font2 = new Font("Verdana", 12.25f, FontStyle.Bold);
            Font font4 = new Font("Verdana", 12.0f, FontStyle.Bold);
            Font font41 = new Font("Verdana", 12.0f);
            Font font3 = new Font("Courier New", 12.0f);
            Font font31 = new Font("Courier New", 12.5f, FontStyle.Bold);
            Font font5 = new Font("Times New Roman", 6.0f, FontStyle.Italic);

            float font1Hght = font1.Height;
            float font2Hght = font2.Height;
            float font3Hght = font3.Height;
            float font4Hght = font4.Height;
            float font5Hght = font5.Height;

            float pageWidth = e.PageSettings.PaperSize.Width - 40;//e.PageSettings.PrintableArea.Width;
            float pageHeight = e.PageSettings.PaperSize.Height - 40;// e.PageSettings.PrintableArea.Height;
                                                                    //Global.mnFrm.cmCde.showMsg(pageWidth.ToString(), 0);
            float startX = 40;
            float startY = 40;
            float offsetY = 0;
            //StringBuilder strPrnt = new StringBuilder();
            //strPrnt.AppendLine("Received From");
            string[] nwLn;

            if (this.pageNo == 1)
            { }//Org Logo
               //RectangleF srcRect = new Rectangle(0, 0, this.BackgroundImage.Width,
               //BackgroundImage.Height);
               //RectangleF destRect = new Rectangle(0, 0, nWidth, nHeight);
               //Rectangle destRect = new Rectangle(0, 0, nWidth, nHeight);
            Image img = Global.mnFrm.cmCde.getDBImageFile(Global.mnFrm.cmCde.Org_id.ToString() + ".png", 0);
            float picWdth = 100.00F;
            float picHght = (float)(picWdth / img.Width) * (float)img.Height;

            g.DrawImage(img, startX, startY + offsetY, picWdth, picHght);
            //g.DrawImage(this.LargerImage, destRect, srcRect, GraphicsUnit.Pixel);

            //Org Name
            nwLn = Global.mnFrm.cmCde.breakTxtDown(
              Global.mnFrm.cmCde.getOrgName(Global.mnFrm.cmCde.Org_id),
              pageWidth + 85, font2, g);
            for (int i = 0; i < nwLn.Length; i++)
            {
                g.DrawString(nwLn[i]
                , font2, Brushes.Black, startX + picWdth, startY + offsetY);
                offsetY += font2Hght;
            }

            //Pstal Address
            g.DrawString(Global.mnFrm.cmCde.getOrgPstlAddrs(Global.mnFrm.cmCde.Org_id).Trim(),
            font2, Brushes.Black, startX + picWdth, startY + offsetY);
            //offsetY += font2Hght;

            float ght = g.MeasureString(
              Global.mnFrm.cmCde.getOrgPstlAddrs(Global.mnFrm.cmCde.Org_id).Trim(), font2).Height;
            offsetY = offsetY + (int)ght;
            //Contacts Nos
            nwLn = Global.mnFrm.cmCde.breakTxtDown(
         Global.mnFrm.cmCde.getOrgContactNos(Global.mnFrm.cmCde.Org_id),
         pageWidth - startX - 100, font2, g);
            for (int i = 0; i < nwLn.Length; i++)
            {
                g.DrawString(nwLn[i]
                , font2, Brushes.Black, startX + picWdth, startY + offsetY);
                offsetY += font2Hght;
            }
            //Email Address
            nwLn = Global.mnFrm.cmCde.breakTxtDown(
         Global.mnFrm.cmCde.getOrgEmailAddrs(Global.mnFrm.cmCde.Org_id),
         pageWidth, font2, g);
            for (int i = 0; i < nwLn.Length; i++)
            {
                g.DrawString(nwLn[i]
                , font2, Brushes.Black, startX + picWdth, startY + offsetY);
                offsetY += font2Hght;
            }
            offsetY += font2Hght;
            if (offsetY < (int)picHght)
            {
                offsetY = font2Hght + (int)picHght;
            }
            DataSet dtst = Global.get_One_MsPyDetSmmry(long.Parse(this.msPyIDTextBox.Text), -1);
            //Title
            nwLn = Global.mnFrm.cmCde.breakTxtDown(
            "ITEM RUN RESULTS SLIP", pageWidth, font1, g);
            for (int i = 0; i < nwLn.Length; i++)
            {
                g.DrawString(nwLn[i]
                , font1, Brushes.Black, startX, startY + offsetY);
                offsetY += font1Hght;
            }
            offsetY += font1Hght;
            //Loop Through Records
            bool hsErn = false;
            bool hsDeduct = false;
            float orgoffsetY = 0;

            string[] itmTypes = new string[7];
            double[] itmTypeTtls = new double[7];
            double netPay = 0;
            int itmTypIdx = 0;
            string lastItmTyp = "";
            float endX = 0;
            endX = startX + (float)(pageWidth * 0.7);
            for (int a = this.prntIdx; a < dtst.Tables[0].Rows.Count; a++)
            {
                if (this.pageNo == 1)
                {
                    g.DrawLine(aPen, startX, startY + offsetY, startX + pageWidth - 40,
                      startY + offsetY);
                    offsetY += font1Hght;
                    g.DrawString("Name(ID): ", font4, Brushes.Black, startX, startY + offsetY);
                    ght = g.MeasureString("Name(ID): ", font4).Width;
                    //Full Name
                    nwLn = Global.mnFrm.cmCde.breakTxtDown(
                    dtst.Tables[0].Rows[a][11].ToString() +
                      " (" + dtst.Tables[0].Rows[a][10].ToString() + ")", pageWidth - ght, font41, g);
                    orgoffsetY = offsetY;
                    for (int i = 0; i < nwLn.Length; i++)
                    {
                        g.DrawString(nwLn[i]
                        , font41, Brushes.Black, startX + ght, startY + offsetY);
                        offsetY += font4Hght;
                    }

                    g.DrawString("Date: ", font4, Brushes.Black, startX, startY + offsetY);
                    ght = g.MeasureString("Date: ", font4).Width;
                    //Date
                    nwLn = Global.mnFrm.cmCde.breakTxtDown(
                    dtst.Tables[0].Rows[a][4].ToString()
                    , pageWidth - ght, font41, g);
                    for (int i = 0; i < nwLn.Length; i++)
                    {
                        g.DrawString(nwLn[i]
                        , font41, Brushes.Black, startX + ght, startY + offsetY);
                        offsetY += font4Hght;
                    }

                    nwLn = Global.mnFrm.cmCde.breakTxtDown(
                    dtst.Tables[0].Rows[a][15].ToString(), pageWidth - ght, font41, g);
                    if (nwLn.Length > 0)
                    {
                        g.DrawString("Job: ", font4, Brushes.Black, startX, startY + offsetY);
                        ght = g.MeasureString("Job: ", font4).Width;
                        //Full Name
                        for (int i = 0; i < nwLn.Length; i++)
                        {
                            g.DrawString(nwLn[i]
                            , font41, Brushes.Black, startX + ght, startY + offsetY);
                            offsetY += font4Hght;
                        }
                        //offsetY += font4Hght;
                    }
                    nwLn = Global.mnFrm.cmCde.breakTxtDown(
                    dtst.Tables[0].Rows[a][14].ToString(), pageWidth - ght, font41, g);
                    if (nwLn.Length > 0)
                    {
                        g.DrawString("Grade: ", font4, Brushes.Black, startX, startY + offsetY);
                        ght = g.MeasureString("Grade: ", font4).Width;
                        for (int i = 0; i < nwLn.Length; i++)
                        {
                            g.DrawString(nwLn[i]
                            , font41, Brushes.Black, startX + ght, startY + offsetY);
                            offsetY += font4Hght;
                        }
                    }
                    nwLn = Global.mnFrm.cmCde.breakTxtDown(
                    dtst.Tables[0].Rows[a][16].ToString(), pageWidth - ght, font41, g);
                    if (nwLn.Length > 0)
                    {
                        g.DrawString("Position: ", font4, Brushes.Black, startX, startY + offsetY);
                        ght = g.MeasureString("Position: ", font4).Width;

                        for (int i = 0; i < nwLn.Length; i++)
                        {
                            g.DrawString(nwLn[i]
                            , font41, Brushes.Black, startX + ght, startY + offsetY);
                            offsetY += font4Hght;
                        }
                    }

                    nwLn = Global.mnFrm.cmCde.breakTxtDown(
                    dtst.Tables[0].Rows[a][17].ToString(), pageWidth - ght, font41, g);
                    if (nwLn.Length > 0)
                    {
                        g.DrawString("SSNIT No.: ", font4, Brushes.Black, startX, startY + offsetY);
                        ght = g.MeasureString("SSNIT No.: ", font4).Width;
                        //Full Name
                        for (int i = 0; i < nwLn.Length; i++)
                        {
                            g.DrawString(nwLn[i]
                            , font41, Brushes.Black, startX + ght, startY + offsetY);
                            offsetY += font4Hght;
                        }
                    }
                    nwLn = Global.mnFrm.cmCde.breakTxtDown(
                    dtst.Tables[0].Rows[a][18].ToString(), pageWidth - ght, font41, g);
                    if (nwLn.Length > 0)
                    {
                        g.DrawString("Bank (Branch): ", font4, Brushes.Black, startX, startY + offsetY);
                        ght = g.MeasureString("Bank (Branch): ", font4).Width;
                        //Full Name
                        for (int i = 0; i < nwLn.Length; i++)
                        {
                            g.DrawString(nwLn[i]
                            , font41, Brushes.Black, startX + ght, startY + offsetY);
                            offsetY += font4Hght;
                        }
                        //offsetY += font4Hght;
                    }
                    nwLn = Global.mnFrm.cmCde.breakTxtDown(
                    dtst.Tables[0].Rows[a][19].ToString(), pageWidth - ght, font41, g);
                    if (nwLn.Length > 0)
                    {
                        g.DrawString("Account: ", font4, Brushes.Black, startX, startY + offsetY);
                        ght = g.MeasureString("Account: ", font4).Width;
                        //Full Name
                        for (int i = 0; i < nwLn.Length; i++)
                        {
                            g.DrawString(nwLn[i]
                            , font41, Brushes.Black, startX + ght, startY + offsetY);
                            offsetY += font4Hght;
                        }
                    }
                    offsetY += font1Hght;
                    g.DrawLine(aPen, startX, startY + offsetY, startX + pageWidth - 40,
               startY + offsetY);
                    offsetY += font2Hght;
                    //offsetY += font2Hght;
                    g.DrawString("Item     ", font1, Brushes.Black, startX, startY + offsetY);
                    ght = g.MeasureString("Item     ", font1).Width;
                    ght = g.MeasureString(("Amount (" + Global.mnFrm.cmCde.getPssblValNm(
               Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id)) +
               ")"), font1).Width;
                    g.DrawString(("Amount (" + Global.mnFrm.cmCde.getPssblValNm(
               Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id)) +
               ")"), font1, Brushes.Black, endX - ght, startY + offsetY);
                    //offsetY += font2Hght;
                    //     g.DrawLine(aPen, startX, startY + offsetY, startX + pageWidth,
                    //startY + offsetY);            offsetY += font1Hght;

                    offsetY += font1Hght;
                }
                //Item Type
                if (dtst.Tables[0].Rows[a][13].ToString() != lastItmTyp)
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
                        nwLn = Global.mnFrm.cmCde.breakTxtDown(
                  "Total " + txt
                  , (float)(pageWidth * 0.5), font31, g);
                        for (int i = 0; i < nwLn.Length; i++)
                        {
                            g.DrawString(nwLn[i]
                            , font31, Brushes.Black, startX, startY + offsetY);
                            offsetY += font3Hght;
                        }
                        nwLn = Global.mnFrm.cmCde.breakTxtDown(
                  itmTypeTtls[itmTypIdx - 1].ToString("#,#0.00")
                  , (float)(pageWidth * 0.5), font31, g);
                        ght = g.MeasureString(" = " + itmTypeTtls[itmTypIdx - 1].ToString("#,#0.00"), font31).Width;
                        for (int i = 0; i < nwLn.Length; i++)
                        {
                            g.DrawString(" = " + nwLn[i]
                            , font31, Brushes.Black, endX - ght, startY + orgoffsetY);
                            offsetY += font3Hght;
                        }
                        //itmTypIdx++;
                        //itmTypeTtls[itmTypIdx] = 0;
                    }
                    nwLn = Global.mnFrm.cmCde.breakTxtDown(
               dtst.Tables[0].Rows[a][13].ToString()
               , (float)(pageWidth * 0.5), font11, g);
                    for (int i = 0; i < nwLn.Length; i++)
                    {
                        g.DrawString(nwLn[i]
                        , font11, Brushes.Black, startX, startY + offsetY);
                        offsetY += font1Hght;
                    }
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
                orgoffsetY = offsetY;
                nwLn = Global.mnFrm.cmCde.breakTxtDown(
                  dtst.Tables[0].Rows[a][12].ToString()
                  , (float)(pageWidth * 0.5), font3, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    g.DrawString(nwLn[i]
                    , font3, Brushes.Black, startX, startY + offsetY);
                    offsetY += font3Hght;
                }
                //offsetY = orgoffsetY;
                //Item
                nwLn = Global.mnFrm.cmCde.breakTxtDown(
                  double.Parse(dtst.Tables[0].Rows[a][3].ToString()).ToString("#,#0.00")
                  , (float)(pageWidth * 0.5), font3, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    ght = g.MeasureString(nwLn[i], font3).Width;
                    g.DrawString(nwLn[i]
                    , font3, Brushes.Black, endX - ght, startY + orgoffsetY);
                    orgoffsetY += font3Hght;
                }
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
                            nwLn = Global.mnFrm.cmCde.breakTxtDown(
                     "Total " + txt
                     , (float)(pageWidth * 0.5), font31, g);
                            for (int i = 0; i < nwLn.Length; i++)
                            {
                                g.DrawString(nwLn[i]
                                , font31, Brushes.Black, startX, startY + offsetY);
                                offsetY += font3Hght;
                            }
                            nwLn = Global.mnFrm.cmCde.breakTxtDown(
                     itmTypeTtls[itmTypIdx - 1].ToString("#,#0.00")
                     , (float)(pageWidth * 0.5), font31, g);
                            ght = g.MeasureString(" = " + itmTypeTtls[itmTypIdx - 1].ToString("#,#0.00"), font31).Width;
                            for (int i = 0; i < nwLn.Length; i++)
                            {
                                g.DrawString(" = " + nwLn[i]
                                , font31, Brushes.Black, endX - ght, startY + orgoffsetY);
                                offsetY += font3Hght;
                            }
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
                                nwLn = Global.mnFrm.cmCde.breakTxtDown(
                        ttlStr
                        , (float)(pageWidth * 0.5), font31, g);
                                for (int i = 0; i < nwLn.Length; i++)
                                {
                                    g.DrawString(nwLn[i]
                                    , font31, Brushes.Black, startX, startY + offsetY);
                                    offsetY += font3Hght;
                                }
                                nwLn = Global.mnFrm.cmCde.breakTxtDown(
                        netPay.ToString("#,#0.00")
                        , (float)(pageWidth * 0.5), font31, g);
                                ght = g.MeasureString(" = " + netPay.ToString("#,#0.00"), font31).Width;
                                for (int i = 0; i < nwLn.Length; i++)
                                {
                                    g.DrawString(" = " + nwLn[i]
                                    , font31, Brushes.Black, endX - ght, startY + orgoffsetY);
                                    offsetY += font3Hght;
                                }
                            }
                        }
                        //this.prntIdx = a;
                        //Slogan: 
                        offsetY += font3Hght;
                        offsetY += font3Hght;
                        g.DrawLine(aPen, startX, startY + offsetY, startX + pageWidth - 40,
                  startY + offsetY);
                        offsetY += font3Hght;
                        nwLn = Global.mnFrm.cmCde.breakTxtDown(
                          Global.mnFrm.cmCde.getOrgSlogan(Global.mnFrm.cmCde.Org_id),
                  pageWidth - ght, font5, g);
                        for (int i = 0; i < nwLn.Length; i++)
                        {
                            g.DrawString(nwLn[i]
                            , font5, Brushes.Black, startX, startY + offsetY);
                            offsetY += font5Hght;
                        }
                        offsetY += font5Hght;
                        nwLn = Global.mnFrm.cmCde.breakTxtDown(
                         "Software Developed by Rhomicom Systems Technologies Ltd.",
                  pageWidth + 40, font5, g);
                        for (int i = 0; i < nwLn.Length; i++)
                        {
                            g.DrawString(nwLn[i]
                            , font5, Brushes.Black, startX, startY + offsetY);
                            offsetY += font5Hght;
                        }
                        nwLn = Global.mnFrm.cmCde.breakTxtDown(
                  "Website:www.rhomicomgh.com",
                  pageWidth + 40, font5, g);
                        for (int i = 0; i < nwLn.Length; i++)
                        {
                            g.DrawString(nwLn[i]
                            , font5, Brushes.Black, startX, startY + offsetY);
                            offsetY += font5Hght;
                        }
                        e.HasMorePages = true;
                        offsetY = 0;
                        this.pageNo = 1;
                        return;
                    }
                }
                //}
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
                nwLn = Global.mnFrm.cmCde.breakTxtDown(
            "Total " + txt
            , (float)(pageWidth * 0.5), font31, g);
                for (int i = 0; i < nwLn.Length; i++)
                {
                    g.DrawString(nwLn[i]
                    , font31, Brushes.Black, startX, startY + offsetY);
                    offsetY += font3Hght;
                }
                nwLn = Global.mnFrm.cmCde.breakTxtDown(
            itmTypeTtls[itmTypIdx - 1].ToString("#,#0.00")
            , (float)(pageWidth * 0.5), font31, g);
                ght = g.MeasureString(" = " + itmTypeTtls[itmTypIdx - 1].ToString("#,#0.00"), font31).Width;
                for (int i = 0; i < nwLn.Length; i++)
                {
                    g.DrawString(" = " + nwLn[i]
                    , font31, Brushes.Black, endX - ght, startY + orgoffsetY);
                    offsetY += font3Hght;
                }
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
                    nwLn = Global.mnFrm.cmCde.breakTxtDown(
               ttlStr
               , (float)(pageWidth * 0.5), font31, g);
                    for (int i = 0; i < nwLn.Length; i++)
                    {
                        g.DrawString(nwLn[i]
                        , font31, Brushes.Black, startX, startY + offsetY);
                        offsetY += font3Hght;
                    }
                    nwLn = Global.mnFrm.cmCde.breakTxtDown(
               netPay.ToString("#,#0.00")
               , (float)(pageWidth * 0.5), font31, g);
                    ght = g.MeasureString(" = " + netPay.ToString("#,#0.00"), font31).Width;
                    for (int i = 0; i < nwLn.Length; i++)
                    {
                        g.DrawString(" = " + nwLn[i]
                        , font31, Brushes.Black, endX - ght, startY + orgoffsetY);
                        offsetY += font3Hght;
                    }
                }
            }
            //Slogan: 
            offsetY += font3Hght;
            offsetY += font3Hght;
            g.DrawLine(aPen, startX, startY + offsetY, startX + pageWidth - 40,
         startY + offsetY);
            offsetY += font3Hght;
            nwLn = Global.mnFrm.cmCde.breakTxtDown(
              Global.mnFrm.cmCde.getOrgSlogan(Global.mnFrm.cmCde.Org_id),
         pageWidth - ght, font5, g);
            for (int i = 0; i < nwLn.Length; i++)
            {
                g.DrawString(nwLn[i]
                , font5, Brushes.Black, startX, startY + offsetY);
                offsetY += font5Hght;
            }
            offsetY += font5Hght;
            nwLn = Global.mnFrm.cmCde.breakTxtDown(
             "Software Developed by Rhomicom Systems Technologies Ltd.",
         pageWidth + 40, font5, g);
            for (int i = 0; i < nwLn.Length; i++)
            {
                g.DrawString(nwLn[i]
                , font5, Brushes.Black, startX, startY + offsetY);
                offsetY += font5Hght;
            }
            nwLn = Global.mnFrm.cmCde.breakTxtDown(
         "Website:www.rhomicomgh.com",
         pageWidth + 40, font5, g);
            for (int i = 0; i < nwLn.Length; i++)
            {
                g.DrawString(nwLn[i]
                , font5, Brushes.Black, startX, startY + offsetY);
                offsetY += font5Hght;
            }

        }

        private void printDocument3_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Pen aPen = new Pen(Brushes.Black, 1);
            Graphics g = e.Graphics;
            e.PageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", 850, 1100);
            //e.PageSettings.
            Font font1 = new Font("Courier New", 12.25f, FontStyle.Underline | FontStyle.Bold);
            Font font2 = new Font("Times New Roman", 12.25f, FontStyle.Bold);
            Font font4 = new Font("Times New Roman", 12.25f, FontStyle.Bold);
            Font font3 = new Font("Courier New", 12.0f);
            Font font5 = new Font("Times New Roman", 6.0f, FontStyle.Italic);

            int font1Hght = font1.Height;
            int font2Hght = font2.Height;
            int font3Hght = font3.Height;
            int font4Hght = font4.Height;
            int font5Hght = font5.Height;

            float pageWidth = e.PageSettings.PaperSize.Width - 40;//e.PageSettings.PrintableArea.Width;
            float pageHeight = e.PageSettings.PaperSize.Height - 40;// e.PageSettings.PrintableArea.Height;
                                                                    //Global.mnFrm.cmCde.showMsg(pageWidth.ToString(), 0);
            int startX = 10;
            int startY = 20;
            int offsetY = 0;
            //StringBuilder strPrnt = new StringBuilder();
            //strPrnt.AppendLine("Received From");
            string[] nwLn;

            if (this.pageNo == 1)
            { }//Org Logo
               //RectangleF srcRect = new Rectangle(0, 0, this.BackgroundImage.Width,
               //BackgroundImage.Height);
               //RectangleF destRect = new Rectangle(0, 0, nWidth, nHeight);
               //Rectangle destRect = new Rectangle(0, 0, nWidth, nHeight);
            Image img = Global.mnFrm.cmCde.getDBImageFile(Global.mnFrm.cmCde.Org_id.ToString() + ".png", 0);
            float picWdth = 100.00F;
            float picHght = (float)(picWdth / img.Width) * (float)img.Height;

            g.DrawImage(img, startX, startY + offsetY, picWdth, picHght);
            //g.DrawImage(this.LargerImage, destRect, srcRect, GraphicsUnit.Pixel);

            //Org Name
            nwLn = Global.mnFrm.cmCde.breakTxtDown(
              Global.mnFrm.cmCde.getOrgName(Global.mnFrm.cmCde.Org_id),
              pageWidth + 85, font2, g);
            for (int i = 0; i < nwLn.Length; i++)
            {
                g.DrawString(nwLn[i]
                , font2, Brushes.Black, startX + picWdth, startY + offsetY);
                offsetY += font2Hght;
            }

            //Pstal Address
            g.DrawString(Global.mnFrm.cmCde.getOrgPstlAddrs(Global.mnFrm.cmCde.Org_id).Trim(),
            font2, Brushes.Black, startX + picWdth, startY + offsetY);
            //offsetY += font2Hght;

            float ght = g.MeasureString(
              Global.mnFrm.cmCde.getOrgPstlAddrs(Global.mnFrm.cmCde.Org_id).Trim(), font2).Height;
            offsetY = offsetY + (int)ght;
            //Contacts Nos
            nwLn = Global.mnFrm.cmCde.breakTxtDown(
         Global.mnFrm.cmCde.getOrgContactNos(Global.mnFrm.cmCde.Org_id),
         pageWidth, font2, g);
            for (int i = 0; i < nwLn.Length; i++)
            {
                g.DrawString(nwLn[i]
                , font2, Brushes.Black, startX + picWdth, startY + offsetY);
                offsetY += font2Hght;
            }
            //Email Address
            nwLn = Global.mnFrm.cmCde.breakTxtDown(
         Global.mnFrm.cmCde.getOrgEmailAddrs(Global.mnFrm.cmCde.Org_id),
         pageWidth, font2, g);
            for (int i = 0; i < nwLn.Length; i++)
            {
                g.DrawString(nwLn[i]
                , font2, Brushes.Black, startX + picWdth, startY + offsetY);
                offsetY += font2Hght;
            }
            offsetY += font2Hght;
            if (offsetY < (int)picHght)
            {
                offsetY = font2Hght + (int)picHght;
            }
            DataSet dtst = Global.get_One_MsPyDet(long.Parse(this.msPyIDTextBox.Text), -1);
            //Title
            nwLn = Global.mnFrm.cmCde.breakTxtDown(
            "BANK ADVICE SLIP FOR PAYMENT RUN", pageWidth, font2, g);
            for (int i = 0; i < nwLn.Length; i++)
            {
                g.DrawString(nwLn[i]
                , font1, Brushes.Black, startX, startY + offsetY);
                offsetY += font1Hght;
            }
            offsetY += font1Hght;
            //Loop Through all Records

            //Slogan: 
            offsetY += font3Hght;
            offsetY += font3Hght;
            g.DrawLine(aPen, startX, startY + offsetY, startX + pageWidth,
         startY + offsetY);
            offsetY += font3Hght;
            nwLn = Global.mnFrm.cmCde.breakTxtDown(
              Global.mnFrm.cmCde.getOrgSlogan(Global.mnFrm.cmCde.Org_id),
         pageWidth - ght, font5, g);
            for (int i = 0; i < nwLn.Length; i++)
            {
                g.DrawString(nwLn[i]
                , font5, Brushes.Black, startX, startY + offsetY);
                offsetY += font5Hght;
            }
            offsetY += font5Hght;
            nwLn = Global.mnFrm.cmCde.breakTxtDown(
             "Software Developed by Rhomicom Systems Technologies Ltd.",
         pageWidth + 40, font5, g);
            for (int i = 0; i < nwLn.Length; i++)
            {
                g.DrawString(nwLn[i]
                , font5, Brushes.Black, startX, startY + offsetY);
                offsetY += font5Hght;
            }
            nwLn = Global.mnFrm.cmCde.breakTxtDown(
         "Website:www.rhomicomgh.com",
         pageWidth + 40, font5, g);
            for (int i = 0; i < nwLn.Length; i++)
            {
                g.DrawString(nwLn[i]
                , font5, Brushes.Black, startX, startY + offsetY);
                offsetY += font5Hght;
            }
        }

        private void quickPayMenuItem_Click(object sender, EventArgs e)
        {
            this.runQckPyButton.PerformClick();
        }

        private void msPyListView_ItemSelectionChanged(object sender, System.Windows.Forms.ListViewItemSelectionChangedEventArgs e)
        {
            if (this.shdObeyMsPyEvts() == false)
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
        #region "TRANSACTIONS SEARCH..."
        private void loadTrnsPanel()
        {
            this.obeytrnsEvnts = false;
            if (this.searchInTrnsComboBox.SelectedIndex < 0)
            {
                this.searchInTrnsComboBox.SelectedIndex = 1;
            }
            int dsply = 0;
            if (this.dsplySizeTrnsComboBox.Text == ""
              || int.TryParse(this.dsplySizeTrnsComboBox.Text, out dsply) == false)
            {
                this.dsplySizeTrnsComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            if (this.searchForTrnsTextBox.Text.Contains("%") == false)
            {
                this.searchForTrnsTextBox.Text = "%" + this.searchForTrnsTextBox.Text.Replace(" ", "%") + "%";
            }
            if (this.searchForTrnsTextBox.Text == "%%")
            {
                this.searchForTrnsTextBox.Text = "%";
            }
            this.is_last_trns = false;
            this.totl_trns = Global.mnFrm.cmCde.Big_Val;
            this.getTrnsPnlData();
            this.obeytrnsEvnts = true;
        }

        private void getTrnsPnlData()
        {
            this.updtTrnsTotals();
            this.populateTrnsGridVw();
            this.updtTrnsNavLabels();
        }

        private void updtTrnsTotals()
        {
            Global.mnFrm.cmCde.navFuncts.FindNavigationIndices(
              int.Parse(this.dsplySizeTrnsComboBox.Text),
            this.totl_trns);

            if (this.cur_trns_idx >= Global.mnFrm.cmCde.navFuncts.totalGroups)
            {
                this.cur_trns_idx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            if (this.cur_trns_idx < 0)
            {
                this.cur_trns_idx = 0;
            }
            Global.mnFrm.cmCde.navFuncts.currentNavigationIndex = this.cur_trns_idx;
        }

        private void updtTrnsNavLabels()
        {
            this.moveFirstTrnsButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveFirstBtnStatus();
            this.movePreviousTrnsButton.Enabled = Global.mnFrm.cmCde.navFuncts.movePrevBtnStatus();
            this.moveNextTrnsButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveNextBtnStatus();
            this.moveLastTrnsButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveLastBtnStatus();
            this.positionTrnsTextBox.Text = Global.mnFrm.cmCde.navFuncts.displayedRecordsNumbers();
            if (this.is_last_trns == true ||
              this.totl_trns != Global.mnFrm.cmCde.Big_Val)
            {
                this.totalRecsTrnsLabel.Text = Global.mnFrm.cmCde.navFuncts.totalRecordsLabel();
            }
            else
            {
                this.totalRecsTrnsLabel.Text = "of Total";
            }
        }

        private void populateTrnsGridVw()
        {
            this.obeytrnsEvnts = false;
            DataSet dtst;

            dtst = Global.get_Pay_Trns(this.searchForTrnsTextBox.Text,
            this.searchInTrnsComboBox.Text, this.cur_trns_idx,
            int.Parse(this.dsplySizeTrnsComboBox.Text), Global.mnFrm.cmCde.Org_id,
            this.vldStrtDteTextBox.Text, this.vldEndDteTextBox.Text);
            this.trnsSearchListView.Items.Clear();
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.last_trns_num = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
                string uom = "Number";
                if (dtst.Tables[0].Rows[i][9].ToString() != "-1")
                {
                    uom = Global.mnFrm.cmCde.getPssblValNm(int.Parse(dtst.Tables[0].Rows[i][9].ToString()));
                }
                ListViewItem nwItem = new ListViewItem(new string[] {
    (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][10].ToString(),
                dtst.Tables[0].Rows[i][11].ToString(),
    dtst.Tables[0].Rows[i][12].ToString(),
    double.Parse(dtst.Tables[0].Rows[i][3].ToString()).ToString("#,##0.00"),
    uom,dtst.Tables[0].Rows[i][4].ToString(),dtst.Tables[0].Rows[i][6].ToString(),
    dtst.Tables[0].Rows[i][7].ToString(),
    dtst.Tables[0].Rows[i][5].ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),
    dtst.Tables[0].Rows[i][2].ToString(),
    dtst.Tables[0].Rows[i][0].ToString(),
    dtst.Tables[0].Rows[i][13].ToString()});
                nwItem.UseItemStyleForSubItems = false;
                if (dtst.Tables[0].Rows[i][13].ToString() == "VALID")
                {
                    nwItem.SubItems[13].BackColor = Color.Lime;
                }
                else
                {
                    nwItem.SubItems[13].BackColor = Color.Red;
                }
                this.trnsSearchListView.Items.Add(nwItem);
            }
            /*
          Global.get_GLBatch_Nm(long.Parse(dtst.Tables[0].Rows[i][8].ToString())),*/
            this.correctTrnsNavLbls(dtst);
            this.obeytrnsEvnts = true;
        }

        private void correctTrnsNavLbls(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.cur_trns_idx == 0 && totlRecs == 0)
            {
                this.is_last_trns = true;
                this.totl_trns = 0;
                this.last_trns_num = 0;
                this.cur_trns_idx = 0;
                this.updtTrnsTotals();
                this.updtTrnsNavLabels();
            }
            else if (this.totl_trns == Global.mnFrm.cmCde.Big_Val
           && totlRecs < long.Parse(this.dsplySizeTrnsComboBox.Text))
            {
                this.totl_trns = this.last_trns_num;
                if (totlRecs == 0)
                {
                    this.cur_trns_idx -= 1;
                    this.updtTrnsTotals();
                    this.populateTrnsGridVw();
                }
                else
                {
                    this.updtTrnsTotals();
                }
            }
        }

        private void TrnsPnlNavButtons(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripButton sentObj =
              (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecsTrnsLabel.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.cur_trns_idx = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.cur_trns_idx -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.cur_trns_idx += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.totl_trns = Global.get_Total_Trns(
            this.searchForTrnsTextBox.Text, this.searchInTrnsComboBox.Text,
              Global.mnFrm.cmCde.Org_id,
            this.vldStrtDteTextBox.Text, this.vldEndDteTextBox.Text);
                this.is_last_trns = true;
                this.updtTrnsTotals();
                this.cur_trns_idx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            this.getTrnsPnlData();
        }

        private void dte1Button_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.selectDate(ref this.vldStrtDteTextBox);
        }

        private void dte2Button_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.selectDate(ref this.vldEndDteTextBox);
        }

        private void goTrnsButton_Click(object sender, EventArgs e)
        {
            this.loadTrnsPanel();
        }

        private void exptPySrchMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.trnsSearchListView);
        }

        private void positionTrnsTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                this.TrnsPnlNavButtons(this.movePreviousTrnsButton, ex);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
            {
                this.TrnsPnlNavButtons(this.moveNextTrnsButton, ex);
            }
        }

        private void searchForTrnsTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.trnsSearchListView.Focus();
                this.goTrnsButton_Click(this.goTrnsButton, ex);
            }
        }

        private void vwSQLTrnsButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(this.trnsDet_SQL1, 8);
        }

        private void recHstryTrnsButton_Click(object sender, EventArgs e)
        {
            if (this.trnsSearchListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(
              Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(
              this.trnsSearchListView.SelectedItems[0].SubItems[12].Text),
              "pay.pay_itm_trnsctns", "pay_trns_id"), 7);
        }

        private void rfrshPySrchMenuItem_Click(object sender, EventArgs e)
        {
            this.goTrnsButton_Click(this.goTrnsButton, e);
        }

        private void vwSQLPySrchMenuItem_Click(object sender, EventArgs e)
        {
            this.vwSQLTrnsButton_Click(this.vwSQLTrnsButton, e);
        }

        private void rcHstryPySrchMenuItem_Click(object sender, EventArgs e)
        {
            this.recHstryTrnsButton_Click(this.recHstryTrnsButton, e);
        }
        #endregion
        #region "GL INTERFACE TABLE..."
        private void loadInfcPanel()
        {
            this.waitLabel.Visible = false;
            System.Windows.Forms.Application.DoEvents();
            this.obeyInfcEvnts = false;
            if (this.searchInInfcComboBox.SelectedIndex < 0)
            {
                this.searchInInfcComboBox.SelectedIndex = 1;
            }
            int dsply = 0;
            if (this.dsplySizeInfcComboBox.Text == ""
              || int.TryParse(this.dsplySizeInfcComboBox.Text, out dsply) == false)
            {
                this.dsplySizeInfcComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            if (this.searchForInfcTextBox.Text.Contains("%") == false)
            {
                this.searchForInfcTextBox.Text = "%" + this.searchForInfcTextBox.Text.Replace(" ", "%") + "%";
            }
            if (this.searchForInfcTextBox.Text == "%%")
            {
                this.searchForInfcTextBox.Text = "%";
            }
            this.is_last_Infc = false;
            this.totl_Infc = Global.mnFrm.cmCde.Big_Val;
            this.getInfcPnlData();

            double dfrnce = 0;
            Global.isGLIntrfcBlcdOrg(Global.mnFrm.cmCde.Org_id, ref dfrnce);
            this.imblncTextBox.Text = dfrnce.ToString("#,##0.00");
            if (dfrnce != 0)
            {
                this.imblncTextBox.BackColor = Color.Red;
            }
            else
            {
                this.imblncTextBox.BackColor = Color.Lime;
            }

            this.obeyInfcEvnts = true;
        }

        private void getInfcPnlData()
        {
            this.updtInfcTotals();
            this.populateInfcGridVw();
            this.updtInfcNavLabels();
        }

        private void updtInfcTotals()
        {
            Global.mnFrm.cmCde.navFuncts.FindNavigationIndices(
              int.Parse(this.dsplySizeInfcComboBox.Text),
            this.totl_Infc);

            if (this.cur_Infc_idx >= Global.mnFrm.cmCde.navFuncts.totalGroups)
            {
                this.cur_Infc_idx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            if (this.cur_Infc_idx < 0)
            {
                this.cur_Infc_idx = 0;
            }
            Global.mnFrm.cmCde.navFuncts.currentNavigationIndex = this.cur_Infc_idx;
        }

        private void updtInfcNavLabels()
        {
            this.moveFirstInfcButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveFirstBtnStatus();
            this.movePreviousInfcButton.Enabled = Global.mnFrm.cmCde.navFuncts.movePrevBtnStatus();
            this.moveNextInfcButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveNextBtnStatus();
            this.moveLastInfcButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveLastBtnStatus();
            this.positionInfcTextBox.Text = Global.mnFrm.cmCde.navFuncts.displayedRecordsNumbers();
            if (this.is_last_Infc == true ||
              this.totl_Infc != Global.mnFrm.cmCde.Big_Val)
            {
                this.totalRecsInfcLabel.Text = Global.mnFrm.cmCde.navFuncts.totalRecordsLabel();
            }
            else
            {
                this.totalRecsInfcLabel.Text = "of Total";
            }
        }

        private void populateInfcGridVw()
        {
            this.obeyInfcEvnts = false;
            DataSet dtst;

            dtst = Global.get_Infc_Trns(this.searchForInfcTextBox.Text,
            this.searchInInfcComboBox.Text, this.cur_Infc_idx,
            int.Parse(this.dsplySizeInfcComboBox.Text), Global.mnFrm.cmCde.Org_id,
            this.infcDte1TextBox.Text, this.infcDte2TextBox.Text,
            this.glInfcCheckBox.Checked, this.imbalnceCheckBox.Checked,
            this.userTrnsCheckBox.Checked, this.numericUpDown1.Value, this.numericUpDown2.Value);
            this.glInfcListView.Items.Clear();

            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.last_Infc_num = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
                ListViewItem nwItem = new ListViewItem(new string[] {
            (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
            dtst.Tables[0].Rows[i][1].ToString(),
            dtst.Tables[0].Rows[i][2].ToString(),
            dtst.Tables[0].Rows[i][3].ToString(),
            double.Parse(dtst.Tables[0].Rows[i][5].ToString()).ToString("#,##0.00"),
            double.Parse(dtst.Tables[0].Rows[i][6].ToString()).ToString("#,##0.00"),
            Global.mnFrm.cmCde.getPssblValNm(int.Parse(dtst.Tables[0].Rows[i][12].ToString())),
            dtst.Tables[0].Rows[i][4].ToString(),
            dtst.Tables[0].Rows[i][8].ToString(),
            dtst.Tables[0].Rows[i][10].ToString(),
            dtst.Tables[0].Rows[i][0].ToString(),
            dtst.Tables[0].Rows[i][7].ToString(),
            dtst.Tables[0].Rows[i][11].ToString(),
            dtst.Tables[0].Rows[i][9].ToString(),
            dtst.Tables[0].Rows[i][13].ToString()});
                this.glInfcListView.Items.Add(nwItem);
            }
            /*
          Global.get_GLBatch_Nm(long.Parse(dtst.Tables[0].Rows[i][8].ToString())),*/
            this.correctInfcNavLbls(dtst);
            this.obeyInfcEvnts = true;
        }

        private void correctInfcNavLbls(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.cur_Infc_idx == 0 && totlRecs == 0)
            {
                this.is_last_Infc = true;
                this.totl_Infc = 0;
                this.last_Infc_num = 0;
                this.cur_Infc_idx = 0;
                this.updtInfcTotals();
                this.updtInfcNavLabels();
            }
            else if (this.totl_Infc == Global.mnFrm.cmCde.Big_Val
           && totlRecs < long.Parse(this.dsplySizeInfcComboBox.Text))
            {
                this.totl_Infc = this.last_Infc_num;
                if (totlRecs == 0)
                {
                    this.cur_Infc_idx -= 1;
                    this.updtInfcTotals();
                    this.populateInfcGridVw();
                }
                else
                {
                    this.updtInfcTotals();
                }
            }
        }

        private void InfcPnlNavButtons(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripButton sentObj =
              (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecsInfcLabel.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.cur_Infc_idx = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.cur_Infc_idx -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.cur_Infc_idx += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.totl_Infc = Global.get_Total_Infc(
            this.searchForInfcTextBox.Text, this.searchInInfcComboBox.Text,
              Global.mnFrm.cmCde.Org_id,
            this.infcDte1TextBox.Text, this.infcDte2TextBox.Text,
              this.glInfcCheckBox.Checked, this.imbalnceCheckBox.Checked,
              this.userTrnsCheckBox.Checked, this.numericUpDown1.Value, this.numericUpDown2.Value);
                this.is_last_Infc = true;
                this.updtInfcTotals();
                this.cur_Infc_idx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            this.getInfcPnlData();
        }

        private void sendAllToGLButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[21]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                  " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to Send all Outstanding\r\n Transactions in the Interface Table to Actual GL?", 1)
            == DialogResult.No)
            {
                Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            this.sendAllToGLButton.Enabled = false;
            System.Windows.Forms.Application.DoEvents();
            bool rs = this.sendToGL();
            if (rs)
            {
                Global.mnFrm.cmCde.showMsg("All Outstanding Transactions Successfully Sent to Actual GL!", 3);
            }
            this.sendAllToGLButton.Enabled = true;
            System.Windows.Forms.Application.DoEvents();

            this.loadInfcPanel();
        }

        private void sendMnlPyToGLMenuItem_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[24]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                  " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.pstPayListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Payment First!", 0);
                return;
            }
            if (this.pstPayListView.SelectedItems[0].SubItems[4].Text == "Balance Amount")
            {
                Global.mnFrm.cmCde.showMsg("Cannot run Payment for a Balance Item!", 0);
                return;
            }

            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to Send the Selected" +
              "Payment(s)' \r\n Transactions in the Interface Table to Actual GL?", 1)
            == DialogResult.No)
            {
                Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            string dateStr = DateTime.ParseExact(
         Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
            for (int a = 0; a < this.pstPayListView.SelectedItems.Count; a++)
            {
                if (this.pstPayListView.SelectedItems[a].SubItems[3].Text == "Balance Amount")
                {
                    Global.mnFrm.cmCde.showMsg("Cannot run Payment for a Balance Item!", 0);
                    return;
                }

                this.updtBlsItms(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text),
            long.Parse(this.itmListViewPymnt.SelectedItems[0].SubItems[4].Text),
            double.Parse(this.pstPayListView.SelectedItems[a].SubItems[1].Text),
            this.pstPayListView.SelectedItems[a].SubItems[3].Text,
            Global.getPymntTyp(long.Parse(this.pstPayListView.SelectedItems[a].SubItems[5].Text)),
            long.Parse(this.pstPayListView.SelectedItems[a].SubItems[11].Text));

                this.sendToGLInterfaceMnl(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text),
            long.Parse(this.itmListViewPymnt.SelectedItems[0].SubItems[4].Text),
            this.pstPayListView.SelectedItems[a].SubItems[2].Text,
            double.Parse(this.pstPayListView.SelectedItems[a].SubItems[1].Text),
            this.pstPayListView.SelectedItems[a].SubItems[3].Text,
            this.pstPayListView.SelectedItems[a].SubItems[6].Text,
            int.Parse(this.pstPayListView.SelectedItems[a].SubItems[7].Text), dateStr,
            Global.getPymntTyp(long.Parse(this.pstPayListView.SelectedItems[a].SubItems[5].Text))
            , this.glDateTextBox.Text,
            long.Parse(this.pstPayListView.SelectedItems[a].SubItems[11].Text));

                bool rs = this.sendToGL(long.Parse(this.pstPayListView.SelectedItems[a].SubItems[5].Text));
                if (rs)
                {
                    Global.mnFrm.cmCde.showMsg("Successfully Sent Payment to Actual GL!", 3);
                }
            }
            this.loadPstPayPanel();
        }

        private void sndMsPyToGLButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[20]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                  " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.msPyIDTextBox.Text == "" || this.msPyIDTextBox.Text == "-1000000")
            {
                Global.mnFrm.cmCde.showMsg("Please select a Mass Pay Run First!", 0);
                return;
            }
            if (this.hsBnRnMsPyCheckBox.Checked == false)
            {
                Global.mnFrm.cmCde.showMsg("Please Run this Mass Pay First!", 0);
                return;
            }

            if (this.hsGoneToGLCheckBox.Checked == false)
            {
                Global.mnFrm.cmCde.showMsg("Please Run this Mass Pay to the End First!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to Send this Mass Pay Run's \r\n Transactions in the Interface Table to Actual GL?", 1)
         == DialogResult.No)
            {
                Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            bool rs = this.sendMsPyToGL(long.Parse(this.msPyIDTextBox.Text));
            if (rs)
            {
                Global.mnFrm.cmCde.showMsg("Mass Pay Successfully Sent to Actual GL!", 3);
            }
            this.loadMsPyPanel();
        }

        private void infcDte1Button_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.selectDate(ref this.infcDte1TextBox);
        }

        private void infcDte2Button_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.selectDate(ref this.infcDte2TextBox);
        }

        private void goInfcButton_Click(object sender, EventArgs e)
        {
            this.loadInfcPanel();
        }

        private void searchForInfcTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.glInfcListView.Focus();
                this.goInfcButton_Click(this.goInfcButton, ex);
            }
        }

        private void positionInfcTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                this.InfcPnlNavButtons(this.movePreviousInfcButton, ex);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
            {
                this.InfcPnlNavButtons(this.moveNextInfcButton, ex);
            }
        }

        private void exptGlIntfcMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.glInfcListView);
        }

        private void rfrshGlIntFcMenuItem_Click(object sender, EventArgs e)
        {
            this.goInfcButton_Click(this.goInfcButton, e);
        }

        private void vwSQLIntFcMenuItem_Click(object sender, EventArgs e)
        {
            this.vwSQLInfcButton_Click(this.vwSQLInfcButton, e);
        }

        private void rcHstryGlIntfcMenuItem_Click(object sender, EventArgs e)
        {
            this.recHstryInfcButton_Click(this.recHstryInfcButton, e);
        }

        private void vwSQLInfcButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(this.vwInfcSQLStmnt, 8);
        }

        private void recHstryInfcButton_Click(object sender, EventArgs e)
        {
            if (this.glInfcListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(
              Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(
              this.glInfcListView.SelectedItems[0].SubItems[12].Text),
              "pay.pay_gl_interface", "interface_id"), 7);
        }
        #endregion
        #region "Benefits & Contributions..."
        private void loadBnftsPanel()
        {
            this.obey_itm_evnts = false;
            if (this.searchInItmComboBox.SelectedIndex < 0)
            {
                this.searchInItmComboBox.SelectedIndex = 0;
            }
            if (this.searchForItmTextBox.Text.Contains("%") == false)
            {
                this.searchForItmTextBox.Text = "%" + this.searchForItmTextBox.Text.Replace(" ", "%") + "%";
            }
            if (this.searchForItmTextBox.Text == "%%")
            {
                this.searchForItmTextBox.Text = "%";
            }
            int dsply = 0;
            if (this.dsplySizeItmComboBox.Text == ""
                || int.TryParse(this.dsplySizeItmComboBox.Text, out dsply) == false)
            {
                this.dsplySizeItmComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            //this.groupBox8.Height = this.g.Bottom - this.toolStrip4.Bottom - 50;
            this.is_last_itm = false;
            this.totl_itm = Global.mnFrm.cmCde.Big_Val;
            this.getItmPnlData();
            this.obey_itm_evnts = true;
        }

        private void getItmPnlData()
        {
            this.updtItmTotals();
            this.populateItmListVw();
            this.updtItmNavLabels();
        }

        private void updtItmTotals()
        {
            Global.mnFrm.cmCde.navFuncts.FindNavigationIndices(
                long.Parse(this.dsplySizeItmComboBox.Text), this.totl_itm);
            if (this.itm_cur_indx >= Global.mnFrm.cmCde.navFuncts.totalGroups)
            {
                this.itm_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            if (this.itm_cur_indx < 0)
            {
                this.itm_cur_indx = 0;
            }
            Global.mnFrm.cmCde.navFuncts.currentNavigationIndex = this.itm_cur_indx;
        }

        private void updtItmNavLabels()
        {
            this.moveFirstItmButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveFirstBtnStatus();
            this.movePreviousItmButton.Enabled = Global.mnFrm.cmCde.navFuncts.movePrevBtnStatus();
            this.moveNextItmButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveNextBtnStatus();
            this.moveLastItmButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveLastBtnStatus();
            this.positionItmTextBox.Text = Global.mnFrm.cmCde.navFuncts.displayedRecordsNumbers();
            if (this.is_last_itm == true ||
                this.totl_itm != Global.mnFrm.cmCde.Big_Val)
            {
                this.totalRecsItmLabel.Text = Global.mnFrm.cmCde.navFuncts.totalRecordsLabel();
            }
            else
            {
                this.totalRecsItmLabel.Text = "of Total";
            }
        }

        private void populateItmListVw()
        {
            this.obey_itm_evnts = false;
            DataSet dtst = Global.get_Basic_Itm(this.searchForItmTextBox.Text,
                this.searchInItmComboBox.Text, this.itm_cur_indx,
                int.Parse(this.dsplySizeItmComboBox.Text),
                Global.mnFrm.cmCde.Org_id);
            this.itemListView.Items.Clear();

            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.last_itm_num = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
                string itmnm = dtst.Tables[0].Rows[i][1].ToString();
                if (dtst.Tables[0].Rows[i][2].ToString() == "Balance Item")
                {
                    itmnm = itmnm.ToUpper();
                }
                ListViewItem nwItem = new ListViewItem(new string[] {
    (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
    itmnm,
    dtst.Tables[0].Rows[i][0].ToString()});
                if (dtst.Tables[0].Rows[i][2].ToString() == "Balance Item")
                {
                    nwItem.ForeColor = Color.Blue;
                }

                this.itemListView.Items.Add(nwItem);
            }
            this.correctItmNavLbls(dtst);
            if (this.itemListView.Items.Count > 0)
            {
                this.obey_itm_evnts = true;
                this.itemListView.Items[0].Selected = true;
            }
            else
            {
                this.populateItmDet(-10000000);
            }
            this.obey_itm_evnts = true;
        }

        private void populateItmDet(long itmID)
        {
            if (this.edititm == false)
            {
                this.clearItmInfo();
                this.disableItmEdit();
            }
            this.obey_itm_evnts = false;
            DataSet dtst = Global.get_One_Itm_Det(itmID);
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.itemIDTextBox.Text = dtst.Tables[0].Rows[i][0].ToString();
                this.itemNameTextBox.Text = dtst.Tables[0].Rows[i][1].ToString();
                this.itemDescTextBox.Text = dtst.Tables[0].Rows[i][2].ToString();

                this.salesItemIDTextBox.Text = dtst.Tables[0].Rows[i][19].ToString();
                this.salesItemTextBox.Text = Global.get_InvItemNm(int.Parse(dtst.Tables[0].Rows[i][19].ToString()));

                this.retroIDTextBox.Text = dtst.Tables[0].Rows[i][18].ToString();
                this.retroNmTextBox.Text = Global.get_PayItemNm(int.Parse(dtst.Tables[0].Rows[i][18].ToString()));

                if (this.edititm == false && this.additm == false)
                {
                    this.itmMajTypComboBox.Items.Clear();
                    this.itmMajTypComboBox.Items.Add(dtst.Tables[0].Rows[i][3].ToString());
                }
                this.itmMajTypComboBox.SelectedItem = dtst.Tables[0].Rows[i][3].ToString();

                if (this.edititm == false && this.additm == false)
                {
                    this.itmMinTypComboBox.Items.Clear();
                    this.itmMinTypComboBox.Items.Add(dtst.Tables[0].Rows[i][4].ToString());
                }
                this.itmMinTypComboBox.SelectedItem = dtst.Tables[0].Rows[i][4].ToString();

                if (this.edititm == false && this.additm == false)
                {
                    this.itmUOMComboBox.Items.Clear();
                    this.itmUOMComboBox.Items.Add(dtst.Tables[0].Rows[i][5].ToString());
                }
                this.itmUOMComboBox.SelectedItem = dtst.Tables[0].Rows[i][5].ToString();

                if (this.edititm == false && this.additm == false)
                {
                    this.freqComboBox.Items.Clear();
                    this.freqComboBox.Items.Add(dtst.Tables[0].Rows[i][11].ToString());
                }
                this.freqComboBox.SelectedItem = dtst.Tables[0].Rows[i][11].ToString();

                if (this.edititm == false && this.additm == false)
                {
                    this.balsTypComboBox.Items.Clear();
                    this.balsTypComboBox.Items.Add(dtst.Tables[0].Rows[i][16].ToString());
                }
                this.balsTypComboBox.SelectedItem = dtst.Tables[0].Rows[i][16].ToString();

                this.isEnabledItmCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][9].ToString());
                this.usesSQLCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][6].ToString());
                this.isRetroCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][17].ToString());
                this.allwEditCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][20].ToString());
                this.createsAccntngCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][21].ToString());
                this.locClassTextBox.Text = dtst.Tables[0].Rows[i][12].ToString();
                this.priorityNumUpDown.Value = Decimal.Parse(dtst.Tables[0].Rows[i][13].ToString());

                if (this.edititm == false && this.additm == false)
                {
                    this.costAccntComboBox.Items.Clear();
                    this.costAccntComboBox.Items.Add(dtst.Tables[0].Rows[i][14].ToString());
                }
                this.costAccntComboBox.SelectedItem = dtst.Tables[0].Rows[i][14].ToString();

                if (this.edititm == false && this.additm == false)
                {
                    this.blsAccntComboBox.Items.Clear();
                    this.blsAccntComboBox.Items.Add(dtst.Tables[0].Rows[i][15].ToString());
                }
                this.blsAccntComboBox.SelectedItem = dtst.Tables[0].Rows[i][15].ToString();

                if (this.edititm == false && this.additm == false)
                {
                    this.effectOnOrgDebtComboBox.Items.Clear();
                    this.effectOnOrgDebtComboBox.Items.Add(dtst.Tables[0].Rows[i][22].ToString());
                }
                this.effectOnOrgDebtComboBox.SelectedItem = dtst.Tables[0].Rows[i][22].ToString();

                this.costAcntIDTextBox.Text = dtst.Tables[0].Rows[i][7].ToString();
                this.costAcntNmTextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(dtst.Tables[0].Rows[i][7].ToString())) +
                    "." + Global.mnFrm.cmCde.getAccntName(int.Parse(dtst.Tables[0].Rows[i][7].ToString()));

                this.balsAcntIDTextBox.Text = dtst.Tables[0].Rows[i][8].ToString();
                this.balsAcntNmTextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(dtst.Tables[0].Rows[i][8].ToString())) +
                    "." + Global.mnFrm.cmCde.getAccntName(int.Parse(dtst.Tables[0].Rows[i][8].ToString()));
            }
            this.loadPyItmsPanel();
            this.loadFeedItmsPanel();
            this.obey_itm_evnts = true;
        }

        private void correctItmNavLbls(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.itm_cur_indx == 0 && totlRecs == 0)
            {
                this.is_last_itm = true;
                this.totl_itm = 0;
                this.last_itm_num = 0;
                this.itm_cur_indx = 0;
                this.updtItmTotals();
                this.updtItmNavLabels();
            }
            else if (this.totl_itm == Global.mnFrm.cmCde.Big_Val
         && totlRecs < long.Parse(this.dsplySizeItmComboBox.Text))
            {
                this.totl_itm = this.last_itm_num;
                if (totlRecs == 0)
                {
                    this.itm_cur_indx -= 1;
                    this.updtItmTotals();
                    this.populateItmListVw();
                }
                else
                {
                    this.updtItmTotals();
                }
            }
        }

        private void clearItmInfo()
        {
            this.obey_itm_evnts = false;
            this.saveItmButton.Enabled = false;
            this.addItmButton.Enabled = this.additms;
            this.editItmButton.Enabled = this.edititms;
            this.itemIDTextBox.Text = "-1";
            this.itemNameTextBox.Text = "";
            this.itemDescTextBox.Text = "";
            this.itmMajTypComboBox.Items.Clear();
            this.itmMinTypComboBox.Items.Clear();
            this.itmUOMComboBox.Items.Clear();
            this.freqComboBox.Items.Clear();
            this.costAccntComboBox.Items.Clear();
            this.blsAccntComboBox.Items.Clear();
            this.balsTypComboBox.Items.Clear();
            this.effectOnOrgDebtComboBox.Items.Clear();
            this.salesItemIDTextBox.Text = "-1";
            this.salesItemTextBox.Text = "";

            this.retroIDTextBox.Text = "-1";
            this.retroNmTextBox.Text = "";

            this.isEnabledItmCheckBox.Checked = false;
            this.usesSQLCheckBox.Checked = false;
            this.isRetroCheckBox.Checked = false;
            this.allwEditCheckBox.Checked = false;
            this.createsAccntngCheckBox.Checked = false;

            this.locClassTextBox.Text = "";
            this.costAcntIDTextBox.Text = "-1";
            this.costAcntNmTextBox.Text = "";

            this.balsAcntIDTextBox.Text = "-1";
            this.balsAcntNmTextBox.Text = "";
            this.priorityNumUpDown.Value = 500;

            this.obey_itm_evnts = true;
        }

        private void prpareForItmEdit()
        {/*
*/
            this.saveItmButton.Enabled = true;
            this.itemNameTextBox.ReadOnly = false;
            this.itemNameTextBox.BackColor = Color.FromArgb(255, 255, 118);
            this.itemDescTextBox.ReadOnly = false;
            this.itemDescTextBox.BackColor = Color.White;

            this.locClassTextBox.ReadOnly = false;
            this.locClassTextBox.BackColor = Color.White;

            this.salesItemTextBox.ReadOnly = true;
            this.salesItemTextBox.BackColor = Color.White;

            this.retroNmTextBox.ReadOnly = true;
            this.retroNmTextBox.BackColor = Color.White;

            this.costAcntNmTextBox.ReadOnly = false;
            this.costAcntNmTextBox.BackColor = Color.White;

            this.balsAcntNmTextBox.ReadOnly = false;
            this.balsAcntNmTextBox.BackColor = Color.White;

            this.priorityNumUpDown.Increment = 1;
            this.priorityNumUpDown.ReadOnly = false;
            string selItm = this.itmMajTypComboBox.Text;
            this.itmMajTypComboBox.Items.Clear();
            this.itmMajTypComboBox.Items.Add("Balance Item");
            this.itmMajTypComboBox.Items.Add("Pay Value Item");
            if (this.edititm == true)
            {
                this.itmMajTypComboBox.SelectedItem = selItm;
            }

            selItm = this.itmMinTypComboBox.Text;
            this.itmMinTypComboBox.Items.Clear();
            this.itmMinTypComboBox.Items.Add("Earnings");
            this.itmMinTypComboBox.Items.Add("Employer Charges");
            this.itmMinTypComboBox.Items.Add("Deductions");
            this.itmMinTypComboBox.Items.Add("Bills/Charges");
            this.itmMinTypComboBox.Items.Add("Purely Informational");
            if (this.edititm == true)
            {
                this.itmMinTypComboBox.SelectedItem = selItm;
            }

            selItm = this.itmUOMComboBox.Text;
            this.itmUOMComboBox.Items.Clear();
            this.itmUOMComboBox.Items.Add("Money");
            this.itmUOMComboBox.Items.Add("Number");
            if (this.edititm == true)
            {
                this.itmUOMComboBox.SelectedItem = selItm;
            }


            selItm = this.freqComboBox.Text;
            this.freqComboBox.Items.Clear();
            this.freqComboBox.Items.Add("Daily");
            this.freqComboBox.Items.Add("Weekly");
            this.freqComboBox.Items.Add("Fortnightly");
            this.freqComboBox.Items.Add("Semi-Monthly");
            this.freqComboBox.Items.Add("Monthly");
            this.freqComboBox.Items.Add("Once a Month");
            this.freqComboBox.Items.Add("Twice a Month");
            this.freqComboBox.Items.Add("Quarterly");
            this.freqComboBox.Items.Add("Half-Yearly");
            this.freqComboBox.Items.Add("Annually");
            this.freqComboBox.Items.Add("Adhoc");
            this.freqComboBox.Items.Add("None");
            if (this.edititm == true)
            {
                this.freqComboBox.SelectedItem = selItm;
            }

            selItm = this.costAccntComboBox.Text;
            this.costAccntComboBox.Items.Clear();
            this.costAccntComboBox.Items.Add("Increase");
            this.costAccntComboBox.Items.Add("Decrease");
            this.costAccntComboBox.Items.Add("None");
            if (this.edititm == true)
            {
                this.costAccntComboBox.SelectedItem = selItm;
            }

            selItm = this.blsAccntComboBox.Text;
            this.blsAccntComboBox.Items.Clear();
            this.blsAccntComboBox.Items.Add("Increase");
            this.blsAccntComboBox.Items.Add("Decrease");
            this.blsAccntComboBox.Items.Add("None");
            if (this.edititm == true)
            {
                this.blsAccntComboBox.SelectedItem = selItm;
            }
            selItm = this.effectOnOrgDebtComboBox.Text;
            this.effectOnOrgDebtComboBox.Items.Clear();
            this.effectOnOrgDebtComboBox.Items.Add("Increase");
            this.effectOnOrgDebtComboBox.Items.Add("Decrease");
            this.effectOnOrgDebtComboBox.Items.Add("None");
            if (this.edititm == true)
            {
                this.effectOnOrgDebtComboBox.SelectedItem = selItm;
            }
            selItm = this.balsTypComboBox.Text;
            this.balsTypComboBox.Items.Clear();
            this.balsTypComboBox.Items.Add("");
            this.balsTypComboBox.Items.Add("Cumulative");
            this.balsTypComboBox.Items.Add("Non-Cumulative");
            if (this.edititm == true)
            {
                this.balsTypComboBox.SelectedItem = selItm;
            }
        }

        private void disableItmEdit()
        {
            this.additm = false;
            this.edititm = false;
            this.saveItmButton.Enabled = false;
            this.editItmButton.Enabled = this.additms;
            this.addItmButton.Enabled = this.edititms;
            this.priorityNumUpDown.Increment = 0;
            this.priorityNumUpDown.ReadOnly = true;
            this.editItmButton.Text = "EDIT";
            this.editItmMenuItem.Text = "Edit Pay Item";
            this.itemNameTextBox.ReadOnly = true;
            this.itemNameTextBox.BackColor = Color.WhiteSmoke;
            this.itemIDTextBox.ReadOnly = true;
            this.itemIDTextBox.BackColor = Color.WhiteSmoke;
            this.itemDescTextBox.ReadOnly = true;
            this.itemDescTextBox.BackColor = Color.WhiteSmoke;
            this.locClassTextBox.ReadOnly = true;
            this.locClassTextBox.BackColor = Color.WhiteSmoke;
            this.salesItemTextBox.ReadOnly = true;
            this.salesItemTextBox.BackColor = Color.WhiteSmoke;

            this.retroNmTextBox.ReadOnly = true;
            this.retroNmTextBox.BackColor = Color.WhiteSmoke;

            this.costAcntNmTextBox.ReadOnly = true;
            this.costAcntNmTextBox.BackColor = Color.WhiteSmoke;

            this.balsAcntNmTextBox.ReadOnly = true;
            this.balsAcntNmTextBox.BackColor = Color.WhiteSmoke;

        }

        private bool shdObeyItmEvts()
        {
            return this.obey_itm_evnts;
        }

        private void ItmPnlNavButtons(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecsItmLabel.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.is_last_itm = false;
                this.itm_cur_indx = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.is_last_itm = false;
                this.itm_cur_indx -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.is_last_itm = false;
                this.itm_cur_indx += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.is_last_itm = true;
                this.totl_itm = Global.get_Total_Itm(this.searchForItmTextBox.Text,
                    this.searchInItmComboBox.Text, Global.mnFrm.cmCde.Org_id);
                this.updtItmTotals();
                this.itm_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            this.getItmPnlData();
        }

        private void itemListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.shdObeyItmEvts() == false || this.itemListView.SelectedItems.Count > 1)
            {
                return;
            }
            if (this.itemListView.SelectedItems.Count > 0)
            {
                this.populateItmDet(int.Parse(this.itemListView.SelectedItems[0].SubItems[2].Text));
            }
            else
            {
                this.populateItmDet(-1000);
            }
        }

        private void goItmButton_Click(object sender, EventArgs e)
        {
            this.disableItmEdit();
            System.Windows.Forms.Application.DoEvents();
            this.loadBnftsPanel();
        }

        private void addItmButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[26]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                return;
            }
            this.clearItmInfo();
            this.additm = true;
            this.edititm = false;
            this.prpareForItmEdit();
            this.loadFeedItmsPanel();
            this.loadPyItmsPanel();
            this.addItmButton.Enabled = false;
            this.editItmButton.Enabled = false;
        }

        private void editItmButton_Click(object sender, EventArgs e)
        {
            if (this.editItmButton.Text == "EDIT")
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[27]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                        " this action!\nContact your System Administrator!", 0);
                    return;
                }
                if (this.itemIDTextBox.Text == "" || this.itemIDTextBox.Text == "-1")
                {
                    Global.mnFrm.cmCde.showMsg("No record to Edit!", 0);
                    return;
                }
                this.additm = false;
                this.edititm = true;
                this.prpareForItmEdit();
                this.addItmButton.Enabled = false;
                //this.editItmButton.Enabled = false;
                this.editItmButton.Text = "STOP";
                this.editItmMenuItem.Text = "STOP EDITING";
            }
            else
            {
                this.disableItmEdit();
                System.Windows.Forms.Application.DoEvents();
                this.loadBnftsPanel();
            }
        }

        private void saveItmButton_Click(object sender, EventArgs e)
        {
            if (this.additm == true)
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[26]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                     " this action!\nContact your System Administrator!", 0);
                    return;
                }
            }
            else
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[27]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                     " this action!\nContact your System Administrator!", 0);
                    return;
                }
            }
            if (this.itemNameTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please enter an Item name!", 0);
                return;
            }

            long oldItmID = Global.mnFrm.cmCde.getItmID(this.itemNameTextBox.Text,
                Global.mnFrm.cmCde.Org_id);
            if (oldItmID > 0
             && this.additm == true)
            {
                Global.mnFrm.cmCde.showMsg("Item Name is already in use in this Organisation!", 0);
                return;
            }
            if (oldItmID > 0
             && this.edititm == true
             && oldItmID.ToString() !=
             this.itemIDTextBox.Text)
            {
                Global.mnFrm.cmCde.showMsg("New Item Name is already in use in this Organisation!", 0);
                return;
            }

            if (this.itmMajTypComboBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Item Major Type cannot be empty!", 0);
                return;
            }
            if (this.itmMinTypComboBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Item Minor Type cannot be empty!", 0);
                return;
            }
            if (this.itmUOMComboBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Item Unit of Measure cannot be empty!", 0);
                return;
            }
            if (this.freqComboBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please indicate Payment Frequency!", 0);
                return;
            }
            if (this.itmUOMComboBox.Text == "Money" && this.createsAccntngCheckBox.Checked)
            {
                if (this.costAccntComboBox.Text == "")
                {
                    Global.mnFrm.cmCde.showMsg("Cost Account(Increase/Decrease) cannot be empty if Item UOM is Money!", 0);
                    return;
                }
                if (this.blsAccntComboBox.Text == "")
                {
                    Global.mnFrm.cmCde.showMsg("Balancing Account(Increase/Decrease) cannot be empty if Item UOM is Money!", 0);
                    return;
                }
                if (this.balsAcntIDTextBox.Text == "" || this.balsAcntIDTextBox.Text == "-1")
                {
                    Global.mnFrm.cmCde.showMsg("Balancing Account cannot be empty if Item UOM is Money!", 0);
                    return;
                }
                if (this.costAcntIDTextBox.Text == "" || this.costAcntIDTextBox.Text == "-1")
                {
                    Global.mnFrm.cmCde.showMsg("Cost Account cannot be Empty if Item UOM is Money!", 0);
                    return;
                }
                if (this.costAcntIDTextBox.Text == this.balsAcntIDTextBox.Text)
                {
                    Global.mnFrm.cmCde.showMsg("Cost Account and Balancing Account cannot be the same!", 0);
                    return;
                }
                if (this.itmMinTypComboBox.Text == "Bills/Charges"
                    || this.itmMinTypComboBox.Text == "Deductions"
                    || this.itmMinTypComboBox.Text == "Deductions"
                    || this.itmMinTypComboBox.Text == "Deductions")
                {
                    if (Global.mnFrm.cmCde.dbtOrCrdtAccnt(int.Parse(this.costAcntIDTextBox.Text),
                        this.costAccntComboBox.Text.Substring(0, 1)) != "Credit")
                    {
                        Global.mnFrm.cmCde.showMsg("The Cost Account specified is Invalid!\r\nExpecting you to increase a Revenue or Liability account\r\n or to Decrease a Receivables account!", 0);
                        return;
                    }
                    if (Global.mnFrm.cmCde.dbtOrCrdtAccnt(int.Parse(this.balsAcntIDTextBox.Text),
                        this.blsAccntComboBox.Text.Substring(0, 1)) != "Debit")
                    {
                        Global.mnFrm.cmCde.showMsg("The Balancing Account specified is Invalid!\r\nExpecting you to increase a Cash Account\r\n or to Decrease a Liability Account!", 0);
                        return;
                    }
                }
                if (this.itmMinTypComboBox.Text == "Employer Charges"
                    || this.itmMinTypComboBox.Text == "Earnings")
                {
                    if (Global.mnFrm.cmCde.dbtOrCrdtAccnt(int.Parse(this.costAcntIDTextBox.Text),
                        this.costAccntComboBox.Text.Substring(0, 1)) != "Debit")
                    {
                        Global.mnFrm.cmCde.showMsg("The Cost Account specified is Invalid!\r\nExpecting you to increase an Expense or Receivables Account!", 0);
                        return;
                    }
                    if (Global.mnFrm.cmCde.dbtOrCrdtAccnt(int.Parse(this.balsAcntIDTextBox.Text),
                        this.blsAccntComboBox.Text.Substring(0, 1)) != "Credit")
                    {
                        Global.mnFrm.cmCde.showMsg("The Balancing Account specified is Invalid!\r\nExpecting you to Decrease a Cash Account\r\n or to Increase a Liability Account!", 0);
                        return;
                    }
                }
            }
            else if (this.itmUOMComboBox.Text == "Number" || this.itmMinTypComboBox.Text == "Purely Informational"
              || this.createsAccntngCheckBox.Checked == false)
            {
                if (this.costAcntIDTextBox.Text != "-1" ||
                    this.balsAcntIDTextBox.Text != "-1")
                {
                    Global.mnFrm.cmCde.showMsg("Cannot provide accounts if item Unit of \r\nMeasure is 'Number' or if Item is a Purely Informational Item\r\n or Item is not allowed to Create Accounting", 0);
                    return;
                }
            }
            if (this.itmMajTypComboBox.Text == "Balance Item" && this.balsTypComboBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Balance Type cannot be empty for a balance Item", 0);
                return;
            }
            if (this.itmMajTypComboBox.Text != "Balance Item" && this.balsTypComboBox.Text != "")
            {
                Global.mnFrm.cmCde.showMsg("Cannot provide a Balance Type for a Pay Value Item", 0);
                return;
            }
            int itmMnTypID = -1;
            if (this.itmMinTypComboBox.Text == "Earnings")
            {
                itmMnTypID = 1;
            }
            else if (this.itmMinTypComboBox.Text == "Employer Charges")
            {
                itmMnTypID = 2;
            }
            else if (this.itmMinTypComboBox.Text == "Deductions")
            {
                itmMnTypID = 3;
            }
            else if (this.itmMinTypComboBox.Text == "Bills/Charges")
            {
                itmMnTypID = 4;
            }
            else if (this.itmMinTypComboBox.Text == "Purely Informational")
            {
                itmMnTypID = 5;
            }
            if (this.itmMajTypComboBox.Text == "Balance Item" && this.usesSQLCheckBox.Checked == true && this.feedItemsListView.Items.Count > 0)
            {
                Global.mnFrm.cmCde.showMsg("Cannot Provide Feed Items for a Balance Item whose \r\nBalance is Generated Dynamically!", 0);
                return;
            }
            if (this.effectOnOrgDebtComboBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Effect on Person's Organisational Debt Cannot be Empty!", 0);
                return;
            }
            if ((this.itmMajTypComboBox.Text == "Balance Item"
              || this.itmMajTypComboBox.Text == "") &&
              (long.Parse(this.retroIDTextBox.Text) > 0 ||
              long.Parse(this.salesItemIDTextBox.Text) > 0))
            {
                Global.mnFrm.cmCde.showMsg("Cannot Select Sales or Retro Item for a Balance Item!", 0);
                return;
            }
            if (this.additm == true)
            {
                Global.createItm(Global.mnFrm.cmCde.Org_id, this.itemNameTextBox.Text,
                    this.itemDescTextBox.Text, this.itmMajTypComboBox.Text, this.itmMinTypComboBox.Text,
                    this.itmUOMComboBox.Text, this.usesSQLCheckBox.Checked,
                    this.isEnabledItmCheckBox.Checked, int.Parse(this.costAcntIDTextBox.Text), int.Parse(this.balsAcntIDTextBox.Text)
                    , this.freqComboBox.Text, this.locClassTextBox.Text,
                    (double)this.priorityNumUpDown.Value, this.costAccntComboBox.Text,
                    this.blsAccntComboBox.Text, this.balsTypComboBox.Text, itmMnTypID,
                    this.isRetroCheckBox.Checked, int.Parse(this.retroIDTextBox.Text),
                    int.Parse(this.salesItemIDTextBox.Text), this.allwEditCheckBox.Checked, this.createsAccntngCheckBox.Checked, this.effectOnOrgDebtComboBox.Text);

                if (this.itmMajTypComboBox.Text == "Balance Item")
                {
                    Global.createItmVal(Global.mnFrm.cmCde.getItmID(this.itemNameTextBox.Text, Global.mnFrm.cmCde.Org_id)
                      , 0, "", this.itemNameTextBox.Text + " Value");
                }
                else
                {
                    if (this.usesSQLCheckBox.Checked == true)
                    {
                        Global.createItmVal(Global.mnFrm.cmCde.getItmID(this.itemNameTextBox.Text, Global.mnFrm.cmCde.Org_id)
                  , 0, "select 0", this.itemNameTextBox.Text + " Value 1");
                    }
                    else
                    {
                        Global.createItmVal(Global.mnFrm.cmCde.getItmID(this.itemNameTextBox.Text, Global.mnFrm.cmCde.Org_id)
                  , 0, "", this.itemNameTextBox.Text + " Value 1");
                    }
                }
                this.saveItmButton.Enabled = false;
                this.additm = false;
                this.edititm = false;
                this.editItmButton.Enabled = this.additms;
                this.addItmButton.Enabled = this.edititms;
                System.Windows.Forms.Application.DoEvents();
                System.Windows.Forms.Application.DoEvents();
                this.itemIDTextBox.Text = Global.mnFrm.cmCde.getGnrlRecID(
                  "org.org_pay_items",
                  "item_code_name", "item_id",
                  this.itemNameTextBox.Text, Global.mnFrm.cmCde.Org_id).ToString();
                bool prv = this.obey_itm_evnts;
                this.obey_itm_evnts = false;
                ListViewItem nwItem = new ListViewItem(new string[] {
    "New",
    this.itemNameTextBox.Text,
      this.itemIDTextBox.Text});
                this.itemListView.Items.Insert(0, nwItem);
                for (int i = 0; i < this.itemListView.SelectedItems.Count; i++)
                {
                    this.itemListView.SelectedItems[i].Font = new Font("Tahoma", 8.25f, FontStyle.Regular);
                    this.itemListView.SelectedItems[i].Selected = false;
                }
                this.itemListView.Items[0].Selected = true;
                this.itemListView.Items[0].Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
                this.obey_itm_evnts = prv;
                System.Windows.Forms.Application.DoEvents();
                Global.mnFrm.cmCde.showMsg("Record Saved!", 3);

                this.saveItmButton.Enabled = true;
                this.edititm = true;
                if (this.edititms == true)
                {
                    this.editItmButton.Enabled = true;
                    this.editItmButton_Click(this.editItmButton, e);
                    this.refreshValButton_Click(this.refreshValButton, e);
                }
                else
                {
                    this.disableItmEdit();
                    System.Windows.Forms.Application.DoEvents();
                    this.loadBnftsPanel();
                }
            }
            else if (this.edititm == true)
            {
                Global.updateItm(Global.mnFrm.cmCde.Org_id, int.Parse(this.itemIDTextBox.Text), this.itemNameTextBox.Text,
                    this.itemDescTextBox.Text, this.itmMajTypComboBox.Text, this.itmMinTypComboBox.Text,
                    this.itmUOMComboBox.Text, this.usesSQLCheckBox.Checked,
                    this.isEnabledItmCheckBox.Checked, int.Parse(this.costAcntIDTextBox.Text), int.Parse(this.balsAcntIDTextBox.Text)
                    , this.freqComboBox.Text, this.locClassTextBox.Text, (double)this.priorityNumUpDown.Value,
                    this.costAccntComboBox.Text, this.blsAccntComboBox.Text, this.balsTypComboBox.Text, itmMnTypID,
                    this.isRetroCheckBox.Checked, int.Parse(this.retroIDTextBox.Text),
                    int.Parse(this.salesItemIDTextBox.Text), this.allwEditCheckBox.Checked, this.createsAccntngCheckBox.Checked, this.effectOnOrgDebtComboBox.Text);

                if (this.itemListView.SelectedItems.Count > 0)
                {
                    this.itemListView.SelectedItems[0].SubItems[1].Text = this.itemNameTextBox.Text;
                }
                Global.mnFrm.cmCde.showMsg("Record Saved!", 3);
            }
        }

        private void cashAcntButton_Click(object sender, EventArgs e)
        {
            if (this.additm == false && this.edititm == false)
            {
                Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                return;
            }
            string[] selVals = new string[1];
            selVals[0] = this.costAcntIDTextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Transaction Accounts"), ref selVals,
                true, false, Global.mnFrm.cmCde.Org_id,
             this.srchWrd, "Both", true);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.costAcntIDTextBox.Text = selVals[i];
                    this.costAcntNmTextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(selVals[i])) + "." + Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));
                }
            }
        }

        private void expnsAcntButton_Click(object sender, EventArgs e)
        {
            if (this.additm == false && this.edititm == false)
            {
                Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                return;
            }
            string[] selVals = new string[1];
            selVals[0] = this.balsAcntIDTextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Transaction Accounts"), ref selVals,
                true, false, Global.mnFrm.cmCde.Org_id,
             this.srchWrd, "Both", true);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.balsAcntIDTextBox.Text = selVals[i];
                    this.balsAcntNmTextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(selVals[i])) + "." + Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));
                }
            }
        }

        private void itmOtherInfoButton_Click(object sender, EventArgs e)
        {
            if (this.itemIDTextBox.Text == ""
                || this.itemIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("No record to View!", 0);
                return;
            }
            DialogResult dgres = this.cmCde.showRowsExtInfDiag(this.cmCde.getMdlGrpID("Pay Items"),
                long.Parse(this.itemIDTextBox.Text), "pay.pay_all_other_info_table",
                this.itemNameTextBox.Text, this.edititms, 8, 7,
                "pay.pay_all_other_info_table_dflt_row_id_seq");
            if (dgres == DialogResult.OK)
            {
            }
        }

        private void addValButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[26]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.itemListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select an Item First!", 0);
                return;
            }
            if (this.itmMajTypComboBox.Text == "Balance Item")
            {
                Global.mnFrm.cmCde.showMsg("Cannot define a value for a Balance Item", 0);
                return;
            }
            itemValDiag nwDiag = new itemValDiag();
            if (this.usesSQLCheckBox.Checked == true)
            {
                nwDiag.pssblValAmntNumericUpDown.Enabled = false;
                nwDiag.pssblValAmntNumericUpDown.ReadOnly = true;
                nwDiag.pssblValAmntNumericUpDown.Value = 0;
            }
            else
            {
                nwDiag.sqlFormulaTextBox.Enabled = false;
                nwDiag.sqlFormulaTextBox.ReadOnly = true;
                nwDiag.sqlFormulaTextBox.Text = "";
            }
            DialogResult dgres = nwDiag.ShowDialog();
            if (dgres == DialogResult.OK)
            {
                long oldValID = Global.mnFrm.cmCde.getItmValID(nwDiag.pssblValNmTextBox.Text,
                  long.Parse(this.itemListView.SelectedItems[0].SubItems[2].Text));
                if (oldValID > 0)
                {
                    Global.mnFrm.cmCde.showMsg("Possible Value Name is already in use in this Pay Item!", 0);
                    return;
                }

                //string dateStr = Global.mnFrm.cmCde.getDB_Date_time();
                //dateStr.Substring(0, 11), "31-Dec-4000"
                Global.createItmVal(long.Parse(this.itemListView.SelectedItems[0].SubItems[2].Text),
                    (double)(nwDiag.pssblValAmntNumericUpDown.Value), nwDiag.sqlFormulaTextBox.Text,
                    nwDiag.pssblValNmTextBox.Text);
                this.loadPyItmsPanel();
            }
        }

        private void editValButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[27]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.itmPssblValDataGridView.CurrentCell != null
         && this.itmPssblValDataGridView.SelectedRows.Count <= 0)
            {
                this.itmPssblValDataGridView.Rows[this.itmPssblValDataGridView.CurrentCell.RowIndex].Selected = true;
            }
            if (this.itmPssblValDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the Row to Edit!", 0);
                return;
            }
            itemValDiag nwDiag = new itemValDiag();
            nwDiag.pssbl_val_id = long.Parse(this.itmPssblValDataGridView.SelectedRows[0].Cells[2].Value.ToString());
            nwDiag.item_id = long.Parse(this.itmPssblValDataGridView.SelectedRows[0].Cells[3].Value.ToString());
            if (this.usesSQLCheckBox.Checked == true)
            {
                nwDiag.pssblValAmntNumericUpDown.Enabled = false;
                nwDiag.pssblValAmntNumericUpDown.ReadOnly = true;
                nwDiag.pssblValAmntNumericUpDown.Value = 0;
                nwDiag.sqlFormulaTextBox.Text = this.itmPssblValDataGridView.SelectedRows[0].Cells[1].Value.ToString();
            }
            else
            {
                nwDiag.sqlFormulaTextBox.Enabled = false;
                nwDiag.sqlFormulaTextBox.ReadOnly = true;
                nwDiag.sqlFormulaTextBox.Text = "";
                decimal itmval = 0;
                decimal.TryParse(
                    this.itmPssblValDataGridView.SelectedRows[0].Cells[1].Value.ToString(), out itmval);
                nwDiag.pssblValAmntNumericUpDown.Value = itmval;
            }
            nwDiag.pssblValNmTextBox.Text = this.itmPssblValDataGridView.SelectedRows[0].Cells[0].Value.ToString();
            DialogResult dgres = nwDiag.ShowDialog();
            if (dgres == DialogResult.OK)
            {
                long oldValID = Global.mnFrm.cmCde.getItmValID(nwDiag.pssblValNmTextBox.Text,
            nwDiag.item_id);
                if (oldValID > 0
                 && oldValID.ToString() !=
                 nwDiag.pssbl_val_id.ToString())
                {
                    Global.mnFrm.cmCde.showMsg("New Possible Value Name is already in use in this Pay Item!", 0);
                    return;
                }
                Global.updateItmVal(nwDiag.pssbl_val_id, nwDiag.item_id,
            (double)nwDiag.pssblValAmntNumericUpDown.Value, nwDiag.sqlFormulaTextBox.Text,
            nwDiag.pssblValNmTextBox.Text);
            }
            this.loadPyItmsPanel();
        }

        private void delValButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[28]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.itmMajTypComboBox.Text == "Balance Item")
            {
                Global.mnFrm.cmCde.showMsg("Cannot DELETE Balance Item Values", 0);
                return;
            }
            if (this.itmPssblValDataGridView.CurrentCell != null)
            {
                this.itmPssblValDataGridView.Rows[this.itmPssblValDataGridView.CurrentCell.RowIndex].Selected = true;
            }
            if (this.itmPssblValDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the Row(s) to delete!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to delete the" +
                "\r\nselected Item Value(s)?", 1) == DialogResult.No)
            {
                Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            for (int i = 0; i < this.itmPssblValDataGridView.SelectedRows.Count; i++)
            {
                Global.deleteItmVals(long.Parse(
                  this.itmPssblValDataGridView.SelectedRows[i].Cells[2].Value.ToString()),
                  this.itmPssblValDataGridView.SelectedRows[i].Cells[0].Value.ToString());
            }
            this.populateItmDet(long.Parse(this.itemListView.SelectedItems[0].SubItems[2].Text));
        }

        private void refreshValButton_Click(object sender, EventArgs e)
        {
            if (this.itemListView.SelectedItems.Count > 0)
            {
                this.loadPyItmsPanel();
            }
        }

        private void addValMenuItem_Click(object sender, EventArgs e)
        {
            this.addValButton_Click(this.addValButton, e);
        }

        private void editValMenuItem_Click(object sender, EventArgs e)
        {
            this.editValButton_Click(this.editValButton, e);
        }

        private void delValMenuItem_Click(object sender, EventArgs e)
        {
            this.delValButton_Click(this.delValButton, e);
        }

        private void loadPyItmsPanel()
        {
            this.obey_pyitm_evnts = false;
            int dsply = 0;
            if (this.dsplySizePsblComboBox.Text == ""
                || int.TryParse(this.dsplySizePsblComboBox.Text, out dsply) == false)
            {
                this.dsplySizePsblComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            //this.groupBox8.Height = this.g.Bottom - this.toolStrip4.Bottom - 50;
            this.pyitm_cur_indx = 0;
            this.is_last_pyitm = false;
            this.totl_pyitm = Global.mnFrm.cmCde.Big_Val;
            this.getPyItmPnlData();
            this.obey_pyitm_evnts = true;
        }

        private void getPyItmPnlData()
        {
            this.updtPyItmTotals();
            this.populatePyItmGrdVw(long.Parse(this.itemIDTextBox.Text));
            //this.itemListView.SelectedItems[0].SubItems[2].Text
            this.updtPyItmNavLabels();
        }

        private void updtPyItmTotals()
        {
            this.myNav.FindNavigationIndices(
                long.Parse(this.dsplySizePsblComboBox.Text), this.totl_pyitm);
            if (this.pyitm_cur_indx >= this.myNav.totalGroups)
            {
                this.pyitm_cur_indx = this.myNav.totalGroups - 1;
            }
            if (this.pyitm_cur_indx < 0)
            {
                this.pyitm_cur_indx = 0;
            }
            this.myNav.currentNavigationIndex = this.pyitm_cur_indx;
        }

        private void updtPyItmNavLabels()
        {
            this.moveFirstPsblButton.Enabled = this.myNav.moveFirstBtnStatus();
            this.movePreviousPsblButton.Enabled = this.myNav.movePrevBtnStatus();
            this.moveNextPsblButton.Enabled = this.myNav.moveNextBtnStatus();
            this.moveLastPsblButton.Enabled = this.myNav.moveLastBtnStatus();
            this.positionPsblTextBox.Text = this.myNav.displayedRecordsNumbers();
            if (this.is_last_pyitm == true ||
                this.totl_pyitm != Global.mnFrm.cmCde.Big_Val)
            {
                this.totalRecsPsblLabel.Text = this.myNav.totalRecordsLabel();
            }
            else
            {
                this.totalRecsPsblLabel.Text = "of Total";
            }
        }

        private void populatePyItmGrdVw(long itmID)
        {
            this.obey_pyitm_evnts = false;

            DataSet dtst = Global.getAllItmVals(this.pyitm_cur_indx,
                int.Parse(this.dsplySizePsblComboBox.Text), itmID);
            this.itmPssblValDataGridView.Rows.Clear();
            this.itmPssblValDataGridView.RowCount = dtst.Tables[0].Rows.Count;
            this.itmPssblValDataGridView.ReadOnly = true;
            this.itmPssblValDataGridView.DefaultCellStyle.BackColor = Color.Gainsboro;
            this.itmPssblValDataGridView.DefaultCellStyle.ForeColor = Color.Black;
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.last_pyitm_num = this.myNav.startIndex() + i;
                this.itmPssblValDataGridView.Rows[i].HeaderCell.Value = (i + 1).ToString();
                Object[] cellDesc = new Object[4];
                cellDesc[0] = dtst.Tables[0].Rows[i][1].ToString();
                if (this.usesSQLCheckBox.Checked == false)
                {
                    cellDesc[1] = dtst.Tables[0].Rows[i][2].ToString();
                }
                else
                {
                    cellDesc[1] = dtst.Tables[0].Rows[i][3].ToString();
                }
                cellDesc[2] = dtst.Tables[0].Rows[i][0].ToString();
                cellDesc[3] = dtst.Tables[0].Rows[i][4].ToString();
                this.itmPssblValDataGridView.Rows[i].SetValues(cellDesc);
            }

            this.correctPyItmNavLbls(dtst);
            this.obey_pyitm_evnts = true;
        }

        private void correctPyItmNavLbls(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.pyitm_cur_indx == 0 && totlRecs == 0)
            {
                this.is_last_pyitm = true;
                this.totl_pyitm = 0;
                this.last_pyitm_num = 0;
                this.pyitm_cur_indx = 0;
                this.updtPyItmTotals();
                this.updtPyItmNavLabels();
            }
            else if (this.totl_pyitm == Global.mnFrm.cmCde.Big_Val
         && totlRecs < long.Parse(this.dsplySizePsblComboBox.Text))
            {
                this.totl_pyitm = this.last_pyitm_num;
                if (totlRecs == 0)
                {
                    this.pyitm_cur_indx -= 1;
                    this.updtPyItmTotals();
                    this.populatePyItmGrdVw(long.Parse(this.itemIDTextBox.Text));
                }
                else
                {
                    this.updtPyItmTotals();
                }
            }
        }

        private bool shdObeyPyItmEvts()
        {
            return this.obey_pyitm_evnts;
        }

        private void PyItmPnlNavButtons(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecsPsblLabel.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.is_last_pyitm = false;
                this.pyitm_cur_indx = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.is_last_pyitm = false;
                this.pyitm_cur_indx -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.is_last_pyitm = false;
                this.pyitm_cur_indx += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.is_last_pyitm = true;

                this.totl_pyitm = Global.get_Total_Psbl_Vl(long.Parse(this.itemIDTextBox.Text));
                this.updtPyItmTotals();
                this.pyitm_cur_indx = this.myNav.totalGroups - 1;
            }
            this.getPyItmPnlData();
        }

        private void exptExclPValsMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.itmPssblValDataGridView);
        }

        private void exptItmMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.itemListView);
        }

        private void exprtItemsButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtItemsTmp();
        }

        private void imprtItemsButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[26]) == false)
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
                Global.mnFrm.cmCde.imprtItemsTmp(this.openFileDialog1.FileName);
            }
            this.populateItmListVw();
        }

        private void itmMajTypComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.itmMajTypComboBox.Text == "Balance Item")
            {
                this.feedGroupBox.Text = "Pay Value Items that feed into this Balance Item".ToUpper();
                this.itmMinTypComboBox.SelectedItem = "None";
                this.freqComboBox.SelectedItem = "None";
                this.itmMinTypComboBox.SelectedItem = "Purely Informational";
                this.costAccntComboBox.SelectedItem = "None";
                this.blsAccntComboBox.SelectedItem = "None";
            }
            else
            {
                this.feedGroupBox.Text = "BALANCE ITEMS THIS ITEM FEEDS INTO";
            }
        }

        private void addFeedMenuItem_Click(object sender, EventArgs e)
        {
            this.addFeedButton_Click(this.addFeedButton, e);
        }

        private void loadFeedItmsPanel()
        {
            this.obey_feed_evnts = false;
            int dsply = 0;
            if (this.dsplySizeFeedComboBox.Text == ""
                || int.TryParse(this.dsplySizeFeedComboBox.Text, out dsply) == false)
            {
                this.dsplySizeFeedComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            this.feed_cur_indx = 0;
            this.is_last_feed = false;
            this.totl_feed = Global.mnFrm.cmCde.Big_Val;
            this.getFeedPnlData();
            this.obey_feed_evnts = true;
        }

        private void getFeedPnlData()
        {
            this.updtFeedTotals();
            this.populateFeedLstVw(long.Parse(this.itemIDTextBox.Text));
            this.updtFeedNavLabels();
        }

        private void updtFeedTotals()
        {
            this.myNav1.FindNavigationIndices(
                long.Parse(this.dsplySizeFeedComboBox.Text), this.totl_feed);
            if (this.feed_cur_indx >= this.myNav1.totalGroups)
            {
                this.feed_cur_indx = this.myNav1.totalGroups - 1;
            }
            if (this.feed_cur_indx < 0)
            {
                this.feed_cur_indx = 0;
            }
            this.myNav1.currentNavigationIndex = this.feed_cur_indx;
        }

        private void updtFeedNavLabels()
        {
            this.moveFirstFeedButton.Enabled = this.myNav1.moveFirstBtnStatus();
            this.movePreviousFeedButton.Enabled = this.myNav1.movePrevBtnStatus();
            this.moveNextFeedButton.Enabled = this.myNav1.moveNextBtnStatus();
            this.moveLastFeedButton.Enabled = this.myNav1.moveLastBtnStatus();
            this.positionFeedTextBox.Text = this.myNav1.displayedRecordsNumbers();
            if (this.is_last_feed == true ||
                this.totl_feed != Global.mnFrm.cmCde.Big_Val)
            {
                this.totalRecsFeedLabel.Text = this.myNav1.totalRecordsLabel();
            }
            else
            {
                this.totalRecsFeedLabel.Text = "of Total";
            }
        }

        private void populateFeedLstVw(long itmID)
        {
            this.obey_feed_evnts = false;
            DataSet dtst = Global.getAllItmFeeds(this.feed_cur_indx,
                int.Parse(this.dsplySizeFeedComboBox.Text), itmID);
            this.feedItemsListView.Items.Clear();
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.last_feed_num = this.myNav1.startIndex() + i;
                long dsplItm = -1;
                if (this.itmMajTypComboBox.Text == "Balance Item")
                {
                    dsplItm = long.Parse(dtst.Tables[0].Rows[i][1].ToString());
                }
                else
                {
                    dsplItm = long.Parse(dtst.Tables[0].Rows[i][0].ToString());
                }
                ListViewItem nwItem = new ListViewItem(new string[] {
    (this.myNav1.startIndex() + i).ToString(),
    Global.mnFrm.cmCde.getItmName(dsplItm),
    dtst.Tables[0].Rows[i][2].ToString(),
    dsplItm.ToString(),
    dtst.Tables[0].Rows[i][3].ToString(),
    dtst.Tables[0].Rows[i][4].ToString()});
                this.feedItemsListView.Items.Add(nwItem);
            }

            this.correctFeedNavLbls(dtst);
            this.obey_feed_evnts = true;
        }

        private void correctFeedNavLbls(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.feed_cur_indx == 0 && totlRecs == 0)
            {
                this.is_last_feed = true;
                this.totl_feed = 0;
                this.last_feed_num = 0;
                this.feed_cur_indx = 0;
                this.updtFeedTotals();
                this.updtFeedNavLabels();
            }
            else if (this.totl_feed == Global.mnFrm.cmCde.Big_Val
         && totlRecs < long.Parse(this.dsplySizeFeedComboBox.Text))
            {
                this.totl_feed = this.last_feed_num;
                if (totlRecs == 0)
                {
                    this.feed_cur_indx -= 1;
                    this.updtFeedTotals();
                    this.populateFeedLstVw(long.Parse(this.itemIDTextBox.Text));
                }
                else
                {
                    this.updtFeedTotals();
                }
            }
        }

        private bool shdObeyFeedEvts()
        {
            return this.obey_feed_evnts;
        }

        private void FeedPnlNavButtons(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecsFeedLabel.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.is_last_feed = false;
                this.feed_cur_indx = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.is_last_feed = false;
                this.feed_cur_indx -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.is_last_feed = false;
                this.feed_cur_indx += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.is_last_feed = true;

                this.totl_feed = Global.get_Total_Feeds(long.Parse(this.itemIDTextBox.Text));
                this.updtFeedTotals();
                this.feed_cur_indx = this.myNav1.totalGroups - 1;
            }
            this.getFeedPnlData();
        }

        private void editFeedMenuItem_Click(object sender, EventArgs e)
        {
            this.editFeedButton_Click(this.editFeedButton, e);
        }

        private void delFeedMenuItem_Click(object sender, EventArgs e)
        {
            this.deleteFeedButton_Click(this.deleteFeedButton, e);
        }

        private void exprtFeedItmsMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.feedItemsListView);
        }

        private void rfrshFeedItmMenuItem_Click(object sender, EventArgs e)
        {
            this.loadFeedItmsPanel();
        }

        private void exptValExclTmpMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtItemsValTmp();
        }

        private void imptValExclTmpltMenuItem_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[26]) == false)
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
                Global.mnFrm.cmCde.imprtItemsValTmp(this.openFileDialog1.FileName);
            }
            this.populateItmListVw();
        }

        private void vwSQLItmButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(this.itm_SQL, 8);
        }

        private void positionFeedTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                this.FeedPnlNavButtons(this.movePreviousFeedButton, ex);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
            {
                this.FeedPnlNavButtons(this.moveNextFeedButton, ex);
            }
        }

        private void positionPsblTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                this.PyItmPnlNavButtons(this.movePreviousPsblButton, ex);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
            {
                this.PyItmPnlNavButtons(this.moveNextPsblButton, ex);
            }
        }

        private void searchForItmTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.goItmButton_Click(this.goItmButton, ex);
            }
        }

        private void positionItmTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                this.ItmPnlNavButtons(this.movePreviousItmButton, ex);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
            {
                this.ItmPnlNavButtons(this.moveNextItmButton, ex);
            }
        }

        private void delItmButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[28]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }

            if (this.itemIDTextBox.Text == "" || this.itemIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select the Pay Item to DELETE!", 0);
                return;
            }
            //if (Global.isItmInUse(int.Parse(this.itemIDTextBox.Text)) == true)
            //{
            //  Global.mnFrm.cmCde.showMsg("This Pay Item is in Use!", 0);
            //  return;
            //}
            if (Global.isItmInUse(int.Parse(this.itemIDTextBox.Text)) == true)
            {
                Global.mnFrm.cmCde.showMsg("This Pay Item is in Use!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Pay Item?" +
         "\r\nThis action cannot be undone!", 1) == DialogResult.No)
            {
                Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            Global.deletePayItm(int.Parse(this.itemIDTextBox.Text), this.itemNameTextBox.Text);
            this.loadBnftsPanel();
        }

        private void recHstryItmButton_Click(object sender, EventArgs e)
        {
            if (this.itemIDTextBox.Text == "-1"
         || this.itemIDTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(
              Global.get_Itm_Rec_Hstry(int.Parse(this.itemIDTextBox.Text)), 7);
        }

        private void addItmMenuItem_Click(object sender, EventArgs e)
        {
            this.addItmButton_Click(this.addItmButton, e);
        }

        private void editItmMenuItem_Click(object sender, EventArgs e)
        {
            this.editItmButton_Click(this.editItmButton, e);
        }

        private void delItmMenuItem_Click(object sender, EventArgs e)
        {
            this.delItmButton_Click(this.delItmButton, e);
        }

        private void rfrshItmMenuItem_Click(object sender, EventArgs e)
        {
            this.goItmButton_Click(this.goItmButton, e);
        }

        private void rcHstryItmMenuItem_Click(object sender, EventArgs e)
        {
            this.recHstryItmButton_Click(this.recHstryItmButton, e);
        }

        private void vwSQLItmMenuItem_Click(object sender, EventArgs e)
        {
            this.vwSQLItmButton_Click(this.vwSQLItmButton, e);
        }

        private void refreshPValsMenuItem_Click(object sender, EventArgs e)
        {
            this.refreshValButton_Click(this.refreshValButton, e);
        }

        private void recHstryPValsMenuItem_Click(object sender, EventArgs e)
        {
            if (this.itmPssblValDataGridView.CurrentCell != null
         && this.itmPssblValDataGridView.SelectedRows.Count <= 0)
            {
                this.itmPssblValDataGridView.Rows[this.itmPssblValDataGridView.CurrentCell.RowIndex].Selected = true;
            }
            if (this.itmPssblValDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(Global.get_Pval_Rec_Hstry(long.Parse(
              this.itmPssblValDataGridView.SelectedRows[0].Cells[2].Value.ToString())), 7);
        }

        private void vwSQLPValsMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(this.itmPval_SQL, 8);
        }

        private void rcHstryFeedItmsMenuItem_Click(object sender, EventArgs e)
        {
            if (this.feedItemsListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(Global.get_Feed_Rec_Hstry(long.Parse(
              this.feedItemsListView.SelectedItems[0].SubItems[4].Text)), 7);
        }

        private void vwSQLFeedItmsMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(this.feed_SQL, 8);
        }

        private void usesSQLCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shdObeyItmEvts() == false
             || beenToCheckBx == true)
            {
                beenToCheckBx = false;
                return;
            }
            beenToCheckBx = true;
            if (this.additm == false && this.edititm == false)
            {
                this.usesSQLCheckBox.Checked = !this.usesSQLCheckBox.Checked;
            }
        }

        private void isEnabledItmCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shdObeyItmEvts() == false
             || beenToCheckBx == true)
            {
                beenToCheckBx = false;
                return;
            }
            beenToCheckBx = true;
            if (this.additm == false && this.edititm == false)
            {
                this.isEnabledItmCheckBox.Checked = !this.isEnabledItmCheckBox.Checked;
            }
        }

        //private void showOnPySlpCheckBox_CheckedChanged(object sender, EventArgs e)
        //{
        //  if (this.shdObeyItmEvts() == false
        //   || beenToCheckBx == true)
        //  {
        //    beenToCheckBx = false;
        //    return;
        //  }
        //  beenToCheckBx = true;
        //  if (this.additm == false && this.edititm == false)
        //  {
        //    this.showOnPySlpCheckBox.Checked = !this.showOnPySlpCheckBox.Checked;
        //  }
        //}

        private void locClassButton_Click(object sender, EventArgs e)
        {
            if (this.additm == false && this.edititm == false)
            {
                Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                return;
            }
            int[] selVals = new int[1];
            selVals[0] = Global.mnFrm.cmCde.getPssblValID(this.locClassTextBox.Text,
              Global.mnFrm.cmCde.getLovID("Pay Item Classifications"));
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Pay Item Classifications"), ref selVals,
                true, false,
             this.srchWrd, "Both", true);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.locClassTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
                }
            }
        }

        private void addFeedButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[26]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.itemIDTextBox.Text == "-1"
              || this.itemIDTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please select a Saved Item First!", 0);
                return;
            }
            if (this.usesSQLCheckBox.Checked == true && this.itmMajTypComboBox.Text == "Balance Item")
            {
                Global.mnFrm.cmCde.showMsg("Cannot add feed Items to a Balance Item \r\nwhose Balance is Generated Dynamically!", 0);
                return;
            }

            addBalItmDiag nwDiag = new addBalItmDiag();
            nwDiag.orgID = Global.mnFrm.cmCde.Org_id;
            if (this.itmMajTypComboBox.Text == "Balance Item")
            {
                nwDiag.onlyBalsItms = false;
            }
            else
            {
                nwDiag.onlyBalsItms = true;
            }

            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
                if (this.itmMajTypComboBox.Text == "Balance Item")
                {
                    bool res = Global.doesItmFeedExists(long.Parse(nwDiag.itemIDTextBox.Text),
               long.Parse(this.itemListView.SelectedItems[0].SubItems[2].Text));
                    if (res == false)
                    {
                        Global.createItmFeed(long.Parse(nwDiag.itemIDTextBox.Text),
                        long.Parse(this.itemListView.SelectedItems[0].SubItems[2].Text),
                                     nwDiag.addSubComboBox.Text,
                                     (double)nwDiag.scaleFctrNumUpDown.Value);
                    }
                }
                else
                {
                    bool res = Global.doesItmFeedExists(
                      long.Parse(this.itemListView.SelectedItems[0].SubItems[2].Text),
               long.Parse(nwDiag.itemIDTextBox.Text));
                    if (res == false)
                    {
                        Global.createItmFeed(long.Parse(this.itemListView.SelectedItems[0].SubItems[2].Text),
                             long.Parse(nwDiag.itemIDTextBox.Text), nwDiag.addSubComboBox.Text, (double)nwDiag.scaleFctrNumUpDown.Value);
                    }
                }
                this.loadFeedItmsPanel();
            }
        }

        private void editFeedButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[27]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.feedItemsListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select an Item First!", 0);
                return;
            }
            addBalItmDiag nwDiag = new addBalItmDiag();
            nwDiag.orgID = Global.mnFrm.cmCde.Org_id;
            nwDiag.addSubComboBox.SelectedItem = this.feedItemsListView.SelectedItems[0].SubItems[2].Text;
            nwDiag.itemIDTextBox.Text = this.feedItemsListView.SelectedItems[0].SubItems[3].Text;

            nwDiag.itmNameTextBox.Text = this.feedItemsListView.SelectedItems[0].SubItems[1].Text;
            nwDiag.itmNameButton.Enabled = false;
            if (this.itmMajTypComboBox.Text == "Balance Item")
            {
                nwDiag.onlyBalsItms = false;
            }
            else
            {
                nwDiag.onlyBalsItms = true;
            }
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
                if (this.itmMajTypComboBox.Text == "Balance Item")
                {
                    Global.updateItmFeed(long.Parse(this.feedItemsListView.SelectedItems[0].SubItems[4].Text),
               long.Parse(this.feedItemsListView.SelectedItems[0].SubItems[3].Text),
               long.Parse(this.itemIDTextBox.Text), nwDiag.addSubComboBox.Text, (double)nwDiag.scaleFctrNumUpDown.Value);
                }
                else
                {
                    Global.updateItmFeed(long.Parse(
                      this.feedItemsListView.SelectedItems[0].SubItems[4].Text),
               long.Parse(this.itemIDTextBox.Text),
               long.Parse(this.feedItemsListView.SelectedItems[0].SubItems[3].Text),
               nwDiag.addSubComboBox.Text, (double)nwDiag.scaleFctrNumUpDown.Value);
                }
                this.loadFeedItmsPanel();
            }
        }

        private void deleteFeedButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[28]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.feedItemsListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the Row(s) to delete!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to delete the" +
                "\r\nselected Item Feed(s)?", 1) == DialogResult.No)
            {
                Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            for (int i = 0; i < this.feedItemsListView.SelectedItems.Count; i++)
            {
                Global.deleteItmFeeds(long.Parse(this.feedItemsListView.SelectedItems[i].SubItems[4].Text),
                  this.feedItemsListView.SelectedItems[i].SubItems[1].Text);
            }
            this.loadFeedItmsPanel();
        }
        #endregion
        #region "ASSIGNED BENEFITS & CONTRIBUTIONS..."
        private void loadPyItmsPanelPrs()
        {
            this.obey_pyitm_evntsPrs = false;
            int dsply = 0;
            if (this.dsplySizePyItmComboBoxPrs.Text == ""
             || int.TryParse(this.dsplySizePyItmComboBoxPrs.Text, out dsply) == false)
            {
                this.dsplySizePyItmComboBoxPrs.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            //this.groupBox8.Height = this.g.Bottom - this.toolStrip4.Bottom - 50;
            this.pyitm_cur_indxPrs = 0;
            this.is_last_pyitmPrs = false;
            this.totl_pyitmPrs = Global.mnFrm.cmCde.Big_Val;
            this.getPyItmPnlDataPrs();
            this.obey_pyitm_evntsPrs = true;
        }

        private void getPyItmPnlDataPrs()
        {
            this.updtPyItmTotalsPrs();
            if (this.prsNamesListView.SelectedItems.Count > 0)
            {
                this.populatePyItmGrdVwPrs(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
            }
            else
            {
                this.populatePyItmGrdVwPrs(-10000000010);
            }

            this.updtPyItmNavLabelsPrs();
        }

        private void updtPyItmTotalsPrs()
        {
            this.myNav.FindNavigationIndices(
             long.Parse(this.dsplySizePyItmComboBoxPrs.Text), this.totl_pyitmPrs);
            if (this.pyitm_cur_indxPrs >= this.myNav.totalGroups)
            {
                this.pyitm_cur_indxPrs = this.myNav.totalGroups - 1;
            }
            if (this.pyitm_cur_indxPrs < 0)
            {
                this.pyitm_cur_indxPrs = 0;
            }
            this.myNav.currentNavigationIndex = this.pyitm_cur_indxPrs;
        }

        private void updtPyItmNavLabelsPrs()
        {
            this.moveFirstPyItmButtonPrs.Enabled = this.myNav.moveFirstBtnStatus();
            this.movePreviousPyItmButtonPrs.Enabled = this.myNav.movePrevBtnStatus();
            this.moveNextPyItmButtonPrs.Enabled = this.myNav.moveNextBtnStatus();
            this.moveLastPyItmButtonPrs.Enabled = this.myNav.moveLastBtnStatus();
            this.positionPyItmTextBoxPrs.Text = this.myNav.displayedRecordsNumbers();
            if (this.is_last_pyitmPrs == true ||
             this.totl_pyitmPrs != Global.mnFrm.cmCde.Big_Val)
            {
                this.totalRecsPyItmLabelPrs.Text = this.myNav.totalRecordsLabel();
            }
            else
            {
                this.totalRecsPyItmLabelPrs.Text = "of Total";
            }
        }

        private void populatePyItmGrdVwPrs(long prsnID)
        {
            this.obey_pyitm_evntsPrs = false;
            string dateStr = DateTime.ParseExact(
         Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");

            DataSet dtst = Global.getAllBnftsPrs(this.pyitm_cur_indxPrs,
             int.Parse(this.dsplySizePyItmComboBoxPrs.Text), prsnID);
            this.itmPrsPyValDataGridView.Rows.Clear();
            this.itmPrsPyValDataGridView.RowCount = dtst.Tables[0].Rows.Count;
            this.itmPrsPyValDataGridView.ReadOnly = true;
            this.itmPrsPyValDataGridView.DefaultCellStyle.BackColor = Color.Gainsboro;
            this.itmPrsPyValDataGridView.DefaultCellStyle.ForeColor = Color.Black;
            this.itmPrsPyValDataGridView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //this.savePostnButton.Enabled = false;
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.last_pyitm_numPrs = this.myNav.startIndex() + i;
                this.itmPrsPyValDataGridView.Rows[i].HeaderCell.Value = (this.myNav.startIndex() + 1).ToString();
                string itmmajtyp = Global.mnFrm.cmCde.getItmMajType(long.Parse(dtst.Tables[0].Rows[i][0].ToString()));
                Object[] cellDesc = new Object[9];
                cellDesc[0] = Global.mnFrm.cmCde.getItmName(long.Parse(dtst.Tables[0].Rows[i][0].ToString()));
                cellDesc[1] = Global.mnFrm.cmCde.getItmValName(long.Parse(dtst.Tables[0].Rows[i][1].ToString()));
                if (itmmajtyp == "Balance Item")
                {
                    cellDesc[0] = cellDesc[0].ToString().ToUpper();
                    cellDesc[1] = cellDesc[0].ToString().ToUpper();
                }
                if (itmmajtyp == "Balance Item" && this.shwAmntCheckBoxPrs.Checked == true)
                {
                    cellDesc[2] = Global.getBlsItmLtstDailyBalsPrs(
                            long.Parse(dtst.Tables[0].Rows[i][0].ToString()),
                          prsnID, dateStr.Substring(0, 11)).ToString("#,##0.00");
                    //if (double.Parse(cellDesc[2].ToString()) == 0)
                    //{
                    //  string valSQL = Global.mnFrm.cmCde.getItmValSQL(long.Parse(dtst.Tables[0].Rows[i][1].ToString()));
                    //  if (valSQL != "")
                    //  {
                    //    cellDesc[2] = Global.mnFrm.cmCde.exctItmValSQL(valSQL, prsnID,
                    //      Global.mnFrm.cmCde.Org_id, dateStr).ToString("#,##0.00");
                    //  }
                    //}
                    //else
                    //{
                    //  cellDesc[2] = double.Parse(cellDesc[2].ToString()).ToString("#,##0.00");
                    //}
                }
                else
                {
                    //string valSQL = Global.mnFrm.cmCde.getItmValSQL(long.Parse(dtst.Tables[0].Rows[i][1].ToString()));
                    //if (valSQL == "" && this.shwAmntCheckBox.Checked == true)
                    //{
                    //  cellDesc[2] = Global.mnFrm.cmCde.getItmValueAmnt(long.Parse(dtst.Tables[0].Rows[i][1].ToString()));
                    //}
                    //else if (this.shwAmntCheckBox.Checked == true)
                    //{
                    //  cellDesc[2] = Global.mnFrm.cmCde.exctItmValSQL(valSQL, prsnID,
                    //    Global.mnFrm.cmCde.Org_id, dateStr).ToString("#,##0.00");
                    //}
                    //else
                    //{
                    //}
                    cellDesc[2] = "-";
                }
                cellDesc[3] = dtst.Tables[0].Rows[i][2].ToString();
                cellDesc[4] = dtst.Tables[0].Rows[i][3].ToString();
                cellDesc[5] = dtst.Tables[0].Rows[i][1].ToString();
                cellDesc[6] = dtst.Tables[0].Rows[i][0].ToString();
                cellDesc[7] = dtst.Tables[0].Rows[i][4].ToString();
                cellDesc[8] = prsnID;
                this.itmPrsPyValDataGridView.Rows[i].SetValues(cellDesc);
            }

            this.itmPrsPyValDataGridView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.correctPyItmNavLblsPrs(dtst);
            this.obey_pyitm_evntsPrs = true;
        }

        private void correctPyItmNavLblsPrs(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.pyitm_cur_indxPrs == 0 && totlRecs == 0)
            {
                this.is_last_pyitmPrs = true;
                this.totl_pyitmPrs = 0;
                this.last_pyitm_numPrs = 0;
                this.pyitm_cur_indxPrs = 0;
                this.updtPyItmTotalsPrs();
                this.updtPyItmNavLabelsPrs();
            }
            else if (this.totl_pyitmPrs == Global.mnFrm.cmCde.Big_Val
          && totlRecs < long.Parse(this.dsplySizePyItmComboBoxPrs.Text))
            {
                this.totl_pyitmPrs = this.last_pyitm_numPrs;
                if (totlRecs == 0)
                {
                    this.pyitm_cur_indxPrs -= 1;
                    this.updtPyItmTotalsPrs();
                    this.populatePyItmGrdVwPrs(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
                }
                else
                {
                    this.updtPyItmTotalsPrs();
                }
            }
        }

        private bool shdObeyPyItmEvtsPrs()
        {
            return this.obey_pyitm_evntsPrs;
        }

        private void PyItmPnlNavButtonsPrs(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecsPyItmLabelPrs.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.is_last_pyitmPrs = false;
                this.pyitm_cur_indxPrs = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.is_last_pyitmPrs = false;
                this.pyitm_cur_indxPrs -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.is_last_pyitmPrs = false;
                this.pyitm_cur_indxPrs += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.is_last_pyitmPrs = true;

                this.totl_pyitmPrs = Global.get_Total_BnftsPrs(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
                this.updtPyItmTotalsPrs();
                this.pyitm_cur_indxPrs = this.myNav.totalGroups - 1;
            }
            this.getPyItmPnlDataPrs();
        }

        private void addValButton_ClickPrs(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[13]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.prsNamesListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Person First!", 0);
                return;
            }
            addBnftsDiag nwDiag = new addBnftsDiag();
            string dateStr = DateTime.ParseExact(
         Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
            nwDiag.vldStrtDteTextBox.Text = dateStr.Substring(0, 11);
            nwDiag.vldEndDteTextBox.Text = "31-Dec-4000";
            DialogResult dgres = nwDiag.ShowDialog();
            if (dgres == DialogResult.OK)
            {
                Global.createBnftsPrs(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text),
                 long.Parse(nwDiag.itemIDTextBox.Text), long.Parse(nwDiag.itmValIDTextBox.Text),
                 nwDiag.vldStrtDteTextBox.Text, nwDiag.vldEndDteTextBox.Text);
                this.loadPyItmsPanelPrs();
            }
        }

        private void editValButton_ClickPrs(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[14]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if ((this.prsNamesListView.SelectedItems.Count <= 0))
            {
                Global.mnFrm.cmCde.showMsg("Please select a Person First!", 0);
                return;
            }
            if (this.itmPrsPyValDataGridView.CurrentCell != null
         && this.itmPrsPyValDataGridView.SelectedRows.Count <= 0)
            {
                this.itmPrsPyValDataGridView.Rows[this.itmPrsPyValDataGridView.CurrentCell.RowIndex].Selected = true;
            }

            if (this.itmPrsPyValDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the Row to Edit!", 0);
                return;
            }
            addBnftsDiag nwDiag = new addBnftsDiag();
            nwDiag.itemIDTextBox.Text = this.itmPrsPyValDataGridView.SelectedRows[0].Cells[6].Value.ToString();
            nwDiag.itmValIDTextBox.Text = this.itmPrsPyValDataGridView.SelectedRows[0].Cells[5].Value.ToString();
            nwDiag.itmNameTextBox.Text = this.itmPrsPyValDataGridView.SelectedRows[0].Cells[0].Value.ToString();
            nwDiag.itmValNameTextBox.Text = this.itmPrsPyValDataGridView.SelectedRows[0].Cells[1].Value.ToString();
            nwDiag.vldStrtDteTextBox.Text = this.itmPrsPyValDataGridView.SelectedRows[0].Cells[3].Value.ToString();
            nwDiag.vldEndDteTextBox.Text = this.itmPrsPyValDataGridView.SelectedRows[0].Cells[4].Value.ToString();
            nwDiag.itmNameButton.Enabled = false;

            DialogResult dgres = nwDiag.ShowDialog();
            if (dgres == DialogResult.OK)
            {
                Global.updateBnftsPrs(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text)
                 , long.Parse(this.itmPrsPyValDataGridView.SelectedRows[0].Cells[7].Value.ToString()),
            long.Parse(nwDiag.itmValIDTextBox.Text), nwDiag.vldStrtDteTextBox.Text, nwDiag.vldEndDteTextBox.Text);
            }
            this.loadPyItmsPanelPrs();
        }

        private void refreshValButton_ClickPrs(object sender, EventArgs e)
        {
            if ((this.prsNamesListView.SelectedItems.Count <= 0))
            {
                Global.mnFrm.cmCde.showMsg("Please select a Person First!", 0);
                return;
            }
            if (this.prsNamesListView.SelectedItems.Count > 0)
            {
                this.loadPyItmsPanelPrs();
            }
        }

        private void delValButton_ClickPrs(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[15]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if ((this.prsNamesListView.SelectedItems.Count <= 0))
            {
                Global.mnFrm.cmCde.showMsg("Please select a Person First!", 0);
                return;
            }
            if (this.itmPrsPyValDataGridView.CurrentCell != null && this.itmPrsPyValDataGridView.SelectedRows.Count <= 0)
            {
                this.itmPrsPyValDataGridView.Rows[this.itmPrsPyValDataGridView.CurrentCell.RowIndex].Selected = true;
            }
            if (this.itmPrsPyValDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the Row(s) to delete!", 0);
                return;
            }

            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to delete the" +
             "\r\nselected Assigned Pay Item(s)?", 1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            string dateStr = Global.mnFrm.cmCde.getFrmtdDB_Date_time();
            for (int i = 0; i < this.itmPrsPyValDataGridView.SelectedRows.Count; i++)
            {
                /*Global.isPrsnItmInUse(int.Parse(
                  this.itmPrsPyValDataGridView.SelectedRows[i].Cells[6].Value.ToString()),
                  long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text)) == true
                  ||*/
                if (
                Global.getBlsItmLtstDailyBalsPrs(
                          int.Parse(this.itmPrsPyValDataGridView.SelectedRows[i].Cells[6].Value.ToString()),
                        long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text), dateStr.Substring(0, 11)) > 0)
                {
                    Global.mnFrm.cmCde.showMsg("Balance Items with Balances cannot be deleted!", 0);
                    //return;
                }
                else
                {
                    Global.deletePayItmPrs(long.Parse(
                      this.itmPrsPyValDataGridView.SelectedRows[i].Cells[7].Value.ToString()),
                      this.prsNamesListView.SelectedItems[0].SubItems[1].Text);
                }
            }
            this.loadPyItmsPanelPrs();
        }

        private void exptItmMenuItem_ClickPrs(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.itmPrsPyValDataGridView);
        }

        private void positionPyItmTextBox_KeyDownPrs(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                this.PyItmPnlNavButtonsPrs(this.movePreviousPyItmButtonPrs, ex);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
            {
                this.PyItmPnlNavButtonsPrs(this.moveNextPyItmButtonPrs, ex);
            }
        }

        private void grpAsgnmntsButton_Click(object sender, EventArgs e)
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
            if (this.pymntTabControl.SelectedTab == this.payTabPage)
            {
                nwDiag.pyItmSetID = int.Parse(this.itmStIDMnlTextBox.Text);
            }
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
            if (this.pymntTabControl.SelectedTab == this.payTabPage)
            {
                this.goItmButtonNw.PerformClick();
            }
            else
            {
                this.refreshValButton_ClickPrs(this.refreshValButtonPrs, e);
            }

        }

        private void addItmMenuItem_ClickPrs(object sender, EventArgs e)
        {
            this.addValButton_ClickPrs(this.addValButtonPrs, e);
        }

        private void editItmMenuItem_ClickPrs(object sender, EventArgs e)
        {
            this.editValButton_ClickPrs(this.editValButtonPrs, e);
        }

        private void delItmMenuItem_ClickPrs(object sender, EventArgs e)
        {
            this.delValButton_ClickPrs(this.delValButtonPrs, e);
        }

        private void rfrshItmMenuItem_ClickPrs(object sender, EventArgs e)
        {
            this.refreshValButton_ClickPrs(this.refreshValButtonPrs, e);
        }

        private void rcHstryItmMenuItem_ClickPrs(object sender, EventArgs e)
        {
            if (this.itmPrsPyValDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(Global.get_PyItm_Rec_HstryPrs(
              long.Parse(this.itmPrsPyValDataGridView.SelectedRows[0].Cells[7].Value.ToString())), 6);
        }

        private void vwSQLItmMenuItem_ClickPrs(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(Global.mnFrm.pyitm_SQLPrs, 5);
        }
        #endregion
        #region "PERSON BANK ACCOUNTS..."
        private void populateAccounts(long prsnID)
        {
            DataSet dtst = Global.getAllAccounts(prsnID);
            this.bankDataGridView.Rows.Clear();
            this.bankDataGridView.RowCount = dtst.Tables[0].Rows.Count;
            this.bankDataGridView.ReadOnly = true;
            this.bankDataGridView.DefaultCellStyle.BackColor = Color.Gainsboro;
            this.bankDataGridView.DefaultCellStyle.ForeColor = Color.Black;
            this.saveBankButton.Enabled = false;
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.bankDataGridView.Rows[i].HeaderCell.Value = (i + 1).ToString();
                Object[] cellDesc = new Object[9];
                cellDesc[0] = dtst.Tables[0].Rows[i][0].ToString();
                cellDesc[1] = dtst.Tables[0].Rows[i][1].ToString();
                cellDesc[2] = dtst.Tables[0].Rows[i][2].ToString();
                cellDesc[3] = dtst.Tables[0].Rows[i][3].ToString();
                cellDesc[4] = dtst.Tables[0].Rows[i][4].ToString();
                cellDesc[5] = dtst.Tables[0].Rows[i][5].ToString();
                cellDesc[6] = dtst.Tables[0].Rows[i][6].ToString();
                cellDesc[7] = dtst.Tables[0].Rows[i][7].ToString();
                cellDesc[8] = prsnID;
                this.bankDataGridView.Rows[i].SetValues(cellDesc);
            }
        }

        private void addBankButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[13]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if ((this.prsNamesListView.SelectedItems.Count <= 0))
            {
                Global.mnFrm.cmCde.showMsg("Please select a Person First!", 0);
                return;
            }
            if (this.prsNamesListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Person First!", 0);
                return;
            }
            this.saveBankButton_Click(this.saveBankButton, e);
            string dateStr = DateTime.ParseExact(
         Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
            Global.createBank(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text),
             "", "", "", "", "", 0, "Percent");
            this.populateAccounts(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
            this.bankDataGridView.DefaultCellStyle.BackColor = Color.White;
            this.bankDataGridView.ReadOnly = false;
            this.saveBankButton.Enabled = true;
        }

        private void editBankButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[14]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if ((this.prsNamesListView.SelectedItems.Count <= 0))
            {
                Global.mnFrm.cmCde.showMsg("Please select a Person First!", 0);
                return;
            }
            this.bankDataGridView.DefaultCellStyle.BackColor = Color.White;
            this.bankDataGridView.DefaultCellStyle.ForeColor = Color.Black;
            this.bankDataGridView.ReadOnly = false;
            this.saveBankButton.Enabled = true;
        }

        private void delBankButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[15]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if ((this.prsNamesListView.SelectedItems.Count <= 0))
            {
                Global.mnFrm.cmCde.showMsg("Please select a Person First!", 0);
                return;
            }
            if (this.bankDataGridView.CurrentCell != null && this.bankDataGridView.SelectedRows.Count <= 0)
            {
                this.bankDataGridView.Rows[this.bankDataGridView.CurrentCell.RowIndex].Selected = true;
            }
            if (this.bankDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the Row(s) to delete!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to delete the\r\nselected Bank Accounts?", 1) == DialogResult.No)
            {
                Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            for (int i = 0; i < this.bankDataGridView.SelectedRows.Count; i++)
            {
                Global.deleteAccount(
                  long.Parse(this.bankDataGridView.SelectedRows[i].Cells[7].Value.ToString()),
                  this.prsNamesListView.SelectedItems[0].SubItems[1].Text);
            }
            this.populateAccounts(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
        }

        private void refreshBankButton_Click(object sender, EventArgs e)
        {
            if ((this.prsNamesListView.SelectedItems.Count <= 0))
            {
                Global.mnFrm.cmCde.showMsg("Please select a Person First!", 0);
                return;
            }
            if (this.prsNamesListView.SelectedItems.Count > 0)
            {
                this.populateAccounts(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
            }
        }

        private void saveBankButton_Click(object sender, EventArgs e)
        {
            if ((this.prsNamesListView.SelectedItems.Count <= 0))
            {
                Global.mnFrm.cmCde.showMsg("Please select a Person First!", 0);
                return;
            }
            string orgCur = Global.mnFrm.cmCde.getPssblValNm(
              Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id));
            for (int i = 0; i < this.bankDataGridView.Rows.Count; i++)
            {
                double num = 0;
                bool isdbl = double.TryParse(this.bankDataGridView.Rows[i].Cells[5].Value.ToString(), out num);
                if (!isdbl)
                {
                    Global.mnFrm.cmCde.showMsg("Invalid Figure for Net Pay Portion!", 0);
                    return;
                }
                if (this.bankDataGridView.Rows[i].Cells[6].Value.ToString() != "Percent"
                  && this.bankDataGridView.Rows[i].Cells[6].Value.ToString() != orgCur)
                {
                    Global.mnFrm.cmCde.showMsg("Portion's UOM can Only be '" + orgCur + "' or 'Percent'!", 0);
                    return;
                }
                if (this.bankDataGridView.Rows[i].Cells[6].Value.ToString().ToLower() == "percent"
                  && num > 100)
                {
                    Global.mnFrm.cmCde.showMsg("Net Pay Portion cannot be greater than 100 if UOM is Percent!", 0);
                    return;
                }
                Global.updateAccount(long.Parse(this.bankDataGridView.Rows[i].Cells[8].Value.ToString()),
                 long.Parse(this.bankDataGridView.Rows[i].Cells[7].Value.ToString()),
                this.bankDataGridView.Rows[i].Cells[1].Value.ToString(),
                this.bankDataGridView.Rows[i].Cells[0].Value.ToString(),
                this.bankDataGridView.Rows[i].Cells[2].Value.ToString(),
                this.bankDataGridView.Rows[i].Cells[3].Value.ToString(),
                this.bankDataGridView.Rows[i].Cells[4].Value.ToString(),
                double.Parse(this.bankDataGridView.Rows[i].Cells[5].Value.ToString()),
                this.bankDataGridView.Rows[i].Cells[6].Value.ToString());
            }
            this.bankDataGridView.DefaultCellStyle.BackColor = Color.Gainsboro;
            this.bankDataGridView.ReadOnly = true;
            this.saveBankButton.Enabled = false;
        }

        private void bankDataGridView_CellBeginEdit(object sender, System.Windows.Forms.DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                //Banks
                int[] selVals = new int[1];
                int curval = -1;
                selVals[0] = curval;
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                 Global.mnFrm.cmCde.getLovID("Banks"), ref selVals, true, true);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.bankDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
                    }
                }
                this.bankDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
            }
            else if (e.ColumnIndex == 1)
            {
                //Bank Branches
                int[] selVals = new int[1];
                int curval = -1;
                selVals[0] = curval;
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                 Global.mnFrm.cmCde.getLovID("Bank Branches"), ref selVals, true, true);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.bankDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
                    }
                }
                this.bankDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
            }
            else if (e.ColumnIndex == 4)
            {
                //Bank Account Types
                int[] selVals = new int[1];
                int curval = -1;
                selVals[0] = curval;
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                 Global.mnFrm.cmCde.getLovID("Bank Account Types"), ref selVals, true, true);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.bankDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
                    }
                }
                this.bankDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
            }
        }

        private void exptBankMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.bankDataGridView);
        }

        private void exptBnkTmpButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtPsnBanksTmp();
        }

        private void imptBnkTmpButton_Click(object sender, EventArgs e)
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
                Global.mnFrm.cmCde.imprtPsnBanksTmp(this.openFileDialog1.FileName);
            }
            this.loadPersBanksPanel();
        }

        private void addBankMenuItem_Click(object sender, EventArgs e)
        {
            this.addBankButton_Click(this.addBankButton, e);
        }

        private void editBankMenuItem_Click(object sender, EventArgs e)
        {
            this.editBankButton_Click(this.editBankButton, e);
        }

        private void delBankMenuItem_Click(object sender, EventArgs e)
        {
            this.delBankButton_Click(this.delBankButton, e);
        }

        private void refreshBankMenuItem_Click(object sender, EventArgs e)
        {
            this.refreshBankButton_Click(this.refreshBankButton, e);
        }

        private void recHstryBankMenuItem_Click(object sender, EventArgs e)
        {
            if (this.bankDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(Global.get_Bank_Rec_Hstry(
              long.Parse(this.bankDataGridView.SelectedRows[0].Cells[7].Value.ToString())), 6);
        }

        private void vvwSQLBankMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(Global.mnFrm.bank_SQL, 5);
        }
        #endregion
        #region "GLOBAL VALUES..."
        private void loadGBVPanel()
        {
            this.obey_gbv_evnts = false;
            if (this.searchInGBVComboBox.SelectedIndex < 0)
            {
                this.searchInGBVComboBox.SelectedIndex = 0;
            }
            if (this.searchForGBVTextBox.Text.Contains("%") == false)
            {
                this.searchForGBVTextBox.Text = "%" + this.searchForGBVTextBox.Text.Replace(" ", "%") + "%";
            }
            if (this.searchForGBVTextBox.Text == "%%")
            {
                this.searchForGBVTextBox.Text = "%";
            }
            int dsply = 0;
            if (this.dsplySizeGBVComboBox.Text == ""
              || int.TryParse(this.dsplySizeGBVComboBox.Text, out dsply) == false)
            {
                this.dsplySizeGBVComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            //this.groupBox8.Height = this.g.Bottom - this.toolStrip4.Bottom - 50;
            this.is_last_gbv = false;
            this.totl_gbv = Global.mnFrm.cmCde.Big_Val;
            this.getGBVPnlData();
            this.obey_gbv_evnts = true;
        }

        private void getGBVPnlData()
        {
            this.updtGBVTotals();
            this.populateGBVListVw();
            this.updtGBVNavLabels();
        }

        private void updtGBVTotals()
        {
            Global.mnFrm.cmCde.navFuncts.FindNavigationIndices(
              long.Parse(this.dsplySizeGBVComboBox.Text), this.totl_gbv);
            if (this.gbv_cur_indx >= Global.mnFrm.cmCde.navFuncts.totalGroups)
            {
                this.gbv_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            if (this.gbv_cur_indx < 0)
            {
                this.gbv_cur_indx = 0;
            }
            Global.mnFrm.cmCde.navFuncts.currentNavigationIndex = this.gbv_cur_indx;
        }

        private void updtGBVNavLabels()
        {
            this.moveFirstGBVButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveFirstBtnStatus();
            this.movePreviousGBVButton.Enabled = Global.mnFrm.cmCde.navFuncts.movePrevBtnStatus();
            this.moveNextGBVButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveNextBtnStatus();
            this.moveLastGBVButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveLastBtnStatus();
            this.positionGBVTextBox.Text = Global.mnFrm.cmCde.navFuncts.displayedRecordsNumbers();
            if (this.is_last_gbv == true ||
              this.totl_gbv != Global.mnFrm.cmCde.Big_Val)
            {
                this.totalRecsGBVLabel.Text = Global.mnFrm.cmCde.navFuncts.totalRecordsLabel();
            }
            else
            {
                this.totalRecsGBVLabel.Text = "of Total";
            }
        }

        private void populateGBVListVw()
        {
            this.obey_gbv_evnts = false;
            DataSet dtst = Global.get_Basic_GBV(this.searchForGBVTextBox.Text,
              this.searchInGBVComboBox.Text, this.gbv_cur_indx,
              int.Parse(this.dsplySizeGBVComboBox.Text), Global.mnFrm.cmCde.Org_id);
            this.gbvListView.Items.Clear();
            this.clearGBVInfo();
            this.loadGBVDetPanel();
            if (!this.editgbv)
            {
                this.disableGBVEdit();
            }
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.last_gbv_num = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
                ListViewItem nwItem = new ListViewItem(new string[] {
    (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),
    dtst.Tables[0].Rows[i][0].ToString(),
    dtst.Tables[0].Rows[i][2].ToString(),
    dtst.Tables[0].Rows[i][4].ToString(),
    dtst.Tables[0].Rows[i][3].ToString()});
                this.gbvListView.Items.Add(nwItem);
            }
            this.correctGBVNavLbls(dtst);
            if (this.gbvListView.Items.Count > 0)
            {
                this.obey_gbv_evnts = true;
                this.gbvListView.Items[0].Selected = true;
            }
            else
            {
            }
            this.obey_gbv_evnts = true;
        }

        private void correctGBVNavLbls(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.gbv_cur_indx == 0 && totlRecs == 0)
            {
                this.is_last_gbv = true;
                this.totl_gbv = 0;
                this.last_gbv_num = 0;
                this.gbv_cur_indx = 0;
                this.updtGBVTotals();
                this.updtGBVNavLabels();
            }
            else if (this.totl_gbv == Global.mnFrm.cmCde.Big_Val
           && totlRecs < long.Parse(this.dsplySizeGBVComboBox.Text))
            {
                this.totl_gbv = this.last_gbv_num;
                if (totlRecs == 0)
                {
                    this.gbv_cur_indx -= 1;
                    this.updtGBVTotals();
                    this.populateGBVListVw();
                }
                else
                {
                    this.updtGBVTotals();
                }
            }
        }

        private bool shdObeyGBVEvts()
        {
            return this.obey_gbv_evnts;
        }

        private void GBVPnlNavButtons(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecsGBVLabel.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.is_last_gbv = false;
                this.gbv_cur_indx = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.is_last_gbv = false;
                this.gbv_cur_indx -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.is_last_gbv = false;
                this.gbv_cur_indx += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.is_last_gbv = true;
                this.totl_gbv = Global.get_Total_GBV(this.searchForGBVTextBox.Text,
                  this.searchInGBVComboBox.Text, Global.mnFrm.cmCde.Org_id);
                this.updtGBVTotals();
                this.gbv_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            this.getGBVPnlData();
        }

        private void clearGBVInfo()
        {
            this.obey_gbv_evnts = false;
            this.isEnbldGBVCheckBox.Checked = false;
            this.gbvNameTextBox.Text = "";
            this.gbvHdrIDTextBox.Text = "-1";
            this.gbvDescTextBox.Text = "";
            this.gbvDataGridView.Rows.Clear();
            this.crtraTypeComboBox.Items.Clear();
            this.obey_gbv_evnts = true;
        }

        private void prpareForGBVEdit()
        {
            this.saveGBVButton.Enabled = true;
            this.gbvNameTextBox.ReadOnly = false;
            this.gbvNameTextBox.BackColor = Color.FromArgb(255, 255, 128);
            this.gbvDescTextBox.ReadOnly = false;
            this.gbvDescTextBox.BackColor = Color.White;
            this.crtraTypeComboBox.BackColor = Color.FromArgb(255, 255, 128);
            /*Everyone
         Divisions/Groups
         Grade
         Job
         Position
         Site/Location
         Person Type*/
            object orgnlItm = null;
            if (this.crtraTypeComboBox.SelectedIndex >= 0)
            {
                orgnlItm = this.crtraTypeComboBox.SelectedItem;
            }
            this.crtraTypeComboBox.Items.Clear();
            this.crtraTypeComboBox.Items.Add("Divisions/Groups");
            this.crtraTypeComboBox.Items.Add("Grade");
            this.crtraTypeComboBox.Items.Add("Job");
            this.crtraTypeComboBox.Items.Add("Position");
            this.crtraTypeComboBox.Items.Add("Site/Location");
            this.crtraTypeComboBox.Items.Add("Person Type");
            if (orgnlItm != null)
            {
                this.crtraTypeComboBox.SelectedItem = orgnlItm;
            }
        }

        private void disableGBVEdit()
        {
            this.addgbv = false;
            this.editgbv = false;
            this.saveGBVButton.Enabled = false;
            this.addGBVButton.Enabled = this.addgbvs;
            this.editGBVButton.Enabled = this.editgbvs;
            this.delGBVButton.Enabled = this.delgbvs;
            this.addGBVDTButton.Enabled = this.editgbvs;
            this.delGBVDTButton.Enabled = this.editgbvs;
            this.gbvNameTextBox.ReadOnly = true;
            this.gbvNameTextBox.BackColor = Color.WhiteSmoke;
            this.gbvDescTextBox.ReadOnly = true;
            this.gbvDescTextBox.BackColor = Color.WhiteSmoke;
            if (this.crtraTypeComboBox.SelectedIndex >= 0)
            {
                object orgnlItm = this.crtraTypeComboBox.SelectedItem;
                this.crtraTypeComboBox.Items.Clear();
                this.crtraTypeComboBox.Items.Add(orgnlItm);
                this.crtraTypeComboBox.SelectedItem = orgnlItm;
            }
            else
            {
                this.crtraTypeComboBox.Items.Clear();
            }
            this.crtraTypeComboBox.BackColor = Color.WhiteSmoke;
        }

        private void prpareForGBVLnsEdit()
        {
            this.gbvDataGridView.ReadOnly = false;
            this.addGBVDTButton.Enabled = true;
            this.delGBVDTButton.Enabled = true;
            this.gbvDataGridView.ReadOnly = false;
            this.gbvDataGridView.Columns[0].ReadOnly = false;
            this.gbvDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            this.gbvDataGridView.Columns[1].ReadOnly = false;
            this.gbvDataGridView.Columns[1].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            this.gbvDataGridView.Columns[2].ReadOnly = true;

            this.gbvDataGridView.Columns[4].ReadOnly = false;
            this.gbvDataGridView.Columns[4].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            this.gbvDataGridView.Columns[5].ReadOnly = false;
            this.gbvDataGridView.Columns[5].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);

            this.gbvDataGridView.Columns[7].ReadOnly = false;
            this.gbvDataGridView.Columns[7].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            this.gbvDataGridView.Columns[9].ReadOnly = true;

            this.gbvDataGridView.DefaultCellStyle.ForeColor = Color.Black;
        }

        private void disableGBVLnsEdit()
        {
            this.gbvDataGridView.DefaultCellStyle.ForeColor = Color.Black;

            this.gbvDataGridView.ReadOnly = true;
            this.gbvDataGridView.Columns[0].ReadOnly = true;
            this.gbvDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.gbvDataGridView.Columns[1].ReadOnly = true;
            this.gbvDataGridView.Columns[1].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.gbvDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.WhiteSmoke;


            this.gbvDataGridView.Columns[4].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.gbvDataGridView.Columns[5].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.gbvDataGridView.Columns[7].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.gbvDataGridView.Columns[9].DefaultCellStyle.BackColor = Color.WhiteSmoke;

        }

        public void createGBVDtRows(int num)
        {
            this.gbvDataGridView.DefaultCellStyle.ForeColor = Color.Black;
            this.obey_gbvdt_evnts = false;
            if (this.crtraTypeComboBox.SelectedIndex < 0)
            {
                this.crtraTypeComboBox.SelectedIndex = 0;
            }
            for (int i = 0; i < num; i++)
            {
                this.gbvDataGridView.Rows.Insert(0, 1);
                int rowIdx = 0;// this.gbvDataGridView.RowCount - 1;
                this.gbvDataGridView.Rows[rowIdx].Cells[0].Value = this.crtraTypeComboBox.Text;
                this.gbvDataGridView.Rows[rowIdx].Cells[1].Value = "";
                this.gbvDataGridView.Rows[rowIdx].Cells[2].Value = "-1";
                this.gbvDataGridView.Rows[rowIdx].Cells[3].Value = "...";
                this.gbvDataGridView.Rows[rowIdx].Cells[4].Value = "0.00";
                this.gbvDataGridView.Rows[rowIdx].Cells[5].Value = Global.mnFrm.cmCde.getFrmtdDB_Date_time().Substring(0, 11) + " 00:00:00";
                this.gbvDataGridView.Rows[rowIdx].Cells[6].Value = "...";
                this.gbvDataGridView.Rows[rowIdx].Cells[7].Value = "31-Dec-4000 23:59:59";
                this.gbvDataGridView.Rows[rowIdx].Cells[8].Value = "...";
                this.gbvDataGridView.Rows[rowIdx].Cells[9].Value = "-1";
            }
            this.obey_gbvdt_evnts = true;
        }

        private void gbvListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.shdObeyGBVEvts() == false || this.gbvListView.SelectedItems.Count > 1)
            {
                return;
            }
            this.clearGBVInfo();
            this.loadGBVDetPanel();
            if (this.gbvListView.SelectedItems.Count == 1)
            {
                this.populateGBVDet();
            }
            else
            {

            }
        }

        private void populateGBVDet()
        {
            this.clearGBVInfo();
            if (!this.editgbv)
            {
                this.disableGBVEdit();
            }
            this.obey_gbv_evnts = false;
            this.gbvDataGridView.Rows.Clear();
            this.gbvHdrIDTextBox.Text = this.gbvListView.SelectedItems[0].SubItems[2].Text;
            this.gbvNameTextBox.Text = this.gbvListView.SelectedItems[0].SubItems[1].Text;
            this.gbvDescTextBox.Text = this.gbvListView.SelectedItems[0].SubItems[3].Text;
            this.isEnbldGBVCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(
              this.gbvListView.SelectedItems[0].SubItems[5].Text);

            this.crtraTypeComboBox.Items.Clear();
            string orgnlItm = this.gbvListView.SelectedItems[0].SubItems[4].Text;
            this.crtraTypeComboBox.Items.Add(orgnlItm);
            this.crtraTypeComboBox.SelectedItem = orgnlItm;

            this.loadGBVDetPanel();
            this.obey_gbv_evnts = true;
        }

        private void loadGBVDetPanel()
        {
            this.obey_gbvdt_evnts = false;
            int dsply = 0;
            if (this.dsplySizeGBVDTComboBox.Text == ""
             || int.TryParse(this.dsplySizeGBVDTComboBox.Text, out dsply) == false)
            {
                this.dsplySizeGBVDTComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            //this.groupBox8.Height = this.g.Bottom - this.toolStrip4.Bottom - 50;
            this.gbvdt_cur_indx = 0;
            this.is_last_gbvdt = false;
            this.last_gbvdt_num = 0;
            this.totl_gbvdt = Global.mnFrm.cmCde.Big_Val;
            this.getGBVDTPnlData();
            this.gbvDataGridView.Focus();

            this.obey_gbvdt_evnts = true;
            SendKeys.Send("{TAB}");
            System.Windows.Forms.Application.DoEvents();
            SendKeys.Send("{HOME}");
            System.Windows.Forms.Application.DoEvents();
            this.gbvListView.Focus();
        }

        private void getGBVDTPnlData()
        {
            this.updtGBVDTTotals();
            this.populateGBVDTGridVw();
            this.updtGBVDTNavLabels();
        }

        private void updtGBVDTTotals()
        {
            int dsply = 0;
            if (this.dsplySizeGBVDTComboBox.Text == ""
              || int.TryParse(this.dsplySizeGBVDTComboBox.Text, out dsply) == false)
            {
                this.dsplySizeGBVDTComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            this.myNav.FindNavigationIndices(
          long.Parse(this.dsplySizeGBVDTComboBox.Text), this.totl_gbvdt);
            if (this.gbvdt_cur_indx >= this.myNav.totalGroups)
            {
                this.gbvdt_cur_indx = this.myNav.totalGroups - 1;
            }
            if (this.gbvdt_cur_indx < 0)
            {
                this.gbvdt_cur_indx = 0;
            }
            this.myNav.currentNavigationIndex = this.gbvdt_cur_indx;
        }

        private void updtGBVDTNavLabels()
        {
            this.moveFirstGBVDTButton.Enabled = this.myNav.moveFirstBtnStatus();
            this.movePreviousGBVDTButton.Enabled = this.myNav.movePrevBtnStatus();
            this.moveNextGBVDTButton.Enabled = this.myNav.moveNextBtnStatus();
            this.moveLastGBVDTButton.Enabled = this.myNav.moveLastBtnStatus();
            this.positionGBVDTTextBox.Text = this.myNav.displayedRecordsNumbers();
            if (this.is_last_gbvdt == true ||
             this.totl_gbvdt != Global.mnFrm.cmCde.Big_Val)
            {
                this.totalRecsGBVDTLabel.Text = this.myNav.totalRecordsLabel();
            }
            else
            {
                this.totalRecsGBVDTLabel.Text = "of Total";
            }
        }

        private void populateGBVDTGridVw()
        {
            this.obey_gbvdt_evnts = false;

            DataSet dtst = Global.get_One_GBVDet(int.Parse(this.gbvHdrIDTextBox.Text),
              this.gbvdt_cur_indx,
              int.Parse(this.dsplySizeGBVDTComboBox.Text));
            this.gbvDataGridView.Rows.Clear();
            if (!this.editgbv)
            {
                this.disableGBVLnsEdit();
            }
            int rwcnt = dtst.Tables[0].Rows.Count;
            for (int i = 0; i < rwcnt; i++)
            {
                this.last_gbvdt_num = this.myNav.startIndex() + i;
                this.gbvDataGridView.RowCount += 1;
                int rowIdx = this.gbvDataGridView.RowCount - 1;

                this.gbvDataGridView.Rows[rowIdx].HeaderCell.Value = (i + 1).ToString();
                this.gbvDataGridView.Rows[rowIdx].Cells[0].Value = dtst.Tables[0].Rows[i][1].ToString();
                this.gbvDataGridView.Rows[rowIdx].Cells[1].Value = dtst.Tables[0].Rows[i][3].ToString();
                this.gbvDataGridView.Rows[rowIdx].Cells[2].Value = dtst.Tables[0].Rows[i][2].ToString();
                this.gbvDataGridView.Rows[rowIdx].Cells[3].Value = "...";

                this.gbvDataGridView.Rows[rowIdx].Cells[4].Value = dtst.Tables[0].Rows[i][4].ToString();
                this.gbvDataGridView.Rows[rowIdx].Cells[5].Value = dtst.Tables[0].Rows[i][5].ToString();
                this.gbvDataGridView.Rows[rowIdx].Cells[6].Value = "...";
                this.gbvDataGridView.Rows[rowIdx].Cells[7].Value = dtst.Tables[0].Rows[i][6].ToString();
                this.gbvDataGridView.Rows[rowIdx].Cells[8].Value = "...";
                this.gbvDataGridView.Rows[rowIdx].Cells[9].Value = dtst.Tables[0].Rows[i][0].ToString();

            }
            this.correctGBVDTNavLbls(dtst);
            this.obey_gbvdt_evnts = true;
        }

        private void correctGBVDTNavLbls(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.totl_gbvdt == Global.mnFrm.cmCde.Big_Val
          && totlRecs < long.Parse(this.dsplySizeGBVDTComboBox.Text))
            {
                this.totl_gbvdt = this.last_gbvdt_num;
                if (totlRecs == 0)
                {
                    this.gbvdt_cur_indx -= 1;
                    this.updtGBVDTTotals();
                    this.populateGBVDTGridVw();
                }
                else
                {
                    this.updtGBVDTTotals();
                }
            }
        }

        private bool shdObeyGBVDTEvts()
        {
            return this.obey_gbvdt_evnts;
        }

        private void GBVDTPnlNavButtons(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecsGBVDTLabel.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.is_last_gbvdt = false;
                this.gbvdt_cur_indx = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.is_last_gbvdt = false;
                this.gbvdt_cur_indx -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.is_last_gbvdt = false;
                this.gbvdt_cur_indx += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.is_last_gbvdt = true;
                this.totl_gbvdt = Global.get_Total_GBVDet(long.Parse(this.gbvHdrIDTextBox.Text));
                this.updtGBVDTTotals();
                this.gbvdt_cur_indx = this.myNav.totalGroups - 1;
            }
            this.getGBVDTPnlData();
        }

        private void positionGBVTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                this.GBVPnlNavButtons(this.movePreviousGBVButton, ex);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
            {
                this.GBVPnlNavButtons(this.moveNextGBVButton, ex);
            }
        }

        private void searchForGBVTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.loadGBVPanel();
            }
        }

        private void positionGBVDTTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                this.GBVDTPnlNavButtons(this.movePreviousGBVButton, ex);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
            {
                this.GBVDTPnlNavButtons(this.moveNextGBVButton, ex);
            }

        }

        private void dsplySizeGBVDTComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.loadGBVDetPanel();
            }
        }

        private void refreshGBVButton_Click(object sender, EventArgs e)
        {
            this.loadGBVPanel();
            //this.showATab(ref this.tabPage8);
        }

        private void refreshGBVDTButton_Click(object sender, EventArgs e)
        {
            this.loadGBVDetPanel();
        }

        private void rcHstryGBVDTButton_Click(object sender, EventArgs e)
        {
            if (this.gbvDataGridView.CurrentCell != null
         && this.gbvDataGridView.SelectedRows.Count <= 0)
            {
                this.gbvDataGridView.Rows[this.gbvDataGridView.CurrentCell.RowIndex].Selected = true;
            }
            if (this.gbvDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }


            Global.mnFrm.cmCde.showRecHstry(
              Global.get_GBVDT_Rec_Hstry(int.Parse(this.gbvDataGridView.SelectedRows[0].Cells[9].Value.ToString())), 7);


        }

        private void vwSQLGBVDTButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(this.gbvdt_SQL, 8);
        }

        private void rcHstryGBVButton_Click(object sender, EventArgs e)
        {
            if (this.gbvHdrIDTextBox.Text == "-1"
         || this.gbvHdrIDTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(
              Global.get_GBV_Rec_Hstry(int.Parse(this.gbvHdrIDTextBox.Text)), 7);

        }

        private void vwSQLGBVButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(this.gbv_SQL, 8);
        }

        private void addGBVButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[38]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.editGBVButton.Text == "STOP")
            {
                this.editGBVButton.PerformClick();
            }
            //this.editGBVButton.Enabled = false;
            this.clearGBVInfo();
            this.gbvDataGridView.Rows.Clear();
            this.addgbv = true;
            this.editgbv = false;
            this.prpareForGBVEdit();
            this.prpareForGBVLnsEdit();
            this.gbvNameTextBox.Focus();
            //this.addGBVButton.Enabled = false;
        }

        private void editGBVButton_Click(object sender, EventArgs e)
        {
            if (this.editGBVButton.Text == "EDIT")
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[39]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                        " this action!\nContact your System Administrator!", 0);
                    return;
                }
                if (this.gbvHdrIDTextBox.Text == "" || this.gbvHdrIDTextBox.Text == "-1")
                {
                    Global.mnFrm.cmCde.showMsg("No record to Edit!", 0);
                    return;
                }
                this.addgbv = false;
                this.editgbv = true;
                this.prpareForGBVEdit();
                this.prpareForGBVLnsEdit();
                //this.addGBVButton.Enabled = false;
                this.editGBVButton.Text = "STOP";
                this.gbvNameTextBox.Focus();
                //this.editMenuItem.Text = "STOP EDITING";
            }
            else
            {
                this.saveGBVButton.Enabled = false;
                this.addgbv = false;
                this.editgbv = false;
                this.addGBVButton.Enabled = this.addgbvs;
                this.editGBVButton.Enabled = this.editgbvs;
                this.addGBVDTButton.Enabled = this.editgbvs;
                this.delGBVDTButton.Enabled = this.editgbvs;
                this.editGBVButton.Text = "EDIT";
                //this.editMenuItem.Text = "Edit Item";
                this.disableGBVEdit();
                this.disableGBVLnsEdit();
                System.Windows.Forms.Application.DoEvents();
                this.loadGBVPanel();
            }
        }

        private void saveGBVButton_Click(object sender, EventArgs e)
        {
            if (this.addgbv == true)
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[38]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                     " this action!\nContact your System Administrator!", 0);
                    return;
                }
            }
            else
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[39]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                     " this action!\nContact your System Administrator!", 0);
                    return;
                }
            }
            if (this.gbvNameTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please enter a Global Value Name!", 0);
                return;
            }

            long oldRecID = Global.getGBVID(this.gbvNameTextBox.Text,
                Global.mnFrm.cmCde.Org_id);
            if (oldRecID > 0
             && this.addgbv == true)
            {
                Global.mnFrm.cmCde.showMsg("Global Value Name is already in use in this Organisation!", 0);
                return;
            }
            if (oldRecID > 0
             && this.editgbv == true
             && oldRecID.ToString() !=
             this.gbvHdrIDTextBox.Text)
            {
                Global.mnFrm.cmCde.showMsg("New Global Value Name is already in use in this Organisation!", 0);
                return;
            }

            if (this.crtraTypeComboBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Default Criteria Type cannot be empty!", 0);
                return;
            }


            if (this.addgbv == true)
            {
                Global.createGBVHdr(Global.mnFrm.cmCde.Org_id, this.gbvNameTextBox.Text,
                  this.gbvDescTextBox.Text, this.crtraTypeComboBox.Text,
                  this.isEnbldGBVCheckBox.Checked);

                //this.saveGBVButton.Enabled = false;
                //this.addgbv = false;
                //this.editgbv = true;
                this.editGBVButton.Enabled = this.addgbvs;
                this.addGBVButton.Enabled = this.editgbvs;

                //Global.mnFrm.cmCde.showMsg("Record Saved!", 3);
                System.Windows.Forms.Application.DoEvents();
                this.gbvHdrIDTextBox.Text = Global.getGBVID(this.gbvNameTextBox.Text,
                  Global.mnFrm.cmCde.Org_id).ToString();
                this.saveGridView(int.Parse(this.gbvHdrIDTextBox.Text));
                this.loadGBVPanel();
            }
            else if (this.editgbv == true)
            {
                Global.updateGBVHdr(int.Parse(this.gbvHdrIDTextBox.Text), this.gbvNameTextBox.Text,
                  this.gbvDescTextBox.Text, this.crtraTypeComboBox.Text,
                  this.isEnbldGBVCheckBox.Checked);

                this.saveGridView(int.Parse(this.gbvHdrIDTextBox.Text));

                if (this.gbvListView.SelectedItems.Count > 0)
                {
                    this.gbvListView.SelectedItems[0].SubItems[1].Text = this.gbvNameTextBox.Text;
                }
                // Global.mnFrm.cmCde.showMsg("Record Saved!", 3);
            }
        }

        private bool checkDtRqrmnts(int rwIdx)
        {
            this.dfltFill(rwIdx);

            if (this.gbvDataGridView.Rows[rwIdx].Cells[2].Value.ToString() == "-1")
            {
                return false;
            }
            if (this.gbvDataGridView.Rows[rwIdx].Cells[4].Value.ToString() == "")
            {
                return false;
            }

            if (this.gbvDataGridView.Rows[rwIdx].Cells[5].Value.ToString() == "")
            {
                return false;
            }
            if (this.gbvDataGridView.Rows[rwIdx].Cells[7].Value.ToString() == "")
            {
                return false;
            }
            return true;
        }

        private void saveGridView(int gbvHdrID)
        {
            int svd = 0;
            if (this.gbvDataGridView.Rows.Count > 0)
            {
                //this.itemsDataGridView.Rows[0].Cells[1].Selected = true;
                this.gbvDataGridView.EndEdit();
                //System.Windows.Forms.Application.DoEvents();
            }
            this.obey_gbvdt_evnts = false;
            for (int i = 0; i < this.gbvDataGridView.Rows.Count; i++)
            {
                if (!this.checkDtRqrmnts(i))
                {
                    this.gbvDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(255, 100, 100);
                    continue;
                }
                else
                {
                    //Check if Doc Ln Rec Exists
                    //Create if not else update
                    int gbvLnDtID = int.Parse(this.gbvDataGridView.Rows[i].Cells[9].Value.ToString());
                    int crtriaID = int.Parse(this.gbvDataGridView.Rows[i].Cells[2].Value.ToString());
                    double amntVal = double.Parse(this.gbvDataGridView.Rows[i].Cells[4].Value.ToString());
                    string crtrType = this.gbvDataGridView.Rows[i].Cells[0].Value.ToString();
                    string strDte = this.gbvDataGridView.Rows[i].Cells[5].Value.ToString();
                    string endDte = this.gbvDataGridView.Rows[i].Cells[7].Value.ToString();
                    if (gbvLnDtID <= 0)
                    {
                        Global.createGBVLn(gbvHdrID, crtriaID, crtrType, strDte, endDte, amntVal);
                        this.gbvDataGridView.Rows[i].Cells[9].Value = Global.getGBVLnID(gbvHdrID, crtriaID, crtrType, strDte);
                        this.gbvDataGridView.EndEdit();
                    }
                    else
                    {
                        Global.updateGBVLn(gbvLnDtID, crtriaID, crtrType, strDte, endDte, amntVal);
                    }
                    svd++;
                    this.gbvDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Lime;
                }
            }

            Global.mnFrm.cmCde.showMsg(svd + " Line(s) Saved Successfully!", 3);
            System.Windows.Forms.Application.DoEvents();
            this.obey_gbvdt_evnts = true;
        }

        private void delGBVButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[40]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.gbvHdrIDTextBox.Text == "" || this.gbvHdrIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select the Record to DELETE!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Record?" +
         "\r\nThis action cannot be undone!", 1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            Global.deleteGBV(int.Parse(this.gbvHdrIDTextBox.Text), this.gbvNameTextBox.Text);
            this.loadGBVPanel();
        }

        private void addGBVDTButton_Click(object sender, EventArgs e)
        {
            if (this.addgbv == false && this.editgbv == false)
            {
                Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                return;
            }
            this.createGBVDtRows(1);
            this.prpareForGBVLnsEdit();
        }

        private void delGBVDTButton_Click(object sender, EventArgs e)
        {
            if (this.addgbv == false && this.editgbv == false)
            {
                Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                return;
            }
            if (this.gbvDataGridView.CurrentCell != null
         && this.gbvDataGridView.SelectedRows.Count <= 0)
            {
                this.gbvDataGridView.Rows[this.gbvDataGridView.CurrentCell.RowIndex].Selected = true;
            }

            if (this.gbvDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the Record(s) to Delete!", 0);
                return;
            }

            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Line?" +
         "\r\nThis action cannot be undone!", 1) == DialogResult.No)
            {
                Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }

            for (int i = 0; i < this.gbvDataGridView.SelectedRows.Count; i++)
            {
                long lnID = -1;
                long.TryParse(this.gbvDataGridView.SelectedRows[i].Cells[9].Value.ToString(), out lnID);
                if (this.gbvDataGridView.SelectedRows[i].Cells[1].Value == null)
                {
                    this.gbvDataGridView.SelectedRows[i].Cells[1].Value = string.Empty;
                }
                Global.deleteGBVLn(lnID, this.gbvDataGridView.SelectedRows[i].Cells[1].Value.ToString());
            }
            this.loadGBVPanel();
        }

        private void dfltFill(int rwIdx)
        {
            if (this.gbvDataGridView.Rows[rwIdx].Cells[0].Value == null)
            {
                this.gbvDataGridView.Rows[rwIdx].Cells[0].Value = "Divisions/Groups";
            }
            if (this.gbvDataGridView.Rows[rwIdx].Cells[1].Value == null)
            {
                this.gbvDataGridView.Rows[rwIdx].Cells[1].Value = string.Empty;
            }
            if (this.gbvDataGridView.Rows[rwIdx].Cells[2].Value == null)
            {
                this.gbvDataGridView.Rows[rwIdx].Cells[2].Value = "-1";
            }
            if (this.gbvDataGridView.Rows[rwIdx].Cells[4].Value == null)
            {
                this.gbvDataGridView.Rows[rwIdx].Cells[4].Value = "0.00";
            }
            if (this.gbvDataGridView.Rows[rwIdx].Cells[5].Value == null)
            {
                this.gbvDataGridView.Rows[rwIdx].Cells[5].Value = string.Empty;
            }
            if (this.gbvDataGridView.Rows[rwIdx].Cells[7].Value == null)
            {
                this.gbvDataGridView.Rows[rwIdx].Cells[7].Value = string.Empty;
            }
            if (this.gbvDataGridView.Rows[rwIdx].Cells[9].Value == null)
            {
                this.gbvDataGridView.Rows[rwIdx].Cells[9].Value = "-1";
            }

        }

        private void gbvDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e == null || this.obey_gbvdt_evnts == false)
            {
                return;
            }
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            bool prv = this.obey_gbvdt_evnts;
            this.obey_gbvdt_evnts = false;
            this.gbvDataGridView.EndEdit();
            System.Windows.Forms.Application.DoEvents();
            this.dfltFill(e.RowIndex);

            if (e.ColumnIndex == 6)
            {
                if (this.addgbv == false && this.editgbv == false)
                {
                    Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                    this.obey_gbvdt_evnts = true;
                    return;
                }

                this.textBox1.Text = this.gbvDataGridView.Rows[e.RowIndex].Cells[5].Value.ToString();
                Global.mnFrm.cmCde.selectDate(ref this.textBox1);
                this.gbvDataGridView.Rows[e.RowIndex].Cells[5].Value = this.textBox1.Text;
                this.gbvDataGridView.EndEdit();

                this.obey_gbvdt_evnts = true;
                DataGridViewCellEventArgs ex = new DataGridViewCellEventArgs(5, e.RowIndex);
                this.gbvDataGridView_CellValueChanged(this.gbvDataGridView, ex);
            }
            else if (e.ColumnIndex == 8)
            {
                if (this.addgbv == false && this.editgbv == false)
                {
                    Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                    this.obey_gbvdt_evnts = true;
                    return;
                }

                this.textBox1.Text = this.gbvDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString();
                Global.mnFrm.cmCde.selectDate(ref this.textBox1);
                this.gbvDataGridView.Rows[e.RowIndex].Cells[7].Value = this.textBox1.Text;
                this.gbvDataGridView.EndEdit();

                this.obey_gbvdt_evnts = true;
                DataGridViewCellEventArgs ex = new DataGridViewCellEventArgs(7, e.RowIndex);
                this.gbvDataGridView_CellValueChanged(this.gbvDataGridView, ex);
            }
            else if (e.ColumnIndex == 3)
            {
                if (this.addgbv == false && this.editgbv == false)
                {
                    Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                    this.obey_gbvdt_evnts = true;
                    return;
                }

                this.srchWrd = this.gbvDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
                if (!this.srchWrd.Contains("%"))
                {
                    this.srchWrd = "%" + this.srchWrd.Replace(" ", "%") + "%";
                    //this.smmryDataGridView.Rows[e.RowIndex].Cells[4].Value = "-1";
                }

                //Item Names
                string slctdCriteria = this.gbvDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
                if (slctdCriteria == "")
                {
                    Global.mnFrm.cmCde.showMsg("Please select a Criteria Type!", 0);
                    this.obey_gbvdt_evnts = true;
                    return;
                }

                string[] selVals = new string[1];
                selVals[0] = this.gbvDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
                string grpCmbo = "";
                if (slctdCriteria == "Divisions/Groups")
                {
                    grpCmbo = "Divisions/Groups";
                }
                else if (slctdCriteria == "Grade")
                {
                    grpCmbo = "Grades";
                }
                else if (slctdCriteria == "Job")
                {
                    grpCmbo = "Jobs";
                }
                else if (slctdCriteria == "Position")
                {
                    grpCmbo = "Positions";
                }
                else if (slctdCriteria == "Site/Location")
                {
                    grpCmbo = "Sites/Locations";
                }
                else if (slctdCriteria == "Person Type")
                {
                    grpCmbo = "Person Types";
                }
                else if (slctdCriteria == "Working Hour Type")
                {
                    grpCmbo = "Working Hours";
                }
                else if (slctdCriteria == "Gathering Type")
                {
                    grpCmbo = "Gathering Types";
                }
                int[] selVal1s = new int[1];

                DialogResult dgRes;
                if (slctdCriteria != "Person Type")
                {
                    dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                    Global.mnFrm.cmCde.getLovID(grpCmbo), ref selVals,
                    true, true, Global.mnFrm.cmCde.Org_id,
                   this.srchWrd, "Both", true);
                }
                else
                {
                    dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                    Global.mnFrm.cmCde.getLovID("Person Types"), ref selVal1s, true, true,
                   this.srchWrd, "Both", true);
                }
                int slctn = 0;
                if (slctdCriteria != "Person Type")
                {
                    slctn = selVals.Length;
                }
                else
                {
                    slctn = selVal1s.Length;
                }
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < slctn; i++)
                    {
                        this.gbvDataGridView.Rows[e.RowIndex].Cells[2].Value = selVals[i];
                        if (slctdCriteria == "Divisions/Groups")
                        {
                            this.gbvDataGridView.Rows[e.RowIndex].Cells[1].Value = Global.mnFrm.cmCde.getDivName(int.Parse(selVals[i]));
                        }
                        else if (slctdCriteria == "Grade")
                        {
                            this.gbvDataGridView.Rows[e.RowIndex].Cells[1].Value = Global.mnFrm.cmCde.getGrdName(int.Parse(selVals[i]));
                        }
                        else if (slctdCriteria == "Job")
                        {
                            this.gbvDataGridView.Rows[e.RowIndex].Cells[1].Value = Global.mnFrm.cmCde.getJobName(int.Parse(selVals[i]));
                        }
                        else if (slctdCriteria == "Position")
                        {
                            this.gbvDataGridView.Rows[e.RowIndex].Cells[1].Value = Global.mnFrm.cmCde.getPosName(int.Parse(selVals[i]));
                        }
                        else if (slctdCriteria == "Site/Location")
                        {
                            this.gbvDataGridView.Rows[e.RowIndex].Cells[1].Value = Global.mnFrm.cmCde.getSiteName(int.Parse(selVals[i]));
                        }
                        else if (slctdCriteria == "Person Type")
                        {
                            this.gbvDataGridView.Rows[e.RowIndex].Cells[2].Value = selVal1s[i].ToString();
                            this.gbvDataGridView.Rows[e.RowIndex].Cells[1].Value = Global.mnFrm.cmCde.getPssblValNm(selVal1s[i]);
                        }
                        else if (slctdCriteria == "Working Hour Type")
                        {
                            this.gbvDataGridView.Rows[e.RowIndex].Cells[1].Value = Global.mnFrm.cmCde.getWkhName(int.Parse(selVals[i]));
                        }
                        else if (slctdCriteria == "Gathering Type")
                        {
                            this.gbvDataGridView.Rows[e.RowIndex].Cells[1].Value = Global.mnFrm.cmCde.getGathName(int.Parse(selVals[i]));
                        }
                    }
                }

                //this.gbvDataGridView.Rows[e.RowIndex].Cells[7].Value = this.textBox1.Text;
                this.gbvDataGridView.EndEdit();

                this.obey_gbvdt_evnts = true;
                DataGridViewCellEventArgs ex = new DataGridViewCellEventArgs(7, e.RowIndex);
                this.gbvDataGridView_CellValueChanged(this.gbvDataGridView, ex);
            }
            this.obey_gbvdt_evnts = true;
        }

        private void gbvDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e == null || this.obey_gbvdt_evnts == false || (this.addgbv == false && this.editgbv == false))
            {
                return;
            }
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            bool prv = this.obey_gbvdt_evnts;
            this.obey_gbvdt_evnts = false;

            this.dfltFill(e.RowIndex);

            if (e.ColumnIndex == 5
              || e.ColumnIndex == 7)
            {
                this.gbvDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();

                string dtetmin = this.gbvDataGridView.Rows[e.RowIndex].Cells[5].Value.ToString();
                string dtetmout = this.gbvDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString();
                if (e.ColumnIndex == 5 && dtetmin != "")
                {
                    dtetmin = Global.mnFrm.cmCde.checkNFormatDate(dtetmin);
                    this.gbvDataGridView.Rows[e.RowIndex].Cells[5].Value = dtetmin;
                    this.gbvDataGridView.EndEdit();
                    System.Windows.Forms.Application.DoEvents();
                }
                if (e.ColumnIndex == 7 && dtetmout != "")
                {
                    dtetmout = Global.mnFrm.cmCde.checkNFormatDate(dtetmout);
                    this.gbvDataGridView.Rows[e.RowIndex].Cells[7].Value = dtetmout;
                    this.gbvDataGridView.EndEdit();
                    System.Windows.Forms.Application.DoEvents();
                }
                this.gbvDataGridView.EndEdit();
            }
            else if (e.ColumnIndex == 1)
            {
                this.obey_gbvdt_evnts = true;
                DataGridViewCellEventArgs e1 = new DataGridViewCellEventArgs(3, e.RowIndex);
                this.gbvDataGridView.EndEdit();
                this.gbvDataGridView_CellContentClick(this.gbvDataGridView, e1);

            }
            else if (e.ColumnIndex == 4)
            {
                double lnAmnt = 0;
                string orgnlAmnt = this.gbvDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
                bool isno = double.TryParse(orgnlAmnt, out lnAmnt);
                if (isno == false)
                {
                    lnAmnt = Global.computeMathExprsn(orgnlAmnt);
                }
                this.gbvDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
                this.gbvDataGridView.Rows[e.RowIndex].Cells[4].Value = lnAmnt;// Math.Round(lnAmnt, 15);
                this.gbvDataGridView.EndEdit();
            }
            this.obey_gbvdt_evnts = true;
        }

        private void gbvDataGridView_CurrentCellChanged(object sender, EventArgs e)
        {
            //if (this.gbvDataGridView.CurrentCell == null || this.obey_gbvdt_evnts == false)
            //{
            // return;
            //}
            //int rwidx = this.gbvDataGridView.CurrentCell.RowIndex;
            //int colidx = this.gbvDataGridView.CurrentCell.ColumnIndex;

            //if (rwidx < 0 || colidx < 0)
            //{
            // return;
            //}
            //bool prv = this.obey_gbvdt_evnts;
            //this.obey_gbvdt_evnts = false;
            //this.dfltFill(rwidx);
            //if (colidx >= 0)
            //{
            // double lnAmnt = 0;
            // string orgnlAmnt = this.gbvDataGridView.Rows[rwidx].Cells[4].Value.ToString();
            // bool isno = double.TryParse(orgnlAmnt, out lnAmnt);
            // if (isno == false)
            // {
            //  lnAmnt = Global.computeMathExprsn(orgnlAmnt);
            // }
            // this.gbvDataGridView.EndEdit();
            // System.Windows.Forms.Application.DoEvents();
            // this.gbvDataGridView.Rows[rwidx].Cells[4].Value = lnAmnt;// Math.Round(lnAmnt, 15);
            // this.gbvDataGridView.EndEdit();

            // /*int acntID = int.Parse(this.gbvDataGridView.Rows[rwidx].Cells[10].Value.ToString());
            // this.gbvDataGridView.Rows[rwidx].Cells[9].Value = Global.mnFrm.cmCde.getAccntNum(acntID) +
            // "." + Global.mnFrm.cmCde.getAccntName(acntID);

            // long prepayID = long.Parse(this.gbvDataGridView.Rows[rwidx].Cells[17].Value.ToString());
            // this.gbvDataGridView.Rows[rwidx].Cells[16].Value = Global.mnFrm.cmCde.getGnrlRecNm(
            // "accb.accb_rcvbls_invc_hdr", "rcvbls_invc_hdr_id", "rcvbls_invc_number", prepayID);*/

            //}
            //this.obey_gbvdt_evnts = true;
        }

        private void resetGBVButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.minimizeMemory();
            this.searchInGBVComboBox.SelectedIndex = 0;
            this.searchForGBVTextBox.Text = "%";

            this.dsplySizeGBVComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            this.dsplySizeGBVDTComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            this.gbv_cur_indx = 0;
            this.gbvdt_cur_indx = 0;
            this.loadGBVPanel();
        }

        private void exprtGBVTmp(int exprtTyp, int gbvHdrID_in)
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
            string[] hdngs = { "Criteria Type**", "Criteria Name**", "Criteria Amount/Value**", "Start Date", "End Date" };
            for (int a = 0; a < hdngs.Length; a++)
            {
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Font.Bold = true;
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Value2 = hdngs[a].ToUpper();
            }
            if (exprtTyp == 2)
            {
                DataSet dtst = Global.get_One_GBVDet(gbvHdrID_in, 0, 1000000000);
                for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
                {
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 2]).Value2 = dtst.Tables[0].Rows[a][1].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dtst.Tables[0].Rows[a][3].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dtst.Tables[0].Rows[a][4].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 5]).Value2 = dtst.Tables[0].Rows[a][5].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 6]).Value2 = dtst.Tables[0].Rows[a][6].ToString();
                }
            }
            else if (exprtTyp >= 3)
            {
                DataSet dtst = Global.get_One_GBVDet(gbvHdrID_in, 0, exprtTyp);
                for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
                {
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 2]).Value2 = dtst.Tables[0].Rows[a][1].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dtst.Tables[0].Rows[a][3].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dtst.Tables[0].Rows[a][4].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 5]).Value2 = dtst.Tables[0].Rows[a][5].ToString();
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 6]).Value2 = dtst.Tables[0].Rows[a][6].ToString();
                }
            }
            else
            {
            }

            Global.mnFrm.cmCde.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 10;
            Global.mnFrm.cmCde.trgtSheets[0].get_Range("A1:A65535", Type.Missing).WrapText = true;

            Global.mnFrm.cmCde.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Columns.AutoFit();
            Global.mnFrm.cmCde.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Rows.AutoFit();
        }

        private void imprtGBVTmp(string filename, int gbvHdrID_in)
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
            string citeriaType = "";
            string criteriaNm = "";
            string amount = "";
            string dtetmenum1 = "";
            string dtetmenum2 = "";
            int rownum = 5;
            do
            {
                try
                {
                    citeriaType = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 2]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    citeriaType = "";
                }
                try
                {
                    criteriaNm = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 3]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    criteriaNm = "";
                }
                try
                {
                    amount = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 4]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    amount = "0.00";
                }
                try
                {
                    dtetmenum1 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 5]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    dtetmenum1 = "";
                }
                try
                {
                    dtetmenum2 = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 6]).Value2.ToString();
                }
                catch (Exception ex)
                {
                    dtetmenum2 = "";
                }

                if (rownum == 5)
                {
                    string[] hdngs = { "Criteria Type**", "Criteria Name**", "Criteria Amount/Value**", "Start Date", "End Date" };

                    if (citeriaType != hdngs[0].ToUpper()
                      || criteriaNm != hdngs[1].ToUpper()
                      || amount != hdngs[2].ToUpper()
                      || dtetmenum1 != hdngs[3].ToUpper()
                      || dtetmenum2 != hdngs[4].ToUpper())
                    {
                        Global.mnFrm.cmCde.showMsg("The Excel File you Selected is not a Valid Template\r\nfor importing records here.", 0);
                        return;
                    }
                    rownum++;
                    continue;
                }
                if (citeriaType != "" && criteriaNm != "" && amount != "")
                {
                    double numFrm = 0;
                    bool isdbl = false;
                    isdbl = double.TryParse(dtetmenum1, out numFrm);
                    string DteFrm;
                    if (isdbl)
                    {
                        DteFrm = DateTime.FromOADate(numFrm).ToString("dd-MMM-yyyy HH:mm:ss");
                    }
                    else
                    {
                        DteFrm = Global.mnFrm.cmCde.getFrmtdDB_Date_time();
                    }

                    numFrm = 0;
                    isdbl = false;
                    isdbl = double.TryParse(dtetmenum2, out numFrm);
                    string DteTo;
                    if (isdbl)
                    {
                        DteTo = DateTime.FromOADate(numFrm).ToString("dd-MMM-yyyy HH:mm:ss");
                    }
                    else
                    {
                        DteTo = "31-Dec-4000 23:59:59";
                    }
                    double amt = double.Parse(amount);
                    int valID = Global.get_CriteriaID(criteriaNm, citeriaType);

                    long gbvDetID = Global.get_One_GBVDetID(
               gbvHdrID_in, valID, citeriaType, DteFrm);
                    //Global.mnFrm.cmCde.showMsg(gbvDetID + "/" + valID + "/" +
                    //  criteriaNm + "/" + citeriaType + "/" + gbvHdrID_in + 
                    //  "/" + DteFrm + "/" + DteTo + "/" + amt, 0);
                    if ((gbvDetID <= 0 && valID > 0)
                      || (gbvDetID <= 0 && citeriaType == "Everyone"))
                    {
                        Global.createGBVLn(gbvHdrID_in, valID, citeriaType
                          , DteFrm, DteTo, amt);
                        Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + rownum + ":G" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 255, 0));
                    }
                    else if (gbvDetID > 0)
                    {
                        Global.updateGBVLn((int)gbvDetID, valID, citeriaType
                          , DteFrm, DteTo, amt);
                        Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + rownum + ":G" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen);
                    }
                    else
                    {
                        Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + rownum + ":G" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 0, 0));
                        //this.trgtSheets[0].get_Range("M" + rownum + ":M" + rownum + "", Type.Missing).Value2 = errMsg;
                    }
                }
                rownum++;
            }
            while (citeriaType != "");
        }

        private void exportGBVButton_Click(object sender, EventArgs e)
        {
            string rspnse = Interaction.InputBox("How many Records will you like to Export?" +
                 "\r\n1=No Records(Empty Template)" +
                 "\r\n2=All Records" +
                 "\r\n3-Infinity=Specify the exact number of Records to Export\r\n",
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
            this.exprtGBVTmp(rsponse, int.Parse(this.gbvHdrIDTextBox.Text));
        }

        private void importGBVButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to Import Records\r\n into this Global Value Header?", 1) == DialogResult.No)
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
                this.imprtGBVTmp(this.openFileDialog1.FileName, int.Parse(this.gbvHdrIDTextBox.Text));
            }
            this.loadGBVDetPanel();
        }

        private void gbvListView_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.Control && e.KeyCode == Keys.S)
            {
                if (this.saveGBVButton.Enabled == true)
                {
                    this.saveGBVButton_Click(this.saveGBVButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                if (this.addGBVButton.Enabled == true)
                {
                    this.addGBVButton_Click(this.addGBVButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                if (this.editGBVButton.Enabled == true)
                {
                    this.editGBVButton_Click(this.editGBVButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                this.resetGBVButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)
            {
                if (this.refreshGBVButton.Enabled == true)
                {
                    this.refreshGBVButton_Click(this.refreshGBVButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.Delete)
            {
                if (this.delGBVButton.Enabled == true)
                {
                    this.delGBVButton_Click(this.delGBVButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                Global.mnFrm.cmCde.listViewKeyDown(this.gbvListView, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void gbvNameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.Control && e.KeyCode == Keys.S)
            {
                if (this.saveGBVButton.Enabled == true)
                {
                    this.saveGBVButton_Click(this.saveGBVButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                if (this.addGBVButton.Enabled == true)
                {
                    this.addGBVButton_Click(this.addGBVButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                if (this.editGBVButton.Enabled == true)
                {
                    this.editGBVButton_Click(this.editGBVButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                this.resetGBVButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)
            {
                if (this.refreshGBVButton.Enabled == true)
                {
                    this.refreshGBVButton_Click(this.refreshGBVButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.Delete)
            {
                if (this.delGBVButton.Enabled == true)
                {
                    this.delGBVButton_Click(this.delGBVButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                //Global.mnFrm.cmCde.listViewKeyDown(this.gbvListView, e);
                e.Handled = false;
                e.SuppressKeyPress = false;
            }
        }
        #endregion
        #region "UNCLASSIFIED CODE...."
        private void glInfcListView_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.Control && e.KeyCode == Keys.S)       // Ctrl-S Save
            {
                // do what you want here
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if (e.Control && e.KeyCode == Keys.N)       // Ctrl-S Save
            {
                // do what you want here
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if (e.Control && e.KeyCode == Keys.E)       // Ctrl-S Save
            {
                // do what you want here
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)       // Ctrl-S Save
            {
                // do what you want here
                this.goInfcButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                this.resetTrnsButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if (e.Control && e.KeyCode == Keys.Delete)
            {

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                e.Handled = false;
                e.SuppressKeyPress = false;  // stops bing! also sets handeled which stop event bubbling
                if (this.glInfcListView.Focused)
                {
                    Global.mnFrm.cmCde.listViewKeyDown(this.glInfcListView, e);
                }
            }
        }

        private void trnsSearchListView_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.Control && e.KeyCode == Keys.S)       // Ctrl-S Save
            {
                // do what you want here
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if (e.Control && e.KeyCode == Keys.N)       // Ctrl-S Save
            {
                // do what you want here
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if (e.Control && e.KeyCode == Keys.E)       // Ctrl-S Save
            {
                // do what you want here
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)       // Ctrl-S Save
            {
                // do what you want here
                this.goTrnsButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                this.resetTrnsSrchButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if (e.Control && e.KeyCode == Keys.Delete)
            {

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                e.Handled = false;
                e.SuppressKeyPress = false;  // stops bing! also sets handeled which stop event bubbling
                if (this.trnsSearchListView.Focused)
                {
                    Global.mnFrm.cmCde.listViewKeyDown(this.trnsSearchListView, e);
                }
            }
        }

        private void msPyListView_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();

            if (e.Control && e.KeyCode == Keys.S)
            {
                if (this.saveMsPyButton.Enabled == true)
                {
                    this.saveMsPyButton_Click(this.saveMsPyButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                if (this.addMsPyButton.Enabled == true)
                {
                    this.addMsPyButton_Click(this.addMsPyButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                if (this.editMsPyButton.Enabled == true)
                {
                    this.editMsPyButton_Click(this.editMsPyButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.F5)
            {
                if (this.msPayActRnButton.Enabled == true)
                {
                    this.msPayActRnButton_Click(this.msPayActRnButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                this.resetMsPyButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)
            {
                if (this.goMsPyButton.Enabled == true)
                {
                    this.goMsPyButton_Click(this.goMsPyButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.Delete)
            {
                if (this.delMsPyButton.Enabled == true)
                {
                    this.delMsPyButton_Click(this.delMsPyButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                Global.mnFrm.cmCde.listViewKeyDown(this.msPyListView, e);
            }
        }

        private void msPyTxtBx_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.Control && e.KeyCode == Keys.S)
            {
                if (this.saveMsPyButton.Enabled == true)
                {
                    this.saveMsPyButton_Click(this.saveMsPyButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                if (this.addMsPyButton.Enabled == true)
                {
                    this.addMsPyButton_Click(this.addMsPyButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                if (this.editMsPyButton.Enabled == true)
                {
                    this.editMsPyButton_Click(this.editMsPyButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.F5)
            {
                if (this.msPayActRnButton.Enabled == true)
                {
                    this.msPayActRnButton_Click(this.msPayActRnButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                this.resetMsPyButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)
            {
                if (this.goMsPyButton.Enabled == true)
                {
                    this.goMsPyButton_Click(this.goMsPyButton, ex);
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

        private void msPyDtListView_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.Control && e.KeyCode == Keys.S)
            {
                if (this.saveMsPyButton.Enabled == true)
                {
                    this.saveMsPyButton_Click(this.saveMsPyButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                if (this.addMsPyButton.Enabled == true)
                {
                    this.addMsPyButton_Click(this.addMsPyButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                if (this.editMsPyButton.Enabled == true)
                {
                    this.editMsPyButton_Click(this.editMsPyButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.F5)
            {
                if (this.msPayActRnButton.Enabled == true)
                {
                    this.msPayActRnButton_Click(this.msPayActRnButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                this.resetMsPyButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)
            {
                if (this.goMsPyButton.Enabled == true)
                {
                    this.goMsPyButton_Click(this.goMsPyButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.Delete)
            {
                if (this.delMsPyButton.Enabled == true)
                {
                    this.delMsPyButton_Click(this.delMsPyButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                Global.mnFrm.cmCde.listViewKeyDown(this.msPyDtListView, e);
            }
        }

        private void pymntTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.shdObeyPrsEvts() == false)
            {
                return;
            }
            if (this.prsNamesListView.SelectedItems.Count <= 0)
            {
                this.pymntTabControl.SelectedTab = this.payTabPage;
                return;
            }
            this.curTabIndx = this.pymntTabControl.SelectedTab.Name;
            this.loadCorrectPanel();
        }

        private void glDateButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.selectDate(ref this.glDateTextBox);
        }

        private void pstPayListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.shdObeyItmEvts1() == false)
            {
                return;
            }
            if (this.itmListViewPymnt.SelectedItems.Count > 0)
            {
                this.populateTodyPymnts();
                //this.loadPstPayPanel();
            }
        }

        private void msPyGLDateButton_Click(object sender, EventArgs e)
        {
            //msPyGLDate
            if (this.addMsPy == false && this.editMsPy == false)
            {
                Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                return;
            }
            Global.mnFrm.cmCde.selectDate(ref this.msPyGLDateTextBox);

        }

        private void attchedValsButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[22]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }

            if (this.msPyIDTextBox.Text == "" || this.msPyIDTextBox.Text == "-1000000")
            {
                Global.mnFrm.cmCde.showMsg("Please select a Mass Pay Run First!", 0);
                return;
            }

            if (this.trnsDateTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please enter a Mass Pay Run Date!", 0);
                return;
            }

            if (this.msPyGLDateTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please enter a Mass Pay Run GL Date!", 0);
                return;
            }
            if (this.hsGoneToGLCheckBox.Checked == false
              && this.editMsPy == false
              && this.addMsPy == false)
            {
                this.editMsPyButton.PerformClick();
            }
            int prsn = -1;
            int.TryParse(this.msPyPrsStIDTextBox.Text, out prsn);
            if (this.msPyPrsStIDTextBox.Text == "" || this.msPyPrsStIDTextBox.Text == "-1"
              || prsn <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Mass Pay Person Set!", 0);
                return;
            }
            if (this.msPyItmStIDTextBox.Text == "" || this.msPyItmStIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select a Mass Pay Item Set!", 0);
                return;
            }
            massPayValuesDiag nwdiag = new massPayValuesDiag();
            nwdiag.msPyID = long.Parse(this.msPyIDTextBox.Text);
            nwdiag.prsnSetID = int.Parse(this.msPyPrsStIDTextBox.Text);
            nwdiag.pyItmSetID = int.Parse(this.msPyItmStIDTextBox.Text);
            nwdiag.addRec = this.addMsPy;
            nwdiag.editRec = this.editMsPy;
            nwdiag.trns_date = this.trnsDateTextBox.Text;
            nwdiag.glDate = this.msPyGLDateTextBox.Text;
            DialogResult dgres = nwdiag.ShowDialog();
            if (dgres == DialogResult.OK)
            {
            }
        }

        private void itmPrsPyValDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();

            if (e.Control && e.KeyCode == Keys.S)
            {
                //if (this.saveTrnsBatchButton.Enabled == true)
                //{
                //  this.saveTrnsBatchButton_Click(this.saveTrnsBatchButton, ex);
                //}
                e.Handled = false;
                e.SuppressKeyPress = false;
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                if (this.addValButtonPrs.Enabled == true)
                {
                    this.addValButton_ClickPrs(this.addValButtonPrs, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                if (this.editValButtonPrs.Enabled == true)
                {
                    this.editValButton_ClickPrs(this.editValButtonPrs, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                this.resetButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)
            {
                if (this.refreshValButtonPrs.Enabled == true)
                {
                    this.refreshValButton_ClickPrs(this.refreshValButtonPrs, ex);
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

        private void bankDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();

            if (e.Control && e.KeyCode == Keys.S)
            {
                if (this.saveBankButton.Enabled == true)
                {
                    this.saveBankButton_Click(this.saveBankButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                if (this.addBankButton.Enabled == true)
                {
                    this.addBankButton_Click(this.addBankButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                if (this.editBankButton.Enabled == true)
                {
                    this.editBankButton_Click(this.editBankButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                this.resetButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)
            {
                if (this.refreshBankButton.Enabled == true)
                {
                    this.refreshBankButton_Click(this.refreshBankButton, ex);
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

        private void itemListView_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.Control && e.KeyCode == Keys.S)
            {
                if (this.saveItmButton.Enabled == true)
                {
                    this.saveItmButton_Click(this.saveItmButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                if (this.addItmButton.Enabled == true)
                {
                    this.addItmButton_Click(this.addItmButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                if (this.editItmButton.Enabled == true)
                {
                    this.editItmButton_Click(this.editItmButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                this.resetPayItemsButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)
            {
                if (this.goItmButton.Enabled == true)
                {
                    this.goItmButton_Click(this.goItmButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.Delete)
            {
                if (this.delItmButton.Enabled == true)
                {
                    this.delItmButton_Click(this.delItmButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                Global.mnFrm.cmCde.listViewKeyDown(this.itemListView, e);
            }
        }

        private void benfitsTxtBx_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.Control && e.KeyCode == Keys.S)
            {
                if (this.saveItmButton.Enabled == true)
                {
                    this.saveItmButton_Click(this.saveItmButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                if (this.addItmButton.Enabled == true)
                {
                    this.addItmButton_Click(this.addItmButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                if (this.editItmButton.Enabled == true)
                {
                    this.editItmButton_Click(this.editItmButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                this.resetPayItemsButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)
            {
                if (this.goItmButton.Enabled == true)
                {
                    this.goItmButton_Click(this.goItmButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;

            }
            else if (e.Control && e.KeyCode == Keys.Delete)
            {
                if (this.delItmButton.Enabled == true)
                {
                    this.delItmButton_Click(this.delItmButton, ex);
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

        private void paymntDescTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if ((e.Control && e.KeyCode == Keys.S))
            {
                this.processPayButton_Click(this.processPayButton, ex);
            }
        }

        private void feedItemsListView_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.Control && e.KeyCode == Keys.S)
            {
                if (this.saveItmButton.Enabled == true)
                {
                    this.saveItmButton_Click(this.saveItmButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                if (this.addFeedButton.Enabled == true)
                {
                    this.addFeedButton_Click(this.addFeedButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                if (this.editFeedButton.Enabled == true)
                {
                    this.editFeedButton_Click(this.editFeedButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                this.resetPayItemsButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)
            {
                if (this.goItmButton.Enabled == true)
                {
                    this.goItmButton_Click(this.goItmButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.Delete)
            {
                if (this.deleteFeedButton.Enabled == true)
                {
                    this.deleteFeedButton_Click(this.deleteFeedButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                Global.mnFrm.cmCde.listViewKeyDown(this.feedItemsListView, e);
            }
        }

        private void itmPssblValDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.Control && e.KeyCode == Keys.S)
            {
                if (this.saveItmButton.Enabled == true)
                {
                    this.saveItmButton_Click(this.saveItmButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                if (this.addValButton.Enabled == true)
                {
                    this.addValButton_Click(this.addValButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                if (this.editValButton.Enabled == true)
                {
                    this.editValButton_Click(this.editValButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                this.resetPayItemsButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)
            {
                if (this.refreshValButton.Enabled == true)
                {
                    this.refreshValButton_Click(this.refreshValButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.Delete)
            {
                if (this.delValButton.Enabled == true)
                {
                    this.delValButton_Click(this.delValButton, ex);
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

        private void glInfcCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.goInfcButton_Click(this.goInfcButton, e);
        }

        private void imbalnceCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.imbalnceCheckBox.Checked == true)
            {
                this.glInfcCheckBox.Checked = true;
            }
            this.goInfcButton_Click(this.goInfcButton, e);
        }

        private void userTrnsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.goInfcButton_Click(this.goInfcButton, e);
        }

        private void correctIntrfcImbals(string intrfcTblNm)
        {

            int suspns_accnt = Global.get_Suspns_Accnt(Global.mnFrm.cmCde.Org_id);
            DataSet dteDtSt = Global.get_Intrfc_dateSums(intrfcTblNm, Global.mnFrm.cmCde.Org_id);
            if (dteDtSt.Tables[0].Rows.Count > 0 && suspns_accnt > 0)
            {
                string msg1 = @"";
                for (int i = 0; i < dteDtSt.Tables[0].Rows.Count; i++)
                {
                    double dlyDbtAmnt = double.Parse(dteDtSt.Tables[0].Rows[i][1].ToString());
                    double dlyCrdtAmnt = double.Parse(dteDtSt.Tables[0].Rows[i][2].ToString());
                    int orgID = Global.mnFrm.cmCde.Org_id;
                    if (dlyDbtAmnt
                     != dlyCrdtAmnt)
                    {
                        //long suspns_batch_id = glBatchID;
                        int funcCurrID = Global.mnFrm.cmCde.getOrgFuncCurID(orgID);
                        decimal dffrnc = (decimal)(dlyDbtAmnt - dlyCrdtAmnt);
                        string incrsDcrs = "D";
                        if (dffrnc < 0)
                        {
                            incrsDcrs = "I";
                        }
                        decimal imbalAmnt = Math.Abs(dffrnc);
                        double netAmnt = (double)Global.dbtOrCrdtAccntMultiplier(suspns_accnt,
                   incrsDcrs) * (double)imbalAmnt;
                        string dateStr1 = DateTime.ParseExact(dteDtSt.Tables[0].Rows[i][0].ToString(), "yyyy-MM-dd",
            System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy") + " 00:00:00";
                        //if (!Global.mnFrm.cmCde.isTransPrmttd(suspns_accnt,
                        //      dateStr, netAmnt))
                        //{
                        //  return; ;
                        //}

                        /*double netamnt = 0;

                        netamnt = Global.mnFrm.cmCde.dbtOrCrdtAccntMultiplier(
                          int.Parse(this.accntIDTextBox.Text),
                          this.incrsDcrsComboBox.Text.Substring(0, 1)) * (double)this.funcCurAmntNumUpDwn.Value;
                        */
                        string dateStr = Global.mnFrm.cmCde.getFrmtdDB_Date_time();

                        if (Global.getIntrfcTrnsID(intrfcTblNm, suspns_accnt, netAmnt,
                          dteDtSt.Tables[0].Rows[i][0].ToString() + " 00:00:00") > 0)
                        {
                            continue;
                        }

                        if (Global.dbtOrCrdtAccnt(suspns_accnt,
                          incrsDcrs) == "Debit")
                        {
                            if (intrfcTblNm == "scm.scm_gl_interface")
                            {
                                Global.createScmGLIntFcLn(suspns_accnt,
                        "Correction of Imbalance in GL Interface Table as at " + dateStr1,
                            (double)imbalAmnt, dateStr1,
                            funcCurrID, 0,
                            netAmnt, "Imbalance Correction", -1, -1, dateStr, "USR");
                            }
                            else
                            {
                                Global.createPayGLIntFcLn(suspns_accnt,
                        "Correction of Imbalance in GL Interface Table as at " + dateStr1,
                            (double)imbalAmnt, dateStr1,
                            funcCurrID, 0,
                            netAmnt, dateStr, "USR");
                            }

                        }
                        else
                        {

                            if (intrfcTblNm == "scm.scm_gl_interface")
                            {
                                Global.createScmGLIntFcLn(suspns_accnt,
                                       "Correction of Imbalance in GL Interface Table as at " + dateStr1,
                                     0, dateStr1,
                                     funcCurrID, (double)imbalAmnt,
                                     netAmnt, "Imbalance Correction", -1, -1, dateStr, "USR");
                            }
                            else
                            {
                                Global.createPayGLIntFcLn(suspns_accnt,
                        "Correction of Imbalance in GL Interface Table as at " + dateStr1,
                            (double)imbalAmnt, dateStr1,
                            funcCurrID, 0,
                            netAmnt, dateStr, "USR");
                            }
                        }

                        /*if (Global.dbtOrCrdtAccnt(suspns_accnt, incrsDcrs) == "Debit")
                        {
                          Global.createTransaction(suspns_accnt,
                              "Correction of Imbalance in GL Batch " + Global.getGnrlRecNm("accb.accb_trnsctn_batches",
                              "batch_id", "batch_name", glBatchID) + " as at " + dateStr1, (double)imbalAmnt,
                              dateStr1
                              , funcCurrID, suspns_batch_id, 0.00, netAmnt,
                            (double)imbalAmnt,
                            funcCurrID,
                            (double)imbalAmnt,
                            funcCurrID,
                            (double)1,
                            (double)1, "D");
                        }
                        else
                        {
                          Global.createTransaction(suspns_accnt,
                          "Correction of Imbalance in GL Batch " + Global.getGnrlRecNm("accb.accb_trnsctn_batches",
                              "batch_id", "batch_name", glBatchID) + " as at " + dateStr1, 0.00,
                          dateStr1, funcCurrID,
                          suspns_batch_id, (double)imbalAmnt, netAmnt,
                      (double)imbalAmnt,
                      funcCurrID,
                      (double)imbalAmnt,
                      funcCurrID,
                      (double)1,
                      (double)1, "C");
                        }*/
                    }

                    //msg1 = msg1 + dteDtSt.Tables[0].Rows[i][0].ToString() + "\t DR=" + 
                    //dteDtSt.Tables[0].Rows[i][1].ToString() + "\t CR=" + 
                    //dteDtSt.Tables[0].Rows[i][2].ToString() + "\r\n";
                }
                //Global.mnFrm.cmCde.showMsg(msg1, 4);
                //return;
            }
            else
            {
                //Global.mnFrm.cmCde.showMsg("There's no Imbalance to correct!", 0);
                //return;
            }
        }

        private void correctImblcsButton_Click(object sender, EventArgs e)
        {
            /**/
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[21]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                  " this action!\nContact your System Administrator!", 0);
                return;
            }
            this.correctIntrfcImbals("pay.pay_gl_interface");

            if (this.glInfcListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select at least one of the Unbalanced Trns.", 0);
                return;
            }
            int suspns_accnt = Global.get_Suspns_Accnt(Global.mnFrm.cmCde.Org_id);
            if (suspns_accnt <= -1)
            {
                Global.mnFrm.cmCde.showMsg("Please define a suspense Account First!", 0);
                return;
            }
            double dfrnce = 0;
            Global.isGLIntrfcBlcdOrg(Global.mnFrm.cmCde.Org_id, ref dfrnce);
            if (dfrnce == 0)
            {
                Global.mnFrm.cmCde.showMsg("There's no Imbalance to correct!", 0);
                return;
            }

            addGLIntfcTrnsDiag nwdiag = new addGLIntfcTrnsDiag();
            nwdiag.trnsDescTextBox.Text = "Correct GL Interface Imbalance- " + this.glInfcListView.SelectedItems[0].SubItems[3].Text;
            nwdiag.trnsDateTextBox.Text = this.glInfcListView.SelectedItems[0].SubItems[7].Text;
            nwdiag.trnsDateTextBox.ReadOnly = true;
            nwdiag.trnsDateButton.Enabled = false;
            nwdiag.orgid = Global.mnFrm.cmCde.Org_id;
            nwdiag.accntIDTextBox.Text = suspns_accnt.ToString();
            nwdiag.accntNameTextBox.Text = Global.mnFrm.cmCde.getAccntName(suspns_accnt);
            nwdiag.accntNumTextBox.Text = Global.mnFrm.cmCde.getAccntNum(suspns_accnt);
            int accntCurrID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
         "accb.accb_chart_of_accnts", "accnt_id", "crncy_id", suspns_accnt));
            nwdiag.acntCurrTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(accntCurrID);
            nwdiag.accntCurrIDTextBox.Text = accntCurrID.ToString();
            nwdiag.amntNumericUpDown.Value = (decimal)Math.Abs(dfrnce);

            nwdiag.crncyTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(accntCurrID);
            nwdiag.crncyIDTextBox.Text = accntCurrID.ToString();

            nwdiag.funcCurrTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(accntCurrID);
            nwdiag.funcCurrIDTextBox.Text = accntCurrID.ToString();

            if (dfrnce < 0)
            {
                nwdiag.incrsDcrsComboBox.SelectedItem = "INCREASE";
            }
            else
            {
                nwdiag.incrsDcrsComboBox.SelectedItem = "DECREASE";
            }

            if (nwdiag.ShowDialog() == DialogResult.OK)
            {
                this.userTrnsCheckBox.Checked = true;
                this.imbalnceCheckBox.Checked = false;
                this.goInfcButton_Click(this.goInfcButton, e);
            }
        }

        private bool rvrsImprtdIntrfcTrns(long intrfcID)
        {
            //try
            //{
            DataSet dtst = Global.getDocGLInfcLns(intrfcID);
            string dateStr = Global.mnFrm.cmCde.getFrmtdDB_Date_time();
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                int accntID = -1;
                double dbtamount = 0;
                double crdtamount = 0;
                int crncy_id = -1;
                double netamnt = 0;
                long srcDocLnID = -1;

                int.TryParse(dtst.Tables[0].Rows[i][1].ToString(), out accntID);
                double.TryParse(dtst.Tables[0].Rows[i][3].ToString(), out dbtamount);
                double.TryParse(dtst.Tables[0].Rows[i][8].ToString(), out crdtamount);
                int.TryParse(dtst.Tables[0].Rows[i][5].ToString(), out crncy_id);
                double.TryParse(dtst.Tables[0].Rows[i][11].ToString(), out netamnt);
                long.TryParse(dtst.Tables[0].Rows[i][12].ToString(), out srcDocLnID);

                string trnsdte = DateTime.ParseExact(
            dtst.Tables[0].Rows[i][4].ToString(), "yyyy-MM-dd HH:mm:ss",
            System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");

                Global.createPymntGLIntFcLn(accntID,
            "(Cancellation) " + dtst.Tables[0].Rows[i][2].ToString(),
            -1 * dbtamount, trnsdte,
            crncy_id, -1 * crdtamount,
            -1 * netamnt, srcDocLnID, dateStr, "USR");

            }
            return true;
            //}
            //catch (Exception ex)
            //{
            //  Global.mnFrm.cmCde.showMsg(ex.InnerException.ToString(), 0);
            //  return false;
            //}
        }

        private void voidButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[21]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                  " this action!\nContact your System Administrator!", 0);
                return;
            }

            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to VOID/DELETE Selected Transactions?", 1)
            == DialogResult.No)
            {
                return;
            }

            if (this.glInfcListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the User Trns. to DELETE!", 0);
                return;
            }
            long intfcID = long.Parse(this.glInfcListView.SelectedItems[0].SubItems[12].Text);
            string trnsSrc = Global.mnFrm.cmCde.getGnrlRecNm(
         "pay.pay_gl_interface", "interface_id", "trns_source", intfcID);
            if (trnsSrc != "USR")
            {
                Global.mnFrm.cmCde.showMsg("Only User Generated Trns. can be VOIDED/DELETED from Here!", 0);
                return;
            }
            Global.deleteGLInfcLine(intfcID);
            this.rvrsImprtdIntrfcTrns(intfcID);
            this.userTrnsCheckBox.Checked = true;
            this.imbalnceCheckBox.Checked = false;
            this.goInfcButton_Click(this.goInfcButton, e);
        }

        private void resetTrnsButton_Click(object sender, EventArgs e)
        {
            this.searchInInfcComboBox.SelectedIndex = 0;
            this.searchForInfcTextBox.Text = "%";
            this.dsplySizeInfcComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();

            this.userTrnsCheckBox.Checked = false;
            this.imbalnceCheckBox.Checked = false;
            this.glInfcCheckBox.Checked = false;
            this.numericUpDown1.Value = 0;
            this.numericUpDown2.Value = 0;
            this.infcDte1TextBox.Text = DateTime.ParseExact(
         Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).AddMonths(-24).ToString("dd-MMM-yyyy HH:mm:ss");
            this.infcDte2TextBox.Text = DateTime.ParseExact(
         Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).AddDays(1).ToString("dd-MMM-yyyy 00:00:00");

            this.cur_Infc_idx = 0;
            this.goInfcButton_Click(this.goInfcButton, e);
        }

        private void voidWrongGLTrnsfrs()
        {
            DataSet wrngDtSt = Global.get_WrongGLBatches(Global.mnFrm.cmCde.Org_id);

            for (int k = 0; k < wrngDtSt.Tables[0].Rows.Count; k++)
            {
                long btchID = long.Parse(wrngDtSt.Tables[0].Rows[k][1].ToString());
                string btchNm = wrngDtSt.Tables[0].Rows[k][0].ToString();

                DataSet dtst = Global.get_Batch_Trns_NoStatus(btchID);
                long ttltrns = dtst.Tables[0].Rows.Count;

                string dateStr = Global.mnFrm.cmCde.getFrmtdDB_Date_time();

                //Begin Process of voiding
                long beenPstdB4 = Global.getSimlrPstdBatchID(btchNm, Global.mnFrm.cmCde.Org_id);

                if (beenPstdB4 > 0)
                {
                    //Global.mnFrm.cmCde.showMsg("This batch has been reversed before\r\n Operation Cancelled!", 4);
                    //return;
                    continue;
                }

                long nwbatchid = Global.getBatchID(btchNm +
                 " (Auto Batch Reversal(Inventory)@" + dateStr.Substring(0, 11) + ")", Global.mnFrm.cmCde.Org_id);

                if (nwbatchid <= 0)
                {
                    Global.createBatch(Global.mnFrm.cmCde.Org_id,
                     btchNm + " (Auto Batch Reversal(Inventory)@" + dateStr.Substring(0, 11) + ")",
                     btchNm + " (Auto Batch Reversal(Inventory)@" + dateStr.Substring(0, 11) + ")",
                     "Auto Batch Reversal (Inventory)",
                     "VALID", btchID, "0");
                    Global.updateBatchVldtyStatus(btchID, "VOID");
                    nwbatchid = Global.getBatchID(btchNm +
                    " (Auto Batch Reversal(Inventory)@" + dateStr.Substring(0, 11) + ")",
                    Global.mnFrm.cmCde.Org_id);
                }
                //Get All Posted/Unposted Transactions in current batch
                //dtst = Global.get_Batch_Trns_NoStatus(long.Parse(this.batchIDTextBox.Text));
                //ttltrns = dtst.Tables[0].Rows.Count;
                for (int i = 0; i < ttltrns; i++)
                {
                    Global.createTransaction(int.Parse(dtst.Tables[0].Rows[i][9].ToString()),
                    dtst.Tables[0].Rows[i][3].ToString() + " (Reversal)", -1 * double.Parse(dtst.Tables[0].Rows[i][4].ToString()),
                    dtst.Tables[0].Rows[i][6].ToString(), int.Parse(dtst.Tables[0].Rows[i][7].ToString()),
                    nwbatchid, -1 * double.Parse(dtst.Tables[0].Rows[i][5].ToString()),
                    -1 * double.Parse(dtst.Tables[0].Rows[i][10].ToString()),
               -1 * double.Parse(dtst.Tables[0].Rows[i][12].ToString()),
               int.Parse(dtst.Tables[0].Rows[i][13].ToString()),
               -1 * double.Parse(dtst.Tables[0].Rows[i][14].ToString()),
               int.Parse(dtst.Tables[0].Rows[i][15].ToString()),
               double.Parse(dtst.Tables[0].Rows[i][16].ToString()),
               double.Parse(dtst.Tables[0].Rows[i][17].ToString()),
               dtst.Tables[0].Rows[i][18].ToString());
                    System.Windows.Forms.Application.DoEvents();
                }
                Global.updateBatchAvlblty(nwbatchid, "1");
                Global.updtBatchTrnsSrcIDs(btchID);
                Global.updtIntrfcTrnsSrcBatchIDs(btchID);
                System.Windows.Forms.Application.DoEvents();
            }
        }

        private void crrctWrngTrnsfrsButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[21]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                  " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.showMsg(@"This will void all GL Batches Involved for the GL Transfer to be Re-done!
Are you sure you want to VOID/DELETE Concerned GL Transactions?", 1)
         == DialogResult.No)
            {
                return;
            }

            this.waitLabel.Visible = true;
            System.Windows.Forms.Application.DoEvents();
            //Global.deleteBrknDocGLInfcLns();
            //System.Windows.Forms.Application.DoEvents();
            this.voidWrongGLTrnsfrs();
            this.waitLabel.Visible = false;
            System.Windows.Forms.Application.DoEvents();
            this.glInfcCheckBox.Checked = true;
            this.goInfcButton_Click(this.goInfcButton, e);
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.minimizeMemory();
            this.searchInPrsComboBox.SelectedIndex = 1;
            this.searchForPrsTextBox.Text = "%";
            this.searchInItmComboBoxNw.SelectedIndex = 0;
            this.searchForItmTextBoxNw.Text = "%";
            this.searchInPstComboBox.SelectedIndex = 1;
            this.searchForPstTextBox.Text = "%";

            this.dsplySizePrsComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            this.dsplySizeItmComboBoxNw.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            this.prs_cur_indx = 0;
            this.itm_cur_indx1 = 0;
            this.goPrsButton_Click(this.goPrsButton, e);
        }

        private void searchForPrsTextBox_Click(object sender, EventArgs e)
        {
            this.searchForPrsTextBox.SelectAll();
        }

        private void searchForItmTextBoxNw_Click(object sender, EventArgs e)
        {
            this.searchForItmTextBoxNw.SelectAll();
        }

        private void searchForPstTextBox_Click(object sender, EventArgs e)
        {
            this.searchForPstTextBox.SelectAll();
        }

        private void prsStNmMnlTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!this.obey_evnts)
            {
                return;
            }
            this.txtChngd = true;
        }

        private void prsStNmMnlTextBox_Leave(object sender, EventArgs e)
        {
            if (this.txtChngd == false)
            {
                return;
            }
            this.txtChngd = false;
            TextBox mytxt = (TextBox)sender;
            this.obey_evnts = false;
            this.srchWrd = mytxt.Text;
            if (!mytxt.Text.Contains("%"))
            {
                this.srchWrd = "%" + this.srchWrd.Replace(" ", "%") + "%";
            }

            if (mytxt.Name == "prsStNmMnlTextBox")
            {
                this.prsStNmMnlTextBox.Text = "";
                this.prsStIDMnlTextBox.Text = "-1";
                this.prsStMnlButton_Click(this.prsStMnlButton, e);
            }
            else if (mytxt.Name == "itmStNmMnlTextBox")
            {
                this.itmStNmMnlTextBox.Text = "";
                this.itmStIDMnlTextBox.Text = "-1";
                this.itmStMnlButton_Click(this.itmStMnlButton, e);
            }
            else if (mytxt.Name == "paymntDateTextBox")
            {
                this.paymntDateTextBox.Text = Global.mnFrm.cmCde.checkNFormatDate(this.paymntDateTextBox.Text);
            }
            else if (mytxt.Name == "glDateTextBox")
            {
                this.glDateTextBox.Text = Global.mnFrm.cmCde.checkNFormatDate(this.glDateTextBox.Text);
            }
            this.srchWrd = "%";
            this.obey_evnts = true;
            this.txtChngd = false;
        }

        private void dsplySizePyItmComboBoxPrs_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.refreshValButton_ClickPrs(this.refreshValButtonPrs, e);
            }
        }

        private void searchForItmStTextBox_Click(object sender, EventArgs e)
        {
            this.searchForItmStTextBox.SelectAll();
        }

        private void resetItmStButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.minimizeMemory();
            this.searchInItmStComboBox.SelectedIndex = 0;
            this.searchForItmStTextBox.Text = "%";

            this.dsplySizeItmStComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            this.dsplySizeItmsDetComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            this.disableItmStDetEdit();
            this.itmst_cur_indx = 0;
            this.goItmStButton_Click(this.goItmStButton, e);
        }

        private void resetPrsSetButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.minimizeMemory();
            this.searchInPrsStComboBox.SelectedIndex = 0;
            this.searchForPrsStTextBox.Text = "%";

            this.dsplySizePrsStComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            this.dsplySizePrsStDtComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();

            this.disablePrsStDetEdit();
            this.prsst_cur_indx = 0;
            this.goPrsStButton_Click(this.goPrsStButton, e);

        }

        private void searchForPrsStTextBox_Click(object sender, EventArgs e)
        {
            this.searchForPrsStTextBox.SelectAll();
        }

        private void dsplySizePrsStDtComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.loadPrsStDetPanel();
            }
        }

        private void dsplySizeItmsDetComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.loadItmStDetPanel();
            }
        }

        private void searchForMsPyTextBox_Click(object sender, EventArgs e)
        {
            this.searchForMsPyTextBox.SelectAll();
        }

        private void resetMsPyButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.minimizeMemory();
            this.searchInMsPyComboBox.SelectedIndex = 0;
            this.searchForMsPyTextBox.Text = "%";

            this.dsplySizeMsPyComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            this.dsplySizeMsPyDtComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();

            this.disableMsPyDetEdit();
            this.mspy_cur_indx = 0;
            this.goMsPyButton_Click(this.goMsPyButton, e);

        }

        private void dsplySizeMsPyDtComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.loadMsPyDetPanel();
            }

        }

        private void trnsDateTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!this.obey_mspy_evnts)
            {
                return;
            }
            this.txtChngd = true;
        }

        private void trnsDateTextBox_Leave(object sender, EventArgs e)
        {
            if (this.txtChngd == false)
            {
                return;
            }
            this.txtChngd = false;
            TextBox mytxt = (TextBox)sender;
            this.obey_mspy_evnts = false;
            this.srchWrd = mytxt.Text;
            if (!mytxt.Text.Contains("%"))
            {
                this.srchWrd = "%" + this.srchWrd.Replace(" ", "%") + "%";
            }

            if (mytxt.Name == "msPyPrsStNmTextBox")
            {
                this.msPyPrsStNmTextBox.Text = "";
                this.msPyPrsStIDTextBox.Text = "-1";
                this.msPyPrsStButton_Click(this.msPyPrsStButton, e);
            }
            else if (mytxt.Name == "msPyItmStNmTextBox")
            {
                this.msPyItmStNmTextBox.Text = "";
                this.msPyItmStIDTextBox.Text = "-1";
                this.msPyItmStButton_Click(this.msPyItmStButton, e);
            }
            else if (mytxt.Name == "trnsDateTextBox")
            {
                this.trnsDateTextBox.Text = Global.mnFrm.cmCde.checkNFormatDate(this.trnsDateTextBox.Text);
                if (this.msPyGLDateTextBox.Text != this.trnsDateTextBox.Text)
                {
                    this.msPyGLDateTextBox.Text = this.trnsDateTextBox.Text;
                }
            }
            else if (mytxt.Name == "msPyGLDateTextBox")
            {
                this.msPyGLDateTextBox.Text = Global.mnFrm.cmCde.checkNFormatDate(this.msPyGLDateTextBox.Text);
            }
            this.srchWrd = "%";
            this.obey_mspy_evnts = true;
            this.txtChngd = false;
        }

        private void vldStrtDteTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!this.obey_evnts)
            {
                return;
            }
            this.txtChngd = true;
        }

        private void vldStrtDteTextBox_Leave(object sender, EventArgs e)
        {
            if (this.txtChngd == false)
            {
                return;
            }
            this.txtChngd = false;
            TextBox mytxt = (TextBox)sender;
            this.obey_evnts = false;
            this.srchWrd = mytxt.Text;
            if (!mytxt.Text.Contains("%"))
            {
                this.srchWrd = "%" + this.srchWrd.Replace(" ", "%") + "%";
            }

            if (mytxt.Name == "vldStrtDteTextBox")
            {
                this.vldStrtDteTextBox.Text = Global.mnFrm.cmCde.checkNFormatDate(this.vldStrtDteTextBox.Text);
            }
            else if (mytxt.Name == "vldEndDteTextBox")
            {
                this.vldEndDteTextBox.Text = Global.mnFrm.cmCde.checkNFormatDate(this.vldEndDteTextBox.Text);
            }
            this.srchWrd = "%";
            this.obey_evnts = true;
            this.txtChngd = false;
        }

        private void searchForTrnsTextBox_Click(object sender, EventArgs e)
        {
            this.searchForTrnsTextBox.SelectAll();
        }

        private void reetTrnsButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.minimizeMemory();
            this.searchInTrnsComboBox.SelectedIndex = 0;
            this.searchForTrnsTextBox.Text = "%";

            this.dsplySizeTrnsComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();

            this.cur_trns_idx = 0;
            this.goTrnsButton_Click(this.goTrnsButton, e);
        }

        private void searchForInfcTextBox_Click(object sender, EventArgs e)
        {
            this.searchForInfcTextBox.SelectAll();
        }

        private void searchForItmTextBox_Click(object sender, EventArgs e)
        {
            this.searchForItmTextBox.SelectAll();
        }

        private void dsplySizeFeedComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.loadFeedItmsPanel();
            }
        }

        private void dsplySizePsblComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.loadPyItmsPanel();
            }
        }

        private void locClassTextBox_Leave(object sender, EventArgs e)
        {
            if (this.txtChngd == false)
            {
                return;
            }
            this.txtChngd = false;
            TextBox mytxt = (TextBox)sender;
            this.obey_itm_evnts = false;
            this.srchWrd = mytxt.Text;
            if (!mytxt.Text.Contains("%"))
            {
                this.srchWrd = "%" + this.srchWrd.Replace(" ", "%") + "%";
            }

            if (mytxt.Name == "locClassTextBox")
            {
                this.locClassTextBox.Text = "";
                this.locClassButton_Click(this.locClassButton, e);
            }
            else if (mytxt.Name == "costAcntNmTextBox")
            {
                this.costAcntNmTextBox.Text = "";
                this.costAcntIDTextBox.Text = "-1";
                this.cashAcntButton_Click(this.costAcntButton, e);
            }
            else if (mytxt.Name == "balsAcntNmTextBox")
            {
                this.balsAcntNmTextBox.Text = "";
                this.balsAcntIDTextBox.Text = "-1";
                this.expnsAcntButton_Click(this.balsAcntButton, e);
            }
            this.srchWrd = "%";
            this.obey_itm_evnts = true;
            this.txtChngd = false;
        }

        private void locClassTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!this.obey_itm_evnts)
            {
                return;
            }
            this.txtChngd = true;
        }

        private void resetPayItemsButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.minimizeMemory();
            this.searchInItmComboBox.SelectedIndex = 0;
            this.searchForItmTextBox.Text = "%";
            this.dsplySizeItmComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            this.dsplySizeFeedComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            this.dsplySizePsblComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            this.itm_cur_indx = 0;
            this.goItmButton_Click(this.goItmButton, e);
        }

        private void infcDte1TextBox_TextChanged(object sender, EventArgs e)
        {
            if (!this.obeyInfcEvnts)
            {
                this.txtChngd = false;
                return;
            }
            this.txtChngd = true;
        }

        private void infcDte1TextBox_Leave(object sender, EventArgs e)
        {
            if (this.txtChngd == false)
            {
                return;
            }
            this.txtChngd = false;
            TextBox mytxt = (TextBox)sender;
            this.obeyInfcEvnts = false;
            this.srchWrd = mytxt.Text;
            if (!mytxt.Text.Contains("%"))
            {
                this.srchWrd = "%" + this.srchWrd.Replace(" ", "%") + "%";
            }

            if (mytxt.Name == "infcDte1TextBox")
            {
                this.infcDte1TextBox.Text = Global.mnFrm.cmCde.checkNFormatDate(this.infcDte1TextBox.Text);
            }
            else if (mytxt.Name == "infcDte2TextBox")
            {
                this.infcDte2TextBox.Text = Global.mnFrm.cmCde.checkNFormatDate(this.infcDte2TextBox.Text);
            }

            this.srchWrd = "%";
            this.obeyInfcEvnts = true;
            this.txtChngd = false;
        }
        #endregion

        private void payRunRsltsPDFMenuItem_Click(object sender, EventArgs e)
        {
            this.payRunSlipPDF(long.Parse(this.msPyIDTextBox.Text), -1);
        }

        public void payRunSlipPDF(long msPyID, long prsnID)
        {
            if (msPyID <= 0)
            {
                Global.mnFrm.cmCde.showMsg("No Valid Pay Run Selected!", 0);
                return;
            }

            Graphics g = Graphics.FromHwnd(this.Handle);
            XPen aPen = new XPen(XColor.FromArgb(Color.Black), 1);
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
            XFont xfont111 = new XFont("Verdana", 10.00f, XFontStyle.Bold);
            XFont xfont2 = new XFont("Verdana", 10.25f, XFontStyle.Bold);
            XFont xfont4 = new XFont("Verdana", 10.0f, XFontStyle.Bold);
            XFont xfont41 = new XFont("Lucida Console", 10.0f);
            XFont xfont3 = new XFont("Lucida Console", 8.25f);
            XFont xfont31 = new XFont("Lucida Console", 10.5f, XFontStyle.Bold);
            XFont xfont311 = new XFont("Lucida Console", 10.2f, XFontStyle.Bold);
            XFont xfont5 = new XFont("Times New Roman", 6.0f, XFontStyle.Italic);

            Font font1 = new Font("Verdana", 10.25f, FontStyle.Underline | FontStyle.Bold);
            Font font11 = new Font("Verdana", 10.25f, FontStyle.Bold);
            Font font111 = new Font("Verdana", 10.0f, FontStyle.Bold);
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
            float startXNw = 40;
            float startXNww = 55;
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
            //string[] itmClsfctns = new string[50];
            double[] itmTypeTtls = new double[50];
            //double[] itmClsfctnsTtls = new double[7];
            double netPay = 0;
            int itmTypIdx = 0;
            string lastItmTyp = "";
            //int itmClsfctnIdx = 0;
            //string lastItmClsfctn = "";
            this.pageNo = 1;
            this.prntIdx = 0;
            string[] hdrs = {"Item                                                    ","      Amount (" + Global.mnFrm.cmCde.getPssblValNm(
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
                    //itmClsfctns = new string[50];
                    //itmClsfctnsTtls = new double[50];
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
                        tf.DrawString("    = " + finlStr
                          , xfont31, XBrushes.Black, rect, XStringFormats.TopLeft);

                        offsetY += (int)ght + 5;
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
                /*//Classifications
                if (dtst.Tables[0].Rows[a][22].ToString() != lastItmClsfctn)
                {
                  if (lastItmClsfctn != "")
                  {
                    itmClsfctnIdx++;
                  }
                  startX = startXNww;

                  wdth = (float)(hdrs[0].Length * 5);
                  nwLn = Global.mnFrm.cmCde.breakTxtDown(
        dtst.Tables[0].Rows[a][22].ToString(), (int)(wdth * 1.3), font11, g);
                  finlStr = "";
                  finlStr = string.Join("\n", nwLn);
                  ght = (float)gfx0.MeasureString(
                 finlStr, xfont111).Height;

                  tf = new XTextFormatter(gfx0);
                  rect = new XRect(startX, startY + offsetY, wdth + 5, ght);
                  gfx0.DrawRectangle(XBrushes.White, rect);
                  tf.DrawString(finlStr
                    , xfont111, XBrushes.Black, rect, XStringFormats.TopLeft);

                  offsetY += (int)ght + 5;

                  itmClsfctns[itmClsfctnIdx] = dtst.Tables[0].Rows[a][22].ToString();
                  lastItmClsfctn = dtst.Tables[0].Rows[a][22].ToString();
                  itmClsfctnsTtls[itmClsfctnIdx] += double.Parse(dtst.Tables[0].Rows[a][3].ToString());
                }
                else
                {
                  itmClsfctns[itmClsfctnIdx] = dtst.Tables[0].Rows[a][22].ToString();
                  lastItmClsfctn = dtst.Tables[0].Rows[a][22].ToString();
                  itmClsfctnsTtls[itmClsfctnIdx] += double.Parse(dtst.Tables[0].Rows[a][3].ToString());
                }*/
                //Item
                startX = 70;
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
                /*int chkcRwIdx = a;
                if (a < dtst.Tables[0].Rows.Count - 1)
                {
                  chkcRwIdx = a + 1;
                }
                else
                {
                  chkcRwIdx = a;
                  lastItmClsfctn = "-1234554321";
                }
                if (dtst.Tables[0].Rows[chkcRwIdx][22].ToString() != lastItmClsfctn)
                {
                  startX = startXNww;
                  orgoffsetY = offsetY;
                  string txt = itmClsfctns[itmClsfctnIdx];
                  if (txt == "Purely Informational")
                  {
                    txt = "Amount";
                  }

                  wdth = (float)(hdrs[0].Length * 5);
                  nwLn = Global.mnFrm.cmCde.breakTxtDown(
        "Total " + txt, (int)(wdth * 1.3), font31, g);
                  ght = (float)gfx0.MeasureString(
                 finlStr, xfont311).Height;

                  tf = new XTextFormatter(gfx0);
                  rect = new XRect(startX, startY + offsetY, wdth + 5, ght);
                  gfx0.DrawRectangle(XBrushes.White, rect);
                  tf.DrawString("Total " + txt
                    , xfont311, XBrushes.Black, rect, XStringFormats.TopLeft);
                  startX += wdth + 10;

                  //wdth = (float)(hdrs[0].Length * 5);
                  nwLn = Global.mnFrm.cmCde.breakTxtDown(
        itmClsfctnsTtls[itmClsfctnIdx].ToString("#,#0.00"), (int)(wdth * 1.3), font31, g);
                  finlStr = "";
                  finlStr = string.Join("\n", nwLn).PadLeft(10);
                  ght = (float)gfx0.MeasureString(
                 finlStr, xfont311).Height;

                  tf = new XTextFormatter(gfx0);
                  rect = new XRect(startX, startY + offsetY, wdth + 5, ght);
                  gfx0.DrawRectangle(XBrushes.White, rect);
                  tf.DrawString("  = " + finlStr
                    , xfont311, XBrushes.Black, rect, XStringFormats.TopLeft);

                  offsetY += (int)ght + 5;
                }*/

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
                            tf.DrawString("    = " + finlStr
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
                                tf.DrawString("    = " + finlStr
                                  , xfont31, XBrushes.Black, rect, XStringFormats.TopLeft);

                                offsetY += (int)ght + 5;

                            }
                        }
                        //itmClsfctnIdx = 0;
                        itmTypIdx = 0;
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
            //offsetY += hghstght + 5;

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
                tf.DrawString("    = " + finlStr
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
                    tf.DrawString("    = " + finlStr
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

        private void lastItemRunRsltsButton_Click(object sender, EventArgs e)
        {
            if (this.prsNamesListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Person First!", 0);
                return;
            }
            this.payRunSlipPDF(Global.get_Last_MsPyID(
              long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text),
              long.Parse(this.itmStIDMnlTextBox.Text)),
              long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));

        }

        private void addRolesMenuItem_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[12]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            //User Roles
            if (this.tabControl1.SelectedTab == this.tabPage2)
            {
                string[] selVals = new string[this.roleStListView.Items.Count];
                for (int i = 0; i < this.roleStListView.Items.Count; i++)
                {
                    selVals[0] = this.roleStListView.Items[i].SubItems[2].Text;
                }

                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                 Global.mnFrm.cmCde.getLovID("User Roles"), ref selVals, false, true);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        if (Global.doesItmSetHvRole(long.Parse(this.itmSetIDTextBox.Text),
                          int.Parse(selVals[i])) <= 0)
                        {
                            Global.createPayRole(long.Parse(this.itmSetIDTextBox.Text), -1,
                            int.Parse(selVals[i]));
                        }
                    }
                    this.populateRolesLstVw();
                }
            }
            else
            {
                string[] selVals = new string[this.roleStListView1.Items.Count];
                for (int i = 0; i < this.roleStListView1.Items.Count; i++)
                {
                    selVals[0] = this.roleStListView1.Items[i].SubItems[2].Text;
                }

                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                 Global.mnFrm.cmCde.getLovID("User Roles"), ref selVals, false, true);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        if (Global.doesPrsnSetHvRole(long.Parse(this.prsStIDTextBox.Text),
                          int.Parse(selVals[i])) <= 0)
                        {
                            Global.createPayRole(-1, long.Parse(this.prsStIDTextBox.Text),
                            int.Parse(selVals[i]));
                        }
                    }
                    this.populateRolesLstVw1();
                }
            }
        }

        private void populateRolesLstVw()
        {
            DataSet dtst = Global.get_AllRoles(
              long.Parse(this.itmSetIDTextBox.Text));
            this.roleStListView.Items.Clear();
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                ListViewItem nwItm = new ListViewItem(new string[] {
          (1+ i).ToString(),
                dtst.Tables[0].Rows[i][1].ToString(),
          dtst.Tables[0].Rows[i][0].ToString(),
          dtst.Tables[0].Rows[i][2].ToString()});
                this.roleStListView.Items.Add(nwItm);
            }
            if (this.roleStListView.Items.Count > 0)
            {
                this.roleStListView.Items[0].Selected = true;
            }
        }

        private void populateRolesLstVw1()
        {
            DataSet dtst = Global.get_AllRoles1(
              long.Parse(this.prsStIDTextBox.Text));
            this.roleStListView1.Items.Clear();
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                ListViewItem nwItm = new ListViewItem(new string[] {
          (1+ i).ToString(),
                dtst.Tables[0].Rows[i][1].ToString(),
          dtst.Tables[0].Rows[i][0].ToString(),
          dtst.Tables[0].Rows[i][2].ToString()});
                this.roleStListView1.Items.Add(nwItm);
            }
            if (this.roleStListView1.Items.Count > 0)
            {
                this.roleStListView1.Items[0].Selected = true;
            }
        }

        private void delRolesMenuItem_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[12]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.tabControl1.SelectedTab == this.tabPage2)
            {
                if (this.roleStListView.SelectedItems.Count <= 0)
                {
                    Global.mnFrm.cmCde.showMsg("Please select the Role(s) to Delete", 0);
                    return;
                }
                int cnt = this.roleStListView.SelectedItems.Count;
                for (int i = 0; i < cnt; i++)
                {
                    Global.mnFrm.cmCde.deleteGnrlRecs(long.Parse(this.roleStListView.SelectedItems[i].SubItems[3].Text),
                    "Item Set Name = " + this.itmSetNmTextBox.Text, "pay.pay_sets_allwd_roles", "pay_roles_id");
                }
                this.populateRolesLstVw();
            }
            else
            {
                if (this.roleStListView1.SelectedItems.Count <= 0)
                {
                    Global.mnFrm.cmCde.showMsg("Please select the Role(s) to Delete", 0);
                    return;
                }
                int cnt = this.roleStListView1.SelectedItems.Count;
                for (int i = 0; i < cnt; i++)
                {
                    Global.mnFrm.cmCde.deleteGnrlRecs(long.Parse(this.roleStListView1.SelectedItems[i].SubItems[3].Text),
                    "Person Set Name = " + this.prsStNmTextBox.Text, "pay.pay_sets_allwd_roles", "pay_roles_id");
                }
                this.populateRolesLstVw1();
            }
        }

        private void exptRolesMenuItem_Click(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedTab == this.tabPage2)
            {
                Global.mnFrm.cmCde.exprtToExcel(this.roleStListView);
            }
            else
            {
                Global.mnFrm.cmCde.exprtToExcel(this.roleStListView1);
            }
        }

        private void rcHstryRolesMenuItem_Click(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedTab == this.tabPage2)
            {
                if (this.roleStListView.SelectedItems.Count <= 0)
                {
                    Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                    return;
                }
                Global.mnFrm.cmCde.showRecHstry(
                  Global.mnFrm.cmCde.get_Gnrl_Create_Hstry(long.Parse(
                  this.roleStListView.SelectedItems[0].SubItems[3].Text),
                  "pay.pay_sets_allwd_roles", "pay_roles_id"), 7);
            }
            else
            {
                if (this.roleStListView1.SelectedItems.Count <= 0)
                {
                    Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                    return;
                }
                Global.mnFrm.cmCde.showRecHstry(
                  Global.mnFrm.cmCde.get_Gnrl_Create_Hstry(long.Parse(
                  this.roleStListView1.SelectedItems[0].SubItems[3].Text),
                  "pay.pay_sets_allwd_roles", "pay_roles_id"), 7);
            }

        }

        private void runRptButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showRptParamsDiag(-1, Global.mnFrm.cmCde);
        }

        private void printRcptButton_Click(object sender, EventArgs e)
        {
            this.printPprPstMenuItem_Click(this.printPprPstMenuItem, e);
        }

        private void printRcptButton1_Click(object sender, EventArgs e)
        {
            this.printPprPstMenuItem_Click(this.printPprPstMenuItem, e);
        }

        private void printPrvwRcptButton_Click(object sender, EventArgs e)
        {
            this.printPstPyMenuItem_Click(this.printPstPyMenuItem, e);
        }

        private void deletePayMenuItem_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[19]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.pstPayListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the Payment to DELETE!", 0);
                return;
            }
            if (long.Parse(this.pstPayListView.SelectedItems[0].SubItems[5].Text) <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Invalid Payment Selected!", 0);
                return;
            }

            if (Global.isMnlpyInUse(long.Parse(this.pstPayListView.SelectedItems[0].SubItems[5].Text)) == true)
            {
                Global.mnFrm.cmCde.showMsg("This Payment has been SENT to GL or has not been REVERSED hence cannot be DELETED!", 0);
                return;
            }
            long rvrslmnlpyid = Global.getPymntRvrsal(long.Parse(this.pstPayListView.SelectedItems[0].SubItems[5].Text));

            if (rvrslmnlpyid <= 0)
            {
                Global.mnFrm.cmCde.showMsg("This Payment has not been REVERSED hence cannot be DELETED!", 0);
                return;
            }

            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE " +
              "the selected Payment and its Reversal?" +
         "\r\nThis action cannot be undone!", 1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }

            Global.mnFrm.cmCde.deleteGnrlRecs(rvrslmnlpyid,
      this.pstPayListView.SelectedItems[0].SubItems[6].Text + " (Reversal)", "pay.pay_itm_trnsctns", "pay_trns_id");

            Global.mnFrm.cmCde.deleteGnrlRecs(long.Parse(this.pstPayListView.SelectedItems[0].SubItems[5].Text),
      this.pstPayListView.SelectedItems[0].SubItems[6].Text, "pay.pay_itm_trnsctns", "pay_trns_id");

            this.populatePstListVw();
        }

        private void salesItemButton_Click(object sender, EventArgs e)
        {
            if (this.additm == false && this.edititm == false)
            {
                Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                return;
            }

            if (this.itmMajTypComboBox.Text == "Balance Item"
              || this.itmMajTypComboBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("This feature is for Non Balance Items Only!", 0);
                return;
            }

            string[] selVals = new string[1];
            selVals[0] = this.salesItemIDTextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Inventory Items"), ref selVals,
                true, false, Global.mnFrm.cmCde.Org_id,
             this.srchWrd, "Both", true);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.salesItemIDTextBox.Text = selVals[i];
                    this.salesItemTextBox.Text = Global.get_InvItemNm(
                     int.Parse(selVals[i]));
                    //this.priceLabel.Text = Global.get_InvItemPrice(int.Parse(selVals[i])).ToString("#,##0.00");
                }
            }
        }

        private void isRetroCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shdObeyItmEvts() == false
             || beenToCheckBx == true)
            {
                beenToCheckBx = false;
                return;
            }
            beenToCheckBx = true;
            if (this.additm == false && this.edititm == false)
            {
                this.isRetroCheckBox.Checked = !this.isRetroCheckBox.Checked;
            }
        }

        private void retroButton_Click(object sender, EventArgs e)
        {
            if (this.additm == false && this.edititm == false)
            {
                Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                return;
            }
            if (this.itmMajTypComboBox.Text == "Balance Item"
              || this.itmMajTypComboBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("This feature is for Non Balance Items Only!", 0);
                return;
            }
            string[] selVals = new string[1];
            selVals[0] = this.retroIDTextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Retro Pay Items"), ref selVals,
                true, false, Global.mnFrm.cmCde.Org_id,
             this.srchWrd, "Both", true);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.retroIDTextBox.Text = selVals[i];
                    this.retroNmTextBox.Text = Global.get_PayItemNm(int.Parse(selVals[i]));
                    //this.priceLabel.Text = Global.get_InvItemPrice(int.Parse(selVals[i])).ToString("#,##0.00");
                }
            }
        }

        private void vwSelfCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.beenToCheckBx == true)
            {
                this.beenToCheckBx = false;
                return;
            }
            this.beenToCheckBx = true;
            if (this.vwAllMsPays == false)
            {
                this.vwSelfCheckBox.Checked = !this.vwSelfCheckBox.Checked;
            }

        }

        private void allwEditCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shdObeyItmEvts() == false
             || beenToCheckBx == true)
            {
                beenToCheckBx = false;
                return;
            }
            beenToCheckBx = true;
            if (this.additm == false && this.edititm == false)
            {
                this.allwEditCheckBox.Checked = !this.allwEditCheckBox.Checked;
            }
        }

        private void leftTreeView_Click(object sender, EventArgs e)
        {
            if (this.leftTreeView.SelectedNode == null)
            {
                return;
            }
            this.loadCorrectPanel(this.leftTreeView.SelectedNode.Text);
        }

        private void createsAccntngCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shdObeyItmEvts() == false
             || beenToCheckBx == true)
            {
                beenToCheckBx = false;
                return;
            }
            beenToCheckBx = true;
            if (this.additm == false && this.edititm == false)
            {
                this.createsAccntngCheckBox.Checked = !this.createsAccntngCheckBox.Checked;
            }
        }

        private void jasperPaySlipMenuItem_Click(object sender, EventArgs e)
        {
            string trnsDate = DateTime.ParseExact(
               this.trnsDateTextBox.Text, "dd-MMM-yyyy HH:mm:ss",
               System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

            string reportName = Global.mnFrm.cmCde.getEnbldPssblValDesc("Pay Slip",
       Global.mnFrm.cmCde.getLovID("Document Custom Print Process Names"));
            string reportTitle = "Pay Slip";
            string paramRepsNVals = "{:fromDate}~" + trnsDate + "|{:orgID}~" + Global.mnFrm.cmCde.Org_id + "|{:toDate}~" + trnsDate + "|{:documentTitle}~" + reportTitle;
            Global.mnFrm.cmCde.showRptParamsDiag(Global.mnFrm.cmCde.getRptID(reportName), Global.mnFrm.cmCde, paramRepsNVals, reportTitle);
        }

        private void payeTaxRatesButton_Click(object sender, EventArgs e)
        {
            payeRatesDiag nwDiag = new payeRatesDiag();
            if (nwDiag.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void newInvoiceButton_Click(object sender, EventArgs e)
        {
            /*if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[9]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                  " this action!\nContact your System Administrator!", 0);
                return;
            }*/
            //Global3.refreshRqrdVrbls();
            invoiceForm nwDiag = new invoiceForm();
            long ctmrID = -1;
            if (this.prsNamesListView.SelectedItems.Count > 0)
            {
                ctmrID = this.checkNCreateCstmr(long.Parse(this.prsNamesListView.SelectedItems[0].SubItems[3].Text));
                nwDiag.inputCstmrID = ctmrID;
                nwDiag.inputItemSetNm = this.itmStNmMnlTextBox.Text;
                nwDiag.Show();
            }
            else
            {
                Global.mnFrm.cmCde.showMsg("Please select a Person First!", 0);
                return;
            }
        }

        private long checkNCreateCstmr(long prsnID)
        {
            long cstmrID = -1;
            long.TryParse(Global.mnFrm.cmCde.getGnrlRecNm(
      "scm.scm_cstmr_suplr", "lnkd_prsn_id", "cust_sup_id",
      prsnID), out cstmrID);
            if (cstmrID <= 0)
            {
                DataSet prsDtst = Global3.get_PrsnCstmrDet(prsnID);
                if (prsDtst.Tables[0].Rows.Count > 0)
                {
                    string fllnm = prsDtst.Tables[0].Rows[0][0].ToString();
                    string gndr = prsDtst.Tables[0].Rows[0][1].ToString();

                    string dob = prsDtst.Tables[0].Rows[0][2].ToString();

                    string telNos = prsDtst.Tables[0].Rows[0][3].ToString();
                    string eml = prsDtst.Tables[0].Rows[0][4].ToString();
                    string siteNm = "OFFICE";// Global.mnFrm.cmCde.getOrgName(Global.mnFrm.cmCde.Org_id);
                    string bllng = prsDtst.Tables[0].Rows[0][5].ToString();
                    string shpAdrs = prsDtst.Tables[0].Rows[0][6].ToString();

                    string ntnlty = prsDtst.Tables[0].Rows[0][7].ToString();

                    Global3.createCstSplrRec(Global.mnFrm.cmCde.Org_id, fllnm, fllnm, "Customer", "Individual",
                      Global3.get_DfltSalesLbltyAcnt(Global.mnFrm.cmCde.Org_id),
                      Global3.get_DfltRcvblAcnt(Global.mnFrm.cmCde.Org_id), prsnID, gndr, dob, true, "",
                      "", "", "", "", "", "", "", 0, "", "");
                    long.TryParse(Global3.mnFrm.cmCde1.getGnrlRecNm(
          "scm.scm_cstmr_suplr", "lnkd_prsn_id", "cust_sup_id",
          prsnID), out cstmrID);
                    if (cstmrID > 0)
                    {
                        Global3.createCstSplrSiteRec(cstmrID, siteNm, siteNm, fllnm, telNos,
                          eml, "", "", "", bllng, shpAdrs, -1,
                          -1, "", ntnlty, "", "", "", "", "", true, "", -1);
                    }
                }
            }
            return cstmrID;
        }

        private void storeButton_Click(object sender, EventArgs e)
        {
            string[] selVals = new string[1];
            selVals[0] = this.storeIDTextBox.Text;
            DialogResult dgRes = Global3.mnFrm.cmCde.showPssblValDiag(
                Global3.mnFrm.cmCde.getLovID("Users' Sales Stores"), ref selVals,
                true, false, Global3.mnFrm.cmCde.Org_id,
                Global3.myInv.user_id.ToString(), "");
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.storeIDTextBox.Text = selVals[i];
                    this.storeNmTextBox.Text = Global3.mnFrm.cmCde.getGnrlRecNm(
                      "inv.inv_itm_subinventories", "subinv_id", "subinv_name",
                      long.Parse(selVals[i]));
                    Global3.selectedStoreID = int.Parse(selVals[i]);
                }
            }
        }
    }
}

