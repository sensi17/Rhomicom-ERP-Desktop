namespace SystemAdministration.Forms
	{
	partial class mainForm
		{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
			{
			if (disposing && (components != null))
				{
				components.Dispose();
				}
			base.Dispose(disposing);
			}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
			{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeVWContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.hideTreevwMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator123 = new System.Windows.Forms.ToolStripSeparator();
            this.leftTreeView = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.accDndLabel = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.glsLabel1 = new glsLabel.glsLabel();
            this.waitLabel = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.usersPanel = new System.Windows.Forms.Panel();
            this.mailLabel = new System.Windows.Forms.Label();
            this.toolStrip8 = new System.Windows.Forms.ToolStrip();
            this.addEdtUsrRoleButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip9 = new System.Windows.Forms.ToolStrip();
            this.addUserButton = new System.Windows.Forms.ToolStripButton();
            this.editUserButton = new System.Windows.Forms.ToolStripButton();
            this.delUserButton = new System.Windows.Forms.ToolStripButton();
            this.imprtUsersButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.changePswdAutoButton = new System.Windows.Forms.Button();
            this.agePswdTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.changePswdManButton = new System.Windows.Forms.Button();
            this.isExpiredCheckBox = new System.Windows.Forms.CheckBox();
            this.lastPwdChngeTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.isTempCheckBox = new System.Windows.Forms.CheckBox();
            this.exprtUsersButton = new System.Windows.Forms.Button();
            this.userRoleslistView = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.userRolesContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addUserRoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exptUsrRolesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshUsrRoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recordHistoryUsrRoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewSQLUsrRoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.usrDte2Button = new System.Windows.Forms.Button();
            this.usrDte1Button = new System.Windows.Forms.Button();
            this.usrVldEndDteTextBox = new System.Windows.Forms.TextBox();
            this.usrVldStrtDteTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lastLoginAtmptTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.failedLgnAtmptTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.isLockedCheckBox = new System.Windows.Forms.CheckBox();
            this.isSuspendedCheckBox = new System.Windows.Forms.CheckBox();
            this.navToolStrip = new System.Windows.Forms.ToolStrip();
            this.moveFirstUserButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.movePreviousUserButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.positionUserTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.totalRecUserLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.moveNextUserButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.moveLastUserButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.dsplySizeUserComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
            this.searchForUserTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator19 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator20 = new System.Windows.Forms.ToolStripSeparator();
            this.searchInUserComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator21 = new System.Windows.Forms.ToolStripSeparator();
            this.refreshUserButton = new System.Windows.Forms.ToolStripButton();
            this.userListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader60 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.usersContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exptUsrsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshUsersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recordHistoryUsrsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewSQLUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label38 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.rolesPanel = new System.Windows.Forms.Panel();
            this.toolStrip10 = new System.Windows.Forms.ToolStrip();
            this.addEditRoleButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip11 = new System.Windows.Forms.ToolStrip();
            this.addRoleButton = new System.Windows.Forms.ToolStripButton();
            this.editRoleButton = new System.Windows.Forms.ToolStripButton();
            this.loadRolesButton = new System.Windows.Forms.ToolStripButton();
            this.panel6 = new System.Windows.Forms.Panel();
            this.glsLabel5 = new glsLabel.glsLabel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.glsLabel4 = new glsLabel.glsLabel();
            this.rolePrvldgsListView = new System.Windows.Forms.ListView();
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader17 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader18 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader20 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader21 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader19 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader22 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rolesPrvlgsContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addRlPrvldgMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator28 = new System.Windows.Forms.ToolStripSeparator();
            this.exptRolePrvldgMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshRlPrvldgMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recHstryRlPrvldgMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vwSQLRlPrvldgMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rolesListView = new System.Windows.Forms.ListView();
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader61 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rolesContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addRoleMainMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editRoleMainMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator27 = new System.Windows.Forms.ToolStripSeparator();
            this.exptRolesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshRoleMainMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recHstryRoleMainMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vwSQLRoleMainMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.moveFirstRoleButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.movePreviousRoleButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.positionRoleTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.totalRecRoleLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.moveNextRoleButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.moveLastRoleButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.dsplySizeRoleComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel6 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator23 = new System.Windows.Forms.ToolStripSeparator();
            this.searchForRoleTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator24 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel7 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator25 = new System.Windows.Forms.ToolStripSeparator();
            this.searchInRoleComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator26 = new System.Windows.Forms.ToolStripSeparator();
            this.refreshRoleButton = new System.Windows.Forms.ToolStripButton();
            this.label8 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.modulesPanel = new System.Windows.Forms.Panel();
            this.modulePrvldgListView = new System.Windows.Forms.ListView();
            this.columnHeader25 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader26 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader31 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.modulePrvlgContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exptMdlPrvldgMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshMdlPrvldgMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator54 = new System.Windows.Forms.ToolStripSeparator();
            this.vwSqlMdlPrvldgMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel11 = new System.Windows.Forms.Panel();
            this.glsLabel10 = new glsLabel.glsLabel();
            this.modulesListView = new System.Windows.Forms.ListView();
            this.columnHeader23 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader24 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader28 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader29 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader30 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader27 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.modulesContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exptMdlMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshMdlMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator53 = new System.Windows.Forms.ToolStripSeparator();
            this.vwSQLMdlMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel10 = new System.Windows.Forms.Panel();
            this.glsLabel9 = new glsLabel.glsLabel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.moveFirstMdlButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator29 = new System.Windows.Forms.ToolStripSeparator();
            this.movePreviousMdlButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator30 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.positionMdlTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.totalRecMdlLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator31 = new System.Windows.Forms.ToolStripSeparator();
            this.moveNextMdlButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator32 = new System.Windows.Forms.ToolStripSeparator();
            this.moveLastMdlButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator34 = new System.Windows.Forms.ToolStripSeparator();
            this.dsplySizeMdlComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator35 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel9 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator37 = new System.Windows.Forms.ToolStripSeparator();
            this.searchForMdlTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator38 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel10 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator39 = new System.Windows.Forms.ToolStripSeparator();
            this.searchInMdlComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator40 = new System.Windows.Forms.ToolStripSeparator();
            this.refreshMdlButton = new System.Windows.Forms.ToolStripButton();
            this.label9 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.extraInfoPanel = new System.Windows.Forms.Panel();
            this.toolStrip12 = new System.Windows.Forms.ToolStrip();
            this.addEditExtInfButton = new System.Windows.Forms.ToolStripButton();
            this.enableDisableButton = new System.Windows.Forms.ToolStripButton();
            this.delLblButton = new System.Windows.Forms.ToolStripButton();
            this.extInfLabelListView = new System.Windows.Forms.ListView();
            this.columnHeader51 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader52 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader58 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader53 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader59 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.extInfLabelContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addEditExtInfMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator87 = new System.Windows.Forms.ToolStripSeparator();
            this.enableDisableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteLaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator92 = new System.Windows.Forms.ToolStripSeparator();
            this.exptInfLblMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshExtInfLblMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vwSQLExtInfLblMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recordHistoryExtInfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel23 = new System.Windows.Forms.Panel();
            this.glsLabel21 = new glsLabel.glsLabel();
            this.extInfSubGroupsListView = new System.Windows.Forms.ListView();
            this.columnHeader46 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader47 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader48 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader55 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader57 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader56 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.subGroupsContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exptSubGrpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshSubGrpsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator85 = new System.Windows.Forms.ToolStripSeparator();
            this.vwSQLSubGrpsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel20 = new System.Windows.Forms.Panel();
            this.glsLabel18 = new glsLabel.glsLabel();
            this.extInfoModuleListView = new System.Windows.Forms.ListView();
            this.columnHeader49 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader50 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader54 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.extInfMdlContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exptExtInfMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshExtInfMdlMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator78 = new System.Windows.Forms.ToolStripSeparator();
            this.viewSQLExtInfMdlMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel21 = new System.Windows.Forms.Panel();
            this.glsLabel19 = new glsLabel.glsLabel();
            this.toolStrip7 = new System.Windows.Forms.ToolStrip();
            this.moveFirstExtInfButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator33 = new System.Windows.Forms.ToolStripSeparator();
            this.movePreviousExtInfButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator36 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel14 = new System.Windows.Forms.ToolStripLabel();
            this.positionExtInfTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.totalRecExtInfLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator46 = new System.Windows.Forms.ToolStripSeparator();
            this.moveNextExtInfButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator47 = new System.Windows.Forms.ToolStripSeparator();
            this.moveLastExtInfButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator48 = new System.Windows.Forms.ToolStripSeparator();
            this.dsplySizeExtInfComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator67 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel22 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator69 = new System.Windows.Forms.ToolStripSeparator();
            this.searchForExtInfTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator74 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel25 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator75 = new System.Windows.Forms.ToolStripSeparator();
            this.searchInExtInfComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator76 = new System.Windows.Forms.ToolStripSeparator();
            this.refreshExtInfoButton = new System.Windows.Forms.ToolStripButton();
            this.label36 = new System.Windows.Forms.Label();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.policyPanel = new System.Windows.Forms.Panel();
            this.auditTblsListView = new System.Windows.Forms.ListView();
            this.columnHeader32 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader33 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader34 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader35 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader36 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader37 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.plcyMdlsContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editPlcyMdlMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator61 = new System.Windows.Forms.ToolStripSeparator();
            this.exptPlcyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshPlcyMdlsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recHstryPlcyMdlsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vwSQLPlcyMdlsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label26 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.sessionNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.wildNoCheckBox = new System.Windows.Forms.CheckBox();
            this.wildYesCheckBox = new System.Windows.Forms.CheckBox();
            this.digitsNoCheckBox = new System.Windows.Forms.CheckBox();
            this.digitsYesCheckBox = new System.Windows.Forms.CheckBox();
            this.smallNoCheckBox = new System.Windows.Forms.CheckBox();
            this.smallYesCheckBox = new System.Windows.Forms.CheckBox();
            this.capsNoCheckBox = new System.Windows.Forms.CheckBox();
            this.capsYesCheckBox = new System.Windows.Forms.CheckBox();
            this.label23 = new System.Windows.Forms.Label();
            this.combinatnsComboBox = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.allwRptnNoCheckBox = new System.Windows.Forms.CheckBox();
            this.allwRptnYesCheckBox = new System.Windows.Forms.CheckBox();
            this.allwUnmNoCheckBox = new System.Windows.Forms.CheckBox();
            this.allwUnmYesCheckBox = new System.Windows.Forms.CheckBox();
            this.autoUnlkTmNmUpDown = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.faildLgnCntNmUpDown = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.maxLenPswdNmUpDown = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.oldPswdCntNmUpDown = new System.Windows.Forms.NumericUpDown();
            this.label18 = new System.Windows.Forms.Label();
            this.minLenPswdNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label19 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.mxNoRecsNmUpDown = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.expryDaysNmUpDown = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.isDefltNoCheckBox = new System.Windows.Forms.CheckBox();
            this.isDefltYesCheckBox = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.policyNmTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.plcyIDTextBox = new System.Windows.Forms.TextBox();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.addPlcyButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator59 = new System.Windows.Forms.ToolStripSeparator();
            this.editPlcyButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator57 = new System.Windows.Forms.ToolStripSeparator();
            this.savePlcyButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator55 = new System.Windows.Forms.ToolStripSeparator();
            this.refreshPlcyButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.vwSQLPlcyButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator56 = new System.Windows.Forms.ToolStripSeparator();
            this.recHstryPlcyButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator58 = new System.Windows.Forms.ToolStripSeparator();
            this.moveFirstPlcyButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator41 = new System.Windows.Forms.ToolStripSeparator();
            this.movePreviousPlcyButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator42 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel8 = new System.Windows.Forms.ToolStripLabel();
            this.positionPlcyTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.totalRecPlcyLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator43 = new System.Windows.Forms.ToolStripSeparator();
            this.moveNextPlcyButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator44 = new System.Windows.Forms.ToolStripSeparator();
            this.moveLastPlcyButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator45 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel12 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator49 = new System.Windows.Forms.ToolStripSeparator();
            this.searchForPlcyTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator50 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel13 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator51 = new System.Windows.Forms.ToolStripSeparator();
            this.searchInPlcyComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.label27 = new System.Windows.Forms.Label();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.emailServerPanel = new System.Windows.Forms.Panel();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.smsDataGridView = new System.Windows.Forms.DataGridView();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.crntOrgTextBox = new System.Windows.Forms.TextBox();
            this.toolStrip6 = new System.Windows.Forms.ToolStrip();
            this.addEmlSvrButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator114 = new System.Windows.Forms.ToolStripSeparator();
            this.editEmlSvrButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator112 = new System.Windows.Forms.ToolStripSeparator();
            this.saveEmlSvrButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator111 = new System.Windows.Forms.ToolStripSeparator();
            this.refreshEmlSvrButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator22 = new System.Windows.Forms.ToolStripSeparator();
            this.recHstryEmlSvrButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator113 = new System.Windows.Forms.ToolStripSeparator();
            this.vwSQLEmlSvrButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator110 = new System.Windows.Forms.ToolStripSeparator();
            this.moveFirstEmlSvrButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator98 = new System.Windows.Forms.ToolStripSeparator();
            this.movePreviousEmlSvrButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator99 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel21 = new System.Windows.Forms.ToolStripLabel();
            this.positionEmlSvrTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.totalRecEmlSvrLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator100 = new System.Windows.Forms.ToolStripSeparator();
            this.moveNextEmlSvrButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator101 = new System.Windows.Forms.ToolStripSeparator();
            this.moveLastEmlSvrButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator104 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel23 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator106 = new System.Windows.Forms.ToolStripSeparator();
            this.searchForEmlSvrTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator107 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel24 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator108 = new System.Windows.Forms.ToolStripSeparator();
            this.searchInEmlSvrComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator109 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.label58 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.loadLOVsButton = new System.Windows.Forms.Button();
            this.restoreButton = new System.Windows.Forms.Button();
            this.bckpButton = new System.Windows.Forms.Button();
            this.bckpDirButton = new System.Windows.Forms.Button();
            this.pgDirButton = new System.Windows.Forms.Button();
            this.bckpFileDirTextBox = new System.Windows.Forms.TextBox();
            this.pgDirTextBox = new System.Windows.Forms.TextBox();
            this.label48 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.timeoutComboBox = new System.Windows.Forms.ComboBox();
            this.label46 = new System.Windows.Forms.Label();
            this.baudRateComboBox = new System.Windows.Forms.ComboBox();
            this.comPortComboBox = new System.Windows.Forms.ComboBox();
            this.label45 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.ftpHomeDirTextBox = new System.Windows.Forms.TextBox();
            this.label59 = new System.Windows.Forms.Label();
            this.enforceFTPCheckBox = new System.Windows.Forms.CheckBox();
            this.ftpBaseDirTextBox = new System.Windows.Forms.TextBox();
            this.ftpPortNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.ftpPswdTextBox = new System.Windows.Forms.TextBox();
            this.ftpUnmTextBox = new System.Windows.Forms.TextBox();
            this.ftpServerTextBox = new System.Windows.Forms.TextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.isDfltNoEmlSvrCheckBox = new System.Windows.Forms.CheckBox();
            this.isDfltYesEmlSvrCheckBox = new System.Windows.Forms.CheckBox();
            this.smtpPortNmUpDown = new System.Windows.Forms.NumericUpDown();
            this.emailPswdTextBox = new System.Windows.Forms.TextBox();
            this.emailUnameTextBox = new System.Windows.Forms.TextBox();
            this.smtpClientTextBox = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.emlSrvrIDTextBox = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.activeDrctryDmnTextBox = new System.Windows.Forms.TextBox();
            this.curOrgPictureBox = new System.Windows.Forms.PictureBox();
            this.label43 = new System.Windows.Forms.Label();
            this.crntOrgButton = new System.Windows.Forms.Button();
            this.crntOrgIDTextBox = new System.Windows.Forms.TextBox();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.auditPanel = new System.Windows.Forms.Panel();
            this.toolStrip5 = new System.Windows.Forms.ToolStrip();
            this.moveFirstAdtButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator80 = new System.Windows.Forms.ToolStripSeparator();
            this.movePreviousAdtButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator81 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel17 = new System.Windows.Forms.ToolStripLabel();
            this.positionAdtTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.totalRecAdtLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator82 = new System.Windows.Forms.ToolStripSeparator();
            this.moveNextAdtButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator83 = new System.Windows.Forms.ToolStripSeparator();
            this.moveLastAdtButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator84 = new System.Windows.Forms.ToolStripSeparator();
            this.dsplySizeAdtComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator86 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel19 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator88 = new System.Windows.Forms.ToolStripSeparator();
            this.searchForAdtTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator89 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel20 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator90 = new System.Windows.Forms.ToolStripSeparator();
            this.searchInAdtComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator91 = new System.Windows.Forms.ToolStripSeparator();
            this.refreshAdtButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
            this.vwSQLAdtButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator95 = new System.Windows.Forms.ToolStripSeparator();
            this.auditTblsTreeView = new System.Windows.Forms.TreeView();
            this.auditTblsDataGridView = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.auditContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exptAudtMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshAdtMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator94 = new System.Windows.Forms.ToolStripSeparator();
            this.vwSQLAdtMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel17 = new System.Windows.Forms.Panel();
            this.glsLabel16 = new glsLabel.glsLabel();
            this.label34 = new System.Windows.Forms.Label();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.loginsPanel = new System.Windows.Forms.Panel();
            this.loginsListView = new System.Windows.Forms.ListView();
            this.columnHeader38 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader39 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader40 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader41 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader42 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader45 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader43 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader44 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.loginsContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exptLgnMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshLgnMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator93 = new System.Windows.Forms.ToolStripSeparator();
            this.vwSQLLgnMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip4 = new System.Windows.Forms.ToolStrip();
            this.moveFirstLgnsButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator62 = new System.Windows.Forms.ToolStripSeparator();
            this.movePreviousLgnsButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator63 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel11 = new System.Windows.Forms.ToolStripLabel();
            this.positionLgnsTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.totalRecLgnsLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator64 = new System.Windows.Forms.ToolStripSeparator();
            this.moveNextLgnsButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator65 = new System.Windows.Forms.ToolStripSeparator();
            this.moveLastLgnsButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator66 = new System.Windows.Forms.ToolStripSeparator();
            this.dsplySizeLgnsComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator68 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel15 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator70 = new System.Windows.Forms.ToolStripSeparator();
            this.searchForLgnsTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator71 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel16 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator72 = new System.Windows.Forms.ToolStripSeparator();
            this.searchInLgnsComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator73 = new System.Windows.Forms.ToolStripSeparator();
            this.refreshLgnsButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.vwSQLLgnsButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator77 = new System.Windows.Forms.ToolStripSeparator();
            this.label35 = new System.Windows.Forms.Label();
            this.showFaildCheckBox = new System.Windows.Forms.CheckBox();
            this.showSuccsflCheckBox = new System.Windows.Forms.CheckBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.infoToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.deleteSrvrButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator52 = new System.Windows.Forms.ToolStripSeparator();
            this.deletePolicyButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator60 = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.treeVWContextMenuStrip.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.usersPanel.SuspendLayout();
            this.toolStrip8.SuspendLayout();
            this.toolStrip9.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.userRolesContextMenuStrip.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.navToolStrip.SuspendLayout();
            this.usersContextMenuStrip.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.rolesPanel.SuspendLayout();
            this.toolStrip10.SuspendLayout();
            this.toolStrip11.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.rolesPrvlgsContextMenuStrip.SuspendLayout();
            this.rolesContextMenuStrip.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.modulesPanel.SuspendLayout();
            this.modulePrvlgContextMenuStrip.SuspendLayout();
            this.panel11.SuspendLayout();
            this.modulesContextMenuStrip.SuspendLayout();
            this.panel10.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.extraInfoPanel.SuspendLayout();
            this.toolStrip12.SuspendLayout();
            this.extInfLabelContextMenuStrip.SuspendLayout();
            this.panel23.SuspendLayout();
            this.subGroupsContextMenuStrip.SuspendLayout();
            this.panel20.SuspendLayout();
            this.extInfMdlContextMenuStrip.SuspendLayout();
            this.panel21.SuspendLayout();
            this.toolStrip7.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.policyPanel.SuspendLayout();
            this.plcyMdlsContextMenuStrip.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sessionNumUpDown)).BeginInit();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.autoUnlkTmNmUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.faildLgnCntNmUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxLenPswdNmUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.oldPswdCntNmUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minLenPswdNumericUpDown)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mxNoRecsNmUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.expryDaysNmUpDown)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.emailServerPanel.SuspendLayout();
            this.groupBox13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smsDataGridView)).BeginInit();
            this.toolStrip6.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ftpPortNumUpDown)).BeginInit();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smtpPortNmUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.curOrgPictureBox)).BeginInit();
            this.tabPage7.SuspendLayout();
            this.auditPanel.SuspendLayout();
            this.toolStrip5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.auditTblsDataGridView)).BeginInit();
            this.auditContextMenuStrip.SuspendLayout();
            this.panel17.SuspendLayout();
            this.tabPage8.SuspendLayout();
            this.loginsPanel.SuspendLayout();
            this.loginsContextMenuStrip.SuspendLayout();
            this.toolStrip4.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.ContextMenuStrip = this.treeVWContextMenuStrip;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.leftTreeView);
            this.splitContainer1.Panel1.Controls.Add(this.accDndLabel);
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(5);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.waitLabel);
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(5);
            this.splitContainer1.Size = new System.Drawing.Size(1276, 733);
            this.splitContainer1.SplitterDistance = 225;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeVWContextMenuStrip
            // 
            this.treeVWContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hideTreevwMenuItem,
            this.toolStripSeparator123});
            this.treeVWContextMenuStrip.Name = "usersContextMenuStrip";
            this.treeVWContextMenuStrip.Size = new System.Drawing.Size(153, 32);
            // 
            // hideTreevwMenuItem
            // 
            this.hideTreevwMenuItem.Image = global::SystemAdministration.Properties.Resources.download__26_;
            this.hideTreevwMenuItem.Name = "hideTreevwMenuItem";
            this.hideTreevwMenuItem.Size = new System.Drawing.Size(152, 22);
            this.hideTreevwMenuItem.Text = "Hide Tree View";
            this.hideTreevwMenuItem.Click += new System.EventHandler(this.hideTreevwMenuItem_Click);
            // 
            // toolStripSeparator123
            // 
            this.toolStripSeparator123.Name = "toolStripSeparator123";
            this.toolStripSeparator123.Size = new System.Drawing.Size(149, 6);
            // 
            // leftTreeView
            // 
            this.leftTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.leftTreeView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.leftTreeView.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.leftTreeView.HideSelection = false;
            this.leftTreeView.HotTracking = true;
            this.leftTreeView.ImageKey = "tick_64.png";
            this.leftTreeView.ImageList = this.imageList1;
            this.leftTreeView.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.leftTreeView.Location = new System.Drawing.Point(8, 50);
            this.leftTreeView.Name = "leftTreeView";
            this.leftTreeView.SelectedImageKey = "tick_64.png";
            this.leftTreeView.ShowNodeToolTips = true;
            this.leftTreeView.Size = new System.Drawing.Size(205, 674);
            this.leftTreeView.TabIndex = 0;
            this.leftTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.leftTreeView_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "54.png");
            this.imageList1.Images.SetKeyName(1, "104.png");
            this.imageList1.Images.SetKeyName(2, "111.png");
            this.imageList1.Images.SetKeyName(3, "groupings.png");
            this.imageList1.Images.SetKeyName(4, "New.ico");
            this.imageList1.Images.SetKeyName(5, "SecurityLock.png");
            this.imageList1.Images.SetKeyName(6, "shield_64.png");
            this.imageList1.Images.SetKeyName(7, "staffs.png");
            this.imageList1.Images.SetKeyName(8, "tick_64.png");
            this.imageList1.Images.SetKeyName(9, "features_audittrail_icon.jpg");
            this.imageList1.Images.SetKeyName(10, "73.ico");
            this.imageList1.Images.SetKeyName(11, "antenna1.png");
            // 
            // accDndLabel
            // 
            this.accDndLabel.AutoSize = true;
            this.accDndLabel.Font = new System.Drawing.Font("Courier New", 20.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accDndLabel.ForeColor = System.Drawing.Color.White;
            this.accDndLabel.Location = new System.Drawing.Point(0, 0);
            this.accDndLabel.Name = "accDndLabel";
            this.accDndLabel.Size = new System.Drawing.Size(237, 30);
            this.accDndLabel.TabIndex = 93;
            this.accDndLabel.Text = "Access Denied!";
            this.accDndLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.accDndLabel.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.glsLabel1);
            this.panel2.Location = new System.Drawing.Point(8, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(204, 39);
            this.panel2.TabIndex = 2;
            // 
            // glsLabel1
            // 
            this.glsLabel1.BottomFill = System.Drawing.Color.Silver;
            this.glsLabel1.Caption = "MAIN MENU";
            this.glsLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glsLabel1.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.glsLabel1.ForeColor = System.Drawing.Color.White;
            this.glsLabel1.Location = new System.Drawing.Point(0, 0);
            this.glsLabel1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.glsLabel1.Name = "glsLabel1";
            this.glsLabel1.Size = new System.Drawing.Size(200, 35);
            this.glsLabel1.TabIndex = 1;
            this.glsLabel1.TopFill = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            // 
            // waitLabel
            // 
            this.waitLabel.BackColor = System.Drawing.Color.Green;
            this.waitLabel.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.waitLabel.ForeColor = System.Drawing.Color.White;
            this.waitLabel.Location = new System.Drawing.Point(250, 222);
            this.waitLabel.Name = "waitLabel";
            this.waitLabel.Size = new System.Drawing.Size(328, 52);
            this.waitLabel.TabIndex = 135;
            this.waitLabel.Text = "LOADING STANDARD ROLES & LOVs...PLEASE WAIT...";
            this.waitLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.waitLabel.Visible = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Controls.Add(this.tabPage8);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.imageList1;
            this.tabControl1.Location = new System.Drawing.Point(5, 5);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1033, 719);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabControl1.TabIndex = 136;
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.Controls.Add(this.usersPanel);
            this.tabPage1.ImageKey = "groupings.png";
            this.tabPage1.Location = new System.Drawing.Point(4, 60);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1025, 655);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "USERS && THEIR ROLES";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // usersPanel
            // 
            this.usersPanel.AutoScroll = true;
            this.usersPanel.BackColor = System.Drawing.Color.LightSlateGray;
            this.usersPanel.Controls.Add(this.mailLabel);
            this.usersPanel.Controls.Add(this.toolStrip8);
            this.usersPanel.Controls.Add(this.toolStrip9);
            this.usersPanel.Controls.Add(this.imprtUsersButton);
            this.usersPanel.Controls.Add(this.groupBox3);
            this.usersPanel.Controls.Add(this.exprtUsersButton);
            this.usersPanel.Controls.Add(this.userRoleslistView);
            this.usersPanel.Controls.Add(this.groupBox2);
            this.usersPanel.Controls.Add(this.groupBox1);
            this.usersPanel.Controls.Add(this.navToolStrip);
            this.usersPanel.Controls.Add(this.userListView);
            this.usersPanel.Controls.Add(this.label38);
            this.usersPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.usersPanel.Location = new System.Drawing.Point(3, 3);
            this.usersPanel.Name = "usersPanel";
            this.usersPanel.Size = new System.Drawing.Size(1019, 649);
            this.usersPanel.TabIndex = 0;
            this.usersPanel.TabStop = true;
            // 
            // mailLabel
            // 
            this.mailLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.mailLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.mailLabel.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mailLabel.ForeColor = System.Drawing.Color.White;
            this.mailLabel.Location = new System.Drawing.Point(305, 198);
            this.mailLabel.Name = "mailLabel";
            this.mailLabel.Size = new System.Drawing.Size(409, 49);
            this.mailLabel.TabIndex = 82;
            this.mailLabel.Text = "Sending Email.....Please Wait.....";
            this.mailLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.mailLabel.Visible = false;
            // 
            // toolStrip8
            // 
            this.toolStrip8.AutoSize = false;
            this.toolStrip8.BackColor = System.Drawing.Color.WhiteSmoke;
            this.toolStrip8.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip8.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addEdtUsrRoleButton});
            this.toolStrip8.Location = new System.Drawing.Point(368, 335);
            this.toolStrip8.Name = "toolStrip8";
            this.toolStrip8.Size = new System.Drawing.Size(205, 25);
            this.toolStrip8.TabIndex = 84;
            this.toolStrip8.TabStop = true;
            this.toolStrip8.Text = "toolStrip8";
            // 
            // addEdtUsrRoleButton
            // 
            this.addEdtUsrRoleButton.Image = global::SystemAdministration.Properties.Resources.plus_32;
            this.addEdtUsrRoleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addEdtUsrRoleButton.Name = "addEdtUsrRoleButton";
            this.addEdtUsrRoleButton.Size = new System.Drawing.Size(147, 22);
            this.addEdtUsrRoleButton.Text = "ADD/EDIT USER ROLES";
            this.addEdtUsrRoleButton.Click += new System.EventHandler(this.addEdtUsrRoleButton_Click);
            // 
            // toolStrip9
            // 
            this.toolStrip9.AutoSize = false;
            this.toolStrip9.BackColor = System.Drawing.Color.WhiteSmoke;
            this.toolStrip9.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip9.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addUserButton,
            this.editUserButton,
            this.delUserButton});
            this.toolStrip9.Location = new System.Drawing.Point(4, 29);
            this.toolStrip9.Name = "toolStrip9";
            this.toolStrip9.Size = new System.Drawing.Size(237, 25);
            this.toolStrip9.TabIndex = 0;
            this.toolStrip9.TabStop = true;
            this.toolStrip9.Text = "toolStrip9";
            // 
            // addUserButton
            // 
            this.addUserButton.Image = global::SystemAdministration.Properties.Resources.plus_32;
            this.addUserButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addUserButton.Name = "addUserButton";
            this.addUserButton.Size = new System.Drawing.Size(51, 22);
            this.addUserButton.Text = "ADD";
            this.addUserButton.Click += new System.EventHandler(this.addUserButton_Click);
            // 
            // editUserButton
            // 
            this.editUserButton.Image = global::SystemAdministration.Properties.Resources.edit32;
            this.editUserButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editUserButton.Name = "editUserButton";
            this.editUserButton.Size = new System.Drawing.Size(51, 22);
            this.editUserButton.Text = "EDIT";
            this.editUserButton.Click += new System.EventHandler(this.editUserButton_Click);
            // 
            // delUserButton
            // 
            this.delUserButton.Image = global::SystemAdministration.Properties.Resources.delete;
            this.delUserButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.delUserButton.Name = "delUserButton";
            this.delUserButton.Size = new System.Drawing.Size(66, 22);
            this.delUserButton.Text = "DELETE";
            this.delUserButton.Click += new System.EventHandler(this.delUserButton_Click);
            // 
            // imprtUsersButton
            // 
            this.imprtUsersButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imprtUsersButton.ForeColor = System.Drawing.Color.Black;
            this.imprtUsersButton.Image = ((System.Drawing.Image)(resources.GetObject("imprtUsersButton.Image")));
            this.imprtUsersButton.Location = new System.Drawing.Point(795, 62);
            this.imprtUsersButton.Name = "imprtUsersButton";
            this.imprtUsersButton.Size = new System.Drawing.Size(108, 25);
            this.imprtUsersButton.TabIndex = 6;
            this.imprtUsersButton.Text = "IMPORT USERS";
            this.imprtUsersButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.imprtUsersButton.UseVisualStyleBackColor = true;
            this.imprtUsersButton.Click += new System.EventHandler(this.imprtUsersButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.changePswdAutoButton);
            this.groupBox3.Controls.Add(this.agePswdTextBox);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.changePswdManButton);
            this.groupBox3.Controls.Add(this.isExpiredCheckBox);
            this.groupBox3.Controls.Add(this.lastPwdChngeTextBox);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.isTempCheckBox);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(118)))));
            this.groupBox3.Location = new System.Drawing.Point(368, 132);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(422, 114);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Password Info";
            // 
            // changePswdAutoButton
            // 
            this.changePswdAutoButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.changePswdAutoButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changePswdAutoButton.ForeColor = System.Drawing.Color.Black;
            this.changePswdAutoButton.ImageKey = "SecurityLock.png";
            this.changePswdAutoButton.ImageList = this.imageList1;
            this.changePswdAutoButton.Location = new System.Drawing.Point(306, 64);
            this.changePswdAutoButton.Name = "changePswdAutoButton";
            this.changePswdAutoButton.Size = new System.Drawing.Size(105, 47);
            this.changePswdAutoButton.TabIndex = 5;
            this.changePswdAutoButton.Text = "Change Password Automatically";
            this.changePswdAutoButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.changePswdAutoButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.changePswdAutoButton.UseVisualStyleBackColor = true;
            this.changePswdAutoButton.Click += new System.EventHandler(this.changePswdAutoButton_Click);
            // 
            // agePswdTextBox
            // 
            this.agePswdTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.agePswdTextBox.ForeColor = System.Drawing.Color.Black;
            this.agePswdTextBox.Location = new System.Drawing.Point(160, 61);
            this.agePswdTextBox.Name = "agePswdTextBox";
            this.agePswdTextBox.ReadOnly = true;
            this.agePswdTextBox.Size = new System.Drawing.Size(145, 21);
            this.agePswdTextBox.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(15, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(132, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Age of Current Password:";
            // 
            // changePswdManButton
            // 
            this.changePswdManButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changePswdManButton.ForeColor = System.Drawing.Color.Black;
            this.changePswdManButton.ImageKey = "SecurityLock.png";
            this.changePswdManButton.ImageList = this.imageList1;
            this.changePswdManButton.Location = new System.Drawing.Point(306, 17);
            this.changePswdManButton.Name = "changePswdManButton";
            this.changePswdManButton.Size = new System.Drawing.Size(105, 47);
            this.changePswdManButton.TabIndex = 4;
            this.changePswdManButton.Text = "Change Password Manually";
            this.changePswdManButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.changePswdManButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.changePswdManButton.UseVisualStyleBackColor = true;
            this.changePswdManButton.Click += new System.EventHandler(this.changePswdManButton_Click);
            // 
            // isExpiredCheckBox
            // 
            this.isExpiredCheckBox.AutoSize = true;
            this.isExpiredCheckBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.isExpiredCheckBox.ForeColor = System.Drawing.Color.White;
            this.isExpiredCheckBox.Location = new System.Drawing.Point(15, 42);
            this.isExpiredCheckBox.Name = "isExpiredCheckBox";
            this.isExpiredCheckBox.Size = new System.Drawing.Size(111, 17);
            this.isExpiredCheckBox.TabIndex = 1;
            this.isExpiredCheckBox.Text = "Password Expired";
            this.isExpiredCheckBox.UseVisualStyleBackColor = true;
            this.isExpiredCheckBox.CheckedChanged += new System.EventHandler(this.isExpiredCheckBox_CheckedChanged);
            // 
            // lastPwdChngeTextBox
            // 
            this.lastPwdChngeTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lastPwdChngeTextBox.ForeColor = System.Drawing.Color.Black;
            this.lastPwdChngeTextBox.Location = new System.Drawing.Point(160, 85);
            this.lastPwdChngeTextBox.Name = "lastPwdChngeTextBox";
            this.lastPwdChngeTextBox.ReadOnly = true;
            this.lastPwdChngeTextBox.Size = new System.Drawing.Size(145, 21);
            this.lastPwdChngeTextBox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(15, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Last Password Change Time:";
            // 
            // isTempCheckBox
            // 
            this.isTempCheckBox.AutoSize = true;
            this.isTempCheckBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.isTempCheckBox.ForeColor = System.Drawing.Color.White;
            this.isTempCheckBox.Location = new System.Drawing.Point(15, 20);
            this.isTempCheckBox.Name = "isTempCheckBox";
            this.isTempCheckBox.Size = new System.Drawing.Size(137, 17);
            this.isTempCheckBox.TabIndex = 0;
            this.isTempCheckBox.Text = "Password is Temporary";
            this.isTempCheckBox.UseVisualStyleBackColor = true;
            this.isTempCheckBox.CheckedChanged += new System.EventHandler(this.isTempCheckBox_CheckedChanged);
            // 
            // exprtUsersButton
            // 
            this.exprtUsersButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exprtUsersButton.ForeColor = System.Drawing.Color.Black;
            this.exprtUsersButton.Image = ((System.Drawing.Image)(resources.GetObject("exprtUsersButton.Image")));
            this.exprtUsersButton.Location = new System.Drawing.Point(795, 38);
            this.exprtUsersButton.Name = "exprtUsersButton";
            this.exprtUsersButton.Size = new System.Drawing.Size(158, 24);
            this.exprtUsersButton.TabIndex = 5;
            this.exprtUsersButton.Text = "EXPORT EXCEL TEMPLATE";
            this.exprtUsersButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.exprtUsersButton.UseVisualStyleBackColor = true;
            this.exprtUsersButton.Click += new System.EventHandler(this.exprtUsersButton_Click);
            // 
            // userRoleslistView
            // 
            this.userRoleslistView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.userRoleslistView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9});
            this.userRoleslistView.ContextMenuStrip = this.userRolesContextMenuStrip;
            this.userRoleslistView.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userRoleslistView.FullRowSelect = true;
            this.userRoleslistView.HideSelection = false;
            this.userRoleslistView.Location = new System.Drawing.Point(368, 363);
            this.userRoleslistView.MinimumSize = new System.Drawing.Size(422, 50);
            this.userRoleslistView.Name = "userRoleslistView";
            this.userRoleslistView.Size = new System.Drawing.Size(647, 283);
            this.userRoleslistView.TabIndex = 4;
            this.userRoleslistView.UseCompatibleStateImageBehavior = false;
            this.userRoleslistView.View = System.Windows.Forms.View.Details;
            this.userRoleslistView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.userRoleslistView_KeyDown);
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "No.";
            this.columnHeader5.Width = 35;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Role Name";
            this.columnHeader6.Width = 200;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "VALID START DATE";
            this.columnHeader7.Width = 120;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "VALID END DATE";
            this.columnHeader8.Width = 130;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "ROLE_ID";
            this.columnHeader9.Width = 0;
            // 
            // userRolesContextMenuStrip
            // 
            this.userRolesContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addUserRoleToolStripMenuItem,
            this.toolStripSeparator2,
            this.exptUsrRolesMenuItem,
            this.refreshUsrRoleToolStripMenuItem,
            this.recordHistoryUsrRoleToolStripMenuItem,
            this.viewSQLUsrRoleToolStripMenuItem});
            this.userRolesContextMenuStrip.Name = "userRolesContextMenuStrip";
            this.userRolesContextMenuStrip.Size = new System.Drawing.Size(179, 120);
            // 
            // addUserRoleToolStripMenuItem
            // 
            this.addUserRoleToolStripMenuItem.Image = global::SystemAdministration.Properties.Resources.plus_32;
            this.addUserRoleToolStripMenuItem.Name = "addUserRoleToolStripMenuItem";
            this.addUserRoleToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.addUserRoleToolStripMenuItem.Text = "&Add/Edit User Roles";
            this.addUserRoleToolStripMenuItem.Click += new System.EventHandler(this.addUserRoleToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(175, 6);
            // 
            // exptUsrRolesMenuItem
            // 
            this.exptUsrRolesMenuItem.Image = global::SystemAdministration.Properties.Resources.image007;
            this.exptUsrRolesMenuItem.Name = "exptUsrRolesMenuItem";
            this.exptUsrRolesMenuItem.Size = new System.Drawing.Size(178, 22);
            this.exptUsrRolesMenuItem.Text = "Export to Excel";
            this.exptUsrRolesMenuItem.Click += new System.EventHandler(this.exptUsrRolesMenuItem_Click);
            // 
            // refreshUsrRoleToolStripMenuItem
            // 
            this.refreshUsrRoleToolStripMenuItem.Image = global::SystemAdministration.Properties.Resources.refresh;
            this.refreshUsrRoleToolStripMenuItem.Name = "refreshUsrRoleToolStripMenuItem";
            this.refreshUsrRoleToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.refreshUsrRoleToolStripMenuItem.Text = "&Refresh";
            this.refreshUsrRoleToolStripMenuItem.Click += new System.EventHandler(this.refreshUsrRoleToolStripMenuItem_Click);
            // 
            // recordHistoryUsrRoleToolStripMenuItem
            // 
            this.recordHistoryUsrRoleToolStripMenuItem.Image = global::SystemAdministration.Properties.Resources.statistics_32;
            this.recordHistoryUsrRoleToolStripMenuItem.Name = "recordHistoryUsrRoleToolStripMenuItem";
            this.recordHistoryUsrRoleToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.recordHistoryUsrRoleToolStripMenuItem.Text = "Record &History";
            this.recordHistoryUsrRoleToolStripMenuItem.Click += new System.EventHandler(this.recordHistoryUsrRoleToolStripMenuItem_Click);
            // 
            // viewSQLUsrRoleToolStripMenuItem
            // 
            this.viewSQLUsrRoleToolStripMenuItem.Image = global::SystemAdministration.Properties.Resources.SQL;
            this.viewSQLUsrRoleToolStripMenuItem.Name = "viewSQLUsrRoleToolStripMenuItem";
            this.viewSQLUsrRoleToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.viewSQLUsrRoleToolStripMenuItem.Text = "&View SQL";
            this.viewSQLUsrRoleToolStripMenuItem.Click += new System.EventHandler(this.viewSQLUsrRoleToolStripMenuItem_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.usrDte2Button);
            this.groupBox2.Controls.Add(this.usrDte1Button);
            this.groupBox2.Controls.Add(this.usrVldEndDteTextBox);
            this.groupBox2.Controls.Add(this.usrVldStrtDteTextBox);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(118)))));
            this.groupBox2.Location = new System.Drawing.Point(367, 249);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(423, 81);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Validity of Account";
            // 
            // usrDte2Button
            // 
            this.usrDte2Button.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usrDte2Button.ForeColor = System.Drawing.Color.Black;
            this.usrDte2Button.Location = new System.Drawing.Point(369, 53);
            this.usrDte2Button.Name = "usrDte2Button";
            this.usrDte2Button.Size = new System.Drawing.Size(28, 22);
            this.usrDte2Button.TabIndex = 3;
            this.usrDte2Button.Text = "...";
            this.usrDte2Button.UseVisualStyleBackColor = true;
            this.usrDte2Button.Click += new System.EventHandler(this.usrDte2Button_Click);
            // 
            // usrDte1Button
            // 
            this.usrDte1Button.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usrDte1Button.ForeColor = System.Drawing.Color.Black;
            this.usrDte1Button.Location = new System.Drawing.Point(369, 31);
            this.usrDte1Button.Name = "usrDte1Button";
            this.usrDte1Button.Size = new System.Drawing.Size(28, 22);
            this.usrDte1Button.TabIndex = 1;
            this.usrDte1Button.Text = "...";
            this.usrDte1Button.UseVisualStyleBackColor = true;
            this.usrDte1Button.Click += new System.EventHandler(this.usrDte1Button_Click);
            // 
            // usrVldEndDteTextBox
            // 
            this.usrVldEndDteTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.usrVldEndDteTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usrVldEndDteTextBox.ForeColor = System.Drawing.Color.Black;
            this.usrVldEndDteTextBox.Location = new System.Drawing.Point(113, 54);
            this.usrVldEndDteTextBox.Name = "usrVldEndDteTextBox";
            this.usrVldEndDteTextBox.Size = new System.Drawing.Size(253, 21);
            this.usrVldEndDteTextBox.TabIndex = 2;
            this.usrVldEndDteTextBox.TextChanged += new System.EventHandler(this.usrVldStrtDteTextBox_TextChanged);
            this.usrVldEndDteTextBox.Leave += new System.EventHandler(this.usrVldStrtDteTextBox_Leave);
            // 
            // usrVldStrtDteTextBox
            // 
            this.usrVldStrtDteTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.usrVldStrtDteTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usrVldStrtDteTextBox.ForeColor = System.Drawing.Color.Black;
            this.usrVldStrtDteTextBox.Location = new System.Drawing.Point(113, 32);
            this.usrVldStrtDteTextBox.Name = "usrVldStrtDteTextBox";
            this.usrVldStrtDteTextBox.Size = new System.Drawing.Size(253, 21);
            this.usrVldStrtDteTextBox.TabIndex = 0;
            this.usrVldStrtDteTextBox.TextChanged += new System.EventHandler(this.usrVldStrtDteTextBox_TextChanged);
            this.usrVldStrtDteTextBox.Leave += new System.EventHandler(this.usrVldStrtDteTextBox_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(23, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Valid End Date:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(23, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Valid Start Date:";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.lastLoginAtmptTextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.failedLgnAtmptTextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.isLockedCheckBox);
            this.groupBox1.Controls.Add(this.isSuspendedCheckBox);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(118)))));
            this.groupBox1.Location = new System.Drawing.Point(367, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(423, 95);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Account Info";
            // 
            // lastLoginAtmptTextBox
            // 
            this.lastLoginAtmptTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lastLoginAtmptTextBox.ForeColor = System.Drawing.Color.Black;
            this.lastLoginAtmptTextBox.Location = new System.Drawing.Point(284, 64);
            this.lastLoginAtmptTextBox.Name = "lastLoginAtmptTextBox";
            this.lastLoginAtmptTextBox.ReadOnly = true;
            this.lastLoginAtmptTextBox.Size = new System.Drawing.Size(128, 21);
            this.lastLoginAtmptTextBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(134, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Last Login Attempt Time:";
            // 
            // failedLgnAtmptTextBox
            // 
            this.failedLgnAtmptTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.failedLgnAtmptTextBox.ForeColor = System.Drawing.Color.Black;
            this.failedLgnAtmptTextBox.Location = new System.Drawing.Point(284, 42);
            this.failedLgnAtmptTextBox.Name = "failedLgnAtmptTextBox";
            this.failedLgnAtmptTextBox.ReadOnly = true;
            this.failedLgnAtmptTextBox.Size = new System.Drawing.Size(128, 21);
            this.failedLgnAtmptTextBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(134, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Current Failed Login Attempts:";
            // 
            // isLockedCheckBox
            // 
            this.isLockedCheckBox.AutoSize = true;
            this.isLockedCheckBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.isLockedCheckBox.ForeColor = System.Drawing.Color.White;
            this.isLockedCheckBox.Location = new System.Drawing.Point(16, 43);
            this.isLockedCheckBox.Name = "isLockedCheckBox";
            this.isLockedCheckBox.Size = new System.Drawing.Size(59, 17);
            this.isLockedCheckBox.TabIndex = 1;
            this.isLockedCheckBox.Text = "Locked";
            this.isLockedCheckBox.UseVisualStyleBackColor = true;
            this.isLockedCheckBox.CheckedChanged += new System.EventHandler(this.isLockedCheckBox_CheckedChanged);
            // 
            // isSuspendedCheckBox
            // 
            this.isSuspendedCheckBox.AutoSize = true;
            this.isSuspendedCheckBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.isSuspendedCheckBox.ForeColor = System.Drawing.Color.White;
            this.isSuspendedCheckBox.Location = new System.Drawing.Point(16, 21);
            this.isSuspendedCheckBox.Name = "isSuspendedCheckBox";
            this.isSuspendedCheckBox.Size = new System.Drawing.Size(79, 17);
            this.isSuspendedCheckBox.TabIndex = 0;
            this.isSuspendedCheckBox.Text = "Suspended";
            this.isSuspendedCheckBox.UseVisualStyleBackColor = true;
            this.isSuspendedCheckBox.CheckedChanged += new System.EventHandler(this.isSuspendedCheckBox_CheckedChanged);
            // 
            // navToolStrip
            // 
            this.navToolStrip.AutoSize = false;
            this.navToolStrip.BackColor = System.Drawing.Color.WhiteSmoke;
            this.navToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moveFirstUserButton,
            this.toolStripSeparator9,
            this.movePreviousUserButton,
            this.toolStripSeparator10,
            this.ToolStripLabel2,
            this.positionUserTextBox,
            this.totalRecUserLabel,
            this.toolStripSeparator11,
            this.moveNextUserButton,
            this.toolStripSeparator12,
            this.moveLastUserButton,
            this.toolStripSeparator13,
            this.dsplySizeUserComboBox,
            this.toolStripSeparator16,
            this.toolStripLabel1,
            this.toolStripSeparator18,
            this.searchForUserTextBox,
            this.toolStripSeparator19,
            this.toolStripLabel3,
            this.toolStripSeparator20,
            this.searchInUserComboBox,
            this.toolStripSeparator21,
            this.refreshUserButton});
            this.navToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.navToolStrip.Location = new System.Drawing.Point(0, 0);
            this.navToolStrip.Name = "navToolStrip";
            this.navToolStrip.Size = new System.Drawing.Size(1019, 25);
            this.navToolStrip.Stretch = true;
            this.navToolStrip.TabIndex = 0;
            this.navToolStrip.TabStop = true;
            this.navToolStrip.Text = "ToolStrip2";
            // 
            // moveFirstUserButton
            // 
            this.moveFirstUserButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveFirstUserButton.Image = global::SystemAdministration.Properties.Resources.DataContainer_MoveFirstHS;
            this.moveFirstUserButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveFirstUserButton.Name = "moveFirstUserButton";
            this.moveFirstUserButton.Size = new System.Drawing.Size(23, 22);
            this.moveFirstUserButton.Text = "Move First";
            this.moveFirstUserButton.Click += new System.EventHandler(this.userPnlNavButtons);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
            // 
            // movePreviousUserButton
            // 
            this.movePreviousUserButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.movePreviousUserButton.Image = global::SystemAdministration.Properties.Resources.DataContainer_MovePreviousHS;
            this.movePreviousUserButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.movePreviousUserButton.Name = "movePreviousUserButton";
            this.movePreviousUserButton.Size = new System.Drawing.Size(23, 22);
            this.movePreviousUserButton.Text = "Move Previous";
            this.movePreviousUserButton.Click += new System.EventHandler(this.userPnlNavButtons);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 25);
            // 
            // ToolStripLabel2
            // 
            this.ToolStripLabel2.AutoToolTip = true;
            this.ToolStripLabel2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToolStripLabel2.Name = "ToolStripLabel2";
            this.ToolStripLabel2.Size = new System.Drawing.Size(47, 22);
            this.ToolStripLabel2.Text = "Record";
            // 
            // positionUserTextBox
            // 
            this.positionUserTextBox.AutoToolTip = true;
            this.positionUserTextBox.BackColor = System.Drawing.Color.White;
            this.positionUserTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.positionUserTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.positionUserTextBox.Name = "positionUserTextBox";
            this.positionUserTextBox.ReadOnly = true;
            this.positionUserTextBox.Size = new System.Drawing.Size(70, 25);
            this.positionUserTextBox.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.positionUserTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.positionUserTextBox_KeyDown);
            // 
            // totalRecUserLabel
            // 
            this.totalRecUserLabel.AutoToolTip = true;
            this.totalRecUserLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalRecUserLabel.Name = "totalRecUserLabel";
            this.totalRecUserLabel.Size = new System.Drawing.Size(50, 22);
            this.totalRecUserLabel.Text = "of Total";
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(6, 25);
            // 
            // moveNextUserButton
            // 
            this.moveNextUserButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveNextUserButton.Image = global::SystemAdministration.Properties.Resources.DataContainer_MoveNextHS;
            this.moveNextUserButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveNextUserButton.Name = "moveNextUserButton";
            this.moveNextUserButton.Size = new System.Drawing.Size(23, 22);
            this.moveNextUserButton.Text = "Move Next";
            this.moveNextUserButton.Click += new System.EventHandler(this.userPnlNavButtons);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(6, 25);
            // 
            // moveLastUserButton
            // 
            this.moveLastUserButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveLastUserButton.Image = global::SystemAdministration.Properties.Resources.DataContainer_MoveLastHS;
            this.moveLastUserButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveLastUserButton.Name = "moveLastUserButton";
            this.moveLastUserButton.Size = new System.Drawing.Size(23, 22);
            this.moveLastUserButton.Text = "Move Last";
            this.moveLastUserButton.Click += new System.EventHandler(this.userPnlNavButtons);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(6, 25);
            // 
            // dsplySizeUserComboBox
            // 
            this.dsplySizeUserComboBox.AutoSize = false;
            this.dsplySizeUserComboBox.Items.AddRange(new object[] {
            "1",
            "5",
            "10",
            "15",
            "20",
            "30",
            "40",
            "50",
            "100"});
            this.dsplySizeUserComboBox.Name = "dsplySizeUserComboBox";
            this.dsplySizeUserComboBox.Size = new System.Drawing.Size(40, 23);
            this.dsplySizeUserComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForUserTextBox_KeyDown);
            // 
            // toolStripSeparator16
            // 
            this.toolStripSeparator16.Name = "toolStripSeparator16";
            this.toolStripSeparator16.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(65, 22);
            this.toolStripLabel1.Text = "Search For:";
            // 
            // toolStripSeparator18
            // 
            this.toolStripSeparator18.Name = "toolStripSeparator18";
            this.toolStripSeparator18.Size = new System.Drawing.Size(6, 25);
            // 
            // searchForUserTextBox
            // 
            this.searchForUserTextBox.Name = "searchForUserTextBox";
            this.searchForUserTextBox.Size = new System.Drawing.Size(100, 25);
            this.searchForUserTextBox.Enter += new System.EventHandler(this.searchForUserTextBox_Click);
            this.searchForUserTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForUserTextBox_KeyDown);
            this.searchForUserTextBox.Click += new System.EventHandler(this.searchForUserTextBox_Click);
            // 
            // toolStripSeparator19
            // 
            this.toolStripSeparator19.Name = "toolStripSeparator19";
            this.toolStripSeparator19.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(58, 22);
            this.toolStripLabel3.Text = "Search In:";
            // 
            // toolStripSeparator20
            // 
            this.toolStripSeparator20.Name = "toolStripSeparator20";
            this.toolStripSeparator20.Size = new System.Drawing.Size(6, 25);
            // 
            // searchInUserComboBox
            // 
            this.searchInUserComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.searchInUserComboBox.Items.AddRange(new object[] {
            "Owned By",
            "Role Name",
            "User Name"});
            this.searchInUserComboBox.Name = "searchInUserComboBox";
            this.searchInUserComboBox.Size = new System.Drawing.Size(121, 25);
            this.searchInUserComboBox.Sorted = true;
            this.searchInUserComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForUserTextBox_KeyDown);
            // 
            // toolStripSeparator21
            // 
            this.toolStripSeparator21.Name = "toolStripSeparator21";
            this.toolStripSeparator21.Size = new System.Drawing.Size(6, 25);
            // 
            // refreshUserButton
            // 
            this.refreshUserButton.Image = global::SystemAdministration.Properties.Resources.action_go;
            this.refreshUserButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshUserButton.Name = "refreshUserButton";
            this.refreshUserButton.Size = new System.Drawing.Size(42, 22);
            this.refreshUserButton.Text = "Go";
            this.refreshUserButton.Click += new System.EventHandler(this.refreshUserButton_Click);
            // 
            // userListView
            // 
            this.userListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.userListView.BackColor = System.Drawing.Color.White;
            this.userListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader10,
            this.columnHeader60});
            this.userListView.ContextMenuStrip = this.usersContextMenuStrip;
            this.userListView.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userListView.FullRowSelect = true;
            this.userListView.GridLines = true;
            this.userListView.HideSelection = false;
            this.userListView.Location = new System.Drawing.Point(4, 58);
            this.userListView.MinimumSize = new System.Drawing.Size(357, 300);
            this.userListView.Name = "userListView";
            this.userListView.Size = new System.Drawing.Size(357, 588);
            this.userListView.TabIndex = 0;
            this.userListView.UseCompatibleStateImageBehavior = false;
            this.userListView.View = System.Windows.Forms.View.Details;
            this.userListView.SelectedIndexChanged += new System.EventHandler(this.userListView_SelectedIndexChanged);
            this.userListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.userListView_KeyDown);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "No.";
            this.columnHeader1.Width = 45;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "User Name";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Owned By";
            this.columnHeader3.Width = 200;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "User ID";
            this.columnHeader4.Width = 0;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "PersonID";
            this.columnHeader10.Width = 0;
            // 
            // columnHeader60
            // 
            this.columnHeader60.Text = "CustomerID";
            this.columnHeader60.Width = 0;
            // 
            // usersContextMenuStrip
            // 
            this.usersContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addUserToolStripMenuItem,
            this.editUserToolStripMenuItem,
            this.toolStripSeparator1,
            this.exptUsrsMenuItem,
            this.refreshUsersToolStripMenuItem,
            this.recordHistoryUsrsToolStripMenuItem,
            this.viewSQLUserToolStripMenuItem});
            this.usersContextMenuStrip.Name = "usersContextMenuStrip";
            this.usersContextMenuStrip.Size = new System.Drawing.Size(153, 142);
            // 
            // addUserToolStripMenuItem
            // 
            this.addUserToolStripMenuItem.Image = global::SystemAdministration.Properties.Resources.plus_32;
            this.addUserToolStripMenuItem.Name = "addUserToolStripMenuItem";
            this.addUserToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.addUserToolStripMenuItem.Text = "&Add User";
            this.addUserToolStripMenuItem.Click += new System.EventHandler(this.addUserToolStripMenuItem_Click);
            // 
            // editUserToolStripMenuItem
            // 
            this.editUserToolStripMenuItem.Image = global::SystemAdministration.Properties.Resources.edit32;
            this.editUserToolStripMenuItem.Name = "editUserToolStripMenuItem";
            this.editUserToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.editUserToolStripMenuItem.Text = "&Edit User";
            this.editUserToolStripMenuItem.Click += new System.EventHandler(this.editUserToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // exptUsrsMenuItem
            // 
            this.exptUsrsMenuItem.Image = global::SystemAdministration.Properties.Resources.image007;
            this.exptUsrsMenuItem.Name = "exptUsrsMenuItem";
            this.exptUsrsMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exptUsrsMenuItem.Text = "Export to Excel";
            this.exptUsrsMenuItem.Click += new System.EventHandler(this.exptUsrsMenuItem_Click);
            // 
            // refreshUsersToolStripMenuItem
            // 
            this.refreshUsersToolStripMenuItem.Image = global::SystemAdministration.Properties.Resources.refresh;
            this.refreshUsersToolStripMenuItem.Name = "refreshUsersToolStripMenuItem";
            this.refreshUsersToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.refreshUsersToolStripMenuItem.Text = "&Refresh";
            this.refreshUsersToolStripMenuItem.Click += new System.EventHandler(this.refreshUsersToolStripMenuItem_Click);
            // 
            // recordHistoryUsrsToolStripMenuItem
            // 
            this.recordHistoryUsrsToolStripMenuItem.Image = global::SystemAdministration.Properties.Resources.statistics_32;
            this.recordHistoryUsrsToolStripMenuItem.Name = "recordHistoryUsrsToolStripMenuItem";
            this.recordHistoryUsrsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.recordHistoryUsrsToolStripMenuItem.Text = "Record &History";
            this.recordHistoryUsrsToolStripMenuItem.Click += new System.EventHandler(this.recordHistoryUsrsToolStripMenuItem_Click);
            // 
            // viewSQLUserToolStripMenuItem
            // 
            this.viewSQLUserToolStripMenuItem.Image = global::SystemAdministration.Properties.Resources.SQL;
            this.viewSQLUserToolStripMenuItem.Name = "viewSQLUserToolStripMenuItem";
            this.viewSQLUserToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.viewSQLUserToolStripMenuItem.Text = "&View SQL";
            this.viewSQLUserToolStripMenuItem.Click += new System.EventHandler(this.viewSQLUserToolStripMenuItem_Click);
            // 
            // label38
            // 
            this.label38.Location = new System.Drawing.Point(613, 60);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(204, 373);
            this.label38.TabIndex = 117;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.rolesPanel);
            this.tabPage2.ImageKey = "staffs.png";
            this.tabPage2.Location = new System.Drawing.Point(4, 60);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1025, 655);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "ROLES && PRIVILEDGES";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // rolesPanel
            // 
            this.rolesPanel.AutoScroll = true;
            this.rolesPanel.BackColor = System.Drawing.Color.LightSlateGray;
            this.rolesPanel.Controls.Add(this.toolStrip10);
            this.rolesPanel.Controls.Add(this.toolStrip11);
            this.rolesPanel.Controls.Add(this.panel6);
            this.rolesPanel.Controls.Add(this.panel5);
            this.rolesPanel.Controls.Add(this.rolePrvldgsListView);
            this.rolesPanel.Controls.Add(this.rolesListView);
            this.rolesPanel.Controls.Add(this.toolStrip1);
            this.rolesPanel.Controls.Add(this.label8);
            this.rolesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rolesPanel.Enabled = false;
            this.rolesPanel.Location = new System.Drawing.Point(3, 3);
            this.rolesPanel.Name = "rolesPanel";
            this.rolesPanel.Size = new System.Drawing.Size(1019, 649);
            this.rolesPanel.TabIndex = 1;
            this.rolesPanel.Visible = false;
            // 
            // toolStrip10
            // 
            this.toolStrip10.AutoSize = false;
            this.toolStrip10.BackColor = System.Drawing.Color.WhiteSmoke;
            this.toolStrip10.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip10.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addEditRoleButton});
            this.toolStrip10.Location = new System.Drawing.Point(467, 71);
            this.toolStrip10.Name = "toolStrip10";
            this.toolStrip10.Size = new System.Drawing.Size(342, 25);
            this.toolStrip10.TabIndex = 86;
            this.toolStrip10.TabStop = true;
            this.toolStrip10.Text = "toolStrip10";
            // 
            // addEditRoleButton
            // 
            this.addEditRoleButton.Image = global::SystemAdministration.Properties.Resources.plus_32;
            this.addEditRoleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addEditRoleButton.Name = "addEditRoleButton";
            this.addEditRoleButton.Size = new System.Drawing.Size(181, 22);
            this.addEditRoleButton.Text = "ADD/EDIT ROLE PRIVILEDGES";
            this.addEditRoleButton.Click += new System.EventHandler(this.addEditRoleButton_Click);
            // 
            // toolStrip11
            // 
            this.toolStrip11.AutoSize = false;
            this.toolStrip11.BackColor = System.Drawing.Color.WhiteSmoke;
            this.toolStrip11.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip11.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addRoleButton,
            this.editRoleButton,
            this.loadRolesButton});
            this.toolStrip11.Location = new System.Drawing.Point(7, 72);
            this.toolStrip11.Name = "toolStrip11";
            this.toolStrip11.Size = new System.Drawing.Size(455, 25);
            this.toolStrip11.TabIndex = 85;
            this.toolStrip11.TabStop = true;
            this.toolStrip11.Text = "toolStrip11";
            // 
            // addRoleButton
            // 
            this.addRoleButton.Image = global::SystemAdministration.Properties.Resources.plus_32;
            this.addRoleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addRoleButton.Name = "addRoleButton";
            this.addRoleButton.Size = new System.Drawing.Size(51, 22);
            this.addRoleButton.Text = "ADD";
            this.addRoleButton.Click += new System.EventHandler(this.addRoleButton_Click);
            // 
            // editRoleButton
            // 
            this.editRoleButton.Image = global::SystemAdministration.Properties.Resources.edit32;
            this.editRoleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editRoleButton.Name = "editRoleButton";
            this.editRoleButton.Size = new System.Drawing.Size(51, 22);
            this.editRoleButton.Text = "EDIT";
            this.editRoleButton.Click += new System.EventHandler(this.editRoleButton_Click);
            // 
            // loadRolesButton
            // 
            this.loadRolesButton.Image = global::SystemAdministration.Properties.Resources.Window_Refresh_icon;
            this.loadRolesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loadRolesButton.Name = "loadRolesButton";
            this.loadRolesButton.Size = new System.Drawing.Size(201, 22);
            this.loadRolesButton.Text = "LOAD STANDARD ROLES && LOVs";
            this.loadRolesButton.Click += new System.EventHandler(this.loadRolesButton_Click);
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel6.Controls.Add(this.glsLabel5);
            this.panel6.Location = new System.Drawing.Point(467, 28);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(549, 39);
            this.panel6.TabIndex = 80;
            // 
            // glsLabel5
            // 
            this.glsLabel5.BottomFill = System.Drawing.Color.Silver;
            this.glsLabel5.Caption = "Corresponding Priviledges";
            this.glsLabel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glsLabel5.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.glsLabel5.ForeColor = System.Drawing.Color.White;
            this.glsLabel5.Location = new System.Drawing.Point(0, 0);
            this.glsLabel5.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.glsLabel5.Name = "glsLabel5";
            this.glsLabel5.Size = new System.Drawing.Size(545, 35);
            this.glsLabel5.TabIndex = 1;
            this.glsLabel5.TopFill = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel5.Controls.Add(this.glsLabel4);
            this.panel5.Location = new System.Drawing.Point(7, 28);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(455, 39);
            this.panel5.TabIndex = 79;
            // 
            // glsLabel4
            // 
            this.glsLabel4.BottomFill = System.Drawing.Color.Silver;
            this.glsLabel4.Caption = "Roles";
            this.glsLabel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glsLabel4.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.glsLabel4.ForeColor = System.Drawing.Color.White;
            this.glsLabel4.Location = new System.Drawing.Point(0, 0);
            this.glsLabel4.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.glsLabel4.Name = "glsLabel4";
            this.glsLabel4.Size = new System.Drawing.Size(451, 35);
            this.glsLabel4.TabIndex = 1;
            this.glsLabel4.TopFill = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            // 
            // rolePrvldgsListView
            // 
            this.rolePrvldgsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rolePrvldgsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader16,
            this.columnHeader17,
            this.columnHeader18,
            this.columnHeader20,
            this.columnHeader21,
            this.columnHeader19,
            this.columnHeader22});
            this.rolePrvldgsListView.ContextMenuStrip = this.rolesPrvlgsContextMenuStrip;
            this.rolePrvldgsListView.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rolePrvldgsListView.FullRowSelect = true;
            this.rolePrvldgsListView.Location = new System.Drawing.Point(467, 100);
            this.rolePrvldgsListView.MinimumSize = new System.Drawing.Size(475, 300);
            this.rolePrvldgsListView.Name = "rolePrvldgsListView";
            this.rolePrvldgsListView.ShowItemToolTips = true;
            this.rolePrvldgsListView.Size = new System.Drawing.Size(550, 546);
            this.rolePrvldgsListView.TabIndex = 1;
            this.rolePrvldgsListView.UseCompatibleStateImageBehavior = false;
            this.rolePrvldgsListView.View = System.Windows.Forms.View.Details;
            this.rolePrvldgsListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rolePrvldgsListView_KeyDown);
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "No.";
            this.columnHeader16.Width = 45;
            // 
            // columnHeader17
            // 
            this.columnHeader17.Text = "Priviledge Name";
            this.columnHeader17.Width = 200;
            // 
            // columnHeader18
            // 
            this.columnHeader18.Text = "Owner Module";
            this.columnHeader18.Width = 132;
            // 
            // columnHeader20
            // 
            this.columnHeader20.Text = "Start Date";
            this.columnHeader20.Width = 120;
            // 
            // columnHeader21
            // 
            this.columnHeader21.Text = "End Date";
            this.columnHeader21.Width = 120;
            // 
            // columnHeader19
            // 
            this.columnHeader19.Text = "PrvldgID";
            this.columnHeader19.Width = 0;
            // 
            // columnHeader22
            // 
            this.columnHeader22.Text = "ModuleID";
            this.columnHeader22.Width = 0;
            // 
            // rolesPrvlgsContextMenuStrip
            // 
            this.rolesPrvlgsContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addRlPrvldgMenuItem,
            this.toolStripSeparator28,
            this.exptRolePrvldgMenuItem,
            this.refreshRlPrvldgMenuItem,
            this.recHstryRlPrvldgMenuItem,
            this.vwSQLRlPrvldgMenuItem});
            this.rolesPrvlgsContextMenuStrip.Name = "userRolesContextMenuStrip";
            this.rolesPrvlgsContextMenuStrip.Size = new System.Drawing.Size(179, 120);
            // 
            // addRlPrvldgMenuItem
            // 
            this.addRlPrvldgMenuItem.Image = global::SystemAdministration.Properties.Resources.plus_32;
            this.addRlPrvldgMenuItem.Name = "addRlPrvldgMenuItem";
            this.addRlPrvldgMenuItem.Size = new System.Drawing.Size(178, 22);
            this.addRlPrvldgMenuItem.Text = "&Add/Edit User Roles";
            this.addRlPrvldgMenuItem.Click += new System.EventHandler(this.addRlPrvldgMenuItem_Click);
            // 
            // toolStripSeparator28
            // 
            this.toolStripSeparator28.Name = "toolStripSeparator28";
            this.toolStripSeparator28.Size = new System.Drawing.Size(175, 6);
            // 
            // exptRolePrvldgMenuItem
            // 
            this.exptRolePrvldgMenuItem.Image = global::SystemAdministration.Properties.Resources.image007;
            this.exptRolePrvldgMenuItem.Name = "exptRolePrvldgMenuItem";
            this.exptRolePrvldgMenuItem.Size = new System.Drawing.Size(178, 22);
            this.exptRolePrvldgMenuItem.Text = "Export to Excel";
            this.exptRolePrvldgMenuItem.Click += new System.EventHandler(this.exptRolePrvldgMenuItem_Click);
            // 
            // refreshRlPrvldgMenuItem
            // 
            this.refreshRlPrvldgMenuItem.Image = global::SystemAdministration.Properties.Resources.refresh;
            this.refreshRlPrvldgMenuItem.Name = "refreshRlPrvldgMenuItem";
            this.refreshRlPrvldgMenuItem.Size = new System.Drawing.Size(178, 22);
            this.refreshRlPrvldgMenuItem.Text = "&Refresh";
            this.refreshRlPrvldgMenuItem.Click += new System.EventHandler(this.refreshRlPrvldgMenuItem_Click);
            // 
            // recHstryRlPrvldgMenuItem
            // 
            this.recHstryRlPrvldgMenuItem.Image = global::SystemAdministration.Properties.Resources.statistics_32;
            this.recHstryRlPrvldgMenuItem.Name = "recHstryRlPrvldgMenuItem";
            this.recHstryRlPrvldgMenuItem.Size = new System.Drawing.Size(178, 22);
            this.recHstryRlPrvldgMenuItem.Text = "Record &History";
            this.recHstryRlPrvldgMenuItem.Click += new System.EventHandler(this.recHstryRlPrvldgMenuItem_Click);
            // 
            // vwSQLRlPrvldgMenuItem
            // 
            this.vwSQLRlPrvldgMenuItem.Image = global::SystemAdministration.Properties.Resources.SQL;
            this.vwSQLRlPrvldgMenuItem.Name = "vwSQLRlPrvldgMenuItem";
            this.vwSQLRlPrvldgMenuItem.Size = new System.Drawing.Size(178, 22);
            this.vwSQLRlPrvldgMenuItem.Text = "&View SQL";
            this.vwSQLRlPrvldgMenuItem.Click += new System.EventHandler(this.vwSQLRlPrvldgMenuItem_Click);
            // 
            // rolesListView
            // 
            this.rolesListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.rolesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader14,
            this.columnHeader15,
            this.columnHeader61});
            this.rolesListView.ContextMenuStrip = this.rolesContextMenuStrip;
            this.rolesListView.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rolesListView.FullRowSelect = true;
            this.rolesListView.GridLines = true;
            this.rolesListView.HideSelection = false;
            this.rolesListView.Location = new System.Drawing.Point(7, 100);
            this.rolesListView.MinimumSize = new System.Drawing.Size(373, 300);
            this.rolesListView.MultiSelect = false;
            this.rolesListView.Name = "rolesListView";
            this.rolesListView.ShowItemToolTips = true;
            this.rolesListView.Size = new System.Drawing.Size(456, 546);
            this.rolesListView.TabIndex = 0;
            this.rolesListView.UseCompatibleStateImageBehavior = false;
            this.rolesListView.View = System.Windows.Forms.View.Details;
            this.rolesListView.SelectedIndexChanged += new System.EventHandler(this.rolesListView_SelectedIndexChanged);
            this.rolesListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rolesListView_KeyDown);
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "No.";
            this.columnHeader11.Width = 45;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Role Name";
            this.columnHeader12.Width = 200;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "Valid Start Date";
            this.columnHeader13.Width = 120;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Valid End Date";
            this.columnHeader14.Width = 120;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "RoleID";
            this.columnHeader15.Width = 0;
            // 
            // columnHeader61
            // 
            this.columnHeader61.Text = "Can Mini Admins Assign?";
            this.columnHeader61.Width = 160;
            // 
            // rolesContextMenuStrip
            // 
            this.rolesContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addRoleMainMenuItem,
            this.editRoleMainMenuItem,
            this.toolStripSeparator27,
            this.exptRolesMenuItem,
            this.refreshRoleMainMenuItem,
            this.recHstryRoleMainMenuItem,
            this.vwSQLRoleMainMenuItem});
            this.rolesContextMenuStrip.Name = "usersContextMenuStrip";
            this.rolesContextMenuStrip.Size = new System.Drawing.Size(153, 142);
            // 
            // addRoleMainMenuItem
            // 
            this.addRoleMainMenuItem.Image = global::SystemAdministration.Properties.Resources.plus_32;
            this.addRoleMainMenuItem.Name = "addRoleMainMenuItem";
            this.addRoleMainMenuItem.Size = new System.Drawing.Size(152, 22);
            this.addRoleMainMenuItem.Text = "&Add Role";
            this.addRoleMainMenuItem.Click += new System.EventHandler(this.addRoleMainMenuItem_Click);
            // 
            // editRoleMainMenuItem
            // 
            this.editRoleMainMenuItem.Image = global::SystemAdministration.Properties.Resources.edit32;
            this.editRoleMainMenuItem.Name = "editRoleMainMenuItem";
            this.editRoleMainMenuItem.Size = new System.Drawing.Size(152, 22);
            this.editRoleMainMenuItem.Text = "&Edit Role";
            this.editRoleMainMenuItem.Click += new System.EventHandler(this.editRoleMainMenuItem_Click);
            // 
            // toolStripSeparator27
            // 
            this.toolStripSeparator27.Name = "toolStripSeparator27";
            this.toolStripSeparator27.Size = new System.Drawing.Size(149, 6);
            // 
            // exptRolesMenuItem
            // 
            this.exptRolesMenuItem.Image = global::SystemAdministration.Properties.Resources.image007;
            this.exptRolesMenuItem.Name = "exptRolesMenuItem";
            this.exptRolesMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exptRolesMenuItem.Text = "Export to Excel";
            this.exptRolesMenuItem.Click += new System.EventHandler(this.exptRolesMenuItem_Click);
            // 
            // refreshRoleMainMenuItem
            // 
            this.refreshRoleMainMenuItem.Image = global::SystemAdministration.Properties.Resources.refresh;
            this.refreshRoleMainMenuItem.Name = "refreshRoleMainMenuItem";
            this.refreshRoleMainMenuItem.Size = new System.Drawing.Size(152, 22);
            this.refreshRoleMainMenuItem.Text = "&Refresh";
            this.refreshRoleMainMenuItem.Click += new System.EventHandler(this.refreshRoleMainMenuItem_Click);
            // 
            // recHstryRoleMainMenuItem
            // 
            this.recHstryRoleMainMenuItem.Image = global::SystemAdministration.Properties.Resources.statistics_32;
            this.recHstryRoleMainMenuItem.Name = "recHstryRoleMainMenuItem";
            this.recHstryRoleMainMenuItem.Size = new System.Drawing.Size(152, 22);
            this.recHstryRoleMainMenuItem.Text = "Record &History";
            this.recHstryRoleMainMenuItem.Click += new System.EventHandler(this.recHstryRoleMainMenuItem_Click);
            // 
            // vwSQLRoleMainMenuItem
            // 
            this.vwSQLRoleMainMenuItem.Image = global::SystemAdministration.Properties.Resources.SQL;
            this.vwSQLRoleMainMenuItem.Name = "vwSQLRoleMainMenuItem";
            this.vwSQLRoleMainMenuItem.Size = new System.Drawing.Size(152, 22);
            this.vwSQLRoleMainMenuItem.Text = "&View SQL";
            this.vwSQLRoleMainMenuItem.Click += new System.EventHandler(this.vwSQLRoleMainMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moveFirstRoleButton,
            this.toolStripSeparator3,
            this.movePreviousRoleButton,
            this.toolStripSeparator4,
            this.toolStripLabel4,
            this.positionRoleTextBox,
            this.totalRecRoleLabel,
            this.toolStripSeparator5,
            this.moveNextRoleButton,
            this.toolStripSeparator6,
            this.moveLastRoleButton,
            this.toolStripSeparator7,
            this.dsplySizeRoleComboBox,
            this.toolStripSeparator14,
            this.toolStripLabel6,
            this.toolStripSeparator23,
            this.searchForRoleTextBox,
            this.toolStripSeparator24,
            this.toolStripLabel7,
            this.toolStripSeparator25,
            this.searchInRoleComboBox,
            this.toolStripSeparator26,
            this.refreshRoleButton});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1019, 25);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.TabStop = true;
            this.toolStrip1.Text = "ToolStrip2";
            // 
            // moveFirstRoleButton
            // 
            this.moveFirstRoleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveFirstRoleButton.Image = global::SystemAdministration.Properties.Resources.DataContainer_MoveFirstHS;
            this.moveFirstRoleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveFirstRoleButton.Name = "moveFirstRoleButton";
            this.moveFirstRoleButton.Size = new System.Drawing.Size(23, 22);
            this.moveFirstRoleButton.Text = "Move First";
            this.moveFirstRoleButton.Click += new System.EventHandler(this.rolePnlNavButtons);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // movePreviousRoleButton
            // 
            this.movePreviousRoleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.movePreviousRoleButton.Image = global::SystemAdministration.Properties.Resources.DataContainer_MovePreviousHS;
            this.movePreviousRoleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.movePreviousRoleButton.Name = "movePreviousRoleButton";
            this.movePreviousRoleButton.Size = new System.Drawing.Size(23, 22);
            this.movePreviousRoleButton.Text = "Move Previous";
            this.movePreviousRoleButton.Click += new System.EventHandler(this.rolePnlNavButtons);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.AutoToolTip = true;
            this.toolStripLabel4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(47, 22);
            this.toolStripLabel4.Text = "Record";
            // 
            // positionRoleTextBox
            // 
            this.positionRoleTextBox.AutoToolTip = true;
            this.positionRoleTextBox.BackColor = System.Drawing.Color.White;
            this.positionRoleTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.positionRoleTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.positionRoleTextBox.Name = "positionRoleTextBox";
            this.positionRoleTextBox.ReadOnly = true;
            this.positionRoleTextBox.Size = new System.Drawing.Size(70, 25);
            this.positionRoleTextBox.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.positionRoleTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.positionRoleTextBox_KeyDown);
            // 
            // totalRecRoleLabel
            // 
            this.totalRecRoleLabel.AutoToolTip = true;
            this.totalRecRoleLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalRecRoleLabel.Name = "totalRecRoleLabel";
            this.totalRecRoleLabel.Size = new System.Drawing.Size(50, 22);
            this.totalRecRoleLabel.Text = "of Total";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // moveNextRoleButton
            // 
            this.moveNextRoleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveNextRoleButton.Image = global::SystemAdministration.Properties.Resources.DataContainer_MoveNextHS;
            this.moveNextRoleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveNextRoleButton.Name = "moveNextRoleButton";
            this.moveNextRoleButton.Size = new System.Drawing.Size(23, 22);
            this.moveNextRoleButton.Text = "Move Next";
            this.moveNextRoleButton.Click += new System.EventHandler(this.rolePnlNavButtons);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // moveLastRoleButton
            // 
            this.moveLastRoleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveLastRoleButton.Image = global::SystemAdministration.Properties.Resources.DataContainer_MoveLastHS;
            this.moveLastRoleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveLastRoleButton.Name = "moveLastRoleButton";
            this.moveLastRoleButton.Size = new System.Drawing.Size(23, 22);
            this.moveLastRoleButton.Text = "Move Last";
            this.moveLastRoleButton.Click += new System.EventHandler(this.rolePnlNavButtons);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // dsplySizeRoleComboBox
            // 
            this.dsplySizeRoleComboBox.AutoSize = false;
            this.dsplySizeRoleComboBox.Items.AddRange(new object[] {
            "1",
            "5",
            "10",
            "15",
            "20",
            "30",
            "40",
            "50",
            "100"});
            this.dsplySizeRoleComboBox.Name = "dsplySizeRoleComboBox";
            this.dsplySizeRoleComboBox.Size = new System.Drawing.Size(40, 23);
            this.dsplySizeRoleComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForRoleTextBox_KeyDown);
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel6
            // 
            this.toolStripLabel6.Name = "toolStripLabel6";
            this.toolStripLabel6.Size = new System.Drawing.Size(65, 22);
            this.toolStripLabel6.Text = "Search For:";
            // 
            // toolStripSeparator23
            // 
            this.toolStripSeparator23.Name = "toolStripSeparator23";
            this.toolStripSeparator23.Size = new System.Drawing.Size(6, 25);
            // 
            // searchForRoleTextBox
            // 
            this.searchForRoleTextBox.Name = "searchForRoleTextBox";
            this.searchForRoleTextBox.Size = new System.Drawing.Size(100, 25);
            this.searchForRoleTextBox.Enter += new System.EventHandler(this.searchForRoleTextBox_Click);
            this.searchForRoleTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForRoleTextBox_KeyDown);
            this.searchForRoleTextBox.Click += new System.EventHandler(this.searchForRoleTextBox_Click);
            // 
            // toolStripSeparator24
            // 
            this.toolStripSeparator24.Name = "toolStripSeparator24";
            this.toolStripSeparator24.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel7
            // 
            this.toolStripLabel7.Name = "toolStripLabel7";
            this.toolStripLabel7.Size = new System.Drawing.Size(58, 22);
            this.toolStripLabel7.Text = "Search In:";
            // 
            // toolStripSeparator25
            // 
            this.toolStripSeparator25.Name = "toolStripSeparator25";
            this.toolStripSeparator25.Size = new System.Drawing.Size(6, 25);
            // 
            // searchInRoleComboBox
            // 
            this.searchInRoleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.searchInRoleComboBox.Items.AddRange(new object[] {
            "Owner Module",
            "Priviledge Name",
            "Role Name"});
            this.searchInRoleComboBox.Name = "searchInRoleComboBox";
            this.searchInRoleComboBox.Size = new System.Drawing.Size(121, 25);
            this.searchInRoleComboBox.Sorted = true;
            this.searchInRoleComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForRoleTextBox_KeyDown);
            // 
            // toolStripSeparator26
            // 
            this.toolStripSeparator26.Name = "toolStripSeparator26";
            this.toolStripSeparator26.Size = new System.Drawing.Size(6, 25);
            // 
            // refreshRoleButton
            // 
            this.refreshRoleButton.Image = global::SystemAdministration.Properties.Resources.action_go;
            this.refreshRoleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshRoleButton.Name = "refreshRoleButton";
            this.refreshRoleButton.Size = new System.Drawing.Size(42, 22);
            this.refreshRoleButton.Text = "Go";
            this.refreshRoleButton.Click += new System.EventHandler(this.refreshRoleButton_Click);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(389, 279);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(268, 23);
            this.label8.TabIndex = 82;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.modulesPanel);
            this.tabPage3.ImageKey = "shield_64.png";
            this.tabPage3.Location = new System.Drawing.Point(4, 60);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1025, 655);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "MODULES && THEIR PRIVILEDGES";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // modulesPanel
            // 
            this.modulesPanel.AutoScroll = true;
            this.modulesPanel.BackColor = System.Drawing.Color.LightSlateGray;
            this.modulesPanel.Controls.Add(this.modulePrvldgListView);
            this.modulesPanel.Controls.Add(this.panel11);
            this.modulesPanel.Controls.Add(this.modulesListView);
            this.modulesPanel.Controls.Add(this.panel10);
            this.modulesPanel.Controls.Add(this.toolStrip2);
            this.modulesPanel.Controls.Add(this.label9);
            this.modulesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modulesPanel.Enabled = false;
            this.modulesPanel.Location = new System.Drawing.Point(3, 3);
            this.modulesPanel.Name = "modulesPanel";
            this.modulesPanel.Size = new System.Drawing.Size(1019, 649);
            this.modulesPanel.TabIndex = 1;
            this.modulesPanel.Visible = false;
            // 
            // modulePrvldgListView
            // 
            this.modulePrvldgListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.modulePrvldgListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader25,
            this.columnHeader26,
            this.columnHeader31});
            this.modulePrvldgListView.ContextMenuStrip = this.modulePrvlgContextMenuStrip;
            this.modulePrvldgListView.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modulePrvldgListView.FullRowSelect = true;
            this.modulePrvldgListView.Location = new System.Drawing.Point(552, 70);
            this.modulePrvldgListView.MinimumSize = new System.Drawing.Size(327, 300);
            this.modulePrvldgListView.Name = "modulePrvldgListView";
            this.modulePrvldgListView.ShowItemToolTips = true;
            this.modulePrvldgListView.Size = new System.Drawing.Size(464, 576);
            this.modulePrvldgListView.TabIndex = 1;
            this.modulePrvldgListView.UseCompatibleStateImageBehavior = false;
            this.modulePrvldgListView.View = System.Windows.Forms.View.Details;
            this.modulePrvldgListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.modulePrvldgListView_KeyDown);
            // 
            // columnHeader25
            // 
            this.columnHeader25.Text = "No.";
            this.columnHeader25.Width = 45;
            // 
            // columnHeader26
            // 
            this.columnHeader26.Text = "Priviledge Name";
            this.columnHeader26.Width = 460;
            // 
            // columnHeader31
            // 
            this.columnHeader31.Text = "PrvldgID";
            this.columnHeader31.Width = 0;
            // 
            // modulePrvlgContextMenuStrip
            // 
            this.modulePrvlgContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exptMdlPrvldgMenuItem,
            this.refreshMdlPrvldgMenuItem,
            this.toolStripSeparator54,
            this.vwSqlMdlPrvldgMenuItem});
            this.modulePrvlgContextMenuStrip.Name = "usersContextMenuStrip";
            this.modulePrvlgContextMenuStrip.Size = new System.Drawing.Size(151, 76);
            // 
            // exptMdlPrvldgMenuItem
            // 
            this.exptMdlPrvldgMenuItem.Image = global::SystemAdministration.Properties.Resources.image007;
            this.exptMdlPrvldgMenuItem.Name = "exptMdlPrvldgMenuItem";
            this.exptMdlPrvldgMenuItem.Size = new System.Drawing.Size(150, 22);
            this.exptMdlPrvldgMenuItem.Text = "Export to Excel";
            this.exptMdlPrvldgMenuItem.Click += new System.EventHandler(this.exptMdlPrvldgMenuItem_Click);
            // 
            // refreshMdlPrvldgMenuItem
            // 
            this.refreshMdlPrvldgMenuItem.Image = global::SystemAdministration.Properties.Resources.refresh;
            this.refreshMdlPrvldgMenuItem.Name = "refreshMdlPrvldgMenuItem";
            this.refreshMdlPrvldgMenuItem.Size = new System.Drawing.Size(150, 22);
            this.refreshMdlPrvldgMenuItem.Text = "&Refresh";
            this.refreshMdlPrvldgMenuItem.Click += new System.EventHandler(this.refreshMdlPrvldgMenuItem_Click);
            // 
            // toolStripSeparator54
            // 
            this.toolStripSeparator54.Name = "toolStripSeparator54";
            this.toolStripSeparator54.Size = new System.Drawing.Size(147, 6);
            // 
            // vwSqlMdlPrvldgMenuItem
            // 
            this.vwSqlMdlPrvldgMenuItem.Image = global::SystemAdministration.Properties.Resources.SQL;
            this.vwSqlMdlPrvldgMenuItem.Name = "vwSqlMdlPrvldgMenuItem";
            this.vwSqlMdlPrvldgMenuItem.Size = new System.Drawing.Size(150, 22);
            this.vwSqlMdlPrvldgMenuItem.Text = "&View SQL";
            this.vwSqlMdlPrvldgMenuItem.Click += new System.EventHandler(this.vwSqlMdlPrvldgMenuItem_Click);
            // 
            // panel11
            // 
            this.panel11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel11.Controls.Add(this.glsLabel10);
            this.panel11.Location = new System.Drawing.Point(7, 28);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(538, 39);
            this.panel11.TabIndex = 81;
            // 
            // glsLabel10
            // 
            this.glsLabel10.BottomFill = System.Drawing.Color.Silver;
            this.glsLabel10.Caption = "Modules";
            this.glsLabel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glsLabel10.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.glsLabel10.ForeColor = System.Drawing.Color.White;
            this.glsLabel10.Location = new System.Drawing.Point(0, 0);
            this.glsLabel10.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.glsLabel10.Name = "glsLabel10";
            this.glsLabel10.Size = new System.Drawing.Size(534, 35);
            this.glsLabel10.TabIndex = 1;
            this.glsLabel10.TopFill = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            // 
            // modulesListView
            // 
            this.modulesListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.modulesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader23,
            this.columnHeader24,
            this.columnHeader28,
            this.columnHeader29,
            this.columnHeader30,
            this.columnHeader27});
            this.modulesListView.ContextMenuStrip = this.modulesContextMenuStrip;
            this.modulesListView.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modulesListView.FullRowSelect = true;
            this.modulesListView.GridLines = true;
            this.modulesListView.HideSelection = false;
            this.modulesListView.Location = new System.Drawing.Point(7, 70);
            this.modulesListView.MinimumSize = new System.Drawing.Size(539, 300);
            this.modulesListView.MultiSelect = false;
            this.modulesListView.Name = "modulesListView";
            this.modulesListView.ShowItemToolTips = true;
            this.modulesListView.Size = new System.Drawing.Size(539, 576);
            this.modulesListView.TabIndex = 0;
            this.modulesListView.UseCompatibleStateImageBehavior = false;
            this.modulesListView.View = System.Windows.Forms.View.Details;
            this.modulesListView.SelectedIndexChanged += new System.EventHandler(this.moulesListView_SelectedIndexChanged);
            this.modulesListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.modulesListView_KeyDown);
            // 
            // columnHeader23
            // 
            this.columnHeader23.Text = "No.";
            this.columnHeader23.Width = 45;
            // 
            // columnHeader24
            // 
            this.columnHeader24.Text = "Module Name";
            this.columnHeader24.Width = 125;
            // 
            // columnHeader28
            // 
            this.columnHeader28.Text = "Module Description";
            this.columnHeader28.Width = 180;
            // 
            // columnHeader29
            // 
            this.columnHeader29.Text = "Date Added";
            this.columnHeader29.Width = 74;
            // 
            // columnHeader30
            // 
            this.columnHeader30.Text = "Audit Trail Table ";
            this.columnHeader30.Width = 105;
            // 
            // columnHeader27
            // 
            this.columnHeader27.Text = "ModuleID";
            this.columnHeader27.Width = 0;
            // 
            // modulesContextMenuStrip
            // 
            this.modulesContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exptMdlMenuItem,
            this.refreshMdlMenuItem,
            this.toolStripSeparator53,
            this.vwSQLMdlMenuItem});
            this.modulesContextMenuStrip.Name = "userRolesContextMenuStrip";
            this.modulesContextMenuStrip.Size = new System.Drawing.Size(151, 76);
            // 
            // exptMdlMenuItem
            // 
            this.exptMdlMenuItem.Image = global::SystemAdministration.Properties.Resources.image007;
            this.exptMdlMenuItem.Name = "exptMdlMenuItem";
            this.exptMdlMenuItem.Size = new System.Drawing.Size(150, 22);
            this.exptMdlMenuItem.Text = "Export to Excel";
            this.exptMdlMenuItem.Click += new System.EventHandler(this.exptMdlMenuItem_Click);
            // 
            // refreshMdlMenuItem
            // 
            this.refreshMdlMenuItem.Image = global::SystemAdministration.Properties.Resources.refresh;
            this.refreshMdlMenuItem.Name = "refreshMdlMenuItem";
            this.refreshMdlMenuItem.Size = new System.Drawing.Size(150, 22);
            this.refreshMdlMenuItem.Text = "&Refresh";
            this.refreshMdlMenuItem.Click += new System.EventHandler(this.refreshMdlMenuItem_Click);
            // 
            // toolStripSeparator53
            // 
            this.toolStripSeparator53.Name = "toolStripSeparator53";
            this.toolStripSeparator53.Size = new System.Drawing.Size(147, 6);
            // 
            // vwSQLMdlMenuItem
            // 
            this.vwSQLMdlMenuItem.Image = global::SystemAdministration.Properties.Resources.SQL;
            this.vwSQLMdlMenuItem.Name = "vwSQLMdlMenuItem";
            this.vwSQLMdlMenuItem.Size = new System.Drawing.Size(150, 22);
            this.vwSQLMdlMenuItem.Text = "&View SQL";
            this.vwSQLMdlMenuItem.Click += new System.EventHandler(this.vwSQLMdlMenuItem_Click);
            // 
            // panel10
            // 
            this.panel10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel10.Controls.Add(this.glsLabel9);
            this.panel10.Location = new System.Drawing.Point(552, 28);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(463, 39);
            this.panel10.TabIndex = 82;
            // 
            // glsLabel9
            // 
            this.glsLabel9.BottomFill = System.Drawing.Color.Silver;
            this.glsLabel9.Caption = "Corresponding Priviledges";
            this.glsLabel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glsLabel9.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.glsLabel9.ForeColor = System.Drawing.Color.White;
            this.glsLabel9.Location = new System.Drawing.Point(0, 0);
            this.glsLabel9.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.glsLabel9.Name = "glsLabel9";
            this.glsLabel9.Size = new System.Drawing.Size(459, 35);
            this.glsLabel9.TabIndex = 1;
            this.glsLabel9.TopFill = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            // 
            // toolStrip2
            // 
            this.toolStrip2.AutoSize = false;
            this.toolStrip2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moveFirstMdlButton,
            this.toolStripSeparator29,
            this.movePreviousMdlButton,
            this.toolStripSeparator30,
            this.toolStripLabel5,
            this.positionMdlTextBox,
            this.totalRecMdlLabel,
            this.toolStripSeparator31,
            this.moveNextMdlButton,
            this.toolStripSeparator32,
            this.moveLastMdlButton,
            this.toolStripSeparator34,
            this.dsplySizeMdlComboBox,
            this.toolStripSeparator35,
            this.toolStripLabel9,
            this.toolStripSeparator37,
            this.searchForMdlTextBox,
            this.toolStripSeparator38,
            this.toolStripLabel10,
            this.toolStripSeparator39,
            this.searchInMdlComboBox,
            this.toolStripSeparator40,
            this.refreshMdlButton});
            this.toolStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(1019, 25);
            this.toolStrip2.Stretch = true;
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.TabStop = true;
            this.toolStrip2.Text = "ToolStrip2";
            // 
            // moveFirstMdlButton
            // 
            this.moveFirstMdlButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveFirstMdlButton.Image = global::SystemAdministration.Properties.Resources.DataContainer_MoveFirstHS;
            this.moveFirstMdlButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveFirstMdlButton.Name = "moveFirstMdlButton";
            this.moveFirstMdlButton.Size = new System.Drawing.Size(23, 22);
            this.moveFirstMdlButton.Text = "Move First";
            this.moveFirstMdlButton.Click += new System.EventHandler(this.mdlPnlNavButtons);
            // 
            // toolStripSeparator29
            // 
            this.toolStripSeparator29.Name = "toolStripSeparator29";
            this.toolStripSeparator29.Size = new System.Drawing.Size(6, 25);
            // 
            // movePreviousMdlButton
            // 
            this.movePreviousMdlButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.movePreviousMdlButton.Image = global::SystemAdministration.Properties.Resources.DataContainer_MovePreviousHS;
            this.movePreviousMdlButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.movePreviousMdlButton.Name = "movePreviousMdlButton";
            this.movePreviousMdlButton.Size = new System.Drawing.Size(23, 22);
            this.movePreviousMdlButton.Text = "Move Previous";
            this.movePreviousMdlButton.Click += new System.EventHandler(this.mdlPnlNavButtons);
            // 
            // toolStripSeparator30
            // 
            this.toolStripSeparator30.Name = "toolStripSeparator30";
            this.toolStripSeparator30.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.AutoToolTip = true;
            this.toolStripLabel5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(47, 22);
            this.toolStripLabel5.Text = "Record";
            // 
            // positionMdlTextBox
            // 
            this.positionMdlTextBox.AutoToolTip = true;
            this.positionMdlTextBox.BackColor = System.Drawing.Color.White;
            this.positionMdlTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.positionMdlTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.positionMdlTextBox.Name = "positionMdlTextBox";
            this.positionMdlTextBox.ReadOnly = true;
            this.positionMdlTextBox.Size = new System.Drawing.Size(70, 25);
            this.positionMdlTextBox.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.positionMdlTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.positionMdlTextBox_KeyDown);
            // 
            // totalRecMdlLabel
            // 
            this.totalRecMdlLabel.AutoToolTip = true;
            this.totalRecMdlLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalRecMdlLabel.Name = "totalRecMdlLabel";
            this.totalRecMdlLabel.Size = new System.Drawing.Size(50, 22);
            this.totalRecMdlLabel.Text = "of Total";
            // 
            // toolStripSeparator31
            // 
            this.toolStripSeparator31.Name = "toolStripSeparator31";
            this.toolStripSeparator31.Size = new System.Drawing.Size(6, 25);
            // 
            // moveNextMdlButton
            // 
            this.moveNextMdlButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveNextMdlButton.Image = global::SystemAdministration.Properties.Resources.DataContainer_MoveNextHS;
            this.moveNextMdlButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveNextMdlButton.Name = "moveNextMdlButton";
            this.moveNextMdlButton.Size = new System.Drawing.Size(23, 22);
            this.moveNextMdlButton.Text = "Move Next";
            this.moveNextMdlButton.Click += new System.EventHandler(this.mdlPnlNavButtons);
            // 
            // toolStripSeparator32
            // 
            this.toolStripSeparator32.Name = "toolStripSeparator32";
            this.toolStripSeparator32.Size = new System.Drawing.Size(6, 25);
            // 
            // moveLastMdlButton
            // 
            this.moveLastMdlButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveLastMdlButton.Image = global::SystemAdministration.Properties.Resources.DataContainer_MoveLastHS;
            this.moveLastMdlButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveLastMdlButton.Name = "moveLastMdlButton";
            this.moveLastMdlButton.Size = new System.Drawing.Size(23, 22);
            this.moveLastMdlButton.Text = "Move Last";
            this.moveLastMdlButton.Click += new System.EventHandler(this.mdlPnlNavButtons);
            // 
            // toolStripSeparator34
            // 
            this.toolStripSeparator34.Name = "toolStripSeparator34";
            this.toolStripSeparator34.Size = new System.Drawing.Size(6, 25);
            // 
            // dsplySizeMdlComboBox
            // 
            this.dsplySizeMdlComboBox.AutoSize = false;
            this.dsplySizeMdlComboBox.Items.AddRange(new object[] {
            "1",
            "5",
            "10",
            "15",
            "20",
            "30",
            "40",
            "50",
            "100"});
            this.dsplySizeMdlComboBox.Name = "dsplySizeMdlComboBox";
            this.dsplySizeMdlComboBox.Size = new System.Drawing.Size(40, 23);
            this.dsplySizeMdlComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForMdlTextBox_KeyDown);
            // 
            // toolStripSeparator35
            // 
            this.toolStripSeparator35.Name = "toolStripSeparator35";
            this.toolStripSeparator35.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel9
            // 
            this.toolStripLabel9.Name = "toolStripLabel9";
            this.toolStripLabel9.Size = new System.Drawing.Size(65, 22);
            this.toolStripLabel9.Text = "Search For:";
            // 
            // toolStripSeparator37
            // 
            this.toolStripSeparator37.Name = "toolStripSeparator37";
            this.toolStripSeparator37.Size = new System.Drawing.Size(6, 25);
            // 
            // searchForMdlTextBox
            // 
            this.searchForMdlTextBox.Name = "searchForMdlTextBox";
            this.searchForMdlTextBox.Size = new System.Drawing.Size(100, 25);
            this.searchForMdlTextBox.Enter += new System.EventHandler(this.searchForMdlTextBox_Click);
            this.searchForMdlTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForMdlTextBox_KeyDown);
            this.searchForMdlTextBox.Click += new System.EventHandler(this.searchForMdlTextBox_Click);
            // 
            // toolStripSeparator38
            // 
            this.toolStripSeparator38.Name = "toolStripSeparator38";
            this.toolStripSeparator38.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel10
            // 
            this.toolStripLabel10.Name = "toolStripLabel10";
            this.toolStripLabel10.Size = new System.Drawing.Size(58, 22);
            this.toolStripLabel10.Text = "Search In:";
            // 
            // toolStripSeparator39
            // 
            this.toolStripSeparator39.Name = "toolStripSeparator39";
            this.toolStripSeparator39.Size = new System.Drawing.Size(6, 25);
            // 
            // searchInMdlComboBox
            // 
            this.searchInMdlComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.searchInMdlComboBox.Items.AddRange(new object[] {
            "Module Name",
            "Priviledge Name"});
            this.searchInMdlComboBox.Name = "searchInMdlComboBox";
            this.searchInMdlComboBox.Size = new System.Drawing.Size(121, 25);
            this.searchInMdlComboBox.Sorted = true;
            this.searchInMdlComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForMdlTextBox_KeyDown);
            // 
            // toolStripSeparator40
            // 
            this.toolStripSeparator40.Name = "toolStripSeparator40";
            this.toolStripSeparator40.Size = new System.Drawing.Size(6, 25);
            // 
            // refreshMdlButton
            // 
            this.refreshMdlButton.Image = global::SystemAdministration.Properties.Resources.action_go;
            this.refreshMdlButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshMdlButton.Name = "refreshMdlButton";
            this.refreshMdlButton.Size = new System.Drawing.Size(42, 22);
            this.refreshMdlButton.Text = "Go";
            this.refreshMdlButton.Click += new System.EventHandler(this.refreshMdlButton_Click);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(141, 382);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(268, 23);
            this.label9.TabIndex = 85;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.extraInfoPanel);
            this.tabPage4.ImageKey = "shield_64.png";
            this.tabPage4.Location = new System.Drawing.Point(4, 60);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(1025, 655);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "EXTRA INFO LABELS";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // extraInfoPanel
            // 
            this.extraInfoPanel.AutoScroll = true;
            this.extraInfoPanel.BackColor = System.Drawing.Color.LightSlateGray;
            this.extraInfoPanel.Controls.Add(this.toolStrip12);
            this.extraInfoPanel.Controls.Add(this.extInfLabelListView);
            this.extraInfoPanel.Controls.Add(this.panel23);
            this.extraInfoPanel.Controls.Add(this.extInfSubGroupsListView);
            this.extraInfoPanel.Controls.Add(this.panel20);
            this.extraInfoPanel.Controls.Add(this.extInfoModuleListView);
            this.extraInfoPanel.Controls.Add(this.panel21);
            this.extraInfoPanel.Controls.Add(this.toolStrip7);
            this.extraInfoPanel.Controls.Add(this.label36);
            this.extraInfoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.extraInfoPanel.Enabled = false;
            this.extraInfoPanel.Location = new System.Drawing.Point(3, 3);
            this.extraInfoPanel.Name = "extraInfoPanel";
            this.extraInfoPanel.Size = new System.Drawing.Size(1019, 649);
            this.extraInfoPanel.TabIndex = 3;
            this.extraInfoPanel.Visible = false;
            // 
            // toolStrip12
            // 
            this.toolStrip12.AutoSize = false;
            this.toolStrip12.BackColor = System.Drawing.Color.WhiteSmoke;
            this.toolStrip12.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip12.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addEditExtInfButton,
            this.enableDisableButton,
            this.delLblButton});
            this.toolStrip12.Location = new System.Drawing.Point(685, 71);
            this.toolStrip12.Name = "toolStrip12";
            this.toolStrip12.Size = new System.Drawing.Size(333, 25);
            this.toolStrip12.TabIndex = 87;
            this.toolStrip12.TabStop = true;
            this.toolStrip12.Text = "toolStrip12";
            // 
            // addEditExtInfButton
            // 
            this.addEditExtInfButton.Image = global::SystemAdministration.Properties.Resources.plus_32;
            this.addEditExtInfButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addEditExtInfButton.Name = "addEditExtInfButton";
            this.addEditExtInfButton.Size = new System.Drawing.Size(87, 22);
            this.addEditExtInfButton.Text = "ADD LABEL";
            this.addEditExtInfButton.Click += new System.EventHandler(this.addEditExtInfButton_Click);
            // 
            // enableDisableButton
            // 
            this.enableDisableButton.Image = global::SystemAdministration.Properties.Resources.edit32;
            this.enableDisableButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.enableDisableButton.Name = "enableDisableButton";
            this.enableDisableButton.Size = new System.Drawing.Size(118, 22);
            this.enableDisableButton.Text = "ENABLE/DISABLE";
            this.enableDisableButton.Click += new System.EventHandler(this.enableDisableButton_Click);
            // 
            // delLblButton
            // 
            this.delLblButton.Image = global::SystemAdministration.Properties.Resources.delete;
            this.delLblButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.delLblButton.Name = "delLblButton";
            this.delLblButton.Size = new System.Drawing.Size(66, 22);
            this.delLblButton.Text = "DELETE";
            this.delLblButton.Click += new System.EventHandler(this.delLblButton_Click);
            // 
            // extInfLabelListView
            // 
            this.extInfLabelListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.extInfLabelListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader51,
            this.columnHeader52,
            this.columnHeader58,
            this.columnHeader53,
            this.columnHeader59});
            this.extInfLabelListView.ContextMenuStrip = this.extInfLabelContextMenuStrip;
            this.extInfLabelListView.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.extInfLabelListView.FullRowSelect = true;
            this.extInfLabelListView.Location = new System.Drawing.Point(683, 100);
            this.extInfLabelListView.MinimumSize = new System.Drawing.Size(327, 300);
            this.extInfLabelListView.Name = "extInfLabelListView";
            this.extInfLabelListView.ShowItemToolTips = true;
            this.extInfLabelListView.Size = new System.Drawing.Size(332, 546);
            this.extInfLabelListView.TabIndex = 2;
            this.extInfLabelListView.UseCompatibleStateImageBehavior = false;
            this.extInfLabelListView.View = System.Windows.Forms.View.Details;
            this.extInfLabelListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.extInfLabelListView_KeyDown);
            // 
            // columnHeader51
            // 
            this.columnHeader51.Text = "No.";
            this.columnHeader51.Width = 45;
            // 
            // columnHeader52
            // 
            this.columnHeader52.Text = "Extra Info Label";
            this.columnHeader52.Width = 200;
            // 
            // columnHeader58
            // 
            this.columnHeader58.Text = "Is Enabled?";
            this.columnHeader58.Width = 73;
            // 
            // columnHeader53
            // 
            this.columnHeader53.Text = "other_info_id";
            this.columnHeader53.Width = 0;
            // 
            // columnHeader59
            // 
            this.columnHeader59.Text = "comb_info_id";
            this.columnHeader59.Width = 0;
            // 
            // extInfLabelContextMenuStrip
            // 
            this.extInfLabelContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addEditExtInfMenuItem,
            this.toolStripSeparator87,
            this.enableDisableToolStripMenuItem,
            this.deleteLaToolStripMenuItem,
            this.toolStripSeparator92,
            this.exptInfLblMenuItem,
            this.refreshExtInfLblMenuItem,
            this.vwSQLExtInfLblMenuItem,
            this.recordHistoryExtInfToolStripMenuItem});
            this.extInfLabelContextMenuStrip.Name = "usersContextMenuStrip";
            this.extInfLabelContextMenuStrip.Size = new System.Drawing.Size(205, 170);
            // 
            // addEditExtInfMenuItem
            // 
            this.addEditExtInfMenuItem.Image = global::SystemAdministration.Properties.Resources.plus_32;
            this.addEditExtInfMenuItem.Name = "addEditExtInfMenuItem";
            this.addEditExtInfMenuItem.Size = new System.Drawing.Size(204, 22);
            this.addEditExtInfMenuItem.Text = "Add/Edit Extra Info Label";
            this.addEditExtInfMenuItem.Click += new System.EventHandler(this.addEditExtInfMenuItem_Click);
            // 
            // toolStripSeparator87
            // 
            this.toolStripSeparator87.Name = "toolStripSeparator87";
            this.toolStripSeparator87.Size = new System.Drawing.Size(201, 6);
            // 
            // enableDisableToolStripMenuItem
            // 
            this.enableDisableToolStripMenuItem.Name = "enableDisableToolStripMenuItem";
            this.enableDisableToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.enableDisableToolStripMenuItem.Text = "Enable/Disable";
            this.enableDisableToolStripMenuItem.Click += new System.EventHandler(this.enableDisableToolStripMenuItem_Click);
            // 
            // deleteLaToolStripMenuItem
            // 
            this.deleteLaToolStripMenuItem.Image = global::SystemAdministration.Properties.Resources.delete;
            this.deleteLaToolStripMenuItem.Name = "deleteLaToolStripMenuItem";
            this.deleteLaToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.deleteLaToolStripMenuItem.Text = "Delete Label(s)";
            this.deleteLaToolStripMenuItem.Click += new System.EventHandler(this.deleteLaToolStripMenuItem_Click);
            // 
            // toolStripSeparator92
            // 
            this.toolStripSeparator92.Name = "toolStripSeparator92";
            this.toolStripSeparator92.Size = new System.Drawing.Size(201, 6);
            // 
            // exptInfLblMenuItem
            // 
            this.exptInfLblMenuItem.Image = global::SystemAdministration.Properties.Resources.image007;
            this.exptInfLblMenuItem.Name = "exptInfLblMenuItem";
            this.exptInfLblMenuItem.Size = new System.Drawing.Size(204, 22);
            this.exptInfLblMenuItem.Text = "Export to Excel";
            this.exptInfLblMenuItem.Click += new System.EventHandler(this.exptInfLblMenuItem_Click);
            // 
            // refreshExtInfLblMenuItem
            // 
            this.refreshExtInfLblMenuItem.Image = global::SystemAdministration.Properties.Resources.refresh;
            this.refreshExtInfLblMenuItem.Name = "refreshExtInfLblMenuItem";
            this.refreshExtInfLblMenuItem.Size = new System.Drawing.Size(204, 22);
            this.refreshExtInfLblMenuItem.Text = "&Refresh";
            this.refreshExtInfLblMenuItem.Click += new System.EventHandler(this.refreshExtInfLblMenuItem_Click);
            // 
            // vwSQLExtInfLblMenuItem
            // 
            this.vwSQLExtInfLblMenuItem.Image = global::SystemAdministration.Properties.Resources.SQL;
            this.vwSQLExtInfLblMenuItem.Name = "vwSQLExtInfLblMenuItem";
            this.vwSQLExtInfLblMenuItem.Size = new System.Drawing.Size(204, 22);
            this.vwSQLExtInfLblMenuItem.Text = "&View SQL";
            this.vwSQLExtInfLblMenuItem.Click += new System.EventHandler(this.vwSQLExtInfLblMenuItem_Click);
            // 
            // recordHistoryExtInfToolStripMenuItem
            // 
            this.recordHistoryExtInfToolStripMenuItem.Image = global::SystemAdministration.Properties.Resources.statistics_32;
            this.recordHistoryExtInfToolStripMenuItem.Name = "recordHistoryExtInfToolStripMenuItem";
            this.recordHistoryExtInfToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.recordHistoryExtInfToolStripMenuItem.Text = "Record History";
            this.recordHistoryExtInfToolStripMenuItem.Click += new System.EventHandler(this.recordHistoryExtInfToolStripMenuItem_Click);
            // 
            // panel23
            // 
            this.panel23.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel23.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel23.Controls.Add(this.glsLabel21);
            this.panel23.Location = new System.Drawing.Point(683, 28);
            this.panel23.Name = "panel23";
            this.panel23.Size = new System.Drawing.Size(331, 39);
            this.panel23.TabIndex = 86;
            // 
            // glsLabel21
            // 
            this.glsLabel21.BottomFill = System.Drawing.Color.Silver;
            this.glsLabel21.Caption = "Extra Information Field Labels";
            this.glsLabel21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glsLabel21.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.glsLabel21.ForeColor = System.Drawing.Color.White;
            this.glsLabel21.Location = new System.Drawing.Point(0, 0);
            this.glsLabel21.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.glsLabel21.Name = "glsLabel21";
            this.glsLabel21.Size = new System.Drawing.Size(327, 35);
            this.glsLabel21.TabIndex = 1;
            this.glsLabel21.TopFill = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            // 
            // extInfSubGroupsListView
            // 
            this.extInfSubGroupsListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.extInfSubGroupsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader46,
            this.columnHeader47,
            this.columnHeader48,
            this.columnHeader55,
            this.columnHeader57,
            this.columnHeader56});
            this.extInfSubGroupsListView.ContextMenuStrip = this.subGroupsContextMenuStrip;
            this.extInfSubGroupsListView.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.extInfSubGroupsListView.FullRowSelect = true;
            this.extInfSubGroupsListView.Location = new System.Drawing.Point(270, 71);
            this.extInfSubGroupsListView.MinimumSize = new System.Drawing.Size(327, 300);
            this.extInfSubGroupsListView.Name = "extInfSubGroupsListView";
            this.extInfSubGroupsListView.ShowItemToolTips = true;
            this.extInfSubGroupsListView.Size = new System.Drawing.Size(408, 575);
            this.extInfSubGroupsListView.TabIndex = 1;
            this.extInfSubGroupsListView.UseCompatibleStateImageBehavior = false;
            this.extInfSubGroupsListView.View = System.Windows.Forms.View.Details;
            this.extInfSubGroupsListView.SelectedIndexChanged += new System.EventHandler(this.extInfSubGroupsListView_SelectedIndexChanged);
            this.extInfSubGroupsListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.extInfSubGroupsListView_KeyDown);
            // 
            // columnHeader46
            // 
            this.columnHeader46.Text = "No.";
            this.columnHeader46.Width = 45;
            // 
            // columnHeader47
            // 
            this.columnHeader47.Text = "Subgroup Name";
            this.columnHeader47.Width = 140;
            // 
            // columnHeader48
            // 
            this.columnHeader48.Text = "Main Table Name";
            this.columnHeader48.Width = 100;
            // 
            // columnHeader55
            // 
            this.columnHeader55.Text = "Key Column";
            this.columnHeader55.Width = 80;
            // 
            // columnHeader57
            // 
            this.columnHeader57.Text = "Date Added";
            // 
            // columnHeader56
            // 
            this.columnHeader56.Text = "table_id";
            this.columnHeader56.Width = 0;
            // 
            // subGroupsContextMenuStrip
            // 
            this.subGroupsContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exptSubGrpMenuItem,
            this.refreshSubGrpsMenuItem,
            this.toolStripSeparator85,
            this.vwSQLSubGrpsMenuItem});
            this.subGroupsContextMenuStrip.Name = "usersContextMenuStrip";
            this.subGroupsContextMenuStrip.Size = new System.Drawing.Size(151, 76);
            // 
            // exptSubGrpMenuItem
            // 
            this.exptSubGrpMenuItem.Image = global::SystemAdministration.Properties.Resources.image007;
            this.exptSubGrpMenuItem.Name = "exptSubGrpMenuItem";
            this.exptSubGrpMenuItem.Size = new System.Drawing.Size(150, 22);
            this.exptSubGrpMenuItem.Text = "Export to Excel";
            this.exptSubGrpMenuItem.Click += new System.EventHandler(this.exptSubGrpMenuItem_Click);
            // 
            // refreshSubGrpsMenuItem
            // 
            this.refreshSubGrpsMenuItem.Image = global::SystemAdministration.Properties.Resources.refresh;
            this.refreshSubGrpsMenuItem.Name = "refreshSubGrpsMenuItem";
            this.refreshSubGrpsMenuItem.Size = new System.Drawing.Size(150, 22);
            this.refreshSubGrpsMenuItem.Text = "&Refresh";
            this.refreshSubGrpsMenuItem.Click += new System.EventHandler(this.refreshSubGrpsMenuItem_Click);
            // 
            // toolStripSeparator85
            // 
            this.toolStripSeparator85.Name = "toolStripSeparator85";
            this.toolStripSeparator85.Size = new System.Drawing.Size(147, 6);
            // 
            // vwSQLSubGrpsMenuItem
            // 
            this.vwSQLSubGrpsMenuItem.Image = global::SystemAdministration.Properties.Resources.SQL;
            this.vwSQLSubGrpsMenuItem.Name = "vwSQLSubGrpsMenuItem";
            this.vwSQLSubGrpsMenuItem.Size = new System.Drawing.Size(150, 22);
            this.vwSQLSubGrpsMenuItem.Text = "&View SQL";
            this.vwSQLSubGrpsMenuItem.Click += new System.EventHandler(this.vwSQLSubGrpsMenuItem_Click);
            // 
            // panel20
            // 
            this.panel20.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel20.Controls.Add(this.glsLabel18);
            this.panel20.Location = new System.Drawing.Point(7, 28);
            this.panel20.Name = "panel20";
            this.panel20.Size = new System.Drawing.Size(257, 39);
            this.panel20.TabIndex = 81;
            // 
            // glsLabel18
            // 
            this.glsLabel18.BottomFill = System.Drawing.Color.Silver;
            this.glsLabel18.Caption = "Modules";
            this.glsLabel18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glsLabel18.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.glsLabel18.ForeColor = System.Drawing.Color.White;
            this.glsLabel18.Location = new System.Drawing.Point(0, 0);
            this.glsLabel18.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.glsLabel18.Name = "glsLabel18";
            this.glsLabel18.Size = new System.Drawing.Size(253, 35);
            this.glsLabel18.TabIndex = 1;
            this.glsLabel18.TopFill = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            // 
            // extInfoModuleListView
            // 
            this.extInfoModuleListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.extInfoModuleListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader49,
            this.columnHeader50,
            this.columnHeader54});
            this.extInfoModuleListView.ContextMenuStrip = this.extInfMdlContextMenuStrip;
            this.extInfoModuleListView.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.extInfoModuleListView.FullRowSelect = true;
            this.extInfoModuleListView.GridLines = true;
            this.extInfoModuleListView.HideSelection = false;
            this.extInfoModuleListView.Location = new System.Drawing.Point(7, 72);
            this.extInfoModuleListView.MinimumSize = new System.Drawing.Size(257, 300);
            this.extInfoModuleListView.MultiSelect = false;
            this.extInfoModuleListView.Name = "extInfoModuleListView";
            this.extInfoModuleListView.ShowItemToolTips = true;
            this.extInfoModuleListView.Size = new System.Drawing.Size(258, 573);
            this.extInfoModuleListView.TabIndex = 0;
            this.extInfoModuleListView.UseCompatibleStateImageBehavior = false;
            this.extInfoModuleListView.View = System.Windows.Forms.View.Details;
            this.extInfoModuleListView.SelectedIndexChanged += new System.EventHandler(this.extInfoModuleListView_SelectedIndexChanged);
            this.extInfoModuleListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.extInfoModuleListView_KeyDown);
            // 
            // columnHeader49
            // 
            this.columnHeader49.Text = "No.";
            this.columnHeader49.Width = 45;
            // 
            // columnHeader50
            // 
            this.columnHeader50.Text = "Module Name";
            this.columnHeader50.Width = 190;
            // 
            // columnHeader54
            // 
            this.columnHeader54.Text = "ModuleID";
            this.columnHeader54.Width = 0;
            // 
            // extInfMdlContextMenuStrip
            // 
            this.extInfMdlContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exptExtInfMenuItem,
            this.refreshExtInfMdlMenuItem,
            this.toolStripSeparator78,
            this.viewSQLExtInfMdlMenuItem});
            this.extInfMdlContextMenuStrip.Name = "usersContextMenuStrip";
            this.extInfMdlContextMenuStrip.Size = new System.Drawing.Size(151, 76);
            // 
            // exptExtInfMenuItem
            // 
            this.exptExtInfMenuItem.Image = global::SystemAdministration.Properties.Resources.image007;
            this.exptExtInfMenuItem.Name = "exptExtInfMenuItem";
            this.exptExtInfMenuItem.Size = new System.Drawing.Size(150, 22);
            this.exptExtInfMenuItem.Text = "Export to Excel";
            this.exptExtInfMenuItem.Click += new System.EventHandler(this.exptExtInfMenuItem_Click);
            // 
            // refreshExtInfMdlMenuItem
            // 
            this.refreshExtInfMdlMenuItem.Image = global::SystemAdministration.Properties.Resources.refresh;
            this.refreshExtInfMdlMenuItem.Name = "refreshExtInfMdlMenuItem";
            this.refreshExtInfMdlMenuItem.Size = new System.Drawing.Size(150, 22);
            this.refreshExtInfMdlMenuItem.Text = "&Refresh";
            this.refreshExtInfMdlMenuItem.Click += new System.EventHandler(this.refreshExtInfMdlMenuItem_Click);
            // 
            // toolStripSeparator78
            // 
            this.toolStripSeparator78.Name = "toolStripSeparator78";
            this.toolStripSeparator78.Size = new System.Drawing.Size(147, 6);
            // 
            // viewSQLExtInfMdlMenuItem
            // 
            this.viewSQLExtInfMdlMenuItem.Image = global::SystemAdministration.Properties.Resources.SQL;
            this.viewSQLExtInfMdlMenuItem.Name = "viewSQLExtInfMdlMenuItem";
            this.viewSQLExtInfMdlMenuItem.Size = new System.Drawing.Size(150, 22);
            this.viewSQLExtInfMdlMenuItem.Text = "&View SQL";
            this.viewSQLExtInfMdlMenuItem.Click += new System.EventHandler(this.viewSQLExtInfMdlMenuItem_Click);
            // 
            // panel21
            // 
            this.panel21.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel21.Controls.Add(this.glsLabel19);
            this.panel21.Location = new System.Drawing.Point(270, 28);
            this.panel21.Name = "panel21";
            this.panel21.Size = new System.Drawing.Size(407, 39);
            this.panel21.TabIndex = 82;
            // 
            // glsLabel19
            // 
            this.glsLabel19.BottomFill = System.Drawing.Color.Silver;
            this.glsLabel19.Caption = "Corresponding Sub-groups";
            this.glsLabel19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glsLabel19.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.glsLabel19.ForeColor = System.Drawing.Color.White;
            this.glsLabel19.Location = new System.Drawing.Point(0, 0);
            this.glsLabel19.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.glsLabel19.Name = "glsLabel19";
            this.glsLabel19.Size = new System.Drawing.Size(403, 35);
            this.glsLabel19.TabIndex = 1;
            this.glsLabel19.TopFill = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            // 
            // toolStrip7
            // 
            this.toolStrip7.AutoSize = false;
            this.toolStrip7.BackColor = System.Drawing.Color.WhiteSmoke;
            this.toolStrip7.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moveFirstExtInfButton,
            this.toolStripSeparator33,
            this.movePreviousExtInfButton,
            this.toolStripSeparator36,
            this.toolStripLabel14,
            this.positionExtInfTextBox,
            this.totalRecExtInfLabel,
            this.toolStripSeparator46,
            this.moveNextExtInfButton,
            this.toolStripSeparator47,
            this.moveLastExtInfButton,
            this.toolStripSeparator48,
            this.dsplySizeExtInfComboBox,
            this.toolStripSeparator67,
            this.toolStripLabel22,
            this.toolStripSeparator69,
            this.searchForExtInfTextBox,
            this.toolStripSeparator74,
            this.toolStripLabel25,
            this.toolStripSeparator75,
            this.searchInExtInfComboBox,
            this.toolStripSeparator76,
            this.refreshExtInfoButton});
            this.toolStrip7.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip7.Location = new System.Drawing.Point(0, 0);
            this.toolStrip7.Name = "toolStrip7";
            this.toolStrip7.Size = new System.Drawing.Size(1019, 25);
            this.toolStrip7.Stretch = true;
            this.toolStrip7.TabIndex = 0;
            this.toolStrip7.TabStop = true;
            this.toolStrip7.Text = "ToolStrip2";
            // 
            // moveFirstExtInfButton
            // 
            this.moveFirstExtInfButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveFirstExtInfButton.Image = global::SystemAdministration.Properties.Resources.DataContainer_MoveFirstHS;
            this.moveFirstExtInfButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveFirstExtInfButton.Name = "moveFirstExtInfButton";
            this.moveFirstExtInfButton.Size = new System.Drawing.Size(23, 22);
            this.moveFirstExtInfButton.Text = "Move First";
            this.moveFirstExtInfButton.Click += new System.EventHandler(this.extInfPnlNavButtons);
            // 
            // toolStripSeparator33
            // 
            this.toolStripSeparator33.Name = "toolStripSeparator33";
            this.toolStripSeparator33.Size = new System.Drawing.Size(6, 25);
            // 
            // movePreviousExtInfButton
            // 
            this.movePreviousExtInfButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.movePreviousExtInfButton.Image = global::SystemAdministration.Properties.Resources.DataContainer_MovePreviousHS;
            this.movePreviousExtInfButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.movePreviousExtInfButton.Name = "movePreviousExtInfButton";
            this.movePreviousExtInfButton.Size = new System.Drawing.Size(23, 22);
            this.movePreviousExtInfButton.Text = "Move Previous";
            this.movePreviousExtInfButton.Click += new System.EventHandler(this.extInfPnlNavButtons);
            // 
            // toolStripSeparator36
            // 
            this.toolStripSeparator36.Name = "toolStripSeparator36";
            this.toolStripSeparator36.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel14
            // 
            this.toolStripLabel14.AutoToolTip = true;
            this.toolStripLabel14.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel14.Name = "toolStripLabel14";
            this.toolStripLabel14.Size = new System.Drawing.Size(47, 22);
            this.toolStripLabel14.Text = "Record";
            // 
            // positionExtInfTextBox
            // 
            this.positionExtInfTextBox.AutoToolTip = true;
            this.positionExtInfTextBox.BackColor = System.Drawing.Color.White;
            this.positionExtInfTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.positionExtInfTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.positionExtInfTextBox.Name = "positionExtInfTextBox";
            this.positionExtInfTextBox.ReadOnly = true;
            this.positionExtInfTextBox.Size = new System.Drawing.Size(70, 25);
            this.positionExtInfTextBox.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.positionExtInfTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.positionExtInfTextBox_KeyDown);
            // 
            // totalRecExtInfLabel
            // 
            this.totalRecExtInfLabel.AutoToolTip = true;
            this.totalRecExtInfLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalRecExtInfLabel.Name = "totalRecExtInfLabel";
            this.totalRecExtInfLabel.Size = new System.Drawing.Size(50, 22);
            this.totalRecExtInfLabel.Text = "of Total";
            // 
            // toolStripSeparator46
            // 
            this.toolStripSeparator46.Name = "toolStripSeparator46";
            this.toolStripSeparator46.Size = new System.Drawing.Size(6, 25);
            // 
            // moveNextExtInfButton
            // 
            this.moveNextExtInfButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveNextExtInfButton.Image = global::SystemAdministration.Properties.Resources.DataContainer_MoveNextHS;
            this.moveNextExtInfButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveNextExtInfButton.Name = "moveNextExtInfButton";
            this.moveNextExtInfButton.Size = new System.Drawing.Size(23, 22);
            this.moveNextExtInfButton.Text = "Move Next";
            this.moveNextExtInfButton.Click += new System.EventHandler(this.extInfPnlNavButtons);
            // 
            // toolStripSeparator47
            // 
            this.toolStripSeparator47.Name = "toolStripSeparator47";
            this.toolStripSeparator47.Size = new System.Drawing.Size(6, 25);
            // 
            // moveLastExtInfButton
            // 
            this.moveLastExtInfButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveLastExtInfButton.Image = global::SystemAdministration.Properties.Resources.DataContainer_MoveLastHS;
            this.moveLastExtInfButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveLastExtInfButton.Name = "moveLastExtInfButton";
            this.moveLastExtInfButton.Size = new System.Drawing.Size(23, 22);
            this.moveLastExtInfButton.Text = "Move Last";
            this.moveLastExtInfButton.Click += new System.EventHandler(this.extInfPnlNavButtons);
            // 
            // toolStripSeparator48
            // 
            this.toolStripSeparator48.Name = "toolStripSeparator48";
            this.toolStripSeparator48.Size = new System.Drawing.Size(6, 25);
            // 
            // dsplySizeExtInfComboBox
            // 
            this.dsplySizeExtInfComboBox.AutoSize = false;
            this.dsplySizeExtInfComboBox.Items.AddRange(new object[] {
            "1",
            "5",
            "10",
            "15",
            "20",
            "30",
            "40",
            "50",
            "100"});
            this.dsplySizeExtInfComboBox.Name = "dsplySizeExtInfComboBox";
            this.dsplySizeExtInfComboBox.Size = new System.Drawing.Size(40, 23);
            this.dsplySizeExtInfComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForExtInfTextBox_KeyDown);
            // 
            // toolStripSeparator67
            // 
            this.toolStripSeparator67.Name = "toolStripSeparator67";
            this.toolStripSeparator67.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel22
            // 
            this.toolStripLabel22.Name = "toolStripLabel22";
            this.toolStripLabel22.Size = new System.Drawing.Size(65, 22);
            this.toolStripLabel22.Text = "Search For:";
            // 
            // toolStripSeparator69
            // 
            this.toolStripSeparator69.Name = "toolStripSeparator69";
            this.toolStripSeparator69.Size = new System.Drawing.Size(6, 25);
            // 
            // searchForExtInfTextBox
            // 
            this.searchForExtInfTextBox.Name = "searchForExtInfTextBox";
            this.searchForExtInfTextBox.Size = new System.Drawing.Size(100, 25);
            this.searchForExtInfTextBox.Enter += new System.EventHandler(this.searchForExtInfTextBox_Click);
            this.searchForExtInfTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForExtInfTextBox_KeyDown);
            this.searchForExtInfTextBox.Click += new System.EventHandler(this.searchForExtInfTextBox_Click);
            // 
            // toolStripSeparator74
            // 
            this.toolStripSeparator74.Name = "toolStripSeparator74";
            this.toolStripSeparator74.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel25
            // 
            this.toolStripLabel25.Name = "toolStripLabel25";
            this.toolStripLabel25.Size = new System.Drawing.Size(58, 22);
            this.toolStripLabel25.Text = "Search In:";
            // 
            // toolStripSeparator75
            // 
            this.toolStripSeparator75.Name = "toolStripSeparator75";
            this.toolStripSeparator75.Size = new System.Drawing.Size(6, 25);
            // 
            // searchInExtInfComboBox
            // 
            this.searchInExtInfComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.searchInExtInfComboBox.Items.AddRange(new object[] {
            "Module Name",
            "Subgroup Name"});
            this.searchInExtInfComboBox.Name = "searchInExtInfComboBox";
            this.searchInExtInfComboBox.Size = new System.Drawing.Size(121, 25);
            this.searchInExtInfComboBox.Sorted = true;
            this.searchInExtInfComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForExtInfTextBox_KeyDown);
            // 
            // toolStripSeparator76
            // 
            this.toolStripSeparator76.Name = "toolStripSeparator76";
            this.toolStripSeparator76.Size = new System.Drawing.Size(6, 25);
            // 
            // refreshExtInfoButton
            // 
            this.refreshExtInfoButton.Image = global::SystemAdministration.Properties.Resources.action_go;
            this.refreshExtInfoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshExtInfoButton.Name = "refreshExtInfoButton";
            this.refreshExtInfoButton.Size = new System.Drawing.Size(42, 22);
            this.refreshExtInfoButton.Text = "Go";
            this.refreshExtInfoButton.Click += new System.EventHandler(this.refreshExtInfoButton_Click);
            // 
            // label36
            // 
            this.label36.Location = new System.Drawing.Point(141, 382);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(268, 23);
            this.label36.TabIndex = 85;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.policyPanel);
            this.tabPage5.ImageKey = "SecurityLock.png";
            this.tabPage5.Location = new System.Drawing.Point(4, 60);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(1025, 655);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "SECURITY POLICIES";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // policyPanel
            // 
            this.policyPanel.AutoScroll = true;
            this.policyPanel.BackColor = System.Drawing.Color.LightSlateGray;
            this.policyPanel.Controls.Add(this.auditTblsListView);
            this.policyPanel.Controls.Add(this.label26);
            this.policyPanel.Controls.Add(this.groupBox7);
            this.policyPanel.Controls.Add(this.groupBox6);
            this.policyPanel.Controls.Add(this.groupBox5);
            this.policyPanel.Controls.Add(this.groupBox4);
            this.policyPanel.Controls.Add(this.toolStrip3);
            this.policyPanel.Controls.Add(this.label27);
            this.policyPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.policyPanel.Enabled = false;
            this.policyPanel.Location = new System.Drawing.Point(3, 3);
            this.policyPanel.Name = "policyPanel";
            this.policyPanel.Size = new System.Drawing.Size(1019, 649);
            this.policyPanel.TabIndex = 1;
            this.policyPanel.Visible = false;
            // 
            // auditTblsListView
            // 
            this.auditTblsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.auditTblsListView.CheckBoxes = true;
            this.auditTblsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader32,
            this.columnHeader33,
            this.columnHeader34,
            this.columnHeader35,
            this.columnHeader36,
            this.columnHeader37});
            this.auditTblsListView.ContextMenuStrip = this.plcyMdlsContextMenuStrip;
            this.auditTblsListView.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.auditTblsListView.FullRowSelect = true;
            this.auditTblsListView.GridLines = true;
            this.auditTblsListView.Location = new System.Drawing.Point(4, 331);
            this.auditTblsListView.MinimumSize = new System.Drawing.Size(638, 100);
            this.auditTblsListView.Name = "auditTblsListView";
            this.auditTblsListView.ShowItemToolTips = true;
            this.auditTblsListView.Size = new System.Drawing.Size(1011, 315);
            this.auditTblsListView.TabIndex = 4;
            this.auditTblsListView.UseCompatibleStateImageBehavior = false;
            this.auditTblsListView.View = System.Windows.Forms.View.Details;
            this.auditTblsListView.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.auditTblsListView_ItemChecked);
            // 
            // columnHeader32
            // 
            this.columnHeader32.Text = "No.";
            this.columnHeader32.Width = 45;
            // 
            // columnHeader33
            // 
            this.columnHeader33.Text = "Module Name";
            this.columnHeader33.Width = 160;
            // 
            // columnHeader34
            // 
            this.columnHeader34.Text = "Audit Trail Table";
            this.columnHeader34.Width = 180;
            // 
            // columnHeader35
            // 
            this.columnHeader35.Text = "Enable Tracking?";
            this.columnHeader35.Width = 100;
            // 
            // columnHeader36
            // 
            this.columnHeader36.Text = "Actions to Track";
            this.columnHeader36.Width = 600;
            // 
            // columnHeader37
            // 
            this.columnHeader37.Text = "MDL_ID";
            this.columnHeader37.Width = 0;
            // 
            // plcyMdlsContextMenuStrip
            // 
            this.plcyMdlsContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editPlcyMdlMenuItem,
            this.toolStripSeparator61,
            this.exptPlcyMenuItem,
            this.refreshPlcyMdlsMenuItem,
            this.recHstryPlcyMdlsMenuItem,
            this.vwSQLPlcyMdlsMenuItem});
            this.plcyMdlsContextMenuStrip.Name = "userRolesContextMenuStrip";
            this.plcyMdlsContextMenuStrip.Size = new System.Drawing.Size(153, 120);
            // 
            // editPlcyMdlMenuItem
            // 
            this.editPlcyMdlMenuItem.Image = global::SystemAdministration.Properties.Resources.plus_32;
            this.editPlcyMdlMenuItem.Name = "editPlcyMdlMenuItem";
            this.editPlcyMdlMenuItem.Size = new System.Drawing.Size(152, 22);
            this.editPlcyMdlMenuItem.Text = "Edit Settings";
            this.editPlcyMdlMenuItem.Click += new System.EventHandler(this.editPlcyMdlMenuItem_Click);
            // 
            // toolStripSeparator61
            // 
            this.toolStripSeparator61.Name = "toolStripSeparator61";
            this.toolStripSeparator61.Size = new System.Drawing.Size(149, 6);
            // 
            // exptPlcyMenuItem
            // 
            this.exptPlcyMenuItem.Image = global::SystemAdministration.Properties.Resources.image007;
            this.exptPlcyMenuItem.Name = "exptPlcyMenuItem";
            this.exptPlcyMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exptPlcyMenuItem.Text = "Export to Excel";
            this.exptPlcyMenuItem.Click += new System.EventHandler(this.exptPlcyMenuItem_Click);
            // 
            // refreshPlcyMdlsMenuItem
            // 
            this.refreshPlcyMdlsMenuItem.Image = global::SystemAdministration.Properties.Resources.refresh;
            this.refreshPlcyMdlsMenuItem.Name = "refreshPlcyMdlsMenuItem";
            this.refreshPlcyMdlsMenuItem.Size = new System.Drawing.Size(152, 22);
            this.refreshPlcyMdlsMenuItem.Text = "&Refresh";
            this.refreshPlcyMdlsMenuItem.Click += new System.EventHandler(this.refreshPlcyMdlsMenuItem_Click);
            // 
            // recHstryPlcyMdlsMenuItem
            // 
            this.recHstryPlcyMdlsMenuItem.Image = global::SystemAdministration.Properties.Resources.statistics_32;
            this.recHstryPlcyMdlsMenuItem.Name = "recHstryPlcyMdlsMenuItem";
            this.recHstryPlcyMdlsMenuItem.Size = new System.Drawing.Size(152, 22);
            this.recHstryPlcyMdlsMenuItem.Text = "Record &History";
            this.recHstryPlcyMdlsMenuItem.Click += new System.EventHandler(this.recHstryPlcyMdlsMenuItem_Click);
            // 
            // vwSQLPlcyMdlsMenuItem
            // 
            this.vwSQLPlcyMdlsMenuItem.Image = global::SystemAdministration.Properties.Resources.SQL;
            this.vwSQLPlcyMdlsMenuItem.Name = "vwSQLPlcyMdlsMenuItem";
            this.vwSQLPlcyMdlsMenuItem.Size = new System.Drawing.Size(152, 22);
            this.vwSQLPlcyMdlsMenuItem.Text = "&View SQL";
            this.vwSQLPlcyMdlsMenuItem.Click += new System.EventHandler(this.vwSQLPlcyMdlsMenuItem_Click);
            // 
            // label26
            // 
            this.label26.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label26.BackColor = System.Drawing.Color.Black;
            this.label26.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.Gold;
            this.label26.Location = new System.Drawing.Point(4, 305);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(1011, 23);
            this.label26.TabIndex = 83;
            this.label26.Text = "AUDIT TRAIL TABLES TO ENABLE";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.sessionNumUpDown);
            this.groupBox7.Controls.Add(this.label7);
            this.groupBox7.Controls.Add(this.wildNoCheckBox);
            this.groupBox7.Controls.Add(this.wildYesCheckBox);
            this.groupBox7.Controls.Add(this.digitsNoCheckBox);
            this.groupBox7.Controls.Add(this.digitsYesCheckBox);
            this.groupBox7.Controls.Add(this.smallNoCheckBox);
            this.groupBox7.Controls.Add(this.smallYesCheckBox);
            this.groupBox7.Controls.Add(this.capsNoCheckBox);
            this.groupBox7.Controls.Add(this.capsYesCheckBox);
            this.groupBox7.Controls.Add(this.label23);
            this.groupBox7.Controls.Add(this.combinatnsComboBox);
            this.groupBox7.Controls.Add(this.label22);
            this.groupBox7.Controls.Add(this.label20);
            this.groupBox7.Controls.Add(this.label16);
            this.groupBox7.Controls.Add(this.label21);
            this.groupBox7.Location = new System.Drawing.Point(324, 94);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(317, 208);
            this.groupBox7.TabIndex = 3;
            this.groupBox7.TabStop = false;
            // 
            // sessionNumUpDown
            // 
            this.sessionNumUpDown.Location = new System.Drawing.Point(201, 178);
            this.sessionNumUpDown.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.sessionNumUpDown.Name = "sessionNumUpDown";
            this.sessionNumUpDown.Size = new System.Drawing.Size(93, 21);
            this.sessionNumUpDown.TabIndex = 9;
            this.sessionNumUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(7, 182);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(136, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Session Timeout(Seconds):";
            // 
            // wildNoCheckBox
            // 
            this.wildNoCheckBox.AutoSize = true;
            this.wildNoCheckBox.ForeColor = System.Drawing.Color.White;
            this.wildNoCheckBox.Location = new System.Drawing.Point(258, 127);
            this.wildNoCheckBox.Name = "wildNoCheckBox";
            this.wildNoCheckBox.Size = new System.Drawing.Size(39, 17);
            this.wildNoCheckBox.TabIndex = 7;
            this.wildNoCheckBox.Text = "No";
            this.wildNoCheckBox.UseVisualStyleBackColor = true;
            this.wildNoCheckBox.CheckedChanged += new System.EventHandler(this.wildNoCheckBox_CheckedChanged);
            // 
            // wildYesCheckBox
            // 
            this.wildYesCheckBox.AutoSize = true;
            this.wildYesCheckBox.ForeColor = System.Drawing.Color.White;
            this.wildYesCheckBox.Location = new System.Drawing.Point(209, 127);
            this.wildYesCheckBox.Name = "wildYesCheckBox";
            this.wildYesCheckBox.Size = new System.Drawing.Size(43, 17);
            this.wildYesCheckBox.TabIndex = 6;
            this.wildYesCheckBox.Text = "Yes";
            this.wildYesCheckBox.UseVisualStyleBackColor = true;
            this.wildYesCheckBox.CheckedChanged += new System.EventHandler(this.wildYesCheckBox_CheckedChanged);
            // 
            // digitsNoCheckBox
            // 
            this.digitsNoCheckBox.AutoSize = true;
            this.digitsNoCheckBox.ForeColor = System.Drawing.Color.White;
            this.digitsNoCheckBox.Location = new System.Drawing.Point(258, 90);
            this.digitsNoCheckBox.Name = "digitsNoCheckBox";
            this.digitsNoCheckBox.Size = new System.Drawing.Size(39, 17);
            this.digitsNoCheckBox.TabIndex = 5;
            this.digitsNoCheckBox.Text = "No";
            this.digitsNoCheckBox.UseVisualStyleBackColor = true;
            this.digitsNoCheckBox.CheckedChanged += new System.EventHandler(this.digitsNoCheckBox_CheckedChanged);
            // 
            // digitsYesCheckBox
            // 
            this.digitsYesCheckBox.AutoSize = true;
            this.digitsYesCheckBox.ForeColor = System.Drawing.Color.White;
            this.digitsYesCheckBox.Location = new System.Drawing.Point(209, 90);
            this.digitsYesCheckBox.Name = "digitsYesCheckBox";
            this.digitsYesCheckBox.Size = new System.Drawing.Size(43, 17);
            this.digitsYesCheckBox.TabIndex = 4;
            this.digitsYesCheckBox.Text = "Yes";
            this.digitsYesCheckBox.UseVisualStyleBackColor = true;
            this.digitsYesCheckBox.CheckedChanged += new System.EventHandler(this.digitsYesCheckBox_CheckedChanged);
            // 
            // smallNoCheckBox
            // 
            this.smallNoCheckBox.AutoSize = true;
            this.smallNoCheckBox.ForeColor = System.Drawing.Color.White;
            this.smallNoCheckBox.Location = new System.Drawing.Point(258, 55);
            this.smallNoCheckBox.Name = "smallNoCheckBox";
            this.smallNoCheckBox.Size = new System.Drawing.Size(39, 17);
            this.smallNoCheckBox.TabIndex = 3;
            this.smallNoCheckBox.Text = "No";
            this.smallNoCheckBox.UseVisualStyleBackColor = true;
            this.smallNoCheckBox.CheckedChanged += new System.EventHandler(this.smallNoCheckBox_CheckedChanged);
            // 
            // smallYesCheckBox
            // 
            this.smallYesCheckBox.AutoSize = true;
            this.smallYesCheckBox.ForeColor = System.Drawing.Color.White;
            this.smallYesCheckBox.Location = new System.Drawing.Point(209, 55);
            this.smallYesCheckBox.Name = "smallYesCheckBox";
            this.smallYesCheckBox.Size = new System.Drawing.Size(43, 17);
            this.smallYesCheckBox.TabIndex = 2;
            this.smallYesCheckBox.Text = "Yes";
            this.smallYesCheckBox.UseVisualStyleBackColor = true;
            this.smallYesCheckBox.CheckedChanged += new System.EventHandler(this.smallYesCheckBox_CheckedChanged);
            // 
            // capsNoCheckBox
            // 
            this.capsNoCheckBox.AutoSize = true;
            this.capsNoCheckBox.ForeColor = System.Drawing.Color.White;
            this.capsNoCheckBox.Location = new System.Drawing.Point(258, 18);
            this.capsNoCheckBox.Name = "capsNoCheckBox";
            this.capsNoCheckBox.Size = new System.Drawing.Size(39, 17);
            this.capsNoCheckBox.TabIndex = 1;
            this.capsNoCheckBox.Text = "No";
            this.capsNoCheckBox.UseVisualStyleBackColor = true;
            this.capsNoCheckBox.CheckedChanged += new System.EventHandler(this.capsNoCheckBox_CheckedChanged);
            // 
            // capsYesCheckBox
            // 
            this.capsYesCheckBox.AutoSize = true;
            this.capsYesCheckBox.ForeColor = System.Drawing.Color.White;
            this.capsYesCheckBox.Location = new System.Drawing.Point(209, 18);
            this.capsYesCheckBox.Name = "capsYesCheckBox";
            this.capsYesCheckBox.Size = new System.Drawing.Size(43, 17);
            this.capsYesCheckBox.TabIndex = 0;
            this.capsYesCheckBox.Text = "Yes";
            this.capsYesCheckBox.UseVisualStyleBackColor = true;
            this.capsYesCheckBox.CheckedChanged += new System.EventHandler(this.capsYesCheckBox_CheckedChanged);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.ForeColor = System.Drawing.Color.White;
            this.label23.Location = new System.Drawing.Point(6, 153);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(195, 13);
            this.label23.TabIndex = 19;
            this.label23.Text = "Combinations of the above to insist on:";
            // 
            // combinatnsComboBox
            // 
            this.combinatnsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combinatnsComboBox.FormattingEnabled = true;
            this.combinatnsComboBox.Items.AddRange(new object[] {
            "NONE",
            "ALL 4",
            "ANY 3",
            "ANY 2",
            "ANY 1"});
            this.combinatnsComboBox.Location = new System.Drawing.Point(201, 149);
            this.combinatnsComboBox.Name = "combinatnsComboBox";
            this.combinatnsComboBox.Size = new System.Drawing.Size(93, 21);
            this.combinatnsComboBox.TabIndex = 8;
            this.combinatnsComboBox.SelectedIndexChanged += new System.EventHandler(this.combinatnsComboBox_SelectedIndexChanged);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.ForeColor = System.Drawing.Color.White;
            this.label22.Location = new System.Drawing.Point(7, 129);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(202, 13);
            this.label22.TabIndex = 15;
            this.label22.Text = "Check for Wild Characters in passwords?";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.ForeColor = System.Drawing.Color.White;
            this.label20.Location = new System.Drawing.Point(7, 92);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(152, 13);
            this.label20.TabIndex = 12;
            this.label20.Text = "Check for Digits in passwords?";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(7, 57);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(187, 13);
            this.label16.TabIndex = 9;
            this.label16.Text = "Check for Small Letters in passwords?";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.ForeColor = System.Drawing.Color.White;
            this.label21.Location = new System.Drawing.Point(7, 20);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(196, 13);
            this.label21.TabIndex = 0;
            this.label21.Text = "Check for Capital Letters in passwords?";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.allwRptnNoCheckBox);
            this.groupBox6.Controls.Add(this.allwRptnYesCheckBox);
            this.groupBox6.Controls.Add(this.allwUnmNoCheckBox);
            this.groupBox6.Controls.Add(this.allwUnmYesCheckBox);
            this.groupBox6.Controls.Add(this.autoUnlkTmNmUpDown);
            this.groupBox6.Controls.Add(this.label14);
            this.groupBox6.Controls.Add(this.faildLgnCntNmUpDown);
            this.groupBox6.Controls.Add(this.label13);
            this.groupBox6.Controls.Add(this.label25);
            this.groupBox6.Controls.Add(this.maxLenPswdNmUpDown);
            this.groupBox6.Controls.Add(this.label17);
            this.groupBox6.Controls.Add(this.oldPswdCntNmUpDown);
            this.groupBox6.Controls.Add(this.label18);
            this.groupBox6.Controls.Add(this.minLenPswdNumericUpDown);
            this.groupBox6.Controls.Add(this.label19);
            this.groupBox6.Controls.Add(this.label24);
            this.groupBox6.Location = new System.Drawing.Point(4, 94);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(314, 208);
            this.groupBox6.TabIndex = 2;
            this.groupBox6.TabStop = false;
            // 
            // allwRptnNoCheckBox
            // 
            this.allwRptnNoCheckBox.AutoSize = true;
            this.allwRptnNoCheckBox.ForeColor = System.Drawing.Color.White;
            this.allwRptnNoCheckBox.Location = new System.Drawing.Point(257, 125);
            this.allwRptnNoCheckBox.Name = "allwRptnNoCheckBox";
            this.allwRptnNoCheckBox.Size = new System.Drawing.Size(39, 17);
            this.allwRptnNoCheckBox.TabIndex = 6;
            this.allwRptnNoCheckBox.Text = "No";
            this.allwRptnNoCheckBox.UseVisualStyleBackColor = true;
            this.allwRptnNoCheckBox.CheckedChanged += new System.EventHandler(this.allwRptnNoCheckBox_CheckedChanged);
            // 
            // allwRptnYesCheckBox
            // 
            this.allwRptnYesCheckBox.AutoSize = true;
            this.allwRptnYesCheckBox.ForeColor = System.Drawing.Color.White;
            this.allwRptnYesCheckBox.Location = new System.Drawing.Point(208, 125);
            this.allwRptnYesCheckBox.Name = "allwRptnYesCheckBox";
            this.allwRptnYesCheckBox.Size = new System.Drawing.Size(43, 17);
            this.allwRptnYesCheckBox.TabIndex = 5;
            this.allwRptnYesCheckBox.Text = "Yes";
            this.allwRptnYesCheckBox.UseVisualStyleBackColor = true;
            this.allwRptnYesCheckBox.CheckedChanged += new System.EventHandler(this.allwRptnYesCheckBox_CheckedChanged);
            // 
            // allwUnmNoCheckBox
            // 
            this.allwUnmNoCheckBox.AutoSize = true;
            this.allwUnmNoCheckBox.ForeColor = System.Drawing.Color.White;
            this.allwUnmNoCheckBox.Location = new System.Drawing.Point(257, 99);
            this.allwUnmNoCheckBox.Name = "allwUnmNoCheckBox";
            this.allwUnmNoCheckBox.Size = new System.Drawing.Size(39, 17);
            this.allwUnmNoCheckBox.TabIndex = 4;
            this.allwUnmNoCheckBox.Text = "No";
            this.allwUnmNoCheckBox.UseVisualStyleBackColor = true;
            this.allwUnmNoCheckBox.CheckedChanged += new System.EventHandler(this.allwUnmNoCheckBox_CheckedChanged);
            // 
            // allwUnmYesCheckBox
            // 
            this.allwUnmYesCheckBox.AutoSize = true;
            this.allwUnmYesCheckBox.ForeColor = System.Drawing.Color.White;
            this.allwUnmYesCheckBox.Location = new System.Drawing.Point(208, 99);
            this.allwUnmYesCheckBox.Name = "allwUnmYesCheckBox";
            this.allwUnmYesCheckBox.Size = new System.Drawing.Size(43, 17);
            this.allwUnmYesCheckBox.TabIndex = 3;
            this.allwUnmYesCheckBox.Text = "Yes";
            this.allwUnmYesCheckBox.UseVisualStyleBackColor = true;
            this.allwUnmYesCheckBox.CheckedChanged += new System.EventHandler(this.allwUnmYesCheckBox_CheckedChanged);
            // 
            // autoUnlkTmNmUpDown
            // 
            this.autoUnlkTmNmUpDown.Location = new System.Drawing.Point(208, 179);
            this.autoUnlkTmNmUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.autoUnlkTmNmUpDown.Name = "autoUnlkTmNmUpDown";
            this.autoUnlkTmNmUpDown.Size = new System.Drawing.Size(89, 21);
            this.autoUnlkTmNmUpDown.TabIndex = 8;
            this.autoUnlkTmNmUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.autoUnlkTmNmUpDown.ValueChanged += new System.EventHandler(this.autoUnlkTmNmUpDown_ValueChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(6, 181);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(169, 13);
            this.label14.TabIndex = 20;
            this.label14.Text = "Time (mins) to auto unlock a user:";
            // 
            // faildLgnCntNmUpDown
            // 
            this.faildLgnCntNmUpDown.Location = new System.Drawing.Point(208, 154);
            this.faildLgnCntNmUpDown.Name = "faildLgnCntNmUpDown";
            this.faildLgnCntNmUpDown.Size = new System.Drawing.Size(89, 21);
            this.faildLgnCntNmUpDown.TabIndex = 7;
            this.faildLgnCntNmUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.faildLgnCntNmUpDown.ValueChanged += new System.EventHandler(this.faildLgnCntNmUpDown_ValueChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(6, 158);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(201, 13);
            this.label13.TabIndex = 18;
            this.label13.Text = "Maximum Allowed Failed Login Attempts:";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.ForeColor = System.Drawing.Color.White;
            this.label25.Location = new System.Drawing.Point(7, 101);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(161, 13);
            this.label25.TabIndex = 12;
            this.label25.Text = "Allow User names in passwords?";
            // 
            // maxLenPswdNmUpDown
            // 
            this.maxLenPswdNmUpDown.Location = new System.Drawing.Point(209, 42);
            this.maxLenPswdNmUpDown.Name = "maxLenPswdNmUpDown";
            this.maxLenPswdNmUpDown.Size = new System.Drawing.Size(88, 21);
            this.maxLenPswdNmUpDown.TabIndex = 1;
            this.maxLenPswdNmUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.maxLenPswdNmUpDown.ValueChanged += new System.EventHandler(this.maxLenPswdNmUpDown_ValueChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(7, 46);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(158, 13);
            this.label17.TabIndex = 4;
            this.label17.Text = "Maximum Length of Passwords:";
            // 
            // oldPswdCntNmUpDown
            // 
            this.oldPswdCntNmUpDown.Location = new System.Drawing.Point(209, 67);
            this.oldPswdCntNmUpDown.Name = "oldPswdCntNmUpDown";
            this.oldPswdCntNmUpDown.Size = new System.Drawing.Size(88, 21);
            this.oldPswdCntNmUpDown.TabIndex = 2;
            this.oldPswdCntNmUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.oldPswdCntNmUpDown.ValueChanged += new System.EventHandler(this.oldPswdCntNmUpDown_ValueChanged);
            // 
            // label18
            // 
            this.label18.ForeColor = System.Drawing.Color.White;
            this.label18.Location = new System.Drawing.Point(7, 67);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(199, 31);
            this.label18.TabIndex = 2;
            this.label18.Text = "No. of old passwords (sequentially) to disallow:";
            // 
            // minLenPswdNumericUpDown
            // 
            this.minLenPswdNumericUpDown.Location = new System.Drawing.Point(209, 17);
            this.minLenPswdNumericUpDown.Name = "minLenPswdNumericUpDown";
            this.minLenPswdNumericUpDown.Size = new System.Drawing.Size(88, 21);
            this.minLenPswdNumericUpDown.TabIndex = 0;
            this.minLenPswdNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.minLenPswdNumericUpDown.ValueChanged += new System.EventHandler(this.minLenPswdNumericUpDown_ValueChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(7, 20);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(154, 13);
            this.label19.TabIndex = 0;
            this.label19.Text = "Minimum Length of Passwords:";
            // 
            // label24
            // 
            this.label24.ForeColor = System.Drawing.Color.White;
            this.label24.Location = new System.Drawing.Point(6, 125);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(198, 31);
            this.label24.TabIndex = 15;
            this.label24.Text = "Allow characters repeating sequentially in passwords?";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.mxNoRecsNmUpDown);
            this.groupBox5.Controls.Add(this.label15);
            this.groupBox5.Controls.Add(this.expryDaysNmUpDown);
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Location = new System.Drawing.Point(324, 25);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(317, 69);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            // 
            // mxNoRecsNmUpDown
            // 
            this.mxNoRecsNmUpDown.Location = new System.Drawing.Point(201, 40);
            this.mxNoRecsNmUpDown.Name = "mxNoRecsNmUpDown";
            this.mxNoRecsNmUpDown.Size = new System.Drawing.Size(93, 21);
            this.mxNoRecsNmUpDown.TabIndex = 1;
            this.mxNoRecsNmUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mxNoRecsNmUpDown.ValueChanged += new System.EventHandler(this.mxNoRecsNmUpDown_ValueChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(3, 44);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(199, 13);
            this.label15.TabIndex = 6;
            this.label15.Text = "Max No. Records to display in ListViews:";
            // 
            // expryDaysNmUpDown
            // 
            this.expryDaysNmUpDown.Location = new System.Drawing.Point(201, 15);
            this.expryDaysNmUpDown.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.expryDaysNmUpDown.Name = "expryDaysNmUpDown";
            this.expryDaysNmUpDown.Size = new System.Drawing.Size(93, 21);
            this.expryDaysNmUpDown.TabIndex = 0;
            this.expryDaysNmUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.expryDaysNmUpDown.ValueChanged += new System.EventHandler(this.expryDaysNmUpDown_ValueChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(3, 19);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(117, 13);
            this.label12.TabIndex = 2;
            this.label12.Text = "Password Expiry Days:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.policyNmTextBox);
            this.groupBox4.Controls.Add(this.isDefltNoCheckBox);
            this.groupBox4.Controls.Add(this.isDefltYesCheckBox);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.plcyIDTextBox);
            this.groupBox4.Location = new System.Drawing.Point(4, 25);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(314, 69);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            // 
            // isDefltNoCheckBox
            // 
            this.isDefltNoCheckBox.AutoSize = true;
            this.isDefltNoCheckBox.ForeColor = System.Drawing.Color.White;
            this.isDefltNoCheckBox.Location = new System.Drawing.Point(133, 40);
            this.isDefltNoCheckBox.Name = "isDefltNoCheckBox";
            this.isDefltNoCheckBox.Size = new System.Drawing.Size(39, 17);
            this.isDefltNoCheckBox.TabIndex = 2;
            this.isDefltNoCheckBox.Text = "No";
            this.isDefltNoCheckBox.UseVisualStyleBackColor = true;
            this.isDefltNoCheckBox.CheckedChanged += new System.EventHandler(this.isDefltNoCheckBox_CheckedChanged);
            // 
            // isDefltYesCheckBox
            // 
            this.isDefltYesCheckBox.AutoSize = true;
            this.isDefltYesCheckBox.ForeColor = System.Drawing.Color.White;
            this.isDefltYesCheckBox.Location = new System.Drawing.Point(84, 40);
            this.isDefltYesCheckBox.Name = "isDefltYesCheckBox";
            this.isDefltYesCheckBox.Size = new System.Drawing.Size(43, 17);
            this.isDefltYesCheckBox.TabIndex = 1;
            this.isDefltYesCheckBox.Text = "Yes";
            this.isDefltYesCheckBox.UseVisualStyleBackColor = true;
            this.isDefltYesCheckBox.CheckedChanged += new System.EventHandler(this.isDefltYesCheckBox_CheckedChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(7, 40);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 13);
            this.label11.TabIndex = 4;
            this.label11.Text = "Is Default?";
            // 
            // policyNmTextBox
            // 
            this.policyNmTextBox.BackColor = System.Drawing.Color.Gainsboro;
            this.policyNmTextBox.Location = new System.Drawing.Point(84, 16);
            this.policyNmTextBox.MaxLength = 100;
            this.policyNmTextBox.Name = "policyNmTextBox";
            this.policyNmTextBox.ReadOnly = true;
            this.policyNmTextBox.Size = new System.Drawing.Size(213, 21);
            this.policyNmTextBox.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(7, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Policy Name:";
            // 
            // plcyIDTextBox
            // 
            this.plcyIDTextBox.Location = new System.Drawing.Point(252, 16);
            this.plcyIDTextBox.Name = "plcyIDTextBox";
            this.plcyIDTextBox.ReadOnly = true;
            this.plcyIDTextBox.Size = new System.Drawing.Size(40, 21);
            this.plcyIDTextBox.TabIndex = 0;
            this.plcyIDTextBox.TabStop = false;
            this.plcyIDTextBox.Text = "-1";
            // 
            // toolStrip3
            // 
            this.toolStrip3.AutoSize = false;
            this.toolStrip3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addPlcyButton,
            this.toolStripSeparator59,
            this.editPlcyButton,
            this.toolStripSeparator57,
            this.savePlcyButton,
            this.toolStripSeparator55,
            this.deletePolicyButton,
            this.toolStripSeparator60,
            this.refreshPlcyButton,
            this.toolStripSeparator8,
            this.vwSQLPlcyButton,
            this.toolStripSeparator56,
            this.recHstryPlcyButton,
            this.toolStripSeparator58,
            this.moveFirstPlcyButton,
            this.toolStripSeparator41,
            this.movePreviousPlcyButton,
            this.toolStripSeparator42,
            this.toolStripLabel8,
            this.positionPlcyTextBox,
            this.totalRecPlcyLabel,
            this.toolStripSeparator43,
            this.moveNextPlcyButton,
            this.toolStripSeparator44,
            this.moveLastPlcyButton,
            this.toolStripSeparator45,
            this.toolStripLabel12,
            this.toolStripSeparator49,
            this.searchForPlcyTextBox,
            this.toolStripSeparator50,
            this.toolStripLabel13,
            this.toolStripSeparator51,
            this.searchInPlcyComboBox});
            this.toolStrip3.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip3.Location = new System.Drawing.Point(0, 0);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(1019, 25);
            this.toolStrip3.Stretch = true;
            this.toolStrip3.TabIndex = 0;
            this.toolStrip3.TabStop = true;
            this.toolStrip3.Text = "ToolStrip2";
            // 
            // addPlcyButton
            // 
            this.addPlcyButton.Image = global::SystemAdministration.Properties.Resources.plus_32;
            this.addPlcyButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addPlcyButton.Name = "addPlcyButton";
            this.addPlcyButton.Size = new System.Drawing.Size(51, 22);
            this.addPlcyButton.Text = "ADD";
            this.addPlcyButton.Click += new System.EventHandler(this.addPlcyButton_Click);
            // 
            // toolStripSeparator59
            // 
            this.toolStripSeparator59.Name = "toolStripSeparator59";
            this.toolStripSeparator59.Size = new System.Drawing.Size(6, 25);
            // 
            // editPlcyButton
            // 
            this.editPlcyButton.Image = global::SystemAdministration.Properties.Resources.edit32;
            this.editPlcyButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editPlcyButton.Name = "editPlcyButton";
            this.editPlcyButton.Size = new System.Drawing.Size(51, 22);
            this.editPlcyButton.Text = "EDIT";
            this.editPlcyButton.Click += new System.EventHandler(this.editPlcyButton_Click);
            // 
            // toolStripSeparator57
            // 
            this.toolStripSeparator57.Name = "toolStripSeparator57";
            this.toolStripSeparator57.Size = new System.Drawing.Size(6, 25);
            // 
            // savePlcyButton
            // 
            this.savePlcyButton.Image = global::SystemAdministration.Properties.Resources.FloppyDisk;
            this.savePlcyButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.savePlcyButton.Name = "savePlcyButton";
            this.savePlcyButton.Size = new System.Drawing.Size(53, 22);
            this.savePlcyButton.Text = "SAVE";
            this.savePlcyButton.Click += new System.EventHandler(this.savePlcyButton_Click);
            // 
            // toolStripSeparator55
            // 
            this.toolStripSeparator55.Name = "toolStripSeparator55";
            this.toolStripSeparator55.Size = new System.Drawing.Size(6, 25);
            // 
            // refreshPlcyButton
            // 
            this.refreshPlcyButton.Image = global::SystemAdministration.Properties.Resources.refresh;
            this.refreshPlcyButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshPlcyButton.Name = "refreshPlcyButton";
            this.refreshPlcyButton.Size = new System.Drawing.Size(66, 22);
            this.refreshPlcyButton.Text = "Refresh";
            this.refreshPlcyButton.Click += new System.EventHandler(this.refreshPlcyButton_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // vwSQLPlcyButton
            // 
            this.vwSQLPlcyButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.vwSQLPlcyButton.Image = global::SystemAdministration.Properties.Resources.SQL;
            this.vwSQLPlcyButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.vwSQLPlcyButton.Name = "vwSQLPlcyButton";
            this.vwSQLPlcyButton.Size = new System.Drawing.Size(23, 22);
            this.vwSQLPlcyButton.Text = "View SQL";
            this.vwSQLPlcyButton.Click += new System.EventHandler(this.vwSQLPlcyButton_Click);
            // 
            // toolStripSeparator56
            // 
            this.toolStripSeparator56.Name = "toolStripSeparator56";
            this.toolStripSeparator56.Size = new System.Drawing.Size(6, 25);
            // 
            // recHstryPlcyButton
            // 
            this.recHstryPlcyButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.recHstryPlcyButton.Image = global::SystemAdministration.Properties.Resources.statistics_32;
            this.recHstryPlcyButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.recHstryPlcyButton.Name = "recHstryPlcyButton";
            this.recHstryPlcyButton.Size = new System.Drawing.Size(23, 22);
            this.recHstryPlcyButton.Text = "Record History";
            this.recHstryPlcyButton.Click += new System.EventHandler(this.recHstryPlcyButton_Click);
            // 
            // toolStripSeparator58
            // 
            this.toolStripSeparator58.Name = "toolStripSeparator58";
            this.toolStripSeparator58.Size = new System.Drawing.Size(6, 25);
            // 
            // moveFirstPlcyButton
            // 
            this.moveFirstPlcyButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveFirstPlcyButton.Image = global::SystemAdministration.Properties.Resources.DataContainer_MoveFirstHS;
            this.moveFirstPlcyButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveFirstPlcyButton.Name = "moveFirstPlcyButton";
            this.moveFirstPlcyButton.Size = new System.Drawing.Size(23, 22);
            this.moveFirstPlcyButton.Text = "Move First";
            this.moveFirstPlcyButton.Click += new System.EventHandler(this.plcyPnlNavButtons);
            // 
            // toolStripSeparator41
            // 
            this.toolStripSeparator41.Name = "toolStripSeparator41";
            this.toolStripSeparator41.Size = new System.Drawing.Size(6, 25);
            // 
            // movePreviousPlcyButton
            // 
            this.movePreviousPlcyButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.movePreviousPlcyButton.Image = global::SystemAdministration.Properties.Resources.DataContainer_MovePreviousHS;
            this.movePreviousPlcyButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.movePreviousPlcyButton.Name = "movePreviousPlcyButton";
            this.movePreviousPlcyButton.Size = new System.Drawing.Size(23, 22);
            this.movePreviousPlcyButton.Text = "Move Previous";
            this.movePreviousPlcyButton.Click += new System.EventHandler(this.plcyPnlNavButtons);
            // 
            // toolStripSeparator42
            // 
            this.toolStripSeparator42.Name = "toolStripSeparator42";
            this.toolStripSeparator42.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel8
            // 
            this.toolStripLabel8.AutoToolTip = true;
            this.toolStripLabel8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel8.Name = "toolStripLabel8";
            this.toolStripLabel8.Size = new System.Drawing.Size(47, 22);
            this.toolStripLabel8.Text = "Record";
            // 
            // positionPlcyTextBox
            // 
            this.positionPlcyTextBox.AutoToolTip = true;
            this.positionPlcyTextBox.BackColor = System.Drawing.Color.White;
            this.positionPlcyTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.positionPlcyTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.positionPlcyTextBox.Name = "positionPlcyTextBox";
            this.positionPlcyTextBox.ReadOnly = true;
            this.positionPlcyTextBox.Size = new System.Drawing.Size(70, 25);
            this.positionPlcyTextBox.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.positionPlcyTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.positionPlcyTextBox_KeyDown);
            // 
            // totalRecPlcyLabel
            // 
            this.totalRecPlcyLabel.AutoToolTip = true;
            this.totalRecPlcyLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalRecPlcyLabel.Name = "totalRecPlcyLabel";
            this.totalRecPlcyLabel.Size = new System.Drawing.Size(50, 22);
            this.totalRecPlcyLabel.Text = "of Total";
            // 
            // toolStripSeparator43
            // 
            this.toolStripSeparator43.Name = "toolStripSeparator43";
            this.toolStripSeparator43.Size = new System.Drawing.Size(6, 25);
            // 
            // moveNextPlcyButton
            // 
            this.moveNextPlcyButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveNextPlcyButton.Image = global::SystemAdministration.Properties.Resources.DataContainer_MoveNextHS;
            this.moveNextPlcyButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveNextPlcyButton.Name = "moveNextPlcyButton";
            this.moveNextPlcyButton.Size = new System.Drawing.Size(23, 22);
            this.moveNextPlcyButton.Text = "Move Next";
            this.moveNextPlcyButton.Click += new System.EventHandler(this.plcyPnlNavButtons);
            // 
            // toolStripSeparator44
            // 
            this.toolStripSeparator44.Name = "toolStripSeparator44";
            this.toolStripSeparator44.Size = new System.Drawing.Size(6, 25);
            // 
            // moveLastPlcyButton
            // 
            this.moveLastPlcyButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveLastPlcyButton.Image = global::SystemAdministration.Properties.Resources.DataContainer_MoveLastHS;
            this.moveLastPlcyButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveLastPlcyButton.Name = "moveLastPlcyButton";
            this.moveLastPlcyButton.Size = new System.Drawing.Size(23, 22);
            this.moveLastPlcyButton.Text = "Move Last";
            this.moveLastPlcyButton.Click += new System.EventHandler(this.plcyPnlNavButtons);
            // 
            // toolStripSeparator45
            // 
            this.toolStripSeparator45.Name = "toolStripSeparator45";
            this.toolStripSeparator45.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel12
            // 
            this.toolStripLabel12.Name = "toolStripLabel12";
            this.toolStripLabel12.Size = new System.Drawing.Size(65, 22);
            this.toolStripLabel12.Text = "Search For:";
            // 
            // toolStripSeparator49
            // 
            this.toolStripSeparator49.Name = "toolStripSeparator49";
            this.toolStripSeparator49.Size = new System.Drawing.Size(6, 25);
            // 
            // searchForPlcyTextBox
            // 
            this.searchForPlcyTextBox.Name = "searchForPlcyTextBox";
            this.searchForPlcyTextBox.Size = new System.Drawing.Size(100, 25);
            this.searchForPlcyTextBox.Enter += new System.EventHandler(this.searchForPlcyTextBox_Click);
            this.searchForPlcyTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForPlcyTextBox_KeyDown);
            this.searchForPlcyTextBox.Click += new System.EventHandler(this.searchForPlcyTextBox_Click);
            // 
            // toolStripSeparator50
            // 
            this.toolStripSeparator50.Name = "toolStripSeparator50";
            this.toolStripSeparator50.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel13
            // 
            this.toolStripLabel13.Name = "toolStripLabel13";
            this.toolStripLabel13.Size = new System.Drawing.Size(58, 22);
            this.toolStripLabel13.Text = "Search In:";
            // 
            // toolStripSeparator51
            // 
            this.toolStripSeparator51.Name = "toolStripSeparator51";
            this.toolStripSeparator51.Size = new System.Drawing.Size(6, 25);
            // 
            // searchInPlcyComboBox
            // 
            this.searchInPlcyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.searchInPlcyComboBox.Items.AddRange(new object[] {
            "Policy Name"});
            this.searchInPlcyComboBox.Name = "searchInPlcyComboBox";
            this.searchInPlcyComboBox.Size = new System.Drawing.Size(121, 25);
            this.searchInPlcyComboBox.Sorted = true;
            this.searchInPlcyComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForPlcyTextBox_KeyDown);
            // 
            // label27
            // 
            this.label27.Location = new System.Drawing.Point(279, 401);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(100, 23);
            this.label27.TabIndex = 85;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.emailServerPanel);
            this.tabPage6.ImageKey = "antenna1.png";
            this.tabPage6.Location = new System.Drawing.Point(4, 60);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(1025, 655);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "SERVER SETTINGS";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // emailServerPanel
            // 
            this.emailServerPanel.AutoScroll = true;
            this.emailServerPanel.BackColor = System.Drawing.Color.LightSlateGray;
            this.emailServerPanel.Controls.Add(this.groupBox13);
            this.emailServerPanel.Controls.Add(this.crntOrgTextBox);
            this.emailServerPanel.Controls.Add(this.toolStrip6);
            this.emailServerPanel.Controls.Add(this.groupBox12);
            this.emailServerPanel.Controls.Add(this.groupBox11);
            this.emailServerPanel.Controls.Add(this.groupBox10);
            this.emailServerPanel.Controls.Add(this.groupBox9);
            this.emailServerPanel.Controls.Add(this.groupBox8);
            this.emailServerPanel.Controls.Add(this.curOrgPictureBox);
            this.emailServerPanel.Controls.Add(this.label43);
            this.emailServerPanel.Controls.Add(this.crntOrgButton);
            this.emailServerPanel.Controls.Add(this.crntOrgIDTextBox);
            this.emailServerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.emailServerPanel.Enabled = false;
            this.emailServerPanel.Location = new System.Drawing.Point(3, 3);
            this.emailServerPanel.Name = "emailServerPanel";
            this.emailServerPanel.Size = new System.Drawing.Size(1019, 649);
            this.emailServerPanel.TabIndex = 2;
            this.emailServerPanel.Visible = false;
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.smsDataGridView);
            this.groupBox13.ForeColor = System.Drawing.Color.White;
            this.groupBox13.Location = new System.Drawing.Point(370, 170);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(387, 329);
            this.groupBox13.TabIndex = 158;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "SMS API GATEWAY SETTINGS (REST)";
            // 
            // smsDataGridView
            // 
            this.smsDataGridView.AllowUserToAddRows = false;
            this.smsDataGridView.AllowUserToDeleteRows = false;
            this.smsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.smsDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.smsDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.smsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.smsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column8,
            this.Column9});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.smsDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.smsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smsDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.smsDataGridView.Location = new System.Drawing.Point(3, 17);
            this.smsDataGridView.Name = "smsDataGridView";
            this.smsDataGridView.RowHeadersWidth = 15;
            this.smsDataGridView.Size = new System.Drawing.Size(381, 309);
            this.smsDataGridView.TabIndex = 0;
            // 
            // Column8
            // 
            this.Column8.FillWeight = 30F;
            this.Column8.HeaderText = "Parameter";
            this.Column8.Name = "Column8";
            this.Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Column9
            // 
            this.Column9.FillWeight = 70F;
            this.Column9.HeaderText = "Value";
            this.Column9.Name = "Column9";
            this.Column9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // crntOrgTextBox
            // 
            this.crntOrgTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.crntOrgTextBox.Location = new System.Drawing.Point(763, 49);
            this.crntOrgTextBox.Multiline = true;
            this.crntOrgTextBox.Name = "crntOrgTextBox";
            this.crntOrgTextBox.ReadOnly = true;
            this.crntOrgTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.crntOrgTextBox.Size = new System.Drawing.Size(241, 35);
            this.crntOrgTextBox.TabIndex = 155;
            // 
            // toolStrip6
            // 
            this.toolStrip6.AutoSize = false;
            this.toolStrip6.BackColor = System.Drawing.Color.WhiteSmoke;
            this.toolStrip6.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addEmlSvrButton,
            this.toolStripSeparator114,
            this.editEmlSvrButton,
            this.toolStripSeparator112,
            this.saveEmlSvrButton,
            this.toolStripSeparator111,
            this.deleteSrvrButton,
            this.toolStripSeparator52,
            this.refreshEmlSvrButton,
            this.toolStripSeparator22,
            this.recHstryEmlSvrButton,
            this.toolStripSeparator113,
            this.vwSQLEmlSvrButton,
            this.toolStripSeparator110,
            this.moveFirstEmlSvrButton,
            this.toolStripSeparator98,
            this.movePreviousEmlSvrButton,
            this.toolStripSeparator99,
            this.toolStripLabel21,
            this.positionEmlSvrTextBox,
            this.totalRecEmlSvrLabel,
            this.toolStripSeparator100,
            this.moveNextEmlSvrButton,
            this.toolStripSeparator101,
            this.moveLastEmlSvrButton,
            this.toolStripSeparator104,
            this.toolStripLabel23,
            this.toolStripSeparator106,
            this.searchForEmlSvrTextBox,
            this.toolStripSeparator107,
            this.toolStripLabel24,
            this.toolStripSeparator108,
            this.searchInEmlSvrComboBox,
            this.toolStripSeparator109});
            this.toolStrip6.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip6.Location = new System.Drawing.Point(0, 0);
            this.toolStrip6.Name = "toolStrip6";
            this.toolStrip6.Size = new System.Drawing.Size(1019, 25);
            this.toolStrip6.Stretch = true;
            this.toolStrip6.TabIndex = 0;
            this.toolStrip6.TabStop = true;
            this.toolStrip6.Text = "ToolStrip2";
            // 
            // addEmlSvrButton
            // 
            this.addEmlSvrButton.Image = global::SystemAdministration.Properties.Resources.plus_32;
            this.addEmlSvrButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addEmlSvrButton.Name = "addEmlSvrButton";
            this.addEmlSvrButton.Size = new System.Drawing.Size(51, 22);
            this.addEmlSvrButton.Text = "ADD";
            this.addEmlSvrButton.Click += new System.EventHandler(this.addEmlSvrButton_Click);
            // 
            // toolStripSeparator114
            // 
            this.toolStripSeparator114.Name = "toolStripSeparator114";
            this.toolStripSeparator114.Size = new System.Drawing.Size(6, 25);
            // 
            // editEmlSvrButton
            // 
            this.editEmlSvrButton.Image = global::SystemAdministration.Properties.Resources.edit32;
            this.editEmlSvrButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editEmlSvrButton.Name = "editEmlSvrButton";
            this.editEmlSvrButton.Size = new System.Drawing.Size(51, 22);
            this.editEmlSvrButton.Text = "EDIT";
            this.editEmlSvrButton.Click += new System.EventHandler(this.editEmlSvrButton_Click);
            // 
            // toolStripSeparator112
            // 
            this.toolStripSeparator112.Name = "toolStripSeparator112";
            this.toolStripSeparator112.Size = new System.Drawing.Size(6, 25);
            // 
            // saveEmlSvrButton
            // 
            this.saveEmlSvrButton.Image = global::SystemAdministration.Properties.Resources.FloppyDisk;
            this.saveEmlSvrButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveEmlSvrButton.Name = "saveEmlSvrButton";
            this.saveEmlSvrButton.Size = new System.Drawing.Size(53, 22);
            this.saveEmlSvrButton.Text = "SAVE";
            this.saveEmlSvrButton.Click += new System.EventHandler(this.saveEmlSvrButton_Click);
            // 
            // toolStripSeparator111
            // 
            this.toolStripSeparator111.Name = "toolStripSeparator111";
            this.toolStripSeparator111.Size = new System.Drawing.Size(6, 25);
            // 
            // refreshEmlSvrButton
            // 
            this.refreshEmlSvrButton.Image = global::SystemAdministration.Properties.Resources.refresh;
            this.refreshEmlSvrButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshEmlSvrButton.Name = "refreshEmlSvrButton";
            this.refreshEmlSvrButton.Size = new System.Drawing.Size(66, 22);
            this.refreshEmlSvrButton.Text = "Refresh";
            this.refreshEmlSvrButton.Click += new System.EventHandler(this.refreshEmlSvrButton_Click);
            // 
            // toolStripSeparator22
            // 
            this.toolStripSeparator22.Name = "toolStripSeparator22";
            this.toolStripSeparator22.Size = new System.Drawing.Size(6, 25);
            // 
            // recHstryEmlSvrButton
            // 
            this.recHstryEmlSvrButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.recHstryEmlSvrButton.Image = global::SystemAdministration.Properties.Resources.statistics_32;
            this.recHstryEmlSvrButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.recHstryEmlSvrButton.Name = "recHstryEmlSvrButton";
            this.recHstryEmlSvrButton.Size = new System.Drawing.Size(23, 22);
            this.recHstryEmlSvrButton.Text = "Record History";
            this.recHstryEmlSvrButton.Click += new System.EventHandler(this.recHstryEmlSvrButton_Click);
            // 
            // toolStripSeparator113
            // 
            this.toolStripSeparator113.Name = "toolStripSeparator113";
            this.toolStripSeparator113.Size = new System.Drawing.Size(6, 25);
            // 
            // vwSQLEmlSvrButton
            // 
            this.vwSQLEmlSvrButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.vwSQLEmlSvrButton.Image = global::SystemAdministration.Properties.Resources.SQL;
            this.vwSQLEmlSvrButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.vwSQLEmlSvrButton.Name = "vwSQLEmlSvrButton";
            this.vwSQLEmlSvrButton.Size = new System.Drawing.Size(23, 22);
            this.vwSQLEmlSvrButton.Text = "View SQL";
            this.vwSQLEmlSvrButton.Click += new System.EventHandler(this.vwSQLEmlSvrButton_Click);
            // 
            // toolStripSeparator110
            // 
            this.toolStripSeparator110.Name = "toolStripSeparator110";
            this.toolStripSeparator110.Size = new System.Drawing.Size(6, 25);
            // 
            // moveFirstEmlSvrButton
            // 
            this.moveFirstEmlSvrButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveFirstEmlSvrButton.Image = global::SystemAdministration.Properties.Resources.DataContainer_MoveFirstHS;
            this.moveFirstEmlSvrButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveFirstEmlSvrButton.Name = "moveFirstEmlSvrButton";
            this.moveFirstEmlSvrButton.Size = new System.Drawing.Size(23, 22);
            this.moveFirstEmlSvrButton.Text = "Move First";
            this.moveFirstEmlSvrButton.Click += new System.EventHandler(this.emlSvrPnlNavButtons);
            // 
            // toolStripSeparator98
            // 
            this.toolStripSeparator98.Name = "toolStripSeparator98";
            this.toolStripSeparator98.Size = new System.Drawing.Size(6, 25);
            // 
            // movePreviousEmlSvrButton
            // 
            this.movePreviousEmlSvrButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.movePreviousEmlSvrButton.Image = global::SystemAdministration.Properties.Resources.DataContainer_MovePreviousHS;
            this.movePreviousEmlSvrButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.movePreviousEmlSvrButton.Name = "movePreviousEmlSvrButton";
            this.movePreviousEmlSvrButton.Size = new System.Drawing.Size(23, 22);
            this.movePreviousEmlSvrButton.Text = "Move Previous";
            this.movePreviousEmlSvrButton.Click += new System.EventHandler(this.emlSvrPnlNavButtons);
            // 
            // toolStripSeparator99
            // 
            this.toolStripSeparator99.Name = "toolStripSeparator99";
            this.toolStripSeparator99.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel21
            // 
            this.toolStripLabel21.AutoToolTip = true;
            this.toolStripLabel21.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel21.Name = "toolStripLabel21";
            this.toolStripLabel21.Size = new System.Drawing.Size(47, 22);
            this.toolStripLabel21.Text = "Record";
            // 
            // positionEmlSvrTextBox
            // 
            this.positionEmlSvrTextBox.AutoToolTip = true;
            this.positionEmlSvrTextBox.BackColor = System.Drawing.Color.White;
            this.positionEmlSvrTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.positionEmlSvrTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.positionEmlSvrTextBox.Name = "positionEmlSvrTextBox";
            this.positionEmlSvrTextBox.ReadOnly = true;
            this.positionEmlSvrTextBox.Size = new System.Drawing.Size(70, 25);
            this.positionEmlSvrTextBox.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.positionEmlSvrTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.positionEmlSvrTextBox_KeyDown);
            // 
            // totalRecEmlSvrLabel
            // 
            this.totalRecEmlSvrLabel.AutoToolTip = true;
            this.totalRecEmlSvrLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalRecEmlSvrLabel.Name = "totalRecEmlSvrLabel";
            this.totalRecEmlSvrLabel.Size = new System.Drawing.Size(50, 22);
            this.totalRecEmlSvrLabel.Text = "of Total";
            // 
            // toolStripSeparator100
            // 
            this.toolStripSeparator100.Name = "toolStripSeparator100";
            this.toolStripSeparator100.Size = new System.Drawing.Size(6, 25);
            // 
            // moveNextEmlSvrButton
            // 
            this.moveNextEmlSvrButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveNextEmlSvrButton.Image = global::SystemAdministration.Properties.Resources.DataContainer_MoveNextHS;
            this.moveNextEmlSvrButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveNextEmlSvrButton.Name = "moveNextEmlSvrButton";
            this.moveNextEmlSvrButton.Size = new System.Drawing.Size(23, 22);
            this.moveNextEmlSvrButton.Text = "Move Next";
            this.moveNextEmlSvrButton.Click += new System.EventHandler(this.emlSvrPnlNavButtons);
            // 
            // toolStripSeparator101
            // 
            this.toolStripSeparator101.Name = "toolStripSeparator101";
            this.toolStripSeparator101.Size = new System.Drawing.Size(6, 25);
            // 
            // moveLastEmlSvrButton
            // 
            this.moveLastEmlSvrButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveLastEmlSvrButton.Image = global::SystemAdministration.Properties.Resources.DataContainer_MoveLastHS;
            this.moveLastEmlSvrButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveLastEmlSvrButton.Name = "moveLastEmlSvrButton";
            this.moveLastEmlSvrButton.Size = new System.Drawing.Size(23, 22);
            this.moveLastEmlSvrButton.Text = "Move Last";
            this.moveLastEmlSvrButton.Click += new System.EventHandler(this.emlSvrPnlNavButtons);
            // 
            // toolStripSeparator104
            // 
            this.toolStripSeparator104.Name = "toolStripSeparator104";
            this.toolStripSeparator104.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel23
            // 
            this.toolStripLabel23.Name = "toolStripLabel23";
            this.toolStripLabel23.Size = new System.Drawing.Size(65, 22);
            this.toolStripLabel23.Text = "Search For:";
            // 
            // toolStripSeparator106
            // 
            this.toolStripSeparator106.Name = "toolStripSeparator106";
            this.toolStripSeparator106.Size = new System.Drawing.Size(6, 25);
            // 
            // searchForEmlSvrTextBox
            // 
            this.searchForEmlSvrTextBox.Name = "searchForEmlSvrTextBox";
            this.searchForEmlSvrTextBox.Size = new System.Drawing.Size(100, 25);
            this.searchForEmlSvrTextBox.Enter += new System.EventHandler(this.searchForEmlSvrTextBox_Click);
            this.searchForEmlSvrTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForEmlSvrTextBox_KeyDown);
            this.searchForEmlSvrTextBox.Click += new System.EventHandler(this.searchForEmlSvrTextBox_Click);
            // 
            // toolStripSeparator107
            // 
            this.toolStripSeparator107.Name = "toolStripSeparator107";
            this.toolStripSeparator107.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel24
            // 
            this.toolStripLabel24.Name = "toolStripLabel24";
            this.toolStripLabel24.Size = new System.Drawing.Size(58, 22);
            this.toolStripLabel24.Text = "Search In:";
            // 
            // toolStripSeparator108
            // 
            this.toolStripSeparator108.Name = "toolStripSeparator108";
            this.toolStripSeparator108.Size = new System.Drawing.Size(6, 25);
            // 
            // searchInEmlSvrComboBox
            // 
            this.searchInEmlSvrComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.searchInEmlSvrComboBox.Items.AddRange(new object[] {
            "SENDER\'s USER NAME",
            "SMTP CLIENT"});
            this.searchInEmlSvrComboBox.Name = "searchInEmlSvrComboBox";
            this.searchInEmlSvrComboBox.Size = new System.Drawing.Size(121, 25);
            this.searchInEmlSvrComboBox.Sorted = true;
            this.searchInEmlSvrComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForEmlSvrTextBox_KeyDown);
            // 
            // toolStripSeparator109
            // 
            this.toolStripSeparator109.Name = "toolStripSeparator109";
            this.toolStripSeparator109.Size = new System.Drawing.Size(6, 25);
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.label58);
            this.groupBox12.Controls.Add(this.label57);
            this.groupBox12.Controls.Add(this.label56);
            this.groupBox12.Controls.Add(this.label55);
            this.groupBox12.Controls.Add(this.label54);
            this.groupBox12.Controls.Add(this.label53);
            this.groupBox12.Controls.Add(this.label52);
            this.groupBox12.Controls.Add(this.label51);
            this.groupBox12.Controls.Add(this.label50);
            this.groupBox12.Controls.Add(this.label47);
            this.groupBox12.ForeColor = System.Drawing.Color.White;
            this.groupBox12.Location = new System.Drawing.Point(763, 86);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(240, 390);
            this.groupBox12.TabIndex = 83;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "QUICK ORGANISATION SETUP GUIDE";
            // 
            // label58
            // 
            this.label58.BackColor = System.Drawing.Color.Black;
            this.label58.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label58.Location = new System.Drawing.Point(5, 351);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(229, 35);
            this.label58.TabIndex = 169;
            this.label58.Text = "10. Define Events/Venues and Time Tables (Events && Attendance)";
            this.label58.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label57
            // 
            this.label57.BackColor = System.Drawing.Color.Black;
            this.label57.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label57.Location = new System.Drawing.Point(5, 316);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(229, 35);
            this.label57.TabIndex = 168;
            this.label57.Text = "9. Define Pay Items/Item Sets/Person Sets and Assign to Persons (Internal Payment" +
    "s Module)";
            this.label57.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label56
            // 
            this.label56.BackColor = System.Drawing.Color.Black;
            this.label56.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label56.Location = new System.Drawing.Point(5, 281);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(229, 35);
            this.label56.TabIndex = 167;
            this.label56.Text = "8. Define Product Categories/Stores/Items (Stores && Inventory)";
            this.label56.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label55
            // 
            this.label55.BackColor = System.Drawing.Color.Black;
            this.label55.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label55.Location = new System.Drawing.Point(5, 15);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(229, 56);
            this.label55.TabIndex = 166;
            this.label55.Text = "1. Define an Organisation at the Organisation Setup Module. Click on Start Menu=>" +
    "Select Roles and select the Organisation you just Created, then click OK.";
            this.label55.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label54
            // 
            this.label54.BackColor = System.Drawing.Color.Black;
            this.label54.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label54.Location = new System.Drawing.Point(5, 71);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(229, 35);
            this.label54.TabIndex = 165;
            this.label54.Text = "2. Define Chart of Accounts in the Accounting Module";
            this.label54.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label53
            // 
            this.label53.BackColor = System.Drawing.Color.Black;
            this.label53.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label53.Location = new System.Drawing.Point(5, 246);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(229, 35);
            this.label53.TabIndex = 164;
            this.label53.Text = "7. Define Persons in the Basic Person Data Module";
            this.label53.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label52
            // 
            this.label52.BackColor = System.Drawing.Color.Black;
            this.label52.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label52.Location = new System.Drawing.Point(5, 211);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(229, 35);
            this.label52.TabIndex = 163;
            this.label52.Text = "6. Define Document Templates in the Accounting Module";
            this.label52.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label51
            // 
            this.label51.BackColor = System.Drawing.Color.Black;
            this.label51.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label51.Location = new System.Drawing.Point(5, 176);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(229, 35);
            this.label51.TabIndex = 162;
            this.label51.Text = "5. Define Payment Methods in the Accounting Module";
            this.label51.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label50
            // 
            this.label50.BackColor = System.Drawing.Color.Black;
            this.label50.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label50.Location = new System.Drawing.Point(5, 141);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(229, 35);
            this.label50.TabIndex = 161;
            this.label50.Text = "4. Define Default Accounts in the Accounting Module";
            this.label50.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label47
            // 
            this.label47.BackColor = System.Drawing.Color.Black;
            this.label47.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label47.Location = new System.Drawing.Point(5, 106);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(229, 35);
            this.label47.TabIndex = 160;
            this.label47.Text = "3. Define Accounting Periods in the Accounting Module";
            this.label47.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.loadLOVsButton);
            this.groupBox11.Controls.Add(this.restoreButton);
            this.groupBox11.Controls.Add(this.bckpButton);
            this.groupBox11.Controls.Add(this.bckpDirButton);
            this.groupBox11.Controls.Add(this.pgDirButton);
            this.groupBox11.Controls.Add(this.bckpFileDirTextBox);
            this.groupBox11.Controls.Add(this.pgDirTextBox);
            this.groupBox11.Controls.Add(this.label48);
            this.groupBox11.Controls.Add(this.label49);
            this.groupBox11.ForeColor = System.Drawing.Color.White;
            this.groupBox11.Location = new System.Drawing.Point(370, 29);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(387, 137);
            this.groupBox11.TabIndex = 82;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "DATABASE BACKUP/RESTORE DEFAULT SETTINGS";
            // 
            // loadLOVsButton
            // 
            this.loadLOVsButton.ForeColor = System.Drawing.Color.Black;
            this.loadLOVsButton.Image = global::SystemAdministration.Properties.Resources.action_go;
            this.loadLOVsButton.Location = new System.Drawing.Point(203, 98);
            this.loadLOVsButton.Name = "loadLOVsButton";
            this.loadLOVsButton.Size = new System.Drawing.Size(174, 34);
            this.loadLOVsButton.TabIndex = 143;
            this.loadLOVsButton.Text = "LOAD ROLES && PRIVILEDGES and REQUIRED LOVs";
            this.loadLOVsButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.loadLOVsButton.UseVisualStyleBackColor = true;
            this.loadLOVsButton.Click += new System.EventHandler(this.loadLOVsButton_Click);
            // 
            // restoreButton
            // 
            this.restoreButton.ForeColor = System.Drawing.Color.Black;
            this.restoreButton.Location = new System.Drawing.Point(106, 98);
            this.restoreButton.Name = "restoreButton";
            this.restoreButton.Size = new System.Drawing.Size(97, 34);
            this.restoreButton.TabIndex = 143;
            this.restoreButton.Text = "RESTORE DATABASE";
            this.restoreButton.UseVisualStyleBackColor = true;
            this.restoreButton.Click += new System.EventHandler(this.restoreButton_Click);
            // 
            // bckpButton
            // 
            this.bckpButton.ForeColor = System.Drawing.Color.Black;
            this.bckpButton.Location = new System.Drawing.Point(9, 98);
            this.bckpButton.Name = "bckpButton";
            this.bckpButton.Size = new System.Drawing.Size(97, 34);
            this.bckpButton.TabIndex = 142;
            this.bckpButton.Text = "BACKUP DATABASE";
            this.bckpButton.UseVisualStyleBackColor = true;
            this.bckpButton.Click += new System.EventHandler(this.bckpButton_Click);
            // 
            // bckpDirButton
            // 
            this.bckpDirButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bckpDirButton.ForeColor = System.Drawing.Color.Black;
            this.bckpDirButton.Location = new System.Drawing.Point(348, 73);
            this.bckpDirButton.Name = "bckpDirButton";
            this.bckpDirButton.Size = new System.Drawing.Size(28, 23);
            this.bckpDirButton.TabIndex = 141;
            this.bckpDirButton.Text = "...";
            this.bckpDirButton.UseVisualStyleBackColor = true;
            this.bckpDirButton.Click += new System.EventHandler(this.bckpDirButton_Click);
            // 
            // pgDirButton
            // 
            this.pgDirButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pgDirButton.ForeColor = System.Drawing.Color.Black;
            this.pgDirButton.Location = new System.Drawing.Point(348, 34);
            this.pgDirButton.Name = "pgDirButton";
            this.pgDirButton.Size = new System.Drawing.Size(28, 23);
            this.pgDirButton.TabIndex = 140;
            this.pgDirButton.Text = "...";
            this.pgDirButton.UseVisualStyleBackColor = true;
            this.pgDirButton.Click += new System.EventHandler(this.pgDirButton_Click);
            // 
            // bckpFileDirTextBox
            // 
            this.bckpFileDirTextBox.Location = new System.Drawing.Point(10, 74);
            this.bckpFileDirTextBox.MaxLength = 100;
            this.bckpFileDirTextBox.Name = "bckpFileDirTextBox";
            this.bckpFileDirTextBox.ReadOnly = true;
            this.bckpFileDirTextBox.Size = new System.Drawing.Size(337, 21);
            this.bckpFileDirTextBox.TabIndex = 1;
            // 
            // pgDirTextBox
            // 
            this.pgDirTextBox.Location = new System.Drawing.Point(10, 35);
            this.pgDirTextBox.MaxLength = 200;
            this.pgDirTextBox.Name = "pgDirTextBox";
            this.pgDirTextBox.ReadOnly = true;
            this.pgDirTextBox.Size = new System.Drawing.Size(337, 21);
            this.pgDirTextBox.TabIndex = 0;
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.ForeColor = System.Drawing.Color.White;
            this.label48.Location = new System.Drawing.Point(9, 59);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(141, 13);
            this.label48.TabIndex = 1;
            this.label48.Text = "BACKUP FILES DIRECTORY:";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.ForeColor = System.Drawing.Color.White;
            this.label49.Location = new System.Drawing.Point(9, 19);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(119, 13);
            this.label49.TabIndex = 0;
            this.label49.Text = "PG_DUMP DIRECTORY:";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.timeoutComboBox);
            this.groupBox10.Controls.Add(this.label46);
            this.groupBox10.Controls.Add(this.baudRateComboBox);
            this.groupBox10.Controls.Add(this.comPortComboBox);
            this.groupBox10.Controls.Add(this.label45);
            this.groupBox10.Controls.Add(this.label44);
            this.groupBox10.ForeColor = System.Drawing.Color.White;
            this.groupBox10.Location = new System.Drawing.Point(8, 214);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(354, 99);
            this.groupBox10.TabIndex = 81;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "GSM MODEM SETTINGS";
            // 
            // timeoutComboBox
            // 
            this.timeoutComboBox.FormattingEnabled = true;
            this.timeoutComboBox.Items.AddRange(new object[] {
            "150",
            "300",
            "600",
            "900",
            "1200",
            "1500",
            "1800",
            "2000"});
            this.timeoutComboBox.Location = new System.Drawing.Point(137, 70);
            this.timeoutComboBox.MaxLength = 100;
            this.timeoutComboBox.Name = "timeoutComboBox";
            this.timeoutComboBox.Size = new System.Drawing.Size(199, 21);
            this.timeoutComboBox.TabIndex = 11;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.ForeColor = System.Drawing.Color.White;
            this.label46.Location = new System.Drawing.Point(14, 73);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(52, 13);
            this.label46.TabIndex = 10;
            this.label46.Text = "TMEOUT:";
            // 
            // baudRateComboBox
            // 
            this.baudRateComboBox.FormattingEnabled = true;
            this.baudRateComboBox.Items.AddRange(new object[] {
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.baudRateComboBox.Location = new System.Drawing.Point(137, 44);
            this.baudRateComboBox.MaxLength = 100;
            this.baudRateComboBox.Name = "baudRateComboBox";
            this.baudRateComboBox.Size = new System.Drawing.Size(199, 21);
            this.baudRateComboBox.TabIndex = 9;
            // 
            // comPortComboBox
            // 
            this.comPortComboBox.FormattingEnabled = true;
            this.comPortComboBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.comPortComboBox.Location = new System.Drawing.Point(137, 19);
            this.comPortComboBox.MaxLength = 100;
            this.comPortComboBox.Name = "comPortComboBox";
            this.comPortComboBox.Size = new System.Drawing.Size(199, 21);
            this.comPortComboBox.TabIndex = 8;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.ForeColor = System.Drawing.Color.White;
            this.label45.Location = new System.Drawing.Point(14, 47);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(67, 13);
            this.label45.TabIndex = 7;
            this.label45.Text = "BAUD RATE:";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.ForeColor = System.Drawing.Color.White;
            this.label44.Location = new System.Drawing.Point(14, 24);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(64, 13);
            this.label44.TabIndex = 3;
            this.label44.Text = "COM PORT:";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.ftpHomeDirTextBox);
            this.groupBox9.Controls.Add(this.label59);
            this.groupBox9.Controls.Add(this.enforceFTPCheckBox);
            this.groupBox9.Controls.Add(this.ftpBaseDirTextBox);
            this.groupBox9.Controls.Add(this.ftpPortNumUpDown);
            this.groupBox9.Controls.Add(this.ftpPswdTextBox);
            this.groupBox9.Controls.Add(this.ftpUnmTextBox);
            this.groupBox9.Controls.Add(this.ftpServerTextBox);
            this.groupBox9.Controls.Add(this.label37);
            this.groupBox9.Controls.Add(this.label39);
            this.groupBox9.Controls.Add(this.label40);
            this.groupBox9.Controls.Add(this.label41);
            this.groupBox9.Controls.Add(this.label42);
            this.groupBox9.ForeColor = System.Drawing.Color.White;
            this.groupBox9.Location = new System.Drawing.Point(7, 315);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(354, 184);
            this.groupBox9.TabIndex = 80;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "FTP SERVER";
            // 
            // ftpHomeDirTextBox
            // 
            this.ftpHomeDirTextBox.Location = new System.Drawing.Point(138, 116);
            this.ftpHomeDirTextBox.MaxLength = 200;
            this.ftpHomeDirTextBox.Name = "ftpHomeDirTextBox";
            this.ftpHomeDirTextBox.Size = new System.Drawing.Size(198, 21);
            this.ftpHomeDirTextBox.TabIndex = 4;
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.ForeColor = System.Drawing.Color.White;
            this.label59.Location = new System.Drawing.Point(4, 120);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(138, 13);
            this.label59.TabIndex = 8;
            this.label59.Text = "FTP User\'s Home Directory:";
            // 
            // enforceFTPCheckBox
            // 
            this.enforceFTPCheckBox.AutoSize = true;
            this.enforceFTPCheckBox.ForeColor = System.Drawing.Color.White;
            this.enforceFTPCheckBox.Location = new System.Drawing.Point(137, 163);
            this.enforceFTPCheckBox.Name = "enforceFTPCheckBox";
            this.enforceFTPCheckBox.Size = new System.Drawing.Size(171, 17);
            this.enforceFTPCheckBox.TabIndex = 7;
            this.enforceFTPCheckBox.Text = "Enforce FTP File Server Usage";
            this.enforceFTPCheckBox.UseVisualStyleBackColor = true;
            this.enforceFTPCheckBox.CheckedChanged += new System.EventHandler(this.enforceFTPCheckBox_CheckedChanged);
            // 
            // ftpBaseDirTextBox
            // 
            this.ftpBaseDirTextBox.Location = new System.Drawing.Point(138, 139);
            this.ftpBaseDirTextBox.MaxLength = 200;
            this.ftpBaseDirTextBox.Name = "ftpBaseDirTextBox";
            this.ftpBaseDirTextBox.Size = new System.Drawing.Size(198, 21);
            this.ftpBaseDirTextBox.TabIndex = 6;
            // 
            // ftpPortNumUpDown
            // 
            this.ftpPortNumUpDown.Location = new System.Drawing.Point(138, 93);
            this.ftpPortNumUpDown.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.ftpPortNumUpDown.Name = "ftpPortNumUpDown";
            this.ftpPortNumUpDown.Size = new System.Drawing.Size(198, 21);
            this.ftpPortNumUpDown.TabIndex = 3;
            this.ftpPortNumUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ftpPortNumUpDown.ValueChanged += new System.EventHandler(this.ftpPortNumUpDown_ValueChanged);
            // 
            // ftpPswdTextBox
            // 
            this.ftpPswdTextBox.Location = new System.Drawing.Point(138, 69);
            this.ftpPswdTextBox.MaxLength = 100;
            this.ftpPswdTextBox.Name = "ftpPswdTextBox";
            this.ftpPswdTextBox.PasswordChar = '*';
            this.ftpPswdTextBox.Size = new System.Drawing.Size(198, 21);
            this.ftpPswdTextBox.TabIndex = 2;
            // 
            // ftpUnmTextBox
            // 
            this.ftpUnmTextBox.Location = new System.Drawing.Point(138, 45);
            this.ftpUnmTextBox.MaxLength = 100;
            this.ftpUnmTextBox.Name = "ftpUnmTextBox";
            this.ftpUnmTextBox.Size = new System.Drawing.Size(198, 21);
            this.ftpUnmTextBox.TabIndex = 1;
            // 
            // ftpServerTextBox
            // 
            this.ftpServerTextBox.Location = new System.Drawing.Point(138, 21);
            this.ftpServerTextBox.MaxLength = 200;
            this.ftpServerTextBox.Name = "ftpServerTextBox";
            this.ftpServerTextBox.Size = new System.Drawing.Size(198, 21);
            this.ftpServerTextBox.TabIndex = 0;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.ForeColor = System.Drawing.Color.White;
            this.label37.Location = new System.Drawing.Point(4, 143);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(120, 13);
            this.label37.TabIndex = 5;
            this.label37.Text = "Base Subdirectory URL:";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.ForeColor = System.Drawing.Color.White;
            this.label39.Location = new System.Drawing.Point(4, 97);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(59, 13);
            this.label39.TabIndex = 3;
            this.label39.Text = "FTP PORT:";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.ForeColor = System.Drawing.Color.White;
            this.label40.Location = new System.Drawing.Point(4, 73);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(118, 13);
            this.label40.TabIndex = 2;
            this.label40.Text = "FTP USER PASSWORD:";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.ForeColor = System.Drawing.Color.White;
            this.label41.Location = new System.Drawing.Point(4, 49);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(89, 13);
            this.label41.TabIndex = 1;
            this.label41.Text = "FTP USER NAME:";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.ForeColor = System.Drawing.Color.White;
            this.label42.Location = new System.Drawing.Point(4, 25);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(92, 13);
            this.label42.TabIndex = 0;
            this.label42.Text = "FTP SERVER URL:";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.smtpClientTextBox);
            this.groupBox8.Controls.Add(this.isDfltNoEmlSvrCheckBox);
            this.groupBox8.Controls.Add(this.isDfltYesEmlSvrCheckBox);
            this.groupBox8.Controls.Add(this.smtpPortNmUpDown);
            this.groupBox8.Controls.Add(this.emailPswdTextBox);
            this.groupBox8.Controls.Add(this.emailUnameTextBox);
            this.groupBox8.Controls.Add(this.label32);
            this.groupBox8.Controls.Add(this.label31);
            this.groupBox8.Controls.Add(this.label30);
            this.groupBox8.Controls.Add(this.label29);
            this.groupBox8.Controls.Add(this.label28);
            this.groupBox8.Controls.Add(this.emlSrvrIDTextBox);
            this.groupBox8.Controls.Add(this.label33);
            this.groupBox8.Controls.Add(this.activeDrctryDmnTextBox);
            this.groupBox8.ForeColor = System.Drawing.Color.White;
            this.groupBox8.Location = new System.Drawing.Point(8, 28);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(354, 183);
            this.groupBox8.TabIndex = 0;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "EMAIL SERVER";
            // 
            // isDfltNoEmlSvrCheckBox
            // 
            this.isDfltNoEmlSvrCheckBox.AutoSize = true;
            this.isDfltNoEmlSvrCheckBox.ForeColor = System.Drawing.Color.White;
            this.isDfltNoEmlSvrCheckBox.Location = new System.Drawing.Point(237, 117);
            this.isDfltNoEmlSvrCheckBox.Name = "isDfltNoEmlSvrCheckBox";
            this.isDfltNoEmlSvrCheckBox.Size = new System.Drawing.Size(39, 17);
            this.isDfltNoEmlSvrCheckBox.TabIndex = 5;
            this.isDfltNoEmlSvrCheckBox.Text = "No";
            this.isDfltNoEmlSvrCheckBox.UseVisualStyleBackColor = true;
            this.isDfltNoEmlSvrCheckBox.CheckedChanged += new System.EventHandler(this.isDfltNoEmlSvrCheckBox_CheckedChanged);
            // 
            // isDfltYesEmlSvrCheckBox
            // 
            this.isDfltYesEmlSvrCheckBox.AutoSize = true;
            this.isDfltYesEmlSvrCheckBox.ForeColor = System.Drawing.Color.White;
            this.isDfltYesEmlSvrCheckBox.Location = new System.Drawing.Point(178, 117);
            this.isDfltYesEmlSvrCheckBox.Name = "isDfltYesEmlSvrCheckBox";
            this.isDfltYesEmlSvrCheckBox.Size = new System.Drawing.Size(43, 17);
            this.isDfltYesEmlSvrCheckBox.TabIndex = 4;
            this.isDfltYesEmlSvrCheckBox.Text = "Yes";
            this.isDfltYesEmlSvrCheckBox.UseVisualStyleBackColor = true;
            this.isDfltYesEmlSvrCheckBox.CheckedChanged += new System.EventHandler(this.isDfltYesEmlSvrCheckBox_CheckedChanged);
            // 
            // smtpPortNmUpDown
            // 
            this.smtpPortNmUpDown.Location = new System.Drawing.Point(177, 93);
            this.smtpPortNmUpDown.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.smtpPortNmUpDown.Name = "smtpPortNmUpDown";
            this.smtpPortNmUpDown.Size = new System.Drawing.Size(159, 21);
            this.smtpPortNmUpDown.TabIndex = 3;
            this.smtpPortNmUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.smtpPortNmUpDown.ValueChanged += new System.EventHandler(this.smtpPortNmUpDown_ValueChanged);
            // 
            // emailPswdTextBox
            // 
            this.emailPswdTextBox.Location = new System.Drawing.Point(178, 69);
            this.emailPswdTextBox.MaxLength = 100;
            this.emailPswdTextBox.Name = "emailPswdTextBox";
            this.emailPswdTextBox.PasswordChar = '*';
            this.emailPswdTextBox.Size = new System.Drawing.Size(158, 21);
            this.emailPswdTextBox.TabIndex = 2;
            // 
            // emailUnameTextBox
            // 
            this.emailUnameTextBox.Location = new System.Drawing.Point(178, 45);
            this.emailUnameTextBox.MaxLength = 100;
            this.emailUnameTextBox.Name = "emailUnameTextBox";
            this.emailUnameTextBox.Size = new System.Drawing.Size(158, 21);
            this.emailUnameTextBox.TabIndex = 1;
            // 
            // smtpClientTextBox
            // 
            this.smtpClientTextBox.Location = new System.Drawing.Point(101, 21);
            this.smtpClientTextBox.MaxLength = 200;
            this.smtpClientTextBox.Name = "smtpClientTextBox";
            this.smtpClientTextBox.Size = new System.Drawing.Size(235, 21);
            this.smtpClientTextBox.TabIndex = 0;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.ForeColor = System.Drawing.Color.White;
            this.label32.Location = new System.Drawing.Point(14, 119);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(69, 13);
            this.label32.TabIndex = 4;
            this.label32.Text = "IS DEFAULT?";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.ForeColor = System.Drawing.Color.White;
            this.label31.Location = new System.Drawing.Point(14, 97);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(67, 13);
            this.label31.TabIndex = 3;
            this.label31.Text = "SMTP PORT:";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.ForeColor = System.Drawing.Color.White;
            this.label30.Location = new System.Drawing.Point(14, 73);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(150, 13);
            this.label30.TabIndex = 2;
            this.label30.Text = "SENDER\'s EMAIL PASSWORD:";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.ForeColor = System.Drawing.Color.White;
            this.label29.Location = new System.Drawing.Point(14, 49);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(150, 13);
            this.label29.TabIndex = 1;
            this.label29.Text = "SENDER\'s EMAIL USER NAME:";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.ForeColor = System.Drawing.Color.White;
            this.label28.Location = new System.Drawing.Point(14, 25);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(75, 13);
            this.label28.TabIndex = 0;
            this.label28.Text = "SMTP CLIENT:";
            // 
            // emlSrvrIDTextBox
            // 
            this.emlSrvrIDTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.emlSrvrIDTextBox.Location = new System.Drawing.Point(237, 21);
            this.emlSrvrIDTextBox.Name = "emlSrvrIDTextBox";
            this.emlSrvrIDTextBox.ReadOnly = true;
            this.emlSrvrIDTextBox.Size = new System.Drawing.Size(99, 21);
            this.emlSrvrIDTextBox.TabIndex = 13;
            this.emlSrvrIDTextBox.TabStop = false;
            // 
            // label33
            // 
            this.label33.ForeColor = System.Drawing.Color.White;
            this.label33.Location = new System.Drawing.Point(14, 133);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(117, 44);
            this.label33.TabIndex = 5;
            this.label33.Text = "LOCAL ACTIVE DIRECTORY DOMAIN NAME:";
            // 
            // activeDrctryDmnTextBox
            // 
            this.activeDrctryDmnTextBox.Location = new System.Drawing.Point(137, 137);
            this.activeDrctryDmnTextBox.MaxLength = 100;
            this.activeDrctryDmnTextBox.Multiline = true;
            this.activeDrctryDmnTextBox.Name = "activeDrctryDmnTextBox";
            this.activeDrctryDmnTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.activeDrctryDmnTextBox.Size = new System.Drawing.Size(199, 36);
            this.activeDrctryDmnTextBox.TabIndex = 6;
            // 
            // curOrgPictureBox
            // 
            this.curOrgPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.curOrgPictureBox.Image = global::SystemAdministration.Properties.Resources.blank;
            this.curOrgPictureBox.Location = new System.Drawing.Point(763, 60);
            this.curOrgPictureBox.Name = "curOrgPictureBox";
            this.curOrgPictureBox.Size = new System.Drawing.Size(28, 14);
            this.curOrgPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.curOrgPictureBox.TabIndex = 153;
            this.curOrgPictureBox.TabStop = false;
            this.curOrgPictureBox.Visible = false;
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.ForeColor = System.Drawing.Color.White;
            this.label43.Location = new System.Drawing.Point(762, 32);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(135, 13);
            this.label43.TabIndex = 154;
            this.label43.Text = "CURRENT ORGANIZATION";
            // 
            // crntOrgButton
            // 
            this.crntOrgButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.crntOrgButton.ForeColor = System.Drawing.Color.Black;
            this.crntOrgButton.Location = new System.Drawing.Point(763, 62);
            this.crntOrgButton.Name = "crntOrgButton";
            this.crntOrgButton.Size = new System.Drawing.Size(28, 23);
            this.crntOrgButton.TabIndex = 157;
            this.crntOrgButton.Text = "...";
            this.crntOrgButton.UseVisualStyleBackColor = true;
            this.crntOrgButton.Click += new System.EventHandler(this.crntOrgButton_Click);
            // 
            // crntOrgIDTextBox
            // 
            this.crntOrgIDTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.crntOrgIDTextBox.Location = new System.Drawing.Point(763, 51);
            this.crntOrgIDTextBox.Multiline = true;
            this.crntOrgIDTextBox.Name = "crntOrgIDTextBox";
            this.crntOrgIDTextBox.ReadOnly = true;
            this.crntOrgIDTextBox.Size = new System.Drawing.Size(27, 23);
            this.crntOrgIDTextBox.TabIndex = 156;
            this.crntOrgIDTextBox.TabStop = false;
            this.crntOrgIDTextBox.Text = "-1";
            this.crntOrgIDTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.auditPanel);
            this.tabPage7.ImageKey = "features_audittrail_icon.jpg";
            this.tabPage7.Location = new System.Drawing.Point(4, 60);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(1025, 655);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "AUDIT TRAIL DATA";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // auditPanel
            // 
            this.auditPanel.AutoScroll = true;
            this.auditPanel.BackColor = System.Drawing.Color.LightSlateGray;
            this.auditPanel.Controls.Add(this.toolStrip5);
            this.auditPanel.Controls.Add(this.auditTblsTreeView);
            this.auditPanel.Controls.Add(this.auditTblsDataGridView);
            this.auditPanel.Controls.Add(this.panel17);
            this.auditPanel.Controls.Add(this.label34);
            this.auditPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.auditPanel.Enabled = false;
            this.auditPanel.Location = new System.Drawing.Point(3, 3);
            this.auditPanel.Name = "auditPanel";
            this.auditPanel.Size = new System.Drawing.Size(1019, 649);
            this.auditPanel.TabIndex = 1;
            this.auditPanel.Visible = false;
            // 
            // toolStrip5
            // 
            this.toolStrip5.AutoSize = false;
            this.toolStrip5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.toolStrip5.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moveFirstAdtButton,
            this.toolStripSeparator80,
            this.movePreviousAdtButton,
            this.toolStripSeparator81,
            this.toolStripLabel17,
            this.positionAdtTextBox,
            this.totalRecAdtLabel,
            this.toolStripSeparator82,
            this.moveNextAdtButton,
            this.toolStripSeparator83,
            this.moveLastAdtButton,
            this.toolStripSeparator84,
            this.dsplySizeAdtComboBox,
            this.toolStripSeparator86,
            this.toolStripLabel19,
            this.toolStripSeparator88,
            this.searchForAdtTextBox,
            this.toolStripSeparator89,
            this.toolStripLabel20,
            this.toolStripSeparator90,
            this.searchInAdtComboBox,
            this.toolStripSeparator91,
            this.refreshAdtButton,
            this.toolStripSeparator17,
            this.vwSQLAdtButton,
            this.toolStripSeparator95});
            this.toolStrip5.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip5.Location = new System.Drawing.Point(0, 0);
            this.toolStrip5.Name = "toolStrip5";
            this.toolStrip5.Size = new System.Drawing.Size(1019, 25);
            this.toolStrip5.Stretch = true;
            this.toolStrip5.TabIndex = 0;
            this.toolStrip5.TabStop = true;
            this.toolStrip5.Text = "ToolStrip2";
            // 
            // moveFirstAdtButton
            // 
            this.moveFirstAdtButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveFirstAdtButton.Image = global::SystemAdministration.Properties.Resources.DataContainer_MoveFirstHS;
            this.moveFirstAdtButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveFirstAdtButton.Name = "moveFirstAdtButton";
            this.moveFirstAdtButton.Size = new System.Drawing.Size(23, 22);
            this.moveFirstAdtButton.Text = "Move First";
            this.moveFirstAdtButton.Click += new System.EventHandler(this.adtPnlNavButtons);
            // 
            // toolStripSeparator80
            // 
            this.toolStripSeparator80.Name = "toolStripSeparator80";
            this.toolStripSeparator80.Size = new System.Drawing.Size(6, 25);
            // 
            // movePreviousAdtButton
            // 
            this.movePreviousAdtButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.movePreviousAdtButton.Image = global::SystemAdministration.Properties.Resources.DataContainer_MovePreviousHS;
            this.movePreviousAdtButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.movePreviousAdtButton.Name = "movePreviousAdtButton";
            this.movePreviousAdtButton.Size = new System.Drawing.Size(23, 22);
            this.movePreviousAdtButton.Text = "Move Previous";
            this.movePreviousAdtButton.Click += new System.EventHandler(this.adtPnlNavButtons);
            // 
            // toolStripSeparator81
            // 
            this.toolStripSeparator81.Name = "toolStripSeparator81";
            this.toolStripSeparator81.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel17
            // 
            this.toolStripLabel17.AutoToolTip = true;
            this.toolStripLabel17.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel17.Name = "toolStripLabel17";
            this.toolStripLabel17.Size = new System.Drawing.Size(47, 22);
            this.toolStripLabel17.Text = "Record";
            // 
            // positionAdtTextBox
            // 
            this.positionAdtTextBox.AutoToolTip = true;
            this.positionAdtTextBox.BackColor = System.Drawing.Color.White;
            this.positionAdtTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.positionAdtTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.positionAdtTextBox.Name = "positionAdtTextBox";
            this.positionAdtTextBox.ReadOnly = true;
            this.positionAdtTextBox.Size = new System.Drawing.Size(70, 25);
            this.positionAdtTextBox.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.positionAdtTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.positionAdtTextBox_KeyDown);
            // 
            // totalRecAdtLabel
            // 
            this.totalRecAdtLabel.AutoToolTip = true;
            this.totalRecAdtLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalRecAdtLabel.Name = "totalRecAdtLabel";
            this.totalRecAdtLabel.Size = new System.Drawing.Size(50, 22);
            this.totalRecAdtLabel.Text = "of Total";
            // 
            // toolStripSeparator82
            // 
            this.toolStripSeparator82.Name = "toolStripSeparator82";
            this.toolStripSeparator82.Size = new System.Drawing.Size(6, 25);
            // 
            // moveNextAdtButton
            // 
            this.moveNextAdtButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveNextAdtButton.Image = global::SystemAdministration.Properties.Resources.DataContainer_MoveNextHS;
            this.moveNextAdtButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveNextAdtButton.Name = "moveNextAdtButton";
            this.moveNextAdtButton.Size = new System.Drawing.Size(23, 22);
            this.moveNextAdtButton.Text = "Move Next";
            this.moveNextAdtButton.Click += new System.EventHandler(this.adtPnlNavButtons);
            // 
            // toolStripSeparator83
            // 
            this.toolStripSeparator83.Name = "toolStripSeparator83";
            this.toolStripSeparator83.Size = new System.Drawing.Size(6, 25);
            // 
            // moveLastAdtButton
            // 
            this.moveLastAdtButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveLastAdtButton.Image = global::SystemAdministration.Properties.Resources.DataContainer_MoveLastHS;
            this.moveLastAdtButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveLastAdtButton.Name = "moveLastAdtButton";
            this.moveLastAdtButton.Size = new System.Drawing.Size(23, 22);
            this.moveLastAdtButton.Text = "Move Last";
            this.moveLastAdtButton.Click += new System.EventHandler(this.adtPnlNavButtons);
            // 
            // toolStripSeparator84
            // 
            this.toolStripSeparator84.Name = "toolStripSeparator84";
            this.toolStripSeparator84.Size = new System.Drawing.Size(6, 25);
            // 
            // dsplySizeAdtComboBox
            // 
            this.dsplySizeAdtComboBox.AutoSize = false;
            this.dsplySizeAdtComboBox.Items.AddRange(new object[] {
            "1",
            "5",
            "10",
            "15",
            "20",
            "30",
            "40",
            "50",
            "100"});
            this.dsplySizeAdtComboBox.Name = "dsplySizeAdtComboBox";
            this.dsplySizeAdtComboBox.Size = new System.Drawing.Size(40, 23);
            this.dsplySizeAdtComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForAdtTextBox_KeyDown);
            // 
            // toolStripSeparator86
            // 
            this.toolStripSeparator86.Name = "toolStripSeparator86";
            this.toolStripSeparator86.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel19
            // 
            this.toolStripLabel19.Name = "toolStripLabel19";
            this.toolStripLabel19.Size = new System.Drawing.Size(65, 22);
            this.toolStripLabel19.Text = "Search For:";
            // 
            // toolStripSeparator88
            // 
            this.toolStripSeparator88.Name = "toolStripSeparator88";
            this.toolStripSeparator88.Size = new System.Drawing.Size(6, 25);
            // 
            // searchForAdtTextBox
            // 
            this.searchForAdtTextBox.Name = "searchForAdtTextBox";
            this.searchForAdtTextBox.Size = new System.Drawing.Size(100, 25);
            this.searchForAdtTextBox.Enter += new System.EventHandler(this.searchForAdtTextBox_Click);
            this.searchForAdtTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForAdtTextBox_KeyDown);
            this.searchForAdtTextBox.Click += new System.EventHandler(this.searchForAdtTextBox_Click);
            // 
            // toolStripSeparator89
            // 
            this.toolStripSeparator89.Name = "toolStripSeparator89";
            this.toolStripSeparator89.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel20
            // 
            this.toolStripLabel20.Name = "toolStripLabel20";
            this.toolStripLabel20.Size = new System.Drawing.Size(58, 22);
            this.toolStripLabel20.Text = "Search In:";
            // 
            // toolStripSeparator90
            // 
            this.toolStripSeparator90.Name = "toolStripSeparator90";
            this.toolStripSeparator90.Size = new System.Drawing.Size(6, 25);
            // 
            // searchInAdtComboBox
            // 
            this.searchInAdtComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.searchInAdtComboBox.Items.AddRange(new object[] {
            "Action Details",
            "Action Type",
            "Date/Time",
            "Login Number",
            "Machine Used",
            "User Name"});
            this.searchInAdtComboBox.Name = "searchInAdtComboBox";
            this.searchInAdtComboBox.Size = new System.Drawing.Size(121, 25);
            this.searchInAdtComboBox.Sorted = true;
            this.searchInAdtComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForAdtTextBox_KeyDown);
            // 
            // toolStripSeparator91
            // 
            this.toolStripSeparator91.Name = "toolStripSeparator91";
            this.toolStripSeparator91.Size = new System.Drawing.Size(6, 25);
            // 
            // refreshAdtButton
            // 
            this.refreshAdtButton.Image = global::SystemAdministration.Properties.Resources.action_go;
            this.refreshAdtButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshAdtButton.Name = "refreshAdtButton";
            this.refreshAdtButton.Size = new System.Drawing.Size(42, 22);
            this.refreshAdtButton.Text = "Go";
            this.refreshAdtButton.Click += new System.EventHandler(this.refreshAdtButton_Click);
            // 
            // toolStripSeparator17
            // 
            this.toolStripSeparator17.Name = "toolStripSeparator17";
            this.toolStripSeparator17.Size = new System.Drawing.Size(6, 25);
            // 
            // vwSQLAdtButton
            // 
            this.vwSQLAdtButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.vwSQLAdtButton.Image = global::SystemAdministration.Properties.Resources.SQL;
            this.vwSQLAdtButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.vwSQLAdtButton.Name = "vwSQLAdtButton";
            this.vwSQLAdtButton.Size = new System.Drawing.Size(23, 22);
            this.vwSQLAdtButton.Text = "View SQL";
            this.vwSQLAdtButton.Click += new System.EventHandler(this.vwSQLAdtButton_Click);
            // 
            // toolStripSeparator95
            // 
            this.toolStripSeparator95.Name = "toolStripSeparator95";
            this.toolStripSeparator95.Size = new System.Drawing.Size(6, 25);
            // 
            // auditTblsTreeView
            // 
            this.auditTblsTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.auditTblsTreeView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.auditTblsTreeView.FullRowSelect = true;
            this.auditTblsTreeView.HideSelection = false;
            this.auditTblsTreeView.ImageKey = "features_audittrail_icon.jpg";
            this.auditTblsTreeView.ImageList = this.imageList1;
            this.auditTblsTreeView.Location = new System.Drawing.Point(813, 69);
            this.auditTblsTreeView.MinimumSize = new System.Drawing.Size(199, 250);
            this.auditTblsTreeView.Name = "auditTblsTreeView";
            this.auditTblsTreeView.SelectedImageIndex = 8;
            this.auditTblsTreeView.Size = new System.Drawing.Size(200, 577);
            this.auditTblsTreeView.TabIndex = 82;
            this.auditTblsTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.auditTblsTreeView_AfterSelect);
            // 
            // auditTblsDataGridView
            // 
            this.auditTblsDataGridView.AllowUserToAddRows = false;
            this.auditTblsDataGridView.AllowUserToDeleteRows = false;
            this.auditTblsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.auditTblsDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.auditTblsDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.auditTblsDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.auditTblsDataGridView.ColumnHeadersHeight = 60;
            this.auditTblsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column1,
            this.Column7});
            this.auditTblsDataGridView.ContextMenuStrip = this.auditContextMenuStrip;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.auditTblsDataGridView.DefaultCellStyle = dataGridViewCellStyle5;
            this.auditTblsDataGridView.Location = new System.Drawing.Point(2, 29);
            this.auditTblsDataGridView.MinimumSize = new System.Drawing.Size(300, 300);
            this.auditTblsDataGridView.Name = "auditTblsDataGridView";
            this.auditTblsDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.auditTblsDataGridView.RowHeadersWidth = 71;
            this.auditTblsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.auditTblsDataGridView.Size = new System.Drawing.Size(806, 617);
            this.auditTblsDataGridView.TabIndex = 1;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "USER NAME";
            this.Column2.Name = "Column2";
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column2.Width = 80;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "ACTION TYPE";
            this.Column3.Name = "Column3";
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column3.Width = 80;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "ACTION DETAILS";
            this.Column4.Name = "Column4";
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column4.Width = 320;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "ACTION DATE/TIME";
            this.Column5.Name = "Column5";
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column5.Width = 80;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "MACHINE USED";
            this.Column6.Name = "Column6";
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column6.Width = 90;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "USER_ID";
            this.Column1.Name = "Column1";
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column1.Visible = false;
            this.Column1.Width = 5;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "LOGIN NUMBER";
            this.Column7.Name = "Column7";
            this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column7.Width = 60;
            // 
            // auditContextMenuStrip
            // 
            this.auditContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exptAudtMenuItem,
            this.refreshAdtMenuItem,
            this.toolStripSeparator94,
            this.vwSQLAdtMenuItem});
            this.auditContextMenuStrip.Name = "usersContextMenuStrip";
            this.auditContextMenuStrip.Size = new System.Drawing.Size(151, 76);
            // 
            // exptAudtMenuItem
            // 
            this.exptAudtMenuItem.Image = global::SystemAdministration.Properties.Resources.image007;
            this.exptAudtMenuItem.Name = "exptAudtMenuItem";
            this.exptAudtMenuItem.Size = new System.Drawing.Size(150, 22);
            this.exptAudtMenuItem.Text = "Export to Excel";
            this.exptAudtMenuItem.Click += new System.EventHandler(this.exptAudtMenuItem_Click);
            // 
            // refreshAdtMenuItem
            // 
            this.refreshAdtMenuItem.Image = global::SystemAdministration.Properties.Resources.refresh;
            this.refreshAdtMenuItem.Name = "refreshAdtMenuItem";
            this.refreshAdtMenuItem.Size = new System.Drawing.Size(150, 22);
            this.refreshAdtMenuItem.Text = "&Refresh";
            this.refreshAdtMenuItem.Click += new System.EventHandler(this.refreshAdtMenuItem_Click);
            // 
            // toolStripSeparator94
            // 
            this.toolStripSeparator94.Name = "toolStripSeparator94";
            this.toolStripSeparator94.Size = new System.Drawing.Size(147, 6);
            // 
            // vwSQLAdtMenuItem
            // 
            this.vwSQLAdtMenuItem.Image = global::SystemAdministration.Properties.Resources.SQL;
            this.vwSQLAdtMenuItem.Name = "vwSQLAdtMenuItem";
            this.vwSQLAdtMenuItem.Size = new System.Drawing.Size(150, 22);
            this.vwSQLAdtMenuItem.Text = "&View SQL";
            this.vwSQLAdtMenuItem.Click += new System.EventHandler(this.vwSQLAdtMenuItem_Click);
            // 
            // panel17
            // 
            this.panel17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel17.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel17.Controls.Add(this.glsLabel16);
            this.panel17.Location = new System.Drawing.Point(814, 29);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(199, 39);
            this.panel17.TabIndex = 84;
            // 
            // glsLabel16
            // 
            this.glsLabel16.BottomFill = System.Drawing.Color.Silver;
            this.glsLabel16.Caption = "LIST OF TABLES";
            this.glsLabel16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glsLabel16.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.glsLabel16.ForeColor = System.Drawing.Color.White;
            this.glsLabel16.Location = new System.Drawing.Point(0, 0);
            this.glsLabel16.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.glsLabel16.Name = "glsLabel16";
            this.glsLabel16.Size = new System.Drawing.Size(195, 35);
            this.glsLabel16.TabIndex = 1;
            this.glsLabel16.TopFill = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            // 
            // label34
            // 
            this.label34.Location = new System.Drawing.Point(400, 400);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(207, 55);
            this.label34.TabIndex = 83;
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.loginsPanel);
            this.tabPage8.ImageKey = "54.png";
            this.tabPage8.Location = new System.Drawing.Point(4, 60);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage8.Size = new System.Drawing.Size(1025, 655);
            this.tabPage8.TabIndex = 7;
            this.tabPage8.Text = "TRACK USER LOGINS";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // loginsPanel
            // 
            this.loginsPanel.AutoScroll = true;
            this.loginsPanel.BackColor = System.Drawing.Color.LightSlateGray;
            this.loginsPanel.Controls.Add(this.loginsListView);
            this.loginsPanel.Controls.Add(this.toolStrip4);
            this.loginsPanel.Controls.Add(this.label35);
            this.loginsPanel.Controls.Add(this.showFaildCheckBox);
            this.loginsPanel.Controls.Add(this.showSuccsflCheckBox);
            this.loginsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loginsPanel.Enabled = false;
            this.loginsPanel.Location = new System.Drawing.Point(3, 3);
            this.loginsPanel.Name = "loginsPanel";
            this.loginsPanel.Size = new System.Drawing.Size(1019, 649);
            this.loginsPanel.TabIndex = 1;
            this.loginsPanel.Visible = false;
            // 
            // loginsListView
            // 
            this.loginsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loginsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader38,
            this.columnHeader39,
            this.columnHeader40,
            this.columnHeader41,
            this.columnHeader42,
            this.columnHeader45,
            this.columnHeader43,
            this.columnHeader44});
            this.loginsListView.ContextMenuStrip = this.loginsContextMenuStrip;
            this.loginsListView.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginsListView.FullRowSelect = true;
            this.loginsListView.GridLines = true;
            this.loginsListView.HideSelection = false;
            this.loginsListView.Location = new System.Drawing.Point(3, 28);
            this.loginsListView.MinimumSize = new System.Drawing.Size(300, 300);
            this.loginsListView.Name = "loginsListView";
            this.loginsListView.Size = new System.Drawing.Size(1013, 617);
            this.loginsListView.TabIndex = 1;
            this.loginsListView.UseCompatibleStateImageBehavior = false;
            this.loginsListView.View = System.Windows.Forms.View.Details;
            this.loginsListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.loginsListView_KeyDown);
            // 
            // columnHeader38
            // 
            this.columnHeader38.Text = "No.";
            this.columnHeader38.Width = 45;
            // 
            // columnHeader39
            // 
            this.columnHeader39.Text = "User Name";
            this.columnHeader39.Width = 100;
            // 
            // columnHeader40
            // 
            this.columnHeader40.Text = "Login Time";
            this.columnHeader40.Width = 120;
            // 
            // columnHeader41
            // 
            this.columnHeader41.Text = "Logout Time";
            this.columnHeader41.Width = 120;
            // 
            // columnHeader42
            // 
            this.columnHeader42.Text = "Machine Details";
            this.columnHeader42.Width = 200;
            // 
            // columnHeader45
            // 
            this.columnHeader45.Text = "Was Login Attempt Successful?";
            this.columnHeader45.Width = 172;
            // 
            // columnHeader43
            // 
            this.columnHeader43.Text = "USER_ID";
            this.columnHeader43.Width = 0;
            // 
            // columnHeader44
            // 
            this.columnHeader44.Text = "Login Number";
            this.columnHeader44.Width = 100;
            // 
            // loginsContextMenuStrip
            // 
            this.loginsContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exptLgnMenuItem,
            this.refreshLgnMenuItem,
            this.toolStripSeparator93,
            this.vwSQLLgnMenuItem});
            this.loginsContextMenuStrip.Name = "usersContextMenuStrip";
            this.loginsContextMenuStrip.Size = new System.Drawing.Size(151, 76);
            // 
            // exptLgnMenuItem
            // 
            this.exptLgnMenuItem.Image = global::SystemAdministration.Properties.Resources.image007;
            this.exptLgnMenuItem.Name = "exptLgnMenuItem";
            this.exptLgnMenuItem.Size = new System.Drawing.Size(150, 22);
            this.exptLgnMenuItem.Text = "Export to Excel";
            this.exptLgnMenuItem.Click += new System.EventHandler(this.exptLgnMenuItem_Click);
            // 
            // refreshLgnMenuItem
            // 
            this.refreshLgnMenuItem.Image = global::SystemAdministration.Properties.Resources.refresh;
            this.refreshLgnMenuItem.Name = "refreshLgnMenuItem";
            this.refreshLgnMenuItem.Size = new System.Drawing.Size(150, 22);
            this.refreshLgnMenuItem.Text = "&Refresh";
            this.refreshLgnMenuItem.Click += new System.EventHandler(this.refreshLgnMenuItem_Click);
            // 
            // toolStripSeparator93
            // 
            this.toolStripSeparator93.Name = "toolStripSeparator93";
            this.toolStripSeparator93.Size = new System.Drawing.Size(147, 6);
            // 
            // vwSQLLgnMenuItem
            // 
            this.vwSQLLgnMenuItem.Image = global::SystemAdministration.Properties.Resources.SQL;
            this.vwSQLLgnMenuItem.Name = "vwSQLLgnMenuItem";
            this.vwSQLLgnMenuItem.Size = new System.Drawing.Size(150, 22);
            this.vwSQLLgnMenuItem.Text = "&View SQL";
            this.vwSQLLgnMenuItem.Click += new System.EventHandler(this.vwSQLLgnMenuItem_Click);
            // 
            // toolStrip4
            // 
            this.toolStrip4.AutoSize = false;
            this.toolStrip4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.toolStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moveFirstLgnsButton,
            this.toolStripSeparator62,
            this.movePreviousLgnsButton,
            this.toolStripSeparator63,
            this.toolStripLabel11,
            this.positionLgnsTextBox,
            this.totalRecLgnsLabel,
            this.toolStripSeparator64,
            this.moveNextLgnsButton,
            this.toolStripSeparator65,
            this.moveLastLgnsButton,
            this.toolStripSeparator66,
            this.dsplySizeLgnsComboBox,
            this.toolStripSeparator68,
            this.toolStripLabel15,
            this.toolStripSeparator70,
            this.searchForLgnsTextBox,
            this.toolStripSeparator71,
            this.toolStripLabel16,
            this.toolStripSeparator72,
            this.searchInLgnsComboBox,
            this.toolStripSeparator73,
            this.refreshLgnsButton,
            this.toolStripSeparator15,
            this.vwSQLLgnsButton,
            this.toolStripSeparator77});
            this.toolStrip4.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip4.Location = new System.Drawing.Point(0, 0);
            this.toolStrip4.Name = "toolStrip4";
            this.toolStrip4.Size = new System.Drawing.Size(1019, 25);
            this.toolStrip4.Stretch = true;
            this.toolStrip4.TabIndex = 0;
            this.toolStrip4.TabStop = true;
            this.toolStrip4.Text = "ToolStrip2";
            // 
            // moveFirstLgnsButton
            // 
            this.moveFirstLgnsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveFirstLgnsButton.Image = global::SystemAdministration.Properties.Resources.DataContainer_MoveFirstHS;
            this.moveFirstLgnsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveFirstLgnsButton.Name = "moveFirstLgnsButton";
            this.moveFirstLgnsButton.Size = new System.Drawing.Size(23, 22);
            this.moveFirstLgnsButton.Text = "Move First";
            this.moveFirstLgnsButton.Click += new System.EventHandler(this.lgnsPnlNavButtons);
            // 
            // toolStripSeparator62
            // 
            this.toolStripSeparator62.Name = "toolStripSeparator62";
            this.toolStripSeparator62.Size = new System.Drawing.Size(6, 25);
            // 
            // movePreviousLgnsButton
            // 
            this.movePreviousLgnsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.movePreviousLgnsButton.Image = global::SystemAdministration.Properties.Resources.DataContainer_MovePreviousHS;
            this.movePreviousLgnsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.movePreviousLgnsButton.Name = "movePreviousLgnsButton";
            this.movePreviousLgnsButton.Size = new System.Drawing.Size(23, 22);
            this.movePreviousLgnsButton.Text = "Move Previous";
            this.movePreviousLgnsButton.Click += new System.EventHandler(this.lgnsPnlNavButtons);
            // 
            // toolStripSeparator63
            // 
            this.toolStripSeparator63.Name = "toolStripSeparator63";
            this.toolStripSeparator63.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel11
            // 
            this.toolStripLabel11.AutoToolTip = true;
            this.toolStripLabel11.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel11.Name = "toolStripLabel11";
            this.toolStripLabel11.Size = new System.Drawing.Size(47, 22);
            this.toolStripLabel11.Text = "Record";
            // 
            // positionLgnsTextBox
            // 
            this.positionLgnsTextBox.AutoToolTip = true;
            this.positionLgnsTextBox.BackColor = System.Drawing.Color.White;
            this.positionLgnsTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.positionLgnsTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.positionLgnsTextBox.Name = "positionLgnsTextBox";
            this.positionLgnsTextBox.ReadOnly = true;
            this.positionLgnsTextBox.Size = new System.Drawing.Size(70, 25);
            this.positionLgnsTextBox.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.positionLgnsTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.positionLgnsTextBox_KeyDown);
            // 
            // totalRecLgnsLabel
            // 
            this.totalRecLgnsLabel.AutoToolTip = true;
            this.totalRecLgnsLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalRecLgnsLabel.Name = "totalRecLgnsLabel";
            this.totalRecLgnsLabel.Size = new System.Drawing.Size(50, 22);
            this.totalRecLgnsLabel.Text = "of Total";
            // 
            // toolStripSeparator64
            // 
            this.toolStripSeparator64.Name = "toolStripSeparator64";
            this.toolStripSeparator64.Size = new System.Drawing.Size(6, 25);
            // 
            // moveNextLgnsButton
            // 
            this.moveNextLgnsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveNextLgnsButton.Image = global::SystemAdministration.Properties.Resources.DataContainer_MoveNextHS;
            this.moveNextLgnsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveNextLgnsButton.Name = "moveNextLgnsButton";
            this.moveNextLgnsButton.Size = new System.Drawing.Size(23, 22);
            this.moveNextLgnsButton.Text = "Move Next";
            this.moveNextLgnsButton.Click += new System.EventHandler(this.lgnsPnlNavButtons);
            // 
            // toolStripSeparator65
            // 
            this.toolStripSeparator65.Name = "toolStripSeparator65";
            this.toolStripSeparator65.Size = new System.Drawing.Size(6, 25);
            // 
            // moveLastLgnsButton
            // 
            this.moveLastLgnsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveLastLgnsButton.Image = global::SystemAdministration.Properties.Resources.DataContainer_MoveLastHS;
            this.moveLastLgnsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveLastLgnsButton.Name = "moveLastLgnsButton";
            this.moveLastLgnsButton.Size = new System.Drawing.Size(23, 22);
            this.moveLastLgnsButton.Text = "Move Last";
            this.moveLastLgnsButton.Click += new System.EventHandler(this.lgnsPnlNavButtons);
            // 
            // toolStripSeparator66
            // 
            this.toolStripSeparator66.Name = "toolStripSeparator66";
            this.toolStripSeparator66.Size = new System.Drawing.Size(6, 25);
            // 
            // dsplySizeLgnsComboBox
            // 
            this.dsplySizeLgnsComboBox.AutoSize = false;
            this.dsplySizeLgnsComboBox.Items.AddRange(new object[] {
            "1",
            "5",
            "10",
            "15",
            "20",
            "30",
            "40",
            "50",
            "100"});
            this.dsplySizeLgnsComboBox.Name = "dsplySizeLgnsComboBox";
            this.dsplySizeLgnsComboBox.Size = new System.Drawing.Size(40, 23);
            this.dsplySizeLgnsComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForLgnsTextBox_KeyDown);
            // 
            // toolStripSeparator68
            // 
            this.toolStripSeparator68.Name = "toolStripSeparator68";
            this.toolStripSeparator68.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel15
            // 
            this.toolStripLabel15.Name = "toolStripLabel15";
            this.toolStripLabel15.Size = new System.Drawing.Size(65, 22);
            this.toolStripLabel15.Text = "Search For:";
            // 
            // toolStripSeparator70
            // 
            this.toolStripSeparator70.Name = "toolStripSeparator70";
            this.toolStripSeparator70.Size = new System.Drawing.Size(6, 25);
            // 
            // searchForLgnsTextBox
            // 
            this.searchForLgnsTextBox.Name = "searchForLgnsTextBox";
            this.searchForLgnsTextBox.Size = new System.Drawing.Size(100, 25);
            this.searchForLgnsTextBox.Text = "%";
            this.searchForLgnsTextBox.Enter += new System.EventHandler(this.searchForLgnsTextBox_Click);
            this.searchForLgnsTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForLgnsTextBox_KeyDown);
            this.searchForLgnsTextBox.Click += new System.EventHandler(this.searchForLgnsTextBox_Click);
            // 
            // toolStripSeparator71
            // 
            this.toolStripSeparator71.Name = "toolStripSeparator71";
            this.toolStripSeparator71.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel16
            // 
            this.toolStripLabel16.Name = "toolStripLabel16";
            this.toolStripLabel16.Size = new System.Drawing.Size(58, 22);
            this.toolStripLabel16.Text = "Search In:";
            // 
            // toolStripSeparator72
            // 
            this.toolStripSeparator72.Name = "toolStripSeparator72";
            this.toolStripSeparator72.Size = new System.Drawing.Size(6, 25);
            // 
            // searchInLgnsComboBox
            // 
            this.searchInLgnsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.searchInLgnsComboBox.Items.AddRange(new object[] {
            "Login Number",
            "Login Time",
            "Logout Time",
            "Machine Details",
            "User Name"});
            this.searchInLgnsComboBox.Name = "searchInLgnsComboBox";
            this.searchInLgnsComboBox.Size = new System.Drawing.Size(121, 25);
            this.searchInLgnsComboBox.Sorted = true;
            this.searchInLgnsComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForLgnsTextBox_KeyDown);
            // 
            // toolStripSeparator73
            // 
            this.toolStripSeparator73.Name = "toolStripSeparator73";
            this.toolStripSeparator73.Size = new System.Drawing.Size(6, 25);
            // 
            // refreshLgnsButton
            // 
            this.refreshLgnsButton.Image = global::SystemAdministration.Properties.Resources.action_go;
            this.refreshLgnsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshLgnsButton.Name = "refreshLgnsButton";
            this.refreshLgnsButton.Size = new System.Drawing.Size(42, 22);
            this.refreshLgnsButton.Text = "Go";
            this.refreshLgnsButton.Click += new System.EventHandler(this.refreshLgnsButton_Click);
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(6, 25);
            // 
            // vwSQLLgnsButton
            // 
            this.vwSQLLgnsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.vwSQLLgnsButton.Image = global::SystemAdministration.Properties.Resources.SQL;
            this.vwSQLLgnsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.vwSQLLgnsButton.Name = "vwSQLLgnsButton";
            this.vwSQLLgnsButton.Size = new System.Drawing.Size(23, 22);
            this.vwSQLLgnsButton.Text = "View SQL";
            this.vwSQLLgnsButton.Click += new System.EventHandler(this.vwSQLLgnsButton_Click);
            // 
            // toolStripSeparator77
            // 
            this.toolStripSeparator77.Name = "toolStripSeparator77";
            this.toolStripSeparator77.Size = new System.Drawing.Size(6, 25);
            // 
            // label35
            // 
            this.label35.Location = new System.Drawing.Point(400, 400);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(131, 23);
            this.label35.TabIndex = 85;
            // 
            // showFaildCheckBox
            // 
            this.showFaildCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.showFaildCheckBox.AutoSize = true;
            this.showFaildCheckBox.Checked = true;
            this.showFaildCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showFaildCheckBox.ForeColor = System.Drawing.Color.White;
            this.showFaildCheckBox.Location = new System.Drawing.Point(8816, 99);
            this.showFaildCheckBox.Name = "showFaildCheckBox";
            this.showFaildCheckBox.Size = new System.Drawing.Size(116, 17);
            this.showFaildCheckBox.TabIndex = 83;
            this.showFaildCheckBox.Text = "Show Failed Logins";
            this.showFaildCheckBox.UseVisualStyleBackColor = true;
            this.showFaildCheckBox.CheckedChanged += new System.EventHandler(this.showFaildCheckBox_CheckedChanged);
            // 
            // showSuccsflCheckBox
            // 
            this.showSuccsflCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.showSuccsflCheckBox.AutoSize = true;
            this.showSuccsflCheckBox.Checked = true;
            this.showSuccsflCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showSuccsflCheckBox.ForeColor = System.Drawing.Color.White;
            this.showSuccsflCheckBox.Location = new System.Drawing.Point(8818, 73);
            this.showSuccsflCheckBox.Name = "showSuccsflCheckBox";
            this.showSuccsflCheckBox.Size = new System.Drawing.Size(138, 17);
            this.showSuccsflCheckBox.TabIndex = 82;
            this.showSuccsflCheckBox.Text = "Show Successful Logins";
            this.showSuccsflCheckBox.UseVisualStyleBackColor = true;
            this.showSuccsflCheckBox.CheckedChanged += new System.EventHandler(this.showSuccsflCheckBox_CheckedChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // infoToolTip
            // 
            this.infoToolTip.AutomaticDelay = 50;
            this.infoToolTip.AutoPopDelay = 5000;
            this.infoToolTip.InitialDelay = 50;
            this.infoToolTip.IsBalloon = true;
            this.infoToolTip.ReshowDelay = 10;
            this.infoToolTip.ShowAlways = true;
            this.infoToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.infoToolTip.ToolTipTitle = "Rhomicom Hint!";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // deleteSrvrButton
            // 
            this.deleteSrvrButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteSrvrButton.Image = global::SystemAdministration.Properties.Resources.delete;
            this.deleteSrvrButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteSrvrButton.Name = "deleteSrvrButton";
            this.deleteSrvrButton.Size = new System.Drawing.Size(23, 22);
            this.deleteSrvrButton.Text = "DELETE";
            this.deleteSrvrButton.Click += new System.EventHandler(this.deleteSrvrButton_Click);
            // 
            // toolStripSeparator52
            // 
            this.toolStripSeparator52.Name = "toolStripSeparator52";
            this.toolStripSeparator52.Size = new System.Drawing.Size(6, 25);
            // 
            // deletePolicyButton
            // 
            this.deletePolicyButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deletePolicyButton.Image = global::SystemAdministration.Properties.Resources.delete;
            this.deletePolicyButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deletePolicyButton.Name = "deletePolicyButton";
            this.deletePolicyButton.Size = new System.Drawing.Size(23, 22);
            this.deletePolicyButton.Text = "DELETE";
            this.deletePolicyButton.Click += new System.EventHandler(this.deletePolicyButton_Click);
            // 
            // toolStripSeparator60
            // 
            this.toolStripSeparator60.Name = "toolStripSeparator60";
            this.toolStripSeparator60.Size = new System.Drawing.Size(6, 25);
            // 
            // mainForm
            // 
            this.BackColor = System.Drawing.Color.LightSlateGray;
            this.ClientSize = new System.Drawing.Size(1276, 733);
            this.Controls.Add(this.splitContainer1);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "mainForm";
            this.TabText = "System Administration";
            this.Text = "System Administration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainForm_FormClosing);
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.treeVWContextMenuStrip.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.usersPanel.ResumeLayout(false);
            this.toolStrip8.ResumeLayout(false);
            this.toolStrip8.PerformLayout();
            this.toolStrip9.ResumeLayout(false);
            this.toolStrip9.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.userRolesContextMenuStrip.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.navToolStrip.ResumeLayout(false);
            this.navToolStrip.PerformLayout();
            this.usersContextMenuStrip.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.rolesPanel.ResumeLayout(false);
            this.toolStrip10.ResumeLayout(false);
            this.toolStrip10.PerformLayout();
            this.toolStrip11.ResumeLayout(false);
            this.toolStrip11.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.rolesPrvlgsContextMenuStrip.ResumeLayout(false);
            this.rolesContextMenuStrip.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.modulesPanel.ResumeLayout(false);
            this.modulePrvlgContextMenuStrip.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.modulesContextMenuStrip.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.extraInfoPanel.ResumeLayout(false);
            this.toolStrip12.ResumeLayout(false);
            this.toolStrip12.PerformLayout();
            this.extInfLabelContextMenuStrip.ResumeLayout(false);
            this.panel23.ResumeLayout(false);
            this.subGroupsContextMenuStrip.ResumeLayout(false);
            this.panel20.ResumeLayout(false);
            this.extInfMdlContextMenuStrip.ResumeLayout(false);
            this.panel21.ResumeLayout(false);
            this.toolStrip7.ResumeLayout(false);
            this.toolStrip7.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.policyPanel.ResumeLayout(false);
            this.plcyMdlsContextMenuStrip.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sessionNumUpDown)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.autoUnlkTmNmUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.faildLgnCntNmUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxLenPswdNmUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.oldPswdCntNmUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minLenPswdNumericUpDown)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mxNoRecsNmUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.expryDaysNmUpDown)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.emailServerPanel.ResumeLayout(false);
            this.emailServerPanel.PerformLayout();
            this.groupBox13.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smsDataGridView)).EndInit();
            this.toolStrip6.ResumeLayout(false);
            this.toolStrip6.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ftpPortNumUpDown)).EndInit();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smtpPortNmUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.curOrgPictureBox)).EndInit();
            this.tabPage7.ResumeLayout(false);
            this.auditPanel.ResumeLayout(false);
            this.toolStrip5.ResumeLayout(false);
            this.toolStrip5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.auditTblsDataGridView)).EndInit();
            this.auditContextMenuStrip.ResumeLayout(false);
            this.panel17.ResumeLayout(false);
            this.tabPage8.ResumeLayout(false);
            this.loginsPanel.ResumeLayout(false);
            this.loginsPanel.PerformLayout();
            this.loginsContextMenuStrip.ResumeLayout(false);
            this.toolStrip4.ResumeLayout(false);
            this.toolStrip4.PerformLayout();
            this.ResumeLayout(false);

			}
