using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SystemAdministration.Classes;
using SystemAdministration.Dialogs;
using Npgsql;

namespace SystemAdministration.Forms
{
    public partial class mainForm : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        #region "GLOBAL VARIABLES..."
        public CommonCode.CommonCodes cmmnCode = new CommonCode.CommonCodes();
        //public NpgsqlConnection gnrlSQLConn = new NpgsqlConnection();
        public Int64 usr_id = -1;
        public int[] role_st_id = new int[0];
        public Int64 lgn_num = -1;
        public int Og_id = -1;

        string[] menuItems = {"Users & their Roles", "Roles & Priviledges",
        "Modules & Priviledges", "Extra Info Labels", "Security Policies", "Server Settings",
        "Track User Logins", "Audit Trail Tables"};
        string[] menuImages = {"groupings.png", "staffs.png", "shield_64.png", "shield_64.png"
            ,"SecurityLock.png", "antenna1.png", "54.png","features_audittrail_icon.jpg"};

        //User Panel Variables;
        Int64 usr_cur_indx = 0;
        Int64 totl_usrs = 0;
        public string usrs_SQL = "";
        public string usr_roles_SQL = "";
        public string prsns_SQL = "";
        bool obey_user_evnts = false;
        public bool txtChngd = false;
        public string srchWrd = "%";
        bool is_last_usr = false;
        long last_usr_num = 0;
        //Roles Panel Variables;
        Int64 role_cur_indx = 0;
        Int64 totl_roles = 0;
        public string roles_SQL = "";
        public string role_prvldgs_SQL = "";
        bool obey_roles_evnts = false;
        bool is_last_role = false;
        long last_role_num = 0;
        //Modules Panel Variables
        Int64 mdl_cur_indx = 0;
        Int64 totl_mdls = 0;
        public string mdls_SQL = "";
        public string mdl_prvldgs_SQL = "";
        bool obey_mdls_evnts = false;
        bool is_last_mdl = false;
        long last_mdl_num = 0;
        //Modules Extra Info Panel Variables
        Int64 extinf_cur_indx = 0;
        Int64 totl_extinf = 0;
        public string extinf_SQL = "";
        public string mdl_subgroups_SQL = "";
        public string extinf_mdls_SQL = "";
        bool obey_extinf_evnts = false;
        bool is_last_extinf = false;
        long last_extinf_num = 0;
        //Policy Panel Variables
        Int64 plcy_cur_indx = 0;
        Int64 totl_plcy = 0;
        public string plcys_SQL = "";
        public string plcy_Adt_Tbls_SQL = "";
        bool obey_pcly_evnts = false;
        bool edit_plcy = false;
        bool add_plcy = false;
        bool is_last_plcy = false;
        long last_plcy_num = 0;
        //Email Server Panel Variables
        Int64 email_cur_indx = 0;
        bool is_last_email = false;
        long last_email_num = 0;
        Int64 totl_eml_srvs = 0;
        public string eml_srvs_SQL = "";
        bool obey_eml_srvs_evnts = false;
        bool edit_eml_srvs = false;
        bool add_eml_srvs = false;
        bool beenToCheckBx = false;
        //Track Logins Panel Variables
        Int64 lgns_cur_indx = 0;
        bool is_last_lgns = false;
        long last_lgns_num = 0;
        Int64 totl_lgns = 0;
        public string lgns_SQL = "";
        bool obey_lgns_evnts = false;
        //Audit Trail Panel Variables
        Int64 adt_cur_indx = 0;
        bool is_last_adt = false;
        long last_adt_num = 0;
        Int64 totl_adts = 0;
        public string adt_SQL = "";
        bool obey_adt_evnts = false;

        bool vwSQL = false;
        bool vwRcHstry = false;
        bool addUsers = false;
        bool editUsers = false;

        bool addRoles = false;
        bool editRoles = false;

        bool addPlcys = false;
        bool editPlcys = false;

        bool addSvrs = false;
        bool editSvrs = false;

        bool setMnlPwd = false;
        bool setAutoPwd = false;
        #endregion

        #region "FORM EVENTS.."
        public mainForm()
        {
            InitializeComponent();
        }
        private void mainForm_Load(object sender, EventArgs e)
        {
            this.accDndLabel.Visible = false;
            Global.mySecurity.Initialize();
            Global.myNwMainFrm = this;
            //Global.myNwMainFrm.cmmnCode.pgSqlConn = this.gnrlSQLConn;
            Global.myNwMainFrm.cmmnCode.Login_number = this.lgn_num;
            Global.myNwMainFrm.cmmnCode.Role_Set_IDs = this.role_st_id;
            Global.myNwMainFrm.cmmnCode.User_id = this.usr_id;
            Global.myNwMainFrm.cmmnCode.Org_id = this.Og_id;

            this.mailLabel.Visible = false;
            this.hideAllPanels();
            System.Windows.Forms.Application.DoEvents();
            Color[] clrs = Global.myNwMainFrm.cmmnCode.getColors();
            this.BackColor = clrs[0];
            //this.mailLabel.BackColor = clrs[0];
            this.usersPanel.BackColor = clrs[0];
            this.rolesPanel.BackColor = clrs[0];
            this.modulesPanel.BackColor = clrs[0];
            this.extraInfoPanel.BackColor = clrs[0];
            this.policyPanel.BackColor = clrs[0];
            this.emailServerPanel.BackColor = clrs[0];
            this.loginsPanel.BackColor = clrs[0];
            this.auditPanel.BackColor = clrs[0];
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
            //this.glsLabel2.TopFill = clrs[0];
            //this.glsLabel2.BackColor = clrs[0];
            //this.glsLabel2.BottomFill = clrs[1];
            //this.glsLabel3.TopFill = clrs[0];
            //this.glsLabel3.BackColor = clrs[0];
            //this.glsLabel3.BottomFill = clrs[1];
            this.glsLabel4.TopFill = clrs[0];
            this.glsLabel4.BackColor = clrs[0];
            this.glsLabel4.BottomFill = clrs[1];
            this.glsLabel5.TopFill = clrs[0];
            this.glsLabel5.BackColor = clrs[0];
            this.glsLabel5.BottomFill = clrs[1];
            //this.glsLabel8.TopFill = clrs[0];
            //this.glsLabel8.BackColor = clrs[0];
            //this.glsLabel8.BottomFill = clrs[1];
            this.glsLabel9.TopFill = clrs[0];
            this.glsLabel9.BackColor = clrs[0];
            this.glsLabel9.BottomFill = clrs[1];
            this.glsLabel10.TopFill = clrs[0];
            this.glsLabel10.BackColor = clrs[0];
            this.glsLabel10.BottomFill = clrs[1];
            //this.glsLabel12.TopFill = clrs[0];
            //this.glsLabel12.BackColor = clrs[0];
            //this.glsLabel12.BottomFill = clrs[1];
            //this.glsLabel13.TopFill = clrs[0];
            //this.glsLabel13.BackColor = clrs[0];
            //this.glsLabel13.BottomFill = clrs[1];
            //this.glsLabel14.TopFill = clrs[0];
            //this.glsLabel14.BackColor = clrs[0];
            //this.glsLabel14.BottomFill = clrs[1];
            //this.glsLabel15.TopFill = clrs[0];
            //this.glsLabel15.BackColor = clrs[0];
            //this.glsLabel15.BottomFill = clrs[1];
            this.glsLabel16.TopFill = clrs[0];
            this.glsLabel16.BackColor = clrs[0];
            this.glsLabel16.BottomFill = clrs[1];
            this.glsLabel18.TopFill = clrs[0];
            this.glsLabel18.BackColor = clrs[0];
            this.glsLabel18.BottomFill = clrs[1];
            this.glsLabel19.TopFill = clrs[0];
            this.glsLabel19.BackColor = clrs[0];
            this.glsLabel19.BottomFill = clrs[1];
            //this.glsLabel20.TopFill = clrs[0];
            //this.glsLabel20.BackColor = clrs[0];
            //this.glsLabel20.BottomFill = clrs[1];
            this.glsLabel21.TopFill = clrs[0];
            this.glsLabel21.BackColor = clrs[0];
            this.glsLabel21.BottomFill = clrs[1];
            System.Windows.Forms.Application.DoEvents();
            Global.refreshRqrdVrbls();
            Global.mySecurity.loadMyRolesNMsgtyps();
            System.Windows.Forms.Application.DoEvents();
            bool vwAct = Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[0]);
            if (!vwAct)
            {
                this.Controls.Clear();
                this.Controls.Add(this.accDndLabel);
                this.accDndLabel.Visible = true;
                return;
            }

