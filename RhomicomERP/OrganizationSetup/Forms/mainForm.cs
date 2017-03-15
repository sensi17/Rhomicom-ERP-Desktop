using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OrganizationSetup.Classes;
using System.Diagnostics;
using OrganizationSetup.Dialogs;
using Npgsql;

namespace OrganizationSetup.Forms
{
    public partial class mainForm : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        #region "GLOBAL VARIABLES..."
        public CommonCode.CommonCodes cmCde = new CommonCode.CommonCodes();
        cadmaFunctions.NavFuncs myNav = new cadmaFunctions.NavFuncs();
        cadmaFunctions.NavFuncs myNav1 = new cadmaFunctions.NavFuncs();
        //public NpgsqlConnection gnrlSQLConn = new NpgsqlConnection();
        public Int64 usr_id = -1;
        public int[] role_st_id = new int[0];
        public Int64 lgn_num = -1;
        public int Og_id = -1;
        public bool txtChngd = false;
        public string srchWrd = "%";
        //  string[] menuItems = {"Organization's Details", "Divisions/Groups", 
        //  "Sites/Locations","Jobs", "Grades", "Positions", "Pay Items", 
        //"Working Hours", "Gathering Types"};
        //  string[] menuImages = {"groupings.png", "staffs.png", "shield_64.png"
        //    ,"SecurityLock.png", "73.ico", "54.png", "staffs.png", "shield_64.png"
        //    ,"SecurityLock.png"};
        string[] menuItems = {"Organization's Details", "Divisions/Groups",
        "Sites/Locations","Jobs", "Grades", "Positions"};
        string[] menuImages = {"1098_png_icons_refresh.png", "images (1).jpg",
"1283107630I68HM7.jpg","Hallmark_job_openings2.jpg", "images (4).jpg",
"supervisor.jpg"};
        /*, 
    "Working Hours", "Gathering Types", "working_overtime.jpg"	,"Gathering-of-Women-Art.jpg"*/
        bool beenToCheckBx = false;

        //Org Panel Variables;
        Int64 orgDet_cur_indx = 0;
        bool is_last_orgDet = false;
        Int64 totl_orgDet = 0;
        long last_org_num = 0;
        public string orgDet_SQL = "";
        public string orgDetHrchy_SQL = "";
        bool obey_orgDet_evnts = false;
        bool addOrg = false;
        bool editOrg = false;
        bool addOrgs = false;
        bool editOrgs = false;
        //Div Panel Variables;
        Int64 divDet_cur_indx = 0;
        bool is_last_divDet = false;
        Int64 totl_divDet = 0;
        long last_div_num = 0;
        public string divDet_SQL = "";
        public string divDetHrchy_SQL = "";
        bool obey_divDet_evnts = false;
        bool addDiv = false;
        bool editDiv = false;
        bool addDivs = false;
        bool editDivs = false;
        bool delDivs = false;
        //Div Panel Variables;
        //Sites Panel Variables;
        Int64 site_cur_indx = 0;
        bool is_last_site = false;
        Int64 totl_site = 0;
        long last_site_num = 0;
        public string site_SQL = "";
        public string siteDet_SQL = "";
        bool obey_site_evnts = false;
        bool addSite = false;
        bool editSite = false;
        bool addSites = false;
        bool editSites = false;
        bool delSites = false;
        //Jobs Panel Variables;
        Int64 jobs_cur_indx = 0;
        bool is_last_job = false;
        Int64 totl_jobs = 0;
        long last_job_num = 0;
        public string jobs_SQL = "";
        public string jobHrchy_SQL = "";
        bool obey_jobs_evnts = false;
        bool addJob = false;
        bool editJob = false;
        bool addJobs = false;
        bool editJobs = false;
        bool delJobs = false;
        //Grades Panel Variables;
        Int64 grd_cur_indx = 0;
        bool is_last_grd = false;
        Int64 totl_grd = 0;
        long last_grd_num = 0;
        public string grd_SQL = "";
        public string grdHrchy_SQL = "";
        bool obey_grd_evnts = false;
        bool addgrd = false;
        bool editgrd = false;
        bool addgrds = false;
        bool editgrds = false;
        bool delgrds = false;
        //Position Panel Variables;
        long pos_cur_indx = 0;
        bool is_last_pos = false;
        long totl_pos = 0;
        long last_pos_num = 0;
        public string pos_SQL = "";
        public string posHrchy_SQL = "";
        bool obey_pos_evnts = false;
        bool addpos = false;
        bool editpos = false;
        bool addposs = false;
        bool editposs = false;
        bool delposs = false;

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
        //Work Hours Panel Variables;
        long wkh_cur_indx = 0;
        bool is_last_wkh = false;
        long totl_wkh = 0;
        long last_wkh_num = 0;
        public string wkh_SQL = "";
        public string wkhDet_SQL = "";
        bool obey_wkh_evnts = false;
        bool addwkh = false;
        bool editwkh = false;
        bool addwkhs = false;
        bool editwkhs = false;
        bool delwkhs = false;
        //Gathering Types Panel Variables;
        long gth_cur_indx = 0;
        bool is_last_gth = false;
        long totl_gth = 0;
        long last_gth_num = 0;
        public string gth_SQL = "";
        public string gthHrchy_SQL = "";
        bool obey_gth_evnts = false;
        bool addgth = false;
        bool editgth = false;
        bool addgths = false;
        bool editgths = false;
        bool delgths = false;
        //Segment Values
        public string segmentValsSQL = "";
        #endregion

        #region "FORM EVENTS..."
        public mainForm()
        {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            this.accDndLabel.Visible = false;
            Global.myOrgStp.Initialize();
            Global.mnFrm = this;
            //Global.mnFrm.cmCde.pgSqlConn = this.gnrlSQLConn;
            Global.mnFrm.cmCde.Login_number = this.lgn_num;
            Global.mnFrm.cmCde.Role_Set_IDs = this.role_st_id;
            Global.mnFrm.cmCde.User_id = this.usr_id;
            Global.mnFrm.cmCde.Org_id = this.Og_id;

            this.hideAllPanels();
            Global.refreshRqrdVrbls();
            System.Windows.Forms.Application.DoEvents();
            Color[] clrs = Global.mnFrm.cmCde.getColors();
            this.BackColor = clrs[0];
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
            //this.glsLabel12.TopFill = clrs[0];
            //this.glsLabel12.BackColor = clrs[0];
            //this.glsLabel12.BottomFill = clrs[1];
            this.glsLabel13.TopFill = clrs[0];
            this.glsLabel13.BackColor = clrs[0];
            this.glsLabel13.BottomFill = clrs[1];
            this.tabPage1.BackColor = clrs[0];
            this.tabPage2.BackColor = clrs[0];
            this.tabPage3.BackColor = clrs[0];
            this.tabPage4.BackColor = clrs[0];
            this.tabPage5.BackColor = clrs[0];
            this.tabPage6.BackColor = clrs[0];

            System.Windows.Forms.Application.DoEvents();

            Global.myOrgStp.loadMyRolesNMsgtyps();
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
        }

