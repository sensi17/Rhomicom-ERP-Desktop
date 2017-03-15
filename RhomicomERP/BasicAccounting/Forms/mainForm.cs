using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Accounting.Classes;
using Accounting.Dialogs;
using Microsoft.VisualBasic;
using Npgsql;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;

namespace Accounting.Forms
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
        public int funCurID = -1;
        public string funcCurCode = "";
        string[] menuItems = {"Chart of Accounts",
        "Journal Entries", "Petty Cash Vouchers", "Transactions Search", "Financial Statements"
    , "Budgets", "Transaction Templates","Accounting Periods", "Assets/Investments"
    ,"Payable Invoices","Receivable Invoices","Invoice Payments","Business Partners/Firms","Tax Codes",
    "Default Accounts","Account Reconciliation"};
        string[] menuImages = {"AccountingIcon1.png", "generaljournal.png", "cashbook_big_icon.png", "CustomIcon.png"
        ,"tbals.jpg"    ,"bdgt.jpg", "tmplt.jpg", "calendar2.ico", "assets1.jpg", "pybls1.jpg", "rcvbls1.jpg"
    ,"pymnts1.jpg", "cstmrs1.jpg", "tax1.jpg", "dfltAccnts1.jpg", "mi_scare_report.png","Notebook.png"};
        //Chart of Accounts Panel Variables;
        Int64 chrt_cur_indx = 0;
        bool is_last_chrt = false;
        Int64 totl_chrt = 0;
        long last_chrt_num = 0;
        public string chrt_SQL = "";
        public string chrtDet_SQL = "";
        public string rates_SQL = "";
        bool obey_chrt_evnts = false;
        bool addChrt = false;
        bool editChrt = false;
        bool beenToIsprntfunc = false;
        bool beenToIsContra = false;
        bool beenToIsRetEarns = false;
        bool beenToIsNetInc = false;
        bool beenToIsEnabled = false;
        bool beenClicked = false;
        bool addAccounts = false;
        bool editAccounts = false;
        bool delAccounts = false;

        //Accounts Transactions Panel Variables;
        Int64 trns_cur_indx = 0;
        bool is_last_trns = false;
        Int64 totl_trns = 0;
        long last_trns_num = 0;
        public string trns_SQL = "";
        public string trnsDet_SQL = "";
        public string pymntsGvn_SQL = "";
        public string tmpltDiag_SQL = "";
        bool obey_trns_evnts = false;
        bool addTrns = false;
        bool editTrns = false;
        bool addBatches = false;
        bool editBatches = false;
        bool delBatches = false;
        bool addTrscns = false;
        bool editTrscns = false;
        bool delTrscns = false;
        bool addTrscnsFrmTmp = false;
        bool postTrscns = false;
        //Transactions Details;
        long tdet_cur_indx = 0;
        bool is_last_tdet = false;
        long totl_tdet = 0;
        long last_tdet_num = 0;
        public string tdet_SQL = "";
        bool obey_tdet_evnts = false;
        //Transactions Search
        private long totl_srch = 0;
        private long cur_srch_idx = 0;
        public string vwsrchSQLStmnt = "";
        private bool is_last_srch = false;
        bool obeySrchEvnts = false;
        long last_srch_num = 0;

        public string tbalsSQLStmnt = "";
        public string pnlSQLStmnt = "";
        public string periodSQLStmnt = "";
        public string cashFlowSQLStmnt = "";
        public string blshtSQLStmnt = "";
        //Transaction Templates Panel Variables;
        Int64 tmplt_cur_indx = 0;
        bool is_last_tmplt = false;
        Int64 totl_tmplt = 0;
        long last_tmplt_num = 0;
        public string tmplt_SQL = "";
        public string tmpltDet_SQL = "";
        public string tmpltUsrs_SQL = "";
        bool obey_tmplt_evnts = false;
        bool addTmplt = false;
        bool editTmplt = false;
        bool addTmplts = false;
        bool editTmplts = false;
        bool delTmplts = false;

        public string bls_SQL = "";
        public string subldgr_SQL = "";
        public string blsDet_SQL = "";
        public string accntStmntSQL = "";
        bool obey_bls_evnts = false;
        //Budget Panel Variables;
        Int64 bdgt_cur_indx = 0;
        bool is_last_bdgt = false;
        Int64 totl_bdgt = 0;
        long last_bdgt_num = 0;
        public string bdgt_SQL = "";
        public string bdgtDet_SQL = "";
        bool obey_bdgt_evnts = false;
        bool addbdgt = false;
        bool editbdgt = false;
        bool addBudgets = false;
        bool editBudgets = false;
        bool delBudgets = false;

        //Budget Details;
        long bdgtDt_cur_indx = 0;
        bool is_last_bdgtDt = false;
        long totl_bdgtDt = 0;
        long last_bdgtDt_num = 0;
        bool obey_bdgtDt_evnts = false;

        Color[] clrs;
        //Memo Account Variables;
        long ima_cur_indx = 0;
        bool is_last_ima = false;
        long totl_ima = 0;
        long last_ima_num = 0;
        public string ima_SQL = "";
        bool obey_ima_evnts = false;
        bool addima = false;
        bool editima = false;

        bool addimas = false;
        bool editimas = false;
        bool delimas = false;

        //Informational/Memo Account Details Variables
        long imadt_cur_indx = 0;
        bool is_last_imadt = false;
        long totl_imadt = 0;
        long last_imadt_num = 0;
        public string imadt_SQL = "";
        bool obey_imadt_evnts = false;
        //Reconciliation
        public int orgid = -1;
        public long batchid = -1;
        public int curid = -1;
        public string curCode = "";
        #endregion

        #region "FORM EVENTS..."
        public mainForm()
        {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            this.obey_evnts = false;
            this.accDndLabel.Visible = false;
            Global.myBscActn.Initialize();
            Global.mnFrm = this;

            //Global.mnFrm.cmCde.pgSqlConn = this.gnrlSQLConn;
            Global.mnFrm.cmCde.Login_number = this.lgn_num;
            Global.mnFrm.cmCde.Role_Set_IDs = this.role_st_id;
            Global.mnFrm.cmCde.User_id = this.usr_id;
            Global.mnFrm.cmCde.Org_id = this.Og_id;

            this.trnsctnsPanel.Visible = false;
            this.trnsSearchPanel.Visible = false;
            this.trialBalancePanel.Visible = false;
            this.prftnlossPanel.Visible = false;
            this.balSheetPanel.Visible = false;
            this.budgetPanel.Visible = false;
            this.trnsTmpltsPanel.Visible = false;
            this.finStmntsPanel.Visible = false;
            this.otherFormsPanel.Visible = false;
            System.Windows.Forms.Application.DoEvents();
            this.clrs = Global.mnFrm.cmCde.getColors();
            this.BackColor = clrs[0];
            this.tabPage1.BackColor = clrs[0];//Color.Transparent;// 
            this.tabPage2.BackColor = clrs[0];
            this.tabPage3.BackColor = clrs[0];
            this.tabPage4.BackColor = clrs[0];
            this.tabPage5.BackColor = clrs[0];
            this.tabPage6.BackColor = clrs[0];
            this.tabPage7.BackColor = clrs[0];
            this.tabPage8.BackColor = clrs[0];
            this.tabPage9.BackColor = clrs[0];
            this.tabPage10.BackColor = clrs[0];
            this.tabPage11.BackColor = clrs[0];
            this.tabPage12.BackColor = clrs[0];
            this.tabPage13.BackColor = clrs[0];
            this.tabPage14.BackColor = clrs[0];
            this.tabPage15.BackColor = clrs[0];
            this.tabPage16.BackColor = clrs[0];
            this.tabPage17.BackColor = clrs[0];
            this.tabPage18.BackColor = clrs[0];
            this.tabPage19.BackColor = clrs[0];
            this.tabPage20.BackColor = clrs[0];
            this.tabPage21.BackColor = clrs[0];
            this.tabPage22.BackColor = clrs[0];
            this.tabPage23.BackColor = clrs[0];

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

            this.tbalTabPage.BackColor = clrs[0];
            this.pnlTabPage.BackColor = clrs[0];
            this.balsShtTabPage.BackColor = clrs[0];
            this.subLedgerTabPage.BackColor = clrs[0];
            this.accntStmntTabPage.BackColor = clrs[0];
            System.Windows.Forms.Application.DoEvents();

            Global.refreshRqrdVrbls();

            //Global.myBscActn.loadMyRolesNMsgtyps();
            bool vwAct = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[0]);
            if (!vwAct)
            {
                this.Controls.Clear();
                this.Controls.Add(this.accDndLabel);
                this.accDndLabel.Visible = true;
                return;
            }
            this.showAllPanels();

            this.isPostedCheckBox.Checked = true;
            this.disableFormButtons();
            //Global.createRqrdLOVs();
            //Global.createRqrdLOVs1();
            this.funCurID = Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id);
            this.funcCurCode = Global.mnFrm.cmCde.getPssblValNm(funCurID);
            this.curid = this.funCurID;
            this.curCode = this.funcCurCode;
            //Global.updtOrgAccntCurrID(Global.mnFrm.cmCde.Org_id, this.funCurID);
            int suspns_accnt = Global.get_Suspns_Accnt(Global.mnFrm.cmCde.Org_id);
            int accntID = Global.mnFrm.cmCde.getAccntID("01SYSTEM_SUSPENSE13040", Global.mnFrm.cmCde.Org_id);
            if (suspns_accnt <= -1 && accntID <= -1)
            {
                Global.createChrt(Global.mnFrm.cmCde.Org_id, "01SYSTEM_SUSPENSE13040", "Suspense Account"
                  , "Suspense Account", false, -1, "A", false, true, false, false, 100, false, -1, this.funCurID, true, "",
                  -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1);
            }
            System.Windows.Forms.Application.DoEvents();
            this.populateTreeView();
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
            this.obey_evnts = true;

        }

        private void mainForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            Global.myBscActn.Dispose();
        }
        #endregion

        #region "GENERAL..."
        private void populateTreeView()
        {
            String neededMdls = "";
            if (Global.mnFrm.cmCde.User_id > 0)
            {
                neededMdls = Global.mnFrm.cmCde.getGnrlRecNm("sec.sec_users", "user_id", "modules_needed", Global.mnFrm.cmCde.User_id);
                if ((!neededMdls.Contains("Only") && !neededMdls.Contains("Modules")) || neededMdls == "")
                {
                    int lvid = Global.mnFrm.cmCde.getLovID("Rhomicom Software Licenses");
                    neededMdls = Global.mnFrm.cmCde.decrypt(Global.mnFrm.cmCde.getEnbldPssblValDesc("Modules/Packages Needed", lvid), CommonCode.CommonCodes.AppKey);
                    if (neededMdls.Contains("Only") || neededMdls.Contains("Modules"))
                    {
                        CommonCode.CommonCodes.ModulesNeeded = neededMdls;
                    }
                    else
                    {
                        CommonCode.CommonCodes.ModulesNeeded = "Person Records Only";
                    }
                }
                else
                {
                    CommonCode.CommonCodes.ModulesNeeded = neededMdls;
                }
            }
            else
            {
                CommonCode.CommonCodes.ModulesNeeded = "Person Records Only";
            }
            if (this.backgroundWorker1.IsBusy == true)
            {
                return;
            }
            if (!Global.mnFrm.cmCde.isThsMchnPrmtd())
            {
                Global.mnFrm.cmCde.showMsg("This Machine is not Permitted to run this software!\r\nContact the Vendor for Assistance!", 4);
                return;
            }
            this.tabControl1.Controls.Clear();

            for (int i = 0; i < menuItems.Length; i++)
            {
                if (i == 2)
                {
                    if (CommonCode.CommonCodes.ModulesNeeded == "Point of Sale Only")
                    {
                        continue;
                    }
                    if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[0] +
                     "~" + Global.dfltPrvldgs[97]) == false)
                    {
                        continue;
                    }
                }
                else if (i == 4)
                {
                    if (CommonCode.CommonCodes.ModulesNeeded == "Point of Sale Only")
                    {
                        continue;
                    }
                    if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[0] +
                     "~" + Global.dfltPrvldgs[29]) == false)
                    {
                        continue;
                    }
                }
                else if (i == 5)
                {
                    if (CommonCode.CommonCodes.ModulesNeeded == "Point of Sale Only")
                    {
                        continue;
                    }
                    if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[0] +
                    "~" + Global.dfltPrvldgs[7]) == false)
                    {
                        continue;
                    }
                }
                else if (i == 6)
                {
                    if (CommonCode.CommonCodes.ModulesNeeded == "Point of Sale Only")
                    {
                        continue;
                    }
                    if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[0] +
                    "~" + Global.dfltPrvldgs[8]) == false)
                    {
                        continue;
                    }
                }
                else if (i == 11)
                {
                    if (CommonCode.CommonCodes.ModulesNeeded == "Point of Sale Only")
                    {
                        continue;
                    }
                    if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[0] +
                    "~" + Global.dfltPrvldgs[41]) == false)
                    {
                        continue;
                    }
                }
                else if (i == 8)
                {
                    if (CommonCode.CommonCodes.ModulesNeeded == "Point of Sale Only")
                    {
                        continue;
                    }
                    if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[0] +
                    "~" + Global.dfltPrvldgs[40]) == false)
                    {
                        continue;
                    }
                }
                else if (i < 11)
                {
                    if ((i == 1 || i == 3 || i == 9 || i == 10) && CommonCode.CommonCodes.ModulesNeeded == "Point of Sale Only")
                    {
                        continue;
                    }
                    else if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[0] +
                     "~" + Global.dfltPrvldgs[i + 22]) == false)
                    {
                        continue;
                    }
                }
                else
                {
                    if ((i == 11 || i == 15) && CommonCode.CommonCodes.ModulesNeeded == "Point of Sale Only")
                    {
                        continue;
                    }
                    if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[0] +
                     "~" + Global.dfltPrvldgs[i + 21]) == false)
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
        }

        public void loadCorrectPanel(string inpt_name)
        {
            this.statusLoadLabel.Visible = false;
            this.statusLoadPictureBox.Visible = false;
            if (this.backgroundWorker1.IsBusy == true)
            {
                return;
            }
            Global.currentPanel = inpt_name;
            // this.disableFormButtons(inpt_name);
            if (inpt_name == menuItems[0])
            {
                this.showATab(ref this.tabPage1);
                this.changeOrg();
                if (this.strtDteIMATextBox.Text == "")
                {
                    this.strtDteIMATextBox.Text = DateTime.ParseExact(
                      Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
                      System.Globalization.CultureInfo.InvariantCulture).AddMonths(-24).ToString("dd-MMM-yyyy 00:00:00");
                }
                if (this.endDteIMATextBox.Text == "")
                {
                    this.endDteIMATextBox.Text = DateTime.ParseExact(
                      Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
                      System.Globalization.CultureInfo.InvariantCulture).AddMonths(24).ToString("01-MMM-yyyy 23:59:59");
                }


                this.loadAccntChrtPanel();
                this.loadIMAPanel();
            }
            else if (inpt_name == menuItems[1])
            {
                this.showATab(ref this.tabPage2);
                this.changeOrg();
                this.loadAccntTrnsPanel();
            }
            else if (inpt_name == menuItems[2])
            {
                this.showATab(ref this.tabPage23);
                this.changeOrg();

                Global.ptycshFrm = (pettyCashDocsForm)Global.isFormAlreadyOpen(typeof(pettyCashDocsForm));
                if (Global.ptycshFrm == null)
                {
                    Global.ptycshFrm = new pettyCashDocsForm();
                    Global.ptycshFrm.TopLevel = false;
                    Global.ptycshFrm.FormBorderStyle = FormBorderStyle.None;
                    Global.ptycshFrm.Dock = DockStyle.Fill;
                    this.tabPage23.Controls.Add(Global.ptycshFrm);
                    Global.ptycshFrm.BackColor = clrs[0];
                    Global.ptycshFrm.loadPrvldgs();
                    Global.ptycshFrm.disableFormButtons();

                    Global.ptycshFrm.Show();
                    Global.ptycshFrm.BringToFront();
                    System.Windows.Forms.Application.DoEvents();
                    Global.ptycshFrm.loadPanel();
                }
                else
                {
                    Global.ptycshFrm.BringToFront();
                }
            }
            else if (inpt_name == menuItems[3])
            {
                if (this.vldEndDteTextBox.Text == "")
                {
                    this.vldEndDteTextBox.Text = DateTime.ParseExact(
                      Global.mnFrm.cmCde.getDB_Date_time().Substring(0, 4) + "-12-31 23:59:59", "yyyy-MM-dd HH:mm:ss",
                      System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy 23:59:59");
                }
                if (this.vldStrtDteTextBox.Text == "")
                {
                    this.vldStrtDteTextBox.Text = DateTime.ParseExact(
                      Global.mnFrm.cmCde.getDB_Date_time().Substring(0, 4) + "-01-01 00:00:00", "yyyy-MM-dd HH:mm:ss",
                      System.Globalization.CultureInfo.InvariantCulture).ToString("01-MMM-yyyy 00:00:00");
                }
                this.showATab(ref this.tabPage3);
                this.changeOrg();
                this.loadSrchPanel();
            }
            else if (inpt_name == menuItems[4])
            {
                this.showATab(ref this.tabPage4);
                this.changeOrg();
                this.obey_evnts = false;
                if (this.tbalDteTextBox.Text == "")
                {
                    this.tbalDteTextBox.Text = DateTime.ParseExact(
                      "01" + Global.mnFrm.cmCde.getFrmtdDB_Date_time().Substring(2), "dd-MMM-yyyy HH:mm:ss",
                      System.Globalization.CultureInfo.InvariantCulture).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy 23:59:59");
                }
                if (this.plDate1TextBox.Text == "")
                    this.plDate1TextBox.Text = DateTime.ParseExact(
                      Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
                      System.Globalization.CultureInfo.InvariantCulture).ToString("01-MMM-yyyy 00:00:00");
                if (this.plDate2TextBox.Text == "")
                    this.plDate2TextBox.Text = DateTime.ParseExact(
                      "01" + Global.mnFrm.cmCde.getFrmtdDB_Date_time().Substring(2), "dd-MMM-yyyy HH:mm:ss",
                      System.Globalization.CultureInfo.InvariantCulture).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy 23:59:59");
                if (this.asAtDteTextBox.Text == "")
                {
                    this.asAtDteTextBox.Text = DateTime.ParseExact(
                      "01" + Global.mnFrm.cmCde.getFrmtdDB_Date_time().Substring(2), "dd-MMM-yyyy HH:mm:ss",
                      System.Globalization.CultureInfo.InvariantCulture).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy 23:59:59");
                }
                if (this.subledgrDteTextBox.Text == "")
                {
                    this.subledgrDteTextBox.Text = DateTime.ParseExact(
                      "01" + Global.mnFrm.cmCde.getFrmtdDB_Date_time().Substring(2), "dd-MMM-yyyy HH:mm:ss",
                      System.Globalization.CultureInfo.InvariantCulture).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy 23:59:59");
                }
                if (this.mnthlyEndDteTextBox.Text == "")
                {
                    this.mnthlyEndDteTextBox.Text = DateTime.ParseExact(
                      Global.mnFrm.cmCde.getDB_Date_time().Substring(0, 4) + "-12-31 23:59:59", "yyyy-MM-dd HH:mm:ss",
                      System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy 23:59:59");
                }
                if (this.mnthlyStrtDteTextBox.Text == "")
                {
                    this.mnthlyStrtDteTextBox.Text = DateTime.ParseExact(
                      Global.mnFrm.cmCde.getDB_Date_time().Substring(0, 4) + "-01-01 00:00:00", "yyyy-MM-dd HH:mm:ss",
                      System.Globalization.CultureInfo.InvariantCulture).ToString("01-MMM-yyyy 00:00:00");
                }
                if (this.mnthlyDrtnComboBox.SelectedIndex < 0)
                {
                    this.mnthlyDrtnComboBox.SelectedIndex = 3;
                }
                if (this.mnthlyAccTypComboBox.SelectedIndex < 0)
                {
                    this.mnthlyAccTypComboBox.SelectedIndex = 4;
                }

                if (this.cashFlowEndTextBox.Text == "")
                {
                    this.cashFlowEndTextBox.Text = DateTime.ParseExact(
                      Global.mnFrm.cmCde.getDB_Date_time().Substring(0, 4) + "-12-31 23:59:59", "yyyy-MM-dd HH:mm:ss",
                      System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy 23:59:59");
                }
                if (this.cashFlowStrtTextBox.Text == "")
                {
                    this.cashFlowStrtTextBox.Text = DateTime.ParseExact(
                      Global.mnFrm.cmCde.getDB_Date_time().Substring(0, 4) + "-01-01 00:00:00", "yyyy-MM-dd HH:mm:ss",
                      System.Globalization.CultureInfo.InvariantCulture).ToString("01-MMM-yyyy 00:00:00");
                }
                if (this.cashFlowDrtnComboBox.SelectedIndex < 0)
                {
                    this.cashFlowDrtnComboBox.SelectedIndex = 3;
                }
                if (this.cashFlowTypComboBox.SelectedIndex < 0)
                {
                    this.cashFlowTypComboBox.SelectedIndex = 3;
                }

                if (this.strtDteAccntStmntTextBox.Text == "")
                    this.strtDteAccntStmntTextBox.Text = DateTime.ParseExact(
                      Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
                      System.Globalization.CultureInfo.InvariantCulture).ToString("01-MMM-yyyy 00:00:00");
                if (this.endDteAccntStmntTextBox.Text == "")
                    this.endDteAccntStmntTextBox.Text = DateTime.ParseExact(
                      "01" + Global.mnFrm.cmCde.getFrmtdDB_Date_time().Substring(2), "dd-MMM-yyyy HH:mm:ss",
                      System.Globalization.CultureInfo.InvariantCulture).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy 23:59:59");
                this.obey_evnts = true;

            }
            else if (inpt_name == menuItems[5])
            {
                this.showATab(ref this.tabPage5);
                this.changeOrg();
                this.loadBdgtPanel();
            }
            else if (inpt_name == menuItems[6])
            {
                this.showATab(ref this.tabPage6);
                this.changeOrg();
                this.loadTmpltsPanel();
            }
            else if (inpt_name == menuItems[7])
            {
                //this.otherFormsPanel.Controls.Clear();
                //Global.actnPrdFrm = null;
                this.showATab(ref this.tabPage7);
                this.changeOrg();
                long clndrID = -1;
                long.TryParse(Global.mnFrm.cmCde.getGnrlRecNm(
                  "accb.accb_periods_hdr", "org_id", "periods_hdr_id",
                  Global.mnFrm.cmCde.Org_id), out clndrID);
                if (clndrID <= 0)
                {
                    Global.createPeriodsHdr(Global.mnFrm.cmCde.Org_id,
                      "Accounting Calendar (" + Global.mnFrm.cmCde.getOrgName(Global.mnFrm.cmCde.Org_id) + ")",
                      "Accounting Calendar (" + Global.mnFrm.cmCde.getOrgName(Global.mnFrm.cmCde.Org_id) + ")",
                      "2-Monthly", true, "Transactions not Allowed Days", "Transactions not Allowed Dates");
                }
                Global.actnPrdFrm = (acctnPrdsForm)Global.isFormAlreadyOpen(typeof(acctnPrdsForm));
                if (Global.actnPrdFrm == null)
                {
                    Global.actnPrdFrm = new acctnPrdsForm();
                    Global.actnPrdFrm.TopLevel = false;
                    Global.actnPrdFrm.FormBorderStyle = FormBorderStyle.None;
                    Global.actnPrdFrm.Dock = DockStyle.Fill;
                    this.otherFormsPanel.Controls.Add(Global.actnPrdFrm);
                    Global.actnPrdFrm.BackColor = clrs[0];
                    Global.actnPrdFrm.glsLabel3.TopFill = clrs[0];
                    Global.actnPrdFrm.glsLabel3.BottomFill = clrs[1];
                    //Global.actnPrdFrm.loadPrvldgs();
                    Global.actnPrdFrm.disableFormButtons();

                    Global.actnPrdFrm.Show();
                    Global.actnPrdFrm.BringToFront();
                    System.Windows.Forms.Application.DoEvents();
                    Global.actnPrdFrm.populateDet(Global.mnFrm.cmCde.Org_id);
                }
                else
                {
                    //this.otherFormsPanel.Controls.Add(Global.actnPrdFrm);
                    //Global.actnPrdFrm.disableFormButtons();
                    Global.actnPrdFrm.BringToFront();
                }
                //Global.actnPrdFrm.positionDetTextBox.Focus();
                //this.showATab(ref this.otherFormsPanel);
            }
            else if (inpt_name == menuItems[8])
            {
                //this.otherFormsPanel.Controls.Clear();
                //Global.fxdAstsFrm = null;
                this.showATab(ref this.tabPage8);
                this.changeOrg();

                Global.fxdAstsFrm = (fxdAsstsForm)Global.isFormAlreadyOpen(typeof(fxdAsstsForm));
                if (Global.fxdAstsFrm == null)
                {
                    Global.fxdAstsFrm = new fxdAsstsForm();
                    Global.fxdAstsFrm.TopLevel = false;
                    Global.fxdAstsFrm.FormBorderStyle = FormBorderStyle.None;
                    Global.fxdAstsFrm.Dock = DockStyle.Fill;
                    this.tabPage8.Controls.Add(Global.fxdAstsFrm);
                    Global.fxdAstsFrm.BackColor = clrs[0];
                    Global.fxdAstsFrm.tabPage1.BackColor = clrs[0];
                    Global.fxdAstsFrm.tabPage1.BackColor = clrs[0];
                    Global.fxdAstsFrm.loadPrvldgs();
                    Global.fxdAstsFrm.disableFormButtons();

                    Global.fxdAstsFrm.Show();
                    Global.fxdAstsFrm.BringToFront();
                    System.Windows.Forms.Application.DoEvents();
                    Global.fxdAstsFrm.loadPanel();
                }
                else
                {
                    //this.otherFormsPanel.Controls.Add(Global.fxdAstsFrm);
                    //Global.pyblsFrm.disableFormButtons();
                    Global.fxdAstsFrm.BringToFront();
                }
                //Global.pyblsFrm.populateDet(Global.mnFrm.cmCde.Org_id);
                //Global.pyblsFrm.positionDetTextBox.Focus();
                //this.showATab(ref this.otherFormsPanel);

            }
            else if (inpt_name == menuItems[9])
            {
                //this.otherFormsPanel.Controls.Clear();
                //Global.pyblsFrm = null;
                this.showATab(ref this.tabPage9);
                this.changeOrg();
                Global.pyblsFrm = (pyblsDocsForm)Global.isFormAlreadyOpen(typeof(pyblsDocsForm));
                if (Global.pyblsFrm == null)
                {
                    Global.pyblsFrm = new pyblsDocsForm();
                    Global.pyblsFrm.TopLevel = false;
                    Global.pyblsFrm.FormBorderStyle = FormBorderStyle.None;
                    Global.pyblsFrm.Dock = DockStyle.Fill;
                    this.tabPage9.Controls.Add(Global.pyblsFrm);
                    Global.pyblsFrm.BackColor = clrs[0];
                    Global.pyblsFrm.glsLabel3.TopFill = clrs[0];
                    Global.pyblsFrm.glsLabel3.BottomFill = clrs[1];
                    Global.pyblsFrm.loadPrvldgs();
                    Global.pyblsFrm.disableFormButtons();

                    Global.pyblsFrm.Show();
                    Global.pyblsFrm.BringToFront();
                    System.Windows.Forms.Application.DoEvents();
                    Global.pyblsFrm.loadPanel();
                }
                else
                {
                    //this.otherFormsPanel.Controls.Add(Global.pyblsFrm);
                    //Global.pyblsFrm.disableFormButtons();
                    Global.pyblsFrm.BringToFront();
                }
                //Global.pyblsFrm.positionDetTextBox.Focus();
                //this.showATab(ref this.otherFormsPanel);
            }
            else if (inpt_name == menuItems[10])
            {
                //this.otherFormsPanel.Controls.Clear();
                //Global.rcvblsFrm = null;
                this.showATab(ref this.tabPage10);
                this.changeOrg();

                Global.rcvblsFrm = (rcvblsDocsForm)Global.isFormAlreadyOpen(typeof(rcvblsDocsForm));
                if (Global.rcvblsFrm == null)
                {
                    Global.rcvblsFrm = new rcvblsDocsForm();
                    Global.rcvblsFrm.TopLevel = false;
                    Global.rcvblsFrm.FormBorderStyle = FormBorderStyle.None;
                    Global.rcvblsFrm.Dock = DockStyle.Fill;
                    this.tabPage10.Controls.Add(Global.rcvblsFrm);
                    Global.rcvblsFrm.BackColor = clrs[0];
                    Global.rcvblsFrm.glsLabel3.TopFill = clrs[0];
                    Global.rcvblsFrm.glsLabel3.BottomFill = clrs[1];
                    Global.rcvblsFrm.loadPrvldgs();
                    Global.rcvblsFrm.disableFormButtons();

                    Global.rcvblsFrm.Show();
                    Global.rcvblsFrm.BringToFront();
                    System.Windows.Forms.Application.DoEvents();
                    Global.rcvblsFrm.loadPanel();
                }
                else
                {
                    //this.otherFormsPanel.Controls.Add(Global.rcvblsFrm);
                    //Global.rcvblsFrm.disableFormButtons();
                    Global.rcvblsFrm.BringToFront();
                }
                //Global.pyblsFrm.populateDet(Global.mnFrm.cmCde.Org_id);
                //Global.pyblsFrm.positionDetTextBox.Focus();
                //this.showATab(ref this.otherFormsPanel);
            }
            else if (inpt_name == menuItems[11])
            {
                //this.otherFormsPanel.Controls.Clear();
                //Global.pymntFrm = null;
                this.showATab(ref this.tabPage11);
                this.changeOrg();

                Global.pymntFrm = (pymntsForm)Global.isFormAlreadyOpen(typeof(pymntsForm));
                if (Global.pymntFrm == null)
                {
                    Global.pymntFrm = new pymntsForm();
                    Global.pymntFrm.TopLevel = false;
                    Global.pymntFrm.FormBorderStyle = FormBorderStyle.None;
                    Global.pymntFrm.Dock = DockStyle.Fill;
                    this.tabPage11.Controls.Add(Global.pymntFrm);
                    Global.pymntFrm.BackColor = clrs[0];
                    Global.pymntFrm.glsLabel2.TopFill = clrs[0];
                    Global.pymntFrm.glsLabel2.BottomFill = clrs[1];
                    Global.pymntFrm.loadPrvldgs();
                    Global.pymntFrm.disableFormButtons();

                    Global.pymntFrm.Show();
                    Global.pymntFrm.BringToFront();
                    System.Windows.Forms.Application.DoEvents();
                    Global.pymntFrm.loadPanel();
                }
                else
                {
                    //this.otherFormsPanel.Controls.Add(Global.pymntFrm);
                    //Global.pymntFrm.disableFormButtons();
                    Global.pymntFrm.BringToFront();
                }
                //Global.pyblsFrm.populateDet(Global.mnFrm.cmCde.Org_id);
                //Global.pyblsFrm.positionDetTextBox.Focus();
                //this.showATab(ref this.otherFormsPanel);
            }
            else if (inpt_name == menuItems[12])
            {
                //this.otherFormsPanel.Controls.Clear();
                //Global.custFrm = null;
                this.showATab(ref this.tabPage12);
                this.changeOrg();

                Global.custFrm = (custSpplrForm)Global.isFormAlreadyOpen(typeof(custSpplrForm));
                if (Global.custFrm == null)
                {
                    Global.custFrm = new custSpplrForm();
                    Global.custFrm.TopLevel = false;
                    Global.custFrm.FormBorderStyle = FormBorderStyle.None;
                    Global.custFrm.Dock = DockStyle.Fill;
                    this.tabPage12.Controls.Add(Global.custFrm);
                    Global.custFrm.BackColor = clrs[0];
                    Global.custFrm.glsLabel3.TopFill = clrs[0];
                    Global.custFrm.glsLabel3.BottomFill = clrs[1];
                    Global.custFrm.tabPage1.BackColor = clrs[0];
                    Global.custFrm.tabPage2.BackColor = clrs[0];
                    //Global.custFrm.loadPrvldgs();
                    Global.custFrm.disableFormButtons();

                    Global.custFrm.Show();
                    Global.custFrm.BringToFront();
                    System.Windows.Forms.Application.DoEvents();
                    Global.custFrm.loadPanel();
                }
                else
                {
                    //this.otherFormsPanel.Controls.Add(Global.custFrm);
                    //Global.custFrm.disableFormButtons();
                    Global.custFrm.BringToFront();
                }
                //Global.pyblsFrm.positionDetTextBox.Focus();
                //this.showATab(ref this.otherFormsPanel);
            }
            else if (inpt_name == menuItems[13])
            {
                //this.otherFormsPanel.Controls.Clear();
                //Global.taxFrm = null;
                this.showATab(ref this.tabPage13);
                this.changeOrg();

                Global.taxFrm = (taxNDscntsForm)Global.isFormAlreadyOpen(typeof(taxNDscntsForm));
                if (Global.taxFrm == null)
                {
                    Global.taxFrm = new taxNDscntsForm();
                    Global.taxFrm.TopLevel = false;
                    Global.taxFrm.FormBorderStyle = FormBorderStyle.None;
                    Global.taxFrm.Dock = DockStyle.Fill;
                    this.tabPage13.Controls.Add(Global.taxFrm);
                    Global.taxFrm.BackColor = clrs[0];
                    Global.taxFrm.glsLabel3.TopFill = clrs[0];
                    Global.taxFrm.glsLabel3.BottomFill = clrs[1];
                    //Global.actnPrdFrm.loadPrvldgs();
                    Global.taxFrm.disableFormButtons();

                    Global.taxFrm.Show();
                    Global.taxFrm.BringToFront();
                    System.Windows.Forms.Application.DoEvents();
                    Global.taxFrm.loadPanel();
                }
                else
                {
                    //this.otherFormsPanel.Controls.Add(Global.taxFrm);
                    //Global.taxFrm.disableFormButtons();
                    Global.taxFrm.BringToFront();
                }
                //Global.pyblsFrm.positionDetTextBox.Focus();
                // this.showATab(ref this.otherFormsPanel);
            }
            else if (inpt_name == menuItems[14])
            {
                //this.otherFormsPanel.Controls.Clear();
                //Global.accntFrm = null;
                this.showATab(ref this.tabPage14);
                this.changeOrg();

                Global.accntFrm = (accntsSetupForm)Global.isFormAlreadyOpen(typeof(accntsSetupForm));
                if (Global.accntFrm == null)
                {
                    Global.accntFrm = new accntsSetupForm();
                    Global.accntFrm.TopLevel = false;
                    Global.accntFrm.FormBorderStyle = FormBorderStyle.None;
                    Global.accntFrm.Dock = DockStyle.Fill;
                    this.tabPage14.Controls.Add(Global.accntFrm);
                    Global.accntFrm.BackColor = clrs[0];
                    Global.accntFrm.glsLabel3.TopFill = clrs[0];
                    Global.accntFrm.glsLabel3.BottomFill = clrs[1];
                    //Global.actnPrdFrm.loadPrvldgs();
                    //Global.accntFrm.disableFormButtons();

                    Global.accntFrm.Show();
                    Global.accntFrm.BringToFront();
                    System.Windows.Forms.Application.DoEvents();
                    Global.accntFrm.populateDet();
                }
                else
                {
                    //this.otherFormsPanel.Controls.Add(Global.accntFrm);
                    //Global.pyblsFrm.disableFormButtons();
                    Global.accntFrm.BringToFront();
                }
                //Global.pyblsFrm.positionDetTextBox.Focus();
                //this.showATab(ref this.otherFormsPanel);
            }
            else if (inpt_name == menuItems[15])
            {
                //this.otherFormsPanel.Controls.Clear();
                //Global.rcncileFrm = null;
                this.showATab(ref this.tabPage15);
                this.changeOrg();

                Global.rcncileFrm = (reconcileForm)Global.isFormAlreadyOpen(typeof(reconcileForm));
                if (Global.rcncileFrm == null)
                {
                    Global.rcncileFrm = new reconcileForm();
                    Global.rcncileFrm.TopLevel = false;
                    Global.rcncileFrm.FormBorderStyle = FormBorderStyle.None;
                    Global.rcncileFrm.Dock = DockStyle.Fill;
                    this.tabPage15.Controls.Add(Global.rcncileFrm);
                    Global.rcncileFrm.BackColor = clrs[0];
                    //Global.rcncileFrm.tabPage1.BackColor = clrs[0];
                    //Global.rcncileFrm.tabPage2.BackColor = clrs[0];
                    Global.rcncileFrm.tabPage3.BackColor = clrs[0];
                    Global.rcncileFrm.tabPage4.BackColor = clrs[0];
                    Global.rcncileFrm.glsLabel3.TopFill = clrs[0];
                    Global.rcncileFrm.glsLabel3.BottomFill = clrs[1];
                    //Global.actnPrdFrm.loadPrvldgs();
                    //Global.pyblsFrm.disableFormButtons();

                    if (Global.rcncileFrm.strtDteAccntStmntTextBox.Text == "")
                        Global.rcncileFrm.strtDteAccntStmntTextBox.Text = DateTime.ParseExact(
                          Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
                          System.Globalization.CultureInfo.InvariantCulture).ToString("01-MMM-yyyy 00:00:00");

                    if (Global.rcncileFrm.endDteAccntStmntTextBox.Text == "")
                        Global.rcncileFrm.endDteAccntStmntTextBox.Text = DateTime.ParseExact(
                          "01" + Global.mnFrm.cmCde.getFrmtdDB_Date_time().Substring(2), "dd-MMM-yyyy HH:mm:ss",
                          System.Globalization.CultureInfo.InvariantCulture).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy 23:59:59");

                    Global.rcncileFrm.Show();
                    Global.rcncileFrm.BringToFront();
                    System.Windows.Forms.Application.DoEvents();
                }
                else
                {
                    //this.otherFormsPanel.Controls.Add(Global.rcncileFrm);
                    //Global.pyblsFrm.disableFormButtons();
                    Global.rcncileFrm.BringToFront();
                }
                //Global.pyblsFrm.populateDet(Global.mnFrm.cmCde.Org_id);
                //Global.pyblsFrm.positionDetTextBox.Focus();
                //this.showATab(ref this.otherFormsPanel);
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

        private void hideAllPanels()
        {
            this.accntsChrtPanel.Visible = false;
            this.accntsChrtPanel.Enabled = false;
            System.Windows.Forms.Application.DoEvents();
            //this.accntsChrtPanel.Dock = DockStyle.None;
            this.trnsctnsPanel.Visible = false;
            this.trnsctnsPanel.Enabled = false;
            System.Windows.Forms.Application.DoEvents();
            this.trnsSearchPanel.Visible = false;
            this.trnsSearchPanel.Enabled = false;
            System.Windows.Forms.Application.DoEvents();
            this.trnsTmpltsPanel.Visible = false;
            this.trnsTmpltsPanel.Enabled = false;
            System.Windows.Forms.Application.DoEvents();
            //this.trnsctnsPanel.Dock = DockStyle.None;      
            System.Windows.Forms.Application.DoEvents();
            //this.trialBalancePanel.Dock = DockStyle.None;      
            System.Windows.Forms.Application.DoEvents();
            //this.prftnlossPanel.Dock = DockStyle.None;     
            System.Windows.Forms.Application.DoEvents();
            this.budgetPanel.Visible = false;
            this.budgetPanel.Enabled = false;
            System.Windows.Forms.Application.DoEvents();
            this.finStmntsPanel.Visible = false;
            this.finStmntsPanel.Enabled = false;
            System.Windows.Forms.Application.DoEvents();
            this.otherFormsPanel.Visible = false;
            this.otherFormsPanel.Enabled = false;
            System.Windows.Forms.Application.DoEvents();
        }

        private void showAllPanels()
        {
            this.trialBalancePanel.Visible = true;
            this.trialBalancePanel.Enabled = true;
            this.prftnlossPanel.Visible = true;
            this.prftnlossPanel.Enabled = true;
            this.balSheetPanel.Visible = true;
            this.balSheetPanel.Enabled = true;

            this.accntsChrtPanel.Visible = true;
            this.accntsChrtPanel.Enabled = true;
            System.Windows.Forms.Application.DoEvents();
            //this.accntsChrtPanel.Dock = DockStyle.None;
            this.trnsctnsPanel.Visible = true;
            this.trnsctnsPanel.Enabled = true;
            System.Windows.Forms.Application.DoEvents();
            this.trnsSearchPanel.Visible = true;
            this.trnsSearchPanel.Enabled = true;
            System.Windows.Forms.Application.DoEvents();
            this.trnsTmpltsPanel.Visible = true;
            this.trnsTmpltsPanel.Enabled = true;
            System.Windows.Forms.Application.DoEvents();
            //this.trnsctnsPanel.Dock = DockStyle.None;      
            System.Windows.Forms.Application.DoEvents();
            //this.trialBalancePanel.Dock = DockStyle.None;      
            System.Windows.Forms.Application.DoEvents();
            //this.prftnlossPanel.Dock = DockStyle.None;     
            System.Windows.Forms.Application.DoEvents();
            this.budgetPanel.Visible = true;
            this.budgetPanel.Enabled = true;
            System.Windows.Forms.Application.DoEvents();
            this.finStmntsPanel.Visible = true;
            this.finStmntsPanel.Enabled = true;
            System.Windows.Forms.Application.DoEvents();
            this.otherFormsPanel.Visible = true;
            this.otherFormsPanel.Enabled = true;
            System.Windows.Forms.Application.DoEvents();
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

        private void leftTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.backgroundWorker1.IsBusy == true)
            {
                return;
            }
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
        //   Global.mnFrm.cmCde.getLovID("Organisations"), ref selVals, true, true);
        //  if (dgRes == DialogResult.OK)
        //  {
        //    this.curOrgPictureBox.Image.Dispose();
        //    this.curOrgPictureBox.Image = Accounting.Properties.Resources.blank;
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
        //       0, ref this.curOrgPictureBox);
        //      if (shdLd == false)
        //      {
        //        this.last_chrt_num = 0;
        //        this.chrt_cur_indx = 0;
        //        //this.last_site_num = 0;
        //        //this.site_cur_indx = 0;

        //        this.loadCorrectPanel(this.leftTreeView.SelectedNode.Text);
        //      }
        //    }
        //  }
        //}

        private void disableFormButtons()
        {
            bool vwSQLAct = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[10]);
            bool rcHstryAct = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[9]);
            bool vwTrns = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[2]);

            //Accounts Chart
            this.saveChrtButton.Enabled = false;
            this.vwAccntTrnsctnsButton.Enabled = vwTrns;
            this.addAccounts = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[11]);
            this.addChrtButton.Enabled = this.addAccounts;
            this.addAcntMenuItem.Enabled = this.addAccounts;
            this.importChartButton.Enabled = this.addAccounts;

            this.editAccounts = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[12]);
            this.editAcntMenuItem.Enabled = this.editAccounts;
            this.editChrtButton.Enabled = this.editAccounts;

            this.delAccounts = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[13]);
            this.deleteChrtButton.Enabled = this.delAccounts;
            this.delAcntMenuItem.Enabled = this.delAccounts;

            this.vwSQLAcntMenuItem.Enabled = vwSQLAct;
            this.vwSQLChrtButton.Enabled = vwSQLAct;

            this.recHstryChrtButton.Enabled = rcHstryAct;
            this.rcHstryAcntMenuItem.Enabled = rcHstryAct;
            //Accounts Transactions
            this.saveTrnsBatchButton.Enabled = false;
            this.addBatches = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[14]);
            this.addTrnsBatchButton.Enabled = this.addBatches;
            this.addBatchMenuItem.Enabled = this.addBatches;

            this.editBatches = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[15]);
            this.editBatchMenuItem.Enabled = this.editBatches;
            this.editTrnsBatchButton.Enabled = this.editBatches;

            this.delBatches = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[16]);
            this.voidBatchButton.Enabled = this.delBatches;
            this.delBatchMenuItem.Enabled = this.delBatches;

            this.vwSQLBatchMenuItem.Enabled = vwSQLAct;
            this.vwSQLBatchButton.Enabled = vwSQLAct;
            this.vwSQLTrnsMenuItem.Enabled = vwSQLAct;

            this.recHstryBatchButton.Enabled = rcHstryAct;
            this.recHstryBatchMenuItem.Enabled = rcHstryAct;
            this.recHstryTrnsMenuItem.Enabled = rcHstryAct;

            this.addTrscns = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]);
            this.addTrnsButton.Enabled = this.addTrscns;
            this.addTrnsMenuItem.Enabled = this.addTrscns;
            this.imprtTrnsTmpltButton.Enabled = this.addTrscns;
            this.addDirTrnsButton.Enabled = this.addTrscns;

            this.editTrscns = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[18]);
            this.editTrnsMenuItem.Enabled = this.editTrscns;
            this.editTrnsButton.Enabled = this.editTrscns;

            this.delTrscns = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[19]);
            this.deleteTrnsMenuItem.Enabled = this.delTrscns;
            this.delTrnsButton.Enabled = this.delTrscns;

            this.addTrscnsFrmTmp = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[20]);
            this.addTrnsTmpltMenuItem.Enabled = this.addTrscnsFrmTmp;
            this.addTrnsTmpltButton.Enabled = this.addTrscnsFrmTmp;
            this.addTrnsFrmTmpButton.Enabled = this.addTrscnsFrmTmp;

            this.postTrscns = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[21]);
            this.postTrnsButton.Enabled = this.postTrscns;
            //Transactions Search
            this.vwSQLSrchButton.Enabled = vwSQLAct;
            this.vwSQLTsrchMenuItem.Enabled = vwSQLAct;

            this.rcHstryTsrchMenuItem.Enabled = rcHstryAct;
            this.recHstrySrchButton.Enabled = rcHstryAct;
            //Trial Balance
            this.vwSQLTbalsMenuItem.Enabled = vwSQLAct;
            this.vwTrnsTbalsMenuItem.Enabled = vwTrns;
            //Profit & Loss
            this.vwSQLPrfNLsMenuItem.Enabled = vwSQLAct;
            this.vwTrnsPrfNLsMenuItem.Enabled = vwTrns;
            //Balance Sheet
            this.vwSQLBlsMenuItem.Enabled = vwSQLAct;
            this.vwTrnsBlsMenuItem.Enabled = vwTrns;
            //Budget
            this.saveBdgButton.Enabled = false;
            this.addBudgets = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[22]);
            this.addBdgButton.Enabled = this.addBudgets;
            this.addBdgtMenuItem.Enabled = this.addBudgets;
            this.addBdgtDtMenuItem.Enabled = this.addBudgets;
            this.imprtBdgtTmpltButton.Enabled = this.addBudgets;
            this.addBdgtDtButton.Enabled = this.addBudgets;

            this.editBudgets = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[23]);
            this.editBdgButton.Enabled = this.editBudgets;
            this.editBdgtMenuItem.Enabled = this.editBudgets;
            this.editBdgtDtMenuItem.Enabled = this.editBudgets;
            this.editBdgtDtButton.Enabled = this.editBudgets;

            this.delBudgets = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[24]);
            this.delBdgButton.Enabled = this.delBudgets;
            this.delBdgtMenuItem.Enabled = this.delBudgets;
            this.delBdgtDtMenuItem.Enabled = this.delBudgets;
            this.delBdgtDtButton.Enabled = this.delBudgets;

            this.vwSQLBdgButton.Enabled = vwSQLAct;
            this.vwSQLBdgtMenuItem.Enabled = vwSQLAct;
            this.vwSQLBdgtDtMenuItem.Enabled = vwSQLAct;

            this.recHstryBdgButton.Enabled = rcHstryAct;
            this.rcHstryBdgtDtMenuItem.Enabled = rcHstryAct;
            this.rcHstryBdgtMenuItem.Enabled = rcHstryAct;
            //Transactions Template
            this.saveTmpltButton.Enabled = false;
            this.addTmplts = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[22]);
            this.addTmpltButton.Enabled = this.addTmplts;
            this.addTmpltMenuItem.Enabled = this.addTmplts;
            this.addTmpltTrnsButton.Enabled = this.addTmplts;
            this.addTmpltTrnsMenuItem.Enabled = this.addTmplts;
            this.addTmpltUsrsButton.Enabled = this.addTmplts;
            this.addUsrMenuItem.Enabled = this.addTmplts;
            this.imprtTrnsTmpltTmpButton.Enabled = this.addTmplts;
            this.addTmpDirTrnsButton.Enabled = this.addTmplts;

            this.editTmplts = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[23]);
            this.editTmpltButton.Enabled = this.editTmplts;
            this.editTmpltMenuItem.Enabled = this.editTmplts;
            this.editTmpltTrnsMenuItem.Enabled = this.editTmplts;
            this.editTmpDirTrnsButton.Enabled = this.editTmplts;

            this.delTmplts = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[24]);
            this.deleteTmpltButton.Enabled = this.delTmplts;
            this.delTmpltMenuItem.Enabled = this.delTmplts;
            this.delTmpltTrnsMenuItem.Enabled = this.delTmplts;
            this.delTmpDirTrnsButton.Enabled = this.delTmplts;

            this.vwSQLTmpltButton.Enabled = vwSQLAct;
            this.vwSQLTmpltMenuItem.Enabled = vwSQLAct;
            this.vwSQLTmpTrnsMenuItem.Enabled = vwSQLAct;
            this.vwSQLTusrMenuItem.Enabled = vwSQLAct;

            this.recHstryTmpltButton.Enabled = rcHstryAct;
            this.recHstryTmpTrnsMenuItem.Enabled = rcHstryAct;
            this.rcHstryTmpltMenuItem.Enabled = rcHstryAct;
            this.rcHstryTusrMenuItem.Enabled = rcHstryAct;
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[28]) == true)
            {
                this.shwMyBatchesCheckBox.Checked = true;
                this.shwMyBatchesCheckBox.Enabled = false;
            }
            //Internal/Memo Accounts
            this.addimas = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[11]);
            this.addIMAButton.Enabled = this.addimas;
            this.importIMAButton.Enabled = this.addimas;
            //this.exportGBVButton.Enabled = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[38]);

            this.editimas = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[12]);
            this.editIMAButton.Enabled = this.editimas;
            this.addIMADtButton.Enabled = this.editimas;
            this.editIMADtButton.Enabled = this.editimas;
            this.delIMADtButton.Enabled = this.editimas;

            this.delimas = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[13]);
            this.delIMAButton.Enabled = this.delimas;
            //this.delPrsStMenuItem.Enabled = this.delgbvs;

            this.vwSQLIMAButton.Enabled = vwSQLAct;
            this.vwSQLIMADtButton.Enabled = vwSQLAct;
            // this.vwSQLPrsStMenuItem.Enabled = vwSQL;

            this.rcHstryIMAButton.Enabled = rcHstryAct;
            this.rcHstryIMADtButton.Enabled = rcHstryAct;
            //this.rcHstryPrsStMenuItem.Enabled = rcHstry;
        }
        #endregion

        #region "CHART OF ACCOUNTS..."
        private void loadAccntChrtPanel()
        {
            this.waitLabel.Visible = false;
            this.correctImblnsButton.Enabled = true;

            System.Windows.Forms.Application.DoEvents();

            this.obey_chrt_evnts = false;
            if (this.searchInChrtComboBox.SelectedIndex < 0)
            {
                this.searchInChrtComboBox.SelectedIndex = 0;
            }
            if (searchForChrtTextBox.Text.Contains("%") == false)
            {
                this.searchForChrtTextBox.Text = "%" + this.searchForChrtTextBox.Text.Replace(" ", "%") + "%";
            }
            if (this.searchForChrtTextBox.Text == "%%")
            {
                this.searchForChrtTextBox.Text = "%";
            }
            int dsply = 0;
            if (this.dsplySizeChrtComboBox.Text == ""
             || int.TryParse(this.dsplySizeChrtComboBox.Text, out dsply) == false)
            {
                this.dsplySizeChrtComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            this.groupBox4.Text = "CHART OF ACCOUNTS BALANCE DETAILS (" + funcCurCode + ")";
            this.funcCurrLabel.Text = "FUNCTIONAL CURRENCY BALANCE (" + funcCurCode + ")";
            this.accntCurrLabel.Text = "ACCOUNT CURRENCY BALANCE (" + funcCurCode + ")";
            this.is_last_chrt = false;
            this.chrt_cur_indx = 0;
            this.totl_chrt = Global.mnFrm.cmCde.Big_Val;
            this.getChrtPnlData();
            this.obey_chrt_evnts = true;
        }

        private void getChrtPnlData()
        {
            this.updtChrtTotals();
            this.populateChrt();
            this.updtChrtNavLabels();
        }

        private void updtChrtTotals()
        {
            Global.mnFrm.cmCde.navFuncts.FindNavigationIndices(int.Parse(this.dsplySizeChrtComboBox.Text), this.totl_chrt);
            if (this.chrt_cur_indx >= Global.mnFrm.cmCde.navFuncts.totalGroups)
            {
                this.chrt_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            if (this.chrt_cur_indx < 0)
            {
                this.chrt_cur_indx = 0;
            }
            Global.mnFrm.cmCde.navFuncts.currentNavigationIndex = this.chrt_cur_indx;
        }

        private void updtChrtNavLabels()
        {
            this.moveFirstChrtButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveFirstBtnStatus();
            this.movePreviousChrtButton.Enabled = Global.mnFrm.cmCde.navFuncts.movePrevBtnStatus();
            this.moveNextChrtButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveNextBtnStatus();
            this.moveLastChrtButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveLastBtnStatus();
            this.positionChrtTextBox.Text = Global.mnFrm.cmCde.navFuncts.displayedRecordsNumbers();
            if (this.is_last_chrt == true ||
             this.totl_chrt != Global.mnFrm.cmCde.Big_Val)
            {
                this.totalRecChrtLabel.Text = Global.mnFrm.cmCde.navFuncts.totalRecordsLabel();
            }
            else
            {
                this.totalRecChrtLabel.Text = "of Total";
            }
        }

        private void populateChrtDet(int accntID)
        {
            if (this.addChrt == true && this.benhr == 0)
            {
                if (Global.mnFrm.cmCde.showMsg("Are you sure you want to Navigate away \r\n from this Record without Saving?", 1) == DialogResult.No)
                {
                    this.benhr++;
                    return;
                }
            }
            else if (this.benhr > 0)
            {
                this.benhr = 0;
                return;
            }
            this.obey_chrt_evnts = false;
            if (this.editChrt == false)
            {
                this.clearChrtInfo();
                this.disableChrtEdit();
            }

            this.obey_chrt_evnts = false;
            DataSet dtst = Global.get_One_Chrt_Det(accntID);
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.accntIDTextBox.Text = dtst.Tables[0].Rows[i][0].ToString();
                this.accntNumTextBox.Text = dtst.Tables[0].Rows[i][1].ToString();
                this.accntNameTextBox.Text = dtst.Tables[0].Rows[i][2].ToString();
                this.accntDescTextBox.Text = dtst.Tables[0].Rows[i][3].ToString();
                if (editChrt == false)
                {
                    this.accClsfctnComboBox.Items.Clear();
                    this.accClsfctnComboBox.Items.Add(dtst.Tables[0].Rows[i][26].ToString());
                }
                this.accClsfctnComboBox.SelectedItem = dtst.Tables[0].Rows[i][26].ToString();
                this.isContraCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][4].ToString());
                this.parentAccntIDTextBox.Text = dtst.Tables[0].Rows[i][5].ToString();
                this.parentAccntTextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(dtst.Tables[0].Rows[i][5].ToString()));
                this.balsDateTextBox.Text = dtst.Tables[0].Rows[i][6].ToString();
                this.accntTypeComboBox.Items.Clear();
                if (dtst.Tables[0].Rows[i][12].ToString() == "A")
                {
                    this.accntTypeComboBox.Items.Add("A -ASSET");
                }
                else if (dtst.Tables[0].Rows[i][12].ToString() == "EQ")
                {
                    this.accntTypeComboBox.Items.Add("EQ-EQUITY");
                }
                else if (dtst.Tables[0].Rows[i][12].ToString() == "L")
                {
                    this.accntTypeComboBox.Items.Add("L -LIABILITY");
                }
                else if (dtst.Tables[0].Rows[i][12].ToString() == "R")
                {
                    this.accntTypeComboBox.Items.Add("R -REVENUE");
                }
                else if (dtst.Tables[0].Rows[i][12].ToString() == "EX")
                {
                    this.accntTypeComboBox.Items.Add("EX-EXPENSE");
                }
                if (this.accntTypeComboBox.Items.Count > 0)
                {
                    this.accntTypeComboBox.SelectedIndex = 0;
                }

                this.isPrntAccntsCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][13].ToString());
                if (this.isPrntAccntsCheckBox.Checked == true)
                {
                    DataSet dtst1 = Global.get_Bals_Prnt_Accnts(int.Parse(this.accntIDTextBox.Text));
                    if (dtst1.Tables[0].Rows.Count > 0)
                    {
                        float a = 0;
                        float b = 0;
                        float c = 0;
                        float.TryParse(dtst1.Tables[0].Rows[0][0].ToString(), out a);
                        float.TryParse(dtst1.Tables[0].Rows[0][1].ToString(), out b);
                        float.TryParse(dtst1.Tables[0].Rows[0][2].ToString(), out c);

                        this.dbtBalNumericUpDown.Value = (Decimal)Math.Round(a, 2);
                        this.crdtBalNumericUpDown.Value = (Decimal)Math.Round(b, 2);
                        this.netBalNumericUpDown.Value = (Decimal)Math.Round(c, 2);
                        this.balsDateTextBox.Text = Global.mnFrm.cmCde.getFrmtdDB_Date_time().Substring(0, 11);
                    }
                }
                else
                {
                    this.dbtBalNumericUpDown.Value = (Decimal)float.Parse(dtst.Tables[0].Rows[i][14].ToString());
                    this.crdtBalNumericUpDown.Value = (Decimal)float.Parse(dtst.Tables[0].Rows[i][15].ToString());
                    this.netBalNumericUpDown.Value = (Decimal)float.Parse(dtst.Tables[0].Rows[i][17].ToString());
                }
                if (this.crdtBalNumericUpDown.Value > this.dbtBalNumericUpDown.Value)
                {
                    this.netBalTypeLabel.Text = "CREDIT";
                }
                else if (this.crdtBalNumericUpDown.Value < this.dbtBalNumericUpDown.Value)
                {
                    this.netBalTypeLabel.Text = "DEBIT";
                }
                else
                {
                    this.netBalTypeLabel.Text = "";
                }

                this.isEnabledAccntsCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][16].ToString());
                this.isRetEarnsCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][18].ToString());
                this.isNetIncmCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][19].ToString());
                this.rptLnNoUpDown.Value = Decimal.Parse(dtst.Tables[0].Rows[i][21].ToString());

                this.hasSubldgrCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][22].ToString());
                this.isSuspensCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][25].ToString());
                this.cntrlAccntIDTextBox.Text = dtst.Tables[0].Rows[i][23].ToString();
                this.cntrlAccntTextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(dtst.Tables[0].Rows[i][23].ToString()));
                this.accntCurrIDTextBox.Text = dtst.Tables[0].Rows[i][24].ToString();
                this.accntCrncyNmTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(int.Parse(dtst.Tables[0].Rows[i][24].ToString()))
                  + " - " + Global.mnFrm.cmCde.getPssblValDesc(int.Parse(dtst.Tables[0].Rows[i][24].ToString()));

                this.accntSgmnt1TextBox.Text = dtst.Tables[0].Rows[i][27].ToString();
                this.accntSgmnt2TextBox.Text = dtst.Tables[0].Rows[i][28].ToString();
                this.accntSgmnt3TextBox.Text = dtst.Tables[0].Rows[i][29].ToString();
                this.accntSgmnt4TextBox.Text = dtst.Tables[0].Rows[i][30].ToString();
                this.accntSgmnt5TextBox.Text = dtst.Tables[0].Rows[i][31].ToString();
                this.accntSgmnt6TextBox.Text = dtst.Tables[0].Rows[i][32].ToString();
                this.accntSgmnt7TextBox.Text = dtst.Tables[0].Rows[i][33].ToString();
                this.accntSgmnt8TextBox.Text = dtst.Tables[0].Rows[i][34].ToString();
                this.accntSgmnt9TextBox.Text = dtst.Tables[0].Rows[i][35].ToString();
                this.accntSgmnt10TextBox.Text = dtst.Tables[0].Rows[i][36].ToString();
                this.mappedAccntIDTextBox.Text = dtst.Tables[0].Rows[i][37].ToString();
                this.mappedAccntTextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(dtst.Tables[0].Rows[i][37].ToString()))
                + "." + Global.mnFrm.cmCde.getAccntName(int.Parse(dtst.Tables[0].Rows[i][37].ToString()));

                this.accntCurrLabel.Text = "ACCOUNT CURRENCY BALANCE (" +
                  Global.mnFrm.cmCde.getPssblValNm(int.Parse(dtst.Tables[0].Rows[i][24].ToString())) + ")";

                if (this.accntCurrIDTextBox.Text == this.funCurID.ToString())
                {
                    this.bals2DteTextBox.Text = this.balsDateTextBox.Text;
                    this.dbtBal2NumericUpDown.Value = this.dbtBalNumericUpDown.Value;
                    this.crdtBal2NumericUpDown.Value = this.crdtBalNumericUpDown.Value;
                    this.netBal2NumericUpDown.Value = this.netBalNumericUpDown.Value;
                }
                else
                {
                    if (this.isPrntAccntsCheckBox.Checked == true)
                    {
                        DataSet dtst1 = Global.get_CurrBals_Prnt_Accnts(
                          int.Parse(this.accntIDTextBox.Text), int.Parse(this.accntCurrIDTextBox.Text));
                        if (dtst1.Tables[0].Rows.Count > 0)
                        {
                            float a = 0;
                            float b = 0;
                            float c = 0;
                            float.TryParse(dtst1.Tables[0].Rows[0][0].ToString(), out a);
                            float.TryParse(dtst1.Tables[0].Rows[0][1].ToString(), out b);
                            float.TryParse(dtst1.Tables[0].Rows[0][2].ToString(), out c);

                            this.bals2DteTextBox.Text = Global.mnFrm.cmCde.getFrmtdDB_Date_time().Substring(0, 11);
                            this.dbtBal2NumericUpDown.Value = (Decimal)Math.Round(a, 2);
                            this.crdtBal2NumericUpDown.Value = (Decimal)Math.Round(b, 2);
                            this.netBal2NumericUpDown.Value = (Decimal)Math.Round(c, 2);
                        }
                        else
                        {
                            this.bals2DteTextBox.Text = "";
                            this.dbtBal2NumericUpDown.Value = 0;
                            this.crdtBal2NumericUpDown.Value = 0;
                            this.netBal2NumericUpDown.Value = 0;
                        }
                    }
                    else if (this.hasSubldgrCheckBox.Checked == true)
                    {
                        DataSet dtst1 = Global.get_CurrBals_Cntrl_Accnts(
                           int.Parse(this.accntIDTextBox.Text), int.Parse(this.accntCurrIDTextBox.Text));
                        if (dtst1.Tables[0].Rows.Count > 0)
                        {
                            float a = 0;
                            float b = 0;
                            float c = 0;
                            float.TryParse(dtst1.Tables[0].Rows[0][0].ToString(), out a);
                            float.TryParse(dtst1.Tables[0].Rows[0][1].ToString(), out b);
                            float.TryParse(dtst1.Tables[0].Rows[0][2].ToString(), out c);

                            this.bals2DteTextBox.Text = Global.mnFrm.cmCde.getFrmtdDB_Date_time().Substring(0, 11);
                            this.dbtBal2NumericUpDown.Value = (Decimal)Math.Round(a, 2);
                            this.crdtBal2NumericUpDown.Value = (Decimal)Math.Round(b, 2);
                            this.netBal2NumericUpDown.Value = (Decimal)Math.Round(c, 2);
                        }
                        else
                        {
                            this.bals2DteTextBox.Text = "";
                            this.dbtBal2NumericUpDown.Value = 0;
                            this.crdtBal2NumericUpDown.Value = 0;
                            this.netBal2NumericUpDown.Value = 0;
                        }
                    }
                    else
                    {
                        DataSet dtst1 = Global.get_CurrBals_Accnts(int.Parse(this.accntIDTextBox.Text));
                        if (dtst1.Tables[0].Rows.Count > 0)
                        {
                            float a = 0;
                            float b = 0;
                            float c = 0;
                            float.TryParse(dtst1.Tables[0].Rows[0][0].ToString(), out a);
                            float.TryParse(dtst1.Tables[0].Rows[0][1].ToString(), out b);
                            float.TryParse(dtst1.Tables[0].Rows[0][2].ToString(), out c);

                            this.bals2DteTextBox.Text = dtst1.Tables[0].Rows[0][3].ToString();
                            this.dbtBal2NumericUpDown.Value = (Decimal)Math.Round(a, 2);
                            this.crdtBal2NumericUpDown.Value = (Decimal)Math.Round(b, 2);
                            this.netBal2NumericUpDown.Value = (Decimal)Math.Round(c, 2);
                        }
                        else
                        {
                            this.bals2DteTextBox.Text = "";
                            this.dbtBal2NumericUpDown.Value = 0;
                            this.crdtBal2NumericUpDown.Value = 0;
                            this.netBal2NumericUpDown.Value = 0;
                        }

                    }
                }
            }
            if (this.editChrt == true)
            {
                if (this.netBalNumericUpDown.Value != 0
                  || this.crdtBalNumericUpDown.Value != 0
                  || this.dbtBalNumericUpDown.Value != 0
                  || this.accntSgmnt1TextBox.Text != "-1")
                {
                    this.isPrntAccntsCheckBox.Enabled = false;
                    this.isContraCheckBox.Enabled = false;
                    this.isRetEarnsCheckBox.Enabled = false;
                    this.isNetIncmCheckBox.Enabled = false;
                    this.accntTypeComboBox.Enabled = false;
                    this.hasSubldgrCheckBox.Enabled = false;
                    this.cntrlAccntTextBox.Enabled = false;
                    this.cntrlAccntButton.Enabled = false;
                    this.accntCrncyNmTextBox.Enabled = false;
                    this.accntCurrButton.Enabled = false;
                }
                else
                {
                    this.isPrntAccntsCheckBox.Enabled = true;
                    this.isContraCheckBox.Enabled = true;
                    this.isRetEarnsCheckBox.Enabled = true;
                    this.isNetIncmCheckBox.Enabled = true;
                    this.accntTypeComboBox.Enabled = true;
                    this.hasSubldgrCheckBox.Enabled = true;
                    this.cntrlAccntTextBox.Enabled = true;
                    this.cntrlAccntButton.Enabled = true;
                    this.accntCrncyNmTextBox.Enabled = true;
                    this.accntCurrButton.Enabled = true;
                }
                if (this.accntSgmnt1TextBox.Text != "-1")
                {
                    this.accntNumTextBox.ReadOnly = true;
                    this.accntNumTextBox.BackColor = Color.WhiteSmoke;
                    this.accntNameTextBox.ReadOnly = true;
                    this.accntNameTextBox.BackColor = Color.WhiteSmoke;
                    this.accntDescTextBox.ReadOnly = true;
                    this.accntDescTextBox.BackColor = Color.WhiteSmoke;
                }
                else
                {
                    this.accntNumTextBox.ReadOnly = false;
                    this.accntNumTextBox.BackColor = Color.FromArgb(255, 255, 118);
                    this.accntNameTextBox.ReadOnly = true;
                    this.accntNameTextBox.BackColor = Color.FromArgb(255, 255, 118);
                    this.accntDescTextBox.ReadOnly = false;
                    this.accntDescTextBox.BackColor = Color.FromArgb(255, 255, 118);
                }
            }
            this.obey_chrt_evnts = true;
        }

        private void populateChrt()
        {
            this.obey_chrt_evnts = false;
            if (this.editChrt == false)
            {
                this.clearChrtInfo();
                this.disableChrtEdit();
            }
            this.coaNetBalNumericUpDown.Value = 0;
            this.coaAEBalNumericUpDown.Value = 0;
            this.coaCRLBalNumericUpDown.Value = 0;
            this.coaNetBalNumericUpDown.BackColor = Color.Green;
            this.coaAEBalNumericUpDown.BackColor = Color.Green;
            this.coaCRLBalNumericUpDown.BackColor = Color.Green;
            this.accntsChrtListView.Items.Clear();
            this.coaCRLBalNumericUpDown.Value = (decimal)Global.get_COA_CRLSum(Global.mnFrm.cmCde.Org_id);
            this.coaAEBalNumericUpDown.Value = (decimal)Global.get_COA_AESum(Global.mnFrm.cmCde.Org_id);
            this.coaNetBalNumericUpDown.Value = Math.Abs(this.coaCRLBalNumericUpDown.Value - this.coaAEBalNumericUpDown.Value);
            if (this.coaNetBalNumericUpDown.Value != (decimal)0)
            {
                this.coaNetBalNumericUpDown.BackColor = Color.Red;
                this.coaAEBalNumericUpDown.BackColor = Color.Red;
                this.coaCRLBalNumericUpDown.BackColor = Color.Red;
            }
            DataSet dtst = Global.get_Basic_ChrtDet(this.searchForChrtTextBox.Text,
             this.searchInChrtComboBox.Text, this.chrt_cur_indx, int.Parse(this.dsplySizeChrtComboBox.Text)
             , Global.mnFrm.cmCde.Org_id);
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.last_chrt_num = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
                ListViewItem nwItem = new ListViewItem(new string[] {
    (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),
    dtst.Tables[0].Rows[i][2].ToString(),
    dtst.Tables[0].Rows[i][0].ToString(),
    dtst.Tables[0].Rows[i][3].ToString()});
                this.accntsChrtListView.Items.Add(nwItem);
            }
            this.correctChrtNavLbls(dtst);
            if (this.accntsChrtListView.Items.Count > 0)
            {
                this.obey_chrt_evnts = true;
                this.accntsChrtListView.Items[0].Selected = true;
            }
            this.obey_chrt_evnts = true;
        }

        private void correctChrtNavLbls(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.chrt_cur_indx == 0 && totlRecs == 0)
            {
                this.is_last_chrt = true;
                this.totl_chrt = 0;
                this.last_chrt_num = 0;
                this.chrt_cur_indx = 0;
                this.updtChrtTotals();
                this.updtChrtNavLabels();
            }
            else if (this.totl_chrt == Global.mnFrm.cmCde.Big_Val
          && totlRecs < int.Parse(this.dsplySizeChrtComboBox.Text))
            {
                this.totl_chrt = this.last_chrt_num;
                if (totlRecs == 0)
                {
                    this.chrt_cur_indx -= 1;
                    this.updtChrtTotals();
                    this.populateChrt();
                }
                else
                {
                    this.updtChrtTotals();
                }
            }
        }

        private void clearChrtInfo()
        {
            this.obey_chrt_evnts = false;
            this.beenClicked = false;
            this.saveChrtButton.Enabled = false;
            this.addChrtButton.Enabled = this.addAccounts;
            this.editChrtButton.Enabled = this.editAccounts;
            this.deleteChrtButton.Enabled = this.delAccounts;

            this.accntIDTextBox.Text = "-1";
            this.accntNumTextBox.Text = "";
            this.accntNameTextBox.Text = "";
            this.accntDescTextBox.Text = "";
            this.accClsfctnComboBox.SelectedIndex = -1;
            this.isEnabledAccntsCheckBox.Checked = true;
            this.isPrntAccntsCheckBox.Checked = false;
            this.isContraCheckBox.Checked = false;
            this.isRetEarnsCheckBox.Checked = false;
            this.isNetIncmCheckBox.Checked = false;
            this.hasSubldgrCheckBox.Checked = false;
            this.isSuspensCheckBox.Checked = false;

            this.isPrntAccntsCheckBox.Enabled = true;
            this.isContraCheckBox.Enabled = true;
            this.isRetEarnsCheckBox.Enabled = true;
            this.isNetIncmCheckBox.Enabled = true;
            this.accntTypeComboBox.Enabled = true;
            this.hasSubldgrCheckBox.Enabled = true;
            this.cntrlAccntButton.Enabled = true;
            this.cntrlAccntTextBox.Enabled = true;
            this.accntCurrButton.Enabled = true;
            this.accntCrncyNmTextBox.Enabled = true;

            this.parentAccntIDTextBox.Text = "-1";
            this.parentAccntTextBox.Text = "";
            this.cntrlAccntIDTextBox.Text = "-1";
            this.cntrlAccntTextBox.Text = "";
            this.accntCurrIDTextBox.Text = "-1";
            this.accntCrncyNmTextBox.Text = "";

            this.accntSgmnt1TextBox.Text = "-1";
            this.accntSgmnt2TextBox.Text = "-1";
            this.accntSgmnt3TextBox.Text = "-1";
            this.accntSgmnt4TextBox.Text = "-1";
            this.accntSgmnt5TextBox.Text = "-1";
            this.accntSgmnt6TextBox.Text = "-1";
            this.accntSgmnt7TextBox.Text = "-1";
            this.accntSgmnt8TextBox.Text = "-1";
            this.accntSgmnt9TextBox.Text = "-1";
            this.accntSgmnt10TextBox.Text = "-1";

            this.accntTypeComboBox.Items.Clear();
            this.rptLnNoUpDown.Value = 100;
            this.balsDateTextBox.Text = "";
            this.dbtBalNumericUpDown.Value = 0;
            this.crdtBalNumericUpDown.Value = 0;
            this.netBalNumericUpDown.Value = 0;
            this.dbtBalNumericUpDown.BackColor = Color.Green;
            this.crdtBalNumericUpDown.BackColor = Color.Green;
            this.netBalNumericUpDown.BackColor = Color.Green;

            this.bals2DteTextBox.Text = "";
            this.dbtBal2NumericUpDown.Value = 0;
            this.crdtBal2NumericUpDown.Value = 0;
            this.netBal2NumericUpDown.Value = 0;
            this.dbtBal2NumericUpDown.BackColor = Color.Green;
            this.crdtBal2NumericUpDown.BackColor = Color.Green;
            this.netBal2NumericUpDown.BackColor = Color.Green;

            this.netBalTypeLabel.Text = "";
            //this.disableFormButtons(Global.currentPanel);
            this.obey_chrt_evnts = true;
        }

        private void prpareForChrtEdit()
        {
            this.saveChrtButton.Enabled = true;

            if (this.accntSgmnt1TextBox.Text != "-1")
            {
                this.accntNumTextBox.ReadOnly = true;
                this.accntNumTextBox.BackColor = Color.WhiteSmoke;
                this.accntNameTextBox.ReadOnly = true;
                this.accntNameTextBox.BackColor = Color.WhiteSmoke;
                this.accntDescTextBox.ReadOnly = true;
                this.accntDescTextBox.BackColor = Color.WhiteSmoke;
            }
            else
            {
                this.accntNumTextBox.ReadOnly = false;
                this.accntNumTextBox.BackColor = Color.FromArgb(255, 255, 118);
                this.accntNameTextBox.ReadOnly = true;
                this.accntNameTextBox.BackColor = Color.FromArgb(255, 255, 118);
                this.accntDescTextBox.ReadOnly = false;
                this.accntDescTextBox.BackColor = Color.FromArgb(255, 255, 118);
            }
            this.accClsfctnComboBox.BackColor = Color.White;

            this.accntCrncyNmTextBox.ReadOnly = false;
            this.accntCrncyNmTextBox.BackColor = Color.FromArgb(255, 255, 118);



            this.rptLnNoUpDown.Increment = 1;
            this.rptLnNoUpDown.ReadOnly = false;
            this.rptLnNoUpDown.BackColor = Color.White;
            string orgItm = this.accntTypeComboBox.Text;
            this.accntTypeComboBox.Items.Clear();
            this.accntTypeComboBox.Items.Add("A -ASSET");
            this.accntTypeComboBox.Items.Add("EQ-EQUITY");
            this.accntTypeComboBox.Items.Add("L -LIABILITY");
            this.accntTypeComboBox.Items.Add("R -REVENUE");
            this.accntTypeComboBox.Items.Add("EX-EXPENSE");
            if (this.editChrt == true)
            {
                this.accntTypeComboBox.SelectedItem = orgItm;
            }

            orgItm = this.accClsfctnComboBox.Text;
            this.accClsfctnComboBox.Items.Clear();
            for (int a = 0; a < Global.cashFlowClsfctns.Length; a++)
            {
                this.accClsfctnComboBox.Items.Add(Global.cashFlowClsfctns[a]);
            }
            if (this.editChrt == true)
            {
                this.accClsfctnComboBox.SelectedItem = orgItm;
            }
        }

        private void disableChrtEdit()
        {
            this.addChrt = false;
            this.editChrt = false;
            this.saveChrtButton.Enabled = false;
            this.editChrtButton.Enabled = this.addAccounts;
            this.addChrtButton.Enabled = this.editAccounts;
            this.deleteChrtButton.Enabled = this.delAccounts;
            this.editChrtButton.Text = "EDIT";
            this.editAcntMenuItem.Text = "&Edit Account";
            this.accntNumTextBox.ReadOnly = true;
            this.accntNumTextBox.BackColor = Color.WhiteSmoke;
            this.accntNameTextBox.ReadOnly = true;
            this.accntNameTextBox.BackColor = Color.WhiteSmoke;
            this.accntDescTextBox.ReadOnly = true;
            this.accntDescTextBox.BackColor = Color.WhiteSmoke;
            this.accClsfctnComboBox.BackColor = Color.WhiteSmoke;

            this.parentAccntTextBox.ReadOnly = true;
            this.parentAccntTextBox.BackColor = Color.WhiteSmoke;
            this.cntrlAccntTextBox.ReadOnly = true;
            this.cntrlAccntTextBox.BackColor = Color.WhiteSmoke;
            this.accntCrncyNmTextBox.ReadOnly = true;
            this.accntCrncyNmTextBox.BackColor = Color.WhiteSmoke;

            this.rptLnNoUpDown.Increment = 0;
            this.rptLnNoUpDown.ReadOnly = true;
            this.rptLnNoUpDown.BackColor = Color.WhiteSmoke;
        }

        private bool shdObeyChrtEvts()
        {
            return this.obey_chrt_evnts;
        }

        private void ChrtPnlNavButtons(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecChrtLabel.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.is_last_chrt = false;
                this.chrt_cur_indx = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.is_last_chrt = false;
                this.chrt_cur_indx -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.is_last_chrt = false;
                this.chrt_cur_indx += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.is_last_chrt = true;
                this.totl_chrt = Global.get_Total_Chrts(this.searchForChrtTextBox.Text,
                 this.searchInChrtComboBox.Text, Global.mnFrm.cmCde.Org_id);
                this.updtChrtTotals();
                this.chrt_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            this.getChrtPnlData();
        }

        private void parntAccntButton_Click(object sender, EventArgs e)
        {
            if (this.addChrt == false && this.editChrt == false)
            {
                Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                return;
            }
            if (this.accntTypeComboBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please select an Account Type First!", 0);
                return;
            }

            string[] selVals = new string[1];
            selVals[0] = this.parentAccntIDTextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
             Global.mnFrm.cmCde.getLovID("Parent Accounts"), ref selVals,
             true, false, Global.mnFrm.cmCde.Org_id,
             this.accntTypeComboBox.Text.Substring(0, 2).Trim(), "");
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.parentAccntIDTextBox.Text = selVals[i];
                    this.parentAccntTextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));
                }
            }
            //if (int.Parse(this.accntIDTextBox.Text) > 0)
            //{
            //  Global.updtAccntPrntID(int.Parse(this.accntIDTextBox.Text),
            //    int.Parse(this.parentAccntIDTextBox.Text));
            //}
        }

        private void accntsExtraInfoButton_Click(object sender, EventArgs e)
        {
            if (this.accntIDTextBox.Text == "" ||
             this.accntIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("No record to View!", 0);
                return;
            }
            bool canEdt = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[12]);
            DialogResult dgres = this.cmCde.showRowsExtInfDiag(this.cmCde.getMdlGrpID("Chart of Accounts"),
             long.Parse(this.accntIDTextBox.Text), "accb.accb_all_other_info_table", this.accntNameTextBox.Text, canEdt, 10, 9,
                "accb.accb_all_other_info_table_dflt_row_id_seq");
            if (dgres == DialogResult.OK)
            {
            }
        }

        private void goChrtButton_Click(object sender, EventArgs e)
        {
            this.disableChrtEdit();
            System.Windows.Forms.Application.DoEvents();
            this.loadAccntChrtPanel();
        }

        private void addChrtButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[11]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            this.clearChrtInfo();
            this.addChrt = true;
            this.editChrt = false;
            this.prpareForChrtEdit();
            this.addChrtButton.Enabled = false;
            this.editChrtButton.Enabled = false;
            this.deleteChrtButton.Enabled = false;
            this.txtChngd = false;
            this.accntCurrIDTextBox.Text = Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id).ToString();
            this.accntCrncyNmTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(int.Parse(this.accntCurrIDTextBox.Text)) +
                  " - " + Global.mnFrm.cmCde.getPssblValDesc(int.Parse(this.accntCurrIDTextBox.Text));
            this.txtChngd = false;
        }

        private void editChrtButton_Click(object sender, EventArgs e)
        {
            if (this.editChrtButton.Text == "EDIT")
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[12]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                     " this action!\nContact your System Administrator!", 0);
                    return;
                }
                if (this.accntIDTextBox.Text == "" || this.accntIDTextBox.Text == "-1")
                {
                    Global.mnFrm.cmCde.showMsg("No record to Edit!", 0);
                    return;
                }
                if (this.netBalNumericUpDown.Value != 0
                  || this.dbtBalNumericUpDown.Value != 0
                  || this.crdtBalNumericUpDown.Value != 0
                  || this.accntSgmnt1TextBox.Text != "-1")
                {
                    this.isPrntAccntsCheckBox.Enabled = false;
                    this.isContraCheckBox.Enabled = false;
                    this.isRetEarnsCheckBox.Enabled = false;
                    this.isNetIncmCheckBox.Enabled = false;
                    this.hasSubldgrCheckBox.Enabled = false;
                    this.isSuspensCheckBox.Enabled = false;
                    this.accntTypeComboBox.Enabled = false;
                    this.cntrlAccntButton.Enabled = false;
                    this.cntrlAccntTextBox.Enabled = false;
                    this.accntCrncyNmTextBox.Enabled = false;
                    this.accntCurrButton.Enabled = false;
                }
                this.addChrt = false;
                this.editChrt = true;
                this.prpareForChrtEdit();
                this.addChrtButton.Enabled = false;
                this.editChrtButton.Enabled = false;
                this.deleteChrtButton.Enabled = false;
                this.editChrtButton.Text = "STOP";
                this.editAcntMenuItem.Text = "STOP EDITING";
            }
            else
            {
                this.disableChrtEdit();
                System.Windows.Forms.Application.DoEvents();
                this.loadAccntChrtPanel();
            }
        }

        private void deleteChrtButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[13]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.accntsChrtListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the record to delete!", 0);
                return;
            }
            long accntid = long.Parse(this.accntsChrtListView.SelectedItems[0].SubItems[3].Text);
            if (Global.get_Accnt_Tot_Trns(accntid) > 0)
            {
                Global.mnFrm.cmCde.showMsg("Cannot delete accounts with Transactions in their Name!", 0);
                return;
            }

            if (Global.get_Accnt_Tot_Chldrn(accntid) > 0)
            {
                Global.mnFrm.cmCde.showMsg("Cannot delete Parent Accounts with Child Accounts!", 0);
                return;
            }
            if (Global.get_Accnt_Tot_Mappngs(accntid) > 0)
            {
                Global.mnFrm.cmCde.showMsg("Cannot delete Accounts with Subsidiary Account Mappings!", 0);
                return;
            }
            if (Global.get_Accnt_Tot_Pymnts(accntid) > 0)
            {
                Global.mnFrm.cmCde.showMsg("Cannot delete accounts with Personnel Payments in their Name!", 0);
                return;
            }
            if (Global.get_Accnt_Tot_PyItms(accntid) > 0)
            {
                Global.mnFrm.cmCde.showMsg("Cannot delete accounts with Pay Items in their Name!", 0);
                return;
            }

            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Account?" +
             "\r\nThis action cannot be undone!", 1) == DialogResult.No)
            {
                Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            Global.deleteAccount(accntid, this.accntsChrtListView.SelectedItems[0].SubItems[1].Text
              , this.accntsChrtListView.SelectedItems[0].SubItems[2].Text);
            this.loadAccntChrtPanel();
        }

        private void saveChrtButton_Click(object sender, EventArgs e)
        {
            if (this.addChrt == true)
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
            char[] w = { '.' };
            this.accntNumTextBox.Text = this.accntNumTextBox.Text.Trim(w);
            this.accntNameTextBox.Text = this.accntNameTextBox.Text.Trim(w);
            if (this.accntNameTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please enter an Account Name!", 0);
                return;
            }
            if (this.accntNumTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please enter an Account Number!", 0);
                return;
            }
            if (this.accntTypeComboBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please select an account Type!", 0);
                return;
            }
            if (this.isRetEarnsCheckBox.Checked == true && this.isPrntAccntsCheckBox.Checked == true)
            {
                Global.mnFrm.cmCde.showMsg("A Parent account cannot be used as Retained Earinings Account!", 0);
                return;
            }
            if (this.isRetEarnsCheckBox.Checked == true && this.isContraCheckBox.Checked == true)
            {
                Global.mnFrm.cmCde.showMsg("A contra account cannot be used as Retained Earinings Account!", 0);
                return;
            }
            if (this.isRetEarnsCheckBox.Checked == true && this.isEnabledAccntsCheckBox.Checked == false)
            {
                Global.mnFrm.cmCde.showMsg("A Retained Earnings Account cannot be disabled!", 0);
                return;
            }

            if (this.isSuspensCheckBox.Checked == true && this.accntTypeComboBox.Text != "A -ASSET")
            {
                Global.mnFrm.cmCde.showMsg("The account type of the Suspense Account must be ASSET", 0);
                return;
            }

            if (this.isRetEarnsCheckBox.Checked == true && this.accntTypeComboBox.Text != "EQ-EQUITY")
            {
                Global.mnFrm.cmCde.showMsg("The account type of a Retained Earnings Account must be NET WORTH", 0);
                return;
            }

            if (this.isNetIncmCheckBox.Checked == true && this.isPrntAccntsCheckBox.Checked == true)
            {
                Global.mnFrm.cmCde.showMsg("A Parent account cannot be used as Net Income Account!", 0);
                return;
            }
            if (this.isNetIncmCheckBox.Checked == true && this.isContraCheckBox.Checked == true)
            {
                Global.mnFrm.cmCde.showMsg("A contra account cannot be used as Net Income Account!", 0);
                return;
            }
            if (this.isNetIncmCheckBox.Checked == true && this.isEnabledAccntsCheckBox.Checked == false)
            {
                Global.mnFrm.cmCde.showMsg("A Net Income Account cannot be disabled!", 0);
                return;
            }
            if (this.isNetIncmCheckBox.Checked == true && this.accntTypeComboBox.Text != "EQ-EQUITY")
            {
                Global.mnFrm.cmCde.showMsg("The account type of a Net Income Account must be NET WORTH", 0);
                return;
            }
            if (this.isRetEarnsCheckBox.Checked == true && this.isNetIncmCheckBox.Checked == true)
            {
                Global.mnFrm.cmCde.showMsg("Same Account cannot be Retained Earnings and Net Income at same time!", 0);
                return;
            }
            if (this.isRetEarnsCheckBox.Checked == true && this.hasSubldgrCheckBox.Checked == true)
            {
                Global.mnFrm.cmCde.showMsg("Retained Earnings account cannot have sub-ledgers!", 0);
                return;
            }
            if (this.isNetIncmCheckBox.Checked == true && this.hasSubldgrCheckBox.Checked == true)
            {
                Global.mnFrm.cmCde.showMsg("Net Income account cannot have sub-ledgers!", 0);
                return;
            }
            if (this.isContraCheckBox.Checked == true && this.hasSubldgrCheckBox.Checked == true)
            {
                Global.mnFrm.cmCde.showMsg("The system does not support Sub-Ledgers on Contra-Accounts!", 0);
                return;
            }
            if (this.isPrntAccntsCheckBox.Checked == true && this.hasSubldgrCheckBox.Checked == true)
            {
                Global.mnFrm.cmCde.showMsg("Parent Account cannot have sub-ledgers!", 0);
                return;
            }
            if (this.cntrlAccntIDTextBox.Text != "-1" && this.hasSubldgrCheckBox.Checked == true)
            {
                Global.mnFrm.cmCde.showMsg("The system does not support Control Accounts reporting to other Control Account!", 0);
                return;
            }
            if (this.cntrlAccntIDTextBox.Text != "-1" && this.parentAccntIDTextBox.Text != "-1")
            {
                Global.mnFrm.cmCde.showMsg("An Account with a Control Account cannot have a Parent Account as well!", 0);
                return;
            }
            if (this.parentAccntIDTextBox.Text != "-1")
            {
                if (Global.mnFrm.cmCde.getAccntType(int.Parse(parentAccntIDTextBox.Text)) !=
                 this.accntTypeComboBox.Text.Substring(0, 2).Trim())
                {
                    Global.mnFrm.cmCde.showMsg("Account Type does not match that of the Parent Account", 0);
                    return;
                }
            }
            if (this.accntCurrIDTextBox.Text == "-1" || this.accntCurrIDTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Account Currency Cannot be Empty!", 0);
                return;
            }
            int oldAccntNosID = Global.mnFrm.cmCde.getAccntID(this.accntNumTextBox.Text, Global.mnFrm.cmCde.Org_id);
            if (oldAccntNosID > 0
             && this.addChrt == true)
            {
                Global.mnFrm.cmCde.showMsg("Account Number is already in use in this Organization!", 0);
                return;
            }
            if (oldAccntNosID > 0
             && this.editChrt == true
             && oldAccntNosID.ToString() != this.accntIDTextBox.Text)
            {
                Global.mnFrm.cmCde.showMsg("New Account Number is already in use in this Organization!", 0);
                return;
            }

            int oldAccntNmID = Global.mnFrm.cmCde.getAccntID(this.accntNameTextBox.Text, Global.mnFrm.cmCde.Org_id);
            if (oldAccntNmID > 0
             && this.addChrt == true)
            {
                Global.mnFrm.cmCde.showMsg("Account Name is already in use in this Organization!", 0);
                return;
            }
            if (oldAccntNmID > 0
             && this.editChrt == true
             && oldAccntNmID.ToString() != this.accntIDTextBox.Text)
            {
                Global.mnFrm.cmCde.showMsg("New Account Name is already in use in this Organization!", 0);
                return;
            }
            if (this.addChrt == true)
            {
                Global.createChrt(Global.mnFrm.cmCde.Org_id,
                 this.accntNumTextBox.Text, this.accntNameTextBox.Text, this.accntDescTextBox.Text,
                 this.isContraCheckBox.Checked, int.Parse(this.parentAccntIDTextBox.Text),
                 this.accntTypeComboBox.Text.Substring(0, 2).Trim(),
                 this.isPrntAccntsCheckBox.Checked, this.isEnabledAccntsCheckBox.Checked,
                 this.isRetEarnsCheckBox.Checked, this.isNetIncmCheckBox.Checked,
                 (int)this.rptLnNoUpDown.Value, this.hasSubldgrCheckBox.Checked,
                 int.Parse(this.cntrlAccntIDTextBox.Text),
                 int.Parse(this.accntCurrIDTextBox.Text), this.isSuspensCheckBox.Checked,
                 this.accClsfctnComboBox.Text,
                 int.Parse(this.accntSgmnt1TextBox.Text),
                 int.Parse(this.accntSgmnt2TextBox.Text),
                 int.Parse(this.accntSgmnt3TextBox.Text),
                 int.Parse(this.accntSgmnt4TextBox.Text),
                 int.Parse(this.accntSgmnt5TextBox.Text),
                 int.Parse(this.accntSgmnt6TextBox.Text),
                 int.Parse(this.accntSgmnt7TextBox.Text),
                 int.Parse(this.accntSgmnt8TextBox.Text),
                 int.Parse(this.accntSgmnt9TextBox.Text),
                 int.Parse(this.accntSgmnt10TextBox.Text),
                 int.Parse(this.mappedAccntIDTextBox.Text));
                this.saveChrtButton.Enabled = false;
                this.addChrt = false;
                this.editChrt = false;
                this.editChrtButton.Enabled = this.addAccounts;
                this.addChrtButton.Enabled = this.editAccounts;
                this.deleteChrtButton.Enabled = this.delAccounts;
                System.Windows.Forms.Application.DoEvents();
                this.loadAccntChrtPanel();
            }
            else if (this.editChrt == true)
            {
                Global.updateChrtDet(Global.mnFrm.cmCde.Org_id, int.Parse(this.accntIDTextBox.Text),
                 this.accntNumTextBox.Text, this.accntNameTextBox.Text, this.accntDescTextBox.Text,
                 this.isContraCheckBox.Checked, int.Parse(this.parentAccntIDTextBox.Text),
                 this.accntTypeComboBox.Text.Substring(0, 2).Trim(),
                 this.isPrntAccntsCheckBox.Checked, this.isEnabledAccntsCheckBox.Checked,
                 this.isRetEarnsCheckBox.Checked, this.isNetIncmCheckBox.Checked,
                 (int)this.rptLnNoUpDown.Value, this.hasSubldgrCheckBox.Checked,
                 int.Parse(this.cntrlAccntIDTextBox.Text),
                 int.Parse(this.accntCurrIDTextBox.Text), this.isSuspensCheckBox.Checked,
                 this.accClsfctnComboBox.Text,
                 int.Parse(this.accntSgmnt1TextBox.Text),
                 int.Parse(this.accntSgmnt2TextBox.Text),
                 int.Parse(this.accntSgmnt3TextBox.Text),
                 int.Parse(this.accntSgmnt4TextBox.Text),
                 int.Parse(this.accntSgmnt5TextBox.Text),
                 int.Parse(this.accntSgmnt6TextBox.Text),
                 int.Parse(this.accntSgmnt7TextBox.Text),
                 int.Parse(this.accntSgmnt8TextBox.Text),
                 int.Parse(this.accntSgmnt9TextBox.Text),
                 int.Parse(this.accntSgmnt10TextBox.Text),
                 int.Parse(this.mappedAccntIDTextBox.Text));
                if (this.accntsChrtListView.SelectedItems.Count > 0)
                {
                    this.accntsChrtListView.SelectedItems[0].SubItems[1].Text = this.accntNumTextBox.Text;
                    this.accntsChrtListView.SelectedItems[0].SubItems[2].Text = this.accntNameTextBox.Text;
                }
                Global.mnFrm.cmCde.showMsg("Record Saved!", 3);
            }
        }

        int benhr = 0;

        private void accntsChrtListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.shdObeyChrtEvts() == false || this.accntsChrtListView.SelectedItems.Count > 1)
            {
                return;
            }
            if (this.accntsChrtListView.SelectedItems.Count > 0)
            {
                this.populateChrtDet(int.Parse(this.accntsChrtListView.SelectedItems[0].SubItems[3].Text));
            }
            else
            {
                this.populateChrtDet(-12345);
            }
        }

        private void accntsChrtListView_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.vwAccntTrnsctnsButton_Click(this.vwAccntTrnsctnsButton, ex);
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                if (this.saveChrtButton.Enabled == true)
                {
                    this.saveChrtButton_Click(this.saveChrtButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                if (this.addChrtButton.Enabled == true)
                {
                    this.addChrtButton_Click(this.addChrtButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                if (this.editChrtButton.Enabled == true)
                {
                    this.editChrtButton_Click(this.editChrtButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)
            {
                if (this.rfrshChrtButton.Enabled == true)
                {
                    this.rfrshChrtButton_Click(this.rfrshChrtButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                this.resetChrtButton.PerformClick();
            }
            else if (e.Control && e.KeyCode == Keys.Delete)
            {
                if (this.voidBatchButton.Enabled == true)
                {
                    this.voidBatchButton_Click(this.voidBatchButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                Global.mnFrm.cmCde.listViewKeyDown(this.accntsChrtListView, e);
            }
        }

        private void accntsChrtListView_DoubleClick(object sender, System.EventArgs e)
        {
            this.vwAccntTrnsctnsButton_Click(this.vwAccntTrnsctnsButton, e);
        }

        private void exprtExclMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.accntsChrtListView);
        }

        private void exptChartButton_Click(object sender, EventArgs e)
        {
            string rspnse = Interaction.InputBox("What Chart of Accounts will you like to Export?" +
              "\r\n1=This Organisations's Chart" +
              "\r\n2=General Organisations's Chart" +
            "\r\n3=Empty Template\r\n",
              "Rhomicom", "1", (Global.mnFrm.cmCde.myComputer.Screen.Bounds.Width / 2) - 170,
              (Global.mnFrm.cmCde.myComputer.Screen.Bounds.Height / 2) - 100);
            if (rspnse.Equals(string.Empty) || rspnse.Equals(null))
            {
                Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            int rsponse = 0;
            bool rsps = int.TryParse(rspnse, out rsponse);
            if (rsps == false)
            {
                Global.mnFrm.cmCde.showMsg("Invalid Option! Expecting 1-3", 4);
                return;
            }
            if (rsponse < 1 || rsponse > 3)
            {
                Global.mnFrm.cmCde.showMsg("Invalid Option! Expecting 1-3", 4);
                return;
            }
            Global.mnFrm.cmCde.exprtChrtTmp(rsponse);
        }

        private void importChartButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[11]) == false)
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
                Global.mnFrm.cmCde.imprtChrtTmp(this.openFileDialog1.FileName);
                this.loadAccntChrtPanel();
            }
            else
            {
                Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 0);
                return;
            }
        }

        private void vwSQLChrtButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(Global.mnFrm.chrt_SQL, 10);
        }

        private void recHstryChrtButton_Click(object sender, EventArgs e)
        {
            if (this.accntIDTextBox.Text == "-1"
         || this.accntIDTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please select an Account First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(Global.get_Chrt_Rec_Hstry(int.Parse(this.accntIDTextBox.Text)), 9);
        }

        private void addAcntMenuItem_Click(object sender, EventArgs e)
        {
            this.addChrtButton_Click(this.addChrtButton, e);
        }

        private void editAcntMenuItem_Click(object sender, EventArgs e)
        {
            this.editChrtButton_Click(this.editChrtButton, e);
        }

        private void delAcntMenuItem_Click(object sender, EventArgs e)
        {
            this.deleteChrtButton_Click(this.deleteChrtButton, e);
        }

        private void rfrshAcntMenuItem_Click(object sender, EventArgs e)
        {
            this.goChrtButton_Click(this.goChrtButton, e);
        }

        private void rcHstryAcntMenuItem_Click(object sender, EventArgs e)
        {
            this.recHstryChrtButton_Click(this.recHstryChrtButton, e);
        }

        private void vwSQLAcntMenuItem_Click(object sender, EventArgs e)
        {
            this.vwSQLChrtButton_Click(this.vwSQLChrtButton, e);
        }

        private void searchForChrtTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.goChrtButton_Click(this.goChrtButton, ex);
            }
        }

        private void positionChrtTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                this.ChrtPnlNavButtons(this.movePreviousChrtButton, ex);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
            {
                this.ChrtPnlNavButtons(this.moveNextChrtButton, ex);
            }
        }

        private void isPrntAccntsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shdObeyChrtEvts() == false
             || beenToIsprntfunc == true)
            {
                beenToIsprntfunc = false;
                return;
            }
            beenToIsprntfunc = true;
            if (this.addChrt == false && this.editChrt == false)
            {
                this.isPrntAccntsCheckBox.Checked = !this.isPrntAccntsCheckBox.Checked;
            }
        }

        private void isContraCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shdObeyChrtEvts() == false
             || beenToIsContra == true)
            {
                beenToIsContra = false;
                return;
            }
            beenToIsContra = true;
            if (this.addChrt == false && this.editChrt == false)
            {
                this.isContraCheckBox.Checked = !this.isContraCheckBox.Checked;
            }
        }

        private void isRetEarnsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shdObeyChrtEvts() == false
             || beenToIsRetEarns == true)
            {
                beenToIsRetEarns = false;
                return;
            }
            beenToIsRetEarns = true;
            if (this.addChrt == false && this.editChrt == false)
            {
                this.isRetEarnsCheckBox.Checked = !this.isRetEarnsCheckBox.Checked;
            }
        }

        private void isNetIncmCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shdObeyChrtEvts() == false
             || beenToIsNetInc == true)
            {
                beenToIsNetInc = false;
                return;
            }
            beenToIsNetInc = true;
            if (this.addChrt == false && this.editChrt == false)
            {
                this.isNetIncmCheckBox.Checked = !this.isNetIncmCheckBox.Checked;
            }
        }

        private void isEnabledAccntsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shdObeyChrtEvts() == false
             || beenToIsEnabled == true)
            {
                beenToIsEnabled = false;
                return;
            }
            beenToIsEnabled = true;
            if (this.addChrt == false && this.editChrt == false)
            {
                this.isEnabledAccntsCheckBox.Checked = !this.isEnabledAccntsCheckBox.Checked;
            }
        }

        private void vwAccntTrnsctnsButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[2]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.accntIDTextBox.Text == "" ||
          this.accntIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("No record to View!", 0);
                return;
            }
            vwTrnsctnsDiag nwDiag = new vwTrnsctnsDiag();
            nwDiag.my_org_id = Global.mnFrm.cmCde.Org_id;
            nwDiag.accnt_name = this.accntNumTextBox.Text;
            nwDiag.accntid = int.Parse(this.accntIDTextBox.Text);
            DialogResult dgres = nwDiag.ShowDialog();
            if (dgres == DialogResult.OK)
            {

            }
        }
        #endregion

        #region "INTERNAL/MEMO ACCOUNTS..."
        private void loadIMAPanel()
        {
            this.obey_ima_evnts = false;
            if (this.searchInIMAComboBox.SelectedIndex < 0)
            {
                this.searchInIMAComboBox.SelectedIndex = 0;
            }
            if (this.searchForIMATextBox.Text.Contains("%") == false)
            {
                this.searchForIMATextBox.Text = "%" + this.searchForIMATextBox.Text.Replace(" ", "%") + "%";
            }
            if (this.searchForIMATextBox.Text == "%%")
            {
                this.searchForIMATextBox.Text = "%";
            }
            int dsply = 0;
            if (this.dsplySizeIMAComboBox.Text == ""
              || int.TryParse(this.dsplySizeIMAComboBox.Text, out dsply) == false)
            {
                this.dsplySizeIMAComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            //this.groupBox8.Height = this.g.Bottom - this.toolStrip4.Bottom - 50;
            this.is_last_ima = false;
            this.totl_ima = Global.mnFrm.cmCde.Big_Val;
            this.getIMAPnlData();
            this.obey_ima_evnts = true;
        }

        private void getIMAPnlData()
        {
            this.updtIMATotals();
            this.populateIMAListVw();
            this.updtIMANavLabels();
        }

        private void updtIMATotals()
        {
            Global.mnFrm.cmCde.navFuncts.FindNavigationIndices(
              long.Parse(this.dsplySizeIMAComboBox.Text), this.totl_ima);
            if (this.ima_cur_indx >= Global.mnFrm.cmCde.navFuncts.totalGroups)
            {
                this.ima_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            if (this.ima_cur_indx < 0)
            {
                this.ima_cur_indx = 0;
            }
            Global.mnFrm.cmCde.navFuncts.currentNavigationIndex = this.ima_cur_indx;
        }

        private void updtIMANavLabels()
        {
            this.moveFirstIMAButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveFirstBtnStatus();
            this.movePreviousIMAButton.Enabled = Global.mnFrm.cmCde.navFuncts.movePrevBtnStatus();
            this.moveNextIMAButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveNextBtnStatus();
            this.moveLastIMAButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveLastBtnStatus();
            this.positionIMATextBox.Text = Global.mnFrm.cmCde.navFuncts.displayedRecordsNumbers();
            if (this.is_last_ima == true ||
              this.totl_ima != Global.mnFrm.cmCde.Big_Val)
            {
                this.totalRecsIMALabel.Text = Global.mnFrm.cmCde.navFuncts.totalRecordsLabel();
            }
            else
            {
                this.totalRecsIMALabel.Text = "of Total";
            }
        }

        private void populateIMAListVw()
        {
            this.obey_ima_evnts = false;
            DataSet dtst = Global.get_Basic_IMA(this.searchForIMATextBox.Text,
              this.searchInIMAComboBox.Text, this.ima_cur_indx,
              int.Parse(this.dsplySizeIMAComboBox.Text), Global.mnFrm.cmCde.Org_id);
            this.imaListView.Items.Clear();
            this.clearIMAInfo();
            this.loadIMADetPanel();
            if (!this.editima)
            {
                this.disableIMAEdit();
            }
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.last_ima_num = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
                ListViewItem nwItem = new ListViewItem(new string[] {
    (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),
    dtst.Tables[0].Rows[i][0].ToString()});
                this.imaListView.Items.Add(nwItem);
            }
            this.correctIMANavLbls(dtst);
            if (this.imaListView.Items.Count > 0)
            {
                this.obey_ima_evnts = true;
                this.imaListView.Items[0].Selected = true;
            }
            else
            {
            }
            this.obey_ima_evnts = true;
        }

        private void correctIMANavLbls(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.ima_cur_indx == 0 && totlRecs == 0)
            {
                this.is_last_ima = true;
                this.totl_ima = 0;
                this.last_ima_num = 0;
                this.ima_cur_indx = 0;
                this.updtIMATotals();
                this.updtIMANavLabels();
            }
            else if (this.totl_ima == Global.mnFrm.cmCde.Big_Val
           && totlRecs < long.Parse(this.dsplySizeIMAComboBox.Text))
            {
                this.totl_ima = this.last_ima_num;
                if (totlRecs == 0)
                {
                    this.ima_cur_indx -= 1;
                    this.updtIMATotals();
                    this.populateIMAListVw();
                }
                else
                {
                    this.updtIMATotals();
                }
            }
        }

        private bool shdObeyIMAEvts()
        {
            return this.obey_ima_evnts;
        }

        private void IMAPnlNavButtons(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecsIMALabel.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.is_last_ima = false;
                this.ima_cur_indx = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.is_last_ima = false;
                this.ima_cur_indx -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.is_last_ima = false;
                this.ima_cur_indx += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.is_last_ima = true;
                this.totl_ima = Global.get_Total_IMA(this.searchForIMATextBox.Text,
                  this.searchInIMAComboBox.Text, Global.mnFrm.cmCde.Org_id);
                this.updtIMATotals();
                this.ima_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            this.getIMAPnlData();
        }

        private void clearIMAInfo()
        {
            this.obey_ima_evnts = false;
            this.isEnbldIMACheckBox.Checked = false;
            this.hsFrmlrIMACheckBox.Checked = false;
            this.accNumIMATextBox.Text = "";
            this.accIDIMATextBox.Text = "-1";
            this.dfltCstActIMAIDTextBox.Text = "-1";
            this.dfltCstActIMATextBox.Text = "";
            this.dfltBalsActIMAIDTextBox.Text = "-1";
            this.dfltBalsActIMATextBox.Text = "";
            this.crncyIMAIDTextBox.Text = "-1";
            this.crncyIMATextBox.Text = "";
            this.acctSQLIMATextBox.Text = "";
            this.acctNmIMATextBox.Text = "";
            this.accntDescIMATextBox.Text = "";
            this.imaDtListView.Items.Clear();
            this.acctTypIMAComboBox.Items.Clear();

            this.balsDteIMATextBox.Text = "";
            this.dbtBalsIMAUpDown.Value = 0;
            this.crdtBalsIMAUpDown.Value = 0;
            this.netBalsIMAUpDown.Value = 0;
            this.dbtBalsIMAUpDown.BackColor = Color.Green;
            this.crdtBalsIMAUpDown.BackColor = Color.Green;
            this.netBalsIMAUpDown.BackColor = Color.Green;

            this.obey_ima_evnts = true;
        }

        private void prpareForIMAEdit()
        {
            this.saveIMAButton.Enabled = true;
            this.accNumIMATextBox.ReadOnly = false;
            this.accNumIMATextBox.BackColor = Color.FromArgb(255, 255, 128);
            this.acctNmIMATextBox.ReadOnly = false;
            this.acctNmIMATextBox.BackColor = Color.FromArgb(255, 255, 128);

            this.dfltCstActIMATextBox.ReadOnly = false;
            this.dfltCstActIMATextBox.BackColor = Color.FromArgb(255, 255, 255);
            this.dfltBalsActIMATextBox.ReadOnly = false;
            this.dfltBalsActIMATextBox.BackColor = Color.FromArgb(255, 255, 255);
            this.crncyIMATextBox.ReadOnly = false;
            this.crncyIMATextBox.BackColor = Color.FromArgb(255, 255, 128);

            this.acctSQLIMATextBox.ReadOnly = false;
            this.crncyIMATextBox.BackColor = Color.FromArgb(255, 255, 128);

            this.accntDescIMATextBox.ReadOnly = false;
            this.accntDescIMATextBox.BackColor = Color.White;
            this.acctTypIMAComboBox.BackColor = Color.FromArgb(255, 255, 128);
            //A -ASSET
            //EQ-EQUITY
            //L -LIABILITY
            //R -REVENUE
            //EX-EXPENSE
            object orgnlItm = null;
            if (this.acctTypIMAComboBox.SelectedIndex >= 0)
            {
                orgnlItm = this.acctTypIMAComboBox.SelectedItem;
            }
            this.acctTypIMAComboBox.Items.Clear();
            this.acctTypIMAComboBox.Items.Add("A -ASSET");
            this.acctTypIMAComboBox.Items.Add("EQ-EQUITY");
            this.acctTypIMAComboBox.Items.Add("L -LIABILITY");
            this.acctTypIMAComboBox.Items.Add("R -REVENUE");
            this.acctTypIMAComboBox.Items.Add("EX-EXPENSE");
            if (orgnlItm != null)
            {
                this.acctTypIMAComboBox.SelectedItem = orgnlItm;
            }
        }

        private void disableIMAEdit()
        {
            this.addima = false;
            this.editima = false;
            this.saveIMAButton.Enabled = false;
            this.addIMAButton.Enabled = this.addimas;
            this.editIMAButton.Enabled = this.editimas;
            this.delIMAButton.Enabled = this.delimas;
            this.addIMADtButton.Enabled = this.editimas;
            this.delIMADtButton.Enabled = this.editimas;
            this.accNumIMATextBox.ReadOnly = true;
            this.accNumIMATextBox.BackColor = Color.WhiteSmoke;
            this.acctNmIMATextBox.ReadOnly = true;
            this.acctNmIMATextBox.BackColor = Color.WhiteSmoke;

            this.dfltCstActIMATextBox.ReadOnly = true;
            this.dfltCstActIMATextBox.BackColor = Color.WhiteSmoke;
            this.dfltBalsActIMATextBox.ReadOnly = true;
            this.dfltBalsActIMATextBox.BackColor = Color.WhiteSmoke;
            this.crncyIMATextBox.ReadOnly = true;
            this.crncyIMATextBox.BackColor = Color.WhiteSmoke;

            this.acctSQLIMATextBox.ReadOnly = true;
            this.crncyIMATextBox.BackColor = Color.WhiteSmoke;

            this.accntDescIMATextBox.ReadOnly = true;
            this.accntDescIMATextBox.BackColor = Color.WhiteSmoke;
            this.acctTypIMAComboBox.BackColor = Color.WhiteSmoke;
            if (this.acctTypIMAComboBox.SelectedIndex >= 0)
            {
                object orgnlItm = this.acctTypIMAComboBox.SelectedItem;
                this.acctTypIMAComboBox.Items.Clear();
                this.acctTypIMAComboBox.Items.Add(orgnlItm);
                this.acctTypIMAComboBox.SelectedItem = orgnlItm;
            }
            else
            {
                this.acctTypIMAComboBox.Items.Clear();
            }
            this.acctTypIMAComboBox.BackColor = Color.WhiteSmoke;
        }

        private void imaListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.shdObeyIMAEvts() == false || this.imaListView.SelectedItems.Count > 1)
            {
                return;
            }
            if (this.imaListView.SelectedItems.Count == 1)
            {
                this.populateIMADet(int.Parse(this.imaListView.SelectedItems[0].SubItems[2].Text));
            }
            else
            {
                this.clearIMAInfo();
                this.disableIMAEdit();
                this.loadIMADetPanel();
            }
        }

        private void populateIMADet(int accntID)
        {
            this.clearIMAInfo();
            this.disableIMAEdit();
            this.obey_ima_evnts = false;
            this.imaDtListView.Items.Clear();
            DataSet dtst = Global.get_Basic_IMADet(accntID);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                this.accIDIMATextBox.Text = dtst.Tables[0].Rows[0][0].ToString();
                this.accNumIMATextBox.Text = dtst.Tables[0].Rows[0][1].ToString();
                this.acctNmIMATextBox.Text = dtst.Tables[0].Rows[0][2].ToString();
                this.accntDescIMATextBox.Text = dtst.Tables[0].Rows[0][3].ToString();

                this.dfltCstActIMAIDTextBox.Text = dtst.Tables[0].Rows[0][8].ToString();
                this.dfltCstActIMATextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(dtst.Tables[0].Rows[0][8].ToString()));

                this.dfltBalsActIMAIDTextBox.Text = dtst.Tables[0].Rows[0][9].ToString();
                this.dfltBalsActIMATextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(dtst.Tables[0].Rows[0][9].ToString()));

                this.crncyIMAIDTextBox.Text = dtst.Tables[0].Rows[0][7].ToString();
                this.crncyIMATextBox.Text = Global.mnFrm.cmCde.getPssblValNm(int.Parse(dtst.Tables[0].Rows[0][7].ToString()))
                  + " - " + Global.mnFrm.cmCde.getPssblValDesc(int.Parse(dtst.Tables[0].Rows[0][7].ToString()));
                this.imaCurrLabel.Text = "ACCOUNT CURRENCY BALANCE (" +
                  Global.mnFrm.cmCde.getPssblValNm(int.Parse(dtst.Tables[0].Rows[0][7].ToString())) + ")";

                this.isEnbldIMACheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(
                  dtst.Tables[0].Rows[0][4].ToString());

                this.hsFrmlrIMACheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(
              dtst.Tables[0].Rows[0][10].ToString());

                this.isCntraIMACheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(
             dtst.Tables[0].Rows[0][12].ToString());
                this.acctSQLIMATextBox.Text = dtst.Tables[0].Rows[0][11].ToString();

                this.acctTypIMAComboBox.Items.Clear();
                //string orgnlItm = dtst.Tables[0].Rows[0][5].ToString();
                //this.acctTypIMAComboBox.Items.Add(orgnlItm);
                //this.acctTypIMAComboBox.SelectedItem = orgnlItm;
                this.accntTypeComboBox.Items.Clear();
                if (dtst.Tables[0].Rows[0][5].ToString() == "A")
                {
                    this.accntTypeComboBox.Items.Add("A -ASSET");
                }
                else if (dtst.Tables[0].Rows[0][5].ToString() == "EQ")
                {
                    this.accntTypeComboBox.Items.Add("EQ-EQUITY");
                }
                else if (dtst.Tables[0].Rows[0][5].ToString() == "L")
                {
                    this.accntTypeComboBox.Items.Add("L -LIABILITY");
                }
                else if (dtst.Tables[0].Rows[0][5].ToString() == "R")
                {
                    this.accntTypeComboBox.Items.Add("R -REVENUE");
                }
                else if (dtst.Tables[0].Rows[0][5].ToString() == "EX")
                {
                    this.accntTypeComboBox.Items.Add("EX-EXPENSE");
                }
                if (this.accntTypeComboBox.Items.Count > 0)
                {
                    this.accntTypeComboBox.SelectedIndex = 0;
                }

                string dte = Global.mnFrm.cmCde.getFrmtdDB_Date_time();
                string[] vals = Global.getIMALstDailyBalsInfo(int.Parse(dtst.Tables[0].Rows[0][0].ToString()), dte);
                float a = 0;
                float b = 0;
                float c = 0;
                float.TryParse(vals[0], out a);
                float.TryParse(vals[1], out b);
                float.TryParse(vals[2], out c);

                this.dbtBalsIMAUpDown.Value = (Decimal)Math.Round(a, 2);
                this.crdtBalsIMAUpDown.Value = (Decimal)Math.Round(b, 2);
                this.netBalsIMAUpDown.Value = (Decimal)Math.Round(c, 2);
                this.balsDteIMATextBox.Text = vals[3].Substring(0, 11);
            }
            this.loadIMADetPanel();
            this.obey_ima_evnts = true;
        }

        private void loadIMADetPanel()
        {
            this.obey_imadt_evnts = false;
            if (this.searchInIMADtComboBox.SelectedIndex < 0)
            {
                this.searchInIMADtComboBox.SelectedIndex = 0;
            }
            if (this.searchForIMADtTextBox.Text.Contains("%") == false)
            {
                this.searchForIMADtTextBox.Text = "%" + this.searchForIMADtTextBox.Text.Replace(" ", "%") + "%";
            }
            if (this.searchForIMADtTextBox.Text == "%%")
            {
                this.searchForIMADtTextBox.Text = "%";
            }
            int dsply = 0;
            if (this.dsplySizeIMADtComboBox.Text == ""
             || int.TryParse(this.dsplySizeIMADtComboBox.Text, out dsply) == false)
            {
                this.dsplySizeIMADtComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            //this.groupBox8.Height = this.g.Bottom - this.toolStrip4.Bottom - 50;
            this.imadt_cur_indx = 0;
            this.is_last_imadt = false;
            this.last_imadt_num = 0;
            this.totl_imadt = Global.mnFrm.cmCde.Big_Val;
            this.getIMADTPnlData();
            this.obey_imadt_evnts = true;
            this.imaListView.Focus();
            //SendKeys.Send("{TAB}");
            //System.Windows.Forms.Application.DoEvents();
            //SendKeys.Send("{HOME}");
            //System.Windows.Forms.Application.DoEvents();
        }

        private void getIMADTPnlData()
        {
            this.updtIMADTTotals();
            this.populateIMADtListVw();
            this.updtIMADTNavLabels();
        }

        private void updtIMADTTotals()
        {
            int dsply = 0;
            if (this.dsplySizeIMADtComboBox.Text == ""
              || int.TryParse(this.dsplySizeIMADtComboBox.Text, out dsply) == false)
            {
                this.dsplySizeIMADtComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            this.myNav.FindNavigationIndices(
          long.Parse(this.dsplySizeIMADtComboBox.Text), this.totl_imadt);
            if (this.imadt_cur_indx >= this.myNav.totalGroups)
            {
                this.imadt_cur_indx = this.myNav.totalGroups - 1;
            }
            if (this.imadt_cur_indx < 0)
            {
                this.imadt_cur_indx = 0;
            }
            this.myNav.currentNavigationIndex = this.imadt_cur_indx;
        }

        private void updtIMADTNavLabels()
        {
            this.moveFirstIMADtButton.Enabled = this.myNav.moveFirstBtnStatus();
            this.movePreviousIMADtButton.Enabled = this.myNav.movePrevBtnStatus();
            this.moveNextIMADtButton.Enabled = this.myNav.moveNextBtnStatus();
            this.moveLastIMADtButton.Enabled = this.myNav.moveLastBtnStatus();
            this.positionIMADtTextBox.Text = this.myNav.displayedRecordsNumbers();
            if (this.is_last_imadt == true ||
             this.totl_imadt != Global.mnFrm.cmCde.Big_Val)
            {
                this.totalRecsIMADtLabel.Text = this.myNav.totalRecordsLabel();
            }
            else
            {
                this.totalRecsIMADtLabel.Text = "of Total";
            }
        }

        private void populateIMADtListVw()
        {
            this.obey_imadt_evnts = false;

            DataSet dtst = Global.get_IMA_Trns(this.searchForIMADtTextBox.Text, this.searchInIMADtComboBox.Text,
              this.imadt_cur_indx, int.Parse(this.dsplySizeIMADtComboBox.Text),
              this.strtDteIMATextBox.Text, this.endDteIMATextBox.Text,
              this.lowValIMAUpDown.Value, this.highValIMAUpDown.Value);
            //int.Parse(this.accIDIMATextBox.Text),
            this.imaDtListView.Items.Clear();

            int rwcnt = dtst.Tables[0].Rows.Count;
            for (int i = 0; i < rwcnt; i++)
            {
                this.last_imadt_num = this.myNav.startIndex() + i;
                ListViewItem nwItem = new ListViewItem(new string[] {
    (this.myNav.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),
    dtst.Tables[0].Rows[i][2].ToString(),
    dtst.Tables[0].Rows[i][3].ToString(),
    dtst.Tables[0].Rows[i][4].ToString(),
    dtst.Tables[0].Rows[i][5].ToString(),
    dtst.Tables[0].Rows[i][12].ToString(),
    dtst.Tables[0].Rows[i][6].ToString(),
    dtst.Tables[0].Rows[i][0].ToString(),
    dtst.Tables[0].Rows[i][7].ToString(),
    dtst.Tables[0].Rows[i][9].ToString(),
    dtst.Tables[0].Rows[i][8].ToString(),
    dtst.Tables[0].Rows[i][10].ToString(),
    dtst.Tables[0].Rows[i][11].ToString()});
                this.imaDtListView.Items.Add(nwItem);
            }
            this.correctIMADTNavLbls(dtst);
            this.obey_imadt_evnts = true;
        }

        private void correctIMADTNavLbls(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.totl_imadt == Global.mnFrm.cmCde.Big_Val
          && totlRecs < long.Parse(this.dsplySizeIMADtComboBox.Text))
            {
                this.totl_imadt = this.last_imadt_num;
                if (totlRecs == 0)
                {
                    this.imadt_cur_indx -= 1;
                    this.updtIMADTTotals();
                    this.populateIMADtListVw();
                }
                else
                {
                    this.updtIMADTTotals();
                }
            }
        }

        private bool shdObeyIMADTEvts()
        {
            return this.obey_imadt_evnts;
        }

        private void IMADTPnlNavButtons(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecsIMADtLabel.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.is_last_imadt = false;
                this.imadt_cur_indx = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.is_last_imadt = false;
                this.imadt_cur_indx -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.is_last_imadt = false;
                this.imadt_cur_indx += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.is_last_imadt = true;
                this.totl_imadt = Global.get_Total_IMA_Trns(this.searchForIMADtTextBox.Text, this.searchInIMADtComboBox.Text,
                 this.strtDteIMATextBox.Text, this.endDteIMATextBox.Text,
                 this.lowValIMAUpDown.Value, this.highValIMAUpDown.Value);
                // int.Parse(this.accIDIMATextBox.Text),
                this.updtIMADTTotals();
                this.imadt_cur_indx = this.myNav.totalGroups - 1;
            }
            this.getIMADTPnlData();
        }

        private void netBalsIMAUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (this.netBalsIMAUpDown.Value < 0 &&
             this.isCntraIMACheckBox.Checked == false)
            {
                this.netBalsIMAUpDown.BackColor = Color.Red;
            }
            else if (this.netBalsIMAUpDown.Value > 0 &&
             this.isContraCheckBox.Checked == true)
            {
                this.netBalsIMAUpDown.BackColor = Color.Red;
            }
            else
            {
                this.netBalsIMAUpDown.BackColor = Color.Green;
            }
        }
        #endregion

        #region "ACCOUNT TRANSACTIONS..."
        private void loadAccntTrnsPanel()
        {
            if (this.backgroundWorker1.IsBusy == true)
            {
                return;
            }
            this.obey_trns_evnts = false;
            if (this.searchInTrnsComboBox.SelectedIndex < 0)
            {
                this.searchInTrnsComboBox.SelectedIndex = 0;
            }
            if (searchForTrnsTextBox.Text.Contains("%") == false)
            {
                this.searchForTrnsTextBox.Text = "%" + this.searchForTrnsTextBox.Text.Replace(" ", "%") + "%";
            }
            if (this.searchForTrnsTextBox.Text == "%%")
            {
                this.searchForTrnsTextBox.Text = "%";
            }
            int dsply = 0;
            if (this.dsplySizeTrnsComboBox.Text == ""
             || int.TryParse(this.dsplySizeTrnsComboBox.Text, out dsply) == false)
            {
                this.dsplySizeTrnsComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            this.is_last_trns = false;
            this.totl_trns = Global.mnFrm.cmCde.Big_Val;
            this.trns_cur_indx = 0;
            this.getTrnsPnlData();
            this.obey_trns_evnts = true;
        }

        private void getTrnsPnlData()
        {
            this.updtTrnsTotals();
            this.populateTrnsBatch();
            this.updtTrnsNavLabels();
        }

        private void updtTrnsTotals()
        {
            Global.mnFrm.cmCde.navFuncts.FindNavigationIndices(
             int.Parse(this.dsplySizeTrnsComboBox.Text), this.totl_trns);
            if (this.trns_cur_indx >= Global.mnFrm.cmCde.navFuncts.totalGroups)
            {
                this.trns_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            if (this.trns_cur_indx < 0)
            {
                this.trns_cur_indx = 0;
            }
            Global.mnFrm.cmCde.navFuncts.currentNavigationIndex = this.trns_cur_indx;
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
                this.totalRecTrnsLabel.Text = Global.mnFrm.cmCde.navFuncts.totalRecordsLabel();
            }
            else
            {
                this.totalRecTrnsLabel.Text = "of Total";
            }
        }

        private void populateTrnsDet(long batchID)
        {
            this.obey_trns_evnts = false;
            if (this.addTrns == false && this.editTrns == false)
            {
                this.clearTrnsInfo();
                this.disableTrnsEdit();
            }
            this.trnsDetListView.Items.Clear();
            this.batchIDTextBox.Text = this.trnsBatchListView.SelectedItems[0].SubItems[2].Text;
            this.batchNameTextBox.Text = this.trnsBatchListView.SelectedItems[0].SubItems[1].Text;
            this.batchDescTextBox.Text = this.trnsBatchListView.SelectedItems[0].SubItems[3].Text;
            if (this.trnsBatchListView.SelectedItems[0].SubItems[4].Text == "1")
            {
                this.batchStatusLabel.Text = "Posted";
                this.batchStatusLabel.BackColor = Color.Green;
                this.addTrnsButton.Enabled = false;
                this.addTrnsMenuItem.Enabled = false;
                this.imprtTrnsTmpltButton.Enabled = false;

                this.editTrnsMenuItem.Enabled = false;

                this.deleteTrnsMenuItem.Enabled = false;

                this.addTrnsTmpltMenuItem.Enabled = false;
                this.addTrnsTmpltButton.Enabled = false;

                this.postTrnsButton.Enabled = false;
            }
            else
            {
                this.batchStatusLabel.Text = "Not Posted";
                this.batchStatusLabel.BackColor = Color.Red;

                this.addTrnsButton.Enabled = this.addTrscns;
                this.addTrnsMenuItem.Enabled = this.addTrscns;
                this.imprtTrnsTmpltButton.Enabled = this.addTrscns;

                this.editTrnsMenuItem.Enabled = this.editTrscns;

                this.deleteTrnsMenuItem.Enabled = this.delTrscns;

                this.addTrnsTmpltMenuItem.Enabled = this.addTrscnsFrmTmp;
                this.addTrnsTmpltButton.Enabled = this.addTrscnsFrmTmp;

                this.postTrnsButton.Enabled = this.postTrscns;
            }
            this.batchDateLabel.Text = this.trnsBatchListView.SelectedItems[0].SubItems[5].Text;
            this.batchSourceLabel.Text = this.trnsBatchListView.SelectedItems[0].SubItems[6].Text;
            this.vldtyLabel.Text = this.trnsBatchListView.SelectedItems[0].SubItems[7].Text.ToUpper();

            if (this.vldtyLabel.Text == "VALID")
            {
                this.vldtyLabel.BackColor = Color.Green;
            }
            else
            {
                this.vldtyLabel.BackColor = Color.Red;
            }

            this.autoPostLabel.Text = this.trnsBatchListView.SelectedItems[0].SubItems[8].Text;
            if (this.autoPostLabel.Text == "Pending Auto-Post")
            {
                this.autoPostLabel.BackColor = Color.Green;
            }
            else
            {
                this.autoPostLabel.BackColor = Color.Black;
            }
            //this.batchStatusLabel.Text = "Posted";
            double dbts = Global.get_Batch_DbtSum(long.Parse(this.batchIDTextBox.Text));
            double crdts = Global.get_Batch_CrdtSum(long.Parse(this.batchIDTextBox.Text));
            this.totalCrdtsLabel.Text = crdts.ToString("#,##0.00");
            this.totalDbtsLabel.Text = dbts.ToString("#,##0.00");
            this.totalDiffLabel.Text = Math.Abs(dbts - crdts).ToString("#,##0.00");
            if (this.totalDbtsLabel.Text != this.totalCrdtsLabel.Text)
            {
                this.totalCrdtsLabel.BackColor = Color.Red;
                this.totalDbtsLabel.BackColor = Color.Red;
                this.totalDiffLabel.BackColor = Color.Red;
            }
            else
            {
                this.totalCrdtsLabel.BackColor = Color.Green;
                this.totalDbtsLabel.BackColor = Color.Green;
                this.totalDiffLabel.BackColor = Color.Green;
            }
            this.loadTrnsDetPanel();
            this.obey_trns_evnts = true;
        }

        private void populateTrnsBatch()
        {
            this.obey_trns_evnts = false;
            DataSet dtst = Global.get_Basic_BatchDet(this.searchForTrnsTextBox.Text,
             this.searchInTrnsComboBox.Text, this.trns_cur_indx, int.Parse(this.dsplySizeTrnsComboBox.Text)
             , Global.mnFrm.cmCde.Org_id, this.shwMyBatchesCheckBox.Checked, this.showUnpostedCheckBox.Checked);
            this.clearTrnsInfo();
            this.disableTrnsEdit();
            this.trnsBatchListView.Items.Clear();
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.last_trns_num = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
                ListViewItem nwItem = new ListViewItem(new string[] {
    (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),
      dtst.Tables[0].Rows[i][0].ToString(),
    dtst.Tables[0].Rows[i][2].ToString(),
    dtst.Tables[0].Rows[i][3].ToString(),
    dtst.Tables[0].Rows[i][4].ToString(),
    dtst.Tables[0].Rows[i][5].ToString(),
    dtst.Tables[0].Rows[i][6].ToString(),
    dtst.Tables[0].Rows[i][7].ToString()});
                this.trnsBatchListView.Items.Add(nwItem);
            }
            this.correctTrnsNavLbls(dtst);
            if (this.trnsBatchListView.Items.Count > 0)
            {
                this.obey_trns_evnts = true;
                this.trnsBatchListView.Items[0].Selected = true;
            }
            else
            {
                this.trns_cur_indx = 0;
                this.totl_tdet = 0;
                this.last_tdet_num = 0;
                this.updtTdetTotals();
                this.updtTdetNavLabels();
            }
            this.obey_trns_evnts = true;
        }

        private void correctTrnsNavLbls(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.trns_cur_indx == 0 && totlRecs == 0)
            {
                this.is_last_trns = true;
                this.totl_trns = 0;
                this.last_trns_num = 0;
                this.trns_cur_indx = 0;
                this.updtTrnsTotals();
                this.updtTrnsNavLabels();
            }
            else if (this.totl_trns == Global.mnFrm.cmCde.Big_Val
          && totlRecs < int.Parse(this.dsplySizeTrnsComboBox.Text))
            {
                this.totl_trns = this.last_trns_num;
                if (totlRecs == 0)
                {
                    this.trns_cur_indx -= 1;
                    this.updtTrnsTotals();
                    this.populateTrnsBatch();
                }
                else
                {
                    this.updtTrnsTotals();
                }
            }
        }

        private void clearTrnsInfo()
        {
            if (this.backgroundWorker1.IsBusy == true)
            {
                return;
            }
            this.obey_trns_evnts = false;
            this.saveTrnsBatchButton.Enabled = false;
            this.addTrnsBatchButton.Enabled = this.addBatches;
            this.editTrnsBatchButton.Enabled = this.editBatches;
            this.voidBatchButton.Enabled = this.delBatches;
            this.batchIDTextBox.Text = "-1";
            this.batchNameTextBox.Text = "";
            this.batchDescTextBox.Text = "";
            this.batchDateLabel.Text = DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss");
            this.batchStatusLabel.Text = "Not Posted";
            this.batchStatusLabel.BackColor = Color.Red;

            this.autoPostLabel.Text = "";
            this.autoPostLabel.BackColor = Color.Black;

            this.addTrnsButton.Enabled = this.addTrscns;
            this.addTrnsMenuItem.Enabled = this.addTrscns;
            this.imprtTrnsTmpltButton.Enabled = this.addTrscns;
            this.editTrnsMenuItem.Enabled = this.editTrscns;
            this.deleteTrnsMenuItem.Enabled = this.delTrscns;
            this.addTrnsTmpltMenuItem.Enabled = this.addTrscnsFrmTmp;
            this.addTrnsTmpltButton.Enabled = this.addTrscnsFrmTmp;
            this.postTrnsButton.Enabled = this.postTrscns;
            this.cancelRunButton.Enabled = false;


            this.batchSourceLabel.Text = "Manual";
            this.autoPostLabel.Text = "Not Monitored";
            this.totalCrdtsLabel.Text = "0.00";
            this.totalDbtsLabel.Text = "0.00";
            this.totalDbtsLabel.BackColor = Color.Green;
            this.totalCrdtsLabel.BackColor = Color.Green;
            this.totalDiffLabel.BackColor = Color.Green;
            this.progressBar1.Value = 0;
            this.progressLabel.Text = "0%";
            this.trnsDetListView.Items.Clear();
            this.tdet_cur_indx = 0;
            this.totl_tdet = 0;
            this.last_tdet_num = 0;
            this.updtTdetTotals();
            this.updtTdetNavLabels();

            this.obey_trns_evnts = true;
        }

        private void prpareForTrnsEdit()
        {
            this.saveTrnsBatchButton.Enabled = true;
            this.batchNameTextBox.ReadOnly = false;
            this.batchNameTextBox.BackColor = Color.FromArgb(255, 255, 128);
            this.batchDescTextBox.ReadOnly = false;
            this.batchDescTextBox.BackColor = Color.White;
        }

        private void disableTrnsEdit()
        {
            this.addTrns = false;
            this.editTrns = false;
            this.batchNameTextBox.ReadOnly = true;
            this.batchNameTextBox.BackColor = Color.WhiteSmoke;
            this.batchDescTextBox.ReadOnly = true;
            this.batchDescTextBox.BackColor = Color.WhiteSmoke;
        }

        private bool shdObeyTrnsEvts()
        {
            return this.obey_trns_evnts;
        }

        private void TrnsPnlNavButtons(object sender, System.EventArgs e)
        {
            if (this.backgroundWorker1.IsBusy == true)
            {
                return;
            }
            System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecTrnsLabel.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.is_last_trns = false;
                this.trns_cur_indx = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.is_last_trns = false;
                this.trns_cur_indx -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.is_last_trns = false;
                this.trns_cur_indx += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.is_last_trns = true;
                this.totl_trns = Global.get_Total_Batches(this.searchForTrnsTextBox.Text,
                 this.searchInTrnsComboBox.Text, Global.mnFrm.cmCde.Org_id,
                 this.shwMyBatchesCheckBox.Checked, this.showUnpostedCheckBox.Checked);
                this.updtTrnsTotals();
                this.trns_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            this.getTrnsPnlData();
        }

        private void addTrnsBatchButton_Click(object sender, EventArgs e)
        {
            if (this.backgroundWorker1.IsBusy == true)
            {
                return;
            }
            this.resetTrnsButton.PerformClick();
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[14]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            this.resetTrnsButton.PerformClick();
            this.clearTrnsInfo();
            this.addTrns = true;
            this.editTrns = false;
            this.prpareForTrnsEdit();
            this.addTrnsBatchButton.Enabled = false;
            this.editTrnsBatchButton.Enabled = false;
            this.voidBatchButton.Enabled = false;
            string initl = Global.mnFrm.cmCde.getUsername(Global.myBscActn.user_id).ToUpper();
            if (initl.Length > 4)
            {
                initl = initl.Substring(0, 4);
            }
            string dte = DateTime.Parse(Global.mnFrm.cmCde.getFrmtdDB_Date_time()).ToString("yyMMdd");
            this.batchNameTextBox.Text = initl + "-" + dte
              + "-" + Global.mnFrm.cmCde.getRandomInt(100, 1000)
                      + "-" + (Global.mnFrm.cmCde.getRecCount("accb.accb_trnsctn_batches", "batch_name",
                      "batch_id", initl + "-" + dte + "-%") + 1).ToString().PadLeft(3, '0');

            //this.batchNameTextBox.Text = initl
            //+ "-" + DateTime.Parse(Global.mnFrm.cmCde.getFrmtdDB_Date_time()).ToString("yyMMdd-HHmmss")
            //           + "-" + Global.mnFrm.cmCde.getRandomInt(10, 100);

            this.saveTrnsBatchButton_Click(this.saveTrnsBatchButton, e);
            this.shwMyBatchesCheckBox.Checked = true;
            //this.editTrnsBatchButton_Click(this.editTrnsBatchButton, e);
        }

        private void trnsBatchListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.shdObeyTrnsEvts() == false || this.trnsBatchListView.SelectedItems.Count > 1)
            {
                return;
            }
            if (this.backgroundWorker1.IsBusy == true)
            {
                return;
            }

            if (this.trnsBatchListView.SelectedItems.Count > 0)
            {
                this.populateTrnsDet(long.Parse(this.trnsBatchListView.SelectedItems[0].SubItems[2].Text));
            }
            else
            {
                this.clearTrnsInfo();
                this.disableTrnsEdit();
            }
        }

        private void goTrnsButton_Click(object sender, EventArgs e)
        {
            if (this.backgroundWorker1.IsBusy == true)
            {
                return;
            }
            this.loadAccntTrnsPanel();
        }

        private void editTrnsBatchButton_Click(object sender, EventArgs e)
        {
            if (this.backgroundWorker1.IsBusy == true)
            {
                return;
            }
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[15]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.batchIDTextBox.Text == "" || this.batchIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("No record to Edit!", 0);
                return;
            }
            if (this.batchStatusLabel.Text == "Posted")
            {
                Global.mnFrm.cmCde.showMsg("Cannot edit an already Posted Batch of Transactions!", 0);
                return;
            }
            if (this.batchSourceLabel.Text != "Manual")
            {
                Global.mnFrm.cmCde.showMsg("Cannot edit Transaction Batches \r\nthat came from other Modules!", 0);
                return;
            }
            this.addTrns = false;
            this.editTrns = true;
            this.prpareForTrnsEdit();
            this.addTrnsBatchButton.Enabled = false;
            this.editTrnsBatchButton.Enabled = false;
            this.voidBatchButton.Enabled = false;
        }

        private void voidBatchButton_Click(object sender, EventArgs e)
        {
            if (this.backgroundWorker1.IsBusy == true)
            {
                return;
            }
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[16]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.trnsBatchListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the Batch to Void/Delete!", 0);
                return;
            }
            DataSet dtst = Global.get_Batch_Trns_NoStatus(long.Parse(this.batchIDTextBox.Text));
            long ttltrns = dtst.Tables[0].Rows.Count;
            //ttltrns > 0 &&
            if ((this.batchSourceLabel.Text != "Manual" && this.batchSourceLabel.Text != "Manual Batch Reversal")
              && (Global.get_ScmIntrfcTrnsCnt(long.Parse(this.batchIDTextBox.Text)) > 0
              || Global.get_PayIntrfcTrnsCnt(long.Parse(this.batchIDTextBox.Text)) > 0))
            {
                if (Global.mnFrm.cmCde.showMsg("Force Deleting/Voiding Batches \r\nthat came from other Modules " +
                "and have Transactions in them can have serious consequences of your Accounting System! \r\nAre you sure you want to Proceed??", 1) == DialogResult.No)
                {
                    return;
                }
            }
            if ((this.batchSourceLabel.Text != "Manual" && this.batchSourceLabel.Text != "Manual Batch Reversal")
              && this.batchStatusLabel.Text == "Posted")
            {
                if (Global.mnFrm.cmCde.showMsg("Force Deleting/Voiding Batches \r\nthat came from other Modules " +
               "and have Transactions in them can have serious consequences of your Accounting System! \r\nAre you sure you want to Proceed??", 1) == DialogResult.No)
                {
                    return;
                }
                /*Global.mnFrm.cmCde.showMsg("Cannot Void Batches \r\nthat came from other Modules " +
                "and have been Posted!", 0);
                return;*/
            }
            if (this.batchStatusLabel.Text == "Posted" && this.batchSourceLabel.Text == "Manual Batch Reversal")
            {
                Global.mnFrm.cmCde.showMsg("Cannot Reverse Posted Reversal Batches Please Key the Original Transactions in a New Batch!", 0);
                return;
            }
            /*&&
              this.batchSourceLabel.Text != "Period Close Process"*/
            if (this.batchStatusLabel.Text == "Not Posted")
            {
                if (Global.mnFrm.cmCde.showMsg("This batch is NOT POSTED so it will be DELETED.\r\nAre you sure you want to DELETE the selected" +
                 "\r\nBatch and all its Transactions? \r\nThis Action cannot be undone!", 1)
                 == DialogResult.No)
                {
                    //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                    return;
                }
                else
                {
                    bool dltd = true;
                    DataSet dtst1 = Global.get_Batch_Attachments(long.Parse(this.trnsBatchListView.SelectedItems[0].SubItems[2].Text));

                    for (int i = 0; i < dtst1.Tables[0].Rows.Count; i++)
                    {
                        if (Global.mnFrm.cmCde.deleteAFile(
                          Global.mnFrm.cmCde.getAcctngImgsDrctry() +
                  @"\" + dtst1.Tables[0].Rows[i][3].ToString()) == true)
                        {
                            Global.deleteAttchmnt(long.Parse(dtst1.Tables[0].Rows[i][0].ToString()),
                              dtst1.Tables[0].Rows[i][2].ToString(), "accb.accb_batch_trns_attchmnts");
                        }
                        else
                        {
                            Global.mnFrm.cmCde.showMsg("Could not delete File: " +
                            Global.mnFrm.cmCde.getAcctngImgsDrctry() +
                     @"\" + dtst1.Tables[0].Rows[i][3].ToString(), 0);
                            dltd = false;
                            break;
                        }
                    }
                    if (dltd == true)
                    {
                        Global.deleteBatchTrns(long.Parse(this.trnsBatchListView.SelectedItems[0].SubItems[2].Text));
                        Global.deleteBatch(long.Parse(this.trnsBatchListView.SelectedItems[0].SubItems[2].Text),
                          this.trnsBatchListView.SelectedItems[0].SubItems[1].Text);
                    }
                }
            }
            else
            {
                bool expiredPrd = true;
                DateTime trnsDte = DateTime.ParseExact(this.batchDateLabel.Text, "dd-MMM-yyyy HH:mm:ss",
            System.Globalization.CultureInfo.InvariantCulture);
                long prdHdrID = Global.mnFrm.cmCde.getPrdHdrID(Global.mnFrm.cmCde.Org_id);
                if (this.trnsDetListView.Items.Count > 0)
                {
                    trnsDte = DateTime.ParseExact(this.trnsDetListView.Items[0].SubItems[7].Text, "dd-MMM-yyyy HH:mm:ss",
            System.Globalization.CultureInfo.InvariantCulture);
                    if (Global.mnFrm.cmCde.getTrnsDteOpenPrdLnID(prdHdrID, trnsDte.ToString("yyyy-MM-dd HH:mm:ss")) < 0)
                    {
                        trnsDte = DateTime.ParseExact(Global.mnFrm.cmCde.getLtstOpenPrdAfterDate(trnsDte.ToString("yyyy-MM-dd HH:mm:ss")), "yyyy-MM-dd HH:mm:ss",
                    System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        trnsDte = DateTime.ParseExact(this.trnsDetListView.Items[0].SubItems[7].Text, "dd-MMM-yyyy HH:mm:ss",
                   System.Globalization.CultureInfo.InvariantCulture);
                        expiredPrd = false;
                    }
                    if (!Global.mnFrm.cmCde.isTransPrmttd(
                      Global.mnFrm.cmCde.get_DfltCashAcnt(Global.mnFrm.cmCde.Org_id),
                      trnsDte.ToString("dd-MMM-yyyy HH:mm:ss"), 200))
                    {
                        return;
                    }
                }
                else
                {
                    trnsDte = DateTime.ParseExact(this.batchDateLabel.Text, "dd-MMM-yyyy HH:mm:ss",
           System.Globalization.CultureInfo.InvariantCulture);
                    if (Global.mnFrm.cmCde.getTrnsDteOpenPrdLnID(prdHdrID, trnsDte.ToString("yyyy-MM-dd HH:mm:ss")) < 0)
                    {
                        trnsDte = DateTime.ParseExact(Global.mnFrm.cmCde.getLtstOpenPrdAfterDate(trnsDte.ToString("yyyy-MM-dd HH:mm:ss")), "yyyy-MM-dd HH:mm:ss",
                    System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        trnsDte = DateTime.ParseExact(this.batchDateLabel.Text, "dd-MMM-yyyy HH:mm:ss",
                   System.Globalization.CultureInfo.InvariantCulture);
                        expiredPrd = false;
                    }
                    if (!Global.mnFrm.cmCde.isTransPrmttd(
                      Global.mnFrm.cmCde.get_DfltCashAcnt(Global.mnFrm.cmCde.Org_id),
                      trnsDte.ToString("dd-MMM-yyyy HH:mm:ss"), 200))
                    {
                        return;
                    }
                }
                if (Global.mnFrm.cmCde.showMsg("This batch has been POSTED already so it will be VOIDED.\r\nAre you sure you want to VOID the selected" +
                 "\r\nBatch and all its Transactions? \r\nThis Action cannot be undone!", 1)
                 == DialogResult.No)
                {
                    //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                    return;
                }
                else
                {
                    string dateStr = DateTime.ParseExact(
               Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
               System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
                    //Begin Process of voiding
                    long beenPstdB4 = Global.getSimlrPstdBatchID(
                      this.batchNameTextBox.Text, Global.mnFrm.cmCde.Org_id);
                    if (beenPstdB4 > 0)
                    {
                        {
                            Global.mnFrm.cmCde.showMsg("This batch has been reversed before\r\n Operation Cancelled!", 4);
                            return;
                        }
                    }
                    long nwbatchid = Global.getBatchID(this.batchNameTextBox.Text +
                     " (Manual Batch Reversal@" + dateStr.Substring(0, 11) + ")",
                     Global.mnFrm.cmCde.Org_id);
                    if (nwbatchid <= 0)
                    {
                        Global.createBatch(Global.mnFrm.cmCde.Org_id,
                         this.batchNameTextBox.Text + " (Manual Batch Reversal@" + dateStr.Substring(0, 11) + ")",
                         this.batchDescTextBox.Text + " (Manual Batch Reversal@" + dateStr.Substring(0, 11) + ")",
                         "Manual Batch Reversal",
                         "VALID",
                         long.Parse(this.batchIDTextBox.Text), "0");
                        Global.updateBatchVldtyStatus(long.Parse(this.batchIDTextBox.Text), "VOID");
                        nwbatchid = Global.getBatchID(this.batchNameTextBox.Text +
                        " (Manual Batch Reversal@" + dateStr.Substring(0, 11) + ")",
                        Global.mnFrm.cmCde.Org_id);
                    }
                    //Get All Posted/Unposted Transactions in current batch
                    dtst = Global.get_Batch_Trns_NoStatus(long.Parse(this.batchIDTextBox.Text));
                    ttltrns = dtst.Tables[0].Rows.Count;
                    string dteToUse = trnsDte.ToString("dd-MMM-yyyy HH:mm:ss");
                    for (int i = 0; i < ttltrns; i++)
                    {
                        if (expiredPrd == false)
                        {
                            dteToUse = dtst.Tables[0].Rows[i][6].ToString();
                        }
                        Global.createTransaction(int.Parse(dtst.Tables[0].Rows[i][9].ToString()),
                        dtst.Tables[0].Rows[i][3].ToString() + " (Reversal)", -1 * double.Parse(dtst.Tables[0].Rows[i][4].ToString()),
                        dteToUse, int.Parse(dtst.Tables[0].Rows[i][7].ToString()),
                        nwbatchid, -1 * double.Parse(dtst.Tables[0].Rows[i][5].ToString()),
                        -1 * double.Parse(dtst.Tables[0].Rows[i][10].ToString()),
                  -1 * double.Parse(dtst.Tables[0].Rows[i][12].ToString()),
                  int.Parse(dtst.Tables[0].Rows[i][13].ToString()),
                  -1 * double.Parse(dtst.Tables[0].Rows[i][14].ToString()),
                  int.Parse(dtst.Tables[0].Rows[i][15].ToString()),
                  double.Parse(dtst.Tables[0].Rows[i][16].ToString()),
                  double.Parse(dtst.Tables[0].Rows[i][17].ToString()),
                  dtst.Tables[0].Rows[i][18].ToString(), "");
                    }
                    Global.updateBatchAvlblty(nwbatchid, "1");
                }
            }
            this.loadAccntTrnsPanel();
        }

        private void saveTrnsBatchButton_Click(object sender, EventArgs e)
        {
            if (this.addTrns == true)
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
            if (this.batchNameTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please enter a Batch Name!", 0);
                return;
            }
            long oldBatchID = Global.mnFrm.cmCde.getTrnsBatchID(this.batchNameTextBox.Text,
              Global.mnFrm.cmCde.Org_id);
            if (oldBatchID > 0
             && this.addTrns == true)
            {
                Global.mnFrm.cmCde.showMsg("Batch Name is already in use in this Organization!", 0);
                return;
            }
            if (oldBatchID > 0
             && this.editTrns == true
             && oldBatchID.ToString() != this.batchIDTextBox.Text)
            {
                Global.mnFrm.cmCde.showMsg("New Batch Name is already in use in this Organization!", 0);
                return;
            }
            if (this.addTrns == true)
            {
                Global.createBatch(Global.mnFrm.cmCde.Org_id,
                 this.batchNameTextBox.Text, this.batchDescTextBox.Text, this.batchSourceLabel.Text,
                 "VALID", -1, "0");
                this.saveTrnsBatchButton.Enabled = false;
                this.addTrns = false;
                this.editTrns = false;
                this.editTrnsBatchButton.Enabled = this.addBatches;
                this.addTrnsBatchButton.Enabled = this.editBatches;
                this.voidBatchButton.Enabled = this.delBatches;
                System.Windows.Forms.Application.DoEvents();
                this.batchIDTextBox.Text = Global.mnFrm.cmCde.getGnrlRecID(
                  "accb.accb_trnsctn_batches",
                  "batch_name", "batch_id",
                  this.batchNameTextBox.Text, Global.mnFrm.cmCde.Org_id).ToString();
                bool prv = this.obey_trns_evnts;
                this.obey_trns_evnts = false;
                ListViewItem nwItem = new ListViewItem(new string[] {
    "New",
    this.batchNameTextBox.Text,
      this.batchIDTextBox.Text,
    this.batchDescTextBox.Text,
    this.batchStatusLabel.Text,
    this.batchDateLabel.Text,
    this.batchSourceLabel.Text,
    this.vldtyLabel.Text,
        this.autoPostLabel.Text});
                this.trnsBatchListView.Items.Insert(0, nwItem);
                for (int i = 0; i < this.trnsBatchListView.SelectedItems.Count; i++)
                {
                    this.trnsBatchListView.SelectedItems[i].Font = new Font("Tahoma", 8.25f, FontStyle.Regular);
                    this.trnsBatchListView.SelectedItems[i].Selected = false;
                }
                this.trnsBatchListView.Items[0].Selected = true;
                this.trnsBatchListView.Items[0].Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
                this.obey_trns_evnts = prv;
                System.Windows.Forms.Application.DoEvents();

                this.saveTrnsBatchButton.Enabled = true;
                this.editTrns = true;
                this.prpareForTrnsEdit();
                Global.mnFrm.cmCde.showMsg("Record Saved!", 3);
                //this.loadAccntTrnsPanel();
            }
            else if (this.editTrns == true)
            {
                Global.updateBatch(long.Parse(this.batchIDTextBox.Text),
                 this.batchNameTextBox.Text, this.batchDescTextBox.Text);
                this.saveTrnsBatchButton.Enabled = false;
                this.editTrns = false;
                this.editTrnsBatchButton.Enabled = this.addBatches;
                this.addTrnsBatchButton.Enabled = this.editBatches;
                this.voidBatchButton.Enabled = this.delBatches;
                this.saveTrnsBatchButton.Enabled = true;
                this.editTrns = true;
                if (this.trnsBatchListView.SelectedItems.Count > 0)
                {
                    if (this.trnsBatchListView.SelectedItems[0].SubItems[2].Text == this.batchIDTextBox.Text)
                    {
                        this.trnsBatchListView.SelectedItems[0].SubItems[1].Text = this.batchNameTextBox.Text;
                        this.trnsBatchListView.SelectedItems[0].SubItems[3].Text = this.batchDescTextBox.Text;
                    }
                }
                Global.mnFrm.cmCde.showMsg("Record Saved!", 3);
                //this.loadAccntTrnsPanel();
            }
        }

        private void addTrnsButton_Click(object sender, EventArgs e)
        {
            this.addDirTrnsButton_Click(this.addDirTrnsButton, e);
        }

        private void postTrnsButton_Click(object sender, EventArgs e)
        {
            if (this.backgroundWorker1.IsBusy == true)
            {
                return;
            }
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[21]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;

            if (this.batchIDTextBox.Text == "" ||
          this.batchIDTextBox.Text == "-1" ||
          this.trnsBatchListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a saved Batch First!", 0);
                return;
            }
            if (this.batchStatusLabel.Text == "Posted")
            {
                Global.mnFrm.cmCde.showMsg("Cannot Post an already Posted Batch of Transactions!", 0);
                return;
            }
            if (this.totalCrdtsLabel.Text != this.totalDbtsLabel.Text)
            {
                Global.mnFrm.cmCde.showMsg("Cannot Post an Unbalanced Batch of Transactions!", 0);
                return;
            }
            DataSet dtst = Global.get_Batch_Trns_NoStatus(long.Parse(this.batchIDTextBox.Text));
            long ttltrns = dtst.Tables[0].Rows.Count;
            if (ttltrns <= 0 && this.batchSourceLabel.Text != "Period Close Process")
            {
                Global.mnFrm.cmCde.showMsg("Only Period Close Process Batches can be posted \r\nwhen the batch has no transactions!", 0);
                return;
            }
            int ret_accnt = Global.get_Rtnd_Erngs_Accnt(Global.mnFrm.cmCde.Org_id);
            int net_accnt = Global.get_Net_Income_Accnt(Global.mnFrm.cmCde.Org_id);
            if (ret_accnt == -1)
            {
                Global.mnFrm.cmCde.showMsg("Until a Retained Earnings Account is defined\r\n no Transaction can be posted into the Accounting!", 0);
                return;
            }
            if (net_accnt == -1)
            {
                Global.mnFrm.cmCde.showMsg("Until a Net Income Account is defined\r\n no Transaction can be posted into the Accounting!", 0);
                return;
            }

            DataSet dteDtSt = Global.get_Batch_dateSums(long.Parse(this.batchIDTextBox.Text));
            if (dteDtSt.Tables[0].Rows.Count > 0)
            {
                string msg1 = @"Your transactions will cause your Balance Sheet to become Unbalanced on some Days!
Please make sure each day has equal debits and credits.
Check the ff Days:" + "\r\n";
                for (int i = 0; i < dteDtSt.Tables[0].Rows.Count; i++)
                {
                    msg1 = msg1 + dteDtSt.Tables[0].Rows[i][0].ToString() + "\t DR=" + dteDtSt.Tables[0].Rows[i][1].ToString() + "\t CR=" + dteDtSt.Tables[0].Rows[i][2].ToString() + "\r\n";
                }
                Global.mnFrm.cmCde.showMsg(msg1, 4);
                return;
            }

            double aesum = Global.get_COA_AESum(Global.mnFrm.cmCde.Org_id);
            double crlsum = Global.get_COA_CRLSum(Global.mnFrm.cmCde.Org_id);
            if (aesum
             != crlsum)
            {
                Global.mnFrm.cmCde.showMsg("Cannot Post this Batch Since Current GL is not Balanced!\r\nPlease correct the Imbalance First!", 0);
                return;
            }

            if (Global.mnFrm.cmCde.showMsg("REMEMBER! Transactions once posted CANNOT be edited!\r\nAre you sure you want to POST this Batch?", 1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }

            this.addTrnsButton.Enabled = false;
            this.addTrnsTmpltButton.Enabled = false;
            this.imprtTrnsTmpltButton.Enabled = false;
            this.exprtTrnsTmpltButton.Enabled = false;
            this.postTrnsButton.Enabled = false;
            this.cancelRunButton.Enabled = true;

            System.Windows.Forms.Application.DoEvents();

            string dateStr = DateTime.ParseExact(
         Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
            Object[] args = {this.batchIDTextBox.Text,
            Global.mnFrm.cmCde.Org_id.ToString(), dateStr, net_accnt.ToString()};
            this.backgroundWorker1.RunWorkerAsync(args);
        }

        private void reloadAcntChrtBals(int netaccntid)
        {
            string dateStr = DateTime.ParseExact(
         Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
            DataSet dtst = Global.get_All_Chrt_Det(Global.mnFrm.cmCde.Org_id);
            //DataSet dtst = Global.get_Batch_Accnts(btchid);
            //if (dateStr.Length > 10)
            //{
            //  dateStr = dateStr.Substring(0, 10);
            //}
            for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
            {
                string[] rslt = Global.getAccntLstDailyBalsInfo(
                  int.Parse(dtst.Tables[0].Rows[a][0].ToString()), dateStr);
                double lstNetBals = double.Parse(rslt[2]);
                double lstDbtBals = double.Parse(rslt[0]);
                double lstCrdtBals = double.Parse(rslt[1]);

                //Global.mnFrm.cmCde.showMsg("Testing!" + rslt[2] + "\r\n" + rslt[3] + "\r\n" + dateStr, 0);
                Global.updtAcntChrtBals(int.Parse(dtst.Tables[0].Rows[a][0].ToString()),
                  lstDbtBals, lstCrdtBals, lstNetBals, rslt[3]);

                //get control accnt id
                int cntrlAcntID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_chart_of_accnts", "accnt_id", "control_account_id", int.Parse(dtst.Tables[0].Rows[a][0].ToString())));
                if (cntrlAcntID > 0)
                {
                    rslt = Global.getAccntLstDailyBalsInfo(
                 cntrlAcntID, dateStr);
                    lstNetBals = double.Parse(rslt[2]);
                    lstDbtBals = double.Parse(rslt[0]);
                    lstCrdtBals = double.Parse(rslt[1]);

                    //Global.mnFrm.cmCde.showMsg("Testing!" + rslt[2] + "\r\n" + rslt[3] + "\r\n" + dateStr, 0);
                    Global.updtAcntChrtBals(cntrlAcntID,
                     lstDbtBals, lstCrdtBals, lstNetBals, rslt[3]);
                }
            }
            if (netaccntid > 0)
            {
                string[] rslt = Global.getAccntLstDailyBalsInfo(
                  netaccntid, dateStr);
                double lstNetBals = double.Parse(rslt[2]);
                double lstDbtBals = double.Parse(rslt[0]);
                double lstCrdtBals = double.Parse(rslt[1]);

                //Global.mnFrm.cmCde.showMsg("Testing!" + rslt[2] + "\r\n" + rslt[3] + "\r\n" + dateStr, 0);
                Global.updtAcntChrtBals(netaccntid,
                  lstDbtBals, lstCrdtBals, lstNetBals, rslt[3]);
            }
        }

        private void reloadOneAcntChrtBals(int accntID, int netaccntid)
        {
            string dateStr = DateTime.ParseExact(
         Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
            //DataSet dtst = Global.get_All_Chrt_Det(Global.mnFrm.cmCde.Org_id);
            //DataSet dtst = Global.get_Batch_Accnts(btchid);
            //if (dateStr.Length > 10)
            //{
            //  dateStr = dateStr.Substring(0, 10);
            //}
            //for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
            //{
            string[] rslt = Global.getAccntLstDailyBalsInfo(accntID, dateStr);
            double lstNetBals = double.Parse(rslt[2]);
            double lstDbtBals = double.Parse(rslt[0]);
            double lstCrdtBals = double.Parse(rslt[1]);

            //Global.mnFrm.cmCde.showMsg("Testing!" + rslt[2] + "\r\n" + rslt[3] + "\r\n" + dateStr, 0);
            Global.updtAcntChrtBals(accntID,
              lstDbtBals, lstCrdtBals, lstNetBals, rslt[3]);

            //get control accnt id
            int cntrlAcntID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_chart_of_accnts", "accnt_id", "control_account_id", accntID));
            if (cntrlAcntID > 0)
            {
                rslt = Global.getAccntLstDailyBalsInfo(
             cntrlAcntID, dateStr);
                lstNetBals = double.Parse(rslt[2]);
                lstDbtBals = double.Parse(rslt[0]);
                lstCrdtBals = double.Parse(rslt[1]);

                //Global.mnFrm.cmCde.showMsg("Testing!" + rslt[2] + "\r\n" + rslt[3] + "\r\n" + dateStr, 0);
                Global.updtAcntChrtBals(cntrlAcntID,
                 lstDbtBals, lstCrdtBals, lstNetBals, rslt[3]);
            }
            //}
            if (netaccntid > 0)
            {
                rslt = Global.getAccntLstDailyBalsInfo(
                 netaccntid, dateStr);
                lstNetBals = double.Parse(rslt[2]);
                lstDbtBals = double.Parse(rslt[0]);
                lstCrdtBals = double.Parse(rslt[1]);

                //Global.mnFrm.cmCde.showMsg("Testing!" + rslt[2] + "\r\n" + rslt[3] + "\r\n" + dateStr, 0);
                Global.updtAcntChrtBals(netaccntid,
                  lstDbtBals, lstCrdtBals, lstNetBals, rslt[3]);
            }
        }

        private void reloadAcntChrtBals(long btchid, int netaccntid)
        {
            string dateStr = DateTime.ParseExact(
         Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
            //DataSet dtst = Global.get_All_Chrt_Det(Global.mnFrm.cmCde.Org_id);
            DataSet dtst = Global.get_Batch_Accnts(btchid);
            //if (dateStr.Length > 10)
            //{
            //  dateStr = dateStr.Substring(0, 10);
            //}
            for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
            {
                string[] rslt = Global.getAccntLstDailyBalsInfo(
                  int.Parse(dtst.Tables[0].Rows[a][0].ToString()), dateStr);
                double lstNetBals = double.Parse(rslt[2]);
                double lstDbtBals = double.Parse(rslt[0]);
                double lstCrdtBals = double.Parse(rslt[1]);

                //Global.mnFrm.cmCde.showMsg("Testing!" + rslt[2] + "\r\n" + rslt[3] + "\r\n" + dateStr, 0);
                Global.updtAcntChrtBals(int.Parse(dtst.Tables[0].Rows[a][0].ToString()),
                  lstDbtBals, lstCrdtBals, lstNetBals, rslt[3]);

                //get control accnt id
                int cntrlAcntID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_chart_of_accnts", "accnt_id", "control_account_id", int.Parse(dtst.Tables[0].Rows[a][0].ToString())));
                if (cntrlAcntID > 0)
                {
                    rslt = Global.getAccntLstDailyBalsInfo(
                 cntrlAcntID, dateStr);
                    lstNetBals = double.Parse(rslt[2]);
                    lstDbtBals = double.Parse(rslt[0]);
                    lstCrdtBals = double.Parse(rslt[1]);

                    //Global.mnFrm.cmCde.showMsg("Testing!" + rslt[2] + "\r\n" + rslt[3] + "\r\n" + dateStr, 0);
                    Global.updtAcntChrtBals(cntrlAcntID,
                     lstDbtBals, lstCrdtBals, lstNetBals, rslt[3]);
                }
            }
            if (netaccntid > 0)
            {
                string[] rslt = Global.getAccntLstDailyBalsInfo(
                  netaccntid, dateStr);
                double lstNetBals = double.Parse(rslt[2]);
                double lstDbtBals = double.Parse(rslt[0]);
                double lstCrdtBals = double.Parse(rslt[1]);

                //Global.mnFrm.cmCde.showMsg("Testing!" + rslt[2] + "\r\n" + rslt[3] + "\r\n" + dateStr, 0);
                Global.updtAcntChrtBals(netaccntid,
                  lstDbtBals, lstCrdtBals, lstNetBals, rslt[3]);
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
                if (this.progressBar1.Value == 100)
                {
                    if (this.batchSourceLabel.Text == "Period Close Process")
                    {
                        Global.updatePrdCloseStatus(long.Parse(this.batchIDTextBox.Text));
                    }
                    Global.mnFrm.cmCde.showMsg("Batch of Transactions POSTED SUCCESSFULLY!", 3);
                }
                else
                {
                    Global.mnFrm.cmCde.showMsg("Posting of Batch of Transactions did not Complete!", 0);
                }
            }
            this.addTrnsButton.Enabled = this.addTrscns;
            this.addTrnsTmpltButton.Enabled = this.addTrscnsFrmTmp;
            this.imprtTrnsTmpltButton.Enabled = this.addTrscns;
            this.exprtTrnsTmpltButton.Enabled = true;
            this.postTrnsButton.Enabled = this.postTrscns;
            this.cancelRunButton.Enabled = false;
            this.loadAccntTrnsPanel();
        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;
            this.progressLabel.Text = e.ProgressPercentage.ToString() + "%";
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                BackgroundWorker worker = sender as BackgroundWorker;
                Object[] myargs = (Object[])e.Argument;

                string dateStr = (string)myargs[2];
                int net_accnt = int.Parse((string)myargs[3]);
                string log_tbl = "accb.accb_post_trns_msgs";
                long msg_id = Global.mnFrm.cmCde.getLogMsgID(log_tbl,
                  "Posting Batch of Transactions", long.Parse((string)myargs[0]));
                if (msg_id <= 0)
                {
                    Global.mnFrm.cmCde.createLogMsg(dateStr + " .... Posting Batch of Transactions is about to Start...",
               log_tbl, "Posting Batch of Transactions", long.Parse((string)myargs[0]), dateStr);
                }
                msg_id = Global.mnFrm.cmCde.getLogMsgID(log_tbl, "Posting Batch of Transactions",
                  long.Parse((string)myargs[0]));

                DataSet dtst = Global.get_Batch_Trns(long.Parse((string)myargs[0]));
                long ttltrns = dtst.Tables[0].Rows.Count;
                string btchSrc = Global.mnFrm.cmCde.getGnrlRecNm(
                  "accb.accb_trnsctn_batches", "batch_id", "batch_source", long.Parse((string)myargs[0]));
                //Check if no other accounting process is running
                bool isAnyRnng = true;
                int witcntr = 0;
                do
                {
                    witcntr++;
                    isAnyRnng = Global.isThereANActvActnPrcss("1,2,3,4,5,6", "10 second");
                    if (worker.CancellationPending == true)
                    {
                        e.Cancel = true;
                        this.addTrnsButton.Enabled = this.addTrscns;
                        this.addTrnsTmpltButton.Enabled = this.addTrscnsFrmTmp;
                        this.imprtTrnsTmpltButton.Enabled = this.addTrscns;
                        this.exprtTrnsTmpltButton.Enabled = true;
                        this.postTrnsButton.Enabled = this.postTrscns;
                        this.cancelRunButton.Enabled = false;
                        this.loadAccntTrnsPanel();
                        return;
                    }
                    else
                    {
                        worker.ReportProgress(Convert.ToInt32((witcntr) * (10.00 / (witcntr + 2))) + 0);
                    }
                }
                while (isAnyRnng == true);

                //Validating Entries
                if (btchSrc != "Period Close Process")
                {
                    for (int i = 0; i < ttltrns; i++)
                    {
                        Global.updtActnPrcss(5);
                        if (worker.CancellationPending == true)
                        {
                            e.Cancel = true;
                            this.addTrnsButton.Enabled = this.addTrscns;
                            this.addTrnsTmpltButton.Enabled = this.addTrscnsFrmTmp;
                            this.imprtTrnsTmpltButton.Enabled = this.addTrscns;
                            this.exprtTrnsTmpltButton.Enabled = true;
                            this.postTrnsButton.Enabled = this.postTrscns;
                            this.cancelRunButton.Enabled = false;
                            this.loadAccntTrnsPanel();
                            return;
                        }
                        else
                        {
                            System.Windows.Forms.Application.DoEvents();
                            int accntid = int.Parse(dtst.Tables[0].Rows[i][9].ToString());
                            double netAmnt = double.Parse(dtst.Tables[0].Rows[i][10].ToString());
                            string lnDte = dtst.Tables[0].Rows[i][6].ToString();

                            if (!Global.mnFrm.cmCde.isTransPrmttd(accntid, lnDte, netAmnt))
                            {
                                e.Cancel = true;
                                this.addTrnsButton.Enabled = this.addTrscns;
                                this.addTrnsTmpltButton.Enabled = this.addTrscnsFrmTmp;
                                this.imprtTrnsTmpltButton.Enabled = this.addTrscns;
                                this.exprtTrnsTmpltButton.Enabled = true;
                                this.postTrnsButton.Enabled = this.postTrscns;
                                this.cancelRunButton.Enabled = false;
                                this.loadAccntTrnsPanel();
                                Global.mnFrm.cmCde.showMsg("Operation Cancelled because the line with the\r\n ff details was detected as an INVALID Transaction!" +
                                "\r\nACCOUNT: " + dtst.Tables[0].Rows[i][1].ToString() + "." + dtst.Tables[0].Rows[i][2].ToString() +
                                "\r\nAMOUNT: " + netAmnt +
                                "\r\nDATE: " + lnDte, 0);
                                return;
                            }
                        }
                        worker.ReportProgress(Convert.ToInt32((i + 1) * (20.00 / ttltrns)) + 10);
                    }
                }

                for (int i = 0; i < ttltrns; i++)
                {
                    Global.updtActnPrcss(5);
                    //if (worker.CancellationPending == true)
                    //{
                    //  //e.Cancel = true;
                    //  //this.addTrnsButton.Enabled = this.addTrscns;
                    //  //this.addTrnsTmpltButton.Enabled = this.addTrscnsFrmTmp;
                    //  //this.imprtTrnsTmpltButton.Enabled = this.addTrscns;
                    //  //this.exprtTrnsTmpltButton.Enabled = true;
                    //  //this.postTrnsButton.Enabled = this.postTrscns;
                    //  //this.cancelRunButton.Enabled = false;
                    //  //this.loadAccntTrnsPanel();
                    //  //break;
                    //}
                    //else
                    //{
                    System.Windows.Forms.Application.DoEvents();

                    //Update the corresponding account balance and 
                    //update net income balance as well if type is R or EX
                    //update control account if any
                    //update accnt curr bals if different from 
                    int accntCurrID = int.Parse(dtst.Tables[0].Rows[i][17].ToString());
                    int funcCurr = int.Parse(dtst.Tables[0].Rows[i][7].ToString());
                    double accntCurrAmnt = double.Parse(dtst.Tables[0].Rows[i][15].ToString());

                    string acctyp = Global.mnFrm.cmCde.getAccntType(
                     int.Parse(dtst.Tables[0].Rows[i][9].ToString()));
                    bool hsBnUpdt = Global.hsTrnsUptdAcntBls(
                      long.Parse(dtst.Tables[0].Rows[i][0].ToString()),
                    dtst.Tables[0].Rows[i][6].ToString(),
                      int.Parse(dtst.Tables[0].Rows[i][9].ToString()));
                    if (hsBnUpdt == false)
                    {
                        double dbt1 = double.Parse(dtst.Tables[0].Rows[i][4].ToString());
                        double crdt1 = double.Parse(dtst.Tables[0].Rows[i][5].ToString());
                        double net1 = double.Parse(dtst.Tables[0].Rows[i][10].ToString());

                        if (funCurID != accntCurrID)
                        {
                            Global.postAccntCurrTransaction(int.Parse(dtst.Tables[0].Rows[i][9].ToString()),
                             Global.getSign(dbt1) * accntCurrAmnt,
                             Global.getSign(crdt1) * accntCurrAmnt,
                             Global.getSign(net1) * accntCurrAmnt,
                             dtst.Tables[0].Rows[i][6].ToString(),
                             long.Parse(dtst.Tables[0].Rows[i][0].ToString()), accntCurrID);
                        }

                        Global.postTransaction(int.Parse(dtst.Tables[0].Rows[i][9].ToString()),
                         dbt1,
                         crdt1,
                         net1,
                         dtst.Tables[0].Rows[i][6].ToString(),
                         long.Parse(dtst.Tables[0].Rows[i][0].ToString()));
                    }

                    hsBnUpdt = Global.hsTrnsUptdAcntBls(
               long.Parse(dtst.Tables[0].Rows[i][0].ToString()),
               dtst.Tables[0].Rows[i][6].ToString(),
               net_accnt);

                    if (hsBnUpdt == false)
                    {
                        if (acctyp == "R")
                        {
                            Global.postTransaction(net_accnt,
                        double.Parse(dtst.Tables[0].Rows[i][4].ToString()),
                            double.Parse(dtst.Tables[0].Rows[i][5].ToString()),
                            double.Parse(dtst.Tables[0].Rows[i][10].ToString()),
                            dtst.Tables[0].Rows[i][6].ToString(),
                            long.Parse(dtst.Tables[0].Rows[i][0].ToString()));
                        }
                        else if (acctyp == "EX")
                        {
                            Global.postTransaction(net_accnt,
                        double.Parse(dtst.Tables[0].Rows[i][4].ToString()),
                        double.Parse(dtst.Tables[0].Rows[i][5].ToString()),
                        (double)(-1) * double.Parse(dtst.Tables[0].Rows[i][10].ToString()),
                            dtst.Tables[0].Rows[i][6].ToString(),
                            long.Parse(dtst.Tables[0].Rows[i][0].ToString()));
                        }
                    }

                    //get control accnt id
                    int cntrlAcntID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_chart_of_accnts", "accnt_id", "control_account_id", int.Parse(dtst.Tables[0].Rows[i][9].ToString())));
                    if (cntrlAcntID > 0)
                    {
                        hsBnUpdt = Global.hsTrnsUptdAcntBls(
                          long.Parse(dtst.Tables[0].Rows[i][0].ToString()),
                        dtst.Tables[0].Rows[i][6].ToString(),
                          cntrlAcntID);

                        if (hsBnUpdt == false)
                        {
                            int cntrlAcntCurrID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
                       "accb.accb_chart_of_accnts", "accnt_id", "crncy_id", cntrlAcntID));

                            double dbt1 = double.Parse(dtst.Tables[0].Rows[i][4].ToString());
                            double crdt1 = double.Parse(dtst.Tables[0].Rows[i][5].ToString());
                            double net1 = double.Parse(dtst.Tables[0].Rows[i][10].ToString());

                            if (funCurID != cntrlAcntCurrID && cntrlAcntCurrID == accntCurrID)
                            {
                                Global.postAccntCurrTransaction(cntrlAcntID,
                                 Global.getSign(dbt1) * accntCurrAmnt,
                                 Global.getSign(crdt1) * accntCurrAmnt,
                                 Global.getSign(net1) * accntCurrAmnt,
                                 dtst.Tables[0].Rows[i][6].ToString(),
                                 long.Parse(dtst.Tables[0].Rows[i][0].ToString()), accntCurrID);
                            }
                            Global.postTransaction(cntrlAcntID,
                             double.Parse(dtst.Tables[0].Rows[i][4].ToString()),
                             double.Parse(dtst.Tables[0].Rows[i][5].ToString()),
                             double.Parse(dtst.Tables[0].Rows[i][10].ToString()),
                             dtst.Tables[0].Rows[i][6].ToString(),
                             long.Parse(dtst.Tables[0].Rows[i][0].ToString()));
                        }
                    }
                    Global.chngeTrnsStatus(long.Parse(dtst.Tables[0].Rows[i][0].ToString()), "1");
                    Global.changeReconciledStatus(long.Parse(dtst.Tables[0].Rows[i][20].ToString()), "1");
                    Global.mnFrm.cmCde.updateLogMsg(msg_id,
               "\r\nSuccessfully posted transaction ID= " + dtst.Tables[0].Rows[i][0].ToString()
               , log_tbl, dateStr);

                    worker.ReportProgress(Convert.ToInt32((i + 1) * (68.00 / ttltrns)) + 30);
                    System.Windows.Forms.Application.DoEvents();

                    //}        
                }
                //Call Accnts Chart Bals Update
                worker.ReportProgress(98);
                this.reloadAcntChrtBals(long.Parse((string)myargs[0]), net_accnt);
                worker.ReportProgress(99);
                Global.mnFrm.cmCde.updateLogMsg(msg_id,
            "\r\nSuccessfully Reloaded Chart of Account Balances!"
            , log_tbl, dateStr);
                System.Windows.Forms.Application.DoEvents();
                double aesum = Global.get_COA_AESum(int.Parse((string)myargs[1]));
                double crlsum = Global.get_COA_CRLSum(int.Parse((string)myargs[1]));
                if (aesum != crlsum)
                {
                    Global.mnFrm.cmCde.updateLogMsg(msg_id,
               "\r\nBatch of Transactions caused an " +
                      "IMBALANCE in the Accounting! A+E=" + aesum + "\r\nC+R+L=" + crlsum + "\r\nDiff=" + (aesum - crlsum), log_tbl, dateStr);
                    string errmsg = "";

                    Global.mnFrm.cmCde.updateLogMsg(msg_id,
               "\r\n" + errmsg + "\r\nProcess to undo the posted transactions " +
               "is about to start...!", log_tbl, dateStr);
                    System.Windows.Forms.Application.DoEvents();
                    System.Windows.Forms.Application.DoEvents();
                    //Global.mnFrm.cmCde.showMsg("Batch of Transactions caused an " +
                    ////  "IMBALANCE in the Accounting !", 0);
                    for (int i = 0; i < ttltrns; i++)
                    {
                        Global.updtActnPrcss(5);
                        int accntCurrID = int.Parse(dtst.Tables[0].Rows[i][17].ToString());
                        int funcCurr = int.Parse(dtst.Tables[0].Rows[i][7].ToString());
                        double accntCurrAmnt = double.Parse(dtst.Tables[0].Rows[i][15].ToString());
                        string acctyp = Global.mnFrm.cmCde.getAccntType(
                         int.Parse(dtst.Tables[0].Rows[i][9].ToString()));
                        bool hsBnUpdt = Global.hsTrnsUptdAcntBls(
                          long.Parse(dtst.Tables[0].Rows[i][0].ToString()),
                        dtst.Tables[0].Rows[i][6].ToString(),
                          int.Parse(dtst.Tables[0].Rows[i][9].ToString()));

                        if (hsBnUpdt == true)
                        {
                            double dbt1 = double.Parse(dtst.Tables[0].Rows[i][4].ToString());
                            double crdt1 = double.Parse(dtst.Tables[0].Rows[i][5].ToString());
                            double net1 = double.Parse(dtst.Tables[0].Rows[i][10].ToString());

                            if (funCurID != accntCurrID)
                            {
                                Global.undoPostAccntCurrTransaction(int.Parse(dtst.Tables[0].Rows[i][9].ToString()),
                                 Global.getSign(dbt1) * accntCurrAmnt,
                                 Global.getSign(crdt1) * accntCurrAmnt,
                                 Global.getSign(net1) * accntCurrAmnt,
                                 dtst.Tables[0].Rows[i][6].ToString(),
                                 long.Parse(dtst.Tables[0].Rows[i][0].ToString()), accntCurrID);
                            }
                            Global.undoPostTransaction(int.Parse(dtst.Tables[0].Rows[i][9].ToString()),
                             double.Parse(dtst.Tables[0].Rows[i][4].ToString()),
                             double.Parse(dtst.Tables[0].Rows[i][5].ToString()),
                             double.Parse(dtst.Tables[0].Rows[i][10].ToString()),
                            dtst.Tables[0].Rows[i][6].ToString(),
                                long.Parse(dtst.Tables[0].Rows[i][0].ToString()));
                        }
                        hsBnUpdt = Global.hsTrnsUptdAcntBls(
                        long.Parse(dtst.Tables[0].Rows[i][0].ToString()),
                      dtst.Tables[0].Rows[i][6].ToString(),
                        net_accnt);
                        if (hsBnUpdt == true)
                        {
                            if (acctyp == "R")
                            {
                                Global.undoPostTransaction(net_accnt,
                            double.Parse(dtst.Tables[0].Rows[i][4].ToString()),
                            double.Parse(dtst.Tables[0].Rows[i][5].ToString()),
                            double.Parse(dtst.Tables[0].Rows[i][10].ToString()),
                               dtst.Tables[0].Rows[i][6].ToString(),
                                   long.Parse(dtst.Tables[0].Rows[i][0].ToString()));
                            }
                            else if (acctyp == "EX")
                            {
                                Global.undoPostTransaction(net_accnt,
                            double.Parse(dtst.Tables[0].Rows[i][4].ToString()),
                            double.Parse(dtst.Tables[0].Rows[i][5].ToString()),
                            (double)(-1) * double.Parse(dtst.Tables[0].Rows[i][10].ToString()),
                               dtst.Tables[0].Rows[i][6].ToString(),
                                   long.Parse(dtst.Tables[0].Rows[i][0].ToString()));
                            }
                        }

                        //get control accnt id
                        int cntrlAcntID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_chart_of_accnts", "accnt_id", "control_account_id", int.Parse(dtst.Tables[0].Rows[i][9].ToString())));
                        if (cntrlAcntID > 0)
                        {
                            hsBnUpdt = Global.hsTrnsUptdAcntBls(
                              long.Parse(dtst.Tables[0].Rows[i][0].ToString()),
                            dtst.Tables[0].Rows[i][6].ToString(),
                              cntrlAcntID);

                            if (hsBnUpdt == true)
                            {
                                int cntrlAcntCurrID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
                        "accb.accb_chart_of_accnts", "accnt_id", "crncy_id", cntrlAcntID));

                                double dbt1 = double.Parse(dtst.Tables[0].Rows[i][4].ToString());
                                double crdt1 = double.Parse(dtst.Tables[0].Rows[i][5].ToString());
                                double net1 = double.Parse(dtst.Tables[0].Rows[i][10].ToString());

                                if (funCurID != cntrlAcntCurrID && cntrlAcntCurrID == accntCurrID)
                                {
                                    Global.undoPostAccntCurrTransaction(cntrlAcntID,
                                     Global.getSign(dbt1) * accntCurrAmnt,
                                     Global.getSign(crdt1) * accntCurrAmnt,
                                     Global.getSign(net1) * accntCurrAmnt,
                                     dtst.Tables[0].Rows[i][6].ToString(),
                                     long.Parse(dtst.Tables[0].Rows[i][0].ToString()), accntCurrID);
                                }
                                Global.undoPostTransaction(cntrlAcntID,
                                 double.Parse(dtst.Tables[0].Rows[i][4].ToString()),
                                 double.Parse(dtst.Tables[0].Rows[i][5].ToString()),
                                 double.Parse(dtst.Tables[0].Rows[i][10].ToString()),
                                dtst.Tables[0].Rows[i][6].ToString(),
                                    long.Parse(dtst.Tables[0].Rows[i][0].ToString()));
                            }
                        }
                        Global.chngeTrnsStatus(long.Parse(dtst.Tables[0].Rows[i][0].ToString()), "0");
                        Global.mnFrm.cmCde.updateLogMsg(msg_id,
                  "\r\nSuccessfully unposted transaction ID= " + dtst.Tables[0].Rows[i][0].ToString()
                  , log_tbl, dateStr);
                        worker.ReportProgress(Convert.ToInt32((ttltrns - (i + 1)) * (99 / ttltrns)));
                        System.Windows.Forms.Application.DoEvents();
                        //}
                    }
                    //Call Accnts Chart Bals Update
                    worker.ReportProgress(10);
                    this.reloadAcntChrtBals(long.Parse((string)myargs[0]), net_accnt);
                    worker.ReportProgress(0);
                    Global.mnFrm.cmCde.updateLogMsg(msg_id,
               "\r\nSuccessfully Reloaded Original Chart of Account Balances!"
               , log_tbl, dateStr);
                    System.Windows.Forms.Application.DoEvents();
                }
                else
                {
                    Global.updateBatchStatus(long.Parse(this.batchIDTextBox.Text));
                    Global.mnFrm.cmCde.updateLogMsg(msg_id,
               "\r\nBatch of Transactions POSTED SUCCESSFULLY!"
               , log_tbl, dateStr);
                    worker.ReportProgress(100);
                    System.Windows.Forms.Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message + "\r\n\r\n" + ex.InnerException + "\r\n\r\n" + ex.StackTrace, 4);
            }
        }

        private void addTrnsTmpltButton_Click(object sender, EventArgs e)
        {
            this.addTrnsFrmTmpButton_Click(this.addTrnsFrmTmpButton, e);
        }

        private void addTrnsMenuItem_Click(object sender, EventArgs e)
        {
            this.addDirTrnsButton_Click(this.addDirTrnsButton, e);
        }

        private void addTrnsTmpltMenuItem_Click(object sender, EventArgs e)
        {
            this.addTrnsFrmTmpButton_Click(this.addTrnsFrmTmpButton, e);
        }

        private void editTrnsMenuItem_Click(object sender, EventArgs e)
        {
            this.editTrnsButton_Click(this.editTrnsButton, e);
        }

        private void deleteTrnsMenuItem_Click(object sender, EventArgs e)
        {
            this.delTrnsButton_Click(this.delTrnsButton, e);
        }

        private void netBalNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (this.netBalNumericUpDown.Value < 0 &&
             this.isContraCheckBox.Checked == false)
            {
                this.netBalNumericUpDown.BackColor = Color.Red;
            }
            else if (this.netBalNumericUpDown.Value > 0 &&
             this.isContraCheckBox.Checked == true)
            {
                this.netBalNumericUpDown.BackColor = Color.Red;
            }
            else
            {
                this.netBalNumericUpDown.BackColor = Color.Green;
            }
        }

        private void loadTrnsDetPanel()
        {
            if (this.backgroundWorker1.IsBusy == true)
            {
                return;
            }
            this.obey_tdet_evnts = false;
            int dsply = 0;
            if (this.dsplySizeDetComboBox.Text == ""
             || int.TryParse(this.dsplySizeDetComboBox.Text, out dsply) == false)
            {
                this.dsplySizeDetComboBox.Text = "5000";// Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            //this.groupBox8.Height = this.g.Bottom - this.toolStrip4.Bottom - 50;
            this.tdet_cur_indx = 0;
            this.is_last_tdet = false;
            this.last_tdet_num = 0;
            this.totl_tdet = Global.mnFrm.cmCde.Big_Val;
            this.getTdetPnlData();
            this.obey_tdet_evnts = true;
        }

        private void getTdetPnlData()
        {
            this.updtTdetTotals();
            this.populateTdetListVw();
            this.updtTdetNavLabels();
        }

        private void updtTdetTotals()
        {
            int dsply = 0;
            if (this.dsplySizeDetComboBox.Text == ""
              || int.TryParse(this.dsplySizeDetComboBox.Text, out dsply) == false)
            {
                this.dsplySizeDetComboBox.Text = "5000";// Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            this.myNav.FindNavigationIndices(
          long.Parse(this.dsplySizeDetComboBox.Text), this.totl_tdet);
            if (this.tdet_cur_indx >= this.myNav.totalGroups)
            {
                this.tdet_cur_indx = this.myNav.totalGroups - 1;
            }
            if (this.tdet_cur_indx < 0)
            {
                this.tdet_cur_indx = 0;
            }
            this.myNav.currentNavigationIndex = this.tdet_cur_indx;
        }

        private void updtTdetNavLabels()
        {
            this.moveFirstDetButton.Enabled = this.myNav.moveFirstBtnStatus();
            this.movePreviousDetButton.Enabled = this.myNav.movePrevBtnStatus();
            this.moveNextDetButton.Enabled = this.myNav.moveNextBtnStatus();
            this.moveLastDetButton.Enabled = this.myNav.moveLastBtnStatus();
            this.positionDetTextBox.Text = this.myNav.displayedRecordsNumbers();
            if (this.is_last_tdet == true ||
             this.totl_tdet != Global.mnFrm.cmCde.Big_Val)
            {
                this.totalRecsDetLabel.Text = this.myNav.totalRecordsLabel();
            }
            else
            {
                this.totalRecsDetLabel.Text = "of Total";
            }
        }

        private void populateTdetListVw()
        {
            this.obey_tdet_evnts = false;

            DataSet dtst = Global.get_One_Batch_Trns(this.tdet_cur_indx,
             int.Parse(this.dsplySizeDetComboBox.Text), long.Parse(this.batchIDTextBox.Text));
            this.trnsDetListView.Items.Clear();
            double ttlDbts = 0;
            double ttlCredits = 0;
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.last_tdet_num = this.myNav.startIndex() + i;
                string trnstatus = "NOT POSTED";
                if (dtst.Tables[0].Rows[i][11].ToString() == "1")
                {
                    trnstatus = "POSTED";
                }
                ListViewItem nwItem = new ListViewItem(new string[] {
    (this.myNav.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),dtst.Tables[0].Rows[i][2].ToString(),
    dtst.Tables[0].Rows[i][3].ToString(),
    double.Parse(dtst.Tables[0].Rows[i][4].ToString()).ToString("#,##0.00"),
    double.Parse(dtst.Tables[0].Rows[i][5].ToString()).ToString("#,##0.00"),
    Global.mnFrm.cmCde.getPssblValNm(int.Parse(dtst.Tables[0].Rows[i][7].ToString())),
    dtst.Tables[0].Rows[i][6].ToString(),
    dtst.Tables[0].Rows[i][0].ToString(),
    dtst.Tables[0].Rows[i][8].ToString(),
    dtst.Tables[0].Rows[i][7].ToString(),
    double.Parse(dtst.Tables[0].Rows[i][10].ToString()).ToString("#,##0.00"),
    dtst.Tables[0].Rows[i][9].ToString(),
          trnstatus,
    double.Parse(dtst.Tables[0].Rows[i][12].ToString()).ToString("#,##0.00"),
    dtst.Tables[0].Rows[i][13].ToString(),
    dtst.Tables[0].Rows[i][14].ToString(),
    double.Parse(dtst.Tables[0].Rows[i][15].ToString()).ToString("#,##0.00"),
    dtst.Tables[0].Rows[i][16].ToString(),
    dtst.Tables[0].Rows[i][17].ToString(),
    double.Parse(dtst.Tables[0].Rows[i][18].ToString()).ToString(),
    double.Parse(dtst.Tables[0].Rows[i][19].ToString()).ToString(),
    dtst.Tables[0].Rows[i][20].ToString()});

                ttlDbts += double.Parse(dtst.Tables[0].Rows[i][4].ToString());
                ttlCredits += double.Parse(dtst.Tables[0].Rows[i][5].ToString());

                this.trnsDetListView.Items.Add(nwItem);
            }
            this.correctTdetNavLbls(dtst);

            ListViewItem nwItem1 = new ListViewItem(new string[] {
    "","","","CURRENT DISPLAY'S TOTALS = ",ttlDbts.ToString("#,##0.00"),
    ttlCredits.ToString("#,##0.00"),
    "","","","","","","","","","","","","","","","",""});
            nwItem1.UseItemStyleForSubItems = false;
            nwItem1.SubItems[3].Font = new Font("Tahoma", 10, FontStyle.Bold | FontStyle.Underline);
            nwItem1.SubItems[4].Font = new Font("Tahoma", 10, FontStyle.Bold | FontStyle.Underline);
            nwItem1.SubItems[5].Font = new Font("Tahoma", 10, FontStyle.Bold | FontStyle.Underline);

            nwItem1.SubItems[3].BackColor = Color.LightGray;
            nwItem1.SubItems[4].BackColor = Color.LightGray;
            nwItem1.SubItems[5].BackColor = Color.LightGray;

            this.trnsDetListView.Items.Add(nwItem1);
            if (ttlDbts > ttlCredits)
            {
                ttlDbts = ttlDbts - ttlCredits;
                ttlCredits = 0;
            }
            else
            {
                ttlCredits = ttlCredits - ttlDbts;
                ttlDbts = 0;
            }
            if (ttlDbts != 0)
            {
                nwItem1 = new ListViewItem(new string[] {
    "","","","DIFFERENCE = ",ttlDbts.ToString("#,##0.00"),"",
    "","","","","","","","","","","","","","","","",""});
            }
            else if (ttlCredits != 0)
            {
                nwItem1 = new ListViewItem(new string[] {
    "","","","DIFFERENCE = ","",
    ttlCredits.ToString("#,##0.00"),
    "","","","","","","","","","","","","","","","",""});
            }

            if (ttlCredits != ttlDbts)
            {
                nwItem1.UseItemStyleForSubItems = false;
                nwItem1.SubItems[3].Font = new Font("Tahoma", 10, FontStyle.Bold | FontStyle.Underline);
                nwItem1.SubItems[4].Font = new Font("Tahoma", 10, FontStyle.Bold | FontStyle.Underline);
                nwItem1.SubItems[5].Font = new Font("Tahoma", 10, FontStyle.Bold | FontStyle.Underline);

                nwItem1.SubItems[3].BackColor = Color.LightGray;
                nwItem1.SubItems[4].BackColor = Color.LightGray;
                nwItem1.SubItems[5].BackColor = Color.LightGray;

                this.trnsDetListView.Items.Add(nwItem1);
            }
            this.obey_tdet_evnts = true;
        }

        private void correctTdetNavLbls(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.tdet_cur_indx == 0 && totlRecs == 0)
            {
                this.is_last_tdet = true;
                this.totl_tdet = 0;
                this.last_tdet_num = 0;
                this.tdet_cur_indx = 0;
                this.updtTdetTotals();
                this.updtTdetNavLabels();
            }
            else if (this.totl_tdet == Global.mnFrm.cmCde.Big_Val
          && totlRecs < long.Parse(this.dsplySizeDetComboBox.Text))
            {
                this.totl_tdet = this.last_tdet_num;
                if (totlRecs == 0)
                {
                    this.tdet_cur_indx -= 1;
                    this.updtTdetTotals();
                    this.populateTdetListVw();
                }
                else
                {
                    this.updtTdetTotals();
                }
            }
        }

        private bool shdObeyTdetEvts()
        {
            return this.obey_tdet_evnts;
        }

        private void TdetPnlNavButtons(object sender, System.EventArgs e)
        {
            if (this.backgroundWorker1.IsBusy == true)
            {
                return;
            }
            System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecsDetLabel.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.is_last_tdet = false;
                this.tdet_cur_indx = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.is_last_tdet = false;
                this.tdet_cur_indx -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.is_last_tdet = false;
                this.tdet_cur_indx += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.is_last_tdet = true;
                this.totl_tdet = Global.get_Total_BatchTrns(long.Parse(this.batchIDTextBox.Text));
                this.updtTdetTotals();
                this.tdet_cur_indx = this.myNav.totalGroups - 1;
            }
            this.getTdetPnlData();
        }

        private void vwSQLTrnsMenuItem_Click(object sender, EventArgs e)
        {
            //string txt = Global.mnFrm.cmCde.encrypt("rHOMICOM2012");
            //string nwtxt = Global.mnFrm.cmCde.decrypt(txt);
            //Global.mnFrm.cmCde.showMsg(txt + "\r\n" + nwtxt, 3);
            //Global.mnFrm.cmCde.exprtToExcel(this.trnsDetListView);
            //Global.mnFrm.cmCde.exprtToExcel(this.excelDtst);
            Global.mnFrm.cmCde.showSQL(Global.mnFrm.trnsDet_SQL, 10);
        }

        private void delBatchMenuItem_Click(object sender, EventArgs e)
        {
            this.voidBatchButton_Click(this.voidBatchButton, e);
        }

        private void exptExclBtchMenuItem_Click(object sender, EventArgs e)
        {
            if (this.backgroundWorker1.IsBusy == true)
            {
                return;
            }
            Global.mnFrm.cmCde.exprtToExcel(this.trnsBatchListView);
        }

        private void exptExclTdetMenuItem_Click(object sender, EventArgs e)
        {
            if (this.backgroundWorker1.IsBusy == true)
            {
                return;
            }
            Global.mnFrm.cmCde.exprtToExcelSelective(this.trnsDetListView, "TRANSACTIONS IN BATCH NO.(" + this.batchNameTextBox.Text + ")");
        }

        private void exprtTrnsTmpltButton_Click(object sender, EventArgs e)
        {
            if (this.backgroundWorker1.IsBusy == true)
            {
                return;
            }
            string rspnse = Interaction.InputBox("How many Transactions will you like to Export?" +
              "\r\n1=No Transaction(Empty Template)" +
              "\r\n2=All Transactions" +
            "\r\n3-Infinity=Specify the exact number of Transactions to Export\r\n",
              "Rhomicom", "1", (Global.mnFrm.cmCde.myComputer.Screen.Bounds.Width / 2) - 170,
              (Global.mnFrm.cmCde.myComputer.Screen.Bounds.Height / 2) - 100);
            if (rspnse.Equals(string.Empty) || rspnse.Equals(null))
            {
                Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            long rsponse = 0;
            bool rsps = long.TryParse(rspnse, out rsponse);
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
            Global.mnFrm.cmCde.exprtTrnsTmp(rsponse, long.Parse(this.batchIDTextBox.Text));
        }

        private void imprtTrnsTmpltButton_Click(object sender, EventArgs e)
        {
            if (this.backgroundWorker1.IsBusy == true)
            {
                return;
            }
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.batchIDTextBox.Text == "" ||
              this.batchIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select the batch to import into!", 0);
                return;
            }
            if (this.batchStatusLabel.Text == "Posted")
            {
                Global.mnFrm.cmCde.showMsg("Cannot Import Transactions into already Posted Batch of Transactions!", 0);
                return;
            }
            if (this.batchSourceLabel.Text != "Manual")
            {
                Global.mnFrm.cmCde.showMsg("Cannot Import Transactions into Batches \r\nthat came from other Modules!", 0);
                return;
            }

            this.openFileDialog1.RestoreDirectory = true;
            this.openFileDialog1.Filter = "All Files|*.*|Excel Files|*.xls;*.xlsx";
            this.openFileDialog1.FilterIndex = 2;
            this.openFileDialog1.Title = "Select an Excel File to Upload...";
            this.openFileDialog1.FileName = "";
            if (this.openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Global.mnFrm.cmCde.imprtTrnsTmp(long.Parse(this.batchIDTextBox.Text), this.openFileDialog1.FileName);
            }
            this.populateTrnsDet(long.Parse(this.batchIDTextBox.Text));
        }

        private void vwLogMsgButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[21]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.batchIDTextBox.Text == "" ||
         this.batchIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select a saved batch First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showLogMsg(
              Global.mnFrm.cmCde.getLogMsgID("accb.accb_post_trns_msgs",
              "Posting Batch of Transactions", long.Parse(this.batchIDTextBox.Text)),
              "accb.accb_post_trns_msgs");
        }

        private void cancelRunButton_Click(object sender, EventArgs e)
        {
            if (this.backgroundWorker1.IsBusy == true)
            {
                this.backgroundWorker1.CancelAsync();
            }
        }

        private void searchForTrnsTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (this.backgroundWorker1.IsBusy == true)
            {
                return;
            }
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.goTrnsButton_Click(this.goTrnsButton, ex);
            }
        }

        private void positionTrnsTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (this.backgroundWorker1.IsBusy == true)
            {
                return;
            }
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

        private void positionDetTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (this.backgroundWorker1.IsBusy == true)
            {
                return;
            }
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                this.TdetPnlNavButtons(this.movePreviousDetButton, ex);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
            {
                this.TdetPnlNavButtons(this.moveNextDetButton, ex);
            }
        }

        private void vwSQLBatchButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(Global.mnFrm.trns_SQL, 10);
        }

        private void recHstryBatchButton_Click(object sender, EventArgs e)
        {
            if (this.batchIDTextBox.Text == "-1"
         || this.batchIDTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please select a Batch First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(Global.get_Batch_Rec_Hstry(long.Parse(this.batchIDTextBox.Text)), 9);
        }

        private void addBatchMenuItem_Click(object sender, EventArgs e)
        {
            this.addTrnsBatchButton_Click(this.addTrnsBatchButton, e);
        }

        private void editBatchMenuItem_Click(object sender, EventArgs e)
        {
            this.editTrnsBatchButton_Click(this.editTrnsBatchButton, e);
        }

        private void refreshBatchMenuItem_Click(object sender, EventArgs e)
        {
            this.goTrnsButton_Click(this.goTrnsButton, e);
        }

        private void recHstryBatchMenuItem_Click(object sender, EventArgs e)
        {
            this.recHstryBatchButton_Click(this.recHstryBatchButton, e);
        }

        private void vwSQLBatchMenuItem_Click(object sender, EventArgs e)
        {
            this.vwSQLBatchButton_Click(this.vwSQLBatchButton, e);
        }

        private void refreshTrnsMenuItem_Click(object sender, EventArgs e)
        {
            if (this.backgroundWorker1.IsBusy == true)
            {
                return;
            }
            this.loadTrnsDetPanel();
        }

        private void recHstryTrnsMenuItem_Click(object sender, EventArgs e)
        {
            if (this.trnsDetListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Transaction First!", 0);
                return;
            }
            if (this.trnsDetListView.SelectedItems[0].SubItems[8].Text == "")
            {
                return;
            }

            Global.mnFrm.cmCde.showRecHstry(Global.get_TrnsDet_Rec_Hstry(long.Parse(this.trnsDetListView.SelectedItems[0].SubItems[8].Text)), 9);
        }
        #endregion

        #region "TRANSACTIONS SEARCH..."
        private void loadSrchPanel()
        {
            this.obeySrchEvnts = false;
            if (this.searchInSrchComboBox.SelectedIndex < 0)
            {
                this.searchInSrchComboBox.SelectedIndex = 4;
            }
            if (this.orderByComboBox.SelectedIndex < 0)
            {
                this.orderByComboBox.SelectedIndex = 0;
            }
            int dsply = 0;
            if (this.dsplySizeSrchComboBox.Text == ""
             || int.TryParse(this.dsplySizeSrchComboBox.Text, out dsply) == false)
            {
                this.dsplySizeSrchComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }

            if (searchForSrchTextBox.Text.Contains("%") == false)
            {
                this.searchForSrchTextBox.Text = "%" + this.searchForSrchTextBox.Text.Replace(" ", "%") + "%";
            }
            if (this.searchForSrchTextBox.Text == "%%")
            {
                this.searchForSrchTextBox.Text = "%";
            }
            this.is_last_srch = false;
            this.totl_srch = Global.mnFrm.cmCde.Big_Val;
            this.getSrchPnlData();
            this.obeySrchEvnts = true;
            if (this.searchForSrchTextBox.Focused)
            {
                this.trnsSearchListView.Focus();
            }
        }

        private void getSrchPnlData()
        {
            this.updtSrchTotals();
            this.populateSrchGridVw();
            this.updtSrchNavLabels();
        }

        private void updtSrchTotals()
        {
            Global.mnFrm.cmCde.navFuncts.FindNavigationIndices(
             int.Parse(this.dsplySizeSrchComboBox.Text),
            this.totl_srch);

            if (this.cur_srch_idx >= Global.mnFrm.cmCde.navFuncts.totalGroups)
            {
                this.cur_srch_idx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            if (this.cur_srch_idx < 0)
            {
                this.cur_srch_idx = 0;
            }
            Global.mnFrm.cmCde.navFuncts.currentNavigationIndex = this.cur_srch_idx;
        }

        private void updtSrchNavLabels()
        {
            this.moveFirstSrchButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveFirstBtnStatus();
            this.movePreviousSrchButton.Enabled = Global.mnFrm.cmCde.navFuncts.movePrevBtnStatus();
            this.moveNextSrchButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveNextBtnStatus();
            this.moveLastSrchButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveLastBtnStatus();
            this.positionSrchTextBox.Text = Global.mnFrm.cmCde.navFuncts.displayedRecordsNumbers();
            if (this.is_last_srch == true ||
             this.totl_srch != Global.mnFrm.cmCde.Big_Val)
            {
                this.totalRecSrchLabel.Text = Global.mnFrm.cmCde.navFuncts.totalRecordsLabel();
            }
            else
            {
                this.totalRecSrchLabel.Text = "of Total";
            }
        }

        private void populateSrchGridVw()
        {
            this.obeySrchEvnts = false;
            DataSet dtst;

            dtst = Global.get_Transactions(this.searchForSrchTextBox.Text,
            this.searchInSrchComboBox.Text, this.cur_srch_idx,
            int.Parse(this.dsplySizeSrchComboBox.Text),
            Global.mnFrm.cmCde.Org_id,
                  this.vldStrtDteTextBox.Text,
                  this.vldEndDteTextBox.Text, this.isPostedCheckBox.Checked,
                  this.numericUpDown1.Value, this.numericUpDown2.Value, this.orderByComboBox.Text);
            this.trnsSearchListView.Items.Clear();
            double ttlDbts = 0;
            double ttlCredits = 0;

            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                string trnstatus = "NOT POSTED";
                if (dtst.Tables[0].Rows[i][12].ToString() == "1")
                {
                    trnstatus = "POSTED";
                }
                this.last_srch_num = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
                ListViewItem nwItem = new ListViewItem(new string[] {
    (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),
                dtst.Tables[0].Rows[i][2].ToString(),
    dtst.Tables[0].Rows[i][3].ToString(),
    double.Parse(dtst.Tables[0].Rows[i][4].ToString()).ToString("#,##0.00"),
    double.Parse(dtst.Tables[0].Rows[i][5].ToString()).ToString("#,##0.00"),
    Global.mnFrm.cmCde.getPssblValNm(int.Parse(dtst.Tables[0].Rows[i][7].ToString())),
    dtst.Tables[0].Rows[i][6].ToString(),
    dtst.Tables[0].Rows[i][0].ToString(),
    dtst.Tables[0].Rows[i][8].ToString(),
    dtst.Tables[0].Rows[i][7].ToString(),
    dtst.Tables[0].Rows[i][10].ToString(),
    dtst.Tables[0].Rows[i][9].ToString(),trnstatus,
    dtst.Tables[0].Rows[i][11].ToString(),
    dtst.Tables[0].Rows[i][13].ToString(),
    double.Parse(dtst.Tables[0].Rows[i][14].ToString()).ToString("#,##0.00"),
    dtst.Tables[0].Rows[i][15].ToString(),
    dtst.Tables[0].Rows[i][16].ToString(),
    double.Parse(dtst.Tables[0].Rows[i][17].ToString()).ToString("#,##0.00"),
    dtst.Tables[0].Rows[i][18].ToString(),
    dtst.Tables[0].Rows[i][19].ToString(),
    double.Parse(dtst.Tables[0].Rows[i][20].ToString()).ToString(),
    double.Parse(dtst.Tables[0].Rows[i][21].ToString()).ToString(),
    dtst.Tables[0].Rows[i][23].ToString()});
                this.trnsSearchListView.Items.Add(nwItem);

                ttlDbts += double.Parse(dtst.Tables[0].Rows[i][4].ToString());
                ttlCredits += double.Parse(dtst.Tables[0].Rows[i][5].ToString());

            }
            this.correctSrchNavLbls(dtst);

            ListViewItem nwItem1 = new ListViewItem(new string[] {
    "","","","CURRENT DISPLAY'S TOTALS = ",ttlDbts.ToString("#,##0.00"),
    ttlCredits.ToString("#,##0.00"),
    "","","","","","","","","","","","","","","","","","",""});
            nwItem1.UseItemStyleForSubItems = false;
            nwItem1.SubItems[3].Font = new Font("Tahoma", 10, FontStyle.Bold | FontStyle.Underline);
            nwItem1.SubItems[4].Font = new Font("Tahoma", 10, FontStyle.Bold | FontStyle.Underline);
            nwItem1.SubItems[5].Font = new Font("Tahoma", 10, FontStyle.Bold | FontStyle.Underline);

            nwItem1.SubItems[3].BackColor = Color.LightGray;
            nwItem1.SubItems[4].BackColor = Color.LightGray;
            nwItem1.SubItems[5].BackColor = Color.LightGray;

            this.trnsSearchListView.Items.Add(nwItem1);
            if (ttlDbts > ttlCredits)
            {
                ttlDbts = ttlDbts - ttlCredits;
                ttlCredits = 0;
            }
            else
            {
                ttlCredits = ttlCredits - ttlDbts;
                ttlDbts = 0;
            }
            if (ttlDbts != 0)
            {
                nwItem1 = new ListViewItem(new string[] {
    "","","","DIFFERENCE = ",ttlDbts.ToString("#,##0.00"),"",
    "","","","","","","","","","","","","","","","","","",""});
            }
            else if (ttlCredits != 0)
            {
                nwItem1 = new ListViewItem(new string[] {
    "","","","DIFFERENCE = ","",
    ttlCredits.ToString("#,##0.00"),
    "","","","","","","","","","","","","","","","","","",""});
            }

            if (ttlCredits != ttlDbts)
            {
                nwItem1.UseItemStyleForSubItems = false;
                nwItem1.SubItems[3].Font = new Font("Tahoma", 10, FontStyle.Bold | FontStyle.Underline);
                nwItem1.SubItems[4].Font = new Font("Tahoma", 10, FontStyle.Bold | FontStyle.Underline);
                nwItem1.SubItems[5].Font = new Font("Tahoma", 10, FontStyle.Bold | FontStyle.Underline);

                nwItem1.SubItems[3].BackColor = Color.LightGray;
                nwItem1.SubItems[4].BackColor = Color.LightGray;
                nwItem1.SubItems[5].BackColor = Color.LightGray;

                this.trnsSearchListView.Items.Add(nwItem1);
            }
            this.obeySrchEvnts = true;
        }

        private void correctSrchNavLbls(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.cur_srch_idx == 0 && totlRecs == 0)
            {
                this.is_last_srch = true;
                this.totl_srch = 0;
                this.last_srch_num = 0;
                this.cur_srch_idx = 0;
                this.updtSrchTotals();
                this.updtSrchNavLabels();
            }
            else if (this.totl_srch == Global.mnFrm.cmCde.Big_Val
          && totlRecs < long.Parse(this.dsplySizeSrchComboBox.Text))
            {
                this.totl_srch = this.last_srch_num;
                if (totlRecs == 0)
                {
                    this.cur_srch_idx -= 1;
                    this.updtSrchTotals();
                    this.populateSrchGridVw();
                }
                else
                {
                    this.updtSrchTotals();
                }
            }
        }

        private void SrchPnlNavButtons(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripButton sentObj =
             (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecSrchLabel.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.cur_srch_idx = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.cur_srch_idx -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.cur_srch_idx += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.totl_srch = Global.get_Total_Transactions(
              this.searchForSrchTextBox.Text, this.searchInSrchComboBox.Text,
               Global.mnFrm.cmCde.Org_id, this.vldStrtDteTextBox.Text,
                    this.vldEndDteTextBox.Text, this.isPostedCheckBox.Checked,
                    this.numericUpDown1.Value, this.numericUpDown2.Value);
                this.is_last_srch = true;
                this.updtSrchTotals();
                this.cur_srch_idx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            this.getSrchPnlData();
        }

        private void goSrchButton_Click(object sender, EventArgs e)
        {
            this.loadSrchPanel();
        }

        private void dte1Button_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.selectDate(ref this.vldStrtDteTextBox);
            this.loadSrchPanel();
        }

        private void dte2Button_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.selectDate(ref this.vldEndDteTextBox);
            this.loadSrchPanel();
        }

        private void exptExclTSrchMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcelSelective(this.trnsSearchListView,
              "SEARCHED TRANSACTIONS FROM " + this.vldStrtDteTextBox.Text + " TO " + this.vldEndDteTextBox.Text);
        }

        private void searchForSrchTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.trnsSearchListView.Focus();
                this.goSrchButton_Click(this.goSrchButton, ex);
            }
        }

        private void positionSrchTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                this.SrchPnlNavButtons(this.movePreviousSrchButton, ex);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
            {
                this.SrchPnlNavButtons(this.moveNextSrchButton, ex);
            }
        }

        private void vwSQLSrchButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(Global.mnFrm.vwsrchSQLStmnt, 10);
        }

        private void recHstrySrchButton_Click(object sender, EventArgs e)
        {
            if (this.trnsSearchListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Transaction First!", 0);
                return;
            }

            Global.mnFrm.cmCde.showRecHstry(Global.get_TrnsDet_Rec_Hstry(long.Parse(this.trnsSearchListView.SelectedItems[0].SubItems[8].Text)), 9);
        }

        private void rfrshTsrchMenuItem_Click(object sender, EventArgs e)
        {
            this.goSrchButton_Click(this.goSrchButton, e);
        }

        private void rcHstryTsrchMenuItem_Click(object sender, EventArgs e)
        {
            this.recHstrySrchButton_Click(this.recHstrySrchButton, e);
        }

        private void vwSQLTsrchMenuItem_Click(object sender, EventArgs e)
        {
            this.vwSQLSrchButton_Click(this.vwSQLSrchButton, e);
        }
        #endregion

        #region "TRIAL BALANCE..."
        //private void importAllBalsToDailyBals()
        //{
        //  DataSet dtst = Global.get_All_Chrt_Det();
        //  for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
        //  {
        //    Global.createDailyBals(int.Parse(dtst.Tables[0].Rows[i][0].ToString()),
        //     double.Parse(dtst.Tables[0].Rows[i][3].ToString()),
        //     double.Parse(dtst.Tables[0].Rows[i][1].ToString()),
        //     double.Parse(dtst.Tables[0].Rows[i][2].ToString()),
        //     DateTime.Parse(dtst.Tables[0].Rows[i][4].ToString()).ToString("dd-MMM-yyyy"));
        //  }
        //}

        private void populateTrialBals(string balsDate)
        {
            //Check if no other accounting process is running
            bool isAnyRnng = true;
            do
            {
                isAnyRnng = Global.isThereANActvActnPrcss("5", "10 second");
                System.Windows.Forms.Application.DoEvents();
            }
            while (isAnyRnng == true);
            Global.updtActnPrcss(1);
            this.statusLoadLabel.Visible = true;
            this.statusLoadPictureBox.Visible = true;
            this.trialBalListView.Visible = false;
            System.Windows.Forms.Application.DoEvents();

            this.trialBalProgressBar.Value = 10;
            DataSet dtst = new DataSet();
            if (int.Parse(this.tbalsAcctIDTextBox.Text) <= 0)
            {
                dtst = Global.get_TrialBalance(Global.mnFrm.cmCde.Org_id, balsDate);
            }
            else
            {
                dtst = Global.get_TrialBalance(Global.mnFrm.cmCde.Org_id, balsDate, int.Parse(this.tbalsAcctIDTextBox.Text));
            }
            this.trialBalListView.Items.Clear();
            int count = dtst.Tables[0].Rows.Count;
            string funccur = Global.mnFrm.cmCde.getPssblValNm(
             Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id));
            this.trialBalGroupBox.Text = "TRIAL BALANCE AS AT " + balsDate + " (" + funccur + ")";
            double dbtsum = 0;// Global.get_COA_dbtSum(Global.mnFrm.cmCde.Org_id);
            double crdtsum = 0;// Global.get_COA_crdtSum(Global.mnFrm.cmCde.Org_id);
            int cntr = 0;
            for (int i = 0; i < count; i++)
            {
                //;
                Global.updtActnPrcss(1);
                this.trialBalProgressBar.Value = 10 + (int)(((double)i / (double)count) * 90);
                if (dtst.Tables[0].Rows[i][7].ToString() == "1")
                {
                    DataSet tDtSt;
                    if (int.Parse(this.tbalsAcctIDTextBox.Text) <= 0)
                    {
                        tDtSt = Global.get_TBals_Prnt_Accnts(int.Parse(dtst.Tables[0].Rows[i][0].ToString())
                      , this.tbalDteTextBox.Text);
                    }
                    else
                    {
                        tDtSt = Global.get_Bals_Prnt_Accnts(int.Parse(dtst.Tables[0].Rows[i][0].ToString())
                      , this.tbalDteTextBox.Text);
                    }
                    double amnt1 = 0;
                    double amnt2 = 0;
                    double amnt3 = 0;
                    if (tDtSt.Tables[0].Rows.Count > 0 && !this.shwBalsVarnCheckBox.Checked)
                    {
                        double.TryParse(tDtSt.Tables[0].Rows[0][0].ToString(), out amnt1);
                        double.TryParse(tDtSt.Tables[0].Rows[0][1].ToString(), out amnt2);
                        double.TryParse(tDtSt.Tables[0].Rows[0][2].ToString(), out amnt3);
                    }
                    if (amnt2 > amnt1)
                    {
                        amnt2 = amnt2 - amnt1;
                        amnt1 = 0;
                    }
                    else
                    {
                        amnt1 = amnt1 - amnt2;
                        amnt2 = 0;
                    }
                    ListViewItem nwItem = new ListViewItem(new string[] {
    (1 + cntr).ToString(),
                    "",dtst.Tables[0].Rows[i][1].ToString(),
          dtst.Tables[0].Rows[i][1].ToString()+"."+dtst.Tables[0].Rows[i][2].ToString().ToUpper(),
          amnt1.ToString("#,##0.00"),
    amnt2.ToString("#,##0.00"),
    amnt3.ToString("#,##0.00")
,"",dtst.Tables[0].Rows[i][0].ToString(),""});
                    if (this.smmryTBalsCheckBox.Checked == false
                      || dtst.Tables[0].Rows[i][1].ToString().Substring(0, 1) != " ")
                    {
                        nwItem.UseItemStyleForSubItems = true;
                        nwItem.BackColor = Color.WhiteSmoke;
                        nwItem.Font = new Font("Tahoma", 8.5F, FontStyle.Bold);
                    }
                    this.trialBalListView.Items.Add(nwItem);
                    cntr++;
                }
                else
                {
                    double amnt1 = 0;
                    double amnt2 = 0;
                    double amnt3 = 0;
                    double.TryParse(dtst.Tables[0].Rows[i][3].ToString(), out amnt1);
                    double.TryParse(dtst.Tables[0].Rows[i][4].ToString(), out amnt2);
                    double.TryParse(dtst.Tables[0].Rows[i][5].ToString(), out amnt3);
                    if (!this.shwBalsVarnCheckBox.Checked)
                    {
                        if (amnt2 > amnt1)
                        {
                            amnt2 = amnt2 - amnt1;
                            amnt1 = 0;
                        }
                        else
                        {
                            amnt1 = amnt1 - amnt2;
                            amnt2 = 0;
                        }
                    }
                    if (this.shwBalsVarnCheckBox.Checked)
                    {
                        double netamnt = 0;
                        if (dtst.Tables[0].Rows[i][8].ToString() == "A"
                          || dtst.Tables[0].Rows[i][8].ToString() == "EX")
                        {
                            netamnt = amnt1 - amnt2;
                        }
                        else
                        {
                            netamnt = amnt2 - amnt1;
                        }
                        //get_Accnt_TrnsSum
                        amnt1 -= Global.get_Accnt_BalsTrnsSum(int.Parse(dtst.Tables[0].Rows[i][0].ToString()),
                          "dbt_amount", balsDate);
                        amnt2 -= Global.get_Accnt_BalsTrnsSum(int.Parse(dtst.Tables[0].Rows[i][0].ToString()),
                          "crdt_amount", balsDate);
                        amnt3 -= netamnt;
                    }

                    if (amnt3 == 0 && this.hideZeroAccntCheckBox1.Checked == true)
                    {
                        continue;
                    }
                    dbtsum += amnt1;
                    crdtsum += amnt2;
                    if (this.smmryTBalsCheckBox.Checked == true)
                    {
                        continue;
                    }
                    ListViewItem nwItem = new ListViewItem(new string[] {
    (1 + cntr).ToString(),"",
    dtst.Tables[0].Rows[i][1].ToString(),
            dtst.Tables[0].Rows[i][1].ToString()+"."+dtst.Tables[0].Rows[i][2].ToString(),
    amnt1.ToString("#,##0.00"),
    amnt2.ToString("#,##0.00"),
    amnt3.ToString("#,##0.00"),
    dtst.Tables[0].Rows[i][6].ToString(),
    dtst.Tables[0].Rows[i][0].ToString(),
                    dtst.Tables[0].Rows[i][7].ToString()});
                    this.trialBalListView.Items.Add(nwItem);
                    cntr++;
                }
                System.Windows.Forms.Application.DoEvents();
            }
            ListViewItem nwItem1 = new ListViewItem(new string[] {
      "",
      "","","TOTAL = ", dbtsum.ToString("#,##0.00"), crdtsum.ToString("#,##0.00"),
            (dbtsum-crdtsum).ToString("#,##0.00"),"","",""});
            nwItem1.BackColor = Color.WhiteSmoke;
            nwItem1.Font = new Font("Tahoma", 10, FontStyle.Bold | FontStyle.Underline);
            //nwItem1.ForeColor = Color.White;
            this.trialBalListView.Items.Add(nwItem1);
            this.trialBalProgressBar.Value = 100;
            this.statusLoadLabel.Visible = false;
            this.statusLoadPictureBox.Visible = false;
            this.trialBalListView.Visible = true;
            System.Windows.Forms.Application.DoEvents();
        }

        private void populateSubledgerBals(string balsDate)
        {
            //Check if no other accounting process is running
            bool isAnyRnng = true;
            do
            {
                isAnyRnng = Global.isThereANActvActnPrcss("5", "10 second");
                System.Windows.Forms.Application.DoEvents();
            }
            while (isAnyRnng == true);
            Global.updtActnPrcss(4);
            this.statusLoadLabel.Visible = true;
            this.statusLoadPictureBox.Visible = true;
            this.subledgerListView.Visible = false;
            System.Windows.Forms.Application.DoEvents();

            this.subledgrProgressBar.Value = 10;
            DataSet dtst = Global.get_SubLdgrBalance(Global.mnFrm.cmCde.Org_id, balsDate);
            this.subledgerListView.Items.Clear();
            int count = dtst.Tables[0].Rows.Count;
            string funccur = Global.mnFrm.cmCde.getPssblValNm(
             Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id));
            this.subLdgrGroupBox.Text = "SUB-LEDGER ACCOUNT BALANCES AS AT " + balsDate + " (" + funccur + ")";
            double dbtsum = 0;// Global.get_COA_dbtSum(Global.mnFrm.cmCde.Org_id);
            double crdtsum = 0;// Global.get_COA_crdtSum(Global.mnFrm.cmCde.Org_id);
            int cntr = 0;
            for (int i = 0; i < count; i++)
            {
                //;
                Global.updtActnPrcss(4);
                this.subledgrProgressBar.Value = 10 + (int)(((double)i / (double)count) * 90);
                if (dtst.Tables[0].Rows[i][6].ToString() == "1")
                {
                    double amnt1 = 0;
                    double amnt2 = 0;
                    double amnt3 = 0;
                    double.TryParse(dtst.Tables[0].Rows[i][2].ToString(), out amnt1);
                    double.TryParse(dtst.Tables[0].Rows[i][3].ToString(), out amnt2);
                    double.TryParse(dtst.Tables[0].Rows[i][4].ToString(), out amnt3);
                    dbtsum += amnt1;
                    crdtsum += amnt2;
                    ListViewItem nwItem = new ListViewItem(new string[] {
    (1 + cntr).ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),"",
    amnt1.ToString("#,##0.00"),
    amnt2.ToString("#,##0.00"),
    amnt3.ToString("#,##0.00"),
    dtst.Tables[0].Rows[i][5].ToString(),
    dtst.Tables[0].Rows[i][0].ToString(),
                    dtst.Tables[0].Rows[i][6].ToString()});
                    nwItem.BackColor = Color.WhiteSmoke;
                    nwItem.Font = new Font("Tahoma", 8.5F, FontStyle.Bold);
                    this.subledgerListView.Items.Add(nwItem);
                    cntr++;
                }
                else
                {
                    double amnt1 = 0;
                    double amnt2 = 0;
                    double amnt3 = 0;
                    double.TryParse(dtst.Tables[0].Rows[i][2].ToString(), out amnt1);
                    double.TryParse(dtst.Tables[0].Rows[i][3].ToString(), out amnt2);
                    double.TryParse(dtst.Tables[0].Rows[i][4].ToString(), out amnt3);
                    if (amnt3 == 0 && this.hideZeroAccntCheckBox4.Checked == true)
                    {
                        continue;
                    }
                    dbtsum += amnt1;
                    crdtsum += amnt2;


                    ListViewItem nwItem = new ListViewItem(new string[] {
    (1 + cntr).ToString(),"",
    dtst.Tables[0].Rows[i][1].ToString(),
    amnt1.ToString("#,##0.00"),
    amnt2.ToString("#,##0.00"),
    amnt3.ToString("#,##0.00"),
    dtst.Tables[0].Rows[i][5].ToString(),
    dtst.Tables[0].Rows[i][0].ToString(),
                    dtst.Tables[0].Rows[i][6].ToString()});
                    this.subledgerListView.Items.Add(nwItem);
                    cntr++;
                }
                System.Windows.Forms.Application.DoEvents();
            }
            //  ListViewItem nwItem1 = new ListViewItem(new string[] {
            //"",
            //"","","TOTAL = ",	dbtsum.ToString("#,##0.00"), crdtsum.ToString("#,##0.00"),
            //  (dbtsum-crdtsum).ToString("#,##0.00"),"","",""});
            //  nwItem1.BackColor = Color.WhiteSmoke;
            //  nwItem1.Font = new Font("Tahoma", 10, FontStyle.Bold | FontStyle.Underline);
            //  //nwItem1.ForeColor = Color.White;
            //  this.subledgerListView.Items.Add(nwItem1);
            this.subledgrProgressBar.Value = 100;
            this.statusLoadLabel.Visible = false;
            this.statusLoadPictureBox.Visible = false;
            this.subledgerListView.Visible = true;
            System.Windows.Forms.Application.DoEvents();
        }

        private void populateAccntStmntBals(int accntID, string strDate, string endDate)
        {
            //Check if no other accounting process is running
            bool isAnyRnng = true;
            do
            {
                isAnyRnng = Global.isThereANActvActnPrcss("5", "10 second");
                System.Windows.Forms.Application.DoEvents();
            }
            while (isAnyRnng == true);
            Global.updtActnPrcss(4);
            this.statusLoadLabel.Visible = true;
            this.statusLoadPictureBox.Visible = true;
            this.accntStmntListView.Visible = false;
            System.Windows.Forms.Application.DoEvents();

            this.acctStmntProgressBar.Value = 10;
            DataSet dtst = Global.get_AccntStmntTransactions(accntID, strDate, endDate, true, 0, 0);
            this.accntStmntListView.Items.Clear();
            int count = dtst.Tables[0].Rows.Count;
            string funccur = Global.mnFrm.cmCde.getPssblValNm(
             Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id));
            this.accntStmntGroupBox.Text = this.accntStmntTextBox.Text.ToUpper() + "'s STATEMENT FROM " + strDate + " TO " + endDate + " (" + funccur + ")";

            double dbtsum = 0;// Global.get_COA_dbtSum(Global.mnFrm.cmCde.Org_id);
            double crdtsum = 0;// Global.get_COA_crdtSum(Global.mnFrm.cmCde.Org_id);

            string opngbalsDate = DateTime.ParseExact(
         strDate, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).AddSeconds(-1).ToString("dd-MMM-yyyy HH:mm:ss");
            string isPrnt = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_chart_of_accnts", "accnt_id", "is_prnt_accnt", accntID);
            double opngBals = 0;
            double opngDbtBals = 0;
            double opngCrdtBals = 0;
            double closngBals = 0;
            double closngDbtBals = 0;
            double closngCrdtBals = 0;
            if (isPrnt == "1")
            {
                opngBals = Global.getPrntAccntDailyBals(accntID, opngbalsDate, "net_amount");
                opngDbtBals = Global.getPrntAccntDailyBals(accntID, opngbalsDate, "dbt_amount");
                opngCrdtBals = Global.getPrntAccntDailyBals(accntID, opngbalsDate, "crdt_amount");

                closngBals = Global.getPrntAccntDailyBals(accntID, endDate, "net_amount");
                closngDbtBals = Global.getPrntAccntDailyBals(accntID, endDate, "dbt_amount");
                closngCrdtBals = Global.getPrntAccntDailyBals(accntID, endDate, "crdt_amount");

            }
            else
            {
                opngBals = Global.getAccntLstDailyNetBals(accntID, opngbalsDate);
                opngDbtBals = Global.getAccntLstDailyDbtBals(accntID, opngbalsDate);
                opngCrdtBals = Global.getAccntLstDailyCrdtBals(accntID, opngbalsDate);
                closngBals = Global.getAccntLstDailyNetBals(accntID, endDate);
                closngDbtBals = Global.getAccntLstDailyDbtBals(accntID, endDate);
                closngCrdtBals = Global.getAccntLstDailyCrdtBals(accntID, endDate);
            }

            if (opngCrdtBals >= opngDbtBals)
            {
                opngCrdtBals = opngCrdtBals - opngDbtBals;
                opngDbtBals = 0;
            }
            else
            {
                opngDbtBals = opngDbtBals - opngCrdtBals;
                opngCrdtBals = 0;
            }
            if (closngCrdtBals >= closngDbtBals)
            {
                closngCrdtBals = closngCrdtBals - closngDbtBals;
                closngDbtBals = 0;
            }
            else
            {
                closngDbtBals = closngDbtBals - closngCrdtBals;
                closngCrdtBals = 0;
            }
            ListViewItem nwItem1 = new ListViewItem(new string[] {
      "",
      "","OPENING BALANCE","",opngDbtBals.ToString("#,##0.00"),opngCrdtBals.ToString("#,##0.00"), opngBals.ToString("#,##0.00"),
        opngbalsDate,"","",""});
            nwItem1.BackColor = Color.Lime;
            nwItem1.Font = new Font("Tahoma", 10, FontStyle.Bold | FontStyle.Underline);
            //nwItem1.ForeColor = Color.White;
            this.accntStmntListView.Items.Add(nwItem1);

            for (int i = 0; i < count; i++)
            {
                //;
                Global.updtActnPrcss(4);
                this.acctStmntProgressBar.Value = 10 + (int)(((double)i / (double)count) * 90);
                double amnt1 = 0;
                double amnt2 = 0;
                double amnt3 = 0;
                double.TryParse(dtst.Tables[0].Rows[i][4].ToString(), out amnt1);
                double.TryParse(dtst.Tables[0].Rows[i][5].ToString(), out amnt2);
                double.TryParse(dtst.Tables[0].Rows[i][10].ToString(), out amnt3);

                if (amnt2 >= amnt1)
                {
                    amnt2 = amnt2 - amnt1;
                    amnt1 = 0;
                }
                else
                {
                    amnt1 = amnt1 - amnt2;
                    amnt2 = 0;
                }
                dbtsum += amnt1;
                crdtsum += amnt2;
                opngBals += amnt3;
                ListViewItem nwItem = new ListViewItem(new string[] {
    (1 + i).ToString(),
    dtst.Tables[0].Rows[i][0].ToString(),
    dtst.Tables[0].Rows[i][3].ToString(),
    dtst.Tables[0].Rows[i][23].ToString(),
    amnt1.ToString("#,##0.00"),
    amnt2.ToString("#,##0.00"),
    opngBals.ToString("#,##0.00"),
    dtst.Tables[0].Rows[i][6].ToString(),
    dtst.Tables[0].Rows[i][0].ToString(),
        dtst.Tables[0].Rows[i][1].ToString()+
        "." + dtst.Tables[0].Rows[i][2].ToString(),
    dtst.Tables[0].Rows[i][11].ToString()});
                nwItem.BackColor = Color.White;
                nwItem.Font = new Font("Tahoma", 8.25F, FontStyle.Regular);
                this.accntStmntListView.Items.Add(nwItem);


                System.Windows.Forms.Application.DoEvents();
            }
            nwItem1 = new ListViewItem(new string[] {
      "","","CLOSING BALANCE","",closngDbtBals.ToString("#,##0.00"),
      closngCrdtBals.ToString("#,##0.00"),
      closngBals.ToString("#,##0.00"),
        endDate,"","",""});
            nwItem1.BackColor = Color.Lime;
            nwItem1.Font = new Font("Tahoma", 10, FontStyle.Bold | FontStyle.Underline);
            //nwItem1.ForeColor = Color.White;
            this.accntStmntListView.Items.Add(nwItem1);

            this.acctStmntProgressBar.Value = 100;
            this.statusLoadLabel.Visible = false;
            this.statusLoadPictureBox.Visible = false;
            this.accntStmntListView.Visible = true;
            System.Windows.Forms.Application.DoEvents();

        }

        private void genRptTrialBalButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[4]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            this.genRptTrialBalButton.Enabled = false;
            System.Windows.Forms.Application.DoEvents();

            this.populateTrialBals(
                  this.tbalDteTextBox.Text);
            this.genRptTrialBalButton.Enabled = true;
            this.trialBalListView.Focus();
        }

        private void exptExclTBalMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcelSelective(this.trialBalListView, this.trialBalGroupBox.Text);
        }

        private void exptRptTrialBalButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcelSelective(this.trialBalListView, this.trialBalGroupBox.Text);
        }

        private void tbalDteButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.selectDate(ref this.tbalDteTextBox);
            if (this.tbalDteTextBox.Text.Length > 11)
            {
                this.tbalDteTextBox.Text = this.tbalDteTextBox.Text.Substring(0, 11) + " 23:59:59";
            }
        }

        private void vwTrnsTbalsMenuItem_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[2]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.trialBalListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select an Account First!", 0);
                return;
            }
            else
            {
                int accIDIn = int.Parse(this.trialBalListView.SelectedItems[0].SubItems[8].Text);
                string isPrnt = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_chart_of_accnts", "accnt_id", "(CASE WHEN is_prnt_accnt='1' THEN is_prnt_accnt ELSE has_sub_ledgers END)", accIDIn);
                if (isPrnt == "1")
                {
                    this.tbalsAcctIDTextBox.Text = accIDIn.ToString();
                    this.tbalsAcctNmTextBox.Text = Global.mnFrm.cmCde.getAccntNum(accIDIn) +
                      "." + Global.mnFrm.cmCde.getAccntName(accIDIn);
                    this.smmryTBalsCheckBox.Checked = false;
                    this.genRptTrialBalButton_Click(this.genRptTrialBalButton, e);
                }
                else
                {
                    vwTrnsctnsDiag nwDiag = new vwTrnsctnsDiag();
                    nwDiag.my_org_id = Global.mnFrm.cmCde.Org_id;
                    nwDiag.accnt_name = this.trialBalListView.SelectedItems[0].SubItems[2].Text.Trim();
                    nwDiag.accntid = int.Parse(this.trialBalListView.SelectedItems[0].SubItems[8].Text);
                    string accTyp = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_chart_of_accnts", "accnt_id", "accnt_type", accIDIn);
                    string lstDte = "01-Jan-1000 00:00:00";
                    if (accTyp == "EX" || accTyp == "R")
                    {
                        lstDte = DateTime.ParseExact(Global.mnFrm.cmCde.getLastPrdClseDate(), "dd-MMM-yyyy HH:mm:ss",
                                     System.Globalization.CultureInfo.InvariantCulture).AddSeconds(1).ToString("dd-MMM-yyyy HH:mm:ss");
                        if (lstDte == "")
                        {
                            lstDte = "01-Jan-1000 00:00:00";
                        }
                    }
                    nwDiag.dte1 = lstDte;
                    nwDiag.dte2 = this.tbalDteTextBox.Text;
                    DialogResult dgres = nwDiag.ShowDialog();
                    if (dgres == DialogResult.OK)
                    {

                    }
                }
            }
        }

        private void vwSQLTbalsMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(Global.mnFrm.tbalsSQLStmnt, 10);
        }

        private void trialBalListView_DoubleClick(object sender, System.EventArgs e)
        {
            this.vwTrnsTbalsMenuItem_Click(this.vwTrnsTbalsMenuItem, e);
        }

        private void trialBalListView_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.vwTrnsTbalsMenuItem_Click(this.vwTrnsTbalsMenuItem, ex);
            }
            else if ((e.Control && e.KeyCode == Keys.R) || e.KeyCode == Keys.F5)
            {
                if (this.genRptTrialBalButton.Enabled == true)
                {
                    this.genRptTrialBalButton_Click(this.genRptTrialBalButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                Global.mnFrm.cmCde.listViewKeyDown(this.trialBalListView, e);
            }
        }
        #endregion

        #region "RECONCILE BALANCES"

        private void addTrnsLineButton_Click(object sender, EventArgs e)
        {
            if (this.trnsDateTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please provide a Default Transaction Date First!", 0);
                return;
            }
            this.createTrnsRows(1);
        }

        public void createTrnsRows(int num)
        {
            this.obey_evnts = false;
            //this.trnsDataGridView.Columns[0].DefaultCellStyle.NullValue = "-1";
            //this.trnsDataGridView.Columns[1].DefaultCellStyle.NullValue = "";
            //this.trnsDataGridView.Columns[2].DefaultCellStyle.NullValue = "Increase";
            //this.trnsDataGridView.Columns[3].DefaultCellStyle.NullValue = "";
            //this.trnsDataGridView.Columns[4].DefaultCellStyle.NullValue = "-1";
            //this.trnsDataGridView.Columns[5].DefaultCellStyle.NullValue = "...";
            //this.trnsDataGridView.Columns[6].DefaultCellStyle.NullValue = "";
            //this.trnsDataGridView.Columns[7].DefaultCellStyle.NullValue = "0.00";
            //this.trnsDataGridView.Columns[8].DefaultCellStyle.NullValue = this.curid;
            //this.trnsDataGridView.Columns[9].DefaultCellStyle.NullValue = this.trnsDateTextBox.Text;
            //this.trnsDataGridView.Columns[10].DefaultCellStyle.NullValue = "...";

            for (int i = 0; i < num; i++)
            {
                //this.trnsDataGridView.RowCount += 1;
                //int rowIdx = this.trnsDataGridView.RowCount - 1;
                int rowIdx = this.trnsDataGridView.RowCount;
                if (this.trnsDataGridView.CurrentCell != null)
                {
                    rowIdx = this.trnsDataGridView.CurrentCell.RowIndex + 1;
                }
                this.trnsDataGridView.Rows.Insert(rowIdx, 1);
                this.trnsDataGridView.Rows[rowIdx].Cells[0].Value = "-1";
                this.trnsDataGridView.Rows[rowIdx].Cells[1].Value = "";
                this.trnsDataGridView.Rows[rowIdx].Cells[2].Value = "";
                this.trnsDataGridView.Rows[rowIdx].Cells[3].Value = "Increase";
                this.trnsDataGridView.Rows[rowIdx].Cells[4].Value = "";
                this.trnsDataGridView.Rows[rowIdx].Cells[5].Value = "-1";
                this.trnsDataGridView.Rows[rowIdx].Cells[6].Value = "...";
                this.trnsDataGridView.Rows[rowIdx].Cells[7].Value = "0.00";
                this.trnsDataGridView.Rows[rowIdx].Cells[8].Value = "...";
                this.trnsDataGridView.Rows[rowIdx].Cells[9].Value = this.curid;
                this.trnsDataGridView.Rows[rowIdx].Cells[10].Value = this.curCode;
                this.trnsDataGridView.Rows[rowIdx].Cells[11].Value = "...";
                this.trnsDataGridView.Rows[rowIdx].Cells[12].Value = this.trnsDateTextBox.Text;
                this.trnsDataGridView.Rows[rowIdx].Cells[13].Value = "...";
                this.trnsDataGridView.Rows[rowIdx].Cells[14].Value = "1.00";
                this.trnsDataGridView.Rows[rowIdx].Cells[15].Value = "1.00";
                this.trnsDataGridView.Rows[rowIdx].Cells[16].Value = "0.00";
                this.trnsDataGridView.Rows[rowIdx].Cells[17].Value = this.curCode;
                this.trnsDataGridView.Rows[rowIdx].Cells[18].Value = "0.00";
                this.trnsDataGridView.Rows[rowIdx].Cells[19].Value = this.curCode;
                this.trnsDataGridView.Rows[rowIdx].Cells[20].Value = this.curid;
                this.trnsDataGridView.Rows[rowIdx].Cells[21].Value = this.curid;
                this.trnsDataGridView.Rows[rowIdx].Cells[22].Value = -1;
            }
            for (int i = 0; i < this.trnsDataGridView.Rows.Count; i++)
            {
                this.trnsDataGridView.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }
            this.obey_evnts = true;
        }

        private void delLineButton_Click(object sender, EventArgs e)
        {
            if (this.trnsDataGridView.CurrentCell != null)
            {
                this.trnsDataGridView.Rows[this.trnsDataGridView.CurrentCell.RowIndex].Selected = true;
            }
            if (this.trnsDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the lines to be Deleted First!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Record?" +
             "\r\nThis action cannot be undone!", 1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            int slctdrows = this.trnsDataGridView.SelectedRows.Count;
            for (int i = 0; i < slctdrows; i++)
            {
                long trnsID = long.Parse(this.trnsDataGridView.Rows[this.trnsDataGridView.SelectedRows[0].Index].Cells[0].Value.ToString());
                Global.deleteTransaction(trnsID);
                this.trnsDataGridView.Rows.RemoveAt(this.trnsDataGridView.SelectedRows[0].Index);
            }
            //this.gotoButton_Click(this.gotoButton, e);
        }

        private void trnsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e == null || this.obey_evnts == false)
            {
                return;
            }
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            //if (this.trnsDataGridView.CurrentCell != null)
            //{
            //  if (e.ColumnIndex != this.trnsDataGridView.CurrentCell.ColumnIndex)
            //  {
            //    return;
            //  }
            //}
            //Global.mnFrm.cmCde.showMsg(this.srchWrd + "/" + e.RowIndex.ToString() + "/" + e.ColumnIndex.ToString(), 0);
            bool prv = this.obey_evnts;
            this.obey_evnts = false;

            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[1].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[1].Value = string.Empty;
            }
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[2].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[2].Value = string.Empty;
            }

            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[4].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[4].Value = string.Empty;
            }
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[5].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[5].Value = "-1";
            }
            //if (this.trnsDataGridView.Rows[e.RowIndex].Cells[6].Value == null)
            //{
            //  this.trnsDataGridView.Rows[e.RowIndex].Cells[6].Value = "";
            //}
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value = 0;
            }
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[9].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[9].Value = "-1";
            }
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value = "";
            }
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value = 1.00;
            }
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value = 1.00;
            }
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[16].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[16].Value = 0;
            }
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[18].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[18].Value = 0;
            }
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[20].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[20].Value = "-1";
            }
            if (e.ColumnIndex == 6)
            {

                string[] selVals = new string[1];
                selVals[0] = this.trnsDataGridView.Rows[e.RowIndex].Cells[5].Value.ToString();
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                  Global.mnFrm.cmCde.getLovID("Transaction Accounts"),
                  ref selVals, true, true, this.orgid,
                  this.srchWrd, "Both", true);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.obey_evnts = false;
                        this.trnsDataGridView.Rows[e.RowIndex].Cells[5].Value = selVals[i];
                        //this.trnsDataGridView.Rows[e.RowIndex].Cells[6].Value = 

                        int accntCurrID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
                          "accb.accb_chart_of_accnts", "accnt_id", "crncy_id", long.Parse(selVals[i])));
                        this.trnsDataGridView.Rows[e.RowIndex].Cells[19].Value = Global.mnFrm.cmCde.getPssblValNm(accntCurrID);
                        this.trnsDataGridView.Rows[e.RowIndex].Cells[20].Value = accntCurrID;
                        this.trnsDataGridView.Rows[e.RowIndex].Cells[4].Value = Global.mnFrm.cmCde.getAccntNum(int.Parse(selVals[i])) +
                  "." + Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));
                        System.Windows.Forms.Application.DoEvents();

                        string slctdCurrID = this.trnsDataGridView.Rows[e.RowIndex].Cells[9].Value.ToString();
                        this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value = Math.Round(
                  Global.get_LtstExchRate(int.Parse(slctdCurrID), this.curid,
                  this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value.ToString()), 15);
                        this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value = Math.Round(
                          Global.get_LtstExchRate(int.Parse(slctdCurrID), accntCurrID,
                  this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value.ToString()), 15);
                        System.Windows.Forms.Application.DoEvents();

                        double funcCurrRate = 0;
                        double accntCurrRate = 0;
                        double entrdAmnt = 0;
                        double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString(), out entrdAmnt);
                        double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value.ToString(), out funcCurrRate);
                        double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value.ToString(), out accntCurrRate);
                        this.trnsDataGridView.Rows[e.RowIndex].Cells[16].Value = (funcCurrRate * entrdAmnt).ToString("#,##0.00");
                        this.trnsDataGridView.Rows[e.RowIndex].Cells[18].Value = (accntCurrRate * entrdAmnt).ToString("#,##0.00");
                        System.Windows.Forms.Application.DoEvents();

                    }
                }
                //SendKeys.Send("{Tab}"); 
                //SendKeys.Send("{Tab}"); 
                this.trnsDataGridView.EndEdit();
                this.obey_evnts = true;
                this.trnsDataGridView.CurrentCell = this.trnsDataGridView.Rows[e.RowIndex].Cells[7];
            }
            else if (e.ColumnIndex == 8)
            {
                trnsAmntBreakDwnDiag nwDiag = new trnsAmntBreakDwnDiag();
                nwDiag.editMode = true;
                nwDiag.trnsaction_id = long.Parse(this.trnsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
                if (nwDiag.ShowDialog() == DialogResult.OK)
                {
                    this.trnsDataGridView.Rows[e.RowIndex].Cells[0].Value = nwDiag.trnsaction_id;

                    this.trnsDataGridView.EndEdit();
                    this.obey_evnts = true;
                    this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value = Math.Round(nwDiag.ttlNumUpDwn.Value, 2).ToString("#,##0.00");
                }
                this.trnsDataGridView.EndEdit();
                this.obey_evnts = true;
                this.trnsDataGridView.CurrentCell = this.trnsDataGridView.Rows[e.RowIndex].Cells[7];
            }
            else if (e.ColumnIndex == 11)
            {
                int[] selVals = new int[1];
                selVals[0] = int.Parse(this.trnsDataGridView.Rows[e.RowIndex].Cells[9].Value.ToString());
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                 Global.mnFrm.cmCde.getLovID("Currencies"), ref selVals,
                 true, true, this.srchWrd, "Both", true);

                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.obey_evnts = false;
                        System.Windows.Forms.Application.DoEvents();
                        this.trnsDataGridView.Rows[e.RowIndex].Cells[9].Value = selVals[i].ToString();

                        string slctdCurrID = selVals[i].ToString();
                        string accntCurrID = this.trnsDataGridView.Rows[e.RowIndex].Cells[20].Value.ToString();
                        string funcCurrID = this.trnsDataGridView.Rows[e.RowIndex].Cells[21].Value.ToString();

                        this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value = Math.Round(
                  Global.get_LtstExchRate(int.Parse(slctdCurrID), int.Parse(funcCurrID),
                  this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value.ToString()), 15);
                        this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value = Math.Round(
                          Global.get_LtstExchRate(int.Parse(slctdCurrID), int.Parse(accntCurrID),
                  this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value.ToString()), 15);
                        System.Windows.Forms.Application.DoEvents();

                        double funcCurrRate = 0;
                        double accntCurrRate = 0;
                        double entrdAmnt = 0;
                        double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString(), out entrdAmnt);
                        double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value.ToString(), out funcCurrRate);
                        double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value.ToString(), out accntCurrRate);
                        this.trnsDataGridView.Rows[e.RowIndex].Cells[16].Value = (funcCurrRate * entrdAmnt).ToString("#,##0.00");
                        this.trnsDataGridView.Rows[e.RowIndex].Cells[18].Value = (accntCurrRate * entrdAmnt).ToString("#,##0.00");
                        System.Windows.Forms.Application.DoEvents();

                        this.trnsDataGridView.EndEdit();
                        this.obey_evnts = false;
                        this.trnsDataGridView.Rows[e.RowIndex].Cells[10].Value = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
                    }
                }
                this.obey_evnts = true;
                this.trnsDataGridView.CurrentCell = this.trnsDataGridView.Rows[e.RowIndex].Cells[7];
            }
            else if (e.ColumnIndex == 13)
            {
                this.trnsDateTextBox.Text = this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value.ToString();
                Global.mnFrm.cmCde.selectDate(ref this.trnsDateTextBox);
                this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value = this.trnsDateTextBox.Text;
                this.trnsDataGridView.EndEdit();

                string slctdCurrID = this.trnsDataGridView.Rows[e.RowIndex].Cells[9].Value.ToString();
                string accntCurrID = this.trnsDataGridView.Rows[e.RowIndex].Cells[20].Value.ToString();
                string funcCurrID = this.trnsDataGridView.Rows[e.RowIndex].Cells[21].Value.ToString();

                this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value = Math.Round(
                Global.get_LtstExchRate(int.Parse(slctdCurrID), int.Parse(funcCurrID),
            this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value.ToString()), 15);
                this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value = Math.Round(
                  Global.get_LtstExchRate(int.Parse(slctdCurrID), int.Parse(accntCurrID),
            this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value.ToString()), 15);
                System.Windows.Forms.Application.DoEvents();

                double funcCurrRate = 0;
                double accntCurrRate = 0;
                double entrdAmnt = 0;
                double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString(), out entrdAmnt);
                double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value.ToString(), out funcCurrRate);
                double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value.ToString(), out accntCurrRate);
                this.trnsDataGridView.Rows[e.RowIndex].Cells[16].Value = (funcCurrRate * entrdAmnt).ToString("#,##0.00");
                this.trnsDataGridView.Rows[e.RowIndex].Cells[18].Value = (accntCurrRate * entrdAmnt).ToString("#,##0.00");
                System.Windows.Forms.Application.DoEvents();

            }

            this.obey_evnts = true;
        }

        private void gotoRcnclButton_Click(object sender, EventArgs e)
        {
            this.gotoRcnclButton.Enabled = false;
            this.trnsDataGridView.EndEdit();
            System.Windows.Forms.Application.DoEvents();
            double ttlDebits = 0;
            double ttlCredits = 0;
            this.trnsDataGridView.EndEdit();
            System.Windows.Forms.Application.DoEvents();
            for (int i = 0; i < this.trnsDataGridView.Rows.Count; i++)
            {
                if (this.trnsDataGridView.Rows[i].Cells[1].Value == null)
                {
                    this.trnsDataGridView.Rows[i].Cells[1].Value = string.Empty;
                }
                if (this.trnsDataGridView.Rows[i].Cells[2].Value == null)
                {
                    this.trnsDataGridView.Rows[i].Cells[2].Value = string.Empty;
                }

                if (this.trnsDataGridView.Rows[i].Cells[3].Value == null)
                {
                    this.trnsDataGridView.Rows[i].Cells[3].Value = "Increase";
                }

                if (this.trnsDataGridView.Rows[i].Cells[4].Value == null)
                {
                    this.trnsDataGridView.Rows[i].Cells[4].Value = string.Empty;
                }
                if (this.trnsDataGridView.Rows[i].Cells[5].Value == null)
                {
                    this.trnsDataGridView.Rows[i].Cells[5].Value = "-1";
                }
                //if (this.trnsDataGridView.Rows[i].Cells[6].Value == null)
                //{
                //  this.trnsDataGridView.Rows[i].Cells[6].Value = "";
                //}
                if (this.trnsDataGridView.Rows[i].Cells[7].Value == null)
                {
                    this.trnsDataGridView.Rows[i].Cells[7].Value = "0.00";
                }
                if (this.trnsDataGridView.Rows[i].Cells[16].Value == null)
                {
                    this.trnsDataGridView.Rows[i].Cells[16].Value = "0.00";
                }
                if (this.trnsDataGridView.Rows[i].Cells[18].Value == null)
                {
                    this.trnsDataGridView.Rows[i].Cells[18].Value = "0.00";
                }
                if (this.trnsDataGridView.Rows[i].Cells[10].Value == null)
                {
                    this.trnsDataGridView.Rows[i].Cells[10].Value = "";
                }
                int accntid = -1;
                int.TryParse(this.trnsDataGridView.Rows[i].Cells[5].Value.ToString(), out accntid);
                double lnAmnt = 0;
                double accntAmnt = 0;
                double.TryParse(this.trnsDataGridView.Rows[i].Cells[16].Value.ToString(), out lnAmnt);
                double.TryParse(this.trnsDataGridView.Rows[i].Cells[18].Value.ToString(), out accntAmnt);

                string lnDte = this.trnsDataGridView.Rows[i].Cells[10].Value.ToString();
                string incrsdcrs = this.trnsDataGridView.Rows[i].Cells[3].Value.ToString().Substring(0, 1);
                string lneDesc = this.trnsDataGridView.Rows[i].Cells[1].Value.ToString();
                //&& (lnAmnt != 0 || accntAmnt != 0)
                if (accntid > 0 && incrsdcrs != "" && lneDesc != "")
                {

                    double netAmnt = (double)Global.dbtOrCrdtAccntMultiplier(accntid,
               incrsdcrs) * (double)lnAmnt;

                    //if (!Global.mnFrm.cmCde.isTransPrmttd(accntid, lnDte, netAmnt))
                    //{
                    //  return;
                    //}

                    //if (Global.mnFrm.cmCde.showMsg("Are you sure you want to Create this Transaction!", 1) == DialogResult.No)
                    //  {
                    //  Global.mnFrm.cmCde.showMsg("Transaction Cancelled!", 0);
                    //  return;
                    //  }
                    if (Global.dbtOrCrdtAccnt(accntid,
                      incrsdcrs) == "Debit")
                    {
                        ttlDebits += lnAmnt;
                    }
                    else
                    {
                        ttlCredits += lnAmnt;
                    }
                    this.trnsDataGridView.Rows[i].Cells[16].Style.BackColor = Color.Lime;
                    this.trnsDataGridView.Rows[i].Cells[18].Style.BackColor = Color.Lime;
                }
                else
                {
                    this.trnsDataGridView.Rows[i].Cells[16].Style.BackColor = Color.FromArgb(255, 255, 128);
                    this.trnsDataGridView.Rows[i].Cells[18].Style.BackColor = Color.FromArgb(255, 255, 128);
                }
                System.Windows.Forms.Application.DoEvents();
            }
            this.ttlDebitsRcnclLabel.Text = ttlDebits.ToString("#,##0.00");
            this.ttlCreditsRcnclLabel.Text = ttlCredits.ToString("#,##0.00");
            this.netBalanceRcnclLabel.Text = Math.Abs(ttlCredits - ttlDebits).ToString("#,##0.00");
            if (ttlCredits.ToString("#,##0.00") == ttlDebits.ToString("#,##0.00"))
            {
                this.ttlCreditsRcnclLabel.BackColor = Color.Green;
                this.ttlDebitsRcnclLabel.BackColor = Color.Green;
            }
            else
            {
                this.ttlCreditsRcnclLabel.BackColor = Color.Red;
                this.ttlDebitsRcnclLabel.BackColor = Color.Red;
            }
            this.gotoRcnclButton.Enabled = true;
        }

        private void trnsDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e == null || this.obey_evnts == false)
            {
                return;
            }

            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            bool prv = this.obey_evnts;
            this.obey_evnts = false;

            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[1].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[1].Value = string.Empty;
            }
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[2].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[2].Value = string.Empty;
            }

            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[4].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[4].Value = string.Empty;
            }
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[5].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[5].Value = "-1";
            }
            //if (this.trnsDataGridView.Rows[e.RowIndex].Cells[6].Value == null)
            //{
            //  this.trnsDataGridView.Rows[e.RowIndex].Cells[6].Value = "";
            //}
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value = 0;
            }
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[9].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[9].Value = "-1";
            }
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value = "";
            }
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value = 1.00;
            }
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value = 1.00;
            }
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[16].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[16].Value = 0;
            }
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[18].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[18].Value = 0;
            }
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[20].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[20].Value = "-1";
            }
            //System.Windows.Forms.Application.DoEvents();
            if (e.ColumnIndex == 4)
            {
                this.srchWrd = this.trnsDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
                if (!this.srchWrd.Contains("%"))
                {
                    this.srchWrd = "%" + this.srchWrd.Replace(" ", "%") + "%";
                }
                this.trnsDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
                this.obey_evnts = true;
                DataGridViewCellEventArgs e1 = new DataGridViewCellEventArgs(6, e.RowIndex);
                this.trnsDataGridView_CellContentClick(this.trnsDataGridView, e1);
                this.srchWrd = "%";
                //Global.mnFrm.cmCde.showMsg(this.srchWrd, 0);
            }
            else if (e.ColumnIndex == 10)
            {
                this.srchWrd = this.trnsDataGridView.Rows[e.RowIndex].Cells[10].Value.ToString();
                if (!this.srchWrd.Contains("%"))
                {
                    this.srchWrd = "%" + this.srchWrd.Replace(" ", "%") + "%";
                }

                this.trnsDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
                this.obey_evnts = true;
                DataGridViewCellEventArgs e1 = new DataGridViewCellEventArgs(11, e.RowIndex);
                this.trnsDataGridView_CellContentClick(this.trnsDataGridView, e1);
                this.srchWrd = "%";
            }
            else if (e.ColumnIndex == 12)
            {
                DateTime dte1 = DateTime.Now;
                bool sccs = DateTime.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value.ToString(), out dte1);
                if (!sccs)
                {
                    dte1 = DateTime.Now;
                }
                this.trnsDataGridView.EndEdit();
                this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value = dte1.ToString("dd-MMM-yyyy HH:mm:ss");

                string slctdCurrID = this.trnsDataGridView.Rows[e.RowIndex].Cells[9].Value.ToString();
                string accntCurrID = this.trnsDataGridView.Rows[e.RowIndex].Cells[20].Value.ToString();
                string funcCurrID = this.trnsDataGridView.Rows[e.RowIndex].Cells[21].Value.ToString();

                this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value = Math.Round(
                Global.get_LtstExchRate(int.Parse(slctdCurrID), int.Parse(funcCurrID),
            this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value.ToString()), 15);
                this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value = Math.Round(
                  Global.get_LtstExchRate(int.Parse(slctdCurrID), int.Parse(accntCurrID),
            this.trnsDataGridView.Rows[e.RowIndex].Cells[12].Value.ToString()), 15);
                System.Windows.Forms.Application.DoEvents();

                double funcCurrRate = 0;
                double accntCurrRate = 0;
                double entrdAmnt = 0;
                double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString(), out entrdAmnt);
                double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value.ToString(), out funcCurrRate);
                double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value.ToString(), out accntCurrRate);
                this.trnsDataGridView.Rows[e.RowIndex].Cells[16].Value = (funcCurrRate * entrdAmnt).ToString("#,##0.00");
                this.trnsDataGridView.Rows[e.RowIndex].Cells[18].Value = (accntCurrRate * entrdAmnt).ToString("#,##0.00");
                System.Windows.Forms.Application.DoEvents();
            }
            else if (e.ColumnIndex == 14)
            {
                double lnAmnt = 0;
                string orgnlAmnt = this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value.ToString();
                bool isno = double.TryParse(orgnlAmnt, out lnAmnt);
                if (isno == false)
                {
                    lnAmnt = Math.Round(Global.computeMathExprsn(orgnlAmnt), 15);
                }
                this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value = Math.Round(lnAmnt, 15);
                double entrdAmnt = 0;
                double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString(), out entrdAmnt);
                this.trnsDataGridView.Rows[e.RowIndex].Cells[16].Value = (entrdAmnt * lnAmnt).ToString("#,##0.00");
            }
            else if (e.ColumnIndex == 15)
            {
                double lnAmnt = 0;
                string orgnlAmnt = this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value.ToString();
                bool isno = double.TryParse(orgnlAmnt, out lnAmnt);
                if (isno == false)
                {
                    lnAmnt = Math.Round(Global.computeMathExprsn(orgnlAmnt), 15);
                }
                this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value = Math.Round(lnAmnt, 15);

                double entrdAmnt = 0;
                double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString(), out entrdAmnt);
                this.trnsDataGridView.Rows[e.RowIndex].Cells[18].Value = (entrdAmnt * lnAmnt).ToString("#,##0.00");

            }
            else if (e.ColumnIndex == 7)
            {
                double lnAmnt = 0;

                string orgnlAmnt = this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString();
                bool isno = double.TryParse(orgnlAmnt, out lnAmnt);
                if (isno == false)
                {
                    lnAmnt = Math.Round(Global.computeMathExprsn(orgnlAmnt), 2);
                }
                this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value = lnAmnt.ToString("#,##0.00");

                double funcCurrRate = 0;
                double accntCurrRate = 0;

                double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value.ToString(), out funcCurrRate);
                double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value.ToString(), out accntCurrRate);

                this.trnsDataGridView.Rows[e.RowIndex].Cells[16].Value = (funcCurrRate * lnAmnt).ToString("#,##0.00");
                this.trnsDataGridView.Rows[e.RowIndex].Cells[18].Value = (accntCurrRate * lnAmnt).ToString("#,##0.00");

                if (e.RowIndex == this.trnsDataGridView.Rows.Count - 1)
                {
                    this.addTrnsLineButton.PerformClick();
                }

            }

            this.obey_evnts = true;
        }

        private void trnsDataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e == null || this.obey_evnts == false)
            {
                return;
            }
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            bool prv = this.obey_evnts;
            this.obey_evnts = false;
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[1].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[1].Value = string.Empty;
            }
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[2].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[2].Value = string.Empty;
            }

            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[4].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[4].Value = string.Empty;
            }
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[5].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[5].Value = "-1";
            }
            //if (this.trnsDataGridView.Rows[e.RowIndex].Cells[6].Value == null)
            //{
            //  this.trnsDataGridView.Rows[e.RowIndex].Cells[6].Value = "";
            //}
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value = 0;
            }
            if (this.trnsDataGridView.Rows[e.RowIndex].Cells[10].Value == null)
            {
                this.trnsDataGridView.Rows[e.RowIndex].Cells[10].Value = "";
            }
            if (e.ColumnIndex == 7)
            {
                //int acntID = int.Parse(this.trnsDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString());
                //this.trnsDataGridView.Rows[e.RowIndex].Cells[3].Value = Global.mnFrm.cmCde.getAccntNum(acntID) +
                //"." + Global.mnFrm.cmCde.getAccntName(acntID);

                //int entrdCurrID = int.Parse(this.trnsDataGridView.Rows[e.RowIndex].Cells[8].Value.ToString());
                //this.trnsDataGridView.Rows[e.RowIndex].Cells[9].Value = Global.mnFrm.cmCde.getPssblValNm(entrdCurrID);

                double lnAmnt = 0;
                string orgnlAmnt = this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString();
                bool isno = double.TryParse(orgnlAmnt, out lnAmnt);
                if (isno == false)
                {
                    lnAmnt = Math.Round(Global.computeMathExprsn(orgnlAmnt), 2);
                }
                this.trnsDataGridView.Rows[e.RowIndex].Cells[7].Value = lnAmnt.ToString("#,##0.00");

                double funcCurrRate = 0;
                double accntCurrRate = 0;

                double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[14].Value.ToString(), out funcCurrRate);
                double.TryParse(this.trnsDataGridView.Rows[e.RowIndex].Cells[15].Value.ToString(), out accntCurrRate);

                this.trnsDataGridView.Rows[e.RowIndex].Cells[16].Value = (funcCurrRate * lnAmnt).ToString("#,##0.00");
                this.trnsDataGridView.Rows[e.RowIndex].Cells[18].Value = (accntCurrRate * lnAmnt).ToString("#,##0.00");
                this.trnsDataGridView.BeginEdit(true);
            }
            else if (e.ColumnIndex == 4 || e.ColumnIndex == 6)
            {

                //int acntID = int.Parse(this.trnsDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString());
                //this.trnsDataGridView.Rows[e.RowIndex].Cells[3].Value = Global.mnFrm.cmCde.getAccntNum(acntID) +
                //"." + Global.mnFrm.cmCde.getAccntName(acntID);
                this.trnsDataGridView.BeginEdit(true);
            }
            else if (e.ColumnIndex == 10 || e.ColumnIndex == 11 || e.ColumnIndex == 12)
            {
                //int entrdCurrID = int.Parse(this.trnsDataGridView.Rows[e.RowIndex].Cells[8].Value.ToString());
                //this.trnsDataGridView.Rows[e.RowIndex].Cells[9].Value = Global.mnFrm.cmCde.getPssblValNm(entrdCurrID);
                this.trnsDataGridView.BeginEdit(true);
            }
            else// if (e.ColumnIndex == 1)
            {
                this.trnsDataGridView.BeginEdit(false);
                //this.trnsDataGridView.Rows[e.RowIndex].Cells[1].Selected = false;
            }

            this.obey_evnts = true;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            if (this.trnsDataGridView.Rows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please create transactions First!", 0);
                this.saveTrnsBatchButton.Enabled = true;
                return;
            }
            this.saveTrnsBatchRcnclButton.Enabled = false;
            this.gotoRcnclButton_Click(this.gotoRcnclButton, e);
            this.createBatch();
            if (this.batchid <= 0
              || Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_trnsctn_batches", "batch_id", "batch_status", this.batchid) == "1")
            {
                Global.mnFrm.cmCde.showMsg("Please select an Unposted Transactions Batch First!", 0);
                this.saveTrnsBatchRcnclButton.Enabled = true;
                return;
            }
            if (this.ttlCreditsRcnclLabel.Text != this.ttlDebitsRcnclLabel.Text)
            {
                if (Global.mnFrm.cmCde.showMsg("These transactions are not balanced! \r\nAre you sure you want to Create them Anyway?", 1) == DialogResult.No)
                {
                    //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 0);
                    this.saveTrnsBatchRcnclButton.Enabled = true;
                    this.saveTrnsBatchRcnclButton.Enabled = true;
                    return;
                }
            }
            //this.waitLabel1.Visible = true;
            for (int i = 0; i < this.trnsDataGridView.Rows.Count; i++)
            {
                if (this.trnsDataGridView.Rows[i].Cells[1].Value == null)
                {
                    this.trnsDataGridView.Rows[i].Cells[1].Value = string.Empty;
                }
                if (this.trnsDataGridView.Rows[i].Cells[2].Value == null)
                {
                    this.trnsDataGridView.Rows[i].Cells[2].Value = string.Empty;
                }
                if (this.trnsDataGridView.Rows[i].Cells[3].Value == null)
                {
                    this.trnsDataGridView.Rows[i].Cells[3].Value = "Increase";
                }
                if (this.trnsDataGridView.Rows[i].Cells[4].Value == null)
                {
                    this.trnsDataGridView.Rows[i].Cells[4].Value = string.Empty;
                }
                if (this.trnsDataGridView.Rows[i].Cells[5].Value == null)
                {
                    this.trnsDataGridView.Rows[i].Cells[5].Value = "-1";
                }
                //if (this.trnsDataGridView.Rows[i].Cells[6].Value == null)
                //{
                //  this.trnsDataGridView.Rows[i].Cells[6].Value = "";
                //}
                if (this.trnsDataGridView.Rows[i].Cells[7].Value == null)
                {
                    this.trnsDataGridView.Rows[i].Cells[7].Value = "0.00";
                }
                if (this.trnsDataGridView.Rows[i].Cells[16].Value == null)
                {
                    this.trnsDataGridView.Rows[i].Cells[16].Value = "0.00";
                }
                if (this.trnsDataGridView.Rows[i].Cells[18].Value == null)
                {
                    this.trnsDataGridView.Rows[i].Cells[18].Value = "0.00";
                }
                if (this.trnsDataGridView.Rows[i].Cells[10].Value == null)
                {
                    this.trnsDataGridView.Rows[i].Cells[10].Value = "";
                }
                if (this.trnsDataGridView.Rows[i].Cells[22].Value == null)
                {
                    this.trnsDataGridView.Rows[i].Cells[22].Value = -1;
                }
                int accntid = -1;
                int.TryParse(this.trnsDataGridView.Rows[i].Cells[5].Value.ToString(), out accntid);
                double lnAmnt = 0;
                double.TryParse(this.trnsDataGridView.Rows[i].Cells[16].Value.ToString(), out lnAmnt);
                double acntAmnt = 0;
                double.TryParse(this.trnsDataGridView.Rows[i].Cells[17].Value.ToString(), out acntAmnt);
                double entrdAmnt = 0;
                double.TryParse(this.trnsDataGridView.Rows[i].Cells[7].Value.ToString(), out entrdAmnt);

                string lnDte = this.trnsDataGridView.Rows[i].Cells[12].Value.ToString();
                string incrsdcrs = this.trnsDataGridView.Rows[i].Cells[3].Value.ToString().Substring(0, 1);
                string lneDesc = this.trnsDataGridView.Rows[i].Cells[1].Value.ToString();
                //&& (lnAmnt != 0 || acntAmnt != 0)
                if (accntid > 0 && incrsdcrs != "" && lneDesc != "")
                {
                    //        double netAmnt = (double)Global.dbtOrCrdtAccntMultiplier(accntid,
                    //incrsdcrs) * (double)lnAmnt;

                    //        if (!Global.mnFrm.cmCde.isTransPrmttd(accntid, lnDte, netAmnt))
                    //        {
                    //          this.waitLabel1.Visible = false;
                    //          return;
                    //        }
                }
                else
                {
                }
                System.Windows.Forms.Application.DoEvents();
            }

            for (int i = 0; i < this.trnsDataGridView.Rows.Count; i++)
            {
                System.Windows.Forms.Application.DoEvents();
                int accntid = -1;
                int.TryParse(this.trnsDataGridView.Rows[i].Cells[5].Value.ToString(), out accntid);
                long trnsid = -1;
                long.TryParse(this.trnsDataGridView.Rows[i].Cells[0].Value.ToString(), out trnsid);
                long srctrnsid = -1;
                long.TryParse(this.trnsDataGridView.Rows[i].Cells[22].Value.ToString(), out srctrnsid);
                double lnAmnt = 0;
                double.TryParse(this.trnsDataGridView.Rows[i].Cells[16].Value.ToString(), out lnAmnt);

                double acntAmnt = 0;
                double.TryParse(this.trnsDataGridView.Rows[i].Cells[18].Value.ToString(), out acntAmnt);
                double entrdAmnt = 0;
                double.TryParse(this.trnsDataGridView.Rows[i].Cells[7].Value.ToString(), out entrdAmnt);

                string lnDte = this.trnsDataGridView.Rows[i].Cells[12].Value.ToString();
                string incrsdcrs = this.trnsDataGridView.Rows[i].Cells[3].Value.ToString().Substring(0, 1);
                string lneDesc = this.trnsDataGridView.Rows[i].Cells[1].Value.ToString();
                string refDocNum = this.trnsDataGridView.Rows[i].Cells[2].Value.ToString();
                if (lneDesc.Length > 499)
                {
                    lneDesc = lneDesc.Substring(0, 499);
                }
                int entrdCurrID = int.Parse(this.trnsDataGridView.Rows[i].Cells[9].Value.ToString());
                int funcCurrID = int.Parse(this.trnsDataGridView.Rows[i].Cells[21].Value.ToString());
                int accntCurrID = int.Parse(this.trnsDataGridView.Rows[i].Cells[20].Value.ToString());
                double funcCurrRate = double.Parse(this.trnsDataGridView.Rows[i].Cells[14].Value.ToString());
                double accntCurrRate = double.Parse(this.trnsDataGridView.Rows[i].Cells[15].Value.ToString());
                //(lnAmnt != 0 || acntAmnt != 0) &&
                if (accntid > 0 && incrsdcrs != "" && lneDesc != "")
                {
                    double netAmnt = (double)Global.dbtOrCrdtAccntMultiplier(accntid,
               incrsdcrs) * (double)lnAmnt;

                    if (Global.dbtOrCrdtAccnt(accntid,
                      incrsdcrs) == "Debit")
                    {
                        if (trnsid <= 0)
                        {
                            long oldtrnsid = trnsid;
                            trnsid = Global.getNewTrnsID();
                            Global.createTransaction(trnsid, accntid,
                              lneDesc, lnAmnt,
                              lnDte, funcCurrID, this.batchid, 0.00,
                              netAmnt, entrdAmnt, entrdCurrID, acntAmnt, accntCurrID, funcCurrRate, accntCurrRate, "D", refDocNum, srctrnsid);
                            Global.updateAmntBrkDwn(oldtrnsid, trnsid);
                            this.trnsDataGridView.Rows[i].Cells[0].Value = trnsid;
                        }
                        else
                        {
                            Global.updateTransaction(accntid,
                     lneDesc, lnAmnt,
                     lnDte, funcCurrID,
                     this.batchid, 0.00, netAmnt, trnsid,
                     entrdAmnt, entrdCurrID, acntAmnt, accntCurrID, funcCurrRate, accntCurrRate, "D", refDocNum, srctrnsid);
                        }
                    }
                    else
                    {
                        if (trnsid <= 0)
                        {
                            long oldtrnsid = trnsid;
                            trnsid = Global.getNewTrnsID();

                            Global.createTransaction(trnsid, accntid,
                            lneDesc, 0.00,
                            lnDte, funcCurrID,
                            this.batchid, lnAmnt, netAmnt,
                     entrdAmnt, entrdCurrID, acntAmnt, accntCurrID, funcCurrRate, accntCurrRate, "C", refDocNum, srctrnsid);
                            Global.updateAmntBrkDwn(oldtrnsid, trnsid);
                            this.trnsDataGridView.Rows[i].Cells[0].Value = trnsid;
                        }
                        else
                        {
                            Global.updateTransaction(accntid,
                     lneDesc, 0.00,
                              lnDte
                              , funcCurrID,
                     this.batchid, lnAmnt, netAmnt,
                     trnsid,
                     entrdAmnt, entrdCurrID, acntAmnt, accntCurrID, funcCurrRate, accntCurrRate, "C", refDocNum, srctrnsid);
                        }
                    }
                }
            }

            this.trnsDataGridView.EndEdit();
            this.waitLabel1.Visible = false;
            this.saveTrnsBatchRcnclButton.Enabled = true;
            this.saveTrnsBatchRcnclButton.Enabled = true;

            if (this.batchid < 1)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Transaction Batch First!", 0);
                return;
            }
            string btchN = this.batchNmRcnclTextBox.Text;
            Global.mnFrm.searchForTrnsTextBox.Text = btchN;
            Global.mnFrm.searchInTrnsComboBox.SelectedItem = "Batch Name";
            Global.mnFrm.loadCorrectPanel("Journal Entries");
            Global.mnFrm.showUnpostedCheckBox.Checked = false;
            if (Global.mnFrm.shwMyBatchesCheckBox.Enabled == true)
            {
                Global.mnFrm.shwMyBatchesCheckBox.Checked = false;
            }
            Global.mnFrm.rfrshTrnsButton.PerformClick();
        }

        private void saveTrnsBatchRcnclButton_Click(object sender, EventArgs e)
        {
            this.OKButton_Click(this.saveTrnsBatchRcnclButton, e);
        }

        private void createBatch()
        {
            if (this.batchid > 0)
            {
                this.batchNmRcnclTextBox.Text = Global.getBatchNm(this.batchid);
                if (this.batchNmRcnclTextBox.Text == "")
                {
                    //do nothing
                }
                else
                {
                    return;
                }
            }
            string initl = Global.mnFrm.cmCde.getUsername(Global.myBscActn.user_id).ToUpper();
            if (initl.Length > 4)
            {
                initl = initl.Substring(0, 4);
            }
            string dte = DateTime.Parse(Global.mnFrm.cmCde.getFrmtdDB_Date_time()).ToString("yyMMdd");
            this.batchNmRcnclTextBox.Text = initl + "-RCNCL-" + dte
              + "-" + Global.mnFrm.cmCde.getRandomInt(100, 1000)
                      + "-" + (Global.mnFrm.cmCde.getRecCount("accb.accb_trnsctn_batches", "batch_name",
                      "batch_id", initl + "-" + dte + "-%") + 1).ToString().PadLeft(3, '0');
            if (this.batchNmRcnclTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please enter a Batch Name!", 0);
                return;
            }
            long oldBatchID = Global.mnFrm.cmCde.getTrnsBatchID(this.batchNmRcnclTextBox.Text,
              Global.mnFrm.cmCde.Org_id);
            if (oldBatchID > 0)
            {
                Global.mnFrm.cmCde.showMsg("Batch Name is already in use in this Organization!", 0);
                return;
            }

            Global.createBatch(Global.mnFrm.cmCde.Org_id,
             this.batchNmRcnclTextBox.Text, "Reconciliation Done on " + Global.mnFrm.cmCde.getFrmtdDB_Date_time(),
             "Manual",
             "VALID", -1, "0");
            System.Windows.Forms.Application.DoEvents();
            this.batchid = Global.getBatchID(this.batchNmRcnclTextBox.Text, Global.mnFrm.cmCde.Org_id);
        }

        private void refreshRcnclButton_Click(object sender, EventArgs e)
        {
            //this.gotoRcnclButton_Click(this.gotoRcnclButton, e);
            if (this.batchid < 1)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Transaction Batch First!", 0);
                return;
            }
            string btchN = this.batchNmRcnclTextBox.Text;
            Global.mnFrm.searchForTrnsTextBox.Text = btchN;
            Global.mnFrm.searchInTrnsComboBox.SelectedItem = "Batch Name";
            Global.mnFrm.loadCorrectPanel("Journal Entries");
            Global.mnFrm.showUnpostedCheckBox.Checked = false;
            if (Global.mnFrm.shwMyBatchesCheckBox.Enabled == true)
            {
                Global.mnFrm.shwMyBatchesCheckBox.Checked = false;
            }
            Global.mnFrm.rfrshTrnsButton.PerformClick();
        }

        private void trnsDataGridView_CurrentCellChanged(object sender, EventArgs e)
        {

            if (this.trnsDataGridView.CurrentCell == null || this.obey_evnts == false)
            {
                return;
            }
            int rwidx = this.trnsDataGridView.CurrentCell.RowIndex;
            int colidx = this.trnsDataGridView.CurrentCell.ColumnIndex;

            if (rwidx < 0 || colidx < 0)
            {
                return;
            }
            bool prv = this.obey_evnts;
            this.obey_evnts = false;
            if (this.trnsDataGridView.Rows[rwidx].Cells[1].Value == null)
            {
                this.trnsDataGridView.Rows[rwidx].Cells[1].Value = string.Empty;
            }
            if (this.trnsDataGridView.Rows[rwidx].Cells[2].Value == null)
            {
                this.trnsDataGridView.Rows[rwidx].Cells[2].Value = string.Empty;
            }

            if (this.trnsDataGridView.Rows[rwidx].Cells[4].Value == null)
            {
                this.trnsDataGridView.Rows[rwidx].Cells[4].Value = string.Empty;
            }
            if (this.trnsDataGridView.Rows[rwidx].Cells[5].Value == null)
            {
                this.trnsDataGridView.Rows[rwidx].Cells[5].Value = "-1";
            }
            //if (this.trnsDataGridView.Rows[rwidx].Cells[6].Value == null)
            //{
            //  this.trnsDataGridView.Rows[rwidx].Cells[6].Value = "";
            //}
            if (this.trnsDataGridView.Rows[rwidx].Cells[7].Value == null)
            {
                this.trnsDataGridView.Rows[rwidx].Cells[7].Value = 0;
            }
            if (this.trnsDataGridView.Rows[rwidx].Cells[9].Value == null)
            {
                this.trnsDataGridView.Rows[rwidx].Cells[9].Value = "-1";
            }
            if (this.trnsDataGridView.Rows[rwidx].Cells[10].Value == null)
            {
                this.trnsDataGridView.Rows[rwidx].Cells[10].Value = "";
            }
            //if (colidx == 7)
            //{
            //  this.obey_evnts = false;
            //  this.trnsDataGridView.CurrentCell = this.trnsDataGridView.Rows[e.RowIndex].Cells[7];
            //}
            if (colidx >= 0)
            {
                //int acntID = int.Parse(this.trnsDataGridView.Rows[rwidx].Cells[5].Value.ToString());
                //this.trnsDataGridView.Rows[rwidx].Cells[4].Value = Global.mnFrm.cmCde.getAccntNum(acntID) +
                //"." + Global.mnFrm.cmCde.getAccntName(acntID);

                //int entrdCurrID = int.Parse(this.trnsDataGridView.Rows[rwidx].Cells[9].Value.ToString());
                //this.trnsDataGridView.Rows[rwidx].Cells[10].Value = Global.mnFrm.cmCde.getPssblValNm(entrdCurrID);

            }

            this.obey_evnts = true;
        }

        private void trnsDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            this.trnsDataGridView.EndEdit();
            System.Windows.Forms.Application.DoEvents();
            this.addTrnsLstDiag_KeyDown(this, e);
        }

        private void addTrnsLstDiag_KeyDown(object sender, KeyEventArgs e)
        {
            this.tabControl1.SelectedTab = this.tabPage3;
            EventArgs ex = new EventArgs();
            if (e.Control && e.KeyCode == Keys.S)       // Ctrl-S Save
            {
                // do what you want here
                this.saveTrnsBatchRcnclButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if (e.Control && e.KeyCode == Keys.N)       // Ctrl-S Save
            {
                // do what you want here
                this.addTrnsLineButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if (e.Control && e.KeyCode == Keys.E)       // Ctrl-S Save
            {
                // do what you want here
                //this.editButton.PerformClick();
                e.Handled = false;
                e.SuppressKeyPress = false;  // stops bing! also sets handeled which stop event bubbling
            }
            else if (e.Control && e.KeyCode == Keys.R)       // Ctrl-S Save
            {
                // do what you want here
                this.openBatchButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else
            {
                e.Handled = false;
                e.SuppressKeyPress = false;  // stops bing! also sets handeled which stop event bubbling
            }
        }

        private void autoBalanceButton_Click(object sender, EventArgs e)
        {
            this.gotoRcnclButton.PerformClick();
            double ttldiff = double.Parse(this.netBalanceRcnclLabel.Text);
            if (ttldiff == 0)
            {
                Global.mnFrm.cmCde.showMsg("Transactions are already Balanced", 0);
                return;
            }
            if (this.trnsDataGridView.Rows.Count > 0)
            {
                this.trnsDataGridView.CurrentCell = this.trnsDataGridView.Rows[this.trnsDataGridView.Rows.Count - 1].Cells[1];
            }
            int rowIdx = this.trnsDataGridView.CurrentCell.RowIndex;
            if (this.trnsDataGridView.Rows[rowIdx].Cells[1].Value.ToString() != "")
            {
                this.addTrnsLineButton.PerformClick();
            }
            if (this.trnsDataGridView.Rows.Count > 0)
            {
                this.trnsDataGridView.CurrentCell = this.trnsDataGridView.Rows[this.trnsDataGridView.Rows.Count - 1].Cells[1];
            }
            double tllDbt = double.Parse(this.ttlDebitsRcnclLabel.Text);
            double tllCrdt = double.Parse(this.ttlCreditsRcnclLabel.Text);
            string incrsDcrs = "Decrease";
            if (tllDbt > tllCrdt)
            {
                incrsDcrs = "Increase";
            }
            int acntID = Global.get_RetEarn_Accnt(Global.mnFrm.cmCde.Org_id);
            rowIdx = this.trnsDataGridView.CurrentCell.RowIndex;
            string trnsDesc = "";
            long trnsaction_id = -1;
            int pssblvalid = -1;
            string lneDesc = "";
            double qty = 1;
            double unitAmnt = 0;
            double lnAmnt = 0;
            long trnsdetid = -1;
            trnsaction_id = -1 * long.Parse(Global.mnFrm.cmCde.getDB_Date_time().Replace("-", "").Replace(":", "").Replace(" ", ""));
            string refDocNums = "";
            for (int i = 0; i < this.trnsDataGridView.Rows.Count; i++)
            {
                int accntid = -1;
                int.TryParse(this.trnsDataGridView.Rows[i].Cells[5].Value.ToString(), out accntid);
                string acntType = Global.mnFrm.cmCde.getAccntType(accntid);
                trnsdetid = Global.getNewAmntBrkDwnID();
                lneDesc = this.trnsDataGridView.Rows[i].Cells[1].Value.ToString();
                string refDocNum = this.trnsDataGridView.Rows[i].Cells[2].Value.ToString();
                double.TryParse(this.trnsDataGridView.Rows[i].Cells[7].Value.ToString(), out unitAmnt);
                if (lneDesc != "")
                {
                    trnsDesc += ": " + lneDesc;
                    refDocNums += ": " + refDocNum;
                    lnAmnt = unitAmnt;
                    qty = Global.dbtOrCrdtAccntMultiplier(accntid,
               this.trnsDataGridView.Rows[i].Cells[3].Value.ToString().Substring(0, 1));
                    //
                    //Global.mnFrm.cmCde.isAccntContra(accntid) == "1"
                    if (((acntType == "A" || acntType == "EX")
                      && incrsDcrs == "Increase")
                      || ((acntType == "R" || acntType == "EQ" || acntType == "L")
                      && incrsDcrs == "Decrease")
                      || (Global.mnFrm.cmCde.isAccntContra(accntid) == "1"
                     && incrsDcrs == "Decrease"))
                    {
                        qty = -1 * qty;
                    }

                    //else
                    //{
                    //  qty = -1 * qty;
                    //}
                    double netAmnt = qty * (double)lnAmnt;
                    Global.createAmntBrkDwn(trnsaction_id, trnsdetid, pssblvalid, lneDesc + " " + refDocNum, qty, unitAmnt, netAmnt);
                }
            }
            char[] w = { ':' };
            if (trnsDesc.Length > 484)
            {
                trnsDesc = trnsDesc.Substring(0, 484);
            }
            this.trnsDataGridView.Rows[rowIdx].Cells[0].Value = trnsaction_id;
            this.trnsDataGridView.Rows[rowIdx].Cells[1].Value = "Balancing Leg: " + trnsDesc.Trim().Trim(w);
            this.trnsDataGridView.Rows[rowIdx].Cells[2].Value = refDocNums;
            this.trnsDataGridView.Rows[rowIdx].Cells[3].Value = incrsDcrs;
            this.trnsDataGridView.Rows[rowIdx].Cells[4].Value = Global.mnFrm.cmCde.getAccntNum(acntID) +
              "." + Global.mnFrm.cmCde.getAccntName(acntID);
            this.trnsDataGridView.Rows[rowIdx].Cells[5].Value = acntID;
            this.trnsDataGridView.Rows[rowIdx].Cells[7].Value = ttldiff;
            this.gotoRcnclButton.PerformClick();
        }

        private void createTrnsTmp(
          long trnsID,
          string trnsDesc,
          string trnsDte,
          string incrsDcrs,
          int accntID,
          string accntNum,
          string entrdAmnt,
          string entrdCurr,
          string refDocNum,
          double funcCurrRate,
          double accntCurrRate)
        {
            this.obey_evnts = false;
            System.Windows.Forms.Application.DoEvents();

            if (trnsDesc != "" && trnsDte != "" && incrsDcrs != "" && accntNum != "" && entrdAmnt != "" && entrdCurr != "")
            {
                string trnsDte1 = DateTime.ParseExact(trnsDte, "dd-MMM-yyyy HH:mm:ss",
           System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                double amntEntrd = 0;
                bool isno = double.TryParse(entrdAmnt, out amntEntrd);
                if (isno == false)
                {
                    amntEntrd = Math.Round(Global.computeMathExprsn(entrdAmnt), 2);
                }
                int entCurID = Global.mnFrm.cmCde.getPssblValID(entrdCurr, Global.mnFrm.cmCde.getLovID("Currencies"));

                if (Global.getTrnsID(trnsDesc, accntID, amntEntrd, entCurID, trnsDte1) > 0)
                {
                    Global.mnFrm.cmCde.showMsg("Similar Transaction has been created Already!", 0);
                    return;
                }
                if (accntID <= 0 || entCurID <= 0)
                {
                    Global.mnFrm.cmCde.showMsg("Either the Account Number or Currency does not Exist!", 0);
                    return;
                }

                this.trnsDataGridView.RowCount += 1;
                int rowIdx = this.trnsDataGridView.RowCount - 1;
                this.trnsDataGridView.Rows[rowIdx].HeaderCell.Value = this.trnsDataGridView.RowCount.ToString();
                this.trnsDataGridView.Rows[rowIdx].Cells[0].Value = "-1";
                this.trnsDataGridView.Rows[rowIdx].Cells[1].Value = trnsDesc;
                this.trnsDataGridView.Rows[rowIdx].Cells[2].Value = refDocNum;
                string incrs_dcrs = "Decrease";
                if (incrsDcrs.ToLower() == "increase")
                {
                    incrs_dcrs = "Increase";
                }
                this.trnsDataGridView.Rows[rowIdx].Cells[3].Value = incrs_dcrs;
                this.trnsDataGridView.Rows[rowIdx].Cells[7].Value = amntEntrd.ToString("#,##0.00");

                this.trnsDataGridView.Rows[rowIdx].Cells[4].Value = Global.mnFrm.cmCde.getAccntNum(accntID) +
                  "." + Global.mnFrm.cmCde.getAccntName(accntID);
                this.trnsDataGridView.Rows[rowIdx].Cells[5].Value = accntID;
                this.trnsDataGridView.Rows[rowIdx].Cells[6].Value = "...";
                //this.trnsDataGridView.Rows[rowIdx].Cells[6].Value = Global.mnFrm.cmCde.getAccntName(accntID);
                this.trnsDataGridView.Rows[rowIdx].Cells[9].Value = entCurID;
                this.trnsDataGridView.Rows[rowIdx].Cells[10].Value = entrdCurr;
                this.trnsDataGridView.Rows[rowIdx].Cells[11].Value = "...";
                this.trnsDataGridView.Rows[rowIdx].Cells[12].Value = trnsDte;
                this.trnsDataGridView.Rows[rowIdx].Cells[13].Value = "...";

                int accntCurrID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
                "accb.accb_chart_of_accnts", "accnt_id", "crncy_id", accntID));
                string slctdCurrID = this.trnsDataGridView.Rows[rowIdx].Cells[9].Value.ToString();
                this.trnsDataGridView.Rows[rowIdx].Cells[14].Value = Math.Round(funcCurrRate, 15);
                this.trnsDataGridView.Rows[rowIdx].Cells[15].Value = Math.Round(accntCurrRate, 15);
                System.Windows.Forms.Application.DoEvents();

                //double.TryParse(this.trnsDataGridView.Rows[rowIdx].Cells[14].Value.ToString(), out funcCurrRate);
                //double.TryParse(this.trnsDataGridView.Rows[rowIdx].Cells[15].Value.ToString(), out accntCurrRate);
                if (accntCurrRate == 0)
                {
                    accntCurrRate = Math.Round(
                      Global.get_LtstExchRate(int.Parse(slctdCurrID), accntCurrID,
               this.trnsDataGridView.Rows[rowIdx].Cells[12].Value.ToString()), 15);
                }
                this.trnsDataGridView.Rows[rowIdx].Cells[16].Value = (funcCurrRate * amntEntrd).ToString("#,##0.00");
                this.trnsDataGridView.Rows[rowIdx].Cells[18].Value = (accntCurrRate * amntEntrd).ToString("#,##0.00");
                System.Windows.Forms.Application.DoEvents();

                this.trnsDataGridView.Rows[rowIdx].Cells[19].Value = Global.mnFrm.cmCde.getPssblValNm(accntCurrID);
                this.trnsDataGridView.Rows[rowIdx].Cells[20].Value = accntCurrID;
                this.trnsDataGridView.Rows[rowIdx].Cells[21].Value = this.curid;
                this.trnsDataGridView.Rows[rowIdx].Cells[17].Value = this.curCode;
                this.trnsDataGridView.Rows[rowIdx].Cells[22].Value = trnsID;
            }
            else
            {
                //Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + rownum + ":E" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 0, 0));
                //this.trgtSheets[0].get_Range("M" + rownum + ":M" + rownum + "", Type.Missing).Value2 = errMsg;
            }
            this.obey_evnts = true;
        }

        private void unpostedBatchButton_Click(object sender, EventArgs e)
        {
            string[] selVals = new string[1];
            selVals[0] = this.batchid.ToString();
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
              Global.mnFrm.cmCde.getLovID("Unposted Batches"),
              ref selVals, false, false, this.orgid,
              Global.myBscActn.user_id.ToString(), "0");
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.obey_evnts = true;
                    this.batchid = long.Parse(selVals[i]);
                    if (i == selVals.Length - 1)
                    {
                        this.batchNmRcnclTextBox.Text = Global.getBatchNm(this.batchid);
                    }
                }
            }
        }

        private void crrctBalsVarnceButton_Click(object sender, EventArgs e)
        {
            this.trnsDateTextBox.Text = this.tbalDteTextBox.Text.Replace("23:59:59", "23:59:50");
            if (this.trialBalListView.CheckedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select an Account(s) to Reconcile!", 0);
                return;
            }
            this.statusLoadLabel.Visible = true;
            this.statusLoadPictureBox.Visible = true;
            System.Windows.Forms.Application.DoEvents();
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to adjust balances for the Selected Line(s)?", 1) == DialogResult.No)
            {
                return;
            }
            int trnsCreated = 0;
            this.batchid = Global.getBatchID(this.batchNmRcnclTextBox.Text, Global.mnFrm.cmCde.Org_id);
            if (this.batchid > 0)
            {
                if (Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_trnsctn_batches", "batch_id", "batch_status", this.batchid) == "1")
                {
                    this.resetRcnclButton_Click(this.resetRcnclButton, e);
                }
            }
            string tstDate = DateTime.Parse(this.tbalDteTextBox.Text).ToString("yyyy-MM");
            string trnsDte = Global.getLastPeriodEndDate(tstDate);
            this.trnsDateTextBox.Text = trnsDte;
            for (int i = 0; i < this.trialBalListView.CheckedItems.Count; i++)
            {
                int curAccntID = -1;
                int.TryParse(this.trialBalListView.CheckedItems[i].SubItems[8].Text, out curAccntID);
                string accntNum1 = Global.mnFrm.cmCde.getAccntNum(curAccntID);
                string isParent = Global.getIsParentOrHsLedger(curAccntID);
                if (curAccntID <= 0 || isParent == "1")
                {
                    //Global.mnFrm.cmCde.showMsg("Please select a Non-Parent Account First!", 0);
                    curAccntID = -1;
                    continue;
                }
                int destAccntID = curAccntID;
                if (destAccntID <= 0)
                {
                    //Global.mnFrm.cmCde.showMsg("Please first select an Account to Move Transactions to!", 0);
                    continue;
                }
                long trnsID = -1;
                // DateTime.Parse(this.trialBalListView.SelectedItems[0].SubItems[7].Text).ToString("dd-MMM-yyyy HH:mm:ss");
                string accntNum2 = Global.mnFrm.cmCde.getAccntNum(destAccntID);

                string refDocNum = "";

                string incrsDcrs1 = "";
                string incrsDcrs2 = "";
                double debitAmnt = 0;
                double.TryParse(this.trialBalListView.CheckedItems[i].SubItems[4].Text, out debitAmnt);
                double creditAmnt = 0;
                double.TryParse(this.trialBalListView.CheckedItems[i].SubItems[5].Text, out creditAmnt);
                double entrdAmnt = 0;
                double.TryParse(this.trialBalListView.CheckedItems[i].SubItems[6].Text, out entrdAmnt);
                if (creditAmnt != 0)
                {
                    incrsDcrs1 = Global.incrsOrDcrsAccnt(curAccntID, "Debit");
                    incrsDcrs2 = Global.incrsOrDcrsAccnt(destAccntID, "Credit");
                    entrdAmnt = creditAmnt;
                }
                else
                {
                    incrsDcrs1 = Global.incrsOrDcrsAccnt(curAccntID, "Credit");
                    incrsDcrs2 = Global.incrsOrDcrsAccnt(destAccntID, "Debit");
                    entrdAmnt = debitAmnt;
                }
                string trnsDesc = "(Closing Balance Adjustments) for balance as at " + this.trialBalListView.CheckedItems[i].SubItems[7].Text +
                    ".\r\nCurrent balance=" + entrdAmnt.ToString("#,##0.00") +
                    ".\r\nAccount Name=" + this.trialBalListView.CheckedItems[i].SubItems[3].Text.Trim();
                string rspnseDesc = "Account Name=" + this.trialBalListView.CheckedItems[i].SubItems[3].Text.Trim()
                    + ".\r\nCurrent balance=" + entrdAmnt.ToString("#,##0.00");
                string rspnse = Microsoft.VisualBasic.Interaction.InputBox(
                  "Type in the new Adjusted Balance (Figure)\r\n\r\n" + rspnseDesc,
                  "Rhomicom", "0", (Global.mnFrm.cmCde.myComputer.Screen.Bounds.Width / 2) - 170,
                  (Global.mnFrm.cmCde.myComputer.Screen.Bounds.Height / 2) - 100);
                if (rspnse.Equals(string.Empty) || rspnse.Equals(null))
                {
                    Global.mnFrm.cmCde.showMsg("Adjusted balance cannot be empty!", 0);
                    continue;
                }
                double rsponse = 0;
                bool rsps = double.TryParse(rspnse, out rsponse);
                if (rsps == false)
                {
                    Global.mnFrm.cmCde.showMsg("Invalid Option! Expecting Numbers only", 0);
                    continue;
                }

                string entrdCurr = this.funcCurCode;
                double funcCurrRate = 1;
                double accntCurrRate = 1;
                this.createTrnsTmp(trnsID, trnsDesc.Replace("\r\n", ""), trnsDte, incrsDcrs1, curAccntID, accntNum1, entrdAmnt.ToString(), entrdCurr, refDocNum, funcCurrRate, accntCurrRate);
                this.createTrnsTmp(trnsID, trnsDesc.Replace("\r\n", ""), trnsDte, incrsDcrs2, destAccntID, accntNum2, rsponse.ToString(), entrdCurr, refDocNum, funcCurrRate, 1);
                trnsCreated++;
                System.Windows.Forms.Application.DoEvents();
            }
            if (trnsCreated <= 0)
            {
                Global.mnFrm.cmCde.showMsg("No transactions Created!", 0);
            }
            else
            {
                this.finStmntsTabControl.SelectedTab = this.tabPage21;
            }
            this.statusLoadLabel.Visible = false;
            this.statusLoadPictureBox.Visible = false;
            System.Windows.Forms.Application.DoEvents();
        }
        #endregion

        #region "PROFIT & LOSS STATEMENT..."
        private void populatePrftNLoss()
        {
            //Check if no other accounting process is running
            bool isAnyRnng = true;
            do
            {
                isAnyRnng = Global.isThereANActvActnPrcss("5", "10 second");
                System.Windows.Forms.Application.DoEvents();
            }
            while (isAnyRnng == true);
            Global.updtActnPrcss(2);
            this.statusLoadLabel.Visible = true;
            this.statusLoadPictureBox.Visible = true;
            this.plListView.Visible = false;
            System.Windows.Forms.Application.DoEvents();

            this.plProgressBar.Value = 0;
            this.plListView.Items.Clear();
            if (this.plDate1TextBox.Text == "" || this.plDate2TextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Start Date and End Date must be entered!", 0);
                return;
            }
            this.plProgressBar.Value = 10;
            DataSet dtst = new DataSet();
            if (int.Parse(this.pnlAccntIDTextBox.Text) <= 0)
            {
                dtst = Global.get_PrftNLoss_Accnts(Global.mnFrm.cmCde.Org_id);
            }
            else
            {
                dtst = Global.get_PrftNLoss_Accnts(Global.mnFrm.cmCde.Org_id, int.Parse(this.pnlAccntIDTextBox.Text));
            }
            int count = dtst.Tables[0].Rows.Count;
            string funccur = Global.mnFrm.cmCde.getPssblValNm(
             Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id));
            this.plGroupBox.Text = "PROFIT & LOSS STATEMENT BETWEEN " +
             this.plDate1TextBox.Text.ToUpper() + " and " + this.plDate2TextBox.Text.ToUpper() + " (" + funccur + ")";
            double rvnsum = 0;
            double expsum = 0;
            int cntr = 0;
            for (int i = 0; i < count; i++)
            {
                Global.updtActnPrcss(2);

                this.plProgressBar.Value = 10 + (int)(((double)i / (double)count) * 90);
                if (dtst.Tables[0].Rows[i][3].ToString() == "1"
                  || dtst.Tables[0].Rows[i][5].ToString() == "1")
                {
                    double ttlV = Global.get_Accnt_Usr_TrnsSumRcsv(int.Parse(dtst.Tables[0].Rows[i][0].ToString()),
                      this.plDate1TextBox.Text,
                      this.plDate2TextBox.Text);
                    ListViewItem nwItem = new ListViewItem(new string[] {
    (1 + cntr).ToString(),
    "", dtst.Tables[0].Rows[i][1].ToString(),
    dtst.Tables[0].Rows[i][1].ToString() + "." + dtst.Tables[0].Rows[i][2].ToString().ToUpper(),
    "",dtst.Tables[0].Rows[i][0].ToString(),ttlV.ToString("#,##0.00")});
                    if (this.pnlSmmryCheckBox.Checked == false
                      || dtst.Tables[0].Rows[i][1].ToString().Substring(0, 1) != " ")
                    {
                        nwItem.UseItemStyleForSubItems = true;
                        nwItem.BackColor = Color.WhiteSmoke;
                        nwItem.Font = new Font("Tahoma", 8.5F, FontStyle.Bold);
                    }
                    this.plListView.Items.Add(nwItem);
                    cntr++;
                }
                else
                {
                    //get_Accnt_TrnsSum
                    double dblVal = Global.get_Accnt_Usr_TrnsSum(int.Parse(dtst.Tables[0].Rows[i][0].ToString()),
                      this.plDate1TextBox.Text,
                      this.plDate2TextBox.Text);
                    if (dblVal == 0 && this.hideZeroAccntCheckBox2.Checked == true)
                    {
                        continue;
                    }
                    if (dtst.Tables[0].Rows[i][4].ToString() == "R")
                    {
                        rvnsum = rvnsum + dblVal;
                    }
                    else
                    {
                        expsum = expsum + dblVal;
                    }
                    if (this.pnlSmmryCheckBox.Checked == true)
                    {
                        continue;
                    }
                    ListViewItem nwItem = new ListViewItem(new string[] {
    (1 + cntr).ToString(),"",
    dtst.Tables[0].Rows[i][1].ToString(),
            dtst.Tables[0].Rows[i][1].ToString()+"."+dtst.Tables[0].Rows[i][2].ToString(),
    dblVal.ToString("#,##0.00"),
    dtst.Tables[0].Rows[i][0].ToString(),""});
                    this.plListView.Items.Add(nwItem);
                    cntr++;
                }
                System.Windows.Forms.Application.DoEvents();
            }

            if (int.Parse(this.pnlAccntIDTextBox.Text) <= 0)
            {
                double netsum = rvnsum - expsum;
                ListViewItem nwItem1 = new ListViewItem(new string[] {
             "",
             "","","","","","",""});
                this.plListView.Items.Add(nwItem1);

                ListViewItem nwItem2 = new ListViewItem(new string[] {
             "",
             "","","TOTAL REVENUES = ",(rvnsum).ToString("#,##0.00"),"","",""});
                nwItem2.BackColor = Color.WhiteSmoke;
                nwItem2.Font = new Font("Tahoma", 10, FontStyle.Bold);
                this.plListView.Items.Add(nwItem2);

                ListViewItem nwItem3 = new ListViewItem(new string[] {
             "",
             "","","TOTAL EXPENSES = ",(expsum).ToString("#,##0.00"),"","",""});
                nwItem3.BackColor = Color.WhiteSmoke;
                nwItem3.Font = new Font("Tahoma", 10, FontStyle.Bold);
                this.plListView.Items.Add(nwItem3);

                ListViewItem nwItem4 = new ListViewItem(new string[] {
             "",
             "","","NET INCOME = ",(netsum).ToString("#,##0.00"),"","",""});
                nwItem4.BackColor = Color.WhiteSmoke;
                nwItem4.Font = new Font("Tahoma", 10, FontStyle.Bold | FontStyle.Underline);
                if (netsum < 0)
                {
                    nwItem4.ForeColor = Color.Red;
                }
                else
                {
                    nwItem4.ForeColor = Color.Black;
                }
                this.plListView.Items.Add(nwItem4);
            }
            this.statusLoadLabel.Visible = false;
            this.statusLoadPictureBox.Visible = false;
            this.plListView.Visible = true;
            System.Windows.Forms.Application.DoEvents();
            this.plProgressBar.Value = 100;
        }

        private void plDate1Button_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[1]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            Global.mnFrm.cmCde.selectDate(ref this.plDate1TextBox);
        }

        private void plDate2Button_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[1]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            Global.mnFrm.cmCde.selectDate(ref this.plDate2TextBox);
            this.plDate2TextBox.Text = this.plDate2TextBox.Text.Replace("00:00:00", "23:59:59");
        }

        private void plGenRptButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[5]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            this.plGenRptButton.Enabled = false;
            System.Windows.Forms.Application.DoEvents();

            this.populatePrftNLoss();
            this.plGenRptButton.Enabled = true;
            this.plListView.Focus();
        }

        private void exptExclPrfNLsMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcelSelective(this.plListView, this.plGroupBox.Text);
        }

        private void plExprtExclButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcelSelective(this.plListView, this.plGroupBox.Text);
        }

        private void vwSQLPrfNLsMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(Global.mnFrm.pnlSQLStmnt, 10);
        }

        private void vwTrnsPrfNLsMenuItem_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[2]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.plListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select an Account First!", 0);
                return;
            }
            else
            {
                int accIDIn = int.Parse(this.plListView.SelectedItems[0].SubItems[5].Text);
                string isPrnt = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_chart_of_accnts", "accnt_id", "(CASE WHEN is_prnt_accnt='1' THEN is_prnt_accnt ELSE has_sub_ledgers END)", accIDIn);
                if (isPrnt == "1")
                {
                    this.pnlAccntIDTextBox.Text = accIDIn.ToString();
                    this.pnlAccntNmTextBox.Text = Global.mnFrm.cmCde.getAccntNum(accIDIn) +
                      "." + Global.mnFrm.cmCde.getAccntName(accIDIn);
                    this.pnlSmmryCheckBox.Checked = false;
                    //this.finStmntsTabControl.SelectedTab = this.tbalTabPage;
                    System.Windows.Forms.Application.DoEvents();
                    this.plGenRptButton_Click(this.plGenRptButton, e);
                }
                else
                {
                    vwTrnsctnsDiag nwDiag = new vwTrnsctnsDiag();
                    nwDiag.my_org_id = Global.mnFrm.cmCde.Org_id;
                    nwDiag.accnt_name = this.plListView.SelectedItems[0].SubItems[2].Text.Trim();
                    nwDiag.accntid = int.Parse(this.plListView.SelectedItems[0].SubItems[5].Text);
                    nwDiag.dte1 = this.plDate1TextBox.Text;
                    nwDiag.dte2 = this.plDate2TextBox.Text;

                    DialogResult dgres = nwDiag.ShowDialog();
                    if (dgres == DialogResult.OK)
                    {

                    }
                }
            }
        }

        private void plListView_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.vwTrnsPrfNLsMenuItem_Click(this.vwTrnsPrfNLsMenuItem, e);
            }
            else if ((e.Control && e.KeyCode == Keys.R) || e.KeyCode == Keys.F5)
            {
                if (this.plGenRptButton.Enabled == true)
                {
                    this.plGenRptButton_Click(this.plGenRptButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                Global.mnFrm.cmCde.listViewKeyDown(this.plListView, e);
            }
        }

        private void plListView_DoubleClick(object sender, System.EventArgs e)
        {
            this.vwTrnsPrfNLsMenuItem_Click(this.vwTrnsPrfNLsMenuItem, e);
        }
        #endregion

        #region "MONTHLY STATEMENT..."

        private void mnthlyDate1Button_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[1]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            Global.mnFrm.cmCde.selectDate(ref this.mnthlyStrtDteTextBox);
        }

        private void mnthlyDate2Button_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[1]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            Global.mnFrm.cmCde.selectDate(ref this.mnthlyEndDteTextBox);
            this.mnthlyEndDteTextBox.Text = this.mnthlyEndDteTextBox.Text.Replace("00:00:00", "23:59:59");
        }

        private void genMnthlyRptButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showMsg("Sorry! Feature not available in this edition!\nContact your the Software Provider!", 0);
            return;
        }

        private void exptExclMnthlyMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcelSelective(this.periodStmntListView, this.periodGroupBox.Text);
        }

        private void mnthlyExprtExclButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcelSelective(this.periodStmntListView, this.periodGroupBox.Text);
        }

        private void vwSQLMnthlyMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(Global.mnFrm.periodSQLStmnt, 10);
        }

        private void vwTrnsMnthlyMenuItem_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[2]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.periodStmntListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select an Account First!", 0);
                return;
            }
            else if (this.periodStmntListView.SelectedItems[0].SubItems[1].Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please select a Non-Parent Account!", 0);
                return;
            }
            vwTrnsctnsDiag nwDiag = new vwTrnsctnsDiag();
            nwDiag.my_org_id = Global.mnFrm.cmCde.Org_id;
            nwDiag.accnt_name = this.periodStmntListView.SelectedItems[0].SubItems[1].Text.Trim();
            nwDiag.accntid = int.Parse(this.periodStmntListView.SelectedItems[0].SubItems[3].Text);
            nwDiag.dte1 = this.mnthlyStrtDteTextBox.Text;
            nwDiag.dte2 = this.mnthlyEndDteTextBox.Text;

            DialogResult dgres = nwDiag.ShowDialog();
            if (dgres == DialogResult.OK)
            {

            }
        }

        private void mnthlyListView_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.vwTrnsMnthlyMenuItem_Click(this.vwTrnsMnthlyMenuItem, e);
            }
            else if ((e.Control && e.KeyCode == Keys.R) || e.KeyCode == Keys.F5)
            {
                if (this.genMthlyRptButton.Enabled == true)
                {
                    this.genMnthlyRptButton_Click(this.genMthlyRptButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                Global.mnFrm.cmCde.listViewKeyDown(this.periodStmntListView, e);
            }
        }

        private void mnthlyListView_DoubleClick(object sender, System.EventArgs e)
        {
            this.vwTrnsMnthlyMenuItem_Click(this.vwTrnsMnthlyMenuItem, e);
        }
        #endregion

        #region "CASH FLOW STATEMENT..."
        private void populateCashFlowStatement()
        {
            //Check if no other accounting process is running
            bool isAnyRnng = true;
            do
            {
                isAnyRnng = Global.isThereANActvActnPrcss("5", "10 second");
                System.Windows.Forms.Application.DoEvents();
            }
            while (isAnyRnng == true);
            Global.updtActnPrcss(2);
            this.statusLoadLabel.Visible = true;
            this.statusLoadPictureBox.Visible = true;
            this.cashFlowListView.Visible = false;
            System.Windows.Forms.Application.DoEvents();
            this.cashFlowProgressBar.Value = 0;
            this.cashFlowListView.Items.Clear();
            if (this.cashFlowStrtTextBox.Text == ""
              || this.cashFlowEndTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Start Date and End Date must be entered!", 0);
                return;
            }
            this.cashFlowProgressBar.Value = 10;
            string[] accClsfctnsIndrct ={"Cash and Cash Equivalents",
"Operating Activities.Net Income",
"Operating Activities.Depreciation Expense",
"Operating Activities.Amortization Expense",
"Operating Activities.Gain on Sale of Equipment"/*NEGATE*/,
"Operating Activities.Loss on Sale of Equipment",
"Operating Activities.Other Non-Cash Expense",
"Operating Activities.Accounts Receivable"/*NEGATE*/,
"Operating Activities.Prepaid Expenses"/*NEGATE*/,
"Operating Activities.Inventory"/*NEGATE*/,
"Operating Activities.Accounts Payable",
"Operating Activities.Accrued Expenses",
"Operating Activities.Taxes Payable",
"Investing Activities.Asset Sales/Purchases"/*NEGATE*/,
"Investing Activities.Equipment Sales/Purchases"/*NEGATE*/,
"Financing Activities.Capital/Stock",
"Financing Activities.Long Term Debts",
"Financing Activities.Short Term Debts",
"Financing Activities.Equity Securities",
"Financing Activities.Dividends Declared"/*NEGATE*/,
"Cash and Cash Equivalents"
};
            double[] accClsfctnsInDrctNeg = { 1, 1, 1, 1, -1, 1, 1, -1, -1, -1, 1, 1, 1, -1, -1, 1, 1, 1, 1, -1, 1 };
            string[] accClsfctnsDrct ={"Cash and Cash Equivalents",
"Operating Activities.Sale of Goods",
"Operating Activities.Sale of Services",
"Operating Activities.Other Income Sources",
"Operating Activities.Cost of Sales",
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
"Cash and Cash Equivalents"
};
            double[] accClsfctnsDrctNeg = { 1, 1, 1, 1, 1, -1, -1, -1, -1, 1, 1, 1, -1, -1, -1, -1, 1, 1, 1, 1, -1, 1 };

            string funccur = Global.mnFrm.cmCde.getPssblValNm(
             Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id));
            this.cashFlowGroupBox.Text = "PERIOD BY PERIOD CASH FLOW STATEMENT FROM " +
             this.cashFlowStrtTextBox.Text.ToUpper() + " TO " + this.cashFlowEndTextBox.Text.ToUpper() + " (" + funccur + ")";

            List<string> dteArray1 = Global.getBdgtDates(this.cashFlowStrtTextBox.Text,
              this.cashFlowEndTextBox.Text, this.cashFlowDrtnComboBox.Text);
            this.cashFlowListView.Columns.Clear();
            this.cashFlowListView.Columns.Add("No.", 35);
            this.cashFlowListView.Columns.Add("Account Number", 0);
            this.cashFlowListView.Columns.Add("Account Name", 450);
            this.cashFlowListView.Columns.Add("accnt_id", 0);
            this.cashFlowListView.Columns.Add("is_parnt", 0);
            //this.cashFlowListView.Columns.Add("Opening Balance", 100);

            int nwColsCnt = 0;
            for (int a = 0; a < dteArray1.Count; a++)
            {
                int rem = 0;
                Math.DivRem(a, 2, out rem);
                if (rem == 0)
                {
                    string colNm = "";
                    if (this.cashFlowDrtnComboBox.Text == "Yearly")
                    {
                        colNm = DateTime.Parse(dteArray1[a]).ToString("yyyy");
                    }
                    else if (this.cashFlowDrtnComboBox.Text == "Half Yearly"
                      || this.cashFlowDrtnComboBox.Text == "Quarterly")
                    {
                        colNm = DateTime.Parse(dteArray1[a]).ToString("MMM-")
                        + DateTime.Parse(dteArray1[a + 1]).ToString("MMM (yyyy)");
                    }
                    else if (this.cashFlowDrtnComboBox.Text == "Monthly")
                    {
                        colNm = DateTime.Parse(dteArray1[a]).ToString("MMM-yyyy");
                    }
                    else
                    {
                        colNm = this.cashFlowDrtnComboBox.Text.Replace("ly", "") + (a + 1).ToString();
                    }
                    this.cashFlowListView.Columns.Add(colNm, 100);
                    nwColsCnt++;
                }
            }
            this.cashFlowListView.Columns.Add("Totals", 100);
            string[] loopingClsfctns;
            double[] loopingNegations;

            if (this.cashFlowTypComboBox.Text.StartsWith("Direct"))
            {
                loopingClsfctns = accClsfctnsDrct;
                loopingNegations = accClsfctnsDrctNeg;
            }
            else
            {
                loopingClsfctns = accClsfctnsIndrct;
                loopingNegations = accClsfctnsInDrctNeg;
            }
            string[] spltchrs = { "." };
            string prevGroup = "";
            int count = loopingClsfctns.Length;
            int cntr = 0;
            int colCntr = 4;
            ListViewItem nwItem;
            string opngBalsDte = "";  // DateTime.Parse(this.cashFlowStrtTextBox.Text).AddDays(-1).ToString("dd-MMM-yyyy 23:59:59");
            string clsngBalsDte = ""; //DateTime.Parse(this.cashFlowEndTextBox.Text).ToString("dd-MMM-yyyy 23:59:59");
            double netOprtngActvts = 0;
            double netInvstngActvts = 0;
            double netFncngActvts = 0;
            double cashOpngBals = 0;
            double cashClsngBals = 0;
            double wrkngValue = 0;

            for (int z = 0; z < dteArray1.Count; z++)
            {
                int rem = 0;
                Math.DivRem(z, 2, out rem);
                this.cashFlowProgressBar.Value = 10 + (int)(((double)z / (double)dteArray1.Count) * 90);
                System.Windows.Forms.Application.DoEvents();
                if (rem == 0)
                {
                    opngBalsDte = DateTime.Parse(dteArray1[z]).AddDays(-1).ToString("dd-MMM-yyyy 23:59:59");
                    clsngBalsDte = DateTime.Parse(dteArray1[z + 1]).ToString("dd-MMM-yyyy 23:59:59");

                    //dteArray1[z] dteArray1[z+1]
                    cntr = 0;
                    colCntr++;
                    netOprtngActvts = 0;
                    netInvstngActvts = 0;
                    netFncngActvts = 0;
                    cashOpngBals = 0;
                    cashClsngBals = 0;
                    wrkngValue = 0;

                    for (int i = 0; i < loopingClsfctns.Length; i++)
                    {
                        if (i == 0)
                        {
                            if (z == 0)
                            {
                                nwItem = new ListViewItem(new string[] {
          (1 + cntr).ToString(), "",
          ("Total "+loopingClsfctns[i].Split(spltchrs, StringSplitOptions.RemoveEmptyEntries)[0]+ " (Opening Balance)").ToUpper(),
          "-1",
          loopingClsfctns[i]});
                                for (int b = 0; b < (4 + nwColsCnt); b++)
                                {
                                    if (b == 0)
                                    {
                                        wrkngValue = Global.getCashFlowAccBlsSum(
                                          loopingClsfctns[i], opngBalsDte, Global.mnFrm.cmCde.Org_id) * loopingNegations[i];

                                        nwItem.SubItems.Add(wrkngValue.ToString("#,##0.00"));
                                        cashOpngBals = wrkngValue;
                                    }
                                    else
                                    {
                                        nwItem.SubItems.Add("0.00");
                                    }
                                }
                                cntr++;
                                nwItem.UseItemStyleForSubItems = true;
                                nwItem.BackColor = Color.WhiteSmoke;
                                nwItem.Font = new Font("Tahoma", 8.5F, FontStyle.Bold);
                                this.cashFlowListView.Items.Add(nwItem);
                            }
                            else
                            {
                                wrkngValue = Global.getCashFlowAccBlsSum(
                  loopingClsfctns[i], opngBalsDte, Global.mnFrm.cmCde.Org_id) * loopingNegations[i];
                                cashOpngBals = wrkngValue;
                                this.cashFlowListView.Items[cntr].SubItems[colCntr].Text = wrkngValue.ToString("#,##0.00");
                                cntr++;
                            }
                        }
                        else if (i == loopingClsfctns.Length - 1)
                        {
                            if (z == 0)
                            {
                                //Net Cash Flow (Calculated)
                                nwItem = new ListViewItem(new string[] {
          (1 + cntr).ToString(), "",
          "                      Net Cash Flow (Calculated)",
          "-1",
          "1"});
                                for (int b = 0; b < (4 + nwColsCnt); b++)
                                {
                                    if (b == 0)
                                    {
                                        wrkngValue = netOprtngActvts + netInvstngActvts + netFncngActvts;
                                        nwItem.SubItems.Add(wrkngValue.ToString("#,##0.00"));
                                    }
                                    else
                                    {
                                        nwItem.SubItems.Add("0.00");
                                    }
                                }
                                cntr++;
                                nwItem.UseItemStyleForSubItems = true;
                                nwItem.BackColor = Color.WhiteSmoke;
                                nwItem.Font = new Font("Tahoma", 8.5F, FontStyle.Bold);
                                this.cashFlowListView.Items.Add(nwItem);
                                //Net Cash Flow (Expected)
                                nwItem = new ListViewItem(new string[] {
          (1 + cntr).ToString(), "",
          "                      Net Cash Flow (Expected)",
          "-1",
          "1"});
                                for (int b = 0; b < (4 + nwColsCnt); b++)
                                {
                                    if (b == 0)
                                    {
                                        cashClsngBals = Global.getCashFlowAccBlsSum(
                    loopingClsfctns[i], clsngBalsDte, Global.mnFrm.cmCde.Org_id) * loopingNegations[i];

                                        wrkngValue = cashClsngBals - cashOpngBals;
                                        nwItem.SubItems.Add(wrkngValue.ToString("#,##0.00"));
                                    }
                                    else
                                    {
                                        nwItem.SubItems.Add("0.00");
                                    }
                                }
                                cntr++;
                                nwItem.UseItemStyleForSubItems = true;
                                nwItem.BackColor = Color.WhiteSmoke;
                                nwItem.Font = new Font("Tahoma", 8.5F, FontStyle.Bold);
                                this.cashFlowListView.Items.Add(nwItem);
                                //Closing Cash Balance
                                nwItem = new ListViewItem(new string[] {
          (1 + cntr).ToString(), "",
          ("Total "+loopingClsfctns[i].Split(spltchrs, StringSplitOptions.RemoveEmptyEntries)[0]+ " (Closing Balance)").ToUpper(),
          "-1",
          loopingClsfctns[i]});
                                for (int b = 0; b < (4 + nwColsCnt); b++)
                                {
                                    if (b == 0)
                                    {
                                        nwItem.SubItems.Add(cashClsngBals.ToString("#,##0.00"));
                                    }
                                    else
                                    {
                                        nwItem.SubItems.Add("0.00");
                                    }
                                }
                                cntr++;
                                nwItem.UseItemStyleForSubItems = true;
                                nwItem.BackColor = Color.WhiteSmoke;
                                nwItem.Font = new Font("Tahoma", 8.5F, FontStyle.Bold);
                                this.cashFlowListView.Items.Add(nwItem);


                            }
                            else
                            {
                                cashClsngBals = Global.getCashFlowAccBlsSum(
                  loopingClsfctns[i], clsngBalsDte, Global.mnFrm.cmCde.Org_id) * loopingNegations[i];

                                wrkngValue = netOprtngActvts + netInvstngActvts + netFncngActvts;
                                this.cashFlowListView.Items[cntr].SubItems[colCntr].Text = wrkngValue.ToString("#,##0.00");
                                cntr++;

                                //Net Cash Flow (Expected)
                                wrkngValue = cashClsngBals - cashOpngBals;
                                this.cashFlowListView.Items[cntr].SubItems[colCntr].Text = wrkngValue.ToString("#,##0.00");
                                cntr++;


                                this.cashFlowListView.Items[cntr].SubItems[colCntr].Text = cashClsngBals.ToString("#,##0.00");
                                cntr++;
                            }
                        }
                        else if (prevGroup != loopingClsfctns[i].Split(spltchrs, StringSplitOptions.RemoveEmptyEntries)[0])
                        {
                            prevGroup = loopingClsfctns[i].Split(spltchrs, StringSplitOptions.RemoveEmptyEntries)[0];
                            if (z == 0)
                            {
                                nwItem = new ListViewItem(new string[] {
          (1 + cntr).ToString(), "",
           ("           Cash Flow from " + prevGroup).ToUpper(),
          "-1",
          "1"});
                                for (int b = 0; b < (4 + nwColsCnt); b++)
                                {
                                    nwItem.SubItems.Add("");
                                }
                                cntr++;
                                nwItem.UseItemStyleForSubItems = true;
                                nwItem.BackColor = Color.WhiteSmoke;
                                nwItem.Font = new Font("Tahoma", 8.5F, FontStyle.Bold);
                                this.cashFlowListView.Items.Add(nwItem);
                            }
                            else
                            {
                                this.cashFlowListView.Items[cntr].SubItems[colCntr].Text = "";
                                cntr++;
                            }
                        }

                        if (loopingClsfctns[i].Contains("."))
                        {
                            if (z == 0)
                            {
                                nwItem = new ListViewItem(new string[] {
          (1 + cntr).ToString(), "",
          "                      "+loopingClsfctns[i].Split(spltchrs, StringSplitOptions.RemoveEmptyEntries)[1],
          "-1",
          loopingClsfctns[i]});
                                for (int b = 0; b < (4 + nwColsCnt); b++)
                                {
                                    if (b == 0)
                                    {
                                        wrkngValue = Global.get_CashFlow_Usr_TrnsSum(
                    loopingClsfctns[i], dteArray1[z], dteArray1[z + 1]) * loopingNegations[i];
                                        if (loopingClsfctns[i].Contains("Operating Activities"))
                                        {
                                            netOprtngActvts += wrkngValue;
                                        }
                                        else if (loopingClsfctns[i].Contains("Investing Activities"))
                                        {
                                            netInvstngActvts += wrkngValue;
                                        }
                                        else if (loopingClsfctns[i].Contains("Financing Activities"))
                                        {
                                            netFncngActvts += wrkngValue;
                                        }
                                        nwItem.SubItems.Add(wrkngValue.ToString("#,##0.00"));
                                    }
                                    else
                                    {
                                        nwItem.SubItems.Add("0.00");
                                    }
                                }
                                cntr++;
                                if (this.cashFlowTypComboBox.Text.Contains("Detail"))
                                {
                                    nwItem.UseItemStyleForSubItems = true;
                                    nwItem.BackColor = Color.WhiteSmoke;
                                    nwItem.Font = new Font("Tahoma", 8.5F, FontStyle.Bold);
                                }
                                this.cashFlowListView.Items.Add(nwItem);
                            }
                            else
                            {
                                wrkngValue = Global.get_CashFlow_Usr_TrnsSum(
                loopingClsfctns[i], dteArray1[z], dteArray1[z + 1]) * loopingNegations[i];
                                if (loopingClsfctns[i].Contains("Operating Activities"))
                                {
                                    netOprtngActvts += wrkngValue;
                                }
                                else if (loopingClsfctns[i].Contains("Investing Activities"))
                                {
                                    netInvstngActvts += wrkngValue;
                                }
                                else if (loopingClsfctns[i].Contains("Financing Activities"))
                                {
                                    netFncngActvts += wrkngValue;
                                }
                                this.cashFlowListView.Items[cntr].SubItems[colCntr].Text = wrkngValue.ToString("#,##0.00");
                                cntr++;
                            }
                        }
                        System.Windows.Forms.Application.DoEvents();
                        if (this.cashFlowTypComboBox.Text.Contains("Detail"))
                        {
                            DataSet dtst = Global.get_Clsfctn_Accnts(Global.mnFrm.cmCde.Org_id,
                              loopingClsfctns[i]);
                            for (int c = 0; c < dtst.Tables[0].Rows.Count; c++)
                            {
                                System.Windows.Forms.Application.DoEvents();
                                if (z == 0)
                                {
                                    nwItem = new ListViewItem(new string[] {
          (1 + cntr).ToString(), dtst.Tables[0].Rows[c][1].ToString(),
          "                                 "+dtst.Tables[0].Rows[c][1].ToString() +
          "." + dtst.Tables[0].Rows[c][2].ToString(),
          dtst.Tables[0].Rows[c][0].ToString(),
          loopingClsfctns[i]});
                                    for (int b = 0; b < (4 + nwColsCnt); b++)
                                    {
                                        if (b == 0)
                                        {
                                            if (i == 0)
                                            {
                                                wrkngValue = Global.getAccntLstDailyNetBals(int.Parse(dtst.Tables[0].Rows[c][0].ToString()),
                                   opngBalsDte) * loopingNegations[i];
                                            }
                                            else if (i == (loopingClsfctns.Length - 1))
                                            {
                                                wrkngValue = Global.getAccntLstDailyNetBals(int.Parse(dtst.Tables[0].Rows[c][0].ToString()),
                                   clsngBalsDte) * loopingNegations[i];
                                            }
                                            else
                                            {
                                                wrkngValue = Global.get_Accnt_Usr_TrnsSum(int.Parse(dtst.Tables[0].Rows[c][0].ToString()),
                                      dteArray1[z],
                                      dteArray1[z + 1]) * loopingNegations[i];
                                            }
                                            if (dtst.Tables[0].Rows[c][3].ToString() == "1"
                                              || dtst.Tables[0].Rows[c][5].ToString() == "1")
                                            {
                                                nwItem.SubItems.Add("");
                                            }
                                            else
                                            {
                                                nwItem.SubItems.Add(wrkngValue.ToString("#,##0.00"));
                                            }
                                        }
                                        else
                                        {
                                            if (dtst.Tables[0].Rows[c][3].ToString() == "1"
                        || dtst.Tables[0].Rows[c][5].ToString() == "1")
                                            {
                                                nwItem.SubItems.Add("");
                                            }
                                            else
                                            {
                                                nwItem.SubItems.Add("0.00");
                                            }
                                        }
                                    }
                                    cntr++;
                                    if (dtst.Tables[0].Rows[c][3].ToString() == "1"
                     || dtst.Tables[0].Rows[c][5].ToString() == "1")
                                    {
                                        nwItem.UseItemStyleForSubItems = true;
                                        nwItem.BackColor = Color.WhiteSmoke;
                                        nwItem.Font = new Font("Tahoma", 8.5F, FontStyle.Bold);
                                    }
                                    this.cashFlowListView.Items.Add(nwItem);
                                }
                                else
                                {
                                    if (i == 0)
                                    {
                                        wrkngValue = Global.getAccntLstDailyNetBals(int.Parse(dtst.Tables[0].Rows[c][0].ToString()),
                           opngBalsDte) * loopingNegations[i];
                                    }
                                    else if (i == (loopingClsfctns.Length - 1))
                                    {
                                        wrkngValue = Global.getAccntLstDailyNetBals(int.Parse(dtst.Tables[0].Rows[c][0].ToString()),
                           clsngBalsDte) * loopingNegations[i];
                                    }
                                    else
                                    {
                                        wrkngValue = Global.get_Accnt_Usr_TrnsSum(int.Parse(dtst.Tables[0].Rows[c][0].ToString()),
                              dteArray1[z],
                              dteArray1[z + 1]) * loopingNegations[i];
                                    }
                                    if (dtst.Tables[0].Rows[c][3].ToString() == "1"
                    || dtst.Tables[0].Rows[c][5].ToString() == "1")
                                    {
                                        this.cashFlowListView.Items[cntr].SubItems[colCntr].Text = "";
                                    }
                                    else
                                    {
                                        this.cashFlowListView.Items[cntr].SubItems[colCntr].Text = wrkngValue.ToString("#,##0.00");
                                    }
                                    cntr++;
                                }
                            }
                        }

                        if (i > 0 && i < (loopingClsfctns.Length - 1))
                        {
                            if (prevGroup != loopingClsfctns[i + 1].Split(spltchrs, StringSplitOptions.RemoveEmptyEntries)[0])
                            {
                                prevGroup = loopingClsfctns[i].Split(spltchrs, StringSplitOptions.RemoveEmptyEntries)[0];
                                if (z == 0)
                                {
                                    nwItem = new ListViewItem(new string[] {
          (1 + cntr).ToString(), "",
           ("           Net Cash Flow provided by " + prevGroup).ToUpper(),
          "-1",
          "1"});
                                    for (int b = 0; b < (4 + nwColsCnt); b++)
                                    {
                                        if (b == 0)
                                        {
                                            if (loopingClsfctns[i].Contains("Operating Activities"))
                                            {
                                                wrkngValue = netOprtngActvts;
                                            }
                                            else if (loopingClsfctns[i].Contains("Investing Activities"))
                                            {
                                                wrkngValue = netInvstngActvts;
                                            }
                                            else if (loopingClsfctns[i].Contains("Financing Activities"))
                                            {
                                                wrkngValue = netFncngActvts;
                                            }
                                            nwItem.SubItems.Add(wrkngValue.ToString("#,##0.00"));
                                        }
                                        else
                                        {
                                            nwItem.SubItems.Add("0.00");
                                        }
                                    }
                                    cntr++;
                                    //i++;
                                    nwItem.UseItemStyleForSubItems = true;
                                    nwItem.BackColor = Color.WhiteSmoke;
                                    nwItem.Font = new Font("Tahoma", 8.5F, FontStyle.Bold);
                                    this.cashFlowListView.Items.Add(nwItem);
                                }
                                else
                                {
                                    if (loopingClsfctns[i].Contains("Operating Activities"))
                                    {
                                        wrkngValue = netOprtngActvts;
                                    }
                                    else if (loopingClsfctns[i].Contains("Investing Activities"))
                                    {
                                        wrkngValue = netInvstngActvts;
                                    }
                                    else if (loopingClsfctns[i].Contains("Financing Activities"))
                                    {
                                        wrkngValue = netFncngActvts;
                                    }

                                    this.cashFlowListView.Items[cntr].SubItems[colCntr].Text = (wrkngValue).ToString("#,##0.00");
                                    cntr++;
                                }
                            }
                        }
                    }
                }
            }
            double tstVal = 0;
            int q = 0;
            //int itmsCnt = this.cashFlowListView.Items.Count;
            bool isNum = false;
            for (q = 0; q < this.cashFlowListView.Items.Count; q++)
            {
                wrkngValue = 0;
                for (int j = 5; j < this.cashFlowListView.Columns.Count - 1; j++)
                {
                    isNum = double.TryParse(this.cashFlowListView.Items[q].SubItems[j].Text, out tstVal);
                    if (this.cashFlowListView.Items[q].SubItems[4].Text.Contains("Cash and Cash Equivalents"))
                    {
                        wrkngValue = tstVal;
                    }
                    else if (isNum)
                    {
                        wrkngValue += tstVal;
                    }
                }
                this.cashFlowListView.Items[q].Text = (q + 1).ToString();
                if (wrkngValue != 0)
                {
                    this.cashFlowListView.Items[q].SubItems[this.cashFlowListView.Columns.Count - 1].Text = wrkngValue.ToString("#,##0.00");
                }
                else if (this.cashFlowListView.Items[q].Font.Bold == false && this.hideZerosCashFlwCheckBox.Checked)
                {
                    this.cashFlowListView.Items.RemoveAt(q);
                    q--;
                }
                System.Windows.Forms.Application.DoEvents();
            }
            this.cashFlowProgressBar.Value = 100;
            this.statusLoadLabel.Visible = false;
            this.statusLoadPictureBox.Visible = false;
            this.cashFlowListView.Visible = true;
            System.Windows.Forms.Application.DoEvents();
        }

        private void CashFlowDate1Button_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[1]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            Global.mnFrm.cmCde.selectDate(ref this.cashFlowStrtTextBox);
        }

        private void CashFlowDate2Button_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[1]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            Global.mnFrm.cmCde.selectDate(ref this.cashFlowEndTextBox);
            this.cashFlowEndTextBox.Text = this.cashFlowEndTextBox.Text.Replace("00:00:00", "23:59:59");
        }

        private void genCashFlowRptButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[5]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            this.genCashFlowRptButton.Enabled = false;
            System.Windows.Forms.Application.DoEvents();

            this.populateCashFlowStatement();
            this.genCashFlowRptButton.Enabled = true;
            this.cashFlowListView.Focus();
        }

        private void exptExclCashFlowMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcelSelective(this.cashFlowListView, this.cashFlowGroupBox.Text);
        }

        private void CashFlowExprtExclButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcelSelective(this.cashFlowListView, this.cashFlowGroupBox.Text);
        }

        private void vwSQLCashFlowMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(Global.mnFrm.cashFlowSQLStmnt, 10);
        }

        private void vwTrnsCashFlowMenuItem_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[2]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.cashFlowListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select an Account First!", 0);
                return;
            }
            else if (this.cashFlowListView.SelectedItems[0].SubItems[1].Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please select a Non-Parent Account!", 0);
                return;
            }
            vwTrnsctnsDiag nwDiag = new vwTrnsctnsDiag();
            nwDiag.my_org_id = Global.mnFrm.cmCde.Org_id;
            nwDiag.accnt_name = this.cashFlowListView.SelectedItems[0].SubItems[1].Text.Trim();
            nwDiag.accntid = int.Parse(this.cashFlowListView.SelectedItems[0].SubItems[3].Text);
            nwDiag.dte1 = this.cashFlowStrtTextBox.Text;
            nwDiag.dte2 = this.cashFlowEndTextBox.Text;

            DialogResult dgres = nwDiag.ShowDialog();
            if (dgres == DialogResult.OK)
            {

            }
        }

        private void CashFlowListView_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.vwTrnsCashFlowMenuItem_Click(this.vwTrnsCashFlowMenuItem, e);
            }
            else if ((e.Control && e.KeyCode == Keys.R) || e.KeyCode == Keys.F5)
            {
                if (this.genCashFlowRptButton.Enabled == true)
                {
                    this.genCashFlowRptButton_Click(this.genCashFlowRptButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                Global.mnFrm.cmCde.listViewKeyDown(this.cashFlowListView, e);
            }
        }

        private void CashFlowListView_DoubleClick(object sender, System.EventArgs e)
        {
            this.vwTrnsCashFlowMenuItem_Click(this.vwTrnsCashFlowMenuItem, e);
        }
        #endregion

        #region "BALANCE SHEET..."
        private void populateBlsDet()
        {
            //Check if no other accounting process is running
            bool isAnyRnng = true;
            do
            {
                isAnyRnng = Global.isThereANActvActnPrcss("5", "10 second");
                System.Windows.Forms.Application.DoEvents();
            }
            while (isAnyRnng == true);
            Global.updtActnPrcss(3);
            this.obey_bls_evnts = false;
            this.blsListView.Items.Clear();
            this.statusLoadLabel.Visible = true;
            this.statusLoadPictureBox.Visible = true;
            this.blsListView.Visible = false;
            System.Windows.Forms.Application.DoEvents();

            DataSet dtst = Global.get_Bls_Det(
             Global.mnFrm.cmCde.Org_id,
                  this.asAtDteTextBox.Text);

            this.blsProgressBar.Value = 0;
            this.blsListView.Items.Clear();
            this.blsProgressBar.Value = 10;
            int count = dtst.Tables[0].Rows.Count;
            string funccur = Global.mnFrm.cmCde.getPssblValNm(
             Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id));
            //blsDteStr = Global.mnFrm.cmCde.getDB_Date_time();
            this.blsGroupBox.Text = "BALANCE SHEET AS AT " + this.asAtDteTextBox.Text;

            double bls_assetsum = 0;
            double bls_networthsum = 0;
            double bls_liabltysum = 0;
            int cntr = 0;
            for (int i = 0; i < count; i++)
            {
                Global.updtActnPrcss(3);
                //;
                this.blsProgressBar.Value = 10 + (int)(((double)i / (double)count) * 90);
                if (dtst.Tables[0].Rows[i][5].ToString() == "1")
                {
                    double ttlV = Global.get_Accnt_BalsSumRcsv(int.Parse(dtst.Tables[0].Rows[i][1].ToString()),
            this.asAtDteTextBox.Text);

                    ListViewItem nwItem = new ListViewItem(new string[] {
             (1 + cntr).ToString(),
              "",
              "",
        dtst.Tables[0].Rows[i][2].ToString().ToUpper()+"."+dtst.Tables[0].Rows[i][3].ToString().ToUpper(),
        "",dtst.Tables[0].Rows[i][1].ToString(),ttlV.ToString("#,##0.00")});
                    if (this.blsSmmryCheckBox.Checked == false
                      || dtst.Tables[0].Rows[i][2].ToString().Substring(0, 1) != " ")
                    {
                        nwItem.UseItemStyleForSubItems = true;
                        nwItem.BackColor = Color.WhiteSmoke;
                        nwItem.Font = new Font("Tahoma", 8.5F, FontStyle.Bold);
                    }
                    this.blsListView.Items.Add(nwItem);
                    cntr++;
                }
                else
                {
                    double netamnt = 0;
                    double.TryParse(dtst.Tables[0].Rows[i][4].ToString(), out netamnt);
                    if (netamnt == 0 && this.hideZeroAccntCheckBox3.Checked == true)
                    {
                        continue;
                    }
                    if (dtst.Tables[0].Rows[i][6].ToString() == "A")
                    {
                        bls_assetsum += netamnt;
                    }
                    else if (dtst.Tables[0].Rows[i][6].ToString() == "EQ")
                    {
                        bls_networthsum += netamnt;
                    }
                    else if (dtst.Tables[0].Rows[i][6].ToString() == "L")
                    {
                        bls_liabltysum += netamnt;
                    }

                    if (this.blsSmmryCheckBox.Checked == true)
                    {
                        continue;
                    }
                    ListViewItem nwItem = new ListViewItem(new string[] {
             (1 + cntr).ToString(),"",
             dtst.Tables[0].Rows[i][2].ToString(),
            dtst.Tables[0].Rows[i][2].ToString().ToUpper()+"."+dtst.Tables[0].Rows[i][3].ToString(),
             netamnt.ToString("#,##0.00"),
             dtst.Tables[0].Rows[i][1].ToString(),""});
                    this.blsListView.Items.Add(nwItem);
                    cntr++;
                }
                System.Windows.Forms.Application.DoEvents();
            }
            bls_assetsum = Math.Round(bls_assetsum, 2);
            bls_networthsum = Math.Round(bls_networthsum, 2);
            bls_liabltysum = Math.Round(bls_liabltysum, 2);

            ListViewItem nwItem1 = new ListViewItem(new string[] {
             "",
             "","","","","",""});
            this.blsListView.Items.Add(nwItem1);

            ListViewItem nwItem2 = new ListViewItem(new string[] {
             "",
             "","","TOTAL ASSETS = ",(bls_assetsum).ToString("#,##0.00"),"","",""});
            nwItem2.BackColor = Color.WhiteSmoke;
            nwItem2.Font = new Font("Tahoma", 10, FontStyle.Bold | FontStyle.Underline);
            nwItem2.ForeColor = Color.Blue;
            this.blsListView.Items.Add(nwItem2);

            ListViewItem nwItem3 = new ListViewItem(new string[] {
             "",
             "","","NET WORTH = ",(bls_networthsum).ToString("#,##0.00"),"","",""});
            nwItem3.BackColor = Color.WhiteSmoke;
            nwItem3.Font = new Font("Tahoma", 10, FontStyle.Bold);
            this.blsListView.Items.Add(nwItem3);

            ListViewItem nwItem4 = new ListViewItem(new string[] {
             "",
             "","","TOTAL LIABILITIES = ",(bls_liabltysum).ToString("#,##0.00"),"","",""});
            nwItem4.BackColor = Color.WhiteSmoke;
            nwItem4.Font = new Font("Tahoma", 10, FontStyle.Bold);
            this.blsListView.Items.Add(nwItem4);

            ListViewItem nwItem5 = new ListViewItem(new string[] {
             "",
             "","","NET WORTH + LIABILITIES = ",(bls_networthsum + bls_liabltysum).ToString("#,##0.00"),"","",""});
            nwItem5.BackColor = Color.WhiteSmoke;
            nwItem5.Font = new Font("Tahoma", 10, FontStyle.Bold | FontStyle.Underline);
            nwItem5.ForeColor = Color.Blue;
            this.blsListView.Items.Add(nwItem5);

            ListViewItem nwItem6 = new ListViewItem(new string[] {
             "",
             "","","IMBALANCE (A - EQ - L)= ",(bls_assetsum-bls_networthsum - bls_liabltysum).ToString("#,##0.00"),"","",""});
            nwItem6.BackColor = Color.WhiteSmoke;
            nwItem6.Font = new Font("Tahoma", 10, FontStyle.Bold | FontStyle.Underline);
            nwItem6.ForeColor = Color.Blue;
            this.blsListView.Items.Add(nwItem6);

            this.blsProgressBar.Value = 100;
            this.statusLoadLabel.Visible = false;
            this.statusLoadPictureBox.Visible = false;
            this.blsListView.Visible = true;
            System.Windows.Forms.Application.DoEvents();

            this.obey_bls_evnts = true;
        }

        private void blsGenRptButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[6]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            this.blsGenRptButton.Enabled = false;
            System.Windows.Forms.Application.DoEvents();

            this.populateBlsDet();
            this.blsGenRptButton.Enabled = true;
            this.blsListView.Focus();
        }

        private void blsExptExclButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcelSelective(this.blsListView, this.blsGroupBox.Text);
        }

        private void exptExclBlsMMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcelSelective(this.blsListView, this.blsGroupBox.Text);
        }

        private void asAtDteButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.selectDate(ref this.asAtDteTextBox);
            if (this.asAtDteTextBox.Text.Length > 11)
            {
                this.asAtDteTextBox.Text = this.asAtDteTextBox.Text.Substring(0, 11) + " 23:59:59";
            }
        }

        private void vwSQLBlsMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(Global.mnFrm.bls_SQL, 10);
        }

        private void vwTrnsBlsMenuItem_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[2]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.blsListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select an Account First!", 0);
                return;
            }
            else
            {
                int accIDIn = int.Parse(this.blsListView.SelectedItems[0].SubItems[5].Text);
                string isPrnt = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_chart_of_accnts", "accnt_id", "(CASE WHEN is_prnt_accnt='1' THEN is_prnt_accnt ELSE has_sub_ledgers END)", accIDIn);
                if (isPrnt == "1")
                {
                    this.tbalsAcctIDTextBox.Text = accIDIn.ToString();
                    this.tbalsAcctNmTextBox.Text = Global.mnFrm.cmCde.getAccntNum(accIDIn) +
                      "." + Global.mnFrm.cmCde.getAccntName(accIDIn);
                    this.smmryTBalsCheckBox.Checked = false;
                    this.tbalDteTextBox.Text = this.asAtDteTextBox.Text;
                    this.finStmntsTabControl.SelectedTab = this.tbalTabPage;
                    System.Windows.Forms.Application.DoEvents();
                    this.genRptTrialBalButton_Click(this.genRptTrialBalButton, e);
                }
                else
                {
                    vwTrnsctnsDiag nwDiag = new vwTrnsctnsDiag();
                    nwDiag.my_org_id = Global.mnFrm.cmCde.Org_id;
                    nwDiag.accnt_name = this.blsListView.SelectedItems[0].SubItems[2].Text.Trim();
                    nwDiag.accntid = int.Parse(this.blsListView.SelectedItems[0].SubItems[5].Text);
                    string accTyp = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_chart_of_accnts", "accnt_id", "accnt_type", accIDIn);
                    string lstDte = "01-Jan-1000 00:00:00";
                    if (accTyp == "EX" || accTyp == "R")
                    {
                        lstDte = DateTime.ParseExact(Global.mnFrm.cmCde.getLastPrdClseDate(), "dd-MMM-yyyy HH:mm:ss",
                                     System.Globalization.CultureInfo.InvariantCulture).AddSeconds(1).ToString("dd-MMM-yyyy HH:mm:ss");
                        if (lstDte == "")
                        {
                            lstDte = "01-Jan-1000 00:00:00";
                        }
                    }
                    nwDiag.dte1 = lstDte;
                    nwDiag.dte2 = this.asAtDteTextBox.Text;
                    DialogResult dgres = nwDiag.ShowDialog();
                    if (dgres == DialogResult.OK)
                    {

                    }
                }
            }
        }

        private void exptExclBlsMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcelSelective(this.blsListView, this.blsGroupBox.Text);
        }

        private void blsListView_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.vwTrnsBlsMenuItem_Click(this.vwTrnsBlsMenuItem, ex);
            }
            else if ((e.Control && e.KeyCode == Keys.R) || e.KeyCode == Keys.F5)
            {
                if (this.blsGenRptButton.Enabled == true)
                {
                    this.blsGenRptButton_Click(this.blsGenRptButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                Global.mnFrm.cmCde.listViewKeyDown(this.blsListView, e);
            }
        }

        private void blsListView_DoubleClick(object sender, System.EventArgs e)
        {
            this.vwTrnsBlsMenuItem_Click(this.vwTrnsBlsMenuItem, e);
        }
        #endregion

        #region "ORG BUDGETS..."
        private void loadBdgtPanel()
        {
            this.obey_bdgt_evnts = false;
            if (this.searchInBdgComboBox.SelectedIndex < 0)
            {
                this.searchInBdgComboBox.SelectedIndex = 0;
            }
            if (searchForBdgTextBox.Text.Contains("%") == false)
            {
                this.searchForBdgTextBox.Text = "%" + this.searchForBdgTextBox.Text.Replace(" ", "%") + "%";
            }
            if (this.searchForBdgTextBox.Text == "%%")
            {
                this.searchForBdgTextBox.Text = "%";
            }
            int dsply = 0;
            if (this.dsplySizeBdgComboBox.Text == ""
             || int.TryParse(this.dsplySizeBdgComboBox.Text, out dsply) == false)
            {
                this.dsplySizeBdgComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            this.is_last_bdgt = false;
            this.totl_bdgt = Global.mnFrm.cmCde.Big_Val;
            this.getBdgtPnlData();
            this.obey_bdgt_evnts = true;
        }

        private void getBdgtPnlData()
        {
            this.updtBdgtTotals();
            this.populateBudget();
            this.updtBdgtNavLabels();
        }

        private void updtBdgtTotals()
        {
            Global.mnFrm.cmCde.navFuncts.FindNavigationIndices(
             int.Parse(this.dsplySizeBdgComboBox.Text), this.totl_bdgt);
            if (this.bdgt_cur_indx >= Global.mnFrm.cmCde.navFuncts.totalGroups)
            {
                this.bdgt_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            if (this.bdgt_cur_indx < 0)
            {
                this.bdgt_cur_indx = 0;
            }
            Global.mnFrm.cmCde.navFuncts.currentNavigationIndex = this.bdgt_cur_indx;
        }

        private void updtBdgtNavLabels()
        {
            this.moveFirstBdgButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveFirstBtnStatus();
            this.movePreviousBdgButton.Enabled = Global.mnFrm.cmCde.navFuncts.movePrevBtnStatus();
            this.moveNextBdgButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveNextBtnStatus();
            this.moveLastBdgButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveLastBtnStatus();
            this.positionBdgTextBox.Text = Global.mnFrm.cmCde.navFuncts.displayedRecordsNumbers();
            if (this.is_last_bdgt == true ||
             this.totl_bdgt != Global.mnFrm.cmCde.Big_Val)
            {
                this.totalRecsBdgLabel.Text = Global.mnFrm.cmCde.navFuncts.totalRecordsLabel();
            }
            else
            {
                this.totalRecsBdgLabel.Text = "of Total";
            }
        }

        private void populateBdgtDet(long bdgtID)
        {
            this.obey_bdgt_evnts = false;
            this.clearBdgtInfo();
            this.disableBdgtEdit();
            this.budgetDetListView.Items.Clear();
            this.budgetIDTextBox.Text = this.budgetListView.SelectedItems[0].SubItems[2].Text;
            this.budgetNmTextBox.Text = this.budgetListView.SelectedItems[0].SubItems[1].Text;
            this.budgetDescTextBox.Text = this.budgetListView.SelectedItems[0].SubItems[3].Text;
            this.isBdgActiveCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(
             this.budgetListView.SelectedItems[0].SubItems[4].Text);

            this.loadBdgtDetPanel();
            this.obey_bdgt_evnts = true;
        }

        private void populateBudget()
        {
            this.obey_bdgt_evnts = false;
            DataSet dtst = Global.get_Basic_Bdgt(this.searchForBdgTextBox.Text,
             this.searchInBdgComboBox.Text, this.bdgt_cur_indx, int.Parse(this.dsplySizeBdgComboBox.Text)
             , Global.mnFrm.cmCde.Org_id);
            this.clearBdgtInfo();
            this.disableBdgtEdit();
            this.budgetListView.Items.Clear();
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.last_bdgt_num = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
                ListViewItem nwItem = new ListViewItem(new string[] {
    (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),dtst.Tables[0].Rows[i][0].ToString(),
    dtst.Tables[0].Rows[i][2].ToString(),
    dtst.Tables[0].Rows[i][3].ToString()});
                this.budgetListView.Items.Add(nwItem);
            }
            this.correctBdgtNavLbls(dtst);
            if (this.budgetListView.Items.Count > 0)
            {
                this.obey_bdgt_evnts = true;
                this.budgetListView.Items[0].Selected = true;
            }
            else
            {
                this.totl_bdgtDt = 0;
                this.last_bdgtDt_num = 0;
                this.updtBdgDtTotals();
                this.updtBdgDtNavLabels();
            }
            this.obey_bdgt_evnts = true;
        }

        private void correctBdgtNavLbls(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.bdgt_cur_indx == 0 && totlRecs == 0)
            {
                this.is_last_bdgt = true;
                this.totl_bdgt = 0;
                this.last_bdgt_num = 0;
                this.bdgt_cur_indx = 0;
                this.updtBdgtTotals();
                this.updtBdgtNavLabels();
            }
            else if (this.totl_bdgt == Global.mnFrm.cmCde.Big_Val
          && totlRecs < int.Parse(this.dsplySizeBdgComboBox.Text))
            {
                this.totl_bdgt = this.last_bdgt_num;
                if (totlRecs == 0)
                {
                    this.bdgt_cur_indx -= 1;
                    this.updtBdgtTotals();
                    this.populateBudget();
                }
                else
                {
                    this.updtBdgtTotals();
                }
            }
        }

        private void clearBdgtInfo()
        {
            this.obey_bdgt_evnts = false;
            this.saveBdgButton.Enabled = false;
            this.addBdgButton.Enabled = this.addBudgets;
            this.editBdgButton.Enabled = this.editBudgets;
            this.delBdgButton.Enabled = this.delBudgets;
            this.budgetIDTextBox.Text = "-1";
            this.budgetNmTextBox.Text = "";
            this.budgetDescTextBox.Text = "";
            this.isBdgActiveCheckBox.Checked = false;
            this.budgetDetListView.Items.Clear();
            this.last_bdgtDt_num = 0;

            this.obey_bdgt_evnts = true;
        }

        private void prpareForBdgtEdit()
        {
            this.saveBdgButton.Enabled = true;
            this.budgetNmTextBox.ReadOnly = false;
            this.budgetNmTextBox.BackColor = Color.FromArgb(255, 255, 118);
            this.budgetDescTextBox.ReadOnly = false;
            this.budgetDescTextBox.BackColor = Color.White;
        }

        private void disableBdgtEdit()
        {
            this.addbdgt = false;
            this.editbdgt = false;
            this.budgetNmTextBox.ReadOnly = true;
            this.budgetNmTextBox.BackColor = Color.WhiteSmoke;
            this.budgetDescTextBox.ReadOnly = true;
            this.budgetDescTextBox.BackColor = Color.WhiteSmoke;
        }

        private bool shdObeyBdgtEvts()
        {
            return this.obey_bdgt_evnts;
        }

        private void BdgtPnlNavButtons(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecsBdgLabel.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.is_last_bdgt = false;
                this.bdgt_cur_indx = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.is_last_bdgt = false;
                this.bdgt_cur_indx -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.is_last_bdgt = false;
                this.bdgt_cur_indx += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.is_last_bdgt = true;
                this.totl_bdgt = Global.get_Total_Bdgt(this.searchForBdgTextBox.Text,
                 this.searchInBdgComboBox.Text, Global.mnFrm.cmCde.Org_id);
                this.updtBdgtTotals();
                this.bdgt_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            this.getBdgtPnlData();
        }

        private void loadBdgtDetPanel()
        {
            this.obey_bdgtDt_evnts = false;
            int dsply = 0;
            if (this.searchInBdgtDtComboBox.SelectedIndex < 0)
            {
                this.searchInBdgtDtComboBox.SelectedIndex = 0;
            }

            if (searchForBdgtDtTextBox.Text.Contains("%") == false)
            {
                this.searchForBdgtDtTextBox.Text = "%" + this.searchForBdgtDtTextBox.Text.Replace(" ", "%") + "%";
            }
            if (this.searchForBdgtDtTextBox.Text == "%%")
            {
                this.searchForBdgtDtTextBox.Text = "%";
            }

            if (this.dsplySizeBdgDtComboBox.Text == ""
             || int.TryParse(this.dsplySizeBdgDtComboBox.Text, out dsply) == false)
            {
                this.dsplySizeBdgDtComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            this.bdgtDt_cur_indx = 0;
            this.is_last_bdgtDt = false;
            this.last_bdgtDt_num = 0;
            this.totl_bdgtDt = Global.mnFrm.cmCde.Big_Val;
            this.getBdgDtPnlData();
            this.obey_bdgtDt_evnts = true;
        }

        private void getBdgDtPnlData()
        {
            this.updtBdgDtTotals();
            this.populateBdgDtListVw();
            this.updtBdgDtNavLabels();
        }

        private void updtBdgDtTotals()
        {
            int dsply = 0;
            if (this.dsplySizeBdgDtComboBox.Text == ""
              || int.TryParse(this.dsplySizeBdgDtComboBox.Text, out dsply) == false)
            {
                this.dsplySizeBdgDtComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            this.myNav.FindNavigationIndices(
             long.Parse(this.dsplySizeBdgDtComboBox.Text), this.totl_bdgtDt);
            if (this.bdgtDt_cur_indx >= this.myNav.totalGroups)
            {
                this.bdgtDt_cur_indx = this.myNav.totalGroups - 1;
            }
            if (this.bdgtDt_cur_indx < 0)
            {
                this.bdgtDt_cur_indx = 0;
            }
            this.myNav.currentNavigationIndex = this.bdgtDt_cur_indx;
        }

        private void updtBdgDtNavLabels()
        {
            this.moveFirstBdgDtButton.Enabled = this.myNav.moveFirstBtnStatus();
            this.movePreviousBdgDtButton.Enabled = this.myNav.movePrevBtnStatus();
            this.moveNextBdgDtButton.Enabled = this.myNav.moveNextBtnStatus();
            this.moveLastBdgDtButton.Enabled = this.myNav.moveLastBtnStatus();
            this.positionBdgDtTextBox.Text = this.myNav.displayedRecordsNumbers();
            if (this.is_last_bdgtDt == true ||
             this.totl_bdgtDt != Global.mnFrm.cmCde.Big_Val)
            {
                this.totalRecsBdgDtLabel.Text = this.myNav.totalRecordsLabel();
            }
            else
            {
                this.totalRecsBdgDtLabel.Text = "of Total";
            }
        }

        private void populateBdgDtListVw()
        {
            this.obey_bdgtDt_evnts = false;

            DataSet dtst = Global.get_One_BdgtDt(
              this.searchForBdgtDtTextBox.Text, this.searchInBdgtDtComboBox.Text,
              this.bdgtDt_cur_indx,
             int.Parse(this.dsplySizeBdgDtComboBox.Text), long.Parse(this.budgetIDTextBox.Text));
            this.budgetDetListView.Items.Clear();
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.last_bdgtDt_num = this.myNav.startIndex() + i;
                ListViewItem nwItem = new ListViewItem(new string[] {
    (this.myNav.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),
          dtst.Tables[0].Rows[i][2].ToString(),
    double.Parse(dtst.Tables[0].Rows[i][3].ToString()).ToString("#,##0.00"),
          double.Parse(dtst.Tables[0].Rows[i][4].ToString()).ToString("#,##0.00"),
          ( double.Parse(dtst.Tables[0].Rows[i][3].ToString()) - double.Parse(dtst.Tables[0].Rows[i][4].ToString())).ToString("#,##0.00"),
    dtst.Tables[0].Rows[i][5].ToString(),
    dtst.Tables[0].Rows[i][6].ToString(),
    dtst.Tables[0].Rows[i][7].ToString(),
    dtst.Tables[0].Rows[i][0].ToString(),
    dtst.Tables[0].Rows[i][8].ToString()});
                this.budgetDetListView.Items.Add(nwItem);
            }
            this.correctBdgDtNavLbls(dtst);
            this.obey_bdgtDt_evnts = true;
        }

        private void correctBdgDtNavLbls(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.bdgtDt_cur_indx == 0 && totlRecs == 0)
            {
                this.is_last_bdgtDt = true;
                this.totl_bdgtDt = 0;
                this.last_bdgtDt_num = 0;
                this.bdgtDt_cur_indx = 0;
                this.updtBdgDtTotals();
                this.updtBdgDtNavLabels();
            }
            else if (this.totl_bdgtDt == Global.mnFrm.cmCde.Big_Val
          && totlRecs < long.Parse(this.dsplySizeBdgDtComboBox.Text))
            {
                this.totl_bdgtDt = this.last_bdgtDt_num;
                if (totlRecs == 0)
                {
                    this.bdgtDt_cur_indx -= 1;
                    this.updtBdgDtTotals();
                    this.populateBdgDtListVw();
                }
                else
                {
                    this.updtBdgDtTotals();
                }
            }
        }

        private bool shdObeyBdgDtEvts()
        {
            return this.obey_bdgtDt_evnts;
        }

        private void BdgDtPnlNavButtons(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecsBdgDtLabel.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.is_last_bdgtDt = false;
                this.bdgtDt_cur_indx = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.is_last_bdgtDt = false;
                this.bdgtDt_cur_indx -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.is_last_bdgtDt = false;
                this.bdgtDt_cur_indx += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.is_last_bdgtDt = true;
                this.totl_bdgtDt = Global.get_Total_BdgtDt(
                  this.searchForBdgtDtTextBox.Text, this.searchInBdgtDtComboBox.Text,
                  long.Parse(this.budgetIDTextBox.Text));
                this.updtBdgDtTotals();
                this.bdgtDt_cur_indx = this.myNav.totalGroups - 1;
            }
            this.getBdgDtPnlData();
        }

        private void budgetListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.shdObeyBdgtEvts() == false || this.budgetListView.SelectedItems.Count > 1)
            {
                return;
            }
            if (this.budgetListView.SelectedItems.Count > 0)
            {
                this.populateBdgtDet(long.Parse(this.budgetListView.SelectedItems[0].SubItems[2].Text));
            }
            else
            {
                this.clearBdgtInfo();
                this.disableBdgtEdit();
            }
        }

        private void goBdgButton_Click(object sender, EventArgs e)
        {
            this.loadBdgtPanel();
        }

        private void addBdgButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[22]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            this.clearBdgtInfo();
            this.addbdgt = true;
            this.editbdgt = false;
            this.prpareForBdgtEdit();
            this.addBdgButton.Enabled = false;
            this.editBdgButton.Enabled = false;
            this.delBdgButton.Enabled = false;
            this.budgetDetListView.Items.Clear();
        }

        private void editBdgButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[23]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.budgetIDTextBox.Text == "" || this.budgetIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("No record to Edit!", 0);
                return;
            }
            this.addbdgt = false;
            this.editbdgt = true;
            this.prpareForBdgtEdit();
            this.addBdgButton.Enabled = false;
            this.editBdgButton.Enabled = false;
            this.delBdgButton.Enabled = false;
        }

        private void saveBdgButton_Click(object sender, EventArgs e)
        {
            if (this.addbdgt == true)
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[22]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                     " this action!\nContact your System Administrator!", 0);
                    return;
                }
            }
            else
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[23]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                     " this action!\nContact your System Administrator!", 0);
                    return;
                }
            }
            if (this.budgetNmTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please enter a Budget Name!", 0);
                return;
            }
            long oldBdgtID = Global.mnFrm.cmCde.getBdgtID(this.budgetNmTextBox.Text,
         Global.mnFrm.cmCde.Org_id);
            if (oldBdgtID > 0
             && this.addbdgt == true)
            {
                Global.mnFrm.cmCde.showMsg("Budget Name is already in use in this Organization!", 0);
                return;
            }
            if (oldBdgtID > 0
             && this.editbdgt == true
             && oldBdgtID.ToString() != this.budgetIDTextBox.Text)
            {
                Global.mnFrm.cmCde.showMsg("New Budget Name is already in use in this Organization!", 0);
                return;
            }
            if (this.isBdgActiveCheckBox.Checked)
            {
                Global.setAllBdgtInActive();
            }
            if (this.addbdgt == true)
            {
                Global.createBudget(Global.mnFrm.cmCde.Org_id,
                  this.budgetNmTextBox.Text, this.budgetDescTextBox.Text,
                  this.isBdgActiveCheckBox.Checked);
                this.saveBdgButton.Enabled = false;
                this.addbdgt = false;
                this.editbdgt = false;
                this.editBdgButton.Enabled = this.addBudgets;
                this.addBdgButton.Enabled = this.addBudgets;
                this.delBdgButton.Enabled = this.addBudgets;
                System.Windows.Forms.Application.DoEvents();
                this.loadBdgtPanel();
            }
            else if (this.editbdgt == true)
            {
                Global.updateBudget(long.Parse(this.budgetIDTextBox.Text),
                  this.budgetNmTextBox.Text, this.budgetDescTextBox.Text,
                  this.isBdgActiveCheckBox.Checked);
                this.saveBdgButton.Enabled = false;
                this.editbdgt = false;
                this.editBdgButton.Enabled = this.addBudgets;
                this.addBdgButton.Enabled = this.addBudgets;
                this.loadBdgtPanel();
            }
        }

        private void addBdgtDtMenuItem_Click(object sender, EventArgs e)
        {
            this.addBdgtDtButton_Click(this.addBdgtDtButton, e);
        }

        private void editBdgtDtMenuItem_Click(object sender, EventArgs e)
        {
            this.editBdgtDtButton_Click(this.editBdgtDtButton, e);
        }

        private void exptBdgtMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.budgetListView);
        }

        private void exptBdgtDtMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.budgetDetListView);
        }

        private void exptBdgtTmpltButton_Click(object sender, EventArgs e)
        {
            bdgtTmpDiag nwDiag = new bdgtTmpDiag();
            DialogResult dgres = nwDiag.ShowDialog();
            if (dgres == DialogResult.OK)
            {
                string rspnse = Interaction.InputBox("Indicate whether you want Budget Amounts Included?" +
                  "\r\n1=No Budget Amounts(Empty Template)" +
                  "\r\n2=All Budget Amounts" +
                "\r\n",
                  "Rhomicom", "1", (Global.mnFrm.cmCde.myComputer.Screen.Bounds.Width / 2) - 170,
                  (Global.mnFrm.cmCde.myComputer.Screen.Bounds.Height / 2) - 100);
                if (rspnse.Equals(string.Empty) || rspnse.Equals(null))
                {
                    //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                    return;
                }
                long rsponse = 0;
                bool rsps = long.TryParse(rspnse, out rsponse);
                if (rsps == false)
                {
                    Global.mnFrm.cmCde.showMsg("Invalid Option! Expecting a Number: 1 or 2", 4);
                    return;
                }
                if (rsponse < 1 || rsponse > 2)
                {
                    Global.mnFrm.cmCde.showMsg("Invalid Option! Expecting a Number: 1 or 2", 4);
                    return;
                }
                if (this.budgetIDTextBox.Text == "" || this.budgetIDTextBox.Text == "-1")
                {
                    Global.mnFrm.cmCde.exprtBdgtTmp(nwDiag.startDteTextBox.Text,
                     nwDiag.endDteTextBox.Text, nwDiag.prdTypComboBox.Text);
                }
                else
                {
                    Global.mnFrm.cmCde.exprtBdgtTmp(nwDiag.startDteTextBox.Text,
                   nwDiag.endDteTextBox.Text, nwDiag.prdTypComboBox.Text,
                   long.Parse(this.budgetIDTextBox.Text), rsponse);
                }
            }
        }

        private void imprtBdgtTmpltButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[22]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.budgetIDTextBox.Text == "" ||
              this.budgetIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select the budget to import into!", 0);
                return;
            }

            this.openFileDialog1.RestoreDirectory = true;
            this.openFileDialog1.Filter = "All Files|*.*|Excel Files|*.xls;*.xlsx";
            this.openFileDialog1.FilterIndex = 2;
            this.openFileDialog1.Title = "Select an Excel File to Upload...";
            this.openFileDialog1.FileName = "";
            if (this.openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Global.mnFrm.cmCde.imprtBdgtTmp(long.Parse(this.budgetIDTextBox.Text), this.openFileDialog1.FileName);
            }
            this.populateBdgtDet(long.Parse(this.budgetIDTextBox.Text));
        }

        private void searchForBdgTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.goBdgButton_Click(this.goBdgButton, ex);
            }
        }

        private void positionBdgDtTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                this.BdgDtPnlNavButtons(this.movePreviousBdgDtButton, ex);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
            {
                this.BdgDtPnlNavButtons(this.moveNextBdgDtButton, ex);
            }
        }

        private void positionBdgTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                this.BdgtPnlNavButtons(this.movePreviousBdgButton, ex);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
            {
                this.BdgtPnlNavButtons(this.moveNextBdgButton, ex);
            }
        }

        private void delBdgButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[24]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.budgetListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the record to delete!", 0);
                return;
            }
            long bdgtid = long.Parse(this.budgetListView.SelectedItems[0].SubItems[2].Text);

            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Budget and all its Details?" +
             "\r\nThis action cannot be undone!", 1) == DialogResult.No)
            {
                Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            Global.deleteBdgtDet(bdgtid, this.budgetListView.SelectedItems[0].SubItems[1].Text);
            Global.deleteBdgt(bdgtid, this.budgetListView.SelectedItems[0].SubItems[1].Text);
            this.loadBdgtPanel();
        }

        private void vwSQLBdgButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(Global.mnFrm.bdgt_SQL, 10);
        }

        private void recHstryBdgButton_Click(object sender, EventArgs e)
        {
            if (this.budgetIDTextBox.Text == "-1"
         || this.budgetIDTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please select a Budget First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(Global.get_Bdgt_Rec_Hstry(long.Parse(this.budgetIDTextBox.Text)), 9);
        }

        private void addBdgtMenuItem_Click(object sender, EventArgs e)
        {
            this.addBdgButton_Click(this.addBdgButton, e);
        }

        private void editBdgtMenuItem_Click(object sender, EventArgs e)
        {
            this.editBdgButton_Click(this.editBdgButton, e);
        }

        private void delBdgtMenuItem_Click(object sender, EventArgs e)
        {
            this.delBdgButton_Click(this.delBdgButton, e);
        }

        private void rfrshBdgtMenuItem_Click(object sender, EventArgs e)
        {
            this.goBdgButton_Click(this.goBdgButton, e);
        }

        private void rcHstryBdgtMenuItem_Click(object sender, EventArgs e)
        {
            this.recHstryBdgButton_Click(this.recHstryBdgButton, e);
        }

        private void vwSQLBdgtMenuItem_Click(object sender, EventArgs e)
        {
            this.vwSQLBdgButton_Click(this.vwSQLBdgButton, e);
        }

        private void delBdgtDtMenuItem_Click(object sender, EventArgs e)
        {
            this.delBdgtDtButton_Click(this.delBdgtDtButton, e);
        }

        private void rfrshBdgtDtMenuItem_Click(object sender, EventArgs e)
        {
            this.loadBdgtDetPanel();
        }

        private void rcHstryBdgtDtMenuItem_Click(object sender, EventArgs e)
        {
            if (this.budgetDetListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Budget First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(Global.get_BdgtDt_Rec_Hstry(
              long.Parse(this.budgetDetListView.SelectedItems[0].SubItems[8].Text)), 9);
        }

        private void vwSQLBdgtDtMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(Global.mnFrm.bdgtDet_SQL, 10);
        }
        #endregion

        #region "TRANSACTION TEMPLATES..."
        private void loadTmpltsPanel()
        {
            this.obey_tmplt_evnts = false;
            if (this.searchInTmpltComboBox.SelectedIndex < 0)
            {
                this.searchInTmpltComboBox.SelectedIndex = 0;
            }
            if (searchForTmpltTextBox.Text.Contains("%") == false)
            {
                this.searchForTmpltTextBox.Text = "%" + this.searchForTmpltTextBox.Text.Replace(" ", "%") + "%";
            }
            if (this.searchForTmpltTextBox.Text == "%%")
            {
                this.searchForTmpltTextBox.Text = "%";
            }
            int dsply = 0;
            if (this.dsplySizeTmpltComboBox.Text == ""
             || int.TryParse(this.dsplySizeTmpltComboBox.Text, out dsply) == false)
            {
                this.dsplySizeTmpltComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            this.is_last_tmplt = false;
            this.totl_tmplt = Global.mnFrm.cmCde.Big_Val;
            this.getTmpltPnlData();
            this.obey_tmplt_evnts = true;
        }

        private void getTmpltPnlData()
        {
            this.updtTmpltTotals();
            this.populateTmplt();
            this.updtTmpltNavLabels();
        }

        private void updtTmpltTotals()
        {
            Global.mnFrm.cmCde.navFuncts.FindNavigationIndices(
             int.Parse(this.dsplySizeTmpltComboBox.Text), this.totl_tmplt);
            if (this.tmplt_cur_indx >= Global.mnFrm.cmCde.navFuncts.totalGroups)
            {
                this.tmplt_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            if (this.tmplt_cur_indx < 0)
            {
                this.tmplt_cur_indx = 0;
            }
            Global.mnFrm.cmCde.navFuncts.currentNavigationIndex = this.tmplt_cur_indx;
        }

        private void updtTmpltNavLabels()
        {
            this.moveFirstTmpltButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveFirstBtnStatus();
            this.movePreviousTmpltButton.Enabled = Global.mnFrm.cmCde.navFuncts.movePrevBtnStatus();
            this.moveNextTmpltButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveNextBtnStatus();
            this.moveLastTmpltButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveLastBtnStatus();
            this.positionTmpltTextBox.Text = Global.mnFrm.cmCde.navFuncts.displayedRecordsNumbers();
            if (this.is_last_tmplt == true ||
             this.totl_tmplt != Global.mnFrm.cmCde.Big_Val)
            {
                this.totalRecTmpltLabel.Text = Global.mnFrm.cmCde.navFuncts.totalRecordsLabel();
            }
            else
            {
                this.totalRecTmpltLabel.Text = "of Total";
            }
        }

        private void populateTmpltTrns(long tmpltID)
        {
            this.obey_tmplt_evnts = false;
            DataSet dtst = Global.get_One_Tmplt_Trns(tmpltID);
            this.clearTmpltInfo();
            this.disableTmpltEdit();
            this.tmpltTrnsDetListView.Items.Clear();
            this.tmpltIDTextBox.Text = this.tmpltListView.SelectedItems[0].SubItems[2].Text;
            this.tmpltNameTextBox.Text = this.tmpltListView.SelectedItems[0].SubItems[1].Text;
            this.tmpltDescTextBox.Text = this.tmpltListView.SelectedItems[0].SubItems[3].Text;

            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                //;
                string action = "INCREASE";
                if (dtst.Tables[0].Rows[i][1].ToString() == "D")
                {
                    action = "DECREASE";
                }
                ListViewItem nwItem = new ListViewItem(new string[] {
    (1 + i).ToString(),
    action,dtst.Tables[0].Rows[i][2].ToString(),
    dtst.Tables[0].Rows[i][3].ToString(),
    dtst.Tables[0].Rows[i][4].ToString(),
    dtst.Tables[0].Rows[i][5].ToString(),
    dtst.Tables[0].Rows[i][0].ToString()});
                this.tmpltTrnsDetListView.Items.Add(nwItem);
            }
            this.obey_tmplt_evnts = true;
        }

        private void populateTmpltUsrs(long tmpltID)
        {
            this.obey_tmplt_evnts = false;
            DataSet dtst = Global.get_One_Tmplt_Usrs(tmpltID);
            this.tmpltUsrsListView.Items.Clear();
            this.usrVldStrtDteTextBox.Text = "";
            this.usrVldEndDteTextBox.Text = "";
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                //;
                ListViewItem nwItem = new ListViewItem(new string[] {
    (1 + i).ToString(),
    dtst.Tables[0].Rows[i][0].ToString(),dtst.Tables[0].Rows[i][1].ToString(),
    dtst.Tables[0].Rows[i][2].ToString(),
    dtst.Tables[0].Rows[i][3].ToString(),
    dtst.Tables[0].Rows[i][4].ToString(),
    dtst.Tables[0].Rows[i][5].ToString(),
    dtst.Tables[0].Rows[i][6].ToString()});
                this.tmpltUsrsListView.Items.Add(nwItem);
            }
            if (this.tmpltUsrsListView.Items.Count > 0)
            {
                this.obey_tmplt_evnts = true;
                this.tmpltUsrsListView.Items[0].Selected = true;
            }
            this.obey_tmplt_evnts = true;
        }

        private void populateTmplt()
        {
            this.obey_tmplt_evnts = false;
            DataSet dtst = Global.get_Basic_Tmplt(this.searchForTmpltTextBox.Text,
             this.searchInTmpltComboBox.Text, this.tmplt_cur_indx,
             int.Parse(this.dsplySizeTmpltComboBox.Text)
             , Global.mnFrm.cmCde.Org_id);
            this.clearTmpltInfo();
            this.disableTmpltEdit();
            this.tmpltTrnsDetListView.Items.Clear();
            this.tmpltUsrsListView.Items.Clear();
            this.usrVldEndDteTextBox.Text = "";
            this.usrVldStrtDteTextBox.Text = "";
            this.obey_tmplt_evnts = false;
            this.tmpltListView.Items.Clear();
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.last_tmplt_num = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
                ListViewItem nwItem = new ListViewItem(new string[] {
    (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),dtst.Tables[0].Rows[i][0].ToString(),
    dtst.Tables[0].Rows[i][2].ToString()});
                this.tmpltListView.Items.Add(nwItem);
            }
            this.correctTmpltNavLbls(dtst);
            if (this.tmpltListView.Items.Count > 0)
            {
                this.obey_tmplt_evnts = true;
                this.tmpltListView.Items[0].Selected = true;
            }
            this.obey_tmplt_evnts = true;
        }

        private void correctTmpltNavLbls(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.tmplt_cur_indx == 0 && totlRecs == 0)
            {
                this.is_last_tmplt = true;
                this.totl_tmplt = 0;
                this.last_tmplt_num = 0;
                this.tmplt_cur_indx = 0;
                this.updtTmpltTotals();
                this.updtTmpltNavLabels();
            }
            else if (this.totl_tmplt == Global.mnFrm.cmCde.Big_Val
          && totlRecs < int.Parse(this.dsplySizeTmpltComboBox.Text))
            {
                this.totl_tmplt = this.last_tmplt_num;
                if (totlRecs == 0)
                {
                    this.tmplt_cur_indx -= 1;
                    this.updtTmpltTotals();
                    this.populateTmplt();
                }
                else
                {
                    this.updtTmpltTotals();
                }
            }
        }

        private void clearTmpltInfo()
        {
            this.obey_tmplt_evnts = false;
            this.saveTmpltButton.Enabled = false;
            this.addTmpltButton.Enabled = this.addTmplts;
            this.editTmpltButton.Enabled = this.editTmplts;
            this.deleteTmpltButton.Enabled = this.delTmplts;
            this.tmpltIDTextBox.Text = "-1";
            this.tmpltNameTextBox.Text = "";
            this.tmpltDescTextBox.Text = "";
            this.obey_tmplt_evnts = true;
        }

        private void prpareForTmpltEdit()
        {
            this.saveTmpltButton.Enabled = true;
            this.tmpltNameTextBox.ReadOnly = false;
            this.tmpltNameTextBox.BackColor = Color.FromArgb(255, 255, 118);
            this.tmpltDescTextBox.ReadOnly = false;
            this.tmpltDescTextBox.BackColor = Color.White;
        }

        private void disableTmpltEdit()
        {
            this.addTmplt = false;
            this.editTmplt = false;
            this.tmpltNameTextBox.ReadOnly = true;
            this.tmpltNameTextBox.BackColor = Color.WhiteSmoke;
            this.tmpltDescTextBox.ReadOnly = true;
            this.tmpltDescTextBox.BackColor = Color.WhiteSmoke;
        }

        private bool shdObeyTmpltEvts()
        {
            return this.obey_tmplt_evnts;
        }

        private void TmpltPnlNavButtons(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecTmpltLabel.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.is_last_tmplt = false;
                this.tmplt_cur_indx = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.is_last_tmplt = false;
                this.tmplt_cur_indx -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.is_last_tmplt = false;
                this.tmplt_cur_indx += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.is_last_tmplt = true;
                this.totl_tmplt = Global.get_Total_Tmplts(this.searchForTmpltTextBox.Text,
                 this.searchInTmpltComboBox.Text, Global.mnFrm.cmCde.Org_id);
                this.updtTmpltTotals();
                this.tmplt_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            this.getTmpltPnlData();
        }

        private void tmpltListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.shdObeyTmpltEvts() == false || this.tmpltListView.SelectedItems.Count > 1)
            {
                return;
            }
            if (this.tmpltListView.SelectedItems.Count > 0)
            {
                this.populateTmpltTrns(long.Parse(this.tmpltListView.SelectedItems[0].SubItems[2].Text));
                this.populateTmpltUsrs(long.Parse(this.tmpltListView.SelectedItems[0].SubItems[2].Text));
            }
            else
            {
                this.clearTmpltInfo();
                this.disableTmpltEdit();
            }
        }

        private void usrDte1Button_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[26]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            Global.mnFrm.cmCde.selectDate(ref this.usrVldStrtDteTextBox);
            if (this.tmpltUsrsListView.SelectedItems.Count > 0)
            {
                Global.changeTmpltUsrVldStrDate(long.Parse(this.tmpltUsrsListView.SelectedItems[0].SubItems[5].Text),
                    this.usrVldStrtDteTextBox.Text);
            }
        }

        private void usrDte2Button_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[26]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            Global.mnFrm.cmCde.selectDate(ref this.usrVldEndDteTextBox);
            if (this.tmpltUsrsListView.SelectedItems.Count > 0)
            {
                Global.changeTmpltUsrVldEndDate(long.Parse(this.tmpltUsrsListView.SelectedItems[0].SubItems[5].Text),
                 this.usrVldEndDteTextBox.Text);
            }
        }

        private void tmpltUsrsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.shdObeyTmpltEvts() == false)
            {
                return;
            }
            if (this.tmpltUsrsListView.SelectedItems.Count > 0)
            {
                this.usrVldStrtDteTextBox.Text = this.tmpltUsrsListView.SelectedItems[0].SubItems[6].Text;
                this.usrVldEndDteTextBox.Text = this.tmpltUsrsListView.SelectedItems[0].SubItems[7].Text;
            }
            else
            {
                this.usrVldStrtDteTextBox.Text = "";
                this.usrVldEndDteTextBox.Text = "";
            }
        }

        private void goTmpltButton_Click(object sender, EventArgs e)
        {
            this.loadTmpltsPanel();
        }

        private void addTmpltButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[25]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            this.clearTmpltInfo();
            this.addTmplt = true;
            this.editTmplt = false;
            this.prpareForTmpltEdit();
            this.addTmpltButton.Enabled = false;
            this.editTmpltButton.Enabled = false;
            this.deleteTmpltButton.Enabled = false;
            this.tmpltTrnsDetListView.Items.Clear();
            this.tmpltUsrsListView.Items.Clear();
            this.usrVldEndDteTextBox.Text = "";
            this.usrVldStrtDteTextBox.Text = "";
        }

        private void editTmpltButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[26]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.tmpltIDTextBox.Text == "" || this.tmpltIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("No record to Edit!", 0);
                return;
            }

            this.prpareForTmpltEdit();
            this.addTmpltButton.Enabled = false;
            this.editTmpltButton.Enabled = false;
            this.deleteTmpltButton.Enabled = false;
            this.addTmplt = false;
            this.editTmplt = true;
        }

        private void deleteTmpltButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[27]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.tmpltIDTextBox.Text == "" || this.tmpltIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select the record to delete!", 0);
                return;
            }
            long tmpltid = long.Parse(this.tmpltIDTextBox.Text);

            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Template and all its Details?" +
             "\r\nThis action cannot be undone!", 1) == DialogResult.No)
            {
                Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            Global.deleteTmpltUsrs(tmpltid, this.tmpltNameTextBox.Text);
            Global.deleteTmpltTrns(tmpltid, this.tmpltNameTextBox.Text);
            Global.deleteTmplt(tmpltid, this.tmpltNameTextBox.Text);
            this.loadTmpltsPanel();
        }

        private void saveTmpltButton_Click(object sender, EventArgs e)
        {
            if (this.addTmplt == true)
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[25]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                     " this action!\nContact your System Administrator!", 0);
                    return;
                }
            }
            else
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[26]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                     " this action!\nContact your System Administrator!", 0);
                    return;
                }
            }
            if (this.tmpltNameTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please enter a Template Name!", 0);
                return;
            }
            long oldTmpltID = Global.mnFrm.cmCde.getTrnsTmpltID(this.tmpltNameTextBox.Text,
         Global.mnFrm.cmCde.Org_id);
            if (oldTmpltID > 0
             && this.addTmplt == true)
            {
                Global.mnFrm.cmCde.showMsg("Template Name is already in use in this Organization!", 0);
                return;
            }
            if (oldTmpltID > 0
             && this.editTmplt == true
             && oldTmpltID.ToString() != this.tmpltIDTextBox.Text)
            {
                Global.mnFrm.cmCde.showMsg("New Template Name is already in use in this Organization!", 0);
                return;
            }
            if (this.addTmplt == true)
            {
                Global.createTmplt(Global.mnFrm.cmCde.Org_id,
                 this.tmpltNameTextBox.Text, this.tmpltDescTextBox.Text);
                this.saveTmpltButton.Enabled = false;
                this.addTmplt = false;
                this.editTmplt = false;
                this.editTmpltButton.Enabled = this.editTmplts;
                this.addTmpltButton.Enabled = this.addTmplts;
                this.deleteTmpltButton.Enabled = this.delTmplts;
                System.Windows.Forms.Application.DoEvents();
                this.loadTmpltsPanel();
            }
            else if (this.editTmplt == true)
            {
                Global.updateTmplt(long.Parse(this.tmpltIDTextBox.Text),
                 this.tmpltNameTextBox.Text, this.tmpltDescTextBox.Text);
                this.saveTmpltButton.Enabled = false;
                this.editTmplt = false;
                this.editTmpltButton.Enabled = this.editTmplts;
                this.addTmpltButton.Enabled = this.addTmplts;
                this.loadTmpltsPanel();
            }
        }

        private void vwSQLTmpltButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(Global.mnFrm.tmplt_SQL, 10);
        }

        private void recHstryTmpltButton_Click(object sender, EventArgs e)
        {
            if (this.tmpltIDTextBox.Text == "-1"
         || this.tmpltIDTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please select a Template First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(Global.get_Tmplt_Rec_Hstry(int.Parse(this.tmpltIDTextBox.Text)), 9);
        }

        private void addTmpltTrnsButton_Click(object sender, EventArgs e)
        {
            this.addTmpDirTrnsButton_Click(this.addTmpDirTrnsButton, e);
        }

        private void addTmpltTrnsMenuItem_Click(object sender, EventArgs e)
        {
            this.addTmpDirTrnsButton_Click(this.addTmpDirTrnsButton, e);
        }

        private void editTmpltTrnsMenuItem_Click(object sender, EventArgs e)
        {
            this.editTmpDirTrnsButton_Click(this.editTmpDirTrnsButton, e);
        }

        private void refreshTmpltTrnsMenuItem_Click(object sender, EventArgs e)
        {
            if (this.tmpltListView.SelectedItems.Count > 0)
            {
                this.populateTmpltTrns(long.Parse(this.tmpltListView.SelectedItems[0].SubItems[2].Text));
            }
        }

        private void recHstryTmpTrnsMenuItem_Click(object sender, EventArgs e)
        {
            if (this.tmpltTrnsDetListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Template Transaction First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(Global.get_TmpltTrns_Rec_Hstry(
              int.Parse(this.tmpltTrnsDetListView.SelectedItems[0].SubItems[6].Text)), 9);
        }

        private void vwSQLTmpTrnsMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(Global.mnFrm.tmpltDet_SQL, 10);
        }

        private void addTmpltUsrsButton_Click(object sender, EventArgs e)
        {
            //Users
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[25]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.tmpltIDTextBox.Text == "" || this.tmpltIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select a saved Template First!", 0);
                return;
            }

            string[] selVals = new string[this.tmpltUsrsListView.Items.Count];
            for (int a = 0; a < this.tmpltUsrsListView.Items.Count; a++)
            {
                selVals[a] = this.tmpltUsrsListView.Items[a].SubItems[3].Text;
            }
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
             Global.mnFrm.cmCde.getLovID("Active Users"), ref selVals,
             false, false);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    if (Global.get_Tmplt_Usr(long.Parse(this.tmpltIDTextBox.Text), long.Parse(selVals[i])) <= 0)
                    {
                        Global.createTmpltUsr(long.Parse(selVals[i]), long.Parse(this.tmpltIDTextBox.Text));
                    }
                }
            }
            this.populateTmpltUsrs(long.Parse(this.tmpltIDTextBox.Text));
        }

        private void delTmpltTrnsMenuItem_Click(object sender, EventArgs e)
        {
            this.delTmpDirTrnsButton_Click(this.delTmpDirTrnsButton, e);
        }

        private void exptExclTmpDtMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.tmpltTrnsDetListView);
        }

        private void exptExclTmpltMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.tmpltListView);
        }

        private void exptExclTusrMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.tmpltUsrsListView);
        }

        private void exptTrnsTmpltButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtTrnsTmpltTmp();
        }

        private void imprtTrnsTmpltTmpButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[25]) == false)
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
                Global.mnFrm.cmCde.imprtTrnsTmpltTmp(this.openFileDialog1.FileName);
            }
            this.loadTmpltsPanel();
        }

        private void addTmpltMenuItem_Click(object sender, EventArgs e)
        {
            this.addTmpltButton_Click(this.addTmpltButton, e);
        }

        private void editTmpltMenuItem_Click(object sender, EventArgs e)
        {
            this.editTmpltButton_Click(this.editTmpltButton, e);
        }

        private void delTmpltMenuItem_Click(object sender, EventArgs e)
        {
            this.deleteTmpltButton_Click(this.deleteTmpltButton, e);
        }

        private void rfrshTmpltMenuItem_Click(object sender, EventArgs e)
        {
            this.goTmpltButton_Click(this.goTmpltButton, e);
        }

        private void rcHstryTmpltMenuItem_Click(object sender, EventArgs e)
        {
            this.recHstryTmpltButton_Click(this.recHstryTmpltButton, e);
        }

        private void vwSQLTmpltMenuItem_Click(object sender, EventArgs e)
        {
            this.vwSQLTmpltButton_Click(this.vwSQLTmpltButton, e);
        }

        private void addUsrMenuItem_Click(object sender, EventArgs e)
        {
            this.addTmpltUsrsButton_Click(this.addTmpltUsrsButton, e);
        }

        private void rfrshTUsrMenuItem_Click(object sender, EventArgs e)
        {
            if (this.tmpltIDTextBox.Text == "")
            {
                return;
            }
            this.populateTmpltUsrs(long.Parse(this.tmpltIDTextBox.Text));
        }

        private void rcHstryTusrMenuItem_Click(object sender, EventArgs e)
        {
            if (this.tmpltUsrsListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Template User First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(Global.get_TmpltTUsr_Rec_Hstry(
              long.Parse(this.tmpltUsrsListView.SelectedItems[0].SubItems[5].Text)), 9);

        }

        private void vwSQLTusrMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(Global.mnFrm.tmpltUsrs_SQL, 10);
        }

        private void searchForTmpltTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter ||
              e.KeyCode == Keys.Return)
            {
                this.goTmpltButton_Click(this.goTmpltButton, ex);
            }
        }

        private void positionTmpltTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                this.TmpltPnlNavButtons(this.movePreviousTmpltButton, ex);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
            {
                this.TmpltPnlNavButtons(this.moveNextTmpltButton, ex);
            }
        }

        private void tmpltListView_ItemSelectionChanged(object sender, System.Windows.Forms.ListViewItemSelectionChangedEventArgs e)
        {
            if (this.shdObeyTmpltEvts() == false)
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

        private void budgetListView_ItemSelectionChanged(object sender, System.Windows.Forms.ListViewItemSelectionChangedEventArgs e)
        {
            if (this.shdObeyBdgtEvts() == false)
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

        private void trnsBatchListView_ItemSelectionChanged(object sender, System.Windows.Forms.ListViewItemSelectionChangedEventArgs e)
        {
            if (this.shdObeyTrnsEvts() == false)
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

        private void accntsChrtListView_ItemSelectionChanged(object sender, System.Windows.Forms.ListViewItemSelectionChangedEventArgs e)
        {
            if (this.shdObeyChrtEvts() == false)
            {
                return;
            }
            if (e.IsSelected)
            {
                e.Item.Font = new Font("Tahoma", 9f, FontStyle.Bold);
            }
            else
            {
                e.Item.Font = new Font("Tahoma", 9f, FontStyle.Regular);
            }
        }
        #endregion

        #region "UNCLASSIFIED CODE..."
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

        private void addBdgtDtButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[22]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.budgetIDTextBox.Text == "" ||
              this.budgetIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select a saved Budget First!", 0);
                return;
            }

            addBdgtLineDiag nwDiag = new addBdgtLineDiag();
            nwDiag.orgid = Global.mnFrm.cmCde.Org_id;
            nwDiag.bdgtID = long.Parse(this.budgetIDTextBox.Text);
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
                if (this.budgetListView.SelectedItems.Count > 0)
                {
                    this.populateBdgtDet(long.Parse(this.budgetListView.SelectedItems[0].SubItems[2].Text));
                }
            }
        }

        private void editBdgtDtButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[23]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.budgetDetListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the Budget Line to edit!", 0);
                return;
            }
            addBdgtLineDiag nwDiag = new addBdgtLineDiag();

            nwDiag.orgid = Global.mnFrm.cmCde.Org_id;
            nwDiag.bdgtID = long.Parse(this.budgetIDTextBox.Text);
            nwDiag.bdgtDtID = long.Parse(this.budgetDetListView.SelectedItems[0].SubItems[8].Text);
            nwDiag.startDteTextBox.Text = this.budgetDetListView.SelectedItems[0].SubItems[5].Text;
            nwDiag.endDteTextBox.Text = this.budgetDetListView.SelectedItems[0].SubItems[6].Text;
            double amntLmt = double.Parse(this.budgetDetListView.SelectedItems[0].SubItems[3].Text);
            nwDiag.amntNumericUpDown.Value = (Decimal)amntLmt;
            nwDiag.actionComboBox.SelectedItem = this.budgetDetListView.SelectedItems[0].SubItems[7].Text;
            nwDiag.accntIDTextBox.Text = this.budgetDetListView.SelectedItems[0].SubItems[9].Text;
            nwDiag.accntNumTextBox.Text = this.budgetDetListView.SelectedItems[0].SubItems[1].Text;
            nwDiag.accntNameTextBox.Text = this.budgetDetListView.SelectedItems[0].SubItems[2].Text;

            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
                if (this.budgetListView.SelectedItems.Count > 0)
                {
                    this.populateBdgtDet(long.Parse(this.budgetListView.SelectedItems[0].SubItems[2].Text));
                }
            }
        }

        private void delBdgtDtButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[24]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.budgetDetListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the record to delete!", 0);
                return;
            }

            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Budget Details?" +
             "\r\nThis action cannot be undone!", 1) == DialogResult.No)
            {
                Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            for (int a = 0; a < this.budgetDetListView.SelectedItems.Count; a++)
            {
                Global.deleteOneBdgtDet(long.Parse(this.budgetDetListView.SelectedItems[a].SubItems[8].Text),
                  this.budgetNmTextBox.Text);
            }
            this.populateBdgDtListVw();
        }

        private void addDirTrnsButton_Click(object sender, EventArgs e)
        {
            if (this.backgroundWorker1.IsBusy == true)
            {
                return;
            }
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.batchIDTextBox.Text == "" ||
             this.batchIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select a saved Batch First!", 0);
                return;
            }
            if (this.batchStatusLabel.Text == "Posted")
            {
                Global.mnFrm.cmCde.showMsg("Cannot add Transactions to already Posted Batch of Transactions!", 0);
                return;
            }
            if (this.batchSourceLabel.Text != "Manual")
            {
                Global.mnFrm.cmCde.showMsg("Cannot add Transactions to Batches \r\nthat came from other Modules!", 0);
                return;
            }
            addTrnsDiag nwDiag = new addTrnsDiag();
            nwDiag.orgid = Global.mnFrm.cmCde.Org_id;
            nwDiag.batchid = long.Parse(this.batchIDTextBox.Text);
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
                if (this.trnsBatchListView.SelectedItems.Count > 0)
                {
                    this.populateTrnsDet(long.Parse(this.trnsBatchListView.SelectedItems[0].SubItems[2].Text));
                }
            }
        }

        private void addTrnsFrmTmpButton_Click(object sender, EventArgs e)
        {
            if (this.backgroundWorker1.IsBusy == true)
            {
                return;
            }
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[20]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.batchIDTextBox.Text == "" ||
          this.batchIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select a saved Batch First!", 0);
                return;
            }
            if (this.batchStatusLabel.Text == "Posted")
            {
                Global.mnFrm.cmCde.showMsg("Cannot add Transactions to already Posted Batch of Transactions!", 0);
                return;
            }
            if (this.batchSourceLabel.Text != "Manual")
            {
                Global.mnFrm.cmCde.showMsg("Cannot add Transactions to Batches \r\nthat came from other Modules!", 0);
                return;
            }
            addFromTmpltDiag nwDiag = new addFromTmpltDiag();
            nwDiag.my_org_id = Global.mnFrm.cmCde.Org_id;
            nwDiag.batchid = long.Parse(this.batchIDTextBox.Text);
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
                this.populateTrnsDet(long.Parse(this.batchIDTextBox.Text));
                //this.loadAccntTrnsPanel();
            }
        }

        private void editTrnsButton_Click(object sender, EventArgs e)
        {
            if (this.backgroundWorker1.IsBusy == true)
            {
                return;
            }
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[18]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.batchStatusLabel.Text == "Posted")
            {
                Global.mnFrm.cmCde.showMsg("Cannot edit Transactions in an already\r\nPosted Batch of Transactions!", 0);
                return;
            }
            if (this.batchSourceLabel.Text != "Manual")
            {
                Global.mnFrm.cmCde.showMsg("Cannot edit Transaction Batches \r\nthat came from other Modules!", 0);
                return;
            }
            if (this.trnsDetListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the transaction to edit!", 0);
                return;
            }
            else if (this.trnsDetListView.SelectedItems[0].SubItems[1].Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please select the Transactions to Delete", 0);
                return;
            }
            if (this.trnsDetListView.SelectedItems[0].SubItems[8].Text == "")
            {
                return;
            }

            addTrnsDiag nwDiag = new addTrnsDiag();
            nwDiag.orgid = Global.mnFrm.cmCde.Org_id;
            nwDiag.batchid = long.Parse(this.batchIDTextBox.Text);
            nwDiag.trnsIDTextBox.Text = this.trnsDetListView.SelectedItems[0].SubItems[8].Text;
            nwDiag.trnsDescTextBox.Text = this.trnsDetListView.SelectedItems[0].SubItems[3].Text;
            nwDiag.refDocNumTextBox.Text = this.trnsDetListView.SelectedItems[0].SubItems[22].Text;
            nwDiag.trnsDateTextBox.Text = this.trnsDetListView.SelectedItems[0].SubItems[7].Text;
            string dbtOrCrdt = Global.mnFrm.cmCde.getGnrlRecNm(
         "accb.accb_trnsctn_details", "transctn_id", "dbt_or_crdt", long.Parse(nwDiag.trnsIDTextBox.Text));

            double dbtAmnt = double.Parse(this.trnsDetListView.SelectedItems[0].SubItems[4].Text);
            double crdtAmnt = double.Parse(this.trnsDetListView.SelectedItems[0].SubItems[5].Text);
            if (dbtOrCrdt == "C")
            {
                nwDiag.incrsDcrsComboBox.SelectedItem = Global.incrsOrDcrsAccnt(
                 int.Parse(this.trnsDetListView.SelectedItems[0].SubItems[12].Text), "Credit");
                nwDiag.funcCurAmntNumUpDwn.Value = (decimal)crdtAmnt;
            }
            else
            {
                nwDiag.incrsDcrsComboBox.SelectedItem = Global.incrsOrDcrsAccnt(
             int.Parse(this.trnsDetListView.SelectedItems[0].SubItems[12].Text), "Debit");
                nwDiag.funcCurAmntNumUpDwn.Value = (decimal)dbtAmnt;
            }
            nwDiag.accntIDTextBox.Text = this.trnsDetListView.SelectedItems[0].SubItems[12].Text;
            nwDiag.accntNumTextBox.Text = this.trnsDetListView.SelectedItems[0].SubItems[1].Text;
            nwDiag.accntNameTextBox.Text = this.trnsDetListView.SelectedItems[0].SubItems[2].Text;
            nwDiag.funcCurrIDTextBox.Text = this.trnsDetListView.SelectedItems[0].SubItems[10].Text;
            nwDiag.funcCurrTextBox.Text = this.trnsDetListView.SelectedItems[0].SubItems[6].Text;

            nwDiag.amntNumericUpDown.Value = decimal.Parse(this.trnsDetListView.SelectedItems[0].SubItems[14].Text);
            nwDiag.crncyIDTextBox.Text = this.trnsDetListView.SelectedItems[0].SubItems[16].Text;
            nwDiag.crncyTextBox.Text = this.trnsDetListView.SelectedItems[0].SubItems[15].Text;

            nwDiag.accntCurrNumUpDwn.Value = decimal.Parse(this.trnsDetListView.SelectedItems[0].SubItems[17].Text);
            nwDiag.accntCurrIDTextBox.Text = this.trnsDetListView.SelectedItems[0].SubItems[19].Text;
            nwDiag.acntCurrTextBox.Text = this.trnsDetListView.SelectedItems[0].SubItems[18].Text;

            nwDiag.funcCurRateNumUpDwn.Value = decimal.Parse(this.trnsDetListView.SelectedItems[0].SubItems[20].Text);
            nwDiag.accntCurRateNumUpDwn.Value = decimal.Parse(this.trnsDetListView.SelectedItems[0].SubItems[21].Text);

            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
                if (this.trnsBatchListView.SelectedItems.Count > 0)
                {
                    this.populateTrnsDet(long.Parse(this.trnsBatchListView.SelectedItems[0].SubItems[2].Text));
                }
            }
        }

        private void delTrnsButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[19]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.batchStatusLabel.Text == "Posted")
            {
                Global.mnFrm.cmCde.showMsg("Cannot Delete Transactions from an already\r\n Posted Batch of Transactions!", 0);
                return;
            }

            if (this.trnsDetListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the Transactions to Delete", 0);
                return;
            }
            else if (this.trnsDetListView.SelectedItems[0].SubItems[1].Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please select the Transactions to Delete", 0);
                return;
            }

            int suspns_accnt = Global.get_Suspns_Accnt(Global.mnFrm.cmCde.Org_id);

            if (Global.mnFrm.cmCde.showMsg("NB: Only Suspense Account Transactions can be Deleted in System Generated Batches! \r\n This action cannot be undone!\r\n" +
             "Are you sure you want to delete the selected Transaction(s)?", 1) == DialogResult.No)
            {
                Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }

            for (int i = 0; i < this.trnsDetListView.SelectedItems.Count; i++)
            {
                if (this.trnsDetListView.SelectedItems[0].SubItems[8].Text == "")
                {
                    continue;
                }

                if (this.batchSourceLabel.Text == "Manual"
            || int.Parse(this.trnsDetListView.SelectedItems[i].SubItems[12].Text) == suspns_accnt)
                {
                    Global.deleteTransaction(long.Parse(this.trnsDetListView.SelectedItems[i].SubItems[8].Text));
                }
            }
            this.populateTrnsDet(long.Parse(this.batchIDTextBox.Text));
        }

        private void addTmpDirTrnsButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[25]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.tmpltIDTextBox.Text == "" ||
             this.tmpltIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select a saved Template First!", 0);
                return;
            }

            addTrnsTmpltDiag nwDiag = new addTrnsTmpltDiag();
            nwDiag.orgid = Global.mnFrm.cmCde.Org_id;
            nwDiag.tmpltid = long.Parse(this.tmpltIDTextBox.Text);
            nwDiag.trnsDescTextBox.Text = this.tmpltDescTextBox.Text;
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
                if (this.tmpltListView.SelectedItems.Count > 0)
                {
                    this.populateTmpltTrns(long.Parse(this.tmpltListView.SelectedItems[0].SubItems[2].Text));
                }
            }
        }

        private void editTmpDirTrnsButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[26]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.tmpltTrnsDetListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the transaction to edit!", 0);
                return;
            }
            addTrnsTmpltDiag nwDiag = new addTrnsTmpltDiag();
            nwDiag.orgid = Global.mnFrm.cmCde.Org_id;
            nwDiag.tmpltid = long.Parse(this.tmpltIDTextBox.Text);
            nwDiag.trnsIDTextBox.Text = this.tmpltTrnsDetListView.SelectedItems[0].SubItems[6].Text;
            nwDiag.trnsDescTextBox.Text = this.tmpltTrnsDetListView.SelectedItems[0].SubItems[4].Text;
            nwDiag.incrsDcrsComboBox.SelectedItem = this.tmpltTrnsDetListView.SelectedItems[0].SubItems[1].Text;
            nwDiag.accntIDTextBox.Text = this.tmpltTrnsDetListView.SelectedItems[0].SubItems[5].Text;
            nwDiag.accntNumTextBox.Text = this.tmpltTrnsDetListView.SelectedItems[0].SubItems[2].Text;
            nwDiag.accntNameTextBox.Text = this.tmpltTrnsDetListView.SelectedItems[0].SubItems[3].Text;
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
                if (this.tmpltListView.SelectedItems.Count > 0)
                {
                    this.populateTmpltTrns(long.Parse(this.tmpltListView.SelectedItems[0].SubItems[2].Text));
                }
            }
        }

        private void delTmpDirTrnsButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[27]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.tmpltTrnsDetListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the transaction to edit!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE selected Transaction(s)?", 1) == DialogResult.No)
            {
                Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            for (int i = 0; i < this.tmpltTrnsDetListView.SelectedItems.Count; i++)
            {
                Global.deleteTmpltTransaction(long.Parse(this.tmpltTrnsDetListView.SelectedItems[i].SubItems[6].Text));
            }
            if (this.tmpltListView.SelectedItems.Count > 0)
            {
                this.populateTmpltTrns(long.Parse(this.tmpltListView.SelectedItems[0].SubItems[2].Text));
            }
        }

        private void rfrshButton_Click(object sender, EventArgs e)
        {
            this.loadTmpltsPanel();
        }

        private void rfrshBdgtButton_Click(object sender, EventArgs e)
        {
            this.loadBdgtPanel();
        }

        private void rfrshTrnsButton_Click(object sender, EventArgs e)
        {
            if (this.backgroundWorker1.IsBusy == true)
            {
                return;
            }
            this.loadAccntTrnsPanel();
        }

        private void rfrshChrtButton_Click(object sender, EventArgs e)
        {
            this.disableChrtEdit();
            System.Windows.Forms.Application.DoEvents();
            this.loadAccntChrtPanel();
        }

        private void viewAtchmntsButton_Click(object sender, EventArgs e)
        {
            if (this.backgroundWorker1.IsBusy == true)
            {
                return;
            }
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[2]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.batchIDTextBox.Text == "" ||
          this.batchIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select a saved Batch First!", 0);
                return;
            }
            attchmntsDiag nwDiag = new attchmntsDiag();
            if (this.batchStatusLabel.Text == "Posted")
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[15]) == false)
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
            }
            nwDiag.prmKeyID = long.Parse(this.batchIDTextBox.Text);
            nwDiag.fldrNm = Global.mnFrm.cmCde.getAcctngImgsDrctry();
            nwDiag.fldrTyp = 5;
            nwDiag.attchCtgry = 1;
            DialogResult dgres = nwDiag.ShowDialog();
            if (dgres == DialogResult.OK)
            {
            }
        }

        private void searchForBdgtDtTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.refreshBdgtDtButton_Click(this.refreshBdgtDtButton, ex);
            }
        }

        private void refreshBdgtDtButton_Click(object sender, EventArgs e)
        {
            this.loadBdgtDetPanel();
        }


        private void budgetListView_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();

            if (e.Control && e.KeyCode == Keys.S)
            {
                if (this.saveBdgButton.Enabled == true)
                {
                    this.saveBdgButton_Click(this.saveBdgButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                if (this.addBdgButton.Enabled == true)
                {
                    this.addBdgButton_Click(this.addBdgButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                if (this.editBdgButton.Enabled == true)
                {
                    this.editBdgButton_Click(this.editBdgButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                this.resetBdgtButton.PerformClick();
            }
            else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)
            {
                if (this.rfrshBdgtButton.Enabled == true)
                {
                    this.rfrshBdgtButton_Click(this.rfrshBdgtButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.Delete)
            {
                if (this.delBdgButton.Enabled == true)
                {
                    this.delBdgButton_Click(this.delBdgButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                Global.mnFrm.cmCde.listViewKeyDown(this.budgetListView, e);
            }
        }

        private void budgetDetListView_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();

            if (e.Control && e.KeyCode == Keys.S)
            {
                if (this.saveBdgButton.Enabled == true)
                {
                    this.saveBdgButton_Click(this.saveBdgButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                if (this.addBdgtDtButton.Enabled == true)
                {
                    this.addBdgtDtButton_Click(this.addBdgtDtButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                if (this.editBdgtDtButton.Enabled == true)
                {
                    this.editBdgtDtButton_Click(this.editBdgtDtButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                this.resetBdgtButton.PerformClick();
            }
            else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)
            {
                if (this.refreshBdgtDtButton.Enabled == true)
                {
                    this.refreshBdgtDtButton_Click(this.refreshBdgtDtButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.Delete)
            {
                if (this.delBdgtDtButton.Enabled == true)
                {
                    this.delBdgtDtButton_Click(this.delBdgtDtButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                Global.mnFrm.cmCde.listViewKeyDown(this.budgetDetListView, e);
            }
        }

        private void tmpltListView_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();

            if (e.Control && e.KeyCode == Keys.S)
            {
                if (this.saveTmpltButton.Enabled == true)
                {
                    this.saveTmpltButton_Click(this.saveTmpltButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                if (this.addTmpltButton.Enabled == true)
                {
                    this.addTmpltButton_Click(this.addTmpltButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                if (this.editTmpltButton.Enabled == true)
                {
                    this.editTmpltButton_Click(this.editTmpltButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                this.resetTmpltsButton.PerformClick();
            }
            else if ((e.Control && e.KeyCode == Keys.R) || e.KeyCode == Keys.F5)
            {
                if (this.rfrshTmpButton.Enabled == true)
                {
                    this.rfrshButton_Click(this.rfrshTmpButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                Global.mnFrm.cmCde.listViewKeyDown(this.tmpltListView, e);
            }
        }

        private void tmpltTrnsDetListView_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();

            if (e.Control && e.KeyCode == Keys.S)
            {
                if (this.saveTmpltButton.Enabled == true)
                {
                    this.saveTmpltButton_Click(this.saveTmpltButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                if (this.addTmpDirTrnsButton.Enabled == true)
                {
                    this.addTmpDirTrnsButton_Click(this.addTmpDirTrnsButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                if (this.editTmpDirTrnsButton.Enabled == true)
                {
                    this.editTmpDirTrnsButton_Click(this.editTmpDirTrnsButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                this.resetTmpltsButton.PerformClick();
            }
            else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)
            {
                if (this.rfrshTmpButton.Enabled == true)
                {
                    this.rfrshButton_Click(this.rfrshTmpButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                Global.mnFrm.cmCde.listViewKeyDown(this.tmpltTrnsDetListView, e);

            }
        }

        private void tmpltUsrsListView_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();

            if (e.Control && e.KeyCode == Keys.S)
            {
                if (this.saveTmpltButton.Enabled == true)
                {
                    this.saveTmpltButton_Click(this.saveTmpltButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                if (this.addTmpltButton.Enabled == true)
                {
                    this.addTmpltButton_Click(this.addTmpltButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                if (this.editTmpltButton.Enabled == true)
                {
                    this.editTmpltButton_Click(this.editTmpltButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                this.resetTmpltsButton.PerformClick();
            }
            else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)
            {
                if (this.rfrshTmpButton.Enabled == true)
                {
                    this.rfrshButton_Click(this.rfrshTmpButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                Global.mnFrm.cmCde.listViewKeyDown(this.tmpltUsrsListView, e);
            }
        }

        private void tmpltTxtbx_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();

            if (e.Control && e.KeyCode == Keys.S)
            {
                if (this.saveTmpltButton.Enabled == true)
                {
                    this.saveTmpltButton_Click(this.saveTmpltButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                if (this.addTmpltButton.Enabled == true)
                {
                    this.addTmpltButton_Click(this.addTmpltButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                if (this.editTmpltButton.Enabled == true)
                {
                    this.editTmpltButton_Click(this.editTmpltButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                this.resetTmpltsButton.PerformClick();
            }
            else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)
            {
                if (this.rfrshTmpButton.Enabled == true)
                {
                    this.rfrshButton_Click(this.rfrshTmpButton, ex);
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

        private void bdgtTxtbx_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();

            if (e.Control && e.KeyCode == Keys.S)
            {
                if (this.saveBdgButton.Enabled == true)
                {
                    this.saveBdgButton_Click(this.saveBdgButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                if (this.addBdgButton.Enabled == true)
                {
                    this.addBdgButton_Click(this.addBdgButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                if (this.editBdgButton.Enabled == true)
                {
                    this.editBdgButton_Click(this.editBdgButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                this.resetBdgtButton.PerformClick();
            }
            else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)
            {
                if (this.rfrshBdgtButton.Enabled == true)
                {
                    this.rfrshBdgtButton_Click(this.rfrshBdgtButton, ex);
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

        private void trnsBatchTxtbx_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();

            if (e.Control && e.KeyCode == Keys.S)
            {
                if (this.saveTrnsBatchButton.Enabled == true)
                {
                    this.saveTrnsBatchButton_Click(this.saveTrnsBatchButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                if (this.addTrnsBatchButton.Enabled == true)
                {
                    this.addTrnsBatchButton_Click(this.addTrnsBatchButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                if (this.editTrnsBatchButton.Enabled == true)
                {
                    this.editTrnsBatchButton_Click(this.editTrnsBatchButton, ex);
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
                if (this.rfrshTrnsButton.Enabled == true)
                {
                    this.rfrshTrnsButton_Click(this.rfrshTrnsButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.Delete)
            {
                if (this.voidBatchButton.Enabled == true)
                {
                    this.voidBatchButton_Click(this.voidBatchButton, ex);
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

        private void acntsChrtTxtbx_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();

            if (e.Control && e.KeyCode == Keys.S)
            {
                if (this.saveChrtButton.Enabled == true)
                {
                    this.saveChrtButton_Click(this.saveChrtButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                if (this.addChrtButton.Enabled == true)
                {
                    this.addChrtButton_Click(this.addChrtButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                if (this.editChrtButton.Enabled == true)
                {
                    this.editChrtButton_Click(this.editChrtButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                this.resetChrtButton.PerformClick();
            }
            else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)
            {
                if (this.rfrshChrtButton.Enabled == true)
                {
                    this.rfrshChrtButton_Click(this.rfrshChrtButton, ex);
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

        private void trnsBatchListView_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();

            if (e.Control && e.KeyCode == Keys.S)
            {
                if (this.saveTrnsBatchButton.Enabled == true)
                {
                    this.saveTrnsBatchButton_Click(this.saveTrnsBatchButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                if (this.addTrnsBatchButton.Enabled == true)
                {
                    this.addTrnsBatchButton_Click(this.addTrnsBatchButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                if (this.editTrnsBatchButton.Enabled == true)
                {
                    this.editTrnsBatchButton_Click(this.editTrnsBatchButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)
            {
                if (this.rfrshTrnsButton.Enabled == true)
                {
                    this.rfrshTrnsButton_Click(this.rfrshTrnsButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                this.resetTrnsButton.PerformClick();
            }
            else if (e.Control && e.KeyCode == Keys.Delete)
            {
                if (this.voidBatchButton.Enabled == true)
                {
                    this.voidBatchButton_Click(this.voidBatchButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                Global.mnFrm.cmCde.listViewKeyDown(this.trnsBatchListView, e);
            }
        }

        private void trnsDetListView_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();

            if (e.Control && e.KeyCode == Keys.S)
            {
                if (this.saveTrnsBatchButton.Enabled == true)
                {
                    this.saveTrnsBatchButton_Click(this.saveTrnsBatchButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                if (this.addTrnsLstButton.Enabled == true)
                {
                    this.addTrnsLstButton_Click(this.addTrnsLstButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                if (this.editTrnsLstButton.Enabled == true)
                {
                    this.editTrnsLstButton_Click(this.editTrnsLstButton, ex);
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
                if (this.rfrshTrnsButton.Enabled == true)
                {
                    this.rfrshTrnsButton_Click(this.rfrshTrnsButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.Delete)
            {
                if (this.delTrnsButton.Enabled == true)
                {
                    this.delTrnsButton_Click(this.delTrnsButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                Global.mnFrm.cmCde.listViewKeyDown(this.trnsDetListView, e);
            }
        }

        private void trnsSearchListView_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();

            if (e.Control && e.KeyCode == Keys.S)
            {

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)
            {
                if (this.goSrchButton.Enabled == true)
                {
                    this.goSrchButton_Click(this.goSrchButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                this.resetSrchButton.PerformClick();
            }
            else if (e.Control && e.KeyCode == Keys.Delete)
            {

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                Global.mnFrm.cmCde.listViewKeyDown(this.trnsSearchListView, e);
            }
        }

        private void addTrnsLstButton_Click(object sender, EventArgs e)
        {
            if (this.backgroundWorker1.IsBusy == true)
            {
                return;
            }
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.batchIDTextBox.Text == "" ||
             this.batchIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select a saved Batch First!", 0);
                return;
            }
            if (this.batchStatusLabel.Text == "Posted")
            {
                Global.mnFrm.cmCde.showMsg("Cannot add Transactions to already Posted Batch of Transactions!", 0);
                return;
            }
            if (this.batchSourceLabel.Text != "Manual")
            {
                Global.mnFrm.cmCde.showMsg("Cannot add Transactions to Batches \r\nthat came from other Modules!", 0);
                return;
            }
            addTrnsLstDiag nwDiag = new addTrnsLstDiag();
            nwDiag.orgid = Global.mnFrm.cmCde.Org_id;
            nwDiag.batchid = long.Parse(this.batchIDTextBox.Text);
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
                if (this.trnsBatchListView.SelectedItems.Count > 0)
                {
                    this.populateTrnsDet(long.Parse(this.trnsBatchListView.SelectedItems[0].SubItems[2].Text));
                }
            }
        }

        private void editTrnsLstButton_Click(object sender, EventArgs e)
        {
            if (this.backgroundWorker1.IsBusy == true)
            {
                return;
            }
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[18]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.batchStatusLabel.Text == "Posted")
            {
                Global.mnFrm.cmCde.showMsg("Cannot edit Transactions in an already\r\nPosted Batch of Transactions!", 0);
                return;
            }
            if (this.batchSourceLabel.Text != "Manual")
            {
                Global.mnFrm.cmCde.showMsg("Cannot edit Transaction Batches \r\nthat came from other Modules!", 0);
                return;
            }
            if (this.trnsDetListView.SelectedItems.Count <= 1)
            {
                this.selectAllButton.PerformClick();
            }
            if (this.trnsDetListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the transaction to edit!", 0);
                return;
            }
            addTrnsLstDiag nwDiag = new addTrnsLstDiag();
            nwDiag.orgid = Global.mnFrm.cmCde.Org_id;
            nwDiag.batchid = long.Parse(this.batchIDTextBox.Text);
            nwDiag.trnsDateTextBox.Text = this.trnsDetListView.SelectedItems[0].SubItems[7].Text;
            nwDiag.crncyIDTextBox.Text = this.trnsDetListView.SelectedItems[0].SubItems[10].Text;
            nwDiag.crncyTextBox.Text = this.trnsDetListView.SelectedItems[0].SubItems[6].Text;
            //nwDiag.trnsIDS = new long[this.trnsDetListView.SelectedItems.Count];
            for (int i = 0; i < this.trnsDetListView.SelectedItems.Count; i++)
            {
                if (this.trnsDetListView.SelectedItems[i].SubItems[1].Text == "")
                {
                    continue;
                }
                nwDiag.obey_evnts = false;
                nwDiag.trnsDataGridView.RowCount += 1;
                int rowIdx = nwDiag.trnsDataGridView.RowCount - 1;
                nwDiag.trnsDataGridView.Rows[rowIdx].HeaderCell.Value = nwDiag.trnsDataGridView.RowCount.ToString();
                nwDiag.trnsDataGridView.Rows[rowIdx].Cells[0].Value = this.trnsDetListView.SelectedItems[i].SubItems[8].Text;
                nwDiag.trnsDataGridView.Rows[rowIdx].Cells[1].Value = this.trnsDetListView.SelectedItems[i].SubItems[3].Text;
                nwDiag.trnsDataGridView.Rows[rowIdx].Cells[2].Value = this.trnsDetListView.SelectedItems[i].SubItems[22].Text;

                string dbtOrCrdt = Global.mnFrm.cmCde.getGnrlRecNm(
            "accb.accb_trnsctn_details", "transctn_id", "dbt_or_crdt",
            long.Parse(this.trnsDetListView.SelectedItems[i].SubItems[8].Text));

                double dbtAmnt = double.Parse(this.trnsDetListView.SelectedItems[i].SubItems[4].Text);
                double crdtAmnt = double.Parse(this.trnsDetListView.SelectedItems[i].SubItems[5].Text);
                if (dbtOrCrdt == "C")
                {
                    nwDiag.trnsDataGridView.Rows[rowIdx].Cells[3].Value = Global.incrsOrDcrsAccnt(
                     int.Parse(this.trnsDetListView.SelectedItems[i].SubItems[12].Text), "Credit").ToLower().Replace("i", "I").Replace("d", "D");
                    nwDiag.trnsDataGridView.Rows[rowIdx].Cells[16].Value = (decimal)crdtAmnt;
                }
                else
                {
                    nwDiag.trnsDataGridView.Rows[rowIdx].Cells[3].Value = Global.incrsOrDcrsAccnt(
                 int.Parse(this.trnsDetListView.SelectedItems[i].SubItems[12].Text), "Debit").ToLower().Replace("i", "I").Replace("d", "D");
                    nwDiag.trnsDataGridView.Rows[rowIdx].Cells[16].Value = (decimal)dbtAmnt;
                }
                nwDiag.trnsDataGridView.Rows[rowIdx].Cells[4].Value = this.trnsDetListView.SelectedItems[i].SubItems[1].Text +
                  "." + this.trnsDetListView.SelectedItems[i].SubItems[2].Text;
                nwDiag.trnsDataGridView.Rows[rowIdx].Cells[5].Value = this.trnsDetListView.SelectedItems[i].SubItems[12].Text;
                nwDiag.trnsDataGridView.Rows[rowIdx].Cells[6].Value = "...";
                nwDiag.trnsDataGridView.Rows[rowIdx].Cells[8].Value = "...";
                //nwDiag.trnsDataGridView.Rows[rowIdx].Cells[6].Value = this.trnsDetListView.SelectedItems[i].SubItems[2].Text;
                nwDiag.trnsDataGridView.Rows[rowIdx].Cells[21].Value = this.trnsDetListView.SelectedItems[i].SubItems[10].Text;
                nwDiag.trnsDataGridView.Rows[rowIdx].Cells[17].Value = this.trnsDetListView.SelectedItems[i].SubItems[6].Text;
                nwDiag.trnsDataGridView.Rows[rowIdx].Cells[11].Value = "...";
                nwDiag.trnsDataGridView.Rows[rowIdx].Cells[12].Value = this.trnsDetListView.SelectedItems[i].SubItems[7].Text;
                nwDiag.trnsDataGridView.Rows[rowIdx].Cells[13].Value = "...";
                nwDiag.trnsDataGridView.Rows[rowIdx].Cells[14].Value = this.trnsDetListView.SelectedItems[i].SubItems[20].Text;
                nwDiag.trnsDataGridView.Rows[rowIdx].Cells[15].Value = this.trnsDetListView.SelectedItems[i].SubItems[21].Text;

                nwDiag.trnsDataGridView.Rows[rowIdx].Cells[7].Value = this.trnsDetListView.SelectedItems[i].SubItems[14].Text;
                nwDiag.trnsDataGridView.Rows[rowIdx].Cells[9].Value = this.trnsDetListView.SelectedItems[i].SubItems[16].Text;
                nwDiag.trnsDataGridView.Rows[rowIdx].Cells[10].Value = this.trnsDetListView.SelectedItems[i].SubItems[15].Text;

                nwDiag.trnsDataGridView.Rows[rowIdx].Cells[18].Value = this.trnsDetListView.SelectedItems[i].SubItems[17].Text;
                nwDiag.trnsDataGridView.Rows[rowIdx].Cells[19].Value = this.trnsDetListView.SelectedItems[i].SubItems[18].Text;
                nwDiag.trnsDataGridView.Rows[rowIdx].Cells[20].Value = this.trnsDetListView.SelectedItems[i].SubItems[19].Text;

            }

            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
                if (this.trnsBatchListView.SelectedItems.Count > 0)
                {
                    this.populateTrnsDet(long.Parse(this.trnsBatchListView.SelectedItems[0].SubItems[2].Text));
                }
            }
        }

        private void subLedgrDteButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.selectDate(ref this.subledgrDteTextBox);
            if (this.subledgrDteTextBox.Text.Length > 11)
            {
                this.subledgrDteTextBox.Text = this.subledgrDteTextBox.Text.Substring(0, 11) + " 23:59:59";
            }
        }

        private void gnrtSubLdgrButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[4]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            this.gnrtSubLdgrButton.Enabled = false;
            System.Windows.Forms.Application.DoEvents();
            this.populateSubledgerBals(
                  this.subledgrDteTextBox.Text);
            this.gnrtSubLdgrButton.Enabled = true;
            this.subledgerListView.Focus();
        }

        private void exprtSubLdgrButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcelSelective(this.subledgerListView, this.subLdgrGroupBox.Text);
        }

        private void cntrlAccntButton_Click(object sender, EventArgs e)
        {
            if (this.addChrt == false && this.editChrt == false)
            {
                Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                return;
            }
            if (this.accntTypeComboBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please select an Account Type First!", 0);
                return;
            }

            string[] selVals = new string[1];
            selVals[0] = this.cntrlAccntIDTextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
             Global.mnFrm.cmCde.getLovID("Control Accounts"), ref selVals,
             true, false, Global.mnFrm.cmCde.Org_id,
             this.accntTypeComboBox.Text.Substring(0, 2).Trim(), "");
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.cntrlAccntIDTextBox.Text = selVals[i];
                    this.cntrlAccntTextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));
                }
            }
            if (int.Parse(this.accntIDTextBox.Text) > 0)
            {
                Global.updtAccntPrntID(int.Parse(this.accntIDTextBox.Text),
                  int.Parse(this.cntrlAccntIDTextBox.Text));
            }
        }

        private void hasSubldgrCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shdObeyChrtEvts() == false
             || beenToIsEnabled == true)
            {
                beenToIsEnabled = false;
                return;
            }
            beenToIsEnabled = true;
            if (this.addChrt == false && this.editChrt == false)
            {
                this.hasSubldgrCheckBox.Checked = !this.hasSubldgrCheckBox.Checked;
            }
        }

        private void subledgerListView_DoubleClick(object sender, EventArgs e)
        {
            KeyEventArgs ex = new KeyEventArgs(Keys.Enter);
            this.subledgerListView_KeyDown(this.subledgerListView, ex);
        }

        private void subledgerListView_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[2]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                     " this action!\nContact your System Administrator!", 0);
                    return;
                }
                if (this.subledgerListView.SelectedItems.Count <= 0)
                {
                    Global.mnFrm.cmCde.showMsg("Please select an Account First!", 0);
                    return;
                }
                //else if (this.subledgerListView.SelectedItems[0].SubItems[8].Text == "0")
                //{
                //  Global.mnFrm.cmCde.showMsg("Please select a Non-Parent Account!", 0);
                //  return;
                //}
                vwTrnsctnsDiag nwDiag = new vwTrnsctnsDiag();
                nwDiag.my_org_id = Global.mnFrm.cmCde.Org_id;

                char[] w = { '.' };

                string[] nwAccntNm = this.subledgerListView.SelectedItems[0].SubItems[1].Text.Split(w, StringSplitOptions.RemoveEmptyEntries);
                if (nwAccntNm.Length <= 0)
                {
                    nwAccntNm = this.subledgerListView.SelectedItems[0].SubItems[2].Text.Split(w, StringSplitOptions.RemoveEmptyEntries);
                }

                if (nwAccntNm.Length > 0)
                {
                    nwDiag.accnt_num = nwAccntNm[0];
                }
                nwDiag.accnt_name = "";
                nwDiag.accntid = int.Parse(this.subledgerListView.SelectedItems[0].SubItems[7].Text);
                nwDiag.dte1 = "01-Jan-1000 00:00:00";
                nwDiag.dte2 = this.subledgrDteTextBox.Text;
                DialogResult dgres = nwDiag.ShowDialog();
                if (dgres == DialogResult.OK)
                {

                }
            }
            else if ((e.Control && e.KeyCode == Keys.R) || e.KeyCode == Keys.F5)
            {
                if (this.gnrtSubLdgrButton.Enabled == true)
                {
                    this.gnrtSubLdgrButton_Click(this.gnrtSubLdgrButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                Global.mnFrm.cmCde.listViewKeyDown(this.subledgerListView, e);
            }
        }

        private void exprtSubLgdrMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcelSelective(this.subledgerListView, this.subLdgrGroupBox.Text);
        }

        private void vwSublLdgrTrnsMenuItem_Click(object sender, EventArgs e)
        {
            this.subledgerListView_DoubleClick(this.subledgerListView, e);
        }

        private void vwSQLSubLdgrMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(Global.mnFrm.subldgr_SQL, 10);
        }

        private void selectAllButton_Click(object sender, EventArgs e)
        {
            this.trnsDetListView.Focus();
            KeyEventArgs ex = new KeyEventArgs(Keys.Control | Keys.A);
            this.trnsDetListView_KeyDown(this.trnsDetListView, ex);
            this.trnsDetListView.Focus();
        }

        private void validateTrnsMenuItem_Click(object sender, EventArgs e)
        {
            if (this.backgroundWorker1.IsBusy == true)
            {
                return;
            }
            if (this.batchStatusLabel.Text == "Posted")
            {
                Global.mnFrm.cmCde.showMsg("Cannot Check Transactions in an already\r\nPosted Batch of Transactions!", 0);
                return;
            }
            if (this.trnsDetListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the transaction to Check!", 0);
                return;
            }
            if (this.trnsDetListView.SelectedItems[0].SubItems[8].Text == "")
            {
                return;
            }

            int acntID = int.Parse(this.trnsDetListView.SelectedItems[0].SubItems[12].Text);

            string trnsdte = this.trnsDetListView.SelectedItems[0].SubItems[7].Text;
            double dbtAmnt = double.Parse(this.trnsDetListView.SelectedItems[0].SubItems[4].Text);
            double crdtAmnt = double.Parse(this.trnsDetListView.SelectedItems[0].SubItems[5].Text);
            string incrsdrcrs = "";
            double amnt = 0;
            if (dbtAmnt == 0)
            {
                incrsdrcrs = Global.incrsOrDcrsAccnt(
                 acntID, "Credit");
                amnt = crdtAmnt;
            }
            else
            {
                incrsdrcrs = Global.incrsOrDcrsAccnt(acntID, "Debit");
                amnt = dbtAmnt;
            }
            double netAmnt = (double)Global.dbtOrCrdtAccntMultiplier(acntID,
         incrsdrcrs.Substring(0, 1)) * amnt;

            if (!Global.mnFrm.cmCde.isTransPrmttd(acntID,
                  trnsdte, netAmnt))
            {
                Global.mnFrm.cmCde.showMsg("INVALID TRANSACTION!", 0);
            }
            else
            {
                Global.mnFrm.cmCde.showMsg("VALID TRANSACTION!", 3);
            }

        }

        private void exchngRatesButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[51]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                  " this action!\nContact your System Administrator!", 0);
                return;
            }
            exchangeRatesDiag nwdiag = new exchangeRatesDiag();
            DialogResult dgres = nwdiag.ShowDialog();
            if (dgres == DialogResult.OK)
            {
            }

        }

        private void accntCurrButton_Click(object sender, EventArgs e)
        {
            if (this.addChrt == false && this.editChrt == false)
            {
                Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                return;
            }

            int[] selVals = new int[1];
            selVals[0] = int.Parse(this.accntCurrIDTextBox.Text);
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
             Global.mnFrm.cmCde.getLovID("Currencies"), ref selVals,
             true, false);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.accntCurrIDTextBox.Text = selVals[i].ToString();
                    this.accntCrncyNmTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(selVals[i]) +
                      " - " + Global.mnFrm.cmCde.getPssblValDesc(selVals[i]);
                }
            }
            //if (int.Parse(this.accntIDTextBox.Text) > 0)
            //{
            //  Global.updtAccntCurrID(int.Parse(this.accntIDTextBox.Text),
            //    int.Parse(this.accntCurrIDTextBox.Text));
            //}
        }

        private void netBal2NumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (this.netBal2NumericUpDown.Value < 0 &&
             this.isContraCheckBox.Checked == false)
            {
                this.netBal2NumericUpDown.BackColor = Color.Red;
            }
            else if (this.netBal2NumericUpDown.Value > 0 &&
             this.isContraCheckBox.Checked == true)
            {
                this.netBal2NumericUpDown.BackColor = Color.Red;
            }
            else
            {
                this.netBal2NumericUpDown.BackColor = Color.Green;
            }
        }

        private void accntCrncyNmTextBox_Leave(object sender, EventArgs e)
        {
            if (this.txtChngd == false)
            {
                return;
            }
            this.accntCrncyNmLOVSearch();
            this.txtChngd = false;
        }

        private void accntCrncyNmLOVSearch()
        {
            this.txtChngd = false;
            if (!this.shdObeyChrtEvts())
            {
                return;
            }
            if (this.addChrt == false && this.editChrt == false)
            {
                //Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                return;
            }
            if (this.accntCrncyNmTextBox.Text == "")
            {
                this.accntCurrIDTextBox.Text = "-1";
                return;
            }
            if (!this.accntCrncyNmTextBox.Text.Contains("%"))
            {
                this.accntCrncyNmTextBox.Text = "%" + this.accntCrncyNmTextBox.Text.Replace(" ", "%") + "%";
                this.accntCurrIDTextBox.Text = "-1";
            }

            int[] selVals = new int[1];
            selVals[0] = int.Parse(this.accntCurrIDTextBox.Text);
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
             Global.mnFrm.cmCde.getLovID("Currencies"), ref selVals,
             true, false, this.accntCrncyNmTextBox.Text, "Both", true);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.accntCurrIDTextBox.Text = selVals[i].ToString();
                    this.accntCrncyNmTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(selVals[i]) +
                      " - " + Global.mnFrm.cmCde.getPssblValDesc(selVals[i]);
                }
            }
        }

        private void cntrlAccntTextBox_Leave(object sender, EventArgs e)
        {
            if (this.txtChngd == false)
            {
                return;
            }
            this.cntrlAccntLOVSearch();
            this.txtChngd = false;
        }

        private void cntrlAccntLOVSearch()
        {
            this.txtChngd = false;
            if (!this.shdObeyChrtEvts())
            {
                return;
            }
            if (this.addChrt == false && this.editChrt == false)
            {
                //Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                return;
            }
            if (this.accntTypeComboBox.Text == "")
            {
                //Global.mnFrm.cmCde.showMsg("Please select an Account Type First!", 0);
                return;
            }
            if (this.cntrlAccntTextBox.Text == "")
            {
                this.cntrlAccntIDTextBox.Text = "-1";
                return;
            }
            if (!this.cntrlAccntTextBox.Text.Contains("%"))
            {
                this.cntrlAccntTextBox.Text = "%" + this.cntrlAccntTextBox.Text.Replace(" ", "%") + "%";
                this.cntrlAccntIDTextBox.Text = "-1";
            }

            string[] selVals = new string[1];
            selVals[0] = this.cntrlAccntIDTextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
             Global.mnFrm.cmCde.getLovID("Control Accounts"), ref selVals,
             true, false, Global.mnFrm.cmCde.Org_id,
             this.accntTypeComboBox.Text.Substring(0, 2).Trim(), "", this.cntrlAccntTextBox.Text, "Both", true);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.cntrlAccntIDTextBox.Text = selVals[i];
                    this.cntrlAccntTextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));
                }
            }
        }

        private void parentAccntTextBox_Leave(object sender, EventArgs e)
        {
            if (this.txtChngd == false)
            {
                return;
            }
            this.parentAccntLOVSearch();
            this.txtChngd = false;
        }

        private void parentAccntLOVSearch()
        {
            this.txtChngd = false;
            if (!this.shdObeyChrtEvts())
            {
                return;
            }
            if (this.addChrt == false && this.editChrt == false)
            {
                //Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                this.txtChngd = false;
                return;
            }
            if (this.accntTypeComboBox.Text == "")
            {
                //Global.mnFrm.cmCde.showMsg("Please select an Account Type First!", 0);
                return;
            }
            if (this.parentAccntTextBox.Text == "")
            {
                this.parentAccntIDTextBox.Text = "-1";
                return;
            }
            if (!this.parentAccntTextBox.Text.Contains("%"))
            {
                this.parentAccntTextBox.Text = "%" + this.parentAccntTextBox.Text.Replace(" ", "%") + "%";
                this.parentAccntIDTextBox.Text = "-1";
            }

            string[] selVals = new string[1];
            selVals[0] = this.parentAccntIDTextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
             Global.mnFrm.cmCde.getLovID("Parent Accounts"), ref selVals,
             true, false, Global.mnFrm.cmCde.Org_id,
             this.accntTypeComboBox.Text.Substring(0, 2).Trim(), "",
             this.parentAccntTextBox.Text, "Both", true);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.parentAccntIDTextBox.Text = selVals[i];
                    this.parentAccntTextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));
                }
            }
        }

        private void accntTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.shdObeyChrtEvts())
            {
                return;
            }
            if (this.addChrt == false && this.editChrt == false)
            {
                //Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                return;
            }
            if (this.accntTypeComboBox.SelectedIndex >= 0)
            {
                //this.parentAccntTextBox.Enabled = true;
                this.parentAccntTextBox.ReadOnly = false;
                this.parentAccntTextBox.BackColor = Color.White;
                this.cntrlAccntTextBox.ReadOnly = false;
                this.cntrlAccntTextBox.BackColor = Color.White;
                //this.parntAccntButton.Enabled = true;

                //this.cntrlAccntTextBox.Enabled = true;
                //this.cntrlAccntButton.Enabled = true;
            }
            else
            {
                //this.parentAccntTextBox.Enabled = false;
                this.parentAccntTextBox.ReadOnly = true;
                this.parentAccntTextBox.BackColor = Color.WhiteSmoke;
                this.cntrlAccntTextBox.ReadOnly = true;
                this.cntrlAccntTextBox.BackColor = Color.WhiteSmoke;
                //this.parntAccntButton.Enabled = false;

                //this.cntrlAccntTextBox.Enabled = false;
                //this.cntrlAccntButton.Enabled = false;
            }
        }

        public bool txtChngd = false;
        public bool obey_evnts = false;
        public string srchWrd = "";
        public bool autoLoad = false;

        private void coaTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!this.shdObeyChrtEvts())
            {
                return;
            }
            if (this.addChrt == false && this.editChrt == false)
            {
                //Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                return;
            }
            this.txtChngd = true;
        }

        private void resetChrtButton_Click(object sender, EventArgs e)
        {
            this.searchInChrtComboBox.SelectedIndex = 0;
            this.searchForChrtTextBox.Text = "%";
            this.dsplySizeChrtComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            this.chrt_cur_indx = 0;
            this.rfrshChrtButton_Click(this.rfrshChrtButton, e);
        }

        private void resetTrnsButton_Click(object sender, EventArgs e)
        {
            this.searchInTrnsComboBox.SelectedIndex = 0;
            this.searchForTrnsTextBox.Text = "%";
            this.dsplySizeTrnsComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();

            this.dsplySizeDetComboBox.Text = "5000";
            this.trns_cur_indx = 0;
            this.showUnpostedCheckBox.Checked = true;
            this.shwMyBatchesCheckBox.Checked = true;
            this.rfrshTrnsButton_Click(this.rfrshTrnsButton, e);
        }

        private void vldStrtDteTextBox_TextChanged(object sender, EventArgs e)
        {
            this.txtChngd = true;
        }

        private void vldStrtDteTextBox_Leave(object sender, EventArgs e)
        {
            this.beenClicked = false;
            if (this.txtChngd == false)
            {
                return;
            }
            this.txtChngd = false;
            TextBox mytxt = (TextBox)sender;

            if (mytxt.Name == "vldStrtDteTextBox")
            {
                this.trnsDte1LOVSrch();
                this.txtChngd = false;
                //this.loadSrchPanel();
            }
            else if (mytxt.Name == "vldEndDteTextBox")
            {
                this.trnsDte2LOVSrch();
                this.txtChngd = false;
                //this.loadSrchPanel();
            }
            else if (mytxt.Name == "strtDteIMATextBox")
            {
                DateTime dte1 = DateTime.Now;
                bool sccs = DateTime.TryParse(this.strtDteIMATextBox.Text, out dte1);
                if (!sccs)
                {
                    dte1 = DateTime.Now.AddMonths(-24);
                }
                this.strtDteIMATextBox.Text = dte1.ToString("dd-MMM-yyyy HH:mm:ss");
                this.txtChngd = false;
                //this.loadIMADetPanel();
            }
            else if (mytxt.Name == "endDteIMATextBox")
            {
                DateTime dte1 = DateTime.Now;
                bool sccs = DateTime.TryParse(this.endDteIMATextBox.Text, out dte1);
                if (!sccs)
                {
                    dte1 = DateTime.Now.AddMonths(24);
                }
                this.endDteIMATextBox.Text = dte1.ToString("dd-MMM-yyyy HH:mm:ss");
                this.txtChngd = false;
                //this.loadIMADetPanel();
            }
        }

        private void trnsDte1LOVSrch()
        {
            DateTime dte1 = DateTime.Now;
            bool sccs = DateTime.TryParse(this.vldStrtDteTextBox.Text, out dte1);
            if (!sccs)
            {
                dte1 = DateTime.Now.AddMonths(-24);
            }
            this.vldStrtDteTextBox.Text = dte1.ToString("dd-MMM-yyyy HH:mm:ss");
        }

        private void trnsDte2LOVSrch()
        {
            DateTime dte1 = DateTime.Now;
            bool sccs = DateTime.TryParse(this.vldEndDteTextBox.Text, out dte1);
            if (!sccs)
            {
                dte1 = DateTime.Now.AddMonths(24);
            }
            this.vldEndDteTextBox.Text = dte1.ToString("dd-MMM-yyyy HH:mm:ss").Replace("00:00:00", "23:59:59");
        }

        private void isPostedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.obeySrchEvnts)
            {
                this.loadSrchPanel();
            }
        }

        private void tbalDteTextBox_TextChanged(object sender, EventArgs e)
        {
            this.txtChngd = true;
        }

        private void tbalDteTextBox_Leave(object sender, EventArgs e)
        {
            if (this.txtChngd == false)
            {
                return;
            }
            this.txtChngd = false;
            TextBox mytxt = (TextBox)sender;

            finRptsDte1LOVSrch(mytxt);
            this.txtChngd = false;
        }

        private void finRptsDte1LOVSrch(TextBox mytxt)
        {
            DateTime dte1 = DateTime.Now;
            bool sccs = DateTime.TryParse(mytxt.Text, out dte1);
            if (!sccs)
            {
                dte1 = DateTime.Now;
            }
            if (mytxt.Name != "plDate1TextBox")
            {
                mytxt.Text = dte1.ToString("dd-MMM-yyyy HH:mm:ss").Replace("00:00:00", "23:59:59");
            }
            else
            {
                mytxt.Text = dte1.ToString("dd-MMM-yyyy HH:mm:ss");
            }
        }

        private void resetSrchButton_Click(object sender, EventArgs e)
        {
            this.searchInSrchComboBox.SelectedIndex = 4;
            this.searchForSrchTextBox.Text = "%";
            this.dsplySizeSrchComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            this.numericUpDown1.Value = 0;
            this.numericUpDown2.Value = 0;
            this.vldEndDteTextBox.Text = DateTime.ParseExact(
              Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
              System.Globalization.CultureInfo.InvariantCulture).AddMonths(24).ToString("dd-MMM-yyyy 23:59:59");
            this.vldStrtDteTextBox.Text = DateTime.ParseExact(
              Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
              System.Globalization.CultureInfo.InvariantCulture).AddMonths(-24).ToString("01-MMM-yyyy 00:00:00");
            this.cur_srch_idx = 0;
            this.loadSrchPanel();
        }

        private void resetBdgtButton_Click(object sender, EventArgs e)
        {
            this.searchInBdgComboBox.SelectedIndex = 0;
            this.searchForBdgTextBox.Text = "%";
            this.dsplySizeBdgComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            this.searchInBdgtDtComboBox.SelectedIndex = 0;
            this.searchForBdgtDtTextBox.Text = "%";
            this.dsplySizeBdgDtComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();

            this.bdgt_cur_indx = 0;
            this.loadBdgtPanel();
        }

        private void isSuspensCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shdObeyChrtEvts() == false
         || beenToIsEnabled == true)
            {
                beenToIsEnabled = false;
                return;
            }
            beenToIsEnabled = true;
            if (this.addChrt == false && this.editChrt == false)
            {
                this.isSuspensCheckBox.Checked = !this.isSuspensCheckBox.Checked;
            }

        }

        private void correctImblnsButton_Click(object sender, EventArgs e)
        {
            try
            {
                int suspns_accnt = Global.get_Suspns_Accnt(Global.mnFrm.cmCde.Org_id);
                if (suspns_accnt <= -1)
                {
                    Global.mnFrm.cmCde.showMsg("Please define a suspense Account First!", 0);
                    return;
                }
                //if (this.coaAEBalNumericUpDown.Value -
                //  this.coaCRLBalNumericUpDown.Value == 0)
                //{
                //  Global.mnFrm.cmCde.showMsg("There's no Imbalance to correct!", 0);
                //  return;
                //}
                int ret_accnt = Global.get_Rtnd_Erngs_Accnt(Global.mnFrm.cmCde.Org_id);
                int net_accnt = Global.get_Net_Income_Accnt(Global.mnFrm.cmCde.Org_id);
                if (ret_accnt == -1)
                {
                    Global.mnFrm.cmCde.showMsg("Until a Retained Earnings Account is defined\r\n no Transaction can be posted into the Accounting!", 0);
                    return;
                }
                if (net_accnt == -1)
                {
                    Global.mnFrm.cmCde.showMsg("Until a Net Income Account is defined\r\n no Transaction can be posted into the Accounting!", 0);
                    return;
                }

                if (Global.mnFrm.cmCde.showMsg("Are you sure you want to Perform this Action!", 1) == DialogResult.No)
                {
                    return;
                }
                this.waitLabel.Visible = true;
                this.correctImblnsButton.Enabled = false;
                System.Windows.Forms.Application.DoEvents();
                bool isAnyRnng = true;
                do
                {
                    isAnyRnng = Global.isThereANActvActnPrcss("1,2,3,4,5,6", "10 second");
                    System.Windows.Forms.Application.DoEvents();
                }
                while (isAnyRnng == true);


                //bool isAnyRnng = true;
                //int witcntr = 0;
                //do
                //{
                //  witcntr++;
                //  isAnyRnng = Global.isThereANActvActnPrcss("1,2,3,4,5,6", "10 second");
                //  System.Windows.Forms.Application.DoEvents();
                //}
                //while (isAnyRnng == true);

                /*PROCEDURE FOR RELOADING ACCOUNT BALANCES
            1. Correct all Trns Det Net Balance Amount
            2. Get all wrong daily bals values
                 */
                Global.updtActnPrcss(5, 90);
                this.waitLabel.Visible = true;
                System.Windows.Forms.Application.DoEvents();

                Global.mnFrm.cmCde.Extra_Adt_Trl_Info = "";
                DataSet dtst = Global.get_WrongNetBalncs(Global.mnFrm.cmCde.Org_id);

                for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
                {
                    double netAmnt = double.Parse(dtst.Tables[0].Rows[i][8].ToString());
                    long trnsID = long.Parse(dtst.Tables[0].Rows[i][0].ToString());
                    string updtSQL = @"UPDATE accb.accb_trnsctn_details 
SET net_amount=" + netAmnt + @" 
   WHERE transctn_id=" + trnsID;
                    Global.mnFrm.cmCde.updateDataNoParams(updtSQL);

                    Global.updtActnPrcss(5, 90);
                    System.Windows.Forms.Application.DoEvents();
                }
                this.waitLabel.Visible = true;
                System.Windows.Forms.Application.DoEvents();

                dtst = Global.get_WrongBalncs(Global.mnFrm.cmCde.Org_id);
                this.waitLabel.Visible = true;
                System.Windows.Forms.Application.DoEvents();
                for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
                {
                    Global.updtActnPrcss(5, 30);
                    System.Windows.Forms.Application.DoEvents();
                    string acctyp = Global.mnFrm.cmCde.getAccntType(
                     int.Parse(dtst.Tables[0].Rows[i][1].ToString()));

                    double dbt1 = double.Parse(dtst.Tables[0].Rows[i][4].ToString());
                    double crdt1 = double.Parse(dtst.Tables[0].Rows[i][5].ToString());
                    double net1 = double.Parse(dtst.Tables[0].Rows[i][6].ToString());


                    Global.postTransaction(int.Parse(dtst.Tables[0].Rows[i][1].ToString()),
                     dbt1,
                     crdt1,
                     net1,
                     dtst.Tables[0].Rows[i][7].ToString(), -993);


                    if (acctyp == "R")
                    {
                        Global.postTransaction(net_accnt,
                       dbt1,
                       crdt1,
                       net1,
                        dtst.Tables[0].Rows[i][7].ToString(), -993);
                    }
                    else if (acctyp == "EX")
                    {
                        Global.postTransaction(net_accnt,
                       dbt1,
                       crdt1,
                    (double)(-1) * net1,
                        dtst.Tables[0].Rows[i][7].ToString(), -993);
                    }


                    //get control accnt id
                    int cntrlAcntID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_chart_of_accnts", "accnt_id", "control_account_id", int.Parse(dtst.Tables[0].Rows[i][1].ToString())));
                    if (cntrlAcntID > 0)
                    {
                        Global.postTransaction(cntrlAcntID,
                       dbt1,
                       crdt1,
                       net1,
                         dtst.Tables[0].Rows[i][7].ToString(), -993);

                    }
                    //this.reloadOneAcntChrtBals(int.Parse(dtst.Tables[0].Rows[i][1].ToString()), net_accnt);
                }

                Global.updtActnPrcss(5, 50);
                this.reloadAcntChrtBals(net_accnt);

                dtst = Global.get_WrongNetIncmBalncs(Global.mnFrm.cmCde.Org_id);
                this.waitLabel.Visible = true;
                System.Windows.Forms.Application.DoEvents();
                for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
                {
                    Global.updtActnPrcss(5, 30);
                    System.Windows.Forms.Application.DoEvents();
                    string acctyp = Global.mnFrm.cmCde.getAccntType(
                     int.Parse(dtst.Tables[0].Rows[i][1].ToString()));

                    double dbt1 = double.Parse(dtst.Tables[0].Rows[i][4].ToString());
                    double crdt1 = double.Parse(dtst.Tables[0].Rows[i][5].ToString());
                    double net1 = double.Parse(dtst.Tables[0].Rows[i][6].ToString());


                    Global.postTransaction(int.Parse(dtst.Tables[0].Rows[i][1].ToString()),
                     dbt1,
                     crdt1,
                     net1,
                     dtst.Tables[0].Rows[i][7].ToString(), -993);



                    //this.reloadOneAcntChrtBals(int.Parse(dtst.Tables[0].Rows[i][1].ToString()), net_accnt);
                }

                Global.updtActnPrcss(5, 50);
                this.reloadOneAcntChrtBals(net_accnt, net_accnt);

                string errmsg = "";
                decimal aesum = (decimal)Global.get_COA_AESum(Global.mnFrm.cmCde.Org_id);
                decimal crlsum = (decimal)Global.get_COA_CRLSum(Global.mnFrm.cmCde.Org_id);
                System.Windows.Forms.Application.DoEvents();
                if (aesum
                 != crlsum)
                {
                    Global.updtActnPrcss(5, 10);
                    if (this.postIntoSuspnsAccnt(aesum,
                      crlsum, Global.mnFrm.cmCde.Org_id, false, ref errmsg) == false
                      && errmsg != "")
                    {
                        Global.mnFrm.cmCde.showMsg(errmsg, 0);
                    }
                }

                this.reloadOneAcntChrtBals(suspns_accnt, net_accnt);

                //        Global.postTransaction(net_accnt,
                //dbt1,
                //crdt1,
                //net1,
                //dtst.Tables[0].Rows[i][7].ToString(), -993);

                //errmsg = "";
                //aesum = (decimal)Global.get_COA_ASum(Global.mnFrm.cmCde.Org_id);
                //crlsum = (decimal)Global.get_COA_CLSum(Global.mnFrm.cmCde.Org_id);
                //System.Windows.Forms.Application.DoEvents();
                //if (aesum
                // != crlsum)
                //{
                //  Global.updtActnPrcss(5, 10);
                //  if (this.postIntoSuspnsAccnt(aesum,
                //    crlsum, Global.mnFrm.cmCde.Org_id, false, ref errmsg) == false
                //    && errmsg != "")
                //  {
                //    Global.mnFrm.cmCde.showMsg(errmsg, 0);
                //  }
                //}
                //this.reloadOneAcntChrtBals(suspns_accnt, net_accnt);
                ////Get Net Balance on Suspense Account
                //string trnsDate = Global.mnFrm.cmCde.getFrmtdDB_Date_time();
                //double lstNetBals = Global.getAccntLstDailyNetBals(suspns_accnt, trnsDate);
                //double lstDbtBals = Global.getAccntLstDailyDbtBals(suspns_accnt, trnsDate);
                //double lstCrdtBals = Global.getAccntLstDailyCrdtBals(suspns_accnt, trnsDate);
                //if (lstDbtBals
                // != lstCrdtBals)
                //{
                //  Global.updtActnPrcss(5, 10);
                //  if (this.postIntoSuspnsAccnt((decimal)lstDbtBals,
                //    (decimal)lstCrdtBals, Global.mnFrm.cmCde.Org_id, true, ref errmsg) == false
                //    && errmsg != "")
                //  {
                //    Global.mnFrm.cmCde.showMsg(errmsg, 0);
                //  }
                //}

                Global.updtActnPrcss(5, 1);
                this.rfrshChrtButton_Click(this.rfrshChrtButton, e);
            }
            catch (Exception ex)
            {
                this.waitLabel.Visible = false;
                this.correctImblnsButton.Enabled = true;
                System.Windows.Forms.Application.DoEvents();
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                this.rfrshChrtButton_Click(this.rfrshChrtButton, e);
            }
        }

        private bool postIntoSuspnsAccnt(decimal aeVal, decimal crlVal, int orgID, bool isspcl, ref string errmsg)
        {
            try
            {
                int suspns_accnt = Global.get_Suspns_Accnt(orgID);
                int net_accnt = Global.get_Net_Income_Accnt(orgID);
                int ret_accnt = Global.get_Rtnd_Erngs_Accnt(orgID);

                if (suspns_accnt == -1)
                {
                    errmsg += "Please define a suspense Account First before imbalance can be Auto-Corrected!";
                    return false;
                }
                long suspns_batch_id = -999999991;
                int funcCurrID = Global.mnFrm.cmCde.getOrgFuncCurID(orgID);
                decimal dffrnc = Math.Round(aeVal - crlVal, 2);
                string incrsDcrs = "D";
                if (dffrnc < 0)
                {
                    incrsDcrs = "I";
                }
                decimal imbalAmnt = Math.Abs(dffrnc);
                double netAmnt = (double)Global.dbtOrCrdtAccntMultiplier(suspns_accnt,
            incrsDcrs) * (double)imbalAmnt;
                string dateStr = Global.mnFrm.cmCde.getFrmtdDB_Date_time();
                if (!Global.mnFrm.cmCde.isTransPrmttd(suspns_accnt,
                      dateStr, netAmnt))
                {
                    return false;
                }

                if (Global.dbtOrCrdtAccnt(suspns_accnt, incrsDcrs) == "Debit")
                {
                    Global.createTransaction(suspns_accnt,
                        "Correction of Imbalance as at " + dateStr, (double)imbalAmnt,
                        dateStr
                        , funcCurrID, suspns_batch_id, 0.00, netAmnt,
                      (double)imbalAmnt,
                      funcCurrID,
                      (double)imbalAmnt,
                      funcCurrID,
                      (double)1,
                      (double)1, "D", "");
                    //if (isspcl)
                    //{
                    //  Global.createTransaction(ret_accnt,
                    //   "Correction of Imbalance as at " + dateStr, (double)imbalAmnt,
                    //   dateStr
                    //   , funcCurrID, suspns_batch_id, 0.00, netAmnt,
                    // (double)imbalAmnt,
                    // funcCurrID,
                    // (double)imbalAmnt,
                    // funcCurrID,
                    // (double)1,
                    // (double)1, "D");
                    //}
                }
                else
                {
                    Global.createTransaction(suspns_accnt,
                    "Correction of Imbalance as at " + dateStr, 0.00,
                    dateStr, funcCurrID,
                    suspns_batch_id, (double)imbalAmnt, netAmnt,
                (double)imbalAmnt,
                funcCurrID,
                (double)imbalAmnt,
                funcCurrID,
                (double)1,
                (double)1, "C", "");
                }

                DataSet dtst = Global.get_Batch_Trns(suspns_batch_id);

                for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
                {
                    bool hsBnUpdt = Global.hsTrnsUptdAcntBls(
                          long.Parse(dtst.Tables[0].Rows[i][0].ToString()),
                        dtst.Tables[0].Rows[i][6].ToString(),
                          int.Parse(dtst.Tables[0].Rows[i][9].ToString()));
                    if (hsBnUpdt == false)
                    {
                        double dbt1 = double.Parse(dtst.Tables[0].Rows[i][4].ToString());
                        double crdt1 = double.Parse(dtst.Tables[0].Rows[i][5].ToString());
                        double net1 = double.Parse(dtst.Tables[0].Rows[i][10].ToString());

                        Global.postTransaction(int.Parse(dtst.Tables[0].Rows[i][9].ToString()),
                         dbt1,
                         crdt1,
                         net1,
                         dtst.Tables[0].Rows[i][6].ToString(),
                         long.Parse(dtst.Tables[0].Rows[i][0].ToString()));
                        Global.chngeTrnsStatus(long.Parse(dtst.Tables[0].Rows[i][0].ToString()), "1");
                    }
                }

                this.reloadAcntChrtBals(suspns_batch_id, net_accnt);

                return true;
            }
            catch (Exception ex)
            {
                errmsg += ex.Message + "\r\n\r\n" + ex.InnerException.ToString();
                return false;
            }
        }

        private void trnsDetMenuItem_Click(object sender, EventArgs e)
        {
            if (this.trnsDetListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select the transaction to Check!", 0);
                return;
            }
            if (this.trnsDetListView.SelectedItems[0].SubItems[8].Text == "")
            {
                return;
            }

            long trnsID = long.Parse(this.trnsDetListView.SelectedItems[0].SubItems[8].Text);

            trnsAmntBreakDwnDiag nwDiag = new trnsAmntBreakDwnDiag();
            nwDiag.editMode = false;
            nwDiag.trnsaction_id = trnsID;
            if (nwDiag.ShowDialog() == DialogResult.OK)
            {
            }

        }

        private void trnsSearchListView_DoubleClick(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[2]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.trnsSearchListView.SelectedItems.Count <= 0)
            {
                return;
            }
            if (this.trnsSearchListView.SelectedItems[0].SubItems[8].Text == "")
            {
                return;
            }

            long trnsID = long.Parse(this.trnsSearchListView.SelectedItems[0].SubItems[8].Text);
            string srcTrns = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_trnsctn_details", "transctn_id",
              "source_trns_ids", trnsID);
            DialogResult dgres;
            if (srcTrns != ",")
            {
                vwTrnsctnsDiag nwDiag = new vwTrnsctnsDiag();
                nwDiag.my_org_id = Global.mnFrm.cmCde.Org_id;
                nwDiag.accnt_name = "";
                nwDiag.accntid = -1;
                nwDiag.trnsctnID = long.Parse(this.trnsSearchListView.SelectedItems[0].SubItems[8].Text);
                dgres = nwDiag.ShowDialog();
            }
            else
            {
                trnsAmntBreakDwnDiag nwDiag = new trnsAmntBreakDwnDiag();
                nwDiag.editMode = false;
                nwDiag.trnsaction_id = trnsID;
                dgres = nwDiag.ShowDialog();
            }
            if (dgres == DialogResult.OK)
            {

            }
        }

        private void trnsDetListView_DoubleClick(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[2]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.trnsDetListView.SelectedItems.Count <= 0)
            {
                return;
            }
            if (this.trnsDetListView.SelectedItems[0].SubItems[8].Text == "")
            {
                return;
            }

            long trnsID = long.Parse(this.trnsDetListView.SelectedItems[0].SubItems[8].Text);
            string srcTrns = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_trnsctn_details", "transctn_id",
              "source_trns_ids", trnsID);
            DialogResult dgres;
            if (srcTrns != ",")
            {
                vwTrnsctnsDiag nwDiag = new vwTrnsctnsDiag();
                nwDiag.my_org_id = Global.mnFrm.cmCde.Org_id;
                nwDiag.accnt_name = "";
                nwDiag.accntid = -1;
                nwDiag.trnsctnID = trnsID;
                dgres = nwDiag.ShowDialog();
            }
            else
            {
                trnsAmntBreakDwnDiag nwDiag = new trnsAmntBreakDwnDiag();
                nwDiag.editMode = false;
                nwDiag.trnsaction_id = trnsID;
                dgres = nwDiag.ShowDialog();
            }
            if (dgres == DialogResult.OK)
            {

            }
        }

        private void shwMyBatchesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shdObeyTrnsEvts())
            {
                this.rfrshTrnsButton_Click(this.rfrshTrnsButton, e);
            }
        }

        private void showUnpostedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shdObeyTrnsEvts())
            {
                this.rfrshTrnsButton_Click(this.rfrshTrnsButton, e);
            }
        }

        private void crrctImblncButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.backgroundWorker1.IsBusy == true)
                {
                    return;
                }
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                     " this action!\nContact your System Administrator!", 0);
                    return;
                }
                if (this.batchIDTextBox.Text == "" ||
                 this.batchIDTextBox.Text == "-1")
                {
                    Global.mnFrm.cmCde.showMsg("Please select a saved Batch First!", 0);
                    return;
                }
                if (this.batchStatusLabel.Text == "Posted")
                {
                    Global.mnFrm.cmCde.showMsg("Cannot add Transactions to already Posted Batch of Transactions!", 0);
                    return;
                }

                int suspns_accnt = Global.get_Suspns_Accnt(Global.mnFrm.cmCde.Org_id);
                if (suspns_accnt <= -1)
                {
                    Global.mnFrm.cmCde.showMsg("Please define a suspense Account First!", 0);
                    return;
                }
                if (long.Parse(this.batchIDTextBox.Text) <= 0
                  || this.trnsDetListView.SelectedItems.Count <= 0)
                {
                    Global.mnFrm.cmCde.showMsg("Please make sure a Saved Batch is selected \r\nthen Select one of its Transactions First!", 0);
                    return;
                }

                int ret_accnt = Global.get_Rtnd_Erngs_Accnt(Global.mnFrm.cmCde.Org_id);
                int net_accnt = Global.get_Net_Income_Accnt(Global.mnFrm.cmCde.Org_id);
                if (ret_accnt == -1)
                {
                    Global.mnFrm.cmCde.showMsg("Until a Retained Earnings Account is defined\r\n no Transaction can be posted into the Accounting!", 0);
                    return;
                }
                if (net_accnt == -1)
                {
                    Global.mnFrm.cmCde.showMsg("Until a Net Income Account is defined\r\n no Transaction can be posted into the Accounting!", 0);
                    return;
                }

                if (Global.mnFrm.cmCde.showMsg("Are you sure you want to Perform this Action!", 1) == DialogResult.No)
                {
                    return;
                }

                string errmsg = "";
                decimal aesum = (decimal)Global.get_Batch_DbtSum(long.Parse(this.batchIDTextBox.Text));
                decimal crlsum = (decimal)Global.get_Batch_CrdtSum(long.Parse(this.batchIDTextBox.Text));
                System.Windows.Forms.Application.DoEvents();
                if (aesum == crlsum)
                {
                    DataSet dteDtSt = Global.get_Batch_dateSums(long.Parse(this.batchIDTextBox.Text));
                    if (dteDtSt.Tables[0].Rows.Count > 0)
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
                                long suspns_batch_id = long.Parse(this.batchIDTextBox.Text);
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
                                string dateStr = DateTime.ParseExact(dteDtSt.Tables[0].Rows[i][0].ToString(), "yyyy-MM-dd",
                   System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy") + " 00:00:00";
                                //if (!Global.mnFrm.cmCde.isTransPrmttd(suspns_accnt,
                                //      dateStr, netAmnt))
                                //{
                                //  return; ;
                                //}

                                if (Global.dbtOrCrdtAccnt(suspns_accnt, incrsDcrs) == "Debit")
                                {
                                    Global.createTransaction(suspns_accnt,
                                        "Correction of Imbalance in GL Batch " + this.batchNameTextBox.Text, (double)imbalAmnt,
                                        dateStr
                                        , funcCurrID, suspns_batch_id, 0.00, netAmnt,
                                      (double)imbalAmnt,
                                      funcCurrID,
                                      (double)imbalAmnt,
                                      funcCurrID,
                                      (double)1,
                                      (double)1, "D", "");
                                }
                                else
                                {
                                    Global.createTransaction(suspns_accnt,
                                    "Correction of Imbalance as at " + dateStr, 0.00,
                                    dateStr, funcCurrID,
                                    suspns_batch_id, (double)imbalAmnt, netAmnt,
                                (double)imbalAmnt,
                                funcCurrID,
                                (double)imbalAmnt,
                                funcCurrID,
                                (double)1,
                                (double)1, "C", "");
                                }
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
                else
                {
                    int orgID = Global.mnFrm.cmCde.Org_id;
                    if (aesum
                     != crlsum)
                    {
                        long suspns_batch_id = long.Parse(this.batchIDTextBox.Text);
                        int funcCurrID = Global.mnFrm.cmCde.getOrgFuncCurID(orgID);
                        decimal dffrnc = (aesum - crlsum);
                        string incrsDcrs = "D";
                        if (dffrnc < 0)
                        {
                            incrsDcrs = "I";
                        }
                        decimal imbalAmnt = Math.Abs(dffrnc);
                        double netAmnt = (double)Global.dbtOrCrdtAccntMultiplier(suspns_accnt,
                   incrsDcrs) * (double)imbalAmnt;
                        string dateStr = this.trnsDetListView.SelectedItems[0].SubItems[7].Text;
                        //if (!Global.mnFrm.cmCde.isTransPrmttd(suspns_accnt,
                        //      dateStr, netAmnt))
                        //{
                        //  return; ;
                        //}

                        if (Global.dbtOrCrdtAccnt(suspns_accnt, incrsDcrs) == "Debit")
                        {
                            Global.createTransaction(suspns_accnt,
                                "Correction of Imbalance in GL Batch " + this.batchNameTextBox.Text, (double)imbalAmnt,
                                dateStr
                                , funcCurrID, suspns_batch_id, 0.00, netAmnt,
                              (double)imbalAmnt,
                              funcCurrID,
                              (double)imbalAmnt,
                              funcCurrID,
                              (double)1,
                              (double)1, "D", "");
                        }
                        else
                        {
                            Global.createTransaction(suspns_accnt,
                            "Correction of Imbalance as at " + dateStr, 0.00,
                            dateStr, funcCurrID,
                            suspns_batch_id, (double)imbalAmnt, netAmnt,
                        (double)imbalAmnt,
                        funcCurrID,
                        (double)imbalAmnt,
                        funcCurrID,
                        (double)1,
                        (double)1, "C", "");
                        }
                    }
                }
                this.populateTrnsDet(long.Parse(this.batchIDTextBox.Text));
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                this.populateTrnsDet(long.Parse(this.batchIDTextBox.Text));
            }
        }

        private void searchForChrtTextBox_Click(object sender, EventArgs e)
        {
            if (this.beenClicked == true)
            {
                return;
            }
            this.beenClicked = true;
            ToolStripTextBox myTxt = (ToolStripTextBox)sender;
            myTxt.SelectAll();
            //this.searchForChrtTextBox.SelectAll();
        }

        private void searchForChrtTextBox_Enter(object sender, EventArgs e)
        {
            ToolStripTextBox myTxt = (ToolStripTextBox)sender;
            myTxt.SelectAll();
            //this.searchForChrtTextBox.SelectAll();
        }

        private void searchForTrnsTextBox_Click(object sender, EventArgs e)
        {
            this.searchForTrnsTextBox.SelectAll();
            //if (this.beenClicked == false)
            //{
            // this.beenClicked = true;
            //}
        }

        private void searchForSrchTextBox_Click(object sender, EventArgs e)
        {
            this.searchForSrchTextBox.SelectAll();
        }

        private void searchForBdgTextBox_Click(object sender, EventArgs e)
        {
            this.searchForBdgTextBox.SelectAll();
        }

        private void searchForBdgtDtTextBox_Click(object sender, EventArgs e)
        {
            this.searchForBdgtDtTextBox.SelectAll();
        }

        private void searchForTmpltTextBox_Click(object sender, EventArgs e)
        {
            this.searchForTmpltTextBox.SelectAll();
        }

        private void resetTmpltsButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.minimizeMemory();
            this.searchInTmpltComboBox.SelectedIndex = 0;
            this.searchForTmpltTextBox.Text = "%";
            this.dsplySizeTmpltComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();

            this.tmplt_cur_indx = 0;
            this.rfrshButton_Click(this.rfrshTmpButton, e);
        }

        private void vldStrtDteTextBox_Click(object sender, EventArgs e)
        {
            if (this.beenClicked == true)
            {
                return;
            }
            this.beenClicked = true;
            TextBox mytxt = (TextBox)sender;

            if (mytxt.Name == "vldStrtDteTextBox")
            {
                this.vldStrtDteTextBox.SelectAll();
            }
            else if (mytxt.Name == "vldEndDteTextBox")
            {
                this.vldEndDteTextBox.SelectAll();
            }
            else if (mytxt.Name == "strtDteIMATextBox")
            {
                this.strtDteIMATextBox.SelectAll();
            }
            else if (mytxt.Name == "endDteIMATextBox")
            {
                this.endDteIMATextBox.SelectAll();
            }
        }

        private void tbalDteTextBox_Click(object sender, EventArgs e)
        {
            TextBox mytxt = (TextBox)sender;

            if (mytxt.Name == "tbalDteTextBox")
            {
                this.tbalDteTextBox.SelectAll();
            }
            else if (mytxt.Name == "plDate1TextBox")
            {
                this.plDate1TextBox.SelectAll();
            }
            else if (mytxt.Name == "plDate2TextBox")
            {
                this.plDate2TextBox.SelectAll();
            }
            else if (mytxt.Name == "asAtDteTextBox")
            {
                this.asAtDteTextBox.SelectAll();
            }
            else if (mytxt.Name == "subledgrDteTextBox")
            {
                this.subledgrDteTextBox.SelectAll();
            }
        }

        private void accntStmntTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!this.obey_evnts)
            {
                return;
            }
            this.txtChngd = true;
        }

        private void accntStmntTextBox_Leave(object sender, EventArgs e)
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

            if (mytxt.Name == "accntStmntTextBox")
            {
                this.accntStmntTextBox.Text = "";
                this.acctIDStmntTextBox.Text = "-1";
                this.accntStmntButton_Click(this.accntStmntButton, e);
            }
            else if (mytxt.Name == "strtDteAccntStmntTextBox")
            {
                this.strtDteAccntStmntTextBox.Text = Global.mnFrm.cmCde.checkNFormatDate(this.strtDteAccntStmntTextBox.Text).Substring(0, 11) + " 00:00:00";
            }
            else if (mytxt.Name == "endDteAccntStmntTextBox")
            {
                this.endDteAccntStmntTextBox.Text = Global.mnFrm.cmCde.checkNFormatDate(this.endDteAccntStmntTextBox.Text).Replace("00:00:00", "23:59:59");
            }
            this.obey_evnts = true;
            this.txtChngd = false;
            this.srchWrd = "%";
        }

        private void accntStmntTextBox_Click(object sender, EventArgs e)
        {
            TextBox mytxt = (TextBox)sender;

            if (mytxt.Name == "accntStmntTextBox")
            {
                this.accntStmntTextBox.SelectAll();
            }
            else if (mytxt.Name == "strtDteAccntStmntTextBox")
            {
                this.strtDteAccntStmntTextBox.SelectAll();
            }
            else if (mytxt.Name == "endDteAccntStmntTextBox")
            {
                this.endDteAccntStmntTextBox.SelectAll();
            }
        }

        private void strtDteAccntStmntButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.selectDate(ref this.strtDteAccntStmntTextBox);
            if (this.strtDteAccntStmntTextBox.Text.Length > 11)
            {
                this.strtDteAccntStmntTextBox.Text = this.strtDteAccntStmntTextBox.Text.Substring(0, 11) + " 00:00:00";
            }
        }

        private void endDteAccntStmntButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.selectDate(ref this.endDteAccntStmntTextBox);
            if (this.endDteAccntStmntTextBox.Text.Length > 11)
            {
                this.endDteAccntStmntTextBox.Text = this.endDteAccntStmntTextBox.Text.Substring(0, 11) + " 23:59:59";
            }
        }

        private void accntStmntButton_Click(object sender, EventArgs e)
        {
            string[] selVals = new string[1];
            selVals[0] = this.acctIDStmntTextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
              Global.mnFrm.cmCde.getLovID("All Accounts"), ref selVals,
              true, true, Global.mnFrm.cmCde.Org_id,
             this.srchWrd, "Both", true);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.acctIDStmntTextBox.Text = selVals[i];
                    this.accntStmntTextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(selVals[i])) +
                      "." + Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));

                }
            }
        }

        private void genRptAccntStmntButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[4]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.acctIDStmntTextBox.Text == "" || this.acctIDStmntTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please Indicate the Account First!", 0);
                return;
            }

            this.genRptAccntStmntButton.Enabled = false;
            System.Windows.Forms.Application.DoEvents();
            this.populateAccntStmntBals(int.Parse(this.acctIDStmntTextBox.Text),
                  this.strtDteAccntStmntTextBox.Text, this.endDteAccntStmntTextBox.Text);
            this.genRptAccntStmntButton.Enabled = true;
            this.accntStmntListView.Focus();
        }

        private void exptExclAccntStmntButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcelSelective(this.accntStmntListView, this.accntStmntGroupBox.Text);
        }

        private void accntStmntListView_DoubleClick(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[2]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.accntStmntListView.SelectedItems.Count <= 0)
            {
                return;
            }

            if (this.accntStmntListView.SelectedItems[0].SubItems[1].Text == "")
            {
                return;
            }
            vwTrnsctnsDiag nwDiag = new vwTrnsctnsDiag();
            nwDiag.my_org_id = Global.mnFrm.cmCde.Org_id;
            nwDiag.accnt_name = "";
            nwDiag.accntid = -1;
            nwDiag.trnsctnID = long.Parse(this.accntStmntListView.SelectedItems[0].SubItems[1].Text);
            DialogResult dgres = nwDiag.ShowDialog();
            if (dgres == DialogResult.OK)
            {

            }
        }

        private void accntStmntListView_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.accntStmntListView_DoubleClick(this.accntStmntListView, ex);
            }
            else if ((e.Control && e.KeyCode == Keys.R) || e.KeyCode == Keys.F5)
            {
                if (this.genRptAccntStmntButton.Enabled == true)
                {
                    this.genRptAccntStmntButton_Click(this.genRptAccntStmntButton, ex);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                Global.mnFrm.cmCde.listViewKeyDown(this.accntStmntListView, e);
            }
        }

        private void accntStmntTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Enter)
            {
                this.genRptAccntStmntButton.Focus();
            }
        }

        private void subledgrDteTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Enter)
            {
                this.gnrtSubLdgrButton.Focus();
            }
        }

        private void asAtDteTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Enter)
            {
                this.blsGenRptButton.Focus();
            }
        }

        private void plDate1TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Enter)
            {
                this.plGenRptButton.Focus();
            }
        }

        private void tbalDteTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Enter)
            {
                this.genRptTrialBalButton.Focus();
            }
        }

        private void exptExclActStmntMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcelSelective(this.accntStmntListView, this.accntStmntGroupBox.Text);
        }

        private void vwTrnsActStmntMenuItem_Click(object sender, EventArgs e)
        {
            this.accntStmntListView_DoubleClick(this.accntStmntListView, e);
        }

        private void vwSQLActStmntMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(Global.mnFrm.accntStmntSQL, 10);
        }

        private void exptToTmplt2MenuItem_Click(object sender, EventArgs e)
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
            string[] hdngs ={"Transaction Description**","Increase/Decrease**","Account Number**","Account Name",
            "AMOUNT**","Curr.**", "Transaction Date**" };
            for (int a = 0; a < hdngs.Length; a++)
            {
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Font.Bold = true;
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Value2 = hdngs[a].ToUpper();
            }
            for (int a = 0; a < this.trnsSearchListView.Items.Count; a++)
            {
                if (this.trnsSearchListView.Items[a].SubItems[3].Text != "CURRENT DISPLAY'S TOTALS = "
                  && this.trnsSearchListView.Items[a].SubItems[3].Text != "DIFFERENCE = ")
                {
                    string dbtOrCrdt = Global.mnFrm.cmCde.getGnrlRecNm(
               "accb.accb_trnsctn_details", "transctn_id", "dbt_or_crdt",
               long.Parse(this.trnsSearchListView.Items[a].SubItems[8].Text));

                    double dbtAmnt = double.Parse(this.trnsSearchListView.Items[a].SubItems[4].Text);
                    double crdtAmnt = double.Parse(this.trnsSearchListView.Items[a].SubItems[5].Text);
                    double entrdAmnt = 0;
                    string incrsdcrs = "";
                    if (dbtOrCrdt == "C")
                    {
                        incrsdcrs = Global.incrsOrDcrsAccnt(
                         int.Parse(this.trnsSearchListView.Items[a].SubItems[12].Text), "Credit").ToLower().Replace("i", "I").Replace("d", "D");
                        entrdAmnt = crdtAmnt;
                    }
                    else
                    {
                        incrsdcrs = Global.incrsOrDcrsAccnt(
                     int.Parse(this.trnsSearchListView.Items[a].SubItems[12].Text), "Debit").ToLower().Replace("i", "I").Replace("d", "D");
                        entrdAmnt = dbtAmnt;
                    }

                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 2]).Value2 = this.trnsSearchListView.Items[a].SubItems[3].Text;
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 3]).Value2 = incrsdcrs;
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 4]).Value2 = this.trnsSearchListView.Items[a].SubItems[1].Text;
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 5]).Value2 = this.trnsSearchListView.Items[a].SubItems[2].Text;
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 6]).Value2 = entrdAmnt;
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 7]).Value2 = this.trnsSearchListView.Items[a].SubItems[6].Text;
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 8]).Value2 = this.trnsSearchListView.Items[a].SubItems[7].Text;
                }
                else if (this.trnsSearchListView.Items[a].SubItems[3].Text == "CURRENT DISPLAY'S TOTALS = ")
                {
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + (a + 6).ToString() + ":H" + (a + 6).ToString(), Type.Missing)).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + (a + 6).ToString() + ":H" + (a + 6).ToString(), Type.Missing)).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + (a + 6).ToString() + ":H" + (a + 6).ToString(), Type.Missing)).Font.Bold = true;
                }
            }

            Global.mnFrm.cmCde.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 10;
            Global.mnFrm.cmCde.trgtSheets[0].get_Range("A1:A65535", Type.Missing).WrapText = true;

            Global.mnFrm.cmCde.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Columns.AutoFit();
            Global.mnFrm.cmCde.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Rows.AutoFit();
        }

        private void exptToTmplt1MenuItem_Click(object sender, EventArgs e)
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
            string[] hdngs ={"Account Number**","Account Name","Transaction Description**",
            "DEBIT**","CREDIT**","Transaction Date**" };
            for (int a = 0; a < hdngs.Length; a++)
            {
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Font.Bold = true;
                ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Value2 = hdngs[a].ToUpper();
            }
            for (int a = 0; a < this.trnsSearchListView.Items.Count; a++)
            {
                if (this.trnsSearchListView.Items[a].SubItems[3].Text != "CURRENT DISPLAY'S TOTALS = "
                  && this.trnsSearchListView.Items[a].SubItems[3].Text != "DIFFERENCE = ")
                {
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 2]).Value2 = this.trnsSearchListView.Items[a].SubItems[1].Text;
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 3]).Value2 = this.trnsSearchListView.Items[a].SubItems[2].Text;
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 4]).Value2 = this.trnsSearchListView.Items[a].SubItems[3].Text;
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 5]).Value2 = this.trnsSearchListView.Items[a].SubItems[4].Text;
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 6]).Value2 = this.trnsSearchListView.Items[a].SubItems[5].Text;
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 7]).Value2 = this.trnsSearchListView.Items[a].SubItems[7].Text;
                }
                else if (this.trnsSearchListView.Items[a].SubItems[3].Text == "CURRENT DISPLAY'S TOTALS = ")
                {
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + (a + 6).ToString() + ":G" + (a + 6).ToString(), Type.Missing)).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + (a + 6).ToString() + ":G" + (a + 6).ToString(), Type.Missing)).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + (a + 6).ToString() + ":G" + (a + 6).ToString(), Type.Missing)).Font.Bold = true;
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 4]).Value2 = this.trnsSearchListView.Items[a].SubItems[3].Text;
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 5]).Value2 = "=SUM(E6:E" + (a + 5).ToString() + ")";
                    ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 6]).Value2 = "=SUM(F6:F" + (a + 5).ToString() + ")";
                }
            }

            Global.mnFrm.cmCde.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 10;
            Global.mnFrm.cmCde.trgtSheets[0].get_Range("A1:A65535", Type.Missing).WrapText = true;

            Global.mnFrm.cmCde.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Columns.AutoFit();
            Global.mnFrm.cmCde.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Rows.AutoFit();
        }
        #endregion

        private void searchForChrtTextBox_Leave(object sender, EventArgs e)
        {
            this.beenClicked = false;
        }

        private void positionIMATextBox_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                this.IMAPnlNavButtons(this.movePreviousIMAButton, ex);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
            {
                this.IMAPnlNavButtons(this.moveNextIMAButton, ex);
            }
        }

        private void searchForIMATextBox_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.loadIMAPanel();
            }
        }

        private void searchForIMADtTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.loadIMADetPanel();
            }
        }

        private void positionIMADtTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                this.IMADTPnlNavButtons(this.movePreviousIMADtButton, ex);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
            {
                this.IMADTPnlNavButtons(this.moveNextIMADtButton, ex);
            }
        }

        private void searchForIMADtTextBox_Click(object sender, EventArgs e)
        {
            if (this.beenClicked == true)
            {
                return;
            }
            this.beenClicked = true;
            TextBox myTxt = (TextBox)sender;
            myTxt.SelectAll();
            //this.searchForChrtTextBox.SelectAll();

        }

        private void searchForIMADtTextBox_Enter(object sender, EventArgs e)
        {
            TextBox myTxt = (TextBox)sender;
            myTxt.SelectAll();
        }

        private void slctAllIMADtButton_Click(object sender, EventArgs e)
        {
            this.imaDtListView.Focus();
            KeyEventArgs ex = new KeyEventArgs(Keys.Control | Keys.A);
            this.imaDtListView_KeyDown(this.imaDtListView, ex);
            this.imaDtListView.Focus();
        }

        private void imaDtListView_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();

            if (e.Control && e.KeyCode == Keys.S)
            {
                if (this.saveIMAButton.Enabled == true)
                {
                    this.saveIMAButton.PerformClick();
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                if (this.addIMADtButton.Enabled == true)
                {
                    this.addIMADtButton.PerformClick();
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                if (this.editIMADtButton.Enabled == true)
                {
                    this.editIMADtButton.PerformClick();
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                this.resetIMAButton.PerformClick();
            }
            else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)
            {
                if (this.refreshIMAButton.Enabled == true)
                {
                    this.refreshIMAButton.PerformClick();
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.Delete)
            {
                if (this.delIMADtButton.Enabled == true)
                {
                    this.delIMADtButton.PerformClick();
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                Global.mnFrm.cmCde.listViewKeyDown(this.imaDtListView, e);
            }
        }

        private void vwSQLIMAButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(this.ima_SQL, 10);
        }

        private void rcHstryIMAButton_Click(object sender, EventArgs e)
        {
            if (this.accIDIMATextBox.Text == "-1"
         || this.accIDIMATextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(
              Global.get_IMA_Rec_Hstry(int.Parse(this.accIDIMATextBox.Text)), 9);

        }

        private void vwSQLIMADtButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(this.imadt_SQL, 10);
        }

        private void rcHstryIMADtButton_Click(object sender, EventArgs e)
        {
            if (this.imaDtListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(
              Global.get_IMADT_Rec_Hstry(int.Parse(this.imaDtListView.SelectedItems[0].SubItems[8].Text)), 9);
        }

        private void refreshIMAButton_Click(object sender, EventArgs e)
        {
            this.loadIMAPanel();
        }

        private void resetIMAButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.minimizeMemory();
            this.searchInIMAComboBox.SelectedIndex = 0;
            this.searchForIMATextBox.Text = "%";

            this.searchInIMADtComboBox.SelectedIndex = 0;
            this.searchForIMADtTextBox.Text = "%";

            this.lowValIMAUpDown.Value = 0;
            this.highValIMAUpDown.Value = 0;

            if (this.strtDteIMATextBox.Text == "")
            {
                this.strtDteIMATextBox.Text = DateTime.ParseExact(
                  Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
                  System.Globalization.CultureInfo.InvariantCulture).AddMonths(-24).ToString("dd-MMM-yyyy 23:59:59");
            }
            if (this.endDteIMATextBox.Text == "")
            {
                this.endDteIMATextBox.Text = DateTime.ParseExact(
                  Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
                  System.Globalization.CultureInfo.InvariantCulture).AddMonths(24).ToString("01-MMM-yyyy 00:00:00");
            }

            this.dsplySizeIMAComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            this.dsplySizeIMADtComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            this.ima_cur_indx = 0;
            this.imadt_cur_indx = 0;
            this.loadIMAPanel();
        }

        private void vldStrtDteTextBox_Enter(object sender, EventArgs e)
        {
            TextBox mytxt = (TextBox)sender;

            if (mytxt.Name == "vldStrtDteTextBox")
            {
                this.vldStrtDteTextBox.SelectAll();
            }
            else if (mytxt.Name == "vldEndDteTextBox")
            {
                this.vldEndDteTextBox.SelectAll();
            }
            else if (mytxt.Name == "strtDteIMATextBox")
            {
                this.strtDteIMATextBox.SelectAll();
            }
            else if (mytxt.Name == "endDteIMATextBox")
            {
                this.endDteIMATextBox.SelectAll();
            }
        }

        private void isCntraIMACheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shdObeyIMAEvts() == false
             || beenToIsContra == true)
            {
                beenToIsContra = false;
                return;
            }
            beenToIsContra = true;
            if (this.addima == false && this.editima == false)
            {
                this.isCntraIMACheckBox.Checked = !this.isCntraIMACheckBox.Checked;
            }
        }

        private void isEnbldIMACheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shdObeyIMAEvts() == false
             || beenToIsContra == true)
            {
                beenToIsContra = false;
                return;
            }
            beenToIsContra = true;
            if (this.addima == false && this.editima == false)
            {
                this.isEnbldIMACheckBox.Checked = !this.isEnbldIMACheckBox.Checked;
            }
        }

        private void hsFrmlrIMACheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shdObeyIMAEvts() == false
            || beenToIsContra == true)
            {
                beenToIsContra = false;
                return;
            }
            beenToIsContra = true;
            if (this.addima == false && this.editima == false)
            {
                this.hsFrmlrIMACheckBox.Checked = !this.hsFrmlrIMACheckBox.Checked;
            }
        }

        private void crncyIMATextBox_TextChanged(object sender, EventArgs e)
        {
            if (!this.shdObeyIMAEvts())
            {
                return;
            }
            if (this.addima == false && this.editima == false)
            {
                //Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                return;
            }
            this.txtChngd = true;
        }

        private void crncyIMATextBox_Leave(object sender, EventArgs e)
        {
            if (this.txtChngd == false)
            {
                return;
            }
            TextBox mytxt = (TextBox)sender;
            string srchWrd = mytxt.Text;
            if (!mytxt.Text.Contains("%"))
            {
                srchWrd = "%" + srchWrd.Replace(" ", "%") + "%";
            }

            if (mytxt.Name == "dfltCstActIMATextBox")
            {
                this.dfltCstAccntLOVSearch(srchWrd, true);
            }
            else if (mytxt.Name == "dfltBalsActIMATextBox")
            {
                this.dfltBalsAccntLOVSearch(srchWrd, true);
            }
            else if (mytxt.Name == "crncyIMATextBox")
            {
                this.imaCurLOVSearch(srchWrd, true);
            }
            this.txtChngd = false;
        }

        private void dfltCstAccntLOVSearch(string srchWrd, bool autoLoad)
        {
            this.txtChngd = false;
            if (!this.shdObeyIMAEvts())
            {
                return;
            }

            if (this.addima == false && this.editima == false)
            {
                //Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                return;
            }
            this.dfltCstActIMAIDTextBox.Text = "-1";

            string[] selVals = new string[1];
            selVals[0] = this.dfltCstActIMAIDTextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
             Global.mnFrm.cmCde.getLovID("Transaction Accounts"), ref selVals,
             true, false, Global.mnFrm.cmCde.Org_id,
             "", "",
             srchWrd, "Both", autoLoad);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.dfltCstActIMAIDTextBox.Text = selVals[i];
                    this.dfltCstActIMATextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));
                }
            }
        }

        private void dfltBalsAccntLOVSearch(string srchWrd, bool autoLoad)
        {
            this.txtChngd = false;
            if (!this.shdObeyIMAEvts())
            {
                return;
            }

            if (this.addima == false && this.editima == false)
            {
                //Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                return;
            }
            this.dfltBalsActIMAIDTextBox.Text = "-1";

            string[] selVals = new string[1];
            selVals[0] = this.dfltBalsActIMAIDTextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
             Global.mnFrm.cmCde.getLovID("Transaction Accounts"), ref selVals,
             true, false, Global.mnFrm.cmCde.Org_id,
             "", "",
             srchWrd, "Both", autoLoad);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.dfltBalsActIMAIDTextBox.Text = selVals[i];
                    this.dfltBalsActIMATextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));
                }
            }
        }

        private void imaCurLOVSearch(string srchWrd, bool autoLoad)
        {
            this.txtChngd = false;
            if (!this.shdObeyIMAEvts())
            {
                return;
            }
            if (this.addima == false && this.editima == false)
            {
                //Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
                return;
            }

            this.crncyIMAIDTextBox.Text = "-1";

            int[] selVals = new int[1];
            selVals[0] = int.Parse(this.crncyIMAIDTextBox.Text);
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
             Global.mnFrm.cmCde.getLovID("Currencies"), ref selVals,
             true, false, srchWrd, "Both", autoLoad);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.crncyIMAIDTextBox.Text = selVals[i].ToString();
                    this.crncyIMATextBox.Text = Global.mnFrm.cmCde.getPssblValNm(selVals[i]) +
                      " - " + Global.mnFrm.cmCde.getPssblValDesc(selVals[i]);
                }
            }
        }

        private void dfltCstActIMAButton_Click(object sender, EventArgs e)
        {
            string srchWrd = this.dfltCstActIMATextBox.Text;
            if (!srchWrd.Contains("%"))
            {
                srchWrd = "%" + srchWrd.Replace(" ", "%") + "%";
            }
            this.dfltCstAccntLOVSearch(srchWrd, false);
        }

        private void dfltBalsActIMAButton_Click(object sender, EventArgs e)
        {
            string srchWrd = this.dfltBalsActIMATextBox.Text;
            if (!srchWrd.Contains("%"))
            {
                srchWrd = "%" + srchWrd.Replace(" ", "%") + "%";
            }
            this.dfltBalsAccntLOVSearch(srchWrd, false);

        }

        private void crncyIMAButton_Click(object sender, EventArgs e)
        {
            string srchWrd = this.crncyIMATextBox.Text;
            if (!srchWrd.Contains("%"))
            {
                srchWrd = "%" + srchWrd.Replace(" ", "%") + "%";
            }
            this.imaCurLOVSearch(srchWrd, false);
        }

        private void strtDteIMAButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.selectDate(ref this.strtDteIMATextBox);
        }

        private void endDteIMAButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.selectDate(ref this.endDteIMATextBox);
        }

        private void openBatchMenuItem_Click(object sender, EventArgs e)
        {
            if (this.trnsSearchListView.SelectedItems.Count == 1)
            {
                string btchN = this.trnsSearchListView.SelectedItems[0].SubItems[14].Text;
                this.searchForTrnsTextBox.Text = btchN;
                this.searchInTrnsComboBox.SelectedItem = "Batch Name";
                this.loadCorrectPanel("Journal Entries");
                this.showUnpostedCheckBox.Checked = false;
                if (this.shwMyBatchesCheckBox.Enabled == true)
                {
                    this.shwMyBatchesCheckBox.Checked = false;
                }
                this.rfrshTrnsButton.PerformClick();
            }
        }

        private void openBtchMenuItem_Click(object sender, EventArgs e)
        {
            if (this.accntStmntListView.SelectedItems.Count == 1)
            {
                string btchN = this.accntStmntListView.SelectedItems[0].SubItems[10].Text;
                this.searchForTrnsTextBox.Text = btchN;
                this.searchInTrnsComboBox.SelectedItem = "Batch Name";
                this.loadCorrectPanel("Journal Entries");
                this.showUnpostedCheckBox.Checked = false;
                if (this.shwMyBatchesCheckBox.Enabled == true)
                {
                    this.shwMyBatchesCheckBox.Checked = false;
                }
                this.rfrshTrnsButton.PerformClick();
            }
        }

        private void enableAutoPostMenuItem_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[21]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.trnsBatchListView.SelectedItems.Count == 1
              && this.batchStatusLabel.Text == "Not Posted")
            {
                if (this.autoPostLabel.Text.Contains("Not Monitored"))
                {
                    Global.updateBatchAvlblty(long.Parse(this.batchIDTextBox.Text), "1");
                }
                else
                {
                    Global.updateBatchAvlblty(long.Parse(this.batchIDTextBox.Text), "0");
                }
            }
            this.getTrnsPnlData();
        }

        private void addIMADtButton_Click(object sender, EventArgs e)
        {

        }

        private void addIMAButton_Click(object sender, EventArgs e)
        {

        }

        private void runRptButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showRptParamsDiag(-1, Global.mnFrm.cmCde);
        }

        private void leftTreeView_Click(object sender, EventArgs e)
        {
            if (this.backgroundWorker1.IsBusy == true)
            {
                return;
            }
            if (this.leftTreeView.SelectedNode == null)
            {
                return;
            }
            this.loadCorrectPanel(this.leftTreeView.SelectedNode.Text);
        }

        //private void accClsfctnButton_Click(object sender, EventArgs e)
        //{
        //  if (this.addChrt == false && this.editChrt == false)
        //  {
        //    Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
        //    return;
        //  }
        //  int[] selVals = new int[1];
        //  selVals[0] = Global.mnFrm.cmCde.getPssblValID(this.accClsfctnTextBox.Text,
        //    Global.mnFrm.cmCde.getLovID("Account Classifications"));
        //  DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        //      Global.mnFrm.cmCde.getLovID("Account Classifications"), ref selVals,
        //      true, false,
        //   this.srchWrd, "Both", true);
        //  if (dgRes == DialogResult.OK)
        //  {
        //    for (int i = 0; i < selVals.Length; i++)
        //    {
        //      this.accClsfctnTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
        //    }
        //  }
        //}

        private void mnthlyStrtDteTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!this.obey_evnts)
            {
                return;
            }
            this.txtChngd = true;
        }

        private void mnthlyStrtDteTextBox_Leave(object sender, EventArgs e)
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

            if (mytxt.Name == "mnthlyStrtDteTextBox")
            {
                this.mnthlyStrtDteTextBox.Text = Global.mnFrm.cmCde.checkNFormatDate(this.mnthlyStrtDteTextBox.Text).Substring(0, 11) + " 00:00:00";
            }
            else if (mytxt.Name == "mnthlyEndDteTextBox")
            {
                this.mnthlyEndDteTextBox.Text = Global.mnFrm.cmCde.checkNFormatDate(this.mnthlyEndDteTextBox.Text).Replace("00:00:00", "23:59:59");
            }
            this.obey_evnts = true;
            this.txtChngd = false;
            this.srchWrd = "%";
        }

        private void mnthlyStrtDteTextBox_Click(object sender, EventArgs e)
        {
            //TextBox mytxt = (TextBox)sender;

            //if (mytxt.Name == "mnthlyStrtDteTextBox")
            //{
            //  this.mnthlyStrtDteTextBox.SelectAll();
            //}
            //else if (mytxt.Name == "mnthlyEndDteTextBox")
            //{
            //  this.mnthlyEndDteTextBox.SelectAll();
            //}
        }

        private void cashFlowTypComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cashFlowTypComboBox.Text.Contains("Detail"))
            {
                this.hideZerosCashFlwCheckBox.Checked = true;
            }
            else
            {
                this.hideZerosCashFlwCheckBox.Checked = false;
            }
        }

        private void cashFlowStrtTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!this.obey_evnts)
            {
                return;
            }
            this.txtChngd = true;
        }

        private void cashFlowStrtTextBox_Leave(object sender, EventArgs e)
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

            if (mytxt.Name == "cashFlowStrtTextBox")
            {
                this.cashFlowStrtTextBox.Text = Global.mnFrm.cmCde.checkNFormatDate(this.cashFlowStrtTextBox.Text).Substring(0, 11) + " 00:00:00";
            }
            else if (mytxt.Name == "cashFlowEndTextBox")
            {
                this.cashFlowEndTextBox.Text = Global.mnFrm.cmCde.checkNFormatDate(this.cashFlowEndTextBox.Text).Replace("00:00:00", "23:59:59");
            }
            this.obey_evnts = true;
            this.txtChngd = false;
            this.srchWrd = "%";
        }

        private void cashFlowStrtTextBox_Click(object sender, EventArgs e)
        {
            //TextBox mytxt = (TextBox)sender;

            //if (mytxt.Name == "cashFlowStrtTextBox")
            //{
            //  this.cashFlowStrtTextBox.SelectAll();
            //}
            //else if (mytxt.Name == "cashFlowEndTextBox")
            //{
            //  this.cashFlowEndTextBox.SelectAll();
            //}
        }

        private void tbalsAcctButton_Click(object sender, EventArgs e)
        {
            string[] selVals = new string[1];
            selVals[0] = this.tbalsAcctIDTextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
              Global.mnFrm.cmCde.getLovID("All Accounts"), ref selVals,
              true, false, Global.mnFrm.cmCde.Org_id,
             this.srchWrd, "Both", this.autoLoad);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.tbalsAcctIDTextBox.Text = selVals[i];
                    this.tbalsAcctNmTextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(selVals[i])) +
                      "." + Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));

                }
            }
        }

        private void tbalsAcctNmTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!this.obey_evnts)
            {
                return;
            }
            this.txtChngd = true;
        }

        private void tbalsAcctNmTextBox_Leave(object sender, EventArgs e)
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

            if (mytxt.Name == "tbalsAcctNmTextBox")
            {
                this.tbalsAcctNmTextBox.Text = "";
                this.tbalsAcctIDTextBox.Text = "-1";
                this.autoLoad = true;
                this.tbalsAcctButton_Click(this.tbalsAcctButton, e);
                this.autoLoad = false;
            }
            else if (mytxt.Name == "pnlAccntNmTextBox")
            {
                this.pnlAccntNmTextBox.Text = "";
                this.pnlAccntIDTextBox.Text = "-1";
                this.autoLoad = true;
                this.pnlAccntNmButton_Click(this.pnlAccntNmButton, e);
                this.autoLoad = false;
            }
            this.obey_evnts = true;
            this.txtChngd = false;
            this.srchWrd = "%";
        }

        private void tbalsAcctNmTextBox_Click(object sender, EventArgs e)
        {
            TextBox mytxt = (TextBox)sender;

            if (mytxt.Name == "tbalsAcctNmTextBox")
            {
                this.tbalsAcctNmTextBox.SelectAll();
            }
            else if (mytxt.Name == "pnlAccntNmTextBox")
            {
                this.pnlAccntNmTextBox.SelectAll();
            }
        }

        private void tbalsAcctNmTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Enter)
            {
                this.genRptTrialBalButton.Focus();
            }
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            this.resetButton.Enabled = false;
            System.Windows.Forms.Application.DoEvents();
            this.txtChngd = false;
            this.obey_evnts = false;
            this.tbalsAcctIDTextBox.Text = "-1";
            this.tbalsAcctNmTextBox.Text = "";
            this.obey_evnts = true;
            this.txtChngd = false;
            this.smmryTBalsCheckBox.Checked = true;
            this.genRptTrialBalButton.PerformClick();
            System.Windows.Forms.Application.DoEvents();
            this.resetButton.Enabled = true;
        }

        private void pnlAccntNmButton_Click(object sender, EventArgs e)
        {
            string[] selVals = new string[1];
            selVals[0] = this.pnlAccntIDTextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
              Global.mnFrm.cmCde.getLovID("All Accounts"), ref selVals,
              true, false, Global.mnFrm.cmCde.Org_id,
             this.srchWrd, "Both", this.autoLoad);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.pnlAccntIDTextBox.Text = selVals[i];
                    this.pnlAccntNmTextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(selVals[i])) +
                      "." + Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));

                }
            }
        }

        private void resetPnLButton_Click(object sender, EventArgs e)
        {
            this.resetPnLButton.Enabled = false;
            System.Windows.Forms.Application.DoEvents();
            this.txtChngd = false;
            this.obey_evnts = false;
            this.pnlAccntIDTextBox.Text = "-1";
            this.pnlAccntNmTextBox.Text = "";
            this.obey_evnts = true;
            this.txtChngd = false;
            this.pnlSmmryCheckBox.Checked = true;
            this.plGenRptButton.PerformClick();
            System.Windows.Forms.Application.DoEvents();
            this.resetPnLButton.Enabled = true;
        }

        private void trialBalListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {

            if (e.IsSelected)
            {
                if (e.Item.Checked)
                {
                    e.Item.Checked = false;
                    e.Item.UseItemStyleForSubItems = true;
                    e.Item.ForeColor = Color.Black;
                    e.Item.BackColor = Color.White;
                }
                else
                {
                    e.Item.Checked = true;
                    e.Item.UseItemStyleForSubItems = true;
                    e.Item.BackColor = Color.CornflowerBlue;
                    e.Item.ForeColor = Color.White;
                }
            }
        }

        private void resetRcnclButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to CLEAR All RECORDS on this Page?" +
             "\r\nThis action cannot be undone!", 1) == DialogResult.No)
            {
                //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            this.batchid = -1;
            this.batchNmRcnclTextBox.Text = "";
            this.trnsDataGridView.Rows.Clear();
            this.ttlCreditsRcnclLabel.Text = "0.00";
            this.ttlDebitsRcnclLabel.Text = "0.00";
            this.netBalanceRcnclLabel.Text = "0.00";
        }

        private void segmentsButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showMsg("Sorry! Feature not available in this edition!\nContact your the Software Provider!", 0);
            return;
        }

        private void budgetDetListView_DoubleClick(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[2]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.budgetDetListView.SelectedItems.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select an Account First!", 0);
                return;
            }
            else
            {
                int accIDIn = int.Parse(this.budgetDetListView.SelectedItems[0].SubItems[10].Text);
                string isPrnt = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_chart_of_accnts", "accnt_id", "(CASE WHEN is_prnt_accnt='1' THEN is_prnt_accnt ELSE has_sub_ledgers END)", accIDIn);
                if (isPrnt == "1")
                {
                    /*this.pnlAccntIDTextBox.Text = accIDIn.ToString();
                    this.pnlAccntNmTextBox.Text = Global.mnFrm.cmCde.getAccntNum(accIDIn) +
                      "." + Global.mnFrm.cmCde.getAccntName(accIDIn);
                    this.pnlSmmryCheckBox.Checked = false;
                    //this.finStmntsTabControl.SelectedTab = this.tbalTabPage;
                    System.Windows.Forms.Application.DoEvents();
                    this.plGenRptButton_Click(this.plGenRptButton, e);*/
                }
                else
                {
                    vwTrnsctnsDiag nwDiag = new vwTrnsctnsDiag();
                    nwDiag.my_org_id = Global.mnFrm.cmCde.Org_id;
                    nwDiag.accnt_name = this.budgetDetListView.SelectedItems[0].SubItems[2].Text.Trim();
                    nwDiag.accntid = int.Parse(this.budgetDetListView.SelectedItems[0].SubItems[10].Text);
                    nwDiag.dte1 = this.budgetDetListView.SelectedItems[0].SubItems[6].Text;
                    nwDiag.dte2 = this.budgetDetListView.SelectedItems[0].SubItems[7].Text;

                    DialogResult dgres = nwDiag.ShowDialog();
                    if (dgres == DialogResult.OK)
                    {

                    }
                }
            }
        }

    }
}