            this.disableFormButtons();
            this.showAllPanels();
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
            System.Windows.Forms.Application.DoEvents();
            System.Windows.Forms.Application.DoEvents();
            //this.timer1.Enabled = true;
            //this.timer1.Interval = 100;
        }

        private void mainForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            Global.mySecurity.Dispose();
        }
        #endregion

        #region "EVENT HANDLERS..."
        private void leftTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null)
            {
                return;
            }
            this.loadCorrectPanel(e.Node.Text);
        }
        #endregion

        #region "CUSTOM FUNCTIONS..."
        #region "GENERAL..."
        private void populateTreeView()
        {
            this.leftTreeView.Nodes.Clear();
            if (!Global.myNwMainFrm.cmmnCode.isThsMchnPrmtd())
            {
                Global.myNwMainFrm.cmmnCode.showMsg("This Machine is not Permitted to run this software!\r\nContact the Vendor for Assistance!", 4);
                return;
            }
            this.tabControl1.Controls.Clear();
            for (int i = 0; i < menuItems.Length; i++)
            {
                if (i >= 3)
                {
                    if (Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[0] +
                 "~" + Global.dfltPrvldgs[i]) == false)
                    {
                        continue;
                    }
                }
                else
                {
                    if (Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[0] +
                     "~" + Global.dfltPrvldgs[i + 1]) == false)
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

        private void loadCorrectPanel(string inpt_name)
        {
            if (inpt_name == menuItems[0])
            {
                this.showATab(ref this.tabPage1);
                this.loadUserPanel();
            }
            else if (inpt_name == menuItems[1])
            {
                this.showATab(ref this.tabPage2);
                this.loadRolesPanel();
            }
            else if (inpt_name == menuItems[2])
            {
                this.showATab(ref this.tabPage3);
                this.loadModulesPanel();
            }
            else if (inpt_name == menuItems[3])
            {
                this.showATab(ref this.tabPage4);
                this.loadExtInfPanel();
            }
            else if (inpt_name == menuItems[4])
            {
                this.showATab(ref this.tabPage5);
                long oldID = Global.getPlcyID("Rho Standard Policy");
                if (oldID <= 0)
                {
                    Global.createPolicy("Rho Standard Policy", 3,
                      90, 30, true, true, true, true,
                      "ANY 3", false, 10, 7, 25, 30, true, false, 4500);
                }
                this.loadPolicyPanel();
            }
            else if (inpt_name == menuItems[5])
            {
                this.showATab(ref this.tabPage6);
                long oldID = Global.getEmlSvrID("smtp.gmail.com");
                if (oldID <= 0)
                {
                    Global.createEml_Svr("smtp.gmail.com", "rhomicomgh@gmail.com",
                      "123", 587, false, "rhomicom.com",
                      "ftp://127.0.0.1", "ftpuser", "123",
                      21, "/test_database", false, Global.myNwMainFrm.cmmnCode.getPGBinDrctry(),
                      Global.myNwMainFrm.cmmnCode.getBackupDrctry(),
                      "1", "9600", "1200", "");
                }
                this.loadEmailSrvrPanel();
            }
            else if (inpt_name == menuItems[6])
            {
                this.showATab(ref this.tabPage8);
                this.loadLoginsPanel();
            }
            else if (inpt_name == menuItems[7])
            {
                this.showATab(ref this.tabPage7);
                this.loadAuditPanel();
            }
            Global.currentPanel = inpt_name;
        }

        private void hideAllPanels()
        {
            this.usersPanel.Visible = false;
            this.usersPanel.Enabled = false;
            System.Windows.Forms.Application.DoEvents();
            this.usersPanel.Dock = DockStyle.None;
            this.rolesPanel.Visible = false;
            this.rolesPanel.Enabled = false;
            System.Windows.Forms.Application.DoEvents();
            this.rolesPanel.Dock = DockStyle.None;
            this.modulesPanel.Visible = false;
            this.modulesPanel.Enabled = false;
            System.Windows.Forms.Application.DoEvents();
            this.modulesPanel.Dock = DockStyle.None;
            this.policyPanel.Visible = false;
            this.policyPanel.Enabled = false;
            System.Windows.Forms.Application.DoEvents();
            this.policyPanel.Dock = DockStyle.None;
            this.loginsPanel.Visible = false;
            this.loginsPanel.Enabled = false;
            System.Windows.Forms.Application.DoEvents();
            this.loginsPanel.Dock = DockStyle.None;
            this.auditPanel.Visible = false;
            this.auditPanel.Enabled = false;
            System.Windows.Forms.Application.DoEvents();
            this.auditPanel.Dock = DockStyle.None;
            this.emailServerPanel.Visible = false;
            this.emailServerPanel.Enabled = false;
            System.Windows.Forms.Application.DoEvents();
            this.emailServerPanel.Dock = DockStyle.None;
            this.extraInfoPanel.Visible = false;
            this.extraInfoPanel.Enabled = false;
            System.Windows.Forms.Application.DoEvents();
            this.extraInfoPanel.Dock = DockStyle.None;
            System.Windows.Forms.Application.DoEvents();
        }

        private void showAllPanels()
        {
            this.usersPanel.Visible = true;
            this.usersPanel.Enabled = true;
            System.Windows.Forms.Application.DoEvents();
            this.usersPanel.Dock = DockStyle.Fill;
            this.rolesPanel.Visible = true;
            this.rolesPanel.Enabled = true;
            System.Windows.Forms.Application.DoEvents();
            this.rolesPanel.Dock = DockStyle.Fill;
            this.modulesPanel.Visible = true;
            this.modulesPanel.Enabled = true;
            System.Windows.Forms.Application.DoEvents();
            this.modulesPanel.Dock = DockStyle.Fill;
            this.policyPanel.Visible = true;
            this.policyPanel.Enabled = true;
            System.Windows.Forms.Application.DoEvents();
            this.policyPanel.Dock = DockStyle.Fill;
            this.loginsPanel.Visible = true;
            this.loginsPanel.Enabled = true;
            System.Windows.Forms.Application.DoEvents();
            this.loginsPanel.Dock = DockStyle.Fill;
            this.auditPanel.Visible = true;
            this.auditPanel.Enabled = true;
            System.Windows.Forms.Application.DoEvents();
            this.auditPanel.Dock = DockStyle.Fill;
            this.emailServerPanel.Visible = true;
            this.emailServerPanel.Enabled = true;
            System.Windows.Forms.Application.DoEvents();
            this.emailServerPanel.Dock = DockStyle.Fill;
            this.extraInfoPanel.Visible = true;
            this.extraInfoPanel.Enabled = true;
            System.Windows.Forms.Application.DoEvents();
            this.extraInfoPanel.Dock = DockStyle.Fill;
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

        private void showAPanel(ref Panel my_panel)
        {
            my_panel.Dock = DockStyle.Fill;
            System.Windows.Forms.Application.DoEvents();
            my_panel.Enabled = true;
            my_panel.Visible = true;
            System.Windows.Forms.Application.DoEvents();
        }

        private void disableFormButtons()
        {
            this.vwSQL = Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[18]);
            this.vwRcHstry = Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[19]);

            this.addUsers = Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[8]);
            this.addUserToolStripMenuItem.Enabled = this.addUsers;
            this.addUserRoleToolStripMenuItem.Enabled = this.addUsers;
            this.imprtUsersButton.Enabled = this.addUsers;

            this.editUsers = Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[9]);
            this.editUserToolStripMenuItem.Enabled = this.editUsers;

            if (this.editUsers == false)
            {
                this.usrVldStrtDteTextBox.ReadOnly = true;
                this.usrVldStrtDteTextBox.BackColor = Color.WhiteSmoke;
                this.usrVldEndDteTextBox.ReadOnly = true;
                this.usrVldEndDteTextBox.BackColor = Color.WhiteSmoke;
            }
            else
            {
                this.usrVldStrtDteTextBox.ReadOnly = false;
                this.usrVldStrtDteTextBox.BackColor = Color.FromArgb(255, 255, 128);
                this.usrVldEndDteTextBox.ReadOnly = false;
                this.usrVldEndDteTextBox.BackColor = Color.FromArgb(255, 255, 128);
            }
            this.addRoles = Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[10]);
            this.addRoleMainMenuItem.Enabled = this.addRoles;
            this.addRlPrvldgMenuItem.Enabled = this.addRoles;

            this.editRoles = Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[11]);
            this.editRoleMainMenuItem.Enabled = this.editRoles;

            this.addPlcys = Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[12]);
            this.addPlcyButton.Enabled = this.addPlcys;

            this.editPlcys = Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[13]);
            this.editPlcyButton.Enabled = this.editPlcys;
            this.editPlcyMdlMenuItem.Enabled = this.editPlcys;

            this.addSvrs = Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[14]);
            this.addEmlSvrButton.Enabled = this.addSvrs;

            this.editSvrs = Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[15]);
            this.editEmlSvrButton.Enabled = this.editSvrs;

            this.setMnlPwd = Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[15]);
            this.setAutoPwd = Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[15]);

            this.changePswdManButton.Enabled = this.setMnlPwd;
            this.changePswdAutoButton.Enabled = this.setAutoPwd;

            bool ext = Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[20]);
            this.addEditExtInfMenuItem.Enabled = ext;
            this.enableDisableToolStripMenuItem.Enabled = Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[20]);
            this.deleteLaToolStripMenuItem.Enabled = Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[21]);

            this.vwSQLAdtButton.Enabled = this.vwSQL;
            this.vwSQLAdtMenuItem.Enabled = this.vwSQL;
            this.vwSQLEmlSvrButton.Enabled = this.vwSQL;
            this.vwSQLExtInfLblMenuItem.Enabled = this.vwSQL;
            this.vwSQLLgnMenuItem.Enabled = this.vwSQL;
            this.vwSQLLgnsButton.Enabled = this.vwSQL;
            this.vwSQLMdlMenuItem.Enabled = this.vwSQL;
            this.vwSqlMdlPrvldgMenuItem.Enabled = this.vwSQL;
            this.vwSQLPlcyButton.Enabled = this.vwSQL;
            this.vwSQLPlcyMdlsMenuItem.Enabled = this.vwSQL;
            this.vwSQLRlPrvldgMenuItem.Enabled = this.vwSQL;
            this.vwSQLRoleMainMenuItem.Enabled = this.vwSQL;
            this.vwSQLSubGrpsMenuItem.Enabled = this.vwSQL;

            this.recHstryEmlSvrButton.Enabled = this.vwRcHstry;
            this.recHstryPlcyButton.Enabled = this.vwRcHstry;
            this.recHstryPlcyMdlsMenuItem.Enabled = this.vwRcHstry;
            this.recHstryRlPrvldgMenuItem.Enabled = this.vwRcHstry;
            this.recHstryRoleMainMenuItem.Enabled = this.vwRcHstry;
            this.recordHistoryExtInfToolStripMenuItem.Enabled = this.vwRcHstry;
            this.recordHistoryUsrRoleToolStripMenuItem.Enabled = this.vwRcHstry;
            this.recordHistoryUsrsToolStripMenuItem.Enabled = this.vwRcHstry;
        }
        #endregion

        #region "USER PANEL..."
        private void loadUserPanel()
        {
            if (this.searchInUserComboBox.SelectedIndex < 0)
            {
                this.searchInUserComboBox.SelectedIndex = 2;
            }
            if (this.searchForUserTextBox.Text.Contains("%") == false)
            {
                this.searchForUserTextBox.Text = "%" + this.searchForUserTextBox.Text.Replace(" ", "%") + "%";
            }
            if (this.searchForUserTextBox.Text == "%%")
            {
                this.searchForUserTextBox.Text = "%";
            }
            int dsply = 0;
            if (this.dsplySizeUserComboBox.Text == ""
              || int.TryParse(this.dsplySizeUserComboBox.Text, out dsply) == false)
            {
                this.dsplySizeUserComboBox.Text = Global.myNwMainFrm.cmmnCode.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            //this.userListView.Height = this.panel7.Top - this.navToolStrip.Bottom - 10;
            //this.userRoleslistView.Height = this.panel7.Top - this.groupBox2.Bottom - 10;
            this.totl_usrs = this.cmmnCode.Big_Val;
            this.is_last_usr = false;
            this.getUsrPnlData();
        }

        private void getUsrPnlData()
        {
            this.updtUsrTotals();
            this.populateUserLstVw();
            this.updtUsrNavLabels();
        }

        private void updtUsrTotals()
        {
            Global.myNwMainFrm.cmmnCode.navFuncts.FindNavigationIndices(
              long.Parse(this.dsplySizeUserComboBox.Text), this.totl_usrs);

            if (this.usr_cur_indx >= Global.myNwMainFrm.cmmnCode.navFuncts.totalGroups)
            {
                this.usr_cur_indx = Global.myNwMainFrm.cmmnCode.navFuncts.totalGroups - 1;
            }
            if (this.usr_cur_indx < 0)
            {
                this.usr_cur_indx = 0;
            }
            Global.myNwMainFrm.cmmnCode.navFuncts.currentNavigationIndex = this.usr_cur_indx;
        }

        private void updtUsrNavLabels()
        {
            this.moveFirstUserButton.Enabled = Global.myNwMainFrm.cmmnCode.navFuncts.moveFirstBtnStatus();
            this.movePreviousUserButton.Enabled = Global.myNwMainFrm.cmmnCode.navFuncts.movePrevBtnStatus();
            this.moveNextUserButton.Enabled = Global.myNwMainFrm.cmmnCode.navFuncts.moveNextBtnStatus();
            this.moveLastUserButton.Enabled = Global.myNwMainFrm.cmmnCode.navFuncts.moveLastBtnStatus();
            this.positionUserTextBox.Text = Global.myNwMainFrm.cmmnCode.navFuncts.displayedRecordsNumbers();
            if (this.is_last_usr)
            {
                this.totalRecUserLabel.Text = Global.myNwMainFrm.cmmnCode.navFuncts.totalRecordsLabel();
            }
            else
            {
                this.totalRecUserLabel.Text = "of Total";
            }
        }

        private void populateUserLstVw()
        {
            this.obey_user_evnts = false;
            DataSet dtst = Global.get_Basic_UserInfo(this.searchForUserTextBox.Text,
              this.searchInUserComboBox.Text, this.usr_cur_indx,
              int.Parse(this.dsplySizeUserComboBox.Text));
            this.userListView.Items.Clear();
            this.clearAcntInfo();
            this.userRoleslistView.Items.Clear();
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.last_usr_num = Global.myNwMainFrm.cmmnCode.navFuncts.startIndex() + i;
                ListViewItem nwItm = new ListViewItem(new string[] { (Global.myNwMainFrm.cmmnCode.navFuncts.startIndex() + i).ToString(),
                dtst.Tables[0].Rows[i][0].ToString(), dtst.Tables[0].Rows[i][1].ToString(),
                dtst.Tables[0].Rows[i][2].ToString(), dtst.Tables[0].Rows[i][3].ToString(),
        dtst.Tables[0].Rows[i][4].ToString(),
        dtst.Tables[0].Rows[i][5].ToString() });
                this.userListView.Items.Add(nwItm);
            }
            this.correctUsrNavLbls(dtst);
            if (this.userListView.Items.Count > 0)
            {
                this.userListView.Items[0].Selected = true;
            }
            this.obey_user_evnts = true;
        }

        private void populateUsrAcntInfo()
        {
            this.obey_user_evnts = false;
            if (this.userListView.SelectedItems.Count > 0)
            {
                this.isSuspendedCheckBox.Checked = Global.isAccntSuspended(this.userListView.SelectedItems[0].SubItems[1].Text);
                this.isTempCheckBox.Checked = Global.isPswdTmp(this.userListView.SelectedItems[0].SubItems[1].Text);
                this.isLockedCheckBox.Checked = Global.isUserAccntLckd(this.userListView.SelectedItems[0].SubItems[1].Text);
                this.isExpiredCheckBox.Checked = Global.isPswdExpired(this.userListView.SelectedItems[0].SubItems[1].Text);
                DataSet dtst = Global.get_AccountInfo(this.userListView.SelectedItems[0].SubItems[1].Text);
                if (dtst.Tables[0].Rows.Count > 0)
                {
                    this.failedLgnAtmptTextBox.Text = dtst.Tables[0].Rows[0][0].ToString();
                    this.lastLoginAtmptTextBox.Text = dtst.Tables[0].Rows[0][1].ToString();
                    this.lastPwdChngeTextBox.Text = dtst.Tables[0].Rows[0][2].ToString();
                    this.usrVldStrtDteTextBox.Text = dtst.Tables[0].Rows[0][3].ToString();
                    this.usrVldEndDteTextBox.Text = dtst.Tables[0].Rows[0][4].ToString();
                    this.agePswdTextBox.Text = dtst.Tables[0].Rows[0][5].ToString();
                }
            }
            this.obey_user_evnts = true;
        }

        private void correctUsrNavLbls(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.usr_cur_indx == 0 && totlRecs == 0)
            {
                this.is_last_usr = true;
                this.totl_usrs = 0;
                this.last_usr_num = 0;
                this.usr_cur_indx = 0;
                this.updtUsrTotals();
                this.updtUsrNavLabels();
            }
            else if (this.totl_usrs == Global.myNwMainFrm.cmmnCode.Big_Val
          && totlRecs < int.Parse(this.dsplySizeUserComboBox.Text))
            {
                this.totl_usrs = this.last_usr_num;
                this.is_last_usr = true;
                if (totlRecs == 0)
                {
                    this.usr_cur_indx -= 1;
                    this.updtUsrTotals();
                    this.populateUserLstVw();
                }
                else
                {
                    this.updtUsrTotals();
                }
            }
        }

        private void clearAcntInfo()
        {
            this.obey_user_evnts = false;
            this.isSuspendedCheckBox.Checked = false;
            this.isTempCheckBox.Checked = false;
            this.isLockedCheckBox.Checked = false;
            this.isExpiredCheckBox.Checked = false;
            this.failedLgnAtmptTextBox.Text = "";
            this.lastLoginAtmptTextBox.Text = "";
            this.lastPwdChngeTextBox.Text = "";
            this.usrVldStrtDteTextBox.Text = "";
            this.usrVldEndDteTextBox.Text = "";
            this.obey_user_evnts = true;
        }

        private void populateUsrRoles()
        {
            this.obey_user_evnts = false;
            this.userRoleslistView.Items.Clear();
            if (this.userListView.SelectedItems.Count > 0)
            {
                DataSet dtst = Global.get_Users_Roles(Global.getUserID(this.userListView.SelectedItems[0].SubItems[1].Text));
                for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
                {
                    ListViewItem nwItm = new ListViewItem(new string[] { ( i +1).ToString(),
                dtst.Tables[0].Rows[i][0].ToString(), dtst.Tables[0].Rows[i][1].ToString(),
                dtst.Tables[0].Rows[i][2].ToString(),
                dtst.Tables[0].Rows[i][3].ToString() });
                    this.userRoleslistView.Items.Add(nwItm);
                }
                if (this.userRoleslistView.Items.Count > 0)
                {
                    this.userRoleslistView.Items[0].Selected = true;
                }
            }
            this.obey_user_evnts = true;
        }

        private bool shdObeyUsrEvts()
        {
            return this.obey_user_evnts;
        }

        private void userPnlNavButtons(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecUserLabel.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.is_last_usr = false;
                this.usr_cur_indx = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.is_last_usr = false;
                this.usr_cur_indx -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.is_last_usr = false;
                this.usr_cur_indx += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.is_last_usr = true;
                this.totl_usrs = Global.get_total_Users(this.searchForUserTextBox.Text, this.searchInUserComboBox.Text);
                this.updtUsrTotals();
                this.usr_cur_indx = Global.myNwMainFrm.cmmnCode.navFuncts.totalGroups - 1;
            }
            this.getUsrPnlData();
        }

        private void userListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (this.userListView.SelectedItems.Count > 0)
            // {
            // this.userListView.SelectedItems[0].BackColor = Color.DodgerBlue;
            // }
            if (this.shdObeyUsrEvts() == false || this.userListView.SelectedItems.Count > 1)
            {
                return;
            }
            this.clearAcntInfo();
            this.populateUsrAcntInfo();
            this.populateUsrRoles();
        }

        private void refreshUserButton_Click(object sender, EventArgs e)
        {
            this.loadUserPanel();
        }

        private void addUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.addUserButton_Click(this.addUserButton, e);
        }

        private void editUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.editUserButton_Click(this.editUserButton, e);
        }

        private void refreshUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.loadUserPanel();
            this.Refresh();
        }

        private void recordHistoryUsrsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.userListView.SelectedItems.Count <= 0)
            {
                return;
            }
            Global.myNwMainFrm.cmmnCode.showRecHstry(Global.get_Users_Rec_Hstry(this.userListView.SelectedItems[0].SubItems[1].Text), 19);
        }

        private void viewSQLUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.cmmnCode.showSQL(Global.myNwMainFrm.usrs_SQL, 18);
        }

        private void addUserRoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.addEdtUsrRoleButton_Click(this.addEdtUsrRoleButton, e);
        }

        private void refreshUsrRoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.populateUsrRoles();
            this.Refresh();
        }

        private void recordHistoryUsrRoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.userListView.SelectedItems.Count <= 0
              || this.userRoleslistView.SelectedItems.Count <= 0)
            {
                return;
            }
            Global.myNwMainFrm.cmmnCode.showRecHstry(Global.get_Usr_Roles_Rec_Hstry(
              long.Parse(this.userListView.SelectedItems[0].SubItems[3].Text),
              int.Parse(this.userRoleslistView.SelectedItems[0].SubItems[4].Text)), 19);
        }

        private void viewSQLUsrRoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.cmmnCode.showSQL(Global.myNwMainFrm.usr_roles_SQL, 18);
        }

        private void isTempCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shdObeyUsrEvts() == false)
            {
                return;
            }
            if (this.userListView.SelectedItems.Count > 0)
            {
                this.isTempCheckBox.Checked = Global.isPswdTmp(this.userListView.SelectedItems[0].SubItems[1].Text);
            }
            else
            {
                this.isTempCheckBox.Checked = false;
            }
        }

        private void isExpiredCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shdObeyUsrEvts() == false)
            {
                return;
            }
            if (this.userListView.SelectedItems.Count > 0)
            {
                this.isExpiredCheckBox.Checked = Global.isPswdExpired(this.userListView.SelectedItems[0].SubItems[1].Text);
            }
            else
            {
                this.isExpiredCheckBox.Checked = false;
            }
        }

        private void isLockedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shdObeyUsrEvts() == false)
            {
                return;
            }
            if (isLockedCheckBox.Checked == true)
            {
                isLockedCheckBox.Checked = false;
            }
            else
            {
                if (this.userListView.SelectedItems.Count > 0)
                {
                    if (Global.isUserAccntLckd(this.userListView.SelectedItems[0].SubItems[1].Text) == false)
                    {
                        this.isLockedCheckBox.Checked = false;
                        return;
                    }
                    if (Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[9]) == false)
                    {
                        Global.myNwMainFrm.cmmnCode.showMsg("You don't have permission to perform" +
                          " this action!\nContact your System Administrator!", 0);
                        bool oldState = this.obey_user_evnts;
                        this.obey_user_evnts = false;
                        this.isLockedCheckBox.Checked = true;
                        this.obey_user_evnts = oldState;
                        return;
                    }

                    if (MessageBox.Show("Are you sure you want to Unlock this User?",
            "Rhomicom Message!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        Global.unlockUsrAccnt(this.userListView.SelectedItems[0].SubItems[1].Text);
                        this.clearAcntInfo();
                        this.populateUsrAcntInfo();
                    }
                    else
                    {
                        bool oldState = this.obey_user_evnts;
                        this.obey_user_evnts = false;
                        this.isLockedCheckBox.Checked = true;
                        this.obey_user_evnts = oldState;
                    }
                }
                else
                {
                    this.isLockedCheckBox.Checked = false;
                }
            }
        }

        private void isSuspendedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shdObeyUsrEvts() == false)
            {
                return;
            }
            if (this.userListView.SelectedItems.Count > 0)
            {
                if (isSuspendedCheckBox.Checked == true)
                {
                    if (Global.isAccntSuspended(this.userListView.SelectedItems[0].SubItems[1].Text) == true)
                    {
                        this.isSuspendedCheckBox.Checked = true;
                        return;
                    }
                    if (Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[9]) == false)
                    {
                        Global.myNwMainFrm.cmmnCode.showMsg("You don't have permission to perform" +
                          " this action!\nContact your System Administrator!", 0);
                        bool oldState = this.obey_user_evnts;
                        this.obey_user_evnts = false;
                        this.isSuspendedCheckBox.Checked = false;
                        this.obey_user_evnts = oldState;
                        return;
                    }
                    if (MessageBox.Show("Are you sure you want to Suspend this User?",
                      "Rhomicom Message!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        Global.suspendAccnt(this.userListView.SelectedItems[0].SubItems[1].Text);
                    }
                    this.isSuspendedCheckBox.Checked = Global.isAccntSuspended(this.userListView.SelectedItems[0].SubItems[1].Text);
                }
                else
                {
                    if (Global.isAccntSuspended(this.userListView.SelectedItems[0].SubItems[1].Text) == false)
                    {
                        this.isSuspendedCheckBox.Checked = false;
                        return;
                    }
                    if (Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[9]) == false)
                    {
                        Global.myNwMainFrm.cmmnCode.showMsg("You don't have permission to perform" +
                          " this action!\nContact your System Administrator!", 0);
                        bool oldState = this.obey_user_evnts;
                        this.obey_user_evnts = false;
                        this.isSuspendedCheckBox.Checked = true;
                        this.obey_user_evnts = oldState;
                        return;
                    }
                    if (MessageBox.Show("Are you sure you want to Unsuspend this User?",
                      "Rhomicom Message!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        Global.unsuspendAccnt(this.userListView.SelectedItems[0].SubItems[1].Text);
                    }
                    this.isSuspendedCheckBox.Checked = Global.isAccntSuspended(this.userListView.SelectedItems[0].SubItems[1].Text);
                }
            }
            else
            {
                this.isSuspendedCheckBox.Checked = false;
            }
        }

        private void usrDte1Button_Click(object sender, EventArgs e)
        {
            if (Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[9]) == false)
            {
                Global.myNwMainFrm.cmmnCode.showMsg("You don't have permission to perform" +
                  " this action!\nContact your System Administrator!", 0);
                return;
            }
            Global.myNwMainFrm.cmmnCode.selectDate(ref this.usrVldStrtDteTextBox);
            if (this.userListView.SelectedItems.Count > 0)
            {
                Global.changeUsrVldStrDate(this.userListView.SelectedItems[0].SubItems[1].Text,
                  this.usrVldStrtDteTextBox.Text);
            }
        }

        private void usrDte2Button_Click(object sender, EventArgs e)
        {
            if (Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[9]) == false)
            {
                Global.myNwMainFrm.cmmnCode.showMsg("You don't have permission to perform" +
                  " this action!\nContact your System Administrator!", 0);
                return;
            }
            Global.myNwMainFrm.cmmnCode.selectDate(ref this.usrVldEndDteTextBox);
            if (this.userListView.SelectedItems.Count > 0)
            {
                Global.changeUsrVldEndDate(this.userListView.SelectedItems[0].SubItems[1].Text,
                  this.usrVldEndDteTextBox.Text);
            }
        }

        private void changePswdManButton_Click(object sender, EventArgs e)
        {
            if (Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[16]) == false)
            {
                Global.myNwMainFrm.cmmnCode.showMsg("You don't have permission to perform" +
                  " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.userListView.SelectedItems.Count > 0)
            {
                chngPswdDiag nwDiag = new chngPswdDiag();
                nwDiag.unameTextBox.Text = this.userListView.SelectedItems[0].SubItems[1].Text;
                DialogResult dgRes = nwDiag.ShowDialog();
                if (dgRes == DialogResult.OK)
                {
                    Global.storeOldPassword(Global.getUserID(nwDiag.unameTextBox.Text), Global.getUserPswd(nwDiag.unameTextBox.Text));
                    Global.changeUserPswd(Global.getUserID(nwDiag.unameTextBox.Text), nwDiag.nwPwdTextBox.Text);
                    Global.myNwMainFrm.cmmnCode.showMsg("Password Successfully Changed!", 3);
                }
            }
        }

        private void changePswdAutoButton_Click(object sender, EventArgs e)
        {
            if (Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[17]) == false)
            {
                Global.myNwMainFrm.cmmnCode.showMsg("You don't have permission to perform" +
                  " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.userListView.SelectedItems.Count <= 0)
            {
                Global.myNwMainFrm.cmmnCode.showMsg("Please select a User First!", 0);
                return;
            }
            string nwPswd = Global.myNwMainFrm.cmmnCode.getRandomPswd();
            Global.storeOldPassword(Global.getUserID(this.userListView.SelectedItems[0].SubItems[1].Text),
              Global.getUserPswd(this.userListView.SelectedItems[0].SubItems[1].Text));
            Global.changeUserPswd(Global.getUserID(this.userListView.SelectedItems[0].SubItems[1].Text),
             nwPswd);
            bool emlRes = false;
            this.mailLabel.Visible = true;
            long prsnID = -1;
            long.TryParse(this.userListView.SelectedItems[0].SubItems[4].Text, out prsnID);
            System.Windows.Forms.Application.DoEvents();
            string errMsg = "";
            emlRes = Global.myNwMainFrm.cmmnCode.sendEmail(
              Global.myNwMainFrm.cmmnCode.getPrsnEmail(prsnID), "", "", "",
              CommonCode.CommonCodes.AppName.ToUpper() + " PASSWORD CHANGE", "Hello " +
              this.userListView.SelectedItems[0].SubItems[2].Text +
              "<br/><br/>Your Login Details have been changed as follows:<br/><br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Username: " +
              this.userListView.SelectedItems[0].SubItems[1].Text +
              "<br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Password: " + nwPswd +
              "<br/>Please login immediately to change it!<br/>Thank you!", prsnID.ToString()+"Pwd", ref errMsg);
            this.mailLabel.Visible = false;
            System.Windows.Forms.Application.DoEvents();
            if (emlRes)
            {
                Global.myNwMainFrm.cmmnCode.showMsg("Email Message Sent Successfully!", 3);
                Global.myNwMainFrm.cmmnCode.showMsg("Password Successfully Changed!", 3);
            }
            else
            {
                Global.myNwMainFrm.cmmnCode.showSQLNoPermsn("Password has been Changed " +
                  "but Email could not be sent!\r\n" + errMsg);
            }
        }

        private void exprtUsersButton_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.cmmnCode.exprtUsersTmp();
        }

        private void imprtUsersButton_Click(object sender, EventArgs e)
        {
            if (Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[8]) == false)
            {
                Global.myNwMainFrm.cmmnCode.showMsg("You don't have permission to perform" +
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
                Global.myNwMainFrm.cmmnCode.imprtUsersTmp(this.openFileDialog1.FileName);
            }
            this.loadUserPanel();
        }

        private void exptUsrsMenuItem_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.cmmnCode.exprtToExcel(this.userListView);
        }

        private void exptUsrRolesMenuItem_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.cmmnCode.exprtToExcel(this.userRoleslistView);
        }

        private void searchForUserTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.refreshUserButton_Click(this.refreshUserButton, ex);
            }
        }

        private void positionUserTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                this.userPnlNavButtons(this.movePreviousUserButton, ex);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
            {
                this.userPnlNavButtons(this.moveNextUserButton, ex);
            }
        }
        #endregion

        #region "ROLES PANEL..."
        private bool shdObeyRolesEvts()
        {
            return this.obey_roles_evnts;
        }

        private void loadRolesPanel()
        {
            this.obey_roles_evnts = false;
            if (this.searchInRoleComboBox.SelectedIndex < 0)
            {
                this.searchInRoleComboBox.SelectedIndex = 2;
            }
            if (this.searchForRoleTextBox.Text.Contains("%") == false)
            {
                this.searchForRoleTextBox.Text = "%" + this.searchForRoleTextBox.Text.Replace(" ", "%") + "%";
            }
            if (this.searchForRoleTextBox.Text == "%%")
            {
                this.searchForRoleTextBox.Text = "%";
            }
            int dsply = 0;
            if (this.dsplySizeRoleComboBox.Text == ""
              || int.TryParse(this.dsplySizeRoleComboBox.Text, out dsply) == false)
            {
                this.dsplySizeRoleComboBox.Text = Global.myNwMainFrm.cmmnCode.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            //this.rolesListView.Height = this.panel8.Top - this.panel5.Bottom - 10;
            //this.rolePrvldgsListView.Height = this.panel8.Top - this.panel5.Bottom - 10;
            this.totl_roles = this.cmmnCode.Big_Val;
            this.is_last_role = false;
            this.getRolesPnlData();
            this.obey_roles_evnts = true;
        }

        private void getRolesPnlData()
        {
            this.updtRoleTotals();
            this.populateRoleLstVw();
            this.updtRoleNavLabels();
        }

        private void updtRoleTotals()
        {
            Global.myNwMainFrm.cmmnCode.navFuncts.FindNavigationIndices(int.Parse(this.dsplySizeRoleComboBox.Text), this.totl_roles);

            if (this.role_cur_indx >= Global.myNwMainFrm.cmmnCode.navFuncts.totalGroups)
            {
                this.role_cur_indx = Global.myNwMainFrm.cmmnCode.navFuncts.totalGroups - 1;
            }
            if (this.role_cur_indx < 0)
            {
                this.role_cur_indx = 0;
            }
            Global.myNwMainFrm.cmmnCode.navFuncts.currentNavigationIndex = this.role_cur_indx;
        }

        private void updtRoleNavLabels()
        {
            this.moveFirstRoleButton.Enabled = Global.myNwMainFrm.cmmnCode.navFuncts.moveFirstBtnStatus();
            this.movePreviousRoleButton.Enabled = Global.myNwMainFrm.cmmnCode.navFuncts.movePrevBtnStatus();
            this.moveNextRoleButton.Enabled = Global.myNwMainFrm.cmmnCode.navFuncts.moveNextBtnStatus();
            this.moveLastRoleButton.Enabled = Global.myNwMainFrm.cmmnCode.navFuncts.moveLastBtnStatus();
            this.positionRoleTextBox.Text = Global.myNwMainFrm.cmmnCode.navFuncts.displayedRecordsNumbers();
            if (this.is_last_role)
            {
                this.totalRecRoleLabel.Text = Global.myNwMainFrm.cmmnCode.navFuncts.totalRecordsLabel();
            }
            else
            {
                this.totalRecRoleLabel.Text = "of Total";
            }
        }

        private void populateRoleLstVw()
        {
            this.obey_roles_evnts = false;
            DataSet dtst = Global.get_Roles_Main(this.searchForRoleTextBox.Text,
              this.searchInRoleComboBox.Text, this.role_cur_indx,
              int.Parse(this.dsplySizeRoleComboBox.Text));
            this.rolesListView.Items.Clear();
            this.rolePrvldgsListView.Items.Clear();
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.last_role_num = Global.myNwMainFrm.cmmnCode.navFuncts.startIndex() + i;
                ListViewItem nwItm = new ListViewItem(new string[] { (Global.myNwMainFrm.cmmnCode.navFuncts.startIndex() + i).ToString(),
                dtst.Tables[0].Rows[i][1].ToString(), dtst.Tables[0].Rows[i][2].ToString(),
                dtst.Tables[0].Rows[i][3].ToString(), dtst.Tables[0].Rows[i][0].ToString()
        , dtst.Tables[0].Rows[i][4].ToString() });
                this.rolesListView.Items.Add(nwItm);
            }
            this.correctRoleNavLbls(dtst);
            this.obey_roles_evnts = true;
            if (this.rolesListView.Items.Count > 0)
            {
                this.rolesListView.Items[0].Selected = true;
            }
            this.obey_roles_evnts = true;
        }

        private void populateRolesPrvldgs()
        {
            this.obey_roles_evnts = false;
            this.rolePrvldgsListView.Items.Clear();
            if (this.rolesListView.SelectedItems.Count > 0)
            {
                DataSet dtst = Global.get_Roles_Prvldgs(int.Parse(this.rolesListView.SelectedItems[0].SubItems[4].Text));
                for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
                {
                    ListViewItem nwItm = new ListViewItem(new string[] { (i +1).ToString(),
                dtst.Tables[0].Rows[i][0].ToString(), dtst.Tables[0].Rows[i][1].ToString(),
                dtst.Tables[0].Rows[i][2].ToString(),
                dtst.Tables[0].Rows[i][3].ToString(),
                dtst.Tables[0].Rows[i][4].ToString(),
                dtst.Tables[0].Rows[i][5].ToString() });
                    this.rolePrvldgsListView.Items.Add(nwItm);
                }
                this.obey_roles_evnts = true;
                if (this.rolePrvldgsListView.Items.Count > 0)
                {
                    this.rolePrvldgsListView.Items[0].Selected = true;
                }
            }
            this.obey_roles_evnts = true;
        }

        private void correctRoleNavLbls(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.role_cur_indx == 0 && totlRecs == 0)
            {
                this.is_last_role = true;
                this.totl_roles = 0;
                this.last_role_num = 0;
                this.role_cur_indx = 0;
                this.updtRoleTotals();
                this.updtRoleNavLabels();
            }
            else if (this.totl_roles == Global.myNwMainFrm.cmmnCode.Big_Val
          && totlRecs < int.Parse(this.dsplySizeRoleComboBox.Text))
            {
                this.totl_roles = this.last_role_num;
                this.is_last_role = true;
                if (totlRecs == 0)
                {
                    this.role_cur_indx -= 1;
                    this.updtRoleTotals();
                    this.populateRoleLstVw();
                }
                else
                {
                    this.updtRoleTotals();
                }
            }
        }

        private void rolePnlNavButtons(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecRoleLabel.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.is_last_role = false;
                this.role_cur_indx = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.is_last_role = false;
                this.role_cur_indx -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.is_last_role = false;
                this.role_cur_indx += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.is_last_role = true;
                this.totl_roles = Global.get_total_Roles_Main(this.searchForRoleTextBox.Text, this.searchInRoleComboBox.Text);
                this.updtRoleTotals();
                this.role_cur_indx = Global.myNwMainFrm.cmmnCode.navFuncts.totalGroups - 1;
            }
            this.getRolesPnlData();
        }

        private void rolesListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.shdObeyRolesEvts() == false || this.rolesListView.SelectedItems.Count > 1)
            {
                return;
            }
            this.populateRolesPrvldgs();
        }

        private void refreshRoleButton_Click(object sender, EventArgs e)
        {
            this.loadRolesPanel();
            this.Refresh();
        }

        private void addRoleMainMenuItem_Click(object sender, EventArgs e)
        {
            this.addRoleButton_Click(this.addRoleButton, e);
        }

        private void editRoleMainMenuItem_Click(object sender, EventArgs e)
        {
            this.editRoleButton_Click(this.editRoleButton, e);
        }

        private void refreshRoleMainMenuItem_Click(object sender, EventArgs e)
        {
            this.loadRolesPanel();
        }

        private void recHstryRoleMainMenuItem_Click(object sender, EventArgs e)
        {
            if (this.rolesListView.SelectedItems.Count <= 0)
            {
                return;
            }
            Global.myNwMainFrm.cmmnCode.showRecHstry(Global.get_Roles_Rec_Hstry(int.Parse(this.rolesListView.SelectedItems[0].SubItems[4].Text)), 19);
        }

        private void vwSQLRoleMainMenuItem_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.cmmnCode.showSQL(Global.myNwMainFrm.roles_SQL, 18);
        }

        private void addRlPrvldgMenuItem_Click(object sender, EventArgs e)
        {
            this.addEditRoleButton_Click(this.addEditRoleButton, e);
        }

        private void refreshRlPrvldgMenuItem_Click(object sender, EventArgs e)
        {
            this.populateRolesPrvldgs();
        }

        private void recHstryRlPrvldgMenuItem_Click(object sender, EventArgs e)
        {
            if (this.rolePrvldgsListView.SelectedItems.Count <= 0)
            {
                return;
            }
            Global.myNwMainFrm.cmmnCode.showRecHstry(Global.get_Roles_Prvlg_Rec_Hstry(
              int.Parse(this.rolePrvldgsListView.SelectedItems[0].SubItems[5].Text),
              int.Parse(this.rolesListView.SelectedItems[0].SubItems[4].Text)), 19);
        }

        private void vwSQLRlPrvldgMenuItem_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.cmmnCode.showSQL(Global.myNwMainFrm.role_prvldgs_SQL, 18);
        }

        private void exptRolesMenuItem_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.cmmnCode.exprtToExcel(this.rolesListView);
        }

        private void exptRolePrvldgMenuItem_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.cmmnCode.exprtToExcel(this.rolePrvldgsListView);
        }

        private void positionRoleTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                this.rolePnlNavButtons(this.movePreviousRoleButton, ex);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
            {
                this.rolePnlNavButtons(this.moveNextRoleButton, ex);
            }
        }

        private void searchForRoleTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.refreshRoleButton_Click(this.refreshRoleButton, ex);
            }
        }
        #endregion

        #region "MODULES PANEL..."
        private bool shdObeyMdlsEvts()
        {
            return this.obey_mdls_evnts;
        }

        private void loadModulesPanel()
        {
            this.obey_mdls_evnts = false;
            if (this.searchInMdlComboBox.SelectedIndex < 0)
            {
                this.searchInMdlComboBox.SelectedIndex = 0;
            }
            if (this.searchForMdlTextBox.Text.Contains("%") == false)
            {
                this.searchForMdlTextBox.Text = "%" + this.searchForMdlTextBox.Text.Replace(" ", "%") + "%";
            }
            if (this.searchForMdlTextBox.Text == "%%")
            {
                this.searchForMdlTextBox.Text = "%";
            }
            int dsply = 0;
            if (this.dsplySizeMdlComboBox.Text == ""
              || int.TryParse(this.dsplySizeMdlComboBox.Text, out dsply) == false)
            {
                this.dsplySizeMdlComboBox.Text = Global.myNwMainFrm.cmmnCode.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            //this.modulesListView.Height = this.panel12.Top - this.panel11.Bottom - 10;
            //this.modulePrvldgListView.Height = this.panel12.Top - this.panel11.Bottom - 10;
            this.is_last_mdl = false;
            this.totl_mdls = this.cmmnCode.Big_Val;
            this.getMdlsPnlData();
            this.obey_mdls_evnts = true;
        }

        private void getMdlsPnlData()
        {
            this.updtMdlTotals();
            this.populateMdlLstVw();
            this.updtMdlNavLabels();
        }

        private void updtMdlTotals()
        {
            Global.myNwMainFrm.cmmnCode.navFuncts.FindNavigationIndices(long.Parse(this.dsplySizeMdlComboBox.Text), this.totl_mdls);

            if (this.mdl_cur_indx >= Global.myNwMainFrm.cmmnCode.navFuncts.totalGroups)
            {
                this.mdl_cur_indx = Global.myNwMainFrm.cmmnCode.navFuncts.totalGroups - 1;
            }
            if (this.mdl_cur_indx < 0)
            {
                this.mdl_cur_indx = 0;
            }
            Global.myNwMainFrm.cmmnCode.navFuncts.currentNavigationIndex = this.mdl_cur_indx;
        }

        private void updtMdlNavLabels()
        {
            this.moveFirstMdlButton.Enabled = Global.myNwMainFrm.cmmnCode.navFuncts.moveFirstBtnStatus();
            this.movePreviousMdlButton.Enabled = Global.myNwMainFrm.cmmnCode.navFuncts.movePrevBtnStatus();
            this.moveNextMdlButton.Enabled = Global.myNwMainFrm.cmmnCode.navFuncts.moveNextBtnStatus();
            this.moveLastMdlButton.Enabled = Global.myNwMainFrm.cmmnCode.navFuncts.moveLastBtnStatus();
            this.positionMdlTextBox.Text = Global.myNwMainFrm.cmmnCode.navFuncts.displayedRecordsNumbers();
            if (this.is_last_mdl == true)
            {
                this.totalRecMdlLabel.Text = Global.myNwMainFrm.cmmnCode.navFuncts.totalRecordsLabel();
            }
            else
            {
                this.totalRecMdlLabel.Text = "of Total";
            }
        }

        private void populateMdlLstVw()
        {
            this.obey_mdls_evnts = false;
            DataSet dtst = Global.get_Mdls_Main(this.searchForMdlTextBox.Text,
              this.searchInMdlComboBox.Text, this.mdl_cur_indx,
              int.Parse(this.dsplySizeMdlComboBox.Text));
            this.modulesListView.Items.Clear();
            this.modulePrvldgListView.Items.Clear();
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.last_mdl_num = Global.myNwMainFrm.cmmnCode.navFuncts.startIndex() + i;
                ListViewItem nwItm = new ListViewItem(new string[] { (Global.myNwMainFrm.cmmnCode.navFuncts.startIndex() + i).ToString(),
                dtst.Tables[0].Rows[i][1].ToString(), dtst.Tables[0].Rows[i][2].ToString(),
                dtst.Tables[0].Rows[i][3].ToString(), dtst.Tables[0].Rows[i][4].ToString(),
                dtst.Tables[0].Rows[i][0].ToString() });
                this.modulesListView.Items.Add(nwItm);
            }
            this.correctMdlNavLbls(dtst);
            this.obey_mdls_evnts = true;
            if (this.modulesListView.Items.Count > 0)
            {
                this.modulesListView.Items[0].Selected = true;
            }
            this.obey_mdls_evnts = true;
        }

        private void populateMdlsPrvldgs()
        {
            this.obey_mdls_evnts = false;
            this.modulePrvldgListView.Items.Clear();
            if (this.modulesListView.SelectedItems.Count > 0)
            {
                DataSet dtst = Global.get_Mdls_Prvldgs(int.Parse(this.modulesListView.SelectedItems[0].SubItems[5].Text));
                for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
                {
                    ListViewItem nwItm = new ListViewItem(new string[] { (i +1).ToString(),
                dtst.Tables[0].Rows[i][0].ToString(), dtst.Tables[0].Rows[i][1].ToString()});
                    this.modulePrvldgListView.Items.Add(nwItm);
                }
                this.obey_mdls_evnts = true;
                if (this.modulePrvldgListView.Items.Count > 0)
                {
                    this.modulePrvldgListView.Items[0].Selected = true;
                }
            }
            this.obey_mdls_evnts = true;
        }

        private void correctMdlNavLbls(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.mdl_cur_indx == 0 && totlRecs == 0)
            {
                this.is_last_mdl = true;
                this.totl_mdls = 0;
                this.last_mdl_num = 0;
                this.mdl_cur_indx = 0;
                this.updtMdlTotals();
                this.updtMdlNavLabels();
            }
            else if (this.totl_mdls == Global.myNwMainFrm.cmmnCode.Big_Val
          && totlRecs < int.Parse(this.dsplySizeMdlComboBox.Text))
            {
                this.totl_mdls = this.last_mdl_num;
                this.is_last_mdl = true;
                if (totlRecs == 0)
                {
                    this.mdl_cur_indx -= 1;
                    this.updtMdlTotals();
                    this.populateMdlLstVw();
                }
                else
                {
                    this.updtMdlTotals();
                }
            }
        }

        private void mdlPnlNavButtons(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecMdlLabel.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.is_last_mdl = false;
                this.mdl_cur_indx = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.is_last_mdl = false;
                this.mdl_cur_indx -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.is_last_mdl = false;
                this.mdl_cur_indx += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.is_last_mdl = true;
                this.totl_mdls = Global.get_total_Mdls_Main(this.searchForMdlTextBox.Text, this.searchInMdlComboBox.Text);
                this.updtMdlTotals();
                this.mdl_cur_indx = Global.myNwMainFrm.cmmnCode.navFuncts.totalGroups - 1;
            }
            this.getMdlsPnlData();
        }

        private void moulesListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.shdObeyMdlsEvts() == false || this.modulesListView.SelectedItems.Count > 1)
            {
                return;
            }
            this.populateMdlsPrvldgs();
        }

        private void refreshMdlMenuItem_Click(object sender, EventArgs e)
        {
            this.loadModulesPanel();
            this.Refresh();
        }

        private void vwSQLMdlMenuItem_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.cmmnCode.showSQL(Global.myNwMainFrm.mdls_SQL, 18);
        }

        private void refreshMdlPrvldgMenuItem_Click(object sender, EventArgs e)
        {
            this.populateMdlsPrvldgs();
            this.Refresh();
        }

        private void vwSqlMdlPrvldgMenuItem_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.cmmnCode.showSQL(Global.myNwMainFrm.mdl_prvldgs_SQL, 18);
        }

        private void refreshMdlButton_Click(object sender, EventArgs e)
        {
            this.loadModulesPanel();
            this.Refresh();
        }

        private void exptMdlMenuItem_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.cmmnCode.exprtToExcel(this.modulesListView);
        }

        private void exptMdlPrvldgMenuItem_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.cmmnCode.exprtToExcel(this.modulePrvldgListView);
        }

        private void positionMdlTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                this.mdlPnlNavButtons(this.movePreviousMdlButton, ex);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
            {
                this.mdlPnlNavButtons(this.moveNextMdlButton, ex);
            }
        }

        private void searchForMdlTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.refreshMdlButton_Click(this.refreshMdlButton, ex);
            }
        }
        #endregion

        #region "EXTRA INFO PANEL..."
        private bool shdObeyExtInfEvts()
        {
            return this.obey_extinf_evnts;
        }

        private void loadExtInfPanel()
        {
            this.obey_extinf_evnts = false;
            if (this.searchInExtInfComboBox.SelectedIndex < 0)
            {
                this.searchInExtInfComboBox.SelectedIndex = 0;
            }
            if (this.searchForExtInfTextBox.Text.Contains("%") == false)
            {
                this.searchForExtInfTextBox.Text = "%" + this.searchForExtInfTextBox.Text.Replace(" ", "%") + "%";
            }
            if (this.searchForExtInfTextBox.Text == "%%")
            {
                this.searchForExtInfTextBox.Text = "%";
            }
            int dsply = 0;
            if (this.dsplySizeExtInfComboBox.Text == ""
             || int.TryParse(this.dsplySizeExtInfComboBox.Text, out dsply) == false)
            {
                this.dsplySizeExtInfComboBox.Text = Global.myNwMainFrm.cmmnCode.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            //this.extInfoModuleListView.Height = this.panel19.Top - this.panel20.Bottom - 10;
            //this.extInfSubGroupsListView.Height = this.panel19.Top - this.panel20.Bottom - 10;
            //this.extInfLabelListView.Height = this.panel19.Top - this.panel20.Bottom - 10;
            this.is_last_extinf = false;
            this.totl_extinf = this.cmmnCode.Big_Val;
            this.getExtInfPnlData();
            this.obey_extinf_evnts = true;
        }

        private void getExtInfPnlData()
        {
            this.updtExtInfTotals();
            this.populateExtInfMdlLstVw();
            this.updtExtInfNavLabels();
        }

        private void updtExtInfTotals()
        {
            Global.myNwMainFrm.cmmnCode.navFuncts.FindNavigationIndices(long.Parse(this.dsplySizeExtInfComboBox.Text), this.totl_extinf);

            if (this.extinf_cur_indx >= Global.myNwMainFrm.cmmnCode.navFuncts.totalGroups)
            {
                this.extinf_cur_indx = Global.myNwMainFrm.cmmnCode.navFuncts.totalGroups - 1;
            }
            if (this.extinf_cur_indx < 0)
            {
                this.extinf_cur_indx = 0;
            }
            Global.myNwMainFrm.cmmnCode.navFuncts.currentNavigationIndex = this.extinf_cur_indx;
        }

        private void updtExtInfNavLabels()
        {
            this.moveFirstExtInfButton.Enabled = Global.myNwMainFrm.cmmnCode.navFuncts.moveFirstBtnStatus();
            this.movePreviousExtInfButton.Enabled = Global.myNwMainFrm.cmmnCode.navFuncts.movePrevBtnStatus();
            this.moveNextExtInfButton.Enabled = Global.myNwMainFrm.cmmnCode.navFuncts.moveNextBtnStatus();
            this.moveLastExtInfButton.Enabled = Global.myNwMainFrm.cmmnCode.navFuncts.moveLastBtnStatus();
            this.positionExtInfTextBox.Text = Global.myNwMainFrm.cmmnCode.navFuncts.displayedRecordsNumbers();
            if (this.is_last_extinf == true)
            {
                this.totalRecExtInfLabel.Text = Global.myNwMainFrm.cmmnCode.navFuncts.totalRecordsLabel();
            }
            else
            {
                this.totalRecExtInfLabel.Text = "of Total";
            }
        }

        private void populateExtInfMdlLstVw()
        {
            this.obey_extinf_evnts = false;
            DataSet dtst = Global.get_Mdls_Main(this.searchForExtInfTextBox.Text,
             this.searchInExtInfComboBox.Text, this.extinf_cur_indx,
             int.Parse(this.dsplySizeExtInfComboBox.Text));
            this.extInfoModuleListView.Items.Clear();
            this.extInfSubGroupsListView.Items.Clear();
            this.extInfLabelListView.Items.Clear();
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.last_extinf_num = Global.myNwMainFrm.cmmnCode.navFuncts.startIndex() + i;
                ListViewItem nwItm = new ListViewItem(new string[] { (Global.myNwMainFrm.cmmnCode.navFuncts.startIndex() + i).ToString(),
                dtst.Tables[0].Rows[i][1].ToString(), dtst.Tables[0].Rows[i][0].ToString() });
                this.extInfoModuleListView.Items.Add(nwItm);
            }
            this.correctExtInfNavLbls(dtst);
            this.obey_extinf_evnts = true;
            if (this.extInfoModuleListView.Items.Count > 0)
            {
                this.extInfoModuleListView.Items[0].Selected = true;
            }
            this.obey_extinf_evnts = true;
        }

        private void correctExtInfNavLbls(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.extinf_cur_indx == 0 && totlRecs == 0)
            {
                this.is_last_extinf = true;
                this.totl_extinf = 0;
                this.last_extinf_num = 0;
                this.extinf_cur_indx = 0;
                this.updtExtInfTotals();
                this.updtExtInfNavLabels();
            }
            else if (this.totl_extinf == Global.myNwMainFrm.cmmnCode.Big_Val
          && totlRecs < int.Parse(this.dsplySizeExtInfComboBox.Text))
            {
                this.totl_extinf = this.last_extinf_num;
                this.is_last_extinf = true;
                if (totlRecs == 0)
                {
                    this.extinf_cur_indx -= 1;
                    this.updtExtInfTotals();
                    this.populateExtInfMdlLstVw();
                }
                else
                {
                    this.updtExtInfTotals();
                }
            }
        }

        private void populateMdlsSubgroups()
        {
            this.obey_extinf_evnts = false;
            this.extInfSubGroupsListView.Items.Clear();
            if (this.extInfoModuleListView.SelectedItems.Count > 0)
            {
                DataSet dtst = Global.get_Mdls_SubGrps(int.Parse(this.extInfoModuleListView.SelectedItems[0].SubItems[2].Text));
                for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
                {
                    ListViewItem nwItm = new ListViewItem(new string[] { (i +1).ToString(),
                dtst.Tables[0].Rows[i][0].ToString(), dtst.Tables[0].Rows[i][1].ToString(),
     dtst.Tables[0].Rows[i][2].ToString(), dtst.Tables[0].Rows[i][3].ToString(),
     dtst.Tables[0].Rows[i][4].ToString()});
                    this.extInfSubGroupsListView.Items.Add(nwItm);
                }
                this.obey_extinf_evnts = true;
                if (this.extInfSubGroupsListView.Items.Count > 0)
                {
                    this.extInfSubGroupsListView.Items[0].Selected = true;
                }
            }
            this.obey_extinf_evnts = true;
        }

        private void populateSubgroupsExtInf()
        {
            this.obey_extinf_evnts = false;
            this.extInfLabelListView.Items.Clear();
            if (this.extInfSubGroupsListView.SelectedItems.Count > 0)
            {
                DataSet dtst = Global.get_SubGrps_ExtInf_Labels(int.Parse(this.extInfSubGroupsListView.SelectedItems[0].SubItems[5].Text));
                for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
                {
                    ListViewItem nwItm = new ListViewItem(new string[] { (i +1).ToString(),
                dtst.Tables[0].Rows[i][0].ToString(),
     Global.myNwMainFrm.cmmnCode.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][1].ToString()).ToString(),
     dtst.Tables[0].Rows[i][2].ToString(),
     dtst.Tables[0].Rows[i][4].ToString()});
                    this.extInfLabelListView.Items.Add(nwItm);
                }
                this.obey_extinf_evnts = true;
                if (this.extInfLabelListView.Items.Count > 0)
                {
                    this.extInfLabelListView.Items[0].Selected = true;
                }
            }
            this.obey_extinf_evnts = true;
        }

        private void extInfPnlNavButtons(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecMdlLabel.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.is_last_extinf = false;
                this.extinf_cur_indx = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.is_last_extinf = false;
                this.extinf_cur_indx -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.is_last_extinf = false;
                this.extinf_cur_indx += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.is_last_extinf = true;
                this.totl_extinf = Global.get_total_Mdls_Main(this.searchForExtInfTextBox.Text, this.searchInExtInfComboBox.Text);
                this.updtExtInfTotals();
                this.extinf_cur_indx = Global.myNwMainFrm.cmmnCode.navFuncts.totalGroups - 1;
            }
            this.getExtInfPnlData();
        }

        private void searchForExtInfTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.loadExtInfPanel();
            }
        }

        private void addEditExtInfMenuItem_Click(object sender, EventArgs e)
        {
            this.addEditExtInfButton_Click(this.addEditExtInfButton, e);
        }

        private void enableDisableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.enableDisableButton_Click(this.enableDisableButton, e);
        }

        private void deleteLaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.delLblButton_Click(this.delLblButton, e);
        }

        private void refreshExtInfLblMenuItem_Click(object sender, EventArgs e)
        {
            this.populateSubgroupsExtInf();
        }

        private void vwSQLExtInfLblMenuItem_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.cmmnCode.showSQL(Global.myNwMainFrm.extinf_SQL, 18);
        }

        private void recordHistoryExtInfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.extInfLabelListView.SelectedItems.Count <= 0)
            {
                return;
            }
            Global.myNwMainFrm.cmmnCode.showRecHstry(Global.get_ExtInfo_Rec_Hstry(
             long.Parse(this.extInfLabelListView.SelectedItems[0].SubItems[4].Text)), 19);
        }

        private void refreshExtInfMdlMenuItem_Click(object sender, EventArgs e)
        {
            this.loadExtInfPanel();
        }

        private void viewSQLExtInfMdlMenuItem_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.cmmnCode.showSQL(Global.myNwMainFrm.extinf_mdls_SQL, 18);
        }

        private void refreshSubGrpsMenuItem_Click(object sender, EventArgs e)
        {
            this.populateMdlsSubgroups();
        }

        private void vwSQLSubGrpsMenuItem_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.cmmnCode.showSQL(Global.myNwMainFrm.mdl_subgroups_SQL, 18);
        }

        private void extInfoModuleListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.shdObeyExtInfEvts() == false || this.extInfoModuleListView.SelectedItems.Count > 1)
            {
                return;
            }
            this.populateMdlsSubgroups();
        }

        private void extInfSubGroupsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.shdObeyExtInfEvts() == false || this.extInfSubGroupsListView.SelectedItems.Count > 1)
            {
                return;
            }
            this.populateSubgroupsExtInf();
        }

        private void refreshExtInfoButton_Click(object sender, EventArgs e)
        {
            this.loadExtInfPanel();
        }

        private void exptExtInfMenuItem_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.cmmnCode.exprtToExcel(this.extInfoModuleListView);
        }

        private void exptSubGrpMenuItem_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.cmmnCode.exprtToExcel(this.extInfSubGroupsListView);
        }

        private void exptInfLblMenuItem_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.cmmnCode.exprtToExcel(this.extInfLabelListView);
        }

        private void positionExtInfTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                this.extInfPnlNavButtons(this.movePreviousExtInfButton, ex);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
            {
                this.extInfPnlNavButtons(this.moveNextExtInfButton, ex);
            }
        }
        #endregion

        #region "POLICY PANEL..."
        private void loadPolicyPanel()
        {
            this.obey_pcly_evnts = false;
            if (this.searchInPlcyComboBox.SelectedIndex < 0)
            {
                this.searchInPlcyComboBox.SelectedIndex = 0;
            }
            if (this.searchForPlcyTextBox.Text.Contains("%") == false)
            {
                this.searchForPlcyTextBox.Text = "%" + this.searchForPlcyTextBox.Text.Replace(" ", "%") + "%";
            }
            if (this.searchForPlcyTextBox.Text == "%%")
            {
                this.searchForPlcyTextBox.Text = "%";
            }
            //this.auditTblsListView.Height = this.policyPanel.Bottom - this.label26.Bottom - 15;
            this.is_last_plcy = false;
            this.totl_plcy = this.cmmnCode.Big_Val;
            this.getPlcyPnlData();
            this.obey_pcly_evnts = true;
        }

        private void getPlcyPnlData()
        {
            this.updtPlcyTotals();
            this.populatePlcyDetails();
            this.updtPlcyNavLabels();
        }

        private void updtPlcyTotals()
        {
            Global.myNwMainFrm.cmmnCode.navFuncts.FindNavigationIndices(1, this.totl_plcy);

            if (this.plcy_cur_indx >= Global.myNwMainFrm.cmmnCode.navFuncts.totalGroups)
            {
                this.plcy_cur_indx = Global.myNwMainFrm.cmmnCode.navFuncts.totalGroups - 1;
            }
            if (this.plcy_cur_indx < 0)
            {
                this.plcy_cur_indx = 0;
            }
            Global.myNwMainFrm.cmmnCode.navFuncts.currentNavigationIndex = this.plcy_cur_indx;
        }

        private void updtPlcyNavLabels()
        {
            this.moveFirstPlcyButton.Enabled = Global.myNwMainFrm.cmmnCode.navFuncts.moveFirstBtnStatus();
            this.movePreviousPlcyButton.Enabled = Global.myNwMainFrm.cmmnCode.navFuncts.movePrevBtnStatus();
            this.moveNextPlcyButton.Enabled = Global.myNwMainFrm.cmmnCode.navFuncts.moveNextBtnStatus();
            this.moveLastPlcyButton.Enabled = Global.myNwMainFrm.cmmnCode.navFuncts.moveLastBtnStatus();
            this.positionPlcyTextBox.Text = Global.myNwMainFrm.cmmnCode.navFuncts.displayedRecordsNumbers();
            if (is_last_plcy == true ||
             this.totl_plcy != Global.myNwMainFrm.cmmnCode.Big_Val)
            {
                this.totalRecPlcyLabel.Text = Global.myNwMainFrm.cmmnCode.navFuncts.totalRecordsLabel();
            }
            else
            {
                this.totalRecPlcyLabel.Text = "of Total";
            }
        }

        private void clearPlcyForm()
        {
            this.obey_pcly_evnts = false;
            this.add_plcy = false;
            this.edit_plcy = false;
            this.savePlcyButton.Enabled = false;
            this.editPlcyButton.Enabled = this.editPlcys;
            this.addPlcyButton.Enabled = this.addPlcys;
            this.plcyIDTextBox.Text = "-1";
            this.policyNmTextBox.Text = "";
            this.policyNmTextBox.ReadOnly = true;
            this.policyNmTextBox.BackColor = Color.WhiteSmoke;
            this.isDefltYesCheckBox.Checked = false;
            this.isDefltNoCheckBox.Checked = false;

            this.minLenPswdNumericUpDown.Value = 0;
            this.maxLenPswdNmUpDown.Value = 0;
            this.oldPswdCntNmUpDown.Value = 0;
            this.allwUnmYesCheckBox.Checked = false;
            this.allwUnmNoCheckBox.Checked = false;
            this.allwRptnYesCheckBox.Checked = false;
            this.allwRptnNoCheckBox.Checked = false;
            this.faildLgnCntNmUpDown.Value = 0;
            this.autoUnlkTmNmUpDown.Value = 0;

            this.expryDaysNmUpDown.Value = 0;
            this.mxNoRecsNmUpDown.Value = 0;
            this.sessionNumUpDown.Value = 0;
            this.capsNoCheckBox.Checked = false;
            this.capsYesCheckBox.Checked = false;
            this.smallNoCheckBox.Checked = false;
            this.smallYesCheckBox.Checked = false;
            this.digitsNoCheckBox.Checked = false;
            this.digitsYesCheckBox.Checked = false;
            this.wildNoCheckBox.Checked = false;
            this.wildYesCheckBox.Checked = false;
            this.combinatnsComboBox.SelectedItem = "NONE";
            this.obey_pcly_evnts = true;
        }

        private void newPlcyForm()
        {
            this.obey_pcly_evnts = false;
            this.add_plcy = true;
            this.edit_plcy = false;
            this.savePlcyButton.Enabled = true;
            this.editPlcyButton.Enabled = false;
            this.addPlcyButton.Enabled = false;
            this.plcyIDTextBox.Text = "-1";
            this.policyNmTextBox.Text = "";
            this.policyNmTextBox.ReadOnly = false;
            this.policyNmTextBox.BackColor = Color.FromArgb(255, 255, 118);
            this.isDefltYesCheckBox.Checked = false;
            this.isDefltNoCheckBox.Checked = true;

            this.minLenPswdNumericUpDown.Value = 7;
            this.maxLenPswdNmUpDown.Value = 25;
            this.oldPswdCntNmUpDown.Value = 10;
            this.allwUnmYesCheckBox.Checked = false;
            this.allwUnmNoCheckBox.Checked = true;
            this.allwRptnYesCheckBox.Checked = true;
            this.allwRptnNoCheckBox.Checked = false;
            this.faildLgnCntNmUpDown.Value = 3;
            this.autoUnlkTmNmUpDown.Value = 60;

            this.expryDaysNmUpDown.Value = 90;
            this.mxNoRecsNmUpDown.Value = 25;
            this.sessionNumUpDown.Value = 300;
            this.sessionNumUpDown.Increment = 1;
            this.sessionNumUpDown.ReadOnly = false;

            this.capsNoCheckBox.Checked = false;
            this.capsYesCheckBox.Checked = true;
            this.smallNoCheckBox.Checked = false;
            this.smallYesCheckBox.Checked = true;
            this.digitsNoCheckBox.Checked = false;
            this.digitsYesCheckBox.Checked = true;
            this.wildNoCheckBox.Checked = false;
            this.wildYesCheckBox.Checked = true;
            this.combinatnsComboBox.SelectedItem = "ANY 3";
            this.populatePlcyAdtTbls();
            this.obey_pcly_evnts = true;
        }

        private void populatePlcyDetails()
        {
            this.clearPlcyForm();
            this.obey_pcly_evnts = false;
            DataSet dtst = Global.get_Plcys(this.searchForPlcyTextBox.Text,
              this.searchInPlcyComboBox.Text, this.plcy_cur_indx,
              1);
            this.auditTblsListView.Items.Clear();
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.last_plcy_num = Global.myNwMainFrm.cmmnCode.navFuncts.startIndex() + i;
                this.plcyIDTextBox.Text = dtst.Tables[0].Rows[i][0].ToString();
                this.policyNmTextBox.Text = dtst.Tables[0].Rows[i][1].ToString();
                bool test = false;
                if (bool.TryParse(dtst.Tables[0].Rows[i][10].ToString(), out test) == true)
                {
                    this.isDefltYesCheckBox.Checked = test;
                    this.isDefltNoCheckBox.Checked = !test;
                }
                this.minLenPswdNumericUpDown.Value = Decimal.Parse(dtst.Tables[0].Rows[i][12].ToString());
                this.maxLenPswdNmUpDown.Value = Decimal.Parse(dtst.Tables[0].Rows[i][13].ToString());
                this.oldPswdCntNmUpDown.Value = Decimal.Parse(dtst.Tables[0].Rows[i][11].ToString());
                if (bool.TryParse(dtst.Tables[0].Rows[i][16].ToString(), out test) == true)
                {
                    this.allwUnmYesCheckBox.Checked = test;
                    this.allwUnmNoCheckBox.Checked = !test;
                }
                if (bool.TryParse(dtst.Tables[0].Rows[i][15].ToString(), out test) == true)
                {
                    this.allwRptnYesCheckBox.Checked = test;
                    this.allwRptnNoCheckBox.Checked = !test;
                }
                this.faildLgnCntNmUpDown.Value = Decimal.Parse(dtst.Tables[0].Rows[i][2].ToString()); ;
                this.autoUnlkTmNmUpDown.Value = Decimal.Parse(dtst.Tables[0].Rows[i][4].ToString()); ;

                this.expryDaysNmUpDown.Value = Decimal.Parse(dtst.Tables[0].Rows[i][3].ToString()); ;
                this.mxNoRecsNmUpDown.Value = Decimal.Parse(dtst.Tables[0].Rows[i][14].ToString()); ;
                this.sessionNumUpDown.Value = Decimal.Parse(dtst.Tables[0].Rows[i][17].ToString());
                if (bool.TryParse(dtst.Tables[0].Rows[i][5].ToString(), out test) == true)
                {
                    this.capsYesCheckBox.Checked = test;
                    this.capsNoCheckBox.Checked = !test;
                }
                if (bool.TryParse(dtst.Tables[0].Rows[i][6].ToString(), out test) == true)
                {
                    this.smallYesCheckBox.Checked = test;
                    this.smallNoCheckBox.Checked = !test;
                }
                if (bool.TryParse(dtst.Tables[0].Rows[i][7].ToString(), out test) == true)
                {
                    this.digitsYesCheckBox.Checked = test;
                    this.digitsNoCheckBox.Checked = !test;
                }
                if (bool.TryParse(dtst.Tables[0].Rows[i][8].ToString(), out test) == true)
                {
                    this.wildYesCheckBox.Checked = test;
                    this.wildNoCheckBox.Checked = !test;
                }
                this.combinatnsComboBox.SelectedItem = dtst.Tables[0].Rows[i][9].ToString();
            }
            this.sessionNumUpDown.Increment = 0;
            this.sessionNumUpDown.ReadOnly = true;
            this.correctPlcyNavLbls(dtst);
            this.obey_pcly_evnts = true;
            this.populatePlcyAdtTbls();
        }

        private void populatePlcyAdtTbls()
        {
            this.obey_pcly_evnts = false;
            this.auditTblsListView.Items.Clear();
            if (this.plcyIDTextBox.Text != "")
            {
                DataSet dtst = Global.get_Plcy_Mdls(int.Parse(this.plcyIDTextBox.Text));
                for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
                {
                    ListViewItem nwItm = new ListViewItem(new string[] { (i +1).ToString(),
                dtst.Tables[0].Rows[i][1].ToString(), dtst.Tables[0].Rows[i][2].ToString()
                    , dtst.Tables[0].Rows[i][3].ToString()
                    , dtst.Tables[0].Rows[i][4].ToString()
                    , dtst.Tables[0].Rows[i][0].ToString()});
                    bool test;
                    if (bool.TryParse(dtst.Tables[0].Rows[i][3].ToString(), out test) == true)
                    {
                        nwItm.Checked = test;
                    }
                    this.auditTblsListView.Items.Add(nwItm);
                }
                this.obey_pcly_evnts = true;
                if (this.auditTblsListView.Items.Count > 0)
                {
                    this.auditTblsListView.Items[0].Selected = true;
                }
            }
            this.obey_pcly_evnts = true;
        }

        private void plcyPnlNavButtons(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecPlcyLabel.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.is_last_plcy = false;
                this.plcy_cur_indx = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.is_last_plcy = false;
                this.plcy_cur_indx -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.is_last_plcy = false;
                this.plcy_cur_indx += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.is_last_plcy = true;
                this.totl_plcy = Global.get_total_Plcy(this.searchForPlcyTextBox.Text, this.searchInPlcyComboBox.Text);
                this.updtPlcyTotals();
                this.plcy_cur_indx = Global.myNwMainFrm.cmmnCode.navFuncts.totalGroups - 1;
            }
            this.getPlcyPnlData();
        }

        private void correctPlcyNavLbls(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.plcy_cur_indx == 0 && totlRecs == 0)
            {
                this.is_last_plcy = true;
                this.totl_plcy = 0;
                this.last_plcy_num = 0;
                this.plcy_cur_indx = 0;
                this.updtPlcyTotals();
                this.updtPlcyNavLabels();
            }
            else if (this.totl_plcy == Global.myNwMainFrm.cmmnCode.Big_Val
          && totlRecs < 1)
            {
                this.totl_plcy = this.last_plcy_num;
                this.is_last_plcy = true;
                if (totlRecs == 0)
                {
                    this.plcy_cur_indx -= 1;
                    this.updtPlcyTotals();
                    this.populatePlcyDetails();
                }
                else
                {
                    this.updtPlcyTotals();
                }
            }
        }

        private bool shlObeyPcyEvnt()
        {
            if (this.obey_pcly_evnts == false)
            {
                return false;
            }
            if ((this.add_plcy == false && this.edit_plcy == false))
            {
                this.loadPolicyPanel();
                return false;
            }
            return true;
        }

        private void addPlcyButton_Click(object sender, EventArgs e)
        {
            if (Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[12]) == false)
            {
                Global.myNwMainFrm.cmmnCode.showMsg("You don't have permission to perform" +
                  " this action!\nContact your System Administrator!", 0);
                return;
            }
            this.newPlcyForm();
        }

        private bool doesChkdRqrmntsMeetCmbntn()
        {
            int cntr = 0;
            if (this.capsYesCheckBox.Checked == true)
            {
                cntr += 1;
            }
            if (this.smallYesCheckBox.Checked == true)
            {
                cntr += 1;
            }
            if (this.wildYesCheckBox.Checked == true)
            {
                cntr += 1;
            }
            if (this.digitsYesCheckBox.Checked == true)
            {
                cntr += 1;
            }
            if (cntr == 0 && this.combinatnsComboBox.Text != "NONE")
            {
                return false;
            }
            else if (cntr == 1 && this.combinatnsComboBox.Text != "ANY 1")
            {
                return false;
            }
            else if (cntr == 2 && this.combinatnsComboBox.Text != "ANY 1" &&
              this.combinatnsComboBox.Text != "ANY 2")
            {
                return false;
            }
            else if (cntr == 3 && this.combinatnsComboBox.Text != "ANY 1" &&
      this.combinatnsComboBox.Text != "ANY 2" &&
      this.combinatnsComboBox.Text != "ANY 3")
            {
                return false;
            }
            else if (cntr == 4 && this.combinatnsComboBox.Text == "NONE")
            {
                return false;
            }
            return true;
        }

        private void savePlcyButton_Click(object sender, EventArgs e)
        {
            if (this.add_plcy == true)
            {
                if (Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[12]) == false)
                {
                    Global.myNwMainFrm.cmmnCode.showMsg("You don't have permission to perform" +
                      " this action!\nContact your System Administrator!", 0);
                    return;
                }
            }
            else
            {
                if (Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[13]) == false)
                {
                    Global.myNwMainFrm.cmmnCode.showMsg("You don't have permission to perform" +
                      " this action!\nContact your System Administrator!", 0);
                    return;
                }
            }
            if (this.policyNmTextBox.Text == "")
            {
                MessageBox.Show("Please enter a policy name!", "Rhomicom Message!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            long oldID = Global.getPlcyID(this.policyNmTextBox.Text);
            if (oldID > 0 && this.add_plcy == true)
            {
                MessageBox.Show("Policy name is already in use!", "Rhomicom Message!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (oldID > 0 && this.edit_plcy == true && oldID.ToString() != this.plcyIDTextBox.Text)
            {
                MessageBox.Show("New Policy name is already in use!", "Rhomicom Message!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (this.doesChkdRqrmntsMeetCmbntn() == false)
            {
                MessageBox.Show("The selected requirement combination '" + this.combinatnsComboBox.Text
                + "' \ndoes not match the checked boxes above it!", "Rhomicom Message!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (this.add_plcy == true)
            {
                if (this.isDefltYesCheckBox.Checked == true)
                {
                    Global.undefaultAllPlcys();
                }
                Global.createPolicy(this.policyNmTextBox.Text, (int)this.faildLgnCntNmUpDown.Value,
                  (int)this.expryDaysNmUpDown.Value, (int)this.autoUnlkTmNmUpDown.Value, this.capsYesCheckBox.Checked,
                  this.smallYesCheckBox.Checked, this.digitsYesCheckBox.Checked, this.wildYesCheckBox.Checked,
                  this.combinatnsComboBox.Text, this.isDefltYesCheckBox.Checked, (int)this.oldPswdCntNmUpDown.Value,
                  (int)this.minLenPswdNumericUpDown.Value, (int)this.maxLenPswdNmUpDown.Value, (int)this.mxNoRecsNmUpDown.Value,
                  this.allwRptnYesCheckBox.Checked, this.allwUnmYesCheckBox.Checked, this.sessionNumUpDown.Value);
                this.savePlcyButton.Enabled = true;
                this.add_plcy = false;
                this.edit_plcy = true;
                this.editPlcyButton.Enabled = true;
                this.addPlcyButton.Enabled = true;
                System.Windows.Forms.Application.DoEvents();
                this.plcyIDTextBox.Text = Global.getPlcyID(this.policyNmTextBox.Text).ToString();
            }
            else if (this.edit_plcy == true)
            {
                if (this.isDefltYesCheckBox.Checked == true)
                {
                    Global.undefaultAllPlcys();
                }
                Global.updatePlcy(int.Parse(this.plcyIDTextBox.Text), this.policyNmTextBox.Text, (int)this.faildLgnCntNmUpDown.Value,
                  (int)this.expryDaysNmUpDown.Value, (int)this.autoUnlkTmNmUpDown.Value, this.capsYesCheckBox.Checked,
                  this.smallYesCheckBox.Checked, this.digitsYesCheckBox.Checked, this.wildYesCheckBox.Checked,
                  this.combinatnsComboBox.Text, this.isDefltYesCheckBox.Checked, (int)this.oldPswdCntNmUpDown.Value,
                  (int)this.minLenPswdNumericUpDown.Value, (int)this.maxLenPswdNmUpDown.Value, (int)this.mxNoRecsNmUpDown.Value,
                  this.allwRptnYesCheckBox.Checked, this.allwUnmYesCheckBox.Checked, this.sessionNumUpDown.Value);
                this.savePlcyButton.Enabled = false;
                this.edit_plcy = false;
                this.editPlcyButton.Enabled = true;
                this.addPlcyButton.Enabled = true;
                this.loadPolicyPanel();
            }
        }

        private void isDefltYesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shlObeyPcyEvnt() == false)
            {
                return;
            }
            this.isDefltNoCheckBox.Checked = !this.isDefltYesCheckBox.Checked;
        }

        private void isDefltNoCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shlObeyPcyEvnt() == false)
            {
                return;
            }
            this.isDefltYesCheckBox.Checked = !this.isDefltNoCheckBox.Checked;
        }

        private void allwUnmYesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shlObeyPcyEvnt() == false)
            {
                return;
            }
            this.allwUnmNoCheckBox.Checked = !this.allwUnmYesCheckBox.Checked;
        }

        private void allwUnmNoCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shlObeyPcyEvnt() == false)
            {
                return;
            }
            this.allwUnmYesCheckBox.Checked = !this.allwUnmNoCheckBox.Checked;
        }

        private void allwRptnYesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shlObeyPcyEvnt() == false)
            {
                return;
            }
            this.allwRptnNoCheckBox.Checked = !this.allwRptnYesCheckBox.Checked;
        }

        private void allwRptnNoCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shlObeyPcyEvnt() == false)
            {
                return;
            }
            this.allwRptnYesCheckBox.Checked = !this.allwRptnNoCheckBox.Checked;
        }

        private void capsYesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shlObeyPcyEvnt() == false)
            {
                return;
            }
            this.capsNoCheckBox.Checked = !this.capsYesCheckBox.Checked;
        }

        private void capsNoCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shlObeyPcyEvnt() == false)
            {
                return;
            }
            this.capsYesCheckBox.Checked = !this.capsNoCheckBox.Checked;
        }

        private void smallYesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shlObeyPcyEvnt() == false)
            {
                return;
            }
            this.smallNoCheckBox.Checked = !this.smallYesCheckBox.Checked;
        }

        private void smallNoCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shlObeyPcyEvnt() == false)
            {
                return;
            }
            this.smallYesCheckBox.Checked = !this.smallNoCheckBox.Checked;
        }

        private void digitsYesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shlObeyPcyEvnt() == false)
            {
                return;
            }
            this.digitsNoCheckBox.Checked = !this.digitsYesCheckBox.Checked;
        }

        private void digitsNoCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shlObeyPcyEvnt() == false)
            {
                return;
            }
            this.digitsYesCheckBox.Checked = !this.digitsNoCheckBox.Checked;
        }

        private void wildYesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shlObeyPcyEvnt() == false)
            {
                return;
            }
            this.wildNoCheckBox.Checked = !this.wildYesCheckBox.Checked;
        }

        private void wildNoCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shlObeyPcyEvnt() == false)
            {
                return;
            }
            this.wildYesCheckBox.Checked = !this.wildNoCheckBox.Checked;
        }

        private void refreshPlcyButton_Click(object sender, EventArgs e)
        {
            this.loadPolicyPanel();
            this.Refresh();
        }

        private void editPlcyButton_Click(object sender, EventArgs e)
        {
            if (Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[13]) == false)
            {
                Global.myNwMainFrm.cmmnCode.showMsg("You don't have permission to perform" +
                  " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.plcyIDTextBox.Text == "" || this.plcyIDTextBox.Text == "-1")
            {
                Global.myNwMainFrm.cmmnCode.showMsg("There's nothing to edit!", 0);
                return;
            }
            this.edit_plcy = true;
            this.add_plcy = false;
            this.policyNmTextBox.ReadOnly = false;
            this.policyNmTextBox.BackColor = Color.FromArgb(255, 255, 118);
            this.savePlcyButton.Enabled = true;
            this.editPlcyButton.Enabled = false;
            this.addPlcyButton.Enabled = false;
            //this.sessionNumUpDown.Value = 300;
            this.sessionNumUpDown.Increment = 1;
            this.sessionNumUpDown.ReadOnly = false;
        }

        private void minLenPswdNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (this.shlObeyPcyEvnt() == false)
            {
                return;
            }
        }

        private void maxLenPswdNmUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (this.shlObeyPcyEvnt() == false)
            {
                return;
            }
        }

        private void oldPswdCntNmUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (this.shlObeyPcyEvnt() == false)
            {
                return;
            }
        }

        private void faildLgnCntNmUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (this.shlObeyPcyEvnt() == false)
            {
                return;
            }
        }

        private void autoUnlkTmNmUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (this.shlObeyPcyEvnt() == false)
            {
                return;
            }
        }

        private void expryDaysNmUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (this.shlObeyPcyEvnt() == false)
            {
                return;
            }
        }

        private void mxNoRecsNmUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (this.shlObeyPcyEvnt() == false)
            {
                return;
            }
        }

        private void combinatnsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.shlObeyPcyEvnt() == false)
            {
                return;
            }
        }

        private void vwSQLPlcyButton_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.cmmnCode.showSQL(Global.myNwMainFrm.plcys_SQL, 18);
        }

        private void recHstryPlcyButton_Click(object sender, EventArgs e)
        {
            if (this.plcyIDTextBox.Text == "-1"
              || this.plcyIDTextBox.Text == "")
            {
                return;
            }
            Global.myNwMainFrm.cmmnCode.showRecHstry(Global.get_Plcys_Rec_Hstry(int.Parse(this.plcyIDTextBox.Text)), 19);
        }

        private void editPlcyMdlMenuItem_Click(object sender, EventArgs e)
        {
            if (Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[13]) == false)
            {
                Global.myNwMainFrm.cmmnCode.showMsg("You don't have permission to perform" +
                  " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (auditTblsListView.SelectedItems.Count <= 0)
            {
                Global.myNwMainFrm.cmmnCode.showMsg("Please select an Item First!", 0);
                return;
            }
            editPlcyMdlsDiag nwDiag = new editPlcyMdlsDiag();
            nwDiag.plcyNameTextBox.Text = this.policyNmTextBox.Text;
            nwDiag.plcyID = int.Parse(this.plcyIDTextBox.Text);
            nwDiag.mdlID = int.Parse(this.auditTblsListView.SelectedItems[0].SubItems[5].Text);
            nwDiag.mdlNameTextBox.Text = this.auditTblsListView.SelectedItems[0].SubItems[1].Text;
            bool test_rn = false;
            if (bool.TryParse(this.auditTblsListView.SelectedItems[0].SubItems[3].Text,
              out test_rn) == true)
            {
                nwDiag.enblTrknYesCheckBox.Checked = test_rn;
                nwDiag.enblTrknNoCheckBox.Checked = !test_rn;
            }
            else
            {
                nwDiag.enblTrknYesCheckBox.Checked = false;
                nwDiag.enblTrknNoCheckBox.Checked = false;
            }
            nwDiag.actions_brght = this.auditTblsListView.SelectedItems[0].SubItems[4].Text;
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
                string[] ary = Global.get_Plcy_Prtclr_Mdl(nwDiag.plcyID, nwDiag.mdlID);
                if (ary.Length >= 2)
                {
                    this.auditTblsListView.SelectedItems[0].SubItems[3].Text = ary[0];
                    this.auditTblsListView.SelectedItems[0].SubItems[4].Text = ary[1];
                    this.obey_pcly_evnts = false;
                    if (ary[0] == "True")
                    {
                        this.auditTblsListView.SelectedItems[0].Checked = true;
                    }
                    else
                    {
                        this.auditTblsListView.SelectedItems[0].Checked = false;
                    }
                    this.obey_pcly_evnts = true;
                }
            }
        }

        private void refreshPlcyMdlsMenuItem_Click(object sender, EventArgs e)
        {
            this.populatePlcyAdtTbls();
        }

        private void recHstryPlcyMdlsMenuItem_Click(object sender, EventArgs e)
        {
            if (this.auditTblsListView.SelectedItems.Count <= 0)
            {
                return;
            }
            Global.myNwMainFrm.cmmnCode.showRecHstry(Global.get_Plcy_Mdls_Rec_Hstry(int.Parse(this.plcyIDTextBox.Text),
              int.Parse(this.auditTblsListView.SelectedItems[0].SubItems[5].Text)), 19);
        }

        private void vwSQLPlcyMdlsMenuItem_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.cmmnCode.showSQL(Global.myNwMainFrm.plcy_Adt_Tbls_SQL, 18);
        }

        private void auditTblsListView_ItemChecked(object sender,
          System.Windows.Forms.ItemCheckedEventArgs e)
        {
            if (this.shlObeyPcyEvnt() == false)
            {
                return;
            }
            if (Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[13]) == false)
            {
                Global.myNwMainFrm.cmmnCode.showMsg("You don't have permission to perform" +
                  " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (e != null)
            {
                if (e.Item.Checked == true)
                {
                    if ((this.add_plcy == true && this.plcyIDTextBox.Text == "-1")
                  || (this.add_plcy == true && this.plcyIDTextBox.Text == ""))
                    {
                        e.Item.Checked = false;
                        return;
                    }

                    if (Global.hasPlcyEvrHdThsMdl(int.Parse(this.plcyIDTextBox.Text),
                      int.Parse(e.Item.SubItems[5].Text)) == false)
                    {
                        Global.asgnMdlToPlcy(int.Parse(this.plcyIDTextBox.Text),
                          int.Parse(e.Item.SubItems[5].Text), true, "UPDATE STATEMENTS, DELETE STATEMENTS");
                    }
                    else
                    {
                        Global.enbldisableTracking(int.Parse(this.plcyIDTextBox.Text),
                        int.Parse(e.Item.SubItems[5].Text), true);
                    }
                }
                else
                {
                    if (Global.hasPlcyEvrHdThsMdl(int.Parse(this.plcyIDTextBox.Text),
                      int.Parse(e.Item.SubItems[5].Text)) == true)
                    {
                        Global.enbldisableTracking(int.Parse(this.plcyIDTextBox.Text),
                        int.Parse(e.Item.SubItems[5].Text), false);
                    }
                }
                if (e.Item.Selected == false)
                {
                    e.Item.Selected = true;
                }
                string[] myStr = Global.get_Plcy_Prtclr_Mdl(int.Parse(this.plcyIDTextBox.Text),
                int.Parse(e.Item.SubItems[5].Text));
                if (myStr.Length >= 2)
                {
                    e.Item.SubItems[3].Text = myStr[0];
                    e.Item.SubItems[4].Text = myStr[1];
                }
            }
        }

        private void exptPlcyMenuItem_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.cmmnCode.exprtToExcel(this.auditTblsListView);
        }

        private void searchForPlcyTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.refreshPlcyButton_Click(this.refreshPlcyButton, ex);
            }
        }

        private void positionPlcyTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                this.plcyPnlNavButtons(this.movePreviousPlcyButton, ex);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
            {
                this.plcyPnlNavButtons(this.moveNextPlcyButton, ex);
            }
        }
        #endregion

        #region "EMAIL SERVER PANEL..."
        private void loadEmailSrvrPanel()
        {
            this.obey_eml_srvs_evnts = false;
            if (this.searchInEmlSvrComboBox.SelectedIndex < 0)
            {
                this.searchInEmlSvrComboBox.SelectedIndex = 1;
            }
            if (this.searchForEmlSvrTextBox.Text.Contains("%") == false)
            {
                this.searchForEmlSvrTextBox.Text = "%" + this.searchForEmlSvrTextBox.Text.Replace(" ", "%") + "%";
            }
            if (this.searchForEmlSvrTextBox.Text == "%%")
            {
                this.searchForEmlSvrTextBox.Text = "%";
            }
            this.is_last_email = false;
            this.totl_eml_srvs = this.cmmnCode.Big_Val;
            this.getEmlPnlData();
            this.obey_eml_srvs_evnts = true;
        }

        private void getEmlPnlData()
        {
            this.updtEmlSvrTotals();
            this.populateEmlSvrDetails();
            this.updtEmlSvrNavLabels();
        }

        private void updtEmlSvrTotals()
        {
            Global.myNwMainFrm.cmmnCode.navFuncts.FindNavigationIndices(1, this.totl_eml_srvs);

            if (this.email_cur_indx >= Global.myNwMainFrm.cmmnCode.navFuncts.totalGroups)
            {
                this.email_cur_indx = Global.myNwMainFrm.cmmnCode.navFuncts.totalGroups - 1;
            }
            if (this.email_cur_indx < 0)
            {
                this.email_cur_indx = 0;
            }
            Global.myNwMainFrm.cmmnCode.navFuncts.currentNavigationIndex = this.email_cur_indx;
        }

        private void updtEmlSvrNavLabels()
        {
            this.moveFirstEmlSvrButton.Enabled = Global.myNwMainFrm.cmmnCode.navFuncts.moveFirstBtnStatus();
            this.movePreviousEmlSvrButton.Enabled = Global.myNwMainFrm.cmmnCode.navFuncts.movePrevBtnStatus();
            this.moveNextEmlSvrButton.Enabled = Global.myNwMainFrm.cmmnCode.navFuncts.moveNextBtnStatus();
            this.moveLastEmlSvrButton.Enabled = Global.myNwMainFrm.cmmnCode.navFuncts.moveLastBtnStatus();
            this.positionEmlSvrTextBox.Text = Global.myNwMainFrm.cmmnCode.navFuncts.displayedRecordsNumbers();
            if (this.is_last_email == true)
            {
                this.totalRecEmlSvrLabel.Text = Global.myNwMainFrm.cmmnCode.navFuncts.totalRecordsLabel();
            }
            else
            {
                this.totalRecEmlSvrLabel.Text = "of Total";
            }
        }

        private void clearEmlSvrForm()
        {
            this.obey_eml_srvs_evnts = false;
            this.add_eml_srvs = false;
            this.edit_eml_srvs = false;
            this.saveEmlSvrButton.Enabled = false;
            this.addEmlSvrButton.Enabled = this.addSvrs;
            this.editEmlSvrButton.Enabled = this.editSvrs;
            this.emlSrvrIDTextBox.Text = "-1";
            this.smtpClientTextBox.Text = "";
            this.smtpClientTextBox.ReadOnly = true;
            this.smtpClientTextBox.BackColor = Color.WhiteSmoke;

            this.smsDataGridView.ReadOnly = true;
            this.smsDataGridView.BackColor = Color.WhiteSmoke;

            this.emailUnameTextBox.Text = "";
            this.emailUnameTextBox.ReadOnly = true;
            this.emailUnameTextBox.BackColor = Color.WhiteSmoke;

            this.emailPswdTextBox.Text = "";
            this.emailPswdTextBox.ReadOnly = true;
            this.emailPswdTextBox.BackColor = Color.WhiteSmoke;

            this.smtpPortNmUpDown.Value = 0;
            this.smtpPortNmUpDown.ReadOnly = true;
            this.smtpPortNmUpDown.BackColor = Color.WhiteSmoke;

            this.activeDrctryDmnTextBox.Text = "";
            this.activeDrctryDmnTextBox.ReadOnly = true;
            this.activeDrctryDmnTextBox.BackColor = Color.WhiteSmoke;

            this.isDfltYesEmlSvrCheckBox.Checked = false;
            this.isDfltNoEmlSvrCheckBox.Checked = false;

            this.ftpServerTextBox.Text = "";
            this.ftpServerTextBox.ReadOnly = true;
            this.ftpServerTextBox.BackColor = Color.WhiteSmoke;

            this.ftpUnmTextBox.Text = "";
            this.ftpUnmTextBox.ReadOnly = true;
            this.ftpUnmTextBox.BackColor = Color.WhiteSmoke;

            this.ftpPswdTextBox.Text = "";
            this.ftpPswdTextBox.ReadOnly = true;
            this.ftpPswdTextBox.BackColor = Color.WhiteSmoke;

            this.ftpPortNumUpDown.Value = 0;
            this.ftpPortNumUpDown.ReadOnly = true;
            this.ftpPortNumUpDown.BackColor = Color.WhiteSmoke;

            this.ftpHomeDirTextBox.Text = "";
            this.ftpHomeDirTextBox.ReadOnly = true;
            this.ftpHomeDirTextBox.BackColor = Color.WhiteSmoke;

            this.ftpBaseDirTextBox.Text = "";
            this.ftpBaseDirTextBox.ReadOnly = true;
            this.ftpBaseDirTextBox.BackColor = Color.WhiteSmoke;
            this.enforceFTPCheckBox.Checked = false;

            this.pgDirTextBox.Text = "";
            this.pgDirTextBox.ReadOnly = true;
            this.pgDirTextBox.BackColor = Color.WhiteSmoke;

            this.bckpFileDirTextBox.Text = "";
            this.bckpFileDirTextBox.ReadOnly = true;
            this.bckpFileDirTextBox.BackColor = Color.WhiteSmoke;

            this.comPortComboBox.Text = "";
            this.comPortComboBox.Enabled = false;
            this.comPortComboBox.BackColor = Color.WhiteSmoke;

            this.baudRateComboBox.Text = "";
            this.baudRateComboBox.Enabled = false;
            this.baudRateComboBox.BackColor = Color.WhiteSmoke;

            this.timeoutComboBox.Text = "";
            this.timeoutComboBox.Enabled = false;
            this.timeoutComboBox.BackColor = Color.WhiteSmoke;

            this.obey_eml_srvs_evnts = true;
        }

        private void newEmlSvrForm()
        {
            this.obey_eml_srvs_evnts = false;
            this.add_eml_srvs = true;
            this.edit_eml_srvs = false;
            this.saveEmlSvrButton.Enabled = true;
            this.editEmlSvrButton.Enabled = false;
            this.addEmlSvrButton.Enabled = false;
            this.emlSrvrIDTextBox.Text = "-1";
            this.smtpClientTextBox.Text = "";
            this.smtpClientTextBox.ReadOnly = false;
            this.smtpClientTextBox.BackColor = Color.FromArgb(255, 255, 118);

            this.emailUnameTextBox.Text = "";
            this.emailUnameTextBox.ReadOnly = false;
            this.emailUnameTextBox.BackColor = Color.FromArgb(255, 255, 118);

            this.emailPswdTextBox.Text = "";
            this.emailPswdTextBox.ReadOnly = false;
            this.emailPswdTextBox.BackColor = Color.FromArgb(255, 255, 118);

            this.smtpPortNmUpDown.Value = 0;
            this.smtpPortNmUpDown.ReadOnly = false;
            this.smtpPortNmUpDown.BackColor = Color.FromArgb(255, 255, 118);

            this.activeDrctryDmnTextBox.Text = "";
            this.activeDrctryDmnTextBox.ReadOnly = false;
            this.activeDrctryDmnTextBox.BackColor = Color.White;

            this.isDfltYesEmlSvrCheckBox.Checked = false;
            this.isDfltNoEmlSvrCheckBox.Checked = true;

            this.ftpServerTextBox.Text = "";
            this.ftpServerTextBox.ReadOnly = false;
            this.ftpServerTextBox.BackColor = Color.White;

            this.ftpUnmTextBox.Text = "";
            this.ftpUnmTextBox.ReadOnly = false;
            this.ftpUnmTextBox.BackColor = Color.White;

            this.ftpPswdTextBox.Text = "";
            this.ftpPswdTextBox.ReadOnly = false;
            this.ftpPswdTextBox.BackColor = Color.White;

            this.ftpPortNumUpDown.Value = 0;
            this.ftpPortNumUpDown.ReadOnly = false;
            this.ftpPortNumUpDown.BackColor = Color.White;

            this.ftpHomeDirTextBox.Text = "";
            this.ftpHomeDirTextBox.ReadOnly = false;
            this.ftpHomeDirTextBox.BackColor = Color.White;

            this.ftpBaseDirTextBox.Text = "";
            this.ftpBaseDirTextBox.ReadOnly = false;
            this.ftpBaseDirTextBox.BackColor = Color.White;
            this.enforceFTPCheckBox.Checked = false;

            this.pgDirTextBox.Text = Global.myNwMainFrm.cmmnCode.getPGBinDrctry();
            //this.pgDirTextBox.ReadOnly = false;
            //this.pgDirTextBox.BackColor = Color.White;

            this.bckpFileDirTextBox.Text = Global.myNwMainFrm.cmmnCode.getBackupDrctry();
            //this.bckpFileDirTextBox.ReadOnly = false;
            //this.bckpFileDirTextBox.BackColor = Color.White;

            this.comPortComboBox.Text = "";
            this.comPortComboBox.Enabled = true;
            this.comPortComboBox.BackColor = Color.White;

            this.baudRateComboBox.Text = "";
            this.baudRateComboBox.Enabled = true;
            this.baudRateComboBox.BackColor = Color.White;

            this.timeoutComboBox.Text = "";
            this.timeoutComboBox.Enabled = true;
            this.timeoutComboBox.BackColor = Color.White;
            this.obey_eml_srvs_evnts = true;
        }

        private void editEmlSvrForm()
        {
            this.obey_eml_srvs_evnts = false;
            this.add_eml_srvs = false;
            this.edit_eml_srvs = true;
            this.saveEmlSvrButton.Enabled = true;
            this.editEmlSvrButton.Enabled = false;
            this.addEmlSvrButton.Enabled = false;
            this.smtpClientTextBox.ReadOnly = false;
            this.smtpClientTextBox.BackColor = Color.FromArgb(255, 255, 118);

            this.emailUnameTextBox.ReadOnly = false;
            this.emailUnameTextBox.BackColor = Color.FromArgb(255, 255, 118);

            this.smsDataGridView.ReadOnly = false;
            this.smsDataGridView.BackColor = Color.White;

            this.emailPswdTextBox.ReadOnly = false;
            this.emailPswdTextBox.BackColor = Color.FromArgb(255, 255, 118);

            this.smtpPortNmUpDown.ReadOnly = false;
            this.smtpPortNmUpDown.BackColor = Color.FromArgb(255, 255, 118);

            this.activeDrctryDmnTextBox.ReadOnly = false;
            this.activeDrctryDmnTextBox.BackColor = Color.White;

            this.ftpServerTextBox.ReadOnly = false;
            this.ftpServerTextBox.BackColor = Color.White;

            this.ftpUnmTextBox.ReadOnly = false;
            this.ftpUnmTextBox.BackColor = Color.White;

            this.ftpPswdTextBox.ReadOnly = false;
            this.ftpPswdTextBox.BackColor = Color.White;

            this.ftpPortNumUpDown.ReadOnly = false;
            this.ftpPortNumUpDown.BackColor = Color.White;

            this.ftpBaseDirTextBox.ReadOnly = false;
            this.ftpBaseDirTextBox.BackColor = Color.White;
            this.ftpHomeDirTextBox.ReadOnly = false;
            this.ftpHomeDirTextBox.BackColor = Color.White;

            //this.pgDirTextBox.ReadOnly = false;
            //this.pgDirTextBox.BackColor = Color.White;

            //this.bckpFileDirTextBox.ReadOnly = false;
            //this.bckpFileDirTextBox.BackColor = Color.White;

            this.comPortComboBox.Enabled = true;
            this.comPortComboBox.BackColor = Color.White;

            this.baudRateComboBox.Enabled = true;
            this.baudRateComboBox.BackColor = Color.White;

            this.timeoutComboBox.Enabled = true;
            this.timeoutComboBox.BackColor = Color.White;
            this.obey_eml_srvs_evnts = true;
        }

        private void populateEmlSvrDetails()
        {
            this.clearEmlSvrForm();
            this.obey_eml_srvs_evnts = false;
            DataSet dtst = Global.get_Eml_Srvrs(this.searchForEmlSvrTextBox.Text,
              this.searchInEmlSvrComboBox.Text, this.email_cur_indx,
              1);
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.last_email_num = Global.myNwMainFrm.cmmnCode.navFuncts.startIndex() + i;
                this.emlSrvrIDTextBox.Text = dtst.Tables[0].Rows[i][0].ToString();
                this.smtpClientTextBox.Text = dtst.Tables[0].Rows[i][1].ToString();
                this.emailUnameTextBox.Text = dtst.Tables[0].Rows[i][2].ToString();
                this.emailPswdTextBox.Text = Global.myNwMainFrm.cmmnCode.decrypt(dtst.Tables[0].Rows[i][3].ToString(), CommonCode.CommonCodes.AppKey);
                bool test = false;
                if (bool.TryParse(dtst.Tables[0].Rows[i][5].ToString(), out test) == true)
                {
                    this.isDfltYesEmlSvrCheckBox.Checked = test;
                    this.isDfltNoEmlSvrCheckBox.Checked = !test;
                }
                this.smtpPortNmUpDown.Value = Decimal.Parse(dtst.Tables[0].Rows[i][4].ToString());
                this.activeDrctryDmnTextBox.Text = dtst.Tables[0].Rows[i][6].ToString();

                this.ftpServerTextBox.Text = dtst.Tables[0].Rows[i][7].ToString();
                this.ftpUnmTextBox.Text = dtst.Tables[0].Rows[i][8].ToString();
                this.ftpPswdTextBox.Text = Global.myNwMainFrm.cmmnCode.decrypt(dtst.Tables[0].Rows[i][9].ToString(), CommonCode.CommonCodes.AppKey);
                this.ftpPortNumUpDown.Value = Decimal.Parse(dtst.Tables[0].Rows[i][10].ToString());
                this.ftpBaseDirTextBox.Text = dtst.Tables[0].Rows[i][11].ToString();
                this.ftpHomeDirTextBox.Text = dtst.Tables[0].Rows[i][18].ToString();
                this.enforceFTPCheckBox.Checked = Global.myNwMainFrm.cmmnCode.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][12].ToString());
                //this.pgDirTextBox.Text = dtst.Tables[0].Rows[i][13].ToString();
                this.pgDirTextBox.Text = Global.myNwMainFrm.cmmnCode.getPGBinDrctry();
                this.bckpFileDirTextBox.Text = Global.myNwMainFrm.cmmnCode.getBackupDrctry();
                this.comPortComboBox.Text = dtst.Tables[0].Rows[i][15].ToString();
                this.baudRateComboBox.Text = dtst.Tables[0].Rows[i][16].ToString();
                this.timeoutComboBox.Text = dtst.Tables[0].Rows[i][17].ToString();

                this.smsDataGridView.Rows.Clear();
                this.smsDataGridView.RowCount = 10;
                this.smsDataGridView.DefaultCellStyle.ForeColor = Color.Black;

                DataSet smsdtst = Global.myNwMainFrm.cmmnCode.selectDataNoParams(@"select sms_param1, sms_param2, sms_param3, 
                                                sms_param4, sms_param5, sms_param6, 
                                                sms_param7, sms_param8, sms_param9, sms_param10 
                                                from sec.sec_email_servers where server_id=" + this.emlSrvrIDTextBox.Text);
                string[] paramNms = new string[10];
                string[] paramVals = new string[10];
                string tmpStr = "";
                string[] tmpArry;
                char[] y = { '|' };

                for (int j = 0; j < smsdtst.Tables[0].Columns.Count; j++)
                {
                    tmpStr = smsdtst.Tables[0].Rows[0][j].ToString().Trim().Trim(y).Trim();
                    tmpArry = tmpStr.Split(y, StringSplitOptions.RemoveEmptyEntries);

                    if (tmpStr == ""
                      || tmpArry.Length < 1)
                    {
                        paramNms[j] = "";
                        paramVals[j] = "";
                    }
                    else if (tmpArry.Length == 1)
                    {
                        paramNms[j] = tmpArry[0];
                        paramVals[j] = "";
                    }
                    else
                    {
                        paramNms[j] = tmpArry[0];
                        paramVals[j] = tmpArry[1];
                    }
                    this.smsDataGridView.Rows[j].Cells[0].Value = paramNms[j];
                    this.smsDataGridView.Rows[j].Cells[1].Value = paramVals[j];
                }
            }
            this.correctEmlSvrNavLbls(dtst);

            this.obey_eml_srvs_evnts = true;
            this.changeOrg();
        }

        public void changeOrg()
        {
            if (Global.myNwMainFrm.cmmnCode.Org_id <= 0)
            {
                Global.myNwMainFrm.cmmnCode.Org_id = Global.myNwMainFrm.cmmnCode.getPrsnOrgID(Global.mySecurity.user_id);
            }
            if (this.crntOrgIDTextBox.Text == "-1"
          || this.crntOrgIDTextBox.Text == "")
            {
                this.crntOrgIDTextBox.Text = Global.myNwMainFrm.cmmnCode.Org_id.ToString();
                this.crntOrgTextBox.Text = Global.myNwMainFrm.cmmnCode.getOrgName(Global.myNwMainFrm.cmmnCode.Org_id);
                //Global.myNwMainFrm.cmmnCode.getDBImageFile(this.crntOrgIDTextBox.Text + ".png",
                // 0, ref this.curOrgPictureBox);
            }
        }

        private void correctEmlSvrNavLbls(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.email_cur_indx == 0 && totlRecs == 0)
            {
                this.is_last_email = true;
                this.totl_eml_srvs = 0;
                this.last_email_num = 0;
                this.email_cur_indx = 0;
                this.updtEmlSvrTotals();
                this.updtEmlSvrNavLabels();
            }
            else if (this.totl_eml_srvs == Global.myNwMainFrm.cmmnCode.Big_Val
          && totlRecs < 1)
            {
                this.totl_eml_srvs = this.last_email_num;
                this.is_last_email = true;
                if (totlRecs == 0)
                {
                    this.email_cur_indx -= 1;
                    this.updtEmlSvrTotals();
                    this.populateEmlSvrDetails();
                }
                else
                {
                    this.updtEmlSvrTotals();
                }
            }
        }

        private void emlSvrPnlNavButtons(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecEmlSvrLabel.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.is_last_email = false;
                this.email_cur_indx = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.is_last_email = false;
                this.email_cur_indx -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.is_last_email = false;
                this.email_cur_indx += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.is_last_email = true;
                this.totl_eml_srvs = Global.get_total_EmlSvr(this.searchForEmlSvrTextBox.Text, this.searchInEmlSvrComboBox.Text);
                this.updtEmlSvrTotals();
                this.email_cur_indx = Global.myNwMainFrm.cmmnCode.navFuncts.totalGroups - 1;
            }
            this.getEmlPnlData();
        }

        private bool shdObeyEmlSvrEvnt()
        {
            if (this.obey_eml_srvs_evnts == false)
            {
                return false;
            }
            if (this.add_eml_srvs == false && this.edit_eml_srvs == false)
            {
                this.loadEmailSrvrPanel();
                return false;
            }
            return true;
        }

        private void addEmlSvrButton_Click(object sender, EventArgs e)
        {
            if (Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[14]) == false)
            {
                Global.myNwMainFrm.cmmnCode.showMsg("You don't have permission to perform" +
                  " this action!\nContact your System Administrator!", 0);
                return;
            }
            this.newEmlSvrForm();
        }

        private void editEmlSvrButton_Click(object sender, EventArgs e)
        {
            if (Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[15]) == false)
            {
                Global.myNwMainFrm.cmmnCode.showMsg("You don't have permission to perform" +
                  " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.emlSrvrIDTextBox.Text == "" || this.emlSrvrIDTextBox.Text == "-1")
            {
                Global.myNwMainFrm.cmmnCode.showMsg("There's nothing to edit!", 0);
                return;
            }
            this.editEmlSvrForm();
        }

        private void saveEmlSvrButton_Click(object sender, EventArgs e)
        {
            if (this.add_eml_srvs == true)
            {
                if (Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[14]) == false)
                {
                    Global.myNwMainFrm.cmmnCode.showMsg("You don't have permission to perform" +
                      " this action!\nContact your System Administrator!", 0);
                    return;
                }
            }
            else
            {
                if (Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[15]) == false)
                {
                    Global.myNwMainFrm.cmmnCode.showMsg("You don't have permission to perform" +
                      " this action!\nContact your System Administrator!", 0);
                    return;
                }
            }
            if (this.smtpClientTextBox.Text == "" || this.emailUnameTextBox.Text == ""
              || this.emailPswdTextBox.Text == "" || this.smtpPortNmUpDown.Value <= 0
            )
            {
                Global.myNwMainFrm.cmmnCode.showMsg("Please fill all required fields!", 0);
                return;
            }
            long oldID = Global.getEmlSvrID(this.smtpClientTextBox.Text);
            if (oldID > 0 && this.add_eml_srvs == true)
            {
                MessageBox.Show("Email Server Name exists already!", "Rhomicom Message!",
                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (oldID > 0 && this.edit_eml_srvs == true && oldID.ToString() != this.emlSrvrIDTextBox.Text)
            {
                MessageBox.Show("New Email Server Name exists already!", "Rhomicom Message!",
                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (this.add_eml_srvs == true)
            {
                if (this.isDfltYesEmlSvrCheckBox.Checked == true)
                {
                    Global.undefaultAllEmlSvrs();
                }
                Global.createEml_Svr(this.smtpClientTextBox.Text, this.emailUnameTextBox.Text,
                  this.emailPswdTextBox.Text, (int)this.smtpPortNmUpDown.Value,
                  this.isDfltYesEmlSvrCheckBox.Checked, this.activeDrctryDmnTextBox.Text,
                  this.ftpServerTextBox.Text, this.ftpUnmTextBox.Text, this.ftpPswdTextBox.Text,
                  (int)this.ftpPortNumUpDown.Value, this.ftpBaseDirTextBox.Text,
                  this.enforceFTPCheckBox.Checked, this.pgDirTextBox.Text, this.bckpFileDirTextBox.Text,
                  this.comPortComboBox.Text, this.baudRateComboBox.Text, this.timeoutComboBox.Text, this.ftpHomeDirTextBox.Text);
                this.saveEmlSvrButton.Enabled = true;
                this.addEmlSvrButton.Enabled = true;
                this.editEmlSvrButton.Enabled = true;
                this.add_eml_srvs = false;
                this.edit_eml_srvs = true;
                System.Windows.Forms.Application.DoEvents();
                this.emlSrvrIDTextBox.Text = Global.getEmlSvrID(this.smtpClientTextBox.Text).ToString();
            }
            else if (this.edit_eml_srvs == true)
            {
                if (this.isDfltYesEmlSvrCheckBox.Checked == true)
                {
                    Global.undefaultAllEmlSvrs();
                }
                Global.updateEmlSvrs(int.Parse(this.emlSrvrIDTextBox.Text),
                  this.smtpClientTextBox.Text, this.emailUnameTextBox.Text,
                  this.emailPswdTextBox.Text, (int)this.smtpPortNmUpDown.Value,
                  this.isDfltYesEmlSvrCheckBox.Checked, this.activeDrctryDmnTextBox.Text,
                  this.ftpServerTextBox.Text, this.ftpUnmTextBox.Text, this.ftpPswdTextBox.Text,
                  (int)this.ftpPortNumUpDown.Value, this.ftpBaseDirTextBox.Text,
                  this.enforceFTPCheckBox.Checked, this.pgDirTextBox.Text, this.bckpFileDirTextBox.Text,
                  this.comPortComboBox.Text, this.baudRateComboBox.Text, this.timeoutComboBox.Text, this.ftpHomeDirTextBox.Text);
                this.saveEmlSvrButton.Enabled = false;
                this.addEmlSvrButton.Enabled = true;
                this.editEmlSvrButton.Enabled = true;
                this.edit_eml_srvs = false;
            }
            //sms_param1
            if (int.Parse(this.emlSrvrIDTextBox.Text) > 0)
            {
                this.smsDataGridView.EndEdit();
                System.Windows.Forms.Application.DoEvents();
                this.emlSrvrIDTextBox.Focus();
                System.Windows.Forms.Application.DoEvents();
                for (int f = 0; f < this.smsDataGridView.Rows.Count; f++)
                {
                    if (this.smsDataGridView.Rows[f].Cells[0].Value == null)
                    {
                        this.smsDataGridView.Rows[f].Cells[0].Value = string.Empty;
                    }
                    if (this.smsDataGridView.Rows[f].Cells[1].Value == null)
                    {
                        this.smsDataGridView.Rows[f].Cells[1].Value = string.Empty;
                    }
                    string updstr = @"UPDATE sec.sec_email_servers SET sms_param" + (f + 1).ToString() +
                      @"='" + (this.smsDataGridView.Rows[f].Cells[0].Value.ToString() + "|" +
                      this.smsDataGridView.Rows[f].Cells[1].Value.ToString()).Replace("'", "''") +
                      @"' WHERE server_id = " + this.emlSrvrIDTextBox.Text;
                    //Global.myNwMainFrm.cmmnCode.showSQLNoPermsn(updstr);
                    Global.myNwMainFrm.cmmnCode.updateDataNoParams(updstr);
                }
            }
            this.loadEmailSrvrPanel();
        }

        private void vwSQLEmlSvrButton_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.cmmnCode.showSQL(Global.myNwMainFrm.eml_srvs_SQL, 18);
        }

        private void recHstryEmlSvrButton_Click(object sender, EventArgs e)
        {
            if (this.emlSrvrIDTextBox.Text == "-1"
              || this.emlSrvrIDTextBox.Text == "")
            {
                return;
            }
            Global.myNwMainFrm.cmmnCode.showRecHstry(Global.get_EmlSvr_Rec_Hstry(int.Parse(this.emlSrvrIDTextBox.Text)), 19);
        }

        private void refreshEmlSvrButton_Click(object sender, EventArgs e)
        {
            this.loadEmailSrvrPanel();
            this.Refresh();
        }

        private void isDfltYesEmlSvrCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shdObeyEmlSvrEvnt() == false)
            {
                return;
            }
            this.isDfltNoEmlSvrCheckBox.Checked = !this.isDfltYesEmlSvrCheckBox.Checked;
        }

        private void isDfltNoEmlSvrCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shdObeyEmlSvrEvnt() == false)
            {
                return;
            }
            this.isDfltYesEmlSvrCheckBox.Checked = !this.isDfltNoEmlSvrCheckBox.Checked;
        }

        private void searchForEmlSvrTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.refreshEmlSvrButton_Click(this.refreshEmlSvrButton, ex);
            }
        }

        private void positionEmlSvrTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                this.emlSvrPnlNavButtons(this.movePreviousEmlSvrButton, ex);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
            {
                this.emlSvrPnlNavButtons(this.moveNextEmlSvrButton, ex);
            }
        }

        private void smtpPortNmUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (this.shdObeyEmlSvrEvnt() == false)
            {
                return;
            }
        }

        private void ftpPortNumUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (this.shdObeyEmlSvrEvnt() == false)
            {
                return;
            }
        }

        private void enforceFTPCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shdObeyEmlSvrEvnt() == false
      || beenToCheckBx == true)
            {
                beenToCheckBx = false;
                return;
            }
            beenToCheckBx = true;
            if (this.add_eml_srvs == false && this.edit_eml_srvs == false)
            {
                this.enforceFTPCheckBox.Checked = !this.enforceFTPCheckBox.Checked;
            }
        }
        #endregion

        #region "USER LOGINS PANEL..."
        private void loadLoginsPanel()
        {
            this.obey_lgns_evnts = false;
            if (this.searchInLgnsComboBox.SelectedIndex < 0)
            {
                this.searchInLgnsComboBox.SelectedIndex = 0;
            }
            if (this.searchForLgnsTextBox.Text.Contains("%") == false)
            {
                this.searchForLgnsTextBox.Text = "%" + this.searchForLgnsTextBox.Text.Replace(" ", "%") + "%";
            }
            if (this.searchForLgnsTextBox.Text == "%%")
            {
                this.searchForLgnsTextBox.Text = "%";
            }
            int dsply = 0;
            if (this.dsplySizeLgnsComboBox.Text == ""
              || int.TryParse(this.dsplySizeLgnsComboBox.Text, out dsply) == false)
            {
                this.dsplySizeLgnsComboBox.Text = Global.myNwMainFrm.cmmnCode.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            //this.loginsListView.Height = this.loginsPanel.Bottom - this.toolStrip4.Bottom - 15;
            //this.loginsListView.Width = this.loginsPanel.Right - this.showSuccsflCheckBox.Width - this.loginsPanel.Left - 40;
            this.showSuccsflCheckBox.Location = new Point(this.loginsListView.Right + 15, this.showSuccsflCheckBox.Location.Y);
            this.showFaildCheckBox.Location = new Point(this.showSuccsflCheckBox.Location.X, this.showFaildCheckBox.Location.Y);
            this.is_last_lgns = false;
            this.totl_lgns = this.cmmnCode.Big_Val;
            this.getLgnsPnldata();
            this.obey_lgns_evnts = true;
        }

        private void getLgnsPnldata()
        {
            this.updtLgnsTotals();
            this.populateLgnsLstVw();
            this.updtLgnsNavLabels();
        }

        private void updtLgnsTotals()
        {
            Global.myNwMainFrm.cmmnCode.navFuncts.FindNavigationIndices(int.Parse(this.dsplySizeLgnsComboBox.Text), this.totl_lgns);

            if (this.lgns_cur_indx >= Global.myNwMainFrm.cmmnCode.navFuncts.totalGroups)
            {
                this.lgns_cur_indx = Global.myNwMainFrm.cmmnCode.navFuncts.totalGroups - 1;
            }
            if (this.lgns_cur_indx < 0)
            {
                this.lgns_cur_indx = 0;
            }
            Global.myNwMainFrm.cmmnCode.navFuncts.currentNavigationIndex = this.lgns_cur_indx;
        }

        private void updtLgnsNavLabels()
        {
            this.moveFirstLgnsButton.Enabled = Global.myNwMainFrm.cmmnCode.navFuncts.moveFirstBtnStatus();
            this.movePreviousLgnsButton.Enabled = Global.myNwMainFrm.cmmnCode.navFuncts.movePrevBtnStatus();
            this.moveNextLgnsButton.Enabled = Global.myNwMainFrm.cmmnCode.navFuncts.moveNextBtnStatus();
            this.moveLastLgnsButton.Enabled = Global.myNwMainFrm.cmmnCode.navFuncts.moveLastBtnStatus();
            this.positionLgnsTextBox.Text = Global.myNwMainFrm.cmmnCode.navFuncts.displayedRecordsNumbers();
            if (this.is_last_lgns == true)
            {
                this.totalRecLgnsLabel.Text = Global.myNwMainFrm.cmmnCode.navFuncts.totalRecordsLabel();
            }
            else
            {
                this.totalRecLgnsLabel.Text = "of Total";
            }
        }

        private void populateLgnsLstVw()
        {
            this.obey_lgns_evnts = false;
            DataSet dtst = Global.get_Lgns(this.searchForLgnsTextBox.Text,
              this.searchInLgnsComboBox.Text, this.lgns_cur_indx,
              int.Parse(this.dsplySizeLgnsComboBox.Text),
              this.showSuccsflCheckBox.Checked, this.showFaildCheckBox.Checked);
            this.loginsListView.Items.Clear();
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.last_lgns_num = Global.myNwMainFrm.cmmnCode.navFuncts.startIndex() + i;
                ListViewItem nwItm = new ListViewItem(new string[] { (Global.myNwMainFrm.cmmnCode.navFuncts.startIndex() + i).ToString(),
                dtst.Tables[0].Rows[i][0].ToString(), dtst.Tables[0].Rows[i][1].ToString(),
                dtst.Tables[0].Rows[i][2].ToString(), dtst.Tables[0].Rows[i][3].ToString(),
                dtst.Tables[0].Rows[i][4].ToString().ToUpper(),dtst.Tables[0].Rows[i][5].ToString(),
                dtst.Tables[0].Rows[i][6].ToString()});
                this.loginsListView.Items.Add(nwItm);
            }
            this.correctLgnsNavLbls(dtst);
            this.obey_lgns_evnts = true;
            if (this.loginsListView.Items.Count > 0)
            {
                this.loginsListView.Items[0].Selected = true;
            }
            this.obey_lgns_evnts = true;
        }

        private void correctLgnsNavLbls(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.lgns_cur_indx == 0 && totlRecs == 0)
            {
                this.is_last_lgns = true;
                this.totl_lgns = 0;
                this.last_lgns_num = 0;
                this.lgns_cur_indx = 0;
                this.updtLgnsTotals();
                this.updtLgnsNavLabels();
            }
            else if (this.totl_lgns == Global.myNwMainFrm.cmmnCode.Big_Val
          && totlRecs < int.Parse(this.dsplySizeLgnsComboBox.Text))
            {
                this.totl_lgns = this.last_lgns_num;
                this.is_last_lgns = true;
                if (totlRecs == 0)
                {
                    this.lgns_cur_indx -= 1;
                    this.updtLgnsTotals();
                    this.populateLgnsLstVw();
                }
                else
                {
                    this.updtLgnsTotals();
                }
            }
        }

        private void lgnsPnlNavButtons(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecLgnsLabel.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.is_last_lgns = false;
                this.lgns_cur_indx = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.is_last_lgns = false;
                this.lgns_cur_indx -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.is_last_lgns = false;
                this.lgns_cur_indx += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.is_last_lgns = true;
                this.totl_lgns = Global.get_total_lgns(this.searchForLgnsTextBox.Text,
          this.searchInLgnsComboBox.Text, this.showSuccsflCheckBox.Checked, this.showFaildCheckBox.Checked);
                this.updtLgnsTotals();
                this.lgns_cur_indx = Global.myNwMainFrm.cmmnCode.navFuncts.totalGroups - 1;
            }
            this.getLgnsPnldata();
        }

        private bool shdObeyLgnsEvts()
        {
            return this.obey_lgns_evnts;
        }

        private void showSuccsflCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shdObeyLgnsEvts() == false)
            {
                return;
            }
            if (this.showSuccsflCheckBox.Checked == false
              && this.showFaildCheckBox.Checked == false)
            {
                this.showFaildCheckBox.Checked = true;
                return;
            }
            this.loadLoginsPanel();
        }

        private void showFaildCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.shdObeyLgnsEvts() == false)
            {
                return;
            }
            if (this.showSuccsflCheckBox.Checked == false
              && this.showFaildCheckBox.Checked == false)
            {
                this.showSuccsflCheckBox.Checked = true;
                return;
            }
            this.loadLoginsPanel();
        }

        private void vwSQLLgnsButton_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.cmmnCode.showSQL(Global.myNwMainFrm.lgns_SQL, 18);
        }

        private void refreshLgnsButton_Click(object sender, EventArgs e)
        {
            this.loadLoginsPanel();
            this.Refresh();
        }

        private void exptLgnMenuItem_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.cmmnCode.exprtToExcel(this.loginsListView);
        }

        private void refreshLgnMenuItem_Click(object sender, EventArgs e)
        {
            this.refreshLgnsButton_Click(this.refreshLgnsButton, e);
        }

        private void vwSQLLgnMenuItem_Click(object sender, EventArgs e)
        {
            this.vwSQLLgnsButton_Click(this.vwSQLLgnsButton, e);
        }

        private void searchForLgnsTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.refreshLgnsButton_Click(this.refreshLgnsButton, ex);
            }
        }

        private void positionLgnsTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                this.lgnsPnlNavButtons(this.movePreviousLgnsButton, ex);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
            {
                this.lgnsPnlNavButtons(this.moveNextLgnsButton, ex);
            }
        }
        #endregion

        #region "AUDIT TABLES PANEL..."
        private void populateAdtTrlTrVw()
        {
            this.obey_adt_evnts = false;
            this.auditTblsTreeView.Nodes.Clear();
            DataSet dtst = Global.get_Module_Nms();
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                TreeNode nwNode = new TreeNode();
                nwNode.Name = "myAdtNode" + i.ToString();
                nwNode.Text = dtst.Tables[0].Rows[i][0].ToString();
                nwNode.ImageKey = menuImages[6];
                this.auditTblsTreeView.Nodes.Add(nwNode);
            }
            this.obey_adt_evnts = true;
            if (this.auditTblsTreeView.Nodes.Count > 0)
            {
                this.auditTblsTreeView.SelectedNode = this.auditTblsTreeView.Nodes[0];
            }
            this.obey_adt_evnts = true;
        }

        private void loadAuditPanel()
        {
            this.obey_adt_evnts = false;
            if (this.searchInAdtComboBox.SelectedIndex < 0)
            {
                this.searchInAdtComboBox.SelectedIndex = 3;
            }
            if (this.searchForAdtTextBox.Text.Contains("%") == false)
            {
                this.searchForAdtTextBox.Text = "%" + this.searchForAdtTextBox.Text.Replace(" ", "%") + "%";
            }
            if (this.searchForAdtTextBox.Text == "%%")
            {
                this.searchForAdtTextBox.Text = "%";
            }
            int dsply = 0;
            if (this.dsplySizeAdtComboBox.Text == ""
              || int.TryParse(this.dsplySizeAdtComboBox.Text, out dsply) == false)
            {
                this.dsplySizeAdtComboBox.Text = Global.myNwMainFrm.cmmnCode.get_CurPlcy_Mx_Dsply_Recs().ToString();
            }
            //this.auditTblsDataGridView.Height = this.auditPanel.Bottom - this.toolStrip5.Bottom - 25;
            //this.auditTblsDataGridView.Width = this.auditPanel.Right - this.auditTblsTreeView.Width - this.auditPanel.Left - 25;
            //this.auditTblsTreeView.Location = new Point(this.auditTblsDataGridView.Right + 10, this.auditTblsTreeView.Location.Y);
            //this.panel17.Location = new Point(this.auditTblsDataGridView.Right + 10, this.panel17.Location.Y);
            //this.auditTblsTreeView.Height = this.auditPanel.Bottom - this.panel17.Bottom - 25;
            //this.auditTblsDataGridView.Columns[1].Width = (int)((double)0.12 * (double)this.auditTblsDataGridView.Width);
            //this.auditTblsDataGridView.Columns[2].Width = (int)((double)0.12 * (double)this.auditTblsDataGridView.Width);
            //this.auditTblsDataGridView.Columns[3].Width = (int)((double)0.30 * (double)this.auditTblsDataGridView.Width);
            //this.auditTblsDataGridView.Columns[4].Width = (int)((double)0.13 * (double)this.auditTblsDataGridView.Width);
            //this.auditTblsDataGridView.Columns[5].Width = (int)((double)0.16 * (double)this.auditTblsDataGridView.Width);
            //this.auditTblsDataGridView.Columns[6].Width = (int)((double)0.10 * (double)this.auditTblsDataGridView.Width);
            this.is_last_adt = false;
            this.totl_adts = this.cmmnCode.Big_Val;
            this.populateAdtTrlTrVw();
            //this.updtAdtTotals();
            //this.populateAdtGrdVw();
            //this.updtAdtNavLabels();
            this.obey_adt_evnts = true;
        }

        private void updtAdtTotals()
        {
            if (this.auditTblsTreeView.SelectedNode == null)
            {
                return;
            }
            Global.myNwMainFrm.cmmnCode.navFuncts.FindNavigationIndices(int.Parse(this.dsplySizeAdtComboBox.Text),
              this.totl_adts);

            if (this.adt_cur_indx >= Global.myNwMainFrm.cmmnCode.navFuncts.totalGroups)
            {
                this.adt_cur_indx = Global.myNwMainFrm.cmmnCode.navFuncts.totalGroups - 1;
            }
            if (this.adt_cur_indx < 0)
            {
                this.adt_cur_indx = 0;
            }
            Global.myNwMainFrm.cmmnCode.navFuncts.currentNavigationIndex = this.adt_cur_indx;
        }

        private void updtAdtNavLabels()
        {
            if (this.auditTblsTreeView.SelectedNode == null)
            {
                return;
            }
            this.moveFirstAdtButton.Enabled = Global.myNwMainFrm.cmmnCode.navFuncts.moveFirstBtnStatus();
            this.movePreviousAdtButton.Enabled = Global.myNwMainFrm.cmmnCode.navFuncts.movePrevBtnStatus();
            this.moveNextAdtButton.Enabled = Global.myNwMainFrm.cmmnCode.navFuncts.moveNextBtnStatus();
            this.moveLastAdtButton.Enabled = Global.myNwMainFrm.cmmnCode.navFuncts.moveLastBtnStatus();
            this.positionAdtTextBox.Text = Global.myNwMainFrm.cmmnCode.navFuncts.displayedRecordsNumbers();
            if (this.is_last_adt == true)
            {
                this.totalRecAdtLabel.Text = Global.myNwMainFrm.cmmnCode.navFuncts.totalRecordsLabel();
            }
            else
            {
                this.totalRecAdtLabel.Text = "of Total";
            }
        }

        private void populateAdtGrdVw()
        {
            if (this.auditTblsTreeView.SelectedNode == null)
            {
                return;
            }
            this.obey_adt_evnts = false;
            DataSet dtst = Global.get_Adt_Trails(this.searchForAdtTextBox.Text,
              this.searchInAdtComboBox.Text, this.adt_cur_indx,
              int.Parse(this.dsplySizeAdtComboBox.Text),
              this.auditTblsTreeView.SelectedNode.Text);
            this.auditTblsDataGridView.Rows.Clear();
            this.auditTblsDataGridView.RowCount = dtst.Tables[0].Rows.Count;
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.last_adt_num = Global.myNwMainFrm.cmmnCode.navFuncts.startIndex() + i;
                this.auditTblsDataGridView.Rows[i].HeaderCell.Value = (Global.myNwMainFrm.cmmnCode.navFuncts.startIndex() + i).ToString();
                Object[] cellDesc = new Object[7];
                cellDesc[0] = dtst.Tables[0].Rows[i][0].ToString();
                cellDesc[1] = dtst.Tables[0].Rows[i][1].ToString();
                cellDesc[2] = dtst.Tables[0].Rows[i][2].ToString();
                cellDesc[3] = dtst.Tables[0].Rows[i][3].ToString();
                cellDesc[4] = dtst.Tables[0].Rows[i][4].ToString().Replace("/", "\r\n");//Replace('/', ' ');
                cellDesc[5] = dtst.Tables[0].Rows[i][5].ToString();
                cellDesc[6] = dtst.Tables[0].Rows[i][6].ToString();
                this.auditTblsDataGridView.Rows[i].SetValues(cellDesc);
            }
            this.correctAdtNavLbls(dtst);
            this.obey_adt_evnts = true;
            if (this.auditTblsDataGridView.Rows.Count > 0)
            {
                this.auditTblsDataGridView.Rows[0].Selected = true;
            }
            this.obey_adt_evnts = true;
        }

        private void correctAdtNavLbls(DataSet dtst)
        {
            long totlRecs = dtst.Tables[0].Rows.Count;
            if (this.adt_cur_indx == 0 && totlRecs == 0)
            {
                this.is_last_adt = true;
                this.totl_adts = 0;
                this.last_adt_num = 0;
                this.adt_cur_indx = 0;
                this.updtAdtTotals();
                this.updtAdtNavLabels();
            }
            else if (this.totl_adts == Global.myNwMainFrm.cmmnCode.Big_Val
          && totlRecs < int.Parse(this.dsplySizeAdtComboBox.Text))
            {
                this.totl_adts = this.last_adt_num;
                this.is_last_adt = true;
                if (totlRecs == 0)
                {
                    this.adt_cur_indx -= 1;
                    this.updtAdtTotals();
                    this.populateAdtGrdVw();
                }
                else
                {
                    this.updtAdtTotals();
                }
            }
        }
        private void adtPnlNavButtons(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
            this.totalRecAdtLabel.Text = "";
            if (sentObj.Name.ToLower().Contains("first"))
            {
                this.is_last_adt = false;
                this.adt_cur_indx = 0;
            }
            else if (sentObj.Name.ToLower().Contains("previous"))
            {
                this.is_last_adt = false;
                this.adt_cur_indx -= 1;
            }
            else if (sentObj.Name.ToLower().Contains("next"))
            {
                this.is_last_adt = false;
                this.adt_cur_indx += 1;
            }
            else if (sentObj.Name.ToLower().Contains("last"))
            {
                this.is_last_adt = true;
                this.totl_adts = 0;
                if (this.auditTblsTreeView.Nodes.Count > 0)
                {
                    this.totl_adts = Global.get_total_adt_trls(this.searchForAdtTextBox.Text,
          this.searchInAdtComboBox.Text, this.auditTblsTreeView.SelectedNode.Text);
                }
                this.updtAdtTotals();
                this.adt_cur_indx = Global.myNwMainFrm.cmmnCode.navFuncts.totalGroups - 1;
            }
            if (this.auditTblsTreeView.Nodes.Count > 0)
            {
                this.updtAdtTotals();
                this.populateAdtGrdVw();
                this.updtAdtNavLabels();
            }
            else
            {
                this.loadAuditPanel();
            }
        }

        private bool shdObeyAdtEvts()
        {
            return this.obey_adt_evnts;
        }

        private void vwSQLAdtButton_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.cmmnCode.showSQL(Global.myNwMainFrm.adt_SQL, 18);
        }

        private void auditTblsTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.shdObeyAdtEvts() == false)
            {
                return;
            }
            if (e.Node.IsSelected == true)
            {
                this.is_last_adt = false;
                this.totl_adts = this.cmmnCode.Big_Val;
                this.updtAdtTotals();
                this.populateAdtGrdVw();
                this.updtAdtNavLabels();
            }
        }

        private void refreshAdtButton_Click(object sender, EventArgs e)
        {
            if (this.auditTblsTreeView.Nodes.Count > 0)
            {
                this.is_last_adt = false;
                this.totl_adts = this.cmmnCode.Big_Val;
                this.updtAdtTotals();
                this.populateAdtGrdVw();
                this.updtAdtNavLabels();
            }
            else
            {
                this.loadAuditPanel();
            }
            this.Refresh();
        }

        private void exptAudtMenuItem_Click(object sender, EventArgs e)
        {
            Global.myNwMainFrm.cmmnCode.exprtToExcel(this.auditTblsDataGridView);
        }

        private void vwSQLAdtMenuItem_Click(object sender, EventArgs e)
        {
            this.vwSQLAdtButton_Click(this.vwSQLAdtButton, e);
        }

        private void refreshAdtMenuItem_Click(object sender, EventArgs e)
        {
            this.refreshAdtButton_Click(this.refreshAdtButton, e);
        }

        private void positionAdtTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                this.adtPnlNavButtons(this.movePreviousAdtButton, ex);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
            {
                this.adtPnlNavButtons(this.moveNextAdtButton, ex);
            }
        }

        private void searchForAdtTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                this.refreshAdtButton_Click(this.refreshAdtButton, ex);
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

        #endregion

        private void addUserButton_Click(object sender, EventArgs e)
        {
            if (Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[8]) == false)
            {
                Global.myNwMainFrm.cmmnCode.showMsg("You don't have permission to perform" +
                  " this action!\nContact your System Administrator!", 0);
                return;
            }
            addUserDiag nwDiag = new addUserDiag();
            DialogResult dgres = nwDiag.ShowDialog();
            if (dgres == DialogResult.OK)
            {
                this.loadUserPanel();
            }
        }

        private void editUserButton_Click(object sender, EventArgs e)
        {
            if (Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[9]) == false)
            {
                Global.myNwMainFrm.cmmnCode.showMsg("You don't have permission to perform" +
                  " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.userListView.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Please select a User!", "Rhomicom Message!",
                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            addUserDiag nwDiag = new addUserDiag();
            nwDiag.uNameTextBox.ReadOnly = true;
            nwDiag.uNameTextBox.Text = this.userListView.SelectedItems[0].SubItems[1].Text;
            if (long.Parse(this.userListView.SelectedItems[0].SubItems[4].Text) > 0)
            {
                //nwDiag.ownerTypComboBox.SelectedItem = "Person";
                nwDiag.ownerTextBox.Text = this.userListView.SelectedItems[0].SubItems[2].Text;
                nwDiag.prsnIDTextBox.Text = this.userListView.SelectedItems[0].SubItems[4].Text;
            }
            if (long.Parse(this.userListView.SelectedItems[0].SubItems[5].Text) > 0)
            {
                //nwDiag.ownerTypComboBox.SelectedItem = "Customer";
                nwDiag.asgndCstmrTextBox.Text = this.userListView.SelectedItems[0].SubItems[2].Text;
                nwDiag.asgndCstmrIDTextBox.Text = this.userListView.SelectedItems[0].SubItems[5].Text;
            }
            nwDiag.usrVldStrtDteTextBox.Text = this.usrVldStrtDteTextBox.Text;
            nwDiag.usrVldEndDteTextBox.Text = this.usrVldEndDteTextBox.Text;
            nwDiag.modulesBaughtComboBox.Text = this.userListView.SelectedItems[0].SubItems[6].Text;
            DialogResult dgres = nwDiag.ShowDialog();
            if (dgres == DialogResult.OK)
            {
                this.getUsrPnlData();
            }
        }

        private void addEdtUsrRoleButton_Click(object sender, EventArgs e)
        {
            if (Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[8]) == false)
            {
                Global.myNwMainFrm.cmmnCode.showMsg("You don't have permission to perform" +
                  " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.userListView.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Please select a User!", "Rhomicom Message!",
                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            addUserRole nwDiag = new addUserRole();
            nwDiag.brght_usrNm = this.userListView.SelectedItems[0].SubItems[1].Text;
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
            this.populateUsrRoles();
        }

        private void addRoleButton_Click(object sender, EventArgs e)
        {
            if (Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[10]) == false)
            {
                Global.myNwMainFrm.cmmnCode.showMsg("You don't have permission to perform" +
                  " this action!\nContact your System Administrator!", 0);
                return;
            }
            addRoleDiag nwDiag = new addRoleDiag();
            nwDiag.brght_role_id = -1;
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
            }
            this.loadRolesPanel();
        }

        private void editRoleButton_Click(object sender, EventArgs e)
        {
            if (Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[11]) == false)
            {
                Global.myNwMainFrm.cmmnCode.showMsg("You don't have permission to perform" +
                  " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.rolesListView.SelectedItems.Count > 0)
            {
                addRoleDiag nwDiag = new addRoleDiag();
                nwDiag.brght_role_id = int.Parse(this.rolesListView.SelectedItems[0].SubItems[4].Text);
                nwDiag.roleNameTextBox.Text = this.rolesListView.SelectedItems[0].SubItems[1].Text;
                nwDiag.roleVldStrtDteTextBox.Text = this.rolesListView.SelectedItems[0].SubItems[2].Text;
                nwDiag.roleVldEndDteTextBox.Text = this.rolesListView.SelectedItems[0].SubItems[3].Text;
                nwDiag.checkBox1.Checked = (this.rolesListView.SelectedItems[0].SubItems[5].Text == "YES") ? true : false;
                DialogResult dgRes = nwDiag.ShowDialog();
                if (dgRes == DialogResult.OK)
                {
                }
                this.populateRoleLstVw();
            }
            else
            {
                MessageBox.Show("Please select a Role first!", "Rhomicom Message!",
          MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void addEditRoleButton_Click(object sender, EventArgs e)
        {
            if (Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[10]) == false)
            {
                Global.myNwMainFrm.cmmnCode.showMsg("You don't have permission to perform" +
                  " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.rolesListView.SelectedItems.Count > 0)
            {
                addRolePrvldgDiag nwDiag = new addRolePrvldgDiag();
                nwDiag.brght_role_id = int.Parse(this.rolesListView.SelectedItems[0].SubItems[4].Text);
                if (this.rolePrvldgsListView.SelectedItems.Count > 0)
                {
                    nwDiag.searchForTextBox.Text = this.rolePrvldgsListView.SelectedItems[0].SubItems[2].Text;
                }
                DialogResult dgRes = nwDiag.ShowDialog();
                if (dgRes == DialogResult.OK)
                {
                }
                this.populateRolesPrvldgs();
            }
            else
            {
                MessageBox.Show("Please select a Role first!", "Rhomicom Message!",
                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void addEditExtInfButton_Click(object sender, EventArgs e)
        {
            if (Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[20]) == false)
            {
                Global.myNwMainFrm.cmmnCode.showMsg("You don't have permission to perform" +
                  " this action!\nContact your System Administrator!", 0);
                return;
            }
            int[] selValuesIDs = new int[this.extInfLabelListView.Items.Count];
            for (int j = 0; j < this.extInfLabelListView.Items.Count; j++)
            {
                selValuesIDs[j] = int.Parse(this.extInfLabelListView.Items[j].SubItems[3].Text);
            }
            DialogResult dgRes = Global.myNwMainFrm.cmmnCode.showPssblValDiag(
             Global.myNwMainFrm.cmmnCode.getLovID("Extra Information Labels"), ref selValuesIDs, false, true);//
            if (dgRes == DialogResult.OK)
            {
                for (int i = 0; i < selValuesIDs.Length; i++)
                {
                    if (Global.doesTableHvThsExtrInfoLbl(long.Parse(this.extInfSubGroupsListView.SelectedItems[0].SubItems[5].Text),
                      selValuesIDs[i]) == false)
                    {
                        Global.createAllwdExtraInfos(
                         long.Parse(this.extInfSubGroupsListView.SelectedItems[0].SubItems[5].Text),
                         selValuesIDs[i], true);
                    }
                }
            }
            this.populateSubgroupsExtInf();
        }

        private void enableDisableButton_Click(object sender, EventArgs e)
        {
            if (Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[20]) == false)
            {
                Global.myNwMainFrm.cmmnCode.showMsg("You don't have permission to perform" +
                  " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.extInfLabelListView.SelectedItems.Count == 0)
            {
                Global.myNwMainFrm.cmmnCode.showMsg("Please select an item first", 0);
                return;
            }
            for (int i = 0; i < this.extInfLabelListView.SelectedItems.Count; i++)
            {
                if (bool.Parse(this.extInfLabelListView.SelectedItems[i].SubItems[2].Text) == true)
                {
                    Global.enblDsblAllwdExtraInfos(long.Parse(this.extInfLabelListView.SelectedItems[i].SubItems[4].Text), false);
                }
                else
                {
                    Global.enblDsblAllwdExtraInfos(long.Parse(this.extInfLabelListView.SelectedItems[i].SubItems[4].Text), true);
                }
            }
            this.populateSubgroupsExtInf();
        }

        private void delLblButton_Click(object sender, EventArgs e)
        {
            if (Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[21]) == false)
            {
                Global.myNwMainFrm.cmmnCode.showMsg("You don't have permission to perform" +
                  " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.extInfLabelListView.SelectedItems.Count == 0)
            {
                Global.myNwMainFrm.cmmnCode.showMsg("Please select an item first", 0);
                return;
            }
            if (Global.myNwMainFrm.cmmnCode.showMsg("Are you sure you want to " +
             "delete the Selected Item(s)?", 1) == DialogResult.No)
            {
                return;
            }
            for (int i = 0; i < this.extInfLabelListView.SelectedItems.Count; i++)
            {
                Global.deleteAllwdExtraInfos(long.Parse(this.extInfLabelListView.SelectedItems[i].SubItems[4].Text));
            }
            this.populateSubgroupsExtInf();
        }

        private void bckpButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.pgDirTextBox.Text == "")
                {
                    Global.myNwMainFrm.cmmnCode.showMsg("Please select the location of the PG_DUMP.EXE File!", 0);
                    return;
                }
                this.folderBrowserDialog1.Description = "Database Backup Folder";
                this.folderBrowserDialog1.ShowNewFolderButton = true;
                this.folderBrowserDialog1.RootFolder = Environment.SpecialFolder.Desktop;
                this.folderBrowserDialog1.SelectedPath = this.bckpFileDirTextBox.Text;
                DialogResult dgRes = this.folderBrowserDialog1.ShowDialog();
                if (dgRes == DialogResult.OK)
                {
                    this.bckpFileDirTextBox.Text = this.folderBrowserDialog1.SelectedPath;
                }

                if (this.bckpFileDirTextBox.Text == "")
                {
                    Global.myNwMainFrm.cmmnCode.showMsg("Please select the location to save", 0);
                    return;
                }
                //C:\Program Files (x86)\PostgreSQL\9.1\bin
                System.IO.StreamWriter sw = new System.IO.StreamWriter(@"DBInfo\DBBackup.bat");
                // Do not change lines / spaces b/w words.
                StringBuilder strSB = new StringBuilder(@"cd /D " + this.pgDirTextBox.Text + "\r\n\r\n");

                string dbnm = CommonCode.CommonCodes.Db_dbase;
                string hostnm = CommonCode.CommonCodes.Db_host;
                string timeStr = Global.myNwMainFrm.cmmnCode.getDB_Date_time().Replace(" ", "").Replace(":", "").Replace("-", "").ToLower();
                strSB.Append("pg_dump.exe --host " + hostnm +
                  " --port " + CommonCode.CommonCodes.Db_port +
                  " --username postgres --format tar --blobs --verbose --file ");
                strSB.Append("\"" + this.bckpFileDirTextBox.Text + "\\" + dbnm + timeStr + ".backup\"");
                strSB.Append(" \"" + dbnm + "\"\r\n\r\n");
                strSB.Append("\r\n\r\nPAUSE");
                sw.WriteLine(strSB);
                sw.Dispose();
                sw.Close();
                System.Diagnostics.Process processDB = System.Diagnostics.Process.Start(@"DBInfo\DBBackup.bat");
                do
                {
                    //dont perform anything
                }
                while (!processDB.HasExited);
                {
                    Global.myNwMainFrm.cmmnCode.showMsg(dbnm.ToUpper() + " Database Backup File created at " + this.bckpFileDirTextBox.Text + "\\" + dbnm + timeStr + ".backup", 3);
                }
            }
            catch (Exception ex)
            {
                Global.myNwMainFrm.cmmnCode.showMsg("Error!\r\n" + ex.Message, 4);
                return;
            }
        }

        private void pgDirButton_Click(object sender, EventArgs e)
        {
            if (this.add_eml_srvs == false && this.edit_eml_srvs == false)
            {
                Global.myNwMainFrm.cmmnCode.showMsg("Must be in ADD/EDIT Mode First!", 0);
                return;
            }

            this.folderBrowserDialog1.Description = "PG_RESTORE/DUMP.EXE Folder";
            this.folderBrowserDialog1.ShowNewFolderButton = false;
            this.folderBrowserDialog1.RootFolder = Environment.SpecialFolder.Desktop;
            this.folderBrowserDialog1.SelectedPath = this.pgDirTextBox.Text;
            DialogResult dgRes = this.folderBrowserDialog1.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
                this.pgDirTextBox.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }

        private void bckpDirButton_Click(object sender, EventArgs e)
        {
            if (this.add_eml_srvs == false && this.edit_eml_srvs == false)
            {
                Global.myNwMainFrm.cmmnCode.showMsg("Must be in ADD/EDIT Mode First!", 0);
                return;
            }
            this.folderBrowserDialog1.ShowNewFolderButton = false;
            this.folderBrowserDialog1.RootFolder = Environment.SpecialFolder.Desktop;
            DialogResult dgRes = this.folderBrowserDialog1.ShowDialog();
            if (dgRes == DialogResult.OK)
            {
                this.bckpFileDirTextBox.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }

        private void restoreButton_Click(object sender, EventArgs e)
        {
            //this.cmnCde.showMsg(CommonCode.CommonCodes.is64BitOperatingSystem.ToString() + this.installPath, 0);
            System.Diagnostics.Process processDB = System.Diagnostics.Process.Start(Application.StartupPath + @"\DBConfig.exe");
            //    try
            //    {
            //      if (this.pgDirTextBox.Text == "")
            //      {
            //        Global.myNwMainFrm.cmmnCode.showMsg("Please select the location of the PG_DUMP.EXE File!", 0);
            //        return;
            //      }
            //      this.openFileDialog1.RestoreDirectory = true;
            //      this.openFileDialog1.Filter = "All Files|*.*|Backup Files|*.backup;";
            //      this.openFileDialog1.FilterIndex = 2;
            //      this.openFileDialog1.Title = "Select an Excel File to Upload...";
            //      this.openFileDialog1.InitialDirectory = this.bckpFileDirTextBox.Text;
            //      this.openFileDialog1.FileName = "";
            //      string srcFile = "";
            //      if (this.openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //      {
            //        srcFile = this.openFileDialog1.FileName;
            //      }
            //      System.IO.StreamWriter sw = new System.IO.StreamWriter(@"DBInfo\DBRestore.bat");
            //      // Do not change lines / spaces b/w words.
            //      StringBuilder strSB = new StringBuilder(@"cd /D " + this.pgDirTextBox.Text + "\r\n\r\n");

            //      string dbnm = Global.myNwMainFrm.cmmnCode.pgSqlConn.Database;
            //      string timeStr = Global.myNwMainFrm.cmmnCode.getDB_Date_time().Replace(" ", "").Replace(":", "").Replace("-", "").ToLower();
            //      Global.myNwMainFrm.cmmnCode.executeGnrlSQL("CREATE DATABASE " + dbnm + timeStr + " " +
            //"WITH OWNER = postgres " +
            //     "ENCODING = 'UTF8' " +
            //     "TABLESPACE = pg_default " +
            //     "LC_COLLATE = 'English_United States.1252' " +
            //     "LC_CTYPE = 'English_United States.1252' " +
            //     "CONNECTION LIMIT = -1");
            //      strSB.Append("pg_restore.exe --host localhost" +
            //        " --port " + Global.myNwMainFrm.cmmnCode.pgSqlConn.Port +
            //        " --username postgres --create --dbname \"" + dbnm + timeStr + "\" --verbose ");
            //      strSB.Append("\"" + srcFile + "\"");
            //      strSB.Append("\r\n\r\nPAUSE");
            //      sw.WriteLine(strSB);
            //      sw.Dispose();
            //      sw.Close();
            //      System.Diagnostics.Process processDB = System.Diagnostics.Process.Start(@"DBInfo\DBRestore.bat");
            //      do
            //      {//dont perform anything
            //      }
            //      while (!processDB.HasExited);
            //      {
            //        Global.myNwMainFrm.cmmnCode.showMsg("Restoration of Backup File to Database " + dbnm.ToUpper() + timeStr.ToUpper() + " Completed", 3);
            //      }
            //    }
            //    catch (Exception ex)
            //    {
            //      Global.myNwMainFrm.cmmnCode.showMsg("Error!\r\n" + ex.Message, 4);
            //      return;
            //    }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.waitLabel.Visible = true;
            System.Windows.Forms.Application.DoEvents();
            this.timer1.Enabled = false;
            System.Windows.Forms.Application.DoEvents();
            Global.refreshRqrdVrbls();
            this.changeOrg();
            if (Global.myNwMainFrm.cmmnCode.Org_id > 0)
            {
                Global.updtOrgAccntCurrID(Global.myNwMainFrm.cmmnCode.Org_id,
                  Global.myNwMainFrm.cmmnCode.getOrgFuncCurID(Global.myNwMainFrm.cmmnCode.Org_id));
            }
            System.Windows.Forms.Application.DoEvents();
            Global.createSysLovs();
            System.Windows.Forms.Application.DoEvents();
            Global.createSysLovsPssblVals();
            Global.createSampleExtraInfos();

            System.Windows.Forms.Application.DoEvents();
            Global.mySecurity.loadOtherMdlsRoles();

            this.waitLabel.Visible = false;
            Global.myNwMainFrm.cmmnCode.showMsg("Remember to Go to General Setup to View the " +
              "\r\nContent of all Accounting LOVs Especially \r\n1. Transactions Date Limit 1" +
            "\r\n2. Transactions Date Limit 2 \r\n3.Transactions not Allowed Days " +
            "\r\n4.Transactions not Allowed Dates\r\n5.Transactions Amount Breakdown Descriptions", 3);
        }

        private void userListView_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.Control && e.KeyCode == Keys.S)       // Ctrl-S Save
            {
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if (e.Control && e.KeyCode == Keys.N)       // Ctrl-S Save
            {
                // do what you want here
                this.addUserButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if (e.Control && e.KeyCode == Keys.E)       // Ctrl-S Save
            {
                // do what you want here
                this.editUserButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if ((e.Control && e.KeyCode == Keys.R) || e.KeyCode == Keys.F5)      // Ctrl-S Save
            {
                // do what you want here
                this.refreshUserButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if (e.Control && e.KeyCode == Keys.Delete)
            {
                this.delUserButton.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                e.Handled = false;
                e.SuppressKeyPress = false;  // stops bing! also sets handeled which stop event bubbling
                Global.myNwMainFrm.cmmnCode.listViewKeyDown(this.userListView, e);
            }
        }

        private void userRoleslistView_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.Control && e.KeyCode == Keys.S)       // Ctrl-S Save
            {
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if (e.Control && e.KeyCode == Keys.N)       // Ctrl-S Save
            {
                // do what you want here
                this.addEdtUsrRoleButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if (e.Control && e.KeyCode == Keys.E)       // Ctrl-S Save
            {
                // do what you want here
                this.addEdtUsrRoleButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if ((e.Control && e.KeyCode == Keys.R) || e.KeyCode == Keys.F5)      // Ctrl-S Save
            {
                // do what you want here
                this.refreshUserButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if (e.Control && e.KeyCode == Keys.Delete)
            {
                this.delUserButton.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                e.Handled = false;
                e.SuppressKeyPress = false;  // stops bing! also sets handeled which stop event bubbling
                Global.myNwMainFrm.cmmnCode.listViewKeyDown(this.userRoleslistView, e);
            }
        }

        private void rolesListView_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.Control && e.KeyCode == Keys.S)       // Ctrl-S Save
            {
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if (e.Control && e.KeyCode == Keys.N)       // Ctrl-S Save
            {
                // do what you want here
                this.addRoleButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if (e.Control && e.KeyCode == Keys.E)       // Ctrl-S Save
            {
                // do what you want here
                this.editRoleButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if ((e.Control && e.KeyCode == Keys.R) || e.KeyCode == Keys.F5)      // Ctrl-S Save
            {
                // do what you want here
                this.refreshRoleButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if (e.Control && e.KeyCode == Keys.Delete)
            {
                //this.delUserButton.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                e.Handled = false;
                e.SuppressKeyPress = false;  // stops bing! also sets handeled which stop event bubbling
                Global.myNwMainFrm.cmmnCode.listViewKeyDown(this.rolesListView, e);
            }
        }

        private void rolePrvldgsListView_KeyDown(object sender, KeyEventArgs e)
        {
            EventArgs ex = new EventArgs();
            if (e.Control && e.KeyCode == Keys.S)       // Ctrl-S Save
            {
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if (e.Control && e.KeyCode == Keys.N)       // Ctrl-S Save
            {
                // do what you want here
                this.addEditRoleButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if (e.Control && e.KeyCode == Keys.E)       // Ctrl-S Save
            {
                // do what you want here
                this.addEditRoleButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if ((e.Control && e.KeyCode == Keys.R) || e.KeyCode == Keys.F5)      // Ctrl-S Save
            {
                // do what you want here
                this.refreshRoleButton.PerformClick();
                e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
            }
            else if (e.Control && e.KeyCode == Keys.Delete)
            {
                //this.delUserButton.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else
            {
                e.Handled = false;
                e.SuppressKeyPress = false;  // stops bing! also sets handeled which stop event bubbling
                Global.myNwMainFrm.cmmnCode.listViewKeyDown(this.rolePrvldgsListView, e);
            }
        }

        private void modulesListView_KeyDown(object sender, KeyEventArgs e)
        {
            Global.myNwMainFrm.cmmnCode.listViewKeyDown(this.modulesListView, e);
        }

        private void modulePrvldgListView_KeyDown(object sender, KeyEventArgs e)
        {
            Global.myNwMainFrm.cmmnCode.listViewKeyDown(this.modulePrvldgListView, e);
        }

        private void extInfoModuleListView_KeyDown(object sender, KeyEventArgs e)
        {
            Global.myNwMainFrm.cmmnCode.listViewKeyDown(this.extInfoModuleListView, e);
        }

        private void extInfSubGroupsListView_KeyDown(object sender, KeyEventArgs e)
        {
            Global.myNwMainFrm.cmmnCode.listViewKeyDown(this.extInfSubGroupsListView, e);
        }

        private void extInfLabelListView_KeyDown(object sender, KeyEventArgs e)
        {
            Global.myNwMainFrm.cmmnCode.listViewKeyDown(this.extInfLabelListView, e);
        }

        private void loginsListView_KeyDown(object sender, KeyEventArgs e)
        {
            Global.myNwMainFrm.cmmnCode.listViewKeyDown(this.loginsListView, e);
        }

        private void loadRolesButton_Click(object sender, EventArgs e)
        {
            this.timer1.Interval = 100;
            this.timer1.Enabled = true;
        }

        private void loadLOVsButton_Click(object sender, EventArgs e)
        {
            this.timer1.Interval = 100;
            this.timer1.Enabled = true;
        }

        private void crntOrgButton_Click(object sender, EventArgs e)
        {
            string[] selVals = new string[1];
            selVals[0] = this.crntOrgIDTextBox.Text;
            DialogResult dgRes = Global.myNwMainFrm.cmmnCode.showPssblValDiag(
              Global.myNwMainFrm.cmmnCode.getLovID("Organisations"), ref selVals, true, true);
            if (dgRes == DialogResult.OK)
            {
                this.curOrgPictureBox.Image.Dispose();
                this.curOrgPictureBox.Image = SystemAdministration.Properties.Resources.blank;
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.crntOrgIDTextBox.Text = selVals[i];
                    this.crntOrgTextBox.Text = Global.myNwMainFrm.cmmnCode.getOrgName(int.Parse(selVals[i]));
                    //Global.myNwMainFrm.cmmnCode.getDBImageFile(this.crntOrgIDTextBox.Text + ".png",
                    //  0, ref this.curOrgPictureBox);
                }
            }

            int orgID = Global.myNwMainFrm.cmmnCode.Org_id;
            if (orgID > 0)
            {
                //this.changeOrg();
                Global.updtOrgAccntCurrID(orgID, Global.myNwMainFrm.cmmnCode.getOrgFuncCurID(orgID));
            }
        }

        private void searchForUserTextBox_Click(object sender, EventArgs e)
        {
            this.searchForUserTextBox.SelectAll();
        }

        private void searchForRoleTextBox_Click(object sender, EventArgs e)
        {
            this.searchForRoleTextBox.SelectAll();
        }

        private void searchForMdlTextBox_Click(object sender, EventArgs e)
        {
            this.searchForMdlTextBox.SelectAll();
        }

        private void searchForExtInfTextBox_Click(object sender, EventArgs e)
        {
            this.searchForExtInfTextBox.SelectAll();
        }

        private void searchForPlcyTextBox_Click(object sender, EventArgs e)
        {
            this.searchForPlcyTextBox.SelectAll();
        }

        private void searchForEmlSvrTextBox_Click(object sender, EventArgs e)
        {
            this.searchForEmlSvrTextBox.SelectAll();
        }

        private void searchForAdtTextBox_Click(object sender, EventArgs e)
        {
            this.searchForAdtTextBox.SelectAll();
        }

        private void searchForLgnsTextBox_Click(object sender, EventArgs e)
        {
            this.searchForLgnsTextBox.SelectAll();
        }

        private void usrVldStrtDteTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!this.obey_user_evnts)
            {
                return;
            }
            this.txtChngd = true;
        }

        private void usrVldStrtDteTextBox_Leave(object sender, EventArgs e)
        {
            if (this.txtChngd == false)
            {
                return;
            }
            this.txtChngd = false;
            TextBox mytxt = (TextBox)sender;
            this.obey_user_evnts = false;
            this.srchWrd = mytxt.Text;
            if (!mytxt.Text.Contains("%"))
            {
                this.srchWrd = "%" + this.srchWrd.Replace(" ", "%") + "%";
            }

            if (mytxt.Name == "usrVldStrtDteTextBox")
            {
                this.usrVldStrtDteTextBox.Text = Global.myNwMainFrm.cmmnCode.checkNFormatDate(this.usrVldStrtDteTextBox.Text);
                if (this.userListView.SelectedItems.Count > 0)
                {
                    Global.changeUsrVldStrDate(this.userListView.SelectedItems[0].SubItems[1].Text,
                      this.usrVldStrtDteTextBox.Text);
                }
            }
            else if (mytxt.Name == "usrVldEndDteTextBox")
            {
                this.usrVldEndDteTextBox.Text = Global.myNwMainFrm.cmmnCode.checkNFormatDate(this.usrVldEndDteTextBox.Text);
                if (this.userListView.SelectedItems.Count > 0)
                {
                    Global.changeUsrVldEndDate(this.userListView.SelectedItems[0].SubItems[1].Text,
                      this.usrVldEndDteTextBox.Text);
                }
            }
            this.srchWrd = "%";
            this.obey_user_evnts = true;
            this.txtChngd = false;
        }

        private void delUserButton_Click(object sender, EventArgs e)
        {
            if (Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[9]) == false)
            {
                Global.myNwMainFrm.cmmnCode.showMsg("You don't have permission to perform" +
                  " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.userListView.SelectedItems.Count == 0)
            {
                Global.myNwMainFrm.cmmnCode.showMsg("Please select an item first", 0);
                return;
            }
            for (int i = 0; i < this.userListView.SelectedItems.Count; i++)
            {
                long u_ID = long.Parse(this.userListView.SelectedItems[i].SubItems[3].Text);
                if (Global.hasUsrEvrLgdIn(u_ID) == true)
                {
                    Global.myNwMainFrm.cmmnCode.showMsg("Cannot Delete a User that has logged in or attempted it before!", 0);
                    return;
                }
            }
            if (Global.myNwMainFrm.cmmnCode.showMsg("Are you sure you want to " +
             "delete the Selected User(s)?", 1) == DialogResult.No)
            {
                return;
            }
            for (int i = 0; i < this.userListView.SelectedItems.Count; i++)
            {
                long u_ID = long.Parse(this.userListView.SelectedItems[i].SubItems[3].Text);
                Global.deleteUser(u_ID, this.userListView.SelectedItems[i].SubItems[1].Text);
            }
            this.loadUserPanel();
        }

        private void deleteSrvrButton_Click(object sender, EventArgs e)
        {
            if (Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[15]) == false)
            {
                Global.myNwMainFrm.cmmnCode.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.emlSrvrIDTextBox.Text == "" || this.emlSrvrIDTextBox.Text == "-1")
            {
                Global.myNwMainFrm.cmmnCode.showMsg("Please select the Server Setting to DELETE!", 0);
                return;
            }
            if (this.isDfltYesEmlSvrCheckBox.Checked || this.enforceFTPCheckBox.Checked)
            {
                Global.myNwMainFrm.cmmnCode.showMsg("This Server Setting is in Use hence cannot be DELETED!", 0);
                return;
            }
            if (Global.myNwMainFrm.cmmnCode.showMsg("Are you sure you want to DELETE the selected Server Setting?" +
       "\r\nThis action cannot be undone!", 1) == DialogResult.No)
            {
                //Global.myNwMainFrm.cmmnCode.showMsg("Operation Cancelled!", 4);
                return;
            }
            Global.myNwMainFrm.cmmnCode.deleteGnrlRecs(long.Parse(this.emlSrvrIDTextBox.Text),
      "Email Server Name = " + this.smtpClientTextBox.Text, "sec.sec_email_servers", "server_id");

            this.loadEmailSrvrPanel();
        }

        private void deletePolicyButton_Click(object sender, EventArgs e)
        {
            if (Global.myNwMainFrm.cmmnCode.test_prmssns(Global.dfltPrvldgs[13]) == false)
            {
                Global.myNwMainFrm.cmmnCode.showMsg("You don't have permission to perform" +
                 " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.plcyIDTextBox.Text == "" || this.plcyIDTextBox.Text == "-1")
            {
                Global.myNwMainFrm.cmmnCode.showMsg("Please select the Security Policy to DELETE!", 0);
                return;
            }
            if (this.isDefltYesCheckBox.Checked)
            {
                Global.myNwMainFrm.cmmnCode.showMsg("This Security Policy is in Use hence cannot be DELETED!", 0);
                return;
            }
            if (Global.myNwMainFrm.cmmnCode.showMsg("Are you sure you want to DELETE the selected Security Policy?" +
       "\r\nThis action cannot be undone!", 1) == DialogResult.No)
            {
                //Global.myNwMainFrm.cmmnCode.showMsg("Operation Cancelled!", 4);
                return;
            }
            Global.myNwMainFrm.cmmnCode.deleteGnrlRecs(long.Parse(this.plcyIDTextBox.Text),
      "Security Policy Name = " + this.policyNmTextBox.Text, "sec.sec_security_policies", "policy_id");

            this.loadPolicyPanel();

        }
    }
}