#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Panel usersPanel;
		private System.Windows.Forms.TreeView leftTreeView;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.Panel panel2;
		private glsLabel.glsLabel glsLabel1;
		private System.Windows.Forms.Panel auditPanel;
		private System.Windows.Forms.Panel loginsPanel;
		private System.Windows.Forms.Panel policyPanel;
		private System.Windows.Forms.Panel modulesPanel;
    private System.Windows.Forms.Panel rolesPanel;
		private System.Windows.Forms.ListView userListView;
		private System.Windows.Forms.ToolStrip navToolStrip;
		internal System.Windows.Forms.ToolStripButton moveFirstUserButton;
		internal System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
		internal System.Windows.Forms.ToolStripButton movePreviousUserButton;
		internal System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
		internal System.Windows.Forms.ToolStripLabel ToolStripLabel2;
		internal System.Windows.Forms.ToolStripTextBox positionUserTextBox;
		internal System.Windows.Forms.ToolStripLabel totalRecUserLabel;
		internal System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
		internal System.Windows.Forms.ToolStripButton moveNextUserButton;
		internal System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
		internal System.Windows.Forms.ToolStripButton moveLastUserButton;
  internal System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
		private System.Windows.Forms.ToolStripComboBox dsplySizeUserComboBox;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator16;
  private System.Windows.Forms.ToolStripButton refreshUserButton;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator18;
		private System.Windows.Forms.ToolStripTextBox searchForUserTextBox;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator19;
		private System.Windows.Forms.ToolStripLabel toolStripLabel3;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator20;
		private System.Windows.Forms.ToolStripComboBox searchInUserComboBox;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator21;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox isLockedCheckBox;
		private System.Windows.Forms.CheckBox isSuspendedCheckBox;
		private System.Windows.Forms.TextBox failedLgnAtmptTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox lastLoginAtmptTextBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox usrVldEndDteTextBox;
		private System.Windows.Forms.TextBox usrVldStrtDteTextBox;
		private System.Windows.Forms.ListView userRoleslistView;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.ContextMenuStrip usersContextMenuStrip;
		private System.Windows.Forms.ContextMenuStrip userRolesContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem addUserToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editUserToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem refreshUsersToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem recordHistoryUsrsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addUserRoleToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem refreshUsrRoleToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem recordHistoryUsrRoleToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem viewSQLUserToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem viewSQLUsrRoleToolStripMenuItem;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.CheckBox isExpiredCheckBox;
		private System.Windows.Forms.TextBox lastPwdChngeTextBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox isTempCheckBox;
		private System.Windows.Forms.Button changePswdManButton;
		private System.Windows.Forms.TextBox agePswdTextBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button usrDte2Button;
		private System.Windows.Forms.Button usrDte1Button;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		private System.Windows.Forms.Button changePswdAutoButton;
		private System.Windows.Forms.Panel emailServerPanel;
		private System.Windows.Forms.ToolStrip toolStrip1;
		internal System.Windows.Forms.ToolStripButton moveFirstRoleButton;
		internal System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		internal System.Windows.Forms.ToolStripButton movePreviousRoleButton;
		internal System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		internal System.Windows.Forms.ToolStripLabel toolStripLabel4;
		internal System.Windows.Forms.ToolStripTextBox positionRoleTextBox;
		internal System.Windows.Forms.ToolStripLabel totalRecRoleLabel;
		internal System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		internal System.Windows.Forms.ToolStripButton moveNextRoleButton;
		internal System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		internal System.Windows.Forms.ToolStripButton moveLastRoleButton;
  internal System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
		private System.Windows.Forms.ToolStripComboBox dsplySizeRoleComboBox;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
  private System.Windows.Forms.ToolStripButton refreshRoleButton;
		private System.Windows.Forms.ToolStripLabel toolStripLabel6;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator23;
		private System.Windows.Forms.ToolStripTextBox searchForRoleTextBox;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator24;
		private System.Windows.Forms.ToolStripLabel toolStripLabel7;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator25;
		private System.Windows.Forms.ToolStripComboBox searchInRoleComboBox;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator26;
		private System.Windows.Forms.ListView rolePrvldgsListView;
		private System.Windows.Forms.ListView rolesListView;
		private System.Windows.Forms.Panel panel5;
		private glsLabel.glsLabel glsLabel4;
		private System.Windows.Forms.Panel panel6;
		private glsLabel.glsLabel glsLabel5;
		private System.Windows.Forms.ColumnHeader columnHeader11;
		private System.Windows.Forms.ColumnHeader columnHeader12;
		private System.Windows.Forms.ColumnHeader columnHeader13;
		private System.Windows.Forms.ColumnHeader columnHeader14;
		private System.Windows.Forms.ColumnHeader columnHeader15;
		private System.Windows.Forms.ColumnHeader columnHeader16;
		private System.Windows.Forms.ColumnHeader columnHeader17;
		private System.Windows.Forms.ColumnHeader columnHeader18;
		private System.Windows.Forms.ColumnHeader columnHeader19;
		private System.Windows.Forms.ColumnHeader columnHeader20;
		private System.Windows.Forms.ColumnHeader columnHeader21;
		private System.Windows.Forms.ColumnHeader columnHeader22;
		private System.Windows.Forms.ContextMenuStrip rolesContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem addRoleMainMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editRoleMainMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator27;
		private System.Windows.Forms.ToolStripMenuItem refreshRoleMainMenuItem;
		private System.Windows.Forms.ToolStripMenuItem recHstryRoleMainMenuItem;
		private System.Windows.Forms.ToolStripMenuItem vwSQLRoleMainMenuItem;
		private System.Windows.Forms.ContextMenuStrip rolesPrvlgsContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem addRlPrvldgMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator28;
		private System.Windows.Forms.ToolStripMenuItem refreshRlPrvldgMenuItem;
		private System.Windows.Forms.ToolStripMenuItem recHstryRlPrvldgMenuItem;
    private System.Windows.Forms.ToolStripMenuItem vwSQLRlPrvldgMenuItem;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ToolStrip toolStrip2;
		internal System.Windows.Forms.ToolStripButton moveFirstMdlButton;
		internal System.Windows.Forms.ToolStripSeparator toolStripSeparator29;
		internal System.Windows.Forms.ToolStripButton movePreviousMdlButton;
		internal System.Windows.Forms.ToolStripSeparator toolStripSeparator30;
		internal System.Windows.Forms.ToolStripLabel toolStripLabel5;
		internal System.Windows.Forms.ToolStripTextBox positionMdlTextBox;
		internal System.Windows.Forms.ToolStripLabel totalRecMdlLabel;
		internal System.Windows.Forms.ToolStripSeparator toolStripSeparator31;
		internal System.Windows.Forms.ToolStripButton moveNextMdlButton;
		internal System.Windows.Forms.ToolStripSeparator toolStripSeparator32;
  internal System.Windows.Forms.ToolStripButton moveLastMdlButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator34;
		private System.Windows.Forms.ToolStripComboBox dsplySizeMdlComboBox;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator35;
  private System.Windows.Forms.ToolStripButton refreshMdlButton;
		private System.Windows.Forms.ToolStripLabel toolStripLabel9;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator37;
		private System.Windows.Forms.ToolStripTextBox searchForMdlTextBox;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator38;
		private System.Windows.Forms.ToolStripLabel toolStripLabel10;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator39;
		private System.Windows.Forms.ToolStripComboBox searchInMdlComboBox;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator40;
		private System.Windows.Forms.ListView modulesListView;
		private System.Windows.Forms.ColumnHeader columnHeader23;
		private System.Windows.Forms.ColumnHeader columnHeader24;
		private System.Windows.Forms.ColumnHeader columnHeader27;
		private System.Windows.Forms.Panel panel10;
		private glsLabel.glsLabel glsLabel9;
		private System.Windows.Forms.Panel panel11;
		private glsLabel.glsLabel glsLabel10;
		private System.Windows.Forms.ListView modulePrvldgListView;
		private System.Windows.Forms.ColumnHeader columnHeader25;
		private System.Windows.Forms.ColumnHeader columnHeader26;
    private System.Windows.Forms.ColumnHeader columnHeader31;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.ColumnHeader columnHeader28;
		private System.Windows.Forms.ColumnHeader columnHeader29;
		private System.Windows.Forms.ColumnHeader columnHeader30;
		private System.Windows.Forms.ToolStrip toolStrip3;
		internal System.Windows.Forms.ToolStripButton moveFirstPlcyButton;
		internal System.Windows.Forms.ToolStripSeparator toolStripSeparator41;
		internal System.Windows.Forms.ToolStripButton movePreviousPlcyButton;
		internal System.Windows.Forms.ToolStripSeparator toolStripSeparator42;
		internal System.Windows.Forms.ToolStripLabel toolStripLabel8;
		internal System.Windows.Forms.ToolStripTextBox positionPlcyTextBox;
		internal System.Windows.Forms.ToolStripLabel totalRecPlcyLabel;
		internal System.Windows.Forms.ToolStripSeparator toolStripSeparator43;
		internal System.Windows.Forms.ToolStripButton moveNextPlcyButton;
		internal System.Windows.Forms.ToolStripSeparator toolStripSeparator44;
		internal System.Windows.Forms.ToolStripButton moveLastPlcyButton;
  internal System.Windows.Forms.ToolStripSeparator toolStripSeparator45;
  private System.Windows.Forms.ToolStripButton refreshPlcyButton;
		private System.Windows.Forms.ToolStripLabel toolStripLabel12;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator49;
		private System.Windows.Forms.ToolStripTextBox searchForPlcyTextBox;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator50;
		private System.Windows.Forms.ToolStripLabel toolStripLabel13;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator51;
    private System.Windows.Forms.ToolStripComboBox searchInPlcyComboBox;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox policyNmTextBox;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.NumericUpDown expryDaysNmUpDown;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.NumericUpDown mxNoRecsNmUpDown;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.GroupBox groupBox6;
		private System.Windows.Forms.NumericUpDown maxLenPswdNmUpDown;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.NumericUpDown oldPswdCntNmUpDown;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.NumericUpDown minLenPswdNumericUpDown;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.GroupBox groupBox7;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.ComboBox combinatnsComboBox;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.NumericUpDown autoUnlkTmNmUpDown;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.NumericUpDown faildLgnCntNmUpDown;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.ContextMenuStrip modulesContextMenuStrip;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator53;
		private System.Windows.Forms.ToolStripMenuItem refreshMdlMenuItem;
		private System.Windows.Forms.ToolStripMenuItem vwSQLMdlMenuItem;
		private System.Windows.Forms.ContextMenuStrip modulePrvlgContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem refreshMdlPrvldgMenuItem;
		private System.Windows.Forms.ToolStripMenuItem vwSqlMdlPrvldgMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator54;
		private System.Windows.Forms.CheckBox isDefltNoCheckBox;
		private System.Windows.Forms.CheckBox isDefltYesCheckBox;
		private System.Windows.Forms.CheckBox allwUnmNoCheckBox;
		private System.Windows.Forms.CheckBox allwUnmYesCheckBox;
		private System.Windows.Forms.CheckBox allwRptnNoCheckBox;
		private System.Windows.Forms.CheckBox allwRptnYesCheckBox;
		private System.Windows.Forms.CheckBox capsNoCheckBox;
		private System.Windows.Forms.CheckBox capsYesCheckBox;
		private System.Windows.Forms.CheckBox digitsNoCheckBox;
		private System.Windows.Forms.CheckBox digitsYesCheckBox;
		private System.Windows.Forms.CheckBox smallNoCheckBox;
		private System.Windows.Forms.CheckBox smallYesCheckBox;
		private System.Windows.Forms.CheckBox wildNoCheckBox;
		private System.Windows.Forms.CheckBox wildYesCheckBox;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.ListView auditTblsListView;
		private System.Windows.Forms.Label label27;
		private System.Windows.Forms.ColumnHeader columnHeader32;
		private System.Windows.Forms.ColumnHeader columnHeader33;
		private System.Windows.Forms.ColumnHeader columnHeader34;
		private System.Windows.Forms.ColumnHeader columnHeader35;
		private System.Windows.Forms.ColumnHeader columnHeader36;
		private System.Windows.Forms.TextBox plcyIDTextBox;
		private System.Windows.Forms.ColumnHeader columnHeader37;
		private System.Windows.Forms.ToolStripButton addPlcyButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator55;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator56;
		private System.Windows.Forms.ToolStripButton savePlcyButton;
		private System.Windows.Forms.ToolStripButton editPlcyButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator57;
		private System.Windows.Forms.ToolStripButton vwSQLPlcyButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator58;
		private System.Windows.Forms.ToolStripButton recHstryPlcyButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator59;
		private System.Windows.Forms.ContextMenuStrip plcyMdlsContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem editPlcyMdlMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator61;
		private System.Windows.Forms.ToolStripMenuItem refreshPlcyMdlsMenuItem;
		private System.Windows.Forms.ToolStripMenuItem recHstryPlcyMdlsMenuItem;
		private System.Windows.Forms.ToolStripMenuItem vwSQLPlcyMdlsMenuItem;
		private System.Windows.Forms.ToolStrip toolStrip4;
		internal System.Windows.Forms.ToolStripButton moveFirstLgnsButton;
		internal System.Windows.Forms.ToolStripSeparator toolStripSeparator62;
		internal System.Windows.Forms.ToolStripButton movePreviousLgnsButton;
		internal System.Windows.Forms.ToolStripSeparator toolStripSeparator63;
		internal System.Windows.Forms.ToolStripLabel toolStripLabel11;
		internal System.Windows.Forms.ToolStripTextBox positionLgnsTextBox;
		internal System.Windows.Forms.ToolStripLabel totalRecLgnsLabel;
		internal System.Windows.Forms.ToolStripSeparator toolStripSeparator64;
		internal System.Windows.Forms.ToolStripButton moveNextLgnsButton;
		internal System.Windows.Forms.ToolStripSeparator toolStripSeparator65;
		internal System.Windows.Forms.ToolStripButton moveLastLgnsButton;
  internal System.Windows.Forms.ToolStripSeparator toolStripSeparator66;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator68;
  private System.Windows.Forms.ToolStripButton refreshLgnsButton;
		private System.Windows.Forms.ToolStripLabel toolStripLabel15;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator70;
		private System.Windows.Forms.ToolStripTextBox searchForLgnsTextBox;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator71;
		private System.Windows.Forms.ToolStripLabel toolStripLabel16;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator72;
		private System.Windows.Forms.ToolStripComboBox searchInLgnsComboBox;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator73;
		private System.Windows.Forms.ToolStripButton vwSQLLgnsButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator77;
		private System.Windows.Forms.ToolStrip toolStrip5;
		internal System.Windows.Forms.ToolStripButton moveFirstAdtButton;
		internal System.Windows.Forms.ToolStripSeparator toolStripSeparator80;
		internal System.Windows.Forms.ToolStripButton movePreviousAdtButton;
		internal System.Windows.Forms.ToolStripSeparator toolStripSeparator81;
		internal System.Windows.Forms.ToolStripLabel toolStripLabel17;
		internal System.Windows.Forms.ToolStripTextBox positionAdtTextBox;
		internal System.Windows.Forms.ToolStripLabel totalRecAdtLabel;
		internal System.Windows.Forms.ToolStripSeparator toolStripSeparator82;
		internal System.Windows.Forms.ToolStripButton moveNextAdtButton;
		internal System.Windows.Forms.ToolStripSeparator toolStripSeparator83;
		internal System.Windows.Forms.ToolStripButton moveLastAdtButton;
		internal System.Windows.Forms.ToolStripSeparator toolStripSeparator84;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator86;
  private System.Windows.Forms.ToolStripButton refreshAdtButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator88;
		private System.Windows.Forms.ToolStripTextBox searchForAdtTextBox;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator89;
		private System.Windows.Forms.ToolStripLabel toolStripLabel20;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator90;
		private System.Windows.Forms.ToolStripComboBox searchInAdtComboBox;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator91;
		private System.Windows.Forms.ToolStripButton vwSQLAdtButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator95;
		private System.Windows.Forms.ToolStrip toolStrip6;
		internal System.Windows.Forms.ToolStripButton moveFirstEmlSvrButton;
		internal System.Windows.Forms.ToolStripSeparator toolStripSeparator98;
		internal System.Windows.Forms.ToolStripButton movePreviousEmlSvrButton;
		internal System.Windows.Forms.ToolStripSeparator toolStripSeparator99;
		internal System.Windows.Forms.ToolStripLabel toolStripLabel21;
		internal System.Windows.Forms.ToolStripTextBox positionEmlSvrTextBox;
		internal System.Windows.Forms.ToolStripLabel totalRecEmlSvrLabel;
		internal System.Windows.Forms.ToolStripSeparator toolStripSeparator100;
		internal System.Windows.Forms.ToolStripButton moveNextEmlSvrButton;
		internal System.Windows.Forms.ToolStripSeparator toolStripSeparator101;
  internal System.Windows.Forms.ToolStripButton moveLastEmlSvrButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator104;
    private System.Windows.Forms.ToolStripButton refreshEmlSvrButton;
		private System.Windows.Forms.ToolStripLabel toolStripLabel23;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator106;
		private System.Windows.Forms.ToolStripTextBox searchForEmlSvrTextBox;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator107;
		private System.Windows.Forms.ToolStripLabel toolStripLabel24;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator108;
		private System.Windows.Forms.ToolStripComboBox searchInEmlSvrComboBox;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator109;
		private System.Windows.Forms.ToolStripButton addEmlSvrButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator110;
		private System.Windows.Forms.ToolStripButton editEmlSvrButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator111;
		private System.Windows.Forms.ToolStripButton saveEmlSvrButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator112;
		private System.Windows.Forms.ToolStripButton vwSQLEmlSvrButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator113;
		private System.Windows.Forms.ToolStripButton recHstryEmlSvrButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator114;
		private System.Windows.Forms.GroupBox groupBox8;
		private System.Windows.Forms.Label label29;
		private System.Windows.Forms.Label label28;
		private System.Windows.Forms.Label label30;
		private System.Windows.Forms.Label label31;
		private System.Windows.Forms.Label label32;
		private System.Windows.Forms.Label label33;
		private System.Windows.Forms.CheckBox isDfltYesEmlSvrCheckBox;
		private System.Windows.Forms.NumericUpDown smtpPortNmUpDown;
		private System.Windows.Forms.TextBox emailPswdTextBox;
		private System.Windows.Forms.TextBox emailUnameTextBox;
		private System.Windows.Forms.TextBox smtpClientTextBox;
		private System.Windows.Forms.TextBox activeDrctryDmnTextBox;
		private System.Windows.Forms.CheckBox isDfltNoEmlSvrCheckBox;
		private System.Windows.Forms.CheckBox showSuccsflCheckBox;
		private System.Windows.Forms.ListView loginsListView;
		private System.Windows.Forms.CheckBox showFaildCheckBox;
		private System.Windows.Forms.Label label35;
		private System.Windows.Forms.TreeView auditTblsTreeView;
		private System.Windows.Forms.DataGridView auditTblsDataGridView;
		private System.Windows.Forms.Label label34;
		private System.Windows.Forms.ColumnHeader columnHeader38;
		private System.Windows.Forms.ColumnHeader columnHeader39;
		private System.Windows.Forms.ColumnHeader columnHeader40;
		private System.Windows.Forms.ColumnHeader columnHeader41;
		private System.Windows.Forms.ColumnHeader columnHeader42;
		private System.Windows.Forms.ColumnHeader columnHeader43;
		private System.Windows.Forms.ColumnHeader columnHeader44;
    private System.Windows.Forms.Panel panel17;
		private System.Windows.Forms.TextBox emlSrvrIDTextBox;
		private System.Windows.Forms.ToolStripComboBox dsplySizeLgnsComboBox;
		private System.Windows.Forms.ColumnHeader columnHeader45;
		private System.Windows.Forms.ToolStripComboBox dsplySizeAdtComboBox;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
  private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
  private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
  private System.Windows.Forms.ToolStripLabel toolStripLabel19;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator17;
    private System.Windows.Forms.Panel extraInfoPanel;
  private System.Windows.Forms.ListView extInfSubGroupsListView;
  private System.Windows.Forms.ColumnHeader columnHeader46;
  private System.Windows.Forms.ColumnHeader columnHeader47;
  private System.Windows.Forms.ColumnHeader columnHeader48;
  private System.Windows.Forms.Panel panel20;
  private glsLabel.glsLabel glsLabel18;
  private System.Windows.Forms.ListView extInfoModuleListView;
  private System.Windows.Forms.ColumnHeader columnHeader49;
  private System.Windows.Forms.ColumnHeader columnHeader50;
  private System.Windows.Forms.ColumnHeader columnHeader54;
  private System.Windows.Forms.Panel panel21;
  private glsLabel.glsLabel glsLabel19;
  private System.Windows.Forms.ToolStrip toolStrip7;
  internal System.Windows.Forms.ToolStripButton moveFirstExtInfButton;
  internal System.Windows.Forms.ToolStripSeparator toolStripSeparator33;
  internal System.Windows.Forms.ToolStripButton movePreviousExtInfButton;
  internal System.Windows.Forms.ToolStripSeparator toolStripSeparator36;
  internal System.Windows.Forms.ToolStripLabel toolStripLabel14;
  internal System.Windows.Forms.ToolStripTextBox positionExtInfTextBox;
  internal System.Windows.Forms.ToolStripLabel totalRecExtInfLabel;
  internal System.Windows.Forms.ToolStripSeparator toolStripSeparator46;
  internal System.Windows.Forms.ToolStripButton moveNextExtInfButton;
  internal System.Windows.Forms.ToolStripSeparator toolStripSeparator47;
  internal System.Windows.Forms.ToolStripButton moveLastExtInfButton;
  private System.Windows.Forms.ToolStripSeparator toolStripSeparator48;
  private System.Windows.Forms.ToolStripComboBox dsplySizeExtInfComboBox;
  private System.Windows.Forms.ToolStripSeparator toolStripSeparator67;
  private System.Windows.Forms.ToolStripLabel toolStripLabel22;
  private System.Windows.Forms.ToolStripSeparator toolStripSeparator69;
  private System.Windows.Forms.ToolStripTextBox searchForExtInfTextBox;
  private System.Windows.Forms.ToolStripSeparator toolStripSeparator74;
  private System.Windows.Forms.ToolStripLabel toolStripLabel25;
  private System.Windows.Forms.ToolStripSeparator toolStripSeparator75;
  private System.Windows.Forms.ToolStripComboBox searchInExtInfComboBox;
  private System.Windows.Forms.ToolStripSeparator toolStripSeparator76;
  private System.Windows.Forms.ToolStripButton refreshExtInfoButton;
  private System.Windows.Forms.Label label36;
  private System.Windows.Forms.ListView extInfLabelListView;
  private System.Windows.Forms.ColumnHeader columnHeader51;
  private System.Windows.Forms.ColumnHeader columnHeader52;
  private System.Windows.Forms.ColumnHeader columnHeader53;
  private System.Windows.Forms.Panel panel23;
  private glsLabel.glsLabel glsLabel21;
  private System.Windows.Forms.ColumnHeader columnHeader55;
  private System.Windows.Forms.ColumnHeader columnHeader57;
  private System.Windows.Forms.ColumnHeader columnHeader56;
  private System.Windows.Forms.ColumnHeader columnHeader58;
  private System.Windows.Forms.ColumnHeader columnHeader59;
  private System.Windows.Forms.ContextMenuStrip extInfMdlContextMenuStrip;
  private System.Windows.Forms.ToolStripMenuItem refreshExtInfMdlMenuItem;
  private System.Windows.Forms.ToolStripSeparator toolStripSeparator78;
  private System.Windows.Forms.ToolStripMenuItem viewSQLExtInfMdlMenuItem;
  private System.Windows.Forms.ContextMenuStrip subGroupsContextMenuStrip;
  private System.Windows.Forms.ToolStripMenuItem refreshSubGrpsMenuItem;
  private System.Windows.Forms.ToolStripSeparator toolStripSeparator85;
  private System.Windows.Forms.ToolStripMenuItem vwSQLSubGrpsMenuItem;
  private System.Windows.Forms.ContextMenuStrip extInfLabelContextMenuStrip;
  private System.Windows.Forms.ToolStripMenuItem refreshExtInfLblMenuItem;
  private System.Windows.Forms.ToolStripSeparator toolStripSeparator87;
  private System.Windows.Forms.ToolStripMenuItem vwSQLExtInfLblMenuItem;
  private System.Windows.Forms.ToolStripMenuItem addEditExtInfMenuItem;
  private System.Windows.Forms.ToolStripMenuItem recordHistoryExtInfToolStripMenuItem;
  private System.Windows.Forms.ToolStripMenuItem enableDisableToolStripMenuItem;
  private System.Windows.Forms.ToolStripMenuItem deleteLaToolStripMenuItem;
  private System.Windows.Forms.ToolStripSeparator toolStripSeparator92;
  private System.Windows.Forms.ContextMenuStrip loginsContextMenuStrip;
  private System.Windows.Forms.ToolStripMenuItem refreshLgnMenuItem;
  private System.Windows.Forms.ToolStripSeparator toolStripSeparator93;
  private System.Windows.Forms.ToolStripMenuItem vwSQLLgnMenuItem;
  private System.Windows.Forms.ContextMenuStrip auditContextMenuStrip;
  private System.Windows.Forms.ToolStripMenuItem refreshAdtMenuItem;
  private System.Windows.Forms.ToolStripSeparator toolStripSeparator94;
  private System.Windows.Forms.ToolStripMenuItem vwSQLAdtMenuItem;
  private System.Windows.Forms.ToolStripMenuItem exptAudtMenuItem;
  private System.Windows.Forms.ToolStripMenuItem exptLgnMenuItem;
  private System.Windows.Forms.ToolStripMenuItem exptMdlMenuItem;
  private System.Windows.Forms.ToolStripMenuItem exptUsrRolesMenuItem;
  private System.Windows.Forms.ToolStripMenuItem exptUsrsMenuItem;
  private System.Windows.Forms.ToolStripMenuItem exptRolesMenuItem;
  private System.Windows.Forms.ToolStripMenuItem exptMdlPrvldgMenuItem;
  private System.Windows.Forms.ToolStripMenuItem exptPlcyMenuItem;
  private System.Windows.Forms.ToolStripMenuItem exptRolePrvldgMenuItem;
  private System.Windows.Forms.ToolStripMenuItem exptExtInfMenuItem;
  private System.Windows.Forms.ToolStripMenuItem exptSubGrpMenuItem;
  private System.Windows.Forms.ToolStripMenuItem exptInfLblMenuItem;
    private System.Windows.Forms.Button imprtUsersButton;
    private System.Windows.Forms.Button exprtUsersButton;
    private System.Windows.Forms.OpenFileDialog openFileDialog1;
    private System.Windows.Forms.GroupBox groupBox9;
    private System.Windows.Forms.TextBox ftpBaseDirTextBox;
    private System.Windows.Forms.NumericUpDown ftpPortNumUpDown;
    private System.Windows.Forms.TextBox ftpPswdTextBox;
    private System.Windows.Forms.TextBox ftpUnmTextBox;
    private System.Windows.Forms.TextBox ftpServerTextBox;
    private System.Windows.Forms.Label label37;
    private System.Windows.Forms.Label label39;
    private System.Windows.Forms.Label label40;
    private System.Windows.Forms.Label label41;
    private System.Windows.Forms.Label label42;
    private System.Windows.Forms.ToolTip infoToolTip;
    private System.Windows.Forms.Label mailLabel;
    private System.Windows.Forms.CheckBox enforceFTPCheckBox;
    private System.Windows.Forms.ContextMenuStrip treeVWContextMenuStrip;
    private System.Windows.Forms.ToolStripMenuItem hideTreevwMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator123;
    private System.Windows.Forms.ToolStrip toolStrip9;
    private System.Windows.Forms.ToolStripButton addUserButton;
    private System.Windows.Forms.ToolStripButton editUserButton;
    private System.Windows.Forms.ToolStrip toolStrip8;
    private System.Windows.Forms.ToolStripButton addEdtUsrRoleButton;
    private System.Windows.Forms.Label label38;
    private System.Windows.Forms.ToolStrip toolStrip10;
    private System.Windows.Forms.ToolStripButton addEditRoleButton;
    private System.Windows.Forms.ToolStrip toolStrip11;
    private System.Windows.Forms.ToolStripButton addRoleButton;
    private System.Windows.Forms.ToolStripButton editRoleButton;
    private System.Windows.Forms.ToolStrip toolStrip12;
    private System.Windows.Forms.ToolStripButton delLblButton;
    private System.Windows.Forms.ToolStripButton enableDisableButton;
    private System.Windows.Forms.ToolStripButton addEditExtInfButton;
    private System.Windows.Forms.GroupBox groupBox10;
    private System.Windows.Forms.ComboBox timeoutComboBox;
    private System.Windows.Forms.Label label46;
    private System.Windows.Forms.ComboBox baudRateComboBox;
    private System.Windows.Forms.ComboBox comPortComboBox;
    private System.Windows.Forms.Label label45;
    private System.Windows.Forms.Label label44;
    private System.Windows.Forms.GroupBox groupBox11;
    private System.Windows.Forms.TextBox bckpFileDirTextBox;
    private System.Windows.Forms.TextBox pgDirTextBox;
    private System.Windows.Forms.Label label48;
    private System.Windows.Forms.Label label49;
    private System.Windows.Forms.Button bckpDirButton;
    private System.Windows.Forms.Button pgDirButton;
    private System.Windows.Forms.Button bckpButton;
    private System.Windows.Forms.Button restoreButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator22;
    private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    private System.Windows.Forms.NumericUpDown sessionNumUpDown;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label accDndLabel;
    private System.Windows.Forms.Timer timer1;
    private glsLabel.glsLabel glsLabel16;
    private System.Windows.Forms.ToolStripButton loadRolesButton;
    private System.Windows.Forms.Label waitLabel;
    private System.Windows.Forms.GroupBox groupBox12;
    private System.Windows.Forms.Button loadLOVsButton;
    private System.Windows.Forms.TextBox crntOrgTextBox;
    private System.Windows.Forms.PictureBox curOrgPictureBox;
    private System.Windows.Forms.Label label43;
    private System.Windows.Forms.Button crntOrgButton;
    public System.Windows.Forms.TextBox crntOrgIDTextBox;
    private System.Windows.Forms.Label label47;
    private System.Windows.Forms.Label label51;
    private System.Windows.Forms.Label label50;
    private System.Windows.Forms.Label label52;
    private System.Windows.Forms.Label label53;
    private System.Windows.Forms.Label label54;
    private System.Windows.Forms.Label label55;
    private System.Windows.Forms.Label label58;
    private System.Windows.Forms.Label label57;
    private System.Windows.Forms.Label label56;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.TabPage tabPage3;
    private System.Windows.Forms.TabPage tabPage4;
    private System.Windows.Forms.TabPage tabPage5;
    private System.Windows.Forms.TabPage tabPage6;
    private System.Windows.Forms.TabPage tabPage7;
    private System.Windows.Forms.TabPage tabPage8;
    private System.Windows.Forms.ToolStripButton delUserButton;
    private System.Windows.Forms.GroupBox groupBox13;
    private System.Windows.Forms.DataGridView smsDataGridView;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
    private System.Windows.Forms.ColumnHeader columnHeader60;
    private System.Windows.Forms.ColumnHeader columnHeader61;
    private System.Windows.Forms.TextBox ftpHomeDirTextBox;
    private System.Windows.Forms.Label label59;
        private System.Windows.Forms.ToolStripButton deleteSrvrButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator52;
        private System.Windows.Forms.ToolStripButton deletePolicyButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator60;
    }
	}