        private void mainForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            Global.myOrgStp.Dispose();
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
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[0] +
                    "~" + Global.dfltPrvldgs[i + 1]) == false)
                {
                    continue;
                }
                TreeNode nwNode = new TreeNode();
                nwNode.Name = "myNode" + i.ToString();
                nwNode.Text = menuItems[i];
                nwNode.ImageKey = menuImages[i];
                this.leftTreeView.Nodes.Add(nwNode);
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

        private void loadCorrectPanel(string inpt_name)
        {
            Global.currentPanel = inpt_name;
            //this.disableFormButtons(inpt_name);

            if (inpt_name == menuItems[0])
            {
                this.showATab(ref this.tabPage1);
                this.changeOrg();
                this.loadOrgDetPanel();
            }
            else
            {
                if (Global.mnFrm.cmCde.Org_id <= 0)
                {
                    Global.mnFrm.cmCde.showMsg("Please Use Select Roles to Choose an Organisation First!", 0);
                    return;
                }
                if (inpt_name == menuItems[1])
                {
                    this.showATab(ref this.tabPage2);
                    this.changeOrg();
                    this.loadDivDetPanel();
                }
                else if (inpt_name == menuItems[2])
                {
                    this.showATab(ref this.tabPage3);
                    this.changeOrg();
                    this.loadSitePanel();
                }
                else if (inpt_name == menuItems[3])
                {
                    this.showATab(ref this.tabPage4);
                    this.changeOrg();
                    this.loadJobsPanel();
                }
                else if (inpt_name == menuItems[4])
                {
                    this.showATab(ref this.tabPage5);
                    this.changeOrg();
                    this.loadGradesPanel();
                }
                else if (inpt_name == menuItems[5])
                {
                    this.showATab(ref this.tabPage6);
                    this.changeOrg();
                    this.loadPositionPanel();
                }

            }
        }

        private void changeOrg()
        {
            //      if (this.crntOrgIDTextBox.Text == "-1"
            //|| this.crntOrgIDTextBox.Text == "")
            //      {
            //        this.crntOrgIDTextBox.Text = Global.mnFrm.cmCde.Org_id.ToString();
            //        this.crntOrgTextBox.Text = Global.mnFrm.cmCde.getOrgName(Global.mnFrm.cmCde.Org_id);
            //        Global.mnFrm.cmCde.getDBImageFile(this.crntOrgIDTextBox.Text + ".png",
            //            0, ref this.curOrgPictureBox);

            //        if (this.crntOrgIDTextBox.Text == "-1"
            //|| this.crntOrgIDTextBox.Text == "")
            //        {
            //          EventArgs e = new EventArgs();
            //          this.crntOrgButton_Click(this.crntOrgButton, e);
            //        }
            //      }
        }

        private void hideAllPanels()
        {
            this.orgDetailsPanel.Visible = false;
            this.orgDetailsPanel.Enabled = false;
            System.Windows.Forms.Application.DoEvents();
            this.orgDetailsPanel.Dock = DockStyle.None;
            //this.orgAttrbtsPanel.Visible = false;
            //this.orgAttrbtsPanel.Enabled = false;
            //System.Windows.Forms.Application.DoEvents();
            //this.orgAttrbtsPanel.Dock = DockStyle.None;
            this.divGrpsPanel.Visible = false;
            this.divGrpsPanel.Enabled = false;
            System.Windows.Forms.Application.DoEvents();
            this.divGrpsPanel.Dock = DockStyle.None;
            this.sitesPanel.Visible = false;
            this.sitesPanel.Enabled = false;
            System.Windows.Forms.Application.DoEvents();
            this.sitesPanel.Dock = DockStyle.None;
            this.jobsPanel.Visible = false;
            this.jobsPanel.Enabled = false;
            System.Windows.Forms.Application.DoEvents();
            this.jobsPanel.Dock = DockStyle.None;
            this.gradesPanel.Visible = false;
            this.gradesPanel.Enabled = false;
            System.Windows.Forms.Application.DoEvents();
            this.gradesPanel.Dock = DockStyle.None;
            this.positionsPanel.Visible = false;
            this.positionsPanel.Enabled = false;
            System.Windows.Forms.Application.DoEvents();
            this.positionsPanel.Dock = DockStyle.None;
            System.Windows.Forms.Application.DoEvents();
        }

        private void showAllPanels()
        {
            this.orgDetailsPanel.Visible = true;
            this.orgDetailsPanel.Enabled = true;
            System.Windows.Forms.Application.DoEvents();
            this.orgDetailsPanel.Dock = DockStyle.Fill;
            //this.orgAttrbtsPanel.Visible = true;
            //this.orgAttrbtsPanel.Enabled = true;
            //System.Windows.Forms.Application.DoEvents();
            //this.orgAttrbtsPanel.Dock = DockStyle.Fill;
            this.divGrpsPanel.Visible = true;
            this.divGrpsPanel.Enabled = true;
            System.Windows.Forms.Application.DoEvents();
            this.divGrpsPanel.Dock = DockStyle.Fill;
            this.sitesPanel.Visible = true;
            this.sitesPanel.Enabled = true;
            System.Windows.Forms.Application.DoEvents();
            this.sitesPanel.Dock = DockStyle.Fill;
            this.jobsPanel.Visible = true;
            this.jobsPanel.Enabled = true;
            System.Windows.Forms.Application.DoEvents();
            this.jobsPanel.Dock = DockStyle.Fill;
            this.gradesPanel.Visible = true;
            this.gradesPanel.Enabled = true;
            System.Windows.Forms.Application.DoEvents();
            this.gradesPanel.Dock = DockStyle.Fill;
            this.positionsPanel.Visible = true;
            this.positionsPanel.Enabled = true;
            System.Windows.Forms.Application.DoEvents();
            this.positionsPanel.Dock = DockStyle.Fill;
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

        private void disableFormButtons()
        {
            bool vwSQL = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[12]);
            bool rcHstry = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[13]);
            //Organization's Details
            this.saveOrgDetButton.Enabled = false;
            this.addOrgs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[14]);
            this.addOrgDetButton.Enabled = this.addOrgs;
            this.imprtOrgTmpltButton.Enabled = this.addOrgs;

            this.editOrgs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[15]);
            this.editOrgDetButton.Enabled = this.editOrgs;
            this.changeLogoButton.Enabled = this.editOrgs;

            //this.delOrgs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[16]);
            //this.delOrgDetButton.Enabled = this.delOrgs;

            this.vwSQLOrgDetButton.Enabled = vwSQL;
            this.recHstryOrgDetButton.Enabled = rcHstry;
            //Divisions/Groups
            this.saveDivButton.Enabled = false;
            this.addDivs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[16]);
            this.addDivButton.Enabled = this.addDivs;
            this.addDivMenuItem.Enabled = this.addDivs;
            this.imprtDivButton.Enabled = this.addDivs;

            this.editDivs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]);
            this.editDivButton.Enabled = this.editDivs;
            this.editDivMenuItem.Enabled = this.editDivs;
            this.changeDivLogoButton.Enabled = this.editDivs;

            this.delDivs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[18]);
            this.delDivButton.Enabled = this.delDivs;
            this.delDivMenuItem.Enabled = this.delDivs;

            this.vwSQLDivButton.Enabled = vwSQL;
            this.rcHstryDivMenuItem.Enabled = rcHstry;
            this.vwSQLDivMenuItem.Enabled = vwSQL;
            this.recHstryDivButton.Enabled = rcHstry;
            //Sites/Locations
            this.saveSiteButton.Enabled = false;
            this.addSites = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[19]);
            this.addSiteButton.Enabled = this.addSites;
            this.addSiteMenuItem.Enabled = this.addSites;
            this.imprtSiteButton.Enabled = this.addSites;

            this.editSites = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[20]);
            this.editSiteButton.Enabled = this.editSites;
            this.editSiteMenuItem.Enabled = this.editSites;

            this.delSites = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[21]);
            this.delSiteButton.Enabled = this.delSites;
            this.delSiteMenuItem.Enabled = this.delSites;

            this.vwSQLSiteButton.Enabled = vwSQL;
            this.rcHstrySiteMenuItem.Enabled = rcHstry;
            this.vwSQLSiteMenuItem.Enabled = vwSQL;
            this.recHstrySiteButton.Enabled = rcHstry;
            //Jobs
            this.saveJobsButton.Enabled = false;
            this.addJobs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[22]);
            this.addJobsButton.Enabled = this.addJobs;
            this.addJobMenuItem.Enabled = this.addJobs;
            this.imprtJobsButton.Enabled = this.addJobs;

            this.editJobs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[23]);
            this.editJobsButton.Enabled = this.editJobs;
            this.editJobMenuItem.Enabled = this.editJobs;

            this.delJobs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[24]);
            this.delJobButton.Enabled = this.delJobs;
            this.delJobMenuItem.Enabled = this.delJobs;

            this.vwSQLJobsButton.Enabled = vwSQL;
            this.rcHstryJobMenuItem.Enabled = rcHstry;
            this.vwSQLJobMenuItem.Enabled = vwSQL;
            this.recHstryJobButton.Enabled = rcHstry;
            //Grades
            this.saveGrdButton.Enabled = false;
            this.addgrds = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[25]);
            this.addGrdButton.Enabled = this.addgrds;
            this.addGradesMenuItem.Enabled = this.addgrds;
            this.imprtGradesButton.Enabled = this.addgrds;

            this.editgrds = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[26]);
            this.editGrdButton.Enabled = this.editgrds;
            this.editGradesMenuItem.Enabled = this.editgrds;

            this.delgrds = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[27]);
            this.delGrdButton.Enabled = this.delgrds;
            this.delGradesMenuItem.Enabled = this.delgrds;

            this.vwSQLGrdButton.Enabled = vwSQL;
            this.rcHstryGradesMenuItem.Enabled = rcHstry;
            this.vwSQLGradesMenuItem.Enabled = vwSQL;
            this.rcHstryGrdButton.Enabled = rcHstry;

            //Positions
            this.savePosButton.Enabled = false;
            this.addposs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[28]);
            this.addPosButton.Enabled = this.addposs;
            this.addPosMenuItem.Enabled = this.addposs;
            this.imprtPosButton.Enabled = this.addposs;

            this.editposs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[29]);
            this.editPosButton.Enabled = this.editposs;
            this.editPosMenuItem.Enabled = this.editposs;

            this.delposs = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[30]);
            this.deletePosButton.Enabled = this.delposs;
            this.delPosMenuItem.Enabled = this.delposs;

            this.vwSQLPosButton.Enabled = vwSQL;
            this.rcHstryPosMenuItem.Enabled = rcHstry;
            this.vwSQLPosMenuItem.Enabled = vwSQL;
            this.recHstryPosButton.Enabled = rcHstry;

        }
        #endregion

        #region "Organization Details..."
        private void loadOrgDetPanel()
        {
            this.obey_orgDet_evnts = false;
            if (this.searchInOrgDetComboBox.SelectedIndex < 0)
            {
                this.searchInOrgDetComboBox.SelectedIndex = 0;
            }
            if (this.searchForOrgDetTextBox.Text.Contains("%") == false)
            {
                this.searchForOrgDetTextBox.Text = "%" + this.searchForOrgDetTextBox.Text.Replace(" ", "%") + "%";
            }
            if (this.searchForOrgDetTextBox.Text == "%%")
            {
                this.searchForOrgDetTextBox.Text = "%";
            }
            this.orgLogoPictureBox.Image = OrganizationSetup.Properties.Resources.blank;
            int dsply = 0;
            if (this.dsplySizeOrgDetComboBox.Text == ""
                || int.TryParse(this.dsplySizeOrgDetComboBox.Text, out dsply) == false)
            {
                this.dsplySizeOrgDetComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            //this.groupBox4.Height = this.orgDetailsPanel.Bottom - this.toolStrip3.Bottom - 65;
            this.is_last_orgDet = false;
            this.totl_orgDet = Global.mnFrm.cmCde.Big_Val;
            this.getOrgDetPnlData();
            this.populateOrgDetTreeView();
            this.obey_orgDet_evnts = true;
        }

        private void getOrgDetPnlData()
        {
            this.updtOrgDetTotals();
            this.populateOrgDet();
            this.updtOrgDetNavLabels();
        }

        private void updtOrgDetTotals()
        {
            Global.mnFrm.cmCde.navFuncts.FindNavigationIndices(1, this.totl_orgDet);
            if (this.orgDet_cur_indx >= Global.mnFrm.cmCde.navFuncts.totalGroups)
            {
                this.orgDet_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            if (this.orgDet_cur_indx < 0)
            {
                this.orgDet_cur_indx = 0;
            }
            Global.mnFrm.cmCde.navFuncts.currentNavigationIndex = this.orgDet_cur_indx;
        }

        private void updtOrgDetNavLabels()
        {
            this.moveFirstOrgDetButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveFirstBtnStatus();
            this.movePreviousOrgDetButton.Enabled = Global.mnFrm.cmCde.navFuncts.movePrevBtnStatus();
            this.moveNextOrgDetButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveNextBtnStatus();
            this.moveLastOrgDetButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveLastBtnStatus();
            this.positionOrgDetTextBox.Text = Global.mnFrm.cmCde.navFuncts.displayedRecordsNumbers();
            if (this.is_last_orgDet == true ||
                this.totl_orgDet != Global.mnFrm.cmCde.Big_Val)
            {
                this.totalRecOrgDetLabel.Text = Global.mnFrm.cmCde.navFuncts.totalRecordsLabel();
            }
            else
            {
                this.totalRecOrgDetLabel.Text = "of Total";
            }
        }

        private void populateOrgDetTreeView()
        {
            this.obey_orgDet_evnts = false;
            DataSet dtst = Global.get_Hrchy_OrgDet(this.searchForOrgDetTextBox.Text,
                this.searchInOrgDetComboBox.Text, 0, int.Parse(this.dsplySizeOrgDetComboBox.Text));
            this.orgDetTreeView.Nodes.Clear();
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[1]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                this.obey_orgDet_evnts = true;
                return;
            }
            TreeNode[] nwNode = new TreeNode[dtst.Tables[0].Rows.Count];

            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                TreeNode aNode = new TreeNode();
                aNode.Name = "orgNode" + int.Parse(dtst.Tables[0].Rows[i][0].ToString()).ToString();
                aNode.Text = dtst.Tables[0].Rows[i][2].ToString();
                //aNode.ImageKey = menuImages[i];
                nwNode[i] = aNode;
                if (int.Parse(dtst.Tables[0].Rows[i][3].ToString()) == 1)
                {
                    this.orgDetTreeView.Nodes.Add(nwNode[i]);
                }
                else
                {
                    try
                    {
                        string prntNodeNm = "orgNode" + int.Parse(dtst.Tables[0].Rows[i][1].ToString()).ToString();
                        this.getNode(nwNode, prntNodeNm).Nodes.Add(nwNode[i]);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                this.orgDetTreeView.ExpandAll();
            }
            this.obey_orgDet_evnts = true;
        }

        private TreeNode getNode(TreeNode[] ndeList, string ndeName)
        {
            for (int i = 0; i < ndeList.Length; i++)
            {
                if (ndeList[i].Name == ndeName)
                {
                    return ndeList[i];
                }
            }
            return null;
        }

        private void populateOrgDet(int orgID)
        {
            this.clearOrgDetInfo();
            this.disableOrgEdit();
            this.disableLnsEdit();
            this.obey_orgDet_evnts = false;
            DataSet dtst = Global.get_One_OrgDet(orgID);
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.orgIDTextBox.Text = dtst.Tables[0].Rows[i][0].ToString();
                this.orgNameTextBox.Text = dtst.Tables[0].Rows[i][1].ToString();
                this.orgPrntIDTextBox.Text = dtst.Tables[0].Rows[i][2].ToString();
                this.orgParentTextBox.Text = dtst.Tables[0].Rows[i][3].ToString();
                this.resAddrsTextBox.Text = dtst.Tables[0].Rows[i][4].ToString();
                this.websiteTextBox.Text = dtst.Tables[0].Rows[i][7].ToString();
                this.crncyIDTextBox.Text = dtst.Tables[0].Rows[i][13].ToString();
                this.crncyCodeTextBox.Text = dtst.Tables[0].Rows[i][14].ToString();
                this.postalAddrsTextBox.Text = dtst.Tables[0].Rows[i][5].ToString();
                this.emailAddrsTextBox.Text = dtst.Tables[0].Rows[i][6].ToString();
                this.contactNosTextBox.Text = dtst.Tables[0].Rows[i][8].ToString();
                this.sloganTextBox.Text = dtst.Tables[0].Rows[i][16].ToString();
                this.orgTypIDTextBox.Text = dtst.Tables[0].Rows[i][9].ToString();
                this.orgTypTextBox.Text = dtst.Tables[0].Rows[i][10].ToString();
                this.orgEnabledCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][12].ToString());
                this.orgDescTextBox.Text = dtst.Tables[0].Rows[i][15].ToString();
                Global.mnFrm.cmCde.getDBImageFile(dtst.Tables[0].Rows[i][11].ToString(), 0, ref this.orgLogoPictureBox);
            }
            this.obey_orgDet_evnts = true;
        }

        private void populateOrgDet()
        {
            this.clearOrgDetInfo();
            this.disableOrgEdit();
            this.disableLnsEdit();
            this.obey_orgDet_evnts = false;
            DataSet dtst = Global.get_Basic_OrgDet(this.searchForOrgDetTextBox.Text,
                this.searchInOrgDetComboBox.Text, this.orgDet_cur_indx, 1);
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.last_org_num = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
                this.orgIDTextBox.Text = dtst.Tables[0].Rows[i][0].ToString();
                this.orgNameTextBox.Text = dtst.Tables[0].Rows[i][1].ToString();
                this.orgPrntIDTextBox.Text = dtst.Tables[0].Rows[i][2].ToString();
                this.orgParentTextBox.Text = dtst.Tables[0].Rows[i][3].ToString();
                this.resAddrsTextBox.Text = dtst.Tables[0].Rows[i][4].ToString();
                this.websiteTextBox.Text = dtst.Tables[0].Rows[i][7].ToString();
                this.crncyIDTextBox.Text = dtst.Tables[0].Rows[i][13].ToString();
                this.crncyCodeTextBox.Text = dtst.Tables[0].Rows[i][14].ToString();
                this.postalAddrsTextBox.Text = dtst.Tables[0].Rows[i][5].ToString();
                this.emailAddrsTextBox.Text = dtst.Tables[0].Rows[i][6].ToString();
                this.contactNosTextBox.Text = dtst.Tables[0].Rows[i][8].ToString();
                this.orgTypIDTextBox.Text = dtst.Tables[0].Rows[i][9].ToString();
                this.orgTypTextBox.Text = dtst.Tables[0].Rows[i][10].ToString();
                this.sloganTextBox.Text = dtst.Tables[0].Rows[i][16].ToString();
                this.orgDescTextBox.Text = dtst.Tables[0].Rows[i][15].ToString();
                this.noOfSgmntsNumUpDown.Value = decimal.Parse(dtst.Tables[0].Rows[i][17].ToString());
                this.delimiterComboBox.SelectedItem = dtst.Tables[0].Rows[i][18].ToString();
                this.orgEnabledCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][12].ToString());
                Global.mnFrm.cmCde.getDBImageFile(dtst.Tables[0].Rows[i][11].ToString(), 0, ref this.orgLogoPictureBox);
            }
            this.correctOrgNavLbls(dtst);
            this.obey_orgDet_evnts = true;
            this.populateSegments();
        }

        private void correctOrgNavLbls(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.orgDet_cur_indx == 0 && totlRecs == 0)
            {
                this.is_last_orgDet = true;
                this.totl_orgDet = 0;
                this.last_org_num = 0;
                this.orgDet_cur_indx = 0;
                this.updtOrgDetTotals();
                this.updtOrgDetNavLabels();
            }
            else if (this.totl_orgDet == Global.mnFrm.cmCde.Big_Val
      && totlRecs < 1)
            {
                this.totl_orgDet = this.last_org_num;
                if (totlRecs == 0)
                {
                    this.orgDet_cur_indx -= 1;
                    this.updtOrgDetTotals();
                    this.populateOrgDet();
                }
                else
                {
                    this.updtOrgDetTotals();
                }
            }
        }

        private void clearOrgDetInfo()
        {
            this.obey_orgDet_evnts = false;
            this.saveOrgDetButton.Enabled = false;
            this.addOrgDetButton.Enabled = this.addOrgs;
            this.editOrgDetButton.Enabled = this.editOrgs;
            this.orgIDTextBox.Text = "-1";
            this.orgNameTextBox.Text = "";
            this.orgPrntIDTextBox.Text = "-1";
            this.orgParentTextBox.Text = "";
            this.resAddrsTextBox.Text = "";
            this.websiteTextBox.Text = "";
            this.crncyIDTextBox.Text = "-1";
            this.crncyCodeTextBox.Text = "";
            this.postalAddrsTextBox.Text = "";
            this.emailAddrsTextBox.Text = "";
            this.contactNosTextBox.Text = "";
            this.orgTypIDTextBox.Text = "-1";
            this.orgTypTextBox.Text = "";
            this.orgDescTextBox.Text = "";
            this.sloganTextBox.Text = "";
            this.noOfSgmntsNumUpDown.Value = 1;
            this.delimiterComboBox.SelectedIndex = 1;
            this.orgEnabledCheckBox.Checked = false;
            this.orgLogoPictureBox.Image = OrganizationSetup.Properties.Resources.blank;
            this.obey_orgDet_evnts = true;
        }

        private void prpareForOrgEdit()
        {
            this.saveOrgDetButton.Enabled = true;
            this.orgNameTextBox.ReadOnly = false;
            this.orgNameTextBox.BackColor = Color.FromArgb(255, 255, 118);
            this.orgTypTextBox.ReadOnly = false;
            this.orgTypTextBox.BackColor = Color.FromArgb(255, 255, 118);
            this.crncyCodeTextBox.ReadOnly = false;
            this.crncyCodeTextBox.BackColor = Color.FromArgb(255, 255, 118);
            this.resAddrsTextBox.ReadOnly = false;
            this.resAddrsTextBox.BackColor = Color.White;
            this.websiteTextBox.ReadOnly = false;
            this.websiteTextBox.BackColor = Color.White;
            this.postalAddrsTextBox.ReadOnly = false;
            this.postalAddrsTextBox.BackColor = Color.White;
            this.emailAddrsTextBox.ReadOnly = false;
            this.emailAddrsTextBox.BackColor = Color.White;
            this.contactNosTextBox.ReadOnly = false;
            this.contactNosTextBox.BackColor = Color.White;
            this.orgDescTextBox.ReadOnly = false;
            this.orgDescTextBox.BackColor = Color.White;
            this.sloganTextBox.ReadOnly = false;
            this.sloganTextBox.BackColor = Color.White;
            this.noOfSgmntsNumUpDown.ReadOnly = false;
            this.noOfSgmntsNumUpDown.BackColor = Color.FromArgb(255, 255, 118);
            this.noOfSgmntsNumUpDown.Increment = 1;
            this.delimiterComboBox.BackColor = Color.FromArgb(255, 255, 118);
        }

        private void disableOrgEdit()
        {
            this.addOrg = false;
            this.editOrg = false;
            this.orgNameTextBox.ReadOnly = true;
            this.orgNameTextBox.BackColor = Color.WhiteSmoke;
            this.orgTypTextBox.ReadOnly = true;
            this.orgTypTextBox.BackColor = Color.WhiteSmoke;
            this.crncyCodeTextBox.ReadOnly = true;
            this.crncyCodeTextBox.BackColor = Color.WhiteSmoke;
            this.resAddrsTextBox.ReadOnly = true;
            this.resAddrsTextBox.BackColor = Color.WhiteSmoke;
            this.websiteTextBox.ReadOnly = true;
            this.websiteTextBox.BackColor = Color.WhiteSmoke;
            this.postalAddrsTextBox.ReadOnly = true;
            this.postalAddrsTextBox.BackColor = Color.WhiteSmoke;
            this.emailAddrsTextBox.ReadOnly = true;
            this.emailAddrsTextBox.BackColor = Color.WhiteSmoke;
            this.contactNosTextBox.ReadOnly = true;
            this.contactNosTextBox.BackColor = Color.WhiteSmoke;
            this.orgIDTextBox.ReadOnly = true;
            this.orgIDTextBox.BackColor = Color.WhiteSmoke;
            this.orgPrntIDTextBox.ReadOnly = true;
            this.orgPrntIDTextBox.BackColor = Color.WhiteSmoke;
            this.orgParentTextBox.ReadOnly = true;
            this.orgParentTextBox.BackColor = Color.WhiteSmoke;
            this.crncyIDTextBox.ReadOnly = true;
            this.crncyIDTextBox.BackColor = Color.WhiteSmoke;
            this.crncyCodeTextBox.ReadOnly = true;
            this.crncyCodeTextBox.BackColor = Color.WhiteSmoke;
            this.orgTypIDTextBox.ReadOnly = true;
            this.orgTypIDTextBox.BackColor = Color.WhiteSmoke;
            this.orgTypTextBox.ReadOnly = true;
            this.orgTypTextBox.BackColor = Color.WhiteSmoke;
            this.orgDescTextBox.ReadOnly = true;
            this.orgDescTextBox.BackColor = Color.WhiteSmoke;
            this.sloganTextBox.ReadOnly = true;
            this.sloganTextBox.BackColor = Color.WhiteSmoke;
            this.noOfSgmntsNumUpDown.ReadOnly = true;
            this.noOfSgmntsNumUpDown.BackColor = Color.WhiteSmoke;
            this.noOfSgmntsNumUpDown.Increment = 0;
            this.delimiterComboBox.BackColor = Color.WhiteSmoke;
        }

        private bool shdObeyOrgDetEvts()
        {
            return this.obey_orgDet_evnts;
        }

        private void OrgDetPnlNavButtons(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecOrgDetLabel.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.is_last_orgDet = false;
                this.orgDet_cur_indx = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.is_last_orgDet = false;
                this.orgDet_cur_indx -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.is_last_orgDet = false;
                this.orgDet_cur_indx += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.is_last_orgDet = true;
                this.totl_orgDet = Global.get_Total_OrgDet(this.searchForOrgDetTextBox.Text,
                    this.searchInOrgDetComboBox.Text);
                this.updtOrgDetTotals();
                this.orgDet_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            this.getOrgDetPnlData();
        }

        private void searchForOrgDetTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.loadOrgDetPanel();
            }
        }

        private void refreshOrgDetButton_Click(object sender, EventArgs e)
        {
            this.loadOrgDetPanel();
            this.Refresh();
        }

        private void extraInfoButton_Click(object sender, EventArgs e)
        {
            if (this.orgIDTextBox.Text == "" || this.orgIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("No record to View!", 0);
                return;
            }
            DialogResult dgres = this.cmCde.showRowsExtInfDiag(this.cmCde.getMdlGrpID("Organization's Details"),
                long.Parse(this.orgIDTextBox.Text), "org.org_all_other_info_table", this.orgNameTextBox.Text, this.editOrgs, 12, 13,
                "org.org_all_other_info_table_dflt_row_id_seq");
            if (dgres == DialogResult.OK)
            {
            }
        }

        private void changeLogoButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[15]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.orgIDTextBox.Text == "" || this.orgIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Can only change the logo of a saved organization", 0);
                return;
            }
            this.orgLogoPictureBox.Image.Dispose();
            if (Global.mnFrm.cmCde.pickAnImage(int.Parse(this.orgIDTextBox.Text),
                ref this.orgLogoPictureBox, 0) == true)
            {
                Global.updtOrgImg(int.Parse(this.orgIDTextBox.Text));
            }
            this.populateOrgDet(int.Parse(this.orgIDTextBox.Text));
        }

        private void addOrgDetButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[14]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                return;
            }
            this.clearOrgDetInfo();
            this.addOrg = true;
            this.editOrg = false;
            this.prpareForOrgEdit();
            this.prpareForLnsEdit();
            this.addOrgDetButton.Enabled = false;
            this.editOrgDetButton.Enabled = false;
        }

        private void editOrgDetButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[15]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.orgIDTextBox.Text == "" || this.orgIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("No record to Edit!", 0);
                return;
            }
            this.addOrg = false;
            this.editOrg = true;
            this.prpareForOrgEdit();
            this.prpareForLnsEdit();
            this.addOrgDetButton.Enabled = false;
            this.editOrgDetButton.Enabled = false;
        }

        private void saveOrgDetButton_Click(object sender, EventArgs e)
        {
            if (this.addOrg == true)
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
            if (this.orgNameTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please enter an Organization name!", 0);
                return;
            }
            if (this.orgTypTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please enter an Organization Type!", 0);
                return;
            }
            if (this.crncyCodeTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please enter an Operational Currency!", 0);
                return;
            }
            int oldOrgID = Global.mnFrm.cmCde.getOrgID(this.orgNameTextBox.Text);
            if (oldOrgID > 0
             && this.addOrg == true)
            {
                Global.mnFrm.cmCde.showMsg("Organisation Name is already in use!", 0);
                return;
            }
            if (oldOrgID > 0
             && this.editOrg == true
             && oldOrgID.ToString() != this.orgIDTextBox.Text)
            {
                Global.mnFrm.cmCde.showMsg("New Organisation Name is already in use!", 0);
                return;
            }

            if (this.addOrg == true)
            {
                Global.createOrg(this.orgNameTextBox.Text, int.Parse(this.orgPrntIDTextBox.Text),
                    this.resAddrsTextBox.Text, this.postalAddrsTextBox.Text,
                    this.websiteTextBox.Text, int.Parse(this.crncyIDTextBox.Text),
                    this.emailAddrsTextBox.Text, this.contactNosTextBox.Text,
                    int.Parse(this.orgTypIDTextBox.Text), this.orgEnabledCheckBox.Checked,
                    this.orgDescTextBox.Text, this.sloganTextBox.Text, (int)this.noOfSgmntsNumUpDown.Value, this.delimiterComboBox.Text);
                this.saveOrgDetButton.Enabled = false;
                this.addOrg = false;
                this.editOrg = false;
                this.editOrgDetButton.Enabled = this.editOrgs;
                this.addOrgDetButton.Enabled = this.addOrgs;
                System.Windows.Forms.Application.DoEvents();
                oldOrgID = Global.mnFrm.cmCde.getOrgID(this.orgNameTextBox.Text);
                this.saveGridView(oldOrgID);
                this.loadOrgDetPanel();
                Global.updtOrgAccntCurrID(oldOrgID, Global.mnFrm.cmCde.getOrgFuncCurID(oldOrgID));
                long rowid = Global.mnFrm.cmCde.getGnrlRecID("scm.scm_dflt_accnts", "rho_name",
        "row_id", "Default Accounts", oldOrgID);
                if (rowid <= 0)
                {
                    Global.createDfltAcnts(oldOrgID);
                }
            }
            else if (this.editOrg == true)
            {
                Global.updateOrgDet(int.Parse(this.orgIDTextBox.Text), this.orgNameTextBox.Text, int.Parse(this.orgPrntIDTextBox.Text),
                    this.resAddrsTextBox.Text, this.postalAddrsTextBox.Text,
                    this.websiteTextBox.Text, int.Parse(this.crncyIDTextBox.Text),
                    this.emailAddrsTextBox.Text, this.contactNosTextBox.Text,
                    int.Parse(this.orgTypIDTextBox.Text), this.orgEnabledCheckBox.Checked,
                    this.orgDescTextBox.Text, this.sloganTextBox.Text, (int)this.noOfSgmntsNumUpDown.Value, this.delimiterComboBox.Text);
                this.saveGridView(int.Parse(this.orgIDTextBox.Text));
                this.saveOrgDetButton.Enabled = false;
                this.editOrg = false;
                this.editOrgDetButton.Enabled = this.editOrgs;
                this.addOrgDetButton.Enabled = this.addOrgs;
                this.loadOrgDetPanel();
                oldOrgID = int.Parse(this.orgIDTextBox.Text);
                Global.updtOrgAccntCurrID(oldOrgID, Global.mnFrm.cmCde.getOrgFuncCurID(oldOrgID));
                long rowid = Global.mnFrm.cmCde.getGnrlRecID("scm.scm_dflt_accnts", "rho_name",
        "row_id", "Default Accounts", oldOrgID);
                if (rowid <= 0)
                {
                    Global.createDfltAcnts(oldOrgID);
                }
            }
        }

        private bool checkDtRqrmnts(int rwIdx)
        {
            if (this.accntSgmntsDataGridView.Rows[rwIdx].Cells[0].Value == null)
            {
                this.accntSgmntsDataGridView.Rows[rwIdx].Cells[0].Value = "-1";
                return false;
            }
            if (this.accntSgmntsDataGridView.Rows[rwIdx].Cells[1].Value == null)
            {
                return false;
            }
            if (this.accntSgmntsDataGridView.Rows[rwIdx].Cells[1].Value.ToString() == "")
            {
                return false;
            }

            if (this.accntSgmntsDataGridView.Rows[rwIdx].Cells[3].Value == null)
            {
                return false;
            }
            if (this.accntSgmntsDataGridView.Rows[rwIdx].Cells[3].Value.ToString() == "")
            {
                return false;
            }

            return true;
        }

        private void saveGridView(long hdrID)
        {
            int svd = 0;
            if (this.accntSgmntsDataGridView.Rows.Count > 0)
            {
                this.accntSgmntsDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
            }

            for (int i = 0; i < this.accntSgmntsDataGridView.Rows.Count; i++)
            {
                if (!this.checkDtRqrmnts(i))
                {
                    this.accntSgmntsDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(255, 100, 100);
                    continue;
                }
                else
                {
                    //Check if Doc Ln Rec Exists
                    //Create if not else update
                    long segmentID = long.Parse(this.accntSgmntsDataGridView.Rows[i].Cells[5].Value.ToString());
                    int segmentNum = int.Parse(this.accntSgmntsDataGridView.Rows[i].Cells[0].Value.ToString());
                    string segmentName = this.accntSgmntsDataGridView.Rows[i].Cells[1].Value.ToString();
                    string sysClsfctn = this.accntSgmntsDataGridView.Rows[i].Cells[3].Value.ToString();

                    if (segmentID <= 0)
                    {
                        segmentID = Global.get_SegmnetID(hdrID, segmentNum);
                    }

                    if (segmentID <= 0)
                    {
                        Global.createAcntSegment(hdrID, segmentNum, segmentName, sysClsfctn);
                        svd++;
                        this.accntSgmntsDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Lime;
                    }
                    else
                    {
                        Global.updtAcntSegment(segmentID, segmentNum, segmentName, sysClsfctn);
                        svd++;
                        this.accntSgmntsDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Lime;
                    }
                }
            }
            Global.mnFrm.cmCde.showMsg(svd + " Line(s) Saved Successfully!", 3);
        }

        private void vwSQLOrgDetButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(this.orgDet_SQL, 12);
        }

        private void recHstryOrgDetButton_Click(object sender, EventArgs e)
        {
            if (this.orgIDTextBox.Text == "-1"
      || this.orgIDTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(Global.get_Org_Rec_Hstry(int.Parse(this.orgIDTextBox.Text)), 13);
        }

        private void saveLogoButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.saveImageToFile(ref this.orgLogoPictureBox);
        }

        private void selPrntOrgButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[15]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            string[] selVals = new string[1];
            selVals[0] = this.orgPrntIDTextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Organisations"), ref selVals, true, false,
             this.srchWrd, "Both", false);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.orgPrntIDTextBox.Text = selVals[i];
                    this.orgParentTextBox.Text = Global.mnFrm.cmCde.getOrgName(int.Parse(selVals[i]));
                }
            }
            if (int.Parse(this.orgIDTextBox.Text) > 0)
            {
                Global.updtOrgPrntID(int.Parse(this.orgIDTextBox.Text), int.Parse(this.orgPrntIDTextBox.Text));
            }
        }

        private void orgTypButton_Click(object sender, EventArgs e)
        {
            //Organisation Types
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[15]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            int[] selValIDs = new int[1];
            selValIDs[0] = int.Parse(this.orgTypIDTextBox.Text);
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Organisation Types"), ref selValIDs, true, true,
             this.srchWrd, "Both", true);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selValIDs.Length; i++)
                {
                    this.orgTypIDTextBox.Text = selValIDs[i].ToString();
                    this.orgTypTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(selValIDs[i]);
                }
            }
            if (int.Parse(this.orgIDTextBox.Text) > 0)
            {
                Global.updtOrgTypID(int.Parse(this.orgIDTextBox.Text), int.Parse(this.orgTypIDTextBox.Text));
            }
        }

        private void selCrncyButton_Click(object sender, EventArgs e)
        {
            //Currencies
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[15]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            int[] selValIDs = new int[1];
            selValIDs[0] = int.Parse(this.orgTypIDTextBox.Text);
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Currencies"), ref selValIDs, true, true,
             this.srchWrd, "Both", true);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selValIDs.Length; i++)
                {
                    this.crncyIDTextBox.Text = selValIDs[i].ToString();
                    this.crncyCodeTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(selValIDs[i]);
                }
            }
            if (int.Parse(this.orgIDTextBox.Text) > 0)
            {
                Global.updtOrgCrncyID(int.Parse(this.orgIDTextBox.Text), int.Parse(this.crncyIDTextBox.Text));
            }
        }

        private void orgDetTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.clearOrgDetInfo();
            if (e.Node == null)
            {
                return;
            }
            this.populateOrgDet(Global.mnFrm.cmCde.getOrgID(e.Node.Text));
        }

        private void exprtOrgTmpltButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtOrgTmp();
        }

        private void imprtOrgTmpltButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[14]) == false)
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
                Global.mnFrm.cmCde.imprtOrgTmp(this.openFileDialog1.FileName);
            }
            this.populateOrgDet();
        }

        private void positionOrgDetTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                this.OrgDetPnlNavButtons(this.movePreviousOrgDetButton, ex);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
            {
                this.OrgDetPnlNavButtons(this.moveNextOrgDetButton, ex);
            }
        }

        private void delOrgDetButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[16]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.orgIDTextBox.Text == "" || this.orgIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select the Organisation to DELETE!", 0);
                return;
            }
            if (Global.isOrgInUse(int.Parse(this.orgIDTextBox.Text)))
            {
                Global.mnFrm.cmCde.showMsg("This Orgaisation either has Accounts and Persons attached or \r\nis Parent to another Organisation hence cannot be DELETED!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Orgaisation?" +
       "\r\nThis action cannot be undone!", 1) == DialogResult.No)
            {
                //Global.myNwMainFrm.cmmnCode.showMsg("Operation Cancelled!", 4);
                return;
            }
            Global.deleteOrg(long.Parse(this.orgIDTextBox.Text), this.orgNameTextBox.Text);

            this.loadCorrectPanel("Organization's Details");

        }

        private void orgEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shdObeyOrgDetEvts() == false
       || beenToCheckBx == true)
            {
                beenToCheckBx = false;
                return;
            }
            beenToCheckBx = true;
            if (this.addOrg == false && this.editOrg == false)
            {
                this.orgEnabledCheckBox.Checked = !this.orgEnabledCheckBox.Checked;
            }
        }
        #endregion

        #region "Divisions/Groups..."
        private void loadDivDetPanel()
        {
            this.obey_divDet_evnts = false;
            if (this.searchInDivComboBox.SelectedIndex < 0)
            {
                this.searchInDivComboBox.SelectedIndex = 0;
            }
            if (this.searchForDivTextBox.Text.Contains("%") == false)
            {
                this.searchForDivTextBox.Text = "%" + this.searchForDivTextBox.Text.Replace(" ", "%") + "%";
            }
            if (this.searchForDivTextBox.Text == "%%")
            {
                this.searchForDivTextBox.Text = "%";
            }
            this.divLogoPictureBox.Image = OrganizationSetup.Properties.Resources.blank;
            int dsply = 0;
            if (this.dsplySizeDivComboBox.Text == ""
                || int.TryParse(this.dsplySizeDivComboBox.Text, out dsply) == false)
            {
                this.dsplySizeDivComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            //this.groupBox3.Height = this.divGrpsPanel.Bottom - this.toolStrip1.Bottom - 50;
            this.is_last_divDet = false;
            this.totl_divDet = Global.mnFrm.cmCde.Big_Val;
            this.getDivDetPnlData();
            this.populateDivLstView();
            this.obey_divDet_evnts = true;
        }

        private void getDivDetPnlData()
        {
            this.updtDivDetTotals();
            this.populateDivLstView();
            this.updtDivDetNavLabels();
        }

        private void updtDivDetTotals()
        {
            Global.mnFrm.cmCde.navFuncts.FindNavigationIndices(long.Parse(this.dsplySizeDivComboBox.Text), this.totl_divDet);
            if (this.divDet_cur_indx >= Global.mnFrm.cmCde.navFuncts.totalGroups)
            {
                this.divDet_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            if (this.divDet_cur_indx < 0)
            {
                this.divDet_cur_indx = 0;
            }
            Global.mnFrm.cmCde.navFuncts.currentNavigationIndex = this.divDet_cur_indx;
        }

        private void updtDivDetNavLabels()
        {
            this.moveFirstDivButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveFirstBtnStatus();
            this.movePreviousDivButton.Enabled = Global.mnFrm.cmCde.navFuncts.movePrevBtnStatus();
            this.moveNextDivButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveNextBtnStatus();
            this.moveLastDivButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveLastBtnStatus();
            this.positionDivTextBox.Text = Global.mnFrm.cmCde.navFuncts.displayedRecordsNumbers();
            if (this.is_last_divDet == true ||
                this.totl_divDet != Global.mnFrm.cmCde.Big_Val)
            {
                this.totalRecDivLabel.Text = Global.mnFrm.cmCde.navFuncts.totalRecordsLabel();
            }
            else
            {
                this.totalRecDivLabel.Text = "of Total";
            }
        }

        //private void populateDivDetTreeView()
        // {
        // this.obey_divDet_evnts = false;
        // DataSet dtst = Global.get_Hrchy_DivDet(this.searchForDivTextBox.Text,
        //  this.searchInDivComboBox.Text, 0, int.Parse(this.dsplySizeDivComboBox.Text), Global.mnFrm.cmCde.Org_id);
        // this.divHrchyTreeView.Nodes.Clear();
        // if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[2]) == false)
        //  {
        //  Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
        //   " this action!\nContact your System Administrator!", 0);
        //  this.obey_divDet_evnts = true;
        //  return;
        //  }
        // TreeNode[] nwNode = new TreeNode[dtst.Tables[0].Rows.Count];

        // for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
        //  {
        //  TreeNode aNode = new TreeNode();
        //  aNode.Name = "divNode" + int.Parse(dtst.Tables[0].Rows[i][0].ToString()).ToString();
        //  aNode.Text = dtst.Tables[0].Rows[i][2].ToString();
        //  //aNode.ImageKey = menuImages[i];
        //  nwNode[i] = aNode;
        //  if (int.Parse(dtst.Tables[0].Rows[i][3].ToString()) == 1)
        //   {
        //   this.divHrchyTreeView.Nodes.Add(nwNode[i]);
        //   }
        //  else
        //   {
        //   try
        //    {
        //    string prntNodeNm = "divNode" + int.Parse(dtst.Tables[0].Rows[i][1].ToString()).ToString();
        //    this.getNode(nwNode, prntNodeNm).Nodes.Add(nwNode[i]);
        //    }
        //   catch (Exception ex)
        //    {
        //    }
        //   }
        //  this.divHrchyTreeView.ExpandAll();
        //  }
        // this.obey_divDet_evnts = true;
        // }

        private void populateDivLstView()
        {
            this.obey_divDet_evnts = false;
            DataSet dtst = Global.get_Basic_DivDet(this.searchForDivTextBox.Text,
                this.searchInDivComboBox.Text, this.divDet_cur_indx,
                int.Parse(this.dsplySizeDivComboBox.Text), Global.mnFrm.cmCde.Org_id);
            this.divListView.Items.Clear();

            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.last_div_num = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
                ListViewItem nwItem = new ListViewItem(new string[] {
    (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),
    dtst.Tables[0].Rows[i][0].ToString()});
                this.divListView.Items.Add(nwItem);
            }
            if (this.divListView.Items.Count > 0)
            {
                this.obey_divDet_evnts = true;
                this.divListView.Items[0].Selected = true;
            }
            else
            {
                populateDivDet(-100000);
            }
            this.correctDivNavLbls(dtst);
            this.obey_divDet_evnts = true;
        }

        private void populateDivDet(int divID)
        {
            this.clearDivDetInfo();
            this.disableDivEdit();
            this.obey_divDet_evnts = false;
            DataSet dtst = Global.get_One_DivDet_Det(divID);
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.divIDTextBox.Text = dtst.Tables[0].Rows[i][0].ToString();
                this.divNameTextBox.Text = dtst.Tables[0].Rows[i][1].ToString();
                this.parentDivIDTextBox.Text = dtst.Tables[0].Rows[i][2].ToString();
                this.parentDivTextBox.Text = dtst.Tables[0].Rows[i][3].ToString();
                this.divTypIDTextBox.Text = dtst.Tables[0].Rows[i][4].ToString();
                this.divTypTextBox.Text = dtst.Tables[0].Rows[i][5].ToString();
                this.isDivEnbldCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][7].ToString());
                this.divDescTextBox.Text = dtst.Tables[0].Rows[i][8].ToString();
                Global.mnFrm.cmCde.getDBImageFile(dtst.Tables[0].Rows[i][6].ToString(), 1, ref this.divLogoPictureBox);
            }
            this.obey_divDet_evnts = true;
        }

        private void populateDivDet()
        {
            this.clearDivDetInfo();
            this.disableDivEdit();
            this.obey_divDet_evnts = false;
            DataSet dtst = Global.get_Basic_DivDet(this.searchForDivTextBox.Text,
                this.searchInDivComboBox.Text, this.divDet_cur_indx, 1, Global.mnFrm.cmCde.Org_id);
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.last_div_num = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
                this.divIDTextBox.Text = dtst.Tables[0].Rows[i][0].ToString();
                this.divNameTextBox.Text = dtst.Tables[0].Rows[i][1].ToString();
                this.parentDivIDTextBox.Text = dtst.Tables[0].Rows[i][2].ToString();
                this.parentDivTextBox.Text = dtst.Tables[0].Rows[i][3].ToString();
                this.divTypIDTextBox.Text = dtst.Tables[0].Rows[i][4].ToString();
                this.divTypTextBox.Text = dtst.Tables[0].Rows[i][5].ToString();
                this.isDivEnbldCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][7].ToString());
                this.divDescTextBox.Text = dtst.Tables[0].Rows[i][8].ToString();
                Global.mnFrm.cmCde.getDBImageFile(dtst.Tables[0].Rows[i][6].ToString(), 1, ref this.divLogoPictureBox);
            }
            this.correctDivNavLbls(dtst);
            this.obey_divDet_evnts = true;
        }

        private void correctDivNavLbls(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.divDet_cur_indx == 0 && totlRecs == 0)
            {
                this.is_last_divDet = true;
                this.totl_divDet = 0;
                this.last_div_num = 0;
                this.divDet_cur_indx = 0;
                this.updtDivDetTotals();
                this.updtDivDetNavLabels();
            }
            else if (this.totl_divDet == Global.mnFrm.cmCde.Big_Val
      && totlRecs < long.Parse(this.dsplySizeDivComboBox.Text))
            {
                this.totl_divDet = this.last_div_num;
                if (totlRecs == 0)
                {
                    this.divDet_cur_indx -= 1;
                    this.updtDivDetTotals();
                    this.populateDivLstView();
                }
                else
                {
                    this.updtDivDetTotals();
                }
            }
        }

        private void clearDivDetInfo()
        {
            this.obey_divDet_evnts = false;
            this.saveDivButton.Enabled = false;
            this.addDivButton.Enabled = this.addDivs;
            this.editDivButton.Enabled = this.editDivs;
            this.divIDTextBox.Text = "-1";
            this.divNameTextBox.Text = "";
            this.parentDivIDTextBox.Text = "-1";
            this.parentDivTextBox.Text = "";
            this.divTypIDTextBox.Text = "-1";
            this.divTypTextBox.Text = "";
            this.divDescTextBox.Text = "";
            this.isDivEnbldCheckBox.Checked = false;
            this.divLogoPictureBox.Image = OrganizationSetup.Properties.Resources.blank;
            this.obey_divDet_evnts = true;
        }

        private void prpareForDivEdit()
        {
            this.saveDivButton.Enabled = true;
            this.divNameTextBox.ReadOnly = false;
            this.divNameTextBox.BackColor = Color.FromArgb(255, 255, 118);
            this.divDescTextBox.ReadOnly = false;
            this.divDescTextBox.BackColor = Color.White;

            this.parentDivTextBox.ReadOnly = false;
            this.parentDivTextBox.BackColor = Color.White;

            this.divTypTextBox.ReadOnly = false;
            this.divTypTextBox.BackColor = Color.FromArgb(255, 255, 118);
        }

        private void disableDivEdit()
        {
            this.addDiv = false;
            this.editDiv = false;
            this.divNameTextBox.ReadOnly = true;
            this.divNameTextBox.BackColor = Color.WhiteSmoke;
            this.divIDTextBox.ReadOnly = true;
            this.divIDTextBox.BackColor = Color.WhiteSmoke;
            this.parentDivIDTextBox.ReadOnly = true;
            this.parentDivIDTextBox.BackColor = Color.WhiteSmoke;
            this.parentDivTextBox.ReadOnly = true;
            this.parentDivTextBox.BackColor = Color.WhiteSmoke;
            this.divTypIDTextBox.ReadOnly = true;
            this.divTypIDTextBox.BackColor = Color.WhiteSmoke;
            this.divTypTextBox.ReadOnly = true;
            this.divTypTextBox.BackColor = Color.WhiteSmoke;
            this.divDescTextBox.ReadOnly = true;
            this.divDescTextBox.BackColor = Color.WhiteSmoke;
        }

        private bool shdObeyDivDetEvts()
        {
            return this.obey_divDet_evnts;
        }

        private void DivDetPnlNavButtons(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecDivLabel.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.is_last_divDet = false;
                this.divDet_cur_indx = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.is_last_divDet = false;
                this.divDet_cur_indx -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.is_last_divDet = false;
                this.divDet_cur_indx += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.is_last_divDet = true;
                this.totl_divDet = Global.get_Total_DivDet(this.searchForDivTextBox.Text,
                    this.searchInDivComboBox.Text, Global.mnFrm.cmCde.Org_id);
                this.updtDivDetTotals();
                this.divDet_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            this.getDivDetPnlData();
        }

        private void goDivButton_Click(object sender, EventArgs e)
        {
            this.loadDivDetPanel();
        }

        private void changeDivLogoButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.divIDTextBox.Text == "" || this.divIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Can only change the logo of a saved division", 0);
                return;
            }
            this.divLogoPictureBox.Image.Dispose();
            if (Global.mnFrm.cmCde.pickAnImage(int.Parse(this.divIDTextBox.Text),
                ref this.divLogoPictureBox, 1) == true)
            {
                Global.updtDivImg(int.Parse(this.divIDTextBox.Text));
            }
            this.populateDivDet(int.Parse(this.divIDTextBox.Text));
        }

        private void saveDivLogoButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.saveImageToFile(ref this.divLogoPictureBox);
        }

        private void divExtraInfoButton_Click(object sender, EventArgs e)
        {
            if (this.divIDTextBox.Text == "" || this.divIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("No record to View!", 0);
                return;
            }
            DialogResult dgres = this.cmCde.showRowsExtInfDiag(this.cmCde.getMdlGrpID("Divisions/Groups"),
                long.Parse(this.divIDTextBox.Text), "org.org_all_other_info_table", this.divNameTextBox.Text, this.editDivs, 12, 13,
                "org.org_all_other_info_table_dflt_row_id_seq");
            if (dgres == DialogResult.OK)
            {
            }
        }

        private void addDivButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[16]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                return;
            }
            this.clearDivDetInfo();
            this.prpareForDivEdit();
            this.addDivButton.Enabled = false;
            this.editDivButton.Enabled = false;
            this.addDiv = true;
            this.editDiv = false;
        }

        private void editDivButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.divIDTextBox.Text == "" || this.divIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("No record to Edit!", 0);
                return;
            }
            this.prpareForDivEdit();
            this.addDivButton.Enabled = false;
            this.editDivButton.Enabled = false;
            this.addDiv = false;
            this.editDiv = true;
        }

        private void saveDivButton_Click(object sender, EventArgs e)
        {
            if (this.addDiv == true)
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[16]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                     " this action!\nContact your System Administrator!", 0);
                    return;
                }
            }
            else
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                     " this action!\nContact your System Administrator!", 0);
                    return;
                }
            }
            if (this.divNameTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please enter a Division name!", 0);
                return;
            }
            if (this.divTypTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please enter a Division Type!", 0);
                return;
            }
            long oldDivID = Global.mnFrm.cmCde.getDivID(this.divNameTextBox.Text, Global.mnFrm.cmCde.Org_id);
            if (oldDivID > 0
             && this.addDiv == true)
            {
                Global.mnFrm.cmCde.showMsg("Division/Group's Name is already in use in this Organisation!", 0);
                return;
            }
            if (oldDivID > 0
             && this.editDiv == true
             && oldDivID.ToString() != this.divIDTextBox.Text)
            {
                Global.mnFrm.cmCde.showMsg("New Division/Group's Name is already in use in this Organisation!", 0);
                return;
            }
            if (this.addDiv == true)
            {
                Global.createDiv(Global.mnFrm.cmCde.Org_id,
                    this.divNameTextBox.Text, int.Parse(this.parentDivIDTextBox.Text),
                    int.Parse(this.divTypIDTextBox.Text), this.isDivEnbldCheckBox.Checked, this.divDescTextBox.Text);
                this.saveDivButton.Enabled = false;
                this.addDiv = false;
                this.editDiv = false;
                this.editDivButton.Enabled = this.editDivs;
                this.addDivButton.Enabled = this.addDivs;
                System.Windows.Forms.Application.DoEvents();
                this.loadDivDetPanel();
            }
            else if (this.editDiv == true)
            {
                Global.updateDivDet(int.Parse(this.divIDTextBox.Text), this.divNameTextBox.Text, int.Parse(this.parentDivIDTextBox.Text),
                int.Parse(this.divTypIDTextBox.Text), this.isDivEnbldCheckBox.Checked, this.divDescTextBox.Text);
                this.saveDivButton.Enabled = false;
                this.editDiv = false;
                this.editDivButton.Enabled = this.editDivs;
                this.addDivButton.Enabled = this.addDivs;
                this.loadDivDetPanel();
            }
        }

        private void vwSQLDivButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(this.divDet_SQL, 12);
        }

        private void recHstryDivButton_Click(object sender, EventArgs e)
        {
            if (this.divIDTextBox.Text == "-1"
      || this.divIDTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(Global.get_Div_Rec_Hstry(int.Parse(this.divIDTextBox.Text)), 13);
        }

        private void parntDivButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            string[] selVals = new string[1];
            selVals[0] = this.parentDivIDTextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Divisions/Groups"), ref selVals,
                true, false, Global.mnFrm.cmCde.Org_id,
             this.srchWrd, "Both", true);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.parentDivIDTextBox.Text = selVals[i];
                    this.parentDivTextBox.Text = Global.mnFrm.cmCde.getDivName(int.Parse(selVals[i]));
                }
            }
            if (int.Parse(this.divIDTextBox.Text) > 0)
            {
                Global.updtDivPrntID(int.Parse(this.divIDTextBox.Text), int.Parse(this.parentDivIDTextBox.Text));
            }
        }

        private void divTypButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            //Division Types
            int[] selValIDs = new int[1];
            selValIDs[0] = int.Parse(this.divTypIDTextBox.Text);
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Divisions or Group Types"), ref selValIDs, true, true,
             this.srchWrd, "Both", true);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selValIDs.Length; i++)
                {
                    this.divTypIDTextBox.Text = selValIDs[i].ToString();
                    this.divTypTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(selValIDs[i]);
                }
            }
            if (int.Parse(this.divIDTextBox.Text) > 0)
            {
                Global.updtDivTypID(int.Parse(this.divIDTextBox.Text), int.Parse(this.divTypIDTextBox.Text));
            }
        }

        //private void divHrchyTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        //{
        //  this.clearDivDetInfo();
        //  if (e.Node == null)
        //  {
        //    return;
        //  }
        //  this.populateDivDet(Global.mnFrm.cmCde.getDivID(e.Node.Text, Global.mnFrm.cmCde.Org_id));
        //}

        private void divListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.shdObeyDivDetEvts() == false)
            {
                return;
            }
            if (this.divListView.SelectedItems.Count > 0)
            {
                this.populateDivDet(int.Parse(this.divListView.SelectedItems[0].SubItems[2].Text));
            }
            else
            {
                this.populateDivDet(-100000);
            }
        }

        private void exptDivMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.divListView);
        }

        private void exprtDivTmpButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtDivTmp();
        }

        private void imprtDivButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[16]) == false)
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
                Global.mnFrm.cmCde.imprtDivTmp(this.openFileDialog1.FileName);
            }
            this.populateDivLstView();
        }

        private void searchForDivTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.goDivButton_Click(this.goDivButton, ex);
            }
        }

        private void positionDivTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                this.DivDetPnlNavButtons(this.movePreviousDivButton, ex);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
            {
                this.DivDetPnlNavButtons(this.moveNextDivButton, ex);
            }
        }

        private void delDivButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[18]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.divIDTextBox.Text == "" || this.divIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select the Division/Group to DELETE!", 0);
                return;
            }
            if (Global.isDivInUse(int.Parse(this.divIDTextBox.Text)) == true)
            {
                Global.mnFrm.cmCde.showMsg("This Division/Group has been assigned to Persons hence cannot be DELETED!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Division/Group?" +
       "\r\nThis action cannot be undone!", 1) == DialogResult.No)
            {
                Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            Global.deleteDiv(int.Parse(this.divIDTextBox.Text), this.divNameTextBox.Text);
            this.loadDivDetPanel();
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
            this.goDivButton_Click(this.goDivButton, e);
        }

        private void rcHstryDivMenuItem_Click(object sender, EventArgs e)
        {
            this.recHstryDivButton_Click(this.recHstryDivButton, e);
        }

        private void vwSQLDivMenuItem_Click(object sender, EventArgs e)
        {
            this.vwSQLDivButton_Click(this.vwSQLDivButton, e);
        }

        private void isDivEnbldCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shdObeyDivDetEvts() == false
            || beenToCheckBx == true)
            {
                beenToCheckBx = false;
                return;
            }
            beenToCheckBx = true;
            if (this.addDiv == false && this.editDiv == false)
            {
                this.isDivEnbldCheckBox.Checked = !this.isDivEnbldCheckBox.Checked;
            }
        }
        #endregion

        #region "Site Details..."
        private void loadSitePanel()
        {
            this.obey_site_evnts = false;
            if (this.searchInSiteComboBox.SelectedIndex < 0)
            {
                this.searchInSiteComboBox.SelectedIndex = 1;
            }
            if (this.searchForSiteTextBox.Text.Contains("%") == false)
            {
                this.searchForSiteTextBox.Text = "%" + this.searchForSiteTextBox.Text.Replace(" ", "%") + "%";
            }
            if (this.searchForSiteTextBox.Text == "%%")
            {
                this.searchForSiteTextBox.Text = "%";
            }
            int dsply = 0;
            if (this.dsplySizeSiteComboBox.Text == ""
                || int.TryParse(this.dsplySizeSiteComboBox.Text, out dsply) == false)
            {
                this.dsplySizeSiteComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            //this.sitesListView.Height = this.sitesPanel.Bottom - this.toolStrip2.Bottom - 50;
            this.is_last_site = false;
            this.totl_site = Global.mnFrm.cmCde.Big_Val;
            this.getSitePnlData();
            this.obey_site_evnts = true;
        }

        private void getSitePnlData()
        {
            this.updtSiteTotals();
            this.populateSiteListView();
            this.updtSiteNavLabels();
        }

        private void updtSiteTotals()
        {
            Global.mnFrm.cmCde.navFuncts.FindNavigationIndices(int.Parse(this.dsplySizeSiteComboBox.Text), this.totl_site);
            if (this.site_cur_indx >= Global.mnFrm.cmCde.navFuncts.totalGroups)
            {
                this.site_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            if (this.site_cur_indx < 0)
            {
                this.site_cur_indx = 0;
            }
            Global.mnFrm.cmCde.navFuncts.currentNavigationIndex = this.site_cur_indx;
        }

        private void updtSiteNavLabels()
        {
            this.moveFirstSiteButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveFirstBtnStatus();
            this.movePreviousSiteButton.Enabled = Global.mnFrm.cmCde.navFuncts.movePrevBtnStatus();
            this.moveNextSiteButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveNextBtnStatus();
            this.moveLastSiteButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveLastBtnStatus();
            this.positionSiteTextBox.Text = Global.mnFrm.cmCde.navFuncts.displayedRecordsNumbers();
            if (this.is_last_site == true ||
                this.totl_site != Global.mnFrm.cmCde.Big_Val)
            {
                this.totalRecSiteLabel.Text = Global.mnFrm.cmCde.navFuncts.totalRecordsLabel();
            }
            else
            {
                this.totalRecSiteLabel.Text = "of Total";
            }
        }

        private void populateSiteDet(int siteID)
        {
            this.clearSiteInfo();
            this.disableSiteEdit();
            this.obey_site_evnts = false;
            DataSet dtst = Global.get_One_Site_Det(siteID);

            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.siteIDTextBox.Text = dtst.Tables[0].Rows[i][0].ToString();
                this.siteNameTextBox.Text = dtst.Tables[0].Rows[i][1].ToString();
                this.siteDescTextBox.Text = dtst.Tables[0].Rows[i][2].ToString();
                this.isEnabledSitesCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][3].ToString());
            }
            this.obey_site_evnts = true;
        }

        private void populateSiteListView()
        {
            this.clearSiteInfo();
            this.disableSiteEdit();
            this.obey_site_evnts = false;
            DataSet dtst = Global.get_Basic_Site(this.searchForSiteTextBox.Text,
                this.searchInSiteComboBox.Text, this.site_cur_indx, int.Parse(this.dsplySizeSiteComboBox.Text), Global.mnFrm.cmCde.Org_id);
            this.sitesListView.Items.Clear();
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.last_site_num = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
                ListViewItem nwItem = new ListViewItem(new string[] {
    (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),
    dtst.Tables[0].Rows[i][0].ToString()});
                this.sitesListView.Items.Add(nwItem);
            }
            this.correctSiteNavLbls(dtst);
            if (this.sitesListView.Items.Count > 0)
            {
                this.obey_site_evnts = true;
                this.sitesListView.Items[0].Selected = true;
            }
            else
            {
                this.populateSiteDet(-100000);
            }
            this.obey_site_evnts = true;
        }

        private void correctSiteNavLbls(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.site_cur_indx == 0 && totlRecs == 0)
            {
                this.is_last_site = true;
                this.totl_site = 0;
                this.last_site_num = 0;
                this.site_cur_indx = 0;
                this.updtSiteTotals();
                this.updtSiteNavLabels();
            }
            else if (this.totl_site == Global.mnFrm.cmCde.Big_Val
      && totlRecs < int.Parse(this.dsplySizeSiteComboBox.Text))
            {
                this.totl_site = this.last_site_num;
                if (totlRecs == 0)
                {
                    this.site_cur_indx -= 1;
                    this.updtSiteTotals();
                    this.populateSiteListView();
                }
                else
                {
                    this.updtSiteTotals();
                }
            }
        }

        private void clearSiteInfo()
        {
            this.obey_site_evnts = false;
            this.saveSiteButton.Enabled = false;
            this.addSiteButton.Enabled = this.addSites;
            this.editSiteButton.Enabled = this.editSites;
            this.siteNameTextBox.Text = "";
            this.siteIDTextBox.Text = "-1";
            this.siteDescTextBox.Text = "";
            this.isEnabledSitesCheckBox.Checked = false;
            this.obey_site_evnts = true;
        }

        private void prpareForSiteEdit()
        {
            this.saveSiteButton.Enabled = true;
            this.siteNameTextBox.ReadOnly = false;
            this.siteNameTextBox.BackColor = Color.FromArgb(255, 255, 118);
            this.siteDescTextBox.ReadOnly = false;
            this.siteDescTextBox.BackColor = Color.White;
        }

        private void disableSiteEdit()
        {
            this.addSite = false;
            this.editSite = false;
            this.siteNameTextBox.ReadOnly = true;
            this.siteNameTextBox.BackColor = Color.WhiteSmoke;
            this.siteIDTextBox.ReadOnly = true;
            this.siteIDTextBox.BackColor = Color.WhiteSmoke;
            this.siteDescTextBox.ReadOnly = true;
            this.siteDescTextBox.BackColor = Color.WhiteSmoke;
        }

        private bool shdObeySiteEvts()
        {
            return this.obey_site_evnts;
        }

        private void SitePnlNavButtons(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecSiteLabel.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.is_last_site = false;
                this.site_cur_indx = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.is_last_site = false;
                this.site_cur_indx -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.is_last_site = false;
                this.site_cur_indx += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.is_last_site = true;
                this.totl_site = Global.get_Total_Sites(this.searchForSiteTextBox.Text,
                    this.searchInSiteComboBox.Text, Global.mnFrm.cmCde.Org_id);
                this.updtSiteTotals();
                this.site_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            this.getSitePnlData();
        }

        private void goSiteButton_Click(object sender, EventArgs e)
        {
            this.loadSitePanel();
        }

        private void addSiteButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[19]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                return;
            }
            this.clearSiteInfo();
            this.addSite = true;
            this.editSite = false;
            this.prpareForSiteEdit();
            this.addSiteButton.Enabled = false;
            this.editSiteButton.Enabled = false;
        }

        private void editSiteButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[20]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.siteIDTextBox.Text == "" || this.siteIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("No record to Edit!", 0);
                return;
            }
            this.addSite = false;
            this.editSite = true;
            this.prpareForSiteEdit();
            this.addSiteButton.Enabled = false;
            this.editSiteButton.Enabled = false;
        }

        private void saveSiteButton_Click(object sender, EventArgs e)
        {
            if (this.addSite == true)
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[19]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                     " this action!\nContact your System Administrator!", 0);
                    return;
                }
            }
            else
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[20]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                     " this action!\nContact your System Administrator!", 0);
                    return;
                }
            }
            if (this.siteNameTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please enter a Site name!", 0);
                return;
            }
            long oldSiteID = Global.mnFrm.cmCde.getSiteID(this.siteNameTextBox.Text, Global.mnFrm.cmCde.Org_id);
            if (oldSiteID > 0
             && this.addSite == true)
            {
                Global.mnFrm.cmCde.showMsg("Site/Location's Name is already in use in this Organisation!", 0);
                return;
            }
            if (oldSiteID > 0
             && this.editSite == true
             && oldSiteID.ToString() != this.siteIDTextBox.Text)
            {
                Global.mnFrm.cmCde.showMsg("New Site/Location's Name is already in use in this Organisation!", 0);
                return;
            }

            if (this.addSite == true)
            {
                Global.createSite(Global.mnFrm.cmCde.Org_id, this.siteNameTextBox.Text,
                    this.siteDescTextBox.Text, this.isEnabledSitesCheckBox.Checked);

                this.saveSiteButton.Enabled = false;
                this.addSite = false;
                this.editSite = false;
                this.editSiteButton.Enabled = this.editSites;
                this.addSiteButton.Enabled = this.addSites;
                System.Windows.Forms.Application.DoEvents();
                this.loadSitePanel();
            }
            else if (this.editSite == true)
            {
                Global.updateSiteDet(int.Parse(this.siteIDTextBox.Text), this.siteNameTextBox.Text,
                    this.siteDescTextBox.Text, this.isEnabledSitesCheckBox.Checked);

                this.saveSiteButton.Enabled = false;
                this.editSite = false;
                this.editSiteButton.Enabled = this.editSites;
                this.addSiteButton.Enabled = this.addSites;
                this.loadSitePanel();
            }
        }

        private void sitesListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.shdObeySiteEvts() == false)
            {
                return;
            }
            if (this.sitesListView.SelectedItems.Count > 0)
            {
                this.populateSiteDet(int.Parse(this.sitesListView.SelectedItems[0].SubItems[2].Text));
            }
            else
            {
                this.populateSiteDet(-100000);
            }
        }

        private void vwSQLSiteButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(this.site_SQL, 12);
        }

        private void recHstrySiteButton_Click(object sender, EventArgs e)
        {
            if (this.siteIDTextBox.Text == "-1"
      || this.siteIDTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(Global.get_Site_Rec_Hstry(int.Parse(this.siteIDTextBox.Text)), 13);
        }

        private void sitesExtraInfoButton_Click(object sender, EventArgs e)
        {
            if (this.siteIDTextBox.Text == ""
                || this.siteIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("No record to View!", 0);
                return;
            }
            DialogResult dgres = this.cmCde.showRowsExtInfDiag(this.cmCde.getMdlGrpID("Sites/Locations"),
                long.Parse(this.siteIDTextBox.Text), "org.org_all_other_info_table",
                this.siteNameTextBox.Text, this.editSites, 12, 13,
                "org.org_all_other_info_table_dflt_row_id_seq");
            if (dgres == DialogResult.OK)
            {
            }
        }

        private void exptSiteMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.sitesListView);
        }

        private void exprtSiteButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtSiteTmp();
        }

        private void searchForSiteTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.goSiteButton_Click(this.goSiteButton, ex);
            }
        }

        private void positionSiteTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                this.SitePnlNavButtons(this.movePreviousSiteButton, ex);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
            {
                this.SitePnlNavButtons(this.moveNextSiteButton, ex);
            }
        }

        private void imprtSiteButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[19]) == false)
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
                Global.mnFrm.cmCde.imprtSiteTmp(this.openFileDialog1.FileName);
            }
            this.populateSiteListView();
        }

        private void delSiteButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[21]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }

            if (this.siteIDTextBox.Text == "" || this.siteIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select the Site/Location to DELETE!", 0);
                return;
            }
            if (Global.isSiteInUse(int.Parse(this.siteIDTextBox.Text)) == true)
            {
                Global.mnFrm.cmCde.showMsg("This Site/Location has been assigned to Persons hence cannot be DELETED!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Site/Location?" +
       "\r\nThis action cannot be undone!", 1) == DialogResult.No)
            {
                Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            Global.deleteSite(int.Parse(this.siteIDTextBox.Text), this.siteNameTextBox.Text);
            this.loadSitePanel();
        }

        private void addSiteMenuItem_Click(object sender, EventArgs e)
        {
            this.addSiteButton_Click(this.addSiteButton, e);
        }

        private void editSiteMenuItem_Click(object sender, EventArgs e)
        {
            this.editSiteButton_Click(this.editSiteButton, e);
        }

        private void delSiteMenuItem_Click(object sender, EventArgs e)
        {
            this.delSiteButton_Click(this.delSiteButton, e);
        }

        private void rfrshSiteMenuItem_Click(object sender, EventArgs e)
        {
            this.goSiteButton_Click(this.goSiteButton, e);
        }

        private void rcHstrySiteMenuItem_Click(object sender, EventArgs e)
        {
            this.recHstrySiteButton_Click(this.recHstrySiteButton, e);
        }

        private void vwSQLSiteMenuItem_Click(object sender, EventArgs e)
        {
            this.vwSQLSiteButton_Click(this.vwSQLSiteButton, e);
        }

        private void isEnabledSitesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shdObeySiteEvts() == false
                  || beenToCheckBx == true)
            {
                beenToCheckBx = false;
                return;
            }
            beenToCheckBx = true;
            if (this.addSite == false && this.editSite == false)
            {
                this.isEnabledSitesCheckBox.Checked = !this.isEnabledSitesCheckBox.Checked;
            }
        }
        #endregion

        #region "Jobs..."
        private void loadJobsPanel()
        {
            this.obey_jobs_evnts = false;
            if (this.searchInJobsComboBox.SelectedIndex < 0)
            {
                this.searchInJobsComboBox.SelectedIndex = 0;
            }
            if (this.searchForJobsTextBox.Text.Contains("%") == false)
            {
                this.searchForJobsTextBox.Text = "%" + this.searchForJobsTextBox.Text.Replace(" ", "%") + "%";
            }
            if (this.searchForJobsTextBox.Text == "%%")
            {
                this.searchForJobsTextBox.Text = "%";
            }
            int dsply = 0;
            if (this.dsplySizeJobsComboBox.Text == ""
                || int.TryParse(this.dsplySizeJobsComboBox.Text, out dsply) == false)
            {
                this.dsplySizeJobsComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            //this.groupBox6.Height = this.jobsPanel.Bottom - this.toolStrip4.Bottom - 50;
            this.is_last_job = false;
            this.totl_jobs = Global.mnFrm.cmCde.Big_Val;
            this.getJobsPnlData();
            this.populateJobsLstView();
            this.obey_jobs_evnts = true;
        }

        private void getJobsPnlData()
        {
            this.updtJobsTotals();
            this.populateJobsLstView();
            this.updtJobsNavLabels();
        }

        private void updtJobsTotals()
        {
            Global.mnFrm.cmCde.navFuncts.FindNavigationIndices(long.Parse(this.dsplySizeJobsComboBox.Text), this.totl_jobs);
            if (this.jobs_cur_indx >= Global.mnFrm.cmCde.navFuncts.totalGroups)
            {
                this.jobs_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            if (this.jobs_cur_indx < 0)
            {
                this.jobs_cur_indx = 0;
            }
            Global.mnFrm.cmCde.navFuncts.currentNavigationIndex = this.jobs_cur_indx;
        }

        private void updtJobsNavLabels()
        {
            this.moveFirstJobsButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveFirstBtnStatus();
            this.movePreviousJobsButton.Enabled = Global.mnFrm.cmCde.navFuncts.movePrevBtnStatus();
            this.moveNextJobsButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveNextBtnStatus();
            this.moveLastJobsButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveLastBtnStatus();
            this.positionJobsTextBox.Text = Global.mnFrm.cmCde.navFuncts.displayedRecordsNumbers();
            if (this.is_last_job == true ||
                this.totl_jobs != Global.mnFrm.cmCde.Big_Val)
            {
                this.totalRecsJobsLabel.Text = Global.mnFrm.cmCde.navFuncts.totalRecordsLabel();
            }
            else
            {
                this.totalRecsJobsLabel.Text = "of Total";
            }
        }

        private void populateJobsLstView()
        {
            this.clearJobsInfo();
            this.disableJobsEdit();
            this.obey_jobs_evnts = false;
            DataSet dtst = Global.get_Basic_Job(this.searchForJobsTextBox.Text,
                this.searchInJobsComboBox.Text, this.jobs_cur_indx, int.Parse(this.dsplySizeJobsComboBox.Text), Global.mnFrm.cmCde.Org_id);
            this.jobListView.Items.Clear();
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.last_job_num = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
                ListViewItem nwItem = new ListViewItem(new string[] {
    (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),
    dtst.Tables[0].Rows[i][0].ToString()});
                this.jobListView.Items.Add(nwItem);
            }
            if (this.jobListView.Items.Count > 0)
            {
                this.obey_jobs_evnts = true;
                this.jobListView.Items[0].Selected = true;
            }
            else
            {
                this.populateJobs(-100000);
            }
            this.correctJobsNavLbls(dtst);
            this.obey_jobs_evnts = true;
        }

        private void populateJobs(int jobID)
        {
            this.clearJobsInfo();
            this.disableJobsEdit();

            this.obey_jobs_evnts = false;
            DataSet dtst = Global.get_One_Job(jobID);
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.jobIDTextBox.Text = dtst.Tables[0].Rows[i][0].ToString();
                this.jobNameTextBox.Text = dtst.Tables[0].Rows[i][1].ToString();
                this.parentJobIDTextBox.Text = dtst.Tables[0].Rows[i][2].ToString();
                this.parentJobTextBox.Text = dtst.Tables[0].Rows[i][3].ToString();
                this.jobDescTextBox.Text = dtst.Tables[0].Rows[i][4].ToString();
                this.isEnabldJobsCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][5].ToString());
            }
            this.obey_jobs_evnts = true;
        }

        private void populateJobs()
        {
            this.clearJobsInfo();
            this.disableJobsEdit();
            this.obey_jobs_evnts = false;
            DataSet dtst = Global.get_Basic_Job(this.searchForJobsTextBox.Text,
                this.searchInJobsComboBox.Text, this.jobs_cur_indx, int.Parse(this.dsplySizeJobsComboBox.Text), Global.mnFrm.cmCde.Org_id);
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.last_job_num = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
                this.jobIDTextBox.Text = dtst.Tables[0].Rows[i][0].ToString();
                this.jobNameTextBox.Text = dtst.Tables[0].Rows[i][1].ToString();
                this.parentJobIDTextBox.Text = dtst.Tables[0].Rows[i][2].ToString();
                this.parentJobTextBox.Text = dtst.Tables[0].Rows[i][3].ToString();
                this.jobDescTextBox.Text = dtst.Tables[0].Rows[i][4].ToString();
                this.isEnabldJobsCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][5].ToString());
            }
            this.correctJobsNavLbls(dtst);
            this.obey_jobs_evnts = true;
        }

        private void correctJobsNavLbls(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.jobs_cur_indx == 0 && totlRecs == 0)
            {
                this.is_last_job = true;
                this.totl_jobs = 0;
                this.last_job_num = 0;
                this.jobs_cur_indx = 0;
                this.updtJobsTotals();
                this.updtJobsNavLabels();
            }
            else if (this.totl_jobs == Global.mnFrm.cmCde.Big_Val
      && totlRecs < long.Parse(this.dsplySizeJobsComboBox.Text))
            {
                this.totl_jobs = this.last_job_num;
                if (totlRecs == 0)
                {
                    this.jobs_cur_indx -= 1;
                    this.updtJobsTotals();
                    this.populateJobsLstView();
                }
                else
                {
                    this.updtJobsTotals();
                }
            }
        }

        private void clearJobsInfo()
        {
            this.obey_jobs_evnts = false;
            this.saveJobsButton.Enabled = false;
            this.addJobsButton.Enabled = this.addJobs;
            this.editJobsButton.Enabled = this.editJobs;
            this.jobIDTextBox.Text = "-1";
            this.jobNameTextBox.Text = "";
            this.parentJobIDTextBox.Text = "-1";
            this.parentJobTextBox.Text = "";
            this.jobDescTextBox.Text = "";
            this.isEnabldJobsCheckBox.Checked = false;
            this.obey_jobs_evnts = true;
        }

        private void prpareForJobsEdit()
        {
            this.saveJobsButton.Enabled = true;
            this.jobNameTextBox.ReadOnly = false;
            this.jobNameTextBox.BackColor = Color.FromArgb(255, 255, 118);
            this.jobDescTextBox.ReadOnly = false;
            this.jobDescTextBox.BackColor = Color.White;
            this.parentJobTextBox.ReadOnly = false;
            this.parentJobTextBox.BackColor = Color.White;
        }

        private void disableJobsEdit()
        {
            this.addJob = false;
            this.editJob = false;
            this.jobNameTextBox.ReadOnly = true;
            this.jobNameTextBox.BackColor = Color.WhiteSmoke;
            this.jobIDTextBox.ReadOnly = true;
            this.jobIDTextBox.BackColor = Color.WhiteSmoke;
            this.parentJobIDTextBox.ReadOnly = true;
            this.parentJobIDTextBox.BackColor = Color.WhiteSmoke;
            this.parentJobTextBox.ReadOnly = true;
            this.parentJobTextBox.BackColor = Color.WhiteSmoke;
            this.jobDescTextBox.ReadOnly = true;
            this.jobDescTextBox.BackColor = Color.WhiteSmoke;
        }

        private bool shdObeyJobsEvts()
        {
            return this.obey_jobs_evnts;
        }

        private void JobsPnlNavButtons(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecsJobsLabel.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.is_last_job = false;
                this.jobs_cur_indx = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.is_last_job = false;
                this.jobs_cur_indx -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.is_last_job = false;
                this.jobs_cur_indx += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.is_last_job = true;
                this.totl_jobs = Global.get_Total_Job(this.searchForJobsTextBox.Text,
                    this.searchInJobsComboBox.Text, Global.mnFrm.cmCde.Org_id);
                this.updtJobsTotals();
                this.jobs_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            this.getJobsPnlData();
        }

        private void parentJobButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[23]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                return;
            }
            string[] selVals = new string[1];
            selVals[0] = this.parentJobIDTextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Jobs"), ref selVals, true,
                false, Global.mnFrm.cmCde.Org_id,
             this.srchWrd, "Both", true);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.parentJobIDTextBox.Text = selVals[i];
                    this.parentJobTextBox.Text = Global.mnFrm.cmCde.getJobName(int.Parse(selVals[i]));
                }
            }
            if (int.Parse(this.jobIDTextBox.Text) > 0)
            {
                Global.updtJobPrntID(int.Parse(this.jobIDTextBox.Text), int.Parse(this.parentJobIDTextBox.Text));
            }
        }

        private void addJobsButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[22]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                return;
            }
            this.clearJobsInfo();
            this.addJob = true;
            this.editJob = false;
            this.prpareForJobsEdit();
            this.addJobsButton.Enabled = false;
            this.editJobsButton.Enabled = false;
        }

        private void editJobsButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[23]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.jobIDTextBox.Text == "" || this.jobIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("No record to Edit!", 0);
                return;
            }
            this.addJob = false;
            this.editJob = true;
            this.prpareForJobsEdit();
            this.addJobsButton.Enabled = false;
            this.editJobsButton.Enabled = false;
        }

        private void saveJobsButton_Click(object sender, EventArgs e)
        {
            if (this.addJob == true)
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
            if (this.jobNameTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please enter a Job name!", 0);
                return;
            }
            long oldJobID = Global.mnFrm.cmCde.getJobID(this.jobNameTextBox.Text,
                Global.mnFrm.cmCde.Org_id);
            if (oldJobID > 0
             && this.addJob == true)
            {
                Global.mnFrm.cmCde.showMsg("Job's Name is already in use in this Organisation!", 0);
                return;
            }
            if (oldJobID > 0
             && this.editJob == true
             && oldJobID.ToString() != this.jobIDTextBox.Text)
            {
                Global.mnFrm.cmCde.showMsg("New Job's Name is already in use in this Organisation!", 0);
                return;
            }

            if (this.addJob == true)
            {
                Global.createJob(Global.mnFrm.cmCde.Org_id, this.jobNameTextBox.Text,
                    int.Parse(this.parentJobIDTextBox.Text),
                    this.jobDescTextBox.Text, this.isEnabldJobsCheckBox.Checked);

                this.saveJobsButton.Enabled = false;
                this.addJob = false;
                this.editJob = false;
                this.editJobsButton.Enabled = this.editJobs;
                this.addJobsButton.Enabled = this.addJobs;
                System.Windows.Forms.Application.DoEvents();
                this.loadJobsPanel();
            }
            else if (this.editJob == true)
            {
                Global.updateJob(int.Parse(this.jobIDTextBox.Text), this.jobNameTextBox.Text,
                    int.Parse(this.parentJobIDTextBox.Text), this.jobDescTextBox.Text,
                    this.isEnabldJobsCheckBox.Checked);

                this.saveJobsButton.Enabled = false;
                this.editJob = false;
                this.editJobsButton.Enabled = this.editJobs;
                this.addJobsButton.Enabled = this.addJobs;
                this.loadJobsPanel();
            }
        }

        private void vwJobsExtraInfoButton_Click(object sender, EventArgs e)
        {
            if (this.jobIDTextBox.Text == ""
                || this.jobIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("No record to View!", 0);
                return;
            }
            DialogResult dgres = this.cmCde.showRowsExtInfDiag(this.cmCde.getMdlGrpID("Jobs"),
                long.Parse(this.jobIDTextBox.Text), "org.org_all_other_info_table",
                this.jobNameTextBox.Text, this.editJobs, 12, 13,
                "org.org_all_other_info_table_dflt_row_id_seq");
            if (dgres == DialogResult.OK)
            {
            }
        }

        private void jobListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.shdObeyJobsEvts() == false)
            {
                return;
            }
            if (this.jobListView.SelectedItems.Count > 0)
            {
                this.populateJobs(int.Parse(this.jobListView.SelectedItems[0].SubItems[2].Text));
            }
            else
            {
                this.populateJobs(-100000);
            }
        }

        private void goJobsButton_Click(object sender, EventArgs e)
        {
            this.loadJobsPanel();
        }

        private void exptJobMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.jobListView);
        }

        private void exprtJobsButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtJobsTmp();
        }

        private void imprtJobsButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[22]) == false)
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
                Global.mnFrm.cmCde.imprtJobsTmp(this.openFileDialog1.FileName);
            }
            this.populateJobsLstView();
        }

        private void searchForJobsTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.goJobsButton_Click(this.goJobsButton, ex);
            }
        }

        private void positionJobsTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                this.JobsPnlNavButtons(this.movePreviousJobsButton, ex);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
            {
                this.JobsPnlNavButtons(this.moveNextJobsButton, ex);
            }
        }

        private void delJobButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[24]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }

            if (this.jobIDTextBox.Text == "" || this.jobIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select the Job to DELETE!", 0);
                return;
            }
            if (Global.isJobInUse(int.Parse(this.jobIDTextBox.Text)) == true)
            {
                Global.mnFrm.cmCde.showMsg("This Job has been assigned to Persons hence cannot be DELETED!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Job?" +
       "\r\nThis action cannot be undone!", 1) == DialogResult.No)
            {
                Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            Global.deleteJob(int.Parse(this.jobIDTextBox.Text), this.jobNameTextBox.Text);
            this.loadJobsPanel();
        }

        private void vwSQLJobsButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(this.jobs_SQL, 12);
        }

        private void recHstryJobButton_Click(object sender, EventArgs e)
        {
            if (this.jobIDTextBox.Text == "-1"
      || this.jobIDTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(Global.get_Job_Rec_Hstry(int.Parse(this.jobIDTextBox.Text)), 13);
        }

        private void addJobMenuItem_Click(object sender, EventArgs e)
        {
            this.addJobsButton_Click(this.addJobsButton, e);
        }

        private void delJobMenuItem_Click(object sender, EventArgs e)
        {
            this.delJobButton_Click(this.delJobButton, e);
        }

        private void editJobMenuItem_Click(object sender, EventArgs e)
        {
            this.editJobsButton_Click(this.editJobsButton, e);
        }

        private void rcHstryJobMenuItem_Click(object sender, EventArgs e)
        {
            this.recHstryJobButton_Click(this.recHstryJobButton, e);
        }

        private void rfrshJobMenuItem_Click(object sender, EventArgs e)
        {
            this.goJobsButton_Click(this.goJobsButton, e);
        }

        private void vwSQLJobMenuItem_Click(object sender, EventArgs e)
        {
            this.vwSQLJobsButton_Click(this.vwSQLJobsButton, e);
        }

        private void isEnabldJobsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shdObeyJobsEvts() == false
                       || beenToCheckBx == true)
            {
                beenToCheckBx = false;
                return;
            }
            beenToCheckBx = true;
            if (this.addJob == false && this.editJob == false)
            {
                this.isEnabldJobsCheckBox.Checked = !this.isEnabldJobsCheckBox.Checked;
            }
        }
        #endregion

        #region "Grades..."
        private void loadGradesPanel()
        {
            this.obey_grd_evnts = false;
            if (this.searchInGrdComboBox.SelectedIndex < 0)
            {
                this.searchInGrdComboBox.SelectedIndex = 0;
            }
            if (this.searchForGrdTextBox.Text.Contains("%") == false)
            {
                this.searchForGrdTextBox.Text = "%" + this.searchForGrdTextBox.Text.Replace(" ", "%") + "%";
            }
            if (this.searchForGrdTextBox.Text == "%%")
            {
                this.searchForGrdTextBox.Text = "%";
            }
            int dsply = 0;
            if (this.dsplySizeGrdComboBox.Text == ""
                || int.TryParse(this.dsplySizeGrdComboBox.Text, out dsply) == false)
            {
                this.dsplySizeGrdComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            //this.groupBox8.Height = this.g.Bottom - this.toolStrip4.Bottom - 50;
            this.is_last_grd = false;
            this.totl_grd = Global.mnFrm.cmCde.Big_Val;
            this.getGrdsPnlData();
            this.obey_grd_evnts = true;
        }

        private void getGrdsPnlData()
        {
            this.updtGrdsTotals();
            this.populateGrdsListVw();
            this.updtGrdNavLabels();
        }

        private void updtGrdsTotals()
        {
            Global.mnFrm.cmCde.navFuncts.FindNavigationIndices(
                long.Parse(this.dsplySizeGrdComboBox.Text), this.totl_grd);
            if (this.grd_cur_indx >= Global.mnFrm.cmCde.navFuncts.totalGroups)
            {
                this.grd_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            if (this.grd_cur_indx < 0)
            {
                this.grd_cur_indx = 0;
            }
            Global.mnFrm.cmCde.navFuncts.currentNavigationIndex = this.grd_cur_indx;
        }

        private void updtGrdNavLabels()
        {
            this.moveFirstGrdButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveFirstBtnStatus();
            this.movePreviousGrdButton.Enabled = Global.mnFrm.cmCde.navFuncts.movePrevBtnStatus();
            this.moveNextGrdButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveNextBtnStatus();
            this.moveLastGrdButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveLastBtnStatus();
            this.positionGrdTextBox.Text = Global.mnFrm.cmCde.navFuncts.displayedRecordsNumbers();
            if (this.is_last_grd == true ||
                this.totl_grd != Global.mnFrm.cmCde.Big_Val)
            {
                this.totalRecsGrdLabel.Text = Global.mnFrm.cmCde.navFuncts.totalRecordsLabel();
            }
            else
            {
                this.totalRecsGrdLabel.Text = "of Total";
            }
        }

        private void populateGrdsListVw()
        {
            this.obey_grd_evnts = false;
            this.gradesListView.Items.Clear();
            DataSet dtst = Global.get_Basic_Grade(this.searchForGrdTextBox.Text,
                this.searchInGrdComboBox.Text, this.grd_cur_indx,
                int.Parse(this.dsplySizeGrdComboBox.Text), Global.mnFrm.cmCde.Org_id);

            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.last_grd_num = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
                ListViewItem nwItem = new ListViewItem(new string[] {
    (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),
    dtst.Tables[0].Rows[i][0].ToString()});
                this.gradesListView.Items.Add(nwItem);
            }
            this.correctGrdsNavLbls(dtst);
            if (this.gradesListView.Items.Count > 0)
            {
                this.obey_grd_evnts = true;
                this.gradesListView.Items[0].Selected = true;
            }
            else
            {
                this.populateGrdDet(-100000);
            }
            this.obey_grd_evnts = true;
        }

        private void populateGrdDet(int grdID)
        {
            this.clearGrdsInfo();
            this.disableGrdsEdit();
            this.obey_grd_evnts = false;
            DataSet dtst = Global.get_One_Grade_Det(grdID);
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.gradeIDTextBox.Text = dtst.Tables[0].Rows[i][0].ToString();
                this.gradeNameTextBox.Text = dtst.Tables[0].Rows[i][1].ToString();
                this.parntGradeIDTextBox.Text = dtst.Tables[0].Rows[i][4].ToString();
                this.parntGradeTextBox.Text = Global.mnFrm.cmCde.getGrdName(int.Parse(dtst.Tables[0].Rows[i][4].ToString()));
                this.gradeCommentsTextBox.Text = dtst.Tables[0].Rows[i][2].ToString();
                this.isEnabledGradeCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][3].ToString());
            }
            this.obey_grd_evnts = true;
        }

        private void correctGrdsNavLbls(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.grd_cur_indx == 0 && totlRecs == 0)
            {
                this.is_last_grd = true;
                this.totl_grd = 0;
                this.last_grd_num = 0;
                this.grd_cur_indx = 0;
                this.updtGrdsTotals();
                this.updtGrdNavLabels();
            }
            else if (this.totl_grd == Global.mnFrm.cmCde.Big_Val
      && totlRecs < long.Parse(this.dsplySizeGrdComboBox.Text))
            {
                this.totl_grd = this.last_grd_num;
                if (totlRecs == 0)
                {
                    this.grd_cur_indx -= 1;
                    this.updtGrdsTotals();
                    this.populateGrdsListVw();
                }
                else
                {
                    this.updtGrdsTotals();
                }
            }
        }

        private void clearGrdsInfo()
        {
            this.obey_grd_evnts = false;
            this.saveGrdButton.Enabled = false;
            this.addGrdButton.Enabled = this.addgrds;
            this.editGrdButton.Enabled = this.editgrds;
            this.gradeIDTextBox.Text = "-1";
            this.gradeNameTextBox.Text = "";
            this.parntGradeIDTextBox.Text = "-1";
            this.parntGradeTextBox.Text = "";
            this.gradeCommentsTextBox.Text = "";
            this.isEnabledGradeCheckBox.Checked = false;
            this.obey_grd_evnts = true;
        }

        private void prpareForGrdsEdit()
        {
            this.saveGrdButton.Enabled = true;
            this.gradeNameTextBox.ReadOnly = false;
            this.gradeNameTextBox.BackColor = Color.FromArgb(255, 255, 118);
            this.gradeCommentsTextBox.ReadOnly = false;
            this.gradeCommentsTextBox.BackColor = Color.White;

            this.parntGradeTextBox.ReadOnly = false;
            this.parntGradeTextBox.BackColor = Color.White;
        }

        private void disableGrdsEdit()
        {
            this.addgrd = false;
            this.editgrd = false;
            this.gradeNameTextBox.ReadOnly = true;
            this.gradeNameTextBox.BackColor = Color.WhiteSmoke;
            this.gradeIDTextBox.ReadOnly = true;
            this.gradeIDTextBox.BackColor = Color.WhiteSmoke;
            this.parntGradeIDTextBox.ReadOnly = true;
            this.parntGradeIDTextBox.BackColor = Color.WhiteSmoke;
            this.parntGradeTextBox.ReadOnly = true;
            this.parntGradeTextBox.BackColor = Color.WhiteSmoke;
            this.gradeCommentsTextBox.ReadOnly = true;
            this.gradeCommentsTextBox.BackColor = Color.WhiteSmoke;
        }

        private bool shdObeyGrdsEvts()
        {
            return this.obey_grd_evnts;
        }

        private void GrdsPnlNavButtons(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecsGrdLabel.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.is_last_grd = false;
                this.grd_cur_indx = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.is_last_grd = false;
                this.grd_cur_indx -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.is_last_grd = false;
                this.grd_cur_indx += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.is_last_grd = true;
                this.totl_grd = Global.get_Total_Grades(this.searchForGrdTextBox.Text,
                    this.searchInGrdComboBox.Text, Global.mnFrm.cmCde.Org_id);
                this.updtGrdsTotals();
                this.grd_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            this.getGrdsPnlData();
        }

        private void goGrdButton_Click(object sender, EventArgs e)
        {
            this.loadGradesPanel();
        }

        private void addGrdButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[25]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                return;
            }
            this.clearGrdsInfo();
            this.addgrd = true;
            this.editgrd = false;
            this.prpareForGrdsEdit();
            this.addGrdButton.Enabled = false;
            this.editGrdButton.Enabled = false;
        }

        private void editGrdButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[26]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.gradeIDTextBox.Text == "" || this.gradeIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("No record to Edit!", 0);
                return;
            }
            this.addgrd = false;
            this.editgrd = true;
            this.prpareForGrdsEdit();
            this.addGrdButton.Enabled = false;
            this.editGrdButton.Enabled = false;
        }

        private void saveGrdButton_Click(object sender, EventArgs e)
        {
            if (this.addgrd == true)
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
            if (this.gradeNameTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please enter a Grade name!", 0);
                return;
            }

            long oldGrdID = Global.mnFrm.cmCde.getGrdID(this.gradeNameTextBox.Text,
                Global.mnFrm.cmCde.Org_id);
            if (oldGrdID > 0
             && this.addgrd == true)
            {
                Global.mnFrm.cmCde.showMsg("Grade's Name is already in use in this Organisation!", 0);
                return;
            }
            if (oldGrdID > 0
             && this.editgrd == true
             && oldGrdID.ToString() != this.gradeIDTextBox.Text)
            {
                Global.mnFrm.cmCde.showMsg("New Grade's Name is already in use in this Organisation!", 0);
                return;
            }

            if (this.addgrd == true)
            {
                Global.createGrd(Global.mnFrm.cmCde.Org_id, this.gradeNameTextBox.Text,
                    int.Parse(this.parntGradeIDTextBox.Text),
                    this.gradeCommentsTextBox.Text, this.isEnabledGradeCheckBox.Checked);

                this.saveGrdButton.Enabled = false;
                this.addgrd = false;
                this.editgrd = false;
                this.editGrdButton.Enabled = this.addgrds;
                this.addGrdButton.Enabled = this.editgrds;
                System.Windows.Forms.Application.DoEvents();
                this.loadGradesPanel();
            }
            else if (this.editgrd == true)
            {
                Global.updateGrd(int.Parse(this.gradeIDTextBox.Text), this.gradeNameTextBox.Text,
                    int.Parse(this.parntGradeIDTextBox.Text), this.gradeCommentsTextBox.Text,
                    this.isEnabledGradeCheckBox.Checked);

                this.saveGrdButton.Enabled = false;
                this.editgrd = false;
                this.editGrdButton.Enabled = this.addgrds;
                this.addGrdButton.Enabled = this.editgrds;
                this.loadGradesPanel();
            }
        }

        private void parntGradeButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[26]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            string[] selVals = new string[1];
            selVals[0] = this.parntGradeIDTextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Grades"), ref selVals, true, false, Global.mnFrm.cmCde.Org_id,
             this.srchWrd, "Both", true);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.parntGradeIDTextBox.Text = selVals[i];
                    this.parntGradeTextBox.Text = Global.mnFrm.cmCde.getGrdName(int.Parse(selVals[i]));
                }
            }
            if (int.Parse(this.gradeIDTextBox.Text) > 0)
            {
                Global.updtGrdPrntID(int.Parse(this.gradeIDTextBox.Text), int.Parse(this.parntGradeIDTextBox.Text));
            }
        }

        private void gradesListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.shdObeyGrdsEvts() == false)
            {
                return;
            }
            if (this.gradesListView.SelectedItems.Count > 0)
            {
                this.populateGrdDet(int.Parse(this.gradesListView.SelectedItems[0].SubItems[2].Text));
            }
            else
            {
                this.populateGrdDet(-100000);
            }
        }

        private void otherInfoGradeButton_Click(object sender, EventArgs e)
        {
            if (this.gradeIDTextBox.Text == ""
                || this.gradeIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("No record to View!", 0);
                return;
            }
            DialogResult dgres = this.cmCde.showRowsExtInfDiag(this.cmCde.getMdlGrpID("Grades"),
                long.Parse(this.gradeIDTextBox.Text), "org.org_all_other_info_table",
                this.gradeNameTextBox.Text, this.editgrds, 12, 13,
                "org.org_all_other_info_table_dflt_row_id_seq");
            if (dgres == DialogResult.OK)
            {
            }
        }

        private void exptGradesMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.gradesListView);
        }

        private void exptGradesButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtGradesTmp();
        }

        private void imprtGradesButton_Click(object sender, EventArgs e)
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
                Global.mnFrm.cmCde.imprtGradesTmp(this.openFileDialog1.FileName);
            }
            this.populateGrdsListVw();
        }

        private void searchForGrdTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.goGrdButton_Click(this.goGrdButton, ex);
            }
        }

        private void positionGrdTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                this.GrdsPnlNavButtons(this.movePreviousGrdButton, ex);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
            {
                this.GrdsPnlNavButtons(this.moveNextGrdButton, ex);
            }
        }

        private void delGrdButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[27]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.gradeIDTextBox.Text == "" || this.gradeIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select the Grade to DELETE!", 0);
                return;
            }
            if (Global.isGrdInUse(int.Parse(this.gradeIDTextBox.Text)) == true)
            {
                Global.mnFrm.cmCde.showMsg("This Grade has been assigned to Persons hence cannot be DELETED!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Grade?" +
       "\r\nThis action cannot be undone!", 1) == DialogResult.No)
            {
                Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            Global.deleteGrd(int.Parse(this.gradeIDTextBox.Text), this.gradeNameTextBox.Text);
            this.loadGradesPanel();
        }

        private void vwSQLGrdButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(this.grd_SQL, 12);
        }

        private void rcHstryGrdButton_Click(object sender, EventArgs e)
        {
            if (this.gradeIDTextBox.Text == "-1"
      || this.gradeIDTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(Global.get_Grd_Rec_Hstry(int.Parse(this.gradeIDTextBox.Text)), 13);
        }

        private void editGradesMenuItem_Click(object sender, EventArgs e)
        {
            this.editGrdButton_Click(this.editGrdButton, e);
        }

        private void addGradesMenuItem_Click(object sender, EventArgs e)
        {
            this.addGrdButton_Click(this.addGrdButton, e);
        }

        private void delGradesMenuItem_Click(object sender, EventArgs e)
        {
            this.delGrdButton_Click(this.delGrdButton, e);
        }

        private void rfrshGradesMenuItem_Click(object sender, EventArgs e)
        {
            this.goGrdButton_Click(this.goGrdButton, e);
        }

        private void rcHstryGradesMenuItem_Click(object sender, EventArgs e)
        {
            this.rcHstryGrdButton_Click(this.rcHstryGrdButton, e);
        }

        private void vwSQLGradesMenuItem_Click(object sender, EventArgs e)
        {
            this.vwSQLGrdButton_Click(this.vwSQLGrdButton, e);
        }

        private void isEnabledGradeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shdObeyGrdsEvts() == false
                     || beenToCheckBx == true)
            {
                beenToCheckBx = false;
                return;
            }
            beenToCheckBx = true;
            if (this.addgrd == false && this.editgrd == false)
            {
                this.isEnabledGradeCheckBox.Checked = !this.isEnabledGradeCheckBox.Checked;
            }
        }
        #endregion

        #region "Positions..."
        private void loadPositionPanel()
        {
            this.obey_pos_evnts = false;
            if (this.searchInPosComboBox.SelectedIndex < 0)
            {
                this.searchInPosComboBox.SelectedIndex = 0;
            }
            if (this.searchForPosTextBox.Text.Contains("%") == false)
            {
                this.searchForPosTextBox.Text = "%" + this.searchForPosTextBox.Text.Replace(" ", "%") + "%";
            }
            if (this.searchForPosTextBox.Text == "%%")
            {
                this.searchForPosTextBox.Text = "%";
            }
            int dsply = 0;
            if (this.dsplySizePosComboBox.Text == ""
                || int.TryParse(this.dsplySizePosComboBox.Text, out dsply) == false)
            {
                this.dsplySizePosComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            //this.groupBox8.Height = this.g.Bottom - this.toolStrip4.Bottom - 50;
            this.is_last_pos = false;
            this.totl_pos = Global.mnFrm.cmCde.Big_Val;
            this.getPosPnlData();
            this.obey_pos_evnts = true;
        }

        private void getPosPnlData()
        {
            this.updtPosTotals();
            this.populatePosListVw();
            this.updtPosNavLabels();
        }

        private void updtPosTotals()
        {
            Global.mnFrm.cmCde.navFuncts.FindNavigationIndices(
                long.Parse(this.dsplySizePosComboBox.Text), this.totl_pos);
            if (this.pos_cur_indx >= Global.mnFrm.cmCde.navFuncts.totalGroups)
            {
                this.pos_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            if (this.pos_cur_indx < 0)
            {
                this.pos_cur_indx = 0;
            }
            Global.mnFrm.cmCde.navFuncts.currentNavigationIndex = this.pos_cur_indx;
        }

        private void updtPosNavLabels()
        {
            this.moveFirstPosButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveFirstBtnStatus();
            this.movePreviousPosButton.Enabled = Global.mnFrm.cmCde.navFuncts.movePrevBtnStatus();
            this.moveNextPosButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveNextBtnStatus();
            this.moveLastPosButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveLastBtnStatus();
            this.positionPosTextBox.Text = Global.mnFrm.cmCde.navFuncts.displayedRecordsNumbers();
            if (this.is_last_pos == true ||
                this.totl_pos != Global.mnFrm.cmCde.Big_Val)
            {
                this.totalRecsPosLabel.Text = Global.mnFrm.cmCde.navFuncts.totalRecordsLabel();
            }
            else
            {
                this.totalRecsPosLabel.Text = "of Total";
            }
        }

        private void populatePosListVw()
        {
            this.obey_pos_evnts = false;
            DataSet dtst = Global.get_Basic_Pos(this.searchForPosTextBox.Text,
                this.searchInPosComboBox.Text, this.pos_cur_indx,
                int.Parse(this.dsplySizePosComboBox.Text), Global.mnFrm.cmCde.Org_id);
            this.positionListView.Items.Clear();

            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.last_pos_num = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
                ListViewItem nwItem = new ListViewItem(new string[] {
    (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),
    dtst.Tables[0].Rows[i][0].ToString()});
                this.positionListView.Items.Add(nwItem);
            }
            this.correctPosNavLbls(dtst);
            if (this.positionListView.Items.Count > 0)
            {
                this.obey_pos_evnts = true;
                this.positionListView.Items[0].Selected = true;
            }
            else
            {
                this.populatePosDet(-100000);
            }
            this.obey_pos_evnts = true;
        }

        private void populatePosDet(int posID)
        {
            this.clearPosInfo();
            this.disablePosEdit();
            this.obey_pos_evnts = false;
            DataSet dtst = Global.get_One_Pos_Det(posID);
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.positionIDTextBox.Text = dtst.Tables[0].Rows[i][0].ToString();
                this.positionNameTextBox.Text = dtst.Tables[0].Rows[i][1].ToString();
                this.parntPositionIDTextBox.Text = dtst.Tables[0].Rows[i][4].ToString();
                this.parntPositionTextBox.Text = Global.mnFrm.cmCde.getPosName(int.Parse(dtst.Tables[0].Rows[i][4].ToString()));
                this.positionDescTextBox.Text = dtst.Tables[0].Rows[i][2].ToString();
                this.isEnabledPosCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][3].ToString());
            }
            this.obey_pos_evnts = true;
        }

        private void correctPosNavLbls(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.pos_cur_indx == 0 && totlRecs == 0)
            {
                this.is_last_pos = true;
                this.totl_pos = 0;
                this.last_pos_num = 0;
                this.pos_cur_indx = 0;
                this.updtPosTotals();
                this.updtPosNavLabels();
            }
            else if (this.totl_pos == Global.mnFrm.cmCde.Big_Val
      && totlRecs < long.Parse(this.dsplySizePosComboBox.Text))
            {
                this.totl_pos = this.last_pos_num;
                if (totlRecs == 0)
                {
                    this.pos_cur_indx -= 1;
                    this.updtPosTotals();
                    this.populatePosListVw();
                }
                else
                {
                    this.updtPosTotals();
                }
            }
        }

        private void clearPosInfo()
        {
            this.obey_pos_evnts = false;
            this.savePosButton.Enabled = false;
            this.addPosButton.Enabled = this.addposs;
            this.editPosButton.Enabled = this.editposs;
            this.positionIDTextBox.Text = "-1";
            this.positionNameTextBox.Text = "";
            this.parntPositionIDTextBox.Text = "-1";
            this.parntPositionTextBox.Text = "";
            this.positionDescTextBox.Text = "";
            this.isEnabledPosCheckBox.Checked = false;
            this.obey_pos_evnts = true;
        }

        private void prpareForPosEdit()
        {
            this.savePosButton.Enabled = true;
            this.positionNameTextBox.ReadOnly = false;
            this.positionNameTextBox.BackColor = Color.FromArgb(255, 255, 118);
            this.positionDescTextBox.ReadOnly = false;
            this.positionDescTextBox.BackColor = Color.White;

            this.parntPositionTextBox.ReadOnly = false;
            this.parntPositionTextBox.BackColor = Color.White;
        }

        private void disablePosEdit()
        {
            this.addpos = false;
            this.editpos = false;
            this.positionNameTextBox.ReadOnly = true;
            this.positionNameTextBox.BackColor = Color.WhiteSmoke;
            this.positionIDTextBox.ReadOnly = true;
            this.positionIDTextBox.BackColor = Color.WhiteSmoke;
            this.parntPositionIDTextBox.ReadOnly = true;
            this.parntPositionIDTextBox.BackColor = Color.WhiteSmoke;
            this.parntPositionTextBox.ReadOnly = true;
            this.parntPositionTextBox.BackColor = Color.WhiteSmoke;
            this.positionDescTextBox.ReadOnly = true;
            this.positionDescTextBox.BackColor = Color.WhiteSmoke;
        }

        private bool shdObeyPosEvts()
        {
            return this.obey_pos_evnts;
        }

        private void PosPnlNavButtons(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecsPosLabel.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.is_last_pos = false;
                this.pos_cur_indx = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.is_last_pos = false;
                this.pos_cur_indx -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.is_last_pos = false;
                this.pos_cur_indx += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.is_last_pos = true;
                this.totl_pos = Global.get_Total_Pos(this.searchForPosTextBox.Text,
                    this.searchInPosComboBox.Text, Global.mnFrm.cmCde.Org_id);
                this.updtPosTotals();
                this.pos_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
            }
            this.getPosPnlData();
        }

        private void goPosButton_Click(object sender, EventArgs e)
        {
            this.loadPositionPanel();
        }

        private void addPosButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[28]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                return;
            }
            this.clearPosInfo();
            this.addpos = true;
            this.editpos = false;
            this.prpareForPosEdit();
            this.addPosButton.Enabled = false;
            this.editPosButton.Enabled = false;
        }

        private void editPosButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[29]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.positionIDTextBox.Text == "" || this.positionIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("No record to Edit!", 0);
                return;
            }
            this.prpareForPosEdit();
            this.addPosButton.Enabled = false;
            this.editPosButton.Enabled = false;
            this.addpos = false;
            this.editpos = true;
        }

        private void savePosButton_Click(object sender, EventArgs e)
        {
            if (this.addpos == true)
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[28]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                     " this action!\nContact your System Administrator!", 0);
                    return;
                }
            }
            else
            {
                if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[29]) == false)
                {
                    Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                     " this action!\nContact your System Administrator!", 0);
                    return;
                }
            }
            if (this.positionNameTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please enter a Position name!", 0);
                return;
            }

            long oldPosID = Global.mnFrm.cmCde.getPosID(this.positionNameTextBox.Text,
                Global.mnFrm.cmCde.Org_id);
            if (oldPosID > 0
             && this.addpos == true)
            {
                Global.mnFrm.cmCde.showMsg("Position Name is already in use in this Organisation!", 0);
                return;
            }
            if (oldPosID > 0
             && this.editpos == true
             && oldPosID.ToString() !=
             this.positionIDTextBox.Text)
            {
                Global.mnFrm.cmCde.showMsg("New Position Name is already in use in this Organisation!", 0);
                return;
            }

            if (this.addpos == true)
            {
                Global.createPos(Global.mnFrm.cmCde.Org_id, this.positionNameTextBox.Text,
                    int.Parse(this.parntPositionIDTextBox.Text),
                    this.positionDescTextBox.Text, this.isEnabledPosCheckBox.Checked);

                this.savePosButton.Enabled = false;
                this.addpos = false;
                this.editpos = false;
                this.editPosButton.Enabled = this.addposs;
                this.addPosButton.Enabled = this.editposs;
                System.Windows.Forms.Application.DoEvents();
                this.loadPositionPanel();
            }
            else if (this.editpos == true)
            {
                Global.updatePos(int.Parse(this.positionIDTextBox.Text), this.positionNameTextBox.Text,
                    int.Parse(this.parntPositionIDTextBox.Text), this.positionDescTextBox.Text,
                    this.isEnabledPosCheckBox.Checked);

                this.savePosButton.Enabled = false;
                this.editpos = false;
                this.editPosButton.Enabled = this.addposs;
                this.addPosButton.Enabled = this.editposs;
                this.loadPositionPanel();
            }
        }

        private void positionListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.shdObeyPosEvts() == false)
            {
                return;
            }
            if (this.positionListView.SelectedItems.Count > 0)
            {
                this.populatePosDet(int.Parse(this.positionListView.SelectedItems[0].SubItems[2].Text));
            }
            else
            {
                this.populatePosDet(-100000);
            }
        }

        private void parntPositionButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[29]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                    " this action!\nContact your System Administrator!", 0);
                return;
            }
            string[] selVals = new string[1];
            selVals[0] = this.parntPositionIDTextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Positions"), ref selVals, true, false, Global.mnFrm.cmCde.Org_id,
             this.srchWrd, "Both", true);
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.parntPositionIDTextBox.Text = selVals[i];
                    this.parntPositionTextBox.Text = Global.mnFrm.cmCde.getPosName(int.Parse(selVals[i]));
                }
            }
            if (int.Parse(this.positionIDTextBox.Text) > 0)
            {
                Global.updtPosPrntID(int.Parse(this.positionIDTextBox.Text), int.Parse(this.parntPositionIDTextBox.Text));
            }
        }

        private void otherInfoPosButton_Click(object sender, EventArgs e)
        {
            if (this.positionIDTextBox.Text == ""
                || this.positionIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("No record to View!", 0);
                return;
            }
            DialogResult dgres = this.cmCde.showRowsExtInfDiag(this.cmCde.getMdlGrpID("Positions"),
                long.Parse(this.positionIDTextBox.Text), "org.org_all_other_info_table",
                this.positionNameTextBox.Text, this.editposs, 12, 13,
                "org.org_all_other_info_table_dflt_row_id_seq");
            if (dgres == DialogResult.OK)
            {
            }
        }

        private void exptPosMenuItem_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtToExcel(this.positionListView);
        }

        private void exprtPosButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.exprtPosTmp();
        }

        private void imprtPosButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[29]) == false)
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
                Global.mnFrm.cmCde.imprtPosTmp(this.openFileDialog1.FileName);
            }
            this.populatePosListVw();
        }

        private void searchForPosTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.goPosButton_Click(this.goPosButton, ex);
            }
        }

        private void positionPosTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                this.PosPnlNavButtons(this.movePreviousPosButton, ex);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
            {
                this.PosPnlNavButtons(this.moveNextPosButton, ex);
            }
        }

        private void deletePosButton_Click(object sender, EventArgs e)
        {
            if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[30]) == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }

            if (this.positionIDTextBox.Text == "" || this.positionIDTextBox.Text == "-1")
            {
                Global.mnFrm.cmCde.showMsg("Please select the Position to DELETE!", 0);
                return;
            }
            if (Global.isPosInUse(int.Parse(this.positionIDTextBox.Text)) == true)
            {
                Global.mnFrm.cmCde.showMsg("This Position has been assigned to Persons hence cannot be DELETED!", 0);
                return;
            }
            if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Position?" +
       "\r\nThis action cannot be undone!", 1) == DialogResult.No)
            {
                Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
                return;
            }
            Global.deletePos(int.Parse(this.positionIDTextBox.Text), this.positionNameTextBox.Text);
            this.loadPositionPanel();
        }

        private void vwSQLPosButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(this.pos_SQL, 12);
        }

        private void recHstryPosButton_Click(object sender, EventArgs e)
        {
            if (this.positionIDTextBox.Text == "-1"
      || this.positionIDTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(Global.get_Pos_Rec_Hstry(int.Parse(this.positionIDTextBox.Text)), 13);
        }

        private void addPosMenuItem_Click(object sender, EventArgs e)
        {
            this.addPosButton_Click(this.addPosButton, e);
        }

        private void editPosMenuItem_Click(object sender, EventArgs e)
        {
            this.editPosButton_Click(this.editPosButton, e);
        }

        private void delPosMenuItem_Click(object sender, EventArgs e)
        {
            this.deletePosButton_Click(this.deletePosButton, e);
        }

        private void rfrshPosMenuItem_Click(object sender, EventArgs e)
        {
            this.goPosButton_Click(this.goPosButton, e);
        }

        private void rcHstryPosMenuItem_Click(object sender, EventArgs e)
        {
            this.recHstryPosButton_Click(this.recHstryPosButton, e);
        }

        private void vwSQLPosMenuItem_Click(object sender, EventArgs e)
        {
            this.vwSQLPosButton_Click(this.vwSQLPosButton, e);
        }

        private void isEnabledPosCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shdObeyPosEvts() == false
                     || beenToCheckBx == true)
            {
                beenToCheckBx = false;
                return;
            }
            beenToCheckBx = true;
            if (this.addpos == false && this.editpos == false)
            {
                this.isEnabledPosCheckBox.Checked = !this.isEnabledPosCheckBox.Checked;
            }
        }
        #endregion

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

        private void divListView_KeyDown(object sender, KeyEventArgs e)
        {
            Global.mnFrm.cmCde.listViewKeyDown(this.divListView, e);
        }

        private void sitesListView_KeyDown(object sender, KeyEventArgs e)
        {
            Global.mnFrm.cmCde.listViewKeyDown(this.sitesListView, e);
        }

        private void jobListView_KeyDown(object sender, KeyEventArgs e)
        {
            Global.mnFrm.cmCde.listViewKeyDown(this.jobListView, e);
        }

        private void gradesListView_KeyDown(object sender, KeyEventArgs e)
        {
            Global.mnFrm.cmCde.listViewKeyDown(this.gradesListView, e);
        }

        private void positionListView_KeyDown(object sender, KeyEventArgs e)
        {
            Global.mnFrm.cmCde.listViewKeyDown(this.positionListView, e);
        }

        private void searchForPosTextBox_Click(object sender, EventArgs e)
        {
            this.searchForPosTextBox.SelectAll();
        }

        private void searchForGrdTextBox_Click(object sender, EventArgs e)
        {
            this.searchForGrdTextBox.SelectAll();
        }

        private void searchForJobsTextBox_Click(object sender, EventArgs e)
        {
            this.searchForJobsTextBox.SelectAll();
        }

        private void searchForSiteTextBox_Click(object sender, EventArgs e)
        {
            this.searchForSiteTextBox.SelectAll();
        }

        private void searchForDivTextBox_Click(object sender, EventArgs e)
        {
            this.searchForDivTextBox.SelectAll();
        }

        private void searchForOrgDetTextBox_Click(object sender, EventArgs e)
        {
            this.searchForOrgDetTextBox.SelectAll();
        }

        private void orgParentTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!this.obey_orgDet_evnts)
            {
                return;
            }
            this.txtChngd = true;
        }

        private void orgParentTextBox_Leave(object sender, EventArgs e)
        {
            if (this.txtChngd == false)
            {
                return;
            }
            this.txtChngd = false;
            TextBox mytxt = (TextBox)sender;
            this.obey_orgDet_evnts = false;
            this.srchWrd = mytxt.Text;
            if (!mytxt.Text.Contains("%"))
            {
                this.srchWrd = "%" + this.srchWrd.Replace(" ", "%") + "%";
            }

            if (mytxt.Name == "orgParentTextBox")
            {
                this.orgParentTextBox.Text = "";
                this.orgPrntIDTextBox.Text = "-1";
                this.selPrntOrgButton_Click(this.selPrntOrgButton, e);
            }
            else if (mytxt.Name == "orgTypTextBox")
            {
                this.orgTypTextBox.Text = "";
                this.orgTypIDTextBox.Text = "-1";
                this.orgTypButton_Click(this.orgTypButton, e);
            }
            else if (mytxt.Name == "crncyCodeTextBox")
            {
                this.crncyCodeTextBox.Text = "";
                this.crncyIDTextBox.Text = "-1";
                this.selCrncyButton_Click(this.selCrncyButton, e);
            }
            this.srchWrd = "%";
            this.obey_orgDet_evnts = true;
            this.txtChngd = false;
        }

        private void parentDivTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!this.obey_divDet_evnts)
            {
                return;
            }
            this.txtChngd = true;
        }

        private void parentDivTextBox_Leave(object sender, EventArgs e)
        {
            if (this.txtChngd == false)
            {
                return;
            }
            this.txtChngd = false;
            TextBox mytxt = (TextBox)sender;
            this.obey_divDet_evnts = false;
            this.srchWrd = mytxt.Text;
            if (!mytxt.Text.Contains("%"))
            {
                this.srchWrd = "%" + this.srchWrd.Replace(" ", "%") + "%";
            }

            if (mytxt.Name == "parentDivTextBox")
            {
                this.parentDivTextBox.Text = "";
                this.parentDivIDTextBox.Text = "-1";
                this.parntDivButton_Click(this.parntDivButton, e);
            }
            else if (mytxt.Name == "divTypTextBox")
            {
                this.divTypTextBox.Text = "";
                this.divTypIDTextBox.Text = "-1";
                this.divTypButton_Click(this.divTypButton, e);
            }

            this.srchWrd = "%";
            this.obey_divDet_evnts = true;
            this.txtChngd = false;
        }

        private void parentJobTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!this.obey_jobs_evnts)
            {
                return;
            }
            this.txtChngd = true;
        }

        private void parentJobTextBox_Leave(object sender, EventArgs e)
        {
            if (this.txtChngd == false)
            {
                return;
            }
            this.txtChngd = false;
            TextBox mytxt = (TextBox)sender;
            this.obey_jobs_evnts = false;
            this.srchWrd = mytxt.Text;
            if (!mytxt.Text.Contains("%"))
            {
                this.srchWrd = "%" + this.srchWrd.Replace(" ", "%") + "%";
            }

            if (mytxt.Name == "parentJobTextBox")
            {
                this.parentJobTextBox.Text = "";
                this.parentJobIDTextBox.Text = "-1";
                this.parentJobButton_Click(this.parentJobButton, e);
            }

            this.srchWrd = "%";
            this.obey_jobs_evnts = true;
            this.txtChngd = false;
        }

        private void parntGradeTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!this.obey_grd_evnts)
            {
                return;
            }
            this.txtChngd = true;
        }

        private void parntGradeTextBox_Leave(object sender, EventArgs e)
        {
            if (this.txtChngd == false)
            {
                return;
            }
            this.txtChngd = false;
            TextBox mytxt = (TextBox)sender;
            this.obey_grd_evnts = false;
            this.srchWrd = mytxt.Text;
            if (!mytxt.Text.Contains("%"))
            {
                this.srchWrd = "%" + this.srchWrd.Replace(" ", "%") + "%";
            }

            if (mytxt.Name == "parntGradeTextBox")
            {
                this.parntGradeTextBox.Text = "";
                this.parntGradeIDTextBox.Text = "-1";
                this.parntGradeButton_Click(this.parntGradeButton, e);
            }

            this.srchWrd = "%";
            this.obey_grd_evnts = true;
            this.txtChngd = false;
        }

        private void parntPositionTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!this.obey_pos_evnts)
            {
                return;
            }
            this.txtChngd = true;
        }

        private void parntPositionTextBox_Leave(object sender, EventArgs e)
        {
            if (this.txtChngd == false)
            {
                return;
            }
            this.txtChngd = false;
            TextBox mytxt = (TextBox)sender;
            this.obey_pos_evnts = false;
            this.srchWrd = mytxt.Text;
            if (!mytxt.Text.Contains("%"))
            {
                this.srchWrd = "%" + this.srchWrd.Replace(" ", "%") + "%";
            }

            if (mytxt.Name == "parntPositionTextBox")
            {
                this.parntPositionTextBox.Text = "";
                this.parntPositionIDTextBox.Text = "-1";
                this.parntPositionButton_Click(this.parntPositionButton, e);
            }

            this.srchWrd = "%";
            this.obey_pos_evnts = true;
            this.txtChngd = false;
        }

        private void noOfSgmntsNumUpDown_ValueChanged(object sender, EventArgs e)
        {
            this.populateSegments();
        }

        private void populateSegments()
        {
            if (this.shdObeyOrgDetEvts() == false)
            {
                return;
            }
            this.obey_orgDet_evnts = false;
            this.accntSgmntsDataGridView.Rows.Clear();
            int rwcnt = (int)this.noOfSgmntsNumUpDown.Value;
            this.accntSgmntsDataGridView.ForeColor = Color.Black;
            this.accntSgmntsDataGridView.DefaultCellStyle.ForeColor = Color.Black;
            for (int i = 0; i < rwcnt; i++)
            {
                this.accntSgmntsDataGridView.RowCount += 1;
                int rowIdx = this.accntSgmntsDataGridView.RowCount - 1;
                DataSet dtst = Global.get_One_SegmentDet((i + 1), int.Parse(this.orgIDTextBox.Text));
                this.accntSgmntsDataGridView.Rows[rowIdx].HeaderCell.Value = (i + 1).ToString();
                this.accntSgmntsDataGridView.Rows[rowIdx].Cells[0].Value = (i + 1).ToString();
                if (dtst.Tables[0].Rows.Count > 0)
                {
                    this.accntSgmntsDataGridView.Rows[rowIdx].Cells[1].Value = dtst.Tables[0].Rows[0][1].ToString();
                    this.accntSgmntsDataGridView.Rows[rowIdx].Cells[2].Value = (dtst.Tables[0].Rows[0][2].ToString() == "NaturalAccount") ? true : false;
                    this.accntSgmntsDataGridView.Rows[rowIdx].Cells[3].Value = dtst.Tables[0].Rows[0][2].ToString();
                    this.accntSgmntsDataGridView.Rows[rowIdx].Cells[4].Value = "Attached Values";
                    this.accntSgmntsDataGridView.Rows[rowIdx].Cells[5].Value = dtst.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    this.accntSgmntsDataGridView.Rows[rowIdx].Cells[1].Value = "";
                    this.accntSgmntsDataGridView.Rows[rowIdx].Cells[2].Value = false;
                    this.accntSgmntsDataGridView.Rows[rowIdx].Cells[3].Value = "Other";
                    this.accntSgmntsDataGridView.Rows[rowIdx].Cells[4].Value = "Attached Values";
                    this.accntSgmntsDataGridView.Rows[rowIdx].Cells[5].Value = -1;
                }
            }
            System.Windows.Forms.Application.DoEvents();
            this.obey_orgDet_evnts = true;
        }

        private void prpareForLnsEdit()
        {
            this.accntSgmntsDataGridView.ReadOnly = false;
            this.accntSgmntsDataGridView.Columns[0].ReadOnly = true;
            this.accntSgmntsDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.accntSgmntsDataGridView.Columns[1].ReadOnly = false;
            this.accntSgmntsDataGridView.Columns[1].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            this.accntSgmntsDataGridView.Columns[2].ReadOnly = true;
            this.accntSgmntsDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.accntSgmntsDataGridView.Columns[3].ReadOnly = false;
            this.accntSgmntsDataGridView.Columns[3].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            this.accntSgmntsDataGridView.DefaultCellStyle.ForeColor = Color.Black;
        }

        private void disableLnsEdit()
        {
            this.accntSgmntsDataGridView.DefaultCellStyle.ForeColor = Color.Black;
            this.accntSgmntsDataGridView.Columns[0].ReadOnly = true;
            this.accntSgmntsDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.accntSgmntsDataGridView.Columns[1].ReadOnly = true;
            this.accntSgmntsDataGridView.Columns[1].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.accntSgmntsDataGridView.Columns[2].ReadOnly = true;
            this.accntSgmntsDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.accntSgmntsDataGridView.Columns[3].ReadOnly = true;
            this.accntSgmntsDataGridView.Columns[3].DefaultCellStyle.BackColor = Color.WhiteSmoke;

        }

        private void accntSgmntsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e == null || this.obey_orgDet_evnts == false)
            {
                return;
            }
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            bool prv = this.obey_orgDet_evnts;
            this.obey_orgDet_evnts = false;

            if (this.accntSgmntsDataGridView.Rows[e.RowIndex].Cells[0].Value == null)
            {
                this.accntSgmntsDataGridView.Rows[e.RowIndex].Cells[0].Value = "-1";
            }

            if (this.accntSgmntsDataGridView.Rows[e.RowIndex].Cells[1].Value == null)
            {
                this.accntSgmntsDataGridView.Rows[e.RowIndex].Cells[1].Value = "";
            }
            if (this.accntSgmntsDataGridView.Rows[e.RowIndex].Cells[2].Value == null)
            {
                this.accntSgmntsDataGridView.Rows[e.RowIndex].Cells[2].Value = false;
            }
            if (this.accntSgmntsDataGridView.Rows[e.RowIndex].Cells[3].Value == null)
            {
                this.accntSgmntsDataGridView.Rows[e.RowIndex].Cells[3].Value = "Other";
            }
            if (this.accntSgmntsDataGridView.Rows[e.RowIndex].Cells[5].Value == null)
            {
                this.accntSgmntsDataGridView.Rows[e.RowIndex].Cells[5].Value = -1;
            }

            if (e.ColumnIndex == 4)
            {
                Global.mnFrm.cmCde.showMsg("Sorry! Feature not available in this edition!\nContact your the Software Provider!", 0);
                return;
            }
            this.obey_orgDet_evnts = true;
        }
    }
}


